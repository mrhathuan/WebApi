/// <reference path="~/Scripts/jquery-1.9.1.intellisense.js" />
/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />

var _REPOwnerAsset_Quota = {
    URL: {

        REPOwnerAssetQuota: 'REPOwner_Asset_Download',

        List: 'CUSSettingsReport_List',
        Save: 'CUSSettingsReport_Save',
        Delete: 'CUSSettingsReport_Delete',
        Template: 'CUSSettingsReport_Template',

        Excel_Quota: 'REPOwner_AssetQuota_Export',
        Search_Quota: 'REPOwner_AssetQuota'
    },
    Data: {
        DataQuota:[],
    },
    Param: {
    },
}

angular.module('myapp').controller('REPOwnerAsset_QuotaCtrl', ['$rootScope', '$scope', '$http', '$location', '$state', '$timeout', '$stateParams', function ($rootScope, $scope, $http, $location, $state, $timeout, $stateParams) {
    if ($rootScope.IsPageComplete != true) return;
    Common.Log('REPOwnerAsset_QuotaCtrl');
    $rootScope.IsLoading = false;

    $scope.Auth = $rootScope.GetAuth();
    $scope.Item = {
        ID: 0,
        TypeOfAssetID: -1,
        TypeExport: 0,
        TypeOfFilter: 1,

    };

    $scope.Search_Click = function ($event) {
        $event.preventDefault();
        $scope.Load_Search();
    };

    $scope.Load_Search = function () {
        $rootScope.IsLoading = true;
        Common.Services.Call($http, {
            url: Common.Services.url.REP,
            method: _REPOwnerAsset_Quota.URL.Search_Quota,
            data: { typeOfAssetID: $scope.Item.TypeExport },
            success: function (res) {
                $rootScope.IsLoading = false;
                _REPOwnerAsset_Quota.Data.DataQuota = res;
                $scope.InitGrid();
            },
            error: function (res) {
                $rootScope.IsLoading = false;
            }
        });
    }

    $scope.cboTypeExport_options = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: "contains", suggest: true,
        dataTextField: 'ValueOfVar', dataValueField: 'ID',
        dataSource: Common.DataSource.Local([], {
            id: 'ID',
            fields: {
                ValueOfVar: { type: 'string' },
                ID: { type: 'number' },
            }
        }),
        change: function (e) {
            $scope.Item.TypeExport = this.value();
            if ($scope.Item.TypeExport.length == 0) {
                $scope.Item.TypeOfAssetID = -1;
            }
        }
    }

    Common.ALL.Get($http, {
        url: Common.ALL.URL.ALL_SYSVarREPOwnerAsset,
        success: function (res) {
            $timeout(function () { 
                $scope.cboTypeExport_options.dataSource.data(res);
                $timeout(function () {
                    if (Common.HasValue(res))
                        $scope.Item.TypeExport = res[0].ID;
                    $scope.Load_Search();
                }, 300);
            }, 1);
        }
    })
    // export
    $scope.Excel_Export = function ($event) {
        $event.preventDefault();

        $rootScope.IsLoading = true;
        Common.Services.Call($http, {
            url: Common.Services.url.REP,
            method: _REPOwnerAsset_Quota.URL.Excel_Quota,
            data: { typeOfAssetID: $scope.Item.TypeExport },
            success: function (res) {
                $rootScope.IsLoading = false;
                $rootScope.DownloadFile(res);
            }
        })
    }


    $scope.InitGrid = function () {
        Common.Log("InitGrid");
        var Model = {
            id: 'ID',
            fields: {
                VehicleID: { type: 'number', editable: false },
                }
        };
        var GridColumn = [
            {
                field: 'RegNo', title: '<b>Số xe</b><br>[RegNo]', width: '120px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'TypeOfAssetName', title: '<b>Loại tài sản</b><br>[TypeOfAssetName]', width: '120px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
        ]

        $scope.fieldGrid = [];
        var array = [];
        var array_column = ["QuantityPerKM", "KMCurrent", "KMStart", "IsWarning"];
        var array_columnsl = ["Định mức", "KM hiện tại", "KM bắt đầu", "Cảnh báo"];
        debugger
        Common.Data.Each(_REPOwnerAsset_Quota.Data.DataQuota.ListKey, function (header) {
            for (var i = 0 ; i < array_column.length ; i++) {
                var field = header.KeyCode + array_column[i];
                field = field.replace(/-/g, '');
                field = field.replace(/[()]/g, '_');
                field = field.replace(/ /g, '_');
                var title = "<b>" + header.KeyCode + '-' + array_columnsl[i] + "</b><br>[" + header.KeyCode + '-' + array_column[i] + "]";
                if (array_column[i] == "IsWarning"){
                    Model.fields[field] = {
                        type: "bool",
                    };
                    GridColumn.push({
                        field: field, title: title, template: "<div style='text-align: center;'><input type='checkbox' disabled='disabled' ng-model='dataItem."+field+"'></input></div>", width: '150px', locked: false,
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
                    });
                }
                else {
                    Model.fields[field] = {
                        type: "number", editable: true,
                        filterable: { cell: { operator: 'equal', showOperators: false } }
                    };
                    GridColumn.push({
                        field: field, title: title, width: '150px', locked: false,
                    });
                }
                array.push(field);
            }
            $scope.fieldGrid.push(array);
        })

        GridColumn.push({ title: ' ', filterable: false, sortable: false })
        $scope.REPOwnerAsset_Quota_grid.setOptions({
            dataSource: Common.DataSource.Local({
                data: [],
                model: Model,
                pageSize: 20,
            }),
            height: '100%', pageable: true, sortable: true, columnMenu: false, resizable: true, reorderable: false, filterable: { mode: 'row' }, editable: false,
            columns: GridColumn
        })

        var dataGrid = [];
        Common.Data.Each(_REPOwnerAsset_Quota.Data.DataQuota.ListAsset, function (dataAsset) {
            var item = {};
            item["RegNo"] = dataAsset.RegNo;
            item["TypeOfAssetName"] = dataAsset.TypeOfAssetName;

            for (var i = 0 ; i < _REPOwnerAsset_Quota.Data.DataQuota.ListQuota.length; i++) {
                if (dataAsset.VehicleID == _REPOwnerAsset_Quota.Data.DataQuota.ListQuota[i].AssetID) {
                    for (var j = 0 ; j < array_column.length ; j++) {
                        var field = _REPOwnerAsset_Quota.Data.DataQuota.ListQuota[i].MaterialCode+'-'+ array_column[j]; 
                        field = field.replace(/-/g, '');
                        field = field.replace(/[()]/g, '_');
                        field = field.replace(/ /g, '_');
                        item[field] = _REPOwnerAsset_Quota.Data.DataQuota.ListQuota[i][array_column[j]];
                    } 
                }
            }

            dataGrid.push(item);
        })
        $scope.REPOwnerAsset_Quota_grid.dataSource.data(dataGrid);
        //$scope.dtpfilter();
        $rootScope.IsLoading = false;
    }

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
                    '<a href="/" ng-click="SettingReport_ActionClick($event,dataItem)" class="k-button"><i class="fa fa-download"></i></a>',
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
            method: _REPOwnerAsset_Quota.URL.List,
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
        } else {
            $scope.SettingItem = { ID: 0 };
            $scope.SettingItem.TypeExport = 67;
            $scope.SettingItem.TypeOfFilter = 1;
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
                    method: _REPOwnerAsset_Quota.URL.Save,
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
                            method: _REPOwnerAsset_Quota.URL.List,
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
                    method: _REPOwnerAsset_Quota.URL.Delete,
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
                            method: _REPOwnerAsset_Quota.URL.List,
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
    $scope.cboTypeOfFilter_options = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: "contains",
        suggest: true, dataTextField: 'ValueName', dataValueField: 'ID',
        dataSource: [
           { ID: 1, ValueName: 'Thông tin loại tài sản' },
           { ID: 2, ValueName: 'Định mức xe' },
        ],

        change: function (e) { }
    }
    $scope.SettingReport_ActionClick = function ($event, data) {
        $event.preventDefault();

        var request = Common.Request.CreateFromGrid($scope.REPOwnerAsset_Quota_grid.dataSource);
        $rootScope.IsLoading = true;
        Common.Services.Call($http, {
            url: Common.Services.url.REP,
            method: _REPOwnerAsset_Quota.URL.REPOwnerAssetQuota,
            data: { item: data },
            success: function (res) {
                debugger
                $rootScope.IsLoading = false;
                $rootScope.DownloadFile(res);
            }
        });
    }


    $scope.window_Close_Click = function ($event, win) {
        $event.preventDefault();
        win.close();
    }
    //#endregion

    //#region Action
    $scope.ShowSetting = function ($event, grid) {
        $event.preventDefault();

        $rootScope.ShowSetting({
            ListView: views.REPOwnerAsset,
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
