
/// <reference path="~/Scripts/jquery-1.9.1.intellisense.js" />
/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />

var _REPDIPL_SpotRate = {
    URL: {
        Search: 'REPDIPL_Spotrate',
        Template: 'REPDIPL_SpotrateTemplate',

        Read_Customer: 'REP_Customer_Read',
        Read_GroupOfProduct: 'REP_GroupOfProduct_Read',

        Spotrate_Export: 'REPDIPL_Spotrate_Export',

        SettingList: 'CUSSettingsReport_List',
        SettingSave: 'CUSSettingsReport_Save',
        SettingDelete: 'CUSSettingsReport_Delete',
        SettingTemplate: 'CUSSettingsReport_Template',

        SettingCusDeleteList: 'CUSSettingReport_CustomerDeleteList',
        SettingCusNotInList: 'CUSSettingReport_CustomerNotInList',
        SettingCusNotInSave: 'CUSSettingReport_CustomerNotInSave',

        SettingGOPDeleteList: 'CUSSettingReport_GroupOfProductDeleteList',
        SettingGOPNotInList: 'CUSSettingReport_GroupOfProductNotInList',
        SettingGOPNotInSave: 'CUSSettingReport_GroupOfProductNotInSave',

        OrderRoutingNotInList: 'CUSSettingReport_OrderRoutingNotInList',
        OrderRoutingNotInSave: 'CUSSettingReport_OrderRoutingNotInSave',
        OrderRoutingDeleteList: 'CUSSettingReport_OrderRoutingDeleteList',

        OPSRoutingNotInList: 'CUSSettingReport_OPSRoutingNotInList',
        OPSRoutingNotInSave: 'CUSSettingReport_OPSRoutingNotInSave',
        OPSRoutingDeleteList: 'CUSSettingReport_OPSRoutingDeleteList',

        SettingAction: 'REPDIPL_SettingDownload',
    },
}

