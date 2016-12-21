/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />

//#region const

var _MONMonitor_Approve_DICost = {
    URL: {
        Read: 'MONInput_DIFLMFee_List',
        Approved: 'MONInput_DIFLMFee_Approved',
        DriverList: 'MONInput_DIFLMFee_DriverList',
        StationCost: 'MONInput_DIFLMFee_StationCostList',
        TroubleCost: 'MONInput_DIFLMFee_TroubleCostList',
        TroubleCostNotIn_List: 'MONInput_DIFLMFee_TroubleCostNotIn_List',
        TroubleCostNotIn_Save: 'MONInput_DIFLMFee_TroubleCostNotIn_SaveList',
        TroubleCost_Delete: 'MONInput_DIFLMFee_TroubleCost_DeleteList',
        TroubleCostSave: 'MONInput_DIFLMFee_TroubleCostSave',
        StationCostSave: 'MONInput_DIFLMFee_StationCostSave',
        Update: 'MONInput_DIFLMFee_Save',
        GetDrivers: 'MONInput_DIFLMFee_GetDrivers',
        SaveDrivers: 'MONInput_DIFLMFee_SaveDrivers',

        ExcelExport: 'PODFLMInput_Export',
        ExcelCheck: 'PODFLMInput_Excel_Check',
        ExcelImport: 'PODFLMInput_Excel_Import',
    },
    Data: {
        DIPODStatus: []
    }
}

//#endregion

