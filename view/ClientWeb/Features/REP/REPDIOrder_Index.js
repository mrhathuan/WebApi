/// <reference path="~/Scripts/jquery-1.9.1.intellisense.js" />
/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />
var _REPDIOrder = {
    URL: {

        Search: 'REPDIStatus_OrderData',
        Template: 'REPDIStatus_OrderTemplate',

        Read_Customer: 'REP_Customer_Read',

        REPDIStatus_Export: 'REPDIStatus_Export',

        SettingList: 'CUSSettingsReport_List',
        SettingSave: 'CUSSettingsReport_Save',
        SettingDelete: 'CUSSettingsReport_Delete',
        SettingTemplate: 'CUSSettingsReport_Template',

        SettingCusDeleteList: 'CUSSettingReport_CustomerDeleteList',
        SettingCusNotInList: 'CUSSettingReport_CustomerNotInList',
        SettingCusNotInSave: 'CUSSettingReport_CustomerNotInSave',

        SettingAction: 'REPDIStatusOrder_SettingDownload',
    },
    Data: {
        ListFilter: [
            { ID: 1, ValueName: 'Ngày gửi yêu cầu' },
            { ID: 2, ValueName: 'Ngày tính giá' },
        ],
        ListStatus: [
            { ID: 0, ValueName: 'Tất cả' },
            { ID: 1, ValueName: 'Đang lập kế hoạch' },
            { ID: 2, ValueName: 'Đã lập chuyến' },
            { ID: 3, ValueName: 'Đã duyệt chuyến' },
            { ID: 4, ValueName: 'Đang vận chuyển' },
            { ID: 5, ValueName: 'Đã giao hàng' },
            { ID: 6, ValueName: 'Đã nhận chứng từ' },
            { ID: 7, ValueName: 'Bị hủy' }, ],
    },
}

