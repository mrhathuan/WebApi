/// <reference path="~/Scripts/jquery-1.9.1.intellisense.js" />
/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />

//#region URL
var _OPSTypeOfDITOGroupProductReturn = {
    URL: {
        Read: 'OPSTypeOfDITOGroupProductReturn_List',
        Delete: 'OPSTypeOfDITOGroupProductReturn_Delete',
        Save: 'OPSTypeOfDITOGroupProductReturn_Save',
        Get: 'OPSTypeOfDITOGroupProductReturn_Get',

        ExcelInit: 'OPSTypeOfDITOGroupProductReturn_ExcelInit',
        ExcelChange: 'OPSTypeOfDITOGroupProductReturn_ExcelChange',
        ExcelImport: 'OPSTypeOfDITOGroupProductReturn_ExcelImport',
        ExcelApprove: 'OPSTypeOfDITOGroupProductReturn_ExcelApprove',
    }
}

//#endregion

angular.module('myapp').controller('OPSTypeOfDITOGroupProductReturn_IndexCtrl', ['$rootScope', '$scope', '$http', '$location', '$state', '$timeout', function ($rootScope, $scope, $http, $location, $state, $timeout) {
    if ($rootScope.IsPageComplete != true) return;
    Common.Log('OPSTypeOfDITOGroupProductReturn_IndexCtrl');
    $rootScope.IsLoading = false;

    $scope.Auth = $rootScope.GetAuth();
    
    $scope.OPSTypeOfDITOGroupProductReturn_gridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.CAT,
            method: _OPSTypeOfDITOGroupProductReturn.URL.Read,
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    Code: { type: 'string' },
                    TypeName: { type: 'string' },
                }
            }
        }),
        height: '100%', pageable: true, sortable: true, columnMenu: false, resizable: true, reorderable: true, filterable: false,
        toolbar: kendo.template($('#OPSTypeOfDITOGroupProductReturn_grid_toolbar').html()),
        columns: [
            {
                field: "Command", title: ' ', width: '90px', sortorder: 0, configurable: false, isfunctionalHidden: false,
                template: '<a href="/" ng-click="Edit_Click($event,OPSTypeOfDITOGroupProductReturn_winPopup,OPSTypeOfDITOGroupProductReturn_grid)" class="k-button"><i class="fa fa-pencil"></i></a>' +
                    '<a href="/" ng-click="Destroy_Click($event,OPSTypeOfDITOGroupProductReturn_grid)" class="k-button"><i class="fa fa-trash"></i></a>',
                filterable: false, sortable: false
            },
            {
                field: 'Code', title: '{{RS.OPSTypeOfDITOGroupProductReturn.Code}}', width: 150, sortorder: 1, configurable: true, isfunctionalHidden: false,
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'TypeName', title: '{{RS.OPSTypeOfDITOGroupProductReturn.TypeName}}', width: 150, sortorder: 2, configurable: true, isfunctionalHidden: false,
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'TypeOfDITOGroupProductReturnStatusName', title: '{{RS.OPSTypeOfDITOGroupProductReturn.TypeOfDITOGroupProductReturnStatusName}}', width: 150, sortorder: 3, configurable: true, isfunctionalHidden: false,
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            { title: '', filterable: false, sortable: false, sortorder: 4, configurable: false, isfunctionalHidden: false }
        ]
    };

    $scope.Edit_Click = function ($event, win, grid) {
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
    $scope.LoadItem = function (win, id) {
        $rootScope.IsLoading = true;
        Common.Services.Call($http, {
            url: Common.Services.url.CAT,
            method: _OPSTypeOfDITOGroupProductReturn.URL.Get,
            data: { 'id': id },
            success: function (res) {
                $scope.Item = res;
                $rootScope.IsLoading = false;
                win.center();
                win.open();
            }
        });
    }

    $scope.Destroy_Click = function ($event, grid) {
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
                        method: _OPSTypeOfDITOGroupProductReturn.URL.Delete,
                        data: { 'item': item },
                        success: function (res) {
                            $scope.OPSTypeOfDITOGroupProductReturn_gridOptions.dataSource.read();
                            $rootScope.IsLoading = false;
                            $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                        }
                    });
                }
            })
        }
    }

    $scope.Save_Click = function ($event, win, vform) {
        $event.preventDefault();
        if (vform()) {
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.CAT,
                method: _OPSTypeOfDITOGroupProductReturn.URL.Save,
                data: { item: $scope.Item },
                success: function (res) {

                    $scope.OPSTypeOfDITOGroupProductReturn_gridOptions.dataSource.read();
                    $rootScope.IsLoading = false;
                    win.close();
                    $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                }
            });
        }
    }

    $scope.window_Close_Click = function ($event, win) {
        $event.preventDefault();
        win.close();
    };

    $scope.AddNew_Click = function ($event, win) {
        $event.preventDefault();
        $scope.LoadItem(win, 0);
    };

    $scope.cboStatus_Options = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: "contains", suggest: true,
        dataTextField: 'ValueOfVar', dataValueField: 'ID',
        dataSource: Common.DataSource.Local([], {
            id: 'ID',
            fields: {
                ValueOfVar: { type: 'string' },
                ID: { type: 'number' },
            }
        })
    }

    Common.ALL.Get($http, {
        url: Common.ALL.URL.TypeOfDITOGroupProductReturnStatus,
        success: function (data) {
            $scope.cboStatus_Options.dataSource.data(data);
        }
    })

    $scope.OPSTypeOfDITOGroupProductReturn_Excel_Click = function ($event) {
        $event.preventDefault();

        var lstMessageError = [];
        for (var i = 0; i < 5; i++) {
            var resource = $rootScope.RS[_FLMDriver.ExcelKey.Resource + '_' + i];
            if (Common.HasValue(resource))
                lstMessageError.push(resource);
        }
        if (lstMessageError.length == 0) {
            lstMessageError = [
                '[Mã] không được trống và > 50 ký tự',
                '[Mã] đã bị trùng',
                '[Tên] không được trống và > 50 ký tự',
                'Không có mã loại',
                'Không tồn tại mã loại trong hệ thống',
            ];
        }

        $rootScope.excelShare.Init({
            functionkey: 'OPSTypeOfDITOGroupProductReturn_Index',
            params: {},
            rowStart: 1,
            colCheckChange: 6,
            url: Common.Services.url.CAT,
            methodInit: _OPSTypeOfDITOGroupProductReturn.URL.ExcelInit,
            methodChange: _OPSTypeOfDITOGroupProductReturn.URL.ExcelChange,
            methodImport: _OPSTypeOfDITOGroupProductReturn.URL.ExcelImport,
            methodApprove: _OPSTypeOfDITOGroupProductReturn.URL.ExcelApprove,
            lstMessageError: lstMessageError,
            Changed: function () {

            },
            Approved: function () {
                $scope.OPSTypeOfDITOGroupProductReturn_gridOptions.dataSource.read();
                $rootScope.Message({ Msg: 'Đã lưu', NotifyType: Common.Message.NotifyType.ERROR });
            }
        });
    };

    $scope.Back_Click = function ($event) {
        $event.preventDefault();

        $state.go("main.PUBAdminCategory.Index", _CUSContract_Price_DI_LoadMOQ.Params)
    }
}]);