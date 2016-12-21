/// <reference path="~/Scripts/jquery-1.9.1.intellisense.js" />
/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />

var _FLMTransferReceipt = {
    URL: {
        StockList: "FLMTransferReceipt_StockList",
        Read: "FLMTransferReceipt_EQMByStock",
        Get_TransferReceipt: 'FLMTransferReceipt_EQMGet',
        Get_Vehicle_List: 'FLMVehicle_List',
        Save_EQM: 'FLMTransferReceipt_EQMSave',

        Excel_Export: 'FLMTransferReceipt_Excel_Export',
        Excel_Check: 'FLMTransferReceipt_Excel_Check',
        Excel_Save: 'FLMTransferReceipt_Excel_Save',
    },
    Data: {
        Location: {}
    }
}

angular.module('myapp').controller('FLMTransferReceipt_IndexCtrl', ['$rootScope', '$scope', '$http', '$location', '$state', '$timeout', function ($rootScope, $scope, $http, $location, $state, $timeout) {
    Common.Log('FLMTransferReceipt_IndexCtrl');
    $rootScope.IsLoading = false;

    $scope.ItemFilter = {};
    $scope.ItemFilter.StockID = 0;
    $scope.EQMItem = {};
    $scope.EQMItem.isVehicle = true;
    $scope.EQMItem.TransferType = 1;
    $scope.isTransferReceipt_Vehicle = true;
    $scope.isNewTransferReceipt = true;

    $scope.FLMTransferReceipt_GridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.FLM,
            method: _FLMTransferReceipt.URL.Read,
            readparam: function () { return { stockID: $scope.ItemFilter.StockID } },
            model: {
                id: 'PartID',
                fields: {
                    PartID: { type: 'number' },
                    PartName: { type: 'string' },
                    StockName:{type:'string'},
                }
            }
        }),
        height: '100%', groupable: false, pageable: true, sortable: true, columnMenu: false, resizable: true,
        selectable: 'row', filterable: { mode: 'row' }, reorderable: true,
        columns: [
            {
                title: ' ', width: '40px',
                template: '<a href="/" ng-click="Edit_Click($event,FLMTransferReceipt_win,FLMTransferReceipt_Grid)"><i class="fa fa-pencil"></i></a>',
                filterable: false, sortable: false
            },
            {
                field: 'StockName', title: 'Kho', width: '125px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'PartNo', title: '{{RS.FLMAsset.No}}', width: '125px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'PartName', title: '{{RS.FLMAsset.Name}}', width: '125px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'GroupOfEquipmentName', title: '{{RS.FLMAsset.GroupOfEquipmentName}}', width: '125px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'YearOfProduction', title: '{{RS.FLMAsset.YearOfProduction}}', width: '125px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'Manufactor', title: '{{RS.FLMAsset.Manufactor}}', width: '125px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'BaseValue', title: '{{RS.FLMAsset.BaseValue}}', width: '125px',
                filterable: { cell: { operator: 'eq', showOperators: false } },
            },
            {
                field: 'CurrentValue', title: '{{RS.FLMAsset.CurrentValue}}', width: '125px',
                filterable: { cell: { operator: 'eq', showOperators: false } },
            },
            {
                field: 'DepreciationPeriod', title: 'Thời gian K/H', width: '125px',
                filterable: { cell: { operator: 'eq', showOperators: false } },
            },
            {
                field: 'DepreciationStart', title: 'Bắt đầu K/H', width: '125px',
                template: "#=DepreciationStart==null?' ':Common.Date.FromJsonDMY(DepreciationStart)#",
                filterable: { cell: { template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); }, operator: 'equal', showOperators: false } },
            },
            {
                field: 'Specification', title: '{{RS.FLMAsset.Specification}}', width: '125px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'WarrantyPeriod', title: 'Thời gian bản hànhs', width: '125px',
                filterable: { cell: { operator: 'eq', showOperators: false } },
            },
            {
                field: 'WarrantyEnd', title: '{{RS.FLMAsset.WarrantyEnd}}', width: '125px',
                template: "#=WarrantyEnd==null?' ':Common.Date.FromJsonDMY(WarrantyEnd)#",
                filterable: { cell: { template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); }, operator: 'equal', showOperators: false } },
            },
            {
                field: 'RemainValue', title: '{{RS.FLMAsset.RemainValue}}', width: '125px',
                filterable: { cell: { operator: 'eq', showOperators: false } },
            }
        ]
    }

    $scope.cboFLMStock_Options = {
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
            //$scope.FINFreightAuditVendor_gridOptions.dataSource.data([]);
        }

    };

    Common.Services.Call($http, {
        url: Common.Services.url.FLM,
        method: _FLMTransferReceipt.URL.StockList,
        success: function (res) {
            if (Common.HasValue(res)) {
                var item = {};
                item.StockName = "Tất cả";
                item.ID = 0;
                res.Data.push(item);
                $scope.cboFLMStock_Options.dataSource.data(res.Data);
            }
        }
    });

    $scope.Search_Click = function ($event) {
        $event.preventDefault();

        $scope.FLMTransferReceipt_GridOptions.dataSource.read();
    };

    $scope.FLMTransferReceipt_New_Click = function ($event, win) {
        $event.preventDefault();
        $scope.isNewTransferReceipt = true;
        $scope.LoadItem(win, 0);
    }

    $scope.LoadItem = function (win, id) {
        $scope.DistributorPartnerID = id;
        Common.Services.Call($http, {
            url: Common.Services.url.FLM,
            method: _FLMTransferReceipt.URL.Get_TransferReceipt,
            data: { id: id },
            success: function (res) {
                $scope.EQMItem = res;
                if (res.VehicleID != null) {
                    $scope.EQMItem.TransferType = 1;
                    $scope.isTransferReceipt_Vehicle = true;
                } else if (res.StockID != null) {
                    $scope.EQMItem.TransferType = 2;
                    $scope.isTransferReceipt_Vehicle = false;
                } else {
                    $scope.EQMItem.TransferType = 1;
                    $scope.isTransferReceipt_Vehicle = true;
                }
                win.center();
                win.open()
            }
        })
    }

    $scope.cboGoe_options = {
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
            if (e.sender.selectedIndex >= 0) $scope.IsEdited = true;
        }
    }
    Common.Services.Call($http, {
        url: Common.Services.url.CAT,
        method: Common.ALL.URL.CATGroupOfEquipment,
        data: {},
        success: function (res) {
            $scope.cboGoe_options.dataSource.data(res.Data)
        }
    });

    Common.Services.Call($http, {
        url: Common.Services.url.FLM,
        method: _FLMTransferReceipt.URL.Get_Vehicle_List,
        data: {},
        success: function (res) {
            $scope.cboFLMVehicle_Options.dataSource.data(res.Data)
        }
    });

    $scope.FLMTransferReceipt_CloseClick = function ($event, win) {
        $event.preventDefault();
        win.close();
    }

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
            //$scope.FINFreightAuditVendor_gridOptions.dataSource.data([]);
        }

    };

    $scope.cboFLMStock1_Options = {
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
            //$scope.FINFreightAuditVendor_gridOptions.dataSource.data([]);
        }

    };

    Common.Services.Call($http, {
        url: Common.Services.url.FLM,
        method: _FLMTransferReceipt.URL.StockList,
        success: function (res) {
            if (Common.HasValue(res)) {
                $scope.cboFLMStock1_Options.dataSource.data(res.Data);
            }
        }
    });

    $scope.numRemainValue_options = { format: 'n0', spinners: false, culture: 'en-US', min: 0, step: 1, decimals: 0, }

    $scope.numYearOfProduction_options = { format: '0000', spinners: false, culture: 'en-US', min: 0, max: 3000, step: 1, decimals: 0, }
    $scope.numBaseValue_options = { format: 'n0', spinners: false, culture: 'en-US', min: 0, step: 1, decimals: 0, }
    $scope.numCurrentValue_options = { format: 'n0', spinners: false, culture: 'en-US', min: 0, step: 1, decimals: 0, }
    $scope.numDepreciationPeriod_options = { format: 'n2', spinners: false, culture: 'en-US', min: 0, step: 1, decimals: 2, }
    $scope.numWarrantyPeriod_options = { format: 'n2', spinners: false, culture: 'en-US', min: 0, step: 1, decimals: 2, }

    $scope.FLMTransferReceipt_changeType = function ($event, isVehicle) {
        switch (isVehicle) {
            default:
                break;
            case 1:
                $scope.isTransferReceipt_Vehicle = true;
                $scope.EQMItem.StockID = null;
                break;
            case 2:
                $scope.isTransferReceipt_Vehicle = false;
                $scope.EQMItem.VehicleID = null;
                break;
        }
    }

    $scope.FLMTransferReceipt_SaveClick = function ($event, win, vform) {
        $event.preventDefault();
        var isChecked = true;
        if ($scope.EQMItem.Quantity < 1) {
            $rootScope.Message({
                Type: Common.Message.Type.Notify,
                NotifyType: Common.Message.NotifyType.ERROR,
                Title: 'Thông báo',
                Msg: 'Lỗi: Số Lượng phải lớn hơn 0.',
                Close: null,
                Ok: null
            })
            isChecked = false;
        }

        if ($scope.EQMItem.TransferType == 1 && $scope.isNewTransferReceipt == true) {
            if($scope.EQMItem.VehicleID == null){
                $rootScope.Message({
                    Type: Common.Message.Type.Notify,
                    NotifyType: Common.Message.NotifyType.ERROR,
                    Title: 'Thông báo',
                    Msg: 'Vui lòng chọn xe',
                    Close: null,
                    Ok: null
                })
                isChecked = false;
            } 
        } else if ($scope.EQMItem.TransferType == 2 && $scope.isNewTransferReceipt == true) {
            if ($scope.EQMItem.StockID == null) {
                $rootScope.Message({
                    Type: Common.Message.Type.Notify,
                    NotifyType: Common.Message.NotifyType.ERROR,
                    Title: 'Thông báo',
                    Msg: 'Vui lòng chọn kho',
                    Close: null,
                    Ok: null
                })
                isChecked = false;
            }
        }

        if (vform() && isChecked) {
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.FLM,
                method: _FLMTransferReceipt.URL.Save_EQM,
                data: { item: $scope.EQMItem },
                success: function (res) {
                    $rootScope.Message({
                        Type: Common.Message.Type.Notify,
                        NotifyType: Common.Message.NotifyType.SUCCESS,
                        Title: 'Thông báo',
                        Msg: 'Thành công',
                        Close: null,
                        Ok: null
                    })

                    win.close();
                    $scope.FLMTransferReceipt_GridOptions.dataSource.read();
                    $rootScope.IsLoading = false;
                },
                error: function() {
                    $rootScope.IsLoading = false;
                }
            });
        }
    }

    $scope.Edit_Click = function ($event, win, grid) {
        $event.preventDefault();

        var tr = $($event.target).closest('tr');
        var item = grid.dataItem(tr);
        $scope.isNewTransferReceipt = false;

        $scope.LoadItem(win, item.id);
    };

    //#region excel
    $scope.FLMTransferReceipt_Excel_Click = function ($event, win) {
        $event.preventDefault();

        if ($scope.ItemFilter.StockID <= 0) {
            $rootScope.Message({ Msg: 'Chưa chọn kho', NotifyType: Common.Message.NotifyType.ERROR });
        } else {
            $rootScope.UploadExcel({
                Path: Common.FolderUpload.Import,
                columns: [
                    { field: 'PartNo', title: 'Mã thiết bị', width: '150px', filterable: { cell: { showOperators: false, operator: "contains" } } },
                    { field: 'PartName', title: 'Tên thiết bị', width: '180px', filterable: { cell: { showOperators: false, operator: "contains" } } }
                ],
                Download: function () {
                    $rootScope.IsLoading = true;
                    Common.Services.Call($http, {
                        url: Common.Services.url.FLM,
                        method: _FLMTransferReceipt.URL.Excel_Export,
                        data: {},
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
                        method: _FLMTransferReceipt.URL.Excel_Check,
                        data: { file: e },
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
                        method: _FLMTransferReceipt.URL.Excel_Save,
                        data: { lst: data, stockID: $scope.ItemFilter.StockID },
                        success: function (res) {
                            $scope.FLMTransferReceipt_GridOptions.dataSource.read()
                            $rootScope.IsLoading = false;
                        }
                    })
                }
            })
        }
    }

    //#endregion
}])