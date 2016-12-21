/// <reference path="~/Scripts/jquery-1.9.1.intellisense.js" />
/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />

var _OPSAppointment_DIRouteMasterVEN = {
    URL: {
        OrderListDN: 'OPS_DIAppointment_Route_HasDNOrderListDN',
        OrderListByGroupID: 'OPS_DIAppointment_Route_HasDNOrderListByGroupID',
        ListGroupID: 'OPS_DIAppointment_Route_HasDNListGroupID',
        VehicleListVendor: 'OPS_DIAppointment_Route_VehicleListVendor',
        VehicleListVehicle: 'OPS_DIAppointment_Route_VehicleListVehicle',
        Delete: 'OPS_DIAppointment_Route_HasDNDelete',
        Approved: 'OPS_DIAppointment_Route_HasDNApproved',
        UnApproved: 'OPS_DIAppointment_Route_HasDNUnApproved',
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
        Save: 'OPS_DIAppointment_Route_TenderSave',
        RequestList: 'OPS_DIAppointment_Route_TenderRequestList',
        AcceptList: 'OPS_DIAppointment_Route_TenderAcceptList',
        RejectList: 'OPS_DIAppointment_Route_TenderRejectList',
        RequestOrderList: 'OPS_DIAppointment_Route_TenderRequestOrderList',
        AcceptOrderList: 'OPS_DIAppointment_Route_TenderAcceptOrderList',
        RejectOrderList: 'OPS_DIAppointment_Route_TenderRejectOrderList',
        ReasonList: 'OPS_DIAppointment_Route_ReasonList',
        SendMailToTender: 'OPS_DIAppointment_Route_SendMailToTender',
        FLMPlaning: 'OPS_DIAppointment_FLMPlaning',

        DITOGroupProduct_List: 'OPSDI_VEN_DITOGroupProduct_List'
    },
    Data: {
        CookieSearch: 'OPS_Appointment_DIRouteMasterVEN_Search',
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
        _dataReason: [],
        FLMPlaning: [],
    },
    Timeline: null
};

