/// <reference path="~/Scripts/jquery-1.9.1.intellisense.js" />
/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />

var _REPOwnerGPS_Detail = {
    URL: {
        Search: 'REPOwner_GPS_Detail',

        Detail_Export: 'REPOwnerGPS_Detail_Export',

        SettingList: 'CUSSettingsReport_List',
        SettingSave: 'CUSSettingsReport_Save',
        SettingDelete: 'CUSSettingsReport_Delete',
        SettingTemplate: 'CUSSettingsReport_Template',

        SettingAction: 'REPOwner_GPS_SettingDownload',
    },
}

angular.module('myapp').controller('REPOwnerGPS_DetailCtrl', ['$rootScope', '$scope', '$http', '$location', '$state', '$timeout', '$stateParams', function ($rootScope, $scope, $http, $location, $state, $timeout, $stateParams) {
    if ($rootScope.IsPageComplete != true) return;
    Common.Log('REPOwnerGPS_DetailCtrl');
    $rootScope.IsLoading = false;

    $scope.Auth = $rootScope.GetAuth();
    $scope.SettingItem = { ID: 0 };

    $scope.Item = {
        DateFrom: Common.Date.AddDay(new Date(), -5),
        DateTo: new Date(),
    }

    $rootScope.DateDMYHH = {
        format: 'dd/MM/yyyy',
        parseFormats: ["yyyy-MM-ddTHH:mm:ss"],
        depth: "year",
        start: "year",
    };
    $scope.REPOwnerGPS_gridOptions = {
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields: {
                    VehicleCode: { type: 'string', editable: false },
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
                 field: 'DITOMasterCode', title: '<b>Mã chuyến</b><br>[DITOMasterCode]', width: '120px',
                 filterable: { cell: { operator: 'contains', showOperators: false } }
             },
            {
                field: 'DriverName1', title: '<b>Tên tài xế</b><br>[DriverName1]', width: '120px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'ETD', title: '<b>ETD</b><br>[ETD]', width: '150px', template: '#=Common.Date.FromJsonDMYHMS(ETD)#',
                filterable: {
                    cell: {
                        template: function (e) {
                            var element = e.element.parent();
                            element.empty();
                            $("<input class='dtp-filter-from' data-field='ETD' style='width:50%; float:left;' />").appendTo(element);
                            $("<input class='dtp-filter-to' data-field='ETD' style='width:50%; float:left;' />").appendTo(element);
                        }, showOperators: false
                    }
                },
            },
            {
                field: 'ETA', title: '<b>ETA</b><br>[ETA]', width: '150px', template: '#=Common.Date.FromJsonDMYHMS(ETA)#',
                filterable: {
                    cell: {
                        template: function (e) {
                            var element = e.element.parent();
                            element.empty();
                            $("<input class='dtp-filter-from' data-field='ETA' style='width:50%; float:left;' />").appendTo(element);
                            $("<input class='dtp-filter-to' data-field='ETA' style='width:50%; float:left;' />").appendTo(element);
                        }, showOperators: false
                    }
                },
            },
               {
                   field: 'ATD', title: '<b>ATD</b><br>[ATD]', width: '150px', template: '#=Common.Date.FromJsonDMYHMS(ETA)#',
                   filterable: {
                       cell: {
                           template: function (e) {
                               var element = e.element.parent();
                               element.empty();
                               $("<input class='dtp-filter-from' data-field='ETA' style='width:50%; float:left;' />").appendTo(element);
                               $("<input class='dtp-filter-to' data-field='ETA' style='width:50%; float:left;' />").appendTo(element);
                           }, showOperators: false
                       }
                   },
               },
               {
                   field: 'ATA', title: '<b>ATA</b><br>[ATA]', width: '150px', template: '#=Common.Date.FromJsonDMYHMS(ETA)#',
                   filterable: {
                       cell: {
                           template: function (e) {
                               var element = e.element.parent();
                               element.empty();
                               $("<input class='dtp-filter-from' data-field='ETA' style='width:50%; float:left;' />").appendTo(element);
                               $("<input class='dtp-filter-to' data-field='ETA' style='width:50%; float:left;' />").appendTo(element);
                           }, showOperators: false
                       }
                   },
               },
            {
                field: 'LocationCode', title: '<b>Mã điểm</b><br>[LocationCode]', width: '120px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'LocationName', title: '<b>Tên điểm</b><br>[LocationName]', width: '120px',
                filterable: { cell: { operator: 'gte', showOperators: false } }
            },
            {
                field: 'LocationAddress', title: '<b>Địa chỉ</b><br>[LocationAddress]', width: '120px',
                filterable: { cell: { operator: 'equal', showOperators: false } }
            },
            {
                field: 'DateCome', title: '<b>Thời gian đến</b><br>[DateCome]', width: '150px', template: '#=Common.Date.FromJsonDMYHMS(DateCome)#',
                filterable: {
                    cell: {
                        template: function (e) {
                            var element = e.element.parent();
                            element.empty();
                            $("<input class='dtp-filter-from' data-field='DateCome' style='width:50%; float:left;' />").appendTo(element);
                            $("<input class='dtp-filter-to' data-field='DateCome' style='width:50%; float:left;' />").appendTo(element);
                        }, showOperators: false
                    }
                },
            },
            {
                field: 'LoadingStart', title: '<b>Thời gian bắt đầu</b><br>[LoadingStart]', width: '150px', template: '#=Common.Date.FromJsonDMYHMS(LoadingStart)#',
                filterable: {
                    cell: {
                        template: function (e) {
                            var element = e.element.parent();
                            element.empty();
                            $("<input class='dtp-filter-from' data-field='LoadingStart' style='width:50%; float:left;' />").appendTo(element);
                            $("<input class='dtp-filter-to' data-field='LoadingStart' style='width:50%; float:left;' />").appendTo(element);
                        }, showOperators: false
                    }
                },
            },
            {
                field: 'LoadingEnd', title: '<b>Thời gian kết thúc</b><br>[LoadingEnd]', width: '150px', template: '#=Common.Date.FromJsonDMYHMS(LoadingEnd)#',
                filterable: {
                    cell: {
                        template: function (e) {
                            var element = e.element.parent();
                            element.empty();
                            $("<input class='dtp-filter-from' data-field='LoadingEnd' style='width:50%; float:left;' />").appendTo(element);
                            $("<input class='dtp-filter-to' data-field='LoadingEnd' style='width:50%; float:left;' />").appendTo(element);
                        }, showOperators: false
                    }
                },
            },
            {
                field: 'DateLeave', title: '<b>Thời gian đi</b><br>[DateLeave]', width: '150px', template: '#=Common.Date.FromJsonDMYHMS(DateCome)#',
                filterable: {
                    cell: {
                        template: function (e) {
                            var element = e.element.parent();
                            element.empty();
                            $("<input class='dtp-filter-from' data-field='DateLeave' style='width:50%; float:left;' />").appendTo(element);
                            $("<input class='dtp-filter-to' data-field='DateLeave' style='width:50%; float:left;' />").appendTo(element);
                        }, showOperators: false
                    }
                },
            },

            {
                field: 'KM', title: '<b>KM</b><br>[KM]', width: '120px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'OrderCode', title: '<b>Đơn hàng</b><br>[OrderCode]', width: '120px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'CustomerCode', title: '<b>Khách hàng</b><br>[CustomerCode]', width: '120px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'TotalLocation', title: '<b>Tổng số điểm chạy</b><br>[TotalLocation]', width: '120px',
                filterable: { cell: { operator: 'equal', showOperators: false } }
            },
            {
                field: 'TotalLocationDelivery', title: '<b>Tổng số điểm giao hàng</b><br>[TotalLocationDelivery]', width: '120px',
                filterable: { cell: { operator: 'equal', showOperators: false } }
            },
            {
                field: 'Price', title: '<b>Chi phí qua trạm/Phát sinh</b><br>[Price]', width: '120px',
                filterable: { cell: { operator: 'equal', showOperators: false } }
            },
            {
                field: 'GroupOfTrouble', title: '<b>Loại chi phí phát sinh</b><br>[GroupOfTrouble]', width: '150px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'TroubleNote', title: '<b>Ghi chú phát sinh</b><br>[TroubleNote]', width: '140px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
             {
                 field: 'LocationNote', title: '<b>Ghi chú điểm</b><br>[LocationNote]', width: '140px',
                 filterable: { cell: { operator: 'contains', showOperators: false } }
             },
             {
                 field: 'LocationNote1', title: '<b>Ghi chú điểm 1</b><br>[LocationNote1]', width: '140px',
                 filterable: { cell: { operator: 'contains', showOperators: false } }
             },
            { title: ' ', sortable: false, filterable: false, menu: false }
        ],
    };

    //TimeOut
    $timeout(function () {
        $('.dtp-filter-from').kendoDatePicker({
            format: Common.Date.Format.DMYHMS,
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
                    if (Common.HasValue($scope.REPOwnerGPS_gridOptions.dataSource.filter())) {
                        filters = $scope.REPOwnerGPS_gridOptions.dataSource.filter().filters;
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
                    $scope.REPOwnerGPS_gridOptions.dataSource.filter(filters);
                    
                }
                catch (e) {
                    $rootScope.Message({ Msg: 'Sai dữ liệu!' });
                }
            }
        })

        $('.dtp-filter-to').kendoDatePicker({
            format: Common.Date.Format.DMYHMS,
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
                    if (Common.HasValue($scope.REPOwnerGPS_gridOptions.dataSource.filter())) {
                        filters = $scope.REPOwnerGPS_gridOptions.dataSource.filter().filters;
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
                    $scope.REPOwnerGPS_gridOptions.dataSource.filter(filters);
                }
                catch (e) {
                    $rootScope.Message({ Msg: 'Sai dữ liệu!' });
                }
            }
        })
    }, 500);

    $scope.Search_Click = function ($event) {
        $event.preventDefault();
        debugger

        $rootScope.IsLoading = true;
        Common.Services.Call($http, {
            url: Common.Services.url.REP,
            method: _REPOwnerGPS_Detail.URL.Search,
            data: { dtfrom: $scope.Item.DateFrom, dtto: $scope.Item.DateTo },
            success: function (res) {
                $rootScope.IsLoading = false;
                angular.forEach(res, function (item, dix) {
                    item.ETD = kendo.parseDate(item.ETD);
                    item.ETD = kendo.parseDate(item.ETA);
                    item.DateCome = kendo.parseDate(item.DateCome);
                    item.LoadingStart = kendo.parseDate(item.LoadingStart);
                    item.LoadingEnd = kendo.parseDate(item.LoadingEnd);
                    item.DateLeave = kendo.parseDate(item.DateLeave);
                });

                $scope.REPOwnerGPS_gridOptions.dataSource.data(res);
            },
            error: function (res) {
                $rootScope.IsLoading = false;
            }
        });
    };


    $scope.ShowSetting = function ($event, grid) {
        $event.preventDefault();

        $rootScope.ShowSetting({
            ListView: views.REPOwnerGPS,
            event: $event,
            grid: grid,
            current: $state.current
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
            method: _REPOwnerGPS_Detail.URL.SettingList,
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
                    method: _REPOwnerGPS_Detail.URL.SettingSave,
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
                            method: _REPOwnerGPS_Detail.URL.SettingList,
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
                    method: _REPOwnerGPS_Detail.URL.SettingDelete,
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
                            method: _REPOwnerGPS_Detail.URL.SettingList,
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
            { ID: 1, ValueName: 'Báo cáo chi tiết' },
            //{ ID: 2, ValueName: 'Báo cáo theo cột' },
        ],
        change: function (e) { }
    }
    $scope.SettingReport_ActionClick = function ($event, data) {
        $event.preventDefault();

        var request = Common.Request.CreateFromGrid($scope.REPOwnerGPS_gridOptions.dataSource);
        $rootScope.IsLoading = true;
        Common.Services.Call($http, {
            url: Common.Services.url.REP,
            method: _REPOwnerGPS_Detail.URL.SettingAction,
            data: { item: data, dtfrom: $scope.Item.DateFrom, dtto: $scope.Item.DateTo, request: request },
            success: function (res) {
                $rootScope.IsLoading = false;
                $rootScope.DownloadFile(res);
            }
        });
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
            method: _REPOwnerGPS_Detail.URL.Detail_Export,
            data: {},
            success: function (res) {
                $rootScope.IsLoading = false;
                $rootScope.DownloadFile(res);
            }
        })
    }
}]);


