/// <reference path="~/Scripts/jquery-1.9.1.intellisense.js" />
/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />

//#region URL
var _FLMStand = {
    URL: {
        Read: 'FLMStand_List',
        Delete: 'FLMStand_Delete',
        Save: 'FLMStand_Save',
        Get: 'FLMStand_Get',
        Location_List: 'FLMStand_Location_List',

        Truck_List: 'FLMStand_Truck_List',
        Truck_NotInList: 'Truck_NotInList',
        Truck_SaveList: 'FLMStand_TruckSave',
        Truck_Delete: 'FLMStand_Truck_Delete',

        Tractor_List: 'FLMStand_Tractor_List',
        Tractor_NotInList: 'Tractor_NotInList',
        Tractor_SaveList: 'FLMStand_TractorSave',
        Tractor_Delete: 'FLMStand_Tractor_Delete',

        Romooc_List: 'FLMStand_Romooc_List',
        Romooc_NotInList: 'Romooc_NotInList',
        Romooc_SaveList: 'FLMStand_RomoocSave',
        Romooc_Delete: 'FLMStand_Romooc_Delete',

        Stand_ExcelInit: 'FLMStand_ExcelInit',
        Stand_ExcelChange: 'FLMStand_ExcelChange',
        Stand_ExcelImport: 'FLMStand_ExcelImport',
        Stand_ExcelApprove: 'FLMStand_ExcelApprove',


    },
    ExcelKey: {
        ResourceStand: "FLMStand",

        Stand: "FLMStand",
    }

}
//#endregion

