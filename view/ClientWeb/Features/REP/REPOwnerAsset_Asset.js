/// <reference path="~/Scripts/jquery-1.9.1.intellisense.js" />
/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />

var _REPOwnerAsset_Asset = {
    URL: {

        Excel_Asset: 'REPOwner_Asset_Export',

        List: 'CUSSettingsReport_List',
        Save: 'CUSSettingsReport_Save',
        Delete: 'CUSSettingsReport_Delete',
        Template: 'CUSSettingsReport_Template',

        SettingActionAsset: 'REPOwner_Asset_Download',
        Search_Asset: 'REPOwner_Asset'
    },
    Param: {
    },
}

angular.module('myapp').controller('REPOwnerAsset_AssetCtrl', ['$rootScope', '$scope', '$http', '$location', '$state', '$timeout', '$stateParams', function ($rootScope, $scope, $http, $location, $state, $timeout, $stateParams) {
    if ($rootScope.IsPageComplete != true) return;
    Common.Log('REPOwnerAsset_AssetCtrl');
    $rootScope.IsLoading = false;

    $scope.Auth = $rootScope.GetAuth();
    $scope.Item = {
        ID: 0,
        TypeOfAssetID:-1,
        TypeExport: 0,
        TypeOfFilter: 1,

    };

    $scope.REPOwnerAsset_Asset_gridOptions = {
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
                field: 'Code', title: '<b>Mã thiết bị</b><br>[Code]', width: '120px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'Name', title: '<b>Tên thiết bị</b><br>[Name]', width: '120px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'RegNo', title: '<b>Số xe</b><br>[RegNo]', width: '120px',
                 filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'Lat', title: '<b>Kinh độ</b><br>[Lat]', width: '120px',
                filterable: { cell: { operator: 'eq', showOperators: false } }
            },
            {
                field: 'Lng', title: '<b>vĩ độ</b><br>[Lng]', width: '120px',
                filterable: { cell: { operator: 'eq', showOperators: false } }
            },
            {
                field: 'LocationCode', title: '<b>Mã địa chỉ</b><br>[LocationCode]', width: '120px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'LocationAddress', title: '<b>Địa chỉ</b><br>[LocationAddress]', width: '120px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'LocationProvinceName', title: '<b>Tỉnh/Thành</b><br>[LocationProvinceName]', width: '120px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'LocationDistrictName', title: '<b>Quận/Huyện</b><br>[LocationDistrictName]', width: '120px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'TypeOfAssetName', title: '<b>Loại tài sản</b><br>[TypeOfAssetName]', width: '120px',
                  filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'GroupOfEquipmentName', title: '<b>Loại Thiết bị</b><br>[GroupOfEquipmentName]', width: '120px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'BaseValue', title: '<b>Giá trị ban đầu</b><br>[BaseValue]', width: '120px',
                filterable: { cell: { operator: 'eq', showOperators: false } }
            },
            {
                field: 'CurrentValue', title: '<b>Giá trị hiện tại</b><br>[CurrentValue]', width: '120px',
                filterable: { cell: { operator: 'eq', showOperators: false } }
            },
            {
                field: 'RemainValue', title: '<b>Giá trị còn lại</b><br>[RemainValue]', width: '120px',
                filterable: { cell: { operator: 'eq', showOperators: false } }
            },
            {
                field: 'WarrantyPeriod', title: '<b>Thời gian Bảo hành</b><br>[WarrantyPeriod]', width: '120px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'WarrantyEnd', title: '<b>Ngày kết thúc bảo hành</b><br>[WarrantyEnd]', width: '120px', template: '#=Common.Date.FromJsonDDMMYY(WarrantyEnd)#',
                filterable: {
                    cell: {
                        template: function (e) {
                            var element = e.element.parent();
                            element.empty();
                            $("<input class='dtp-filter-from' data-field='WarrantyEnd' style='width:50%; float:left;' />").appendTo(element);
                            $("<input class='dtp-filter-to' data-field='WarrantyEnd' style='width:50%; float:left;' />").appendTo(element);
                        }, showOperators: false
                    }
                },
            },
            {
                field: 'DepreciationPeriod', title: '<b>Thời gian khấu hao</b><br>[DepreciationPeriod]', width: '120px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'DepreciationStart', title: '<b>Ngày bắt đầu khấu hao</b><br>[DepreciationStart]', width: '120px', template: '#=Common.Date.FromJsonDDMMYY(DepreciationStart)#',
                filterable: {
                    cell: {
                        template: function (e) {
                            var element = e.element.parent();
                            element.empty();
                            $("<input class='dtp-filter-from' data-field='DepreciationStart' style='width:50%; float:left;' />").appendTo(element);
                            $("<input class='dtp-filter-to' data-field='DepreciationStart' style='width:50%; float:left;' />").appendTo(element);
                        }, showOperators: false
                    }
                },
            },
            {
                field: 'YearOfProduction', title: '<b>Năm sản xuất</b><br>[YearOfProduction]', width: '120px',
                filterable: { cell: { operator: 'gte', showOperators: false } }
            },
            {
                field: 'Manufactor', title: '<b>Nhà sản xuất</b><br>[Manufactor]', width: '120px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'Specification', title: '<b>Mô tả</b><br>[Specification]', width: '120px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'MinWeight', title: '<b>Trọng tải đối thiểu</b><br>[MinWeight]', width: '120px',
                filterable: { cell: { operator: 'eq', showOperators: false } }
            },
            {
                field: 'MaxWeight', title: '<b>Trọng tải đối đa</b><br>[MaxWeight]', width: '120px',
                filterable: { cell: { operator: 'eq', showOperators: false } }
            },
            {
                field: 'MaxWeightCal', title: '<b>Tổng trọng tải</b><br>[MaxWeightCal]', width: '120px',
                filterable: { cell: { operator: 'eq', showOperators: false } }
            },
            {
                field: 'MinCapacity', title: '<b>Khối tối thiểu</b><br>[MinCapacity]', width: '120px',
                filterable: { cell: { operator: 'eq', showOperators: false } }
            },
            {
                field: 'MaxCapacity', title: '<b>Khối tối đa</b><br>[MaxCapacity]', width: '120px',
                filterable: { cell: { operator: 'eq', showOperators: false } }
            },
            {
                field: 'RegWeight', title: '<b>Trọng tải đăng ký</b><br>[RegWeight]', width: '120px',
                filterable: { cell: { operator: 'eq', showOperators: false } }
            },
            {
                field: 'RegCapacity', title: '<b>Số khối đăng ký</b><br>[RegCapacity]', width: '120px',
                filterable: { cell: { operator: 'eq', showOperators: false } }
            },
            {
                field: 'TempMin', title: '<b>Nhiệt độ tối thiểu</b><br>[TempMin]', width: '120px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },

            {
                field: 'TempMax', title: '<b>Nhiệt độ tối đa</b><br>[TempMax]', width: '120px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'EmptyWeight', title: '<b>Trọng tải rỗng</b><br>[EmptyWeight]', width: '120px',
                filterable: { cell: { operator: 'eq', showOperators: false } }
            },
            {
                field: 'GPSCode', title: '<b>Mã GPS</b><br>[GPSCode]', width: '120px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: "IsRent", title: '<b>Cho thuê</b><br>[IsRent]', width: 82, attributes: { style: "text-align: center;" },
                template: '<input type="checkbox" #= IsRent=="true" ? "checked=checked" : "" # disabled="disabled" />',
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
            },
            {
                field: "IsDisposal", title: '<b>Thanh lý</b><br>[IsDisposal]', width: 82, attributes: { style: "text-align: center;" },
                template: '<input type="checkbox" #= IsDisposal=="true" ? "checked=checked" : "" # disabled="disabled" />',
                filterable: {
                    cell: {
                        template: function (container) {
                            container.element.kendoComboBox({
                                dataSource: [{ Text: 'Chưa thanh lý', Value: false }, { Text: 'Thanh lý', Value: true }, { Text: 'Tất cả', Value: '' }],
                                dataTextField: "Text", dataValueField: "Value"
                            });
                        },
                        showOperators: false
                    }
                },
            },
            {
                field: 'NoOfDelivery', title: '<b>Số lượng cont 20</b><br>[NoOfDelivery]', width: '120px',
                filterable: { cell: { operator: 'eq', showOperators: false } }
            },
            {
                field: "IsFloor", title: '<b>Romooc sàn</b><br>[IsFloor]', width: 82, attributes: { style: "text-align: center;" },
                template: '<input type="checkbox" #= IsFloor=="true" ? "checked=checked" : "" # disabled="disabled" />',
                filterable: {
                    cell: {
                        template: function (container) {
                            container.element.kendoComboBox({
                                dataSource: [{ Text: 'Là mooc sàn', Value: false }, { Text: 'Không là mooc sàn', Value: true }, { Text: 'Tất cả', Value: '' }],
                                dataTextField: "Text", dataValueField: "Value"
                            });
                        },
                        showOperators: false
                    }
                },
            },
            { title: ' ', sortable: false, filterable: false, menu: false }
        ],
    };


    $scope.Search_Click = function ($event) {
        $event.preventDefault();

        $rootScope.IsLoading = true;
        Common.Services.Call($http, {
            url: Common.Services.url.REP,
            method: _REPOwnerAsset_Asset.URL.Search_Asset,
            data: { typeOfAssetID: $scope.Item.TypeExport },
            success: function (res) {
                $rootScope.IsLoading = false;
                $scope.REPOwnerAsset_Asset_gridOptions.dataSource.data(res);
            },
            error: function (res) {
                $rootScope.IsLoading = false;
            }
        });
    };

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
            if(Common.HasValue(res))
                $scope.Item.TypeExport = res[0].ID;
            $scope.cboTypeExport_options.dataSource.data(res);
        }
    })

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
                    if (Common.HasValue($scope.REPOwnerAsset_Asset_gridOptions.dataSource.filter())) {
                        filters = $scope.REPOwnerAsset_Asset_gridOptions.dataSource.filter().filters;
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
                    $scope.REPOwnerAsset_Asset_gridOptions.dataSource.filter(filters);
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
                    if (Common.HasValue($scope.REPOwnerAsset_Asset_gridOptions.dataSource.filter())) {
                        filters = $scope.REPOwnerAsset_Asset_gridOptions.dataSource.filter().filters;
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
                    $scope.REPOwnerAsset_Asset_gridOptions.dataSource.filter(filters);
                }
                catch (e) {
                    $rootScope.Message({ Msg: 'Sai dữ liệu!' });
                }
            }
        })
    }, 500);
    // export
    $scope.Excel_Export = function ($event) {
        $event.preventDefault();

        $rootScope.IsLoading = true;
        Common.Services.Call($http, {
            url: Common.Services.url.REP,
            method: _REPOwnerAsset_Asset.URL.Excel_Asset,
            data: { },
            success: function (res) {
                $rootScope.IsLoading = false;
                $rootScope.DownloadFile(res);
            }
        })
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
            method: _REPOwnerAsset_Asset.URL.List,
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
                    method: _REPOwnerAsset_Asset.URL.Save,
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
                            method: _REPOwnerAsset_Asset.URL.List,
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
                    method: _REPOwnerAsset_Asset.URL.Delete,
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
                            method: _REPOwnerAsset_Asset.URL.List,
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

        var request = Common.Request.CreateFromGrid($scope.REPOwnerAsset_Asset_gridOptions.dataSource);
        $rootScope.IsLoading = true;
        Common.Services.Call($http, {
            url: Common.Services.url.REP,
            method: _REPOwnerAsset_Asset.URL.SettingActionAsset,
            data: { item:data},
            success: function (res) {
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