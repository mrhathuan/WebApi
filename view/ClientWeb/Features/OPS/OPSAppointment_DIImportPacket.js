/// <reference path="~/Scripts/jquery-1.9.1.intellisense.js" />
/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />

var _DIImportPacket = {
    URL: {
        List: 'OPS_DI_Import_Packet_List',
        Save: 'OPS_DI_Import_Packet_Save',
        Delete: 'OPS_DI_Import_Packet_Delete',
        SettingPlan_List: 'OPS_DI_Import_Packet_SettingPlan',
    }
}

angular.module('myapp').controller('OPSAppointment_DIImportPacketCtrl', ['$rootScope', '$scope', '$http', '$location', '$state', '$timeout', function ($rootScope, $scope, $http, $location, $state, $timeout) {
    Common.Log('OPSAppointment_DIImportPacketCtrl');
    $rootScope.IsLoading = false;

    $scope.PacketItem = {
        ID: -1, PacketName: '', Note: ''
    }
    $scope.IsCreated = false;

    $scope.packetGrid_Options = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.OPS,
            method: _DIImportPacket.URL.List,
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    IsCreated: { type: 'bool' },
                    TotalTon: { type: 'number' },
                    TotalCBM: { type: 'number' },
                    TotalQuantity: { type: 'number' }
                }
            },
            readparam: function () {
                return { IsCreated: $scope.IsCreated }
            },
            pageSize: 100
        }),
        height: '100%', pageable: Common.PageSize, sortable: true, columnMenu: false,
        filterable: { mode: 'row' }, selectable: true, reorderable: false, resizable: true,
        columns: [
            {
                width: '85px', title: ' ', sortable: false, headerAttributes: { style: 'text-align: right;' }, attributes: { style: 'text-align: right;' },
                template: "<a  href='/' ng-show='dataItem.IsCreated == false' class='k-button' ng-click='Packet_Edit($event,dataItem,packet_info_win)'><i class='fa fa-pencil'></i></a>" +
                    "<a  href='/' class='k-button' ng-click='Packet_Delete($event,dataItem,packetGrid)'><i class='fa fa-trash'></i></a>"
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
                field: 'TotalTon', width: '100px', title: 'Tấn', template: '#=Math.round(TotalTon*100000)/100000#',
                filterable: { cell: { operator: 'gte', showOperators: false } }
            },
            {
                field: 'TotalCBM', width: '100px', title: 'Khối', template: '#=Math.round(TotalCBM*100000)/100000#',
                filterable: { cell: { operator: 'gte', showOperators: false } }
            },
            {
                field: 'TotalQuantity', title: 'Số lượng', template: '#=Math.round(TotalQuantity*100000)/100000#',
                filterable: { cell: { operator: 'gte', showOperators: false } }
            }
        ],
        change: function () { $scope.HasSelect = true; },
        dataBound: function () {
            this.element.find('tr').dblclick(function (e) {
                e.preventDefault();
                var item = $scope.packetGrid.dataItem(this);
                if (Common.HasValue(item)) {
                    $state.go("main.OPSAppointment.DIImportPacket_Detail_Input", { ID: item.ID });
                }
            });
        }
    }

    $scope.$watch('IsCreated', function () {
        $scope.packetGrid.dataSource.read();
    });

    $scope.Packet_New = function ($event, grid, win) {
        $event.preventDefault();

        $scope.PacketItem.ID = -1;
        $scope.PacketItem.CUSSettingID = -1;
        $scope.PacketItem.PacketName = '';
        $scope.PacketItem.Note = '';
        win.center().open();
    }

    $scope.Packet_Edit = function ($event, item, win) {
        $event.preventDefault();

        $scope.PacketItem = $.extend({}, true, item);
        win.center().open();
    }

    $scope.cboSettingPlan_Options = {
        autoBind: true, valuePrimitive: true, ignoreCase: true,
        filter: 'contains', suggest: true, dataTextField: 'Name', dataValueField: 'ID',
        dataSource: Common.DataSource.Local({
            data: [], model: { id: 'ID', fields: { ID: { type: 'number' }, Name: { type: 'string' } } }
        })
    }

    Common.Services.Call($http, {
        url: Common.Services.url.OPS,
        method: _DIImportPacket.URL.SettingPlan_List,
        success: function (data) {
            data.unshift({ ID: -1, Name: '' });
            $scope.cboSettingPlan_Options.dataSource.data(data);
        }
    });

    $scope.Packet_Delete = function ($event, item, grid) {
        $event.preventDefault();

        $rootScope.Message({
            Msg: "Xác nhận xóa?",
            Type: Common.Message.Type.Confirm,
            Ok: function () {
                $rootScope.IsLoading = true;
                Common.Services.Call($http, {
                    url: Common.Services.url.OPS,
                    method: _DIImportPacket.URL.Delete,
                    data: { pID: item.ID },
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
                method: _DIImportPacket.URL.Save,
                data: { item: $scope.PacketItem },
                success: function (res) {
                    Common.Services.Error(res, function () {
                        $rootScope.Message({
                            Msg: 'Đã cập nhật.',
                            NotifyType: Common.Message.NotifyType.SUCCESS
                        });
                        win.close();
                        $state.go("main.OPSAppointment.DIImportPacket_Detail_Input", { ID: res });
                    })
                }
            })
        }
    };

    $scope.Close_Click = function ($event, win) {
        $event.preventDefault();

        win.close();
    }

    $scope.Packet_Select = function ($event, grid) {
        $event.preventDefault();

        var item = grid.dataItem(grid.select());
        if (Common.HasValue(item)) {
            $state.go("main.OPSAppointment.DIImportPacket_Detail_Input", { ID: item.ID });
        }
    }

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
}]);