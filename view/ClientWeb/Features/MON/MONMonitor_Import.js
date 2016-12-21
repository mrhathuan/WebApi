/// <reference path="~/Scripts/jquery-1.9.1.intellisense.js" />
/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />

//#region URL

var _MONMonitor_Import = {
    URL: {
        Template_List: 'MONImport_Index_Setting_List',
        Template_Download: 'MONImport_Index_Setting_Download',
        Import: 'MONImport_Excel_Import',
        Check: 'MONImport_Excel_Check'
    },
    Data: {
        FromFile: [],
    },
}

//#endregion

angular.module('myapp').controller('MONMonitor_ImportCtrl', ['$rootScope', '$scope', '$http', '$location', '$state', '$timeout', function ($rootScope, $scope, $http, $location, $state, $timeout) {
    Common.Log('MONMonitor_ImportCtrl');
    $rootScope.IsLoading = false;

    $scope.Item = {
        DateFrom: Common.Date.AddDay(new Date(), -5),
        DateTo: new Date(),
    }

    $scope.HasTemplate = false;
    $scope.Template = {
        ID: -1, Name: '', CustomerName: '', CustomerID: -1
    }
    $scope.IsExcelChecked = false;

    $scope.excel_gridOptions = {
        dataSource: Common.DataSource.Local({
            data: [],
            pageSize: 0,
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number', editable: false },
                    ExcelSuccess: { type: 'boolean' }
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
                field: 'ExcelError', width: '250px', title: 'Thông báo', filterable: false,
            },
            {
                field: 'ID', width: '100px', title: 'Số thứ tự', filterable: { cell: { operator: 'contains', showOperators: false } },
            },
            {
                field: 'CustomerCode', width: '120px', title: 'Mã KH', filterable: { cell: { operator: 'contains', showOperators: false } },
            },
            {
                field: 'SOCode', width: '100px', title: 'Số SO', filterable: { cell: { operator: 'contains', showOperators: false } },
            },
            {
                field: 'DNCode', width: '100px', title: 'Số DN', filterable: { cell: { operator: 'contains', showOperators: false } },
            },
            {
                field: 'RegNo', width: '100px', title: 'Xe', filterable: { cell: { operator: 'contains', showOperators: false } },
            },
            {
                field: 'RequestDate', width: '130px', title: 'Ngày gửi y/c', template: "#=RequestDate==null?' ':Common.Date.FromJsonDMY(RequestDate)#",
                filterable: {
                    cell: {
                        template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); },
                        operator: 'equal', showOperators: false,
                    }
                },
            },
            {
                field: 'LocationFromCode', width: '100px', title: 'Mã kho', filterable: { cell: { operator: 'contains', showOperators: false } },
            },
            {
                field: 'LocationToAddress', width: '250px', title: 'Đ/c giao', filterable: { cell: { operator: 'contains', showOperators: false } },
            },
            {
                field: 'Ton', width: '100px', title: 'Tấn', filterable: { cell: { operator: 'equal', showOperators: false } },
            },
            {
                field: 'CBM', width: '100px', title: 'Khối', filterable: { cell: { operator: 'equal', showOperators: false } },
            },
            {
                field: 'Quantity', width: '100px', title: 'Số lượng', filterable: { cell: { operator: 'equal', showOperators: false } },
            },
            {
                field: 'TonTranfer', width: '100px', title: 'Tấn lấy', filterable: { cell: { operator: 'equal', showOperators: false } },
            },
            {
                field: 'CBMTranfer', width: '100px', title: 'Khối lấy', filterable: { cell: { operator: 'equal', showOperators: false } },
            },
            {
                field: 'QuantityTranfer', width: '100px', title: 'Số lượng lấy', filterable: { cell: { operator: 'equal', showOperators: false } },
            },
            {
                field: 'TonBBGN', width: '100px', title: 'Tấn giao', filterable: { cell: { operator: 'equal', showOperators: false } },
            },
            {
                field: 'CBMBBGN', width: '100px', title: 'Khối giao', filterable: { cell: { operator: 'equal', showOperators: false } },
            },
            {
                field: 'QuantityBBGN', width: '100px', title: 'Số lượng giao', filterable: { cell: { operator: 'equal', showOperators: false } },
            },
            { title: '', filterable: false, sortable: false }
        ],
        dataBound: function (e) {
        }
    }

    $scope.excel_template_gridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.MON,
            method: _MONMonitor_Import.URL.Template_List,
            pageSize: 0,
            model: {
                id: 'SettingID',
                fields: {
                    SettingID: { type: 'number' }
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

                        $scope.Template.ID = item.SettingID;
                        $scope.Template.Name = item.Name;
                        $scope.Template.CustomerName = item.SettingCustomerName;
                        $scope.Template.CustomerID = item.CustomerID;


                        $scope.template_win.close();
                    }, 1)
                }
            })
        },
        columns: [
            {
                field: 'Name', title: 'Tên', sortable: true,
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'SettingCustomerName', title: 'Khách hàng', sortable: true, width: "150px",
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                title: "", sortable: false, filterable: false,
            }
        ]
    }

    $scope.Template_Click = function ($event, win) {
        if (Common.HasValue($event))
            $event.preventDefault();

        $scope.excel_template_gridOptions.dataSource.read();
        if (win.wrapper.length > 0)
            win.center().open();
    }

    $scope.Excel_Template_Accept_Click = function ($event, win, grid) {
        $event.preventDefault();

        $scope.HasTemplate = false;
        $scope.Template = {
            ID: -1, Name: '', CustomerName: '', CustomerID: -1
        }

        var item = grid.dataItem(grid.select());
        if (Common.HasValue(item)) {
            $scope.HasTemplate = true;

            $scope.Template.ID = item.SettingID;
            $scope.Template.Name = item.Name;
            $scope.Template.CustomerName = item.SettingCustomerName;
            $scope.Template.CustomerID = item.CustomerID;
        }

        win.close();
    }

    $scope.UpFile_Click = function ($event, file) {
        $event.preventDefault();

        $timeout(function () {
            angular.element(file.element[0]).trigger('click');
        }, 1);
    }

    $scope.DownFile_Click = function ($event) {
        $event.preventDefault();

        $rootScope.IsLoading = true;
        Common.Services.Call($http, {
            url: Common.Services.url.MON,
            method: _MONMonitor_Import.URL.Template_Download,
            data: {
                templateID: $scope.Template.ID, customerID: $scope.Template.CustomerID, dtfrom: $scope.Item.DateFrom, dtto: $scope.Item.DateTo
            },
            success: function (res) {
                $rootScope.IsLoading = false;
                Common.Services.Error(res, function (res) {
                    $rootScope.DownloadFile(res);
                })
            }
        });
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
            $scope.File.ReferID = $rootScope.FunctionItem.ID;
            $scope.File.TypeOfFileCode = Common.CATTypeOfFileCode.DIPOD;

            $scope.Excel_Check();
        }
    };

    $scope.Excel_Check = function () {
        $rootScope.IsLoading = true;

        Common.Services.Call($http, {
            url: Common.Services.url.MON,
            method: _MONMonitor_Import.URL.Check,
            data: {
                file: $scope.File.FilePath,
                templateID: $scope.Template.ID,
                customerID: $scope.Template.CustomerID,
                dtfrom: $scope.Item.DateFrom, dtto: $scope.Item.DateTo
            },
            success: function (res) {
                $rootScope.IsLoading = false;
                Common.Services.Error(res, function (res) {
                    $rootScope.Message({
                        Msg: "Đã kiểm tra."
                    })
                    $scope.IsExcelChecked = true;
                    _MONMonitor_Import.Data.FromFile = res;
                    $scope.excel_gridOptions.dataSource.data(res);
                })
            }
        });
    }

    $scope.Import_Click = function ($event) {
        $event.preventDefault();

        var grid = $scope.excel_grid;

        var importData = grid.dataSource.data();
        var lstError = [];
        $.each(importData, function (i, item) {
            if (!item.ExcelSuccess) {
                if (lstError.indexOf(item.ID) < 0)
                    lstError.push(item.ID);
            }
        })

        var strConfirm = 'Xác nhận lưu?';
        if (lstError.length > 0) {
            var strError = lstError.join(", ");
            strConfirm = "Dòng ID: (" + strError + ") có lỗi, tiếp tục lưu?";
        }
        $rootScope.Message({
            Type: Common.Message.Type.Confirm,
            Msg: strConfirm,
            Ok: function () {
                $rootScope.IsLoading = true;
                var data = _MONMonitor_Import.Data.FromFile;

                Common.Services.Call($http, {
                    url: Common.Services.url.MON,
                    method: _MONMonitor_Import.URL.Import,
                    data: {
                        Data: data,
                        TemplateID: $scope.Template.ID,
                    },
                    success: function (res) {
                        $rootScope.IsLoading = false;
                        Common.Services.Error(res, function () {
                            $rootScope.Message({ Msg: "Thành công!" });
                            grid.dataSource.data([]);
                            $scope.IsExcelChecked = false;
                        })
                    }
                });
            }
        })
    }


    $timeout(function () {
        $scope.Template_Click(null, $scope.template_win);
    }, 100)


    $scope.Back_Click = function ($event) {
        $event.preventDefault();
        $state.go("main.MONMonitor.Input");
    }

    $scope.Close_Click = function ($event, win) {
        $event.preventDefault();

        win.close();
    }
}]);