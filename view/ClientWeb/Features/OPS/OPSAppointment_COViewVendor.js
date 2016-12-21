/// <reference path="~/Scripts/jquery-1.9.1.intellisense.js" />
/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />

var _OPSAppointment_COViewVendor = {
    URL: {
        COTO_List: 'OPSCO_VEN_COTOContainer_List',
        COTO_Add_No:'OPSCO_VEN_COTOContainer_Add_No',
        Driver_List: 'OPSCO_VEN_Driver_List',
        Tractor_List: 'OPSCO_VEN_Tractor_List',
        Romooc_List: 'OPSCO_VEN_Romooc_List',
        Reason_List: 'OPSCO_VEN_Reason_List',
        Reject: 'OPSCO_VEN_Reject',
        Accept: 'OPSCO_VEN_Accept',

        VENVehicle_List: 'OPSCO_VEN_Vehicle_List',
        Change_Time: 'OPSCO_VEN_Change_Time',
        Change_Vehicle: 'OPSCO_VEN_Change_Vehicle',
        Change_Driver: 'OPSCO_VEN_Change_Driver',
        Vehicle_New: 'OPSCO_VEN_Vehicle_New'
    }
}

angular.module('myapp').controller('OPSAppointment_COViewVendorCtrl', ['$rootScope', '$scope', '$http', '$location', '$state', '$timeout', 'openMapV2', function ($rootScope, $scope, $http, $location, $state, $timeout, openMapV2) {
    if ($rootScope.IsPageComplete != true) return;
    Common.Log('OPSAppointment_COViewVendorCtrl');
    $rootScope.IsLoading = false;

    $scope.Summary = '';
    $scope.TypeOfView = 1;
    $scope.HasChoose = false;
    $scope.Auth = $rootScope.GetAuth();
    
    //#region ViewVendor
    $scope.conGrid_Options = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.OPS,
            method: _OPSAppointment_COViewVendor.URL.COTO_List,
            pageSize: 0,
            group: [{ field: 'TOMasterCode' }],
            readparam: function () {
                return {
                    typeOfView: $scope.TypeOfView
                }
            },
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    IsChoose: { type: 'bool', defaultValue: false },
                    ETD: { type: 'date' },
                    ETA: { type: 'date' },
                    TOETD: { type: 'date' },
                    TOETA: { type: 'date' },
                    TOLastUpdateTime: { type: 'date' },
                    TOCreatedDate: { type: 'date' },
                    Ton: { type: 'number' }
                }
            }
        }),
        height: '99%', groupable: false, pageable: false, columnMenu: false, resizable: true, reorderable: false, sortable: true, filterable: { mode: 'row' },
        toolbar: kendo.template($('#conGrid_Toolbar').html()),
        dataBound: function () {
            $scope.HasChoose = false;
            $(this.element).find('chkGroupChooseAll').prop('checked', false);
            if ($scope.TypeOfView != 1) {
                this.hideColumn("Check");
            } else {
                this.showColumn("Check");
            }
        },
        columns: [
            {
                field: 'Check', title: ' ', width: '35px',
                sortorder: 0, configurable: false, isfunctionalHidden: false,
                headerTemplate: '<input class="chkGroupChooseAll" type="checkbox" ng-click="onGroupChooseAll($event,conGrid)" />',
                headerAttributes: { style: 'text-align: center;' }, attributes: { style: 'text-align: center;' }, filterable: false, sortable: false, groupable: false
            },
            {
                field: 'TOMasterCode', width: '100px', title: 'Mã chuyến',
                sortorder: 1, configurable: true, isfunctionalHidden: false,
                filterable: { cell: { operator: 'contains', showOperators: false } },
                groupHeaderTemplate: function (e) {
                    try {
                        var obj = e.aggregates.parent().items[0];
                        var tmp = e.aggregates.parent().items;
                        if (Common.HasValue(obj)) {
                            var sumTon = 0, sumCBM = 0, sumQty = 0;
                            Common.Data.Each(e.aggregates.parent().items, function (o) {
                                sumTon += o.Ton; sumCBM += o.CBM; sumQty += o.Quantity;
                            })
                            var strVeh = obj.TOVehicleNo == "" || obj.TOVehicleNo == null ? "[Chưa nhập]" : obj.TOVehicleNo;
                            var strRom = obj.TORomoocNo == "" || obj.TORomoocNo == null ? "[Chưa nhập]" : obj.TORomoocNo;
                            var strTel = obj.TODriverTel == "" || obj.TODriverTel == null ? "[Chưa nhập]" : obj.TODriverTel;
                            var strName = obj.TODriverName == "" || obj.TODriverName == null ? "[Chưa nhập]" : obj.TODriverName;
                            var style = "position:relative;top:2px;";
                            if ($scope.TypeOfView != 1)
                                style = "position:relative;top:2px;display:none;";
                            return "<input style='" + style + "' ng-click='onGroupChoose($event,conGrid)' "
                                + "type='checkbox' class='chkGroupChoose' data-item='" + JSON.stringify(obj, Common.JSON.QuoteReplacer) + "'></input>"
                                + "<a class='btnGroup' href='/' ng-click='Group_Click($event)' data-modified='false' data-item='" + JSON.stringify(tmp) + "'><span style='padding-left:5px;'>" + obj.TOMasterCode + " - " + obj.TOVendorCode + " - " + "</span>"
                                + "<span data-index='1'>" + strVeh + " - </span><span data-index='2'>" + strRom + "</span><span data-index='3'> - " + strName + " - " + strTel
                                + "</span><span data-index='4'> - " + Common.Date.FromJsonDDMMHM(obj.TOETD) + " - " + Common.Date.FromJsonDDMMHM(obj.TOETA) + "</span></a>"
                                + "<span style='padding-left:5px;' class='splitter hidden'>" + obj.TOMasterCode + " - " + obj.TOVendorCode + "</span><span class='splitter hidden'> - </span>"
                                + "<input class='cus-combobox cbo-veh hidden' style='width:120px;'/><span class='splitter hidden'> - </span><input class='cus-combobox cbo-rom hidden' style='width:120px;'/><span class='splitter hidden'> - </span>"
                                + "<input class='cus-complete hidden' style='width:100px;'/><span class='splitter hidden'> - </span><input class='k-textbox hidden' placeholder='SĐT' style='width:100px;'/>"
                                + "<span class='hidden'> - " + Common.Date.FromJsonDDMMHM(obj.TOETD) + " - " + Common.Date.FromJsonDDMMHM(obj.TOETA) + "</span>"
                                + "<span style='font-size:12px;font-weight:lighter;'> - cập nhật cuối: " + Common.Date.FromJsonDDMMHM(obj.TOLastUpdateTime) + " bởi " + obj.TOLastUpdate + " </span>";
                        }
                    } catch (e) {
                        return "<span>" + e.value + "</span>";
                    }
                }
            },
            {
                field: 'CustomerCode', width: '120px', title: 'Mã khách hàng', filterable: { cell: { operator: 'contains', showOperators: false } },
                sortorder: 2, configurable: true, isfunctionalHidden: false               
            },
            {
                field: 'OrderCode', width: '150px', title: 'Đơn hàng', filterable: { cell: { operator: 'contains', showOperators: false } },
                sortorder: 3, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'ContainerNo', width: '100px', title: 'Số container',
                template: "<a href='/' style='display:block;width:100%;' ng-click='Add_No_Click($event,AddNo_win,dataItem)'>#=ContainerNo!=''?ContainerNo:'&nbsp;'#</a>",
                filterable: { cell: { operator: 'contains', showOperators: false } },
                sortorder: 4, configurable: true, isfunctionalHidden: false                
            },
            {
                field: 'SealNo1', width: '80px', title: 'Số seal 1',
                template: "<a href='/' style='display:block;width:100%;' ng-click='Add_No_Click($event,AddNo_win,dataItem)'>#=SealNo1!=''?SealNo1:'&nbsp;'#</a>",
                filterable: { cell: { operator: 'contains', showOperators: false } },
                sortorder: 5, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'SealNo2', width: '80px', title: 'Số seal 2',
                template: "<a href='/' style='display:block;width:100%;' ng-click='Add_No_Click($event,AddNo_win,dataItem)'>#=SealNo2!=''?SealNo2:'&nbsp;'#</a>",
                filterable: { cell: { operator: 'contains', showOperators: false } },
                sortorder: 6, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'ServiceOfOrderName', width: '100px', title: 'Dịch vụ v/c', filterable: { cell: { operator: 'contains', showOperators: false } },
                sortorder: 7, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'TypeOfContainerName', width: '100px', title: 'Loại container', filterable: { cell: { operator: 'contains', showOperators: false } },
                sortorder: 8, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'Ton', width: '80px', title: 'Tấn', filterable: { cell: { operator: 'gte', showOperators: false } },
                sortorder: 9, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'ETD', width: '120px', title: 'ETD', template: "#=ETD != null ? Common.Date.FromJsonDMYHM(ETD) : ''#",
                filterable: { cell: { template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); }, operator: 'equal', showOperators: false } },
                sortorder: 10, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'ETA', width: '120px', title: 'ETA', template: "#=ETA != null ? Common.Date.FromJsonDMYHM(ETA) : ''#",
                filterable: { cell: { template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); }, operator: 'equal', showOperators: false } },
                sortorder: 11, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'LocationFromCode', width: '100px', title: 'Điểm nhận', filterable: { cell: { operator: 'contains', showOperators: false } },
                sortorder: 12, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'LocationFromAddress', width: '250px', title: 'Địa chỉ nhận', filterable: { cell: { operator: 'contains', showOperators: false } },
                sortorder: 13, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'LocationToCode', width: '100px', title: 'Điểm giao', filterable: { cell: { operator: 'contains', showOperators: false } },
                sortorder: 14, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'LocationToAddress', width: '250px', title: 'Địa chỉ giao', filterable: { cell: { operator: 'contains', showOperators: false } },
                sortorder: 15, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'LocationDepotCode', width: '120px', title: 'Điểm lấy rỗng', filterable: { cell: { operator: 'contains', showOperators: false } },
                sortorder: 16, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'LocationDepotAddress', width: '120px', title: 'Địa chỉ lấy rỗng', filterable: { cell: { operator: 'contains', showOperators: false } },
                sortorder: 17, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'LocationDepotReturnCode', width: '120px', title: 'Điểm trả rỗng', filterable: { cell: { operator: 'contains', showOperators: false } },
                sortorder: 18, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'LocationDepotReturnAddress', width: '120px', title: 'Địa chỉ trả rỗng', filterable: { cell: { operator: 'contains', showOperators: false } },
                sortorder: 19, configurable: true, isfunctionalHidden: false
            }, 
            {
                field: 'TripNo', width: 100, title: 'Số chuyến', filterable: { cell: { operator: 'contains', showOperators: false } },
                sortorder: 20, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'ShipNo', width: 100, title: 'Số tàu', filterable: { cell: { operator: 'contains', showOperators: false } },
                sortorder: 21, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'ShipName', width: 100, title: 'Tên tàu', filterable: { cell: { operator: 'contains', showOperators: false } },
                sortorder: 22, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'Note0', width: '120px', title: 'Ghi chú', filterable: { cell: { operator: 'contains', showOperators: false } },
                sortorder: 24, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'Note1', width: '120px', title: 'Ghi chú 1', filterable: { cell: { operator: 'contains', showOperators: false } },
                sortorder: 25, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'Note2', width: '120px', title: 'Ghi chú 2', filterable: { cell: { operator: 'contains', showOperators: false } },
                sortorder: 26, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'GroupProductCode', width: '160px', title: 'Mã hàng hóa', filterable: false, sortable: false,
                sortorder: 27, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'GroupProductName', width: '160px', title: 'Tên hàng hóa', filterable: false, sortable: false,
                sortorder: 28, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'TOVendorCode', width: '150px', title: 'Đối tác', filterable: { cell: { operator: 'contains', showOperators: false } } ,
                sortorder: 29, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'TOVehicleNo', width: '120px', title: 'Số đầu kéo', filterable: { cell: { operator: 'contains', showOperators: false } } ,
                sortorder: 30, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'TORomoocNo', width: '120px', title: 'Số romooc', filterable: { cell: { operator: 'contains', showOperators: false } } ,
                sortorder: 31, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'TODriverName', width: '100px', title: 'Tài xế', filterable: { cell: { operator: 'contains', showOperators: false } },
                sortorder: 32, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'TODriverTel', width: '100px', title: 'SĐT', filterable: { cell: { operator: 'contains', showOperators: false } } ,
                sortorder: 33, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'RateNote', width: '150px', title: 'Ghi chú', filterable: { cell: { operator: 'contains', showOperators: false } },
                sortorder: 34, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'TOCreatedDate', width: '160px', title: 'Ngày tạo chuyến', template: "#=TOCreatedDate != null ? Common.Date.FromJsonDMYHM(TOCreatedDate) : ''#",
                filterable: { cell: { template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); }, operator: 'equal', showOperators: false } },
                sortorder: 35, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'TOCreatedBy', width: '160px', title: 'Người tạo chuyến', filterable: { cell: { operator: 'contains', showOperators: false } },
                sortorder: 36, configurable: true, isfunctionalHidden: false
            },
            { title: ' ', filterable: false, menu: false, sortable: false, sortorder: 100, configurable: false, isfunctionalHidden: false }
        ]
    };
    
    $scope.onGroupChooseAll = function ($event, grid) {
        var value = $($event.currentTarget).prop('checked');
        grid.tbody.find('.k-grouping-row .chkGroupChoose').each(function () {
            var obj = $(this).data('item');
            if (Common.HasValue(obj)) {
                $(this).prop('checked', value);
            }
        })
        $scope.HasChoose = value;
    }

    $scope.COTOItem = {};
    $scope.Add_No_Click = function ($event, win, dataItem) {
        $event.preventDefault();
        if ($scope.TypeOfView == 2) {
            $scope.COTOItem = $.extend(true, {}, dataItem);
            win.center().open();
        }
    }

    $scope.Add_No_Save = function ($event, win) {
        $event.preventDefault();
        $rootScope.IsLoading = true;
        Common.Services.Call($http, {
            url: Common.Services.url.OPS,
            data: {
                item: $scope.COTOItem
            },
            method: _OPSAppointment_COViewVendor.URL.COTO_Add_No,
            success: function (res) {
                $rootScope.IsLoading = false;
                $rootScope.Message({ Msg: "Thành công!" });
                $scope.conGrid.dataSource.read();
                win.close();
            }
        })
    }

    $scope.onGroupChoose = function ($event, grid) {
        var chk = $($event.currentTarget), val = chk.prop('checked'), obj = chk.data('item');
        if (Common.HasValue(obj)) {

        }
        var flag = false;
        grid.tbody.find('.k-grouping-row .chkGroupChoose').each(function () {
            flag = flag || $(this).prop('checked');
        })
        $scope.HasChoose = flag;
    }

    $scope.$watch("TypeOfView", function () {
        $scope.conGrid.dataSource.read();
    })

    $scope.Veh_Data = [];
    $scope.Rom_Data = [];
    $scope.Drv_Data = [];
    $scope.OPS_Data = [];
    $scope.Reason_Data = [];
    Common.Services.Call($http, {
        url: Common.Services.url.OPS,
        method: _OPSAppointment_COViewVendor.URL.Driver_List,
        success: function (res) {
            Common.Data.Each(res, function (o) {
                if (!Common.HasValue($scope.Drv_Data[o.VendorID])) {
                    $scope.Drv_Data[o.VendorID] = [];
                }
                $scope.Drv_Data[o.VendorID].push(o);
            })
        }
    })
    Common.Services.Call($http, {
        url: Common.Services.url.OPS,
        method: _OPSAppointment_COViewVendor.URL.Tractor_List,
        success: function (res) {
            Common.Data.Each(res, function (o) {
                if (!Common.HasValue($scope.Veh_Data[o.VendorID])) {
                    $scope.Veh_Data[o.VendorID] = [];
                }
                $scope.Veh_Data[o.VendorID].push(o);
            })
        }
    })
    Common.Services.Call($http, {
        url: Common.Services.url.OPS,
        method: _OPSAppointment_COViewVendor.URL.Romooc_List,
        success: function (res) {
            Common.Data.Each(res, function (o) {
                if (!Common.HasValue($scope.Rom_Data[o.VendorID])) {
                    $scope.Rom_Data[o.VendorID] = [];
                }
                $scope.Rom_Data[o.VendorID].push(o);
            })
        }
    })
    Common.Services.Call($http, {
        url: Common.Services.url.OPS,
        method: _OPSAppointment_COViewVendor.URL.Reason_List,
        success: function (res) {
            $scope.Reason_Data = res;
        }
    })

    $scope.VehicleType = 1;
    $scope.VehicleTypeName = "ĐẦU KÉO";
    $scope.TOItem = { ID: -1, VendorID: -1 };
    $scope.Group_Click = function ($event) {
        $event.preventDefault();
        if ($scope.TypeOfView == 1) {
            var target = $event.currentTarget, element = target.parentElement, obj = $(target).data('item'),
                cboVeh = $(element).find('.cus-combobox.cbo-veh'), cboRom = $(element).find('.cus-combobox.cbo-rom'),
                cboDrN = $(element).find('.cus-complete'), txDrT = $(element).find('.k-textbox');
            $(target).hide();
            $(target).data('modified', true);
            $(element).find('input, .splitter').removeClass('hidden');
            $(element).find('input, .splitter').show();
            var item = $scope.OPS_Data[obj[0].TOMasterID];
            if (!Common.HasValue(item)) {
                item = {
                    ID: obj[0].TOMasterID,
                    TOMasterCode: obj[0].TOMasterCode,
                    VendorID: obj[0].TOVendorID,
                    VendorCode: obj[0].TOVendorCode,
                    VehicleID: obj[0].TOVehicleID,
                    VehicleNo: obj[0].TOVehicleNo,
                    RomoocID: obj[0].TORomoocID,
                    RomoocNo: obj[0].TORomoocNo,
                    DriverName: obj[0].TODriverName,
                    DriverTel: obj[0].TODriverTel
                }
                $scope.OPS_Data[item.ID] = item;
            }
            var dataRom = $scope.Rom_Data[item.VendorID] || [];
            var dataVeh = $scope.Veh_Data[item.VendorID] || [];
            var dataDrv = $scope.Drv_Data[item.VendorID] || [];
            var cbo_Veh = cboVeh.kendoComboBox({
                index: 0, autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true,
                dataTextField: 'VehicleNo', dataValueField: 'VehicleID',
                dataSource: Common.DataSource.Local({
                    data: dataVeh,
                    model: {
                        id: 'VehicleID', fields: { VehicleNo: { type: 'string' } }
                    }
                }),
                change: function (e) {
                    var o = this.dataItem(this.select());
                    if (Common.HasValue(o)) {
                        item.VehicleID = o.VehicleID;
                        item.VehicleNo = o.VehicleNo;
                        if (o.DriverName != null && o.DriverName != "") {
                            item.DriverName = o.DriverName;
                            item.DriverTel = o.DriverTel;
                            cbo_DrN.value(item.DriverName);
                            txDrT.val(item.DriverTel);
                        }
                    }
                }
            }).data('kendoComboBox');
            cbo_Veh.value(item.VehicleID);
            var cbo_Rom = cboRom.kendoComboBox({
                index: 0, autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true,
                dataTextField: 'RomoocNo', dataValueField: 'RomoocID',
                dataSource: Common.DataSource.Local({
                    data: dataRom,
                    model: {
                        id: 'RomoocID', fields: { RomoocNo: { type: 'string' } }
                    }
                }),
                change: function (e) {
                    var o = this.dataItem(this.select());
                    if (Common.HasValue(o)) {
                        item.RomoocID = o.RomoocID;
                        item.RomoocNo = o.RomoocNo;
                    }
                }
            }).data('kendoComboBox');
            cbo_Rom.value(item.RomoocID);
            var cbo_DrN = cboDrN.kendoAutoComplete({
                autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true, placeholder: "Tài xế",
                dataTextField: 'DriverName', dataValueField: 'DriverName',
                dataSource: Common.DataSource.Local({
                    data: dataDrv,
                    model: {
                        id: 'ID', fields: { DriverName: { type: 'number' } }
                    }
                }),
                change: function () {
                    item.DriverName = this.value();
                    var o = this.dataItem(this.select());
                    if (Common.HasValue(o)) {
                        item.DriverTel = o.DriverTel;
                        txDrT.val(item.DriverTel);
                    }
                }
            }).data('kendoAutoComplete');
            cbo_DrN.value(item.DriverName);
            txDrT.val(item.DriverTel);
            txDrT.change(function () {
                item.DriverTel = $(this).val();
            })
        }
        else if ($scope.TypeOfView == 2) {
            var target = $event.currentTarget, element = target.parentElement, obj = $(target).data('item')[0], index = $($event.target).data('index');
            switch (index) {
                case 1:
                    $scope.TOItem.ID = obj.TOMasterID;
                    $scope.TOItem.VendorID = obj.TOVendorID;
                    $scope.VehicleType = 1;
                    $scope.VehicleTypeName = "ĐẦU KÉO";
                    $scope.vehicle_win.center().open();
                    $scope.vehicleGrid.dataSource.read();
                    $scope.vehicleGrid.hideColumn('RomoocNo');
                    $scope.vehicleGrid.showColumn('VehicleNo');
                    break;
                case 2:
                    $scope.TOItem.ID = obj.TOMasterID;
                    $scope.TOItem.VendorID = obj.TOVendorID;
                    $scope.VehicleType = 2;
                    $scope.VehicleTypeName = "ROMOOC";
                    $scope.vehicle_win.center().open();
                    $scope.vehicleGrid.dataSource.read();
                    $scope.vehicleGrid.showColumn('RomoocNo');
                    $scope.vehicleGrid.hideColumn('VehicleNo');
                    break;
                case 3:
                    $scope.TOItem.ID = obj.TOMasterID;
                    $scope.TOItem.VendorID = obj.TOVendorID;
                    $scope.TOItem.DriverTel = obj.TODriverTel;
                    $scope.TOItem.DriverName = obj.TODriverName;
                    var dataDrv = $scope.Drv_Data[$scope.TOItem.VendorID] || [];
                    $scope.atcDriverName.dataSource.data(dataDrv);
                    $scope.driver_win.center().open();
                    break;
                case 4:
                    $scope.TOItem.ID = obj.TOMasterID;
                    $scope.TOItem.VendorID = obj.TOVendorID;
                    $scope.TOItem.ETA = Common.Date.FromJson(obj.TOETA);
                    $scope.TOItem.ETD = Common.Date.FromJson(obj.TOETD);
                    $scope.time_win.center().open();
                    break;
                default:
                    break;
            }
        }
    }

    $scope.atcDriverNameOptions = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true, placeholder: "Tài xế",
        dataTextField: 'DriverName', dataValueField: 'DriverName',
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID', fields: { DriverName: { type: 'number' } }
            }
        }),
        change: function () {
            $scope.TOItem.DriverName = this.value();
            var o = this.dataItem(this.select());
            if (Common.HasValue(o)) {
                $scope.TOItem.DriverTel = o.DriverTel;
            }
        }
    }

    $scope.vehicleGrid_Options = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.OPS,
            method: _OPSAppointment_COViewVendor.URL.VENVehicle_List,
            readparam: function () {
                return {
                    typeOfVehicle: $scope.VehicleType, venID: $scope.TOItem.VendorID
                }
            },
            pageSize: 0,
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    MaxWeight: { type: 'number' }
                }
            }
        }),
        height: '99%', groupable: false, pageable: false, sortable: true, columnMenu: false, resizable: true,
        selectable: 'row', filterable: { mode: 'row', visible: false }, reorderable: false,
        columns: [
            { field: 'VehicleNo', width: 150, title: 'Số xe', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'RomoocNo', width: 150, title: 'Số romooc', hidden: true, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'MaxWeight', title: 'Trọng tải', filterable: { cell: { operator: 'gte', showOperators: false } } }
        ]
    }

    $scope.NewVehicleVendor = { RegNo: '', MaxWright: 0 }
    $scope.Vehicle_NEW_Click = function ($event, win) {
        $event.preventDefault();
        $scope.NewVehicleVendor = { RegNo: '', MaxWright: 0 }
        win.center().open();
    }

    $scope.Vehicle_OK_Click = function ($event, grid, win) {
        $event.preventDefault();

        var item = grid.dataItem(grid.select());
        if (Common.HasValue(item)) {
            $rootScope.Message({
                Msg: "Xác nhận lưu phương tiện?",
                Type: Common.Message.Type.Confirm,
                Ok: function () {
                    $rootScope.IsLoading = true;
                    Common.Services.Call($http, {
                        url: Common.Services.url.OPS,
                        method: _OPSAppointment_COViewVendor.URL.Change_Vehicle,
                        data: {
                            mID: $scope.TOItem.ID,
                            vehID: $scope.VehicleType == 1 ? item.ID : -1,
                            romID: $scope.VehicleType == 2 ? item.ID : -1
                        },
                        success: function (res) {
                            $rootScope.IsLoading = false;
                            $rootScope.Message({ Msg: "Thành công!" });
                            $scope.conGrid.dataSource.read();
                            win.close();
                        }
                    })
                }
            })
        } else {
            win.close();
        }
    }

    $scope.Vehicle_New_OK_Click = function ($event, grid, win) {
        $event.preventDefault();

        if ($scope.NewVehicleVendor.RegNo == "") {
            $rootScope.Message({
                Msg: "Vui lòng nhập số xe", Type: Common.Message.Type.Alert
            })
        } else {
            $rootScope.Message({
                Msg: "Đồng ý thêm mới phương tiện?",
                Type: Common.Message.Type.Confirm,
                Ok: function () {
                    $rootScope.IsLoading = true;
                    Common.Services.Call($http, {
                        url: Common.Services.url.OPS,
                        method: _OPSAppointment_COViewVendor.URL.Vehicle_New,
                        data: {
                            venID: $scope.TOItem.VendorID,
                            regNo: $scope.NewVehicleVendor.RegNo,
                            maxWeight: $scope.NewVehicleVendor.MaxWeight,
                            typeOfVehicle: $scope.VehicleType
                        },
                        success: function (res) {
                            $rootScope.IsLoading = false;
                            $rootScope.Message({ Msg: "Thành công!" });
                            grid.dataSource.read();
                            win.close();
                        }
                    })
                }
            })
        }
    }
    
    $scope.Driver_OK_Click = function ($event, win, grid) {
        $event.preventDefault();

        if ($scope.TOItem.DriverName == "") {
            $rootScope.Message({
                Msg: "Vui lòng nhập tên tài xế", Type: Common.Message.Type.Alert
            })
        } else {
            $rootScope.Message({
                Msg: "Xác nhận lưu tài xế?",
                Type: Common.Message.Type.Confirm,
                Ok: function () {
                    $rootScope.IsLoading = true;
                    Common.Services.Call($http, {
                        url: Common.Services.url.OPS,
                        method: _OPSAppointment_COViewVendor.URL.Change_Driver,
                        data: {
                            mID: $scope.TOItem.ID,
                            name: $scope.TOItem.DriverName,
                            tel: $scope.TOItem.DriverTel
                        },
                        success: function (res) {
                            $rootScope.IsLoading = false;
                            $rootScope.Message({ Msg: "Thành công!" });
                            grid.dataSource.read();
                            win.close();
                        }
                    })
                }
            })
        }
    }

    $scope.Time_OK_Click = function ($event, win, grid) {
        $event.preventDefault();

        if ($scope.TOItem.ETD == "" || $scope.TOItem.ETD == null) {
            $rootScope.Message({
                Msg: "Vui lòng nhập ETD", Type: Common.Message.Type.Alert
            })
        } else if($scope.TOItem.ETA == "" || $scope.TOItem.ETA == null) {
            $rootScope.Message({
                Msg: "Vui lòng nhập ETA", Type: Common.Message.Type.Alert
            })
        } else {
            $rootScope.Message({
                Msg: "Xác nhận lưu thời gian?",
                Type: Common.Message.Type.Confirm,
                Ok: function () {
                    $rootScope.IsLoading = true;
                    Common.Services.Call($http, {
                        url: Common.Services.url.OPS,
                        method: _OPSAppointment_COViewVendor.URL.Change_Time,
                        data: {
                            mID: $scope.TOItem.ID,
                            ETA: $scope.TOItem.ETA,
                            ETD: $scope.TOItem.ETD
                        },
                        success: function (res) {
                            $rootScope.IsLoading = false;
                            $rootScope.Message({ Msg: "Thành công!" });
                            grid.dataSource.read();
                            win.close();
                        }
                    })
                }
            })
        }
    }    

    $scope.Accept_Click = function ($event, grid) {
        $event.preventDefault();

        var data = [];
        grid.tbody.find('.k-grouping-row .chkGroupChoose').each(function () {
            if ($(this).prop('checked')) {
                var chk = $(this), ele = $(chk).parent(), btn = $(ele).find('a.btnGroup'), obj = $(chk).data('item');
                if (btn.data('modified') == true || btn.data('modified') == 'true') {
                    data.push($scope.OPS_Data[obj.TOMasterID]);
                } else {
                    data.push({
                        ID: obj.TOMasterID, TOMasterCode: obj.TOMasterCode,
                        VendorID: obj.TOVendorID, VendorCode: obj.TOVendorCode,
                        VehicleID: obj.TOVehicleID, VehicleNo: obj.TOVehicleNo,
                        RomoocID: obj.TORomoocID, RomoocNo: obj.TORomoocNo,
                        DriverName: obj.TODriverName, DriverTel: obj.TODriverTel
                    });
                }
            }
        })
        if (data.length > 0) {
            var error = [];
            //Common.Data.Each(data, function (o) {
            //    if (o.VehicleID <= 2)
            //        error.push(o.TOMasterCode);
            //})
            if (error.length == 0) {
                $rootScope.Message({
                    Msg: "Xác nhận vận chuyển các chuyến đã chọn?",
                    Type: Common.Message.Type.Confirm,
                    Ok: function () {
                        $rootScope.IsLoading = true;
                        Common.Services.Call($http, {
                            url: Common.Services.url.OPS,
                            method: _OPSAppointment_COViewVendor.URL.Accept,
                            data: { data: data },
                            success: function (res) {
                                $rootScope.IsLoading = false;
                                $rootScope.Message({ Msg: "Thành công!" });
                                grid.dataSource.read();
                            },
                            error: function () {
                                grid.dataSource.read();
                                $rootScope.IsLoading = false;
                            }
                        })
                    }
                })
            } else {
                $rootScope.Message({
                    Msg: "Vui lòng chọn xe cho các chuyến " + error.join(", "),
                    Type: Common.Message.Type.Alert
                })
            }
        }
    }

    $scope.Reject_Click = function ($event, grid, win) {
        $event.preventDefault();

        var data = [];
        grid.tbody.find('.k-grouping-row .chkGroupChoose').each(function () {
            if ($(this).prop('checked')) {
                var obj = $(this).data('item');
                if (Common.HasValue(obj)) {
                    data.push({
                        ID: obj.TOMasterID, TOMasterCode: obj.TOMasterCode,
                        Reason: "", ReasonID: "", ReasonName: "",
                        DataReason: $scope.Reason_Data
                    });
                }
            }
        })
        if (data.length > 0) {
            win.center().open();
            $scope.rejectGrid.dataSource.data(data);
            $timeout(function () {
                $scope.rejectGrid.refresh();
            }, 100)
        }
    }

    $scope.rejectGrid_Options = {
        dataSource: Common.DataSource.Local({
            data: []
        }),
        height: '100%', groupable: false, pageable: false, sortable: true, columnMenu: false, filterable: false, resizable: true,
        dataBound: function () { },
        columns: [
            { field: 'TOMasterCode', title: 'Mã chuyến', width: 200 },
            {
                field: 'ReasonID', width: 350, title: 'Lý do từ chối',
                template: '<select ng-model="dataItem.ReasonID" kendo-combo-box k-data-text-field="\'ReasonName\'" '
                    + 'k-data-value-field="\'ID\'"  k-filter="\'contains\'" k-auto-bind="true" k-min-length="3" '
                    + 'k-on-data-bound="cboReasonDataBound(kendoEvent, dataItem)" k-data-source="dataItem.DataReason" style="width: 100%"></select>'
            },
            { field: 'Season', title: 'Ghi chú', template: '<input class="k-textbox" ng-model="dataItem.Season" style="width: 100%"/>' }
        ]
    }

    $scope.Reject_OK_Click = function ($event, grid, win) {
        $event.preventDefault();

        var data = grid.dataSource.data();
        var error = [];
        Common.Data.Each(data, function (o) {
            if (o.ReasonID < 1 && o.Reason == "")
                error.push(o.TOMasterCode);
        })
        if (error.length == 0) {
            $rootScope.Message({
                Msg: "Xác nhận từ chối các chuyến đã chọn?",
                Type: Common.Message.Type.Confirm,
                Ok: function () {
                    $rootScope.IsLoading = true;
                    Common.Services.Call($http, {
                        url: Common.Services.url.OPS,
                        method: _OPSAppointment_COViewVendor.URL.Reject,
                        data: { data: data },
                        success: function (res) {
                            $rootScope.IsLoading = false;
                            $rootScope.Message({ Msg: "Thành công!" });
                            win.close();
                            $scope.conGrid.dataSource.read();
                        },
                        error: function () {
                            $scope.conGrid.dataSource.read();
                            $rootScope.IsLoading = false;
                            win.close();
                        }
                    })

                }
            })
        } else {
            $rootScope.Message({
                Msg: "Vui lòng chọn lý do cho các chuyến " + error.join(", "),
                Type: Common.Message.Type.Alert
            })
        }
    }
    //#endregion

    //#region ViewAdmin
    $scope.con2Grid_Options = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.OPS,
            method: _OPSAppointment_COViewVendor.URL.COTO_List,
            pageSize: 0,
            group: [{ field: 'TOMasterCode' }],
            readparam: function () {
                return {
                    typeOfView: $scope.TypeOfViewAdmin
                }
            },
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    IsChoose: { type: 'bool', defaultValue: false },
                    ETD: { type: 'date' },
                    ETA: { type: 'date' },
                    TOETD: { type: 'date' },
                    TOETA: { type: 'date' },
                    TOLastUpdateTime: { type: 'date' },
                    TOCreatedDate: { type: 'date' },
                    Ton: { type: 'number' }
                }
            }
        }),
        height: '99%', groupable: false, pageable: false, columnMenu: false, resizable: true, reorderable: false, sortable: true, filterable: { mode: 'row' },
        toolbar: kendo.template($('#con2Grid_Toolbar').html()),
        dataBound: function () {
            if ($scope.TypeOfViewAdmin == 3) {
                this.showColumn("Reason");
                this.showColumn("ReasonName");
            } else {
                this.hideColumn("Reason");
                this.hideColumn("ReasonName");
            }
        },
        columns: [
            {
                field: 'TOMasterCode', width: '100px', title: 'Mã chuyến',
                filterable: { cell: { operator: 'contains', showOperators: false } },
                groupHeaderTemplate: function (e) {
                    try {
                        var obj = e.aggregates.parent().items[0];
                        var tmp = e.aggregates.parent().items;
                        if (Common.HasValue(obj)) {
                            var sumTon = 0, sumCBM = 0, sumQty = 0;
                            Common.Data.Each(e.aggregates.parent().items, function (o) {
                                sumTon += o.Ton; sumCBM += o.CBM; sumQty += o.Quantity;
                            })
                            var strVeh = obj.TOVehicleNo == "" || obj.TOVehicleNo == null ? "[Chưa nhập]" : obj.TOVehicleNo;
                            var strRom = obj.TORomoocNo == "" || obj.TORomoocNo == null ? "[Chưa nhập]" : obj.TORomoocNo;
                            var strTel = obj.TODriverTel == "" || obj.TODriverTel == null ? "[Chưa nhập]" : obj.TODriverTel;
                            var strName = obj.TODriverName == "" || obj.TODriverName == null ? "[Chưa nhập]" : obj.TODriverName;
                            var strRea = obj.ReasonName == "" ? obj.Reason : obj.ReasonName;
                            if (strRea == "")
                                strRea = "[Ko có lý do]";
                            var eleRea = "";
                            if ($scope.TypeOfViewAdmin == 3) {
                                eleRea = "<span> Lý do từ chối: " + strRea + "</span>";
                            }
                            return "<span style='padding-left:5px;'>" + obj.TOMasterCode + " - " + obj.TOVendorCode + " - " + "</span>"
                                + "<span>" + strVeh + " - " + strRom + "</span>" + "<span> - " + strName + " - " + strTel + "</span>"
                                + "<span> - " + Common.Date.FromJsonDDMMHM(obj.TOETD) + " - " + Common.Date.FromJsonDDMMHM(obj.TOETA) + "</span>"
                                + eleRea + "<span style='font-size:12px;font-weight:lighter;'> - cập nhật cuối: " + Common.Date.FromJsonDDMMHM(obj.TOLastUpdateTime) + " bởi " + obj.TOLastUpdate + " </span>";
                        }
                    } catch (e) {
                        return "<span>" + e.value + "</span>";
                    }
                }
            },
            { field: 'CustomerCode', width: '120px', title: 'Mã khách hàng', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'OrderCode', width: '150px', title: 'Đơn hàng', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'ContainerNo', width: '100px', title: 'Số container', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'SealNo1', width: '80px', title: 'Số seal 1', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'SealNo2', width: '80px', title: 'Số seal 2', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'ServiceOfOrderName', width: '100px', title: 'Dịch vụ v/c', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'TypeOfContainerName', width: '100px', title: 'Loại container', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'Ton', width: '80px', title: 'Tấn', filterable: { cell: { operator: 'gte', showOperators: false } } },
            {
                field: 'ETD', width: '120px', title: 'ETD', template: "#=ETD != null ? Common.Date.FromJsonDMYHM(ETD) : ''#",
                filterable: { cell: { template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); }, operator: 'equal', showOperators: false } }
            },
            {
                field: 'ETA', width: '120px', title: 'ETA', template: "#=ETA != null ? Common.Date.FromJsonDMYHM(ETA) : ''#",
                filterable: { cell: { template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); }, operator: 'equal', showOperators: false } }
            },
            { field: 'LocationFromCode', width: '100px', title: 'Điểm nhận', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'LocationFromAddress', width: '250px', title: 'Địa chỉ', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'LocationToCode', width: '100px', title: 'Điểm giao', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'LocationToAddress', width: '250px', title: 'Địa chỉ', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'TripNo', width: 100, title: 'Số chuyến', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'ShipNo', width: 100, title: 'Số tàu', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'ShipName', width: 100, title: 'Tên tàu', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'Note0', width: 150, title: 'Ghi chú', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'Note1', width: 150, title: 'Ghi chú 1', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'Note2', width: 150, title: 'Ghi chú 2', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'TOVendorCode', width: '150px', title: 'Đối tác', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'TOVehicleNo', width: '120px', title: 'Số đầu kéo', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'TORomoocNo', width: '120px', title: 'Số romooc', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'TODriverName', width: '100px', title: 'Tài xế', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'TODriverTel', width: '100px', title: 'SĐT', filterable: { cell: { operator: 'contains', showOperators: false } } },
            {
                field: 'TOCreatedDate', width: '160px', title: 'Ngày tạo chuyến', template: "#=TOCreatedDate != null ? Common.Date.FromJsonDMYHM(TOCreatedDate) : ''#",
                filterable: { cell: { template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); }, operator: 'equal', showOperators: false } }
            },
            { field: 'TOCreatedBy', width: '160px', title: 'Người tạo chuyến', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'ReasonName', width: '200px', title: 'Lý do từ chối', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'Reason', width: '200px', title: 'Ghi chú từ chối', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { title: ' ', filterable: false, menu: false, sortable: false }
        ]
    }

    $scope.TypeOfViewAdmin = 1;
    $scope.$watch("TypeOfViewAdmin", function () {
        $scope.con2Grid.dataSource.read();
    })

    //#endregion

    //#region Action
    $scope.Close_Click = function ($event, win, code) {
        $event.preventDefault();
        
        try {
            switch (code) {
                default:
                    break;
            }
            win.close();
        } catch (e) {
        }
    }

    $scope.ShowSetting = function ($event, grid) {
        $event.preventDefault();

        $rootScope.ShowSetting({
            ListView: views.OPSAppointmentCO_Ven,
            event: $event, grid: grid,
            current: $state.current
        });
    };

    $scope.HideSetting = function ($event) {
        $event.preventDefault();
        $rootScope.HideSetting();
    }
    //#endregion
}]);
