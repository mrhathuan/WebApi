/// <reference path="~/Scripts/jquery-1.9.1.intellisense.js" />
/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />

var _REPOwnerFixedCost_Detail = {
    URL: {
        Search: 'REPOwner_FixedCost_Detail',

        Detail_Export: 'REPOwnerFixedCost_Detail_Export',

        SettingList: 'CUSSettingsReport_List',
        SettingSave: 'CUSSettingsReport_Save',
        SettingDelete: 'CUSSettingsReport_Delete',
        SettingTemplate: 'CUSSettingsReport_Template',

        SettingAction: 'REPOwner_FixedCost_SettingDownload',
    },
    Param: {
    },
    Data: {
        TypeExport: [
            [
                { ID: 1, ValueName: 'Chi tiết' },
                { ID: 2, ValueName: 'Theo cột' },
            ]
        ]
    }
}

angular.module('myapp').controller('REPOwnerFixedCost_DetailCtrl', ['$rootScope', '$scope', '$http', '$location', '$state', '$timeout', '$stateParams', function ($rootScope, $scope, $http, $location, $state, $timeout, $stateParams) {
    if ($rootScope.IsPageComplete != true) return;
    Common.Log('REPOwnerFixedCost_DetailCtrl');
    $rootScope.IsLoading = false;

    $scope.Auth = $rootScope.GetAuth();
    $scope.SettingItem = { ID: 0 };

    $scope.Item = {
        DateFrom: Common.Date.AddDay(new Date(), -5),
        DateTo: new Date(),
        data:null,
    }

    $scope.REPOwnerFixedCost_gridOptions = {
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields: {
                    VehicleID: { type: 'number', editable: false },
                }
            },
            pageSize: 20
        }),
        height: '100%', pageable: true, sortable: { mode: 'multiple', allowUnsort: true }, columnMenu: false, resizable: true, reorderable: false, filterable: { mode: 'row' }, selectable: 'row', pageSize: 20,
        columns: [
            {
                field: 'VehicleCode', title: '<b>Xe</b><br>[VehicleCode]', width: '120px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'ReceiptNo', title: '<b>Số phiếu</b><br>[ReceiptNo]', width: '120px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'Month', title: '<b>Tháng</b><br>[Month]', width: '120px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'Year', title: '<b>Năm</b><br>[Year]', width: '120px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'Total', title: '<b>Số tiền</b><br>[Total]', width: '120px', template: '#=Total==null?" ":Common.Number.ToMoney(Total)#',
                filterable: { cell: { operator: 'gte', showOperators: false } }
            },

            { title: ' ', sortable: false, filterable: false, menu: false }
        ],
    };

    $scope.Search_Click = function ($event) {
        $event.preventDefault();
        debugger

        $rootScope.IsLoading = true;
        Common.Services.Call($http, {
            url: Common.Services.url.REP,
            method: _REPOwnerFixedCost_Detail.URL.Search,
            data: { dtfrom: $scope.Item.DateFrom, dtto: $scope.Item.DateTo },
            success: function (res) {
                $rootScope.IsLoading = false;
                angular.forEach(res, function (item, dix) {
                    item.Date = kendo.parseDate(item.Date);
                });

                $scope.REPOwnerFixedCost_gridOptions.dataSource.data(res);
            },
            error: function (res) {
                $rootScope.IsLoading = false;
            }
        });
    };

    //#region Setting Report
    $scope.SettingItem = { ID: 0 };
    $scope.SettingHasChoose = false;
    $scope.settingReport_GridOptions = {
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number', editable: false },
                    IsChoose: { type: 'boolean' },
                }
            },
            pageSize: 20
        }),
        height: '100%', pageable: true, sortable: true, columnMenu: false, filterable: { mode: 'row' }, resizable: true, editable: false, selectable: "row",
        columns: [
            {
                title: ' ', width: '35px',
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,settingReport_Grid,settingReport_GridChoose_Change)" />',
                headerAttributes: { style: 'text-align: center;' }, attributes: { style: 'text-align: center;' },
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,settingReport_Grid,settingReport_GridChoose_Change)" />',
                filterable: false, sortable: false
            },
            {
                title: ' ', width: '90px',
                template: '<a href="/" ng-click="settingReport_GridEdit_Click($event,SettingReport_win,dataItem,Setting_vform)" class="k-button"><i class="fa fa-pencil"></i></a>' +
                      '<a href="/" ng-click="SettingReport_ActionClick($event,dataItem,SettingDownload_win)" class="k-button"><i class="fa fa-download"></i></a>',
                filterable: false, sortable: false
            },
            {
                field: 'Name', title: 'Tên thiết lập', width: '150px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'CreateDate', title: 'Ngày tạo', width: '150px', template: '#=Common.Date.FromJsonDDMMYY(CreateDate)#',
                filterable: { cell: { template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); }, operator: 'gte', showOperators: false } },
            },
            {
                field: 'FileName', title: 'Tên File', width: '150px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            { title: '', filterable: false, sortable: false }
        ]
    }
    $scope.settingReport_GridChoose_Change = function ($event, grid, hasChoose) {
        $scope.SettingHasChoose = hasChoose;
    }
    $scope.SettingReport = function ($event, win) {
        $event.preventDefault();
        $rootScope.IsLoading = true;
        Common.Services.Call($http, {
            url: Common.Services.url.REP,
            method: _REPOwnerFixedCost_Detail.URL.SettingList,
            data: { functionID: $rootScope.FunctionItem.ID },
            success: function (res) {
                $rootScope.IsLoading = false;
                if (Common.HasValue(res)) {
                    angular.forEach(res, function (value, key) {
                        value.IsChoose = false;
                    });
                }
                $scope.settingReport_GridOptions.dataSource.data(res);
                win.center().open();
                $scope.settingReport_Grid.resize();
            }
        });
    }
    $scope.SettingReport_TabIndex = 0;
    $scope.SettingReport_TabOptions = {
        animation: {
            open: { effects: "fadeIn" }
        },
        select: function (e) {
            $timeout(function () {
                $scope.SettingReport_TabIndex = angular.element(e.item).data('tabindex');
                Common.Log("Select_Tab_" + $scope.TabIndex);
            }, 1)
        }
    }
    $scope.SettingReport_AddClick = function ($event, win, vform) {
        $event.preventDefault();
        $scope.LoadSettingItem(win, null, vform);
    }

    $scope.settingReport_GridEdit_Click = function ($event, win, data, vform) {
        $event.preventDefault();
        $scope.LoadSettingItem(win, data, vform);
    }

    $scope.LoadSettingItem = function (win, data, vform) {
        if (data != null) {
            $scope.SettingItem = data;
            if (Common.HasValue(data.ListCustomer)) {
                angular.forEach(data.ListCustomer, function (value, key) {
                    value.IsChoose = false;
                });
                $scope.SettingReport_Customer_Grid.dataSource.data(data.ListCustomer);
            }
            if (Common.HasValue(data.ListGroupProduct)) {
                angular.forEach(data.ListGroupProduct, function (value, key) {
                    value.IsChoose = false;
                });
                $scope.SettingReport_GroupProduct_Grid.dataSource.data(data.ListGroupProduct);
            }
        } else {
            $scope.SettingItem = { ID: 0 };
            $scope.SettingItem.TypeExport = 1;
            $scope.SettingItem.TypeDateRange = 1;
            $scope.SettingItem.StatusID = 1;
        }
        vform({ clear: true });
        win.center().open();
    }
    $scope.SettingReport_AddFileClick = function ($event) {
        $event.preventDefault();
        var functionID = $rootScope.FunctionItem.ID;
        $rootScope.UploadFile({
            IsImage: false, ID: functionID, AllowChange: true, ShowChoose: true,
            Type: Common.CATTypeOfFileCode.TEMPLATEREPORT,
            Complete: function (file) {
                if (file != null) {
                    $scope.SettingItem.FileID = file.ID;
                    $scope.SettingItem.FileName = file.FileName;
                    $scope.SettingItem.FilePath = file.FilePath;
                }
            }
        });
    };
    $scope.SettingReport_SaveClick = function ($event, win, vform) {
        $event.preventDefault();
        if (vform()) {
            var error = false;
            if (!Common.HasValue($scope.SettingItem.FileID)) {
                $rootScope.Message({
                    Type: Common.Message.Type.Alert,
                    NotifyType: Common.Message.NotifyType.ERROR,
                    Title: 'Thông báo',
                    Msg: 'Chưa chọn file template',
                    Close: null,
                    Ok: null
                })
                error = true;
            }
            if (!error) {
                $rootScope.IsLoading = true;
                $scope.SettingItem.ReferID = $rootScope.FunctionItem.ID;
                Common.Services.Call($http, {
                    url: Common.Services.url.REP,
                    method: _REPOwnerFixedCost_Detail.URL.SettingSave,
                    data: { item: $scope.SettingItem },
                    success: function (res) {
                        $rootScope.Message({
                            Type: Common.Message.Type.Notify,
                            NotifyType: Common.Message.NotifyType.SUCCESS,
                            Title: 'Thông báo',
                            Msg: 'Thành công',
                            Ok: null,
                            close: null,
                        })

                        Common.Services.Call($http, {
                            url: Common.Services.url.REP,
                            method: _REPOwnerFixedCost_Detail.URL.SettingList,
                            data: { functionID: $rootScope.FunctionItem.ID },
                            success: function (res) {
                                $rootScope.IsLoading = false;
                                $rootScope.IsLoading = false;
                                if (Common.HasValue(res)) {
                                    angular.forEach(res, function (value, key) {
                                        value.IsChoose = false;
                                    });
                                }
                                $scope.settingReport_GridOptions.dataSource.data(res);
                            }
                        });
                        win.close();
                    }
                });
            }
        }
    }
    $scope.SettingReport_Destroy_Click = function ($event, win) {
        $event.preventDefault();
        if (Common.HasValue($scope.SettingItem)) {
            var datasend = [];
            datasend.push($scope.SettingItem.ID);
            $scope.SettingDelete(win, datasend);
        }
    }
    $scope.settingReport_GridDestroy_Click = function ($event, grid) {
        $event.preventDefault();
        $event.preventDefault();
        var data = grid.dataSource.data();
        var datasend = [];
        Common.Data.Each(data, function (o) {
            if (o.IsChoose) datasend.push(o.ID);
        })
        if (datasend.length > 0) {
            $scope.SettingDelete(null, datasend);
        }
    }
    $scope.SettingDelete = function (win, datasend) {
        $rootScope.Message({
            Type: Common.Message.Type.Confirm,
            NotifyType: Common.Message.NotifyType.SUCCESS,
            Title: 'Thông báo',
            Msg: 'Bạn muốn xóa dữ liệu đã chọn?',
            Close: null,
            Ok: function () {
                $rootScope.IsLoading = true;
                Common.Services.Call($http, {
                    url: Common.Services.url.REP,
                    method: _REPOwnerFixedCost_Detail.URL.SettingDelete,
                    data: { lst: datasend },
                    success: function (res) {
                        $rootScope.Message({
                            Type: Common.Message.Type.Notify,
                            NotifyType: Common.Message.NotifyType.SUCCESS,
                            Title: 'Thông báo',
                            Msg: 'Thành công',
                            Ok: null,
                            close: null,
                        })

                        Common.Services.Call($http, {
                            url: Common.Services.url.REP,
                            method: _REPOwnerFixedCost_Detail.URL.SettingList,
                            data: { functionID: $rootScope.FunctionItem.ID },
                            success: function (res) {
                                $rootScope.IsLoading = false;
                                $rootScope.IsLoading = false;
                                if (Common.HasValue(res)) {
                                    angular.forEach(res, function (value, key) {
                                        value.IsChoose = false;
                                    });
                                }
                                $scope.settingReport_GridOptions.dataSource.data(res);
                            }
                        });
                        if (Common.HasValue(win))
                            win.close();
                    }
                });
            }
        })
    }
    // dinh dang thang
    $rootScope.DateMY = {
        format: 'MM/yyyy',
        parseFormats: ["yyyy-MM-ddTHH:mm:ss"],
        depth: "year",
        start: "year",
    };
    $scope.cboTypeExport_options = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: "contains",
        suggest: true, dataTextField: 'ValueName', dataValueField: 'ID',
        dataSource: [
            { ID: 1, ValueName: 'Báo cáo chi tiết' },
            { ID: 2, ValueName: 'Báo cáo theo xe' },
        ],
        change: function (e) { }
    }
    $scope.SettingReport_ActionClick = function ($event, data, win) {
        $event.preventDefault();

        $scope.Item.data = data;
        win.center().open();
    }

    $scope.SettingReport_ActionClickConfirm = function ($event, vform, win) {
        $event.preventDefault();

        var request = "";//Common.Request.CreateFromGrid($scope.settingReport_GridOptions.dataSource);
        var error = false;
        if (!Common.HasValue($scope.Item.DateFrom)) {
            error = true;
            $rootScope.Message({
                Type: Common.Message.Type.Alert,
                NotifyType: Common.Message.NotifyType.ERROR,
                Title: 'Thông báo',
                Msg: 'Tháng năm không thể trống',
                Close: null,
                Ok: null
            })
        }
        if (vform() && !error) {
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.REP,
                method: _REPOwnerFixedCost_Detail.URL.SettingAction,
                data: { item: $scope.Item.data, request: request, dtfrom: $scope.Item.DateFrom },
                success: function (res) {
                    $rootScope.IsLoading = false;
                    $rootScope.DownloadFile(res);
                    win.close();
                }
            });
        }
    }


    $scope.window_Close_Click = function ($event, win) {
        $event.preventDefault();
        win.close();
    }

    // export
    $scope.Excel_Export = function ($event) {
        $event.preventDefault();

        $rootScope.IsLoading = true;
        Common.Services.Call($http, {
            url: Common.Services.url.REP,
            method: _REPOwnerFixedCost_Detail.URL.Detail_Export,
            data: {},
            success: function (res) {
                $rootScope.IsLoading = false;
                $rootScope.DownloadFile(res);
            }
        })
    }

    //#region Action
    $scope.ShowSetting = function ($event, grid) {
        $event.preventDefault();

        $rootScope.ShowSetting({
            ListView: views.REPOwnerFixedCost,
            event: $event,
            grid: grid,
            current: $state.current
        });
    };

    $scope.HideSetting = function ($event) {
        $event.preventDefault();

        $rootScope.HideSetting();
    };
    //#endregion
}]);