angular.module('myapp').controller('REPDIOrder_IndexCtrl', ['$rootScope', '$scope', '$http', '$location', '$state', '$timeout', '$stateParams', function ($rootScope, $scope, $http, $location, $state, $timeout, $stateParams) {
    if ($rootScope.IsPageComplete != true) return;
    Common.Log('REPDIOrder_IndexCtrl');
    $rootScope.IsLoading = false;

    $scope.Auth = $rootScope.GetAuth();
    $scope.SettingItem = { ID: 0 };
    $scope.Item = {
        StatusID: 0,
        TypeOfFilterID: 1,
        lstCustomerID: [],
        DateFrom: Common.Date.AddDay(new Date(), -5),
        DateTo: new Date(),
    }

    $scope.mulCustomer_Options = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', placeholder: 'Chọn khách hàng',
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
        method: _REPDIOrder.URL.Read_Customer,
        data: {},
        success: function (res) {
            $scope.mulCustomer_Options.dataSource.data(res.Data);
        }
    });

    $scope.cboCustomer_Options = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true, dataTextField: 'CustomerName', dataValueField: 'ID',
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    TypeName: { type: 'string' },
                }
            }
        }),
        change: function (e) {
        }
    }
    $scope.REPDIStatus_cboFilter_Options = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: "contains",
        suggest: true, dataTextField: 'ValueName', dataValueField: 'ID',
        dataSource: _REPDIOrder.Data.ListFilter,
        change: function (e) {
        }
    }

    $scope.REPDIStatus_cbostatusID_Options = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: "contains",
        suggest: true, dataTextField: 'ValueName', dataValueField: 'ID',
        dataSource: _REPDIOrder.Data.ListStatus,
        change: function (e) {
        }
    }

    $scope.REPDIStatus_gridOptions = {
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number', editable: false },
                    RequestDate: { type: 'date' },
                    DateConfig: { type: 'date' },
                }
            },
            pageSize: 20
        }),
        height: '100%', pageable: true, sortable: { mode: 'multiple', allowUnsort: true }, columnMenu: false, resizable: true, reorderable: false, filterable: { mode: 'row' }, selectable: 'row',
        columns: [
            {
                field: 'OrderStatus', title: '<b>Trạng thái đơn hàng</b><br>[OrderStatus]', width: '120px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'OrderGroupStatus', title: '<b>Trạng thái nhóm hàng</b><br>[OrderGroupStatus]', width: '120px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'OrderCode', title: '<b>Đơn hàng</b><br>[OrderCode]', width: '120px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'DNCode', title: '<b>Số DN</b><br>[DNCode]', width: '120px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'VehicleCode', title: '<b>Số xe</b><br>[VehicleCode]', width: '120px',
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
                field: 'SOCode', title: '<b>Số SO</b><br>[SOCode]', width: '120px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            
            {
                field: 'OrderDateConfig', title: '<b>Ngày tính giá đơn hàng</b><br>[OrderDateConfig]', width: '120px', template: '#=Common.Date.FromJsonDDMMYY(OrderDateConfig)#',
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
                field: 'DateConfig', title: '<b>Ngày tính giá</b><br>[DateConfig]', width: '120px', template: '#=Common.Date.FromJsonDDMMYY(DateConfig)#',
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
                field: 'RequestDate', title: '<b>Ngày y/c v/c</b><br>[RequestDate]', width: '120px', template: '#=Common.Date.FromJsonDMYHM(RequestDate)#',
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
                field: 'LocationToAddress', title: '<b>Địa chỉ giao</b><br>[LocationToAddress]', width: '120px',
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
                field: 'ProductCode', title: '<b>Mã hàng hóa</b><br>[ProductCode]', width: '120px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'ProductName', title: '<b>Hàng hóa</b><br>[ProductName]', width: '120px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'ProductDescription', title: '<b>Ghi chú hàng hóa</b><br>[ProductDescription]', width: '120px',
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
                field: 'Description', title: '<b>Ghi chú</b><br>[Description]', width: '120px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'TransportModeIncome', title: '<b>Loại hình v.chuyển khách hàng</b><br>[TransportModeIncome]', width: '120px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'TransportModeCost', title: '<b>Loại hình v.chuyển thầu</b><br>[TransportModeCost]', width: '120px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'TOMasterCode', title: '<b>Số chuyến</b><br>[TOMasterCode]', width: '120px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            
            {
                field: 'DriverName', title: '<b>Tài xế</b><br>[DriverName]', width: '120px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'TelNo', title: '<b>Số điện thoại</b><br>[TelNo]', width: '120px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'DrivingLicense', title: '<b>GPLX</b><br>[DrivingLicense]', width: '120px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'ETD', title: '<b>Ngày tới kho</b><br>[ETD]', width: '120px', template: '#=Common.Date.FromJsonDMYHM(ETD)#',
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
                field: 'ETA', title: '<b>Ngày giao hàng</b><br>[ETA]', width: '120px', template: '#=Common.Date.FromJsonDMYHM(ETA)#',
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
                field: 'VendorCode', title: '<b>Mã nhà vận tải</b><br>[VendorCode]', width: '120px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'VendorName', title: '<b>Nhà vận tải</b><br>[VendorName]', width: '120px',
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
                            $("<input class='dtp-filter-from' data-field='DateToLoadStart' style='width:50%; float:left;' />").appendTo(element);
                            $("<input class='dtp-filter-to' data-field='DateToLoadStart' style='width:50%; float:left;' />").appendTo(element);
                        }, showOperators: false
                    }
                },
            },
             {
                 field: 'InvoiceDate', title: '<b>Ngày nhận C/Từ</b><br>[InvoiceDate]', width: '150px', template: '#=Common.Date.FromJsonDMYHM(InvoiceDate)#',
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
                field: 'InvoiceReturnNote', title: '<b>Số c.từ trả về</b><br>[InvoiceReturnNote]', width: '120px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'TonTranfer', title: '<b>Tấn lấy</b><br>[TonTranfer]', width: '120px',
                filterable: { cell: { operator: 'equal', showOperators: false } }
            },
            {
                field: 'CBMTranfer', title: '<b>Khối lấy</b><br>[CBMTranfer]', width: '120px',
                filterable: { cell: { operator: 'equal', showOperators: false } }
            },
            {
                field: 'QuantityTranfer', title: '<b>Số lượng lấy</b><br>[QuantityTranfer]', width: '120px',
                filterable: { cell: { operator: 'equal', showOperators: false } }
            },
            {
                field: 'TonBBGN', title: '<b>Tấn giao</b><br>[TonBBGN]', width: '120px',
                filterable: { cell: { operator: 'equal', showOperators: false } }
            },
            {
                field: 'CBMBBGN', title: '<b>Khối giao</b><br>[CBMBBGN]', width: '120px',
                filterable: { cell: { operator: 'equal', showOperators: false } }
            },
            {
                field: 'QuantityBBGN', title: '<b>Số lượng giao</b><br>[QuantityBBGN]', width: '120px',
                filterable: { cell: { operator: 'equal', showOperators: false } }
            },
            {
                field: 'TonReturn', title: '<b>Tấn trả về</b><br>[TonReturn]', width: '120px',
                filterable: { cell: { operator: 'equal', showOperators: false } }
            },
            {
                field: 'CBMReturn', title: '<b>Khối trả về</b><br>[CBMReturn]', width: '120px',
                filterable: { cell: { operator: 'equal', showOperators: false } }
            },
            {
                field: 'QuantityReturn', title: '<b>Số lượng trả về</b><br>[QuantityReturn]', width: '120px',
                filterable: { cell: { operator: 'equal', showOperators: false } }
            },
            {
                field: 'KgTranfer', title: '<b>Kg lấy</b><br>[KgTranfer]', width: '120px', template: '#=KgTranfer==null?" ":Common.Number.ToNumber3(KgTranfer)#',
                filterable: { cell: { operator: 'gte', showOperators: false } }
            },
            {
                field: 'KgBBGN', title: '<b>Kg giao</b><br>[KgBBGN]', width: '120px', template: '#=KgBBGN==null?" ":Common.Number.ToNumber3(KgBBGN)#',
                filterable: { cell: { operator: 'gte', showOperators: false } }
            },
            {
                field: 'KgReturn', title: '<b>Kg trả về</b><br>[KgReturn]', width: '120px', template: '#=KgReturn==null?" ":Common.Number.ToNumber3(KgReturn)#',
                filterable: { cell: { operator: 'gte', showOperators: false } }
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
                field: 'OPSContract', title: '<b>Hợp đồng chuyến</b><br>[OPSContract]', width: '120px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'OPSRouting', title: '<b>Cung đường chuyến</b><br>[OPSRouting]', width: '120px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'KM', title: '<b>Số KM đã chạy</b><br>[KM]', width: '120px',
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
            
            
            { title: ' ', sortable: false, filterable: false, menu: false }
        ],
    };

    $scope.Search_Click = function ($event) {
        $event.preventDefault();

        $rootScope.IsLoading = true;
        Common.Services.Call($http, {
            url: Common.Services.url.REP,
            method: _REPDIOrder.URL.Search,
            data: { lstid: $scope.Item.lstCustomerID, dtfrom: $scope.Item.DateFrom, dtto: $scope.Item.DateTo, statusID: $scope.Item.StatusID, typeOfFilter: $scope.Item.TypeOfFilterID },
            success: function (res) {
                $rootScope.IsLoading = false;
                angular.forEach(res, function (item, dix) {
                    if (Common.HasValue(item.RequestDate))
                        item.RequestDate = kendo.parseDate(item.RequestDate);
                    if (Common.HasValue(item.DateConfig))
                        item.DateConfig = kendo.parseDate(item.DateConfig);
                    if (Common.HasValue(item.OrderDateConfig))
                        item.OrderDateConfig = kendo.parseDate(item.OrderDateConfig);
                    if (Common.HasValue(item.ETD))
                        item.ETD = kendo.parseDate(item.ETD);
                    if (Common.HasValue(item.ETA))
                        item.ETA = kendo.parseDate(item.ETA);
                    if (Common.HasValue(item.InvoiceReturnDate))
                        item.InvoiceReturnDate = kendo.parseDate(item.InvoiceReturnDate);
                    if (Common.HasValue(item.InvoiceDate))
                        item.InvoiceDate = kendo.parseDate(item.InvoiceDate);
                    if (Common.HasValue(item.DateFromCome))
                        item.DateFromCome = kendo.parseDate(item.DateFromCome);
                    if (Common.HasValue(item.DateFromLeave))
                        item.DateFromLeave = kendo.parseDate(item.DateFromLeave);
                    if (Common.HasValue(item.DateFromLoadStart))
                        item.DateFromLoadStart = kendo.parseDate(item.DateFromLoadStart);
                    if (Common.HasValue(item.DateFromLoadEnd))
                        item.DateFromLoadEnd = kendo.parseDate(item.DateFromLoadEnd);
                    if (Common.HasValue(item.DateToCome))
                        item.DateToCome = kendo.parseDate(item.DateToCome);
                    if (Common.HasValue(item.DateToLeave))
                        item.DateToLeave = kendo.parseDate(item.DateToLeave);
                    if (Common.HasValue(item.DateToLoadStart))
                        item.DateToLoadStart = kendo.parseDate(item.DateToLoadStart);
                    if (Common.HasValue(item.DateToLoadEnd))
                        item.DateToLoadEnd = kendo.parseDate(item.DateToLoadEnd);
                });

                $scope.REPDIStatus_gridOptions.dataSource.data(res);
            },
            error: function (res) {
                $rootScope.IsLoading = false;
            }
        });
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
                    if (Common.HasValue($scope.REPDIStatus_gridOptions.dataSource.filter())) {
                        filters = $scope.REPDIStatus_gridOptions.dataSource.filter().filters;
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
                    $scope.REPDIStatus_gridOptions.dataSource.filter(filters);
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
                    if (Common.HasValue($scope.REPDIStatus_gridOptions.dataSource.filter())) {
                        filters = $scope.REPDIStatus_gridOptions.dataSource.filter().filters;
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
                    $scope.REPDIStatus_gridOptions.dataSource.filter(filters);
                }
                catch (e) {
                    $rootScope.Message({ Msg: 'Sai dữ liệu!' });
                }
            }
        })
    }, 500);

    $scope.REPDIStatus_Order_Export = function ($event) {
        $event.preventDefault();

        $rootScope.IsLoading = true;
        Common.Services.Call($http, {
            url: Common.Services.url.REP,
            method: _REPDIOrder.URL.REPDIStatus_Export,
            data: {},
            success: function (res) {
                $rootScope.IsLoading = false;
                $rootScope.DownloadFile(res);
            }
        })
    }
    $scope.ShowSetting = function ($event, grid) {
        $event.preventDefault();

        $rootScope.ShowSetting({
            ListView: '',
            event: $event,
            grid: grid,
            current: $state.current
        });
    };

    // Setting export
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
                    CreateDate:{type:'date'},
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
                field: 'Name', title: 'Tên thiết lập', width: '150px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'CreateDate', title: 'Ngày tạo', width: '150px', template: '#=Common.Date.FromJsonDDMMYY(CreateDate)#',
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
            method: _REPDIOrder.URL.SettingList,
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
            if (Common.HasValue(data.ListCustomer)) {
                angular.forEach(data.ListCustomer, function (value, key) {
                    value.IsChoose = false;
                });
                $scope.SettingReport_Customer_Grid.dataSource.data(data.ListCustomer);
            } else {
                $scope.SettingReport_Customer_Grid.dataSource.data([]);
            }
        } else {
            $scope.SettingItem = { ID: 0 };
            $scope.SettingItem.TypeExport = 1;
            $scope.SettingItem.TypeDateRange = 1;
            $scope.SettingReport_Tab.select(0);
            $scope.SettingItem.StatusID = 0;
            $scope.SettingItem.TypeOfFilter = 1;
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
                    method: _REPDIOrder.URL.SettingSave,
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
                            method: _REPDIOrder.URL.SettingList,
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
                    method: _REPDIOrder.URL.SettingDelete,
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
                            method: _REPDIOrder.URL.SettingList,
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
            { ID: 1, ValueName: 'Trạng thái đơn hàng chi tiết' },
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
            method: _REPDIOrder.URL.SettingCusNotInList,
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
                method: _REPDIOrder.URL.SettingCusNotInSave,
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
                        method: _REPDIOrder.URL.SettingList,
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
                method: _REPDIOrder.URL.SettingCusDeleteList,
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
                    $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });

                    Common.Services.Call($http, {
                        url: Common.Services.url.REP,
                        method: _REPDIOrder.URL.SettingList,
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

    $scope.SettingReport_ActionClick = function ($event, data) {
        $event.preventDefault();

        var request = Common.Request.CreateFromGrid($scope.REPDIStatus_grid.dataSource);
        $rootScope.IsLoading = true;
        Common.Services.Call($http, {
            url: Common.Services.url.REP,
            method: _REPDIOrder.URL.SettingAction,
            data: { item: data, dtfrom: $scope.Item.DateFrom, dtto: $scope.Item.DateTo, request: request},
            success: function (res) {
                $rootScope.IsLoading = false;
                $rootScope.DownloadFile(res);
            }
        });
    }
    //#endregion

    $scope.window_Close_Click = function ($event, win) {
        $event.preventDefault();
        win.close();
    }
}]);