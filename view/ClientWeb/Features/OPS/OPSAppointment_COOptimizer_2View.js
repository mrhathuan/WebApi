/// <reference path="~/Scripts/jquery-1.9.1.intellisense.js" />
/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />

var _COOptimizer_2View = {
    URL: {
        List: "Opt_COTOContainer_List",
        Container_List: "Opt_2ViewCO_Container_List",
        Delete: "Opt_2ViewCO_Delete",
        Run: "Opt_Optimizer_Run",
        ListMaster: "Opt_2View_COTOMaster_List",
        Save: "Opt_2ViewCO_SaveList",
        VehicleListTractor: 'Opt_Vehicle_List',
        VehicleListRomooc: 'Opt_Romooc_List'
    },
    Data: {
        _dataHas: [],
        _dataHasMaster: [],
        _dataHasSort: [],
        _dataHasTractor: [],
        _dataHasRommoc: [],
        _dataGroupProduct:[],
        _formatMoney: "n0",
    }
}

angular.module('myapp').controller('OPSAppointment_COOptimizer_2ViewCtrl', ['$rootScope', '$scope', '$http', '$location', '$state', '$timeout', 'openMap', function ($rootScope, $scope, $http, $location, $state, $timeout, openMap) {
    if ($rootScope.IsPageComplete != true) return;
    Common.Log('OPSAppointment_COOptimizer_2ViewCtrl');
    $rootScope.IsLoading = false;
    $scope.OptimizerID = parseInt($state.params.OptimizerID);
    $scope.OptimizerName = ""; 
    $scope.HasChoose = false;
    $scope.IsExpand = true;
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
                                $scope.ResetView();
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
            { field: 'ContainerNo', width: '80px', title: 'Số Con.', sortable: false, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'OrderCode', width: '90px', title: 'Mã ĐH', sortable: false, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'ServiceOfOrderName', width: '100px', title: 'Loại v.chuyển', sortable: false, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'CustomerCode', width: '90px', title: 'KH', sortable: false, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'PackingName', width: '90px', title: 'Loại Con', sortable: false, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'StatusOfCOContainerName', width: '90px', title: 'Tình trạng', sortable: false, filterable: { cell: { operator: 'contains', showOperators: false } } },            
            { field: 'Ton', width: '80px', title: 'Trọng tải', template: '', sortable: false, filterable: { cell: { operator: 'gte', showOperators: false } } },
            { field: 'SealNo1', width: '90px', title: 'Số Seal1', sortable: false, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'SealNo2', width: '90px', title: 'Số Seal2', sortable: false, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'Note', width: '90px', title: 'Ghi chú', sortable: false, filterable: { cell: { operator: 'contains', showOperators: false } } },
            {
                field: 'ETD', width: '110px', title: 'ETD',
                template: "#=ETD != null ? Common.Date.FromJsonDMYHM(ETD) : ''#", filterable: {
                    cell: {
                        template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); },
                        operator: 'equal', showOperators: false
                    }
                }
            },
            {
                field: 'ETA', width: '110px', title: 'ETA',
                template: "#=ETA != null ? Common.Date.FromJsonDMYHM(ETA) : ''#",
                filterable: {
                    cell: {
                        template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); },
                        operator: 'equal', showOperators: false
                    }
                }
            },
            { field: 'LocationFromName', width: '100px', title: 'Điểm giao', sortable: false, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'LocationToName', width: '100px', title: 'Điểm nhận', sortable: false, filterable: { cell: { operator: 'contains', showOperators: false } } },
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
                        var item = _COOptimizer_2View.Data._dataHasSort[sort];
                        var tr = $(this).closest('tr');
                        if (Common.HasValue(item)) {
                            if (item.IsChange == true) {
                                if (!$(tr).hasClass('approved'))
                                    $(tr).addClass('approved');
                            }
                        }
                        if (!Common.HasValue(item)) {
                            item = _COOptimizer_2View.Data._dataHasSort[0];
                            _COOptimizer_2View.Data._dataHasSort[sort] = { 'CreateSortOrder': sort, 'VehicleID': item.VehicleID, 'TypeID': 0, 'VehicleNo': item.VehicleNo, 'RomoocID': item.RomoocID, 'RomoocNo': item.RomoocNo, 'DriverName1': '', DriverTel1: '', 'ETD': Common.Date.FromJson(Date()), 'ETA': Common.Date.FromJson(Date()), 'VendorOfVehicleID': -1, 'VendorOfVehicleName': "Xe nhà", 'ID': 0, 'IsChange': false, 'TotalTon': 0, 'HasChanged': true };
                        }
                        var VehicleID = item.VehicleID != null ? item.VehicleID : '';
                        var RomoocID = item.RomoocID != null ? item.RomoocID : '';
                        if (item.TypeID == 1) {
                            $(this).html('<input type="checkbox" class="chkChooseVehicle"/> Chuyến ' + sort + ' - ' +
                               ' <a href="" class="btnHasGroup">' + item.VehicleNo + ' - ' + item.RomoocNo + ' - ' + Common.Date.FromJsonDMYHM(item.ETD) + ' - ' + Common.Date.FromJsonDMYHM(item.ETA) + '</a>' +
                               '<input style="display:none;width:100px" focus-k-combobox class="cboHasTractor cus-combobox" placeholder="Đầu kéo" value="' + VehicleID + '" /><span style="display:none" class="lblHasSplit"> - </span>' +
                               '<input style="display:none;width:100px" focus-k-combobox class="cboHasRomooc cus-combobox" placeholder="Romooc" value="' + RomoocID + '" /><span style="display:none" class="lblHasSplit"> - </span>' +
                               '<input style="display:none;width:160px" class="txtCreateDateTime_ETD" value="' + Common.Date.FromJsonDMYHM(item.ETD) + '"/>' + '<span style="display:none" class="lblHasSplit"> - </span>' +
                               '<input style="display:none;width:160px" class="txtCreateDateTime_ETA" value="' + Common.Date.FromJsonDMYHM(item.ETA) + '"/>');
                        }
                        else {
                            $(this).html('Chuyến ' + sort + ' - ' +
                               ' <a href="" class="btnHasGroup">' + item.VehicleNo + ' - ' + item.RomoocNo + ' - ' + Common.Date.FromJsonDMYHM(item.ETD) + ' - ' + Common.Date.FromJsonDMYHM(item.ETA) + '</a>' +
                               '<input style="display:none;width:100px" focus-k-combobox class="cboHasTractor cus-combobox" placeholder="Đầu kéo" value="' + VehicleID + '" /><span style="display:none" class="lblHasSplit"> - </span>' +
                               '<input style="display:none;width:100px" focus-k-combobox class="cboHasRomooc cus-combobox" placeholder="Romooc" value="' + RomoocID + '" /><span style="display:none" class="lblHasSplit"> - </span>' +
                               '<input style="display:none;width:160px" class="txtCreateDateTime_ETD" value="' + Common.Date.FromJsonDMYHM(item.ETD) + '"/>' + '<span style="display:none" class="lblHasSplit"> - </span>' +
                               '<input style="display:none;width:160px" class="txtCreateDateTime_ETA" value="' + Common.Date.FromJsonDMYHM(item.ETA) + '"/>');
                        }

                    }
                });

                this.element.find('.HasGridGroup .btnHasGroup').click(function (e) {
                    e.preventDefault();
                    var tr = $(this).closest('tr');
                    var sort = parseInt($(tr).find('.HasGridGroup').attr('tabid'));
                    _COOptimizer_2View.Data._dataHasSort[sort].HasChanged = true;
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
                    IsGopSplit: { type: 'boolean' },
                    ETD: { type: 'date' },
                    ETA: { type: 'date' },
                }
            },
        }),
        height: '99%', groupable: false, pageable: false, columnMenu: false, sortable: false, resizable: true, reorderable: true, sortable: { mode: 'row' },
        columns: [
            { field: 'CreateSortOrder', width: '50px', template: '<form class="cus-form-enter" ng-submit="SortEnter_Click($event)"><input kendo-numeric-text-box class="txtCreateSortOrder" value="#=CreateSortOrder>0?CreateSortOrder:0#" data-k-min="0" k-options="numericSortOrderOptions" style="width:100%" /></form>', headerTemplate: '<a class="k-button" href="/"><i class="fa"></i></a>', sortable: false, filterable: false, menu: false },
            { field: 'ContainerNo', width: '90px', title: 'Số Con.', sortable: false, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'OrderCode', width: '90px', title: 'Mã ĐH', sortable: false, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'CustomerCode', width: '90px', title: 'KH', sortable: false, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'ServiceOfOrderName', width: '100px', title: 'Loại v.chuyển', sortable: false, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'PackingName', width: '90px', title: 'Loại Con', sortable: false, filterable: { cell: { operator: 'contains', showOperators: false } } },          
            { field: 'Ton', width: '80px', title: 'Trọng tải', filterable: { cell: { operator: 'gte', showOperators: false } } },
            { field: 'StatusOfCOContainerName', width: '90px', title: 'Tình trạng', sortable: false, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'SealNo1', width: '90px', title: 'Số Seal1', sortable: false, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'SealNo2', width: '90px', title: 'Số Seal2', sortable: false, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'Note', width: '90px', title: 'Ghi chú', sortable: false, filterable: { cell: { operator: 'contains', showOperators: false } } },
            {
                field: 'ETD', width: '100px', title: 'ETD',
                template: "#=ETD != null ? Common.Date.FromJsonDMYHM(ETD) : ''#", filterable: {
                    cell: {
                        template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); },
                        operator: 'equal', showOperators: false
                    }
                }
            },
            {
                field: 'ETA', width: '100px', title: 'ETA',
                template: "#=ETA != null ? Common.Date.FromJsonDMYHM(ETA) : ''#",
                filterable: {
                    cell: {
                        template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); },
                        operator: 'equal', showOperators: false
                    }
                }
            },
             { field: 'LocationFromName', width: '100px', title: 'Điểm giao', sortable: false, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'LocationToName', width: '100px', title: 'Điểm nhận', sortable: false, filterable: { cell: { operator: 'contains', showOperators: false } } },
             { title: ' ', filterable: false, menu: false, sortable: false }
        ],
        filterable: { mode: 'row' },
        dataBound: function () {
            Common.Log("GridDataBound");

            var grid = this;
            var h = Common.Cookie.Get("Scroll");
            grid.wrapper.find('.k-grid-content').scrollTop(h);

            if (Common.HasValue(this) && Common.HasValue(this.tbody)) {
                if (_COOptimizer_2View.Data._dataGroupProduct.length == 0)
                    $scope.ReloadSort();
                this.element.find('tr[role="row"]').each(function () {
                    var btnAdd = $(this).find('.btnAdd');
                    var btnAddSave = $(this).find('.btnAddSave');
                    var chkAdd = $(this).find('.chkAdd');
                    if (Common.HasValue($scope.gridNo)) {
                        var dataItem = $scope.gridNo.dataItem(this);
                        if (Common.HasValue(dataItem)) {
                            if (_COOptimizer_2View.Data._dataGroupProduct[dataItem.ID].length > 1)
                                btnAdd.show();
                        }
                    }
                });
            }
        },
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

    $scope.ReloadSort = function ($event) {
        Common.Log("ReloadSort");
        $scope.IsShowUnMerge = false;
        var totalton = 0;
        var totalcbm = 0;
        var totalquan = 0;

        _COOptimizer_2View.Data._dataGroupProduct = [];
        $.each(_COOptimizer_2View.Data._dataHas, function (i, v) {
            if (v.CreateSortOrder <= 0) {
                v.Ton = v.Ton > 0 ? Math.round(v.Ton * 1000000) / 1000000 : 0;
                if (!Common.HasValue(_COOptimizer_2View.Data._dataGroupProduct[v.ID]))
                    _COOptimizer_2View.Data._dataGroupProduct[v.ID] = [];
                _COOptimizer_2View.Data._dataGroupProduct[v.ID].push(v);
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
        $.each(_COOptimizer_2View.Data._dataGroupProduct[dataItem.ID], function (i, v) {
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
        $.each(_COOptimizer_2View.Data._dataGroupProduct[dataItem.ID], function (i, v) {
            if (v.uid != dataItem.uid) {
                var tr = grid.tbody.find("tr[data-uid='" + v.uid + "']");
                var chkAdd = $(tr).find('.chkAdd');

                if ($(chkAdd).prop('checked') == true) {
                    flat++;
                    dataItem.Ton += v.Ton;
                    if (_COOptimizer_2View.Data._dataGroupProduct[dataItem.ID].count == flat) {
                        dataItem.IsSplit = false;
                    }
                    var index = _COOptimizer_2View.Data._dataHas.indexOf(v);
                    _COOptimizer_2View.Data._dataHas.splice(index, 1);
                }
            }
        });
        $scope.ReloadSort();
        grid.dataSource.data(_COOptimizer_2View.Data._dataHas);
    };

    $scope.MergeCancel_Click = function ($event, grid) {
        Common.Log("MergeCancel_Click");
        $event.preventDefault();
        grid.dataSource.data(_COOptimizer_2View.Data._dataHas);
        $scope.IsShowCombine = false;
        $scope.IsShowUnMerge = false;
    };
    
    $scope.ReloadButton = function () {
        var flat = 0;
        $.each($scope.gridNoMaster.tbody.find('.HasGridGroup .chkChooseVehicle'), function (i, v) {
            if (this.checked) {
                flat = 1;
            }
        });
        if (flat == 1)
            $scope.HasChoose = true;
        else
            $scope.HasChoose = false;
    };

    $scope.Delete_Click = function ($event) {
        $event.preventDefault();
        var data = [];
        $scope.gridNoMaster.tbody.find('.HasGridGroup').each(function () {
            var sort = parseInt($(this).attr('tabid'));
            var item = _COOptimizer_2View.Data._dataHasSort[sort];
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
                method: _COOptimizer_2View.URL.Delete,
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
            method: _COOptimizer_2View.URL.VehicleListTractor,
            data: { optimizerID: $scope.OptimizerID, request: "" },
            success: function (res) {
                _COOptimizer_2View.Data._dataHasTractor = res.Data;

                Common.Services.Call($http, {
                    url: Common.Services.url.OPS,
                    method: _COOptimizer_2View.URL.VehicleListRomooc,
                    data: { optimizerID: $scope.OptimizerID, request: "" },
                    success: function (res) {
                        _COOptimizer_2View.Data._dataHasRommoc = res.Data;

                        Common.Services.Call($http, {
                            url: Common.Services.url.OPS,
                            method: _COOptimizer_2View.URL.Container_List,
                            data: { request: paramCo, optimizerID: $scope.OptimizerID, hasMaster: true },
                            success: function (res) {
                                $scope.totalTonNoMaster = 0;
                                _COOptimizer_2View.Data._dataHasMaster = res.Data;

                                Common.Services.Call($http, {
                                    url: Common.Services.url.OPS,
                                    method: _COOptimizer_2View.URL.ListMaster,
                                    data: { request: paramCo, optimizerID: $scope.OptimizerID },
                                    success: function (res) {
                                        _COOptimizer_2View.Data._dataHasSort[0] = { 'CreateSortOrder': 0, 'VehicleID': _COOptimizer_2View.Data._dataHasTractor[0].VehicleID, 'TypeID': 0, 'VehicleNo': _COOptimizer_2View.Data._dataHasTractor[0].VehicleNo, 'RomoocID': _COOptimizer_2View.Data._dataHasRommoc[0].RomoocID, 'RomoocNo': _COOptimizer_2View.Data._dataHasRommoc[0].RomoocNo, 'DriverName1': '', 'DriverTel1': '', 'ETD': Common.Date.FromJson(Date()), 'ETA': Common.Date.FromJson(Date()), 'VendorOfVehicleID': -1, 'VendorOfVehicleName': "Xe nhà", 'ID': 0, 'IsChange': false, 'TotalTon': 0 };
                                        $.each(res.Data, function (i, v) {
                                            v.TypeID = 1;
                                            _COOptimizer_2View.Data._dataHasSort[v.CreateSortOrder] = v;
                                            $.each(_COOptimizer_2View.Data._dataHasMaster, function (i, m) {
                                                if (m.OPTCOTOMasterID == v.ID) {
                                                    m.CreateSortOrder = v.CreateSortOrder;
                                                }
                                            });
                                        });
                                        $scope.gridNoMasterOptions.dataSource.data(_COOptimizer_2View.Data._dataHasMaster);
                                    }
                                });

                                $rootScope.IsLoading = false;
                            }
                        });
                    }
                });
            }
        });

        Common.Services.Call($http, {
            url: Common.Services.url.OPS,
            method: _COOptimizer_2View.URL.Container_List,
            data: { request: paramCo, optimizerID: $scope.OptimizerID, hasMaster: false },
            success: function (res) {
                $scope.totalTonNoMaster = 0;
                _COOptimizer_2View.Data._dataHas = res.Data;
                $.each(res.Data, function (i, v) {
                    if (v.CreateSortOrder <= 0) {
                        $scope.totalTonNoMaster = $scope.totalTonNoMaster + v.Ton;
                    }
                });
                $scope.gridNoOptions.dataSource.data(res.Data);
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
                if (Common.HasValue(_COOptimizer_2View.Data._dataHasSort[sort]))
                    _COOptimizer_2View.Data._dataHasSort[sort].HasChanged = true;
                var tr = $(e.sender.element).closest('tr');
                var dataItem = $scope.gridNo.dataItem(tr);
                // Add vào grid chuyến, remove khỏi grid đơn hàng
                dataItem.CreateSortOrder = sort;

                // Add vào grid chuyến
                _COOptimizer_2View.Data._dataHasMaster = [];
                _COOptimizer_2View.Data._dataHasMaster = $.extend(true, [], $scope.gridNoMaster.dataSource.data());
                _COOptimizer_2View.Data._dataHasMaster.push(dataItem);
                // remove khỏi grid đơn hàng
                $scope.gridNo.removeRow(tr);
                $timeout(function () {
                    $scope.IsShowSave = true;
                    $scope.gridNoMaster.dataSource.data(_COOptimizer_2View.Data._dataHasMaster);
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
            _COOptimizer_2View.Data._dataHasSort[sortOld].HasChanged = true;
            if (Common.HasValue(dataItem)) {
                // remove khỏi grid chuyến, add vào grid đơn hàng
                if (sort <= 0) {
                    dataItem.CreateSortOrder = 0;
                    // Add vào grid đơn hàng
                    _COOptimizer_2View.Data._dataHas = $.extend(true, [], $scope.gridNo.dataSource.data());
                    _COOptimizer_2View.Data._dataHas.push(dataItem);
                    $scope.gridNo.dataSource.data(_COOptimizer_2View.Data._dataHas);
                    // remove khỏi grid chuyến
                    $scope.gridNoMaster.removeRow(tr);
                } else {
                    dataItem.CreateSortOrder = sort;
                    _COOptimizer_2View.Data._dataHasSort[sort].HasChanged = true;
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
        $.each(_COOptimizer_2View.Data._dataHasSort, function (i, v) {
            if(Common.HasValue(v) && v.CreateSortOrder > 0)
            {
                data.push(v);
            }
        });
        
        Common.Services.Call($http, {
            url: Common.Services.url.OPS,
            method: _COOptimizer_2View.URL.Save,
            data: { optimizerID: $scope.OptimizerID, dataMaster: data, dataContainer: grid.dataSource.data() },
            success: function (res) {
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
                _COOptimizer_2View.Data._dataHasSort[sort].ETD = e.sender.value();
                _COOptimizer_2View.Data._dataHasSort[sort].IsChange = true;
            },

        });
        $rootScope.FocusKDateTimePicker(Time_ETD);

        var Time_ETA = tr.find('input.txtCreateDateTime_ETA');
        Time_ETA.kendoDateTimePicker({
            format: Common.Date.Format.DMYHM,
            timeFormat: Common.Date.Format.HM,
            change: function (e) {
                var EditDate = new Date(e.sender.value());
                _COOptimizer_2View.Data._dataHasSort[sort].ETA = e.sender.value();
            },

        });
        $rootScope.FocusKDateTimePicker(Time_ETA);

        var cboTractor = tr.find('input.cboHasTractor');
        cboTractor.kendoComboBox({
            index: 0, autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true,
            dataTextField: 'VehicleNo', dataValueField: 'VehicleID',
            dataSource:
                Common.DataSource.Local({
                    data: _COOptimizer_2View.Data._dataHasTractor,
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
                _COOptimizer_2View.Data._dataHasSort[sort].VehicleNo = text;
                _COOptimizer_2View.Data._dataHasSort[sort].VehicleID = ID;
            }

        });
        $rootScope.FocusKCombobox(cboTractor);

        var cboRomooc = tr.find('input.cboHasRomooc');
        cboRomooc.kendoComboBox({
            index: 0, autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true,
            dataTextField: 'RomoocNo', dataValueField: 'RomoocID',
            dataSource: Common.DataSource.Local({
                data: _COOptimizer_2View.Data._dataHasRommoc,
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
                _COOptimizer_2View.Data._dataHasSort[sort].IsChange = true;
                _COOptimizer_2View.Data._dataHasSort[sort].RomoocNo = text;
                _COOptimizer_2View.Data._dataHasSort[sort].RomoocID = ID;
            },

        });
        $rootScope.FocusKCombobox(cboRomooc);
    };

    $scope.Expand_Click = function ($event, grid) {
        Common.Log('Expand_Click');
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

    $scope.Back_click = function ($event) {
        $event.preventDefault();
        $state.go("main.OPSAppointment.COOptimizer_Master", { OptimizerID: $scope.OptimizerID });
    }

    $scope.Close_Click = function ($event, win) {
        $event.preventDefault();

        win.close();
    }
}]);