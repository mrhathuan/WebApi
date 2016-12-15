//#region Members
var lang = 'vi-VN';
//#endregion

//var service = 'MobileService.asmx/';
//var server = '/';

//#region Common
var Common = {
    LogHistory:[],
    IsReady: false,
    CATTypeOfFileCode: {
        NONE: 'None',
        FUNCTION: 'Function',
        USER: 'User',
        CUSTOMER: 'Customer',
        TROUBLE: 'Trouble',
        TROUBLECO: 'TroubleCO',
        COPOD: 'COPOD',
        DIPOD: 'DIPOD',
        TEMPLATEREPORT: 'TemplateReport',
        RECEIPT: 'Receipt',
        //BOOKINGCONFIRM: 'BookingConfirmation',
        ORD: 'Order',
        ImportOPS: 'ImportOPS'
    },
    Auth: {
        HeaderKey: '',
        Item: null,
        GetHeaderKey: function ($cookies) {
            //return $cookies.getObject('HeaderKey');
        },
        SetHeaderKey: function ($cookies,key) {
            this.HeaderKey = key;
            $cookies.putObject('HeaderKey', key, {});
        }
    },
    Services: {
        url: {
            Host:'http://localhost:2743/',
            SYS: '/api/SYS/',
            MOBI: '/api/Mobile/',

            domain: 'smartlog.vn',
        },
        Type: { GET: 'GET', POST: 'POST', PUT: 'PUT' },
        Call: function ($http, options) {
            /// <summary>
            /// Call service
            /// </summary>
            /// <param name="url" type="string">action of controller. Examle: @Url.CustomAction(action, controller)</param>
            /// <param name="type" type="Common.Services.Type">use Common.Services.Type</param>
            /// <param name="data" type="object">data send</param>
            /// <param name="method" type="string">method</param>
            /// <param name="success" type="function">function success(response)</param>
            /// <param name="error" type="function">function error(jqXHR, textStatus, errorThrown)</param>

            options = angular.merge({}, {
                url: '',
                method: '',
                type: 'POST',
                data: {},
                success: null,
                error: null,
                showLoading: true,
                contentType: 'application/json; charset=utf-8',
                dataType: 'json'
            }, options);

            $http({
                method: options.type,
                url: options.url + options.method,
                headers: {
                    'Content-Type': options.contentType,
                    'auth': Common.Auth.HeaderKey
                },
                data: Common.Services._tojson(options.data)
            }).then(function successCallback(res) {
                if (res.status == 200) {
                    if (Common.HasValue(options.success))
                        options.success(res.data);
                }
                else if (res.status == 204) {
                    if (Common.HasValue(options.success))
                        options.success(res.data);
                }
                else {
                    setTimeout(function () {
                        var scope = angular.element($(document).find('html')[0]).scope();
                        scope.$apply(function () {
                            scope.$root.IsLoading = false;
                            var msgText = res.statusText;
                            if (res.data && res.data.ErrorMessage) {
                                msgText = res.data.ErrorMessage;
                            }
                            scope.$root.PopupAlert({ title: msgText });
                        });
                    }, 1);

                    if (Common.HasValue(options.error))
                        options.error(res.data);
                }

            }, function errorCallback(res) {
                setTimeout(function () {
                    var scope = angular.element($(document).find('[ng-controller=indexController]')).scope()
                    scope.$apply(function () {
                        scope.$root.IsLoading = false;
                        var msgText = res.statusText;
                        if (res.data && res.data.ErrorMessage) {
                            msgText = res.data.ErrorMessage;
                        }
                        scope.$root.PopupAlert({ title: msgText });
                    });
                }, 1);
                if (Common.HasValue(options.error))
                    options.error(res.data);
            });
        },
        Error: function (e, success, fail) {
            if (Common.HasValue(e)) {
                if (success != null)
                    success(e);
            }
            else {
                setTimeout(function () {
                    var scope = angular.element($(document).find('html')[0]).scope();
                    scope.$apply(function () {
                        scope.$root.Message({ Msg: 'Fail', NotifyType: Common.Message.NotifyType.ERROR });
                    });
                }, 1);

                if (fail != null)
                    fail(e);
            }
        },
        _tojson: function (obj) {
            return JSON.stringify(obj);
        }
    },
    RootObj: {},
    Cookie: {
        Get: function (name) {
            return $.cookie(name);
        },
        Set: function (name, value) {
            $.cookie(name, value, { expires: 7, path: '/' });
        },
        Clear: function (name) {
            $.removeCookie(name, { path: '/' });
        }
    },
    HasValue: function (obj) {
        if (obj != null && obj != undefined) return true;
        return false;
    },
    Log: function (msg) {
        //this.LogHistory.push(msg);
        console.log(msg);
    },
    Message: {
        Show: function (message, type) {
            /// <summary>
            /// Show message in page
            /// </summary>
            /// <param name="message" type="string">Input message notification</param>
            /// <param name="type" type="Type">Type of message</param>
            if (Common.HasValue(type)) type = Common.Message.Type.INFO;

            //            if (Common.Message._notification == null || Common.Message._notification.length == 0) {
            //                Common.Message._notification = $("#page-notification").kendoNotification({
            //                    // hide automatically after 7 seconds
            //                    autoHideAfter: 5000,
            //                    // prevent accidental hiding for 1 second
            //                    allowHideAfter: 1000,
            //                    // show a hide button
            //                    button: true,
            //                    // prevent hiding by clicking on the notification content
            //                    hideOnClick: false
            //                }).data("kendoNotification");
            //            }

            //            if (Common.HasValue(Common.Message._notification)) {
            //                if (message.indexOf('<p>') >= 0) {
            //                    message = message.replace(/<p>/g, '');
            //                    var strs = message.split('</p>');
            //                    for (var i = 0; i < strs.length - 1; i++) {
            //                        ShowMessage(strs[i], type);
            //                    }
            //                }
            //                else {
            //                    Common.Message._notification.show(message, type);
            //                }
            //            }
        },
        Alert: function (message, callback, param, title, textok) {
            /// <summary>
            /// Alert
            /// </summary>
            /// <param name="message" type="string">Input message</param>
            /// <param name="callback" type="function">function(param)</param>
            /// <param name="param" type="object">param</param>
            //            ShowCommonDialogAlert(message, callback, param, title, textok);
        },
        Confirm: function (message, callok, callcancel, param, title, textok, textcancel) {
            /// <summary>
            /// Confirm
            /// </summary>
            /// <param name="message" type="string">Input message</param>
            /// <param name="callok" type="function">function(param)</param>
            /// <param name="callcancel" type="function">function(param)</param>
            /// <param name="param" type="object">param</param>
            //            ShowCommonDialogConfirm(message, callok, callcancel, param, title, textok, textcancel);
            var i = '1';
        },
        Type: {
            INFO: 'info',
            SUCCESS: 'success',
            WARNING: 'warning',
            ERROR: 'error'
        },
        _notification: null
    },
    URL: function (str) {
        return encodeURIComponent(str).replace(/['()]/g, escape).replace(/\*/g, '%2A').replace(/%(?:7C|60|5E)/g, unescape);
    },
    Controller: {
        IsFirst: function (ctrname) {
            if (this._listFirst.indexOf(ctrname) >= 0)
                return false;
            else {
                this._listFirst.push(ctrname);
                return true;
            }
        },
        _listFirst: []
    },
    Cache: {
        _listCacheKey: [],
        _listCacheValue: [],
        Get: function (key) {
            var index = this._listCacheKey.indexOf(key);
            if (index >= 0) {
                var dt = new Date();
                if (this._listCacheValue[index].Date > dt) {
                    return this._listCacheValue[index].Value;
                }
            }
            return null;
        },
        Set: function (key, value) {
            var index = this._listCacheKey.indexOf(key);
            //set 2 min
            var dt = new Date((new Date()).getTime() + (2 * 60000));
            if (index >= 0)
                this._listCacheValue[index] = { 'Date': dt, 'Value': value };
            else {
                this._listCacheKey.push(key);
                this._listCacheValue.push({ 'Date': dt, 'Value': value });
            }
        }
    },
    Date: {
        Format: {
            //en: MM/DD/YYYY, vi: DD/MM/YYYY
            DMY: 'dd/MM/yyyy',
            //en: MM/DD/YYYY HH:mm, vi: DD/MM/YYYY HH:mm
            DMYHM: 'dd/MM/yyyy HH:mm',

            HM: 'HH:mm',
            DDMM: 'dd/MM',
            DDMMHM: 'dd/MM HH:mm',

            DDMMYY: 'dd/MM/yyyy',
            DMYHMS: 'dd/MM/yyyy HH:mm:ss',

            YYMMDD: 'yyyy-MM-dd',
            YYMMDDHM: 'yyyy-MM-dd HH:mm'
        },
        ToString: function (pValue, pFormat) {
            if (!Common.HasValue(pFormat)) pFormat = Common.Date.Format.YYMMDDHM;
            return kendo.toString(pValue, pFormat);
        },
        Date: function (pDate) {
            if (Common.HasValue(pDate) && Common.HasValue(pDate.getTime))
                return new Date(pDate.getFullYear(), pDate.getMonth(), pDate.getDate());
            else
                return pDate;
        },
        AddDay: function (pDate, pVal) {
            if (Common.HasValue(pDate) && Common.HasValue(pDate.getTime))
                return new Date(pDate.getTime() + (pVal * 24 * 60 * 60000));
            else
                return pDate;
        },
        AddHour: function (pDate, pVal) {
            if (Common.HasValue(pDate) && Common.HasValue(pDate.getTime))
                return new Date(pDate.getTime() + (pVal * 60 * 60000));
            else
                return pDate;
        },
        AddMinute: function (pDate, pVal) {
            if (Common.HasValue(pDate) && Common.HasValue(pDate.getTime))
                return new Date(pDate.getTime() + (pVal * 60000));
            else
                return pDate;
        },
        EndDay: function (pDate) {
            if (Common.HasValue(pDate) && Common.HasValue(pDate.getTime)){
                pDate.setHours(23, 59, 59, 999);
                return pDate;
            }
            else
                return pDate;
        },
        StartDay: function (pDate) {
            if (Common.HasValue(pDate) && Common.HasValue(pDate.getTime)) {
                pDate.setHours(0, 0, 0, 0);
                return pDate;
            }
            else
                return pDate;
        }
    },
    ListCategory: [
            { ID: 1, CategoryName: 'Sản phẩm độc đáo', CategoryCode: 'docdao', ACode: 'docdao', Childs: [] },
            { ID: 2, CategoryName: 'Sản phẩm khuyên dùng', CategoryCode: 'khuyendung', ACode: 'khuyendung', Childs: [] },
            {
                ID: 3, CategoryName: 'Nhà bếp', CategoryCode: 'nhabep', ACode: '', Childs: [
                  { ID: -1, CategoryName: 'Sản phẩm quanh sàn bếp', CategoryCode: '' },
                  { ID: -1, CategoryName: 'Dụng cụ nấu ăn', CategoryCode: '' },
                  { ID: -1, CategoryName: 'Dụng cụ làm bánh', CategoryCode: '' },
                  { ID: -1, CategoryName: 'Sản phẩm trên bàn ăn', CategoryCode: '' },
                  { ID: -1, CategoryName: 'Hộp cơm và phụ kiện', CategoryCode: '' },
                  { ID: -1, CategoryName: 'Đồ đựng nhà bếp', CategoryCode: '' }
                ]
            },
            {
                ID: 4, CategoryName: 'Gia dụng khác', CategoryCode: 'giadungkhac', ACode: '', Childs: [
                  { ID: -1, CategoryName: 'Sản phẩm nhà tắm', CategoryCode: '' },
                  { ID: -1, CategoryName: 'Sản phẩm nhà vệ sinh', CategoryCode: '' }
                ]
            },
            { ID: 5, CategoryName: 'Ngoài trời - Đi lại', CategoryCode: 'ngoaitroidilai', ACode: '', Childs: [] },
            { ID: 6, CategoryName: 'Dụng cụ đồ nghề', CategoryCode: 'dungcudonghe', ACode: '', Childs: [] }
    ],
    Data: {
        Each: function (pArray, pCallback) {
            if (pArray != null && pCallback != null) {
                for (var i = 0; i < pArray.length; i++) {
                    try {
                        if (Common.HasValue(pArray[i]))
                            pCallback(pArray[i], i);
                    } catch (e) {
                        Common.Log(e);
                        throw e;
                    }

                }
            }
        },
        Where: function (pArray, pCondition, pCallback) {
            if (pArray != null && pCondition != null) {
                if (pCallback != null) {
                    for (var i = 0; i < pArray.length; i++) {
                        if (Common.HasValue(pArray[i]))
                            if (pCondition(pArray[i]))
                                pCallback(pArray[i], i);
                    }
                }
                else {
                    var result = new Array();
                    this.Each(pArray, function (o) {
                        if (pCondition(o))
                            result.push(o);
                    });
                    return result;
                }
            }
        },
        First: function (pArray, pCondition) {
            if (pArray != null) {
                if (pCondition != null) {
                    for (var i = 0; i < pArray.length; i++) {
                        if (pCondition(pArray[i]))
                            return pArray[i];
                    }
                }
                else if (pArray.length > 0)
                    return pArray[0];
                return null;
            }
        }
    },
};

//#endregion
