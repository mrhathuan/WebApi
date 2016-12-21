
/// <reference path="~/Scripts/jquery-1.9.1.intellisense.js" />
/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />

var _REPDIPLOrder = {
    URL: {
        Search: 'REPDIPLOrder_Search',
        Template: 'REPDIPLOrder_Template',
        Read_Customer: 'REP_Customer_Read',
        Excel_Export: 'REPDIPLOrder_Export',
        Excel_ExportNew: 'REPDIPLOrder_ExportNew',
    },
}

angular.module('myapp').controller('REPDIPLOrder_IndexCtrl', ['$rootScope', '$scope', '$http', '$location', '$state', '$timeout', '$stateParams', function ($rootScope, $scope, $http, $location, $state, $timeout, $stateParams) {
    if ($rootScope.IsPageComplete != true) return;
    Common.Log('REPDIPLOrder_IndexCtrl');
    $rootScope.IsLoading = false;

    $scope.Auth = $rootScope.GetAuth();

    $scope.Item = {
        lstCustomerID: [],
        DateFrom: Common.Date.AddDay(new Date(), -5),
        DateTo: new Date(),
    }

    $scope.mulCustomer_Options = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: "contains",
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
        change: function (e) {
            var lst = this;
            $timeout(function () {
                $scope.SetWidth_Select(lst);
            }, 1);
        }

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

    Common.Services.Call($http, {
        url: Common.Services.url.REP,
        method: _REPDIPLOrder.URL.Read_Customer,
        data: {},
        success: function (res) {
            $scope.mulCustomer_Options.dataSource.data(res.Data);
        }
    });

    $scope.REPDIPLOrder_gridOptions = {
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
                }
            }
        }),
        height: '100%', pageable: false, sortable: false, columnMenu: false, resizable: true, reorderable: false, filterable: { mode: 'row' }, selectable: 'row',
        columns: [
            {
                field: 'OrderCode', title: '<b>Đơn hàng</b><br>[OrderCode]', width: '120px', locked: true,
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'CustomerCode', title: '<b>Mã khách hàng</b><br>[CustomerCode]', width: '120px', locked: true,
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'VehicleCode', title: '<b>Số xe</b><br>[VehicleCode]', width: '120px', locked: true,
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'VendorCode', title: '<b>Mã nhà vận tải</b><br>[VendorCode]', width: '120px', locked: true,
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
                field: 'DateConfig', title: '<b>Ngày tính giá</b><br>[DateConfig]', width: '120px', template: '#=Common.Date.FromJsonDDMMYY(DateConfig)#',
                filterable: { cell: { template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); }, operator: 'equal', showOperators: false } },
            },
            {
                field: 'RequestDate', title: '<b>Ngày y/c v/c</b><br>[RequestDate]', width: '120px', template: '#=Common.Date.FromJsonDDMMYY(RequestDate)#',
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
                field: 'VendorName', title: '<b>Nhà vận tải</b><br>[VendorName]', width: '120px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'CustomerName', title: '<b>Khách hàng</b><br>[CustomerName]', width: '120px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'KgTranfer', title: '<b>Kg lấy</b><br>[KgTranfer]', width: '120px', template: '#=KgTranfer==null?" ":Common.Number.ToNumber5(KgTranfer)#',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'TonTranfer', title: '<b>Tấn lấy</b><br>[TonTranfer]', width: '120px', template: '#=TonTranfer==null?" ":Common.Number.ToNumber5(TonTranfer)#',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'CBMTranfer', title: '<b>Khối lấy</b><br>[CBMTranfer]', width: '120px', template: '#=CBMTranfer==null?" ":Common.Number.ToNumber5(CBMTranfer)#',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'QuantityTranfer', title: '<b>Số lượng lấy</b><br>[QuantityTranfer]', width: '120px', template: '#=QuantityTranfer==null?" ":Common.Number.ToNumber5(QuantityTranfer)#',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'KgBBGN', title: '<b>Kg giao</b><br>[KgBBGN]', width: '120px', template: '#=KgBBGN==null?" ":Common.Number.ToNumber5(KgBBGN)#',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'TonBBGN', title: '<b>Tấn giao</b><br>[TonBBGN]', width: '120px', template: '#=TonBBGN==null?" ":Common.Number.ToNumber5(TonBBGN)#',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'CBMBBGN', title: '<b>Khối giao</b><br>[CBMBBGN]', width: '120px', template: '#=CBMBBGN==null?" ":Common.Number.ToNumber5(CBMBBGN)#',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'QuantityBBGN', title: '<b>Số lượng giao</b><br>[QuantityBBGN]', width: '120px', template: '#=QuantityBBGN==null?" ":Common.Number.ToNumber5(QuantityBBGN)#',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'KgReturn', title: '<b>Kg trả về</b><br>[KgReturn]', width: '120px', template: '#=KgReturn==null?" ":Common.Number.ToNumber5(KgReturn)#',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'TonReturn', title: '<b>Tấn trả về</b><br>[TonReturn]', width: '120px', template: '#=TonReturn==null?" ":Common.Number.ToNumber5(TonReturn)#',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'CBMReturn', title: '<b>Khối trả về</b><br>[CBMReturn]', width: '120px', template: '#=CBMReturn==null?" ":Common.Number.ToNumber5(CBMReturn)#',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'QuantityReturn', title: '<b>Số lượng trả về</b><br>[QuantityReturn]', width: '120px', template: '#=QuantityReturn==null?" ":Common.Number.ToNumber5(QuantityReturn)#',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'IncomeUnitPrice', title: '<b>Giá thu</b><br>[IncomeUnitPrice]', width: '120px', template: '#=IncomeUnitPrice==null?" ":Common.Number.ToMoney(IncomeUnitPrice)#',
                filterable: { cell: { operator: 'lte', showOperators: false } }
            },
            {
                field: 'IncomeQuantity', title: '<b>Số lượng thu</b><br>[IncomeQuantity]', width: '120px', template: '#=IncomeQuantity==null?" ":Common.Number.ToNumber5(IncomeQuantity)#',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'Income', title: '<b>Cước thu</b><br>[Income]', width: '120px', template: '#=Income==null?" ":Common.Number.ToMoney(Income)#',
                filterable: { cell: { operator: 'lte', showOperators: false } }
            },
            {
                field: 'IncomeLoadUnitPrice', title: '<b>Giá b.x lên thu</b><br>[IncomeLoadUnitPrice]', width: '120px', template: '#=IncomeLoadUnitPrice==null?" ":Common.Number.ToMoney(IncomeLoadUnitPrice)#',
                filterable: { cell: { operator: 'lte', showOperators: false } }
            },
            {
                field: 'IncomeLoadQuantity', title: '<b>Số lượng b.x lên thu</b><br>[IncomeLoadQuantity]', width: '120px', template: '#=IncomeLoadQuantity==null?" ":Common.Number.ToNumber5(IncomeLoadQuantity)#',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'IncomeLoad', title: '<b>Cước b.x lên thu</b><br>[IncomeLoad]', width: '120px', template: '#=IncomeLoad==null?" ":Common.Number.ToMoney(IncomeLoad)#',
                filterable: { cell: { operator: 'lte', showOperators: false } }
            },
            {
                field: 'IncomeUnLoadUnitPrice', title: '<b>Giá b.x xuống thu</b><br>[IncomeUnLoadUnitPrice]', width: '120px', template: '#=IncomeUnLoadUnitPrice==null?" ":Common.Number.ToMoney(IncomeUnLoadUnitPrice)#',
                filterable: { cell: { operator: 'lte', showOperators: false } }
            },
            {
                field: 'IncomeUnLoadQuantity', title: '<b>Số lượng b.x xuống thu</b><br>[IncomeUnLoadQuantity]', width: '120px', template: '#=IncomeUnLoadQuantity==null?" ":Common.Number.ToNumber5(IncomeUnLoadQuantity)#',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'IncomeUnLoad', title: '<b>Cước b.x xuống thu</b><br>[IncomeUnLoad]', width: '120px', template: '#=IncomeUnLoad==null?" ":Common.Number.ToMoney(IncomeUnLoad)#',
                filterable: { cell: { operator: 'lte', showOperators: false } }
            },
            {
                field: 'IncomeReturnUnitPrice', title: '<b>Giá trả về thu</b><br>[IncomeReturnUnitPrice]', width: '120px', template: '#=IncomeReturnUnitPrice==null?" ":Common.Number.ToMoney(IncomeReturnUnitPrice)#',
                filterable: { cell: { operator: 'lte', showOperators: false } }
            },
            {
                field: 'IncomeReturnQuantity', title: '<b>Số lượng trả về thu</b><br>[IncomeReturnQuantity]', width: '120px', template: '#=IncomeReturnQuantity==null?" ":Common.Number.ToNumber5(IncomeReturnQuantity)#',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'IncomeReturn', title: '<b>Cước trả về thu</b><br>[IncomeReturn]', width: '120px', template: '#=IncomeReturn==null?" ":Common.Number.ToMoney(IncomeReturn)#',
                filterable: { cell: { operator: 'lte', showOperators: false } }
            },
            {
                field: 'IncomeReturnUnLoadUnitPrice', title: '<b>Giá b.x xuống trả về thu</b><br>[IncomeReturnUnLoadUnitPrice]', width: '120px', template: '#=IncomeReturnUnLoadUnitPrice==null?" ":Common.Number.ToMoney(IncomeReturnUnLoadUnitPrice)#',
                filterable: { cell: { operator: 'lte', showOperators: false } }
            },
            {
                field: 'IncomeReturnUnLoadQuantity', title: '<b>Số lượng b.x xuống trả về thu</b><br>[IncomeReturnUnLoadQuantity]', width: '120px', template: '#=IncomeReturnUnLoadQuantity==null?" ":Common.Number.ToNumber5(IncomeReturnUnLoadQuantity)#',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'IncomeReturnUnLoad', title: '<b>Cước b.x xuống trả về thu</b><br>[IncomeReturnUnLoad]', width: '120px', template: '#=IncomeReturnUnLoad==null?" ":Common.Number.ToMoney(IncomeReturnUnLoad)#',
                filterable: { cell: { operator: 'lte', showOperators: false } }
            },
            {
                field: 'IncomeReturnLoadUnitPrice', title: '<b>Giá b.x lên trả về thu</b><br>[IncomeReturnLoadUnitPrice]', width: '120px', template: '#=IncomeReturnLoadUnitPrice==null?" ":Common.Number.ToMoney(IncomeReturnLoadUnitPrice)#',
                filterable: { cell: { operator: 'lte', showOperators: false } }
            },
            {
                field: 'IncomeReturnLoadQuantity', title: '<b>Số lượng b.x lên trả về thu</b><br>[IncomeReturnLoadQuantity]', width: '120px', template: '#=IncomeReturnLoadQuantity==null?" ":Common.Number.ToNumber5(IncomeReturnLoadQuantity)#',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'IncomeReturnLoad', title: '<b>Cước b.x lên trả về thu</b><br>[IncomeReturnLoad]', width: '120px', template: '#=IncomeReturnLoad==null?" ":Common.Number.ToMoney(IncomeReturnLoad)#',
                filterable: { cell: { operator: 'lte', showOperators: false } }
            },
            {
                field: 'IncomeExUnitPrice', title: '<b>Giá phụ thu</b><br>[IncomeExUnitPrice]', width: '120px', template: '#=IncomeExUnitPrice==null?" ":Common.Number.ToMoney(IncomeExUnitPrice)#',
                filterable: { cell: { operator: 'lte', showOperators: false } }
            },
            {
                field: 'IncomeExQuantity', title: '<b>Số lượng phụ thu</b><br>[IncomeExQuantity]', width: '120px', template: '#=IncomeExQuantity==null?" ":Common.Number.ToNumber5(IncomeExQuantity)#',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'IncomeEx', title: '<b>Cước phụ thu</b><br>[IncomeEx]', width: '120px', template: '#=IncomeEx==null?" ":Common.Number.ToMoney(IncomeEx)#',
                filterable: { cell: { operator: 'lte', showOperators: false } }
            },
            {
                field: 'IncomeExCostName', title: '<b>Loại phụ thu</b><br>[IncomeExCostName]', width: '120px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'IncomeExNote', title: '<b>Ghi chú phụ thu</b><br>[IncomeExNote]', width: '120px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'CostUnitPrice', title: '<b>Giá chi</b><br>[CostUnitPrice]', width: '120px', template: '#=CostUnitPrice==null?" ":Common.Number.ToMoney(CostUnitPrice)#',
                filterable: { cell: { operator: 'lte', showOperators: false } }
            },
            {
                field: 'CostQuantity', title: '<b>Số lượng chi</b><br>[CostQuantity]', width: '120px', template: '#=CostQuantity==null?" ":Common.Number.ToNumber5(CostQuantity)#',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'Cost', title: '<b>Cước chi</b><br>[Cost]', width: '120px', template: '#=Cost==null?" ":Common.Number.ToMoney(Cost)#',
                filterable: { cell: { operator: 'lte', showOperators: false } }
            },
            {
                field: 'CostLoadUnitPrice', title: '<b>Giá b.x lên chi</b><br>[CostLoadUnitPrice]', width: '120px', template: '#=CostLoadUnitPrice==null?" ":Common.Number.ToMoney(CostLoadUnitPrice)#',
                filterable: { cell: { operator: 'lte', showOperators: false } }
            },
            {
                field: 'CostLoadQuantity', title: '<b>Số lượng b.x lên chi</b><br>[CostLoadQuantity]', width: '120px', template: '#=CostLoadQuantity==null?" ":Common.Number.ToNumber5(CostLoadQuantity)#',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'CostLoad', title: '<b>Cước b.x lên chi</b><br>[CostLoad]', width: '120px', template: '#=CostLoad==null?" ":Common.Number.ToMoney(CostLoad)#',
                filterable: { cell: { operator: 'lte', showOperators: false } }
            },
            {
                field: 'CostUnLoadUnitPrice', title: '<b>Giá b.x xuống chi</b><br>[CostUnLoadUnitPrice]', width: '120px', template: '#=CostUnLoadUnitPrice==null?" ":Common.Number.ToMoney(CostUnLoadUnitPrice)#',
                filterable: { cell: { operator: 'lte', showOperators: false } }
            },
            {
                field: 'CostUnLoadQuantity', title: '<b>Số lượng b.x xuống chi</b><br>[CostUnLoadQuantity]', width: '120px', template: '#=CostUnLoadQuantity==null?" ":Common.Number.ToNumber5(CostUnLoadQuantity)#',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'CostUnLoad', title: '<b>Cước b.x xuống chi</b><br>[CostUnLoad]', width: '120px', template: '#=CostUnLoad==null?" ":Common.Number.ToMoney(CostUnLoad)#',
                filterable: { cell: { operator: 'lte', showOperators: false } }
            },
            {
                field: 'CostReturnUnitPrice', title: '<b>Giá trả về chi</b><br>[CostReturnUnitPrice]', width: '120px', template: '#=CostReturnUnitPrice==null?" ":Common.Number.ToMoney(CostReturnUnitPrice)#',
                filterable: { cell: { operator: 'lte', showOperators: false } }
            },
            {
                field: 'CostReturnQuantity', title: '<b>Số lượng trả về chi</b><br>[CostReturnQuantity]', width: '120px', template: '#=CostReturnQuantity==null?" ":Common.Number.ToNumber5(CostReturnQuantity)#',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'CostReturn', title: '<b>Cước trả về chi</b><br>[CostReturn]', width: '120px', template: '#=CostReturn==null?" ":Common.Number.ToMoney(CostReturn)#',
                filterable: { cell: { operator: 'lte', showOperators: false } }
            },
            {
                field: 'CostReturnUnLoadUnitPrice', title: '<b>Giá b.x xuống trả về chi</b><br>[CostReturnUnLoadUnitPrice]', width: '120px', template: '#=CostReturnUnLoadUnitPrice==null?" ":Common.Number.ToMoney(CostReturnUnLoadUnitPrice)#',
                filterable: { cell: { operator: 'lte', showOperators: false } }
            },
            {
                field: 'CostReturnUnLoadQuantity', title: '<b>Số lượng b.x xuống trả về chi</b><br>[CostReturnUnLoadQuantity]', width: '120px', template: '#=CostReturnUnLoadQuantity==null?" ":Common.Number.ToNumber5(CostReturnUnLoadQuantity)#',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'CostReturnUnLoad', title: '<b>Cước b.x xuống trả về chi</b><br>[CostReturnUnLoad]', width: '120px', template: '#=CostReturnUnLoad==null?" ":Common.Number.ToMoney(CostReturnUnLoad)#',
                filterable: { cell: { operator: 'lte', showOperators: false } }
            },
            {
                field: 'CostReturnLoadUnitPrice', title: '<b>Giá b.x lên trả về chi</b><br>[CostReturnLoadUnitPrice]', width: '120px', template: '#=CostReturnLoadUnitPrice==null?" ":Common.Number.ToMoney(CostReturnLoadUnitPrice)#',
                filterable: { cell: { operator: 'lte', showOperators: false } }
            },
            {
                field: 'CostReturnLoadQuantity', title: '<b>Số lượng b.x lên trả về chi</b><br>[CostReturnLoadQuantity]', width: '120px', template: '#=CostReturnLoadQuantity==null?" ":Common.Number.ToNumber5(CostReturnLoadQuantity)#',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'CostReturnLoad', title: '<b>Giá b.x lên trả về chi</b><br>[CostReturnLoad]', width: '120px', template: '#=CostReturnLoad==null?" ":Common.Number.ToMoney(CostReturnLoad)#',
                filterable: { cell: { operator: 'lte', showOperators: false } }
            },
            {
                field: 'CostExUnitPrice', title: '<b>Giá phụ chi</b><br>[CostExUnitPrice]', width: '120px', template: '#=CostExUnitPrice==null?" ":Common.Number.ToMoney(CostExUnitPrice)#',
                filterable: { cell: { operator: 'lte', showOperators: false } }
            },
            {
                field: 'CostExQuantity', title: '<b>Số lượng phụ chi</b><br>[CostExQuantity]', width: '120px', template: '#=CostExQuantity==null?" ":Common.Number.ToNumber5(CostExQuantity)#',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'CostEx', title: '<b>Cước phụ chi</b><br>[CostEx]', width: '120px', template: '#=CostEx==null?" ":Common.Number.ToMoney(CostEx)#',
                filterable: { cell: { operator: 'lte', showOperators: false } }
            },
            {
                field: 'CostExCostName', title: '<b>Loại phụ thu chi</b><br>[CostExCostName]', width: '120px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'CostExNote', title: '<b>Ghi chú phụ thu chi</b><br>[CostExNote]', width: '120px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'Note1', title: '<b>Ghi chú</b><br>[Note1]', width: '120px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'TotalIncome', title: '<b>Tổng thu</b><br>[TotalIncome]', width: '120px', template: '#=TotalIncome==null?" ":Common.Number.ToMoney(TotalIncome)#',
                filterable: { cell: { operator: 'lte', showOperators: false } }
            },
            {
                field: 'TotalCost', title: '<b>Tổng chi</b><br>[TotalCost]', width: '120px', template: '#=TotalCost==null?" ":Common.Number.ToMoney(TotalCost)#',
                filterable: { cell: { operator: 'lte', showOperators: false } }
            },
            {
                field: 'TotalPL', title: '<b>PL</b><br>[TotalPL]', width: '120px', template: '#=TotalPL==null?" ":Common.Number.ToMoney(TotalPL)#',
                filterable: { cell: { operator: 'lte', showOperators: false } }
            },
            {
                field: 'OrderCreatedBy', title: '<b>Người tạo đơn</b><br>[OrderCreatedBy]', width: '120px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'OrderCreatedDate', title: '<b>Ngày tạo đơn</b><br>[OrderCreatedDate]', width: '120px', template: '#=Common.Date.FromJsonDDMMYY(OrderCreatedDate)#',
                filterable: { cell: { template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); }, operator: 'equal', showOperators: false } },
            },
            {
                field: 'OrderDateConfig', title: '<b>Ngày tính giá đơn hàng</b><br>[OrderDateConfig]', width: '120px', template: '#=Common.Date.FromJsonDDMMYY(OrderDateConfig)#',
                filterable: { cell: { template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); }, operator: 'equal', showOperators: false } },
            },
            {
                field: 'OrderGroupProductDateConfig', title: '<b>Ngày tính giá c.t đơn hàng</b><br>[OrderGroupProductDateConfig]', width: '120px', template: '#=Common.Date.FromJsonDDMMYY(OrderGroupProductDateConfig)#',
                filterable: { cell: { template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); }, operator: 'equal', showOperators: false } },
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
                field: 'OPSDateConfig', title: '<b>Ngày tính giá chuyến</b><br>[OPSDateConfig]', width: '120px', template: '#=Common.Date.FromJsonDDMMYY(OPSDateConfig)#',
                filterable: { cell: { template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); }, operator: 'equal', showOperators: false } },
            },
            {
                field: 'OPSGroupProductDateConfig', title: '<b>Ngày tính giá c.t chuyến</b><br>[OPSGroupProductDateConfig]', width: '120px', template: '#=Common.Date.FromJsonDDMMYY(OPSGroupProductDateConfig)#',
                filterable: { cell: { template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); }, operator: 'equal', showOperators: false } },
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
                field: 'ETD', title: '<b>Ngày vận chuyển</b><br>[ETD]', width: '120px', template: '#=Common.Date.FromJsonDMYHM(ETD)#',
                filterable: { cell: { template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); }, operator: 'equal', showOperators: false } },
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
            { title: ' ', sortable: false, filterable: false, menu: false }
        ],
        dataBound: function (e) {
            debugger
        },
    };

    $scope.Search_Click = function ($event) {
        $event.preventDefault();

        $rootScope.IsLoading = true;
        Common.Services.Call($http, {
            url: Common.Services.url.REP,
            method: _REPDIPLOrder.URL.Search,
            data: { lstid: $scope.Item.lstCustomerID, dtfrom: $scope.Item.DateFrom, dtto: $scope.Item.DateTo },
            success: function (res) {
                $rootScope.IsLoading = false;

                $scope.REPDIPLOrder_gridOptions.dataSource.data(res);
            },
            error: function (res) {
                $rootScope.IsLoading = false;
            }
        });
    };

    $scope.Excel_Click = function ($event) {
        $event.preventDefault();
        
        var request = Common.Request.CreateFromGrid($scope.REPDIPLOrder_gridOptions.dataSource);

        var functionID = $rootScope.FunctionItem.ID;
        $rootScope.UploadFile({
            IsImage: false, ID: functionID, AllowChange: true, ShowChoose: true,
            Type: Common.CATTypeOfFileCode.TEMPLATEREPORT,
            Complete: function (file) {
                $rootScope.IsLoading = true;
                Common.Services.Call($http, {
                    url: Common.Services.url.REP,
                    method: _REPDIPLOrder.URL.Template,
                    data: { itemfile: file, lstid: $scope.Item.lstCustomerID, dtfrom: $scope.Item.DateFrom, dtto: $scope.Item.DateTo, request: request },
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




    $scope.REPDIPLOrder_ExportExcel_Click = function ($event) {
        $event.preventDefault();

        if (!Common.HasValue($scope.Item.DateFrom) || !Common.HasValue($scope.Item.DateTo)) {
            $rootScope.Message({
                Type: Common.Message.Type.Alert,
                NotifyType: Common.Message.NotifyType.ERROR,
                Title: 'Thông báo',
                Msg: 'Vui lòng chọn Từ ngày- Đến ngày chính xác',
                Close: null,
                Ok: null
            })
        }
        else if ($scope.Item.DateFrom > $scope.Item.DateTo) {
            $rootScope.Message({
                Type: Common.Message.Type.Alert,
                NotifyType: Common.Message.NotifyType.ERROR,
                Title: 'Thông báo',
                Msg: 'Vui lòng chọn Từ ngày nhỏ hơn Đến ngày',
                Close: null,
                Ok: null
            })
        }
        else {
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.REP,
                method: _REPDIPLOrder.URL.Excel_Export,
                data: { lstid: $scope.Item.lstCustomerID, dtfrom: $scope.Item.DateFrom, dtto: $scope.Item.DateTo },
                success: function (res) {
                    $rootScope.IsLoading = false;
                    $rootScope.DownloadFile(res);
                }
            });
        }
    }

    $scope.REPDIPLOrder_ExportExcelMSNNew_Click = function ($event) {
        $event.preventDefault();

        if (!Common.HasValue($scope.Item.DateFrom) || !Common.HasValue($scope.Item.DateTo)) {
            $rootScope.Message({
                Type: Common.Message.Type.Alert,
                NotifyType: Common.Message.NotifyType.ERROR,
                Title: 'Thông báo',
                Msg: 'Vui lòng chọn Từ ngày- Đến ngày chính xác',
                Close: null,
                Ok: null
            })
        }
        else if ($scope.Item.DateFrom > $scope.Item.DateTo) {
            $rootScope.Message({
                Type: Common.Message.Type.Alert,
                NotifyType: Common.Message.NotifyType.ERROR,
                Title: 'Thông báo',
                Msg: 'Vui lòng chọn Từ ngày nhỏ hơn Đến ngày',
                Close: null,
                Ok: null
            })
        }
        else {
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.REP,
                method: _REPDIPLOrder.URL.Excel_ExportNew,
                data: { lstid: $scope.Item.lstCustomerID, dtfrom: $scope.Item.DateFrom, dtto: $scope.Item.DateTo },
                success: function (res) {
                    $rootScope.IsLoading = false;
                    $rootScope.DownloadFile(res);
                }
            });
        }
    }


}]);