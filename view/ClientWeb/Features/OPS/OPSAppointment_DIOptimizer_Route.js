/// <reference path="~/Scripts/angular.intellisense.js" />
/// <reference path="~/Scripts/jquery-1.9.1.intellisense.js" />
/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />
/// <reference path="~/Scripts/openMap.js" />

var _DIOptimizer_Route = {
    URL: {
        Routing_List: 'Opt_Routing_List',
        Routing_Require_List: 'Opt_Routing_Require_List',
        Routing_Require_Save: 'Opt_Routing_Require_Save',
        Routing_Require_Remove: 'Opt_Routing_Require_Remove',
        Routing_Require_Reset: 'Opt_Routing_Require_Reset',
        Opt_Routing_Save: 'Opt_Routing_Save',
        Optimizer_Get: 'Opt_Optimizer_Get'
    },
    Data: {
        ItemNew: {
            ID: -1, Date: null, DayOfWeek: 0, TimeFrom: new Date(new Date().setHours(6, 0, 0, 0)), TimeTo: new Date(new Date().setHours(18, 0, 0, 0)), Weight: 0, Length: 0, Height: 0, Width: 0
        },
        DayOfWeek: [
            { ID: 1, Name: 'Thứ 2' },
            { ID: 2, Name: 'Thứ 3' },
            { ID: 3, Name: 'Thứ 4' },
            { ID: 4, Name: 'Thứ 5' },
            { ID: 5, Name: 'Thứ 6' },
            { ID: 6, Name: 'Thứ 7' },
            { ID: 0, Name: 'Chủ nhật' }
        ]
    }
}

