/// <reference path="~/Scripts/jquery-1.9.1.intellisense.js" />
/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />

var _DIOptimizer_2View = {
    URL: {
        List: "Opt_COTOContainer_List",
        Container_List: "Opt_2ViewDI_GroupProduct_List",
        Run: "Opt_Optimizer_Run",
        Delete:"Opt_2ViewDI_Delete",
        ListMaster: "Opt_2ViewDI_Master_List",
        ListVehicle: "Opt_Vehicle_List",
        Save: "Opt_2ViewDI_SaveList",
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

angular.module('myapp').controller('OPSAppointment_DIOptimizer_2ViewCtrl', ['$rootScope', '$scope', '$http', '$location', '$state', '$timeout', 'openMap', function ($rootScope, $scope, $http, $location, $state, $timeout, openMap) {
    if ($rootScope.IsPageComplete != true) return;
    Common.Log('OPSAppointment_DIOptimizer_2ViewCtrl');
    $rootScope.IsLoading = false;
    $scope.OptimizerID = parseInt($state.params.OptimizerID);
    $scope.OptimizerName = "";
    $scope.IsShowSave = false;
    $scope.IsShowUnMerge = false;

    try {
        var objCookie = JSON.parse(Common.Cookie.Get("OPSCOOptimizer"));
        if (Common.HasValue(objCookie)) {
            if (objCookie.ID != $scope.OptimizerID) {
                Common.Services.Call($http, {
                    url: Common.Services.url.OPS,
                    method: _COOptimizer_Container.URL.Optimizer_Get,
                    data: {
                        optimizerID: $scope.OptimizerID
                    },
                    success: function (res) {
                        Common.Services.Error(res, function (res) {
                            if (res.ID > 0) {
                                Common.Cookie.Set("OPSCOOptimizer", JSON.stringify(res));
                                $scope.OptimizerName = res.OptimizerName;
                                $scope.StatusOfOptimizer = res.StatusOfOptimizer;
                                $scope.OptimizerClosed = $scope.StatusOfOptimizer == 2;
                            } else {
                                $rootScope.Message({ Msg: "Không tìm thấy optimizer." });
                                $state.go("main.OPSAppointment.COOptimizer");
                            }
                        })
                    }
                })
            } else {
                $scope.OptimizerName = objCookie.OptimizerName;
                $scope.StatusOfOptimizer = objCookie.StatusOfOptimizer;
                $scope.OptimizerClosed = $scope.StatusOfOptimizer == 2;
            }
        }
    }
    catch (e) { }

    $scope.gridNoMasterOptions = {
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields:
                    {
                        ID: { type: 'number' },
                        ETD: { type: 'date' },
                        IsGopSplit: { type: 'boolean' },
                        ETA: { type: 'date' },
                    }
            },
            group: [{ field: 'CreateSortOrder', dir: 'desc' }]
        }),
        height: '99%', groupable: false, pageable: false, columnMenu: false, sortable: false, resizable: true, reorderable: true, sortable: { mode: 'multiple' },
        columns: [
             {
                 field: 'Expand', width: '45px',
                 headerTemplate: '<a class="k-button" href="/" ng-click="Expand_Click($event,gridNoMaster)"><i class="fa fa-minus"></i></a>',
                 sortable: false, filterable: false, menu: false
             },
            {
                field: 'CreateSortOrder', width: '45px',
                template: '<form class="cus-form-enter" ng-submit="SortEnter_Click($event)"><input kendo-numeric-text-box class="txtCreateSortOrder" value="#=CreateSortOrder>0?CreateSortOrder:0#" data-k-min="0" k-options="numericSortOrderMasterOptions" style="width:100%" /></form>',
                headerTemplate: '<input type="checkbox" ng-model="ValCheckBox" ng-click="CheckBox_All_Check($event,gridNoMaster)" />',
                groupHeaderTemplate: '<span class="HasGridGroup" tabid="#=value#"></span>', sortable: false, filterable: false, menu: false
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
                field: 'ETDStart', width: '130px', title: 'BĐ ETD',
                template: "#=ETDStart==null?' ':Common.Date.ToString(ETDStart)#",
                filterable: {
                    cell: {
                        template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); },
                        operator: 'equal', showOperators: false
                    }
                },
            },
            {
                field: 'ETD', width: '130px', title: 'ETD',
                template: "#=ETD==null?' ':Common.Date.ToString(ETD)#",
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
                field: 'ETAStart', width: '130px', title: 'BĐ ETA',
                template: "#=ETAStart==null?' ':Common.Date.ToString(ETAStart)#",
                filterable: {
                    cell: {
                        template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); },
                        operator: 'equal', showOperators: false
                    }
                },
            },
            {
                field: 'ETA', width: '130px', title: 'ETA',
                template: "#=ETA==null?' ':Common.Date.ToString(ETA)#",
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
                field: 'LocationFromName', width: '150px', title: 'Điểm nhận hàng',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'LocationFromAddress', width: '200px', title: 'Địa chỉ nhận',
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
                field: 'LocationToName', width: '150px', title: 'Điểm giao hàng',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'LocationToAddress', width: '200px', title: 'Địa chỉ giao',
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
                        var item = _DIOptimizer_2View.Data._data2ViewSort[sort];
                        var tr = $(this).closest('tr');
                        if (Common.HasValue(item)) {
                            if (item.IsChange == true) {
                                if (!$(tr).hasClass('approved'))
                                    $(tr).addClass('approved');
                            }
                        }
                        if (!Common.HasValue(item)) {
                            item = _DIOptimizer_2View.Data._data2ViewSort[0];
                            _DIOptimizer_2View.Data._data2ViewSort[sort] = { 'CreateSortOrder': sort, 'VehicleID': item.VehicleID, 'TypeID': 0, 'VehicleNo': item.VehicleNo, 'ETD': Common.Date.FromJson(Date()), 'ETA': Common.Date.FromJson(Date()),'ID': 0, 'HasChanged': true };
                        }
                        var VehicleID = item.VehicleID != null ? item.VehicleID : '';
                        if (item.TypeID == 1) {
                            $(this).html('<input type="checkbox" class="chkChooseVehicle"/> Chuyến ' + sort + ' - ' +
                                ' <a href="" class="btnHasGroup">' + item.VehicleNo + ' - ' + Common.Date.FromJsonDMYHM(item.ETD) + ' - ' + Common.Date.FromJsonDMYHM(item.ETA) + '</a>' +
                                '<input style="display:none;width:100px" focus-k-combobox class="cboHasVehicle cus-combobox" placeholder="Đầu kéo" value="' + VehicleID + '" /><span style="display:none" class="lblHasSplit"> - </span>' +
                                '<span style="display:none" class="lblHasSplit"> - </span>' +
                                '<input style="display:none;width:160px" class="txtCreateDateTime_ETD" value="' + Common.Date.FromJsonDMYHM(item.ETD) + '"/>' + '<span style="display:none" class="lblHasSplit"> - </span>' +
                                '<input style="display:none;width:160px" class="txtCreateDateTime_ETA" value="' + Common.Date.FromJsonDMYHM(item.ETA) + '"/>');
                        }
                        else {
                            $(this).html('Chuyến ' + sort + ' - ' +
                               ' <a href="" class="btnHasGroup">' + item.VehicleNo + ' - ' + Common.Date.FromJsonDMYHM(item.ETD) + ' - ' + Common.Date.FromJsonDMYHM(item.ETA) + '</a>' +
                               '<input style="display:none;width:100px" focus-k-combobox class="cboHasVehicle cus-combobox" placeholder="Đầu kéo" value="' + VehicleID + '" /><span style="display:none" class="lblHasSplit"> - </span>' +
                               '<span style="display:none" class="lblHasSplit"> - </span>' +
                               '<input style="display:none;width:160px" class="txtCreateDateTime_ETD" value="' + Common.Date.FromJsonDMYHM(item.ETD) + '"/>' + '<span style="display:none" class="lblHasSplit"> - </span>' +
                               '<input style="display:none;width:160px" class="txtCreateDateTime_ETA" value="' + Common.Date.FromJsonDMYHM(item.ETA) + '"/>');
                        }
                    }
                });

                this.element.find('.HasGridGroup .btnHasGroup').click(function (e) {
                    e.preventDefault();
                    var tr = $(this).closest('tr');
                    var sort = parseInt($(tr).find('.HasGridGroup').attr('tabid'));
                    _DIOptimizer_2View.Data._data2ViewSort[sort].HasChanged = true;
                    $scope.IsShowSave = true;
                    $scope.Group_Click(this, sort);
                });

                this.element.find('.HasGridGroup .chkChooseVehicle').click(function (e) {
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
                    ETA: { type: 'date' },
                    IsGopSplit: { type: 'boolean' },
                }
            },
        }),
        height: '99%', groupable: false, pageable: false, columnMenu: false, sortable: false, resizable: true, reorderable: true, sortable: { mode: 'row' },
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
                  '<input type="checkbox" class="chkAdd" style="display:none"/>',Attributes: { style: 'text-align: center;' }, filterable: false, menu: false, sortable: false
            },
            {
                field: 'Ton', width: '80px', title: 'Tấn', template: function (dataItem) {
                    if (dataItem.TypeEditID == 1)
                        return '<form class="cus-form-enter"><input kendo-numeric-text-box value="{{dataItem.Ton}}" k-options="numericTonOptions" style="width:100%" /></form> ';
                    else return dataItem.Ton;
                },
                filterable: { cell: { operator: 'gte', showOperators: false } }
            },
            {
                field: 'CBM', width: '80px', title: 'Khối', template: function (dataItem) {
                    if (dataItem.TypeEditID == 2)
                        return '<form class="cus-form-enter"><input kendo-numeric-text-box value="{{dataItem.CBM}}" k-options="numericCBMOptions" style="width:100%" /></form>';
                    else return dataItem.CBM;
                },
                filterable: { cell: { operator: 'gte', showOperators: false } }
            },
            {
                field: 'Quantity', width: '80px', title: 'Sản lượng', template: function (dataItem) {
                    if (dataItem.TypeEditID == 3)
                        return '<form class="cus-form-enter"><input kendo-numeric-text-box value="{{dataItem.Quantity}}" k-options="numericQuantityOptions" style="width:100%" /></form> ';
                    else return dataItem.Quantity;
                },
                filterable: { cell: { operator: 'gte', showOperators: false } }
            },
            {
                field: 'DNCode', width: '100px', title: 'Số DN',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'ETDStart', width: '130px', title: 'BĐ ETD',
                template: "#=ETDStart==null?' ':Common.Date.ToString(ETDStart)#",
                filterable: {
                    cell: {
                        template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); },
                        operator: 'equal', showOperators: false
                    }
                },
            },
            {
                field: 'ETD', width: '130px', title: 'ETD',
                template: "#=ETD==null?' ':Common.Date.ToString(ETD)#",
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
                field: 'ETAStart', width: '130px', title: 'BĐ ETA',
                template: "#=ETAStart==null?' ':Common.Date.ToString(ETAStart)#",
                filterable: {
                    cell: {
                        template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); },
                        operator: 'equal', showOperators: false
                    }
                },
            },
            {
                field: 'ETA', width: '130px', title: 'ETA',
                template: "#=ETA==null?' ':Common.Date.ToString(ETA)#",
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
                field: 'LocationFromName', width: '150px', title: 'Điểm nhận hàng',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'LocationFromAddress', width: '200px', title: 'Địa chỉ nhận',
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
                field: 'LocationToName', width: '150px', title: 'Điểm giao hàng',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'LocationToAddress', width: '200px', title: 'Địa chỉ giao',
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

            if (Common.HasValue(this) && Common.HasValue(this.tbody)) {
                if (_DIOptimizer_2View.Data._dataGroupProduct.length == 0)
                    $scope.ReloadSort();
                this.element.find('tr[role="row"]').each(function () {
                    var btnAdd = $(this).find('.btnAdd');
                    var btnAddSave = $(this).find('.btnAddSave');
                    var chkAdd = $(this).find('.chkAdd');
                    if (Common.HasValue($scope.gridNo)) {
                        var dataItem = $scope.gridNo.dataItem(this);
                        if (Common.HasValue(dataItem)) {
                            if (_DIOptimizer_2View.Data._dataGroupProduct[dataItem.ID].length > 1)
                                btnAdd.show();
                        }
                    }
                });
            }
        },
    };

    $scope.ReloadSort = function ($event) {
        Common.Log("ReloadSort");
        $scope.IsShowUnMerge = false;
        var totalton = 0;
        var totalcbm = 0;
        var totalquan = 0;

        _DIOptimizer_2View.Data._dataGroupProduct = [];
        $.each(_DIOptimizer_2View.Data._data2View, function (i, v) {
            if (v.CreateSortOrder <= 0) {
                v.Ton = v.Ton > 0 ? Math.round(v.Ton * 1000000) / 1000000 : 0;
                v.CBM = v.CBM > 0 ? Math.round(v.CBM * 1000) / 1000 : 0;
                v.Quantity = v.Quantity > 0 ? Math.round(v.Quantity * 1000) / 1000 : 0;
                if (!Common.HasValue(_DIOptimizer_2View.Data._dataGroupProduct[v.ID]))
                    _DIOptimizer_2View.Data._dataGroupProduct[v.ID] = [];
                _DIOptimizer_2View.Data._dataGroupProduct[v.ID].push(v);
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
        $.each(_DIOptimizer_2View.Data._dataGroupProduct[dataItem.ID], function (i, v) {
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
        $.each(_DIOptimizer_2View.Data._dataGroupProduct[dataItem.ID], function (i, v) {
            if (v.uid != dataItem.uid) {
                var tr = grid.tbody.find("tr[data-uid='" + v.uid + "']");
                var chkAdd = $(tr).find('.chkAdd');

                if ($(chkAdd).prop('checked') == true) {
                    if (dataItem.TypeEditID == 1) {
                        flat++;
                        dataItem.Ton += v.Ton;
                        dataItem.CBM += v.CBM;
                        dataItem.Quantity += v.Quantity;
                    } else {
                        if (dataItem.TypeEditID == 2) {
                            flat++;
                            dataItem.CBM += v.CBM;
                            dataItem.Ton += v.Ton;
                            dataItem.Quantity += v.Quantity;
                        } else {
                            if (dataItem.TypeEditID == 3) {
                                flat++;
                                dataItem.Quantity += v.Quantity;
                                dataItem.Ton += v.Ton;
                                dataItem.CBM += v.CBM;
                            }
                        }
                    }
                    if (_DIOptimizer_2View.Data._dataGroupProduct[dataItem.ID].count == flat) {
                        dataItem.IsSplit = false;
                    }
                    var index = _DIOptimizer_2View.Data._data2View.indexOf(v);
                    _DIOptimizer_2View.Data._data2View.splice(index, 1);
                }
            }
        });
        $scope.ReloadSort();
        grid.dataSource.data(_DIOptimizer_2View.Data._data2View);
    };

    $scope.MergeCancel_Click = function ($event, grid) {
        Common.Log("MergeCancel_Click");
        $event.preventDefault();
        grid.dataSource.data(_DIOptimizer_2View.Data._data2View);
        $scope.IsShowCombine = false;
        $scope.IsShowUnMerge = false;
    };

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
            if (this.value() > 0) {
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
                    _DIOptimizer_2View.Data._data2View = $scope.gridNo.dataSource.data();
                    var index = _DIOptimizer_2View.Data._data2View.indexOf(dataItem);
                    _DIOptimizer_2View.Data._data2View.splice(index, 0, itemsub);
                    $scope.ReloadSort();
                    $scope.gridNo.dataSource.data(_DIOptimizer_2View.Data._data2View);

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
                    _DIOptimizer_2View.Data._data2View = $scope.gridNo.dataSource.data();
                    var index = _DIOptimizer_2View.Data._data2View.indexOf(dataItem);
                    _DIOptimizer_2View.Data._data2View.splice(index, 0, itemsub);
                    $scope.ReloadSort();
                    $scope.gridNo.dataSource.data(_DIOptimizer_2View.Data._data2View);
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
                    _DIOptimizer_2View.Data._data2View = $scope.gridNo.dataSource.data();
                    var index = _DIOptimizer_2View.Data._data2View.indexOf(dataItem);
                    _DIOptimizer_2View.Data._data2View.splice(index, 0, itemsub);
                    $scope.ReloadSort();
                    $scope.gridNo.dataSource.data(_DIOptimizer_2View.Data._data2View);
                }
            }
        }
    };

    $scope.CheckBox_All_Check = function ($event, grid) {

        var checked = $event.currentTarget.checked;
        if (checked == true) {
            grid.tbody.find('.HasGridGroup').each(function () {
                var CheckBox = $(this).find('input.chkChooseVehicle');
                CheckBox.prop('checked', true);
            });
            $scope.HasChoose = true;
        }
        else {
            grid.tbody.find('.HasGridGroup').each(function () {
                var CheckBox = $(this).find('input.chkChooseVehicle');
                CheckBox.prop('checked', false);
            });
            $scope.HasChoose = false;
        }
        
    }

    $scope.ReloadButton = function () {
        var flat = 0;
        $.each($scope.gridNoMaster.tbody.find('.HasGridGroup .chkChooseVehicle'), function (i, v) {
            if (this.checked) {
                flat = 1;
            }
        });
        if(flat == 1)
            $scope.HasChoose = true;
        else
            $scope.HasChoose = false;
    };

    $scope.Delete_Click = function($event)
    {
        $event.preventDefault();
        var data = [];
        $scope.gridNoMaster.tbody.find('.HasGridGroup').each(function () {
            var sort = parseInt($(this).attr('tabid'));
            var item = _DIOptimizer_2View.Data._data2ViewSort[sort];
            var tr = $(this).closest('tr');
            if (sort > 0 && item.ID > 0) {
                var chk = $(tr).find('.chkChooseVehicle');
                if (chk.prop('checked') == true)
                    data.push(item.ID);
            }
        });

        if (Common.HasValue(data)) {
            Common.Services.Call($http, {
                url: Common.Services.url.OPS,
                method: _DIOptimizer_2View.URL.Delete,
                data: { optimizerID: $scope.OptimizerID, data: data },
                success: function (res) {
                    $scope.LoadData();
                    $rootScope.Message({
                        Msg: 'Đã xóa.',
                        NotifyType: Common.Message.NotifyType.SUCCESS
                    });
                }
            });

        }
    }

    $scope.LoadData = function () {
        Common.Log('LoadData');
        $rootScope.IsLoading = true;
        var paramCo = Common.Request.Create({
            Sorts: [], Filters: []
        });
        Common.Services.Call($http, {
            url: Common.Services.url.OPS,
            method: _DIOptimizer_2View.URL.ListVehicle,
            data: { optimizerID: $scope.OptimizerID, request: "" },
            success: function (res) {
                _DIOptimizer_2View.Data._data2ViewVehicle = res.Data;

                Common.Services.Call($http, {
                    url: Common.Services.url.OPS,
                    method: _DIOptimizer_2View.URL.Container_List,
                    data: { request: paramCo, optimizerID: $scope.OptimizerID, hasMaster: true },
                    success: function (res) {
                        $scope.totalTonNoMaster = 0;
                        _DIOptimizer_2View.Data._data2ViewMaster = res.Data;

                        Common.Services.Call($http, {
                            url: Common.Services.url.OPS,
                            method: _DIOptimizer_2View.URL.ListMaster,
                            data: { request: paramCo, optimizerID: $scope.OptimizerID },
                            success: function (res) {
                                _DIOptimizer_2View.Data._data2ViewSort[0] = { 'CreateSortOrder': 0, 'VehicleID': _DIOptimizer_2View.Data._data2ViewVehicle[0].VehicleID, 'TypeID': 0, 'VehicleNo': _DIOptimizer_2View.Data._data2ViewVehicle[0].VehicleNo, 'ETD': Common.Date.FromJson(Date()), 'ETA': Common.Date.FromJson(Date()), 'ID': 0, 'IsChange': false, 'TotalTon': 0 };
                                $.each(res.Data, function (i, v) {
                                    v.TypeID = 1;
                                    _DIOptimizer_2View.Data._data2ViewSort[v.CreateSortOrder] = v;
                                    $.each(_DIOptimizer_2View.Data._data2ViewMaster, function (i, m) {
                                        if (m.OPTDITOMasterID == v.ID) {
                                            m.CreateSortOrder = v.CreateSortOrder;
                                        }
                                    });
                                });
                                $scope.gridNoMasterOptions.dataSource.data(_DIOptimizer_2View.Data._data2ViewMaster);
                            }
                        });

                        $rootScope.IsLoading = false;
                    }
                });
            }
        });
        
        Common.Services.Call($http, {
            url: Common.Services.url.OPS,
            method: _DIOptimizer_2View.URL.Container_List,
            data: { request: paramCo, optimizerID: $scope.OptimizerID, hasMaster: false },
            success: function (res) {
                $scope.totalTonNoMaster = 0;
                _DIOptimizer_2View.Data._data2View = res.Data;
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
                if (Common.HasValue(_DIOptimizer_2View.Data._data2ViewSort[sort]))
                    _DIOptimizer_2View.Data._data2ViewSort[sort].HasChanged = true;
                var tr = $(e.sender.element).closest('tr');
                var dataItem = $scope.gridNo.dataItem(tr);
                // Add vào grid chuyến, remove khỏi grid đơn hàng
                dataItem.CreateSortOrder = sort;

                // Add vào grid chuyến
                _DIOptimizer_2View.Data._data2ViewMaster = [];
                _DIOptimizer_2View.Data._data2ViewMaster = $.extend(true, [], $scope.gridNoMaster.dataSource.data());
                _DIOptimizer_2View.Data._data2ViewMaster.push(dataItem);
                // remove khỏi grid đơn hàng
                $scope.gridNo.removeRow(tr);
                $timeout(function () {
                    $scope.IsShowSave = true;
                    $scope.gridNoMaster.dataSource.data(_DIOptimizer_2View.Data._data2ViewMaster);
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
            _DIOptimizer_2View.Data._data2ViewSort[sortOld].HasChanged = true;
            if (Common.HasValue(dataItem)) {
                // remove khỏi grid chuyến, add vào grid đơn hàng
                if (sort <= 0) {
                    dataItem.CreateSortOrder = 0;
                    // Add vào grid đơn hàng
                    _DIOptimizer_2View.Data._data2View = $.extend(true, [], $scope.gridNo.dataSource.data());
                    _DIOptimizer_2View.Data._data2View.push(dataItem);
                    $scope.gridNo.dataSource.data(_DIOptimizer_2View.Data._data2View);
                    // remove khỏi grid chuyến
                    $scope.gridNoMaster.removeRow(tr);
                } else {
                    dataItem.CreateSortOrder = sort;
                    _DIOptimizer_2View.Data._data2ViewSort[sort].HasChanged = true;
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
        $.each(_DIOptimizer_2View.Data._data2ViewSort, function (i, v) {
            if (Common.HasValue(v) && v.CreateSortOrder > 0) {
                data.push(v);
            }
        });

        Common.Services.Call($http, {
            url: Common.Services.url.OPS,
            method: _DIOptimizer_2View.URL.Save,
            data: { optimizerID: $scope.OptimizerID, dataMaster: data, dataContainer: grid.dataSource.data() },
            success: function (res) {
                $rootScope.Message({
                    Msg: 'Thêm thành công.',
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
                _DIOptimizer_2View.Data._data2ViewSort[sort].ETD = e.sender.value();
                _DIOptimizer_2View.Data._data2ViewSort[sort].IsChange = true;
            },

        });
        $rootScope.FocusKDateTimePicker(Time_ETD);

        var Time_ETA = tr.find('input.txtCreateDateTime_ETA');
        Time_ETA.kendoDateTimePicker({
            format: Common.Date.Format.DMYHM,
            timeFormat: Common.Date.Format.HM,
            change: function (e) {
                var EditDate = new Date(e.sender.value());
                _DIOptimizer_2View.Data._data2ViewSort[sort].ETA = e.sender.value();
                _DIOptimizer_2View.Data._data2ViewSort[sort].IsChange = true;
            },

        });
        $rootScope.FocusKDateTimePicker(Time_ETA);

        var cboVehicle = tr.find('input.cboHasVehicle');
        cboVehicle.kendoComboBox({
            index: 0, autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true,
            dataTextField: 'VehicleNo', dataValueField: 'VehicleID',
            dataSource:
                Common.DataSource.Local({
                    data: _DIOptimizer_2View.Data._data2ViewVehicle,
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
                _DIOptimizer_2View.Data._data2ViewSort[sort].VehicleNo = text;
                _DIOptimizer_2View.Data._data2ViewSort[sort].VehicleID = ID;
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
        $timeout(function () {
            if ($scope.IsExpand) {
                angular.forEach(grid.element.find('.k-grouping-row'), function (o) {
                    grid.expandGroup(o);
                })
                $($event.currentTarget).find("i").removeClass("fa-plus");
                $($event.currentTarget).find("i").addClass("fa-minus");
            } else {
                angular.forEach(grid.element.find('.k-grouping-row'), function (o) {
                    grid.collapseGroup(o);
                })
                $($event.currentTarget).find("i").removeClass("fa-minus");
                $($event.currentTarget).find("i").addClass("fa-plus");
            }
        }, 10)
    };

   
    $scope.Close_Click = function ($event, win) {
        $event.preventDefault();

        win.close();
    }
}]);