angular.module('myapp').controller('FLMStand_IndexCtrl', ['$rootScope', '$scope', '$http', '$location', '$state', '$timeout', function ($rootScope, $scope, $http, $location, $state, $timeout) {
    if ($rootScope.IsPageComplete != true) return;
    Common.Log('FLMStand_IndexCtrl');
    $rootScope.IsLoading = false;

    $scope.Auth = $rootScope.GetAuth();

    $scope.Item = {ID:0,};
        
    $scope.SettingHasChoose = false;

    $scope.FLMStand_TabIndex = 0;
    $scope.FLMStand_TabOptions = {
        animation: {
            open: { effects: "fadeIn" }
        },
        select: function (e) {
            $timeout(function () {
                $scope.FLMStand_TabIndex = angular.element(e.item).data('tabindex');
                Common.Log("Select_Tab_" + $scope.TabIndex);
            }, 1)
        }
    }

    $scope.FLMStand_gridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.FLM,
            method: _FLMStand.URL.Read,
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    Code: { type: 'string' },
                    StandName: { type: 'string' },
                    LocationName: { type: 'string' },
                }
            }
        }),
        height: '100%', pageable: true, sortable: true, columnMenu: false, resizable: true, reorderable: true, filterable: { mode: 'row' },
        columns: [
            {
                title: ' ', width: '35px',
                template: '<a href="/" ng-click="FLMStandEdit_Click($event,FLMStand_win,FLMStand_grid,vform)" class="k-button"><i class="fa fa-pencil"></i></a>' +
                '<a href="/" ng-click="FLMStandDestroy_Click($event,dataItem)" class="k-button"><i class="fa fa-trash"></i></a>',
                filterable: false, sortable: false
            },
            {
                field: 'Code', title: 'Mã bãi', width: '150px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'StandName', title: 'Tên bãi', width: '150px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'LocationName', title: 'Địa điểm', width: '150px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
        ]
    };


    $scope.Save_Click = function ($event, win, vform) {
        $event.preventDefault();
        if (vform()) {
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.FLM,
                method: _FLMStand.URL.Save,
                data: { item: $scope.Item },
                success: function (res) {
                    debugger
                    win.close();
                    $rootScope.Message({ Msg: "Đã cập nhật!", NotifyType: Common.Message.NotifyType.SUCCESS });
                    $rootScope.IsLoading = false;
                    $scope.FLMStand_gridOptions.dataSource.read();
                }
            });
        }
    }
    $scope.LoadItem = function (win, id, vform) {
        $rootScope.IsLoading = true;
        vform({ clear: true });
        Common.Services.Call($http, {
            url: Common.Services.url.FLM,
            method: _FLMStand.URL.Get,
            data: { 'ID': id },
            success: function (res) {
                $rootScope.IsLoading = false;
                $scope.Item = res;
                win.center().open();
            }
        });
    }
    $scope.FLMStand_AddNew_Click = function ($event, win, vform) {
        $event.preventDefault();
        $scope.FLMStand_Tab.select(0);
        $scope.LoadItem(win, 0, vform);
    }
    $scope.LocationID = 0;
    $scope.FLMStandEdit_Click = function ($event, win, grid, vform) {
        $event.preventDefault();
        Common.Log("FLMStandEdit_Click");
        var tr = $($event.target).closest('tr');
        var item = grid.dataItem(tr);
        if (Common.HasValue(item))
            $scope.Location = item.LocationName;
        $scope.LocationID = item.ID;
        $scope.LoadItem(win, item.ID, vform);
        $scope.Item.ID = item.ID;
        $scope.FLMStand_Truck_GridOptions.dataSource.read();
        $scope.FLMStand_Tractor_GridOptions.dataSource.read();
        $scope.FLMStand_Romooc_GridOptions.dataSource.read();
    }

    $scope.FLMStandDestroy_Click = function ($event, item) {
        $event.preventDefault();
        Common.Log("FLMStandDestroy_Click");
        $rootScope.IsLoading = true;
        if (Common.HasValue(item)) {
            Common.Services.Call($http, {
                url: Common.Services.url.FLM,
                method: _FLMStand.URL.Delete,
                data: { 'item': item },
                success: function (res) {
                    $rootScope.Message({ Msg: "Đã xóa!", NotifyType: Common.Message.NotifyType.INFO });
                    $rootScope.IsLoading = false;
                    $scope.FLMStand_gridOptions.dataSource.read();
                }
            });
        }
    }

    $scope.Location_Click = function ($event, win) {
        $event.preventDefault();
        win.center().open();
        $timeout(function () {
            $scope.LocationAddress_gridOptions.dataSource.read();
            $scope.LocationAddress_grid.resize();
        }, 100);
    }
    $scope.LocationAddress_gridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.FLM,
            method: _FLMStand.URL.Location_List,
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
                    IsChoose: { type: 'bool' },
                }
            }
        }),
        selectable: "multiple row",
        height: '100%', pageable: true, sortable: true, columnMenu: false, resizable: true, reorderable: true, filterable: { mode: 'row' },
        columns: [

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

    $scope.Location = "";
    $scope.LocationAddress_Win_SaveClick = function ($event, win) {
        $event.preventDefault();

        var data = $scope.LocationAddress_grid.dataItem($scope.LocationAddress_grid.select());

        if (Common.HasValue(data)) {
            $scope.Location = data.Location;
            $scope.Item.LocationID = data.ID;
            win.close();
        }
        else $rootScope.Message({ Msg: 'Chưa chọn điểm', NotifyType: Common.Message.NotifyType.ERROR });
    }

    $scope.LocationAddress_GridChoose_Change = function ($event, grid, hasChoose) {
        $scope.SettingHasChoose = hasChoose;
    }
    //#region Truck
    $scope.TruckHasChoose = false;
    $scope.FLMStand_Truck_GridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.FLM,
            method: _FLMStand.URL.Truck_List,
            readparam: function () { return { standID: $scope.Item.ID, } },
            model: {
                id: 'ID',
                fields: {
                    ID: { field: 'ID', type: 'number', editable: false, nullable: false },
                    WarrantyEnd: { type: 'date' },
                    IsRent: { type: 'string' },
                    GroupOfVehicleName: { type: 'string' },
                    RegWeight: { type: 'number' },
                    GPSCode: { type: 'string' },
                    MinWeight: { type: 'number' },
                    MaxWeight: { type: 'number' },
                    RegCapacity: { type: 'number' },
                    MinCapacity: { type: 'number' },
                    MaxCapacity: { type: 'number' },
                    YearOfProduction: { type: 'number' },
                    URL: { type: 'string' },
                    IsChoose: { type: 'boolean' },
                }
            }
        }),
        height: '100%', pageable: true, sortable: true, columnMenu: false, filterable: { mode: 'row' }, resizable: true,
        columns: [
            {
                title: ' ', width: '35px',
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,FLMStand_Truck_Grid,Truck_GridChoose_Change)" />',
                headerAttributes: { style: 'text-align: center;' }, attributes: { style: 'text-align: center;' },
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,FLMStand_Truck_Grid,Truck_GridChoose_Change)" />',
                filterable: false, sortable: false
            },
            {
                field: "RegNo", title: 'Số xe', width: 100,
                filterable: { cell: { showOperators: false, operator: "contains" } }
            },
            { field: "IsDefault", title: 'Mặc định', width: 100, attributes: { style: 'text-align: center;' }, template: '<input type="checkbox" ng-model="dataItem.IsDefault" disabled/>', filterable: false, sortable: false },
            {
                field: "RegWeight", title: 'Trọng tải đăng kí', width: 120,
                template: "#=RegWeight==null?\"\":RegWeight#", filterable: { cell: { showOperators: false, operator: "eq" } }
            },
            {
                field: "MinWeight", title: 'Trọng tải tối thiểu', width: 120,
                template: "#=MinWeight==null?\"\":MinWeight#", filterable: { cell: { showOperators: false, operator: "eq" } }
            },
            {
                field: "MaxWeight", title: 'Trọng tải tối đa', width: 120,
                template: "#=MaxWeight==null?\"\":MaxWeight#", filterable: { cell: { showOperators: false, operator: "eq" } }
            },
            {
                field: "RegCapacity", title: 'Đăng kí', width: '100px',
                template: "#=RegCapacity==null?\"\":RegCapacity#", filterable: { cell: { showOperators: false, operator: "eq" } }
            },
            {
                field: "MinCapacity", title: 'Tối thiểu', width: '100px',
                template: "#=MinCapacity==null?\"\":MinCapacity#", filterable: { cell: { showOperators: false, operator: "eq" } }
            },
            {
                field: "MaxCapacity", title: 'Tối đa', width: '100px',
                template: "#=MaxCapacity==null?\"\":MaxCapacity#", filterable: { cell: { showOperators: false, operator: "eq" } }
            },
            {
                field: 'GPSCode', title: 'Mã GPS', width: 100, headerAttributes: { style: "text-align: center;" },
                template: "#=GPSCode==null?\"\":GPSCode#",
                filterable: { cell: { showOperators: false, operator: "contains" } }
            },
            {
                field: 'YearOfProduction', title: 'Năm sản xuất', width: 110, headerAttributes: { style: "text-align: center;" },
                filterable: { cell: { showOperators: false, operator: "eq" } }
            },
            {
                field: 'Manufactor', title: 'Nhà sản xuất', width: 110,
                template: "#=Manufactor==null?\"\":Manufactor#", filterable: { cell: { showOperators: false, operator: "contains" } }
            },
            {
                field: 'WarrantyEnd', title: 'Ngày kết thúc BH', width: '145px', headerAttributes: { style: "text-align: center" },
                template: "#=WarrantyEnd==null?\"\":kendo.toString(WarrantyEnd,'dd/MM/yyyy')#",
                filterable: { cell: { template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); }, operator: 'equal', showOperators: false } },
            },
            { title: "", sortable: false, filterable: false }
        ]
    }
    $scope.Truck_GridChoose_Change = function ($event, grid, hasChoose) {
        $scope.TruckHasChoose = hasChoose;
    }

    $scope.Truck_AddNew = function ($event, win) {
        $event.preventDefault();
        win.center().open();
        $scope.TruckNotIn_GridOptions.dataSource.read();
    }

    $scope.TruckNotIn_GridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.FLM,
            method: _FLMStand.URL.Truck_NotInList,
            readparam: function () { return { standID: $scope.Item.ID, } },
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number', editable: false, nullable: true },
                    IsChoose: { type: 'boolean' },
                }
            }
        }),
        height: '100%', pageable: true, sortable: true, columnMenu: false, filterable: { mode: 'row' }, resizable: true, editable: false,
        columns: [
            {
                title: ' ', width: '35px',
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,TruckNotIn_Grid,TruckNotIn_GridChoose_Change)" />',
                headerAttributes: { style: 'text-align: center;' }, attributes: { style: 'text-align: center;' },
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,TruckNotIn_Grid,TruckNotIn_GridChoose_Change)" />',
                filterable: false, sortable: false
            },
            {
                field: "IsDefault", title: 'Mặc định', width: 100,attributes: { style: 'text-align: center;' },
                template: '<input type="checkbox" ng-model="dataItem.IsDefault" />',
                filterable: false, sortable: false
            },
            {
                field: "RegNo", title: 'Số xe', width: 100,
                filterable: { cell: { showOperators: false, operator: "contains" } }
            },
            {
                field: "RegWeight", title: 'Trọng tải đăng kí', width: 120,
                template: "#=RegWeight==null?\"\":RegWeight#", filterable: { cell: { showOperators: false, operator: "eq" } }
            },
            {
                field: "MinWeight", title: 'Trọng tải tối thiểu', width: 120,
                template: "#=MinWeight==null?\"\":MinWeight#", filterable: { cell: { showOperators: false, operator: "eq" } }
            },
            {
                field: "MaxWeight", title: 'Trọng tải tối đa', width: 120,
                template: "#=MaxWeight==null?\"\":MaxWeight#", filterable: { cell: { showOperators: false, operator: "eq" } }
            },
            {
                field: "RegCapacity", title: 'Đăng kí', width: '100px',
                template: "#=RegCapacity==null?\"\":RegCapacity#", filterable: { cell: { showOperators: false, operator: "eq" } }
            },
            {
                field: "MinCapacity", title: 'Tối thiểu', width: '100px',
                template: "#=MinCapacity==null?\"\":MinCapacity#", filterable: { cell: { showOperators: false, operator: "eq" } }
            },
            {
                field: "MaxCapacity", title: 'Tối đa', width: '100px',
                template: "#=MaxCapacity==null?\"\":MaxCapacity#", filterable: { cell: { showOperators: false, operator: "eq" } }
            },
            {
                field: "IsRent", title: 'Cho thuê', width: 82, attributes: { style: "text-align: center;" },
                template: '<input type="checkbox" #= IsRent=="true" ? "checked=checked" : "" # disabled="disabled" />',
                filterable: {
                    cell: {
                        template: function (container) {
                            container.element.kendoComboBox({
                                dataSource: [{ Text: 'Sai', Value: false }, { Text: 'Đúng', Value: true }, { Text: 'Tất cả', Value: '' }],
                                dataTextField: "Text", dataValueField: "Value"
                            });
                        },
                        showOperators: false
                    }
                }
            },
            {
                 field: 'GroupOfVehicleName', title: 'Phân loại', width: 100, headerAttributes: { style: "text-align: center;" },
                 template: "#=GroupOfVehicleName==null?\"\":GroupOfVehicleName#",
                 filterable: { cell: { showOperators: false, operator: "contains" } }
             },
            {
                field: 'GPSCode', title: 'Mã GPS', width: 100, headerAttributes: { style: "text-align: center;" },
                template: "#=GPSCode==null?\"\":GPSCode#",
                filterable: { cell: { showOperators: false, operator: "contains" } }
            },
            {
                field: 'YearOfProduction', title: 'Năm sản xuất', width: 110, headerAttributes: { style: "text-align: center;" },
                filterable: { cell: { showOperators: false, operator: "eq" } }
            },
            {
                field: 'Manufactor', title: 'Nhà sản xuất', width: 110,
                template: "#=Manufactor==null?\"\":Manufactor#", filterable: { cell: { showOperators: false, operator: "contains" } }
            },
            {
                field: 'WarrantyEnd', title: 'Ngày kết thúc BH', width: '145px', headerAttributes: { style: "text-align: center" },
                template: "#=WarrantyEnd==null?\"\":kendo.toString(WarrantyEnd,'dd/MM/yyyy')#",
                filterable: { cell: { template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); }, operator: 'equal', showOperators: false } },
            },
            { title: "", sortable: false, filterable: false }
            
        ]
    }

    $scope.TruckNotIn_GridChoose_Change = function ($event, grid, hasChoose) {
        $scope.TruckNotInHasChoose = hasChoose;
    }
    $scope.TruckNotIn_Save_Click = function ($event, win, grid) {
        $event.preventDefault();
        var data = grid.dataSource.data();
        var datasend = [];
        Common.Data.Each(data, function (o) {
            if (o.IsChoose) datasend.push(o);
        })
        if (datasend.length > 0) {
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.FLM,
                method: _FLMStand.URL.Truck_SaveList,
                data: { lstFLMTruck: datasend, standID: $scope.Item.ID },
                success: function (res) {
                    if (Common.HasValue(res)) {
                        angular.forEach(res, function (value, key) {
                            value.IsChoose = false;
                        });
                    }
                    win.close();
                    $rootScope.Message({ Msg: "Đã cập nhật!", NotifyType: Common.Message.NotifyType.SUCCESS });
                    $rootScope.IsLoading = false;
                    $scope.FLMStand_Truck_GridOptions.dataSource.read();
                }
            });
        }
    }
    $scope.Truck_Delete = function ($event, grid) {
        $event.preventDefault();

        var data = grid.dataSource.data();
        var datasend = [];
        Common.Data.Each(data, function (o) {
            if (o.IsChoose) datasend.push(o);
        })
        if (datasend.length > 0) {
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.FLM,
                method: _FLMStand.URL.Truck_Delete,
                data: { lstFLMTruck: datasend, standID: $scope.Item.ID },
                success: function (res) {
                    $scope.FLMStand_Truck_GridOptions.dataSource.read();
                    $rootScope.IsLoading = false;
                    $scope.TruckHasChoose = false;
                    $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                },
                error: function (e) {
                    $rootScope.IsLoading = false;
                }
            })
        }
    }
    //#endregion

    //#region Tractor
    $scope.TractorHasChoose = false;
    $scope.FLMStand_Tractor_GridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.FLM,
            method: _FLMStand.URL.Tractor_List,
            readparam: function () { return { standID: $scope.Item.ID, } },
            model: {
                id: 'ID',
                fields: {
                    ID: { field: 'ID', type: 'number', editable: false, nullable: false },
                    WarrantyEnd: { type: 'date' },
                    IsOwn: { type: 'string' },
                    IsRent: { type: 'string' },
                    GroupOfVehicleName: { type: 'string' },
                    RegWeight: { type: 'number' },
                    MinWeight: { type: 'number' },
                    MaxWeight: { type: 'number' },
                    YearOfProduction: { type: 'number' },
                    GPSCode: { type: 'string' },
                    IsChoose: { type: 'boolean' },
                }
            }
        }),
        height: '100%', pageable: true, sortable: true, columnMenu: false, filterable: { mode: 'row' }, resizable: true,
        dataBound: function () {
        },
        columns: [
            {
                title: ' ', width: '35px',
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,FLMStand_Tractor_Grid,Tractor_GridChoose_Change)" />',
                headerAttributes: { style: 'text-align: center;' }, attributes: { style: 'text-align: center;' },
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,FLMStand_Tractor_Grid,Tractor_GridChoose_Change)" />',
                filterable: false, sortable: false
            },
            {
                field: "RegNo", title: 'Số xe', width: 100,
                template: '<a href="\" ng-click="FLMAsset_TractorEdit_Click($event,dataItem)">#=RegNo#</a>',
                filterable: { cell: { showOperators: false, operator: "contains" } }
            },
            { field: "IsDefault", title: 'Mặc định', width: 100, attributes: { style: 'text-align: center;' }, template: '<input type="checkbox" ng-model="dataItem.IsDefault" disabled/>', filterable: false, sortable: false },
            {
                field: "RegWeight", title: 'Trọng tải đăng kí', width: 120,
                filterable: { cell: { showOperators: false, operator: "eq" } }
            },
            {
                field: "MinWeight", title: 'Trọng tải tối thiểu', width: 120,
                template: "#=MinWeight==null?\"\":MinWeight#", filterable: { cell: { showOperators: false, operator: "eq" } }
            },
            {
                field: "MaxWeight", title: 'Trọng tải tối đa', width: 120,
                template: "#=MaxWeight==null?\"\":MaxWeight#", filterable: { cell: { showOperators: false, operator: "eq" } }
            },
            {
                field: 'GPSCode', title: 'Mã GPS', width: 100, headerAttributes: { style: "text-align: center;" },
                template: "#=GPSCode==null?\"\":GPSCode#",
                filterable: { cell: { showOperators: false, operator: "contains" } }
            },
            {
                field: 'YearOfProduction', title: 'Năm sản xuất', width: 110, headerAttributes: { style: "text-align: center;" },
                filterable: { cell: { showOperators: false, operator: "eq" } }
            },
            {
                field: 'Manufactor', title: 'Nhà sản xuất', width: 110,
                template: "#=Manufactor==null?\"\":Manufactor#", filterable: { cell: { showOperators: false, operator: "contains" } }
            },
            {
                field: 'WarrantyEnd', title: 'Ngày kết thúc BH', width: '145px', headerAttributes: { style: "text-align: center;" },
                template: "#=WarrantyEnd==null?\"\":kendo.toString(WarrantyEnd,'dd/MM/yyyy')#",
                filterable: { cell: { template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); }, operator: 'equal', showOperators: false } },
            },
            { title: "", sortable: false, filterable: false }
        ]
    }

    $scope.Tractor_GridChoose_Change = function ($event, grid, hasChoose) {
        $scope.TractorHasChoose = hasChoose;
    }

    $scope.Tractor_AddNew = function ($event, win) {
        $event.preventDefault();
        win.center().open();
        $scope.TractorNotIn_GridOptions.dataSource.read();
    }

    $scope.TractorNotIn_GridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.FLM,
            method: _FLMStand.URL.Tractor_NotInList,
            readparam: function () { return { standID: $scope.Item.ID, } },
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number', editable: false, nullable: true },
                    IsChoose: { type: 'boolean' },
                }
            }
        }),
        height: '100%', pageable: true, sortable: true, columnMenu: false, filterable: { mode: 'row' }, resizable: true, editable: false,
        columns: [
            {
                title: ' ', width: '35px',
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,TractorNotIn_Grid,TractorNotIn_GridChoose_Change)" />',
                headerAttributes: { style: 'text-align: center;' }, attributes: { style: 'text-align: center;' },
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,TractorNotIn_Grid,TractorNotIn_GridChoose_Change)" />',
                filterable: false, sortable: false
            },
             {
                 field: "IsDefault", title: 'Mặc định', width: 100, attributes: { style: 'text-align: center;' },
                 template: '<input type="checkbox" ng-model="dataItem.IsDefault" />',
                 filterable: false, sortable: false
             },
            {
                 field: "RegNo", title: 'Số xe', width: 100,
                 filterable: { cell: { showOperators: false, operator: "contains" } }
             },
            {
                field: "RegWeight", title: 'Trọng tải đăng kí', width: 120,
                template: "#=RegWeight==null?\"\":RegWeight#", filterable: { cell: { showOperators: false, operator: "eq" } }
            },
            {
                field: "MinWeight", title: 'Trọng tải tối thiểu', width: 120,
                template: "#=MinWeight==null?\"\":MinWeight#", filterable: { cell: { showOperators: false, operator: "eq" } }
            },
            {
                field: "MaxWeight", title: 'Trọng tải tối đa', width: 120,
                template: "#=MaxWeight==null?\"\":MaxWeight#", filterable: { cell: { showOperators: false, operator: "eq" } }
            },
            {
                field: "IsRent", title: 'Cho thuê', width: 82, attributes: { style: "text-align: center;" },
                template: '<input type="checkbox" #= IsRent=="true" ? "checked=checked" : "" # disabled="disabled" />',
                filterable: {
                    cell: {
                        template: function (container) {
                            container.element.kendoComboBox({
                                dataSource: [{ Text: 'Sai', Value: false }, { Text: 'Đúng', Value: true }, { Text: 'Tất cả', Value: '' }],
                                dataTextField: "Text", dataValueField: "Value"
                            });
                        },
                        showOperators: false
                    }
                }
            },
            {
                field: 'GroupOfVehicleName', title: 'Phân loại', width: 100, headerAttributes: { style: "text-align: center;" },
                template: "#=GroupOfVehicleName==null?\"\":GroupOfVehicleName#",
                filterable: { cell: { showOperators: false, operator: "contains" } }
            },
            {
                field: 'GPSCode', title: 'Mã GPS', width: 100, headerAttributes: { style: "text-align: center;" },
                template: "#=GPSCode==null?\"\":GPSCode#",
                filterable: { cell: { showOperators: false, operator: "contains" } }
            },
            {
                field: 'YearOfProduction', title: 'Năm sản xuất', width: 110, headerAttributes: { style: "text-align: center;" },
                filterable: { cell: { showOperators: false, operator: "eq" } }
            },
            {
                field: 'Manufactor', title: 'Nhà sản xuất', width: 110,
                template: "#=Manufactor==null?\"\":Manufactor#", filterable: { cell: { showOperators: false, operator: "contains" } }
            },
            {
                field: 'WarrantyEnd', title: 'Ngày kết thúc BH', width: '145px', headerAttributes: { style: "text-align: center;" },
                template: "#=WarrantyEnd==null?\"\":kendo.toString(WarrantyEnd,'dd/MM/yyyy')#",
                filterable: { cell: { template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); }, operator: 'equal', showOperators: false } },
            },
            { title: "", sortable: false, filterable: false }

        ]
    }

    $scope.TractorNotIn_GridChoose_Change = function ($event, grid, hasChoose) {
        $scope.TractorNotInHasChoose = hasChoose;
    }
    $scope.TractorNotIn_Save_Click = function ($event, win, grid) {
        $event.preventDefault();
        var data = grid.dataSource.data();
        var datasend = [];
        Common.Data.Each(data, function (o) {
            if (o.IsChoose) datasend.push(o);
        })
        if (datasend.length > 0) {
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.FLM,
                method: _FLMStand.URL.Tractor_SaveList,
                data: { lstFLMTractor: datasend, standID: $scope.Item.ID },
                success: function (res) {
                    win.close();
                    $rootScope.Message({ Msg: "Đã cập nhật!", NotifyType: Common.Message.NotifyType.SUCCESS });
                    $rootScope.IsLoading = false;
                    $scope.FLMStand_Tractor_GridOptions.dataSource.read();
                }
            });
        }
    }
    $scope.Tractorr_Delete = function ($event, grid) {
        $event.preventDefault();

        var data = grid.dataSource.data();
        var datasend = [];
        Common.Data.Each(data, function (o) {
            if (o.IsChoose) datasend.push(o);
        })
        if (datasend.length > 0) {
                    $rootScope.IsLoading = true;
                    Common.Services.Call($http, {
                        url: Common.Services.url.FLM,
                        method: _FLMStand.URL.Tractor_Delete,
                        data: { lstFLMTractor: datasend, standID: $scope.Item.ID },
                        success: function (res) {
                            $scope.FLMStand_Tractor_GridOptions.dataSource.read();
                            $rootScope.IsLoading = false;
                            $scope.TractorHasChoose = false;
                            $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                        },
                        error: function (e) {
                            $rootScope.IsLoading = false;
                }
            })
        }
    }
    //#endregion

    //#region Romooc
    $scope.RomoocHasChoose = false;
    $scope.FLMStand_Romooc_GridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.FLM,
            method: _FLMStand.URL.Romooc_List,
            readparam: function () { return { standID: $scope.Item.ID, } },
            model: {
                id: 'ID',
                fields: {
                    ID: { field: 'ID', type: 'number', editable: false, nullable: false },
                    WarrantyEnd: { type: 'date' },
                    IsRent: { type: 'string' },
                    GroupOfVehicleName: { type: 'string' },
                    MaxWeight: { type: 'number' },
                    YearOfProduction: { type: 'number' },
                    IsChoose: { type: 'boolean' },
                }
            }
        }),
        height: '100%', pageable: true, sortable: true, columnMenu: false, filterable: { mode: 'row' }, resizable: true,
        dataBound: function () {
        },
        columns: [
            {
                title: ' ', width: '35px',
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,FLMStand_Romooc_Grid,Romooc_GridChoose_Change)" />',
                headerAttributes: { style: 'text-align: center;' }, attributes: { style: 'text-align: center;' },
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,FLMStand_Romooc_Grid,Romooc_GridChoose_Change)" />',
                filterable: false, sortable: false
            },
            {
                field: "RegNo", title: 'Số xe', width: 100,
                filterable: { cell: { showOperators: false, operator: "contains" } }
            },
            { field: "IsDefault", title: 'Mặc định', width: 100, attributes: { style: 'text-align: center;' }, template: '<input type="checkbox" ng-model="dataItem.IsDefault" disabled/>', filterable: false, sortable: false },
            {
                field: "MaxWeight", title: 'Tải trọng', width: '100px',
                template: "#=MaxWeight==null?\"\":MaxWeight#", filterable: { cell: { showOperators: false, operator: "eq" } }
            },
            {
                field: 'GroupOfRomoocName', title: 'Phân loại', width: 100, headerAttributes: { style: "text-align: center;" },
                template: "#=GroupOfRomoocName==null?\"\":GroupOfRomoocName#",
                filterable: { cell: { showOperators: false, operator: "contains" } }
            },
            { title: "", sortable: false, filterable: false }
        ]
    }

    $scope.Romooc_GridChoose_Change = function ($event, grid, hasChoose) {
        $scope.RomoocHasChoose = hasChoose;
    }

    $scope.Romooc_AddNew = function ($event, win) {
        $event.preventDefault();
        win.center().open();
        $scope.RomoocNotIn_GridOptions.dataSource.read();
    }

    $scope.RomoocNotIn_GridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.FLM,
            method: _FLMStand.URL.Romooc_NotInList,
            readparam: function () { return { standID: $scope.Item.ID, } },
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number', editable: false, nullable: true },
                    IsChoose: { type: 'boolean' },
                    GroupOfVehicleName: { type: 'string' },
                }
            }
        }),
        height: '100%', pageable: true, sortable: true, columnMenu: false, filterable: { mode: 'row' }, resizable: true, editable: false,
        columns: [
            {
                title: ' ', width: '35px',
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,RomoocNotIn_Grid,RomoocNotIn_GridChoose_Change)" />',
                headerAttributes: { style: 'text-align: center;' }, attributes: { style: 'text-align: center;' },
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,RomoocNotIn_Grid,RomoocNotIn_GridChoose_Change)" />',
                filterable: false, sortable: false
            },
            {
                field: "IsDefault", title: 'Mặc định', width: 100, attributes: { style: 'text-align: center;' },
                template: '<input type="checkbox" ng-model="dataItem.IsDefault" />',
                filterable: false, sortable: false
            },
            {
                 field: "RegNo", title: 'Số xe', width: 100,
                 filterable: { cell: { showOperators: false, operator: "contains" } }
             },
            {
                field: "MaxWeight", title: 'Tải trọng', width: '100px',
                template: "#=MaxWeight==null?\"\":MaxWeight#", filterable: { cell: { showOperators: false, operator: "eq" } }
            },
            {
                 field: "IsRent", title: 'Cho thuê', width: 82, attributes: { style: "text-align: center;" },
                 template: '<input type="checkbox" #= IsRent=="true" ? "checked=checked" : "" # disabled="disabled" />',
                 filterable: {
                     cell: {
                         template: function (container) {
                             container.element.kendoComboBox({
                                 dataSource: [{ Text: 'Sai', Value: false }, { Text: 'Đúng', Value: true }, { Text: 'Tất cả', Value: '' }],
                                 dataTextField: "Text", dataValueField: "Value"
                             });
                         },
                         showOperators: false
                     }
                 }
             },
            {
                field: 'GroupOfRomoocName', title: 'Phân loại', width: 100, headerAttributes: { style: "text-align: center;" },
                template: "#=GroupOfRomoocName==null?\"\":GroupOfRomoocName#",
                filterable: { cell: { showOperators: false, operator: "contains" } }
            },
            {
                field: 'YearOfProduction', title: 'Năm sản xuất', width: 110, headerAttributes: { style: "text-align: center;" },
                filterable: { cell: { showOperators: false, operator: "eq" } }
            },
            {
                field: 'Manufactor', title: 'Nhà sản xuất', width: 110,
                template: "#=Manufactor==null?\"\":Manufactor#", filterable: { cell: { showOperators: false, operator: "contains" } }
            },
            {
                field: 'WarrantyEnd', title: 'Ngày kết thúc BH', width: '145px', headerAttributes: { style: "text-align: center;" },
                template: "#=WarrantyEnd==null?\"\":kendo.toString(WarrantyEnd,'dd/MM/yyyy')#",
                filterable: { cell: { template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); }, operator: 'equal', showOperators: false } },
            },
            { title: "", sortable: false, filterable: false }

        ]
    }

    $scope.RomoocNotIn_GridChoose_Change = function ($event, grid, hasChoose) {
        $scope.RomoocNotInHasChoose = hasChoose;
    }
    $scope.RomoocNotIn_Save_Click = function ($event, win, grid) {
        $event.preventDefault();
        var data = grid.dataSource.data();
        var datasend = [];
        Common.Data.Each(data, function (o) {
            if (o.IsChoose) datasend.push(o);
        })
        if (datasend.length > 0) {
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.FLM,
                method: _FLMStand.URL.Romooc_SaveList,
                data: { lstFLMRomooc: datasend, standID: $scope.Item.ID },
                success: function (res) {
                    if (Common.HasValue(res)) {
                        angular.forEach(res, function (value, key) {
                            value.IsChoose = false;
                        });
                    }
                    win.close();
                    $rootScope.Message({ Msg: "Đã cập nhật!", NotifyType: Common.Message.NotifyType.SUCCESS });
                    $rootScope.IsLoading = false;
                    $scope.FLMStand_Romooc_GridOptions.dataSource.read();
                }
            });
        }
    }

    $scope.Romooc_Delete = function ($event, grid) {
        $event.preventDefault();

        var data = grid.dataSource.data();
        var datasend = [];
        Common.Data.Each(data, function (o) {
            if (o.IsChoose) datasend.push(o);
        })
        if (datasend.length > 0) {
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.FLM,
                method: _FLMStand.URL.Romooc_Delete,
                data: { lstFLMRomooc: datasend, standID: $scope.Item.ID },
                success: function (res) {
                    $scope.FLMStand_Romooc_GridOptions.dataSource.read();
                    $rootScope.IsLoading = false;
                    $scope.RomoocHasChoose = false;
                    $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                },
                error: function (e) {
                    $rootScope.IsLoading = false;
                }
            })
        }
    }
    //#endregion

    $scope.FLMStand_ExcelOnlClick = function ($event, grid) {
        $event.preventDefault();

        var lstMessageError = [];
        for (var i = 0; i < 7; i++) {
            var resource = $rootScope.RS[_FLMStand.ExcelKey.ResourceStand + '_' + i];
            if (Common.HasValue(resource))
                lstMessageError.push(resource);
        }
        if (lstMessageError.length == 0) {
            lstMessageError = [
                '[Mã trạm] không được trống và > 50 ký tự',
                '[Tên trạm] không được > 50 ký tự',
                '[Mã địa điểm] không được trống và > 50 ký tự',
                '[Mã địa điểm] không tồn tại trong hệ thống',
                '[Số xe tải] không tồn tại trong hệ thống',
                '[Số xe đầu kéo không tồn tại trong hệ thống',
                '[Số xe Romooc] không tồn tại trong hệ thống',
                '[Mã loại] không tồn tại trong hệ thống',
            ];
        }

        $rootScope.excelShare.Init({
            functionkey: _FLMStand.ExcelKey.Stand,
            params: {},
            rowStart: 1,
            colCheckChange: 9,
            url: Common.Services.url.FLM,
            methodInit: _FLMStand.URL.Stand_ExcelInit,
            methodChange: _FLMStand.URL.Stand_ExcelChange,
            methodImport: _FLMStand.URL.Stand_ExcelImport,
            methodApprove: _FLMStand.URL.Stand_ExcelApprove,
            lstMessageError: lstMessageError,

            Approved: function () {
                $scope.FLMStand_gridOptions.dataSource.read();
            }
        });
    };
    $scope.Close_Click = function ($event, win) {
        $event.preventDefault();
        win.close();
    }

    $scope.ShowSetting = function ($event, grid) {
        $event.preventDefault();

        $rootScope.ShowSetting({
            ListView:'',
            event: $event,
            grid: grid,
            current: $state.current
        });
    };
}]);