/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />

//#region const

var _PODFLMCO_Input = {
    URL: {
        Read: 'PODFLMCOInput_List',
        FLMCOInput_Approved: 'PODFLMCOInput_Approved',
        DriverList: 'PODFLMCOInput_DriverList',
        StationCost: 'PODFLMCOInput_StationCostList',
        TroubleCost: 'PODFLMCOInput_TroubleCostList',
        TroubleCostNotIn_List: 'PODFLMCOInput_TroubleCostNotIn_List',
        TroubleCostNotIn_Save: 'PODFLMCOInput_TroubleCostNotIn_SaveList',
        TroubleCost_Delete: 'PODFLMCOInput_TroubleCost_DeleteList',
        TroubleCostSave: 'PODFLMCOInput_TroubleCostSave',
        StationCostSave: 'PODFLMCOInput_StationCostSave',
        Update: 'PODFLMCOInput_Save',
        GetDrivers: 'PODFLMCOInput_GetDrivers',
        SaveDrivers: 'PODFLMCOInput_SaveDrivers',

        ExcelExport: 'PODFLMInput_Export',
        ExcelCheck: 'PODFLMInput_Excel_Check',
        ExcelImport: 'PODFLMInput_Excel_Import',
    },
    Data: {
        DIPODStatus: []
    }
}

//#endregion

