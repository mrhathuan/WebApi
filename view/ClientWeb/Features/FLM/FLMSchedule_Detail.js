
/// <reference path="~/Scripts/jquery-1.9.1.intellisense.js" />
/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />

var _FLMSchedule_Detail = {
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

angular.module('myapp').controller('FLMSchedule_DetailCtrl', ['$rootScope', '$scope', '$http', '$location', '$state', '$timeout', function ($rootScope, $scope, $http, $location, $state, $timeout) {
    Common.Log('FLMSchedule_DetailCtrl');
    $rootScope.IsLoading = false;

    //#region Driver
    $scope.Item = null;
    $scope.DriverNotInHasChoose = false;
    $scope.TabIndex = 0;

    _FLMSchedule_Detail.Param = $.extend(true, _FLMSchedule_Detail.Params, $state.params);

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

    $scope.InitView_Schedule = function () {
        Common.Log("InitView_Schedule");
        $rootScope.IsLoading = true;
        Common.Services.Call($http, {
            url: Common.Services.url.FLM,
            method: _FLMSchedule_Detail.URL.Detail_Data,
            data: { scheduleID: _FLMSchedule_Detail.Params.ScheduleID },
            success: function (res) {
                _FLMSchedule_Detail.Data._dataDetail = res.ListDetail;
                _FLMSchedule_Detail.Data._dataDate = res.ListDate;
                _FLMSchedule_Detail.Data._dataDriver = res.ListDriver;

                var Model = {
                    id: 'ID',
                    fields: {
                        EmployeeCode: { type: 'string', editable: false },
                        DriverName: { type: 'string', editable: false },
                        ID: { type: 'number', editable: false },
                    }
                }
                var GridColumn = [
                    { field: 'EmployeeCode', title: "Mã tài xế", width: 120, locked: true, filterable: { cell: { operator: 'contains', showOperators: false } } },
                    { field: 'DriverName', title: "Tên tài xế", width: 150, locked: true, filterable: { cell: { operator: 'contains', showOperators: false } } }
                ]

                Common.Data.Each(res.ListDate, function (date) {
                    var listCol = [];
                    var field = "L" + date.ID;
                    Model.fields[field] = { type: "boolean", editable: true };
                    var _date = new Date(date.Date);
                    var cl = "";
                    if (date.TypeDate == 2) {
                        cl = "holiday";
                    } else if (date.TypeDate == 3) {
                        cl = "absence";
                    }
                    var title = "<div style='text-align:center' class='"+cl+"'>" + date.DateName + "<br/>" + _date.getDate() + '/' + (_date.getMonth() + 1) + "</div>";
                    //var title = "#=Date==null?' ':Common.Date.FromJsonDMY(Date)#";
                    GridColumn.push({
                        field: field, title: title, width: 45, locked: false,filterable:false,
                        template: "<div style='text-align: center;'><input type='checkbox' ng-model='dataItem."+field+"'></input></div>",
                    });
                })

                GridColumn.push({ title: ' ', filterable: false, sortable: false })
                $scope.schedule_grid.setOptions({
                    dataSource: Common.DataSource.Local({
                        data: [],
                        model: Model,
                        pageSize: 20,
                    }),
                    height: '100%', pageable: true, sortable: true, columnMenu: false, resizable: true, reorderable: false, filterable: { mode: 'row', visible: false }, editable: false,
                    columns: GridColumn
                })

                //tao data source
                var dataCheck = {};
                Common.Data.Each(res.ListDetail, function (o) {
                    if (!Common.HasValue(dataCheck["R" + o.DriverID + "_L" + o.ScheduleDateID]))
                        dataCheck["R" + o.DriverID + "_L" + o.ScheduleDateID] = o;
                })
                var dataGrid = [];
                
                Common.Data.Each(res.ListDriver, function (driver) {
                    var item = {};
                    item["EmployeeCode"] = driver.EmployeeCode;
                    item["DriverName"] = driver.DriverName;
                    item["ID"] = driver.ID;
                    Common.Data.Each(res.ListDate, function (date) {
                        var field = "L" + date.ID;
                        item[field] = false;
                        if (Common.HasValue(dataCheck["R" + driver.ID + "_L" + date.ID])) {
                            item[field] = true;
                        }
                    });
                    dataGrid.push(item);
                })

                $scope.schedule_grid.dataSource.data(dataGrid)
                $rootScope.IsLoading = false;
            },
            error: function (res) {
                $rootScope.IsLoading = false;
            }
        });
    }


    $scope.InitView_Schedule();

    $scope.schedule_Save_Click = function ($event, grid) {
        $event.preventDefault(); 
        var data = grid.dataSource.data();
        var dataSave = [];
        Common.Data.Each(data, function (row) {
            Common.Data.Each(_FLMSchedule_Detail.Data._dataDate, function (date) {
                
                if (row["L" + date.ID] == true) {
                    
                    dataSave.push({
                        ScheduleDateID: date.ID,
                        DriverID: row.ID,
                        IsChecked: row["L" + date.ID],
                    })
                }
            })
        })
        $rootScope.IsLoading = true;
        Common.Services.Call($http, {
            url: Common.Services.url.FLM,
            method: _FLMSchedule_Detail.URL.Detail_Save,
            data: { lst: dataSave, scheduleID: _FLMSchedule_Detail.Params.ScheduleID },
            success: function (res) {
                $scope.InitView_Schedule();
                $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
            },
            error: function (res) {
                $rootScope.IsLoading = false;
            }
        });
    }
    //#region driver

    $scope.driver_Click = function ($event , win) {
        $event.preventDefault();
        win.center().open();
        $scope.driverGrid_Options.dataSource.read();
    }

    $scope.driverGrid_Options = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.FLM,
            method: _FLMSchedule_Detail.URL.Driver_List,
            readparam: function () { return { scheduleID: _FLMSchedule_Detail.Params.ScheduleID } },
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number', editable: false, nullable: true },
                    IsAssistant: { type: 'string', },
                    IsChoose: { type: 'boolean' },
                    DaysAllowoff: { type: 'number' },
                    DateStart: { type: 'date' },
                }
            }
        }),
        height: '100%', pageable: true, sortable: true, columnMenu: false, filterable: { mode: 'row' }, resizable: true, editable: false,
        columns: [
            {
                title: ' ', width: '90px',
                template: '<a href="/" ng-click="driver_GridEdit_Click($event,driverDetail_win,dataItem,driverDetail_vform)" class="k-button"><i class="fa fa-pencil"></i></a>' +
                    '<a href="/" ng-click="driver_GridDestroy_Click($event,dataItem)" class="k-button"><i class="fa fa-trash"></i></a>',
                filterable: false, sortable: false
            },
            { field: 'DriverCode', title: "Mã tài xế", width: 120, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'DriverName', title: "Tên tài xế", width: 120, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'CardNumber', title: "CMND", width: 120, filterable: { cell: { operator: 'contains', showOperators: false } } },
            {
                field: "IsAssistant", title: 'Phụ lái', width: 85, attributes: { style: "text-align: center;" },
                template: '<input type="checkbox" #= IsAssistant=="true" ? "checked=checked" : "" # disabled="disabled" />',
                filterable: {
                    cell: {
                        template: function (container) {
                            container.element.kendoComboBox({
                                dataSource: [{ Text: 'Phụ lái', Value: true }, { Text: 'Khác phụ lái', Value: false }, { Text: 'Tất cả', Value: '' }],
                                dataTextField: "Text", dataValueField: "Value"
                            });
                        },
                        showOperators: false
                    }
                }
            },
            { field: 'FeeBase', title: "Lương căn bản", width: 120, filterable: { cell: { operator: 'equal', showOperators: false } } },
            {
                field: 'DateStart', width: "120px", title: 'Ngày vào làm', template: "#=DateStart==null?' ':Common.Date.FromJsonDMY(DateStart)#",
                filterable: { cell: { template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); }, operator: 'gte', showOperators: false } },
            },
            { field: 'AssetNo', title: "Số xe", width: 150, filterable: { cell: { operator: 'contains', showOperators: false } } },
            {
                field: 'DaysAllowoff', width: "120px", title: 'Số ngày nghỉ phép', template: "#=DaysAllowoff==null?' ':Common.Number.ToNumber1(DaysAllowoff)#",
                filterable: { cell: { operator: 'lte', showOperators: false } },
            },
            { title: ' ', filterable: false, sortable: false }
        ]
    }

    $scope.driver_AddNew = function ($event, win) {
        $event.preventDefault();
        win.center().open();
        $scope.driverNotIn_GridOptions.dataSource.read();
    }

    $scope.driverNotIn_GridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.FLM,
            method: _FLMSchedule_Detail.URL.Driver_NotInList,
            readparam: function () { return { scheduleID: _FLMSchedule_Detail.Params.ScheduleID } },
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
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,driverNotIn_Grid,driverNotIn_GridChoose_Change)" />',
                headerAttributes: { style: 'text-align: center;' }, attributes: { style: 'text-align: center;' },
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,driverNotIn_Grid,driverNotIn_GridChoose_Change)" />',
                filterable: false, sortable: false
            },
            { field: 'EmployeeCode', title: "Mã tài xế", width: 120, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'LastName', title: "Họ", width: 150, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'FirstName', title: "Tên", width: 150, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'CardNumber', title: "CMND", width: 150, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'Cellphone', title: "Số điện thoại", width: 150, filterable: { cell: { operator: 'contains', showOperators: false } } },
            
            { title: ' ', filterable: false, sortable: false }
        ]
    }
    $scope.driverNotIn_GridChoose_Change = function ($event, grid, hasChoose) {
        $scope.DriverNotInHasChoose = hasChoose;
    }

    $scope.driverNotIn_Save_Click = function ($event, win, grid) {
        $event.preventDefault();
        var data = grid.dataSource.data();
        var datasend = [];
        Common.Data.Each(data, function (o) {
            if (o.IsChoose) datasend.push(o.ID);
        })
        if (datasend.length > 0) {
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.FLM,
                method: _FLMSchedule_Detail.URL.Driver_NotInSave,
                data: { scheduleID: _FLMSchedule_Detail.Params.ScheduleID, lst: datasend },
                success: function (res) {
                    $scope.driverGrid_Options.dataSource.read();
                    $scope.InitView_Schedule();
                    win.close();
                    $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                },
                error: function (res) {
                    $rootScope.IsLoading = false;
                }
            });

        }
    }

    $scope.driver_GridDestroy_Click = function ($event, data) {
        $event.preventDefault();
        $rootScope.IsLoading = true;
        Common.Services.Call($http, {
            url: Common.Services.url.FLM,
            method: _FLMSchedule_Detail.URL.Driver_Delete,
            data: { id: data.id },
            success: function (res) {
                $scope.driverGrid_Options.dataSource.read();
                $scope.InitView_Schedule();
                $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
            },
            error: function (res) {
                $rootScope.IsLoading = false;
            }
        });
    }

    $scope.driver_GridEdit_Click = function ($event, win, data, vform) {
        $event.preventDefault();
        vform({ clear: true });
        $rootScope.IsLoading = true;

        Common.Services.Call($http, {
            url: Common.Services.url.FLM,
            method: _FLMSchedule_Detail.URL.Driver_Get,
            data: { id: data.ID },
            success: function (res) {
                $rootScope.IsLoading = false;
                $scope.ItemDriver = res;
                win.center().open();
            },
            error: function (res) {
                $rootScope.IsLoading = false;
            }
        });
    }

    $scope.driverDetail_Save_Click = function ($event, win, vform) {
        $event.preventDefault();
        if (vform()) {
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.FLM,
                method: _FLMSchedule_Detail.URL.Driver_Save,
                data: { item: $scope.ItemDriver },
                success: function (res) {
                    $scope.driverGrid_Options.dataSource.read();
                    $scope.InitView_Schedule();
                    win.close();
                },
                error: function (res) {
                    $rootScope.IsLoading = false;
                }
            });
        }

    }



    $scope.numFeeBase_options = { format: 'n0', spinners: false, culture: 'en-US', min: 0, step: 1, decimals: 0, }
    $scope.numDaysFee_options = { format: 'n0', spinners: false, culture: 'en-US', min: 0, step: 1, decimals: 0, }
    $scope.numDaysAllowOff_options = { format: 'n0', spinners: false, culture: 'en-US', min: 0, step: 1, decimals: 0, }
    $scope.cboDriverAsset_Options = {
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
        method: _FLMSchedule_Detail.URL.AssetFee_AsestList,
        data: {},
        success: function (res) {
            var data = [];
            data.push({ ID: -1, RegNo: ' ' });
            Common.Data.Each(res.Data, function (o) {
                data.push(o);
            })
            $scope.cboDriverAsset_Options.dataSource.data(data);
        }
    });
    //#endregion

    //#region date
    $scope.dateGrid_Options = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.FLM,
            method: _FLMSchedule_Detail.URL.Date_List,
            readparam: function () { return { scheduleID: _FLMSchedule_Detail.Params.ScheduleID }; },
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number', editable: false, },
                    DateName: { type: 'string' },
                    Date: { type: 'date' },
                    TypeScheduleDateName: { type: 'string' },
                }
            }
        }),
        selectable: false, reorderable: true, editable: false,
        height: '100%', pageable: true, sortable: false, columnMenu: false, filterable: { mode: 'row' }, resizable: true,
        columns: [
            {
                title: ' ', width: '45px',
                template: '<a href="/" ng-click="date_GridEdit_Click($event,date_win,dataItem,date_vform)" class="k-button"><i class="fa fa-pencil"></i></a>',
                filterable: false, sortable: false
            },
            {
                field: 'DateName', title: 'Thứ', width: 120,
                filterable: { cell: { operator: 'contains', showOperators: false }, editable: false, }
            },
            {
                field: 'Date', title: 'Ngày', template: "#=Date==null?' ':Common.Date.FromJsonDMY(Date)#", width: 120,
                filterable: { cell: { template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); }, operator: 'gte', showOperators: false } },
            },
            {
                field: 'TypeScheduleDateName', title: 'Loại ngày',width: 120,
                filterable: { cell: { operator: 'contains', showOperators: false }, editable: false, }
            },
            { title: ' ', filterable: false, sortable: false }
        ]
    }

    $scope.date_GridEdit_Click = function ($event, win, data, vform) {
        $event.preventDefault();
        $scope.LoadItemDate(data.id, win, vform);
    }

    $scope.LoadItemDate = function (id, win, vform) {
        vform({ clear: true });
        $rootScope.IsLoading = true;
        Common.Services.Call($http, {
            url: Common.Services.url.FLM,
            method: _FLMSchedule_Detail.URL.Date_Get,
            data: { 'id': id },
            success: function (res) {
                $rootScope.IsLoading = false;
                $scope.ItemDate = res;
                
                win.center();
                win.open();
            },
            error: function (res) {
                $rootScope.IsLoading = false;
            }
        })
    }

    $scope.date_Save_Click = function ($event, win, vform) {
        $event.preventDefault();
        if (vform()) {
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.FLM,
                method: _FLMSchedule_Detail.URL.Date_Save,
                data: { item: $scope.ItemDate },
                success: function (res) {
                    $rootScope.IsLoading = false;
                    $scope.dateGrid_Options.dataSource.read();
                    $scope.InitView_Schedule();
                    win.close();
                },
                error: function (res) {
                    $rootScope.IsLoading = false;
                }
            });
        }
    }

    $scope.cboTypeScheduleDate_Options = {
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
        change: function (e) {
        }
    }

    Common.Services.Call($http, {
        url: Common.Services.url.CAT,
        method: _FLMSchedule_Detail.URL.TypeScheduleDate,
        data: {},
        success: function (res) {
            if (Common.HasValue(res)) {
                $scope.cboTypeScheduleDate_Options.dataSource.data(res.Data);
            }
        }
    });

    //#endregion

    //#region driverFee
    //$scope.driverFeeGrid_Options = {
    //    dataSource: Common.DataSource.Grid($http, {
    //        url: Common.Services.url.FLM,
    //        method: _FLMSchedule_Detail.URL.DriverFee_List,
    //        readparam: function () { return { scheduleID: _FLMSchedule_Detail.Params.ScheduleID }; },
    //        model: {
    //            id: 'ID',
    //            fields: {
    //                ID: { type: 'number', editable: false, },
    //            }
    //        }
    //    }),
    //    selectable: false, reorderable: true, editable: false,
    //    height: '100%', pageable: true, sortable: false, columnMenu: false, filterable: { mode: 'row' }, resizable: true,
    //    columns: [
    //        {
    //            title: ' ', width: '90px',
    //            template: '<a href="/" ng-click="driverFee_GridEdit_Click($event,driverFee_win,dataItem,driverFee_vform)" class="k-button"><i class="fa fa-pencil"></i></a>' +
    //                        '<a href="/" ng-click="driverFee_GridDestroy_Click($event,dataItem)" class="k-button"><i class="fa fa-trash"></i></a>',
    //            filterable: false, sortable: false
    //        },
    //        {
    //            field: 'DriverName', title: 'Tài xế', width: 150,
    //            filterable: { cell: { operator: 'contains', showOperators: false }, editable: false, }
    //        },
    //        {
    //            field: 'TypeOfScheduleFeeCode', title: 'Mã loại tính phí', width: 120,
    //            filterable: { cell: { operator: 'contains', showOperators: false }, editable: false, }
    //        },
    //        {
    //            field: 'TypeOfScheduleFeeName', title: 'Loại tính phí', width: 120,
    //            filterable: { cell: { operator: 'contains', showOperators: false }, editable: false, }
    //        },
    //        {
    //            field: 'Price', title: 'Giá', width: 120, template: "#=Price==null?'':Common.Number.ToMoney(Price)#",
    //            filterable: { cell: { operator: 'contains', showOperators: false }, editable: false, }
    //        },
    //        { title: ' ', filterable: false, sortable: false }
    //    ]
    //}

    //$scope.driverFee_AddNew = function ($event, win, vform) {
    //    $event.preventDefault();
    //    $scope.LoadItemDriverFee(0, win, vform);
    //}

    //$scope.driverFee_GridEdit_Click = function ($event, win, data, vform) {
    //    $event.preventDefault();
    //    $scope.LoadItemDriverFee(data.id, win, vform);
    //}

    //$scope.LoadItemDriverFee = function (id, win, vform) {
    //    vform({ clear: true });
    //    $scope.LoadDriver();
    //    $rootScope.IsLoading = true;
    //    Common.Services.Call($http, {
    //        url: Common.Services.url.FLM,
    //        method: _FLMSchedule_Detail.URL.DriverFee_Get,
    //        data: { 'id': id, scheduleID: _FLMSchedule_Detail.Param.ScheduleID },
    //        success: function (res) {
    //            $rootScope.IsLoading = false;
    //            $scope.ItemDriverFee = res;

    //            win.center();
    //            win.open();
    //        },
    //        error: function (res) {
    //            $rootScope.IsLoading = false;
    //        }
    //    })
    //}

    //$scope.driverFee_Save_Click = function ($event, win, vform) {
    //    $event.preventDefault();
    //    if (vform()) {
    //        $rootScope.IsLoading = true;
    //        Common.Services.Call($http, {
    //            url: Common.Services.url.FLM,
    //            method: _FLMSchedule_Detail.URL.DriverFee_Save,
    //            data: { item: $scope.ItemDriverFee, scheduleID: _FLMSchedule_Detail.Param.ScheduleID },
    //            success: function (res) {
    //                $rootScope.IsLoading = false;
    //                $scope.driverFeeGrid_Options.dataSource.read();
    //                win.close();
    //            },
    //            error: function (res) {
    //                $rootScope.IsLoading = false;
    //            }
    //        });
    //    }
    //}

    //$scope.cboDriver_Options = {
    //    autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true, dataTextField: 'DriverName', dataValueField: 'DriverID',
    //    dataSource: Common.DataSource.Local({
    //        data: [],
    //        model: {
    //            id: 'DriverID',
    //            fields: {
    //                DriverID: { type: 'number' },
    //                DriverName: { type: 'string' },
    //            }
    //        }
    //    }),
    //    change: function (e) {
    //    }
    //}

    //$scope.LoadDriver = function(){
    //    Common.Services.Call($http, {
    //        url: Common.Services.url.FLM,
    //        method: _FLMSchedule_Detail.URL.DriverFee_DriverList,
    //        data: { scheduleID: _FLMSchedule_Detail.Params.ScheduleID },
    //        success: function (res) {
    //            if (Common.HasValue(res)) {
    //                $scope.cboDriver_Options.dataSource.data(res.Data);
    //            }
    //        }
    //    });
    //}

    //$scope.cboTypeOfScheduleFee_Options = {
    //    autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true, dataTextField: 'TypeName', dataValueField: 'ID',
    //    dataSource: Common.DataSource.Local({
    //        data: [],
    //        model: {
    //            id: 'ID',
    //            fields: {
    //                ID: { type: 'number' },
    //                TypeName: { type: 'string' },
    //            }
    //        }
    //    }),
    //    change: function (e) {
    //    }
    //}

    //Common.Services.Call($http, {
    //    url: Common.Services.url.CAT,
    //    method: _FLMSchedule_Detail.URL.TypeOfScheduleFee,
    //    data: {},
    //    success: function (res) {
    //        if (Common.HasValue(res)) {
                
    //            $scope.cboTypeOfScheduleFee_Options.dataSource.data(res.Data);
    //        }
    //    }
    //});

    //$scope.driverFee_GridDestroy_Click = function ($event, data) {
    //    $event.preventDefault();
    //    $rootScope.Message({
    //        Type: Common.Message.Type.Confirm,
    //        NotifyType: Common.Message.NotifyType.SUCCESS,
    //        Title: 'Thông báo',
    //        Msg: 'Bạn muốn xóa dữ liệu đã chọn?',
    //        Close: null,
    //        Ok: function () {
    //            Common.Services.Call($http, {
    //                url: Common.Services.url.FLM,
    //                method: _FLMSchedule_Detail.URL.DriverFee_Delete,
    //                data: { 'id': data.ID },
    //                success: function (res) {
    //                    $rootScope.IsLoading = false;
    //                    $scope.driverFeeGrid_Options.dataSource.read();
    //                },
    //                error: function (res) {
    //                    $rootScope.IsLoading = false;
    //                }
    //            })
    //        }
    //    })
    //}
    //#endregion

    //#region assetFee
    //$scope.assetFeeGrid_Options = {
    //    dataSource: Common.DataSource.Grid($http, {
    //        url: Common.Services.url.FLM,
    //        method: _FLMSchedule_Detail.URL.AssetFee_List,
    //        readparam: function () { return { scheduleID: _FLMSchedule_Detail.Params.ScheduleID }; },
    //        model: {
    //            id: 'ID',
    //            fields: {
    //                ID: { type: 'number', editable: false, },
    //            }
    //        }
    //    }),
    //    selectable: false, reorderable: true, editable: false,
    //    height: '100%', pageable: true, sortable: false, columnMenu: false, filterable: { mode: 'row' }, resizable: true,
    //    columns: [
    //        {
    //            title: ' ', width: '90px',
    //            template: '<a href="/" ng-click="assetFee_GridEdit_Click($event,assetFee_win,dataItem,assetFee_vform)" class="k-button"><i class="fa fa-pencil"></i></a>' +
    //                '<a href="/" ng-click="assetFee_GridDestroy_Click($event,dataItem)" class="k-button"><i class="fa fa-trash"></i></a>',
    //            filterable: false, sortable: false
    //        },
    //        {
    //            field: 'AssetNo', title: 'Xe', width: 150,
    //            filterable: { cell: { operator: 'contains', showOperators: false }, editable: false, }
    //        },
    //        {
    //            field: 'TypeOfScheduleFeeCode', title: 'Mã loại tính phí', width: 120,
    //            filterable: { cell: { operator: 'contains', showOperators: false }, editable: false, }
    //        },
    //        {
    //            field: 'TypeOfScheduleFeeName', title: 'Loại tính phí', width: 120,
    //            filterable: { cell: { operator: 'contains', showOperators: false }, editable: false, }
    //        },
    //        {
    //            field: 'Price', title: 'Giá', width: 120, template: "#=Price==null?'':Common.Number.ToMoney(Price)#",
    //            filterable: { cell: { operator: 'contains', showOperators: false }, editable: false, }
    //        },
    //        { title: ' ', filterable: false, sortable: false }
    //    ]
    //}

    //$scope.assetFee_AddNew = function ($event, win, vform) {
    //    $event.preventDefault();
    //    $scope.LoadItemAssetFee(0, win, vform);
    //}

    //$scope.assetFee_GridEdit_Click = function ($event, win, data, vform) {
    //    $event.preventDefault();
    //    $scope.LoadItemAssetFee(data.id, win, vform);
    //}

    //$scope.LoadItemAssetFee = function (id, win, vform) {
    //    vform({ clear: true });
    //    $rootScope.IsLoading = true;
    //    Common.Services.Call($http, {
    //        url: Common.Services.url.FLM,
    //        method: _FLMSchedule_Detail.URL.AssetFee_Get,
    //        data: { 'id': id, scheduleID: _FLMSchedule_Detail.Param.ScheduleID },
    //        success: function (res) {
    //            $rootScope.IsLoading = false;
    //            $scope.ItemAssetFee = res;

    //            win.center();
    //            win.open();
    //        },
    //        error: function (res) {
    //            $rootScope.IsLoading = false;
    //        }
    //    })
    //}

    //$scope.assetFee_Save_Click = function ($event, win, vform) {
    //    $event.preventDefault();
    //    if (vform()) {
    //        $rootScope.IsLoading = true;
    //        Common.Services.Call($http, {
    //            url: Common.Services.url.FLM,
    //            method: _FLMSchedule_Detail.URL.AssetFee_Save,
    //            data: { item: $scope.ItemAssetFee, scheduleID: _FLMSchedule_Detail.Param.ScheduleID },
    //            success: function (res) {
    //                $rootScope.IsLoading = false;
    //                $scope.assetFeeGrid_Options.dataSource.read();
    //                win.close();
    //            },
    //            error: function (res) {
    //                $rootScope.IsLoading = false;
    //            }
    //        });
    //    }
    //}

    //$scope.cboAsset_Options = {
    //    autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true, dataTextField: 'RegNo', dataValueField: 'ID',
    //    dataSource: Common.DataSource.Local({
    //        data: [],
    //        model: {
    //            id: 'ID',
    //            fields: {
    //                ID: { type: 'number' },
    //                RegNo: { type: 'string' },
    //            }
    //        }
    //    }),
    //    change: function (e) {
    //    }
    //}

    //Common.Services.Call($http, {
    //    url: Common.Services.url.FLM,
    //    method: _FLMSchedule_Detail.URL.AssetFee_AsestList,
    //    data: {},
    //    success: function (res) {
                
    //        $scope.cboAsset_Options.dataSource.data(res.Data);
    //        var data = [];
    //        data.push({ ID: -1, RegNo: ' ' });
    //        Common.Data.Each(res.Data, function (o) {
    //            data.push(o);
    //        })
    //        $scope.cboDriverAsset_Options.dataSource.data(data);
    //    }
    //});

    //$scope.assetFee_GridDestroy_Click = function ($event, data) {
    //    $event.preventDefault();
    //    $rootScope.Message({
    //        Type: Common.Message.Type.Confirm,
    //        NotifyType: Common.Message.NotifyType.SUCCESS,
    //        Title: 'Thông báo',
    //        Msg: 'Bạn muốn xóa dữ liệu đã chọn?',
    //        Close: null,
    //        Ok: function () {
    //            Common.Services.Call($http, {
    //                url: Common.Services.url.FLM,
    //                method: _FLMSchedule_Detail.URL.AssetFee_Delete,
    //                data: { 'id': data.ID },
    //                success: function (res) {
    //                    $rootScope.IsLoading = false;
    //                    $scope.assetFeeGrid_Options.dataSource.read();
    //                },
    //                error: function (res) {
    //                    $rootScope.IsLoading = false;
    //                }
    //            })
    //        }
    //    })
    //}
    //#endregion

    //#region assistantFee
    //$scope.assistantFee_GridOptions = {
    //    dataSource: Common.DataSource.Grid($http, {
    //        url: Common.Services.url.FLM,
    //        method: _FLMSchedule_Detail.URL.AssistantFee_List,
    //        readparam: function () { return { scheduleID: _FLMSchedule_Detail.Params.ScheduleID }; },
    //        model: {
    //            id: 'ID',
    //            fields: {
    //                ID: { type: 'number', editable: false, },
    //                IsAssistant:{type:"boolean"},
    //            }
    //        }
    //    }),
    //    selectable: false, reorderable: true, editable: false,
    //    height: '100%', pageable: true, sortable: false, columnMenu: false, filterable: { mode: 'row' }, resizable: true,
    //    columns: [
    //        {
    //            title: ' ', width: '90px',
    //            template: '<a href="/" ng-click="assistantFee_GridEdit_Click($event,assistantFee_win,dataItem,assistantFee_vform)" class="k-button"><i class="fa fa-pencil"></i></a>' +
    //                '<a href="/" ng-click="assistantFee_GridDestroy_Click($event,dataItem)" class="k-button"><i class="fa fa-trash"></i></a>',
    //            filterable: false, sortable: false
    //        },
    //        {
    //            field: 'TypeOfScheduleFeeCode', title: 'Mã loại tính phí', width: 120,
    //            filterable: { cell: { operator: 'contains', showOperators: false }, editable: false, }
    //        },
    //        {
    //            field: 'TypeOfScheduleFeeName', title: 'Loại tính phí', width: 120,
    //            filterable: { cell: { operator: 'contains', showOperators: false }, editable: false, }
    //        },
    //        {
    //            field: 'Price', title: 'Giá', width: 120, template: "#=Price==null?'':Common.Number.ToMoney(Price)#",
    //            filterable: { cell: { operator: 'contains', showOperators: false }, editable: false, }
    //        },
    //        {
    //            field: 'IsAssistant', title: 'Lái xe', width: 120, template: '<input type="checkbox" ng-model="dataItem.IsAssistant" disabled="disabled"/>',
    //            filterable: {
    //                cell: {
    //                    template: function (container) {
    //                        container.element.kendoComboBox({
    //                            dataSource: [{ Text: 'Phụ xe', Value: false }, { Text: 'Lái xe', Value: true }, { Text: 'Tất cả', Value: '' }],
    //                            dataTextField: "Text", dataValueField: "Value"
    //                        });
    //                    },
    //                    showOperators: false
    //                }
    //            }
    //        },
    //        { title: ' ', filterable: false, sortable: false }
    //    ]
    //}

    //$scope.assistantFee_AddNew = function ($event, win, vform) {
    //    $event.preventDefault();
    //    $scope.LoadItemAssistantFee(0, win, vform);
    //}

    //$scope.assetFee_GridEdit_Click = function ($event, win, data, vform) {
    //    $event.preventDefault();
    //    $scope.LoadItemAssistantFee(data.id, win, vform);
    //}

    //$scope.LoadItemAssistantFee = function (id, win, vform) {
    //    vform({ clear: true });
    //    $rootScope.IsLoading = true;
    //    Common.Services.Call($http, {
    //        url: Common.Services.url.FLM,
    //        method: _FLMSchedule_Detail.URL.AssistantFee_Get,
    //        data: { 'id': id, scheduleID: _FLMSchedule_Detail.Param.ScheduleID },
    //        success: function (res) {
    //            $rootScope.IsLoading = false;
    //            $scope.ItemAssistantFee = res;

    //            win.center();
    //            win.open();
    //        },
    //        error: function (res) {
    //            $rootScope.IsLoading = false;
    //        }
    //    })
    //}

    //$scope.assistantFee_Save_Click = function ($event, win, vform) {
    //    $event.preventDefault();
    //    if (vform()) {
    //        $rootScope.IsLoading = true;
    //        Common.Services.Call($http, {
    //            url: Common.Services.url.FLM,
    //            method: _FLMSchedule_Detail.URL.AssistantFee_Save,
    //            data: { item: $scope.ItemAssistantFee, scheduleID: _FLMSchedule_Detail.Param.ScheduleID },
    //            success: function (res) {
    //                $rootScope.IsLoading = false;
    //                $scope.assistantFee_GridOptions.dataSource.read();
    //                win.close();
    //            },
    //            error: function (res) {
    //                $rootScope.IsLoading = false;
    //            }
    //        });
    //    }
    //}

    //$scope.assistantFee_GridDestroy_Click = function ($event, data) {
    //    $event.preventDefault();
    //    $rootScope.Message({
    //        Type: Common.Message.Type.Confirm,
    //        NotifyType: Common.Message.NotifyType.SUCCESS,
    //        Title: 'Thông báo',
    //        Msg: 'Bạn muốn xóa dữ liệu đã chọn?',
    //        Close: null,
    //        Ok: function () {
    //            Common.Services.Call($http, {
    //                url: Common.Services.url.FLM,
    //                method: _FLMSchedule_Detail.URL.AssistantFee_Delete,
    //                data: { 'id': data.ID },
    //                success: function (res) {
    //                    $rootScope.IsLoading = false;
    //                    $scope.assetFeeGrid_Options.dataSource.read();
    //                },
    //                error: function (res) {
    //                    $rootScope.IsLoading = false;
    //                }
    //            })
    //        }
    //    })
    //}

    //$scope.cboAssistantTypeOfScheduleFee_Options = {
    //    autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true, dataTextField: 'TypeName', dataValueField: 'ID',
    //    dataSource: Common.DataSource.Local({
    //        data: [],
    //        model: {
    //            id: 'ID',
    //            fields: {
    //                ID: { type: 'number' },
    //                TypeName: { type: 'string' },
    //            }
    //        }
    //    }),
    //    change: function (e) {
    //    }
    //}

    //Common.Services.Call($http, {
    //    url: Common.Services.url.CAT,
    //    method: _FLMSchedule_Detail.URL.TypeOfScheduleFee,
    //    data: {},
    //    success: function (res) {
    //        if (Common.HasValue(res)) {

    //            $scope.cboAssistantTypeOfScheduleFee_Options.dataSource.data(res.Data);
    //        }
    //    }
    //});

    //$scope.numAssistantDay_options = { format: 'n0', spinners: false, culture: 'en-US', min: 0, step: 1, decimals: 0, }
    //$scope.numAssistantPrice_options = { format: 'n0', spinners: false, culture: 'en-US', min: 0, step: 1, decimals: 0, }
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
                    method: _FLMSchedule_Detail.URL.ScheduleExcel_Export,
                    data: { scheduleID: _FLMSchedule_Detail.Param.ScheduleID },
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
                    method: _FLMSchedule_Detail.URL.ScheduleExcel_Check,
                    data: { file: e, scheduleID: _FLMSchedule_Detail.Param.ScheduleID },
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
                    method: _FLMSchedule_Detail.URL.ScheduleExcel_Save,
                    data: { lst: data, scheduleID: _FLMSchedule_Detail.Param.ScheduleID },
                    success: function (res) {
                        $scope.InitView_Schedule();
                    }
                })
            }
        })
    }

    //#region DriverFee Excel
    //$scope.driverFee_Excel_Click = function ($event, win) {
    //    $event.preventDefault();
    //    $rootScope.UploadExcel({
    //        Path: Common.FolderUpload.Import,
    //        columns: [
    //            { field: 'DriverCode', title: 'Mã tài xế', width: '150px', filterable: { cell: { showOperators: false, operator: "contains" } } },
    //            { field: 'DriverName', title: 'Tên tài xế', width: '150px', filterable: { cell: { showOperators: false, operator: "contains" } } },
    //            { field: 'TypeOfScheduleFeeName', title: 'Loại tính phí', width: '150px', filterable: { cell: { showOperators: false, operator: "contains" } } }
    //        ],
    //        Download: function () {
    //            $rootScope.IsLoading = true;
    //            Common.Services.Call($http, {
    //                url: Common.Services.url.FLM,
    //                method: _FLMSchedule_Detail.URL.DriverFeeExcel_Export,
    //                data: { scheduleID: _FLMSchedule_Detail.Param.ScheduleID },
    //                success: function (res) {
    //                    $rootScope.IsLoading = false;
    //                    $rootScope.DownloadFile(res);
    //                }
    //            })
    //        },
    //        Upload: function (e, callback) {
    //            $rootScope.IsLoading = true;
    //            Common.Services.Call($http, {
    //                url: Common.Services.url.FLM,
    //                method: _FLMSchedule_Detail.URL.DriverFeeExcel_Check,
    //                data: { file: e, scheduleID: _FLMSchedule_Detail.Param.ScheduleID },
    //                success: function (data) {
    //                    callback(data);
    //                    $rootScope.IsLoading = false;
    //                }
    //            })
    //        },
    //        Window: win,
    //        Complete: function (e, data) {
    //            $rootScope.IsLoading = true;
    //            Common.Services.Call($http, {
    //                url: Common.Services.url.FLM,
    //                method: _FLMSchedule_Detail.URL.DriverFeeExcel_Save,
    //                data: { lst: data, scheduleID: _FLMSchedule_Detail.Param.ScheduleID },
    //                success: function (res) {
    //                    $scope.driverFeeGrid_Options.dataSource.read();
    //                    $rootScope.IsLoading = false;
    //                }
    //            })
    //        }
    //    })
    //}
    //#endregion

    //#region AssetFee Excel
    //$scope.assetFee_Excel_Click = function ($event, win) {
    //    $event.preventDefault();
    //    $rootScope.UploadExcel({
    //        Path: Common.FolderUpload.Import,
    //        columns: [
    //            { field: 'AssetNo', title: 'Mã tài xế', width: '150px', filterable: { cell: { showOperators: false, operator: "contains" } } },
    //            { field: 'TypeOfScheduleFeeName', title: 'Loại tính phí', width: '150px', filterable: { cell: { showOperators: false, operator: "contains" } } }
    //        ],
    //        Download: function () {
    //            $rootScope.IsLoading = true;
    //            Common.Services.Call($http, {
    //                url: Common.Services.url.FLM,
    //                method: _FLMSchedule_Detail.URL.AssetFeeExcel_Export,
    //                data: { scheduleID: _FLMSchedule_Detail.Param.ScheduleID },
    //                success: function (res) {
    //                    $rootScope.IsLoading = false;
    //                    $rootScope.DownloadFile(res);
    //                }
    //            })
    //        },
    //        Upload: function (e, callback) {
    //            $rootScope.IsLoading = true;
    //            Common.Services.Call($http, {
    //                url: Common.Services.url.FLM,
    //                method: _FLMSchedule_Detail.URL.AssetFeeExcel_Check,
    //                data: { file: e, scheduleID: _FLMSchedule_Detail.Param.ScheduleID },
    //                success: function (data) {
    //                    callback(data);
    //                    $rootScope.IsLoading = false;
    //                }
    //            })
    //        },
    //        Window: win,
    //        Complete: function (e, data) {
    //            $rootScope.IsLoading = true;
    //            Common.Services.Call($http, {
    //                url: Common.Services.url.FLM,
    //                method: _FLMSchedule_Detail.URL.AssetFeeExcel_Save,
    //                data: { lst: data, scheduleID: _FLMSchedule_Detail.Param.ScheduleID },
    //                success: function (res) {
    //                    $scope.assetFeeGrid_Options.dataSource.read();
    //                    $rootScope.IsLoading = false;
    //                }
    //            })
    //        }
    //    })
    //}
    //#endregion
    $scope.WinClose_Click = function ($event, win) {
        $event.preventDefault();
        win.close();
    }

    //$scope.Calculate_Click = function ($event) {
    //    $event.preventDefault();
    //    $rootScope.IsLoading = true;
    //    Common.Services.Call($http, {
    //        url: Common.Services.url.FLM,
    //        method: _FLMSchedule_Detail.URL.CaculateFee,
    //        data: { scheduleID: _FLMSchedule_Detail.Param.ScheduleID },
    //        success: function (res) {
    //            $rootScope.IsLoading = false;
    //                $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
    //        },
    //        error: function (e) {
    //            $rootScope.IsLoading = false;
    //        }
    //    });
    //};

    $scope.driver_Update_Click = function ($event, grid) {
        $event.preventDefault();
        $rootScope.IsLoading = true;
        Common.Services.Call($http, {
            url: Common.Services.url.FLM,
            method: _FLMSchedule_Detail.URL.UpdateInfo,
            data: { scheduleID: _FLMSchedule_Detail.Param.ScheduleID },
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

    $scope.Close_Click = function ($event) {
        $event.preventDefault();

        $state.go("main.FLMSchedule.Index");
    };
}]);

