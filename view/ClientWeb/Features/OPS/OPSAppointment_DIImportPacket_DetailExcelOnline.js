/// <reference path="~/Scripts/jquery-1.9.1.intellisense.js" />
/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />

//#region URL

var _DIImportPacket_Detail = {
    URL: {
        Get: 'OPS_DI_Import_Packet_Get',
        ToOPS: 'OPS_DI_Import_Packet_ToOPS',
        Reset: 'OPS_DI_Import_Packet_Reset',
        Check: 'OPS_DI_Import_Packet_Check',
        Import: 'OPS_DI_Import_Packet_Import',
        Template_List: 'OPS_DI_Import_Packet_Setting_List',
        Template_Get: 'OPS_DI_Import_Packet_Setting_Get',
        TOMaster_List: 'OPS_DI_Import_Packet_TOMaster_List',
        Vehicle_Update: 'OPS_DI_Import_Packet_Vehicle_Update',
        GroupProduct_List: 'OPS_DI_Import_Packet_GroupProduct_ByMaster_List',

        ExcelOnline_Init: 'OPS_DI_Import_ExcelOnline_Init',
        ExcelOnline_Change: 'ORDOrder_ExcelOnline_Change',
        ExcelOnline_Import: 'ORDOrder_ExcelOnline_Import',
        ExcelOnline_Approve: 'ORDOrder_ExcelOnline_Approve',
    },
    Data: { FromFile: null },
    ExcelKey: {
        Resource: "DIImportPacket_Detail",
        Order: "DIImportPacket_Detail"
    }
}

//#endregion

