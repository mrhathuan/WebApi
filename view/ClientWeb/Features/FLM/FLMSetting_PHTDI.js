/// <reference path="~/Scripts/jquery-1.9.1.intellisense.js" />
/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />

var _FLMSetting_PHTDI = {
    URL: {
        Read: 'FLMPHTDIMaster_List',
        Save: 'FLMPHTDIMaster_Save',
        Delete: 'FLMPHTDIMaster_DeleteList',
        DriverFee_Import_Data: 'FLMPHTDIMaster_Import_Data',
        StationCost: 'FLMPHTDIMaster_StationList',
        StationCost_Save: 'FLMPHTDIMaster_StationSaveList',
        StationCost_Delete: 'FLMPHTDIMaster_StationDeleteList',
        StationCostNoList: 'FLMPHTDIMaster_StationNotInList',
        TroubleCost: 'FLMPHTDIMaster_TroubleList',
        TroubleCost_SaveAll: 'FLMPHTDIMaster_TroubleSaveList',
        TroubleCost_Delete: 'FLMPHTDIMaster_TroubleDeleteList',
      

        Excel_Export: 'FLMPHTDI_Export',
        Excel_Save: 'FLMPHTDI_Excel_Import',
        Excel_Check: 'FLMPHTDI_Excel_Check',
    },
    Data: {
        ItemBackUp: null,
        DataCBO: [],
    }
}

