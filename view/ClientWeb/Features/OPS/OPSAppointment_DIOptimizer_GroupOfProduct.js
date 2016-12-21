/// <reference path="~/Scripts/jquery-1.9.1.intellisense.js" />
/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />

var _DIOptimizer_GroupOfProduct = {
    URL: {
        GroupOfProduct_List: 'Opt_GroupOfProduct_List',
        GroupOfProduct_NotIn_List: 'Opt_GroupOfProduct_NotIn_List',
        GroupOfProduct_SaveList: 'Opt_GroupOfProduct_SaveList',
        GroupOfProduct_Remove: 'Opt_GroupOfProduct_Remove',
        GroupOfProduct_Update: 'Opt_GroupOfProduct_Update',
        Optimizer_Get: 'Opt_Optimizer_Get'
    }
}

angular.module('myapp').controller('OPSAppointment_DIOptimizer_GroupOfProductCtrl', ['$rootScope', '$scope', '$http', '$location', '$state', '$timeout', function ($rootScope, $scope, $http, $location, $state, $timeout) {
    Common.Log('OPSAppointment_DIOptimizer_GroupOfProductCtrl');
    $rootScope.IsLoading = false;

    $scope.HasChoose = false;
    $scope.OptimizerID = parseInt($state.params.OptimizerID);
    $scope.ButtonRunTooltip = "Chạy tối ưu";
    $scope.OptimizerName = "";
    $scope.StatusOfOptimizer = 0;

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
                                if ($scope.StatusOfOptimizer > 0)
                                    $scope.ButtonRunTooltip = "Xem kết quả";
                                $scope.ResetView();
                            } else {
                                $rootScope.Message({ Msg: "Không tìm thấy optimizer." });
                                $state.go("main.OPSAppointment.DIOptimizer");
                            }
                        })
                    }
                })
            } else {
                $scope.OptimizerName = objCookie.OptimizerName;
                $scope.StatusOfOptimizer = objCookie.StatusOfOptimizer;
                if ($scope.StatusOfOptimizer > 0)
                    $scope.ButtonRunTooltip = "Xem kết quả";
            }
        }
    }
    catch (e) { }

    $scope.gopGrid_Options = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.OPS,
            method: _DIOptimizer_GroupOfProduct.URL.GroupOfProduct_List,
            readparam: function () {
                return {
                    optimizerID: $scope.OptimizerID
                }
            },
            pageSize: 20,
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number', editable: false },
                    Ton: { type: 'number', editable: false },
                    CBM: { type: 'number', editable: false },
                    Quantity: { type: 'number', editable: false },
                    IsChoose: { type: 'bool', defaultValue: false },
                    Choose: { editable: false },
                    ETD: { type: 'date' },
                    ETA: { type: 'date' },
                    ETDStart: { type: 'date' },
                    ETAStart: { type: 'date' },
                    OrderCode: { editable: false },
                    GroupProductName: { editable: false },
                    CustomerName: { editable: false },
                    DNCode: { editable: false },
                    Note1: { editable: false },
                    PartnerCode: { editable: false },
                    Note1: { editable: false },
                    LocationFromName: { editable: false },
                    LocationFromAddress: { editable: false },
                    LocationFromProvinceName: { editable: false },
                    LocationFromDistrictName: { editable: false },
                    LocationToName: { editable: false },
                    LocationToAddress: { editable: false },
                    LocationToProvinceName: { editable: false },
                    LocationToDistrictName: { editable: false }
                }
            }
        }),
        height: '100%', pageable: Common.PageSize, sortable: true, columnMenu: false, resizable: true, reorderable: false, filterable: { mode: 'row' }, editable: 'incell',
        toolbar: $scope.StatusOfOptimizer == 2 ? false : kendo.template($("#gopGrid_toolbar").html()),
        save: function (e) {
            var grid = this;
            $timeout(function () {
                var item = e.model;
                $rootScope.IsLoading = true;
                Common.Services.Call($http, {
                    url: Common.Services.url.OPS,
                    method: _DIOptimizer_GroupOfProduct.URL.GroupOfProduct_Update,
                    data: { item: item },
                    success: function (res) {
                        Common.Services.Error(res, function (res) {
                            $rootScope.IsLoading = false;
                            grid.dataSource.read();
                        })
                    }
                })
            }, 1)
        },
        columns: [
            {
                field: 'Choose', title: ' ', width: '35px',
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,gopGrid,gopGridChoose_Change)" />',
                headerAttributes: { style: 'text-align: center;' }, attributes: { style: 'text-align: center;' },
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,gopGrid,gopGridChoose_Change)" />',
                filterable: false, sortable: false, hidden: $scope.StatusOfOptimizer == 2 ? true : false
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
                field: 'ETDStart', width: '160px', title: 'BĐ ETD',
                template: "#=ETDStart==null?' ':Common.Date.ToString(ETDStart)#",
                editor: function (container, options) {
                    var input = $("<input/>");
                    input.attr("name", options.field);
                    input.appendTo(container);
                    input.kendoDateTimePicker({ format: 'dd/MM/yyyy HH:mm', timeFormat: "HH:mm", parseFormats: ["yyyy-MM-ddTHH:mm:ss"] });
                },
                filterable: {
                    cell: {
                        template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); },
                        operator: 'equal', showOperators: false
                    }
                },
            },
            {
                field: 'ETD', width: '160px', title: 'ETD',
                template: "#=ETD==null?' ':Common.Date.ToString(ETD)#",
                editor: function (container, options) {
                    var input = $("<input/>");
                    input.attr("name", options.field);
                    input.appendTo(container);
                    input.kendoDateTimePicker({ format: 'dd/MM/yyyy HH:mm', timeFormat: "HH:mm", parseFormats: ["yyyy-MM-ddTHH:mm:ss"] });
                },
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
                field: 'ETAStart', width: '160px', title: 'BĐ ETA',
                template: "#=ETAStart==null?' ':Common.Date.ToString(ETAStart)#",
                editor: function (container, options) {
                    var input = $("<input/>");
                    input.attr("name", options.field);
                    input.appendTo(container);
                    input.kendoDateTimePicker({ format: 'dd/MM/yyyy HH:mm', timeFormat: "HH:mm", parseFormats: ["yyyy-MM-ddTHH:mm:ss"] });
                },
                filterable: {
                    cell: {
                        template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); },
                        operator: 'equal', showOperators: false
                    }
                },
            },
            {
                field: 'ETA', width: '160px', title: 'ETA',
                template: "#=ETA==null?' ':Common.Date.ToString(ETA)#",
                editor: function (container, options) {
                    var input = $("<input/>");
                    input.attr("name", options.field);
                    input.appendTo(container);
                    input.kendoDateTimePicker({ format: 'dd/MM/yyyy HH:mm', timeFormat: "HH:mm", parseFormats: ["yyyy-MM-ddTHH:mm:ss"] });
                },
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
    };

    $scope.gopGridChoose_Change = function ($event, grid, hasChoose) {
        $scope.HasChoose = hasChoose;
    }

    $scope.Delete_Click = function ($event, grid) {
        $event.preventDefault();
        var lstID = [];
        $.each(grid.dataSource.data(), function (i, v) {
            if (v.IsChoose == true)
                lstID.push(v.ID);
        });
        if (lstID.length > 0) {
            $rootScope.Message({
                Type: Common.Message.Type.Confirm,
                Msg: "Bạn muốn xóa các đơn hàng đã chọn?",
                Ok: function () {
                    $rootScope.IsLoading = true;
                    Common.Services.Call($http, {
                        url: Common.Services.url.OPS,
                        method: _DIOptimizer_GroupOfProduct.URL.GroupOfProduct_Remove,
                        data: { optimizerID: $scope.OptimizerID, lstID: lstID },
                        success: function (res) {
                            $rootScope.IsLoading = false;
                            Common.Services.Error(res, function (res) {
                                $rootScope.Message({ Msg: "Đã xóa!" });
                                grid.dataSource.read();
                            })
                        }
                    })
                }
            });
        }
    }

    $scope.Insert_Click = function ($event, grid, win) {
        $event.preventDefault();

        win.center().open();
        grid.dataSource.read();
    }

    $scope.gopNotInGrid_Options = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.OPS,
            method: _DIOptimizer_GroupOfProduct.URL.GroupOfProduct_NotIn_List,
            readparam: function () {
                return {
                    optimizerID: $scope.OptimizerID,
                }
            },
            pageSize: 0,
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    IsChoose: { type: 'bool' },
                    ETD: { type: 'date' },
                    ETA: { type: 'date' },
                    RequestDate: { type: 'date' },
                    Ton: { type: 'number' }
                }
            }
        }),
        height: '100%', pageable: false, sortable: true, columnMenu: false, resizable: true, reorderable: false, filterable: { mode: 'row' },
        columns: [
            {
                title: ' ', width: '40px', filterable: false, sortable: false,
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,gopNotInGrid,gopNotInGrid_ChooseChange)" />',
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,gopNotInGrid,gopNotInGrid_ChooseChange)" />',
                headerAttributes: { style: 'text-align: center;' }, attributes: { style: 'text-align: center;' }
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
                field: 'DNCode', width: '100px', title: 'Mã DN',
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
    };

    $scope.Accept_Click = function ($event, grid, win) {
        $event.preventDefault();

        var data = [];
        Common.Data.Each(grid.dataSource.data(), function (o) {
            if (o.IsChoose == true)
                data.push(o.ID);
        })
        if (data.length > 0) {
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.OPS,
                method: _DIOptimizer_GroupOfProduct.URL.GroupOfProduct_SaveList,
                data: { optimizerID: $scope.OptimizerID, data: data },
                success: function (res) {
                    Common.Services.Error(res, function () {
                        $rootScope.Message({
                            Msg: 'Đã cập nhật.',
                            NotifyType: Common.Message.NotifyType.SUCCESS
                        });
                        $rootScope.IsLoading = false;
                        win.close();
                        $scope.gopGrid_Options.dataSource.read();
                    })
                }
            })
        } else {
            win.close();
        }
    }

    $scope.Close_Click = function ($event, win) {
        $event.preventDefault();
        win.close();
    }

    $scope.Back_Click = function ($event) {
        $event.preventDefault();
        $state.go("main.OPSAppointment.DIOptimizer");
    }

    $scope.ResetView = function () {
        if ($scope.StatusOfOptimizer == 2) {
            $scope.gopGrid.setOptions({ toolbar: false });
            $timeout(function () {
                $scope.gopGrid.hideColumn("Choose");
                $scope.gopGrid.hideColumn("Command");
            }, 300)
        }
        $scope.gopGrid.refresh();
    }
}]);