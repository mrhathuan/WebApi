/// <reference path="~/Scripts/jquery-1.9.1.intellisense.js" />
/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />

var _REPDIPL_Index = {
    URL: {
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

        SettingStockDeleteList: 'CUSSettingReport_StockDeleteList',
        SettingStockNotInList: 'CUSSettingReport_StockNotInList',
        SettingStockNotInSave: 'CUSSettingReport_StockNotInSave',

        SettingProvinceInList: 'CUSSettingReport_ProvinceNotInList',
        SettingProvinceDeleteList: 'CUSSettingReport_ProvinceDeleteList',
        SettingProvinceNotInSave: 'CUSSettingReport_ProvinceNotInSave',

        SettingLocationNotInList: 'CUSSettingReport_GroupOfLocationNotInList',
        SettingLocationDeleteList: 'CUSSettingReport_GroupOfLocationDeleteList',
        SettingLocationNotInSave: 'CUSSettingReport_GroupOfLocationNotInSave',

        SettingPartnerInList: 'CUSSettingReport_GroupOfPartnerNotInList',
        SettingPartnerDeleteList: 'CUSSettingReport_GroupOfPartnerDeleteList',
        SettingPartnerNotInSave: 'CUSSettingReport_GroupOfPartnerNotInSave',

        SettingServiceOfOrderInList: 'CUSSettingReport_ServiceOfOrderNotInList',
        SettingServiceOfOrderNotInSave: 'CUSSettingReport_ServiceOfOrderNotInSave',
        SettingServiceOfOrderDeleteList: 'CUSSettingReport_ServiceOfOrderDeleteList',

        OrderRoutingNotInList: 'CUSSettingReport_OrderRoutingNotInList',
        OrderRoutingNotInSave: 'CUSSettingReport_OrderRoutingNotInSave',
        OrderRoutingDeleteList: 'CUSSettingReport_OrderRoutingDeleteList',

        OPSRoutingNotInList: 'CUSSettingReport_OPSRoutingNotInList',
        OPSRoutingNotInSave: 'CUSSettingReport_OPSRoutingNotInSave',
        OPSRoutingDeleteList: 'CUSSettingReport_OPSRoutingDeleteList',

        PartnerNotInList: 'CUSSettingReport_PartnerNotInList',
        PartnerNotInSave: 'CUSSettingReport_PartnerNotInSave',
        PartnerDeleteList: 'CUSSettingReport_PartnerDeleteList',

        SettingAction: 'REPDIPL_SettingDownload',
        SettingDownLoad: 'REPDIPL_SettingDownload_Summary',
    },
    Param: {
    },
}

