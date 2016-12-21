/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />
/// <reference path="~/Scripts/map.js" />
var _PODInput_UploadOrder = {
    URL: {
        Read: "PODDIInput_UploadOrder_List",
        Get: "PODDIInput_UploadOrder_Get",
        Save: 'PODDIInput_UploadOrder_Save',
        LoadData: 'PODDIInput_UploadOrder_GetData',
        ExcelInit: 'PODDIInput_UploadOrder_ExcelInit',
        ExcelChange: 'PODDIInput_UploadOrder_ExcelChange',
        ExcelImport: 'PODDIInput_UploadOrder_ExcelImport',
        ExcelApprove: 'PODDIInput_UploadOrder_ExcelApprove',
    },
    Data: {
        ListCustomer: [],
        ListVendor: [],
        ListGroupProduct: {},
        ListProduct: {},
        ListPartner: {},
        ListStock: {},
        ListPartnerLocation: {},
        ListVehicle: {},
        ListDriver: [],
        ListGOPInStock: {},
        
    },
    ExcelKey: {
        Resource: "PODInput_UpLoadOrder_Excel",
        UPLOAD_Order: "PODInput_UpLoadOrder"
    }
}

angular.module('myapp').controller('PODInput_UploadOrderCtrl', ['$rootScope', '$scope', '$http', '$location', '$state', '$timeout', 'openMapV2', '$compile', '$interval', function ($rootScope, $scope, $http, $location, $state, $timeout, openMapV2, $compile, $interval) {
    Common.Log('PODInput_UploadOrderCtrl');
    $rootScope.IsLoading = false;

    if ($rootScope.IsPageComplete != true) return;
    $scope.Auth = $rootScope.GetAuth();

    $scope.ItemSearch = {
        DateFrom: (new Date()).addDays(-7),
        DateTo: (new Date()),
    }

    $scope.Item = null;

    $scope.BackToIndex_Click = function ($event) {
        $event.preventDefault();
        $state.go("main.PODInput.Index");
    };

    $scope.Close_Click = function ($event, win) {
        $event.preventDefault();
        win.close();
    };

    $scope.Search_Click = function ($event, grid) {
        $event.preventDefault();
        if (!Common.HasValue($scope.ItemSearch.DateFrom) || !Common.HasValue($scope.ItemSearch.DateTo)) {
            $rootScope.Message({
                Type: Common.Message.Type.Alert,
                NotifyType: Common.Message.NotifyType.ERROR,
                Title: 'Thông báo',
                Msg: 'Vui lòng chọn Từ ngày- Đến ngày chính xác',
                Close: null,
                Ok: null
            })
        }
        else if ($scope.ItemSearch.DateFrom > $scope.ItemSearch.DateTo) {
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
            grid.dataSource.read();
        }
    };

    $scope.Excel_Click = function ($event, grid) {
        $event.preventDefault();

        var lstMessageError = [];
        for (var i = 0; i < 32; i++) {
            var resource = $rootScope.RS[_PODInput_UploadOrder.ExcelKey.Resource + '_' + i];
            if (Common.HasValue(resource))
                lstMessageError.push(resource);
        }

        if (lstMessageError.length == 0) {
            lstMessageError = [
                '[Mã đơn hàng] không được trống và > 50 ký tự',
                '[Mã khách hàng] không được trống',
                '[Mã khách hàng] không tồn tại',
                '[Ngày gửi y/c] nhập sai ({0}_{1})',
                '[Loại vận chuyển] không được trống',
                '[Loại vận chuyển] không chính xác',
                '[Mã kho] không được trống',
                '[Mã kho] không chính xác với khách hàng đã chọn',
                '[Mã nhóm hàng] không được trống',
                '[Mã nhóm hàng] không chính xác với khách hàng đã chọn',
                '[Mã nhóm hàng] không tồn tại trong kho đã chọn',
                '[Mã hàng] không được trống',
                '[Mã hàng] không tồn tại trong nhóm hàng đã chọn',
                '[Nhà phân phối] không được trống',
                '[Nhà phân phối] không tồn tại trong khách hàng đã chọn',
                '[Mã điểm giao] không được trống',
                '[Mã điểm giao] không tồn tại trong nhà phân phối đã chọn',
                '[ATD] nhập sai ({0}_{1})',
                '[ATA] nhập sai ({0}_{1})',
                '[Số lượng] nhập sai ({0}_{1})',
                '[Tấn] nhập sai ({0}_{1})',
                '[CBM] nhập sai ({0}_{1})',
                '[Nhà vận tải] không được trống',
                '[Nhà vận tải] không tồn tại trong hệ thống',
                '[Số xe] không được trống',
                '[Số xe] không phải xe nhà',
                '[Số xe] không chính xác với nhà vận tải',
                '[Tài xế 1] không được trống',
                '[Tài xế 1] không phải tài xế nhà',
                '[Tài xế 2] không phải tài xế nhà',
                '[Số chứng từ] không được > 1000 ký tự',
                '[Loại chứng từ] không được > 500 ký tự',
            ];
        }

        $rootScope.excelShare.Init({
            functionkey: _PODInput_UploadOrder.ExcelKey.UPLOAD_Order,
            params: {
                'dtFrom': $scope.ItemSearch.DateFrom,
                'dtTo': $scope.ItemSearch.DateTo,
            },
            rowStart: 1,
            colCheckChange: 24,
            url: Common.Services.url.POD,
            methodInit: _PODInput_UploadOrder.URL.ExcelInit,
            methodChange: _PODInput_UploadOrder.URL.ExcelChange,
            methodImport: _PODInput_UploadOrder.URL.ExcelImport,
            methodApprove: _PODInput_UploadOrder.URL.ExcelApprove,
            lstMessageError: lstMessageError,

            Changed: function () {

            },
            Approved: function () {
                grid.dataSource.read();
            }
        });
    };

    $scope.main_gridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.POD,
            method: _PODInput_UploadOrder.URL.Read,
            readparam: function () {
                return {
                    'dtFrom': $scope.ItemSearch.DateFrom,
                    'dtTo': $scope.ItemSearch.DateTo,
                    // 'listCustomerID': $scope.ItemSearch.ListCustomerID
                };
            },
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    RequestDate: { type: 'date' },
                    ETD: { type: 'date' },
                    ETA: { type: 'date' },
                    Quantity: { type: 'number' },
                    Ton: { type: 'number' },
                    CBM: { type: 'number' },
                }
            }
        }),
        height: '100%', pageable: true, sortable: true, columnMenu: false, filterable: { mode: 'row' }, resizable: true, editable: false,
        columns: [
            {
                field: 'F_command', title: ' ', width: '70px',
                template: '<a href="/" ng-click="Edit_Click($event,dataItem,Detail_win)" class="k-button"><i class="fa fa-pencil"></i></a>',
                filterable: false, sortable: false
            },
            { field: 'OrderCode', title: 'Mã đơn hàng', width: '125px', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'CustomerCode', title: 'Mã KH', width: '125px', filterable: { cell: { operator: 'contains', showOperators: false } } },
            {
                field: 'RequestDate', title: 'Ngày gửi y/c', width: '145px', template: "#=Common.Date.FromJsonDMY(RequestDate)#",
                filterable: { cell: { template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); }, operator: 'equal', showOperators: false } },
            },
            { field: 'TransportModeName', title: 'Loại v/c', width: 50, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'StockCode', title: 'Kho', width: 100, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'GroupOfProductCode', title: 'Mã nhóm hàng', width: 100, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'GroupOfProductName', title: 'Nhóm hàng', width: 100, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'ProductCode', title: 'Mã hàng', width: 100, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'ProductName', title: 'Hàng hóa', width: 100, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'PartnerCode', title: 'Nhà phân phối', width: 100, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'LocationToCode', title: 'Mã điểm giao', width: 100, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'LocationToAddress', title: 'Địa chỉ đ.giao', width: 100, filterable: { cell: { operator: 'contains', showOperators: false } } },
            {
                field: 'ETD', title: 'ETD', width: 120, template: "#=Common.Date.FromJsonDMY(ETD)#",
                filterable: { cell: { template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); }, operator: 'equal', showOperators: false } },
            },
            {
                field: 'ETA', title: 'ETA', width: 120, template: "#=Common.Date.FromJsonDMY(ETA)#",
                filterable: { cell: { template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); }, operator: 'equal', showOperators: false } },
            },
            {
                field: 'Quantity', title: 'Số lượng', width: 120, template: "#=Common.Number.ToNumber6(Quantity)#",
                filterable: { cell: { template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); }, operator: 'equal', showOperators: false } },
            },
            {
                field: 'Ton', title: 'Tấn', width: 120, template: "#=Common.Number.ToNumber6(Ton)#",
                filterable: { cell: { template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); }, operator: 'equal', showOperators: false } },
            },
            {
                field: 'CBM', title: 'CBM', width: 120, template: "#=Common.Number.ToNumber6(CBM)#",
                filterable: { cell: { template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); }, operator: 'equal', showOperators: false } },
            },
            { field: 'VendorCode', title: 'Nhà vận tải', width: 100, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'VehicleCode', title: 'Số xe', width: 100, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'DriverName1', title: 'Tài xế 1', width: 100, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'DriverName2', title: 'Tài xế 2', width: 100, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'InvoiceNo', title: 'Số chứng từ', width: 100, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'Note1', title: 'Loại chứng từ', width: 100, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { title: ' ', filterable: false, sortable: false }
        ]
    }

    //#region popup
    $scope.cboCustomer_Options = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true, dataTextField: 'Code', dataValueField: 'ID',
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    Code: { type: 'string' },
                }
            }
        }),
        change: function (e) {
            if (e.sender.selectedIndex >= 0) {
                $scope.Item.StockID = -1;
                $scope.Item.GroupOfProductID = -1;
                $scope.Item.ProductID = -1;
                $scope.Item.PartnerID = -1;
                $scope.Item.LocationToID = -1;
                $scope.LoadDataWithCus($scope.Item)
            }
            else {
                $scope.Item.CustomerID = "";
                $scope.Item.GroupOfProductID = -1;
                $scope.Item.ProductID = -1;
                $scope.Item.PartnerID = -1;
                $scope.Item.LocationToID = -1;
                $scope.LoadDataWithCus($scope.Item)
            }
        }
    }
    $scope.cboTransport_Options = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true, dataTextField: 'ValueOfVar', dataValueField: 'ID',
        dataSource: Common.DataSource.Local({
            data: [
                { ValueOfVar: 'FTL', ID: 33 },
                { ValueOfVar: 'LTL', ID: 34 },
            ],
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    ValueOfVar: { type: 'string' },
                }
            }
        })
    }

    $scope.cboStock_Options = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true, dataTextField: 'Code', dataValueField: 'ID',
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    Code: { type: 'string' },
                }
            }
        }),
        change: function (e) {
            if (e.sender.selectedIndex >= 0) {
                $scope.Item.GroupOfProductID = -1;
                $scope.Item.ProductID = -1;
                $scope.LoadDataWithStock($scope.Item)
            }
            else {
                $scope.Item.StockID = "";
                $scope.Item.GroupOfProductID = "";
                $scope.Item.ProductID = "";
                $scope.LoadDataWithStock($scope.Item)
            }
        }
    }

    $scope.cboPartner_Options = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true, dataTextField: 'PartnerName', dataValueField: 'ID',
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    PartnerName: { type: 'string' },
                }
            }
        }),
        change: function (e) {
            if (e.sender.selectedIndex >= 0) {
                $scope.Item.LocationToID = -1;
                $scope.LoadDataWithPartner($scope.Item)
            }
            else {
                $scope.Item.PartnerID = "";
                $scope.Item.LocationToID = "";
                $scope.LoadDataWithPartner($scope.Item)
            }
        }
    }

    $scope.cboLocationTo_Options = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true, dataTextField: 'Code', dataValueField: 'ID',
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    Code: { type: 'string' },
                }
            }
        })
    }

    $scope.cboGroupOfProduct_Options = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true, dataTextField: 'GroupOfProductName', dataValueField: 'GroupOfProductID',
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'GroupOfProductID',
                fields: {
                    GroupOfProductID: { type: 'number' },
                    GroupOfProductName: { type: 'string' },
                }
            }
        }),
        change: function (e) {
            if (e.sender.selectedIndex >= 0) {
                $scope.Item.ProductID = -1;
                $scope.LoadDataWithGOP($scope.Item)
            }
            else {
                $scope.Item.GroupOfProductID = "";
                $scope.Item.ProductID = "";
                $scope.LoadDataWithGOP($scope.Item)
            }
        }
    }
    $scope.cboProduct_Options = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true, dataTextField: 'ProductName', dataValueField: 'ID',
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    ProductName: { type: 'string' },
                }
            }
        })
    }
    $scope.cboVendor_Options = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true, dataTextField: 'CustomerName', dataValueField: 'ID',
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    CustomerName: { type: 'string' },
                }
            }
        }),
        change: function (e) {
            if (e.sender.selectedIndex >= 0) {
                $scope.Item.VehicleID = -1;
                $scope.Item.DriverName1 = "";
                $scope.Item.DriverName2 = "";
                $scope.LoadDataWithVen($scope.Item)
            }
            else {
                $scope.Item.VendorID = "";
                $scope.Item.VehicleID = "";
                $scope.Item.DriverName1 = "";
                $scope.Item.DriverName2 = "";
                $scope.LoadDataWithVen($scope.Item)
            }
        }
    }
    $scope.cboVehicle_Options = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true, dataTextField: 'RegNo', dataValueField: 'VehicleID',
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'VehicleID',
                fields: {
                    VehicleID: { type: 'number' },
                    RegNo: { type: 'string' },
                }
            }
        })
    }
    $scope.numTon_Options = { format: 'n2', spinners: false, culture: 'en-US', min: 0, step: 1, decimals: 2, }
    $scope.numCBM_Options = { format: 'n2', spinners: false, culture: 'en-US', min: 0, step: 1, decimals: 2, }
    $scope.numQuantity_Options = { format: 'n0', spinners: false, culture: 'en-US', min: 0, step: 1, decimals: 0, }

    $scope.atcDriver_Options = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true, placeholder: "Họ tên", dataTextField: "DriverName",
        dataSource: Common.DataSource.Local({
            data: [],
        })
    }

    $scope.Add_Click = function ($event, win) {
        $event.preventDefault();
        $rootScope.IsLoading = true;
        Common.Services.Call($http, {
            url: Common.Services.url.POD,
            method: _PODInput_UploadOrder.URL.Get,
            data: { id: 0 },
            success: function (res) {

                $scope.Item = res;
                $scope.LoadDataWithCus($scope.Item);
                $scope.LoadDataWithVen($scope.Item);
                $rootScope.IsLoading = false;
                win.center();
                win.open();
            },
            error: function (res) {
                $rootScope.IsLoading = false;
            }
        });
    };

    $scope.Save_Click = function ($event, vform, win) {
        $event.preventDefault();
        if (vform()) {
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.POD,
                method: _PODInput_UploadOrder.URL.Save,
                data: { item: $scope.Item },
                success: function (res) {
                    $scope.main_grid.dataSource.read();
                    $rootScope.IsLoading = false;
                    $rootScope.Message({ Msg: 'Thành công', Type: Common.Message.Type.Notify, NotifyType: Common.Message.NotifyType.SUCCESS });
                    win.close();

                },
                error: function (res) {
                    $rootScope.IsLoading = false;
                }
            });
        }
    };

    $scope.Edit_Click = function ($event, data,win) {
        $event.preventDefault();
        $rootScope.IsLoading = true;
        Common.Services.Call($http, {
            url: Common.Services.url.POD,
            method: _PODInput_UploadOrder.URL.Get,
            data: { id: data.ID },
            success: function (res) {

                $scope.Item = res;
                $scope.LoadDataWithCus($scope.Item);
                $scope.LoadDataWithVen($scope.Item);
                $rootScope.IsLoading = false;
                win.center();
                win.open();
            },
            error: function (res) {
                $rootScope.IsLoading = false;
            }
        });
    };

    //#endregion

    //#region getdata
    $rootScope.IsLoading = true;
    Common.Services.Call($http, {
        url: Common.Services.url.POD,
        method: _PODInput_UploadOrder.URL.LoadData,
        data: {},
        success: function (res) {

            $scope.cboCustomer_Options.dataSource.data(res.ListCustomer);
            _PODInput_UploadOrder.Data.ListCustomer = res.ListCustomer;
            _PODInput_UploadOrder.Data.ListGroupProduct = {};
            _PODInput_UploadOrder.Data.ListProduct = {};
            _PODInput_UploadOrder.Data.ListPartner = {};
            _PODInput_UploadOrder.Data.ListStock = {};
            _PODInput_UploadOrder.Data.ListPartnerLocation = {};

            Common.Data.Each(res.ListGroupProduct, function (o) {
                if (!Common.HasValue(_PODInput_UploadOrder.Data.ListGroupProduct[o.StockID]))
                    _PODInput_UploadOrder.Data.ListGroupProduct[o.StockID] = [o];
                else _PODInput_UploadOrder.Data.ListGroupProduct[o.StockID].push(o);
            })

            Common.Data.Each(res.ListProduct, function (o) {
                if (!Common.HasValue(_PODInput_UploadOrder.Data.ListProduct[o.GroupOfProductID]))
                    _PODInput_UploadOrder.Data.ListProduct[o.GroupOfProductID] = [o];
                else _PODInput_UploadOrder.Data.ListProduct[o.GroupOfProductID].push(o);
            })

            Common.Data.Each(res.ListPartner, function (o) {
                if (!Common.HasValue(_PODInput_UploadOrder.Data.ListPartner[o.CustomerID]))
                    _PODInput_UploadOrder.Data.ListPartner[o.CustomerID] = [o];
                else _PODInput_UploadOrder.Data.ListPartner[o.CustomerID].push(o);
            })

            Common.Data.Each(res.ListStock, function (o) {
                if (!Common.HasValue(_PODInput_UploadOrder.Data.ListStock[o.CustomerID]))
                    _PODInput_UploadOrder.Data.ListStock[o.CustomerID] = [o];
                else _PODInput_UploadOrder.Data.ListStock[o.CustomerID].push(o);
            })


            Common.Data.Each(res.ListPartnerLocation, function (o) {
                if (!Common.HasValue(_PODInput_UploadOrder.Data.ListPartnerLocation[o.CusPartID]))
                    _PODInput_UploadOrder.Data.ListPartnerLocation[o.CusPartID] = [o];
                else _PODInput_UploadOrder.Data.ListPartnerLocation[o.CusPartID].push(o);
            })

            $scope.cboVendor_Options.dataSource.data(res.ListVendor);
            _PODInput_UploadOrder.Data.ListVendor = res.ListVendor;
            _PODInput_UploadOrder.Data.ListVehicle = {};

            Common.Data.Each(res.ListVehicle, function (o) {
                if (!Common.HasValue(_PODInput_UploadOrder.Data.ListVehicle[o.CurrentVendorID]))
                    _PODInput_UploadOrder.Data.ListVehicle[o.CurrentVendorID] = [o];
                else _PODInput_UploadOrder.Data.ListVehicle[o.CurrentVendorID].push(o);
            })

            _PODInput_UploadOrder.Data.ListDriver = {};
            Common.Data.Each(res.ListDriver, function (o) {
                if (!Common.HasValue(_PODInput_UploadOrder.Data.ListDriver[o.CustomerID]))
                    _PODInput_UploadOrder.Data.ListDriver[o.CustomerID] = [o];
                else _PODInput_UploadOrder.Data.ListDriver[o.CustomerID].push(o);
            })
            $rootScope.IsLoading = false;

        },
        error: function (res) {
            $rootScope.IsLoading = false;
        }
    });

    $scope.LoadDataWithCus = function (item) {
        Common.Log("LoadDataWithCus");
        var cusID = item.CustomerID;
        var stockid = item.StockID;
        var partnerid = item.PartnerID;
        var locationid = item.LocationToID;
        var groupproductid = item.GroupOfProductID;
        var productid = item.ProductID;

        var data = _PODInput_UploadOrder.Data.ListStock[cusID];
        $scope.cboStock_Options.dataSource.data(data);
        if (Common.HasValue(stockid)) {
            if (stockid < 0) {
                if (Common.HasValue(data) && data.length > 0)
                    stockid = data[0].ID;
                else stockid = "";
            }
        }
        $timeout(function () {
            item.StockID = stockid;
        }, 1)

        data = _PODInput_UploadOrder.Data.ListGroupProduct[stockid];
        $scope.cboGroupOfProduct_Options.dataSource.data(data);
        if (Common.HasValue(groupproductid)) {
            if (groupproductid < 0) {
                if (Common.HasValue(data) && data.length > 0)
                    groupproductid = data[0].GroupOfProductID;
                else groupproductid = "";
            }
        }
        $timeout(function () {
            item.GroupOfProductID = groupproductid;
        }, 1)

        data = _PODInput_UploadOrder.Data.ListProduct[groupproductid];
        $scope.cboProduct_Options.dataSource.data(data);
        if (Common.HasValue(productid)) {
            if (productid < 0) {
                if (Common.HasValue(data) && data.length > 0)
                    productid = data[0].ID;
                else productid = "";
            }
        }
        $timeout(function () {
            item.ProductID = productid;
        }, 1)

        data = _PODInput_UploadOrder.Data.ListPartner[cusID];
        $scope.cboPartner_Options.dataSource.data(data);
        if (Common.HasValue(partnerid)) {
            if (partnerid < 0) {
                if (Common.HasValue(data) && data.length > 0)
                    partnerid = data[0].ID;
                else partnerid = "";
            }
        }
        $timeout(function () {
            item.PartnerID = partnerid;
        }, 1)

        data = _PODInput_UploadOrder.Data.ListPartnerLocation[partnerid];
        $scope.cboLocationTo_Options.dataSource.data(data);
        if (Common.HasValue(locationid)) {
            if (locationid < 0) {
                if (Common.HasValue(data) && data.length > 0)
                    locationid = data[0].ID;
                else locationid = "";
            }
        }
        $timeout(function () {
            item.LocationToID = locationid;
        }, 1)


    }

    $scope.LoadDataWithGOP = function (item) {
        Common.Log("LoadDataWithGOP");
        var groupproductid = item.GroupOfProductID;
        var productid = item.ProductID;
        data = _PODInput_UploadOrder.Data.ListProduct[groupproductid];
        $scope.cboProduct_Options.dataSource.data(data);
        if (Common.HasValue(productid)) {
            if (productid < 0) {
                productid = data[0].ID;
            }
        }
        $timeout(function () {
            item.ProductID = productid;
        }, 1)
    }

    $scope.LoadDataWithVen = function (item) {
        Common.Log("LoadDataWithVen");
        var vendorid = item.VendorID;
        var vehicleid = item.VehicleID;
        var driver1 = item.DriverName1;
        var driver2 = item.DriverName2;

        var data = _PODInput_UploadOrder.Data.ListVehicle[vendorid];
        $scope.cboVehicle_Options.dataSource.data(data);
        if (Common.HasValue(vehicleid)) {
            if (vehicleid < 0) {
                if (Common.HasValue(data) && data.length > 0)
                    vehicleid = data[0].VehicleID;
                else vehicleid = "";
            }
        }
        $timeout(function () {
            item.VehicleID = vehicleid;
        }, 1)

        data = _PODInput_UploadOrder.Data.ListDriver[vendorid];
        $scope.atcDriver_Options.dataSource.data(data)
        item.DriverName1 = "";
        item.DriverName2 = "";
    }

    $scope.LoadDataWithPartner = function (item) {
        Common.Log("LoadDataWithPartner");
        var partnerid = item.PartnerID;
        var locationid = item.LocationToID;

        var data = _PODInput_UploadOrder.Data.ListPartnerLocation[partnerid];
        $scope.cboLocationTo_Options.dataSource.data(data);
        if (Common.HasValue(locationid)) {
            if (locationid < 0) {
                locationid = data[0].ID;
            }
        }
        $timeout(function () {
            item.LocationToID = locationid;
        }, 1)
    }

    $scope.LoadDataWithStock = function (item) {
        Common.Log("LoadDataWithStock");
        var stockid = item.StockID;
        var groupid = item.GroupOfProductID;
        var productid = item.ProductID;

        var data = _PODInput_UploadOrder.Data.ListGroupProduct[stockid];
        $scope.cboGroupOfProduct_Options.dataSource.data(data);
        if (Common.HasValue(groupid)) {
            if (groupid < 0) {
                groupid = data[0].GroupOfProductID;
            }
        }

        $timeout(function () {
            item.GroupOfProductID = groupid;
        }, 1)

        data = _PODInput_UploadOrder.Data.ListProduct[groupid];
        $scope.cboProduct_Options.dataSource.data(data)
        if (Common.HasValue(productid)) {
            if (productid < 0) {
                productid = data[0].ID;
            }
        }
        $timeout(function () {
            item.ProductID = productid;
        }, 1)
    }

    //#endregion

}])