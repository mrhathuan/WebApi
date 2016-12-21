/// <reference path="~/Scripts/jquery-1.9.1.intellisense.js" />
/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />

//#region OPS object
var _OPSAppointment_CORouteTOMaster = {
    URL: {

        VehicleListVendor: 'OPS_COAppointment_Route_VehicleListVendor',
        VehicleListTractor: 'OPS_COAppointment_Route_VehicleListTractor',
        VehicleListRomooc: 'OPS_COAppointment_Route_VehicleListRomooc',
        VehicleListDriver: 'OPS_DIAppointment_Route_VehicleListDriver',
        Master_CheckDriver: 'COAppointment_2View_Master_CheckDriver',
        Delete: 'COAppointment_2View_Master_Delete',
        Tendered: 'COAppointment_2View_Master_Tender',
        UnApproved: 'COAppointment_2View_Master_Revert',
        Save: 'COAppointment_2View_Master_Update',
        ContainerHasMaster_List: 'OPS_COAppointment_ContainerHasMaster_List',
        Container_List: 'OPS_COAppointment_Container_List',
        Master_List: 'OPS_COAppointment_Master_List',
        CustomerList: 'OPS_COAppointment_Route_CustomerList',
        Tender: 'COAppointment_2View_Master_ToVendor',
        Reject: 'OPS_DIAppointment_Route_TenderReject',
        Accept: 'OPS_DIAppointment_Route_TenderApproved',
        SendMailToTender: 'COAppointment_2View_Master_ToVendor_Email',
        FLMPlaning: 'OPS_DIAppointment_FLMPlaning',
    },
    Data: {
        _dataHasDN: [],
        _dataHasDNMaster: [],
        _dataHasDNSort: [],
        _dataHasDNVendor: [],
        _dataHasDNTractor: [],
        _dataHasDNVehicle: [],
        _dataVehicleVEN: [],
        _dataDriver: [],
        _formatMoney: "n0",
        FLMPlaning: [],
    }
};

