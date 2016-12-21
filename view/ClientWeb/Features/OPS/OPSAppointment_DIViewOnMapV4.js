/// <reference path="~/Scripts/jquery-1.9.1.intellisense.js" />
/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />

var _OPSAppointment_DIViewOnMapV4 = {
    URL: {
        Order_List: 'OPSDI_MAP_Order_List',
        Vehicle_List: 'OPSDI_MAP_Vehicle_List',
        VehicleVendor_List: 'OPSDI_MAP_VehicleVendor_List',
        TOMaster_List: 'OPSDI_MAP_TOMaster_List',
        TOMaster_GroupProduct_List: 'OPSDI_MAP_TOMaster_GroupProduct_List',
        CheckVehicleAvailable: 'OPSDI_MAP_CheckVehicleAvailable',

        Setting: 'OPSCO_MAP_Setting',
        Vendor_List: 'OPSCO_MAP_Vendor_List',
        Driver_List: 'OPSCO_MAP_Driver_List',        
        Customer_List: 'OPSCO_MAP_Customer_List',
        DriverVendor_List: 'OPSCO_MAP_DriverVendor_List',
        Location_List: 'OPSCO_MAP_Location_List',

        TripByID: 'OPSDI_MAP_TripByID',
        TripByVehicle_List: 'OPSDI_MAP_TripByVehicle_List',
        Schedule_Data: 'OPSDI_MAP_Schedule_Data',
        Save: 'OPSDI_MAP_Save',
        Update: 'OPSDI_MAP_Update',
        ToMon: 'OPSDI_MAP_ToMON',
        Cancel: 'OPSDI_MAP_Cancel',
        UpdateAndToMon: 'OPSDI_MAP_UpdateAndToMON',
        ToOPS: 'OPSDI_MAP_ToOPS',
        Delete: 'OPSDI_MAP_Delete',
        ToVendor: 'OPSDI_MAP_ToVendor',
        Split: 'OPSDI_MAP_GroupProduct_Split',
        Split_Cancel: 'OPSDI_MAP_GroupProduct_Split_Cancel',

        GroupByTrip_List: 'OPSDI_MAP_GroupByTrip_List',
        FTLSplit: 'OPSDI_MAP_FTL_Split',
        FTLSplit_Check: 'OPSDI_MAP_FTL_Split_Check',
        FTLMerge: 'OPSDI_MAP_FTL_Merge',

        Vehicle_New: 'OPSDI_MAP_Vehicle_New',
        
        DITOGroupProduct_List: 'OPSDI_MAP_DITOGroupProduct_List',
        DI2View_GroupProduct_List: 'OPSDI_MAP_2View_GroupProduct_List',
        DI2View_Master_Update_Check4Delete: 'OPSDI_MAP_2View_Master_Update_Check4Delete',
        DI2View_Master_Update_Check4Consolidate: 'OPSDI_MAP_2View_Master_Update_Check4Consolidate',
        DI2View_Master_Update_Group_Quantity: 'OPSDI_MAP_2View_Master_Update_Group_Quantity',
        DI2View_Master_Update_Check4Update: 'OPSDI_MAP_2View_Master_Update_Check4Update',
        DI2View_Master_Update_TimeLine: 'OPSDI_MAP_2View_Master_Update_TimeLine',
        DI2View_Master_Update_Group: 'OPSDI_MAP_2View_Master_Update_Group',
        DI2View_Master_Update: 'OPSDI_MAP_2View_Master_Update',
        
        New_Schedule_Data: 'OPSDI_MAP_New_Schedule_Data',
        New_Schedule_TOMaster_List: 'OPSDI_MAP_New_Schedule_DITOGroupProduct_List',
        Customer_List: 'OPSCO_MAP_Customer_List',
        TimeLine_VehicleInfo: 'OPSDI_MAP_TimeLine_Vehicle_Info',
        TimeLine_Master_Update_Group: 'OPSDI_MAP_TimeLine_Master_Update_Group'
    },
    Data: {
        Location: {
            LocationStartID: -1,
            LocationStartName: '',
            LocationEndID: -1,
            LocationEndName: '',
            LocationStartLat: null,
            LocationStartLng: null,
            LocationEndLat: null,
            LocationEndLng: null,
        },
        VendorList: [],
        VehicleInfo: [],
    }
}

