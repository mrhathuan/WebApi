/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />

var _REPOwnerCostVehicle_DetailColumn = {
    URL: {
        Search: 'REPOwner_VehicleFee_Cost_ColumnDetailData',

        SettingList: 'CUSSettingsReport_List',
        SettingSave: 'CUSSettingsReport_Save',
        SettingDelete: 'CUSSettingsReport_Delete',
        SettingTemplate: 'CUSSettingsReport_Template',

        SettingCusDeleteList: 'CUSSettingReport_CustomerDeleteList',
        SettingCusNotInList: 'CUSSettingReport_CustomerNotInList',
        SettingCusNotInSave: 'CUSSettingReport_CustomerNotInSave',

        SettingGOPDeleteList: 'CUSSettingReport_GroupOfProductDeleteList',
        SettingGOPNotInList: 'CUSSettingReport_GroupOfProductNotInList',
        SettingGOPNotInSave: 'CUSSettingReport_GroupOfProductNotInSave',

        SettingAction: 'REPOwner_VehicleFee_Cost_SettingDownload',

        Excel_Export: 'REPOwner_VehicleFeeColumnDetail_Export',
    },
    Data: {
        _listCost: [],
        _listData: [],
        _listCostKey: [],
    }
}

angular.module('myapp').controller('REPOwnerCostVehicle_DetailColumnCtrl', ['$rootScope', '$scope', '$http', '$location', '$state', '$timeout', '$stateParams', function ($rootScope, $scope, $http, $location, $state, $timeout, $stateParams) {
    if ($rootScope.IsPageComplete != true) return;
    Common.Log('REPOwnerCostVehicle_DetailColumnCtrl');
    $rootScope.IsLoading = false;

    $scope.Auth = $rootScope.GetAuth();

    $scope.Item = {
        DateFrom: Common.Date.AddDay(new Date(), -5),
        DateTo: new Date(),
    }


    $scope.dtpfilter = function () {
        $timeout(function () {
            $('.dtp-filter-from').kendoDatePicker({
                format: Common.Date.Format.DDMMYY,
                change: function (e) {
                    try {
                        var element = this.wrapper.parent();
                        var field = e.sender.element.data('field');
                        var dtp_To = $(element).find('.dtp-filter-to[data-field=' + field + ']').data('kendoDatePicker');
                        var f = this.value();
                        var t = dtp_To.value();

                        var f_filter = { field: field, operator: "gte", value: f };
                        var t_filter = { field: field, operator: "lte", value: t };
                        var filters = [];
                        if (Common.HasValue($scope.REPDIPL_gridOptions.dataSource.filter())) {
                            filters = $scope.REPDIPL_gridOptions.dataSource.filter().filters;
                            if (Common.HasValue(f)) {
                                var index = 0;
                                var isNew = true;
                                var field = f_filter.field;
                                var operator = f_filter.operator;
                                for (index = 0; index < filters.length; index++) {
                                    if (filters[index].field == field && filters[index].operator == operator) {
                                        isNew = false;
                                        break;
                                    }
                                }

                                if (isNew) {
                                    filters.push(f_filter);
                                }
                                else {
                                    filters[index] = f_filter;
                                }
                            }
                            else {
                                var field = f_filter.field;
                                var operator = f_filter.operator;
                                for (index = 0; index < filters.length; index++) {
                                    if (filters[index].field == field && filters[index].operator == operator) {
                                        filters.splice(index, 1);
                                        break;
                                    }
                                }
                            }
                            if (Common.HasValue(t)) {
                                var isNew = true;
                                var index = 0;
                                var field = t_filter.field;
                                var operator = t_filter.operator;
                                for (index = 0; index < filters.length; index++) {
                                    if (filters[index].field == field && filters[index].operator == operator) {
                                        isNew = false;
                                        break;
                                    }
                                }

                                if (isNew) {
                                    filters.push(t_filter);
                                }
                                else {
                                    filters[index] = t_filter;
                                }
                            }
                            else {
                                var field = t_filter.field;
                                var operator = t_filter.operator;
                                for (index = 0; index < filters.length; index++) {
                                    if (filters[index].field == field && filters[index].operator == operator) {
                                        filters.splice(index, 1);
                                        break;
                                    }
                                }
                            }
                        }
                        else {
                            if (Common.HasValue(f))
                                filters.push(f_filter);
                            if (Common.HasValue(t))
                                filters.push(t_filter);

                        }
                        $scope.REPDIPL_gridOptions.dataSource.filter(filters);
                    }
                    catch (e) {
                        $rootScope.Message({ Msg: 'Sai dữ liệu!' });
                    }
                }
            })

            $('.dtp-filter-to').kendoDatePicker({
                format: Common.Date.Format.DDMMYY,
                change: function (e) {
                    try {
                        var element = this.wrapper.parent();
                        var field = e.sender.element.data('field');
                        var dtp_From = $(element).find('.dtp-filter-from[data-field=' + field + ']').data('kendoDatePicker');
                        var f = dtp_From.value();
                        var t = this.value();

                        var f_filter = { field: field, operator: "gte", value: f };
                        var t_filter = { field: field, operator: "lte", value: t };

                        var filters = [];
                        if (Common.HasValue($scope.REPDIPL_gridOptions.dataSource.filter())) {
                            filters = $scope.REPDIPL_gridOptions.dataSource.filter().filters;
                            if (Common.HasValue(f)) {
                                var index = 0;
                                var isNew = true;
                                var field = f_filter.field;
                                var operator = f_filter.operator;
                                for (index = 0; index < filters.length; index++) {
                                    if (filters[index].field == field && filters[index].operator == operator) {
                                        isNew = false;
                                        break;
                                    }
                                }

                                if (isNew) {
                                    filters.push(f_filter);
                                }
                                else {
                                    filters[index] = f_filter;
                                }
                            }
                            else {
                                var field = f_filter.field;
                                var operator = f_filter.operator;
                                for (index = 0; index < filters.length; index++) {
                                    if (filters[index].field == field && filters[index].operator == operator) {
                                        filters.splice(index, 1);
                                        break;
                                    }
                                }
                            }
                            if (Common.HasValue(t)) {
                                var isNew = true;
                                var index = 0;
                                var field = t_filter.field;
                                var operator = t_filter.operator;
                                for (index = 0; index < filters.length; index++) {
                                    if (filters[index].field == field && filters[index].operator == operator) {
                                        isNew = false;
                                        break;
                                    }
                                }

                                if (isNew) {
                                    filters.push(t_filter);
                                }
                                else {
                                    filters[index] = t_filter;
                                }
                            }
                            else {
                                var field = t_filter.field;
                                var operator = t_filter.operator;
                                for (index = 0; index < filters.length; index++) {
                                    if (filters[index].field == field && filters[index].operator == operator) {
                                        filters.splice(index, 1);
                                        break;
                                    }
                                }
                            }
                        }
                        else {
                            if (Common.HasValue(f))
                                filters.push(f_filter);
                            if (Common.HasValue(t))
                                filters.push(t_filter);
                        }
                        $scope.REPDIPL_gridOptions.dataSource.filter(filters);
                    }
                    catch (e) {
                        $rootScope.Message({ Msg: 'Sai dữ liệu!' });
                    }
                }
            })
        }, 500);
    }
    $scope.SearchData = function () {
        $rootScope.IsLoading = true;
        Common.Services.Call($http, {
            url: Common.Services.url.REP,
            method: _REPOwnerCostVehicle_DetailColumn.URL.Search,
            data: { request: "", dtfrom: $scope.Item.DateFrom, dtto: $scope.Item.DateTo },
            success: function (res) {
                $rootScope.IsLoading = false;
                //angular.forEach(res, function (item, dix) {
                //    item.DateConfig = kendo.parseDate(item.DateConfig);
                //});
                _REPOwnerCostVehicle_DetailColumn.Data._listCost = res.ListCost;
                _REPOwnerCostVehicle_DetailColumn.Data._listData = res.ListData;
                _REPOwnerCostVehicle_DetailColumn.Data._listCostKey = res.ListCostKey;
                $scope.InitGrid();
            },
            error: function (res) {
                $rootScope.IsLoading = false;
            }
        });
    }

    $scope.SearchData();

    $scope.Search_Click = function ($event) {
        $event.preventDefault();
        $scope.SearchData();
    };

    $scope.InitGrid = function () {
        Common.Log("InitGrid");
        var Model = {
            id: 'ID',
            fields: {
                ID: { type: 'number', editable: false },
                IsAssistant: { type: 'string' },
            },
        }
        var GridColumn = [
            {
                field: 'VehicleCode', title: '<b>Xe</b><br>[VehicleCode]', width: '120px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'DateConfig', title: '<b>Ngày áp dụng</b><br>[DateConfig]', width: '150px', template: '#=Common.Date.FromJsonDDMMYY(DateConfig)#',
                filterable: {
                    cell: {
                        template: function (e) {
                            var element = e.element.parent();
                            element.empty();
                            $("<input class='dtp-filter-from' data-field='DateConfig' style='width:50%; float:left;' />").appendTo(element);
                            $("<input class='dtp-filter-to' data-field='DateConfig' style='width:50%; float:left;' />").appendTo(element);
                        }, showOperators: false
                    }
                },
            },
            {
                field: 'CostDepreciation', title: '<b>Chi phí khấu hao xe</b><br>[CostDepreciation]', width: '120px', template: '#=CostDepreciation==null?" ":Common.Number.ToNumber1(CostDepreciation)#',
                filterable: { cell: { operator: 'gte', showOperators: false } }
            },
            {
                field: 'CostDepreciationReceipt', title: '<b>Chi phí khấu hao phiếu</b><br>[CostDepreciationReceipt]', width: '120px', template: '#=CostDepreciationReceipt==null?" ":Common.Number.ToNumber1(CostDepreciationReceipt)#',
                filterable: { cell: { operator: 'gte', showOperators: false } }
            },
            {
                field: 'CostReceiptMaintence', title: '<b>Chi phí phiếu bảo trì</b><br>[CostReceiptMaintence]', width: '120px', template: '#=CostReceiptMaintence==null?" ":Common.Number.ToNumber1(CostReceiptMaintence)#',
                filterable: { cell: { operator: 'gte', showOperators: false } }
            },
            {
                field: 'CostReceiptMaterial', title: '<b>Chi phí vật liệu</b><br>[CostReceiptMaterial]', width: '120px', template: '#=CostReceiptMaterial==null?" ":Common.Number.ToNumber1(CostReceiptMaterial)#',
                filterable: { cell: { operator: 'gte', showOperators: false } }
            },
            {
                field: 'CostReceiptRegistry', title: '<b>Chi phí đăng kiểm</b><br>[CostReceiptRegistry]', width: '120px', template: '#=CostReceiptRegistry==null?" ":Common.Number.ToNumber1(CostReceiptRegistry)#',
                filterable: { cell: { operator: 'gte', showOperators: false } }
            },
            {
                field: 'CostReceiptRepairLarge', title: '<b>Chi phí sửa chữa lớn</b><br>[CostReceiptRepairLarge]', width: '120px', template: '#=CostReceiptRepairLarge==null?" ":Common.Number.ToNumber1(CostReceiptRepairLarge)#',
                filterable: { cell: { operator: 'gte', showOperators: false } }
            },
            {
                field: 'CostReceiptRepairSmall', title: '<b>Chi phí sửa chữa nhỏ</b><br>[CostReceiptRepairSmall]', width: '120px', template: '#=CostReceiptRepairSmall==null?" ":Common.Number.ToNumber1(CostReceiptRepairSmall)#',
                filterable: { cell: { operator: 'gte', showOperators: false } }
            },
            {
                field: 'CostStationDebit', title: '<b>Chi phí trạm</b><br>[CostStationDebit]', width: '120px', template: '#=CostStationDebit==null?" ":Common.Number.ToNumber1(CostStationDebit)#',
                filterable: { cell: { operator: 'gte', showOperators: false } }
            },
            {
                field: 'CostTrouble', title: '<b>Chi phí phát sinh</b><br>[CostTrouble]', width: '120px', template: '#=CostTrouble==null?" ":Common.Number.ToNumber1(CostTrouble)#',
                filterable: { cell: { operator: 'gte', showOperators: false } }
            },
            {
                field: 'CostQuota', title: '<b>Chi phí định mức</b><br>[CostQuota]', width: '120px', template: '#=CostQuota==null?" ":Common.Number.ToNumber1(CostQuota)#',
                filterable: { cell: { operator: 'gte', showOperators: false } }
            },
        ]

        $scope.fieldGrid = [];
        Common.Data.Each(_REPOwnerCostVehicle_DetailColumn.Data._listCostKey, function (item) {
            var field = 'C' + item.KeyCode;
            field = field.replace(/([ -.,*+?^$|(){}\[\]])/g, "_");
            Model.fields[field] = {
                type: "number", editable: true,
                filterable: { cell: { operator: 'equal', showOperators: false } }
            };
            GridColumn.push({
                field: field, title: item.KeyCode, width: 120, locked: false,
            });
        });

        $scope.rep_grid.setOptions({
            dataSource: Common.DataSource.Local({
                data: [],
                model: Model,
                pageSize: 20,
            }),
            height: '100%', pageable: true, sortable: true, columnMenu: false, resizable: true, reorderable: false, filterable: { mode: 'row' }, editable: false, 
            columns: GridColumn
        })

        GridColumn.push({ title: ' ', filterable: false, sortable: false })
        var dataGrid = [];
        Common.Data.Each(_REPOwnerCostVehicle_DetailColumn.Data._listData, function (data) {
            Common.Data.Each(_REPOwnerCostVehicle_DetailColumn.Data._listCostKey, function (header) {
                for (var i = 0 ; i < _REPOwnerCostVehicle_DetailColumn.Data._listCost.length ; i++) {
                    var item = _REPOwnerCostVehicle_DetailColumn.Data._listCost[i];
                    var date1 = new Date(data.DateConfig);
                    var date2 = new Date(item.DateConfig);
                    var field = header.KeyCode;
                    field = field.replace(/([ -.,*+?^$|(){}\[\]])/g, "_");
                    if (header.KeyCode == item.KeyCode && data.VehicleID == item.VehicleID && data.DateConfig == item.DateConfig) {
                        data['C' + field] = item.Cost;
                    }
                }
            });
            dataGrid.push(data);
        })
        $scope.rep_grid.dataSource.data(dataGrid);
        $scope.dtpfilter();
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
            method: _REPOwnerCostVehicle_DetailColumn.URL.SettingList,
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
                    method: _REPOwnerCostVehicle_DetailColumn.URL.SettingSave,
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
                            method: _REPOwnerCostVehicle_DetailColumn.URL.SettingList,
                            data: { functionID: $rootScope.FunctionItem.ID },
                            success: function (res) {
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
                    method: _REPOwnerCostVehicle_DetailColumn.URL.SettingDelete,
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
                            method: _REPOwnerCostVehicle_DetailColumn.URL.SettingList,
                            data: { functionID: $rootScope.FunctionItem.ID },
                            success: function (res) {
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
    $scope.cboTypeExport_options = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: "contains",
        suggest: true, dataTextField: 'ValueName', dataValueField: 'ID',
        dataSource: [
            { ID: 1, ValueName: 'Chi phí xe tải' },
            { ID: 2, ValueName: 'Chi phí xe Cont' },
            { ID: 3, ValueName: 'Chi phí theo cột' },
            { ID: 4, ValueName: 'Chi phí định mức xe' },
        ],
        change: function (e) { }
    }

    $scope.cboTypeDateRange_options = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: "contains",
        suggest: true, dataTextField: 'ValueName', dataValueField: 'ID',
        dataSource: [
            { ID: 1, ValueName: 'Theo tuần' },
            { ID: 2, ValueName: 'Theo tháng' },
            { ID: 3, ValueName: 'Theo ngày đã chọn' },
        ],
        change: function (e) {
        }
    }
    $scope.SettingReport_ActionClick = function ($event, data) {
        $event.preventDefault();

        var request = Common.Request.CreateFromGrid($scope.rep_grid.dataSource);
        $rootScope.IsLoading = true;
        Common.Services.Call($http, {
            url: Common.Services.url.REP,
            method: _REPOwnerCostVehicle_DetailColumn.URL.SettingAction,
            data: { item: data, dtfrom: $scope.Item.DateFrom, dtto: $scope.Item.DateTo, request: request },
            success: function (res) {
                $rootScope.IsLoading = false;
                $rootScope.DownloadFile(res);
            }
        });
    }
    //#region Customer
    $scope.CustomerHasChoose = false;
    $scope.SettingReport_Customer_GridOptions = {
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields: {
                    CustomerID: { type: 'number', editable: false },
                    IsChoose: { type: 'boolean' },
                }
            },
            pageSize: 20
        }),
        height: '100%', pageable: true, sortable: { mode: 'multiple', allowUnsort: true }, columnMenu: false, resizable: true, reorderable: false, filterable: { mode: 'row' }, selectable: 'row',
        columns: [
            {
                title: ' ', width: '35px',
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,SettingReport_Customer_Grid,customer_GridChoose_Change)" />',
                headerAttributes: { style: 'text-align: center;' }, attributes: { style: 'text-align: center;' },
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,SettingReport_Customer_Grid,customer_GridChoose_Change)" />',
                filterable: false, sortable: false
            },
            {
                field: 'CustomerCode', title: 'Mã khách hàng', width: '150px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'CustomerName', title: 'Tên khách hàng', width: '150px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            { title: '', filterable: false, sortable: false }
        ]
    }

    $scope.customer_GridChoose_Change = function ($event, grid, hasChoose) {
        $scope.CustomerHasChoose = hasChoose;
    }

    $scope.customer_AddNew = function ($event, win) {
        $event.preventDefault();
        win.center().open();
        $scope.customerNotIn_GridOptions.dataSource.read();
    }

    $scope.customerNotIn_GridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.REP,
            method: _REPOwnerCostVehicle_DetailColumn.URL.SettingCusNotInList,
            readparam: function () { return { lstCus: $scope.SettingItem.ListCustomer } },
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number', editable: false, nullable: true },
                    IsChoose: { type: 'boolean' },
                }
            }
        }),
        height: '100%', pageable: true, sortable: true, columnMenu: false, filterable: { mode: 'row' }, resizable: true, editable: false,
        columns: [
            {
                title: ' ', width: '35px',
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,customerNotIn_Grid,customerNotIn_GridChoose_Change)" />',
                headerAttributes: { style: 'text-align: center;' }, attributes: { style: 'text-align: center;' },
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,customerNotIn_Grid,customerNotIn_GridChoose_Change)" />',
                filterable: false, sortable: false
            },
            { field: 'Code', title: "Mã khách hàng", width: 150, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'CustomerName', title: "Tên khách hàng", width: 150, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'Address', title: "Địa chỉ", width: 150, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { title: ' ', filterable: false, sortable: false }
        ]
    }

    $scope.customerNotIn_GridChoose_Change = function ($event, grid, hasChoose) {
        $scope.CustomerNotInHasChoose = hasChoose;
    }

    $scope.customerNotIn_Save_Click = function ($event, win, grid) {
        $event.preventDefault();
        var data = grid.dataSource.data();
        var datasend = [];
        Common.Data.Each(data, function (o) {
            if (o.IsChoose) datasend.push(o.ID);
        })
        if (datasend.length > 0) {
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.REP,
                method: _REPOwnerCostVehicle_DetailColumn.URL.SettingCusNotInSave,
                data: { item: $scope.SettingItem, lst: datasend },
                success: function (res) {
                    $scope.SettingItem = res;
                    if (Common.HasValue(res.ListCustomer)) {
                        angular.forEach(res.ListCustomer, function (value, key) {
                            value.IsChoose = false;
                        });
                        $scope.SettingReport_Customer_Grid.dataSource.data(res.ListCustomer);
                    }
                    win.close();
                    $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });

                    Common.Services.Call($http, {
                        url: Common.Services.url.REP,
                        method: _REPOwnerCostVehicle_DetailColumn.URL.SettingList,
                        data: { functionID: $rootScope.FunctionItem.ID },
                        success: function (res) {
                            $rootScope.IsLoading = false;
                            if (Common.HasValue(res)) {
                                angular.forEach(res, function (value, key) {
                                    value.IsChoose = false;
                                });
                            }
                            $scope.settingReport_GridOptions.dataSource.data(res);
                        }
                    });
                },
                error: function (res) {
                    $rootScope.IsLoading = false;
                }
            });

        }
    }

    $scope.customer_Delete = function ($event, grid) {
        $event.preventDefault();
        var data = grid.dataSource.data();
        var datasend = [];
        Common.Data.Each(data, function (o) {
            if (o.IsChoose) datasend.push(o.CustomerID);
        })
        if (datasend.length > 0) {
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.REP,
                method: _REPOwnerCostVehicle_DetailColumn.URL.SettingCusDeleteList,
                data: { item: $scope.SettingItem, lst: datasend },
                success: function (res) {
                    $scope.SettingItem = res;
                    if (Common.HasValue(res.ListCustomer)) {
                        angular.forEach(res.ListCustomer, function (value, key) {
                            value.IsChoose = false;
                        });
                        $scope.SettingReport_Customer_Grid.dataSource.data(res.ListCustomer);
                    }
                    if (Common.HasValue(res.ListGroupProduct)) {
                        angular.forEach(res.ListGroupProduct, function (value, key) {
                            value.IsChoose = false;
                        });
                        $scope.SettingReport_GroupProduct_Grid.dataSource.data(res.ListGroupProduct);
                    }
                    $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });

                    Common.Services.Call($http, {
                        url: Common.Services.url.REP,
                        method: _REPOwnerCostVehicle_DetailColumn.URL.SettingList,
                        data: { functionID: $rootScope.FunctionItem.ID },
                        success: function (res) {
                            $rootScope.IsLoading = false;
                            if (Common.HasValue(res)) {
                                angular.forEach(res, function (value, key) {
                                    value.IsChoose = false;
                                });
                            }
                            $scope.settingReport_GridOptions.dataSource.data(res);
                        }
                    });
                },
                error: function (res) {
                    $rootScope.IsLoading = false;
                }
            });

        }
    }

    //#endregion

    //#region GOP
    $scope.GOPHasChoose = false;
    $scope.SettingReport_GroupProduct_GridOptions = {
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields: {
                    CustomerID: { type: 'number', editable: false },
                    IsChoose: { type: 'boolean' },
                }
            },
            pageSize: 20
        }),
        height: '100%', pageable: true, sortable: { mode: 'multiple', allowUnsort: true }, columnMenu: false, resizable: true, reorderable: false, filterable: { mode: 'row' }, selectable: 'row',
        columns: [
            {
                title: ' ', width: '35px',
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,SettingReport_GroupProduct_Grid,gop_GridChoose_Change)" />',
                headerAttributes: { style: 'text-align: center;' }, attributes: { style: 'text-align: center;' },
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,SettingReport_GroupProduct_Grid,gop_GridChoose_Change)" />',
                filterable: false, sortable: false
            },
            {
                field: 'GroupProductCode', title: 'Mã nhóm sản phẩm', width: '150px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'GroupProductName', title: 'Tên nhóm sản phẩm', width: '150px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            { title: '', filterable: false, sortable: false }
        ]
    }
    $scope.gop_GridChoose_Change = function ($event, grid, hasChoose) {
        $scope.GOPHasChoose = hasChoose;
    }

    $scope.gop_AddNew = function ($event, win) {
        $event.preventDefault();
        if (!Common.HasValue($scope.SettingItem.ListCustomer) || $scope.SettingItem.ListCustomer.length == 0) {
            $rootScope.Message({
                Type: Common.Message.Type.Alert,
                NotifyType: Common.Message.NotifyType.ERROR,
                Title: 'Thông báo',
                Msg: 'Vui lòng chọn khách hàng',
                Close: null,
                Ok: null
            })
        } else {
            win.center().open();
            $scope.gopNotIn_GridOptions.dataSource.read();
        }
    }

    $scope.gopNotIn_GridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.REP,
            method: _REPOwnerCostVehicle_DetailColumn.URL.SettingGOPNotInList,
            readparam: function () { return { lstCus: $scope.SettingItem.ListCustomer, lstGOP: $scope.SettingItem.ListGroupProduct } },
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number', editable: false, nullable: true },
                    IsChoose: { type: 'boolean' },
                }
            }
        }),
        height: '100%', pageable: true, sortable: true, columnMenu: false, filterable: { mode: 'row' }, resizable: true, editable: false,
        columns: [
            {
                title: ' ', width: '35px',
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,gopNotIn_Grid,gopNotIn_GridChoose_Change)" />',
                headerAttributes: { style: 'text-align: center;' }, attributes: { style: 'text-align: center;' },
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,gopNotIn_Grid,gopNotIn_GridChoose_Change)" />',
                filterable: false, sortable: false
            },
            { field: 'Code', title: "Mã nhóm sản phẩm", width: 150, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'GroupName', title: "Tên nhóm sản phẩm", width: 150, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { title: ' ', filterable: false, sortable: false }
        ]
    }

    $scope.gopNotIn_GridChoose_Change = function ($event, grid, hasChoose) {
        $scope.CustomerNotInHasChoose = hasChoose;
    }

    $scope.gopNotIn_Save_Click = function ($event, win, grid) {
        $event.preventDefault();
        var data = grid.dataSource.data();
        var datasend = [];
        Common.Data.Each(data, function (o) {
            if (o.IsChoose) datasend.push(o.ID);
        })
        if (datasend.length > 0) {
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.REP,
                method: _REPOwnerCostVehicle_DetailColumn.URL.SettingGOPNotInSave,
                data: { item: $scope.SettingItem, lst: datasend },
                success: function (res) {
                    $scope.SettingItem = res;
                    if (Common.HasValue(res.ListGroupProduct)) {
                        angular.forEach(res.ListGroupProduct, function (value, key) {
                            value.IsChoose = false;
                        });
                        $scope.SettingReport_GroupProduct_GridOptions.dataSource.data(res.ListGroupProduct);
                    }
                    win.close();
                    $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });

                    Common.Services.Call($http, {
                        url: Common.Services.url.REP,
                        method: _REPOwnerCostVehicle_DetailColumn.URL.SettingList,
                        data: { functionID: $rootScope.FunctionItem.ID },
                        success: function (res) {
                            $rootScope.IsLoading = false;
                            if (Common.HasValue(res)) {
                                angular.forEach(res, function (value, key) {
                                    value.IsChoose = false;
                                });
                            }
                            $scope.settingReport_GridOptions.dataSource.data(res);
                        }
                    });
                },
                error: function (res) {
                    $rootScope.IsLoading = false;
                }
            });

        }
    }

    $scope.gop_Delete = function ($event, grid) {
        $event.preventDefault();
        var data = grid.dataSource.data();
        var datasend = [];
        Common.Data.Each(data, function (o) {
            if (o.IsChoose) datasend.push(o.GroupProductID);
        })
        if (datasend.length > 0) {
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.REP,
                method: _REPOwnerCostVehicle_DetailColumn.URL.SettingGOPDeleteList,
                data: { item: $scope.SettingItem, lst: datasend },
                success: function (res) {
                    $scope.SettingItem = res;
                    if (Common.HasValue(res.ListGroupProduct)) {
                        angular.forEach(res.ListGroupProduct, function (value, key) {
                            value.IsChoose = false;
                        });
                        $scope.SettingReport_GroupProduct_GridOptions.dataSource.data(res.ListGroupProduct);
                    }
                    $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });

                    Common.Services.Call($http, {
                        url: Common.Services.url.REP,
                        method: _REPOwnerCostVehicle_DetailColumn.URL.SettingList,
                        data: { functionID: $rootScope.FunctionItem.ID },
                        success: function (res) {
                            $rootScope.IsLoading = false;
                            if (Common.HasValue(res)) {
                                angular.forEach(res, function (value, key) {
                                    value.IsChoose = false;
                                });
                            }
                            $scope.settingReport_GridOptions.dataSource.data(res);
                        }
                    });
                },
                error: function (res) {
                    $rootScope.IsLoading = false;
                }
            });

        }
    }
    //#endregion
    $scope.window_Close_Click = function ($event, win) {
        $event.preventDefault();
        win.close();
    }
    //#endregion

    $scope.Excel_Export = function ($event) {
        $event.preventDefault();

        $rootScope.IsLoading = true;
        Common.Services.Call($http, {
            url: Common.Services.url.REP,
            method: _REPOwnerCostVehicle_DetailColumn.URL.Excel_Export,
            data: { dtfrom: $scope.Item.DateFrom, dtto: $scope.Item.DateTo },
            success: function (res) {
                $rootScope.IsLoading = false;
                $rootScope.DownloadFile(res);
            }
        })
    }

    $scope.ShowSetting = function ($event) {
        $event.preventDefault();

        $rootScope.ShowSetting({
            ListView: views.REPOwnerCostVehicle,
            event: $event,
            current: $state.current
        });
    };
}]);