angular.module('myapp').controller('OPSAppointment_CORouteTOMasterCtrl', ['$rootScope', '$scope', '$http', '$location', '$state', '$timeout', function ($rootScope, $scope, $http, $location, $state, $timeout) {
    if ($rootScope.IsPageComplete != true) return;
    Common.Log('OPSAppointment_CORouteTOMasterCtrl');

    $scope.Search = {};
    $scope.Search.ListCustomerID = [];
    $scope.Search.TypeOfViewID = 1;
    $scope.Search.DateFrom = new Date().addDays(-3);
    $scope.Search.DateTo = new Date().addDays(3);
    $scope.IsChangeView = false;

    $scope.RateTime = 0;
    $scope.totalTonNoMaster = 0;
    Common.Cookie.Set("Scroll", 0);
    $scope.SummaryMaster = '';
    _OPSAppointment_CORouteTOMaster.Data._dataTypeOfView = [];
    _OPSAppointment_CORouteTOMaster.Data._dataTypeOfView.push({ ID: 1, TypeOfViewName: 'Đang lập' });
    _OPSAppointment_CORouteTOMaster.Data._dataTypeOfView.push({ ID: 2, TypeOfViewName: 'Đã duyệt' });
    _OPSAppointment_CORouteTOMaster.Data._dataTypeOfView.push({ ID: 3, TypeOfViewName: 'Gửi Tender' });
    _OPSAppointment_CORouteTOMaster.Data._dataTypeOfView.push({ ID: 4, TypeOfViewName: 'Tất cả' });

    $scope.gridNoDNMasterOptions = {
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields:
                    {
                        ID: { type: 'number' },
                        ETD: { type: 'date' },
                        ETA: { type: 'date' },
                    }
            },
            group: [{ field: 'CreateSortOrder', dir: 'desc' }]
        }),
        height: '99%', groupable: false, pageable: false, columnMenu: false, resizable: true, reorderable: true, sortable: false,
        columns: [

            {
                field: 'CreateSortOrder', width: '65px',
                template: '<form class="cus-form-enter" ng-submit="SortEnter_Click($event)">' +
                    '<input kendo-numeric-text-box class="txtCreateSortOrder" value="#=CreateSortOrder>0?CreateSortOrder:0#" data-k-min="0" k-options="numericSortOrderMasterOptions" style="width:100%" /></form>',
                headerTemplate: '<a class="k-button" href="/" ng-click="Expand_Click($event,gridNoDNMaster)"><i class="fa fa-minus"></i></a>' +
                    '<input type="checkbox" ng-model="ValCheckBox" ng-click="CheckBox_All_Check($event,gridNoDNMaster)" />',
                headerAttributes: { style: 'text-align: left;' },
                groupHeaderTemplate: '<span class="HasDNGridGroup" tabid="#=value#"></span>', filterable: false, menu: false
            },
            { field: 'ContainerNo', width: '80px', title: 'SO Con', sortable: true, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'OrderCode', width: '90px', title: 'Mã ĐH', sortable: true, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'CustomerName', width: '90px', title: 'KH', sortable: true, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'PackingName', width: '90px', title: 'Loại Con', sortable: true, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'StatusOfCOContainerName', width: '90px', title: 'Tình trạng Con', sortable: true, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'Ton', width: '80px', title: 'Tấn', template: '', sortable: true, filterable: { cell: { operator: 'eq', showOperators: false } } },
            { field: 'SealNo1', width: '90px', title: 'SealNo1', sortable: true, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'SealNo2', width: '90px', title: 'SealNo2', sortable: true, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'Note', width: '90px', title: 'Ghi chú', sortable: true, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'ServiceOfOrderName', width: '90px', title: 'Loại vận chuyển', sortable: true, filterable: { cell: { operator: 'contains', showOperators: false } } },
            {
                field: 'ETD', width: '100px', title: 'ETD',
                template: "#=ETD != null ? Common.Date.FromJsonDMY(ETD) : ''#", filterable: {
                    cell: {
                        template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); },
                        operator: 'equal', showOperators: false
                    }
                }
            },
            {
                field: 'ETA', width: '100px', title: 'ETA',
                template: "#=ETA != null ? Common.Date.FromJsonDMY(ETA) : ''#",
                filterable: {
                    cell: {
                        template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); },
                        operator: 'equal', showOperators: false
                    }
                }
            },
            { field: 'LocationFromName', width: '100px', title: 'LocationFromName', sortable: true, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'LocationFromAddress', width: '100px', title: 'Địa chỉ', sortable: true, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'LocationFromProvinceName', width: '100px', title: 'Tỉnh', sortable: true, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'LocationFromDistrictName', width: '100px', title: 'Quận / Huyện', sortable: true, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'LocationToName', width: '100px', title: 'LocationToName', sortable: true, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'LocationToDistrictName', width: '100px', title: 'Quận / Huyện', sortable: true, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'LocationToAddress', width: '200px', title: 'Địa chỉ', sortable: true, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'LocationToProvinceName', width: '100px', title: 'Tỉnh / Thành', sortable: true, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { title: ' ', filterable: false, menu: false, sortable: false }
        ],
        toolbar: kendo.template($('#OPSAppointment_CORouteTOMaster_gridMasterToolbar').html()),
        filterable: { mode: 'row' },
        dataBound: function () {
            Common.Log("GridMasterDataBound");

            if (Common.HasValue(this) && Common.HasValue(this.tbody)) {
                var sort = 0;
                this.element.find('.HasDNGridGroup')
                this.element.find('.HasDNGridGroup').each(function () {
                    sort = parseInt($(this).attr('tabid'));
                    var tr = $(this).closest('tr');
                    if (sort > 0) {
                        var item = _OPSAppointment_CORouteTOMaster.Data._dataHasDNSort[sort];
                        var tr = $(this).closest('tr');
                        if (Common.HasValue(item)) {
                            if (item.IsChange == true) {
                                if (!$(tr).hasClass('approved'))
                                    $(tr).addClass('approved');
                            }
                            else if (item.TypeID == 2) {
                                if (!$(tr).hasClass('tendered'))
                                    $(tr).addClass('tendered');
                            }
                            if (item.TypeID == 3) {
                                if (!$(tr).hasClass('sendtender'))
                                    $(tr).addClass('sendtender');
                            }
                        }
                        if (!Common.HasValue(item)) {
                            item = _OPSAppointment_CORouteTOMaster.Data._dataHasDNSort[0];
                            $.each(_OPSAppointment_CORouteTOMaster.Data._dataHasDNMaster, function (i, v) {
                                if (v.CreateSortOrder == sort) {
                                    item.TotalTon = v.Ton;
                                }
                            });
                            _OPSAppointment_CORouteTOMaster.Data._dataHasDNSort[sort] = { 'CreateSortOrder': sort, 'TypeID': 1, 'VehicleID': item.VehicleID, 'VehicleNo': item.VehicleNo, 'RomoocID': item.RomoocID, 'RomoocNo': item.RomoocNo, 'DriverName1': '', 'DriverTel1': '', 'ETD': item.ETD, 'VendorOfVehicleID': item.VendorOfVehicleID, 'VendorVehicleName': item.VendorVehicleName, 'ID': 0, "IsChange": true, 'TotalTon': item.TotalTon };
                        }
                        var drivername = item.DriverName1 != null ? item.DriverName1 : '';
                        var phone = item.DriverTel1 != null ? item.DriverTel1 : '';
                        var VehicleID = item.VehicleID != null ? item.VehicleID : '';
                        var RomoocID = item.RomoocID != null ? item.RomoocID : '';
                        if (Common.HasValue(item) && item.TypeID > 1) {
                            $(this).html('<input type="checkbox" class="chkChooseVehicle"/> Chuyến ' + sort + ' - KL: ' + Common.Number.ToNumber1(item.TotalTon) + ' - ' +
                                  item.VendorVehicleName + ' - ' + item.VehicleNo + ' - ' + item.RomoocNo + ' - ' + drivername + ' - ' + phone + ' - ' + Common.Date.FromJsonDMYHM(item.ETD) + '</a>');
                        }
                        else if (Common.HasValue(item) && item.TypeID == 1) {
                            $(this).html('<input type="checkbox" class="chkChooseVehicle"/> Chuyến ' + sort + ' - KL: ' + Common.Number.ToNumber1(item.TotalTon) + ' - ' +
                                 '<a href="" class="btnHasDNGroup">' + item.VendorVehicleName + ' - ' + item.VehicleNo + ' - ' + item.RomoocNo + ' - ' + drivername + ' - ' + phone + ' - ' + Common.Date.FromJsonDMYHM(item.ETD) + '</a>' +
                                 '<input style="display:none;width:100px" focus-k-combobox class="cboHasDNVendor cus-combobox" placeholder="Nhà vận tải" value="' + item.VendorOfVehicleID + '" /><span style="display:none" class="lblHasDNSplit"> - </span>' +
                                 '<input style="display:none;width:100px" focus-k-combobox class="cboHasDNTractor cus-combobox" placeholder="Đầu kéo" value="' + VehicleID + '" /><span style="display:none" class="lblHasDNSplit"> - </span>' +
                                  '<input style="display:none;width:100px" focus-k-combobox class="cboHasDNRomooc cus-combobox" placeholder="Romooc" value="' + RomoocID + '" /><span style="display:none" class="lblHasDNSplit"> - </span>' +
                                 '<input style="display:none;width:100px" type="text" class="txtHasDNDriverName k-textbox" placeholder="Nhập tài xế" value="' + drivername + '" /><span style="display:none" class="lblHasDNSplit"> - </span>' +
                                 '<input style="display:none;width:100px" type="text" class="txtHasDNTelephone k-textbox" placeholder="Nhập điện thoại" value="' + phone + '"/><span style="display:none" class="lblHasDNSplit"> - </span>' +
                                 '<input style="display:none;width:160px" class="txtCreateDateTime" value="' + Common.Date.FromJsonDMYHM(item.ETD) + '"/>');
                        }
                        else {
                            $(this).html('Chuyến ' + sort + ' - KL: ' + Common.Number.ToNumber1(item.TotalTon) + ' - ' +
                                 '<a href="" class="btnHasDNGroup">' + item.VendorVehicleName + ' - ' + item.VehicleNo + ' - ' + item.RomoocNo + ' - ' + drivername + ' - ' + phone + ' - ' + Common.Date.FromJsonDMYHM(item.ETD) + '</a>' +
                                 '<input style="display:none;width:100px" focus-k-combobox class="cboHasDNVendor cus-combobox" placeholder="Nhà vận tải" value="' + item.VendorOfVehicleID + '" /><span style="display:none" class="lblHasDNSplit"> - </span>' +
                                 '<input style="display:none;width:100px" focus-k-combobox class="cboHasDNTractor cus-combobox" placeholder="Đầu kéo" value="' + VehicleID + '" /><span style="display:none" class="lblHasDNSplit"> - </span>' +
                                  '<input style="display:none;width:100px" focus-k-combobox class="cboHasDNRomooc cus-combobox" placeholder="Romooc" value="' + RomoocID + '" /><span style="display:none" class="lblHasDNSplit"> - </span>' +
                                 '<input style="display:none;width:100px" type="text" class="txtHasDNDriverName k-textbox" placeholder="Nhập tài xế" value="' + drivername + '" /><span style="display:none" class="lblHasDNSplit"> - </span>' +
                                 '<input style="display:none;width:100px" type="text" class="txtHasDNTelephone k-textbox" placeholder="Nhập điện thoại" value="' + phone + '"/><span style="display:none" class="lblHasDNSplit"> - </span>' +
                                 '<input style="display:none;width:160px" class="txtCreateDateTime" value="' + Common.Date.FromJsonDMYHM(item.ETD) + '"/>');
                        }

                    }
                });

                this.element.find('.HasDNGridGroup .btnHasDNGroup').click(function (e) {
                    e.preventDefault();
                    var tr = $(this).closest('tr');
                    var sort = parseInt($(tr).find('.HasDNGridGroup').attr('tabid'));
                    $scope.GroupDN_Click(this, sort);
                });

                this.element.find('.HasDNGridGroup .txtHasDNDriverName').change(function (e) {
                    e.preventDefault();
                    var tr = $(this).closest('tr');
                    var sort = parseInt($(tr).find('.HasDNGridGroup').attr('tabid'));
                    _OPSAppointment_CORouteTOMaster.Data._dataHasDNSort[sort].DriverName1 = this.value;
                    _OPSAppointment_CORouteTOMaster.Data._dataHasDNSort[sort].IsChange = true;
                    $timeout(function () {
                        $scope.IsShowSave = true;
                    }, 10);

                });

                this.element.find('.HasDNGridGroup .txtHasDNTelephone').change(function (e) {
                    e.preventDefault();
                    var tr = $(this).closest('tr');
                    var sort = parseInt($(tr).find('.HasDNGridGroup').attr('tabid'));
                    _OPSAppointment_CORouteTOMaster.Data._dataHasDNSort[sort].DriverTel1 = this.value;
                    _OPSAppointment_CORouteTOMaster.Data._dataHasDNSort[sort].IsChange = true;
                    $timeout(function () {
                        $scope.IsShowSave = true;
                    }, 10);
                });

                this.element.find('.HasDNGridGroup .chkChooseVehicle').click(function (e) {
                    $scope.ReloadButton();
                });

            }
        }
    };

    $scope.gridNoDNOptions = {
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                fields:
                {
                    ID: { type: 'number' },
                    ETD: { type: 'date' },
                    ETA: { type: 'date' },
                }
            },
        }),
        height: '99%', groupable: false, pageable: false, columnMenu: false, sortable: true, resizable: true, reorderable: true, sortable: { mode: 'row' },
        columns: [
            {
                field: 'CreateSortOrder', width: '50px', template: '<form class="cus-form-enter" ng-submit="SortEnter_Click($event)"><input kendo-numeric-text-box class="txtCreateSortOrder" value="#=CreateSortOrder>0?CreateSortOrder:0#" data-k-min="0" k-options="numericSortOrderOptions" style="width:100%" /></form>', headerTemplate: '<a class="k-button" href="/"><i class="fa"></i></a>', sortable: false, filterable: false, menu: false,
                sortorder: 0, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'ContainerNo', width: '80px', title: 'SO Con', sortable: true, filterable: { cell: { operator: 'contains', showOperators: false } },
                sortorder: 1, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'OrderCode', width: '90px', title: 'Mã ĐH', sortable: true, filterable: { cell: { operator: 'contains', showOperators: false } },
                sortorder: 2, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'CustomerName', width: '90px', title: 'KH', sortable: true, filterable: { cell: { operator: 'contains', showOperators: false } },
                sortorder: 3, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'PackingName', width: '90px', title: 'Loại Con', sortable: true, filterable: { cell: { operator: 'contains', showOperators: false } },
                sortorder: 4, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'StatusOfCOContainerName', width: '90px', title: 'Tình trạng Con', sortable: true, filterable: { cell: { operator: 'contains', showOperators: false } },
                sortorder: 5, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'Ton', width: '80px', title: 'Tấn', sortable: true, filterable: { cell: { operator: 'eq', showOperators: false } },
                sortorder: 6, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'SealNo1', width: '90px', title: 'SealNo1', sortable: true, filterable: { cell: { operator: 'contains', showOperators: false } },
                sortorder: 7, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'SealNo2', width: '90px', title: 'SealNo2', sortable: true, filterable: { cell: { operator: 'contains', showOperators: false } },
                sortorder: 8, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'Note', width: '90px', title: 'Ghi chú', sortable: true, filterable: { cell: { operator: 'contains', showOperators: false } },
                sortorder: 9, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'ServiceOfOrderName', width: '90px', title: 'Loại vận chuyển', sortable: true, filterable: { cell: { operator: 'contains', showOperators: false } },
                sortorder: 10, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'ETD', width: '100px', title: 'ETD',
                template: "#=ETD != null ? Common.Date.FromJsonDMY(ETD) : ''#", filterable: {
                    cell: {
                        template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); },
                        operator: 'equal', showOperators: false
                    }
                },
                sortorder: 11, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'ETA', width: '100px', title: 'ETA',
                template: "#=ETA != null ? Common.Date.FromJsonDMY(ETA) : ''#",
                filterable: {
                    cell: {
                        template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); },
                        operator: 'equal', showOperators: false
                    }
                },
                sortorder: 12, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'LocationFromName', width: '100px', title: 'LocationFromName', sortable: true, filterable: { cell: { operator: 'contains', showOperators: false } },
                sortorder: 13, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'LocationFromAddress', width: '100px', title: 'Địa chỉ', sortable: true, filterable: { cell: { operator: 'contains', showOperators: false } },
                sortorder: 14, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'LocationFromProvinceName', width: '100px', title: 'Tỉnh', sortable: true, filterable: { cell: { operator: 'contains', showOperators: false } },
                sortorder: 15, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'LocationFromDistrictName', width: '100px', title: 'Quận / Huyện', sortable: true, filterable: { cell: { operator: 'contains', showOperators: false } },
                sortorder: 16, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'LocationToName', width: '100px', title: 'LocationToName', sortable: true, filterable: { cell: { operator: 'contains', showOperators: false } },
                sortorder: 17, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'LocationToDistrictName', width: '100px', title: 'Quận / Huyện', sortable: true, filterable: { cell: { operator: 'contains', showOperators: false } },
                sortorder: 18, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'LocationToAddress', width: '200px', title: 'Địa chỉ', sortable: true, filterable: { cell: { operator: 'contains', showOperators: false } },
                sortorder: 19, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'LocationToProvinceName', width: '100px', title: 'Tỉnh / Thành', sortable: true, filterable: { cell: { operator: 'contains', showOperators: false } },
                sortorder: 20, configurable: true, isfunctionalHidden: false
            },
            { title: ' ', filterable: false, menu: false, sortable: false, sortorder: 100, configurable: false, isfunctionalHidden: false }
        ],
        toolbar: kendo.template($('#OPSAppointment_CORouteTOMaster_gridToolbar').html()),
        filterable: { mode: 'row' },
        dataBound: function () {
            Common.Log("GridDataBound");

            var grid = this;
            var h = Common.Cookie.Get("Scroll");
            grid.wrapper.find('.k-grid-content').scrollTop(h);
        },
    };

    $scope.OPSAppointment_CORouteTOMaster_VendorRate_gridOptions = {
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'SortOrder',
                fields:
                    {
                        SortOrder: { type: 'number', editable: false },
                        VendorID: { type: 'number' },
                        IsManual: { type: 'boolean' },
                        Debit: { type: 'number' },
                        RateTime: { type: 'number' },
                    }
            },
        }),
        height: '100%', groupable: false, pageable: false, sortable: true, columnMenu: false, filterable: false, resizable: true,
        columns: [
            { field: 'SortOrder', width: '50px', title: 'T.tự' },
            { field: 'VendorID', width: '300px', title: 'Nhà vận tải', template: '<input focus-k-combobox class="cus-combobox cboVendor" kendo-combo-box k-options="cboVendorOptions" data-bind="value:VendorID" ng-model="dataItem.VendorID" k-data-source="dataItem.lstVendor"/>' },
            { field: 'RateTime', width: '90px', title: 'Thời gian', template: '<input type="number" class="cus-number RateTimeVender" min="0" max="100000" style="width:100%"/>', filterable: false, menu: false, sortable: true },
            { field: 'IsManual', width: '90px', title: 'Nhập giá', template: '<input style="text-align:center" class="chkIsManual" type="checkbox" #= IsManual ? checked="checked" : "" #/>', filterable: false, menu: false, sortable: true },
            { field: 'Debit', width: '120px', title: 'Giá', template: '<input class="txtDebit cus-number" value="#=Debit#" style="width:100%"/>', filterable: false, menu: false, sortable: true },
        ],
        toolbar: kendo.template($('#OPSAppointment_CORouteTOMaster_RateTime').html()),
        dataBound: function (e) {
            if (Common.HasValue($scope.OPSAppointment_CORouteTOMaster_VendorRate_grid) && Common.HasValue($scope.OPSAppointment_CORouteTOMaster_VendorRate_grid.tbody)) {
                Common.Log('WinRateGridVENDataBound');

                $($scope.OPSAppointment_CORouteTOMaster_VendorRate_grid.element).find('.txtDebit').kendoNumericTextBox({
                    format: '#,##0', spinners: false, culture: 'en-US', min: 0, max: 100000000, step: 1000, decimals: 0,
                    value: 0
                });

                $($scope.OPSAppointment_CORouteTOMaster_VendorRate_grid.element).find('.RateTimeVender').kendoNumericTextBox({
                    format: '#,##0', spinners: false, culture: 'en-US', min: 0, max: 100000000, step: 1000, decimals: 0,
                    value: 0
                });

                $($scope.OPSAppointment_CORouteTOMaster_VendorRate_grid.element).find('.chkIsManual').change(function () {
                    var tr = $(this).closest('tr');
                    var txtDebit = $($(tr).find('input.txtDebit.cus-number')[1]).data('kendoNumericTextBox');
                    txtDebit.enable($(this).prop('checked'));
                });

                var lst = $scope.OPSAppointment_CORouteTOMaster_VendorRate_grid.tbody.find('tr');
                $.each(lst, function (itr, tr) {
                    var item = $scope.OPSAppointment_CORouteTOMaster_VendorRate_grid.dataItem(tr);

                    var txtDebit = $($(tr).find('input.txtDebit.cus-number')[1]).data('kendoNumericTextBox');
                    var chkIsManual = $($(this).find('.chkIsManual')[0]);
                    if (item.IsManual == true)
                        txtDebit.enable(true);
                    else
                        txtDebit.enable(false);
                    chkIsManual.prop('checked', item.IsManual);
                });
            }
        }
    }

    // khach hang
    $scope.mltCustomerOptions = {
        autoBind: false,
        valuePrimitive: true, placeholder: 'Chọn khách hàng',
        dataTextField: 'CustomerName',
        dataValueField: 'ID',
        filter: 'contains',
        suggest: true,
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    CustomerName: { type: 'string' }
                }
            }
        }),
        change: function (e) {
            $scope.Search.ListCustomerID = this.value();
        }
    };

    // Load danh sách khách hàng
    Common.Services.Call($http, {
        url: Common.Services.url.OPS,
        method: _OPSAppointment_CORouteTOMaster.URL.CustomerList,
        data: {},
        success: function (res) {
            if (Common.HasValue(res)) {
                $timeout(function () {
                    $scope.mltCustomerOptions.dataSource.data(res);
                }, 1);
            }
        }
    });

    // Load danh sách phân tài
    Common.Services.Call($http, {
        url: Common.Services.url.OPS,
        method: _OPSAppointment_CORouteTOMaster.URL.FLMPlaning,
        success: function (res) {
            _OPSAppointment_CORouteTOMaster.Data.FLMPlaning = res;
        }
    });

    //search
    $scope.Search_Click = function ($event) {
        $event.preventDefault();
        if ($scope.IsShowSave) {
            $rootScope.Message({
                Type: Common.Message.Type.Confirm,
                Msg: 'Bạn muốn lưu các chuyến đã lập ?',
                Ok: function () {
                    $scope.Save_Click($event, false);
                },
                Close: function () {
                    $scope.LoadData();
                }
            });
        }
        else
            $scope.LoadData();
    }

    $scope.cboVendorOptions = {
        index: 0, autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true,
        dataTextField: 'Code', dataValueField: 'ID',
    };

    $scope.TenderClose_Click = function ($event, win) {
        Common.Log('TenderClose_Click');
        $event.preventDefault();

        win.close();
    };

    $scope.LoadData = function () {
        Common.Log('LoadData');
        $rootScope.IsLoading = true;
        var lstFilters = [];
        var TypeOfViewID = parseInt($scope.Search.TypeOfViewID);
        switch (TypeOfViewID) {
            case 1:
                lstFilters.push(Common.Request.FilterParamWithOr('TypeID', Common.Request.FilterType.Equal, 1));
                break;
            case 2:
                lstFilters.push(Common.Request.FilterParamWithOr('TypeID', Common.Request.FilterType.Equal, 2));
                break;
            case 3:
                lstFilters.push(Common.Request.FilterParamWithOr('TypeID', Common.Request.FilterType.Equal, 3));
                break;
            case 4:
                lstFilters.push(Common.Request.FilterParamWithOr('TypeID', Common.Request.FilterType.Equal, 1));
                lstFilters.push(Common.Request.FilterParamWithOr('TypeID', Common.Request.FilterType.Equal, 2));
                lstFilters.push(Common.Request.FilterParamWithOr('TypeID', Common.Request.FilterType.Equal, 3));
                break;
        }
        var param = Common.Request.Create({
            Sorts: [], Filters: lstFilters
        });
        var flag = 0;
        IndexData = [];
        var DataFlag = [];
        _OPSAppointment_CORouteTOMaster.Data._dataHasDNSort = [];
        _OPSAppointment_CORouteTOMaster.Data._dataHasDN = [];

        Common.Services.Call($http, {
            url: Common.Services.url.OPS,
            method: _OPSAppointment_CORouteTOMaster.URL.ContainerHasMaster_List,
            data: { request: param, lstCustomerID: $scope.Search.ListCustomerID, DateFrom: $scope.Search.DateFrom, DateTo: $scope.Search.DateTo },
            success: function (res) {
                Common.Services.Error(res, function (res) {
                    DataFlag = res;
                    Common.Services.Call($http, {
                        url: Common.Services.url.OPS,
                        method: _OPSAppointment_CORouteTOMaster.URL.Master_List,
                        data: { request: param, lstCustomerID: $scope.Search.ListCustomerID, DateFrom: $scope.Search.DateFrom, DateTo: $scope.Search.DateTo },
                        success: function (res) {
                            Common.Services.Error(res, function (res) {
                                _OPSAppointment_CORouteTOMaster.Data._dataHasDNSort[0] = { 'CreateSortOrder': 0, 'VehicleID': '', 'TypeID': 1, 'VehicleNo': "[Chờ nhập xe]", 'RomoocID': '', 'RomoocNo': "[Chờ nhập romooc]", 'DriverName1': '', DriverTel1: '', ETD: Common.Date.FromJson(Date()), 'VendorOfVehicleID': -1, 'VendorVehicleName': "Xe nhà", 'ID': 0, 'IsChange': false, 'TotalTon': 0 };
                                if (Common.HasValue(res)) {
                                    _OPSAppointment_CORouteTOMaster.Data._dataHasDNMaster = [];
                                    var index = 1;
                                    $.each(res, function (i, v) {
                                        v.CreateSortOrder = index;
                                        v.TotalTon = 0;
                                        $.each(DataFlag, function (i, m) {
                                            if (v.ID == m.COTOMasterID) {
                                                v.TotalTon = v.TotalTon + m.Ton;
                                                v.TypeID = m.TypeID;
                                                v.IsChange = false;
                                                m.CreateSortOrder = index;
                                                _OPSAppointment_CORouteTOMaster.Data._dataHasDNMaster.push(m);
                                            }
                                        });
                                        _OPSAppointment_CORouteTOMaster.Data._dataHasDNSort[index] = v;
                                        index++;
                                    });
                                }
                                $scope.gridNoDNMaster.dataSource.data(_OPSAppointment_CORouteTOMaster.Data._dataHasDNMaster);
                                flag++;
                                if (flag == 1) {
                                    $scope.ReloadSortMaster();
                                }
                            });

                        }
                    });
                    var paramCo = Common.Request.Create({
                        Sorts: [], Filters: []
                    });
                    Common.Services.Call($http, {
                        url: Common.Services.url.OPS,
                        method: _OPSAppointment_CORouteTOMaster.URL.Container_List,
                        data: { request: paramCo, lstCustomerID: $scope.Search.ListCustomerID, DateFrom: $scope.Search.DateFrom, DateTo: $scope.Search.DateTo },
                        success: function (res) {
                            $scope.totalTonNoMaster = 0;
                            _OPSAppointment_CORouteTOMaster.Data._dataHasDN = res;
                            $scope.gridNoDN.dataSource.data(_OPSAppointment_CORouteTOMaster.Data._dataHasDN);
                            $.each(_OPSAppointment_CORouteTOMaster.Data._dataHasDN, function (i, v) {
                                if (v.CreateSortOrder <= 0) {
                                    $scope.totalTonNoMaster = $scope.totalTonNoMaster + v.Ton;
                                }
                            });
                            $scope.TotalTonNo($scope.totalTonNoMaster);
                            $rootScope.IsLoading = false;
                        }
                    });

                });
            }
        });

        $timeout(function () {
            $scope.IsShowSave = false;
            $scope.IsShowApproved = false;
            $scope.IsShowUnApproved = false;
            $scope.IsShowTendered = false;
            $scope.IsShowDelete = false;
        }, 10);
    };

    $scope.ddlTypeOfViewOptions = {
        autoBind: true, index: 0,
        valuePrimitive: true,
        dataTextField: 'TypeOfViewName',
        dataValueField: 'ID',
        dataSource: Common.DataSource.Local({
            data: _OPSAppointment_CORouteTOMaster.Data._dataTypeOfView,
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    TypeOfViewName: { type: 'string' }
                }
            }
        }),
    };

    $scope.numericSortOrderOptions = {
        format: '#', spinners: false, culture: 'en-US', step: 1, decimals: 0, min: 0,
        change: function (e) {
            var h = $scope.gridNoDN.wrapper.find('.k-grid-content').scrollTop();
            Common.Cookie.Set("Scroll", h);
            var sort = e.sender.element.val() == '' ? 0 : parseInt(e.sender.element.val());
            var TypeID = 1;
            if (_OPSAppointment_CORouteTOMaster.Data._dataHasDNSort[sort] != null) {
                TypeID = _OPSAppointment_CORouteTOMaster.Data._dataHasDNSort[sort].TypeID;
            }
            if (sort > 0) {
                if (TypeID < 2) {
                    var tr = $(e.sender.element).closest('tr');
                    var dataItem = $scope.gridNoDN.dataItem(tr);
                    // Add vào grid chuyến, remove khỏi grid đơn hàng
                    dataItem.CreateSortOrder = sort;
                    // Tính lại khối lượng
                    $scope.totalTonNoMaster = $scope.totalTonNoMaster - dataItem.Ton;
                    $scope.TotalTonNo($scope.totalTonNoMaster);
                    // Add vào grid chuyến
                    _OPSAppointment_CORouteTOMaster.Data._dataHasDNMaster = $.extend(true, [], $scope.gridNoDNMaster.dataSource.data());
                    _OPSAppointment_CORouteTOMaster.Data._dataHasDNMaster.push(dataItem);
                    $scope.TotalTon();
                    // remove khỏi grid đơn hàng
                    $scope.gridNoDN.removeRow(tr);
                    $timeout(function () {
                        $scope.IsShowSave = true;
                        $scope.gridNoDNMaster.dataSource.data(_OPSAppointment_CORouteTOMaster.Data._dataHasDNMaster);
                        $scope.ReloadSortMaster();
                    }, 1);
                }
                else {
                    $rootScope.Message({
                        Msg: 'Chuyến ' + sort + ' đã giám sát!',
                        NotifyType: Common.Message.NotifyType.WARNING
                    });
                }
            }
        }
    };

    $scope.numericSortOrderMasterOptions = {
        format: '#', spinners: false, culture: 'en-US', step: 1, decimals: 0, min: 0,
        change: function (e) {
            var sort = e.sender.element.val() == '' ? 0 : parseInt(e.sender.element.val());
            var tr = $(e.sender.element).closest('tr');
            var dataItem = $scope.gridNoDNMaster.dataItem(tr);
            if (Common.HasValue(dataItem)) {
                // remove khỏi grid chuyến, add vào grid đơn hàng
                if (sort <= 0) {
                    dataItem.CreateSortOrder = 0;
                    $scope.totalTonNoMaster = $scope.totalTonNoMaster + dataItem.Ton;
                    $scope.TotalTonNo($scope.totalTonNoMaster);
                    // Add vào grid đơn hàng
                    _OPSAppointment_CORouteTOMaster.Data._dataHasDNMaster = [];
                    _OPSAppointment_CORouteTOMaster.Data._dataHasDNMaster = $scope.gridNoDNMaster.dataSource.data();
                    $scope.TotalTon();
                    _OPSAppointment_CORouteTOMaster.Data._dataHasDN = $.extend(true, [], $scope.gridNoDN.dataSource.data());
                    _OPSAppointment_CORouteTOMaster.Data._dataHasDN.push(dataItem);
                    $scope.gridNoDN.dataSource.data(_OPSAppointment_CORouteTOMaster.Data._dataHasDN);
                    // remove khỏi grid chuyến
                    $scope.gridNoDNMaster.removeRow(tr);
                } else {
                    _OPSAppointment_CORouteTOMaster.Data._dataHasDNMaster = [];
                    dataItem.CreateSortOrder = sort;
                    _OPSAppointment_CORouteTOMaster.Data._dataHasDNMaster = $scope.gridNoDNMaster.dataSource.data();
                    $scope.TotalTon();
                    $scope.gridNoDNMaster.dataSource.data($scope.gridNoDNMaster.dataSource.data());
                }
                $timeout(function () {
                    $scope.ReloadSortMaster();
                    $scope.IsShowSave = true;
                }, 1);
            }
        }
    };

    // Load danh sách Vendor
    Common.Services.Call($http, {
        url: Common.Services.url.OPS,
        method: _OPSAppointment_CORouteTOMaster.URL.VehicleListVendor,
        data: {},
        success: function (res) {
            if (Common.HasValue(res)) {
                $timeout(function () {
                    _OPSAppointment_CORouteTOMaster.Data._dataHasDNVendor = res;
                }, 1);
            }
        }
    });

    $scope.Expand_Click = function ($event, grid) {
        Common.Log('Expand_Click');
        if (Common.HasValue($event))
            $event.preventDefault();
        $scope.IsExpand = !$scope.IsExpand;

        // Expand tất cả group
        if ($scope.IsExpand) {
            $(grid.element).find("a.k-icon.k-i-expand").trigger("click");
            $($event.currentTarget).find("i").removeClass("fa-plus");
            $($event.currentTarget).find("i").addClass("fa-minus");
        } else {
            // Collapse tất cả group
            $(grid.element).find("a.k-icon.k-i-collapse").trigger("click");
            $($event.currentTarget).find("i").removeClass("fa-minus");
            $($event.currentTarget).find("i").addClass("fa-plus");
        }
    };

    // Load danh sách tài xế
    Common.Services.Call($http, {
        url: Common.Services.url.OPS,
        method: _OPSAppointment_CORouteTOMaster.URL.VehicleListDriver,
        success: function (res) {
            Common.Services.Error(res, function (res) {
                _OPSAppointment_CORouteTOMaster.Data._dataDriver = [];
                $.each(res, function (i, v) {
                    _OPSAppointment_CORouteTOMaster.Data._dataDriver.push({ 'ID': v.ID, 'Text': v.LastName + ' ' + v.FirstName + ' (' + v.EmployeeCode + ')', 'DriverName': v.LastName + ' ' + v.FirstName, 'DriverTelNo': v.Cellphone });
                });
            });
        }
    });

    $scope.GroupDN_Click = function (item, sort) {
        Common.Log('GroupDN_Click');

        $(item).hide();
        var dataVehicle = [];
        var group = $(item).closest('.HasDNGridGroup');
        var tr = $(item).closest('tr');
        $(tr).find('.lblHasDNSplit,input').show();
        $(tr).find('.txtHasDNMaxWeight,.txtHasDNMaxCBM').hide();
        $(tr).find('.lblHasDNSplit.vehicle').hide();
        tr.find('input.cboHasDNVendor').kendoComboBox({
            index: 0, autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true,
            dataTextField: 'Code', dataValueField: 'ID',
            dataSource: Common.DataSource.Local({
                data: _OPSAppointment_CORouteTOMaster.Data._dataHasDNVendor,
                model: {
                    id: 'ID',
                    fields: {
                        ID: { type: 'number' },
                        CustomerName: { type: 'string' }
                    }
                },
            }),
            change: function () {
                var ID = this.value();
                Common.Log(ID);
                var text = this.text();

                if ($.isNumeric(ID) && ID > -1) {
                    ID = ID;
                } else {
                    ID = null;
                }
                _OPSAppointment_CORouteTOMaster.Data._dataHasDNSort[sort].VendorVehicleName = text;
                _OPSAppointment_CORouteTOMaster.Data._dataHasDNSort[sort].VendorOfVehicleID = ID;
                _OPSAppointment_CORouteTOMaster.Data._dataHasDNSort[sort].IsChange = true;
                _OPSAppointment_CORouteTOMaster.Data._dataHasDNRomooc = [];
                _OPSAppointment_CORouteTOMaster.Data._dataHasDNTractor = [];
                var txtDriver = $(tr).find("input.txtHasDNDriverName");
                var txtPhone = $(tr).find("input.txtHasDNTelephone");
                var txtDate = $(tr).find("input.txtCreateDateTime");
                _OPSAppointment_CORouteTOMaster.Data._dataHasDNSort[sort].DriverName1 = "";
                _OPSAppointment_CORouteTOMaster.Data._dataHasDNSort[sort].DriverTel1 = "";
                _OPSAppointment_CORouteTOMaster.Data._dataHasDNSort[sort].VehicleID = "";
                _OPSAppointment_CORouteTOMaster.Data._dataHasDNSort[sort].RomoocID = "";
                _OPSAppointment_CORouteTOMaster.Data._dataHasDNSort[sort].ETD = new Date();
                txtDriver.val("");
                txtPhone.val("");
                txtDate.val(Common.Date.FromJsonDMYHM(new Date()));
                Common.Services.Call($http, {
                    url: Common.Services.url.OPS,
                    method: _OPSAppointment_CORouteTOMaster.URL.VehicleListTractor,
                    data: { vendorid: ID },
                    success: function (res) {
                        Tractor.dataSource.data(res.Data);
                        $timeout(function () {
                            Tractor.select(0);
                        }, 100)
                        dataVehicle = res.Data;
                    }
                });
                Common.Services.Call($http, {
                    url: Common.Services.url.OPS,
                    method: _OPSAppointment_CORouteTOMaster.URL.VehicleListRomooc,
                    data: { vendorid: ID },
                    success: function (res) {
                        Romooc.dataSource.data(res.Data);
                        Romooc.select(-1);
                    }
                });
                $timeout(function () {
                    $scope.IsShowSave = true;
                }, 10);
            }

        });
        tr.find('input.txtCreateDateTime').kendoDateTimePicker({
            format: Common.Date.Format.DMYHM,
            timeFormat: Common.Date.Format.HM,
            change: function (e) {
                var EditDate = new Date(e.sender.value());
                _OPSAppointment_CORouteTOMaster.Data._dataHasDNSort[sort].ETD = e.sender.value();
                _OPSAppointment_CORouteTOMaster.Data._dataHasDNSort[sort].IsChange = true;

                try {
                    var txtHasDNDriverName = $(span).find('input.txtHasDNDriverName').data("kendoAutoComplete");
                    var objD = $scope.FindDriver(_OPSAppointment_CORouteTOMaster.Data._dataHasDNSort[sort]);
                    if (objD.ID > 1) {
                        txtHasDNDriverName.value(objD.Name);
                        txtHasDNDriverName.trigger('change');
                    }
                } catch (e) {
                }

                $timeout(function () {
                    $scope.IsShowSave = true;
                }, 10);
            }
        });

        //dua du lieu vao 2 combobox tractor va romooc
        if (_OPSAppointment_CORouteTOMaster.Data._dataHasDNSort[sort].VendorOfVehicleID < 0) {
            _OPSAppointment_CORouteTOMaster.Data._dataHasDNSort[sort].VendorOfVehicleID = null;
        }
        if (_OPSAppointment_CORouteTOMaster.Data._dataHasDNSort[sort].VendorOfVehicleID > 0 || _OPSAppointment_CORouteTOMaster.Data._dataHasDNSort[sort].VendorOfVehicleID == null) {
            Common.Services.Call($http, {
                url: Common.Services.url.OPS,
                method: _OPSAppointment_CORouteTOMaster.URL.VehicleListTractor,
                data: { vendorid: _OPSAppointment_CORouteTOMaster.Data._dataHasDNSort[sort].VendorOfVehicleID },
                success: function (res) {
                    Tractor.dataSource.data(res.Data);
                    dataVehicle = res.Data;
                }
            });

            Common.Services.Call($http, {
                url: Common.Services.url.OPS,
                method: _OPSAppointment_CORouteTOMaster.URL.VehicleListRomooc,
                data: { vendorid: _OPSAppointment_CORouteTOMaster.Data._dataHasDNSort[sort].VendorOfVehicleID },
                success: function (res) {
                    Romooc.dataSource.data(res.Data);
                }
            });
        }
        var Tractor = tr.find('input.cboHasDNTractor').kendoComboBox({
            index: 0, autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true,
            dataTextField: 'RegNo', dataValueField: 'ID',
            dataSource: Common.DataSource.Local({
                    data: [],
                    model: {
                        id: 'ID',
                        fields: {
                            ID: { type: 'number' },
                            RegNo: { type: 'string' }
                        }
                    },
                }),
            change: function () {
                var ID = this.value();
                var text = this.text();
                                
                _OPSAppointment_CORouteTOMaster.Data._dataHasDNSort[sort].IsChange = true;
                _OPSAppointment_CORouteTOMaster.Data._dataHasDNSort[sort].VehicleNo = text;
                _OPSAppointment_CORouteTOMaster.Data._dataHasDNSort[sort].VehicleID = ID;

                try {
                    var objD = $scope.FindDriver(_OPSAppointment_CORouteTOMaster.Data._dataHasDNSort[sort]);
                    if (objD.ID < 1) {
                        $.each(dataVehicle, function (i, v) {
                            if (v.ID == ID) {
                                tr.find("input.txtHasDNDriverName").val(v.DriverName);
                                tr.find("input.txtHasDNTelephone").val(v.Cellphone);
                            }
                        });
                    } else {
                        tr.find("input.txtHasDNDriverName").val(objD.Name);
                        tr.find("input.txtHasDNTelephone").val(objD.Tel);
                    }
                } catch (e) {

                }              

                $timeout(function () {
                    $scope.IsShowSave = true;
                }, 10);
            }

        }).data('kendoComboBox');

        var Romooc = tr.find('input.cboHasDNRomooc').kendoComboBox({
            index: 0, autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true,
            dataTextField: 'RegNo', dataValueField: 'ID',
            dataSource: Common.DataSource.Local({
                data: [],
                model: {
                    id: 'ID',
                    fields: {
                        ID: { type: 'number' },
                        RegNo: { type: 'string' }
                    }
                },
            }),
            change: function () {
                var ID = this.value();
                var text = this.text();
                _OPSAppointment_CORouteTOMaster.Data._dataHasDNSort[sort].IsChange = true;
                _OPSAppointment_CORouteTOMaster.Data._dataHasDNSort[sort].RomoocNo = text;
                _OPSAppointment_CORouteTOMaster.Data._dataHasDNSort[sort].RomoocID = ID;
                $timeout(function () {
                    $scope.IsShowSave = true;
                }, 10);
            },

        }).data('kendoComboBox');

        tr.find('input.txtHasDNDriverName').kendoAutoComplete({
            autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true, placeholder: "Nhập tài xế",
            dataTextField: 'DriverName', dataValueField: 'ID',
            dataSource: Common.DataSource.Local({
                data: _OPSAppointment_CORouteTOMaster.Data._dataDriver,
                model: {
                    id: 'ID',
                    fields: {
                        ID: { type: 'number' },
                        DriverName: { type: 'string' }
                    }
                },
            }),
            change: function (e) {
                var ID = -1;
                var span = $(this.element).closest('span.HasDNGridGroup');
                sort = $(span).attr('tabid');
                var item = _OPSAppointment_CORouteTOMaster.Data._dataHasDNSort[sort];
                item.DriverName1 = this.value();
                if (Common.HasValue(this.dataItem(e.item))) {
                    ID = this.dataItem(e.item).ID;
                }
                item.IsChange = true;
                $timeout(function () {
                    $scope.IsShowSave = true;
                }, 10);
                if (ID > 0) {
                    if (item.VehicleID > 0) {
                        var ETD = '';
                        var ETA = '';
                        $.each(_OPSAppointment_CORouteTOMaster.Data._dataHasDNSort, function (i, v) {
                            if (Common.HasValue(v)) {
                                $.each($scope.gridNoDNMaster.dataSource.data(), function (i, m) {
                                    if (v.CreateSortOrder > 0 && v.CreateSortOrder == m.CreateSortOrder) {
                                        ETD = m.ETD;
                                        ETA = m.ETA;
                                    }
                                });
                            }
                        });

                        Common.Services.Call($http, {
                            url: Common.Services.url.OPS,
                            method: _OPSAppointment_CORouteTOMaster.URL.Master_CheckDriver,
                            data: { vehicleID: item.VehicleID, driverID: ID, etd: ETD, eta: ETA },
                            success: function (res) {
                                if (res == false) {
                                    $rootScope.Message({
                                        Msg: 'Tài xế chưa đc phân công.',
                                        NotifyType: Common.Message.NotifyType.WARNING
                                    });
                                }
                            }
                        });
                    }
                    else {
                        $rootScope.Message({
                            Msg: 'Chưa chọn xe.',
                            NotifyType: Common.Message.NotifyType.ERROR
                        });
                    }
                }
                //alert(item.CreateDriverName);
            }
        });
    };

    $scope.FindDriver = function (item) {
        var obj = {
            ID: -1,
            Name: '',
            Tel: ''
        };
        try {
            if (item.CreateVendorName == 'Xe nhà') {
                $.each(_OPSAppointment_CORouteTOMaster.Data.FLMPlaning, function (i, v) {
                    if (item.CreateVehicleCode == v.VehicleCode) {
                        if (Common.Date.FromJson(v.DateFrom) < item.CreateDateTime && Common.Date.FromJson(v.DateTo) > item.CreateDateTime) {
                            obj = {
                                ID: v.DriverID,
                                Name: v.DriverName,
                                Tel: v.DriverTel
                            }
                        }
                    }
                })
            }
        } catch (e) {
        }
        return obj;
    }

    $scope.Save_Click = function ($event, isConfirm) {
        Common.Log('Save_Click');
        $event.preventDefault();

        var dataMaster = [];
        var dataMasterSort = [];
        $.each(_OPSAppointment_CORouteTOMaster.Data._dataHasDNSort, function (i, v) {
            if (v != null && v.CreateSortOrder > 0) {
                dataMasterSort.push(v);
            }
        });
        var flag = true;
        var error = '';
        Common.Data.Each(dataMasterSort, function (v) {
            if (v.VehicleID == null || v.VehicleNo == '') {
                // || v.VehicleNo == '[Chờ nhập xe]' || v.RomoocID == null || v.RomoocNo == '' || v.RomoocNo == '[Chờ nhập romooc]'
                flag = false;
                error += ', ' + v.CreateSortOrder;
            }
        })
        if (flag) {
            Common.Services.Call($http, {
                url: Common.Services.url.OPS,
                method: _OPSAppointment_CORouteTOMaster.URL.Save,
                data: { dataMaster: dataMasterSort, dataContainer: $scope.gridNoDNMasterOptions.dataSource.data() },
                success: function (res) {
                    $scope.LoadData();
                }
            });
        } else {
            $rootScope.Message({ Msg: 'Các chuyến ' + error.substr(2) + ' chưa chọn xe hoặc romooc.', NotifyType: Common.Message.NotifyType.ERROR });
        }
    };

    $scope.ReloadButton = function () {
        var isUnApproved = true;
        var isApproved = true;
        var isTendered = true;
        var isHasApproved = false;
        var isHasUnApproved = false;
        var isHasTendered = false;
        if ($scope.IsShowSave == false) {
            $.each($scope.gridNoDNMaster.tbody.find('.HasDNGridGroup .chkChooseVehicle'), function (i, v) {
                if (this.checked) {
                    var group = $(v).closest('.HasDNGridGroup');
                    var sort = parseInt($(group).attr('tabid'));
                    var itemSort = _OPSAppointment_CORouteTOMaster.Data._dataHasDNSort[sort];
                    if (itemSort.TypeID == 1) {
                        isUnApproved = false;
                        isHasUnApproved = true;
                        if (itemSort.ID > 0)
                            isHasTendered = true;
                    } else {
                        if (itemSort.TypeID == 2) {
                            isHasApproved = true;
                            isApproved = false;
                            isTendered = false;
                        }
                    }
                }
            });

            if (isHasApproved && isUnApproved) {
                $timeout(function () {
                    $scope.IsShowUnApproved = true;
                }, 10);
            } else {
                $timeout(function () {
                    $scope.IsShowUnApproved = false;
                }, 10);
            }

            if (isHasUnApproved && isApproved) {
                $timeout(function () {
                    $scope.IsShowApproved = true;
                    $scope.IsShowDelete = true;
                }, 10);
            } else {
                $timeout(function () {
                    $scope.IsShowApproved = false;
                    $scope.IsShowDelete = false;
                }, 10);
            }

            if (isHasTendered && isTendered) {
                $timeout(function () {
                    $scope.IsShowTendered = true;
                }, 10);
            } else {
                $timeout(function () {
                    $scope.IsShowTendered = false;
                }, 10);
            }
        }
        else {
            //$rootScope.Message({ Msg: 'Lưu trước khi thực hiện các thao tác khác!', NotifyType: Common.Message.NotifyType.WARNING });
        }
    };

    $scope.UnApproved_Click = function ($event) {
        Common.Log('UnApproved_Click');
        $event.preventDefault();

        var data = [];
        $scope.gridNoDNMaster.tbody.find('.HasDNGridGroup').each(function () {
            var sort = parseInt($(this).attr('tabid'));
            var item = _OPSAppointment_CORouteTOMaster.Data._dataHasDNSort[sort];
            var tr = $(this).closest('tr');

            if (sort > 0 && item.ID > 0) {
                var chk = $(tr).find('.chkChooseVehicle');
                if (Common.HasValue(chk) && chk.prop('checked') == true)
                    data.push(item.ID);
            }
        });
        if (data.length > 0) {
            $rootScope.Message({
                Type: Common.Message.Type.Confirm,
                Msg: 'Bạn muốn hủy duyệt các chuyến đã chọn ?',
                Ok: function () {
                    $rootScope.IsLoading = true;
                    Common.Services.Call($http, {
                        url: Common.Services.url.OPS,
                        method: _OPSAppointment_CORouteTOMaster.URL.UnApproved,
                        data: { lst: data },
                        success: function (res) {
                            Common.Services.Error(res, function (res) {
                                $scope.LoadData();
                                $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                            });
                        }
                    });
                }
            });
        }
    };

    $scope.Delete_Click = function ($event) {
        Common.Log('Delete_Click');
        $event.preventDefault();

        var data = [];
        $scope.gridNoDNMaster.tbody.find('.HasDNGridGroup').each(function () {
            var sort = parseInt($(this).attr('tabid'));
            var item = _OPSAppointment_CORouteTOMaster.Data._dataHasDNSort[sort];
            var tr = $(this).closest('tr');

            if (sort > 0 && item.ID > 0) {
                var chk = $(tr).find('.chkChooseVehicle');
                if (Common.HasValue(chk) && chk.prop('checked') == true && item.TypeID < 2 && item.ID > 0)
                    data.push(item.ID);
            }
        });
        if (data.length > 0) {
            $rootScope.Message({
                Type: Common.Message.Type.Confirm,
                Msg: 'Bạn muốn xóa các chuyến đã chọn ?',
                Ok: function () {
                    $rootScope.IsLoading = true;
                    Common.Services.Call($http, {
                        url: Common.Services.url.OPS,
                        method: _OPSAppointment_CORouteTOMaster.URL.Delete,
                        data: { lst: data },
                        success: function (res) {
                            Common.Services.Error(res, function (res) {
                                $scope.LoadData();
                                $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                            });
                        }
                    });
                }
            });
        }
        else
            $rootScope.Message({ Msg: 'Chỉ xóa các chuyến chưa duyệt', NotifyType: Common.Message.NotifyType.ERROR });
    };

    $scope.Approved_Click = function ($event) {
        Common.Log('Approved_Click');
        $event.preventDefault();

        var data = []; var error = [];
        $scope.gridNoDNMaster.tbody.find('.HasDNGridGroup').each(function () {
            var sort = parseInt($(this).attr('tabid'));
            var item = _OPSAppointment_CORouteTOMaster.Data._dataHasDNSort[sort];
            var tr = $(this).closest('tr');

            if (sort > 0 && item.ID > 0) {
                var chk = $(tr).find('.chkChooseVehicle');
                if (Common.HasValue(chk) && chk.prop('checked') == true) {
                    if (item.VehicleNo == '' || item.VehicleNo == "[Chờ nhập xe]" || item.RomoocNo == '' || item.RomoocNo == '[Chờ nhập romooc]') {
                        error.push(item.CreateSortOrder);
                    }
                    data.push(item.ID);
                }
            }
        });

        if (data.length > 0) {
            if (error.length > 0) {
                $rootScope.Message({
                    Type: Common.Message.Type.Alert,
                    Msg: 'Chuyến "' + error.join(', ') + '" chưa chọn xe hoặc romooc.'
                });
            } else {
                $rootScope.Message({
                    Type: Common.Message.Type.Confirm,
                    Msg: 'Bạn muốn duyệt các chuyến đã chọn ?',
                    Ok: function () {
                        $rootScope.IsLoading = true;
                        $scope.Save_Click($event, false);
                        Common.Services.Call($http, {
                            url: Common.Services.url.OPS,
                            method: _OPSAppointment_CORouteTOMaster.URL.Tendered,
                            data: { lst: data },
                            success: function (res) {
                                Common.Services.Error(res, function (res) {
                                    $scope.LoadData();
                                    $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                                });
                            }
                        });
                    }
                });
            }
        }
    };

    $scope.TenderSend_Click = function ($event, win, grid) {
        Common.Log('TenderSend_Click');
        $event.preventDefault();

        var data = [];
        $scope.gridNoDNMaster.tbody.find('.chkChooseVehicle').each(function () {
            if (this.checked) {
                var group = $(this).closest('.HasDNGridGroup');
                var sort = parseInt($(group).attr('tabid'));
                var item = _OPSAppointment_CORouteTOMaster.Data._dataHasDNSort[sort];
                if (sort > 0 && item.ID > 0) {
                    data.push(item.ID);
                }
            }
        });
        if (data.length > 0) {
            $(grid.element).find('tbody tr').each(function () {
                var cboVendor = $($(this).find('input.cboVendor.cus-combobox')[1]).data('kendoComboBox');
                var chkIsManual = $($(this).find('.chkIsManual')[0]);
                var txtDebit = $($(this).find('input.txtDebit.cus-number')[1]).data('kendoNumericTextBox');
                var item = grid.dataItem(this);
                var RateTimeVender = $($(this).find('input.RateTimeVender.cus-number')[1]).data('kendoNumericTextBox');
                item.VendorID = cboVendor.value();
                item.IsManual = chkIsManual.prop('checked');
                item.Debit = txtDebit.value()
                item.RateTime = RateTimeVender.value();
            });
            $rootScope.Message({
                Type: Common.Message.Type.Confirm,
                Msg: 'Bạn muốn gửi Tender các chuyến đã chọn ?',
                Ok: function () {
                    $rootScope.IsLoading = true;
                    Common.Services.Call($http, {
                        url: Common.Services.url.OPS,
                        method: _OPSAppointment_CORouteTOMaster.URL.Tender,
                        data: { dataMaster: data, dataRate: grid.dataSource.data(), rTime: $scope.RateTime },
                        success: function (res) {
                            Common.Services.Error(res, function (res) {
                                $scope.LoadData();
                                $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                                win.close();

                                // Gửi mail Tender
                                Common.Services.Call($http, {
                                    url: Common.Services.url.OPS,
                                    method: _OPSAppointment_CORouteTOMaster.URL.SendMailToTender,
                                    data: { lst: res },
                                    success: function (res) {
                                        $rootScope.Message({ Msg: 'Đã gửi mail', NotifyType: Common.Message.NotifyType.SUCCESS });
                                    }
                                });
                            });
                        }
                    });
                }
            });
        }
    };

    $scope.Tendered_Click = function ($event, win, grid) {
        Common.Log('Tendered_Click');
        $event.preventDefault();

        var data = [];
        var firstVendorID = -1;
        $scope.gridNoDNMaster.tbody.find('.chkChooseVehicle').each(function () {
            if (this.checked) {
                var group = $(this).closest('.HasDNGridGroup');
                var sort = parseInt($(group).attr('tabid'));
                var item = _OPSAppointment_CORouteTOMaster.Data._dataHasDNSort[sort];

                if (sort > 0 && item.ID > 0) {
                    if (firstVendorID < 0)
                        firstVendorID = item.VendorOfVehicleID;
                    data.push(item.ID);
                }
            }
        });
        if (data.length > 0) {
            var dataVEN = [];
            var dataVendor = $.extend(true, [], _OPSAppointment_CORouteTOMaster.Data._dataHasDNVendor);
            dataVendor.splice(0, 1);
            dataVEN.push({ SortOrder: 1, VendorID: dataVendor[0].ID, IsManual: false, Debit: 0, lstVendor: dataVendor });
            dataVEN.push({ SortOrder: 2, VendorID: dataVendor[1].ID, IsManual: false, Debit: 0, lstVendor: dataVendor });
            dataVEN.push({ SortOrder: 3, VendorID: dataVendor[2].ID, IsManual: false, Debit: 0, lstVendor: dataVendor });

            $scope.OPSAppointment_CORouteTOMaster_VendorRate_grid.dataSource.data(dataVEN);

            $timeout(function () {
                $scope.OPSAppointment_CORouteTOMaster_VendorRate_grid.resize();
            }, 10);

            win.center().open();
        }
    };

    $scope.TotalTon = function () {
        $.each(_OPSAppointment_CORouteTOMaster.Data._dataHasDNSort, function (i, v) {
            if (Common.HasValue(v)) {
                var totalton = 0;
                $.each(_OPSAppointment_CORouteTOMaster.Data._dataHasDNMaster, function (j, m) {
                    if (m.CreateSortOrder == v.CreateSortOrder && m.CreateSortOrder > 0) {
                        totalton = totalton + m.Ton;
                    }
                });
                v.TotalTon = totalton;
            }
        });
    };

    $scope.TotalTonNo = function (totalTonNoMaster) {
        $scope.Summary = '';
        $scope.Summary = 'KL: ' + Common.Number.ToNumber1($scope.totalTonNoMaster);
    };

    $scope.CheckBox_All_Check = function ($event, grid) {
        var checked = $event.currentTarget.checked;
        if (checked == true) {
            grid.tbody.find('.HasDNGridGroup').each(function () {
                var CheckBox = $(this).find('input.chkChooseVehicle');
                CheckBox.prop('checked', true);
            });
        }
        else {
            grid.tbody.find('.HasDNGridGroup').each(function () {
                var CheckBox = $(this).find('input.chkChooseVehicle');
                CheckBox.prop('checked', false);
            });
        }
        $scope.ReloadButton();
    }

    $scope.ReloadSortMaster = function () {
        Common.Log("ReloadSortMaster");
        $scope.IsShowUnMerge = false;
        var ton = 0;
        var index = 0;
        $scope.SummaryMaster = '';
        $scope.gridNoDNMaster.tbody.find('.HasDNGridGroup').each(function () {
            var sort = parseInt($(this).attr('tabid'));
            var item = _OPSAppointment_CORouteTOMaster.Data._dataHasDNSort[sort];
            var tr = $(this).closest('tr');
            if (sort > 0) {
                ton = ton + item.TotalTon;
                index++;
            }
        });
        $scope.SummaryMaster = 'KL: ' + Common.Number.ToNumber1(ton) + ' - Số chuyến: ' + index;
    };

    $scope.ViewA_Click = function ($event) {
        $event.preventDefault();

        $('#2view').kendoWindow({
            title: false,
            close: function (e) {
                var win = this;
                $timeout(function () {
                    $('#2view-container').append(win.element);
                    $('#2view').resize();
                    win.destroy();
                    $scope.IsFullScreen = false;
                }, 1)
            }
        });
        $('#2view').data('kendoWindow').maximize();
        $scope.IsFullScreen = true;
    }

    $scope.Change_View = function ($event, data) {
        $event.preventDefault();

        if (data == 1) {
            $scope.IsChangeView = false;
        } else if (data == 2) {
            $scope.IsChangeView = true;
        }
        $timeout(function () {
            $scope.gridNoDNMaster.refresh();
            $scope.gridNoDN.refresh();
        });
    }

    $scope.ViewB_Click = function ($event) {
        $event.preventDefault();

        var win = $('#2view').data('kendoWindow');
        $('#2view-container').append(win.element);
        $('#2view').resize();
        win.destroy();
        $scope.IsFullScreen = false;
    }

    //#region Action
    $scope.ShowSetting = function ($event, grid) {
        $event.preventDefault();

        $rootScope.ShowSetting({
            ListView: views.OPSAppointment,
            event: $event,
            grid: grid,
            current: $state.current
        });
    };

}]);