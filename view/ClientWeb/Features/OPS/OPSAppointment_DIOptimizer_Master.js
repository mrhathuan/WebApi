/// <reference path="~/Scripts/jquery-1.9.1.intellisense.js" />
/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />

var _DIOptimizer_Master = {
    URL: {
        List: "Opt_DITOMaster_List",
        Master_Container_List: "Opt_DITOMaster_GroupProduct_List",

        Run: "Opt_Optimizer_Run",
        Cal: "Opt_Optimizer_Cal",
        Out: "Opt_Optimizer_Out",
        ToMon: "Opt_DITOMaster_Save",
        JsonSet: "Opt_Optimizer_GetJsonSet",
        Change: "Opt_DITOMaster_Change",
        Delete: "Opt_DITOMaster_Delete",
        Romooc_Data: "Opt_Romooc_List",
        Vehicle_Data: "Opt_Vehicle_List",
        Optimizer_Get: 'Opt_Optimizer_Get',

        CheckRun: "Opt_Optmizer_HasRun",
        CheckSave: "Opt_Optmizer_HasSave",
        Location_Data: "Opt_DITOLocation_List",
        Check_Setting: "Opt_Optimizer_Run_Check_Setting",
        Check_Vehicle: "Opt_Optimizer_Run_Check_Vehicle",
        Check_Location: "Opt_Optimizer_Run_Check_Location",
        Location_Matrix_List: "Opt_Optimizer_Run_Get_LocationMatrix",
        Location_Matrix_Save: "Opt_Optimizer_Run_Update_LocationMatrix"
    },
    Data: {
        Location: [],
        MasterLocation: {},
        Count: 0
    }
}