angular.module('myapp').controller('MONMonitor_Approve_DICostCtrl', ['$rootScope', '$scope', '$http', '$location', '$state', '$timeout', function ($rootScope, $scope, $http, $location, $state, $timeout) {

    Common.Log('MONMonitor_Approve_DICostCtrl');
    $rootScope.IsLoading = false;
    $scope.HasChoose = false;
    $scope.HasApproved = false;
    $scope.DITOMasterID = 0;
    $scope.dataDriver = {
        'DriverID1': -1,
        'DriverID2': -1,
        'DriverID3': -1,
        'DriverID4': -1,
        'DriverID5': -1,
        'TypeOfDriverID1': -1,
        'TypeOfDriverID2': -1,
        'TypeOfDriverID3': -1,
        'TypeOfDriverID4': -1,
        'TypeOfDriverID5': -1,
    };

    $scope.ItemSearch = {
        DateFrom: (new Date()).addDays(-7),
        DateTo: (new Date()),
    }

    $scope.DIFLMFee_SearchClick = function ($event) {
        $event.preventDefault();
        if (!Common.HasValue($scope.ItemSearch.DateFrom) || !Common.HasValue($scope.ItemSearch.DateTo)) {
            $rootScope.Message({
                Type: Common.Message.Type.Alert,
                NotifyType: Common.Message.NotifyType.ERROR,
                Title: 'Thông báo',
                Msg: 'Vui lòng chọn Từ ngày- Đến ngày chính xác',
                Close: null,
                Ok: null
            })
        }
        else if ($scope.ItemSearch.DateFrom > $scope.ItemSearch.DateTo) {
            $rootScope.Message({
                Type: Common.Message.Type.Alert,
                NotifyType: Common.Message.NotifyType.ERROR,
                Title: 'Thông báo',
                Msg: 'Vui lòng chọn Từ ngày nhỏ hơn Đến ngày',
                Close: null,
                Ok: null
            })
        }
        else {
            $scope.DIFLMFee_gridOptions.dataSource.read();
        }
    }

    $scope.DriverID_Options = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true, dataTextField: 'DriverName', dataValueField: 'ID',
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    DriverName: { type: 'string' },
                }
            },
            change: function (e) {
                Common.Log(e.keyCode);
                if (e.keyCode === 13) {
                    e.preventDefault();
                }

            }
        })
    }

    $scope.TypeOfDriverID1_Options = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true, dataTextField: 'ValueOfVar', dataValueField: 'ID',
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
        select: function (e) {
            var tr = $(e.sender.element).closest('tr');
            var Item = $scope.DIFLMFee_grid.dataItem(tr);
            if (Common.HasValue(Item)) {
                if (Item.DriverID1 == -1) {
                    $rootScope.Message({
                        Msg: 'Chưa chọn tài xế.',
                        NotifyType: Common.Message.NotifyType.ERROR
                    });
                    Item.TypeOfDriverID1 = -1;
                }
            }
            else {
                if ($scope.dataDriver.DriverID1 == -1) {
                    $rootScope.Message({
                        Msg: 'Chưa chọn tài xế.',
                        NotifyType: Common.Message.NotifyType.ERROR
                    });
                    $scope.dataDriver.TypeOfDriverID1 = -1;
                    e.sender.value(-1);
                }
            }
        },
    }

    $scope.TypeOfDriverID2_Options = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true, dataTextField: 'ValueOfVar', dataValueField: 'ID',
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
        select: function (e) {
            var tr = $(e.sender.element).closest('tr');
            var Item = $scope.DIFLMFee_grid.dataItem(tr);
            if (Common.HasValue(Item)) {
                if (Item.DriverID2 == -1) {
                    $rootScope.Message({
                        Msg: 'Chưa chọn tài xế.',
                        NotifyType: Common.Message.NotifyType.ERROR
                    });
                    Item.TypeOfDriverID2 = -1;
                }
            }
            else {
                if ($scope.dataDriver.DriverID2 == -1) {
                    $rootScope.Message({
                        Msg: 'Chưa chọn tài xế.',
                        NotifyType: Common.Message.NotifyType.ERROR
                    });
                    $scope.dataDriver.TypeOfDriverID2 = -1;
                    e.sender.value(-1);
                }
            }
        },
    }

    $scope.TypeOfDriverID3_Options = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true, dataTextField: 'ValueOfVar', dataValueField: 'ID',
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    ValueOfVar: { type: 'string' },
                }
            },
        }),
        select: function (e) {
            if ($scope.dataDriver.DriverID3 == -1) {
                $rootScope.Message({
                    Msg: 'Chưa chọn tài xế.',
                    NotifyType: Common.Message.NotifyType.ERROR
                });
                $scope.dataDriver.TypeOfDriverID3 = -1;
            }
        },
    }

    $scope.TypeOfDriverID4_Options = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true, dataTextField: 'ValueOfVar', dataValueField: 'ID',
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
        select: function (e) {
            if ($scope.dataDriver.DriverID4 == -1) {
                $rootScope.Message({
                    Msg: 'Chưa chọn tài xế.',
                    NotifyType: Common.Message.NotifyType.ERROR
                });
                $scope.dataDriver.TypeOfDriverID4 = -1;
            }
        },
    }

    $scope.TypeOfDriverID5_Options = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true, dataTextField: 'ValueOfVar', dataValueField: 'ID',
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
        select: function (e) {
            if ($scope.dataDriver.DriverID5 == -1) {
                $rootScope.Message({
                    Msg: 'Chưa chọn tài xế.',
                    NotifyType: Common.Message.NotifyType.ERROR
                });
                $scope.dataDriver.TypeOfDriverID5 = -1;
                e.sender.value(-1);
            }
        },
    }

    Common.Services.Call($http, {
        url: Common.Services.url.MON,
        method: _MONMonitor_Approve_DICost.URL.DriverList,
        success: function (res) {
            var data = { 'ID': -1, 'DriverName': '' };
            res.Data.unshift(data);
            $scope.DriverID_Options.dataSource.data(res.Data);
        }
    });

    Common.ALL.Get($http, {
        url: Common.ALL.URL.ALL_SYSVarTypeOfDriver,
        success: function (data) {
            var re = { 'ID': -1, 'ValueOfVar': '' };
            data.unshift(re);
            $scope.TypeOfDriverID1_Options.dataSource.data(data);
            $scope.TypeOfDriverID2_Options.dataSource.data(data);
            $scope.TypeOfDriverID3_Options.dataSource.data(data);
            $scope.TypeOfDriverID4_Options.dataSource.data(data);
            $scope.TypeOfDriverID5_Options.dataSource.data(data);
        }
    })

    $scope.DIFLMFee_gridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.MON,
            method: _MONMonitor_Approve_DICost.URL.Read,
            readparam: function () {
                return {
                    'dtFrom': $scope.ItemSearch.DateFrom,
                    'dtTo': $scope.ItemSearch.DateTo,
                };
            },
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number', editable: false, nullable: false },
                    ETD: { type: 'date' },
                    ETA: { type: 'date' },
                    AETD: { type: 'date' },
                    AETA: { type: 'date' },
                    KmStart: { type: 'number' },
                    KmEnd: { type: 'number' },
                    TotalStationCost: { type: 'number' },
                    TotalTroubleCost: { type: 'number' },
                    IsChoose: { type: 'bool', defaultValue: false },
                }
            },
            filter: {
                logic: "or",
                filters:[
                    { field: "TotalTroubleCost", operator: "gt", value: 0 },
                    { field: "TotalStationCost", operator: "gt", value: 0 },
                ]
                
            }
        }),
        selectable: false, reorderable: false, editable: false,
        height: '100%', pageable: Common.PageSize, sortable: false, columnMenu: false, filterable: { mode: 'row' }, resizable: true,
        columns: [
            {
                title: ' ', width: '85px', field: 'F_Command',
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,DIFLMFee_grid,DIFLMFee_gridChooseChange)" />',
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,DIFLMFee_grid,DIFLMFee_gridChooseChange)" />' +
                    '<a href="/" ng-show="!dataItem.IsApproved" ng-click="DIFLMFee_SaveClick($event,DIFLMFee_grid)" class="k-button" data-title="Lưu"><i class="fa fa-check"></i></a>',
                filterable: false, sortable: false
            },
            { field: 'DITOMasterCode', width: 120, title: 'Mã chuyến', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'VehicleNo', width: 120, title: 'Số xe', filterable: { cell: { operator: 'contains', showOperators: false } } },
            {
                field: 'ETD', width: 120, title: 'ETD', template: "#=ETD==null?' ':Common.Date.FromJsonDMYHM(ETD)#",
                filterable: {
                    cell: {
                        template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); }, operator: 'equal', showOperators: false
                    }
                },
            },
            { field: 'TotalStationCost', width: 100, title: 'Số tiền qua trạm', template: "#=TotalStationCost==null?' ':Common.Number.ToMoney(TotalStationCost)#", filterable: { cell: { operator: 'lte', showOperators: false } } },
            { field: 'ButtonStationCost', width: 100, title: 'Thông tin trạm', template: '<a href="/" ng-click="StationCost_Click($event,dataItem,StationCost_win)" class="k-button" data-title="Hiển thị"><i class="fa fa-bars"></i></a>', filterable: { cell: { operator: 'lte', showOperators: false } } },
            { field: 'TotalTroubleCost', width: 100, title: 'Số tiền phát sinh', template: "#=TotalTroubleCost==null?' ':Common.Number.ToMoney(TotalTroubleCost)#", filterable: { cell: { operator: 'lte', showOperators: false } } },
            { field: 'ButtonTroubleCost', width: 100, title: 'Thông tin phát sinh', template: '<a href="/" ng-click="TroubleCost_Click($event,dataItem,TroubleCost_win)" class="k-button" data-title="Hiển thị"><i class="fa fa-bars"></i></a>', filterable: { cell: { operator: 'lte', showOperators: false } } },
            { field: 'ApprovedBy', width: 100, title: 'Người duyệt', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'CreatedBy', width: 100, title: 'Người tạo', filterable: { cell: { operator: 'contains', showOperators: false } } },
            {
                field: 'Note1', width: 150, title: 'Ghi chú 1', template: '<input type="text" class="k-textbox EvenEnter" style="width: 100%" ng-model="dataItem.Note1" />', filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'ETA', width: 120, title: 'ETA', template: "#=ETA==null?' ':Common.Date.FromJsonDMYHM(ETA)#",
                filterable: {
                    cell: {
                        template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); }, operator: 'equal', showOperators: false
                    }
                },
            },
            {
                field: 'AETD', width: 120, title: 'AETD', template: "#=AETD==null?' ':Common.Date.FromJsonDMYHM(AETD)#",
                filterable: {
                    cell: {
                        template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); }, operator: 'equal', showOperators: false
                    }
                },
            },
            {
                field: 'AETA', width: 120, title: 'AETA', template: "#=AETA==null?' ':Common.Date.FromJsonDMYHM(AETA)#",
                filterable: {
                    cell: {
                        template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); }, operator: 'equal', showOperators: false
                    }
                },
            },
            { field: 'DriverName1', width: 150, title: 'Tài xế', filterable: { cell: { operator: 'contains', showOperators: false } },},
            { field: 'DriverName2', width: 150, title: 'Phụ xe', filterable: { cell: { operator: 'contains', showOperators: false } },},
            { field: 'ButtonDriver', width: 100, title: 'Thông tin tài xế', template: '<a href="/" ng-click="DriverList_Click($event,dataItem,DriverList_win)" class="k-button" data-title="Hiển thị"><i class="fa fa-ellipsis-h"></i></a>', filterable: { cell: { operator: 'lte', showOperators: false } } },
            { field: 'SortOrder', width: 100, title: 'Thứ tự chuyến', filterable: { cell: { operator: 'lte', showOperators: false } } },
            { field: 'KmStart', width: 100, title: 'Km đầu', filterable: { cell: { operator: 'lte', showOperators: false } } },
            { field: 'KmEnd', width: 100, title: 'Km cuối', filterable: { cell: { operator: 'lte', showOperators: false } } },
            {
                field: 'Note2', width: 120, title: 'Ghi chú 2', template: '<input type="text" class="k-textbox EvenEnter" style="width: 100%" ng-model="dataItem.Note2" />', filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            { title: ' ', field: 'F_Empty', sortable: false, filterable: false, menu: false }
        ],
        dataBound: function (e) {
            var grid = this;
            this.element.find('.EvenEnter').on("keydown", function (event) {
                var tr = $(this).closest('tr');
                var Item = grid.dataItem(tr);
                if (event.keyCode == 13) {
                    if (!Item.IsApproved) {
                        $scope.SaveDataInput(Item);
                    }
                }
            });
        }
    }

    $scope.TroubleDriverCbbOptions = {
        autoBind: true,
        valuePrimitive: true,
        ignoreCase: true,
        filter: "contains",
        dataTextField: 'DriverName',
        dataValueField: 'ID',
        dataSource: Common.DataSource.Local({ data: [] })
    }

    Common.Services.Call($http, {
        url: Common.Services.url.MON,
        method: "Monitor_DriverList",
        data: {},
        success: function (res) {
            res.Data.push({ DriverName: " ", ID: null });
            $scope.TroubleDriverCbbOptions.dataSource.data(res.Data);
        }
    })

    $scope.TroubleCostHasChoose = false;
    $scope.TroubleCost_gridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.MON,
            method: _MONMonitor_Approve_DICost.URL.TroubleCost,
            readparam: function () {
                return {
                    DITOMasterID: $scope.DITOMasterID
                };
            },
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number', editable: false, nullable: false },
                    IsChoose: { type: 'boolean', },
                }
            },
            pageSize: 20
        }),
        selectable: false, reorderable: false, editable: false,
        height: '100%', pageable: true, sortable: false, columnMenu: false, filterable: { mode: 'row' }, resizable: true,
        columns: [
            {
                title: ' ', width: '35px',
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,TroubleCost_grid,troubleCost_GridChoose_Change)" />',
                headerAttributes: { style: 'text-align: center;' }, attributes: { style: 'text-align: center;' },
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,TroubleCost_grid,troubleCost_GridChoose_Change)" />',
                filterable: false, sortable: false
            },
            { field: 'GroupOfTroubleCode', width: '100px', title: 'Mã chi phí', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'GroupOfTroubleName', width: '150px', title: 'Chi phí', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'Cost', width: '150px', title: 'Chi phí', template: '<input type="text" class="k-textbox" style="width: 100%" ng-model="dataItem.Cost" />', filterable: { cell: { operator: 'equal', showOperators: false } } },
            {
                field: 'DriverID', width: 150, title: 'Tài xế', filterable: false,
                template: '<input focus-k-combobox kendo-combo-box k-options="TroubleDriverCbbOptions"  k-ng-model="dataItem.DriverID" style="width: 100%" />',
            },
            { field: 'Note', width: '200px', title: 'Ghi chú', template: '<input type="text" class="k-textbox" style="width: 100%" ng-model="dataItem.Note" />', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { title: ' ', field: '', sortable: false, filterable: false, menu: false }
        ],
        dataBound: function (e) {
        }
    };

    $scope.troubleCost_GridChoose_Change = function ($event, grid, hasChoose) {
        $scope.TroubleCostHasChoose = hasChoose;
    }

    $scope.troubleCost_AddNew = function ($event, win) {
        $event.preventDefault();
        win.center().open();
        $scope.TroubleCostNotIn_gridOptions.dataSource.read();
    }

    $scope.troubleCost_Delete = function ($event, grid) {
        $event.preventDefault();
        var data = grid.dataSource.data();
        var datasend = [];
        Common.Data.Each(data, function (o) {
            if (o.IsChoose) datasend.push(o.ID);
        })
        if (datasend.length > 0) {
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.MON,
                method: _MONMonitor_Approve_DICost.URL.TroubleCost_Delete,
                data: { lst: datasend },
                success: function (res) {
                    $rootScope.IsLoading = false;
                    $scope.TroubleCost_gridOptions.dataSource.read();
                    $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                },
                error: function (res) {
                    $rootScope.IsLoading = false;
                }
            });

        }
    }

    $scope.TroubleCostNotInHasChoose = false;
    $scope.TroubleCostNotIn_gridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.MON,
            method: _MONMonitor_Approve_DICost.URL.TroubleCostNotIn_List,
            readparam: function () {
                return {
                    DITOMasterID: $scope.DITOMasterID
                };
            },
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number', editable: false, nullable: false },
                    IsChoose: { type: 'boolean', },
                }
            },
            pageSize: 20
        }),
        selectable: false, reorderable: false, editable: false,
        height: '100%', pageable: true, sortable: false, columnMenu: false, filterable: { mode: 'row' }, resizable: true,
        columns: [
            {
                title: ' ', width: '35px',
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,TroubleCostNotIn_grid,troubleCostNotIn_GridChoose_Change)" />',
                headerAttributes: { style: 'text-align: center;' }, attributes: { style: 'text-align: center;' },
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,TroubleCostNotIn_grid,troubleCostNotIn_GridChoose_Change)" />',
                filterable: false, sortable: false
            },
            { field: 'GroupOfTroubleCode', width: '100px', title: 'Mã chi phí', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'GroupOfTroubleName', width: '150px', title: 'Chi phí', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { title: ' ', field: '', sortable: false, filterable: false, menu: false }
        ],
        dataBound: function (e) {
        }
    }
    $scope.troubleCostNotIn_GridChoose_Change = function ($event, grid, hasChoose) {
        $scope.TroubleCostNotInHasChoose = hasChoose;
    }

    $scope.TroubleCostNotIn_Save_Click = function ($event, win, grid) {
        $event.preventDefault();
        var data = grid.dataSource.data();
        var datasend = [];
        Common.Data.Each(data, function (o) {
            if (o.IsChoose) datasend.push(o.ID);
        })
        if (datasend.length > 0) {
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.MON,
                method: _MONMonitor_Approve_DICost.URL.TroubleCostNotIn_Save,
                data: { DITOMasterID: $scope.DITOMasterID, lst: datasend },
                success: function (res) {
                    $rootScope.IsLoading = false;
                    $scope.TroubleCost_gridOptions.dataSource.read();
                    win.close();
                    $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                },
                error: function (res) {
                    $rootScope.IsLoading = false;
                }
            });

        }
    }

    $scope.StationCost_gridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.MON,
            method: _MONMonitor_Approve_DICost.URL.StationCost,
            readparam: function () {
                return {
                    DITOMasterID: $scope.DITOMasterID
                };
            },
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number', editable: false, nullable: false },
                }
            },
            pageSize: 20
        }),
        selectable: false, reorderable: false, editable: false,
        height: '100%', pageable: true, sortable: false, columnMenu: false, filterable: { mode: 'row' }, resizable: true,
        columns: [
            { field: 'LocationCode', width: '100px', title: 'Mã trạm', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'LocationName', width: '150px', title: 'Tên trạm', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'LocationAddress', width: '150px', title: 'Địa chỉ', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'Price', width: '150px', title: 'Giá', template: '<input type="text" class="k-textbox" style="width: 100%" ng-model="dataItem.Price" />', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { title: ' ', field: '', sortable: false, filterable: false, menu: false }
        ],
        dataBound: function (e) {
        }
    }

    $scope.DIFLMFee_gridChooseChange = function ($event, grid, haschoose) {
        $scope.HasChoose = haschoose;
    };


    $scope.DriverList_Click = function ($event, data, win) {
        $event.preventDefault();
        $rootScope.IsLoading = true;
        $scope.HasApproved = data.IsApproved;
        Common.Services.Call($http, {
            url: Common.Services.url.MON,
            method: _MONMonitor_Approve_DICost.URL.GetDrivers,
            data: { DITOMasterID: data.ID },
            success: function (res) {
                $scope.dataDriver = res;
                win.center().open();
                $rootScope.IsLoading = false;
            }
        });
    }

    $scope.DriverList_Save_Click = function ($event, win) {
        $event.preventDefault();
        $rootScope.IsLoading = true;
        if ($scope.dataDriver.DriverID1 == -1)
            $scope.dataDriver.TypeOfDriverID1 = -1;
        if ($scope.dataDriver.DriverID2 == -1)
            $scope.dataDriver.TypeOfDriverID2 = -1;
        if ($scope.dataDriver.DriverID3 == -1)
            $scope.dataDriver.TypeOfDriverID3 = -1;
        if ($scope.dataDriver.DriverID4 == -1)
            $scope.dataDriver.TypeOfDriverID4 = -1;
        if ($scope.dataDriver.DriverID5 == -1)
            $scope.dataDriver.TypeOfDriverID5 = -1;

        Common.Services.Call($http, {
            url: Common.Services.url.MON,
            method: _MONMonitor_Approve_DICost.URL.SaveDrivers,
            data: { DITOMasterID: $scope.dataDriver.DITOMasterID, item: $scope.dataDriver },
            success: function (res) {
                $scope.DIFLMFee_gridOptions.dataSource.read();
                $rootScope.Message({
                    Msg: 'Đã cập nhật.',
                    NotifyType: Common.Message.NotifyType.SUCCESS
                });
                win.close();
                $rootScope.IsLoading = false;
            }
        });
    }

    $scope.StationCost_Click = function ($event, data, win) {
        $event.preventDefault();
        $scope.DITOMasterID = data.ID;
        $scope.HasApproved = data.IsApproved;
        $scope.StationCost_gridOptions.dataSource.read();
        $timeout(function () { $scope.StationCost_grid.resize(); }, 1);
        win.center().open();
    }

    $scope.TroubleCost_Click = function ($event, data, win) {
        $event.preventDefault();
        $scope.DITOMasterID = data.ID;
        $scope.HasApproved = data.IsApproved;
        $scope.TroubleCost_gridOptions.dataSource.read();
        $timeout(function () { $scope.TroubleCost_grid.resize(); }, 1);
        win.center().open();
    }

    $scope.TroubleCost_Save_Click = function ($event, win) {
        $event.preventDefault();
        $rootScope.IsLoading = true;
        Common.Services.Call($http, {
            url: Common.Services.url.MON,
            method: _MONMonitor_Approve_DICost.URL.TroubleCostSave,
            data: { lst: $scope.TroubleCost_gridOptions.dataSource.data() },
            success: function (res) {
                $rootScope.Message({ Msg: 'Lưu thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                $scope.DIFLMFee_grid.dataSource.read();
                $rootScope.IsLoading = false;
                win.close();
            }
        });
    }

    $scope.StationCost_Save_Click = function ($event, win) {
        $event.preventDefault();
        $rootScope.IsLoading = true;

        Common.Services.Call($http, {
            url: Common.Services.url.MON,
            method: _MONMonitor_Approve_DICost.URL.StationCostSave,
            data: { lst: $scope.StationCost_gridOptions.dataSource.data() },
            success: function (res) {
                $rootScope.Message({ Msg: 'Lưu thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                $scope.DIFLMFee_grid.dataSource.read();
                $rootScope.IsLoading = false;
                win.close();
            }
        });
    }

    $scope.DIFLMFee_SaveClick = function ($event, grid) {
        $event.preventDefault();
        var tr = $($event.target).closest('tr');
        var item = grid.dataItem(tr);
        if (Common.HasValue(item)) {
            $scope.SaveDataInput(item);
        }
    }

    $scope.SaveDataInput = function (item) {
        $rootScope.IsLoading = true;
        if (item.DriverID1 == -1)
            item.TypeOfDriverID1 = -1;
        if (item.DriverID2 == -1)
            item.TypeOfDriverID2 = -1;

        Common.Services.Call($http, {
            url: Common.Services.url.MON,
            method: _MONMonitor_Approve_DICost.URL.Update,
            data: { item: item },
            success: function (res) {
                $rootScope.IsLoading = false;
                $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                $scope.DIFLMFee_gridOptions.dataSource.read();
            },
            error: function (res) {
                $rootScope.IsLoading = false;
            }
        });
    };

    $scope.FLMDIInput_Approved_Click = function ($event, grid,type) {
        $event.preventDefault();
        var lstid = [];
        var data = grid.dataSource.data();
        $.each(data, function (i, v) {
            if (v.IsChoose == true)
                lstid.push(v.ID);
        });

        if (lstid.length > 0) {
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.MON,
                method: _MONMonitor_Approve_DICost.URL.Approved,
                data: { lst: lstid, type : type },
                success: function (res) {
                    $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                    grid.dataSource.read();
                    $scope.HasChoose = false;
                    $rootScope.IsLoading = false;
                }
            });
        }
    }


    $scope.Close_Click = function ($event, win) {
        $event.preventDefault();
        win.close();
    };
    $scope.Back_Click = function ($event, win) {
        $event.preventDefault();
        $state.go("main.MONMonitor.Input")
    };

    $scope.ShowSetting = function ($event, grid) {
        $event.preventDefault();

        $rootScope.ShowSetting({
            ListView: views.MONCost,
            event: $event,
            grid: grid,
            current: $state.current
        });
    };

    $scope.HideSetting = function ($event) {
        $event.preventDefault();
        $rootScope.HideSetting();
    }

}]);