
/// <reference path="~/Scripts/jquery-1.9.1.intellisense.js" />
/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />

var _REPDIPL_Order = {
    URL: {
        Search: 'REPDIPL_Order',
        Template: 'REPDIPL_OrderTemplate',

        Read_Customer: 'REP_Customer_Read',
        Read_GroupOfProduct: 'REP_GroupOfProduct_Read',
        Read_Stock: 'REPDIPL_StockList',

        Order_Export: 'REPDIPL_Order_Export',

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

        SettingStockDeleteList: 'CUSSettingReport_StockDeleteList',
        SettingStockNotInList: 'CUSSettingReport_StockNotInList',
        SettingStockNotInSave: 'CUSSettingReport_StockNotInSave',

        SettingProvinceInList: 'CUSSettingReport_ProvinceNotInList',
        SettingProvinceDeleteList: 'CUSSettingReport_ProvinceDeleteList',
        SettingProvinceNotInSave: 'CUSSettingReport_ProvinceNotInSave',

        SettingLocationNotInList: 'CUSSettingReport_GroupOfLocationNotInList',
        SettingLocationDeleteList: 'CUSSettingReport_GroupOfLocationDeleteList',
        SettingLocationNotInSave: 'CUSSettingReport_GroupOfLocationNotInSave',

        SettingPartnerInList: 'CUSSettingReport_GroupOfPartnerNotInList',
        SettingPartnerDeleteList: 'CUSSettingReport_GroupOfPartnerDeleteList',
        SettingPartnerNotInSave: 'CUSSettingReport_GroupOfPartnerNotInSave',

        SettingServiceOfOrderInList: 'CUSSettingReport_ServiceOfOrderNotInList',
        SettingServiceOfOrderNotInSave: 'CUSSettingReport_ServiceOfOrderNotInSave',
        SettingServiceOfOrderDeleteList: 'CUSSettingReport_ServiceOfOrderDeleteList',

        OrderRoutingNotInList: 'CUSSettingReport_OrderRoutingNotInList',
        OrderRoutingNotInSave: 'CUSSettingReport_OrderRoutingNotInSave',
        OrderRoutingDeleteList: 'CUSSettingReport_OrderRoutingDeleteList',

        OPSRoutingNotInList: 'CUSSettingReport_OPSRoutingNotInList',
        OPSRoutingNotInSave: 'CUSSettingReport_OPSRoutingNotInSave',
        OPSRoutingDeleteList: 'CUSSettingReport_OPSRoutingDeleteList',

        PartnerNotInList: 'CUSSettingReport_PartnerNotInList',
        PartnerNotInSave: 'CUSSettingReport_PartnerNotInSave',
        PartnerDeleteList: 'CUSSettingReport_PartnerDeleteList',

        SettingAction: 'REPDIPL_SettingDownload',
    },
}