angular.module('myapp').controller('OPSAppointment_DIOptimizer_MasterCtrl', ['$rootScope', '$scope', '$http', '$location', '$state', '$timeout', 'openMapV2', function ($rootScope, $scope, $http, $location, $state, $timeout, openMapV2) {
    if ($rootScope.IsPageComplete != true) return;
    Common.Log('OPSAppointment_DIOptimizer_MasterCtrl');

    $scope.OptimizerID = parseInt($state.params.OptimizerID);
    $scope.OptimizerName = "";
    $scope.StatusOfOptimizer = 0;
    $scope.OptimizerClosed = false;
    $scope.HasChoose = false;
    $scope.DITOMaster = { ID: -1, Note: '', ETA: null, ETD: null, VehicleNo: "", VehicleID: "" };

    try {
        var objCookie = JSON.parse(Common.Cookie.Get("OPSDIOptimizer"));
        if (Common.HasValue(objCookie)) {
            if (objCookie.ID != $scope.OptimizerID) {
                Common.Services.Call($http, {
                    url: Common.Services.url.OPS,
                    method: _DIOptimizer_GroupOfProduct.URL.Optimizer_Get,
                    data: {
                        optimizerID: $scope.OptimizerID
                    },
                    success: function (res) {
                        Common.Services.Error(res, function (res) {
                            if (res.ID > 0) {
                                Common.Cookie.Set("OPSDIOptimizer", JSON.stringify(res));
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
                $timeout(function () {
                    $scope.ResetView();
                }, 100)
            }
        }
    }
    catch (e) { }

    $scope.verSplitter_Options = {
        orientation: "vertical",
        panes: [
            { collapsible: true, resizable: true, size: '50%' },
            { collapsible: true, resizable: true, size: '50%', collapsed: false }
        ],
        resize: function (e) {
            try {
                openMapV2.Resize();
            }
            catch (e) { }
        }
    }

    openMapV2.Init({
        Element: 'map',
        Tooltip_Show: true,
        Tooltip_Element: 'map_tooltip',
        InfoWin_Show: false,
        DefinedLayer: [{
            Name: 'VectorMarker',
            zIndex: 100
        }, {
            Name: 'VectorRoute',
            zIndex: 90
        }]
    });

    $scope.master_gridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.OPS,
            method: _DIOptimizer_Master.URL.List,
            readparam: function () {
                return {
                    optimizerID: $scope.OptimizerID
                }
            },
            pageSize: 0,
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    ETA: { type: 'date' },
                    ETD: { type: 'date' },
                    IsChoose: { type: 'bool', defaultValue: false },
                }
            }
        }),
        height: '100%', pageable: false, sortable: true, columnMenu: false, resizable: true,
        reorderable: false, filterable: { mode: 'row' }, selectable: 'multiple, row',
        change: function (e) {
            var grid = this;

            openMapV2.ClearVector("VectorMarker");
            openMapV2.ClearVector("VectorRoute");
            $timeout(function () {
                var one = grid.select().length == 1;
                var tmp = {};
                var tmpRoute = {};
                angular.forEach(grid.select(), function (tr) {
                    var item = grid.dataItem(tr);
                    if (Common.HasValue(item)) {
                        var obj = _DIOptimizer_Master.Data.MasterLocation[item.ID];
                        if (Common.HasValue(obj)) {
                            var idx = 0;
                            var color = 'rgba(0, 0, 255, 0.8)';
                            for (var i = 1; i < obj.ListLocation.length; i++) {
                                var a = obj.ListLocation[i - 1];
                                var b = obj.ListLocation[i];
                                var icon = openMapV2.NewStyle.Icon(Common.String.Format(openMapV2.NewImage.Location), 1);
                                if (tmp[a.ID] == null) {
                                    tmp[a.ID] = true;
                                    openMapV2.NewMarker(a.Lat, a.Lng, a.Code, a.Location, icon, {}, "VectorMarker")
                                }
                                if (tmp[b.ID] == null) {
                                    tmp[b.ID] = true;
                                    openMapV2.NewMarker(b.Lat, b.Lng, b.Code, b.Location, icon, {}, "VectorMarker")
                                }
                                openMapV2.NewPolyLine([openMapV2.NewPoint(a.Lat, a.Lng), openMapV2.NewPoint(b.Lat, b.Lng)], "", obj.TripNo + "-" + " Chặng " + i + ": " + a.Location + "-" + b.Location, openMapV2.NewStyle.Line(4, 'rgba(255, 0, 0, 0.6)'), null, "VectorRoute");
                            }
                        }
                    }
                })
                openMapV2.FitBound("VectorMarker", 15);
            }, 10)
        },
        columns: [
            {
                field: 'Choose', title: ' ', width: '35px',
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,master_grid,masterGridChoose_Change)" />',
                headerAttributes: { style: 'text-align: center;' }, attributes: { style: 'text-align: center;' },
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,master_grid,masterGridChoose_Change)" />',
                filterable: false, sortable: false, hidden: $scope.StatusOfOptimizer == 2 ? true : false
            },
            {
                field: 'Note', width: '100px', title: 'Số chuyến', template: "<a style='cursor:pointer;#=HasChanged?'color: red' : ''#' ng-click='Detail_Click($event,dataItem,master_win)'>#=Note#</a>",
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'VendorOfVehicleName', width: '150px', title: 'Nhà xe',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'VehicleNo', width: '100px', title: 'Số xe',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'ETD', width: '150px', title: 'ETD',
                template: "#=ETD != null ? Common.Date.ToString(ETD) : ''#",
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'ETA', width: '150px', title: 'ETA',
                template: "#=ETA != null ? Common.Date.ToString(ETA) : ''#",
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'KM', width: '150px', title: 'Khoảng cách', template: '',
                filterable: { cell: { operator: 'gte', showOperators: false } }
            },
            {
                field: 'Credit', width: '100px', title: 'Doanh thu', template: '',
                filterable: { cell: { operator: 'gte', showOperators: false } }
            },
            {
                field: 'Debit', width: '100px', title: 'Chi phí', template: '',
                filterable: { cell: { operator: 'gte', showOperators: false } }
            },
            { title: ' ', filterable: false, menu: false, sortable: false }
        ]
    }

    $scope.routingGrid_Options = {
        dataSource: Common.DataSource.Local({
            data: [],
            pageSize: 0,
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' }
                }
            }
        }),
        height: '100%', pageable: false, sortable: true, columnMenu: false, resizable: true,
        reorderable: false, filterable: { mode: 'row' }, selectable: 'multiple, row',
        columns: [
            {
                field: 'LocationFromName', width: '250px', title: 'Điểm đi',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'LocationFromLat', width: '100px', title: 'Kinh độ',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'LocationFromLng', width: '100px', title: 'Vĩ độ',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'LocationToName', width: '250px', title: 'Điểm đến',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'LocationToLat', width: '100px', title: 'Kinh độ',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'LocationToLng', width: '100px', title: 'Vĩ độ',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            { title: ' ', filterable: false, menu: false, sortable: false }
        ]
    }

    $scope.Detail_Click = function ($event, item, win) {
        $event.preventDefault();
        $scope.DITOMaster = {
            ID: item.ID, Note: item.Note, ETA: item.ETA, ETD: item.ETD,
            VehicleNo: item.VehicleNo, VehicleID: item.VehicleID
        }
        $scope.gopGrid_Options.dataSource.read();
        win.center().open();
    }

    $scope.gopGrid_Options = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.OPS,
            method: _DIOptimizer_Master.URL.Master_Container_List,
            readparam: function () {
                return {
                    optMasterID: $scope.DITOMaster.ID
                }
            },
            pageSize: 0,
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    Ton: { type: 'number' },
                    CBM: { type: 'number' },
                    Quantity: { type: 'number' },
                    IsChoose: { type: 'bool', defaultValue: false },
                    ETD: { type: 'date' },
                    ETA: { type: 'date' },
                    ETDStart: { type: 'date' },
                    ETAStart: { type: 'date' }
                }
            }
        }),
        height: '100%', pageable: false, sortable: true, columnMenu: false, resizable: true,
        reorderable: false, filterable: { mode: 'row' }, selectable: true,
        columns: [
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
        ]
    }

    $scope.cboVehicle_Options = {
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'VehicleID',
                fields: {
                    VehicleID: { type: 'number' },
                    VehicleNo: { type: 'string' }
                }
            },
        }),
        index: 0, autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true,
        dataTextField: 'VehicleNo', dataValueField: 'VehicleID'
    }

    $scope.LoadData = function () {
        Common.Log("LoadData");
        Common.Services.Call($http, {
            url: Common.Services.url.OPS,
            method: _DIOptimizer_Master.URL.Location_Data,
            data: {
                optimizerID: $scope.OptimizerID
            },
            success: function (res) {
                angular.forEach(res.Data, function (o, i) {
                    _DIOptimizer_Master.Data.MasterLocation[o.ID] = o;
                })
            }
        })
        Common.Services.Call($http, {
            url: Common.Services.url.OPS,
            method: _DIOptimizer_Master.URL.Vehicle_Data,
            data: {
                optimizerID: $scope.OptimizerID,
                request: ''
            },
            success: function (res) {
                $scope.cboVehicle_Options.dataSource.data(res.Data);
            }
        })
    }

    $scope.ReOptimize_Click = function ($event) {
        $event.preventDefault();

        $rootScope.Message({
            Type: Common.Message.Type.Confirm,
            Msg: 'Chạy lại tối ưu?',
            Ok: function () {
                $scope.Run();
            }
        })
    }

    $scope.Optimize_ToMon_Click = function ($event) {
        $event.preventDefault();

        var lstID = [];
        $.each($scope.master_grid.dataSource.data(), function (i, v) {
            if (v.IsChoose == true)
                lstID.push(v.ID);
        });

        $rootScope.Message({
            Type: Common.Message.Type.Confirm,
            Msg: 'Lưu chuyến và gửi điều phối?',
            Ok: function () {
                $rootScope.IsLoading = true;

                Common.Services.Call($http, {
                    url: Common.Services.url.OPS,
                    method: _DIOptimizer_Master.URL.ToMon,
                    data: { optimizerID: $scope.OptimizerID, data: lstID },
                    success: function (res) {
                        Common.Services.Error(res, function (res) {
                            $rootScope.IsLoading = false;
                            $scope.StatusOfOptimizer = 2;
                            try {
                                var objCookie = JSON.parse(Common.Cookie.Get("OPSCOOptimizer"));
                                if (Common.HasValue(objCookie)) {
                                    objCookie.StatusOfOptimizer = 2;
                                    Common.Cookie.Set("OPSCOOptimizer", JSON.stringify(objCookie));
                                }
                            }
                            catch (e) { }
                            $rootScope.Message({ Msg: "Đã cập nhật!" });
                            $scope.OptimizerClosed = true;
                            $scope.HasChoose = false;
                            $scope.master_grid.hideColumn("Choose");
                            $timeout(function () {
                                $scope.master_grid.refresh();
                            }, 200)
                        }, function () {
                            $rootScope.IsLoading = false;
                        })
                    }
                })
            }
        })
    }

    $scope.Accept_Click = function ($event, win) {
        $event.preventDefault();

        $rootScope.Message({
            Type: Common.Message.Type.Confirm,
            Msg: 'Lưu chuyến?',
            Ok: function () {
                $rootScope.IsLoading = true;

                Common.Services.Call($http, {
                    url: Common.Services.url.OPS,
                    method: _DIOptimizer_Master.URL.Change,
                    data: { item: $scope.DITOMaster },
                    success: function (res) {
                        Common.Services.Error(res, function (res) {
                            $rootScope.IsLoading = false;
                            $rootScope.Message({ Msg: "Đã cập nhật!" });
                            win.close();
                            $scope.master_gridOptions.dataSource.read();
                            $scope.LoadData();
                        }, function () {
                            $rootScope.IsLoading = false;
                        })
                    }
                })
            }
        })
    }

    $scope.Delete_Click = function ($event, win) {
        $event.preventDefault();

        $rootScope.Message({
            Type: Common.Message.Type.Confirm,
            Msg: 'Xóa chuyến?',
            Ok: function () {
                $rootScope.IsLoading = true;

                Common.Services.Call($http, {
                    url: Common.Services.url.OPS,
                    method: _DIOptimizer_Master.URL.Delete,
                    data: { optMasterID: $scope.DITOMaster.ID },
                    success: function (res) {
                        Common.Services.Error(res, function (res) {
                            $rootScope.IsLoading = false;
                            $rootScope.Message({ Msg: "Đã xóa!" });
                            win.close();
                            $scope.master_gridOptions.dataSource.read();
                            $scope.LoadData();
                        }, function () {
                            $rootScope.IsLoading = false;
                        })
                    }
                })
            }
        })
    }

    $scope.Close_Click = function ($event, win) {
        $event.preventDefault();

        win.close();
    }

    $scope.ResetView = function () {
        Common.Log("ResetView");
        //Nếu chưa Run=> Check và Run.
        if ($scope.StatusOfOptimizer == 0) {
            $scope.Run();
        }
        else { //Load dữ liệu chuyến.
            $rootScope.IsLoading = false;
            $scope.LoadData();

            if ($scope.StatusOfOptimizer == 2) {
                $timeout(function () {
                    $scope.master_grid.hideColumn("Choose");
                }, 500)
            }
        }
    }

    $scope.notification_winOptions = {
        close: function (e) {
            if (e.userTriggered)
                e.preventDefault();
        }
    }
    $scope.Notification_Text = "Kiểm tra DL cung đường";
    $scope.Notification_Color = {
        color: 'blue'
    }

    $scope.Run = function () {
        Common.Log("Run");
        $scope.notification_win.center().open();
        $scope.Notification_Text = "Kiểm tra DL cung đường";
        Common.Services.Call($http, {
            url: Common.Services.url.OPS,
            method: _DIOptimizer_Master.URL.Location_Matrix_List,
            data: { optimizerID: $scope.OptimizerID },
            success: function (res) {
                Common.Services.Error(res, function (res) {
                    if (res == null || res.length == 0) {
                        $scope.Notification_Text = "Khởi tạo DL đầu vào";
                        Common.Services.Call($http, {
                            url: Common.Services.url.OPS,
                            method: _DIOptimizer_Master.URL.Run,
                            data: { optimizerID: $scope.OptimizerID },
                            success: function (res) {
                                Common.Services.Error(res, function (res) {
                                    $scope.Notification_Text = "Chạy optimize";
                                    Common.Services.Call($http, {
                                        url: Common.Services.url.OPS,
                                        method: _DIOptimizer_Master.URL.Cal,
                                        data: { optimizerID: $scope.OptimizerID },
                                        success: function (res) {
                                            Common.Services.Error(res, function (res) {
                                                $scope.Notification_Text = "Lưu dữ liệu";
                                                Common.Services.Call($http, {
                                                    url: Common.Services.url.OPS,
                                                    method: _DIOptimizer_Master.URL.Out,
                                                    data: { optimizerID: $scope.OptimizerID },
                                                    success: function (res) {
                                                        Common.Services.Error(res, function (res) {
                                                            $scope.Notification_Text = "Tối ưu thành công! Load dữ liệu";
                                                            $timeout(function () {
                                                                $scope.notification_win.close();
                                                            }, 1000)
                                                            $scope.LoadData();
                                                            $scope.master_gridOptions.dataSource.read();
                                                            var objCookie = JSON.parse(Common.Cookie.Get("OPSDIOptimizer"));
                                                            if (Common.HasValue(objCookie)) {
                                                                objCookie.StatusOfOptimizer = 1;
                                                                Common.Cookie.Set("OPSDIOptimizer", JSON.stringify(objCookie));
                                                            }
                                                        })
                                                    },
                                                    error: $scope.Error
                                                })
                                            })
                                        },
                                        error: $scope.Error
                                    })
                                })
                            },
                            error: $scope.Error
                        })
                    } else {
                        $scope.Notification_Text = "Thiếu DL cung đường. Dừng optimize";
                        $scope.routingGrid.dataSource.data(res);
                        $rootScope.IsLoading = false;
                        $timeout(function () {
                            $scope.notification_win.close();
                            $scope.routing_win.center().open();
                        }, 1000)
                    }
                })
            },
            error: $scope.Error
        })
    }

    $scope.masterGridChoose_Change = function ($event, grid, hasChoose) {
        $scope.HasChoose = hasChoose;
    }

    $scope.Error = function () {
        $rootScope.IsLoading = false;
        $rootScope.Message({
            Msg: "Lỗi. Optimize không thành công! Quay lại trang đơn hàng.",
            NotifyType: Common.Message.NotifyType.ERROR
        });
        $timeout(function () {
            $state.go("main.OPSAppointment.DIOptimizer_GroupOfProduct", { OptimizerID: $scope.OptimizerID });
        }, 1000)
    }

    $scope.Optimize_GetJson_Click = function ($event) {
        $event.preventDefault();

        $rootScope.IsLoading = true;
        Common.Services.Call($http, {
            url: Common.Services.url.OPS,
            method: _COOptimizer_Master.URL.JsonSet,
            data: { optimizerID: $scope.OptimizerID },
            success: function (res) {
                Common.Services.Error(res, function (res) {
                    $rootScope.IsLoading = false;
                    $rootScope.DownloadFile(res);
                }, function () {
                    $rootScope.IsLoading = false;
                })
            }
        })
    }
}]);