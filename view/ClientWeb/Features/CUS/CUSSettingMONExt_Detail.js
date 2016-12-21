/// <reference path="~/Scripts/jquery-1.9.1.intellisense.js" />
/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />

var _CUSSettingMONExtDetail = {
    URL: {
        Get: 'CUSSettingMONExt_Get',
        Save: 'CUSSettingMONExt_Save',
    },
    Data: {
        DataCustomer: [],
        DataTransport: [
            { ValueOfVar: 'Phân phối', ID: 1 },
            { ValueOfVar: 'Container', ID: 2 },
        ],
        DataContract: [],
        GridAllSetting: [
            { Required: true, Type: 0, Code: "ID", Name: "ID", IsKey: false },
            { Required: true, Type: 0, Code: "DNCode", Name: "Số DN", IsKey: false },
            { Required: true, Type: 0, Code: "SOCode", Name: "Số SO", IsKey: false },
            { Required: true, Type: 0, Code: "DateDN", Name: "Ngày DN", IsKey: false },
            { Required: false, Type: 0, Code: "OrderCode", Name: "Mã đơn hàng", IsKey: false },
            { Required: false, Type: 0, Code: "ETARequest", Name: "Ngày y/c giao hàng", IsKey: false },
            { Required: false, Type: 0, Code: "MasterETDDate", Name: "Ngày xuất kho", IsKey: false },
            { Required: false, Type: 0, Code: "MasterETDDatetime", Name: "Ngày giờ xuất kho", IsKey: false },
            { Required: false, Type: 0, Code: "OrderGroupETDDate", Name: "Ngày ETD chi tiết đơn", IsKey: false },
            { Required: false, Type: 0, Code: "OrderGroupETDDatetime", Name: "Ngày giờ ETD chi tiết đơn", IsKey: false },
            { Required: false, Type: 0, Code: "CustomerCode", Name: "Mã Khách hàng", IsKey: false },
            { Required: false, Type: 0, Code: "CustomerName", Name: "Tên khách hàng", IsKey: false },
            { Required: false, Type: 0, Code: "CreatedDate", Name: "Ngày tạo", IsKey: false },
            { Required: true, Type: 0, Code: "MasterCode", Name: "Mã chuyến", IsKey: false },
            { Required: false, Type: 0, Code: "DriverName", Name: "Tài xế", IsKey: false },
            { Required: false, Type: 0, Code: "DriverTel", Name: "SĐT Tài xế", IsKey: false },
            { Required: false, Type: 0, Code: "DriverCard", Name: "CMND", IsKey: false },
            { Required: true, Type: 0, Code: "RegNo", Name: "Xe", IsKey: false },
            { Required: false, Type: 0, Code: "RequestDate", Name: "Ngày gửi y/c", IsKey: false },
            { Required: false, Type: 0, Code: "LocationFromCode", Name: "Mã kho", IsKey: false },
            { Required: false, Type: 0, Code: "LocationToCode", Name: "Mã điểm giao", IsKey: false },
            { Required: false, Type: 0, Code: "LocationToName", Name: "Tên điểm giao", IsKey: false },
            { Required: false, Type: 0, Code: "LocationToAddress", Name: "Địa chỉ", IsKey: false },
            { Required: false, Type: 0, Code: "LocationToProvince", Name: "Tỉnh", IsKey: false },
            { Required: false, Type: 0, Code: "LocationToDistrict", Name: "Quận huyện", IsKey: false },
            { Required: false, Type: 0, Code: "DistributorCode", Name: "Mã NPP", IsKey: false },
            { Required: false, Type: 0, Code: "DistributorName", Name: "Tên NPP", IsKey: false },
            { Required: false, Type: 0, Code: "DistributorCodeName", Name: "Mã - Tên NPP", IsKey: false },
            { Required: true, Type: 0, Code: "IsInvoice", Name: "Nhận chứng từ", IsKey: false },
            { Required: true, Type: 0, Code: "DateFromCome", Name: "Ngày đến kho", IsKey: false },
            { Required: true, Type: 0, Code: "DateFromLeave", Name: "Ngày rời kho", IsKey: false },
            { Required: true, Type: 0, Code: "DateFromLoadStart", Name: "Thời gian vào máng", IsKey: false },
            { Required: true, Type: 0, Code: "DateFromLoadEnd", Name: "Thời gian ra máng", IsKey: false },
            { Required: true, Type: 0, Code: "DateToCome", Name: "Ngày đến NPP", IsKey: false },
            { Required: true, Type: 0, Code: "DateToLeave", Name: "Ngày rời NPP", IsKey: false },
            { Required: true, Type: 0, Code: "DateToLoadStart", Name: "Thời gian b.đầu dỡ hàng", IsKey: false },
            { Required: true, Type: 0, Code: "DateToLoadEnd", Name: "Thời gian k.thúc dỡ hàng", IsKey: false },
            { Required: true, Type: 0, Code: "InvoiceBy", Name: "Người nhận chứng từ", IsKey: false },
            { Required: true, Type: 0, Code: "InvoiceDate", Name: "Ngày nhận c/t", IsKey: false },
            { Required: true, Type: 0, Code: "InvoiceNote", Name: "Ghi chú c/t", IsKey: false },
            { Required: true, Type: 0, Code: "Note", Name: "Ghi chú", IsKey: false },
            { Required: true, Type: 0, Code: "OPSGroupNote1", Name: "Ghi chú 1", IsKey: false },
            { Required: true, Type: 0, Code: "OPSGroupNote2", Name: "Ghi chú 2", IsKey: false },
            { Required: true, Type: 0, Code: "ORDGroupNote1", Name: "Ghi chú Đ/h 1", IsKey: false },
            { Required: true, Type: 0, Code: "ORDGroupNote2", Name: "Ghi chú Đ/h 2", IsKey: false },
            { Required: true, Type: 0, Code: "TOMasterNote1", Name: "Ghi chú chuyến 1", IsKey: false },
            { Required: true, Type: 0, Code: "TOMasterNote2", Name: "Ghi chú chuyến 2", IsKey: false },
            { Required: false, Type: 0, Code: "VendorName", Name: "Nhà vận tải", IsKey: false },
            { Required: false, Type: 0, Code: "VendorCode", Name: "Mã nhà vận tải", IsKey: false },
            { Required: false, Type: 0, Code: "Description", Name: "Mô tả", IsKey: false },
            { Required: false, Type: 0, Code: "GroupOfProductCode", Name: "Mã nhóm sản phẩm", IsKey: false },
            { Required: false, Type: 0, Code: "GroupOfProductName", Name: "Nhóm sản phẩm", IsKey: false },
            { Required: true, Type: 0, Code: "ChipNo", Name: "ChipNo", IsKey: false },
            { Required: true, Type: 0, Code: "Temperature", Name: "Temperature", IsKey: false },
            { Required: false, Type: 0, Code: "Ton", Name: "Số tấn/kg kế hoạch", IsKey: false },
            { Required: false, Type: 0, Code: "CBM", Name: "Số khối kế hoạch", IsKey: false },
            { Required: false, Type: 0, Code: "Quantity", Name: "Số lượng kế hoạch", IsKey: false },
            { Required: true, Type: 0, Code: "TonTranfer", Name: "Tấn/kg lấy", IsKey: false },
            { Required: true, Type: 0, Code: "CBMTranfer", Name: "Khối lấy", IsKey: false },
            { Required: true, Type: 0, Code: "QuantityTranfer", Name: "Số lượng lấy", IsKey: false },
            { Required: true, Type: 0, Code: "TonBBGN", Name: "Tấn/kg giao", IsKey: false },
            { Required: true, Type: 0, Code: "CBMBBGN", Name: "Khối giao", IsKey: false },
            { Required: true, Type: 0, Code: "QuantityBBGN", Name: "Số lượng giao", IsKey: false },
            { Required: false, Type: 0, Code: "Packing", Name: "Mã hàng hóa/Đơn vị tính", IsKey: false },
            { Required: false, Type: 0, Code: "PackingName", Name: "Tên hàng hóa/Đơn vị tính", IsKey: false },
            { Required: true, Type: 0, Code: "VENLoadCode", Name: "Vendor bốc xếp lên", IsKey: false },
            { Required: true, Type: 0, Code: "VENUnLoadCode", Name: "Vendor bốc xếp xuống", IsKey: false },
            { Required: true, Type: 0, Code: "TonReturn", Name: "Tấn/kg trả về", IsKey: false },
            { Required: true, Type: 0, Code: "CBMReturn", Name: "Khối trả về", IsKey: false },
            { Required: true, Type: 0, Code: "QuantityReturn", Name: "Số lượng trả về", IsKey: false },
            { Required: true, Type: 0, Code: "InvoiceReturnNote", Name: "Số chứng từ trả về", IsKey: false },
            { Required: true, Type: 0, Code: "InvoiceReturnDate", Name: "Ngày chứng từ trả về", IsKey: false },
            { Required: true, Type: 0, Code: "ReasonCancelNote", Name: "Ghi chú lí do", IsKey: false },
        ],
        GridSelected: [
            { Required: false, Type: 0, Column: 'A', Code: "", Name: "", IndexColumn: 1, IsKey: false },
            { Required: false, Type: 0, Column: 'B', Code: "", Name: "", IndexColumn: 2, IsKey: false },
            { Required: false, Type: 0, Column: 'C', Code: "", Name: "", IndexColumn: 3, IsKey: false },
            { Required: false, Type: 0, Column: 'D', Code: "", Name: "", IndexColumn: 4, IsKey: false },
            { Required: false, Type: 0, Column: 'E', Code: "", Name: "", IndexColumn: 5, IsKey: false },
            { Required: false, Type: 0, Column: 'F', Code: "", Name: "", IndexColumn: 6, IsKey: false },
            { Required: false, Type: 0, Column: 'G', Code: "", Name: "", IndexColumn: 7, IsKey: false },
            { Required: false, Type: 0, Column: 'H', Code: "", Name: "", IndexColumn: 8, IsKey: false },
            { Required: false, Type: 0, Column: 'I', Code: "", Name: "", IndexColumn: 9, IsKey: false },
            { Required: false, Type: 0, Column: 'J', Code: "", Name: "", IndexColumn: 10, IsKey: false },
            { Required: false, Type: 0, Column: 'K', Code: "", Name: "", IndexColumn: 11, IsKey: false },
            { Required: false, Type: 0, Column: 'L', Code: "", Name: "", IndexColumn: 12, IsKey: false },
            { Required: false, Type: 0, Column: 'M', Code: "", Name: "", IndexColumn: 13, IsKey: false },
            { Required: false, Type: 0, Column: 'N', Code: "", Name: "", IndexColumn: 14, IsKey: false },
            { Required: false, Type: 0, Column: 'O', Code: "", Name: "", IndexColumn: 15, IsKey: false },
            { Required: false, Type: 0, Column: 'P', Code: "", Name: "", IndexColumn: 16, IsKey: false },
            { Required: false, Type: 0, Column: 'Q', Code: "", Name: "", IndexColumn: 17, IsKey: false },
            { Required: false, Type: 0, Column: 'R', Code: "", Name: "", IndexColumn: 18, IsKey: false },
            { Required: false, Type: 0, Column: 'S', Code: "", Name: "", IndexColumn: 19, IsKey: false },
            { Required: false, Type: 0, Column: 'T', Code: "", Name: "", IndexColumn: 20, IsKey: false },
            { Required: false, Type: 0, Column: 'U', Code: "", Name: "", IndexColumn: 21, IsKey: false },
            { Required: false, Type: 0, Column: 'V', Code: "", Name: "", IndexColumn: 22, IsKey: false },
            { Required: false, Type: 0, Column: 'W', Code: "", Name: "", IndexColumn: 23, IsKey: false },
            { Required: false, Type: 0, Column: 'X', Code: "", Name: "", IndexColumn: 24, IsKey: false },
            { Required: false, Type: 0, Column: 'Y', Code: "", Name: "", IndexColumn: 25, IsKey: false },
            { Required: false, Type: 0, Column: 'Z', Code: "", Name: "", IndexColumn: 26, IsKey: false },
            { Required: false, Type: 0, Column: 'AA', Code: "", Name: "", IndexColumn: 27, IsKey: false },
            { Required: false, Type: 0, Column: 'AB', Code: "", Name: "", IndexColumn: 28, IsKey: false },
            { Required: false, Type: 0, Column: 'AC', Code: "", Name: "", IndexColumn: 29, IsKey: false },
            { Required: false, Type: 0, Column: 'AD', Code: "", Name: "", IndexColumn: 30, IsKey: false },
            { Required: false, Type: 0, Column: 'AE', Code: "", Name: "", IndexColumn: 31, IsKey: false },
            { Required: false, Type: 0, Column: 'AF', Code: "", Name: "", IndexColumn: 32, IsKey: false },
            { Required: false, Type: 0, Column: 'AG', Code: "", Name: "", IndexColumn: 33, IsKey: false },
            { Required: false, Type: 0, Column: 'AH', Code: "", Name: "", IndexColumn: 34, IsKey: false },
            { Required: false, Type: 0, Column: 'AI', Code: "", Name: "", IndexColumn: 35, IsKey: false },
            { Required: false, Type: 0, Column: 'AJ', Code: "", Name: "", IndexColumn: 36, IsKey: false },
            { Required: false, Type: 0, Column: 'AK', Code: "", Name: "", IndexColumn: 37, IsKey: false },
            { Required: false, Type: 0, Column: 'AL', Code: "", Name: "", IndexColumn: 38, IsKey: false },
            { Required: false, Type: 0, Column: 'AM', Code: "", Name: "", IndexColumn: 39, IsKey: false },
            { Required: false, Type: 0, Column: 'AN', Code: "", Name: "", IndexColumn: 40, IsKey: false },
            { Required: false, Type: 0, Column: 'AO', Code: "", Name: "", IndexColumn: 41, IsKey: false },
            { Required: false, Type: 0, Column: 'AP', Code: "", Name: "", IndexColumn: 42, IsKey: false },
            { Required: false, Type: 0, Column: 'AQ', Code: "", Name: "", IndexColumn: 43, IsKey: false },
            { Required: false, Type: 0, Column: 'AR', Code: "", Name: "", IndexColumn: 44, IsKey: false },
            { Required: false, Type: 0, Column: 'AS', Code: "", Name: "", IndexColumn: 45, IsKey: false },
            { Required: false, Type: 0, Column: 'AT', Code: "", Name: "", IndexColumn: 46, IsKey: false },
            { Required: false, Type: 0, Column: 'AU', Code: "", Name: "", IndexColumn: 47, IsKey: false },
            { Required: false, Type: 0, Column: 'AV', Code: "", Name: "", IndexColumn: 48, IsKey: false },
            { Required: false, Type: 0, Column: 'AW', Code: "", Name: "", IndexColumn: 49, IsKey: false },
            { Required: false, Type: 0, Column: 'AX', Code: "", Name: "", IndexColumn: 50, IsKey: false },
            { Required: false, Type: 0, Column: 'AY', Code: "", Name: "", IndexColumn: 51, IsKey: false },
            { Required: false, Type: 0, Column: 'AZ', Code: "", Name: "", IndexColumn: 52, IsKey: false },
        ],
        TypeTransport: [
            { ValueOfVar: 'Phân phối', ID: 1 },
            { ValueOfVar: 'Container', ID: 2 },
        ],
    },
    Param: { id: 0 }
}

