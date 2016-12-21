/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />
/// <reference path="~/Scripts/Default.js" />


var _FLMInputFuel = {
    URL: {
        Read: 'FLMReceipt_List',
        Get_Vehicle: 'AllVehicle_List',
        Get_Suplier: 'AllSupplier_List',
        Get_Material: 'Material_Read',
        Excel_Export: 'FLMInputFuel_ExcelExport',
        Excel_Import: 'FLMInputFuel_ExcelImport',
        Excel_Check: 'FLMInputFuel_ExcelCheck',

        ApprovedList: 'FLMReceipt_ApprovedList',
        UnApprovedList: 'FLMReceipt_UnApprovedList',

        Delete: 'FLMReceipt_Delete',
        ImportEQM_ExcelInit: 'FLMReceipt_ImportEQM_ExcelInit',
        ImportEQM_ExcelChange: 'FLMReceipt_ImportEQM_ExcelChange',
        ImportEQM_ExcelImport: 'FLMReceipt_ImportEQM_ExcelImport',
        ImportEQM_ExcelApprove: 'FLMReceipt_ImportEQM_ExcelApprove',
    },
    Data: {
        Material: []
    },
    ExcelKey: {
        Resource_ImportEQM:"FLMWorkOrder_ImportEQM",
        ImportEQM:"FLMWorkOrder_ImportEQM"
    }
}

