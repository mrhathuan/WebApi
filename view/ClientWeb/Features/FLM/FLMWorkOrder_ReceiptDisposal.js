/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />
/// <reference path="~/Scripts/Default.js" />


var _FLMWO_ReceiptDisposal = {
    URL: {
        Get: 'FLMReceipt_DisposalGet',
        Save: 'FLMReceipt_DisposalSave',

        Vehicle_List: 'FLMReceipt_DisposalVehicleList',
        EQM_List: 'FLMReceipt_DisposalEQMList',

        GetEQMByVehicle: 'FLMReceipt_DisposalEQMByVehicle'
    },
    Data: {
        Vehicle: [],
        EQM: [],
    },
    Params: {
        ID: -1,
    },
}

angular.module('myapp').controller('FLMWorkOrder_ReceiptDisposalCtrl', ['$rootScope', '$scope', '$http', '$location', '$state', '$timeout', function ($rootScope, $scope, $http, $location, $state, $timeout) {
    Common.Log('FLMWorkOrder_ReceiptDisposalCtrl');
    $rootScope.IsLoading = false;
    _FLMWO_ReceiptDisposal.Params = $.extend({}, true, $state.params);
    $scope.ListVehicleID = [];
    $scope.ListEQMID = [];

    $scope.Item = null;

    $scope.ListAssetExist = [];

    $scope.FLMWOReceiptDisposal_splitter_Options = {
        orientation: "vertical",
        panes: [
            { collapsible: false, resizable: false, size: "50%", min: '270px' },
            { collapsible: false, resizable: false }
        ]
    }

    Common.Services.Call($http, {
        url: Common.Services.url.FLM,
        method: _FLMWO_ReceiptDisposal.URL.Get,
        data: { receiptID: _FLMWO_ReceiptDisposal.Params.ID },
        success: function (res) {
            $scope.Item = res;
            
                $scope.Vehicle_Grid.dataSource.data(res.ListVehicle);
                $scope.EQM_Grid.dataSource.data(res.ListEquipment);
        }
    });

    $scope.mulVehicle_Options = {
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    RegNo: { type: 'string' },
                    RegWeight: { type: 'string' }
                }
            }
        }),
        valuePrimitive: true,
        dataTextField: "RegNo",
        dataValueField: "ID",
        placeholder: "Chọn xe...",
        filter: "contains",
        ignoreCase: true,
        highlightFirst: true,
        autoClose: false,
        itemTemplate: '<span>#= RegNo #</span><span style="float:right;">#= RegWeight #</span>',
        headerTemplate: '<strong><span> Xe </span><span style="float:right;"> Tấn </span></strong>',
        select: function (e) {
            var mul = this;
            var vehicle = mul.dataItem(e.item);
            if (vehicle != null) {
                if ($scope.ListAssetExist.indexOf(vehicle.ID) < 0) {
                    $rootScope.IsLoading = true;
                    Common.Services.Call($http, {
                        url: Common.Services.url.FLM,
                        method: _FLMWO_ReceiptDisposal.URL.GetEQMByVehicle,
                        data: {vehicleID:vehicle.ID},
                        success: function (res) {
                            Common.Log("Get equipment of vedhicleID: " + vehicle.ID + " done!")
                            var data = $scope.Vehicle_Grid.dataSource.data();
                            vehicle.ListEquipment = res;
                            vehicle.WarrantyEnd = Common.Date.FromJson(vehicle.WarrantyEnd);
                            vehicle.DepreciationStart = Common.Date.FromJson(vehicle.DepreciationStart);
                            data.push(vehicle);
                            $scope.Vehicle_Grid.dataSource.data(data);
                            $scope.ListAssetExist.push(vehicle.ID);
                            $rootScope.IsLoading = false;
                        },
                        error: function () {
                            $rootScope.IsLoading = false;
                        }
                    });
                    
                }
                else {
                    $rootScope.Message({ Msg: 'Không được chọn trùng xe', NotifyType: Common.Message.NotifyType.ERROR });
                }
                $timeout(function () {
                    $scope.ListVehicleID = [];
                }, 10)
            }
        }
    };


    Common.Services.Call($http, {
        url: Common.Services.url.FLM,
        method: _FLMWO_ReceiptDisposal.URL.Vehicle_List,
        data: {},
        success: function (res) {
            Common.Log("load veihicle done")
            $scope.mulVehicle_Options.dataSource.data(res);
        }
    });

    $scope.mulEQM_Options = {
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'PartID',
                fields: {
                    PartID: { type: 'number' },
                    PartNo: { type: 'string' }
                }
            }
        }),
        valuePrimitive: true,
        dataTextField: "PartNo",
        dataValueField: "PartID",
        placeholder: "Chọn thiết bị...",
        filter: "contains",
        ignoreCase: true,
        highlightFirst: true,
        autoClose: false,
        enable: false,
        select: function (e) {
            var mul = this;
            var eqm = mul.dataItem(e.item);
            if (eqm != null) {
                if ($scope.ListAssetExist.indexOf(eqm.PartID) < 0) {
                    var data = $scope.EQM_Grid.dataSource.data();
                    data.push(eqm);
                    $scope.EQM_Grid.dataSource.data(data);
                    $scope.ListAssetExist.push(eqm.PartID);
                }
                else {
                    $rootScope.Message({ Msg: 'Không được chọn trùng xe', NotifyType: Common.Message.NotifyType.ERROR });
                }
                $timeout(function () {
                    $scope.ListVehicleID = [];
                }, 10)
            }
        }
    };


    Common.Services.Call($http, {
        url: Common.Services.url.FLM,
        method: _FLMWO_ReceiptDisposal.URL.EQM_List,
        data: {},
        success: function (res) {
            Common.Log("eqm data done")
            $rootScope.IsLoading = false;
            $scope.mulEQM_Options.dataSource.data(res)
        }
    });
    //#region

    $scope.changeType = function ($event, value) {
        switch (value) {
            default:
                break;
            case 1:
                $scope.Amount = 0;
                $scope.Item.IsVehicle = true;
                $scope.EQM_GridOptions.dataSource.data([]);
                $scope.ListAssetExist = [];
                break;
            case 2:
                $scope.Amount = 0;
                $scope.Item.IsVehicle = false;
                $scope.Vehicle_GridOptions.dataSource.data([]);
                $scope.ListAssetExist = [];
                $scope.hasVehicle = false;
                break;
        }
    }

    $scope.Vehicle_GridOptions = {
        dataSource: Common.DataSource.Local({
            model: {
                id: 'ID',
                fields: {
                    ID: { field: 'ID', type: 'number', editable: false, nullable: false },
                    RegNo: { type: 'string' },
                    IsChoose: { type: 'boolean' },
                    WarrantyEnd: { type: 'date' },
                    DepreciationStart: { type: 'date' }
                }
            }
        }),
        height: '100%', pageable: false, sortable: true, columnMenu: false, filterable: { mode: 'row' }, resizable: true, editable: false,
        dataBound: function () {
            Common.Log("Vehicle Grid Databound")
        },
        columns: [
            {
                title: ' ', width: 85, template: '<a href="/" ng-click="Vehicle_Grid_DeleteClick($event,dataItem,Vehicle_Grid)" class="k-button" ><i class="fa fa-trash"></i></a>' +
                    '<a href="/" ng-click="Vehicle_Grid_DetailClick($event,dataItem,Detail_win)" class="k-button" ><i class="fa fa-file"></i></a>',
                filterable: false, sorable: false
            },
            { field: 'RegNo', title: "Xe", width: 150, filterable: { cell: { operator: 'contains', showOperators: false } } },
            {
                field: 'WarrantyEnd', title: 'Hạn bảo hành', width: 150,
                template: "#=Common.Date.FromJsonDDMMYY(WarrantyEnd)#", headerAttributes: { style: "text-align: center" },
                filterable: { cell: { template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DDMMYY }); }, operator: 'equal', showOperators: false } },
            },
            { field: 'WarrantyPeriod', title: "Thời gian bảo hành", width: 150, filterable: { cell: { operator: 'equal', showOperators: false } } },
            { field: 'YearOfProduction', title: "Năm sản xuất", width: 150, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'Manufactor', title: "Nhà sản xuất", width: 150, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'CurrentValue', title: "Giá trị hiện tại", width: 150, filterable: { cell: { operator: 'equal', showOperators: false } } },
            { field: 'DepreciationPeriod', title: "T/g khấu hao", width: 150, filterable: { cell: { operator: 'equal', showOperators: false } } },
            {
                field: 'DepreciationStart', title: 'T/g bắt đầu tính KH', width: 150,
                template: "#=Common.Date.FromJsonDDMMYY(DepreciationStart)#", headerAttributes: { style: "text-align: center" },
                filterable: { cell: { template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DDMMYY }); }, operator: 'equal', showOperators: false } },
            },
            { field: 'BaseValue', title: "Giá trị thanh lí", width: 150, filterable: { cell: { operator: 'equal', showOperators: false } } },
            { field: 'TypeOfAssetName', title: "Loại Xe", width: 150, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { title: ' ', filterable: false, sortable: false }
        ]
    }

    $scope.Detail_GridOptions = {
        dataSource: Common.DataSource.Local({
            model: {
                id: 'PartID',
                fields: {
                    PartID: { type: 'number' },
                    WarrantyEnd: { type: 'date' },
                    DepreciationStart: { type: 'date' }
                }
            }
        }),
        height: '100%', pageable: false, sortable: true, columnMenu: false, filterable: { mode: 'row' }, resizable: true, editable: false,
        columns: [
            { field: 'PartName', title: "Mã thiết bị", width: 150, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'PartNo', title: "Mã thiết bị", width: 150, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'GroupOfEquipmentName', title: "Nhóm thiết bị", width: 150, filterable: { cell: { operator: 'contains', showOperators: false } } },
            {
                field: 'WarrantyEnd', width: 150, template: "#=WarrantyEnd==null?' ':Common.Date.FromJsonDDMMYY(WarrantyEnd)#", headerAttributes: { style: "text-align: center" },
                filterable: { cell: { template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DDMMYY }); }, operator: 'equal', showOperators: false } },
            },
            { field: 'WarrantyPeriod', title: "Thời gian bảo hành", width: 150, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'YearOfProduction', title: "Năm sản xuất", width: 150, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'Manufactor', title: "Nhà sản xuất", width: 150, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'CurrentValue', title: "Giá trị hiện tại", width: 150, filterable: { cell: { operator: 'equal', showOperators: false } } },
            { field: 'DepreciationPeriod', title: "T/g khấu hao", width: 150, filterable: { cell: { operator: 'equal', showOperators: false } } },
            {
                field: 'DepreciationStart', width: 150, title: 'T/g bắt đầu tính KH', template: "#=DepreciationStart==null?' ':Common.Date.FromJsonDDMMYY(DepreciationStart)#", headerAttributes: { style: "text-align: center" },
                filterable: { cell: { template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DDMMYY }); }, operator: 'equal', showOperators: false } },
            },
            { field: 'BaseValue', title: "Giá trị ban đầu", width: 150, filterable: { cell: { operator: 'equal', showOperators: false } } },
            { title: ' ', filterable: false, sortable: false }
        ]
    }

    $scope.Vehicle_Grid_DeleteClick = function ($event, data, grid) {
        $event.preventDefault()
        $rootScope.Message({
            Type: Common.Message.Type.Confirm,
            NotifyType: Common.Message.NotifyType.SUCCESS,
            Title: 'Thông báo',
            Msg: 'Bạn muốn xóa dữ liệu đã chọn?',
            Close: null,
            Ok: function () {
                grid.dataSource.remove(data);
            }
        })
    }

    $scope.Vehicle_Grid_DetailClick = function ($event, data, win) {
        $event.preventDefault()
        win.center();
        win.open();
        $scope.Detail_Grid.dataSource.data(data.ListEquipment)
    }

    $scope.EQM_GridOptions = {
        dataSource: Common.DataSource.Local({
            model: {
                id: 'PartID',
                fields: {
                    PartID: { type: 'number' },
                    WarrantyEnd: { type: 'date' },
                    DepreciationStart: { type: 'date' }
                }
            }
        }),
        height: '100%', pageable: false, sortable: true, columnMenu: false, filterable: { mode: 'row' }, resizable: true, editable: false,
        columns: [
            {
                title: ' ', width: 45, template: '<a href="/" ng-click="EQM_Grid_DeleteClick($event,dataItem,EQM_Grid)" class="k-button" ><i class="fa fa-trash"></i></a>',
                filterable: false, sorable: false
            },
            { field: 'PartName', title: "Mã thiết bị", width: 150, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'PartNo', title: "Mã thiết bị", width: 150, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'GroupOfEquipmentName', title: "Nhóm thiết bị", width: 150, filterable: { cell: { operator: 'contains', showOperators: false } } },
            {
                field: 'WarrantyEnd', width: 150, title: 'Hạn bảo hành', template: "#=WarrantyEnd==null?' ':Common.Date.FromJsonDDMMYY(WarrantyEnd)#", headerAttributes: { style: "text-align: center" },
                filterable: { cell: { template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DDMMYY }); }, operator: 'equal', showOperators: false } },
            },
            { field: 'WarrantyPeriod', title: "Thời gian bảo hành", width: 150, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'YearOfProduction', title: "Năm sản xuất", width: 150, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'Manufactor', title: "Nhà sản xuất", width: 150, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'CurrentValue', title: "Giá trị hiện tại", width: 150, filterable: { cell: { operator: 'equal', showOperators: false } } },
            { field: 'DepreciationPeriod', title: "T/g khấu hao", width: 150, filterable: { cell: { operator: 'equal', showOperators: false } } },
            {
                field: 'DepreciationStart', width: 150, title: 'T/g bắt đầu tính KH', template: "#=DepreciationStart==null?' ':Common.Date.FromJsonDDMMYY(DepreciationStart)#", headerAttributes: { style: "text-align: center" },
                filterable: { cell: { template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DDMMYY }); }, operator: 'equal', showOperators: false } },
            },
            { field: 'BaseValue', title: "Giá trị ban đầu", width: 150, filterable: { cell: { operator: 'equal', showOperators: false } } },
            { title: ' ', filterable: false, sortable: false }
        ]
    }

    $scope.EQM_Grid_DeleteClick = function ($event, data, grid) {
        $event.preventDefault()
        $rootScope.Message({
            Type: Common.Message.Type.Confirm,
            NotifyType: Common.Message.NotifyType.SUCCESS,
            Title: 'Thông báo',
            Msg: 'Bạn muốn xóa dữ liệu đã chọn?',
            Close: null,
            Ok: function () {
                grid.dataSource.remove(data);
            }
        })
    }

    $scope.FLMWOReceiptDisposal_SaveClick = function ($event) {
        $event.preventDefault();
        var vehiclelst = $scope.Vehicle_Grid.dataSource.data();
        var eqmlst = $scope.EQM_Grid.dataSource.data();

        var error = false;
        if (!Common.HasValue($scope.Item.DateReceipt)) {
            $rootScope.Message({ Msg: 'Chưa chọn Ngày cấp phiếu', Type: Common.Message.Type.Notify, NotifyType: Common.Message.NotifyType.ERROR });
            error = true;
        }
        if ($scope.Item.IsVehicle) {
            if (vehiclelst.length > 0) {
                var hasV = false;
                var check = {};
                Common.Data.Each(eqmlst, function (o) {
                    if (Common.HasValue(check[o.ID])) { error = true; hasV = true; }
                    else check[o.ID] = o;
                })
                if (hasV) {
                    $rootScope.Message({ Msg: 'Xe thanh lí bị trùng', Type: Common.Message.Type.Notify, NotifyType: Common.Message.NotifyType.ERROR });
                }
                $scope.Item.ListVehicle = vehiclelst;
                $scope.Item.ListEquipment = [];
            }
            else {
                $rootScope.Message({ Msg: 'Chưa chọn xe', Type: Common.Message.Type.Notify, NotifyType: Common.Message.NotifyType.ERROR });
                error = true;
            }
        } else {
            if (eqmlst.length > 0) {
                var hasEQM = false;
                var check = {};
                Common.Data.Each(eqmlst, function (o) {
                    if (Common.HasValue(check[o.PartID])) { error = true; hasEQM = true; }
                    else check[o.PartID] = o;
                })
                if (hasEQM) {
                    $rootScope.Message({ Msg: 'Thiết bị thanh lí bị trùng', Type: Common.Message.Type.Notify, NotifyType: Common.Message.NotifyType.ERROR });
                }
                $scope.Item.ListEquipment = eqmlst;
                $scope.Item.ListVehicle = [];
            }
            else {
                $rootScope.Message({ Msg: 'Chưa chọn thiết bị', Type: Common.Message.Type.Notify, NotifyType: Common.Message.NotifyType.ERROR });
                error = true;
            }
        }

        if (!error) {
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.FLM,
                method: _FLMWO_ReceiptDisposal.URL.Save,
                data: { item: $scope.Item },
                success: function (res) {
                    $rootScope.IsLoading = false;
                    $rootScope.Message({ Msg: 'Thành công', Type: Common.Message.Type.Notify, NotifyType: Common.Message.NotifyType.SUCCESS });
                    $state.go("main.FLMInputFuel.Index");
                }
            });
        }
    }

    $scope.Back_Click = function ($event) {
        $event.preventDefault();
        $state.go("main.FLMInputFuel.Index");
    }
    $scope.Win_CloseClick = function ($event,win) {
        $event.preventDefault();
        win.close();
    }
}])