angular.module('myapp').controller('CUSSettingMONExt_DetailCtrl', ['$rootScope', '$scope', '$http', '$location', '$state', '$timeout', '$stateParams', function ($rootScope, $scope, $http, $location, $state, $timeout, $stateParams) {
    Common.Log('CUSSettingMONExt_DetailCtrl');
    $rootScope.IsLoading = false;

    _CUSSettingMONExtDetail.Param = $.extend(true, _CUSSettingMONExtDetail.Param, $state.params);

    $scope.TranferItem = null;
    $scope.RevertItem = null;
    $scope.PreItem = null;

    $scope.cboCustomer_Options = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: "contains", suggest: true,
        dataTextField: 'CustomerName', dataValueField: 'ID',
        dataSource: Common.DataSource.Local([], {
            id: 'ID',
            fields: {
                CustomerName: { type: 'string' },
                ID: { type: 'number' },
            }
        }),
        change: function (e) {

        }
    }
    Common.ALL.Get($http, {
        url: Common.ALL.URL.Customer,
        success: function (data) {
            var newData = [];
            newData.push({ ID: 0, CustomerName: "Tất cả" })
            Common.Data.Each(data, function (o) {
                newData.push(o)
            });
            $scope.cboCustomer_Options.dataSource.data(newData);
        }
    })

    $scope.cboType_Options = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: "contains", suggest: true,
        dataTextField: 'ValueOfVar', dataValueField: 'ID',
        dataSource: Common.DataSource.Local([], {
            id: 'ID',
            fields: {
                ValueOfVar: { type: 'string' },
                ID: { type: 'number' },
            },
        }),
        change: function (e) {
            var val = this.value();
            Common.Log("TransportMode:" + val);
        }
    }
    $scope.Item = { TypeOfTransportModeID: 1, IsSKU: false, IsReturnSetting: false };
    $scope.cboType_Options.dataSource.data(_CUSSettingMONExtDetail.Data.TypeTransport);

    $scope.splitter_Options = {
        orientation: "horizontal",
        panes: [
            { collapsible: false, resizable: false, size: "50%", min: '250px' },
            { collapsible: false, resizable: false, size: "50%", min: '250px' }
        ]
    }

    $scope.AllSettingGrid_Options = {
        dataSource: Common.DataSource.Local([], {
            model: {
                //id: 'id',
                fields: {
                    IsStock: { type: 'boolean' },
                    Code: { type: 'string' },
                    Name: { type: 'string' },
                }
            },
            pageSize: 0
        }),
        height: '100%', pageable: false, sortable: true, columnMenu: false, filterable: { mode: 'row' }, resizable: true, editable: false, selectable: 'row',
        columns: [
            { field: 'Code', width: "100px", title: 'Mã', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'Name', width: "150px", title: 'Tên', filterable: { cell: { operator: 'contains', showOperators: false } } },
        ],
        change: function (e) {
            var selected = this.select();
            if (selected.length > 0) {
                $scope.TranferItem = this.dataItem(selected);
                $timeout(function () {
                    $scope.RevertItem = null;
                    $scope.SelectedSettingGrid.clearSelection();
                }, 1)
            }
        },
        dataBound: function (e) {
            Common.Log("dataBound")
            var grid = this;

            if (Common.HasValue(grid.tbody) && Common.HasValue(grid)) {
                var data = grid.dataSource.data();
                Common.Data.Each(data, function (o) {
                    if (o.Required)
                        $('tr[data-uid="' + o.uid + '"] ').css({ "background-color": "#fda" });
                })
            }
        }
    };


    $scope.numRowStart_Options = { format: 'n0', spinners: false, culture: 'en-US', min: 1, step: 1, decimals: 0 }

    // $scope.AllSettingGrid_Options.dataSource.data(_CUSSettingMONExtDetail.Data.GridAllSetting)

    $scope.SelectedSettingGrid_Options = {
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'IndexColumn',
                fields: {
                    IndexColumn: { type: 'number' },
                    IsKey: { type: 'boolean', },
                }
            },
            pageSize: 0
        }),
        height: '100%', pageable: false, sortable: false, columnMenu: false, filterable: false, resizable: true, editable: false, selectable: 'row',
        columns: [
            { field: 'Column', width: "100px", title: 'Cột Excel' },
            { field: 'Code', width: "100px", title: 'Mã' },
            { field: 'Name', width: "150px", title: 'Tên' },
            {
                field: 'IsKey', width: 100, title: 'Khóa kiểm tra', attributes: { style: "text-align: center; " },
                template: "<input type='checkbox' ng-model='dataItem.IsKey' ng-change=''></input>",
            },
        ],
        change: function (e) {
            var selected = this.select();

            if (selected.length > 0) {
                var item = this.dataItem(selected);
                //co item chuuyen
                if (Common.HasValue($scope.TranferItem)) {
                    if (item.Code == "" && item.Name == "") {//chi chuyen vao item chua dc chọn
                        item.Code = $scope.TranferItem.Code;
                        item.Name = $scope.TranferItem.Name;
                        item.Type = $scope.TranferItem.Type;
                        item.IsKey = $scope.TranferItem.IsKey;
                        item.Required = $scope.TranferItem.Required;

                        $timeout(function () {
                            $scope.AllSettingGrid_Options.dataSource.remove($scope.TranferItem);
                            $scope.TranferItem = null;
                            $scope.AllSettingGrid.clearSelection()
                            $scope.SelectedSettingGrid.clearSelection()
                        }, 1)
                    }
                }
                else {
                    //neu ko co item revert thi select row
                    if (!Common.HasValue($scope.RevertItem)) {
                        if (item.Code != "" && item.Name != "") {
                            $scope.RevertItem = item;
                            $scope.PreItem = item;
                        }
                        else $scope.RevertItem = null
                    }
                    else {
                        // co item revert => change loction
                        var itemBackup = $.extend(true, itemBackup, item)
                        item.Code = $scope.RevertItem.Code;
                        item.Name = $scope.RevertItem.Name;
                        item.Type = $scope.RevertItem.Type;

                        $scope.PreItem.Code = itemBackup.Code;
                        $scope.PreItem.Name = itemBackup.Name;
                        $scope.PreItem.Type = itemBackup.Type;
                        $scope.PreItem.IsKey = itemBackup.IsKey;
                        $scope.PreItem.Required = itemBackup.Required;

                        $timeout(function () {
                            $scope.RevertItem = null;
                            $scope.PreItem = null;
                            $scope.AllSettingGrid.clearSelection()
                            $scope.SelectedSettingGrid.clearSelection()
                        }, 1)
                    }
                }
            }

        }
    }

    $scope.change_key = function (data) {
        if (data.Code == "RegNo" || data.Code == "ETD" || data.Code == "OrderCode" || data.Code == "LocationToCode") {
            if (!data.IsKey) {
                $rootScope.Message({ Msg: 'Khóa bắt buộc', NotifyType: Common.Message.NotifyType.ERROR });
                data.IsKey = true;
            }
        }
    }

    $scope.SelectedSettingGrid_Options.dataSource.data(_CUSSettingMONExtDetail.Data.GridSelected)

    $scope.ResetSetting_Click = function ($event) {
        $event.preventDefault();
    }

    $scope.Save_Click = function ($event) {
        $event.preventDefault();
        if (!Common.HasValue($scope.Item.CustomerID) && $scope.Item.CustomerID <= 0) {
            $rootScope.Message({ Msg: 'Chưa chọn khách hàng', NotifyType: Common.Message.NotifyType.ERROR });
        }
        else if (!Common.HasValue($scope.Item.TypeOfTransportModeID) && $scope.Item.TypeOfTransportModeID <= 0) {
            $rootScope.Message({ Msg: 'Chưa chọn loại v/c', NotifyType: Common.Message.NotifyType.ERROR });
        }
        else if (Common.HasValue($scope.Item.Name) && $scope.Item.Name != "") {
            $rootScope.IsLoading = true;
            var data = $scope.SelectedSettingGrid.dataSource.data();

            var ItemSend = {
                ID: $scope.Item.ID,
                Name: $scope.Item.Name,
                RowStart: $scope.Item.RowStart,
                CustomerID: $scope.Item.CustomerID,
                TypeOfTransportModeID: $scope.Item.TypeOfTransportModeID,
                IsSKU: $scope.Item.IsSKU,
                IsReturnSetting: $scope.Item.IsReturnSetting,
            }

            //var hasRegNo = false;
            //var hasETD = false;
            //var hasOrderCode = false;
            //var hasLocationToCode = false;
            //var hasTon = false;
            //var hasCBM = false;
            //var hasQuantity = false;
            var hasKey = false;
            Common.Data.Each(data, function (o) {
                if (o.Code != "" && o.Name != "") {
                    ItemSend[o.Code] = o.IndexColumn;
                    ItemSend[o.Code + "Key"] = o.IsKey;
                    if (!hasKey && o.IsKey) {
                        hasKey = true;
                    }
                }
            })

            var hasError = false;
            //if (!hasRegNo) {
            //    $rootScope.Message({ Msg: 'Thiếu cột Xe', NotifyType: Common.Message.NotifyType.ERROR });
            //    hasError = true;
            //    $rootScope.IsLoading = false;
            //}
            //if (!hasETD) {
            //    $rootScope.Message({ Msg: 'Thiếu cột ETD', NotifyType: Common.Message.NotifyType.ERROR });
            //    hasError = true;
            //    $rootScope.IsLoading = false;
            //}
            //if (!hasOrderCode) {
            //    $rootScope.Message({ Msg: 'Thiếu cột Mã đơn hàng', NotifyType: Common.Message.NotifyType.ERROR });
            //    hasError = true;
            //    $rootScope.IsLoading = false;
            //}
            //if (!hasLocationToCode) {
            //    $rootScope.Message({ Msg: 'Thiếu cột Mã nhà phân phối', NotifyType: Common.Message.NotifyType.ERROR });
            //    hasError = true;
            //    $rootScope.IsLoading = false;
            //}

            //if (!hasTon) {
            //    $rootScope.Message({ Msg: 'Thiếu cột Tấn', NotifyType: Common.Message.NotifyType.ERROR });
            //    hasError = true;
            //    $rootScope.IsLoading = false;
            //}
            //if (!hasCBM) {
            //    $rootScope.Message({ Msg: 'Thiếu cột khối', NotifyType: Common.Message.NotifyType.ERROR });
            //    hasError = true;
            //    $rootScope.IsLoading = false;
            //}
            //if (!hasQuantity) {
            //    $rootScope.Message({ Msg: 'Thiếu cột Số lượng', NotifyType: Common.Message.NotifyType.ERROR });
            //    hasError = true;
            //    $rootScope.IsLoading = false;
            //}
            if (!hasKey) {
                $rootScope.Message({ Msg: 'Thiếu khóa kiểm tra', NotifyType: Common.Message.NotifyType.ERROR });
                hasError = true;
                $rootScope.IsLoading = false;
            }

            if (!hasError) {
                Common.Services.Call($http, {
                    url: Common.Services.url.CUS,
                    method: _CUSSettingMONExtDetail.URL.Save,
                    data: { item: ItemSend, id: _CUSSettingMONExtDetail.Param.id },
                    success: function (res) {
                        $rootScope.IsLoading = false;
                        $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                        $state.go("main.CUSSettingMonitor.Index")
                    },
                    error: function (res) {
                        $rootScope.IsLoading = false;
                    }
                });
            }
        }
        else $rootScope.Message({ Msg: 'Thiếu tên', NotifyType: Common.Message.NotifyType.ERROR });

    }

    $scope.Revert_Click = function ($event) {
        if (Common.HasValue($scope.RevertItem)) {
            var item = { Code: $scope.RevertItem.Code, Name: $scope.RevertItem.Name, IsStock: $scope.RevertItem.StockID > 0, StockID: $scope.RevertItem.StockID, GOPID: $scope.RevertItem.GOPID, ProductID: $scope.RevertItem.ProductID, IsKey: $scope.RevertItem.IsKey, Required: $scope.RevertItem.Required };
            $scope.AllSettingGrid_Options.dataSource.insert(0, item);

            var itemrevert = $scope.SelectedSettingGrid.dataItem($scope.SelectedSettingGrid.select())

            if (Common.HasValue(itemrevert)) {
                itemrevert.Code = "";
                itemrevert.Name = "";
                itemrevert.StockID = 0;
                itemrevert.Type = 0;
                itemrevert.GOPID = -1;
                itemrevert.ProductID = -1;
                itemrevert.IsKey = false;
            }
            $scope.RevertItem = null;
            $scope.SelectedSettingGrid.clearSelection()
        }
    }

    Common.Services.Call($http, {
        url: Common.Services.url.CUS,
        method: _CUSSettingMONExtDetail.URL.Get,
        data: { id: _CUSSettingMONExtDetail.Param.id },
        success: function (res) {
            $scope.Item = res;
            if (_CUSSettingMONExtDetail.Param.id <= 0)
                $scope.Item.RowStart = 2;

            var dataAll = [];
            var dataSource = $scope.SelectedSettingGrid.dataSource;

            var dataGridAllSetting = $.extend([], true, _CUSSettingMONExtDetail.Data.GridAllSetting)

            Common.Data.Each(dataGridAllSetting, function (o) {
                if (!$scope.Item[o.Code] > 0) {
                    dataAll.push(o);
                }
                else {
                    //Get vị trí row
                    var dataItem = dataSource.get($scope.Item[o.Code]);
                    //Gán
                    dataItem.Code = o.Code; dataItem.Name = o.Name;
                    if ($scope.Item[o.Code + "Key"]) {
                        dataItem.IsKey = true;
                    } else {
                        dataItem.IsKey = false;
                    }
                }
            })

            $scope.AllSettingGrid.dataSource.data(dataAll);
        }
    });

    $scope.Back_Click = function ($event) {
        $event.preventDefault();
        $state.go("main.CUSSettingMonitor.Index")
    }
}]);