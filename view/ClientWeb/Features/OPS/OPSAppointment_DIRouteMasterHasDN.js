/// <reference path="~/Scripts/jquery-1.9.1.intellisense.js" />
/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />

//#region OPS object
var _OPSAppointment_DIRouteMasterHasDN = {
    URL: {
        OrderListDN: 'OPS_DIAppointment_Route_HasDNOrderListDN',
        OrderListByGroupID: 'OPS_DIAppointment_Route_HasDNOrderListByGroupID',
        List: 'OPS_DIAppointment_Route_HasDNList',
        OrderList: 'OPS_DIAppointment_Route_HasDNOrderList',
        ListGroupID: 'OPS_DIAppointment_Route_HasDNListGroupID',
        VehicleListVendor: 'OPS_DIAppointment_Route_VehicleListVendor',
        VehicleListVehicle: 'OPS_DIAppointment_Route_VehicleListVehicle',
        Delete: 'OPS_DIAppointment_Route_HasDNDelete',
        Save: 'OPS_DIAppointment_Route_HasDNSave',
        Approved: 'OPS_DIAppointment_Route_HasDNApproved',
        UnApproved: 'OPS_DIAppointment_Route_HasDNUnApproved',
        VehicleListDriver: 'OPS_DIAppointment_Route_VehicleListDriver',
        FLMPlaning: 'OPS_DIAppointment_FLMPlaning',
    },
    Data: {
        _createListDN: [],
        _createListID: [],
        _dataHasDNGroupProduct: [],
        _dataHasDN: [],
        _dataHasDNSort: [],
        _dataHasDNVendor: [],
        _dataHasDNVehicle: [],
        _dataHasDNVehicleVEN: [],
        _dataHasDNVehicleHome: [],
        _dataDriver: [],
        FLMPlaning: [],
    }
};
//endregion OPS object
angular.module('myapp').controller('OPSAppointment_DIRouteMasterHasDNCtrl', ['$rootScope', '$scope', '$http', '$location', '$state', '$timeout', function ($rootScope, $scope, $http, $location, $state, $timeout) {
    if ($rootScope.IsPageComplete != true) return;
    Common.Log('OPSAppointment_DIRouteMasterHasDNCtrl');
    $rootScope.IsLoading = false;

    $scope.DNCode = "";
    $scope.IsShowTxtDN = true;
    $scope.IsShowCombine = false;
    $scope.IsHasDNMonitor = false;
    $scope.IsCreateByID = true;
    $scope.IsExpand = true;

    $scope.Summary = '';

    $scope.cboOrderListDNOptions = {
        minLength: 4, ignoreCase: true, filter: 'contains', suggest: true, highlightFirst: true,
        dataSource: Common.DataSource.Local({
            data: [],
        }),
    };

    $scope.numericSortOrderOptions = {
        format: '#', spinners: false, culture: 'en-US', step: 1, decimals: 0, min: 0,
        change: function (e) {
            var firstVendorID = _OPSAppointment_DIRouteMasterHasDN.Data._dataHasDNVendor.length > 0 ? _OPSAppointment_DIRouteMasterHasDN.Data._dataHasDNVendor[0].ID : -1;
            var firstVehicleCode = _OPSAppointment_DIRouteMasterHasDN.Data._dataHasDNVehicle.length > 0 ? _OPSAppointment_DIRouteMasterHasDN.Data._dataHasDNVehicle[0].RegNo : '';
            var firstVendorName = _OPSAppointment_DIRouteMasterHasDN.Data._dataHasDNVendor.length > 0 ? _OPSAppointment_DIRouteMasterHasDN.Data._dataHasDNVendor[0].CustomerName : '';

            var tr = $(e.sender.element).closest('tr');
            var dataItem = $scope.gridHasDN.dataItem(tr);
            var sort = e.sender.element.val() == '' ? 0 : parseInt(e.sender.element.val());
            if (!Common.HasValue(_OPSAppointment_DIRouteMasterHasDN.Data._dataHasDNSort[sort]))
                _OPSAppointment_DIRouteMasterHasDN.Data._dataHasDNSort[sort] = { 'TOMasterID': -1, 'CreateSortOrder': sort, 'CreateVendorID': firstVendorID, 'CreateVendorName': firstVendorName, 'CreateVehicleCode': firstVehicleCode, 'CreateDriverName': '', 'CreateTelephone': '', 'CreateDateTime': new Date(), 'Kg': 0, 'IsChange': true, 'TypeID': 1, 'MaxWeight': 0, 'MaxCBM': 0 };
            if (sort == 0 || _OPSAppointment_DIRouteMasterHasDN.Data._dataHasDNSort[sort].TypeID < 2) {
                dataItem.CreateSortOrder = sort;
            }

            _OPSAppointment_DIRouteMasterHasDN.Data._dataHasDN = $scope.gridHasDNOptions.dataSource.data();
            $scope.ReloadSort();
            $scope.gridHasDNOptions.dataSource.data(_OPSAppointment_DIRouteMasterHasDN.Data._dataHasDN);
        }
    };

    // Format cho textbox Kg
    $scope.numericKgOptions = {
        min: 0,
        decimals: Common.Number.DI_Decimals,
        culture: "en-US",
        format: "n3",
        spinners: false,
        change: function (e) {
            var tr = $(e.sender.element).closest('tr');
            var dataItem = $scope.gridHasDN.dataItem(tr);
            var sub = dataItem.Kg - this.value();

            if (sub > 0.001) {
                var itemsub = $.extend(true, {}, dataItem);
                itemsub.Kg = sub;
                itemsub.Ton = sub / 1000;
                itemsub.IsOrigin = false;
                dataItem.Kg = this.value();
                dataItem.Ton = dataItem.Kg / 1000;

                _OPSAppointment_DIRouteMasterHasDN.Data._dataHasDN = $scope.gridHasDN.dataSource.data();
                _OPSAppointment_DIRouteMasterHasDN.Data._dataHasDN.push(itemsub);
                $scope.ReloadSort();
                $scope.gridHasDN.dataSource.data(_OPSAppointment_DIRouteMasterHasDN.Data._dataHasDN);
            }
        }
    };

    // Format cho textbox Tấn
    $scope.numericTonOptions = {
        min: 0,
        decimals: Common.Number.DI_Decimals,
        spinners: false,
        culture: "en-US",
        format: "n5",
        change: function (e) {
            var tr = $(e.sender.element).closest('tr');
            var dataItem = $scope.gridHasDN.dataItem(tr);
            var sub = dataItem.Ton - this.value();

            if (sub > 0.001) {
                var itemsub = $.extend(true, {}, dataItem);
                itemsub.Ton = sub;
                itemsub.Kg = sub * 1000;
                itemsub.IsOrigin = false;
                dataItem.Ton = this.value();
                dataItem.Kg = dataItem.Ton * 1000;

                _OPSAppointment_DIRouteMasterHasDN.Data._dataHasDN = $scope.gridHasDN.dataSource.data();
                _OPSAppointment_DIRouteMasterHasDN.Data._dataHasDN.push(itemsub);
                $scope.ReloadSort();
                $scope.gridHasDN.dataSource.data(_OPSAppointment_DIRouteMasterHasDN.Data._dataHasDN);
            }
        }
    };

    // Load Combobx danh sách order DN
    Common.Services.Call($http, {
        url: Common.Services.url.OPS,
        method: _OPSAppointment_DIRouteMasterHasDN.URL.OrderListDN,
        success: function (res) {
            if (Common.HasValue(res)) {
                $timeout(function () {
                    _OPSAppointment_DIRouteMasterHasDN.Data._createListDN = res;
                    var data = [];
                    $.each(_OPSAppointment_DIRouteMasterHasDN.Data._createListDN, function (i, v) {
                        data.push(v.DNCode);
                    });
                    $scope.cboOrderListDNOptions.dataSource.data(data);
                }, 1);
            }
        }
    });

    // Load danh sách Vendor
    Common.Services.Call($http, {
        url: Common.Services.url.OPS,
        method: _OPSAppointment_DIRouteMasterHasDN.URL.VehicleListVendor,
        data: {},
        success: function (res) {
            if (Common.HasValue(res)) {
                $timeout(function () {
                    _OPSAppointment_DIRouteMasterHasDN.Data._dataHasDNVendor = res;
                    if (_OPSAppointment_DIRouteMasterHasDN.Data._dataHasDNVendor.length > 0 && _OPSAppointment_DIRouteMasterHasDN.Data._dataHasDNVehicle.length > 0)
                        $scope.LoadDataVehicleVendor();
                }, 1);
            }
        }
    });

    // Load danh sách xe
    Common.Services.Call($http, {
        url: Common.Services.url.OPS,
        method: _OPSAppointment_DIRouteMasterHasDN.URL.VehicleListVehicle,
        data: {},
        success: function (res) {
            if (Common.HasValue(res)) {
                $timeout(function () {
                    _OPSAppointment_DIRouteMasterHasDN.Data._dataHasDNVehicle = res;
                    if (_OPSAppointment_DIRouteMasterHasDN.Data._dataHasDNVendor.length > 0 && _OPSAppointment_DIRouteMasterHasDN.Data._dataHasDNVehicle.length > 0)
                        $scope.LoadDataVehicleVendor();
                }, 1);
            }
        }
    });

    // Load danh sách tài xế
    Common.Services.Call($http, {
        url: Common.Services.url.OPS,
        method: _OPSAppointment_DIRouteMasterHasDN.URL.VehicleListDriver,
        success: function (res) {
            Common.Services.Error(res, function (res) {
                _OPSAppointment_DIRouteMasterHasDN.Data._dataDriver = [];
                $.each(res, function (i, v) {
                    _OPSAppointment_DIRouteMasterHasDN.Data._dataDriver.push({ 'ID': v.ID, 'Text': v.LastName + ' ' + v.FirstName + ' (' + v.EmployeeCode + ')', 'DriverName': v.LastName + ' ' + v.FirstName, 'DriverTelNo': v.Cellphone });
                });
            });
        }
    });

    // Load danh sách phân tài
    Common.Services.Call($http, {
        url: Common.Services.url.OPS,
        method: _OPSAppointment_DIRouteMasterHasDN.URL.FLMPlaning,
        success: function (res) {
            _OPSAppointment_DIRouteMasterHasDN.Data.FLMPlaning = res;
        }
    });
    $scope.gridHasDNOptions = {
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
            { field: 'CreateSortOrder', width: '50px', template: '<form class="cus-form-enter" ng-submit="SortEnter_Click($event)" ng-show="dataItem.TypeID != 2"><input kendo-numeric-text-box class="txtCreateSortOrder" value="#=CreateSortOrder==0? 0:CreateSortOrder#" k-options="numericSortOrderOptions" style="width:100%" /></form>', headerTemplate: '<a class="k-button" href="/" ng-click="Expand_Click($event,gridHasDN)"><i class="fa fa-minus"></i></a>', groupHeaderTemplate: '<span class="HasDNGridGroup" tabid="#=value#"></span>', sortable: false, filterable: false, menu: false },
            { field: 'SOCode', width: '80px', title: 'SO', sortable: true, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'IsOrigin', width: '30px', title: 'g', template: '<input type="checkbox" ng-model="dataItem.IsOrigin" />', sortable: false, filterable: false },
            { field: 'DNCode', width: '90px', title: 'DN', sortable: true, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { title: 'Gộp', width: '45px', template: '<a href="" class="k-button btnAdd" ng-click="Merge_Click($event)" style="display:none">M</a><a href="" class="k-button btnAddSave" ng-click="MergeSave_Click($event)" style="display:none">S</a><input type="checkbox" class="chkAdd" style="display:none"/>', filterable: false, menu: false, sortable: false },
            { field: 'Kg', width: '80px', title: 'KG', template: '<form class="cus-form-enter"><input kendo-numeric-text-box value="dataItem.Kg" k-options="numericKgOptions" data-k-max="dataItem.Kg" style="width:100%" /></form>', sortable: true, filterable: { cell: { operator: 'eq', showOperators: false } } },
            { field: 'Ton', width: '80px', title: 'Tấn', template: '<form class="cus-form-enter"><input kendo-numeric-text-box value="dataItem.Ton" k-options="numericTonOptions" data-k-max="dataItem.Ton" style="width:100%" /></form>', sortable: true, filterable: { cell: { operator: 'eq', showOperators: false } } },
            { field: 'DistrictName', width: '100px', title: 'Quận / Huyện', sortable: true, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'Address', width: '200px', title: 'Địa chỉ', sortable: true, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'ProvinceName', width: '100px', title: 'Tỉnh / Thành', sortable: true, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'TranferItem', width: '80px', title: 'Vận chuyển', sortable: true, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'RequestDateEmpty', width: '100px', title: 'Ngày gửi', template: "#=Common.Date.FromJsonDDMMHM(RequestDate)#", sortable: true, filterable: { cell: { operator: 'eq', showOperators: false } } },
            { field: 'OrderCode', width: '80px', title: 'List', sortable: true, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'CBM', width: '60px', title: 'Khối', template: '#=Common.Number.ToNumber1(CBM)#', sortable: true, filterable: { cell: { operator: 'eq', showOperators: false } } },
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
        toolbar: kendo.template($('#OPSAppointment_DIRouteMasterHasDN_gridToolbar').html()),
        filterable: { mode: 'row' },
        dataBound: function () {
            Common.Log("GridDataBound");
            if (Common.HasValue(this) && Common.HasValue(this.tbody)) {
                if (_OPSAppointment_DIRouteMasterHasDN.Data._dataHasDNGroupProduct.length == 0)
                    $scope.ReloadSort();

                this.element.find('tr[role="row"]').each(function () {
                    var btnAdd = $(this).find('.btnAdd');
                    if (Common.HasValue($scope.gridHasDN)) {
                        var dataItem = $scope.gridHasDN.dataItem(this);
                        if (Common.HasValue(dataItem)) {
                            if (_OPSAppointment_DIRouteMasterHasDN.Data._dataHasDNGroupProduct[dataItem.ID].length > 1)
                                btnAdd.show();
                        }
                    }
                });

                this.element.find('.HasDNGridGroup').each(function () {
                    var sort = parseInt($(this).attr('tabid'));
                    var item = _OPSAppointment_DIRouteMasterHasDN.Data._dataHasDNSort[sort];

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
                                    '<a href="/" class="btnHasDNGroup">' + item.CreateVendorName + ' - ' + item.CreateVehicleCode + ' - ' + drivername + ' - ' + telephone + ' - ' + Common.Date.FromJsonDMYHM(item.CreateDateTime) + '</a>' +
                                    '<input style="display:none;width:200px" class="cboHasDNVendor cus-combobox" placeholder="Nhà vận tải" value="' + item.CreateVendorID + '" /><span style="display:none" class="lblHasDNSplit"> - </span>' +
                                    '<input style="display:none;width:200px" id="cboHasDNVehicle" class="cboHasDNVehicle cus-combobox" placeholder="Nhập số xe" value="' + item.CreateVehicleCode + '" /><span style="display:none" class="lblHasDNSplit"> - </span>' +
                                    '<input style="display:none;width:50px" type="number" min="0" class="txtHasDNMaxWeight k-textbox" placeholder="T.Tải" value="" /><span style="display:none" class="lblHasDNSplit vehicle"> - </span>' +
                                    '<input style="display:none;width:50px" type="number" min="0" class="txtHasDNMaxCBM k-textbox" placeholder="S.Khối" value="" /><span style="display:none" class="lblHasDNSplit vehicle"> - </span>' +
                                    '<input style="display:none;width:150px;" type="text" class="txtHasDNDriverName k-textbox" placeholder="Nhập tài xế" value="' + drivername + '" /><span style="display:none" class="lblHasDNSplit"> - </span>' +
                                    '<input style="display:none;width:150px" type="text" class="txtHasDNTelephone k-textbox" placeholder="Nhập điện thoại" value="' + telephone + '"/><span style="display:none" class="lblHasDNSplit"> - </span>' +
                                    '<input style="display:none;width:150px" class="txtCreateDateTime"  value="' + Common.Date.FromJsonDMYHM(item.CreateDateTime) + '"/>' + strMaxWeight + strOverLoad);
                            }
                            else {
                                $(this).html('&nbsp;&nbsp;&nbsp; Chuyến ' + sort + ' - Kg: ' + Common.Number.ToNumber1(item.Kg) + ' - ' +
                                    '<a href="/" class="btnHasDNGroup">' + item.CreateVendorName + ' - ' + item.CreateVehicleCode + ' - ' + drivername + ' - ' + telephone + ' - ' + Common.Date.FromJsonDMYHM(item.CreateDateTime) + '</a>' +
                                    '<input style="display:none;width:200px" class="cboHasDNVendor cus-combobox" placeholder="Nhà vận tải" value="' + item.CreateVendorID + '" /><span style="display:none" class="lblHasDNSplit"> - </span>' +
                                    '<input style="display:none;width:200px" id="cboHasDNVehicle" class="cboHasDNVehicle cus-combobox" placeholder="Nhập số xe" value="' + item.CreateVehicleCode + '" /><span style="display:none" class="lblHasDNSplit"> - </span>' +
                                    '<input style="display:none;width:50px" type="number" min="0" class="txtHasDNMaxWeight k-textbox" placeholder="T.Tải" value="" /><span style="display:none" class="lblHasDNSplit vehicle"> - </span>' +
                                    '<input style="display:none;width:50px" type="number" min="0" class="txtHasDNMaxCBM k-textbox" placeholder="S.Khối" value="" /><span style="display:none" class="lblHasDNSplit vehicle"> - </span>' +
                                    '<input style="display:none;width:150px;" type="text" class="txtHasDNDriverName k-textbox" placeholder="Nhập tài xế" value="' + drivername + '" /><span style="display:none" class="lblHasDNSplit"> - </span>' +
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
        Common.Controls.Grid.ReorderColumns({ Grid: $scope.gridHasDN, CookieName: 'OPS_DIAppointment_Route_gridHasDN' });
    }, 10);

    $scope.LoadDataVehicleVendor = function () {
        var lstVendor = _OPSAppointment_DIRouteMasterHasDN.Data._dataHasDNVendor;
        var lstVehicle = _OPSAppointment_DIRouteMasterHasDN.Data._dataHasDNVehicle;
        _OPSAppointment_DIRouteMasterHasDN.Data._dataHasDNVehicleVEN = [];
        _OPSAppointment_DIRouteMasterHasDN.Data._dataHasDNVehicleHome = [];
        $.each(lstVehicle, function (idx, vehicle) {
            if (vehicle.RegNo != "[Chờ nhập xe]") {
                for (var i = 0; i < vehicle.ListVendorID.length; i++) {
                    var vendorid = vehicle.ListVendorID[i];
                    if (!Common.HasValue(_OPSAppointment_DIRouteMasterHasDN.Data._dataHasDNVehicleVEN[vendorid]))
                        _OPSAppointment_DIRouteMasterHasDN.Data._dataHasDNVehicleVEN[vendorid] = [];
                    _OPSAppointment_DIRouteMasterHasDN.Data._dataHasDNVehicleVEN[vendorid].push(vehicle);
                }
                if (vehicle.IsOwn) {
                    _OPSAppointment_DIRouteMasterHasDN.Data._dataHasDNVehicleHome.push(vehicle);
                }
            } else {
                _OPSAppointment_DIRouteMasterHasDN.Data._dataHasDNVehicleHome.push(vehicle);
                for (var i = 0; i < lstVendor.length; i++) {
                    var vendorid = lstVendor[i].ID;
                    if (!Common.HasValue(_OPSAppointment_DIRouteMasterHasDN.Data._dataHasDNVehicleVEN[vendorid]))
                        _OPSAppointment_DIRouteMasterHasDN.Data._dataHasDNVehicleVEN[vendorid] = [];
                    _OPSAppointment_DIRouteMasterHasDN.Data._dataHasDNVehicleVEN[vendorid].push(vehicle);
                }
            }
        });
    }

    $scope.GridFilter = function () {
        
    };

    $scope.LoadData = function () {
        Common.Log('LoadData');
        $rootScope.IsLoading = true;
        $scope.Summary = '';
        var i = 0;
        while (i < _OPSAppointment_DIRouteMasterHasDN.Data._dataHasDNVendor.length) {
            if (_OPSAppointment_DIRouteMasterHasDN.Data._dataHasDNVendor[i].ID < 0)
                _OPSAppointment_DIRouteMasterHasDN.Data._dataHasDNVendor.splice(i, 1);
            else
                i++;
        }
        _OPSAppointment_DIRouteMasterHasDN.Data._dataHasDNVendor.splice(0, 0, { 'ID': -1, 'CustomerName': 'Xe nhà' });
        var i = 0;
        while (i < _OPSAppointment_DIRouteMasterHasDN.Data._dataHasDNVehicle.length) {
            if (_OPSAppointment_DIRouteMasterHasDN.Data._dataHasDNVehicle[i].ID < 0)
                _OPSAppointment_DIRouteMasterHasDN.Data._dataHasDNVehicle.splice(i, 1);
            else
                i++;
        }

        _OPSAppointment_DIRouteMasterHasDN.Data._dataHasDNSort = [];
        _OPSAppointment_DIRouteMasterHasDN.Data._dataHasDNGroupProduct = [];

        var lstFilters = [Common.Request.FilterParamWithOr('TypeID', Common.Request.FilterType.Equal, 1)];
        if ($scope.IsHasDNMonitor)
            lstFilters.push(Common.Request.FilterParamWithOr('TypeID', Common.Request.FilterType.Equal, 2));
        var param = Common.Request.Create({
            Sorts: [], Filters: lstFilters
        });

        if (!$scope.IsCreateByID) {
            Common.Services.Call($http, {
                url: Common.Services.url.OPS,
                method: _OPSAppointment_DIRouteMasterHasDN.URL.List,
                data: { request: param },
                success: function (res) {
                    Common.Services.Error(res, function (res) {
                        _OPSAppointment_DIRouteMasterHasDN.Data._dataHasDNSort[0] = { 'CreateSortOrder': 0, 'CreateVendorID': -1, 'CreateVehicleCode': '', 'CreateDriverName': '', 'CreateTelephone': '', 'Kg': 0, 'MaxWeight': 0, 'MaxCBM': 0 };
                        var datafix = [];
                        var hasMonitor = $scope.IsHasDNMonitor;

                        Common.Services.Call($http, {
                            url: Common.Services.url.OPS,
                            method: _OPSAppointment_DIRouteMasterHasDN.URL.OrderList,
                            data: { request: param },
                            success: function (res) {
                                Common.Services.Error(res, function (res) {
                                    _OPSAppointment_DIRouteMasterHasDN.Data._dataHasDN = [];
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
                                                _OPSAppointment_DIRouteMasterHasDN.Data._dataHasDN.push(v);
                                        }
                                    });
                                    $scope.gridHasDNOptions.dataSource.data(_OPSAppointment_DIRouteMasterHasDN.Data._dataHasDN);
                                    $rootScope.IsLoading = false;
                                });
                            }
                        });

                        var index = 1;
                        $.each(res, function (i, v) {
                            if (hasMonitor || v.TypeID < 2) {
                                v.CreateDateTime = Common.Date.FromJson(v.CreateDateTime);
                                v.Kg = 0;
                                _OPSAppointment_DIRouteMasterHasDN.Data._dataHasDNSort[index] = v;
                                datafix[v.CreateSortOrder] = index;
                                v.CreateSortOrder = index;
                                v.IsChange = false;

                                index++;
                            }
                        });
                    });
                }
            });
        }
        else {
            Common.Services.Call($http, {
                url: Common.Services.url.OPS,
                method: _OPSAppointment_DIRouteMasterHasDN.URL.ListGroupID,
                data: { lstid: _OPSAppointment_DIRouteMasterHasDN.Data._createListID },
                success: function (res) {
                    Common.Services.Error(res, function (res) {
                        _OPSAppointment_DIRouteMasterHasDN.Data._dataHasDNSort[0] = { 'CreateSortOrder': 0, 'CreateVendorID': -1, 'CreateVehicleCode': '', 'CreateDriverName': '', 'CreateTelephone': '', 'Kg': 0, 'MaxWeight': 0, 'MaxCBM': 0 };
                        var datafix = [];
                        var hasMonitor = $scope.IsHasDNMonitor;

                        Common.Services.Call($http, {
                            url: Common.Services.url.OPS,
                            method: _OPSAppointment_DIRouteMasterHasDN.URL.OrderListByGroupID,
                            data: { lstid: _OPSAppointment_DIRouteMasterHasDN.Data._createListID },
                            success: function (res) {
                                Common.Services.Error(res, function (res) {
                                    _OPSAppointment_DIRouteMasterHasDN.Data._dataHasDN = [];
                                    $.each(res, function (i, v) {
                                        if (hasMonitor || v.TypeID < 2) {
                                            if (Common.HasValue(v.RequestDate)) {
                                                v.RequestDate = Common.Date.FromJson(v.RequestDate);
                                                v.RequestDateEmpty = Common.Date.Date(v.RequestDate);
                                            }
                                            v.Kg = v.Ton * 1000;

                                            if (v.CreateSortOrder > 0)
                                                v.CreateSortOrder = datafix[v.CreateSortOrder];

                                            _OPSAppointment_DIRouteMasterHasDN.Data._dataHasDN.push(v);
                                        }
                                    });
                                    $scope.gridHasDNOptions.dataSource.data(_OPSAppointment_DIRouteMasterHasDN.Data._dataHasDN);
                                    $rootScope.IsLoading = false;
                                });
                            }
                        });

                        var index = 1;
                        $.each(res, function (i, v) {
                            if (hasMonitor || v.TypeID < 2) {
                                v.CreateDateTime = Common.Date.FromJson(v.CreateDateTime);
                                v.Kg = 0;
                                _OPSAppointment_DIRouteMasterHasDN.Data._dataHasDNSort[index] = v;
                                datafix[v.CreateSortOrder] = index;
                                v.CreateSortOrder = index;
                                v.IsChange = false;

                                index++;
                            }
                        });
                    });
                }
            });
        }
    };

    $scope.ReloadSort = function ($event) {
        Common.Log("ReloadSort");
        $scope.IsShowCombine = false;

        var totalsort = 0;
        var totalkg = 0;
        var totalvehicle = 0;
        $scope.Summary = '';

        $.each(_OPSAppointment_DIRouteMasterHasDN.Data._dataHasDNSort, function (i, v) {
            if (Common.HasValue(v)) {
                v.Kg = 0;
                if (v.CreateSortOrder > 0)
                    totalsort++;
            }
        });

        _OPSAppointment_DIRouteMasterHasDN.Data._dataHasDNGroupProduct = [];
        $.each(_OPSAppointment_DIRouteMasterHasDN.Data._dataHasDN, function (i, v) {
            if (v.Kg > 0) {
                if (Common.HasValue(_OPSAppointment_DIRouteMasterHasDN.Data._dataHasDNSort[v.CreateSortOrder]))
                    _OPSAppointment_DIRouteMasterHasDN.Data._dataHasDNSort[v.CreateSortOrder].Kg += v.Kg;
                else {
                    var item = { 'CreateSortOrder': v.CreateSortOrder, 'CreateVendorID': v.VendorOfVehicleID, 'CreateVendorName': v.VendorOfVehicleName, 'CreateVehicleCode': v.VehicleCode, 'CreateDriverName': v.DriverName, 'CreateTelephone': v.DriverTelNo, 'Kg': v.Kg, 'CreateVehicleID': v.VehicleID, 'TOMasterID': v.TOMasterID, 'CreateDateTime': v.CreateDateTime, 'TypeID': v.TypeID, 'MaxWeight': v.MaxWeightCal, 'MaxCBM': v.MaxCBM };
                    _OPSAppointment_DIRouteMasterHasDN.Data._dataHasDNSort[v.CreateSortOrder] = item;
                }
                if (v.CreateSortOrder > 0)
                    totalvehicle += v.Kg;
                totalkg += v.Kg;
            }

            if (!Common.HasValue(_OPSAppointment_DIRouteMasterHasDN.Data._dataHasDNGroupProduct[v.ID]))
                _OPSAppointment_DIRouteMasterHasDN.Data._dataHasDNGroupProduct[v.ID] = [];
            _OPSAppointment_DIRouteMasterHasDN.Data._dataHasDNGroupProduct[v.ID].push(v);
        });

        $scope.Summary = 'Kg: ' + Common.Number.ToNumber1(totalvehicle) + ' / ' + Common.Number.ToNumber1(totalkg) + ' - Số chuyến: ' + totalsort;
    };

    $scope.Approved_Click = function ($event) {
        Common.Log('Approved_Click');
        $event.preventDefault();

        var data = []; var error = [];
        $scope.gridHasDN.tbody.find('.HasDNGridGroup').each(function () {
            var sort = parseInt($(this).attr('tabid'));
            var item = _OPSAppointment_DIRouteMasterHasDN.Data._dataHasDNSort[sort];
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
                            method: _OPSAppointment_DIRouteMasterHasDN.URL.Approved,
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
        $scope.gridHasDN.tbody.find('.HasDNGridGroup').each(function () {
            var sort = parseInt($(this).attr('tabid'));
            var item = _OPSAppointment_DIRouteMasterHasDN.Data._dataHasDNSort[sort];
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
                        method: _OPSAppointment_DIRouteMasterHasDN.URL.UnApproved,
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
        $scope.gridHasDN.tbody.find('.HasDNGridGroup').each(function () {
            var sort = parseInt($(this).attr('tabid'));
            var item = _OPSAppointment_DIRouteMasterHasDN.Data._dataHasDNSort[sort];
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
                        method: _OPSAppointment_DIRouteMasterHasDN.URL.Delete,
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
        $.each(_OPSAppointment_DIRouteMasterHasDN.Data._dataHasDNSort, function (i, v) {
            if (Common.HasValue(v)) {
                if (v.CreateSortOrder > 0 && (v.CreateVehicleCode == '')) {
                    error += ', ' + v.CreateSortOrder;
                    flag = false;
                }
            }
        });

        var data = [];
        $.each(_OPSAppointment_DIRouteMasterHasDN.Data._dataHasDN, function (i, v) {
            v.Ton = v.Kg / 1000;

            if (v.CreateSortOrder > 0 && _OPSAppointment_DIRouteMasterHasDN.Data._dataHasDNSort[v.CreateSortOrder].TypeID < 2 && v.Kg > 0) {
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
                    $.each(_OPSAppointment_DIRouteMasterHasDN.Data._dataHasDNSort, function (i, v) {
                        if (Common.HasValue(v)) {
                            if (v.CreateSortOrder > 0 && v.TypeID < 2) {
                                if (v.Kg == 0 && v.TOMasterID > 0)
                                    dataDel.push(v.TOMasterID);
                                else
                                    dataAdd.push(v);
                            }
                        }
                    });
                    _OPSAppointment_DIRouteMasterHasDN.Data._dataHasDNSort = dataAdd;
                    _OPSAppointment_DIRouteMasterHasDN.Data._dataHasDN = data;

                    Common.Services.Call($http, {
                        url: Common.Services.url.OPS,
                        method: _OPSAppointment_DIRouteMasterHasDN.URL.Delete,
                        data: { lst: dataDel },
                        success: function (res) {
                            Common.Services.Error(res, function (res) {
                                Common.Services.Call($http, {
                                    url: Common.Services.url.OPS,
                                    method: _OPSAppointment_DIRouteMasterHasDN.URL.Save,
                                    data: { lstOrder: _OPSAppointment_DIRouteMasterHasDN.Data._dataHasDN, lstVehicle: _OPSAppointment_DIRouteMasterHasDN.Data._dataHasDNSort },
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

        _OPSAppointment_DIRouteMasterHasDN.Data._createListDN = [];
        _OPSAppointment_DIRouteMasterHasDN.Data._createListID = [];
        _OPSAppointment_DIRouteMasterHasDN.Data._dataHasDNGroupProduct = [];
        _OPSAppointment_DIRouteMasterHasDN.Data._dataHasDN = [];
        _OPSAppointment_DIRouteMasterHasDN.Data._dataHasDNSort = [];
        _OPSAppointment_DIRouteMasterHasDN.Data._dataHasDNVendor = [];
        _OPSAppointment_DIRouteMasterHasDN.Data._dataHasDNVehicle = [];
        $state.go('main.OPSAppointment.DIRoute');
    };

    $scope.DNEnter_Click = function ($event) {
        Common.Log('DNEnter_Click');
        if (Common.HasValue($event))
            $event.preventDefault();

        if (Common.HasValue($scope.DNCode) && $scope.DNCode != '') {
            var id = -1;
            $.each(_OPSAppointment_DIRouteMasterHasDN.Data._createListDN, function (i, v) {
                if (v.DNCode == $scope.DNCode)
                    id = v.TOMasterID;
            });
            if (id > 0) {
                if (_OPSAppointment_DIRouteMasterHasDN.Data._createListID.indexOf(id) < 0) {
                    _OPSAppointment_DIRouteMasterHasDN.Data._createListID.push(id);

                    Common.Services.Call($http, {
                        url: Common.Services.url.OPS,
                        method: _OPSAppointment_DIRouteMasterHasDN.URL.OrderListByGroupID,
                        data: { lstid: [id] },
                        success: function (res) {
                            if (Common.HasValue(res)) {
                                $.each(res, function (i, v) {
                                    if (Common.HasValue(v.RequestDate)) {
                                        v.RequestDate = Common.Date.FromJson(v.RequestDate);
                                        v.RequestDateEmpty = Common.Date.Date(v.RequestDate);
                                    }
                                    v.Kg = v.Ton * 1000;

                                    if (!Common.HasValue(_OPSAppointment_DIRouteMasterHasDN.Data._dataHasDNGroupProduct[v.ID]))
                                        _OPSAppointment_DIRouteMasterHasDN.Data._dataHasDNGroupProduct[v.ID] = [];
                                    _OPSAppointment_DIRouteMasterHasDN.Data._dataHasDNGroupProduct[v.ID].push(v);

                                    _OPSAppointment_DIRouteMasterHasDN.Data._dataHasDN.push(v);
                                });

                                $scope.ReloadSort();
                                $scope.gridHasDNOptions.dataSource.data(_OPSAppointment_DIRouteMasterHasDN.Data._dataHasDN);
                            }
                        }
                    });
                }
            }
        }
    };

    $scope.SortEnter_Click = function ($event) {
        Common.Log('SortEnter_Click');
        if (Common.HasValue($event))
            $event.preventDefault();

        if (Common.HasValue(this.dataItem)) {
            var firstVendorID = _OPSAppointment_DIRouteMasterHasDN.Data._dataHasDNVendor.length > 0 ? _OPSAppointment_DIRouteMasterHasDN.Data._dataHasDNVendor[0].ID : -1;
            var firstVehicleCode = _OPSAppointment_DIRouteMasterHasDN.Data._dataHasDNVehicle.length > 0 ? _OPSAppointment_DIRouteMasterHasDN.Data._dataHasDNVehicle[0].RegNo : '';
            var firstVendorName = _OPSAppointment_DIRouteMasterHasDN.Data._dataHasDNVendor.length > 0 ? _OPSAppointment_DIRouteMasterHasDN.Data._dataHasDNVendor[0].CustomerName : '';

            var dataItem = this.dataItem;
            var sort = dataItem.CreateSortOrder;
            if (!Common.HasValue(_OPSAppointment_DIRouteMasterHasDN.Data._dataHasDNSort[sort]))
                _OPSAppointment_DIRouteMasterHasDN.Data._dataHasDNSort[sort] = { 'TOMasterID': -1, 'CreateSortOrder': sort, 'CreateVendorID': firstVendorID, 'CreateVendorName': firstVendorName, 'CreateVehicleCode': firstVehicleCode, 'CreateDriverName': '', 'CreateTelephone': '', 'CreateDateTime': new Date(), 'Kg': 0, 'IsChange': true, 'TypeID': 1, 'MaxWeight': 0, 'MaxCBM': 0 };
            if (sort == 0 || _OPSAppointment_DIRouteMasterHasDN.Data._dataHasDNSort[sort].TypeID < 2) {
                dataItem.CreateSortOrder = sort;
            }

            _OPSAppointment_DIRouteMasterHasDN.Data._dataHasDN = $scope.gridHasDNOptions.dataSource.data();
            $scope.ReloadSort();
            $scope.gridHasDNOptions.dataSource.data(_OPSAppointment_DIRouteMasterHasDN.Data._dataHasDN);
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
        $scope.gridHasDN.tbody.find('tr[role="row"] .btnAdd').hide();
        // Hiện nút Save
        var btnAddSave = $($($event.currentTarget).closest('tr')).find('.btnAddSave').show();
        // Hiện nút Hủy gom hàng
        $scope.IsShowCombine = true;
        // Lấy ra dataItem hiện tại
        var dataItem = this.dataItem;

        $.each(_OPSAppointment_DIRouteMasterHasDN.Data._dataHasDNGroupProduct[dataItem.ID], function (i, v) {
            if (v.uid != dataItem.uid) {
                var tr = $scope.gridHasDN.tbody.find("tr[data-uid='" + v.uid + "']");
                var chkAdd = $(tr).find('.chkAdd');
                $(chkAdd).show();
            }
        });
    };

    $scope.MergeSave_Click = function ($event) {
        Common.Log("MergeSave_Click");
        $event.preventDefault();

        var dataItem = this.dataItem;
        $.each(_OPSAppointment_DIRouteMasterHasDN.Data._dataHasDNGroupProduct[dataItem.ID], function (i, v) {
            if (v.uid != dataItem.uid) {
                var tr = $scope.gridHasDN.tbody.find("tr[data-uid='" + v.uid + "']");
                var chkAdd = $(tr).find('.chkAdd');

                if ($(chkAdd).prop('checked') == true) {
                    dataItem.Kg += v.Kg;
                    dataItem.Ton += v.Kg / 1000;
                    var index = _OPSAppointment_DIRouteMasterHasDN.Data._dataHasDN.indexOf(v);
                    _OPSAppointment_DIRouteMasterHasDN.Data._dataHasDN.splice(index, 1);
                }
            }
        });
        $scope.ReloadSort();
        $scope.gridHasDN.dataSource.data(_OPSAppointment_DIRouteMasterHasDN.Data._dataHasDN);
    };

    $scope.MergeCancel_Click = function ($event) {
        Common.Log("MergeCancel_Click");
        $event.preventDefault();

        $scope.gridHasDN.dataSource.data(_OPSAppointment_DIRouteMasterHasDN.Data._dataHasDN);
        $scope.IsShowCombine = false;
    };

    $scope.TonEnter_Click = function ($event) {
        Common.Log('TonEnter_Click');
        if (Common.HasValue($event))
            $event.preventDefault();


    };

    $scope.CreateHasDN_Click = function ($event) {
        Common.Log('CreateHasDN_Click');

        // Lập lệnh đã có DN --> ko hiện các lệnh đã lập trước đó
        if ($scope.IsShowTxtDN) {
            $scope.IsCreateByID = true;

            Common.Services.Call($http, {
                url: Common.Services.url.OPS,
                method: _OPSAppointment_DIRouteMasterHasDN.URL.OrderListDN,
                success: function (res) {
                    Common.Services.Error(res, function (res) {
                        _OPSAppointment_DIRouteMasterHasDN.Data._createListDN = res;
                        var data = [];
                        $.each(_OPSAppointment_DIRouteMasterHasDN.Data._createListDN, function (i, v) {
                            data.push(v.DNCode);
                        });
                        $scope.cboOrderListDNOptions.dataSource.data(data);
                    });
                }
            });
        } else {
            // Lập lệnh -- hiển thị sẵn các lệnh đã lập trc đó
            $scope.IsCreateByID = false;
        }
        _OPSAppointment_DIRouteMasterHasDN.Data._createListID = [];
        $scope.GridFilter();
        $scope.LoadData();
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
        var item = $scope.gridHasDN.dataItem(tr);
        var dataVehicle = [];
        var sort = _OPSAppointment_DIRouteMasterHasDN.Data._dataHasDNSort[item.CreateSortOrder];
        
        if (sort.CreateVendorID == null || sort.CreateVendorID == 0 || sort.CreateVendorID == -1) {
            dataVehicle = _OPSAppointment_DIRouteMasterHasDN.Data._dataHasDNVehicleHome;
        } else
        {
            dataVehicle = _OPSAppointment_DIRouteMasterHasDN.Data._dataHasDNVehicleVEN[sort.CreateVendorID];
        }
        
        tr.find('input.cboHasDNVendor').kendoComboBox({
            index: 0, autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true,
            dataTextField: 'CustomerName', dataValueField: 'ID',
            dataSource: Common.DataSource.Local({
                data: _OPSAppointment_DIRouteMasterHasDN.Data._dataHasDNVendor,
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
                var item = _OPSAppointment_DIRouteMasterHasDN.Data._dataHasDNSort[sort];
                var sel = e.sender.dataItem();
                var CreateVendorID = sel != null ? sel.ID : 0;
                if (item.CreateVendorID != CreateVendorID) {
                    item.CreateVendorID = CreateVendorID;

                    var cboVehicle = $(span).find("#cboHasDNVehicle").data("kendoComboBox");
                    cboVehicle.dataSource.data([]);
                    var txtDriver = $(span).find("input.txtHasDNDriverName").data("kendoAutoComplete");
                    var dataDriver = [];
                    
                    if (item.CreateVendorID == null || item.CreateVendorID == 0 || item.CreateVendorID == -1) {
                        cboVehicle.dataSource.data(_OPSAppointment_DIRouteMasterHasDN.Data._dataHasDNVehicleHome);
                        dataDriver = _OPSAppointment_DIRouteMasterHasDN.Data._dataDriver;
                    } else
                        cboVehicle.dataSource.data(_OPSAppointment_DIRouteMasterHasDN.Data._dataHasDNVehicleVEN[item.CreateVendorID]);

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
                index: 0, autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true, value: _OPSAppointment_DIRouteMasterHasDN.Data._dataHasDNSort[item.CreateSortOrder].CreateVehicleCode,
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
                    var item = _OPSAppointment_DIRouteMasterHasDN.Data._dataHasDNSort[sort];
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

                    $.each(_OPSAppointment_DIRouteMasterHasDN.Data._dataHasDNVehicle, function (i, v) {
                        if (item.CreateVehicleCode == v.RegNo) {
                            driverName = v.DriverName;
                            driverTel = v.Cellphone;
                            item.MaxWeight = v.MaxWeight;
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

            cbo.text(_OPSAppointment_DIRouteMasterHasDN.Data._dataHasDNSort[item.CreateSortOrder].CreateVehicleCode);
        });
        
        tr.find('input.txtHasDNDriverName').kendoAutoComplete({
            autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true, placeholder: "Nhập tài xế",
            dataTextField: 'DriverName', dataValueField: 'DriverName',
            dataSource: Common.DataSource.Local({
                data: _OPSAppointment_DIRouteMasterHasDN.Data._dataDriver,
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
                var item = _OPSAppointment_DIRouteMasterHasDN.Data._dataHasDNSort[sort];
                item.CreateDriverName = this.value();
                item.IsChange = true;
            }
        });

        tr.find('input.txtHasDNMaxWeight').change(function (e) {
            var span = $(this).closest('span.HasDNGridGroup');
            sort = $(span).attr('tabid');
            var item = _OPSAppointment_DIRouteMasterHasDN.Data._dataHasDNSort[sort];
            item.MaxWeight = $(this).val();
            item.IsChange = true;
        });

        tr.find('input.txtHasDNMaxCBM').change(function (e) {
            var span = $(this).closest('span.HasDNGridGroup');
            sort = $(span).attr('tabid');
            var item = _OPSAppointment_DIRouteMasterHasDN.Data._dataHasDNSort[sort];
            item.MaxCBM = $(this).val();
            item.IsChange = true;
        });

        tr.find('input.txtHasDNTelephone').change(function (e) {
            var span = $(this).closest('span.HasDNGridGroup');
            sort = $(span).attr('tabid');
            var item = _OPSAppointment_DIRouteMasterHasDN.Data._dataHasDNSort[sort];
            item.CreateTelephone = $(this).val();
            item.IsChange = true;
        });

        tr.find('input.txtCreateDateTime').kendoDateTimePicker({
            format: Common.Date.Format.DMYHM,
            timeFormat: Common.Date.Format.HM,
            change: function (e) {
                var span = $(e.sender.element).closest('span.HasDNGridGroup');
                sort = $(span).attr('tabid');
                var item = _OPSAppointment_DIRouteMasterHasDN.Data._dataHasDNSort[sort];
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

    $scope.LoadData();

    $scope.FindDriver = function (item) {
        var obj = {
            ID: -1,
            Name: '',
            Tel: ''
        };
        try {
            if (item.CreateVendorName == 'Xe nhà') {
                $.each(_OPSAppointment_DIRouteMasterHasDN.Data.FLMPlaning, function (i, v) {
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
}]);