angular.module('myapp').controller('REPDIPL_OrderCtrl', ['$rootScope', '$scope', '$http', '$location', '$state', '$timeout', '$stateParams', function ($rootScope, $scope, $http, $location, $state, $timeout, $stateParams) {
    if ($rootScope.IsPageComplete != true) return;
    Common.Log('REPDIPL_OrderCtrl');
    $rootScope.IsLoading = false;

    $scope.Auth = $rootScope.GetAuth();
    $scope.SettingItem = { ID: 0 };
    $scope.Item = {
        lstCustomerID: [],
        lstGroupID: [],
        lstStockID: [],
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
                method: _REPDIPL_Order.URL.Read_GroupOfProduct,
                data: { lstid: lstid },
                success: function (res) {
                    $scope.mulGroupOfProduct_Options.dataSource.data(res.Data);
                }
            });
            Common.Services.Call($http, {
                url: Common.Services.url.REP,
                method: _REPDIPL_Order.URL.Read_Stock,
                data: { lstid: lstid },
                success: function (res) {
                    $scope.mulStock_Options.dataSource.data(res);
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
    $scope.mulStock_Options = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains',
        suggest: true, dataTextField: 'LocationName', dataValueField: 'ID',
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields: {
                    LocationName: { type: 'string' },
                    ID: { type: 'number' },
                }
            }
        })
    };

    Common.Services.Call($http, {
        url: Common.Services.url.REP,
        method: _REPDIPL_Order.URL.Read_Customer,
        data: {},
        success: function (res) {
            $scope.mulCustomer_Options.dataSource.data(res.Data);
        }
    });

    $scope.REPDIPL_Order_gridOptions = {
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number', editable: false },
                    RequestDate: { type: 'date' },
                    DateConfig: { type: 'date' },
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
                    ETD: { type: 'date' },
                    ETA: { type: 'date' },
                },
                pageSize: 20
            },
            pageSize: 20,
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
                field: 'DateConfig', title: '<b>Ngày tính giá</b><br>[DateConfig]', width: '120px', template: '#=Common.Date.FromJsonDMYHM(DateConfig)#',
                filterable: { cell: { template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); }, operator: 'equal', showOperators: false } },
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
                field: 'RequestDate', title: '<b>Ngày y/c v/c</b><br>[RequestDate]', width: '120px', template: '#=Common.Date.FromJsonDMYHM(RequestDate)#',
                filterable: { cell: { template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); }, operator: 'equal', showOperators: false } },
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
                field: 'LocationToCode', title: '<b>Mã địa điểm</b><br>[LocationToCode]', width: '120px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'LocationToName', title: '<b>Tên địa điểm</b><br>[LocationToName]', width: '120px',
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
                field: 'AreaToCodeCredit', title: '<b>Mã khu vực đến</b><br>[AreaToCodeCredit]', width: '120px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'AreaToNameCredit', title: '<b>Tên khu vực đến</b><br>[AreaToNameCredit]', width: '120px',
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
                field: 'GroupOfProductCode', title: '<b>Mã nhóm hàng</b><br>[GroupOfProductCode]', width: '120px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'GroupOfProductName', title: '<b>Nhóm hàng</b><br>[GroupOfProductName]', width: '120px',
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
                field: 'IncomeUnitPrice', title: '<b>Giá thu</b><br>[IncomeUnitPrice]', width: '120px', template: '#=IncomeUnitPrice==null?" ":Common.Number.ToMoney(IncomeUnitPrice)#',
                filterable: { cell: { operator: 'gte', showOperators: false } }
            },
            {
                field: 'IncomeQuantity', title: '<b>Số lượng thu</b><br>[IncomeQuantity]', width: '120px', template: '#=IncomeQuantity==null?" ":Common.Number.ToNumber3(IncomeQuantity)#',
                filterable: { cell: { operator: 'gte', showOperators: false } }
            },
            {
                field: 'Income', title: '<b>Cước thu</b><br>[Income]', width: '120px', template: '#=Income==null?" ":Common.Number.ToMoney(Income)#',
                filterable: { cell: { operator: 'gte', showOperators: false } }
            },
            {
                field: 'IncomeLoadUnitPrice', title: '<b>Giá b.x lên thu</b><br>[IncomeLoadUnitPrice]', width: '120px', template: '#=IncomeLoadUnitPrice==null?" ":Common.Number.ToMoney(IncomeLoadUnitPrice)#',
                filterable: { cell: { operator: 'gte', showOperators: false } }
            },
            {
                field: 'IncomeLoadQuantity', title: '<b>Số lượng b.x lên thu</b><br>[IncomeLoadQuantity]', width: '120px', template: '#=IncomeLoadQuantity==null?" ":Common.Number.ToNumber3(IncomeLoadQuantity)#',
                filterable: { cell: { operator: 'gte', showOperators: false } }
            },
            {
                field: 'IncomeLoad', title: '<b>Cước b.x lên thu</b><br>[IncomeLoad]', width: '120px', template: '#=IncomeLoad==null?" ":Common.Number.ToMoney(IncomeLoad)#',
                filterable: { cell: { operator: 'gte', showOperators: false } }
            },
            {
                field: 'IncomeUnLoadUnitPrice', title: '<b>Giá b.x xuống thu</b><br>[IncomeUnLoadUnitPrice]', width: '120px', template: '#=IncomeUnLoadUnitPrice==null?" ":Common.Number.ToMoney(IncomeUnLoadUnitPrice)#',
                filterable: { cell: { operator: 'gte', showOperators: false } }
            },
            {
                field: 'IncomeUnLoadQuantity', title: '<b>Số lượng b.x xuống thu</b><br>[IncomeUnLoadQuantity]', width: '120px', template: '#=IncomeUnLoadQuantity==null?" ":Common.Number.ToNumber3(IncomeUnLoadQuantity)#',
                filterable: { cell: { operator: 'gte', showOperators: false } }
            },
            {
                field: 'IncomeUnLoad', title: '<b>Cước b.x xuống thu</b><br>[IncomeUnLoad]', width: '120px', template: '#=IncomeUnLoad==null?" ":Common.Number.ToMoney(IncomeUnLoad)#',
                filterable: { cell: { operator: 'gte', showOperators: false } }
            },
            {
                field: 'IncomeReturnUnitPrice', title: '<b>Giá trả về thu</b><br>[IncomeReturnUnitPrice]', width: '120px', template: '#=IncomeReturnUnitPrice==null?" ":Common.Number.ToMoney(IncomeReturnUnitPrice)#',
                filterable: { cell: { operator: 'gte', showOperators: false } }
            },
            {
                field: 'IncomeReturnQuantity', title: '<b>Số lượng trả về thu</b><br>[IncomeReturnQuantity]', width: '120px', template: '#=IncomeReturnQuantity==null?" ":Common.Number.ToNumber3(IncomeReturnQuantity)#',
                filterable: { cell: { operator: 'gte', showOperators: false } }
            },
            {
                field: 'IncomeReturn', title: '<b>Cước trả về thu</b><br>[IncomeReturn]', width: '120px', template: '#=IncomeReturn==null?" ":Common.Number.ToMoney(IncomeReturn)#',
                filterable: { cell: { operator: 'gte', showOperators: false } }
            },
            {
                field: 'IncomeReturnUnLoadUnitPrice', title: '<b>Giá b.x xuống trả về thu</b><br>[IncomeReturnUnLoadUnitPrice]', width: '120px', template: '#=IncomeReturnUnLoadUnitPrice==null?" ":Common.Number.ToMoney(IncomeReturnUnLoadUnitPrice)#',
                filterable: { cell: { operator: 'gte', showOperators: false } }
            },
            {
                field: 'IncomeReturnUnLoadQuantity', title: '<b>Số lượng b.x xuống trả về thu</b><br>[IncomeReturnUnLoadQuantity]', width: '120px', template: '#=IncomeReturnUnLoadQuantity==null?" ":Common.Number.ToNumber3(IncomeReturnUnLoadQuantity)#',
                filterable: { cell: { operator: 'gte', showOperators: false } }
            },
            {
                field: 'IncomeReturnUnLoad', title: '<b>Cước b.x xuống trả về thu</b><br>[IncomeReturnUnLoad]', width: '120px', template: '#=IncomeReturnUnLoad==null?" ":Common.Number.ToMoney(IncomeReturnUnLoad)#',
                filterable: { cell: { operator: 'gte', showOperators: false } }
            },
            {
                field: 'IncomeReturnLoadUnitPrice', title: '<b>Giá b.x lên trả về thu</b><br>[IncomeReturnLoadUnitPrice]', width: '120px', template: '#=IncomeReturnLoadUnitPrice==null?" ":Common.Number.ToMoney(IncomeReturnLoadUnitPrice)#',
                filterable: { cell: { operator: 'gte', showOperators: false } }
            },
            {
                field: 'IncomeReturnLoadQuantity', title: '<b>Số lượng b.x lên trả về thu</b><br>[IncomeReturnLoadQuantity]', width: '120px', template: '#=IncomeReturnLoadQuantity==null?" ":Common.Number.ToNumber3(IncomeReturnLoadQuantity)#',
                filterable: { cell: { operator: 'gte', showOperators: false } }
            },
            {
                field: 'IncomeReturnLoad', title: '<b>Cước b.x lên trả về thu</b><br>[IncomeReturnLoad]', width: '120px', template: '#=IncomeReturnLoad==null?" ":Common.Number.ToMoney(IncomeReturnLoad)#',
                filterable: { cell: { operator: 'gte', showOperators: false } }
            },
            {
                field: 'IncomeExUnitPrice', title: '<b>Giá phụ thu</b><br>[IncomeExUnitPrice]', width: '120px', template: '#=IncomeExUnitPrice==null?" ":Common.Number.ToMoney(IncomeExUnitPrice)#',
                filterable: { cell: { operator: 'gte', showOperators: false } }
            },
            {
                field: 'IncomeExQuantity', title: '<b>Số lượng phụ thu</b><br>[IncomeExQuantity]', width: '120px', template: '#=IncomeExQuantity==null?" ":Common.Number.ToNumber3(IncomeExQuantity)#',
                filterable: { cell: { operator: 'gte', showOperators: false } }
            },
            {
                field: 'IncomeEx', title: '<b>Cước phụ thu</b><br>[IncomeEx]', width: '120px', template: '#=IncomeEx==null?" ":Common.Number.ToMoney(IncomeEx)#',
                filterable: { cell: { operator: 'gte', showOperators: false } }
            },
            {
                field: 'IncomeExCostName', title: '<b>Loại phụ thu</b><br>[IncomeExCostName]', width: '120px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'IncomeTrouble', title: '<b>Thu phát sinh</b><br>[IncomeTrouble]', width: '120px', template: '#=IncomeTrouble==null?" ":Common.Number.ToMoney(IncomeTrouble)#',
                filterable: { cell: { operator: 'gte', showOperators: false } }
            },
            {
                field: 'IncomeExNote', title: '<b>Ghi chú phụ thu</b><br>[IncomeExNote]', width: '120px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'IncomeManual', title: '<b>Thu nhập tay</b><br>[IncomeManual]', width: '120px', template: '#=IncomeManual==null?" ":Common.Number.ToMoney(IncomeManual)#',
                filterable: { cell: { operator: 'gte', showOperators: false } }
            },
            {
                field: 'IncomeManualUnitPrice', title: '<b>Giá thu nhập tay</b><br>[IncomeManualUnitPrice]', width: '120px', template: '#=IncomeManualUnitPrice==null?" ":Common.Number.ToMoney(IncomeManualUnitPrice)#',
                filterable: { cell: { operator: 'gte', showOperators: false } }
            },
            {
                field: 'IncomeManualQuantity', title: '<b>SL thu nhập tay</b><br>[IncomeManualQuantity]', width: '120px', template: '#=IncomeManualQuantity==null?" ":Common.Number.ToNumber3(IncomeManualQuantity)#',
                filterable: { cell: { operator: 'gte', showOperators: false } }
            },
            {
                field: 'IncomeManualNote', title: '<b>Ghi chú thu nhập tay</b><br>[IncomeManualNote]', width: '120px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'CostUnitPrice', title: '<b>Giá chi</b><br>[CostUnitPrice]', width: '120px', template: '#=CostUnitPrice==null?" ":Common.Number.ToMoney(CostUnitPrice)#',
                filterable: { cell: { operator: 'gte', showOperators: false } }
            },
            {
                field: 'CostQuantity', title: '<b>Số lượng chi</b><br>[CostQuantity]', width: '120px', template: '#=CostQuantity==null?" ":Common.Number.ToNumber3(CostQuantity)#',
                filterable: { cell: { operator: 'gte', showOperators: false } }
            },
            {
                field: 'Cost', title: '<b>Cước chi</b><br>[Cost]', width: '120px', template: '#=Cost==null?" ":Common.Number.ToMoney(Cost)#',
                filterable: { cell: { operator: 'gte', showOperators: false } }
            },
            {
                field: 'CostLoadUnitPrice', title: '<b>Giá b.x lên chi</b><br>[CostLoadUnitPrice]', width: '120px', template: '#=CostLoadUnitPrice==null?" ":Common.Number.ToMoney(CostLoadUnitPrice)#',
                filterable: { cell: { operator: 'gte', showOperators: false } }
            },
            {
                field: 'CostLoadQuantity', title: '<b>Số lượng b.x lên chi</b><br>[CostLoadQuantity]', width: '120px', template: '#=CostLoadQuantity==null?" ":Common.Number.ToNumber3(CostLoadQuantity)#',
                filterable: { cell: { operator: 'gte', showOperators: false } }
            },
            {
                field: 'CostLoad', title: '<b>Cước b.x lên chi</b><br>[CostLoad]', width: '120px', template: '#=CostLoad==null?" ":Common.Number.ToMoney(CostLoad)#',
                filterable: { cell: { operator: 'gte', showOperators: false } }
            },
            {
                field: 'CostLoadVendorCode', title: '<b>B.x lên chi mã nhà xe</b><br>[CostLoadVendorCode]', width: '120px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'CostLoadVendorName', title: '<b>B.x lên chi nhà xe</b><br>[CostLoadVendorName]', width: '120px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'CostLoadVendorCode', title: '<b>B.x lên chi mã nhà xe</b><br>[CostLoadVendorCode]', width: '120px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'CostLoadVendorName', title: '<b>B.x lên chi nhà xe</b><br>[CostLoadVendorName]', width: '120px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'CostUnLoadUnitPrice', title: '<b>Giá b.x xuống chi</b><br>[CostUnLoadUnitPrice]', width: '120px', template: '#=CostUnLoadUnitPrice==null?" ":Common.Number.ToMoney(CostUnLoadUnitPrice)#',
                filterable: { cell: { operator: 'gte', showOperators: false } }
            },
            {
                field: 'CostUnLoadQuantity', title: '<b>Số lượng b.x xuống chi</b><br>[CostUnLoadQuantity]', width: '120px', template: '#=CostUnLoadQuantity==null?" ":Common.Number.ToNumber3(CostUnLoadQuantity)#',
                filterable: { cell: { operator: 'gte', showOperators: false } }
            },
            {
                field: 'CostUnLoad', title: '<b>Cước b.x xuống chi</b><br>[CostUnLoad]', width: '120px', template: '#=CostUnLoad==null?" ":Common.Number.ToMoney(CostUnLoad)#',
                filterable: { cell: { operator: 'gte', showOperators: false } }
            },
            {
                field: 'CostUnLoadVendorCode', title: '<b>B.x xuống chi mã nhà xe</b><br>[CostUnLoadVendorCode]', width: '120px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'CostUnLoadVendorName', title: '<b>B.x xuống chi nhà xe</b><br>[CostUnLoadVendorName]', width: '120px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'CostUnLoadVendorCode', title: '<b>B.x xuống chi mã nhà xe</b><br>[CostUnLoadVendorCode]', width: '120px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'CostUnLoadVendorName', title: '<b>B.x xuống chi nhà xe</b><br>[CostUnLoadVendorName]', width: '120px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'CostReturnUnitPrice', title: '<b>Giá trả về chi</b><br>[CostReturnUnitPrice]', width: '120px', template: '#=CostReturnUnitPrice==null?" ":Common.Number.ToMoney(CostReturnUnitPrice)#',
                filterable: { cell: { operator: 'gte', showOperators: false } }
            },
            {
                field: 'CostReturnQuantity', title: '<b>Số lượng trả về chi</b><br>[CostReturnQuantity]', width: '120px', template: '#=CostReturnQuantity==null?" ":Common.Number.ToNumber3(CostReturnQuantity)#',
                filterable: { cell: { operator: 'gte', showOperators: false } }
            },
            {
                field: 'CostReturn', title: '<b>Cước trả về chi</b><br>[CostReturn]', width: '120px', template: '#=CostReturn==null?" ":Common.Number.ToMoney(CostReturn)#',
                filterable: { cell: { operator: 'gte', showOperators: false } }
            },
            {
                field: 'CostReturnUnLoadUnitPrice', title: '<b>Giá b.x xuống trả về chi</b><br>[CostReturnUnLoadUnitPrice]', width: '120px', template: '#=CostReturnUnLoadUnitPrice==null?" ":Common.Number.ToMoney(CostReturnUnLoadUnitPrice)#',
                filterable: { cell: { operator: 'gte', showOperators: false } }
            },
            {
                field: 'CostReturnUnLoadQuantity', title: '<b>Số lượng b.x xuống trả về chi</b><br>[CostReturnUnLoadQuantity]', width: '120px', template: '#=CostReturnUnLoadQuantity==null?" ":Common.Number.ToNumber3(CostReturnUnLoadQuantity)#',
                filterable: { cell: { operator: 'gte', showOperators: false } }
            },
            {
                field: 'CostReturnUnLoad', title: '<b>Cước b.x xuống trả về chi</b><br>[CostReturnUnLoad]', width: '120px', template: '#=CostReturnUnLoad==null?" ":Common.Number.ToMoney(CostReturnUnLoad)#',
                filterable: { cell: { operator: 'gte', showOperators: false } }
            },
            {
                field: 'CostReturnUnLoadVendorCode', title: '<b>B.x xuống trả về chi mã nhà xe</b><br>[CostReturnUnLoadVendorCode]', width: '120px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'CostReturnUnLoadVendorName', title: '<b>B.x xuống trả về chi nhà xe</b><br>[CostReturnUnLoadVendorName]', width: '120px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'CostReturnUnLoadVendorCode', title: '<b>B.x xuống trả về chi mã nhà xe</b><br>[CostReturnUnLoadVendorCode]', width: '120px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'CostReturnUnLoadVendorName', title: '<b>B.x xuống trả về chi nhà xe</b><br>[CostReturnUnLoadVendorName]', width: '120px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'CostReturnLoadUnitPrice', title: '<b>Giá b.x lên trả về chi</b><br>[CostReturnLoadUnitPrice]', width: '120px', template: '#=CostReturnLoadUnitPrice==null?" ":Common.Number.ToMoney(CostReturnLoadUnitPrice)#',
                filterable: { cell: { operator: 'gte', showOperators: false } }
            },
            {
                field: 'CostReturnLoadQuantity', title: '<b>Số lượng b.x lên trả về chi</b><br>[CostReturnLoadQuantity]', width: '120px', template: '#=CostReturnLoadQuantity==null?" ":Common.Number.ToNumber3(CostReturnLoadQuantity)#',
                filterable: { cell: { operator: 'gte', showOperators: false } }
            },
            {
                field: 'CostReturnLoad', title: '<b>Giá b.x lên trả về chi</b><br>[CostReturnLoad]', width: '120px', template: '#=CostReturnLoad==null?" ":Common.Number.ToMoney(CostReturnLoad)#',
                filterable: { cell: { operator: 'gte', showOperators: false } }
            },
            {
                field: 'CostReturnLoadVendorCode', title: '<b>B.x lên trả về chi mã nhà xe</b><br>[CostReturnLoadVendorCode]', width: '120px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'CostReturnLoadVendorName', title: '<b>B.x lên trả về chi nhà xe</b><br>[CostReturnLoadVendorName]', width: '120px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'CostReturnLoadVendorCode', title: '<b>B.x lên trả về chi mã nhà xe</b><br>[CostReturnLoadVendorCode]', width: '120px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'CostReturnLoadVendorName', title: '<b>B.x lên trả về chi nhà xe</b><br>[CostReturnLoadVendorName]', width: '120px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'CostExUnitPrice', title: '<b>Giá phụ chi</b><br>[CostExUnitPrice]', width: '120px', template: '#=CostExUnitPrice==null?" ":Common.Number.ToMoney(CostExUnitPrice)#',
                filterable: { cell: { operator: 'gte', showOperators: false } }
            },
            {
                field: 'CostExQuantity', title: '<b>Số lượng phụ chi</b><br>[CostExQuantity]', width: '120px', template: '#=CostExQuantity==null?" ":Common.Number.ToNumber3(CostExQuantity)#',
                filterable: { cell: { operator: 'gte', showOperators: false } }
            },
            {
                field: 'CostEx', title: '<b>Cước phụ chi</b><br>[CostEx]', width: '120px', template: '#=CostEx==null?" ":Common.Number.ToMoney(CostEx)#',
                filterable: { cell: { operator: 'gte', showOperators: false } }
            },
            {
                field: 'CostExCostName', title: '<b>Loại phụ thu chi</b><br>[CostExCostName]', width: '120px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'CostTrouble', title: '<b>Giá chi</b><br>[CostTrouble]', width: '120px', template: '#=CostTrouble==null?" ":Common.Number.ToMoney(CostTrouble)#',
                filterable: { cell: { operator: 'gte', showOperators: false } }
            },
            {
                field: 'CostExNote', title: '<b>Ghi chú phụ thu chi</b><br>[CostExNote]', width: '120px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'CostManual', title: '<b>Chi nhập tay</b><br>[CostManual]', width: '120px', template: '#=CostManual==null?" ":Common.Number.ToMoney(CostManual)#',
                filterable: { cell: { operator: 'gte', showOperators: false } }
            },
            {
                field: 'CostManualUnitPrice', title: '<b>Giá chi nhập tay</b><br>[CostManualUnitPrice]', width: '120px', template: '#=CostManualUnitPrice==null?" ":Common.Number.ToMoney(CostManualUnitPrice)#',
                filterable: { cell: { operator: 'gte', showOperators: false } }
            },
            {
                field: 'CostManualQuantity', title: '<b>SL chi nhập tay</b><br>[CostManualQuantity]', width: '120px', template: '#=CostManualQuantity==null?" ":Common.Number.ToNumber3(CostManualQuantity)#',
                filterable: { cell: { operator: 'gte', showOperators: false } }
            },
            {
                field: 'CostManualNote', title: '<b>Ghi chú chi nhập tay</b><br>[CostManualNote]', width: '120px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'CostDepreciation', title: '<b>Cước khấu hao</b><br>[CostDepreciation]', width: '120px', template: '#=CostDepreciation==null?" ":Common.Number.ToMoney(CostDepreciation)#',
                filterable: { cell: { operator: 'gte', showOperators: false } }
            },
            {
                field: 'CostSchedule', title: '<b>Cước cố định</b><br>[CostSchedule]', width: '120px', template: '#=CostSchedule==null?" ":Common.Number.ToMoney(CostSchedule)#',
                filterable: { cell: { operator: 'gte', showOperators: false } }
            },
            {
                field: 'CostDriver', title: '<b>Cước tài xế</b><br>[CostDriver]', width: '120px', template: '#=CostDriver==null?" ":Common.Number.ToMoney(CostDriver)#',
                filterable: { cell: { operator: 'gte', showOperators: false } }
            },
            {
                field: 'CostStation', title: '<b>Cước trạm</b><br>[CostStation]', width: '120px', template: '#=CostStation==null?" ":Common.Number.ToMoney(CostStation)#',
                filterable: { cell: { operator: 'gte', showOperators: false } }
            },
            {
                field: 'TonOrder', title: '<b>Tấn yêu cầu</b><br>[TonOrder]', width: '120px',
                filterable: { cell: { operator: 'gte', showOperators: false } }
            },
            {
                field: 'CBMOrder', title: '<b>Khối yêu cầu</b><br>[CBMOrder]', width: '120px',
                filterable: { cell: { operator: 'gte', showOperators: false } }
            },
            {
                field: 'QuantityOrder', title: '<b>SL yêu cầu</b><br>[QuantityOrder]', width: '120px',
                filterable: { cell: { operator: 'gte', showOperators: false } }
            },
            {
                field: 'KgOrder', title: '<b>Kg yêu cầu</b><br>[KgOrder]', width: '120px',
                filterable: { cell: { operator: 'gte', showOperators: false } }
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
                field: 'InvoiceNote', title: '<b>Số chứng từ</b><br>[InvoiceNote]', width: '120px',
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
                field: 'TransportModeIncome', title: '<b>Loại vận chuyển thu</b><br>[TransportModeIncome]', width: '120px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'TransportModeCost', title: '<b>Loại vận chuyển chi</b><br>[TransportModeCost]', width: '120px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'TotalIncome', title: '<b>Tổng thu</b><br>[TotalIncome]', width: '120px', template: '#=TotalIncome==null?" ":Common.Number.ToMoney(TotalIncome)#',
                filterable: { cell: { operator: 'gte', showOperators: false } }
            },
            {
                field: 'TotalCost', title: '<b>Tổng chi</b><br>[TotalCost]', width: '120px', template: '#=TotalCost==null?" ":Common.Number.ToMoney(TotalCost)#',
                filterable: { cell: { operator: 'gte', showOperators: false } }
            },
            {
                field: 'TotalPL', title: '<b>PL</b><br>[TotalPL]', width: '120px', template: '#=TotalPL==null?" ":Common.Number.ToMoney(TotalPL)#',
                filterable: { cell: { operator: 'gte', showOperators: false } }
            },
            {
                field: 'OrderCreatedBy', title: '<b>Người tạo đơn</b><br>[OrderCreatedBy]', width: '120px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'ETD', title: '<b>Ngày vận chuyển</b><br>[ETD]', width: '120px', template: '#=Common.Date.FromJsonDMYHM(ETD)#',
                filterable: { cell: { template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); }, operator: 'equal', showOperators: false } },
            },
            {
                field: 'ETA', title: '<b>Ngày giao hàng</b><br>[ETA]', width: '120px', template: '#=Common.Date.FromJsonDMYHM(ETA)#',
                filterable: { cell: { template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); }, operator: 'equal', showOperators: false } },
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
                field: 'OrderCreatedDate', title: '<b>Ngày tạo đơn</b><br>[OrderCreatedDate]', width: '120px', template: '#=Common.Date.FromJsonDMYHM(OrderCreatedDate)#',
                filterable: { cell: { template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); }, operator: 'equal', showOperators: false } },
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
                field: 'TOMasterNote1', title: '<b>Chuyến ghi chú 1</b><br>[TOMasterNote1]', width: '120px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'TOMasterNote2', title: '<b>Chuyến ghi chú 2</b><br>[TOMasterNote2]', width: '120px',
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
            {
                field: 'MaxWeightCal', title: '<b>Trọng tải xe</b><br>[MaxWeightCal]', width: '120px', template: '#=MaxWeightCal==null?" ":Common.Number.ToNumber3(MaxWeightCal)#',
                filterable: { cell: { operator: 'gte', showOperators: false } }
            },
            {
                field: 'RegWeight', title: '<b>Trọng tải đăng ký xe</b><br>[RegWeight]', width: '120px', template: '#=RegWeight==null?" ":Common.Number.ToNumber3(RegWeight)#',
                filterable: { cell: { operator: 'gte', showOperators: false } }
            },
            {
                field: 'RegCapacity', title: '<b>Số khối đăng ký xe</b><br>[RegCapacity]', width: '120px', template: '#=RegCapacity==null?" ":Common.Number.ToNumber3(RegCapacity)#',
                filterable: { cell: { operator: 'gte', showOperators: false } }
            },
            {
                field: 'SortConfigOrder', title: '<b>Thứ tự đơn hàng trong tháng</b><br>[SortConfigOrder]', width: '120px',
                filterable: { cell: { operator: 'equal', showOperators: false } }
            },
            {
                field: 'SortConfigMaster', title: '<b>Thứ tự chuyến trong tháng</b><br>[SortConfigMaster]', width: '120px',
                filterable: { cell: { operator: 'equal', showOperators: false } }
            },
            {
                field: 'TotalOrderInDay', title: '<b>Tổng đơn hàng trong ngày</b><br>[TotalOrderInDay]', width: '120px',
                filterable: { cell: { operator: 'equal', showOperators: false } }
            },
            {
                field: 'TotalMasterInDay', title: '<b>Tổng chuyến trong ngày</b><br>[TotalMasterInDay]', width: '120px',
                filterable: { cell: { operator: 'equal', showOperators: false } }
            },
            { title: ' ', sortable: false, filterable: false, menu: false }
        ]
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
                    if (Common.HasValue($scope.REPDIPL_Order_gridOptions.dataSource.filter())) {
                        filters = $scope.REPDIPL_Order_gridOptions.dataSource.filter().filters;
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
                    $scope.REPDIPL_Order_gridOptions.dataSource.filter(filters);
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
                    if (Common.HasValue($scope.REPDIPL_Order_gridOptions.dataSource.filter())) {
                        filters = $scope.REPDIPL_Order_gridOptions.dataSource.filter().filters;
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
                    $scope.REPDIPL_Order_gridOptions.dataSource.filter(filters);
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
            method: _REPDIPL_Order.URL.Search,
            data: { lstid: $scope.Item.lstCustomerID, lstgroupid: $scope.Item.lstGroupID, lststockid: $scope.Item.lstStockID, dtfrom: $scope.Item.DateFrom, dtto: $scope.Item.DateTo },
            success: function (res) {
                $rootScope.IsLoading = false;

                $scope.REPDIPL_Order_gridOptions.dataSource.data(res);
            },
            error: function (res) {
                $rootScope.IsLoading = false;
            }
        });
    };

    $scope.Excel_Click = function ($event) {
        $event.preventDefault();

        var request = Common.Request.CreateFromGrid($scope.REPDIPL_Order_gridOptions.dataSource);

        var functionID = $rootScope.FunctionItem.ID;
        $rootScope.UploadFile({
            IsImage: false, ID: functionID, AllowChange: true, ShowChoose: true,
            Type: Common.CATTypeOfFileCode.TEMPLATEREPORT,
            Complete: function (file) {
                $rootScope.IsLoading = true;
                Common.Services.Call($http, {
                    url: Common.Services.url.REP,
                    method: _REPDIPL_Order.URL.Template,
                    data: { itemfile: file, lstid: $scope.Item.lstCustomerID, lstgroupid: $scope.Item.lstGroupID, dtfrom: $scope.Item.DateFrom, dtto: $scope.Item.DateTo, request: request },
                    success: function (res) {
                        $rootScope.IsLoading = false;
                        $rootScope.DownloadFile(res);
                    },
                    error: function (res) {
                        $rootScope.IsLoading = false;
                    }
                });
            }
        });
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
            method: _REPDIPL_Order.URL.SettingList,
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
                    method: _REPDIPL_Order.URL.SettingSave,
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
                            method: _REPDIPL_Order.URL.SettingList,
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
                    method: _REPDIPL_Order.URL.SettingDelete,
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
                            method: _REPDIPL_Order.URL.SettingList,
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

        var request = Common.Request.CreateFromGrid($scope.REPDIPL_Order_gridOptions.dataSource);
        $rootScope.IsLoading = true;
        Common.Services.Call($http, {
            url: Common.Services.url.REP,
            method: _REPDIPL_Order.URL.SettingAction,
            data: { item: data, dtfrom: $scope.Item.DateFrom, dtto: $scope.Item.DateTo, request: request },
            success: function (res) {
                $rootScope.IsLoading = false;
                $rootScope.DownloadFile(res);
            }
        });
    }

    //#region Common
    $scope.SettingReport_OrderRoutingClick = function ($event, win) {
        $event.preventDefault();

        var data = $scope.SettingItem;
        $scope.OrderRoutingHasChoose = false;
        win.center().open();
        if (Common.HasValue(data.ListOrderRouting)) {
            angular.forEach(data.ListOrderRouting, function (value, key) {
                value.IsChoose = false;
            });
            $scope.SettingReport_OrderRouting_Grid.dataSource.data(data.ListOrderRouting);
        } else {
            $scope.SettingReport_OrderRouting_Grid.dataSource.data([]);
        }
    }

    $scope.SettingReport_OPSRoutingClick = function ($event, win) {
        $event.preventDefault();

        var data = $scope.SettingItem;
        $scope.OPSRoutingHasChoose = false;
        win.center().open();
        if (Common.HasValue(data.ListOPSRouting)) {
            angular.forEach(data.ListOPSRouting, function (value, key) {
                value.IsChoose = false;
            });
            $scope.SettingReport_OPSRouting_Grid.dataSource.data(data.ListOPSRouting);
        } else {
            $scope.SettingReport_OPSRouting_Grid.dataSource.data([]);
        }
    }

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

    $scope.SettingReport_GOPartnerClick = function ($event, win) {
        $event.preventDefault();

        var data = $scope.SettingItem;
        $scope.GOPartnerHasChoose = false;
        win.center().open();
        if (Common.HasValue(data.ListGroupOfPartner)) {
            angular.forEach(data.ListGroupOfPartner, function (value, key) {
                value.IsChoose = false;
            });
            $scope.SettingReport_GOPartner_Grid.dataSource.data(data.ListGroupOfPartner);
        } else {
            $scope.SettingReport_GOPartner_Grid.dataSource.data([]);
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

    $scope.SettingReport_PartnerClick = function ($event, win) {
        $event.preventDefault();

        var data = $scope.SettingItem;
        $scope.PartnerHasChoose = false;
        win.center().open();
        if (Common.HasValue(data.ListPartner)) {
            angular.forEach(data.ListPartner, function (value, key) {
                value.IsChoose = false;
            });
            $scope.SettingReport_Partner_Grid.dataSource.data(data.ListPartner);
        } else {
            $scope.SettingReport_Partner_Grid.dataSource.data([]);
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
            method: _REPDIPL_Order.URL.SettingCusNotInList,
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
                method: _REPDIPL_Order.URL.SettingCusNotInSave,
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
                        method: _REPDIPL_Order.URL.SettingList,
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
                method: _REPDIPL_Order.URL.SettingCusDeleteList,
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
                        method: _REPDIPL_Order.URL.SettingList,
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
            method: _REPDIPL_Order.URL.SettingGOPNotInList,
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
                method: _REPDIPL_Order.URL.SettingGOPNotInSave,
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
                        method: _REPDIPL_Order.URL.SettingList,
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
                method: _REPDIPL_Order.URL.SettingGOPDeleteList,
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
                        method: _REPDIPL_Order.URL.SettingList,
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

    //#region Stock
    $scope.StockHasChoose = false;
    $scope.SettingReport_Stock_GridOptions = {
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
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,SettingReport_Stock_Grid,stock_GridChoose_Change)" />',
                headerAttributes: { style: 'text-align: center;' }, attributes: { style: 'text-align: center;' },
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,SettingReport_Stock_Grid,stock_GridChoose_Change)" />',
                filterable: false, sortable: false
            },
            {
                field: 'StockCode', title: 'Mã', width: '150px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'StockLocationName', title: 'Tên', width: '150px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'StockCountryName', title: 'Quốc gia', width: '175px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'StockProvinceName', title: 'Tỉnh thành', width: '175px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'StockDistrictName', title: 'Quận huyện',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
        ]
    }
    $scope.stock_GridChoose_Change = function ($event, grid, hasChoose) {
        $scope.StockHasChoose = hasChoose;
    }

    $scope.stock_AddNew = function ($event, win) {
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
            $scope.stockNotIn_GridOptions.dataSource.read();
        }
    }

    $scope.stockNotIn_GridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.REP,
            method: _REPDIPL_Order.URL.SettingStockNotInList,
            readparam: function () { return { lstCus: $scope.SettingItem.ListCustomer, lstStock: $scope.SettingItem.ListStock } },
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
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,stockNotIn_Grid,stockNotIn_GridChoose_Change)" />',
                headerAttributes: { style: 'text-align: center;' }, attributes: { style: 'text-align: center;' },
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,stockNotIn_Grid,stockNotIn_GridChoose_Change)" />',
                filterable: false, sortable: false
            },

            {
                field: 'Code', title: 'Mã', width: '150px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'LocationName', title: 'Tên', width: '150px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'CountryName', title: 'Quốc gia', width: '175px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'ProvinceName', title: 'Tỉnh thành', width: '175px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'DistrictName', title: 'Quận huyện',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
        ]
    }

    $scope.stockNotIn_GridChoose_Change = function ($event, grid, hasChoose) {
        $scope.StockNotInHasChoose = hasChoose;
    }

    $scope.stockNotIn_Save_Click = function ($event, win, grid) {
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
                method: _REPDIPL_Order.URL.SettingStockNotInSave,
                data: { item: $scope.SettingItem, lst: datasend },
                success: function (res) {
                    $scope.SettingItem = res;
                    if (Common.HasValue(res.ListStock)) {
                        angular.forEach(res.ListStock, function (value, key) {
                            value.IsChoose = false;
                        });
                        $scope.SettingReport_Stock_GridOptions.dataSource.data(res.ListStock);
                    }
                    win.close();
                    $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });

                    Common.Services.Call($http, {
                        url: Common.Services.url.REP,
                        method: _REPDIPL_Order.URL.SettingList,
                        data: { functionID: $rootScope.FunctionItem.ID },
                        success: function (res) {
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

    $scope.stock_Delete = function ($event, grid) {
        $event.preventDefault();
        var data = grid.dataSource.data();
        var datasend = [];
        Common.Data.Each(data, function (o) {
            if (o.IsChoose) datasend.push(o.StockID);
        })
        if (datasend.length > 0) {
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.REP,
                method: _REPDIPL_Order.URL.SettingStockDeleteList,
                data: { item: $scope.SettingItem, lst: datasend },
                success: function (res) {
                    $scope.SettingItem = res;
                    if (Common.HasValue(res.ListStock)) {
                        angular.forEach(res.ListStock, function (value, key) {
                            value.IsChoose = false;
                        });
                        $scope.SettingReport_Stock_GridOptions.dataSource.data(res.ListStock);
                    }
                    $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });

                    Common.Services.Call($http, {
                        url: Common.Services.url.REP,
                        method: _REPDIPL_Order.URL.SettingList,
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

    //#region Province
    $scope.ProvinceHasChoose = false;
    $scope.SettingReport_Province_GridOptions = {
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number', editable: false },
                    ProvinceCode: { type: 'string' },
                    ProvinceName: { type: 'string' },
                    IsChoose: { type: 'boolean' },
                }
            },
            pageSize: 20
        }),
        height: '100%', pageable: true, sortable: { mode: 'multiple', allowUnsort: true }, columnMenu: false, resizable: true, reorderable: false, filterable: { mode: 'row' }, selectable: 'row',
        columns: [
            {
                title: ' ', width: '35px',
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,SettingReport_Province_Grid,Province_GridChoose_Change)" />',
                headerAttributes: { style: 'text-align: center;' }, attributes: { style: 'text-align: center;' },
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,SettingReport_Province_Grid,Province_GridChoose_Change)" />',
                filterable: false, sortable: false
            },
            {
                field: 'ProvinceCode', title: 'Mã', width: '150px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'ProvinceName', title: 'Tỉnh/Thành', width: '150px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
        ]
    }
    $scope.Province_GridChoose_Change = function ($event, grid, hasChoose) {
        $scope.ProvinceHasChoose = hasChoose;
    }

    $scope.Province_AddNew = function ($event, win) {
        $event.preventDefault();
        win.center().open();
        $scope.ProvinceNotIn_GridOptions.dataSource.read();
    }

    $scope.ProvinceNotIn_GridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.REP,
            method: _REPDIPL_Index.URL.SettingProvinceInList,
            readparam: function () { return { lstProvince: $scope.SettingItem.ListProvince } },
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
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,ProvinceNotIn_Grid,ProvinceNotIn_GridChoose_Change)" />',
                headerAttributes: { style: 'text-align: center;' }, attributes: { style: 'text-align: center;' },
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,ProvinceNotIn_Grid,ProvinceNotIn_GridChoose_Change)" />',
                filterable: false, sortable: false
            },

            {
                field: 'Code', title: 'Mã', width: '150px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'ProvinceName', title: 'Tỉnh/Thành', width: '150px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
        ]
    }

    $scope.ProvinceNotIn_GridChoose_Change = function ($event, grid, hasChoose) {
        $scope.ProvinceNotInHasChoose = hasChoose;
    }

    $scope.ProvinNotIn_Save_Click = function ($event, win, grid) {
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
                method: _REPDIPL_Index.URL.SettingProvinceNotInSave,
                data: { item: $scope.SettingItem, lstProvinceID: datasend },
                success: function (res) {
                    debugger
                    $scope.SettingItem = res;
                    if (Common.HasValue(res.ListProvince)) {
                        angular.forEach(res.ListProvince, function (value, key) {
                            value.IsChoose = false;
                        });
                        $scope.SettingReport_Province_Grid.dataSource.data(res.ListProvince);
                    }
                    win.close();
                    $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });

                    Common.Services.Call($http, {
                        url: Common.Services.url.REP,
                        method: _REPDIPL_Index.URL.SettingList,
                        data: { functionID: $rootScope.FunctionItem.ID },
                        success: function (res) {
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

    $scope.Province_Delete = function ($event, grid) {
        $event.preventDefault();
        var data = grid.dataSource.data();
        var datasend = [];
        Common.Data.Each(data, function (o) {
            if (o.IsChoose) datasend.push(o.ProvinceID);
        })
        if (datasend.length > 0) {
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.REP,
                method: _REPDIPL_Index.URL.SettingProvinceDeleteList,
                data: { item: $scope.SettingItem, lstProvinceID: datasend },
                success: function (res) {
                    $scope.SettingItem = res;
                    if (Common.HasValue(res.ListProvince)) {
                        angular.forEach(res.ListProvince, function (value, key) {
                            value.IsChoose = false;
                        });
                        $scope.SettingReport_Province_GridOptions.dataSource.data(res.ListProvince);
                    }
                    $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });

                    Common.Services.Call($http, {
                        url: Common.Services.url.REP,
                        method: _REPDIPL_Index.URL.SettingList,
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


    //#region GroupOfLocation
    $scope.LocationHasChoose = false;
    $scope.SettingReport_Location_GridOptions = {
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
        height: '100%', pageable: true, sortable: { mode: 'multiple', allowUnsort: true }, columnMenu: false, resizable: true, reorderable: false, filterable: { mode: 'row' }, selectable: 'row',
        columns: [
            {
                title: ' ', width: '35px',
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,SettingReport_Location_Grid,Location_GridChoose_Change)" />',
                headerAttributes: { style: 'text-align: center;' }, attributes: { style: 'text-align: center;' },
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,SettingReport_Location_Grid,Location_GridChoose_Change)" />',
                filterable: false, sortable: false
            },
            {
                field: 'GroupOfLocationCode', title: 'Mã', width: '150px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'GroupOfLocationName', title: 'Tên địa chỉ', width: '150px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
        ]
    }
    $scope.Location_GridChoose_Change = function ($event, grid, hasChoose) {
        $scope.LocationHasChoose = hasChoose;
    }

    $scope.Location_AddNew = function ($event, win) {
        $event.preventDefault();
        win.center().open();
        $scope.LocationNotIn_GridOptions.dataSource.read();
    }

    $scope.LocationNotIn_GridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.REP,
            method: _REPDIPL_Index.URL.SettingLocationNotInList,
            readparam: function () { return { lstGroupOfLocation: $scope.SettingItem.ListGroupOfLocation } },
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
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,LocationNotIn_Grid,LocationNotIn_GridChoose_Change)" />',
                headerAttributes: { style: 'text-align: center;' }, attributes: { style: 'text-align: center;' },
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,LocationNotIn_Grid,LocationNotIn_GridChoose_Change)" />',
                filterable: false, sortable: false
            },

            {
                field: 'Code', title: 'Mã', width: '150px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'GroupName', title: 'Tên địa điểm', width: '150px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
        ]
    }

    $scope.LocationNotIn_GridChoose_Change = function ($event, grid, hasChoose) {
        $scope.LocationNotInHasChoose = hasChoose;
    }

    $scope.LocationNotIn_Save_Click = function ($event, win, grid) {
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
                method: _REPDIPL_Index.URL.SettingLocationNotInSave,
                data: { item: $scope.SettingItem, lstGroupOfLocationID: datasend },
                success: function (res) {
                    debugger
                    $scope.SettingItem = res;
                    if (Common.HasValue(res.ListGroupOfLocation)) {
                        angular.forEach(res.ListGroupOfLocation, function (value, key) {
                            value.IsChoose = false;
                        });
                        $scope.SettingReport_Location_GridOptions.dataSource.data(res.ListGroupOfLocation);
                    }
                    win.close();
                    $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });

                    Common.Services.Call($http, {
                        url: Common.Services.url.REP,
                        method: _REPDIPL_Index.URL.SettingList,
                        data: { functionID: $rootScope.FunctionItem.ID },
                        success: function (res) {
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

    $scope.Location_Delete = function ($event, grid) {
        $event.preventDefault();
        var data = grid.dataSource.data();
        var datasend = [];
        Common.Data.Each(data, function (o) {
            if (o.IsChoose) datasend.push(o.GroupOfLocationID);
        })
        if (datasend.length > 0) {
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.REP,
                method: _REPDIPL_Index.URL.SettingLocationDeleteList,
                data: { item: $scope.SettingItem, lstGroupOfLocationID: datasend },
                success: function (res) {
                    $scope.SettingItem = res;
                    if (Common.HasValue(res.ListGroupOfLocation)) {
                        angular.forEach(res.ListGroupOfLocation, function (value, key) {
                            value.IsChoose = false;
                        });
                        $scope.SettingReport_Location_GridOptions.dataSource.data(res.ListGroupOfLocation);
                    }
                    $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });

                    Common.Services.Call($http, {
                        url: Common.Services.url.REP,
                        method: _REPDIPL_Index.URL.SettingList,
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

    //#region GroupOfPartner
    $scope.GOPartnerHasChoose = false;
    $scope.SettingReport_GOPartner_GridOptions = {
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
        height: '100%', pageable: true, sortable: { mode: 'multiple', allowUnsort: true }, columnMenu: false, resizable: true, reorderable: false, filterable: { mode: 'row' }, selectable: 'row',
        columns: [
            {
                title: ' ', width: '35px',
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,SettingReport_GOPartner_Grid,GOPartner_GridChoose_Change)" />',
                headerAttributes: { style: 'text-align: center;' }, attributes: { style: 'text-align: center;' },
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,SettingReport_GOPartner_Grid,GOPartner_GridChoose_Change)" />',
                filterable: false, sortable: false
            },
            {
                field: 'GroupOfPartnerCode', title: 'Mã', width: '150px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'GroupOfPartnerName', title: 'Tên địa chỉ', width: '150px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
        ]
    }
    $scope.GOPartner_GridChoose_Change = function ($event, grid, hasChoose) {
        $scope.GOPartnerHasChoose = hasChoose;
    }

    $scope.GOPartner_AddNew = function ($event, win) {
        $event.preventDefault();
        win.center().open();
        $scope.GOPartnerNotIn_GridOptions.dataSource.read();
    }

    $scope.GOPartnerNotIn_GridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.REP,
            method: _REPDIPL_Index.URL.SettingPartnerInList,
            readparam: function () { return { lstGroupOfPartner: $scope.SettingItem.ListGroupOfPartner } },
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
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,GOPartnerNotIn_Grid,GOPartnerNotIn_GridChoose_Change)" />',
                headerAttributes: { style: 'text-align: center;' }, attributes: { style: 'text-align: center;' },
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,GOPartnerNotIn_Grid,GOPartnerNotIn_GridChoose_Change)" />',
                filterable: false, sortable: false
            },

            {
                field: 'Code', title: 'Mã', width: '150px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'GroupName', title: 'Tên địa điểm', width: '150px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
        ]
    }

    $scope.GOPartnerNotIn_GridChoose_Change = function ($event, grid, hasChoose) {
        $scope.GOPartnerNotInHasChoose = hasChoose;
    }

    $scope.GOPartnerNotIn_Save_Click = function ($event, win, grid) {
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
                method: _REPDIPL_Index.URL.SettingPartnerNotInSave,
                data: { item: $scope.SettingItem, lstGroupOfPartnerID: datasend },
                success: function (res) {
                    debugger
                    $scope.SettingItem = res;
                    if (Common.HasValue(res.ListGroupOfPartner)) {
                        angular.forEach(res.ListGroupOfPartner, function (value, key) {
                            value.IsChoose = false;
                        });
                        $scope.SettingReport_GOPartner_GridOptions.dataSource.data(res.ListGroupOfPartner);
                    }
                    win.close();
                    $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });

                    Common.Services.Call($http, {
                        url: Common.Services.url.REP,
                        method: _REPDIPL_Index.URL.SettingList,
                        data: { functionID: $rootScope.FunctionItem.ID },
                        success: function (res) {
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

    $scope.GOPartner_Delete = function ($event, grid) {
        $event.preventDefault();
        var data = grid.dataSource.data();
        var datasend = [];
        Common.Data.Each(data, function (o) {
            if (o.IsChoose) datasend.push(o.GroupOfPartnerID);
        })
        if (datasend.length > 0) {
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.REP,
                method: _REPDIPL_Index.URL.SettingPartnerDeleteList,
                data: { item: $scope.SettingItem, lstGroupOfPartnerID: datasend },
                success: function (res) {
                    $scope.SettingItem = res;
                    if (Common.HasValue(res.ListGroupOfPartner)) {
                        angular.forEach(res.ListGroupOfPartner, function (value, key) {
                            value.IsChoose = false;
                        });
                        $scope.SettingReport_GOPartner_GridOptions.dataSource.data(res.ListGroupOfPartner);
                    }
                    $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });

                    Common.Services.Call($http, {
                        url: Common.Services.url.REP,
                        method: _REPDIPL_Index.URL.SettingList,
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


    //#region ServiceOfOrder
    $scope.ServiceOfOrderHasChoose = false;
    $scope.SettingReport_ServiceOfOrder_GridOptions = {
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
        height: '100%', pageable: true, sortable: { mode: 'multiple', allowUnsort: true }, columnMenu: false, resizable: true, reorderable: false, filterable: { mode: 'row' }, selectable: 'row',
        columns: [
            {
                title: ' ', width: '35px',
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,SettingReport_ServiceOfOrder_Grid,ServiceOfOrder_GridChoose_Change)" />',
                headerAttributes: { style: 'text-align: center;' }, attributes: { style: 'text-align: center;' },
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,SettingReport_ServiceOfOrder_Grid,ServiceOfOrder_GridChoose_Change)" />',
                filterable: false, sortable: false
            },
            {
                field: 'ServiceOfOrderCode', title: 'Mã', width: '150px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'ServiceOfOrderName', title: 'Tên dịch vụ', width: '150px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
        ]
    }
    $scope.ServiceOfOrder_GridChoose_Change = function ($event, grid, hasChoose) {
        $scope.ServiceOfOrderHasChoose = hasChoose;
    }

    $scope.ServiceOfOrder_AddNew = function ($event, win) {
        $event.preventDefault();
        win.center().open();
        $scope.ServiceOfOrderNotIn_GridOptions.dataSource.read();
    }

    $scope.ServiceOfOrderNotIn_GridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.REP,
            method: _REPDIPL_Index.URL.SettingServiceOfOrderInList,
            readparam: function () { return { lstServiceOfOrder: $scope.SettingItem.ListServiceOfOrder } },
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
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,ServiceOfOrderNotIn_Grid,ServiceOfOrderNotIn_GridChoose_Change)" />',
                headerAttributes: { style: 'text-align: center;' }, attributes: { style: 'text-align: center;' },
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,ServiceOfOrderNotIn_Grid,ServiceOfOrderNotIn_GridChoose_Change)" />',
                filterable: false, sortable: false
            },

            {
                field: 'Code', title: 'Mã', width: '150px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'Name', title: 'Tên dịch vụ', width: '150px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
        ]
    }

    $scope.ServiceOfOrderNotIn_GridChoose_Change = function ($event, grid, hasChoose) {
        $scope.ServiceOfOrderNotInHasChoose = hasChoose;
    }

    $scope.ServiceOfOrderNotIn_Save_Click = function ($event, win, grid) {
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
                method: _REPDIPL_Index.URL.SettingServiceOfOrderNotInSave,
                data: { item: $scope.SettingItem, lstServiceOfOrderID: datasend },
                success: function (res) {
                    debugger
                    $scope.SettingItem = res;
                    if (Common.HasValue(res.ListServiceOfOrder)) {
                        angular.forEach(res.ListServiceOfOrder, function (value, key) {
                            value.IsChoose = false;
                        });
                        $scope.SettingReport_ServiceOfOrder_GridOptions.dataSource.data(res.ListServiceOfOrder);
                    }
                    win.close();
                    $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });

                    Common.Services.Call($http, {
                        url: Common.Services.url.REP,
                        method: _REPDIPL_Index.URL.SettingList,
                        data: { functionID: $rootScope.FunctionItem.ID },
                        success: function (res) {
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

    $scope.ServiceOfOrder_Delete = function ($event, grid) {
        $event.preventDefault();
        var data = grid.dataSource.data();
        var datasend = [];
        Common.Data.Each(data, function (o) {
            if (o.IsChoose) datasend.push(o.ServiceOfOrderID);
        })
        if (datasend.length > 0) {
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.REP,
                method: _REPDIPL_Index.URL.SettingServiceOfOrderDeleteList,
                data: { item: $scope.SettingItem, lstServiceOfOrderID: datasend },
                success: function (res) {
                    $scope.SettingItem = res;
                    if (Common.HasValue(res.ListServiceOfOrder)) {
                        angular.forEach(res.ListServiceOfOrder, function (value, key) {
                            value.IsChoose = false;
                        });
                        $scope.SettingReport_ServiceOfOrder_GridOptions.dataSource.data(res.ListServiceOfOrder);
                    }
                    $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });

                    Common.Services.Call($http, {
                        url: Common.Services.url.REP,
                        method: _REPDIPL_Index.URL.SettingList,
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

    //#region OrderRouting
    $scope.OrderRoutingHasChoose = false;
    $scope.SettingReport_OrderRouting_GridOptions = {
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'RoutingID',
                fields: {
                    RoutingID: { type: 'number', editable: false },
                    IsChoose: { type: 'boolean' },
                }
            },
            pageSize: 20
        }),
        height: '100%', pageable: true, sortable: { mode: 'multiple', allowUnsort: true }, columnMenu: false, resizable: true, reorderable: false, filterable: { mode: 'row' }, selectable: 'row',
        columns: [
            {
                title: ' ', width: '35px',
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,SettingReport_OrderRouting_Grid,OrderRouting_GridChoose_Change)" />',
                headerAttributes: { style: 'text-align: center;' }, attributes: { style: 'text-align: center;' },
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,SettingReport_OrderRouting_Grid,OrderRouting_GridChoose_Change)" />',
                filterable: false, sortable: false
            },
            {
                field: 'RoutingCode', title: 'Mã cung đường', width: '150px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'RoutingName', title: 'Cung đường', width: '150px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
        ]
    }
    $scope.OrderRouting_GridChoose_Change = function ($event, grid, hasChoose) {
        $scope.OrderRoutingHasChoose = hasChoose;
    }

    $scope.OrderRouting_AddNew = function ($event, win) {
        $event.preventDefault();
        win.center().open();
        $scope.OrderRoutingNotIn_GridOptions.dataSource.read();
    }

    $scope.OrderRoutingNotIn_GridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.REP,
            method: _REPDIPL_Index.URL.OrderRoutingNotInList,
            readparam: function () { return { lstCus: $scope.SettingItem.ListCustomer, lstOrderRouting: $scope.SettingItem.ListOrderRouting } },
            model: {
                id: 'RoutingID',
                fields: {
                    RoutingID: { type: 'number', editable: false, nullable: true },
                    IsChoose: { type: 'boolean' },
                }
            }
        }),
        height: '100%', pageable: true, sortable: true, columnMenu: false, filterable: { mode: 'row' }, resizable: true, editable: false,
        columns: [
            {
                title: ' ', width: '35px',
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,OrderRoutingNotIn_Grid,OrderRoutingNotIn_GridChoose_Change)" />',
                headerAttributes: { style: 'text-align: center;' }, attributes: { style: 'text-align: center;' },
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,OrderRoutingNotIn_Grid,OrderRoutingNotIn_GridChoose_Change)" />',
                filterable: false, sortable: false
            },

            {
                field: 'RoutingCode', title: 'Mã cung đường', width: '150px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'RoutingName', title: 'Cung đường', width: '150px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
        ]
    }

    $scope.OrderRoutingNotIn_GridChoose_Change = function ($event, grid, hasChoose) {
        $scope.OrderRoutingNotInHasChoose = hasChoose;
    }

    $scope.OrderRoutingNotIn_Save_Click = function ($event, win, grid) {
        $event.preventDefault();

        var data = grid.dataSource.data();
        var datasend = [];
        Common.Data.Each(data, function (o) {
            if (o.IsChoose) datasend.push(o.RoutingID);
        })
        if (datasend.length > 0) {
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.REP,
                method: _REPDIPL_Index.URL.OrderRoutingNotInSave,
                data: { item: $scope.SettingItem, lstOrderRoutingID: datasend },
                success: function (res) {
                    debugger
                    $scope.SettingItem = res;
                    if (Common.HasValue(res.ListOrderRouting)) {
                        angular.forEach(res.ListOrderRouting, function (value, key) {
                            value.IsChoose = false;
                        });
                        $scope.SettingReport_OrderRouting_GridOptions.dataSource.data(res.ListOrderRouting);
                    }
                    win.close();
                    $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });

                    Common.Services.Call($http, {
                        url: Common.Services.url.REP,
                        method: _REPDIPL_Index.URL.SettingList,
                        data: { functionID: $rootScope.FunctionItem.ID },
                        success: function (res) {
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

    $scope.OrderRouting_Delete = function ($event, grid) {
        $event.preventDefault();
        var data = grid.dataSource.data();
        var datasend = [];
        Common.Data.Each(data, function (o) {
            if (o.IsChoose) datasend.push(o.RoutingID);
        })
        if (datasend.length > 0) {
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.REP,
                method: _REPDIPL_Index.URL.OrderRoutingDeleteList,
                data: { item: $scope.SettingItem, lstOrderRoutingID: datasend },
                success: function (res) {
                    $scope.SettingItem = res;
                    if (Common.HasValue(res.ListOrderRouting)) {
                        angular.forEach(res.ListOrderRouting, function (value, key) {
                            value.IsChoose = false;
                        });
                        $scope.SettingReport_OrderRouting_GridOptions.dataSource.data(res.ListOrderRouting);
                    }
                    $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });

                    Common.Services.Call($http, {
                        url: Common.Services.url.REP,
                        method: _REPDIPL_Index.URL.SettingList,
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

    //#region OPSRouting
    $scope.OPSRoutingHasChoose = false;
    $scope.SettingReport_OPSRouting_GridOptions = {
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'RoutingID',
                fields: {
                    RoutingID: { type: 'number', editable: false },
                    IsChoose: { type: 'boolean' },
                }
            },
            pageSize: 20
        }),
        height: '100%', pageable: true, sortable: { mode: 'multiple', allowUnsort: true }, columnMenu: false, resizable: true, reorderable: false, filterable: { mode: 'row' }, selectable: 'row',
        columns: [
            {
                title: ' ', width: '35px',
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,SettingReport_OPSRouting_Grid,OPSRouting_GridChoose_Change)" />',
                headerAttributes: { style: 'text-align: center;' }, attributes: { style: 'text-align: center;' },
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,SettingReport_OPSRouting_Grid,OPSRouting_GridChoose_Change)" />',
                filterable: false, sortable: false
            },
            {
                field: 'RoutingCode', title: 'Mã cung đường', width: '150px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'RoutingName', title: 'Cung đường', width: '150px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
        ]
    }
    $scope.OPSRouting_GridChoose_Change = function ($event, grid, hasChoose) {
        $scope.OPSRoutingHasChoose = hasChoose;
    }

    $scope.OPSRouting_AddNew = function ($event, win) {
        $event.preventDefault();
        win.center().open();
        $scope.OPSRoutingNotIn_GridOptions.dataSource.read();
    }

    $scope.OPSRoutingNotIn_GridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.REP,
            method: _REPDIPL_Index.URL.OPSRoutingNotInList,
            readparam: function () { return { lstCus: $scope.SettingItem.ListCustomer, lstOPSRouting: $scope.SettingItem.ListOPSRouting } },
            model: {
                id: 'RoutingID',
                fields: {
                    RoutingID: { type: 'number', editable: false, nullable: true },
                    IsChoose: { type: 'boolean' },
                }
            }
        }),
        height: '100%', pageable: true, sortable: true, columnMenu: false, filterable: { mode: 'row' }, resizable: true, editable: false,
        columns: [
            {
                title: ' ', width: '35px',
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,OPSRoutingNotIn_Grid,OPSRoutingNotIn_GridChoose_Change)" />',
                headerAttributes: { style: 'text-align: center;' }, attributes: { style: 'text-align: center;' },
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,OPSRoutingNotIn_Grid,OPSRoutingNotIn_GridChoose_Change)" />',
                filterable: false, sortable: false
            },

            {
                field: 'RoutingCode', title: 'Mã cung đường', width: '150px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'RoutingName', title: 'Cung đường', width: '150px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
        ]
    }

    $scope.OPSRoutingNotIn_GridChoose_Change = function ($event, grid, hasChoose) {
        $scope.OPSRoutingNotInHasChoose = hasChoose;
    }

    $scope.OPSRoutingNotIn_Save_Click = function ($event, win, grid) {
        $event.preventDefault();

        var data = grid.dataSource.data();
        var datasend = [];
        Common.Data.Each(data, function (o) {
            if (o.IsChoose) datasend.push(o.RoutingID);
        })
        if (datasend.length > 0) {
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.REP,
                method: _REPDIPL_Index.URL.OPSRoutingNotInSave,
                data: { item: $scope.SettingItem, lstOPSRoutingID: datasend },
                success: function (res) {
                    debugger
                    $scope.SettingItem = res;
                    if (Common.HasValue(res.ListOPSRouting)) {
                        angular.forEach(res.ListOPSRouting, function (value, key) {
                            value.IsChoose = false;
                        });
                        $scope.SettingReport_OPSRouting_GridOptions.dataSource.data(res.ListOPSRouting);
                    }
                    win.close();
                    $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });

                    Common.Services.Call($http, {
                        url: Common.Services.url.REP,
                        method: _REPDIPL_Index.URL.SettingList,
                        data: { functionID: $rootScope.FunctionItem.ID },
                        success: function (res) {
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

    $scope.OPSRouting_Delete = function ($event, grid) {
        $event.preventDefault();
        var data = grid.dataSource.data();
        var datasend = [];
        Common.Data.Each(data, function (o) {
            if (o.IsChoose) datasend.push(o.RoutingID);
        })
        if (datasend.length > 0) {
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.REP,
                method: _REPDIPL_Index.URL.OPSRoutingDeleteList,
                data: { item: $scope.SettingItem, lstOPSRoutingID: datasend },
                success: function (res) {
                    $scope.SettingItem = res;
                    if (Common.HasValue(res.ListOPSRouting)) {
                        angular.forEach(res.ListOPSRouting, function (value, key) {
                            value.IsChoose = false;
                        });
                        $scope.SettingReport_OPSRouting_GridOptions.dataSource.data(res.ListOPSRouting);
                    }
                    $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });

                    Common.Services.Call($http, {
                        url: Common.Services.url.REP,
                        method: _REPDIPL_Index.URL.SettingList,
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

    //#region Partner
    $scope.PartnerHasChoose = false;
    $scope.SettingReport_Partner_GridOptions = {
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'PartnerID',
                fields: {
                    PartnerID: { type: 'number', editable: false },
                    IsChoose: { type: 'boolean' },
                }
            },
            pageSize: 20
        }),
        height: '100%', pageable: true, sortable: { mode: 'multiple', allowUnsort: true }, columnMenu: false, resizable: true, reorderable: false, filterable: { mode: 'row' }, selectable: 'row',
        columns: [
            {
                title: ' ', width: '35px',
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,SettingReport_Partner_Grid,Partner_GridChoose_Change)" />',
                headerAttributes: { style: 'text-align: center;' }, attributes: { style: 'text-align: center;' },
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,SettingReport_Partner_Grid,Partner_GridChoose_Change)" />',
                filterable: false, sortable: false
            },
            {
                field: 'PartnerCode', title: 'Mã NPP', width: '150px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'PartnerName', title: 'Nhà phân phối', width: '150px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'PartnerAddress', title: 'Địa chỉ', width: '150px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
        ]
    }
    $scope.Partner_GridChoose_Change = function ($event, grid, hasChoose) {
        $scope.PartnerHasChoose = hasChoose;
    }

    $scope.Partner_AddNew = function ($event, win) {
        $event.preventDefault();
        win.center().open();
        $scope.PartnerNotIn_GridOptions.dataSource.read();
    }

    $scope.PartnerNotIn_GridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.REP,
            method: _REPDIPL_Index.URL.PartnerNotInList,
            readparam: function () { return { lstPartner: $scope.SettingItem.ListPartner, lstCus: $scope.SettingItem.ListCustomer } },
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
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,PartnerNotIn_Grid,PartnerNotIn_GridChoose_Change)" />',
                headerAttributes: { style: 'text-align: center;' }, attributes: { style: 'text-align: center;' },
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,PartnerNotIn_Grid,PartnerNotIn_GridChoose_Change)" />',
                filterable: false, sortable: false
            },
            {
                field: 'PartnerCode', title: 'Mã NPP', width: '150px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'PartnerName', title: 'Nhà phân phối', width: '150px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'PartnerAddress', title: 'Địa chỉ', width: '150px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
        ]
    }

    $scope.PartnerNotIn_GridChoose_Change = function ($event, grid, hasChoose) {
        $scope.PartnerNotInHasChoose = hasChoose;
    }

    $scope.PartnerNotIn_Save_Click = function ($event, win, grid) {
        $event.preventDefault();

        var data = grid.dataSource.data();
        var datasend = [];
        Common.Data.Each(data, function (o) {
            if (o.IsChoose) datasend.push(o.PartnerID);
        })
        if (datasend.length > 0) {
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.REP,
                method: _REPDIPL_Index.URL.PartnerNotInSave,
                data: { item: $scope.SettingItem, lstPartnerID: datasend },
                success: function (res) {
                    debugger
                    $scope.SettingItem = res;
                    if (Common.HasValue(res.ListPartner)) {
                        angular.forEach(res.ListPartner, function (value, key) {
                            value.IsChoose = false;
                        });
                        $scope.SettingReport_Partner_GridOptions.dataSource.data(res.ListPartner);
                    }
                    win.close();
                    $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });

                    Common.Services.Call($http, {
                        url: Common.Services.url.REP,
                        method: _REPDIPL_Index.URL.SettingList,
                        data: { functionID: $rootScope.FunctionItem.ID },
                        success: function (res) {
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

    $scope.Partner_Delete = function ($event, grid) {
        $event.preventDefault();
        var data = grid.dataSource.data();
        var datasend = [];
        Common.Data.Each(data, function (o) {
            if (o.IsChoose) datasend.push(o.PartnerID);
        })
        if (datasend.length > 0) {
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.REP,
                method: _REPDIPL_Index.URL.PartnerDeleteList,
                data: { item: $scope.SettingItem, lstPartnerID: datasend },
                success: function (res) {
                    $scope.SettingItem = res;
                    if (Common.HasValue(res.ListPartner)) {
                        angular.forEach(res.ListPartner, function (value, key) {
                            value.IsChoose = false;
                        });
                        $scope.SettingReport_Partner_GridOptions.dataSource.data(res.ListPartner);
                    }
                    $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });

                    Common.Services.Call($http, {
                        url: Common.Services.url.REP,
                        method: _REPDIPL_Index.URL.SettingList,
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

    $scope.REPDIPL_Order_Export = function ($event) {
        $event.preventDefault();

        $rootScope.IsLoading = true;
        Common.Services.Call($http, {
            url: Common.Services.url.REP,
            method: _REPDIPL_Order.URL.Order_Export,
            data: {},
            success: function (res) {
                $rootScope.IsLoading = false;
                $rootScope.DownloadFile(res);
            }
        })
    }
}]);