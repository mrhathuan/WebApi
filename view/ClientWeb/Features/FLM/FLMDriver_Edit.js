
/// <reference path="~/Scripts/jquery-1.9.1.intellisense.js" />
/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />
//(function () {
//    kendo.cultures["vi-VN"] = {
//        name: "vi-VN", numberFormat: { pattern: ["-n"], decimals: 2, ",": ".", ".": ",", groupSize: [3], percent: { pattern: ["-n %", "n %"], decimals: 2, ",": ".", ".": ",", groupSize: [3], symbol: "%" }, currency: { pattern: ["-n $", "n $"], decimals: 2, ",": ".", ".": ",", groupSize: [3], symbol: "â‚«" } }, calendars: {
//            standard: {
//                days: {
//                    names: ["Chủ Nhật", "Thứ Hai", "Thứ Ba", "Thứ Tư", "Thứ Năm", "Thứ Sáu", "Thứ Bảy"],
//                    namesAbbr: ["CN", "Hai", "Ba", "Tư", "Năm", "Sáu", "Bảy"],
//                    namesShort: ["CN", "Hai", "Ba", "Tư", "Năm", "Sáu", "Bảy"]
//                },
//                months: {
//                    names: ["Tháng một", "Tháng Hai", "Tháng Ba", "Tháng Tư", "Tháng Năm", "Tháng Sáu", "Tháng Bảy", "Tháng Tám", "Tháng Chín", "Tháng Mười", "Tháng Mười Mọt", "Tháng Mười Hai", ""],
//                    namesAbbr: ["Thg1", "Thg2", "Thg3", "Thg4", "Thg5", "Thg6", "Thg7", "Thg8", "Thg9", "Thg10", "Thg11", "Thg12", ""]
//                },
//                AM: ["SA", "sa", "SA"],
//                PM: ["CH", "ch", "CH"],
//                patterns: { d: "dd/MM/yyyy", D: "dd MMMM yyyy", F: "dd MMMM yyyy h:mm:ss tt", g: "dd/MM/yyyy h:mm tt", G: "dd/MM/yyyy h:mm:ss tt", m: "dd MMMM", M: "dd MMMM", s: "yyyy'-'MM'-'dd'T'HH':'mm':'ss", t: "h:mm tt", T: "h:mm:ss tt", u: "yyyy'-'MM'-'dd HH':'mm':'ss'Z'", y: "MMMM yyyy", Y: "MMMM yyyy" }, "/": "/", ":": ":", firstDay: 1
//            }
//        }
//    }
//})(this);
var _FLMDriverEdit = {
    URL: {
        Update: 'FLMDriver_Update',
        Delete: 'FLMDriver_Delete',
        Get: 'FLMDriver_Get',

        Read_TransportHistory: 'FLMDriver_TransportHistory_Read',
        Get_Department: 'FLMDriver_Department_Get',
        Read_Schedule: 'FLMDriver_Schedule_Read',

        CATDrivingLicence_List: 'ALL_CATDrivingLicence',
        DrivingLicence_List: 'FLMDriver_DrivingLicence_List',
        DrivingLicence_Get: 'FLMDriver_DrivingLicence_GetDetail',
        DrivingLicence_Save: 'FLMDriver_DrivingLicence_Save',
        DrivingLicence_Delete: 'FLMDriver_DrivingLicence_Delete',

        Schedule_Data: 'FLMDriver_Schedule_Data',

        FeeDefault_List: 'FLMDriver_FLMScheduleFeeDefault_List',
        FeeDefault_Get: 'FLMDriver_FLMScheduleFeeDefault_Get',
        FeeDefault_Save: 'FLMDriver_FLMScheduleFeeDefault_Save',
        FeeDefault_Delete: 'FLMDriver_FLMScheduleFeeDefault_Delete',

        FLMDriverRole_List: 'FLMDriver_FLMDriverRole_List',
        FLMDriverRole_Get: 'FLMDriver_FLMDriverRole_Get',
        TypeOfDriver: 'ALL_SYSVarTypeOfDriver',
        FLMDriverRole_Save: 'FLMDriver_FLMDriverRole_Save',
        FLMDriverRole_Delete: 'FLMDriver_FLMDriverRole_Delete',


    },
    Param: {
        DriverID: -1
    },
    Data: {
        ListDate: [],
        ListSumaryDate:[],
        ObjDate: {},
        objSchedule: {}
    }
}

