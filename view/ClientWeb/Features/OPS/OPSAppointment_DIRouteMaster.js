/// <reference path="~/Scripts/jquery-1.9.1.intellisense.js" />
/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />

//#region OPS object
var _OPSAppointment_DIRouteMaster = {
    URL: {
        OrderListDN: 'OPS_DIAppointment_Route_HasDNOrderListDN',
        OrderListByGroupID: 'OPS_DIAppointment_Route_HasDNOrderListByGroupID',
        ListGroupID: 'OPS_DIAppointment_Route_HasDNListGroupID',
        VehicleListVendor: 'OPS_DIAppointment_Route_VehicleListVendor',
        VehicleListVehicle: 'OPS_DIAppointment_Route_VehicleListVehicle',
        Delete: 'OPS_DIAppointment_Route_HasDNDelete',
        Approved: 'OPS_DIAppointment_Route_HasDNApproved',
        UnApproved: 'OPS_DIAppointment_Route_HasDNUnApproved',
        Save: 'OPS_DIAppointment_Route_NoDNSave',
        List: 'OPS_DIAppointment_Route_NoDNList',
        OrderList: 'OPS_DIAppointment_Route_NoDNOrderList',
        CustomerList: 'OPS_DIAppointment_Route_CustomerList',
        VehicleListDriver: 'OPS_DIAppointment_Route_VehicleListDriver',
        VehicleAddRate: 'OPS_DIAppointment_Route_VehicleAddRate',
        VehicleTOVEN: 'OPS_DIAppointment_Route_VehicleTOVEN',
        VehicleTOVENGet: 'OPS_DIAppointment_Route_VehicleTOVENGet',
        VehicleList: 'OPS_DIAppointment_Route_VehicleList',
        VehicleTimeList: 'OPS_DIAppointment_Route_VehicleTimeList',
        VehicleTOVENList: 'OPS_DIAppointment_Route_VehicleTOVENList',
        Tender: 'OPS_DIAppointment_Route_SendToTender',
        Reject: 'OPS_DIAppointment_Route_TenderReject',
        Accept: 'OPS_DIAppointment_Route_TenderApproved',
        SendMailToTender: 'OPS_DIAppointment_Route_SendMailToTender',
        FLMPlaning: 'OPS_DIAppointment_FLMPlaning',
    },
    Data: {
        CookieSearch: 'OPS_Appointment_DIRouteMaster_Search',
        CookieVehicle: 'OPS_Appointment_DIRouteMaster_Vehicle',
        _dataDistrict: [],
        _createListDN: [],
        _createListID: [],
        _dataHasDNGroupProduct: [],
        _dataHasDN: [],
        _dataHasDNSort: [],
        _dataHasDNVendor: [],
        _dataHasDNVehicle: [],
        _dataVehicleVEN: [],
        _dataHasDNVehicleVEN: [],
        _dataHasDNVehicleHome: [],
        _dataDriver: [],
        FLMPlaning: [],
    },
    Timeline: null
};
//endregion OPS object
angular.module('myapp').controller('OPSAppointment_DIRouteMasterCtrl', ['$rootScope', '$scope', '$http', '$location', '$state', '$timeout', function ($rootScope, $scope, $http, $location, $state, $timeout) {
    Common.Log('OPSAppointment_DIRouteMasterCtrl');
    $rootScope.IsLoading = false;
    $scope.DNCode = "";
    $scope.IsShowTxtDN = true;
    $scope.IsShowCombine = false;
    $scope.IsHasDNMonitor = false;
    $scope.IsCreateByID = true;
    $scope.IsExpand = true;
    $scope.IsShowVehicleVendor = false;
    $scope.Search = {};
    $scope.Vehicle = {};
    $scope.RateTime = 2;
    $scope.Summary = '';
    $scope.mltCustomerOptions = {
        autoBind: true,
        valuePrimitive: true, placeholder: 'Chọn khách hàng',
        dataTextField: 'CustomerName',
        dataValueField: 'ID',
        filter: 'contains',
        suggest: true,
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    CustomerName: { type: 'string' }
                }
            }
        }),
        change: function (e) {
            $scope.Search.ListCustomerID = this.value();
        }
    };

    $scope.cboVendorOptions = {
        index: 0, autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true,
        dataTextField: 'CustomerName', dataValueField: 'ID',
    };

    $scope.cboVehicleOptions = {
        index: 0, autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true,
        dataTextField: 'RegNo', dataValueField: 'RegNo',
    };

    $scope.numericSortOrderOptions = {
        format: '#', spinners: false, culture: 'en-US', step: 1, decimals: 0, min: 0,
        change: function (e) {
            var firstVendorID = _OPSAppointment_DIRouteMaster.Data._dataHasDNVendor.length > 0 ? _OPSAppointment_DIRouteMaster.Data._dataHasDNVendor[0].ID : -1;
            var firstVehicleCode = '';
            var firstVendorName = _OPSAppointment_DIRouteMaster.Data._dataHasDNVendor.length > 0 ? _OPSAppointment_DIRouteMaster.Data._dataHasDNVendor[0].CustomerName : '';
            if (firstVendorID == -1) {
                firstVehicleCode = _OPSAppointment_DIRouteMaster.Data._dataHasDNVehicleHome[0].RegNo;
            } else {
                firstVehicleCode = _OPSAppointment_DIRouteMaster.Data._dataHasDNVehicleVEN[firstVendorID].RegNo;
            }
            var tr = $(e.sender.element).closest('tr');
            var dataItem = $scope.gridNoDN.dataItem(tr);
            var sort = e.sender.element.val() == '' ? 0 : parseInt(e.sender.element.val());
            if (!Common.HasValue(_OPSAppointment_DIRouteMaster.Data._dataHasDNSort[sort]))
                _OPSAppointment_DIRouteMaster.Data._dataHasDNSort[sort] = { 'TOMasterID': -1, 'CreateSortOrder': sort, 'CreateVendorID': firstVendorID, 'CreateVendorName': firstVendorName, 'CreateVehicleCode': firstVehicleCode, 'CreateDriverName': '', 'CreateTelephone': '', 'CreateDateTime': new Date(), 'Kg': 0, 'IsChange': true, 'TypeID': 1 };
            if (sort == 0 || _OPSAppointment_DIRouteMaster.Data._dataHasDNSort[sort].TypeID < 2) {
                dataItem.CreateSortOrder = sort;
            }
            _OPSAppointment_DIRouteMaster.Data._dataHasDN = $scope.gridNoDNOptions.dataSource.data();
            $scope.ReloadSort();
            $scope.gridNoDNOptions.dataSource.data(_OPSAppointment_DIRouteMaster.Data._dataHasDN);
        }
    };

    // Format cho textbox Kg
    $scope.numericKgOptions = {
        min: 0,
        spinners: false,
        decimals: Common.Number.DI_Decimals,
        culture: "en-US",
        format: "n3",
        change: function (e) {
            var tr = $(e.sender.element).closest('tr');
            var dataItem = $scope.gridNoDN.dataItem(tr);
            var sub = dataItem.Kg - this.value();
            if (sub > 0.001) {
                var itemsub = $.extend(true, {}, dataItem);
                itemsub.Kg = sub;
                itemsub.Ton = sub / 1000;
                itemsub.IsOrigin = false;
                dataItem.Kg = this.value();
                dataItem.Ton = dataItem.Kg / 1000;
                dataItem.CBM = 0;
                dataItem.Quantity = 0;
                if (dataItem.ExchangeTon > 0) {
                    itemsub.CBM = dataItem.ExchangeCBM * itemsub.Ton / dataItem.ExchangeTon;
                    itemsub.Quantity = dataItem.ExchangeQuantity * itemsub.Ton / dataItem.ExchangeTon;
                    dataItem.CBM = dataItem.ExchangeCBM * dataItem.Ton / dataItem.ExchangeTon;
                    dataItem.Quantity = dataItem.ExchangeQuantity * dataItem.Ton / dataItem.ExchangeTon;
                }
                _OPSAppointment_DIRouteMaster.Data._dataHasDN = $scope.gridNoDN.dataSource.data();
                _OPSAppointment_DIRouteMaster.Data._dataHasDN.push(itemsub);
                $scope.ReloadSort();
                $scope.gridNoDN.dataSource.data(_OPSAppointment_DIRouteMaster.Data._dataHasDN);
            }
        }
    };

    // Format cho textbox Tấn
    $scope.numericTonOptions = {
        min: 0,
        spinners: false,
        decimals: Common.Number.DI_Decimals,
        culture: "en-US",
        format: "n5",
        change: function (e) {
            var tr = $(e.sender.element).closest('tr');
            var dataItem = $scope.gridNoDN.dataItem(tr);
            var sub = dataItem.Ton - this.value();
            if (sub > 0.001) {
                var itemsub = $.extend(true, {}, dataItem);
                itemsub.Ton = sub;
                itemsub.Kg = sub * 1000;
                itemsub.IsOrigin = false;
                dataItem.Ton = this.value();
                dataItem.Kg = dataItem.Ton * 1000;
                dataItem.CBM = 0;
                dataItem.Quantity = 0;
                if (dataItem.ExchangeTon > 0) {
                    itemsub.CBM = dataItem.ExchangeCBM * itemsub.Ton / dataItem.ExchangeTon;
                    itemsub.Quantity = dataItem.ExchangeQuantity * itemsub.Ton / dataItem.ExchangeTon;
                    dataItem.CBM = dataItem.ExchangeCBM * dataItem.Ton / dataItem.ExchangeTon;
                    dataItem.Quantity = dataItem.ExchangeQuantity * dataItem.Ton / dataItem.ExchangeTon;
                }
                _OPSAppointment_DIRouteMaster.Data._dataHasDN = $scope.gridNoDN.dataSource.data();
                _OPSAppointment_DIRouteMaster.Data._dataHasDN.push(itemsub);
                $scope.ReloadSort();
                $scope.gridNoDN.dataSource.data(_OPSAppointment_DIRouteMaster.Data._dataHasDN);
            }
        }
    };

    // Format cho textbox CBM
    $scope.numericCBMOptions = {
        min: 0,
        spinners: false,
        decimals: Common.Number.DI_Decimals,
        culture: "en-US",
        format: "n3",
        change: function (e) {
            var tr = $(e.sender.element).closest('tr');
            var dataItem = $scope.gridNoDN.dataItem(tr);
            var sub = dataItem.CBM - this.value();
            if (sub > 0.001) {
                var itemsub = $.extend(true, {}, dataItem);
                itemsub.CBM = sub;
                itemsub.IsOrigin = false;
                dataItem.CBM = this.value();
                dataItem.Ton = 0;
                dataItem.Kg = 0;
                dataItem.Quantity = 0;
                if (dataItem.ExchangeCBM > 0) {
                    itemsub.Ton = dataItem.ExchangeTon * itemsub.CBM / dataItem.ExchangeCBM;
                    itemsub.Kg = itemsub.Ton * 1000;
                    itemsub.Quantity = dataItem.ExchangeQuantity * itemsub.CBM / dataItem.ExchangeCBM;
                    dataItem.Ton = dataItem.ExchangeTon * dataItem.CBM / dataItem.ExchangeCBM;
                    dataItem.Kg = dataItem.Ton * 1000;
                    dataItem.Quantity = dataItem.ExchangeQuantity * dataItem.CBM / dataItem.ExchangeCBM;
                }
                _OPSAppointment_DIRouteMaster.Data._dataHasDN = $scope.gridNoDN.dataSource.data();
                _OPSAppointment_DIRouteMaster.Data._dataHasDN.push(itemsub);
                $scope.ReloadSort();
                $scope.gridNoDN.dataSource.data(_OPSAppointment_DIRouteMaster.Data._dataHasDN);
            }
        }
    };

    // Format cho textbox Quantity
    $scope.numericQuantityOptions = {
        min: 0,
        spinners: false,
        decimals: Common.Number.DI_Decimals,
        culture: "en-US",
        format: "n3",
        change: function (e) {
            var tr = $(e.sender.element).closest('tr');
            var dataItem = $scope.gridNoDN.dataItem(tr);
            var sub = dataItem.Quantity - this.value();
            if (sub > 0.001) {
                var itemsub = $.extend(true, {}, dataItem);
                itemsub.Quantity = sub;
                itemsub.IsOrigin = false;
                dataItem.Quantity = this.value();
                dataItem.Ton = 0;
                dataItem.Kg = 0;
                dataItem.CBM = 0;
                if (dataItem.ExchangeQuantity > 0) {
                    itemsub.Ton = dataItem.ExchangeTon * itemsub.Quantity / dataItem.ExchangeQuantity;
                    itemsub.Kg = itemsub.Ton * 1000;
                    itemsub.CBM = dataItem.ExchangeCBM * itemsub.Quantity / dataItem.ExchangeQuantity;
                    dataItem.Ton = dataItem.ExchangeTon * dataItem.Quantity / dataItem.ExchangeQuantity;
                    dataItem.Kg = dataItem.Ton * 1000;
                    dataItem.CBM = dataItem.ExchangeCBM * dataItem.Quantity / dataItem.ExchangeQuantity;
                }
                _OPSAppointment_DIRouteMaster.Data._dataHasDN = $scope.gridNoDN.dataSource.data();
                _OPSAppointment_DIRouteMaster.Data._dataHasDN.push(itemsub);
                $scope.ReloadSort();
                $scope.gridNoDN.dataSource.data(_OPSAppointment_DIRouteMaster.Data._dataHasDN);
            }
        }
    };

    // Load danh sách Vendor
    Common.Services.Call($http, {
        url: Common.Services.url.OPS,
        method: _OPSAppointment_DIRouteMaster.URL.VehicleListVendor,
        data: {},
        success: function (res) {
            if (Common.HasValue(res)) {
                $timeout(function () {
                    _OPSAppointment_DIRouteMaster.Data._dataHasDNVendor = res;
                    if (_OPSAppointment_DIRouteMaster.Data._dataHasDNVendor.length > 0 && _OPSAppointment_DIRouteMaster.Data._dataHasDNVehicle.length > 0)
                        $scope.LoadDataVehicleVendor();
                }, 1);
            }
        }
    });

    // Load danh sách xe
    Common.Services.Call($http, {
        url: Common.Services.url.OPS,
        method: _OPSAppointment_DIRouteMaster.URL.VehicleListVehicle,
        data: {},
        success: function (res) {
            if (Common.HasValue(res)) {
                $timeout(function () {
                    _OPSAppointment_DIRouteMaster.Data._dataHasDNVehicle = res;
                    _OPSAppointment_DIRouteMaster.Data._dataVehicleVEN = res;
                    if (_OPSAppointment_DIRouteMaster.Data._dataHasDNVendor.length > 0 && _OPSAppointment_DIRouteMaster.Data._dataHasDNVehicle.length > 0)
                        $scope.LoadDataVehicleVendor();
                }, 1);
            }
        }
    });

    // Load danh sách khách hàng
    Common.Services.Call($http, {
        url: Common.Services.url.OPS,
        method: _OPSAppointment_DIRouteMaster.URL.CustomerList,
        data: {},
        success: function (res) {
            if (Common.HasValue(res)) {
                $timeout(function () {
                    $scope.mltCustomerOptions.dataSource.data(res);
                    $scope.Init_LoadCookie();
                }, 1);
            }
        }
    });

    // Load danh sách tài xế
    Common.Services.Call($http, {
        url: Common.Services.url.OPS,
        method: _OPSAppointment_DIRouteMaster.URL.VehicleListDriver,
        success: function (res) {
            Common.Services.Error(res, function (res) {
                _OPSAppointment_DIRouteMaster.Data._dataDriver = [];
                $.each(res, function (i, v) {
                    _OPSAppointment_DIRouteMaster.Data._dataDriver.push({ 'ID': v.ID, 'Text': v.LastName + ' ' + v.FirstName + ' (' + v.EmployeeCode + ')', 'DriverName': v.LastName + ' ' + v.FirstName, 'DriverTelNo': v.Cellphone });
                });
            });
        }
    });

    // Load danh sách phân tài
    Common.Services.Call($http, {
        url: Common.Services.url.OPS,
        method: _OPSAppointment_DIRouteMaster.URL.FLMPlaning,
        success: function (res) {
            _OPSAppointment_DIRouteMaster.Data.FLMPlaning = res;
        }
    });

    $scope.LoadData = function () {
        Common.Log('LoadData');
        $rootScope.IsLoading = true;
        Common.Cookie.Set(_OPSAppointment_DIRouteMaster.Data.CookieSearch, JSON.stringify($scope.Search));
        $scope.Summary = '';
        var i = 0;
        while (i < _OPSAppointment_DIRouteMaster.Data._dataHasDNVendor.length) {
            if (_OPSAppointment_DIRouteMaster.Data._dataHasDNVendor[i].ID < 0)
                _OPSAppointment_DIRouteMaster.Data._dataHasDNVendor.splice(i, 1);
            else
                i++;
        }
        _OPSAppointment_DIRouteMaster.Data._dataHasDNVendor.splice(0, 0, { 'ID': -1, 'CustomerName': 'Xe nhà' });
        _OPSAppointment_DIRouteMaster.Data._dataHasDNVehicle = $.extend(true, [], _OPSAppointment_DIRouteMaster.Data._dataVehicleVEN);
        var i = 0;
        while (i < _OPSAppointment_DIRouteMaster.Data._dataHasDNVehicle.length) {
            if (_OPSAppointment_DIRouteMaster.Data._dataHasDNVehicle[i].ID < 0)
                _OPSAppointment_DIRouteMaster.Data._dataHasDNVehicle.splice(i, 1);
            else
                i++;
        }
        _OPSAppointment_DIRouteMaster.Data._dataHasDNSort = [];
        _OPSAppointment_DIRouteMaster.Data._dataHasDNGroupProduct = [];
        var lstFilters = [];
        lstFilters.push(Common.Request.FilterParamWithOr('TypeID', Common.Request.FilterType.Equal, 1));
        if ($scope.IsHasDNMonitor)
            lstFilters.push(Common.Request.FilterParamWithOr('TypeID', Common.Request.FilterType.Equal, 2));
        var param = Common.Request.Create({
            Sorts: [], Filters: lstFilters
        });
        Common.Services.Call($http, {
            url: Common.Services.url.OPS,
            method: _OPSAppointment_DIRouteMaster.URL.List,
            data: { request: param, lstCustomerID: $scope.Search.ListCustomerID, DateFrom: $scope.Search.DateFrom, DateTo: $scope.Search.DateTo },
            success: function (res) {
                Common.Services.Error(res, function (res) {
                    _OPSAppointment_DIRouteMaster.Data._dataHasDNSort[0] = { 'CreateSortOrder': 0, 'CreateVendorID': -1, 'CreateVehicleCode': '', 'CreateDriverName': '', 'CreateTelephone': '', 'Kg': 0 };
                    var datafix = [];
                    var hasMonitor = $scope.IsHasDNMonitor;
                    Common.Services.Call($http, {
                        url: Common.Services.url.OPS,
                        method: _OPSAppointment_DIRouteMaster.URL.OrderList,
                        data: { request: param, lstCustomerID: $scope.Search.ListCustomerID, DateFrom: $scope.Search.DateFrom, DateTo: $scope.Search.DateTo },
                        success: function (res) {
                            Common.Services.Error(res, function (res) {
                                _OPSAppointment_DIRouteMaster.Data._dataHasDN = [];
                                $.each(res, function (i, v) {
                                    if (hasMonitor || v.TypeID < 2) {
                                        if (Common.HasValue(v.RequestDate)) {
                                            v.RequestDate = Common.Date.FromJson(v.RequestDate);
                                            v.RequestDateEmpty = Common.Date.Date(v.RequestDate);
                                        }
                                        v.Kg = v.Ton * 1000;
                                        if (v.CreateSortOrder > 0)
                                            v.CreateSortOrder = datafix[v.CreateSortOrder];
                                        if (Common.HasValue(v.CreateSortOrder))
                                            _OPSAppointment_DIRouteMaster.Data._dataHasDN.push(v);
                                    }
                                });
                                $scope.gridNoDN.dataSource.data(_OPSAppointment_DIRouteMaster.Data._dataHasDN);
                                $rootScope.IsLoading = false;
                            });
                        }
                    });

                    var index = 1;
                    $.each(res, function (i, v) {
                        if (hasMonitor || v.TypeID < 2) {
                            v.CreateDateTime = Common.Date.FromJson(v.CreateDateTime);
                            v.Kg = 0;
                            _OPSAppointment_DIRouteMaster.Data._dataHasDNSort[index] = v;
                            datafix[v.CreateSortOrder] = index;
                            v.CreateSortOrder = index;
                            v.IsChange = false;
                            index++;
                        }
                    });
                });
            }
        });
    };

    $scope.Init_LoadCookie = function () {
        Common.Log('Init_LoadCookie');
        var lstCustomerID = [];
        var strCookie = Common.Cookie.Get(_OPSAppointment_DIRouteMaster.Data.CookieSearch);
        if (Common.HasValue(strCookie) && strCookie != '') {
            try {
                var objCookie = eval('[' + strCookie + ']')[0];
                $scope.Search.DateFrom = Common.Date.FromJson(objCookie.DateFrom);
                $scope.Search.DateTo = Common.Date.FromJson(objCookie.DateTo);
                if (Common.HasValue(objCookie.ListCustomerID)) {
                    lstCustomerID = objCookie.ListCustomerID;
                }
            } catch (e) { }
        }
        if (!Common.HasValue($scope.Search.DateFrom) || !Common.HasValue($scope.Search.DateTo)) {
            $scope.Search.DateFrom = new Date().addDays(-3);
            $scope.Search.DateTo = new Date().addDays(3);
            $scope.Search.ListCustomerID = [];
        }
        $timeout(function () {
            $scope.Search.ListCustomerID = lstCustomerID;
            Common.Cookie.Set(_OPSAppointment_DIRouteMaster.Data.CookieSearch, JSON.stringify($scope.Search));
            $scope.LoadData();
        }, 10);
    };

    $scope.Init_LoadCookie();
    $scope.gridNoDNOptions = {
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'GenID',
                fields:
                    {
                        GenID: { type: 'string', editable: false },
                        ID: { type: 'number' },
                        CreateSortOrder: { type: 'number' },
                        VehicleCode: { type: 'string' },
                        SOCode: { type: 'string' },
                        RequestDateEmpty: { type: 'date' },
                        RequestDate: { type: 'date' },
                        RouteCode: { type: 'string' },
                        Ton: { type: 'number' },
                        Kg: { type: 'number' },
                        Note1: { type: 'string' },
                        Note2: { type: 'string' },
                        DNCode: { type: 'string' }
                    }
            },
            group: [{ field: 'CreateSortOrder', dir: 'desc' }]
        }),
        height: '99%', groupable: false, pageable: false, sortable: true, resizable: true, reorderable: true, sortable: { mode: 'multiple' },
        columns: [
            { field: 'CreateSortOrder', width: '50px', template: '<form class="cus-form-enter" ng-submit="SortEnter_Click($event)" ng-show="dataItem.TypeID != 2"><input kendo-numeric-text-box class="txtCreateSortOrder" value="#=CreateSortOrder>0?CreateSortOrder:0#" data-k-min="0" k-options="numericSortOrderOptions" style="width:100%" /></form>', headerTemplate: '<a class="k-button" href="/" ng-click="Expand_Click($event,gridNoDN)"><i class="fa fa-minus"></i></a>', groupHeaderTemplate: '<span class="HasDNGridGroup" tabid="#=value#"></span>', sortable: false, filterable: false, menu: false },
            { field: 'SOCode', width: '80px', title: 'SO', sortable: true, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'IsOrigin', width: '30px', title: 'g', template: '<input type="checkbox" ng-model="dataItem.IsOrigin" />', sortable: false, filterable: false },
            { field: 'DNCode', width: '90px', title: 'DN', sortable: true, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { title: 'Gộp', width: '45px', template: '<a href="" class="k-button btnAdd" ng-click="Merge_Click($event)" style="display:none">M</a><a href="" class="k-button btnAddSave" ng-click="MergeSave_Click($event)" style="display:none">S</a><input type="checkbox" class="chkAdd" style="display:none"/>', filterable: false, menu: false, sortable: false },
            { field: 'Kg', width: '80px', title: 'KG', template: '# if(TypeEditID == 1){# <form class="cus-form-enter"><input kendo-numeric-text-box value="#:Kg#" k-options="numericKgOptions" data-k-max="#:Kg#" style="width:100%" /></form> #}else{# #:Kg# #}#', sortable: true, filterable: { cell: { operator: 'eq', showOperators: false } } },
            { field: 'Ton', width: '80px', title: 'Tấn', template: '# if(TypeEditID == 1){# <form class="cus-form-enter"><input kendo-numeric-text-box value="#:Ton#" k-options="numericTonOptions" data-k-max="#:Ton#" style="width:100%" /></form> #}else{# #:Ton# #}#', sortable: true, filterable: { cell: { operator: 'eq', showOperators: false } } },
            { field: 'CBM', width: '80px', title: 'Khối', template: '# if(TypeEditID == 2){# <form class="cus-form-enter"><input kendo-numeric-text-box value="#:CBM#" k-options="numericCBMOptions" data-k-max="#:CBM#" style="width:100%" /></form> #}else{# #:CBM# #}#', sortable: true, filterable: { cell: { operator: 'eq', showOperators: false } } },
            { field: 'Quantity', width: '80px', title: 'S.Lượng', template: '# if(TypeEditID == 3){# <form class="cus-form-enter"><input kendo-numeric-text-box value="#:Quantity#" k-options="numericQuantityOptions" data-k-max="#:Quantity#" style="width:100%" /></form> #}else{# #:Quantity# #}#', sortable: true, filterable: { cell: { operator: 'eq', showOperators: false } } },
            { field: 'DistrictName', width: '100px', title: 'Quận / Huyện', sortable: true, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'Address', width: '200px', title: 'Địa chỉ', sortable: true, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'ProvinceName', width: '100px', title: 'Tỉnh / Thành', sortable: true, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'TranferItem', width: '80px', title: 'Vận chuyển', sortable: true, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'RequestDateEmpty', width: '100px', title: 'Ngày gửi', template: "#=Common.Date.FromJsonDDMMHM(RequestDate)#", sortable: true, filterable: { cell: { operator: 'eq', showOperators: false } } },
            { field: 'OrderCode', width: '80px', title: 'List', sortable: true, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'ETD', swidth: '100px', title: 'ETD', template: "#=Common.Date.FromJsonDDMMHM(ETD)#", sortable: true, filterable: { cell: { operator: 'eq', showOperators: false } } },
            { field: 'PartnerCode', width: '100px', title: 'Mã NPP', sortable: true, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'PartnerName', width: '100px', title: 'NPP', sortable: true, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'FromCode', width: '100px', title: 'Mã l.hàng', sortable: true, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'FromAddress', width: '150px', title: 'Điểm l.hàng', sortable: true, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'FromProvince', width: '100px', title: 'Tỉnh thành l.hàng', sortable: true, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'FromDistrict', width: '100px', title: 'Quận huyện l.hàng', sortable: true, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'Description', width: '100px', title: 'Ghi chú', sortable: true, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'CUSRoutingCode', width: '180px', title: 'Cung đường', sortable: true, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { title: ' ', filterable: false, menu: false, sortable: false }
        ],
        toolbar: kendo.template($('#OPSAppointment_DIRouteMaster_gridToolbar').html()),
        filterable: { mode: 'row' },
        dataBound: function () {
            Common.Log("GridDataBound");
            if (Common.HasValue(this) && Common.HasValue(this.tbody)) {
                if (_OPSAppointment_DIRouteMaster.Data._dataHasDNGroupProduct.length == 0)
                    $scope.ReloadSort();
                this.element.find('tr[role="row"]').each(function () {
                    var btnAdd = $(this).find('.btnAdd');
                    var btnAddSave = $(this).find('.btnAddSave');
                    var chkAdd = $(this).find('.chkAdd');
                    if (Common.HasValue($scope.gridNoDN)) {
                        var dataItem = $scope.gridNoDN.dataItem(this);
                        if (Common.HasValue(dataItem)) {
                            if (_OPSAppointment_DIRouteMaster.Data._dataHasDNGroupProduct[dataItem.ID].length > 1)
                                btnAdd.show();
                        }
                    }
                });
                this.element.find('.HasDNGridGroup').each(function () {
                    var sort = parseInt($(this).attr('tabid'));
                    var item = _OPSAppointment_DIRouteMaster.Data._dataHasDNSort[sort];

                    var tr = $(this).closest('tr');
                    if (Common.HasValue(item)) {
                        if (item.IsChange == true) {
                            if (!$(tr).hasClass('approved'))
                                $(tr).addClass('approved');
                        }
                        else if (item.TypeID == 2) {
                            if (!$(tr).hasClass('tendered'))
                                $(tr).addClass('tendered');
                        }
                    }
                    if (sort > 0) {
                        if (Common.HasValue(item)) {
                            var drivername = item.CreateDriverName != null ? item.CreateDriverName : '';
                            var telephone = item.CreateTelephone != null ? item.CreateTelephone : '';
                            var strOverLoad = "<span class='overloadMessage'></span>";
                            var strMaxWeight = "<span class='maxWeight'></span>";
                            if (Common.HasValue(item.MaxWeight) && item.MaxWeight > 0) {
                                strMaxWeight = "<span class='maxWeight'> - [ Trọng tải xe: " + item.MaxWeight + " tấn ]</span>";
                                if (item.Kg > item.MaxWeight * 1000) {
                                    strOverLoad = "<span class='overloadMessage'> - Quá trọng tải</span>";
                                }
                            }
                            if (item.TypeID == 2) {
                                $(this).html('<input type="checkbox" class="chkChooseVehicle"/> Chuyến ' + sort + ' - Kg: ' + Common.Number.ToNumber1(item.Kg) + ' - ' +
                                    item.CreateVendorName + ' - ' + item.CreateVehicleCode + ' - ' + drivername + ' - ' + telephone + ' - ' + Common.Date.FromJsonDMYHM(item.CreateDateTime)) + strMaxWeight + strOverLoad;
                            }
                            else if (item.TOMasterID > 0) {
                                $(this).html('<input type="checkbox" class="chkChooseVehicle"/> Chuyến ' + sort + ' - Kg: ' + Common.Number.ToNumber1(item.Kg) + ' - ' +
                                    '<a href="" class="btnHasDNGroup">' + item.CreateVendorName + ' - ' + item.CreateVehicleCode + ' - ' + drivername + ' - ' + telephone + ' - ' + Common.Date.FromJsonDMYHM(item.CreateDateTime) + '</a>' +
                                    '<input style="display:none;width:200px" class="cboHasDNVendor cus-combobox" placeholder="Nhà vận tải" value="' + item.CreateVendorID + '" /><span style="display:none" class="lblHasDNSplit"> - </span>' +
                                    '<input style="display:none;width:200px" id="cboHasDNVehicle" class="cboHasDNVehicle cus-combobox" placeholder="Nhập số xe" value="' + item.CreateVehicleCode + '" /><span style="display:none" class="lblHasDNSplit"> - </span>' +
                                    '<input style="display:none;width:50px" type="number" min="0" class="txtHasDNMaxWeight k-textbox" placeholder="T.Tải" value="" /><span style="display:none" class="lblHasDNSplit vehicle"> - </span>' +
                                    '<input style="display:none;width:50px" type="number" min="0" class="txtHasDNMaxCBM k-textbox" placeholder="S.Khối" value="" /><span style="display:none" class="lblHasDNSplit vehicle"> - </span>' +
                                    '<input style="display:none;width:150px" type="text" class="txtHasDNDriverName k-textbox" placeholder="Nhập tài xế" value="' + drivername + '" /><span style="display:none" class="lblHasDNSplit"> - </span>' +
                                    '<input style="display:none;width:150px" type="text" class="txtHasDNTelephone k-textbox" placeholder="Nhập điện thoại" value="' + telephone + '"/><span style="display:none" class="lblHasDNSplit"> - </span>' +
                                    '<input style="display:none;width:160px" class="txtCreateDateTime"  value="' + Common.Date.FromJsonDMYHM(item.CreateDateTime) + '"/>' + strMaxWeight + strOverLoad);
                            }
                            else {
                                $(this).html('&nbsp;&nbsp;&nbsp; Chuyến ' + sort + ' - Kg: ' + Common.Number.ToNumber1(item.Kg) + ' - ' +
                                    '<a href="" class="btnHasDNGroup">' + item.CreateVendorName + ' - ' + item.CreateVehicleCode + ' - ' + drivername + ' - ' + telephone + ' - ' + Common.Date.FromJsonDMYHM(item.CreateDateTime) + '</a>' +
                                    '<input style="display:none;width:200px" class="cboHasDNVendor cus-combobox" placeholder="Nhà vận tải" value="' + item.CreateVendorID + '" /><span style="display:none" class="lblHasDNSplit"> - </span>' +
                                    '<input style="display:none;width:200px" id="cboHasDNVehicle" class="cboHasDNVehicle cus-combobox" placeholder="Nhập số xe" value="' + item.CreateVehicleCode + '" /><span style="display:none" class="lblHasDNSplit"> - </span>' +
                                    '<input style="display:none;width:50px" type="number" min="0" class="txtHasDNMaxWeight k-textbox" placeholder="T.Tải" value="" /><span style="display:none" class="lblHasDNSplit vehicle"> - </span>' +
                                    '<input style="display:none;width:50px" type="number" min="0" class="txtHasDNMaxCBM k-textbox" placeholder="S.Khối" value="" /><span style="display:none" class="lblHasDNSplit vehicle"> - </span>' +
                                    '<input style="display:none;width:150px" type="text" class="txtHasDNDriverName k-textbox" placeholder="Nhập tài xế" value="' + drivername + '" /><span style="display:none" class="lblHasDNSplit"> - </span>' +
                                    '<input style="display:none;width:150px" type="text" class="txtHasDNTelephone k-textbox" placeholder="Nhập điện thoại" value="' + telephone + '"/><span style="display:none" class="lblHasDNSplit"> - </span>' +
                                    '<input style="display:none;width:160px" class="txtCreateDateTime"  value="' + Common.Date.FromJsonDMYHM(item.CreateDateTime) + '"/>' + strMaxWeight + strOverLoad);
                            }
                        }
                    }
                    else
                        $(this).html('Kg: ' + Common.Number.ToNumber1(item.Kg));
                });
                this.element.find('.HasDNGridGroup .btnHasDNGroup').click(function (e) {
                    e.preventDefault();
                    $scope.GroupDN_Click(this);
                });
            }
        }
    };

    $scope.OPSAppointment_DIRouteMaster_VendorRate_gridOptions = {
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'SortOrder',
                fields:
                    {
                        SortOrder: { type: 'number', editable: false },
                        VendorID: { type: 'number' },
                        IsManual: { type: 'boolean' },
                        Debit: { type: 'number' },
                    }
            },
        }),
        height: '100%', groupable: false, pageable: false, sortable: true, columnMenu: false, filterable: false, resizable: true,
        columns: [
            { field: 'SortOrder', width: '50px', title: 'T.tự' },
            { field: 'VendorID', width: '300px', title: 'Nhà vận tải', template: '<input focus-k-combobox class="cus-combobox cboVendor" kendo-combo-box k-options="cboVendorOptions" data-bind="value:VendorID" ng-model="dataItem.VendorID" k-data-source="dataItem.ListVendor"/>' },
            { field: 'IsManual', width: '90px', title: 'Nhập giá', template: '<input style="text-align:center" class="chkIsManual" type="checkbox" #= IsManual ? checked="checked" : "" #/>', filterable: false, menu: false, sortable: true },
            { field: 'Debit', width: '120px', title: 'Giá', template: '<input class="txtDebit cus-number" value="#=Debit#" style="width:100%"/>', filterable: false, menu: false, sortable: true },
        ],
        dataBound: function (e) {
            if (Common.HasValue($scope.OPSAppointment_DIRouteMaster_VendorRate_grid) && Common.HasValue($scope.OPSAppointment_DIRouteMaster_VendorRate_grid.tbody)) {
                Common.Log('WinRateGridVENDataBound');
                $($scope.OPSAppointment_DIRouteMaster_VendorRate_grid.element).find('.txtDebit').kendoNumericTextBox({
                    format: '#,##0', spinners: false, culture: 'en-US', min: 0, max: 100000000, step: 1000, decimals: 0,
                    value: 0
                });
                $($scope.OPSAppointment_DIRouteMaster_VendorRate_grid.element).find('.chkIsManual').change(function () {
                    var tr = $(this).closest('tr');
                    var txtDebit = $($(tr).find('input.txtDebit.cus-number')[1]).data('kendoNumericTextBox');
                    txtDebit.enable($(this).prop('checked'));
                });
                var lst = $scope.OPSAppointment_DIRouteMaster_VendorRate_grid.tbody.find('tr');
                $.each(lst, function (itr, tr) {
                    var item = $scope.OPSAppointment_DIRouteMaster_VendorRate_grid.dataItem(tr);
                    var txtDebit = $($(tr).find('input.txtDebit.cus-number')[1]).data('kendoNumericTextBox');
                    var chkIsManual = $($(this).find('.chkIsManual')[0]);
                    if (item.IsManual == true)
                        txtDebit.enable(true);
                    else
                        txtDebit.enable(false);
                    chkIsManual.prop('checked', item.IsManual);
                });
            }
        }
    }

    $timeout(function () {
        Common.Controls.Grid.ReorderColumns({ Grid: $scope.gridNoDN, CookieName: 'OPS_DIAppointment_RouteMaster_gridNoDN' });
    }, 10);

    $scope.LoadDataVehicleVendor = function () {
        var lstVendor = _OPSAppointment_DIRouteMaster.Data._dataHasDNVendor;
        var lstVehicle = _OPSAppointment_DIRouteMaster.Data._dataHasDNVehicle;
        _OPSAppointment_DIRouteMaster.Data._dataHasDNVehicleVEN = [];
        _OPSAppointment_DIRouteMaster.Data._dataHasDNVehicleHome = [];
        $.each(lstVehicle, function (idx, vehicle) {
            if (vehicle.RegNo != "[Chờ nhập xe]") {
                for (var i = 0; i < vehicle.ListVendorID.length; i++) {
                    var vendorid = vehicle.ListVendorID[i];
                    if (!Common.HasValue(_OPSAppointment_DIRouteMaster.Data._dataHasDNVehicleVEN[vendorid]))
                        _OPSAppointment_DIRouteMaster.Data._dataHasDNVehicleVEN[vendorid] = [];
                    _OPSAppointment_DIRouteMaster.Data._dataHasDNVehicleVEN[vendorid].push(vehicle);
                }
                if (vehicle.IsOwn) {
                    _OPSAppointment_DIRouteMaster.Data._dataHasDNVehicleHome.push(vehicle);
                }
            } else {
                _OPSAppointment_DIRouteMaster.Data._dataHasDNVehicleHome.push(vehicle);
                for (var i = 0; i < lstVendor.length; i++) {
                    var vendorid = lstVendor[i].ID;
                    if (!Common.HasValue(_OPSAppointment_DIRouteMaster.Data._dataHasDNVehicleVEN[vendorid]))
                        _OPSAppointment_DIRouteMaster.Data._dataHasDNVehicleVEN[vendorid] = [];
                    _OPSAppointment_DIRouteMaster.Data._dataHasDNVehicleVEN[vendorid].push(vehicle);
                }
            }
        });
    }

    $scope.ReloadSort = function ($event) {
        Common.Log("ReloadSort");
        $scope.IsShowCombine = false;
        var totalsort = 0;
        var totalton = 0;
        var totaltoncomplete = 0;
        var totalcbm = 0;
        var totalcbmcomplete = 0;
        var totalquan = 0;
        $scope.Summary = '';
        $.each(_OPSAppointment_DIRouteMaster.Data._dataHasDNSort, function (i, v) {
            if (Common.HasValue(v)) {
                v.Kg = 0;
                v.CBM = 0;
                v.Quantity = 0;
                if (v.CreateSortOrder > 0)
                    totalsort++;
            }
        });
        _OPSAppointment_DIRouteMaster.Data._dataHasDNGroupProduct = [];
        $.each(_OPSAppointment_DIRouteMaster.Data._dataHasDN, function (i, v) {
            v.Kg = v.Kg > 0 ? Math.round(v.Kg * 1000) / 1000 : 0;
            v.Ton = v.Ton > 0 ? Math.round(v.Ton * 1000000) / 1000000 : 0;
            v.CBM = v.CBM > 0 ? Math.round(v.CBM * 1000) / 1000 : 0;
            v.Quantity = v.Quantity > 0 ? Math.round(v.Quantity * 1000) / 1000 : 0;
            var itemSort = _OPSAppointment_DIRouteMaster.Data._dataHasDNSort[v.CreateSortOrder];
            if (!Common.HasValue(itemSort))
                itemSort = { 'CreateSortOrder': v.CreateSortOrder, 'CreateVendorID': v.VendorOfVehicleID, 'CreateVendorName': v.VendorOfVehicleName, 'CreateVehicleCode': v.VehicleCode, 'CreateDriverName': v.DriverName, 'CreateTelephone': v.DriverTelNo, 'Kg': v.Kg, 'CreateVehicleID': v.VehicleID, 'TOMasterID': v.TOMasterID, 'CreateDateTime': v.CreateDateTime, 'TypeID': v.TypeID, 'MaxWeight': v.MaxWeightCal, 'MaxCBM': 0 };
            if (v.Ton > 0) {
                totalton += v.Ton;
                itemSort.Ton += v.Ton;
                itemSort.Kg += v.Kg;
                if (v.CreateSortOrder > 0) totaltoncomplete += v.Ton;
            }
            if (v.CBM > 0) {
                totalcbm += v.CBM;
                itemSort.CBM += v.CBM;
                if (v.CreateSortOrder > 0) totalcbmcomplete += v.CBM;
            }
            if (v.Quantity > 0) {
                totalquan += v.Quantity;
                itemSort.Quantity += v.Quantity;
            }
            if (!Common.HasValue(_OPSAppointment_DIRouteMaster.Data._dataHasDNGroupProduct[v.ID]))
                _OPSAppointment_DIRouteMaster.Data._dataHasDNGroupProduct[v.ID] = [];
            _OPSAppointment_DIRouteMaster.Data._dataHasDNGroupProduct[v.ID].push(v);
        });
        var ixdel = 0;
        while (ixdel < _OPSAppointment_DIRouteMaster.Data._dataHasDNSort.length) {
            if (Common.HasValue(_OPSAppointment_DIRouteMaster.Data._dataHasDNSort[ixdel])) {
                var itemSort = _OPSAppointment_DIRouteMaster.Data._dataHasDNSort[ixdel];
                if (itemSort.Ton < 0.001 && itemSort.CBM < 0.001) {
                    _OPSAppointment_DIRouteMaster.Data._dataHasDNSort.splice(ixdel, 1);
                }
                else ixdel++;
            }
            else ixdel++;
        }

        $scope.Summary = 'Tấn: ' + Common.Number.ToNumber1(totaltoncomplete) + ' / ' + Common.Number.ToNumber1(totalton) + ' - Khối: ' + Common.Number.ToNumber1(totalcbmcomplete) + ' / ' + Common.Number.ToNumber1(totalcbm) + ' - Số chuyến: ' + totalsort;
    };

    $scope.Approved_Click = function ($event) {
        Common.Log('Approved_Click');
        $event.preventDefault();
        var data = []; var error = [];
        $scope.gridNoDN.tbody.find('.HasDNGridGroup').each(function () {
            var sort = parseInt($(this).attr('tabid'));
            var item = _OPSAppointment_DIRouteMaster.Data._dataHasDNSort[sort];
            var tr = $(this).closest('tr');
            if (sort > 0 && item.TOMasterID > 0) {
                var chk = $(tr).find('.chkChooseVehicle');
                if (Common.HasValue(chk) && chk.prop('checked') == true) {
                    if (item.CreateVehicleCode == '' || item.CreateVehicleCode == "[Chờ nhập xe]") {
                        error.push(item.CreateSortOrder);
                    }
                    data.push(item.TOMasterID);
                }
            }
        });
        if (data.length > 0) {
            if (error.length > 0) {
                $rootScope.Message({
                    Type: Common.Message.Type.Alert,
                    Msg: 'Chuyến "' + error.join(', ') + '" chưa chọn xe'
                });
            } else {
                $rootScope.Message({
                    Type: Common.Message.Type.Confirm,
                    Msg: 'Bạn muốn duyệt các chuyến đã chọn ?',
                    Ok: function () {
                        $rootScope.IsLoading = true;
                        Common.Services.Call($http, {
                            url: Common.Services.url.OPS,
                            method: _OPSAppointment_DIRouteMaster.URL.Approved,
                            data: { lst: data },
                            success: function (res) {
                                Common.Services.Error(res, function (res) {
                                    $scope.LoadData();
                                    $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                                });
                            }
                        });
                    }
                });
            }
        }
    };

    $scope.Tendered_Click = function ($event, win, grid) {
        Common.Log('Tendered_Click');
        $event.preventDefault();
        var data = [];
        $scope.gridNoDN.tbody.find('.HasDNGridGroup').each(function () {
            var sort = parseInt($(this).attr('tabid'));
            var item = _OPSAppointment_DIRouteMaster.Data._dataHasDNSort[sort];
            var tr = $(this).closest('tr');
            if (item.TOMasterID <= 0) {
                $rootScope.Message({ Msg: 'Chỉ gửi Tender các chuyến đã lưu', NotifyType: Common.Message.NotifyType.ERROR });
                data = [];
                return false;
            }
            if (sort > 0 && item.TOMasterID > 0) {
                var chk = $(tr).find('.chkChooseVehicle');
                if (Common.HasValue(chk) && chk.prop('checked') == true)
                    data.push(item.TOMasterID);
            }
        });
        if (data.length > 0) {
            var dataVEN = [];
            var dataVendor = $.extend(true, [], _OPSAppointment_DIRouteMaster.Data._dataHasDNVendor);
            dataVendor.splice(0, 1);
            dataVEN.push({ SortOrder: 1, VendorID: dataVendor[0].ID, IsManual: false, Debit: 0, lstVendor: dataVendor });
            dataVEN.push({ SortOrder: 2, VendorID: dataVendor[0].ID, IsManual: false, Debit: 0, lstVendor: dataVendor });
            dataVEN.push({ SortOrder: 3, VendorID: dataVendor[0].ID, IsManual: false, Debit: 0, lstVendor: dataVendor });
            $scope.OPSAppointment_DIRouteMaster_VendorRate_grid.dataSource.data(dataVEN);
            $timeout(function () {
                $scope.OPSAppointment_DIRouteMaster_VendorRate_grid.resize();
            }, 10);
            win.center().open();
        }
    };

    $scope.UnApproved_Click = function ($event) {
        Common.Log('UnApproved_Click');
        $event.preventDefault();
        var data = [];
        $scope.gridNoDN.tbody.find('.HasDNGridGroup').each(function () {
            var sort = parseInt($(this).attr('tabid'));
            var item = _OPSAppointment_DIRouteMaster.Data._dataHasDNSort[sort];
            var tr = $(this).closest('tr');
            if (sort > 0 && item.TOMasterID > 0) {
                var chk = $(tr).find('.chkChooseVehicle');
                if (Common.HasValue(chk) && chk.prop('checked') == true)
                    data.push(item.TOMasterID);
            }
        });
        if (data.length > 0) {
            $rootScope.Message({
                Type: Common.Message.Type.Confirm,
                Msg: 'Bạn muốn hủy duyệt các chuyến đã chọn ?',
                Ok: function () {
                    $rootScope.IsLoading = true;
                    Common.Services.Call($http, {
                        url: Common.Services.url.OPS,
                        method: _OPSAppointment_DIRouteMaster.URL.UnApproved,
                        data: { lst: data },
                        success: function (res) {
                            Common.Services.Error(res, function (res) {
                                $scope.LoadData();
                                $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                            });
                        }
                    });
                }
            });
        }
    };

    $scope.Delete_Click = function ($event) {
        Common.Log('Delete_Click');
        $event.preventDefault();
        var data = [];
        $scope.gridNoDN.tbody.find('.HasDNGridGroup').each(function () {
            var sort = parseInt($(this).attr('tabid'));
            var item = _OPSAppointment_DIRouteMaster.Data._dataHasDNSort[sort];
            var tr = $(this).closest('tr');
            if (sort > 0 && item.TOMasterID > 0) {
                var chk = $(tr).find('.chkChooseVehicle');
                if (Common.HasValue(chk) && chk.prop('checked') == true && item.TypeID < 2 && item.TOMasterID > 0)
                    data.push(item.TOMasterID);
            }
        });
        if (data.length > 0) {
            $rootScope.Message({
                Type: Common.Message.Type.Confirm,
                Msg: 'Bạn muốn xóa các chuyến đã chọn ?',
                Ok: function () {
                    $rootScope.IsLoading = true;
                    Common.Services.Call($http, {
                        url: Common.Services.url.OPS,
                        method: _OPSAppointment_DIRouteMaster.URL.Delete,
                        data: { lst: data },
                        success: function (res) {
                            Common.Services.Error(res, function (res) {
                                $scope.LoadData();
                                $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                            });
                        }
                    });
                }
            });
        }
        else
            $rootScope.Message({ Msg: 'Chỉ xóa các chuyến chưa duyệt', NotifyType: Common.Message.NotifyType.ERROR });
    };

    $scope.Save_Click = function ($event) {
        Common.Log('Save_Click');
        $event.preventDefault();
        var flag = true;
        var error = '';
        $.each(_OPSAppointment_DIRouteMaster.Data._dataHasDNSort, function (i, v) {
            if (Common.HasValue(v)) {
                if (v.CreateSortOrder > 0 && (v.CreateVehicleCode == '')) {
                    //|| v.CreateVehicleCode == '[Chờ nhập xe]'
                    error += ', ' + v.CreateSortOrder;
                    flag = false;
                }
            }
        });
        var data = [];
        $.each(_OPSAppointment_DIRouteMaster.Data._dataHasDN, function (i, v) {
            if (v.CreateSortOrder > 0 && _OPSAppointment_DIRouteMaster.Data._dataHasDNSort[v.CreateSortOrder].TypeID < 2 && v.Kg > 0) {
                if (!Common.HasValue(v.CreateDriverName))
                    v.CreateDriverName = '';
                if (!Common.HasValue(v.CreateTelephone))
                    v.CreateTelephone = '';
                data.push(v);
            }
        });
        if (flag) {
            $rootScope.Message({
                Type: Common.Message.Type.Confirm,
                Msg: 'Bạn muốn lưu các chuyến đã lập ?',
                Ok: function () {
                    $rootScope.IsLoading = true;
                    var dataDel = [];
                    var dataAdd = [];
                    $.each(_OPSAppointment_DIRouteMaster.Data._dataHasDNSort, function (i, v) {
                        if (Common.HasValue(v)) {
                            if (v.CreateSortOrder > 0 && v.TypeID < 2) {
                                if (v.Kg == 0 && v.TOMasterID > 0)
                                    dataDel.push(v.TOMasterID);
                                else
                                    dataAdd.push(v);
                            }
                        }
                    });
                    _OPSAppointment_DIRouteMaster.Data._dataHasDNSort = dataAdd;
                    _OPSAppointment_DIRouteMaster.Data._dataHasDN = data;
                    Common.Services.Call($http, {
                        url: Common.Services.url.OPS,
                        method: _OPSAppointment_DIRouteMaster.URL.Delete,
                        data: { lst: dataDel },
                        success: function (res) {
                            Common.Services.Error(res, function (res) {
                                Common.Services.Call($http, {
                                    url: Common.Services.url.OPS,
                                    method: _OPSAppointment_DIRouteMaster.URL.Save,
                                    data: { lstOrder: _OPSAppointment_DIRouteMaster.Data._dataHasDN, lstVehicle: _OPSAppointment_DIRouteMaster.Data._dataHasDNSort },
                                    success: function (res) {
                                        Common.Services.Error(res, function (res) {
                                            $scope.LoadData();
                                            $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                                        });
                                    }
                                });
                            });
                        }
                    });
                }
            });
        }
        else
            $rootScope.Message({ Msg: 'Các chuyến ' + error.substr(2) + ' chưa chọn xe', NotifyType: Common.Message.NotifyType.ERROR });
    };

    $scope.Close_Click = function ($event) {
        Common.Log('Close_Click');
        if (Common.HasValue($event))
            $event.preventDefault();
        _OPSAppointment_DIRouteMaster.Data._dataDistrict = [];
        _OPSAppointment_DIRouteMaster.Data._createListDN = [];
        _OPSAppointment_DIRouteMaster.Data._createListID = [];
        _OPSAppointment_DIRouteMaster.Data._dataHasDNGroupProduct = [];
        _OPSAppointment_DIRouteMaster.Data._dataHasDN = [];
        _OPSAppointment_DIRouteMaster.Data._dataHasDNSort = [];
        _OPSAppointment_DIRouteMaster.Data._dataHasDNVendor = [];
        _OPSAppointment_DIRouteMaster.Data._dataHasDNVehicle = [];
        $state.go('main.OPSAppointment.DI');
    };

    $scope.SortEnter_Click = function ($event) {
        Common.Log('SortEnter_Click');
        if (Common.HasValue($event))
            $event.preventDefault();
        if (Common.HasValue(this.dataItem)) {
            var firstVendorID = _OPSAppointment_DIRouteMaster.Data._dataHasDNVendor.length > 0 ? _OPSAppointment_DIRouteMaster.Data._dataHasDNVendor[0].ID : -1;
            var firstVehicleCode = _OPSAppointment_DIRouteMaster.Data._dataHasDNVehicle.length > 0 ? _OPSAppointment_DIRouteMaster.Data._dataHasDNVehicle[0].RegNo : '';
            var firstVendorName = _OPSAppointment_DIRouteMaster.Data._dataHasDNVendor.length > 0 ? _OPSAppointment_DIRouteMaster.Data._dataHasDNVendor[0].CustomerName : '';
            var dataItem = this.dataItem;
            var sort = dataItem.CreateSortOrder;
            if (!Common.HasValue(_OPSAppointment_DIRouteMaster.Data._dataHasDNSort[sort]))
                _OPSAppointment_DIRouteMaster.Data._dataHasDNSort[sort] = { 'TOMasterID': -1, 'CreateSortOrder': sort, 'CreateVendorID': firstVendorID, 'CreateVendorName': firstVendorName, 'CreateVehicleCode': firstVehicleCode, 'CreateDriverName': '', 'CreateTelephone': '', 'CreateDateTime': new Date(), 'Kg': 0, 'IsChange': true, 'TypeID': 1 };
            if (sort == 0 || _OPSAppointment_DIRouteMaster.Data._dataHasDNSort[sort].TypeID < 2) {
                dataItem.CreateSortOrder = sort;
            }
            _OPSAppointment_DIRouteMaster.Data._dataHasDN = $scope.gridNoDNOptions.dataSource.data();
            $scope.ReloadSort();
            $scope.gridNoDNOptions.dataSource.data(_OPSAppointment_DIRouteMaster.Data._dataHasDN);
        }
    };

    $scope.Expand_Click = function ($event, grid) {
        Common.Log('Expand_Click');
        if (Common.HasValue($event))
            $event.preventDefault();
        $scope.IsExpand = !$scope.IsExpand;
        // Expand tất cả group
        if ($scope.IsExpand) {
            $(grid.element).find("a.k-icon.k-i-expand").trigger("click");
            $($event.currentTarget).find("i").removeClass("fa-plus");
            $($event.currentTarget).find("i").addClass("fa-minus");
        } else {
            // Collapse tất cả group
            $(grid.element).find("a.k-icon.k-i-collapse").trigger("click");
            $($event.currentTarget).find("i").removeClass("fa-minus");
            $($event.currentTarget).find("i").addClass("fa-plus");
        }
    };

    $scope.Merge_Click = function ($event) {
        Common.Log("Merge_Click");
        $event.preventDefault();
        // Ẩn các nút Merge
        $scope.gridNoDN.tbody.find('tr[role="row"] .btnAdd').hide();
        // Hiện nút Save
        var btnAddSave = $($($event.currentTarget).closest('tr')).find('.btnAddSave').show();
        // Hiện nút Hủy gom hàng
        $scope.IsShowCombine = true;
        // Lấy ra dataItem hiện tại
        var dataItem = this.dataItem;
        $.each(_OPSAppointment_DIRouteMaster.Data._dataHasDNGroupProduct[dataItem.ID], function (i, v) {
            if (v.uid != dataItem.uid) {
                var tr = $scope.gridNoDN.tbody.find("tr[data-uid='" + v.uid + "']");
                var chkAdd = $(tr).find('.chkAdd');
                $(chkAdd).show();
            }
        });
    };

    $scope.MergeSave_Click = function ($event) {
        Common.Log("MergeSave_Click");
        $event.preventDefault();
        var dataItem = this.dataItem;
        $.each(_OPSAppointment_DIRouteMaster.Data._dataHasDNGroupProduct[dataItem.ID], function (i, v) {
            if (v.uid != dataItem.uid) {
                var tr = $scope.gridNoDN.tbody.find("tr[data-uid='" + v.uid + "']");
                var chkAdd = $(tr).find('.chkAdd');
                if ($(chkAdd).prop('checked') == true) {
                    if (dataItem.TypeEditID == 1) {
                        dataItem.Kg += v.Kg;
                        dataItem.Ton += v.Kg / 1000;
                        if (dataItem.ExchangeTon > 0) {
                            dataItem.CBM += dataItem.ExchangeCBM * v.Ton / dataItem.ExchangeTon;
                            dataItem.Quantity += dataItem.ExchangeQuantity * v.Ton / dataItem.ExchangeTon;
                        }
                    } else {
                        if (dataItem.TypeEditID == 2) {
                            dataItem.CBM += v.CBM;
                            if (dataItem.ExchangeCBM > 0) {
                                dataItem.Ton += dataItem.ExchangeCBM * v.CBM / dataItem.ExchangeCBM;
                                dataItem.Kg += dataItem.Ton * 1000;
                                dataItem.Quantity += dataItem.ExchangeQuantity * v.CBM / dataItem.ExchangeCBM;
                            }
                        } else {
                            if (dataItem.TypeEditID == 3) {
                                dataItem.Quantity += v.Quantity;
                                if (dataItem.ExchangeQuantity > 0) {
                                    dataItem.Ton = dataItem.ExchangeTon * v.Quantity / dataItem.ExchangeQuantity;
                                    dataItem.Kg += dataItem.Ton * 1000;
                                    dataItem.CBM = dataItem.ExchangeCBM * v.Quantity / dataItem.ExchangeQuantity;
                                }
                            }
                        }
                    }
                    var index = _OPSAppointment_DIRouteMaster.Data._dataHasDN.indexOf(v);
                    _OPSAppointment_DIRouteMaster.Data._dataHasDN.splice(index, 1);
                }
            }
        });
        $scope.ReloadSort();
        $scope.gridNoDN.dataSource.data(_OPSAppointment_DIRouteMaster.Data._dataHasDN);
    };

    $scope.MergeCancel_Click = function ($event) {
        Common.Log("MergeCancel_Click");
        $event.preventDefault();
        $scope.gridNoDN.dataSource.data(_OPSAppointment_DIRouteMaster.Data._dataHasDN);
        $scope.IsShowCombine = false;
    };

    $scope.TonEnter_Click = function ($event) {
        Common.Log('TonEnter_Click');
        if (Common.HasValue($event))
            $event.preventDefault();
    };

    $scope.CreateHasDNMonitor_Click = function ($event) {
        Common.Log('CreateHasDNMonitor_Click');
        $scope.LoadData();
    };

    $scope.GroupDN_Click = function (item) {
        Common.Log('GroupDN_Click');
        $(item).hide();
        var tr = $(item).closest('tr');
        $(tr).find('.lblHasDNSplit,input').show();
        $(tr).find('.txtHasDNMaxWeight,.txtHasDNMaxCBM').hide();
        $(tr).find('.lblHasDNSplit.vehicle').hide();
        var item = $scope.gridNoDN.dataItem(tr);
        var dataVehicle = [];
        var sort = _OPSAppointment_DIRouteMaster.Data._dataHasDNSort[item.CreateSortOrder];
        if (sort.CreateVendorID == null || sort.CreateVendorID == 0 || sort.CreateVendorID == -1) {
            dataVehicle = _OPSAppointment_DIRouteMaster.Data._dataHasDNVehicleHome;
        } else {
            dataVehicle = _OPSAppointment_DIRouteMaster.Data._dataHasDNVehicleVEN[sort.CreateVendorID];
        }
        tr.find('input.cboHasDNVendor').kendoComboBox({
            index: 0, autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true,
            dataTextField: 'CustomerName', dataValueField: 'ID',
            dataSource: Common.DataSource.Local({
                data: _OPSAppointment_DIRouteMaster.Data._dataHasDNVendor,
                model: {
                    id: 'ID',
                    fields: {
                        ID: { type: 'number' },
                        CustomerName: { type: 'string' }
                    }
                },
            }),
            change: function (e) {
                var span = $(e.sender.element).closest('span.HasDNGridGroup');
                sort = $(span).attr('tabid');
                var item = _OPSAppointment_DIRouteMaster.Data._dataHasDNSort[sort];
                var sel = e.sender.dataItem();
                var CreateVendorID = sel != null ? sel.ID : 0;
                if (item.CreateVendorID != CreateVendorID) {
                    item.CreateVendorID = sel != null ? sel.ID : 0;
                    var cboVehicle = $(span).find("#cboHasDNVehicle").data("kendoComboBox");
                    cboVehicle.dataSource.data([]);
                    var txtDriver = $(span).find("input.txtHasDNDriverName").data("kendoAutoComplete");
                    var dataDriver = [];
                    if (item.CreateVendorID == null || item.CreateVendorID == 0 || item.CreateVendorID == -1) {
                        cboVehicle.dataSource.data(_OPSAppointment_DIRouteMaster.Data._dataHasDNVehicleHome);
                        dataDriver = _OPSAppointment_DIRouteMaster.Data._dataDriver;
                    } else
                        cboVehicle.dataSource.data(_OPSAppointment_DIRouteMaster.Data._dataHasDNVehicleVEN[item.CreateVendorID]);
                    item.CreateVehicleCode = cboVehicle.dataSource.data()[0].RegNo;
                    cboVehicle.text(item.CreateVehicleCode);
                    $timeout(function () {
                        txtDriver.dataSource.data(dataDriver);
                    }, 10);
                }
            }
        });

        tr.find('input.cboHasDNVehicle').each(function () {
            var cbo = $(this).kendoComboBox({
                index: 0, autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true,
                dataTextField: 'RegNo', dataValueField: 'RegNo',
                dataSource: Common.DataSource.Local({
                    data: dataVehicle,
                    model: {
                        id: 'string',
                        fields: {
                            RegNo: { type: 'string' }
                        }
                    }
                }),
                change: function (e) {
                    var span = $(e.sender.element).closest('span.HasDNGridGroup');
                    sort = $(span).attr('tabid');
                    var item = _OPSAppointment_DIRouteMaster.Data._dataHasDNSort[sort];
                    item.CreateVehicleCode = e.sender.text();
                    item.MaxWeight = 0;
                    item.IsChange = true;
                    var txtHasDNDriverName = $(span).find('input.txtHasDNDriverName').data("kendoAutoComplete");
                    var txtHasDNTelephone = $(span).find('.txtHasDNTelephone');
                    var txtMaxWeight = $(span).find('.maxWeight');
                    var txtOverLoadMessage = $(span).find('.overloadMessage');
                    var txtHasDNMaxWeight = $(span).find('.txtHasDNMaxWeight');
                    var txtHasDNMaxCBM = $(span).find('.txtHasDNMaxCBM');
                    txtMaxWeight.text("");
                    txtOverLoadMessage.text("");
                    var isHasVehicle = false;
                    var driverName = "";
                    var driverTel = "";

                    var flag = true;
                    var objD = $scope.FindDriver(item);
                    if (objD.ID < 1) {
                        flag = false;
                    }
                    driverName = objD.Name;
                    driverTel = objD.Tel;

                    $.each(_OPSAppointment_DIRouteMaster.Data._dataHasDNVehicle, function (i, v) {
                        if (item.CreateVehicleCode == v.RegNo) {
                            if (Common.HasValue(v.MaxWeight) && v.MaxWeight > 0) {
                                txtMaxWeight.text(" - [ Trọng tải xe: " + v.MaxWeight + " tấn ]");
                                if (item.Kg > v.MaxWeight * 1000)
                                    txtOverLoadMessage.text(" - Quá trọng tải");
                            }
                            driverName = v.DriverName;
                            driverTel = v.Cellphone;
                            isHasVehicle = true;
                            txtHasDNMaxWeight.hide();
                            txtHasDNMaxCBM.hide();
                            $(span).find('.lblHasDNSplit.vehicle').hide();
                            return;
                        }
                    });
                    txtHasDNDriverName.value(driverName);
                    txtHasDNTelephone.val(driverTel);
                    txtHasDNDriverName.trigger('change');
                    $(txtHasDNTelephone).trigger('change');
                    // Nếu nhập xe mới -> hiển thị trọng tải, số khối cho nhập vào
                    if (!isHasVehicle) {
                        txtHasDNMaxWeight.val('');
                        txtHasDNMaxCBM.val('');
                        txtHasDNMaxWeight.show();
                        txtHasDNMaxCBM.show();
                        $(span).find('.lblHasDNSplit.vehicle').show();
                    }
                }
            }).data('kendoComboBox');
            cbo.text(_OPSAppointment_DIRouteMaster.Data._dataHasDNSort[item.CreateSortOrder].CreateVehicleCode);
        });

        tr.find('input.txtHasDNDriverName').kendoAutoComplete({
            autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true, placeholder: "Nhập tài xế",
            dataTextField: 'DriverName', dataValueField: 'DriverName',
            dataSource: Common.DataSource.Local({
                data: _OPSAppointment_DIRouteMaster.Data._dataDriver,
                model: {
                    id: 'ID',
                    fields: {
                        ID: { type: 'number' },
                        DriverName: { type: 'string' }
                    }
                },
            }),
            change: function (e) {
                var span = $(this.element).closest('span.HasDNGridGroup');
                sort = $(span).attr('tabid');
                var item = _OPSAppointment_DIRouteMaster.Data._dataHasDNSort[sort];
                item.CreateDriverName = this.value();
                item.IsChange = true;
            }
        });

        tr.find('input.txtHasDNMaxWeight').change(function (e) {
            var span = $(this).closest('span.HasDNGridGroup');
            sort = $(span).attr('tabid');
            var item = _OPSAppointment_DIRouteMaster.Data._dataHasDNSort[sort];
            item.MaxWeight = $(this).val();
            item.IsChange = true;
        });

        tr.find('input.txtHasDNMaxCBM').change(function (e) {
            var span = $(this).closest('span.HasDNGridGroup');
            sort = $(span).attr('tabid');
            var item = _OPSAppointment_DIRouteMaster.Data._dataHasDNSort[sort];
            item.MaxCBM = $(this).val();
            item.IsChange = true;
        });

        tr.find('input.txtHasDNTelephone').change(function (e) {
            var span = $(this).closest('span.HasDNGridGroup');
            sort = $(span).attr('tabid');
            var item = _OPSAppointment_DIRouteMaster.Data._dataHasDNSort[sort];
            item.CreateTelephone = $(this).val();
            item.IsChange = true;
        });

        tr.find('input.txtCreateDateTime').kendoDateTimePicker({
            format: Common.Date.Format.DMYHM,
            timeFormat: Common.Date.Format.HM,
            change: function (e) {
                var span = $(e.sender.element).closest('span.HasDNGridGroup');
                sort = $(span).attr('tabid');
                var item = _OPSAppointment_DIRouteMaster.Data._dataHasDNSort[sort];
                item.CreateDateTime = e.sender.value();
                item.IsChange = true;

                try {
                    var txtHasDNDriverName = $(span).find('input.txtHasDNDriverName').data("kendoAutoComplete");
                    var txtHasDNTelephone = $(span).find('.txtHasDNTelephone');
                    var objD = $scope.FindDriver(item);
                    if (objD.ID > 1) {
                        txtHasDNDriverName.value(objD.Name);
                        txtHasDNTelephone.val(objD.Tel);
                        txtHasDNDriverName.trigger('change');
                        txtHasDNTelephone.trigger('change');
                    }
                } catch (e) {

                }                
            }
        });
    };

    $scope.FindDriver = function (item) {
        var obj = {
            ID: -1,
            Name: '',
            Tel: ''
        };
        try {
            if (item.CreateVendorName == 'Xe nhà') {
                $.each(_OPSAppointment_DIRouteMaster.Data.FLMPlaning, function (i, v) {
                    if (item.CreateVehicleCode == v.VehicleCode) {
                        if (Common.Date.FromJson(v.DateFrom) < item.CreateDateTime && Common.Date.FromJson(v.DateTo) > item.CreateDateTime) {
                            obj = {
                                ID: v.DriverID,
                                Name: v.DriverName,
                                Tel: v.DriverTel
                            }
                        }
                    }
                })
            }
        } catch (e) {

        }
        return obj;
    }

    $scope.Search_Click = function ($event) {
        $event.preventDefault();
        $scope.LoadData();
        Common.Cookie.Set(_OPSAppointment_DIRouteMaster.Data.CookieSearch, JSON.stringify($scope.Search));
    }

    $scope.ShowTimeLine_Click = function ($event) {
        if ($scope.Search.IsShowTimeLine) {
            $scope.Search.MyClass = "left3-4";
            $timeout(function () {
                $scope.OPSAppointment_DIRouteMaster_grid.resize();
            }, 10);
        }

        else
            $scope.Search.MyClass = "full";

        Common.Cookie.Set(_OPSAppointment_DIRouteMaster.Data.CookieSearch, JSON.stringify($scope.Search));
    }

    $scope.FTL_Click = function ($event) {
        $event.preventDefault();
        $state.go("main.OPSAppointment.DIRouteMasterFTL");
    }

    //#region Vehicle
    $scope.OPSAppointment_DIRouteMaster_Vehicle_splitterOptions = {
        orientation: "vertical",
        panes: [
            { collapsible: false, resizable: false, size: '40px' },
            { collapsible: false, resizable: true },
        ],
    };
    $scope.OPSAppointment_DIRouteMaster_Vehicle_LoadLabel = function () {
        $scope.Vehicle.ConfigString = '';
        var lst = _OPSAppointment_DIRouteMaster.Timeline.GetListRouteInDay();
        var str = '';
        $.each(lst, function (i, v) {
            str += '[' + v.Name + ']:' + Common.Date.ToString(v.HourFrom, Common.Date.Format.HM) + '-' + Common.Date.ToString(v.HourTo, Common.Date.Format.HM) + '&nbsp;&nbsp;';
        });
        if (str != '')
            str = 'Ngày: ' + Common.Date.ToString($scope.Vehicle.DateFrom, Common.Date.Format.DDMMYY) + ' - ' +
                Common.Date.ToString($scope.Vehicle.DateTo, Common.Date.Format.DDMMYY) + '&nbsp;&nbsp;&nbsp;' + str;
        else
            str = 'Ngày: ' + Common.Date.ToString($scope.Vehicle.DateFrom, Common.Date.Format.DDMMYY) + '&nbsp;&nbsp;&nbsp;' +
                Common.Date.ToString($scope.Vehicle.HourFrom, Common.Date.Format.HM) + ' - ' + Common.Date.ToString($scope.Vehicle.HourTo, Common.Date.Format.HM);
        $scope.Vehicle.ConfigString = str;
    }

    $scope.VehicleConfig_Click = function ($event) {
        Common.Log('VehicleConfig_Click');
        $event.preventDefault();
        $scope.Vehicle.IsShowConfig = !$scope.Vehicle.IsShowConfig;
        // Nếu đóng config thì load lại grid + lưu cookie
        if (!$scope.Vehicle.IsShowConfig) {
            // Load lại data
            _OPSAppointment_DIRouteMaster.Timeline.ChangeTime({
                search: {
                    DateFrom: $scope.Vehicle.DateFrom,
                    DateTo: $scope.Vehicle.DateTo,
                    HourFrom: $scope.Vehicle.HourFrom,
                    HourTo: $scope.Vehicle.HourTo
                }
            });
            // Set cookie
            Common.Cookie.Set(_OPSAppointment_DIRouteMaster.Data.CookieVehicle, JSON.stringify($scope.Vehicle));
            // Resize
            $scope.OPSAppointment_DIRouteMaster_Vehicle_splitter.size(".k-pane:first", "35px");
        } else {
            $scope.OPSAppointment_DIRouteMaster_Vehicle_splitter.size(".k-pane:first", "70px");
        }
        $scope.OPSAppointment_DIRouteMaster_Vehicle_LoadLabel();
    };

    $timeout(function () {
        _OPSAppointment_DIRouteMaster.Timeline = Common.Timeline({
            grid: $scope.OPSAppointment_DIRouteMaster_grid,
            model: {
                id: 'ID',
                fields:
                    {
                        ID: { type: 'number', editable: false },
                        RegNo: { type: 'string' },
                        VehicleID: { type: 'number' },
                        VendorName: { type: 'string' },
                        MaxWeightCal: { type: 'number' }
                    }
            },
            modelGroup: [{ field: 'VendorName' }],
            modelSort: { field: 'VehicleID', dir: 'asc' },
            columns: [
                { field: 'VendorName', hidden: true, groupHeaderTemplate: '#= value #' },
                { field: 'RegNo', width: '90px', title: 'Xe', template: '<div class="bgtruck allowdrop"><span class="fa fa-truck"></span>&nbsp;#=RegNo#</div>', sortable: true, locked: true },
                { field: 'MaxWeightCal', width: '50px', template: '<div class="allowdrop">#=MaxWeightCal==null?"":MaxWeightCal#</div>', title: 'T.tải', sortable: true, locked: true }
            ],
            search: $scope.Vehicle,
            eventMainData: function () {
                Common.Log('VehicleMainData');
                if (!$scope.IsShowVehicleVendor) {
                    Common.Services.Call($http, {
                        url: Common.Services.url.OPS,
                        method: _OPSAppointment_DIRouteMaster.URL.VehicleList,
                        success: function (res) {
                            Common.Services.Error(res, function (res) {
                                _OPSAppointment_DIRouteMaster.Data._dataVehicle = [];
                                $.each(res, function (i, v) {
                                    v.VendorName = 'Xe nhà';
                                    v.VehicleID = v.ID;
                                    _OPSAppointment_DIRouteMaster.Data._dataVehicle.push(v);
                                });

                                res.splice(0, 0, { 'ID': -1, 'RegNo': ' Xe ngoài', 'VehicleID': -1, 'MaxWeightCal': null, 'GroupOfVehicleID': null, 'GroupOfVehicleName': '', 'VendorName': '' });
                                _OPSAppointment_DIRouteMaster.Timeline.SetMainData(res);
                            });
                        }
                    });
                } else {
                    Common.Services.Call($http, {
                        url: Common.Services.url.OPS,
                        method: _OPSAppointment_DIRouteMaster.URL.VehicleTOVENList,
                        success: function (res) {
                            Common.Services.Error(res, function (res) {
                                var data = res;
                                $.each(data, function (i, v) {
                                    v.MaxWeightCal = null;
                                    v.GroupOfVehicleID = null;
                                    v.GroupOfVehicleName = '';
                                });
                                res.splice(0, 0, { 'ID': -1, 'RegNo': '[ Chờ xác nhận ]', 'MaxWeightCal': null, 'GroupOfVehicleID': null, 'GroupOfVehicleName': '', 'VendorName': '' });
                                _OPSAppointment_DIRouteMaster.Timeline.SetMainData(res);
                            });
                        }
                    });
                }
            },
            eventDetailData: function (dtFrom, dtTo) {
                Common.Log('VehicleDetailData');
                if (!$scope.IsShowVehicleVendor) {
                    var lst = _OPSAppointment_DIRouteMaster.Timeline.GetListRouteInDay();
                    var str = '';
                    $.each(lst, function (i, v) {
                        str += '[' + v.Name + ']:' + Common.Date.ToString($scope.Vehicle.HourFrom, Common.Date.Format.HM) + '-' + Common.Date.ToString($scope.Vehicle.HourTo, Common.Date.Format.HM) + '&nbsp;&nbsp;';
                    });
                    if (str != '')
                        str = 'Ngày: ' + Common.Date.ToString($scope.Vehicle.DateFrom, Common.Date.Format.DDMMYY) + ' - ' +
                            Common.Date.ToString($scope.Vehicle.DateTo, Common.Date.Format.DDMMYY) + '&nbsp;&nbsp;&nbsp;' + str;
                    else
                        str = 'Ngày: ' + Common.Date.ToString($scope.Vehicle.DateFrom, Common.Date.Format.DDMMYY) + '&nbsp;&nbsp;&nbsp;' +
                            Common.Date.ToString($scope.Vehicle.HourFrom, Common.Date.Format.HM) + ' - ' + Common.Date.ToString($scope.Vehicle.HourTo, Common.Date.Format.HM);
                    $scope.Vehicle.ConfigString = str;
                    var param = Common.Request.Create({
                        Sorts: [], Filters: [
                            Common.Request.FilterParamWithAnd('DateFrom', Common.Request.FilterType.GreaterThanOrEqual, dtFrom),
                            Common.Request.FilterParamWithAnd('DateFrom', Common.Request.FilterType.LessThanOrEqual, dtTo)
                        ]
                    });
                    Common.Services.Call($http, {
                        url: Common.Services.url.OPS,
                        method: _OPSAppointment_DIRouteMaster.URL.VehicleTimeList,
                        data: { request: param },
                        success: function (res) {
                            Common.Services.Error(res, function (res) {
                                $.each(res, function (i, v) {
                                    v.DateFrom = Common.Date.FromJson(v.DateFrom);
                                    v.DateTo = Common.Date.FromJson(v.DateTo);
                                });
                                _OPSAppointment_DIRouteMaster.Timeline.SetDetailData(res);
                            });
                        }
                    });
                } else {
                    var param = Common.Request.Create({
                        Sorts: [], Filters: [
                            Common.Request.FilterParamWithAnd('DateFrom', Common.Request.FilterType.GreaterThanOrEqual, dtFrom),
                            Common.Request.FilterParamWithAnd('DateFrom', Common.Request.FilterType.LessThanOrEqual, dtTo)
                        ]
                    });

                    Common.Services.Call($http, {
                        url: Common.Services.url.OPS,
                        method: _OPSAppointment_DIRouteMaster.URL.VehicleTOVEN,
                        data: { request: param },
                        success: function (res) {
                            Common.Services.Error(res, function (res) {
                                $.each(res, function (i, v) {
                                    v.DateFrom = Common.Date.FromJson(v.DateFrom);
                                    v.DateTo = Common.Date.FromJson(v.DateTo);
                                });
                                _OPSAppointment_DIRouteMaster.Timeline.SetDetailData(res);
                            });
                        }
                    });
                }
            },
        });
        _OPSAppointment_DIRouteMaster.Timeline.Init();
    }, 500);

    $scope.OPSAppointment_DIRouteMaster_VehicleMenuOptions = {
        orientation: 'vertical',
        target: '#OPSAppointment_DIRouteMaster_grid',
        filter: 'tr',
        animation: { open: { effects: 'fadeIn' }, duration: 300 },
        open: function (e) {
            if ($scope.IsShowVehicleVendor)
                $($scope.OPSAppointment_DIRouteMaster_VehicleMenu.element.find(".lblVehicleList")).html('Hiện danh sách xe nhà');
            else
                $($scope.OPSAppointment_DIRouteMaster_VehicleMenu.element.find(".lblVehicleList")).html('Hiện danh sách xe ngoài');
        },
        select: function (e) {
            var index = $(e.item).index();
            switch (index) {
                case 0:
                    $scope.IsShowVehicleVendor = !$scope.IsShowVehicleVendor;
                    _OPSAppointment_DIRouteMaster.Timeline.RefreshMain();
                    break;
                case 1:
                    $scope.IsShowVehicleVendor = !$scope.IsShowVehicleVendor;
                    _OPSAppointment_DIRouteMaster.Timeline.RefreshMain();
                    break;
                case 3: break;
                case 4: break;
                case 5: break;
            }
        },
    };

    $scope.TenderSend_Click = function ($event, win, grid) {
        Common.Log('TenderSend_Click');
        $event.preventDefault();
        var data = [];
        $scope.gridNoDN.tbody.find('.HasDNGridGroup').each(function () {
            var sort = parseInt($(this).attr('tabid'));
            var item = _OPSAppointment_DIRouteMaster.Data._dataHasDNSort[sort];
            var tr = $(this).closest('tr');
            if (item.TOMasterID <= 0) {
                $rootScope.Message({ Msg: 'Chỉ gửi Tender các chuyến đã lưu', NotifyType: Common.Message.NotifyType.ERROR });
                data = [];
                return false;
            }
            if (sort > 0 && item.TOMasterID > 0) {
                var chk = $(tr).find('.chkChooseVehicle');
                if (Common.HasValue(chk) && chk.prop('checked') == true)
                    data.push(item.TOMasterID);
            }
        });
        if (data.length > 0) {
            $($scope.OPSAppointment_DIRouteMaster_VendorRate_grid.element).find('tbody tr').each(function () {
                var cboVendor = $($(this).find('input.cboVendor.cus-combobox')[1]).data('kendoComboBox');
                var chkIsManual = $($(this).find('.chkIsManual')[0]);
                var txtDebit = $($(this).find('input.txtDebit.cus-number')[1]).data('kendoNumericTextBox');
                var item = $scope.OPSAppointment_DIRouteMaster_VendorRate_grid.dataItem(this);
                item.VendorID = cboVendor.value();
                item.IsManual = chkIsManual.prop('checked');
                item.Debit = txtDebit.value();
            });
            $rootScope.Message({
                Type: Common.Message.Type.Confirm,
                Msg: 'Bạn muốn gửi Tender các chuyến đã chọn ?',
                Ok: function () {
                    $rootScope.IsLoading = true;
                    Common.Services.Call($http, {
                        url: Common.Services.url.OPS,
                        method: _OPSAppointment_DIRouteMaster.URL.Tender,
                        data: { lst: data, lstTender: $scope.OPSAppointment_DIRouteMaster_VendorRate_grid.dataSource.data(), RateTime: $scope.RateTime },
                        success: function (res) {
                            Common.Services.Error(res, function (res) {
                                $scope.LoadData();
                                $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                                win.close();
                                // Gửi mail Tender
                                Common.Services.Call($http, {
                                    url: Common.Services.url.OPS,
                                    method: _OPSAppointment_DIRouteMaster.URL.SendMailToTender,
                                    data: { lst: res },
                                    success: function (res) {
                                        $rootScope.Message({ Msg: 'Đã gửi mail', NotifyType: Common.Message.NotifyType.SUCCESS });
                                    }
                                });
                            });
                        }
                    });
                }
            });
        }
    };

    $scope.TenderClose_Click = function ($event, win) {
        Common.Log('TenderClose_Click');
        $event.preventDefault();
        win.close();
    };
}]);