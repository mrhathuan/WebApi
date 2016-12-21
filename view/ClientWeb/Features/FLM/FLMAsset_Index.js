
/// <reference path="~/Scripts/jquery-1.9.1.intellisense.js" />
/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />

var _FLMAsset = {
    URL: {
        Truck_Read: 'FLMAsset_Truck_Read',
        Truck_ExportEcxel: 'FLMAsset_Truck_Excel_Export',
        Truck_ImportExcel: 'FLMAsset_Truck_Excel_Save',
        Truck_CheckExcel: 'FLMAsset_Truck_Excel_Check',

        Tractor_Read: 'FLMAsset_Tractor_Read',
        Tractor_ExportEcxel: 'FLMAsset_Tractor_Excel_Export',
        Tractor_ImportExcel: 'FLMAsset_Tractor_Excel_Save',
        Tractor_CheckExcel: 'FLMAsset_Tractor_Excel_Check',

        Tractor_RomoocDefault_List: 'Tractor_RomoocDefault_List',
        Tractor_RomoocDefault_Get: 'Tractor_RomoocDefault_Get',
        Tractor_RomoocDefault_Save: 'Tractor_RomoocDefault_Save',
        Tractor_RomoocDefault_Delete: 'Tractor_RomoocDefault_Delete',
        Tractor_RomoocDefault_TractorList: 'Tractor_RomoocDefault_TractorList',
        Tractor_RomoocDefault_RomoocList: 'Tractor_RomoocDefault_RomoocList',

        Romooc_Read: 'FLMAsset_Romooc_Read',
        Romooc_ExportEcxel: 'FLMAsset_Romooc_Excel_Export',
        Romooc_ImportExcel: 'FLMAsset_Romooc_Excel_Save',
        Romooc_CheckExcel: 'FLMAsset_Romooc_Excel_Check',

        Eqm_Read: 'FLMAsset_Eqm_Read',
        Eqm_ExportEcxel: 'FLMAsset_Eqm_Excel_Export',
        Eqm_ImportExcel: 'FLMAsset_Eqm_Excel_Save',
        Eqm_CheckExcel: 'FLMAsset_Eqm_Excel_Check',

        Container_Read: 'FLMAsset_Container_read',

        Truck_ExcelInit: 'FLMAsset_Truck_ExcelInit',
        Truck_ExcelChange: 'FLMAsset_Truck_ExcelChange',
        Truck_ExcelImport: 'FLMAsset_Truck_ExcelImport',
        Truck_ExcelApprove: 'FLMAsset_Truck_ExcelApprove',

        Tractor_ExcelInit: 'FLMAsset_Tractor_ExcelInit',
        Tractor_ExcelChange: 'FLMAsset_Tractor_ExcelChange',
        Tractor_ExcelImport: 'FLMAsset_Tractor_ExcelImport',
        Tractor_ExcelApprove: 'FLMAsset_Tractor_ExcelApprove',

        Romooc_ExcelInit: 'FLMAsset_Romooc_ExcelInit',
        Romooc_ExcelChange: 'FLMAsset_Romooc_ExcelChange',
        Romooc_ExcelImport: 'FLMAsset_Romooc_ExcelImport',
        Romooc_ExcelApprove: 'FLMAsset_Romooc_ExcelApprove',

        Tractor_RomoocDefault_ExcelInit: 'Tractor_RomoocDefault_ExcelInit',
        Tractor_RomoocDefault_ExcelChange: 'Tractor_RomoocDefault_ExcelChange',
        Tractor_RomoocDefault_ExcelImport: 'Tractor_RomoocDefault_ExcelImport',
        Tractor_RomoocDefault_ExcelApprove: 'Tractor_RomoocDefault_ExcelApprove',
    },
    ExcelKey: {
        ResourceTruck: "FLMAsset_Truck",
        ResourceTractor: "FLMAsset_Tractor",
        ResourceRomooc: "FLMAsset_Romooc",
        ResourceRomoocDefault: "FLMAsset_TractorRomoocDefault",

        Truck: "FLMAsset_Truck",
        Tractor: "FLMAsset_Tractor",
        Romooc: "FLMAsset_Romooc",

        RomoocDefault:"FLMAsset_TractorRomoocDefault",
    }
}

