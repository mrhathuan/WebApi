/// <reference path="../angular.min.js" />
var app = angular.module('myapp', ['ngRoute', 'kendo.directives']);

var Common = {
    Message: {
        Type: {
            Notify: 'Notify',
            Alert: 'Alert',
            Confirm: 'Confirm'
        },
        NotifyType: {
            INFO: 'info',
            SUCCESS: 'success',
            WARNING: 'warning',
            ERROR: 'error'
        },
    }
};
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
        $scope.Default_ShowUserMessage = false;
        $rootScope.IsShowMenu = true;
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
                    Icon: 'fa fa-bars'
                },
                {
                    NameChild: 'Danh mục',
                    IsActive: false,
                    Link: '#',
                    Icon: 'fa fa-align-justify'
                },
                {
                    NameChild: 'Đơn hàng',
                    IsActive: false,
                    Link: '#',
                    Icon: 'fa fa-clipboard'
                }
                ]
            },
            {
                Name: 'Quản trị', Childs: [
                    {
                        NameChild: 'Nội dung',
                        IsActive: false,
                        Link: '#',
                        Icon: 'fa fa-cog'
                    },
                    {
                        NameChild: 'Tài khoản',
                        IsActive: false,
                        Link: '#',
                        Icon: 'fa fa-user'
                    }
                ]
            }
        ];

        $scope.MenuClick = function ($event) {
            $event.preventDefault();
            $rootScope.IsShowMenu = !$rootScope.IsShowMenu;
            $(document).find(".cus-splitter.ver-splitter > .k-pane").each(function (i, v) {
                $(v).css('width', '100%');
                $(v).find(".cus-splitter.hor-splitter").each(function (j, o) {
                    try {
                        $timeout(function () {
                            $(o).data('kendoSplitter').resize();
                        }, 101);
                    } catch (e) { }
                })
            })
            $(document).find(".cus-splitter.ver-splitter > .k-splitbar").each(function (i, v) {
                $(v).css('width', '100%');
            })

            $(document).find(".cus-splitter").each(function (i, v) {
                $timeout(function () {
                    $(v).data('kendoSplitter').resize();
                }, 110)
            });
            $(document).find(".cus-grid.k-grid").each(function (i, v) {
                $timeout(function () {
                    $(v).data('kendoGrid').resize();
                }, 110)
            });
            $(document).find(".cus-chart.k-chart").each(function (i, v) {
                $timeout(function () {
                    $(v).data('kendoChart').resize();
                }, 110)
            });
            $(document).find(".cus-chart.k-gauge").each(function (i, v) {
                $timeout(function () {
                    $(v).data('kendoRadialGauge').resize();
                }, 110)
            });
            $(document).find(".cus-map").each(function (i, v) {
                $timeout(function () {
                    try {
                        $(v).data('openMap').Resize();
                    } catch (e) { }
                }, 150)
            });
        };
        $scope.OnView_Resize = function (callback) {
            $(document).find(".cus-splitter.ver-splitter > .k-pane").each(function (i, v) {
                $(v).css('width', '100%');
                $(v).find(".cus-splitter").each(function (j, o) {
                    $(o).data('kendoSplitter').resize();
                })
            })
            $(document).find(".cus-splitter.ver-splitter > .k-splitbar").each(function (i, v) {
                $(v).css('width', '100%');
            })
            if (Common.HasValue(callback)) {
                callback();
            }
        }


        $scope.ShowProfileClick = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            $scope.Default_ShowProfile = !$scope.Default_ShowProfile;
        }

        $scope.HideProfileClick = function () {
            $scope.Default_ShowProfile = false;
        }

        $scope.UserMessage_Click = function ($event) {
            $event.preventDefault();
            $scope.Default_ShowUserMessage = !$scope.Default_ShowUserMessage;
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

        $scope.msgOptions = {
            autoHideAfter: 5000,
            allowHideAfter: 1000,
            position: { top: 120 },
            stacking: "down",
            templates: [{
                type: "info",
                template: "<div class='notify info'><i style='display: none' class='fa fa-exclamation-circle'></i><span>#=Msg#</span></div>"
            }, {
                type: "success",
                template: "<div class='notify success'><i style='display: none' class='fa fa-check-circle-o'></i><span>#=Msg#</span></div>"
            }, {
                type: "warning",
                template: "<div class='notify warning'><i style='display: none' class='fa fa-exclamation-triangle'></i><span>#=Msg#</span></div>"
            }, {
                type: "error",
                template: "<div class='notify error'><i style='display: none' class='fa fa-exclamation-triangle'></i><span>#=Msg#</span></div>"
            }],
            show: function (e) {
                var con = e.element.parent();
                //con.css("left", "")
                //con.css("right", "10px");
                //con.css("opacity", "0.9");
                con.css("z-index", "999999");
                //con.css("border-radius", "5px");
                //e.element.css("border-radius", "5px");
            }
        };
        $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
        $rootScope.Message = function (options) {
            options = $.extend(true, {
                Type: Common.Message.Type.Notify,
                NotifyType: Common.Message.NotifyType.INFO,
                Title: 'Thông báo',
                Msg: '',
                Action: true,
                Close: null,
                Ok: null,
                pars: null
            }, options);
            $rootScope.winmessage_Item = options;

            switch (options.Type) {
                case Common.Message.Type.Notify: $scope.msg.show({ Msg: $rootScope.winmessage_Item.Msg }, $rootScope.winmessage_Item.NotifyType); break;
                case Common.Message.Type.Alert:
                    $scope.winmessage_title = $rootScope.winmessage_Item.Title;
                    $scope.winmessage_msg = $rootScope.winmessage_Item.Msg;
                    $scope.winmessage_IsAlert = true;
                    if ($scope.winmessage != null) {
                        $scope.winmessage.center();
                        $scope.winmessage.open();

                        $timeout(function () {
                            var msg = $scope.winmessage.element.find('div.winmessage_msg span');
                            var height = msg.height() + 120;
                            $scope.winmessage.setOptions({
                                height: height
                            });
                        }, 300);

                        $timeout(function () {
                            $scope.winmessage.element.find('a.close').focus();
                        }, 400);
                    }
                    break;
                case Common.Message.Type.Confirm:
                    $scope.winmessage_title = $rootScope.winmessage_Item.Title;
                    $scope.winmessage_msg = $rootScope.winmessage_Item.Msg;
                    $scope.winmessage_IsAlert = false;
                    if ($scope.winmessage != null) {
                        $scope.winmessage.center();
                        $scope.winmessage.open();

                        $timeout(function () {
                            var msg = $scope.winmessage.element.find('div.winmessage_msg span');
                            var height = msg.height() + 120;
                            $scope.winmessage.setOptions({
                                height: height
                            });
                        }, 300);

                        $timeout(function () {
                            $scope.winmessage.element.find('a.accept').focus();
                        }, 400);
                    }
                    break;
            }
        };

        $rootScope.winmessage_Item = null;
        $scope.winmessage_IsAlert = false;
        $scope.winmessage_msg = '';
        $scope.winmessage_title = '';
        $scope.winmessage_Win_Close = function ($event) {
            if ($rootScope.winmessage_Item.Action && $rootScope.winmessage_Item.Close != null)
                $rootScope.winmessage_Item.Close();
        };
        $scope.winmessage_Close_Click = function ($event) {
            $event.preventDefault();

            $scope.winmessage.close();
        };
        $scope.winmessage_Keydown = function ($event) {
            if (angular.element($event.target).hasClass('close')) {
                if ($event.keyCode == 27) {
                    $scope.winmessage.close();
                }
            }
            else if (angular.element($event.target).hasClass('accept')) {
                if ($event.keyCode == 27) {
                    $scope.winmessage.close();
                }
            }
        };
        $scope.winmessage_Save_Click = function ($event) {
            $event.preventDefault();

            $rootScope.winmessage_Item.Action = false;
            if ($rootScope.winmessage_Item.Ok != null)
                $rootScope.winmessage_Item.Ok($rootScope.winmessage_Item.pars);
            $scope.winmessage.close();
        };
    }
]);