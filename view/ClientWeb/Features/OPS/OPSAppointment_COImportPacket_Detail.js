﻿/// <reference path="~/Scripts/jquery-1.9.1.intellisense.js" />
/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />

//#region URL

var _COImportPacket_Detail = {
    URL: {
        Get: 'OPS_CO_Import_Packet_Get',
        ToOPS: 'OPS_CO_Import_Packet_ToOPS',
        Reset: 'OPS_CO_Import_Packet_Reset',
        Check: 'OPS_CO_Import_Packet_Check',
        Import: 'OPS_CO_Import_Packet_Import',
        Template_List: 'OPS_CO_Import_Packet_Setting_List',
        TOMaster_List: 'OPS_CO_Import_Packet_TOMaster_List',
        Vehicle_Update: 'OPS_CO_Import_Packet_Vehicle_Update',
        Container_List: 'OPS_CO_Import_Packet_Container_ByMaster_List',
        CheckLocation: 'OPS_CO_Import_Packet_CheckLocation'
    },
    Data: { FromFile: null }
}

//#endregion

angular.module('myapp').controller('OPSAppointment_COImportPacket_DetailCtrl', ['$rootScope', '$scope', '$http', '$location', '$state', '$timeout', function ($rootScope, $scope, $http, $location, $state, $timeout) {
    Common.Log('OPSAppointment_COImportPacket_DetailCtrl');
    $rootScope.IsLoading = false;

    $scope.IsExcelChecked = false;
    $scope.HasTemplate = false;
    $scope.IsClose = true;
    $scope.Template = {
        ID: -1, Name: ''
    }
    $scope.PacketID = parseInt($state.params.ID);
    $scope.MasterID = -1;
    $scope.CheckLocation = "";

    try {
        Common.Services.Call($http, {
            url: Common.Services.url.OPS,
            method: _COImportPacket_Detail.URL.Get,
            data: {
                pID: $scope.PacketID
            },
            success: function (res) {
                Common.Services.Error(res, function (res) {
                    if (res.ID != $scope.PacketID) {
                        $rootScope.Message({
                            Msg: "Không tìm thấy Packet. Quay về trang trước."
                        });
                        $timeout(function () {
                            $state.go("main.OPSAppointment.COImportPacket");
                        }, 1000)
                    } else {
                        $scope.IsClose = res.IsCreated;
                        $scope.HasTemplate = res.CUSSettingID > 0;
                        $scope.Template.ID = res.CUSSettingID;
                        $scope.Template.Name = res.CUSSettingName;
                    }
                })
            }
        })

        Common.Services.Call($http, {
            url: Common.Services.url.OPS,
            method: _COImportPacket_Detail.URL.CheckLocation,
            success: function (res) {
                $timeout(function () {
                    $scope.CheckLocation = res;
                }, 10);
            }
        });
    }
    catch (e) { }

    //MainView
    $scope.excel_gridOptions = {
        dataSource: Common.DataSource.Local({
            data: [],
            pageSize: 0,
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number', editable: false },
                    ExcelSuccess: { type: 'boolean' },
                    ETD: { type: 'date' },
                    ETA: { type: 'date' }
                }
            }
        }),
        height: '100%', groupable: false, pageable: false, sortable: true, columnMenu: false, filterable: false, resizable: true,
        columns: [
            {
                field: 'ExcelSuccess', title: 'TC', width: '40px',
                template: '<input class="chkChoose" type="checkbox" #= ExcelSuccess ? "checked=checked" : "" # disabled="disabled" />',
                attributes: { style: 'text-align: center;' }, headerAttributes: { style: 'text-align: center;' }
            },
            {
                field: 'ExcelError', width: '350px', title: 'Thông báo', template: "<a href='/' ng-click='ShowError($event,dataItem)'>#=ExcelError!=null?ExcelError:''#</a>"
            },
            {
                field: 'SortOrder', width: '100px', title: 'Chuyến', template: "<a href='/' ng-click='Detail_Click($event,dataItem,con_win)'>Chuyến #=SortOrder#</a>"
            },
            {
                field: 'VendorCode', width: '110px', title: 'Đối tác'
            },
            {
                field: 'VehicleNo', width: '110px', title: 'Số xe'
            },
            {
                field: 'RomoocNo', width: '110px', title: 'Số romooc'
            },
            {
                field: 'ETD', width: '110px', title: 'ETD',
                template: "#=ETD==null?' ':Common.Date.FromJsonDMYHM(ETD)#",
            },
            {
                field: 'ETA', width: '110px', title: 'ETA',
                template: "#=ETA==null?' ':Common.Date.FromJsonDMYHM(ETA)#",
            },
            {
                field: 'Note', width: '250px', title: 'Ghi chú'
            },
            { title: '', filterable: false, sortable: false }
        ]
    }

    $scope.ShowError = function ($event, item) {
        $event.preventDefault();

        $rootScope.Message({            
            Msg: item.ExcelError,
            Type: Common.Message.Type.Alert
        });
    }

    $scope.excel_template_gridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.OPS,
            method: _COImportPacket_Detail.URL.Template_List,
            pageSize: 0,
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' }
                }
            }
        }),
        height: '100%', groupable: false, pageable: false, sortable: true, columnMenu: false,
        filterable: { mode: 'row' }, resizable: true, selectable: true,
        dataBound: function () {
            var grid = this;
            $(grid.items()).on('dblclick', function (e) {
                var tr = e.currentTarget;
                var item = grid.dataItem(tr);
                if (Common.HasValue(item)) {
                    $timeout(function () {
                        $scope.HasTemplate = true;
                        $scope.Template.ID = item.ID;
                        $scope.Template.Name = item.Name;                        
                        $scope.template_win.close();
                    }, 1)
                }
            })
        },
        columns: [
            {
                field: 'Name', title: 'Tên', sortable: true,
                filterable: { cell: { operator: 'contains', showOperators: false } }
            }
        ]
    }
    
    $scope.con_gridOptions = {
        dataSource: Common.DataSource.Local({
            data: [],
            pageSize: 0,
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    ETD: { type: 'date' },
                    ETA: { type: 'date' },
                    DateGetEmpty: { type: 'date' },
                    DateReturnEmpty: { type: 'date' }
                }
            }
        }),
        height: '100%', groupable: false, pageable: false, sortable: true, columnMenu: false, resizable: true, selectable: true,
        columns: [
            {
                field: 'CustomerCode', width: '150px', title: 'Khách hàng',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'OrderCode', width: '150px', title: 'Mã ĐH',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'ServiceOfOrder', width: '100px', title: 'Hình thức v/c',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'TypeOfTransportMode', width: '100px', title: 'Loại hình v/c',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'TypeOfContainerName', width: '125px', title: 'Loại container',
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
                field: 'CarrierCode', width: '100px', title: 'Hãng tàu',
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
                field: 'DateGetEmpty', width: '130px', title: 'Ngày lấy rỗng',
                template: "#=DateGetEmpty==null?' ':Common.Date.FromJsonDMYHM(DateGetEmpty)#",
                filterable: {
                    cell: {
                        template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); },
                        operator: 'equal', showOperators: false
                    }
                },
            },
            {
                field: 'DateReturnEmpty', width: '130px', title: 'Ngày trả rỗng',
                template: "#=DateReturnEmpty==null?' ':Common.Date.FromJsonDMYHM(DateReturnEmpty)#",
                filterable: {
                    cell: {
                        template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); },
                        operator: 'equal', showOperators: false
                    }
                },
            },
            {
                field: 'LocationFromName', width: '150px', title: 'Điểm nhận hàng',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'LocationToName', width: '150px', title: 'Điểm giao hàng',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'LocationDepotCode', width: '150px', title: 'Điểm lấy rỗng',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'LocationReturnCode', width: '150px', title: 'Điểm trả rỗng',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            { title: ' ', filterable: false, menu: false, sortable: false }
        ]
    }
    
    $scope.con_byid_gridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.OPS,
            method: _COImportPacket_Detail.URL.Container_List,
            readparam: function () { return { masterID: $scope.MasterID } },
            pageSize: 0,
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    ETD: { type: 'date' },
                    ETA: { type: 'date' },
                    DateGetEmpty: { type: 'date' },
                    DateReturnEmpty: { type: 'date' }
                }
            }
        }),
        height: '100%', groupable: false, pageable: false, sortable: true, columnMenu: false, resizable: true, selectable: true,
        columns: [
            {
                field: 'CustomerCode', width: '150px', title: 'Khách hàng',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'OrderCode', width: '150px', title: 'Mã ĐH',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'ServiceOfOrder', width: '100px', title: 'Hình thức v/c',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'TypeOfTransportMode', width: '100px', title: 'Loại hình v/c',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'PackingName', width: '125px', title: 'Loại container',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'StatusOfCOContainerName', width: '125px', title: 'Chặng',
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
                field: 'CarrierCode', width: '100px', title: 'Hãng tàu',
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
                field: 'DateGetEmpty', width: '130px', title: 'Ngày lấy rỗng',
                template: "#=DateGetEmpty==null?' ':Common.Date.FromJsonDMYHM(DateGetEmpty)#",
                filterable: {
                    cell: {
                        template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); },
                        operator: 'equal', showOperators: false
                    }
                },
            },
            {
                field: 'DateReturnEmpty', width: '130px', title: 'Ngày trả rỗng',
                template: "#=DateReturnEmpty==null?' ':Common.Date.FromJsonDMYHM(DateReturnEmpty)#",
                filterable: {
                    cell: {
                        template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); },
                        operator: 'equal', showOperators: false
                    }
                },
            },
            {
                field: 'LocationFromName', width: '150px', title: 'Điểm nhận hàng',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'LocationToName', width: '150px', title: 'Điểm giao hàng',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'LocationDepotCode', width: '150px', title: 'Điểm lấy rỗng',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'LocationReturnCode', width: '150px', title: 'Điểm trả rỗng',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            { title: ' ', filterable: false, menu: false, sortable: false }
        ]
    }

    $scope.vehicle_gridOptions = {
        dataSource: Common.DataSource.Local({
            pageSize: 0,
            model: { id: 'ID', fields: { ID: { type: 'number' } } }
        }),
        height: '100%', groupable: false, pageable: false, sortable: true, columnMenu: false, resizable: true, selectable: true,
        columns: [
            {
                field: 'VehicleNo', width: '200px', title: 'Số xe',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'VendorCode', title: 'Mã nhà xe',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'TypeOfVehicleName', title: 'Loại', width: '150px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            }
        ]
    }

    $scope.LoadData = function () {
        Common.Services.Call($http, {
            url: Common.Services.url.OPS,
            method: _COImportPacket_Detail.URL.TOMaster_List,
            data: { request: '', pID: $scope.PacketID },
            success: function (res) {
                Common.Services.Error(res, function (res) {
                    Common.Data.Each(res.Data, function (o) {
                        o.ExcelSuccess = true;
                        o.ETD = Common.Date.FromJson(o.ETD);
                        o.ETA = Common.Date.FromJson(o.ETA);
                        o.ExcelError = o.IsWarning == true ? "Quá trọng tải." : ""
                    })
                    $scope.excel_grid.dataSource.data(res.Data);
                })
            }
        });
    }    

    $scope.Template_Click = function ($event, win) {
        if (Common.HasValue($event))
            $event.preventDefault();

        $scope.excel_template_gridOptions.dataSource.read();
        if (Common.HasValue(win) && win.wrapper.length > 0)
            win.center().open();
    }
    
    $scope.Excel_Template_Accept_Click = function ($event, win, grid) {
        $event.preventDefault();
        
        $scope.HasTemplate = false;
        $scope.Template = {
            ID: -1, Name: ''
        }

        var item = grid.dataItem(grid.select());
        if (Common.HasValue(item)) {
            $scope.HasTemplate = true;

            $scope.Template.ID = item.ID;
            $scope.Template.Name = item.Name;
        }

        win.close();
    }

    $scope.UpFile_Click = function ($event, file) {
        $event.preventDefault();

        if ($scope.CheckLocation != "") {
            $rootScope.Message({ Msg: $scope.CheckLocation, NotifyType: Common.Message.NotifyType.ERROR });
        } else {
            $timeout(function () {
                angular.element(file.element[0]).trigger('click');
            }, 1);
        }
    }
    
    $scope.excel_fileOptions = {
        async: {
            saveUrl: '/Handler/File.ashx',
            autoUpload: true
        },
        multiple: false,
        showFileList: false,
        upload: function (e) {
            var xhr = e.XMLHttpRequest;
            xhr.addEventListener('readystatechange', function (e) {
                if (xhr.readyState == 1)
                    xhr.setRequestHeader('auth', Common.Auth.HeaderKey);
            });
            e.data = { 'folderPath': Common.FolderUpload.Import }
        },
        success: function (e) {
            $scope.File = e.response;

            $rootScope.IsLoading = true;
            $scope.Excel_Check();
        }
    };
    
    $scope.Excel_Check = function () {
        $rootScope.IsLoading = true;

        $scope.IsExcelChecked = false;
        $scope.excel_grid.dataSource.data([]);

        $rootScope.IsLoading = true;
        Common.Services.Call($http, {
            url: Common.Services.url.OPS,
            method: _COImportPacket_Detail.URL.Check,
            data: {
                pID: $scope.PacketID,
                file: $scope.File.FilePath,
                templateID: $scope.Template.ID
            },
            success: function (res) {
                $rootScope.IsLoading = false;
                Common.Services.Error(res, function (res) {
                    if (res.length == 0) {
                        $rootScope.Message({ Msg: "Không có thông tin.", NotifyType: Common.Message.NotifyType.ERROR });
                    } else {
                        $rootScope.Message({ Msg: "Đã kiểm tra." });
                        _COImportPacket_Detail.Data.FromFile = res;
                        $scope.excel_grid.dataSource.data(res);
                        $scope.IsExcelChecked = true;

                        var dataCheck = {};
                        var dataVehicleNew = [];
                        Common.Data.Each(res, function (o) {
                            if (o.IsNewVendorVehicle && !dataCheck[o.VehicleNo] && o.VendorID > 0) {
                                dataCheck[o.VehicleNo] = true;
                                dataVehicleNew.push({
                                    VehicleNo: o.VehicleNo,
                                    VendorID: o.VendorID,
                                    VendorCode: o.VendorCode,
                                    TypeOfVehicleID: 1,
                                    TypeOfVehicleName: "Đầu kéo",
                                    MaxWeight: 0, ID: -1
                                });
                            }
                            if (o.IsNewVendorRomooc && !dataCheck[o.RomoocNo] && o.VendorID > 0) {
                                dataCheck[o.VehicleNo] = true;
                                dataVehicleNew.push({
                                    VehicleNo: o.RomoocNo,
                                    VendorID: o.VendorID,
                                    VendorCode: o.VendorCode,
                                    TypeOfVehicleID: 2,
                                    TypeOfVehicleName: "Romooc",
                                    MaxWeight: 0, ID: -1
                                });
                            }
                        })
                        if (dataVehicleNew.length > 0) {
                            $rootScope.Message({
                                Type: Common.Message.Type.Confirm,
                                Msg: "Có dữ liệu đầu kéo/romooc mới. Bạn có muốn lưu?",
                                Ok: function () {
                                    $scope.vehicle_win.center().open();
                                    $timeout(function () {
                                        $scope.vehicle_gridOptions.dataSource.data(dataVehicleNew);
                                    }, 100);
                                }
                            })
                        }
                    }
                })
            }
        });
    }
        
    $scope.Import_Click = function ($event) {
        $event.preventDefault();

        var data = [];
        var idx = 1;
        Common.Data.Each(_COImportPacket_Detail.Data.FromFile, function (o) {
            if (o.ExcelSuccess) {
                o.SortOrder = idx++;
                data.push(o);
            }                
        })

        if (data.length > 0) {
            $rootScope.Message({
                Type: Common.Message.Type.Confirm,
                Msg: "Xác nhận lưu?",
                Ok: function () {
                    $rootScope.IsLoading = true;
                    Common.Services.Call($http, {
                        url: Common.Services.url.OPS,
                        method: _COImportPacket_Detail.URL.Import,
                        data: {
                            templateID: $scope.Template.ID,
                            pID: $scope.PacketID,
                            data: data
                        },
                        success: function (res) {
                            $rootScope.IsLoading = false;
                            Common.Services.Error(res, function () {
                                $rootScope.Message({ Msg: "Thành công!" });
                                $scope.IsExcelChecked = false;
                                $rootScope.Message({
                                    Type: Common.Message.Type.Confirm,
                                    Msg: "Gửi điều phối các chuyến đã tạo?",
                                    Ok: function () {
                                        $rootScope.IsLoading = true;
                                        Common.Services.Call($http, {
                                            url: Common.Services.url.OPS,
                                            method: _COImportPacket_Detail.URL.ToOPS,
                                            data: { pID: $scope.PacketID },
                                            success: function (res) {
                                                Common.Services.Error(res, function (res) {
                                                    $rootScope.IsLoading = false;
                                                    $scope.IsClose = true;
                                                    $rootScope.Message({ Msg: "Thành công." });
                                                    $rootScope.Message({
                                                        Type: Common.Message.Type.Confirm,
                                                        Msg: "Xóa dữ liệu tạm trong gói import?",
                                                        Ok: function () {
                                                            $rootScope.IsLoading = true;
                                                            Common.Services.Call($http, {
                                                                url: Common.Services.url.OPS,
                                                                method: _COImportPacket_Detail.URL.Reset,
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
                                                            $state.go("main.OPSAppointment.COImportPacket_Detail_2View", { ID: $scope.PacketID });
                                                        }
                                                    });
                                                })
                                            }
                                        })
                                    },
                                    Close: function () {
                                        $state.go("main.OPSAppointment.COImportPacket_Detail_2View", { ID: $scope.PacketID });
                                    }
                                });
                            })
                        }
                    });
                }
            })
        }
    }

    $scope.Detail_Click = function ($event, item, win) {
        $event.preventDefault();
        $scope.MasterID = item.ID;
        if ($scope.MasterID > 0) {
            $scope.con_byid_grid.dataSource.read();
            $timeout(function () {
                $scope.con_byid_grid.refresh();
            }, 100)
        } else {
            $scope.con_grid.dataSource.data(item.ListContainer);
            $timeout(function () {
                $scope.con_grid.refresh();
            }, 100)
        }
        win.center().open();
    }

    $scope.Close_Click = function ($event, win) {
        $event.preventDefault();

        win.close();
    }

    $scope.ToOPS_Click = function ($event) {
        $event.preventDefault();

        $rootScope.Message({
            Type: Common.Message.Type.Confirm,
            Msg: "Xác nhận gửi điều phối?",
            Ok: function () {
                $rootScope.IsLoading = true;
                Common.Services.Call($http, {
                    url: Common.Services.url.OPS,
                    method: _COImportPacket_Detail.URL.ToOPS,
                    data: {
                        pID: $scope.PacketID
                    },
                    success: function (res) {
                        Common.Services.Error(res, function (res) {
                            $rootScope.IsLoading = false;
                            $rootScope.Message({ Msg: "Thành công." });
                            $scope.IsClose = true;
                            $rootScope.Message({
                                Type: Common.Message.Type.Confirm,
                                Msg: "Xóa dữ liệu tạm trong gói import?",
                                Ok: function () {
                                    $rootScope.IsLoading = true;
                                    Common.Services.Call($http, {
                                        url: Common.Services.url.OPS,
                                        method: _COImportPacket_Detail.URL.Reset,
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
                                    $state.go("main.OPSAppointment.COImportPacket_Detail_2View", { ID: $scope.PacketID });
                                }
                            });
                        })
                    }
                })
            }
        })
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
                    method: _COImportPacket_Detail.URL.Reset,
                    data: { pID: $scope.PacketID },
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
    
    $scope.NewVehicle_Update_Click = function ($event, grid, win) {
        $event.preventDefault();

        $rootScope.IsLoading = true;
        Common.Services.Call($http, {
            url: Common.Services.url.OPS,
            method: _COImportPacket_Detail.URL.Vehicle_Update,
            data: { data: grid.dataSource.data() },
            success: function (res) {
                $rootScope.IsLoading = false;
                Common.Services.Error(res, function (res) {
                    win.close();
                    $rootScope.Message({ Msg: "Thành công." })
                    $scope.Excel_Check();
                })
            }
        });
    }

    $scope.DownLoadFile_Click = function ($event) {
        $event.preventDefault();

        $rootScope.IsLoading = true;
        Common.Services.Call($http, {
            url: Common.Services.url.OPS,
            method: _COImportPacket_Detail_Input.URL.Download,
            data: { sID: $scope.Template.ID, pID: $scope.PacketID, data: [] },
            success: function (res) {
                $rootScope.IsLoading = false;
                $rootScope.DownloadFile(res);
            },
            error: function (res) {
                $rootScope.IsLoading = false;
            }
        });
    }
}]);