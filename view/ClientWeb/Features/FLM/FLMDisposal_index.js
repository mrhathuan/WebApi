
/// <reference path="~/Scripts/jquery-1.9.1.intellisense.js" />
/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />
/// <reference path="~/Scripts/angular.js" />

var _FLMDisposal = {
    URL: {
        Read: 'FLMDisposal_List',
        Save: 'FLMDisposal_Save',
        Delete: '',
        Get: 'FLMDisposal_Get',
        Get_Vehicle_List: 'FLMDisposal_Vehicle_List',
        Get_EQM_List: 'FLMDisposal_EQM_List',
        Get_EQM_ByVehicle: 'FLMDisposal_EQMByVehicle_List'
    },
    Data: {
        EQM: {}
    }
}

angular.module('myapp').controller('FLMDisposal_IndexCtrl', ['$rootScope', '$scope', '$http', '$location', '$state', '$timeout', function ($rootScope, $scope, $http, $location, $state, $timeout) {
    Common.Log('FLMDisposal_IndexCtrl');
    $rootScope.IsLoading = false;
    $scope.TotalValue = 0;
    $scope.IsEdit = false;
    $scope.IsDisableAtcVehicle = false;
    $scope.IsDisableAtcEQM = false;
    _FLMDisposal.Data.EQM = {};
    $scope.FLMDisposal_GridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.FLM,
            method: _FLMDisposal.URL.Read,
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number', editable: false, nullable: true },
                    DateReceipt: { type: 'date' }
                }
            }
        }),
        height: '100%', pageable: true, sortable: true, columnMenu: false, filterable: false, resizable: true, editable: { mode: 'inline' },
        columns: [
            {
                field: 'Code', title: 'Mã phiếu', width: '200px', filterable: { cell: { showOperators: false, operator: "contains" } },
                template: '<a href="/" ng-click="FLMDisposal_EditClick($event,FLMDisposal_win,FLMDisposal_Grid)" >#=Code#</a>'
            },
            {
                field: 'DateReceipt', title: 'Ngày phát sinh', width: '200px', template: "#=DateReceipt==null?\" \": Common.Date.FromJsonDMY(DateReceipt)#",
                filterable: { cell: { template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); }, operator: 'equal', showOperators: false } },
            },
            { field: 'Note', title: 'Ghi chú', width: '200px', filterable: { cell: { showOperators: false, operator: "contains" } } },
            { title: ' ', filterable: false, sortable: false }
        ]
    }
   
    $scope.FLMDisposal_AddNewClick = function ($event,win) {
        $event.preventDefault()
        $scope.LoadItem(0,win)
    }

    $scope.FLMDisposal_EditClick = function ($event,win,grid) {
        $event.preventDefault();
        var tr = $($event.target).closest('tr');
        var id = 0;
        var item = grid.dataItem(tr);
        if (Common.HasValue(item)) id = item.ID;
        $scope.LoadItem(id, win);
    }

    $scope.LoadItem = function (id, win) {
        $rootScope.IsLoading = true;
        Common.Services.Call($http, {
            url: Common.Services.url.FLM,
            method: _FLMDisposal.URL.Get,
            data: { 'id': id },
            success: function (res) {
                $scope.IsEdit = false;
                $rootScope.IsLoading = false;
                $scope.Item = res;
                $scope.IsDisableAtcVehicle = !res.IsVehicle;
                $scope.IsDisableAtcEQM = res.IsVehicle;
                if (id > 0) {
                    $scope.IsEdit = true;
                    if (res.IsVehicle) {
                        $scope.FLMDisposal_win_GridVehicle.dataSource.data([res.Vehicle])
                    }
                    else {
                        $scope.FLMDisposal_win_GridEQM.dataSource.data(res.lstEquipment)
                    }
                }
                win.center();
                win.open();
            },
            error: function (res) {
                $rootScope.IsLoading = false;
            }
        })
    }

    //#region popup
    $scope.FLMDisposal_winTabOptions = { animation: { open: { effects: "fadeIn" } } }

    $scope.FLMDisposal_win_CloseClick = function ($event,win) {
        $event.preventDefault();
        win.close()
    }

    $scope.atcVehicle_Options = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: "contains", suggest: true, dataTextField: 'RegNo', dataValueField: 'ID', placeholder: 'Chọn xe',
        dataSource: Common.DataSource.Local([], {
            id: 'ID',
            fields: {
                RegNo: { type: 'string' },
                ID: { type: 'number' },
            }
        }),
        select: function (e) {
            $rootScope.IsLoading = true;
            var item = this.dataItem(e.item.index());
            //debugger
            if(Common.HasValue(item)){
                Common.Services.Call($http, {
                    url: Common.Services.url.FLM,
                    method: _FLMDisposal.URL.Get_EQM_ByVehicle,
                    data: { id: item.VehicleID },
                    success: function (res) {
                        $rootScope.IsLoading = false;
                        item['lstEquipment'] = res.Data;
                       // $timeout(function () {
                            $scope.FLMDisposal_win_GridVehicle.dataSource.data([item]);
                       // },1)
                        
                        //reset atc vehicle
                    },
                    error: function (res) {
                        $rootScope.IsLoading = false;
                    }
                });
        }
        }
    }

    Common.Services.Call($http, {
        url: Common.Services.url.FLM,
        method: _FLMDisposal.URL.Get_Vehicle_List,
        data: {},
        success: function (res) {
            $scope.atcVehicle_Options.dataSource.data(res.Data)
        }
    });

    $scope.atcEQM_Options = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: "contains", suggest: true, dataTextField: 'Code', dataValueField: 'ID', placeholder: 'Chọn thiết bị',
        dataSource: Common.DataSource.Local([], {
            id: 'ID',
            fields: {
                Code: { type: 'string' },
                ID: { type: 'number' },
            }
        }),
        select: function (e) {
                var item = this.dataItem(e.item.index());
                if (Common.HasValue(item)) {
                    if (!Common.HasValue(_FLMDisposal.Data.EQM[item.ID])) {
                        _FLMDisposal.Data.EQM[item.ID] = 1;
                        $scope.FLMDisposal_win_GridEQM.dataSource.insert(0,item)
                    }
                    else {
                        $rootScope.Message({ Msg: 'Thiết bị đã được chọn trước đó', NotifyType: Common.Message.NotifyType.ERROR });
                    }
                }
        },
        change: function (e) {
            var atc = this;
            this.value('');
            this.focus();
        }
    }

    Common.Services.Call($http, {
        url: Common.Services.url.FLM,
        method: _FLMDisposal.URL.Get_EQM_List,
        data: {},
        success: function (res) {
            $scope.atcEQM_Options.dataSource.data(res.Data)
        }
    });

    $scope.FLMDisposal_win_GridVehicleOptions = {
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    CurrentValue: { type: 'number'},
                    WarrantyEnd: { type: 'date' },
                    WarrantyPeriod: { type: 'number' },
                }
            },
            pageSize: 0
        }),
        height: '99%', pageable: false, sortable: true, columnMenu: false, filterable: false, resizable: true, editable: false,
        toolbar: kendo.template($('#FLMDisposal_win_GridToolbar').html()),
        columns: [
            {
                title: ' ', width: '50px', filterable: false, sortable: false,
                template: '<a href="/" ng-click="FLMDisposal_win_GridVehicle_EditClick($event,FLMDisposal_win_GridVehicle)" ng-show="!IsEdit" class="k-button"><i class="fa fa-trash"></i></a>'
            },
            { field: 'RegNo', title: 'Số xe', width: '100px', },
            { field: 'Manufactor', title: 'Nhà sản xuất', width: '100px', },
            { field: 'YearOfProduction', title: 'Năm sản xuất', width: '100px', },
            { field: 'WarrantyEnd', title: 'Ngày kết thúc B.H', width: '150px', template: '#=WarrantyEnd==null?" ":Common.Date.FromJsonDDMMYY(WarrantyEnd)#' },
            { field: 'WarrantyPeriod', title: 'Thời gian B.H', width: '100px', },
            { field: 'CurrentValue', title: 'Giá trị hiện tại', width: '150px', template: '#=CurrentValue==null?" ":Common.Number.ToMoney(CurrentValue)#' },
            { title: ' ', filterable: false, sortable: false }
        ],
        dataBound: function (e) {
            var grid = this;
            var totalVal = 0;
            Common.Data.Each(grid.dataSource.data(), function (o) {
                totalVal += o.CurrentValue;
                if (Common.HasValue(o.lstEquipment)) {
                    Common.Data.Each(o.lstEquipment, function (eqm) {
                        totalVal += eqm.CurrentValue;
                    })
                }
            })
            $timeout(function () {
                $scope.TotalValue = totalVal;
            },1)
        }
    }

    $scope.FLMDisposal_win_GridVehicleDetailOptions = function (data) {
        if (!Common.HasValue(data.lstEquipment)) data.lstEquipment = [];
        return {
            dataSource: Common.DataSource.Local({
                data: data.lstEquipment,
                model: {
                    id: 'ID',
                    fields: {
                        ID: { type: 'number' },
                        WarrantyEnd: { type: 'date' },
                        ID: { type: 'number' },
                        WarrantyPeriod: { type: 'number' },
                        CurrentValue: { type: 'number' },
                    }
                },
                pageSize: 0
            }),
            height: '100%', pageable: false, sortable: true, columnMenu: false, filterable: false, resizable: true, editable: false,
            columns: [
                { field: 'Code', title: 'Mã thiết bị', width: '100px', },
                { field: 'Manufactor', title: 'Nhà sản xuất', width: '100px', },
                { field: 'YearOfProduction', title: 'Năm sản xuất', width: '100px', },
                { field: 'WarrantyEnd', title: 'Ngày kết thúc B.H', width: '100px', template: '#=WarrantyEnd==null?" ":Common.Date.FromJsonDDMMYY(WarrantyEnd)#' },
                { field: 'WarrantyPeriod', title: 'Thời gian B.H', width: '100px', },
                { field: 'CurrentValue', title: 'Giá trị hiện tại', width: '150px', template: '#=CurrentValue==null?" ":Common.Number.ToMoney(CurrentValue)#' },
                { title: ' ', filterable: false, sortable: false }
            ]
        }
    }

    $scope.FLMDisposal_win_GridEQMOptions = {
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    CurrentValue: { type: 'number' },
                    WarrantyEnd: { type: 'date' },
                    WarrantyPeriod: { type: 'number' },
                }
            },
            pageSize: 0
        }),
        height: '99%', pageable: false, sortable: true, columnMenu: false, filterable: false, resizable: true, editable: false,
        toolbar: kendo.template($('#FLMDisposal_win_GridToolbar').html()),
        columns: [
            {
                title: ' ', width: '50px', filterable: false, sortable: false,
                template: '<a href="/" ng-click="FLMDisposal_win_GridEQM_EditClick($event,FLMDisposal_win_GridEQM)" ng-show="!IsEdit" class="k-button"><i class="fa fa-trash></i></a>'
            },
            { field: 'Code', title: 'Mã thiết bị', width: '100px', },
                { field: 'Manufactor', title: 'Nhà sản xuất', width: '100px', },
                { field: 'YearOfProduction', title: 'Năm sản xuất', width: '100px', },
                { field: 'WarrantyEnd', title: 'Ngày kết thúc B.H', width: '100px', template: '#=WarrantyEnd==null?" ":Common.Date.FromJsonDDMMYY(WarrantyEnd)#' },
                { field: 'WarrantyPeriod', title: 'Thời gian B.H', width: '100px', },
                { field: 'CurrentValue', title: 'Giá trị hiện tại', width: '150px', template: '#=CurrentValue==null?" ":Common.Number.ToMoney(CurrentValue)#' },
                { field: 'RegNo', title: 'Số xe', width: '100px', hidden: true, groupHeaderTemplate: "Vị trí hiện tại: #=value==''?'Tự do':value#", },
                { title: ' ', filterable: false, sortable: false }
        ],
        dataBound: function (e) {
            var grid = this;
            var totalValue = 0;
            Common.Data.Each(grid.dataSource.data(), function (o) {
                totalValue += o.CurrentValue;
            })
            $timeout(function () {
                $scope.TotalValue = totalValue;
            },1)
        }
    }

    $scope.FLMDisposal_win_GridEQM_EditClick = function ($event,grid) {
        $event.preventDefault();
        var tr = $($event.target).closest('tr');
        var item = grid.dataItem(tr);
        if (Common.HasValue(item)) grid.dataSource.remove(item)
        else $rootScope.Message({ Msg: 'Không tìm thấy dữ liệu', NotifyType: Common.Message.NotifyType.ERROR });
    }

    $scope.numTotal_Options={ format: 'n0', spinners: false, culture: 'en-US', min: 0, step: 1, decimals: 0, }

    $scope.FLMDisposal_win_SaveClick = function ($event,win,vform) {
        $event.preventDefault();
        if (vform()) {
            if (Common.HasValue($scope.Item.DateReceipt)) {
                //debugger
                if ($scope.Item.ID < 1) {
                    if ($scope.Item.IsVehicle) {
                        var data = $scope.FLMDisposal_win_GridVehicle.dataSource.data();
                        if (data.length > 0) $scope.Item.Vehicle = data[0];
                    } else
                        $scope.Item.lstEquipment = $scope.FLMDisposal_win_GridEQM.dataSource.data();
                }

                Common.Services.Call($http, {
                    url: Common.Services.url.FLM,
                    method: _FLMDisposal.URL.Save,
                    data: {item:$scope.Item},
                    success: function (res) {
                        $rootScope.IsLoading = false;
                        $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                        $scope.FLMDisposal_GridOptions.dataSource.read();
                        win.close()
                    },
                    error: function (res) {
                        $rootScope.IsLoading = false;
                    }
                });

            }
            else 
                $rootScope.Message({ Msg: 'Chưa chọn ngày lập phiếu', NotifyType: Common.Message.NotifyType.ERROR });
        }
    }

    $scope.FLMDisposal_win_ChangeTypeDisposal = function ($event,isVehicle) {
        switch (isVehicle) {
            default:
                break;
            case true:
                $scope.FLMDisposal_win_GridVehicle.dataSource.data([]);
                $scope.IsDisableAtcEQM = true;
                $scope.IsDisableAtcVehicle = false;
                break;
            case false:
                _FLMDisposal.Data.EQM = {};
                $scope.FLMDisposal_win_GridEQM.dataSource.data([]);
                $scope.IsDisableAtcEQM = false;
                $scope.IsDisableAtcVehicle = true;
                $timeout(function () {
                    $scope.FLMDisposal_win_GridEQM.refresh()
                },1)
                break;
        }
    }

    //#endregion

    //#region Action
    $scope.ShowSetting = function ($event, grid) {
        $event.preventDefault();

        $rootScope.ShowSetting({
            ListView: [],
            event: $event,
            grid: grid,
            current: $state.current
        });
    };

    $scope.HideSetting = function ($event) {
        $event.preventDefault();

        $rootScope.HideSetting();
    }
    //#endregion

}])

