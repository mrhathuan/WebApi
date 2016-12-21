/// <reference path="~/Scripts/jquery-1.9.1.intellisense.js" />
/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />

var _CUSSettingExtReturnDetail = {
    URL: {
        Get: 'CUSSettingExtReturn_Get',
        Save: 'CUSSettingExtReturn_Save',
    },
    Data: {
        DataCustomer: [],
        DataTransport: [
            { ValueOfVar: 'Phân phối', ID: 1 },
            { ValueOfVar: 'Container', ID: 2 },
        ],
        DataContract: [],
        GridAllSetting: [
            { Required: false, Type: 0, Code: "ID", Name: "ID", IsKey: true },
            { Required: true, Type: 0, Code: "InvoiceNo", Name: "Số chứng từ", IsKey: false },
            { Required: true, Type: 0, Code: "InvoiceDate", Name: "Ngày chứng từ", IsKey: false },
            { Required: false, Type: 0, Code: "DITOMasterCode", Name: "Mã chuyến", IsKey: false },
            { Required: false, Type: 0, Code: "CustomerCode", Name: "Mã khách hàng", IsKey: false },
            { Required: false, Type: 0, Code: "VendorCode", Name: "Mã nhà xe", IsKey: false },
            { Required: false, Type: 0, Code: "VendorName", Name: "Tên nhà xe", IsKey: false },
            { Required: false, Type: 0, Code: "VehicleNo", Name: "Xe", IsKey: false },
            { Required: false, Type: 0, Code: "GroupProductCode", Name: "Mã nhóm sản phẩm", IsKey: false },
            { Required: false, Type: 0, Code: "Packing", Name: "Mã sản phẩm", IsKey: false },
            { Required: false, Type: 0, Code: "OrderCode", Name: "Mã ĐH", IsKey: false },
            { Required: false, Type: 0, Code: "DNCode", Name: "Số DN", IsKey: false },
            { Required: false, Type: 0, Code: "SOCode", Name: "Số SO", IsKey: false },
            { Required: false, Type: 0, Code: "RequestDate", Name: "Ngày yêu cầu", IsKey: false },
            { Required: false, Type: 0, Code: "ETD", Name: "ETD", IsKey: false },
            { Required: false, Type: 0, Code: "ETA", Name: "ETA", IsKey: false },
            { Required: false, Type: 0, Code: "QuantityCurrent", Name: "Số lượng còn lại", IsKey: false },
            { Required: true, Type: 0, Code: "Quantity", Name: "Số lượng", IsKey: false },
            { Required: true, Type: 0, Code: "ExtReturnStatusCode", Name: "Loại trả về", IsKey: false },
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

angular.module('myapp').controller('CUSSettingExtReturn_DetailCtrl', ['$rootScope', '$scope', '$http', '$location', '$state', '$timeout', '$stateParams', function ($rootScope, $scope, $http, $location, $state, $timeout, $stateParams) {
    Common.Log('CUSSettingExtReturn_DetailCtrl');
    $rootScope.IsLoading = false;

    _CUSSettingExtReturnDetail.Param = $.extend(true, _CUSSettingExtReturnDetail.Param, $state.params);

    $scope.TranferItem = null;
    $scope.RevertItem = null;
    $scope.PreItem = null;

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

    // $scope.AllSettingGrid_Options.dataSource.data(_CUSSettingExtReturnDetail.Data.GridAllSetting)

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
                template: "<input type='checkbox' ng-model='dataItem.IsKey' ng-change='change_key(dataItem)'></input>",
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
        if (data.Code == "InvoiceNo" || data.Code == "InvoiceDate" || data.Code == "QuantityCurrent" || data.Code == "Quantity" || data.Code == "ExtReturnStatusCode") {
            if (data.IsKey) {
                $rootScope.Message({ Msg: 'Cột không được phép làm khóa.', NotifyType: Common.Message.NotifyType.ERROR });
                data.IsKey = false;
            }
        }
    }

    $scope.SelectedSettingGrid_Options.dataSource.data(_CUSSettingExtReturnDetail.Data.GridSelected)

    $scope.ResetSetting_Click = function ($event) {
        $event.preventDefault();
    }

    $scope.Save_Click = function ($event) {
        $event.preventDefault();
        if (Common.HasValue($scope.Item.Name) && $scope.Item.Name != "") {
            $rootScope.IsLoading = true;
            var data = $scope.SelectedSettingGrid.dataSource.data();

            var ItemSend = {
                ID: $scope.Item.ID,
                Name: $scope.Item.Name,
                RowStart: $scope.Item.RowStart,
            }

            //var hasRegNo = false;
            //var hasETD = false;
            //var hasOrderCode = false;
            //var hasLocationToCode = false;
            //var hasTon = false;
            //var hasCBM = false;
            //var hasQuantity = false;
            var hasQuantityExtReturn = false;
            var hasExtReturnStatusCode = false;

            var hasKey = false;
            Common.Data.Each(data, function (o) {
                if (o.Code != "" && o.Name != "") {
                    ItemSend[o.Code] = o.IndexColumn;
                    ItemSend[o.Code + "Key"] = o.IsKey;
                    if (o.Code == "Quantity") {
                        hasQuantityExtReturn = true;
                    }
                    if (o.Code == "ExtReturnStatusCode") {
                        hasExtReturnStatusCode = true;
                    }
                    if (!hasKey && o.IsKey) {
                        hasKey = true;
                    }
                }
            })

            var hasError = false;
            
            if (!hasKey) {
                $rootScope.Message({ Msg: 'Thiếu khóa kiểm tra', NotifyType: Common.Message.NotifyType.ERROR });
                hasError = true;
                $rootScope.IsLoading = false;
            }

            if (!hasQuantityExtReturn)
            {
                $rootScope.Message({ Msg: 'Thiếu cột số lượng', NotifyType: Common.Message.NotifyType.ERROR });
                hasError = true;
                $rootScope.IsLoading = false;
            }

            if (!hasExtReturnStatusCode) {
                $rootScope.Message({ Msg: 'Thiếu cột mã loại trả về', NotifyType: Common.Message.NotifyType.ERROR });
                hasError = true;
                $rootScope.IsLoading = false;
            }

            if (!hasError) {
                Common.Services.Call($http, {
                    url: Common.Services.url.CUS,
                    method: _CUSSettingExtReturnDetail.URL.Save,
                    data: { item: ItemSend, id: _CUSSettingExtReturnDetail.Param.id },
                    success: function (res) {
                        $rootScope.IsLoading = false;
                        $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                        $state.go("main.CUSSettingMonitor.ExtReturn")
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
        method: _CUSSettingExtReturnDetail.URL.Get,
        data: { id: _CUSSettingExtReturnDetail.Param.id },
        success: function (res) {
            $scope.Item = res;
            if (_CUSSettingExtReturnDetail.Param.id <= 0)
                $scope.Item.RowStart = 2;

            var dataAll = [];
            var dataSource = $scope.SelectedSettingGrid.dataSource;

            var dataGridAllSetting = $.extend([], true, _CUSSettingExtReturnDetail.Data.GridAllSetting)

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
        $state.go("main.CUSSettingMonitor.ExtReturn")
    }
}]);