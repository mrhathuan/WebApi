/// <reference path="../../angular.min.js" />
var app = angular.module('myapp', ['ngRoute', 'kendo.directives', 'ngCkeditor']);

app.config(function ($routeProvider) {

    $routeProvider.when("/DASH_Index", {
        templateUrl: '/Pn/Pn/DASH_Index',
        controller: 'DASH_IndexCtr'
    });
    $routeProvider.when("/PRO_Index", {
        templateUrl: '/Pn/Pn/PRO_Index',
        controller: 'PRO_IndexCtr'
    });
    $routeProvider.when("/CAT_Index", {
        templateUrl: '/Pn/Pn/CAT_Index',
        controller: 'CAT_IndexCtr'
    });
    $routeProvider.when("/ORD_Index", {
        templateUrl: '/Pn/Pn/ORD_Index',
        controller: 'ORD_IndexCtr'
    });
    $routeProvider.when("/PAYMENT_Index", {
        templateUrl: '/Pn/Pn/PAYMENT_Index',
        controller: 'PAYMENT_IndexCtr'
    });
    $routeProvider.when("/ERROR_Index", {
        templateUrl: '/Pn/Pn/ERROR_Index',
        controller: 'ERROR_IndexCtr'
    });
    //user
    $routeProvider.when("/USER_Index", {
        templateUrl: '/Pn/Users/USER_Index',
        controller: 'USER_IndexCtr'
    });
    $routeProvider.when("/USER_ChangePasss", {
        templateUrl: '/Pn/Users/USER_ChangePasss',
        controller: 'USER_ChangePassCtr'
    });

    $routeProvider.when("/POST_Index", {
        templateUrl: '/Pn/Post/POST_Index',
        controller: 'POST_IndexCtr'
    });

    $routeProvider.when("/CONTENT_Index", {
        templateUrl: '/Pn/Content/CONTENT_Index',
        controller: 'CONTENT_IndexCtr'
    });
    $routeProvider.when("/NOTI_Index", {
        templateUrl: '/Pn/Content/NOTI_Index',
        controller: 'NOTI_IndexCtr'
    });
    $routeProvider.when("/SLIDE_Index", {
        templateUrl: '/Pn/Content/SLIDE_Index',
        controller: 'SLIDE_IndexCtr'
    });
    $routeProvider.when("/NOTI_Index", {
        templateUrl: '/Pn/Content/NOTI_Index',
        controller: 'NOTI_IndexCtr'
    });
    $routeProvider.when("/FOOTER_Index",
    {
        templateUrl: '/Pn/Content/FOOTER_Index',
        controller: 'FOOTER_IndexCtr'
    });
    $routeProvider.when("/SEO_Index",
    {
        templateUrl: '/Pn/Content/SEO_Index',
        controller: 'SEO_IndexCtr'
    });

    $routeProvider.when("/CONTACT_Index", {
        templateUrl: '/Pn/CONTACT/CONTACT_Index',
        controller: 'CONTACT_IndexCtr'
    });

    $routeProvider.when("/MENU_Index", {
        templateUrl: '/Pn/Menu/MENU_Index',
        controller: 'MENU_IndexCtr'
    });

    $routeProvider.when("/PAGE_Index", {
        templateUrl: '/Pn/Page/PAGE_Index',
        controller: 'PAGE_IndexCtr'
    });

    $routeProvider.otherwise({ redirectTo: "/DASH_Index" });
});


app.directive('ckEditor',
    function() {
        return {
            require: '?ngModel',
            link: function(scope, elm, attr, ngModel) {
                var ck = CKEDITOR.replace(elm[0]);
                if (!ngModel) return;
                ck.on('instanceReady',
                    function() {
                        ck.setData(ngModel.$viewValue);
                    });

                function updateModel() {
                    scope.$apply(function() {
                        ngModel.$setViewValue(ck.getData());
                    });
                }

                ck.on('change', updateModel);
                ck.on('key', updateModel);
                ck.on('dataReady', updateModel);

                ngModel.$render = function(value) {
                    ck.setData(ngModel.$viewValue);
                };
            }
        };
    });

app.directive('html', ['$compile', function ($compile) {
    return function (scope, element, attrs) {
        scope.$watch(
            function (scope) {
                return scope.$eval(attrs.compile);
            },
            function (value) {                
                element.html(value);      
                $compile(element.contents())(scope);
            }
        );
    };
}]);

app.directive('vForm', [function () {
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
                    if (lst==null && lst==undefined)
                        lst = $element.find("textarea[ng-model='" + options.model + "']");
                    if (lst!=null && lst !=undefined) {
                        var error = angular.element(lst.closest('div.rowinput')).find('div.error');
                        if (error != null && error != undefined) {
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

                        if (angular.element(v).attr("v-form-drdl") == 'true') {
                            var cbb = $scope[$(v).attr('kendo-drop-down-list')];                            
                            if (cbb.select()<0 || cbb.value()==undefined || cbb.text()=='' || cbb.value()=='') {
                                bValid = false;
                                sValid = 'Chưa chọn';
                            }
                            
                        }
                        
                        if (angular.element(v).attr("v-form-require") == 'true') {
                            if (v.value == null || v.value == 0 || v.value == undefined || v.value == '') {
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
                        if (bValid == true && angular.element(v).attr("v-form-length") != null && angular.element(v).attr("v-form-length") != undefined) {

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
                        if (error != null && error !=undefined) {
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

app.directive("modalShow", function () {
    return {
        restrict: "A",
        scope: {
            modalVisible: "="
        },
        link: function (scope, element, attrs) {

            //Hide or show the modal
            scope.showModal = function (visible) {
                if (visible) {
                    element.modal("show");
                }
                else {
                    element.modal("hide");
                }
            }

            //Check to see if the modal-visible attribute exists
            if (!attrs.modalVisible) {

                //The attribute isn't defined, show the modal by default
                scope.showModal(true);

            }
            else {

                //Watch for changes to the modal-visible attribute
                scope.$watch("modalVisible", function (newValue, oldValue) {
                    scope.showModal(newValue);
                });

                //Update the visible value when the dialog is closed through UI actions (Ok, cancel, etc.)
                element.bind("hide.bs.modal", function () {
                    scope.modalVisible = false;
                    if (!scope.$$phase && !scope.$root.$$phase)
                        scope.$apply();
                });

            }

        }
    };

});

app.controller('indexController',
[
    '$scope', '$rootScope', function($scope, $rootScope) {
        $rootScope.title = 'Quản trị';
        $rootScope.ShowMenu_Children = false;

        Date.prototype.addDays = function(days) {
            var dat = new Date(this.valueOf());
            dat.setDate(dat.getDate() + days);
            return dat;
        };
        $rootScope.menuClick_Children = function($event) {
            $event.preventDefault();
            //$rootScope.ShowMenu_Children = !$rootScope.ShowMenu_Children;
        }
    }
]);

