//#region Common
var Common = {
    ALL: {
        URL: {
            Country: 'ALL_Country',
            District: 'ALL_District',
            Province: 'ALL_Province',
            Ward: 'ALL_Ward',
            Customer: 'ALL_Customer',
            CustomerInUser: 'ALL_CustomerInUser',
            Vendor: 'ALL_Vendor',
            VendorInUser: 'ALL_VendorInUser',
            GroupOfVehicle: 'ALL_GroupOfVehicle',
            Service: 'ALL_Service',
            CATPackingCO: 'ALL_CATPackingCO',
            TypeOfVehicle: 'ALL_TypeOfVehicle',
            Material: 'ALL_Material',

            CATGroupOfLocation: 'ALL_CATGroupOfLocation',
            CATGroupOfRomooc: 'ALL_CATGroupOfRomooc',
            CATGroupOfEquipment: 'ALL_CATGroupOfEquipment',
            CATGroupOfPartner: 'All_CATGroupOfPartner',
            CATGroupOfMaterial: 'ALL_CATGroupOfMaterial',
            CATGroupOfCost: 'ALL_CATGroupOfCost',
            CATTypeOfPriceDIEx: 'ALL_CATTypeOfPriceDIEx',
            CATTypeOfPriceCOEx: 'ALL_CATTypeOfPriceCOEx',

            OPSTypeOfDITOGroupProductReturn: 'ALL_OPSTypeOfDITOGroupProductReturn',

            TypeOfRunLevel: 'ALL_SYSVarTypeOfRunLevel',
            TypeOfSGroupProductChange: 'ALL_TypeOfSGroupProductChange',
            TypeOfDITOGroupProductReturnStatus: 'ALL_SYSVarTypeOfDITOGroupProductReturnStatus',
            SYSVarTypeOfTOLocation: 'ALL_SYSVarTypeOfTOLocation',
            SYSVarDIExSum: 'ALL_SYSVarDIExSum',
            SYSVarCOExSum: 'ALL_SYSVarCOExSum',
            SYSVarDIMOQSum: 'ALL_SYSVarDIMOQSum',
            SYSVarDIMOQLoadSum: 'ALL_SYSVarDIMOQLoadSum',
            SYSVarServiceOfOrder: 'ALL_SYSVarServiceOfOrder',
            SYSVarTransportMode: 'ALL_SYSVarTransportMode',
            SYSVarTypeOfOrder: 'ALL_SYSVarTypeOfOrder',
            SYSVarTypeOfContractQuantity: 'ALL_SYSVarTypeOfContractQuantity',
            SYSVarScheduleDetailType: 'ALL_SYSVarScheduleDetailType',
            SYSVarTypeOfPriceEX: 'ALL_SYSVarTypeOfPriceEX',
            SYSVarTypeOfReason: 'ALL_SYSVarTypeOfReason',
            SYSVarTypeOfDIPODStatus: 'ALL_SYSVarTypeOfDIPODStatus',
            SYSVarMaterialAuditStatus:'ALL_SYSVarMaterialAuditStatus',

            SYSVarTypeOfContract: 'ALL_SYSVarTypeOfContract',
            SYSVarTypeOfContractDate: 'ALL_SYSVarTypeOfContractDate',
            SYSVarPriceOfGOP: 'ALL_SYSVarPriceOfGOP',
            SYSVarTypeOfDriverRouteFee: 'ALL_SYSVarTypeOfDriverRouteFee',
            SYSVarActivityRepeat: 'ALL_SYSVarActivityRepeat',
            SYSVarConstraintRequireType: 'ALL_SYSVarConstraintRequireType',
            SYSVarTypeOfWAInspectionStatus: 'ALL_SYSVarTypeOfWAInspectionStatus',

            SLI_SYSVarPriceOfGOP: 'SLI_SYSVarPriceOfGOP',
            ALL_CATPackingGOP: 'ALL_CATPackingGOP',
            ALL_TroubleCostStatus: 'ALL_TroubleCostStatus',
            SLI_SYSVarTransportModeOPSDI: 'SLI_SYSVarTransportModeOPSDI',
            SLI_SYSVarTypeOfActivity: 'SLI_SYSVarTypeOfActivity',
            SLI_CATPackingService: 'ALL_CATPackingService',
            SLI_SYSVarTypeOfGroupTrouble: 'SLI_SYSVarTypeOfGroupTrouble',
            SLI_SYSVarTypeOfCost: 'SLI_SYSVarTypeOfCost',
            ALL_SYSVarTypeOfDriver: 'ALL_SYSVarTypeOfDriver',
            ALL_SYSVarTypeOfScheduleFeeStatus: 'ALL_SYSVarTypeOfScheduleFeeStatus',

            ALL_SYSVarTypeOfKPI: 'ALL_SYSVarTypeOfKPI',
            ALL_SYSVarColumnType: 'ALL_SYSVarColumnType',
            ALL_FLMTypeOfScheduleFee: 'ALL_FLMTypeOfScheduleFee',

            ALL_SYSVarFLMTypeWarning: 'ALL_SYSVarFLMTypeWarning',

            ALL_SYSVarREPOwnerAsset: 'ALL_SYSVarREPOwnerAsset',

            ALL_SYSVarPacketProcessType: 'ALL_SYSVarPacketProcessType',
            ALL_SYSVarPacketSettingType: 'ALL_SYSVarPacketSettingType',

            ALL_SYSVarContractRoutingType: 'ALL_SYSVarContractRoutingType',
            ALL_SYSVarRouteDetailStatusMode: 'ALL_SYSVarRouteDetailStatusMode',
            ALL_SYSVarExtReturnStatus: 'ALL_SYSVarExtReturnStatus'
        },
        Get: function ($http, options) {
            options = $.extend(true, {
                url: '',
                success: null
            }, options);

            if (Common.HasValue(this._data[options.url])) {
                if (Common.HasValue(options.success))
                    options.success(this._data[options.url].Data);
            }
            else {
                Common.Services.Call($http, {
                    url: Common.Services.url.CAT,
                    method: options.url,
                    data: {},
                    success: function (res) {
                        Common.ALL._data[options.url] = res;
                        if (Common.HasValue(options.success))
                            options.success(Common.ALL._data[options.url].Data);
                    }
                });
            }
        },
        Refresh: function ($http, options) {
            options = $.extend(true, {
                url: '',
                success: null
            }, options);

            Common.Services.Call($http, {
                url: Common.Services.url.CAT,
                method: options.url,
                data: {},
                success: function (res) {
                    Common.ALL._data[options.url] = res;
                    if (Common.HasValue(options.success))
                        options.success(Common.ALL._data[options.url].Data);
                }
            });
        },
        Init: function ($http) {
            //var lst = [];
            $.each(this.URL, function (key, value) {
                if (Common.HasValue(value)) {
                    Common.ALL.Get($http, { url: value });
                }
                //lst.push(value);
            });

        },
        _data: []
    },
    IsReady: false,
    Auth: {
        HeaderKey: '',
        Item: {
            UserID: -1,
            UserName: '',
            GroupID: '',
            ListActionCode: []
        },
        GetHeaderKey: function () {
            return Common.Cookie.Get('HeaderKey');
        },
        SetHeaderKey: function (key) {
            this.HeaderKey = key;

            Common.Cookie.Set('HeaderKey', key);
        },

        SYSActionCode: {
            ActAdd: 'ActAdd',
            ActAddAndApproved: 'ActAddAndApproved',
            ActEdit: 'ActEdit',
            ActDel: 'ActDel',
            ActApproved: 'ActApproved',
            ActOPS: 'ActOPS',
            ActReturn: 'ActReturn',
            ActComment: 'ActComment',
            ActPrint: 'ActPrint',
            ActExcel: 'ActExcel',
            ActContainer: 'ActContainer',
            ActTruck: 'ActTruck',
            ActSave: 'ActSave',
            ActSaveAndApproved: 'ActSaveAndApproved'
        },
        SYSViewCode: {
            ViewAdmin: 'ViewAdmin',
            ViewCustomer: 'ViewCustomer',
            ViewVendor: 'ViewVendor',
            ViewApproved: 'ViewApproved',
            ViewDriver: 'ViewDriver'
        }
    },
    FolderUpload: {
        Images: '/images/',

        Customer: 'Uploads/cus/',
        Function: 'Uploads/function/',
        User: 'Uploads/user/',
        POD: 'Uploads/pod/',
        Trouble: 'Uploads/pod/',
        PLCosting: 'Uploads/plcosting/',
        Driver: 'Uploads/driver/',
        DITOLocation: 'Uploads/ditolocation/',
        Address: 'Uploads/address/',

        Export: 'Uploads/temp/',
        Import: 'Uploads/temp/'
    },
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
    Comment: {
        NONE: 'NONE',
        ORD: 'ORD',
        OPSCO: 'OPSCO',
        ORDDN: 'ORDDN'
    },
    SYSTypeOfEmailTemplate: {
        EmailTemplate_EmployeeReview: { Value: 1, Code: "EmailTemplate_EmployeeReview" },
        EmailTemplate_Support: { Value: 2, Code: 'EmailTemplate_Support' },
        EmailTemplate_SupportComplete: { Value: 3, Code: 'EmailTemplate_SupportComplete' },
        EmailTemplate_EmployeeContent: { Value: 4, Code: 'EmailTemplate_EmployeeContent' },
        EmailTemplate_MainToEmployee: { Value: 5, Code: 'EmailTemplate_MainToEmployee' },
        EmailTemplate_MainComplete: { Value: 6, Code: 'EmailTemplate_MainComplete' },
        EmailTemplate_ApprovedSalary: { Value: 7, Code: 'EmailTemplate_ApprovedSalary' }
    },
    Date: {
        ParseFormat: ["yyyy-MM-ddTHH:mm:ss"],
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
        FromJson: function (pValue) {
            if (Common.HasValue(pValue)) {
                var pValue = pValue.toString();
                if (pValue.lastIndexOf('/Date') > 0) {
                    var dateISO = /\d{4}-\d{2}-\d{2}T\d{2}:\d{2}:\d{2}(?:[.,]\d+)?Z/i;
                    var dateNet = /\/Date\((\d+)(?:-\d+)?\)\//i;

                    // Replacer RegExp
                    var replaceISO = /"(\d{4})-(\d{2})-(\d{2})T(\d{2}):(\d{2}):(\d{2})(?:[.,](\d+))?Z"/i;
                    var replaceNet = /"\\\/Date\((\d+)(?:-\d+)?\)\\\/"/i;
                    if (typeof (pValue) === "string") {
                        if (dateISO.test(pValue)) {
                            return new Date(pValue);
                        }
                        if (dateNet.test(pValue)) {
                            return new Date(parseInt(dateNet.exec(pValue)[1], 10));
                        }
                    }
                }
                else {
                    pValue = new Date(pValue);
                }
                return pValue;
            }
        },
        FromJsonDMY: function (pValue) {
            if (Common.HasValue(pValue)) {
                var val = Common.Date.FromJson(pValue);
                return kendo.toString(val, Common.Date.Format.DMY);
            }
            else
                return '';
        },
        FromJsonDMYHM: function (pValue) {
            if (Common.HasValue(pValue)) {
                var val = Common.Date.FromJson(pValue);
                return kendo.toString(val, Common.Date.Format.DMYHM);
            }
            else
                return '';
        },
        FromJsonDDMMHM: function (pValue) {
            if (Common.HasValue(pValue)) {
                var val = Common.Date.FromJson(pValue);
                return kendo.toString(val, Common.Date.Format.DDMMHM);
            }
            else
                return '';
        },

        FromJsonHM: function (pValue) {
            if (Common.HasValue(pValue)) {
                var val = Common.Date.FromJson(pValue);
                return kendo.toString(val, Common.Date.Format.HM);
            }
            else
                return '';
        },
        FromJsonDDMM: function (pValue) {
            if (Common.HasValue(pValue)) {
                var val = Common.Date.FromJson(pValue);
                return kendo.toString(val, Common.Date.Format.DDMM);
            }
            else
                return '';
        },
        FromJsonDDMMYY: function (pValue) {
            if (Common.HasValue(pValue)) {
                var val = Common.Date.FromJson(pValue);
                return kendo.toString(val, Common.Date.Format.DDMMYY);
            }
            else
                return '';
        },
        FromJsonDMYHMS: function (pValue) {
            if (Common.HasValue(pValue)) {
                var val = Common.Date.FromJson(pValue);
                return kendo.toString(val, Common.Date.Format.DMYHMS);
            }
            else
                return '';
        },

        FromJsonYYMMDD: function (pValue) {
            if (Common.HasValue(pValue)) {
                var val = Common.Date.FromJson(pValue);
                return kendo.toString(val, Common.Date.Format.YYMMDD);
            }
            else
                return '';
        },
        FromJsonYYMMDDHM: function (pValue) {
            if (Common.HasValue(pValue)) {
                var val = Common.Date.FromJson(pValue);
                return kendo.toString(val, Common.Date.Format.YYMMDDHM);
            }
            else
                return '';
        },

        ToString: function (pValue, pFormat) {
            if (!Common.HasValue(pFormat)) pFormat = Common.Date.Format.YYMMDDHM;
            return kendo.toString(pValue, pFormat);
        },
        ParseFromString: function (pValue, pFormat) {
            if (!Common.HasValue(pFormat)) pFormat = Common.Date.Format.YYMMDDHM;
            if (pValue.indexOf("/") >= 0) {
                var arr = pValue.split("/");
                return kendo.toString(new Date(arr[2], parseInt(arr[1]) - 1, arr[0]), pFormat);
            } else {
                if (pValue.indexOf("-") >= 0) {
                    pValue = pValue.replace("T", "-");
                    var arr = pValue.split("-");
                    return kendo.toString(new Date(arr[0], parseInt(arr[1]) - 1, arr[2]), pFormat);
                }
            }
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
        }
    },
    Services: {
        host: 'localhost',
        url: {
            SYS: 'api/SYS/',
            CFG: 'api/CFG/',
            OPS: 'api/OPS/',
            ORD: 'api/ORD/',
            MON: 'api/MON/',
            POD: 'api/POD/',
            Driver: 'api/Driver/',
            CAT: 'api/CAT/',
            FLM: 'api/FLM/',
            KPI: 'api/KPI/',
            CUS: 'api/CUS/',
            VEN: 'api/VEN/',
            FIN: 'api/FIN/',
            REP: 'api/REP/',
            WFL: 'api/WFL/',
            Trigger: 'api/Trigger/',
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

            options = $.extend(true, {
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
                            scope.$root.Message({ Msg: msgText, NotifyType: Common.Message.NotifyType.ERROR });
                        });
                    }, 1);

                    if (Common.HasValue(options.error))
                        options.error(res.data);
                }

            }, function errorCallback(res) {
                setTimeout(function () {
                    var scope = angular.element($(document).find('html')[0]).scope();
                    scope.$apply(function () {
                        scope.$root.IsLoading = false;
                        var msgText = res.statusText;
                        if (res.data && res.data.ErrorMessage) {
                            msgText = res.data.ErrorMessage;
                        }
                        scope.$root.Message({ Msg: msgText, NotifyType: Common.Message.NotifyType.ERROR });
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
    DataSource: {
        Local: function (options) {
            var options = $.extend(true, {
                data: [],
                model: {},
                group: [],
                sort: null,
                filter: [],
                aggregate: null,
                pageSize: null
            }, options);

            if (Common.HasValue(options.pageSize)) {
                return new kendo.data.DataSource({
                    data: options.data,
                    schema: { model: options.model },
                    pageSize: options.pageSize,
                    group: options.group,
                    filter: options.filter,
                    sort: options.sort,
                    aggregate: options.aggregate
                });
            }
            else {
                return new kendo.data.DataSource({
                    data: options.data,
                    schema: { model: options.model },
                    group: options.group,
                    filter: options.filter,
                    sort: options.sort,
                    aggregate: options.aggregate
                });
            }
        },
        Grid: function ($http, options) {
            options = $.extend(true, {
                url: '',
                method: '',
                type: Common.Services.Type.POST,
                group: [],
                serverPaging: true,
                serverSorting: true,
                serverFiltering: true,
                serverGrouping: false,
                batch: false,
                pageSize: 20,
                sort: [],
                readcustomize: null,
                readparam: null,
                model: {},
                filter: {},
                _columndate: []
            }, options);

            options._columndate = [];
            if (Common.HasValue(options.model)) {
                if (Common.HasValue(options.model.fields)) {
                    angular.forEach(options.model.fields, function (v, i) {
                        if (v.type == 'date')
                            options._columndate.push(i);
                    });
                }
            }

            var result = new kendo.data.DataSource({
                transport: {
                    read: function (e) {
                        if (Common.HasValue(options.readcustomize)) {
                            options.readcustomize(e);
                        }
                        else {
                            var webapi = new kendo.data.transports.webapi({ prefix: "" });

                            if (options._columndate.length > 0 && Common.HasValue(e.data.filter))
                                Common.DataSource._gridfilterdate(options._columndate, e.data.filter);

                            var params = webapi.parameterMap(e.data);

                            var data = null;
                            if (Common.HasValue(options.readparam))
                                data = options.readparam();
                            if (Common.HasValue(data))
                                data['request'] = params;
                            else
                                data = { request: params };

                            if (Common.HasValue(Common.Auth.Item) && Common.Auth.Item.UserID > 0) {
                                Common.Services.Call($http, {
                                    url: options.url,
                                    method: options.method,
                                    type: options.type,
                                    data: data,
                                    success: function (res) {
                                        if (Common.HasValue(res)) {
                                            e.success(res);
                                        }
                                    }
                                });
                            }
                            else
                                e.success({ Data: [], Total: 0 });
                        }
                    },
                    update: function () { },
                },
                schema: $.extend({}, kendo.data.schemas.webapi, { data: 'Data', total: 'Total', errors: 'Errors', model: options.model }),
                error: function (e, status) {

                },
                requestEnd: function (e) {

                },
                group: options.group,
                serverPaging: options.serverPaging,
                serverSorting: options.serverSorting,
                serverFiltering: options.serverFiltering,
                serverGrouping: options.serverGrouping,
                batch: options.batch,
                pageSize: options.pageSize,
                sort: options.sort,
                filter: options.filter,
                inputData: options
            });
            return result;
        },
        TreeList: function ($http, options) {
            var options = $.extend(true, {
                method: '',
                module: '',
                type: Common.Services.Type.POST,
                group: [],
                serverPaging: true,
                serverSorting: true,
                serverFiltering: true,
                serverGrouping: false,
                batch: false,
                readcustomize: null,
                readparam: null,
                sort: [],
                model: {}
            }, options);

            var result = new kendo.data.TreeListDataSource({
                transport: {
                    read: function (e) {
                        if (Common.HasValue(options.readcustomize)) {
                            options.readcustomize(e);
                        }
                        else {
                            var webapi = new kendo.data.transports.webapi({ prefix: "" });
                            var params = webapi.parameterMap(e.data);

                            var data = null;
                            if (Common.HasValue(options.readparam))
                                data = options.readparam();
                            if (Common.HasValue(data))
                                data['request'] = params;
                            else
                                data = { request: params };

                            Common.Services.Call($http, {
                                url: options.url,
                                method: options.method,
                                type: options.type,
                                data: data,
                                success: function (res) {
                                    if (Common.HasValue(res)) {
                                        e.success(res);
                                    }
                                }
                            });
                        }
                    }
                },
                schema: $.extend({}, kendo.data.schemas.webapi, { data: "Data", total: "Total", errors: "Errors", model: options.model }),
                error: function (e, status) {

                },
                requestEnd: function (e) {

                },
                group: options.group,
                serverPaging: options.serverPaging,
                serverSorting: options.serverSorting,
                serverFiltering: options.serverFiltering,
                serverGrouping: options.serverGrouping,
                batch: options.batch,
                pageSize: options.pageSize,
                sort: options.sort
            });
            return result;
        },

        _gridfilterdate: function (data, f) {
            var lstadd = [];
            $.each(f.filters, function (i, v) {
                if (data.indexOf(v.field) >= 0) {
                    var val = Common.Date.Date(v.value);
                    lstadd.push({ field: v.field, operator: 'lt', value: Common.Date.AddDay(val, 1) });
                    v.operator = 'gte';
                    v.value = val;
                }
            });
            if (lstadd.length > 0) {
                $.each(lstadd, function (i, v) {
                    f.filters.push(v);
                });
            }
        }
    },
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
        console.log(msg);
    },
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
    },
    Timeline: function (options) {
        this.options = $.extend(true, {
            grid: null,
            model: null,
            modelGroup: null,
            modelSort: null,
            columns: [],
            search: {
                DateFrom: Common.Date.AddDay(new Date(), -2),
                DateTo: Common.Date.AddDay(new Date(), 2),
                HourFrom: Common.Date.AddDay(new Date(), -2),
                HourTo: Common.Date.AddHour(new Date(), 8),
                RouteInDay: 3
            },
            eventMainData: null,
            eventDetailData: null,
            eventClickTime: null,
            eventDrop: null,
            eventDropInTimeline: null,
            eventSelect: null,

            _columnsCount: 0,
            _isInTimeline: false,
            _isDrag: false,
            _fieldDate: [],
            _mainData: [],
            _listRouteInDay: null
        }, options);

        this.Init = function () {
            if (Common.HasValue(this.options.grid)) {
                this.options._columnsCount = this.options.columns.length;

                this.CreateRouteInDay();
                this.CreateColumns();
                this.CreateGrid();

                if (Common.HasValue(this.options.eventMainData))
                    this.options.eventMainData();
            }
        }

        this.CreateRouteInDay = function () {
            Common.Log('Timeline : CreateRouteInDay');
            var me = this.options;

            var f = me.search.HourFrom;
            var t = me.search.HourTo;
            var h = (t.getTime() - f.getTime()) / 3600000;
            me._listRouteInDay = [];
            if (h > 0) {
                if (me.search.DateFrom.getTime() != me.search.DateTo.getTime()) {
                    var p = h / me.search.RouteInDay;
                    if (p % 1 > 0) {
                        if (p % 1 >= 0.5)
                            p = p - (p % 1) + 0.5;
                        else
                            p = p - (p % 1);
                    }
                    for (var i = 1; i <= me.search.RouteInDay; i++) {
                        me._listRouteInDay.push({ 'Title': i + '', 'Name': i + '', 'HourFrom': f, 'HourTo': new Date(f.getTime() + (p * 3600000)) });
                        f = new Date(f.getTime() + (p * 3600000));
                    }
                }
                else {
                    var time = t.getTime() - f.getTime();
                    if ((time / (1 * 3600000)) % 1 > 0)
                        t = new Date(t.getTime() + (0.5 * 3600000));
                    while (f.getTime() <= t.getTime()) {
                        me._listRouteInDay.push({ 'Title': Common.Date.ToString(f, 'HH:mm'), 'Name': Common.Date.ToString(f, 'HH_mm'), 'HourFrom': f, 'HourTo': new Date(f.getTime() + (1 * 3600000)) });
                        f = new Date(f.getTime() + (1 * 3600000));
                    }
                }
            }

        }

        this.CreateColumns = function () {
            Common.Log('Timeline : CreateColumns');
            var me = this.options;

            if (me._fieldDate.length > 0) {
                me.columns.splice(me._columnsCount, me.columns.length - me._columnsCount);
                me._fieldDate = [];
            }

            var f = me.search.DateFrom;
            while (f <= me.search.DateTo) {
                var sd = 'col' + f.getFullYear() + '' + f.getMonth() + '' + f.getDate();
                var cols = [];
                $.each(me._listRouteInDay, function (index, route) {
                    var field = sd + route.Name;
                    me._fieldDate.push(field);
                    cols.push({
                        field: field, width: '45px', title: route.Title, attributes: {
                            'class': 'no-padding tdtime',
                            'date': Common.Date.ToString(f, Common.Date.Format.YYMMDD),
                            'hourfrom': Common.Date.ToString(route.HourFrom, 'HH:mm'),
                            'hourto': Common.Date.ToString(route.HourTo, 'HH:mm')
                        }
                    });
                });
                me.columns.push({ title: Common.Date.ToString(f, Common.Date.Format.DDMMYY), columns: cols });
                f = Common.Date.AddDay(f, 1);
            }
            me.columns.push({ title: ' ', filterable: false, sortable: false });
        }

        this.CreateGrid = function () {
            Common.Log('Timeline : CreateGrid');
            var me = this.options;

            //var grid = $(me.selector).data('kendoGrid');
            //if (Common.HasValue(grid)) {
            //    grid.destroy();
            //    $(me.selector).empty();
            //}
            me.grid.setOptions({
                dataSource: Common.DataSource.Local({
                    data: [],
                    model: me.model,
                    group: me.modelGroup,
                    sort: me.modelSort
                }),
                height: '99%', groupable: false, pageable: false, sortable: true, columnMenu: false, filterable: false, resizable: true,
                selectable: 'row',
                columns: me.columns,
                change: function (e) {
                    //var me = Common.Controls.Grid._timelinecurrent;
                    //var grid = $(me.selector).data('kendoGrid');
                    var grid = me.grid;

                    if (Common.HasValue(me.eventSelect) && !me._isDrag) {
                        var sel = e.sender.select();
                        if (Common.HasValue(sel)) {
                            if (sel.length > 0) {
                                var lstid = [];
                                var lst = [];
                                $.each(sel, function (itr, tr) {
                                    var item = grid.dataItem(tr);
                                    if (lstid.indexOf(item.ID) < 0) {
                                        lstid.push(item.ID);
                                        lst.push(item);
                                    }
                                });
                                me.eventSelect(lst);
                            }
                        }
                    }
                },
                dataBinding: function (e) {
                    Common._timelineBinding = true;
                },
                dataBound: function (e) {
                    var grid = me.grid;

                    if (grid != null && Common.HasValue(grid.tbody)) {
                        var lst = grid.tbody.find('tr');
                        var groupcell = [];
                        $.each(lst, function (i, v) {
                            var item = grid.dataItem(v);
                            $(v).find('td.tdtime').each(function () {
                                if ($(this).html() != '') {
                                    var strs = $(this).html().split(';;');
                                    var typeid = parseInt(strs[1]);
                                    var typeclass = 'black';

                                    switch (typeid) {
                                        case 1: typeclass = 'approved'; break;
                                        case 2: typeclass = 'tendered'; break;
                                        case 3: typeclass = 'received'; break;
                                        case -1: typeclass = 'maintenance'; break;
                                        case -2: typeclass = 'skyblue'; break;
                                        case -3: typeclass = 'lightcyan'; break;
                                        case -4: typeclass = 'seablue'; break;
                                        case -5: typeclass = 'royalblue'; break;
                                    }
                                    var note = (Common.HasValue(strs[3]) && strs[3] != '') ? strs[3] : '&nbsp;';
                                    $(this).html('<a targetid=' + strs[0] + ' typeid=' + typeid + ' href="/" title="' + strs[2] + '" class="timeline ' + typeclass + '">' + note + '</a>');
                                    if (!Common.HasValue(groupcell[strs[0]]))
                                        groupcell[strs[0]] = [];
                                    groupcell[strs[0]].push(this);
                                }
                                else {
                                    $(this).html('<div class="allowdrop" style="width:100%;">&nbsp;</div>');
                                }
                            });
                        });
                        $.each(groupcell, function (i, vg) {
                            if (Common.HasValue(vg)) {
                                if (vg.length > 1) {
                                    var td = vg[0];
                                    $(td).attr('colspan', vg.length);
                                    for (var i = 1; i < vg.length; i++) {
                                        td = vg[i];
                                        $(td).remove();
                                    }
                                }
                            }
                        });

                        $(grid.element).find('tbody > tr > td.no-padding > a.timeline').click(function (e) {
                            e.preventDefault();
                            //var grid = $(e.target).closest('div.cus-grid.k-grid').data('kendoGrid');
                            var grid = me.grid;
                            var id = $(this).attr('targetid');
                            var typeid = parseInt($(this).attr('typeid'));
                            var tr = $(this).closest('tr');
                            var item = grid.dataItem(tr);
                            grid.tbody.find(".timeline.selected").removeClass("selected");
                            $(this).addClass("selected");
                            if (Common.HasValue(me.eventClickTime)) {
                                me.eventClickTime(id, item, typeid);
                            }
                        });

                        if (Common.HasValue(me.eventDrop)) {
                            if (Common.HasValue(me.eventDropInTimeline)) {
                                grid.table.kendoDraggable({
                                    filter: 'tbody > tr > td.no-padding > a.timeline',
                                    group: 'gridGroup',
                                    hint: function (e) {
                                        //var me = Common.Controls.Grid._timelinecurrent;

                                        me._isInTimeline = true;
                                        return '<div><span class="fa fa-calendar" style="font-size:16px;"></span></div>';
                                    }
                                });
                            }

                            $(grid.element).find('td > .allowdrop').kendoDropTarget({
                                group: 'gridGroup',
                                drop: function (e) {
                                    //var me = Common.Controls.Grid._timelinecurrent;
                                    //var grid = $(me.selector).data('kendoGrid');
                                    var grid = me.grid;

                                    var tr = $(e.dropTarget).closest('tr');
                                    var item = grid.dataItem(tr);
                                    var td = $(e.dropTarget).closest('td');
                                    var tdtime = null;
                                    if ($(td).hasClass('tdtime')) {
                                        var strdate = $(td).attr('date'); var strhourfrom = $(td).attr('hourfrom'); var strhourto = $(td).attr('hourto');
                                        var idate = null; var ifrom = null; var ito = null;
                                        if (Common.HasValue(strdate)) {
                                            idate = new Date(strdate);
                                            var hour = parseInt(strhourfrom.substr(0, strhourfrom.indexOf(':')));
                                            var min = parseInt(strhourfrom.substr(strhourfrom.indexOf(':') + 1, strhourfrom.length - strhourfrom.indexOf(':')));
                                            ifrom = new Date(idate.getFullYear(), idate.getMonth(), idate.getDate(), hour, min);
                                            hour = parseInt(strhourto.substr(0, strhourto.indexOf(':')));
                                            min = parseInt(strhourto.substr(strhourto.indexOf(':') + 1, strhourto.length - strhourto.indexOf(':')));
                                            ito = new Date(idate.getFullYear(), idate.getMonth(), idate.getDate(), hour, min);

                                            tdtime = { 'Date': idate, 'HourFrom': ifrom, 'HourTo': ito };
                                        }
                                    }

                                    if (!me._isInTimeline)
                                        me.eventDrop(e, tdtime, item);
                                    else {
                                        me._isInTimeline = false;
                                        var id = $(e.draggable.currentTarget).attr('targetid');
                                        var typeid = parseInt($(e.draggable.currentTarget).attr('typeid'));

                                        var data = { id: id, typeid: typeid, source: grid.dataItem($(e.draggable.currentTarget).closest('tr')), target: item, tdtime: tdtime };
                                        me.eventDropInTimeline(e, data);
                                    }

                                    me._isDrag = false;
                                },
                                dragenter: function (e) {
                                    //var me = Common.Controls.Grid._timelinecurrent;
                                    //var grid = $(me.selector).data('kendoGrid');

                                    var grid = me.grid;
                                    me._isDrag = true;
                                    var tr = $(e.dropTarget).closest('tr');
                                    grid.select(tr);
                                },
                                dragleave: function (e) {
                                    //var me = Common.Controls.Grid._timelinecurrent;
                                    me._isDrag = false;
                                }
                            });
                        }

                        setTimeout(function () {
                            //Common.Log('Timeline: run scroll');
                            var selector = Common._timelineGrid.element.attr('kendo-grid');
                            if (Common.HasValue(selector) && selector != '') {
                                var scrollTopCache = Common.Cookie.Get(selector + 'scrollTop');
                                Common.Log('Timeline: scroll' + scrollTopCache);
                                if (Common.HasValue(scrollTopCache) && scrollTopCache != '')
                                    $(Common._timelineGrid.element).find('.k-grid-content').scrollTop(scrollTopCache);
                                else
                                    Common.Cookie.Set(selector + 'scrollTop', '0');
                            }
                        }, 500);
                        Common._timelineGrid = me.grid;

                        setTimeout(function () {
                            Common._timelineBinding = false;
                        }, 500);
                    }
                    Common._timelineBinding = true;
                }
            });

            Common._timelineGrid = me.grid;
            var selector = Common._timelineGrid.element.attr('kendo-grid');
            if (Common.HasValue(selector) && selector != '') {
                $(Common._timelineGrid.element).find('.k-grid-content').scroll(function (e) {
                    var grid = $(this).closest('.cus-grid.k-grid');
                    var selector = $(grid).attr('kendo-grid');
                    if (Common._timelineBinding == false) {
                        //Common.Log('Timeline: set scroll' + e.target.scrollTop);
                        Common.Cookie.Set(selector + 'scrollTop', e.target.scrollTop);
                    }
                });
            }
        }

        this.SetMainData = function (data) {
            Common.Log('Timeline : SetMainData');
            var me = this.options;

            me._mainData = [];
            $.each(data, function (i, v) {
                me._mainData[v.ID] = v;
            });

            var dtFrom = Common.Date.Date(me.search.DateFrom);
            var dtTo = Common.Date.AddDay(Common.Date.Date(me.search.DateTo), 1);

            if (Common.HasValue(me.eventDetailData))
                me.eventDetailData(dtFrom, dtTo);
        };

        this.SetDetailData = function (data) {
            Common.Log('Timeline : SetDetailData');
            var me = this.options;

            var lstTemp = $.extend(true, {}, me._mainData);
            $.each(data, function (i, v) {
                //var f = Common.Date.FromJson(v.DateFrom);
                //var t = Common.Date.FromJson(v.DateTo);
                var f = v.DateFrom;
                var t = v.DateTo;
                var lsthour = [];
                var firstindex = -1;
                var lastindex = -1;
                $.each(me._listRouteInDay, function (ih, vh) {
                    var hourf = new Date(2015, 1, 1, f.getHours(), f.getMinutes());
                    var hourt = new Date(2015, 1, 1, t.getHours(), t.getMinutes());
                    var hourif = new Date(2015, 1, 1, vh.HourFrom.getHours(), vh.HourFrom.getMinutes());
                    var hourit = new Date(2015, 1, 1, vh.HourTo.getHours(), vh.HourTo.getMinutes());

                    if (((hourif.getTime() <= hourf.getTime() && hourit.getTime() > hourf.getTime()) || firstindex >= 0) &&
                        hourif.getTime() <= hourt.getTime()) {
                        if (firstindex < 0)
                            firstindex = ih;
                        lastindex = ih;
                        lsthour[vh.Name] = vh;
                    }
                    else if (Common.Date.Date(f).getTime() != Common.Date.Date(t).getTime() && hourif.getTime() <= hourt.getTime()) {
                        if (firstindex < 0)
                            firstindex = ih;
                        lastindex = ih;
                        lsthour[vh.Name] = vh;
                    }
                });

                var sdf = 'col' + f.getFullYear() + '' + f.getMonth() + '' + f.getDate();
                var sdt = 'col' + t.getFullYear() + '' + t.getMonth() + '' + t.getDate();

                if (sdf == sdt) {
                    if (Common.HasValue(lstTemp[v.AssetID])) {
                        $.each(me._listRouteInDay, function (ih, vh) {
                            if (Common.HasValue(lsthour[vh.Name]))
                                lstTemp[v.AssetID][sdf + vh.Name] = v.ID + ';;' + v.TypeID + ';;' + v.StatusOfAssetTimeSheetName + ';;' + v.Note;
                        });
                    }
                }
                else {
                    if (Common.HasValue(lstTemp[v.AssetID])) {
                        while (f.getTime() <= t.getTime()) {

                            sdf = 'col' + f.getFullYear() + '' + f.getMonth() + '' + f.getDate();
                            $.each(me._listRouteInDay, function (ih, vh) {
                                if (firstindex >= ih)
                                    lstTemp[v.AssetID][sdf + vh.Name] = v.ID + ';;' + v.TypeID + ';;' + v.StatusOfAssetTimeSheetName + ';;' + v.Note;
                                else if (f.getTime() == t.getTime()) {
                                    if (ih <= lastindex)
                                        lstTemp[v.AssetID][sdf + vh.Name] = v.ID + ';;' + v.TypeID + ';;' + v.StatusOfAssetTimeSheetName + ';;' + v.Note;
                                }
                                else
                                    lstTemp[v.AssetID][sdf + vh.Name] = v.ID + ';;' + v.TypeID + ';;' + v.StatusOfAssetTimeSheetName + ';;' + v.Note;
                            });
                            f = Common.Date.Date(Common.Date.AddDay(f, 1));
                            if (Common.Date.Date(f).getTime() == Common.Date.Date(t).getTime())
                                f = t;
                            firstindex = -1;

                        }
                    }
                }
            });

            var griddata = [];
            $.each(lstTemp, function (i, v) {
                griddata.push(v);
            });
            me.grid.dataSource.data(griddata);
        }

        this.ChangeTime = function (options) {
            Common.Log('Timeline : ChangeTime');
            var me = this.options;

            if (me._mainData.length > 0) {
                var flag = false;
                if (!this.CompareDay(me.search.DateFrom, options.search.DateFrom)) {
                    flag = true;
                    me.search.DateFrom = options.search.DateFrom;
                }
                if (!this.CompareDay(me.search.DateTo, options.search.DateTo)) {
                    flag = true;
                    me.search.DateTo = options.search.DateTo;
                }
                if (!this.CompareHour(me.search.HourFrom, options.search.HourFrom)) {
                    flag = true;
                    me.search.HourFrom = options.search.HourFrom;
                }
                if (!this.CompareHour(me.search.HourTo, options.search.HourTo)) {
                    flag = true;
                    me.search.HourTo = options.search.HourTo;
                }
                if (flag) {
                    this.CreateRouteInDay();
                    this.CreateColumns();
                    //this.CreateGrid();
                    me.grid.setOptions({
                        columns: me.columns,
                    });

                    var dtFrom = Common.Date.Date(me.search.DateFrom);
                    var dtTo = Common.Date.AddDay(Common.Date.Date(me.search.DateTo), 1);
                    if (me.eventDetailData)
                        me.eventDetailData(dtFrom, dtTo);


                    Common._timelineGrid = me.grid;
                    var selector = Common._timelineGrid.element.attr('kendo-grid');
                    if (Common.HasValue(selector) && selector != '') {
                        $(Common._timelineGrid.element).find('.k-grid-content').scroll(function (e) {
                            var grid = $(this).closest('.cus-grid.k-grid');
                            var selector = $(grid).attr('kendo-grid');
                            if (Common._timelineBinding == false) {
                                //Common.Log('Timeline: set scroll' + e.target.scrollTop);
                                Common.Cookie.Set(selector + 'scrollTop', e.target.scrollTop);
                            }
                        });
                    }
                }
            }
        };

        this.RefreshMain = function () {
            Common.Log('Timeline : RefreshMain');
            var me = this.options;

            me._mainData = [];
            me.grid.dataSource.data([]);
            if (Common.HasValue(me.eventMainData))
                me.eventMainData();
        };

        this.RefreshDetail = function () {
            Common.Log('Timeline : RefreshDetail');
            var me = this.options;

            var dtFrom = Common.Date.Date(me.search.DateFrom);
            var dtTo = Common.Date.AddDay(Common.Date.Date(me.search.DateTo), 1);
            if (me.eventDetailData)
                me.eventDetailData(dtFrom, dtTo);
        };

        this.CompareDay = function (source, tartget) {
            return Common.Date.Date(source).getTime() == Common.Date.Date(tartget).getTime();
        };

        this.CompareHour = function (source, tartget) {
            var tf = new Date(2015, 1, 1, source.getHours(), source.getMinutes());
            var tt = new Date(2015, 1, 1, tartget.getHours(), tartget.getMinutes());
            return tf.getTime() == tt.getTime();
        };

        this.GetListRouteInDay = function () {
            Common.Log('Timeline : GetRouteInDay');
            var me = this.options;

            if (me.search.DateFrom.getTime() == me.search.DateTo.getTime())
                return [];
            else
                return me._listRouteInDay;
        };

        return this;
    },
    _timelineBinding: false,
    _timelineGrid: null,

    ClientHub: {
        Registry: function (methodname, callback) {
            for (var i = 0; i < 100; i++) {
                if (!Common.HasValue(this._events[methodname][i])) {
                    this._events[methodname][i] = callback;
                    break;
                }
            }
        },

        _events: { ditoMasterChangeStatus: [] },
    },

    Number: {
        Format: {
            Money: '',
            Number: ''
        },
        ToMoneyMillion: function (pValue) {
            if (typeof (pValue) == "string")
                pValue = parseFloat(pValue);
            kendo.culture('en-US');
            var result = kendo.toString(pValue / 1000000, "n0");
            return result;
        },
        ToMoney: function (pValue) {
            if (typeof (pValue) == "string")
                pValue = parseFloat(pValue);
            kendo.culture('en-US');
            var result = kendo.toString(pValue, "n0");
            return result;
        },
        ToNumber1: function (pValue) {
            kendo.culture('en-US');
            var result = kendo.toString(pValue, "n1");
            return result;
        },
        ToNumber2: function (pValue) {
            if (typeof (pValue) == "string")
                pValue = parseFloat(pValue);
            kendo.culture('en-US');
            var result = kendo.toString(pValue, "n2");
            return result;
        },
        ToNumber3: function (pValue) {
            kendo.culture('en-US');
            var result = kendo.toString(pValue, "n3");
            return result;
        },
        ToNumber5: function (pValue) {
            kendo.culture('en-US');
            var result = kendo.toString(pValue, "n5");
            return result;
        },
        ToNumber6: function (pValue) {
            kendo.culture('en-US');
            var result = kendo.toString(pValue, "n6");
            return result;
        },
        ToNumber9: function (pValue) {
            kendo.culture('en-US');
            var result = kendo.toString(pValue, "n9");
            return result;
        }
    },
    String: {
        Format: function (str, params) {
            /// <summary>
            /// Format
            /// </summary>
            /// <param name="str" type="string">str input. Example: a{0}, t{0}e{1}</param>
            /// <param name="params" type="object">List params. Example: param1, param2</param>

            var result = '';
            if (!arguments.length || arguments.length == 1)
                return str;
            var args = arguments;
            result = str;
            for (arg in args) {
                var index = parseInt(arg);
                if (index > 0) {
                    index -= 1;
                    result = result.replace(RegExp('\\{' + index + '\\}', 'gi'), args[arg]);
                }
            }
            return result;
        },
        FixString: function (str, max, charbreak, lastmax) {
            /// <summary>
            /// FixString
            /// </summary>
            /// <param name="str" type="string">str input. Example: a{0}, t{0}e{1}</param>
            /// <param name="max" type="number">Max in line</param>
            /// <param name="charbreak" type="string">char break line. Example: \n, br, ...</param>
            /// <param name="lastmax" type="string">Max in line</param>

            charbreak = typeof charbreak !== 'undefined' ? charbreak : '';
            lastmax = typeof lastmax !== 'undefined' ? lastmax : '';

            var result = '';
            if (charbreak == '') {
                if (str.length > max) result = str.substr(0, max) + lastmax;
                else result = str;
            }
            else {
                var strs = str.split(' ');
                var l = '';
                for (var i = 0; i < strs.length; i++) {
                    var s = strs[i];
                    l += s + ' ';
                    if (l.length > max) {
                        l = s + ' ';
                        result += charbreak + s + ' ';
                    }
                    else
                        result += s + ' ';
                }
            }
            return result;
        }
    },
    Request: {
        Create: function (options) {
            /// <summary>
            /// Create inline datasource
            /// </summary>
            /// <param name="options" type="object">Sorts, Filters</param>

            var sorts = [];
            var filters = [];
            if (Common.HasValue(options)) {
                if (Common.HasValue(options.Sorts))
                    sorts = options.Sorts;
                if (Common.HasValue(options.Filters))
                    filters = options.Filters;
            }
            var result = { sort: '', page: 0, pageSize: 0, group: '', filter: '' };
            $.each(sorts, function (index, value) {
                result.sort += value;
            });
            $.each(filters, function (index, value) {
                result.filter += value;
            });

            var indexand = result.filter.lastIndexOf('~and~');
            var indexor = result.filter.lastIndexOf('~or~');
            var indexandlast = result.filter.lastIndexOf('~and~)');
            var indexorlast = result.filter.lastIndexOf('~or~)');
            if (indexandlast >= 0) {
                result.filter = result.filter.substr(0, result.filter.length - 6) + ')';
            }
            else if (indexorlast >= 0) {
                result.filter = result.filter.substr(0, result.filter.length - 5) + ')';
            }
            else if (indexand > indexor) {
                result.filter = result.filter.substr(0, result.filter.length - 5);
            }
            else if (indexor > 0) {
                result.filter = result.filter.substr(0, result.filter.length - 4);
            }
            return result;
        },
        CreateFromGrid: function (dataSource) {
            /// <summary>
            /// Create inline datasource
            /// </summary>
            /// <param name="dataSource" type="object">dataSource</param>

            var result = { sort: '', page: 0, pageSize: 0, group: '', filter: '' };
            if (Common.HasValue(dataSource)) {
                var f = dataSource.filter();
                if (Common.HasValue(f)) {
                    var logic = f.logic;

                    for (var i = 0; i < f.filters.length; i++) {
                        var filter = f.filters[i];
                        if (!Common.HasValue(filter.logic)) {
                            result.filter += this.FilterParamWithAnd(filter.field, filter.operator, filter.value);
                        }
                    }

                    var indexand = result.filter.lastIndexOf('~and~');
                    var indexor = result.filter.lastIndexOf('~or~');
                    if (indexand > indexor) {
                        result.filter = result.filter.substr(0, result.filter.length - 5);
                    }
                    else if (indexor > 0) {
                        result.filter = result.filter.substr(0, result.filter.length - 4);
                    }
                }
            }

            return result;
        },
        Sort: function (member, sorttype) {
            return member + '-' + sorttype;
        },
        SortType: {
            ASC: 'asc',
            DESC: 'desc'
        },
        FilterParamWithAnd: function (member, condition, value) {
            var val = value;
            if (typeof (value) == 'string')
                val = "'" + value + "'";
            else if (Common.HasValue(value.getDay))
                val = "datetime'" + Common.Date.ToString(value, Common.Date.Format.YYMMDD) + 'T' + Common.Date.ToString(value, 'HH-mm-ss') + "'";
            return member + '~' + condition + '~' + val + '~and~';
        },
        FilterParamWithOr: function (member, condition, value) {
            var val = this._filtervalue(value);
            if (val != null) {
                if (typeof (value) == 'string')
                    val = "'" + value + "'";
                else if (Common.HasValue(value.getDay))
                    val = "'" + Common.Date.ToString(value, Common.Date.Format.YYMMDDHM) + "'";
                return member + '~' + condition + '~' + val + '~or~';
            }
            else
                return '';
        },
        FilterType: {
            Equal: 'eq',
            NotEqual: 'neq',
            GreaterThan: 'gt',
            GreaterThanOrEqual: 'gte',
            LessThan: 'lt',
            LessThanOrEqual: 'lte',
            Contains: 'contains'
        },
        _filtervalue: function (value) {
            if (Common.HasValue(value)) {
                var val = value;
                if (typeof (value) == 'string')
                    val = "'" + value + "'";
                else if (Common.HasValue(value.getDay))
                    val = "'" + Common.Date.ToString(value, Common.Date.Format.YYMMDDHM) + "'";
                return val;
            }
        }
    },
    Controls: {
        Textbox: {
            _valuefirst: [null, null, null],
            _sender: [null, null, null],
            _timer: [null, null, null],
            FocusEnd: function (selector) {
                $(selector).each(function () {
                    $(this).focus();
                    var val = $(this).val();
                    $(this).val('').val(val);
                });
            },
            IntegerChange: function (selector, callback) {
                $(selector).each(function () {
                    $(this).keydown(function (e) {
                        var index = -1;
                        if (Common.Controls.Textbox._sender[0] == null)
                            index = 0;
                        else if (Common.Controls.Textbox._sender[1] == null)
                            index = 1;
                        else if (Common.Controls.Textbox._sender[2] == null)
                            index = 2;
                        var val = $(this).val();
                        var sort = -1;
                        if (Common.HasValue(val) && val != '')
                            sort = parseInt(val);
                        Common.Controls.Textbox._valuefirst[index] = sort;
                        Common.Controls.Textbox._sender[index] = this;
                        if (Common.Controls.Textbox._timer[index] != null)
                            clearTimeout(Common.Controls.Textbox._timer[index]);
                        Common.Controls.Textbox._timer[index] = setTimeout(function () {
                            clearTimeout(Common.Controls.Textbox._timer[index]);
                            Common.Controls.Textbox._timer[index] = null;
                            var val = $(Common.Controls.Textbox._sender[index]).val();
                            var sort = -1;
                            if (Common.HasValue(val)) {
                                if (val != '') {
                                    if ($.isNumeric(val))
                                        sort = parseInt(val);
                                    else {
                                        sort = Common.Controls.Textbox._valuefirst[index];
                                        if (Common.Controls.Textbox._valuefirst[index] == -1)
                                            $(Common.Controls.Textbox._sender[index]).val('');
                                        else
                                            $(Common.Controls.Textbox._sender[index]).val(Common.Controls.Textbox._valuefirst[index]);
                                    }
                                }
                            }
                            callback(Common.Controls.Textbox._sender[index], sort);
                            Common.Controls.Textbox._sender[index] = null;
                        }, 1000);
                    });
                });
            }
        },
        Grid: {
            Timeline: function (selector, options) {
                $(selector).data('tl', this._timelinecach.length);
                var obj = new this._timeline(selector, options);
                this._timelinecach.push(obj);
                return obj;
            },
            _timeline: function (selector, options) {
                Common.Log('Timeline : Contructor');

                this.selector = selector;
                this.DateFrom = Common.Date.Date(Common.Date.AddDay(new Date(), -2));
                this.DateTo = Common.Date.Date(Common.Date.AddDay(new Date(), 2));
                this.HourFrom = new Date(2015, 1, 1, 6, 0);
                this.HourTo = new Date(2015, 1, 1, 18, 0);
                this.RouteInDay = 3;
                this.Model = null;
                this.ModelGroup = [];
                this.ModelSort = null;
                this.Columns = [];

                this.EventMainData = null;
                this.EventDetailData = null;
                this.EventSelect = null;
                this.EventClickTime = null;
                this.EventDrop = null;
                this.EventDropInTimeline = null;

                $.extend(this, options);

                this.IsDrag = false;
                this.ColumnsCount = 0;
                this.MainData = [];
                this.DetailData = [];
                this.ListRouteInDay = [];
                this.FieldDate = [];
                this._isInTimeline = false;

                this.StartDrag = function () {
                    Common.Controls.Grid._timelinecurrent = this;
                    var me = Common.Controls.Grid._timelinecurrent;

                    me._isInTimeline = false;
                };

                this.CreateRouteInDay = function () {
                    Common.Log('Timeline : CreateRouteInDay');
                    var me = Common.Controls.Grid._timelinecurrent;

                    var f = me.HourFrom;
                    var t = me.HourTo;
                    var h = (t.getTime() - f.getTime()) / 3600000;
                    me.ListRouteInDay = [];
                    if (h > 0) {
                        if (me.DateFrom.getTime() != me.DateTo.getTime()) {
                            var p = h / me.RouteInDay;
                            if (p % 1 > 0) {
                                if (p % 1 >= 0.5)
                                    p = p - (p % 1) + 0.5;
                                else
                                    p = p - (p % 1);
                            }
                            for (var i = 1; i <= me.RouteInDay; i++) {
                                me.ListRouteInDay.push({ 'Title': i + '', 'Name': i + '', 'HourFrom': f, 'HourTo': new Date(f.getTime() + (p * 3600000)) });
                                f = new Date(f.getTime() + (p * 3600000));
                            }
                        }
                        else {
                            var time = t.getTime() - f.getTime();
                            if ((time / (1 * 3600000)) % 1 > 0)
                                t = new Date(t.getTime() + (0.5 * 3600000));
                            while (f.getTime() <= t.getTime()) {
                                me.ListRouteInDay.push({ 'Title': Common.Date.ToString(f, 'HH:mm'), 'Name': Common.Date.ToString(f, 'HH_mm'), 'HourFrom': f, 'HourTo': new Date(f.getTime() + (1 * 3600000)) });
                                f = new Date(f.getTime() + (1 * 3600000));
                            }
                        }
                    }
                };

                this.CreateColumns = function () {
                    Common.Log('Timeline : CreateColumns');
                    var me = Common.Controls.Grid._timelinecurrent;

                    if (me.FieldDate.length > 0) {
                        me.Columns.splice(me.ColumnsCount, me.Columns.length - me.ColumnsCount);
                        me.FieldDate = [];
                    }

                    var f = me.DateFrom;
                    while (f <= me.DateTo) {
                        var sd = 'col' + f.getFullYear() + '' + f.getMonth() + '' + f.getDate();
                        var cols = [];
                        $.each(me.ListRouteInDay, function (index, route) {
                            var field = sd + route.Name;
                            me.FieldDate.push(field);
                            cols.push({
                                field: field, width: '45px', title: route.Title, attributes: {
                                    'class': 'no-padding tdtime',
                                    'date': Common.Date.ToString(f, Common.Date.Format.YYMMDD),
                                    'hourfrom': Common.Date.ToString(route.HourFrom, 'HH:mm'),
                                    'hourto': Common.Date.ToString(route.HourTo, 'HH:mm')
                                }
                            });
                        });
                        me.Columns.push({ title: Common.Date.ToString(f, Common.Date.Format.DDMMYY), columns: cols });
                        f = Common.Date.AddDay(f, 1);
                    }
                    me.Columns.push({ title: ' ', filterable: false, sortable: false });
                };

                this.CreateGrid = function () {
                    Common.Log('Timeline : CreateGrid');
                    var me = Common.Controls.Grid._timelinecurrent;

                    var grid = $(me.selector).data('kendoGrid');
                    if (Common.HasValue(grid)) {
                        grid.destroy();
                        $(me.selector).empty();
                    }

                    grid = $(me.selector).kendoGrid({
                        dataSource: Common.DataSource.CreateLocal([], me.Model, me.ModelGroup, null, null, me.ModelSort),
                        height: '99%', groupable: false, pageable: false, sortable: true, columnMenu: false, filterable: false, resizable: true,
                        selectable: 'row',
                        columns: me.Columns,
                        change: function (e) {
                            var me = Common.Controls.Grid._timelinecurrent;
                            var grid = $(me.selector).data('kendoGrid');

                            if (Common.HasValue(me.EventSelect) && !me.IsDrag) {
                                var sel = e.sender.select();
                                if (Common.HasValue(sel)) {
                                    if (sel.length > 0) {
                                        var lstid = [];
                                        var lst = [];
                                        $.each(sel, function (itr, tr) {
                                            var item = grid.dataItem(tr);
                                            if (lstid.indexOf(item.ID) < 0) {
                                                lstid.push(item.ID);
                                                lst.push(item);
                                            }
                                        });
                                        me.EventSelect(lst);
                                    }
                                }
                            }
                        },
                        dataBound: function (e) {
                            var me = Common.Controls.Grid._timelinecurrent;
                            var grid = $(me.selector).data('kendoGrid');

                            if (grid != null && Common.HasValue(grid.tbody)) {
                                var lst = grid.tbody.find('tr');
                                var groupcell = [];
                                $.each(lst, function (i, v) {
                                    var item = grid.dataItem(v);
                                    $(v).find('td.tdtime').each(function () {
                                        if ($(this).html() != '') {
                                            var strs = $(this).html().split(';;');
                                            var typeid = parseInt(strs[1]);
                                            var typeclass = 'black';

                                            switch (typeid) {
                                                case 1: typeclass = 'red'; break;
                                                case 2: typeclass = 'yellow'; break;
                                                case 3: typeclass = 'green'; break;
                                                case -1: typeclass = 'oceanblue'; break;
                                                case -2: typeclass = 'skyblue'; break;
                                                case -3: typeclass = 'lightcyan'; break;
                                                case -4: typeclass = 'seablue'; break;
                                                case -5: typeclass = 'royalblue'; break;
                                            }
                                            var note = (Common.HasValue(strs[3]) && strs[3] != '') ? strs[3] : '&nbsp;';
                                            $(this).html('<a targetid=' + strs[0] + ' typeid=' + typeid + ' href="#" title="' + strs[2] + '" class="timeline ' + typeclass + '">' + note + '</a>');
                                            if (!Common.HasValue(groupcell[strs[0]]))
                                                groupcell[strs[0]] = [];
                                            groupcell[strs[0]].push(this);
                                        }
                                        else {
                                            $(this).html('<div class="allowdrop" style="width:100%;">&nbsp;</div>');
                                        }
                                    });
                                });
                                $.each(groupcell, function (i, vg) {
                                    if (Common.HasValue(vg)) {
                                        if (vg.length > 1) {
                                            var td = vg[0];
                                            $(td).attr('colspan', vg.length);
                                            for (var i = 1; i < vg.length; i++) {
                                                td = vg[i];
                                                $(td).remove();
                                            }
                                        }
                                    }
                                });

                                $(grid.element).find('tbody > tr > td.no-padding > a.timeline').click(function (e) {
                                    e.preventDefault();
                                    var me = Common.Controls.Grid._timelinecurrent;
                                    var grid = $(me.selector).data('kendoGrid');

                                    var id = $(this).attr('targetid');
                                    var typeid = parseInt($(this).attr('typeid'));
                                    var tr = $(this).closest('tr');
                                    var item = grid.dataItem(tr);

                                    if (Common.HasValue(me.EventClickTime)) {
                                        me.EventClickTime(id, item, typeid);
                                    }
                                });

                                if (Common.HasValue(me.EventDrop)) {
                                    if (Common.HasValue(me.EventDropInTimeline)) {
                                        grid.table.kendoDraggable({
                                            filter: 'tbody > tr > td.no-padding > a.timeline',
                                            group: 'gridGroup',
                                            hint: function (e) {
                                                var me = Common.Controls.Grid._timelinecurrent;

                                                me._isInTimeline = true;
                                                return '<div><span class="fa fa-calendar" style="font-size:16px;"></span></div>';
                                            }
                                        });
                                    }

                                    $(grid.element).find('td > .allowdrop').kendoDropTarget({
                                        group: 'gridGroup',
                                        drop: function (e) {
                                            var me = Common.Controls.Grid._timelinecurrent;
                                            var grid = $(me.selector).data('kendoGrid');

                                            var tr = $(e.dropTarget).closest('tr');
                                            var item = grid.dataItem(tr);
                                            var td = $(e.dropTarget).closest('td');
                                            var tdtime = null;
                                            if ($(td).hasClass('tdtime')) {
                                                var strdate = $(td).attr('date'); var strhourfrom = $(td).attr('hourfrom'); var strhourto = $(td).attr('hourto');
                                                var idate = null; var ifrom = null; var ito = null;
                                                if (Common.HasValue(strdate)) {
                                                    idate = new Date(strdate);
                                                    var hour = parseInt(strhourfrom.substr(0, strhourfrom.indexOf(':')));
                                                    var min = parseInt(strhourfrom.substr(strhourfrom.indexOf(':') + 1, strhourfrom.length - strhourfrom.indexOf(':')));
                                                    ifrom = new Date(idate.getFullYear(), idate.getMonth(), idate.getDate(), hour, min);
                                                    hour = parseInt(strhourto.substr(0, strhourto.indexOf(':')));
                                                    min = parseInt(strhourto.substr(strhourto.indexOf(':') + 1, strhourto.length - strhourto.indexOf(':')));
                                                    ito = new Date(idate.getFullYear(), idate.getMonth(), idate.getDate(), hour, min);

                                                    tdtime = { 'Date': idate, 'HourFrom': ifrom, 'HourTo': ito };
                                                }
                                            }

                                            if (!me._isInTimeline)
                                                me.EventDrop(e, tdtime, item);
                                            else {
                                                me._isInTimeline = false;
                                                var id = $(e.draggable.currentTarget).attr('targetid');
                                                var typeid = parseInt($(e.draggable.currentTarget).attr('typeid'));

                                                var data = { id: id, typeid: typeid, source: grid.dataItem($(e.draggable.currentTarget).closest('tr')), target: item, tdtime: tdtime };
                                                me.EventDropInTimeline(e, data);
                                            }

                                            me.IsDrag = false;
                                        },
                                        dragenter: function (e) {
                                            var me = Common.Controls.Grid._timelinecurrent;
                                            var grid = $(me.selector).data('kendoGrid');

                                            me.IsDrag = true;
                                            var tr = $(e.dropTarget).closest('tr');
                                            grid.select(tr);
                                        },
                                        dragleave: function (e) {
                                            var me = Common.Controls.Grid._timelinecurrent;
                                            me.IsDrag = false;
                                        }
                                    });

                                }
                            }
                        }
                    }).data('kendoGrid');
                };

                this.SetMainData = function (data) {
                    Common.Log('Timeline : SetMainData');
                    Common.Controls.Grid._timelinecurrent = this;
                    var me = Common.Controls.Grid._timelinecurrent;

                    me.MainData = [];
                    $.each(data, function (i, v) {
                        me.MainData[v.ID] = v;
                    });

                    var dtFrom = Common.Date.Date(me.DateFrom);
                    var dtTo = Common.Date.AddDay(Common.Date.Date(me.DateTo), 1);

                    if (Common.HasValue(me.EventDetailData))
                        me.EventDetailData(dtFrom, dtTo);
                };

                this.SetDetailData = function (data) {
                    Common.Log('Timeline : SetDetailData');
                    Common.Controls.Grid._timelinecurrent = this;
                    var me = Common.Controls.Grid._timelinecurrent;
                    var grid = $(me.selector).data('kendoGrid');

                    var lstTemp = $.extend(true, {}, me.MainData);
                    $.each(data, function (i, v) {
                        var f = Common.Date.FromJson(v.DateFrom);
                        var t = Common.Date.FromJson(v.DateTo);
                        var lsthour = [];
                        var firstindex = -1;
                        var lastindex = -1;
                        $.each(me.ListRouteInDay, function (ih, vh) {
                            var hourf = new Date(2015, 1, 1, f.getHours(), f.getMinutes());
                            var hourt = new Date(2015, 1, 1, t.getHours(), t.getMinutes());
                            var hourif = new Date(2015, 1, 1, vh.HourFrom.getHours(), vh.HourFrom.getMinutes());
                            var hourit = new Date(2015, 1, 1, vh.HourTo.getHours(), vh.HourTo.getMinutes());

                            if (((hourif.getTime() <= hourf.getTime() && hourit.getTime() > hourf.getTime()) || firstindex >= 0) &&
                                hourif.getTime() <= hourt.getTime()) {
                                if (firstindex < 0)
                                    firstindex = ih;
                                lastindex = ih;
                                lsthour[vh.Name] = vh;
                            }
                            else if (Common.Date.Date(f).getTime() != Common.Date.Date(t).getTime() && hourif.getTime() <= hourt.getTime()) {
                                if (firstindex < 0)
                                    firstindex = ih;
                                lastindex = ih;
                                lsthour[vh.Name] = vh;
                            }
                        });

                        var sdf = 'col' + f.getFullYear() + '' + f.getMonth() + '' + f.getDate();
                        var sdt = 'col' + t.getFullYear() + '' + t.getMonth() + '' + t.getDate();

                        if (sdf == sdt) {
                            if (Common.HasValue(lstTemp[v.AssetID])) {
                                $.each(me.ListRouteInDay, function (ih, vh) {
                                    if (Common.HasValue(lsthour[vh.Name]))
                                        lstTemp[v.AssetID][sdf + vh.Name] = v.ID + ';;' + v.TypeID + ';;' + v.StatusOfAssetTimeSheetName + ';;' + v.Note;
                                });
                            }
                        }
                        else {
                            if (Common.HasValue(lstTemp[v.AssetID])) {
                                while (f.getTime() <= t.getTime()) {

                                    sdf = 'col' + f.getFullYear() + '' + f.getMonth() + '' + f.getDate();
                                    $.each(me.ListRouteInDay, function (ih, vh) {
                                        if (firstindex >= ih)
                                            lstTemp[v.AssetID][sdf + vh.Name] = v.ID + ';;' + v.TypeID + ';;' + v.StatusOfAssetTimeSheetName + ';;' + v.Note;
                                        else if (f.getTime() == t.getTime()) {
                                            if (ih <= lastindex)
                                                lstTemp[v.AssetID][sdf + vh.Name] = v.ID + ';;' + v.TypeID + ';;' + v.StatusOfAssetTimeSheetName + ';;' + v.Note;
                                        }
                                        else
                                            lstTemp[v.AssetID][sdf + vh.Name] = v.ID + ';;' + v.TypeID + ';;' + v.StatusOfAssetTimeSheetName + ';;' + v.Note;
                                    });
                                    f = Common.Date.Date(Common.Date.AddDay(f, 1));
                                    if (Common.Date.Date(f).getTime() == Common.Date.Date(t).getTime())
                                        f = t;
                                    firstindex = -1;

                                }
                            }
                        }
                    });

                    var griddata = [];
                    $.each(lstTemp, function (i, v) {
                        griddata.push(v);
                    });

                    grid.dataSource.data(griddata);

                    //grid.dataSource.sync();
                };

                this.Init = function () {
                    Common.Log('Timeline : Init');
                    Common.Controls.Grid._timelinecurrent = this;
                    var me = Common.Controls.Grid._timelinecurrent;

                    me.ColumnsCount = me.Columns.length;
                    me.CreateRouteInDay();
                    me.CreateColumns();
                    me.CreateGrid();

                    if (Common.HasValue(me.EventMainData))
                        me.EventMainData();
                };

                this.ChangeTime = function (options) {
                    Common.Log('Timeline : ChangeTime');
                    Common.Controls.Grid._timelinecurrent = this;
                    var me = Common.Controls.Grid._timelinecurrent;

                    if (me.MainData.length > 0) {
                        var flag = false;
                        if (!me.CompareDay(me.DateFrom, options.DateFrom)) {
                            flag = true;
                            me.DateFrom = options.DateFrom;
                        }
                        if (!me.CompareDay(me.DateTo, options.DateTo)) {
                            flag = true;
                            me.DateTo = options.DateTo;
                        }
                        if (!me.CompareHour(me.HourFrom, options.HourFrom)) {
                            flag = true;
                            me.HourFrom = options.HourFrom;
                        }
                        if (!me.CompareHour(me.HourTo, options.HourTo)) {
                            flag = true;
                            me.HourTo = options.HourTo;
                        }

                        if (flag) {
                            me.CreateRouteInDay();
                            me.CreateColumns();
                            me.CreateGrid();

                            var dtFrom = Common.Date.Date(me.DateFrom);
                            var dtTo = Common.Date.AddDay(Common.Date.Date(me.DateTo), 1);
                            if (me.EventDetailData)
                                me.EventDetailData(dtFrom, dtTo);
                        }
                    }
                };

                this.RefreshMain = function () {
                    Common.Log('Timeline : RefreshMain');
                    Common.Controls.Grid._timelinecurrent = this;
                    var me = Common.Controls.Grid._timelinecurrent;
                    var grid = $(me.selector).data('kendoGrid');

                    me.MainData = [];
                    grid.dataSource.data([]);
                    if (Common.HasValue(me.EventMainData))
                        me.EventMainData();
                };

                this.RefreshDetail = function () {
                    Common.Log('Timeline : RefreshDetail');
                    Common.Controls.Grid._timelinecurrent = this;
                    var me = Common.Controls.Grid._timelinecurrent;

                    var dtFrom = Common.Date.Date(me.DateFrom);
                    var dtTo = Common.Date.AddDay(Common.Date.Date(me.DateTo), 1);
                    if (me.EventDetailData)
                        me.EventDetailData(dtFrom, dtTo);
                };

                this.CompareDay = function (source, tartget) {
                    return Common.Date.Date(source).getTime() == Common.Date.Date(tartget).getTime();
                };

                this.CompareHour = function (source, tartget) {
                    var tf = new Date(2015, 1, 1, source.getHours(), source.getMinutes());
                    var tt = new Date(2015, 1, 1, tartget.getHours(), tartget.getMinutes());
                    return tf.getTime() == tt.getTime();
                };

                this.GetListRouteInDay = function () {
                    Common.Log('Timeline : GetRouteInDay');
                    Common.Controls.Grid._timelinecurrent = this;
                    var me = Common.Controls.Grid._timelinecurrent;

                    if (me.DateFrom.getTime() == me.DateTo.getTime())
                        return [];
                    else
                        return me.ListRouteInDay;
                };
            },
            _timelinecurrent: null,
            _timelinecach: [],
            MergeRow: function (grid, options) {
                var opt = { 'Columns': null, 'Compare': null };
                $.extend(opt, options);

                var lvlitem = null;
                var lvlColStr = '';
                if (Common.HasValue(opt.Columns) && opt.Columns.length > 0) {
                    Common.Data.Each(opt.Columns, function (o) {
                        lvlColStr += '[' + o + ']';
                    });
                }
                if (Common.HasValue(grid) && lvlColStr.length > 0) {
                    $(grid.element).find('.k-grid-content > table').each(function (index, item) {
                        var cols = [];
                        var dimension_col = 1;
                        $(grid.element).find('.k-grid-header > .k-grid-header-wrap > table th').each(function () {
                            var name = $(this).attr('data-field');
                            if (!Common.HasValue(name)) name = $(this).attr('colname');
                            if (Common.HasValue(name)) {
                                cols.push({ index: dimension_col, name: name });
                                dimension_col++;
                            }
                        });

                        var lstmerge = [];
                        var lstrow = [];
                        $(item).find('tr[role="row"]').each(function () {
                            var dataitem = grid.dataItem(this);

                            if (lvlitem == null || opt.Compare(lvlitem, dataitem)) {
                                lstmerge.push(this);
                                lvlitem = dataitem;
                            }
                            else {
                                if (lstmerge.length > 1) {
                                    var lsttd = [];
                                    for (var i = 0; i < lstmerge.length; i++) {
                                        var tr = lstmerge[i];
                                        if (i == 0) {
                                            for (var j = 0; j < cols.length; j++) {
                                                var col = cols[j];

                                                if (Common.HasValue(col.name) && lvlColStr.indexOf(col.name) >= 0) {
                                                    var dimension_td = $(tr).find('td:nth-child(' + col.index + ')');
                                                    lsttd.push(dimension_td);
                                                }
                                            }
                                        }
                                        else {
                                            for (var j = 0; j < cols.length; j++) {
                                                var col = cols[j];

                                                if (Common.HasValue(col.name) && lvlColStr.indexOf(col.name) >= 0) {
                                                    var dimension_td = $(tr).find('td:nth-child(' + col.index + ')');
                                                    dimension_td.remove();
                                                }
                                            }
                                        }
                                    }
                                    $.each(lsttd, function (itd, td) {
                                        td.attr('rowspan', lstmerge.length);
                                    });
                                }

                                lstmerge = [];
                                lsttd = [];
                                lstmerge.push(this);
                                lvlitem = dataitem;
                            }
                        });

                        if (lstmerge.length > 1) {
                            var lsttd = [];
                            for (var i = 0; i < lstmerge.length; i++) {
                                var tr = lstmerge[i];
                                if (i == 0) {
                                    for (var j = 0; j < cols.length; j++) {
                                        var col = cols[j];

                                        if (Common.HasValue(col.name) && lvlColStr.indexOf(col.name) >= 0) {
                                            var dimension_td = $(tr).find('td:nth-child(' + col.index + ')');
                                            lsttd.push(dimension_td);
                                        }
                                    }
                                }
                                else {
                                    for (var j = 0; j < cols.length; j++) {
                                        var col = cols[j];

                                        if (Common.HasValue(col.name) && lvlColStr.indexOf(col.name) >= 0) {
                                            var dimension_td = $(tr).find('td:nth-child(' + col.index + ')');
                                            dimension_td.remove();
                                        }
                                    }
                                }
                            }
                            $.each(lsttd, function (itd, td) {
                                td.attr('rowspan', lstmerge.length);
                            });
                        }
                    });

                    var isalt = false;
                    var lst = $(grid.element).find('.k-grid-content > table tr[role="row"]');
                    for (var i = 0; i < lst.length; i++) {
                        var tr = lst[i];
                        var rowspan = $($(tr).children('td')[0]).attr('rowspan');
                        if (Common.HasValue(rowspan)) {
                            var irow = parseInt(rowspan);
                            if (irow > 1) {
                                for (var j = i; j < i + irow; j++) {
                                    tr = lst[j];
                                    if ($(tr).hasClass('k-alt')) $(tr).removeClass('k-alt');
                                    if (isalt) $(tr).addClass('k-alt');
                                }
                                isalt = !isalt;
                                i += (irow - 1);
                                continue;
                            }
                        }

                        if ($(tr).hasClass('k-alt')) $(tr).removeClass('k-alt');
                        if (isalt) $(tr).addClass('k-alt');
                        isalt = !isalt;
                    }
                    $(grid.element).find('.k-grid-content > table tr[role="row"]').each(function (index, tr) {
                        var rowspan = $($(this).children('td')[0]).attr('rowspan');
                        var flag = true;
                        if (Common.HasValue(rowspan)) {
                            var irow = parseInt(rowspan);
                            if (irow > 1) {
                                flag = false;
                            }
                        }
                    });
                }
            },
            ReorderColumns: function (options) {
                /// <summary>
                /// ReorderColumns
                /// </summary>
                /// <param name="options" type="object">Grid | CookieName</param>
                /// <returns type="null"></returns>
                var opt = { 'Grid': null, 'CookieName': null };
                $.extend(opt, options);

                if (opt.Grid != null && opt.CookieName !== null) {
                    $(opt.Grid.element).data('CookieName', opt.CookieName);

                    var strCookie = Common.Cookie.Get(opt.CookieName);
                    if (Common.HasValue(strCookie) && strCookie != '') {
                        try {
                            var columns = eval('[' + strCookie + ']')[0];
                            var recolumns = [];
                            var colField = [];
                            $.each(columns, function (i, v) {
                                var strs = v.split('|');
                                v = { 'field': strs[0], 'index': parseInt(strs[1]), 'hidden': strs[2] == '1', 'width': parseInt(strs[3]) };
                                recolumns.push(v);
                                colField.push(v.field);
                                if (v.hidden == true)
                                    opt.Grid.hideColumn(v.field);
                            });
                            var colOrder = [];
                            $.each(opt.Grid.columns, function (i, col) {
                                if (Common.HasValue(col.field)) {
                                    var index = colField.indexOf(col.field);
                                    if (index >= 0) {
                                        if (opt.Grid._group)
                                            i++;
                                        $(opt.Grid.element).find('.k-grid-header-wrap colgroup col').eq(i).width(recolumns[index].width);
                                        $(opt.Grid.element).find('.k-grid-content colgroup col').eq(i).width(recolumns[index].width);
                                        colOrder[recolumns[index].index] = col;
                                    }
                                }
                            });
                            for (var i = 0; i < colOrder.length; i++) {
                                var v = colOrder[i];
                                if (Common.HasValue(v)) {
                                    opt.Grid.reorderColumn(i, v);
                                }
                            }


                        } catch (e) {
                            Common.Cookie.Set(cookiename, JSON.stringify([]));
                        }
                    }

                    opt.Grid.bind("columnReorder", function (e) {
                        Common.Controls.Grid._reorderColumnsGrid = e.sender;
                        setTimeout(function () { Common.Controls.Grid._reorderColumnsRefresh(); }, 10);

                    });
                    opt.Grid.bind("columnShow", function (e) {
                        Common.Controls.Grid._reorderColumnsGrid = e.sender;
                        setTimeout(function () { Common.Controls.Grid._reorderColumnsRefresh(); }, 10);
                    });
                    opt.Grid.bind("columnHide", function (e) {
                        Common.Controls.Grid._reorderColumnsGrid = e.sender;
                        setTimeout(function () { Common.Controls.Grid._reorderColumnsRefresh(); }, 10);
                    });
                }
            },
            _reorderColumnsRefresh: function () {
                if (Common.HasValue(this._reorderColumnsGrid)) {
                    var cookiename = $(this._reorderColumnsGrid.element).data('CookieName');
                    var columns = [];
                    $.each(this._reorderColumnsGrid.columns, function (i, v) {
                        if (Common.HasValue(v.field)) {
                            var hideid = v.hidden == true ? 1 : 0;
                            var width = v.width + '';
                            if (width.indexOf('px') >= 0) width = width.substr(0, width.length - 2);
                            columns.push(v.field + '|' + i + '|' + hideid + '|' + width);
                        }
                    });
                    Common.Cookie.Set(cookiename, JSON.stringify(columns));
                }
            },
            _reorderColumnsGrid: null,
            ToHTML: function (obj) {
                return obj;
            },
            PageSizes: { pageSizes: [20, 50, 100] },
            ButtonViewColumn: function (selector, grid) {
                if (selector != null && grid != null) {
                    var winid = kendo.guid();
                    var divid = kendo.guid();
                    var top = $(selector).offset().top + $(selector).height() + 10;
                    var left = $(selector).offset().left + $(selector).width() - 490;
                    $('<div id="' + winid + '" class="cus-window" style="display:none"><div id="' + divid + '" class="cus-listview"></div></div>').appendTo($('body'));
                    var win = $('#' + winid).kendoWindow({
                        title: 'Các cột hiển thị',
                        width: '500px', height: '300px', position: { top: top, left: left },
                        draggable: false, modal: true, resizable: false,
                        actions: ['Close']
                    }).data('kendoWindow');
                    $('#' + winid).keydown(function (e) {
                        if (e.keyCode === 27) {
                            e.preventDefault();

                            //debugger;
                            $(this).closest('.k-window-content').data('kendoWindow').close();
                        }
                    });
                    $(selector).attr('winid', winid);
                    $(selector).attr('divid', divid);

                    $(selector).click(function (e) {
                        e.preventDefault();

                        var winid = $(this).attr('winid');
                        var divid = $(this).attr('divid');
                        var win = Common.Controls.Grid._buttonViewColumnGetWin('#' + winid, '#' + divid, grid);
                        win.open();
                    });
                }
            },
            _buttonViewColumnGetWin: function (winid, divid, grid) {
                var div = $(divid).data('kendoListView');
                if (div == null) {
                    var gridid = '#' + $(grid.element).attr('id');
                    var data = [];
                    $.each(grid.columns, function (index, value) {
                        if (value.title.trim() != '') {
                            var col = { IsChoose: true, Field: value.field, Title: value.title, DivID: divid, GridID: gridid };
                            if (value.hidden == true)
                                col.IsChoose = false;
                            data.push(col);
                        }
                    });
                    $(divid).kendoListView({
                        dataSource: data,
                        template: kendo.template($("#ViewColumnTemplate").html()),
                        dataBound: function (e) {
                            $(e.sender.element).find('input[type="checkbox"]').change(function () {
                                var divid = $(this).attr('divid');
                                var gridid = $(this).attr('gridid');
                                var div = $(divid).data('kendoListView');
                                var grid = $(gridid).data('kendoGrid');

                                var dataitem = div.dataItem($(this).closest('.viewcolumn'));
                                dataitem.IsChoose = $(this).prop('checked');
                                if (!dataitem.IsChoose)
                                    grid.hideColumn(dataitem.Field);
                                else
                                    grid.showColumn(dataitem.Field);
                            });
                        }
                    });
                }
                return $(winid).data('kendoWindow');
            }
        }
    },
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
    Report: {
        ServiceUrl: 'api/reports/',
        TemplateUrl: 'Scripts/ReportViewer/templates/telerikReportViewerTemplate-9.0.15.225.html',
        ViewMode: {
            PRINT_PREVIEW: 'PRINT_PREVIEW',
            INTERACTIVE: 'INTERACTIVE'
        },
        ScaleMode: {
            SPECIFIC: 'SPECIFIC',
            FIT_PAGE: 'FIT_PAGE',
            FIT_PAGE_WIDTH: 'FIT_PAGE_WIDTH'
        },
        PrintMode: {
            AUTO_SELECT: 'AUTO_SELECT'
        },
        ReportSource: function (report, paramread) {
            return { "report": "ClientReport." + report + ", ClientReport, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "parameters": paramread };
        }
    },
    PageSize: {
        pageSizes: [20, 50, 100, "all"], buttonCount: 3,
        messages: { display: "{0}-{1}/{2} dòng", empty: "DL trống", itemsPerPage: "dòng mỗi trang" }
    },
    FilterDate: { cell: { template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); }, operator: 'equal', showOperators: false } },
    JSON: {
        QuoteReplacer: function (key, value) {
            if (typeof (value) === "string") {
                value = value.replace(/["']/g, " ");
            }
            return value;
        }
    }
};

var RS = {};

Date.prototype.addDays = function (days) {
    var dat = new Date(this.valueOf() + days * 24 * 60 * 60 * 1000);
    return dat;
}

String.prototype.isValidCode = function () {
    if (this.indexOf(" ") > -1) {
        return false;
    }
    for (var i = 0; i < this.length; i++) {
        if (this.charCodeAt(i) > 127 || this.charCodeAt(i) < 32) {
            return false;
        }
    }
    return true;
}