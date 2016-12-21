﻿/// <reference path="~/Scripts/jquery-1.9.1.intellisense.js" />
/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />

//#region URL

var _COImportPacket_Detail_2View = {
    URL: {
        Get: 'OPS_CO_Import_Packet_Get',
        Reset: 'OPS_CO_Import_Packet_Reset',
        ToOPS: 'OPS_CO_Import_Packet_ToOPS',
        ToMON: 'OPS_CO_Import_Packet_ToMON',
        Save: 'OPS_CO_Import_Packet_2View_Save',
        TOMaster_List: 'OPS_CO_Import_Packet_TOMaster_List',
        Container_List: 'OPS_CO_Import_Packet_Container_List',
        Data: 'OPS_CO_Import_Packet_Data', //Danh sách nhà xe + xe
    },
    Data: {
        DataSort: [],
        DataVendor: [],
        DataVehicle: [],
        DataTOMaster: []
    }
}

//#endregion

angular.module('myapp').controller('OPSAppointment_COImportPacket_Detail_2ViewCtrl', ['$rootScope', '$scope', '$http', '$location', '$state', '$timeout', function ($rootScope, $scope, $http, $location, $state, $timeout) {
    Common.Log('OPSAppointment_COImportPacket_Detail_2ViewCtrl');
    $rootScope.IsLoading = false;

    $scope.IsExpand = true;
    $scope.DataVendor = [];
    $scope.DataVendorDriver = [];    
    $scope.DataVendorRomooc = [];
    $scope.DataVendorVehicle = [];
    $scope.Auth = $rootScope.GetAuth();
    $scope.PacketID = parseInt($state.params.ID);
    $scope.IsChange = false;
    $scope.IsSuggestRomooc = false; //Lấy romooc mặc định theo xe.

    try {
        Common.Services.Call($http, {
            url: Common.Services.url.OPS,
            method: _COImportPacket_Detail_2View.URL.Get,
            data: {
                pID: $scope.PacketID
            },
            success: function (res) {
                Common.Services.Error(res, function (res) {
                    if (res.ID != $scope.PacketID) {
                        $rootScope.Message({
                            Msg: "Không tìm thấy dữ liệu. Quay về trang trước."
                        });
                        $timeout(function () {
                            $state.go("main.OPSAppointment.COImportPacket");
                        }, 1000)
                    } else {
                        $scope.IsClose = res.IsCreated;
                    }
                })
            }
        })
    }
    catch (e) { }

    //MainView
    $scope.gridOptions = {
        dataSource: Common.DataSource.Local({
            model: {
                id: 'ID', fields: {
                    ID: { type: 'number' },
                    ETD: { type: 'date' },
                    ETA: { type: 'date' }
                }
            },
            group: [{ field: 'CreateSortOrder' }]
        }),
        filterable: { mode: 'row' }, height: '99%', groupable: false, pageable: false, columnMenu: false,
        sortable: false, resizable: true, reorderable: true, sortable: { mode: 'multiple' },
        dataBound: function () {
            Common.Log("GridDataBound");
            $scope.IsChange = false;

            if (Common.HasValue(this) && Common.HasValue(this.tbody)) {
                this.element.find('.my-group').each(function () {
                    var tr = $(this).closest('tr'),
                        idx = parseInt($(this).data('sortorder'));

                    if (idx > 0) {
                        var item = _COImportPacket_Detail_2View.Data.DataSort[idx];
                        if (Common.HasValue(item)) {
                            var romID = item.RomoocID != null ? item.RomoocID : '';
                            var vehID = item.VehicleID != null ? item.VehicleID : '';
                            if ($scope.IsClose) {
                                $(this).html('Chuyến ' + idx + ' - ' + item.VendorCode + ' - ' + item.VehicleNo + ' - ' + item.RomoocNo + ' - ' + item.DriverName + ' - ' + item.DriverTel + ' - ' + Common.Date.FromJsonDMYHM(item.ETD) + ' - ' + Common.Date.FromJsonDMYHM(item.ETA));
                            } else {
                                $(this).html('<a href="/" class="btn-group" data-sortorder="' + idx + '">Chuyến ' + idx + ' - ' + item.VendorCode + ' - ' + item.VehicleNo + ' - ' + item.RomoocNo + ' - ' + item.DriverName + ' - ' + item.DriverTel + ' - ' + Common.Date.FromJsonDMYHM(item.ETD) + ' - ' + Common.Date.FromJsonDMYHM(item.ETA) + '</a>' +
                                '<span style="display:none" class="txt-split" style="position:relative;top:2px;">Chuyến ' + idx + ' </span>' +
                                '<input style="display:none;width:200px" class="cboVendor cus-combobox" value="' + item.VendorID + '" /><span style="display:none" class="txt-split"> - </span>' +
                                '<input style="display:none;width:120px" class="cboVehicle cus-combobox" value="' + vehID + '" /><span style="display:none" class="txt-split"> - </span>' +
                                '<input style="display:none;width:120px" class="cboRomooc cus-combobox" value="' + romID + '" /><span style="display:none" class="txt-split"> - </span>' +
                                '<input style="display:none;width:120px" class="cboDriverName" value="' + item.DriverName + '" /><span style="display:none" class="txt-split"> - </span>' +
                                '<input style="display:none;width:120px" class="cboDriverTel  k-textbox" placeholder="SĐT" value="' + item.DriverTel + '" /><span style="display:none" class="txt-split"> - </span>' +
                                '<input style="display:none;width:160px" class="dtpETD"  value="' + Common.Date.FromJsonDMYHM(item.ETD) + '"/><span style="display:none" class="txt-split"> - </span>' +
                                '<input style="display:none;width:160px" class="dtpETA"  value="' + Common.Date.FromJsonDMYHM(item.ETA) + '"/>');
                            }
                        }
                    }
                });

                this.element.find('.my-group .btn-group').click(function (e) {
                    e.preventDefault();
                    $scope.Group_Click(this);
                });
            }
        },
        columns: [
            {
                field: '', width: '45px',
                headerTemplate: '<a class="k-button" href="/" ng-click="Expand_Click($event,grid)"><i class="fa fa-minus"></i></a>',
                sortable: false, filterable: false, menu: false
            },
            {
                field: 'CreateSortOrder', width: '45px', title: 'Số chuyến',
                groupHeaderTemplate: '<span class="my-group" data-sortorder="#=value#"></span>',
                sortable: false, filterable: false, menu: false, hidden: true
            },
            {
                field: 'CustomerCode', width: '150px', title: 'Mã KH',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'OrderCode', width: '150px', title: 'Mã ĐH',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'PackingName', width: '100px', title: 'Loại container',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'StatusOfCOContainerName', width: '100px', title: 'Chặng',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'ContainerNo', width: '125px', title: 'Số container',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'SealNo1', width: '100px', title: 'Số Seal 1',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'SealNo2', width: '100px', title: 'Số Seal 2',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'PartnerCode', width: '100px', title: 'Hãng tàu',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'ETD', width: '130px', title: 'ETD',
                template: "#=ETD==null?' ':Common.Date.FromJsonDMYHM(ETD)#",
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
                template: "#=ETA==null?' ':Common.Date.FromJsonDMYHM(ETA)#",
                filterable: {
                    cell: {
                        template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); },
                        operator: 'equal', showOperators: false
                    }
                },
            },
            {
                field: 'LocationFromCode', width: '150px', title: 'Mã điểm nhận hàng',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'LocationFromName', width: '150px', title: 'Điểm nhận hàng',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'LocationToCode', width: '150px', title: 'Mã điểm giao hàng',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'LocationToName', width: '150px', title: 'Điểm giao hàng',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            { title: ' ', filterable: false, menu: false, sortable: false }
        ],
    };

    Common.Services.Call($http, {
        url: Common.Services.url.OPS,
        method: _COImportPacket_Detail_2View.URL.Data,
        data: { pID: $scope.PacketID },
        success: function (res) {
            if (Common.HasValue(res)) {
                $scope.LoadDataVehicle(res);
            }
        }
    });

    $scope.LoadDataVehicle = function (data) {
        Common.Log('LoadDataVehicle');

        $scope.DataVendor = data.ListVendor;

        $.each(data.ListVendor, function (idx, vendor) {
            $scope.DataVendorDriver[vendor.ID] = [];
            $scope.DataVendorVehicle[vendor.ID] = [];
            $scope.DataVendorRomooc[vendor.ID] = [];
        });

        $.each(data.ListVehicle, function (idx, vehicle) {
            if (!Common.HasValue($scope.DataVendorVehicle[vehicle.VendorID]))
                $scope.DataVendorVehicle[vehicle.VendorID] = [];
            $scope.DataVendorVehicle[vehicle.VendorID].push(vehicle);
        });
        $.each(data.ListRomooc, function (idx, vehicle) {
            if (!Common.HasValue($scope.DataVendorRomooc[vehicle.VendorID]))
                $scope.DataVendorRomooc[vehicle.VendorID] = [];
            $scope.DataVendorRomooc[vehicle.VendorID].push(vehicle);
        });
        $.each(data.ListDriver, function (idx, driver) {
            if (!Common.HasValue($scope.DataVendorDriver[driver.VendorID]))
                $scope.DataVendorDriver[driver.VendorID] = [];
            $scope.DataVendorDriver[driver.VendorID].push(driver);
        });
    }

    $scope.LoadData = function () {
        Common.Log('LoadData');
        
        $rootScope.IsLoading = true;
        _COImportPacket_Detail_2View.Data.DataSort = [];
        Common.Services.Call($http, {
            url: Common.Services.url.OPS,
            method: _COImportPacket_Detail_2View.URL.TOMaster_List,
            data: { request: '', pID: $scope.PacketID },
            success: function (res) {
                Common.Services.Error(res, function (res) {
                    _COImportPacket_Detail_2View.Data.DataSort[0] = { 'CreateSortOrder': 0, 'VendorID': -1, 'VehicleNo': '', 'DriverName': '', 'DriverTel': '' };
                    var datafix = [];
                    var index = 1;
                    $.each(res.Data, function (i, v) {
                        _COImportPacket_Detail_2View.Data.DataSort[index] = v;
                        v.CreateSortOrder = index;
                        datafix[v.CreateSortOrder] = index;
                        index++;
                    });

                    Common.Services.Call($http, {
                        url: Common.Services.url.OPS,
                        method: _COImportPacket_Detail_2View.URL.Container_List,
                        data: { request: '', pID: $scope.PacketID },
                        success: function (res) {
                            Common.Services.Error(res, function (res) {
                                _COImportPacket_Detail_2View.Data.DataTOMaster = [];
                                $.each(res.Data, function (i, v) {
                                    if (v.CreateSortOrder > 0) {
                                        v.CreateSortOrder = datafix[v.CreateSortOrder];
                                        _COImportPacket_Detail_2View.Data.DataTOMaster.push(v);
                                    }
                                });
                                try {
                                    $scope.grid.dataSource.data(_COImportPacket_Detail_2View.Data.DataTOMaster);
                                    $rootScope.IsLoading = false;
                                }
                                catch (e) {
                                    $rootScope.IsLoading = false;
                                }
                            });
                        }
                    });
                });
            }
        });
    };

    $scope.LoadData();

    $scope.Group_Click = function (ele) {
        Common.Log('Group_Click');

        var idx = parseInt($(ele).data('sortorder')),
            group = $(ele).closest('.my-group'),
            tr = $(ele).closest('tr'),
            dtpETD = $(tr).find('input.dtpETD'),
            dtpETA = $(tr).find('input.dtpETA'),
            cboVen = $(tr).find('input.cboVendor'),
            cboRom = $(tr).find('input.cboRomooc'),
            cboVeh = $(tr).find('input.cboVehicle'),
            cboDriTel = $(tr).find('input.cboDriverTel'),
            cboDriName = $(tr).find('input.cboDriverName'),
            dataVehicle = [], dataDriver = [];

        $scope.IsChange = true;
        $(ele).hide();
        $(tr).find('.txt-split,input,.cus-button').show();

        dtpETD.kendoDateTimePicker({
            format: Common.Date.Format.DMYHM,
            timeFormat: Common.Date.Format.HM,
            change: function (e) {
                var val = this.value();
                if (Common.HasValue(val)) {
                    _COImportPacket_Detail_2View.Data.DataSort[idx].ETD = val;
                }
            }
        });
        $rootScope.FocusKDateTimePicker(dtpETD);

        dtpETA.kendoDateTimePicker({
            format: Common.Date.Format.DMYHM,
            timeFormat: Common.Date.Format.HM,
            change: function (e) {
                var val = this.value();
                if (Common.HasValue(val)) {
                    _COImportPacket_Detail_2View.Data.DataSort[idx].ETA = val;
                }
            }
        });
        $rootScope.FocusKDateTimePicker(dtpETA);

        var item = _COImportPacket_Detail_2View.Data.DataSort[idx];
        dataVehicle = $scope.DataVendorVehicle[item.VendorID];
        if (!Common.HasValue(dataVehicle))
            dataVehicle = [];
        dataRomooc = $scope.DataVendorRomooc[item.VendorID];
        if (!Common.HasValue(dataRomooc))
            dataRomooc = [];

        cboVen.kendoComboBox({
            index: 0, autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true,
            dataTextField: 'VendorCode', dataValueField: 'ID',
            dataSource: Common.DataSource.Local({
                data: $scope.DataVendor,
                model: { id: 'ID', fields: { ID: { type: 'number' }, VendorCode: { type: 'string' } } },
            }),
            change: function (e) {
                var cbo = this;

                var group = $(cbo.element).closest('span.my-group'),
                    cboVeh = $(group).find("input.cboVehicle[data-role='combobox']"),
                    cboRom = $(group).find("input.cboRomooc[data-role='combobox']"),
                    cboDri = $(group).find("input.cboDriverName[data-role='autocomplete']"),
                    txtDri = $(group).find('input.cboDriverTel'),
                    idx = group.data('sortorder'),
                    item = _COImportPacket_Detail_2View.Data.DataSort[idx],
                    obj = cbo.dataItem(cbo.select()),
                    venID = obj != null ? obj.ID : 0,
                    venCode = obj != null ? obj.VendorCode: "",
                    venName = obj != null ? obj.VendorName: "";                    
                if (venID != item.VendorID) {
                    item.VendorID = venID;
                    item.VendorName = venName;
                    item.venCode = venCode;
                    var cboVehicle = $(cboVeh).data('kendoComboBox');
                    var cboRomooc = $(cboRom).data('kendoComboBox');
                    var cboDriverName = $(cboDri).data('kendoAutoComplete');
                    $timeout(function () {
                        try {
                            item.RomoocNo = '';
                            item.RomoocID = '';
                            item.VehicleNo = '';
                            item.VehicleID = '';
                            item.DriverName = '';
                            item.DriverTel = '';
                            cboVehicle.dataSource.data($scope.DataVendorVehicle[venID]);
                            cboRomooc.dataSource.data($scope.DataVendorRomooc[venID]);
                            cboVehicle.text(item.VehicleNo);
                            cboRomooc.text(item.RomoocNo);
                            txtDri.val('');
                            cboDriverName.value('');
                            cboDriverName.dataSource.data($scope.DataVendorDriver[venID]);
                        } catch (e) { }
                    }, 1)
                }
            }
        });
        $rootScope.FocusKCombobox(cboVen);

        cboVeh.kendoComboBox({
            index: 0, autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true,
            dataTextField: 'VehicleNo', dataValueField: 'ID',
            dataSource: Common.DataSource.Local({
                data: dataVehicle,
                model: { id: 'ID', fields: { ID: { type: 'number' }, VehicleNo: { type: 'string' } } },
            }),
            change: function () {
                var cbo = this;

                var obj = cbo.dataItem(cbo.select());
                if (Common.HasValue(obj)) {
                    var group = $(cbo.element).closest('span.my-group'),
                        cboRom = $(group).find("input.cboRomooc[data-role='combobox']"),
                        cboDri = $(group).find("input.cboDriverName[data-role='autocomplete']"),
                        txtDri = $(group).find('input.cboDriverTel'),
                        idx = group.data('sortorder'),
                        item = _COImportPacket_Detail_2View.Data.DataSort[idx];
                    item.VehicleNo = obj.VehicleNo;
                    item.VehicleID = obj.ID;
                    item.DriverName = obj.DriverName;
                    item.DriverTel = obj.DriverTel;
                    var cboDriverName = $(cboDri).data('kendoAutoComplete');
                    var cboRomooc = $(cboRom).data('kendoComboBox');
                    $timeout(function () {
                        try {
                            if ($scope.IsSuggestRomooc && (item.VendorID == null || item.VendorID < 1 || item.VendorName == 'Xe nhà') && obj.CurrentRomoocID > 0) {
                                cboRomooc.value(obj.CurrentRomoocID);
                                item.RomoocID = obj.CurrentRomoocID;
                                item.RomoocNo = obj.CurrentRomoocNo;
                            }
                            txtDri.val(item.DriverTel);
                            cboDriverName.value(item.DriverName);
                        } catch (e) { }
                    }, 1)
                }
            }
        });
        cboVeh.text(item.VehicleNo);
        $rootScope.FocusKCombobox(cboVeh);
        
        cboRom.kendoComboBox({
            index: 0, autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true,
            dataTextField: 'VehicleNo', dataValueField: 'ID',
            dataSource: Common.DataSource.Local({
                data: dataRomooc,
                model: { id: 'ID', fields: { ID: { type: 'number' }, VehicleNo: { type: 'string' } } },
            }),
            change: function () {
                var cbo = this;

                var obj = cbo.dataItem(cbo.select());
                if (Common.HasValue(obj)) {
                    var group = $(cbo.element).closest('span.my-group'),
                        idx = group.data('sortorder'),
                        item = _COImportPacket_Detail_2View.Data.DataSort[idx];
                    item.RomoocNo = obj.VehicleNo;
                    item.RomoocID = obj.ID;
                }
            }
        });
        cboRom.text(item.RomoocNo);
        $rootScope.FocusKCombobox(cboRom);

        dataDriver = $scope.DataVendorDriver[item.VendorID];
        if (!Common.HasValue(dataDriver))
            dataDriver = [];
        cboDriName.kendoAutoComplete({
            dataSource: Common.DataSource.Local({ data: dataDriver }),
            autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true, placeholder: "Họ tên", dataTextField: "DriverName",
            change: function (e) {
                var cbo = this;
                $timeout(function () {
                    var obj = cbo.dataItem(cbo.select());
                    if (Common.HasValue(obj)) {
                        var group = $(cbo.element).closest('span.my-group'),
                            idx = group.data('sortorder'),
                            cboDriTel = $(group).find('input.cboDriverTel'),
                            item = _COImportPacket_Detail_2View.Data.DataSort[idx];
                        item.DriverName = obj.DriverName;
                        item.DriverTel = obj.DriverTel;
                        cboDriTel.val(item.DriverTel);
                    }
                }, 10)
            }
        })

        cboDriTel.on('change', function (e) {
            var group = $(this).closest('span.my-group'),
                idx = group.data('sortorder'),
                item = _COImportPacket_Detail_2View.Data.DataSort[idx];
            item.DriverTel = obj.DriverTel;
        })
    };

    $scope.Expand_Click = function ($event, grid) {
        $event.preventDefault();

        $scope.IsExpand = !$scope.IsExpand;
        $timeout(function () {
            if ($scope.IsExpand) {
                angular.forEach(grid.element.find('.k-grouping-row'), function (o) {
                    grid.expandGroup(o);
                })
                $($event.currentTarget).find("i").removeClass("fa-plus");
                $($event.currentTarget).find("i").addClass("fa-minus");
            } else {
                angular.forEach(grid.element.find('.k-grouping-row'), function (o) {
                    grid.collapseGroup(o);
                })
                $($event.currentTarget).find("i").removeClass("fa-minus");
                $($event.currentTarget).find("i").addClass("fa-plus");
            }
        }, 10)
    };

    $scope.Save_Click = function ($event) {
        $event.preventDefault();

        $rootScope.Message({
            Type: Common.Message.Type.Confirm,
            Msg: "Xác nhận lưu thay đổi?",
            Ok: function () {
                $rootScope.IsLoading = true;

                var data = [];
                Common.Data.Each(_COImportPacket_Detail_2View.Data.DataSort, function (o) {
                    if (o.CreateSortOrder > 0)
                        data.push(o);
                })

                Common.Services.Call($http, {
                    url: Common.Services.url.OPS,
                    method: _COImportPacket_Detail_2View.URL.Save,
                    data: {
                        data: data
                    },
                    success: function (res) {
                        Common.Services.Error(res, function (res) {
                            $rootScope.IsLoading = false;
                            $rootScope.Message({
                                Msg: "Thành công."
                            });
                            $scope.LoadData();
                        })
                    }
                })
            }
        })
    }

    $scope.Cancel_Click = function ($event) {
        $event.preventDefault();

        $rootScope.Message({
            Type: Common.Message.Type.Confirm,
            Msg: "Xác nhận hủy thay đổi?",
            Ok: function () {
                $scope.LoadData();
            }
        })
    }

    $scope.ToOPS_Click = function ($event) {
        $event.preventDefault();

        if ($scope.IsChange) {
            $rootScope.Message({
                Type: Common.Message.Type.Confirm,
                Msg: "Xác nhận Lưu và gửi điều phối?",
                Ok: function () {
                    $rootScope.IsLoading = true;
                    var data = [];
                    Common.Data.Each(_COImportPacket_Detail_2View.Data.DataSort, function (o) {
                        if (o.CreateSortOrder > 0)
                            data.push(o);
                    })
                    Common.Services.Call($http, {
                        url: Common.Services.url.OPS,
                        method: _COImportPacket_Detail_2View.URL.Save,
                        data: {
                            data: data
                        },
                        success: function (res) {
                            Common.Services.Error(res, function (res) {
                                $rootScope.IsLoading = false;
                                Common.Services.Call($http, {
                                    url: Common.Services.url.OPS,
                                    method: _COImportPacket_Detail_2View.URL.ToOPS,
                                    data: {
                                        pID: $scope.PacketID
                                    },
                                    success: function (res) {
                                        Common.Services.Error(res, function (res) {
                                            $rootScope.IsLoading = false;
                                            $rootScope.Message({
                                                Msg: "Thành công."
                                            });
                                            $scope.IsClose = true;
                                            $rootScope.Message({
                                                Type: Common.Message.Type.Confirm,
                                                Msg: "Xóa dữ liệu tạm trong gói import?",
                                                Ok: function () {
                                                    $rootScope.IsLoading = true;
                                                    Common.Services.Call($http, {
                                                        url: Common.Services.url.OPS,
                                                        method: _COImportPacket_Detail_2View.URL.Reset,
                                                        data: { pID: $scope.PacketID },
                                                        success: function (res) {
                                                            Common.Services.Error(res, function (res) {
                                                                $rootScope.IsLoading = false;
                                                                $rootScope.Message({ Msg: "Thành công." });
                                                                $state.go("main.OPSAppointment.COImportPacket_Detail_Input", { ID: $scope.PacketID });
                                                            })
                                                        }
                                                    })
                                                },
                                                Close: function () {
                                                    $scope.LoadData();
                                                }
                                            });
                                        })
                                    }
                                })
                            }, function () {
                                $rootScope.IsLoading = false;                       
                                $rootScope.Message({
                                    Type: Common.Message.Type.Alert, Msg: "Không thể gửi điều phối. Dữ liệu lỗi!"
                                });
                            })
                        }
                    })
                }
            })
        } else {
            $rootScope.Message({
                Type: Common.Message.Type.Confirm,
                Msg: "Xác nhận gửi điều phối?",
                Ok: function () {
                    $rootScope.IsLoading = true;
                    Common.Services.Call($http, {
                        url: Common.Services.url.OPS,
                        method: _COImportPacket_Detail_2View.URL.ToOPS,
                        data: {
                            pID: $scope.PacketID
                        },
                        success: function (res) {
                            Common.Services.Error(res, function (res) {
                                $rootScope.IsLoading = false;
                                $rootScope.Message({
                                    Msg: "Thành công."
                                });
                                $scope.IsClose = true;
                                $rootScope.Message({
                                    Type: Common.Message.Type.Confirm,
                                    Msg: "Xóa dữ liệu tạm trong gói import?",
                                    Ok: function () {
                                        $rootScope.IsLoading = true;
                                        Common.Services.Call($http, {
                                            url: Common.Services.url.OPS,
                                            method: _COImportPacket_Detail_2View.URL.Reset,
                                            data: { pID: $scope.PacketID },
                                            success: function (res) {
                                                Common.Services.Error(res, function (res) {
                                                    $rootScope.IsLoading = false;
                                                    $rootScope.Message({ Msg: "Thành công." });
                                                    $state.go("main.OPSAppointment.COImportPacket_Detail_Input", { ID: $scope.PacketID });
                                                })
                                            }
                                        })
                                    },
                                    Close: function () {
                                        $scope.LoadData();
                                    }
                                });
                            })
                        }
                    })
                }
            })
        }
    }

    $scope.ToMon_Click = function ($event) {
        $event.preventDefault();
        
        if ($scope.IsChange) {
            $rootScope.Message({
                Type: Common.Message.Type.Confirm,
                Msg: "Xác nhận Lưu và gửi giám sát?",
                Ok: function () {
                    $rootScope.IsLoading = true;
                    var data = [];
                    Common.Data.Each(_COImportPacket_Detail_2View.Data.DataSort, function (o) {
                        if (o.CreateSortOrder > 0)
                            data.push(o);
                    })
                    Common.Services.Call($http, {
                        url: Common.Services.url.OPS,
                        method: _COImportPacket_Detail_2View.URL.Save,
                        data: {
                            data: data
                        },
                        success: function (res) {
                            Common.Services.Error(res, function (res) {
                                $rootScope.IsLoading = false;
                                Common.Services.Call($http, {
                                    url: Common.Services.url.OPS,
                                    method: _COImportPacket_Detail_2View.URL.ToMON,
                                    data: {
                                        pID: $scope.PacketID
                                    },
                                    success: function (res) {
                                        Common.Services.Error(res, function (res) {
                                            $rootScope.IsLoading = false;
                                            $rootScope.Message({
                                                Msg: "Thành công."
                                            });
                                            $scope.IsClose = true;
                                            $rootScope.Message({
                                                Type: Common.Message.Type.Confirm,
                                                Msg: "Xóa dữ liệu tạm trong gói import?",
                                                Ok: function () {
                                                    $rootScope.IsLoading = true;
                                                    Common.Services.Call($http, {
                                                        url: Common.Services.url.OPS,
                                                        method: _COImportPacket_Detail_2View.URL.Reset,
                                                        data: { pID: $scope.PacketID },
                                                        success: function (res) {
                                                            Common.Services.Error(res, function (res) {
                                                                $rootScope.IsLoading = false;
                                                                $rootScope.Message({ Msg: "Thành công." });
                                                                $state.go("main.OPSAppointment.COImportPacket_Detail_Input", { ID: $scope.PacketID });
                                                            })
                                                        }
                                                    })
                                                },
                                                Close: function () {
                                                    $scope.LoadData();
                                                }
                                            });
                                        })
                                    }
                                })
                            }, function () {
                                $rootScope.IsLoading = false;
                                $rootScope.Message({
                                    Type: Common.Message.Type.Alert, Msg: "Không thể gửi điều phối. Dữ liệu lỗi!"
                                });
                            })
                        }
                    })
                }
            })
        } else {
            $rootScope.Message({
                Type: Common.Message.Type.Confirm,
                Msg: "Xác nhận gửi giám sát?",
                Ok: function () {
                    $rootScope.IsLoading = true;

                    Common.Services.Call($http, {
                        url: Common.Services.url.OPS,
                        method: _COImportPacket_Detail_2View.URL.ToMON,
                        data: {
                            pID: $scope.PacketID
                        },
                        success: function (res) {
                            Common.Services.Error(res, function (res) {
                                $rootScope.IsLoading = false;
                                $rootScope.Message({
                                    Msg: "Thành công."
                                });
                                $scope.IsClose = true;
                                $rootScope.Message({
                                    Type: Common.Message.Type.Confirm,
                                    Msg: "Xóa dữ liệu tạm trong gói import?",
                                    Ok: function () {
                                        $rootScope.IsLoading = true;
                                        Common.Services.Call($http, {
                                            url: Common.Services.url.OPS,
                                            method: _COImportPacket_Detail_2View.URL.Reset,
                                            data: { pID: $scope.PacketID },
                                            success: function (res) {
                                                Common.Services.Error(res, function (res) {
                                                    $rootScope.IsLoading = false;
                                                    $rootScope.Message({ Msg: "Thành công." });
                                                    $state.go("main.OPSAppointment.COImportPacket_Detail_Input", { ID: $scope.PacketID });
                                                })
                                            }
                                        })
                                    },
                                    Close: function () {
                                        $scope.LoadData();
                                    }
                                });
                            })
                        }
                    })
                }
            })
        }
    }

    $scope.Reset_Click = function ($event, grid) {
        $event.preventDefault();

        $rootScope.Message({
            Msg: "Xác nhận xóa?",
            Type: Common.Message.Type.Confirm,
            Ok: function () {
                $rootScope.IsLoading = true;
                Common.Services.Call($http, {
                    url: Common.Services.url.OPS,
                    method: _COImportPacket_Detail_2View.URL.Reset,
                    data: { pID: $scope.PacketID },
                    success: function (res) {
                        Common.Services.Error(res, function () {
                            $rootScope.Message({
                                Msg: 'Đã xóa.',
                                NotifyType: Common.Message.NotifyType.SUCCESS
                            });
                            $scope.LoadData();
                            $rootScope.IsLoading = false;
                        })
                    }
                })
            }
        })
    }
}]);