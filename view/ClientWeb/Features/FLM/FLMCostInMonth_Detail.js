
/// <reference path="~/Scripts/jquery-1.9.1.intellisense.js" />
/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />

var _FLMCostInMonth_Detail = {
    URL: {
        Detail_Data:'FLMSchedule_Detail_Data',
        Detail_Save: 'FLMSchedule_Detail_Save',

        Driver_List: 'FLMSchedule_Driver_List',
        Driver_Get: 'FLMSchedule_Driver_Get',
        Driver_Save: 'FLMSchedule_Driver_Save',
        Driver_Delete: 'FLMSchedule_Driver_Delete',
        Driver_NotInList: 'FLMSchedule_Driver_NotInList',
        Driver_NotInSave: 'FLMSchedule_Driver_NotInSave',
        
        Date_List: 'FLMSchedule_Date_List',
        Date_Get: 'FLMSchedule_Date_Get',
        Date_Save: 'FLMSchedule_Date_Save',

        TypeScheduleDate: 'ALL_TypeScheduleDate',

        DriverFee_List: 'FLMSchedule_DriverFee_List',
        DriverFee_Get: 'FLMSchedule_DriverFee_Get',
        DriverFee_Save: 'FLMSchedule_DriverFee_Save',
        DriverFee_Delete: 'FLMSchedule_DriverFee_Delete',
        DriverFee_DriverList: 'FLMSchedule_DriverFee_DriverList',

        AssetFee_List: 'FLMSchedule_AssetFee_List',
        AssetFee_Get: 'FLMSchedule_AssetFee_Get',
        AssetFee_Save: 'FLMSchedule_AssetFee_Save',
        AssetFee_Delete: 'FLMSchedule_AssetFee_Delete',
        AssetFee_AsestList: 'FLMSchedule_AssetFee_AsestList',

        AssistantFee_List: 'FLMSchedule_AssistantFee_List',
        AssistantFee_Get: 'FLMSchedule_AssistantFee_Get',
        AssistantFee_Save: 'FLMSchedule_AssistantFee_Save',
        AssistantFee_Delete: 'FLMSchedule_AssistantFee_Delete',

        TypeOfScheduleFee: 'ALL_FLMTypeOfScheduleFee',

        ScheduleExcel_Export: 'FLMSchedule_Excel_Export',
        ScheduleExcel_Save: 'FLMSchedule_Excel_Save',
        ScheduleExcel_Check: 'FLMSchedule_Excel_Check',

        DriverFeeExcel_Export: 'FLMDriverFee_Excel_Export',
        DriverFeeExcel_Save: 'FLMDriverFee_Excel_Save',
        DriverFeeExcel_Check: 'FLMDriverFee_DriverFee_Excel_Check',

        AssetFeeExcel_Export: 'FLMAssetFee_Excel_Export',
        AssetFeeExcel_Save: 'FLMAssetFee_Excel_Save',
        AssetFeeExcel_Check: 'FLMAssetFee_Excel_Check',

        CaculateFee: 'FLM_Schedule_Detail_CalculateFee',
        UpdateInfo: 'FLMSchedule_Driver_UpdateInfo',
        RefreshFee: 'FLM_Schedule_Detail_RefreshFee',
    },
    Data: {
        _dataDetail: [],
        _dataDate: [],
        _dataDriver: [],

    },
    Params: {
        ScheduleID : -1,
    }
}

