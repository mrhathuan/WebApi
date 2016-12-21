
/// <reference path="~/Scripts/jquery-1.9.1.intellisense.js" />
/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />

var _FLMDriver = {
    URL: {
        Read: 'FLMDriver_Read',
        Excel_Export: 'FLMDriver_Excel_Export',
        Excel_Save: 'FLMDriver_Excel_Save',
        Excel_Check: 'FLMDriver_Excel_Check',

        ExcelInit: 'FLMDriver_ExcelInit',
        ExcelChange: 'FLMDriver_ExcelChange',
        ExcelImport: 'FLMDriver_ExcelImport',
        ExcelApprove: 'FLMDriver_ExcelApprove',
    },
    ExcelKey: {
        Resource: "FLMDriver_Excel",
        Driver: "FLMDriver"
    }
}

angular.module('myapp').controller('FLMDriver_IndexCtrl', ['$rootScope', '$scope', '$http', '$location', '$state', '$timeout', function ($rootScope, $scope, $http, $location, $state, $timeout) {
    Common.Log('FLMDriver_IndexCtrl');

    $rootScope.IsLoading = false;
    $scope.ParamEdit = { DriverID: -1 }

    $scope.FLMDriver_Grid_Options = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.FLM,
            method: _FLMDriver.URL.Read,
            model: {
                id: 'ID',
                fields: {
                    ID: { field: 'ID', type: 'number', editable: false, nullable: false },
                    Birthday: { type: 'date' },
                    IsAssistant: { type: 'string' },
                    EmployeeCode: { type: 'string' }
                }
            }
        }),
        height: '100%', pageable: true, sortable: true, columnMenu: false, filterable: { mode: 'row' }, resizable: true,
        columns: [
            {
                field: "EmployeeCode", title: 'Mã tài xế', width: 100,
                template: '<a href="\" ng-click="FLMDriver_Edit_Click($event,dataItem)" style=" text-decoration:underline; color:blue">#=EmployeeCode#</a>',
                filterable: { cell: { showOperators: false, operator: "contains" } }
            },
            { field: 'LastName', title: 'Họ', width: 100, filterable: { cell: { showOperators: false, operator: "contains" } } },
            { field: 'FirstName', title: 'Tên', width: 100, filterable: { cell: { showOperators: false, operator: "contains" } } },
            {
                field: 'Birthday', title: 'Ngày sinh', template: "#=Birthday==null?' ':Common.Date.FromJsonDMY(Birthday)#", width: 120,
                filterable: {
                    cell: {
                        template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); }, operator: 'gte', showOperators: false
                    }
                },

            },
            {
                field: 'IsAssistant', title: 'Phụ lái?', width: 120,
                template: '<input type="checkbox" #= IsAssistant=="true" ? "checked=checked" : "" # disabled="disabled" />',
                attributes: { style: "text-align: center; " },
                filterable: {
                    cell: {
                        template: function (container) {
                            container.element.kendoComboBox({
                                dataSource: [{ Text: 'Phụ lái', Value: true }, { Text: 'Lái xe', Value: false }, { Text: 'Tấ cả', Value: '' }],
                                dataTextField: "Text", dataValueField: "Value",
                            });
                        }, showOperators: false
                    }
                }
            },
            { field: 'Cellphone', title: 'Điện thoại', width: 100, filterable: { cell: { showOperators: false, operator: "contains" } } },
            { field: 'CardNumber', title: 'CMND', width: 100, filterable: { cell: { showOperators: false, operator: "contains" } } },
            { field: 'Note', title: 'Ghi chú', width: 100, filterable: { cell: { showOperators: false, operator: "contains" } } }
        ]
    }

    $scope.FLMDriver_Edit_Click = function ($event, data) {
        $event.preventDefault();
        $scope.ParamEdit.DriverID = data.ID;
        $state.go('main.FLMDriver.Edit', $scope.ParamEdit)
    }

    $scope.FLMDriver_AddNew_Click = function ($event) {
        $event.preventDefault();
        $scope.ParamEdit.DriverID = -1;
        $state.go('main.FLMDriver.Edit', $scope.ParamEdit)
    }

    $scope.FLMDriver_Excel_Click = function ($event, grid) {
        $event.preventDefault();

        var lstMessageError = [];
        for (var i = 0; i < 20; i++) {
            var resource = $rootScope.RS[_FLMDriver.ExcelKey.Resource + '_' + i];
            if (Common.HasValue(resource)) 
                lstMessageError.push(resource);
        }
        if (lstMessageError.length == 0) {
            lstMessageError = [
                '[Mã tài xế] không được trống và > 50 ký tự',
                '[Mã tài xế] đã bị trùng',
                '[Họ] không được trống và > 50 ký tự',
                '[Tên] không được trống và > 50 ký tự',
                '[Lái xe/Phụ xe] nhập sai',
                '[Ghi chú] không được > 2000 ký tự',
                '[Số xe] không tồn tại',
                '[Điện thoại] không được > 500 ký tự',
                '[Số CMND] không được > 500 ký tự',
                '[Loại bằng lái] không tìm được',
                '[Số bằng lái] chưa có loại bằng',
                '[Lương cơ bản] nhập sai',
                '[Ngày nghỉ phép còn lại] nhập sai ({0}_{1})',
                '[Ngày vào làm] nhập sai ({0}_{1})',
                '[Ngày thôi việc] nhập sai ({0}_{1})',
                '[Phân loại] không được > 50 ký tự'
            ];
        }

        $rootScope.excelShare.Init({
            functionkey: _FLMDriver.ExcelKey.Driver,
            params: {},
            rowStart: 2,
            colCheckChange: 20,
            url: Common.Services.url.FLM,
            methodInit: _FLMDriver.URL.ExcelInit,
            methodChange: _FLMDriver.URL.ExcelChange,
            methodImport: _FLMDriver.URL.ExcelImport,
            methodApprove: _FLMDriver.URL.ExcelApprove,
            lstMessageError: lstMessageError,

            Changed: function () { },
            Approved: function () {
                $scope.FLMDriver_Grid_Options.dataSource.read();
            }
        });
    };

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
}])

