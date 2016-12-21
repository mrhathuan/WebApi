/// <reference path="~/Scripts/jquery-1.9.1.intellisense.js" />
/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />

var _DIPacket_PacketGroupProduct = {
    URL: {
        Get: 'OPS_DI_Tendering_Packet_Get',
        Send: 'OPS_DI_Tendering_Packet_Send',
        List: 'OPS_DI_Tendering_PacketGroupProduct_List',
        NotIn_List: 'OPS_DI_Tendering_PacketGroupProduct_NotIn_List',
        SaveList: 'OPS_DI_Tendering_PacketGroupProduct_Save',
        Remove: 'OPS_DI_Tendering_PacketGroupProduct_Remove',
        Detail_List: 'OPS_DI_Tendering_PacketDetail_List',

        Rate_List: 'OPS_DI_Tendering_PacketRate_List',
        Rate_Save: 'OPS_DI_Tendering_PacketRate_Save',
        Rate_Delete: 'OPS_DI_Tendering_PacketRate_Remove',

        VendorList: 'OPS_Tendering_Vendor_List'
    },
    Data: {
        VendorList: []
    }
}

angular.module('myapp').controller('OPSAppointment_DIPacket_GroupOfProductCtrl', ['$rootScope', '$scope', '$http', '$location', '$state', '$timeout', function ($rootScope, $scope, $http, $location, $state, $timeout) {
    if ($rootScope.IsPageComplete != true) return;
    Common.Log('OPSAppointment_DIPacket_PacketGroupProductCtrl');
    $rootScope.IsLoading = false;

    $scope.packetID = parseInt($state.params.packetID);

    $scope.HasChoose = false;
    $scope.HasChooseNoIn = false;
    $scope.IsSend = false;
    $scope.PacketName = "";
    $scope.RateEdit = {};

    Common.Services.Call($http, {
        url: Common.Services.url.OPS,
        method: _DIPacket_PacketGroupProduct.URL.Get,
        data: { packetID: $scope.packetID, request: '' },
        success: function (res) {
            $scope.PacketName = res.PacketName;
            $scope.IsSend = res.IsSend;
            if ($scope.IsSend == true) {
                $timeout(function () {
                    $scope.ResetView();
                }, 1)
            }

            Common.Services.Call($http, {
                url: Common.Services.url.OPS,
                method: _DIPacket_PacketGroupProduct.URL.VendorList,
                data: {},
                success: function (res) {
                    if (Common.HasValue(res)) {
                        $timeout(function () {
                            _DIPacket_PacketGroupProduct.Data.VendorList = res.Data;
                            $scope.cboPacketRateVendor_Options.dataSource.data(res.Data);
                        }, 1);
                    }
                }
            });
        }
    });

    $scope.tabstripOptions = {
        animation: {
            open: { effects: "fadeIn" }
        }
    }

    $scope.gopGrid_Options = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.OPS,
            method: _DIPacket_PacketGroupProduct.URL.List,
            readparam: function () {
                return {
                    packetID: $scope.packetID
                }
            },
            pageSize: 100,
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    Ton: { type: 'number' },
                    CBM: { type: 'number' },
                    Quantity: { type: 'number' },
                    IsChoose: { type: 'bool', defaultValue: false },
                    ETD: { type: 'date' },
                    ETA: { type: 'date' },
                    ETDStart: { type: 'date' },
                    ETAStart: { type: 'date' }
                }
            }
        }),
        height: '100%', pageable: Common.PageSize, sortable: true, columnMenu: false, resizable: true, reorderable: false, filterable: { mode: 'row' },
        toolbar: $scope.IsSend == false ? kendo.template($("#gopGrid_toolbar").html()) : false,
        columns: [
            {
                field: 'Choose', title: '', width: '35px',
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,gopGrid,gopGridChoose_Change)" />',
                headerAttributes: { style: 'text-align: center;' }, attributes: { style: 'text-align: center;' },
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,gopGrid,gopGridChoose_Change)" />',
                filterable: false, sortable: false, hidden: $scope.IsSend == true ? true : false
            },
            {
                field: 'GroupProductName', width: '150px', title: 'Nhóm sản phẩm',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'OrderCode', width: '100px', title: 'Mã ĐH',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'CustomerCode', width: '110px', title: 'Khách hàng',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'Ton', width: '80px', title: 'Tấn', template: '',
                filterable: { cell: { operator: 'gte', showOperators: false } }
            },
            {
                field: 'CBM', width: '80px', title: 'Khối', template: '',
                filterable: { cell: { operator: 'gte', showOperators: false } }
            },
            {
                field: 'Quantity', width: '80px', title: 'Sản lượng', template: '',
                filterable: { cell: { operator: 'gte', showOperators: false } }
            },
            {
                field: 'DNCode', width: '100px', title: 'Số DN',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'ETD', width: '130px', title: 'ETD',
                template: "#=ETD==null?' ':Common.Date.ToString(ETD)#",
                filterable: {
                    cell: {
                        template: function (e) {
                            e.element.kendoDatePicker({ format: Common.Date.Format.DMY });
                        },
                        operator: 'equal', showOperators: false
                    }
                },
            },
            {
                field: 'ETA', width: '130px', title: 'ETA',
                template: "#=ETA==null?' ':Common.Date.ToString(ETA)#",
                filterable: {
                    cell: {
                        template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); },
                        operator: 'equal', showOperators: false
                    }
                },
            },
            {
                field: 'Note1', width: '180px', title: 'Ghi chú',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'PartnerCode', width: '100px', title: 'NPP',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'LocationFromName', width: '150px', title: 'Điểm nhận',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'LocationFromAddress', width: '200px', title: 'Địa chỉ',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'LocationFromProvince', width: '100px', title: 'Tỉnh thành',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'LocationFromDistrict', width: '100px', title: 'Quận huyện',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'LocationToName', width: '150px', title: 'Điểm giao',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'LocationToAddress', width: '200px', title: 'Địa chỉ',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'LocationToDistrict', width: '100px', title: 'Quận huyện',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'LocationToProvince', width: '100px', title: 'Tỉnh thành',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            { title: ' ', filterable: false, menu: false, sortable: false }
        ]
    };

    $scope.gopNotInGrid_Options = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.OPS,
            method: _DIPacket_PacketGroupProduct.URL.NotIn_List,
            readparam: function () {
                return {
                    packetID: $scope.packetID
                }
            },
            pageSize: 100,
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    Ton: { type: 'number' },
                    CBM: { type: 'number' },
                    Quantity: { type: 'number' },
                    ETD: { type: 'date' },
                    ETA: { type: 'date' },
                    IsChoose: { type: 'bool', defaultValue: false }
                }
            }
        }),
        height: '100%', pageable: Common.PageSize, sortable: true, columnMenu: false, resizable: true, reorderable: false, filterable: { mode: 'row' },
        columns: [
            {
                field: 'Choose', title: ' ', width: '35px',
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,gopNotInGrid,gopNotInGridChoose_Change)" />',
                headerAttributes: { style: 'text-align: center;' }, attributes: { style: 'text-align: center;' },
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,gopNotInGrid,gopNotInGridChoose_Change)" />',
                filterable: false, sortable: false
            },
            {
                field: 'GroupProductName', width: '150px', title: 'Nhóm sản phẩm',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'OrderCode', width: '100px', title: 'Mã ĐH',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'CustomerCode', width: '110px', title: 'Khách hàng',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'Ton', width: '80px', title: 'Tấn', template: '',
                filterable: { cell: { operator: 'gte', showOperators: false } }
            },
            {
                field: 'CBM', width: '80px', title: 'Khối', template: '',
                filterable: { cell: { operator: 'gte', showOperators: false } }
            },
            {
                field: 'Quantity', width: '80px', title: 'Sản lượng', template: '',
                filterable: { cell: { operator: 'gte', showOperators: false } }
            },
            {
                field: 'DNCode', width: '100px', title: 'Số DN',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'ETD', width: '130px', title: 'ETD',
                template: "#=ETD==null?' ':Common.Date.ToString(ETD)#",
                filterable: {
                    cell: {
                        template: function (e) {
                            e.element.kendoDatePicker({ format: Common.Date.Format.DMY });
                        },
                        operator: 'equal', showOperators: false
                    }
                },
            },
            {
                field: 'ETA', width: '130px', title: 'ETA',
                template: "#=ETA==null?' ':Common.Date.ToString(ETA)#",
                filterable: {
                    cell: {
                        template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); },
                        operator: 'equal', showOperators: false
                    }
                },
            },
            {
                field: 'Note1', width: '180px', title: 'Ghi chú',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'PartnerCode', width: '100px', title: 'NPP',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'LocationFromName', width: '150px', title: 'Điểm nhận',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'LocationFromAddress', width: '200px', title: 'Địa chỉ',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'LocationFromProvince', width: '100px', title: 'Tỉnh thành',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'LocationFromDistrict', width: '100px', title: 'Quận huyện',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'LocationToName', width: '150px', title: 'Điểm giao',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'LocationToAddress', width: '200px', title: 'Địa chỉ',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'LocationToDistrict', width: '100px', title: 'Quận huyện',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'LocationToProvince', width: '100px', title: 'Tỉnh thành',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            { title: ' ', filterable: false, menu: false, sortable: false }
        ]
    };

    $scope.rateGrid_Options = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.OPS,
            method: _DIPacket_PacketGroupProduct.URL.Rate_List,
            readparam: function () {
                return {
                    packetID: $scope.packetID
                }
            },
            pageSize: 100,
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    RateTime: { type: 'number' },
                    RateTimeConfirm: { type: 'number' },
                }
            }
        }),
        height: '100%', pageable: Common.PageSize, sortable: true, columnMenu: false, resizable: true, reorderable: false, filterable: { mode: 'row' },
        toolbar: $scope.IsSend == false ? kendo.template($("#rateGrid_toolbar").html()) : false,
        columns: [
            {
                field: 'Command', title: ' ', width: '85px',
                headerAttributes: { style: 'text-align: center;' }, attributes: { style: 'text-align: center;' },
                template: '<a class="k-button" href="/" ng-click="Rate_Edit($event,dataItem,gopPacketRateEdit_win)"><i class="fa fa-pencil"></i></a>' +
                    '<a class="k-button" ng-click="Rate_Delete($event,dataItem,rateGrid)"><i class="fa fa-trash"></i></a>',
                filterable: false, sortable: false, hidden: $scope.IsSend == false ? false : true,
            },
            {
                field: 'SortOrder', width: '60px', title: 'STT', filterable: false, sortable: false, headerAttributes: { style: 'text-align: center;' }, attributes: { style: 'text-align: center;' },
            },
            {
                field: 'VendorName', width: '300px', title: 'Tên đối tác',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'RateTime', width: '100px', title: 'Tgian đồng ý',
                filterable: { cell: { operator: 'gte', showOperators: false } }
            },
            {
                field: 'RateTimeConfirm', title: 'Tgian xác nhận',
                filterable: { cell: { operator: 'gte', showOperators: false } }
            }
        ]
    };

    $scope.detailGrid_Options = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.OPS,
            method: _DIPacket_PacketGroupProduct.URL.Detail_List,
            readparam: function () {
                return {
                    packetID: $scope.packetID
                }
            },
            pageSize: 100,
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    TonOrder: { type: 'number' },
                    QuantityOrder: { type: 'number' },
                    CBMOrder: { type: 'number' },
                    TonAccept: { type: 'number' },
                    CBMAccept: { type: 'number' },
                    QuantityAccept: { type: 'number' }
                }
            }
        }),
        height: '100%', pageable: Common.PageSize, sortable: true, columnMenu: false, resizable: true, reorderable: false, filterable: { mode: 'row' },
        columns: [
            {
                field: 'DetailName', width: '120px', title: 'Tên Packet',
                filterable: false, sortable: false
            },
            {
                field: 'VendorName', width: '150px', title: 'Tên đối tác',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'Note', width: '100px', title: 'Ghi chú',
                filterable: { cell: { operator: 'gte', showOperators: false } }
            },
            {
                field: 'TonOrder', width: '80px', title: 'Tấn',
                template: "#=TonOrder > 0 ? Common.Number.ToNumber5(TonOrder) : 0#",
                filterable: { cell: { operator: 'gte', showOperators: false } }
            },
            {
                field: 'CBMOrder', width: '80px', title: 'Khối',
                template: "#=CBMOrder > 0 ? Common.Number.ToNumber5(CBMOrder) : 0#",
                filterable: { cell: { operator: 'gte', showOperators: false } }
            },
            {
                field: 'QuantityOrder', width: '80px', title: 'S.Lượng',
                template: "#=QuantityOrder > 0 ? Common.Number.ToNumber5(QuantityOrder) : 0#",
                filterable: { cell: { operator: 'gte', showOperators: false } }
            },
            {
                field: 'TonAccept', width: '120px', title: 'Tấn chấp nhận',
                template: "#=TonAccept > 0 ? Common.Number.ToNumber5(TonAccept) : 0#",
                filterable: { cell: { operator: 'gte', showOperators: false } }
            },
            {
                field: 'CBMAccept', width: '120px', title: 'Khối chấp nhận',
                template: "#=CBMAccept > 0 ? Common.Number.ToNumber5(CBMAccept) : 0#",
                filterable: { cell: { operator: 'gte', showOperators: false } }
            },
            {
                field: 'QuantityAccept', width: '150px', title: 'S.Lượng chấp nhận',
                template: "#=QuantityAccept > 0 ? Common.Number.ToNumber5(QuantityAccept) : 0#",
                filterable: { cell: { operator: 'gte', showOperators: false } }
            },
            {
                field: 'TonRemain', width: '150px', title: 'Tấn từ chối',
                template: "#=TonRemain > 0 ? Common.Number.ToNumber5(TonRemain) : 0#",
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'CBMRemain', width: '150px', title: 'Khối từ chối',
                template: "#=CBMRemain > 0 ? Common.Number.ToNumber5(CBMRemain) : 0#",
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'QuantityRemain', width: '150px', title: 'S.Lượng từ chối',
                template: "#=QuantityRemain > 0 ? Common.Number.ToNumber5(QuantityRemain) : 0#",
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            { title: ' ', filterable: false, menu: false, sortable: false }
        ]
    };

    $scope.gopGridChoose_Change = function ($event, grid, hasChoose) {
        $scope.HasChoose = hasChoose;
    }

    $scope.gopNotInGridChoose_Change = function ($event, grid, hasChoose) {
        $scope.HasChooseNoIn = hasChoose;
    }

    $scope.cboPacketRateVendor_Options = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true, minLength: 3,
        dataTextField: 'CustomerName', dataValueField: 'ID',
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    CustomerName: { type: 'string' },
                }
            }
        })
    };

    $scope.Send_Click = function ($event) {
        $event.preventDefault();

        $rootScope.Message({
            Type: Common.Message.Type.Confirm,
            Msg: "Đồng ý gửi đơn hàng đến đối tác?",
            Ok: function () {
                $rootScope.IsLoading = true;
                Common.Services.Call($http, {
                    url: Common.Services.url.OPS,
                    method: _DIPacket_PacketGroupProduct.URL.Send,
                    data: { packetID: $scope.packetID },
                    success: function (res) {
                        $rootScope.IsLoading = false;
                        $rootScope.Message({
                            Msg: 'Đã gửi.',
                            NotifyType: Common.Message.NotifyType.SUCCESS
                        });
                        $scope.IsSend = true;
                        $timeout(function () {
                            $scope.ResetView();
                        }, 1)
                    }
                })
            }
        });
    }

    $scope.Add_Gop_Click = function ($event, grid, win) {
        $event.preventDefault();

        grid.dataSource.read();
        $timeout(function () {
            win.center().open();
        }, 100)
    }

    $scope.Accept_Click = function ($event, grid, win) {
        $event.preventDefault();

        var data = [];
        Common.Data.Each(grid.dataSource.data(), function (o) {
            if (o.IsChoose == true)
                data.push(o.ID);
        })
        if (data.length > 0) {
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.OPS,
                method: _DIPacket_PacketGroupProduct.URL.SaveList,
                data: { packetID: $scope.packetID, data: data },
                success: function (res) {
                    Common.Services.Error(res, function () {
                        $rootScope.Message({
                            Msg: 'Đã cập nhật.',
                            NotifyType: Common.Message.NotifyType.SUCCESS
                        });
                        $rootScope.IsLoading = false;
                        win.close();
                        $scope.gopGrid_Options.dataSource.read();
                    })
                }
            })
        } else {
            win.close();
        }
    }

    $scope.Delete_Click = function ($event, grid) {
        $event.preventDefault();
        var lstID = [];
        $.each(grid.dataSource.data(), function (i, v) {
            if (v.IsChoose == true)
                lstID.push(v.ID);
        });
        if (lstID.length > 0) {
            $rootScope.Message({
                Type: Common.Message.Type.Confirm,
                Msg: "Bạn muốn xóa các đơn hàng đã chọn?",
                Ok: function () {
                    $rootScope.IsLoading = true;
                    Common.Services.Call($http, {
                        url: Common.Services.url.OPS,
                        method: _DIPacket_PacketGroupProduct.URL.Remove,
                        data: { packetID: $scope.packetID, data: lstID },
                        success: function (res) {
                            $rootScope.IsLoading = false;
                            Common.Services.Error(res, function (res) {
                                $rootScope.Message({ Msg: "Đã xóa!" });
                                grid.dataSource.read();
                            })
                        }
                    })
                }
            });
        }
    }

    $scope.Add_Rate_Click = function ($event, win) {
        $event.preventDefault();

        $scope.RateEdit.ID = 0;
        $scope.RateEdit.RateTime = 2;
        $scope.RateEdit.RateTimeConfirm = 2;
        win.center().open();
    }

    $scope.Rate_Edit = function ($event, data, win) {
        $event.preventDefault();

        $scope.cboPacketRate_Options.dataSource.data(_DIPacket_PacketGroupProduct.Data.VendorList);
        $scope.RateEdit = data;
        win.open().center();
    }

    $scope.Save_Click = function ($event, win) {
        $event.preventDefault();

        Common.Services.Call($http, {
            url: Common.Services.url.OPS,
            method: _DIPacket_PacketGroupProduct.URL.Rate_Save,
            data: { packetID: $scope.packetID, data: $scope.RateEdit },
            success: function (res) {
                Common.Services.Error(res, function () {
                    $rootScope.Message({
                        Msg: 'Đã cập nhật.',
                        NotifyType: Common.Message.NotifyType.SUCCESS
                    });
                    $rootScope.IsLoading = false;
                    win.close();
                    $scope.rateGrid.dataSource.read();
                })
            }
        })

    }

    $scope.Rate_Delete = function ($event, data, grid) {
        $event.preventDefault();

        $rootScope.Message({
            Type: Common.Message.Type.Confirm,
            Msg: "Bạn muốn xóa danh sách nhà xe đã chọn?",
            Ok: function () {
                $rootScope.IsLoading = true;
                Common.Services.Call($http, {
                    url: Common.Services.url.OPS,
                    method: _DIPacket_PacketGroupProduct.URL.Rate_Delete,
                    data: { packetID: $scope.packetID, data: data },
                    success: function (res) {
                        $rootScope.IsLoading = false;
                        Common.Services.Error(res, function (res) {
                            $rootScope.Message({ Msg: "Đã xóa!" });
                            grid.dataSource.read();
                        })
                    }
                })
            }
        });
    }

    $scope.Close_Click = function ($event, win) {
        $event.preventDefault();
        win.close();
    }

    $scope.Back_Click = function ($event) {
        $event.preventDefault();
        $state.go("main.OPSAppointment.DIPacket");
    }

    $scope.ResetView = function () {
        $timeout(function () {
            $scope.gopGrid.hideColumn("Choose");
            $scope.rateGrid.hideColumn("Command");
        }, 1)
        $scope.gopGrid.setOptions({ toolbar: false });
        $scope.rateGrid.setOptions({ toolbar: false });
    }
}]);