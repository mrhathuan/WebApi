﻿
/// <reference path="~/Scripts/jquery-1.9.1.intellisense.js" />
/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />

var _REPOwnerPLVehicle_PriceDetailColumn = {
    URL: {
        Search: 'REPOwner_VehiclePrice_ColumnDetailData',
        Template: 'REPOwnerPLVehicle_DetailColumnTemplate',

        Excel_Export: 'REPOwner_VehiclePrice_DetailColumn_Export',

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

        SettingAction: 'REPOwner_VehicleFee_PL_SettingDownload',

        Read_Customer: 'REP_Customer_Read',
    },
    Data: {
        _listPrice: [],
        _listPriceKey: [],
        _listData: [],
    }
}

angular.module('myapp').controller('REPOwnerPLVehicle_PriceDetailColumnCtrl', ['$rootScope', '$scope', '$http', '$location', '$state', '$timeout', '$stateParams', function ($rootScope, $scope, $http, $location, $state, $timeout, $stateParams) {
    if ($rootScope.IsPageComplete != true) return;
    Common.Log('REPOwnerPLVehicle_PriceDetailColumnCtrl');
    $rootScope.IsLoading = false;

    $scope.Auth = $rootScope.GetAuth();
    $scope.SettingItem = { ID: 0 };
    $scope.arrayColumn = ['Depreciation', 'DepreciationReceipt','Receipt','Station','Trouble','ScheduleFee','Driver','Transfer'];
    $scope.arrayColumnRS = ['Khấu hao xe', 'Khấu hao phiếu', 'Phiếu', 'Trạm thu phí', 'Phát sinh', 'Chi phí hàng tháng', 'Tài xế', 'Vận chuyển' ];
    $scope.fieldGrid = [];

    //#region search
    $scope.Item = {
        lstCustomerID: [],
        DateFrom: Common.Date.AddDay(new Date(), -5),
        DateTo: new Date(),
    }

    $scope.mulCustomer_Options = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains',
        suggest: true, dataTextField: 'CustomerName', dataValueField: 'ID',
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields: {
                    CustomerName: { type: 'string' },
                    ID: { type: 'number' },
                }
            }
        }),
        change: function () {
            var lstid = this.value();
            var lst = this;
            $timeout(function () {
                $scope.SetWidth_Select(lst);
            }, 1);
        },
    };

    $scope.isWidth = false;
    $scope.SetWidth_Select = function (lst) {
        var list = lst;
        var lst1 = lst.wrapper.find('.k-floatwrap:first ul li');
        var widthDiv = lst.wrapper.find('.k-floatwrap:first').width();
        var w = 0;
        var obj = null;
        var lst2 = [];
        if (lst1.length > 1) {
            $.each(lst1, function (i, v) {
                if ($(v).attr('data-show') != 'unshow') {
                    lst2.push(v);
                }
            });
        }
        else {
            lst2 = lst1;
        }

        $.each(lst2, function (i, v) {
            w += $(v).outerWidth(true);
            $(v).attr('data-show', 'show')
            if (w >= widthDiv) {
                $(v).hide();
                $(v).attr('data-show', 'unshow');
            }
            obj = v;
        });
        if (obj == null) {
            $scope.isWidth = false;
        }
        if (w >= widthDiv && !$scope.isWidth) {
            $scope.isWidth = true;
            $(obj).show();
            $(obj).html('...');
        }
        if (w > widthDiv) {
            $scope.SetWidth_Select(list);
        }
    }

    Common.Services.Call($http, {
        url: Common.Services.url.REP,
        method: _REPDIPL_Detail.URL.Read_Customer,
        data: {},
        success: function (res) {
            $scope.mulCustomer_Options.dataSource.data(res.Data);
        }
    });

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
                        if (Common.HasValue($scope.REPOwnerPLVehicle_gridOptions.dataSource.filter())) {
                            filters = $scope.REPOwnerPLVehicle_gridOptions.dataSource.filter().filters;
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
                        $scope.REPOwnerPLVehicle_gridOptions.dataSource.filter(filters);
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
                        if (Common.HasValue($scope.REPOwnerPLVehicle_gridOptions.dataSource.filter())) {
                            filters = $scope.REPOwnerPLVehicle_gridOptions.dataSource.filter().filters;
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
                        $scope.REPOwnerPLVehicle_gridOptions.dataSource.filter(filters);
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
            method: _REPOwnerPLVehicle_PriceDetailColumn.URL.Search,
            data: { lstid: $scope.Item.lstCustomerID, lstgroupid: $scope.Item.lstGroupID, dtfrom: $scope.Item.DateFrom, dtto: $scope.Item.DateTo },
            success: function (res) {
                angular.forEach(res, function (item, dix) {
                    if (Common.HasValue(item)) {
                        if (Common.HasValue(item.RequestDate))
                            item.RequestDate = kendo.parseDate(item.RequestDate);
                        if (Common.HasValue(item.DateConfig))
                            item.DateConfig = kendo.parseDate(item.DateConfig);
                        if (Common.HasValue(item.OrderDateConfig))
                            item.OrderDateConfig = kendo.parseDate(item.OrderDateConfig);
                        if (Common.HasValue(item.OrderGroupProductDateConfig))
                            item.OrderGroupProductDateConfig = kendo.parseDate(item.OrderGroupProductDateConfig);
                        if (Common.HasValue(item.OPSDateConfig))
                            item.OPSDateConfig = kendo.parseDate(item.OPSDateConfig);
                        if (Common.HasValue(item.OPSGroupProductDateConfig))
                            item.OPSGroupProductDateConfig = kendo.parseDate(item.OPSGroupProductDateConfig);
                        if (Common.HasValue(item.ETD))
                            item.ETD = kendo.parseDate(item.ETD);
                        if (Common.HasValue(item.OrderCreatedDate))
                            item.OrderCreatedDate = kendo.parseDate(item.OrderCreatedDate);
                    }
                });
                _REPOwnerPLVehicle_PriceDetailColumn.Data._listData = res.ListData;
                _REPOwnerPLVehicle_PriceDetailColumn.Data._listPrice = res.ListPrice;
                _REPOwnerPLVehicle_PriceDetailColumn.Data._listPriceKey = res.ListPriceKey;
                //$scope.REPOwnerPLVehicle_gridOptions.dataSource.data(res);
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
    //#endregion
    //#region grid
    $scope.InitGrid = function () {
        Common.Log("InitGrid");
        var Model = {
            id: 'ID',
            fields: {
                ID: { type: 'number', editable: false },
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
        ]

        $scope.fieldGrid = [];
        Common.Data.Each(_REPOwnerPLVehicle_PriceDetailColumn.Data._listPriceKey, function (group) {
            var array = []; 
            for (var i = 0 ; i < $scope.arrayColumn.length ; i++) {
                var field = $scope.arrayColumn[i] + group.KeyCode;
                field = field.replace(/([ -.,*+?^$|(){}\[\]])/g, "_");
                Model.fields[field] = {
                    type: "number", editable: true,
                    filterable: { cell: { operator: 'equal', showOperators: false } }
                };
                array.push(field);
                GridColumn.push({
                    field: field, title: '<b>' + group.KeyCode + "-" + $scope.arrayColumnRS[i] + '</b><br>' + '[' + group.KeyCode + '-' + $scope.arrayColumn[i] + ']', width: 120, locked: false,
                });
            }
            $scope.fieldGrid.push(array);
        })

        GridColumn.push({ title: ' ', filterable: false, sortable: false })
        $scope.rep_grid.setOptions({
            dataSource: Common.DataSource.Local({
                data: [],
                model: Model,
                pageSize: 20,
            }),
            height: '100%', pageable: true, sortable: true, columnMenu: false, resizable: true, reorderable: false, filterable: { mode: 'row' }, editable: false,
            columns: GridColumn
        })

        var dataGrid = [];
        Common.Data.Each(_REPOwnerPLVehicle_PriceDetailColumn.Data._listData, function (data) {
            for (var i = 0 ; i < _REPOwnerPLVehicle_PriceDetailColumn.Data._listPrice.length ; i++) {
                var price = _REPOwnerPLVehicle_PriceDetailColumn.Data._listPrice[i];
                var dateD = new Date(data.DateConfig);
                var dateP = new Date(price.DateConfig);
                if (price.VehicleID == data.VehicleID && dateD.getTime() == dateP.getTime()) {
                    var count = 0;
                    Common.Data.Each(_REPOwnerPLVehicle_PriceDetailColumn.Data._listPriceKey, function (key) {
                        var dataGroup = null;
                        if (price.KeyCode == key.KeyCode) {
                            dataGroup = price;
                            _REPOwnerPLVehicle_PriceDetailColumn.Data._listPrice.splice(i, 1);
                        }

                        if (dataGroup != null) {
                            data[$scope.fieldGrid[count][0]] = dataGroup.Depreciation;
                            data[$scope.fieldGrid[count][1]] = dataGroup.Receipt;
                            data[$scope.fieldGrid[count][2]] = dataGroup.Station;
                            data[$scope.fieldGrid[count][3]] = dataGroup.Trouble
                            data[$scope.fieldGrid[count][4]] = dataGroup.ScheduleFee;
                            data[$scope.fieldGrid[count][5]] = dataGroup.Driver;
                            data[$scope.fieldGrid[count][6]] = dataGroup.Transfer;
                        } else {
                            for (var i = 0 ; i < $scope.fieldGrid[count].length ; i++) {
                                data[$scope.fieldGrid[count][i]] = '';
                            }
                        }
                        count++;

                    });
                }
            }
            dataGrid.push(data);
        })
        $scope.rep_grid.dataSource.data(dataGrid);
        $scope.dtpfilter();
        $rootScope.IsLoading = false;
    }

    //#region excel
    $scope.Excel_Click = function ($event) {
        $event.preventDefault();

        var request = Common.Request.CreateFromGrid($scope.REPOwnerPLVehicle_gridOptions.dataSource);

        var functionID = $rootScope.FunctionItem.ID;
        $rootScope.UploadFile({
            IsImage: false, ID: functionID, AllowChange: true, ShowChoose: true,
            Type: Common.CATTypeOfFileCode.TEMPLATEREPORT,
            Complete: function (file) {
                $rootScope.IsLoading = true;
                Common.Services.Call($http, {
                    url: Common.Services.url.REP,
                    method: _REPOwnerPLVehicle_PriceDetailColumn.URL.Template,
                    data: { itemfile: file, lstid: $scope.Item.lstCustomerID, lstgroupid: $scope.Item.lstGroupID, dtfrom: $scope.Item.DateFrom, dtto: $scope.Item.DateTo, request: request },
                    success: function (res) {
                        $rootScope.IsLoading = false;
                        $rootScope.DownloadFile(res);
                    },
                    error: function (res) {
                        $rootScope.IsLoading = false;
                    }
                });
            }
        });
    };

    $scope.ShowSetting = function ($event) {
        $event.preventDefault();

        $rootScope.ShowSetting({
            ListView: views.REPOwnerPLVehicle,
            event: $event,
            current: $state.current
        });
    };
    //#endregion

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
            method: _REPOwnerPLVehicle_PriceDetailColumn.URL.SettingList,
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
            } else {
                $scope.SettingReport_Customer_Grid.dataSource.data([]);
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
                    method: _REPOwnerPLVehicle_PriceDetailColumn.URL.SettingSave,
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
                            method: _REPOwnerPLVehicle_PriceDetailColumn.URL.SettingList,
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
                    method: _REPOwnerPLVehicle_PriceDetailColumn.URL.SettingDelete,
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
                            method: _REPOwnerPLVehicle_PriceDetailColumn.URL.SettingList,
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
    $scope.cboTypeExport_options = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: "contains",
        suggest: true, dataTextField: 'ValueName', dataValueField: 'ID',
        dataSource: [
            { ID: 1, ValueName: 'Lợi nhuận chi tiết' },
            { ID: 2, ValueName: 'Lợi nhuận theo cột' },
            { ID: 3, ValueName: 'Lợi nhuận bảng giá chi tiết' },
            { ID: 4, ValueName: 'Lợi nhuận bảng giá theo cột' },
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
            method: _REPOwnerPLVehicle_Index.URL.SettingAction,
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
            method: _REPOwnerPLVehicle_PriceDetailColumn.URL.SettingCusNotInList,
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
                method: _REPOwnerPLVehicle_PriceDetailColumn.URL.SettingCusNotInSave,
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
                        method: _REPOwnerPLVehicle_PriceDetailColumn.URL.SettingList,
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
                method: _REPOwnerPLVehicle_PriceDetailColumn.URL.SettingCusDeleteList,
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
                        method: _REPOwnerPLVehicle_PriceDetailColumn.URL.SettingList,
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
            method: _REPOwnerPLVehicle_PriceDetailColumn.URL.SettingGOPNotInList,
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
                method: _REPOwnerPLVehicle_PriceDetailColumn.URL.SettingGOPNotInSave,
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
                        method: _REPOwnerPLVehicle_PriceDetailColumn.URL.SettingList,
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
                method: _REPOwnerPLVehicle_PriceDetailColumn.URL.SettingGOPDeleteList,
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
                        method: _REPOwnerPLVehicle_PriceDetailColumn.URL.SettingList,
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
            method: _REPOwnerPLVehicle_PriceDetailColumn.URL.Excel_Export,
            data: { dtfrom: $scope.Item.DateFrom, dtto: $scope.Item.DateTo, lstid: $scope.Item.lstCustomerID },
            success: function (res) {
                $rootScope.IsLoading = false;
                $rootScope.DownloadFile(res);
            }
        })
    }
}]);