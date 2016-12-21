/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />
/// <reference path="~/Scripts/Default.js" />


var _FLMWorkOrder_RepairLarge = {
    URL: {
        Get: 'FLMReceipt_RepairLargeGet',
        Save: 'FLMReceipt_RepairLargeSave',
        RepairChangeToSmall: 'FLMReceipt_RepairChangeToSmall',
        RepairChangeToLarge: 'FLMReceipt_RepairChangeToLarge',

        Get_Suplier: 'AllSupplier_List',
        Get_VehicleAutoComplete: 'VehicleAutoComplete_List',
        Get_Material: 'MaterialAll_List',

        Cost_Read: 'FLMMaintenance_CostList',
        
        Asset_CheckExpr: 'FLMFixedCost_ByAsset_CheckExpr',

        ListFixCost: 'FLMReceipt_RepairLargeListFixCost',
        GenerateFixCost: 'FLMReceipt_RepairLargeGenerateFixCost',
        DeleteFixCost: 'FLMReceipt_RepairLargeDeleteFixCost',
        SaveFixCost: 'FLMReceipt_RepairLargeSaveFixCost',
    },
    Data: {
        Vehicle: [],
        Material: [],
        IDUP: 1,
        Supplier: []
    },
    Params: {
        ID: -1,
    },
}

