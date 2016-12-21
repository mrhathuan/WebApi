
/// <reference path="~/Scripts/jquery-1.9.1.intellisense.js" />
/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />

var _FLMOwner_Index = {
    URL: {
        SearchDate: 'FLMOwner_MasterList',
        MasterDetail: 'FLMOwner_MasterDetailList',
        AcceptReceipt_Save: 'FLMOwner_AcceptReceipt',
        GenerateReceipt_List: 'FLMOwner_GenerateReceipt',
    },
    Data: {
        listColumn: [],
        _listColumnKey: [],
        _listData: [],
    }
    
}


angular.module('myapp').controller('FLMOwner_IndexCtrl', ['$rootScope', '$scope', '$http', '$location', '$state', '$timeout', function ($rootScope, $scope, $http, $location, $state, $timeout) {
    Common.Log('FLMOwner_IndexCtrl');
    $rootScope.IsLoading = false;


    $scope.HasChoose = false;
    $scope.Item = {
        DateFrom: null,
        DateTo: null,
        VehicleID : 0,
    }
    $scope.arrayColumn = ['MaterialCode'];
    $scope.arrayColumnRS = ['Định mức'];
    $scope.arrayColumnType = [0, 1,];
    $scope.fieldGrid = [];

    $scope.DateData = function () {
        $rootScope.IsLoading = true;
        Common.Services.Call($http, {
            url: Common.Services.url.FLM,
            method: _FLMOwner_Index.URL.SearchDate,
            data: { dtfrom: $scope.Item.DateFrom, dtto: $scope.Item.DateTo },
            success: function (res) {
                _FLMOwner_Index.Data._listData = res.ListAsset;
                _FLMOwner_Index.Data._listColumn = res.ListQuota;
                _FLMOwner_Index.Data._listColumnKey = res.ListKey;
                $scope.InitGrid();
            },
            error: function (res) {
                $rootScope.IsLoading = false;
            }
        });
    }

    $scope.DateData();

    $scope.SettingDate_Click = function ($event, win, vform) {
        $event.preventDefault();
        if (vform()) {
            $scope.DateData();
            win.close();
        }
    };

    $scope.FLMOwner_Date_Click = function ($event, win, vform) {
        $event.preventDefault();
        vform({ clear: true });
        win.center().open();
    }

    $scope.window_Close_Click = function ($event, win) {
        $event.preventDefault();
        win.close();
    }
    $scope.InitGrid = function () {
        Common.Log("InitGrid");

        var Model = {
            id: 'ID',
            fields: {
                ID: { type: 'number', editable: false },
                IsChoose: { type: 'boolean', editable:false },
                VehicleID: { type: 'number', editable: false },
                RegNo: { type: 'string', editable: false },
                TypeOfAssetName: { type: 'string', editable: false },
                RegWeight: { type: 'number', editable: false },
                MaxWeight: { type: 'number', editable: false },
                MinWeight: { type: 'number', editable: false },
                RegCapacity: { type: 'number', editable: false },
                MaxCapacity: { type: 'number', editable: false },
                MinCapacity: { type: 'number', editable: false },
                GroupOfVehicleCode: { type: 'string', editable: false },
                GroupOfVehicleName: { type: 'string', editable: false },
                KMStart: { type: 'number', editable: false },
                KMEnd: { type: 'number', editable: false },
                TotalMaster: { type: 'number', editable: false },
                
            }
        }
        var GridColumn = [
           {
                title: ' ', width: '40px',
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAllFLM_Check($event,FLMOwner_grid,FLMOwner_gridChooseChange)" />',
                headerAttributes: { style: 'text-align: center;' },
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,FLMOwner_grid,FLMOwner_gridChooseChange)"  ng-disabled="dataItem.TotalMaster <= 0"/>',
                templateAttributes: { style: 'text-align: center;' },
                filterable: false, sortable: false
            },
           {
               field: "RegNo", title: 'Số xe', width: 100,
               filterable: { cell: { showOperators: false, operator: "contains" } }
           },
           {
               field: "TypeOfAssetName", title: 'Loại xe', width: 100,
               filterable: { cell: { showOperators: false, operator: "contains" } }
           },
           {
               field: "RegWeight", title: 'Trọng tải đăng kí', width: 120, template: '#=RegWeight==null?" ":Common.Number.ToNumber3(RegWeight)#',
               filterable: { cell: { showOperators: false, operator: "eq" } }
           },
           {
               field: "MaxWeight", title: 'Trọng tải tối đa', width: 120, template: '#=MaxWeight==null?" ":Common.Number.ToNumber3(MaxWeight)#',
               filterable: { cell: { showOperators: false, operator: "eq" } }
           },
           {
               field: "MinWeight", title: 'Trọng tải tối thiểu', width: 120, template: '#=MinWeight==null?" ":Common.Number.ToNumber3(MinWeight)#',
               filterable: { cell: { showOperators: false, operator: "eq" } }
           },
           {
               field: "RegCapacity", title: 'Số khối đăng ký', width: 120, template: '#=RegCapacity==null?" ":Common.Number.ToNumber3(RegCapacity)#',
               filterable: { cell: { showOperators: false, operator: "eq" } }
           },
           {
               field: "MaxCapacity", title: 'Số khối tối đa', width: 120, template: '#=MaxCapacity==null?" ":Common.Number.ToNumber3(MaxCapacity)#',
               filterable: { cell: { showOperators: false, operator: "eq" } }
           },
           {
               field: "MinCapacity", title: 'Số khối tối thiểu', width: 120, template: '#=MinCapacity==null?" ":Common.Number.ToNumber3(MinCapacity)#',
               filterable: { cell: { showOperators: false, operator: "eq" } }
           },
           {
               field: 'GroupOfVehicleCode', title: 'Mã loại', width: 100, headerAttributes: { style: "text-align: center;" },
               filterable: { cell: { showOperators: false, operator: "contains" } }
           },
           {
               field: 'GroupOfVehicleName', title: 'Phân loại', width: 100, headerAttributes: { style: "text-align: center;" },
               filterable: { cell: { showOperators: false, operator: "contains" } }
           },
           {
               field: 'KMStart', title: 'KM bắt đầu', width: 100, headerAttributes: { style: "text-align: center;" }, template: '#=KMStart==null?" ":Common.Number.ToNumber3(KMStart)#',
               filterable: { cell: { showOperators: false, operator: "contains" } }
           },
           {
               field: 'KMEnd', title: 'KM kết thúc', width: 100, headerAttributes: { style: "text-align: center;" }, template: '#=KMEnd==null?" ":Common.Number.ToNumber3(KMEnd)#',
               filterable: { cell: { showOperators: false, operator: "contains" } }
           },
           {
               field: 'TotalMaster', title: 'Tổng số chuyến', width: 100, headerAttributes: { style: "text-align: center;" },
               template: '<a href="\" ng-click="FLMOwner_Edit_Click($event, dataItem, Setting_win)" >#=TotalMaster#</a>',
               filterable: { cell: { showOperators: false, operator: "contains" } }
           },
        ]
        $scope.fieldGrid = [];

        Common.Data.Each(_FLMOwner_Index.Data._listColumnKey, function (group) {
            var array = [];
            for (var i = 0 ; i < $scope.arrayColumn.length ; i++) {
                var field = $scope.arrayColumn[i] + group.MaterialCode;
                field = field.replace(/-/g, '');
                field = field.replace(/[()]/g, '_');
                field = field.replace(/ /g, '_');
                array.push(field);
                switch ($scope.arrayColumnType[i]) {
                    case 0:
                        Model.fields[field] = {
                            type: "string",
                        };
                        GridColumn.push({
                            field: field, title: '<b>' + group.MaterialCode + "-" + $scope.arrayColumnRS[i] + '</b><br>' + '[' + group.MaterialCode + '-' + $scope.arrayColumn[i] + ']', width: 120, locked: false,
                            filterable: { cell: { operator: 'contains', showOperators: false } }
                        });
                        break;
                    case 1:
                        Model.fields[field] = {
                            type: "number",
                        };
                        GridColumn.push({
                            field: field, title: '<b>' + group.MaterialCode + "-" + $scope.arrayColumnRS[i] + '</b><br>' + '[' + group.MaterialCode + '-' + $scope.arrayColumn[i] + ']', width: 120, locked: false,
                            filterable: { cell: { operator: 'gte', showOperators: false } },
                        });
                        break;
                    default:
                        GridColumn.push({
                            field: field, title: '<b>' + group.MaterialCode + "-" + $scope.arrayColumnRS[i] + '</b><br>' + '[' + group.MaterialCode + '-' + $scope.arrayColumn[i] + ']', width: 120, locked: false,
                            filterable: false,
                        });
                        break;
                }
            }
            $scope.fieldGrid.push(array);
        })

        GridColumn.push({ title: ' ', filterable: false, sortable: false })
        $scope.FLMOwner_grid.setOptions({
            dataSource: Common.DataSource.Local({
                data: [],
                model: Model,
                pageSize: 20,
            }),
            height: '100%', pageable: true, sortable: true, columnMenu: false, resizable: true, reorderable: false, filterable: { mode: 'row' }, editable: false, pageSize: 20,
            columns: GridColumn
        })

        var dataGrid = [];
        Common.Data.Each(_FLMOwner_Index.Data._listData, function (data) {
            var count = 0;
            Common.Data.Each(_FLMOwner_Index.Data._listColumnKey, function (key) {

                var dataGroup = null;
                var code = key.QuantityPerKM;
                Common.Data.Each(_FLMOwner_Index.Data._listColumn, function (group) {
                    if (group.QuantityPerKM == code) {
                        dataGroup = group;
                    }
                });

                if (dataGroup != null) {
                    data[$scope.fieldGrid[count][0]] = (!Common.HasValue(dataGroup.QuantityPerKM) || dataGroup.QuantityPerKM == "") ? " " : Common.Number.ToMoney(dataGroup.QuantityPerKM);
                  
                } else {
                    for (var i = 0 ; i < $scope.fieldGrid[count].length ; i++) {
                        data[$scope.fieldGrid[count][i]] = '';
                    }
                }
                count++;
            });
            data.IsChoose = false;
            dataGrid.push(data);
        })
        $scope.FLMOwner_grid.dataSource.data(dataGrid);
        $rootScope.IsLoading = false;
    }
    
    $scope.FLMOwner_Edit_Click = function ($event,data, win) {
        $event.preventDefault();
        if (data.TotalMaster > 0) {
            $scope.SettingReport_Tab.select(0);
            win.center().open();
        }
    };
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
    $scope.FLMOwner_gridChooseChange = function ($event, grid, haschoose) {
        $scope.HasChoose = haschoose;
    };
    $scope.gridChooseAllFLM_Check = function ($event, grid, callback) {
        
        if ($event.target.checked == true) {
            grid.items().each(function () {

                var tr = this;
                var item = grid.dataItem(tr);
                if (item.TotalMaster > 0) {
                    if (item.IsChoose != true) {
                        $(tr).find('td input.chkChoose').prop('checked', true);
                        item.IsChoose = true;

                        if (!$(tr).hasClass('IsChoose'))
                            $(tr).addClass('IsChoose');
                    }
                }
            });
        }
        else {
            grid.items().each(function () {
                var tr = this;
                var item = grid.dataItem(tr);
                if (item.IsChoose == true) {
                    $(tr).find('td input.chkChoose').prop('checked', false);
                    item.IsChoose = false;
                    if ($(tr).hasClass('IsChoose'))
                        $(tr).removeClass('IsChoose');
                }
            });
        }

        if (Common.HasValue(callback)) {
            callback($event, grid, $event.target.checked);
        }
    };
    

    //#region Truck
    $scope.Setting_Truck_GridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.FLM,
            method: _FLMOwner_Index.URL.MasterDetail,
            readparam: function () { return { vehicleID: $scope.Item.VehicleID, dtfrom: $scope.Item.DateFrom, dtto: $scope.Item.DateTo } },
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
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,Setting_Truck_Grid,Truck_GridChoose_Change)" />',
                headerAttributes: { style: 'text-align: center;' }, attributes: { style: 'text-align: center;' },
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,Setting_Truck_Grid,Truck_GridChoose_Change)" />',
                filterable: false, sortable: false
            },
            {
                field: 'Code', title: 'Số xe', width: '150px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'LocationName', title: 'Mã tài xế 1', width: '150px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'CountryName', title: 'Tên tài xế 1', width: '175px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'ProvinceName', title: 'Mã tài xế 2', width: '175px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'DistrictName', title: 'Tên tài xế 2',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'KM', title: 'KM', width: '120px', template: '#=KM==null?" ":Common.Number.ToNumber3(KM)#',
                filterable: { cell: { operator: 'gte', showOperators: false } }
            },
            {
                field: 'ETD', title: 'ETD', width: '150px', template: '#=Common.Date.FromJsonDDMMYY(ETD)#',
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
                field: 'ETA', title: 'ETA', width: '150px', template: '#=Common.Date.FromJsonDDMMYY(ETA)#',
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
                field: 'ATD', title: 'ATD', width: '150px', template: '#=Common.Date.FromJsonDDMMYY(ATD)#',
                filterable: {
                    cell: {
                        template: function (e) {
                            var element = e.element.parent();
                            element.empty();
                            $("<input class='dtp-filter-from' data-field='ATD' style='width:50%; float:left;' />").appendTo(element);
                            $("<input class='dtp-filter-to' data-field='ATD' style='width:50%; float:left;' />").appendTo(element);
                        }, showOperators: false
                    }
                },
            },
            {
                field: 'ATA', title: 'ATA', width: '150px', template: '#=Common.Date.FromJsonDDMMYY(ATA)#',
                filterable: {
                    cell: {
                        template: function (e) {
                            var element = e.element.parent();
                            element.empty();
                            $("<input class='dtp-filter-from' data-field='ATA' style='width:50%; float:left;' />").appendTo(element);
                            $("<input class='dtp-filter-to' data-field='ATA' style='width:50%; float:left;' />").appendTo(element);
                        }, showOperators: false
                    }
                },
            },
            {
                field: 'TextCustomerCode', title: 'TextCustomerCode', width: '175px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'TextCustomerName', title: 'TextCustomerName', width: '175px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'TextGroupLocationCode', title: 'TextGroupLocationCode', width: '175px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            { title: ' ', sortable: false, filterable: false, menu: false }
        ]
    }
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
                    if (Common.HasValue($scope.SettingReport_TabOptions.dataSource.filter())) {
                        filters = $scope.SettingReport_TabOptions.dataSource.filter().filters;
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
                    $scope.SettingReport_TabOptions.dataSource.filter(filters);
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
                    if (Common.HasValue($scope.SettingReport_TabOptions.dataSource.filter())) {
                        filters = $scope.SettingReport_TabOptions.dataSource.filter().filters;
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
                    $scope.SettingReport_TabOptions.dataSource.filter(filters);
                }
                catch (e) {
                    $rootScope.Message({ Msg: 'Sai dữ liệu!' });
                }
            }
        })
    }, 500);

    $scope.TruckHasChoose = false;
    $scope.Truck_GridChoose_Change = function ($event, grid, hasChoose) {
        $scope.TruckHasChoose = hasChoose;
    }
    //#endregion

    //#region Cont
    $scope.Setting_Cont_GridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.FLM,
            method: _FLMOwner_Index.URL.MasterDetail,
            readparam: function () { return { vehicleID: $scope.Item.VehicleID, dtfrom: $scope.Item.DateFrom, dtto: $scope.Item.DateTo } },
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
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,Setting_Cont_Grid,Truck_GridChoose_Change)" />',
                headerAttributes: { style: 'text-align: center;' }, attributes: { style: 'text-align: center;' },
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,Setting_Cont_Grid,Truck_GridChoose_Change)" />',
                filterable: false, sortable: false
            },

            {
                field: 'Code', title: 'Số xe', width: '150px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'LocationName', title: 'Mã tài xế 1', width: '150px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'CountryName', title: 'Tên tài xế 1', width: '175px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'ProvinceName', title: 'Mã tài xế 2', width: '175px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'DistrictName', title: 'Tên tài xế 2',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'KM', title: 'KM', width: '120px', template: '#=KM==null?" ":Common.Number.ToNumber3(KM)#',
                filterable: { cell: { operator: 'gte', showOperators: false } }
            },
            {
                field: 'ETD', title: 'ETD', width: '150px', template: '#=Common.Date.FromJsonDDMMYY(ETD)#',
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
                field: 'ETA', title: 'ETA', width: '150px', template: '#=Common.Date.FromJsonDDMMYY(ETA)#',
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
                field: 'ATD', title: 'ATD', width: '150px', template: '#=Common.Date.FromJsonDDMMYY(ATD)#',
                filterable: {
                    cell: {
                        template: function (e) {
                            var element = e.element.parent();
                            element.empty();
                            $("<input class='dtp-filter-from' data-field='ATD' style='width:50%; float:left;' />").appendTo(element);
                            $("<input class='dtp-filter-to' data-field='ATD' style='width:50%; float:left;' />").appendTo(element);
                        }, showOperators: false
                    }
                },
            },
            {
                field: 'ATA', title: 'ATA', width: '150px', template: '#=Common.Date.FromJsonDDMMYY(ATA)#',
                filterable: {
                    cell: {
                        template: function (e) {
                            var element = e.element.parent();
                            element.empty();
                            $("<input class='dtp-filter-from' data-field='ATA' style='width:50%; float:left;' />").appendTo(element);
                            $("<input class='dtp-filter-to' data-field='ATA' style='width:50%; float:left;' />").appendTo(element);
                        }, showOperators: false
                    }
                },
            },
            {
                field: 'TextCustomerCode', title: 'TextCustomerCode', width: '175px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'TextCustomerName', title: 'TextCustomerName', width: '175px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'TextGroupLocationCode', title: 'TextGroupLocationCode', width: '175px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            { title: ' ', sortable: false, filterable: false, menu: false }
        ]
    }
    $scope.ContHasChoose = false;
    $scope.Cont_GridChoose_Change = function ($event, grid, hasChoose) {
        $scope.ContHasChoose = hasChoose;
    }
    //#endregion


    //#region Lập phiếu nhiên liệu
    $scope.FLMOwner_Check_Click = function ($event, win) {
        $event.preventDefault();
        win.center().open();
        $scope.FLMOwnerRefresh_GridOptions.dataSource.read();
    }
    $scope.FLMOwnerRefresh_GridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.FLM,
            method: _FLMOwner_Index.URL.GenerateReceipt_List,
            readparam: function () { return { lst: $scope.Item.lst } },
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
                template: '<a href="/" ng-click="FLMOwner_Document_Click($event,dataItem, FLMOwnerMaterial_win)" class="k-button"><i class="fa fa-file-text"></i></a>',
                filterable: false, sortable: false
            },
            { field: 'RegNo', title: "Số xe", width: 150, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'Code', title: "Mã phiếu", width: 150, filterable: { cell: { operator: 'contains', showOperators: false } } },
            {
                field: 'DateReceipt', title: 'Ngày lập phiếu', width: '150px', template: '#=Common.Date.FromJsonDDMMYY(DateReceipt)#',
                filterable: {
                    cell: {
                        template: function (e) {
                            var element = e.element.parent();
                            element.empty();
                            $("<input class='dtp-filter-from' data-field='DateReceipt' style='width:50%; float:left;' />").appendTo(element);
                            $("<input class='dtp-filter-to' data-field='DateReceipt' style='width:50%; float:left;' />").appendTo(element);
                        }, showOperators: false
                    }
                },
            },
            {
                 field: 'InvoiceDate', title: 'Ngày lập chứng từ', width: '150px', template: '#=Common.Date.FromJsonDDMMYY(InvoiceDate)#',
                 filterable: {
                     cell: {
                         template: function (e) {
                             var element = e.element.parent();
                             element.empty();
                             $("<input class='dtp-filter-from' data-field='InvoiceDate' style='width:50%; float:left;' />").appendTo(element);
                             $("<input class='dtp-filter-to' data-field='InvoiceDate' style='width:50%; float:left;' />").appendTo(element);
                         }, showOperators: false
                     }
                 },
             },
             { field: 'InvoiceNo', title: "Số chứng từ", width: 150, filterable: { cell: { operator: 'contains', showOperators: false } } },
             { field: 'KMStart', title: "KM bắt đầu", width: 150, filterable: { cell: { operator: 'gte', showOperators: false } } },
             { field: 'KMEnd', title: "KM kết thúc", width: 150, filterable: { cell: { operator: 'gte', showOperators: false } } },
             { field: 'TotalMoney', title: "Tổng tiền", width: 150, filterable: { cell: { operator: 'gte', showOperators: false } } },
            { title: ' ', filterable: false, sortable: false }
        ]
    }

    $scope.FLMOwner_Document_Click = function ($event, data, win) {
        $event.preventDefault();
        $scope.Item.ID = data.ID;
        win.center().open();
        $scope.FLMOwnerMaterial_GridOptions.dataSource.read();
    }

    $scope.FLMOwnerMaterial_GridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.FLM,
            method: _FLMOwner_Index.URL.GenerateReceipt_List,
            readparam: function () { return { lst: $scope.Item.ListDetail } },
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
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,FLMOwnerMaterial_Grid,Material_GridChoose_Change)" />',
                headerAttributes: { style: 'text-align: center;' }, attributes: { style: 'text-align: center;' },
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,FLMOwnerMaterial_Grid,Material_GridChoose_Change)" />',
                filterable: false, sortable: false
            },
            { field: 'MaterialName', title: "Vật tư", width: 150, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'Quantity', title: "Số lượng", width: 150, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'Price', title: "Giá", width: 150, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'UnitPrice', title: "Đơn giá vật tư", width: 150, filterable: { cell: { operator: 'gte', showOperators: false } } },
            { title: ' ', filterable: false, sortable: false }
        ]
    }

    
    $scope.Material_GridChoose_Change = function ($event, grid, hasChoose) {
        $scope.HasChoose = hasChoose;
    }

    $scope.FLMOwnerRefresh_Save_Click = function ($event, win) {
        $event.preventDefault();
        
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.FLM,
                method: _FLMOwner_Index.URL.AcceptReceipt_Save,
                data: { lst: $scope.Item.lst },
                success: function (res) {
                    $scope.FLMOwnerRefresh_GridOptions.dataSource.read();
                    $rootScope.IsLoading = false;
                    $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                    win.close();
                },
                error: function (res) {
                    $rootScope.IsLoading = false;
                }
            });
    }
    //#endregion

}]);