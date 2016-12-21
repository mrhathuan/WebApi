/// <reference path="~/Scripts/jquery-1.9.1.intellisense.js" />
/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />

var _OPSAppointment_DIRouteTOMaster = {
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
        List: 'OPS_DIAppointment_Route_MasterList',
        Master_CheckDriver: 'DIAppointment_Route_Master_CheckDriver',
        Master_ChangeMode: 'DIAppointment_Route_Master_ChangeMode',
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
        OfferPL: 'OPS_DIAppointment_Route_Master_OfferPL',
        FLMPlaning: 'OPS_DIAppointment_FLMPlaning',
    },
    Data: {
        _createListDN: [],
        _createListID: [],
        _dataHasDNGroupProduct: [],
        _dataHasDN: [],
        _dataHasDNMaster: [],
        _dataHasDNSort: [],
        _dataHasDNVendor: [],
        _dataHasDNVehicle: [],
        _dataVehicleVEN: [],
        _dataHasDNVehicleVEN: [],
        _dataHasDNVehicleHome: [],
        _dataDriver: [],
        _dataTypeOfView: [],
        _formatMoney: "n0",
        _reloadDataNoDN: false,
        _reloadDataNoDNMaster: false,
        _totalDN: { Ton: 0, CBM: 0 },
        _totalDNMaster: { Ton: 0, CBM: 0 },
        FLMPlaning: [],
        _vehicleTempData: [],
        CookieSearch: 'OPS_Appointment_DIRouteTOMaster_Search',
    }
};