angular.module('myapp').controller('REPDIPL_IndexCtrl', ['$rootScope', '$scope', '$http', '$location', '$state', '$timeout', '$stateParams', function ($rootScope, $scope, $http, $location, $state, $timeout, $stateParams) {
    if ($rootScope.IsPageComplete != true) return;
    Common.Log('REPDIPL_IndexCtrl');
    $rootScope.IsLoading = false;

    $scope.Auth = $rootScope.GetAuth();
    $scope.SettingItem = { ID: 0 };

    $scope.ItemDownload = {
        DateFrom: Common.Date.AddDay(new Date(), -5),
        DateTo: new Date(),
        data: null,
    }

    $rootScope.IsLoading = true;
    Common.Services.Call($http, {
        url: Common.Services.url.REP,
        method: _REPDIPL_Index.URL.SettingList,
        data: { functionID: $rootScope.FunctionItem.ID },
        success: function (res) {
            $rootScope.IsLoading = false;
            if (Common.HasValue(res)) {
                angular.forEach(res, function (value, key) {
                    value.IsChoose = false;
                });
            }
            $scope.settingReport_GridOptions.dataSource.data(res);
            $scope.settingReport_Grid.resize();
        }
    });

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
                field: 'Name', title: 'Tên thiết lập',
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
            $scope.SettingItem.TypeExport = 1;
            $scope.SettingItem.TypeOfDate = 0;
            $scope.SettingItem.TypeDateRange = 1;
            $scope.SettingReport_Tab.select(0);
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
                    method: _REPDIPL_Index.URL.SettingSave,
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
                            method: _REPDIPL_Index.URL.SettingList,
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
                    method: _REPDIPL_Index.URL.SettingDelete,
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
                            method: _REPDIPL_Index.URL.SettingList,
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
            { ID: 1, ValueName: 'Lợi nhuận chuyến chi tiết' },
            { ID: 2, ValueName: 'Lợi nhuận chuyến theo cột' },
            { ID: 3, ValueName: 'Lợi nhuận chuyến theo cột nhóm kho' },
            { ID: 4, ValueName: 'Lợi nhuận chuyến theo cột nhóm hàng' },
            { ID: 5, ValueName: 'Lợi nhuận chuyến theo cột MOQ, phụ thu' },
            { ID: 6, ValueName: 'Lợi nhuận đơn hàng chi tiết' },
            { ID: 7, ValueName: 'Lợi nhuận đơn hàng theo cột' },
            { ID: 8, ValueName: 'Lợi nhuận đơn hàng theo cột nhóm kho' },
            { ID: 9, ValueName: 'Lợi nhuận đơn hàng theo cột nhóm hàng' },
            { ID: 10, ValueName: 'Lợi nhuận đơn hàng theo cột MOQ, phụ thu' },
            { ID: 11, ValueName: 'Lợi nhuận không hợp đồng' },
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
    $scope.cboTypeOfDate_options = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: "contains",
        suggest: true, dataTextField: 'ValueName', dataValueField: 'ID',
        dataSource: [
           { ID: 0, ValueName: 'Theo ngày tính giá thu' },
            { ID: 1, ValueName: 'Theo ngày tính giá chi' },
            { ID: 2, ValueName: 'Theo ngày gửi yêu cầu' },
            { ID: 3, ValueName: 'Theo ngày ATD chuyến' },
            { ID: 4, ValueName: 'Theo ngày ATA chuyến' },
            { ID: 5, ValueName: 'Theo ngày ETD chuyến' },
            { ID: 6, ValueName: 'Theo ngày ETA chuyến' },
            { ID: 7, ValueName: 'Theo ngày ETD đơn hàng' },
            { ID: 8, ValueName: 'Theo ngày ETA đơn hàng' },
        ],
        change: function (e) {
        }
    }

    $scope.SettingReport_ActionClick = function ($event, data, win) {
        $event.preventDefault();

        var request = "";//Common.Request.CreateFromGrid($scope.settingReport_GridOptions.dataSource);
        
        if (data.TypeDateRange == 3) {
            $scope.ItemDownload.data = data;
            win.center().open();
        } else {
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.REP,
                method: _REPDIPL_Index.URL.SettingAction,
                data: { item: data, dtfrom: new Date(), dtto: new Date(), request: request },
                success: function (res) {
                    $rootScope.IsLoading = false;
                    $rootScope.DownloadFile(res);
                }
            });
        }
    }

    $scope.SettingReport_ActionClickConfirm = function ($event, vform, win) {
        $event.preventDefault();
        debugger
        var request = "";//Common.Request.CreateFromGrid($scope.settingReport_GridOptions.dataSource);
        var error = false;
        if ($scope.ItemDownload.DateFrom > $scope.ItemDownload.DateTo) {
            error = true;
            $rootScope.Message({
                Type: Common.Message.Type.Alert,
                NotifyType: Common.Message.NotifyType.ERROR,
                Title: 'Thông báo',
                Msg: 'Vui lòng chọn Từ ngày nhỏ hơn Đến ngày',
                Close: null,
                Ok: null
            })
        }

        if (vform() && !error) {
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.REP,
                method: _REPDIPL_Index.URL.SettingAction,
                data: { item: $scope.ItemDownload.data, dtfrom: $scope.ItemDownload.DateFrom, dtto: $scope.ItemDownload.DateTo, request: request },
                success: function (res) {
                    $rootScope.IsLoading = false;
                    $rootScope.DownloadFile(res);
                    win.close();
                }
            });
        }
    }

    //#region Common
    $scope.SettingReport_CustomerClick = function ($event, win) {
        $event.preventDefault();

        var data = $scope.SettingItem;
        $scope.CustomerHasChoose = false;
        win.center().open();
        if (Common.HasValue(data.ListCustomer)) {
            angular.forEach(data.ListCustomer, function (value, key) {
                value.IsChoose = false;
            });
            $scope.SettingReport_Customer_Grid.dataSource.data(data.ListCustomer);
        } else {
            $scope.SettingReport_Customer_Grid.dataSource.data([]);
        }
    }

    $scope.SettingReport_GOPClick = function ($event, win) {
        $event.preventDefault();

        var data = $scope.SettingItem;
        $scope.GOPHasChoose = false;
        win.center().open();
        if (Common.HasValue(data.ListGroupProduct)) {
            angular.forEach(data.ListGroupProduct, function (value, key) {
                value.IsChoose = false;
            });
            $scope.SettingReport_GroupProduct_Grid.dataSource.data(data.ListGroupProduct);
        } else {
            $scope.SettingReport_GroupProduct_Grid.dataSource.data([]);
        }
    }

    $scope.SettingReport_StockClick = function ($event, win) {
        $event.preventDefault();

        var data = $scope.SettingItem;
        $scope.StockHasChoose = false;
        win.center().open();
        if (Common.HasValue(data.ListStock)) {
            angular.forEach(data.ListStock, function (value, key) {
                value.IsChoose = false;
            });
            $scope.SettingReport_Stock_Grid.dataSource.data(data.ListStock);
        } else {
            $scope.SettingReport_Stock_Grid.dataSource.data([]);
        }
    }

    $scope.SettingReport_GOPartnerClick = function ($event, win) {
        $event.preventDefault();

        var data = $scope.SettingItem;
        $scope.GOPartnerHasChoose = false;
        win.center().open();
        if (Common.HasValue(data.ListGroupOfPartner)) {
            angular.forEach(data.ListGroupOfPartner, function (value, key) {
                value.IsChoose = false;
            });
            $scope.SettingReport_GOPartner_Grid.dataSource.data(data.ListGroupOfPartner);
        } else {
            $scope.SettingReport_GOPartner_Grid.dataSource.data([]);
        }
    }

    $scope.SettingReport_LocationClick = function ($event, win) {
        $event.preventDefault();

        var data = $scope.SettingItem;
        $scope.LocationHasChoose = false;
        win.center().open();
        if (Common.HasValue(data.ListGroupOfLocation)) {
            angular.forEach(data.ListGroupOfLocation, function (value, key) {
                value.IsChoose = false;
            });
            $scope.SettingReport_Location_Grid.dataSource.data(data.ListGroupOfLocation);
        } else {
            $scope.SettingReport_Location_Grid.dataSource.data([]);
        }
    }

    $scope.SettingReport_ProvinceClick = function ($event, win) {
        $event.preventDefault();

        var data = $scope.SettingItem;
        $scope.ProvinceHasChoose = false;
        win.center().open();
        if (Common.HasValue(data.ListProvince)) {
            angular.forEach(data.ListProvince, function (value, key) {
                value.IsChoose = false;
            });
            $scope.SettingReport_Province_Grid.dataSource.data(data.ListProvince);
        } else {
            $scope.SettingReport_Province_Grid.dataSource.data([]);
        }
    }

    $scope.SettingReport_ServiceOfOrderClick = function ($event, win) {
        $event.preventDefault();

        var data = $scope.SettingItem;
        $scope.ServiceOfOrderHasChoose = false;
        win.center().open();
        if (Common.HasValue(data.ListServiceOfOrder)) {
            angular.forEach(data.ListServiceOfOrder, function (value, key) {
                value.IsChoose = false;
            });
            $scope.SettingReport_ServiceOfOrder_Grid.dataSource.data(data.ListServiceOfOrder);
        } else {
            $scope.SettingReport_ServiceOfOrder_Grid.dataSource.data([]);
        }
    }

    $scope.SettingReport_OrderRoutingClick = function ($event, win) {
        $event.preventDefault();

        var data = $scope.SettingItem;
        $scope.OrderRoutingHasChoose = false;
        win.center().open();
        if (Common.HasValue(data.ListOrderRouting)) {
            angular.forEach(data.ListOrderRouting, function (value, key) {
                value.IsChoose = false;
            });
            $scope.SettingReport_OrderRouting_Grid.dataSource.data(data.ListOrderRouting);
        } else {
            $scope.SettingReport_OrderRouting_Grid.dataSource.data([]);
        }
    }

    $scope.SettingReport_OPSRoutingClick = function ($event, win) {
        $event.preventDefault();

        var data = $scope.SettingItem;
        $scope.OPSRoutingHasChoose = false;
        win.center().open();
        if (Common.HasValue(data.ListOPSRouting)) {
            angular.forEach(data.ListOPSRouting, function (value, key) {
                value.IsChoose = false;
            });
            $scope.SettingReport_OPSRouting_Grid.dataSource.data(data.ListOPSRouting);
        } else {
            $scope.SettingReport_OPSRouting_Grid.dataSource.data([]);
        }
    }

    $scope.SettingReport_PartnerClick = function ($event, win) {
        $event.preventDefault();

        var data = $scope.SettingItem;
        $scope.PartnerHasChoose = false;
        win.center().open();
        if (Common.HasValue(data.ListPartner)) {
            angular.forEach(data.ListPartner, function (value, key) {
                value.IsChoose = false;
            });
            $scope.SettingReport_Partner_Grid.dataSource.data(data.ListPartner);
        } else {
            $scope.SettingReport_Partner_Grid.dataSource.data([]);
        }
    }
    //#endregion

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
            method: _REPDIPL_Index.URL.SettingCusNotInList,
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
                method: _REPDIPL_Index.URL.SettingCusNotInSave,
                data: { item: $scope.SettingItem, lst: datasend },
                success: function (res) {
                    $scope.SettingItem = res;
                    if (Common.HasValue(res.ListCustomer)) {
                        angular.forEach(res.ListCustomer, function (value, key) {
                            value.IsChoose = false;
                        });
                        $scope.SettingReport_Customer_Grid.dataSource.data(res.ListCustomer);
                    } else {
                        $scope.SettingReport_Customer_Grid.dataSource.data([]);
                    }
                    win.close();
                    $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });

                    Common.Services.Call($http, {
                        url: Common.Services.url.REP,
                        method: _REPDIPL_Index.URL.SettingList,
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
                method: _REPDIPL_Index.URL.SettingCusDeleteList,
                data: { item: $scope.SettingItem, lst: datasend },
                success: function (res) {
                    $scope.SettingItem = res;
                    if (Common.HasValue(res.ListCustomer)) {
                        angular.forEach(res.ListCustomer, function (value, key) {
                            value.IsChoose = false;
                        });
                        $scope.SettingReport_Customer_Grid.dataSource.data(res.ListCustomer);
                    } else {
                        $scope.SettingReport_Customer_Grid.dataSource.data([]);
                    }
                    if (Common.HasValue(res.ListGroupProduct)) {
                        angular.forEach(res.ListGroupProduct, function (value, key) {
                            value.IsChoose = false;
                        });
                        $scope.SettingReport_GroupProduct_Grid.dataSource.data(res.ListGroupProduct);
                    } else {
                        $scope.SettingReport_GroupProduct_Grid.dataSource.data([]);
                    }
                    $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });

                    Common.Services.Call($http, {
                        url: Common.Services.url.REP,
                        method: _REPDIPL_Index.URL.SettingList,
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
            method: _REPDIPL_Index.URL.SettingGOPNotInList,
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
                method: _REPDIPL_Index.URL.SettingGOPNotInSave,
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
                        method: _REPDIPL_Index.URL.SettingList,
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
                method: _REPDIPL_Index.URL.SettingGOPDeleteList,
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
                        method: _REPDIPL_Index.URL.SettingList,
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
    $scope.Back_Click = function ($event) {
        $event.preventDefault();

        $state.go("main.REPDIPL.Index");
    }
    //#endregion

    //#region Stock
    $scope.StockHasChoose = false;
    $scope.SettingReport_Stock_GridOptions = {
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
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,SettingReport_Stock_Grid,stock_GridChoose_Change)" />',
                headerAttributes: { style: 'text-align: center;' }, attributes: { style: 'text-align: center;' },
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,SettingReport_Stock_Grid,stock_GridChoose_Change)" />',
                filterable: false, sortable: false
            },
            {
                field: 'StockCode', title: 'Mã', width: '150px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'StockLocationName', title: 'Tên', width: '150px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'StockCountryName', title: 'Quốc gia', width: '175px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'StockProvinceName', title: 'Tỉnh thành', width: '175px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'StockDistrictName', title: 'Quận huyện',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
        ]
    }
    $scope.stock_GridChoose_Change = function ($event, grid, hasChoose) {
        $scope.StockHasChoose = hasChoose;
    }

    $scope.stock_AddNew = function ($event, win) {
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
            $scope.stockNotIn_GridOptions.dataSource.read();
        }
    }

    $scope.stockNotIn_GridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.REP,
            method: _REPDIPL_Index.URL.SettingStockNotInList,
            readparam: function () { return { lstCus: $scope.SettingItem.ListCustomer, lstStock: $scope.SettingItem.ListStock } },
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
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,stockNotIn_Grid,stockNotIn_GridChoose_Change)" />',
                headerAttributes: { style: 'text-align: center;' }, attributes: { style: 'text-align: center;' },
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,stockNotIn_Grid,stockNotIn_GridChoose_Change)" />',
                filterable: false, sortable: false
            },

            {
                field: 'Code', title: 'Mã', width: '150px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'LocationName', title: 'Tên', width: '150px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'CountryName', title: 'Quốc gia', width: '175px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'ProvinceName', title: 'Tỉnh thành', width: '175px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'DistrictName', title: 'Quận huyện',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
        ]
    }

    $scope.stockNotIn_GridChoose_Change = function ($event, grid, hasChoose) {
        $scope.StockNotInHasChoose = hasChoose;
    }

    $scope.stockNotIn_Save_Click = function ($event, win, grid) {
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
                method: _REPDIPL_Index.URL.SettingStockNotInSave,
                data: { item: $scope.SettingItem, lst: datasend },
                success: function (res) {
                    $scope.SettingItem = res;
                    if (Common.HasValue(res.ListStock)) {
                        angular.forEach(res.ListStock, function (value, key) {
                            value.IsChoose = false;
                        });
                        $scope.SettingReport_Stock_GridOptions.dataSource.data(res.ListStock);
                    }
                    win.close();
                    $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });

                    Common.Services.Call($http, {
                        url: Common.Services.url.REP,
                        method: _REPDIPL_Index.URL.SettingList,
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

    $scope.stock_Delete = function ($event, grid) {
        $event.preventDefault();
        var data = grid.dataSource.data();
        var datasend = [];
        Common.Data.Each(data, function (o) {
            if (o.IsChoose) datasend.push(o.StockID);
        })
        if (datasend.length > 0) {
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.REP,
                method: _REPDIPL_Index.URL.SettingStockDeleteList,
                data: { item: $scope.SettingItem, lst: datasend },
                success: function (res) {
                    $scope.SettingItem = res;
                    if (Common.HasValue(res.ListStock)) {
                        angular.forEach(res.ListStock, function (value, key) {
                            value.IsChoose = false;
                        });
                        $scope.SettingReport_Stock_GridOptions.dataSource.data(res.ListStock);
                    }
                    $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });

                    Common.Services.Call($http, {
                        url: Common.Services.url.REP,
                        method: _REPDIPL_Index.URL.SettingList,
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

    //#region Province
    $scope.ProvinceHasChoose = false;
    $scope.SettingReport_Province_GridOptions = {
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number', editable: false },
                    ProvinceCode: { type: 'string' },
                    ProvinceName: { type: 'string' },
                    IsChoose: { type: 'boolean' },
                }
            },
            pageSize: 20
        }),
        height: '100%', pageable: true, sortable: { mode: 'multiple', allowUnsort: true }, columnMenu: false, resizable: true, reorderable: false, filterable: { mode: 'row' }, selectable: 'row',
        columns: [
            {
                title: ' ', width: '35px',
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,SettingReport_Province_Grid,Province_GridChoose_Change)" />',
                headerAttributes: { style: 'text-align: center;' }, attributes: { style: 'text-align: center;' },
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,SettingReport_Province_Grid,Province_GridChoose_Change)" />',
                filterable: false, sortable: false
            },
            {
                field: 'ProvinceCode', title: 'Mã', width: '150px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'ProvinceName', title: 'Tỉnh/Thành', width: '150px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
        ]
    }
    $scope.Province_GridChoose_Change = function ($event, grid, hasChoose) {
        $scope.ProvinceHasChoose = hasChoose;
    }

    $scope.Province_AddNew = function ($event, win) {
        $event.preventDefault();
            win.center().open();
            $scope.ProvinceNotIn_GridOptions.dataSource.read();
    }

    $scope.ProvinceNotIn_GridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.REP,
            method: _REPDIPL_Index.URL.SettingProvinceInList,
            readparam: function () { return { lstProvince: $scope.SettingItem.ListProvince } },
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
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,ProvinceNotIn_Grid,ProvinceNotIn_GridChoose_Change)" />',
                headerAttributes: { style: 'text-align: center;' }, attributes: { style: 'text-align: center;' },
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,ProvinceNotIn_Grid,ProvinceNotIn_GridChoose_Change)" />',
                filterable: false, sortable: false
            },

            {
                field: 'Code', title: 'Mã', width: '150px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'ProvinceName', title: 'Tỉnh/Thành', width: '150px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
        ]
    }

    $scope.ProvinceNotIn_GridChoose_Change = function ($event, grid, hasChoose) {
        $scope.ProvinceNotInHasChoose = hasChoose;
    }

    $scope.ProvinNotIn_Save_Click = function ($event, win, grid) {
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
                method: _REPDIPL_Index.URL.SettingProvinceNotInSave,
                data: { item: $scope.SettingItem, lstProvinceID: datasend },
                success: function (res) {
                    debugger
                    $scope.SettingItem = res;
                    if (Common.HasValue(res.ListProvince)) {
                        angular.forEach(res.ListProvince, function (value, key) {
                            value.IsChoose = false;
                        });
                        $scope.SettingReport_Province_Grid.dataSource.data(res.ListProvince);
                    }
                    win.close();
                    $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });

                    Common.Services.Call($http, {
                        url: Common.Services.url.REP,
                        method: _REPDIPL_Index.URL.SettingList,
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

    $scope.Province_Delete = function ($event, grid) {
        $event.preventDefault();
        var data = grid.dataSource.data();
        var datasend = [];
        Common.Data.Each(data, function (o) {
            if (o.IsChoose) datasend.push(o.ProvinceID);
        })
        if (datasend.length > 0) {
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.REP,
                method: _REPDIPL_Index.URL.SettingProvinceDeleteList,
                data: { item: $scope.SettingItem, lstProvinceID: datasend },
                success: function (res) {
                    $scope.SettingItem = res;
                    if (Common.HasValue(res.ListProvince)) {
                        angular.forEach(res.ListProvince, function (value, key) {
                            value.IsChoose = false;
                        });
                        $scope.SettingReport_Province_GridOptions.dataSource.data(res.ListProvince);
                    }
                    $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });

                    Common.Services.Call($http, {
                        url: Common.Services.url.REP,
                        method: _REPDIPL_Index.URL.SettingList,
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

    //#region GroupOfLocation
    $scope.LocationHasChoose = false;
    $scope.SettingReport_Location_GridOptions = {
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
        height: '100%', pageable: true, sortable: { mode: 'multiple', allowUnsort: true }, columnMenu: false, resizable: true, reorderable: false, filterable: { mode: 'row' }, selectable: 'row',
        columns: [
            {
                title: ' ', width: '35px',
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,SettingReport_Location_Grid,Location_GridChoose_Change)" />',
                headerAttributes: { style: 'text-align: center;' }, attributes: { style: 'text-align: center;' },
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,SettingReport_Location_Grid,Location_GridChoose_Change)" />',
                filterable: false, sortable: false
            },
            {
                field: 'GroupOfLocationCode', title: 'Mã', width: '150px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'GroupOfLocationName', title: 'Tên địa chỉ', width: '150px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
        ]
    }
    $scope.Location_GridChoose_Change = function ($event, grid, hasChoose) {
        $scope.LocationHasChoose = hasChoose;
    }

    $scope.Location_AddNew = function ($event, win) {
        $event.preventDefault();
        win.center().open();
        $scope.LocationNotIn_GridOptions.dataSource.read();
    }

    $scope.LocationNotIn_GridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.REP,
            method: _REPDIPL_Index.URL.SettingLocationNotInList,
            readparam: function () { return { lstGroupOfLocation: $scope.SettingItem.ListGroupOfLocation } },
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
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,LocationNotIn_Grid,LocationNotIn_GridChoose_Change)" />',
                headerAttributes: { style: 'text-align: center;' }, attributes: { style: 'text-align: center;' },
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,LocationNotIn_Grid,LocationNotIn_GridChoose_Change)" />',
                filterable: false, sortable: false
            },

            {
                field: 'Code', title: 'Mã', width: '150px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'GroupName', title: 'Tên địa điểm', width: '150px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
        ]
    }

    $scope.LocationNotIn_GridChoose_Change = function ($event, grid, hasChoose) {
        $scope.LocationNotInHasChoose = hasChoose;
    }

    $scope.LocationNotIn_Save_Click = function ($event, win, grid) {
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
                method: _REPDIPL_Index.URL.SettingLocationNotInSave,
                data: { item: $scope.SettingItem, lstGroupOfLocationID: datasend },
                success: function (res) {
                    debugger
                    $scope.SettingItem = res;
                    if (Common.HasValue(res.ListGroupOfLocation)) {
                        angular.forEach(res.ListGroupOfLocation, function (value, key) {
                            value.IsChoose = false;
                        });
                        $scope.SettingReport_Location_GridOptions.dataSource.data(res.ListGroupOfLocation);
                    }
                    win.close();
                    $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });

                    Common.Services.Call($http, {
                        url: Common.Services.url.REP,
                        method: _REPDIPL_Index.URL.SettingList,
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

    $scope.Location_Delete = function ($event, grid) {
        $event.preventDefault();
        var data = grid.dataSource.data();
        var datasend = [];
        Common.Data.Each(data, function (o) {
            if (o.IsChoose) datasend.push(o.GroupOfLocationID);
        })
        if (datasend.length > 0) {
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.REP,
                method: _REPDIPL_Index.URL.SettingLocationDeleteList,
                data: { item: $scope.SettingItem, lstGroupOfLocationID: datasend },
                success: function (res) {
                    $scope.SettingItem = res;
                    if (Common.HasValue(res.ListGroupOfLocation)) {
                        angular.forEach(res.ListGroupOfLocation, function (value, key) {
                            value.IsChoose = false;
                        });
                        $scope.SettingReport_Location_GridOptions.dataSource.data(res.ListGroupOfLocation);
                    }
                    $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });

                    Common.Services.Call($http, {
                        url: Common.Services.url.REP,
                        method: _REPDIPL_Index.URL.SettingList,
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

    //#region GroupOfPartner
    $scope.GOPartnerHasChoose = false;
    $scope.SettingReport_GOPartner_GridOptions = {
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
        height: '100%', pageable: true, sortable: { mode: 'multiple', allowUnsort: true }, columnMenu: false, resizable: true, reorderable: false, filterable: { mode: 'row' }, selectable: 'row',
        columns: [
            {
                title: ' ', width: '35px',
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,SettingReport_GOPartner_Grid,GOPartner_GridChoose_Change)" />',
                headerAttributes: { style: 'text-align: center;' }, attributes: { style: 'text-align: center;' },
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,SettingReport_GOPartner_Grid,GOPartner_GridChoose_Change)" />',
                filterable: false, sortable: false
            },
            {
                field: 'GroupOfPartnerCode', title: 'Mã', width: '150px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'GroupOfPartnerName', title: 'Tên địa chỉ', width: '150px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
        ]
    }
    $scope.GOPartner_GridChoose_Change = function ($event, grid, hasChoose) {
        $scope.GOPartnerHasChoose = hasChoose;
    }

    $scope.GOPartner_AddNew = function ($event, win) {
        $event.preventDefault();
        win.center().open();
        $scope.GOPartnerNotIn_GridOptions.dataSource.read();
    }

    $scope.GOPartnerNotIn_GridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.REP,
            method: _REPDIPL_Index.URL.SettingPartnerInList,
            readparam: function () { return { lstGroupOfPartner: $scope.SettingItem.ListGroupOfPartner } },
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
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,GOPartnerNotIn_Grid,GOPartnerNotIn_GridChoose_Change)" />',
                headerAttributes: { style: 'text-align: center;' }, attributes: { style: 'text-align: center;' },
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,GOPartnerNotIn_Grid,GOPartnerNotIn_GridChoose_Change)" />',
                filterable: false, sortable: false
            },

            {
                field: 'Code', title: 'Mã', width: '150px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'GroupName', title: 'Tên địa điểm', width: '150px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
        ]
    }

    $scope.GOPartnerNotIn_GridChoose_Change = function ($event, grid, hasChoose) {
        $scope.GOPartnerNotInHasChoose = hasChoose;
    }

    $scope.GOPartnerNotIn_Save_Click = function ($event, win, grid) {
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
                method: _REPDIPL_Index.URL.SettingPartnerNotInSave,
                data: { item: $scope.SettingItem, lstGroupOfPartnerID: datasend },
                success: function (res) {
                    debugger
                    $scope.SettingItem = res;
                    if (Common.HasValue(res.ListGroupOfPartner)) {
                        angular.forEach(res.ListGroupOfPartner, function (value, key) {
                            value.IsChoose = false;
                        });
                        $scope.SettingReport_GOPartner_GridOptions.dataSource.data(res.ListGroupOfPartner);
                    }
                    win.close();
                    $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });

                    Common.Services.Call($http, {
                        url: Common.Services.url.REP,
                        method: _REPDIPL_Index.URL.SettingList,
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

    $scope.GOPartner_Delete = function ($event, grid) {
        $event.preventDefault();
        var data = grid.dataSource.data();
        var datasend = [];
        Common.Data.Each(data, function (o) {
            if (o.IsChoose) datasend.push(o.GroupOfPartnerID);
        })
        if (datasend.length > 0) {
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.REP,
                method: _REPDIPL_Index.URL.SettingPartnerDeleteList,
                data: { item: $scope.SettingItem, lstGroupOfPartnerID: datasend },
                success: function (res) {
                    $scope.SettingItem = res;
                    if (Common.HasValue(res.ListGroupOfPartner)) {
                        angular.forEach(res.ListGroupOfPartner, function (value, key) {
                            value.IsChoose = false;
                        });
                        $scope.SettingReport_GOPartner_GridOptions.dataSource.data(res.ListGroupOfPartner);
                    }
                    $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });

                    Common.Services.Call($http, {
                        url: Common.Services.url.REP,
                        method: _REPDIPL_Index.URL.SettingList,
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

    //#region ServiceOfOrder
    $scope.ServiceOfOrderHasChoose = false;
    $scope.SettingReport_ServiceOfOrder_GridOptions = {
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
        height: '100%', pageable: true, sortable: { mode: 'multiple', allowUnsort: true }, columnMenu: false, resizable: true, reorderable: false, filterable: { mode: 'row' }, selectable: 'row',
        columns: [
            {
                title: ' ', width: '35px',
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,SettingReport_ServiceOfOrder_Grid,ServiceOfOrder_GridChoose_Change)" />',
                headerAttributes: { style: 'text-align: center;' }, attributes: { style: 'text-align: center;' },
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,SettingReport_ServiceOfOrder_Grid,ServiceOfOrder_GridChoose_Change)" />',
                filterable: false, sortable: false
            },
            {
                field: 'ServiceOfOrderCode', title: 'Mã', width: '150px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'ServiceOfOrderName', title: 'Tên dịch vụ', width: '150px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
        ]
    }
    $scope.ServiceOfOrder_GridChoose_Change = function ($event, grid, hasChoose) {
        $scope.ServiceOfOrderHasChoose = hasChoose;
    }

    $scope.ServiceOfOrder_AddNew = function ($event, win) {
        $event.preventDefault();
        win.center().open();
        $scope.ServiceOfOrderNotIn_GridOptions.dataSource.read();
    }

    $scope.ServiceOfOrderNotIn_GridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.REP,
            method: _REPDIPL_Index.URL.SettingServiceOfOrderInList,
            readparam: function () { return { lstServiceOfOrder: $scope.SettingItem.ListServiceOfOrder } },
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
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,ServiceOfOrderNotIn_Grid,ServiceOfOrderNotIn_GridChoose_Change)" />',
                headerAttributes: { style: 'text-align: center;' }, attributes: { style: 'text-align: center;' },
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,ServiceOfOrderNotIn_Grid,ServiceOfOrderNotIn_GridChoose_Change)" />',
                filterable: false, sortable: false
            },

            {
                field: 'Code', title: 'Mã', width: '150px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'Name', title: 'Tên dịch vụ', width: '150px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
        ]
    }

    $scope.ServiceOfOrderNotIn_GridChoose_Change = function ($event, grid, hasChoose) {
        $scope.ServiceOfOrderNotInHasChoose = hasChoose;
    }

    $scope.ServiceOfOrderNotIn_Save_Click = function ($event, win, grid) {
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
                method: _REPDIPL_Index.URL.SettingServiceOfOrderNotInSave,
                data: { item: $scope.SettingItem, lstServiceOfOrderID: datasend },
                success: function (res) {
                    debugger
                    $scope.SettingItem = res;
                    if (Common.HasValue(res.ListServiceOfOrder)) {
                        angular.forEach(res.ListServiceOfOrder, function (value, key) {
                            value.IsChoose = false;
                        });
                        $scope.SettingReport_ServiceOfOrder_GridOptions.dataSource.data(res.ListServiceOfOrder);
                    }
                    win.close();
                    $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });

                    Common.Services.Call($http, {
                        url: Common.Services.url.REP,
                        method: _REPDIPL_Index.URL.SettingList,
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

    $scope.ServiceOfOrder_Delete = function ($event, grid) {
        $event.preventDefault();
        var data = grid.dataSource.data();
        var datasend = [];
        Common.Data.Each(data, function (o) {
            if (o.IsChoose) datasend.push(o.ServiceOfOrderID);
        })
        if (datasend.length > 0) {
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.REP,
                method: _REPDIPL_Index.URL.SettingServiceOfOrderDeleteList,
                data: { item: $scope.SettingItem, lstServiceOfOrderID: datasend },
                success: function (res) {
                    $scope.SettingItem = res;
                    if (Common.HasValue(res.ListServiceOfOrder)) {
                        angular.forEach(res.ListServiceOfOrder, function (value, key) {
                            value.IsChoose = false;
                        });
                        $scope.SettingReport_ServiceOfOrder_GridOptions.dataSource.data(res.ListServiceOfOrder);
                    }
                    $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });

                    Common.Services.Call($http, {
                        url: Common.Services.url.REP,
                        method: _REPDIPL_Index.URL.SettingList,
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

    //#region OrderRouting
    $scope.OrderRoutingHasChoose = false;
    $scope.SettingReport_OrderRouting_GridOptions = {
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'RoutingID',
                fields: {
                    RoutingID: { type: 'number', editable: false },
                    IsChoose: { type: 'boolean' },
                }
            },
            pageSize: 20
        }),
        height: '100%', pageable: true, sortable: { mode: 'multiple', allowUnsort: true }, columnMenu: false, resizable: true, reorderable: false, filterable: { mode: 'row' }, selectable: 'row',
        columns: [
            {
                title: ' ', width: '35px',
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,SettingReport_OrderRouting_Grid,OrderRouting_GridChoose_Change)" />',
                headerAttributes: { style: 'text-align: center;' }, attributes: { style: 'text-align: center;' },
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,SettingReport_OrderRouting_Grid,OrderRouting_GridChoose_Change)" />',
                filterable: false, sortable: false
            },
            {
                field: 'RoutingCode', title: 'Mã cung đường', width: '150px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'RoutingName', title: 'Cung đường', width: '150px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
        ]
    }
    $scope.OrderRouting_GridChoose_Change = function ($event, grid, hasChoose) {
        $scope.OrderRoutingHasChoose = hasChoose;
    }

    $scope.OrderRouting_AddNew = function ($event, win) {
        $event.preventDefault();
        win.center().open();
        $scope.OrderRoutingNotIn_GridOptions.dataSource.read();
    }

    $scope.OrderRoutingNotIn_GridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.REP,
            method: _REPDIPL_Index.URL.OrderRoutingNotInList,
            readparam: function () { return { lstCus: $scope.SettingItem.ListCustomer, lstOrderRouting: $scope.SettingItem.ListOrderRouting } },
            model: {
                id: 'RoutingID',
                fields: {
                    RoutingID: { type: 'number', editable: false, nullable: true },
                    IsChoose: { type: 'boolean' },
                }
            }
        }),
        height: '100%', pageable: true, sortable: true, columnMenu: false, filterable: { mode: 'row' }, resizable: true, editable: false,
        columns: [
            {
                title: ' ', width: '35px',
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,OrderRoutingNotIn_Grid,OrderRoutingNotIn_GridChoose_Change)" />',
                headerAttributes: { style: 'text-align: center;' }, attributes: { style: 'text-align: center;' },
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,OrderRoutingNotIn_Grid,OrderRoutingNotIn_GridChoose_Change)" />',
                filterable: false, sortable: false
            },

            {
                field: 'RoutingCode', title: 'Mã cung đường', width: '150px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'RoutingName', title: 'Cung đường', width: '150px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
        ]
    }

    $scope.OrderRoutingNotIn_GridChoose_Change = function ($event, grid, hasChoose) {
        $scope.OrderRoutingNotInHasChoose = hasChoose;
    }

    $scope.OrderRoutingNotIn_Save_Click = function ($event, win, grid) {
        $event.preventDefault();

        var data = grid.dataSource.data();
        var datasend = [];
        Common.Data.Each(data, function (o) {
            if (o.IsChoose) datasend.push(o.RoutingID);
        })
        if (datasend.length > 0) {
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.REP,
                method: _REPDIPL_Index.URL.OrderRoutingNotInSave,
                data: { item: $scope.SettingItem, lstOrderRoutingID: datasend },
                success: function (res) {
                    debugger
                    $scope.SettingItem = res;
                    if (Common.HasValue(res.ListOrderRouting)) {
                        angular.forEach(res.ListOrderRouting, function (value, key) {
                            value.IsChoose = false;
                        });
                        $scope.SettingReport_OrderRouting_GridOptions.dataSource.data(res.ListOrderRouting);
                    }
                    win.close();
                    $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });

                    Common.Services.Call($http, {
                        url: Common.Services.url.REP,
                        method: _REPDIPL_Index.URL.SettingList,
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

    $scope.OrderRouting_Delete = function ($event, grid) {
        $event.preventDefault();
        var data = grid.dataSource.data();
        var datasend = [];
        Common.Data.Each(data, function (o) {
            if (o.IsChoose) datasend.push(o.RoutingID);
        })
        if (datasend.length > 0) {
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.REP,
                method: _REPDIPL_Index.URL.OrderRoutingDeleteList,
                data: { item: $scope.SettingItem, lstOrderRoutingID: datasend },
                success: function (res) {
                    $scope.SettingItem = res;
                    if (Common.HasValue(res.ListOrderRouting)) {
                        angular.forEach(res.ListOrderRouting, function (value, key) {
                            value.IsChoose = false;
                        });
                        $scope.SettingReport_OrderRouting_GridOptions.dataSource.data(res.ListOrderRouting);
                    }
                    $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });

                    Common.Services.Call($http, {
                        url: Common.Services.url.REP,
                        method: _REPDIPL_Index.URL.SettingList,
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

    //#region OPSRouting
    $scope.OPSRoutingHasChoose = false;
    $scope.SettingReport_OPSRouting_GridOptions = {
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'RoutingID',
                fields: {
                    RoutingID: { type: 'number', editable: false },
                    IsChoose: { type: 'boolean' },
                }
            },
            pageSize: 20
        }),
        height: '100%', pageable: true, sortable: { mode: 'multiple', allowUnsort: true }, columnMenu: false, resizable: true, reorderable: false, filterable: { mode: 'row' }, selectable: 'row',
        columns: [
            {
                title: ' ', width: '35px',
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,SettingReport_OPSRouting_Grid,OPSRouting_GridChoose_Change)" />',
                headerAttributes: { style: 'text-align: center;' }, attributes: { style: 'text-align: center;' },
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,SettingReport_OPSRouting_Grid,OPSRouting_GridChoose_Change)" />',
                filterable: false, sortable: false
            },
            {
                field: 'RoutingCode', title: 'Mã cung đường', width: '150px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'RoutingName', title: 'Cung đường', width: '150px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
        ]
    }
    $scope.OPSRouting_GridChoose_Change = function ($event, grid, hasChoose) {
        $scope.OPSRoutingHasChoose = hasChoose;
    }

    $scope.OPSRouting_AddNew = function ($event, win) {
        $event.preventDefault();
        win.center().open();
        $scope.OPSRoutingNotIn_GridOptions.dataSource.read();
    }

    $scope.OPSRoutingNotIn_GridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.REP,
            method: _REPDIPL_Index.URL.OPSRoutingNotInList,
            readparam: function () { return { lstCus: $scope.SettingItem.ListCustomer, lstOPSRouting: $scope.SettingItem.ListOPSRouting } },
            model: {
                id: 'RoutingID',
                fields: {
                    RoutingID: { type: 'number', editable: false, nullable: true },
                    IsChoose: { type: 'boolean' },
                }
            }
        }),
        height: '100%', pageable: true, sortable: true, columnMenu: false, filterable: { mode: 'row' }, resizable: true, editable: false,
        columns: [
            {
                title: ' ', width: '35px',
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,OPSRoutingNotIn_Grid,OPSRoutingNotIn_GridChoose_Change)" />',
                headerAttributes: { style: 'text-align: center;' }, attributes: { style: 'text-align: center;' },
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,OPSRoutingNotIn_Grid,OPSRoutingNotIn_GridChoose_Change)" />',
                filterable: false, sortable: false
            },

            {
                field: 'RoutingCode', title: 'Mã cung đường', width: '150px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'RoutingName', title: 'Cung đường', width: '150px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
        ]
    }

    $scope.OPSRoutingNotIn_GridChoose_Change = function ($event, grid, hasChoose) {
        $scope.OPSRoutingNotInHasChoose = hasChoose;
    }

    $scope.OPSRoutingNotIn_Save_Click = function ($event, win, grid) {
        $event.preventDefault();

        var data = grid.dataSource.data();
        var datasend = [];
        Common.Data.Each(data, function (o) {
            if (o.IsChoose) datasend.push(o.RoutingID);
        })
        if (datasend.length > 0) {
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.REP,
                method: _REPDIPL_Index.URL.OPSRoutingNotInSave,
                data: { item: $scope.SettingItem, lstOPSRoutingID: datasend },
                success: function (res) {
                    debugger
                    $scope.SettingItem = res;
                    if (Common.HasValue(res.ListOPSRouting)) {
                        angular.forEach(res.ListOPSRouting, function (value, key) {
                            value.IsChoose = false;
                        });
                        $scope.SettingReport_OPSRouting_GridOptions.dataSource.data(res.ListOPSRouting);
                    }
                    win.close();
                    $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });

                    Common.Services.Call($http, {
                        url: Common.Services.url.REP,
                        method: _REPDIPL_Index.URL.SettingList,
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

    $scope.OPSRouting_Delete = function ($event, grid) {
        $event.preventDefault();
        var data = grid.dataSource.data();
        var datasend = [];
        Common.Data.Each(data, function (o) {
            if (o.IsChoose) datasend.push(o.RoutingID);
        })
        if (datasend.length > 0) {
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.REP,
                method: _REPDIPL_Index.URL.OPSRoutingDeleteList,
                data: { item: $scope.SettingItem, lstOPSRoutingID: datasend },
                success: function (res) {
                    $scope.SettingItem = res;
                    if (Common.HasValue(res.ListOPSRouting)) {
                        angular.forEach(res.ListOPSRouting, function (value, key) {
                            value.IsChoose = false;
                        });
                        $scope.SettingReport_OPSRouting_GridOptions.dataSource.data(res.ListOPSRouting);
                    }
                    $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });

                    Common.Services.Call($http, {
                        url: Common.Services.url.REP,
                        method: _REPDIPL_Index.URL.SettingList,
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

    //#region Partner
    $scope.PartnerHasChoose = false;
    $scope.SettingReport_Partner_GridOptions = {
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'PartnerID',
                fields: {
                    PartnerID: { type: 'number', editable: false },
                    IsChoose: { type: 'boolean' },
                }
            },
            pageSize: 20
        }),
        height: '100%', pageable: true, sortable: { mode: 'multiple', allowUnsort: true }, columnMenu: false, resizable: true, reorderable: false, filterable: { mode: 'row' }, selectable: 'row',
        columns: [
            {
                title: ' ', width: '35px',
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,SettingReport_Partner_Grid,Partner_GridChoose_Change)" />',
                headerAttributes: { style: 'text-align: center;' }, attributes: { style: 'text-align: center;' },
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,SettingReport_Partner_Grid,Partner_GridChoose_Change)" />',
                filterable: false, sortable: false
            },
            {
                field: 'PartnerCode', title: 'Mã NPP', width: '150px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'PartnerName', title: 'Nhà phân phối', width: '150px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'PartnerAddress', title: 'Địa chỉ', width: '150px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
        ]
    }
    $scope.Partner_GridChoose_Change = function ($event, grid, hasChoose) {
        $scope.PartnerHasChoose = hasChoose;
    }

    $scope.Partner_AddNew = function ($event, win) {
        $event.preventDefault();
        win.center().open();
        $scope.PartnerNotIn_GridOptions.dataSource.read();
    }

    $scope.PartnerNotIn_GridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.REP,
            method: _REPDIPL_Index.URL.PartnerNotInList,
            readparam: function () { return { lstPartner: $scope.SettingItem.ListPartner, lstCus: $scope.SettingItem.ListCustomer } },
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
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,PartnerNotIn_Grid,PartnerNotIn_GridChoose_Change)" />',
                headerAttributes: { style: 'text-align: center;' }, attributes: { style: 'text-align: center;' },
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,PartnerNotIn_Grid,PartnerNotIn_GridChoose_Change)" />',
                filterable: false, sortable: false
            },
            {
                field: 'PartnerCode', title: 'Mã NPP', width: '150px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'PartnerName', title: 'Nhà phân phối', width: '150px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'PartnerAddress', title: 'Địa chỉ', width: '150px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
        ]
    }

    $scope.PartnerNotIn_GridChoose_Change = function ($event, grid, hasChoose) {
        $scope.PartnerNotInHasChoose = hasChoose;
    }

    $scope.PartnerNotIn_Save_Click = function ($event, win, grid) {
        $event.preventDefault();

        var data = grid.dataSource.data();
        var datasend = [];
        Common.Data.Each(data, function (o) {
            if (o.IsChoose) datasend.push(o.PartnerID);
        })
        if (datasend.length > 0) {
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.REP,
                method: _REPDIPL_Index.URL.PartnerNotInSave,
                data: { item: $scope.SettingItem, lstPartnerID: datasend },
                success: function (res) {
                    debugger
                    $scope.SettingItem = res;
                    if (Common.HasValue(res.ListPartner)) {
                        angular.forEach(res.ListPartner, function (value, key) {
                            value.IsChoose = false;
                        });
                        $scope.SettingReport_Partner_GridOptions.dataSource.data(res.ListPartner);
                    }
                    win.close();
                    $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });

                    Common.Services.Call($http, {
                        url: Common.Services.url.REP,
                        method: _REPDIPL_Index.URL.SettingList,
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

    $scope.Partner_Delete = function ($event, grid) {
        $event.preventDefault();
        var data = grid.dataSource.data();
        var datasend = [];
        Common.Data.Each(data, function (o) {
            if (o.IsChoose) datasend.push(o.PartnerID);
        })
        if (datasend.length > 0) {
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.REP,
                method: _REPDIPL_Index.URL.PartnerDeleteList,
                data: { item: $scope.SettingItem, lstPartnerID: datasend },
                success: function (res) {
                    $scope.SettingItem = res;
                    if (Common.HasValue(res.ListPartner)) {
                        angular.forEach(res.ListPartner, function (value, key) {
                            value.IsChoose = false;
                        });
                        $scope.SettingReport_Partner_GridOptions.dataSource.data(res.ListPartner);
                    }
                    $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });

                    Common.Services.Call($http, {
                        url: Common.Services.url.REP,
                        method: _REPDIPL_Index.URL.SettingList,
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

    //#region Action

    $scope.ShowSetting = function ($event, grid) {
        $event.preventDefault();

        $rootScope.ShowSetting({
            ListView: views.REPDIPL,
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

    //#region Summary
    $scope.settingReport_GridDownLoad_Click = function ($event, grid, win) {
        $event.preventDefault();
        var data = grid.dataSource.data();
        var datasend = [];
        Common.Data.Each(data, function (o) {
            if (o.IsChoose) datasend.push(o.ID);
        })
        if (datasend.length > 0) {
            win.center().open();
        }
    }

    $scope.SettingReport_Summary_ActionClickConfirm = function ($event, vform, win) {
        $event.preventDefault();
        
        var request = "";//Common.Request.CreateFromGrid($scope.settingReport_GridOptions.dataSource);
        var error = false;
        if ($scope.ItemDownload.DateFrom > $scope.ItemDownload.DateTo) {
            error = true;
            $rootScope.Message({
                Type: Common.Message.Type.Alert,
                NotifyType: Common.Message.NotifyType.ERROR,
                Title: 'Thông báo',
                Msg: 'Vui lòng chọn Từ ngày nhỏ hơn Đến ngày',
                Close: null,
                Ok: null
            })
        }

        if (vform() && !error) {
            $rootScope.IsLoading = true;
            var data = $scope.settingReport_Grid.dataSource.data();
            var lst = [];
            Common.Data.Each(data, function (o) {
                if (o.IsChoose) lst.push(o);
            });
            Common.Services.Call($http, {
                url: Common.Services.url.REP,
                method: _REPDIPL_Index.URL.SettingDownLoad,
                data: { lst: lst, dtfrom: $scope.ItemDownload.DateFrom, dtto: $scope.ItemDownload.DateTo, request: request },
                success: function (res) {
                    $rootScope.IsLoading = false;
                    $rootScope.DownloadFile(res);
                    win.close();
                }
            });
        }
    }
    //#endregion

}]);