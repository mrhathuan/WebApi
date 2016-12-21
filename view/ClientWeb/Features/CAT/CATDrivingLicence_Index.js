/// <reference path="~/Scripts/jquery-1.9.1.intellisense.js" />
/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />

var _CATDrivingLicenceIndex = {
    URL: {
        Read: 'DrivingLicence_List',
        Get: 'DrivingLicence_Get',
        Save: 'DrivingLicence_Save',
        Delete: 'DrivingLicence_Delete',

        ExcelInit: 'DrivingLicence_ExcelInit',
        ExcelChange: 'DrivingLicence_ExcelChange',
        ExcelImport: 'DrivingLicence_ExcelImport',
        ExcelApprove: 'DrivingLicence_ExcelApprove',
    },
    Data: {
        Vehicle: []
    }
}

angular.module('myapp').controller('CATDrivingLicence_IndexCtrl', ['$rootScope', '$scope', '$http', '$location', '$state', '$timeout', '$stateParams', function ($rootScope, $scope, $http, $location, $state, $timeout, $stateParams) {
    if ($rootScope.IsPageComplete != true) return;
    Common.Log('CATDrivingLicence_IndexCtrl');
    $rootScope.IsLoading = false;

    $scope.Auth = $rootScope.GetAuth();
    $scope.Item = null;

    $scope.DrivingLicence_gridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.CAT,
            method: _CATDrivingLicenceIndex.URL.Read,
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                }
            }
        }),
        height: '99%', groupable: false, pageable: true, sortable: true, columnMenu: false, resizable: true,
        selectable: 'row', filterable: { mode: 'row' }, reorderable: true,
        columns: [
            {
                field: "Command", title: ' ', width: '90px', sortorder: 0, configurable: false, isfunctionalHidden: false,
                template: '<a href="/" ng-click="DrivingLicence_GridEdit_Click($event,dataItem,DrivingLicence_win,DrivingLicence_vform)" class="k-button"><i class="fa fa-pencil"></i></a>' +
                    '<a href="/" ng-click="DrivingLicence_GridDestroy_Click($event,dataItem)" class="k-button"><i class="fa fa-trash"></i></a>',
                filterable: false, sortable: false
            },
            {
                field: 'Code', width: 150, title: 'Mã bằng lái', sortorder: 1, configurable: true, isfunctionalHidden: false,
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'DrivingLicenceName', width: 150, title: 'Loại bằng lái', sortorder: 2, configurable: true, isfunctionalHidden: false,
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'Description', width: 200, title: 'Mô tả',sortorder: 3, configurable: true, isfunctionalHidden: false,
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
             { title: '', filterable: false, sortable: false, sortorder: 4, configurable: false, isfunctionalHidden: false }
        ]
    };


    $scope.numFeeBase_options = { format: 'n3', spinners: false, culture: 'en-US', min: 0, step: 1, decimals: 3, }

    $scope.DrivingLicence_AddNew = function ($event, win, vform) {
        $event.preventDefault();
        $scope.DrivingLicenceLoadItem(0, win, vform)
    }

    $scope.DrivingLicence_GridEdit_Click = function ($event, item, win, vform) {
        $event.preventDefault();
        $scope.DrivingLicenceLoadItem(item.ID, win, vform)
    }

    $scope.DrivingLicenceLoadItem = function (id, win, vform) {
        vform({ clear: true });
        $rootScope.IsLoading = true;
        Common.Services.Call($http, {
            url: Common.Services.url.CAT,
            method: _CATDrivingLicenceIndex.URL.Get,
            data: { id: id },
            success: function (res) {
                $rootScope.IsLoading = false;
                $scope.Item = res;
                vform({ clear: true })
                win.center().open();
            },
            error: function (res) {
                $rootScope.IsLoading = false;
            }
        });

    }

    $scope.DrivingLicence_GridDestroy_Click = function ($event, item) {
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
                    url: Common.Services.url.CAT,
                    method: _CATDrivingLicenceIndex.URL.Delete,
                    data: { item: item },
                    success: function (res) {
                        $scope.DrivingLicence_gridOptions.dataSource.read();
                        $rootScope.IsLoading = false;
                        $rootScope.Message({ Msg: "Đã xóa!" });
                    }, error: function (res) {
                        $rootScope.IsLoading = false;
                    }
                });
            }
        })

    }

    $scope.DrivingLicence_Save_Click = function ($event, win, vform) {
        $event.preventDefault();
        if (vform()) {
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.CAT,
                method: _CATDrivingLicenceIndex.URL.Save,
                data: { item: $scope.Item },
                success: function (res) {
                    $scope.DrivingLicence_gridOptions.dataSource.read();
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

    //combobox
    $scope.DrivingLicence_win_cboVehicleOptions = {
        autoBind: true,
        valuePrimitive: true,
        ignoreCase: true,
        filter: 'contains',
        suggest: true,
        dataTextField: 'ValueOfVar',
        dataValueField: 'ID',

        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    ValueOfVar: { type: 'string' },
                }
            }
        }),
        change: function (e) {
        }
    }

    Common.ALL.Get($http, {
        url: Common.ALL.URL.TypeOfVehicle,
        success: function (data) {
            var item = { ID: -1, ValueOfVar: '' };
            data.unshift(item);
            $scope.DrivingLicence_win_cboVehicleOptions.dataSource.data(data);
        }
    })

    $scope.Win_Close = function ($event, win) {
        $event.preventDefault();
        win.close();
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

    $scope.Back_Click = function ($event) {
        $event.preventDefault();

        $state.go("main.PUBAdminFleet.Index", _CUSContract_Price_DI_LoadMOQ.Params)
    }

    $scope.DrivingLicence_Excel_Click = function ($event) {
        $event.preventDefault();

        var lstMessageError = [];
        for (var i = 0; i < 3; i++) {
            var resource = $rootScope.RS[_FLMDriver.ExcelKey.Resource + '_' + i];
            if (Common.HasValue(resource))
                lstMessageError.push(resource);
        }
        if (lstMessageError.length == 0) {
            lstMessageError = [
                '[Mã] không được trống và > 50 ký tự',
                '[Mã] đã bị trùng',
                '[Tên] không được trống và > 50 ký tự',
            ];
        }

        $rootScope.excelShare.Init({
            functionkey: 'CATDrivingLicence_Index',
            params: {},
            rowStart: 1,
            colCheckChange: 3,
            url: Common.Services.url.CAT,
            methodInit: _CATDrivingLicenceIndex.URL.ExcelInit,
            methodChange: _CATDrivingLicenceIndex.URL.ExcelChange,
            methodImport: _CATDrivingLicenceIndex.URL.ExcelImport,
            methodApprove: _CATDrivingLicenceIndex.URL.ExcelApprove,
            lstMessageError: lstMessageError,

            Changed: function () {

            },
            Approved: function () {
                $scope.DrivingLicence_gridOptions.dataSource.read();
                $rootScope.Message({ Msg: 'Đã lưu', NotifyType: Common.Message.NotifyType.ERROR });
            }
        });
    };
}]);