/// <reference path="~/Scripts/jquery-1.9.1.intellisense.js" />
/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />
/// <reference path="~/Scripts/map.js" />

var _FLMAsset_Romooc_Edit = {
    URL: {
        Get_Romooc: 'FLMAsset_Romooc_Get',
        Save_Romooc: 'FLMAsset_Romooc_Update',
        Delete_Romooc: 'FLMAsset_Romooc_Destroy',

        Get_EQMList: 'FLMAsset_EQM_ReadByAsset',
        Get_FuelHistory: 'FLMAsset_FuelHistoryRead',

        Read_Consumption: 'FLMAsset_Consumption_ReadByAsset',
        Save_Consumption: 'FLMAsset_Consumption_UpdateByAsset',
        Delete_Consumption: 'FLMAsset_Consumption_Destroy',
        Read_ConsumptionNotIn: 'FLMAsset_Consumption_NotChooseRead',
        Save_ConsumptionNotIn: 'FLMAsset_Consumption_NotChooseUpdate',

        History_Depreciation: 'FLMAsset_History_DepreciationList',

        Check_Depreciation: 'FLMAsset_Depreciation_Check',
        Read_Depreciation: 'FLMAsset_Depreciation_Read',
        Save_Depreciation: 'FLMAsset_Depreciation_Update',
        Delete_Depreciation: 'FLMAsset_Depreciation_Delete',
        Generate_Depreciation: 'FLMAsset_Depreciation_Generate',

        Get_ALLDriverList: 'FLMAsset_AllDriverRead',
        Get_RomoocDriverList: 'FLMAsset_Romooc_Read',
        Get_RentList: 'FLMAsset_RentRead',

        LocationAddress_List: 'FLMAsset_Location_List',
        Location_Get: 'FLMAsset_Location_Get',
        Location_Save: 'FLMAsset_Location_Save',
        Location_Delete: 'FLMAsset_Location_Delete',

    FLM_LocationNotIn_Save: 'FLMAsset_LocationNotIn_Save',
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

angular.module('myapp').controller('FLMAsset_EditRomoocCtrl', ['$rootScope', '$scope', '$http', '$location', '$state', '$timeout', function ($rootScope, $scope, $http, $location, $state, $timeout) {
    Common.Log('FLMAsset_EditRomoocCtrl');
    $rootScope.IsLoading = false;

    $rootScope.Loading.Show();
    $scope.HasChoose = false;
    $scope.IsNewVehicle = true;
    $scope.IsEdited = false;
    $scope.TabIndex = 1;
    var LoadingStep = 30;

    $rootScope.$watch('Loading.Progress', function (v, n) {
        if ($rootScope.Loading.Progress >= 100) {
            $rootScope.Loading.Progress = 0;
            $rootScope.Loading.Hide();
        }
    })
    _FLMAsset_Romooc_Edit.Param = $.extend(true, _FLMAsset_Romooc_Edit.Param, $state.params);
    $scope.RomoocItem = null;
    $scope.ConsumptionEdit = null;
    $scope.IsConsumptionEdit = false;
    $scope.IsShowTab = false;
    if (_FLMAsset_Romooc_Edit.Param.AssetID > 0) { $scope.IsShowTab = true; $scope.IsNewVehicle = false; }

    Common.Services.Call($http, {
        url: Common.Services.url.FLM,
        method: _FLMAsset_Romooc_Edit.URL.Get_Romooc,
        data: { AssetID: _FLMAsset_Romooc_Edit.Param.AssetID },
        success: function (res) {
            $scope.RomoocItem = res;
            $rootScope.Loading.Change("Thông tin chung...", 10)
        }
    });

    $scope.FLMAsset_EditRomooc_TabOptions = {
        animation: { open: { effects: "fadeIn" } },
        select: function (e) {
            $timeout(function () {
                $scope.TabIndex = angular.element(e.item).data('tabindex'); //or
            }, 1);
        }
    }

    $scope.FLMAsset_Romooc_SaveClick = function ($event,vform) {
        $event.preventDefault();
        if (vform()) {
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.FLM,
                method: _FLMAsset_Romooc_Edit.URL.Save_Romooc,
                data: { item: $scope.RomoocItem },
                success: function (res) {
                    $rootScope.IsLoading = false;
                    $rootScope.Message({ Msg: 'Đã lưu', Type: Common.Message.Type.Notify, NotifyType: Common.Message.NotifyType.SUCCESS });
                    $state.go('main.FLMAsset.EditRomooc', { AssetID: res }, { reload: true });
                },
                error: function (e) {
                    $rootScope.IsLoading = false;
                }
            });
        }
    }

    $scope.FLMAsset_Romooc_BackClick = function ($event) {
        $event.preventDefault();
        $state.go("main.FLMAsset.Index")
    }

    $scope.FLMAsset_Romooc_DelClick = function ($event) {
        $event.preventDefault();
        $rootScope.Message({
            Type: Common.Message.Type.Confirm,
            NotifyType: Common.Message.NotifyType.SUCCESS,
            Title: 'Thông báo',
            Msg: 'Bạn muốn xóa dữ liệu này',
            Ok: function (e) {
                $rootScope.IsLoading = true;
                Common.Services.Call($http, {
                    url: Common.Services.url.FLM,
                    method: _FLMAsset_Romooc_Edit.URL.Delete_Romooc,
                    data: { item: $scope.RomoocItem },
                    success: function (res) {
                        $rootScope.IsLoading = false;
                        $state.go('main.FLMAsset.Index')
                    },
                    error: function (e) {
                        $rootScope.IsLoading = false;
                    }
                });
            }
        })


    }
    //tab 1
    $scope.masked_RegNo_Options = {
        mask: "00A-00009",
        clearPromptChar: true,
        promptChar: " ",
    }
    $scope.cboGor_options = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: "contains",
        suggest: true, dataTextField: 'GroupName', dataValueField: 'ID',
        dataSource: Common.DataSource.Local([], {
            id: 'ID',
            fields: {
                TypeName: { type: 'string' },
                ID: { type: 'number' },
            }
        }),
        change: function (e) {
            if (e.sender.selectedIndex >= 0) $scope.IsEdited = true;
        }
    }
    $rootScope.IsLoading = true;
    Common.Services.Call($http, {
        url: Common.Services.url.CAT,
        method: Common.ALL.URL.CATGroupOfRomooc,
        data: {},
        success: function (res) {
            $scope.cboGor_options.dataSource.data(res.Data);
            $rootScope.IsLoading = false;
        }
    });

    $scope.numRegCapacity_options = { format: 'n0', spinners: false, culture: 'en-US', min: 1,max:2, step: 1, decimals: 0, }

    $scope.numMaxWeight_options = { format: 'n3', spinners: false, culture: 'en-US', min: 0, step: 1, decimals: 3, }
    $scope.numRemainValue_options = {
        format: 'n0', spinners: false, culture: 'en-US', min: 0, step: 1, decimals: 0,
        change: function (e) {
            if ($scope.RomoocItem.RemainValue > 0)
                $scope.RomoocItem.CurrentValue = $scope.RomoocItem.BaseValue - $scope.RomoocItem.RemainValue;
            else
                $scope.RomoocItem.CurrentValue = $scope.RomoocItem.BaseValue;
        }
    }
    $scope.numYearOfProduction_options = { format: '0000', spinners: false, culture: 'en-US', min: 0, max: 3000, step: 1, decimals: 0, }
    $scope.numBaseValue_options = {
        format: 'n0', spinners: false, culture: 'en-US', min: 0, step: 1, decimals: 0,
        change: function (e) {
            if ($scope.RomoocItem.RemainValue > 0)
                $scope.RomoocItem.CurrentValue = $scope.RomoocItem.BaseValue - $scope.RomoocItem.RemainValue;
            else
                $scope.RomoocItem.CurrentValue = $scope.RomoocItem.BaseValue;
        }
    }
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
        method: _FLMAsset_Romooc_Edit.URL.Get_RentList,
        data: {},
        success: function (res) {
            $scope.cboRent_options.dataSource.data(res.Data)
        }
    });

    $scope.FLMAsset_Depreciation_GenerateClick = function ($event, vform) {
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
                            method: _FLMAsset_Romooc_Edit.URL.Save_Romooc,
                            data: { item: $scope.RomoocItem },
                            success: function (res) {
                                Common.Services.Call($http, {
                                    url: Common.Services.url.FLM,
                                    method: _FLMAsset_Romooc_Edit.URL.Check_Depreciation,
                                    data: { AssetID: _FLMAsset_Romooc_Edit.Param.AssetID, DepreciationPeriod: $scope.RomoocItem.DepreciationPeriod, DepreciationStart: $scope.RomoocItem.DepreciationStart },
                                    success: function (res) {
                                        Common.Services.Call($http, {
                                            url: Common.Services.url.FLM,
                                            method: _FLMAsset_Romooc_Edit.URL.Generate_Depreciation,
                                            data: { AssetID: _FLMAsset_Romooc_Edit.Param.AssetID },
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
                method: _FLMAsset_Romooc_Edit.URL.Check_Depreciation,
                data: { AssetID: _FLMAsset_Romooc_Edit.Param.AssetID, DepreciationPeriod: $scope.RomoocItem.DepreciationPeriod, DepreciationStart: $scope.RomoocItem.DepreciationStart },
                success: function (res) {
                    Common.Services.Call($http, {
                        url: Common.Services.url.FLM,
                        method: _FLMAsset_Romooc_Edit.URL.Generate_Depreciation,
                        data: { AssetID: _FLMAsset_Romooc_Edit.Param.AssetID },
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

    //map

    $scope.CreateMap = function () {
        try {
            if (google) {
                if (google.maps != undefined)
                    Map.HasMap = true;
            }
        } catch (e) { }

        _FLMAsset_Romooc_Edit.Map.Map = Map.Create($http, 'Romooc_map', 10, 10.777915, 106.699991, true, 'ROADMAP', function (event) {
            if (Common.HasValue(_FLMAsset_Romooc_Edit.Map.Marker))
                _FLMAsset_Romooc_Edit.Map.Marker.setMap(null);

            $scope.RomoocItem.Lat = event.latLng.lat();
            $scope.RomoocItem.Lng = event.latLng.lng();
            _FLMAsset_Romooc_Edit.Map.Marker = Map.Marker(_FLMAsset_Romooc_Edit.Map.Map, $scope.RomoocItem.RegNo, $scope.RomoocItem.Lat, $scope.RomoocItem.Lng, _FLMAsset_Romooc_Edit.Map.IconMarker, 1, null, null)
            $scope.IsEdited = true;

            _FLMAsset_Romooc_Edit.Map.Map.setCenter(_FLMAsset_Romooc_Edit.Map.Marker.getPosition());

        }, null);

        if (Common.HasValue($scope.RomoocItem.Lat) && Common.HasValue($scope.RomoocItem.Lng)) {
            if ($scope.RomoocItem.Lat != 0 && $scope.RomoocItem.Lng != 0) {
                _FLMAsset_Romooc_Edit.Map.Marker = Map.Marker(_FLMAsset_Romooc_Edit.Map.Map, $scope.RomoocItem.RegNo, $scope.RomoocItem.Lat, $scope.RomoocItem.Lng, _FLMAsset_Romooc_Edit.Map.IconMarker, 1, null, null)
                _FLMAsset_Romooc_Edit.Map.Map.setCenter(_FLMAsset_Romooc_Edit.Map.Marker.getPosition());
            }
        }

    }

    //tab 2

    $scope.currEQM_GridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.FLM,
            method: _FLMAsset_Romooc_Edit.URL.Get_EQMList,
            readparam: function () { return { AssetID: _FLMAsset_Romooc_Edit.Param.AssetID } },
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
            method: _FLMAsset_Romooc_Edit.URL.Get_EQMList,
            readparam: function () { return { AssetID: _FLMAsset_Romooc_Edit.Param.AssetID } },
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

    $scope.ConsumptionGrid_Options = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.FLM,
            method: _FLMAsset_Romooc_Edit.URL.Read_Consumption,
            readparam: function () { return { AssetID: _FLMAsset_Romooc_Edit.Param.AssetID } },
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number', editable: false, nullable: true },
                    MaterialName: { type: 'string', editable: false },
                    QuantityPerKM: { type: 'number', editable: true },
                    IsFuel: { type: 'boolean', editable: false },
                    GroupOfMaterialName: { type: 'string', editable: false }
                }
            }
        }),
        height: '100%', pageable: true, sortable: true, columnMenu: false, filterable: false, resizable: true, editable: { mode: 'inline' },
        dataBound: function () {
            $scope.IsConsumptionEdit = false;
            $rootScope.Loading.Change("Định mức...", $rootScope.Loading.Progress + LoadingStep);
        },
        columns: [
             {
                 title: ' ', width: '85px',
                 template: '<a ng-show="!IsConsumptionEdit" href="/" ng-click="FLMAsset_ConsumptionGrid_Edit($event,ConsumptionGrid)" class="k-button"><i class="fa fa-pencil"></i></a>' +
                     '<a ng-show="!IsConsumptionEdit" href="/" ng-click="FLMAsset_ConsumptionGrid_Delete($event,ConsumptionGrid)" class="k-button"><i class="fa fa-trash"></i></a>' +
                     '<a ng-show="IsConsumptionEdit && ConsumptionEdit.ID==#=ID#?true:false" href="/" ng-click="FLMAsset_ConsumptionGrid_Save($event,ConsumptionGrid)" class="k-button"><i class="fa fa-check "></i></a>' +
                     '<a ng-show="IsConsumptionEdit && ConsumptionEdit.ID==#=ID#?true:false" href="/" ng-click="FLMAsset_ConsumptionGrid_Cancel($event,ConsumptionGrid)" class="k-button"><i class="fa fa-ban"></i></a>',
                 filterable: false, sortable: false
             },
            { field: 'MaterialName', title: 'Vật tư', width: '200px' },
            {
                field: 'QuantityPerKM', title: 'Định mức/KM', template: '#=Common.Number.ToNumber3(QuantityPerKM)#', width: '200px',
                editor: '<input class="cus-combobox" kendo-numeric-text-box k-options="numQuantityPerKMOptions" data-bind="value:QuantityPerKM" ng-model="ConsumptionEdit.QuantityPerKM"/>'
            },
            {
                field: 'IsFuel', title: 'Nhiên liệu?', width: '100px', hidden: true,
                template: '<input type="checkbox" #= IsFuel ? "checked=checked" : "" # disabled="disabled" />',
                groupHeaderTemplate: "#=(value == true)?'Là nhiên liệu':'Không phải nhiên liệu'#",
            },
            {
                field: 'GroupOfMaterialName', title: 'Nhóm vật tư', width: '100px', hidden: true,
                template: '#=GroupOfMaterialName==null?"":GroupOfMaterialName#',
            },
        ]
    }

    $scope.FLMAsset_Consumption_AddClick = function ($event, win) {
        $event.preventDefault();
        win.center();
        win.open();
        $scope.ConsumptionNotChoose_GridOptions.dataSource.read();
    }

    $scope.numQuantityPerKMOptions = { format: 'n3', spinners: false, culture: 'en-US', min: 0, step: 1, decimals: 3, }

    $scope.ConsumptionNotChoose_GridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.FLM,
            method: _FLMAsset_Romooc_Edit.URL.Read_ConsumptionNotIn,
            readparam: function () { return { AssetID: _FLMAsset_Romooc_Edit.Param.AssetID } },
            group: [{ field: "IsFuel" }, { field: "GroupOfMaterialName" }],
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number', editable: false, nullable: true },
                    IsChoose: { type: 'boolean', defaultValue: false },
                    QuantityPerKM: { type: 'number' },
                    MaterialID: { type: 'number', editable: false },
                    PackingID: { type: 'number' },
                    MaterialName: { type: 'string', editable: false },
                    PackingName: { type: 'string' },
                    GroupOfMaterialName: { type: 'string', editable: false },
                    IsFuel: { type: 'boolean', editable: false }
                }
            }
        }),
        height: '100%', pageable: false, sortable: true, columnMenu: false, filterable: false, resizable: true, editable: 'incell', groupable: false,
        columns: [
            {
                title: ' ', width: '100px',
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,ConsumptionNotChoose_Grid,ConsumptionNotChoose_ChooseChange)" />',
                headerAttributes: { style: 'text-align: center;' },
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,ConsumptionNotChoose_Grid,ConsumptionNotChoose_ChooseChange)" />',
                filterable: false, sortable: false
            },
            { field: 'MaterialName', title: 'Vật tư', template: '#:MaterialName#', width: '200px' },
            {
                field: 'QuantityPerKM', title: 'Định mức/KM', template: '#=Common.Number.ToNumber3(QuantityPerKM)#', width: '200px',
                editor: function (container, options) {
                    $('<input data-bind="value:' + options.field + '" />').appendTo(container).kendoNumericTextBox({
                        format: 'n3', spinners: false, culture: 'en-US', min: 0, step: 1, decimals: 3,
                    })
                }
            },
            {
                field: 'IsFuel', title: 'Nhiên liệu?', width: '100px', hidden: true,
                template: '<input type="checkbox" #= IsFuel ? "checked=checked" : "" # disabled="disabled" />',
                groupHeaderTemplate: "#=(value == true)?'Là nhiên liệu':'Không phải nhiên liệu'#",
            },
            {
                field: 'GroupOfMaterialName', title: 'Nhóm vật tư', width: '100px', hidden: true,
                template: '#=GroupOfMaterialName==null?"":GroupOfMaterialName#',
            },
        ]
    }
    $scope.ConsumptionNotChoose_ChooseChange = function ($event, grid, haschoose) {
        $scope.HasChoose = haschoose;
    };

    $scope.FLMAsset_ConsumptionGrid_Edit = function ($event, grid) {
        $scope.IsConsumptionEdit = true;
        var tr = $event.target.closest('tr');
        $scope.ConsumptionEdit = grid.dataItem(tr);
        _FLMAsset_Romooc_Edit.Data.ItemBackup = $.extend(true, {}, $scope.ConsumptionEdit);
        grid.editRow(tr);
        var td = $(tr).find('input');
        td[0].focus();
    }

    $scope.FLMAsset_ConsumptionGrid_Save = function ($event, grid) {
        $event.preventDefault();

        $rootScope.IsLoading = true;
        $scope.IsConsumptionEdit = false;
        Common.Services.Call($http, {
            url: Common.Services.url.FLM,
            method: _FLMAsset_Romooc_Edit.URL.Save_Consumption,
            data: { item: $scope.ConsumptionEdit, AssetID: _FLMAsset_Romooc_Edit.Param.AssetID },
            success: function (res) {
                $rootScope.IsLoading = false;
                $scope.ConsumptionGrid_Options.dataSource.read()
            }
        });

    }

    $scope.FLMAsset_ConsumptionGrid_Delete = function ($event, grid) {
        $event.preventDefault();

        var obj = grid.dataItem($event.target.closest('tr'));
        if (Common.HasValue(obj)) {
            Common.Services.Call($http, {
                url: Common.Services.url.FLM,
                method: _FLMAsset_Romooc_Edit.URL.Delete_Consumption,
                data: { item: obj },
                success: function (res) {
                    $scope.ConsumptionGrid_Options.dataSource.read()
                    $rootScope.IsLoading = false;
                }
            });
        }
    }

    $scope.FLMAsset_ConsumptionGrid_Cancel = function ($event, grid) {
        $event.preventDefault();
        $rootScope.IsLoading = true;
        $scope.IsConsumptionEdit = false;
        var i = 0;
        var data = grid.dataSource.data();
        while (i < data.length) {
            var obj = data[i];
            if (obj.ID == _FLMAsset_Romooc_Edit.Data.ItemBackup.ID) {
                data.splice(i, 1);
                data.splice(i, 0, $.extend(true, {}, _FLMAsset_Romooc_Edit.Data.ItemBackup))
            }
            i++;
        }
        $rootScope.IsLoading = false;
    }

    $scope.ConsumptionNotChoose_Win_SaveChooseClick = function ($event, win, grid) {
        $event.preventDefault();

        var data = grid.dataSource.data();
        var dataChoose = $.grep(data, function (o) { return o.IsChoose == true });
        var checkError = $.grep(dataChoose, function (o) { return o.QuantityPerKM == 0 });
        if (dataChoose.length > 0) {
            if (checkError.length == 0) {
                $rootScope.Message({
                    Type: Common.Message.Type.Confirm,
                    NotifyType: Common.Message.NotifyType.SUCCESS,
                    Title: 'Thông báo',
                    Msg: 'Đồng ý thêm các định mức đã chọn',
                    Action: true,
                    Close: null,
                    Ok: function (e) {
                        Common.Services.Call($http, {
                            url: Common.Services.url.FLM,
                            method: _FLMAsset_Romooc_Edit.URL.Save_ConsumptionNotIn,
                            data: { lst: dataChoose, AssetID: _FLMAsset_Romooc_Edit.Param.AssetID },
                            success: function (res) {
                                win.close();
                                $scope.ConsumptionGrid_Options.dataSource.read();
                            }
                        });
                    },
                    pars: null
                })
            }
            else {
                $rootScope.Message({
                    Type: Common.Message.Type.Alert,
                    NotifyType: Common.Message.NotifyType.ERROR,
                    Title: 'Thông báo',
                    Msg: 'Điền định mức/km trước khi lưu',
                    Action: true,
                    Close: null,
                    Ok: null,
                    pars: null
                })
            }
        }
        else {
            $rootScope.Message({
                Type: Common.Message.Type.Alert,
                NotifyType: Common.Message.NotifyType.ERROR,
                Title: 'Thông báo',
                Msg: 'Chưa chọn vật tư',
                Action: true,
                Close: null,
                Ok: null,
                pars: null
            })
        }
    }
    $scope.ConsumptionNotChoose_Win_CloseClick = function ($event, win) {
        $event.preventDefault();
        win.close();
    }
    //tab 3
    $scope.DepreciationGrid_Options = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.FLM,
            method: _FLMAsset_Romooc_Edit.URL.History_Depreciation,
            readparam: function () { return { AssetID: _FLMAsset_Romooc_Edit.Param.AssetID } },
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
            $rootScope.Loading.Change("Lịch sử khấu hao...", $rootScope.Loading.Progress + LoadingStep);
        }
    }
    //tab 5

    //tab cuoi
    $scope.FuelHistoryGrid_Options = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.FLM,
            method: _FLMAsset_Romooc_Edit.URL.Get_FuelHistory,
            readparam: function () { return { AssetID: _FLMAsset_Romooc_Edit.Param.AssetID } },
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number', editable: false, nullable: true },
                    DateReceipt: { type: 'date' }
                }
            }
        }),
        height: '100%', pageable: true, sortable: true, columnMenu: false, filterable: false, resizable: true,
        columns: [
            { field: "DateReceipt", title: 'Ngày phát sinh', width: '15%', template: "#=DateReceipt==null?\"\":Common.Date.FromJsonDDMMYY(DateReceipt)#" },
            { field: "MaterialName", title: 'Tên vật tư', width: '15%', },
            { field: "GroupOfMaterialName", title: 'Loại vật tư', width: '15%', },
            { field: "Quantity", title: 'Số lượng', width: '10%', attributes: { style: "text-align: right; " }, },
            { field: "Price", title: 'Đơn giá', width: '15%', attributes: { style: "text-align: right; " }, },
            { field: "Amount", title: 'Thành tiền', width: '15%', attributes: { style: "text-align: right; " }, },
            { field: "Code", title: 'Mã phiếu', width: '15%', attributes: { style: "text-align: right; " }, }
        ],
        dataBound: function (e) {
            $rootScope.Loading.Change("Lịch sử cấp phát vật tư...", $rootScope.Loading.Progress + LoadingStep);
        }
    }

    //#region LocationAddress
    $scope.LocationAddress_gridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.FLM,
            method: _FLMAsset_Romooc_Edit.URL.LocationAddress_List,
            pageSize: 50,
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    Code: { type: 'string' },
                    Location: { type: 'string' },
                    Address: { type: 'string' },
                    Lat: { type: 'number' },
                    Lng: { type: 'number' },
                    CountryName: { type: 'string' },
                }
            }
        }),
        selectable: "multiple row",
        height: '100%', pageable: true, sortable: true, columnMenu: false, resizable: true, reorderable: true, filterable: { mode: 'row' },
        //toolbar: kendo.template($('#CATLocation_grid_toolbar').html()),
        columns: [
            {
                title: ' ', width: '135px',
                template: '<a href="/" ng-click="LocationEdit_Click($event,LocationEdit_win,dataItem)" class="k-button"><i class="fa fa-pencil"></i></a>' +
                    '<a href="/" ng-click="LocationMap_Click($event,LocationMap_win,dataItem)" class="k-button"><i class="fa fa-map-marker"></i></a>' +
                    '<a href="/" ng-click="LocationDestroy_Click($event,dataItem)" class="k-button"><i class="fa fa-trash"></i></a>',
                filterable: false, sortable: false
            },
            {
                field: 'Code', title: '{{RS.CATLocation.Code}}', width: 100,
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'Location', title: '{{RS.CATLocation.Location}}', width: 200,
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'Address', title: '{{RS.CATLocation.Address}}', width: 250,
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'EconomicZone', title: '{{RS.CATLocation.EconomicZone}}', width: 150,
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'DistrictName', title: '{{RS.CATDistrict.DistrictName}}', width: 150,
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'ProvinceName', title: '{{RS.CATProvince.ProvinceName}}', width: 150,
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'Note', title: '{{RS.CATLocation.Note}}', width: 150,
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'Note1', title: '{{RS.CATLocation.Note1}}', width: 150,
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'Lat', title: '{{RS.CATLocation.Lat}}', width: 150,
                template: "#=Lat==null?\"\":Lat#",
                filterable: {
                    cell: {
                        template: function (container) {
                            container.element.kendoNumericTextBox({
                                format: '#,##0.00000', spinners: false, culture: 'en-US', min: 0, step: 1, decimals: 5,
                            })
                        },
                        operator: "gte",
                        showOperators: false
                    }
                }
            },
            {
                field: 'Lng', title: '{{RS.CATLocation.Lng}}', width: 150,
                template: "#=Lng==null?\"\":Lng#",
                filterable: {
                    cell: {
                        template: function (container) {
                            container.element.kendoNumericTextBox({
                                format: '#,##0.00000', spinners: false, culture: 'en-US', min: 0, step: 1, decimals: 5,
                            })
                        },
                        operator: "gte",
                        showOperators: false
                    }
                }
            },
            {
                field: 'CreateBy', title: '{{RS.CATLocation.CreateBy}}', width: 150,
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'CreateDate', title: '{{RS.CATLocation.CreateDate}}', width: 150,
                template: '#=CreateDate==null?"":Common.Date.FromJsonDMYHM(CreateDate)#',
                filterable: { cell: { operator: 'gte', showOperators: false } }
            },
            {
                field: 'GroupOfLocationName', title: 'Loại địa điểm', width: 150,
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'LoadTimeCO', title: 'LoadTimeCO', width: 150,
                filterable: { cell: { operator: 'equal', showOperators: false } }
            },
            {
                field: 'UnLoadTimeCO', title: 'UnLoadTimeCO', width: 150,
                filterable: { cell: { operator: 'equal', showOperators: false } }
            },
            {
                field: 'LoadTimeDI', title: 'LoadTimeDI', width: 150,
                filterable: { cell: { operator: 'equal', showOperators: false } }
            },
            {
                field: 'UnLoadTimeDI', title: 'UnLoadTimeDI', width: 150,
                filterable: { cell: { operator: 'equal', showOperators: false } }
            },
        ]
    };

    $scope.LocationEdit_Click = function ($event, win, data) {
        $event.preventDefault();
        $scope.LoadItem(win, data.ID);
    }

    $scope.LocationAddress_Win_AddClick = function ($event, win) {
        $event.preventDefault();
        $scope.LoadItem(win, 0);
    }

    $scope.LoadItem = function (win, id) {
        $rootScope.IsLoading = true;
        Common.Services.Call($http, {
            url: Common.Services.url.FLM,
            method: _FLMAsset_Romooc_Edit.URL.Location_Get,
            data: { 'ID': id },
            success: function (res) {
                $scope.LocationItem = res;
                $scope.LoadRegionData($scope.LocationItem);
                win.center();
                win.open();
                $rootScope.IsLoading = false;
            }
        });
    }

    $scope.LocationAddress_Click = function ($event, win) {
        $event.preventDefault();
        win.center().open();
        $timeout(function () {
            $scope.LocationAddress_grid.resize();
        }, 1);
    }

    $scope.LocationEdit_win_cboCountryOptions = {
        autoBind: true,
        valuePrimitive: true,
        ignoreCase: true,
        filter: 'contains',
        suggest: true,
        dataTextField: 'CountryName',
        dataValueField: 'ID',
        minLength: 3,
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    CountryName: { type: 'string' },
                }
            }
        }),
        change: function (e) {
            var cbo = this;
            if (e.sender.selectedIndex >= 0) {
                $scope.LocationItem.ProvinceID = -1;
                $scope.LocationItem.DistrictID = -1;
                $scope.LocationItem.WardID = "";
                $scope.LoadRegionData($scope.LocationItem);
            }
            else {
                $scope.LocationItem.CountryID = "";
                $scope.LocationItem.ProvinceID = "";
                $scope.LocationItem.DistrictID = "";
                $scope.LocationItem.WardID = "";
                $scope.LoadRegionData($scope.LocationItem);
            }
        }
    }

    Common.ALL.Get($http, {
        url: Common.ALL.URL.Country,
        success: function (data) {
            _FLMAsset_Romooc_Edit.Data.Country = data;
            $scope.LocationEdit_win_cboCountryOptions.dataSource.data(data);
        }
    })

    $scope.LocationEdit_win_cboProvinceOptions = {
        autoBind: true,
        valuePrimitive: true,
        ignoreCase: true,
        filter: 'contains',
        suggest: true,
        dataTextField: 'ProvinceName',
        dataValueField: 'ID',
        minLength: 3,
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    ProvinceName: { type: 'string' },
                }
            }
        }),
        change: function (e) {
            var cbo = this;
            if (e.sender.selectedIndex >= 0) {
                $scope.LocationItem.DistrictID = -1;
                $scope.LocationItem.WardID = "";
                $scope.LoadRegionData($scope.LocationItem);
            }
            else {
                $scope.LocationItem.ProvinceID = "";
                $scope.LocationItem.DistrictID = "";
                $scope.LocationItem.WardID = "";
                $scope.LoadRegionData($scope.LocationItem);
            }
        }
    }

    Common.ALL.Get($http, {
        url: Common.ALL.URL.Province,
        success: function (data) {
            _FLMAsset_Romooc_Edit.Data.Province = {};
            angular.forEach(data, function (obj, indx) {
                if (Common.HasValue(_FLMAsset_Romooc_Edit.Data.Province[obj.CountryID]))
                    _FLMAsset_Romooc_Edit.Data.Province[obj.CountryID].push(obj);
                else _FLMAsset_Romooc_Edit.Data.Province[obj.CountryID] = [obj];
            })
        }
    })

    $scope.LocationEdit_win_cboDistrictOptions = {
        autoBind: true,
        valuePrimitive: true,
        ignoreCase: true,
        filter: 'contains',
        suggest: true,
        dataTextField: 'DistrictName',
        dataValueField: 'ID',
        minLength: 3,
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    DistrictName: { type: 'string' },
                }
            }
        }),
        change: function (e) {
            var cbo = this;
            if (e.sender.selectedIndex >= 0) {
                $scope.LocationItem.WardID = "";
                $scope.LoadRegionData($scope.LocationItem);
            }
            else {
                $scope.LocationItem.DistrictID = "";
                $scope.LocationItem.WardID = "";
                $scope.LoadRegionData($scope.LocationItem);
            }
        }
    }

    Common.ALL.Get($http, {
        url: Common.ALL.URL.District,
        success: function (data) {
            _FLMAsset_Romooc_Edit.Data.District = {};
            angular.forEach(data, function (obj, indx) {
                if (Common.HasValue(_FLMAsset_Romooc_Edit.Data.District[obj.ProvinceID]))
                    _FLMAsset_Romooc_Edit.Data.District[obj.ProvinceID].push(obj);
                else _FLMAsset_Romooc_Edit.Data.District[obj.ProvinceID] = [obj];
            })
        }
    });

    $scope.LocationEdit_win_cboWardOptions = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: "contains", suggest: true,
        dataTextField: 'WardName', dataValueField: 'ID',
        dataSource: Common.DataSource.Local([], {
            id: 'ID',
            fields: {
                WardName: { type: 'string' },
                ID: { type: 'number' },
            }
        }),
        change: function (e) {
            var cbo = this;
            if (e.sender.selectedIndex >= 0) {
            }
            else {
                $scope.LocationItem.WardID = "";
            }
        }
    }

    $scope.LocationEdit_win_cboGOLOptions = {
        autoBind: true,
        valuePrimitive: true,
        ignoreCase: true,
        filter: 'contains',
        suggest: true,
        dataTextField: 'GroupName',
        dataValueField: 'ID',
        minLength: 3,
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    GroupName: { type: 'string' },
                }
            }
        }),
        change: function (e) {
        }
    }

    Common.ALL.Get($http, {
        url: Common.ALL.URL.CATGroupOfLocation,
        success: function (data) {
            var item = { ID: -1, GroupName: '' };
            data.unshift(item);
            $scope.LocationEdit_win_cboGOLOptions.dataSource.data(data)
        }
    });
    $scope.LoadRegionData = function (item) {
        Common.Log("LoadRegionData");
        try {
            var countryID = item.CountryID;
            var provinceID = item.ProvinceID;
            var districtID = item.DistrictID;
            var wardID = item.WardID;

            var data = _FLMAsset_Romooc_Edit.Data.Province[countryID];
            $scope.LocationEdit_win_cboProvinceOptions.dataSource.data(data);
            if (Common.HasValue(provinceID)) {
                if (provinceID < 0) {
                    provinceID = data[0].ID;
                }
            }
            $timeout(function () {
                item.ProvinceID = provinceID;
            }, 1)

            data = _FLMAsset_Romooc_Edit.Data.District[provinceID];
            $scope.LocationEdit_win_cboDistrictOptions.dataSource.data(data);
            if (Common.HasValue(districtID)) {
                if (districtID < 0) {
                    districtID = data[0].ID;
                }
            }
            $timeout(function () {
                item.DistrictID = districtID;
            }, 1)
        }
        catch (e) { }
    }

    $scope.LocationSave_Click = function ($event, win, vform) {
        $event.preventDefault();
        if (vform()) {
            if (Common.HasValue($scope.LocationItem.CountryID) && $scope.LocationItem.CountryID > 0) {
                if (Common.HasValue($scope.LocationItem.ProvinceID) && $scope.LocationItem.ProvinceID > 0) {
                    if (Common.HasValue($scope.LocationItem.DistrictID) && $scope.LocationItem.DistrictID > 0) {
                        Common.Services.Call($http, {
                            url: Common.Services.url.FLM,
                            method: _FLMAsset_Romooc_Edit.URL.Location_Save,
                            data: { item: $scope.LocationItem },
                            success: function (res) {
                                win.close();
                                $scope.LocationAddress_grid.dataSource.read();
                                $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS })
                            }
                        });
                    } else $rootScope.Message({ Msg: 'Quận huyện không chính xác', Type: Common.Message.Type.Alert, NotifyType: Common.Message.NotifyType.ERROR });
                } else $rootScope.Message({ Msg: 'Tỉnh thành không chính xác', Type: Common.Message.Type.Alert, NotifyType: Common.Message.NotifyType.ERROR });
            }
            else $rootScope.Message({ Msg: 'Quốc gia không chính xác', Type: Common.Message.Type.Alert, NotifyType: Common.Message.NotifyType.ERROR });
        }
    }

    $scope.LocationDestroy_Click = function ($event, data) {
        $event.preventDefault();
        $rootScope.Message({
            Msg: "Xác nhận xóa?",
            Type: Common.Message.Type.Confirm,
            Ok: function () {
                Common.Services.Call($http, {
                    url: Common.Services.url.FLM,
                    method: _FLMAsset_Romooc_Edit.URL.Location_Delete,
                    data: { 'item': data },
                    success: function (res) {
                        $scope.LocationAddress_grid.dataSource.read();
                        $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS })
                    }
                });
            }
        })
    }
    $scope.LocationAddress_Win_SaveClick = function ($event, win, grid) {
        var data = $scope.LocationAddress_grid.dataItem($scope.LocationAddress_grid.select());

        if (Common.HasValue(data)) {
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.FLM,
                method: _FLMAsset_Romooc_Edit.URL.FLM_LocationNotIn_Save,
                data: { locationID: data.ID, assetID: $scope.RomoocItem.ID },
                success: function (res) {
                    $rootScope.IsLoading = false;
                    $scope.LocationAddress_gridOptions.dataSource.read();
                    $scope.RomoocItem.LocationAddress = data.Address;
                    win.close();
                    $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                },
                error: function (res) {
                    $rootScope.IsLoading = false;
                }
            });

        }
        else $rootScope.Message({ Msg: 'Chưa chọn điểm', NotifyType: Common.Message.NotifyType.ERROR });
    }
    $scope.LocationEdit_win_numLatOptions = { format: '#,##0.00000', spinners: false, culture: 'en-US', min: 0, step: 1, decimals: 5, }
    $scope.LocationEdit_win_numLngOptions = { format: '#,##0.00000', spinners: false, culture: 'en-US', min: 0, step: 1, decimals: 5, }
    $scope.LocationEdit_win_numLoadTimeCOOptions = { format: '#,##0.00', spinners: false, culture: 'en-US', min: 0, step: 1, decimals: 2, }
    $scope.LocationEdit_win_numUnLoadTimeCOOptions = { format: '#,##0.00', spinners: false, culture: 'en-US', min: 0, step: 1, decimals: 2, }
    $scope.LocationEdit_win_numLoadTimeDIOptions = { format: '#,##0.00', spinners: false, culture: 'en-US', min: 0, step: 1, decimals: 2, }
    $scope.LocationEdit_win_numUnLoadTimeDIOptions = { format: '#,##0.00', spinners: false, culture: 'en-US', min: 0, step: 1, decimals: 2, }

    //openMap.Create({
    //    Element: 'map',
    //    Tooltip_Show: true,
    //    Tooltip_Element: 'map_tooltip',
    //    InfoWin_Show: false,
    //    ClickMarker: null,
    //    ClickMap: function (e) {

    //        openMap.SetVisible(null, false);
    //        openMap.Clear();
    //        $timeout(function () {
    //            var img = Common.String.Format(openMap.mapImage.Marker_Yellow);
    //            var icon = openMap.mapStyle.Icon(img, 1);
    //            var o = openMap._to4326(e);
    //            $scope.locationMap.Lat = o[1];
    //            $scope.locationMap.Lng = o[0];
    //            $scope.mapEdited = true;
    //            var marker = openMap.Marker(o[1], o[0], '', icon, '');
    //            openMap.FitBound([marker]);
    //        }, 100)
    //    }
    //});

    $scope.LocationMap_Click = function ($event, win, data) {
        $event.preventDefault();
        $rootScope.IsLoading = true;
        Common.Services.Call($http, {
            url: Common.Services.url.FLM,
            method: _FLMAsset_Romooc_Edit.URL.Location_Get,
            data: { 'ID': data.id },
            success: function (res) {
                $scope.LocationItem = res;
                $scope.mapEdited = false;
                $rootScope.IsLoading = false;
                $scope.InitMap(win, $scope.LocationItem);
                win.center();
                win.open();
            }
        });
    }

    $scope.InitMap = function (win, data) {
        if (Common.HasValue(data)) {
            openMap.SetVisible(null, false);
            openMap.Clear();
            $timeout(function () {
                if (data.Lat > 0 && data.Lng > 0) {
                    var img = Common.String.Format(openMap.mapImage.Marker_Red);
                    var icon = openMap.mapStyle.Icon(img, 1);
                    var marker = openMap.Marker(data.Lat, data.Lng, data.Address, icon, data);
                    openMap.FitBound([marker]);
                }
            }, 100)
        }
        win.center();
        win.open();
    }

    $scope.LocationMapSave_Click = function ($event, win) {
        $event.preventDefault();
        if ($scope.mapEdited) {
            $rootScope.Message({
                Type: Common.Message.Type.Confirm,
                NotifyType: Common.Message.NotifyType.SUCCESS,
                Title: 'Thông báo',
                Msg: 'Bạn muốn lưu địa điểm đã chọn?',
                Close: function () {
                    if (Common.HasValue($scope.LocationItem)) {
                        openMap.SetVisible(null, false);
                        openMap.Clear();
                        $timeout(function () {
                            if ($scope.LocationItem.Lat > 0 && $scope.LocationItem.Lng > 0) {
                                var img = Common.String.Format(openMap.mapImage.Marker_Red);
                                var icon = openMap.mapStyle.Icon(img, 1);
                                var marker = openMap.Marker($scope.LocationItem.Lat, $scope.LocationItem.Lng, $scope.LocationItem.Address, icon, $scope.LocationItem);
                                openMap.FitBound([marker]);
                            }
                        }, 100)
                    }
                },
                Ok: function () {
                    $rootScope.IsLoading = true;
                    $scope.LocationItem.Lat = $scope.locationMap.Lat;
                    $scope.LocationItem.Lng = $scope.locationMap.Lng;
                    Common.Services.Call($http, {
                        url: Common.Services.url.FLM,
                        method: _FLMAsset_Romooc_Edit.URL.Location_Save,
                        data: { item: $scope.LocationItem },
                        success: function (res) {
                            win.close();
                            $scope.LocationAddress_grid.dataSource.read();
                            $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                            $rootScope.IsLoading = false;
                        }
                    });
                }
            })
        }
    }
    $scope.Win_Close = function ($event, win) {
        $event.preventDefault();
        win.close();
    }
    //#endregion
}])
