/// <reference path="~/Scripts/jquery-1.9.1.intellisense.js" />
/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />
/// <reference path="~/Scripts/map.js" />

var _FLMAsset_ContainerEdit = {
    URL: {
        Get_Container: 'FLMAsset_Container_Get',
        Save_Container: 'FLMAsset_Container_Update',
        Delete_Container: 'FLMAsset_Container_Destroy',

        Get_EQMList: 'FLMAsset_EQM_ReadByAsset',
        Get_FuelHistory: 'FLMAsset_FuelHistoryRead',

        Read_Consumption: 'FLMAsset_Consumption_ReadByAsset',
        Save_Consumption: 'FLMAsset_Consumption_UpdateByAsset',
        Delete_Consumption: 'FLMAsset_Consumption_Destroy',
        Read_ConsumptionNotIn: 'FLMAsset_Consumption_NotChooseRead',
        Save_ConsumptionNotIn: 'FLMAsset_Consumption_NotChooseUpdate',

        Check_Depreciation: 'FLMAsset_Depreciation_Check',
        //Read_Depreciation: 'FLMAsset_Depreciation_Read',
        Read_Depreciation: 'FLMAsset_History_DepreciationList',
        Save_Depreciation: 'FLMAsset_Depreciation_Update',
        Delete_Depreciation: 'FLMAsset_Depreciation_Delete',
        Generate_Depreciation: 'FLMAsset_Depreciation_Generate',

        Get_ALLDriverList: 'FLMAsset_AllDriverRead',
        Get_ContainerDriverList: 'FLMAsset_Container_Read',
        Get_RentList: 'FLMAsset_RentRead'
    },
    Param: {
        AssetID: -1
    },
    Data: {
        ItemBackup: null
    },
    Map: {
        Map: null,
        Marker: null,
        IconMarker: '/Images/map/truck_green.png'
    }
}