angular.module('myapp').controller('REPDIPL_SpotRateCtrl', ['$rootScope', '$scope', '$http', '$location', '$state', '$timeout', '$stateParams', function ($rootScope, $scope, $http, $location, $state, $timeout, $stateParams) {
    if ($rootScope.IsPageComplete != true) return;
    Common.Log('REPDIPL_SpotRateCtrl');
    $rootScope.IsLoading = false;

    $scope.Auth = $rootScope.GetAuth();
    $scope.SettingItem = { ID: 0 };

    $scope.Item = {
        lstCustomerID: [],
        lstGroupID: [],
        DateFrom: Common.Date.AddDay(new Date(), -5),
        DateTo: new Date(),
    }

    $scope.mulCustomer_Options = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains',
        suggest: true, dataTextField: 'CustomerName', dataValueField: 'ID',
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields: {
                    CustomerName: { type: 'string' },
                    ID: { type: 'number' },
                }
            }
        }),
        change: function () {
            var lstid = this.value();
            var lst = this;
            $timeout(function () {
                $scope.SetWidth_Select(lst);
            }, 1);
            Common.Services.Call($http, {
                url: Common.Services.url.REP,
                method: _REPDIPL_SpotRate.URL.Read_GroupOfProduct,
                data: { lstid: lstid },
                success: function (res) {
                    $scope.mulGroupOfProduct_Options.dataSource.data(res.Data);
                }
            });
        },
    };

    $scope.isWidth = false;
    $scope.SetWidth_Select = function (lst) {
        var list = lst;
        var lst1 = lst.wrapper.find('.k-floatwrap:first ul li');
        var widthDiv = lst.wrapper.find('.k-floatwrap:first').width();
        var w = 0;
        var obj = null;
        var lst2 = [];
        if (lst1.length > 1) {
            $.each(lst1, function (i, v) {
                if ($(v).attr('data-show') != 'unshow') {
                    lst2.push(v);
                }
            });
        }
        else {
            lst2 = lst1;
        }

        $.each(lst2, function (i, v) {
            w += $(v).outerWidth(true);
            $(v).attr('data-show', 'show')
            if (w >= widthDiv) {
                $(v).hide();
                $(v).attr('data-show', 'unshow');
            }
            obj = v;
        });
        if (obj == null) {
            $scope.isWidth = false;
        }
        if (w >= widthDiv && !$scope.isWidth) {
            $scope.isWidth = true;
            $(obj).show();
            $(obj).html('...');
        }
        if (w > widthDiv) {
            $scope.SetWidth_Select(list);
        }
    }
    $scope.mulGroupOfProduct_Options = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains',
        suggest: true, dataTextField: 'GroupName', dataValueField: 'ID',
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields: {
                    GroupName: { type: 'string' },
                    ID: { type: 'number' },
                }
            }
        })
    };

    Common.Services.Call($http, {
        url: Common.Services.url.REP,
        method: _REPDIPL_SpotRate.URL.Read_Customer,
        data: {},
        success: function (res) {
            $scope.mulCustomer_Options.dataSource.data(res.Data);
        }
    });

    $scope.REPDIPL_gridOptions = {
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number', editable: false },
                    RequestDate: { type: 'date' },
                    DateConfig: { type: 'date' },
                    OrderDateConfig: { type: 'date' },
                    OrderGroupProductDateConfig: { type: 'date' },
                    OPSDateConfig: { type: 'date' },
                    OPSGroupProductDateConfig: { type: 'date' },
                    ETD: { type: 'date' },
                    OrderCreatedDate: { type: 'date' },
                    IncomeUnitPrice: { type: 'number' },
                    IncomeUnLoadUnitPrice: { type: 'number' },
                    IncomeUnLoad: { type: 'number' },
                    IncomeLoadUnitPrice: { type: 'number' },
                    IncomeLoad: { type: 'number' },
                    CostUnitPrice: { type: 'number' },
                    Cost: { type: 'number' },
                    CostUnLoadUnitPrice: { type: 'number' },
                    CostUnLoad: { type: 'number' },
                    CostLoadUnitPrice: { type: 'number' },
                    CostLoad: { type: 'number' },
                    TotalIncome: { type: 'number' },
                    TotalCost: { type: 'number' },
                    TotalPL: { type: 'number' },
                    Income: { type: 'number' },
                }
            },
            pageSize: 20
        }),
        height: '100%', pageable: true, sortable: { mode: 'multiple', allowUnsort: true }, columnMenu: false, resizable: true, reorderable: false, filterable: { mode: 'row' }, selectable: 'row', pageSize: 20,
        columns: [
            {
                field: 'SortOrder', title: '<b>STT Đơn hàng</b><br>[SortOrder]', width: '120px',
                filterable: { cell: { operator: 'contains', showOperators: false } }, attributes: { style: "text-align: center; " },
            },
            {
                field: 'OrderCode', title: '<b>Đơn hàng</b><br>[OrderCode]', width: '120px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'CustomerCode', title: '<b>Mã khách hàng</b><br>[CustomerCode]', width: '120px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'CustomerName', title: '<b>Khách hàng</b><br>[CustomerName]', width: '120px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'VendorCode', title: '<b>Mã nhà vận tải</b><br>[VendorCode]', width: '120px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'VendorName', title: '<b>Nhà vận tải</b><br>[VendorName]', width: '120px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'VendorShortName', title: '<b>Tên tắt nhà vận tải</b><br>[VendorShortName]', width: '120px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'VehicleCode', title: '<b>Số xe</b><br>[VehicleCode]', width: '120px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'DriverName', title: '<b>Tài xế</b><br>[DriverName]', width: '120px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'DNCode', title: '<b>Số DN</b><br>[DNCode]', width: '120px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'SOCode', title: '<b>Số SO</b><br>[SOCode]', width: '120px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'DateConfig', title: '<b>Ngày tính giá</b><br>[DateConfig]', width: '150px', template: '#=Common.Date.FromJsonDMYHM(DateConfig)#',
                filterable: {
                    cell: {
                        template: function (e) {
                            var element = e.element.parent();
                            element.empty();
                            $("<input class='dtp-filter-from' data-field='DateConfig' style='width:50%; float:left;' />").appendTo(element);
                            $("<input class='dtp-filter-to' data-field='DateConfig' style='width:50%; float:left;' />").appendTo(element);
                        }, showOperators: false
                    }
                },
            },
            {
                field: 'RequestDate', title: '<b>Ngày y/c v/c</b><br>[RequestDate]', width: '150px', template: '#=Common.Date.FromJsonDMYHM(RequestDate)#',
                filterable: {
                    cell: {
                        template: function (e) {
                            var element = e.element.parent();
                            element.empty();
                            $("<input class='dtp-filter-from' data-field='RequestDate' style='width:50%; float:left;' />").appendTo(element);
                            $("<input class='dtp-filter-to' data-field='RequestDate' style='width:50%; float:left;' />").appendTo(element);
                        }, showOperators: false
                    }
                },
            },
            {
                field: 'StockCode', title: '<b>Mã điểm lấy</b><br>[StockCode]', width: '120px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'StockName', title: '<b>Tên điểm lấy</b><br>[StockName]', width: '150px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'StockAddress', title: '<b>Điểm lấy</b><br>[StockAddress]', width: '200px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'PartnerCode', title: '<b>Mã NPP</b><br>[PartnerCode]', width: '150px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'PartnerName', title: '<b>Nhà phân phối</b><br>[PartnerName]', width: '200px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'PartnerCodeName', title: '<b>Mã + Tên phân phối</b><br>[PartnerCodeName]', width: '200px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'Address', title: '<b>Điểm giao</b><br>[Address]', width: '120px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'LocationToProvince', title: '<b>Tỉnh giao</b><br>[LocationToProvince]', width: '120px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'LocationToDistrict', title: '<b>Quận huyện giao</b><br>[LocationToDistrict]', width: '120px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'GroupOfLocationToCode', title: '<b>Mã loại điểm giao</b><br>[GroupOfLocationToCode]', width: '120px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'GroupOfLocationToName', title: '<b>Tên loại điểm giao</b><br>[GroupOfLocationToName]', width: '120px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'CUSRoutingCode', title: '<b>Mã cung đường</b><br>[CUSRoutingCode]', width: '120px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'CUSRoutingName', title: '<b>Cung đường</b><br> [CUSRoutingName]', width: '120px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'GroupOfVehicleCode', title: '<b>GroupOfVehicleCode</b><br>[GroupOfVehicleCode]', width: '120px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'GroupOfVehicleName', title: '<b>GroupOfVehicleName</b><br>[GroupOfVehicleName]', width: '120px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'GroupOfVehicleCodeVEN', title: '<b>GroupOfVehicleCodeVEN</b><br>[GroupOfVehicleCodeVEN]', width: '120px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'GroupOfVehicleNameVEN', title: '<b>GroupOfVehicleNameVEN</b><br>[GroupOfVehicleNameVEN]', width: '120px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'GroupOfProductCode', title: '<b>Mã nhóm hàng</b><br>[GroupOfProductCode]', width: '120px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'GroupOfProductName', title: '<b>Nhóm hàng</b><br>[GroupOfProductName]', width: '120px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'GroupOfProductVendorCode', title: '<b>Mã loại hàng</b><br>[GroupOfProductVendorCode]', width: '120px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'GroupOfProductVendorName', title: '<b>Loại hàng</b><br>[GroupOfProductVendorName]', width: '120px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'ProductCode', title: '<b>Mã sản phẩm</b><br>[ProductCode]', width: '120px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'ProductName', title: '<b>Sản phẩm</b><br>[ProductName]', width: '120px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'ProductDescription', title: '<b>Sản phẩm mô tả</b><br>[ProductDescription]', width: '120px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'KgTranfer', title: '<b>Kg lấy</b><br>[KgTranfer]', width: '120px', template: '#=KgTranfer==null?" ":Common.Number.ToNumber3(KgTranfer)#',
                filterable: { cell: { operator: 'gte', showOperators: false } }
            },
            {
                field: 'TonTranfer', title: '<b>Tấn lấy</b><br>[TonTranfer]', width: '120px', template: '#=TonTranfer==null?" ":Common.Number.ToNumber3(TonTranfer)#',
                filterable: { cell: { operator: 'gte', showOperators: false } }
            },
            {
                field: 'CBMTranfer', title: '<b>Khối lấy</b><br>[CBMTranfer]', width: '120px', template: '#=CBMTranfer==null?" ":Common.Number.ToNumber3(CBMTranfer)#',
                filterable: { cell: { operator: 'gte', showOperators: false } }
            },
            {
                field: 'QuantityTranfer', title: '<b>Số lượng lấy</b><br>[QuantityTranfer]', width: '120px', template: '#=QuantityTranfer==null?" ":Common.Number.ToNumber3(QuantityTranfer)#',
                filterable: { cell: { operator: 'gte', showOperators: false } }
            },
            {
                field: 'KgBBGN', title: '<b>Kg giao</b><br>[KgBBGN]', width: '120px', template: '#=KgBBGN==null?" ":Common.Number.ToNumber3(KgBBGN)#',
                filterable: { cell: { operator: 'gte', showOperators: false } }
            },
            {
                field: 'TonBBGN', title: '<b>Tấn giao</b><br>[TonBBGN]', width: '120px', template: '#=TonBBGN==null?" ":Common.Number.ToNumber3(TonBBGN)#',
                filterable: { cell: { operator: 'gte', showOperators: false } }
            },
            {
                field: 'CBMBBGN', title: '<b>Khối giao</b><br>[CBMBBGN]', width: '120px', template: '#=CBMBBGN==null?" ":Common.Number.ToNumber3(CBMBBGN)#',
                filterable: { cell: { operator: 'gte', showOperators: false } }
            },
            {
                field: 'QuantityBBGN', title: '<b>Số lượng giao</b><br>[QuantityBBGN]', width: '120px', template: '#=QuantityBBGN==null?" ":Common.Number.ToNumber3(QuantityBBGN)#',
                filterable: { cell: { operator: 'gte', showOperators: false } }
            },
            {
                field: 'KgReturn', title: '<b>Kg trả về</b><br>[KgReturn]', width: '120px', template: '#=KgReturn==null?" ":Common.Number.ToNumber3(KgReturn)#',
                filterable: { cell: { operator: 'gte', showOperators: false } }
            },
            {
                field: 'TonReturn', title: '<b>Tấn trả về</b><br>[TonReturn]', width: '120px', template: '#=TonReturn==null?" ":Common.Number.ToNumber3(TonReturn)#',
                filterable: { cell: { operator: 'gte', showOperators: false } }
            },
            {
                field: 'CBMReturn', title: '<b>Khối trả về</b><br>[CBMReturn]', width: '120px', template: '#=CBMReturn==null?" ":Common.Number.ToNumber3(CBMReturn)#',
                filterable: { cell: { operator: 'gte', showOperators: false } }
            },
            {
                field: 'QuantityReturn', title: '<b>Số lượng trả về</b><br>[QuantityReturn]', width: '120px', template: '#=QuantityReturn==null?" ":Common.Number.ToNumber3(QuantityReturn)#',
                filterable: { cell: { operator: 'gte', showOperators: false } }
            },
            {
                field: 'Price', title: '<b>Đơn giá</b><br>[Price]', width: '120px', template: '#=Price==null?" ":Common.Number.ToMoney(Price)#',
                filterable: { cell: { operator: 'gte', showOperators: false } }
            },
            {
                field: 'TotalPrice', title: '<b>Thành tiền</b><br>[TotalPrice]', width: '120px', template: '#=TotalPrice==null?" ":Common.Number.ToMoney(TotalPrice)#',
                filterable: { cell: { operator: 'gte', showOperators: false } }
            },
            {
                field: 'OrderCreatedBy', title: '<b>Người tạo đơn</b><br>[OrderCreatedBy]', width: '120px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'OrderCreatedDate', title: '<b>Ngày tạo đơn</b><br>[OrderCreatedDate]', width: '150px', template: '#=Common.Date.FromJsonDMYHM(OrderCreatedDate)#',
                filterable: {
                    cell: {
                        template: function (e) {
                            var element = e.element.parent();
                            element.empty();
                            $("<input class='dtp-filter-from' data-field='OrderCreatedDate' style='width:50%; float:left;' />").appendTo(element);
                            $("<input class='dtp-filter-to' data-field='OrderCreatedDate' style='width:50%; float:left;' />").appendTo(element);
                        }, showOperators: false
                    }
                },
            },
            {
                field: 'OrderDateConfig', title: '<b>Ngày tính giá đơn hàng</b><br>[OrderDateConfig]', width: '150px', template: '#=Common.Date.FromJsonDMYHM(OrderDateConfig)#',
                filterable: {
                    cell: {
                        template: function (e) {
                            var element = e.element.parent();
                            element.empty();
                            $("<input class='dtp-filter-from' data-field='OrderDateConfig' style='width:50%; float:left;' />").appendTo(element);
                            $("<input class='dtp-filter-to' data-field='OrderDateConfig' style='width:50%; float:left;' />").appendTo(element);
                        }, showOperators: false
                    }
                },
            },
            {
                field: 'OrderGroupProductDateConfig', title: '<b>Ngày tính giá c.t đơn hàng</b><br>[OrderGroupProductDateConfig]', width: '150px', template: '#=Common.Date.FromJsonDMYHM(OrderGroupProductDateConfig)#',
                filterable: {
                    cell: {
                        template: function (e) {
                            var element = e.element.parent();
                            element.empty();
                            $("<input class='dtp-filter-from' data-field='OrderGroupProductDateConfig' style='width:50%; float:left;' />").appendTo(element);
                            $("<input class='dtp-filter-to' data-field='OrderGroupProductDateConfig' style='width:50%; float:left;' />").appendTo(element);
                        }, showOperators: false
                    }
                },
            },
            {
                field: 'OrderContract', title: '<b>Hợp đồng ĐH</b><br>[OrderContract]', width: '120px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'OrderRouting', title: '<b>Cung đường đơn hàng</b><br>[OrderRouting]', width: '120px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'OrderGroupProductRouting', title: '<b>Cung đường c.t ĐH</b><br>[OrderGroupProductRouting]', width: '120px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'OPSDateConfig', title: '<b>Ngày tính giá chuyến</b><br>[OPSDateConfig]', width: '150px', template: '#=Common.Date.FromJsonDMYHM(OPSDateConfig)#',
                filterable: {
                    cell: {
                        template: function (e) {
                            var element = e.element.parent();
                            element.empty();
                            $("<input class='dtp-filter-from' data-field='OPSDateConfig' style='width:50%; float:left;' />").appendTo(element);
                            $("<input class='dtp-filter-to' data-field='OPSDateConfig' style='width:50%; float:left;' />").appendTo(element);
                        }, showOperators: false
                    }
                },
            },
            {
                field: 'OPSGroupProductDateConfig', title: '<b>Ngày tính giá c.t chuyến</b><br>[OPSGroupProductDateConfig]', width: '150px', template: '#=Common.Date.FromJsonDMYHM(OPSGroupProductDateConfig)#',
                filterable: {
                    cell: {
                        template: function (e) {
                            var element = e.element.parent();
                            element.empty();
                            $("<input class='dtp-filter-from' data-field='OPSGroupProductDateConfig' style='width:50%; float:left;' />").appendTo(element);
                            $("<input class='dtp-filter-to' data-field='OPSGroupProductDateConfig' style='width:50%; float:left;' />").appendTo(element);
                        }, showOperators: false
                    }
                },
            },
            {
                field: 'OPSContract', title: '<b>Hợp đồng chuyến</b><br>[OPSContract]', width: '120px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'OPSRouting', title: '<b>Cung đường chuyến</b><br>[OPSRouting]', width: '120px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'OPSGroupProductRouting', title: '<b>Cung đường c.t chuyến</b><br>[OPSGroupProductRouting]', width: '120px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'KM', title: '<b>KM</b><br>[KM]', width: '120px',
                filterable: { cell: { operator: 'gte', showOperators: false } }
            },
            {
                field: 'ETD', title: '<b>Ngày vận chuyển</b><br>[ETD]', width: '150px', template: '#=Common.Date.FromJsonDMYHM(ETD)#',
                filterable: {
                    cell: {
                        template: function (e) {
                            var element = e.element.parent();
                            element.empty();
                            $("<input class='dtp-filter-from' data-field='ETD' style='width:50%; float:left;' />").appendTo(element);
                            $("<input class='dtp-filter-to' data-field='ETD' style='width:50%; float:left;' />").appendTo(element);
                        }, showOperators: false
                    }
                },
            },
            {
                field: 'ETA', title: '<b>ETA</b><br>[ETA]', width: '150px', template: '#=Common.Date.FromJsonDMYHM(ETA)#',
                filterable: {
                    cell: {
                        template: function (e) {
                            var element = e.element.parent();
                            element.empty();
                            $("<input class='dtp-filter-from' data-field='ETA' style='width:50%; float:left;' />").appendTo(element);
                            $("<input class='dtp-filter-to' data-field='ETA' style='width:50%; float:left;' />").appendTo(element);
                        }, showOperators: false
                    }
                },
            },
            {
                field: 'ATD', title: '<b>ATD</b><br>[ATD]', width: '150px', template: '#=Common.Date.FromJsonDMYHM(ATD)#',
                filterable: {
                    cell: {
                        template: function (e) {
                            var element = e.element.parent();
                            element.empty();
                            $("<input class='dtp-filter-from' data-field='ATD' style='width:50%; float:left;' />").appendTo(element);
                            $("<input class='dtp-filter-to' data-field='ATD' style='width:50%; float:left;' />").appendTo(element);
                        }, showOperators: false
                    }
                },
            },
            {
                field: 'ATA', title: '<b>ATA</b><br>[ATA]', width: '150px', template: '#=Common.Date.FromJsonDMYHM(ATA)#',
                filterable: {
                    cell: {
                        template: function (e) {
                            var element = e.element.parent();
                            element.empty();
                            $("<input class='dtp-filter-from' data-field='ATA' style='width:50%; float:left;' />").appendTo(element);
                            $("<input class='dtp-filter-to' data-field='ATA' style='width:50%; float:left;' />").appendTo(element);
                        }, showOperators: false
                    }
                },
            },
           {
               field: 'DateFromCome', title: '<b>Ngày đến kho</b><br>[DateFromCome]', width: '150px', template: '#=Common.Date.FromJsonDMYHM(DateFromCome)#',
               filterable: {
                   cell: {
                       template: function (e) {
                           var element = e.element.parent();
                           element.empty();
                           $("<input class='dtp-filter-from' data-field='DateFromCome' style='width:50%; float:left;' />").appendTo(element);
                           $("<input class='dtp-filter-to' data-field='DateFromCome' style='width:50%; float:left;' />").appendTo(element);
                       }, showOperators: false
                   }
               },
           },
            {
                field: 'DateFromLeave', title: '<b>Ngày rời kho</b><br>[DateFromLeave]', width: '150px', template: '#=Common.Date.FromJsonDMYHM(DateFromLeave)#',
                filterable: {
                    cell: {
                        template: function (e) {
                            var element = e.element.parent();
                            element.empty();
                            $("<input class='dtp-filter-from' data-field='DateFromLeave' style='width:50%; float:left;' />").appendTo(element);
                            $("<input class='dtp-filter-to' data-field='DateFromLeave' style='width:50%; float:left;' />").appendTo(element);
                        }, showOperators: false
                    }
                },
            },
            {
                field: 'DateFromLoadStart', title: '<b>T.gian vào máng</b><br>[DateFromLoadStart]', width: '150px', template: '#=Common.Date.FromJsonDMYHM(DateFromLoadStart)#',
                filterable: {
                    cell: {
                        template: function (e) {
                            var element = e.element.parent();
                            element.empty();
                            $("<input class='dtp-filter-from' data-field='DateFromLoadStart' style='width:50%; float:left;' />").appendTo(element);
                            $("<input class='dtp-filter-to' data-field='DateFromLoadStart' style='width:50%; float:left;' />").appendTo(element);
                        }, showOperators: false
                    }
                },
            },
            {
                field: 'DateFromLoadEnd', title: '<b>T.gian ra máng</b><br>[DateFromLoadEnd]', width: '150px', template: '#=Common.Date.FromJsonDMYHM(DateFromLoadEnd)#',
                filterable: {
                    cell: {
                        template: function (e) {
                            var element = e.element.parent();
                            element.empty();
                            $("<input class='dtp-filter-from' data-field='DateFromLoadEnd' style='width:50%; float:left;' />").appendTo(element);
                            $("<input class='dtp-filter-to' data-field='DateFromLoadEnd' style='width:50%; float:left;' />").appendTo(element);
                        }, showOperators: false
                    }
                },
            },
            {
                field: 'DateToCome', title: '<b>Ngày đến điểm giao</b><br>[DateToCome]', width: '150px', template: '#=Common.Date.FromJsonDMYHM(DateToCome)#',
                filterable: {
                    cell: {
                        template: function (e) {
                            var element = e.element.parent();
                            element.empty();
                            $("<input class='dtp-filter-from' data-field='DateToCome' style='width:50%; float:left;' />").appendTo(element);
                            $("<input class='dtp-filter-to' data-field='DateToCome' style='width:50%; float:left;' />").appendTo(element);
                        }, showOperators: false
                    }
                },
            },
            {
                field: 'DateToLeave', title: '<b>Ngày rời điểm giao</b><br>[DateToLeave]', width: '150px', template: '#=Common.Date.FromJsonDMYHM(DateToLeave)#',
                filterable: {
                    cell: {
                        template: function (e) {
                            var element = e.element.parent();
                            element.empty();
                            $("<input class='dtp-filter-from' data-field='DateToLeave' style='width:50%; float:left;' />").appendTo(element);
                            $("<input class='dtp-filter-to' data-field='DateToLeave' style='width:50%; float:left;' />").appendTo(element);
                        }, showOperators: false
                    }
                },
            },
            {
                field: 'DateToLoadStart', title: '<b>T.gian bắt đầu dỡ hàng</b><br>[DateToLoadStart]', width: '150px', template: '#=Common.Date.FromJsonDMYHM(DateToLoadStart)#',
                filterable: {
                    cell: {
                        template: function (e) {
                            var element = e.element.parent();
                            element.empty();
                            $("<input class='dtp-filter-from' data-field='DateToLoadStart' style='width:50%; float:left;' />").appendTo(element);
                            $("<input class='dtp-filter-to' data-field='DateToLoadStart' style='width:50%; float:left;' />").appendTo(element);
                        }, showOperators: false
                    }
                },
            },
            {
                field: 'DateToLoadEnd', title: '<b>T.gian kết thúc dỡ hàng</b><br>[DateToLoadEnd]', width: '150px', template: '#=Common.Date.FromJsonDMYHM(DateToLoadEnd)#',
                filterable: {
                    cell: {
                        template: function (e) {
                            var element = e.element.parent();
                            element.empty();
                            $("<input class='dtp-filter-from' data-field='DateToLoadEnd' style='width:50%; float:left;' />").appendTo(element);
                            $("<input class='dtp-filter-to' data-field='DateToLoadEnd' style='width:50%; float:left;' />").appendTo(element);
                        }, showOperators: false
                    }
                },
            },
            {
                field: 'PODStatus', title: '<b>Tình trạng c/t</b><br>[PODStatus]', width: '120px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'InvoiceDate', title: '<b>Ngày nhận c.từ</b><br>[InvoiceDate]', width: '150px', template: '#=Common.Date.FromJsonDMYHM(InvoiceDate)#',
                filterable: {
                    cell: {
                        template: function (e) {
                            var element = e.element.parent();
                            element.empty();
                            $("<input class='dtp-filter-from' data-field='InvoiceDate' style='width:50%; float:left;' />").appendTo(element);
                            $("<input class='dtp-filter-to' data-field='InvoiceDate' style='width:50%; float:left;' />").appendTo(element);
                        }, showOperators: false
                    }
                },
            },
            {
                field: 'InvoiceNote', title: '<b>Số c.từ</b><br>[InvoiceNote]', width: '120px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'InvoiceReturnDate', title: '<b>Ngày nhận c.từ trả về</b><br>[InvoiceReturnDate]', width: '150px', template: '#=Common.Date.FromJsonDMYHM(InvoiceReturnDate)#',
                filterable: {
                    cell: {
                        template: function (e) {
                            var element = e.element.parent();
                            element.empty();
                            $("<input class='dtp-filter-from' data-field='InvoiceReturnDate' style='width:50%; float:left;' />").appendTo(element);
                            $("<input class='dtp-filter-to' data-field='InvoiceReturnDate' style='width:50%; float:left;' />").appendTo(element);
                        }, showOperators: false
                    }
                },
            },
            {
                field: 'InvoiceReturnNote', title: '<b>Số c.từ trả về</b><br>[InvoiceReturnNote]', width: '120px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'TransportModeIncome', title: '<b>Loại hình v.chuyển thu</b><br>[TransportModeIncome]', width: '120px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'TransportModeCost', title: '<b>Loại hình v.chuyển chi</b><br>[TransportModeCost]', width: '120px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'ExternalCode', title: '<b>Mã giao dịch</b><br>[ExternalCode]', width: '120px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'ExternalDate', title: '<b>Ngày giao dịch</b><br>[ExternalDate]', width: '120px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'UserDefine1', title: '<b>Định nghĩa 1</b><br>[UserDefine1]', width: '120px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'UserDefine2', title: '<b>Định nghĩa 2</b><br>[UserDefine2]', width: '120px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },

            {
                field: 'TOMasterCode', title: '<b>Mã chuyến</b><br>[TOMasterCode]', width: '120px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
             {
                 field: 'TotalLocation', title: '<b>Tổng số điểm giao</b><br>[TotalLocation]', width: '120px',
                 filterable: { cell: { operator: 'equal', showOperators: false } }
             },
            {
                field: 'TOMasterNote1', title: '<b>Ghi chú chuyến 1</b><br>[TOMasterNote1]', width: '120px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'TOMasterNote2', title: '<b>Ghi chú chuyến 2</b><br>[TOMasterNote2]', width: '120px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'OPSGroupNote', title: '<b>Ghi chú</b><br>[OPSGroupNote]', width: '120px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'OPSGroupNote1', title: '<b>Ghi chú 1</b><br>[OPSGroupNote1]', width: '120px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'OPSGroupNote2', title: '<b>Ghi chú 2</b><br>[OPSGroupNote2]', width: '120px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'ORDGroupNote1', title: '<b>Ghi chú Đ/h 1</b><br>[ORDGroupNote1]', width: '120px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'ORDGroupNote2', title: '<b>Ghi chú Đ/h 2</b><br>[ORDGroupNote2]', width: '120px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'TotalLocationMaster', title: '<b>Tổng số Đ/đ theo chuyến</b><br>[TotalLocationMaster]', width: '120px',
                filterable: { cell: { operator: 'equal', showOperators: false } }
            },
            {
                field: 'TotalLocationMasterDelivery', title: '<b>Tổng số Đ/đ giao hàng theo chuyến</b><br>[TotalLocationMasterDelivery]', width: '120px',
                filterable: { cell: { operator: 'equal', showOperators: false } }
            },
            {
                field: 'TotalLocationOrder', title: '<b>Tổng số Đ/đ theo Đ/h</b><br>[TotalLocationOrder]', width: '120px',
                filterable: { cell: { operator: 'equal', showOperators: false } }
            },
            {
                field: 'TotalLocationOrderDelivery', title: '<b>Tổng số Đ/đ giao hàng theo Đ/h</b><br>[TotalLocationOrderDelivery]', width: '120px',
                filterable: { cell: { operator: 'equal', showOperators: false } }
            },
            {
                field: 'HasCashCollect', title: '<b>Có thu hộ</b><br>[HasCashCollect]', width: '120px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            { title: ' ', sortable: false, filterable: false, menu: false }
        ],
    };

    $timeout(function () {
        $('.dtp-filter-from').kendoDatePicker({
            format: Common.Date.Format.DDMMYY,
            change: function (e) {
                try {
                    var element = this.wrapper.parent();
                    var field = e.sender.element.data('field');
                    var dtp_To = $(element).find('.dtp-filter-to[data-field=' + field + ']').data('kendoDatePicker');
                    var f = this.value();
                    var t = dtp_To.value();

                    var f_filter = { field: field, operator: "gte", value: f };
                    var t_filter = { field: field, operator: "lte", value: t };
                    var filters = [];
                    if (Common.HasValue($scope.REPDIPL_gridOptions.dataSource.filter())) {
                        filters = $scope.REPDIPL_gridOptions.dataSource.filter().filters;
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
                        if (Common.HasValue(f))
                            filters.push(f_filter);
                        if (Common.HasValue(t))
                            filters.push(t_filter);

                    }
                    $scope.REPDIPL_gridOptions.dataSource.filter(filters);
                }
                catch (e) {
                    $rootScope.Message({ Msg: 'Sai dữ liệu!' });
                }
            }
        })

        $('.dtp-filter-to').kendoDatePicker({
            format: Common.Date.Format.DDMMYY,
            change: function (e) {
                try {
                    var element = this.wrapper.parent();
                    var field = e.sender.element.data('field');
                    var dtp_From = $(element).find('.dtp-filter-from[data-field=' + field + ']').data('kendoDatePicker');
                    var f = dtp_From.value();
                    var t = this.value();

                    var f_filter = { field: field, operator: "gte", value: f };
                    var t_filter = { field: field, operator: "lte", value: t };

                    var filters = [];
                    if (Common.HasValue($scope.REPDIPL_gridOptions.dataSource.filter())) {
                        filters = $scope.REPDIPL_gridOptions.dataSource.filter().filters;
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
                        if (Common.HasValue(f))
                            filters.push(f_filter);
                        if (Common.HasValue(t))
                            filters.push(t_filter);
                    }
                    $scope.REPDIPL_gridOptions.dataSource.filter(filters);
                }
                catch (e) {
                    $rootScope.Message({ Msg: 'Sai dữ liệu!' });
                }
            }
        })
    }, 500);

    $scope.Search_Click = function ($event) {
        $event.preventDefault();

        $rootScope.IsLoading = true;
        Common.Services.Call($http, {
            url: Common.Services.url.REP,
            method: _REPDIPL_SpotRate.URL.Search,
            data: { lstid: $scope.Item.lstCustomerID, lstgroupid: $scope.Item.lstGroupID, dtfrom: $scope.Item.DateFrom, dtto: $scope.Item.DateTo },
            success: function (res) {
                $rootScope.IsLoading = false;
                angular.forEach(res, function (item, dix) {
                    item.RequestDate = kendo.parseDate(item.RequestDate);
                    item.DateConfig = kendo.parseDate(item.DateConfig);
                    item.OrderDateConfig = kendo.parseDate(item.OrderDateConfig);
                    item.OrderGroupProductDateConfig = kendo.parseDate(item.OrderGroupProductDateConfig);
                    item.OPSDateConfig = kendo.parseDate(item.OPSDateConfig);
                    item.OPSGroupProductDateConfig = kendo.parseDate(item.OPSGroupProductDateConfig);
                    item.ETD = kendo.parseDate(item.ETD);
                    item.OrderCreatedDate = kendo.parseDate(item.OrderCreatedDate);
                });

                $scope.REPDIPL_gridOptions.dataSource.data(res);
            },
            error: function (res) {
                $rootScope.IsLoading = false;
            }
        });
    };

    //$scope.Excel_Click = function ($event) {
    //    $event.preventDefault();

    //    var request = Common.Request.CreateFromGrid($scope.REPDIPL_gridOptions.dataSource);

    //    var functionID = $rootScope.FunctionItem.ID;
    //    $rootScope.UploadFile({
    //        IsImage: false, ID: functionID, AllowChange: true, ShowChoose: true,
    //        Type: Common.CATTypeOfFileCode.TEMPLATEREPORT,
    //        Complete: function (file) {
    //            $rootScope.IsLoading = true;
    //            Common.Services.Call($http, {
    //                url: Common.Services.url.REP,
    //                method: _REPDIPL_SpotRate.URL.Template,
    //                data: { itemfile: file, lstid: $scope.Item.lstCustomerID, lstgroupid: $scope.Item.lstGroupID, dtfrom: $scope.Item.DateFrom, dtto: $scope.Item.DateTo, request: request },
    //                success: function (res) {
    //                    $rootScope.IsLoading = false;
    //                    $rootScope.DownloadFile(res);
    //                },
    //                error: function (res) {
    //                    $rootScope.IsLoading = false;
    //                }
    //            });
    //        }
    //    });
    //};

    $scope.Excel_Click = function ($event) {
        $event.preventDefault();
    };

    $scope.ShowSetting = function ($event) {
        $event.preventDefault();

        $rootScope.ShowSetting({
            ListView: views.REPDIPL,
            event: $event,
            current: $state.current
        });
    };


    //#region Setting Report
    $scope.SettingItem = { ID: 0 };
    $scope.SettingHasChoose = false;
    $scope.settingReport_GridOptions = {
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number', editable: false },
                    IsChoose: { type: 'boolean' },
                }
            },
            pageSize: 20
        }),
        height: '100%', pageable: true, sortable: true, columnMenu: false, filterable: { mode: 'row' }, resizable: true, editable: false, selectable: "row",
        columns: [
            {
                title: ' ', width: '35px',
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,settingReport_Grid,settingReport_GridChoose_Change)" />',
                headerAttributes: { style: 'text-align: center;' }, attributes: { style: 'text-align: center;' },
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,settingReport_Grid,settingReport_GridChoose_Change)" />',
                filterable: false, sortable: false
            },
            {
                title: ' ', width: '90px',
                template: '<a href="/" ng-click="settingReport_GridEdit_Click($event,SettingReport_win,dataItem,Setting_vform)" class="k-button"><i class="fa fa-pencil"></i></a>' +
                    '<a href="/" ng-click="SettingReport_ActionClick($event,dataItem)" class="k-button"><i class="fa fa-download"></i></a>',
                filterable: false, sortable: false
            },
            {
                field: 'Name', title: 'Tên thiết lập',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'CreateDate', title: 'Ngày tạo', width: '150px', template: '#=Common.Date.FromJsonDMYHM(CreateDate)#',
                filterable: { cell: { template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); }, operator: 'gte', showOperators: false } },
            },
            {
                field: 'FileName', title: 'Tên File', width: '150px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            { title: '', filterable: false, sortable: false }
        ]
    }


    $scope.settingReport_GridChoose_Change = function ($event, grid, hasChoose) {
        $scope.SettingHasChoose = hasChoose;
    }
    $scope.SettingReport = function ($event, win) {
        $event.preventDefault();
        $rootScope.IsLoading = true;
        Common.Services.Call($http, {
            url: Common.Services.url.REP,
            method: _REPDIPL_SpotRate.URL.SettingList,
            data: { functionID: $rootScope.FunctionItem.ID },
            success: function (res) {
                $rootScope.IsLoading = false;
                if (Common.HasValue(res)) {
                    angular.forEach(res, function (value, key) {
                        value.IsChoose = false;
                    });
                }
                $scope.settingReport_GridOptions.dataSource.data(res);
                win.center().open();
                $scope.settingReport_Grid.resize();
            }
        });
    }


    $scope.SettingReport_TabIndex = 0;
    $scope.SettingReport_TabOptions = {
        animation: {
            open: { effects: "fadeIn" }
        },
        select: function (e) {
            $timeout(function () {
                $scope.SettingReport_TabIndex = angular.element(e.item).data('tabindex');
                Common.Log("Select_Tab_" + $scope.TabIndex);
            }, 1)
        }
    }
    $scope.SettingReport_AddClick = function ($event, win, vform) {
        $event.preventDefault();
        $scope.LoadSettingItem(win, null, vform);
    }

    $scope.settingReport_GridEdit_Click = function ($event, win, data, vform) {
        $event.preventDefault();
        $scope.LoadSettingItem(win, data, vform);
    }

    $scope.LoadSettingItem = function (win, data, vform) {
        if (data != null) {
            $scope.SettingItem = data;
        } else {
            $scope.SettingItem = { ID: 0 };
            $scope.SettingItem.TypeExport = 1;
            $scope.SettingItem.TypeOfDate = 0;
            $scope.SettingItem.TypeDateRange = 1;
            $scope.SettingReport_Tab.select(0);
        }
        vform({ clear: true });
        win.center().open();
    }

    $scope.SettingReport_AddFileClick = function ($event) {
        $event.preventDefault();
        var functionID = $rootScope.FunctionItem.ID;
        $rootScope.UploadFile({
            IsImage: false, ID: functionID, AllowChange: true, ShowChoose: true,
            Type: Common.CATTypeOfFileCode.TEMPLATEREPORT,
            Complete: function (file) {
                if (file != null) {
                    $scope.SettingItem.FileID = file.ID;
                    $scope.SettingItem.FileName = file.FileName;
                    $scope.SettingItem.FilePath = file.FilePath;
                }
            }
        });
    };

    $scope.SettingReport_SaveClick = function ($event, win, vform) {
        $event.preventDefault();
        if (vform()) {
            var error = false;
            if (!Common.HasValue($scope.SettingItem.FileID)) {
                $rootScope.Message({
                    Type: Common.Message.Type.Alert,
                    NotifyType: Common.Message.NotifyType.ERROR,
                    Title: 'Thông báo',
                    Msg: 'Chưa chọn file template',
                    Close: null,
                    Ok: null
                })
                error = true;
            }
            if (!error) {
                $rootScope.IsLoading = true;
                $scope.SettingItem.ReferID = $rootScope.FunctionItem.ID;
                Common.Services.Call($http, {
                    url: Common.Services.url.REP,
                    method: _REPDIPL_SpotRate.URL.SettingSave,
                    data: { item: $scope.SettingItem },
                    success: function (res) {
                        $rootScope.Message({
                            Type: Common.Message.Type.Notify,
                            NotifyType: Common.Message.NotifyType.SUCCESS,
                            Title: 'Thông báo',
                            Msg: 'Thành công',
                            Ok: null,
                            close: null,
                        })

                        Common.Services.Call($http, {
                            url: Common.Services.url.REP,
                            method: _REPDIPL_SpotRate.URL.SettingList,
                            data: { functionID: $rootScope.FunctionItem.ID },
                            success: function (res) {
                                $rootScope.IsLoading = false;
                                $rootScope.IsLoading = false;
                                if (Common.HasValue(res)) {
                                    angular.forEach(res, function (value, key) {
                                        value.IsChoose = false;
                                    });
                                }
                                $scope.settingReport_GridOptions.dataSource.data(res);
                            }
                        });
                        win.close();
                    }
                });
            }
        }
    }

    $scope.SettingReport_Destroy_Click = function ($event, win) {
        $event.preventDefault();
        if (Common.HasValue($scope.SettingItem)) {
            var datasend = [];
            datasend.push($scope.SettingItem.ID);
            $scope.SettingDelete(win, datasend);
        }
    }
    $scope.settingReport_GridDestroy_Click = function ($event, grid) {
        $event.preventDefault();
        $event.preventDefault();
        var data = grid.dataSource.data();
        var datasend = [];
        Common.Data.Each(data, function (o) {
            if (o.IsChoose) datasend.push(o.ID);
        })
        if (datasend.length > 0) {
            $scope.SettingDelete(null, datasend);
        }
    }

    $scope.SettingDelete = function (win, datasend) {
        $rootScope.Message({
            Type: Common.Message.Type.Confirm,
            NotifyType: Common.Message.NotifyType.SUCCESS,
            Title: 'Thông báo',
            Msg: 'Bạn muốn xóa dữ liệu đã chọn?',
            Close: null,
            Ok: function () {
                $rootScope.IsLoading = true;
                Common.Services.Call($http, {
                    url: Common.Services.url.REP,
                    method: _REPDIPL_SpotRate.URL.SettingDelete,
                    data: { lst: datasend },
                    success: function (res) {
                        $rootScope.Message({
                            Type: Common.Message.Type.Notify,
                            NotifyType: Common.Message.NotifyType.SUCCESS,
                            Title: 'Thông báo',
                            Msg: 'Thành công',
                            Ok: null,
                            close: null,
                        })

                        Common.Services.Call($http, {
                            url: Common.Services.url.REP,
                            method: _REPDIPL_SpotRate.URL.SettingList,
                            data: { functionID: $rootScope.FunctionItem.ID },
                            success: function (res) {
                                $rootScope.IsLoading = false;
                                $rootScope.IsLoading = false;
                                if (Common.HasValue(res)) {
                                    angular.forEach(res, function (value, key) {
                                        value.IsChoose = false;
                                    });
                                }
                                $scope.settingReport_GridOptions.dataSource.data(res);
                            }
                        });
                        if (Common.HasValue(win))
                            win.close();
                    }
                });
            }
        })
    }
    $scope.cboTypeExport_options = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: "contains",
        suggest: true, dataTextField: 'ValueName', dataValueField: 'ID',
        dataSource: [
            { ID: 1, ValueName: 'Lợi nhuận chuyến chi tiết' },
            { ID: 2, ValueName: 'Lợi nhuận chuyến theo cột' },
            { ID: 3, ValueName: 'Lợi nhuận chuyến theo cột nhóm kho' },
            { ID: 4, ValueName: 'Lợi nhuận chuyến theo cột nhóm hàng' },
            { ID: 5, ValueName: 'Lợi nhuận chuyến theo cột MOQ, phụ thu' },
            { ID: 6, ValueName: 'Lợi nhuận đơn hàng chi tiết' },
            { ID: 7, ValueName: 'Lợi nhuận đơn hàng theo cột' },
            { ID: 8, ValueName: 'Lợi nhuận đơn hàng theo cột nhóm kho' },
            { ID: 9, ValueName: 'Lợi nhuận đơn hàng theo cột nhóm hàng' },
            { ID: 10, ValueName: 'Lợi nhuận đơn hàng theo cột MOQ, phụ thu' },
            { ID: 11, ValueName: 'Lợi nhuận không hợp đồng' },
        ],
        change: function (e) { }
    }


    $scope.cboTypeDateRange_options = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: "contains",
        suggest: true, dataTextField: 'ValueName', dataValueField: 'ID',
        dataSource: [
           { ID: 1, ValueName: 'Theo tuần' },
           { ID: 2, ValueName: 'Theo tháng' },
           { ID: 3, ValueName: 'Theo ngày đã chọn' },
        ],
        change: function (e) {
        }
    }
    $scope.cboTypeOfDate_options = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: "contains",
        suggest: true, dataTextField: 'ValueName', dataValueField: 'ID',
        dataSource: [
           { ID: 0, ValueName: 'Theo ngày tính giá thu' },
            { ID: 1, ValueName: 'Theo ngày tính giá chi' },
            { ID: 2, ValueName: 'Theo ngày gửi yêu cầu' },
            { ID: 3, ValueName: 'Theo ngày ATD chuyến' },
            { ID: 4, ValueName: 'Theo ngày ATA chuyến' },
            { ID: 5, ValueName: 'Theo ngày ETD chuyến' },
            { ID: 6, ValueName: 'Theo ngày ETA chuyến' },
            { ID: 7, ValueName: 'Theo ngày ETD đơn hàng' },
            { ID: 8, ValueName: 'Theo ngày ETA đơn hàng' },
        ],
        change: function (e) {
        }
    }

    $scope.SettingReport_ActionClick = function ($event, data) {
        $event.preventDefault();

        var request = Common.Request.CreateFromGrid($scope.REPDIPL_gridOptions.dataSource);
        $rootScope.IsLoading = true;
        Common.Services.Call($http, {
            url: Common.Services.url.REP,
            method: _REPDIPL_Index.URL.SettingAction,
            data: { item: data, dtfrom: $scope.Item.DateFrom, dtto: $scope.Item.DateTo, request: request },
            success: function (res) {
                $rootScope.IsLoading = false;
                $rootScope.DownloadFile(res);
            }
        });
    }

    //#region Common
    $scope.SettingReport_CustomerClick = function ($event, win) {
        $event.preventDefault();

        var data = $scope.SettingItem;
        $scope.CustomerHasChoose = false;
        win.center().open();
        if (Common.HasValue(data.ListCustomer)) {
            angular.forEach(data.ListCustomer, function (value, key) {
                value.IsChoose = false;
            });
            $scope.SettingReport_Customer_Grid.dataSource.data(data.ListCustomer);
        } else {
            $scope.SettingReport_Customer_Grid.dataSource.data([]);
        }
    }

    $scope.SettingReport_GOPClick = function ($event, win) {
        $event.preventDefault();

        var data = $scope.SettingItem;
        $scope.GOPHasChoose = false;
        win.center().open();
        if (Common.HasValue(data.ListGroupProduct)) {
            angular.forEach(data.ListGroupProduct, function (value, key) {
                value.IsChoose = false;
            });
            $scope.SettingReport_GroupProduct_Grid.dataSource.data(data.ListGroupProduct);
        } else {
            $scope.SettingReport_GroupProduct_Grid.dataSource.data([]);
        }
    }

    $scope.SettingReport_StockClick = function ($event, win) {
        $event.preventDefault();

        var data = $scope.SettingItem;
        $scope.StockHasChoose = false;
        win.center().open();
        if (Common.HasValue(data.ListStock)) {
            angular.forEach(data.ListStock, function (value, key) {
                value.IsChoose = false;
            });
            $scope.SettingReport_Stock_Grid.dataSource.data(data.ListStock);
        } else {
            $scope.SettingReport_Stock_Grid.dataSource.data([]);
        }
    }

    $scope.SettingReport_PartnerClick = function ($event, win) {
        $event.preventDefault();

        var data = $scope.SettingItem;
        $scope.PartnerHasChoose = false;
        win.center().open();
        if (Common.HasValue(data.ListGroupOfPartner)) {
            angular.forEach(data.ListGroupOfPartner, function (value, key) {
                value.IsChoose = false;
            });
            $scope.SettingReport_Partner_Grid.dataSource.data(data.ListGroupOfPartner);
        } else {
            $scope.SettingReport_Partner_Grid.dataSource.data([]);
        }
    }

    $scope.SettingReport_LocationClick = function ($event, win) {
        $event.preventDefault();

        var data = $scope.SettingItem;
        $scope.LocationHasChoose = false;
        win.center().open();
        if (Common.HasValue(data.ListGroupOfLocation)) {
            angular.forEach(data.ListGroupOfLocation, function (value, key) {
                value.IsChoose = false;
            });
            $scope.SettingReport_Location_Grid.dataSource.data(data.ListGroupOfLocation);
        } else {
            $scope.SettingReport_Location_Grid.dataSource.data([]);
        }
    }

    $scope.SettingReport_ProvinceClick = function ($event, win) {
        $event.preventDefault();

        var data = $scope.SettingItem;
        $scope.ProvinceHasChoose = false;
        win.center().open();
        if (Common.HasValue(data.ListProvince)) {
            angular.forEach(data.ListProvince, function (value, key) {
                value.IsChoose = false;
            });
            $scope.SettingReport_Province_Grid.dataSource.data(data.ListProvince);
        } else {
            $scope.SettingReport_Province_Grid.dataSource.data([]);
        }
    }

    $scope.SettingReport_ServiceOfOrderClick = function ($event, win) {
        $event.preventDefault();

        var data = $scope.SettingItem;
        $scope.ServiceOfOrderHasChoose = false;
        win.center().open();
        if (Common.HasValue(data.ListServiceOfOrder)) {
            angular.forEach(data.ListServiceOfOrder, function (value, key) {
                value.IsChoose = false;
            });
            $scope.SettingReport_ServiceOfOrder_Grid.dataSource.data(data.ListServiceOfOrder);
        } else {
            $scope.SettingReport_ServiceOfOrder_Grid.dataSource.data([]);
        }
    }
    //#endregion

    //#region Customer
    $scope.CustomerHasChoose = false;
    $scope.SettingReport_Customer_GridOptions = {
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields: {
                    CustomerID: { type: 'number', editable: false },
                    IsChoose: { type: 'boolean' },
                }
            },
            pageSize: 20
        }),
        height: '100%', pageable: true, sortable: { mode: 'multiple', allowUnsort: true }, columnMenu: false, resizable: true, reorderable: false, filterable: { mode: 'row' }, selectable: 'row',
        columns: [
            {
                title: ' ', width: '35px',
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,SettingReport_Customer_Grid,customer_GridChoose_Change)" />',
                headerAttributes: { style: 'text-align: center;' }, attributes: { style: 'text-align: center;' },
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,SettingReport_Customer_Grid,customer_GridChoose_Change)" />',
                filterable: false, sortable: false
            },
            {
                field: 'CustomerCode', title: 'Mã khách hàng', width: '150px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'CustomerName', title: 'Tên khách hàng', width: '150px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            { title: '', filterable: false, sortable: false }
        ]
    }

    $scope.customer_GridChoose_Change = function ($event, grid, hasChoose) {
        $scope.CustomerHasChoose = hasChoose;
    }

    $scope.customer_AddNew = function ($event, win) {
        $event.preventDefault();
        win.center().open();
        $scope.customerNotIn_GridOptions.dataSource.read();
    }

    $scope.customerNotIn_GridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.REP,
            method: _REPDIPL_SpotRate.URL.SettingCusNotInList,
            readparam: function () { return { lstCus: $scope.SettingItem.ListCustomer } },
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number', editable: false, nullable: true },
                    IsChoose: { type: 'boolean' },
                }
            }
        }),
        height: '100%', pageable: true, sortable: true, columnMenu: false, filterable: { mode: 'row' }, resizable: true, editable: false,
        columns: [
            {
                title: ' ', width: '35px',
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,customerNotIn_Grid,customerNotIn_GridChoose_Change)" />',
                headerAttributes: { style: 'text-align: center;' }, attributes: { style: 'text-align: center;' },
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,customerNotIn_Grid,customerNotIn_GridChoose_Change)" />',
                filterable: false, sortable: false
            },
            { field: 'Code', title: "Mã khách hàng", width: 150, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'CustomerName', title: "Tên khách hàng", width: 150, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'Address', title: "Địa chỉ", width: 150, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { title: ' ', filterable: false, sortable: false }
        ]
    }

    $scope.customerNotIn_GridChoose_Change = function ($event, grid, hasChoose) {
        $scope.CustomerNotInHasChoose = hasChoose;
    }

    $scope.customerNotIn_Save_Click = function ($event, win, grid) {
        $event.preventDefault();
        var data = grid.dataSource.data();
        var datasend = [];
        Common.Data.Each(data, function (o) {
            if (o.IsChoose) datasend.push(o.ID);
        })
        if (datasend.length > 0) {
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.REP,
                method: _REPDIPL_SpotRate.URL.SettingCusNotInSave,
                data: { item: $scope.SettingItem, lst: datasend },
                success: function (res) {
                    $scope.SettingItem = res;
                    if (Common.HasValue(res.ListCustomer)) {
                        angular.forEach(res.ListCustomer, function (value, key) {
                            value.IsChoose = false;
                        });
                        $scope.SettingReport_Customer_Grid.dataSource.data(res.ListCustomer);
                    } else {
                        $scope.SettingReport_Customer_Grid.dataSource.data([]);
                    }
                    win.close();
                    $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });

                    Common.Services.Call($http, {
                        url: Common.Services.url.REP,
                        method: _REPDIPL_SpotRate.URL.SettingList,
                        data: { functionID: $rootScope.FunctionItem.ID },
                        success: function (res) {
                            $rootScope.IsLoading = false;
                            $rootScope.IsLoading = false;
                            if (Common.HasValue(res)) {
                                angular.forEach(res, function (value, key) {
                                    value.IsChoose = false;
                                });
                            }
                            $scope.settingReport_GridOptions.dataSource.data(res);
                        }
                    });
                },
                error: function (res) {
                    $rootScope.IsLoading = false;
                }
            });

        }
    }

    $scope.customer_Delete = function ($event, grid) {
        $event.preventDefault();
        var data = grid.dataSource.data();
        var datasend = [];
        Common.Data.Each(data, function (o) {
            if (o.IsChoose) datasend.push(o.CustomerID);
        })
        if (datasend.length > 0) {
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.REP,
                method: _REPDIPL_SpotRate.URL.SettingCusDeleteList,
                data: { item: $scope.SettingItem, lst: datasend },
                success: function (res) {
                    $scope.SettingItem = res;
                    if (Common.HasValue(res.ListCustomer)) {
                        angular.forEach(res.ListCustomer, function (value, key) {
                            value.IsChoose = false;
                        });
                        $scope.SettingReport_Customer_Grid.dataSource.data(res.ListCustomer);
                    } else {
                        $scope.SettingReport_Customer_Grid.dataSource.data([]);
                    }
                    if (Common.HasValue(res.ListGroupProduct)) {
                        angular.forEach(res.ListGroupProduct, function (value, key) {
                            value.IsChoose = false;
                        });
                        $scope.SettingReport_GroupProduct_Grid.dataSource.data(res.ListGroupProduct);
                    } else {
                        $scope.SettingReport_GroupProduct_Grid.dataSource.data([]);
                    }
                    $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });

                    Common.Services.Call($http, {
                        url: Common.Services.url.REP,
                        method: _REPDIPL_SpotRate.URL.SettingList,
                        data: { functionID: $rootScope.FunctionItem.ID },
                        success: function (res) {
                            $rootScope.IsLoading = false;
                            $rootScope.IsLoading = false;
                            if (Common.HasValue(res)) {
                                angular.forEach(res, function (value, key) {
                                    value.IsChoose = false;
                                });
                            }
                            $scope.settingReport_GridOptions.dataSource.data(res);
                        }
                    });
                },
                error: function (res) {
                    $rootScope.IsLoading = false;
                }
            });

        }
    }

    //#endregion

    //#region GOP
    $scope.GOPHasChoose = false;
    $scope.SettingReport_GroupProduct_GridOptions = {
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields: {
                    CustomerID: { type: 'number', editable: false },
                    IsChoose: { type: 'boolean' },
                }
            },
            pageSize: 20
        }),
        height: '100%', pageable: true, sortable: { mode: 'multiple', allowUnsort: true }, columnMenu: false, resizable: true, reorderable: false, filterable: { mode: 'row' }, selectable: 'row',
        columns: [
            {
                title: ' ', width: '35px',
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,SettingReport_GroupProduct_Grid,gop_GridChoose_Change)" />',
                headerAttributes: { style: 'text-align: center;' }, attributes: { style: 'text-align: center;' },
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,SettingReport_GroupProduct_Grid,gop_GridChoose_Change)" />',
                filterable: false, sortable: false
            },
            {
                field: 'GroupProductCode', title: 'Mã nhóm sản phẩm', width: '150px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'GroupProductName', title: 'Tên nhóm sản phẩm', width: '150px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            { title: '', filterable: false, sortable: false }
        ]
    }
    $scope.gop_GridChoose_Change = function ($event, grid, hasChoose) {
        $scope.GOPHasChoose = hasChoose;
    }

    $scope.gop_AddNew = function ($event, win) {
        $event.preventDefault();
        if (!Common.HasValue($scope.SettingItem.ListCustomer) || $scope.SettingItem.ListCustomer.length == 0) {
            $rootScope.Message({
                Type: Common.Message.Type.Alert,
                NotifyType: Common.Message.NotifyType.ERROR,
                Title: 'Thông báo',
                Msg: 'Vui lòng chọn khách hàng',
                Close: null,
                Ok: null
            })
        } else {
            win.center().open();
            $scope.gopNotIn_GridOptions.dataSource.read();
        }
    }

    $scope.gopNotIn_GridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.REP,
            method: _REPDIPL_SpotRate.URL.SettingGOPNotInList,
            readparam: function () { return { lstCus: $scope.SettingItem.ListCustomer, lstGOP: $scope.SettingItem.ListGroupProduct } },
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number', editable: false, nullable: true },
                    IsChoose: { type: 'boolean' },
                }
            }
        }),
        height: '100%', pageable: true, sortable: true, columnMenu: false, filterable: { mode: 'row' }, resizable: true, editable: false,
        columns: [
            {
                title: ' ', width: '35px',
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,gopNotIn_Grid,gopNotIn_GridChoose_Change)" />',
                headerAttributes: { style: 'text-align: center;' }, attributes: { style: 'text-align: center;' },
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,gopNotIn_Grid,gopNotIn_GridChoose_Change)" />',
                filterable: false, sortable: false
            },
            { field: 'Code', title: "Mã nhóm sản phẩm", width: 150, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'GroupName', title: "Tên nhóm sản phẩm", width: 150, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { title: ' ', filterable: false, sortable: false }
        ]
    }

    $scope.gopNotIn_GridChoose_Change = function ($event, grid, hasChoose) {
        $scope.CustomerNotInHasChoose = hasChoose;
    }

    $scope.gopNotIn_Save_Click = function ($event, win, grid) {
        $event.preventDefault();
        var data = grid.dataSource.data();
        var datasend = [];
        Common.Data.Each(data, function (o) {
            if (o.IsChoose) datasend.push(o.ID);
        })
        if (datasend.length > 0) {
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.REP,
                method: _REPDIPL_SpotRate.URL.SettingGOPNotInSave,
                data: { item: $scope.SettingItem, lst: datasend },
                success: function (res) {
                    $scope.SettingItem = res;
                    if (Common.HasValue(res.ListGroupProduct)) {
                        angular.forEach(res.ListGroupProduct, function (value, key) {
                            value.IsChoose = false;
                        });
                        $scope.SettingReport_GroupProduct_GridOptions.dataSource.data(res.ListGroupProduct);
                    }
                    win.close();
                    $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });

                    Common.Services.Call($http, {
                        url: Common.Services.url.REP,
                        method: _REPDIPL_SpotRate.URL.SettingList,
                        data: { functionID: $rootScope.FunctionItem.ID },
                        success: function (res) {
                            $rootScope.IsLoading = false;
                            $rootScope.IsLoading = false;
                            if (Common.HasValue(res)) {
                                angular.forEach(res, function (value, key) {
                                    value.IsChoose = false;
                                });
                            }
                            $scope.settingReport_GridOptions.dataSource.data(res);
                        }
                    });
                },
                error: function (res) {
                    $rootScope.IsLoading = false;
                }
            });

        }
    }

    $scope.gop_Delete = function ($event, grid) {
        $event.preventDefault();
        var data = grid.dataSource.data();
        var datasend = [];
        Common.Data.Each(data, function (o) {
            if (o.IsChoose) datasend.push(o.GroupProductID);
        })
        if (datasend.length > 0) {
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.REP,
                method: _REPDIPL_SpotRate.URL.SettingGOPDeleteList,
                data: { item: $scope.SettingItem, lst: datasend },
                success: function (res) {
                    $scope.SettingItem = res;
                    if (Common.HasValue(res.ListGroupProduct)) {
                        angular.forEach(res.ListGroupProduct, function (value, key) {
                            value.IsChoose = false;
                        });
                        $scope.SettingReport_GroupProduct_GridOptions.dataSource.data(res.ListGroupProduct);
                    }
                    $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });

                    Common.Services.Call($http, {
                        url: Common.Services.url.REP,
                        method: _REPDIPL_SpotRate.URL.SettingList,
                        data: { functionID: $rootScope.FunctionItem.ID },
                        success: function (res) {
                            $rootScope.IsLoading = false;
                            $rootScope.IsLoading = false;
                            if (Common.HasValue(res)) {
                                angular.forEach(res, function (value, key) {
                                    value.IsChoose = false;
                                });
                            }
                            $scope.settingReport_GridOptions.dataSource.data(res);
                        }
                    });
                },
                error: function (res) {
                    $rootScope.IsLoading = false;
                }
            });

        }
    }
    //#endregion
    $scope.window_Close_Click = function ($event, win) {
        $event.preventDefault();
        win.close();
    }
    //#endregion

    $scope.Excel_Export = function ($event) {
        $event.preventDefault();

        $rootScope.IsLoading = true;
        Common.Services.Call($http, {
            url: Common.Services.url.REP,
            method: _REPDIPL_SpotRate.URL.Spotrate_Export,
            data: {},
            success: function (res) {
                $rootScope.IsLoading = false;
                $rootScope.DownloadFile(res);
            }
        })
    }
}]);