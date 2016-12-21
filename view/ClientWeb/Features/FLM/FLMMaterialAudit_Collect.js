/// <reference path="~/Scripts/jquery-1.9.1.intellisense.js" />
/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />

var _FLMMaterialAudit_Edit = {
    URL: {
        Get: 'FLMMaterialAudit_Get',
        DITOMaster_List: 'FLMMaterialAudit_DITOMaster_List',
        COTOMaster_List: 'FLMMaterialAudit_COTOMaster_List',
        Receipt_List: 'FLMMaterialAudit_Receipt_List',
        Result_List: 'FLMMaterialAudit_Result_List',
        FLMMaterialAudit_Generate: 'FLMMaterialAudit_Generate',
        FLMMaterialAudit_Close: 'FLMMaterialAudit_Close',
        FLMMaterialAudit_Open: 'FLMMaterialAudit_Open',
    },
    Param: {
        ID: -1
    },
}


angular.module('myapp').controller('FLMMaterialAudit_CollectCtrl', ['$rootScope', '$scope', '$http', '$location', '$state', '$timeout', function ($rootScope, $scope, $http, $location, $state, $timeout) {
    Common.Log('FLMMaterialAudit_CollectCtrl');
    $rootScope.IsLoading = false;

    if ($rootScope.IsPageComplete != true) return;
    $scope.Auth = $rootScope.GetAuth();
    _FLMMaterialAudit_Edit.Param = $.extend(true, _FLMMaterialAudit_Edit.Param, $state.params);

    
    $scope.Item = { AssetID: 0 };
    $scope.FLMAsset_ShowBtnExcel = true;
    $scope.TabIndex = 1;
    var LoadingStep = 25;
    $scope.ConsumptionEdit = null;
    $scope.FLMMaterialAudit_Tab = 0;
    $scope.FLMMaterialAudit_TabOptions = {
        animation: { open: { effects: "fadeIn" } },
        select: function (e) {
            $timeout(function () {
                $scope.FLMMaterialAudit_Tab = angular.element(e.item).data('tabindex');
                Common.Log("Select_Tab_" + $scope.TabIndex);
            }, 1)
        }
    }
    //#region Thong tin
    Common.Services.Call($http, {
        url: Common.Services.url.FLM,
        method: _FLMMaterialAudit_Edit.URL.Get,
        data: { ID: _FLMMaterialAudit_Edit.Param.ID, },
        success: function (res) {
            $scope.Item = res;
            
        }
    });
    //#endregion

    //#region Phan phoi
    $scope.FLMMaterialAudit_AssetGrid_Options = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.FLM,
            method: _FLMMaterialAudit_Edit.URL.DITOMaster_List,
            readparam: function () { return { id: _FLMMaterialAudit_Edit.Param.ID } },
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number', editable: false, nullable: true },
                }
            }
        }),
        height: '100%', pageable: true, sortable: true, columnMenu: false, filterable: false, resizable: true,
        columns: [
            {
                field: "RegNo", title: 'Số xe', width: '100px',
                filterable: { cell: { showOperators: false, operator: "contains" } },
            },
            { field: 'Code', title: 'Mã chuyến', width: '200px', filterable: { cell: { showOperators: false, operator: "contains" } } },
            { field: 'DriverName', title: 'Tài xế', width: '200px', filterable: { cell: { showOperators: false, operator: "contains" } } },
            { field: "ATD", title: 'ATD', width: '15%', template: "#=ATD==null?\"\":Common.Date.FromJsonYYMMDDHM(ATD)#" },
            { field: "ATA", title: 'ATA', width: '15%', template: "#=ATA==null?\"\":Common.Date.FromJsonYYMMDDHM(ATA)#" },
            { field: "ETD", title: 'ETD', width: '15%', template: "#=ETD==null?\"\":Common.Date.FromJsonYYMMDDHM(ETD)#" },
            { field: "ETA", title: 'ETA', width: '15%', template: "#=ETA==null?\"\":Common.Date.FromJsonYYMMDDHM(ETA)#" },
            {
                field: "KM", title: 'Tổng KM', width: '120px',
                template: "#=KM==null?\"\":KM#", filterable: { cell: { showOperators: false, operator: "eq" } }
            },
        ]
    }
    //#endregion

    //#region Cont
    $scope.FLMMaterialAudit_ContGrid_Options = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.FLM,
            method: _FLMMaterialAudit_Edit.URL.COTOMaster_List,
            readparam: function () { return { id: _FLMMaterialAudit_Edit.Param.ID } },
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number', editable: false, nullable: true },
                }
            }
        }),
        height: '100%', pageable: true, sortable: true, columnMenu: false, filterable: false, resizable: true,
        columns: [
            {
                field: "RegNo", title: 'Số xe', width: '100px',
                filterable: { cell: { showOperators: false, operator: "contains" } },
            },
            { field: 'Code', title: 'Mã chuyến', width: '200px', filterable: { cell: { showOperators: false, operator: "contains" } } },
            { field: 'DriverName', title: 'Tài xế', width: '200px', filterable: { cell: { showOperators: false, operator: "contains" } } },
            { field: "ATD", title: 'ATD', width: '15%', template: "#=ATD==null?\"\":Common.Date.FromJsonYYMMDDHM(ATD)#" },
            { field: "ATA", title: 'ATA', width: '15%', template: "#=ATA==null?\"\":Common.Date.FromJsonYYMMDDHM(ATA)#" },
            { field: "ETD", title: 'ETD', width: '15%', template: "#=ETD==null?\"\":Common.Date.FromJsonYYMMDDHM(ETD)#" },
            { field: "ETA", title: 'ETA', width: '15%', template: "#=ETA==null?\"\":Common.Date.FromJsonYYMMDDHM(ETA)#" },
            {
                field: "KM", title: 'Tổng KM', width: '120px',
                template: "#=KM==null?\"\":KM#", filterable: { cell: { showOperators: false, operator: "eq" } }
            },
        ]
    }
    //#endregion

    //#region Phieu xuat nhien lieu
    $scope.FLMMaterialAudit_ReceiptGrid_Options = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.FLM,
            method: _FLMMaterialAudit_Edit.URL.Receipt_List,
            readparam: function () { return { id: _FLMMaterialAudit_Edit.Param.ID } },
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number', editable: false, nullable: true },
                }
            }
        }),
        height: '100%', pageable: true, sortable: true, columnMenu: false, filterable: false, resizable: true,
        columns: [
            
            { field: 'Code', title: 'Mã phiếu', width: '200px', filterable: { cell: { showOperators: false, operator: "contains" } } },
            { field: "DateReceipt", title: 'Ngày lập phiếu', width: '15%', template: "#=DateReceipt==null?\"\":Common.Date.FromJsonDDMMYY(DateReceipt)#" },
            {
                field: "KM", title: 'Tổng KM', width: '120px',
                template: "#=KM==null?\"\":KM#", filterable: { cell: { showOperators: false, operator: "eq" } }
            },
        ]
    }
    //#endregion

    //#region Ket qua

    $scope.FLMMaterialAudit_ResultGrid_Options = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.FLM,
            method: _FLMMaterialAudit_Edit.URL.Result_List,
            readparam: function () { return { id: _FLMMaterialAudit_Edit.Param.ID} },
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number', editable: false, nullable: true },
                    RegNo: { type: 'string', editable: false },
                    Code: { type: 'string', editable: false },
                    QuantityModified: { type: 'number'},
                    NoteModified: { type: 'string'},
                    QuantityReceipt: { type: 'number', editable: false },
                    QuantityRemain: { type: 'number', editable: false },
                    QuantityTOMaster: { type: 'number', editable: false },
                    QuantityTotal: { type: 'number', editable: false },
                }
            }
        }),
        height: '100%', pageable: true, sortable: true, columnMenu: false, filterable: false, resizable: true, editable: { mode: 'inline' },
        columns: [
            {
                field: "RegNo", title: 'Số xe', width: '100px',
                filterable: { cell: { showOperators: false, operator: "contains" } },
            },
            { field: 'Code', title: 'Mã nhiên liệu', width: '100px', filterable: { cell: { showOperators: false, operator: "contains" } } },
            {
                field: "QuantityModified", title: 'Số lượng điều chình', width: 120,
                filterable: { cell: { showOperators: false, operator: "eq" }},
                template: '<input ng-model="dataItem.QuantityModified" class="k-textbox" type="number" />'
            },
            {
                field: "NoteModified", title: 'Ghi chú điều chỉnh', width: '100px',
                filterable: { cell: { showOperators: false, operator: "contains" } },
                template: '<input ng-model="dataItem.NoteModified" class="k-textbox" />'
                
            },
            {
                field: "QuantityReceipt", title: 'Số lượng phiếu', width: '120px',
                template: "#=QuantityReceipt==null?\"\":QuantityReceipt#", filterable: { cell: { showOperators: false, operator: "eq" } }
            },
            {
                field: "QuantityRemain", title: 'Số lượng còn lại', width: '120px',
                template: "#=QuantityRemain==null?\"\":QuantityRemain#", filterable: { cell: { showOperators: false, operator: "eq" } }
            },
            {
                field: "QuantityTOMaster", title: 'Số lượng chuyến', width: '120px',
                template: "#=QuantityTOMaster==null?\"\":QuantityTOMaster#", filterable: { cell: { showOperators: false, operator: "eq" } }
            },
            {
                field: "QuantityTotal", title: 'Tổng số lượng', width: '120px',
                template: "#=QuantityTotal==null?\"\":QuantityTotal#", filterable: { cell: { showOperators: false, operator: "eq" } }
            },
        ]
    }
    $scope.numQuantityModifiedOptions = { format: 'n3', spinners: false, culture: 'en-US', min: 0, step: 1, decimals: 3, }
    //#endregion

    $scope.FLMMaterialAudit_Generate_Click = function ($event) {
        $event.preventDefault();

            $rootScope.Message({
                Type: Common.Message.Type.Confirm,
                NotifyType: Common.Message.NotifyType.SUCCESS,
                Title: 'Thông báo',
                Msg: 'Bạn muốn cập nhật lại không ?',
                Close: null,
                Ok: function () {
                    $rootScope.IsLoading = true;
                    Common.Services.Call($http, {
                        url: Common.Services.url.FLM,
                        method: _FLMMaterialAudit_Edit.URL.FLMMaterialAudit_Generate,
                        data: { id: _FLMMaterialAudit_Edit.Param.ID },
                        success: function (res) {
                            $rootScope.IsLoading = false;
                            $rootScope.Message({
                                Type: Common.Message.Type.Notify,
                                NotifyType: Common.Message.NotifyType.SUCCESS,
                                Title: 'Thông báo',
                                Msg: 'Thành công',
                                Close: null,
                                Ok: null
                            });
                            $scope.FLMMaterialAudit_ResultGrid_Options.dataSource.read();
                            $scope.FLMMaterialAudit_ReceiptGrid_Options.dataSource.read();
                            $scope.FLMMaterialAudit_ContGrid_Options.dataSource.read();
                            $scope.FLMMaterialAudit_AssetGrid_Options.dataSource.read();
                        }
                    });
                }
            })
    }

    $scope.FLMMaterialAudit_Close_Click = function ($event) {
        $event.preventDefault();

        $rootScope.Message({
            Type: Common.Message.Type.Confirm,
            NotifyType: Common.Message.NotifyType.SUCCESS,
            Title: 'Thông báo',
            Msg: 'Bạn muốn đóng không lại không ?',
            Close: null,
            Ok: function () {
                $rootScope.IsLoading = true;
                Common.Services.Call($http, {
                    url: Common.Services.url.FLM,
                    method: _FLMMaterialAudit_Edit.URL.FLMMaterialAudit_Close,
                    data: { id: _FLMMaterialAudit_Edit.Param.ID },
                    success: function (res) {
                        $rootScope.IsLoading = false;
                        $rootScope.Message({
                            Type: Common.Message.Type.Notify,
                            NotifyType: Common.Message.NotifyType.INFO,
                            Title: 'Thông báo',
                            Msg: 'Thành công',
                            Close: null,
                            Ok: null
                        })
                    }
                });
            }
        })
    }

    $scope.FLMMaterialAudit_Open_Click = function ($event) {
        $event.preventDefault();

        $rootScope.Message({
            Type: Common.Message.Type.Confirm,
            NotifyType: Common.Message.NotifyType.SUCCESS,
            Title: 'Thông báo',
            Msg: 'Bạn muốn mở dữ liệu không không lại không ?',
            Close: null,
            Ok: function () {
                $rootScope.IsLoading = true;
                Common.Services.Call($http, {
                    url: Common.Services.url.FLM,
                    method: _FLMMaterialAudit_Edit.URL.FLMMaterialAudit_Open,
                    data: { id: _FLMMaterialAudit_Edit.Param.ID },
                    success: function (res) {
                        $rootScope.IsLoading = false;
                        $rootScope.Message({
                            Type: Common.Message.Type.Notify,
                            NotifyType: Common.Message.NotifyType.SUCCESS,
                            Title: 'Thông báo',
                            Msg: 'Thành công',
                            Close: null,
                            Ok: null
                        })
                    }
                });
            }
        })
    }

    $scope.Win_Close = function ($event, win) {
        $event.preventDefault();
        win.close();
    }
    
    $scope.FLMMaterialAudit_BackClick = function ($event) {
        $event.preventDefault();
        $state.go("main.FLMMaterialAudit.Index")
    }
}]);