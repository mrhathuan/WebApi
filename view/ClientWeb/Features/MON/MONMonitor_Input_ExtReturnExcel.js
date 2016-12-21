/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />

//#region const

var _MONInput_ExtReturnExcel = {
    URL: {
        ExcelOnline_Init: 'MONOPSExtReturn_ExcelOnline_Init',
        ExcelOnline_Change: 'MONOPSExtReturn_ExcelOnline_Change',
        ExcelOnline_Import: 'MONOPSExtReturn_ExcelOnline_Import',
        ExcelOnline_Approve: 'MONOPSExtReturn_ExcelOnline_Approve',

        Template_List: 'MONOPSExtReturn_Setting_List',
        Template_Get: 'MONOPSExtReturn_Setting_Get',
        ALL_SYSVarExtReturnStatus: 'ALL_SYSVarExtReturnStatus',
    },
    Data: {
    },
    ExcelKey: {
        Resource: "MONMonitor_Input_ExtReturnExcel",
        MONInput_ExtReturn: "MONInput_ExtReturn"
    }
}

//#endregion

angular.module('myapp').controller('MONMonitor_Input_ExtReturnExcelCtrl', ['$rootScope', '$scope', '$http', '$location', '$state', '$timeout', function ($rootScope, $scope, $http, $location, $state, $timeout) {
    Common.Log('MONMonitor_Input_ExtReturnExcelCtrl');
    $rootScope.IsLoading = false;

    $scope.HasTemplate = false;
    $scope.Template = {
        ID: -1, Name: '',
    }

    $scope.Search = {
        DateFrom: (new Date()).addDays(-7),
        DateTo: (new Date()),
        ListCustomerID: []
    }

    var cookie = Common.Cookie.Get("ExtReturnExcel_Search");
    if (!Common.HasValue(cookie) || cookie == '') {
        var val = JSON.stringify($scope.Search)
        Common.Cookie.Set("ExtReturnExcel_Search", val)
    }
    else {
        var val = JSON.parse(cookie);
        $scope.Search = val;
    }

    $scope.mts_CustomerOptions = {
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    Code: { type: 'string' },
                    CustomerName: { type: 'string' }
                }
            }
        }),
        valuePrimitive: true, dataTextField: "Code", dataValueField: "ID", placeholder: "Chọn khách hàng...", filter: "contains", ignoreCase: true,
        highlightFirst: true, autoClose: false,
        itemTemplate: '<span>#= Code #</span><span style="float:right;">#= CustomerName #</span>',
        headerTemplate: '<strong><span> Mã khách hàng </span><span style="float:right;"> Tên khách hàng </span></strong>',
        change: function (e) {
        }
    }

    Common.ALL.Get($http, {
        url: Common.ALL.URL.Customer,
        success: function (data) {
            $scope.mts_CustomerOptions.dataSource.data(data)
        }
    })

    $scope.SearchClick = function ($event) {
        $event.preventDefault();

        if (!$scope.HasTemplate) {
            $rootScope.Message({ Msg: 'Chưa chọn template', NotifyType: Common.Message.NotifyType.ERROR });
        }
        else if (!Common.HasValue($scope.Search.DateFrom) || !Common.HasValue($scope.Search.DateTo)) {
            $rootScope.Message({ Msg: 'Vui lòng chọn Từ ngày- Đến ngày chính xác', NotifyType: Common.Message.NotifyType.ERROR });
        }
        else if ($scope.Search.DateFrom > $scope.Search.DateTo) {
            $rootScope.Message({ Msg: 'Vui lòng chọn Từ ngày nhỏ hơn Đến ngày', NotifyType: Common.Message.NotifyType.ERROR });
        }
        else {
            var val = JSON.stringify($scope.Search)
            Common.Cookie.Set("SearchClick", val);

            var lstMessageError = [];
            for (var i = 0; i < 15; i++) {
                var resource = $rootScope.RS[_MONInput_ExtReturnExcel.ExcelKey.Resource + '_' + i];
                if (Common.HasValue(resource))
                    lstMessageError.push(resource);
            }
            if (lstMessageError.length == 0) {
                lstMessageError = [
                    '[ID] không được trống', //0
                    '[Mã khách hàng] không chính xác', //1
                    '[Tên khách hàng] không được trống', //2
                    '[Số xe] không được trống', //3
                    '[Mã nhà xe] không được trống', //4
                    '[Tên nhà xe] không được trống', //5
                    '[Tài xế] không được trống', //6
                    '[Mã sản phẩm] không được trống', //7
                    '[Mã nhóm sản phẩm] không được trống', //8
                    '[Mã chuyến] không được trống', //9
                    '[Mã đơn hàng] không được trống', //10
                    '[Ngày gửi yêu cầu] không được trống', //11
                    '[ETD] không được trống', //12
                    '[ETA] không được trống', //13
                    '[Số DN] không được trống', //14
                    '[Số SO] không được trống', //15
                    '[Ngày gửi yêu cầu] không chính xác', //16
                    '[ETD] không chính xác', //17
                    '[ETA] không chính xác', //18
                    '[Số lượng] không được trống', //19
                    '[Số lượng] không chính xác', //20
                    '[Loại trả về] không được trống', //21
                    '[Loại trả về] không tồn tại', //22
                    'Không tìm thấy dữ liệu phù hợp', //23
                    '[Ngày nhận c/t] không hợp lệ', //24
                    '[Số lượng] phải lớn hơn 0', //25
                ];
            }
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.MON,
                method: _MONInput_ExtReturnExcel.URL.Template_Get,
                data: {
                    templateID: $scope.Template.ID,
                },
                success: function (res) {
                    $rootScope.IsLoading = false;
                    if (Common.HasValue(res)) {
                        $scope.File = { FileName: 'MONInput_ExtReturn_ExcelOnline', FileExt: 'xlsx', FilePath: '', ReferID: $rootScope.FunctionItem.ID, TypeOfFileCode: Common.CATTypeOfFileCode.ORD };

                        $scope.excelExtReturn.Init({
                            functionkey: _MONInput_ExtReturnExcel.ExcelKey.MONInput_ExtReturn + '_' + new Date().getTime(),
                            params: {
                                'dtFrom': $scope.Search.DateFrom,
                                'dtTo': $scope.Search.DateTo,
                                'listCustomerID': $scope.Search.ListCustomerID,
                                'templateID': $scope.Template.ID,
                            },
                            rowStart: res.RowStart,
                            colCheckChange: res.ColumnIndexMax,
                            url: Common.Services.url.MON,
                            methodInit: _MONInput_ExtReturnExcel.URL.ExcelOnline_Init,
                            methodChange: _MONInput_ExtReturnExcel.URL.ExcelOnline_Change,
                            methodImport: _MONInput_ExtReturnExcel.URL.ExcelOnline_Import,
                            methodApprove: _MONInput_ExtReturnExcel.URL.ExcelOnline_Approve,
                            lstMessageError: lstMessageError,

                            Changed: function () {

                            },
                            Approved: function () {
                            }
                        });
                    }
                }
            });
        }
    };

    //#region Excel
    $timeout(function () {
        if (Common.HasValue($('#spreadsheetExtReturn'))) {
            $scope.excelExtReturn.data.spreadsheet = $('#spreadsheetExtReturn').kendoSpreadsheet({
                excelImport: function (e) {
                    if (e.file.size > 3000000) {
                        e.preventDefault();
                        $rootScope.Message({ Msg: "File upload phải có dung lượng < 3MB" });
                        return;
                    } else {
                        $scope.excelExtReturn.data.isImport = true;
                        e.promise.progress(function (e) { })
                        .done(function (res) {
                            $rootScope.IsLoading = true;

                            var data = { id: $scope.excelExtReturn.data.item.ID, worksheets: $scope.excelExtReturn.data.spreadsheet.sheets(), lstMessageError: $scope.excelExtReturn.data.options.lstMessageError };
                            angular.forEach($scope.excelExtReturn.data.options.params, function (v, p) {
                                data[p] = v;
                            });
                            Common.Services.Call($http, {
                                url: $scope.excelExtReturn.data.options.url,
                                method: $scope.excelExtReturn.data.options.methodImport,
                                data: data,
                                success: function (res) {
                                    $rootScope.IsLoading = false;

                                    $scope.excelExtReturn.data.spreadsheet.fromJSON({ sheets: res.Worksheets });
                                    $scope.excelExtReturn.data.isImport = false;
                                    $scope.excelExtReturn.data.isChange = true;
                                },
                                error: function (res) {
                                    $rootScope.IsLoading = false;
                                    $scope.excelExtReturn.data.isImport = false;
                                }
                            });
                        });
                    }
                },
                change: function (arg) {
                    if (!$scope.excelExtReturn.data.isImport) {
                        var lstRowCheck = [];
                        var sheet = $scope.excelExtReturn.data.spreadsheet.activeSheet();
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
                            if (col < $scope.excelExtReturn.data.options.colCheckChange) {
                                rowChange = row;
                                colChange = col;
                                if (Common.HasValue(value.value))
                                    valChange = value.value;
                            }
                        });

                        if (lstRowCheck.length == 0 && rowChange > 0)
                            lstRowCheck.push(rowChange);

                        if (colFail < 2) {
                            if (lstRowCheck.length > 0) {
                                angular.forEach(lstRowCheck, function (rowindex, p) {
                                    var range = sheet.range('A' + (rowindex + 1) + ':AZ' + (rowindex + 1));
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

                                    $scope.excelExtReturn.data.rowRunning++;
                                    $scope.excelExtReturn.data.isChange = true;
                                    $scope.excelExtReturn.LoadActive();
                                    var data = { id: $scope.excelExtReturn.data.item.ID, row: rowindex, cells: cells, lstMessageError: $scope.excelExtReturn.data.options.lstMessageError };
                                    angular.forEach($scope.excelExtReturn.data.options.params, function (v, p) {
                                        data[p] = v;
                                    });
                                    Common.Services.Call($http, {
                                        url: $scope.excelExtReturn.data.options.url,
                                        method: $scope.excelExtReturn.data.options.methodChange,
                                        data: data,
                                        success: function (res) {
                                            $scope.excelExtReturn.data.rowRunning--;

                                            $scope.excelExtReturn.data.isImport = true;
                                            var sheet = $scope.excelExtReturn.data.spreadsheet.activeSheet();
                                            angular.forEach(res.cells, function (col, p) {
                                                var cell = sheet.range(res.index, col.index, 0, 0);
                                                cell.color(col.color);
                                                cell.background(col.background);
                                                cell.value(col.value);
                                                cell.fontSize(col.fontSize);
                                                cell.fontFamily(col.fontFamily);
                                            });
                                            $scope.excelExtReturn.data.isImport = false;

                                            if (Common.HasValue($scope.excelExtReturn.data.options.Changed))
                                                $scope.excelExtReturn.data.options.Changed();
                                            $scope.excelExtReturn.LoadActive();
                                        },
                                        error: function (res) {
                                            $rootScope.IsLoading = false;
                                        }
                                    });
                                });
                            }
                        }
                    }
                },
                render: function (arg) {
                    //debugger;
                }
            }).data("kendoSpreadsheet");
        }
    }, 1000);

    $scope.excelExtReturn = {
        data: {
            spreadsheet: null,
            item: null,
            isImport: false,
            isChange: false,
            options: null,
            rowRunning: 0,
            totalRow: 0,
            activeAccept: true,
            captionAccept: 'Đồng ý',
            captionWin: 'Excel'
        },
        LoadActive: function () {
            if ($scope.excelExtReturn.data.rowRunning > 0) {
                $scope.excelExtReturn.data.activeAccept = true;
                $scope.excelExtReturn.data.captionAccept = '(' + $scope.excelExtReturn.data.rowRunning + ') kiểm tra';
            }
            else {
                $scope.excelExtReturn.data.activeAccept = false;
                $scope.excelExtReturn.data.captionAccept = 'Đồng ý';
            }
        },
        Init: function (options) {
            options = $.extend(true, {
                functionkey: '',
                params: {},
                lstMessageError: [],
                colData: [],
                rowStart: -1,
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
                $scope.excelExtReturn.data.options = options;

                //$scope.winexcelshare.center();
                //$scope.winexcelshare.maximize();
                //$scope.winexcelshare.open();

                var data = { functionid: $rootScope.FunctionItem.ID, functionkey: $scope.excelExtReturn.data.options.functionkey, isreload: false };
                angular.forEach($scope.excelExtReturn.data.options.params, function (v, p) {
                    data[p] = v;
                });
                $rootScope.IsLoading = true;
                Common.Services.Call($http, {
                    url: options.url,
                    method: options.methodInit,
                    data: data,
                    success: function (res) {
                        $scope.excelExtReturn.data.isImport = true;
                        $scope.excelExtReturn.data.item = res;
                        $scope.excelExtReturn.data.spreadsheet.fromJSON({ sheets: res.Worksheets });
                        $scope.excelExtReturn.data.isImport = false;

                        $rootScope.IsLoading = false;

                        var rowStart = $scope.excelExtReturn.data.options.rowStart;
                        var colData = $scope.excelExtReturn.data.options.colData;
                        if (rowStart > 0 && colData.length > 0) {
                            var sheet = $scope.excelExtReturn.data.spreadsheet.activeSheet();
                            var range = null;
                            if (colData.length > 1)
                                range = sheet.range(colData[0] + '' + rowStart + ':' + colData[1] + sheet._rows._count);
                            else
                                range = sheet.range(colData[0] + '' + rowStart + ':' + colData[0] + sheet._rows._count);

                            if (range != null) {
                                var fail = 0;
                                var lstRow = [];

                                range.forEachCell(function (row, column, value) {
                                    if (fail <= 2 && (!Common.HasValue(value) || !Common.HasValue(value.value)))
                                        fail++;
                                    else if (fail <= 2) {
                                        if (lstRow.indexOf(row))
                                            lstRow.push(row);
                                    }
                                });

                                $scope.excelExtReturn.data.totalRow = lstRow.length;
                            }
                        }
                    },
                    error: function (res) {
                        $rootScope.IsLoading = false;
                    }
                });
            }
        }
    };

    $scope.excel_Approve_Click = function ($event, win) {
        $event.preventDefault();

        if ($scope.excelExtReturn.data.rowRunning == 0) {
            if ($scope.excelExtReturn.data.isChange == true) {
                $rootScope.Message({
                    Type: Common.Message.Type.Confirm,
                    Msg: 'Bạn muốn lưu các dữ liệu đã thay đổi ?',
                    pars: {},
                    Ok: function (pars) {
                        //$rootScope.IsLoading = true;

                        $rootScope.Loading.Show();
                        var data = { id: $scope.excelExtReturn.data.item.ID };
                        angular.forEach($scope.excelExtReturn.data.options.params, function (v, p) {
                            data[p] = v;
                        });
                        Common.Services.Call($http, {
                            url: $scope.excelExtReturn.data.options.url,
                            method: $scope.excelExtReturn.data.options.methodApprove,
                            data: data,
                            success: function (res) {
                                $scope.excelExtReturn.data.isChange = false;
                                $rootScope.Loading.Change('Lấy dữ liệu excel mới', 10);
                                if ((res + '').toLowerCase() == 'true') {
                                    var data = { functionid: $rootScope.FunctionItem.ID, functionkey: $scope.excelExtReturn.data.options.functionkey, isreload: true };
                                    angular.forEach($scope.excelExtReturn.data.options.params, function (v, p) {
                                        data[p] = v;
                                    });
                                    Common.Services.Call($http, {
                                        url: $scope.excelExtReturn.data.options.url,
                                        method: $scope.excelExtReturn.data.options.methodInit,
                                        data: data,
                                        success: function (res) {
                                            $rootScope.Loading.Hide();
                                            $rootScope.Message({ Msg: 'Lưu dữ liệu thành công', NotifyType: Common.Message.NotifyType.SUCCESS });

                                            $scope.excelExtReturn.data.isImport = true;
                                            $scope.excelExtReturn.data.item = res;
                                            $scope.excelExtReturn.data.spreadsheet.fromJSON({ sheets: res.Worksheets });
                                            $scope.excelExtReturn.data.isImport = false;

                                            if (Common.HasValue($scope.excelExtReturn.data.options.Approved))
                                                $scope.excelExtReturn.data.options.Approved();
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

    $scope.winexcelshare_Reload_Click = function ($event, win) {
        $event.preventDefault();
        if (!$scope.HasTemplate) {
            rootScope.Message({ Msg: 'Chưa chọn template', NotifyType: Common.Message.NotifyType.ERROR });
        }
        else if (!Common.HasValue($scope.Search.DateFrom) || !Common.HasValue($scope.Search.DateTo)) {
            rootScope.Message({ Msg: 'Vui lòng chọn Từ ngày- Đến ngày chính xác', NotifyType: Common.Message.NotifyType.ERROR });
        }
        else if ($scope.Search.DateFrom > $scope.Search.DateTo) {
            rootScope.Message({ Msg: 'Vui lòng chọn Từ ngày nhỏ hơn Đến ngày', NotifyType: Common.Message.NotifyType.ERROR });
        }
        else {
            var val = JSON.stringify($scope.Search)
            Common.Cookie.Set("Reload_Click", val);
            $rootScope.IsLoading = true;

            var data = { functionid: $rootScope.FunctionItem.ID, functionkey: $scope.excelExtReturn.data.options.functionkey, isreload: true };
            angular.forEach($scope.excelExtReturn.data.options.params, function (v, p) {
                data[p] = v;
            });
            Common.Services.Call($http, {
                url: $scope.excelExtReturn.data.options.url,
                method: $scope.excelExtReturn.data.options.methodInit,
                data: data,
                success: function (res) {
                    $rootScope.IsLoading = false;
                    $scope.excelExtReturn.data.item = res;
                    $scope.excelExtReturn.data.spreadsheet.fromJSON({ sheets: res.Worksheets });
                },
                error: function (res) {
                    $rootScope.IsLoading = false;
                }
            });
        }
    };

    //#endregion

    //#region Template
    $scope.excel_template_gridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.MON,
            method: _MONInput_ExtReturnExcel.URL.Template_List,
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
            ID: -1, Name: '',
        }

        var item = grid.dataItem(grid.select());
        if (Common.HasValue(item)) {
            $scope.HasTemplate = true;

            $scope.Template.ID = item.SettingID;
            $scope.Template.Name = item.Name;
        }

        win.close();
    }
    $timeout(function () {
        $scope.Template_Click(null, $scope.template_win);
    }, 100)

    $scope.Close_Click = function ($event, win) {
        $event.preventDefault();

        win.close();
    }
    //#endregion

    //#region status
    $scope.extReturnStatus_gridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.CAT,
            method: _MONInput_ExtReturnExcel.URL.ALL_SYSVarExtReturnStatus,
            pageSize: 0,
            model: {
                ID: 'ID',
                fields: {
                    ID: { type: 'number' }
                }
            }
        }),
        height: '100%', groupable: false, pageable: false, sortable: true, columnMenu: false,
        filterable: { mode: 'row' }, resizable: true, selectable: true,
        columns: [
            {
                field: 'Code', title: 'Mã loại', sortable: true,
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'ValueOfVar', title: 'Loại trả về', sortable: true,
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                title: "", sortable: false, filterable: false,
            }
        ]
    }

    $scope.Status_Click = function ($event, win) {
        $event.preventDefault();
        win.center().open();

        $scope.extReturnStatus_gridOptions.dataSource.read();
    }
    //#endregion
    //actions
    $scope.ShowSetting = function ($event, grid) {
        $event.preventDefault();

        $rootScope.ShowSetting({
            ListView: views.PODInput_ExtReturn,
            event: $event,
            grid: grid,
            current: $state.current
        });
    };

    $scope.Back_Click = function ($event) {
        $event.preventDefault();
        $state.go("main.MONMonitor.Index")
    };
}]);