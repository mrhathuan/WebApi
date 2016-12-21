/// <reference path="~/Scripts/jquery-1.9.1.intellisense.js" />
/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />
/// <reference path="~/Scripts/map.js" />

var _FLMAsset_EQMEdit = {
    URL: {
        Get_EQM: 'FLMAsset_EQM_Get',
        Save_EQM: 'FLMAsset_EQM_Update',
        Delete_EQM: 'FLMAsset_EQM_Destroy',

        Check_Depreciation: 'FLMAsset_Depreciation_Check',
        //Read_Depreciation: 'FLMAsset_Depreciation_Read',
        Read_Depreciation: 'FLMAsset_History_DepreciationList',
        Save_Depreciation: 'FLMAsset_Depreciation_Update',
        Delete_Depreciation: 'FLMAsset_Depreciation_Delete',
        Generate_Depreciation: 'FLMAsset_Depreciation_Generate',

        Read_TranferHistory: 'FLMAsset_EQM_ReadHistory',
        Get_RentList: 'FLMAsset_RentRead',
        Get_Location: 'FLMAsset_Eqm_GetLocation'

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
        IconMarker: '/Images/map/EQM_green.png'
    }
}

angular.module('myapp').controller('FLMAsset_EditEQMCtrl', ['$rootScope', '$scope', '$http', '$location', '$state', '$timeout', function ($rootScope, $scope, $http, $location, $state, $timeout) {
    Common.Log('FLMAsset_EditEQMCtrl');
    $rootScope.IsLoading = false;
    $scope.HasChoose = false;
    $scope.IsNewVehicle = true;
    $scope.IsEdited = false;

    _FLMAsset_EQMEdit.Param = $.extend(true, _FLMAsset_EQMEdit.Param, $state.params);
    $scope.EQMItem = null;
    $scope.Location = null;

    $scope.IsShowTab = false;
    if (_FLMAsset_EQMEdit.Param.AssetID > 0) { $scope.IsShowTab = true; $scope.IsNewVehicle = false; }

    Common.Services.Call($http, {
        url: Common.Services.url.FLM,
        method: _FLMAsset_EQMEdit.URL.Get_EQM,
        data: { AssetID: _FLMAsset_EQMEdit.Param.AssetID },
        success: function (res) {
            $scope.EQMItem = res;
        }
    });

    $scope.FLMAsset_EditEQM_TabOptions = { animation: { open: { effects: "fadeIn" } } }

    $scope.FLMAsset_EQM_SaveClick = function ($event, win,vform) {
        $event.preventDefault();
        if (vform()) {
            if (!$scope.IsNewVehicle) {
                Common.Services.Call($http, {
                    url: Common.Services.url.FLM,
                    method: _FLMAsset_EQMEdit.URL.Save_EQM,
                    data: { item: $scope.EQMItem },
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
            else {
                win.center();
                win.open();
            }
        }
    }

    $scope.FLMAsset_EQM_BackClick = function ($event) {
        $event.preventDefault();
        $state.go("main.FLMAsset.Index")
    }

    //$scope.FLMAsset_EQM_DelClick = function ($event) {
    //    $event.preventDefault();
    //    $rootScope.Message({
    //        Type: Common.Message.Type.Confirm,
    //        NotifyType: Common.Message.NotifyType.SUCCESS,
    //        Title: 'Thông báo',
    //        Msg: 'Bạn muốn xóa dữ liệu này',
    //        Ok: function (e) {
    //            Common.Services.Call($http, {
    //                url: Common.Services.url.FLM,
    //                method: _FLMAsset_EQMEdit.URL.Delete_EQM,
    //                data: { item: $scope.EQMItem },
    //                success: function (res) {
    //                    $state.go('main.FLMAsset.Index')
    //                }
    //            });
    //        }
    //    })


    //}
    //tab 1
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

    $scope.numRemainValue_options = { format: 'n0', spinners: false, culture: 'en-US', min: 0, step: 1, decimals: 0, }

    $scope.numYearOfProduction_options = { format: 'n0', spinners: false, culture: 'en-US', min: 0, max: 3000, step: 1, decimals: 0, }
    $scope.numBaseValue_options = { format: 'n0', spinners: false, culture: 'en-US', min: 0, step: 1, decimals: 0, }
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
        method: _FLMAsset_EQMEdit.URL.Get_RentList,
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
                            method: _FLMAsset_EQMEdit.URL.Save_EQM,
                            data: { item: $scope.EQMItem },
                            success: function (res) {
                                Common.Services.Call($http, {
                                    url: Common.Services.url.FLM,
                                    method: _FLMAsset_EQMEdit.URL.Check_Depreciation,
                                    data: { AssetID: _FLMAsset_EQMEdit.Param.AssetID, DepreciationPeriod: $scope.EQMItem.DepreciationPeriod, DepreciationStart: $scope.EQMItem.DepreciationStart },
                                    success: function (res) {
                                        Common.Services.Call($http, {
                                            url: Common.Services.url.FLM,
                                            method: _FLMAsset_EQMEdit.URL.Generate_Depreciation,
                                            data: { AssetID: _FLMAsset_EQMEdit.Param.AssetID },
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
                method: _FLMAsset_EQMEdit.URL.Check_Depreciation,
                data: { AssetID: _FLMAsset_EQMEdit.Param.AssetID, DepreciationPeriod: $scope.EQMItem.DepreciationPeriod, DepreciationStart: $scope.EQMItem.DepreciationStart },
                success: function (res) {
                    Common.Services.Call($http, {
                        url: Common.Services.url.FLM,
                        method: _FLMAsset_EQMEdit.URL.Generate_Depreciation,
                        data: { AssetID: _FLMAsset_EQMEdit.Param.AssetID },
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

    //tab 3
    $scope.DepreciationGrid_Options = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.FLM,
            method: _FLMAsset_EQMEdit.URL.Read_Depreciation,
            readparam: function () { return { AssetID: _FLMAsset_EQMEdit.Param.AssetID } },
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
        ]
    }
    //tab 5

    //tab cuoi
    $scope.TransHistoryGrid_Options = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.FLM,
            method: _FLMAsset_EQMEdit.URL.Read_TranferHistory,
            readparam: function () { return { AssetID: _FLMAsset_EQMEdit.Param.AssetID } },
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number', editable: false, nullable: true },
                    DateTranfer: { type: 'date' }
                }
            }
        }),
        height: '100%', pageable: true, sortable: true, columnMenu: false, filterable: false, resizable: true,
        columns: [
            { field: "LocationFromName", title: 'Vị trí ban đầu', width: 200 },
            { field: "LocationToName", title: 'Vị trí sau', width: 200 },
            { field: "DateTranfer", title: 'Ngày phát sinh', width: 150, template: "#=DateTranfer==null?\"\":Common.Date.FromJsonDDMMYY(DateTranfer)#" },
            { field: "Amount", title: 'Giá trị lúc chuyển', width: 150, },
        ]
    }

    //popup location

    $scope.CboAssignToTruck_Options = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: "contains", suggest: true, dataTextField: 'Code', dataValueField: 'ID', placeholder: "Chọn xe tải",
        dataSource: Common.DataSource.Local([],
            {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    Code: { type: 'string' }
                }
            })
    }
    $scope.CboAssignToTractor_Options = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: "contains", suggest: true, dataTextField: 'Code', dataValueField: 'ID', placeholder: "Chọn đầu kéo",
        dataSource: Common.DataSource.Local([],
            {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    Code: { type: 'string' }
                }
            })
    }
    $scope.CboAssignToRomooc_Options = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: "contains", suggest: true, dataTextField: 'Code', dataValueField: 'ID', placeholder: "Chọn romooc",
        dataSource: Common.DataSource.Local([],
            {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    Code: { type: 'string' }
                }
            })
    }
    $scope.CboAssignToStock_Options = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: "contains", suggest: true, dataTextField: 'StockName', dataValueField: 'ID', placeholder: "Chọn kho",
        dataSource: Common.DataSource.Local([],
            {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    StockName: { type: 'string' }
                }
            })
    }
    Common.Services.Call($http, {
        url: Common.Services.url.FLM,
        method: _FLMAsset_EQMEdit.URL.Get_Location,
        data: {},
        success: function (res) {
            $scope.CboAssignToTruck_Options.dataSource.data(res.lstTruck)
            $scope.CboAssignToTractor_Options.dataSource.data(res.lstTractor)
            $scope.CboAssignToRomooc_Options.dataSource.data(res.lstRomooc)
            $scope.CboAssignToStock_Options.dataSource.data(res.lstStock)
        }
    });

    $scope.ChooseLocation_win_SaveClick = function ($event, win) {
        $event.preventDefault();
        switch ($scope.Location.Choose) {
            default:
            case 'truck':
                if (!$scope.Location.TruckID > 0)
                    $rootScope.Message({
                        Type: Common.Message.Type.Alert,
                        NotifyType: Common.Message.NotifyType.ERROR,
                        Title: 'Thông báo',
                        Msg: 'Chưa chọn xe',
                        Close: null,
                        Ok: null
                    })
                else {
                    $scope.EQMItem.VehicleID = $scope.Location.TruckID;
                    $scope.EQMItem.RomoocID = null;
                    $scope.EQMItem.StockID = null;
                    Common.Services.Call($http, {
                        url: Common.Services.url.FLM,
                        method: _FLMAsset_EQMEdit.URL.Save_EQM,
                        data: { item: $scope.EQMItem },
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
                break;
            case 'tractor':
                if (!$scope.Location.TractorID > 0)
                    $rootScope.Message({
                        Type: Common.Message.Type.Alert,
                        NotifyType: Common.Message.NotifyType.ERROR,
                        Title: 'Thông báo',
                        Msg: 'Chưa chọn xe',
                        Close: null,
                        Ok: null
                    })
                else {
                    $scope.EQMItem.VehicleID = $scope.Location.TractorID;
                    $scope.EQMItem.RomoocID = null;
                    $scope.EQMItem.StockID = null;
                    Common.Services.Call($http, {
                        url: Common.Services.url.FLM,
                        method: _FLMAsset_EQMEdit.URL.Save_EQM,
                        data: { item: $scope.EQMItem },
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
                break;
            case 'romooc':
                if (!$scope.Location.RomoocID > 0)
                    $rootScope.Message({
                        Type: Common.Message.Type.Alert,
                        NotifyType: Common.Message.NotifyType.ERROR,
                        Title: 'Thông báo',
                        Msg: 'Chưa chọn romooc',
                        Close: null,
                        Ok: null
                    })
                else {
                    $scope.EQMItem.RomoocID = $scope.Location.RomoocID;
                    $scope.EQMItem.VehicleID = null;
                    $scope.EQMItem.StockID = null;
                    Common.Services.Call($http, {
                        url: Common.Services.url.FLM,
                        method: _FLMAsset_EQMEdit.URL.Save_EQM,
                        data: { item: $scope.EQMItem },
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
                break;
            case 'stock':
                if (!$scope.Location.StockID > 0)
                    $rootScope.Message({
                        Type: Common.Message.Type.Alert,
                        NotifyType: Common.Message.NotifyType.ERROR,
                        Title: 'Thông báo',
                        Msg: 'Chưa chọn kho',
                        Close: null,
                        Ok: null
                    })
                else {
                    $scope.EQMItem.StockID = $scope.Location.StockID;
                    $scope.EQMItem.RomoocID = null;
                    $scope.EQMItem.VehicleID = null;
                    Common.Services.Call($http, {
                        url: Common.Services.url.FLM,
                        method: _FLMAsset_EQMEdit.URL.Save_EQM,
                        data: { item: $scope.EQMItem },
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
                break;
        }
    }

    $scope.ChooseLocation_win_CloseClick = function ($event,win) {
        $event.preventDefault();
        win.close();
    }
}])