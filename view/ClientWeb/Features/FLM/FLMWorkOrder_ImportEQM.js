/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />
/// <reference path="~/Scripts/Default.js" />


var _FLMWorkOrder_ImportEQM = {
    URL: {
        Get: 'FLMReceipt_ImportEQM_Get',
        Save: 'FLMReceipt_ImportEQM_Save',

        Get_NewEQM: 'FLMReceipt_ImportEQM_GetNewEQM',

        Stock_List: "FLMReceipt_ImportEQM_StockList",
        Vehicle_List: 'FLMReceipt_ImportEQM_VehicleList',
    },
    Data: {

    },
    Params: {
        ReceiptID: -1,
    },
}

angular.module('myapp').controller('FLMWorkOrder_ImportEQMCtrl', ['$rootScope', '$scope', '$http', '$location', '$state', '$timeout', function ($rootScope, $scope, $http, $location, $state, $timeout) {
    Common.Log('FLMWorkOrder_ImportEQMCtrl');
    $rootScope.IsLoading = false;
    _FLMWorkOrder_ImportEQM.Params = $.extend({}, true, $state.params);

    $scope.Item = null;
    $scope.isAddNew = false;

    $scope.ItemEQM = null;

    $scope.IsEmptyList = true;

    $scope.IsDisable = { Vehicle: true, Stock: false }

    Common.Services.Call($http, {
        url: Common.Services.url.FLM,
        method: _FLMWorkOrder_ImportEQM.URL.Get,
        data: { ReceiptID: _FLMWorkOrder_ImportEQM.Params.ReceiptID },
        success: function (res) {
            $scope.Item = res;
            $scope.FLMWOEQM_GridOptions.dataSource.data($scope.Item.ListDetail);
            $scope.IsEmptyList = res.ListDetail.length == 0;
            
            if (res.IsStock)//theo kho
            {
                $scope.IsDisable.Vehicle = true;
                $scope.IsDisable.Stock = false;
                if (res.ID > 0 && res.ListDetail.length > 0)
                    $scope.IsDisable.Stock = true;
            }
            else {
                $scope.IsDisable.Vehicle = false;
                $scope.IsDisable.Stock = true;
                if (res.ID > 0 && res.ListDetail.length > 0)
                    $scope.IsDisable.Vehicle = true;
            }
        }
    });

    //#region
    $scope.FLMWOEQM_splitter_Options = {
        orientation: "vertical",
        panes: [
            { collapsible: false, resizable: false, size: "50%" },
            { collapsible: false, resizable: false }
        ]
    }

    $scope.FLMWOEQM_GridOptions = {
        dataSource: Common.DataSource.Local({
            model: {
                id: 'ID',
                fields: {
                    Name: { type: 'string' },
                    Quantity: { type: 'number' },
                    GroupOfEquipmentID: { type: 'int' },
                    YearOfProduction: { type: 'string' },
                    Manufactor: { type: 'string' },
                    Specification: { type: 'string' },
                    BaseValue: { type: 'number' },
                    CurrentValue: { type: 'number' },
                    RemainValue: { type: 'number' },
                    DepreciationPeriod: { type: 'number' },
                    DepreciationStart: { type: 'date' },
                    WarrantyPeriod: { type: 'number' },
                    WarrantyEnd: { type: 'date' },
                }
            }
        }),
        height: '100%', pageable: false, sortable: true, columnMenu: false, filterable: { mode: 'row' }, resizable: true, editable: false,
        columns: [
            {
                title: ' ', width: '85px',
                template: '<a href="/" ng-click="Edit_Click($event,DetailEQM_win,DetailEQM_vform,dataItem,FLMWOEQM_Grid)" class="k-button"><i class="fa fa-pencil"></i></a>' +
                    '<a href="/" ng-click="Delete_Click($event,dataItem,FLMWOEQM_Grid)" class="k-button"><i class="fa fa-trash"></i></a>',
                filterable: false, sortable: false
            },
            { field: 'Name', title: "Tên thiết bị", width: 150, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'Quantity', title: "Số lượng", width: 70, filterable: { cell: { operator: 'equal', showOperators: false } } },
            { field: 'GroupOfEquipmentName', title: "Loại thiết bị", width: 150, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'YearOfProduction', title: "Năm sản xuất", width: 100, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'Manufactor', title: "Nhà sản xuất", width: 130, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'Specification', title: "Thông số kỹ thuật", width: 125, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'BaseValue', title: "Giá trị ban đầu", width: 100, filterable: { cell: { operator: 'equal', showOperators: false } } },
            { field: 'CurrentValue', title: "Giá trị hiện tại", width: 100, filterable: { cell: { operator: 'equal', showOperators: false } } },
            { field: 'RemainValue', title: "Giá trị còn lại", width: 100, filterable: { cell: { operator: 'equal', showOperators: false } } },
            { field: 'DepreciationPeriod', title: "T/g k.hao (tháng)", width: 100, filterable: { cell: { operator: 'equal', showOperators: false } } },
            {
                field: 'DepreciationStart', title: "T/g bắt đầu tính KH", width: '125px',
                template: "#=Common.Date.FromJsonDMY(DepreciationStart)#",
                filterable: { cell: { template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); }, operator: 'equal', showOperators: false } },
            },
            { field: 'WarrantyPeriod', title: "T/g bảo hành", width: 100, filterable: { cell: { operator: 'equal', showOperators: false } } },
            {
                field: 'WarrantyEnd', title: "Hạn bảo hành", width: '125px',
                template: "#=Common.Date.FromJsonDMY(WarrantyEnd)#",
                filterable: { cell: { template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); }, operator: 'equal', showOperators: false } },
            },
            { title: ' ', filterable: false, sortable: false }
        ]
    }

    $scope.Edit_Click = function ($event, win, vform, data, grid) {
        $event.preventDefault();
        $scope.isAddNew = false;
        $scope.ItemEQM = $.extend({}, true, data);
        $scope.LoadItem(win, vform)
    };
    $scope.Delete_Click = function ($event, data, grid) {
        $event.preventDefault();
        $rootScope.Message({
            Type: Common.Message.Type.Confirm,
            NotifyType: Common.Message.NotifyType.SUCCESS,
            Title: 'Thông báo',
            Msg: 'Bạn muốn xóa dữ liệu đã chọn?',
            Close: null,
            Ok: function () {
                grid.dataSource.remove(data);
                $timeout(function () {
                    $scope.IsEmptyList = (grid.dataSource.data().length == 0);
                }, 10)
            }
        })

    };

    $scope.cboFLMVehicleTo_Options = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true, minLength: 3,
        dataTextField: 'Code', dataValueField: 'ID', index: 0,
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    Code: { type: 'string' },
                }
            }
        }),
        change: function () {
        }

    };
    Common.Services.Call($http, {
        url: Common.Services.url.FLM,
        method: _FLMWorkOrder_ImportEQM.URL.Vehicle_List,
        data: {},
        success: function (res) {
            var data = [];
            data.push({ ID: -1, Code: 'Chọn xe' });
            Common.Data.Each(res.Data, function (o) {
                data.push(o);
            })
            $scope.cboFLMVehicleTo_Options.dataSource.data(data)
        }
    });

    $scope.cboFLMStockTo_Options = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true, minLength: 3,
        dataTextField: 'StockName', dataValueField: 'ID', index: 0,
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    StockName: { type: 'string' },
                }
            }
        }),
        change: function () {

        }

    };

    Common.Services.Call($http, {
        url: Common.Services.url.FLM,
        method: _FLMWorkOrder_ImportEQM.URL.Stock_List,
        success: function (res) {
            if (Common.HasValue(res)) {
                var data = [];
                data.push({ ID: -1, StockName: 'Chọn kho' });
                Common.Data.Each(res.Data, function (o) {
                    data.push(o);
                })
                $scope.cboFLMStockTo_Options.dataSource.data(data);
            }
        }
    });

    $scope.FLMWOEQM_changeTypeTo = function ($event) {
        if ($scope.Item.IsStock) {//chọn kho
            $scope.IsDisable.Vehicle = true;
            $scope.IsDisable.Stock = false;
        }
        else {
            $scope.IsDisable.Vehicle = false;
            $scope.IsDisable.Stock = true;
        }
    }

    $scope.FLMWOEQM_SaveClick = function ($event,vform) {
        $event.preventDefault();
        if(vform())
        {
            var check = true;
            if ($scope.Item.IsStock && !($scope.Item.StockID > 0)) { check = false; $rootScope.Message({ Msg: 'Chưa chọn vị trí kho đến', NotifyType: Common.Message.NotifyType.ERROR }); }
            if (!$scope.Item.IsStock && !($scope.Item.AssetID > 0)) { check = false; $rootScope.Message({ Msg: 'Chưa chọn vị trí xe đến', NotifyType: Common.Message.NotifyType.ERROR }); }

            if (check) {
                $scope.Item.ListDetail = $scope.FLMWOEQM_Grid.dataSource.data();
                $rootScope.IsLoading = true;
                Common.Services.Call($http, {
                    url: Common.Services.url.FLM,
                    method: _FLMWorkOrder_ImportEQM.URL.Save,
                    data: {item:$scope.Item},
                    success: function (res) {
                        $rootScope.IsLoading = false;
                        $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });

                        $state.go("main.FLMInputFuel.ImportEQM",{ReceiptID:res},{reload:true})
                    },
                    error: function (res) {
                        $rootScope.IsLoading = false;
                    }
                });

            }
        }
    }

    $scope.numQtyOfProduction_options = { format: 'n0', spinners: false, culture: 'en-US', min: 0, step: 1, decimals: 0, }
    $scope.numRemainValue_options = { format: 'n0', spinners: false, culture: 'en-US', min: 0, step: 1, decimals: 0, }
    $scope.numYearOfProduction_options = { format: '0000', spinners: false, culture: 'en-US', min: 0, max: 3000, step: 1, decimals: 0, }
    $scope.numBaseValue_options = { format: 'n0', spinners: false, culture: 'en-US', min: 0, step: 1, decimals: 0, }
    $scope.numCurrentValue_options = { format: 'n0', spinners: false, culture: 'en-US', min: 0, step: 1, decimals: 0, }
    $scope.numDepreciationPeriod_options = { format: 'n2', spinners: false, culture: 'en-US', min: 0, step: 1, decimals: 2, }
    $scope.numWarrantyPeriod_options = { format: 'n2', spinners: false, culture: 'en-US', min: 0, step: 1, decimals: 2, }


    $scope.EQM_AddClick = function ($event, win, vform) {
        $event.preventDefault();
        var item = null;
        $scope.isAddNew = true;
        Common.Services.Call($http, {
            url: Common.Services.url.FLM,
            method: _FLMWorkOrder_ImportEQM.URL.Get_NewEQM,
            data: {},
            success: function (res) {
                $scope.ItemEQM = res;
                $scope.LoadItem(win, vform);
            }
        })

    }

    $scope.cboGOE_options = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: "contains",
        suggest: true, dataTextField: 'GroupName', dataValueField: 'ID',
        dataSource: Common.DataSource.Local([], {
            id: 'ID',
            fields: {
                GroupName: { type: 'string' },
                ID: { type: 'number' },
            }
        }),
        change: function (e) {
            var cbb = this;
            if (e.sender.selectedIndex >= 0) {
                var text = cbb.text();
                $scope.ItemEQM.GroupOfEquipmentName = text;
            }
        }
    }
    Common.ALL.Get($http, {
        url: Common.ALL.URL.CATGroupOfEquipment,
        success: function (data) {
            var Newdata = [];
            Newdata.push({ ID: -1, GroupName: ' ' });
            Common.Data.Each(data, function (o) {
                Newdata.push(o);
            })
            $scope.cboGOE_options.dataSource.data(Newdata);
        }
    })

    $scope.Save_EQM = function ($event, win, vform, grid) {
        $event.preventDefault();
        if (vform()) {
            if ($scope.ItemEQM.Quantity < 1) $rootScope.Message({ Msg: 'Số lượng tối thiểu là một cái', NotifyType: Common.Message.NotifyType.ERROR });
            else {
                var data = grid.dataSource.data();
                if ($scope.isAddNew) {
                    data.push($scope.ItemEQM);
                }
                else {
                    for (var i = 0; i < data.length; i++) {
                        if (data[i].uid == $scope.ItemEQM.uid) {
                            data[i] = $.extend({}, true, $scope.ItemEQM);
                        }
                    }
                }
                grid.dataSource.data(data);
                $scope.IsEmptyList = (data.length == 0);
                win.close()
            }
        }

    }

    $scope.LoadItem = function (win, vform) {
        vform({ clear: true });
        win.center();
        win.open();
    }

    $scope.Win_CloseClick = function ($event, win) {
        $event.preventDefault();
        win.close();
    }

    $scope.Back_Click = function ($event) {
        $event.preventDefault();
        $state.go("main.FLMInputFuel.Index");
    }

    $scope.Excel_Click = function ($event) {
        $event.preventDefault();
        
    };
}])