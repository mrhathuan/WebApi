/// <reference path="~/Scripts/jquery-1.9.1.intellisense.js" />
/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />

var _DIPacket_Ven_2View = {
    URL: {
        GroupProduct_List: "OPS_DI_VEN_2View_GroupProduct_List",
        ListMaster: "OPS_DI_VEN_2View_TOMaster_List",
        ListVehicle: "TruckByVendorID_List",
        Save: "OPS_DI_VEN_2View_TOMaster_Save",
        Delete: "OPS_DI_VEN_2View_TOMaster_Delete",
        Packet_Confirm: "OPS_DI_VEN_Tendering_Packet_Confirm",
        Get: 'OPS_DI_VEN_2View_Get',
        List: "Opt_COTOGroupProduct_List",
        Run: "Opt_Optimizer_Run",
    },
    Data: {
        _data2View: [],
        _data2ViewMaster: [],
        _data2ViewSort: [],
        _data2ViewVehicle: [],
        _dataGroupProduct:[],
        _formatMoney: "n0",
    }
}

angular.module('myapp').controller('OPSAppointment_DIPacket_Ven_2ViewCtrl', ['$rootScope', '$scope', '$http', '$location', '$state', '$timeout', 'openMap', function ($rootScope, $scope, $http, $location, $state, $timeout, openMap) {
    if ($rootScope.IsPageComplete != true) return;
    Common.Log('OPSAppointment_DIPacket_Ven_2ViewCtrl');
    $rootScope.IsLoading = false;
    $scope.ID = parseInt($state.params.ID);
    $scope.packetID = parseInt($state.params.DetailID);
    $scope.vendorID = parseInt($state.params.VendorID);
    $scope.PacketName = "";
    $scope.IsFullScreen = false;
    $scope.IsShowSave = false;
    $scope.IsDelete = false;
    $scope.IsShowUnMerge = false;
    $scope.IsConfirm = false;;

    //try {
    //    var objCookie = JSON.parse(Common.Cookie.Get("OPSCOOptimizer"));
    //    if (Common.HasValue(objCookie)) {
    //        if (objCookie.ID != $scope.OptimizerID) {
    //            Common.Services.Call($http, {
    //                url: Common.Services.url.OPS,
    //                method: _COOptimizer_Container.URL.Optimizer_Get,
    //                data: {
    //                    optimizerID: $scope.OptimizerID
    //                },
    //                success: function (res) {
    //                    Common.Services.Error(res, function (res) {
    //                        if (res.ID > 0) {
    //                            Common.Cookie.Set("OPSCOOptimizer", JSON.stringify(res));
    //                            $scope.OptimizerName = res.OptimizerName;
    //                            $scope.StatusOfOptimizer = res.StatusOfOptimizer;
    //                            $scope.OptimizerClosed = $scope.StatusOfOptimizer == 2;
    //                        } else {
    //                            $rootScope.Message({ Msg: "Không tìm thấy optimizer." });
    //                            $state.go("main.OPSAppointment.COOptimizer");
    //                        }
    //                    })
    //                }
    //            })
    //        } else {
    //            $scope.OptimizerName = objCookie.OptimizerName;
    //            $scope.StatusOfOptimizer = objCookie.StatusOfOptimizer;
    //            $scope.OptimizerClosed = $scope.StatusOfOptimizer == 2;
    //        }
    //    }
    //}
    //catch (e) { }

    $scope.gridNoMasterOptions = {
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields:
                    {
                        ID: { type: 'number' },
                        IsGopSplit: { type: 'boolean' },
                        ETD: { type: 'date' },
                        ETA: { type: 'date' },
                    }
            },
            group: [{ field: 'CreateSortOrder', dir: 'desc' }]
        }),
        height: '99%', groupable: false, pageable: false, columnMenu: false, sortable: false, resizable: true, reorderable: true, sortable: { mode: 'multiple' },
        columns: [
            {
                field: '', width: '45px',
                headerTemplate: '<a class="k-button" href="/" ng-click="Expand_Click($event,gridNoDNMaster)"><i class="fa fa-minus"></i></a>',
                sortable: false, filterable: false, menu: false
            },
            {
                field: 'CreateSortOrder', width: '45px',
                template: '<form class="cus-form-enter" ng-submit="SortEnter_Click($event)"><input kendo-numeric-text-box class="txtCreateSortOrder" value="#=CreateSortOrder>0?CreateSortOrder:0#" data-k-min="0" k-options="numericSortOrderMasterOptions" style="width:100%" /></form>',
                headerTemplate: '<input type="checkbox" ng-show="!IsConfirm" ng-model="ValCheckBox" ng-click="CheckBox_All_Check($event,gridNoMaster)" />',
                groupHeaderTemplate: '<span class="HasGridGroup" tabid="#=value#"></span>',
                sortable: false, filterable: false, menu: false
            },
            {
                field: 'GroupProductName', width: '150px', title: 'Nhóm sản phẩm',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'OrderCode', width: '100px', title: 'Mã ĐH',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'CustomerCode', width: '110px', title: 'Khách hàng',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'IsGopSplit', width: '85px', title: 'Đã chia', templateAttributes: { style: 'text-align: center;' }, template: "<input type='checkbox' ng-model='dataItem.IsGopSplit' class='checkbox' disabled/>",
                attributes: { style: "text-align: center; " }, headerAttributes: { style: 'text-align: center;' },
                filterable: {
                    cell: {
                        template: function (container) {
                            container.element.kendoComboBox({
                                dataSource: [{ Text: 'Sai', Value: false }, { Text: 'Đúng', Value: true }, { Text: 'Tất cả', Value: '' }],
                                dataTextField: "Text", dataValueField: "Value",
                            });
                        },
                        showOperators: false
                    }
                }
            },
            {
                field: 'Ton', width: '80px', title: 'Tấn', template: '',
                filterable: { cell: { operator: 'gte', showOperators: false } }
            },
            {
                field: 'CBM', width: '80px', title: 'Khối', template: '',
                filterable: { cell: { operator: 'gte', showOperators: false } }
            },
            {
                field: 'Quantity', width: '80px', title: 'Sản lượng', template: '',
                filterable: { cell: { operator: 'gte', showOperators: false } }
            },
            {
                field: 'DNCode', width: '100px', title: 'Số DN',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'ETD', width: '150px', title: 'ETD',
                template: "#=ETD==null?' ':Common.Date.FromJsonDMYHM(ETD)#",
                filterable: {
                    cell: {
                        template: function (e) {
                            e.element.kendoDatePicker({ format: Common.Date.Format.DMY });
                        },
                        operator: 'equal', showOperators: false
                    }
                },
            },
            {
                field: 'ETA', width: '150px', title: 'ETA',
                template: "#=ETA==null?' ':Common.Date.FromJsonDMYHM(ETA)#",
                filterable: {
                    cell: {
                        template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); },
                        operator: 'equal', showOperators: false
                    }
                },
            },
            {
                field: 'Note1', width: '180px', title: 'Ghi chú',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'PartnerCode', width: '100px', title: 'NPP',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'LocationFromName', width: '150px', title: 'Điểm nhận',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'LocationFromAddress', width: '200px', title: 'Địa chỉ',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'LocationFromProvince', width: '100px', title: 'Tỉnh thành',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'LocationFromDistrict', width: '100px', title: 'Quận huyện',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'LocationToName', width: '150px', title: 'Điểm giao',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'LocationToAddress', width: '200px', title: 'Địa chỉ',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'LocationToDistrict', width: '100px', title: 'Quận huyện',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'LocationToProvince', width: '100px', title: 'Tỉnh thành',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            { title: ' ', filterable: false, menu: false, sortable: false }
        ],
        filterable: { mode: 'row' },
        dataBound: function () {
            Common.Log("GridMasterDataBound");

            if (Common.HasValue(this) && Common.HasValue(this.tbody)) {
                var sort = 0;
                this.element.find('.HasGridGroup').each(function () {
                    sort = parseInt($(this).attr('tabid'));
                    var tr = $(this).closest('tr');
                    if (sort > 0) {
                        var item = _DIPacket_Ven_2View.Data._data2ViewSort[sort];
                        var tr = $(this).closest('tr');
                        if (Common.HasValue(item)) {
                            if (item.IsChange == true) {
                                if (!$(tr).hasClass('approved'))
                                    $(tr).addClass('approved');
                            }
                        }
                        if (!Common.HasValue(item)) {
                            item = _DIPacket_Ven_2View.Data._data2ViewSort[0];
                            _DIPacket_Ven_2View.Data._data2ViewSort[sort] = { 'CreateSortOrder': sort, 'VehicleID': item.VehicleID, 'VehicleNo': item.VehicleNo, 'ETD': Common.Date.FromJson(Date()), 'ETA': Common.Date.FromJson(Date()),'HasChanged': true };
                        }
                        var VehicleID = item.VehicleID != null ? item.VehicleID : '';
                        if ($scope.IsConfirm)
                        {
                            $(this).html('Chuyến ' + sort + ' - ' + item.VehicleNo + ' - ' + Common.Date.FromJsonDMYHM(item.ETD) + ' - ' + Common.Date.FromJsonDMYHM(item.ETA));
                        }
                        else
                        $(this).html('<input type="checkbox" class="chkChooseVehicle"/> Chuyến ' + sort + ' - ' +
                                ' <a href="" class="btnHasGroup">' + item.VehicleNo + ' - ' + Common.Date.FromJsonDMYHM(item.ETD) + ' - ' + Common.Date.FromJsonDMYHM(item.ETA) + '</a>' +
                                '<input style="display:none;width:100px" focus-k-combobox class="cboHasVehicle cus-combobox" placeholder="Đầu kéo" value="' + VehicleID + '" /><span style="display:none" class="lblHasSplit"> - </span>' +
                                '<span style="display:none" class="lblHasSplit"> - </span>' +
                                '<input style="display:none;width:160px" class="txtCreateDateTime_ETD" value="' + Common.Date.FromJsonDMYHM(item.ETD) + '"/>' + '<span style="display:none" class="lblHasSplit"> - </span>' +
                                '<input style="display:none;width:160px" class="txtCreateDateTime_ETA" value="' + Common.Date.FromJsonDMYHM(item.ETA) + '"/>');

                    }
                });

                this.element.find('.HasGridGroup .btnHasGroup').click(function (e) {
                    e.preventDefault();
                    var tr = $(this).closest('tr');
                    var sort = parseInt($(tr).find('.HasGridGroup').attr('tabid'));
                    _DIPacket_Ven_2View.Data._data2ViewSort[sort].HasChanged = true;
                    $scope.IsShowSave = true;
                    $scope.Group_Click(this, sort);
                });
                this.element.find('.HasGridGroup .chkChooseVehicle').click(function(e){
                    $scope.ReloadButton();
                });
            }
        }
    };

    $scope.gridNoOptions = {
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                fields:
                {
                    ID: { type: 'number' },
                    ETD: { type: 'date' },
                    IsGopSplit: { type: 'boolean' },
                    ETA: { type: 'date' },
                }
            },
            pageSize:20,
        }),
        height: '99%', groupable: false, pageable: true, columnMenu: false, sortable: false, resizable: true, reorderable: true, sortable: { mode: 'row' },
        columns: [
            { field: 'CreateSortOrder', width: '50px', template: '<form class="cus-form-enter" ng-submit="SortEnter_Click($event)"><input kendo-numeric-text-box class="txtCreateSortOrder" value="#=CreateSortOrder>0?CreateSortOrder:0#" data-k-min="0" k-options="numericSortOrderOptions" style="width:100%" /></form>', headerTemplate: '<a class="k-button" href="/"><i class="fa"></i></a>', sortable: false, filterable: false, menu: false },
            {
                field: 'GroupProductName', width: '150px', title: 'Nhóm sản phẩm',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'OrderCode', width: '100px', title: 'Mã ĐH',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'CustomerCode', width: '110px', title: 'Khách hàng',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'IsGopSplit', width: '85px', title: 'Đã chia', templateAttributes: { style: 'text-align: center;' }, template: "<input type='checkbox' ng-model='dataItem.IsGopSplit' class='checkbox' disabled/>",
                attributes: { style: "text-align: center; " }, headerAttributes: { style: 'text-align: center;' },
                filterable: {
                    cell: {
                        template: function (container) {
                            container.element.kendoComboBox({
                                dataSource: [{ Text: 'Sai', Value: false }, { Text: 'Đúng', Value: true }, { Text: 'Tất cả', Value: '' }],
                                dataTextField: "Text", dataValueField: "Value",
                            });
                        },
                        showOperators: false
                    }
                }
            },
            {
                field: 'Merge', title: 'Gộp', width: '45px', template: '<a href="" class="k-button btnAdd" ng-click="Merge_Click($event, gridNo)" style="display:none">M</a>' +
                  '<a href="" class="k-button btnAddSave" ng-click="MergeSave_Click($event, gridNo)" style="display:none">S</a>' +
                  '<input type="checkbox" class="chkAdd" style="display:none"/>', attributes: { style: "text-align: center; " }, filterable: false, menu: false, sortable: false
            },
            {
                field: 'Ton', width: '80px', title: 'Tấn', template: function (dataItem) {
                    if ($scope.IsConfirm)
                        return dataItem.Quantity;
                    else {
                        if (dataItem.EditType == 1)
                            return '<form class="cus-form-enter"><input kendo-numeric-text-box value="{{dataItem.Ton}}" k-options="numericTonOptions" style="width:100%" /></form> ';
                        else return dataItem.Ton;
                    }
                },
                filterable: { cell: { operator: 'gte', showOperators: false } }
            },
            {
                field: 'CBM', width: '80px', title: 'Khối', template: function (dataItem) {
                    if ($scope.IsConfirm)
                        return dataItem.Quantity;
                    else {
                        if (dataItem.EditType == 2)
                            return '<form class="cus-form-enter"><input kendo-numeric-text-box value="{{dataItem.CBM}}" k-options="numericCBMOptions" style="width:100%" /></form>';
                        else return dataItem.CBM;
                    }
                },
                filterable: { cell: { operator: 'gte', showOperators: false } }
            },
            {
                field: 'Quantity', width: '80px', title: 'Sản lượng', template: function (dataItem) {
                    if ($scope.IsConfirm)
                        return dataItem.Quantity;
                    else {
                        if (dataItem.EditType == 3)
                            return '<form class="cus-form-enter"><input kendo-numeric-text-box value="{{dataItem.Quantity}}" k-options="numericQuantityOptions" style="width:100%" /></form> ';
                        else return dataItem.Quantity;
                    }
                },
                filterable: { cell: { operator: 'gte', showOperators: false } }
            },
            {
                field: 'DNCode', width: '100px', title: 'Số DN',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'ETD', width: '150px', title: 'ETD',
                template: "#=ETD==null?' ':Common.Date.FromJsonDMYHM(ETD)#",
                filterable: {
                    cell: {
                        template: function (e) {
                            e.element.kendoDatePicker({ format: Common.Date.Format.DMY });
                        },
                        operator: 'equal', showOperators: false
                    }
                },
            },
            {
                field: 'ETA', width: '150px', title: 'ETA',
                template: "#=ETA==null?' ':Common.Date.FromJsonDMYHM(ETA)#",
                filterable: {
                    cell: {
                        template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); },
                        operator: 'equal', showOperators: false
                    }
                },
            },
            {
                field: 'Note1', width: '180px', title: 'Ghi chú',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'PartnerCode', width: '100px', title: 'NPP',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'LocationFromName', width: '150px', title: 'Điểm nhận',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'LocationFromAddress', width: '200px', title: 'Địa chỉ',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'LocationFromProvince', width: '100px', title: 'Tỉnh thành',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'LocationFromDistrict', width: '100px', title: 'Quận huyện',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'LocationToName', width: '150px', title: 'Điểm giao',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'LocationToAddress', width: '200px', title: 'Địa chỉ',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'LocationToDistrict', width: '100px', title: 'Quận huyện',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'LocationToProvince', width: '100px', title: 'Tỉnh thành',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            { title: ' ', filterable: false, menu: false, sortable: false }
        ],
        filterable: { mode: 'row' },
        dataBound: function () {
            Common.Log("GridDataBound");

            var grid = this;
            var h = Common.Cookie.Get("Scroll");
            grid.wrapper.find('.k-grid-content').scrollTop(h);
            this.element.find('tr[role="row"] input.chkAdd').closest('td').css('text-align','center');

            if (Common.HasValue(this) && Common.HasValue(this.tbody)) {
                if (_DIPacket_Ven_2View.Data._dataGroupProduct.length == 0)
                    $scope.ReloadSort();
                this.element.find('tr[role="row"]').each(function () {
                    var btnAdd = $(this).find('.btnAdd');
                    var btnAddSave = $(this).find('.btnAddSave');
                    var chkAdd = $(this).find('.chkAdd');
                    if (Common.HasValue($scope.gridNo)) {
                        var dataItem = $scope.gridNo.dataItem(this);
                        if (Common.HasValue(dataItem)) {
                            if (_DIPacket_Ven_2View.Data._dataGroupProduct[dataItem.ID].length > 1)
                                btnAdd.show();
                        }
                    }
                });
            }
        },
    };

    $scope.LoadData = function () {
        Common.Log('LoadData');
        $rootScope.IsLoading = true;
        var paramCo = Common.Request.Create({
            Sorts: [], Filters: []
        });
        Common.Services.Call($http, {
            url: Common.Services.url.OPS,
            method: _DIPacket_Ven_2View.URL.ListVehicle,
            data: { vendorID: $scope.vendorID, request: "" },
            success: function (res) {
                _DIPacket_Ven_2View.Data._data2ViewVehicle = res.Data;

                Common.Services.Call($http, {
                    url: Common.Services.url.OPS,
                    method: _DIPacket_Ven_2View.URL.GroupProduct_List,
                    data: { request: paramCo, packetID: $scope.packetID, hasMaster: true },
                    success: function (res) {
                        $scope.totalTonNoMaster = 0;
                        _DIPacket_Ven_2View.Data._data2ViewMaster = res.Data;

                        Common.Services.Call($http, {
                            url: Common.Services.url.OPS,
                            method: _DIPacket_Ven_2View.URL.ListMaster,
                            data: { request: paramCo, packetID: $scope.packetID },
                            success: function (res) {
                                _DIPacket_Ven_2View.Data._data2ViewSort[0] = { 'CreateSortOrder': 0, 'VehicleID': _DIPacket_Ven_2View.Data._data2ViewVehicle[0].ID, 'VehicleNo': _DIPacket_Ven_2View.Data._data2ViewVehicle[0].RegNo, 'ETD': Common.Date.FromJson(Date()), 'ETA': Common.Date.FromJson(Date()), 'ID': 0 };
                                var index = 1;
                                $.each(res.Data, function (i, m) {
                                    m.CreateSortOrder = index;
                                    _DIPacket_Ven_2View.Data._data2ViewSort[index] = m;
                                    $.each(_DIPacket_Ven_2View.Data._data2ViewMaster, function (i, v) {
                                        if (v.DIPacketTOMasterID == m.ID) {
                                            v.CreateSortOrder = m.CreateSortOrder;
                                        }
                                    });
                                    index++;
                                });
                                $scope.gridNoMasterOptions.dataSource.data(_DIPacket_Ven_2View.Data._data2ViewMaster);
                                $rootScope.IsLoading = false;
                            }
                        });
                    }
                });
            }
        });
        $rootScope.IsLoading = true;
        Common.Services.Call($http, {
            url: Common.Services.url.OPS,
            method: _DIPacket_Ven_2View.URL.GroupProduct_List,
            data: { request: paramCo, packetID: $scope.packetID, hasMaster: false },
            success: function (res) {
                $scope.totalTonNoMaster = 0;
                _DIPacket_Ven_2View.Data._data2View = res.Data;
                $scope.gridNoOptions.dataSource.data(res.Data);
                $.each(res.Data, function (i, v) {
                    if (v.CreateSortOrder <= 0) {
                        $scope.totalTonNoMaster = $scope.totalTonNoMaster + v.Ton;
                    }
                });
                //$scope.TotalTonNo($scope.totalTonNoMaster);
                $rootScope.IsLoading = false;
            }
        });
        $timeout(function () {
            $scope.IsShowSave = false;
        }, 1);

    };
    $scope.LoadData();

    $scope.numericSortOrderOptions = {
        format: '#', spinners: false, culture: 'en-US', step: 1, decimals: 0, min: 0,
        change: function (e) {
            var h = $scope.gridNo.wrapper.find('.k-grid-content').scrollTop();
            Common.Cookie.Set("Scroll", h);
            var sort = e.sender.element.val() == '' ? 0 : parseInt(e.sender.element.val());
            if (sort > 0) {
                if (Common.HasValue(_DIPacket_Ven_2View.Data._data2ViewSort[sort]))
                    _DIPacket_Ven_2View.Data._data2ViewSort[sort].HasChanged = true;
                var tr = $(e.sender.element).closest('tr');
                var dataItem = $scope.gridNo.dataItem(tr);
                // Add vào grid chuyến, remove khỏi grid đơn hàng
                dataItem.CreateSortOrder = sort;

                // Add vào grid chuyến
                _DIPacket_Ven_2View.Data._data2ViewMaster = [];
                _DIPacket_Ven_2View.Data._data2ViewMaster = $.extend(true, [], $scope.gridNoMaster.dataSource.data());
                _DIPacket_Ven_2View.Data._data2ViewMaster.push(dataItem);
                // remove khỏi grid đơn hàng
                $scope.gridNo.removeRow(tr);
                $timeout(function () {
                    $scope.IsShowSave = true;
                    $scope.gridNoMaster.dataSource.data(_DIPacket_Ven_2View.Data._data2ViewMaster);
                }, 1);
            }
        }
    };

    $scope.numericSortOrderMasterOptions = {
        format: '#', spinners: false, culture: 'en-US', step: 1, decimals: 0, min: 0,
        change: function (e) {
            var sort = e.sender.element.val() == '' ? 0 : parseInt(e.sender.element.val());
            var sortOld = e.sender.element[0].defaultValue;
            var tr = $(e.sender.element).closest('tr');
            var dataItem = $scope.gridNoMaster.dataItem(tr);
            _DIPacket_Ven_2View.Data._data2ViewSort[sortOld].HasChanged = true;
            if (Common.HasValue(dataItem)) {
                // remove khỏi grid chuyến, add vào grid đơn hàng
                if (sort <= 0) {
                    dataItem.CreateSortOrder = 0;
                    // Add vào grid đơn hàng
                    _DIPacket_Ven_2View.Data._data2View = $.extend(true, [], $scope.gridNo.dataSource.data());
                    _DIPacket_Ven_2View.Data._data2View.push(dataItem);
                    $scope.gridNo.dataSource.data(_DIPacket_Ven_2View.Data._data2View);
                    // remove khỏi grid chuyến
                    $scope.gridNoMaster.removeRow(tr);
                } else {
                    dataItem.CreateSortOrder = sort;
                    _DIPacket_Ven_2View.Data._data2ViewSort[sort].HasChanged = true;
                    $scope.gridNoMaster.dataSource.data($scope.gridNoMaster.dataSource.data());
                }
            }
            $timeout(function () {
                $scope.IsShowSave = true;
            }, 1);
        }
    };

    $scope.Save_Click = function ($event, grid) {
        $event.preventDefault();

        var data = [];
        $.each(_DIPacket_Ven_2View.Data._data2ViewSort, function (i, v) {
            if (Common.HasValue(v) && v.CreateSortOrder > 0) {
                data.push(v);
            }
        });

        Common.Services.Call($http, {
            url: Common.Services.url.OPS,
            method: _DIPacket_Ven_2View.URL.Save,
            data: { packetID: $scope.packetID, dataMaster: data, dataGop: grid.dataSource.data() },
            success: function (res) {
                $rootScope.Message({
                    Msg: 'Đã cập nhật.',
                    NotifyType: Common.Message.NotifyType.SUCCESS
                });
                $scope.LoadData();
            }
        });

    }
    //kendo
    $scope.hor_splitterOptions = {

        orientation: "horizontal",
        panes: [
            { collapsible: true, size: "50%" },
            { collapsible: true, size: "50%" }
        ]
    };

    $scope.Group_Click = function (item, sort) {
        Common.Log('Group_Click');

        $(item).hide();
        var dataVehicle = [];
        var group = $(item).closest('.HasGridGroup');
        var tr = $(item).closest('tr');
        $(tr).find('.lblHasSplit,input').show();
        $(tr).find('.txtHasMaxWeight,.txtHasMaxCBM').hide();
        $(tr).find('.lblHasSplit.vehicle').hide();

        var Time_ETD = tr.find('input.txtCreateDateTime_ETD');
        Time_ETD.kendoDateTimePicker({
            format: Common.Date.Format.DMYHM,
            timeFormat: Common.Date.Format.HM,
            change: function (e) {
                var EditDate = new Date(e.sender.value());
                _DIPacket_Ven_2View.Data._data2ViewSort[sort].ETD = e.sender.value();
                _DIPacket_Ven_2View.Data._data2ViewSort[sort].IsChange = true;
            },

        });
        $rootScope.FocusKDateTimePicker(Time_ETD);

        var Time_ETA = tr.find('input.txtCreateDateTime_ETA');
        Time_ETA.kendoDateTimePicker({
            format: Common.Date.Format.DMYHM,
            timeFormat: Common.Date.Format.HM,
            change: function (e) {
                var EditDate = new Date(e.sender.value());
                _DIPacket_Ven_2View.Data._data2ViewSort[sort].ETA = e.sender.value();
                _DIPacket_Ven_2View.Data._data2ViewSort[sort].IsChange = true;
            },

        });
        $rootScope.FocusKDateTimePicker(Time_ETA);

        var cboVehicle = tr.find('input.cboHasVehicle');
        cboVehicle.kendoComboBox({
            index: 0, autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true,
            dataTextField: 'RegNo', dataValueField: 'ID',
            dataSource:
                Common.DataSource.Local({
                    data: _DIPacket_Ven_2View.Data._data2ViewVehicle,
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
                _DIPacket_Ven_2View.Data._data2ViewSort[sort].VehicleNo = text;
                _DIPacket_Ven_2View.Data._data2ViewSort[sort].VehicleID = ID;
            }

        });
        $rootScope.FocusKCombobox(cboVehicle);
    };

    $scope.Expand_Click = function ($event, grid) {
        Common.Log('Expand_Click');
        $event.preventDefault();

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

    Common.Services.Call($http, {
        url: Common.Services.url.OPS,
        method: _DIPacket_Ven_2View.URL.Get,
        data: { ID: $scope.ID},
        success: function (res) {
            $scope.PacketName = res.PacketName;
            if (res.DIPacketDetailID == $scope.packetID && $scope.vendorID == res.VendorID) {
                $scope.IsConfirm = res.IsConfirm;
                if ($scope.IsConfirm) {
                    $timeout(function () {
                        $scope.gridNo.hideColumn("CreateSortOrder");
                    }, 100)
                }
            }
            else
                $state.go("main.OPSAppointment.DIPacket_Ven");
        }
    });

    $scope.Close_Click = function ($event, win) {
        $event.preventDefault();

        win.close();
    }

    $scope.View = {
        Style: {
            position: 'relative', top: 0, left: 0, 'z-index': 10000
        }
    }

    // Format cho textbox Tấn
    $scope.numericTonOptions = {
        min: 0,
        spinners: false,
        decimals: Common.Number.DI_Decimals,
        culture: "en-US",
        format: "n5",
        change: function (e) {
            //set scroll
            var h = $scope.gridNo.wrapper.find('.k-grid-content').scrollTop();
            Common.Cookie.Set("Scroll", h);

            var tr = $(e.sender.element).closest('tr');
            var dataItem = $scope.gridNo.dataItem(tr);
            if (this.value() > 0)
            {
                var sub = dataItem.Ton - this.value();
                var TL = dataItem.Ton / this.value();
                if (sub > 0.001) {
                    var itemsub = $.extend(true, {}, dataItem);
                    itemsub.Ton = sub;
                    itemsub.IsSplit = true;
                    dataItem.IsSplit = true;
                    dataItem.Ton = this.value();
                    itemsub.CBM = dataItem.CBM - (dataItem.CBM / TL);
                    itemsub.Quantity = dataItem.Quantity - (dataItem.Quantity / TL);
                    dataItem.CBM = dataItem.CBM / TL;
                    dataItem.Quantity = dataItem.Quantity / TL;
                    _DIPacket_Ven_2View.Data._data2View = $scope.gridNo.dataSource.data();
                    var index = _DIPacket_Ven_2View.Data._data2View.indexOf(dataItem);
                    _DIPacket_Ven_2View.Data._data2View.splice(index, 0, itemsub);
                    $scope.ReloadSort();
                    $scope.gridNo.dataSource.data(_DIPacket_Ven_2View.Data._data2View);

                }
            }
        }
    };

    // Format cho textbox CBM
    $scope.numericCBMOptions = {
        min: 0,
        spinners: false,
        decimals: Common.Number.DI_Decimals,
        culture: "en-US",
        format: "n3",
        change: function (e) {
            //set scroll
            var h = $scope.gridNo.wrapper.find('.k-grid-content').scrollTop();
            Common.Cookie.Set("Scroll", h);

            var tr = $(e.sender.element).closest('tr');
            var dataItem = $scope.gridNo.dataItem(tr);
            if (this.value() > 0) {
                var sub = dataItem.CBM - this.value();
                var TL = dataItem.CBM / this.value();
                if (sub > 0.001) {
                    var itemsub = $.extend(true, {}, dataItem);
                    itemsub.CBM = sub;
                    itemsub.IsSplit = true;
                    dataItem.IsSplit = true;
                    dataItem.CBM = this.value();
                    itemsub.Ton = dataItem.Ton - (dataItem.Ton / TL);
                    itemsub.Quantity = dataItem.Quantity - (dataItem.Quantity / TL);
                    dataItem.Ton = dataItem.Ton / TL;
                    dataItem.Quantity = dataItem.Quantity / TL;
                    _DIPacket_Ven_2View.Data._data2View = $scope.gridNo.dataSource.data();
                    var index = _DIPacket_Ven_2View.Data._data2View.indexOf(dataItem);
                    _DIPacket_Ven_2View.Data._data2View.splice(index, 0, itemsub);
                    $scope.ReloadSort();
                    $scope.gridNo.dataSource.data(_DIPacket_Ven_2View.Data._data2View);
                }
            }
        }
    };

    // Format cho textbox Quantity
    $scope.numericQuantityOptions = {
        min: 0,
        spinners: false,
        decimals: Common.Number.DI_Decimals,
        culture: "en-US",
        format: "n3",
        change: function (e) {
            //set scroll
            var h = $scope.gridNo.wrapper.find('.k-grid-content').scrollTop();
            Common.Cookie.Set("Scroll", h);

            var tr = $(e.sender.element).closest('tr');
            var dataItem = $scope.gridNo.dataItem(tr);
            if (this.value() > 0) {
                var sub = dataItem.Quantity - this.value();
                var TL = dataItem.CBM / this.value();
                if (sub > 0.001) {
                    var itemsub = $.extend(true, {}, dataItem);
                    itemsub.Quantity = sub;
                    itemsub.IsSplit = true;
                    dataItem.IsSplit = true;
                    dataItem.Quantity = this.value();
                    itemsub.Ton = dataItem.Ton - (dataItem.Ton / TL);
                    itemsub.CBM = dataItem.CBM - (dataItem.CBM / TL);
                    dataItem.Ton = dataItem.Ton / TL;
                    dataItem.CBM = dataItem.CBM / TL;
                    _DIPacket_Ven_2View.Data._data2View = $scope.gridNo.dataSource.data();
                    var index = _DIPacket_Ven_2View.Data._data2View.indexOf(dataItem);
                    _DIPacket_Ven_2View.Data._data2View.splice(index, 0, itemsub);
                    $scope.ReloadSort();
                    $scope.gridNo.dataSource.data(_DIPacket_Ven_2View.Data._data2View);
                }
            }
        }
    };

    $scope.ReloadSort = function ($event) {
        Common.Log("ReloadSort");
        $scope.IsShowUnMerge = false;
        var totalton = 0;
        var totalcbm = 0;
        var totalquan = 0;

        _DIPacket_Ven_2View.Data._dataGroupProduct = [];
        $.each(_DIPacket_Ven_2View.Data._data2View, function (i, v) {
            if (v.CreateSortOrder <= 0) {
                v.Ton = v.Ton > 0 ? Math.round(v.Ton * 1000000) / 1000000 : 0;
                v.CBM = v.CBM > 0 ? Math.round(v.CBM * 1000) / 1000 : 0;
                v.Quantity = v.Quantity > 0 ? Math.round(v.Quantity * 1000) / 1000 : 0;
                if (!Common.HasValue(_DIPacket_Ven_2View.Data._dataGroupProduct[v.ID]))
                    _DIPacket_Ven_2View.Data._dataGroupProduct[v.ID] = [];
                _DIPacket_Ven_2View.Data._dataGroupProduct[v.ID].push(v);
            }
        });
    };

    $scope.Merge_Click = function ($event, grid) {
        Common.Log("Merge_Click");
        $event.preventDefault();
        // Ẩn các nút Merge
        grid.tbody.find('tr[role="row"] .btnAdd').hide();
        // Hiện nút Save
        var btnAddSave = $($($event.currentTarget).closest('tr')).find('.btnAddSave').show();
        // Hiện nút Hủy gom hàng
        $scope.IsShowUnMerge = true;
        // Lấy ra dataItem hiện tại
        var dataItem = this.dataItem;
        $.each(_DIPacket_Ven_2View.Data._dataGroupProduct[dataItem.ID], function (i, v) {
            if (v.uid != dataItem.uid) {
                var tr = grid.tbody.find("tr[data-uid='" + v.uid + "']");
                var chkAdd = $(tr).find('.chkAdd');
                $(chkAdd).show();
            }
        });
    };

    $scope.MergeSave_Click = function ($event, grid) {
        Common.Log("MergeSave_Click");
        $event.preventDefault();
        $scope.IsShowUnMerge = true;
        var dataItem = this.dataItem;
        var flat = 0;
        $.each(_DIPacket_Ven_2View.Data._dataGroupProduct[dataItem.ID], function (i, v) {
            if (v.uid != dataItem.uid) {
                var tr = grid.tbody.find("tr[data-uid='" + v.uid + "']");
                var chkAdd = $(tr).find('.chkAdd');

                if ($(chkAdd).prop('checked') == true) {
                    if (dataItem.EditType == 1) {
                        flat++;
                        dataItem.Ton += v.Ton;
                        dataItem.CBM += v.CBM;
                        dataItem.Quantity += v.Quantity;
                    } else {
                        if (dataItem.EditType == 2) {
                            flat++;
                            dataItem.CBM += v.CBM;
                            dataItem.Ton += v.Ton;
                            dataItem.Quantity += v.Quantity;
                        } else {
                            if (dataItem.EditType == 3) {
                                flat++;
                                dataItem.Quantity += v.Quantity;
                                dataItem.Ton += v.Ton;
                                dataItem.CBM += v.CBM;
                            }
                        }
                    }
                    if (_DIPacket_Ven_2View.Data._dataGroupProduct[dataItem.ID].count == flat) {
                        dataItem.IsSplit = false;
                    }
                    var index = _DIPacket_Ven_2View.Data._data2View.indexOf(v);
                    _DIPacket_Ven_2View.Data._data2View.splice(index, 1);
                }
            }
        });
        $scope.ReloadSort();
        grid.dataSource.data(_DIPacket_Ven_2View.Data._data2View);
    };

    $scope.MergeCancel_Click = function ($event, grid) {
        Common.Log("MergeCancel_Click");
        $event.preventDefault();
        grid.dataSource.data(_DIPacket_Ven_2View.Data._data2View);
        $scope.IsShowCombine = false;
    };

    $scope.Delete_Click = function($event, grid)
    {
        $event.preventDefault();
        var data = [];
        $scope.gridNoMaster.tbody.find('.HasGridGroup').each(function () {
            var sort = parseInt($(this).attr('tabid'));
            var item = _DIPacket_Ven_2View.Data._data2ViewSort[sort];
            var tr = $(this).closest('tr');
            if (sort > 0 ) {
                var chk = $(tr).find('.chkChooseVehicle');
                if (Common.HasValue(chk) && chk.prop('checked') == true && item.ID > 0)
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
                        method: _DIPacket_Ven_2View.URL.Delete,
                        data: {packetID: $scope.packetID, data: data },
                        success: function (res) {
                            Common.Services.Error(res, function (res) {
                                $scope.LoadData();
                                $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                            });
                        }
                    });
                }
            });
        } else
            $rootScope.Message({ Msg: 'Chỉ xóa các chuyến đã lưu', NotifyType: Common.Message.NotifyType.ERROR });
    }

    $scope.CheckBox_All_Check = function ($event, grid) {
        var checked = $event.currentTarget.checked;
        if (checked == true) {
            grid.tbody.find('.HasGridGroup').each(function () {
                var CheckBox = $(this).find('input.chkChooseVehicle');
                CheckBox.prop('checked', true);
            });
        }
        else {
            grid.tbody.find('.HasGridGroup').each(function () {
                var CheckBox = $(this).find('input.chkChooseVehicle');
                CheckBox.prop('checked', false);
            });
        }
        $scope.ReloadButton();
    }

    $scope.ReloadButton = function ($event) {
        var ischecked = false;
        $.each($scope.gridNoMaster.tbody.find('.HasGridGroup .chkChooseVehicle'), function (i, v) {
            if (this.checked) {
                ischecked = true;
            }
        });
        if (ischecked) $scope.IsDelete = true;
        else $scope.IsDelete = false;
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
    $scope.ViewB_Click = function ($event) {
        $event.preventDefault();

        var win = $('#2view').data('kendoWindow');
        $('#2view-container').append(win.element);
        $('#2view').resize();
        win.destroy();
        $scope.IsFullScreen = false;
    }

    $scope.Packet_Confirm_Click = function($event)
    {
        $event.preventDefault();
        
        $rootScope.Message({
            Msg: "Xác nhận!",
            Type: Common.Message.Type.Confirm,
            Ok: function () {
                $rootScope.IsLoading = true;
                Common.Services.Call($http, {
                    url: Common.Services.url.OPS,
                    method: _DIPacket_Ven_2View.URL.Packet_Confirm,
                    data: { packetID: $scope.packetID },
                    success: function (res) {
                        $state.go("main.OPSAppointment.DIPacket_Ven");
                        $rootScope.Message({
                            Msg: 'Thành công.',
                            NotifyType: Common.Message.NotifyType.SUCCESS
                        });
                        $rootScope.IsLoading = false;
                    }
                });
            }
        })
    }
}]);