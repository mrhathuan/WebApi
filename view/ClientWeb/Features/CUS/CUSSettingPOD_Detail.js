/// <reference path="~/Scripts/jquery-1.9.1.intellisense.js" />
/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />

var _CUSSettingPODDetail = {
    URL: {
        Get_StockByCus: 'CUSSettingORD_StockByCus_List',
        Get: 'CUSSettingPOD_Get',
        Save: 'CUSSettingPOD_Save',
    },
    Data: {
        DataCustomer: [],
        DataTransport: [
            { ValueOfVar: 'Phân phối', ID: 1 },
            { ValueOfVar: 'Container', ID: 2 },
        ],
        DataContract: [],
        GridAllSetting: [
            { Required: true, Type: 0, Code: "ID", Name: "Số thứ tự" },
            { Required: true, Type: 0, Code: "DNCode", Name: "Số DN" },
            { Required: true, Type: 0, Code: "SOCode", Name: "Số SO" },
            { Required: false, Type: 0, Code: "OrderCode", Name: "Mã đơn hàng" },
            { Required: false, Type: 0, Code: "ETARequest", Name: "Ngày y/c giao hàng" },
            { Required: false, Type: 0, Code: "ETD", Name: "ETD" },
            { Required: false, Type: 0, Code: "CustomerCode", Name: "Mã Khách hàng" },
            { Required: false, Type: 0, Code: "CustomerName", Name: "Tên khách hàng" },
            { Required: false, Type: 0, Code: "CreatedDate", Name: "Ngày tạo" },
            { Required: true, Type: 0, Code: "MasterCode", Name: "Mã chuyến" },
            { Required: false, Type: 0, Code: "DriverName", Name: "Tài xế" },
            { Required: false, Type: 0, Code: "DriverTel", Name: "SĐT Tài xế" },
            { Required: false, Type: 0, Code: "DriverCard", Name: "CMND" },
            { Required: false, Type: 0, Code: "RegNo", Name: "Xe" },
            { Required: false, Type: 0, Code: "RequestDate", Name: "Ngày gửi y/c" },
            { Required: false, Type: 0, Code: "LocationFromCode", Name: "Mã kho" },
            { Required: false, Type: 0, Code: "LocationToCode", Name: "Mã NPP" },
            { Required: false, Type: 0, Code: "LocationToName", Name: "NPP" },
            { Required: false, Type: 0, Code: "LocationToAddress", Name: "Địa chỉ" },
            { Required: false, Type: 0, Code: "LocationToProvince", Name: "Tỉnh" },
            { Required: false, Type: 0, Code: "LocationToDistrict", Name: "Quận huyện" },
            { Required: true, Type: 0, Code: "IsInvoice", Name: "Nhận chứng từ" },
            { Required: true, Type: 0, Code: "DateFromCome", Name: "Ngày đến kho" },
            { Required: true, Type: 0, Code: "DateFromLeave", Name: "Ngày rời kho" },
            { Required: true, Type: 0, Code: "DateFromLoadStart", Name: "Thời gian vào máng" },
            { Required: true, Type: 0, Code: "DateFromLoadEnd", Name: "Thời gian ra máng" },
            { Required: true, Type: 0, Code: "DateToCome", Name: "Ngày đến NPP" },
            { Required: true, Type: 0, Code: "DateToLeave", Name: "Ngày rời NPP" },
            { Required: true, Type: 0, Code: "DateToLoadStart", Name: "Thời gian b.đầu dỡ hàng" },
            { Required: true, Type: 0, Code: "DateToLoadEnd", Name: "Thời gian k.thúc dỡ hàng" },
            { Required: true, Type: 0, Code: "InvoiceBy", Name: "Người tạo chứng từ" },
            { Required: true, Type: 0, Code: "InvoiceDate", Name: "Ngày tạo c/t" },
            { Required: true, Type: 0, Code: "InvoiceNote", Name: "Ghi chú c/t" },
            { Required: true, Type: 0, Code: "Note", Name: "Ghi chú" },
            { Required: true, Type: 0, Code: "Note1", Name: "Ghi chú 1" },
            { Required: true, Type: 0, Code: "Note2", Name: "Ghi chú 2" },
            { Required: false, Type: 0, Code: "VendorName", Name: "Nhà vận tải" },
            { Required: false, Type: 0, Code: "VendorCode", Name: "Mã nhà vận tải" },
            { Required: false, Type: 0, Code: "Description", Name: "Description" },
            { Required: false, Type: 0, Code: "GroupOfProductCode", Name: "Mã nhóm sản phẩm" },
            { Required: false, Type: 0, Code: "GroupOfProductName", Name: "Nhóm sản phẩm" },
            { Required: true, Type: 0, Code: "ChipNo", Name: "ChipNo" },
            { Required: true, Type: 0, Code: "Temperature", Name: "Temperature" },
            { Required: false, Type: 0, Code: "Ton", Name: "Số tấn kế hoạch" },
            { Required: false, Type: 0, Code: "CBM", Name: "Số khối kế hoạch" },
            { Required: false, Type: 0, Code: "Quantity", Name: "Số lượng kế hoạch" },
            { Required: true, Type: 0, Code: "TonTranfer", Name: "Tấn lấy" },
            { Required: true, Type: 0, Code: "CBMTranfer", Name: "Khối lấy" },
            { Required: true, Type: 0, Code: "QuantityTranfer", Name: "Số lượng lấy" },
            { Required: true, Type: 0, Code: "TonBBGN", Name: "Tấn giao" },
            { Required: true, Type: 0, Code: "CBMBBGN", Name: "Khối giao" },
            { Required: true, Type: 0, Code: "QuantityBBGN", Name: "Số lượng giao" },
        ],
        GridSelected: [
            { Required: false, Type: 0, Column: 'A', Code: "", Name: "", IndexColumn: 1, },
            { Required: false, Type: 0, Column: 'B', Code: "", Name: "", IndexColumn: 2, },
            { Required: false, Type: 0, Column: 'C', Code: "", Name: "", IndexColumn: 3, },
            { Required: false, Type: 0, Column: 'D', Code: "", Name: "", IndexColumn: 4, },
            { Required: false, Type: 0, Column: 'E', Code: "", Name: "", IndexColumn: 5, },
            { Required: false, Type: 0, Column: 'F', Code: "", Name: "", IndexColumn: 6, },
            { Required: false, Type: 0, Column: 'G', Code: "", Name: "", IndexColumn: 7, },
            { Required: false, Type: 0, Column: 'H', Code: "", Name: "", IndexColumn: 8, },
            { Required: false, Type: 0, Column: 'I', Code: "", Name: "", IndexColumn: 9, },
            { Required: false, Type: 0, Column: 'J', Code: "", Name: "", IndexColumn: 10, },
            { Required: false, Type: 0, Column: 'K', Code: "", Name: "", IndexColumn: 11, },
            { Required: false, Type: 0, Column: 'L', Code: "", Name: "", IndexColumn: 12, },
            { Required: false, Type: 0, Column: 'M', Code: "", Name: "", IndexColumn: 13, },
            { Required: false, Type: 0, Column: 'N', Code: "", Name: "", IndexColumn: 14, },
            { Required: false, Type: 0, Column: 'O', Code: "", Name: "", IndexColumn: 15, },
            { Required: false, Type: 0, Column: 'P', Code: "", Name: "", IndexColumn: 16, },
            { Required: false, Type: 0, Column: 'Q', Code: "", Name: "", IndexColumn: 17, },
            { Required: false, Type: 0, Column: 'R', Code: "", Name: "", IndexColumn: 18, },
            { Required: false, Type: 0, Column: 'S', Code: "", Name: "", IndexColumn: 19, },
            { Required: false, Type: 0, Column: 'T', Code: "", Name: "", IndexColumn: 20, },
            { Required: false, Type: 0, Column: 'U', Code: "", Name: "", IndexColumn: 21, },
            { Required: false, Type: 0, Column: 'V', Code: "", Name: "", IndexColumn: 22, },
            { Required: false, Type: 0, Column: 'W', Code: "", Name: "", IndexColumn: 23, },
            { Required: false, Type: 0, Column: 'X', Code: "", Name: "", IndexColumn: 24, },
            { Required: false, Type: 0, Column: 'Y', Code: "", Name: "", IndexColumn: 25, },
            { Required: false, Type: 0, Column: 'Z', Code: "", Name: "", IndexColumn: 26, },
            { Required: false, Type: 0, Column: 'AA', Code: "", Name: "", IndexColumn: 27, },
            { Required: false, Type: 0, Column: 'AB', Code: "", Name: "", IndexColumn: 28, },
            { Required: false, Type: 0, Column: 'AC', Code: "", Name: "", IndexColumn: 29, },
            { Required: false, Type: 0, Column: 'AD', Code: "", Name: "", IndexColumn: 30, },
            { Required: false, Type: 0, Column: 'AE', Code: "", Name: "", IndexColumn: 31, },
            { Required: false, Type: 0, Column: 'AF', Code: "", Name: "", IndexColumn: 32, },
            { Required: false, Type: 0, Column: 'AG', Code: "", Name: "", IndexColumn: 33, },
            { Required: false, Type: 0, Column: 'AH', Code: "", Name: "", IndexColumn: 34, },
            { Required: false, Type: 0, Column: 'AI', Code: "", Name: "", IndexColumn: 35, },
            { Required: false, Type: 0, Column: 'AJ', Code: "", Name: "", IndexColumn: 36, },
            { Required: false, Type: 0, Column: 'AK', Code: "", Name: "", IndexColumn: 37, },
            { Required: false, Type: 0, Column: 'AL', Code: "", Name: "", IndexColumn: 38, },
            { Required: false, Type: 0, Column: 'AM', Code: "", Name: "", IndexColumn: 39, },
            { Required: false, Type: 0, Column: 'AN', Code: "", Name: "", IndexColumn: 40, },
            { Required: false, Type: 0, Column: 'AO', Code: "", Name: "", IndexColumn: 41, },
            { Required: false, Type: 0, Column: 'AP', Code: "", Name: "", IndexColumn: 42, },
            { Required: false, Type: 0, Column: 'AQ', Code: "", Name: "", IndexColumn: 43, },
            { Required: false, Type: 0, Column: 'AR', Code: "", Name: "", IndexColumn: 44, },
            { Required: false, Type: 0, Column: 'AS', Code: "", Name: "", IndexColumn: 45, },
            { Required: false, Type: 0, Column: 'AT', Code: "", Name: "", IndexColumn: 46, },
            { Required: false, Type: 0, Column: 'AU', Code: "", Name: "", IndexColumn: 47, },
            { Required: false, Type: 0, Column: 'AV', Code: "", Name: "", IndexColumn: 48, },
            { Required: false, Type: 0, Column: 'AW', Code: "", Name: "", IndexColumn: 49, },
            { Required: false, Type: 0, Column: 'AX', Code: "", Name: "", IndexColumn: 50, },
            { Required: false, Type: 0, Column: 'AY', Code: "", Name: "", IndexColumn: 51, },
            { Required: false, Type: 0, Column: 'AZ', Code: "", Name: "", IndexColumn: 52, },
        ],
        TypeTransport: [
            { ValueOfVar: 'Phân phối', ID: 1 },
            { ValueOfVar: 'Container', ID: 2 },
        ],
    },
    Param: { id: 0 }
}