angular.module('myapp').controller('FLMCostInMonth_DetailCtrl', ['$rootScope', '$scope', '$http', '$location', '$state', '$timeout', function ($rootScope, $scope, $http, $location, $state, $timeout) {
    Common.Log('FLMCostInMonth_DetailCtrl');
    $rootScope.IsLoading = false;

    //#region Driver
    $scope.Item = null;
    $scope.DriverNotInHasChoose = false;
    $scope.TabIndex = 0;

    _FLMCostInMonth_Detail.Param = $.extend(true, _FLMCostInMonth_Detail.Params, $state.params);

    $scope.FLMSchedule_Detail_TabOptions = {
        animation: {
            open: { effects: "fadeIn" }
        },
        select: function (e) {
            $timeout(function () {
                $scope.TabIndex = angular.element(e.item).data('tabindex');
                Common.Log("Select_Tab_" + $scope.TabIndex);
            }, 1)
        }
    }

    //#region driverFee
    $scope.driverFeeGrid_Options = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.FLM,
            method: _FLMCostInMonth_Detail.URL.DriverFee_List,
            readparam: function () { return { scheduleID: _FLMCostInMonth_Detail.Params.ScheduleID }; },
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number', editable: false, },
                    Price:{type:'number'}
                }
            }
        }),
        selectable: false, reorderable: true, editable: false,
        height: '100%', pageable: true, sortable: false, columnMenu: false, filterable: { mode: 'row' }, resizable: true,
        columns: [
            {
                title: ' ', width: '90px',
                template: '<a href="/" ng-click="driverFee_GridEdit_Click($event,driverFee_win,dataItem,driverFee_vform)" class="k-button"><i class="fa fa-pencil"></i></a>',
                filterable: false, sortable: false
            },
            {
                field: 'DriverName', title: 'Tài xế', width: 150,
                filterable: { cell: { operator: 'contains', showOperators: false }, editable: false, }
            },
            {
                field: 'TypeOfScheduleFeeCode', title: 'Mã loại tính phí', width: 120,
                filterable: { cell: { operator: 'contains', showOperators: false }, editable: false, }
            },
            {
                field: 'TypeOfScheduleFeeName', title: 'Loại tính phí', width: 120,
                filterable: { cell: { operator: 'contains', showOperators: false }, editable: false, }
            },
            {
                field: 'Price', title: 'Giá', width: 120, template: "#=Price==null?'':Common.Number.ToMoney(Price)#",
                filterable: { cell: { operator: 'equal', showOperators: false }, editable: false, }
            },
            { title: ' ', filterable: false, sortable: false }
        ]
    }

    $scope.driverFee_AddNew = function ($event, win, vform) {
        $event.preventDefault();
        $scope.LoadItemDriverFee(0, win, vform);
    }

    $scope.driverFee_GridEdit_Click = function ($event, win, data, vform) {
        $event.preventDefault();
        $scope.LoadItemDriverFee(data.id, win, vform);
    }

    $scope.LoadItemDriverFee = function (id, win, vform) {
        vform({ clear: true });
        $scope.LoadDriver();
        $rootScope.IsLoading = true;
        Common.Services.Call($http, {
            url: Common.Services.url.FLM,
            method: _FLMCostInMonth_Detail.URL.DriverFee_Get,
            data: { 'id': id, scheduleID: _FLMCostInMonth_Detail.Param.ScheduleID },
            success: function (res) {
                $rootScope.IsLoading = false;
                $scope.ItemDriverFee = res;

                win.center();
                win.open();
            },
            error: function (res) {
                $rootScope.IsLoading = false;
            }
        })
    }

    $scope.driverFee_Save_Click = function ($event, win, vform) {
        $event.preventDefault();
        if (vform()) {
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.FLM,
                method: _FLMCostInMonth_Detail.URL.DriverFee_Save,
                data: { item: $scope.ItemDriverFee, scheduleID: _FLMCostInMonth_Detail.Param.ScheduleID },
                success: function (res) {
                    $rootScope.IsLoading = false;
                    $scope.driverFeeGrid_Options.dataSource.read();
                    win.close();
                },
                error: function (res) {
                    $rootScope.IsLoading = false;
                }
            });
        }
    }

    $scope.cboDriver_Options = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true, dataTextField: 'DriverName', dataValueField: 'DriverID',
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'DriverID',
                fields: {
                    DriverID: { type: 'number' },
                    DriverName: { type: 'string' },
                }
            }
        }),
        change: function (e) {
        }
    }

    $scope.LoadDriver = function(){
        Common.Services.Call($http, {
            url: Common.Services.url.FLM,
            method: _FLMCostInMonth_Detail.URL.DriverFee_DriverList,
            data: { scheduleID: _FLMCostInMonth_Detail.Params.ScheduleID },
            success: function (res) {
                if (Common.HasValue(res)) {
                    $scope.cboDriver_Options.dataSource.data(res.Data);
                }
            }
        });
    }

    $scope.cboTypeOfScheduleFee_Options = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true, dataTextField: 'TypeName', dataValueField: 'ID',
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    TypeName: { type: 'string' },
                }
            }
        }),
        change: function (e) {
        }
    }

    Common.Services.Call($http, {
        url: Common.Services.url.CAT,
        method: _FLMCostInMonth_Detail.URL.TypeOfScheduleFee,
        data: {},
        success: function (res) {
            if (Common.HasValue(res)) {
                
                $scope.cboTypeOfScheduleFee_Options.dataSource.data(res.Data);
            }
        }
    });

    $scope.driverFee_GridDestroy_Click = function ($event, data) {
        $event.preventDefault();
        $rootScope.Message({
            Type: Common.Message.Type.Confirm,
            NotifyType: Common.Message.NotifyType.SUCCESS,
            Title: 'Thông báo',
            Msg: 'Bạn muốn xóa dữ liệu đã chọn?',
            Close: null,
            Ok: function () {
                Common.Services.Call($http, {
                    url: Common.Services.url.FLM,
                    method: _FLMCostInMonth_Detail.URL.DriverFee_Delete,
                    data: { 'id': data.ID },
                    success: function (res) {
                        $rootScope.IsLoading = false;
                        $scope.driverFeeGrid_Options.dataSource.read();
                    },
                    error: function (res) {
                        $rootScope.IsLoading = false;
                    }
                })
            }
        })
    }
    //#endregion

    //#region assetFee
    $scope.assetFeeGrid_Options = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.FLM,
            method: _FLMCostInMonth_Detail.URL.AssetFee_List,
            readparam: function () { return { scheduleID: _FLMCostInMonth_Detail.Params.ScheduleID }; },
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number', editable: false, },
                    Price:{type:'number'}
                }
            }
        }),
        selectable: false, reorderable: true, editable: false,
        height: '100%', pageable: true, sortable: false, columnMenu: false, filterable: { mode: 'row' }, resizable: true,
        columns: [
            {
                title: ' ', width: '90px',
                template: '<a href="/" ng-click="assetFee_GridEdit_Click($event,assetFee_win,dataItem,assetFee_vform)" class="k-button"><i class="fa fa-pencil"></i></a>',
                filterable: false, sortable: false
            },
            {
                field: 'AssetNo', title: 'Xe', width: 150,
                filterable: { cell: { operator: 'contains', showOperators: false }, editable: false, }
            },
            {
                field: 'TypeOfScheduleFeeCode', title: 'Mã loại tính phí', width: 120,
                filterable: { cell: { operator: 'contains', showOperators: false }, editable: false, }
            },
            {
                field: 'TypeOfScheduleFeeName', title: 'Loại tính phí', width: 120,
                filterable: { cell: { operator: 'contains', showOperators: false }, editable: false, }
            },
            {
                field: 'Price', title: 'Giá', width: 120, template: "#=Price==null?'':Common.Number.ToMoney(Price)#",
                filterable: { cell: { operator: 'equal', showOperators: false }, editable: false, }
            },
            { title: ' ', filterable: false, sortable: false }
        ]
    }

    $scope.assetFee_AddNew = function ($event, win, vform) {
        $event.preventDefault();
        $scope.LoadItemAssetFee(0, win, vform);
    }

    $scope.assetFee_GridEdit_Click = function ($event, win, data, vform) {
        $event.preventDefault();
        $scope.LoadItemAssetFee(data.id, win, vform);
    }

    $scope.LoadItemAssetFee = function (id, win, vform) {
        vform({ clear: true });
        $rootScope.IsLoading = true;
        Common.Services.Call($http, {
            url: Common.Services.url.FLM,
            method: _FLMCostInMonth_Detail.URL.AssetFee_Get,
            data: { 'id': id, scheduleID: _FLMCostInMonth_Detail.Param.ScheduleID },
            success: function (res) {
                $rootScope.IsLoading = false;
                $scope.ItemAssetFee = res;

                win.center();
                win.open();
            },
            error: function (res) {
                $rootScope.IsLoading = false;
            }
        })
    }

    $scope.assetFee_Save_Click = function ($event, win, vform) {
        $event.preventDefault();
        if (vform()) {
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.FLM,
                method: _FLMCostInMonth_Detail.URL.AssetFee_Save,
                data: { item: $scope.ItemAssetFee, scheduleID: _FLMCostInMonth_Detail.Param.ScheduleID },
                success: function (res) {
                    $rootScope.IsLoading = false;
                    $scope.assetFeeGrid_Options.dataSource.read();
                    win.close();
                },
                error: function (res) {
                    $rootScope.IsLoading = false;
                }
            });
        }
    }

    $scope.cboAsset_Options = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true, dataTextField: 'RegNo', dataValueField: 'ID',
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    RegNo: { type: 'string' },
                }
            }
        }),
        change: function (e) {
        }
    }

    Common.Services.Call($http, {
        url: Common.Services.url.FLM,
        method: _FLMCostInMonth_Detail.URL.AssetFee_AsestList,
        data: {},
        success: function (res) {
                
            $scope.cboAsset_Options.dataSource.data(res.Data);
            var data = [];
            data.push({ ID: -1, RegNo: ' ' });
            Common.Data.Each(res.Data, function (o) {
                data.push(o);
            })
            //$scope.cboDriverAsset_Options.dataSource.data(data);
        }
    });

    $scope.assetFee_GridDestroy_Click = function ($event, data) {
        $event.preventDefault();
        $rootScope.Message({
            Type: Common.Message.Type.Confirm,
            NotifyType: Common.Message.NotifyType.SUCCESS,
            Title: 'Thông báo',
            Msg: 'Bạn muốn xóa dữ liệu đã chọn?',
            Close: null,
            Ok: function () {
                Common.Services.Call($http, {
                    url: Common.Services.url.FLM,
                    method: _FLMCostInMonth_Detail.URL.AssetFee_Delete,
                    data: { 'id': data.ID },
                    success: function (res) {
                        $rootScope.IsLoading = false;
                        $scope.assetFeeGrid_Options.dataSource.read();
                    },
                    error: function (res) {
                        $rootScope.IsLoading = false;
                    }
                })
            }
        })
    }
    //#endregion

    //#region assistantFee
    $scope.assistantFee_GridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.FLM,
            method: _FLMCostInMonth_Detail.URL.AssistantFee_List,
            readparam: function () { return { scheduleID: _FLMCostInMonth_Detail.Params.ScheduleID }; },
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number', editable: false, },
                    IsAssistant: { type: "boolean" },
                    Price:{type:'number'},
                }
            }
        }),
        selectable: false, reorderable: true, editable: false,
        height: '100%', pageable: true, sortable: false, columnMenu: false, filterable: { mode: 'row' }, resizable: true,
        columns: [
            {
                title: ' ', width: '90px',
                template: '<a href="/" ng-click="assistantFee_GridEdit_Click($event,assistantFee_win,dataItem,assistantFee_vform)" class="k-button"><i class="fa fa-pencil"></i></a>',
                filterable: false, sortable: false
            },
            {
                field: 'TypeOfScheduleFeeCode', title: 'Mã loại tính phí', width: 120,
                filterable: { cell: { operator: 'contains', showOperators: false }, editable: false, }
            },
            {
                field: 'TypeOfScheduleFeeName', title: 'Loại tính phí', width: 120,
                filterable: { cell: { operator: 'contains', showOperators: false }, editable: false, }
            },
            {
                field: 'Price', title: 'Giá', width: 120, template: "#=Price==null?'':Common.Number.ToMoney(Price)#",
                filterable: { cell: { operator: 'equal', showOperators: false }, editable: false, }
            },
            {
                field: 'IsAssistant', title: 'Lái xe', width: 120, template: '<input type="checkbox" ng-model="dataItem.IsAssistant" disabled="disabled"/>',
                filterable: {
                    cell: {
                        template: function (container) {
                            container.element.kendoComboBox({
                                dataSource: [{ Text: 'Phụ xe', Value: false }, { Text: 'Lái xe', Value: true }, { Text: 'Tất cả', Value: '' }],
                                dataTextField: "Text", dataValueField: "Value"
                            });
                        },
                        showOperators: false
                    }
                }
            },
            { title: ' ', filterable: false, sortable: false }
        ]
    }

    $scope.assistantFee_AddNew = function ($event, win, vform) {
        $event.preventDefault();
        $scope.LoadItemAssistantFee(0, win, vform);
    }

    $scope.assistantFee_GridEdit_Click = function ($event, win, data, vform) {
        $event.preventDefault();
        $scope.LoadItemAssistantFee(data.id, win, vform);
    }

    $scope.LoadItemAssistantFee = function (id, win, vform) {
        vform({ clear: true });
        $rootScope.IsLoading = true;
        Common.Services.Call($http, {
            url: Common.Services.url.FLM,
            method: _FLMCostInMonth_Detail.URL.AssistantFee_Get,
            data: { 'id': id, scheduleID: _FLMCostInMonth_Detail.Param.ScheduleID },
            success: function (res) {
                $rootScope.IsLoading = false;
                $scope.ItemAssistantFee = res;

                win.center();
                win.open();
            },
            error: function (res) {
                $rootScope.IsLoading = false;
            }
        })
    }

    $scope.assistantFee_Save_Click = function ($event, win, vform) {
        $event.preventDefault();
        if (vform()) {
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.FLM,
                method: _FLMCostInMonth_Detail.URL.AssistantFee_Save,
                data: { item: $scope.ItemAssistantFee, scheduleID: _FLMCostInMonth_Detail.Param.ScheduleID },
                success: function (res) {
                    $rootScope.IsLoading = false;
                    $scope.assistantFee_GridOptions.dataSource.read();
                    win.close();
                },
                error: function (res) {
                    $rootScope.IsLoading = false;
                }
            });
        }
    }

    $scope.assistantFee_GridDestroy_Click = function ($event, data) {
        $event.preventDefault();
        $rootScope.Message({
            Type: Common.Message.Type.Confirm,
            NotifyType: Common.Message.NotifyType.SUCCESS,
            Title: 'Thông báo',
            Msg: 'Bạn muốn xóa dữ liệu đã chọn?',
            Close: null,
            Ok: function () {
                Common.Services.Call($http, {
                    url: Common.Services.url.FLM,
                    method: _FLMCostInMonth_Detail.URL.AssistantFee_Delete,
                    data: { 'id': data.ID },
                    success: function (res) {
                        $rootScope.IsLoading = false;
                        $scope.assetFeeGrid_Options.dataSource.read();
                    },
                    error: function (res) {
                        $rootScope.IsLoading = false;
                    }
                })
            }
        })
    }

    $scope.cboAssistantTypeOfScheduleFee_Options = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true, dataTextField: 'TypeName', dataValueField: 'ID',
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    TypeName: { type: 'string' },
                }
            }
        }),
        change: function (e) {
        }
    }

    Common.Services.Call($http, {
        url: Common.Services.url.CAT,
        method: _FLMCostInMonth_Detail.URL.TypeOfScheduleFee,
        data: {},
        success: function (res) {
            if (Common.HasValue(res)) {

                $scope.cboAssistantTypeOfScheduleFee_Options.dataSource.data(res.Data);
            }
        }
    });

    $scope.numAssistantDay_options = { format: 'n0', spinners: false, culture: 'en-US', min: 0, step: 1, decimals: 0, }
    $scope.numAssistantPrice_options = { format: 'n0', spinners: false, culture: 'en-US', min: 0, step: 1, decimals: 0, }
    //#endregion

    //#region Schedule Excel
    $scope.schedule_Excel_Click = function ($event, win) {
        $event.preventDefault();
        $rootScope.UploadExcel({
            Path: Common.FolderUpload.Import,
            columns: [
                { field: 'DriverCode', title: 'Mã tài xế', width: '150px', filterable: { cell: { showOperators: false, operator: "contains" } } },
                { field: 'DriverName', title: 'Tên tài xế', width: '150px', filterable: { cell: { showOperators: false, operator: "contains" } } }
            ],
            Download: function () {
                $rootScope.IsLoading = true;
                Common.Services.Call($http, {
                    url: Common.Services.url.FLM,
                    method: _FLMCostInMonth_Detail.URL.ScheduleExcel_Export,
                    data: { scheduleID: _FLMCostInMonth_Detail.Param.ScheduleID },
                    success: function (res) {
                        $rootScope.IsLoading = false;
                        $rootScope.DownloadFile(res);
                    }
                })
            },
            Upload: function (e, callback) {
                $rootScope.IsLoading = true;
                Common.Services.Call($http, {
                    url: Common.Services.url.FLM,
                    method: _FLMCostInMonth_Detail.URL.ScheduleExcel_Check,
                    data: { file: e, scheduleID: _FLMCostInMonth_Detail.Param.ScheduleID },
                    success: function (data) {
                        callback(data);
                        $rootScope.IsLoading = false;
                    }
                })
            },
            Window: win,
            Complete: function (e, data) {
                $rootScope.IsLoading = true;
                Common.Services.Call($http, {
                    url: Common.Services.url.FLM,
                    method: _FLMCostInMonth_Detail.URL.ScheduleExcel_Save,
                    data: { lst: data, scheduleID: _FLMCostInMonth_Detail.Param.ScheduleID },
                    success: function (res) {
                        $scope.InitView_Schedule();
                    }
                })
            }
        })
    }

    //#region DriverFee Excel
    $scope.driverFee_Excel_Click = function ($event, win) {
        $event.preventDefault();
        $rootScope.UploadExcel({
            Path: Common.FolderUpload.Import,
            columns: [
                { field: 'DriverCode', title: 'Mã tài xế', width: '150px', filterable: { cell: { showOperators: false, operator: "contains" } } },
                { field: 'DriverName', title: 'Tên tài xế', width: '150px', filterable: { cell: { showOperators: false, operator: "contains" } } },
                { field: 'TypeOfScheduleFeeName', title: 'Loại tính phí', width: '150px', filterable: { cell: { showOperators: false, operator: "contains" } } }
            ],
            Download: function () {
                $rootScope.IsLoading = true;
                Common.Services.Call($http, {
                    url: Common.Services.url.FLM,
                    method: _FLMCostInMonth_Detail.URL.DriverFeeExcel_Export,
                    data: { scheduleID: _FLMCostInMonth_Detail.Param.ScheduleID },
                    success: function (res) {
                        $rootScope.IsLoading = false;
                        $rootScope.DownloadFile(res);
                    }
                })
            },
            Upload: function (e, callback) {
                $rootScope.IsLoading = true;
                Common.Services.Call($http, {
                    url: Common.Services.url.FLM,
                    method: _FLMCostInMonth_Detail.URL.DriverFeeExcel_Check,
                    data: { file: e, scheduleID: _FLMCostInMonth_Detail.Param.ScheduleID },
                    success: function (data) {
                        callback(data);
                        $rootScope.IsLoading = false;
                    }
                })
            },
            Window: win,
            Complete: function (e, data) {
                $rootScope.IsLoading = true;
                Common.Services.Call($http, {
                    url: Common.Services.url.FLM,
                    method: _FLMCostInMonth_Detail.URL.DriverFeeExcel_Save,
                    data: { lst: data, scheduleID: _FLMCostInMonth_Detail.Param.ScheduleID },
                    success: function (res) {
                        $scope.driverFeeGrid_Options.dataSource.read();
                        $rootScope.IsLoading = false;
                    }
                })
            }
        })
    }
    //#endregion

    //#region AssetFee Excel
    $scope.assetFee_Excel_Click = function ($event, win) {
        $event.preventDefault();
        $rootScope.UploadExcel({
            Path: Common.FolderUpload.Import,
            columns: [
                { field: 'AssetNo', title: 'Mã tài xế', width: '150px', filterable: { cell: { showOperators: false, operator: "contains" } } },
                { field: 'TypeOfScheduleFeeName', title: 'Loại tính phí', width: '150px', filterable: { cell: { showOperators: false, operator: "contains" } } }
            ],
            Download: function () {
                $rootScope.IsLoading = true;
                Common.Services.Call($http, {
                    url: Common.Services.url.FLM,
                    method: _FLMCostInMonth_Detail.URL.AssetFeeExcel_Export,
                    data: { scheduleID: _FLMCostInMonth_Detail.Param.ScheduleID },
                    success: function (res) {
                        $rootScope.IsLoading = false;
                        $rootScope.DownloadFile(res);
                    }
                })
            },
            Upload: function (e, callback) {
                $rootScope.IsLoading = true;
                Common.Services.Call($http, {
                    url: Common.Services.url.FLM,
                    method: _FLMCostInMonth_Detail.URL.AssetFeeExcel_Check,
                    data: { file: e, scheduleID: _FLMCostInMonth_Detail.Param.ScheduleID },
                    success: function (data) {
                        callback(data);
                        $rootScope.IsLoading = false;
                    }
                })
            },
            Window: win,
            Complete: function (e, data) {
                $rootScope.IsLoading = true;
                Common.Services.Call($http, {
                    url: Common.Services.url.FLM,
                    method: _FLMCostInMonth_Detail.URL.AssetFeeExcel_Save,
                    data: { lst: data, scheduleID: _FLMCostInMonth_Detail.Param.ScheduleID },
                    success: function (res) {
                        $scope.assetFeeGrid_Options.dataSource.read();
                        $rootScope.IsLoading = false;
                    }
                })
            }
        })
    }
    //#endregion
    $scope.WinClose_Click = function ($event, win) {
        $event.preventDefault();
        win.close();
    }

    $scope.Calculate_Click = function ($event) {
        $event.preventDefault();
        $rootScope.IsLoading = true;
        Common.Services.Call($http, {
            url: Common.Services.url.FLM,
            method: _FLMCostInMonth_Detail.URL.CaculateFee,
            data: { scheduleID: _FLMCostInMonth_Detail.Param.ScheduleID },
            success: function (res) {
                $rootScope.IsLoading = false;
                $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                $scope.driverFeeGrid_Options.dataSource.read();
                $scope.assetFeeGrid_Options.dataSource.read();
                $scope.assistantFee_GridOptions.dataSource.read();
            },
            error: function (e) {
                $rootScope.IsLoading = false;
            }
        });
    };
    $scope.Refresh_Click = function ($event) {
        $event.preventDefault();
        $rootScope.IsLoading = true;
        Common.Services.Call($http, {
            url: Common.Services.url.FLM,
            method: _FLMCostInMonth_Detail.URL.RefreshFee,
            data: { scheduleID: _FLMCostInMonth_Detail.Param.ScheduleID },
            success: function (res) {
                $rootScope.IsLoading = false;
                $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                $scope.driverFeeGrid_Options.dataSource.read();
                $scope.assetFeeGrid_Options.dataSource.read();
                $scope.assistantFee_GridOptions.dataSource.read();
            },
            error: function (e) {
                $rootScope.IsLoading = false;
            }
        });
    };

    $scope.driver_Update_Click = function ($event, grid) {
        $event.preventDefault();
        $rootScope.IsLoading = true;
        Common.Services.Call($http, {
            url: Common.Services.url.FLM,
            method: _FLMCostInMonth_Detail.URL.UpdateInfo,
            data: { scheduleID: _FLMCostInMonth_Detail.Param.ScheduleID },
            success: function (res) {
                $rootScope.IsLoading = false;
                grid.dataSource.read();
                $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
            },
            error: function (e) {
                $rootScope.IsLoading = false;
            }
        });

    }

    $scope.numFeeBase_options = { format: '#', spinners: false, culture: 'en-US', min: 0, max: 3000, step: 1, decimals: 0, }

    $scope.Close_Click = function ($event) {
        $event.preventDefault();

        $state.go("main.FLMSchedule.Index");
    };
}]);

