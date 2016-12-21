/// <reference path="~/Scripts/jquery-1.9.1.intellisense.js" />
/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />                      

//#region URL
var _CATDriver = {
    URL: {
        Read: 'Driver_List',
        Delete: 'Driver_Delete',
        Save: 'Driver_Save',
        Get: 'Driver_Get',

        ExcelInit: 'Driver_ExcelInit',
        ExcelChange: 'Driver_ExcelChange',
        ExcelImport: 'Driver_ExcelImport',
        ExcelApprove: 'Driver_ExcelApprove',
    },
    ExcelKey: {
        Resource: "CATDriver_Excel",
        CATDriver: "CATDriver"
    }
}

//#endregion

angular.module('myapp').controller('CATDriver_IndexCtrl', ['$rootScope', '$scope', '$http', '$location', '$state', '$timeout', function ($rootScope, $scope, $http, $location, $state, $timeout) {
    if ($rootScope.IsPageComplete != true) return;
    Common.Log('CATDriver_IndexCtrl');
    $rootScope.IsLoading = false;
    $scope.DriverItem = null;

    $scope.Auth = $rootScope.GetAuth();
    $scope.CATDriver_gridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.CAT,
            method: _CATDriver.URL.Read,
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    Birthday:{type:'date'}
                }
            }
        }),
        height: '100%', pageable: true, sortable: true, columnMenu: false, resizable: true, reorderable: true, filterable: false,
        toolbar: kendo.template($('#CATDriver_grid_toolbar').html()),
        columns: [
            {
                field: 'Command', title: ' ', width: '85px', sortorder: 0, configurable: false, isfunctionalHidden: false,
                template: '<a href="/" ng-click="DriverEdit_Click($event,CATDriver_winPopup,CATDriver_grid)" class="k-button"><i class="fa fa-pencil"></i></a>' +
                    '<a href="/" ng-click="DriverDestroy_Click($event,CATDriver_grid)" class="k-button"><i class="fa fa-trash"></i></a>',
                filterable: false, sortable: false
            },
            {
                field: 'LastName', title: 'Họ và tên', width: 200, template: "#=LastName# #=FirstName#", sortorder: 1, configurable: true, isfunctionalHidden: false,
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'CardNumber', title: 'CMND', sortorder: 2, configurable: true, isfunctionalHidden: false,
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'Birthday', width: '130px', title: 'Ngày sinh', template: "#=Birthday==null?' ':Common.Date.FromJsonDMY(Birthday)#", sortorder: 3, configurable: true, isfunctionalHidden: false,
                filterable: {
                    cell: {
                        template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); },operator: 'equal', showOperators: false
                    }
                },
            },
            {
                field: 'Cellphone', title: 'Số điện thoại', sortorder: 4, configurable: true, isfunctionalHidden: false,
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'ListDrivingLicence', title: 'Giấy phép lái xe', sortorder: 5, configurable: true, isfunctionalHidden: false,
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'Note', title: 'Ghi chú', sortorder: 6, configurable: true, isfunctionalHidden: false,
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            { title: '', filterable: false, sortable: false, sortorder: 7, configurable: false, isfunctionalHidden: false }
        ]
    };

    $scope.DriverEdit_Click = function ($event, win, grid) {
        $event.preventDefault();
        var tr = $($event.target).closest('tr');
        var id = 0;
        var item = grid.dataItem(tr);
        if (Common.HasValue(item)) id = item.ID;
        $scope.LoadItem(win, item.ID);
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

    //#endregion

    $scope.CATDriverAddNew_Click = function ($event, win) {
        $event.preventDefault();
        $scope.LoadItem(win, 0);
    };

    $scope.LoadItem = function (win, id) {
        $rootScope.IsLoading = true;
        Common.Services.Call($http, {
            url: Common.Services.url.CAT,
            method: _CATDriver.URL.Get,
            data: { 'ID': id },
            success: function (res) {
                $scope.DriverItem = res;
                $rootScope.IsLoading = false;
                win.center();
                win.open();
            }
        });
    }

    $scope.DriverDestroy_Click = function ($event, grid) {
        $event.preventDefault();
        var tr = $($event.target).closest('tr');
        var item = grid.dataItem(tr);
        if (Common.HasValue(item)) {
            $rootScope.Message({
                Type: Common.Message.Type.Confirm,
                NotifyType: Common.Message.NotifyType.SUCCESS,
                Title: 'Thông báo',
                Msg: 'Bạn muốn xóa dữ liệu đã chọn?',
                Close: null,
                Ok: function () {
                    $rootScope.IsLoading = true;
                    Common.Services.Call($http, {
                        url: Common.Services.url.CAT,
                        method: _CATDriver.URL.Delete,
                        data: { 'ID': item.ID },
                        success: function (res) {
                            $scope.CATDriver_gridOptions.dataSource.read();
                            $rootScope.IsLoading = false;
                            $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                        }
                    });
                }
            })
        }
    }

    $scope.CATDriverSave_Click = function ($event, win, vform) {
        $event.preventDefault();
        if (vform()) {
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.CAT,
                method: _CATDriver.URL.Save,
                data: { item: $scope.DriverItem },
                success: function (res) {
                    $scope.CATDriver_gridOptions.dataSource.read();
                    $rootScope.IsLoading = false;
                    win.close();
                    $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                }
            });
        }
        else {
            $rootScope.Message({ Msg: 'Chưa nhập đủ thông tin', NotifyType: Common.Message.NotifyType.ERROR });
        }
    }

    $scope.Close_Click = function ($event, win) {
        $event.preventDefault();

        win.close();
    };

    $scope.Back_Click = function ($event) {
        $event.preventDefault();

        $state.go("main.PUBAdminCategory.Index", _CUSContract_Price_DI_LoadMOQ.Params)
    }


    $rootScope.CATDriver_Excel_Click = function ($event) {
        $event.preventDefault();

        var lstMessageError = [];
        for (var i = 0; i < 20; i++) {
            var resource = $rootScope.RS[_CATDriver.ExcelKey.Resource + '_' + i];
            if (Common.HasValue(resource))
                lstMessageError.push(resource);
        }
        if (lstMessageError.length == 0) {
            lstMessageError = [
                '[Mã] không được trống và > 50 ký tự',
                '[Mã] trùng',
                '[Họ tài xế] không được trống và > 50 ký tự',
                '[Tên tài xế] không được trống và > 50 ký tự',
                '[Số CMND] không được > 500 ký tự',
                '[Ngày sinh] nhập sai ({0}_{1})',
            ];
        }
        $rootScope.excelShare.Init({
            functionkey: _CATDriver.ExcelKey.CATDriver,
            params: {},
            rowStart: 2,
            colCheckChange: 15,
            url: Common.Services.url.CAT,
            methodInit: _CATDriver.URL.ExcelInit,
            methodChange: _CATDriver.URL.ExcelChange,
            methodImport: _CATDriver.URL.ExcelImport,
            methodApprove: _CATDriver.URL.ExcelApprove,
            lstMessageError: lstMessageError,
            Changed: function () {

            },
            Approved: function () {
                $scope.CATDriver_gridOptions.dataSource.read();
            }
        });
    };
}]);