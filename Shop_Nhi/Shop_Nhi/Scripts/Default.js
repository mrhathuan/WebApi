angular.module('myapp').factory('signalRHubProxy', ['$rootScope', 'signalRServer', function ($rootScope, signalRServer) {
    function signalRHubProxyFactory(serverUrl, hubName, options) {
        options = $.extend(true, {
            //start: { logging: true, transport: 'longPolling' },
            start: { logging: true },
            jsonp: true,
            done: null, //function
            fail: null, //function
        }, options);

        var connection = $.hubConnection(signalRServer);

        var proxy = connection.createHubProxy(hubName);
        connection.start(options.start).done(function () {
            if (options.done != null)
                options.done();
        }).fail(function () {
            if (options.fail != null)
                options.fail();
        });

        return {
            on: function (eventName, callback) {
                proxy.on(eventName, function (result) {
                    $rootScope.$apply(function () {
                        if (callback) {
                            callback(result);
                        }
                    });
                });
            },
            off: function (eventName, callback) {
                proxy.off(eventName, function (result) {
                    $rootScope.$apply(function () {
                        if (callback) {
                            callback(result);
                        }
                    });
                });
            },
            invoke: function (methodName, callback) {
                proxy.invoke(methodName)
                    .done(function (result) {
                        $rootScope.$apply(function () {
                            if (callback) {
                                callback(result);
                            }
                        });
                    });
            },
            connection: connection
        };
    };

    return signalRHubProxyFactory;
}]);

angular.module('myapp').run(['$rootScope', '$location', '$state', '$urlRouter', function ($rootScope, $location, $state, $urlRouter) {
    $rootScope.$on('$stateChangeStart', function (event, toState, toParams, fromState, fromParams) {
        if ($rootScope.Default_ShowUserMessage == true) {
            $rootScope.Default_ShowUserMessage = false;
        }
        if (Common.HasValue($rootScope.ListState) && toState.name != 'main') {
            var key = toState.name.split('.')[1];
            if (Common.HasValue($rootScope.ListState[key]) && $rootScope.FunctionItem.ID > 0) {
                var itemState = $rootScope.ListState[key];
                var item = $rootScope.FunctionItem;
                if (item.ID != itemState.ID) {
                    $rootScope.FunctionItem = itemState;
                    if (item.IDMain != itemState.IDMain) {
                        angular.forEach($rootScope.Functions, function (parent, i) {
                            angular.forEach(parent.Childs, function (child, i) {
                                if (child.IDMain == itemState.IDMain)
                                    child.IsActive = true;
                                else
                                    child.IsActive = false;

                                if (child.ID == itemState.ID) {
                                    $rootScope.FunctionItem = child;
                                    $rootScope.Breakumb = $.extend(true, [], child.Breakumb);
                                    $rootScope.BreakumbDetail = $.extend(true, [], child.BreakumbDetail);
                                }
                            });
                        });
                    }
                }
                else if (item.Childs.length != itemState.Childs.length) {
                    angular.forEach($rootScope.Functions, function (parent, i) {
                        angular.forEach(parent.Childs, function (child, i) {
                            if (child.ID == itemState.ID) {
                                $rootScope.FunctionItem = child;
                                $rootScope.Breakumb = $.extend(true, [], child.Breakumb);
                                $rootScope.BreakumbDetail = $.extend(true, [], child.BreakumbDetail);
                            }
                        });
                    });
                }
            }
        }
        else if (fromState.name == 'main' && toState.name.split('.').length > 2) {
            var key = toState.name.split('.')[1];
            if (Common.HasValue($rootScope.ListState) && $rootScope.FunctionItem.ID > 0) {
                var itemState = $rootScope.ListState[key];
                var item = $rootScope.FunctionItem;
                if (item.ID != itemState.ID) {
                    $rootScope.FunctionItem = itemState;
                    if (item.IDMain != itemState.IDMain) {
                        angular.forEach($rootScope.Functions, function (parent, i) {
                            angular.forEach(parent.Childs, function (child, i) {
                                if (child.IDMain == itemState.IDMain)
                                    child.IsActive = true;
                                else
                                    child.IsActive = false;

                                if (child.ID == itemState.ID) {
                                    $rootScope.FunctionItem = child;
                                    $rootScope.Breakumb = $.extend(true, [], child.Breakumb);
                                    $rootScope.BreakumbDetail = $.extend(true, [], child.BreakumbDetail);
                                }
                            });
                        });
                    }
                }
                else if (item.Childs.length != itemState.Childs.length) {
                    angular.forEach($rootScope.Functions, function (parent, i) {
                        angular.forEach(parent.Childs, function (child, i) {
                            if (child.ID == itemState.ID) {
                                $rootScope.FunctionItem = child;
                                $rootScope.Breakumb = $.extend(true, [], child.Breakumb);
                                $rootScope.BreakumbDetail = $.extend(true, [], child.BreakumbDetail);
                            }
                        });
                    });
                }
            }
        }
        //Common.Log('$stateChangeStart to ' + toState.name + '- fired when the transition begins. toState,toParams : \n', toState, toParams);
    });

    $rootScope.$on('$stateChangeSuccess', function (event, toState, toParams, fromState, fromParams) {
        //if (Common.HasValue($rootScope.FunctionItem)) {
        //    if ($rootScope.FunctionItem.ID > 0) {
        //        $rootScope.QuickAddItem.Show = true;
        //        $rootScope.FunctionItem.Code = toState.name;
        //        $rootScope.FunctionItem.Params = toParams;
        //        var item = $rootScope.FunctionItem;
        //        Common.Cookie.Set('main_ItemMenu', JSON.stringify([{ IDMain: item.IDMain, ID: item.ID, Code: item.Code, CodeView: item.CodeView, ListActions: item.ListActions, Params: item.Params }]));
        //    }
        //}
    });
}]);

angular.module('myapp').directive('expandKGrid', ['$window', '$compile', function ($window, $compile) {
    var directive = {
        link: function (scope, element, attrs) {
            var gridElement = $(element);
            $($window).resize(function () {
                //var dataElement = gridElement.find('.k-grid-content');
                //var nonDataElements = gridElement.children().not('.k-grid-content');
                //var currentGridHeight = gridElement.innerHeight();
                //var nonDataElementsHeight = 0;
                //nonDataElements.each(function () { nonDataElementsHeight += $(this).outerHeight(); });
                //dataElement.height(currentGridHeight - nonDataElementsHeight);
            });
            setTimeout(function () {
                //Identify grid
                //var grid = $(gridElement).data('kendoGrid');
                //if (Common.HasValue(grid)) {
                //    grid.bind('dataBinding', function (e) {
                //        $(this.element).find('.k-grid-content').scrollTop(0);
                //        $(this.element).find('.k-grid-content').perfectScrollbar('destroy');
                //        $(this.element).find('.k-grid-content').perfectScrollbar({
                //            minScrollbarLength: 30
                //        });
                //        $(this.element).find('.k-grid-header').css('padding-right', '0');
                //        $(this.element).find('.k-grid-content-locked').css('height', $(this.element).find('.k-grid-content-locked').height() + 17);
                //    });
                //}
                //$(gridElement).find('.k-grid-content').perfectScrollbar('destroy');
                //$(gridElement).find('.k-grid-content').perfectScrollbar({
                //    minScrollbarLength: 30
                //});
                //$(gridElement).find('.k-grid-header').css('padding-right', '0');
                //$(gridElement).find('.k-grid-content-locked').css('height', $(gridElement).find('.k-grid-content-locked').height() + 17);
                //$(gridElement).find(".k-grid-header th span[data-role='filtercell']").each(function () {
                //    var menu = $(this).data('kendoFilterCell');
                //    if (Common.HasValue(menu)) {
                //        try {
                //            var auto = menu.input.data('kendoAutoComplete');
                //            if (auto != null) {
                //                auto.bind('open', function (e) {
                //                    var me = $(this.ul).closest('.k-list-scroller');
                //                    setTimeout(function () {
                //                        me.css('overflow', 'hidden');
                //                        me.scrollTop(0);
                //                        me.perfectScrollbar('destroy');
                //                        me.perfectScrollbar({
                //                            minScrollbarLength: 50,
                //                            suppressScrollX: true
                //                        });
                //                    }, 100)
                //                })
                //            }
                //            var cbb = menu.input.data('kendoComboBox');
                //            if (cbb != null) {

                //            }
                //        } catch (e) { }
                //    }
                //});
            }, 100);
        }
    };
    return directive;
}]);