angular.module('myapp').controller('FLMInputFuel_IndexCtrl', ['$rootScope', '$scope', '$http', '$location', '$state', '$timeout', function ($rootScope, $scope, $http, $location, $state, $timeout) {

    Common.Log('FLMInputFuel_IndexCtrl');
    $rootScope.IsLoading = false;

    $scope.InputFuelHasChoose = false;
    $scope.TypeOfReceipt = 0;
    $scope.FLMInputFuelItem = null;
    $scope.ParamSearch = {
        DateFrom: (new Date()).addDays(-7),
        DateTo: (new Date()),
    }
  
    $scope.FLMInputFuel_Options = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.FLM,
            method: _FLMInputFuel.URL.Read,
            readparam: function () { return { 'dtFrom': $scope.ParamSearch.DateFrom, 'dtTo': $scope.ParamSearch.DateTo, }; },
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number', editable: false, },
                    DateReceipt: { type: 'date' },
                    CreatedDate: { type: 'date' },
                    ApprovedDate:{type:'date'},
                    IsChoose: { type: 'boolean' },
                    IsApproved: { type: 'string' },
                }
            }
        }),
        selectable: false, reorderable: true, editable: false,
        height: '100%', pageable: true, sortable: false, columnMenu: false, filterable: { mode: 'row' }, resizable: true,
        columns: [
            {
                title: ' ', width: '35px',
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,FLMInputFuel_Grid,FLMInputFuel_GridChoose_Change)" />',
                headerAttributes: { style: 'text-align: center;' }, attributes: { style: 'text-align: center;' },
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,FLMInputFuel_Grid,FLMInputFuel_GridChoose_Change)" />',
                filterable: false, sortable: false, locked: false,
            },
            {
                field: ' ', title: ' ', width: 50, template: '<a href="/" ng-click="FLMInputFuel_DeleteClick($event,dataItem)" class="k-button"><i class="fa fa-trash"></i></a>',
                filterable: false , locked: false,
            },
            {
                field: 'IsApproved', title: 'Đã duyệt', width: 80,
                template: '<input type="checkbox" #= IsApproved=="true" ? "checked=checked" : "" # disabled="disabled" />',
                attributes: { style: "text-align: center; " },
                filterable: {
                    cell: {
                        template: function (container) {
                            container.element.kendoComboBox({
                                dataSource: [{ Text: 'Tất cả', Value: '' }, { Text: 'Đã duyệt', Value: true }, { Text: 'Chưa duyệt', Value: false }],
                                dataTextField: "Text", dataValueField: "Value",
                            });
                        }, showOperators: false
                    }
                }
            },
            {
                field: 'Code', title: 'Mã phiếu', width: 150, template: '<a href="/" ng-click="FLMInputFuel_EditClick($event,FLMInputFuel_win,FLMInputFuel_Grid)" >#=Code#</a>',
                filterable: { cell: { operator: 'contains', showOperators: false } }, locked: false,
            },
            {
                field: 'DateReceipt', title: 'Ngày cấp phiếu', width: 140,
                filterable: { cell: { template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); }, operator: 'equal', showOperators: false } },
                template: '#=Common.Date.FromJsonDDMMYY(DateReceipt)#',
            },
            { field: 'TypeOfReceiptName', title: 'Loại phiếu', width: 150, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'ListRegNo', title: ' ', width: 150, filterable: false },
            { field: 'Note', title: 'Ghi chú', width: 150, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'CreatedBy', title: 'Người tạo', width: 150, filterable: { cell: { operator: 'contains', showOperators: false } } },
            {
                field: 'CreatedDate', title: 'Ngày tạo', width: 110,
                filterable: { cell: { template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); }, operator: 'equal', showOperators: false } },
                template: '#=Common.Date.FromJsonDDMMYY(CreatedDate)#',
            },
            { field: 'ApprovedBy', title: 'Người duyệt', width: 110, filterable: { cell: { operator: 'contains', showOperators: false } } },
            {
                field: 'ApprovedDate', title: 'Ngày duyệt', width: 110,
                filterable: { cell: { template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); }, operator: 'equal', showOperators: false } },
                template: '#=Common.Date.FromJsonDDMMYY(ApprovedDate)#',
            },
            
            { title: ' ', filterable: false, sortable: false }
        ]
    }

    $scope.FLMInputFuel_GridChoose_Change = function ($event, grid, hasChoose) {
        $scope.InputFuelHasChoose = hasChoose;
    }

    $scope.FLMInputFuel_ApprovedClick = function ($event, grid) {debugger
        $event.preventDefault();
        var data = grid.dataSource.data();
        var datasend = [];
        Common.Data.Each(data, function (o) {
            if (o.IsChoose && o.IsApproved == "false") datasend.push(o.ID);
        })
        if (datasend.length > 0) {
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.FLM,
                method: _FLMInputFuel.URL.ApprovedList,
                data: { lst: datasend },
                success: function (res) {
                    $rootScope.IsLoading = false;
                    $scope.FLMInputFuel_Options.dataSource.read();
                    $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                },
                error: function (res) {
                    $rootScope.IsLoading = false;
                }
            });

        }
    }

    $scope.FLMInputFuel_UnApprovedClick = function ($event, grid) {
        $event.preventDefault();
        var data = grid.dataSource.data();
        var datasend = [];
        Common.Data.Each(data, function (o) {
            if (o.IsChoose && o.IsApproved == "true") datasend.push(o.ID);
        })
        if (datasend.length > 0) {
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.FLM,
                method: _FLMInputFuel.URL.UnApprovedList,
                data: { lst: datasend },
                success: function (res) {
                    $rootScope.IsLoading = false;
                    $scope.FLMInputFuel_Options.dataSource.read();
                    $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                },
                error: function (res) {
                    $rootScope.IsLoading = false;
                }
            });

        }
    }

    $scope.FLMInputFuel_SearchClick = function ($event) {
        $event.preventDefault();
        if (!Common.HasValue($scope.ParamSearch.DateFrom) || !Common.HasValue($scope.ParamSearch.DateTo)) {
            $rootScope.Message({
                Type: Common.Message.Type.Alert,
                NotifyType: Common.Message.NotifyType.ERROR,
                Title: 'Thông báo',
                Msg: 'Vui lòng chọn Từ ngày- Đến ngày chính xác',
                Close: null,
                Ok: null
            })
        }
        else if ($scope.ParamSearch.DateFrom > $scope.ParamSearch.DateTo) {
            $rootScope.Message({
                Type: Common.Message.Type.Alert,
                NotifyType: Common.Message.NotifyType.ERROR,
                Title: 'Thông báo',
                Msg: 'Vui lòng chọn Từ ngày nhỏ hơn Đến ngày',
                Close: null,
                Ok: null
            })
        }
        else {
            $scope.FLMInputFuel_Options.dataSource.read();
        }
    }

    $scope.FLMInputFuel_AddNewClick = function ($event, win) {
        $event.preventDefault();
        win.center();
        win.open();
    }

    $scope.FLMInputFuel_EditClick = function ($event, win, grid) {
        $event.preventDefault();
        var tr = $($event.target).closest('tr');
        var ID = 0;
        var viewID = 0;
        var item = grid.dataItem(tr);
        if (Common.HasValue(item)) {
            ID = item.ID;
            viewID = item.viewID;
            switch (viewID) {
                default:
                    break;
                case 1: $state.go("main.FLMInputFuel.Fuel", { 'ID': ID }); break;
                case 2: $state.go("main.FLMInputFuel.Tranfer", { 'ID': ID }); break;
                case 3: $state.go("main.FLMInputFuel.RepairLarge", { 'ID': ID }); break;
                case 4: $state.go("main.FLMInputFuel.RepairLarge", { 'ID': ID }); break;
                case 5: $state.go("main.FLMInputFuel.ReceiptDisposal", { 'ID': ID }); break;
                case 6: $state.go("main.FLMInputFuel.ReceiptDisposal", { 'ID': ID }); break;
                case 7: $state.go("main.FLMInputFuel.ImportEQM", { 'ReceiptID': ID }); break;
            }
        }

    }

    $scope.FLMInputFuel_DeleteClick = function ($event, data) {
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
                    method: _FLMInputFuel.URL.Delete,
                    data: { id: data.ID },
                    success: function (res) {
                        $scope.FLMInputFuel_Options.dataSource.read();
                        $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                        $rootScope.IsLoading = false;
                    },
                    error: function (res) {
                        $rootScope.IsLoading = false;
                    }
                });
            }
        })
        

    }

    $scope.FLMInputFuel_win_CloseClick = function ($event, win) {
        $event.preventDefault();
        win.close()
    }

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

    $scope.cboTypeOfReceipt_Options = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: "contains", suggest: true,
        dataTextField: 'viewName', dataValueField: 'TypeOfReceipt',
        dataSource: [
            { viewName: "Chọn phiếu", TypeOfReceipt: 0 },
            { viewName: "Phiếu cấp nhiên liệu", TypeOfReceipt: 1 },
            { viewName: "Phiếu điều chuyển thiết bị", TypeOfReceipt: 2 },
            { viewName: "Phiếu sửa chữa", TypeOfReceipt: 3 },
            { viewName: "Phiếu thanh lí", TypeOfReceipt: 5 },
            { viewName: "Phiếu nhập thiết bị", TypeOfReceipt: 7 },
        ]
    }

    $scope.Add_TypeOfReceipt = function ($event) {
        $event.preventDefault();
        if ($scope.TypeOfReceipt == 0) {
            $rootScope.Message({ Msg: 'Chưa chọn loại phiếu', NotifyType: Common.Message.NotifyType.ERROR });
        } else if ($scope.TypeOfReceipt == 1) {
            $state.go('main.FLMInputFuel.Fuel', {ID : 0});
        } else if ($scope.TypeOfReceipt == 2) {
            $state.go('main.FLMInputFuel.Tranfer', { ID: 0 });
        } else if ($scope.TypeOfReceipt == 3) {
            $state.go("main.FLMInputFuel.RepairLarge", { 'ID': 0 });
        } else if ($scope.TypeOfReceipt == 5 || $scope.TypeOfReceipt == 6) {
            $state.go("main.FLMInputFuel.ReceiptDisposal", { 'ID': 0 });
        } else if ($scope.TypeOfReceipt == 7) {
            $state.go("main.FLMInputFuel.ImportEQM", { 'ReceiptID': 0 });
        }
    }

    $scope.ExcelImportEQM_Click = function ($event ) {
        $event.preventDefault();
        var lstMessageError = [];
        for (var i = 0; i < 32; i++) {
            var resource = $rootScope.RS[_FLMInputFuel.ExcelKey.Resource_ImportEQM + '_' + i];
            if (Common.HasValue(resource))
                lstMessageError.push(resource);
        }
        if (lstMessageError.length == 0) {
            lstMessageError = [
                '[Số xe] không được trống',
                '[Loại phương tiện] không được trống',
                '[Loại phương tiện] không tồn tại',
                '[Số xe-Loại phương tiện] không chính xác',
                '[Mã thiết bị] đã sử dụng',
                '[Mã thiết bị] bị trùng trên file',
                '[Tên thiết bị] không được trống và >500 kí tự',
                '[Loại thiết bị] không được trống',
                '[Loại thiết bị] không tồn tại',
                '[Ghi chú] không được >2000 kí tự',
                '[Ngày bắt đầu tính k/h] nhập sai ({0}_{1})',
                '[T/g tính k/h] nhập sai ({0}_{1})',
            ];
        }
        $rootScope.excelShare.Init({
            functionkey: _FLMInputFuel.ExcelKey.ImportEQM,
            params: {
                'dtFrom': $scope.ParamSearch.DateFrom,
                'dtTo': $scope.ParamSearch.DateTo,
            },
            rowStart: 1,
            colCheckChange: 9,
            url: Common.Services.url.FLM,
            methodInit: _FLMInputFuel.URL.ImportEQM_ExcelInit,
            methodChange: _FLMInputFuel.URL.ImportEQM_ExcelChange,
            methodImport: _FLMInputFuel.URL.ImportEQM_ExcelImport,
            methodApprove: _FLMInputFuel.URL.ImportEQM_ExcelApprove,
            lstMessageError: lstMessageError,

            Changed: function () {

            },
            Approved: function () {
                $scope.FLMInputFuel_Options.dataSource.read();
                $rootScope.Message({ Msg: 'Đã lưu', NotifyType: Common.Message.NotifyType.ERROR });
            }
        });
    };
}])