angular.module('myapp').controller('FLMAsset_IndexCtrl', ['$rootScope', '$scope', '$http', '$location', '$state', '$timeout', function ($rootScope, $scope, $http, $location, $state, $timeout) {
    Common.Log('FLMAsset_IndexCtrl');
    $rootScope.IsLoading = false;

    if ($rootScope.IsPageComplete != true) return;
    $scope.Auth = $rootScope.GetAuth();
    $rootScope.Loading.Show('Thông tin phương tiện');

    $scope.ParamEdit = { AssetID: -1 }
    $scope.FLMAsset_ShowBtnExcel = true;
    $scope.TabIndex = 1;
    var LoadingStep = 25;

    $scope.FLMAsset_Excel_Click = function ($event, win, tab) {
        $event.preventDefault();
        var tabIndex = tab.select().index();
        switch (tabIndex) {
            default:
            case 0:
                $rootScope.UploadExcel({
                    Path: Common.FolderUpload.Import,
                    columns: [
                        { field: 'RegNo', title: 'Số xe', width: 120 }
                    ],
                    Download: function () {
                        $rootScope.IsLoading = true;
                        Common.Services.Call($http, {
                            url: Common.Services.url.FLM,
                            method: _FLMAsset.URL.Truck_ExportEcxel,
                            data: {},
                            success: function (res) {
                                $rootScope.IsLoading = false;
                                $rootScope.DownloadFile(res);
                            },
                            error: function (res) {
                                $rootScope.IsLoading = false;
                            }
                        })
                    },
                    Upload: function (e, callback) {
                        $rootScope.IsLoading = true;
                        Common.Services.Call($http, {
                            url: Common.Services.url.FLM,
                            method: _FLMAsset.URL.Truck_CheckExcel,
                            data: { file: e },
                            success: function (data) {
                                $rootScope.IsLoading = false;
                                callback(data);
                            }
                        })
                    },
                    Window: win,
                    Complete: function (e, data) {
                        $rootScope.IsLoading = true;
                        Common.Services.Call($http, {
                            url: Common.Services.url.FLM,
                            method: _FLMAsset.URL.Truck_ImportExcel,
                            data: { lst: data },
                            success: function (res) {
                                $rootScope.IsLoading = false;
                                $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                                $scope.FLMAsset_TruckGrid_Options.dataSource.read();
                            },
                            error: function (res) {
                                $rootScope.IsLoading = false;
                            }
                        })
                    }
                })
                break;
            case 1:
                $rootScope.UploadExcel({
                    Path: Common.FolderUpload.Import,
                    columns: [{ field: 'RegNo', title: 'Số xe', width: '150px' }],
                    Download: function () {

                        $rootScope.IsLoading = true;
                        Common.Services.Call($http, {
                            url: Common.Services.url.FLM,
                            method: _FLMAsset.URL.Tractor_ExportEcxel,
                            data: {},
                            success: function (res) {
                                $rootScope.IsLoading = false;
                                $rootScope.DownloadFile(res);
                            },
                            error: function (res) {
                                $rootScope.IsLoading = false;
                            }
                        })
                    },
                    Upload: function (e, callback) {
                        $rootScope.IsLoading = true;
                        Common.Services.Call($http, {
                            url: Common.Services.url.FLM,
                            method: _FLMAsset.URL.Tractor_CheckExcel,
                            data: { file: e },
                            success: function (data) {
                                $rootScope.IsLoading = false;
                                callback(data);
                            },
                            error: function (res) {
                                $rootScope.IsLoading = false;
                            }
                        })
                    },
                    Window: win,
                    Complete: function (e, data) {
                        $rootScope.IsLoading = true;
                        Common.Services.Call($http, {
                            url: Common.Services.url.FLM,
                            method: _FLMAsset.URL.Tractor_ImportExcel,
                            data: { lst: data },
                            success: function (res) {
                                $rootScope.IsLoading = false;
                                $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                                $scope.FLMAsset_TractorGrid_Options.dataSource.read();
                            },
                            error: function (res) {
                                $rootScope.IsLoading = false;
                            }
                        })
                    }
                })
                break;
            case 2:
                $rootScope.UploadExcel({
                    Path: Common.FolderUpload.Import,
                    columns: [{ field: 'RegNo', title: 'Số xe', width: '150px' }],
                    Download: function () {
                        $rootScope.IsLoading = true;
                        Common.Services.Call($http, {
                            url: Common.Services.url.FLM,
                            method: _FLMAsset.URL.Romooc_ExportEcxel,
                            data: {},
                            success: function (res) {
                                $rootScope.IsLoading = false;
                                $rootScope.DownloadFile(res);
                            },
                            error: function (res) {
                                $rootScope.IsLoading = false;
                            }
                        })
                    },
                    Upload: function (e, callback) {
                        $rootScope.IsLoading = true;
                        Common.Services.Call($http, {
                            url: Common.Services.url.FLM,
                            method: _FLMAsset.URL.Romooc_CheckExcel,
                            data: { file: e },
                            success: function (data) {
                                $rootScope.IsLoading = false;
                                callback(data);
                            }
                        })
                    },
                    Window: win,
                    Complete: function (e, data) {
                        $rootScope.IsLoading = true;
                        Common.Services.Call($http, {
                            url: Common.Services.url.FLM,
                            method: _FLMAsset.URL.Romooc_ImportExcel,
                            data: { lst: data },
                            success: function (res) {
                                $rootScope.IsLoading = false;
                                $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                                $scope.FLMAsset_RomoocGrid_Options.dataSource.read();
                            },
                            error: function (res) {
                                $rootScope.IsLoading = false;
                            }
                        })
                    }
                })
                break;
            case 3: break;
            case 4:
                $rootScope.UploadExcel({
                    Path: Common.FolderUpload.Import,
                    columns: [{ field: 'Code', title: 'Số Part', width: '150px' }, ],
                    Download: function () {
                        $rootScope.IsLoading = true;
                        Common.Services.Call($http, {
                            url: Common.Services.url.FLM,
                            method: _FLMAsset.URL.Eqm_ExportEcxel,
                            data: {},
                            success: function (res) {
                                $rootScope.IsLoading = false;
                                $rootScope.DownloadFile(res);
                            },
                            error: function (res) {
                                $rootScope.IsLoading = false;
                            }
                        })
                    },
                    Upload: function (e, callback) {
                        $rootScope.IsLoading = true;
                        Common.Services.Call($http, {
                            url: Common.Services.url.FLM,
                            method: _FLMAsset.URL.Eqm_CheckExcel,
                            data: { file: e },
                            success: function (data) {
                                $rootScope.IsLoading = false;
                                callback(data);
                            },
                            error: function (res) {
                                $rootScope.IsLoading = false;
                            }
                        })
                    },
                    Window: win,
                    Complete: function (e, data) {
                        $rootScope.IsLoading = true;
                        Common.Services.Call($http, {
                            url: Common.Services.url.FLM,
                            method: _FLMAsset.URL.Eqm_ImportExcel,
                            data: { lst: data },
                            success: function (res) {
                                $rootScope.IsLoading = false;
                                $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                                $scope.FLMAsset_EQMGrid_Options.dataSource.read();
                            },
                            error: function (res) {
                                $rootScope.IsLoading = false;
                            }
                        })
                    }
                })
                break;
        }
    }

    $scope.FLMAsset_AddNew_Click = function ($event, tab) {
        $event.preventDefault();
        $scope.ParamEdit.AssetID = -1;
        var tabIndex = tab.select().index();
        var view = '';
        switch (tabIndex) {
            default:
            case 0: view = "main.FLMAsset.EditTruck"; break;
            case 1: view = "main.FLMAsset.EditTractor"; break;
            case 2: view = "main.FLMAsset.EditRomooc"; break;
            case 3: view = "main.FLMAsset.EditContainer"; break;
            case 4: view = "main.FLMAsset.EditEQM"; break;
        }
        if (view != null)
            $state.go(view, $scope.ParamEdit)
    }

    $scope.FLMAsset_TabOptions = {
        animation: { open: { effects: "fadeIn" } },
        select: function (e) {
            var id = e.item.id;
            switch (id) {
                default: $scope.FLMAsset_ShowBtnExcel = true; break;
                case 'tab_truck': $timeout(function () {
                    $scope.FLMAsset_ShowBtnExcel = true;
                }, 1)
                    break;
                case 'tab_tractor': $timeout(function () {
                    $scope.FLMAsset_ShowBtnExcel = true;
                }, 1)
                    break;
                case 'tab_romooc': $timeout(function () {
                    $scope.FLMAsset_ShowBtnExcel = true;
                }, 1)
                    break;
                case 'tab_cont':
                    $timeout(function () {
                        $scope.FLMAsset_ShowBtnExcel = false;
                    }, 1)
                    break;
                case 'tab_eqm': $timeout(function () {
                    $scope.FLMAsset_ShowBtnExcel = true;
                }, 1)
                    break;
            }
            $timeout(function () {
                $scope.TabIndex = angular.element(e.item).data('tabindex');
                Common.Log($scope.TabIndex);
            }, 1);
        }
    }
    $rootScope.$watch('Loading.Progress', function (v, n) {
        if ($rootScope.Loading.Progress >= 100) {
            $rootScope.Loading.Progress = 0;
            $rootScope.Loading.Hide();
        }
    })
   

    //#region Truck
    $scope.FLMAsset_TruckGrid_Options = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.FLM,
            method: _FLMAsset.URL.Truck_Read,
            model: {
                id: 'ID',
                fields: {
                    ID: { field: 'ID', type: 'number', editable: false, nullable: false },
                    WarrantyEnd: { type: 'date' },
                    IsRent: { type: 'string' },
                    GroupOfVehicleName: { type: 'string' },
                    RegWeight: { type: 'number' },
                    GPSCode:{type:'string'},
                    MinWeight: { type: 'number' },
                    MaxWeight: { type: 'number' },
                    RegCapacity: { type: 'number' },
                    MinCapacity: { type: 'number' },
                    MaxCapacity: { type: 'number' },
                    YearOfProduction: { type: 'number' },
                    URL: { type: 'string' }
                }
            }
        }),
        height: '100%', pageable: true, sortable: true, columnMenu: false, filterable: { mode: 'row' }, resizable: true,
        columns: [
            {
                field: "RegNo", title: 'Số xe', width: 100,
                template: '<a href="\" ng-click="FLMAsset_TruckEdit_Click($event,dataItem)">#=RegNo#</a>',
                filterable: { cell: { showOperators: false, operator: "contains" } }
            },
            {
                title: 'Trọng tải', headerAttributes: { style: "text-align: center;" },
                columns:
                    [
                        {
                            field: "RegWeight", title: 'Đăng kí', width: '100px',
                            template: "#=RegWeight==null?\"\":RegWeight#",
                            filterable: {
                                cell: {
                                    showOperators: false, operator: "eq",
                                    template: function (args) {
                                        args.element.kendoNumericTextBox({
                                            min: 0, decimals: 0,
                                            format: 'n0'
                                        });
                                    },
                                }
                            }
                        },
                        {
                            field: "MinWeight", title: 'Tối thiểu', width: '100px',
                            template: "#=MinWeight==null?\"\":MinWeight#", filterable: { cell: { showOperators: false, operator: "eq" } }
                        },
                        {
                            field: "MaxWeight", title: 'Tối đa', width: '100px',
                            template: "#=MaxWeight==null?\"\":MaxWeight#", filterable: { cell: { showOperators: false, operator: "eq" } }
                        },
                    ]
            },
            {
                title: 'Số khối', headerAttributes: { style: "text-align: center;" },
                columns:
                    [
                        {
                            field: "RegCapacity", title: 'Đăng kí', width: '100px',
                            template: "#=RegCapacity==null?\"\":RegCapacity#", filterable: { cell: { showOperators: false, operator: "eq" } }
                        },
                        {
                            field: "MinCapacity", title: 'Tối thiểu', width: '100px',
                            template: "#=MinCapacity==null?\"\":MinCapacity#", filterable: { cell: { showOperators: false, operator: "eq" } }
                        },
                        {
                            field: "MaxCapacity", title: 'Tối đa', width: '100px',
                            template: "#=MaxCapacity==null?\"\":MaxCapacity#", filterable: { cell: { showOperators: false, operator: "eq" } }
                        },

                    ]
            },
            {
                field: "IsRent", title: 'Cho thuê', width: 82, attributes: { style: "text-align: center;" },
                template: '<input type="checkbox" #= IsRent=="true" ? "checked=checked" : "" # disabled="disabled" />',
                filterable: {
                    cell: {
                        template: function (container) {
                            container.element.kendoComboBox({
                                dataSource: [{ Text: 'Sai', Value: false }, { Text: 'Đúng', Value: true }, { Text: 'Tất cả', Value: '' }],
                                dataTextField: "Text", dataValueField: "Value"
                            });
                        },
                        showOperators: false
                    }
                }
            },
            {
                field: 'GroupOfVehicleName', title: 'Phân loại', width: 100, headerAttributes: { style: "text-align: center;" },
                template: "#=GroupOfVehicleName==null?\"\":GroupOfVehicleName#",
                filterable: { cell: { showOperators: false, operator: "contains" } }
            },
            {
                field: 'GPSCode', title: 'Mã GPS', width: 100, headerAttributes: { style: "text-align: center;" },
                template: "#=GPSCode==null?\"\":GPSCode#",
                filterable: { cell: { showOperators: false, operator: "contains" } }
            },
            {
                field: 'YearOfProduction', title: 'Năm sản xuất', width: 110, headerAttributes: { style: "text-align: center;" },
                filterable: { cell: { showOperators: false, operator: "eq" } }
            },
            {
                field: 'Manufactor', title: 'Nhà sản xuất', width: 110,
                template: "#=Manufactor==null?\"\":Manufactor#", filterable: { cell: { showOperators: false, operator: "contains" } }
            },
            {
                field: 'WarrantyEnd', title: 'Ngày kết thúc BH', width: '145px', headerAttributes: { style: "text-align: center" },
                template: "#=WarrantyEnd==null?\"\":kendo.toString(WarrantyEnd,'dd/MM/yyyy')#",
                filterable: { cell: { template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); }, operator: 'equal', showOperators: false } },
            },
            { title: "", sortable: false, filterable: false }
        ],
        dataBound: function (e) {
            $rootScope.Loading.Change("Thông tin phương tiện xe tải ...", $rootScope.Loading.Progress + LoadingStep);
        }
    }

    $scope.FLMAsset_TruckEdit_Click = function ($event, data) {
        $event.preventDefault();
        $scope.ParamEdit.AssetID = data.ID;
        $state.go('main.FLMAsset.EditTruck', $scope.ParamEdit)
    }

    $scope.FLMAsset_Truck_ExcelOnlClick = function ($event, grid) {
        $event.preventDefault();

        var lstMessageError = [];
        for (var i = 0; i < 30; i++) {
            var resource = $rootScope.RS[_FLMAsset.ExcelKey.ResourceTruck + '_' + i];
            if (Common.HasValue(resource))
                lstMessageError.push(resource);
        }
        if (lstMessageError.length == 0) {
            lstMessageError = [
                '[Số xe] không được trống và > 50 ký tự',
                '[Số xe] nhập sai. VD: 20C-1234, 52EC-11223',
                '[Số xe] đã bị trùng',
                '[Số xe] đã sử dụng cho loại khác',
                '[Trọng tải - Đăng kí] nhập sai',
                '[Trọng tải - Tối thiểu] nhập sai',
                '[Trọng tải - Tối Đa] nhập sai',
                '[Số khối - Đăng kí] nhập sai',
                '[Số khối - Tối thiểu] nhập sai',
                '[Số khối - Tối Đa] nhập sai',
                '[Trọng tải không chở hàng] nhập sai',
                '[Phân loại] không tồn tại',
                '[Tài xế] không tồn tại',
                '[Phụ lái] không tồn tại',
                '[Năm sản xuất] không được > 50 ký tự',
                '[Nhà sản xuất] không được > 500 ký tự',
                '[Giá trị ban đầu] nhập sai',
                '[Giá trị hiện tại] nhập sai',
                '[T/g k.hao(tháng)] nhập sai',
                '[T/g bắt đầu tính KH] nhập sai',
                '[T/g b.hành (tháng)] nhập sai',
                '[Ngày kết thúc BH] nhập sai',
                '[Thông số kỹ thuật] nhập sai',
                '[Nhiệt độ thấp nhất] nhập sai',
                '[Nhiệt độ cao nhất] nhập sai',
                '[Mã GPS] không được > 50 ký tự',
            ];
        }

        $rootScope.excelShare.Init({
            functionkey: _FLMAsset.ExcelKey.Truck,
            params: { },
            rowStart: 2,
            colCheckChange: 30,
            url: Common.Services.url.FLM,
            methodInit: _FLMAsset.URL.Truck_ExcelInit,
            methodChange: _FLMAsset.URL.Truck_ExcelChange,
            methodImport: _FLMAsset.URL.Truck_ExcelImport,
            methodApprove: _FLMAsset.URL.Truck_ExcelApprove,
            lstMessageError: lstMessageError,

            Approved: function () {
                $scope.FLMAsset_TruckGrid_Options.dataSource.read();
            }
        });
    };

    $scope.FLMAsset_Tractor_ExcelOnlClick = function ($event, grid) {
        $event.preventDefault();

        var lstMessageError = [];
        for (var i = 0; i < 30; i++) {
            var resource = $rootScope.RS[_FLMAsset.ExcelKey.ResourceTractor + '_' + i];
            if (Common.HasValue(resource))
                lstMessageError.push(resource);
        }
        if (lstMessageError.length == 0) {
            lstMessageError = [
                '[Số xe] không được trống và > 50 ký tự',
                '[Số xe] nhập sai. VD: 20C-1234, 52EC-11223',
                '[Số xe] đã bị trùng',
                '[Số xe] đã sử dụng cho loại khác',
                '[Trọng tải - Đăng kí] nhập sai',
                '[Trọng tải - Tối thiểu] nhập sai',
                '[Trọng tải - Tối Đa] nhập sai',
                '[Trọng tải không chở hàng] nhập sai',
                '[Phân loại] không tồn tại',
                '[Tài xế] không tồn tại',
                '[Phụ lái] không tồn tại',
                '[Romooc mặc định] không tồn tại',
                '[Năm sản xuất] không được > 50 ký tự',
                '[Nhà sản xuất] không được > 500 ký tự',
                '[Giá trị ban đầu] nhập sai',
                '[Giá trị hiện tại] nhập sai',
                '[T/g k.hao(tháng)] nhập sai',
                '[T/g bắt đầu tính KH] nhập sai',
                '[T/g b.hành (tháng)] nhập sai',
                '[Ngày kết thúc BH] nhập sai',
                '[Thông số kỹ thuật] nhập sai',
                '[Mã GPS] không được > 50 ký tự',
            ];
        }

        $rootScope.excelShare.Init({
            functionkey: _FLMAsset.ExcelKey.Tractor,
            params: {},
            rowStart: 2,
            colCheckChange: 30,
            url: Common.Services.url.FLM,
            methodInit: _FLMAsset.URL.Tractor_ExcelInit,
            methodChange: _FLMAsset.URL.Tractor_ExcelChange,
            methodImport: _FLMAsset.URL.Tractor_ExcelImport,
            methodApprove: _FLMAsset.URL.Tractor_ExcelApprove,
            lstMessageError: lstMessageError,

            Approved: function () {
                $scope.FLMAsset_TractorGrid_Options.dataSource.read();
            }
        });
    };

    $scope.FLMAsset_Romooc_ExcelOnlClick = function ($event, grid) {
        $event.preventDefault();

        var lstMessageError = [];
        for (var i = 0; i < 30; i++) {
            var resource = $rootScope.RS[_FLMAsset.ExcelKey.ResourceRomooc + '_' + i];
            if (Common.HasValue(resource))
                lstMessageError.push(resource);
        }
        if (lstMessageError.length == 0) {
            lstMessageError = [
                '[Số mooc] không được trống và > 50 ký tự',
                '[Số mooc] nhập sai. VD: 20C-1234, 52EC-11223',
                '[Số mooc] đã bị trùng',
                '[Phân loại] không tồn tại',
                '[Trọng tải] nhập sai',
                '[SL Con.20DC tối đa] nhập sai',
                '[Mooc sàn] nhập sai',
                '[Năm sản xuất] không được > 50 ký tự',
                '[Nhà sản xuất] không được > 500 ký tự',
                '[Giá trị ban đầu] nhập sai',
                '[Giá trị hiện tại] nhập sai',
                '[T/g k.hao(tháng)] nhập sai',
                '[T/g bắt đầu tính KH] nhập sai',
                '[T/g b.hành (tháng)] nhập sai',
                '[Ngày kết thúc BH] nhập sai',
                '[Thông số kỹ thuật] nhập sai'
            ];
        }

        $rootScope.excelShare.Init({
            functionkey: _FLMAsset.ExcelKey.Romooc,
            params: {},
            rowStart: 1,
            colCheckChange: 30,
            url: Common.Services.url.FLM,
            methodInit: _FLMAsset.URL.Romooc_ExcelInit,
            methodChange: _FLMAsset.URL.Romooc_ExcelChange,
            methodImport: _FLMAsset.URL.Romooc_ExcelImport,
            methodApprove: _FLMAsset.URL.Romooc_ExcelApprove,
            lstMessageError: lstMessageError,

            Approved: function () {
                $scope.FLMAsset_RomoocGrid_Options.dataSource.read();
            }
        });
    };

    //#endregion

    //#region track
    $scope.FLMAsset_TractorGrid_Options = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.FLM,
            method: _FLMAsset.URL.Tractor_Read,
            model: {
                id: 'ID',
                fields: {
                    ID: { field: 'ID', type: 'number', editable: false, nullable: false },
                    WarrantyEnd: { type: 'date' },
                    IsOwn: { type: 'string' },
                    IsRent: { type: 'string' },
                    GroupOfVehicleName: { type: 'string' },
                    RegWeight: { type: 'number' },
                    MinWeight: { type: 'number' },
                    MaxWeight: { type: 'number' },
                    YearOfProduction: { type: 'number' },
                    GPSCode: { type: 'string' },
                }
            }
        }),
        height: '100%', pageable: true, sortable: true, columnMenu: false, filterable: { mode: 'row' }, resizable: true,
        dataBound: function () {
            $rootScope.Loading.Change("Thông tin phương tiện đầu kéo ...", $rootScope.Loading.Progress + LoadingStep);
        },
        columns: [
            {
                field: "RegNo", title: 'Số xe', width: 100,
                template: '<a href="\" ng-click="FLMAsset_TractorEdit_Click($event,dataItem)">#=RegNo#</a>',
                filterable: { cell: { showOperators: false, operator: "contains" } }
            },
            {
                field: "RegWeight", title: 'Trọng tải đăng kí', width: 120,
                template: "#=RegWeight==null?\"\":RegWeight#", filterable: { cell: { showOperators: false, operator: "eq" } }
            },
            {
                field: "MinWeight", title: 'Trọng tải tối thiểu', width: 120,
                template: "#=MinWeight==null?\"\":MinWeight#", filterable: { cell: { showOperators: false, operator: "eq" } }
            },
            {
                field: "MaxWeight", title: 'Trọng tải tối đa', width: 120,
                template: "#=MaxWeight==null?\"\":MaxWeight#", filterable: { cell: { showOperators: false, operator: "eq" } }
            },
            {
                field: "IsRent", title: 'Cho thuê', width: 82, attributes: { style: "text-align: center;" },
                template: '<input type="checkbox" #= IsRent=="true" ? "checked=checked" : "" # disabled="disabled" />',
                filterable: {
                    cell: {
                        template: function (container) {
                            container.element.kendoComboBox({
                                dataSource: [{ Text: 'Sai', Value: false }, { Text: 'Đúng', Value: true }, { Text: 'Tất cả', Value: '' }],
                                dataTextField: "Text", dataValueField: "Value"
                            });
                        },
                        showOperators: false
                    }
                }
            },
            {
                field: 'GroupOfVehicleName', title: 'Phân loại', width: 100, headerAttributes: { style: "text-align: center;" },
                template: "#=GroupOfVehicleName==null?\"\":GroupOfVehicleName#",
                filterable: { cell: { showOperators: false, operator: "contains" } }
            },
            {
                field: 'GPSCode', title: 'Mã GPS', width: 100, headerAttributes: { style: "text-align: center;" },
                template: "#=GPSCode==null?\"\":GPSCode#",
                filterable: { cell: { showOperators: false, operator: "contains" } }
            },
            {
                field: 'YearOfProduction', title: 'Năm sản xuất', width: 110, headerAttributes: { style: "text-align: center;" },
                filterable: { cell: { showOperators: false, operator: "eq" } }
            },
            {
                field: 'Manufactor', title: 'Nhà sản xuất', width: 110,
                template: "#=Manufactor==null?\"\":Manufactor#", filterable: { cell: { showOperators: false, operator: "contains" } }
            },
            {
                field: 'WarrantyEnd', title: 'Ngày kết thúc BH', width: '145px', headerAttributes: { style: "text-align: center;" },
                template: "#=WarrantyEnd==null?\"\":kendo.toString(WarrantyEnd,'dd/MM/yyyy')#",
                filterable: { cell: { template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); }, operator: 'equal', showOperators: false } },
            },
            { title: "", sortable: false, filterable: false }
        ],
    }

    $scope.FLMAsset_TractorEdit_Click = function ($event, data) {
        $event.preventDefault();
        $scope.ParamEdit.AssetID = data.ID;
        $state.go('main.FLMAsset.EditTractor', $scope.ParamEdit)
    }
    //#endregion

    //#region Romooc
    $scope.FLMAsset_RomoocGrid_Options = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.FLM,
            method: _FLMAsset.URL.Romooc_Read,
            model: {
                id: 'ID',
                fields: {
                    ID: { field: 'ID', type: 'number', editable: false, nullable: false },
                    WarrantyEnd: { type: 'date' },
                    IsRent: { type: 'string' },
                    GroupOfVehicleName: { type: 'string' },
                    MaxWeight: { type: 'number' },
                    //RegCapacity: { type: 'number' },
                    YearOfProduction: { type: 'number' },
                    //IsFloor: { type: 'string' },
                }
            }
        }),
        height: '100%', pageable: true, sortable: true, columnMenu: false, filterable: { mode: 'row' }, resizable: true,
        dataBound: function () {
            $rootScope.Loading.Change("Thông tin phương tiện Romooc ...", $rootScope.Loading.Progress + LoadingStep);
        },
        columns: [
            {
                field: "RegNo", title: 'Số xe', width: 100,
                template: '<a href="\" ng-click="FLMAsset_RomoocEdit_Click($event,dataItem)">#=RegNo#</a>',
                filterable: { cell: { showOperators: false, operator: "contains" } }
            },
            {
                field: "MaxWeight", title: 'Tải trọng', width: '100px',
                template: "#=MaxWeight==null?\"\":MaxWeight#", filterable: { cell: { showOperators: false, operator: "eq" } }
            },
            //{
            //    field: "RegCapacity", title: 'SL Cont20 tối đa', width: '100px',
            //    template: "#=RegCapacity==null?\"\":RegCapacity#", filterable: { cell: { showOperators: false, operator: "eq" } }
            //},
            {
                field: "IsRent", title: 'Cho thuê', width: 82, attributes: { style: "text-align: center;" },
                template: '<input type="checkbox" #= IsRent=="true" ? "checked=checked" : "" # disabled="disabled" />',
                filterable: {
                    cell: {
                        template: function (container) {
                            container.element.kendoComboBox({
                                dataSource: [{ Text: 'Sai', Value: false }, { Text: 'Đúng', Value: true }, { Text: 'Tất cả', Value: '' }],
                                dataTextField: "Text", dataValueField: "Value"
                            });
                        },
                        showOperators: false
                    }
                }
            },
            {
                field: 'GroupOfRomoocName', title: 'Phân loại', width: 100, headerAttributes: { style: "text-align: center;" },
                template: "#=GroupOfRomoocName==null?\"\":GroupOfRomoocName#",
                filterable: { cell: { showOperators: false, operator: "contains" } }
            },
            
            {
                field: 'YearOfProduction', title: 'Năm sản xuất', width: 110, headerAttributes: { style: "text-align: center;" },
                filterable: { cell: { showOperators: false, operator: "eq" } }
            },
            {
                field: 'Manufactor', title: 'Nhà sản xuất', width: 110,
                template: "#=Manufactor==null?\"\":Manufactor#", filterable: { cell: { showOperators: false, operator: "contains" } }
            },
            {
                field: 'WarrantyEnd', title: 'Ngày kết thúc BH', width: '145px', headerAttributes: { style: "text-align: center;" },
                template: "#=WarrantyEnd==null?\"\":kendo.toString(WarrantyEnd,'dd/MM/yyyy')#",
                filterable: { cell: { template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); }, operator: 'equal', showOperators: false } },
            },
            //{
            //    field: "IsFloor", title: 'Mooc sàn', width: '120px', attributes: { style: "text-align: center;" },
            //    template: '<input type="checkbox" #= IsFloor=="true" ? "checked=checked" : "" # disabled="disabled" />',
            //    attributes: { style: "text-align: center; " },
            //    filterable: {
            //        cell: {
            //            template: function (container) {
            //                container.element.kendoComboBox({
            //                    dataSource: [{ Text: 'Chưa mooc sàn', Value: false }, { Text: 'Đã mooc sàn', Value: true }, { Text: 'Tất cả', Value: '' }],
            //                    dataTextField: "Text", dataValueField: "Value",
            //                });
            //            }, showOperators: false
            //        }
            //    }
            //},
            { title: "", sortable: false, filterable: false }
        ]
    }

    $scope.FLMAsset_RomoocEdit_Click = function ($event, data) {
        $event.preventDefault();
        $scope.ParamEdit.AssetID = data.ID;
        $state.go('main.FLMAsset.EditRomooc', $scope.ParamEdit)
    }
    //#endregion

    //#region Container
    $scope.FLMAsset_ContGrid_Options = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.FLM,
            method: _FLMAsset.URL.Container_Read,
            model: {
                id: 'ID',
                fields: {
                    ID: { field: 'ID', type: 'number', editable: false, nullable: false },
                    IsRent: { type: 'string' },
                    PackageName: { type: 'string' },
                    Width: { type: 'number' },
                    Length: { type: 'number' },
                    Height: { type: 'number' },
                    Capacity: { type: 'number' },
                    YearOfProduction: { type: 'number' },
                }
            }
        }),
        height: '100%', pageable: true, sortable: true, columnMenu: false, filterable: { mode: 'row' }, resizable: true,
        columns: [
            {
                field: "ContainerNo", title: 'Số Con.', width: 100,
                template: '<a href="\" ng-click="FLMAsset_ContainerEdit_Click($event,dataItem)">#=ContainerNo#</a>',
                filterable: { cell: { showOperators: false, operator: "contains" } }
            },
            {
                field: "PackageName", title: 'Loại Con.', width: '100px',
                template: "#=PackageName==null?\"\":PackageName#", filterable: { cell: { showOperators: false, operator: "contains" } }
            },
            {
                field: "IsRent", title: 'Cho thuê', width: 82, attributes: { style: "text-align: center;" },
                template: '<input type="checkbox" #= IsRent=="true" ? "checked=checked" : "" # disabled="disabled" />',
                filterable: {
                    cell: {
                        template: function (container) {
                            container.element.kendoComboBox({
                                dataSource: [{ Text: 'Sai', Value: false }, { Text: 'Đúng', Value: true }, { Text: 'Tất cả', Value: '' }],
                                dataTextField: "Text", dataValueField: "Value"
                            });
                        },
                        showOperators: false
                    }
                }
            },
            {
                field: 'Length', title: 'Chiều dài', width: '100px',
                template: "#=Length==null?\"\":Length#", filterable: { cell: { showOperators: false, operator: "eq" } }
            },
            {
                field: 'Width', title: 'Chiều rộng', width: '100px',
                template: "#=Width==null?\"\":Width#", filterable: { cell: { showOperators: false, operator: "eq" } }
            },
            {
                field: 'Height', title: 'Trọng tải', width: '100px',
                template: "#=Height==null?\"\":Height#", filterable: { cell: { showOperators: false, operator: "eq" } }
            },
            {
                field: 'Capacity', title: 'SL Con.20 tối đa', width: '150px',
                template: "#=Capacity==null?\"\":Capacity#", filterable: { cell: { showOperators: false, operator: "eq" } }
            },
            {
                field: 'Manufactor', title: 'Nhà sản xuất', width: 110,
                template: "#=Manufactor==null?\"\":Manufactor#", filterable: { cell: { showOperators: false, operator: "contains" } }
            },
            { title: "", sortable: false, filterable: false }
        ],
        dataBound: function () {
            $rootScope.Loading.Change("Thông tin phương tiện Container ...", $rootScope.Loading.Progress + LoadingStep);
        },
    }

    $scope.FLMAsset_ContainerEdit_Click = function ($event, data) {
        $event.preventDefault();
        $scope.ParamEdit.AssetID = data.ID;
        $state.go('main.FLMAsset.EditContainer', $scope.ParamEdit)
    }
    //#endregion

    //#region Device
    $scope.FLMAsset_EQMGrid_Options = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.FLM,
            method: _FLMAsset.URL.Eqm_Read,
            model: {
                id: 'ID',
                fields: {
                    ID: { field: 'ID', type: 'number', editable: false, nullable: false },
                    IsRent: { type: 'string' }
                }
            }
        }),
        height: '100%', pageable: true, sortable: true, columnMenu: false, filterable: { mode: 'row' }, resizable: true,
        dataBound: function () {
        },
        columns: [
               {
                   field: "Code", title: 'Số Part.', width: '120px',
                   template: '<a href="\" ng-click="FLMAsset_EQMEdit_Click($event,dataItem)">#=Code#</a>',
                   filterable: { cell: { showOperators: false, operator: "contains" } }
               },
               {
                   field: "Name", title: 'Thiết bị', width: '120px',
                   template: "#=Name==null?\"\":Name#", filterable: { cell: { showOperators: false, operator: "contains" } }
               },
               {
                   field: "CurrentLocation", title: 'Vị trí hiện tại', width: '200px', filterable: { cell: { showOperators: false, operator: "contains" } }
               },
               {
                   field: "GroupOfEquipmentName", title: 'Nhóm thiết bị', width: '120px', filterable: { cell: { showOperators: false, operator: "contains" } }
               },
               {
                   field: "IsRent", title: 'Cho thuê', width: 82, attributes: { style: "text-align: center;" },
                   template: '<input type="checkbox" #= IsRent=="true" ? "checked=checked" : "" # disabled="disabled" />',
                   filterable: {
                       cell: {
                           template: function (container) {
                               container.element.kendoComboBox({
                                   dataSource: [{ Text: 'Sai', Value: false }, { Text: 'Đúng', Value: true }, { Text: 'Tất cả', Value: '' }],
                                   dataTextField: "Text", dataValueField: "Value"
                               });
                           },
                           showOperators: false
                       }
                   }
               },
               { field: "RentName", title: 'Cty Cho thuê', width: '200px', filterable: { cell: { showOperators: false, operator: "contains" } } },
               { title: "", sortable: false, filterable: false }
        ]
    }

    $scope.FLMAsset_EQMEdit_Click = function ($event, data) {
        $event.preventDefault();
        $scope.ParamEdit.AssetID = data.ID;
        $state.go('main.FLMAsset.EditEQM', $scope.ParamEdit)
    }
    //#endregion

    //#region Action
    $scope.ShowSetting = function ($event, grid) {
        $event.preventDefault();

        $rootScope.ShowSetting({
            ListView: [],
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

    //#region romooc default
    $scope.FLMAsset_Tractor_defaultRomoocClick = function ($event, win,grid) {
        $event.preventDefault();
        win.center();
        win.open();
        grid.dataSource.read();
    };

    $scope.defaultRomooc_GridOPtions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.FLM,
            method: _FLMAsset.URL.Tractor_RomoocDefault_List,
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number', editable: false, nullable: false },
                    SortOrder: { type: 'number' }
                }
            }
        }),
        height: '100%', pageable: true, sortable: true, columnMenu: false, filterable: { mode: 'row' }, resizable: true,
        dataBound: function () {
        },
        columns: [
            {
                title: ' ', width: '90px',
                template: '<a href="/" ng-click="defaultRomooc_EditClick($event,NewdefaultRomooc_win,dataItem)" class="k-button"><i class="fa fa-pencil"></i></a>'+
                   '<a href="/" ng-click="defaultRomooc_DeleteClick($event,defaultRomooc_Grid,dataItem)" class="k-button"><i class="fa fa-trash"></i></a>',
                filterable: false, sortable: false
            },
            { field: "VehicleNo", title: 'Số xe', width: 100, filterable: { cell: { showOperators: false, operator: "contains" } } },
            { field: "RomoocNo", title: 'Số romooc', width: 100, filterable: { cell: { showOperators: false, operator: "contains" } } },
            { field: "SortOrder", title: 'Độ ưu tiên', width: 100, filterable: { cell: { showOperators: false, operator: "eq" } } },
            { title: "", sortable: false, filterable: false }
        ]
    }

    $scope.defaultRomooc_EditClick = function ($event, win,data) {
        $event.preventDefault();
        $scope.LoadItemRomoocDefault(win,data.ID)
    };

    $scope.defaultRomooc_AddClick = function ($event, win) {
        $event.preventDefault();
        $scope.LoadItemRomoocDefault(win, 0)
    };

    $scope.LoadItemRomoocDefault = function (win, id) {
        $rootScope.IsLoading = true;
        Common.Services.Call($http, {
            url: Common.Services.url.FLM,
            method: _FLMAsset.URL.Tractor_RomoocDefault_Get,
            data: { id:id },
            success: function (res) {
                $rootScope.IsLoading = false;
                $scope.Item = res;
                win.center();
                win.open();
            },
            error: function (res) {
                $rootScope.IsLoading = false;
            }
        });
    };

    $scope.defaultRomooc_DeleteClick = function ($event, grid,data) {
        $event.preventDefault();
        $rootScope.Message({
            Type: Common.Message.Type.Confirm,
            NotifyType: Common.Message.NotifyType.SUCCESS,
            Title: 'Thông báo',
            Msg: 'Bạn muốn xóa dữ liệu đã chọn?',
            Close: null,
            Ok: function () {
                $rootScope.IsLoading = true;
                Common.Services.Call($http, {
                    url: Common.Services.url.FLM,
                    method: _FLMAsset.URL.Tractor_RomoocDefault_Delete,
                    data: { id:data.ID },
                    success: function (res) {
                        $rootScope.IsLoading = false;
                        $rootScope.Message({ Msg: 'Đã xóa', Type: Common.Message.Type.Notify, NotifyType: Common.Message.NotifyType.SUCCESS });
                        grid.dataSource.read();

                    },
                    error: function (res) {
                        $rootScope.IsLoading = false;
                    }
                });
            }
        })
    };

    $scope.cboTractor_Options = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true, dataTextField: 'RegNo', dataValueField: 'VehicleID',
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'VehicleID',
                fields: {
                    VehicleID: { type: 'number' },
                    RegNo: { type: 'string' },
                }
            }
        })
    }

    Common.Services.Call($http, {
        url: Common.Services.url.FLM,
        method: _FLMAsset.URL.Tractor_RomoocDefault_TractorList,
        data: {  },
        success: function (res) {
            $scope.cboTractor_Options.dataSource.data(res);
        }
    });

    $scope.cboRomooc_Options = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true, dataTextField: 'RegNo', dataValueField: 'RomoocID',
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'RomoocID',
                fields: {
                    RomoocID: { type: 'number' },
                    RegNo: { type: 'string' },
                }
            }
        })
    }

    Common.Services.Call($http, {
        url: Common.Services.url.FLM,
        method: _FLMAsset.URL.Tractor_RomoocDefault_RomoocList,
        data: {},
        success: function (res) {
            $scope.cboRomooc_Options.dataSource.data(res);
        }
    });

    $scope.numSortOrder_options = { format: 'n0', spinners: false, culture: 'en-US', step: 1, decimals: 0, }

    $scope.NewdefaultRomooc_win_SaveClick = function ($event, win,vform,grid) {
        $event.preventDefault();
        if (vform()) {
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.FLM,
                method: _FLMAsset.URL.Tractor_RomoocDefault_Save,
                data: { item:$scope.Item },
                success: function (res) {
                    $rootScope.IsLoading = false;
                    $rootScope.Message({ Msg: 'Đã cập nhật', Type: Common.Message.Type.Notify, NotifyType: Common.Message.NotifyType.SUCCESS });
                    grid.dataSource.read();
                    win.close();
                },
                error: function (res) {
                    $rootScope.IsLoading = false;
                }
            });
        }
    };

    $scope.defaultRomooc_ExcelClick = function ($event, grid) {
        $event.preventDefault();
        var lstMessageError = [];
        for (var i = 0; i < 6; i++) {
            var resource = $rootScope.RS[_FLMAsset.ExcelKey.ResourceRomoocDefault + '_' + i];
            if (Common.HasValue(resource))
                lstMessageError.push(resource);
        }
        if (lstMessageError.length == 0) {
            lstMessageError = [
                '[Số xe] không được trống',
                '[Số xe] không có trong xe nhà',
                '[Số romooc] không được trống',
                '[Số romooc] không có trong romooc nhà',
                '[Độ ưu tiên] không được trống',
                '[Độ ưu tiên] nhập sai',
                '[Số xe - Số romooc - Độ ưu tiên] bị trùng trên file',
            ];
        }

        $rootScope.excelShare.Init({
            functionkey: _FLMAsset.ExcelKey.RomoocDefault,
            params: {},
            rowStart: 1,
            colCheckChange: 5,
            url: Common.Services.url.FLM,
            methodInit: _FLMAsset.URL.Tractor_RomoocDefault_ExcelInit,
            methodChange: _FLMAsset.URL.Tractor_RomoocDefault_ExcelChange,
            methodImport: _FLMAsset.URL.Tractor_RomoocDefault_ExcelImport,
            methodApprove: _FLMAsset.URL.Tractor_RomoocDefault_ExcelApprove,
            lstMessageError: lstMessageError,

            Approved: function () {
                grid.dataSource.read();
            }
        });
    };
    //#endregion
}])