/// <reference path="~/Scripts/jquery-1.9.1.intellisense.js" />
/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />

//#region URL

var _ORDOrder_Plan_ExcelOnline = {
    URL: {
        Template_List: 'ORDOrder_Plan_Excel_Setting_List',
        Location_Create: 'ORDOrder_Excel_Location_Create',
        Template_Get: 'ORDOrder_Plan_Excel_Setting_Get',

        //GOP
        GOPRead: "GroupOfProduct_Read",
        Product_Create: "ORDOrder_Excel_Product_Create",

        ExcelOnline_Init: 'ORDOrder_Plan_ExcelOnline_Init',
        ExcelOnline_Change: 'ORDOrder_Plan_ExcelOnline_Change',
        ExcelOnline_Import: 'ORDOrder_Plan_ExcelOnline_Import',
        ExcelOnline_Approve: 'ORDOrder_Plan_ExcelOnline_Approve',
        ExcelOnline_LocationToSave: 'ORDOrder_Plan_ExcelOnline_LocationToSave',
    },
    Data: {
        Province: [],
        District: [],
        FromFile: [],
        Service: [
            {
                ID: 26,
                ValueOfVar: 'Nhập khẩu',
                ListMode: [
                    {
                        ID: 31,
                        ValueOfVar: 'FCL'
                    }
                ]
            },
            {
                ID: 27,
                ValueOfVar: 'Xuất khẩu',
                ListMode: [
                    {
                        ID: 31,
                        ValueOfVar: 'FCL'
                    }
                ]
            },
            {
                ID: 28,
                ValueOfVar: 'Nội địa',
                ListMode: [
                    {
                        ID: 31,
                        ValueOfVar: 'FCL'
                    },
                    {
                        ID: 33,
                        ValueOfVar: 'FTL'
                    },
                    {
                        ID: 34,
                        ValueOfVar: 'LTL'
                    }
                ]
            }
        ],

        ListGOP: [],
        ListPacking: [],

        CookieSearch: "_ORDOrder_Plan_ExcelOnline_Template",
        CheckResult: [],
    },
    iFCL: 31,
    iFTL: 33, 
    iLTL: 34,
    ExcelKey: {
        Resource: "ORDOrder_ExcelOnline",
        Order: "ORDOrder_ExcelOnline"
    }
}

//#endregion