angular.module('myapp').controller('OPSAppointment_DIViewOnMapV4Ctrl', ['$rootScope', '$scope', '$http', '$location', '$state', '$timeout', '$compile', 'openMapV2', function ($rootScope, $scope, $http, $location, $state, $timeout, $compile, openMapV2) {
    if ($rootScope.IsPageComplete != true) return;
    Common.Log('OPSAppointment_DIViewOnMapV4Ctrl');
    $rootScope.IsLoading = false;

    $scope.Color = {
        None: '#f6fafe',
        Error: '#fc0000',
        Success: '#31B6FC'
    }
    $scope.DateRequest = {
        fDate: null,
        tDate: null
    }
    
    //Lấy thông tin điểm bắt đầu, điểm kết thúc
    Common.Services.Call($http, {
        url: Common.Services.url.OPS,
        method: _OPSAppointment_DIViewOnMapV4.URL.Setting,
        success: function (res) {
            if (Common.HasValue(res)) {
                _OPSAppointment_DIViewOnMapV4.Data.Location.LocationStartID = res.LocationStartID;
                _OPSAppointment_DIViewOnMapV4.Data.Location.LocationStartName = res.LocationStartName;
                _OPSAppointment_DIViewOnMapV4.Data.Location.LocationEndID = res.LocationEndID;
                _OPSAppointment_DIViewOnMapV4.Data.Location.LocationEndName = res.LocationEndName;
                _OPSAppointment_DIViewOnMapV4.Data.Location.LocationEndLat = res.LocationEndLat;
                _OPSAppointment_DIViewOnMapV4.Data.Location.LocationEndLng = res.LocationEndLng;
                _OPSAppointment_DIViewOnMapV4.Data.Location.LocationStartLat = res.LocationStartLat;
                _OPSAppointment_DIViewOnMapV4.Data.Location.LocationStartLng = res.LocationStartLng;
            }
        }
    });

    $scope.gopTon_Options = {
        min: 0, spinners: false, culture: "en-US", format: "n5",
        change: function (e) {
            var txt = this;
            var val = this.value();
            var tr = $(e.sender.element).closest('tr');
            var grid = $(e.sender.element).closest('.cus-grid.k-grid').data('kendoGrid');
            var dataItem = grid.dataItem(tr);
            if (val > 0.001 && dataItem.Ton - val > 0.001) {
                $rootScope.Message({
                    Msg: "Xác nhận chia đơn hàng?",
                    Type: Common.Message.Type.Confirm,
                    Ok: function () {
                        $rootScope.IsLoading = true;
                        Common.Services.Call($http, {
                            url: Common.Services.url.OPS,
                            method: _OPSAppointment_DIViewOnMapV4.URL.Split,
                            data: {
                                gopID: dataItem.ID, total: -1, value: val, packingType: 1
                            },
                            success: function (res) {
                                $rootScope.IsLoading = false;
                                $rootScope.Message({
                                    Msg: 'Thành công!'
                                })
                                grid.dataSource.read();
                                $scope.ChangeData = true;
                            }
                        });
                    },
                    Close: function () {
                        txt.value(dataItem.Ton);
                    }
                })
            }
        }
    }
    
    $scope.gopCBM_Options = {
        min: 0, spinners: false, culture: "en-US", format: "n5",
        change: function (e) {
            var txt = this;
            var val = this.value();
            var tr = $(e.sender.element).closest('tr');
            var grid = $(e.sender.element).closest('.cus-grid.k-grid').data('kendoGrid');
            var dataItem = grid.dataItem(tr);
            if (val > 0.001 && dataItem.CBM - val > 0.001) {
                $rootScope.Message({
                    Msg: "Xác nhận chia đơn hàng?",
                    Type: Common.Message.Type.Confirm,
                    Ok: function () {
                        $rootScope.IsLoading = true;
                        Common.Services.Call($http, {
                            url: Common.Services.url.OPS,
                            method: _OPSAppointment_DIViewOnMapV4.URL.Split,
                            data: {
                                gopID: dataItem.ID, total: -1, value: val, packingType: 2
                            },
                            success: function (res) {
                                $rootScope.IsLoading = false;
                                $rootScope.Message({
                                    Msg: 'Thành công!'
                                })
                                grid.dataSource.read();
                                $scope.ChangeData = true;
                            }
                        });
                    },
                    Close: function () {
                        txt.value(dataItem.CBM);
                    }
                })
            }
        }
    }

    $scope.gopQuantity_Options = {
        min: 0, spinners: false, culture: "en-US", format: "n5",
        change: function (e) {
            var val = this.value();
            var tr = $(e.sender.element).closest('tr');
            var grid = $(e.sender.element).closest('.cus-grid.k-grid').data('kendoGrid');
            var dataItem = grid.dataItem(tr);
            if (val > 0.001 && dataItem.Quantity - val > 0.001) {
                $rootScope.Message({
                    Msg: "Xác nhận chia đơn hàng?",
                    Type: Common.Message.Type.Confirm,
                    Ok: function () {
                        $rootScope.IsLoading = true;
                        Common.Services.Call($http, {
                            url: Common.Services.url.OPS,
                            method: _OPSAppointment_DIViewOnMapV4.URL.Split,
                            data: {
                                gopID: dataItem.ID, total: -1, value: val, packingType: 3
                            },
                            success: function (res) {
                                $rootScope.IsLoading = false;
                                $rootScope.Message({
                                    Msg: 'Thành công!'
                                })
                                grid.dataSource.read();
                                $scope.ChangeData = true;
                            }
                        });
                    },
                    Close: function () {
                        txt.value(dataItem.CBM);
                    }
                })
            }
        }
    }

    $scope.IsFTLMerge = false;

    $scope.GroupProduct_Cancel = function ($event, item, grid) {
        $event.preventDefault();

        $rootScope.Message({
            Msg: "Xác nhận hủy đơn hàng?",
            Type: Common.Message.Type.Confirm,
            Ok: function () {
                $rootScope.IsLoading = true;
                Common.Services.Call($http, {
                    url: Common.Services.url.OPS,
                    method: _OPSAppointment_DIViewOnMapV4.URL.Cancel,
                    data: {
                        data: [item.ID]
                    },
                    success: function (res) {
                        Common.Services.Error(res, function (res) {
                            $rootScope.IsLoading = false;
                            $rootScope.Message({
                                Msg: 'Thành công!'
                            })
                        }, function () {
                            $rootScope.IsLoading = false;
                        })
                    }
                })
            }
        })
    }

    $scope.GroupProduct_Merge = function ($event, item, grid) {
        $event.preventDefault();

        $($event.target).closest('td').find('.btn-merge').hide();
        $($event.target).closest('td').find('.btn-merge-ok').show();
        Common.Data.Each(grid.items(), function (tr) {
            var o = grid.dataItem(tr);
            $(tr).find('.btn-split').hide();
            if (Common.HasValue(o) && o.OrderGroupProductID == item.OrderGroupProductID && o.ID != item.ID) {
                $(tr).find('.btn-merge').hide();
                var chk = $(tr).find('.chk-select-to-merge');
                chk.prop('checked', '');
                chk.show();
            }
        })
    }

    $scope.GroupProduct_Merge_OK = function ($event, item, grid) {
        $event.preventDefault();

        var flag = false;
        var data = [item.ID];
        Common.Data.Each(grid.items(), function (tr) {
            var o = grid.dataItem(tr);
            if (Common.HasValue(o) && o.OrderGroupProductID == item.OrderGroupProductID && o.ID != item.ID) {
                var chk = $(tr).find('.chk-select-to-merge');
                if (chk.prop('checked')) {
                    data.push(o.ID);
                    flag = true;
                }
            }
        })

        if (flag) {
            $rootScope.Message({
                Msg: "Xác nhận gom đơn hàng đã chọn?",
                Type: Common.Message.Type.Confirm,
                Ok: function () {
                    $rootScope.IsLoading = true;
                    Common.Services.Call($http, {
                        url: Common.Services.url.OPS,
                        method: _OPSAppointment_DIViewOnMapV4.URL.Split_Cancel,
                        data: {
                            orderGopID: item.OrderGroupProductID, data: data
                        },
                        success: function (res) {
                            $rootScope.IsLoading = false;
                            $rootScope.Message({
                                Msg: 'Thành công!'
                            })
                            grid.dataSource.read();
                        }
                    });
                }
            })
        } else {
            $($event.target).closest('td').find('.btn-merge').show();
            $($event.target).closest('td').find('.btn-merge-ok').hide();
            Common.Data.Each(grid.items(), function (tr) {
                var o = grid.dataItem(tr);
                if (Common.HasValue(o) && o.OrderGroupProductID == item.OrderGroupProductID && o.ID != item.ID) {
                    var chk = $(tr).find('.chk-select-to-merge');
                    chk.hide();
                    $(tr).find('.btn-merge').show();
                }
            })
        }
    }
        
    $scope.IsVehicleVendor = false;
    $scope.VehicleVendorID = -1;
    $scope.NewVehicleVendor = {
        RegNo: '', MaxWeight: 0
    }
    
    $scope.TripItem = { ID: -1 };
    $scope.ResetTrip = function () {
        $scope.TripItem = { ID: -1 };
        $scope.NewTimeLineGroupSelected = [];
        $scope.timeline_gopV2_Grid.dataSource.read();
    }

    //#endregion

    //#region FTL Split & Merge
    $scope.DataFTLMerge = [];
    $scope.FTLMerge_Click = function ($event, grid) {
        $event.preventDefault();

        if (!$scope.IsFTLMerge) {
            $scope.IsFTLMerge = true;
            $scope.DataFTLMerge = [];
            angular.forEach(grid.element.find('.chkFTLChoose'), function (o) {
                var chk = $(o).closest('td').find('input.chkFTLChooseMerge');
                chk.show(); $(o).hide();
                if ($(o).prop("checked")) {
                    chk.prop("checked", true);
                    var uid = $(chk).data('uid');
                    $scope.DataFTLMerge.push(uid);
                }
            })
        } else {
            if ($scope.DataFTLMerge.length < 2) {
                $rootScope.Message({
                    Msg: "Vui lòng chọn ít nhất 2 đơn hàng!", Type: Common.Message.Type.Alert
                })
            } else {
                $rootScope.Message({
                    Msg: "Xác nhận gộp các đơn hàng đã chọn?",
                    Type: Common.Message.Type.Confirm,
                    Ok: function () {
                        $rootScope.IsLoading = true;
                        Common.Services.Call($http, {
                            url: Common.Services.url.OPS,
                            method: _OPSAppointment_DIViewOnMapV4.URL.FTLMerge,
                            data: { data: $scope.DataFTLMerge },
                            success: function (res) {
                                Common.Services.Error(res, function (res) {
                                    Common.Services.Error(res, function (res) {
                                        $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                                        $scope.ResetTrip(true);
                                        $rootScope.IsLoading = false;
                                        $scope.IsFTLMerge = false;
                                        $scope.DataFTLMerge = [];
                                    });
                                });
                            }
                        });
                    }
                })
            }
        }
    }

    $scope.gridChooseMergeFTL_Change = function ($event, grid) {
        var chk = $(event.target), val = $(chk).prop('checked'), uid = $(chk).data('uid');
        if (val == true) {
            if ($scope.DataFTLMerge.indexOf(uid) == -1) $scope.DataFTLMerge.push(uid);
        } else {
            if ($scope.DataFTLMerge.indexOf(uid) > -1) $scope.DataFTLMerge.splice($scope.DataFTLMerge.indexOf(uid), 1);
        }
    }

    $scope.FTLMerge_Cancel_Click = function ($event, grid) {
        $event.preventDefault();

        if ($scope.DataFTLMerge.length > 1) {
            $rootScope.Message({
                Msg: "Xác nhận hủy gộp?",
                Type: Common.Message.Type.Confirm,
                Ok: function () {
                    $scope.IsFTLMerge = false;
                    $scope.DataFTLMerge = [];
                    angular.forEach(grid.element.find('.chkFTLChooseMerge'), function (o) {
                        var chk = $(o).closest('td').find('input.chkFTLChoose');
                        chk.show(); $(o).hide(); $(o).prop("checked", false);
                    })
                }
            })
        } else {
            $scope.IsFTLMerge = false;
            $scope.DataFTLMerge = [];
            angular.forEach(grid.element.find('.chkFTLChooseMerge'), function (o) {
                var chk = $(o).closest('td').find('input.chkFTLChoose');
                chk.show(); $(o).hide(); $(o).prop("checked", false);
            })
        }
    }
    
    $scope.FTLSplit_Click = function ($event, grid, win) {
        $event.preventDefault();

        $rootScope.IsLoading = true;
        Common.Services.Call($http, {
            url: Common.Services.url.OPS,
            method: _OPSAppointment_DIViewOnMapV4.URL.FTLSplit_Check,
            data: {
                toMasterID: $scope.TripItem.ID
            },
            success: function (res) {
                $rootScope.IsLoading = false;
                Common.Services.Error(res, function (res) {
                    if (res == true) {
                        $rootScope.Message({
                            Msg: 'Xác nhận tách đơn hàng?',
                            Type: Common.Message.Type.Confirm,
                            Ok: function () {
                                $rootScope.IsLoading = true;
                                Common.Services.Call($http, {
                                    url: Common.Services.url.OPS,
                                    method: _OPSAppointment_DIViewOnMapV4.URL.FTLSplit,
                                    data: { toMasterID: $scope.TripItem.ID, dataGop: [] },
                                    success: function (res) {
                                        Common.Services.Error(res, function (res) {
                                            Common.Services.Error(res, function (res) {
                                                $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                                                $scope.ResetTrip(true);
                                                $rootScope.IsLoading = false;
                                            });
                                        });
                                    }
                                });
                            }
                        });
                    } else {
                        $rootScope.IsLoading = true;
                        Common.Services.Call($http, {
                            url: Common.Services.url.OPS,
                            method: _OPSAppointment_DIViewOnMapV4.URL.GroupByTrip_List,
                            data: {
                                tripID: $scope.TripItem.ID
                            },
                            success: function (res) {
                                Common.Services.Error(res, function (res) {
                                    $rootScope.IsLoading = false;
                                    $scope.splitGop_GridOptions.dataSource.data(res);
                                    win.center().open();
                                    $timeout(function () {
                                        $scope.splitGop_GridOptions.dataSource.sync();
                                    }, 101)
                                })
                            }
                        })
                    }
                })
            }
        })
    }

    $scope.splitGop_GridOptions = {
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'GenID',
                fields:
                    {
                        GenID: { type: 'string', editable: false },
                        ID: { type: 'number' },
                        TOMasterID: { type: 'number' },
                        ETD: { type: 'date' },
                        ETA: { type: 'date' },
                        Ton: { type: 'number' },
                        CBM: { type: 'number' },
                        Quantity: { type: 'number' },
                        TempMin: { type: 'number' },
                        TempMax: { type: 'number' }
                    }
            },
            group: [{ field: 'TOMasterID', dir: 'asc' }]
        }),
        height: '99%', groupable: false, pageable: false, sortable: true, resizable: true, reorderable: true, sortable: { mode: 'multiple' }, auboBind: false,
        columns: [
            {
                field: 'TOMasterID', width: 50, title: ' ', filterable: false, sortable: false, groupable: false, groupHeaderTemplate: "Chuyến: #=value#",
                template: '<form class="cus-form-enter" ng-submit="TOMasterEnter_Click($event)"><input kendo-numeric-text-box class="txtTOMaster" value="#=TOMasterID>0?TOMasterID:0#" data-k-min="1" k-options="gopSplitTOMaster_Options" style="width:100%" /></form>',
            },
            {
                field: 'Command', title: ' ', width: '35px',
                attributes: { style: 'text-align: center;' },
                template: '<a class="k-button small-button btn-merge" ng-show="dataItem.IsSplit" title="Gộp" href="/" ng-click="FTLGroupProduct_Merge($event,dataItem,splitGop_Grid)">M</a>' +
                    '<a class="k-button small-button btn-merge-ok" style="display:none;" title="Xác nhận" href="/" ng-click="FTLGroupProduct_Merge_OK($event,dataItem,splitGop_Grid)">S</a>' +
                    '<input type="checkbox" style="display:none;" class="chk-select-to-merge" />',
                filterable: false, sortable: false, groupable: false
            },
            { field: 'CustomerCode', width: 100, title: 'Mã KH', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'CustomerShortName', width: 150, title: 'Tên KH', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'OrderCode', width: 100, title: 'Đơn hàng', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'GroupProductCode', width: 100, title: 'Nhóm hàng', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'ProductCode', width: 80, title: 'Hàng hóa', filterable: { cell: { operator: 'contains', showOperators: false } } },
            {
                field: 'Ton', width: 80, title: 'Tấn',
                template: '#if(PackingType==1&&IsFTL&&Ton>0){#<form class="submit-form"><input kendo-numeric-text-box value="#:Ton#" k-options="gopSplitTon_Options" data-k-max="#:Ton#" style="width:100%"/></form>#}else{##:Ton##}#',
                sortable: true, filterable: { cell: { operator: 'gte', showOperators: false } }
            },
            {
                field: 'CBM', width: 80, title: 'Khối',
                template: '#if(PackingType==2&&IsFTL&&CBM>0){#<form class="submit-form"><input kendo-numeric-text-box value="#:CBM#" k-options="gopSplitCBM_Options" data-k-max="#:CBM#" style="width:100%"/></form>#}else{##:CBM##}#',
                sortable: true, filterable: { cell: { operator: 'gte', showOperators: false } }
            },
            {
                field: 'Quantity', width: 80, title: 'S.Lượng',
                template: '#if(PackingType==3&&IsFTL&&Quantity>0){#<form class="submit-form"><input kendo-numeric-text-box value="#:Quantity#" k-options="gopSplitQuantity_Options" data-k-max="#:Quantity#" style="width:100%"/></form>#}else{##:Quantity##}#',
                sortable: true, filterable: { cell: { operator: 'gte', showOperators: false } }
            },
            { field: 'DNCode', width: 120, title: 'Số DN', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'SOCode', width: 120, title: 'Số SO', filterable: { cell: { operator: 'contains', showOperators: false } } },
            {
                field: 'ETD', width: 120, title: 'ETD', template: "#=ETD==null?' ':kendo.toString(ETD, '" + Common.Date.Format.DMYHM + "')#",
                filterable: { cell: { template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); }, operator: 'equal', showOperators: false } },
            },
            {
                field: 'ETA', width: 120, title: 'ETA', template: "#=ETA==null?' ':kendo.toString(ETA, '" + Common.Date.Format.DMYHM + "')#",
                filterable: { cell: { template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); }, operator: 'equal', showOperators: false } },
            },
            { field: 'TempMin', width: 100, title: 'N.Độ tối thiểu', template: '#=TempMin!=null?TempMin:""#', filterable: { cell: { operator: 'gte', showOperators: false } } },
            { field: 'TempMax', width: 100, title: 'N.Độ tối đa', template: '#=TempMax!=null?TempMax:""#', filterable: { cell: { operator: 'gte', showOperators: false } } },
            { field: 'LocationFromCode', width: 150, title: 'Điểm nhận', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'LocationToCode', width: 150, title: 'Điểm giao', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'LocationFromAddress', width: 250, title: 'Địa chỉ nhận', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'LocationToAddress', width: 250, title: 'Địa chỉ giao', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { title: ' ', filterable: false, menu: false, sortable: false }
        ],
        filterable: { mode: 'row' },
        dataBound: function () {
            Common.Log("GridDataBound");
        }
    }

    $scope.ToFix3 = function (val) {
        return val > 0 ? Math.round(val * 1000) / 1000 : 0;
    }

    $scope.ToFix6 = function (val) {
        return val > 0 ? Math.round(val * 1000000) / 1000000 : 0;
    }

    $scope.gopSplitTon_Options = {
        min: 0, spinners: false, decimals: Common.Number.DI_Decimals, culture: "en-US", format: "n5",
        change: function (e) {
            var tr = $(e.sender.element).closest('tr');
            var obj = $scope.splitGop_Grid.dataItem(tr);
            if (Common.HasValue(obj)) {
                obj.IsSplit = true;
                var val = this.value();
                var objNew = angular.copy(obj);
                objNew.Ton = obj.Ton - val;
                obj.Ton = val;

                objNew.CBM = $scope.ToFix3(objNew.ExchangeCBM * objNew.Ton / objNew.ExchangeTon);
                objNew.Quantity = $scope.ToFix3(objNew.ExchangeQuantity * objNew.Ton / objNew.ExchangeTon);
                obj.CBM = $scope.ToFix3(obj.ExchangeCBM * obj.Ton / obj.ExchangeTon);
                obj.Quantity = $scope.ToFix3(obj.ExchangeQuantity * obj.Ton / obj.ExchangeTon);

                var index = $scope.splitGop_Grid.dataSource.data().indexOf(obj);
                $scope.splitGop_Grid.dataSource.data().splice(index, 0, objNew.toJSON());
            }            
        }
    };

    $scope.gopSplitCBM_Options = {
        min: 0, spinners: false, decimals: Common.Number.DI_Decimals, culture: "en-US", format: "n5",
        change: function (e) {
            var tr = $(e.sender.element).closest('tr');
            var obj = $scope.splitGop_Grid.dataItem(tr);
            if (Common.HasValue(obj)) {
                obj.IsSplit = true;
                var val = this.value();
                var objNew = angular.copy(obj);
                objNew.CBM = obj.CBM - val;
                obj.CBM = val;

                objNew.Ton = $scope.ToFix3(objNew.ExchangeTon * objNew.CBM / objNew.ExchangeCBM);
                objNew.Quantity = $scope.ToFix3(objNew.ExchangeQuantity * objNew.CBM / objNew.ExchangeCBM);
                obj.Ton = $scope.ToFix3(obj.ExchangeTon * obj.CBM / obj.ExchangeCBM);
                obj.Quantity = $scope.ToFix3(obj.ExchangeQuantity * obj.CBM / obj.ExchangeCBM);

                var index = $scope.splitGop_Grid.dataSource.data().indexOf(obj);
                $scope.splitGop_Grid.dataSource.data().splice(index, 0, objNew.toJSON());
            }
        }
    };

    $scope.gopSplitQuantity_Options = {
        min: 0, spinners: false, decimals: Common.Number.DI_Decimals, culture: "en-US", format: "n5",
        change: function (e) {
            var tr = $(e.sender.element).closest('tr');
            var obj = $scope.splitGop_Grid.dataItem(tr);
            if (Common.HasValue(obj)) {
                obj.IsSplit = true;
                var val = this.value();
                var objNew = angular.copy(obj);
                objNew.Quantity = obj.Quantity - val;
                obj.Quantity = val;

                objNew.CBM = $scope.ToFix3(objNew.ExchangeCBM * objNew.Quantity / objNew.ExchangeQuantity);
                objNew.Ton = $scope.ToFix3(objNew.ExchangeTon * objNew.Quantity / objNew.ExchangeQuantity);
                obj.CBM = $scope.ToFix3(obj.ExchangeCBM * obj.Quantity / obj.ExchangeQuantity);
                obj.Ton = $scope.ToFix3(obj.ExchangeTon * obj.Quantity / obj.ExchangeQuantity);

                var index = $scope.splitGop_Grid.dataSource.data().indexOf(obj);
                $scope.splitGop_Grid.dataSource.data().splice(index, 0, objNew.toJSON());
            }
        }
    };

    $scope.gopSplitTOMaster_Options = {
        format: '#', spinners: false, culture: 'en-US', step: 1, decimals: 0, min: 1,
        change: function (e) {
            var tr = $(e.sender.element).closest('tr');
            var obj = $scope.splitGop_Grid.dataItem(tr);
            if (Common.HasValue(obj)) {
                obj.TOMasterID = this.value();
                $scope.splitGop_Grid.dataSource.sync();
            }
        }
    };

    $scope.FTLGroupProduct_Merge = function ($event, item, grid) {
        $event.preventDefault();

        $($event.target).closest('td').find('.btn-merge').hide();
        $($event.target).closest('td').find('.btn-merge-ok').show();
        Common.Data.Each(grid.items(), function (tr) {
            var o = grid.dataItem(tr);
            if (Common.HasValue(o) && o.OrderGroupProductID == item.OrderGroupProductID && o.uid != item.uid) {
                $(tr).find('.btn-merge').hide();
                var chk = $(tr).find('.chk-select-to-merge');
                chk.prop('checked', '');
                chk.show();
            }
        })
    }

    $scope.FTLGroupProduct_Merge_OK = function ($event, item, grid) {
        $event.preventDefault();

        var flag = false;
        var data = [item.ID];
        Common.Data.Each(grid.items(), function (tr) {
            var o = grid.dataItem(tr);
            if (Common.HasValue(o) && o.OrderGroupProductID == item.OrderGroupProductID && o.uid != item.uid) {
                var chk = $(tr).find('.chk-select-to-merge');
                if (chk.prop('checked')) {
                    data.push(o.uid);
                    flag = true;
                }
            }
        })

        if (flag) {
            var isAll = true; //Check All
            $($event.target).closest('td').find('.btn-merge').show();
            $($event.target).closest('td').find('.btn-merge-ok').hide();
            var dataSource = grid.dataSource;
            Common.Data.Each(grid.items(), function (tr) {
                var o = grid.dataItem(tr);
                if (Common.HasValue(o) && o.OrderGroupProductID == item.OrderGroupProductID && o.uid != item.uid) {
                    if (data.indexOf(o.uid) > -1) {
                        item.Ton += o.Ton;
                        item.CBM += o.CBM;
                        item.Quantity += o.Quantity;
                        dataSource.remove(o);
                    } else {
                        isAll = false;
                    }
                }
            })
            if (isAll) {
                item.IsSplit = false;
            }
            dataSource.sync();
        } else {
            $($event.target).closest('td').find('.btn-merge').show();
            $($event.target).closest('td').find('.btn-merge-ok').hide();
            Common.Data.Each(grid.items(), function (tr) {
                var o = grid.dataItem(tr);
                if (Common.HasValue(o) && o.OrderGroupProductID == item.OrderGroupProductID && o.uid != item.uid) {
                    var chk = $(tr).find('.chk-select-to-merge');
                    chk.hide();
                    $(tr).find('.btn-merge').show();
                }
            })
        }
    }

    $scope.FTLSplit_OK_Click = function ($event, grid, win) {
        $event.preventDefault();

        var temp = [];
        Common.Data.Each(grid.dataSource.data(), function (o) {
            if (o.TOMasterID > 0 && temp.indexOf(o.TOMasterID) == -1) {
                temp.push(o.TOMasterID);
            }
        })
        if (temp.length > 1) {
            $rootScope.Message({
                Msg: 'Xác nhận lưu?',
                Type: Common.Message.Type.Confirm,
                Ok: function () {
                    $rootScope.IsLoading = true;
                    Common.Services.Call($http, {
                        url: Common.Services.url.OPS,
                        method: _OPSAppointment_DIViewOnMapV4.URL.FTLSplit,
                        data: { toMasterID: $scope.TripItem.ID, dataGop: grid.dataSource.data() },
                        success: function (res) {
                            Common.Services.Error(res, function (res) {
                                Common.Services.Error(res, function (res) {
                                    $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });

                                    $scope.ResetTrip(true);
                                    if ($scope.IsShowNewTimeLineV2)
                                        $scope.timeline_gopV2_Grid.dataSource.read();
                                    win.close();
                                    $rootScope.IsLoading = false;
                                });
                            });
                        }
                    });
                }
            });
        } else {
            win.close();
        }
    }
    
    //#endregion

    $scope.ConvertLatLng = function (value) {
        var str = parseInt(value, 10);
        var deg = value - str;
        deg = Math.round(deg * 60);

        return str + "*" + deg;
    }
   
    //#region NewTimeLine V2

    $scope.IsNewTimeLineV2Bound = false;
    $scope.IsShowNewTimeLineV2 = false;
    $scope.NewTimeLineLoadingV2 = true;
    $scope.TimeLineViewOrderDate = false;
    $scope.TimeLineViewOrderLTLV2 = true;
    $scope.ShowTimeLineV2OrderDate = false;
    $scope.IsShowTimeLineWithVehicle = true;
    $scope.TimeLineV2DateRequest = { fData: null, tData: null }
    $scope.NewTimeLineDetail = false;
    $scope.NewTimeLineVehicleSearchString = "";
    $scope.NewTimeLineResourceType = 1;
    $scope.NewTimeLineVehicleDataStatus = [];
    $scope.NewTimeLineVehicleDataCustomer = [];
    $scope.NewTimeLineVehicleItem = {
        ID: -1, VendorID: -1, VendorName: '',
        RegNo: '', MaxWeight: 0, RomoocNo: '', DriverName: ''
    }
    $scope.TimeLineViewVehicleV2 = true;
    $scope.TimeLineEventDragDrop = false;
    $scope.TimeLineViewOrderV2WithTimeLineFilter = false;
    $scope.TimeLineViewTripAction = false;
    
    $scope.LoadNewTimeLineV2Data = function (flag) {
        if (flag == null || flag == undefined)
            flag = true;
        $scope.NewTimeLineLoadingV2 = true;
        $scope.IsNewTimeLineV2Bound = false;
        Common.Services.Call($http, {
            url: Common.Services.url.OPS,
            method: _OPSAppointment_DIViewOnMapV4.URL.New_Schedule_Data,
            data: {
                isShowVehicle: $scope.IsShowTimeLineWithVehicle,
                strVehicle: $scope.NewTimeLineVehicleSearchString,
                typeOfResource: $scope.NewTimeLineResourceType,
                dataCus: $scope.NewTimeLineVehicleDataCustomer,
                dataStt: $scope.NewTimeLineVehicleDataStatus
            },
            success: function (res) {
                $scope.IsShowNewTimeLineV2 = true;
                if ($scope.NewTimeLineResourceType == 1) {
                    Common.Data.Each(res.DataSources, function (o) {
                        o.field = o.VendorID + "_" + o.VehicleID;
                    })
                    var dataSource = new kendo.data.SchedulerDataSource({
                        data: res.DataSources,
                        schema: {
                            model: {
                                id: "id",
                                fields: {
                                    id: { from: "ID", type: "number" },
                                    title: { from: "Title" },
                                    start: { type: "date", from: "StartDate" },
                                    end: { type: "date", from: "EndDate" },
                                    field: { from: "field" }
                                }
                            }
                        }
                    });
                    $scope.new_timeline_v2.setDataSource(dataSource);

                    var data = [];
                    Common.Data.Each(res.Resources, function (o) {
                        var obj = {
                            value: o.VendorID + "_" + o.VehicleID, text: o.Text, VendorID: o.VendorID, VendorCode: o.VendorCode, VendorName: o.VendorName,
                            RomoocID: o.RomoocID, RomoocNo: o.RomoocNo, VehicleID: o.VehicleID, VehicleNo: o.VehicleNo, IsChoose: false
                        }
                        data.push(obj);
                    })
                    if (data.length == 0) {
                        data.push({
                            value: '-2_-1', text: "DL trống", VendorID: -1, VendorCode: "", VendorName: "", VehicleID: -1, VehicleNo: "", IsChoose: false
                        });
                    }
                    $scope.new_timeline_v2.resources[0].dataSource.data(data);
                }
                else if ($scope.NewTimeLineResourceType == 2) {
                    var dataSource = new kendo.data.SchedulerDataSource({
                        data: res.DataSources,
                        schema: {
                            model: {
                                id: "id",
                                fields: {
                                    id: { from: "ID", type: "number" },
                                    title: { from: "Title" },
                                    start: { type: "date", from: "StartDate" },
                                    end: { type: "date", from: "EndDate" },
                                    field: { from: "GroupID" }
                                }
                            }
                        }
                    });
                    $scope.new_timeline_v2.setDataSource(dataSource);

                    var data = [];
                    Common.Data.Each(res.Resources, function (o) {
                        var obj = {
                            value: o.Value, text: o.Text, IsChoose: false
                        }
                        data.push(obj);
                    })
                    if (data.length == 0) {
                        data.push({
                            value: '-1', text: "DL trống", IsChoose: false
                        });
                    }
                    $scope.new_timeline_v2.resources[0].dataSource.data(data);
                }
            }
        })
        if (flag) {
            $scope.NewTimeLineGroupSelected = [];
            $scope.timeline_gopV2_Grid.dataSource.read();
        }
    }

    $timeout(function () { $scope.LoadNewTimeLineV2Data(false) }, 1000);

    $scope.timelineV2Splitter_Options = {
        orientation: "horizontal",
        panes: [
            { collapsible: true, resizable: true, min: '500px' },
            { collapsible: true, resizable: true, size: '500px', min: '330px' }
        ],
        resize: function (e) {
            try {
                $scope.new_timeline_v2.refresh();
            }
            catch (e) { }
        }
    }

    $scope.new_timeline_v2Options = {
        date: new Date(), footer: false, snap: false,
        eventHeight: 20, majorTick: 60, height: '99%', messages: { today: "Hôm nay" },
        editable: { create: false, destroy: false, move: true, resize: true, update: false },
        //editable: { create: false, destroy: false, move: false, resize: false, update: false },
        views: [
            {
                type: "timeline",
                title: "Ngày",
                columnWidth: 40,
                selectedDateFormat: "{0:dd-MM-yyyy}",
                dateHeaderTemplate: kendo.template("<strong>#=kendo.toString(date, 'dd/MM')#</strong>"),
                majorTimeHeaderTemplate: kendo.template("<strong>#=1+Math.round(kendo.toString(date, 'HH'))#:00</strong>"),
                majorTick: 120
            },
            {
                type: "timelineWeek",
                title: "Tuần",
                columnWidth: 40,
                selectedDateFormat: "{0:dd-MM-yyyy} - {1:dd-MM-yyyy}",
                dateHeaderTemplate: kendo.template("<strong>#=kendo.toString(date, 'dd/MM')#</strong>"),
                majorTimeHeaderTemplate: kendo.template("<strong>#=3+Math.round(kendo.toString(date, 'HH'))#:00</strong>"),
                majorTick: 360
            },
            {
                type: "timelineMonth",
                title: "Tháng",
                columnWidth: 60,
                selectedDateFormat: "{0:MM-yyyy}",
                dateHeaderTemplate: kendo.template("<strong>#=kendo.toString(date, 'dd/MM')#</strong>"),
                majorTimeHeaderTemplate: kendo.template("<strong>#=3+Math.round(kendo.toString(date, 'HH'))#:00</strong>"),
                majorTick: 360
            }
        ],
        dataSource: {
            data: [],
            schema: {
                model: {
                    id: "id",
                    fields: {
                        id: { from: "ID", type: "number" },
                        title: { from: "Title" },
                        start: { type: "date", from: "StartDate" },
                        end: { type: "date", from: "EndDate" },
                        field: { from: "field" }
                    }
                }
            }
        },
        groupHeaderTemplate: kendo.template("<input style='position:relative;top:2px;display:none !important;' type='checkbox' data-uid='#=value#' class='chk_vehicle_timeline' /><span data-uid='#=value#' class='txtTimeLineVehicle' style='cursor: pointer;' title='Click xem chi tiết'><strong>#=text#</strong></span><i class='fa fa-spinner fa-spin' style='color:rgb(248, 248, 248);padding: 0 2px;'></i>"),
        eventTemplate: $("#new-timeline-v2-event-template").html(),
        group: { resources: ["Group"], orientation: "vertical" },
        dataBound: function (e) {
            var scheduler = this;
            $scope.TimeLineToMonAvailable = false;
            $scope.TimeLineToOpsAvailable = false;
            $(scheduler.element).find('.k-scheduler-navigation .k-nav-current a').on('click', false);
            $timeout(function () {
                if ($scope.IsNewTimeLineV2Bound == false && $scope.IsShowNewTimeLineV2 == true) {
                    $scope.IsNewTimeLineV2Bound = true;
                    scheduler.view(scheduler.view().name);
                    //scheduler.element.find('.k-nav-today a').trigger('click');
                } else if ($scope.IsNewTimeLineV2Bound == true && $scope.IsShowNewTimeLineV2 == true) {
                    var data = scheduler.dataSource.data();
                    Common.Data.Each(scheduler.items(), function (o) {
                        Common.Data.Each(data, function (i) {
                            if (i.uid == $(o).data('uid')) {
                                switch (i.TypeOfEvent) {
                                    case -1:
                                        $(o).addClass('vendor-trip');
                                        $(o).find('.k-resize-handle').hide();
                                        break;
                                    case 1:
                                        if (i.StatusOfEvent == 1) {
                                            $(o).addClass('approved');
                                        } else if (i.StatusOfEvent == 2) {
                                            $(o).addClass('tendered');
                                            $(o).find('.k-resize-handle').hide();
                                        } else if (i.StatusOfEvent == 3) {
                                            $(o).addClass('recieved');
                                            $(o).find('.k-resize-handle').hide();
                                        } else if (i.StatusOfEvent == 11) {
                                            $(o).addClass('tenderable');
                                        } else {
                                            $(o).addClass('undefined');
                                            $(o).find('.k-resize-handle').hide();
                                        }
                                        break;
                                    case 2:
                                        $(o).addClass('maintainance');
                                        $(o).find('.k-resize-handle').hide();
                                        break;
                                    case 3:
                                        $(o).addClass('registry');
                                        $(o).find('.k-resize-handle').hide();
                                        break;
                                    case 4:
                                        $(o).addClass('repair');
                                        $(o).find('.k-resize-handle').hide();
                                        break;
                                    default:
                                        $(o).find('.k-resize-handle').hide();
                                        break;
                                }
                            }
                        })
                    })

                    if ($scope.NewTimeLineResourceType < 2) {
                        scheduler.element.find('span.txtTimeLineVehicle').each(function () {
                            $(this).unbind('click');
                            $(this).click(function (e) {
                                var uid = $(e.currentTarget).data('uid');

                                $scope.NewTimeLineVehicleItem.ID = uid.split('_')[1];
                                $scope.NewTimeLineVehicleItem.VendorID = uid.split('_')[0];
                                if ($scope.NewTimeLineVehicleItem.ID > 0) {
                                    if ($scope.NewTimeLineVehicleItem.VendorID == -1) {
                                        $scope.vehicle_map_win.center().open();
                                        $scope.IsVehicleMapActived = true;
                                        $scope.VehicleMapRequestDate = new Date();
                                        $timeout(function () {                                            
                                            $scope.vehMap_Grid.dataSource.read();
                                            $rootScope.IsLoading = false;
                                        }, 100);
                                    } else {
                                        Common.Services.Call($http, {
                                            url: Common.Services.url.OPS,
                                            method: _OPSAppointment_DIViewOnMapV4.URL.TimeLine_VehicleInfo,
                                            data: { venID: $scope.NewTimeLineVehicleItem.VendorID, vehID: $scope.NewTimeLineVehicleItem.ID, now: new Date() },
                                            success: function (res) {
                                                $scope.NewTimeLineVehicleItem.VendorName = res.VendorName;
                                                $scope.NewTimeLineVehicleItem.MaxWeight = res.MaxWeight;
                                                $scope.NewTimeLineVehicleItem.RegNo = res.Regno;
                                                $scope.NewTimeLineVehicleItem.DriverName = res.DriverName;
                                                $timeout(function () {
                                                    $scope.new_timeline_vehicle_info_win.center().open();
                                                }, 100)
                                            }
                                        });
                                    }
                                }
                            })
                        })
                        scheduler.element.find('.chk_vehicle_timeline').each(function () {
                            $(this).change(function (e) {
                                var uid = $(e.target).data('uid');
                                var flag = $(e.target).prop('checked');
                                var data = scheduler.resources[0].dataSource.data();
                                Common.Data.Each(data, function (o) {
                                    if (o.value == uid) {
                                        o.IsChoose = flag;
                                    }
                                })
                                var tmp = [];
                                Common.Data.Each(data, function (o) {
                                    if (o.IsChoose == true) {
                                        tmp[o.value] = true;
                                    }
                                })

                                var view = scheduler.view();
                                var fDate = view.startDate();
                                var tDate = view.endDate();
                                $scope.NewTimeLineToMonAvailable = false;
                                $scope.NewTimeLineToOpsAvailable = false;
                                Common.Data.Each(scheduler.dataSource.data(), function (o) {
                                    if (tmp[o.field] && ((o.start > fDate && o.start < tDate) || (o.end > fDate && o.end < tDate))) {
                                        if (o.Status == 1) {
                                            $timeout(function () {
                                                $scope.NewTimeLineToMonAvailable = true;
                                            }, 1)
                                        } else {
                                            $timeout(function () {
                                                $scope.NewTimeLineToOpsAvailable = true;
                                            }, 1)
                                        }
                                    }
                                })
                            })
                        })
                        scheduler.element.find('.k-scheduler-content tr td').each(function (idx, td) {
                            var slot = scheduler.slotByElement(td), resource = scheduler.resources[0].dataSource.data();
                            if (Common.HasValue(slot) && Common.HasValue(resource[slot.groupIndex])) {
                                var uid = resource[slot.groupIndex].value;
                                if (uid != null && uid.split('_')[1] == -1 && uid.split('_')[0] >= -1) {
                                    $(td).css('background', 'rgb(255, 249, 158)');
                                }
                            }
                        })
                        scheduler.element.find('.k-scheduler-times tr').each(function (idx, tr) {
                            var uid = $(tr).find('input.chk_vehicle_timeline').data('uid');
                            if (uid != null && uid.split('_')[1] == -1 && uid.split('_')[0] >= -1) {
                                $(tr).css('background', 'rgb(255, 249, 158)');
                                $(tr).find('i').css('color', 'rgb(255, 249, 158)');
                            }
                        })
                    }
                    $scope.InitTimeLineDropV2(scheduler);
                    if ($scope.NewTimeLineResourceType == 1) {
                        var thGroup = angular.element('.new-timeline-trip-v2 .k-scheduler-layout>tbody>tr>td>.k-scheduler-times .k-scheduler-table:eq(0) tr:eq(0)>th');
                        thGroup.empty();
                        thGroup.append($compile("<a href='/' style='width:100%;' title='Nhấn để đổi cách hiển thị' class='k-button' ng-click='NewTimeLineVehicleVisible_Click($event)'>Ẩn/hiện xe</a>")($scope));
                    }
                    if ($scope.NewTimeLineResourceType < 2 && $scope.IsShowTimeLineWithVehicle) {
                        var thVehicle = angular.element('.new-timeline-trip-v2 .k-scheduler-layout>tbody>tr>td>.k-scheduler-times .k-scheduler-table:eq(0) tr:eq(1)>th');
                        thVehicle.empty();
                        thVehicle.append($compile("<input class='k-textbox my-textbox' ng-model='NewTimeLineVehicleSearchString' style='width:calc(100% - 26px);text-align:center;font-weight: bold;color:#46bdfc;'/><a href='/' class='k-button' ng-click='NewTimeLineVehicleFilter_Click($event)'><i class='fa fa-refresh'></i></a>")($scope));
                    }
                    $scope.$apply();
                    if ($scope.TimeLineViewTripAction) {
                        $scope.InitTimeLineViewActionDragDrop(scheduler);
                    }
                    $timeout(function () {
                        $scope.NewTimeLineLoadingV2 = false;
                    }, 1000)
                }
            }, 10)
        },
        resources: [
            {
                field: "field", name: "Group", dataSource: [{ value: '', text: 'Không có DL' }], multiple: true
            }
        ],
        moveStart: function (e) {
            if (!$scope.TimeLineViewTripAction) {
                if (e.event.TypeOfEvent != 1 || e.event.StatusOfEvent == 2 || e.event.StatusOfEvent == 3 || $scope.NewTimeLineResourceType != 1) {
                    e.preventDefault();
                } else {
                    $scope.NewTripTimeLineEdit = $.extend(true, {}, e.event);
                    $scope.TimeLineEventDragDrop = true;
                    $scope.HideTimeLineV2Tooltip();
                }
            } else {
                e.preventDefault();
            }
        },
        resizeStart: function (e) {
            if (!$scope.TimeLineViewTripAction) {
                if (e.event.TypeOfEvent != 1 || e.event.StatusOfEvent == 2 || e.event.StatusOfEvent == 3 || $scope.NewTimeLineResourceType != 1) {
                    e.preventDefault();
                } else {
                    $scope.NewTripTimeLineEdit = $.extend(true, {}, e.event);
                    $scope.TimeLineEventDragDrop = true;
                    $scope.HideTimeLineV2Tooltip();
                }
            } else {
                e.preventDefault();
            }
        },
        save: function (e) {
            e.preventDefault();
            $scope.TimeLineEventDragDrop = false;
            $scope.HideTimeLineV2Tooltip();
            var scheduler = this, obj = $.extend(true, {}, e.event), data = scheduler._data, field = "";
            if (typeof obj.field == "string") field = obj.field;
            else if (typeof obj.field == "object") field = obj.field[0];
            var ds = new kendo.data.DataSource({
                data: data,
                filter: [{
                    logic: 'or',
                    filters: [{
                        logic: 'and',
                        filters: [
                            { field: 'start', operator: 'gte', value: obj.start },
                            { field: 'end', operator: 'gte', value: obj.start },
                            { field: 'start', operator: 'lt', value: obj.end }
                        ]
                    }, {
                        logic: 'and',
                        filters: [
                            { field: 'start', operator: 'lte', value: obj.start },
                            { field: 'end', operator: 'gt', value: obj.start }
                        ]
                    }, {
                        logic: 'and',
                        filters: [
                            { field: 'start', operator: 'lte', value: obj.end },
                            { field: 'end', operator: 'gt', value: obj.end }
                        ]
                    }]
                }, {
                    field: 'field', operator: 'eq', value: field
                }, {
                    logic: 'or',
                    filters: [{
                        field: 'StatusOfEvent', operator: 'eq', value: 1
                    }, {
                        field: 'StatusOfEvent', operator: 'eq', value: 2
                    }, {
                        field: 'StatusOfEvent', operator: 'eq', value: 11
                    }]
                }]
            })
            ds.fetch(function () {
                var view = this.view();
                //Kéo 1 chuyến vào 1 chuyến
                if (view.length > 0 && $.grep(view, function (o) { return o.id == obj.id }).length == 0) {
                    var objDrop = view[0];
                    $rootScope.Message({ Msg: "Chức năng chưa có! Vui lòng chờ!.", Type: Common.Message.Type.Alert })
                    var idx = 0;
                    $.each(scheduler.dataSource.data(), function (i, o) {
                        if (o.id == obj.id) {
                            idx = i;
                        }
                    })
                    var item = scheduler.dataSource.at(idx);
                    if (Common.HasValue(item)) {
                        item.end = $scope.NewTripTimeLineEdit.end;
                        item.start = $scope.NewTripTimeLineEdit.start;
                        item.field = $scope.NewTripTimeLineEdit.field;
                    }
                    scheduler.refresh();
                } else {
                    var venID = -1, vehID = -1;
                    if (typeof obj.field == "string") {
                        venID = obj.field.split('_')[0], vehID = obj.field.split('_')[1];
                    } else if (typeof obj.field == "object") {
                        venID = obj.field[0].split('_')[0], vehID = obj.field[0].split('_')[1];
                    }
                    if (vehID < 1) {
                        $rootScope.Message({ Msg: "Không thể lưu. Vui lòng chọn xe.", Type: Common.Message.Type.Alert })
                        var idx = 0;
                        $.each(scheduler.dataSource.data(), function (i, o) {
                            if (o.id == obj.id) {
                                idx = i;
                            }
                        })
                        var item = scheduler.dataSource.at(idx);
                        if (Common.HasValue(item)) {
                            item.end = $scope.NewTripTimeLineEdit.end;
                            item.start = $scope.NewTripTimeLineEdit.start;
                            item.field = $scope.NewTripTimeLineEdit.field;
                        }
                        scheduler.refresh();
                    } else {
                        if (venID > 0) {
                            $rootScope.Message({
                                Msg: "Xác nhận lưu thay đổi?",
                                Type: Common.Message.Type.Confirm,
                                Ok: function () {
                                    $rootScope.IsLoading = true;
                                    Common.Services.Call($http, {
                                        url: Common.Services.url.OPS,
                                        method: _OPSAppointment_DIViewOnMapV4.URL.DI2View_Master_Update_TimeLine,
                                        data: { mID: obj.id, vendorID: venID, vehicleID: vehID, ETD: obj.start, ETA: obj.end },
                                        success: function (res) {
                                            Common.Services.Error(res, function (res) {
                                                $scope.ChangeData = true;
                                                $rootScope.IsLoading = false;
                                                $rootScope.Message({ Msg: 'Thành công!' })
                                                scheduler.refresh();
                                            }, function () {
                                                var idx = 0;
                                                $.each(scheduler.dataSource.data(), function (i, o) {
                                                    if (o.id == obj.id) {
                                                        idx = i;
                                                    }
                                                })
                                                var item = scheduler.dataSource.at(idx);
                                                if (Common.HasValue(item)) {
                                                    item.end = $scope.NewTripTimeLineEdit.end;
                                                    item.start = $scope.NewTripTimeLineEdit.start;
                                                    item.field = $scope.NewTripTimeLineEdit.field;
                                                }
                                                scheduler.refresh();
                                                $rootScope.IsLoading = false;
                                            })
                                        },
                                        error: function () {
                                            var idx = 0;
                                            $.each(scheduler.dataSource.data(), function (i, o) {
                                                if (o.id == obj.id) {
                                                    idx = i;
                                                }
                                            })
                                            var item = scheduler.dataSource.at(idx);
                                            if (Common.HasValue(item)) {
                                                item.end = $scope.NewTripTimeLineEdit.end;
                                                item.start = $scope.NewTripTimeLineEdit.start;
                                                item.field = $scope.NewTripTimeLineEdit.field;
                                            }
                                            scheduler.refresh();
                                            $rootScope.IsLoading = false;
                                        }
                                    });
                                },
                                Close: function () {
                                    $rootScope.IsLoading = true;
                                    var idx = 0;
                                    $.each(scheduler.dataSource.data(), function (i, o) {
                                        if (o.id == obj.id) {
                                            idx = i;
                                        }
                                    })
                                    var item = scheduler.dataSource.at(idx);
                                    if (Common.HasValue(item)) {
                                        item.end = $scope.NewTripTimeLineEdit.end;
                                        item.start = $scope.NewTripTimeLineEdit.start;
                                        item.field = $scope.NewTripTimeLineEdit.field;
                                    }
                                    scheduler.refresh();
                                    $rootScope.IsLoading = false;
                                }
                            })
                        } else {
                            //==X==
                            $rootScope.IsLoading = true;
                            Common.Services.Call($http, {
                                url: Common.Services.url.OPS,
                                method: _OPSAppointment_DIViewOnMapV4.URL.TripByID,
                                data: { masterID: obj.id },
                                success: function (res) {
                                    if (Common.HasValue(res) && res.ID > 0) {
                                        $scope.NewTimeLineItem = {
                                            ID: res.ID,
                                            Code: res.Code,
                                            DriverName: res.DriverName,
                                            DriverTel: res.DriverTel,
                                            StatusCode: 'Có thể cập nhật',
                                            StatusColor: $scope.Color.None,
                                            VehicleID: vehID,
                                            VehicleNo: "",
                                            VendorOfVehicleID: res.VendorOfVehicleID,
                                            VendorOfVehicleCode: res.VendorCode,
                                            Ton: res.TotalTon,
                                            CBM: res.TotalCBM,
                                            Status: obj.Status,
                                            ETD: obj.start, ETA: obj.end,
                                            ListOPSGOP: [],
                                            ListOPSGOPName: [],
                                            LocationStartID: res.LocationStartID,
                                            LocationStartName: res.LocationStartName,
                                            LocationEndID: res.LocationEndID,
                                            LocationEndName: res.LocationEndName,
                                            LocationStartLat: res.LocationStartLat,
                                            LocationStartLng: res.LocationStartLng,
                                            LocationEndLat: res.LocationEndLat,
                                            LocationEndLng: res.LocationEndLng,
                                            ListLocation: res.ListLocation,
                                            HasChange: false
                                        }
                                        $scope.NewTimeLineDetail = true;
                                        if ($scope.NewTimeLineItem.VendorOfVehicleID == null)
                                            $scope.NewTimeLineItem.VendorOfVehicleID = -1;
                                        $scope.LoadDataNewTimeLineInfo(false);
                                        $timeout(function () {
                                            if ($scope.NewTimeLineItem.VendorOfVehicleID > 0) {
                                                $scope.new_timeline_vendor_info_win.center().open();
                                            } else {
                                                $scope.new_timeline_info_win.center().open();
                                            }
                                            $rootScope.IsLoading = false;
                                        }, 100)
                                    }
                                }
                            });
                            var idx = 0;
                            $.each(scheduler.dataSource.data(), function (i, o) {
                                if (o.id == obj.id) {
                                    idx = i;
                                }
                            })
                            var item = scheduler.dataSource.at(idx);
                            if (Common.HasValue(item)) {
                                item.end = $scope.NewTripTimeLineEdit.end;
                                item.start = $scope.NewTripTimeLineEdit.start;
                                item.field = $scope.NewTripTimeLineEdit.field;
                            }
                            scheduler.refresh();
                        }
                    }
                }
            })
        }
    }

    $timeout(function () { $scope.timelineV2Splitter.resize(); }, 100);

    $scope.InitTimeLineDropV2 = function (scheduler) {        
        if ($scope.NewTimeLineResourceType < 2) {
            $(scheduler.element).find('.k-scheduler-times tr').kendoDropTarget({
                drop: function (e) {
                    var uid = e.draggable.currentTarget.data("uid"), grid = $scope.timeline_gopV2_Grid, item = grid.dataSource.getByUid(uid);
                    var str = e.dropTarget.html(), s1 = str.indexOf("data-uid="), s2 = str.indexOf("chk_vehicle_timeline"), s3 = s2 - s1 - 19, group = str.substr(s1 + 10, s3);
                    var flag = false, data = $scope.new_timeline_v2.resources[0].dataSource.data();
                    var tomasterID = -1, tomasterCode= "mới tạo", ton = 0, eta, etd, dataGop = [];
                    if ($scope.TimeLineViewOrderLTLV2) {
                        if (item.IsChoose) {
                            Common.Data.Each($scope.NewTimeLineGroupSelected, function (o) {
                                ton += o.Ton;
                                dataGop.push(o.ID);
                                if (!Common.HasValue(eta)) eta = o.ETA;
                                if (!Common.HasValue(etd)) etd = o.ETD;
                                if (eta < o.ETA) eta = o.ETA;
                                if (etd > o.ETD) etd = o.ETD;
                            })
                        } else {
                            dataGop.push(item.ID);
                            ton = item.Ton; eta = item.ETA; etd = item.ETD;
                        }
                    } else {
                        item = $(e.draggable.currentTarget).find('.txtCode').data('item');
                        tomasterID = item.ID; tomasterCode = item.Code; eta = item.ETA; etd = item.ETD;
                    }
                   
                    Common.Data.Each(data, function (o) { if (o.value == group) flag = true; })
                    if (flag) {
                        var venID = group.split('_')[0], vehID = group.split('_')[1];
                        Common.Services.Call($http, {
                            url: Common.Services.url.OPS,
                            method: _OPSAppointment_DIViewOnMapV4.URL.TimeLine_VehicleInfo,
                            data: { venID: venID, vehID: vehID, now: etd  || new Date() },
                            success: function (res) {
                                Common.Services.Error(res, function (res) {
                                    $scope.NewTimeLineItem = {
                                        ID: tomasterID,
                                        Code: tomasterCode,
                                        DriverName: res.DriverName,
                                        DriverTel: res.DriverTel,
                                        StatusCode: 'Chưa chọn xe',
                                        StatusColor: $scope.Color.Error,
                                        VehicleID: res.ID,
                                        VehicleNo: res.Regno,
                                        VendorOfVehicleID: venID,
                                        VendorOfVehicleCode: res.VendorName,
                                        Ton: ton,
                                        Status: 1,
                                        ETD: slot.startDate, ETA: slot.endDate.addDays((item.ETA - item.ETD) / (24 * 60 * 60 * 1000)),
                                        ListOPSGOP: dataGop,
                                        ListOPSGOPName: [],
                                        LocationStartID: _OPSAppointment_DIViewOnMapV4.Data.Location.LocationStartID,
                                        LocationStartName: _OPSAppointment_DIViewOnMapV4.Data.Location.LocationStartName,
                                        LocationEndID: _OPSAppointment_DIViewOnMapV4.Data.Location.LocationEndID,
                                        LocationEndName: _OPSAppointment_DIViewOnMapV4.Data.Location.LocationEndName,
                                        LocationStartLat: _OPSAppointment_DIViewOnMapV4.Data.Location.LocationStartLat,
                                        LocationStartLng: _OPSAppointment_DIViewOnMapV4.Data.Location.LocationStartLng,
                                        LocationEndLat: _OPSAppointment_DIViewOnMapV4.Data.Location.LocationEndLat,
                                        LocationEndLng: _OPSAppointment_DIViewOnMapV4.Data.Location.LocationEndLng,
                                        ListLocation: [],
                                        HasChange: false
                                    }
                                    $scope.NewTimeLineDetail = true;
                                    if ($scope.NewTimeLineItem.VendorOfVehicleID == null)
                                        $scope.NewTimeLineItem.VendorOfVehicleID = -1;
                                    $scope.LoadDataNewTimeLineInfo(false);
                                    $timeout(function () {
                                        if ($scope.NewTimeLineItem.VendorOfVehicleID > 0) {
                                            $scope.new_timeline_vendor_info_win.center().open();
                                        } else {
                                            $scope.new_timeline_info_win.center().open();
                                        }
                                    }, 100)
                                })
                            }
                        })
                    }
                },
                group: "timelineGroup",
            });

            $(scheduler.element).find('.k-scheduler-content td').kendoDropTarget({
                drop: function (e) {
                    $timeout(function () {
                        var uid = e.draggable.currentTarget.data("uid"), grid = $scope.timeline_gopV2_Grid, item = grid.dataSource.getByUid(uid);
                        var scheduler = $scope.new_timeline_v2, slot = scheduler.slotByElement(e.dropTarget), resource = scheduler.resources[0].dataSource.data();
                        var tomasterID = -1, tomasterCode = "mới tạo", ton = 0, eta, etd, dataGop = [];
                        if ($scope.TimeLineViewOrderLTLV2) {
                            if (item.IsChoose) {
                                Common.Data.Each($scope.NewTimeLineGroupSelected, function (o) {
                                    ton += o.Ton;
                                    dataGop.push(o.ID);
                                    if (!Common.HasValue(eta)) eta = o.ETA;
                                    if (!Common.HasValue(etd)) etd = o.ETD;
                                    if (eta < o.ETA) eta = o.ETA;
                                    if (etd > o.ETD) etd = o.ETD;
                                })
                            } else {
                                dataGop.push(item.ID);
                                ton = item.Ton; eta = item.ETA; etd = item.ETD;
                            }
                        } else {
                            item = $(e.draggable.currentTarget).find('.txtCode').data('item');
                            tomasterID = item.ID; tomasterCode = item.Code; eta = item.ETA; etd = item.ETD;
                        }

                        if (Common.HasValue(resource[slot.groupIndex])) {
                            var obj = resource[slot.groupIndex];
                            var venID = obj.value.split('_')[0], vehID = obj.value.split('_')[1];
                            Common.Services.Call($http, {
                                url: Common.Services.url.OPS,
                                method: _OPSAppointment_DIViewOnMapV4.URL.TimeLine_VehicleInfo,
                                data: { venID: venID, vehID: vehID, now: slot.startDate },
                                success: function (res) {
                                    Common.Services.Error(res, function (res) {
                                        $scope.NewTimeLineItem = {
                                            ID: tomasterID,
                                            Code: tomasterCode,
                                            DriverName: res.DriverName,
                                            DriverTel: res.DriverTel,
                                            StatusCode: 'Có thể cập nhật',
                                            StatusColor: $scope.Color.None,
                                            VehicleID: res.ID,
                                            VehicleNo: res.Regno,
                                            VendorOfVehicleID: -1,
                                            VendorOfVehicleCode: res.VendorName,
                                            Ton: ton,
                                            Status: 1,
                                            ETD: slot.startDate, ETA: slot.endDate.addDays((item.ETA - item.ETD) / (24 * 60 * 60 * 1000)),
                                            ListOPSGOP: dataGop,
                                            ListOPSGOPName: [],
                                            LocationStartID: _OPSAppointment_DIViewOnMapV4.Data.Location.LocationStartID,
                                            LocationStartName: _OPSAppointment_DIViewOnMapV4.Data.Location.LocationStartName,
                                            LocationEndID: _OPSAppointment_DIViewOnMapV4.Data.Location.LocationEndID,
                                            LocationEndName: _OPSAppointment_DIViewOnMapV4.Data.Location.LocationEndName,
                                            LocationStartLat: _OPSAppointment_DIViewOnMapV4.Data.Location.LocationStartLat,
                                            LocationStartLng: _OPSAppointment_DIViewOnMapV4.Data.Location.LocationStartLng,
                                            LocationEndLat: _OPSAppointment_DIViewOnMapV4.Data.Location.LocationEndLat,
                                            LocationEndLng: _OPSAppointment_DIViewOnMapV4.Data.Location.LocationEndLng,
                                            ListLocation: [],
                                            HasChange: false
                                        }

                                        $scope.NewTimeLineDetail = true;
                                        if ($scope.NewTimeLineItem.VendorOfVehicleID == null)
                                            $scope.NewTimeLineItem.VendorOfVehicleID = -1;
                                        $scope.LoadDataNewTimeLineInfo(false);
                                        $timeout(function () {
                                            if ($scope.NewTimeLineItem.VendorOfVehicleID > 0) {
                                                $scope.new_timeline_vendor_info_win.center().open();
                                            } else {
                                                $scope.new_timeline_info_win.center().open();
                                            }
                                        }, 100)
                                    })
                                }
                            })
                        }
                    }, 1)
                },
                group: "timelineGroup",
            });
        }
        if ($scope.TimeLineViewOrderLTLV2) {
            $(scheduler.element).find('.k-scheduler-content .k-event').kendoDropTarget({
                drop: function (e) {
                    $rootScope.IsLoading = true;
                    var uid = e.draggable.currentTarget.data("uid"), grid = $scope.timeline_gopV2_Grid, item = grid.dataSource.getByUid(uid);
                    var str = e.dropTarget.html(), s1 = str.lastIndexOf("$event"), s2 = str.indexOf("timeline_v2_trip_info_win"), s3 = s2 - s1 - 8;
                    var sid = str.substr(s1 + 7, s3), obj = $scope.new_timeline_v2.dataSource.get(sid.split(',')[0]);
                    if (Common.HasValue(obj)) {
                        if (obj.TypeOfEvent == 1 && (obj.StatusOfEvent == 1 || obj.StatusOfEvent == 2 || obj.StatusOfEvent == 11)) {
                            var ton = 0, cbm = 0, dataGop = [];
                            if (item.IsChoose) {
                                Common.Data.Each($scope.NewTimeLineGroupSelected, function (o) {
                                    ton += o.Ton; cbm += o.CBM; dataGop.push(o.ID);
                                })
                            } else {
                                dataGop.push(item.ID); ton = item.Ton; cbm = item.CBM;
                            }
                            Common.Services.Call($http, {
                                url: Common.Services.url.OPS,
                                method: _OPSAppointment_DIViewOnMapV4.URL.TripByID,
                                data: { masterID: obj.id },
                                success: function (res) {
                                    if (Common.HasValue(res) && res.ID > 0) {
                                        $scope.NewTimeLineItem = {
                                            ID: res.ID,
                                            Code: res.Code,
                                            DriverName: res.DriverName,
                                            DriverTel: res.DriverTel,
                                            StatusCode: 'Có thể cập nhật',
                                            StatusColor: $scope.Color.None,
                                            VehicleID: res.VehicleID,
                                            VehicleNo: res.VehicleNo,
                                            VendorOfVehicleID: res.VendorOfVehicleID,
                                            VendorOfVehicleCode: res.VendorCode,
                                            Ton: res.TotalTon + ton,
                                            CBM: res.TotalCBM + cbm,
                                            Status: res.Status,
                                            ETD: obj.start, ETA: obj.end,
                                            ListOPSGOP: dataGop,
                                            ListOPSGOPName: [],
                                            LocationStartID: res.LocationStartID,
                                            LocationStartName: res.LocationStartName,
                                            LocationEndID: res.LocationEndID,
                                            LocationEndName: res.LocationEndName,
                                            LocationStartLat: res.LocationStartLat,
                                            LocationStartLng: res.LocationStartLng,
                                            LocationEndLat: res.LocationEndLat,
                                            LocationEndLng: res.LocationEndLng,
                                            ListLocation: res.ListLocation,
                                            HasChange: false
                                        }

                                        $scope.NewTimeLineDetail = true;
                                        if ($scope.NewTimeLineItem.VendorOfVehicleID == null)
                                            $scope.NewTimeLineItem.VendorOfVehicleID = -1;
                                        $scope.LoadDataNewTimeLineInfo(false);
                                        $timeout(function () {
                                            if ($scope.NewTimeLineItem.VendorOfVehicleID > 0) {
                                                $scope.new_timeline_vendor_info_win.center().open();
                                            } else {
                                                $scope.new_timeline_info_win.center().open();
                                            }
                                            $rootScope.IsLoading = false;
                                        }, 100)
                                    }
                                }
                            });
                        }
                        else {
                            $rootScope.IsLoading = false;
                            $rootScope.Message({
                                Msg: "Không thể thêm đơn hàng vào chuyến đã hoàn thành!", Type: Common.Message.Type.Alert
                            })
                        }
                    } else {
                        $rootScope.IsLoading = false;
                    }
                },
                group: "timelineGroup",
            });
        }
    }

    $scope.InitTimeLineDragV2 = function (grid) {
        if ($scope.TimeLineViewOrderLTLV2) {
            $(grid.element).kendoDraggable({
                filter: "tbody>tr:not(.k-grouping-row)", group: "timelineGroup", cursorOffset: { top: 0, left: 0 }, ignore: 'input,a',
                drag: function (e) {
                    if ($scope.NewTimeLineResourceType < 2) {
                        if ($(e.elementUnderCursor).is('td[data-role="droptarget"]')) {
                            var uid = e.currentTarget.data("uid"), grid = $scope.timeline_gopV2_Grid, item = grid.dataSource.getByUid(uid);
                            var table = $(e.elementUnderCursor).closest('.k-scheduler-content');
                            angular.forEach(table.find('td[data-role="droptarget"].hight-light'), function (td) {
                                $(td).removeClass('hight-light');
                            })
                            $(e.elementUnderCursor).addClass("hight-light");
                            var eta = item.ETA, etd = item.ETD;
                            if (item.IsChoose) {
                                Common.Data.Each(grid.dataSource.data(), function (o) {
                                    if (o.IsChoose) {
                                        if (o.ETA > eta) eta = o.ETA;
                                        if (o.ETD < etd) etd = o.ETD;
                                    }
                                })
                            }
                            var scheduler = $scope.new_timeline_v2, ele = e.elementUnderCursor, slot = scheduler.slotByElement(ele);
                            if (Common.HasValue(slot)) {
                                var time = slot.endDate;
                                while (Common.HasValue(slot) && slot.endDate - time <= eta - etd) {
                                    $(ele).addClass("hight-light"); ele = ele.nextSibling;
                                    if (Common.HasValue(ele) && ele != []) slot = scheduler.slotByElement(ele)
                                    else slot = null;
                                }
                            }
                        }
                    }
                },
                dragend: function (e) {
                    $timeout(function () {
                        var table = $($scope.new_timeline_v2.element).find('.k-scheduler-content');
                        angular.forEach(table.find('td[data-role="droptarget"].hight-light'), function (td) {
                            $(td).removeClass('hight-light');
                        })
                    }, 50)
                },
                hint: function (e) {
                    var grid = $scope.timeline_gopV2_Grid, item = grid.dataItem(e);
                    if (item.IsChoose) {
                        var html = '';
                        Common.Data.Each($scope.NewTimeLineGroupSelected, function (item) {
                            html += '<tr><td>' + item.CustomerCode + '</td>' + '<td>' + item.OrderCode + '</td>' + '<td>' + item.GroupProductCode + '</td></tr>';
                        })
                        return $('<div class="k-grid k-widget" style="background-color: #3ab9fc !important; color: #fff !important;"><table><tbody>' + html + '</tbody></table></div>');
                    } else {
                        return $('<div class="k-grid k-widget" style="background-color: #3ab9fc !important; color: #fff !important;"><table><tbody><tr>' +
                       '<td>' + item.CustomerCode + '</td>' + '<td>' + item.OrderCode + '</td>' + '<td>' + item.GroupProductCode + '</td>' + '</tr></tbody></table></div>');
                    }
                }
            });
        } else {
            angular.forEach($(grid.element).find('tbody>tr.k-grouping-row'), function (tr, idx) {                
                if ($(tr).find('.txtCode').length > 0) {
                    $(tr).kendoDraggable({
                        group: "timelineGroup", cursorOffset: { top: 0, left: 0 }, ignore: 'input,a',
                        drag: function (e) {
                            if ($scope.NewTimeLineResourceType < 2) {
                                if ($(e.elementUnderCursor).is('td[data-role="droptarget"]')) {
                                    var uid = $(e.currentTarget).find('.txtCode').data("uid"), item = $(e.currentTarget).find('.txtCode').data('item');
                                    var table = $(e.elementUnderCursor).closest('.k-scheduler-content');
                                    angular.forEach(table.find('td[data-role="droptarget"].hight-light'), function (td) {
                                        $(td).removeClass('hight-light');
                                    })
                                    $(e.elementUnderCursor).addClass("hight-light");
                                    var eta = Common.Date.FromJson(item.ETA), etd = Common.Date.FromJson(item.ETD);
                                    var scheduler = $scope.new_timeline_v2, ele = e.elementUnderCursor, slot = scheduler.slotByElement(ele);
                                    if (Common.HasValue(slot)) {
                                        var time = slot.endDate;
                                        while (Common.HasValue(slot) && slot.endDate - time <= eta - etd) {
                                            $(ele).addClass("hight-light"); ele = ele.nextSibling;
                                            if (Common.HasValue(ele) && ele != []) slot = scheduler.slotByElement(ele)
                                            else slot = null;
                                        }
                                    }
                                }
                            }
                        },
                        dragend: function (e) {
                            $timeout(function () {
                                var table = $($scope.new_timeline_v2.element).find('.k-scheduler-content');
                                angular.forEach(table.find('td[data-role="droptarget"].hight-light'), function (td) {
                                    $(td).removeClass('hight-light');
                                })
                            }, 50)
                        },
                        hint: function (e) {
                            item = $(e).find('.txtCode').data('item');
                            return $('<div class="k-grid k-widget" style="background-color: #3ab9fc !important; color: #fff !important;"><table><tbody><tr>' +
                               '<td>' + item.Code + '</td>' + '<td>' + Common.Date.FromJsonDMYHM(item.ETD) + '</td>' + '<td>' + Common.Date.FromJsonDMYHM(item.ETA) + '</td>' + '</tr></tbody></table></div>');
                        }
                    });
                }
            })            
        }
    }

    $scope.TimelineOrderV2ViewResource_Click = function ($event, scheduler, value) {
        $event.preventDefault();

        $scope.NewTimeLineResourceType = value;
    }

    $scope.$watch("NewTimeLineResourceType", function () {
        if ($scope.IsShowNewTimeLineV2)
            $scope.LoadNewTimeLineV2Data(false);
    })

    $scope.NewTimeLineGroupSelected = [];
    $scope.timeline_gopV2_Grid_Options = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.OPS,
            method: _OPSAppointment_DIViewOnMapV4.URL.Order_List,
            pageSize: 20,
            readparam: function () {
                return {
                    typeOfOrder: $scope.TimeLineViewOrderLTLV2 ? 1 : 2,
                    fDate: $scope.TimeLineV2DateRequest.fDate,
                    tDate: $scope.TimeLineV2DateRequest.tDate,
                    dataCus: $scope.TimeLineViewOrderV2WithTimeLineFilter ? $scope.NewTimeLineVehicleDataCustomer : []
                }
            },
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    IsChoose: { type: 'bool', defaultValue: false },
                    ETD: { type: 'date' },
                    ETA: { type: 'date' },
                    ETDRequest: { type: 'date' },
                    ETARequest: { type: 'date' },
                    DateGetEmpty: { type: 'date' },
                    DateReturnEmpty: { type: 'date' }
                }
            }
        }),
        pageable: Common.PageSize,
        height: '99%', groupable: true, sortable: true, columnMenu: false, resizable: true, filterable: { mode: 'row' }, reorderable: false, auboBind: false,
        dataBound: function () {
            Common.Log("timeline_gopV2_Grid_Options.dataBound")
            var grid = this;
            if ($scope.TimeLineViewOrderLTLV2) {
                grid.showColumn('Command');
                grid.hideColumn('CommandFTL');
            } else {
                grid.hideColumn('Command');
                grid.showColumn('CommandFTL');
            }
            Common.Data.Each(grid.items(), function (tr) {
                var item = grid.dataItem(tr);
                if (Common.HasValue(item) && Common.HasValue($scope.NewTimeLineGroupSelected[item.ID])) {
                    item.IsChoose = true;
                    $(tr).find('.chkChoose').prop('checked', true);
                }
            })
            $scope.InitTimeLineDragV2(grid);
        },
        columns: [
            {
                field: "Choose", title: ' ', width: '35px',
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,timeline_gopV2_Grid,timeline_gopV2_Grid_Choose_Change)" />',
                headerAttributes: { style: 'text-align: center;' }, attributes: { style: 'text-align: center;' },
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,timeline_gopV2_Grid,timeline_gopV2_Grid_Choose_Change)" />',
                filterable: false, sortable: false, groupable: false, sortorder: 0, configurable: false, isfunctionalHidden: false
            },
            {
                field: 'Command', title: ' ', width: 50, attributes: { style: 'text-align: center;' },
                template: '<a class="k-button small-button btn-merge" ng-show="dataItem.IsSplit" title="Gộp" href="/" ng-click="GroupProduct_Merge($event,dataItem,timeline_gopV2_Grid)">M</a>' +
                    '<a class="k-button small-button btn-merge-ok" style="display:none;" title="Xác nhận" href="/" ng-click="GroupProduct_Merge_OK($event,dataItem,timeline_gopV2_Grid)">S</a>' +
                    '<input type="checkbox" style="display:none;" class="chk-select-to-merge" />',
                filterable: false, sortable: false, groupable: false, sortorder: 1, configurable: false, isfunctionalHidden: true
            },
            {
                field: 'CommandFTL', title: ' ', width: 30, attributes: { style: 'text-align: center;' },
                template: '<a class="k-button small-button" title="Hủy đơn hàng" href="/" ng-click="GroupProduct_Cancel($event,dataItem,timeline_gopV2_Grid)">C</a>',
                filterable: false, sortable: false, groupable: false, sortorder: 2, configurable: false, isfunctionalHidden: true
            },
            {
                field: 'TOMasterCode', width: 0, title: ' ', hidden: true,
                groupHeaderTemplate: function (e) {
                    var uid = -1;
                    var obj = { ID: -1, Code: "", ETD: null, ETA: null }
                    try {
                        var idx = 1;
                        var o = e.aggregates.parent();
                        while (idx < 100 && o.hasSubgroups == true && o.items != null && o.items.length > 0) {
                            o = o.items[0].aggregates.parent();
                            idx++;                           
                        }
                        uid = o.items[0].TOMasterID;
                        obj = { ID: o.items[0].TOMasterID, Code: e.value, ETD: o.items[0].TOETD, ETA: o.items[0].TOETA }
                    } catch (e) { }
                    return "<span style='cursor:move;' class='txtCode' data-uid='" + uid + "' data-item='" + JSON.stringify(obj, Common.JSON.QuoteReplacer)
                        + "'>Mã chuyến: " + e.value + "</span><a href='/' class='my-button' data-uid='" + uid + "' ng-click='TimeLineFTLSplit_Click($event,timeline_gopV2_Grid,split_gop_win)'><span>Chia ĐH</span></a>";
                },
                filterable: { cell: { operator: 'contains', showOperators: false } }, sortorder: 3, configurable: false, isfunctionalHidden: true
            },
            {
                field: 'IsWarning', width: 80, title: 'Cảnh báo', attributes: { 'style': 'text-align: center;' }, headerAttributes: { 'style': 'text-align: center;' },
                template: '<img class="img-warning" data-value="#=WarningMsg#" ng-show="dataItem.IsWarning" src="images/function/ico_warning_active.png"/>', filterable: false, sortorder: 4, configurable: true, isfunctionalHidden: false
            },
            { field: 'CustomerCode', width: 100, title: 'Mã KH', filterable: { cell: { operator: 'contains', showOperators: false } }, sortorder: 5, configurable: true, isfunctionalHidden: false },
            { field: 'CustomerShortName', width: 100, title: 'Tên KH', filterable: { cell: { operator: 'contains', showOperators: false } }, sortorder: 6, configurable: true, isfunctionalHidden: false },
            { field: 'OrderCode', width: 100, title: 'Đơn hàng', filterable: { cell: { operator: 'contains', showOperators: false } }, sortorder: 7, configurable: true, isfunctionalHidden: false },
            { field: 'GroupProductCode', width: 100, title: 'Nhóm hàng', filterable: { cell: { operator: 'contains', showOperators: false } }, sortorder: 8, configurable: true, isfunctionalHidden: false },
            { field: 'ProductCode', width: 100, title: 'Hàng hóa', filterable: { cell: { operator: 'contains', showOperators: false } }, sortorder: 9, configurable: true, isfunctionalHidden: false },
            {
                field: 'Ton', width: 80, title: 'Tấn',
                template: '#if(!IsFTL&&Ton>0){#<form class="submit-form"><input kendo-numeric-text-box value="#:Ton#" k-options="gopTon_Options" data-k-max="#:Ton#" style="width:100%"/></form>#}else{##:Ton##}#',
                sortable: true, filterable: { cell: { operator: 'gte', showOperators: false } }, sortorder: 10, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'CBM', width: 80, title: 'Khối',
                template: '#if(!IsFTL&&CBM>0){#<form class="submit-form"><input kendo-numeric-text-box value="#:CBM#" k-options="gopCBM_Options" data-k-max="#:CBM#" style="width:100%"/></form>#}else{##:CBM##}#',
                sortable: true, filterable: { cell: { operator: 'gte', showOperators: false } }, sortorder: 11, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'Quantity', width: 80, title: 'S.Lượng',
                template: '#if(!IsFTL&&Quantity>0){#<form class="submit-form"><input kendo-numeric-text-box value="#:Quantity#" k-options="gopQuantity_Options" data-k-max="#:Quantity#" style="width:100%"/></form>#}else{##:Quantity##}#',
                sortable: true, filterable: { cell: { operator: 'gte', showOperators: false } }, sortorder: 12, configurable: true, isfunctionalHidden: false
            },
            { field: 'DNCode', width: 100, title: 'Số DN', filterable: { cell: { operator: 'contains', showOperators: false } }, sortorder: 14, configurable: true, isfunctionalHidden: false },
            { field: 'SOCode', width: 100, title: 'Số SO', filterable: { cell: { operator: 'contains', showOperators: false } }, sortorder: 15, configurable: true, isfunctionalHidden: false },
            {
                field: 'ETD', width: 110, title: 'ETD', template: "#=ETD==null?' ':kendo.toString(ETD, '" + Common.Date.Format.DMYHM + "')#",
                filterable: { cell: { template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); }, operator: 'equal', showOperators: false } }, sortorder: 16, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'ETA', width: 110, title: 'ETA', template: "#=ETA==null?' ':kendo.toString(ETA, '" + Common.Date.Format.DMYHM + "')#",
                filterable: { cell: { template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); }, operator: 'equal', showOperators: false } }, sortorder: 17, configurable: true, isfunctionalHidden: false
            },
            { field: 'TempMin', width: 100, title: 'N.Độ tối thiểu', template: '#=TempMin!=null?TempMin:""#', filterable: { cell: { operator: 'gte', showOperators: false } }, sortorder: 18, configurable: true, isfunctionalHidden: false },
            { field: 'TempMax', width: 100, title: 'N.Độ tối đa', template: '#=TempMax!=null?TempMax:""#', filterable: { cell: { operator: 'gte', showOperators: false } }, sortorder: 19, configurable: true, isfunctionalHidden: false },
            { field: 'DistributorCode', width: 120, title: 'Mã NPP', filterable: { cell: { operator: 'contains', showOperators: false } }, sortorder: 20, configurable: true, isfunctionalHidden: false },
            { field: 'DistributorName', width: 120, title: 'Tên NPP', filterable: { cell: { operator: 'contains', showOperators: false } }, sortorder: 21, configurable: true, isfunctionalHidden: false },
            { field: 'LocationFromCode', width: 120, title: 'Mã điểm nhận', filterable: { cell: { operator: 'contains', showOperators: false } }, sortorder: 22, configurable: true, isfunctionalHidden: false },
            { field: 'LocationFromName', width: 120, title: 'Tên điểm nhận', filterable: { cell: { operator: 'contains', showOperators: false } }, sortorder: 23, configurable: true, isfunctionalHidden: false },
            { field: 'LocationToCode', width: 120, title: 'Mã điểm giao', filterable: { cell: { operator: 'contains', showOperators: false } }, sortorder: 24, configurable: true, isfunctionalHidden: false },
            { field: 'LocationToName', width: 120, title: 'Tên điểm giao', filterable: { cell: { operator: 'contains', showOperators: false } }, sortorder: 25, configurable: true, isfunctionalHidden: false },
            { field: 'LocationFromAddress', width: 120, title: 'Địa chỉ nhận', filterable: { cell: { operator: 'contains', showOperators: false } }, sortorder: 26, configurable: true, isfunctionalHidden: false },
            { field: 'LocationFromDistrict', width: 120, title: 'Quận huyên nhận', filterable: { cell: { operator: 'contains', showOperators: false } }, sortorder: 27, configurable: true, isfunctionalHidden: false },
            { field: 'LocationFromProvince', width: 120, title: 'Tỉnh thành nhận', filterable: { cell: { operator: 'contains', showOperators: false } }, sortorder: 28, configurable: true, isfunctionalHidden: false },
            { field: 'LocationToAddress', width: 120, title: 'Địa chỉ giao', filterable: { cell: { operator: 'contains', showOperators: false } }, sortorder: 29, configurable: true, isfunctionalHidden: false },
            { field: 'LocationToDistrict', width: 120, title: 'Quận huyên giao', filterable: { cell: { operator: 'contains', showOperators: false } }, sortorder: 30, configurable: true, isfunctionalHidden: false },
            { field: 'LocationToProvince', width: 120, title: 'Tỉnh thành giao', filterable: { cell: { operator: 'contains', showOperators: false } }, sortorder: 31, configurable: true, isfunctionalHidden: false },
            { field: 'UserDefine1', width: 100, title: 'Ghi chú 1', filterable: { cell: { operator: 'contains', showOperators: false } }, sortorder: 32, configurable: true, isfunctionalHidden: false },
            { field: 'UserDefine2', width: 100, title: 'Ghi chú 2', filterable: { cell: { operator: 'contains', showOperators: false } }, sortorder: 33, configurable: true, isfunctionalHidden: false },
            { field: 'CUSRoutingCode', width: 100, title: 'Mã cung đường', filterable: { cell: { operator: 'contains', showOperators: false } }, sortorder: 34, configurable: true, isfunctionalHidden: false },
            { field: 'CUSRoutingName', width: 100, title: 'Tên cung đường', filterable: { cell: { operator: 'contains', showOperators: false } }, sortorder: 35, configurable: true, isfunctionalHidden: false },
            { field: 'CUSRoutingNote', width: 100, title: 'Ghi chú cung đường', filterable: { cell: { operator: 'contains', showOperators: false } }, sortorder: 36, configurable: true, isfunctionalHidden: false },
            { field: 'WarningTime', width: 100, title: 'TG cảnh báo', filterable: { cell: { operator: 'contains', showOperators: false } }, sortorder: 37, configurable: true, isfunctionalHidden: false },
            { field: 'WarningMsg', width: 100, title: 'ND cảnh báo', filterable: { cell: { operator: 'contains', showOperators: false } }, sortorder: 38, configurable: true, isfunctionalHidden: false },
            { field: 'GroupSort', width: 100, title: ' ', filterable: { cell: { operator: 'contains', showOperators: false } }, sortorder: 39, configurable: true, isfunctionalHidden: false },
            { title: '', filterable: false, sortable: false, sortorder: 100, configurable: false, isfunctionalHidden: true }
        ]
    };

    $scope.timeline_gopV2_Grid_Choose_Change = function ($event, grid, haschange) {
        if ($scope.TimeLineViewOrderLTLV2) {
            Common.Data.Each(grid.dataSource.data(), function (o) {
                if (o.IsChoose) {
                    $scope.NewTimeLineGroupSelected[o.ID] = $.extend(true, {}, o);
                } else if (Common.HasValue($scope.NewTimeLineGroupSelected[o.ID])) {
                    $scope.NewTimeLineGroupSelected[o.ID] = null;
                }
            })
        } else {
            $scope.NewTimeLineGroupSelected = [];
        }
    }
    
    $scope.NewTimeLineVehicleVisible_Click = function ($event) {
        $event.preventDefault();

        $scope.IsShowTimeLineWithVehicle = !$scope.IsShowTimeLineWithVehicle;
        $scope.LoadNewTimeLineV2Data(false);
    }
    
    $scope.timelinetooltipV2Options = {
        filter: ".img-warning", position: "top",
        content: function (e) { return $(e.target).data('value'); }
    }

    $scope.TimelineOrderV2ViewStatus_Click = function ($event, grid) {
        $event.preventDefault();
        try {
            $scope.NewTimeLineGroupSelected = [];
            $scope.TimeLineViewTripAction = false;
            $scope.ShowTimeLineV2OrderDate = false;
            var value = $($event.currentTarget).data('tabindex');
            var group = grid.dataSource.group();
            if (value == 1) {
                grid.showColumn('Choose');
                $scope.TimeLineViewOrderLTLV2 = true;
                if (group != null && group.length > 0) {
                    var i = 0;
                    while (i < group.length) {
                        var o = group[i];
                        if (o.field == 'TOMasterCode') {
                            group.splice(i, 1);
                        } else {
                            i++;
                        }
                    }
                    grid.dataSource.group(group);
                } else {
                    grid.dataSource.read();
                }
            } else {                
                grid.hideColumn('Choose');
                $scope.TimeLineViewOrderLTLV2 = false;

                if (group == null || group.length == 0) {
                    group = [{ field: 'TOMasterCode', dir: 'desc' }];
                    grid.dataSource.group(group);
                } else {
                    var flag = false;
                    Common.Data.Each(group, function (o) {
                        if (o.field == 'TOMasterCode')
                            flag = true;
                    })
                    if (!flag) {
                        group.splice(0, 0, { field: 'TOMasterCode', dir: 'desc' });
                        grid.dataSource.group(group);
                    } else {
                        grid.dataSource.read();
                    }
                }
            }
        }
        catch (e) { }
    }

    $scope.TimelineOrderV2ViewDate_Click = function ($event, grid) {
        $event.preventDefault();

        $scope.ShowTimeLineV2OrderDate = !$scope.ShowTimeLineV2OrderDate;
    }

    $scope.TimelineOrderV2ViewTimeLineWithFilter_Click = function ($event, grid) {
        $event.preventDefault();

        $scope.TimeLineViewOrderV2WithTimeLineFilter = !$scope.TimeLineViewOrderV2WithTimeLineFilter;
        $scope.NewTimeLineGroupSelected = [];
        grid.dataSource.read();
    }

    $scope.TimeLineOrderV2ViewDate_OK_Click = function ($event, grid) {
        $event.preventDefault();
        $scope.TimeLineViewV2OrderDate = true;
        $scope.ShowTimeLineV2OrderDate = false;
        $scope.NewTimeLineGroupSelected = [];
        grid.dataSource.read();
    }

    $scope.TimeLineOrderV2ViewDate_Cancel_Click = function ($event, grid) {
        $event.preventDefault();
        $scope.TimeLineViewV2OrderDate = false;
        $scope.ShowTimeLineV2OrderDate = false;
        $scope.TimeLineV2DateRequest = { fDate: null, tDate: null };
        $scope.NewTimeLineGroupSelected = [];
        grid.dataSource.read();
    }

    $scope.TimeLineFTLSplit_Click = function ($event, grid, win) {
        $event.preventDefault();

        $rootScope.IsLoading = true;
        $scope.ResetTrip(true);
        $scope.TripItem.ID = $($event.currentTarget).data('uid');
        Common.Services.Call($http, {
            url: Common.Services.url.OPS,
            method: _OPSAppointment_DIViewOnMapV4.URL.FTLSplit_Check,
            data: {
                toMasterID: $scope.TripItem.ID
            },
            success: function (res) {
                $rootScope.IsLoading = false;
                Common.Services.Error(res, function (res) {
                    if (res == true) {
                        $rootScope.Message({
                            Msg: 'Xác nhận tách đơn hàng?',
                            Type: Common.Message.Type.Confirm,
                            Ok: function () {
                                $rootScope.IsLoading = true;
                                Common.Services.Call($http, {
                                    url: Common.Services.url.OPS,
                                    method: _OPSAppointment_DIViewOnMapV4.URL.FTLSplit,
                                    data: { toMasterID: $scope.TripItem.ID, dataGop: [] },
                                    success: function (res) {
                                        Common.Services.Error(res, function (res) {
                                            Common.Services.Error(res, function (res) {
                                                $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                                                $scope.ResetTrip(true);
                                                $rootScope.IsLoading = false;
                                            });
                                        });
                                    }
                                });
                            }
                        });
                    } else {
                        $rootScope.IsLoading = true;
                        Common.Services.Call($http, {
                            url: Common.Services.url.OPS,
                            method: _OPSAppointment_DIViewOnMapV4.URL.GroupByTrip_List,
                            data: {
                                tripID: $scope.TripItem.ID
                            },
                            success: function (res) {
                                Common.Services.Error(res, function (res) {
                                    $rootScope.IsLoading = false;
                                    $scope.splitGop_GridOptions.dataSource.data(res);
                                    win.center().open();
                                    $timeout(function () {
                                        $scope.splitGop_GridOptions.dataSource.sync();
                                    }, 101)
                                })
                            }
                        })
                    }
                })
            }
        })
    }

    //#region Event Click
    $scope.NewTimeLineItem = {
        ID: -1,
        Code: "mới tạo",
        DriverName: "",
        DriverTel: "",
        StatusCode: 'Chưa chọn xe',
        StatusColor: $scope.Color.Error,
        VehicleID: -1,
        VehicleNo: "",
        VendorOfVehicleID: -1,
        VendorOfVehicleCode: "",
        Ton: 0,
        Status: 1,
        ETD: null, ETA: null,
        ListOPSGOP: [],
        ListOPSGOPName: [],
        LocationStartID: _OPSAppointment_DIViewOnMapV4.Data.Location.LocationStartID,
        LocationStartName: _OPSAppointment_DIViewOnMapV4.Data.Location.LocationStartName,
        LocationEndID: _OPSAppointment_DIViewOnMapV4.Data.Location.LocationEndID,
        LocationEndName: _OPSAppointment_DIViewOnMapV4.Data.Location.LocationEndName,
        LocationStartLat: _OPSAppointment_DIViewOnMapV4.Data.Location.LocationStartLat,
        LocationStartLng: _OPSAppointment_DIViewOnMapV4.Data.Location.LocationStartLng,
        LocationEndLat: _OPSAppointment_DIViewOnMapV4.Data.Location.LocationEndLat,
        LocationEndLng: _OPSAppointment_DIViewOnMapV4.Data.Location.LocationEndLng,
        ListLocation: [],
        HasChange: false
    }
    $scope.$watch('NewTimeLineItem.ETD', function () {
        $scope.CheckNewTimeLine("ETD");
    });
    $scope.$watch('NewTimeLineItem.ETA', function () {
        $scope.CheckNewTimeLine();
    });
    $scope.$watch('NewTimeLineItem.VehicleID', function () {
        $scope.CheckNewTimeLine();
    });

    $scope.CheckNewTimeLine = function (props) {
        Common.Log("CheckNewTimeLine")
        if ($scope.NewTimeLineDetail && $scope.NewTimeLineItem != null && $scope.NewTimeLineItem.Status == 1) {
            $scope.NewTimeLineItem.StatusCode = "";
            $scope.NewTimeLineItem.StatusColor = $scope.Color.None;
            if (props == "ETD" && $scope.NewTimeLineItem.ETD != null && $scope.NewTimeLineItem.ETA != null) {
                if ($scope.NewTimeLineItem.ETD >= $scope.NewTimeLineItem.ETA || $scope.NewTimeLineItem.ETD.addDays(1 / 48) > $scope.NewTimeLineItem.ETA) {
                    $scope.NewTimeLineItem.ETA = $scope.NewTimeLineItem.ETD.addDays(1 / 48);
                }
            }

            //Trường hợp xe vendor không cần kiểm tra.
            if ($scope.NewTimeLineItem.VendorOfVehicleID == -1 && $scope.NewTimeLineItem.VehicleID > 0 && $scope.NewTimeLineItem.ETD != null && $scope.NewTimeLineItem.ETA != null) {
                Common.Log('Trip checing...');

                if ($scope.NewTimeLineItem.ETD >= $scope.NewTimeLineItem.ETA) {
                    $scope.NewTimeLineItem.StatusCode = "Tg không hợp lệ.";
                    $scope.NewTimeLineItem.StatusColor = $scope.Color.Error;
                }
                else {
                    $scope.NewTimeLineItem.IsCheching = true;
                    Common.Services.Call($http, {
                        url: Common.Services.url.OPS,
                        method: _OPSAppointment_DIViewOnMapV4.URL.CheckVehicleAvailable,
                        data: {
                            vehicleID: $scope.NewTimeLineItem.VehicleID,
                            masterID: $scope.NewTimeLineItem.ID,
                            ETD: $scope.NewTimeLineItem.ETD,
                            ETA: $scope.NewTimeLineItem.ETA,
                            Ton: $scope.NewTimeLineItem.TotalTon || 0
                        },
                        success: function (res) {
                            Common.Services.Error(res, function (res) {
                                $scope.NewTimeLineItem.IsCheching = false;
                                if (res.IsOverWeight) {
                                    $scope.NewTimeLineItem.StatusCode = "Quá trọng tải";
                                    $scope.NewTimeLineItem.StatusColor = $scope.Color.Error;
                                } else {
                                    if (res.IsVehicleAvailable) {
                                        $scope.NewTimeLineItem.DriverTel = res.DriverTel;
                                        $scope.NewTimeLineItem.DriverName = res.DriverName;
                                        $scope.NewTimeLineItem.StatusCode = "Có thể cập nhật";
                                        $scope.NewTimeLineItem.StatusColor = $scope.Color.Success;
                                    } else {
                                        $scope.NewTimeLineItem.StatusCode = "Xe bận.";
                                        $scope.NewTimeLineItem.StatusColor = $scope.Color.Error;
                                    }
                                }
                            })
                        }
                    })
                }
            }

            try {
                if ($scope.NewTimeLineItem.VehicleID != $scope.NewTimeLineItemTemp.VehicleID || $scope.NewTimeLineItem.RomoocID != $scope.NewTimeLineItemTemp.RomoocID || $scope.NewTimeLineItem.ETA.getTime() != $scope.NewTimeLineItemTemp.ETA.getTime() || $scope.NewTimeLineItem.ETD.getTime() != $scope.NewTimeLineItemTemp.ETD.getTime()) {
                    $scope.NewTimeLineItem.HasChange = true;
                }
                else {
                    $scope.NewTimeLineItem.HasChange = false;
                }
            } catch (e) {
                $scope.NewTimeLineItem.HasChange = true;
            }

            try {
                if ($scope.NewTimeLineItem.VehicleID != $scope.NewTimeLineItem.VehicleOfferID || $scope.NewTimeLineItem.RomoocID != $scope.NewTimeLineItem.RomoocOfferID || $scope.NewTimeLineItem.ETA.getTime() != $scope.NewTimeLineItem.ETAOffer.getTime() || $scope.NewTimeLineItem.ETD.getTime() != $scope.NewTimeLineItem.ETDOffer.getTime()) {
                    $scope.NewTimeLineItem.HasTimeChange = true;
                    $scope.NewTimeLineItem.ETAOffer = $scope.NewTimeLineItem.ETA;
                    $scope.NewTimeLineItem.ETDOffer = $scope.NewTimeLineItem.ETD;
                    $scope.NewTimeLineItem.RomoocOfferID = $scope.NewTimeLineItem.RomoocID;
                    $scope.NewTimeLineItem.VehicleOfferID = $scope.NewTimeLineItem.VehicleID;
                }
            } catch (e) {
                $scope.NewTimeLineItem.HasTimeChange = true;
            }
        }
    }
    
    $scope.cboNewTimeLineVehicle_Options = {
        autoBind: true, valuePrimitive: true, ignoreCase: true,
        filter: 'contains', suggest: true, dataTextField: 'Regno', dataValueField: 'ID',
        dataSource: Common.DataSource.Local({
            data: [], model: { id: 'ID', fields: { ID: { type: 'number' }, Regno: { type: 'string' } } }
        })
    }

    $scope.atcNewTimeLineDriverNameOptions = {
        dataSource: Common.DataSource.Local({ data: [] }),
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true, placeholder: "Họ tên", dataTextField: "DriverName",
        change: function (e) {
            var cbo = this;
            $timeout(function () {
                var obj = cbo.dataItem(cbo.select());
                if (Common.HasValue(obj)) {
                    $scope.NewTimeLineItem.DriverTel = obj.DriverTel;
                }
            }, 10)
        }
    }

    $scope.LoadDataNewTimeLineInfo = function (isNew) {
        Common.Log("LoadDataNewTimeLineInfo")
        Common.Services.Call($http, {
            url: Common.Services.url.OPS,
            method: _OPSAppointment_DIViewOnMapV4.URL.VehicleVendor_List,
            data: {
                vendorID: $scope.NewTimeLineItem.VendorOfVehicleID, request: ''
            },
            success: function (res) {
                $timeout(function () {
                    $scope.cboNewTimeLineVehicle_Options.dataSource.data(res.Data);
                    if (isNew) {
                        if (Common.HasValue(res.Data[0]))
                            $scope.NewTimeLineItem.VehicleID = res.Data[0].ID;
                        else
                            $scope.NewTimeLineItem.VehicleID = null;
                    }
                }, 10)
            }
        });
        Common.Services.Call($http, {
            url: Common.Services.url.OPS,
            method: _OPSAppointment_DIViewOnMapV4.URL.DriverVendor_List,
            data: {
                vendorID: $scope.NewTimeLineItem.VendorOfVehicleID
            },
            success: function (res) {
                var data = [];
                $.each(res, function (i, v) {
                    data.push({ 'ID': v.ID, 'Text': v.LastName + ' ' + v.FirstName + ' (' + v.EmployeeCode + ')', 'DriverName': v.LastName + ' ' + v.FirstName, 'DriverTel': v.Cellphone });
                });
                $scope.atcNewTimeLineDriverNameOptions.dataSource.data(data);
            }
        });
    }

    $scope.NewTimeLineV2Event_Click = function ($event, uid, type, start, end, win, wingroup) {
        $event.preventDefault();

        if (type == 1) {
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.OPS,
                method: _OPSAppointment_DIViewOnMapV4.URL.TripByID,
                data: { masterID: uid },
                success: function (res) {
                    if (Common.HasValue(res) && res.ID > 0) {
                        $scope.NewTimeLineItem = {
                            ID: res.ID,
                            Code: res.Code,
                            VehicleNo: res.VehicleNo,
                            DriverName: res.DriverName,
                            DriverTel: res.DriverTel,
                            StatusCode: 'Có thể cập nhật',
                            StatusColor: $scope.Color.None,
                            VehicleID: res.VehicleID,
                            VendorOfVehicleID: res.VendorOfVehicleID,
                            VendorOfVehicleCode: res.VendorCode,
                            Ton: res.TotalTon,
                            Status: res.Status,
                            ETA: Common.Date.FromJson(res.ETA),
                            ETD: Common.Date.FromJson(res.ETD),
                            LocationStartID: res.LocationStartID,
                            LocationStartName: res.LocationStartName,
                            LocationEndID: res.LocationEndID,
                            LocationEndName: res.LocationEndName,
                            LocationStartLat: res.LocationStartLat,
                            LocationStartLng: res.LocationStartLng,
                            LocationEndLat: res.LocationEndLat,
                            LocationEndLng: res.LocationEndLng,
                            ListLocation: res.ListLocation,
                            HasChange: false
                        }
                        if ($scope.NewTimeLineItem.VendorOfVehicleID == null) {
                            $scope.NewTimeLineItem.VendorOfVehicleID = -1;
                        }
                        $scope.NewTimeLineItemTemp = $.extend(true, {}, $scope.NewTimeLineItem);

                        $scope.TimeLineInfoTabIndex = 1;
                        $scope.NewTimeLineDetail = true;
                        $scope.LoadDataNewTimeLineInfo(false);
                        $scope.new_timeline_trip_info_Grid.dataSource.read();
                        $timeout(function () {
                            $rootScope.IsLoading = false;
                            win.center().open();
                        }, 100)
                    }
                    else {
                        $rootScope.Message({ Msg: "Không tìm thấy chuyến! Xóa khỏi timeline!" });
                        $scope.LoadNewTimeLineV2Data(false);
                        $rootScope.IsLoading = false;
                    }
                }
            });
        } else if (type == -1) {
            wingroup.center().open();
            $scope.TimeLineGroupEvent = {
                VendorID: uid, fDate: new Date(start), tDate: new Date(end)
            }
            $scope.timeline_tomaster_Grid.dataSource.read();
        }
    }

    $scope.TimeLineGroupEvent = {
        VendorID: -1, fDate: new Date(), tDate: new Date()
    }
    $scope.timeline_tomaster_Grid_Options = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.OPS,
            method: _OPSAppointment_DIViewOnMapV4.URL.New_Schedule_TOMaster_List,
            pageSize: 0,
            group: [{ field: 'TOMasterCode' }],
            readparam: function () {
                return {
                    vendorID: $scope.TimeLineGroupEvent.VendorID,
                    fDate: $scope.TimeLineGroupEvent.fDate, tDate: $scope.TimeLineGroupEvent.tDate,
                    dataCus: $scope.NewTimeLineVehicleDataCustomer,
                    dataStt: $scope.NewTimeLineVehicleDataStatus
                }
            },
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    IsChoose: { type: 'bool', defaultValue: false },
                    ETD: { type: 'date' },
                    ETA: { type: 'date' },
                    TOETD: { type: 'date' },
                    TOETA: { type: 'date' },
                    TOLastUpdateTime: { type: 'date' },
                    TOCreatedDate: { type: 'date' },
                    Ton: { type: 'number' },
                    CBM: { type: 'number' },
                    Quantity: { type: 'number' }
                }
            }
        }),
        height: '99%', groupable: false, pageable: false, columnMenu: false, resizable: true, reorderable: false, sortable: true, filterable: { mode: 'row' }, auboBind: false,
        columns: [
            {
                field: 'TOMasterCode', width: '120px', title: 'Mã chuyến',
                filterable: { cell: { operator: 'contains', showOperators: false } },
                groupHeaderTemplate: function (e) {
                    try {
                        var obj = e.aggregates.parent().items[0];
                        if (Common.HasValue(obj)) {
                            var sumTon = 0, sumCBM = 0, sumQty = 0;
                            Common.Data.Each(e.aggregates.parent().items, function (o) {
                                sumTon += o.Ton; sumCBM += o.CBM; sumQty += o.Quantity;
                            })
                            var strVeh = obj.TOVehicleNo == "" || obj.TOVehicleNo == null ? "[Chưa nhập]" : obj.TOVehicleNo;
                            var strTel = obj.TODriverTel == "" || obj.TODriverTel == null ? "[Chưa nhập]" : obj.TODriverTel;
                            var strName = obj.TODriverName == "" || obj.TODriverName == null ? "[Chưa nhập]" : obj.TODriverName;
                            var strTitle = "Kế hoạch";
                            var sty = "width:20px;background:red;display:inline-block;height:16px;position:relative;top:3px;margin-right:7px;"
                            if (obj.TOStatus == 2) {
                                strTitle = "Đã duyệt";
                                sty = "width:20px;background:blue;display:inline-block;height:16px;position:relative;top:3px;margin-right:7px;"
                            }
                            return "<span style='" + sty + "' title='" + strTitle + "'></span>"
                                + "<span>" + obj.TOMasterCode + " - " + obj.TOVendorCode + " - " + strVeh + " - " + strName + " - " + strTel
                                + " - " + Common.Date.FromJsonDDMMHM(obj.TOETD) + " - " + Common.Date.FromJsonDDMMHM(obj.TOETA) + "</span>"
                                + " - Tấn: " + Math.round(sumTon * 100000) / 100000 + " - Khối: " + Math.round(sumCBM * 100000) / 100000 + " </span>"
                                + "<span style='font-size:12px;font-weight:lighter;'> - cập nhật cuối: " + Common.Date.FromJsonDDMMHM(obj.TOLastUpdateTime) + " bởi " + obj.TOLastUpdate + " </span>";
                        }
                    } catch (e) {
                        return "<span>" + e.value + "</span>";
                    }
                }
            },
            {
                field: 'IsWarning', width: 100, title: 'Cảnh báo', attributes: { 'style': 'text-align: center;' }, headerAttributes: { 'style': 'text-align: center;' },
                template: '<img class="img-warning" data-value="#=WarningMsg#" ng-show="dataItem.IsWarning" src="images/function/ico_warning_active.png"/>', filterable: false
            },
            { field: 'CustomerCode', width: 100, title: 'Mã KH', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'CustomerShortName', width: 150, title: 'Tên KH', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'OrderCode', width: '150px', title: 'Đơn hàng', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'TransportModeName', width: '80px', title: 'Loại v/c', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'DistributorCode', width: '120px', title: 'Nhà phân phối', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'GroupProductCode', width: '150px', title: 'Nhóm hàng', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'ProductCode', width: '100px', title: 'Hàng hóa', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'Ton', width: '80px', title: 'Tấn', filterable: { cell: { operator: 'gte', showOperators: false } } },
            { field: 'CBM', width: '80px', title: 'Khối', filterable: { cell: { operator: 'gte', showOperators: false } } },
            { field: 'Quantity', width: '80px', title: 'SL', filterable: { cell: { operator: 'gte', showOperators: false } } },
            {
                field: 'ETD', width: '120px', title: 'ETD', template: "#=ETD != null ? Common.Date.FromJsonDMYHM(ETD) : ''#",
                filterable: { cell: { template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); }, operator: 'equal', showOperators: false } }
            },
            {
                field: 'ETA', width: '120px', title: 'ETA', template: "#=ETA != null ? Common.Date.FromJsonDMYHM(ETA) : ''#",
                filterable: { cell: { template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); }, operator: 'equal', showOperators: false } }
            },
            { field: 'TempMin', width: '100px', title: 'N.Độ tối thiểu', filterable: { cell: { operator: 'gte', showOperators: false } } },
            { field: 'TempMax', width: '100px', title: 'N.Độ tối đa', filterable: { cell: { operator: 'gte', showOperators: false } } },
            { field: 'SOCode', width: '100px', title: 'Số SO', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'DNCode', width: '100px', title: 'Số DN', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'UserDefine1', width: '100px', title: 'Ghi chú 1', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'UserDefine2', width: '100px', title: 'Ghi chú 2', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'LocationFromCode', width: '100px', title: 'Điểm nhận', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'LocationFromAddress', width: '250px', title: 'Địa chỉ', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'LocationToCode', width: '100px', title: 'Điểm giao', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'LocationToAddress', width: '250px', title: 'Địa chỉ', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'WarningTime', width: 100, title: 'TG cảnh báo', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'WarningMsg', width: 100, title: 'ND cảnh báo', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'TOVendorCode', width: '150px', title: 'Đối tác', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'TOVehicleNo', width: '120px', title: 'Số xe', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'TODriverName', width: '100px', title: 'Tài xế', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'TODriverTel', width: '100px', title: 'SĐT', filterable: { cell: { operator: 'contains', showOperators: false } } },
            {
                field: 'TOCreatedDate', width: '160px', title: 'Ngày tạo chuyến', template: "#=TOCreatedDate != null ? Common.Date.FromJsonDMYHM(TOCreatedDate) : ''#",
                filterable: { cell: { template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); }, operator: 'equal', showOperators: false } }
            },
            { field: 'TOCreatedBy', width: '160px', title: 'Người tạo chuyến', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { title: ' ', filterable: false, menu: false, sortable: false }
        ]
    }
   
    $scope.NewTimeLineItemTemp = {};
    $scope.TimeLineInfoTabIndex = 1;
    $scope.TimeLineV2InfoItem = null;
    $scope.TimeLineV2InfoStyle = { 'display': 'none', 'top': 0, 'left': 0 }
    $scope.$watch("TimeLineInfoTabIndex", function () {
        if ($scope.TimeLineInfoTabIndex == 2) {
            $timeout(function () {
                $scope.new_timeline_trip_info_Grid.refresh();
            }, 10)
        }
    })
    
    $scope.NewTimeLineInfo_Update_Click = function ($event, win) {
        $event.preventDefault();

        if ($scope.NewTimeLineItem.ETA == null || $scope.NewTimeLineItem.ETD == null) {
            flag = false;
            $rootScope.Message({ Msg: "Điền đầy đủ ETD và ETA.", Type: Common.Message.Type.Alert });
        } else if ($scope.NewTimeLineItem.ETA <= $scope.NewTimeLineItem.ETD) {
            flag = false;
            $rootScope.Message({ Msg: "Sai ràng buộc ETD và ETA.", Type: Common.Message.Type.Alert });
        } else {
            $rootScope.Message({
                Msg: "Xác nhận lưu?",
                Type: Common.Message.Type.Confirm,
                Ok: function () {
                    $rootScope.IsLoading = true;
                    Common.Services.Call($http, {
                        url: Common.Services.url.OPS,
                        method: _OPSAppointment_DIViewOnMapV4.URL.DI2View_Master_Update_TimeLine,
                        data: { mID: $scope.NewTimeLineItem.ID, vehicleID: $scope.NewTimeLineItem.VehicleID, ETD: $scope.NewTimeLineItem.ETD, ETA: $scope.NewTimeLineItem.ETA, dataOffer: $scope.NewTimeLineItem.DataContainerOffer },
                        success: function (res) {
                            Common.Services.Error(res, function (res) {
                                $scope.ChangeData = true;
                                $rootScope.IsLoading = false;
                                $rootScope.Message({ Msg: 'Thành công!' });
                                $scope.LoadNewTimeLineV2Data(false);
                                $scope.NewTimeLineItem.HasChange = false;
                            }, function () {
                                $rootScope.IsLoading = false;
                            })
                        }
                    })
                }
            })
        }
    }

    $scope.new_timeline_trip_info_Grid_Options = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.OPS,
            method: _OPSAppointment_DIViewOnMapV4.URL.TOMaster_GroupProduct_List,
            pageSize: 0,
            readparam: function () {
                return { mID: $scope.NewTimeLineItem.ID }
            },
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    IsChoose: { type: 'bool', defaultValue: false },
                    ETD: { type: 'date' },
                    ETA: { type: 'date' },
                    Ton: { type: 'number' }
                }
            }
        }),
        height: '99%', groupable: false, pageable: false, columnMenu: false, resizable: true, reorderable: false, sortable: true, filterable: { mode: 'row' }, auboBind: false,
        columns: [
            { field: 'CustomerCode', width: '120px', title: 'Mã khách hàng', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'OrderCode', width: '150px', title: 'Đơn hàng', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'GroupProductCode', width: '125px', title: 'Nhóm hàng hóa', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'ProductCode', width: '100px', title: 'Hàng hóa', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'SOCode', width: '80px', title: 'Số SO', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'DNCode', width: '80px', title: 'Số DN', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'Ton', width: '80px', title: 'Tấn', filterable: { cell: { operator: 'gte', showOperators: false } } },
            { field: 'CBM', width: '80px', title: 'Khối', filterable: { cell: { operator: 'gte', showOperators: false } } },
            { field: 'Quantity', width: '80px', title: 'SL', filterable: { cell: { operator: 'gte', showOperators: false } } },
            {
                field: 'ETD', width: '120px', title: 'ETD', template: "#=ETD != null ? Common.Date.FromJsonDMYHM(ETD) : ''#",
                filterable: { cell: { template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); }, operator: 'equal', showOperators: false } }
            },
            {
                field: 'ETA', width: '120px', title: 'ETA', template: "#=ETA != null ? Common.Date.FromJsonDMYHM(ETA) : ''#",
                filterable: { cell: { template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); }, operator: 'equal', showOperators: false } }
            },
            { field: 'LocationFromCode', width: '100px', title: 'Điểm nhận', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'LocationFromAddress', width: '250px', title: 'Địa chỉ', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'LocationToCode', width: '100px', title: 'Điểm giao', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'LocationToAddress', width: '250px', title: 'Địa chỉ', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { title: ' ', filterable: false, menu: false, sortable: false }
        ]
    }

    $scope.NewVendorVehicle = { ID: -1, RegNo: '', MaxWeight: 0, MaxCapacity: 0 };
    $scope.TimeLine_Vehicle_Vendor_Create = function ($event, win) {
        $event.preventDefault();
        $scope.NewVendorVehicle = { ID: -1, RegNo: '', MaxWeight: 0, MaxCapacity: 0 };
        win.center().open();
    }
    
    $scope.VehicleVendor_New_OK_Click = function ($event, win) {
        $event.preventDefault();

        $rootScope.Message({
            Msg: "Xác nhận lưu?",
            Type: Common.Message.Type.Confirm,
            Ok: function () {
                $rootScope.IsLoading = true;
                Common.Services.Call($http, {
                    url: Common.Services.url.OPS,
                    method: _OPSAppointment_DIViewOnMapV4.URL.Vehicle_New,
                    data: {
                        vendorID: $scope.NewTimeLineItem.VendorOfVehicleID,
                        regNo: $scope.NewVendorVehicle.RegNo,
                        maxWeight: $scope.NewVendorVehicle.MaxWeight
                    },
                    success: function (res) {
                        Common.Services.Error(res, function (res) {
                            $rootScope.IsLoading = false;
                            $rootScope.Message({
                                Msg: 'Thành công!'
                            });
                            win.close();
                            $scope.LoadDataNewTimeLineInfo(false);
                        })
                    }
                })
            }
        })
    }

    $scope.NewTimeLineVehicle_Change_Click = function ($event, win) {
        $event.preventDefault();

        win.center().open();
        $scope.IsVehicleMapActived = true;
        $scope.VehicleMapRequestDate = $scope.NewTimeLineItem.ETD || new Date();
        $timeout(function () {
            $scope.NewTimeLineVehicleItem.ID = $scope.NewTimeLineItem.VehicleID;
            $scope.vehMap_Grid.dataSource.read();
            $rootScope.IsLoading = false;
        }, 100);
    }
    
    $scope.NewTimeLineV2_Update_OK_Click = function ($event, win) {
        $event.preventDefault();
        var flag = true;
        if ($scope.NewTimeLineItem.ETA == null || $scope.NewTimeLineItem.ETD == null) {
            flag = false;
            $rootScope.Message({ Msg: "Điền đầy đủ ETD và ETA.", Type: Common.Message.Type.Alert });
        }
        else if ($scope.NewTimeLineItem.ETA <= $scope.NewTimeLineItem.ETD) {
            flag = false;
            $rootScope.Message({ Msg: "Sai ràng buộc ETD và ETA.", Type: Common.Message.Type.Alert });
        }
        else if ($scope.NewTimeLineItem.VehicleID < 1) {
            flag = false;
            $rootScope.Message({ Msg: "Vui lòng chọn xe.", Type: Common.Message.Type.Alert });
        }

        if (flag) {
            $rootScope.Message({
                Msg: "Xác nhận lưu?",
                Type: Common.Message.Type.Confirm,
                Ok: function () {
                    $rootScope.IsLoading = true;
                    if ($scope.NewTimeLineItem.ID < 1) {
                        Common.Services.Call($http, {
                            url: Common.Services.url.OPS,
                            method: _OPSAppointment_DIViewOnMapV4.URL.Save,
                            data: {
                                item: $scope.NewTimeLineItem
                            },
                            success: function (res) {
                                Common.Services.Error(res, function (res) {
                                    $scope.ChangeData = true;
                                    $rootScope.IsLoading = false;
                                    $rootScope.Message({ Msg: 'Thành công!' });
                                    $scope.LoadNewTimeLineV2Data(true);
                                    $scope.NewTimeLineDetail = false;
                                    win.close();
                                }, function () {
                                    $rootScope.IsLoading = false;
                                })
                            }
                        })
                    } else {
                        //Thay đổi thông tin xe, tgian...
                        if ($scope.NewTimeLineItem.ListOPSGOP == null || $scope.NewTimeLineItem.ListOPSGOP == []) {
                            Common.Services.Call($http, {
                                url: Common.Services.url.OPS,
                                method: _OPSAppointment_DIViewOnMapV4.URL.Update,
                                data: { item: $scope.NewTimeLineItem },
                                success: function (res) {
                                    Common.Services.Error(res, function (res) {
                                        $scope.ChangeData = true;
                                        $rootScope.IsLoading = false;
                                        $rootScope.Message({ Msg: 'Thành công!' });
                                        $scope.LoadNewTimeLineV2Data(true);
                                        $scope.NewTimeLineDetail = false;
                                        win.close();
                                    }, function () {
                                        $rootScope.IsLoading = false;
                                    })
                                }
                            })
                        } else {
                            //+Bổ sung nhóm hàng vào chuyến.
                            Common.Services.Call($http, {
                                url: Common.Services.url.OPS,
                                method: _OPSAppointment_DIViewOnMapV4.URL.TimeLine_Master_Update_Group,
                                data: { mID: $scope.NewTimeLineItem.ID, dataGop: $scope.NewTimeLineItem.ListOPSGOP },
                                success: function (res) {
                                    Common.Services.Error(res, function (res) {
                                        Common.Services.Call($http, {
                                            url: Common.Services.url.OPS,
                                            method: _OPSAppointment_DIViewOnMapV4.URL.Update,
                                            data: { item: $scope.NewTimeLineItem },
                                            success: function (res) {
                                                Common.Services.Error(res, function (res) {
                                                    $scope.ChangeData = true;
                                                    $rootScope.IsLoading = false;
                                                    $rootScope.Message({ Msg: 'Thành công!' });
                                                    $scope.LoadNewTimeLineV2Data(true);
                                                    $scope.NewTimeLineDetail = false;
                                                    win.close();
                                                }, function () {
                                                    $rootScope.IsLoading = false;
                                                })
                                            }
                                        })
                                    }, function () {
                                        $rootScope.IsLoading = false;
                                    })
                                }
                            })
                        }
                    }
                }
            })
        }
    }
    
    //#endregion

    //#region Tooltip
    $('#timeline_v2_tooltip').detach().appendTo('body');

    $scope.ShowTimeLineV2Tooltip = function ($event, uid) {
        if ($scope.TimeLineEventDragDrop == false) {
            var off = $($event.currentTarget).offset();
            $scope.TimeLineV2InfoStyle = {
                'display': '', 'top': off.top + 32, 'left': off.left > 100 ? off.left : 100
            }

            Common.Data.Each($scope.new_timeline_v2.dataSource.data(), function (i) {
                if (i.id == uid) {
                    $scope.TimeLineV2InfoItem = i;
                }
            })
        }
    }

    $scope.HideTimeLineV2Tooltip = function ($event) {
        $scope.TimeLineV2InfoItem = null;
        $scope.TimeLineV2InfoStyle = {
            'display': 'none', 'top': 0, 'left': 0
        }
    }
    //#endregion

    //#region Filter
    $scope.NewTimeLineVehicleFilter_Click = function ($event) {
        $event.preventDefault();
        $scope.LoadNewTimeLineV2Data(false);
    }
    
    $scope.new_timeline_v2_customer_select_Grid_Options = {
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' }, Code: { type: 'string' },
                    CustomerName: { type: 'string' }, IsChoose: { type: 'bool', defaultValue: false }
                }
            }
        }),
        height: '99%', groupable: false, pageable: false, columnMenu: false, resizable: true, reorderable: true, sortable: true, auboBind: false,
        dataBound: function (e) {
            var grid = this;
            angular.forEach(grid.items(), function (tr) {
                var item = grid.dataItem(tr);
                if (Common.HasValue(item)) {
                    if ($scope.NewTimeLineVehicleDataCustomer != null && $scope.NewTimeLineVehicleDataCustomer.indexOf(item.ID) > -1) {
                        item.IsChoose = true;
                        $(tr).find('chkChoose').prop('checked', item.IsChoose);
                        $(tr).addClass('IsChoose');
                    } else {
                        item.IsChoose = false;
                        $(tr).find('chkChoose').prop('checked', item.IsChoose);
                        $(tr).removeClass('IsChoose');
                    }
                }
            });
        },
        columns: [
            {
                title: ' ', width: '35px',
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,new_timeline_v2_customer_select_Grid)" />',
                headerAttributes: { style: 'text-align: center;' }, attributes: { style: 'text-align: center;' },
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,new_timeline_v2_customer_select_Grid)" />',
                filterable: false, sortable: false, groupable: false
            },
            { field: 'Code', width: '150px', title: 'Mã khách hàng', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'CustomerName', title: 'Tên khách hàng', filterable: { cell: { operator: 'contains', showOperators: false } } }
        ]
    }

    $scope.new_timeline_v2_status_select_Grid_Options = {
        dataSource: Common.DataSource.Local({
            data: [{
                ID: 1, StatusName: 'Chuyến mới'
            }, {
                ID: 2, StatusName: 'Gửi giám sát'
            }, {
                ID: 3, StatusName: 'Hoàn thành'
            }, {
                ID: 4, StatusName: 'Khác'
            }],
            model: {
                id: 'ID',
                fields: { ID: { type: 'number' }, StatusName: { type: 'string' }, IsChoose: { type: 'bool', defaultValue: false } }
            }
        }),
        height: '99%', groupable: false, pageable: false, columnMenu: false, resizable: true, reorderable: true, sortable: true, auboBind: false,
        dataBound: function (e) {
            var grid = this;
            angular.forEach(grid.items(), function (tr) {
                var item = grid.dataItem(tr);
                if (Common.HasValue(item)) {
                    if ($scope.NewTimeLineVehicleDataStatus != null && $scope.NewTimeLineVehicleDataStatus.indexOf(item.ID) > -1) {
                        item.IsChoose = true;
                        $(tr).find('chkChoose').prop('checked', item.IsChoose);
                        $(tr).addClass('IsChoose');
                    } else {
                        item.IsChoose = false;
                        $(tr).find('chkChoose').prop('checked', item.IsChoose);
                        $(tr).removeClass('IsChoose');
                    }
                }
            });
        },
        columns: [
            {
                title: ' ', width: '35px',
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,new_timeline_v2_status_select_Grid)" />',
                headerAttributes: { style: 'text-align: center;' }, attributes: { style: 'text-align: center;' },
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,new_timeline_v2_status_select_Grid)" />',
                filterable: false, sortable: false, groupable: false
            },
            { field: 'StatusName', title: 'Trạng thái', filterable: { cell: { operator: 'contains', showOperators: false } } }
        ]
    }

    Common.Services.Call($http, {
        url: Common.Services.url.OPS,
        method: _OPSAppointment_DIViewOnMapV4.URL.Customer_List,
        success: function (res) {
            Common.Data.Each(res, function (o) { o.IsChoose = false });
            $scope.new_timeline_v2_customer_select_Grid.dataSource.data(res);
        }
    })
    
    $scope.TimelineOrderV2ViewCustomerSelect_Click = function ($event, win) {
        $event.preventDefault();

        if ($scope.NewTimeLineVehicleDataCustomer.length == 0) {
            win.center().open();
            $timeout(function () {
                $scope.new_timeline_v2_customer_select_Grid.refresh();
            }, 100)
        } else {
            $scope.NewTimeLineVehicleDataCustomer = [];
            $timeout(function () {
                $scope.new_timeline_v2_customer_select_Grid.refresh();
            }, 100)
            $scope.LoadNewTimeLineV2Data($scope.TimeLineViewOrderV2WithTimeLineFilter);
        }
    }

    $scope.NewTimeLineV2CustomerSelect_OK_Click = function ($event, grid, win) {
        $event.preventDefault();

        var data = [];
        Common.Data.Each(grid.dataSource.data(), function (o) {
            if (o.IsChoose) data.push(o.ID);
        })
        if (data.sort().toString() != $scope.NewTimeLineVehicleDataCustomer.sort().toString()) {
            $scope.NewTimeLineVehicleDataCustomer = data;
            $scope.LoadNewTimeLineV2Data($scope.TimeLineViewOrderV2WithTimeLineFilter);
        }
        win.close();
    }

    $scope.TimelineOrderV2ViewStatusSelect_Click = function ($event, win) {
        $event.preventDefault();

        if ($scope.NewTimeLineVehicleDataStatus.length == 0) {
            win.center().open();
            $timeout(function () {
                $scope.new_timeline_v2_status_select_Grid.refresh();
            }, 100)
        } else {
            $scope.NewTimeLineVehicleDataStatus = [];
            $timeout(function () {
                $scope.new_timeline_v2_status_select_Grid.refresh();
            }, 100)
            $scope.LoadNewTimeLineV2Data(false);
        }
    }

    $scope.NewTimeLineV2StatusSelect_OK_Click = function ($event, grid, win) {
        $event.preventDefault();

        var data = [];
        Common.Data.Each(grid.dataSource.data(), function (o) {
            if (o.IsChoose) data.push(o.ID);
        })
        if (data.sort().toString() != $scope.NewTimeLineVehicleDataStatus.sort().toString()) {
            $scope.NewTimeLineVehicleDataStatus = data;
            $scope.LoadNewTimeLineV2Data(false);
        }
        win.close();
    }
    //#endregion

    //#region TimeLineAction
    $scope.TimelineOrderV2ViewTripAction_Click = function ($event, scheduler) {
        $event.preventDefault();

        $scope.TimeLineViewTripAction = !$scope.TimeLineViewTripAction;
        if ($scope.TimeLineViewTripAction) {
            $scope.InitTimeLineViewActionDragDrop(scheduler);
        } else {
            $(scheduler.element).find('.k-scheduler-content').each(function (i, o) {
                $(o).removeClass('action-timeline');
            })
        }
    }
    $('.panel.panel_action.panel_action_tomon,.panel.panel_action.panel_action_toord').kendoDropTarget({
        drop: function (e) {
            $scope.OnTimeLineViewActionDrop(e);
        },
        dragenter: function (e) {
            $(e.dropTarget).find('.panel-backgroud').css("opacity", 1);
        },
        dragleave: function (e) {
            $(e.dropTarget).find('.panel-backgroud').css("opacity", 0.5);
        },
        group: "timelineAction1"
    });
    $('.panel.panel_action.panel_action_toops').kendoDropTarget({
        drop: function (e) {
            $scope.OnTimeLineViewActionDrop(e);
        },
        dragenter: function (e) {
            $(e.dropTarget).find('.panel-backgroud').css("opacity", 1);
        },
        dragleave: function (e) {
            $(e.dropTarget).find('.panel-backgroud').css("opacity", 0.5);
        },
        group: "timelineAction2"
    });

    $scope.InitTimeLineViewActionDragDrop = function (scheduler) {
        $(scheduler.element).find('.k-scheduler-content').each(function (i, o) {
            $(o).addClass('action-timeline');
        })
        $(scheduler.element).kendoDraggable({
            filter: ".action-timeline .k-event.approved,.action-timeline .k-event.tenderable", group: "timelineAction1", cursorOffset: { top: 0, left: 0 },
            drag: function (e) {
                $scope.TimeLineEventDragDrop = true;
            },
            dragend: function (e) {
                $scope.TimeLineEventDragDrop = false;
            },
            hint: function (e) {
                return e.clone();
            }
        });
        $(scheduler.element).kendoDraggable({
            filter: ".action-timeline .k-event.tendered", group: "timelineAction2", cursorOffset: { top: 0, left: 0 },
            drag: function (e) {
                $scope.TimeLineEventDragDrop = true;
            },
            dragend: function (e) {
                $scope.TimeLineEventDragDrop = false;
            },
            hint: function (e) {
                return e.clone();
            }
        });
    }

    $scope.OnTimeLineViewActionDrop = function (e) {
        var scheduler = $scope.new_timeline_v2, element = e.draggable.hint,
            item = scheduler.dataSource.getByUid(element.data('uid')), action = e.dropTarget.data('action');
        if (Common.HasValue(item)) {
            switch (action) {
                case 1:
                    $rootScope.Message({
                        Msg: 'Xác nhận duyệt chuyến?',
                        Type: Common.Message.Type.Confirm,
                        Ok: function (o) {
                            $rootScope.IsLoading = true;
                            Common.Services.Call($http, {
                                url: Common.Services.url.OPS,
                                method: _OPSAppointment_DIViewOnMapV4.URL.ToMon,
                                data: { data: [item.id] },
                                success: function (res) {
                                    Common.Services.Error(res, function (res) {
                                        $rootScope.IsLoading = false;
                                        item.StatusOfEvent = 2;
                                        $rootScope.Message({ Msg: 'Thành công!' });
                                        scheduler.refresh();
                                    }, function () {
                                        $rootScope.IsLoading = false;
                                    })
                                }
                            })
                        }
                    })
                    break;
                case 2:
                    $rootScope.Message({
                        Msg: 'Xác nhận hủy duyệt chuyến?',
                        Type: Common.Message.Type.Confirm,
                        Ok: function (o) {
                            $rootScope.IsLoading = true;
                            Common.Services.Call($http, {
                                url: Common.Services.url.OPS,
                                method: _OPSAppointment_DIViewOnMapV4.URL.ToOPS,
                                data: { data: [item.id] },
                                success: function (res) {
                                    Common.Services.Error(res, function (res) {
                                        $rootScope.IsLoading = false;
                                        item.StatusOfEvent = 11;
                                        $rootScope.Message({ Msg: 'Thành công!' });
                                        scheduler.refresh();
                                    }, function () {
                                        $rootScope.IsLoading = false;
                                    })
                                }
                            })
                        }
                    })
                    break;
                case 3:
                    $rootScope.Message({
                        Msg: 'Xác nhận xóa chuyến?',
                        Type: Common.Message.Type.Confirm,
                        Ok: function (o) {
                            $rootScope.IsLoading = true;
                            Common.Services.Call($http, {
                                url: Common.Services.url.OPS,
                                method: _OPSAppointment_DIViewOnMapV4.URL.Delete,
                                data: { data: [item.id] },
                                success: function (res) {
                                    Common.Services.Error(res, function (res) {
                                        $rootScope.IsLoading = false;
                                        scheduler.dataSource.remove(item);
                                        $rootScope.Message({ Msg: 'Thành công!' });
                                        scheduler.refresh();
                                    }, function () {
                                        $rootScope.IsLoading = false;
                                    })
                                }
                            })
                        }
                    })
                    break;
            }
        }
    }
    //#endregion

    //#region Vehicle Map
    $scope.ChangeVehicleType = 1;
    $scope.IsVehicleMapActived = false;
    $scope.VehicleMapRequestDate = new Date();

    $scope.vehMap_Grid_Options = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.OPS,
            method: _OPSAppointment_DIViewOnMapV4.URL.Vehicle_List,
            readparam: function () {
                return {
                    requestDate: $scope.VehicleMapRequestDate
                }
            },
            pageSize: 0,
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    MaxWeight: { type: 'number' }
                }
            }
        }),
        height: '99%', groupable: false, pageable: false, sortable: true, columnMenu: false, resizable: true,
        selectable: 'row', filterable: { mode: 'row', visible: false }, reorderable: false, auboBind: false,
        dataBound: function () {
            var grid = this;
            if ($scope.IsVehicleMapActived) {
                openMapV2.ClearVector("VectorMarkerVEH");
                angular.forEach(grid.items(), function (tr) {
                    if (!$scope.NewTimeLineDetail) $(tr).addClass('unselectable');
                });
            }
        },
        columns: [
            { field: 'Regno', width: 120, title: 'Số xe', template: '<span>#=Regno# </span>' + '<a class="k-button select" ng-click="VehicleOnMap_Select($event,dataItem,vehicle_map_win)"><span>LC</span></a>', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'MaxWeight', width: 70, title: 'Trọng tải', filterable: { cell: { operator: 'gte', showOperators: false } } },
            { field: 'GroupOfVehicleName', width: 100, title: 'Loại xe', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'StatusOfVehicleName', title: 'T/Trạng', filterable: false, sortable: false }
        ]
    };

    $scope.VehicleOnMap_Select = function ($event, item, win) {
        $event.preventDefault();

        if ($scope.IsShowNewTimeLineV2 && $scope.NewTimeLineDetail) {
            if ($scope.NewTimeLineItem.ID > 0) {
                $rootScope.Message({
                    Msg: "Xác nhận lưu?",
                    Type: Common.Message.Type.Confirm,
                    Ok: function () {
                        $rootScope.IsLoading = true;
                        Common.Services.Call($http, {
                            url: Common.Services.url.OPS,
                            method: _OPSAppointment_DIViewOnMapV4.URL.DI2View_Master_Update_TimeLine,
                            data: { mID: $scope.NewTimeLineItem.ID, vehicleID: item.ID, ETD: $scope.NewTimeLineItem.ETD, ETA: $scope.NewTimeLineItem.ETA },
                            success: function (res) {
                                Common.Services.Error(res, function (res) {
                                    $scope.ChangeData = true;
                                    $rootScope.IsLoading = false;
                                    $rootScope.Message({ Msg: 'Thành công!' });
                                    $scope.LoadNewTimeLineV2Data(false);
                                    $scope.NewTimeLineItem.HasChange = false;
                                    $scope.NewTimeLineItemTemp.VehicleID = item.ID;
                                    $scope.NewTimeLineItemTemp.VehicleNo = item.Regno;
                                    $scope.NewTimeLineItem.VehicleID = item.ID;
                                    $scope.NewTimeLineItem.VehicleNo = item.Regno;
                                    win.close();
                                }, function () {
                                    $rootScope.IsLoading = false;
                                })
                            }
                        })
                    }
                })
            }
            else {
                $scope.NewTimeLineItem.HasChange = true;
                $scope.NewTimeLineItem.VehicleID = item.ID;
                $scope.NewTimeLineItem.VehicleNo = item.Regno;
                win.close();
            }
        } 
    }
    //#endregion

    //#endregion
    
    $scope.ToDateString = function (v) {
        return Common.Date.FromJsonDMYHM(v);
    }

    $scope.ToDateStringDMHM = function (v) {
        return Common.Date.FromJsonDDMM(v) + " " + Common.Date.FromJsonHM(v);
    }

    //#region Action
    $scope.Close_Click = function ($event, win, code) {
        $event.preventDefault();

        try {
            switch (code) {
                case 'ORD':
                    break;
                case 'TO':
                    $scope.TripActived = false;
                    $scope.IsShowTrip = false;
                    $scope.IsShowTimeLineTrip = false;
                    break;
                case 'NewTO':
                    $scope.IsShowNewTrip = false;
                    break;
                case 'VEHICLE':
                    break;
                case 'MAP':
                    openMapV2.Active($scope.indexMap);
                    break;
                case 'VehicleMAP':
                    openMapV2.Active($scope.indexMap);
                    break;
                case 'TimeLine':
                    $scope.TripActived = false;
                    $scope.IsShowTimeLine = false;
                    $scope.IsShowTimeLineTrip = false;
                    break;
                case 'TOTimeLine':
                    $scope.TripActived = false;
                    $scope.IsShowTimeLineTrip = false;
                    break;
                case 'TODetail':
                    $scope.TripDetail = false;
                    break;
                case 'NewTODetail':
                    $scope.NewTripDetail = false;
                    break;
                case 'TimeLineTODetail':
                    $scope.TimeLineTripDetail = false;
                    break;
                case 'TimeLineVehicleSelect':
                    $scope.NewTimeLineVehicleData = $.extend(true, [], $scope.NewTimeLineVehicleDataTemp);
                    break;
                case 'NewTimeLineV2':
                    $scope.IsShowNewTimeLineV2 = false;
                    break;
                case 'NewTimeLineDetail':
                    $scope.NewTimeLineDetail = false;
                    break;
            }
            win.close();
        } catch (e) {
        }
    }

    $scope.On_Close = function (code) {
        switch (code) {
            case 'ORD':
                break;
            case 'TO':
                $scope.TripActived = false;
                $scope.IsShowTrip = false;
                $scope.IsShowTimeLineTrip = false;
                break;
            case 'NewTO':
                $scope.IsShowNewTrip = false;
                break;
            case 'VEHICLE':
                break;
            case 'MAP':
                openMapV2.Active($scope.indexMap);
                break;
            case 'VehicleMAP':
                openMapV2.Active($scope.indexMap);
                break;
            case 'TimeLine':
                $scope.TripActived = false;
                $scope.IsShowTimeLine = false;
                $scope.IsShowTimeLineTrip = false;
                break;
            case 'TOTimeLine':
                $scope.TripActived = false;
                $scope.IsShowTimeLineTrip = false;
                break;
            case 'TODetail':
                $scope.TripDetail = false;
                break;
            case 'NewTODetail':
                $scope.NewTripDetail = false;
                break;
            case 'TimeLineTODetail':
                $scope.TimeLineTripDetail = false;
                break;
            case 'TimeLineVehicleSelect':
                $scope.NewTimeLineVehicleData = $.extend(true, [], $scope.NewTimeLineVehicleDataTemp);
                break;
            case 'NewTimeLineV2':
                $scope.IsShowNewTimeLineV2 = false;
                break;
            case 'NewTimeLineDetail':
                $scope.NewTimeLineDetail = false;
                break;
        }
    }

    $scope.ShowSetting = function ($event, grid) {
        $event.preventDefault();
        $rootScope.ShowSetting({
            ListView: views.OPSAppointmentDI,
            event: $event, grid: grid,
            current: $state.current,
            customview: true, customcache: "view.OPSAppointment.DIViewOnMap",
            callback: function (e) {
                if (e) {
                    Common.Cookie.Set("view.OPSAppointment.DIViewOnMap", $state.current.name);
                } else {
                    Common.Cookie.Set("view.OPSAppointment.DIViewOnMap", 'main.OPSAppointment.DIViewOnMapV2');
                }
            }
        });
    };

    $scope.HideSetting = function ($event) {
        $event.preventDefault();
        $rootScope.HideSetting();
    }

    $scope.ViewA_Click = function ($event) {
        $event.preventDefault();

        angular.element('#2view').addClass('fullscreen');
        angular.element('.mainform .mainmenu').addClass('fullscreen');
        angular.element('#2view').resize();
        $scope.IsFullScreen = true;
    }

    $scope.ViewB_Click = function ($event) {
        $event.preventDefault();

        $scope.IsFullScreen = false;
        angular.element('.mainform .mainmenu').removeClass('fullscreen');
        angular.element('#2view').removeClass('fullscreen');
        angular.element('#2view').resize();
        $scope.timelineV2Splitter.resize();
    };
    //#endregion

    $rootScope.IsLoading = false;
    $scope.InitComplete = true;
}]);