angular.module('myapp').controller('OPSAppointment_DIRouteTOMasterCtrl', ['$rootScope', '$scope', '$http', '$location', '$state', '$timeout', '$compile', function ($rootScope, $scope, $http, $location, $state, $timeout, $compile) {
    if ($rootScope.IsPageComplete != true) return;
    Common.Log('OPSAppointment_DIRouteTOMasterCtrl');
    $rootScope.IsLoading = false;

    $scope.IsFullScreen = false;
    //Hiển thị chi tiết chuyến
    $scope.IsExpand = true;
    $scope.Search = {};
    $scope.Search.ListCustomerID = [];
    $scope.Search.DateFrom = new Date().addDays(-3);
    $scope.Search.DateTo = new Date().addDays(3);
    $scope.Search.TypeOfViewID = 1;
    $scope.gridNoDNMaster = null;
    $scope.gridNoDN = null;

    //Tgian duyệt chuyến danh cho vendor.
    $scope.RateTime = 2;
    $scope.IsHasDNMonitor = false;
    //Tóm tắt
    $scope.Summary = '';
    $scope.SummaryMaster = '';
    //Tình trạng chuyến
    _OPSAppointment_DIRouteTOMaster.Data._dataTypeOfView = [];
    _OPSAppointment_DIRouteTOMaster.Data._dataTypeOfView.push({ ID: 1, TypeOfViewName: 'Đang lập' });
    _OPSAppointment_DIRouteTOMaster.Data._dataTypeOfView.push({ ID: 2, TypeOfViewName: 'Đã duyệt' });
    _OPSAppointment_DIRouteTOMaster.Data._dataTypeOfView.push({ ID: 3, TypeOfViewName: 'Gửi Tender' });
    _OPSAppointment_DIRouteTOMaster.Data._dataTypeOfView.push({ ID: 4, TypeOfViewName: 'Tất cả' });
    // ẩn hiện các nút
    $scope.IsShowApproved = false;
    $scope.IsShowUnApproved = false;
    $scope.IsShowSave = false;
    $scope.IsShowDelete = false;
    $scope.IsShowUnMerge = false;
    $scope.IsShowTendered = false;
    $scope.IsChangeView = false;

    $scope.viewSplitter_Options = {
        orientation: "horizontal",
        panes: [
            { collapsible: true, resizable: true, size: '50%' },
            { collapsible: true, resizable: true, size: '50%', collapsed: false }
        ],
        resize: function (e) {
            //try {
            //   
            //}
            //catch (e) { }
        }
    }

    $scope.mltCustomerOptions = {
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
            Common.Cookie.Set(_OPSAppointment_DIRouteTOMaster.Data.CookieSearch, JSON.stringify($scope.Search));
        }
    };

    $scope.ddlTypeOfViewOptions = {
        autoBind: true, index: 0,
        valuePrimitive: true,
        dataTextField: 'TypeOfViewName',
        dataValueField: 'ID',
        dataSource: Common.DataSource.Local({
            data: _OPSAppointment_DIRouteTOMaster.Data._dataTypeOfView,
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    TypeOfViewName: { type: 'string' }
                }
            }
        }),
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
            var sort = e.sender.element.val() == '' ? 0 : parseInt(e.sender.element.val());
            var tr = $(e.sender.element).closest('tr');
            var dataItem = $scope.gridNoDN.dataItem(tr);
            if (!Common.HasValue(_OPSAppointment_DIRouteTOMaster.Data._dataHasDNSort[sort])) {
                var firstVendorID = _OPSAppointment_DIRouteTOMaster.Data._dataHasDNVendor.length > 0 ? _OPSAppointment_DIRouteTOMaster.Data._dataHasDNVendor[0].ID : -1;
                var firstVehicleCode = '';
                var firstVendorName = _OPSAppointment_DIRouteTOMaster.Data._dataHasDNVendor.length > 0 ? _OPSAppointment_DIRouteTOMaster.Data._dataHasDNVendor[0].CustomerName : '';
                if (firstVendorID == -1) {
                    firstVehicleCode = _OPSAppointment_DIRouteTOMaster.Data._dataHasDNVehicleHome[0].RegNo;
                } else {
                    firstVehicleCode = _OPSAppointment_DIRouteTOMaster.Data._dataHasDNVehicleVEN[firstVendorID].RegNo;
                }
                _OPSAppointment_DIRouteTOMaster.Data._dataHasDNSort[sort] = {
                    'TOMasterID': -1, 'CreateSortOrder': sort, 'CreateVendorID': firstVendorID,
                    'CreateVendorName': firstVendorName, 'CreateVehicleCode': firstVehicleCode,
                    'CreateDriverName': '', 'CreateTelephone': '', 'CreateDateTime': Common.Date.FromJson(dataItem.ETD),
                    'Kg': 0, 'Ton': 0, 'CBM': 0, 'IsChange': true, 'TypeID': 1, 'Credit': 0, 'Debit': 0, 'PL': 0, 'IsChange': true
                };
                _OPSAppointment_DIRouteTOMaster.Data._totalDNMaster.Sort++;
            } 
            // Add vào grid chuyến, remove khỏi grid đơn hàng
            if (sort == 0 || _OPSAppointment_DIRouteTOMaster.Data._dataHasDNSort[sort].TypeID < 2) {
                dataItem.CreateSortOrder = sort;
                // Add vào grid chuyến
                _OPSAppointment_DIRouteTOMaster.Data._dataHasDNMaster = $scope.gridNoDNMaster.dataSource.data();
                _OPSAppointment_DIRouteTOMaster.Data._dataHasDNMaster.push(dataItem);
                // remove khỏi grid đơn hàng
                $scope.gridNoDN.removeRow(tr);
                $timeout(function () {
                    $scope.IsShowSave = true;
                }, 1);
            }
            else {
                $rootScope.Message({
                    Msg: 'Chuyến ' + sort + ' đã giám sát!',
                    NotifyType: Common.Message.NotifyType.WARNING
                });
            }
        }
    };

    $scope.numericSortOrderMasterOptions = {
        format: '#', spinners: false, culture: 'en-US', step: 1, decimals: 0, min: 0,
        change: function (e) {
            var sort = e.sender.element.val() == '' ? 0 : parseInt(e.sender.element.val());
            var tr = $(e.sender.element).closest('tr');
            var dataItem = null;
            $.each($scope.gridNoDNMaster.dataSource.data(), function (i, v) {
                if (v.uid == tr.attr("data-uid")) {
                    dataItem = v;
                    return false;
                }
            });
            
            if (Common.HasValue(dataItem)) {
                _OPSAppointment_DIRouteTOMaster.Data._dataHasDNSort[dataItem.CreateSortOrder].IsChange = true;
                if (!Common.HasValue(_OPSAppointment_DIRouteTOMaster.Data._dataHasDNSort[sort])) {
                    var firstVendorID = _OPSAppointment_DIRouteTOMaster.Data._dataHasDNVendor.length > 0 ? _OPSAppointment_DIRouteTOMaster.Data._dataHasDNVendor[0].ID : -1;
                    var firstVehicleCode = '';
                    var firstVendorName = _OPSAppointment_DIRouteTOMaster.Data._dataHasDNVendor.length > 0 ? _OPSAppointment_DIRouteTOMaster.Data._dataHasDNVendor[0].CustomerName : '';
                    if (firstVendorID == -1) {
                        firstVehicleCode = _OPSAppointment_DIRouteTOMaster.Data._dataHasDNVehicleHome[0].RegNo;
                    } else {
                        firstVehicleCode = _OPSAppointment_DIRouteTOMaster.Data._dataHasDNVehicleVEN[firstVendorID].RegNo;
                    }
                    _OPSAppointment_DIRouteTOMaster.Data._dataHasDNSort[sort] = {
                        'TOMasterID': -1, 'CreateSortOrder': sort, 'CreateVendorID': firstVendorID,
                        'CreateVendorName': firstVendorName, 'CreateVehicleCode': firstVehicleCode,
                        'CreateDriverName': '', 'CreateTelephone': '', 'CreateDateTime': Common.Date.FromJson(dataItem.ETD),
                        'Kg': 0, 'IsChange': true, 'TypeID': 1, 'Credit': 0, 'Debit': 0, 'PL': 0
                    };
                } else {
                    _OPSAppointment_DIRouteTOMaster.Data._dataHasDNSort[sort].IsChange = true;
                }
                _OPSAppointment_DIRouteTOMaster.Data._previousSort = dataItem.CreateSortOrder;
                // remove khỏi grid chuyến, add vào grid đơn hàng
                if (sort <= 0) {
                    var tmp = $.extend(true, {}, dataItem);
                    dataItem.CreateSortOrder = 0;
                    // Add vào grid đơn hàng
                    _OPSAppointment_DIRouteTOMaster.Data._dataHasDN = $scope.gridNoDN.dataSource.data();
                    _OPSAppointment_DIRouteTOMaster.Data._dataHasDN.push(dataItem);
                    // remove khỏi grid chuyến
                    $scope.gridNoDNMaster.removeRow(tr);
                } else {
                    if (_OPSAppointment_DIRouteTOMaster.Data._dataHasDNSort[sort].TypeID < 2) {
                        var tmp = dataItem.toJSON()
                        tmp.CreateSortOrder = sort;
                        // Remove khoi chuyen cu
                        $scope.gridNoDNMaster.removeRow(tr);
                        _OPSAppointment_DIRouteTOMaster.Data._dataHasDNMaster = $scope.gridNoDNMaster.dataSource.data();
                        // Add vao chuyen moi
                        _OPSAppointment_DIRouteTOMaster.Data._dataHasDNMaster.push(tmp);
                    }
                }
                $timeout(function () {
                    $scope.IsShowSave = true;
                }, 1);
            }
        }
    };

    $scope.LoadDataVehicleVendor = function () {
        var lstVendor = _OPSAppointment_DIRouteTOMaster.Data._dataHasDNVendor;
        var lstVehicle = _OPSAppointment_DIRouteTOMaster.Data._dataHasDNVehicle;
        _OPSAppointment_DIRouteTOMaster.Data._dataHasDNVehicleVEN = [];
        _OPSAppointment_DIRouteTOMaster.Data._dataHasDNVehicleHome = [];
        $.each(lstVehicle, function (idx, vehicle) {
            if (vehicle.RegNo != "[Chờ nhập xe]") {
                for (var i = 0; i < vehicle.ListVendorID.length; i++) {
                    var vendorid = vehicle.ListVendorID[i];
                    if (!Common.HasValue(_OPSAppointment_DIRouteTOMaster.Data._dataHasDNVehicleVEN[vendorid]))
                        _OPSAppointment_DIRouteTOMaster.Data._dataHasDNVehicleVEN[vendorid] = [];
                    _OPSAppointment_DIRouteTOMaster.Data._dataHasDNVehicleVEN[vendorid].push(vehicle);
                }
                if (vehicle.IsOwn) {
                    _OPSAppointment_DIRouteTOMaster.Data._dataHasDNVehicleHome.push(vehicle);
                }
            } else {
                _OPSAppointment_DIRouteTOMaster.Data._dataHasDNVehicleHome.push(vehicle);
                for (var i = 0; i < lstVendor.length; i++) {
                    var vendorid = lstVendor[i].ID;
                    if (!Common.HasValue(_OPSAppointment_DIRouteTOMaster.Data._dataHasDNVehicleVEN[vendorid]))
                        _OPSAppointment_DIRouteTOMaster.Data._dataHasDNVehicleVEN[vendorid] = [];
                    _OPSAppointment_DIRouteTOMaster.Data._dataHasDNVehicleVEN[vendorid].push(vehicle);
                }
            }
        });
    }

    $scope.Master_ChangeMode_Click = function ($event) {
        $event.preventDefault();
        $rootScope.IsLoading = true;

        var data = [];
        $scope.gridNoDNMaster.tbody.find('.HasDNGridGroup').each(function () {
            var sort = parseInt($(this).attr('tabid'));
            var item = _OPSAppointment_DIRouteTOMaster.Data._dataHasDNSort[sort];
            var tr = $(this).closest('tr');
            if (sort > 0 && item.TOMasterID > 0) {
                var chk = $(tr).find('.chkChooseVehicle');
                if (Common.HasValue(chk) && chk.prop('checked') == true && item.TOMasterID > 0)
                    data.push(item.TOMasterID);
            }
        });

        Common.Services.Call($http, {
            url: Common.Services.url.OPS,
            method: _OPSAppointment_DIRouteTOMaster.URL.Master_ChangeMode,
            data: { data: data, fromFTL: false },
            success: function (res) {
                $rootScope.IsLoading = false;
                $scope.LoadData();
                $rootScope.Message({
                    Msg: 'Đã cập nhật.',
                    NotifyType: Common.Message.NotifyType.SUCCESS
                });
            }
        });
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
            _OPSAppointment_DIRouteTOMaster.Data._previousItemValues = {Ton: dataItem.Ton, CBM: dataItem.CBM };
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
                _OPSAppointment_DIRouteTOMaster.Data._dataHasDN = $scope.gridNoDN.dataSource.data();
                var index = _OPSAppointment_DIRouteTOMaster.Data._dataHasDN.indexOf(dataItem);
                _OPSAppointment_DIRouteTOMaster.Data._dataHasDN.splice(index, 0, itemsub.toJSON());
                // Fire change event to recalculate the sum
                dataItem.set("TimeModified", Date.now());
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
            //set scroll
            var h = $scope.gridNoDN.wrapper.find('.k-grid-content').scrollTop();
            Common.Cookie.Set("Scroll", h);

            var tr = $(e.sender.element).closest('tr');
            var dataItem = $scope.gridNoDN.dataItem(tr);
            _OPSAppointment_DIRouteTOMaster.Data._previousItemValues = { Ton: dataItem.Ton, CBM: dataItem.CBM };
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
                _OPSAppointment_DIRouteTOMaster.Data._dataHasDN = $scope.gridNoDN.dataSource.data();
                var index = _OPSAppointment_DIRouteTOMaster.Data._dataHasDN.indexOf(dataItem);
                _OPSAppointment_DIRouteTOMaster.Data._dataHasDN.splice(index, 0, itemsub.toJSON());
                // Fire change event to recalculate the sum
                dataItem.set("TimeModified", Date.now());
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
            //set scroll
            var h = $scope.gridNoDN.wrapper.find('.k-grid-content').scrollTop();
            Common.Cookie.Set("Scroll", h);

            var tr = $(e.sender.element).closest('tr');
            var dataItem = $scope.gridNoDN.dataItem(tr);
            _OPSAppointment_DIRouteTOMaster.Data._previousItemValues = { Ton: dataItem.Ton, CBM: dataItem.CBM };
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
                _OPSAppointment_DIRouteTOMaster.Data._dataHasDN = $scope.gridNoDN.dataSource.data();
                var index = _OPSAppointment_DIRouteTOMaster.Data._dataHasDN.indexOf(dataItem);
                _OPSAppointment_DIRouteTOMaster.Data._dataHasDN.splice(index, 0, itemsub.toJSON());
                // Fire change event to recalculate the sum
                dataItem.set("TimeModified", Date.now());
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
            //set scroll
            var h = $scope.gridNoDN.wrapper.find('.k-grid-content').scrollTop();
            Common.Cookie.Set("Scroll", h);

            var tr = $(e.sender.element).closest('tr');
            var dataItem = $scope.gridNoDN.dataItem(tr);
            _OPSAppointment_DIRouteTOMaster.Data._previousItemValues = { Ton: dataItem.Ton, CBM: dataItem.CBM };
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
                _OPSAppointment_DIRouteTOMaster.Data._dataHasDN = $scope.gridNoDN.dataSource.data();
                var index = _OPSAppointment_DIRouteTOMaster.Data._dataHasDN.indexOf(dataItem);
                _OPSAppointment_DIRouteTOMaster.Data._dataHasDN.splice(index, 0, itemsub.toJSON());
                // Fire change event to recalculate the sum
                dataItem.set("TimeModified", Date.now());
            }
        }
    };

    // Master - Format cho textbox Kg
    $scope.numericKgMasterOptions = {
        min: 0,
        spinners: false,
        decimals: Common.Number.DI_Decimals,
        culture: "en-US",
        format: "n3",
        change: function (e) {
            var tr = $(e.sender.element).closest('tr');
            var dataItem = $scope.FindItemByTr(tr);
            _OPSAppointment_DIRouteTOMaster.Data._previousItemValues = { Kg: dataItem.Kg, Ton: dataItem.Ton, CBM: dataItem.CBM };
            var sub = dataItem.Kg - this.value();
            var sort = dataItem.CreateSortOrder;
            if (sub > 0.001) {
                var itemsub = angular.copy(dataItem);
                itemsub.Kg = sub;
                itemsub.Ton = sub / 1000;
                itemsub.IsOrigin = false;
                itemsub.CreateSortOrder = 0;
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
                _OPSAppointment_DIRouteTOMaster.Data._dataHasDN = $scope.gridNoDN.dataSource.data();
                _OPSAppointment_DIRouteTOMaster.Data._dataHasDN.push(itemsub.toJSON());
                _OPSAppointment_DIRouteTOMaster.Data._dataHasDNSort[sort].IsChange = true;
                // Fire change event to recalculate the sum for left grid(cac chuyen)
                dataItem.set("TimeModified", Date.now());
                $timeout(function () {
                    $scope.IsShowSave = true;
                }, 1);
            }
        }
    };

    // Master - Format cho textbox Tấn
    $scope.numericTonMasterOptions = {
        min: 0,
        spinners: false,
        decimals: Common.Number.DI_Decimals,
        culture: "en-US",
        format: "n5",
        change: function (e) {
            var tr = $(e.sender.element).closest('tr');
            var dataItem = $scope.FindItemByTr(tr);
            _OPSAppointment_DIRouteTOMaster.Data._previousItemValues = { Kg: dataItem.Kg, Ton: dataItem.Ton, CBM: dataItem.CBM };
            var sub = dataItem.Ton - this.value();
            var sort = dataItem.CreateSortOrder;
            if (sub > 0.001) {
                var itemsub = angular.copy(dataItem);
                itemsub.Ton = sub;
                itemsub.Kg = sub * 1000;
                itemsub.IsOrigin = false;
                itemsub.CreateSortOrder = 0;
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
                _OPSAppointment_DIRouteTOMaster.Data._dataHasDN = $scope.gridNoDN.dataSource.data();
                _OPSAppointment_DIRouteTOMaster.Data._dataHasDN.push(itemsub.toJSON());
                _OPSAppointment_DIRouteTOMaster.Data._dataHasDNSort[sort].IsChange = true;
                // Fire change event to recalculate the sum for left grid(cac chuyen)
                dataItem.set("TimeModified", Date.now());
                $timeout(function () {
                    $scope.IsShowSave = true;
                }, 1);
            }
        }
    };

    // Master - Format cho textbox CBM
    $scope.numericCBMMasterOptions = {
        min: 0,
        spinners: false,
        decimals: Common.Number.DI_Decimals,
        culture: "en-US",
        format: "n3",
        change: function (e) {
            var tr = $(e.sender.element).closest('tr');
            var dataItem = $scope.FindItemByTr(tr);
            _OPSAppointment_DIRouteTOMaster.Data._previousItemValues = { Kg: dataItem.Kg, Ton: dataItem.Ton, CBM: dataItem.CBM };
            var sub = dataItem.CBM - this.value();
            var sort = dataItem.CreateSortOrder;
            if (sub > 0.001) {
                var itemsub = angular.copy(dataItem);
                itemsub.CBM = sub;
                itemsub.IsOrigin = false;
                itemsub.CreateSortOrder = 0;
                dataItem.CBM = this.value();
                dataItem.Ton = 0;
                dataItem.Kg = 0;
                dataItem.Quantity = 0;
                if (dataItem.ExchangeCBM > 0) {
                    itemsub.Ton = $scope.ConvertToFix6(dataItem.ExchangeTon * itemsub.CBM / dataItem.ExchangeCBM);
                    itemsub.Kg = itemsub.Ton * 1000;
                    itemsub.Quantity = $scope.ConvertToFix3(dataItem.ExchangeQuantity * itemsub.CBM / dataItem.ExchangeCBM);
                    dataItem.Ton = $scope.ConvertToFix6(dataItem.ExchangeTon * dataItem.CBM / dataItem.ExchangeCBM);
                    dataItem.Kg = dataItem.Ton * 1000;
                    dataItem.Quantity = $scope.ConvertToFix3(dataItem.ExchangeQuantity * dataItem.CBM / dataItem.ExchangeCBM);
                }
                _OPSAppointment_DIRouteTOMaster.Data._dataHasDN = $scope.gridNoDN.dataSource.data();
                _OPSAppointment_DIRouteTOMaster.Data._dataHasDN.push(itemsub.toJSON());
                _OPSAppointment_DIRouteTOMaster.Data._dataHasDNSort[sort].IsChange = true;
                // Fire change event to recalculate the sum for left grid(cac chuyen)
                dataItem.set("TimeModified", Date.now());
                $timeout(function () {
                    $scope.IsShowSave = true;
                }, 1);
            }
        }
    };

    // Master - Format cho textbox Quantity
    $scope.numericQuantityMasterOptions = {
        min: 0,
        spinners: false,
        decimals: Common.Number.DI_Decimals,
        culture: "en-US",
        format: "n3",
        change: function (e) {
            var tr = $(e.sender.element).closest('tr');
            var dataItem = $scope.FindItemByTr(tr);
            _OPSAppointment_DIRouteTOMaster.Data._previousItemValues = { Kg: dataItem.Kg, Ton: dataItem.Ton, CBM: dataItem.CBM };
            var sub = dataItem.Quantity - this.value();
            var sort = dataItem.CreateSortOrder;
            if (sub > 0.001) {
                var itemsub = angular.copy(dataItem);
                itemsub.Quantity = sub;
                itemsub.IsOrigin = false;
                itemsub.CreateSortOrder = 0;
                dataItem.Quantity = this.value();
                dataItem.Ton = 0;
                dataItem.Kg = 0;
                dataItem.CBM = 0;
                if (dataItem.ExchangeQuantity > 0) {
                    itemsub.Ton = $scope.ConvertToFix6(dataItem.ExchangeTon * itemsub.Quantity / dataItem.ExchangeQuantity);
                    itemsub.Kg = itemsub.Ton * 1000;
                    itemsub.CBM = $scope.ConvertToFix3(dataItem.ExchangeCBM * itemsub.Quantity / dataItem.ExchangeQuantity);
                    dataItem.Ton = $scope.ConvertToFix6(dataItem.ExchangeTon * dataItem.Quantity / dataItem.ExchangeQuantity);
                    dataItem.Kg = dataItem.Ton * 1000;
                    dataItem.CBM = $scope.ConvertToFix3(dataItem.ExchangeCBM * dataItem.Quantity / dataItem.ExchangeQuantity);
                }
                _OPSAppointment_DIRouteTOMaster.Data._dataHasDN = $scope.gridNoDN.dataSource.data();
                _OPSAppointment_DIRouteTOMaster.Data._dataHasDN.push(itemsub.toJSON());
                _OPSAppointment_DIRouteTOMaster.Data._dataHasDNSort[sort].IsChange = true;
                // Fire change event to recalculate the sum for left grid(cac chuyen)
                dataItem.set("TimeModified", Date.now());
                $timeout(function () {
                    $scope.IsShowSave = true;
                }, 1);
            }
        }
    };

    // Load danh sách xe
    Common.Services.Call($http, {
        url: Common.Services.url.OPS,
        method: _OPSAppointment_DIRouteTOMaster.URL.VehicleListVehicle,
        data: {},
        success: function (res) {
            if (Common.HasValue(res)) {
                $timeout(function () {
                    _OPSAppointment_DIRouteTOMaster.Data._dataHasDNVehicle = res;
                    _OPSAppointment_DIRouteTOMaster.Data._dataVehicleVEN = res;
                    if (_OPSAppointment_DIRouteTOMaster.Data._dataHasDNVendor.length > 0 && _OPSAppointment_DIRouteTOMaster.Data._dataHasDNVehicle.length > 0)
                        $scope.LoadDataVehicleVendor();
                }, 1);
            }
        }
    });

    // Load danh sách Vendor
    Common.Services.Call($http, {
        url: Common.Services.url.OPS,
        method: _OPSAppointment_DIRouteTOMaster.URL.VehicleListVendor,
        data: {},
        success: function (res) {
            if (Common.HasValue(res)) {
                $timeout(function () {
                    _OPSAppointment_DIRouteTOMaster.Data._dataHasDNVendor = res;
                    if (_OPSAppointment_DIRouteTOMaster.Data._dataHasDNVendor.length > 0 && _OPSAppointment_DIRouteTOMaster.Data._dataHasDNVehicle.length > 0)
                        $scope.LoadDataVehicleVendor();
                }, 1);
            }
        }
    });

    // Load danh sách khách hàng
    Common.Services.Call($http, {
        url: Common.Services.url.OPS,
        method: _OPSAppointment_DIRouteTOMaster.URL.CustomerList,
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
        method: _OPSAppointment_DIRouteTOMaster.URL.VehicleListDriver,
        success: function (res) {
            Common.Services.Error(res, function (res) {
                _OPSAppointment_DIRouteTOMaster.Data._dataDriver = [];
                $.each(res, function (i, v) {
                    _OPSAppointment_DIRouteTOMaster.Data._dataDriver.push({ 'ID': v.ID, 'Text': v.LastName + ' ' + v.FirstName + ' (' + v.EmployeeCode + ')', 'DriverName': v.LastName + ' ' + v.FirstName, 'DriverTelNo': v.Cellphone });
                });
            });
        }
    });
    
    // Load danh sách phân tài
    Common.Services.Call($http, {
        url: Common.Services.url.OPS,
        method: _OPSAppointment_DIRouteTOMaster.URL.FLMPlaning,
        success: function (res) {
            _OPSAppointment_DIRouteTOMaster.Data.FLMPlaning = res;
        }
    });

    $scope.gridNoDNDSOption =
        {
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
                        IsSplit: { type: 'boolean' },
                        Note1: { type: 'string' },
                        Note2: { type: 'string' },
                        DNCode: { type: 'string' }
                    }
            },
            pageSize: 20,
        };

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
                        IsSplit: { type: 'boolean' },
                        Note1: { type: 'string' },
                        Note2: { type: 'string' },
                        DNCode: { type: 'string' }
                    }
            },
            pageSize: 20,
        }),
        pageable: {
            previousNext: true,
            buttonCount: 3, messages: {
                display: "Hiện {0}-{1} trên {2}",
                empty: "DL trống", itemsPerPage: 'dòng mỗi trang'
            }
        },
        height: '99%', groupable: false, columnMenu: false, sortable: true, resizable: true, reorderable: true, sortable: { mode: 'multiple' },
        columns: [
            {
                field: 'CreateSortOrder', width: '50px', template: '<form class="cus-form-enter" ng-submit="SortEnter_Click($event)" ng-show="dataItem.TypeID == 1"><input kendo-numeric-text-box class="txtCreateSortOrder" value="#=CreateSortOrder>0?CreateSortOrder:0#" data-k-min="0" k-options="numericSortOrderOptions" style="width:100%" /></form>', headerTemplate: '<a class="k-button" href="/"><i class="fa"></i></a>', groupHeaderTemplate: '<span class="HasDNGridGroup" tabid="#=value#"></span>', sortable: false, filterable: false, menu: false,
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
                field: 'IsSplit', width: '85px', title: 'Đã chia', templateAttributes: { style: 'text-align: center;' }, template: "<input type='checkbox' ng-model='dataItem.IsSplit' class='checkbox' disabled/>",
                attributes: { style: "text-align: center; " }, headerAttributes: { style: 'text-align: center;' },
                filterable: {
                    cell: {
                        template: function (container) {
                            container.element.kendoComboBox({
                                dataSource: [{ Text: 'Sai', Value: false }, { Text: 'Đúng', Value: true }, { Text: 'Tất cả', Value: '' }],
                                dataTextField: "Text", dataValueField: "Value",
                            });
                        },
                        showOperators: false
                    }
                },
                sortorder: 4, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'Merge', title: 'Gộp', width: '45px', template: '<a href="" class="k-button btnAdd" ng-click="Merge_Click($event, gridNoDN)" style="display:none">M</a><a href="" class="k-button btnAddSave" ng-click="MergeSave_Click($event, gridNoDN)" style="display:none">S</a><input type="checkbox" class="chkAdd" style="display:none"/>', filterable: false, menu: false, sortable: false,
                sortorder: 5, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'Kg', width: '80px', title: 'KG', template: '# if(TypeEditID == 1 && TypeID == 1){# <form class="cus-form-enter"><input kendo-numeric-text-box value="#:Kg#" k-options="numericKgOptions" data-k-max="#:Kg#" style="width:100%" /></form> #}else{# #:Kg# #}#', sortable: true, filterable: { cell: { operator: 'eq', showOperators: false } },
                sortorder: 6, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'Ton', width: '80px', title: 'Tấn', template: '# if(TypeEditID == 1 && TypeID == 1){# <form class="cus-form-enter"><input kendo-numeric-text-box value="#:Ton#" k-options="numericTonOptions" data-k-max="#:Ton#" style="width:100%" /></form> #}else{# #:Ton# #}#', sortable: true, filterable: { cell: { operator: 'eq', showOperators: false } },
                sortorder: 7, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'CBM', width: '80px', title: 'Khối', template: '# if(TypeEditID == 2 && TypeID == 1){# <form class="cus-form-enter"><input kendo-numeric-text-box value="#:CBM#" k-options="numericCBMOptions" data-k-max="#:CBM#" style="width:100%" /></form> #}else{# #:CBM# #}#', sortable: true, filterable: { cell: { operator: 'eq', showOperators: false } },
                sortorder: 8, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'Quantity', width: '90px', title: 'S.Lượng', template: '# if(TypeEditID == 3 && TypeID == 1){# <form class="cus-form-enter"><input kendo-numeric-text-box value="#:Quantity#" k-options="numericQuantityOptions" data-k-max="#:Quantity#" style="width:100%" /></form> #}else{# #:Quantity# #}#', sortable: true, filterable: { cell: { operator: 'eq', showOperators: false } },
                sortorder: 9, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'ProductCodeList', width: '150px', title: 'Mã hàng hóa', sortable: false, filterable: false, menu: false,
                sortorder: 10, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'ProductNameList', width: '150px', title: 'Tên hàng hóa', sortable: false, filterable: false, menu: false,
                sortorder: 11, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'DistrictName', width: '100px', title: 'Quận / Huyện', sortable: true, filterable: { cell: { operator: 'contains', showOperators: false } },
                sortorder: 12, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'Address', width: '200px', title: 'Địa chỉ', sortable: true, filterable: { cell: { operator: 'contains', showOperators: false } },
                sortorder: 13, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'ProvinceName', width: '100px', title: 'Tỉnh / Thành', sortable: true, filterable: { cell: { operator: 'contains', showOperators: false } },
                sortorder: 14, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'TranferItem', width: '80px', title: 'Vận chuyển', sortable: true, filterable: { cell: { operator: 'contains', showOperators: false } },
                sortorder: 15, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'RequestDateEmpty', width: '100px', title: 'Ngày gửi', template: "#=Common.Date.FromJsonDDMMHM(RequestDate)#", sortable: true, filterable: { cell: { operator: 'eq', showOperators: false } },
                sortorder: 16, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'OrderCode', width: '80px', title: 'List', sortable: true, filterable: { cell: { operator: 'contains', showOperators: false } },
                sortorder: 17, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'ETD', width: '100px', title: 'ETD', template: "#=Common.Date.FromJsonDDMMHM(ETD)#", sortable: true, filterable: { cell: { operator: 'eq', showOperators: false } },
                sortorder: 18, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'PartnerCode', width: '100px', title: 'Mã NPP', sortable: true, filterable: { cell: { operator: 'contains', showOperators: false } },
                sortorder: 19, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'PartnerName', width: '100px', title: 'NPP', sortable: true, filterable: { cell: { operator: 'contains', showOperators: false } },
                sortorder: 20, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'FromCode', width: '100px', title: 'Mã l.hàng', sortable: true, filterable: { cell: { operator: 'contains', showOperators: false } },
                sortorder: 21, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'FromAddress', width: '150px', title: 'Điểm l.hàng', sortable: true, filterable: { cell: { operator: 'contains', showOperators: false } },
                sortorder: 22, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'FromProvince', width: '100px', title: 'Tỉnh thành l.hàng', sortable: true, filterable: { cell: { operator: 'contains', showOperators: false } },
                sortorder: 23, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'FromDistrict', width: '100px', title: 'Quận huyện l.hàng', sortable: true, filterable: { cell: { operator: 'contains', showOperators: false } },
                sortorder: 24, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'Description', width: '100px', title: 'Ghi chú', sortable: true, filterable: { cell: { operator: 'contains', showOperators: false } },
                sortorder: 25, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'CUSRoutingCode', width: '180px', title: 'Mã cung đường', sortable: true, filterable: { cell: { operator: 'contains', showOperators: false } },
                sortorder: 26, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'CUSRoutingName', width: '180px', title: 'Cung đường', sortable: true, filterable: { cell: { operator: 'contains', showOperators: false } },
                sortorder: 27, configurable: true, isfunctionalHidden: false
            },
            { field: 'Empty', title: ' ', filterable: false, menu: false, sortable: false, sortorder: 28, configurable: false, isfunctionalHidden: false }
        ],
        toolbar: kendo.template($('#OPSAppointment_DIRouteTOMaster_gridToolbar').html()),
        filterable: { mode: 'row' },
        dataBound: function () {
            Common.Log("GridDataBound");
            try {
                if (Common.HasValue(this) && Common.HasValue(this.tbody)) {
                    if (_OPSAppointment_DIRouteTOMaster.Data._reloadDataNoDN) {
                        $scope.ReloadSort();
                        _OPSAppointment_DIRouteTOMaster.Data._reloadDataNoDN = false;
                    }
                    if (Common.HasValue($scope.gridNoDN)) {
                        this.element.find('tr[role="row"]').each(function () {
                            try {
                                var dataItem = $scope.gridNoDN.dataItem(this);
                                if (Common.HasValue(dataItem) && Common.HasValue(_OPSAppointment_DIRouteTOMaster.Data._dataHasDNGroupProduct[dataItem.ID])) {
                                    if (_OPSAppointment_DIRouteTOMaster.Data._dataHasDNGroupProduct[dataItem.ID].length > 1) {
                                        var btnAdd = $(this).find('.btnAdd');
                                        btnAdd.show();
                                    }
                                }
                            } catch (e) {

                            }
                        });
                    }
                }

                //get scroll
                var grid = this;
                var h = Common.Cookie.Get("Scroll");
                grid.wrapper.find('.k-grid-content').scrollTop(h);
                this.element.find('tr[role="row"] input.chkAdd').closest('td').css('text-align', 'center');
            } catch (e) {

            }
        }
    };

    $scope.gridNoDNMasterDSOption = {
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
                    IsSplit: { type: 'boolean' },
                    Note2: { type: 'string' },
                    DNCode: { type: 'string' }

                }
        },
        group: [{ field: 'CreateSortOrder', dir: 'desc' }]
    };

    $scope.gridNoDNMasterOptions = {
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
                        IsSplit: { type: 'boolean' },
                        Note2: { type: 'string' },
                        DNCode: { type: 'string' }

                    }
            },
            group: [{ field: 'CreateSortOrder', dir: 'desc' }]
        }),
        height: '99%', groupable: false, pageable: false, columnMenu: false, sortable: true, resizable: true, reorderable: true, sortable: { mode: 'multiple' },
        columns: [
            {
                field: 'Expand', width: '45px', headerTemplate: '<a class="k-button" href="/" ng-click="Expand_Click($event,gridNoDNMaster)"><i class="fa fa-minus"></i></a>',
                sortable: false, filterable: false, menu: false
            },
            {
                field: 'CreateSortOrder', width: '45px',
                template: '<form class="cus-form-enter" ng-submit="SortEnter_Click($event)" ng-show="dataItem.TypeID == 1"><input kendo-numeric-text-box class="txtCreateSortOrder" value="#=CreateSortOrder>0?CreateSortOrder:0#" data-k-min="0" k-options="numericSortOrderMasterOptions" style="width:100%" /></form>',
                headerTemplate: '<input type="checkbox" ng-model="ValCheckBox" ng-click="CheckBox_All_Check($event,gridNoDNMaster)" />',
                groupHeaderTemplate: '<span class="HasDNGridGroup" tabid="#=value#"></span>',
                groupFooterTemplate: '<span class="HasDNGridGroupFooter" tabid="#= data.parent().value#"></span>', sortable: false, filterable: false, menu: false
            },
            { field: 'SOCode', width: '80px', title: 'SO', sortable: true, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'IsOrigin', width: '30px', title: 'g', template: '<input type="checkbox" ng-model="dataItem.IsOrigin" />', sortable: false, filterable: false },
            { field: 'DNCode', width: '90px', title: 'DN', sortable: true, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'Merge', title: 'Gộp', width: '45px', template: '<a href="" class="k-button btnAdd" ng-click="MergeMaster_Click($event, gridNoDNMaster)" style="display:none">M</a><a href="" class="k-button btnAddSave" ng-click="MergeSaveMaster_Click($event, gridNoDNMaster)" style="display:none">S</a><input type="checkbox" class="chkAdd" style="display:none"/>', filterable: false, menu: false, sortable: false },
            { field: 'Kg', width: '80px', title: 'KG', template: '# if(TypeEditID == 1 && TypeID == 1){# <form class="cus-form-enter"><input kendo-numeric-text-box value="#:Kg#" k-options="numericKgMasterOptions" data-k-max="#:Kg#" style="width:100%" /></form> #}else{# #:Kg# #}#', sortable: true, filterable: { cell: { operator: 'eq', showOperators: false } } },
            { field: 'Ton', width: '80px', title: 'Tấn', template: '# if(TypeEditID == 1 && TypeID == 1){# <form class="cus-form-enter"><input kendo-numeric-text-box value="#:Ton#" k-options="numericTonMasterOptions" data-k-max="#:Ton#" style="width:100%" /></form> #}else{# #:Ton# #}#', sortable: true, filterable: { cell: { operator: 'eq', showOperators: false } } },
            { field: 'CBM', width: '80px', title: 'Khối', template: '# if(TypeEditID == 2 && TypeID == 1){# <form class="cus-form-enter"><input kendo-numeric-text-box value="#:CBM#" k-options="numericCBMMasterOptions" data-k-max="#:CBM#" style="width:100%" /></form> #}else{# #:CBM# #}#', sortable: true, filterable: { cell: { operator: 'eq', showOperators: false } } },
            { field: 'Quantity', width: '90px', title: 'S.Lượng', template: '# if(TypeEditID == 3 && TypeID == 1){# <form class="cus-form-enter"><input kendo-numeric-text-box value="#:Quantity#" k-options="numericQuantityMasterOptions" data-k-max="#:Quantity#" style="width:100%" /></form> #}else{# #:Quantity# #}#', sortable: true, filterable: { cell: { operator: 'eq', showOperators: false } } },
             {
                 field: 'IsSplit', width: '85px', title: 'Đã chia', templateAttributes: { style: 'text-align: center;' }, template: "<input type='checkbox' ng-model='dataItem.IsSplit' class='checkbox' disabled/>",
                 attributes: { style: "text-align: center; " }, headerAttributes: { style: 'text-align: center;' },
                 filterable: {
                     cell: {
                         template: function (container) {
                             container.element.kendoComboBox({
                                 dataSource: [{ Text: 'Sai', Value: false }, { Text: 'Đúng', Value: true }, { Text: 'Tất cả', Value: '' }],
                                 dataTextField: "Text", dataValueField: "Value",
                             });
                         },
                         showOperators: false
                     }
                 }
             },
            { field: 'ProductCodeList', width: '150px', title: 'Mã hàng hóa', sortable: false, filterable: false, menu: false },
            { field: 'ProductNameList', width: '150px', title: 'Tên hàng hóa', sortable: false, filterable: false, menu: false },
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
            { field: 'CUSRoutingCode', width: '180px', title: 'Mã cung đường', sortable: true, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'CUSRoutingName', width: '180px', title: 'Cung đường', sortable: true, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'Empty', title: ' ', filterable: false, menu: false, sortable: false }
        ],
        toolbar: kendo.template($('#OPSAppointment_DIRouteTOMaster_gridMasterToolbar').html()),
        filterable: { mode: 'row' },
        dataBound: function () {
            Common.Log("$scope.gridNoDNMasterOptions.dataBound");
            var grid = this;
            $scope.IsExpand = true;
            grid.thead.find('th[data-field="CreateSortOrder"]').find("i").removeClass("fa-plus");
            grid.thead.find('th[data-field="CreateSortOrder"]').find("i").addClass("fa-minus");
            //this.element.find('tr[role="row"] .k-header:first-child').css("display", "none");
            if (Common.HasValue(grid) && Common.HasValue(grid.tbody)) {
                if (_OPSAppointment_DIRouteTOMaster.Data._reloadDataNoDNMaster) {
                    $scope.ReloadSortMaster();
                    _OPSAppointment_DIRouteTOMaster.Data._reloadDataNoDNMaster = false;

                    this.element.unbind()
                        .on('click', '.HasDNGridGroup .btnHasDNGroup', function (e) {
                            e.preventDefault();
                            $scope.GroupDN_Click(this);
                        })
                        .on('click', '.groupPLFooter .pointer', function (e) {
                            e.preventDefault();
                            $scope.CalculatePL_Click(this);
                        })
                        .on('click', '.HasDNGridGroup .chkChooseVehicle', function (e) {
                            $scope.ReloadButton();
                        });

                }
                this.element.find('.HasDNGridGroup').each(function () {
                    var sort = parseInt($(this).attr('tabid'));
                    $scope.updateRouteTripInfo(sort);
                });
                this.element.find('.HasDNGridGroupFooter').each(function () {
                    var sort = parseInt($(this).attr('tabid'));
                    var item = _OPSAppointment_DIRouteTOMaster.Data._dataHasDNSort[sort];
                    var plClass = "";
                    if (item.PL > 0)
                        plClass = "green";
                    else
                        if (item.PL < 0)
                            plClass = "red";
                    var tr = $(this).closest('tr');
                    $(tr).replaceWith("<div class='groupPLFooter' tabid='" + sort + "'><i class='fa fa-refresh pointer' title='Tính lợi nhuận'></i><span>Thu: <span class='Credit'>" + kendo.toString(item.Credit, _OPSAppointment_DIRouteTOMaster.Data._formatMoney) + "</span> - Chi: <span class='Debit'>" + kendo.toString(item.Debit, _OPSAppointment_DIRouteTOMaster.Data._formatMoney) + "</span> - Lợi nhuận: <span class='PL " + plClass + "'>" + kendo.toString(item.PL, _OPSAppointment_DIRouteTOMaster.Data._formatMoney) + "</span></span></div>");
                });
            }
        }

    };

    $scope.OPSAppointment_DIRouteTOMaster_VendorRate_gridOptions = {
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
            { field: 'RateTime', width: '90px', title: 'Thời gian', template: '<input type="number" class="cus-number RateTimeVender" min="0" max="100000" style="width:100%"/>', filterable: false, menu: false, sortable: true },
            { field: 'IsManual', width: '90px', title: 'Nhập giá', template: '<input style="text-align:center" class="chkIsManual" type="checkbox" #= IsManual ? checked="checked" : "" #/>', filterable: false, menu: false, sortable: true },
            { field: 'Debit', width: '120px', title: 'Giá', template: '<input class="txtDebit cus-number" value="#=Debit#" style="width:100%"/>', filterable: false, menu: false, sortable: true },
        ],
        toolbar: kendo.template($('#OPSAppointment_DIRouteTOMaster_RateTime').html()),
        dataBound: function (e) {
            if (Common.HasValue($scope.OPSAppointment_DIRouteTOMaster_VendorRate_grid) && Common.HasValue($scope.OPSAppointment_DIRouteTOMaster_VendorRate_grid.tbody)) {
                Common.Log('WinRateGridVENDataBound');
                $($scope.OPSAppointment_DIRouteTOMaster_VendorRate_grid.element).find('.RateTimeVender').kendoNumericTextBox({
                    format: '#,##0', spinners: false, culture: 'en-US', min: 0, max: 100000000, step: 1000, decimals: 0,
                    value: 0
                });
                $($scope.OPSAppointment_DIRouteTOMaster_VendorRate_grid.element).find('.txtDebit').kendoNumericTextBox({
                    format: '#,##0', spinners: false, culture: 'en-US', min: 0, max: 100000000, step: 1000, decimals: 0,
                    value: 0
                });
                $($scope.OPSAppointment_DIRouteTOMaster_VendorRate_grid.element).find('.chkIsManual').change(function () {
                    var tr = $(this).closest('tr');
                    var txtDebit = $($(tr).find('input.txtDebit.cus-number')[1]).data('kendoNumericTextBox');
                    txtDebit.enable($(this).prop('checked'));
                });
                var lst = $scope.OPSAppointment_DIRouteTOMaster_VendorRate_grid.tbody.find('tr');
                $.each(lst, function (itr, tr) {
                    var item = $scope.OPSAppointment_DIRouteTOMaster_VendorRate_grid.dataItem(tr);
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

    $scope.LoadData = function () {
        Common.Log('LoadData');
        $rootScope.IsLoading = true;
        //$scope.Summary = '';
        var i = 0;
        while (i < _OPSAppointment_DIRouteTOMaster.Data._dataHasDNVendor.length) {
            if (_OPSAppointment_DIRouteTOMaster.Data._dataHasDNVendor[i].ID < 0)
                _OPSAppointment_DIRouteTOMaster.Data._dataHasDNVendor.splice(i, 1);
            else
                i++;
        }
        _OPSAppointment_DIRouteTOMaster.Data._dataHasDNVendor.splice(0, 0, { 'ID': -1, 'CustomerName': 'Xe nhà' });
        _OPSAppointment_DIRouteTOMaster.Data._dataHasDNVehicle = $.extend(true, [], _OPSAppointment_DIRouteTOMaster.Data._dataVehicleVEN);
        var i = 0;
        while (i < _OPSAppointment_DIRouteTOMaster.Data._dataHasDNVehicle.length) {
            if (_OPSAppointment_DIRouteTOMaster.Data._dataHasDNVehicle[i].ID < 0)
                _OPSAppointment_DIRouteTOMaster.Data._dataHasDNVehicle.splice(i, 1);
            else
                i++;
        }

        _OPSAppointment_DIRouteTOMaster.Data._dataHasDNSort = [];
        _OPSAppointment_DIRouteTOMaster.Data._dataHasDNGroupProduct = [];
        var lstFilters = [];
        var TypeOfViewID = parseInt($scope.Search.TypeOfViewID);
        switch (TypeOfViewID) {
            case 1:
                lstFilters.push(Common.Request.FilterParamWithOr('TypeID', Common.Request.FilterType.Equal, 1));
                break;
            case 2:
                lstFilters.push(Common.Request.FilterParamWithOr('TypeID', Common.Request.FilterType.Equal, 2));
                break;
            case 3:
                lstFilters.push(Common.Request.FilterParamWithOr('TypeID', Common.Request.FilterType.Equal, 3));
                break;
            case 4:
                lstFilters.push(Common.Request.FilterParamWithOr('TypeID', Common.Request.FilterType.Equal, 1));
                lstFilters.push(Common.Request.FilterParamWithOr('TypeID', Common.Request.FilterType.Equal, 2));
                lstFilters.push(Common.Request.FilterParamWithOr('TypeID', Common.Request.FilterType.Equal, 3));
                break;
        }
        var param = Common.Request.Create({
            Sorts: [], Filters: lstFilters
        });

        Common.Services.Call($http, {
            url: Common.Services.url.OPS,
            method: _OPSAppointment_DIRouteTOMaster.URL.List,
            data: { request: param, lstCustomerID: $scope.Search.ListCustomerID, DateFrom: $scope.Search.DateFrom, DateTo: $scope.Search.DateTo },
            success: function (res) {
                Common.Services.Error(res, function (res) {
                    _OPSAppointment_DIRouteTOMaster.Data._dataHasDNSort[0] = { 'CreateSortOrder': 0, 'CreateVendorID': -1, 'CreateVehicleCode': '', 'CreateDriverName': '', 'CreateTelephone': '', 'Kg': 0, 'Quantity': 0, 'Credit': 0, 'Debit': 0, 'PL': 0 };
                    var datafix = [];
                    var hasMonitor = $scope.Search.TypeOfViewID != 1;
                    var index = 1;
                    
                    $.each(res, function (i, v) {
                        if (hasMonitor || v.TypeID < 2) {
                            v.CreateDateTime = Common.Date.FromJson(v.CreateDateTime);
                            v.Kg = 0;
                            v.CBM = 0;
                            v.Ton = 0;
                            v.IsCalculate = false;
                            _OPSAppointment_DIRouteTOMaster.Data._dataHasDNSort[index] = v;
                            datafix[v.CreateSortOrder] = index;
                            v.CreateSortOrder = index;
                            v.IsChange = false;
                            index++;
                        }
                    });

                    Common.Services.Call($http, {
                        url: Common.Services.url.OPS,
                        method: _OPSAppointment_DIRouteTOMaster.URL.OrderList,
                        data: { request: param, lstCustomerID: $scope.Search.ListCustomerID, DateFrom: $scope.Search.DateFrom, DateTo: $scope.Search.DateTo },
                        success: function (res) {
                            Common.Services.Error(res, function (res) {
                                _OPSAppointment_DIRouteTOMaster.Data._dataHasDN = [];
                                _OPSAppointment_DIRouteTOMaster.Data._dataHasDNMaster = [];
                                $.each(res, function (i, v) {
                                    if (hasMonitor || v.TypeID < 2) {
                                        if (Common.HasValue(v.RequestDate)) {
                                            v.RequestDate = Common.Date.FromJson(v.RequestDate);
                                            v.RequestDateEmpty = Common.Date.Date(v.RequestDate);
                                        }
                                        v.Kg = v.Ton * 1000;
                                        if (v.CreateSortOrder > 0) {
                                            v.CreateSortOrder = datafix[v.CreateSortOrder];
                                            _OPSAppointment_DIRouteTOMaster.Data._dataHasDNMaster.push(v);
                                        } else {
                                            _OPSAppointment_DIRouteTOMaster.Data._dataHasDN.push(v);
                                        }
                                    }
                                });
                                try {
                                    _OPSAppointment_DIRouteTOMaster.Data._reloadDataNoDN = true;
                                    _OPSAppointment_DIRouteTOMaster.Data._reloadDataNoDNMaster = true;

                                    angular.extend($scope.gridNoDNMasterDSOption, { data: _OPSAppointment_DIRouteTOMaster.Data._dataHasDNMaster });
                                    $scope.gridNoDNMaster.setDataSource(new kendo.data.DataSource($scope.gridNoDNMasterDSOption));
                                    $scope.gridNoDNMaster.dataSource.bind("change", function (e) {
                                        $scope.gridNoDNMasterChanged(e);
                                    })

                                    angular.extend($scope.gridNoDNDSOption, { data: _OPSAppointment_DIRouteTOMaster.Data._dataHasDN });
                                    $scope.gridNoDN.setDataSource(new kendo.data.DataSource($scope.gridNoDNDSOption));
                                    $scope.gridNoDN.dataSource.bind("change", function (e) {
                                        $scope.gridNoDNChanged(e);
                                    })

                                    $rootScope.IsLoading = false;
                                }
                                catch (e) {
                                    //$rootScope.Message({
                                    //    Msg: 'Dữ liệu lỗi!',
                                    //    NotifyType: Common.Message.NotifyType.ERROR
                                    //});
                                    $rootScope.IsLoading = false;
                                    Common.Log(res);
                                    Common.Log(datafix);
                                    Common.Log(_OPSAppointment_DIRouteTOMaster.Data._dataHasDN);
                                    Common.Log(_OPSAppointment_DIRouteTOMaster.Data._dataHasDNMaster);
                                }
                            });
                        }
                    });
                });
            }
        });

        $timeout(function () {
            $scope.IsShowSave = false;
            $scope.IsShowApproved = false;
            $scope.IsShowUnApproved = false;
            $scope.IsShowTendered = false;
            $scope.IsShowDelete = false;
        }, 10);
    };

    $timeout(function () {
        //$scope.gridNoDN.resize();
        //$scope.gridNoDNMaster.resize();
        $rootScope.ApplyGridSettings($scope.gridNoDN);
        $rootScope.ApplyGridSettings($scope.gridNoDNMaster);
    }, 300);

    $scope.updateRouteTripInfo = function (sort) {
        Common.Log("updateRouteTripInfo");
        
        var $groupInfo = $scope.gridNoDNMaster.element.find('.HasDNGridGroup[tabid=' + sort + ']');
        if ($groupInfo.length = 0) {
            return;
        }

        $groupInfo = $groupInfo[0];
        var item = _OPSAppointment_DIRouteTOMaster.Data._dataHasDNSort[sort];
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
            if (item.TypeID == 3) {
                if (!$(tr).hasClass('sendtender'))
                    $(tr).addClass('sendtender');
            }

            if (sort > 0) {
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
                if (item.TypeID > 1) {
                    angular.element($groupInfo).html('<input type="checkbox" class="chkChooseVehicle"/> Chuyến ' + sort + ' - Kg: ' + Common.Number.ToNumber1(item.Kg) + ' - ' +
                        item.CreateVendorName + ' - ' + item.CreateVehicleCode + ' - ' + drivername + ' - ' + telephone + ' - ' + Common.Date.FromJsonDMYHM(item.CreateDateTime) + strMaxWeight + strOverLoad);
                }
                else if (item.TOMasterID > 0) {
                    angular.element($groupInfo).html('<input type="checkbox" class="chkChooseVehicle"/> Chuyến ' + sort + ' - Kg: ' + Common.Number.ToNumber1(item.Kg) + ' - ' +
                        '<a href="" class="btnHasDNGroup">' + item.CreateVendorName + ' - ' + item.CreateVehicleCode + ' - ' + drivername + ' - ' + telephone + ' - ' + Common.Date.FromJsonDMYHM(item.CreateDateTime) + '</a>' +
                        '<input style="display:none;width:100px" class="cboHasDNVendor cus-combobox" placeholder="Nhà vận tải" value="' + item.CreateVendorID + '" /><span style="display:none" class="lblHasDNSplit"> - </span>' +
                        '<input style="display:none;width:100px" id="cboHasDNVehicle" class="cboHasDNVehicle cus-combobox" placeholder="Nhập số xe" value="' + item.CreateVehicleCode + '" /><span style="display:none" class="lblHasDNSplit"> - </span>' +
                        '<input style="display:none;width:50px" type="number" min="0" class="txtHasDNMaxWeight k-textbox" placeholder="T.Tải" value="" /><span style="display:none" class="lblHasDNSplit vehicle"> - </span>' +
                        '<input style="display:none;width:50px" type="number" min="0" class="txtHasDNMaxCBM k-textbox" placeholder="S.Khối" value="" /><span style="display:none" class="lblHasDNSplit vehicle"> - </span>' +
                        '<input style="display:none;width:100px" type="text" class="txtHasDNDriverName k-textbox" placeholder="Nhập tài xế" value="' + drivername + '" /><span style="display:none" class="lblHasDNSplit"> - </span>' +
                        '<input style="display:none;width:100px" type="text" class="txtHasDNTelephone k-textbox" placeholder="Nhập điện thoại" value="' + telephone + '"/><span style="display:none" class="lblHasDNSplit"> - </span>' +
                        '<input style="display:none;width:160px" class="txtCreateDateTime"  value="' + Common.Date.FromJsonDMYHM(item.CreateDateTime) + '"/>' + strMaxWeight + strOverLoad);
                }
                else {
                    angular.element($groupInfo).html('Chuyến ' + sort + ' - Kg: ' + Common.Number.ToNumber1(item.Kg) + ' - ' +
                        '<a href="" class="btnHasDNGroup">' + item.CreateVendorName + ' - ' + item.CreateVehicleCode + ' - ' + drivername + ' - ' + telephone + ' - ' + Common.Date.FromJsonDMYHM(item.CreateDateTime) + '</a>' +
                        '<input style="display:none;width:100px" class="cboHasDNVendor cus-combobox" placeholder="Nhà vận tải" value="' + item.CreateVendorID + '" /><span style="display:none" class="lblHasDNSplit"> - </span>' +
                        '<input style="display:none;width:100px" id="cboHasDNVehicle" class="cboHasDNVehicle cus-combobox" placeholder="Nhập số xe" value="' + item.CreateVehicleCode + '" /><span style="display:none" class="lblHasDNSplit"> - </span>' +
                        '<input style="display:none;width:50px" type="number" min="0" class="txtHasDNMaxWeight k-textbox" placeholder="T.Tải" value="" /><span style="display:none" class="lblHasDNSplit vehicle"> - </span>' +
                        '<input style="display:none;width:50px" type="number" min="0" class="txtHasDNMaxCBM k-textbox" placeholder="S.Khối" value="" /><span style="display:none" class="lblHasDNSplit vehicle"> - </span>' +
                        '<input style="display:none;width:100px" type="text" class="txtHasDNDriverName k-textbox" placeholder="Nhập tài xế" value="' + drivername + '" /><span style="display:none" class="lblHasDNSplit"> - </span>' +
                        '<input style="display:none;width:100px" type="text" class="txtHasDNTelephone k-textbox" placeholder="Nhập điện thoại" value="' + telephone + '"/><span style="display:none" class="lblHasDNSplit"> - </span>' +
                        '<input style="display:none;width:160px" class="txtCreateDateTime"  value="' + Common.Date.FromJsonDMYHM(item.CreateDateTime) + '"/>' + strMaxWeight + strOverLoad);
                }
            }
            else
                $($groupInfo).html('Kg: ' + Common.Number.ToNumber1(item.Kg));
        }
    }

    $scope.setRouteTripCreateDateTime = function (sort) {
        var trip = _OPSAppointment_DIRouteTOMaster.Data._dataHasDNSort[sort];
        if (!trip) {
            return;
        }
        trip.CreateDateTime = null;
        $.each(_OPSAppointment_DIRouteTOMaster.Data._dataHasDNMaster, function (j, o) {
            if (o.CreateSortOrder == sort) {
                if (!trip.CreateDateTime || trip.CreateDateTime > Common.Date.FromJson(o.ETD)) {
                    trip.CreateDateTime == Common.Date.FromJson(o.ETD);
                }
            }
        })
    }

    $scope.gridNoDNMasterChanged = function (e) {
        var item = e.items[0];
        var itemSort = _OPSAppointment_DIRouteTOMaster.Data._dataHasDNSort[item.CreateSortOrder];
        
        switch (e.action) {
            case "add":
                _OPSAppointment_DIRouteTOMaster.Data._totalDNMaster.Ton += item.Ton;
                _OPSAppointment_DIRouteTOMaster.Data._totalDNMaster.CBM += item.CBM;
                if (!Common.HasValue(itemSort)) {
                    itemSort = { 'Ton': 0, 'CBM': 0, 'CreateSortOrder': item.CreateSortOrder, 'CreateVendorID': item.VendorOfVehicleID, 'CreateVendorName': item.VendorOfVehicleName, 'CreateVehicleCode': item.VehicleCode, 'CreateDriverName': item.DriverName, 'CreateTelephone': item.DriverTelNo, 'Kg': item.Kg, 'CreateVehicleID': item.VehicleID, 'TOMasterID': item.TOMasterID, 'CreateDateTime': item.CreateDateTime, 'TypeID': item.TypeID, 'MaxWeight': item.MaxWeightCal, 'MaxCBM': 0, 'Quantity': item.Quantity, 'Credit': item.Credit, 'Debit': item.Debit, 'IsCalculate': false, 'IsChange': true };
                    _OPSAppointment_DIRouteTOMaster.Data._totalDNMaster.Sort++;
                }
                itemSort.Kg += item.Kg;
                itemSort.Ton += item.Ton;
                itemSort.CBM += item.CBM;

                var etd = Common.Date.FromJson(item.ETD);
                if (!itemSort.CreateDateTime || itemSort.CreateDateTime > etd) {
                    itemSort.CreateDateTime = etd;
                }

                $scope.updateRouteTripInfo(item.CreateSortOrder);
                break;
            case "remove":
                itemSort = _OPSAppointment_DIRouteTOMaster.Data._dataHasDNSort[_OPSAppointment_DIRouteTOMaster.Data._previousSort];
                _OPSAppointment_DIRouteTOMaster.Data._totalDNMaster.Ton -= item.Ton;
                _OPSAppointment_DIRouteTOMaster.Data._totalDNMaster.CBM -= item.CBM;
                itemSort.Kg -= item.Kg;
                itemSort.Ton -= item.Ton;
                itemSort.CBM -= item.CBM;
                if (itemSort.Ton < 0.001 && itemSort.CBM < 0.001) {
                    _OPSAppointment_DIRouteTOMaster.Data._totalDNMaster.Sort--;
                }
                $scope.setRouteTripCreateDateTime(_OPSAppointment_DIRouteTOMaster.Data._previousSort)
                $scope.updateRouteTripInfo(_OPSAppointment_DIRouteTOMaster.Data._previousSort);
                break;
            case "itemchange":
                _OPSAppointment_DIRouteTOMaster.Data._totalDNMaster.Ton += item.Ton - _OPSAppointment_DIRouteTOMaster.Data._previousItemValues.Ton;
                _OPSAppointment_DIRouteTOMaster.Data._totalDNMaster.CBM += item.CBM - _OPSAppointment_DIRouteTOMaster.Data._previousItemValues.CBM;
                itemSort.Kg += item.Kg - _OPSAppointment_DIRouteTOMaster.Data._previousItemValues.Kg;
                itemSort.Ton += item.Ton - _OPSAppointment_DIRouteTOMaster.Data._previousItemValues.Ton;
                itemSort.CBM += item.CBM - _OPSAppointment_DIRouteTOMaster.Data._previousItemValues.CBM;
                $scope.updateRouteTripInfo(item.CreateSortOrder);
                break;
            default:
                break;
        }
        $scope.$applyAsync(function () {
            $scope.SummaryMaster = 'Tấn: ' + Common.Number.ToNumber1(_OPSAppointment_DIRouteTOMaster.Data._totalDNMaster.Ton)
                                    + ' - Khối: ' + Common.Number.ToNumber1(_OPSAppointment_DIRouteTOMaster.Data._totalDNMaster.CBM)
                                    + ' - Số chuyến: ' + _OPSAppointment_DIRouteTOMaster.Data._totalDNMaster.Sort;
        });
    };

    $scope.gridNoDNChanged = function (e) {
        var item = e.items[0];
        switch (e.action) {
            case "add":
                _OPSAppointment_DIRouteTOMaster.Data._totalDN.Ton += item.Ton;
                _OPSAppointment_DIRouteTOMaster.Data._totalDN.CBM += item.CBM;
                if (!Common.HasValue(_OPSAppointment_DIRouteTOMaster.Data._dataHasDNGroupProduct[item.ID]))
                    _OPSAppointment_DIRouteTOMaster.Data._dataHasDNGroupProduct[item.ID] = [];
                _OPSAppointment_DIRouteTOMaster.Data._dataHasDNGroupProduct[item.ID].push(item);
                break;
            case "remove":
                _OPSAppointment_DIRouteTOMaster.Data._totalDN.Ton -= item.Ton;
                _OPSAppointment_DIRouteTOMaster.Data._totalDN.CBM -= item.CBM;
                var index = _OPSAppointment_DIRouteTOMaster.Data._dataHasDNGroupProduct[item.ID].indexOf(item);
                _OPSAppointment_DIRouteTOMaster.Data._dataHasDNGroupProduct[item.ID].splice(index, 1);
                break;
            case "itemchange":
                _OPSAppointment_DIRouteTOMaster.Data._totalDN.Ton += item.Ton - _OPSAppointment_DIRouteTOMaster.Data._previousItemValues.Ton;
                _OPSAppointment_DIRouteTOMaster.Data._totalDN.CBM += item.CBM - _OPSAppointment_DIRouteTOMaster.Data._previousItemValues.CBM;
                break;
            default:
                break;
        }
        $scope.$applyAsync(function () {
            $scope.Summary = 'Tấn: ' + Common.Number.ToNumber1(_OPSAppointment_DIRouteTOMaster.Data._totalDN.Ton) + ' - Khối: ' + Common.Number.ToNumber1(_OPSAppointment_DIRouteTOMaster.Data._totalDN.CBM);
        });
    };

    // For Kg, CBM, Quantity
    $scope.ConvertToFix3 = function (number) {
        return number > 0 ? Math.round(number * 1000) / 1000 : 0;
    }

    // For Ton
    $scope.ConvertToFix6 = function (number) {
        return number > 0 ? Math.round(number * 1000000) / 1000000 : 0;
    }

    $scope.Init_LoadCookie = function () {
        Common.Log('Init_LoadCookie');
        var lstCustomerID = [];
        var strCookie = Common.Cookie.Get(_OPSAppointment_DIRouteTOMaster.Data.CookieSearch);
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
            Common.Cookie.Set(_OPSAppointment_DIRouteTOMaster.Data.CookieSearch, JSON.stringify($scope.Search));
            $scope.LoadData();
        }, 10);
    };

    $scope.ReloadSort = function ($event) {
        Common.Log("ReloadSort");
        $scope.IsShowUnMerge = false;

        var totalton = 0;
        var totalcbm = 0;
        var totalquan = 0;
        $scope.Summary = '';

        if (Common.HasValue($scope.gridNoDN)) {
            _OPSAppointment_DIRouteTOMaster.Data._dataHasDN = $scope.gridNoDN.dataSource.data();
        }
        _OPSAppointment_DIRouteTOMaster.Data._dataHasDNGroupProduct = [];
        $.each(_OPSAppointment_DIRouteTOMaster.Data._dataHasDN, function (i, v) {
            if (v.CreateSortOrder <= 0) {
                v.Kg = $scope.ConvertToFix3(v.Kg);
                v.Ton = $scope.ConvertToFix6(v.Ton);
                v.CBM = $scope.ConvertToFix3(v.CBM);
                v.Quantity = $scope.ConvertToFix3(v.Quantity);
                if (v.Ton > 0) {
                    totalton += v.Ton;
                }
                if (v.CBM > 0) {
                    totalcbm += v.CBM;
                }
                if (v.Quantity > 0) {
                    totalquan += v.Quantity;
                }
                if (!Common.HasValue(_OPSAppointment_DIRouteTOMaster.Data._dataHasDNGroupProduct[v.ID]))
                    _OPSAppointment_DIRouteTOMaster.Data._dataHasDNGroupProduct[v.ID] = [];
                _OPSAppointment_DIRouteTOMaster.Data._dataHasDNGroupProduct[v.ID].push(v);
            }
        });

        _OPSAppointment_DIRouteTOMaster.Data._totalDN.Ton = totalton;
        _OPSAppointment_DIRouteTOMaster.Data._totalDN.CBM = totalcbm;
        $timeout(function () {
            $scope.Summary = 'Tấn: ' + Common.Number.ToNumber1(totalton) + ' - Khối: ' + Common.Number.ToNumber1(totalcbm);
        }, 10)
    };

    $scope.ReloadSortMaster = function ($event, eventItem1, eventItem2) {
        Common.Log("ReloadSortMaster");
        $scope.IsShowUnMerge = false;
        var totalsort = 0;
        var totalton = 0;
        var totaltoncomplete = 0;
        var totalcbm = 0;
        var totalcbmcomplete = 0;
        var totalquan = 0;
        $scope.SummaryMaster = '';
        
        $.each(_OPSAppointment_DIRouteTOMaster.Data._dataHasDNSort, function (i, v) {
            if (v.CreateSortOrder > 0) {
                totalsort++;
            }
            v.Ton = 0;
            v.Kg = 0;
            v.Quantity = 0;
            v.CBM = 0;
        });

        $.each(_OPSAppointment_DIRouteTOMaster.Data._dataHasDNMaster, function (i, v) {
            if (v.CreateSortOrder > 0) {
                v.Kg = $scope.ConvertToFix3(v.Kg);
                v.Ton = $scope.ConvertToFix6(v.Ton);
                v.CBM = $scope.ConvertToFix3(v.CBM);
                v.Quantity = $scope.ConvertToFix3(v.Quantity);
                var itemSort = _OPSAppointment_DIRouteTOMaster.Data._dataHasDNSort[v.CreateSortOrder];
                if (!Common.HasValue(itemSort))
                    itemSort = {
                        'CreateSortOrder': v.CreateSortOrder, 'CreateVendorID': v.VendorOfVehicleID, 'CreateVendorName': v.VendorOfVehicleName,
                        'CreateVehicleCode': v.VehicleCode, 'CreateDriverName': v.DriverName, 'CreateTelephone': v.DriverTelNo, 'Kg': 0, 'Ton': 0, 'CBM': 0, 
                        'CreateVehicleID': v.VehicleID, 'TOMasterID': v.TOMasterID, 'CreateDateTime': null, 'TypeID': v.TypeID,
                        'MaxWeight': v.MaxWeightCal, 'MaxCBM': 0, 'Quantity': v.Quantity, 'Credit': v.Credit, 'Debit': v.Debit, 'IsCalculate': false, 'IsChange': true
                    };
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
                if (!_OPSAppointment_DIRouteTOMaster.Data._reloadDataNoDNMaster) {
                    var etd = Common.Date.FromJson(v.ETD);
                    if (!itemSort.CreateDateTime || itemSort.CreateDateTime > etd) {
                        itemSort.CreateDateTime = etd;
                    }
                }
            }
        });

        _OPSAppointment_DIRouteTOMaster.Data._totalDNMaster.Ton = totaltoncomplete;
        _OPSAppointment_DIRouteTOMaster.Data._totalDNMaster.CBM = totalcbmcomplete;
        _OPSAppointment_DIRouteTOMaster.Data._totalDNMaster.Sort = totalsort;

        $scope.$applyAsync(function () {
            $scope.SummaryMaster = 'Tấn: ' + Common.Number.ToNumber1(totaltoncomplete) + ' - Khối: ' + Common.Number.ToNumber1(totalcbmcomplete) + ' - Số chuyến: ' + totalsort;
        })
    };

    $scope.ReloadButton = function ($event) {
        var isUnApproved = true;
        var isApproved = true;
        var isTendered = true;
        var isHasApproved = false;
        var isHasUnApproved = false;
        var isHasTendered = false;
        $.each($scope.gridNoDNMaster.tbody.find('.HasDNGridGroup .chkChooseVehicle'), function (i, v) {
            if (this.checked) {
                var group = $(v).closest('.HasDNGridGroup');
                var sort = parseInt($(group).attr('tabid'));
                var itemSort = _OPSAppointment_DIRouteTOMaster.Data._dataHasDNSort[sort];
                if (itemSort.TypeID == 1) {
                    isUnApproved = false;
                    isHasUnApproved = true;
                    if (itemSort.TOMasterID > 0)
                        isHasTendered = true;
                } else {
                    if (itemSort.TypeID == 2) {
                        isHasApproved = true;
                        isApproved = false;
                        isTendered = false;
                    }
                }
            }
        });

        if (isHasApproved && isUnApproved) {
            $timeout(function () {
                $scope.IsShowUnApproved = true;
            }, 10);
        } else {
            $timeout(function () {
                $scope.IsShowUnApproved = false;
            }, 10);
        }
        if (isHasUnApproved && isApproved) {
            $timeout(function () {
                $scope.IsShowApproved = true;
                $scope.IsShowDelete = true;
            }, 10);
        } else {
            $timeout(function () {
                $scope.IsShowApproved = false;
                $scope.IsShowDelete = false;
            }, 10);
        }
        if (isHasTendered && isTendered) {
            $timeout(function () {
                $scope.IsShowTendered = true;
            }, 10);
        } else {
            $timeout(function () {
                $scope.IsShowTendered = false;
            }, 10);
        }
    };

    $scope.Approved_Click = function ($event) {
        Common.Log('Approved_Click');
        $event.preventDefault();
        var data = [];
        var error = [];
        $scope.gridNoDNMaster.tbody.find('.HasDNGridGroup').each(function () {
            var sort = parseInt($(this).attr('tabid'));
            var item = _OPSAppointment_DIRouteTOMaster.Data._dataHasDNSort[sort];
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
                            method: _OPSAppointment_DIRouteTOMaster.URL.Approved,
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
        var firstVendorID = -1;
        $scope.gridNoDNMaster.tbody.find('.chkChooseVehicle').each(function () {
            if (this.checked) {
                var group = $(this).closest('.HasDNGridGroup');
                var sort = parseInt($(group).attr('tabid'));
                var item = _OPSAppointment_DIRouteTOMaster.Data._dataHasDNSort[sort];

                if (sort > 0 && item.TOMasterID > 0) {
                    if (firstVendorID < 0)
                        firstVendorID = item.CreateVendorID;
                    data.push(item.TOMasterID);
                }
            }
        });
        if (data.length > 0) {
            var dataVEN = [];
            var dataVendor = $.extend(true, [], _OPSAppointment_DIRouteTOMaster.Data._dataHasDNVendor);
            dataVendor.splice(0, 1);
            if (firstVendorID < 0)
                firstVendorID = dataVendor[0].ID;
            dataVEN.push({ SortOrder: 1, VendorID: firstVendorID, IsManual: false, Debit: 0, ListVendor: dataVendor });
            dataVEN.push({ SortOrder: 2, VendorID: firstVendorID, IsManual: false, Debit: 0, ListVendor: dataVendor });
            dataVEN.push({ SortOrder: 3, VendorID: firstVendorID, IsManual: false, Debit: 0, ListVendor: dataVendor });
            $scope.OPSAppointment_DIRouteTOMaster_VendorRate_grid.dataSource.data(dataVEN);
            $timeout(function () {
                $scope.OPSAppointment_DIRouteTOMaster_VendorRate_grid.resize();
            }, 10);
            win.center().open();
        }
    };

    $scope.TenderSend_Click = function ($event, win, grid) {
        Common.Log('TenderSend_Click');
        $event.preventDefault();
        var data = [];
        $scope.gridNoDNMaster.tbody.find('.chkChooseVehicle').each(function () {
            if (this.checked) {
                var group = $(this).closest('.HasDNGridGroup');
                var sort = parseInt($(group).attr('tabid'));
                var item = _OPSAppointment_DIRouteTOMaster.Data._dataHasDNSort[sort];
                if (sort > 0 && item.TOMasterID > 0) {
                    data.push(item.TOMasterID);
                }
            }
        });
        if (data.length > 0) {
            $($scope.OPSAppointment_DIRouteTOMaster_VendorRate_grid.element).find('tbody tr[role="row"]').each(function () {
                var cboVendor = $($(this).find('input.cboVendor.cus-combobox')[1]).data('kendoComboBox');
                var chkIsManual = $($(this).find('.chkIsManual')[0]);
                var txtDebit = $($(this).find('input.txtDebit.cus-number')[1]).data('kendoNumericTextBox');
                var item = $scope.OPSAppointment_DIRouteTOMaster_VendorRate_grid.dataItem(this);
                var RateTimeVender = $($(this).find('input.RateTimeVender.cus-number')[1]).data('kendoNumericTextBox');
                item.VendorID = cboVendor.value();
                item.IsManual = chkIsManual.prop('checked');
                item.Debit = txtDebit.value()
                item.RateTime = RateTimeVender.value();
            });
            $rootScope.Message({
                Type: Common.Message.Type.Confirm,
                Msg: 'Bạn muốn gửi Tender các chuyến đã chọn ?',
                Ok: function () {
                    $rootScope.IsLoading = true;
                    Common.Services.Call($http, {
                        url: Common.Services.url.OPS,
                        method: _OPSAppointment_DIRouteTOMaster.URL.Tender,
                        data: { lst: data, lstTender: $scope.OPSAppointment_DIRouteTOMaster_VendorRate_grid.dataSource.data(), RateTime: $scope.RateTime },
                        success: function (res) {
                            Common.Services.Error(res, function (res) {
                                $scope.LoadData();
                                $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                                win.close();
                                // Gửi mail Tender
                                Common.Services.Call($http, {
                                    url: Common.Services.url.OPS,
                                    method: _OPSAppointment_DIRouteTOMaster.URL.SendMailToTender,
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

    $scope.UnApproved_Click = function ($event) {
        Common.Log('UnApproved_Click');
        $event.preventDefault();
        var data = [];
        $scope.gridNoDNMaster.tbody.find('.HasDNGridGroup').each(function () {
            var sort = parseInt($(this).attr('tabid'));
            var item = _OPSAppointment_DIRouteTOMaster.Data._dataHasDNSort[sort];
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
                        method: _OPSAppointment_DIRouteTOMaster.URL.UnApproved,
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
        $scope.gridNoDNMaster.tbody.find('.HasDNGridGroup').each(function () {
            var sort = parseInt($(this).attr('tabid'));
            var item = _OPSAppointment_DIRouteTOMaster.Data._dataHasDNSort[sort];
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
                        method: _OPSAppointment_DIRouteTOMaster.URL.Delete,
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

    $scope.Save_Click = function ($event, isConfirm) {
        Common.Log('Save_Click');
        if (Common.HasValue($event))
            $event.preventDefault();
        var flag = true;
        var error = '';
        $.each(_OPSAppointment_DIRouteTOMaster.Data._dataHasDNSort, function (i, v) {
            if (Common.HasValue(v)) {
                if (v.CreateSortOrder > 0 && (v.CreateVehicleCode == '')) {
                    // || v.CreateVehicleCode == '[Chờ nhập xe]'
                    error += ', ' + v.CreateSortOrder;
                    flag = false;
                }
            }
        });

        var data = [];
        $.each($scope.gridNoDNMaster.dataSource.data(), function (i, v) {
            //&& (v.Kg > 0 || v.CBM > 0 || v.Quantity > 0)
            if (v.CreateSortOrder > 0 && _OPSAppointment_DIRouteTOMaster.Data._dataHasDNSort[v.CreateSortOrder].TypeID < 2) {
                if (!Common.HasValue(v.CreateDriverName))
                    v.CreateDriverName = '';
                if (!Common.HasValue(v.CreateTelephone))
                    v.CreateTelephone = '';
                data.push(v);
            }
        });
        if (flag) {
            if (data.length > 0) {
                if (isConfirm) {
                    $rootScope.Message({
                        Type: Common.Message.Type.Confirm,
                        Msg: 'Bạn muốn lưu các chuyến đã lập ?',
                        Ok: function () {
                            $scope.SaveOnly(data, true);
                        }
                    });
                } else {
                    $scope.SaveOnly(data, true);
                }
            }
        }
        else
            $rootScope.Message({ Msg: 'Các chuyến ' + error.substr(2) + ' chưa chọn xe.', NotifyType: Common.Message.NotifyType.ERROR });
    };

    $scope.SaveOnly = function (data, isReload) {
        $rootScope.IsLoading = true;
        var dataDel = [];
        var dataAdd = [];
        $.each(_OPSAppointment_DIRouteTOMaster.Data._dataHasDNSort, function (i, v) {
            if (Common.HasValue(v)) {
                if (v.CreateSortOrder > 0 && v.TypeID < 2) {
                    if (v.Kg == 0 && v.CBM == 0 && v.Quantity == 0 && v.TOMasterID > 0)
                        dataDel.push(v.TOMasterID);
                    else
                        dataAdd.push(v);
                }
            }
        });
        _OPSAppointment_DIRouteTOMaster.Data._dataHasDNSort = dataAdd;
        _OPSAppointment_DIRouteTOMaster.Data._dataHasDNMaster = data;
        Common.Services.Call($http, {
            url: Common.Services.url.OPS,
            method: _OPSAppointment_DIRouteTOMaster.URL.Delete,
            data: { lst: dataDel },
            success: function (res) {
                Common.Services.Error(res, function (res) {
                    var lstVehicle = [];
                    $.each(_OPSAppointment_DIRouteTOMaster.Data._dataHasDNSort, function (i, v) {
                        if (v.IsChange)
                            lstVehicle.push(v);
                    });
                    Common.Services.Call($http, {
                        url: Common.Services.url.OPS,
                        method: _OPSAppointment_DIRouteTOMaster.URL.Save,
                        data: { lstOrder: _OPSAppointment_DIRouteTOMaster.Data._dataHasDNMaster, lstVehicle: lstVehicle },
                        success: function (res) {
                            Common.Services.Error(res, function (res) {
                                if (isReload)
                                    $scope.LoadData();
                                $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                            });
                        }
                    });
                });
            }
        });
    }

    $scope.CloseView_Click = function () {
        _OPSAppointment_DIRouteTOMaster.Data._dataDistrict = [];
        _OPSAppointment_DIRouteTOMaster.Data._createListDN = [];
        _OPSAppointment_DIRouteTOMaster.Data._createListID = [];
        _OPSAppointment_DIRouteTOMaster.Data._dataHasDNGroupProduct = [];
        _OPSAppointment_DIRouteTOMaster.Data._dataHasDN = [];
        _OPSAppointment_DIRouteTOMaster.Data._dataHasDNMaster = [];
        _OPSAppointment_DIRouteTOMaster.Data._dataHasDNSort = [];
        _OPSAppointment_DIRouteTOMaster.Data._dataHasDNVendor = [];
        _OPSAppointment_DIRouteTOMaster.Data._dataHasDNVehicle = [];
        $state.go('main.OPSAppointment.DI');
    };

    $scope.Close_Click = function ($event) {
        Common.Log('Close_Click');
        if (Common.HasValue($event))
            $event.preventDefault();
        if ($scope.IsShowSave) {
            $rootScope.Message({
                Type: Common.Message.Type.Confirm,
                Msg: 'Bạn có muốn lưu các chuyến đã lập trước khi đóng?',
                Ok: function () {
                    $scope.SaveOnly(false);
                    $scope.CloseView_Click();
                },
                Close: function () {
                    $scope.CloseView_Click();
                }
            });
        } else
            $scope.CloseView_Click();

    };

    $scope.SortEnter_Click = function ($event) {
        Common.Log('SortEnter_Click');
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
        var group = $(item).closest('.HasDNGridGroup');
        var tr = $(item).closest('tr');
        $(tr).find('.lblHasDNSplit,input').show();
        $(tr).find('.txtHasDNMaxWeight,.txtHasDNMaxCBM').hide();
        $(tr).find('.lblHasDNSplit.vehicle').hide();
        var sort = parseInt($(group).attr('tabid'));
        var dataVehicle = [];
        var itemSort = _OPSAppointment_DIRouteTOMaster.Data._dataHasDNSort[sort];
        if (itemSort.CreateVendorID == null || itemSort.CreateVendorID == 0 || itemSort.CreateVendorID == -1) {
            dataVehicle = _OPSAppointment_DIRouteTOMaster.Data._dataHasDNVehicleHome;
        } else {
            dataVehicle = _OPSAppointment_DIRouteTOMaster.Data._dataHasDNVehicleVEN[itemSort.CreateVendorID];
        }
        tr.find('input.cboHasDNVendor').kendoComboBox({
            index: 0, autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true,
            dataTextField: 'CustomerName', dataValueField: 'ID',
            dataSource: Common.DataSource.Local({
                data: _OPSAppointment_DIRouteTOMaster.Data._dataHasDNVendor,
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
                var item = _OPSAppointment_DIRouteTOMaster.Data._dataHasDNSort[sort];
                var sel = e.sender.dataItem();
                var CreateVendorID = sel != null ? sel.ID : 0;
                if (item.CreateVendorID != CreateVendorID) {
                    item.CreateVendorID = sel != null ? sel.ID : 0;
                    var cboVehicle = $(span).find("#cboHasDNVehicle").data("kendoComboBox");
                    cboVehicle.dataSource.data([]);
                    var txtDriver = $(span).find("input.txtHasDNDriverName").data("kendoAutoComplete");
                    var dataDriver = [];
                    if (item.CreateVendorID == null || item.CreateVendorID == 0 || item.CreateVendorID == -1) {
                        cboVehicle.dataSource.data(_OPSAppointment_DIRouteTOMaster.Data._dataHasDNVehicleHome);
                        dataDriver = _OPSAppointment_DIRouteTOMaster.Data._dataDriver;
                    } else
                        cboVehicle.dataSource.data(_OPSAppointment_DIRouteTOMaster.Data._dataHasDNVehicleVEN[item.CreateVendorID]);
                    item.CreateVehicleCode = cboVehicle.dataSource.data()[0].RegNo;
                    cboVehicle.text(item.CreateVehicleCode);
                    $timeout(function () {
                        txtDriver.dataSource.data(dataDriver);
                    }, 10);

                    $timeout(function () {
                        $scope.IsShowSave = true;
                    }, 10);

                    item.IsChange = true;
                }
            }
        });
        $rootScope.FocusKCombobox(tr.find('input.cboHasDNVendor[data-role="combobox"]'));

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
                    var item = _OPSAppointment_DIRouteTOMaster.Data._dataHasDNSort[sort];

                    var tmpMin, tmpMax;
                    $.each(_OPSAppointment_DIRouteTOMaster.Data._dataHasDNMaster, function (i, v) {
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
                    $.each(_OPSAppointment_DIRouteTOMaster.Data._dataHasDNVehicle, function (i, v) {
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
                        e.sender.text(_OPSAppointment_DIRouteTOMaster.Data._dataHasDNSort.CreateVehicleCode || "[Chờ nhập xe]");
                    }
                    else {
                        item.CreateVehicleCode = e.sender.text();

                        _OPSAppointment_DIRouteTOMaster.Data._vehicleTempData[sort] = item.CreateVehicleCode;

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

                        $.each(_OPSAppointment_DIRouteTOMaster.Data._dataHasDNVehicle, function (i, v) {
                            if (item.CreateVehicleCode == v.RegNo) {
                                if (Common.HasValue(v.MaxWeight) && v.MaxWeight > 0) {
                                    txtMaxWeight.text(" - [ Trọng tải xe: " + v.MaxWeight + " tấn ]");
                                    if (item.Kg > v.MaxWeight * 1000)
                                        txtOverLoadMessage.text(" - Quá trọng tải");
                                }
                                if (!flag) {
                                    driverName = v.DriverName;
                                    driverTel = v.Cellphone;
                                }
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
                        $timeout(function () {
                            $scope.IsShowSave = true;
                        }, 10);
                        item.IsChange = true;
                    }
                }
            }).data('kendoComboBox');
            cbo.text(_OPSAppointment_DIRouteTOMaster.Data._dataHasDNSort[itemSort.CreateSortOrder].CreateVehicleCode);
            $rootScope.FocusKCombobox($(this));
        });

        tr.find('input.txtHasDNDriverName').kendoAutoComplete({
            autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true, placeholder: "Nhập tài xế",
            dataTextField: 'DriverName', dataValueField: 'ID',
            dataSource: Common.DataSource.Local({
                data: _OPSAppointment_DIRouteTOMaster.Data._dataDriver,
                model: {
                    id: 'ID',
                    fields: {
                        ID: { type: 'number' },
                        DriverName: { type: 'string' }
                    }
                },
            }),
            change: function (e) {
                var ID = -1;
                var span = $(this.element).closest('span.HasDNGridGroup');
                sort = $(span).attr('tabid');
                var item = _OPSAppointment_DIRouteTOMaster.Data._dataHasDNSort[sort];
                item.CreateDriverName = this.value();
                if (Common.HasValue(this.dataItem(e.item))) {
                    ID = this.dataItem(e.item).ID;
                }
                item.IsChange = true;
                $timeout(function () {
                    $scope.IsShowSave = true;
                }, 10);

                if (ID > 0) {
                    var vehicleID = -1;
                    $.each(dataVehicle, function (i, v) {
                        if (v.RegNo == item.CreateVehicleCode) {
                            vehicleID = v.ID;
                        }
                    });
                    if (vehicleID > 0) {
                        var ETD = '';
                        var ETA = '';
                        $.each(_OPSAppointment_DIRouteTOMaster.Data._dataHasDNSort, function (i, v) {
                            if (Common.HasValue(v)) {
                                $.each($scope.gridNoDNMaster.dataSource.data(), function (i, m) {
                                    if (v.CreateSortOrder > 0 && v.CreateSortOrder == m.CreateSortOrder) {
                                        ETD = m.ETD;
                                        ETA = m.ETA;
                                    }
                                });
                            }
                        });

                        Common.Services.Call($http, {
                            url: Common.Services.url.OPS,
                            method: _OPSAppointment_DIRouteTOMaster.URL.Master_CheckDriver,
                            data: { vehicleID: vehicleID, driverID: ID, etd: ETD, eta: ETA },
                            success: function (res) {
                                if (res == false) {
                                    $rootScope.Message({
                                        Msg: 'Tài xế chưa đc phân công.',
                                        NotifyType: Common.Message.NotifyType.WARNING
                                    });
                                }
                            }
                        });
                    }
                    else {
                        $rootScope.Message({
                            Msg: 'Chưa chọn xe.',
                            NotifyType: Common.Message.NotifyType.ERROR
                        });
                    }

                }
                //alert(item.CreateDriverName);
            }
        });

        tr.find('input.txtHasDNMaxWeight').change(function (e) {
            var span = $(this).closest('span.HasDNGridGroup');
            sort = $(span).attr('tabid');
            var item = _OPSAppointment_DIRouteTOMaster.Data._dataHasDNSort[sort];
            item.MaxWeight = $(this).val();
            item.IsChange = true;
            $timeout(function () {
                $scope.IsShowSave = true;
            }, 10);
        });

        tr.find('input.txtHasDNMaxCBM').change(function (e) {
            var span = $(this).closest('span.HasDNGridGroup');
            sort = $(span).attr('tabid');
            var item = _OPSAppointment_DIRouteTOMaster.Data._dataHasDNSort[sort];
            item.MaxCBM = $(this).val();
            item.IsChange = true;
            $timeout(function () {
                $scope.IsShowSave = true;
            }, 10);
        });

        tr.find('input.txtHasDNTelephone').change(function (e) {
            var span = $(this).closest('span.HasDNGridGroup');
            sort = $(span).attr('tabid');
            var item = _OPSAppointment_DIRouteTOMaster.Data._dataHasDNSort[sort];
            item.CreateTelephone = $(this).val();
            item.IsChange = true;
            $timeout(function () {
                $scope.IsShowSave = true;
            }, 10);
        });

        tr.find('input.txtCreateDateTime').kendoDateTimePicker({
            format: Common.Date.Format.DMYHM,
            timeFormat: Common.Date.Format.HM,
            change: function (e) {
                var span = $(e.sender.element).closest('span.HasDNGridGroup');
                sort = $(span).attr('tabid');
                var item = _OPSAppointment_DIRouteTOMaster.Data._dataHasDNSort[sort];
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

                $timeout(function () {
                    $scope.IsShowSave = true;
                }, 10);
            }
        });
        $rootScope.FocusKDateTimePicker(tr.find('input.txtCreateDateTime'));
    };

    $scope.FindDriver = function (item) {
        var obj = {
            ID: -1,
            Name: '',
            Tel: ''
        };
        try {
            if (item.CreateVendorName == 'Xe nhà') {
                $.each(_OPSAppointment_DIRouteTOMaster.Data.FLMPlaning, function (i, v) {
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
        if ($scope.IsShowSave) {
            $rootScope.Message({
                Type: Common.Message.Type.Confirm,
                Msg: 'Bạn muốn lưu các chuyến đã lập ?',
                Ok: function () {
                    $scope.Save_Click($event, false);
                },
                Close: function () {
                    $scope.LoadData();
                }
            });
        } else {
            $scope.LoadData();
        }
        Common.Cookie.Set(_OPSAppointment_DIRouteTOMaster.Data.CookieSearch, JSON.stringify($scope.Search));
    }

    $scope.CalculatePL_Click = function (item) {
        Common.Log("CalculatePL_Click");

        var group = $(item).closest('.groupPLFooter');
        var sort = parseInt($(group).attr('tabid'));
        var master = _OPSAppointment_DIRouteTOMaster.Data._dataHasDNSort[sort];
        if (Common.HasValue(master)) {
            var lstOrder = [];
            $.each(_OPSAppointment_DIRouteTOMaster.Data._dataHasDNMaster, function (i, v) {
                if (v.CreateSortOrder == sort) {
                    lstOrder.push(v);
                }
            });
            if (lstOrder.length > 0) {
                Common.Services.Call($http, {
                    url: Common.Services.url.OPS,
                    method: _OPSAppointment_DIRouteTOMaster.URL.OfferPL,
                    data: { master: master, lstOrder: lstOrder },
                    success: function (res) {
                        Common.Services.Error(res, function (res) {
                            var footer = $scope.gridNoDNMaster.tbody.find(".groupPLFooter[tabid ='" + res.CreateSortOrder + "']");
                            var credit = footer.find(".Credit");
                            var debit = footer.find(".Debit");
                            var pl = footer.find(".PL");
                            var plClass = "";
                            if (res.PL > 0)
                                plClass = "green";
                            else
                                if (res.PL < 0)
                                    plClass = "red";
                            credit.text(kendo.toString(res.Credit, _OPSAppointment_DIRouteTOMaster.Data._formatMoney));
                            debit.text(kendo.toString(res.Debit, _OPSAppointment_DIRouteTOMaster.Data._formatMoney));
                            pl.removeClass("green");
                            pl.removeClass("red");
                            pl.addClass(plClass);
                            pl.text(kendo.toString(res.PL, _OPSAppointment_DIRouteTOMaster.Data._formatMoney));
                            $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                            var itemSort = _OPSAppointment_DIRouteTOMaster.Data._dataHasDNSort[res.CreateSortOrder];
                            if (Common.HasValue(itemSort)) {
                                itemSort.IsCalculate = true;
                            }
                        });
                    }
                });
            }
        }
    }

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
        $.each(_OPSAppointment_DIRouteTOMaster.Data._dataHasDNGroupProduct[dataItem.ID], function (i, v) {
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
        _OPSAppointment_DIRouteTOMaster.Data._previousItemValues = { Ton: dataItem.Ton, CBM: dataItem.CBM };
        var groupItems = $.extend(true, [], _OPSAppointment_DIRouteTOMaster.Data._dataHasDNGroupProduct[dataItem.ID]);
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
                    var index = _OPSAppointment_DIRouteTOMaster.Data._dataHasDN.indexOf(v);
                    _OPSAppointment_DIRouteTOMaster.Data._dataHasDN.splice(index, 1);
                }
            }
        });
        // Raise change event for this item to recalculate the sum
        dataItem.set("TimeModified", Date.now());
    };

    $scope.MergeCancel_Click = function ($event, grid) {
        Common.Log("MergeCancel_Click");
        $event.preventDefault();
        grid.dataSource.data(_OPSAppointment_DIRouteTOMaster.Data._dataHasDN);
        $scope.IsShowCombine = false;
    };

    $scope.FindItemByTr = function (tr) {
        var dataItem = null;
        $.each($scope.gridNoDNMaster.dataSource.data(), function (i, v) {
            if (v.uid == tr.attr("data-uid")) {
                dataItem = v;
                return false;
            }
        });
        return dataItem;
    }

    $scope.Change_View = function ($event, data) {
        $event.preventDefault();

        try {
            $('#tmp-container').append($('#grid-container'));
            $('#grid-view').empty();
            Common.Log($scope.gridNoDN)
            if (data == 1) {
                //Hor
                $scope.IsChangeView = false;
                $scope.viewSplitter_Options.orientation = "horizontal";
            } else if (data == 2) {
                //Ver
                $scope.IsChangeView = true;
                $scope.viewSplitter_Options.orientation = "vertical";
            }
            $timeout(function () {
                $('#grid-view').empty();
                $('#grid-view').append($('#tmp-container').children());
                $('#grid-container').resize();
                $scope.gridNoDNMaster.refresh();
                $scope.gridNoDN.refresh();
            }, 100);
        } catch (e) {
        }
    }

    $scope.CheckBox_All_Check = function ($event, grid) {

        var checked = $event.currentTarget.checked;
        if (checked == true) {
            grid.tbody.find('.HasDNGridGroup').each(function () {
                var CheckBox = $(this).find('input.chkChooseVehicle');
                CheckBox.prop('checked', true);
            });
        }
        else {
            grid.tbody.find('.HasDNGridGroup').each(function () {
                var CheckBox = $(this).find('input.chkChooseVehicle');
                CheckBox.prop('checked', false);
            });
        }
        $scope.ReloadButton();

    }

    $scope.Excel_Click = function ($event) {
        $event.preventDefault();

        $state.go("main.OPSAppointment.DIBookingConfirmation");
    };

    $scope.ViewA_Click = function ($event) {
        $event.preventDefault();

        $('#2view').kendoWindow({
            title: false,
            close: function (e) {
                var win = this;
                $timeout(function () {
                    $('#2view-container').append(win.element);
                    $('#2view').resize();
                    win.destroy();
                    $scope.IsFullScreen = false;
                }, 1)
            }
        });
        $('#2view').data('kendoWindow').maximize();
        $scope.IsFullScreen = true;
    }
    $scope.ViewB_Click = function ($event) {
        $event.preventDefault();

        var win = $('#2view').data('kendoWindow');
        $('#2view-container').append(win.element);
        $('#2view').resize();
        win.destroy();
        $scope.IsFullScreen = false;
    };
    //#region Action
    $scope.ShowSetting = function ($event, grid) {
        $event.preventDefault();

        $rootScope.ShowSetting({
            ListView: views.OPSAppointment,
            event: $event,
            grid: grid,
            current: $state.current
        });
    };
}]);