angular.module('myapp').controller('FLMSetting_PHTDICtrl', ['$rootScope', '$scope', '$http', '$location', '$state', '$timeout', '$stateParams', function ($rootScope, $scope, $http, $location, $state, $timeout, $stateParams) {
    Common.Log('FLMSetting_PHTDICtrl');
    $rootScope.IsLoading = false;
    $scope.ISEdit = true;
    $scope.masterID = 0;
    $scope.SettingEdit = null;
    $scope.TroubleCostEdit = null;
    $scope.HasChoose = false;
    $scope.StationNotListHasChoose = false;
    $scope.StationCost_gridHasChoose = false;
    $scope.TroubleCostHasChoose = false;
    $scope.IsAddTroubleCost = true;
    $scope.GroupOfTroubleID = 0;
    $scope.IsSaveClick = 1;
    $scope.ItemSearch = {
        DateFrom: (new Date()).addDays(-7),
        DateTo: (new Date()),
    }

    $scope.SearchClick = function ($event) {
        $event.preventDefault();

        if (!Common.HasValue($scope.ItemSearch.DateFrom) || !Common.HasValue($scope.ItemSearch.DateTo)) {
            $rootScope.Message({
                Type: Common.Message.Type.Alert,
                NotifyType: Common.Message.NotifyType.ERROR,
                Title: 'Thông báo',
                Msg: 'Vui lòng chọn Từ ngày- Đến ngày chính xác',
                Close: null,
                Ok: null
            })
        }
        else if ($scope.ItemSearch.DateFrom > $scope.ItemSearch.DateTo) {
            $rootScope.Message({
                Type: Common.Message.Type.Alert,
                NotifyType: Common.Message.NotifyType.ERROR,
                Title: 'Thông báo',
                Msg: 'Vui lòng chọn Từ ngày nhỏ hơn Đến ngày',
                Close: null,
                Ok: null
            })
        }
        else {
            $scope.setting_PHTDI_gridOptions.dataSource.read();
        }
    }

    $scope.setting_PHTDI_gridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.FLM,
            method: _FLMSetting_PHTDI.URL.Read,
            pageSize: 20,
            readparam: function () {
                return {
                    'dtFrom': $scope.ItemSearch.DateFrom,
                    'dtTo': $scope.ItemSearch.DateTo,
                };
            },
            model: {
                id: 'ID',
                fields: {
                    RegNo: { type: 'string' },
                    MasterCode: { type: 'string', editable: false },
                    IsChoose: { type: 'bool', defaultValue: false },
                    KMEnd: { type: 'number' },
                    KMEnd: { type: 'number' },
                    DateConfig: { type: 'DateTime' },
                    VehicleCode: { type: 'string' },
                    CustomerName: { type: 'string' },
                    TypeOfLocationName: { type: 'string' },
                    DriverName1: { type: 'string' },
                    DriverName2: { type: 'string' },
                    DriverName3: { type: 'string' },
                    DriverName4: { type: 'string' },
                    DriverName5: { type: 'string' },
                    ExTotalDayOut: { type: 'number' },
                    ExIsOverNight: { type: 'boolane' },
                    ExIsOverWeight: { type: 'boolane' },
                    ButtonTroubleCost: { type: 'number', editable: false },
                    ButtonStationCost: { type: 'number', editable: false },
                    StationCost: { type: 'number', editable: false },
                    TroubleCost: { type: 'number', editable: false },
                    abc: { type: 'number', editable: false },
                }
            }
        }),
        height: '99%', groupable: false, pageable: true, sortable: true, columnMenu: false, resizable: true,
        selectable: false, filterable: { mode: 'row', visible: false }, reorderable: true,
        editable: { mode: 'inline' },

        columns: [
              {
                  title: ' ', width: '40px',
                  headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,setting_PHTDI_grid,setting_PHTDI_gridChooseChange)" />',
                  headerAttributes: { style: 'text-align: center;' },
                  template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,setting_PHTDI_grid,setting_PHTDI_gridChooseChange)" />',
                  templateAttributes: { style: 'text-align: center;' },
                  filterable: false, sortable: false
              },
            {
                field: 'abc', title: ' ', width: '70px',
                template: '<a ng-show="!IsSettingEdit" href="/" ng-click="Edit_Click($event,setting_PHTDI_grid)" class="k-button"><i class="fa fa-pencil"></i></a>' +
                    '<a ng-show="IsSettingEdit && SettingEdit.ID==dataItem.ID?true:false" href="/" ng-click="Save_Click($event,setting_PHTDI_grid)" class="k-button"><i class="fa fa-check"></i></a>' +
                    '<a ng-show="IsSettingEdit && SettingEdit.ID==dataItem.ID?true:false" href="/" ng-click="Cancel_Click($event,setting_PHTDI_grid)" class="k-button"><i class="fa fa-ban"></i></a>',
                filterable: false, sortable: false
            },
            {
                field: 'MasterCode', title: 'Mã', template: "#=MasterCode#", width: 120,
            },
            {
                field: 'DateConfig', width: '130px', title: 'Ngày phát sinh', template: "#=DateConfig==null?' ':Common.Date.FromJsonDMY(DateConfig)#",
                editor: function (container, options) {
                    var input = $("<input/>");
                    input.attr("name", options.field);
                    input.appendTo(container);
                    input.kendoDatePicker({ format: 'dd/MM/yyyy', parseFormats: ["yyyy-MM-dd"] });
                },
                filterable: {
                    cell: {
                        template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); }, operator: 'equal', showOperators: false
                    }
                },
            },
            {
                field: 'ETD', width: '130px', title: 'Ngày kết thúc', template: "#=ETD==null?' ':Common.Date.FromJsonDMY(ETD)#",
                editor: function (container, options) {
                    var input = $("<input/>");
                    input.attr("name", options.field);
                    input.appendTo(container);
                    input.kendoDatePicker({ format: 'dd/MM/yyyy', parseFormats: ["yyyy-MM-dd"] });
                },
                filterable: {
                    cell: {
                        template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); }, operator: 'equal', showOperators: false
                    }
                },
            },
           {
               field: 'VehicleID', title: 'Số Xe', template: "#=VehicleCode#", width: 130,
               editor: '<form class="cus-form-enter" ng-submit="Enter_Click($event)"><input ng- ng-model="SettingEdit.VehicleID"  class="cus-combobox" focus-k-combobox  kendo-combo-box="cboVehicle_cboGroup" k-options="cboVehicle_Options"  />'
           },
           {
               field: 'SortOrder', title: 'Số chuyến', width: 100,
               editor: '<form class="cus-form-enter" ng-submit="Enter_Click($event)"><input ng-model="SettingEdit.SortOrder" class="k-textbox" type="number" style="width:100%" /></form>'
           },
           {
               field: 'CustomerID', title: 'Khách hàng', template: "#=CustomerName#", width: 150,
               editor: '<form class="cus-form-enter" ng-submit="Enter_Click($event)"><input ng-model="SettingEdit.CustomerID"  class="cus-combobox" focus-k-combobox  kendo-combo-box="cboCustomer_cboGroup" k-options="cboCustomer_Options"  /></form>',
           },
            {
                field: 'TypeOfLocationID', title: 'Loại điểm', template: "#=TypeOfLocationName#", width: 150,
                editor: '<form class="cus-form-enter" ng-submit="Enter_Click($event)"><input ng-model="SettingEdit.TypeOfLocationID"  class="cus-combobox" focus-k-combobox  kendo-combo-box="cboTypeOfLocation_cboGroup" k-options="cboTypeOfLocation_Options"  /></form>'
            },
            {
                field: 'DriverID1', title: 'Tài xế 1', template: "#=DriverName1#", width: 120,
                editor: '<form class="cus-form-enter" ng-submit="Enter_Click($event)"><input ng-model="SettingEdit.DriverID1"  class="cus-combobox" focus-k-combobox  kendo-combo-box="cboDriver_cboGroup" k-options="cboDriver1_Options"  /></form>'
            },
            {
                field: 'DriverID2', title: 'Phụ lái 1', template: "#=DriverName2#", width: 120,
                editor: '<form class="cus-form-enter" ng-submit="Enter_Click($event)"><input ng-model="SettingEdit.DriverID2"  class="cus-combobox" focus-k-combobox  kendo-combo-box="cboDriver_cboGroup" k-options="cboDriver2_Options"  /></form>'
            },
            {
                field: 'DriverID3', title: 'Phụ lái 2', template: "#=DriverName3#", width: 120,
                editor: '<form class="cus-form-enter" ng-submit="Enter_Click($event)"><input ng-model="SettingEdit.DriverID3"  class="cus-combobox" focus-k-combobox  kendo-combo-box="cboDriver_cboGroup" k-options="cboDriver3_Options"  /></form>'
            },
            {
                field: 'DriverID4', title: 'Phụ lái 3', template: "#=DriverName4#", width: 120,
                editor: '<form class="cus-form-enter" ng-submit="Enter_Click($event)"><input ng-model="SettingEdit.DriverID4"  class="cus-combobox" focus-k-combobox  kendo-combo-box="cboDriver_cboGroup" k-options="cboDriver4_Options"  /></form>'
            },
            {
                field: 'DriverID5', title: 'Phụ lái 4', template: "#=DriverName5#", width: 120,
                editor: '<form class="cus-form-enter" ng-submit="Enter_Click($event)"><input ng-model="SettingEdit.DriverID5"  class="cus-combobox" focus-k-combobox  kendo-combo-box="cboDriver_cboGroup" k-options="cboDriver5_Options"  /></form>'
            },
            {
                field: 'ExTotalDayOut', title: 'Số ngày đi tỉnh', width: 150,
                editor: '<form class="cus-form-enter" ng-submit="Enter_Click($event)"><input ng-model="SettingEdit.ExTotalDayOut" class="k-textbox" style="width:100%" /></form>',
                filterable: { cell: { operator: 'lte', showOperators: false } }
            },
            {
                field: 'ExTotalJoin', title: 'Kết hợp', width: 100,
                editor: '<form class="cus-form-enter" ng-submit="Enter_Click($event)"><input ng-model="SettingEdit.ExTotalJoin" class="k-textbox" type="number" style="width:100%" /></form>',
                filterable: { cell: { operator: 'lte', showOperators: false } }
            },
           {
               field: 'PHTLoading', title: 'Tính bốc xếp', template: '<input class="chkChoose" disabled type="checkbox" #= PHTLoading ? "checked=checked" : "" #" />', width: 150,
               editor: '<form class="cus-form-enter" ng-submit="Enter_Click($event)"><form class="cus-form-enter" ng-submit="Enter_Click($event)"><input ng-model="SettingEdit.PHTLoading"  class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" #/></form>',
               filterable: {
                   cell: {
                       template: function (container) {
                           container.element.kendoComboBox({
                               dataSource: [{ Text: 'Không tính bốc xếp', Value: false }, { Text: 'Tính bốc xếp', Value: true }, { Text: 'Tất cả', Value: '' }],
                               dataTextField: "Text", dataValueField: "Value",
                           });
                       },
                       showOperators: false
                   }
               }
           },
            {
                field: 'ExIsOverWeight', title: 'Quá tải', template: '<input class="chkChoose" disabled type="checkbox" #= ExIsOverWeight ? "checked=checked" : "" #" />', width: 150,
                editor: '<form class="cus-form-enter" ng-submit="Enter_Click($event)"><input ng-model="SettingEdit.ExIsOverWeight"  class="chkChoose" type="checkbox" #= ExIsOverWeight ? "checked=checked" : "" #/></form>',
                filterable: {
                    cell: {
                        template: function (container) {
                            container.element.kendoComboBox({
                                dataSource: [{ Text: 'Không quá tải', Value: false }, { Text: 'Quá tải', Value: true }, { Text: 'Tất cả', Value: '' }],
                                dataTextField: "Text", dataValueField: "Value",
                            });
                        },
                        showOperators: false
                    }
                }
            },
            {
                field: 'ExIsOverNight', title: 'Qua đêm', template: '<input class="chkChoose" disabled type="checkbox" #= ExIsOverNight ? "checked=checked" : "" #" />', width: 150,
                editor: '<form class="cus-form-enter" ng-submit="Enter_Click($event)"><input ng-model="SettingEdit.ExIsOverNight"  class="chkChoose" type="checkbox" #= ExIsOverNight ? "checked=checked" : "" #/></form>',
                filterable: {
                    cell: {
                        template: function (container) {
                            container.element.kendoComboBox({
                                dataSource: [{ Text: 'Không qua đêm', Value: false }, { Text: 'Qua đêm', Value: true }, { Text: 'Tất cả', Value: '' }],
                                dataTextField: "Text", dataValueField: "Value",
                            });
                        },
                        showOperators: false
                    }
                }
            },
            {
                field: 'KMStart', title: 'Số KM bắt đầu', width: 150,
                editor: '<form class="cus-form-enter" ng-submit="Enter_Click($event)"><input ng-model="SettingEdit.KMStart" class="k-textbox" type="number" style="width:100%" /></form>',
                filterable: { cell: { operator: 'lte', showOperators: false } }
            },
            {
                field: 'KMEnd', title: 'Số KM kết thúc', width: 150,
                editor: '<form class="cus-form-enter" ng-submit="Enter_Click($event)"><input ng-model="SettingEdit.KMEnd"class="k-textbox EvenEnter"  type="number" style="width:100%" /></form>'
                , filterable: { cell: { operator: 'lte', showOperators: false } }
            },
            {
                field: 'StationCost', width: 150, title: 'Số tiền qua trạm', template: "#=StationCost==null?' ':Common.Number.ToMoney(StationCost)#",
                filterable: { cell: { operator: 'lte', showOperators: false } }
            },
            { field: 'ButtonStationCost', width: 100, title: 'Thông tin trạm', template: '<a href="/" ng-show="dataItem.ID > 0" ng-click="StationCost_Click($event,dataItem,StationCost_win)" class="k-button" data-title="Hiển thị"><i class="fa fa-bars"></i></a>', filterable: { cell: { operator: 'lte', showOperators: false } } },
            {
                field: 'TroubleCost', width: 150, title: 'Số tiền phát sinh', template: "#=TroubleCost==null?' ':Common.Number.ToMoney(TroubleCost)#",
                filterable: { cell: { operator: 'lte', showOperators: false } }
            },
            { field: 'ButtonTroubleCost', width: 100, title: 'Thông tin phát sinh', template: '<a href="/" ng-show="dataItem.ID > 0" ng-click="TroubleCost_Click($event,dataItem,TroubleCost_win)" class="k-button" data-title="Hiển thị"><i class="fa fa-bars"></i></a>', filterable: { cell: { operator: 'lte', showOperators: false } } },
            { title: ' ', filterable: false, sortable: false }
        ],
        dataBound: function () {
            $scope.IsSettingEdit = false;
        },
    }

    $scope.Enter_Click = function ($event) {
        var tr = $($event.target).closest('tr');
        var item = $scope.setting_PHTDI_grid.dataItem(tr);
        if (Common.HasValue(item)) {
            if (item.DateConfig == "" || item.CustomerID < 1 || item.TypeOfLocationID < 1 || item.SortOrder < 1) {
                $rootScope.Message({
                    Msg: 'Dữ liệu sai.',
                    NotifyType: Common.Message.NotifyType.ERROR
                });
            }
            else {
                $rootScope.IsLoading = true;
                Common.Services.Call($http, {
                    url: Common.Services.url.FLM,
                    method: _FLMSetting_PHTDI.URL.Save,
                    data: { item: item },
                    success: function (res) {
                        $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                        $scope.setting_PHTDI_grid.dataSource.read();
                        $scope.IsSettingEdit = false;
                        $rootScope.IsLoading = false;
                    }
                });
            }
        }
    }

    $scope.setting_PHTDI_gridChooseChange = function ($event, grid, haschoose) {
        $scope.HasChoose = haschoose;
    };

    $scope.cboCustomer_Options = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true, dataTextField: 'Name', dataValueField: 'ID',
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    Name: { type: 'string' },
                }
            }
        }),
        change: function (e) {
            var val = this.value();
            var name = this.text();
            var gridEditElement = $scope.setting_PHTDI_grid.editable.element;
            var dataItem = $scope.setting_PHTDI_grid.dataItem(gridEditElement.closest('tr'))
            if (dataItem != null) {
                dataItem.CustomerID = val;
                dataItem.CustomerName = name;
            }
        }
    };
    $scope.cboTypeOfLocation_Options = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true, dataTextField: 'Name', dataValueField: 'ID',
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    Name: { type: 'string' },
                }
            }
        }),
        change: function (e) {
            var val = this.value();
            var name = this.text();
            var gridEditElement = $scope.setting_PHTDI_grid.editable.element;
            var dataItem = $scope.setting_PHTDI_grid.dataItem(gridEditElement.closest('tr'))
            if (dataItem != null) {
                dataItem.TypeOfLocationID = val;
                dataItem.TypeOfLocationName = name;
            }
        }
    };
    $scope.cboDriver1_Options = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true, dataTextField: 'Name', dataValueField: 'ID',
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    Name: { type: 'string' },
                }
            }
        }),
        change: function (e) {
            var val = this.value();
            var name = this.text();
            var gridEditElement = $scope.setting_PHTDI_grid.editable.element;
            var dataItem = $scope.setting_PHTDI_grid.dataItem(gridEditElement.closest('tr'))
            if (dataItem != null) {
                dataItem.DriverID1 = val;
                dataItem.DriverName1 = name;
            }
        }
    };
    $scope.cboDriver2_Options = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true, dataTextField: 'Name', dataValueField: 'ID',
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    Name: { type: 'string' },
                }
            }
        }),
        change: function (e) {
            var val = this.value();
            var name = this.text();
            var gridEditElement = $scope.setting_PHTDI_grid.editable.element;
            var dataItem = $scope.setting_PHTDI_grid.dataItem(gridEditElement.closest('tr'))
            if (dataItem != null) {
                dataItem.DriverID2 = val;
                dataItem.DriverName2 = name;
            }
        }
    };
    $scope.cboDriver3_Options = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true, dataTextField: 'Name', dataValueField: 'ID',
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    Name: { type: 'string' },
                }
            }
        }),
        change: function (e) {
            var val = this.value();
            var name = this.text();
            var gridEditElement = $scope.setting_PHTDI_grid.editable.element;
            var dataItem = $scope.setting_PHTDI_grid.dataItem(gridEditElement.closest('tr'))
            if (dataItem != null) {
                dataItem.DriverID3 = val;
                dataItem.DriverName3 = name;
            }
        }
    };
    $scope.cboDriver4_Options = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true, dataTextField: 'Name', dataValueField: 'ID',
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    Name: { type: 'string' },
                }
            }
        }),
        change: function (e) {
            var val = this.value();
            var name = this.text();
            var gridEditElement = $scope.setting_PHTDI_grid.editable.element;
            var dataItem = $scope.setting_PHTDI_grid.dataItem(gridEditElement.closest('tr'))
            if (dataItem != null) {
                dataItem.DriverID4 = val;
                dataItem.DriverName4 = name;
            }
        }
    };
    $scope.cboDriver5_Options = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true, dataTextField: 'Name', dataValueField: 'ID',
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    Name: { type: 'string' },
                }
            }
        }),
        change: function (e) {
            var val = this.value();
            var name = this.text();
            var gridEditElement = $scope.setting_PHTDI_grid.editable.element;
            var dataItem = $scope.setting_PHTDI_grid.dataItem(gridEditElement.closest('tr'))
            if (dataItem != null) {
                dataItem.DriverID5 = val;
                dataItem.DriverName5 = name;
            }
        }
    };

    $scope.cboVehicle_Options = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true, dataTextField: 'Name', dataValueField: 'ID',
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    Name: { type: 'string' },
                }
            }
        }),
        change: function (e) {
            var val = this.value();
            var name = this.text();
            var gridEditElement = $scope.setting_PHTDI_grid.editable.element;
            var dataItem = $scope.setting_PHTDI_grid.dataItem(gridEditElement.closest('tr'))
            if (dataItem != null) {
                dataItem.VehicleID = val;
                dataItem.VehicleCode = name;
            }
        }
    };

    Common.Services.Call($http, {
        url: Common.Services.url.FLM,
        method: _FLMSetting_PHTDI.URL.DriverFee_Import_Data,
        success: function (res) {
            if (Common.HasValue(res.lstGroupOfTrouble)) {
                res.lstGroupOfTrouble.push({ID:0, Name:""});
            }
            $scope.cboCustomer_Options.dataSource.data(res.lstCustomer);
            $scope.cboDriver1_Options.dataSource.data(res.lstDriver);
            $scope.cboDriver2_Options.dataSource.data(res.lstDriver);
            $scope.cboDriver3_Options.dataSource.data(res.lstDriver);
            $scope.cboDriver4_Options.dataSource.data(res.lstDriver);
            $scope.cboDriver5_Options.dataSource.data(res.lstDriver);
            $scope.cboVehicle_Options.dataSource.data(res.lstVehicle);
            $scope.cboTypeOfLocation_Options.dataSource.data(res.lstGroupOfLocation);
            $scope.cboGroupOfTrouble_Options.dataSource.data(res.lstGroupOfTrouble);
            _FLMSetting_PHTDI.Data.DataCBO = res; 
        }
    })

    $scope.New_Click = function ($event, grid) {
        $event.preventDefault();
        var itemNew = {
            SortOrder: 1, IsChoose: false, MasterCode: "", ETD: new Date(),
            PHTLoading: false, StationCost: 0, TroubleCost: 0, DateConfig: new Date(),
            VehicleCode: "", VehicleID: _FLMSetting_PHTDI.Data.DataCBO.lstVehicle[0].ID, CustomerCode: "", CustomerName: "",
            CustomerID: _FLMSetting_PHTDI.Data.DataCBO.lstCustomer[0].ID,
            TypeOfLocationName: "", TypeOfLocationID: _FLMSetting_PHTDI.Data.DataCBO.lstGroupOfLocation[0].ID, DriverName1: "", DriverName2: "", DriverName3: "", DriverName4: "",
            DriverName5: "", DriverID1: _FLMSetting_PHTDI.Data.DataCBO.lstDriver[0].ID,
            DriverID2: null, DriverID3: null, DriverID4: null, DriverID5: null, ExIsOverNight: false,
            ExIsOverWeight: false, ExTotalJoin: 0, KMStart: 0, KMEnd: 0
        };
        $scope.IsSettingEdit = false;
        grid.dataSource.insert(0, itemNew);
        var tr = grid.tbody.find('tr:first');
        $scope.SettingEdit = grid.dataItem(tr);
        _FLMSetting_PHTDI.Data.ItemBackUp = $.extend(true, {}, $scope.SettingEdit);
        grid.editRow(tr);
        $scope.IsSettingEdit = true;
    }

    $scope.Edit_Click = function ($event, grid) {
        $event.preventDefault();
        $timeout(function () {
            $scope.IsSettingEdit = true;
            var tr = $event.target.closest('tr');
            $scope.SettingEdit = grid.dataItem(tr);
            $scope.SettingEdit.IsChoose = false;
            _FLMSetting_PHTDI.Data.ItemBackUp = $.extend(true, {}, $scope.SettingEdit);
            grid.editRow(tr);
        }, 1)

    }

    $scope.Cancel_Click = function ($event, grid) {
        $event.preventDefault();
        var tr = $($event.target).closest('tr');
        var item = grid.dataItem(tr);
        if (Common.HasValue(item)) {
            var data = grid.dataSource.data();
            if (item.ID > 0) {
                $rootScope.IsLoading = true;
                $scope.IsProductEdit = false;
                var i = 0;
                while (i < data.length) {
                    var obj = data[i];
                    if (obj.ID == _FLMSetting_PHTDI.Data.ItemBackUp.ID) {
                        data.splice(i, 1);
                        data.splice(i, 0, $.extend(true, {}, _FLMSetting_PHTDI.Data.ItemBackUp))
                    }
                    i++;
                }
                grid.dataSource.data(data);
                $rootScope.IsLoading = false;
            }
            else {
                data.remove(item);
            }
        }
        
    }

    $scope.Delete_Click = function ($event, grid) {
        $event.preventDefault();
        var data = [];
        $.each(grid.dataSource.data(), function (i, v) {
            if (v.IsChoose) {
                data.push(v.ID);
            }
        });
        if (Common.HasValue(data)) {
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.FLM,
                method: _FLMSetting_PHTDI.URL.Delete,
                data: { lst: data },
                success: function (res) {
                    $rootScope.Message({ Msg: 'Xóa thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                    grid.dataSource.read();
                    $scope.ISEdit = true;
                    $rootScope.IsLoading = false;
                }
            });
        }
    }

    $scope.Save_Click = function ($event, grid) {
        $event.preventDefault();
        if ($scope.IsSaveClick == 1) {
            $scope.IsSaveClick = 0;
            var tr = $($event.target).closest('tr');
            var item = grid.dataItem(tr);
            if (Common.HasValue(item)) {
                var error = true;
                if (item.DateConfig == "") {
                    $rootScope.Message({
                        Msg: 'Ngày phát sinh sai.',
                        NotifyType: Common.Message.NotifyType.ERROR
                    });
                    error = false;
                }
                if (item.CustomerID < 1) {
                    $rootScope.Message({
                        Msg: 'Chọn khách hàng sai.',
                        NotifyType: Common.Message.NotifyType.ERROR
                    });
                    error = false;
                }
                if (item.TypeOfLocationID < 1) {
                    $rootScope.Message({
                        Msg: 'Loại điểm sai.',
                        NotifyType: Common.Message.NotifyType.ERROR
                    });
                    error = false;
                }
                if (item.SortOrder < 1) {
                    $rootScope.Message({
                        Msg: 'Số chuyến sai.',
                        NotifyType: Common.Message.NotifyType.ERROR
                    });
                    error = false;
                }
                if (item.VehicleID < 1) {
                    $rootScope.Message({
                        Msg: 'Chưa chọn số xe.',
                        NotifyType: Common.Message.NotifyType.ERROR
                    });
                    error = false;
                }
                if (item.DriverID1 < 1) {
                    $rootScope.Message({
                        Msg: 'Chưa chọn tài xế.',
                        NotifyType: Common.Message.NotifyType.ERROR
                    });
                    error = false;
                }
                if(error == true) {
                    $rootScope.IsLoading = true;
                    Common.Services.Call($http, {
                        url: Common.Services.url.FLM,
                        method: _FLMSetting_PHTDI.URL.Save,
                        data: { item: item },
                        success: function (res) {
                            $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                            grid.dataSource.read();
                            $scope.IsSettingEdit = false;
                            $rootScope.IsLoading = false;
                        }
                    });
                }
            }
        }
        $timeout(function () {
            $scope.IsSaveClick = 1;
        }, 3000);

    }

    $scope.StationCost_Click = function ($event, data, win) {
        $event.preventDefault();
        $scope.masterID = data.ID;
        Common.Services.Call($http, {
            url: Common.Services.url.FLM,
            method: _FLMSetting_PHTDI.URL.StationCost,
            data: { masterID: $scope.masterID },
            success: function (res) {
                $.each(res, function (i, v) {
                    v.IsChoose = false;
                });
                $scope.StationCost_gridOptions.dataSource.data(res);
            }
        });
        $timeout(function () { $scope.StationCost_grid.resize(); }, 1);
        win.center().open();
    }

    $scope.TroubleCost_Click = function ($event, data, win) {
        $event.preventDefault();
        $scope.masterID = data.ID;
        $scope.IsAddTroubleCost = true;
        Common.Services.Call($http, {
            url: Common.Services.url.FLM,
            method: _FLMSetting_PHTDI.URL.TroubleCost,
            data: { masterID: $scope.masterID },
            success: function (res) {
                $.each(res, function (i, v) {
                    v.IsChoose = false;
                });
                $scope.TroubleCost_gridOptions.dataSource.data(res);
            }
        });
        $timeout(function () { $scope.TroubleCost_grid.resize(); }, 1);
        win.center().open();
    }


    $scope.StationCost_gridOptions = {
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number', editable: false, nullable: false },
                }
            },
            pageSize: 20
        }),
        height: '99%', groupable: false, pageable: false, sortable: false, columnMenu: false, resizable: true,
        selectable: false, filterable: false, reorderable: true, editable: false,
        columns: [
             {
                 title: ' ', width: '40px',
                 headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,StationCost_grid,StationCost_gridChooseChange)" />',
                 headerAttributes: { style: 'text-align: center;' },
                 template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,StationCost_grid,StationCost_gridChooseChange)" />',
                 templateAttributes: { style: 'text-align: center;' },
                 filterable: false, sortable: false
             },
            {
                field: 'LocationCode', width: '100px', title: 'Mã trạm', filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'LocationName', width: '150px', title: 'Tên trạm', filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'LocationAddress', width: '150px', title: 'Địa chỉ', filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'Price', width: '150px', title: 'Giá',
                template: '<input ng-model="dataItem.Price" class="k-textbox" type="number" style="width: 100%;" ></input>',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            { title: ' ', field: '', sortable: false, filterable: false, menu: false }
        ],

    }

    $scope.StationCost_gridChooseChange = function ($event, grid, haschoose) {
        $scope.StationCost_gridHasChoose = haschoose;
    };

    $scope.StationNotList_gridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.FLM,
            method: _FLMSetting_PHTDI.URL.StationCostNoList,
            readparam: function () {
                return {
                    masterID: $scope.masterID
                };
            },
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number', editable: false, nullable: false },
                    IsChoose: { type: 'boolane', defaultValue: false },
                }
            },
            pageSize: 20
        }),
        height: '99%', groupable: false, pageable: false, sortable: false, columnMenu: false, resizable: true,
        selectable: false, filterable: false, reorderable: true, editable: false,
        columns: [
            {
                title: ' ', width: '40px',
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,StationNotList_grid,StationNotList_gridChooseChange)" />',
                headerAttributes: { style: 'text-align: center;' },
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,StationNotList_grid,StationNotList_gridChooseChange)" />',
                templateAttributes: { style: 'text-align: center;' },
                filterable: false, sortable: false
            },
            {
                field: 'LocationCode', width: '100px', title: 'Mã trạm', filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'LocationName', width: '150px', title: 'Tên trạm', filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'LocationAddress', width: '150px', title: 'Địa chỉ', filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            { title: ' ', field: '', sortable: false, filterable: false, menu: false }
        ],

    }
    $scope.StationNotList_gridChooseChange = function ($event, grid, haschoose) {
        $scope.StationNotListHasChoose = haschoose;
    };

    $scope.Add_StationNotList_Click = function ($event, grid, win) {
        $event.preventDefault();
        var data = $scope.StationCost_gridOptions.dataSource.data();
        $.each(grid.dataSource.data(), function (i, v) {
            if (v.IsChoose == true) {
                v.IsChoose = false;
                data.push(v);
            }
        });
        $scope.StationCost_gridOptions.dataSource.data(data);
        win.close();
    }

    $scope.StationCost_New_Click = function ($event, win) {
        $event.preventDefault();
        $scope.StationNotList_grid.dataSource.read();
        $timeout(function () {
            $scope.StationNotList_grid.resize();
        }, 10);
        win.center().open();
    }


    $scope.StationCost_Save_Click = function ($event, grid,win) {
        $event.preventDefault();
        var data = grid.dataSource.data();

        if (Common.HasValue(data)) {
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.FLM,
                method: _FLMSetting_PHTDI.URL.StationCost_Save,
                data: { lst: data, masterID: $scope.masterID },
                success: function (res) {
                    $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                    $scope.setting_PHTDI_grid.dataSource.read();
                    win.close();
                    $rootScope.IsLoading = false;
                }
            });
        }
    }

    $scope.StationCost_Delete_Click = function ($event, grid) {
        $event.preventDefault();
        var data = grid.dataSource.data();
        var lst = [];
        $.each(data, function (i, v) {
            if (v.IsChoose == true) {
                lst.push(v.ID);
            }
        });
        if (Common.HasValue(lst)) {
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.FLM,
                method: _FLMSetting_PHTDI.URL.StationCost_Delete,
                data: { lst: lst, masterID: $scope.masterID },
                success: function (res) {
                    $rootScope.Message({ Msg: 'Xóa thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                    Common.Services.Call($http, {
                        url: Common.Services.url.FLM,
                        method: _FLMSetting_PHTDI.URL.StationCost,
                        data: { masterID: $scope.masterID },
                        success: function (res) {
                            $.each(res, function (i, v) {
                                v.IsChoose = false;
                            });
                            $scope.StationCost_gridOptions.dataSource.data(res);
                        }
                    });
                    $rootScope.IsLoading = false;
                }
            });
        }
    }

    $scope.TroubleCost_gridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.FLM,
            method: _FLMSetting_PHTDI.URL.TroubleCost,
            readparam: function () {
                return {
                    masterID: $scope.masterID
                };
            },
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number', editable: false, nullable: false },
                    IsChoose: { type: 'boolane', defaultValue: false },
                }
            },
            pageSize: 20
        }),
        height: '99%', groupable: false, pageable: false, sortable: false, columnMenu: false, resizable: true,
        selectable: false, filterable: false, reorderable: true,
        editable: { mode: 'inline' },
        dataBound: function () {
            $scope.IsTroubleCostEdit = false;
        },
        columns: [
             {
                 title: ' ', width: '40px',
                 headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,TroubleCost_grid,TroubleCost_gridChooseChange)" />',
                 headerAttributes: { style: 'text-align: center;' },
                 template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,TroubleCost_grid,TroubleCost_gridChooseChange)" />',
                 templateAttributes: { style: 'text-align: center;' },
                 filterable: false, sortable: false
             },
             {
                 title: ' ', width: '85px',
                 template: '<a ng-show="!IsTroubleCostEdit" href="/" ng-click="TroubleCost_Edit_Click($event,TroubleCost_grid)" class="k-button"><i class="fa fa-pencil"></i></a>' +
                     '<a ng-show="IsTroubleCostEdit && TroubleCostEdit.ID==dataItem.ID?true:false" href="/" ng-click="TroubleCost_Save_Click($event,TroubleCost_grid)" class="k-button"><i class="fa fa-check"></i></a>' +
                     '<a ng-show="IsTroubleCostEdit && TroubleCostEdit.ID==dataItem.ID?true:false" href="/" ng-click="TroubleCost_Cancel_Click($event,TroubleCost_grid)" class="k-button"><i class="fa fa-ban"></i></a>',
                 filterable: false, sortable: false
             },
             {
                 field: 'GroupOfTroubleName', title: 'Loại chi phí', template: "#=GroupOfTroubleName#", width: "120px",
                 editor: '<input ng-model="TroubleCostEdit.GroupOfTroubleID"  class="cus-combobox" focus-k-combobox  kendo-combo-box="cboGroupOfTrouble_cboGroup" k-options="cboGroupOfTrouble_Options"  />'
             },
             {
                 field: 'Cost', width: "120px", title: 'Chi phí',
                 editor: '<input ng-model="TroubleCostEdit.Cost" class="k-textbox" type="number" style="width: 100%;" ></input>'
             },
             {
                 field: 'Description', width: "120px", title: 'Ghi chú',
                 editor: '<input ng-model="TroubleCostEdit.Description" class="k-textbox" type="text" style="width: 100%;" ></input>'
             },
            { title: ' ', field: '', sortable: false, filterable: false, menu: false }
        ],
    };

    $scope.TroubleCost_gridChooseChange = function ($event, grid, haschoose) {
        $scope.TroubleCostHasChoose = haschoose;
    };

    $scope.TroubleCost_New_Click = function ($event, grid) {
        $event.preventDefault();
        var itemNew = { IsChoose: false, GroupOfTroubleID: _FLMSetting_PHTDI.Data.DataCBO.lstGroupOfTrouble[0].ID, GroupOfTroubleName: _FLMSetting_PHTDI.Data.DataCBO.lstGroupOfTrouble[0].Name, Cost: 0, Description: "" };
        $scope.IsTroubleCostEdit = false;
        grid.dataSource.insert(0, itemNew);
        $timeout(function () {
            $scope.IsTroubleCostEdit = true;
            var tr = grid.tbody.find('tr:first');
            $scope.TroubleCostEdit = grid.dataItem(tr);
            _FLMSetting_PHTDI.Data.ItemBackUp = $.extend(true, {}, $scope.SettingEdit);
            grid.editRow(tr);
        }, 1)
    }

    $scope.TroubleCost_Edit_Click = function ($event, grid) {
        $event.preventDefault();
        $timeout(function () {
            $scope.IsTroubleCostEdit = true;
            var tr = $event.target.closest('tr');
            $scope.TroubleCostEdit = grid.dataItem(tr);
            _FLMSetting_PHTDI.Data.ItemBackUp = $.extend(true, {}, $scope.SettingEdit);
            grid.editRow(tr);
        }, 1)

    }

    $scope.TroubleCost_Cancel_Click = function ($event, grid) {
        $event.preventDefault();
        var tr = $($event.target).closest('tr');
        var item = grid.dataItem(tr);
        $rootScope.IsLoading = true;
        $scope.IsTroubleCostEdit = false;
        if (Common.HasValue(item)) {
            var data = grid.dataSource.data();
            if (item.ID > 0) {
                var i = 0;
                while (i < data.length) {
                    var obj = data[i];
                    if (obj.ID == _FLMSetting_PHTDI.Data.ItemBackUp.ID) {
                        data.splice(i, 1);
                        data.splice(i, 0, $.extend(true, {}, _FLMSetting_PHTDI.Data.ItemBackUp))
                    }
                    i++;
                }
            }
            else {
                data.remove(item);
            }
        }
        $rootScope.IsLoading = false;
    }

    $scope.TroubleCost_DeleteAll_Click = function ($event, grid) {
        $event.preventDefault();
        var lst = [];
        $.each(grid.dataSource.data(), function (i, v) {
            if (v.ID > 0 && v.IsChoose == true) {
                lst.push(v.ID);
            }
        });

        if (Common.HasValue(lst)) {
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.FLM,
                method: _FLMSetting_PHTDI.URL.TroubleCost_Delete,
                data: { lst: lst },
                success: function (res) {
                    $rootScope.Message({ Msg: 'Xóa thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                    Common.Services.Call($http, {
                        url: Common.Services.url.FLM,
                        method: _FLMSetting_PHTDI.URL.TroubleCost,
                        data: { masterID: $scope.masterID },
                        success: function (res) {
                            $.each(res, function (i, v) {
                                v.IsChoose = false;
                            });
                            $scope.TroubleCost_gridOptions.dataSource.data(res);
                        }
                    });
                    $rootScope.IsLoading = false;
                }
            });
        }
    }

    $scope.TroubleCost_Save_Click = function ($event, grid) {
        $event.preventDefault();
        $scope.IsTroubleCostEdit = false;
        $rootScope.IsLoading = true;
        var i = 0;
        var data = grid.dataSource.data();
        var result = [];
        while (i < data.length) {
            var obj = data[i];
            if (obj.ID == $scope.TroubleCostEdit.ID) {
                obj = $.extend(true, {}, $scope.TroubleCostEdit);
            }
            result.push(obj);
            i++;
        }
        grid.dataSource.data(result);
        $rootScope.IsLoading = false;
    }

    $scope.TroubleCost_SaveAll_Click = function ($event, grid,win) {
        $event.preventDefault();
        var lst = grid.dataSource.data();
        Common.Services.Call($http, {
            url: Common.Services.url.FLM,
            method: _FLMSetting_PHTDI.URL.TroubleCost_SaveAll,
            data: { lst: lst, masterID: $scope.masterID },
            success: function (res) {
                $rootScope.Message({ Msg: 'Thêm thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                $scope.setting_PHTDI_grid.dataSource.read();
                win.close();
                $scope.IsAsddTroubleCost = true;
                $rootScope.IsLoading = false;
            }
        });
    }

    $scope.cboGroupOfTrouble_Options = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true, dataTextField: 'Name', dataValueField: 'ID',
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    Name: { type: 'string' },
                }
            }
        }),
        change: function (e) {
            var val = this.value();
            var name = this.text();
            var gridEditElement = $scope.TroubleCost_grid.editable.element;
            var dataItem = $scope.TroubleCost_grid.dataItem(gridEditElement.closest('tr'))
            if (dataItem != null) {
                dataItem.GroupOfTroubleID = val;
                dataItem.GroupOfTroubleName = name;
            }
        }
    };


    $scope.Close_Click = function ($event, win) {
        $event.preventDefault();
        win.close();
    }

    $scope.Excel_Click = function ($event, win) {
        $event.preventDefault();
        $rootScope.UploadExcel({
            Path: Common.FolderUpload.Import,
            columns: [
                { field: 'VehicleCode', title: 'Xe', width: '150px', filterable: { cell: { showOperators: false, operator: "contains" } } },
                {
                    field: 'DateConfig', title: 'Từ ngày', template: "#=DateConfig==null?' ':Common.Date.FromJsonDMY(DateConfig)#", width: 120,
                    filterable: { cell: { showOperators: false, operator: "gte" } }
                },
                { field: 'SortOrder', title: 'Chuyến', width: '150px', filterable: { cell: { showOperators: false, operator: "equals" } } },
            ],
            Download: function () {
                $rootScope.IsLoading = true;
                Common.Services.Call($http, {
                    url: Common.Services.url.FLM,
                    method: _FLMSetting_PHTDI.URL.Excel_Export,
                    data: {
                        'dtfrom': $scope.ItemSearch.DateFrom,
                        'dtto': $scope.ItemSearch.DateTo,
                    },
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
                    method: _FLMSetting_PHTDI.URL.Excel_Check,
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
                    method: _FLMSetting_PHTDI.URL.Excel_Save,
                    data: { lst: data },
                    success: function (res) {
                        $scope.setting_PHTDI_gridOptions.dataSource.read()
                        $rootScope.IsLoading = false;
                        $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                    }
                })
            }
        })
    }



    //#region Action
    $scope.ShowSetting = function ($event, grid) {
        $event.preventDefault();
        $rootScope.ShowSetting({
            ListView: views.FLMSetting,
            event: $event,
            grid: grid,
            current: $state.current
        });
    };
    $scope.HideSetting = function ($event) {
        $event.preventDefault();
        $rootScope.HideSetting();
    }
    //#endregion
}]);