angular.module('myapp').controller('FLMAsset_EditContainerCtrl', ['$rootScope', '$scope', '$http', '$location', '$state', '$timeout', function ($rootScope, $scope, $http, $location, $state, $timeout) {
    Common.Log('FLMAsset_EditContainerCtrl');
    $rootScope.IsLoading = false;

    $rootScope.Loading.Show();
    $scope.HasChoose = false;
    $scope.IsNewVehicle = true;
    $scope.IsEdited = false;
    var LoadingStep = 70;


    $rootScope.$watch('Loading.Progress', function (v, n) {
        if ($rootScope.Loading.Progress >= 100) {
            $rootScope.Loading.Progress = 0;
            $rootScope.Loading.Hide();
        }
    })
    _FLMAsset_ContainerEdit.Param = $.extend(true, _FLMAsset_ContainerEdit.Param, $state.params);
    $scope.ContainerItem = null;
    $scope.IsShowTab = false;
    if (_FLMAsset_ContainerEdit.Param.AssetID > 0) { $scope.IsShowTab = true; $scope.IsNewVehicle = false; }

    Common.Services.Call($http, {
        url: Common.Services.url.FLM,
        method: _FLMAsset_ContainerEdit.URL.Get_Container,
        data: { AssetID: _FLMAsset_ContainerEdit.Param.AssetID },
        success: function (res) {
            $scope.ContainerItem = res;
            $rootScope.Loading.Change("Thông tin chung", 35);
            //$scope.CreateMap();
        }
    });

    $scope.FLMAsset_EditContainer_TabOptions = { animation: { open: { effects: "fadeIn" } } }

    $scope.FLMAsset_Container_SaveClick = function ($event,vform) {
        $event.preventDefault();
        if (vform()) {
            Common.Services.Call($http, {
                url: Common.Services.url.FLM,
                method: _FLMAsset_ContainerEdit.URL.Save_Container,
                data: { item: $scope.ContainerItem },
                success: function (res) {
                    $rootScope.Message({
                        Type: Common.Message.Type.Alert,
                        NotifyType: Common.Message.NotifyType.SUCCESS,
                        Title: 'Thông báo',
                        Msg: 'Thành công',
                        Close: function (e) {
                            $state.go('main.FLMAsset.Index')
                        },
                        Ok: function (e) {
                            $state.go('main.FLMAsset.Index')
                        }
                    })
                }
            });
        }
    }

    $scope.FLMAsset_Container_BackClick = function ($event) {
        $event.preventDefault();
        $state.go("main.FLMAsset.Index")
    }

    $scope.FLMAsset_Container_DelClick = function ($event) {
        $event.preventDefault();
        $rootScope.Message({
            Type: Common.Message.Type.Confirm,
            NotifyType: Common.Message.NotifyType.SUCCESS,
            Title: 'Thông báo',
            Msg: 'Bạn muốn xóa dữ liệu này',
            Ok: function (e) {
                Common.Services.Call($http, {
                    url: Common.Services.url.FLM,
                    method: _FLMAsset_ContainerEdit.URL.Delete_Container,
                    data: { item: $scope.ContainerItem },
                    success: function (res) {
                        $state.go('main.FLMAsset.Index')
                    }
                });
            }
        })


    }
    //tab 1
    $scope.cboPackage_options = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: "contains",
        suggest: true, dataTextField: 'Code', dataValueField: 'ID',
        dataSource: Common.DataSource.Local([], {
            id: 'ID',
            fields: {
                Code: { type: 'string' },
                ID: { type: 'number' },
            }
        }),
        change: function (e) {
            if (e.sender.selectedIndex >= 0) $scope.IsEdited = true;
        }
    }
    Common.Services.Call($http, {
        url: Common.Services.url.CAT,
        method: Common.ALL.URL.CATPackingCO,
        data: {},
        success: function (res) {
            $scope.cboPackage_options.dataSource.data(res.Data)
        }
    });

    $scope.numLength_options = { format: 'n3', spinners: false, culture: 'en-US', min: 0, step: 1, decimals: 3, }
    $scope.numWidth_options = { format: 'n3', spinners: false, culture: 'en-US', min: 0, step: 1, decimals: 3, }
    $scope.numHeight_options = { format: 'n3', spinners: false, culture: 'en-US', min: 0, step: 1, decimals: 3, }

    $scope.numYearOfProduction_options = { format: '0000', spinners: false, culture: 'en-US', min: 0, max: 3000, step: 1, decimals: 0, }
    $scope.numBaseValue_options = { format: 'n0', spinners: false, culture: 'en-US', min: 0, step: 1, decimals: 0, }
    $scope.numRemainValue_options = {format: 'n0', spinners: false, culture: 'en-US', min: 0, step: 1, decimals: 0,}
    $scope.numCurrentValue_options = { format: 'n0', spinners: false, culture: 'en-US', min: 0, step: 1, decimals: 0, }
    $scope.numDepreciationPeriod_options = { format: 'n2', spinners: false, culture: 'en-US', min: 0, step: 1, decimals: 2, }
    $scope.numWarrantyPeriod_options = { format: 'n2', spinners: false, culture: 'en-US', min: 0, step: 1, decimals: 2, }

    

    $scope.cboRent_options = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: "contains", suggest: true, dataTextField: 'VendorName', dataValueField: 'ID',
        dataSource: Common.DataSource.Local([],
            {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    VendorName: { type: 'string' }
                }
            }),
        change: function (e) {
            if (e.sender.selectedIndex >= 0) $scope.IsEdited = true;
        }
    }
    Common.Services.Call($http, {
        url: Common.Services.url.FLM,
        method: _FLMAsset_ContainerEdit.URL.Get_RentList,
        data: {},
        success: function (res) {
            $scope.cboRent_options.dataSource.data(res.Data)
        }
    });

    $scope.FLMAsset_Depreciation_GenerateClick = function ($event,vform) {
        $event.preventDefault();
        if ($scope.IsEdited) {
            $rootScope.Message({
                Type: Common.Message.Type.Confirm,
                NotifyType: Common.Message.NotifyType.WARNING,
                Title: 'Thông báo',
                Msg: 'Lưu thay đổi trước khi tiếp tục',
                Close: null,
                Ok: function (e) {
                    if (vform()) {
                        Common.Services.Call($http, {
                            url: Common.Services.url.FLM,
                            method: _FLMAsset_ContainerEdit.URL.Save_Container,
                            data: { item: $scope.ContainerItem },
                            success: function (res) {
                                Common.Services.Call($http, {
                                    url: Common.Services.url.FLM,
                                    method: _FLMAsset_ContainerEdit.URL.Check_Depreciation,
                                    data: { AssetID: _FLMAsset_ContainerEdit.Param.AssetID, DepreciationPeriod: $scope.ContainerItem.DepreciationPeriod, DepreciationStart: $scope.ContainerItem.DepreciationStart },
                                    success: function (res) {
                                        Common.Services.Call($http, {
                                            url: Common.Services.url.FLM,
                                            method: _FLMAsset_ContainerEdit.URL.Generate_Depreciation,
                                            data: { AssetID: _FLMAsset_ContainerEdit.Param.AssetID },
                                            success: function (res) {
                                                $rootScope.Message({
                                                    Type: Common.Message.Type.Alert,
                                                    NotifyType: Common.Message.NotifyType.SUCCESS,
                                                    Title: 'Thông báo',
                                                    Msg: 'Thành công',
                                                    Close: function (e) {
                                                        $state.go('main.FLMAsset.Index')
                                                    },
                                                    Ok: function (e) {
                                                        $state.go('main.FLMAsset.Index')
                                                    }
                                                })
                                            }
                                        });
                                    }
                                });
                            }
                        });
                    }
                }
            })
        }
        else {
            Common.Services.Call($http, {
                url: Common.Services.url.FLM,
                method: _FLMAsset_ContainerEdit.URL.Check_Depreciation,
                data: { AssetID: _FLMAsset_ContainerEdit.Param.AssetID, DepreciationPeriod: $scope.ContainerItem.DepreciationPeriod, DepreciationStart: $scope.ContainerItem.DepreciationStart },
                success: function (res) {
                    Common.Services.Call($http, {
                        url: Common.Services.url.FLM,
                        method: _FLMAsset_ContainerEdit.URL.Generate_Depreciation,
                        data: { AssetID: _FLMAsset_ContainerEdit.Param.AssetID },
                        success: function (res) {
                            $rootScope.Message({
                                Type: Common.Message.Type.Alert,
                                NotifyType: Common.Message.NotifyType.SUCCESS,
                                Title: 'Thông báo',
                                Msg: 'Thành công',
                                Close: function (e) {
                                    $state.go('main.FLMAsset.Index')
                                },
                                Ok: function (e) {
                                    $state.go('main.FLMAsset.Index')
                                }
                            })
                        }
                    });
                }
            });
        }
    }

   
    //tab 2

    $scope.currEQM_GridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.FLM,
            method: _FLMAsset_ContainerEdit.URL.Get_EQMList,
            readparam: function () { return { AssetID: _FLMAsset_ContainerEdit.Param.AssetID } },
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number', editable: false, nullable: true },
                    WarrantyEnd: { type: 'date' }
                }
            }
        }),
        height: '100%', pageable: true, sortable: true, columnMenu: false, filterable: false, resizable: true,
        columns: [
            { field: 'Code', title: 'Số Part.', width: 100, template: '<a href="/" ng-click=FLMAsset_EQM_EditClick($event,dataItem) style=" text-decoration:underline; color:blue">#=Code#</a>' },
            { field: 'GroupOfEquipmentName', title: 'Loại thiết bị', width: 100, },
            { field: 'BaseValue', title: 'Giá trị ban đầu', width: 100 },
            { field: 'CurrentValue', title: 'Giá trị hiện tại', width: 100 },
            { field: 'DepreciationPeriod', title: 'T/g k.hao (tháng)', width: 100 },
            { field: 'WarrantyEnd', title: 'Ngày kết thúc BH', template: '#=WarrantyEnd==null?"":Common.Date.FromJsonDDMMYY(WarrantyEnd)#', width: 100 }
        ]
    }

    $scope.pastEQM_GridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.FLM,
            method: _FLMAsset_ContainerEdit.URL.Get_EQMList,
            readparam: function () { return { AssetID: _FLMAsset_ContainerEdit.Param.AssetID } },
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number', editable: false, nullable: true },
                    WarrantyEnd: { type: 'date' }
                }
            }
        }),
        height: '100%', pageable: true, sortable: true, columnMenu: false, filterable: false, resizable: true,
        columns: [
            { field: 'Code', title: 'Số Part.', width: 100, template: '<a href="/" ng-click=FLMAsset_EQM_EditClick($event,dataItem) style=" text-decoration:underline; color:blue">#=Code#</a>' },
            { field: 'GroupOfEquipmentName', title: 'Loại thiết bị', width: 100, },
            { field: 'BaseValue', title: 'Giá trị ban đầu', width: 100 },
            { field: 'CurrentValue', title: 'Giá trị hiện tại', width: 100 },
            { field: 'DepreciationPeriod', title: 'T/g k.hao (tháng)', width: 100 },
            { field: 'WarrantyEnd', title: 'Ngày kết thúc BH', template: '#=WarrantyEnd==null?"":Common.Date.FromJsonDDMMYY(WarrantyEnd)#', width: 100 }
        ]
    }

    $scope.SplitterEQM_Options = {
        orientation: "vertical",
        panes: [
            { collapsible: false, resizable: false, size: "50%" },
            { collapsible: false, resizable: false, size: "50%" }
        ]
    }
    
    $scope.FLMAsset_EQM_EditClick = function ($event, data) {
        $event.preventDefault();
        $state.go("main.FLMAsset.EditEQM", { AssetID: data.ID })
    }

    //tab 3
    $scope.DepreciationGrid_Options = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.FLM,
            method: _FLMAsset_ContainerEdit.URL.Read_Depreciation,
            readparam: function () { return { AssetID: _FLMAsset_ContainerEdit.Param.AssetID } },
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number', editable: false, nullable: true },
                    Value: { type: 'number' }
                }
            }
        }),
        height: '100%', pageable: true, sortable: true, columnMenu: false, filterable: false, resizable: true,
        columns: [
            { field: 'Month', title: 'Tháng', width: 100, },
            { field: 'Year', title: 'Năm', width: 100 },
            { field: 'Value', title: 'Giá trị', template: '#=Common.Number.ToMoney(Value)#', width: 100 }
        ],
        dataBound: function (e) {
            $rootScope.Loading.Change("Lịch sử kháu hao....", $rootScope.Loading.Progress + LoadingStep);
        }
    }
}])