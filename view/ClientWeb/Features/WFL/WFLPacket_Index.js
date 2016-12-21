//ban moi

/// <reference path="~/Scripts/jquery-1.9.1.intellisense.js" />
/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />
//#region Data
var _WFLPacket_Index = {
    URL: {
        Read: 'WFLPacket_Read',
        Get: 'WFLPacket_Get',
        Save: 'WFLPacket_Save',
        Delete: 'WFLPacket_Delete',
        WFLPacket_Send: "WFLPacket_Send",
        GroupProductList: 'WFLPacket_ORDGroupProductList',
        GroupProductNotInList: 'WFLPacket_ORDGroupProductNotInList',
        GroupProductSaveList: 'WFLPacket_ORDGroupProductSaveList',

        DITOMasterList: 'WFLPacket_DITOMasterList',
        DITOMasterNotInList: 'WFLPacket_DITOMasterNotInList',
        DITOMasterSaveList: 'WFLPacket_DITOMasterSaveList',

        COTOMasterList: 'WFLPacket_COTOMasterList',
        COTOMasterNotInList: 'WFLPacket_COTOMasterNotInList',
        COTOMasterSaveList: 'WFLPacket_COTOMasterSaveList',

        DeleteList: 'WFLPacket_DetailDelete',

        SettingAllList: 'WFLPacket_SettingAllList',
        
    },
    Data: {
        packet_process: [],
    }
};
//#endregion