angular.module('myapp').directive('scrolldiv', ['$window', function ($window) {
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

angular.module('myapp').directive('expandKSplitter', ['$window', function ($window) {
    var directive = {
        controller: function ($element, $timeout) {
            //$element.children('div').perfectScrollbar({
            //    minScrollbarLength: 50
            //});
        },
        restrict: 'A'
    };
    return directive;
}]);

angular.module('myapp').directive('expandKTabstrip', ['$window', function ($window) {
    var directive = {
        controller: function ($element, $timeout) {
            //$element.children('div').perfectScrollbar({
            //    minScrollbarLength: 50
            //});
            //$timeout(function () {
            //    var tab = $element.data('kendoTabStrip');
            //    if (Common.HasValue(tab)) {
            //        tab.bind('select', function (e) {
            //            $(e.contentElement).scrollTop(0);
            //        })
            //    }
            //}, 100)
        },
        restrict: 'A'
    };
    return directive;
}]);

angular.module('myapp').directive('expandKScheduler', ['$window', function ($window) {
    var directive = {
        link: function (scope, element, attrs) {
            //var gridElement = $(element);
            //setTimeout(function () {
            //    //Identify grid
            //    var grid = $(gridElement).data('kendoScheduler');
            //    if (Common.HasValue(grid)) {
            //        grid.bind('dataBinding', function (e) {
            //            $(this.element).find('.k-scheduler-content').scrollTop(0);
            //            $(this.element).find('.k-scheduler-content').perfectScrollbar('destroy');
            //            $(this.element).find('.k-scheduler-content').perfectScrollbar({
            //                minScrollbarLength: 30
            //            });
            //            $(this.element).find('.k-scheduler-header').css('padding-right', '0');
            //            $(this.element).find('.k-scheduler-layout > tbody > tr:eq(1) .k-scheduler-times').css('height', $(this.element).find('.k-scheduler-layout > tbody > tr:eq(1) .k-scheduler-times').height() + 17);
            //        });
            //    }
            //    $(gridElement).find('.k-scheduler-content').perfectScrollbar('destroy');
            //    $(gridElement).find('.k-scheduler-content').perfectScrollbar({
            //        minScrollbarLength: 30
            //    });
            //    $(gridElement).find('.k-scheduler-header').css('padding-right', '0');
            //    $(gridElement).find('.k-scheduler-layout > tbody > tr:eq(1) .k-scheduler-times').css('height', $(gridElement).find('.k-scheduler-layout > tbody > tr:eq(1) .k-scheduler-times').height() + 17);
            //}, 100);
        }
    };
    return directive;
}]);

angular.module('myapp').directive('focusKDatetimepicker', ['$window', function ($window) {
    var directive = {
        controller: function ($element, $timeout) {
            $timeout(function () {
                var picker = $element.data("kendoDateTimePicker");
                if (Common.HasValue(picker)) {
                    $element.on("click", function (e) {
                        $timeout(function () {
                            if (!picker.dateView.popup.wrapper.is(":visible") && !picker.timeView.popup.wrapper.is(":visible"))
                                picker.open();
                        }, 100)
                    });
                    $element.bind("focus", function (e) {
                        $timeout(function () {
                            if (!picker.dateView.popup.wrapper.is(":visible") && !picker.timeView.popup.wrapper.is(":visible"))
                                picker.open();
                        }, 100)
                    });
                    picker.bind("open", function (e) {
                        switch (e.view) {
                            case 'date':
                                var widget = this.dateView.calendar.wrapper[0];
                                var wrapper = this.wrapper;
                                var w = this.wrapper.width();
                                var pLeft = w + wrapper.position().left;
                                var oLeft = w + this.wrapper.offset().left
                                $timeout(function () {
                                    try {
                                        var container = $(widget).closest('.k-animation-container');
                                        if (Common.HasValue(container[0])) {
                                            var win = container[0];
                                            var w1 = $(win).width();
                                            if (pLeft > w1) {
                                                win.style.left = oLeft - w1 + 'px';
                                            }
                                            win.style['z-index'] = 999999;
                                        }
                                    } catch (e) {
                                    }
                                }, 1)
                                break;
                            case 'time':
                                var container = this.timeView.popup.element;
                                $timeout(function () {
                                    try {
                                        var win = $(container).closest('.k-animation-container')[0];
                                        win.style['z-index'] = 999999;
                                    } catch (e) {
                                    }
                                }, 1);
                                break;
                            default:
                                break;
                        }
                    });
                    //picker.timeView.popup.element.perfectScrollbar();
                }
            }, 300)
        },
        restrict: 'A'
    };
    return directive;
}]);

angular.module('myapp').directive('focusKDatepicker', ['$window', function ($window) {
    var directive = {
        controller: function ($element, $timeout) {
            $timeout(function () {
                var picker = $element.data("kendoDatePicker");
                if (Common.HasValue(picker)) {
                    $element.on("click", function () {
                        $(this).data("kendoDatePicker").open();
                    });
                    $element.bind("focus", function () {
                        $(this).data("kendoDatePicker").open();
                    });
                    picker.bind("open", function (e) {
                        var widget = this.dateView.calendar.wrapper[0];
                        var wrapper = this.wrapper;
                        var w = this.wrapper.width();
                        var pLeft = w + wrapper.position().left;
                        var oLeft = w + this.wrapper.offset().left
                        $timeout(function () {
                            var container = $(widget).closest('.k-animation-container');
                            if (Common.HasValue(container[0])) {
                                var win = container[0];
                                var w1 = $(win).width();
                                if (pLeft > w1) {
                                    win.style.left = oLeft - w1 + 'px';
                                }
                                win.style['z-index'] = 999999;
                            }
                        }, 1)
                    });
                }
            }, 300)
        },
        restrict: 'A'
    };
    return directive;
}]);

angular.module('myapp').directive('focusKTimepicker', ['$window', function ($window) {
    var directive = {
        controller: function ($element, $timeout) {
            $timeout(function () {
                var picker = $element.data("kendoTimePicker");
                if (Common.HasValue(picker)) {
                    $element.bind("focus", function () {
                        $(this).data("kendoTimePicker").open();
                    });
                    $element.on("click", function () {
                        $(this).data("kendoTimePicker").open();
                    });
                    picker.bind("open", function (e) {
                        var container = this.timeView.popup.element;
                        $timeout(function () {
                            try {
                                var win = $(container).closest('.k-animation-container')[0];
                                win.style['z-index'] = 999999;
                            } catch (e) {
                            }
                        }, 1);
                    });
                    //picker.timeView.popup.element.perfectScrollbar();
                }
            }, 300)
        },
        restrict: 'A'
    };
    return directive;
}]);

angular.module('myapp').directive('focusKCombobox', ['$window', function ($window) {
    var directive = {
        controller: function ($element, $timeout) {
            $timeout(function () {
                try {
                    var widget = $element.getKendoComboBox();
                    widget.input.on("focus", function () {
                        try {
                            widget.open();
                        } catch (e) {
                        }
                    });
                    widget.input.on("click", function () {
                        try {
                            widget.open();
                        } catch (e) {
                        }
                    });

                    //$(widget.popup.element).find('.k-list-scroller').css('overflow', 'hidden');
                    //$(widget.popup.element).find('.k-list-scroller').perfectScrollbar();
                }
                catch (e) { Common.Log("Combobox not found!") }
            }, 300)
        },
        restrict: 'A'
    };
    return directive;
}]);

angular.module('myapp').directive('focusKSelect', ['$window', function ($window) {
    var directive = {
        controller: function ($element, $timeout) {
            $timeout(function () {
                try {
                    var widget = $element.getKendoMultiSelect();
                    widget.input.on("focus", function () {
                        try {
                            widget.open();
                        } catch (e) {
                        }
                    });

                    //$(widget.popup.element).find('.k-list-scroller').css('overflow', 'hidden');
                    //$(widget.popup.element).find('.k-list-scroller').perfectScrollbar();
                }
                catch (e) { Common.Log("Select not found!") }
                try {
                    var widget = $element.getKendoAutoComplete();

                    //$(widget.popup.element).find('.k-list-scroller').css('overflow', 'hidden');
                    //$(widget.popup.element).find('.k-list-scroller').perfectScrollbar();
                }
                catch (e) { Common.Log("AutoComplete not found!") }
            }, 300)
        },
        restrict: 'A'
    };
    return directive;
}]);

angular.module('myapp').directive('draggableKWindow', ['$window', function ($window) {
    var directive = {
        controller: function ($element, $timeout) {
            $timeout(function () {
                try {
                    var win = $element.data('kendoWindow');
                    if (Common.HasValue(win)) {
                        //Draggable
                        var widget = $element.closest('.k-widget.k-window');
                        var handle = $element.find('.form-header')[0];
                        widget.draggable({
                            handle: handle
                        });

                        var flag = $element.attr('allowexit');
                        if (win.options.modal == true && flag) {
                            win.bind('open', function () {
                                $timeout(function () {
                                    $(document).find('.k-overlay').click(function (e) {
                                        win.close();
                                    })
                                }, 100)
                            })
                        }
                    }
                }
                catch (e) { Common.Log("Window not found!") }
            }, 100)
        },
        restrict: 'A'
    };
    return directive;
}]);

angular.module('myapp').directive('vForm', [function () {
    var directive = {
        controller: function ($scope, $element, $attrs) {
            var key = $attrs.vForm;
            $scope[key] = function (options) {
                options = $.extend(true, {
                    clear: false,
                    model: '',
                    error: ''
                }, options);

                if (options.clear == true) {
                    angular.forEach($element.find('div.error.show'), function (v, i) {
                        var error = angular.element(v);
                        error.removeClass('show');
                    });
                }
                else if (options.model != '') {
                    var lst = $element.find("input[ng-model='" + options.model + "']");
                    if (!Common.HasValue(lst))
                        lst = $element.find("textarea[ng-model='" + options.model + "']");
                    if (Common.HasValue(lst)) {
                        var error = angular.element(lst.closest('div.rowinput')).find('div.error');
                        if (Common.HasValue(error)) {
                            if (!error.hasClass('show'))
                                error.addClass('show');
                            error.find('a').attr('title', options.error);
                        }
                    }
                    return false;
                }
                else {
                    var reEmail = /^(([^<>()\[\]\.,;:\s@\"]+(\.[^<>()\[\]\.,;:\s@\"]+)*)|(\".+\"))@(([^<>()[\]\.,;:\s@\"]+\.)+[^<>()[\]\.,;:\s@\"]{2,})$/i;
                    var lst = $element.find("input,textarea");
                    var IsValid = true;
                    angular.forEach(lst, function (v, i) {
                        var bValid = true;
                        var sValid = '';

                        if (angular.element(v).attr("v-form-cbb") == 'true') {
                            var cbb = $scope[$(v).attr('kendo-combobox')];
                            if (cbb.select() < 0) {
                                bValid = false;
                                sValid = 'Chưa nhập';
                            }
                        }

                        if (angular.element(v).attr("v-form-require") == 'true') {
                            if (!Common.HasValue(v.value) || v.value == '') {
                                bValid = false;
                                sValid = 'Chưa nhập';
                            }
                        }
                        if (bValid == true && angular.element(v).attr("v-form-email") == 'true') {
                            if (!reEmail.test(v.value)) {
                                bValid = false;
                                sValid = 'Nhập sai email';
                            }
                        }
                        if (bValid == true && Common.HasValue(angular.element(v).attr("v-form-length"))) {

                            var val = angular.element(v).attr("v-form-length");
                            if (val.length > 0) {
                                if (val.indexOf('-') >= 0) {
                                    var arr = angular.element(v).attr("v-form-length").split('-');
                                    var minLength = arr.length == 2 ? arr[0] : 0;
                                    var maxLength = arr.length == 1 ? arr[0] : arr.length == 2 ? arr[1] : 0;
                                    if (minLength > 0 && v.value.length < minLength)
                                        bValid = false;
                                    if (maxLength > 0 && v.value.length > maxLength)
                                        bValid = false;

                                    if (bValid == false) {
                                        if (minLength > 0 && maxLength > 0)
                                            sValid = 'Nhập cho phép nhập ' + minLength + '-' + maxLength + ' ký tự';
                                        else if (minLength > 0)
                                            sValid = 'Nhập cho phép nhập ít nhất ' + minLength + ' ký tự';
                                        else if (maxLength > 0)
                                            sValid = 'Nhập cho phép nhập nhiều nhất ' + minLength + ' ký tự';
                                    }
                                }
                                else {
                                    if (v.value.length > parseInt(val)) {
                                        bValid = false;
                                        sValid = 'Chỉ được nhập ' + val + ' ký tự';
                                    }
                                }
                            }
                        }

                        if (bValid == false)
                            IsValid = false;

                        var error = angular.element(v.closest('div.rowinput')).find('div.error');
                        if (Common.HasValue(error)) {
                            if (bValid) {
                                if (error.hasClass('show'))
                                    error.removeClass('show');
                                error.find('a').attr('title', '');
                            }
                            else {
                                if (!error.hasClass('show'))
                                    error.addClass('show');
                                error.find('a').attr('title', sValid);
                            }
                        }
                    });
                    return IsValid;
                }
            }
        },
        restrict: 'A'
    };
    return directive;
}]);

angular.module('myapp').directive('mainSetting', ['$document', function ($document) {
    var directive = {
        controller: function ($rootScope, $element, $attrs) {
            $rootScope.isPopupVisible = false;

            $rootScope.PopupVisible = function (target) {
                $rootScope.Target = target;
                $rootScope.isPopupVisible = !$rootScope.isPopupVisible;
            }

            $document.bind('click', function ($event) {
                var isClickedElement = $element
                  .find($event.target)
                  .length > 0;

                if (isClickedElement)
                    return;
                if ($event.target == $rootScope.Target)
                    return;

                $rootScope.isPopupVisible = false;
                $rootScope.$apply();
            });
        },
        restrict: 'A'
    };
    return directive;
}]);

angular.module('myapp').directive('clickAnywhereButMessage', ['$document', '$parse', function ($document, $parse) {
    return {
        restrict: 'A',
        scope: {
            clickAnywhereButMessage: '&'
        },
        link: function (scope, element, attr, ctrl) {
            var handler = function (event) {
                if (!element[0].contains(event.target)) {
                    scope.$apply(function () {
                        scope.clickAnywhereButMessage({ $event: event });
                    });
                }
            };

            $document.on('click', handler);
            scope.$on('$destroy', function () {
                $document.off('click', handler);
            });
        }
    }
}]);

angular.module('myapp').config(['$stateProvider', function ($stateProvider) {
    var states = [];

    states.push({
        name: 'login', url: "^/login",
        views: {
            'view': { templateUrl: featuresA + 'Features/login.html' + featuresV, controller: 'loginController' }
        }
    });
    states.push({
        cache: false,
        name: 'main', url: "^/main",
        views: {
            'view': { templateUrl: featuresA + 'Features/main.html' + featuresV, controller: 'mainController' }
        }
    });

    $.each(features, function (i, v) {
        states.push({
            cache: v.cache,
            name: v.name, url: v.url,
            views: {
                'view': { templateUrl: v.templateUrl, controller: v.controller }
            }
        });
    });

    angular.forEach(states, function (state) { $stateProvider.state(state); });
}]);

//#region Data
var _default = {
    URL: {
        GetAuthorization: 'App_GetAuthorization',
        Logout: 'App_Logout',
        ListResource: 'App_ListResource',
        ListResourceEmpty: 'App_ListResourceEmpty',
        FileList: 'App_FileList',
        FileSave: 'App_FileSave',
        FileDelete: 'App_FileDelete',
        CommentList: 'App_CommentList',
        CommentSave: 'App_CommentSave',
        MessageCall_User: 'App_MessageCall_User',
        MessageCall_LoadMore: 'App_MessageCall_LoadMore',
        MessageCall_Read: 'App_MessageCall_Read',

        FileHandler: '/Handler/File.ashx',
        NoImage: '/Images/empty.jpg',

        SYSSearchDI: 'SYSSearchDI',
        SYSSearchCO: 'SYSSearchCO',

        Notification_Read: 'Notification_Read',
    },
    Data: {
        ExcelColumns: ["A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z",
            "AA", "AB", "AC", "AD", "AE", "AF", "AG", "AH", "AI", "AJ", "AK", "AL", "AM", "AN", "AO", "AP", "AQ", "AR", "AS", "AT", "AU", "AV", "AW", "AX", "AY", "AZ",
            "BA", "BB", "BC", "BD", "BE", "BF", "BG", "BH", "BI", "BJ", "BK", "BL", "BM", "BN", "BO", "BP", "BQ", "BR", "BS", "BT", "BU", "BV", "BW", "BX", "BY", "BZ",
            "CA", "CB", "CC", "CD", "CE", "CF", "CG", "CH", "CI", "CJ", "CK", "CL", "CM", "CN", "CO", "CP", "CQ", "CR", "CS", "CT", "CU", "CV", "CW", "CX", "CY", "CZ"]
    }
};
//#endregion

angular.module('myapp').controller('defaultController', ['$rootScope', '$scope', '$http', '$sce', '$location', '$state', '$timeout', '$window', '$compile', 'signalRHubProxy', function ($rootScope, $scope, $http, $sce, $location, $state, $timeout, $window, $compile, signalRHubProxy) {
    Common.Log('defaultController');

    $rootScope.RS = {};
    $rootScope.RSS = {};
    Common.Services.Call($http, {
        url: Common.Services.url.SYS,
        method: _default.URL.ListResourceEmpty,
        data: { dt: new Date() },
        success: function (res) {
            var RS = {};
            var RSS = {};
            angular.forEach(res, function (v, i) {
                var table = v.Key.substr(0, v.Key.indexOf('.'));
                var col = v.Key.substr(v.Key.indexOf('.') + 1);
                var current = RS[table];
                if (!Common.HasValue(current)) {
                    RS[table] = {};
                    RSS[table] = {};
                }
                RS[table][col] = v.Name;
                RSS[table][col] = v.ShortName;
            });
            $rootScope.RS = RS;
            $rootScope.RSS = RSS;
        }
    });

    $rootScope.QuickAddItem = {
        Show: false, Call: function ($event) {
            $event.preventDefault();

            $state.go('main.ORDOrder.New');
            $scope.IsDetail = true;
        }
    };
    $rootScope.QuickAdd = function (options) {
        options = $.extend(true, {
            Show: false,
            Call: null
        }, options);

        $rootScope.QuickAddItem.Call = null;
        $rootScope.QuickAddItem.Show = options.Show;
        $rootScope.QuickAddItem.Call = options.Call;
    };
    $rootScope.ServerTime = { IsConnected: false, Date: null, Day: '', Month: '', Hour: '', Minute: '' };

    $timeout(function () {
        $rootScope.signalProxy = signalRHubProxy(signalRHubProxy.defaultServer, 'clientHub', {
            done: function () {

            },
            fail: function () {
                $rootScope.ServerTime.IsConnected = false;
            }
        });

        $rootScope.signalProxy.on('serverTime', function (data) {
            var dt = new Date(data);
            $rootScope.ServerTime.IsConnected = true;
            $rootScope.ServerTime.Month = (dt.getMonth() + 1) + '';
            $rootScope.ServerTime.Day = dt.getDate() + '';
            $rootScope.ServerTime.Hour = dt.getHours() + '';
            $rootScope.ServerTime.Minute = dt.getMinutes() + '';
            $rootScope.ServerTime.Date = dt;
        });


        $rootScope.signalProxy.on('eventcommonworkflow', function (data) {
            angular.forEach(data.sender, function (v, i) {
                if (!Common.HasValue(Common.ClientHub._events[v.Code]))
                    Common.ClientHub._events[v.Code] = [];
                else {
                    angular.forEach(Common.ClientHub._events[v.Code], function (f, i) {
                        if (Common.HasValue(f))
                            f(data);
                    });
                }
            });
        });

        //if (!Common.HasValue(Common.ClientHub._events['ditoMasterChangeStatus']))
        //    Common.ClientHub._events['ditoMasterChangeStatus'] = [];
        //$scope.signalProxy.on('ditoMasterChangeStatus', function (data) {
        //    $.each(Common.ClientHub._events['ditoMasterChangeStatus'], function (i, v) {
        //        if (Common.HasValue(v))
        //            v(data);
        //    });
        //});
    }, 2);

    $rootScope.IsShowMenu = true;
    $scope.TextTime = '';
    $scope.TextDate = '';

    //Global variables
    $rootScope.fileDownload = 'empty.html';
    $rootScope.IsLoading = true;
    $rootScope.DateDMYHM = {
        format: 'dd/MM/yyyy HH:mm',
        timeFormat: "HH:mm",
        parseFormats: ["yyyy-MM-ddTHH:mm:ss"]
    };
    $rootScope.DateDMY = {
        format: 'dd/MM/yyyy',
        parseFormats: ["yyyy-MM-ddTHH:mm:ss"]
    };
    $rootScope.DateHM = {
        format: 'HH:mm',
        parseFormats: ["yyyy-MM-ddTHH:mm:ss"]
    };

    //Global methods
    Common.Log('DownloadFile');
    $rootScope.DownloadFile = function (url) {
        $rootScope.fileDownload = url;
    };
    $rootScope.HTML = function (html) {
        return $sce.trustAsHtml(html);
    }
    $rootScope.gridChooseAll_Check = function ($event, grid, callback) {
        if ($event.target.checked == true) {
            grid.items().each(function () {

                var tr = this;
                var item = grid.dataItem(tr);
                if (item.IsChoose != true) {
                    $(tr).find('td input.chkChoose').prop('checked', true);
                    item.IsChoose = true;
                    if (!$(tr).hasClass('IsChoose'))
                        $(tr).addClass('IsChoose');
                }
            });
        }
        else {
            grid.items().each(function () {
                var tr = this;
                var item = grid.dataItem(tr);
                if (item.IsChoose == true) {
                    $(tr).find('td input.chkChoose').prop('checked', false);
                    item.IsChoose = false;
                    if ($(tr).hasClass('IsChoose'))
                        $(tr).removeClass('IsChoose');
                }
            });
        }

        if (Common.HasValue(callback)) {
            callback($event, grid, $event.target.checked);
        }
    };
    $rootScope.gridChoose_Check = function ($event, grid, callback) {
        var tr = $($event.target).closest('tr');
        var item = grid.dataItem(tr);
        if ($event.target.checked == true) {
            item.IsChoose = true;
            if (!$(tr).hasClass('IsChoose'))
                $(tr).addClass('IsChoose');
        }
        else {
            item.IsChoose = false;
            if ($(tr).hasClass('IsChoose'))
                $(tr).removeClass('IsChoose');
        }

        var flag = item.IsChoose;
        if (flag != true) {
            grid.items().each(function () {
                var tr = this;
                var item = grid.dataItem(tr);
                if (item.IsChoose == true)
                    flag = true;
            });
        }
        if (Common.HasValue(callback)) {
            callback($event, grid, flag);
        }
    };

    //Winfile, image
    Common.Log('UploadFile');
    $rootScope.winfile_IsImage = false;
    $rootScope.winfile_ShowChoose = true;
    $rootScope.winfile_AllowChange = true;
    $rootScope.winfile_gridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.SYS,
            method: _default.URL.FileList,
            readparam: function () {
                return {
                    code: $rootScope.winfile_Item != null ? $rootScope.winfile_Item.Type : "",
                    id: $rootScope.winfile_Item != null ? $rootScope.winfile_Item.ID : -1,
                }
            },
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    IsChoose: { type: 'bool', defaultValue: false }
                }
            },
            pageSize: 0,
        }),
        height: '100%', pageable: false, sortable: true, columnMenu: false, resizable: true, reorderable: true, filterable: { mode: 'row' },
        columns: [
            {
                title: ' ', width: '30px',
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,winfile_grid,winfile_gridChooseChange)" />',
                headerAttributes: { style: 'text-align: center;' },
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,winfile_grid,winfile_gridChooseChange)" />',
                templateAttributes: { style: 'text-align: center;' },
                filterable: false, sortable: false
            },
            {
                field: 'FileName', title: 'Tệp',
                template: '<a target="_blank" href="#=FilePath#">#=FileName#</a>',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            }
        ]
    };
    $rootScope.winfile_gridHasChoose = false;
    $rootScope.winfile_gridChooseChange = function ($event, grid, haschoose) {
        $rootScope.winfile_gridHasChoose = !$rootScope.winfile_gridHasChoose;
    };
    $rootScope.winfile_fileOptions = {
        async: {
            saveUrl: _default.URL.FileHandler,
            autoUpload: true
        },
        multiple: false,
        showFileList: false,
        upload: function (e) {
            var xhr = e.XMLHttpRequest;
            xhr.addEventListener('readystatechange', function (e) {
                if (xhr.readyState == 1)
                    xhr.setRequestHeader('auth', Common.Auth.HeaderKey);
            });
            e.data = { 'folderPath': $rootScope.winfile_Item.Path }
        },
        success: function (e) {
            var file = e.response;

            file.ReferID = $rootScope.winfile_Item.ID;
            file.TypeOfFileCode = $rootScope.winfile_Item.Type;
            Common.Services.Call($http, {
                url: Common.Services.url.SYS,
                method: _default.URL.FileSave,
                data: { item: file },
                success: function (res) {
                    if ($rootScope.winfile_IsImage)
                        $rootScope.winfile_viewDataSource.read();
                    else
                        $rootScope.winfile_gridOptions.dataSource.read();

                    if ($rootScope.winfile_Item.UploadComplete != null)
                        $rootScope.winfile_Item.UploadComplete(res);
                }
            });
        }
    };
    $rootScope.winfile_Upload_Click = function ($event, winfile) {
        $event.preventDefault();

        $timeout(function () {
            angular.element(winfile.element[0]).trigger('click');
        }, 1);
    };
    $rootScope.winfile_Save_Click = function ($event, grid, view) {
        $event.preventDefault();

        var lst = [];
        if ($rootScope.winfile_IsImage) {
            var sel = view.select();
            if (Common.HasValue(sel)) {
                $rootScope.Message({
                    Type: Common.Message.Type.Confirm,
                    Msg: 'Bạn muốn lấy dữ liệu đã chọn?',
                    Ok: function (e) {
                        if ($rootScope.winfile_Item.Complete != null)
                            $rootScope.winfile_Item.Complete(view.dataItem(sel));
                        $scope.winfile.close();
                    }
                })
            }
        }
        else {
            var data = grid.dataSource.data();
            $.each(data, function (i, v) {
                if (v.IsChoose == true)
                    lst.push(v);
            });
        }
        if (lst.length > 0) {
            $rootScope.Message({
                Type: Common.Message.Type.Confirm,
                Msg: 'Bạn muốn lấy dữ liệu đã chọn?',
                Ok: function (e) {
                    if ($rootScope.winfile_Item.Complete != null)
                        $rootScope.winfile_Item.Complete(lst[0]);
                    $scope.winfile.close();
                }
            })
        }
    };
    $rootScope.winfile_Close_Click = function ($event) {
        $event.preventDefault();

        $scope.winfile.close();
    };
    $rootScope.winfile_Del_Click = function ($event, grid, view) {
        $event.preventDefault();

        var lstid = [];
        if ($rootScope.winfile_IsImage) {
            var data = view.dataSource.data();
            $.each(data, function (i, v) {
                if (v.IsChoose == true)
                    lstid.push(v.ID);
            });
        }
        else {
            var data = grid.dataSource.data();
            $.each(data, function (i, v) {
                if (v.IsChoose == true)
                    lstid.push(v.ID);
            });
        }
        if (lstid.length > 0) {
            if (confirm('Bạn muốn xóa dữ liệu đã chọn ?')) {
                $rootScope.IsLoading = true;
                Common.Services.Call($http, {
                    url: Common.Services.url.SYS,
                    method: _default.URL.FileDelete,
                    data: { lstid: lstid },
                    success: function (res) {
                        $rootScope.IsLoading = false;

                        if ($rootScope.winfile_IsImage)
                            $scope.winfile_viewDataSource.read();
                        else
                            $scope.winfile_gridOptions.dataSource.read();

                        if ($rootScope.winfile_Item.DeleteComplete != null)
                            $rootScope.winfile_Item.DeleteComplete(lstid);
                    }
                });
            }
        }
    };
    $rootScope.winfile_viewOptions = {
        template: '<div ng-dblclick="winfile_viewItem_Click($event,winfile_view);" class="item"><img src="#= FilePath #" alt="#: FileName #" title="#=FileName#" /><div class="choose"><input type="checkbox" class="chkChoose" #= IsChoose ? "checked=checked" : "" # ng-click="winfile_viewChoose($event,winfile_view)" /></div><div class="name">#=FileName#</div><div class="clear"></div></div>',
        selectable: 'SINGLE',
        navigatable: true
    };
    $rootScope.winfile_viewChoose = function ($event, view) {
        var div = $($event.target).closest('div.item');
        var item = view.dataItem(div);
        if ($event.target.checked == true)
            item.IsChoose = true;
        else
            item.IsChoose = false;

        var flag = item.IsChoose;
        if (flag != true) {
            view.element.find('div.item').each(function () {
                var item = view.dataItem(this);
                if (item.IsChoose == true)
                    flag = true;
            });
        }
        $rootScope.winfile_gridHasChoose = flag;
    };
    $rootScope.winfile_viewDataSource = Common.DataSource.Grid($http, {
        url: Common.Services.url.SYS,
        method: _default.URL.FileList,
        readparam: function () {
            return {
                code: $rootScope.winfile_Item != null ? $rootScope.winfile_Item.Type : "",
                id: $rootScope.winfile_Item != null ? $rootScope.winfile_Item.ID : -1,
            }
        },
        pageSize: 100,
        model: {
            id: 'ID',
            fields: {
                ID: { type: 'number' },
                IsChoose: { type: 'bool', defaultValue: false }
            }
        }
    });
    $rootScope.winfile_viewItem_Click = function ($event, view) {
        $event.preventDefault();

        var item = view.dataItem($($event.target).closest('div.item'));
        if (Common.HasValue(item)) {
            $rootScope.Message({
                Type: Common.Message.Type.Confirm,
                Msg: 'Bạn muốn lấy dữ liệu đã chọn?',
                Ok: function (e) {
                    if ($rootScope.winfile_Item.Complete != null)
                        $rootScope.winfile_Item.Complete(item);
                    $scope.winfile.close();
                }
            })
        }
    };
    $rootScope.UploadFile = function (options) {
        options = $.extend(true, {
            IsImage: false,
            AllowChange: true, //Allow delete, upload
            ShowChoose: true, //Show Save button
            ID: -1,
            Type: '', //Common.CATTypeOfFileCode
            Complete: null,
            DeleteComplete: null,
            UploadComplete: null
        }, options);
        options.Path = '';
        switch (options.Type) {
            case 'Function': options.Path = Common.FolderUpload.FUNCTION; break;
            case 'User': options.Path = Common.FolderUpload.User; break;
            case 'Customer': options.Path = Common.FolderUpload.Customer; break;
            case 'COPOD': options.Path = Common.FolderUpload.POD; break;
            case 'DIPOD': options.Path = Common.FolderUpload.POD; break;
            case 'TemplateReport': options.Path = Common.FolderUpload.Export; break;
            case 'ImportOPS': options.Path = Common.FolderUpload.Export; break;
            case 'ORD': options.Path = Common.FolderUpload.Export; break;
        }
        $rootScope.winfile_Item = options;
        $rootScope.winfile_IsImage = options.IsImage;
        $rootScope.winfile_ShowChoose = options.ShowChoose;
        $rootScope.winfile_AllowChange = options.AllowChange;

        if (options.IsImage)
            $rootScope.winfile_viewDataSource.read();
        else
            $rootScope.winfile_gridOptions.dataSource.read();
        $scope.winfile.center();
        $scope.winfile.open();
    };

    //Winexcel
    Common.Log('UploadExcel');
    $scope.winexcel_fileOptions = {
        async: {
            saveUrl: _default.URL.FileHandler,
            autoUpload: true
        },
        multiple: false,
        showFileList: false,
        upload: function (e) {
            var xhr = e.XMLHttpRequest;
            xhr.addEventListener('readystatechange', function (e) {
                if (xhr.readyState == 1)
                    xhr.setRequestHeader('auth', Common.Auth.HeaderKey);
            });
            e.data = { 'folderPath': $rootScope.winexcel_Item.Path }
        },
        success: function (e) {
            var file = e.response;

            if ($rootScope.winexcel_Item.Upload != null) {
                $rootScope.winexcel_Item.Upload(file, function (data) {
                    $scope.winexcel_gridOptions.dataSource.data(data);
                });
            }
        },
        error: function (e) {
            debugger;
            //Common._fileCurrent.LoadData();
        }
    };
    $scope.winexcel_gridOptions = {
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ExcelRow',
                fields: {
                    ExcelRow: { type: 'string' },
                    ExcelSuccess: { type: 'bool' },
                    ExcelError: { type: 'string' }
                }
            }
        }),
        height: '100%', pageable: false, sortable: true, columnMenu: false, resizable: true, reorderable: false, filterable: { mode: 'row' },
        columns: []
    };
    $scope.winexcel_Download_Click = function ($event) {
        $event.preventDefault();

        if ($rootScope.winexcel_Item.Download != null)
            $rootScope.winexcel_Item.Download($event);
    };
    $scope.winexcel_Upload_Click = function ($event, excelfile) {
        $event.preventDefault();

        $timeout(function () {
            angular.element(excelfile.element[0]).trigger('click');
        }, 1);
    };
    $scope.winexcel_Save_Click = function ($event) {
        $event.preventDefault();

        if ($rootScope.winexcel_Item.Complete != null)
            $rootScope.winexcel_Item.Complete($event, $scope.winexcel_gridOptions.dataSource.data());
        $scope.winexcel.close();
    };
    $scope.winexcel_Close_Click = function ($event) {
        $event.preventDefault();

        $scope.winexcel.close();
    };
    $rootScope.UploadExcel = function (options) {
        options = $.extend(true, {
            Path: Common.FolderUpload.Import,
            width: '900px',
            height: '500px',
            columns: [],
            Download: null,
            Upload: null,
            Window: null,
            Complete: null
        }, options);
        $rootScope.winexcel_Item = options;
        $scope.winexcel.setOptions({
            width: $rootScope.winexcel_Item.width,
            height: $rootScope.winexcel_Item.height
        });

        var cols = [
            {
                field: 'ExcelRow', title: 'Dòng', width: '50px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'ExcelSuccess', title: 'TC', width: '40px',
                template: '<input class="chkChoose" type="checkbox" #= ExcelSuccess ? "checked=checked" : "" # disabled="disabled" />',
                sortable: false, filterable: false, menu: false
            },
            {
                field: 'ExcelError', title: 'Lỗi', width: '200px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            }
        ];
        angular.forEach(options.columns, function (v, i) {
            cols.push(v);
        });
        cols.push({ title: ' ', sortable: false, filterable: false, menu: false });
        $scope.winexcel_gridOptions.columns = cols;
        $scope.winexcel_gridOptions.dataSource.data([]);

        $scope.winexcel.center();
        $scope.winexcel.open();
        //$rootScope.winexcel_Item.Window.center();
        //$rootScope.winexcel_Item.Window.open();
    };
    $rootScope.winexcel_Item = null;

    //Win message
    Common.Log('Message');
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

    //Comment
    Common.Log('Comment');
    $rootScope.hasComment = false;
    $rootScope.comment_list = [];
    $rootScope.comment_title = '';
    $rootScope.comment_item = null;
    $rootScope.Comment = function (options) {
        if (Common.HasValue($rootScope.comment_item) && $rootScope.comment_item.Type == options.Type) {
            $rootScope.comment_item = null;
            $rootScope.hasComment = false;
        }
        else {
            options = $.extend(true, {
                Type: Common.Comment.NONE,
                ReferID: -1,
                Title: '&nbsp;'
            }, options);
            $rootScope.comment_item = options;
            $rootScope.comment_title = '<a title="' + $rootScope.comment_item.Title.replace('</b>', '').replace('<b>', '').replace('</i>', '').replace('<i>', '') + '">' + $rootScope.comment_item.Title + '</a>';

            $rootScope.Comment_Load();
            $rootScope.hasComment = true;
        }

        $rootScope.ResizeMain();
    };
    $rootScope.Comment_Show = function (referid, title) {
        if ($rootScope.comment_item != null) {
            $rootScope.comment_item.ReferID = referid;
            $rootScope.comment_title = '<a title="' + title.replace('</b>', '').replace('<b>', '').replace('</i>', '').replace('<i>', '') + '">' + title + '</a>';
            $rootScope.Comment_Load();
        }
    };
    $rootScope.Comment_Load = function () {
        Common.Services.Call($http, {
            url: Common.Services.url.SYS,
            method: _default.URL.CommentList,
            data: { type: $rootScope.comment_item.Type, referid: $rootScope.comment_item.ReferID },
            success: function (res) {
                if (Common.HasValue(res)) {
                    angular.forEach(res, function (v, i) {
                        if (!Common.HasValue(v.Image) || v.Image == '')
                            v.Image = _default.URL.NoImage;
                        v.Date = Common.Date.FromJsonDMYHM(v.Date);
                        v.Mine = v.UserID == Common.Auth.Item.UserID;
                    });
                    $rootScope.comment_list = res;
                }
            }
        });
    }

    //Resize Window.
    $rootScope.ResizeMain = function () {
        $timeout(function () {
            $(document).find('div.cus-splitter').trigger('resize');
        }, 101);
    };
    $rootScope.ResizeDefault = function () {
        $timeout(function () {
            $(window).trigger('resize');
        }, 1);
    };

    //Info
    $rootScope.Default_IsLogin = false;
    $rootScope.Default_Logo = '';
    $rootScope.Default_UserName = '';
    $rootScope.Default_DisplayName = '';
    $rootScope.Default_Address = '';
    $rootScope.Default_Tel = '';
    $rootScope.Default_Fax = '';

    //Authorization
    $rootScope.IsPageComplete = false;
    Common.Log('GetAuthorization');
    Common.Auth.HeaderKey = Common.Auth.GetHeaderKey();
    $timeout(function () {
        Common.Services.Call($http, {
            url: Common.Services.url.SYS,
            method: _default.URL.GetAuthorization,
            data: { dt: new Date() },
            success: function (res) {
                if (Common.HasValue(res) && res.UserID > 0) {
                    Common.Auth.Item = res;
                    Common.Auth.SetHeaderKey(res.HeaderKey);

                    $rootScope.Default_IsLogin = true;
                    $rootScope.Default_UserName = Common.Auth.Item.UserName;
                    $rootScope.Default_DisplayName = Common.Auth.Item.LastName + ' ' + Common.Auth.Item.FirstName;
                    $rootScope.Default_Address = Common.Auth.Item.Address;
                    $rootScope.Default_Tel = Common.Auth.Item.Tel;
                    $rootScope.Default_Fax = Common.Auth.Item.Fax;

                    Common.ALL.Init($http);

                    if (Common.HasValue($rootScope.ListState) && Common.HasValue($rootScope.FunctionItem) && $rootScope.FunctionItem.ID > 0) {
                        if (Common.HasValue($rootScope.FunctionItem) && Common.HasValue($rootScope.FunctionItem.Code)) {
                            if ($rootScope.FunctionItem.Code != '') {
                                if (Common.HasValue($rootScope.FunctionItem.Params))
                                    $state.go($rootScope.FunctionItem.Code, $rootScope.FunctionItem.Params);
                                else
                                    $state.go($rootScope.FunctionItem.Code);
                            }
                        }
                    }
                    else {
                        $rootScope._completefunction = function () {
                            $rootScope.IsPageComplete = true;
                            if (Common.HasValue($rootScope.ListState) && Common.HasValue($rootScope.FunctionItem) && $rootScope.FunctionItem.ID > 0) {
                                if ($rootScope.FunctionItem.Code != '') {
                                    if (Common.HasValue($rootScope.FunctionItem.Params))
                                        $state.go($rootScope.FunctionItem.Code, $rootScope.FunctionItem.Params);
                                    else
                                        $state.go($rootScope.FunctionItem.Code);
                                }
                            }
                        };
                    }
                    $timeout(function () {
                        if (Common.HasValue($rootScope.FunctionItem)) {
                            if (Common.HasValue($rootScope.FunctionItem.Code))
                                $state.go('main');
                        }
                        else
                            $state.go('main');
                    }, 5);

                    //var RS = {};
                    //var RSS = {};
                    //$rootScope.RS = {};
                    //$rootScope.RSS = {};
                    //Common.Services.Call($http, {
                    //    url: Common.Services.url.SYS,
                    //    method: _default.URL.ListResource,
                    //    data: { dt: new Date() },
                    //    success: function (res) {
                    //        $rootScope.IsLoading = false;

                    //        angular.forEach(res, function (v, i) {
                    //            var table = v.Key.substr(0, v.Key.indexOf('.'));
                    //            var col = v.Key.substr(v.Key.indexOf('.') + 1);
                    //            var current = RS[table];
                    //            if (!Common.HasValue(current)) {
                    //                RS[table] = {};
                    //                RSS[table] = {};
                    //            }
                    //            RS[table][col] = v.Name;
                    //            RSS[table][col] = v.ShortName;
                    //        });
                    //        $rootScope.RS = RS;
                    //        $rootScope.RSS = RSS;


                    //    }
                    //});

                    $timeout(function () {
                        $rootScope.signalProxy.on('messagecall' + res.UserID, function (data) {
                            if (Common.HasValue($rootScope.UserMessage_Change))
                                $rootScope.UserMessage_Change(data.total);
                        });
                    }, 3);
                }
                else {
                    $timeout(function () { $state.go('login'); }, 1);
                }
            }
        });
    }, 1);

    $scope.Logout = function ($event) {
        $event.preventDefault();

        Common.Services.Call($http, {
            url: Common.Services.url.SYS,
            method: _default.URL.Logout,
            data: { dt: new Date() },
            success: function (res) {
                $rootScope.signalProxy.off('messagecall' + Common.Auth.Item.UserID);
                Common.Auth.Item = null;
                Common.Auth.HeaderKey = '';
                Common.Auth.SetHeaderKey('');
                $rootScope.Default_IsLogin = false;
                $state.go('login');
            }
        });
    }
    $scope.MenuClick = function ($event) {
        $event.preventDefault();
        Common.Log("MenuClick");

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

    //Profile
    $scope.Default_ShowProfile = false;
    $scope.ShowProfileClick = function ($event) {
        $event.preventDefault();
        $event.stopPropagation();

        $rootScope.HideSetting();
        $scope.Default_ShowProfile = !$scope.Default_ShowProfile;
    };
    $scope.HideProfileClick = function ($event) {
        $scope.Default_ShowProfile = false;
    };

    //UserMessage
    $('.lazyload').bind('scroll', function () {
        if ($(this).scrollTop() + $(this).innerHeight() >= $(this)[0].scrollHeight) {
            $scope.UserMessage_LoadMore_Click();
        }
    })

    $scope.userMessage_Tabstrip_Options = {
        animation: {
            open: { effects: "fadeIn" }
        },
        activate: function (e) {
            $scope.Default_UserMessageTabIndex = angular.element(e.item).data('tabindex');
        }
    }
    $rootScope.UserMessageTotal = 0;
    $scope.Default_UserMessageTabIndex = 0;
    $rootScope.Default_ShowUserMessage = false;
    $scope.IsClickMessage = false;
    $scope.UserMessage = {
        All: { Page: 0, Data: [], TypeOfMessage: "All" },
        ORD: { Page: 0, Data: [], TypeOfMessage: "ORD" },
        OPS: { Page: 0, Data: [], TypeOfMessage: "OPS" },
        MON: { Page: 0, Data: [], TypeOfMessage: "MON" },
        POD: { Page: 0, Data: [], TypeOfMessage: "POD" },
    }

    $scope.UserMessage_IsLoadMore = false;
    $scope.UserMessage_PageSize = 10;
    $scope.IsFullScreen = false;

    $rootScope.UserMessage_Change = function (total) {
        $rootScope.UserMessageTotal = total;
    }
    $rootScope.UserMessage_Click = function ($event) {
        $scope.IsClickMessage = true;
        $event.preventDefault();

        $rootScope.Default_ShowUserMessage = !$rootScope.Default_ShowUserMessage;

        if ($rootScope.Default_ShowUserMessage == true) {
            // All
            if ($scope.UserMessage.All.Page == 0 || $rootScope.UserMessageTotal > 0) {
                Common.Services.Call($http, {
                    url: Common.Services.url.SYS,
                    method: _default.URL.MessageCall_LoadMore,
                    data: { currentPage: $scope.UserMessage.All.Page, pageSize: $scope.UserMessage_PageSize, typeOfMessage: $scope.UserMessage.All.TypeOfMessage },
                    success: function (res) {
                        $scope.UserMessage.All.Page = res.CurrentPage;
                        if (res.CurrentPage == 1) {
                            $scope.UserMessage.All.Data = [];
                        }
                        angular.forEach(res.ListMessage, function (v, i) {
                            v.CreatedDate = Common.Date.FromJsonDDMMHM(v.CreatedDate);
                            $scope.UserMessage.All.Data.push(v);
                        });
                    }
                });
            }

            // ORD
            if ($scope.UserMessage.ORD.Page == 0 || $rootScope.UserMessageTotal > 0) {
                Common.Services.Call($http, {
                    url: Common.Services.url.SYS,
                    method: _default.URL.MessageCall_LoadMore,
                    data: { currentPage: $scope.UserMessage.ORD.Page, pageSize: $scope.UserMessage_PageSize, typeOfMessage: $scope.UserMessage.ORD.TypeOfMessage },
                    success: function (res) {
                        $scope.UserMessage.ORD.Page = res.CurrentPage;
                        if (res.CurrentPage == 1) {
                            $scope.UserMessage.ORD.Data = [];
                        }
                        angular.forEach(res.ListMessage, function (v, i) {
                            v.CreatedDate = Common.Date.FromJsonDDMMHM(v.CreatedDate);
                            $scope.UserMessage.ORD.Data.push(v);
                        });
                    }
                });
            }

            // OPS
            if ($scope.UserMessage.OPS.Page == 0 || $rootScope.UserMessageTotal > 0) {
                Common.Services.Call($http, {
                    url: Common.Services.url.SYS,
                    method: _default.URL.MessageCall_LoadMore,
                    data: { currentPage: $scope.UserMessage.OPS.Page, pageSize: $scope.UserMessage_PageSize, typeOfMessage: $scope.UserMessage.OPS.TypeOfMessage },
                    success: function (res) {
                        $scope.UserMessage.OPS.Page = res.CurrentPage;
                        if (res.CurrentPage == 1) {
                            $scope.UserMessage.OPS.Data = [];
                        }
                        angular.forEach(res.ListMessage, function (v, i) {
                            v.CreatedDate = Common.Date.FromJsonDDMMHM(v.CreatedDate);
                            $scope.UserMessage.OPS.Data.push(v);
                        });
                    }
                });
            }

            // MON
            if ($scope.UserMessage.MON.Page == 0 || $rootScope.UserMessageTotal > 0) {
                Common.Services.Call($http, {
                    url: Common.Services.url.SYS,
                    method: _default.URL.MessageCall_LoadMore,
                    data: { currentPage: $scope.UserMessage.MON.Page, pageSize: $scope.UserMessage_PageSize, typeOfMessage: $scope.UserMessage.MON.TypeOfMessage },
                    success: function (res) {
                        $scope.UserMessage.MON.Page = res.CurrentPage;
                        if (res.CurrentPage == 1) {
                            $scope.UserMessage.MON.Data = [];
                        }
                        angular.forEach(res.ListMessage, function (v, i) {
                            v.CreatedDate = Common.Date.FromJsonDDMMHM(v.CreatedDate);
                            $scope.UserMessage.MON.Data.push(v);
                        });
                    }
                });
            }

            // POD
            if ($scope.UserMessage.POD.Page == 0 || $rootScope.UserMessageTotal > 0) {
                Common.Services.Call($http, {
                    url: Common.Services.url.SYS,
                    method: _default.URL.MessageCall_LoadMore,
                    data: { currentPage: $scope.UserMessage.POD.Page, pageSize: $scope.UserMessage_PageSize, typeOfMessage: $scope.UserMessage.POD.TypeOfMessage },
                    success: function (res) {
                        $scope.UserMessage.POD.Page = res.CurrentPage;
                        if (res.CurrentPage == 1) {
                            $scope.UserMessage.POD.Data = [];
                        }
                        angular.forEach(res.ListMessage, function (v, i) {
                            v.CreatedDate = Common.Date.FromJsonDDMMHM(v.CreatedDate);
                            $scope.UserMessage.POD.Data.push(v);
                        });
                    }
                });
            }

            // Read
            Common.Services.Call($http, {
                url: Common.Services.url.SYS,
                method: _default.URL.MessageCall_Read,
                success: function (res) {
                    $rootScope.UserMessageTotal = 0;
                }
            });
        }
    }
    $scope.UserMessage_CheckClose = function ($event) {
        if (!$scope.IsClickMessage && $rootScope.Default_ShowUserMessage)
            $rootScope.Default_ShowUserMessage = false;

        $scope.IsClickMessage = false;
    }
    $scope.UserMessage_LoadMore_Click = function ($event) {
        if (Common.HasValue($event))
            $event.preventDefault();

        if (!$scope.UserMessage_IsLoadMore) {
            // All
            $scope.UserMessage_IsLoadMore = true;
            if ($scope.Default_UserMessageTabIndex == 0) {
                Common.Services.Call($http, {
                    url: Common.Services.url.SYS,
                    method: _default.URL.MessageCall_LoadMore,
                    data: { currentPage: $scope.UserMessage.All.Page + 1, pageSize: $scope.UserMessage_PageSize, typeOfMessage: $scope.UserMessage.All.TypeOfMessage },
                    success: function (res) {
                        if (res.CurrentPage == 1) {
                            $scope.UserMessage.All.Page = res.CurrentPage;
                            $scope.UserMessage.All.Data = [];
                        } else {
                            if (res.ListMessage.length > 0)
                                $scope.UserMessage.All.Page = res.CurrentPage + 1;
                        }
                        angular.forEach(res.ListMessage, function (v, i) {
                            v.CreatedDate = Common.Date.FromJsonDDMMHM(v.CreatedDate);
                            $scope.UserMessage.All.Data.push(v);
                        });
                        $scope.UserMessage_IsLoadMore = false;
                    }
                });
            }

            // ORD
            if ($scope.Default_UserMessageTabIndex == 1) {
                Common.Services.Call($http, {
                    url: Common.Services.url.SYS,
                    method: _default.URL.MessageCall_LoadMore,
                    data: { currentPage: $scope.UserMessage.ORD.Page + 1, pageSize: $scope.UserMessage_PageSize, typeOfMessage: $scope.UserMessage.ORD.TypeOfMessage },
                    success: function (res) {
                        if (res.CurrentPage == 1) {
                            $scope.UserMessage.ORD.Page = res.CurrentPage;
                            $scope.UserMessage.ORD.Data = [];
                        } else {
                            if (res.ListMessage.length > 0)
                                $scope.UserMessage.ORD.Page = res.CurrentPage + 1;
                        }
                        angular.forEach(res.ListMessage, function (v, i) {
                            v.CreatedDate = Common.Date.FromJsonDDMMHM(v.CreatedDate);
                            $scope.UserMessage.ORD.Data.push(v);
                        });
                        $scope.UserMessage_IsLoadMore = false;
                    }
                });
            }

            // OPS
            if ($scope.Default_UserMessageTabIndex == 2) {
                Common.Services.Call($http, {
                    url: Common.Services.url.SYS,
                    method: _default.URL.MessageCall_LoadMore,
                    data: { currentPage: $scope.UserMessage.OPS.Page + 1, pageSize: $scope.UserMessage_PageSize, typeOfMessage: $scope.UserMessage.OPS.TypeOfMessage },
                    success: function (res) {
                        if (res.CurrentPage == 1) {
                            $scope.UserMessage.OPS.Page = res.CurrentPage;
                            $scope.UserMessage.OPS.Data = [];
                        } else {
                            if (res.ListMessage.length > 0)
                                $scope.UserMessage.OPS.Page = res.CurrentPage + 1;
                        }
                        angular.forEach(res.ListMessage, function (v, i) {
                            v.CreatedDate = Common.Date.FromJsonDDMMHM(v.CreatedDate);
                            $scope.UserMessage.OPS.Data.push(v);
                        });
                        $scope.UserMessage_IsLoadMore = false;
                    }
                });
            }

            // MON
            if ($scope.Default_UserMessageTabIndex == 3) {
                Common.Services.Call($http, {
                    url: Common.Services.url.SYS,
                    method: _default.URL.MessageCall_LoadMore,
                    data: { currentPage: $scope.UserMessage.MON.Page + 1, pageSize: $scope.UserMessage_PageSize, typeOfMessage: $scope.UserMessage.MON.TypeOfMessage },
                    success: function (res) {
                        if (res.CurrentPage == 1) {
                            $scope.UserMessage.MON.Page = res.CurrentPage;
                            $scope.UserMessage.MON.Data = [];
                        } else {
                            if (res.ListMessage.length > 0)
                                $scope.UserMessage.MON.Page = res.CurrentPage + 1;
                        }
                        angular.forEach(res.ListMessage, function (v, i) {
                            v.CreatedDate = Common.Date.FromJsonDDMMHM(v.CreatedDate);
                            $scope.UserMessage.MON.Data.push(v);
                        });
                        $scope.UserMessage_IsLoadMore = false;
                    }
                });
            }

            // MON
            if ($scope.Default_UserMessageTabIndex == 4) {
                Common.Services.Call($http, {
                    url: Common.Services.url.SYS,
                    method: _default.URL.MessageCall_LoadMore,
                    data: { currentPage: $scope.UserMessage.POD.Page + 1, pageSize: $scope.UserMessage_PageSize, typeOfMessage: $scope.UserMessage.POD.TypeOfMessage },
                    success: function (res) {
                        if (res.CurrentPage == 1) {
                            $scope.UserMessage.POD.Page = res.CurrentPage;
                            $scope.UserMessage.POD.Data = [];
                        } else {
                            if (res.ListMessage.length > 0)
                                $scope.UserMessage.POD.Page = res.CurrentPage + 1;
                        }
                        angular.forEach(res.ListMessage, function (v, i) {
                            v.CreatedDate = Common.Date.FromJsonDDMMHM(v.CreatedDate);
                            $scope.UserMessage.POD.Data.push(v);
                        });
                        $scope.UserMessage_IsLoadMore = false;
                    }
                });
            }
        }
    }

    $scope.UserMessage_ViewAll_Click = function ($event, win) {
        $event.preventDefault();
        win.center();
        win.open();
        $scope.userMessage_ViewAll_gridOptions.dataSource.read();
    }
    $scope.userMessage_ViewAll_gridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.SYS,
            method: _default.URL.Notification_Read,
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number', editable: false, nullable: true },
                    CreatedDate: { type: 'date' },
                },
            },
            sort: [{ field: "CreatedDate", dir: "DESC" }]
        }),
        height: '100%', pageable: true, sortable: true, columnMenu: false, resizable: true, reorderable: true, filterable: false,
        filterable: { mode: 'row' },
        dataBound: function () {
            var grid = this;
            if (Common.HasValue(this) && Common.HasValue(this.tbody)) {
                this.element.find('tr[role="row"]').each(function () {
                    var item = grid.dataItem(this);
                    if (Common.HasValue(item)) {
                        if (item.IsUnRead) {
                            $(this).addClass('tr-unread');
                        }
                    }
                });
            }
        },
        columns: [
            { field: 'TypeOfEvent', width: "120px", title: 'Loại sự kiện', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'Message', title: 'Nội dung', encoded: false, filterable: { cell: { operator: 'contains', showOperators: false } } },
            {
                field: 'CreatedDate', width: "150px", title: 'Ngày tạo',
                template: '#=Common.Date.FromJsonDMYHM(CreatedDate)#',
                filterable: { cell: { template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); }, operator: 'equal', showOperators: false } },
            },
        ]
    }

    $scope.UserMessageViewAllClose_Click = function ($event, win) {
        $event.preventDefault();
        win.close();
    }

    $scope.UserMessageViewAll_Zoom_Click = function ($event) {
        $event.preventDefault();
        $scope.IsFullScreen = true;

        $scope.UserMessageViewAll_win.setOptions({ draggable: false });
        $scope.UserMessageViewAll_win.maximize();
        $scope.UserMessageViewAll_win.center().open();
    }

    $scope.UserMessageViewAll_Minimize_Click = function ($event) {
        $event.preventDefault();
        $scope.IsFullScreen = false;
        $scope.UserMessageViewAll_win.setOptions({ draggable: true });
        $scope.UserMessageViewAll_win.center().open();
    }

    //Directive
    $rootScope.FocusKCombobox = function (element) {
        $timeout(function () {
            try {
                var widget = element.getKendoComboBox();
                widget.input.on("focus", function () {
                    try {
                        widget.open();
                    } catch (e) {
                    }
                });
                widget.input.on("click", function () {
                    try {
                        widget.open();
                    } catch (e) {
                    }
                });

                //$(widget.popup.element).find('.k-list-scroller').css('overflow', 'hidden');
                //$(widget.popup.element).find('.k-list-scroller').perfectScrollbar();
            }
            catch (e) { Common.Log("Combobox not found!") }
        }, 300)
    }
    $rootScope.FocusKSelect = function (element) {
        $timeout(function () {
            try {
                var widget = element.getKendoMultiSelect();
                widget.input.on("focus", function () {
                    try {
                        widget.open();
                    } catch (e) {
                    }
                });

                //$(widget.popup.element).find('.k-list-scroller').css('overflow', 'hidden');
                //$(widget.popup.element).find('.k-list-scroller').perfectScrollbar();
            }
            catch (e) { Common.Log("Select not found!") }
        }, 300)
    }
    $rootScope.FocusKDateTimePicker = function (element) {
        $timeout(function () {
            var picker = element.data("kendoDateTimePicker");
            if (Common.HasValue(picker)) {
                element.on("click", function () {
                    $(this).data("kendoDateTimePicker").open();
                });
                element.bind("focus", function () {
                    $(this).data("kendoDateTimePicker").open();
                });
                picker.bind("open", function (e) {
                    switch (e.view) {
                        case 'date':
                            var widget = this.dateView.calendar.wrapper[0];
                            var wrapper = this.wrapper;
                            var w = this.wrapper.width();
                            var pLeft = w + wrapper.position().left;
                            var oLeft = w + this.wrapper.offset().left
                            $timeout(function () {
                                var container = $(widget).closest('.k-animation-container');
                                if (Common.HasValue(container[0])) {
                                    var win = container[0];
                                    var w1 = $(win).width();
                                    if (pLeft > w1) {
                                        win.style.left = oLeft - w1 + 'px';
                                    }
                                }
                            }, 1)
                            break;
                        case 'time':
                            break;
                        default:
                            break;
                    }
                });
                //picker.timeView.popup.element.perfectScrollbar();
            }
        }, 300)
    }
    $rootScope.FocusKDatePicker = function (element) {
        $timeout(function () {
            var picker = $element.data("kendoDatePicker");
            if (Common.HasValue(picker)) {
                element.on("click", function () {
                    $(this).data("kendoDatePicker").open();
                });
                element.bind("focus", function () {
                    $(this).data("kendoDatePicker").open();
                });
                picker.bind("open", function (e) {
                    var widget = this.dateView.calendar.wrapper[0];
                    var wrapper = this.wrapper;
                    var w = this.wrapper.width();
                    var pLeft = w + wrapper.position().left;
                    var oLeft = w + this.wrapper.offset().left
                    $timeout(function () {
                        var container = $(widget).closest('.k-animation-container');
                        if (Common.HasValue(container[0])) {
                            var win = container[0];
                            var w1 = $(win).width();
                            if (pLeft > w1) {
                                win.style.left = oLeft - w1 + 'px';
                            }
                        }
                    }, 1)
                });
            }
        }, 300)
    }
    $rootScope.FocusKTimePicker = function (element) {
        $timeout(function () {
            var picker = element.data("kendoTimePicker");
            if (Common.HasValue(picker)) {
                element.bind("focus", function () {
                    $(this).data("kendoTimePicker").open();
                });
                element.on("click", function () {
                    $(this).data("kendoTimePicker").open();
                });
                //picker.timeView.popup.element.perfectScrollbar();
            }
        }, 300)
    }
    $rootScope.ExpandKGrid = function (element, scope) {
        $($window).resize(function () {
            var dataElement = element.find('.k-grid-content');
            var nonDataElements = element.children().not('.k-grid-content');
            var currentGridHeight = element.innerHeight();
            var nonDataElementsHeight = 0;
            nonDataElements.each(function () { nonDataElementsHeight += $(this).outerHeight(); });
            dataElement.height(currentGridHeight - nonDataElementsHeight);
        });
        setTimeout(function () {
            //Identify grid
            var grid = $(element).data('kendoGrid');
            if (Common.HasValue(grid)) {
                //$(element).find('.k-grid-content').perfectScrollbar("destroy");
                //$(element).find('.k-grid-content').perfectScrollbar({
                //    minScrollbarLength: 30
                //});
                //$(element).find('.k-grid-header').css('padding-right', '0');
                //$(element).find('.k-grid-content-locked').css('height', $(element).find('.k-grid-content-locked').height() + 17);
                //$(element).find(".k-grid-header th span[data-role='filtercell']").each(function () {
                //    var menu = $(this).data('kendoFilterCell');
                //    if (Common.HasValue(menu)) {
                //        try {
                //            var auto = menu.input.data('kendoAutoComplete');
                //            if (auto != null) {
                //                auto.bind('open', function (e) {
                //                    var me = $(this.ul).closest('.k-list-scroller');
                //                    setTimeout(function () {
                //                        me.css('overflow', 'hidden');
                //                        me.scrollTop(0);
                //                        me.perfectScrollbar('destroy');
                //                        me.perfectScrollbar({
                //                            minScrollbarLength: 50,
                //                            suppressScrollX: true
                //                        });
                //                    }, 100)
                //                })
                //            }
                //            var cbb = menu.input.data('kendoComboBox');
                //            if (cbb != null) {

                //            }
                //        } catch (e) { }
                //    }
                //});
            }
        }, 100);
    }

    //SettingGrid
    $rootScope.SettingGrid = {
        grid: null,
        gridName: '',
        hasFilterRow: true,
        hasFilterRowEnable: true,

        filterType: '',
        hasSettingView: true,
        hasFilterDefault: true
    }
    $rootScope.wincustom_view_gridOptions = {
        dataSource: Common.DataSource.Local({
            data: []
        }),
        height: '100%', groupable: false, pageable: false, sortable: false, columnMenu: false, filterable: false, resizable: true,
        columns: [
            {
                field: 'Code', title: 'Mã cột', width: '150px',
                headerAttributes: { style: 'text-align: left;' }, attributes: { style: 'text-align: left;' }
            },
            {
                field: 'Name', title: 'Tên cột', width: '150px',
                headerAttributes: { style: 'text-align: left;' }, attributes: { style: 'text-align: left;' }
            },
            {
                field: 'IsHidden', title: 'Ẩn', width: '40px',
                template: '<input type="checkbox" ng-model="dataItem.IsHidden" />',
                headerAttributes: { style: 'text-align: center;' }, attributes: { style: 'text-align: center;' }
            },
            {
                field: 'Width', title: 'Kích thước cột', width: '1050px',
                template: '<input type="nunber" class="k-textbox" style="width: 50px;float:left;" ng-model="dataItem.Width" /><input kendo-slider ng-model="dataItem.Width" k-tooltip="{ enabled: true }" k-max="980" k-min="0" style="width: 1000px;" />',
                headerAttributes: { style: 'text-align: left;' }, attributes: { style: 'text-align: left;' }
            },
            {
                title: ' ', filterable: false
            }
        ],
        columnResize: function (e) {
            if (e.column.field == 'Width')
                e.preventDefault();
        },
        dataBound: function () {
            var grid = this;
            grid.table.kendoSortable({
                filter: ">tbody >tr",
                hint: $.noop,
                ignore: 'input,a',
                cursor: "move",
                placeholder: function (element) {
                    return element.clone().addClass("k-state-hover").css("opacity", 0.65);
                },
                container: ".cus-grid.k-grid tbody",
                change: function (e) {
                    var skip = 1,
                        oldIndex = e.oldIndex + skip,
                        newIndex = e.newIndex + skip,
                        dataItem = grid.dataSource.getByUid(e.item.data("uid"));

                    grid.dataSource.remove(dataItem);
                    grid.dataSource.insert(newIndex - 1, dataItem);
                }
            });
        }
    }
    $rootScope.SettingGrid_Click = function ($event, win, grid) {
        $event.preventDefault();

        if (Common.HasValue(grid)) {
            $rootScope.SettingGrid.grid = grid;
        }
        else {
            grid = $rootScope.SettingGrid.grid;
        }
        if (Common.HasValue(grid)) {

            if (Common.HasValue(grid.options.filterable) && (grid.options.filterable.mode == 'row' || grid.options.filterable.mode == '')) {
                $rootScope.SettingGrid.hasFilterRow = true;

                if (grid.options.filterable.mode == 'row')
                    $rootScope.SettingGrid.hasFilterRowEnable = true;
                else
                    $rootScope.SettingGrid.hasFilterRowEnable = false;
            }
            else {
                $rootScope.SettingGrid.hasFilterRow = false;
            }

            var sKey = grid.wrapper.attr("kendo-grid");
            if (sKey != null && sKey != "") {
                $rootScope.SettingGrid.gridName = sKey;
                $rootScope.SettingGrid.hasSettingView = true;
            } else {
                $rootScope.SettingGrid.gridName = sKey;
                $rootScope.SettingGrid.hasSettingView = false;
            }

        }
        win.center().open();
    }
    $scope.SettingGrid_FilterRow_Click = function ($event) {
        $event.preventDefault();

        var grid = $rootScope.SettingGrid.grid;
        if (Common.HasValue(grid)) {
            $rootScope.SettingGrid.hasFilterRowEnable = !$rootScope.SettingGrid.hasFilterRowEnable;
            Common.Services.Call($http, {
                url: Common.Services.url.SYS,
                method: 'App_UserGridSetting_Save',
                data: {
                    referID: $rootScope.FunctionItem.ID,
                    referKey: $rootScope.FunctionItem.Code,
                    item: {
                        Name: $rootScope.SettingGrid.gridName,
                        FilterRowHidden: !$rootScope.SettingGrid.hasFilterRowEnable,
                        FilterType: $rootScope.SettingGrid.filterType,
                        Columns: []
                    }
                },
                success: function (res) {
                    Common.Services.Error(res, function (res) {
                        var obj = $rootScope.FunctionItem.ListSettings[$rootScope.FunctionItem.Code];
                        //Nếu có thiết lập => Replace thiết lập cũ.
                        if (Common.HasValue(obj)) {
                            if (obj.Grids == null) {
                                obj.Grids = [{
                                    Name: $rootScope.SettingGrid.gridName,
                                    FilterRowHidden: $rootScope.SettingGrid.hasFilterRowEnable,
                                    FilterType: $rootScope.SettingGrid.filterType,
                                    Columns: []
                                }]
                            } else {
                                var flag = false;
                                angular.forEach(obj.Grids, function (v) {
                                    if (v.Name == $rootScope.SettingGrid.gridName) {
                                        flag = true;
                                        v.FilterRowHidden = $rootScope.SettingGrid.hasFilterRowEnable;
                                    }
                                })
                                if (!flag) {
                                    obj.Grids.push({
                                        Name: $rootScope.SettingGrid.gridName,
                                        FilterRowHidden: $rootScope.SettingGrid.hasFilterRowEnable,
                                        FilterType: $rootScope.SettingGrid.filterType,
                                        Columns: []
                                    });
                                }
                            }
                        }
                            //Thêm mới thiết lập.
                        else {
                            obj = {
                                DefaultFunctionID: 0,
                                DefaultKey: '',
                                Grids: [{
                                    Name: $rootScope.SettingGrid.gridName,
                                    FilterRowHidden: $rootScope.SettingGrid.hasFilterRowEnable,
                                    FilterType: $rootScope.SettingGrid.filterType,
                                    Columns: []
                                }],
                                ReferID: $rootScope.FunctionItem.ID,
                                ReferKey: $rootScope.FunctionItem.Code
                            }
                        }

                        Common.Log($rootScope.FunctionItem.ListSettings[$rootScope.FunctionItem.Code]);

                        //Áp dụng thiết lập cho grid.
                        if (Common.HasValue(grid.options.filterable) && grid.options.filterable.mode == 'row') {
                            if (!$rootScope.SettingGrid.hasFilterRowEnable) {
                                grid.setOptions({ filterable: { mode: '' } });
                            }
                        } else if ($rootScope.SettingGrid.hasFilterRowEnable) {
                            grid.setOptions({ filterable: { mode: 'row' } });
                        }
                    })
                }
            })
        }
    }
    $scope.SettingGrid_View_Click = function ($event, win, grid) {
        $event.preventDefault();

        if (Common.HasValue($rootScope.SettingGrid.grid)) {
            var key = $rootScope.SettingGrid.grid.wrapper.attr('kendo-grid');
            var obj = $rootScope.FunctionItem.ListSettings[$rootScope.FunctionItem.Code];
            var vSet = null;
            if (key != '' && Common.HasValue(obj)) {
                Common.Data.Each(obj.Grids, function (v) {
                    if (v.Name == key) {
                        vSet = v;
                    }
                })
            }
            if (vSet == null) {
                vSet = {
                    Name: key,
                    FilterType: '',
                    FilterRowHidden: false,
                    Columns: []
                }
                angular.forEach($rootScope.SettingGrid.grid.columns, function (o, j) {
                    if (o.field != null && o.field != '') {
                        var col = {
                            Code: o.field,
                            Name: o.title,
                            SortOrder: j + 1,
                            IsHidden: o.hidden,
                            Width: typeof (o.width) == "string" ? o.width.slice(0, -2) : o.width || 0
                        };
                        try {
                            col.Name = eval(col.Name);
                        } catch (e) {
                        }
                        vSet.Columns.push(col);
                    }
                })
            }
            else if (vSet.Columns == null || vSet.Columns.length == 0) {
                vSet.Columns = [];
                angular.forEach($rootScope.SettingGrid.grid.columns, function (o, j) {
                    if (o.field != null && o.field != '') {
                        var col = {
                            Code: o.field,
                            Name: o.title,
                            SortOrder: j + 1,
                            IsHidden: o.hidden,
                            Width: typeof (o.width) == "string" ? o.width.slice(0, -2) : o.width || 0
                        };
                        try {
                            if (col.Name != null && col.Name.length >= 4 && col.Name.startsWith('{{') && col.Name.endsWith('}}')) {
                                var str = '$rootScope.' + col.Name.substr(2).substr(0, col.Name.length - 4);
                                col.Name = eval(str);
                            }
                        } catch (e) {
                        }
                        vSet.Columns.push(col);
                    }
                })
            }

            angular.forEach(vSet.Columns, function (o, j) {
                if (o.Code != null && o.Code != '') {
                    try {
                        if (o.Name != null && o.Name.length >= 4 && o.Name.startsWith('{{') && o.Name.endsWith('}}')) {
                            var str = '$rootScope.' + o.Name.substr(2).substr(0, o.Name.length - 4);
                            o.Name = eval(str);
                        }
                    } catch (e) {
                    }
                }
            })

            vSet.Columns.sort(function (a, b) {
                if (a.SortOrder < b.SortOrder)
                    return -1;
                else if (a.SortOrder > b.SortOrder)
                    return 1;
                else
                    return 0;
            })

            win.center().open();
            grid.dataSource.data(vSet.Columns);
            $timeout(function () { grid.refresh() }, 1);
        }
    }
    $scope.wincustom_view_Update_Click = function ($event, win, grid) {
        $event.preventDefault();

        var idx = 1, data = grid.dataSource.data();
        Common.Data.Each(data, function (o) {
            o.SortOrder = idx++;
        })
        Common.Services.Call($http, {
            url: Common.Services.url.SYS,
            method: 'App_UserGridSetting_Save',
            data: {
                referID: $rootScope.FunctionItem.ID,
                referKey: $rootScope.FunctionItem.Code,
                item: {
                    Name: $rootScope.SettingGrid.gridName,
                    FilterRowHidden: !$rootScope.SettingGrid.hasFilterRowEnable,
                    FilterType: $rootScope.SettingGrid.filterType,
                    Columns: data
                }
            },
            success: function (res) {
                Common.Services.Error(res, function (res) {
                    var obj = $rootScope.FunctionItem.ListSettings[$rootScope.FunctionItem.Code];
                    //Nếu có thiết lập => Replace thiết lập cũ.
                    if (Common.HasValue(obj)) {
                        if (obj.Grids == null) {
                            obj.Grids = [{
                                Name: $rootScope.SettingGrid.gridName,
                                FilterRowHidden: $rootScope.SettingGrid.hasFilterRowEnable,
                                FilterType: $rootScope.SettingGrid.filterType,
                                Columns: data
                            }]
                        } else {
                            var flag = false;
                            angular.forEach(obj.Grids, function (v) {
                                if (v.Name == $rootScope.SettingGrid.gridName) {
                                    flag = true;
                                    v.Columns = data
                                }
                            })
                            if (!flag) {
                                obj.Grids.push({
                                    Name: $rootScope.SettingGrid.gridName,
                                    FilterRowHidden: $rootScope.SettingGrid.hasFilterRowEnable,
                                    FilterType: $rootScope.SettingGrid.filterType,
                                    Columns: data
                                });
                            }
                        }
                    }
                        //Thêm mới thiết lập.
                    else {
                        obj = {
                            DefaultFunctionID: 0,
                            DefaultKey: '',
                            Grids: [{
                                Name: $rootScope.SettingGrid.gridName,
                                FilterRowHidden: $rootScope.SettingGrid.hasFilterRowEnable,
                                FilterType: $rootScope.SettingGrid.filterType,
                                Columns: data
                            }],
                            ReferID: $rootScope.FunctionItem.ID,
                            ReferKey: $rootScope.FunctionItem.Code
                        }
                    }

                    Common.Log($rootScope.FunctionItem.ListSettings[$rootScope.FunctionItem.Code]);

                    //Áp dụng thiết lập cho grid.
                    var vSort = {}, vShow = {}, vWidth = {};
                    Common.Data.Each(data, function (col) {
                        vSort[col.Code] = col.SortOrder - 1;
                        vShow[col.Code] = !col.IsHidden;
                        vWidth[col.Code] = col.Width + "px";
                    });

                    angular.forEach($rootScope.SettingGrid.grid.columns, function (col, idx) {
                        if (col.field != null && col.field != '') {
                            if (Common.HasValue(vShow[col.field])) {
                                col.SortOrder = vSort[col.field];
                            }
                        }
                    });

                    $rootScope.SettingGrid.grid.columns.sort(function (a, b) {
                        if (a.SortOrder != null && b.SortOrder != null) {
                            if (a.SortOrder < b.SortOrder)
                                return -1;
                            else if (a.SortOrder > b.SortOrder)
                                return 1;
                            else
                                return 0;
                        }
                        else
                            return 0;
                    })

                    //angular.forEach($rootScope.SettingGrid.grid.columns, function (col, idx) {
                    //    if (col.field != null && col.field != '') {
                    //        if (Common.HasValue(vShow[col.field])) {
                    //            $rootScope.SettingGrid.grid.reorderColumn(vSort[col.field], col);
                    //        }
                    //    }
                    //});

                    angular.forEach($rootScope.SettingGrid.grid.columns, function (col, idx) {
                        if (col.field != null && col.field != '') {
                            if (Common.HasValue(vShow[col.field])) {
                                if (vShow[col.field] == false || vShow[col.field] == "false") {
                                    col.hidden = true;
                                } else {
                                    col.hidden = false;
                                }
                            }
                            if (Common.HasValue(vWidth[col.field])) {
                                col.width = vWidth[col.field];
                            }
                        }
                    });
                    $rootScope.SettingGrid.grid.setOptions({ columns: $rootScope.SettingGrid.grid.columns })

                    win.close();
                })
            }
        })
    }
    $scope.wincustom_view_Cancel_Click = function ($event, win) {
        $event.preventDefault();

        win.close();
    }
    $rootScope.GetColumnSettings = function (key) {
        var res = {};
        try {
            var obj = $rootScope.FunctionItem.ListSettings[$rootScope.FunctionItem.Code];
            if (Common.HasValue(obj)) {
                Common.Data.Each(obj.Grids, function (v) {
                    if (v.Name == key) {
                        Common.Data.Each(v.Columns, function (col) {
                            res[col.Code] = col.Width;
                        })
                    }
                })
            }
        }
        catch (e) { }
        return res;
    }
    $rootScope.ApplyGridSettings = function (grid) {
        Common.Log("ApplyGridSettings");
        if (Common.HasValue(grid)) {
            try {
                var key = grid.wrapper.attr('kendo-grid');
                var obj = $rootScope.FunctionItem.ListSettings[$rootScope.FunctionItem.Code];
                if (Common.HasValue(obj) && obj.Grids.length > 0) {
                    Common.Data.Each(obj.Grids, function (v) {
                        if (v.Name == key) {
                            //Thiết lập cột.
                            var vSort = {}, vShow = {}, vWidth = {};
                            v.Columns.sort(function (a, b) {
                                if (a.SortOrder != null && b.SortOrder != null) {
                                    if (a.SortOrder < b.SortOrder)
                                        return -1;
                                    else if (a.SortOrder > b.SortOrder)
                                        return 1;
                                    else
                                        return 0;
                                }
                                else
                                    return 0;
                            })
                            Common.Data.Each(v.Columns, function (col) {
                                vSort[col.Code] = col.SortOrder - 1;
                                vShow[col.Code] = !col.IsHidden;
                                vWidth[col.Code] = col.Width + "px";
                            });
                            angular.forEach(grid.columns, function (col, idx) {
                                if (col.field != null && col.field != '') {
                                    if (Common.HasValue(vShow[col.field])) {
                                        col.SortOrder = vSort[col.field];
                                    }
                                }
                            });
                            grid.columns.sort(function (a, b) {
                                if (a.SortOrder != null && b.SortOrder != null) {
                                    if (a.SortOrder < b.SortOrder)
                                        return -1;
                                    else if (a.SortOrder > b.SortOrder)
                                        return 1;
                                    else
                                        return 0;
                                }
                                else
                                    return 0;
                            })
                            //angular.forEach(grid.columns, function (col, idx) {
                            //    if (col.field != null && col.field != '') {
                            //        if (Common.HasValue(vShow[col.field])) {
                            //            grid.reorderColumn(vSort[col.field], col);
                            //        }
                            //    }
                            //});
                            angular.forEach(grid.columns, function (col, idx) {
                                if (col.field != null && col.field != '') {
                                    if (Common.HasValue(vShow[col.field])) {
                                        if (vShow[col.field] == false || vShow[col.field] == "false") {
                                            col.hidden = true;
                                        } else {
                                            col.hidden = false;
                                        }
                                    }
                                    if (Common.HasValue(vWidth[col.field])) {
                                        col.width = vWidth[col.field];
                                    }
                                }
                            });
                            grid.setOptions({ columns: grid.columns })
                            $timeout(function () {
                                grid.refresh();
                            }, 1)
                            //Thiết lập ẩn hiện filter row.
                            if (Common.HasValue(grid.options.filterable) && grid.options.filterable.mode == 'row') {
                                if (v.FilterRowHidden) {
                                    grid.setOptions({ filterable: { mode: '' } });
                                }
                            } else if (!v.FilterRowHidden) {
                                grid.setOptions({ filterable: { mode: 'row' } });
                            }
                        }
                    })
                }
            }
            catch (e) { }
        }
    }

    //Check authority
    $rootScope.CheckView = function (ActionCode, ViewReturn, Param) {
        if ($rootScope.IsPageComplete) {
            if (Common.Auth.Item.ListActionCode.indexOf(ActionCode) >= 0) {
                return true;
            } else {
                if (Common.HasValue(ViewReturn)) {
                    $state.go(ViewReturn, Param);
                    return false;
                }
            }
        } else
            return false;
    }

    $rootScope.GetAuth = function () {
        var Auth = {
            ActAdd: false,
            ActEdit: false,
            ActDel: false,
            ActApproved: false,
            ActOPS: false,
            ActComment: false,
            ActExcel: false,
            ActContainer: false,
            ActTruck: false,
            ActAddAndApproved: false,
            ActSave: false,
            ActSaveAndApproved: false,
            ViewAdmin: false,
            ViewCustomer: false,
            ViewVendor: false,
            ViewApproved: false,
            ViewDriver: false,
            ViewVehicle: false,
        }
        angular.forEach(Common.Auth.Item.ListActionCode, function (v, i) {
            switch (v) {
                case "ActAdd": Auth.ActAdd = true; break;
                case "ActEdit": Auth.ActEdit = true; break;
                case "ActDel": Auth.ActDel = true; break;
                case "ActApproved": Auth.ActApproved = true; break;
                case "ActOPS": Auth.ActOPS = true; break;
                case "ActComment": Auth.ActComment = true; break;
                case "ActExcel": Auth.ActExcel = true; break;
                case "ActContainer": Auth.ActContainer = true; break;
                case "ActTruck": Auth.ActTruck = true; break;
                case "ActAddAndApproved": Auth.ActAddAndApproved = true; break;
                case "ActSave": Auth.ActSave = true; break;
                case "ActSaveAndApproved": Auth.ActSaveAndApproved = true; break;
                case "ViewAdmin": Auth.ViewAdmin = true; break;
                case "ViewCustomer": Auth.ViewCustomer = true; break;
                case "ViewVendor": Auth.ViewVendor = true; break;
                case "ViewApproved": Auth.ViewApproved = true; break;
                case "ViewDriver": Auth.ViewDriver = true; break;
                case "ViewVehicle": Auth.ViewVehicle = true; break;
            }
        });
        return Auth;
    };

    //Search
    $scope.Search = {
        Text: "Đơn hàng",
        ValueSelect: 1,
        Content: "",
        IsTruckComplete: false,
        IsContainerComplete: false,
        IsSearch: false,
    };

    $scope.Search_BTN_Click = function ($event) {
        $event.preventDefault();
        $('.dropdown-menu').slideToggle(300);
    }

    $scope.Search_Item_Click = function ($event, value) {
        $scope.Search.ValueSelect = value;
        switch (value) {
            case 1: $scope.Search.Text = "Đơn hàng"; break;
            case 2: $scope.Search.Text = "Mã chuyến"; break;
            case 3: $scope.Search.Text = "Số xe"; break;
            case 4: $scope.Search.Text = "Tài xế"; break;
            case 5: $scope.Search.Text = "Địa chỉ giao"; break;
            case 6: $scope.Search.Text = "Chứng từ"; break;
        }
        $('.dropdown-menu').slideUp(300);
    }

    $scope.Search_Click = function ($event, win) {
        $event.preventDefault();
        if ($scope.Search.Content.length >= 3) {
            $rootScope.IsLoading = true;
            $scope.Search.IsSearch = true;
            $scope.Search.IsTruckComplete = false;
            $scope.Search.IsContainerComplete = false;
            $scope.Search_TruckGrid_Options.dataSource.read();
            $scope.Search_ContainerGrid_Options.dataSource.read();
        }
        else {
            $rootScope.Message({ Msg: 'Vui lòng nhập ít nhất 3 ký tự', NotifyType: Common.Message.NotifyType.WARNING });
        }
    }

    $scope.Search_ShowResult = function () {
        if ($scope.Search.IsSearch) {
            $scope.Search.IsSearch = false;
            $rootScope.IsLoading = false;
            if ($scope.Search_TruckGrid_Options.dataSource.data().length > 0 || $scope.Search_ContainerGrid_Options.dataSource.data().length > 0) {
                $scope.ResultSearch_view.center().open();
                $rootScope.IsLoading = false;
            }
            else {
                $rootScope.IsLoading = false;
                $rootScope.Message({
                    Msg: 'Không có dữ liệu',
                    NotifyType: Common.Message.NotifyType.WARNING
                });
            }
            $timeout(function () {
                $scope.Search_TruckGrid.resize();
                $scope.Search_ContainerGrid.resize();
            }, 300);
            $scope.Search_DateFilter();
        }
    }

    $scope.Search_Zoom_Click = function ($event) {
        $event.preventDefault();
        $scope.IsFullScreen = true;

        $scope.ResultSearch_view.setOptions({ draggable: false });
        $scope.ResultSearch_view.maximize();
        $scope.ResultSearch_view.center().open();
    }

    $scope.Search_Minimize_Click = function ($event) {
        $event.preventDefault();
        $scope.IsFullScreen = false;
        $scope.ResultSearch_view.setOptions({ draggable: true });
        $scope.ResultSearch_view.center().open();
    }

    $scope.Search_TruckGrid_Options = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.SYS,
            method: _default.URL.SYSSearchDI,
            readparam: function () { //input truyen vao
                return {
                    type: $scope.Search.ValueSelect, content: $scope.Search.Content
                }
            },
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    RequestDate: { type: 'date' },
                    CreatedDate: { type: 'date' },
                    DateFromCome: { type: 'date' },
                    DateFromLeave: { type: 'date' },
                    DateFromLoadEnd: { type: 'date' },
                    DateFromLoadStart: { type: 'date' },
                    DateToCome: { type: 'date' },
                    DateToLeave: { type: 'date' },
                    DateToLoadEnd: { type: 'date' },
                    DateToLoadStart: { type: 'date' },
                    MasterETD: { type: 'date' },
                    InvoiceDate: { type: 'date' },
                    CutOffTime: { type: 'date' },
                    DateDocument: { type: 'date' },
                    DateGetEmpty: { type: 'date' },
                    DateInspect: { type: 'date' },
                    DateLoading: { type: 'date' },
                    DateReturnEmpty: { type: 'date' },
                    DateShipCome: { type: 'date' },
                    DateUnloading: { type: 'date' },
                    TonTranfer: { type: 'number' },
                    CBMTranfer: { type: 'number' },
                    QuantityTranfer: { type: 'number' },
                }
            },
            pageSize: 20,
        }),
        height: '99%', groupable: false, pageable: Common.PageSize, sortable: true, columnMenu: false, resizable: true,
        filterable: { mode: 'row', visible: false }, reorderable: false,
        columns: [
            {
                field: 'DITOGroupProductStatusName', width: 140, title: 'Trạng thái v.chuyển',
                template: "<span class='lblStatus'>#=DITOGroupProductStatusName#</span>",
                filterable: false, groupable: false
            },
            {
                field: 'DITOGroupProductStatusPODName', width: 100, title: 'Trạng thái c.từ',
                filterable: false, groupable: false
            },
            {
                field: 'OrderCode', width: 100, title: 'Mã ĐH',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'CustomerName', width: 100, title: 'Khách hàng',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'MasterCode', width: 100, title: 'Số chuyến',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'VehicleCode', width: 100, title: 'Số xe',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'LocationToAddress', width: 200, title: 'Địa chỉ giao',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'DriverName1', width: 100, title: 'Tài xế',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'LocationFromCode', width: 100, title: 'Mã kho',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'LocationToCode', width: 100, title: 'Mã điểm giao',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'LocationToName', width: 100, title: 'Điểm giao',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'LocationToProvince', width: 120, title: 'Tỉnh thành',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'LocationToDistrict', width: 120, title: 'Quận huyện',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'SOCode', width: 100, title: 'Số SO',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'DNCode', width: 100, title: 'Số DN',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'MasterETD', width: 150, title: 'Ngày chạy', template: "#=MasterETD==null?' ':kendo.toString(MasterETD, '" + Common.Date.Format.DMYHM + "')#",
                filterable: { cell: { template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); }, operator: 'equal', showOperators: false } },
            },
            {
                field: 'DriverTel1', width: 100, title: 'SĐT tài xế',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'TonTranfer', width: 60, title: 'Số tấn',
                filterable: { cell: { operator: 'gte', showOperators: false } }
            },
            {
                field: 'CBMTranfer', width: 60, title: 'Số khối',
                filterable: { cell: { operator: 'gte', showOperators: false } }
            },
            {
                field: 'QuantityTranfer', width: 80, title: 'Số lượng',
                filterable: { cell: { operator: 'gte', showOperators: false } }
            },
            {
                field: 'RequestDate', width: 150, title: 'Ngày gửi yêu cầu', template: "#=RequestDate==null?' ':kendo.toString(RequestDate, '" + Common.Date.Format.DMYHM + "')#",
                filterable: { cell: { template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); }, operator: 'equal', showOperators: false } },
            },
            {
                field: 'DateFromCome', width: 150, title: 'Ngày đến kho', template: "#=DateFromCome==null?' ':kendo.toString(DateFromCome, '" + Common.Date.Format.DMYHM + "')#",
                filterable: { cell: { template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); }, operator: 'equal', showOperators: false } },
            },
            {
                field: 'DateFromLoadStart', width: 150, title: 'Ngày vào máng', template: "#=DateFromLoadStart==null?' ':kendo.toString(DateFromLoadStart, '" + Common.Date.Format.DMYHM + "')#",
                filterable: { cell: { template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); }, operator: 'equal', showOperators: false } },
            },
            {
                field: 'DateFromLoadEnd', width: 150, title: 'Ngày ra máng', template: "#=DateFromLoadEnd==null?' ':kendo.toString(DateFromLoadEnd, '" + Common.Date.Format.DMYHM + "')#",
                filterable: { cell: { template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); }, operator: 'equal', showOperators: false } },
            },
            {
                field: 'DateFromLeave', width: 150, title: 'Ngày rời kho', template: "#=DateFromLeave==null?' ':kendo.toString(DateFromLeave, '" + Common.Date.Format.DMYHM + "')#",
                filterable: { cell: { template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); }, operator: 'equal', showOperators: false } },
            },
            {
                field: 'DateToCome', width: 150, title: 'Ngày đến NPP', template: "#=DateToCome==null?' ':kendo.toString(DateToCome, '" + Common.Date.Format.DMYHM + "')#",
                filterable: { cell: { template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); }, operator: 'equal', showOperators: false } },
            },
            {
                field: 'DateToLoadStart', width: 150, title: 'Tg BD dỡ hàng', template: "#=DateToLoadStart==null?' ':kendo.toString(DateToLoadStart, '" + Common.Date.Format.DMYHM + "')#",
                filterable: { cell: { template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); }, operator: 'equal', showOperators: false } },
            },
            {
                field: 'DateToLoadEnd', width: 150, title: 'Tg KT dỡ hàng', template: "#=DateToLoadEnd==null?' ':kendo.toString(DateToLoadEnd, '" + Common.Date.Format.DMYHM + "')#",
                filterable: { cell: { template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); }, operator: 'equal', showOperators: false } },
            },
            {
                field: 'DateToLeave', width: 150, title: 'Ngày rời NPP', template: "#=DateToLeave==null?' ':kendo.toString(DateToLeave, '" + Common.Date.Format.DMYHM + "')#",
                filterable: { cell: { template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); }, operator: 'equal', showOperators: false } },
            },
            {
                field: 'Note', width: 100, title: 'Ghi chú',
                filterable: false, sortable: false
            },
            {
                field: 'Note1', width: 100, title: 'Ghi chú 1',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'Note2', width: 100, title: 'Ghi chú 2',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            { title: '', filterable: false, sortable: false }
        ],
        dataBound: function () {
            $scope.Search.IsTruckComplete = true;
            if ($scope.Search.IsContainerComplete) {
                $scope.Search_ShowResult();
            }

            var grid = this;
            Common.Data.Each(grid.items(), function (tr) {
                var item = grid.dataItem(tr);
                if (item != null) {
                    switch (item.DITOGroupProductStatusID) {
                        case 1:
                            $(tr).find('span.lblStatus').addClass('item-status-approvedactive');
                            break;
                        case 2:
                            $(tr).find('span.lblStatus').addClass('item-status-receivedactive');
                            break;
                        case 3:
                            $(tr).find('span.lblStatus').addClass('item-status-cancelactive');
                            break;
                    }
                }
            })
        }
    };

    $scope.Search_ContainerGrid_Options = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.SYS,
            method: _default.URL.SYSSearchCO,
            readparam: function () { //input truyen vao
                return {
                    type: $scope.Search.ValueSelect, content: $scope.Search.Content
                }
            },
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    RequestDate: { type: 'date' },
                    CreatedDate: { type: 'date' },
                    DateFromCome: { type: 'date' },
                    DateFromLeave: { type: 'date' },
                    DateFromLoadEnd: { type: 'date' },
                    DateFromLoadStart: { type: 'date' },
                    DateToCome: { type: 'date' },
                    DateToLeave: { type: 'date' },
                    DateToLoadEnd: { type: 'date' },
                    DateToLoadStart: { type: 'date' },
                    MasterETD: { type: 'date' },
                    InvoiceDate: { type: 'date' },
                    CutOffTime: { type: 'date' },
                    DateDocument: { type: 'date' },
                    DateGetEmpty: { type: 'date' },
                    DateInspect: { type: 'date' },
                    DateLoading: { type: 'date' },
                    DateReturnEmpty: { type: 'date' },
                    DateShipCome: { type: 'date' },
                    DateUnloading: { type: 'date' },
                }
            },
            pageSize: 20,
        }),
        height: '99%', groupable: false, pageable: Common.PageSize, sortable: true, columnMenu: false, resizable: true,
        filterable: { mode: 'row', visible: false }, reorderable: false,
        columns: [
            {
                field: 'TypeOfStatusContainerName', width: 140, title: 'Trạng thái v.chuyển',
                template: "<span class='lblStatus'>#=TypeOfStatusContainerName#</span>",
                filterable: false, groupable: false
            },
            {
                field: 'TypeOfStatusContainerPODName', width: 100, title: 'Trạng thái c.từ',
                filterable: false, groupable: false
            },
            {
                field: 'StatusOfCOContainerName', width: 100, title: 'Chặng',
                filterable: false, groupable: false
            },
            {
                field: 'OrderCode', width: 100, title: 'Mã ĐH',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
           {
               field: 'CustomerName', width: 100, title: 'Khách hàng',
               filterable: { cell: { operator: 'contains', showOperators: false } }
           },
            {
                field: 'MasterCode', width: 100, title: 'Số chuyến',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'VehicleCode', width: 100, title: 'Số xe',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'RomoocNo', width: 100, title: 'Số Romooc',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'LocationToAddress', width: 200, title: 'Địa chỉ giao',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'DriverName1', width: 100, title: 'Tài xế',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'PackingCode', width: 100, title: 'Loại cont',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'ContainerNo', width: 100, title: 'Số Con',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'SealNo1', width: 100, title: 'Số Seal 1',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'SealNo2', width: 100, title: 'Số Seal 2',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
           {
               field: 'RequestDate', width: 120, title: 'Ngày yêu cầu', template: "#=RequestDate==null?' ':kendo.toString(RequestDate, '" + Common.Date.Format.DMYHM + "')#",
               filterable: { cell: { template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); }, operator: 'equal', showOperators: false } },
           },
           {
               field: 'TransportModeName', width: 100, title: 'Loại v/chuyển',
               filterable: { cell: { operator: 'contains', showOperators: false } }
           },
           {
               field: 'ServiceOfOrderName', width: 80, title: 'Dịch vụ',
               filterable: { cell: { operator: 'contains', showOperators: false } }
           },
           {
               field: 'LocationFromName', width: 150, title: 'Nơi lấy hàng',
               filterable: { cell: { operator: 'contains', showOperators: false } }
           },
           {
               field: 'LocationToName', width: 150, title: 'Nơi giao hàng',
               filterable: { cell: { operator: 'contains', showOperators: false } }
           },
           {
               field: 'LocationDepotCode', width: 150, title: 'Nơi lấy rỗng',
               filterable: { cell: { operator: 'contains', showOperators: false } }
           },
           {
               field: 'LocationDepotReturnCode', width: 150, title: 'Nơi trả rỗng',
               filterable: { cell: { operator: 'contains', showOperators: false } }
           },
           {
               field: 'DateGetEmpty', width: 160, title: 'Tg lấy rỗng', template: "#=DateGetEmpty==null?' ':kendo.toString(DateGetEmpty, '" + Common.Date.Format.DMYHM + "')#",
               filterable: { cell: { template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); }, operator: 'equal', showOperators: false } },
           },
           {
               field: 'DateReturnEmpty', width: 160, title: 'Tg trả rỗng', template: "#=DateReturnEmpty==null?' ':kendo.toString(DateReturnEmpty, '" + Common.Date.Format.DMYHM + "')#",
               filterable: { cell: { template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); }, operator: 'equal', showOperators: false } },
           },
           {
               field: 'DateDocument', width: 160, title: 'Tg gửi TK', template: "#=DateDocument==null?' ':kendo.toString(DateDocument, '" + Common.Date.Format.DMYHM + "')#",
               filterable: { cell: { template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); }, operator: 'equal', showOperators: false } },
           },
           { title: '', filterable: false, sortable: false }
        ],
        dataBound: function () {
            $scope.Search.IsContainerComplete = true;
            if ($scope.Search.IsTruckComplete) {
                $scope.Search_ShowResult();
            }

            var grid = this;
            Common.Data.Each(grid.items(), function (tr) {
                var item = grid.dataItem(tr);
                if (item != null) {
                    switch (item.TypeOfStatusContainerID) {
                        case 1:
                            $(tr).find('span.lblStatus').addClass('item-status-approvedactive');
                            break;
                        case 2:
                            $(tr).find('span.lblStatus').addClass('item-status-receivedactive');
                            break;
                        case 3:
                            $(tr).find('span.lblStatus').addClass('item-status-deliveryactive');
                            break;
                    }
                }
            })
        }
    }

    $scope.Search_WinTabOptions = {
        animation: {
            open: { effects: "fadeIn" }
        },
        select: function (e) {

        }
    }

    $scope.Search_DateFilter = function () {
        $timeout(function () {
            $('.dtp-filter-from').kendoDatePicker({
                format: Common.Date.Format.DDMM,
                change: function (e) {
                    try {
                        var element = this.wrapper.parent();
                        var field = e.sender.element.data('field');
                        var dtp_To = $(element).find('.dtp-filter-to[data-field=' + field + ']').data('kendoDatePicker');
                        var f = this.value();
                        var t = dtp_To.value();
                        if (Common.HasValue(t)) {
                            try {
                                t = t.addDays(1);
                            }
                            catch (e) {

                            }
                        }

                        var f_filter = { field: field, operator: "gte", value: f };
                        var t_filter = { field: field, operator: "lte", value: t };

                        if (Common.HasValue($scope.orderDN_grid.dataSource.filter())) {
                            var filters = $scope.orderDN_grid.dataSource.filter().filters;
                            if (Common.HasValue(f)) {
                                var index = 0;
                                var isNew = true;
                                var field = f_filter.field;
                                var operator = f_filter.operator;
                                for (index = 0; index < filters.length; index++) {
                                    if (filters[index].field == field && filters[index].operator == operator) {
                                        isNew = false;
                                        break;
                                    }
                                }

                                if (isNew) {
                                    filters.push(f_filter);
                                }
                                else {
                                    filters[index] = f_filter;
                                }
                            }
                            else {
                                var field = f_filter.field;
                                var operator = f_filter.operator;
                                for (index = 0; index < filters.length; index++) {
                                    if (filters[index].field == field && filters[index].operator == operator) {
                                        filters.splice(index, 1);
                                        break;
                                    }
                                }
                            }
                            if (Common.HasValue(t)) {
                                var isNew = true;
                                var index = 0;
                                var field = t_filter.field;
                                var operator = t_filter.operator;
                                for (index = 0; index < filters.length; index++) {
                                    if (filters[index].field == field && filters[index].operator == operator) {
                                        isNew = false;
                                        break;
                                    }
                                }

                                if (isNew) {
                                    filters.push(t_filter);
                                }
                                else {
                                    filters[index] = t_filter;
                                }
                            }
                            else {
                                var field = t_filter.field;
                                var operator = t_filter.operator;
                                for (index = 0; index < filters.length; index++) {
                                    if (filters[index].field == field && filters[index].operator == operator) {
                                        filters.splice(index, 1);
                                        break;
                                    }
                                }
                            }

                        }
                        else {
                            var filters = [];
                            if (Common.HasValue(f))
                                filters.push(f_filter);
                            if (Common.HasValue(t))
                                filters.push(t_filter);

                            $scope.orderDN_grid.dataSource.filter(filters);
                        }
                        $scope.orderDN_grid.dataSource.read();
                    }
                    catch (e) {
                        $rootScope.Message({ Msg: 'Sai dữ liệu!' });
                    }
                }
            })

            $('.dtp-filter-to').kendoDatePicker({
                format: Common.Date.Format.DDMM,
                change: function (e) {
                    try {
                        var element = this.wrapper.parent();
                        var field = e.sender.element.data('field');
                        var dtp_From = $(element).find('.dtp-filter-from[data-field=' + field + ']').data('kendoDatePicker');
                        var f = dtp_From.value();
                        var t = this.value()
                        if (Common.HasValue(t)) {
                            try {
                                t = t.addDays(1);
                            }
                            catch (e) {

                            }
                        }

                        var f_filter = { field: field, operator: "gte", value: f };
                        var t_filter = { field: field, operator: "lte", value: t };

                        if (Common.HasValue($scope.orderDN_grid.dataSource.filter())) {
                            var filters = $scope.orderDN_grid.dataSource.filter().filters;
                            if (Common.HasValue(f)) {
                                var index = 0;
                                var isNew = true;
                                var field = f_filter.field;
                                var operator = f_filter.operator;
                                for (index = 0; index < filters.length; index++) {
                                    if (filters[index].field == field && filters[index].operator == operator) {
                                        isNew = false;
                                        break;
                                    }
                                }

                                if (isNew) {
                                    filters.push(f_filter);
                                }
                                else {
                                    filters[index] = f_filter;
                                }
                            }
                            else {
                                var field = f_filter.field;
                                var operator = f_filter.operator;
                                for (index = 0; index < filters.length; index++) {
                                    if (filters[index].field == field && filters[index].operator == operator) {
                                        filters.splice(index, 1);
                                        break;
                                    }
                                }
                            }
                            if (Common.HasValue(t)) {
                                var isNew = true;
                                var index = 0;
                                var field = t_filter.field;
                                var operator = t_filter.operator;
                                for (index = 0; index < filters.length; index++) {
                                    if (filters[index].field == field && filters[index].operator == operator) {
                                        isNew = false;
                                        break;
                                    }
                                }

                                if (isNew) {
                                    filters.push(t_filter);
                                }
                                else {
                                    filters[index] = t_filter;
                                }
                            }
                            else {
                                var field = t_filter.field;
                                var operator = t_filter.operator;
                                for (index = 0; index < filters.length; index++) {
                                    if (filters[index].field == field && filters[index].operator == operator) {
                                        filters.splice(index, 1);
                                        break;
                                    }
                                }
                            }

                        }
                        else {
                            var filters = [];
                            if (Common.HasValue(f))
                                filters.push(f_filter);
                            if (Common.HasValue(t))
                                filters.push(t_filter);

                            $scope.orderDN_grid.dataSource.filter(filters);
                        }
                        $scope.orderDN_grid.dataSource.read();
                    }
                    catch (e) {
                        $rootScope.Message({ Msg: 'Sai dữ liệu!' });
                    }
                }
            })
        }, 500)
    }


    $timeout(function () {
        if (Common.HasValue($('#spreadsheet'))) {
            $rootScope.excelShare.data.spreadsheet = $('#spreadsheet').kendoSpreadsheet({
                excelImport: function (e) {
                    $rootScope.excelShare.data.isImport = true;
                    e.promise.progress(function (e) { })
                    .done(function (res) {
                        $rootScope.IsLoading = true;

                        var data = { id: $rootScope.excelShare.data.item.ID, worksheets: $rootScope.excelShare.data.spreadsheet.sheets() };
                        angular.forEach($rootScope.excelShare.data.options.params, function (v, p) {
                            data[p] = v;
                        });

                        Common.Services.Call($http, {
                            url: $rootScope.excelShare.data.options.url,
                            method: $rootScope.excelShare.data.options.methodImport,
                            data: data,
                            success: function (res) {
                                $rootScope.IsLoading = false;
                                $rootScope.excelShare.data.isImport = false;

                                $rootScope.excelShare.data.spreadsheet.fromJSON({ sheets: res.Worksheets });
                            },
                            error: function (res) {
                                $rootScope.IsLoading = false;
                                $rootScope.excelShare.data.isImport = false;
                            }
                        });
                    });
                },
                change: function (arg) {
                    if (!$rootScope.excelShare.data.isImport) {
                        var lstRowCheck = [];
                        var sheet = $rootScope.excelShare.data.spreadsheet.activeSheet();
                        var rowChange = -1;
                        var colChange = -1;
                        var valChange = '';

                        var colFail = 0;
                        arg.range.forEachCell(function (row, col, value) {
                            if (Common.HasValue(value) && Common.HasValue(value.value)) {
                                if (lstRowCheck.indexOf(row) < 0)
                                    lstRowCheck.push(row);
                            }
                            else
                                colFail++;
                            if (col < $rootScope.excelShare.data.options.colCheckChange) {
                                rowChange = row;
                                colChange = col;
                                if (Common.HasValue(value.value))
                                    valChange = value.value;
                            }
                        });
                        if (colFail < 2) {
                            if (lstRowCheck.length == 1) {
                                if (rowChange > 0) {
                                    var range = sheet.range('A' + (rowChange + 1) + ':Z' + (rowChange + 1));
                                    var cells = [];
                                    var flag = 1;
                                    range.forEachCell(function (row, column, value) {
                                        if (flag < 5) {
                                            var val = '';
                                            if (Common.HasValue(value.value)) {
                                                val = value.value;
                                                flag = 1;
                                            }
                                            else
                                                flag++;
                                            cells.push({ Index: column, Value: val });
                                        }
                                    });

                                    var data = { id: $rootScope.excelShare.data.item.ID, row: rowChange, col: colChange, val: valChange, cells: cells };
                                    angular.forEach($rootScope.excelShare.data.options.params, function (v, p) {
                                        data[p] = v;
                                    });

                                    Common.Services.Call($http, {
                                        url: $rootScope.excelShare.data.options.url,
                                        method: $rootScope.excelShare.data.options.methodChange,
                                        data: data,
                                        success: function (res) {
                                            var sheet = $rootScope.excelShare.data.spreadsheet.activeSheet();
                                            for (var row = 0; row < res.length; row++) {
                                                var eRow = res[row];
                                                for (var col = 0; col < eRow.cells.length; col++) {
                                                    var eCell = eRow.cells[col];
                                                    var cell = sheet.range(eRow.index, eCell.index, 0, 0);
                                                    cell.color(eCell.color);
                                                    cell.background(eCell.background);
                                                    cell.value(eCell.value);
                                                    cell.fontSize(eCell.fontSize);
                                                    cell.fontFamily(eCell.fontFamily);
                                                }
                                            }
                                            if (Common.HasValue($rootScope.excelShare.data.options.Changed))
                                                $rootScope.excelShare.data.options.Changed();
                                        },
                                        error: function (res) {
                                            $rootScope.IsLoading = false;
                                        }
                                    });
                                }
                            }
                            else if (lstRowCheck.length > 0) {
                                $rootScope.IsLoading = true;

                                var data = { id: $rootScope.excelShare.data.item.ID, worksheets: $rootScope.excelShare.data.spreadsheet.sheets() };
                                angular.forEach($rootScope.excelShare.data.options.params, function (v, p) {
                                    data[p] = v;
                                });

                                Common.Services.Call($http, {
                                    url: $rootScope.excelShare.data.options.url,
                                    method: $rootScope.excelShare.data.options.methodImport,
                                    data: data,
                                    success: function (res) {
                                        $rootScope.IsLoading = false;
                                        $rootScope.excelShare.data.isImport = false;

                                        $rootScope.excelShare.data.spreadsheet.fromJSON({ sheets: res.Worksheets });
                                    },
                                    error: function (res) {
                                        $rootScope.IsLoading = false;
                                        $rootScope.excelShare.data.isImport = false;
                                    }
                                });
                            }
                        }
                    }
                }
            }).data("kendoSpreadsheet");
        }
    }, 1000);

    $rootScope.excelShare = {
        data: {
            spreadsheet: null,
            item: null,
            isImport: false,
            options: null,
        },
        Init: function (options) {
            options = $.extend(true, {
                functionkey: '',
                params: {},
                colCheckChange: -1,
                url: '',
                methodInit: '',
                methodChange: '',
                methodImport: '',
                methodApprove: '',

                Changed: null,
                Approved: null
            }, options);

            if (!Common.HasValue(options.methodInit) || options.methodInit == '')
                throw 'ExcelShare: methodInit fail';
            else if (!Common.HasValue(options.methodChange) || options.methodChange == '')
                throw 'ExcelShare: methodChange fail';
            else if (!Common.HasValue(options.methodImport) || options.methodImport == '')
                throw 'ExcelShare: methodImport fail';
            else if (!Common.HasValue(options.methodApprove) || options.methodApprove == '')
                throw 'ExcelShare: methodApprove fail';
            else if (!Common.HasValue(options.colCheckChange) || options.colCheckChange < 1)
                throw 'ExcelShare: colCheckChange fail';
            else {
                $rootScope.excelShare.data.options = options;

                $scope.winexcelshare.center();
                $scope.winexcelshare.open();

                var data = { functionid: $rootScope.FunctionItem.ID, functionkey: $rootScope.excelShare.data.options.functionkey };
                angular.forEach($rootScope.excelShare.data.options.params, function (v, p) {
                    data[p] = v;
                });
                $rootScope.IsLoading = true;
                Common.Services.Call($http, {
                    url: options.url,
                    method: options.methodInit,
                    data: data,
                    success: function (res) {
                        $rootScope.excelShare.data.item = res;
                        $rootScope.excelShare.data.spreadsheet.fromJSON({ sheets: res.Worksheets });

                        $rootScope.IsLoading = false;
                    },
                    error: function (res) {
                        $rootScope.IsLoading = false;
                    }
                });
            }
        }
    };

    $scope.winexcelshare_Approve_Click = function ($event, win) {
        $event.preventDefault();

        $rootScope.Message({
            Type: Common.Message.Type.Confirm,
            Msg: 'Bạn muốn lưu các dữ liệu đã thay đổi ?',
            pars: {},
            Ok: function (pars) {
                $rootScope.IsLoading = true;

                var data = { id: $rootScope.excelShare.data.item.ID };
                angular.forEach($rootScope.excelShare.data.options.params, function (v, p) {
                    data[p] = v;
                });

                Common.Services.Call($http, {
                    url: $rootScope.excelShare.data.options.url,
                    method: $rootScope.excelShare.data.options.methodApprove,
                    data: data,
                    success: function (res) {
                        $rootScope.IsLoading = false;

                        $rootScope.excelShare.data.item = res;
                        $rootScope.excelShare.data.spreadsheet.fromJSON({ sheets: res.Worksheets });

                        if (Common.HasValue($rootScope.excelShare.data.options.Approved))
                            $rootScope.excelShare.data.options.Approved();
                    },
                    error: function (res) {
                        $rootScope.IsLoading = false;
                    }
                });
            }
        });
    };

    $scope.winexcelshare_Close_Click = function ($event, win) {
        $event.preventDefault();
        win.close();
    };
}]);