angular.module('myapp').controller('OPSAppointment_DIRouteMasterVENCtrl', ['$rootScope', '$scope', '$http', '$location', '$state', '$timeout', function ($rootScope, $scope, $http, $location, $state, $timeout) {
    if ($rootScope.IsPageComplete != true) return;
    Common.Log('OPSAppointment_DIRouteMasterVENCtrl');
    $rootScope.IsLoading = false;
    $scope.Auth = $rootScope.GetAuth();

    $scope.IsShowCombine = false;
    $scope.IsHasDNMonitor = false;
    $scope.IsCreateByID = true;
    $scope.IsExpand = true;

    $scope.Search = {};
    $scope.Reason = null;
    $scope.Summary = '';

    $scope.cboReasonOptions = {
        dataTextField: 'ReasonName', dataValueField: 'ID', placeholder: 'Chọn lý do',
        filter: 'contains', autoClose: false, ignoreCase: true,
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    ReasonName: { type: 'string' },
                }
            }
        }),
    };

    // Load danh sách Vendor
    Common.Services.Call($http, {
        url: Common.Services.url.OPS,
        method: _OPSAppointment_DIRouteMasterVEN.URL.VehicleListVendor,
        data: {},
        success: function (res) {
            if (Common.HasValue(res)) {
                $timeout(function () {
                    res.splice(0, 1);
                    _OPSAppointment_DIRouteMasterVEN.Data._dataHasDNVendor = res;
                    if (_OPSAppointment_DIRouteMasterVEN.Data._dataHasDNVendor.length > 0 && _OPSAppointment_DIRouteMasterVEN.Data._dataHasDNVehicle.length > 0)
                        $scope.LoadDataVehicleVendor();
                }, 1);
            }
        }
    });

    // Load danh sách xe
    Common.Services.Call($http, {
        url: Common.Services.url.OPS,
        method: _OPSAppointment_DIRouteMasterVEN.URL.VehicleListVehicle,
        data: {},
        success: function (res) {
            if (Common.HasValue(res)) {
                $timeout(function () {
                    _OPSAppointment_DIRouteMasterVEN.Data._dataHasDNVehicle = res;
                    _OPSAppointment_DIRouteMasterVEN.Data._dataVehicleVEN = res;
                    if (_OPSAppointment_DIRouteMasterVEN.Data._dataHasDNVendor.length > 0 && _OPSAppointment_DIRouteMasterVEN.Data._dataHasDNVehicle.length > 0)
                        $scope.LoadDataVehicleVendor();
                }, 1);
            }
        }
    });

    // Load danh sách tài xế
    Common.Services.Call($http, {
        url: Common.Services.url.OPS,
        method: _OPSAppointment_DIRouteMasterVEN.URL.VehicleListDriver,
        success: function (res) {
            Common.Services.Error(res, function (res) {
                _OPSAppointment_DIRouteMasterVEN.Data._dataDriver = [];
                $.each(res, function (i, v) {
                    _OPSAppointment_DIRouteMasterVEN.Data._dataDriver.push({ 'ID': v.ID, 'Text': v.LastName + ' ' + v.FirstName + ' (' + v.EmployeeCode + ')', 'DriverName': v.LastName + ' ' + v.FirstName, 'DriverTelNo': v.Cellphone });
                });
            });
        }
    });

    // Load danh sách lý do từ chối
    Common.Services.Call($http, {
        url: Common.Services.url.OPS,
        method: _OPSAppointment_DIRouteMasterVEN.URL.ReasonList,
        success: function (res) {
            Common.Services.Error(res, function (res) {
                _OPSAppointment_DIRouteMasterVEN.Data._dataReason = res.Data;
                $scope.cboReasonOptions.dataSource.data(res.Data);
            });
        }
    });

    // Load danh sách phân tài
    Common.Services.Call($http, {
        url: Common.Services.url.OPS,
        method: _OPSAppointment_DIRouteMasterVEN.URL.FLMPlaning,
        success: function (res) {
            _OPSAppointment_DIRouteMasterVEN.Data.FLMPlaning = res;
        }
    });

    $scope.LoadData = function () {
        Common.Log('LoadData');
        $rootScope.IsLoading = true;

        $scope.Summary = '';
        var i = 0;
        while (i < _OPSAppointment_DIRouteMasterVEN.Data._dataHasDNVendor.length) {
            if (_OPSAppointment_DIRouteMasterVEN.Data._dataHasDNVendor[i].ID < 0)
                _OPSAppointment_DIRouteMasterVEN.Data._dataHasDNVendor.splice(i, 1);
            else
                i++;
        }
        _OPSAppointment_DIRouteMasterVEN.Data._dataHasDNVehicle = $.extend(true, [], _OPSAppointment_DIRouteMasterVEN.Data._dataVehicleVEN);
        var i = 0;
        while (i < _OPSAppointment_DIRouteMasterVEN.Data._dataHasDNVehicle.length) {
            if (_OPSAppointment_DIRouteMasterVEN.Data._dataHasDNVehicle[i].ID < 0)
                _OPSAppointment_DIRouteMasterVEN.Data._dataHasDNVehicle.splice(i, 1);
            else
                i++;
        }

        _OPSAppointment_DIRouteMasterVEN.Data._dataHasDNSort = [];
        _OPSAppointment_DIRouteMasterVEN.Data._dataHasDNGroupProduct = [];

        var lstFilters = [];
        var param = Common.Request.Create({
            Sorts: [], Filters: lstFilters
        });

        var _urlMasterList = _OPSAppointment_DIRouteMasterVEN.URL.RequestList;
        var _urlOrderList = _OPSAppointment_DIRouteMasterVEN.URL.RequestOrderList;

        if ($scope.Search.TypeOfView == 'Reject') {
            _urlMasterList = _OPSAppointment_DIRouteMasterVEN.URL.RejectList;
            _urlOrderList = _OPSAppointment_DIRouteMasterVEN.URL.RejectOrderList;
        } else {
            if ($scope.Search.TypeOfView == 'Accept') {
                _urlMasterList = _OPSAppointment_DIRouteMasterVEN.URL.AcceptList;
                _urlOrderList = _OPSAppointment_DIRouteMasterVEN.URL.AcceptOrderList;
            }
        }

        Common.Services.Call($http, {
            url: Common.Services.url.OPS,
            method: _urlMasterList,
            data: { request: param, DateFrom: $scope.Search.DateFrom, DateTo: $scope.Search.DateTo },
            success: function (res) {
                Common.Services.Error(res, function (res) {
                    _OPSAppointment_DIRouteMasterVEN.Data._dataHasDNSort[0] = { 'CreateSortOrder': 0, 'CreateVendorID': -1, 'CreateVehicleCode': '', 'CreateDriverName': '', 'CreateTelephone': '', 'Kg': 0 };
                    var datafix = [];

                    Common.Services.Call($http, {
                        url: Common.Services.url.OPS,
                        method: _urlOrderList,
                        data: { request: param, DateFrom: $scope.Search.DateFrom, DateTo: $scope.Search.DateTo },
                        success: function (res) {
                            Common.Services.Error(res, function (res) {
                                _OPSAppointment_DIRouteMasterVEN.Data._dataHasDN = [];

                                $.each(res, function (i, v) {
                                    if (Common.HasValue(v.RequestDate)) {
                                        v.RequestDate = Common.Date.FromJson(v.RequestDate);
                                        v.RequestDateEmpty = Common.Date.Date(v.RequestDate);
                                    }
                                    v.Kg = v.Ton * 1000;

                                    if (v.CreateSortOrder > 0)
                                        v.CreateSortOrder = datafix[v.CreateSortOrder];

                                    _OPSAppointment_DIRouteMasterVEN.Data._dataHasDN.push(v);
                                });
                                $scope.gridNoDN.dataSource.data(_OPSAppointment_DIRouteMasterVEN.Data._dataHasDN);
                                $rootScope.IsLoading = false;
                            });
                        }
                    });

                    var index = 1;
                    $.each(res, function (i, v) {
                        v.CreateDateTime = Common.Date.FromJson(v.CreateDateTime);
                        v.Kg = 0;
                        _OPSAppointment_DIRouteMasterVEN.Data._dataHasDNSort[index] = v;
                        datafix[v.CreateSortOrder] = index;
                        v.CreateSortOrder = index;
                        v.IsChange = false;

                        index++;
                    });
                });
            }
        });
    };

    $scope.Init_LoadCookie = function () {
        Common.Log('Init_LoadCookie');

        var strCookie = Common.Cookie.Get(_OPSAppointment_DIRouteMasterVEN.Data.CookieSearch);
        if (Common.HasValue(strCookie) && strCookie != '') {
            try {
                var objCookie = eval('[' + strCookie + ']')[0];
                $scope.Search.DateFrom = Common.Date.FromJson(objCookie.DateFrom);
                $scope.Search.DateTo = Common.Date.FromJson(objCookie.DateTo);
                if (Common.HasValue(objCookie.TypeOfView)) {
                    $scope.Search.TypeOfView = objCookie.TypeOfView;
                } else {
                    $scope.Search.TypeOfView = 'Waitting';
                }
            } catch (e) { }
        }
        if (!Common.HasValue($scope.Search.DateFrom) || !Common.HasValue($scope.Search.DateTo)) {
            $scope.Search.DateFrom = new Date().addDays(-3);
            $scope.Search.DateTo = new Date().addDays(3);
            $scope.Search.TypeOfView = 'Waitting';
        }

        $timeout(function () {
            Common.Cookie.Set(_OPSAppointment_DIRouteMasterVEN.Data.CookieSearch, JSON.stringify($scope.Search));
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
            {
                field: 'CreateSortOrder', width: '50px', headerTemplate: '<a class="k-button" href="/" ng-click="Expand_Click($event,gridNoDN)"><i class="fa fa-minus"></i></a>', groupHeaderTemplate: '<span class="HasDNGridGroup" tabid="#=value#"></span>', sortable: false, filterable: false, menu: false,
                sortorder: 0, configurable: false, isfunctionalHidden: false
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
        toolbar: kendo.template($('#OPSAppointment_DIRouteMasterVEN_gridToolbar').html()),
        filterable: { mode: 'row' },
        dataBound: function () {
            Common.Log("GridDataBound");

            if (Common.HasValue(this) && Common.HasValue(this.tbody)) {
                if (_OPSAppointment_DIRouteMasterVEN.Data._dataHasDNGroupProduct.length == 0)
                    $scope.ReloadSort();

                this.element.find('.HasDNGridGroup').each(function () {
                    var sort = parseInt($(this).attr('tabid'));
                    var item = _OPSAppointment_DIRouteMasterVEN.Data._dataHasDNSort[sort];

                    var tr = $(this).closest('tr');
                    if (Common.HasValue(item)) {
                        //if (item.TypeID == 1) {
                        //    if (!$(tr).hasClass('red'))
                        //        $(tr).addClass('red');
                        //}
                        //else if (item.TypeID == 2) {
                        //    if (!$(tr).hasClass('yellow'))
                        //        $(tr).addClass('yellow');
                        //}
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
                                $(this).html('<input type="checkbox" class="chkChooseVehicle"/> Chuyến ' + sort + ' - Kg: ' + Common.Number.ToNumber1(item.Kg) + ' - ' + item.CreateVendorName + ' - ' +
                                    '<a href="" class="btnHasDNGroup">' + item.CreateVehicleCode + ' - ' + drivername + ' - ' + telephone + ' - ' + Common.Date.FromJsonDMYHM(item.CreateDateTime) + '</a>' +
                                    '<input style="display:none;width:200px" id="cboHasDNVehicle" class="cboHasDNVehicle cus-combobox" placeholder="Nhập số xe" value="' + item.CreateVehicleCode + '" /><span style="display:none" class="lblHasDNSplit"> - </span>' +
                                    '<input style="display:none;width:50px" type="number" min="0" class="txtHasDNMaxWeight k-textbox" placeholder="T.Tải" value="" /><span style="display:none" class="lblHasDNSplit vehicle"> - </span>' +
                                    '<input style="display:none;width:50px" type="number" min="0" class="txtHasDNMaxCBM k-textbox" placeholder="S.Khối" value="" /><span style="display:none" class="lblHasDNSplit vehicle"> - </span>' +
                                    '<input style="display:none;width:150px" type="text" class="txtHasDNDriverName k-textbox" placeholder="Nhập tài xế" value="' + drivername + '" /><span style="display:none" class="lblHasDNSplit"> - </span>' +
                                    '<input style="display:none;width:150px" type="text" class="txtHasDNTelephone k-textbox" placeholder="Nhập điện thoại" value="' + telephone + '"/><span style="display:none" class="lblHasDNSplit"> - </span>' +
                                    '<input style="display:none;width:150px" class="txtCreateDateTime"  value="' + Common.Date.FromJsonDMYHM(item.CreateDateTime) + '"/>' + strMaxWeight + strOverLoad);
                            }
                            else {
                                $(this).html('&nbsp;&nbsp;&nbsp; Chuyến ' + sort + ' - Kg: ' + Common.Number.ToNumber1(item.Kg) + ' - ' + item.CreateVendorName + ' - ' +
                                    '<a href="" class="btnHasDNGroup">' + item.CreateVehicleCode + ' - ' + drivername + ' - ' + telephone + ' - ' + Common.Date.FromJsonDMYHM(item.CreateDateTime) + '</a>' +
                                    '<input style="display:none;width:200px" id="cboHasDNVehicle" class="cboHasDNVehicle cus-combobox" placeholder="Nhập số xe" value="' + item.CreateVehicleCode + '" /><span style="display:none" class="lblHasDNSplit"> - </span>' +
                                    '<input style="display:none;width:50px" type="number" min="0" class="txtHasDNMaxWeight k-textbox" placeholder="T.Tải" value="" /><span style="display:none" class="lblHasDNSplit vehicle"> - </span>' +
                                    '<input style="display:none;width:50px" type="number" min="0" class="txtHasDNMaxCBM k-textbox" placeholder="S.Khối" value="" /><span style="display:none" class="lblHasDNSplit vehicle"> - </span>' +
                                    '<input style="display:none;width:150px" type="text" class="txtHasDNDriverName k-textbox" placeholder="Nhập tài xế" value="' + drivername + '" /><span style="display:none" class="lblHasDNSplit"> - </span>' +
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
        Common.Controls.Grid.ReorderColumns({ Grid: $scope.gridNoDN, CookieName: 'OPS_DIAppointment_Route_gridNoDN' });
    }, 10);

    $scope.LoadDataVehicleVendor = function () {
        var lstVendor = _OPSAppointment_DIRouteMasterVEN.Data._dataHasDNVendor;
        var lstVehicle = _OPSAppointment_DIRouteMasterVEN.Data._dataHasDNVehicle;
        _OPSAppointment_DIRouteMasterVEN.Data._dataHasDNVehicleVEN = [];
        _OPSAppointment_DIRouteMasterVEN.Data._dataHasDNVehicleHome = [];
        $.each(lstVehicle, function (idx, vehicle) {
            if (vehicle.RegNo != "[Chờ nhập xe]") {
                for (var i = 0; i < vehicle.ListVendorID.length; i++) {
                    var vendorid = vehicle.ListVendorID[i];
                    if (!Common.HasValue(_OPSAppointment_DIRouteMasterVEN.Data._dataHasDNVehicleVEN[vendorid]))
                        _OPSAppointment_DIRouteMasterVEN.Data._dataHasDNVehicleVEN[vendorid] = [];
                    _OPSAppointment_DIRouteMasterVEN.Data._dataHasDNVehicleVEN[vendorid].push(vehicle);
                }
                if (vehicle.IsOwn) {
                    _OPSAppointment_DIRouteMasterVEN.Data._dataHasDNVehicleHome.push(vehicle);
                }
            } else {
                _OPSAppointment_DIRouteMasterVEN.Data._dataHasDNVehicleHome.push(vehicle);
                for (var i = 0; i < lstVendor.length; i++) {
                    var vendorid = lstVendor[i].ID;
                    if (!Common.HasValue(_OPSAppointment_DIRouteMasterVEN.Data._dataHasDNVehicleVEN[vendorid]))
                        _OPSAppointment_DIRouteMasterVEN.Data._dataHasDNVehicleVEN[vendorid] = [];
                    _OPSAppointment_DIRouteMasterVEN.Data._dataHasDNVehicleVEN[vendorid].push(vehicle);
                }
            }
        });
    }

    $scope.GridFilter = function () {

    };

    $scope.ReloadSort = function ($event) {
        Common.Log("ReloadSort");

        var totalsort = 0;
        var totalton = 0;
        var totaltoncomplete = 0;
        var totalcbm = 0;
        var totalcbmcomplete = 0;
        var totalquan = 0;
        $scope.Summary = '';

        $.each(_OPSAppointment_DIRouteMasterVEN.Data._dataHasDNSort, function (i, v) {
            if (Common.HasValue(v)) {
                v.Kg = 0;
                v.CBM = 0;
                v.Quantity = 0;
                if (v.CreateSortOrder > 0)
                    totalsort++;
            }
        });

        _OPSAppointment_DIRouteMasterVEN.Data._dataHasDNGroupProduct = [];
        $.each(_OPSAppointment_DIRouteMasterVEN.Data._dataHasDN, function (i, v) {
            v.Kg = v.Kg > 0 ? Math.round(v.Kg * 1000) / 1000 : 0;
            v.Ton = v.Ton > 0 ? Math.round(v.Ton * 1000000) / 1000000 : 0;
            v.CBM = v.CBM > 0 ? Math.round(v.CBM * 1000) / 1000 : 0;
            v.Quantity = v.Quantity > 0 ? Math.round(v.Quantity * 1000) / 1000 : 0;

            var itemSort = _OPSAppointment_DIRouteMasterVEN.Data._dataHasDNSort[v.CreateSortOrder];
            if (!Common.HasValue(itemSort)) {
                itemSort = { 'CreateSortOrder': v.CreateSortOrder, 'CreateVendorID': v.VendorOfVehicleID, 'CreateVendorName': v.VendorOfVehicleName, 'CreateVehicleCode': v.VehicleCode, 'CreateDriverName': v.DriverName, 'CreateTelephone': v.DriverTelNo, 'Kg': 0, 'Ton': 0, 'CBM': 0, 'Quantity': 0, 'CreateVehicleID': v.VehicleID, 'TOMasterID': v.TOMasterID, 'CreateDateTime': v.CreateDateTime, 'TypeID': v.TypeID, 'MaxWeight': v.MaxWeightCal, 'MaxCBM': 0 };
                _OPSAppointment_DIRouteMasterVEN.Data._dataHasDNSort[v.CreateSortOrder] = itemSort;
            }
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

            if (!Common.HasValue(_OPSAppointment_DIRouteMasterVEN.Data._dataHasDNGroupProduct[v.ID]))
                _OPSAppointment_DIRouteMasterVEN.Data._dataHasDNGroupProduct[v.ID] = [];
            _OPSAppointment_DIRouteMasterVEN.Data._dataHasDNGroupProduct[v.ID].push(v);
        });

        var ixdel = 0;
        while (ixdel < _OPSAppointment_DIRouteMasterVEN.Data._dataHasDNSort.length) {
            if (Common.HasValue(_OPSAppointment_DIRouteMasterVEN.Data._dataHasDNSort[ixdel])) {
                var itemSort = _OPSAppointment_DIRouteMasterVEN.Data._dataHasDNSort[ixdel];
                if (itemSort.Ton < 0.001 && itemSort.CBM < 0.001) {
                    _OPSAppointment_DIRouteMasterVEN.Data._dataHasDNSort.splice(ixdel, 1);
                }
                else
                    ixdel++;
            }
            else
                ixdel++;
        }

        $scope.Summary = 'Tấn: ' + Common.Number.ToNumber1(totaltoncomplete) + ' / ' + Common.Number.ToNumber1(totalton) + ' - Khối: ' + Common.Number.ToNumber1(totalcbmcomplete) + ' / ' + Common.Number.ToNumber1(totalcbm) + ' - Số chuyến: ' + totalsort;
    };

    $scope.Approved_Click = function ($event) {
        Common.Log('Approved_Click');
        $event.preventDefault();

        var data = []; var error = [];
        $scope.gridNoDN.tbody.find('.HasDNGridGroup').each(function () {
            var sort = parseInt($(this).attr('tabid'));
            var item = _OPSAppointment_DIRouteMasterVEN.Data._dataHasDNSort[sort];
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
                            method: _OPSAppointment_DIRouteMasterVEN.URL.Accept,
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

    $scope.Rejected_Click = function ($event, win) {
        Common.Log('Rejected_Click');
        $event.preventDefault();

        var data = [];
        $scope.gridNoDN.tbody.find('.HasDNGridGroup').each(function () {
            var sort = parseInt($(this).attr('tabid'));
            var item = _OPSAppointment_DIRouteMasterVEN.Data._dataHasDNSort[sort];
            var tr = $(this).closest('tr');

            if (sort > 0 && item.TOMasterID > 0) {
                var chk = $(tr).find('.chkChooseVehicle');
                if (Common.HasValue(chk) && chk.prop('checked') == true)
                    data.push(item.TOMasterID);
            }
        });

        if (data.length > 0) {
            $scope.Reason.Reason = '';
            $scope.Reason.ReasonID = _OPSAppointment_DIRouteMasterVEN.Data._dataReason.length > 0 ? _OPSAppointment_DIRouteMasterVEN.Data._dataReason[0].ID : 0;
            win.center().open();
        }
    };

    $scope.Save_Click = function ($event) {
        Common.Log('Save_Click');
        $event.preventDefault();

        var flag = true;
        var error = '';
        $.each(_OPSAppointment_DIRouteMasterVEN.Data._dataHasDNSort, function (i, v) {
            if (Common.HasValue(v)) {
                if (v.CreateSortOrder > 0 && (v.CreateVehicleCode == '')) {
                    // || v.CreateVehicleCode == '[Chờ nhập xe]'
                    error += ', ' + v.CreateSortOrder;
                    flag = false;
                }
            }
        });

        var data = [];
        $.each(_OPSAppointment_DIRouteMasterVEN.Data._dataHasDN, function (i, v) {
            if (v.CreateSortOrder > 0 && _OPSAppointment_DIRouteMasterVEN.Data._dataHasDNSort[v.CreateSortOrder].TypeID < 2 && v.Kg > 0) {
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
                    Common.Services.Call($http, {
                        url: Common.Services.url.OPS,
                        method: _OPSAppointment_DIRouteMasterVEN.URL.Save,
                        data: { lstVehicle: _OPSAppointment_DIRouteMasterVEN.Data._dataHasDNSort },
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
            $rootScope.Message({ Msg: 'Các chuyến ' + error.substr(2) + ' chưa chọn xe', NotifyType: Common.Message.NotifyType.ERROR });
    };

    $scope.Close_Click = function ($event) {
        Common.Log('Close_Click');
        if (Common.HasValue($event))
            $event.preventDefault();

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

    $scope.GroupDN_Click = function (item) {
        Common.Log('GroupDN_Click');

        $(item).hide();
        var tr = $(item).closest('tr');
        $(tr).find('.lblHasDNSplit,input').show();

        $(tr).find('.txtHasDNMaxWeight,.txtHasDNMaxCBM').hide();
        $(tr).find('.lblHasDNSplit.vehicle').hide();
        var item = $scope.gridNoDN.dataItem(tr);
        var dataVehicle = [];
        var sort = _OPSAppointment_DIRouteMasterVEN.Data._dataHasDNSort[item.CreateSortOrder];
        dataVehicle = _OPSAppointment_DIRouteMasterVEN.Data._dataHasDNVehicleVEN[sort.CreateVendorID];

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
                    var item = _OPSAppointment_DIRouteMasterVEN.Data._dataHasDNSort[sort];
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

                    $.each(_OPSAppointment_DIRouteMasterVEN.Data._dataHasDNVehicle, function (i, v) {
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
                    txtHasDNTelephone.trigger('change');
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

            cbo.text(_OPSAppointment_DIRouteMasterVEN.Data._dataHasDNSort[item.CreateSortOrder].CreateVehicleCode);
        });

        tr.find('input.txtHasDNDriverName').kendoAutoComplete({
            autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true, placeholder: "Nhập tài xế",
            dataTextField: 'DriverName', dataValueField: 'DriverName',
            dataSource: Common.DataSource.Local({
                data: _OPSAppointment_DIRouteMasterVEN.Data._dataDriver,
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
                var item = _OPSAppointment_DIRouteMasterVEN.Data._dataHasDNSort[sort];
                item.CreateDriverName = this.value();
                item.IsChange = true;
            }
        });

        tr.find('input.txtHasDNMaxWeight').change(function (e) {
            var span = $(this).closest('span.HasDNGridGroup');
            sort = $(span).attr('tabid');
            var item = _OPSAppointment_DIRouteMasterVEN.Data._dataHasDNSort[sort];
            item.MaxWeight = $(this).val();
            item.IsChange = true;
        });

        tr.find('input.txtHasDNMaxCBM').change(function (e) {
            var span = $(this).closest('span.HasDNGridGroup');
            sort = $(span).attr('tabid');
            var item = _OPSAppointment_DIRouteMasterVEN.Data._dataHasDNSort[sort];
            item.MaxCBM = $(this).val();
            item.IsChange = true;
        });

        tr.find('input.txtHasDNTelephone').change(function (e) {
            var span = $(this).closest('span.HasDNGridGroup');
            sort = $(span).attr('tabid');
            var item = _OPSAppointment_DIRouteMasterVEN.Data._dataHasDNSort[sort];
            item.CreateTelephone = $(this).val();
            item.IsChange = true;
        });

        tr.find('input.txtCreateDateTime').kendoDateTimePicker({
            format: Common.Date.Format.DMYHM,
            timeFormat: Common.Date.Format.HM,
            change: function (e) {
                var span = $(e.sender.element).closest('span.HasDNGridGroup');
                sort = $(span).attr('tabid');
                var item = _OPSAppointment_DIRouteMasterVEN.Data._dataHasDNSort[sort];
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
                $.each(_OPSAppointment_DIRouteMasterVEN.Data.FLMPlaning, function (i, v) {
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
    }

    $scope.ChangeView_Click = function ($event) {
        $scope.LoadData();
    }

    $scope.RejectSave_Click = function ($event, win) {
        Common.Log('RejectSave_Click');
        if (Common.HasValue($event))
            $event.preventDefault();

        var data = [];
        $scope.gridNoDN.tbody.find('.HasDNGridGroup').each(function () {
            var sort = parseInt($(this).attr('tabid'));
            var item = _OPSAppointment_DIRouteMasterVEN.Data._dataHasDNSort[sort];
            var tr = $(this).closest('tr');

            if (sort > 0 && item.TOMasterID > 0) {
                var chk = $(tr).find('.chkChooseVehicle');
                if (Common.HasValue(chk) && chk.prop('checked') == true)
                    data.push(item.TOMasterID);
            }
        });

        if (data.length > 0) {
            if ($scope.Reason.ReasonID <= 0)
                $rootScope.Message({ Msg: 'Vui lòng chọn lý do từ chối', NotifyType: Common.Message.NotifyType.ERROR });
            else {
                $rootScope.Message({
                    Type: Common.Message.Type.Confirm,
                    Msg: 'Bạn muốn từ chối các chuyến đã chọn ?',
                    Ok: function () {
                        $rootScope.IsLoading = true;
                        Common.Services.Call($http, {
                            url: Common.Services.url.OPS,
                            method: _OPSAppointment_DIRouteMasterVEN.URL.Reject,
                            data: { lst: data, item: $scope.Reason },
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
                                        }
                                    });
                                });
                            }
                        });
                    }
                });
            }
        }
    };

    $scope.RejectClose_Click = function ($event, win) {
        Common.Log('RejectClose_Click');
        if (Common.HasValue($event))
            $event.preventDefault();

        win.close();
    };

    //#region ViewAdmin

    $scope.TypeOfViewAdmin = 1;
    $scope.$watch("TypeOfViewAdmin", function () {
        $scope.groupGrid.dataSource.read();
    })

    $scope.groupGrid_Options = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.OPS,
            method: _OPSAppointment_DIRouteMasterVEN.URL.DITOGroupProduct_List,
            pageSize: 0,
            group: [{ field: 'TOMasterCode' }],
            readparam: function () {
                return {
                    typeOfView: $scope.TypeOfViewAdmin
                }
            },
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    IsChoose: { type: 'bool', defaultValue: false },
                    ETD: { type: 'date' },
                    ETA: { type: 'date' },
                    TOETD: { type: 'date' },
                    TOETA: { type: 'date' },
                    TOLastUpdateTime: { type: 'date' },
                    TOCreatedDate: { type: 'date' },
                    Ton: { type: 'number' },
                    CBM: { type: 'number' },
                    Quantity: { type: 'number' },
                    TempMin: { type: 'number' },
                    TempMax: { type: 'number' }
                }
            }
        }),
        height: '99%', groupable: false, pageable: false, columnMenu: false, resizable: true, reorderable: true, sortable: true, filterable: { mode: 'row' },
        toolbar: kendo.template($('#groupGrid_Toolbar').html()),
        dataBound: function () {
            if ($scope.TypeOfViewAdmin == 3) {
                this.showColumn("Reason");
                this.showColumn("ReasonName");
            } else {
                this.hideColumn("Reason");
                this.hideColumn("ReasonName");
            }
        },
        columns: [
            {
                field: 'TOMasterCode', width: '120px', title: 'Mã chuyến',
                filterable: { cell: { operator: 'contains', showOperators: false } },
                groupHeaderTemplate: function (e) {
                    try {
                        var obj = e.aggregates.parent().items[0];
                        if (Common.HasValue(obj)) {
                            var sumTon = 0, sumCBM = 0, sumQty = 0;
                            Common.Data.Each(e.aggregates.parent().items, function (o) {
                                sumTon += o.Ton; sumCBM += o.CBM; sumQty += o.Quantity;
                            })
                            var strVeh = obj.TOVehicleNo == "" || obj.TOVehicleNo == null ? "[Chưa nhập]" : obj.TOVehicleNo;
                            var strTel = obj.TODriverTel == "" || obj.TODriverTel == null ? "[Chưa nhập]" : obj.TODriverTel;
                            var strName = obj.TODriverName == "" || obj.TODriverName == null ? "[Chưa nhập]" : obj.TODriverName;
                            var strRea = obj.ReasonName == "" ? obj.Reason : obj.ReasonName;
                            if (strRea == "")
                                strRea = "[Ko có lý do]";
                            var eleRea = "";
                            if ($scope.TypeOfViewAdmin == 3) {
                                eleRea = "<span> Lý do từ chối: " + strRea + "</span>";
                            }
                            return "<span>" + obj.TOMasterCode + " - " + obj.TOVendorCode + " - " + strVeh + " - " + strName + " - " + strTel
                                + " - " + Common.Date.FromJsonDDMMHM(obj.TOETD) + " - " + Common.Date.FromJsonDDMMHM(obj.TOETA) + "</span>"
                                + " - Tấn: " + Math.round(sumTon * 100000) / 100000 + " - Khối: " + Math.round(sumCBM * 100000) / 100000 + " </span>"
                                + eleRea + "<span style='font-size:12px;font-weight:lighter;'> - cập nhật cuối: " + Common.Date.FromJsonDDMMHM(obj.TOLastUpdateTime) + " bởi " + obj.TOLastUpdate + " </span>";
                        }
                    } catch (e) {
                        return "span>" + e.value + "</span>";
                    }
                }
            },
            { field: 'CustomerCode', width: 100, title: 'Mã KH', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'CustomerShortName', width: 150, title: 'Tên KH', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'OrderCode', width: '150px', title: 'Đơn hàng', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'TransportModeName', width: '80px', title: 'Loại v/c', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'DistributorCode', width: '120px', title: 'Nhà phân phối', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'GroupProductCode', width: '150px', title: 'Nhóm hàng', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'ProductCode', width: '100px', title: 'Hàng hóa', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'Ton', width: '80px', title: 'Tấn', filterable: { cell: { operator: 'gte', showOperators: false } } },
            { field: 'CBM', width: '80px', title: 'Khối', filterable: { cell: { operator: 'gte', showOperators: false } } },
            { field: 'Quantity', width: '80px', title: 'SL', filterable: { cell: { operator: 'gte', showOperators: false } } },
            {
                field: 'ETD', width: '120px', title: 'ETD', template: "#=ETD != null ? Common.Date.FromJsonDMYHM(ETD) : ''#",
                filterable: { cell: { template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); }, operator: 'equal', showOperators: false } }
            },
            {
                field: 'ETA', width: '120px', title: 'ETA', template: "#=ETA != null ? Common.Date.FromJsonDMYHM(ETA) : ''#",
                filterable: { cell: { template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); }, operator: 'equal', showOperators: false } }
            },
            { field: 'TempMin', width: '100px', title: 'N.Độ tối thiểu', filterable: { cell: { operator: 'gte', showOperators: false } } },
            { field: 'TempMax', width: '100px', title: 'N.Độ tối đa', filterable: { cell: { operator: 'gte', showOperators: false } } },
            { field: 'SOCode', width: '100px', title: 'Số SO', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'DNCode', width: '100px', title: 'Số DN', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'UserDefine1', width: '100px', title: 'Ghi chú 1', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'UserDefine2', width: '100px', title: 'Ghi chú 2', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'LocationFromCode', width: '100px', title: 'Điểm nhận', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'LocationFromAddress', width: '250px', title: 'Địa chỉ', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'LocationToCode', width: '100px', title: 'Điểm giao', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'LocationToAddress', width: '250px', title: 'Địa chỉ', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'TOVendorCode', width: '150px', title: 'Đối tác', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'TOVehicleNo', width: '120px', title: 'Số xe', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'TODriverName', width: '100px', title: 'Tài xế', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'TODriverTel', width: '100px', title: 'SĐT', filterable: { cell: { operator: 'contains', showOperators: false } } },
            {
                field: 'TOCreatedDate', width: '160px', title: 'Ngày tạo chuyến', template: "#=TOCreatedDate != null ? Common.Date.FromJsonDMYHM(TOCreatedDate) : ''#",
                filterable: { cell: { template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); }, operator: 'equal', showOperators: false } }
            },
            { field: 'TOCreatedBy', width: '160px', title: 'Người tạo chuyến', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'ReasonName', width: '200px', title: 'Lý do từ chối', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'Reason', width: '200px', title: 'Ghi chú từ chối', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { title: ' ', filterable: false, menu: false, sortable: false }
        ]
    }


    //#endregion

    //#region Action
    $scope.ShowSetting = function ($event, grid) {
        $event.preventDefault();

        $rootScope.ShowSetting({
            ListView: views.OPSAppointment_Ven,
            event: $event,
            grid: grid,
            current: $state.current
        });
    };

    $scope.HideSetting = function ($event) {
        $event.preventDefault();

        $rootScope.HideSetting();
    }
    //#endregion
}]);