angular.module('myapp').controller('WFLPacket_IndexCtrl', ['$rootScope', '$scope', '$http', '$location', '$state', '$timeout', '$stateParams', function ($rootScope, $scope, $http, $location, $state, $timeout, $stateParams) {
    if ($rootScope.IsPageComplete != true) return;
    Common.Log('WFLPacket_IndexCtrl');
    $rootScope.IsLoading = false;

    $scope.Auth = $rootScope.GetAuth();

    $scope.packetID = 0;
    $scope.groupProductListHasChoose = false;
    $scope.GroupProductNotInHasChoose = false;
    $scope.DIMasterListHasChoose = false;
    $scope.DITOMasterNotInHasChoose = false;
    $scope.COMasterListHasChoose = false;
    $scope.COTOMasterNotInHasChoose = false;
    $scope.HasChoose = false;

    $scope.Packet_GridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.WFL,
            method: _WFLPacket_Index.URL.Read,
            model: {
                    id: 'ID',
                    fields: {
                        ID: { type: 'number' },
                        IsChoose: { type: 'bool', defaultValue: false },
                        PacketDate: { type: 'date' },
                    }
            },
            pageSize:20,
            readparam: function () {return {} },
        }),
        height: '100%', pageable: true, sortable: true, columnMenu: false, resizable: true, reorderable: true, filterable: { mode: 'row' },
        columns: [
             {
                 title: ' ', width: '40px',
                 headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridCheck_All($event,Packet_Grid)" />',
                 headerAttributes: { style: 'text-align: center;' },
                 template: '<input class="chkChoose" ng-show="dataItem.IsCheck == true" ng-click="gridCheck($event)" type="checkbox" #= IsChoose ? "checked=checked" : ""#/>',
                 templateAttributes: { style: 'text-align: center;' },
                 filterable: false, sortable: false
             },
            {
                title: ' ', width: '85px',
                template: '<a href="/" ng-click="Packet_Edit_Click($event,dataItem,WFLPaking_win)" class="k-button"><i class="fa fa-pencil"></i></a>'+
                          '<a href="/" ng-click="Packet_Delete_Click($event,dataItem)" class="k-button"><i class="fa fa-trash"></i></a>',
                filterable: false, sortable: false
            },
            {
                field: 'PacketProcess', title: 'Trạng thái', width: '100px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'SettingName', title: 'Tên', width: '150px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'SettingType', title: 'Loại thiết lập', width: '150px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'CustomerName', title: 'Tên khách hàng', width: '150px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            { 
                field: 'PacketDate', width: '130px', title: 'Ngày',template: "#=PacketDate==null?' ':Common.Date.FromJsonDMY(PacketDate)#",
                filterable: {
                    cell: {
                        template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); },
                        operator: 'equal', showOperators: false,
                    }
                },
            },
            { title: '', sortable: false, filterable: false, menu: false }
        ],
    };

    $scope.gridCheck_All = function ($event, grid) {
        if ($event.target.checked == true) {
            grid.items().each(function () {
                var tr = this;
                var item = grid.dataItem(tr);
                if (item.IsChoose != true) {
                    $(tr).find('td input.chkChoose').prop('checked', true);
                    item.IsChoose = true;
                }
            });
            $scope.HasChoose = true;
        }
        else {
            grid.items().each(function () {
                var tr = this;
                var item = grid.dataItem(tr);
                if (item.IsChoose == true) {
                    $(tr).find('td input.chkChoose').prop('checked', false);
                    item.IsChoose = false;
                }
            });
            $scope.HasChoose = false;
        }
    };

    $scope.gridCheck = function ($event) {
        if ($event.target.checked == true) {
            $scope.HasChoose = true;
        }
        else {
            $scope.HasChoose = false;
        }
    }

    $scope.Packet_Delete_Click = function ($event, data) {
        $event.preventDefault();
        $rootScope.IsLoading = true;
        Common.Services.Call($http, {
            url: Common.Services.url.WFL,
            method: _WFLPacket_Index.URL.Delete,
            data:{id: data.ID},
            success: function (res) {
                $scope.Packet_GridOptions.dataSource.read();
                $rootScope.Message({
                    Msg: 'Đã xóa.',
                    NotifyType: Common.Message.NotifyType.SUCCESS
                });
                $rootScope.IsLoading = false;
            }
        });
    }

    $scope.Packet_ChangeProcess_Click = function ($event, grid) {
        $event.preventDefault();
        $rootScope.Message({
            Msg: "Thay đổi trạng thái?",
            Type: Common.Message.Type.Confirm,
            Ok: function () {
                $rootScope.IsLoading = true;
                data = grid.dataSource.data();
                var lst = [];
                $.each(data, function (i, v) {
                    if (v.IsChoose == true && v.IsCheck == true) {
                        lst.push(v.ID);
                    }
                });
                Common.Services.Call($http, {
                    url: Common.Services.url.WFL,
                    method: _WFLPacket_Index.URL.WFLPacket_Send,
                    data: { lstid: lst },
                    success: function (res) {
                        grid.dataSource.read();
                        $rootScope.IsLoading = false;
                        $scope.HasChoose = false;
                        $rootScope.Message({
                            Msg: 'Đã cập nhật.',
                            NotifyType: Common.Message.NotifyType.SUCCESS
                        });
                    }
                })
            }
        })
    }

    $scope.cboPacketSetting_Options = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true, dataTextField: 'SettingName', dataValueField: 'ID',
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    SettingName: { type: 'string' },
                }
            }
        }),
        change: function (e) {
        }
    };

    $scope.cboPacketProcess_Options = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true, dataTextField: 'ValueOfVar', dataValueField: 'ID',
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    ValueOfVar: { type: 'string' },
                }
            }
        }),
        change: function (e) {
        }
    };

    Common.Services.Call($http, {
        url: Common.Services.url.WFL,
        method: _WFLPacket_Index.URL.SettingAllList,
        success: function (res) {
            $scope.cboPacketSetting_Options.dataSource.data(res);
        }
    });

    Common.ALL.Get($http, {
        url: Common.ALL.URL.ALL_SYSVarPacketProcessType,
        success: function (data) {
            _WFLPacket_Index.Data.packet_process = data;
            $scope.cboPacketProcess_Options.dataSource.data(data);
        }
    })


    $scope.TabIndex = 1;
    $scope.Packet_win_tabOptions = {
        animation: {
            open: { effects: "fadeIn" }
        },
        select: function (e) {
            $timeout(function () {
                $scope.TabIndex = angular.element(e.item).data('tabindex'); //or
            }, 1);
        }
    }

    $scope.Packet_Add_Click = function ($event, win) {
        $event.preventDefault();
        $scope.LoadData_Packet(0, win);
    };

    $scope.Packet_Edit_Click = function ($event, data, win) {
        $event.preventDefault();
        $scope.LoadData_Packet(data.ID, win);
    };

    $scope.FeeDefault_Edit_Click = function ($event, data, win) {
        $event.preventDefault();
        $scope.LoadData_FeeDefault(data.ID, win);
    };

    $scope.LoadData_Packet = function (ID, win) {
        $rootScope.IsLoading = true;
        Common.Services.Call($http, {
            url: Common.Services.url.WFL,
            method: _WFLPacket_Index.URL.Get,
            data: { id: ID },
            success: function (res) {
                if (Common.HasValue(res)) {
                    $scope.Item = res;
                    if(ID < 1)
                        $scope.Item.PacketDate = new Date();
                    $scope.packetID = ID;
                    if ($scope.Item.ID < 0) {
                        if (Common.HasValue(_WFLPacket_Index.Data.packet_process))
                            $scope.Item.PacketProcessID = _WFLPacket_Index.Data.packet_process[0].ID;
                    }
                    win.center().open();

                    $scope.groupProductList_GridOptions.dataSource.read();
                    $scope.DIMasterList_GridOptions.dataSource.read();
                    $scope.COMasterList_GridOptions.dataSource.read();

                    $rootScope.IsLoading = false;

                }
            }
        });
    }

    $scope.Save_Click = function ($event, win, vform) {
        $event.preventDefault();
        if (vform()) {
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.WFL,
                method: _WFLPacket_Index.URL.Save,
                data: { item: $scope.Item},
                success: function (res) {
                    if (Common.HasValue(res)) {
                        $rootScope.Message({
                            Msg: 'Đã cập nhật.',
                            NotifyType: Common.Message.NotifyType.SUCCESS
                        });
                        $scope.Packet_Grid.dataSource.read();
                        $scope.packetID = res;
                        Common.Services.Call($http, {
                            url: Common.Services.url.WFL,
                            method: _WFLPacket_Index.URL.Get,
                            data: { id: res },
                            success: function (r) {
                                if (Common.HasValue(r)) {
                                    $scope.Item = r;
                                    if ($scope.Item.ID < 0) {
                                        if (Common.HasValue(_WFLPacket_Index.Data.packet_process))
                                            $scope.Item.PacketProcessID = _WFLPacket_Index.Data.packet_process[0].ID;
                                    }
                                    $rootScope.IsLoading = false;

                                }
                            }
                        });

                        $rootScope.IsLoading = false;
                    }
                }
            });
        } else {
            $rootScope.Message({
                Msg: 'Lỗi dữ liệu.',
                NotifyType: Common.Message.NotifyType.ERROR
            });
        }
    }

    //#region GroupProduct
    $scope.groupProductList_GridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.WFL,
            method: _WFLPacket_Index.URL.GroupProductList,
            readparam: function () { return { packetID: $scope.packetID } },
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number', editable: false, nullable: true },
                    IsChoose: { type: 'boolean', },
                }
            }
        }),
        height: '100%', pageable: true, sortable: true, columnMenu: false, filterable: { mode: 'row' }, resizable: true, editable: false,
        columns: [
            {
                title: ' ', width: '35px',
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,groupProductList_Grid,groupProductList_GridChoose_Change)" />',
                headerAttributes: { style: 'text-align: center;' }, attributes: { style: 'text-align: center;' },
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,groupProductList_Grid,groupProductList_GridChoose_Change)" />',
                filterable: false, sortable: false
            },
            { field: 'OrderCode', title: "Mã đơn hàng", width: 120, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'CustomerCode', title: "Mã khách hàng", width: 120, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'CustomerName', title: "Tên khách hàng", width: 150, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'ServiceOfOrderName', title: "Loại dịch vụ", width: 150, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'TransportModeName', title: "Loại vận chuyển", width: 150, filterable: { cell: { operator: 'contains', showOperators: false } } },
            {
                field: 'ETD', width: '130px', title: 'ETD', template: "#=ETD==null?' ':Common.Date.FromJsonDMY(ETD)#",
                filterable: {
                    cell: {
                        template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); },operator: 'equal', showOperators: false
                    }
                },
            },
            {
                field: 'ETA', width: '130px', title: 'ETA', template: "#=ETA==null?' ':Common.Date.FromJsonDMY(ETA)#",
                filterable: {
                    cell: {
                        template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); }, operator: 'equal', showOperators: false
                    }
                },
            },
            { field: 'TotalTon', title: "Tấn", width: 100, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'TotalCBM', title: "Khối", width: 100, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { title: ' ', filterable: false, sortable: false }
        ]
    }

    $scope.groupProductList_GridChoose_Change = function ($event, grid, hasChoose) {
        $scope.groupProductListHasChoose = hasChoose;
    }

    $scope.GroupProductList_AddNew = function ($event, win) {
        $event.preventDefault();
        win.center().open();
        $scope.GroupProductNotIn_Grid.dataSource.read();
    }

    $scope.GroupProductNotIn_GridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.WFL,
            method: _WFLPacket_Index.URL.GroupProductNotInList,
            readparam: function () { return { packetID: $scope.packetID } },
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
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,GroupProductNotIn_Grid,GroupProductNotIn_GridChoose_Change)" />',
                headerAttributes: { style: 'text-align: center;' }, attributes: { style: 'text-align: center;' },
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,GroupProductNotIn_Grid,GroupProductNotIn_GridChoose_Change)" />',
                filterable: false, sortable: false
            },
            { field: 'OrderCode', title: "Mã đơn hàng", width: 120, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'CustomerCode', title: "Mã khách hàng", width: 150, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'CustomerName', title: "Tên khách hàng", width: 150, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'ServiceOfOrderName', title: "Loại dịch vụ", width: 150, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'TransportModeName', title: "Loại vận chuyển", width: 150, filterable: { cell: { operator: 'contains', showOperators: false } } },

            {
                field: 'ETD', width: '130px', title: 'ETD', template: "#=ETD==null?' ':Common.Date.FromJsonDMY(ETD)#",
                filterable: {
                    cell: {
                        template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); }, operator: 'equal', showOperators: false
                    }
                },
            },
            {
                field: 'ETA', width: '130px', title: 'ETA', template: "#=ETA==null?' ':Common.Date.FromJsonDMY(ETA)#",
                filterable: {
                    cell: {
                        template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); }, operator: 'equal', showOperators: false
                    }
                },
            },
            { field: 'TotalTon', title: "Tấn", width: 100, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'TotalCBM', title: "Khối", width: 100, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { title: ' ', filterable: false, sortable: false }
        ]
    }

    $scope.GroupProductNotIn_GridChoose_Change = function ($event, grid, hasChoose) {
        $scope.GroupProductNotInHasChoose = hasChoose;
    }

    $scope.GroupProductNotIn_Save_Click = function ($event, win, grid) {
        $event.preventDefault();
        var data = grid.dataSource.data();
        var datasend = [];
        Common.Data.Each(data, function (o) {
            if (o.IsChoose) datasend.push(o.ID);
        })
        if (datasend.length > 0) {
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.WFL,
                method: _WFLPacket_Index.URL.GroupProductSaveList,
                data: { packetID: $scope.packetID, lst: datasend },
                success: function (res) {
                    $rootScope.IsLoading = false;
                    $scope.groupProductList_Grid.dataSource.read();
                    win.close();
                    $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                },
                error: function (res) {
                    $rootScope.IsLoading = false;
                }
            });

        }
    }

    $scope.GroupProductList_Delete = function ($event, grid) {
        $event.preventDefault();
        var data = grid.dataSource.data();
        var datasend = [];
        Common.Data.Each(data, function (o) {
            if (o.IsChoose) datasend.push(o.ID);
        })
        if (datasend.length > 0) {
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.WFL,
                method: _WFLPacket_Index.URL.DeleteList,
                data: { packetID: $scope.packetID, lst: datasend },
                success: function (res) {
                    $rootScope.IsLoading = false;
                    $scope.groupProductList_Grid.dataSource.read();
                    $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                },
                error: function (res) {
                    $rootScope.IsLoading = false;
                }
            });

        }
    }

    //#endregion

    //#region DIMasterList
    $scope.DIMasterList_GridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.WFL,
            method: _WFLPacket_Index.URL.DITOMasterList,
            readparam: function () { return { packetID: $scope.packetID } },
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number', editable: false, nullable: true },
                    IsChoose: { type: 'boolean', },
                }
            }
        }),
        height: '100%', pageable: true, sortable: true, columnMenu: false, filterable: { mode: 'row' }, resizable: true, editable: false,
        columns: [
            {
                title: ' ', width: '35px',
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,DIMasterList_Grid,DIMasterList_GridChoose_Change)" />',
                headerAttributes: { style: 'text-align: center;' }, attributes: { style: 'text-align: center;' },
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,DIMasterList_Grid,DIMasterList_GridChoose_Change)" />',
                filterable: false, sortable: false
            },
            { field: 'MasterCode', title: "Mã", width: 120, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'VehicleCode', title: "Số xe", width: 150, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'DriverName', title: "Tài xế", width: 150, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'VendorName', title: "Đối tác", width: 150, filterable: { cell: { operator: 'contains', showOperators: false } } },
            {
                field: 'ETD', width: '130px', title: 'ETD', template: "#=ETD==null?' ':Common.Date.FromJsonDMY(ETD)#",
                filterable: {
                    cell: {
                        template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); }, operator: 'equal', showOperators: false
                    }
                },
            },
            {
                field: 'ETA', width: '130px', title: 'ETA', template: "#=ETA==null?' ':Common.Date.FromJsonDMY(ETA)#",
                filterable: {
                    cell: {
                        template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); }, operator: 'equal', showOperators: false
                    }
                },
            },
            { title: ' ', filterable: false, sortable: false }
        ]
    }

    $scope.DIMasterList_GridChoose_Change = function ($event, grid, hasChoose) {
        $scope.DIMasterListHasChoose = hasChoose;
    }

    $scope.DIMasterList_AddNew = function ($event, win) {
        $event.preventDefault();
        win.center().open();
        $scope.DITOMasterNotIn_Grid.dataSource.read();
    }

    $scope.DITOMasterNotIn_GridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.WFL,
            method: _WFLPacket_Index.URL.DITOMasterNotInList,
            readparam: function () { return { packetID: $scope.packetID } },
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
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,DITOMasterNotIn_Grid,DITOMasterNotIn_GridChoose_Change)" />',
                headerAttributes: { style: 'text-align: center;' }, attributes: { style: 'text-align: center;' },
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,DITOMasterNotIn_Grid,DITOMasterNotIn_GridChoose_Change)" />',
                filterable: false, sortable: false
            },
          { field: 'MasterCode', title: "Mã", width: 120, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'VehicleCode', title: "Số xe", width: 150, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'DriverName', title: "Tài xế", width: 150, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'VendorName', title: "Đối tác", width: 150, filterable: { cell: { operator: 'contains', showOperators: false } } },
            {
                field: 'ETD', width: '130px', title: 'ETD', template: "#=ETD==null?' ':Common.Date.FromJsonDMY(ETD)#",
                filterable: {
                    cell: {
                        template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); }, operator: 'equal', showOperators: false
                    }
                },
            },
            {
                field: 'ETA', width: '130px', title: 'ETA', template: "#=ETA==null?' ':Common.Date.FromJsonDMY(ETA)#",
                filterable: {
                    cell: {
                        template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); }, operator: 'equal', showOperators: false
                    }
                },
            },
            { title: ' ', filterable: false, sortable: false }
        ]
    }

    $scope.DITOMasterNotIn_GridChoose_Change = function ($event, grid, hasChoose) {
        $scope.DITOMasterNotInHasChoose = hasChoose;
    }

    $scope.DITOMasterNotIn_Save_Click = function ($event, win, grid) {
        $event.preventDefault();
        var data = grid.dataSource.data();
        var datasend = [];
        Common.Data.Each(data, function (o) {
            if (o.IsChoose) datasend.push(o.ID);
        })
        if (datasend.length > 0) {
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.WFL,
                method: _WFLPacket_Index.URL.DITOMasterSaveList,
                data: { packetID: $scope.packetID, lst: datasend },
                success: function (res) {
                    $rootScope.IsLoading = false;
                    $scope.DIMasterList_Grid.dataSource.read();
                    win.close();
                    $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                },
                error: function (res) {
                    $rootScope.IsLoading = false;
                }
            });

        }
    }

    $scope.DIMasterList_Delete = function ($event, grid) {
        $event.preventDefault();
        var data = grid.dataSource.data();
        var datasend = [];
        Common.Data.Each(data, function (o) {
            if (o.IsChoose) datasend.push(o.ID);
        })
        if (datasend.length > 0) {
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.WFL,
                method: _WFLPacket_Index.URL.DeleteList,
                data: { packetID: $scope.packetID, lst: datasend },
                success: function (res) {
                    $rootScope.IsLoading = false;
                    $scope.DIMasterList_Grid.dataSource.read();
                    $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                },
                error: function (res) {
                    $rootScope.IsLoading = false;
                }
            });

        }
    }
    //#endregion

    //#region COMasterList
    $scope.COMasterList_GridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.WFL,
            method: _WFLPacket_Index.URL.COTOMasterList,
            readparam: function () { return { packetID: $scope.packetID } },
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number', editable: false, nullable: true },
                    IsChoose: { type: 'boolean', },
                }
            }
        }),
        height: '100%', pageable: true, sortable: true, columnMenu: false, filterable: { mode: 'row' }, resizable: true, editable: false,
        columns: [
            {
                title: ' ', width: '35px',
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,COMasterList_Grid,COMasterList_GridChoose_Change)" />',
                headerAttributes: { style: 'text-align: center;' }, attributes: { style: 'text-align: center;' },
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,COMasterList_Grid,COMasterList_GridChoose_Change)" />',
                filterable: false, sortable: false
            },
            { field: 'MasterCode', title: "Mã", width: 120, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'VehicleCode', title: "Số xe", width: 150, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'DriverName', title: "Tài xế", width: 150, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'VendorName', title: "Đối tác", width: 150, filterable: { cell: { operator: 'contains', showOperators: false } } },
            {
                field: 'ETD', width: '130px', title: 'ETD', template: "#=ETD==null?' ':Common.Date.FromJsonDMY(ETD)#",
                filterable: {
                    cell: {
                        template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); }, operator: 'equal', showOperators: false
                    }
                },
            },
            {
                field: 'ETA', width: '130px', title: 'ETA', template: "#=ETA==null?' ':Common.Date.FromJsonDMY(ETA)#",
                filterable: {
                    cell: {
                        template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); }, operator: 'equal', showOperators: false
                    }
                },
            },
            { title: ' ', filterable: false, sortable: false }
        ]
    }

    $scope.COMasterList_GridChoose_Change = function ($event, grid, hasChoose) {
        $scope.COMasterListHasChoose = hasChoose;
    }

    $scope.COMasterList_AddNew = function ($event, win) {
        $event.preventDefault();
        win.center().open();
        $scope.COTOMasterNotIn_Grid.dataSource.read();
    }

    $scope.COTOMasterNotIn_GridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.WFL,
            method: _WFLPacket_Index.URL.COTOMasterNotInList,
            readparam: function () { return { packetID: $scope.packetID } },
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
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,COTOMasterNotIn_Grid,COTOMasterNotIn_GridChoose_Change)" />',
                headerAttributes: { style: 'text-align: center;' }, attributes: { style: 'text-align: center;' },
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,COTOMasterNotIn_Grid,COTOMasterNotIn_GridChoose_Change)" />',
                filterable: false, sortable: false
            },
          { field: 'MasterCode', title: "Mã", width: 120, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'VehicleCode', title: "Số xe", width: 150, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'DriverName', title: "Tài xế", width: 150, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'VendorName', title: "Đối tác", width: 150, filterable: { cell: { operator: 'contains', showOperators: false } } },
            {
                field: 'ETD', width: '130px', title: 'ETD', template: "#=ETD==null?' ':Common.Date.FromJsonDMY(ETD)#",
                filterable: {
                    cell: {
                        template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); }, operator: 'equal', showOperators: false
                    }
                },
            },
            {
                field: 'ETA', width: '130px', title: 'ETA', template: "#=ETA==null?' ':Common.Date.FromJsonDMY(ETA)#",
                filterable: {
                    cell: {
                        template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); }, operator: 'equal', showOperators: false
                    }
                },
            },
            { title: ' ', filterable: false, sortable: false }
        ]
    }

    $scope.COTOMasterNotIn_GridChoose_Change = function ($event, grid, hasChoose) {
        $scope.COTOMasterNotInHasChoose = hasChoose;
    }

    $scope.COTOMasterNotIn_Save_Click = function ($event, win, grid) {
        $event.preventDefault();
        var data = grid.dataSource.data();
        var datasend = [];
        Common.Data.Each(data, function (o) {
            if (o.IsChoose) datasend.push(o.ID);
        })
        if (datasend.length > 0) {
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.WFL,
                method: _WFLPacket_Index.URL.COTOMasterSaveList,
                data: { packetID: $scope.packetID, lst: datasend },
                success: function (res) {
                    $rootScope.IsLoading = false;
                    $scope.COMasterList_Grid.dataSource.read();
                    win.close();
                    $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                },
                error: function (res) {
                    $rootScope.IsLoading = false;
                }
            });

        }
    }

    $scope.COMasterList_Delete = function ($event, grid) {
        $event.preventDefault();
        var data = grid.dataSource.data();
        var datasend = [];
        Common.Data.Each(data, function (o) {
            if (o.IsChoose) datasend.push(o.ID);
        })
        if (datasend.length > 0) {
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.WFL,
                method: _WFLPacket_Index.URL.DeleteList,
                data: { packetID: $scope.packetID, lst: datasend },
                success: function (res) {
                    $rootScope.IsLoading = false;
                    $scope.COMasterList_Grid.dataSource.read();
                    $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                },
                error: function (res) {
                    $rootScope.IsLoading = false;
                }
            });

        }
    }
    //#endregion

    $scope.Win_Close = function ($event, win) {
        $event.preventDefault();
        win.close();
    }

    //#region action
    $scope.ShowSetting = function ($event, grid) {
        $event.preventDefault();

        $rootScope.ShowSetting({
            ListView: views.WFLPacket,
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
}]);
