/// <reference path="~/Scripts/jquery-1.9.1.intellisense.js" />
/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />

var _DIPacket = {
    URL: {
        List: 'OPS_DI_Tendering_Packet_List',
        Save: 'OPS_DI_Tendering_Packet_Save',
        Setting_Packet_List: 'OPS_DI_Tendering_Setting_Packet_List',
        CreateViaSetting: 'OPS_DI_Tendering_Packet_CreateViaSetting',
        Packet_Order_List: 'OPS_DI_Tendering_Setting_Packet_Order_List',
        Delete: 'OPS_DI_Tendering_Packet_Delete',
    }
}

angular.module('myapp').controller('OPSAppointment_DIPacketCtrl', ['$rootScope', '$scope', '$http', '$location', '$state', '$timeout', function ($rootScope, $scope, $http, $location, $state, $timeout) {
    if ($rootScope.IsPageComplete != true) return;
    Common.Log('OPSAppointment_DIPacketCtrl');
    $rootScope.IsLoading = false;
    $scope.sID = 0;

    $scope.PacketItem = { ID: -1, PacketName: '', Note:'' };
    $scope.packetGrid_Options = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.OPS,
            method: _DIPacket.URL.List,
            readparam: function () {
                return {
                }
            },
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    TonOrder: { type: 'number' },
                    QuantityOrder: { type: 'number' },
                    CBMOrder: { type: 'number' },
                }
            },
            pageSize: 100
        }),
        height: '100%', pageable: Common.PageSize, sortable: true, columnMenu: false,
        filterable: { mode: 'row' }, selectable: true, reorderable: false, resizable: true,
        columns: [
            {
                width: '85px', title: ' ', sortable: false,
                template: "<a  href='/' ng-show='dataItem.IsSend == false' class='k-button' ng-click='Packet_Edit($event,dataItem,packet_info_win)'><i class='fa fa-pencil'></i></a>" +
                    "<a  href='/' ng-show='dataItem.IsSend == false' class='k-button' ng-click='Packet_Delete($event,dataItem,packetGrid)'><i class='fa fa-trash'></i></a>"
            },
            {
                field: 'PacketName', title: 'Tên Packet', width: '250px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'Note', width: '250px', title: 'Ghi chú',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'DIPacketStatusName', width: '125px', title: 'Tình trạng',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'TonOrder', width: '100px', title: 'Tấn',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'CBMOrder', width: '100px', title: 'Khối',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'QuantityOrder', title: 'Sản lượng',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            }
        ],
        change: function () {
            $scope.HasSelect = true;
        },
        dataBound: function () {
            this.element.find('tr').dblclick(function (e) {
                e.preventDefault();
                var item = $scope.packetGrid.dataItem(this);
                if (Common.HasValue(item)) {
                    $state.go("main.OPSAppointment.DIPacket_GroupOfProduct", { packetID: item.ID });
                }
            });
        }
    }

    $scope.PacketSettingGrid_Options = {
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    CreateDate: { type: 'date' },
                }
            },
            pageSize: 100
        }),
        height: '100%', pageable: Common.PageSize, sortable: true, columnMenu: false,
        filterable: { mode: 'row' }, selectable: true, reorderable: false, resizable: true,
        columns: [
            {
                width: '45px', title: '', sortable: false, templateAttributes: { style: 'text-align: center;' },
                template: "<a  href='/' class='k-button' ng-click='PacketSetting_New($event,dataItem,PacketOrder_List_Win)'><i class='fa fa-list'></i></a>",
            },
            {
                field: 'Name', title: 'Tên', width: '250px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'CreateDate', title: 'Ngày tạo', template: "#=CreateDate==null?' ':Common.Date.FromJsonDMYHM(CreateDate)#",
                filterable: {
                    cell: {
                        template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); }, operator: 'equal', showOperators: false
                    }
                }
            }
        ],
    };

    $scope.loadData = function () {
        Common.Services.Call($http, {
            url: Common.Services.url.OPS,
            method: _DIPacket.URL.Setting_Packet_List,
            data: { fID: $rootScope.FunctionItem.ID },
            success: function (res) {
                $scope.PacketSettingGrid_Options.dataSource.data(res);
            }
        })
    };

    $scope.Packet_New = function ($event, grid, win) {
        $event.preventDefault();

        $scope.PacketItem.ID = -1;
        $scope.PacketItem.PacketName = '';
        $scope.PacketItem.Note = '';
        win.center().open();
    }

    $scope.PacketOrder_List_Options = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.OPS,
            method: _DIPacket.URL.Packet_Order_List,
            readparam: function () {
                return {
                    sID: $scope.sID
                }
            },
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                }
            },
        }),
        height: '100%', pageable: Common.PageSize, sortable: true, columnMenu: false,
        filterable: { mode: 'row' }, selectable: true, reorderable: false, resizable: true,
    columns: [
        {
            field: 'GroupProductCode', title: 'Mã nhóm s.phẩm', width: '250px',
            filterable: { cell: { operator: 'contains', showOperators: false } }
        },
        {
            field: 'GroupProductCode', title: 'Tên nhóm s.phẩm', width: '250px',
            filterable: { cell: { operator: 'contains', showOperators: false } }
        },
        {
            field: 'CustomerCode', title: 'Tên khách hàng', width: '250px',
            filterable: { cell: { operator: 'contains', showOperators: false } }
        },
        {
            field: 'ETD', title: 'ETD', template: "#=ETD==null?' ':Common.Date.FromJsonDMYHM(ETD)#",
            filterable: {
                cell: {
                    template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); }, operator: 'equal', showOperators: false
                }
            }
        },
        {
            field: 'ETA', title: 'ETA', template: "#=ETA==null?' ':Common.Date.FromJsonDMYHM(ETA)#",
            filterable: {
                cell: {
                    template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); }, operator: 'equal', showOperators: false
                }
            },
        },
       ]
    };

    $scope.PacketSetting_New = function ($event, data, win) {
        $event.preventDefault();
        $scope.sID = data.ID;
        $scope.name = data.Name + " " + Common.Date.FromJsonDMY(new Date());
        $scope.PacketOrder_List_Grid.dataSource.read();
        win.center().open();
        
    };

    $scope.PacketOrder_Click = function ($event, win) {
        $event.preventDefault();
        var data = [];
        $.each($scope.PacketOrder_List_Grid.dataSource.data(), function(i,v){
            data.push(v.ID);
        });
        $rootScope.IsLoading = true;
        Common.Services.Call($http, {
            url: Common.Services.url.OPS,
            method: _DIPacket.URL.CreateViaSetting,
            data: {sID:$scope.sID,name:$scope.name ,dataGop:data },
            success: function (res) {
                $rootScope.IsLoading = false;
                win.close();
                $rootScope.Message({
                    Msg: 'Đã cập nhật.',
                    NotifyType: Common.Message.NotifyType.SUCCESS
                });
            }
        });
    };

    $scope.PacketSetting_New_Click = function ($event, win) {
        $event.preventDefault();
        $scope.loadData();
        $timeout(function () {
            $scope.PacketSettingGrid.resize();
        });
        win.center().open();
    };

    $scope.Packet_Save_Click = function ($event, grid, win) {
        $event.preventDefault();

        if ($scope.PacketItem.PacketName == '' || $scope.PacketItem.PacketName == null) {
            $rootScope.Message({
                Msg: 'Nhập tên', NotifyType: Common.Message.NotifyType.WARNING
            })
        }
        else {
            Common.Services.Call($http, {
                url: Common.Services.url.OPS,
                method: _DIPacket.URL.Save,
                data: { item: $scope.PacketItem },
                success: function (res) {
                    Common.Services.Error(res, function () {
                        $rootScope.Message({
                            Msg: 'Đã cập nhật.',
                            NotifyType: Common.Message.NotifyType.SUCCESS
                        });

                        win.close();
                        grid.dataSource.read();
                    })
                }
            })
        }
    };

    $scope.Packet_Edit = function ($event, item, win) {
        $event.preventDefault();

        $scope.PacketItem = $.extend({}, true, item);
        win.center().open();
    }

    $scope.Packet_Delete = function ($event, item, grid) {
        $event.preventDefault();

        $rootScope.Message({
            Msg: "Xác nhận xóa?",
            Type: Common.Message.Type.Confirm,
            Ok: function () {
                $rootScope.IsLoading = true;
                Common.Services.Call($http, {
                    url: Common.Services.url.OPS,
                    method: _DIPacket.URL.Delete,
                    data: { packetID: item.ID },
                    success: function (res) {
                        Common.Services.Error(res, function () {
                            $rootScope.Message({
                                Msg: 'Đã xóa.',
                                NotifyType: Common.Message.NotifyType.SUCCESS
                            });
                            grid.dataSource.read();
                            $rootScope.IsLoading = false;
                        })
                    }
                })
            }
        })
    }

    $scope.Packet_Select = function ($event) {
        $event.preventDefault();

        var item = $scope.packetGrid.dataItem($scope.packetGrid.select());
        if (Common.HasValue(item)) {
            $state.go("main.OPSAppointment.DIPacket_GroupOfProduct", { packetID: item.ID });
        }
    }

    $scope.Back_Click = function ($event) {
        $event.preventDefault();
        $state.go("main.OPSAppointment.DI");
    }

    //#region Action
    $scope.ShowSetting = function ($event, grid) {
        $event.preventDefault();

        $rootScope.ShowSetting({
            ListView: views.OPSAppointmentDI_TEN,
            event: $event, grid: grid,
            current: $state.current
        });
    };
}]);