angular.module('myapp').controller('FLMDriver_EditCtrl', ['$rootScope', '$scope', '$http', '$location', '$state', '$timeout', '$compile', function ($rootScope, $scope, $http, $location, $state, $timeout, $compile) {
    Common.Log('FLMDriver_EditCtrl');
    $rootScope.IsLoading = false;
    $scope.ParamEdit = { DriverID: -1 }

    $scope.IsNewDriver = true;
    $scope.IsShowTab = false;
    $scope.IsDetail = false;
    $scope.TabIndex = 1;
    $scope.scheduleID = '';
    $scope.ValueTypeDate = {};
    $scope.MonthYear = {
        date:(new Date()),
        month: (new Date()).getMonth()+1,
        year: (new Date()).getFullYear()
    }
    $scope.monthyear = [];

    $scope.DriverTransItem = {
        dtFrom: (new Date()).addDays(-7),
        dtTo: new Date(),
    }

    $scope.DriverScheduleItem = {
        dtFrom: (new Date()).addDays(-7),
        dtTo: new Date(),
    }

    _FLMDriverEdit.Param = $.extend(true, _FLMDriverEdit.Param, $state.params);
    $scope.DriverItem = { DriverID: 0 };
    if (_FLMDriverEdit.Param.DriverID > 0) { $scope.IsShowTab = true; $scope.IsNewDriver = false; }

    Common.Services.Call($http, {
        url: Common.Services.url.FLM,
        method: _FLMDriverEdit.URL.Get,
        data: { DriverID: _FLMDriverEdit.Param.DriverID },
        success: function (res) {
            $scope.DriverItem = res;
            $scope.drivingLicence_GridOptions.dataSource.read();
            $scope.FeeDefault_GridOptions.dataSource.read();
        }
    });

    $scope.FLMDriver_Edit_TabOptions = {
        animation: { open: { effects: "fadeIn" } },
        select: function (e) {
            $timeout(function () {
                $scope.TabIndex = angular.element(e.item).data('tabindex'); //or
            }, 1);
        }
    }

    $scope.FLMDriver_SaveClick = function ($event, vform) {
        $event.preventDefault();
        if (vform()) {
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.FLM,
                method: _FLMDriverEdit.URL.Update,
                data: { item: $scope.DriverItem },
                success: function (res) {
                    $rootScope.IsLoading = false;
                    $rootScope.Message({
                        Type: Common.Message.Type.Alert,
                        NotifyType: Common.Message.NotifyType.SUCCESS,
                        Title: 'Thông báo',
                        Msg: 'Thành công',
                        Close: function (e) {
                            $state.go('main.FLMDriver.Index')
                        },
                        Ok: function (e) {
                            $state.go('main.FLMDriver.Index')
                        }
                    })
                }
            });
        }
    }

    $scope.FLMDriver_BackClick = function ($event) {
        $event.preventDefault();
        $state.go("main.FLMDriver.Index")
    }

    $scope.FLMDriver_DelClick = function ($event) {
        $event.preventDefault();
        $rootScope.Message({
            Type: Common.Message.Type.Confirm,
            NotifyType: Common.Message.NotifyType.SUCCESS,
            Title: 'Thông báo',
            Msg: 'Bạn muốn xóa dữ liệu này',
            Ok: function (e) {
                $rootScope.IsLoading = true;
                Common.Services.Call($http, {
                    url: Common.Services.url.FLM,
                    method: _FLMDriverEdit.URL.Delete,
                    data: { item: $scope.DriverItem },
                    success: function (res) {
                        $rootScope.IsLoading = false;
                        $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                        $state.go('main.FLMDriver.Index')
                    },
                    error: function (res) {
                        $rootScope.IsLoading = false;
                    }
                });
            },
            close: null,
        })
    }

    $scope.cboDepartment_options = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: "contains",
        suggest: true, dataTextField: 'DepartmentName', dataValueField: 'ID',
        dataSource: Common.DataSource.Local([], {
            id: 'ID',
            fields: {
                DepartmentName: { type: 'string' },
                ID: { type: 'number' },
            }
        })
    }

    Common.Services.Call($http, {
        url: Common.Services.url.FLM,
        method: _FLMDriverEdit.URL.Get_Department,
        data: {},
        success: function (res) {
            $scope.cboDepartment_options.dataSource.data(res.Data)
        }
    });

    $scope.numFeeBase_options = { format: 'n0', spinners: false, culture: 'en-US', min: 0, step: 1, decimals: 0, }
    $scope.vehicleWeight_options = { format: 'n0', spinners: false, culture: 'en-US', min: 0, step: 1, decimals: 0, }
    $scope.DaysAllowOff_options = { format: '#', spinners: false, culture: 'en-US', min: 0, step: 0.1 }

    $scope.typeTrans = 1;
    $scope.cboTypeTranHistory_options = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: "contains",
        suggest: true, dataTextField: 'ValueName', dataValueField: 'ID',
        dataSource: [
            { ID: 1, ValueName: 'Đã nhận' },
            { ID: 2, ValueName: 'Đã từ chối' },
        ],
        change: function (e) {
        }
    }

    $scope.FLMDriver_TranHistory_Options = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.FLM,
            method: _FLMDriverEdit.URL.Read_TransportHistory,
            readparam: function () { return { DriverID: _FLMDriverEdit.Param.DriverID, dateFrom: $scope.DriverTransItem.dtFrom, dateTo: $scope.DriverTransItem.dtTo, typeTrans: $scope.typeTrans } },
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number', editable: false, nullable: true },
                    MasterCode: { type: 'string' },
                    RegNo: { type: 'string' },
                    Debit: { type: 'number' },
                    ATA: { type: 'date' },
                    ATD: { type: 'date' },
                    IsDITOMaster: { type: 'boolean' }
                }
            }
        }),
        height: '99%', pageable: true, sortable: true, columnMenu: false, filterable: { mode: 'row' }, resizable: true, editable: false,
        columns: [
            { field: 'MasterCode', title: 'Lệnh vận chuyển', width: '200px', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'RegNo', title: 'Số xe', width: '200px', filterable: { cell: { operator: 'contains', showOperators: false } } },
            {
                field: 'ATD', title: 'Ngày bắt đầu', width: '150px', template: "#=ATD==null?\" \": Common.Date.FromJsonDMYHM(ATD)#",
                filterable: { cell: { template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); }, operator: 'gte', showOperators: false } }
            },
            {
                field: 'ATA', title: 'Ngày kết thúc', width: '150px', template: "#=ATA==null?\" \": Common.Date.FromJsonDMYHM(ATA)#",
                filterable: { cell: { template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); }, operator: 'gte', showOperators: false } }
            },
            {
                field: 'IsDITOMaster', title: 'Xe tải/container', width: '100px', template: '<input type="checkbox" #= IsDITOMaster==true ? "checked=checked" : "" # disabled="disabled" />', attributes: { style: "text-align: center; " },
                filterable: {
                    cell: {
                        template: function (container) {
                            container.element.kendoComboBox({
                                dataSource: [{ Text: 'Xe tải', Value: true }, { Text: 'Container', Value: false }, { Text: 'Tất cả', Value: "" }],
                                dataTextField: "Text", dataValueField: "Value",
                            });
                        }, showOperators: false
                    }
                }
            },
            { field: 'Debit', title: 'Chi phí', width: '200px' },
        ]
    }

    $scope.DriverRole_GridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.FLM,
            method: _FLMDriverEdit.URL.FLMDriverRole_List,
            readparam: function () { return { DriverID: _FLMDriverEdit.Param.DriverID} },
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number', editable: false, nullable: true },
                    IsAssistant: { type: 'string' },
                    EffectDate: { type: 'date' }
                }
            }
        }),
        height: '99%', pageable: true, sortable: true, columnMenu: false, filterable: false, resizable: true, editable: { mode: 'row' },
        columns: [
            {
                title: ' ', width: '90px',
                template: '<a href="/" ng-click="driverRole_Edit_Click($event,dataItem, DriverRole_win)" class="k-button"><i class="fa fa-pencil"></i></a>' +
                    '<a href="/" ng-click="driverRole_Destroy_Click($event,dataItem)" class="k-button"><i class="fa fa-trash"></i></a>',
                filterable: false, sortable: false
            },
            {
                field: 'EffectDate', title: 'Ngày hiệu lực', width: '200px', template: '#=Common.Date.FromJsonDDMMYY(EffectDate)#',
                filterable: { cell: { template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); }, operator: 'gte', showOperators: false } }, width: '150px'
            },
            {
                field: 'IsAssistant', title: 'Phụ xe', width: '100px', template: '<input type="checkbox" #= IsAssistant=="true" ? "checked=checked" : "" # disabled="disabled" />', attributes: { style: "text-align: center; " },
                filterable: {
                    cell: {
                        template: function (container) {
                            container.element.kendoComboBox({
                                dataSource: [{ Text: 'Phụ xe', Value: true }, { Text: 'Lái xe', Value: false }],
                                dataTextField: "Text", dataValueField: "Value",
                            });
                        }, showOperators: false
                    }
                }
            },
            { field: 'TypeOfVehicleName', title: 'Loại tài xế', width: '150px'},
            { field: 'VehicleWeight', title: 'Trọng tải', width: '100px' },
            { title: ' ', filterable: false, sortable: false }
        ]
    }

    $scope.DriverRole_Add_Click = function ($event, win) {
        $event.preventDefault();
        $scope.loadDriverRole(0, win);
    }
    $scope.driverRole_Edit_Click = function ($event, data, win) {
        $event.preventDefault();
        $scope.loadDriverRole(data.ID, win);
    }
    $scope.driverRole_Destroy_Click = function ($event, data) {
        $rootScope.Message({
            Msg: "Xác nhận xóa?",
            Type: Common.Message.Type.Confirm,
            Ok: function () {
                $rootScope.IsLoading = true;
                Common.Services.Call($http, {
                    url: Common.Services.url.FLM,
                    method: _FLMDriverEdit.URL.FLMDriverRole_Delete,
                    data: { item:data.ID },
                    success: function (res) {
                        $scope.DriverRole_GridOptions.dataSource.read();
                        $rootScope.Message({
                            Msg: 'Đã cập nhật.',
                            NotifyType: Common.Message.NotifyType.SUCCESS
                        });
                        $rootScope.IsLoading = false;
                    }
                });
            }
        })
    }
    $scope.loadDriverRole = function(ID, win){
        $rootScope.IsLoading = true;
        Common.Services.Call($http, {
            url: Common.Services.url.FLM,
            method: _FLMDriverEdit.URL.FLMDriverRole_Get,
            data: { id:ID },
            success: function (res) {
                $scope.driverRole = res;
                win.center().open();
                $rootScope.IsLoading = false;
            }
        })

    }

    $scope.driverRole_Save_Click = function($event, win, vform){
        $event.preventDefault();
        if (vform()) {
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.FLM,
                method: _FLMDriverEdit.URL.FLMDriverRole_Save,
                data: { DriverID: _FLMDriverEdit.Param.DriverID, item: $scope.driverRole },
                success: function (res) {
                    $scope.DriverRole_GridOptions.dataSource.read();
                    win.close();
                    $rootScope.Message({
                        Msg: 'Đã cập nhật.',
                        NotifyType: Common.Message.NotifyType.SUCCESS
                    });
                    $rootScope.IsLoading = false;
                }
            });
        }
    }

    $scope.cboTypeOfVehicle_Options = {
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
        })
    }
    $scope.cboTypeVehicle_Options = {
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
        })
    }
    
    Common.ALL.Get($http, {
        url: Common.ALL.URL.TypeOfVehicle,
        success: function (res) {
            var item = { ID: -1, ValueOfVar: ' ' };
            var data = [];
            data.push(item);
            $.each(res, function (i, v) {
                data.push(v);
            });
            $scope.cboTypeOfVehicle_Options.dataSource.data(data);
            $scope.cboTypeVehicle_Options.dataSource.data(data);
        }
    })


    $scope.FLMDriver_TransHistory_ReReadClick = function ($event) {
        $event.preventDefault()
        if (Common.HasValue($scope.DriverTransItem.dtFrom) && Common.HasValue($scope.DriverTransItem.dtTo)) {
            $rootScope.IsLoading = true;
            $scope.FLMDriver_TranHistory_Options.dataSource.read()
            $rootScope.IsLoading = false;
        }
        else {
            $rootScope.Message({
                Type: Common.Message.Type.Alert,
                NotifyType: Common.Message.NotifyType.SUCCESS,
                Title: 'Thông báo',
                Msg: 'Chọn từ ngày, đến ngày cho chính xác',
                Close: null,
                Ok: null
            })
        }
    }

    $scope.FLMDriver_Schedule_Options = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.FLM,
            method: _FLMDriverEdit.URL.Read_Schedule,
            readparam: function () { return { DriverID: _FLMDriverEdit.Param.DriverID, dateFrom: $scope.DriverScheduleItem.dtFrom, dateTo: $scope.DriverScheduleItem.dtTo } },
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number', editable: false, nullable: true },
                    Date: { type: 'date' }
                }
            }
        }),
        height: '100%', pageable: true, sortable: true, columnMenu: false, filterable: false, resizable: true, editable: { mode: 'inline' },
        columns: [
            { field: 'Date', title: 'Ngày', width: '200px', template: "#=Date==null?\" \": Common.Date.FromJsonDMY(Date)#" },
            { field: 'ScheduleDetailTypeName', title: 'Chấm công(T-tăng ca, X-chấm công, O-chạy xe, P-Phép, L-lễ)', width: '250px' },
        ]
    }
    $scope.FLMDriver_Schedule_ReReadClick = function ($event) {
        $event.preventDefault()
        if (Common.HasValue($scope.DriverScheduleItem.dtFrom) && Common.HasValue($scope.DriverScheduleItem.dtTo)) {
            $rootScope.IsLoading = true;
            $scope.FLMDriver_Schedule_Options.dataSource.read()
            $rootScope.IsLoading = false;
        }
        else {
            $rootScope.Message({
                Type: Common.Message.Type.Alert,
                NotifyType: Common.Message.NotifyType.SUCCESS,
                Title: 'Thông báo',
                Msg: 'Chọn từ ngày, đến ngày cho chính xác',
                Close: null,
                Ok: null
            })
        }
    }


    //#region Driving Licence
    $scope.drivingLicence_GridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.FLM,
            method: _FLMDriverEdit.URL.DrivingLicence_List,
            readparam: function () { return { driverId: $scope.DriverItem.DriverID } },
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number', editable: false, nullable: true },
                    ExpiredDate: { type: 'date' },
                    IsUse: { type: 'string' },
                    IsChoose: { type: 'boolean' },
                }
            }
        }),
        height: '100%', pageable: true, sortable: true, columnMenu: false, filterable: { mode: 'row' }, resizable: true, editable: false,
        columns: [
            {
                title: ' ', width: '90px',
                template: '<a href="/" ng-click="drivingLicence_GridEdit_Click($event,dataItem, drivingLicence_win,drivingLicence_vform)" class="k-button"><i class="fa fa-pencil"></i></a>' +
                    '<a href="/" ng-click="drivingLicence_GridDestroy_Click($event,dataItem)" class="k-button"><i class="fa fa-trash"></i></a>',
                filterable: false, sortable: false
            },
            { field: 'DrivingLicenceName', title: "Loại bằng lái", width: 120, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'DrivingLicenceNumber', title: "Số bằng lái", width: 150, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'Note', title: "Ghi chú", width: 150, filterable: { cell: { operator: 'contains', showOperators: false } } },
            {
                field: 'ExpiredDate', title: 'Ngày hết hạn', template: '#=Common.Date.FromJsonDDMMYY(ExpiredDate)#',
                filterable: { cell: { template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); }, operator: 'gte', showOperators: false } }, width: '150px'
            },
            {
                field: 'IsUse', title: 'Đang sử dụng', width: 120,
                template: '<input type="checkbox" #= IsUse=="true" ? "checked=checked" : "" # disabled="disabled" />',
                attributes: { style: "text-align: center; " },
                filterable: {
                    cell: {
                        template: function (container) {
                            container.element.kendoComboBox({
                                dataSource: [{ Text: 'Đang sử dụng', Value: true }, { Text: 'Không sử dụng', Value: false }],
                                dataTextField: "Text", dataValueField: "Value",
                            });
                        }, showOperators: false
                    }
                }
            },
            { title: ' ', filterable: false, sortable: false }
        ]
    }

    $scope.drivingLicence_AddNew = function ($event, win, vform) {
        $event.preventDefault();
        $scope.DrivingLicenceLoadItem(0, win, vform)
    }

    $scope.drivingLicence_GridEdit_Click = function ($event, item, win, vform) {
        $event.preventDefault();
        $scope.DrivingLicenceLoadItem(item.ID, win, vform)
    }

    $scope.DrivingLicenceLoadItem = function (id, win, vform) {
        vform({ clear: true });
        $rootScope.IsLoading = true;
        Common.Services.Call($http, {
            url: Common.Services.url.FLM,
            method: _FLMDriverEdit.URL.DrivingLicence_Get,
            data: { id: id },
            success: function (res) {
                $rootScope.IsLoading = false;
                $scope.ItemDrivingLicence = res;
                vform({ clear: true })
                win.center().open();
            },
            error: function (res) {
                $rootScope.IsLoading = false;
            }
        });

    }

    $scope.drivingLicence_GridDestroy_Click = function ($event, item) {
        $event.preventDefault();
        $rootScope.Message({
            Type: Common.Message.Type.Confirm,
            NotifyType: Common.Message.NotifyType.SUCCESS,
            Title: 'Thông báo',
            Msg: 'Bạn muốn xóa bằng lái đã chọn?',
            Close: null,
            Ok: function () {
                $rootScope.IsLoading = true;
                Common.Services.Call($http, {
                    url: Common.Services.url.FLM,
                    method: _FLMDriverEdit.URL.DrivingLicence_Delete,
                    data: { item: item },
                    success: function (res) {
                        $scope.drivingLicence_GridOptions.dataSource.read();
                        $rootScope.IsLoading = false;
                        $rootScope.Message({ Msg: "Đã xóa!" });
                    }, error: function (res) {
                        $rootScope.IsLoading = false;
                    }
                });
            }
        })

    }

    $scope.drivingLicence_Save_Click = function ($event, win, vform) {
        $event.preventDefault();
        if (vform()) {
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.FLM,
                method: _FLMDriverEdit.URL.DrivingLicence_Save,
                data: { driverId: $scope.DriverItem.DriverID, item: $scope.ItemDrivingLicence },
                success: function (res) {
                    $scope.drivingLicence_GridOptions.dataSource.read();
                    $rootScope.IsLoading = false;
                    $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                    win.close();
                },
                error: function (res) {
                    $rootScope.IsLoading = false;
                }
            });
        }
    }

    $scope.DrivingLicence_CBbOoptions = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true, dataTextField: 'DrivingLicenceName', dataValueField: 'ID',
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    DrivingLicenceName: { type: 'string' },
                }
            }
        })
    }
    Common.Services.Call($http, {
        url: Common.Services.url.CAT,
        method: _FLMDriverEdit.URL.CATDrivingLicence_List,
        data: {},
        success: function (res) {
            if (Common.HasValue(res)) {
                $scope.DrivingLicence_CBbOoptions.dataSource.data(res.Data);
            }
        }
    });
    //#end region

    $scope.Search_Schedule_Click = function () {
      
        $rootScope.IsLoading = true;
        Common.Services.Call($http, {
            url: Common.Services.url.FLM,
            method: _FLMDriverEdit.URL.Schedule_Data,
            data: { driverId: _FLMDriverEdit.Param.DriverID, month: $scope.MonthYear.month, year: $scope.MonthYear.year },
            success: function (res) {
                $rootScope.IsLoading = false;
                _FLMDriverEdit.Data.ListSumaryDate = res.ListSumaryDate;
                var item = { Month: 1, Year: new Date().getFullYear() };
                if ($scope.scheduleID > 0) {
                    if (Common.HasValue(_FLMDriverEdit.Data.objSchedule[$scope.scheduleID])) {
                        item.Month = _FLMDriverEdit.Data.objSchedule[$scope.scheduleID].Month;
                        item.Year = _FLMDriverEdit.Data.objSchedule[$scope.scheduleID].Year;
                    }
                }
                var dataDate = [];
                _FLMDriverEdit.Data.ObjDate = {};

                _FLMDriverEdit.Data.ListDate = [];
                _FLMDriverEdit.Data.ListDate = res.ListDetail;
                Common.Data.Each(res.ListDetail, function (o) {
                    var field = Common.Date.FromJsonDMY(o.Date);
                    if (!Common.HasValue(_FLMDriverEdit.Data.ObjDate[field])) {
                        _FLMDriverEdit.Data.ObjDate[field] = o;
                    }
                    dataDate.push(Common.Date.FromJson(o.Date));
                })
                var today = $scope.MonthYear.date;
                $scope.calendar_Options.value = today;
                $scope.calendar_Options.dates = dataDate;
                $timeout(function () {
                    $scope.GetStyleDriver();
                }, 10);
                //get valuetypedate
                $.each(res.ListSumaryDate, function(i,v){
                    if(v.TypeDate == 1)
                        $scope.ValueTypeDate.work = v.Total;
                    else if (v.TypeDate == 2)
                        $scope.ValueTypeDate.offday = v.Total;
                    else if (v.TypeDate == 3)
                        $scope.ValueTypeDate.holiday = v.Total;
                });
            }
        });
    };
    $scope.Search_Schedule_Click();

    $scope.viewSplitter_Options = {
        orientation: "horizontal",
        panes: [
            { collapsible: true, resizable: true, size: '70%' },
            { collapsible: true, resizable: true, size: '30%', collapsed: false }
        ],
    }

    $scope.calendar_Options = {
        value: new Date(),
        culture: "vi-VN",
        dates: [],
        month: {
            // template for dates in month view
            content: '<div class="' + '#=getClass(data)#' + '">#=data.value #</div>'
        },
        change:function(e){
            Common.Log(this.value());
            if ((new Date(this.value()).getMonth()+1) != $scope.MonthYear.month) {
                $scope.MonthYear.date = new Date(this.value());
                $scope.MonthYear.month = new Date(this.value()).getMonth() + 1;
                $scope.MonthYear.year = new Date(this.value()).getFullYear();
                $scope.Search_Schedule_Click();
            }
        },
        navigate: function (e) {
            if (Common.Date.FromJsonDMY(this._current) != Common.Date.FromJsonDMY($scope.MonthYear.date))
            {
                $scope.MonthYear.date = new Date(this._current);
                $scope.MonthYear.month = new Date(this._current).getMonth() + 1;
                $scope.MonthYear.year = new Date(this._current).getFullYear();
                $scope.Search_Schedule_Click();
            }
        },
        footer: false
    };

    $scope.GetStyleDriver = function () {
        $('.cus-calendar').find('tr[role="row"]').each(function () {
            $(this).find('.workday').closest('td').addClass("style-color-1");
            $(this).find('.offday').closest('td').addClass("style-color-2");
            $(this).find('.holiday').closest('td').addClass("style-color-3");
        });
    };

    $scope.PrintScheduleFee_Click = function ($event) {
        $event.preventDefault();
        var view = $scope.calendar.view();
        var current = $scope.calendar.current();
        var month = current.getMonth() + 1;
        var year = current.getFullYear();
        location.href = "Report.aspx#/ScheduleDriverFee/" + Common.Auth.HeaderKey + "/" + _FLMDriverEdit.Param.DriverID + "/"+month+"/"+year;
    }

    //#region PhuPhiThang
    $scope.FeeDefault_GridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.FLM,
            method: _FLMDriverEdit.URL.FeeDefault_List,
            readparam: function () { return { driverID: _FLMDriverEdit.Param.DriverID } },
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number', editable: false, nullable: true },
                    WarrantyEnd: { type: 'date' }
                }
            }
        }),
        height: '100%', pageable: true, sortable: true, columnMenu: false, filterable: false, resizable: true,
        columns: [
            {
                title: ' ', width: '85px',
                template: '<a href="/" ng-click="FeeDefault_Edit_Click($event,dataItem,assetFee_win)" class="k-button" ><i class="fa fa-pencil"></i></a>' +
                    '<a href="/" ng-click="FeeDefault_Delete_Click($event,dataItem)" class="k-button" ><i class="fa fa-trash"></i></a>',
                filterable: false, sortable: false
            },
            { field: 'TypeOfScheduleFeeCode', title: 'Mã chi phí', width: 150, template: '' },
            { field: 'TypeOfScheduleFeeName', title: 'Tên chi phí', width: 150, },
            { field: 'ExprPrice ', title: 'Công thức giá', width: 150, template: '' },
            { field: 'ExprInputDay ', title: 'Điều kiện ngày', width: 150, },
            { field: 'ExprPriceDay ', title: 'Công thức theo ngày', width: 150, },
            { field: 'ExprInputTOMaster ', title: 'Điều kiện chuyến', width: 150, },
            { field: 'ExprPriceTOMaster ', title: 'Công thức theo chuyến', width: 150, },
            { title: '', filterable: false, sortable: false },
        ]
    }

    $scope.FeeDefault_Add_Click = function ($event, win) {
        $event.preventDefault();
        $scope.LoadData_FeeDefault(0, win);
    };

    $scope.FeeDefault_Edit_Click = function ($event, data, win) {
        $event.preventDefault();
        $scope.LoadData_FeeDefault(data.ID, win);
    };

    $scope.LoadData_FeeDefault = function (ID, win) {
        $rootScope.IsLoading = true;
        Common.Services.Call($http, {
            url: Common.Services.url.FLM,
            method: _FLMDriverEdit.URL.FeeDefault_Get,
            data: { ID: ID },
            success: function (res) {
                if (Common.HasValue(res)) {
                    $scope.ItemAssetFee = res;
                    win.center().open();
                    $rootScope.IsLoading = false;

                }
            }
        });
    }

    $scope.FeeDefault_Delete_Click = function ($event, data) {
        $event.preventDefault();
        $rootScope.Message({
            Msg: "Xác nhận xóa phụ phí?",
            Type: Common.Message.Type.Confirm,
            Ok: function () {
                $rootScope.IsLoading = true;
                Common.Services.Call($http, {
                    url: Common.Services.url.FLM,
                    method: _FLMDriverEdit.URL.FeeDefault_Delete,
                    data: { ID: data.ID },
                    success: function (res) {
                        if (Common.HasValue(res)) {
                            $rootScope.Message({
                                Msg: 'Đã xóa.',
                                NotifyType: Common.Message.NotifyType.SUCCESS
                            });
                            $scope.FeeDefault_Grid.dataSource.read();
                            $rootScope.IsLoading = false;
                        }
                    }
                });
            }
        })
    };

    $scope.assetFee_Save_Click = function ($event, win, vform) {
        $event.preventDefault();
        if (vform()) {
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.FLM,
                method: _FLMDriverEdit.URL.FeeDefault_Save,
                data: { driverID: _FLMDriverEdit.Param.DriverID, item: $scope.ItemAssetFee },
                success: function (res) {
                    if (Common.HasValue(res)) {
                        $rootScope.Message({
                            Msg: 'Đã cập nhật.',
                            NotifyType: Common.Message.NotifyType.SUCCESS
                        });
                        $scope.FeeDefault_Grid.dataSource.read();
                        $rootScope.IsLoading = false;
                        win.close();
                    }
                }
            });
        }
    }

    $scope.cboTypeOfScheduleFee_Options = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true, dataTextField: 'TypeName', dataValueField: 'ID',
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    TypeName: { type: 'string' },
                }
            }
        }),
        change: function (e) {
        }
    };
    Common.ALL.Get($http, {
        url: Common.ALL.URL.ALL_FLMTypeOfScheduleFee,
        success: function (data) {
            $scope.cboTypeOfScheduleFee_Options.dataSource.data(data);
        }
    })

    //#endregion


   
    $scope.Win_Close = function ($event, win) {
        $event.preventDefault();
        win.close();
    }
}]);

var getClass = function (data) {
    var result = "";

    var field = Common.Date.ToString(data.date, Common.Date.Format.DMY);
    if (Common.HasValue(_FLMDriverEdit.Data.ObjDate[field])) {
        if (_FLMDriverEdit.Data.ObjDate[field].TypeDate == 1)
            result = "workday"
        if (_FLMDriverEdit.Data.ObjDate[field].TypeDate == 2)
            result = "offday"
        if (_FLMDriverEdit.Data.ObjDate[field].TypeDate == 3)
            result = "holiday"
    }

    return result;
}

