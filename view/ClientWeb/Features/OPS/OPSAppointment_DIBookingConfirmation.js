/// <reference path="~/Scripts/jquery-1.9.1.intellisense.js" />
/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />

//#region OPS object
var _OPSAppointment_DIBookingConfirmation = {
    URL: {
        Read: 'OPS_DIAppointment_BookingConfirmation_Read',
        Excel: 'OPS_DIAppointment_BookingConfirmation_Excel',
    },
    Data: {
        CookieSearch: 'OPS_DIAppointment_BookingConfirmation_Cookie'
    }
};

angular.module('myapp').controller('OPSAppointment_DIBookingConfirmationCtrl', ['$rootScope', '$scope', '$http', '$location', '$state', '$timeout', function ($rootScope, $scope, $http, $location, $state, $timeout) {
    Common.Log('OPSAppointment_DIBookingConfirmationCtrl');
    $rootScope.IsLoading = false;
    $scope.HasChoose = false;
    $scope.Search = {}; 

    $scope.cboCustomerOptions = {
        autoBind: true, index: 0,
        valuePrimitive: true,
        dataTextField: 'CustomerName',
        dataValueField: 'ID',
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
    };

    // Load danh sách khách hàng
    Common.Services.Call($http, {
        url: Common.Services.url.OPS,
        method: _OPSAppointment_DIRouteTOMaster.URL.CustomerList,
        data: {},
        success: function (res) {
            if (Common.HasValue(res)) {
                $timeout(function () {
                    $scope.cboCustomerOptions.dataSource.data(res);
                    $scope.Init_LoadCookie();
                }, 1);
            }
        }
    });

    $scope.Init_LoadCookie = function () {
        Common.Log('Init_LoadCookie');
        var CustomerID = null;
        var strCookie = Common.Cookie.Get(_OPSAppointment_DIBookingConfirmation.Data.CookieSearch);
        if (Common.HasValue(strCookie) && strCookie != '') {
            try {
                var objCookie = eval('[' + strCookie + ']')[0];
                $scope.Search.DateFrom = Common.Date.FromJson(objCookie.DateFrom);
                $scope.Search.DateTo = Common.Date.FromJson(objCookie.DateTo);
                if (Common.HasValue(objCookie.CustomerID)) {
                    CustomerID = objCookie.CustomerID;
                }
            } catch (e) { }
        }
        if (!Common.HasValue($scope.Search.DateFrom) || !Common.HasValue($scope.Search.DateTo)) {
            $scope.Search.DateFrom = new Date().addDays(-3);
            $scope.Search.DateTo = new Date().addDays(3);
            $scope.Search.CustomerID = null;
        }

        $timeout(function () {
            $scope.Search.CustomerID = CustomerID;
            Common.Cookie.Set(_OPSAppointment_DIBookingConfirmation.Data.CookieSearch, JSON.stringify($scope.Search));
            $scope.OPSAppointment_DIBookingConfirmation_gridOptions.dataSource.read();
        }, 10);
    };

    $scope.OPSAppointment_DIBookingConfirmation_gridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.OPS,
            method: _OPSAppointment_DIBookingConfirmation.URL.Read,
            readparam: function () { return { customerID: $scope.Search.CustomerID == null ? 0 : $scope.Search.CustomerID, dtfrom: $scope.Search.DateFrom, dtto: $scope.Search.DateTo } },
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    ETD: { type: 'date' },
                    ETA: { type: 'date' },
                    RequestDate: { type: 'date' },
                }
            }
        }),
        height: '100%', pageable: true, sortable: true, columnMenu: false, resizable: true, reorderable: true, filterable: { mode: 'row' },
        toolbar: kendo.template($('#OPSAppointment_DIBookingConfirmation_gridToolbar').html()),
        columns: [
            {
                field: 'TOMasterCode', title: '<b>Số chuyến</b><br>[TOMasterCode]', width: 160,
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'VehicleCode', title: '<b>Số xe</b><br>[VehicleCode]', width: 160,
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'DriverName', title: '<b>Tài xế</b><br>[DriverName]', width: 160,
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'ETD', title: '<b>Ngày đến kho</b><br>[ETD]', width: 160,
                template: "#=ETD==null?' ':Common.Date.FromJsonDMYHMS(ETD)#",
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'ETA', title: '<b>Ngày giao hàng</b><br>[ETA]', width: 160,
                template: "#=ETA==null?' ':Common.Date.FromJsonDMYHMS(ETA)#",
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'RequestDate', title: '<b>Ngày gửi đơn hàng</b><br>[RequestDate]', width: 150,
                template: "#=RequestDate==null?' ':Common.Date.FromJsonDMYHMS(RequestDate)#",
                filterable: { cell: { showOperators: false, operator: "gte" } }
            },
            {
                field: 'OrderCode', title: '<b>Mã đơn hàng</b><br>[OrderCode]', width: 150,
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'SOCode', title: '<b>Số SO</b><br>[SOCode]', width: 150,
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'DNCode', title: '<b>Số DN</b><br>[DNCode]', width: 150,
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'GroupOfProductName', title: '<b>Hàng hóa</b><br>[GroupOfProductName]', width: 150,
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'Ton', title: '<b>Tấn y/cầu</b><br>[Ton]', width: 150, format: "{0:n5}",
                filterable: { cell: { operator: 'eq', showOperators: false } }
            },
            {
                field: 'CBM', title: '<b>Khối y/cầu</b><br>[CBM]', width: 150, format: "{0:n5}",
                filterable: { cell: { operator: 'eq', showOperators: false } }
            },
            {
                field: 'Quantity', title: '<b>S.Lượng y/cầu</b><br>[Quantity]', width: 150, format: "{0:n5}",
                filterable: { cell: { operator: 'eq', showOperators: false } }
            },
            {
                field: 'TonTransfer', title: '<b>Tấn chở</b><br>[TonTransfer]', width: 150, format: "{0:n5}",
                filterable: { cell: { operator: 'eq', showOperators: false } }
            },
            {
                field: 'CBMTransfer', title: '<b>Khối chở</b><br>[CBMTransfer]', width: 150, format: "{0:n5}",
                filterable: { cell: { operator: 'eq', showOperators: false } }
            },
            {
                field: 'QuantityTransfer', title: '<b>S.Lượng chở</b><br>[QuantityTransfer]', width: 150, format: "{0:n5}",
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'Description', title: '<b>Ghi chú</b><br>[Description]', width: 150,
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'Note1', title: '<b>Ghi chú 1</b><br>[Note1]', width: 150,
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'Note2', title: '<b>Ghi chú 2</b><br>[Note2]', width: 150,
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'StockCode', title: '<b>Mã kho</b><br>[StockCode]', width: 150,
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'StockName', title: '<b>Kho lấy hàng</b><br>[StockName]', width: 150,
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'StockAddress', title: '<b>Địa chỉ lấy hàng</b><br>[StockAddress]', width: 150,
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'StockProvinceName', title: '<b>Tỉnh lấy hàng</b><br>[StockProvinceName]', width: 150,
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'StockDistrictName', title: '<b>Quận lấy hàng</b><br>[StockDistrictName]', width: 150,
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'PartnerCode', title: '<b>Mã điểm giao</b><br>[PartnerCode]', width: 150,
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'PartnerName', title: '<b>Tên điểm giao</b><br>[PartnerName]', width: 150,
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'PartnerAddress', title: '<b>Địa chỉ giao</b><br>[PartnerAddress]', width: 150,
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'PartnerProvinceName', title: '<b>Tỉnh giao hàng</b><br>[PartnerProvinceName]', width: 150,
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'PartnerDistrictName', title: '<b>Quận giao hàng</b><br>[PartnerDistrictName]', width: 150,
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'RoutingCode', title: '<b>Mã cung đường</b><br>[RoutingCode]', width: 150,
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'RoutingName', title: '<b>Cung đường</b><br>[RoutingName]', width: 150,
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },

            { title: '', filterable: false, sortable: false }
        ]
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

    $scope.HideSetting = function ($event) {
        $event.preventDefault();

        $rootScope.HideSetting();
    }
    //#endregion
    
    $scope.Close_Click = function ($event) {
        $event.preventDefault();

        $state.go("main.OPSAppointment.DI");
    };

    $scope.Search_Click = function ($event) {
        $event.preventDefault();

        $scope.OPSAppointment_DIBookingConfirmation_gridOptions.dataSource.read();
    };

    $scope.Excel_Click = function ($event) {
        $event.preventDefault();

        var functionID = $rootScope.FunctionID;
        $rootScope.UploadFile({
            IsImage: false, ID: functionID, AllowChange: true, ShowChoose: true,
            Type: Common.CATTypeOfFileCode.TEMPLATEREPORT,
            Complete: function (file) {
                $rootScope.IsLoading = true;
                Common.Services.Call($http, {
                    url: Common.Services.url.OPS,
                    method: _OPSAppointment_DIBookingConfirmation.URL.Excel,
                    data: { itemfile: file, customerID: $scope.Search.CustomerID, dtfrom: $scope.Search.DateFrom, dtto: $scope.Search.DateTo },
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
}]);