angular.module('myapp').controller('PODFLMCO_InputCtrl', ['$rootScope', '$scope', '$http', '$location', '$state', '$timeout', function ($rootScope, $scope, $http, $location, $state, $timeout) {

    Common.Log('PODFLMCO_InputCtrl');
    $rootScope.IsLoading = false;
    $scope.HasChoose = false;
    $scope.COTOMasterID = 0;
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

    $scope.PODInputItem = {
        DateFrom: (new Date()).addDays(-7),
        DateTo: (new Date()),
    }

    $scope.PODInput_SearchClick = function ($event) {
        $event.preventDefault();
        if (!Common.HasValue($scope.PODInputItem.DateFrom) || !Common.HasValue($scope.PODInputItem.DateTo)) {
            $rootScope.Message({
                Type: Common.Message.Type.Alert,
                NotifyType: Common.Message.NotifyType.ERROR,
                Title: 'Thông báo',
                Msg: 'Vui lòng chọn Từ ngày- Đến ngày chính xác',
                Close: null,
                Ok: null
            })
        }
        else if ($scope.PODInputItem.DateFrom > $scope.PODInputItem.DateTo) {
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
            $scope.PODInput_gridOptions.dataSource.read();
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
            var Item = $scope.PODInput_grid.dataItem(tr);
            if (Common.HasValue(Item)) {
                if(Item.DriverID1 == -1)
                {
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
            var Item = $scope.PODInput_grid.dataItem(tr);
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
        url: Common.Services.url.POD,
        method: _PODFLMCO_Input.URL.DriverList,
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

    var CS = $.extend({
        DITOMasterCode: 110, VehicleNo: 110, ETD: 120, ETA: 120, AETD: 120, AETA: 120,
        DriverID1: 150, TypeOfDriverID1: 100, DriverID2: 150, TypeOfDriverID2: 100, ButtonDriver: 50,
        SortOrder: 80, KmStart: 80, KmEnd: 80, TotalStationCost: 120, ButtonStationCost: 50,
        TotalTroubleCost: 120, ButtonTroubleCost: 50, Note1: 200, Note2: 200
    }, true, $rootScope.GetColumnSettings('PODInput_grid'));

    $scope.PODInput_gridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.POD,
            method: _PODFLMCO_Input.URL.Read,
            readparam: function () {
                return {
                    'dtFrom': $scope.PODInputItem.DateFrom,
                    'dtTo': $scope.PODInputItem.DateTo,
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
            pageSize: 10
        }),
        selectable: false, reorderable: false, editable: false,
        height: '100%', pageable: true, sortable: false, columnMenu: false, filterable: { mode: 'row' }, resizable: true,
        columns: [
            {
                title: ' ', width: '85px',field: 'F_Command',
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,PODInput_grid,PODInput_gridChooseChange)" />',
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,PODInput_grid,PODInput_gridChooseChange)" />' +
                    '<a href="/" ng-show="!dataItem.IsApproved" ng-click="PODInput_SaveClick($event,PODInput_grid)" class="k-button" data-title="Lưu"><i class="fa fa-check"></i></a>',
                filterable: false, sortable: false
            },
            { field: 'COTOMasterCode', width: CS['DITOMasterCode'], title: 'Mã chuyến', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'VehicleNo', width: CS['VehicleNo'], title: 'Số xe', filterable: { cell: { operator: 'contains', showOperators: false } } },
            {
                field: 'ETD', width: CS['ETD'], title: 'ETD', template: "#=ETD==null?' ':Common.Date.FromJsonDMYHM(ETD)#",
                filterable: {
                    cell: {
                        template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); },operator: 'equal', showOperators: false
                    }
                },
            },
            {
                field: 'ETA', width: CS['ETA'], title: 'ETA', template: "#=ETA==null?' ':Common.Date.FromJsonDMYHM(ETA)#",
                filterable: {
                    cell: {
                        template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); }, operator: 'equal', showOperators: false
                    }
                },
            },
            {
                field: 'AETD', width: CS['AETD'], title: 'AETD', template: "#=AETD==null?' ':Common.Date.FromJsonDMYHM(AETD)#",
                filterable: {
                    cell: {
                        template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); }, operator: 'equal', showOperators: false
                    }
                },
            },
            {
                field: 'AETA', width: CS['AETA'], title: 'AETA', template: "#=AETA==null?' ':Common.Date.FromJsonDMYHM(AETA)#",
                filterable: {
                    cell: {
                        template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); }, operator: 'equal', showOperators: false
                    }
                },
            },
            {
                field: 'DriverID1', width: CS['DriverID1'], title: 'Tài xế 1', filterable: false,
                template: '<input class="EvenEnter" focus-k-combobox kendo-combo-box k-options="DriverID_Options"  ng-model="dataItem.DriverID1" value="dataItem.DriverID1"  style="width: 100%"></input>',
            },
            {
                field: 'TypeOfDriverID1', width: CS['TypeOfDriverID1'], title: 'Loại Tài xế 1', filterable: false,
                template: '<input class="EvenEnter" focus-k-combobox kendo-combo-box k-options="TypeOfDriverID1_Options"  ng-model="dataItem.TypeOfDriverID1" value="dataItem.TypeOfDriverID1"  style="width: 100%"></input>',
            },
            {
                field: 'DriverID2', width: CS['DriverID2'], title: 'Tài xế 2', filterable: false,
                template: '<input class="EvenEnter" focus-k-combobox kendo-combo-box k-options="DriverID_Options"  ng-model="dataItem.DriverID2" value="dataItem.DriverID2"  style="width: 100%"></input>',
            },
            {
                field: 'TypeOfDriverID2', width: CS['TypeOfDriverID2'], title: 'Loại Tài xế 2', filterable: false,
                template: '<input class="EvenEnter" focus-k-combobox kendo-combo-box k-options="TypeOfDriverID2_Options"  ng-model="dataItem.TypeOfDriverID2" value="dataItem.TypeOfDriverID2"  style="width: 100%"></input>',
            },
            { field: 'ButtonDriver', width: CS['ButtonDriver'], title: 'Thông tin tài xế', template: '<a href="/" ng-click="DriverList_Click($event,dataItem,DriverList_win)" class="k-button" data-title="Hiển thị"><i class="fa fa-ellipsis-h"></i></a>', filterable: { cell: { operator: 'lte', showOperators: false } } },
            { field: 'SortOrder', width: CS['SortOrder'], title: 'Thứ tự chuyến', filterable: { cell: { operator: 'lte', showOperators: false } } },
            { field: 'KmStart', width: CS['KmStart'], title: 'Km đầu', filterable: { cell: { operator: 'lte', showOperators: false } } },
            { field: 'KmEnd', width: CS['KmEnd'], title: 'Km cuối', filterable: { cell: { operator: 'lte', showOperators: false } } },
            { field: 'TotalStationCost', width: CS['TotalStationCost'], title: 'Số tiền qua trạm', template: "#=TotalStationCost==null?' ':Common.Number.ToMoney(TotalStationCost)#", filterable: { cell: { operator: 'lte', showOperators: false } } },
            { field: 'ButtonStationCost', width: CS['ButtonStationCost'], title: 'Thông tin trạm', template: '<a href="/" ng-click="StationCost_Click($event,dataItem,StationCost_win)" class="k-button" data-title="Hiển thị"><i class="fa fa-bars"></i></a>', filterable: { cell: { operator: 'lte', showOperators: false } } },
            { field: 'TotalTroubleCost', width: CS['TotalTroubleCost'], title: 'Số tiền phát sinh', template: "#=TotalTroubleCost==null?' ':Common.Number.ToMoney(TotalTroubleCost)#", filterable: { cell: { operator: 'lte', showOperators: false } } },
            { field: 'ButtonTroubleCost', width: CS['ButtonTroubleCost'], title: 'Thông tin phát sinh', template: '<a href="/" ng-click="TroubleCost_Click($event,dataItem,TroubleCost_win)" class="k-button" data-title="Hiển thị"><i class="fa fa-bars"></i></a>', filterable: { cell: { operator: 'lte', showOperators: false } } },
            {
                field: 'Note1', width: CS['Note1'], title: 'Ghi chú 1', template: '<input type="text" class="k-textbox EvenEnter" style="width: 100%" ng-model="dataItem.Note1" />', filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'Note2', width: CS['Note2'], title: 'Ghi chú 2', template: '<input type="text" class="k-textbox EvenEnter" style="width: 100%" ng-model="dataItem.Note2" />', filterable: { cell: { operator: 'contains', showOperators: false } }
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
    $scope.TroubleCostHasChoose = false;
    $scope.TroubleCost_gridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.POD,
            method: _PODFLMCO_Input.URL.TroubleCost,
            readparam: function () {
                return {
                    COTOMasterID: $scope.COTOMasterID
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
            { field: 'GroupOfTroubleCode', width: '100px', title: 'Mã trạm', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'GroupOfTroubleName', width: '150px', title: 'Tên trạm', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'Cost', width: '150px', title: 'Chi phí', template: '<input type="text" class="k-textbox" style="width: 100%" ng-model="dataItem.Cost" />', filterable: { cell: { operator: 'equal', showOperators: false } } },
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
                url: Common.Services.url.POD,
                method: _PODFLMCO_Input.URL.TroubleCost_Delete,
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
            url: Common.Services.url.POD,
            method: _PODFLMCO_Input.URL.TroubleCostNotIn_List,
            readparam: function () {
                return {
                    COTOMasterID: $scope.COTOMasterID
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
                url: Common.Services.url.POD,
                method: _PODFLMCO_Input.URL.TroubleCostNotIn_Save,
                data: { COTOMasterID: $scope.COTOMasterID, lst: datasend },
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
            url: Common.Services.url.POD,
            method: _PODFLMCO_Input.URL.StationCost,
            readparam: function () {
                return {
                    COTOMasterID: $scope.COTOMasterID
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
            { field: 'LocationCode', width: '100px', title: 'Mã chuyến', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'LocationName', width: '150px', title: 'Tên chuyến', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'LocationAddress', width: '150px', title: 'Địa chỉ', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'Price', width: '150px', title: 'Giá', template: '<input type="text" class="k-textbox" style="width: 100%" ng-model="dataItem.Price" />', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { title: ' ', field: '', sortable: false, filterable: false, menu: false }
        ],
        dataBound: function (e) {
        }
    }

    $scope.PODInput_gridChooseChange = function ($event, grid, haschoose) {
        $scope.HasChoose = haschoose;
    };

    
    $scope.DriverList_Click = function ($event, data, win) {
        $event.preventDefault();
        $rootScope.IsLoading = true;
        Common.Services.Call($http, {
            url: Common.Services.url.POD,
            method: _PODFLMCO_Input.URL.GetDrivers,
            data: { COTOMasterID: data.ID },
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
            url: Common.Services.url.POD,
            method: _PODFLMCO_Input.URL.SaveDrivers,
            data: { COTOMasterID: $scope.dataDriver.COTOMasterID, item: $scope.dataDriver },
            success: function (res) {
                $scope.PODInput_gridOptions.dataSource.read();
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
        $scope.COTOMasterID = data.ID;
        $scope.StationCost_gridOptions.dataSource.read();
        $timeout(function () { $scope.StationCost_grid.resize(); }, 1);
        win.center().open();
    }

    $scope.TroubleCost_Click = function ($event, data, win) {
        $event.preventDefault();
        $scope.COTOMasterID = data.ID;
        $scope.TroubleCost_gridOptions.dataSource.read();
        $timeout(function () { $scope.TroubleCost_grid.resize(); }, 1);
        win.center().open();
    }

    $scope.TroubleCost_Save_Click = function ($event, win) {
        $event.preventDefault();
        $rootScope.IsLoading = true;
        Common.Services.Call($http, {
            url: Common.Services.url.POD,
            method: _PODFLMCO_Input.URL.TroubleCostSave,
            data: { lst: $scope.TroubleCost_gridOptions.dataSource.data() },
            success: function (res) {
                $rootScope.Message({ Msg: 'Lưu thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                $scope.PODInput_grid.dataSource.read();
                $rootScope.IsLoading = false;
                win.close();
            }
        });
    }

    $scope.StationCost_Save_Click = function ($event, win) {
        $event.preventDefault();
        $rootScope.IsLoading = true;

        Common.Services.Call($http, {
            url: Common.Services.url.POD,
            method: _PODFLMCO_Input.URL.StationCostSave,
            data: { lst: $scope.StationCost_gridOptions.dataSource.data() },
            success: function (res) {
                $rootScope.Message({ Msg: 'Lưu thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                $scope.PODInput_grid.dataSource.read();
                $rootScope.IsLoading = false;
                win.close();
            }
        });
    }

    $scope.PODInput_SaveClick = function ($event, grid) {
        $event.preventDefault();
        var tr = $($event.target).closest('tr');
        var item = grid.dataItem(tr);
        if (Common.HasValue(item)) {
            $scope.SaveDataInput(item);
        }
    }

    $scope.FLMDIInput_Approved_Click = function ($event, grid) {
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
                url: Common.Services.url.POD,
                method: _PODFLMCO_Input.URL.FLMCOInput_Approved,
                data: { lst: lstid },
                success: function (res) {
                    $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                    grid.dataSource.read();
                    $scope.HasChoose = false;
                    $rootScope.IsLoading = false;
                }
            });
        }
    }

    $scope.SaveDataInput = function (item) {
        $rootScope.IsLoading = true;
        if (item.DriverID1 == -1)
            item.TypeOfDriverID1 = -1;
        if (item.DriverID2 == -1)
            item.TypeOfDriverID2 = -1;

        Common.Services.Call($http, {
            url: Common.Services.url.POD,
            method: _PODFLMCO_Input.URL.Update,
            data: { item: item },
            success: function (res) {
                $rootScope.IsLoading = false;
                $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                $scope.PODInput_gridOptions.dataSource.read();
            },
            error: function (res) {
                $rootScope.IsLoading = false;
            }
        });
    };

    $scope.FLMCOInput_Approved_Click = function ($event, grid) {
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
                url: Common.Services.url.POD,
                method: _PODFLMCO_Input.URL.FLMCOInput_Approved,
                data: { lst: lstid },
                success: function (res) {
                    $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                    grid.dataSource.read();
                    $scope.HasChoose = false;
                    $rootScope.IsLoading = false;
                }
            });
        }
    }
    //#region Excel
    $scope.Excel_Click = function ($event, win) {
        $event.preventDefault();
        $rootScope.UploadExcel({
            Path: Common.FolderUpload.Import,
            columns: [
                { field: 'DITOMasterCode', title: 'Mã chuyến', width: 120 },
                { field: 'VehicleNo', title: 'Xe', width: 120 }
            ],
            Download: function () {
                $rootScope.IsLoading = true;
                Common.Services.Call($http, {
                    url: Common.Services.url.POD,
                    method: _PODFLMCO_Input.URL.ExcelExport,
                    data: {
                        'dtFrom': $scope.PODInputItem.DateFrom,
                        'dtTo': $scope.PODInputItem.DateTo,
                        'isCO': 1,
                    },
                    success: function (res) {
                        $rootScope.IsLoading = false;
                        $rootScope.DownloadFile(res);
                    },
                    error: function (res) {
                        $rootScope.IsLoading = false;
                    }
                })
            },
            Upload: function (e, callback) {
                $rootScope.IsLoading = true;
                Common.Services.Call($http, {
                    url: Common.Services.url.POD,
                    method: _PODFLMCO_Input.URL.ExcelCheck,
                    data: { file: e, 'dtFrom': $scope.PODInputItem.DateFrom, 'dtTo': $scope.PODInputItem.DateTo, 'isCO': 1, },
                    success: function (data) {
                        $rootScope.IsLoading = false;
                        callback(data);
                    }
                })
            },
            Window: win,
            Complete: function (e, data) {
                $rootScope.IsLoading = true;
                Common.Services.Call($http, {
                    url: Common.Services.url.POD,
                    method: _PODFLMCO_Input.URL.ExcelImport,
                    data: { lst: data, 'isCO': 1},
                    success: function (res) {
                        $rootScope.IsLoading = false;
                        $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                        $state.go('main.PODInput.FLMCO', {}, { reload: true });
                    },
                    error: function (res) {
                        $rootScope.IsLoading = false;
                    }
                })
            }
        })
    }
    //#endregion
    //actions
    $scope.ShowSetting = function ($event, grid) {
        $event.preventDefault();

        $rootScope.ShowSetting({
            ListView: views.PODInput,
            event: $event,
            grid: grid,
            current: $state.current
        });
    };

    $scope.Close_Click = function ($event, win) {
        $event.preventDefault();
        win.close();
    };

    $scope.HideSetting = function ($event) {
        $event.preventDefault();

        $rootScope.HideSetting();
    }
}]);