angular.module('myapp').controller('ORDOrder_Plan_ExcelOnlineCtrl', ['$rootScope', '$scope', '$http', '$location', '$state', '$timeout', function ($rootScope, $scope, $http, $location, $state, $timeout) {
    if ($rootScope.IsPageComplete != true) return;
    Common.Log('ORDOrder_Plan_ExcelOnlineCtrl');
    $rootScope.IsLoading = false;

    if (!$rootScope.CheckView("ActExcel", "main.ORDOrder.Index")) return;
    $scope.Auth = $rootScope.GetAuth();

    $scope.IsCO = false;
    $scope.IsDI = false;
    $scope.IsExcelChecked = false;
    $scope.IsNewLocation = false;
    $scope.IsNewProduct = false;
    $scope.HasTemplate = false;
    $scope.IsFullScreen = false;
    $scope.TotalLocationNew = 0;
    $scope.TotalProductNew = 0;
    $scope.pID = -1;

    $scope.Template = {
        ID: -1, Name: '', CustomerName: '', CustomerID: -1, SYSCustomerID: -1
    }

    //#region Template
    $scope.excel_template_gridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.ORD,
            method: _ORDOrder_Plan_ExcelOnline.URL.Template_List,
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
                        $scope.Template.CustomerName = item.SettingCustomerName;
                        $scope.Template.CustomerID = item.CustomerID;
                        $scope.Template.SYSCustomerID = item.SYSCustomerID;
                        Common.Cookie.Set(_ORDOrder_Plan_ExcelOnline.Data.CookieSearch, JSON.stringify($scope.Template));
                        $scope.template_win.close();
                        $scope.pID = -1;
                        $scope.ExcelOnline_Click();
                    }, 1)
                }
            })
        },
        columns: [ { field: 'Name', title: '{{RS.CUSSetting.Name}}', sortable: true, filterable: { cell: { operator: 'contains', showOperators: false } } } ]
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
            ID: -1, Name: '', CustomerName: '', CustomerID: -1, SYSCustomerID: -1
        }

        var item = grid.dataItem(grid.select());
        if (Common.HasValue(item)) {
            $scope.HasTemplate = true;

            $scope.Template.ID = item.ID;
            $scope.Template.Name = item.Name;
            $scope.Template.CustomerName = item.SettingCustomerName;
            $scope.Template.CustomerID = item.CustomerID;
            $scope.Template.SYSCustomerID = item.SYSCustomerID;

            Common.Cookie.Set(_ORDOrder_Plan_ExcelOnline.Data.CookieSearch, JSON.stringify($scope.Template));
            $scope.pID = -1;
            $scope.ExcelOnline_Click();
        }

        win.close();
    }
    //#endregion

    $timeout(function () {
        $scope.Init_LoadCookie();
    }, 100)

    //#region cookie
    $scope.Init_LoadCookie = function () {
        Common.Log('Init_LoadCookie');
        var strCookie = Common.Cookie.Get(_ORDOrder_Plan_ExcelOnline.Data.CookieSearch);
        if (Common.HasValue(strCookie) && strCookie != '') {
            try {
                var objCookie = eval('[' + strCookie + ']')[0];
                $scope.Template.ID = objCookie.ID;
            } catch (e) { }
        }

        if ($scope.Template.ID >= 0) {
            Common.Services.Call($http, {
                url: Common.Services.url.ORD,
                method: _ORDOrder_Plan_ExcelOnline.URL.Template_List,
                data: { request: "" },
                success: function (res) {
                    if (res != null && res.Data != null) {
                        angular.forEach(res.Data, function (item, key) {
                            if (item.ID == $scope.Template.ID && $scope.HasTemplate == false) {
                                $scope.Template.Name = item.Name;
                                $scope.Template.CustomerName = item.SettingCustomerName;
                                $scope.Template.CustomerID = item.CustomerID;
                                $scope.Template.SYSCustomerID = item.SYSCustomerID;
                                $scope.HasTemplate = true;
                                $scope.pID = -1;
                                $scope.ExcelOnline_Click();
                            }
                        });
                        if ($scope.HasTemplate == false) {
                            $scope.Template = {
                                ID: -1, Name: '', CustomerName: '', CustomerID: -1
                            };
                            $scope.Template_Click(null, $scope.template_win);
                        }
                    }
                }
            });
        } else {
            $scope.Template = {
                ID: -1, Name: '', CustomerName: '', CustomerID: -1
            };
            $scope.Template_Click(null, $scope.template_win);
        }
    };
    //#endregion

    $scope.ExcelOnline_Click = function () {
        //$event.preventDefault();
        var lstMessageError = [];
        for (var i = 0; i < 15; i++) {
            var resource = $rootScope.RS[_ORDOrder_Plan_ExcelOnline.ExcelKey.Resource + '_' + i];
            if (Common.HasValue(resource))
                lstMessageError.push(resource);
        }
        if (lstMessageError.length == 0) {
            lstMessageError = [
                'Thiếu mã KH.', //0 Api
                'KH không tồn tại.', //1 Api
                'Địa chỉ ko tồn tại', //2 Api
                'Npp không tồn tại.', //3 Api
                'Npp không xác định.', //4 Api
                'Chưa thiết lập cột loại vận chuyển.', //5
                'Loại vận chuyển không xác định.', //6
                'Không xác định loại vận chuyển', //7
                'Chưa thiết lập cột dịch vụ vận chuyển.', //8
                'Dịch vụ vận chuyển không xác định.', //9
                'Không xác định dịch vụ vận chuyển.', //10
                'Chưa thiết lập cột loại xe.', //11
                'Sai giờ gửi yêu cầu.', //12
                'Sai ngày gửi yêu cầu.', //13
                'Sai ETD.', //14
                'Sai giờ ETD.', //15
                'Sai ETA.', // 16
                'Sai giờ ETA.', //17
                'Sai ngày y.c giao hàng.', //18
                'Sai giờ y.c giao hàng.', //19
                'Sai ngày y.c đến kho.', //20
                'Sai giờ y.c đến kho.', //21
                'Không có hợp đồng.', //22
                'Có hơn 1 hợp đồng.', //23
                'Thiếu mã ĐH.', //24
                'Trùng mã đơn hàng ĐH.', //25
                'Chưa thiết lập kho [Hiện kho].', //26
                'Kho thiết lập không xác định.', //27
                'Sai tấn.', //28
                'Sai số khối.', //29
                'Sai số lượng.', //30
                'Thiếu sản lượng.', //31
                'Chưa thiết lập kho và nhóm hàng [Hiện Kho-Hàng].', //32
                'Kho thiết lập không xác định.', //33
                'Kho thiết lập không xác định nhóm hàng.', // 34
                'Kho thiết lập không xác định hàng hóa.', //35
                'Chưa thiết lập điểm bốc hàng [LocationFromCode]', //36
                'Kho không tồn tại.', //37
                'Kho không có sản phẩm.', //38
                'Sản phẩm không tồn tại.', //39
                'Không có thông tin sản phẩm.', //40
                'Kho không có nhóm sản phẩm.', //41
                'Kho có nhiều hơn 1 nhóm sản phẩm.', //42
                'Nhóm sp không có trong kho.', //43
                'Nhóm sp không tồn tại hoặc chưa thiết lập kho.', //44
                'Nhóm sp không xác định.',//45
                'Kho có nhiều hơn 1 nhóm sản phẩm.',//46
                'Kho không có sản phẩm.',//47
                'Sản phẩm không tồn tại.',//48
                'Không có thông tin sản phẩm.',//49
                'Nhóm sp có nhiều hơn 1 ĐVT',//50
                'Nhóm sp chưa thiết lập Hàng hóa/ĐVT',//51
                'Nhóm sp chưa thiết lập ĐVT.',//52
                'Loại xe không tồn tại.',//53
                'Loại xe không xác định.',//54
                'Sai nhiệt độ tối thiểu.',//55
                'Sai nhiệt độ tối đa.',//56
                'Không có điểm giao.',//57
                'Sai giá chuyến.',//58
                'Sai ràng buộc ETD-ETA.',//59
                'Không có số tấn.',//60
                'Không có số khối.',//61
                'Không có số lượng.',//62
                'Sai giá tấn.',//63
                'Sai giá khối.',//64
                'Sai giá SL.',//65
                'Mã ĐH không hợp lệ.',//66
                'Trùng mã ĐH.',//67
                'Trùng mã ĐH và ngày gửi yêu cầu.',//68
                'Sai cut-off-time.',//69
                'Sai ngày lấy rỗng.',//70
                'Sai giờ lấy rỗng.',//71
                'Sai ngày trả rỗng.',//72
                'Sai giờ trả rỗng.',//73
                'Bãi container không tồn tại.',//74
                'Điểm nhận hàng không tồn tại.',//75
                'Điểm giao hàng không tồn tại.',//76
                'Điểm nhận hàng và điểm giao hàng trùng nhau.',//77
                'Điểm nhận hàng và điểm lấy rỗng trùng nhau.',//78
                'Điểm giao hàng và điểm trả rỗng trùng nhau.',//79
                'Sai ngày kiểm hóa.',//80
                'Hãng tàu chưa thiết lập bãi container.',//81
                'Hãng tàu không tồn tại.',//82
                'Hãng tàu không xác định.',//83
                'Số lượng container phải lớn hơn 0.',//84
                'Số lượng không hợp lệ.',//85
                'Loại container không tồn tại.',//86
                'Chưa thiết lập cột các loại cont [Loại cont].',//87
                'Loại cont thiết lập không xác định.',//88
                'Số lượng container không hợp lệ.',//89
                'Sai trọng tải.',//90
                'Chưa thiết lập loại Cont.',//91
                'Sai mã địa chỉ.',//92
                'Địa chỉ không khớp hoàn toàn.',//93
                'Sai giờ ETD chuyến.', //94
                'Sai ETD chuyến.', //95
                'Sai thời gian chuyến.', //96
                'Sai giờ ETA chuyến.', //97
                'Sai ETA chuyến.', //98
                'Sai ràng buộc thời gian ETD-ETA chuyến', //99
                'Nhà xe không tồn tại.', //100
                'Nhà xe không xác định.', //101
                'Không có thông tin tài xế.', //102
                'Xe không tồn tại.', //103
                'Không tìm thấy loại xe chuyến.', //104
                'Chuyến LTL không thể chạy đơn FTL.', //105
                'Không thể chạy 2 đơn FTL.', //106
                'Quá số tấn.', //107
                'Quá số khối.', //108
                'Quá số lượng.', //109
                'Không tìm thấy sản phẩm.', //110
                'Chuyến FTL không thể chạy đơn LTL.', //111
                'Không có thông tin sản lượng.', //112
                'Chưa phân chuyến hết đơn FTL.', //113
                'Quá trọng tải.', //114
                'Chưa nhập xe.', //115
                'Chỉ áp dụng cho đơn FTL hoặc LTL', //116
                'XXX', //
                'XXX', //
                'XXX', //
            ];
        }

        $rootScope.IsLoading = true;
        Common.Services.Call($http, {
            url: Common.Services.url.ORD,
            method: _ORDOrder_Plan_ExcelOnline.URL.Template_Get,
            data: {
                templateID: $scope.Template.ID,
            },
            success: function (res) {
                $rootScope.IsLoading = false;
                if (Common.HasValue(res)) {
                    $scope.File = { FileName: 'ORDOrder_Plan_ExcelOnline', FileExt: 'xlsx', FilePath: '', ReferID: $rootScope.FunctionItem.ID, TypeOfFileCode: Common.CATTypeOfFileCode.ORD };
                    
                    $scope.excelOrderPlan.Init({
                        functionkey: _ORDOrder_Plan_ExcelOnline.ExcelKey.Order + $scope.Template.ID + "_" + $scope.Template.CustomerID + "_" + new Date().getTime(),
                        params: {
                            templateID: $scope.Template.ID, File: $scope.File,
                            customerID: $scope.Template.CustomerID,
                            pID: $scope.pID,
                        },
                        rowStart: res.RowStart,
                        colCheckChange: res.TotalColumn,
                        url: Common.Services.url.ORD,
                        methodInit: _ORDOrder_Plan_ExcelOnline.URL.ExcelOnline_Init,
                        methodChange: _ORDOrder_Plan_ExcelOnline.URL.ExcelOnline_Change,
                        methodImport: _ORDOrder_Plan_ExcelOnline.URL.ExcelOnline_Import,
                        methodApprove: _ORDOrder_Plan_ExcelOnline.URL.ExcelOnline_Approve,
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
        if (Common.HasValue($('#spreadsheetOrder'))) {
            $scope.excelOrderPlan.data.spreadsheet = $('#spreadsheetOrder').kendoSpreadsheet({
                excelImport: function (e) {
                    if (e.file.size > 3000000) {
                        e.preventDefault();
                        $rootScope.Message({ Msg: "File upload phải có dung lượng < 3MB" });
                        return;
                    } else {
                        $scope.excelOrderPlan.data.isImport = true;
                        e.promise.progress(function (e) { })
                        .done(function (res) {
                            $rootScope.IsLoading = true;

                            var data = { id: $scope.excelOrderPlan.data.item.ID, worksheets: $scope.excelOrderPlan.data.spreadsheet.sheets(), lstMessageError: $scope.excelOrderPlan.data.options.lstMessageError };
                            angular.forEach($scope.excelOrderPlan.data.options.params, function (v, p) {
                                data[p] = v;
                            });
                            Common.Services.Call($http, {
                                url: $scope.excelOrderPlan.data.options.url,
                                method: $scope.excelOrderPlan.data.options.methodImport,
                                data: data,
                                success: function (res) {
                                    $rootScope.IsLoading = false;

                                    $scope.excelOrderPlan.data.spreadsheet.fromJSON({ sheets: res.SYSExcel.Worksheets });
                                    $scope.excelOrderPlan.data.isImport = false;
                                    $scope.excelOrderPlan.data.isChange = true;
                                    if (Common.HasValue(res.ListRowResult) && res.ListRowResult.length > 0) {
                                        $scope.IsDI = false;
                                        if (res.ListRowResult[0].TransportModeID == _ORDOrder_Plan_ExcelOnline.iFTL || res.ListRowResult[0].TransportModeID == _ORDOrder_Plan_ExcelOnline.iLTL) {
                                            $scope.IsDI = true;
                                        }
                                        _ORDOrder_Plan_ExcelOnline.Data.CheckResult = res.ListRowResult;
                                        $scope.Check_DataResult(res.ListRowResult);

                                        $scope.excel_locationTo_gridOptions.dataSource.data(res.ListRowResult);
                                    }
                                },
                                error: function (res) {
                                    $rootScope.IsLoading = false;
                                    $scope.excelOrderPlan.data.isImport = false;
                                }
                            });
                        });
                    }
                },
                change: function (arg) {
                    if (!$scope.excelOrderPlan.data.isImport) {
                        var lstRowCheck = [];
                        var sheet = $scope.excelOrderPlan.data.spreadsheet.activeSheet();
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
                            if (col < $scope.excelOrderPlan.data.options.colCheckChange) {
                                rowChange = row;
                                colChange = col;
                                if (Common.HasValue(value.value))
                                    valChange = value.value;
                            }
                        });

                        if (colFail < 2) {
                            if (lstRowCheck.length > 0) {
                                angular.forEach(lstRowCheck, function (rowindex, p) {
                                    var range = sheet.range('A' + (rowindex + 1) + ':AZ' + (rowindex + 1));
                                    var cells = [];
                                    var flag = 1;
                                    range.forEachCell(function (row, column, value) {
                                        if (flag < 7) {
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

                                    $scope.excelOrderPlan.data.rowRunning++;
                                    $scope.excelOrderPlan.data.isChange = true;
                                    $scope.excelOrderPlan.LoadActive();
                                    var data = { id: $scope.excelOrderPlan.data.item.ID, row: rowindex, cells: cells, lstMessageError: $scope.excelOrderPlan.data.options.lstMessageError };
                                    angular.forEach($scope.excelOrderPlan.data.options.params, function (v, p) {
                                        data[p] = v;
                                    });
                                    Common.Services.Call($http, {
                                        url: $scope.excelOrderPlan.data.options.url,
                                        method: $scope.excelOrderPlan.data.options.methodChange,
                                        data: data,
                                        success: function (res) {
                                            $scope.excelOrderPlan.data.rowRunning--;

                                            $scope.excelOrderPlan.data.isImport = true;
                                            var sheet = $scope.excelOrderPlan.data.spreadsheet.activeSheet();
                                            angular.forEach(res.Row.cells, function (col, p) {
                                                var cell = sheet.range(res.Row.index, col.index, 0, 0);
                                                cell.color(col.color);
                                                cell.background(col.background);
                                                cell.value(col.value);
                                                cell.fontSize(col.fontSize);
                                                cell.fontFamily(col.fontFamily);
                                            });
                                            $scope.excelOrderPlan.data.isImport = false;

                                            if (Common.HasValue($scope.excelOrderPlan.data.options.Changed))
                                                $scope.excelOrderPlan.data.options.Changed();
                                            $scope.excelOrderPlan.LoadActive();

                                            $scope.IsDI = false;
                                            if (res.TransportModeID == _ORDOrder_Plan_ExcelOnline.iFTL || res.TransportModeID == _ORDOrder_Plan_ExcelOnline.iLTL) {
                                                $scope.IsDI = true;
                                            }

                                            var dataCheck = _ORDOrder_Plan_ExcelOnline.Data.CheckResult;

                                            if (Common.HasValue(dataCheck)) {
                                                for (var i = 0 ; i < dataCheck.length ; i++) {
                                                    var value = dataCheck[i];
                                                    if (value.Index == res.Index) {
                                                        dataCheck.splice(i, 1);
                                                    }
                                                }

                                                dataCheck.push(res);
                                                _ORDOrder_Plan_ExcelOnline.Data.CheckResult = dataCheck;
                                                $scope.Check_DataResult(dataCheck);

                                                var dataLocationTo = $.extend(true, [], $scope.excel_locationTo_gridOptions.dataSource.data());
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
                                            $scope.excelOrderPlan.data.rowRunning--;
                                            $scope.excelOrderPlan.data.isChange = false;
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

    $scope.excelOrderPlan = {
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
            if ($scope.excelOrderPlan.data.rowRunning > 0) {
                $scope.excelOrderPlan.data.activeAccept = true;
                $scope.excelOrderPlan.data.captionAccept = '(' + $scope.excelOrderPlan.data.rowRunning + ') kiểm tra';
            }
            else {
                $scope.excelOrderPlan.data.activeAccept = false;
                $scope.excelOrderPlan.data.captionAccept = 'Đồng ý';
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
                $scope.excelOrderPlan.data.options = options;

                //$scope.winexcelshare.center();
                //$scope.winexcelshare.maximize();
                //$scope.winexcelshare.open();

                var data = { functionid: $rootScope.FunctionItem.ID, functionkey: $scope.excelOrderPlan.data.options.functionkey, isreload: false };
                angular.forEach($scope.excelOrderPlan.data.options.params, function (v, p) {
                    data[p] = v;
                });
                $rootScope.IsLoading = true;
                Common.Services.Call($http, {
                    url: options.url,
                    method: options.methodInit,
                    data: data,
                    success: function (res) {
                        $scope.excelOrderPlan.data.isImport = true;
                        $scope.excelOrderPlan.data.item = res;
                        $scope.excelOrderPlan.data.spreadsheet.fromJSON({ sheets: res.Worksheets });
                        $scope.excelOrderPlan.data.isImport = false;

                        $rootScope.IsLoading = false;
                    },
                    error: function (res) {
                        $rootScope.IsLoading = false;
                    }
                });
            }
        }
    };

    $scope.excelOrder_Approve_Click = function ($event) {
        $event.preventDefault();

        if ($scope.excelOrderPlan.data.rowRunning == 0) {
            if ($scope.excelOrderPlan.data.isChange == true) {
                $rootScope.Message({
                    Type: Common.Message.Type.Confirm,
                    Msg: 'Bạn muốn lưu các dữ liệu đã thay đổi ?',
                    pars: {},
                    Ok: function (pars) {
                        //$rootScope.IsLoading = true;

                        $rootScope.Loading.Show();
                        var data = { id: $scope.excelOrderPlan.data.item.ID };
                        angular.forEach($scope.excelOrderPlan.data.options.params, function (v, p) {
                            data[p] = v;
                        });
                        Common.Services.Call($http, {
                            url: $scope.excelOrderPlan.data.options.url,
                            method: $scope.excelOrderPlan.data.options.methodApprove,
                            data: data,
                            success: function (res) {
                                $scope.excelOrderPlan.data.isChange = false;
                                $rootScope.Loading.Change('Lấy dữ liệu excel mới', 10);
                                if (res.ID > 0) {
                                    var data = { functionid: $rootScope.FunctionItem.ID, functionkey: $scope.excelOrderPlan.data.options.functionkey, isreload: true };
                                    angular.forEach($scope.excelOrderPlan.data.options.params, function (v, p) {
                                        data[p] = v;
                                    });
                                    
                                    $scope.pID = res.ID;
                                    data['pID'] = $scope.pID;
                                    Common.Services.Call($http, {
                                        url: $scope.excelOrderPlan.data.options.url,
                                        method: $scope.excelOrderPlan.data.options.methodInit,
                                        data: data,
                                        success: function (res) {
                                            $rootScope.Loading.Hide();
                                            $rootScope.Message({ Msg: 'Lưu dữ liệu thành công', NotifyType: Common.Message.NotifyType.SUCCESS });

                                            $scope.excelOrderPlan.data.isImport = true;
                                            $scope.excelOrderPlan.data.item = res;
                                            $scope.excelOrderPlan.data.spreadsheet.fromJSON({ sheets: res.Worksheets });
                                            $scope.excelOrderPlan.data.isImport = false;

                                            if (Common.HasValue($scope.excelOrderPlan.data.options.Approved))
                                                $scope.excelOrderPlan.data.options.Approved();

                                            $scope.IsCO = false;
                                            $scope.IsDI = false;
                                            $scope.IsNewLocation = false;
                                            $scope.TotalLocationNew = 0;
                                            $scope.TotalProductNew = 0;
                                        },
                                        error: function (res) {
                                            $rootScope.Loading.Hide();
                                        }
                                    });
                                    if (res.Message != "") {
                                        $rootScope.Message({ Msg: res.Message, NotifyType: Common.Message.NotifyType.ERROR });
                                    }

                                    if (res.Warning != "") {
                                        $rootScope.Message({ Msg: res.Warning, NotifyType: Common.Message.NotifyType.WARNING });
                                    }
                                }
                                else {
                                    $rootScope.Loading.Hide();
                                    $rootScope.Message({ Msg: 'Dữ liệu gửi lỗi', NotifyType: Common.Message.NotifyType.ERROR });

                                    if (res.Message != "") {
                                        $rootScope.Message({ Msg: res.Message, NotifyType: Common.Message.NotifyType.ERROR });
                                    }

                                    if (res.Warning != "") {
                                        $rootScope.Message({ Msg: res.Warning, NotifyType: Common.Message.NotifyType.WARNING });
                                    }
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

    $scope.excelOrder_Reload_Click = function ($event) {
        $event.preventDefault();
        $rootScope.IsLoading = true;

        var data = { functionid: $rootScope.FunctionItem.ID, functionkey: $scope.excelOrderPlan.data.options.functionkey, isreload: true };
        angular.forEach($scope.excelOrderPlan.data.options.params, function (v, p) {
            data[p] = v;
        });
        Common.Services.Call($http, {
            url: $scope.excelOrderPlan.data.options.url,
            method: $scope.excelOrderPlan.data.options.methodInit,
            data: data,
            success: function (res) {
                $rootScope.IsLoading = false;
                $scope.excelOrderPlan.data.item = res;
                $scope.excelOrderPlan.data.spreadsheet.fromJSON({ sheets: res.Worksheets });
                $scope.IsCO = false;
                $scope.IsDI = false;
                $scope.IsNewLocation = false;
                $scope.TotalLocationNew = 0;
                $scope.TotalProductNew = 0;
            },
            error: function (res) {
                $rootScope.IsLoading = false;
            }
        });
    };

    $scope.Check_DataResult = function (data) {
        Common.Log("Check_Data");

        if ($scope.IsDI) {
            $scope.IsNewLocation = false;
            $scope.IsNewProduct = false;
            $scope.TotalLocationNew = 0;
            $scope.TotalProductNew = 0;
            angular.forEach(data, function (o, i) {
                if (Common.HasValue(o.ListProductNew) && o.ListProductNew.length > 0) {
                    $scope.IsNewProduct = true;
                    $scope.TotalProductNew++;
                }
                if (o.IsLocationToFail) {
                    $scope.IsNewLocation = true;
                    $scope.TotalLocationNew++;
                }
            });

            if ($scope.IsNewLocation) {
                $rootScope.Message({
                    Msg: "Có địa chỉ mới, bạn muốn thêm địa chỉ này?",
                    Type: Common.Message.Type.Confirm,
                    Ok: function () {
                        $scope.Location_Click(null, $scope.excel_location_win, data);
                    }
                })
            }

            if ($scope.IsNewProduct) {
                $rootScope.Message({
                    Msg: "Có sản phẩm mới, bạn muốn thêm sản phẩm này?",
                    Type: Common.Message.Type.Confirm,
                    Ok: function () {
                        $scope.Product_Click(null, $scope.excel_product_win, data);
                    }
                })
            }
        }
    }

    $scope.Excel_Check = function () {
        $scope.excelOrderPlan.data.isImport = true;
        $rootScope.IsLoading = true;

        var data = { id: $scope.excelOrderPlan.data.item.ID, worksheets: $scope.excelOrderPlan.data.spreadsheet.sheets(), lstMessageError: $scope.excelOrderPlan.data.options.lstMessageError };
        angular.forEach($scope.excelOrderPlan.data.options.params, function (v, p) {
            data[p] = v;
        });
        Common.Services.Call($http, {
            url: $scope.excelOrderPlan.data.options.url,
            method: $scope.excelOrderPlan.data.options.methodImport,
            data: data,
            success: function (res) {
                $rootScope.IsLoading = false;

                $scope.excelOrderPlan.data.spreadsheet.fromJSON({ sheets: res.SYSExcel.Worksheets });
                $scope.excelOrderPlan.data.isImport = false;
                $scope.excelOrderPlan.data.isChange = true;
                if (Common.HasValue(res.ListRowResult)) {
                    _ORDOrder_Plan_ExcelOnline.Data.CheckResult = res.ListRowResult;
                    $scope.Check_DataResult(res.ListRowResult);

                    $scope.excel_locationTo_gridOptions.dataSource.data(res.ListRowResult);
                }
            },
            error: function (res) {
                $rootScope.IsLoading = false;
                $scope.excelOrderPlan.data.isImport = false;
            }
        });
    }
    //#endregion

    //#region Win Location  
    $scope.Location_AddClick = function ($event) {
        $event.preventDefault();
        $scope.Location_Click($event, $scope.excel_location_win, _ORDOrder_Plan_ExcelOnline.Data.CheckResult);
    }

    $scope.Location_Click = function ($event, win, data) {
        if (Common.HasValue($event))
            $event.preventDefault();

        $rootScope.IsLoading = true;
        var grid = $scope.excel_locationTo_grid;

        var dataRes = [];
        var temp = [];
        $.each(grid.items(), function (i, tr) {
            var o = grid.dataItem(tr);

            if (Common.HasValue(o)) {
                var isNew = $(tr).find(".chkNew").prop("checked");
                if (isNew) {
                    var objOrder = null;
                    $.each(data, function (i, row) {
                        if (o.Index == row.Index) {
                            objOrder = row;
                        }
                    });
                    if (objOrder.IsLocationToFail) {
                        if (objOrder.ListPartnerLocation.length > 0) {
                            $.each(objOrder.ListPartnerLocation, function (j, partner) {
                                if (partner.PartnerID > 0 && objOrder.IsCreateLocation) {
                                    var item = {
                                        'PartnerID': partner.PartnerID, 'PartnerCode': partner.PartnerCode, 'PartnerName': partner.PartnerName, 'LocationCode': partner.LocationCode, CustomerID: partner.CustomerID,
                                        'LocationAddress': partner.LocationAddress, 'EconomicZone': partner.EconomicZone, 'RoutingAreaCode': partner.RoutingAreaCode, 'RouteDescription': partner.RouteDescription, 'ProvinceID': partner.ProvinceID, 'DistrictID': partner.DistrictID, IsChoose: true
                                    };
                                    temp.push(item);
                                }
                            });
                        } else if (objOrder.IsCreatePartner) {
                            var item = {
                                'PartnerID': -1, 'PartnerCode': objOrder.PartnerCode, 'PartnerName': objOrder.PartnerName, 'LocationCode': objOrder.LocationToCode, CustomerID: objOrder.CustomerID,
                                'LocationAddress': objOrder.LocationToAddress, 'EconomicZone': objOrder.EconomicZone, 'RoutingAreaCode': objOrder.RoutingAreaCode, 'RouteDescription': "", 'ProvinceID': null, 'DistrictID': null, IsChoose: true
                            };
                            temp.push(item);
                        }
                    }
                }
            }
        });
        //$.each(data, function (i, row) {
        //    if (row.IsLocationToFail) {
        //        if (row.ListPartnerLocation.length > 0) {
        //            $.each(row.ListPartnerLocation, function (j, partner) {
        //                if (partner.PartnerID > 0) {
        //                    var item = {
        //                        'PartnerID': partner.PartnerID, 'PartnerCode': partner.PartnerCode, 'PartnerName': partner.PartnerName, 'LocationCode': partner.LocationCode, CustomerID: partner.CustomerID,
        //                        'LocationAddress': partner.LocationAddress, 'EconomicZone': partner.EconomicZone, 'RoutingAreaCode': partner.RoutingAreaCode, 'RouteDescription': partner.RouteDescription, 'ProvinceID': partner.ProvinceID, 'DistrictID': partner.DistrictID, IsChoose: true
        //                    };
        //                    temp.push(item);
        //                }
        //            });
        //        } else {
        //            var item = {
        //                'PartnerID': -1, 'PartnerCode': row.PartnerCode, 'PartnerName': row.PartnerName, 'LocationCode': row.LocationToCode, CustomerID: row.CustomerID,
        //                'LocationAddress': row.LocationToAddress, 'EconomicZone': row.EconomicZone,'RoutingAreaCode': row.RoutingAreaCode, 'RouteDescription': "", 'ProvinceID': null, 'DistrictID': null, IsChoose: true
        //            };
        //            temp.push(item);
        //        }
        //    }
        //});
        if (temp.length > 0)
            dataRes.push(temp[0]);
        for (var i = 0; i < temp.length; i++) {
            var flag = false;
            for (var j = 0; j < dataRes.length; j++) {
                if (dataRes[j].PartnerCode == temp[i].PartnerCode && dataRes[j].LocationAddress == temp[i].LocationAddress) {
                    flag = true;
                    break;
                }
            }
            if (!flag)
                dataRes.push(temp[i]);
    }
        if (_ORDOrder_Plan_ExcelOnline.Data.Province.length > 0) {
            $.each(dataRes, function (i, o) {
                o.DataProvince = _ORDOrder_Plan_ExcelOnline.Data.Province;
                if (o.ProvinceID == null || o.ProvinceID <= 0)
                    o.ProvinceID = _ORDOrder_Plan_ExcelOnline.Data.Province[0].ID;
                o.DataDistrict = $.grep(_ORDOrder_Plan_ExcelOnline.Data.District, function (v) { return o.ProvinceID == v.ProvinceID });
                if (o.DataDistrict.length > 0) {
                    if (o.DistrictID == null || o.DistrictID <= 0)
                        o.DistrictID = o.DataDistrict[0].ID;
                }
            })
        }

        $timeout(function () {
            win.center().open();
            $scope.excel_location_gridOptions.dataSource.data(dataRes);
            $rootScope.IsLoading = false;
        }, 1)
    }
    
    $scope.excelLocationHasChoose = false;

    $scope.excel_location_gridOptions = {
        dataSource: Common.DataSource.Local({
            data: [],
            pageSize: 0,
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number', editable: false },
                    IsChoose: { type: 'boolean', },
                }
            }
        }),
        height: '100%', groupable: false, pageable: false, sortable: true, columnMenu: false, filterable: { mode: 'row' }, resizable: true,
        columns: [
            //{
            //    title: ' ', width: '35px',
            //    headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,excel_location_grid,excel_location_GridChoose_Change)" />',
            //    headerAttributes: { style: 'text-align: center;' }, attributes: { style: 'text-align: center;' },
            //    template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,excel_location_grid,excel_location_GridChoose_Change)" />',
            //    filterable: false, sortable: false
            //},
            { field: 'PartnerCode', width: '80px', title: '{{RS.CUSPartner.PartnerCode}}', sortable: true, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'PartnerName', width: '150px', title: '{{RS.CATPartner.PartnerName}}', sortable: true, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'LocationCode', width: '150px', title: '{{RS.CUSLocation.Code}}', template: '<input type="text" class="k-textbox "  ng-model="dataItem.LocationCode"  style="width: 100%"></input>', sortable: true, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'LocationAddress', width: '200px', title: '{{RS.CATLocation.Address}}', template: '<input type="text" class="k-textbox "  ng-model="dataItem.LocationAddress"  style="width: 100%"></input>', sortable: true, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { width: '150px', title: '{{RS.CATProvince.ProvinceName}}', template: '<select ng-model="dataItem.ProvinceID" k-index="0" focus-k-combobox kendo-combo-box k-data-text-field="\'ProvinceName\'" k-data-value-field="\'ID\'" k-filter="\'contains\'" k-auto-bind="true" k-min-length="3" k-data-source="dataItem.DataProvince" style="width: 100%" k-on-change="onLocation_ProvinceChange(kendoEvent,dataItem)"></select>', sortable: false },
            { width: '150px', title: '{{RS.CATDistrict.DistrictName}}', template: '<select ng-model="dataItem.DistrictID" k-index="0" focus-k-combobox kendo-combo-box k-data-text-field="\'DistrictName\'" k-data-value-field="\'ID\'" k-filter="\'contains\'" k-auto-bind="true" k-min-length="3" k-data-source="dataItem.DataDistrict" style="width: 100%" k-on-data-bound="onLocation_DistrictBound(kendoEvent,dataItem)"></select>', sortable: false },
            { field: 'EconomicZone', width: '200px', title: '{{RS.CATLocation.EconomicZone}}', sortable: true, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'RoutingAreaCode', width: '200px', title: '{{RS.CATLocation.RoutingAreaCode}}', template: '<input type="text" class="k-textbox "  ng-model="dataItem.RoutingAreaCode"  style="width: 100%"></input>', sortable: true, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'RouteDescription', width: '200px', title: '{{RS.ORDOrder_ExcelOnline.RouteDescription}}', sortable: true, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { title: ' ', filterable: false, sortable: false }
        ]
    }

    $scope.excel_location_GridChoose_Change = function ($event, grid, hasChoose) {
        $scope.excelLocationHasChoose = hasChoose;
    }

    $scope.onLocation_ProvinceChange = function (event, item) {
        var cbb = event.sender;
        var obj = cbb.dataItem(cbb.select());
        if (Common.HasValue(obj)) {
            var data = $.grep(_ORDOrder_Plan_ExcelOnline.Data.District, function (o) { return o.ProvinceID == obj.ID });
            item.DataDistrict = data;
            item.ChangeProvice = true;
        } else {
            item.DataDistrict = [];
            item.ChangeProvice = true;
        }
    }

    $scope.onLocation_DistrictBound = function (event, item) {
        if (item.ChangeProvice) {
            item.ChangeProvice = false;
            var cbb = event.sender;
            cbb.select(0);
            $timeout(function () {
                var obj = cbb.dataItem(cbb.select());
                if (Common.HasValue(obj)) {
                    item.DistrictID = obj.ID;
                } else {
                    item.DistrictID = -1;
                }
            }, 50)
        }
    }

    Common.ALL.Get($http, {
        url: Common.ALL.URL.Province,
        success: function (res) {
            _ORDOrder_Plan_ExcelOnline.Data.Province = res;
        }
    });

    Common.ALL.Get($http, {
        url: Common.ALL.URL.District,
        success: function (res) {
            _ORDOrder_Plan_ExcelOnline.Data.District = res;
        }
    });
    
    $scope.Excel_Location_Update_Click = function ($event, win, grid) {
        $event.preventDefault();

        var data = grid.dataSource.data();
        var isValid = true;
        var isError = false;

        $.each(data, function (i, v) {
            if (!(v.DistrictID > 0 && v.ProvinceID > 0))
                isValid = false;
            else if (v.LocationAddress == "") {
                isError = true;
                $rootScope.Message({ Msg: 'Chưa nhập địa chỉ', NotifyType: Common.Message.NotifyType.ERROR });
            }
        })

        if (!isValid) {
            $rootScope.Message({ Msg: 'Kiểm tra lại dữ liệu tỉnh thành, quận huyện!', NotifyType: Common.Message.NotifyType.WARNING });
        }

        if (isValid && !isError) {
            $rootScope.Message({
                Type: Common.Message.Type.Confirm,
                Msg: "Xác nhận lưu?",
                Ok: function () {
                    $rootScope.IsLoading = true;
                    Common.Services.Call($http, {
                        url: Common.Services.url.ORD,
                        method: _ORDOrder_Plan_ExcelOnline.URL.Location_Create,
                        data: {
                            dataLocation: data
                        },
                        success: function (res) {
                            $rootScope.IsLoading = false;
                            Common.Services.Error(res, function () {
                                win.close();
                                $rootScope.Message({ Msg: 'Thành công! Tải lại dữ liệu excel', NotifyType: Common.Message.NotifyType.INFO });
                                $scope.Excel_Check();
                            })
                        }
                    });
                }
            });
        } 
    }

    $scope.Location_Zoom_Click = function ($event, win) {
        $event.preventDefault();
        $scope.IsFullScreen = true;

        win.setOptions({ draggable: false });
        win.maximize();
        win.center().open();
    }

    $scope.Location_Minimize_Click = function ($event, win) {
        $event.preventDefault();
        $scope.IsFullScreen = false;
        win.setOptions({ draggable: true });
        win.center().open();
    }
    //#endregion
    
    //#region Product

    //Cbo GOP
    $scope.GOP_CbbOptions = {
        autoBind: true,
        valuePrimitive: true,
        ignoreCase: true,
        dataTextField: 'GroupName',
        dataValueField: 'ID',
        index: 0,
        change: function (e) {
            var val = this.value();
            var name = this.text();
            var gridEditElement = $scope.excel_product_grid.editable.element;
            var dataItem = $scope.excel_product_grid.dataItem(gridEditElement.closest('tr'))
            if (dataItem != null) {
                dataItem.GroupOfProductID = val;
                dataItem.GroupName = name;
            }
        },
        dataSource: Common.DataSource.Local({ data: [] })
    };

    //CBo UOM
    $scope.Packing_CbbOptions = {
        autoBind: true,
        valuePrimitive: true,
        ignoreCase: true,
        dataTextField: 'Code',
        dataValueField: 'ID',
        index: 0,
        change: function (e) {
            if (!Common.HasValue(e.sender.dataItem(e.item)) || this.value() == "") {
                $rootScope.Message({ Msg: 'Dữ liệu không thể thiếu', NotifyType: Common.Message.NotifyType.SUCCESS });
                $scope.ProductData.PackingID = "";
                this.open();
                this.select(0);
            } else {
                var val = this.value();
                var name = this.text();
                var gridEditElement = $scope.excel_product_grid.editable.element;
                var dataItem = $scope.excel_product_grid.dataItem(gridEditElement.closest('tr'))
                if (dataItem != null) {
                    dataItem.PackingID = val;
                    dataItem.PackingName = name;
                }
            }

        },
        dataSource: Common.DataSource.Local({ data: [] })
    };

    Common.ALL.Get($http, {
        url: Common.ALL.URL.ALL_CATPackingGOP,
        success: function (res) {
            $scope.Packing_CbbOptions.dataSource.data(res);
            $scope.IsLoading = false;
        }
    });

    //win
    $scope.Product_AddClick = function ($event) {
        $event.preventDefault();
        $scope.Product_Click($event, $scope.excel_product_win, _ORDOrder_Plan_ExcelOnline.Data.CheckResult);
    }

    $scope.Product_Click = function ($event, win, data) {
        if (Common.HasValue($event))
            $event.preventDefault();

        $rootScope.IsLoading = true;

        var dataRes = [];
        var temp = [];
        $.each(data, function (i, row) {
            if (Common.HasValue(row)) {
                if (row.ListProductNew.length > 0) {
                    $.each(row.ListProductNew, function (j, o) {
                        var item = {
                            'GroupOfProductID': o.GroupOfProductID, 'ProductCode': o.ProductCode, 'ProductName': o.ProductName, 'Description': o.Description, 'PackingID': o.PackingID,
                            'Length': o.Length, 'Height': o.Height, 'Width': o.Width, 'CBM': o.CBM, 'Weight': o.Weight, 'IsKg': o.IsKg, 'IsDefault': o.IsDefault, 'TempMin': o.TempMin
                            , 'TempMax': o.TempMax, 'GroupName': o.GroupName, 'PackingName': o.PackingName, IsChoose: true
                        };
                        temp.push(item);
                    });
                }
            }
        });
        if (temp.length > 0)
            dataRes.push(temp[0]);
        for (var i = 0; i < temp.length; i++) {
            var flag = false;
            for (var j = 0; j < dataRes.length; j++) {
                if (dataRes[j].ProductCode == temp[i].ProductCode && dataRes[j].GroupOfProductID == temp[i].GroupOfProductID) {
                    flag = true;
                    break;
                }
            }
            if (!flag)
                dataRes.push(temp[i]);
        }
        //cbo GOP
        var cusID = -1;
        if ($scope.Template.CustomerID != $scope.Template.SYSCustomerID) {
            cusID = $scope.Template.CustomerID;
        }
        Common.Services.Call($http, {
            url: Common.Services.url.ORD,
            method: _ORDOrder_Plan_ExcelOnline.URL.GOPRead,
            data: {
                id: cusID,
            },
            success: function (res) {
                $scope.GOP_CbbOptions.dataSource.data(res);
            }
        });

        $timeout(function () {
            win.center().open();
            $scope.excel_product_gridOptions.dataSource.data(dataRes);
            $rootScope.IsLoading = false;
        }, 1)
    }

    $scope.excelProductHasChoose = false;
    $scope.excel_product_gridOptions = {
        dataSource: Common.DataSource.Local({
            data: [],
            pageSize: 0,
            model: {
                id: 'ID',
                fields: {
                    GroupOfProductID: { type: 'number'},
                    ProductCode: { type: 'string' },
                    ProductName: { type: 'string' },
                    Description: { type: 'string' },
                    PackingID: { type: 'number' },
                    Length: { type: 'number' },
                    Height: { type: 'number' },
                    Width: { type: 'number' },
                    CBM: { type: 'number' },
                    Weight: { type: 'number' },
                    IsKg: { type: 'boolean', },
                    IsDefault: { type: 'boolean', },
                    TempMin: { type: 'number' },
                    TempMax: { type: 'number' },
                    IsChoose: { type: 'boolean', },
                }
            }
        }),
        height: '100%', groupable: false, pageable: false, sortable: true, columnMenu: false, filterable: false, resizable: true,
        columns: [
            {
                title: ' ', width: '35px',
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,excel_product_grid,excel_product_GridChoose_Change)" />',
                headerAttributes: { style: 'text-align: center;' }, attributes: { style: 'text-align: center;' },
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,excel_product_grid,excel_product_GridChoose_Change)" />',
                filterable: false, sortable: false
            },
            {
                field: 'GroupOfProductID', title: 'Nhóm sản phẩm', width: 150, sortable: false,
                template: '<input  class="cus-combobox" focus-k-combobox ng-model="dataItem.GroupOfProductID" kendo-combo-box k-options="GOP_CbbOptions" focus-k-combobox/>',
                //editor: function (container, options) {
                //    $('<input  class="cus-combobox" focus-k-combobox name="' + options.field + '" kendo-combo-box k-options="GOP_CbbOptions"/>').appendTo(container)
                //}
            },
            {
                field: 'ProductCode', width: 150,
                title: 'Mã sản phẩm', template: '<input type="text" class="k-textbox "  ng-model="dataItem.ProductCode"  style="width: 100%"></input>',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'ProductName', width: 150,
                title: 'Sản phẩm', template: '<input type="text" class="k-textbox "  ng-model="dataItem.ProductName"  style="width: 100%"></input>',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'PackingID', title: 'UOM', width: 150, sortable: false,
                template: '<input class="cus-combobox" focus-k-combobox  ng-model="dataItem.PackingID" kendo-combo-box k-options="Packing_CbbOptions" focus-k-combobox/>'
                //editor: function (container, options) {
                //    $('<input class="cus-combobox" focus-k-combobox  name="' + options.field + '" kendo-combo-box k-options="Packing_CbbOptions"/>').appendTo(container)
                //}
            },
            {
                field: 'IsKg', width: 90, title: 'Tính theo Kg', attributes: { style: "text-align: center; " },
                template: "<input type='checkbox' ng-model='dataItem.IsKg' ></input>",
                filterable: {
                    cell: {
                        template: function (container) {
                            container.element.kendoComboBox({
                                dataSource: [{ Text: 'Có', Value: true }, { Text: 'Không', Value: false }, { Text: 'Tất cả', Value: '' }],
                                dataTextField: "Text",
                                dataValueField: "Value",
                            });
                        },
                        showOperators: false
                    }
                }
            },
            {
                field: 'IsDefault', width: 100, title: 'Hàng mặc định', attributes: { style: "text-align: center; " },
                template: "<input type='checkbox' ng-model='dataItem.IsDefault' ></input>",
                filterable: {
                    cell: {
                        template: function (container) {
                            container.element.kendoComboBox({
                                dataSource: [{ Text: 'Có', Value: true }, { Text: 'Không', Value: false }, { Text: 'Tất cả', Value: '' }],
                                dataTextField: "Text",
                                dataValueField: "Value",
                            });
                        },
                        showOperators: false
                    }
                }
            },
            {
                field: 'Length', width: 90, title: 'Dài (cm)', filterable: { cell: { operator: 'gte', showOperators: false } },
                template: '<input ng-model="dataItem.Length" class="k-textbox" type="number" style="width:100%" />'
            },
            {
                field: 'Width', width: 90, title: 'Rộng (cm)', filterable: { cell: { operator: 'gte', showOperators: false } },
                template: '<input ng-model="dataItem.Width" class="k-textbox" type="number" style="width:100%" />'
            },
            {
                field: 'Height', width: 90, title: 'Cao (cm)', filterable: { cell: { operator: 'gte', showOperators: false } },
                template: '<input ng-model="dataItem.Height" class="k-textbox" type="number" style="width:100%" />'
            },
            {
                field: 'CBM', width: 90, title: 'Thể tích (cm3)', filterable: { cell: { operator: 'gte', showOperators: false } },
                template: '<input ng-model="dataItem.CBM" class="k-textbox" type="number" style="width:100%" />'
            },
            {
                field: 'Weight', width: 90, title: 'Trọng tải (kg)', filterable: { cell: { operator: 'gte', showOperators: false } },
                template: '<input ng-model="dataItem.Weight" class="k-textbox" type="number" style="width:100%" />'
            },

            {
                field: 'Description', width: 150,
                title: 'Mô tả', template: '<input type="text" class="k-textbox "  ng-model="dataItem.Description"  style="width: 100%"></input>',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'TempMin', width: 120, title: 'Nhiệt độ tối đa (độ C)', filterable: { cell: { operator: 'gte', showOperators: false } },
                template: '<input ng-model="dataItem.TempMin" class="k-textbox" type="number" style="width:100%" />'
            },
            {
                field: 'TempMax', width: 120, title: 'Nhiệt độ tối thiểu (độ C)', filterable: { cell: { operator: 'gte', showOperators: false } },
                template: '<input ng-model="dataItem.TempMax" class="k-textbox" type="number" style="width:100%" />'
            },
            { title: ' ', filterable: false, sortable: false }
        ]
    }
    $scope.excel_product_GridChoose_Change = function ($event, grid, hasChoose) {
        $scope.excelProductHasChoose = hasChoose;
    }

    $scope.Excel_Product_Update_Click = function ($event, win, grid) {
        $event.preventDefault();
        var datasend = [];
        var data = grid.dataSource.data();
        var isValid = true;
        $.each(data, function (i, v) {
            if (v.IsChoose) {
                if (!Common.HasValue(v.GroupOfProductID) || v.GroupOfProductID < 0) {
                    $rootScope.Message({ Msg: 'Chưa chọn nhóm sản phẩm', NotifyType: Common.Message.NotifyType.ERROR });
                    isValid = false;
                }
                else if (v.ProductCode == "") {
                    $rootScope.Message({ Msg: 'Chưa nhập mã sản phẩm', NotifyType: Common.Message.NotifyType.ERROR });
                    isValid = false;
                }
                else if (v.ProductCode == "") {
                    $rootScope.Message({ Msg: 'Chưa nhập tên sản phẩm', NotifyType: Common.Message.NotifyType.ERROR });
                    isValid = false;
                }
                else if (!Common.HasValue(v.PackingID) || v.PackingID < 0) {
                    $rootScope.Message({ Msg: 'Chưa chọn UOM', NotifyType: Common.Message.NotifyType.ERROR });
                    isValid = false;
                }
                else {
                    datasend.push(v);
                }
            }
        })
        if (isValid) {
            $rootScope.Message({
                Type: Common.Message.Type.Confirm,
                Msg: "Xác nhận lưu?",
                Ok: function () {
                    $rootScope.IsLoading = true;
                    Common.Services.Call($http, {
                        url: Common.Services.url.ORD,
                        method: _ORDOrder_Plan_ExcelOnline.URL.Product_Create,
                        data: {
                            data: data
                        },
                        success: function (res) {
                            $rootScope.IsLoading = false;
                            Common.Services.Error(res, function () {
                                win.close();
                                $rootScope.Message({ Msg: 'Thành công! Tải lại dữ liệu excel', NotifyType: Common.Message.NotifyType.INFO });
                                $scope.Excel_Check();
                            })
                        }
                    });
                }
            });
        } else {
            $rootScope.Message({ Msg: 'Kiểm tra lại dữ liệu hàng hóa!', NotifyType: Common.Message.NotifyType.WARNING });
        }
    }
    //#endregion

    //#region LocationTo
    $scope.LocationTo_Click = function ($event, win) {
        $event.preventDefault();
        win.center().open();
        $timeout(function () {
            $scope.excel_location_grid.resize();
        }, 1);
    }

    $scope.excel_locationTo_gridOptions = {
        dataSource: Common.DataSource.Local({
            data: [],
            pageSize: 0,
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number', editable: false },
                    PartnerID: { type: 'number', editable: false },
                }
            }
        }),
        height: '100%', groupable: false, pageable: false, sortable: true, columnMenu: false, filterable: false, resizable: true, selectable: true,
        columns: [
            {
                field: 'OrderCode', width: '100px', title: '{{RS.ORDOrder.Code}}'
            },
            {
                field: 'CustomerCode', width: '120px', title: '{{RS.CUSCustomer.Code}}'
            },
            {
                field: 'SOCode', width: '100px', title: '{{RS.ORDGroupProduct.SOCode}}'
            },
            {
                field: 'DNCode', width: '100px', title: '{{RS.ORDGroupProduct.DNCode}}'
            },
            {
                field: 'TransportModeName', width: '100px', title: '{{RS.ORDOrder.TransportModeID}}'
            },
            {
                field: 'RequestDate', width: '110px', title: '{{RS.ORDOrder.RequestDate}}',
                template: "#=RequestDate==null?' ':Common.Date.FromJsonDMY(RequestDate)#",
            },
            {
                field: 'PartnerCode', width: '120px', title: '{{RS.CUSPartner.PartnerCode}}'
            },
            {
                field: 'LocationToAddress', width: '250px', title: '{{RS.ORDOrder_Excel.LocationToAddress}}'
            },
            //{
            //    field: 'ListLocation', width: '250px', title: '{{RS.ORDOrder_Excel.ListLocation}}', sortable: false,
            //    template: '<select ng-model="dataItem.LocationToID" focus-k-combobox kendo-combo-box k-data-text-field="\'Address\'" '
            //        + 'k-data-value-field="\'CUSLocationID\'"  k-filter="\'contains\'" k-auto-bind="true" k-min-length="3" '
            //        + 'k-on-data-bound="cboLocationToDataBound(kendoEvent, dataItem)" k-data-source="dataItem.ListLocationToAddress" style="width: 100%"></select>'
            //},
            {
                field: 'LocationToID', width: '250px', title: '{{RS.ORDOrder_Excel.ListLocation}}', sortable: false,
                template: '<input  class="cus-combobox" focus-k-combobox k-ng-model="dataItem.LocationToID" kendo-combo-box k-options="LocationAddress_CbbOptions" value="dataItem.LocationToID" focus-k-combobox k-data-source="dataItem.ListLocationToAddress"/>',
                
            },
            {
                field: 'IsLocationToFail', width: '110px', title: '{{RS.ORDOrder_Excel.IsLocationToFail}}',
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAllExcel_Check($event,excel_locationTo_grid,excel_locationTo_GridChoose_Change)" />',
                headerAttributes: { style: 'text-align: center;' }, attributes: { style: 'text-align: center;' },
                template: '<input type="checkbox" class="chkNew chkChooseExcel" #= IsLocationToFail && ((IsCreateLocation && PartnerID > 0) || (PartnerID < 0 && IsCreatePartner))? checked="checked" : ""# ng-click="gridChoose_Check($event,excel_locationTo_grid,excel_locationTo_GridChoose_Change)" #=!IsLocationToFail? "disabled" : ""#/>',
                attributes: { style: 'text-align: center;' }, headerAttributes: { style: 'text-align: center;' },
                filterable: false, sortable: false
            },
            { title: '', filterable: false, sortable: false }
        ],
        dataBound: function (e) {
            Common.Log('excel_locationTo_gridOptionsBound');

            $rootScope.IsLoading = true;

            var grid = this;
            $.each(grid.items(), function (i, tr) {
                var item = grid.dataItem(tr);
                if (Common.HasValue(item)) {
                    if (item.IsLocationToFail)
                        $(tr).addClass('newlocation');
                }
            })

            $rootScope.IsLoading = false;
        }
    }

    $scope.LocationAddress_CbbOptions = {
        autoBind: true,
        valuePrimitive: true,
        ignoreCase: true,
        dataTextField: 'Address',
        dataValueField: 'CUSLocationID',
        change: function (e) {
        },
    };

    $scope.excel_locationTo_GridChoose_Change = function ($event, grid, hasChoose) {
        var row = $event.target.closest('tr');
        var selectedItem = grid.dataItem(row);

        var isChecked = $($event.target).prop("checked");
        if(isChecked)
        {
            if (selectedItem.PartnerID < 0) {
                if (!selectedItem.IsCreatePartner)
                {
                    $rootScope.Message({ Msg: 'Khách hàng không có quyền thêm nhà phân phối.', NotifyType: Common.Message.NotifyType.WARNING });
                    $($event.target).prop("checked", false);
                }
            }
            else
            {
                if (!selectedItem.IsCreateLocation)
                {
                    $rootScope.Message({ Msg: 'Khách hàng không có quyền thêm điểm.', NotifyType: Common.Message.NotifyType.WARNING });
                    $($event.target).prop("checked", false);
                }
            }
        }
    }

    $scope.cboLocationToDataBound = function ($kendoEvent, item) {
        Common.Log('cboLocationToDataBound');
        if (item && !item.dirty) {
            $kendoEvent.sender.select(function (o) { return o.CUSLocationID == item.LocationToID });
            item.dirty = true;
            Common.Log(item);
        }
    }

    $rootScope.gridChooseAllExcel_Check = function ($event, grid, callback) {
        if ($event.target.checked == true) {
            grid.items().each(function () {

                var tr = this;
                var item = grid.dataItem(tr);
                if (item.IsLocationToFail != true) {
                    $(tr).find('td input.chkChooseExcel').prop('checked', true);
                    item.IsLocationToFail = true;
                }
            });
        }
        else {
            grid.items().each(function () {
                var tr = this;
                var item = grid.dataItem(tr);
                if (item.IsLocationToFail == true) {
                    $(tr).find('td input.chkChooseExcel').prop('checked', false);
                    item.IsLocationToFail = false;
                }
            });
        }

        if (Common.HasValue(callback)) {
            callback($event, grid, $event.target.checked);
        }
    };

    $scope.Excel_LocationTo_Update_Click = function ($event, win, grid) {
        $event.preventDefault();

        var data = grid.dataSource.data();

        if (data.length > 0) {
            $rootScope.Message({
                Type: Common.Message.Type.Confirm,
                Msg: "Xác nhận lưu?",
                Ok: function () {
                    $rootScope.IsLoading = true;
                    Common.Services.Call($http, {
                        url: Common.Services.url.ORD,
                        method: _ORDOrder_Plan_ExcelOnline.URL.ExcelOnline_LocationToSave,
                        data: {
                            id: $scope.excelOrderPlan.data.item.ID,
                            templateID: $scope.Template.ID,
                            lst: data,
                        },
                        success: function (res) {
                            $rootScope.IsLoading = false;
                            win.close();
                            $rootScope.Message({ Msg: 'Chỉnh sửa địa chỉ giao thành công.', NotifyType: Common.Message.NotifyType.INFO });
                        }
                    });
                }
            });
        } else {
            $rootScope.Message({ Msg: 'Không có dữ liệu', NotifyType: Common.Message.NotifyType.WARNING });
        }
    }
    //#endregion

    $scope.ShowSetting = function ($event, grid) {
        $event.preventDefault();

        $rootScope.ShowSetting({
            ListView: views.ORDOrder,
            event: $event,
            grid: grid,
            current: $state.current
        });
    };

    $scope.HideSetting = function ($event) {
        $event.preventDefault();

        $rootScope.HideSetting();
    }

    $scope.Close_Click = function ($event, win) {
        $event.preventDefault();

        win.close();
    }
}]);