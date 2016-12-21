/// <reference path="~/Scripts/jquery-1.9.1.intellisense.js" />
/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />

//#region OPS object
var _OPSAppointment_DIRouteMasterFTL = {
    URL: {
        VehicleListVendor: 'OPS_DIAppointment_Route_VehicleListVendor',
        VehicleListVehicle: 'OPS_DIAppointment_Route_VehicleListVehicle',
        Delete: 'OPS_DIAppointment_Route_HasDNDelete',
        Approved: 'OPS_DIAppointment_Route_HasDNApproved',
        UnApproved: 'OPS_DIAppointment_Route_HasDNUnApproved',
        ChangeMode: 'DIAppointment_Route_Master_ChangeMode',
        Save: 'OPS_DIAppointment_Route_FTL_NoDNSave',
        List: 'OPS_DIAppointment_Route_FTL_NoDNList',
        OrderList: 'OPS_DIAppointment_Route_FTL_NoDNOrderList',
        CustomerList: 'OPS_DIAppointment_Route_CustomerList',
        VehicleListDriver: 'OPS_DIAppointment_Route_VehicleListDriver',
        VehicleAddRate: 'OPS_DIAppointment_Route_VehicleAddRate',
        VehicleTOVEN: 'OPS_DIAppointment_Route_VehicleTOVEN',
        VehicleTOVENGet: 'OPS_DIAppointment_Route_VehicleTOVENGet',
        VehicleList: 'OPS_DIAppointment_Route_VehicleList',
        VehicleTimeList: 'OPS_DIAppointment_Route_VehicleTimeList',
        VehicleTOVENList: 'OPS_DIAppointment_Route_VehicleTOVENList',
        Cancel: 'OPS_DIAppointment_Route_FTL_NoDNCancel',
        FLMPlaning: 'OPS_DIAppointment_FLMPlaning',
        Split: 'OPS_DIAppointment_Route_FTL_NoDNSplit'
    },
    Data: {
        CookieSearch: 'OPS_Appointment_DIRouteMasterFTL_Search',
        CookieVehicle: 'OPS_Appointment_DIRouteMasterFTL_Vehicle',
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

        _dataGop: [],
        _dataTOMaster: [],
        _dataTOMasterSort: [],
        _previousItemValues: {},
        _dataGopSplit: []
    },
    Timeline: null
};
//endregion OPS object
angular.module('myapp').controller('OPSAppointment_DIRouteMasterFTLCtrl', ['$rootScope', '$scope', '$http', '$location', '$state', '$timeout', function ($rootScope, $scope, $http, $location, $state, $timeout) {
    if ($rootScope.IsPageComplete != true) return;
    Common.Log('OPSAppointment_DIRouteMasterFTLCtrl');
    $rootScope.IsLoading = false;
    $scope.DNCode = "";
    $scope.IsShowTxtDN = true;
    $scope.IsShowCombine = false;
    $scope.IsHasDNMonitor = false;
    $scope.IsCreateByID = true;
    $scope.IsExpand = true;
    $scope.IsShowVehicleVendor = false;
    $scope.Search = {};
    $scope.Search.ListCustomerID = [];
    $scope.Vehicle = {};
    $scope.Summary = '';

    Common.Log($scope.mltCustomer)

    $scope.mltCustomerFTLOptions = {
        autoBind: false,
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
            Common.Cookie.Set(_OPSAppointment_DIRouteMasterFTL.Data.CookieSearch, JSON.stringify($scope.Search));
        }
    };

    // Load danh sách Vendor
    Common.Services.Call($http, {
        url: Common.Services.url.OPS,
        method: _OPSAppointment_DIRouteMasterFTL.URL.VehicleListVendor,
        data: {},
        success: function (res) {
            if (Common.HasValue(res)) {
                $timeout(function () {
                    _OPSAppointment_DIRouteMasterFTL.Data._dataHasDNVendor = res;
                    if (_OPSAppointment_DIRouteMasterFTL.Data._dataHasDNVendor.length > 0 && _OPSAppointment_DIRouteMasterFTL.Data._dataHasDNVehicle.length > 0)
                        $scope.LoadDataVehicleVendor();
                }, 1);
            }
        }
    });

    // Load danh sách xe
    Common.Services.Call($http, {
        url: Common.Services.url.OPS,
        method: _OPSAppointment_DIRouteMasterFTL.URL.VehicleListVehicle,
        data: {},
        success: function (res) {
            if (Common.HasValue(res)) {
                $timeout(function () {
                    _OPSAppointment_DIRouteMasterFTL.Data._dataHasDNVehicle = res;
                    _OPSAppointment_DIRouteMasterFTL.Data._dataVehicleVEN = res;
                    if (_OPSAppointment_DIRouteMasterFTL.Data._dataHasDNVendor.length > 0 && _OPSAppointment_DIRouteMasterFTL.Data._dataHasDNVehicle.length > 0)
                        $scope.LoadDataVehicleVendor();
                }, 1);
            }
        }
    });

    // Load danh sách khách hàng
    Common.Services.Call($http, {
        url: Common.Services.url.OPS,
        method: _OPSAppointment_DIRouteMasterFTL.URL.CustomerList,
        data: {},
        success: function (res) {
            if (Common.HasValue(res)) {
                $timeout(function () {
                    $scope.mltCustomerFTLOptions.dataSource.data(res);
                    $scope.Init_LoadCookie();
                }, 1);
            }
        }
    });

    // Load danh sách tài xế
    Common.Services.Call($http, {
        url: Common.Services.url.OPS,
        method: _OPSAppointment_DIRouteMasterFTL.URL.VehicleListDriver,
        success: function (res) {
            Common.Services.Error(res, function (res) {
                _OPSAppointment_DIRouteMasterFTL.Data._dataDriver = [];
                $.each(res, function (i, v) {
                    _OPSAppointment_DIRouteMasterFTL.Data._dataDriver.push({ 'ID': v.ID, 'Text': v.LastName + ' ' + v.FirstName + ' (' + v.EmployeeCode + ')', 'DriverName': v.LastName + ' ' + v.FirstName, 'DriverTelNo': v.Cellphone });
                });
            });
        }
    });

    // Load danh sách phân tài
    Common.Services.Call($http, {
        url: Common.Services.url.OPS,
        method: _OPSAppointment_DIRouteMasterFTL.URL.FLMPlaning,
        success: function (res) {
            _OPSAppointment_DIRouteMasterFTL.Data.FLMPlaning = res;
        }
    });

    $scope.LoadData = function () {
        Common.Log('LoadData');
        $rootScope.IsLoading = true;
        Common.Cookie.Set(_OPSAppointment_DIRouteMasterFTL.Data.CookieSearch, JSON.stringify($scope.Search));
        $scope.Summary = '';
        var i = 0;
        while (i < _OPSAppointment_DIRouteMasterFTL.Data._dataHasDNVendor.length) {
            if (_OPSAppointment_DIRouteMasterFTL.Data._dataHasDNVendor[i].ID < 0)
                _OPSAppointment_DIRouteMasterFTL.Data._dataHasDNVendor.splice(i, 1);
            else
                i++;
        }
        _OPSAppointment_DIRouteMasterFTL.Data._dataHasDNVendor.splice(0, 0, { 'ID': -1, 'CustomerName': 'Xe nhà' });
        _OPSAppointment_DIRouteMasterFTL.Data._dataHasDNVehicle = $.extend(true, [], _OPSAppointment_DIRouteMasterFTL.Data._dataVehicleVEN);
        var i = 0;
        while (i < _OPSAppointment_DIRouteMasterFTL.Data._dataHasDNVehicle.length) {
            if (_OPSAppointment_DIRouteMasterFTL.Data._dataHasDNVehicle[i].ID < 0)
                _OPSAppointment_DIRouteMasterFTL.Data._dataHasDNVehicle.splice(i, 1);
            else
                i++;
        }

        _OPSAppointment_DIRouteMasterFTL.Data._dataHasDNSort = [];
        _OPSAppointment_DIRouteMasterFTL.Data._dataHasDNGroupProduct = [];
        var lstFilters = [];
        lstFilters.push(Common.Request.FilterParamWithOr('TypeID', Common.Request.FilterType.Equal, 1));
        if ($scope.IsHasDNMonitor)
            lstFilters.push(Common.Request.FilterParamWithOr('TypeID', Common.Request.FilterType.Equal, 2));
        var param = Common.Request.Create({
            Sorts: [], Filters: lstFilters
        });
        Common.Services.Call($http, {
            url: Common.Services.url.OPS,
            method: _OPSAppointment_DIRouteMasterFTL.URL.List,
            data: { request: param, lstCustomerID: $scope.Search.ListCustomerID, DateFrom: $scope.Search.DateFrom, DateTo: $scope.Search.DateTo },
            success: function (res) {
                Common.Services.Error(res, function (res) {
                    _OPSAppointment_DIRouteMasterFTL.Data._dataHasDNSort[0] = { 'CreateSortOrder': 0, 'CreateVendorID': -1, 'CreateVehicleCode': '', 'CreateDriverName': '', 'CreateTelephone': '', 'Kg': 0 };
                    var datafix = [];
                    var hasMonitor = $scope.IsHasDNMonitor;
                    Common.Services.Call($http, {
                        url: Common.Services.url.OPS,
                        method: _OPSAppointment_DIRouteMasterFTL.URL.OrderList,
                        data: { request: param, lstCustomerID: $scope.Search.ListCustomerID, DateFrom: $scope.Search.DateFrom, DateTo: $scope.Search.DateTo },
                        success: function (res) {
                            Common.Services.Error(res, function (res) {
                                _OPSAppointment_DIRouteMasterFTL.Data._dataHasDN = [];
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
                                            _OPSAppointment_DIRouteMasterFTL.Data._dataHasDN.push(v);
                                    }
                                });
                                $scope.gridNoDNOptions.dataSource.data(_OPSAppointment_DIRouteMasterFTL.Data._dataHasDN);
                                $rootScope.IsLoading = false;
                            });
                        }
                    });

                    var index = 1;
                    $.each(res, function (i, v) {
                        if (hasMonitor || v.TypeID < 2) {
                            v.CreateDateTime = Common.Date.FromJson(v.CreateDateTime);
                            v.Kg = 0;
                            _OPSAppointment_DIRouteMasterFTL.Data._dataHasDNSort[index] = v;
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
        var strCookie = Common.Cookie.Get(_OPSAppointment_DIRouteMasterFTL.Data.CookieSearch);
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
            Common.Cookie.Set(_OPSAppointment_DIRouteMasterFTL.Data.CookieSearch, JSON.stringify($scope.Search));
            $scope.LoadData();
        }, 10);
    };

    //$scope.Init_LoadCookie();

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
            {
                field: 'CreateSortOrder', width: '50px', headerTemplate: '<a class="k-button" href="/" ng-click="Expand_Click($event,gridNoDN)"><i class="fa fa-minus"></i></a>', groupHeaderTemplate: '<span class="HasDNGridGroup" tabid="#=value#"></span>', sortable: false, filterable: false, menu: false,
                sortorder: 0, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'SOCode', width: '80px', title: 'SO', sortable: true, filterable: { cell: { operator: 'contains', showOperators: false } },
                sortorder: 1, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'IsOrigin', width: '30px', title: 'g', template: '<input type="checkbox" ng-model="dataItem.IsOrigin" />', sortable: false, filterable: false,
                sortorder: 2, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'DNCode', width: '90px', title: 'DN', sortable: true, filterable: { cell: { operator: 'contains', showOperators: false } },
                sortorder: 3, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'Kg', width: '80px', title: 'KG', template: '#=Common.Number.ToNumber1(Kg)#', sortable: true, filterable: { cell: { operator: 'eq', showOperators: false } },
                sortorder: 4, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'Ton', width: '80px', title: 'Tấn', template: '#=Common.Number.ToNumber3(Ton)#', sortable: true, filterable: { cell: { operator: 'eq', showOperators: false } },
                sortorder: 5, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'CBM', width: '80px', title: 'Khối', template: '#=Common.Number.ToNumber3(CBM)#', sortable: true, filterable: { cell: { operator: 'eq', showOperators: false } },
                sortorder: 6, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'Quantity', width: '80px', title: 'S.Lượng', template: '#=Common.Number.ToNumber1(Quantity)#', sortable: true, filterable: { cell: { operator: 'eq', showOperators: false } },
                sortorder: 7, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'DistrictName', width: '100px', title: 'Quận / Huyện', sortable: true, filterable: { cell: { operator: 'contains', showOperators: false } },
                sortorder: 8, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'Address', width: '200px', title: 'Địa chỉ', sortable: true, filterable: { cell: { operator: 'contains', showOperators: false } },
                sortorder: 9, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'ProvinceName', width: '100px', title: 'Tỉnh / Thành', sortable: true, filterable: { cell: { operator: 'contains', showOperators: false } },
                sortorder: 10, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'TranferItem', width: '80px', title: 'Vận chuyển', sortable: true, filterable: { cell: { operator: 'contains', showOperators: false } },
                sortorder: 11, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'RequestDateEmpty', width: '100px', title: 'Ngày gửi', template: "#=Common.Date.FromJsonDDMMHM(RequestDate)#", sortable: true, filterable: { cell: { operator: 'eq', showOperators: false } },
                sortorder: 12, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'OrderCode', width: '80px', title: 'List', sortable: true, filterable: { cell: { operator: 'contains', showOperators: false } },
                sortorder: 13, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'ETD', swidth: '100px', title: 'ETD', template: "#=Common.Date.FromJsonDDMMHM(ETD)#", sortable: true, filterable: { cell: { operator: 'eq', showOperators: false } },
                sortorder: 14, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'PartnerCode', width: '100px', title: 'Mã NPP', sortable: true, filterable: { cell: { operator: 'contains', showOperators: false } },
                sortorder: 15, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'PartnerName', width: '100px', title: 'NPP', sortable: true, filterable: { cell: { operator: 'contains', showOperators: false } },
                sortorder: 16, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'FromCode', width: '100px', title: 'Mã l.hàng', sortable: true, filterable: { cell: { operator: 'contains', showOperators: false } },
                sortorder: 17, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'FromAddress', width: '150px', title: 'Điểm l.hàng', sortable: true, filterable: { cell: { operator: 'contains', showOperators: false } },
                sortorder: 18, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'FromProvince', width: '100px', title: 'Tỉnh thành l.hàng', sortable: true, filterable: { cell: { operator: 'contains', showOperators: false } },
                sortorder: 19, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'FromDistrict', width: '100px', title: 'Quận huyện l.hàng', sortable: true, filterable: { cell: { operator: 'contains', showOperators: false } },
                sortorder: 20, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'Description', width: '100px', title: 'Ghi chú', sortable: true, filterable: { cell: { operator: 'contains', showOperators: false } },
                sortorder: 21, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'CUSRoutingCode', width: '180px', title: 'Cung đường', sortable: true, filterable: { cell: { operator: 'contains', showOperators: false } },
                sortorder: 22, configurable: true, isfunctionalHidden: false
            },
            { title: ' ', filterable: false, menu: false, sortable: false, sortorder: 100, configurable: false, isfunctionalHidden: false }
        ],
        //toolbar: kendo.template($('#OPSAppointment_DIRouteMasterFTL_gridToolbar').html()),
        filterable: { mode: 'row' },
        dataBound: function () {
            Common.Log("GridDataBound");
            if (Common.HasValue(this) && Common.HasValue(this.tbody)) {
                if (_OPSAppointment_DIRouteMasterFTL.Data._dataHasDNGroupProduct.length == 0)
                    $scope.ReloadSort();
                this.element.find('.HasDNGridGroup').each(function () {
                    var sort = parseInt($(this).attr('tabid'));
                    var item = _OPSAppointment_DIRouteMasterFTL.Data._dataHasDNSort[sort];
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
                                    '<input style="display:none;width:150px" type="text" class="txtHasDNDriverName" placeholder="Nhập tài xế" value="' + drivername + '" /><span style="display:none" class="lblHasDNSplit"> - </span>' +
                                    '<input style="display:none;width:150px" type="text" class="txtHasDNTelephone k-textbox" placeholder="Nhập điện thoại" value="' + telephone + '"/><span style="display:none" class="lblHasDNSplit"> - </span>' +
                                    '<input style="display:none;width:150px" class="txtCreateDateTime"  value="' + Common.Date.FromJsonDMYHM(item.CreateDateTime) + '"/>' + strMaxWeight + strOverLoad);
                            }
                            else {
                                $(this).html('&nbsp;&nbsp;&nbsp; Chuyến ' + sort + ' - Kg: ' + Common.Number.ToNumber1(item.Kg) + ' - ' +
                                    '<a href="" class="btnHasDNGroup">' + item.CreateVendorName + ' - ' + item.CreateVehicleCode + ' - ' + drivername + ' - ' + telephone + ' - ' + Common.Date.FromJsonDMYHM(item.CreateDateTime) + '</a>' +
                                    '<input style="display:none;width:200px" class="cboHasDNVendor cus-combobox" placeholder="Nhà vận tải" value="' + item.CreateVendorID + '" /><span style="display:none" class="lblHasDNSplit"> - </span>' +
                                    '<input style="display:none;width:200px" id="cboHasDNVehicle" class="cboHasDNVehicle cus-combobox" placeholder="Nhập số xe" value="' + item.CreateVehicleCode + '" /><span style="display:none" class="lblHasDNSplit"> - </span>' +
                                    '<input style="display:none;width:50px" type="number" min="0" class="txtHasDNMaxWeight k-textbox" placeholder="T.Tải" value="" /><span style="display:none" class="lblHasDNSplit vehicle"> - </span>' +
                                    '<input style="display:none;width:50px" type="number" min="0" class="txtHasDNMaxCBM k-textbox" placeholder="S.Khối" value="" /><span style="display:none" class="lblHasDNSplit vehicle"> - </span>' +
                                    '<input style="display:none;width:150px" type="text" class="txtHasDNDriverName" placeholder="Nhập tài xế" value="' + drivername + '" /><span style="display:none" class="lblHasDNSplit"> - </span>' +
                                    '<input style="display:none;width:150px" type="text" class="txtHasDNTelephone k-textbox" placeholder="Nhập điện thoại" value="' + telephone + '"/><span style="display:none" class="lblHasDNSplit"> - </span>' +
                                    '<input style="display:none;width:150px" class="txtCreateDateTime"  value="' + Common.Date.FromJsonDMYHM(item.CreateDateTime) + '"/>' + strMaxWeight + strOverLoad);
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

    $timeout(function () {
        //Common.Controls.Grid.ReorderColumns({ Grid: $scope.gridNoDN, CookieName: 'OPS_DIAppointment_RouteMasterFTL_gridNoDN' });
    }, 10);

    $scope.LoadDataVehicleVendor = function () {
        var lstVendor = _OPSAppointment_DIRouteMasterFTL.Data._dataHasDNVendor;
        var lstVehicle = _OPSAppointment_DIRouteMasterFTL.Data._dataHasDNVehicle;
        _OPSAppointment_DIRouteMasterFTL.Data._dataHasDNVehicleVEN = [];
        _OPSAppointment_DIRouteMasterFTL.Data._dataHasDNVehicleHome = [];
        $.each(lstVehicle, function (idx, vehicle) {
            if (vehicle.RegNo != "[Chờ nhập xe]") {
                for (var i = 0; i < vehicle.ListVendorID.length; i++) {
                    var vendorid = vehicle.ListVendorID[i];
                    if (!Common.HasValue(_OPSAppointment_DIRouteMasterFTL.Data._dataHasDNVehicleVEN[vendorid]))
                        _OPSAppointment_DIRouteMasterFTL.Data._dataHasDNVehicleVEN[vendorid] = [];
                    _OPSAppointment_DIRouteMasterFTL.Data._dataHasDNVehicleVEN[vendorid].push(vehicle);
                }
                if (vehicle.IsOwn) {
                    _OPSAppointment_DIRouteMasterFTL.Data._dataHasDNVehicleHome.push(vehicle);
                }
            } else {
                _OPSAppointment_DIRouteMasterFTL.Data._dataHasDNVehicleHome.push(vehicle);
                for (var i = 0; i < lstVendor.length; i++) {
                    var vendorid = lstVendor[i].ID;
                    if (!Common.HasValue(_OPSAppointment_DIRouteMasterFTL.Data._dataHasDNVehicleVEN[vendorid]))
                        _OPSAppointment_DIRouteMasterFTL.Data._dataHasDNVehicleVEN[vendorid] = [];
                    _OPSAppointment_DIRouteMasterFTL.Data._dataHasDNVehicleVEN[vendorid].push(vehicle);
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
        $.each(_OPSAppointment_DIRouteMasterFTL.Data._dataHasDNSort, function (i, v) {
            if (Common.HasValue(v)) {
                v.Kg = 0;
                v.CBM = 0;
                v.Quantity = 0;
                if (v.CreateSortOrder > 0)
                    totalsort++;
            }
        });
        _OPSAppointment_DIRouteMasterFTL.Data._dataHasDNGroupProduct = [];
        $.each(_OPSAppointment_DIRouteMasterFTL.Data._dataHasDN, function (i, v) {
            v.Kg = v.Kg > 0 ? Math.round(v.Kg * 1000) / 1000 : 0;
            v.Ton = v.Ton > 0 ? Math.round(v.Ton * 1000000) / 1000000 : 0;
            v.CBM = v.CBM > 0 ? Math.round(v.CBM * 1000) / 1000 : 0;
            v.Quantity = v.Quantity > 0 ? Math.round(v.Quantity * 1000) / 1000 : 0;
            var itemSort = _OPSAppointment_DIRouteMasterFTL.Data._dataHasDNSort[v.CreateSortOrder];
            if (!Common.HasValue(itemSort))
                itemSort = { 'CreateSortOrder': v.CreateSortOrder, 'CreateVendorID': v.VendorOfVehicleID, 'CreateVendorName': v.VendorOfVehicleName, 'CreateVehicleCode': v.VehicleCode, 'CreateDriverName': v.DriverName, 'CreateTelephone': v.DriverTelNo, 'Kg': v.Kg, 'CreateVehicleID': v.VehicleID, 'TOMasterID': v.TOMasterID, 'CreateDateTime': v.CreateDateTime, 'TypeID': v.TypeID, 'MaxWeight': v.MaxWeightCal, 'MaxCBM' : 0 };
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
            if (!Common.HasValue(_OPSAppointment_DIRouteMasterFTL.Data._dataHasDNGroupProduct[v.ID]))
                _OPSAppointment_DIRouteMasterFTL.Data._dataHasDNGroupProduct[v.ID] = [];
            _OPSAppointment_DIRouteMasterFTL.Data._dataHasDNGroupProduct[v.ID].push(v);
        });
        var ixdel = 0;
        while (ixdel < _OPSAppointment_DIRouteMasterFTL.Data._dataHasDNSort.length) {
            if (Common.HasValue(_OPSAppointment_DIRouteMasterFTL.Data._dataHasDNSort[ixdel])) {
                var itemSort = _OPSAppointment_DIRouteMasterFTL.Data._dataHasDNSort[ixdel];
                if (itemSort.Ton < 0.001 && itemSort.CBM < 0.001) {
                    _OPSAppointment_DIRouteMasterFTL.Data._dataHasDNSort.splice(ixdel, 1);
                }
                else
                    ixdel++;
            }
            else
                ixdel++;
        }
        $scope.Summary = 'Tấn: ' + Common.Number.ToNumber1(totaltoncomplete) + ' / ' + Common.Number.ToNumber1(totalton) + ' - Khối: ' + Common.Number.ToNumber1(totalcbmcomplete) + ' / ' + Common.Number.ToNumber1(totalcbm) + ' - Số chuyến: ' + totalsort;
    };

    $scope.ChangeMode_Click = function ($event) {
        Common.Log('ChangeMode_Click');
        $event.preventDefault();
        var data = [];
        $scope.gridNoDN.tbody.find('.HasDNGridGroup').each(function () {
            var sort = parseInt($(this).attr('tabid'));
            var item = _OPSAppointment_DIRouteMasterFTL.Data._dataHasDNSort[sort];
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
                Msg: 'Bạn muốn chuyển các chuyến đã chọn ?',
                Ok: function () {
                    $rootScope.IsLoading = true;
                    Common.Services.Call($http, {
                        url: Common.Services.url.OPS,
                        method: _OPSAppointment_DIRouteMasterFTL.URL.ChangeMode,
                        data: { data: data, fromFTL:true },
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

    $scope.Approved_Click = function ($event) {
        Common.Log('Approved_Click');
        $event.preventDefault();
        var data = []; var error = [];
        $scope.gridNoDN.tbody.find('.HasDNGridGroup').each(function () {
            var sort = parseInt($(this).attr('tabid'));
            var item = _OPSAppointment_DIRouteMasterFTL.Data._dataHasDNSort[sort];
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
                            method: _OPSAppointment_DIRouteMasterFTL.URL.Approved,
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

    $scope.UnApproved_Click = function ($event) {
        Common.Log('UnApproved_Click');
        $event.preventDefault();
        var data = [];
        $scope.gridNoDN.tbody.find('.HasDNGridGroup').each(function () {
            var sort = parseInt($(this).attr('tabid'));
            var item = _OPSAppointment_DIRouteMasterFTL.Data._dataHasDNSort[sort];
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
                        method: _OPSAppointment_DIRouteMasterFTL.URL.UnApproved,
                        data: { lst: data },
                        success: function (res) {
                            Common.Services.Error(res, function (res) {
                                if (res != '')
                                {
                                    $rootScope.Message({ Msg: res, NotifyType: Common.Message.NotifyType.SUCCESS });
                                }
                                else
                                {
                                    $scope.LoadData();
                                    $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                                } 
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
            var item = _OPSAppointment_DIRouteMasterFTL.Data._dataHasDNSort[sort];
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
                        method: _OPSAppointment_DIRouteMasterFTL.URL.Delete,
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
        var lstVehicle = [];
        $scope.gridNoDN.tbody.find('.chkChooseVehicle').each(function () {
            if (this.checked) {
                var tr = $(this).closest('.HasDNGridGroup');
                var sort = parseInt($(tr).attr('tabid'));
                var item = _OPSAppointment_DIRouteMasterFTL.Data._dataHasDNSort[sort];
                if (Common.HasValue(item)) {
                    lstVehicle.push(item);
                    if (item.CreateSortOrder > 0 && (item.CreateVehicleCode == '')) {
                        // || item.CreateVehicleCode == '[Chờ nhập xe]'
                        error += ', ' + item.CreateSortOrder;
                        flag = false;
                    }
                }
            }
        });
        if (flag) {
            if (lstVehicle.length > 0) {
                $rootScope.Message({
                    Type: Common.Message.Type.Confirm,
                    Msg: 'Bạn muốn lưu các chuyến đã lập?',
                    Ok: function () {
                        $rootScope.IsLoading = true;
                        Common.Services.Call($http, {
                            url: Common.Services.url.OPS,
                            method: _OPSAppointment_DIRouteMasterFTL.URL.Save,
                            data: { lstVehicle: lstVehicle },
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
        else
            $rootScope.Message({ Msg: 'Các chuyến ' + error.substr(2) + ' chưa chọn xe', NotifyType: Common.Message.NotifyType.ERROR });
    };

    $scope.Cancel_Click = function ($event) {
        Common.Log('Cancel_Click');
        $event.preventDefault();
        var flag = true;
        var error = '';
        var lstVehicle = [];
        $scope.gridNoDN.tbody.find('.chkChooseVehicle').each(function () {
            if (this.checked) {
                var tr = $(this).closest('.HasDNGridGroup');
                var sort = parseInt($(tr).attr('tabid'));
                var item = _OPSAppointment_DIRouteMasterFTL.Data._dataHasDNSort[sort];
                if (Common.HasValue(item)) {
                    lstVehicle.push(item);
                }
            }
        });
        if (flag) {
            if (lstVehicle.length > 0) {
                $rootScope.Message({
                    Type: Common.Message.Type.Confirm,
                    Msg: 'Bạn muốn trả về các chuyến đã lập ?',
                    Ok: function () {
                        $rootScope.IsLoading = true;
                        Common.Services.Call($http, {
                            url: Common.Services.url.OPS,
                            method: _OPSAppointment_DIRouteMasterFTL.URL.Cancel,
                            data: { lstVehicle: lstVehicle },
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
        else
            $rootScope.Message({ Msg: 'Các chuyến ' + error.substr(2) + ' thiếu thông tin', NotifyType: Common.Message.NotifyType.ERROR });
    };

    $scope.Close_Click = function ($event) {
        Common.Log('Close_Click');
        if (Common.HasValue($event))
            $event.preventDefault();
        _OPSAppointment_DIRouteMasterFTL.Data._createListDN = [];
        _OPSAppointment_DIRouteMasterFTL.Data._createListID = [];
        _OPSAppointment_DIRouteMasterFTL.Data._dataHasDNGroupProduct = [];
        _OPSAppointment_DIRouteMasterFTL.Data._dataHasDN = [];
        _OPSAppointment_DIRouteMasterFTL.Data._dataHasDNSort = [];
        _OPSAppointment_DIRouteMasterFTL.Data._dataHasDNVendor = [];
        _OPSAppointment_DIRouteMasterFTL.Data._dataHasDNVehicle = [];
        $state.go('main.OPSAppointment.DI');
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
        var sort = _OPSAppointment_DIRouteMasterFTL.Data._dataHasDNSort[item.CreateSortOrder];
        if (sort.CreateVendorID == null || sort.CreateVendorID == 0 || sort.CreateVendorID == -1) {
            dataVehicle = _OPSAppointment_DIRouteMasterFTL.Data._dataHasDNVehicleHome;
        } else {
            dataVehicle = _OPSAppointment_DIRouteMasterFTL.Data._dataHasDNVehicleVEN[sort.CreateVendorID];
        }
        tr.find('input.cboHasDNVendor').kendoComboBox({
            index: 0, autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true,
            dataTextField: 'CustomerName', dataValueField: 'ID',
            dataSource: Common.DataSource.Local({
                data: _OPSAppointment_DIRouteMasterFTL.Data._dataHasDNVendor,
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
                var item = _OPSAppointment_DIRouteMasterFTL.Data._dataHasDNSort[sort];
                var sel = e.sender.dataItem();
                var CreateVendorID = sel != null ? sel.ID : 0;
                if (item.CreateVendorID != CreateVendorID) {
                    item.CreateVendorID = sel != null ? sel.ID : 0;
                    var cboVehicle = $(span).find("#cboHasDNVehicle").data("kendoComboBox");
                    cboVehicle.dataSource.data([]);
                    var txtDriver = $(span).find("input.txtHasDNDriverName").data("kendoAutoComplete");
                    var dataDriver = [];
                    if (item.CreateVendorID == null || item.CreateVendorID == 0 || item.CreateVendorID == -1) {
                        cboVehicle.dataSource.data(_OPSAppointment_DIRouteMasterFTL.Data._dataHasDNVehicleHome);
                        dataDriver = _OPSAppointment_DIRouteMasterFTL.Data._dataDriver;
                    } else
                        cboVehicle.dataSource.data(_OPSAppointment_DIRouteMasterFTL.Data._dataHasDNVehicleVEN[item.CreateVendorID]);
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
                    var item = _OPSAppointment_DIRouteMasterFTL.Data._dataHasDNSort[sort];
                                        
                    var tmpMin, tmpMax;
                    $.each(_OPSAppointment_DIRouteMasterFTL.Data._dataHasDN, function (i, v) {
                        if (sort == v.CreateSortOrder) {
                            if (tmpMin == null || tmpMin == undefined)
                                tmpMin = v.TempMin;
                            else if (v.TempMin < tmpMin)
                                tmpMin = v.TempMin;

                            if (tmpMax == null || tmpMax == undefined)
                                tmpMax = v.TempMin;
                            else if (v.TempMax > tmpMax)
                                tmpMax = v.TempMax;
                        }
                    });
                    var flag = false;
                    $.each(_OPSAppointment_DIRouteMasterFTL.Data._dataHasDNVehicle, function (i, v) {
                        if (item.CreateVehicleCode == v.RegNo) {
                            if (tmpMax != null || tmpMax != undefined) {
                                if (v.TempMax == null || v.TempMax == undefined || v.TempMax < tmpMax)
                                    flag = true;
                            }
                            if (tmpMin != null || tmpMin != undefined) {
                                if (v.TempMin == null || v.TempMin == undefined || v.TempMin > tmpMin)
                                    flag = true;
                            }
                        }
                    });

                    if (flag) {
                        $rootScope.Message({
                            Msg: 'Không đáp ứng yêu cầu nhiệt độ.', Type: Common.Message.Type.Alert
                        })
                        e.sender.text(_OPSAppointment_DIRouteMasterFTL.Data._dataHasDNSort.CreateVehicleCode || "[Chờ nhập xe]");
                    }
                    else {
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
                        $.each(_OPSAppointment_DIRouteMasterFTL.Data._dataHasDNVehicle, function (i, v) {
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
                        if (!isHasVehicle && item.CreateVendorID > 0) {
                            txtHasDNMaxWeight.val('');
                            txtHasDNMaxCBM.val('');
                            txtHasDNMaxWeight.show();
                            txtHasDNMaxCBM.show();
                            $(span).find('.lblHasDNSplit.vehicle').show();
                        }
                    }
                }
            }).data('kendoComboBox');
            cbo.text(_OPSAppointment_DIRouteMasterFTL.Data._dataHasDNSort[item.CreateSortOrder].CreateVehicleCode);
        });

        tr.find('input.txtHasDNDriverName').kendoAutoComplete({
            autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true, placeholder: "Nhập tài xế",
            dataTextField: 'DriverName', dataValueField: 'DriverName',
            dataSource: Common.DataSource.Local({
                data: _OPSAppointment_DIRouteMasterFTL.Data._dataDriver,
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
                var item = _OPSAppointment_DIRouteMasterFTL.Data._dataHasDNSort[sort];
                item.CreateDriverName = this.value();
                item.IsChange = true;
            }
        });

        tr.find('input.txtHasDNMaxWeight').change(function (e) {
            var span = $(this).closest('span.HasDNGridGroup');
            sort = $(span).attr('tabid');
            var item = _OPSAppointment_DIRouteMasterFTL.Data._dataHasDNSort[sort];
            item.MaxWeight = $(this).val();
            item.IsChange = true;
        });

        tr.find('input.txtHasDNMaxCBM').change(function (e) {
            var span = $(this).closest('span.HasDNGridGroup');
            sort = $(span).attr('tabid');
            var item = _OPSAppointment_DIRouteMasterFTL.Data._dataHasDNSort[sort];
            item.MaxCBM = $(this).val();
            item.IsChange = true;
        });

        tr.find('input.txtHasDNTelephone').change(function (e) {
            var span = $(this).closest('span.HasDNGridGroup');
            sort = $(span).attr('tabid');
            var item = _OPSAppointment_DIRouteMasterFTL.Data._dataHasDNSort[sort];
            item.CreateTelephone = $(this).val();
            item.IsChange = true;
        });

        tr.find('input.txtCreateDateTime').kendoDateTimePicker({
            format: Common.Date.Format.DMYHM,
            timeFormat: Common.Date.Format.HM,
            change: function (e) {
                var span = $(e.sender.element).closest('span.HasDNGridGroup');
                sort = $(span).attr('tabid');
                var item = _OPSAppointment_DIRouteMasterFTL.Data._dataHasDNSort[sort];
                item.CreateDateTime = e.sender.value();
                item.IsChange = true;
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
                $.each(_OPSAppointment_DIRouteMasterFTL.Data.FLMPlaning, function (i, v) {
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
        Common.Cookie.Set(_OPSAppointment_DIRouteMasterFTL.Data.CookieSearch, JSON.stringify($scope.Search));
    }

    $scope.ShowTimeLine_Click = function ($event) {
        if ($scope.Search.IsShowTimeLine){
            $scope.Search.MyClass = "left3-4";
            $timeout(function () {
                $scope.OPSAppointment_DIRouteMasterFTL_grid.resize();
            }, 10);
        }
        else {
            $scope.Search.MyClass = "full";
        }
        Common.Cookie.Set(_OPSAppointment_DIRouteMasterFTL.Data.CookieSearch, JSON.stringify($scope.Search));
    }

    $scope.LTL_Click = function ($event) {
        $event.preventDefault();
        $state.go("main.OPSAppointment.DIRouteMaster");
    }

    //#region Vehicle
    $scope.OPSAppointment_DIRouteMasterFTL_Vehicle_splitterOptions = {
        orientation: "vertical",
        panes: [
            { collapsible: false, resizable: false, size: '40px' },
            { collapsible: false, resizable: true },
        ],
    };

    $scope.OPSAppointment_DIRouteMasterFTL_Vehicle_LoadLabel = function () {
        $scope.Vehicle.ConfigString = '';
        var lst = _OPSAppointment_DIRouteMasterFTL.Timeline.GetListRouteInDay();
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
            _OPSAppointment_DIRouteMasterFTL.Timeline.ChangeTime({
                search: {
                    DateFrom: $scope.Vehicle.DateFrom,
                    DateTo: $scope.Vehicle.DateTo,
                    HourFrom: $scope.Vehicle.HourFrom,
                    HourTo: $scope.Vehicle.HourTo
                }
            });
            // Set cookie
            Common.Cookie.Set(_OPSAppointment_DIRouteMasterFTL.Data.CookieVehicle, JSON.stringify($scope.Vehicle));
            // Resize
            $scope.OPSAppointment_DIRouteMasterFTL_Vehicle_splitter.size(".k-pane:first", "35px");
        } else {
            $scope.OPSAppointment_DIRouteMasterFTL_Vehicle_splitter.size(".k-pane:first", "70px");
        }
        $scope.OPSAppointment_DIRouteMasterFTL_Vehicle_LoadLabel();
    };

    $timeout(function () {
        _OPSAppointment_DIRouteMasterFTL.Timeline = Common.Timeline({
            grid: $scope.OPSAppointment_DIRouteMasterFTL_grid,
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
                        method: _OPSAppointment_DIRouteMasterFTL.URL.VehicleList,
                        success: function (res) {
                            Common.Services.Error(res, function (res) {
                                _OPSAppointment_DIRouteMasterFTL.Data._dataVehicle = [];
                                $.each(res, function (i, v) {
                                    v.VendorName = 'Xe nhà';
                                    v.VehicleID = v.ID;
                                    _OPSAppointment_DIRouteMasterFTL.Data._dataVehicle.push(v);
                                });
                                res.splice(0, 0, { 'ID': -1, 'RegNo': ' Xe ngoài', 'VehicleID': -1, 'MaxWeightCal': null, 'GroupOfVehicleID': null, 'GroupOfVehicleName': '', 'VendorName': '' });
                                _OPSAppointment_DIRouteMasterFTL.Timeline.SetMainData(res);
                            });
                        }
                    });
                } else {
                    Common.Services.Call($http, {
                        url: Common.Services.url.OPS,
                        method: _OPSAppointment_DIRouteMasterFTL.URL.VehicleTOVENList,
                        success: function (res) {
                            Common.Services.Error(res, function (res) {
                                var data = res;
                                $.each(data, function (i, v) {
                                    v.MaxWeightCal = null;
                                    v.GroupOfVehicleID = null;
                                    v.GroupOfVehicleName = '';
                                });
                                res.splice(0, 0, { 'ID': -1, 'RegNo': '[ Chờ xác nhận ]', 'MaxWeightCal': null, 'GroupOfVehicleID': null, 'GroupOfVehicleName': '', 'VendorName': '' });
                                _OPSAppointment_DIRouteMasterFTL.Timeline.SetMainData(res);
                            });
                        }
                    });
                }
            },
            eventDetailData: function (dtFrom, dtTo) {
                Common.Log('VehicleDetailData');
                if (!$scope.IsShowVehicleVendor) {
                    var lst = _OPSAppointment_DIRouteMasterFTL.Timeline.GetListRouteInDay();
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
                        method: _OPSAppointment_DIRouteMasterFTL.URL.VehicleTimeList,
                        data: { request: param },
                        success: function (res) {
                            Common.Services.Error(res, function (res) {
                                $.each(res, function (i, v) {
                                    v.DateFrom = Common.Date.FromJson(v.DateFrom);
                                    v.DateTo = Common.Date.FromJson(v.DateTo);
                                });
                                _OPSAppointment_DIRouteMasterFTL.Timeline.SetDetailData(res);
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
                        method: _OPSAppointment_DIRouteMasterFTL.URL.VehicleTOVEN,
                        data: { request: param },
                        success: function (res) {
                            Common.Services.Error(res, function (res) {
                                $.each(res, function (i, v) {
                                    v.DateFrom = Common.Date.FromJson(v.DateFrom);
                                    v.DateTo = Common.Date.FromJson(v.DateTo);
                                });
                                _OPSAppointment_DIRouteMasterFTL.Timeline.SetDetailData(res);
                            });
                        }
                    });
                }
            },
        });
        _OPSAppointment_DIRouteMasterFTL.Timeline.Init();
    }, 500);

    $scope.OPSAppointment_DIRouteMasterFTL_VehicleMenuOptions = {
        orientation: 'vertical',
        target: '#OPSAppointment_DIRouteMasterFTL_grid',
        filter: 'tr',
        animation: { open: { effects: 'fadeIn' }, duration: 300 },
        open: function (e) {
            if ($scope.IsShowVehicleVendor)
                $($scope.OPSAppointment_DIRouteMasterFTL_VehicleMenu.element.find(".lblVehicleList")).html('Hiện danh sách xe nhà');
            else
                $($scope.OPSAppointment_DIRouteMasterFTL_VehicleMenu.element.find(".lblVehicleList")).html('Hiện danh sách xe ngoài');
        },
        select: function (e) {
            var index = $(e.item).index();
            switch (index) {
                case 0:
                    $scope.IsShowVehicleVendor = !$scope.IsShowVehicleVendor;
                    _OPSAppointment_DIRouteMasterFTL.Timeline.RefreshMain();
                    break;
                case 1:
                    $scope.IsShowVehicleVendor = !$scope.IsShowVehicleVendor;
                    _OPSAppointment_DIRouteMasterFTL.Timeline.RefreshMain();
                    break;
                case 3:
                    break;
                case 4:
                    break;
                case 5:
                    break;
            }
        },
    };

    //#region Action
    $scope.Close_Click = function ($event, win) {
        $event.preventDefault();

        win.close();
    }

    $scope.ShowSetting = function ($event, grid) {
        $event.preventDefault();

        $rootScope.ShowSetting({
            ListView: views.OPSAppointment,
            event: $event,
            grid: grid,
            current: $state.current
        });
    };
    //#endregion
    
    //#region Tách chuyến

    $scope.TOMasterSplitID = -1;

    $scope.split_gridOptions = {
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
            {
                field: 'CreateSortOrder', width: '50px',
                template: '<form class="cus-form-enter" ng-submit="SortEnter_Click($event)"><input kendo-numeric-text-box class="txtCreateSortOrder" value="#=CreateSortOrder>0?CreateSortOrder:0#" data-k-min="0" k-options="numericSortOrderOptions" style="width:100%" /></form>',
                headerTemplate: '<a class="k-button" href="/" ng-click="Expand_Click($event,split_grid)"><i class="fa fa-minus"></i></a>',
                groupHeaderTemplate: '<span class="HasSplitGridGroup" tabid="#=value#"></span>', sortable: false, filterable: false, menu: false
            },
            { field: 'SOCode', width: '80px', title: 'SO', sortable: true, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'IsOrigin', width: '30px', title: 'g', template: '<input type="checkbox" ng-model="dataItem.IsOrigin" />', sortable: false, filterable: false },
            { field: 'DNCode', width: '90px', title: 'DN', sortable: true, filterable: { cell: { operator: 'contains', showOperators: false } } },
            {
                field: 'Merge', title: 'Gộp', width: '45px', attributes: { style: "text-align: center; " },
                template: '<a href="" class="k-button btnAdd" ng-click="Merge_Click($event, split_grid)" style="display:none">M</a><a href="" class="k-button btnAddSave" ng-click="MergeSave_Click($event, split_grid)" style="display:none">S</a><input type="checkbox" class="chkAdd" style="display:none"/>', filterable: false, menu: false, sortable: false
            },
            { field: 'Kg', width: '80px', title: 'KG', template: '# if(TypeEditID == 1 && TypeID == 1){# <form class="cus-form-enter"><input kendo-numeric-text-box value="#:Kg#" k-options="numericKgOptions" data-k-max="#:Kg#" style="width:100%" /></form> #}else{# #:Kg# #}#', sortable: true, filterable: { cell: { operator: 'gte', showOperators: false } } },
            { field: 'Ton', width: '80px', title: 'Tấn', template: '# if(TypeEditID == 1 && TypeID == 1){# <form class="cus-form-enter"><input kendo-numeric-text-box value="#:Ton#" k-options="numericTonOptions" data-k-max="#:Ton#" style="width:100%" /></form> #}else{# #:Ton# #}#', sortable: true, filterable: { cell: { operator: 'gte', showOperators: false } } },
            { field: 'CBM', width: '80px', title: 'Khối', template: '# if(TypeEditID == 2 && TypeID == 1){# <form class="cus-form-enter"><input kendo-numeric-text-box value="#:CBM#" k-options="numericCBMOptions" data-k-max="#:CBM#" style="width:100%" /></form> #}else{# #:CBM# #}#', sortable: true, filterable: { cell: { operator: 'gte', showOperators: false } } },
            { field: 'Quantity', width: '90px', title: 'S.Lượng', template: '# if(TypeEditID == 3 && TypeID == 1){# <form class="cus-form-enter"><input kendo-numeric-text-box value="#:Quantity#" k-options="numericQuantityOptions" data-k-max="#:Quantity#" style="width:100%" /></form> #}else{# #:Quantity# #}#', sortable: true, filterable: { cell: { operator: 'gte', showOperators: false } } },
            
            //{ field: 'Kg', width: '80px', title: 'KG', template: '#=Common.Number.ToNumber1(Kg)#', sortable: true, filterable: { cell: { operator: 'eq', showOperators: false } } },
            //{ field: 'Ton', width: '80px', title: 'Tấn', template: '#=Common.Number.ToNumber3(Ton)#', sortable: true, filterable: { cell: { operator: 'eq', showOperators: false } } },
            //{ field: 'CBM', width: '80px', title: 'Khối', template: '#=Common.Number.ToNumber3(CBM)#', sortable: true, filterable: { cell: { operator: 'eq', showOperators: false } } },
            //{ field: 'Quantity', width: '80px', title: 'S.Lượng', template: '#=Common.Number.ToNumber1(Quantity)#', sortable: true, filterable: { cell: { operator: 'eq', showOperators: false } } },

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
        filterable: { mode: 'row' },
        dataBound: function () {
            Common.Log("GridDataBound");
            if (Common.HasValue(this) && Common.HasValue(this.tbody)) {
                this.element.find('tr[role="row"]').each(function () {
                    try {
                        var dataItem = $scope.split_grid.dataItem(this);
                        if (Common.HasValue(dataItem) && Common.HasValue(_OPSAppointment_DIRouteMasterFTL.Data._dataGopSplit[dataItem.ID])) {
                            if (_OPSAppointment_DIRouteMasterFTL.Data._dataGopSplit[dataItem.ID].length > 1) {
                                var btnAdd = $(this).find('.btnAdd');
                                btnAdd.show();
                            }
                        }
                    } catch (e) {

                    }
                });

                if (_OPSAppointment_DIRouteMasterFTL.Data._dataGop.length == 0)
                    $scope.ReloadSort();
                this.element.find('.HasSplitGridGroup').each(function () {
                    var sort = parseInt($(this).attr('tabid'));
                    var item = _OPSAppointment_DIRouteMasterFTL.Data._dataTOMaster[sort];
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

                            $(this).html('&nbsp;&nbsp;&nbsp; Chuyến ' + sort + ' - Kg: ' + Common.Number.ToNumber1(item.Kg) + ' - ' +
                                   '<a href="" class="btnSplitGroup">' + item.CreateVendorName + ' - ' + item.CreateVehicleCode + ' - ' + drivername + ' - ' + telephone + ' - ' + Common.Date.FromJsonDMYHM(item.CreateDateTime) + '</a>' +
                                   '<input style="display:none;width:200px" class="cboSplitVendor cus-combobox" placeholder="Nhà vận tải" value="' + item.CreateVendorID + '" /><span style="display:none" class="lblSplitSplit"> - </span>' +
                                   '<input style="display:none;width:200px" id="cboSplitVehicle" class="cboSplitVehicle cus-combobox" placeholder="Nhập số xe" value="' + item.CreateVehicleCode + '" /><span style="display:none" class="lblSplitSplit"> - </span>' +
                                   '<input style="display:none;width:50px" type="number" min="0" class="txtSplitMaxWeight k-textbox" placeholder="T.Tải" value="" /><span style="display:none" class="lblSplitSplit vehicle"> - </span>' +
                                   '<input style="display:none;width:50px" type="number" min="0" class="txtSplitMaxCBM k-textbox" placeholder="S.Khối" value="" /><span style="display:none" class="lblSplitSplit vehicle"> - </span>' +
                                   '<input style="display:none;width:150px" type="text" class="txtSplitDriverName" placeholder="Nhập tài xế" value="' + drivername + '" /><span style="display:none" class="lblSplitSplit"> - </span>' +
                                   '<input style="display:none;width:150px" type="text" class="txtSplitTelephone k-textbox" placeholder="Nhập điện thoại" value="' + telephone + '"/><span style="display:none" class="lblSplitSplit"> - </span>' +
                                   '<input style="display:none;width:150px" class="txtCreateDateTime"  value="' + Common.Date.FromJsonDMYHM(item.CreateDateTime) + '"/>' + strMaxWeight + strOverLoad);
                        }
                    }
                    else
                        $(this).html('Kg: ' + Common.Number.ToNumber1(item.Kg));
                });
                this.element.find('.HasSplitGridGroup .btnSplitGroup').click(function (e) {
                    e.preventDefault();
                    $scope.Split_Group_Click(this);
                });
            }
        }
    }
    
    $scope.ConvertToFix3 = function (val) {
        return val > 0 ? Math.round(val * 1000) / 1000 : 0;
    }

    $scope.ConvertToFix6 = function (val) {
        return val > 0 ? Math.round(val * 1000000) / 1000000 : 0;
    }
        
    $scope.numericKgOptions = {
        min: 0,
        spinners: false,
        decimals: Common.Number.DI_Decimals,
        culture: "en-US",
        format: "n3",
        change: function (e) {
            var tr = $(e.sender.element).closest('tr');
            var dataItem = $scope.split_grid.dataItem(tr);
            _OPSAppointment_DIRouteMasterFTL.Data._previousItemValues = { Ton: dataItem.Ton, CBM: dataItem.CBM };
            var sub = dataItem.Kg - this.value();
            if (sub > 0.001) {
                var itemsub = angular.copy(dataItem);
                itemsub.Kg = sub;
                itemsub.Ton = sub / 1000;
                itemsub.IsOrigin = false;
                dataItem.Kg = this.value();
                dataItem.Ton = dataItem.Kg / 1000;
                dataItem.CBM = 0;
                dataItem.Quantity = 0;
                if (dataItem.ExchangeTon > 0) {
                    itemsub.CBM = $scope.ConvertToFix3(dataItem.ExchangeCBM * itemsub.Ton / dataItem.ExchangeTon);
                    itemsub.Quantity = $scope.ConvertToFix3(dataItem.ExchangeQuantity * itemsub.Ton / dataItem.ExchangeTon);
                    dataItem.CBM = $scope.ConvertToFix3(dataItem.ExchangeCBM * dataItem.Ton / dataItem.ExchangeTon);
                    dataItem.Quantity = $scope.ConvertToFix3(dataItem.ExchangeQuantity * dataItem.Ton / dataItem.ExchangeTon);
                }
                _OPSAppointment_DIRouteMasterFTL.Data._dataGop = $scope.split_grid.dataSource.data();
                var index = _OPSAppointment_DIRouteMasterFTL.Data._dataGop.indexOf(dataItem);
                _OPSAppointment_DIRouteMasterFTL.Data._dataGop.splice(index, 0, itemsub.toJSON());

                itemsub = $scope.split_grid.dataSource.data()[index];

                if (!Common.HasValue(_OPSAppointment_DIRouteMasterFTL.Data._dataGopSplit[dataItem.ID]))
                    _OPSAppointment_DIRouteMasterFTL.Data._dataGopSplit[dataItem.ID] = [dataItem];
                _OPSAppointment_DIRouteMasterFTL.Data._dataGopSplit[dataItem.ID].push(itemsub);

                dataItem.set("TimeModified", Date.now());
            }
        }
    };

    $scope.numericTonOptions = {
        min: 0,
        spinners: false,
        decimals: Common.Number.DI_Decimals,
        culture: "en-US",
        format: "n5",
        change: function (e) {
            //set scroll
            var h = $scope.split_grid.wrapper.find('.k-grid-content').scrollTop();
            Common.Cookie.Set("Scroll", h);

            var tr = $(e.sender.element).closest('tr');
            var dataItem = $scope.split_grid.dataItem(tr);
            _OPSAppointment_DIRouteMasterFTL.Data._previousItemValues = { Ton: dataItem.Ton, CBM: dataItem.CBM };
            var sub = dataItem.Ton - this.value();
            if (sub > 0.001) {
                var itemsub = angular.copy(dataItem);
                itemsub.Ton = sub;
                itemsub.Kg = sub * 1000;
                itemsub.IsOrigin = false;
                dataItem.Ton = this.value();
                dataItem.Kg = dataItem.Ton * 1000;
                dataItem.CBM = 0;
                dataItem.Quantity = 0;
                if (dataItem.ExchangeTon > 0) {
                    itemsub.CBM = $scope.ConvertToFix3(dataItem.ExchangeCBM * itemsub.Ton / dataItem.ExchangeTon);
                    itemsub.Quantity = $scope.ConvertToFix3(dataItem.ExchangeQuantity * itemsub.Ton / dataItem.ExchangeTon);
                    dataItem.CBM = $scope.ConvertToFix3(dataItem.ExchangeCBM * dataItem.Ton / dataItem.ExchangeTon);
                    dataItem.Quantity = $scope.ConvertToFix3(dataItem.ExchangeQuantity * dataItem.Ton / dataItem.ExchangeTon);
                }
                _OPSAppointment_DIRouteMasterFTL.Data._dataGop = $scope.split_grid.dataSource.data();
                var index = _OPSAppointment_DIRouteMasterFTL.Data._dataGop.indexOf(dataItem);
                _OPSAppointment_DIRouteMasterFTL.Data._dataGop.splice(index, 0, itemsub.toJSON());

                itemsub = $scope.split_grid.dataSource.data()[index];

                if (!Common.HasValue(_OPSAppointment_DIRouteMasterFTL.Data._dataGopSplit[dataItem.ID]))
                    _OPSAppointment_DIRouteMasterFTL.Data._dataGopSplit[dataItem.ID] = [dataItem];
                _OPSAppointment_DIRouteMasterFTL.Data._dataGopSplit[dataItem.ID].push(itemsub);

                dataItem.set("TimeModified", Date.now());
            }
        }
    };

    $scope.numericCBMOptions = {
        min: 0,
        spinners: false,
        decimals: Common.Number.DI_Decimals,
        culture: "en-US",
        format: "n3",
        change: function (e) {
            //set scroll
            var h = $scope.split_grid.wrapper.find('.k-grid-content').scrollTop();
            Common.Cookie.Set("Scroll", h);

            var tr = $(e.sender.element).closest('tr');
            var dataItem = $scope.split_grid.dataItem(tr);
            _OPSAppointment_DIRouteMasterFTL.Data._previousItemValues = { Ton: dataItem.Ton, CBM: dataItem.CBM };
            var sub = dataItem.CBM - this.value();
            if (sub > 0.001) {
                var itemsub = angular.copy(dataItem);
                itemsub.CBM = sub;
                itemsub.IsOrigin = false;
                dataItem.CBM = this.value();
                dataItem.Ton = 0;
                dataItem.Quantity = 0;
                if (dataItem.ExchangeCBM > 0) {
                    itemsub.Ton = $scope.ConvertToFix6(dataItem.ExchangeTon * itemsub.CBM / dataItem.ExchangeCBM);
                    itemsub.Quantity = $scope.ConvertToFix3(dataItem.ExchangeQuantity * itemsub.CBM / dataItem.ExchangeCBM);
                    dataItem.Ton = $scope.ConvertToFix6(dataItem.ExchangeTon * dataItem.CBM / dataItem.ExchangeCBM);
                    dataItem.Quantity = $scope.ConvertToFix3(dataItem.ExchangeQuantity * dataItem.CBM / dataItem.ExchangeCBM);
                }
                dataItem.Kg = dataItem.Ton * 1000;
                itemsub.Kg = itemsub.Ton * 1000;
                _OPSAppointment_DIRouteMasterFTL.Data._dataGop = $scope.split_grid.dataSource.data();
                var index = _OPSAppointment_DIRouteMasterFTL.Data._dataGop.indexOf(dataItem);
                _OPSAppointment_DIRouteMasterFTL.Data._dataGop.splice(index, 0, itemsub.toJSON());

                itemsub = $scope.split_grid.dataSource.data()[index];

                if (!Common.HasValue(_OPSAppointment_DIRouteMasterFTL.Data._dataGopSplit[dataItem.ID]))
                    _OPSAppointment_DIRouteMasterFTL.Data._dataGopSplit[dataItem.ID] = [dataItem];
                _OPSAppointment_DIRouteMasterFTL.Data._dataGopSplit[dataItem.ID].push(itemsub);

                dataItem.set("TimeModified", Date.now());
            }
        }
    };

    $scope.numericQuantityOptions = {
        min: 0,
        spinners: false,
        decimals: Common.Number.DI_Decimals,
        culture: "en-US",
        format: "n3",
        change: function (e) {
            //set scroll
            var h = $scope.split_grid.wrapper.find('.k-grid-content').scrollTop();
            Common.Cookie.Set("Scroll", h);

            var tr = $(e.sender.element).closest('tr');
            var dataItem = $scope.split_grid.dataItem(tr);
            _OPSAppointment_DIRouteMasterFTL.Data._previousItemValues = { Ton: dataItem.Ton, CBM: dataItem.CBM };
            var sub = dataItem.Quantity - this.value();
            if (sub > 0.001) {
                var itemsub = angular.copy(dataItem);
                itemsub.Quantity = sub;
                itemsub.IsOrigin = false;
                dataItem.Quantity = this.value();
                dataItem.Ton = 0;
                dataItem.CBM = 0;
                if (dataItem.ExchangeQuantity > 0) {
                    itemsub.Ton = $scope.ConvertToFix6(dataItem.ExchangeTon * itemsub.Quantity / dataItem.ExchangeQuantity);
                    itemsub.CBM = $scope.ConvertToFix3(dataItem.ExchangeCBM * itemsub.Quantity / dataItem.ExchangeQuantity);
                    dataItem.Ton = $scope.ConvertToFix6(dataItem.ExchangeTon * dataItem.Quantity / dataItem.ExchangeQuantity);
                    dataItem.CBM = $scope.ConvertToFix3(dataItem.ExchangeCBM * dataItem.Quantity / dataItem.ExchangeQuantity);
                }
                _OPSAppointment_DIRouteMasterFTL.Data._dataGop = $scope.split_grid.dataSource.data();
                var index = _OPSAppointment_DIRouteMasterFTL.Data._dataGop.indexOf(dataItem);
                _OPSAppointment_DIRouteMasterFTL.Data._dataGop.splice(index, 0, itemsub.toJSON());

                itemsub = $scope.split_grid.dataSource.data()[index];

                if (!Common.HasValue(_OPSAppointment_DIRouteMasterFTL.Data._dataGopSplit[dataItem.ID]))
                    _OPSAppointment_DIRouteMasterFTL.Data._dataGopSplit[dataItem.ID] = [dataItem];
                _OPSAppointment_DIRouteMasterFTL.Data._dataGopSplit[dataItem.ID].push(itemsub);

                dataItem.set("TimeModified", Date.now());
            }
        }
    };
    
    $scope.IsShowUnMerge = false;

    $scope.Merge_Click = function ($event, grid) {
        Common.Log("Merge_Click");
        $event.preventDefault();
        // Ẩn các nút Merge
        grid.tbody.find('tr[role="row"] .btnAdd').hide();
        // Hiện nút Save
        var btnAddSave = $($($event.currentTarget).closest('tr')).find('.btnAddSave').show();
        // Hiện nút Hủy gom hàng
        $scope.IsShowUnMerge = true;
        // Lấy ra dataItem hiện tại
        var dataItem = this.dataItem;
        $.each(_OPSAppointment_DIRouteMasterFTL.Data._dataGopSplit[dataItem.ID], function (i, v) {
            if (v.uid != dataItem.uid) {
                var tr = grid.tbody.find("tr[data-uid='" + v.uid + "']");
                var chkAdd = $(tr).find('.chkAdd');
                $(chkAdd).show();
            }
        });
    };

    $scope.MergeSave_Click = function ($event, grid) {
        Common.Log("MergeSave_Click");
        $event.preventDefault();
        var dataItem = this.dataItem;
        _OPSAppointment_DIRouteMasterFTL.Data._previousItemValues = { Ton: dataItem.Ton, CBM: dataItem.CBM };
        var groupItems = $.extend(true, [], _OPSAppointment_DIRouteMasterFTL.Data._dataGopSplit[dataItem.ID]);
        $.each(groupItems, function (i, v) {
            if (v.uid != dataItem.uid) {
                var tr = grid.tbody.find("tr[data-uid='" + v.uid + "']");
                var chkAdd = $(tr).find('.chkAdd');
                if ($(chkAdd).prop('checked') == true) {
                    if (dataItem.TypeEditID == 1) {
                        dataItem.Kg += v.Kg;
                        dataItem.Ton += v.Kg / 1000;
                        if (dataItem.ExchangeTon > 0) {
                            dataItem.CBM += $scope.ConvertToFix3(dataItem.ExchangeCBM * v.Ton / dataItem.ExchangeTon);
                            dataItem.Quantity += $scope.ConvertToFix3(dataItem.ExchangeQuantity * v.Ton / dataItem.ExchangeTon);
                        }
                    } else {
                        if (dataItem.TypeEditID == 2) {
                            dataItem.CBM += v.CBM;
                            if (dataItem.ExchangeCBM > 0) {
                                dataItem.Ton += $scope.ConvertToFix6(dataItem.ExchangeCBM * v.CBM / dataItem.ExchangeCBM);
                                dataItem.Kg += dataItem.Ton * 1000;
                                dataItem.Quantity += $scope.ConvertToFix3(dataItem.ExchangeQuantity * v.CBM / dataItem.ExchangeCBM);
                            }
                        } else {
                            if (dataItem.TypeEditID == 3) {
                                dataItem.Quantity += v.Quantity;
                                if (dataItem.ExchangeQuantity > 0) {
                                    dataItem.Ton = $scope.ConvertToFix6(dataItem.ExchangeTon * v.Quantity / dataItem.ExchangeQuantity);
                                    dataItem.Kg += dataItem.Ton * 1000;
                                    dataItem.CBM = $scope.ConvertToFix3(dataItem.ExchangeCBM * v.Quantity / dataItem.ExchangeQuantity);
                                }
                            }
                        }
                    }
                    var indexA = _OPSAppointment_DIRouteMasterFTL.Data._dataGopSplit[dataItem.ID].indexOf(v);
                    _OPSAppointment_DIRouteMasterFTL.Data._dataGopSplit[dataItem.ID].splice(indexA, 1);

                    var index = _OPSAppointment_DIRouteMasterFTL.Data._dataGop.indexOf(v);
                    _OPSAppointment_DIRouteMasterFTL.Data._dataGop.splice(index, 1);

                }
            }
        });
        // Raise change event for this item to recalculate the sum
        dataItem.set("TimeModified", Date.now());
    };

    $scope.Merge_Cancel_Click = function ($event, grid) {
        Common.Log("MergeCancel_Click");
        $event.preventDefault();
        grid.dataSource.data(_OPSAppointment_DIRouteMasterFTL.Data._dataGop);
        $scope.IsShowCombine = false;
    };
    
    $scope.Split_Click = function ($event, win) {
        $event.preventDefault();

        var flag = true;
        var temp = [];
        $scope.gridNoDN.tbody.find('.chkChooseVehicle').each(function () {
            if (this.checked) {
                var sort = parseInt($(this).closest('.HasDNGridGroup').attr('tabid'));
                temp.push(sort);
            }
        });

        if (temp.length != 1) {
            $rootScope.Message({ Msg: 'Chọn 1 chuyến để chia.', NotifyType: Common.Message.NotifyType.WARNING });
            return;
        }

        _OPSAppointment_DIRouteMasterFTL.Data._dataGop = [];
        _OPSAppointment_DIRouteMasterFTL.Data._dataTOMaster = [];
        _OPSAppointment_DIRouteMasterFTL.Data._dataGopSplit = [];

        var sort = temp[0];
        var item = $.extend({}, true, _OPSAppointment_DIRouteMasterFTL.Data._dataHasDNSort[sort]);
        $scope.TOMasterSplitID = item.TOMasterID;

        item.CreateSortOrder = 1;
        _OPSAppointment_DIRouteMasterFTL.Data._dataTOMaster[1] = item;
        _OPSAppointment_DIRouteMasterFTL.Data._dataTOMaster[0] = { 'CreateSortOrder': 0, 'CreateVendorID': -1, 'CreateVehicleCode': '', 'CreateDriverName': '', 'CreateTelephone': '', 'Kg': 0, 'Quantity': 0, 'Credit': 0, 'Debit': 0, 'PL': 0 };

        Common.Data.Each(_OPSAppointment_DIRouteMasterFTL.Data._dataHasDN, function (o) {
            if (o.CreateSortOrder == sort) {
                var obj = $.extend({}, true, o);
                obj.CreateSortOrder = 1;
                _OPSAppointment_DIRouteMasterFTL.Data._dataGop.push(obj);
            }
        })

        win.center().open();
        $timeout(function () {
            $scope.split_gridOptions.dataSource.data(_OPSAppointment_DIRouteMasterFTL.Data._dataGop);
        }, 100)
    }
    
    $scope.Split_Group_Click = function (item) {
        Common.Log('Split_Group_Click');
        $(item).hide();
        var tr = $(item).closest('tr');
        $(tr).find('.lblSplitSplit,input').show();
        $(tr).find('.txtSplitMaxWeight,.txtSplitMaxCBM').hide();
        $(tr).find('.lblSplitSplit.vehicle').hide();
        var item = $scope.split_grid.dataItem(tr);
        var dataVehicle = [];
        var sort = _OPSAppointment_DIRouteMasterFTL.Data._dataTOMaster[item.CreateSortOrder];
        if (sort.CreateVendorID == null || sort.CreateVendorID == 0 || sort.CreateVendorID == -1) {
            dataVehicle = _OPSAppointment_DIRouteMasterFTL.Data._dataHasDNVehicleHome;
        } else {
            dataVehicle = _OPSAppointment_DIRouteMasterFTL.Data._dataHasDNVehicleVEN[sort.CreateVendorID];
        }
        tr.find('input.cboSplitVendor').kendoComboBox({
            index: 0, autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true,
            dataTextField: 'CustomerName', dataValueField: 'ID',
            dataSource: Common.DataSource.Local({
                data: _OPSAppointment_DIRouteMasterFTL.Data._dataHasDNVendor,
                model: {
                    id: 'ID',
                    fields: {
                        ID: { type: 'number' },
                        CustomerName: { type: 'string' }
                    }
                },
            }),
            change: function (e) {
                var span = $(e.sender.element).closest('span.HasSplitGridGroup');
                sort = $(span).attr('tabid');
                var item = _OPSAppointment_DIRouteMasterFTL.Data._dataTOMaster[sort];
                var sel = e.sender.dataItem();
                var CreateVendorID = sel != null ? sel.ID : 0;
                if (item.CreateVendorID != CreateVendorID) {
                    item.CreateVendorID = sel != null ? sel.ID : 0;
                    var cboVehicle = $(span).find("#cboSplitVehicle").data("kendoComboBox");
                    cboVehicle.dataSource.data([]);
                    var txtDriver = $(span).find("input.txtSplitDriverName").data("kendoAutoComplete");
                    var dataDriver = [];
                    if (item.CreateVendorID == null || item.CreateVendorID == 0 || item.CreateVendorID == -1) {
                        cboVehicle.dataSource.data(_OPSAppointment_DIRouteMasterFTL.Data._dataHasDNVehicleHome);
                        dataDriver = _OPSAppointment_DIRouteMasterFTL.Data._dataDriver;
                    } else
                        cboVehicle.dataSource.data(_OPSAppointment_DIRouteMasterFTL.Data._dataHasDNVehicleVEN[item.CreateVendorID]);
                    item.CreateVehicleCode = cboVehicle.dataSource.data()[0].RegNo;
                    cboVehicle.text(item.CreateVehicleCode);
                    $timeout(function () {
                        txtDriver.dataSource.data(dataDriver);
                    }, 10);
                }
            }
        });

        tr.find('input.cboSplitVehicle').each(function () {
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
                    var span = $(e.sender.element).closest('span.HasSplitGridGroup');
                    sort = $(span).attr('tabid');
                    var item = _OPSAppointment_DIRouteMasterFTL.Data._dataTOMaster[sort];
                                       
                    var tmpMin, tmpMax;
                    $.each(_OPSAppointment_DIRouteMasterFTL.Data._dataGop, function (i, v) {
                        if (sort == v.CreateSortOrder) {
                            if (tmpMin == null || tmpMin == undefined)
                                tmpMin = v.TempMin;
                            else if (v.TempMin < tmpMin)
                                tmpMin = v.TempMin;

                            if (tmpMax == null || tmpMax == undefined)
                                tmpMax = v.TempMin;
                            else if (v.TempMax > tmpMax)
                                tmpMax = v.TempMax;
                        }
                    });
                    var flag = false;
                    $.each(_OPSAppointment_DIRouteMasterFTL.Data._dataHasDNVehicle, function (i, v) {
                        if (item.CreateVehicleCode == v.RegNo) {
                            if (tmpMax != null || tmpMax != undefined) {
                                if (v.TempMax == null || v.TempMax == undefined || v.TempMax < tmpMax)
                                    flag = true;
                            }
                            if (tmpMin != null || tmpMin != undefined) {
                                if (v.TempMin == null || v.TempMin == undefined || v.TempMin > tmpMin)
                                    flag = true;
                            }
                        }
                    });

                    if (flag) {
                        $rootScope.Message({
                            Msg: 'Không đáp ứng yêu cầu nhiệt độ.', Type: Common.Message.Type.Alert
                        })
                        e.sender.text(_OPSAppointment_DIRouteMasterFTL.Data._dataTOMaster.CreateVehicleCode || "[Chờ nhập xe]");
                    }
                    else {
                        item.CreateVehicleCode = e.sender.text();
                        item.MaxWeight = 0;
                        item.IsChange = true;
                        var txtHasDNDriverName = $(span).find('input.txtSplitDriverName').data("kendoAutoComplete");
                        var txtHasDNTelephone = $(span).find('.txtSplitTelephone');
                        var txtMaxWeight = $(span).find('.maxWeight');
                        var txtOverLoadMessage = $(span).find('.overloadMessage');
                        var txtHasDNMaxWeight = $(span).find('.txtSplitMaxWeight');
                        var txtHasDNMaxCBM = $(span).find('.txtSplitMaxCBM');
                        txtMaxWeight.text("");
                        txtOverLoadMessage.text("");
                        var isHasVehicle = false;
                        var driverName = "";
                        var driverTel = "";
                        $.each(_OPSAppointment_DIRouteMasterFTL.Data._dataHasDNVehicle, function (i, v) {
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
                                $(span).find('.lblSplitSplit.vehicle').hide();
                                return;
                            }
                        });
                        txtHasDNDriverName.value(driverName);
                        txtHasDNTelephone.val(driverTel);
                        txtHasDNDriverName.trigger('change');
                        $(txtHasDNTelephone).trigger('change');
                        // Nếu nhập xe mới -> hiển thị trọng tải, số khối cho nhập vào
                        if (!isHasVehicle && item.CreateVendorID > 0) {
                            txtHasDNMaxWeight.val('');
                            txtHasDNMaxCBM.val('');
                            txtHasDNMaxWeight.show();
                            txtHasDNMaxCBM.show();
                            $(span).find('.lblSplitSplit.vehicle').show();
                        }
                    }
                }
            }).data('kendoComboBox');
            cbo.text(_OPSAppointment_DIRouteMasterFTL.Data._dataTOMaster[item.CreateSortOrder].CreateVehicleCode);
        });

        tr.find('input.txtSplitDriverName').kendoAutoComplete({
            autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true, placeholder: "Nhập tài xế",
            dataTextField: 'DriverName', dataValueField: 'DriverName',
            dataSource: Common.DataSource.Local({
                data: _OPSAppointment_DIRouteMasterFTL.Data._dataDriver,
                model: {
                    id: 'ID',
                    fields: {
                        ID: { type: 'number' },
                        DriverName: { type: 'string' }
                    }
                },
            }),
            change: function (e) {
                var span = $(this.element).closest('span.HasSplitGridGroup');
                sort = $(span).attr('tabid');
                var item = _OPSAppointment_DIRouteMasterFTL.Data._dataTOMaster[sort];
                item.CreateDriverName = this.value();
                item.IsChange = true;
            }
        });

        tr.find('input.txtSplitMaxWeight').change(function (e) {
            var span = $(this).closest('span.HasSplitGridGroup');
            sort = $(span).attr('tabid');
            var item = _OPSAppointment_DIRouteMasterFTL.Data._dataTOMaster[sort];
            item.MaxWeight = $(this).val();
            item.IsChange = true;
        });

        tr.find('input.txtSplitMaxCBM').change(function (e) {
            var span = $(this).closest('span.HasSplitGridGroup');
            sort = $(span).attr('tabid');
            var item = _OPSAppointment_DIRouteMasterFTL.Data._dataTOMaster[sort];
            item.MaxCBM = $(this).val();
            item.IsChange = true;
        });

        tr.find('input.txtSplitTelephone').change(function (e) {
            var span = $(this).closest('span.HasSplitGridGroup');
            sort = $(span).attr('tabid');
            var item = _OPSAppointment_DIRouteMasterFTL.Data._dataTOMaster[sort];
            item.CreateTelephone = $(this).val();
            item.IsChange = true;
        });

        tr.find('input.txtCreateDateTime').kendoDateTimePicker({
            format: Common.Date.Format.DMYHM,
            timeFormat: Common.Date.Format.HM,
            change: function (e) {
                var span = $(e.sender.element).closest('span.HasSplitGridGroup');
                sort = $(span).attr('tabid');
                var item = _OPSAppointment_DIRouteMasterFTL.Data._dataTOMaster[sort];
                item.CreateDateTime = e.sender.value();
                item.IsChange = true;
            }
        });
    };
    
    $scope.Split_OK_Click = function ($event, win) {
        $event.preventDefault();

        var flag = false;
        Common.Data.Each(_OPSAppointment_DIRouteMasterFTL.Data._dataGop, function (o) {
            if (o.CreateSortOrder < 1)
                flag = true;
        })

        if (flag) {
            $rootScope.Message({ Msg: 'Vui lòng phân chuyến cho tất cả sản phẩm.', Type: Common.Message.Type.Alert });
            return;
        }
        
        Common.Data.Each(_OPSAppointment_DIRouteMasterFTL.Data._dataTOMaster, function (o) {
            if (Common.HasValue(o) && o.CreateSortOrder >= 1 && o.CreateVehicleCode == '') {
                flag = true;
            }
        })

        if (flag) {
            $rootScope.Message({ Msg: 'Vui lòng chọn xe cho tất cả các chuyến.', Type: Common.Message.Type.Alert });
            return;
        }

        $rootScope.Message({
            Msg: 'Xác nhận lưu?',
            Type: Common.Message.Type.Confirm,
            Ok: function () {
                $rootScope.IsLoading = true;
                var data = [];
                Common.Data.Each(_OPSAppointment_DIRouteMasterFTL.Data._dataTOMaster, function (o) {
                    if (Common.HasValue(o) && o.CreateSortOrder >= 1)
                        data.push(o);
                })

                Common.Services.Call($http, {
                    url: Common.Services.url.OPS,
                    method: _OPSAppointment_DIRouteMasterFTL.URL.Split,
                    data: { toMasterID: $scope.TOMasterSplitID, dataGop: _OPSAppointment_DIRouteMasterFTL.Data._dataGop, dataVehicle: data },
                    success: function (res) {
                        Common.Services.Error(res, function (res) {
                            Common.Services.Error(res, function (res) {
                                $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });

                                $scope.LoadData();
                                win.close();
                                $rootScope.IsLoading = false;
                            });
                        });
                    }
                });
            }
        });
    }

    $scope.numericSortOrderOptions = {
        format: '#', spinners: false, culture: 'en-US', step: 1, decimals: 0, min: 1,
        change: function (e) {
            var sort = e.sender.element.val() == '' ? 0 : parseInt(e.sender.element.val());
            var tr = $(e.sender.element).closest('tr');
            var dataItem = null;
            $.each($scope.split_grid.dataSource.data(), function (i, v) {
                if (v.uid == tr.attr("data-uid")) {
                    dataItem = v;
                    return false;
                }
            });

            if (Common.HasValue(dataItem)) {
                _OPSAppointment_DIRouteMasterFTL.Data._dataTOMaster[dataItem.CreateSortOrder].IsChange = true;
                if (!Common.HasValue(_OPSAppointment_DIRouteMasterFTL.Data._dataTOMaster[sort])) {
                    var firstVendorID = _OPSAppointment_DIRouteMasterFTL.Data._dataHasDNVendor.length > 0 ? _OPSAppointment_DIRouteMasterFTL.Data._dataHasDNVendor[0].ID : -1;
                    var firstVehicleCode = '';
                    var firstVendorName = _OPSAppointment_DIRouteMasterFTL.Data._dataHasDNVendor.length > 0 ? _OPSAppointment_DIRouteMasterFTL.Data._dataHasDNVendor[0].CustomerName : '';
                    if (firstVendorID == -1) {
                        firstVehicleCode = _OPSAppointment_DIRouteMasterFTL.Data._dataHasDNVehicleHome[0].RegNo;
                    } else {
                        firstVehicleCode = _OPSAppointment_DIRouteMasterFTL.Data._dataHasDNVehicleVEN[firstVendorID].RegNo;
                    }
                    _OPSAppointment_DIRouteMasterFTL.Data._dataTOMaster[sort] = {
                        'TOMasterID': -1, 'CreateSortOrder': sort, 'CreateVendorID': firstVendorID,
                        'CreateVendorName': firstVendorName, 'CreateVehicleCode': firstVehicleCode,
                        'CreateDriverName': '', 'CreateTelephone': '', 'CreateDateTime': Common.Date.FromJson(dataItem.ETD),
                        'Kg': 0, 'IsChange': true, 'TypeID': 1, 'Credit': 0, 'Debit': 0, 'PL': 0
                    };
                } else {
                    _OPSAppointment_DIRouteMasterFTL.Data._dataTOMaster[sort].IsChange = true;
                }
                _OPSAppointment_DIRouteMasterFTL.Data._previousSort = dataItem.CreateSortOrder;

                var tmp = dataItem.toJSON()
                tmp.CreateSortOrder = sort;
                // Remove khoi chuyen cu
                $scope.split_grid.removeRow(tr);
                _OPSAppointment_DIRouteMasterFTL.Data._dataGop = $scope.split_grid.dataSource.data();
                // Add vao chuyen moi
                _OPSAppointment_DIRouteMasterFTL.Data._dataGop.push(tmp);

                $timeout(function () {
                    $scope.IsShowSave = true;
                }, 1);
            }
        }
    };

    //#endregion
}]);