angular.module('myapp').controller('FLMWorkOrder_RepairLargeCtrl', ['$rootScope', '$scope', '$http', '$location', '$state', '$timeout', function ($rootScope, $scope, $http, $location, $state, $timeout) {
    Common.Log('FLMWorkOrder_RepairLargeCtrl');
    $rootScope.IsLoading = false;
    _FLMWorkOrder_RepairLarge.Params = $.extend({}, true, $state.params);

    $scope.IDRepair = _FLMWorkOrder_RepairLarge.Params.ID;
    $scope.isNew = true;
    $scope.Item = null;
    $scope.IsDepre = false;
    $scope.idx = -1;
    $scope.ExprDay = {};

    $scope.dataExprDay = [{ ID: 1, name: "", value: "" },
                         { ID: 2, name: "Theo tất cả ngày", value: "[Value]/[TotalDay]" },
                         { ID: 3, name: "Theo ngày hoạt động", value: "[Value]/[TotalDayOn]" }];

    $scope.dataExprInputDay = [{ ID: 1, name: "", value: "" }, { ID: 2, name: "Theo tất cả ngày", value: true },
                               { ID: 3, name: "Ngày hoạt động", value: "[IsDayOn]" },
                               { ID: 4, name: "Không phân bổ", value: null },
                               { ID: 5, name: "Khác", value: "" }];

    _FLMWorkOrder_RepairLarge.Data.IDUP = 1;
   
    Common.Services.Call($http, {
        url: Common.Services.url.FLM,
        method: _FLMWorkOrder_RepairLarge.URL.Get,
        data: { receiptID: _FLMWorkOrder_RepairLarge.Params.ID },
        success: function (res) {
            $scope.Item = res;
            $scope.IsDepre = res.IsDepreciation;
            $scope.LoadDataVehicle();
            $scope.LoadDataSupplier();
            $scope.LoadDataMaterial();

            if (res.ListVehicle.length > 0) {
                angular.forEach(res.ListVehicle, function (dataItem, key) {
                    dataItem.NumbersOfMaterial = dataItem.ListMaterial.length;
                    dataItem.NumbersOfCost = dataItem.ListCost.length;
                    var _amount = 0;
                    if (dataItem.ListMaterial.length > 0) {
                        angular.forEach(dataItem.ListMaterial, function (item, key) {
                            _amount += item.Amount
                        });
                    }
                    if (dataItem.ListCost.length > 0) {
                        angular.forEach(dataItem.ListCost, function (item, key) {
                            _amount += item.Amount
                        });
                    }
                    dataItem.Amount = _amount;
                });
                $scope.Vehicle_GridOptions.dataSource.data(res.ListVehicle);
            }
            $timeout(function () {
                $scope.cboExprDay_options.dataSource.data($scope.dataExprDay);
                $scope.cboExprInputDay_options.dataSource.data($scope.dataExprInputDay);
            }, 10);

        }
    });

    //#region
    $scope.FLMWORepairLarge_splitter_Options = {
        orientation: "vertical",
        panes: [
            { collapsible: false, resizable: false, size: '250px' },
            { collapsible: false, resizable: false }
        ]
    }

    $scope.RepairLarge_win_splitter_Options = {
        orientation: "vertical",
        panes: [
            { collapsible: false, resizable: false, size: "50%" },
            { collapsible: false, resizable: false }
        ]
    }

    //#region cbx

    $scope.numSupplier_Options = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true, dataTextField: 'SupplierName', dataValueField: 'ID',
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    SupplierName: { type: 'string' },
                }
            }
        })
    }


    Common.Services.Call($http, {
        url: Common.Services.url.FLM,
        method: _FLMWorkOrder_Fuel.URL.Get_Suplier,
        data: {},
        success: function (res) {
            res.Data.splice(0, 0, { SupplierName: ' ', ID: -1 })
            $scope.numSupplier_Options.dataSource.data(res.Data);
        }
    });

    $scope.FLMWORepairLarge_cboVehicle_Options = {
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
            var val = this.value();
            var name = this.text();
            var gridEditElement = $scope.Vehicle_Grid.editable.element;
            var dataItem = $scope.Vehicle_Grid.dataItem(gridEditElement.closest('tr'))
            if (dataItem != null) {
                dataItem.AssetID = val;
                dataItem.RegNo = name;
            }
        }
    }
    $scope.LoadDataVehicle = function () {
        Common.Services.Call($http, {
            url: Common.Services.url.FLM,
            method: _FLMWorkOrder_RepairLarge.URL.Get_VehicleAutoComplete,
            success: function (res) {
                _FLMWorkOrder_RepairLarge.Data.Vehicle = res.Data;
                $scope.FLMWORepairLarge_cboVehicle_Options.dataSource.data(res.Data);
            }
        });
    }

    $scope.FLMWORepairLarge_cboSupplier_Options = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true, dataTextField: 'SupplierName', dataValueField: 'ID',
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    SupplierName: { type: 'string' },
                }
            }
        }),
        change: function (e) {
            var val = this.value();
            var name = this.text();
            var grid = $("#material_Grid").data("kendoGrid");
            var gridEditElement = grid.editable.element;
            var dataItem = grid.dataItem(gridEditElement.closest('tr'))
            if (dataItem != null) {
                dataItem.SupplierID = val;
                dataItem.SupplierName = name;
            }
        }
    }
    $scope.LoadDataSupplier = function () {
        Common.Services.Call($http, {
            url: Common.Services.url.FLM,
            method: _FLMWorkOrder_RepairLarge.URL.Get_Suplier,
            success: function (res) {
                _FLMWorkOrder_RepairLarge.Data.Supplier = res.Data;
                $scope.FLMWORepairLarge_cboSupplier_Options.dataSource.data(res.Data);
            }
        });
    }

    $scope.FLMWORepairLarge_cboMaterial_Options = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true, dataTextField: 'MaterialName', dataValueField: 'MaterialID',
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'MaterialID',
                fields: {
                    MaterialID: { type: 'number' },
                    MaterialName: { type: 'string' },
                }
            }
        }),
        change: function (e) {
            var val = this.value();
            var name = this.text();
            var grid = $("#material_Grid").data("kendoGrid");
            var gridEditElement = grid.editable.element;
            var dataItem = grid.dataItem(gridEditElement.closest('tr'))
            if (dataItem != null) {
                dataItem.MaterialID = val;
                dataItem.MaterialName = name;
            }
        }
    }
    $scope.LoadDataMaterial = function () {
        Common.Services.Call($http, {
            url: Common.Services.url.FLM,
            method: _FLMWorkOrder_RepairLarge.URL.Get_Material,
            success: function (res) {
                var item = { MaterialID: -1, MaterialName: '' };
                res.Data.unshift(item);
                $scope.FLMWORepairLarge_cboMaterial_Options.dataSource.data(res.Data);
                _FLMWorkOrder_RepairLarge.Data.Material = res.Data;
            }
        });
    }

    $scope.FLMWORepairLarge_cboCost_Options = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true, dataTextField: 'CostName', dataValueField: 'ID',
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    CostName: { type: 'string' },
                    Code: { type: 'string' },
                }
            }
        }),
        change: function (e) {
            var val = this.value();
            var name = this.text();
            var grid = $("#cost_Grid").data("kendoGrid");
            var gridEditElement = grid.editable.element;
            var dataItem = grid.dataItem(gridEditElement.closest('tr'))
            if (dataItem != null) {
                dataItem.CostID = val;
                dataItem.CostName = name;
            }
        }
    }
    Common.Services.Call($http, {
        url: Common.Services.url.FLM,
        method: _FLMWorkOrder_RepairLarge.URL.Cost_Read,
        success: function (res) {
            $scope.FLMWORepairLarge_cboCost_Options.dataSource.data(res.Data);
        }
    });

    //#endregion
    $scope.FLMWOMaterial_numQuantity_Options = {
        format: '#,##0.00', spinners: false, culture: 'en-US', min: 0, step: 1, decimals: 2,
        change: function (e) {
            var val = this.value();
            var grid = $("#material_Grid").data("kendoGrid");
            var gridEditElement = grid.editable.element;
            var dataItem = grid.dataItem(gridEditElement.closest('tr'))
            if (dataItem != null) {
                dataItem.Amount = val * dataItem.Price;
            }
        }
    }

    $scope.FLMWOMaterial_numPrice_Options = {
        format: '#,##0', spinners: false, culture: 'en-US', min: 0, step: 1, decimals: 0,
        change: function (e) {
            var val = this.value();
            var grid = $("#material_Grid").data("kendoGrid");
            var gridEditElement = grid.editable.element;
            var dataItem = grid.dataItem(gridEditElement.closest('tr'))
            if (dataItem != null) {
                dataItem.Amount = val * dataItem.Quantity;
            }
        }
    }

    $scope.FLMWOVehicle_numKMEnd_Options = { format: '#,##0.00', spinners: false, culture: 'en-US', min: 0, step: 1, decimals: 2, }

    //#region vehicle
    $scope.Vehicle_GridOptions = {
        dataSource: Common.DataSource.Local({
            model: {
                id: 'ID',
                fields: {
                    RegNo: { type: 'string' },
                    KMEnd: { type: 'number' },
                    NumbersOfMaterial: { type: 'number', editable: false },
                    NumbersOfCost: { type: 'number', editable: false },
                    Amount: { type: 'number', editable: false },
                }
            }
        }),
        height: '100%', pageable: false, sortable: true, columnMenu: false, filterable: { mode: 'row' }, resizable: true, editable: 'incell',
        dataBound: function () {
            this.expandRow(this.tbody.find("tr.k-master-row").first());
        },
        columns: [
            {
                title: ' ', width: 90, template: '<a href="/" ng-show="!IsDepre && !Item.IsApproved" ng-click="FLMWORepairLarge_Grid_DeleteClick($event,Vehicle_Grid)" class="k-button" ><i class="fa fa-trash"></i></a>' +
                    '<a href="/" ng-click="FLMWORepairLarge_Grid_EditClick($event,RepairLarge_win,dataItem)" class="k-button"><i class="fa fa-file"></i></a>',
                filterable: false, sorable: false
            },
            {
                field: 'AssetID', title: 'Xe', template: "#=RegNo#", width: 150,
                editor: function (container, options) {
                    $('<input  name="' + options.field + '" kendo-combo-box k-options="FLMWORepairLarge_cboVehicle_Options"/>').appendTo(container)
                }
            },
            {
                field: 'KMEnd', title: 'Số KM end', width: 150,
                editor: function (container, options) {
                    $('<input  name="' + options.field + '" kendo-numeric-text-box style="width:100%" k-options="FLMWOVehicle_numKMEnd_Options"/>').appendTo(container)
                }
            },
            { field: 'NumbersOfMaterial', title: "Số loại vật tư", width: 150, filterable: { cell: { operator: 'eq', showOperators: false } } },
            { field: 'NumbersOfCost', title: "Số loại chi phí", width: 150, filterable: { cell: { operator: 'eq', showOperators: false } } },
            { field: 'Amount', title: "Tổng tiền", width: 150, filterable: { cell: { operator: 'eq', showOperators: false } } },
            { title: ' ', filterable: false, sortable: false }
        ]
    }

    $scope.FLMWORepairLarge_Grid_EditClick = function ($event, win, data) {
        $event.preventDefault();
        $scope.RepairLargeLoadItem(data, win);
    }

    $scope.RepairLargeLoadItem = function (data, win) {
        $scope.idx = $("#Vehicle_Grid").data("kendoGrid").dataSource.data().indexOf(data);
        $scope.material_GridOptions.dataSource.data(data.ListMaterial);
        $scope.cost_GridOptions.dataSource.data(data.ListCost);
        $scope.LoadDataSupplier();
        win.center();
        win.open();
        
        $("#RepairLarge_win_splitter").data('kendoSplitter').resize();
    }

    $scope.RepairLarge_win_Save_Click = function ($event, win, material_grid, cost_grid) {
        $event.preventDefault();

        var materiallst = material_grid.dataSource.data();
        var costlst = cost_grid.dataSource.data();

        var vehicle_grid = $("#Vehicle_Grid").data("kendoGrid");
        var vehiclelst = vehicle_grid.dataSource.data();
        var dataItem = vehiclelst[$scope.idx];

        var error = false;
        var check = {};
        Common.Data.Each(materiallst, function (o) {
            if (Common.HasValue(check[o.MaterialID])) { error = true; }
            else check[o.MaterialID] = o;
        })

        if (error)
            $rootScope.Message({ Msg: 'Vật tư bị trùng', NotifyType: Common.Message.NotifyType.ERROR });
        else if (materiallst.length == 0 && costlst.length == 0)
            $rootScope.Message({ Msg: 'Chưa thêm vật tư hoặc chi phí', Type: Common.Message.Type.Notify, NotifyType: Common.Message.NotifyType.ERROR });
        else {
            var checkListMaterial = true;
            angular.forEach(materiallst, function (item, idx) {
                if (!Common.HasValue(item.SupplierID) || item.SupplierID <= 0) {
                    $rootScope.Message({ Msg: 'Chưa chọn nhà phân phối', NotifyType: Common.Message.NotifyType.ERROR });
                    checkListMaterial = false;
                }
                if (!Common.HasValue(item.MaterialID) || item.MaterialID <= 0) {
                    $rootScope.Message({ Msg: 'Chưa chọn vật tư', NotifyType: Common.Message.NotifyType.ERROR });
                    checkListMaterial = false;
                }
                if (!Common.HasValue(item.Quantity) || item.Quantity <= 0) {
                    $rootScope.Message({ Msg: 'Chưa nhập số lượng', NotifyType: Common.Message.NotifyType.ERROR });
                    checkListMaterial = false;
                }
                if (!Common.HasValue(item.Price) || item.Price <= 0) {
                    $rootScope.Message({ Msg: 'Chưa nhập đơn giá', NotifyType: Common.Message.NotifyType.ERROR });
                    checkListMaterial = false;
                }
            });

            var checkListCost = true;
            angular.forEach(costlst, function (item, idx) {
                if (!Common.HasValue(item.CostID) || item.CostID <= 0) {
                    $rootScope.Message({ Msg: 'Chưa nhập tên chi phí', NotifyType: Common.Message.NotifyType.ERROR });
                    checkListMaterial = false;
                }

                if (!Common.HasValue(item.Amount) || item.Amount <= 0) {
                    $rootScope.Message({ Msg: 'Thành tiền phải lớn hơn 0', NotifyType: Common.Message.NotifyType.ERROR });
                    checkListMaterial = false;
                }
            });

            if (checkListMaterial && checkListCost) {
                dataItem.ListMaterial = materiallst;
                dataItem.ListCost = costlst;

                dataItem.NumbersOfMaterial = materiallst.length;
                dataItem.NumbersOfCost = costlst.length;
                var _amount = 0;
                if (materiallst.length > 0) {
                    angular.forEach(materiallst, function (item, key) {
                        _amount += item.Amount
                    });
                }
                if (costlst.length > 0) {
                    angular.forEach(costlst, function (item, key) {
                        _amount += item.Amount
                    });
                }
                dataItem.Amount = _amount;
                //$("#Vehicle_Grid").data("kendoGrid").dataSource.data(vehiclelst);
                //$rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                win.close();
            }
        }
    }

    $scope.FLMWORepairLarge_Grid_DeleteClick = function ($event, grid) {
        $event.preventDefault()
        var tr = $($event.target).closest('tr');
        var item = grid.dataItem(tr);

        if (Common.HasValue(item))
            grid.dataSource.remove(item)
    }

    $scope.FLMWORepairLarge_AddVehicleClick = function ($event, grid) {
        $event.preventDefault();
        var itemNew = { AssetID: _FLMWorkOrder_RepairLarge.Data.Vehicle[0].ID, RegNo: _FLMWorkOrder_RepairLarge.Data.Vehicle[0].RegNo, KMEnd: 0, ListMaterial: [], ListCost: [], NumbersOfMaterial: 0, NumbersOfCost: 0, Amount: 0 };

        grid.dataSource.insert(0, itemNew);
    }
    //#endregion

    //#region material
    $scope.material_GridOptions = {
        dataSource: Common.DataSource.Local({
            model: {
                id: 'ID',
                fields: {
                    SupplierName: { type: 'string' },
                    MaterialName: { type: 'string' },
                    Quantity: { type: 'number' },
                    Price: { type: 'number' },
                    Amount: { type: 'number', editable: false },
                }
            }
        }),
        height: '100%', pageable: false, sortable: true, columnMenu: false, filterable: { mode: 'row' }, resizable: true, editable: 'incell',
        dataBound: function () {
            this.expandRow(this.tbody.find("tr.k-master-row").first());
        },
        columns: [
            {
                title: ' ', width: 50, template: '<a href="/" ng-show="!IsDepre && !Item.IsApproved" ng-click="material_win_Grid_DeleteClick($event,material_Grid)" class="k-button" ><i class="fa fa-trash"></i></a>',
                filterable: false, sorable: false
            },
            {
                field: 'SupplierID', title: 'Nhà phân phối', template: "#=SupplierName#", width: 150,
                editor: function (container, options) {
                    $('<input  name="' + options.field + '" kendo-combo-box k-options="FLMWORepairLarge_cboSupplier_Options"/>').appendTo(container)
                }
            },
            {
                field: 'MaterialID', title: 'Vật tư', template: "#=MaterialName#", width: 150,
                editor: function (container, options) {
                    $('<input  name="' + options.field + '" kendo-combo-box ng-show="!IsDepre" k-options="FLMWORepairLarge_cboMaterial_Options"/>').appendTo(container)
                }
            },
            {
                field: 'Quantity', title: 'Số lượng', width: 150,
                editor: function (container, options) {
                    $('<input  name="' + options.field + '" kendo-numeric-text-box ng-show="!IsDepre" style="width:100%" k-options="FLMWOMaterial_numQuantity_Options"/>').appendTo(container)
                }
            },
            {
                field: 'Price', title: 'Đơn giá', width: 150,
                editor: function (container, options) {
                    $('<input  name="' + options.field + '" kendo-numeric-text-box ng-show="!IsDepre" style="width:100%" k-options="FLMWOMaterial_numPrice_Options"/>').appendTo(container)
                }
            },
            { field: "Amount", title: "Thành tiền", width: "120px", filterable: { cell: { operator: 'eq', showOperators: false } } },
            { title: ' ', filterable: false, sortable: false }
        ]
    }

    $scope.material_win_Grid_DeleteClick = function ($event, grid) {
        $event.preventDefault()
        var tr = $($event.target).closest('tr');
        var item = grid.dataItem(tr);

        if (Common.HasValue(item))
            grid.dataSource.remove(item)
    }

    $scope.RepairLarge_AddMaterialClick = function ($event, grid) {
        $event.preventDefault();
        var itemNew = { SupplierID: _FLMWorkOrder_RepairLarge.Data.Supplier[0].ID, SupplierName: _FLMWorkOrder_RepairLarge.Data.Supplier[0].SupplierName, MaterialID: -1, MaterialName: 'Chọn vật tư', Quantity: 0, Price: 0, Amount: 0 };

        grid.dataSource.insert(0, itemNew);
    }
    //#endregion

    //#region cost
    $scope.cost_GridOptions = {
        dataSource: Common.DataSource.Local({
            model: {
                id: 'ID',
                fields: {
                    CostID: { type: 'number' },
                    CostCode: { type: 'string' },
                    CostName: { type: 'string' },
                    Amount: { type: 'number' },
                }
            }
        }),
        height: '100%', pageable: false, sortable: true, columnMenu: false, filterable: { mode: 'row' }, resizable: true, editable: "incell",
        dataBound: function () {
            //this.expandRow(this.tbody.find("tr.k-master-row").first());
        },
        columns: [
            {
                title: ' ', width: 50, template: '<a href="/" ng-show="!IsDepre" ng-click="cost_win_Grid_DeleteClick($event,cost_Grid)" class="k-button" ><i class="fa fa-trash"></i></a>',
                filterable: false, sorable: false
            },
            {
                field: 'CostID', title: 'Chi phí', template: "#=CostName#", width: 150,
                editor: function (container, options) {
                    $('<input ng-show="!IsDepre" name="' + options.field + '" kendo-combo-box k-options="FLMWORepairLarge_cboCost_Options"/>').appendTo(container)
                }
            },
            { field: "Amount", title: "Thành tiền", width: "120px", filterable: { cell: { operator: 'eq', showOperators: false } } },
            { title: ' ', filterable: false, sortable: false }
        ]
    }

    $scope.cost_win_Grid_DeleteClick = function ($event, grid) {
        $event.preventDefault()
        var tr = $($event.target).closest('tr');
        var item = grid.dataItem(tr);

        if (Common.HasValue(item))
            grid.dataSource.remove(item)
    }

    $scope.RepairLarge_AddCostClick = function ($event, grid) {
        $event.preventDefault();
        var itemNew = { CostID: 0, CostCode: '', CostName: '', Amount: 0 };

        grid.dataSource.insert(0, itemNew);
    }
    //#endregion
    $scope.FLMWORepairLarge_SaveClick = function ($event, grid) {
        $event.preventDefault();

        var vehiclelst = grid.dataSource.data();
        var error = false;
        var checkVehiclelst = true;
        var check = {};
        Common.Data.Each(vehiclelst, function (o) {
            if (Common.HasValue(check[o.AssetID])) { error = true; }
            else check[o.AssetID] = o;

            if (!Common.HasValue(o.AssetID) || !o.AssetID > 0) {
                $rootScope.Message({ Msg: 'Chưa chọn xe', Type: Common.Message.Type.Notify, NotifyType: Common.Message.NotifyType.ERROR });
                checkVehiclelst = false;
            }
            if (!Common.HasValue(o.KMEnd) || !(o.KMEnd >= 0)) {
                $rootScope.Message({ Msg: 'Chưa nhập số KM end', Type: Common.Message.Type.Notify, NotifyType: Common.Message.NotifyType.ERROR });
                checkVehiclelst = false;
            }
            if (o.NumbersOfMaterial == 0 && o.NumbersOfCost == 0) {
                $rootScope.Message({ Msg: 'Chưa nhập vật tư hoặc chi phí', Type: Common.Message.Type.Notify, NotifyType: Common.Message.NotifyType.ERROR });
                checkVehiclelst = false;
            }
        })

        if (error)
            $rootScope.Message({ Msg: 'Xe bị trùng', Type: Common.Message.Type.Notify, NotifyType: Common.Message.NotifyType.ERROR });
        if (!Common.HasValue($scope.Item.DateReceipt)) {
            $rootScope.Message({ Msg: 'Chưa chọn Ngày cấp phiếu', Type: Common.Message.Type.Notify, NotifyType: Common.Message.NotifyType.ERROR });
            error = true;
        }
        if ($scope.Item.IsDepreciation) {
            if (!Common.HasValue($scope.Item.DepreciationStart)) {
                $rootScope.Message({ Msg: 'Chưa chọn T/g bắt đầu tính khấu hao', Type: Common.Message.Type.Notify, NotifyType: Common.Message.NotifyType.ERROR });
            }
            if ($scope.Item.DepreciationPeriod <= 0) {
                $rootScope.Message({ Msg: 'Chưa nhập T/g khấu hao', Type: Common.Message.Type.Notify, NotifyType: Common.Message.NotifyType.ERROR });
            }
        }

        if (!error && checkVehiclelst) {
            $scope.Item.ListVehicle = vehiclelst;
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.FLM,
                method: _FLMWorkOrder_RepairLarge.URL.Save,
                data: { item: $scope.Item },
                success: function (res) {
                    $rootScope.IsLoading = false;
                    $rootScope.Message({ Msg: 'Thành công', Type: Common.Message.Type.Notify, NotifyType: Common.Message.NotifyType.SUCCESS });
                    $state.go("main.FLMInputFuel.RepairLarge", { 'ID': res });
                }
            });
        }
    }

    $scope.numQuantity_options = { format: 'n0', spinners: false, culture: 'en-US', min: 0, step: 1, decimals: 0, }

    $scope.depreciation_GridOptions = {
        dataSource: Common.DataSource.Local({
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number', editable: false, nullable: true },
                    Month: { type: 'number', editable: false },
                    Year: { type: 'number', editable: false },
                    Value: { type: 'number' },
                    IsClosed: { type: 'boolean', editable: false },
                }
            }
        }),
        height: '100%', pageable: false, sortable: true, columnMenu: false, filterable: { mode: 'row' }, resizable: true, editable: false,
        columns: [
            { field: 'AssetCode', title: "Số xe", width: 150, filterable: { cell: { operator: 'contains', showOperators: false } } },
            {
                field: 'Month', title: 'Tháng',
                filterable: { cell: { operator: 'equal', showOperators: false }, width: 100, editable: false, }
            },
            {
                field: 'Year', title: 'Năm',
                filterable: { cell: { operator: 'equal', showOperators: false }, width: 100, editable: false, }
            },
            {
                field: 'Value', title: 'Giá trị', width: 200,
                template: '#=Common.Number.ToNumber2(Value)#',
                editor: function (container, options) {
                    $('<input  name="' + options.field + '" kendo-numeric-text-box style="width:100%" k-options="numValue_Options"/>').appendTo(container)
                }
            },
            {
                field: 'IsClosed', title: 'Đã đóng', width: 100,
                template: '<input type="checkbox" ng-model="dataItem.IsClosed" disabled="disabled" />',
                attributes: { style: "text-align: center; " },
                filterable: {
                    cell: {
                        template: function (container) {
                            container.element.kendoComboBox({
                                dataSource: [{ Text: 'Tất cả', Value: '' }, { Text: 'Đã đóng', Value: true }, { Text: 'Chưa đóng', Value: false }],
                                dataTextField: "Text", dataValueField: "Value",
                            });
                        }, showOperators: false
                    }
                }
            },
            {
                title: '', filterable: false, sortable: false
            },
        ]
    }


    $scope.cboExprInputDay_options = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: "contains", suggest: true, dataTextField: 'name', dataValueField: 'value',
        dataSource: Common.DataSource.Local([], {
            id: 'value',
            fields: {
                name: { type: 'string' },
                value: { type: 'string' },
            }
        }),
        change: function (e) {
            if (e.sender.selectedIndex >= 0) $scope.IsEdited = true;
        }
    }
    $scope.cboExprDay_options = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: "contains", suggest: true, dataTextField: 'name', dataValueField: 'value',
        dataSource: Common.DataSource.Local([], {
            id: 'value',
            fields: {
                name: { type: 'string' },
                value: { type: 'string' },
            }
        }),
        change: function (e) {
            if (e.sender.selectedIndex >= 0) $scope.IsEdited = true;
        }
    }
    $scope.depreciation_win_Click = function ($event, win) {
        $event.preventDefault();
        $rootScope.IsLoading = true;
        Common.Services.Call($http, {
            url: Common.Services.url.FLM,
            method: _FLMWorkOrder_RepairLarge.URL.ListFixCost,
            data: { receiptID: _FLMWorkOrder_RepairLarge.Params.ID },
            success: function (res) {
                $scope.depreciation_GridOptions.dataSource.data(res);
                win.center().open();
                $rootScope.IsLoading = false;
                $timeout(function () {
                    $scope.depreciation_Grid.resize();
                }, 1);
            }
        });
    };

    $scope.FixedCost_Generate = function ($event, grid) {
        $event.preventDefault();
        $rootScope.IsLoading = true;
        Common.Services.Call($http, {
            url: Common.Services.url.FLM,
            method: _FLMWorkOrder_RepairLarge.URL.GenerateFixCost,
            data: { receiptID: _FLMWorkOrder_RepairLarge.Params.ID },
            success: function (res) {
                grid.dataSource.data(res);
                $rootScope.IsLoading = false;
            }
        });
    }

    $scope.FixedCost_Save = function ($event, grid, win) {
        $event.preventDefault();
        $rootScope.IsLoading = true;
        Common.Services.Call($http, {
            url: Common.Services.url.FLM,
            method: _FLMWorkOrder_RepairLarge.URL.SaveFixCost,
            data: { receiptID: _FLMWorkOrder_RepairLarge.Params.ID, lst: grid.dataSource.data() },
            success: function (res) {
                Common.Services.Call($http, {
                    url: Common.Services.url.FLM,
                    method: _FLMWorkOrder_RepairLarge.URL.Get,
                    data: { receiptID: _FLMWorkOrder_RepairLarge.Params.ID },
                    success: function (res) {
                        $scope.Item = res;
                        $scope.IsDepre = res.IsDepreciation;
                        $scope.LoadDataVehicle();
                        $scope.LoadDataSupplier();
                        $scope.LoadDataMaterial();

                        if (res.ListVehicle.length > 0) {
                            angular.forEach(res.ListVehicle, function (dataItem, key) {
                                dataItem.NumbersOfMaterial = dataItem.ListMaterial.length;
                                dataItem.NumbersOfCost = dataItem.ListCost.length;
                                var _amount = 0;
                                if (dataItem.ListMaterial.length > 0) {
                                    angular.forEach(dataItem.ListMaterial, function (item, key) {
                                        _amount += item.Amount
                                    });
                                }
                                if (dataItem.ListCost.length > 0) {
                                    angular.forEach(dataItem.ListCost, function (item, key) {
                                        _amount += item.Amount
                                    });
                                }
                                dataItem.Amount = _amount;
                            });
                            $scope.Vehicle_GridOptions.dataSource.data(res.ListVehicle);
                        }

                        win.close();
                        $rootScope.IsLoading = false;
                        $rootScope.Message({ Msg: 'Đã cập nhật.', NotifyType: Common.Message.NotifyType.SUCCESS });
                    }
                });
            }
        });
    }

    $scope.FixedCost_Delete = function ($event, grid, win) {
        $event.preventDefault();
        $rootScope.IsLoading = true;
        Common.Services.Call($http, {
            url: Common.Services.url.FLM,
            method: _FLMWorkOrder_RepairLarge.URL.DeleteFixCost,
            data: { receiptID: _FLMWorkOrder_RepairLarge.Params.ID },
            success: function (res) {

                Common.Services.Call($http, {
                    url: Common.Services.url.FLM,
                    method: _FLMWorkOrder_RepairLarge.URL.Get,
                    data: { receiptID: _FLMWorkOrder_RepairLarge.Params.ID },
                    success: function (res) {
                        $scope.Item = res;
                        $scope.IsDepre = res.IsDepreciation;
                        $scope.LoadDataVehicle();
                        $scope.LoadDataSupplier();
                        $scope.LoadDataMaterial();

                        if (res.ListVehicle.length > 0) {
                            angular.forEach(res.ListVehicle, function (dataItem, key) {
                                dataItem.NumbersOfMaterial = dataItem.ListMaterial.length;
                                dataItem.NumbersOfCost = dataItem.ListCost.length;
                                var _amount = 0;
                                if (dataItem.ListMaterial.length > 0) {
                                    angular.forEach(dataItem.ListMaterial, function (item, key) {
                                        _amount += item.Amount
                                    });
                                }
                                if (dataItem.ListCost.length > 0) {
                                    angular.forEach(dataItem.ListCost, function (item, key) {
                                        _amount += item.Amount
                                    });
                                }
                                dataItem.Amount = _amount;
                            });
                            $scope.Vehicle_GridOptions.dataSource.data(res.ListVehicle);
                        }

                        win.close();
                        $rootScope.IsLoading = false;
                        $rootScope.Message({ Msg: 'Đã Xóa.', NotifyType: Common.Message.NotifyType.SUCCESS });
                    }
                });

            }
        });
    }

    $scope.RepairChangeToLarge_Click = function ($event) {
        $event.preventDefault();
      
        $rootScope.Message({
            Msg: "Xác nhận tính khấu hao?",
            Type: Common.Message.Type.Confirm,
            Ok: function () {
                $rootScope.IsLoading = true;
                Common.Services.Call($http, {
                    url: Common.Services.url.FLM,
                    method: _FLMWorkOrder_RepairLarge.URL.RepairChangeToLarge,
                    data: { receiptID: _FLMWorkOrder_RepairLarge.Params.ID },
                    success: function (res) {
                        $rootScope.IsLoading = false;
                        $timeout(function () {
                            $state.go("main.FLMInputFuel.RepairLarge", { 'ID': res }, { reload: true });
                        }, 10);
                    }
                });
            }
        })
    }

    $scope.RepairChangeToSmall_Click = function ($event) {
        $event.preventDefault();
       
        $rootScope.Message({
            Msg: "Xác nhận không tính khấu hao?",
            Type: Common.Message.Type.Confirm,
            Ok: function () {
                $rootScope.IsLoading = true;
                Common.Services.Call($http, {
                    url: Common.Services.url.FLM,
                    method: _FLMWorkOrder_RepairLarge.URL.RepairChangeToSmall,
                    data: { receiptID: _FLMWorkOrder_RepairLarge.Params.ID },
                    success: function (res) {
                        $rootScope.IsLoading = false;
                        $state.go("main.FLMInputFuel.RepairLarge", { 'ID': res }, {reload:true});
                    }
                });
            }
        })
    }

    $scope.window_Close_Click = function ($event, win) {
        $event.preventDefault();
        win.close();
    }

    $scope.Back_Click = function ($event) {
        $event.preventDefault();

        $state.go("main.FLMInputFuel.Index");
    };
}])