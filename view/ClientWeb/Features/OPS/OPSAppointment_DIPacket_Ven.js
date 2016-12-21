/// <reference path="~/Scripts/jquery-1.9.1.intellisense.js" />
/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />

var _DIPacketVen = {
    URL: {
        List: 'OPS_DI_VEN_Tendering_Rate_List',
        GroupProduct_List: 'OPS_DI_VEN_Tendering_GroupProduct_List',
        Rate_AcceptPart: 'OPS_DI_VEN_Tendering_Rate_AcceptPart',
        Rate_Accept: 'OPS_DI_VEN_Tendering_Rate_Accept',
        Rate_Reject: 'OPS_DI_VEN_Tendering_Rate_Reject'
    }
}

angular.module('myapp').controller('OPSAppointment_DIPacket_VenCtrl', ['$rootScope', '$scope', '$http', '$location', '$state', '$timeout', function ($rootScope, $scope, $http, $location, $state, $timeout) {
    Common.Log('OPSAppointment_DIPacket_VenCtrl');
    $rootScope.IsLoading = false;

    $scope.HasChoose = false;
    $scope.IsAccept = null;
    $scope.IsRateSelected = false;
    $scope.packetDetailID = 0;
    $scope.packetDetailRateID = 0;

    $scope.packetGrid_Options = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.OPS,
            method: _DIPacketVen.URL.List,
            readparam: function () {
                return {
                    isAccept: $scope.IsAccept
                }
            },
            pageSize: 20,
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    FirstRateTime: { type: 'date' },
                    LastRateTime: { type: 'date' },
                }
            }
        }),
        height: '100%', pageable: Common.PageSize, sortable: true, columnMenu: false,
        filterable: { mode: 'row' }, selectable: true, reorderable: false, resizable: true,
        change: function (e) {
            var grid = this;
            var obj = grid.dataItem(grid.select());
            if (Common.HasValue(obj)) {
                $scope.IsRateSelected = obj.IsAccept != true;
            }
        },
        columns: [
            {
                field: 'Command', width: '85px', title: ' ',
                attributes: { style: 'text-align: center;' }, attributes: { style: 'text-align: center;' },
                template: "<a  href='/' class='k-button' title='Chi tiết' ng-click='Packet_Detail($event,dataItem,packet_info_win)'><i class='fa fa-caret-right'></i></a>" +
                    "<a  href='/' ng-show='dataItem.IsAccept' class='k-button' title='Tạo chuyến' ui-sref='main.OPSAppointment.DIPacket_Ven2View({ID:{{dataItem.ID}}, DetailID:{{dataItem.DIPacketDetailID}},VendorID:{{dataItem.VendorID}}})' ng-click='Master_Detail($event,dataItem)'><i class='fa fa-bars'></i></a>",
                sortable: false, filterable: false
            },
            {
                field: 'DetailName', title: 'Tên', width: '250px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'FirstRateTime', width: '150px', title: 'Tgian bắt đầu',
                template: "#=FirstRateTime != null ? Common.Date.ToString(FirstRateTime) : ''#",
                filterable: {
                    cell: {
                        template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); },
                        operator: 'equal', showOperators: false
                    }
                }
            },
            {
                field: 'LastRateTime', width: '150px', title: 'Tgian kết thúc',
                template: "#=LastRateTime != null ? Common.Date.ToString(LastRateTime) : ''#",
                filterable: {
                    cell: {
                        template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); },
                        operator: 'equal', showOperators: false
                    }
                }
            },
            {
                field: 'LastRateTimeConfirm', width: '150px', title: 'Tgian hoàn tất',
                template: "#=LastRateTimeConfirm != null ? Common.Date.ToString(LastRateTimeConfirm) : ''#",
                filterable: {
                    cell: {
                        template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); },
                        operator: 'equal', showOperators: false
                    }
                }
            },
            {
                field: 'TonAccept', title: 'Tấn', width: '125px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'CBMAccept', title: 'Khối', width: '125px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'QuantityAccept', title: 'Sản lượng',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            }
        ]
    }

    $scope.gopGrid_Options = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.OPS,
            method: _DIPacketVen.URL.GroupProduct_List,
            readparam: function () {
                return {
                    packetDetailID: $scope.packetDetailID
                }
            },
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    IsChoose: { type: 'bool', defaultValue: false },
                    FirstRateTime: { type: 'date' },
                    LastRateTime: { type: 'date' },
                }
            }
        }),
        height: '100%', pageable: Common.PageSize, sortable: true, columnMenu: false,
        filterable: { mode: 'row' }, selectable: true, reorderable: false, resizable: true,
        columns: [
            {
                field: 'Choose', title: ' ', width: '35px',
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,gopGrid,gopGridChoose_Change)" />',
                headerAttributes: { style: 'text-align: center;' }, attributes: { style: 'text-align: center;' },
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,gopGrid,gopGridChoose_Change)" />',
                filterable: false, sortable: false,
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
    }

    $scope.gopGridChoose_Change = function ($event, grid, hasChoose) {
        $scope.HasChoose = hasChoose;
    }

    $scope.AcceptPart_Click = function ($event, grid, win) {
        $event.preventDefault();

        var data = [];
        $.each(grid.dataSource.data(), function (i, v) {
            if (v.IsChoose == true)
                data.push(v.ID);
        });
        if (data.length > 0) {
            $rootScope.Message({
                Type: Common.Message.Type.Confirm,
                Msg: "Đồng ý nhận đơn hàng?",
                Ok: function () {
                    $rootScope.IsLoading = true;
                    Common.Services.Call($http, {
                        url: Common.Services.url.OPS,
                        method: _DIPacketVen.URL.Rate_AcceptPart,
                        data: { packetDetailRateID: $scope.packetDetailRateID, data: data },
                        success: function (res) {
                            $rootScope.IsLoading = false;
                            $rootScope.Message({ Msg: "Thành công!" });
                            $scope.packetGrid.dataSource.read();
                            win.close();
                        }
                    })
                }
            });
        }
    }

    $scope.Packet_Detail = function ($event, item, win) {
        $event.preventDefault();

        $scope.HasChoose = false;
        $scope.packetDetailRateID = item.ID;
        $scope.packetDetailID = item.DIPacketDetailID;
        $scope.gopGrid_Options.dataSource.read();
        if (item.IsAccept != true) {
            $scope.gopGrid.showColumn('Choose');
        } else {
            $scope.gopGrid.hideColumn('Choose');
        }
        win.center().open();
    }

    $scope.Rate_Accept = function ($event, grid) {
        $event.preventDefault();

        var obj = grid.dataItem(grid.select());
        if (Common.HasValue(obj)) {
            $rootScope.Message({
                Type: Common.Message.Type.Confirm,
                Msg: "Chấp nhận đơn hàng đã chọn?",
                Ok: function () {
                    $rootScope.IsLoading = true;
                    Common.Services.Call($http, {
                        url: Common.Services.url.OPS,
                        method: _DIPacketVen.URL.Rate_Accept,
                        data: { data: obj },
                        success: function (res) {
                            $rootScope.IsLoading = false;
                            $scope.IsRateSelected = false;
                            $rootScope.Message({ Msg: "Thành công!" });
                            grid.dataSource.read();
                        }
                    })
                }
            });
        }        
    }

    $scope.Rate_Reject = function ($event, grid) {
        $event.preventDefault();
        
        var obj = grid.dataItem(grid.select());
        if (Common.HasValue(obj)) {
            $rootScope.Message({
                Type: Common.Message.Type.Confirm,
                Msg: "Từ chối đơn đã chọn?",
                Ok: function () {
                    $rootScope.IsLoading = true;
                    Common.Services.Call($http, {
                        url: Common.Services.url.OPS,
                        method: _DIPacketVen.URL.Rate_Reject,
                        data: { data: obj },
                        success: function (res) {
                            $rootScope.IsLoading = false;
                            $scope.IsRateSelected = false;
                            $rootScope.Message({ Msg: "Thành công!" });
                            grid.dataSource.read();
                        }
                    })
                }
            });
        }
    }

    $scope.Refresh_Click = function ($event, grid) {
        grid.dataSource.read();
        if ($scope.IsAccept == false) {
            grid.hideColumn("Command");
            grid.hideColumn("LastRateTimeConfirm");
        }
        else {
            grid.showColumn("Command");
            grid.showColumn("LastRateTimeConfirm");
        }
    }

    $scope.Back_Click = function ($event) {
        $event.preventDefault();
        $state.go("main.OPSAppointment.DI");
    }

    $scope.Close_Click = function ($event, win) {
        $event.preventDefault();
        win.close();
    }
}]);