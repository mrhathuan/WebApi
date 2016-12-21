/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />
/// <reference path="~/Scripts/Default.js" />


var _FLMWorkOrder_Tranfer = {
    URL: {
        Get: 'FLMReceipt_TranfersGet',
        Save: 'FLMReceipt_TranfersSave',

        List_EQMByStock: 'FLMReceipt_TranfersEQMByStock',
        List_EQMByVehicle: 'FLMReceipt_TranfersEQMByVehicle',

        Stock_List: "FLMTransferReceipt_StockList",
        Vehicle_List: 'FLMVehicle_List',
    },
    Data: {
        Material: []
    },
    Params: {
        ID: -1,
    },
}

angular.module('myapp').controller('FLMWorkOrder_TranferCtrl', ['$rootScope', '$scope', '$http', '$location', '$state', '$timeout', function ($rootScope, $scope, $http, $location, $state, $timeout) {
    Common.Log('FLMWorkOrder_TranferCtrl');
    $rootScope.IsLoading = false;
    _FLMWorkOrder_Tranfer.Params = $.extend({}, true, $state.params);

    $scope.isNew = true;
    if (_FLMWorkOrder_Tranfer.Params.ID > 0) {
        $scope.isNew = false;
    }
    $scope.Item = null;
    $scope.isVehicle = 1;
    $scope.isVehicleTo = 1;
    $scope.GridHasChoose = false;

    Common.Services.Call($http, {
        url: Common.Services.url.FLM,
        method: _FLMWorkOrder_Tranfer.URL.Get,
        data: { ID: _FLMWorkOrder_Tranfer.Params.ID },
        success: function (res) {
            $scope.Item = res;
            $scope.LoadVehicle();
            $scope.LoadStock();
            if ($scope.Item.ID > 0) {
                if ($scope.Item.VehicleID > 0) {
                    $scope.isVehicle = 1;
                    $scope.Item.StockID = -1;
                    
                } else if ($scope.Item.StockID > 0) {
                    $scope.isVehicle = 2;
                    $scope.Item.VehicleID = -1;
                }
                if ($scope.Item.VehicleToID > 0) {
                    $scope.isVehicleTo = 1;
                    $scope.Item.StockToID = -1;

                } else if ($scope.Item.StockToID > 0) {
                    $scope.isVehicleTo = 2;
                    $scope.Item.VehicleToID = -1;
                }
                Common.Data.Each($scope.Item.ListDetail, function (o) {
                    o['IsChoose'] = false;
                })
                $scope.FLMWOTranfer_GridOptions.dataSource.data($scope.Item.ListDetail);
                $("#cboFLMVehicle").data("kendoComboBox").enable(false);
                $("#cboFLMStock").data("kendoComboBox").enable(false);
                $("#cboFLMVehicleTo").data("kendoComboBox").enable(false);
                $("#cboFLMStockTo").data("kendoComboBox").enable(false);
                var grid = $("#FLMWOTranfer_Grid").data("kendoGrid");
                grid.hideColumn(0);
                $scope.GridHasChoose = true;
            } else {
                $scope.Item.VehicleID = -1;
                $scope.Item.StockID = -1;
                $scope.Item.StockToID = -1;
                $scope.Item.VehicleToID = -1;
            }
        }
    });

    //#region
    $scope.FLMWOTranfer_splitter_Options = {
        orientation: "vertical",
        panes: [
            { collapsible: false, resizable: false, size: "50%" },
            { collapsible: false, resizable: false }
        ]
    }

    $scope.FLMWOTranfer_GridOptions = {
        dataSource: Common.DataSource.Local({
            model: {
                id: 'ID',
                fields: {
                    PartNo: { type: 'string' },
                    PartName: { type: 'string' },
                    IsChoose: { type: 'boolean'},
                }
            }
        }),
        height: '100%', pageable: false, sortable: true, columnMenu: false, filterable: { mode: 'row' }, resizable: true, editable: false,
        columns: [
            {
                title: ' ', width: '35px',
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,FLMWOTranfer_Grid,FLMWOTranfer_GridChoose_Change)" />',
                headerAttributes: { style: 'text-align: center;' }, attributes: { style: 'text-align: center;' },
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,FLMWOTranfer_Grid,FLMWOTranfer_GridChoose_Change)" />',
                filterable: false, sortable: false
            },
            { field: 'PartNo', title: "Mã thiết bị", width: 150, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'PartName', title: "Tên thiết bị", width: 150, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { title: ' ', filterable: false, sortable: false }
        ]
    }

    $scope.FLMWOTranfer_GridChoose_Change = function ($event, grid, hasChoose) {
        $scope.GridHasChoose = hasChoose;
    }
    //#region cbx
    $scope.cboFLMVehicle_Options = {
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
            var val = this.value();
            var text = this.text();
            $scope.Item.LocationFrom = text;
            Common.Services.Call($http, {
                url: Common.Services.url.FLM,
                method: _FLMWorkOrder_Tranfer.URL.List_EQMByVehicle,
                data: { vehicleID: val },
                success: function (res) {
                    Common.Data.Each(res, function (o) {
                        o['IsChoose'] = false;
                    })
                    $scope.FLMWOTranfer_GridOptions.dataSource.data(res);
                    $scope.GridHasChoose = false;
                }
            })
        }

    };
    $scope.LoadVehicle = function () {
        Common.Services.Call($http, {
            url: Common.Services.url.FLM,
            method: _FLMWorkOrder_Tranfer.URL.Vehicle_List,
            data: {},
            success: function (res) {
                var item = { ID: -1, Code: 'Chọn xe' };
                res.Data.unshift(item);
                $scope.cboFLMVehicle_Options.dataSource.data(res.Data)
            }
        });
    }

    $scope.cboFLMStock_Options = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true, minLength: 3, enable: false,
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
            var val = this.value();
            var text = this.text();
            $scope.Item.LocationFrom = text;
            Common.Services.Call($http, {
                url: Common.Services.url.FLM,
                method: _FLMWorkOrder_Tranfer.URL.List_EQMByStock,
                data: { stockID: val },
                success: function (res) {
                    Common.Data.Each(res, function (o) {
                        o['IsChoose'] = false;
                    })
                    $scope.FLMWOTranfer_GridOptions.dataSource.data(res);
                    $scope.GridHasChoose = false;
                }
            })
        }

    };

    $scope.LoadStock = function () {
        Common.Services.Call($http, {
            url: Common.Services.url.FLM,
            method: _FLMWorkOrder_Tranfer.URL.Stock_List,
            success: function (res) {
                if (Common.HasValue(res)) {
                    var item = { ID: -1, StockName: 'Chọn kho' };
                    res.Data.unshift(item);
                    $scope.cboFLMStock_Options.dataSource.data(res.Data);
                }
            }
        });
    }

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
            var text = this.text();
            $scope.Item.LocationTo = text;
        }

    };

    Common.Services.Call($http, {
        url: Common.Services.url.FLM,
        method: _FLMWorkOrder_Tranfer.URL.Vehicle_List,
        data: {},
        success: function (res) {
            var item = { ID: -1, Code: 'Chọn xe' };
            res.Data.unshift(item);
            $scope.cboFLMVehicleTo_Options.dataSource.data(res.Data)
        }
    });

    $scope.cboFLMStockTo_Options = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true, minLength: 3, enable: false,
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
            var text = this.text();
            $scope.Item.LocationTo = text;
        }

    };

    Common.Services.Call($http, {
        url: Common.Services.url.FLM,
        method: _FLMWorkOrder_Tranfer.URL.Stock_List,
        success: function (res) {
            if (Common.HasValue(res)) {
                var item = { ID: -1, StockName: 'Chọn kho' };
                res.Data.unshift(item);
                $scope.cboFLMStockTo_Options.dataSource.data(res.Data);
                $scope.GridHasChoose = false;
            }
        }
    });
    //#endregion
    $scope.FLMWOTranfer_changeType = function ($event, value) {
        switch (value) {
            default:
                break;
            case 1:
                $("#cboFLMVehicle").data("kendoComboBox").enable();
                $("#cboFLMStock").data("kendoComboBox").enable(false);
                Common.Services.Call($http, {
                    url: Common.Services.url.FLM,
                    method: _FLMWorkOrder_Tranfer.URL.List_EQMByVehicle,
                    data: { vehicleID: $scope.Item.VehicleID },
                    success: function (res) {
                        Common.Data.Each(res, function (o) {
                            o['IsChoose'] = false;
                        })
                        $scope.FLMWOTranfer_GridOptions.dataSource.data(res);
                    }
                })
                $scope.Item.StockID = -1;
                break;
            case 2:
                $("#cboFLMVehicle").data("kendoComboBox").enable(false);
                $("#cboFLMStock").data("kendoComboBox").enable();
                Common.Services.Call($http, {
                    url: Common.Services.url.FLM,
                    method: _FLMWorkOrder_Tranfer.URL.List_EQMByStock,
                    data: { stockID: $scope.Item.StockID },
                    success: function (res) {
                        Common.Data.Each(res, function (o) {
                            o['IsChoose'] = false;
                        })
                        $scope.FLMWOTranfer_GridOptions.dataSource.data(res);
                    }
                })
                $scope.Item.VehicleID = -1;
                break;
        }
    }

    $scope.FLMWOTranfer_changeTypeTo = function ($event, value) {
        switch (value) {
            default:
                break;
            case 1:
                $("#cboFLMVehicleTo").data("kendoComboBox").enable();
                $("#cboFLMStockTo").data("kendoComboBox").enable(false);
                $scope.Item.StockToID = -1;
                break;
            case 2:
                $("#cboFLMVehicleTo").data("kendoComboBox").enable(false);
                $("#cboFLMStockTo").data("kendoComboBox").enable();
                $scope.Item.VehicleToID = -1;
                break;
        }
    }

    $scope.FLMWOTranfer_SaveClick = function ($event) {
        $event.preventDefault();
        var checked = true;
        
        if ($scope.Item.VehicleID <= 0 && $scope.isVehicle == 1) {
            $rootScope.Message({ Msg: 'Chưa chọn xe điều chuyển từ', Type: Common.Message.Type.Notify, NotifyType: Common.Message.NotifyType.ERROR });
            checked = false;
        }
        if ($scope.isVehicle == 1 && $scope.isVehicleTo == 1 && ($scope.Item.VehicleID == $scope.Item.VehicleToID)) {
            $rootScope.Message({ Msg: 'Điều chuyến trùng xe đến', Type: Common.Message.Type.Notify, NotifyType: Common.Message.NotifyType.ERROR });
            checked = false;
        }

        if ($scope.Item.StockID <= 0 && $scope.isVehicle == 2) {
            $rootScope.Message({ Msg: 'Chưa chọn kho điều chuyển từ', Type: Common.Message.Type.Notify, NotifyType: Common.Message.NotifyType.ERROR });
            checked = false;
        } 

        if ($scope.isVehicle == 2 && $scope.isVehicleTo == 2 && ($scope.Item.StockID == $scope.Item.StockToID)) {
            $rootScope.Message({ Msg: 'Điều chuyến trùng xe đến', Type: Common.Message.Type.Notify, NotifyType: Common.Message.NotifyType.ERROR });
            checked = false;
        }

        if ($scope.Item.VehicleToID <= 0 && $scope.isVehicleTo == 1) {
            $rootScope.Message({ Msg: 'Chưa chọn xe điều chuyển đến', Type: Common.Message.Type.Notify, NotifyType: Common.Message.NotifyType.ERROR });
            checked = false;
        }
        if ($scope.Item.StockToID <= 0 && $scope.isVehicleTo == 2) {
            $rootScope.Message({ Msg: 'Chưa chọn kho điều chuyển đến', Type: Common.Message.Type.Notify, NotifyType: Common.Message.NotifyType.ERROR });
            checked = false;
        }

        if ($scope.GridHasChoose == false) {
            $rootScope.Message({ Msg: 'Chưa chọn thiết bị điều chuyển', Type: Common.Message.Type.Notify, NotifyType: Common.Message.NotifyType.ERROR });
            checked = false;
        }
        if (!Common.HasValue($scope.Item.DateReceipt)) {
            $rootScope.Message({ Msg: 'Chưa chọn Ngày cấp phiếu', Type: Common.Message.Type.Notify, NotifyType: Common.Message.NotifyType.ERROR });
            checked = false;
        }

        if(checked == true){
            var data = $scope.FLMWOTranfer_Grid.dataSource.data();

            var listDetail = [];
            angular.forEach(data, function (item, idx) {
                if (item.IsChoose == true) {
                    listDetail.push(item);
                }
            });
            if ($scope.isNew) {
                $scope.Item.ListDetail = listDetail;
            }
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.FLM,
                method: _FLMWorkOrder_Tranfer.URL.Save,
                data: { item: $scope.Item },
                success: function (res) {
                    $rootScope.IsLoading = false;
                    $rootScope.Message({ Msg: 'Thành công', Type: Common.Message.Type.Notify, NotifyType: Common.Message.NotifyType.SUCCESS });
                    $state.go("main.FLMInputFuel.Index", _FLMWorkOrder_Tranfer.Params);
                }
            });
        }
    }


    $scope.Back_Click = function ($event) {
        $event.preventDefault();
        $state.go("main.FLMInputFuel.Index", _FLMWorkOrder_Tranfer.Params);
    }
}])