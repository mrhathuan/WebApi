/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />
/// <reference path="~/Scripts/Default.js" />


var _FLMWorkOrder_Fuel = {
    URL: {
        Get: 'FLMReceipt_FuelGet',
        FLMReceipt_FuelRequestGet:'FLMReceipt_FuelRequestGet',
        FLMReceipt_QuantityPerKMGet:'FLMReceipt_QuantityPerKMGet',
        Save: 'FLMReceipt_FuelSave',

        Get_Suplier: 'AllSupplier_List',
        Get_Vehicle: 'FLMVehicle_List',
        Get_Material: 'MaterialAll_List',
    },
    Data: {
        Material: []
    },
    Params: {
        ID: -1,
    },
}

angular.module('myapp').controller('FLMWorkOrder_FuelCtrl', ['$rootScope', '$scope', '$http', '$location', '$state', '$timeout', function ($rootScope, $scope, $http, $location, $state, $timeout) {
    Common.Log('FLMWorkOrder_FuelCtrl');
    $rootScope.IsLoading = false;
    _FLMWorkOrder_Fuel.Params = $.extend({}, true, $state.params);
    $scope.FuelID = _FLMWorkOrder_Fuel.Params.ID;
    $scope.Item = null;

    Common.Services.Call($http, {
        url: Common.Services.url.FLM,
        method: _FLMWorkOrder_Fuel.URL.Get,
        data: { ID: _FLMWorkOrder_Fuel.Params.ID },
        success: function (res) {
            $scope.Item = res;
            $scope.LoadDataMaterial();
            $scope.LoadDataVehicle();
            $scope.FLMWorkOrderFuel_win_GridOptions.dataSource.data(res.ListDetail);
            $timeout(function () {
                $scope.FLMWorkOrderFuel_splitter.resize();
            }, 1);

            Common.Services.Call($http, {
                url: Common.Services.url.FLM,
                method: _FLMWorkOrder_Fuel.URL.FLMReceipt_FuelRequestGet,
                data: { ID: _FLMWorkOrder_Fuel.Params.ID, VehicleID: $scope.Item.VehicleID },
                success: function (res) {
                    if (Common.HasValue(res)) {
                        if(res.KMStart > 0)
                          $scope.Item.KMStart = res.KMStart;
                    }
                }
            });
        }
    });

    //#region
    $scope.FLMWorkOrderFuel_splitter_Options = {
        orientation: "vertical",
        panes: [
            { collapsible: false, resizable: false, size: "55%" },
            { collapsible: false, resizable: false }
        ]
    }

    $scope.FLMWorkOrderFuel_cboVehicle_Options = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true, dataTextField: 'Code', dataValueField: 'ID',
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
        change: function (e) {
            var val = this.value();
            Common.Services.Call($http, {
                url: Common.Services.url.FLM,
                method: _FLMWorkOrder_Fuel.URL.FLMReceipt_FuelRequestGet,
                data: { ID: _FLMWorkOrder_Fuel.Params.ID, VehicleID: val },
                success: function (res) {
                    if (Common.HasValue(res)) {
                        if (res.KMStart > 0)
                            $scope.Item.KMStart = res.KMStart;
                    }
                }
            });
        }
    }

    $scope.FLMWorkOrderFuel_numSupplier_Options = {
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
            $scope.FLMWorkOrderFuel_numSupplier_Options.dataSource.data(res.Data);
        }
    });


    $scope.LoadDataVehicle = function () {
        Common.Services.Call($http, {
            url: Common.Services.url.FLM,
            method: _FLMWorkOrder_Fuel.URL.Get_Vehicle,
            data: {},
            success: function (res) {
                if (Common.HasValue(res)) {
                    $scope.FLMWorkOrderFuel_cboVehicle_Options.dataSource.data(res.Data);
                }
            }
        });
    }
    $scope.FLMWorkOrderFuel_cboMaterial_Options = {
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
            var gridEditElement = $scope.FLMWorkOrderFuel_win_Grid.editable.element;
            var dataItem = $scope.FLMWorkOrderFuel_win_Grid.dataItem(gridEditElement.closest('tr'))
            if (dataItem != null) {
                dataItem.MaterialID = val;
                dataItem.MaterialName = name;
            }
            Common.Services.Call($http, {
                url: Common.Services.url.FLM,
                method: _FLMWorkOrder_Fuel.URL.FLMReceipt_QuantityPerKMGet,
                data: { MaterialID: val, VehicleID: $scope.Item.VehicleID },
                success: function (res) {
                    if ($scope.Item.KMEnd > $scope.Item.KMStart)
                        dataItem.Quantity = ($scope.Item.KMEnd - $scope.Item.KMStart) * res[0];
                    else
                        dataItem.Quantity = 0;
                    dataItem.Price = res[1];
                    dataItem.Amount = dataItem.Price * dataItem.Quantity;
                }
            });
        }
    }

    $scope.LoadDataMaterial = function () {
        Common.Services.Call($http, {
            url: Common.Services.url.FLM,
            method: _FLMWorkOrder_Fuel.URL.Get_Material,
            data: {  },
            success: function (res) {
                _FLMWorkOrder_Fuel.Data.Material=[]
                Common.Data.Each(res.Data, function (o) {
                    if (o.IsFuel)
                        _FLMWorkOrder_Fuel.Data.Material.push(o);
                })
                
                $scope.FLMWorkOrderFuel_cboMaterial_Options.dataSource.data(_FLMWorkOrder_Fuel.Data.Material);
            }
        });
    }

    $scope.FLMWorkOrderFuel_numKMStart_Options = {
        format: 'n', spinners: false, culture: 'en-US', min: 0, step: 1, decimals: 2,
        change: function (e) {
            var val = this.value();
            if (val > $scope.Item.KMEnd) {
                $rootScope.Message({
                    Msg: 'Số km bắt đầu phải nhỏ hơn số km cuối.',
                    NotifyType: Common.Message.NotifyType.ERROR
                });
            }
            else {
                var data = $scope.FLMWorkOrderFuel_win_GridOptions.dataSource.data();
                if (Common.HasValue(data)) {
                    $.each(data, function (i, v) {
                        Common.Services.Call($http, {
                            url: Common.Services.url.FLM,
                            method: _FLMWorkOrder_Fuel.URL.FLMReceipt_QuantityPerKMGet,
                            data: { MaterialID: v.MaterialID, VehicleID: $scope.Item.VehicleID },
                            success: function (res) {
                                v.Quantity = ($scope.Item.KMEnd - val) * res[0];
                                v.Price = res[1];
                                v.Amount = v.Price * v.Quantity;
                                if (i == (data.length - 1))
                                    $scope.FLMWorkOrderFuel_win_GridOptions.dataSource.data(data);
                            }
                        });
                    });
                }
            }
        }
    }
    $scope.FLMWorkOrderFuel_numKMEnd_Options = {
        format: 'n2', spinners: false, culture: 'en-US', min: 0, step: 1, decimals: 2,
        change: function (e) {
            var val = this.value();
            if (val < $scope.Item.KMStart) {
                $rootScope.Message({
                    Msg: 'Số km bắt đầu phải nhỏ hơn số km cuối.',
                    NotifyType: Common.Message.NotifyType.ERROR
                });
            }
            else {
                var data = $scope.FLMWorkOrderFuel_win_GridOptions.dataSource.data();
                if (Common.HasValue(data)) {
                    $.each(data, function (i, v) {
                        Common.Services.Call($http, {
                            url: Common.Services.url.FLM,
                            method: _FLMWorkOrder_Fuel.URL.FLMReceipt_QuantityPerKMGet,
                            data: { MaterialID: v.MaterialID, VehicleID: $scope.Item.VehicleID },
                            success: function (res) {
                                v.Quantity = (val - $scope.Item.KMStart) * res[0];
                                v.Price = res[1];
                                v.Amount = v.Price * v.Quantity;
                                if (i == (data.length - 1))
                                    $scope.FLMWorkOrderFuel_win_GridOptions.dataSource.data(data);
                            }
                        });
                    });
                }
            }
        }
    }

    $scope.FLMWorkOrderFuel_numPrice_Options = {
        format: 'n0', spinners: false, culture: 'en-US', min: 0, step: 1, decimals: 0,
        change: function (e) {
            var val = this.value();
            var gridEditElement = $scope.FLMWorkOrderFuel_win_Grid.editable.element;
            var dataItem = $scope.FLMWorkOrderFuel_win_Grid.dataItem(gridEditElement.closest('tr'))
            if (dataItem != null) {
                dataItem.Amount = val * dataItem.Quantity;
            }
        }
    }
    $scope.FLMWorkOrderFuel_numAmount_Options = { format: 'n0', spinners: false, culture: 'en-US', min: 0, step: 1, decimals: 0, }
    $scope.FLMWorkOrderFuel_numQuantity_Options = {
        format: 'n2', spinners: false, culture: 'en-US', min: 0, step: 1, decimals: 2,
        change: function (e) {
            var val = this.value();
            var gridEditElement = $scope.FLMWorkOrderFuel_win_Grid.editable.element;
            var dataItem = $scope.FLMWorkOrderFuel_win_Grid.dataItem(gridEditElement.closest('tr'))
            if (dataItem != null) {
                dataItem.Amount = val * dataItem.Price;
            }
        }
    }

    $scope.FLMWorkOrderFuel_win_GridOptions = {
        dataSource: Common.DataSource.Local({
            model: {
                id: 'ID',
                fields: {
                    MaterialID: { type: 'number' },
                    Quantity: { type: 'number' },
                    Price: { type: 'number' },
                    Amount: { type: 'number', editable: false}
                }
            }
        }),
        selectable: false, reorderable: true, editable: 'incell',
        height: '100%', pageable: false, sortable: false, columnMenu: false, filterable: false, resizable: true,
        columns: [
            {
                title: ' ', width: 50, template: '<a href="/" ng-show="!Item.IsApproved" ng-click="FLMWorkOrderFuel_win_Grid_DeleteClick($event,FLMWorkOrderFuel_win_Grid)" class="k-button" ><i class="fa fa-trash"></i></a>',
                filterable: false, sorable: false
            },
            {
                field: 'MaterialID', title: 'Vật tư', template: "#=MaterialName#", width: 150,
                editor: function (container, options) {
                    $('<input  name="' + options.field + '" kendo-combo-box k-options="FLMWorkOrderFuel_cboMaterial_Options"/>').appendTo(container)
                }
            },
            {
                field: 'Quantity', title: 'Số lượng', width: 150, template: "#=Quantity==null?' ':Common.Number.ToNumber2(Quantity)#",
                editor: function (container, options) {
                    $('<input  name="' + options.field + '" kendo-numeric-text-box style="width:100%" k-options="FLMWorkOrderFuel_numQuantity_Options"/>').appendTo(container)
                }
            },
            {
                field: 'Price', title: 'Đơn giá', width: 150, template: "#=Price==null?' ':Common.Number.ToMoney(Price)#",
                editor: function (container, options) {
                    $('<input  name="' + options.field + '" kendo-numeric-text-box style="width:100%" k-options="FLMWorkOrderFuel_numPrice_Options"/>').appendTo(container)
                }
            },
            {
                field: 'Amount', title: 'Tổng số tiền', width: 150, template: "#=Amount==null?' ':Common.Number.ToMoney(Amount)#",
            },
            { title: ' ', filterable: false, sortable: false }
        ]
    }

    $scope.FLMWorkOrderFuel_win_AddMaterialClick = function ($event, grid) {
        $event.preventDefault();
        var itemNew = { MaterialID: 0, MaterialName: '', Price: 0, Quantity: 0 , Amount: 0};
        if (_FLMWorkOrder_Fuel.Data.Material.length > 0) {
            var item = $.extend(true, {}, _FLMWorkOrder_Fuel.Data.Material[0]);
            itemNew.MaterialID = item.MaterialID;
            itemNew.MaterialName = item.MaterialName;
            Common.Services.Call($http, {
                url: Common.Services.url.FLM,
                method: _FLMWorkOrder_Fuel.URL.FLMReceipt_QuantityPerKMGet,
                data: { MaterialID: itemNew.MaterialID, VehicleID: $scope.Item.VehicleID },
                success: function (res) {
                    if ($scope.Item.KMEnd > $scope.Item.KMStart) 
                        itemNew.Quantity = ($scope.Item.KMEnd - $scope.Item.KMStart) * res[0];
                    else
                        itemNew.Quantity = 0;
                    itemNew.Price = res[1];
                    itemNew.Amount = itemNew.Price * itemNew.Quantity;
                    grid.dataSource.insert(0, itemNew);
                }
            });
        }
        else grid.dataSource.insert(0, itemNew);
    }

    $scope.FLMWorkOrderFuel_win_Grid_DeleteClick = function ($event, grid) {
        $event.preventDefault()
        var tr = $($event.target).closest('tr');
        var item = grid.dataItem(tr);

        if (Common.HasValue(item))
            grid.dataSource.remove(item)
    }

    $scope.FLMWorkOrderFuel_win_SaveClick = function ($event) {
        $event.preventDefault()
        var data = $scope.FLMWorkOrderFuel_win_Grid.dataSource.data();
        var error = false;
        var check = {};
        Common.Data.Each(data, function (o) {
            if (Common.HasValue(check[o.MaterialID])) { error = true; }
            else check[o.MaterialID] = o;
        })
        if (error)
            $rootScope.Message({ Msg: 'Chi tiết vật tư bị trùng', Type: Common.Message.Type.Alert, NotifyType: Common.Message.NotifyType.ERROR });
        else if (data.length == 0)
            $rootScope.Message({ Msg: 'Chưa chọn chi tiết vật tư', Type: Common.Message.Type.Notify, NotifyType: Common.Message.NotifyType.ERROR });
        else if (!Common.HasValue($scope.Item.VehicleID) || !$scope.Item.VehicleID > 0)
            $rootScope.Message({ Msg: 'Chưa chọn xe', Type: Common.Message.Type.Notify, NotifyType: Common.Message.NotifyType.ERROR });
        else if (!Common.HasValue($scope.Item.DateReceipt))
            $rootScope.Message({ Msg: 'Chưa chọn Ngày cấp phiếu', Type: Common.Message.Type.Notify, NotifyType: Common.Message.NotifyType.ERROR });
        else {
            var checkQuantity = true;
            angular.forEach(data, function (item, idx) {
                if (item.Quantity <= 0) {
                    $rootScope.Message({ Msg: 'Số lượng vật tư phải lớn hơn 0', NotifyType: Common.Message.NotifyType.ERROR });
                    checkQuantity = false;
                }
                if (item.Price <= 0) {
                    $rootScope.Message({ Msg: 'Giá vật tư phải lớn hơn 0', NotifyType: Common.Message.NotifyType.ERROR });
                    checkQuantity = false;
                }
            });

            if (checkQuantity) {
                $scope.Item.ListDetail = data;
                $rootScope.IsLoading = true;
                Common.Services.Call($http, {
                    url: Common.Services.url.FLM,
                    method: _FLMWorkOrder_Fuel.URL.Save,
                    data: { item: $scope.Item },
                    success: function (res) {
                        $rootScope.IsLoading = false;
                        $state.go("main.FLMInputFuel.Index")
                        $rootScope.Message({ Msg: 'Thành công', Type: Common.Message.Type.Notify, NotifyType: Common.Message.NotifyType.SUCCESS });
                    }
                });
            }
        }
    }

    $scope.FLMWorkOrderFuel_win_PrintClick = function ($event) {
        $event.preventDefault();
        location.href = "Report.aspx#/WorkOrderFuel/" + Common.Auth.HeaderKey + "/" + $rootScope.Default_UserName + "/" + _FLMWorkOrder_Fuel.Params.ID;
    }

    $scope.Back_Click = function ($event) {
        $event.preventDefault();
        $state.go("main.FLMInputFuel.Index", _FLMWorkOrder_Fuel.Params)
    }
}])