angular.module('myapp').controller('OPSAppointment_DIImportPacket_DetailExcelOnlineCtrl', ['$rootScope', '$scope', '$http', '$location', '$state', '$timeout', function ($rootScope, $scope, $http, $location, $state, $timeout) {
    if ($rootScope.IsPageComplete != true) return;
    Common.Log('OPSAppointment_DIImportPacket_DetailExcelOnlineCtrl');
    $rootScope.IsLoading = false;

    $scope.IsExcelChecked = false;
    $scope.HasTemplate = false;
    $scope.IsClose = true;
    $scope.Template = {
        ID: -1, Name: ''
    }
    $scope.PacketID = parseInt($state.params.ID);
    $scope.MasterID = -1;

    try {
        Common.Services.Call($http, {
            url: Common.Services.url.OPS,
            method: _DIImportPacket_Detail.URL.Get,
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
                            $state.go("main.OPSAppointment.DIImportPacket");
                        }, 1000)
                    } else {
                        $scope.IsClose = res.IsCreated;
                        $scope.HasTemplate = res.CUSSettingID > 0;
                        $scope.Template.ID = res.CUSSettingID;
                        $scope.Template.Name = res.CUSSettingName;
                        $scope.ExcelOnline_Click();
                    }
                })
            }
        })
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
                field: 'SortOrder', width: '100px', title: 'Chuyến', template: "<a href='/' ng-click='Detail_Click($event,dataItem,gop_win)'>Chuyến #=SortOrder#</a>"
            },
            {
                field: 'VendorCode', width: '110px', title: 'Đối tác'
            },
            {
                field: 'VehicleNo', width: '110px', title: 'Số xe'
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
            method: _DIImportPacket_Detail.URL.Template_List,
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

    $scope.gop_gridOptions = {
        dataSource: Common.DataSource.Local({
            data: [],
            pageSize: 0,
            model: {
                id: 'ID',
                fields: {
                    ETD: { type: 'date' },
                    ETA: { type: 'date' },
                    ID: { type: 'number' }
                }
            }
        }),
        height: '100%', groupable: false, pageable: false, sortable: true, columnMenu: false, resizable: true, selectable: true,
        columns: [
            {
                field: 'GroupCode', width: '150px', title: 'Nhóm sản phẩm',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'OrderCode', width: '100px', title: 'Mã ĐH',
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
                field: 'LocationFromName', width: '150px', title: 'Điểm nhận hàng',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'LocationToName', width: '150px', title: 'Điểm giao hàng',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            { title: ' ', filterable: false, menu: false, sortable: false }
        ]
    }

    $scope.gopbyid_gridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.OPS,
            method: _DIImportPacket_Detail.URL.GroupProduct_List,
            readparam: function () {
                return {
                    masterID: $scope.MasterID
                }
            },
            pageSize: 0,
            model: {
                id: 'ID',
                fields: {
                    ETD: { type: 'date' },
                    ETA: { type: 'date' },
                    ID: { type: 'number' }
                }
            }
        }),
        height: '100%', groupable: false, pageable: false, sortable: true, columnMenu: false, resizable: true, selectable: true,
        columns: [
            {
                field: 'GroupCode', width: '150px', title: 'Nhóm sản phẩm',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'OrderCode', width: '100px', title: 'Mã ĐH',
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
                field: 'LocationFromName', width: '150px', title: 'Điểm nhận hàng',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'LocationToName', width: '150px', title: 'Điểm giao hàng',
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
            }
        ]
    }

    $scope.LoadData = function () {
        Common.Services.Call($http, {
            url: Common.Services.url.OPS,
            method: _DIImportPacket_Detail.URL.TOMaster_List,
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

        $timeout(function () {
            angular.element(file.element[0]).trigger('click');
        }, 1);
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
            method: _DIImportPacket_Detail.URL.Check,
            data: {
                pID: $scope.PacketID,
                file: $scope.File.FilePath,
                templateID: $scope.Template.ID
            },
            success: function (res) {
                $rootScope.IsLoading = false;
                Common.Services.Error(res, function (res) {
                    $rootScope.Message({
                        Msg: "Đã kiểm tra."
                    })
                    _DIImportPacket_Detail.Data.FromFile = res;
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
                                MaxWeight: 0, ID: -1
                            });
                        }
                    })
                    if (dataVehicleNew.length > 0) {
                        $rootScope.Message({
                            Type: Common.Message.Type.Confirm,
                            Msg: "Có dữ liệu xe mới. Bạn có muốn lưu?",
                            Ok: function () {
                                $scope.vehicle_win.center().open();
                                $timeout(function () {
                                    $scope.vehicle_gridOptions.dataSource.data(dataVehicleNew);
                                }, 100);
                            }
                        })
                    }
                })
            }
        });
    }

    $scope.Import_Click = function ($event) {
        $event.preventDefault();

        var data = [];
        var idx = 1;
        Common.Data.Each(_DIImportPacket_Detail.Data.FromFile, function (o) {
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
                        method: _DIImportPacket_Detail.URL.Import,
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
                                            method: _DIImportPacket_Detail.URL.ToOPS,
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
                                                                method: _DIImportPacket_Detail.URL.Reset,
                                                                data: { pID: $scope.PacketID },
                                                                success: function (res) {
                                                                    Common.Services.Error(res, function (res) {
                                                                        $rootScope.IsLoading = false;
                                                                        $rootScope.Message({ Msg: "Thành công." });
                                                                        $state.go("main.OPSAppointment.DIImportPacket_Detail_Input", { ID: $scope.PacketID });
                                                                    })
                                                                }
                                                            })
                                                        },
                                                        Close: function () {
                                                            $state.go("main.OPSAppointment.DIImportPacket_Detail_2View", { ID: $scope.PacketID });
                                                        }
                                                    });
                                                })
                                            }
                                        })
                                    },
                                    Close: function () {
                                        $state.go("main.OPSAppointment.DIImportPacket_Detail_2View", { ID: $scope.PacketID });
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
        if (item.ExcelSuccess) {
            $scope.MasterID = item.ID;
            if ($scope.MasterID > 0) {
                $scope.gopbyid_grid.dataSource.read();
                $timeout(function () {
                    $scope.gopbyid_grid.refresh();
                }, 100)
            } else {
                $scope.gop_grid.dataSource.data(item.ListDITOGroupProduct);
                $timeout(function () {
                    $scope.gop_grid.refresh();
                }, 100)
            }
            win.center().open();
        }
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
                    method: _DIImportPacket_Detail.URL.ToOPS,
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
                                        method: _DIImportPacket_Detail.URL.Reset,
                                        data: { pID: $scope.PacketID },
                                        success: function (res) {
                                            Common.Services.Error(res, function (res) {
                                                $rootScope.IsLoading = false;
                                                $rootScope.Message({ Msg: "Thành công." });
                                                $state.go("main.OPSAppointment.DIImportPacket_Detail_Input", { ID: $scope.PacketID });
                                            })
                                        }
                                    })
                                },
                                Close: function () {
                                    $state.go("main.OPSAppointment.DIImportPacket_Detail_2View", { ID: $scope.PacketID });
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
                    method: _DIImportPacket_Detail.URL.Reset,
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
            method: _DIImportPacket_Detail.URL.Vehicle_Update,
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
            method: _DIImportPacket_Detail_Input.URL.Download,
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

    $scope.ExcelOnline_Click = function () {
        //$event.preventDefault();
        var lstMessageError = [];
        for (var i = 0; i < 15; i++) {
            var resource = $rootScope.RS[_DIImportPacket_Detail.ExcelKey.Resource + '_' + i];
            if (Common.HasValue(resource))
                lstMessageError.push(resource);
        }
        if (lstMessageError.length == 0) {
            lstMessageError = [
                'Sai giờ ETD.', //0
                'Sai ETD.', //1
                'Sai thời gian chuyến.', //2
                'Sai giờ ETA.', //3
                'Sai ETA.', //4
                'Sai ràng buộc thời gian ETD-ETA', //5
                'Nhà xe không tồn tại.', //6
                'Nhà xe không xác định.', //7
                'Không có thông tin tài xế.', //8
                'Xe không tồn tại.', //9
                'Không tìm thấy loại xe.', //10
                'Chuyến LTL không thể chạy đơn FTL.', //11
                'Không thể chạy 2 đơn FTL.', //12
                'Quá số tấn.', //13
                'Quá số khối.', //14
                'Quá số lượng.', //15
                'Không tìm thấy sản phẩm.', //16
                'Chuyến FTL không thể chạy đơn LTL.', //17
                'Không có thông tin sản lượng.', //18
                'Chưa phân chuyến hết đơn FTL.', //19
                'Quá trọng tải.', //20
                'XXX', //
                'XXX', //
                'XXX', //
                'XXX', //
                'XXX', //
            ];
        }

        $rootScope.IsLoading = true;
        Common.Services.Call($http, {
            url: Common.Services.url.OPS,
            method: _DIImportPacket_Detail.URL.Template_Get,
            data: {
                templateID: $scope.Template.ID,
            },
            success: function (res) {
                $rootScope.IsLoading = false;
                if (Common.HasValue(res)) {
                    $scope.File = { FileName: 'DIImportPacket_Detail', FileExt: 'xlsx', FilePath: '', ReferID: $rootScope.FunctionItem.ID, TypeOfFileCode: Common.CATTypeOfFileCode.ImportOPS };

                    $scope.excelOPS.Init({
                        functionkey: _DIImportPacket_Detail.ExcelKey.Order + $scope.Template.ID + "_" + $scope.Template.CustomerID + "_" + new Date().getTime(),
                        params: {
                            templateID: $scope.Template.ID, pID: $scope.PacketID
                        },
                        rowStart: res.RowStart,
                        colCheckChange: res.TotalColumn,
                        url: Common.Services.url.OPS,
                        methodInit: _DIImportPacket_Detail.URL.ExcelOnline_Init,
                        methodChange: _DIImportPacket_Detail.URL.ExcelOnline_Change,
                        methodImport: _DIImportPacket_Detail.URL.ExcelOnline_Import,
                        methodApprove: _DIImportPacket_Detail.URL.ExcelOnline_Approve,
                        lstMessageError: lstMessageError,

                        Changed: function () {

                        },
                        Approved: function () {
                            //$scope.CATField_gridOptions.dataSource.read();
                            //$rootScope.Message({ Msg: 'Đã lưu', NotifyType: Common.Message.NotifyType.ERROR });
                        }
                    });
                } else {
                    $rootScope.Message({ Msg: 'Không tìm thấy template', NotifyType: Common.Message.NotifyType.ERROR });
                }
            }
        });

    };

    //#region Excel online
    $timeout(function () {
        if (Common.HasValue($('#spreadsheetOPS'))) {
            $scope.excelOPS.data.spreadsheet = $('#spreadsheetOPS').kendoSpreadsheet({
                excelImport: function (e) {
                    $scope.excelOPS.data.isImport = true;
                    e.promise.progress(function (e) { })
                    .done(function (res) {
                        $rootScope.IsLoading = true;

                        var data = { id: $scope.excelOPS.data.item.ID, worksheets: $scope.excelOPS.data.spreadsheet.sheets(), lstMessageError: $scope.excelOPS.data.options.lstMessageError };
                        angular.forEach($scope.excelOPS.data.options.params, function (v, p) {
                            data[p] = v;
                        });
                        Common.Services.Call($http, {
                            url: $scope.excelOPS.data.options.url,
                            method: $scope.excelOPS.data.options.methodImport,
                            data: data,
                            success: function (res) {
                                $rootScope.IsLoading = false;

                                $scope.excelOPS.data.spreadsheet.fromJSON({ sheets: res.SYSExcel.Worksheets });
                                $scope.excelOPS.data.isImport = false;
                                $scope.excelOPS.data.isChange = true;
                                if (Common.HasValue(res.ListRowResult) && res.ListRowResult.length > 0) {
                                    $scope.IsDI = false;
                                    if (res.ListRowResult[0].TransportModeID == _ORDOrder_ExcelOnline.iFTL || res.ListRowResult[0].TransportModeID == _ORDOrder_ExcelOnline.iLTL) {
                                        $scope.IsDI = true;
                                    }
                                    _ORDOrder_ExcelOnline.Data.CheckResult = res.ListRowResult;
                                    $scope.Check_DataResult(res.ListRowResult);

                                    $scope.excel_locationTo_gridOptions.dataSource.data(res.ListRowResult);
                                }
                            },
                            error: function (res) {
                                $rootScope.IsLoading = false;
                                $scope.excelOPS.data.isImport = false;
                            }
                        });
                    });
                },
                change: function (arg) {
                    if (!$scope.excelOPS.data.isImport) {
                        var lstRowCheck = [];
                        var sheet = $scope.excelOPS.data.spreadsheet.activeSheet();
                        var rowChange = -1;
                        var colChange = -1;
                        var valChange = '';

                        var colFail = 0;
                        arg.range.forEachCell(function (row, col, value) {
                            if (Common.HasValue(value) && Common.HasValue(value.value)) {
                                if (lstRowCheck.indexOf(row) < 0)
                                    lstRowCheck.push(row);
                            }
                            else
                                colFail++;
                            if (col < $scope.excelOPS.data.options.colCheckChange) {
                                rowChange = row;
                                colChange = col;
                                if (Common.HasValue(value.value))
                                    valChange = value.value;
                            }
                        });

                        if (colFail < 2) {
                            if (lstRowCheck.length > 0) {
                                angular.forEach(lstRowCheck, function (rowindex, p) {
                                    var range = sheet.range('A' + (rowindex + 1) + ':Z' + (rowindex + 1));
                                    var cells = [];
                                    var flag = 1;
                                    range.forEachCell(function (row, column, value) {
                                        if (flag < 5) {
                                            var val = '';
                                            if (Common.HasValue(value.value)) {
                                                val = value.value;
                                                flag = 1;
                                            }
                                            else
                                                flag++;
                                            cells.push({ Index: column, Value: val });
                                        }
                                    });

                                    $scope.excelOPS.data.rowRunning++;
                                    $scope.excelOPS.data.isChange = true;
                                    $scope.excelOPS.LoadActive();
                                    var data = { id: $scope.excelOPS.data.item.ID, row: rowindex, cells: cells, lstMessageError: $scope.excelOPS.data.options.lstMessageError };
                                    angular.forEach($scope.excelOPS.data.options.params, function (v, p) {
                                        data[p] = v;
                                    });
                                    Common.Services.Call($http, {
                                        url: $scope.excelOPS.data.options.url,
                                        method: $scope.excelOPS.data.options.methodChange,
                                        data: data,
                                        success: function (res) {
                                            $scope.excelOPS.data.rowRunning--;

                                            $scope.excelOPS.data.isImport = true;
                                            var sheet = $scope.excelOPS.data.spreadsheet.activeSheet();
                                            angular.forEach(res.Row.cells, function (col, p) {
                                                var cell = sheet.range(res.Row.index, col.index, 0, 0);
                                                cell.color(col.color);
                                                cell.background(col.background);
                                                cell.value(col.value);
                                                cell.fontSize(col.fontSize);
                                                cell.fontFamily(col.fontFamily);
                                            });
                                            $scope.excelOPS.data.isImport = false;

                                            if (Common.HasValue($scope.excelOPS.data.options.Changed))
                                                $scope.excelOPS.data.options.Changed();
                                            $scope.excelOPS.LoadActive();

                                            $scope.IsDI = false;
                                            if (res.TransportModeID == _ORDOrder_ExcelOnline.iFTL || res.TransportModeID == _ORDOrder_ExcelOnline.iLTL) {
                                                $scope.IsDI = true;
                                            }

                                            var dataCheck = _ORDOrder_ExcelOnline.Data.CheckResult;

                                            if (Common.HasValue(dataCheck)) {
                                                for (var i = 0 ; i < dataCheck.length ; i++) {
                                                    var value = dataCheck[i];
                                                    if (value.Index == res.Index) {
                                                        dataCheck.splice(i, 1);
                                                    }
                                                }

                                                dataCheck.push(res);
                                                _ORDOrder_ExcelOnline.Data.CheckResult = dataCheck;
                                                $scope.Check_DataResult(dataCheck);

                                                var dataLocationTo = $scope.excel_locationTo_gridOptions.dataSource.data();
                                                if (Common.HasValue(dataLocationTo)) {
                                                    if (dataLocationTo.length == 0) {
                                                        dataLocationTo.push(res);
                                                    } else {
                                                        var hasValue = false;
                                                        for (var i = 0 ; i < dataLocationTo.length ; i++) {
                                                            var value = dataLocationTo[i];
                                                            if (value.Index == res.Index && !hasValue) {
                                                                hasValue = true;
                                                                dataLocationTo.splice(i, 1);
                                                                dataLocationTo.push(res);
                                                            }
                                                        }
                                                    }

                                                    $scope.excel_locationTo_gridOptions.dataSource.data(dataLocationTo);
                                                }
                                            }
                                        },
                                        error: function (res) {
                                            $scope.excelOPS.data.rowRunning--;
                                            $scope.excelOPS.data.isChange = false;
                                            $rootScope.IsLoading = false;
                                        }
                                    });
                                });
                            }
                        }
                    }
                },
            }).data("kendoSpreadsheet");
        }
    }, 1);

    $scope.excelOPS = {
        data: {
            spreadsheet: null,
            item: null,
            isImport: false,
            isChange: false,
            options: null,
            rowRunning: 0,
            activeAccept: true,
            captionAccept: 'Đồng ý',
            captionWin: 'Excel'
        },
        LoadActive: function () {
            if ($scope.excelOPS.data.rowRunning > 0) {
                $scope.excelOPS.data.activeAccept = true;
                $scope.excelOPS.data.captionAccept = '(' + $scope.excelOPS.data.rowRunning + ') kiểm tra';
            }
            else {
                $scope.excelOPS.data.activeAccept = false;
                $scope.excelOPS.data.captionAccept = 'Đồng ý';
            }
        },
        Init: function (options) {
            options = $.extend(true, {
                functionkey: '',
                params: {},
                lstMessageError: [],
                colCheckChange: -1,
                url: '',
                methodInit: '',
                methodChange: '',
                methodImport: '',
                methodApprove: '',

                Changed: null,
                Approved: null
            }, options);

            if (!Common.HasValue(options.methodInit) || options.methodInit == '')
                throw 'ExcelShare: methodInit fail';
            else if (!Common.HasValue(options.methodChange) || options.methodChange == '')
                throw 'ExcelShare: methodChange fail';
            else if (!Common.HasValue(options.methodImport) || options.methodImport == '')
                throw 'ExcelShare: methodImport fail';
            else if (!Common.HasValue(options.methodApprove) || options.methodApprove == '')
                throw 'ExcelShare: methodApprove fail';
            else if (!Common.HasValue(options.colCheckChange) || options.colCheckChange < 1)
                throw 'ExcelShare: colCheckChange fail';
            else {
                $scope.excelOPS.data.options = options;

                //$scope.winexcelshare.center();
                //$scope.winexcelshare.maximize();
                //$scope.winexcelshare.open();

                var data = { functionid: $rootScope.FunctionItem.ID, functionkey: $scope.excelOPS.data.options.functionkey, isreload: false };
                angular.forEach($scope.excelOPS.data.options.params, function (v, p) {
                    data[p] = v;
                });
                $rootScope.IsLoading = true;
                Common.Services.Call($http, {
                    url: options.url,
                    method: options.methodInit,
                    data: data,
                    success: function (res) {
                        $scope.excelOPS.data.isImport = true;
                        $scope.excelOPS.data.item = res;
                        $scope.excelOPS.data.spreadsheet.fromJSON({ sheets: res.Worksheets });
                        $scope.excelOPS.data.isImport = false;

                        $rootScope.IsLoading = false;
                    },
                    error: function (res) {
                        $rootScope.IsLoading = false;
                    }
                });
            }
        }
    };

    $scope.excelOPS_Approve_Click = function ($event) {
        $event.preventDefault();

        if ($scope.excelOPS.data.rowRunning == 0) {
            if ($scope.excelOPS.data.isChange == true) {
                $rootScope.Message({
                    Type: Common.Message.Type.Confirm,
                    Msg: 'Bạn muốn lưu các dữ liệu đã thay đổi ?',
                    pars: {},
                    Ok: function (pars) {
                        //$rootScope.IsLoading = true;

                        $rootScope.Loading.Show();
                        var data = { id: $scope.excelOPS.data.item.ID };
                        angular.forEach($scope.excelOPS.data.options.params, function (v, p) {
                            data[p] = v;
                        });
                        Common.Services.Call($http, {
                            url: $scope.excelOPS.data.options.url,
                            method: $scope.excelOPS.data.options.methodApprove,
                            data: data,
                            success: function (res) {
                                $scope.excelOPS.data.isChange = false;
                                $rootScope.Loading.Change('Lấy dữ liệu excel mới', 10);
                                if (res > 0) {
                                    var data = { functionid: $rootScope.FunctionItem.ID, functionkey: $scope.excelOPS.data.options.functionkey, isreload: true };
                                    angular.forEach($scope.excelOPS.data.options.params, function (v, p) {
                                        data[p] = v;
                                    });

                                    $scope.pID = res;
                                    data['pID'] = $scope.pID;
                                    Common.Services.Call($http, {
                                        url: $scope.excelOPS.data.options.url,
                                        method: $scope.excelOPS.data.options.methodInit,
                                        data: data,
                                        success: function (res) {
                                            $rootScope.Loading.Hide();
                                            $rootScope.Message({ Msg: 'Lưu dữ liệu thành công', NotifyType: Common.Message.NotifyType.SUCCESS });

                                            $scope.excelOPS.data.isImport = true;
                                            $scope.excelOPS.data.item = res;
                                            $scope.excelOPS.data.spreadsheet.fromJSON({ sheets: res.Worksheets });
                                            $scope.excelOPS.data.isImport = false;

                                            if (Common.HasValue($scope.excelOPS.data.options.Approved))
                                                $scope.excelOPS.data.options.Approved();
                                        },
                                        error: function (res) {
                                            $rootScope.Loading.Hide();
                                        }
                                    });
                                }
                                else {
                                    $rootScope.Loading.Hide();
                                    $rootScope.Message({ Msg: 'Dữ liệu gửi lỗi', NotifyType: Common.Message.NotifyType.Error });
                                }
                            },
                            error: function (res) {
                                $rootScope.Loading.Hide();
                            }
                        });
                        $rootScope.Loading.Change('Gửi dữ liệu', 10);
                    }
                });
            }
        }
        else
            $rootScope.Message({ Msg: 'Đang kiểm tra không nhấn đồng ý được', NotifyType: Common.Message.NotifyType.ERROR });
    };

    $scope.excelOPS_Reload_Click = function ($event) {
        $event.preventDefault();
        $rootScope.IsLoading = true;

        var data = { functionid: $rootScope.FunctionItem.ID, functionkey: $scope.excelOPS.data.options.functionkey, isreload: true };
        angular.forEach($scope.excelOPS.data.options.params, function (v, p) {
            data[p] = v;
        });
        Common.Services.Call($http, {
            url: $scope.excelOPS.data.options.url,
            method: $scope.excelOPS.data.options.methodInit,
            data: data,
            success: function (res) {
                $rootScope.IsLoading = false;
                $scope.excelOPS.data.item = res;
                $scope.excelOPS.data.spreadsheet.fromJSON({ sheets: res.Worksheets });
                $scope.IsCO = false;
                $scope.IsDI = false;
                $scope.IsNewLocation = false;
                $scope.TotalLocationNew = 0;
            },
            error: function (res) {
                $rootScope.IsLoading = false;
            }
        });
    };
    //#endregion
}]);