angular.module('myapp').controller('CUSSettingPOD_DetailCtrl', ['$rootScope', '$scope', '$http', '$location', '$state', '$timeout', '$stateParams', function ($rootScope, $scope, $http, $location, $state, $timeout, $stateParams) {
    Common.Log('CUSSettingPOD_DetailCtrl');
    $rootScope.IsLoading = false;
    
    _CUSSettingPODDetail.Param = $.extend(true, _CUSSettingPODDetail.Param, $state.params);

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
    $scope.Item = {TypeOfTransportModeID: 1};
    $scope.cboType_Options.dataSource.data(_CUSSettingPODDetail.Data.TypeTransport);

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

    // $scope.AllSettingGrid_Options.dataSource.data(_CUSSettingPODDetail.Data.GridAllSetting)

    $scope.SelectedSettingGrid_Options = {
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'IndexColumn',
                fields: {
                    IndexColumn: { type: 'number' },
                }
            },
            pageSize: 0
        }),
        height: '100%', pageable: false, sortable: false, columnMenu: false, filterable: false, resizable: true, editable: false, selectable: 'row',
        columns: [
            { field: 'Column', width: "100px", title: 'Cột Excel' },
            { field: 'Code', width: "100px", title: 'Mã' },
            { field: 'Name', width: "150px", title: 'Tên' },
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

    $scope.SelectedSettingGrid_Options.dataSource.data(_CUSSettingPODDetail.Data.GridSelected)

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
            }

            var errorSortOrder = false;
            var hasID = false;
            Common.Data.Each(data, function (o) {
                if (o.Code != "" && o.Name != "") {
                    ItemSend[o.Code] = o.IndexColumn;

                    if (o.Code == "ID") {
                        hasID = true;
                        if (o.IndexColumn != 1) {
                            errorSortOrder = true;
                        }
                    }
                }
            })

            if (!hasID) {
                $rootScope.Message({ Msg: 'Thiếu cột Số thứ tự', NotifyType: Common.Message.NotifyType.ERROR });
                $rootScope.IsLoading = false;
            } else if (errorSortOrder) {
                $rootScope.Message({ Msg: 'Cột Số thứ tự phải ở cột A', NotifyType: Common.Message.NotifyType.ERROR });
                $rootScope.IsLoading = false;
            }
            if (!errorSortOrder && hasID) {
                Common.Services.Call($http, {
                    url: Common.Services.url.CUS,
                    method: _CUSSettingPODDetail.URL.Save,
                    data: { item: ItemSend, id: _CUSSettingPODDetail.Param.id },
                    success: function (res) {
                        $rootScope.IsLoading = false;
                        $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                        $state.go("main.CUSSettingPOD.Index")
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
            var item = { Code: $scope.RevertItem.Code, Name: $scope.RevertItem.Name, IsStock: $scope.RevertItem.StockID > 0, StockID: $scope.RevertItem.StockID, GOPID: $scope.RevertItem.GOPID, ProductID: $scope.RevertItem.ProductID };
            $scope.AllSettingGrid_Options.dataSource.insert(0, item);

            var itemrevert = $scope.SelectedSettingGrid.dataItem($scope.SelectedSettingGrid.select())

            if (Common.HasValue(itemrevert)) {
                itemrevert.Code = "";
                itemrevert.Name = "";
                itemrevert.StockID = 0;
                itemrevert.Type = 0;
                itemrevert.GOPID = -1;
                itemrevert.ProductID = -1;
            }
            $scope.RevertItem = null;
            $scope.SelectedSettingGrid.clearSelection()
        }
    }

    Common.Services.Call($http, {
        url: Common.Services.url.CUS,
        method: _CUSSettingPODDetail.URL.Get,
        data: { id: _CUSSettingPODDetail.Param.id },
        success: function (res) {
            $scope.Item = res;
            if (_CUSSettingPODDetail.Param.id <= 0)
                $scope.Item.RowStart = 2;

            var dataAll = [];
            var dataSource = $scope.SelectedSettingGrid.dataSource;

            var dataGridAllSetting = $.extend([], true, _CUSSettingPODDetail.Data.GridAllSetting)

            Common.Data.Each(dataGridAllSetting, function (o) {
                if (!$scope.Item[o.Code] > 0) {
                    dataAll.push(o);
                }
                else {
                    var dataItem = dataSource.get($scope.Item[o.Code]);
                    dataItem.Code = o.Code; dataItem.Name = o.Name;
                }
            })

            $scope.AllSettingGrid.dataSource.data(dataAll);
        }
    });

    $scope.Back_Click = function ($event) {
        $event.preventDefault();
        $state.go("main.CUSSettingPOD.Index")
    }
}]);