angular.module('myapp').controller('OPSAppointment_DIOptimizer_RouteCtrl', ['$rootScope', '$scope', '$http', '$location', '$state', '$timeout', 'openMap', function ($rootScope, $scope, $http, $location, $state, $timeout, openMap) {
    if ($rootScope.IsPageComplete != true) return;
    Common.Log('OPSAppointment_DIOptimizer_RouteCtrl');
    $rootScope.IsLoading = false;

    $scope.OptimizerID = parseInt($state.params.OptimizerID);
    $scope.ButtonRunTooltip = "Chạy tối ưu";
    $scope.OptimizerName = "";
    $scope.StatusOfOptimizer = 0;
    $scope.HasChoose = false;

    try {
        var objCookie = JSON.parse(Common.Cookie.Get("OPSDIOptimizer"));
        if (Common.HasValue(objCookie)) {
            if (objCookie.ID != $scope.OptimizerID) {
                Common.Services.Call($http, {
                    url: Common.Services.url.OPS,
                    method: _DIOptimizer_Route.URL.Optimizer_Get,
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

    $scope.tabIndex = 0;
    $scope.RouteName = "";
    $scope.optRouteID = -1;

    $scope.routeGrid_Options = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.OPS,
            method: _DIOptimizer_Route.URL.Routing_List,
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
                    IsChoose: { type: 'bool' },
                    KM: { type: 'number' },
                    Time: { type: 'number' }
                }
            }
        }),
        height: '100%', pageable: false, sortable: true, columnMenu: false, resizable: true, reorderable: false, filterable: false,
        columns: [
            {
                title: ' ', width: '40px',
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,routeGrid,routeGridChooseChange)" />',
                headerAttributes: { style: 'text-align: center;' },
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,routeGrid,routeGridChooseChange)" />',
                attributes: { style: 'text-align: center;' },
                filterable: false, sortable: false
            },
            {
                title: ' ', width: '45px',
                attributes: { style: 'text-align: center;' },
                template: "<a  href='/' class='k-button' ng-click='Route_Require_Click($event,dataItem,required_win)' title='Ràng buộc'><i class='fa fa-bolt'></i></a>",
                filterable: false, sortable: false
            },
            {
                field: 'Code', width: '200px', title: 'Mã',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'RoutingName', width: '350px', title: 'Tên cung đường',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'Time', width: '150px', title: 'Thời gian(m)', template: '#=Common.Number.ToNumber3(Time*60)#',
                filterable: { cell: { operator: 'eq', showOperators: false } }
            },
            {
                field: 'KM', width: '150px', title: 'Khoảng cách(km)',
                filterable: { cell: { operator: 'eq', showOperators: false } }
            },
            {
                field: 'RoutingDescription', width: '250px', title: 'Mô tả',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            { title: ' ', filterable: false, menu: false, sortable: false }
        ]
    };
    $scope.route_win_tabOptions = {
        animation: {
            open: {
                effects: "fadeIn"
            }
        },
        select: function (e) {
            $timeout(function () {
                $scope.tabIndex = angular.element(e.item).data('tabindex');
            }, 1);
        }
    };
    
    $scope.routeGridChooseChange = function ($event, grid, haschoose) {
        $scope.HasChoose = haschoose;
    };

    $scope.Route_Require_Click = function ($event, dataItem, win) {
        $event.preventDefault();

        $scope.optRouteID = dataItem.ID;
        $scope.RouteName = dataItem.RoutingName;

        win.center().open();
        $scope.tabIndex = 0;
        $scope.location_win_tab.select($scope.tabIndex);
        $timeout(function () {
            $scope.time_Grid_Options.dataSource.read();
            $scope.size_Grid_Options.dataSource.read();
        }, 100)
    }

    $scope.IsTimeEdit = false;
    $scope.IsSizeEdit = false;
    $scope.ItemTimeEdit = {};
    $scope.ItemSizeEdit = {};

    $scope.time_Grid_Options = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.OPS,
            method: _DIOptimizer_Route.URL.Routing_Require_List,
            readparam: function () {
                return {
                    optRouteID: $scope.optRouteID,
                    isSize: false
                }
            },
            pageSize: 0,
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    TimeFrom: { type: 'date' },
                    TimeTo: { type: 'date' },
                    Date: { type: 'date' }
                }
            }
        }),
        height: '100%', pageable: false, sortable: false, columnMenu: false, resizable: true, reorderable: false, editable: 'inline',
        toolbar: kendo.template($("#time_Grid_toolbar").html()),
        columns: [
            {
                title: ' ', width: '85px', filterable: false, sortable: false, hidden: $scope.StatusOfOptimizer == 2 ? true : false,
                headerAttributes: { style: 'text-align: center;' }, attributes: { style: 'text-align: center;' },
                template: "<a  href='/' ng-show='!IsTimeEdit' class='k-button' ng-click='Time_Edit_Click($event,dataItem,time_Grid)'><i class='fa fa-pencil'></i></a>" +
                    "<a  href='/' ng-show='!IsTimeEdit' class='k-button' ng-click='Time_Delete_Click($event,dataItem,time_Grid)'><i class='fa fa-trash'></i></a>" +
                    "<a  href='/' class='k-button' ng-show='IsTimeEdit&&ItemTimeEdit.ID==#=ID#?true:false' ng-click='Time_Save_Click($event,dataItem,time_Grid)'><i class='fa fa-check'></i></a>" +
                    "<a  href='/' class='k-button' ng-show='IsTimeEdit&&ItemTimeEdit.ID==#=ID#?true:false' ng-click='Time_Cancel_Click($event,dataItem,time_Grid)'><i class='fa fa-ban'></i></a>",
            },
            {
                field: 'DayOfWeek', title: 'Ngày trong tuần', template: "#=DayOfWeek==0?'Chủ nhật':'Thứ '+(DayOfWeek+1)#",
                editor: '<input class="cus-combobox" focus-k-combobox  kendo-combo-box="cboDayOfWeek" ng-model="ItemTimeEdit.DayOfWeek" k-options="cboDayOfWeek_Options" />',
            },
            {
                field: 'TimeFrom', width: '150px', title: 'Tg BĐ', template: "#=TimeFrom==null?' ':kendo.toString(TimeFrom, '" + Common.Date.Format.HM + "')#",
                editor: '<input class="cus-datetimepicker" focus-k-timepicker kendo-time-picker k-options="DateHM" k-ng-model="ItemTimeEdit.TimeFrom" v-form-require="true" />',
            },
            {
                field: 'TimeTo', width: '150px', title: 'Tg KT', template: "#=TimeTo==null?' ':kendo.toString(TimeTo, '" + Common.Date.Format.HM + "')#",
                editor: '<input class="cus-datetimepicker" focus-k-timepicker kendo-time-picker k-options="DateHM" k-ng-model="ItemTimeEdit.TimeTo" v-form-require="true" />',
            },
            {
                field: 'Date', width: '200px', title: 'Ngày cụ thể', template: "#=Date==null?' ':kendo.toString(Date, '" + Common.Date.Format.DMY + "')#",
                editor: '<input class="cus-datetimepicker" focus-k-datepicker kendo-date-picker k-options="DateDMY" k-ng-model="ItemTimeEdit.Date" v-form-require="false" />',
            }
        ]
    };
    $scope.size_Grid_Options = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.OPS,
            method: _DIOptimizer_Route.URL.Routing_Require_List,
            readparam: function () {
                return {
                    optRouteID: $scope.optRouteID,
                    isSize: true
                }
            },
            pageSize: 0,
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    TimeFrom: { type: 'date' },
                    TimeTo: { type: 'date' },
                    Date: { type: 'date' }
                }
            }
        }),
        height: '100%', pageable: false, sortable: false, columnMenu: false, resizable: true, reorderable: false, editable: 'inline',
        toolbar: kendo.template($("#size_Grid_toolbar").html()),
        columns: [
            {
                title: ' ', width: '60px', filterable: false, sortable: false, hidden: $scope.StatusOfOptimizer == 2 ? true : false,
                headerAttributes: { style: 'text-align: center;' }, attributes: { style: 'text-align: center;' },
                template: "<a  href='/' ng-show='!IsSizeEdit' class='k-button' ng-click='Size_Edit_Click($event,dataItem,size_Grid)'><i class='fa fa-pencil'></i></a>" +
                    "<a  href='/' ng-show='!IsSizeEdit' class='k-button' ng-click='Size_Delete_Click($event,dataItem,size_Grid)'><i class='fa fa-trash'></i></a>" +
                    "<a  href='/' class='k-button' ng-show='IsSizeEdit&&ItemSizeEdit.ID==#=ID#?true:false' ng-click='Size_Save_Click($event,dataItem,size_Grid)'><i class='fa fa-check'></i></a>" +
                    "<a  href='/' class='k-button' ng-show='IsSizeEdit&&ItemSizeEdit.ID==#=ID#?true:false' ng-click='Size_Cancel_Click($event,dataItem,size_Grid)'><i class='fa fa-ban'></i></a>",
            },
            {
                field: 'DayOfWeek',width: '90px', title: 'Ngày trong tuần', template: "#=DayOfWeek==0?'Chủ nhật':'Thứ '+(DayOfWeek+1)#",
                editor: '<input class="cus-combobox" focus-k-combobox  kendo-combo-box="cboDayOfWeek" ng-model="ItemSizeEdit.DayOfWeek" k-options="cboDayOfWeek_Options" />',
            },
            {
                field: 'TimeFrom', width: '80px', title: 'Tg BĐ', template: "#=TimeFrom==null?' ':kendo.toString(TimeFrom, '" + Common.Date.Format.HM + "')#",
                editor: '<input class="cus-datetimepicker" focus-k-timepicker kendo-time-picker k-options="DateHM" k-ng-model="ItemSizeEdit.TimeFrom" v-form-require="true" />',
            },
            {
                field: 'TimeTo', width: '80px', title: 'Tg KT', template: "#=TimeTo==null?' ':kendo.toString(TimeTo, '" + Common.Date.Format.HM + "')#",
                editor: '<input class="cus-datetimepicker" focus-k-timepicker kendo-time-picker k-options="DateHM" k-ng-model="ItemSizeEdit.TimeTo" v-form-require="true" />',
            },
            {
                field: 'Height', width: '80px', title: 'Chiều cao',
                editor: '<input class="k-textbox cus-number" type="number" ng-model="ItemSizeEdit.Height" style="width: 100%;" v-form-require="true" />',
            },
            {                                                                                         
                field: 'Width', width: '80px', title: 'Chiều rộng',
                editor: '<input class="k-textbox cus-number" type="number" ng-model="ItemSizeEdit.Width" style="width: 100%;" v-form-require="true" />',
            },
            {
                field: 'Length', width: '80px', title: 'Chiều dài',
                editor: '<input class="k-textbox cus-number" type="number" ng-model="ItemSizeEdit.Length" style="width: 100%;" v-form-require="true" />',
            },
            {
                field: 'Weight', width: '80px', title: 'Trọng tải',
                editor: '<input class="k-textbox cus-number" type="number" ng-model="ItemSizeEdit.Weight" style="width: 100%;" v-form-require="true" />',
            },
            {
                field: 'Date', width: '100px', title: 'Ngày cụ thể', template: "#=Date==null?' ':kendo.toString(Date, '" + Common.Date.Format.DMY + "')#",
                editor: '<input class="cus-datetimepicker" focus-k-datepicker kendo-date-picker k-options="DateDMY" k-ng-model="ItemSizeEdit.Date" v-form-require="false" />',
            }
        ]
    };
    $scope.cboDayOfWeek_Options = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true, minLength: 3,
        dataTextField: 'Name', dataValueField: 'ID',
        dataSource: Common.DataSource.Local({
            data: _DIOptimizer_Route.Data.DayOfWeek,
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    Name: { type: 'string' }
                }
            }
        }),
    };

    $scope.New_Time_Click = function ($event, grid) {
        $event.preventDefault();

        grid.dataSource.insert(0, $.extend(true, {}, _DIOptimizer_Route.Data.ItemNew));
        $timeout(function () {
            $scope.IsTimeEdit = true;
            var items = grid.items();
            $scope.ItemTimeEdit = grid.dataItem(items[0]);
            grid.editRow(items[0]);
            $scope.TimeSaved = false;
        }, 100)
    }
    $scope.Time_Edit_Click = function ($event, item, grid) {
        $event.preventDefault();

        var tr = $event.target.closest('tr');
        $scope.IsTimeEdit = true;
        $scope.ItemTimeEdit = grid.dataItem(tr);
        grid.editRow(tr);
        $scope.TimeSaved = false;
    }
    $scope.Time_Delete_Click = function ($event, item, grid) {
        $event.preventDefault();

        Common.Services.Call($http, {
            url: Common.Services.url.OPS,
            method: _DIOptimizer_Route.URL.Routing_Require_Remove,
            data: {
                data: [item.ID]
            },
            success: function (res) {
                Common.Services.Error(res, function () {
                    $rootScope.Message({ Msg: "Đã xóa!" });
                    grid.dataSource.read();
                })
            }
        })
    }

    $scope.TimeSaved = false;
    $scope.Time_Save_Click = function ($event, item, grid) {
        $event.preventDefault();

        if ($scope.TimeSaved == false) {
            if (item.TimeFrom != null && item.TimeTo != null && item.DayOfWeek != null) {
                item.IsSize = false;
                $scope.TimeSaved = true;

                Common.Services.Call($http, {
                    url: Common.Services.url.OPS,
                    method: _DIOptimizer_Route.URL.Routing_Require_Save,
                    data: {
                        optRoutingID: $scope.optRouteID,
                        item: $scope.ItemTimeEdit
                    },
                    success: function (res) {
                        Common.Services.Error(res, function () {
                            $rootScope.Message({ Msg: "Đã cập nhật!" });
                            $scope.IsTimeEdit = false;
                            grid.dataSource.read();
                        }, function (res) {
                        })
                    },
                    error: function (res) {
                        $scope.TimeSaved = false;
                    }
                })
            } else {
                $rootScope.Message({ Msg: "Thiếu dữ liệu!" });
            }
        }
    }
    $scope.Time_Cancel_Click = function ($event, item, grid) {
        $event.preventDefault();
        $scope.IsTimeEdit = false;
        grid.cancelChanges();
    }

    $scope.New_Size_Click = function ($event, grid) {
        $event.preventDefault();

        grid.dataSource.insert(0, $.extend(true, {}, _DIOptimizer_Route.Data.ItemNew));
        $timeout(function () {
            $scope.IsSizeEdit = true;
            var items = grid.items();
            $scope.ItemSizeEdit = grid.dataItem(items[0]);
            grid.editRow(items[0]);
            $scope.SizeSaved = false;
        }, 100)
    }
    $scope.Size_Edit_Click = function ($event, item, grid) {
        $event.preventDefault();

        var tr = $event.target.closest('tr');
        $scope.IsSizeEdit = true;
        $scope.ItemSizeEdit = grid.dataItem(tr);
        grid.editRow(tr);
        $scope.SizeSaved = false;
    }
    $scope.Size_Delete_Click = function ($event, item, grid) {
        $event.preventDefault();

        Common.Services.Call($http, {
            url: Common.Services.url.OPS,
            method: _DIOptimizer_Route.URL.Routing_Require_Remove,
            data: {
                data: [item.ID]
            },
            success: function (res) {
                Common.Services.Error(res, function () {
                    $rootScope.Message({ Msg: "Đã xóa!" });
                    grid.dataSource.read();
                })
            }
        })
    }

    $scope.SizeSaved = false;
    $scope.Size_Save_Click = function ($event, item, grid) {
        $event.preventDefault();

        if ($scope.SizeSaved == false) {
            if (item.TimeFrom != null && item.TimeTo != null && item.DayOfWeek != null) {
                item.IsSize = true;
                $scope.SizeSaved = true;

                Common.Services.Call($http, {
                    url: Common.Services.url.OPS,
                    method: _DIOptimizer_Route.URL.Routing_Require_Save,
                    data: {
                        optRoutingID: $scope.optRouteID,
                        item: $scope.ItemSizeEdit
                    },
                    success: function (res) {
                        Common.Services.Error(res, function () {
                            $rootScope.Message({ Msg: "Đã cập nhật!" });
                            $scope.IsSizeEdit = false;
                            grid.dataSource.read();
                        }, function (res) {
                        })
                    },
                    error: function (res) {
                        $scope.SizeSaved = false;
                    }
                })
            } else {
                $rootScope.Message({ Msg: "Thiếu dữ liệu!" });
            }
        }
    }
    $scope.Size_Cancel_Click = function ($event, item, grid) {
        $event.preventDefault();
        $scope.IsSizeEdit = false;
        grid.cancelChanges();
    }

    $scope.Reset_Click = function ($event, grid) {
        $event.preventDefault();

        var data = [];
        Common.Data.Each(grid.dataSource.data(), function (o) {
            if (o.IsChoose == true)
                data.push(o.ID);
        })

        if (data.length > 0) {
            $rootScope.Message({
                Msg: "Xác nhận lấy DL ràng buộc mặc định?",
                Type: Common.Message.Type.Confirm,
                Ok: function () {
                    Common.Services.Call($http, {
                        url: Common.Services.url.OPS,
                        method: _DIOptimizer_Route.URL.Routing_Require_Reset,
                        data: {
                            data: data
                        },
                        success: function (res) {
                            Common.Services.Error(res, function () {
                                $rootScope.Message({ Msg: "Đã cập nhật!" });
                                grid.dataSource.read();
                            })
                        }
                    })
                }
            })
        }
    }
    
    $scope.Distance_Click = function ($event) {
        $event.preventDefault();

        var data = $scope.routeGrid_Options.dataSource.data();
        angular.forEach(data, function (o) {
            try {
                openMap.Distance({ Lat: o.FromLat, Lng: o.FromLng }, { Lat: o.ToLat, Lng: o.ToLng }, function (p1, p2, p) {
                    Common.Services.Call($http, {
                        url: Common.Services.url.OPS,
                        method: _DIOptimizer_Route.URL.Opt_Routing_Save,
                        data: { ID: o.ID, Time: p.Time / 3600, Distance: p.Distance / 1000 },
                        success: function (res) {
                            Common.Services.Error(res, function (res) {
                                Common.Log(o);
                            })
                        }
                    })
                });
            }
            catch (e) { }
        })
    }

    $scope.Back_Click = function ($event) {
        $event.preventDefault();
        $state.go("main.OPSAppointment.DIOptimizer");
    }

    $scope.Close_Click = function ($event, win) {
        $event.preventDefault();
        $scope.IsTimeEdit = false;
        $scope.IsSizeEdit = false;
        win.close();
    }

    //Alt + N
    $scope.OnKeyDown = function ($event) {
        if ($event.altKey && $event.keyCode == 78) {
            if ($scope.tabIndex == 0 && $scope.IsTimeEdit == false) {
                $scope.New_Time_Click($event, $scope.time_Grid);
            } else if ($scope.tabIndex == 1 && $scope.IsSizeEdit == false) {
                $scope.New_Size_Click($event, $scope.size_Grid);
            }
        }
    }
}]);