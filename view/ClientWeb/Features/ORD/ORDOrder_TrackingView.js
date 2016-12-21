/// <reference path="~/Scripts/jquery-1.9.1.intellisense.js" />
/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />

//#region URL
var ORDOrder_TrackingView = {
    URL: {
        Order_List: 'ORD_Tracking_Order_List',
        Customer_List: 'ORDOrder_CustomerList',
        OrderTrip_List: 'ORD_Tracking_TripByOrder_List',
        TripLocation_List: 'ORD_Tracking_LocationByTrip_List',
    }
}

//#endregion

angular.module('myapp').controller('ORDOrder_TrackingViewCtrl', ['$rootScope', '$scope', '$http', '$location', '$state', '$timeout', '$compile', 'openMapV2', function ($rootScope, $scope, $http, $location, $state, $timeout, $compile, openMapV2) {
    if ($rootScope.IsPageComplete != true) return;
    Common.Log('ORDOrder_TrackingViewCtrl');
    $rootScope.IsLoading = false;

    $scope.IsLoaded = false;
    $scope.ListCustomer = [];
    $scope.OrderID = "";
    $scope.TripItem = null;
    $scope.CellX = [];
    $scope.CellY = [];
    $scope.CellZ = [];

    $scope.Now = new Date();
    $scope.ViewDate = '';
    $scope.TotalTrip = 0;
    $scope.DriverTel = '';
    $scope.DriverName = '';

    $scope.viewSplitter_Options = {
        orientation: "vertical",
        panes: [
            { collapsible: true, resizable: true },
            { collapsible: true, resizable: true, size: '200px' }
        ],
        resize: function (e) {
            try {
                openMapV2.Resize();
                $scope.gop_Grid.resize();
            }
            catch (e) { }
        }
    }

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
            $scope.ListCustomer = this.value();
        }
    };

    Common.Services.Call($http, {
        url: Common.Services.url.ORD,
        method: ORDOrder_TrackingView.URL.Customer_List,
        success: function (res) {
            if (Common.HasValue(res)) {
                $timeout(function () {
                    $scope.mltCustomerOptions.dataSource.data(res.Data);
                }, 1);
            }
        }
    })

    $scope.cboOrderCodeOptions = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true,
        dataTextField: 'OrderCode', dataValueField: 'ID', minLength: 3, placeholder: "Chọn đơn hàng",
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    OrderCode: { type: 'string' }
                }
            }
        })
    }

    $scope.LoadDataORD = function () {
        Common.Services.Call($http, {
            url: Common.Services.url.ORD,
            method: ORDOrder_TrackingView.URL.Order_List,
            data: { dataCus: $scope.ListCustomer },
            success: function (res) {
                $scope.cboOrderCodeOptions.dataSource.data(res);
                $timeout(function () {
                    if ($scope.IsLoaded) {
                        $scope.IsLoaded = false;
                        $scope.cboOrderCode.value($scope.OrderID);
                    } else {
                        $scope.OrderID = "";
                        $scope.cboOrderCode.select(-1);
                    }
                }, 1)
            }
        })
    }

    $scope.$watch('ListCustomer', function () {
        $scope.LoadDataORD();
    })
        
    $scope.$watch("OrderID", function () {
        Common.Services.Call($http, {
            url: Common.Services.url.ORD,
            method: ORDOrder_TrackingView.URL.OrderTrip_List,
            data: {
                orderID: $scope.OrderID == "" ? -1 : $scope.OrderID
            },
            success: function (res) {
                $scope.gop_GridOptions.dataSource.data(res);
                $scope.TotalTrip = res.length;
            }
        })
    })

    $timeout(function () {
        if (Common.HasValue($state.params.ID)) {
            var str = $state.params.ID + "";
            var data = str.split('&');
            if (data.length > 1) {
                $scope.IsLoaded = true;
                try {
                    $scope.ListCustomer = Array.from(data[1].split(','));
                    $scope.OrderID = data[0];
                    $scope.mltCustomer.value($scope.ListCustomer);
                } catch (e) {
                    $scope.ListCustomer = [];
                    $scope.OrderID = "";
                }
            } else {
                $scope.OrderID = $state.params.ID;
            }
        }
    }, 1000)

    $scope.gop_GridOptions = {
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    ETD: { type: 'date' },
                    ETA: { type: 'date' },
                    ETARequest: { type: 'date' }
                }
            },
            pageSize: 0
        }),
        height: '99%', groupable: false, pageable: false, sortable: true, columnMenu: false, resizable: true,
        selectable: true, filterable: false, reorderable: true, editable: false,
        dataBound: function () {
            $scope.TripItem = null;
            $scope.InitView();
        },
        change: function (e) {
            var grid = this;
            var item = grid.dataItem(grid.select());
            if (Common.HasValue(item)) {
                $scope.TripItem = item;
                $scope.DriverName = item.DriverName;
                $scope.DriverTel = item.DriverTel;
                $scope.ResetView(Common.Date.FromJson(item.Start), Common.Date.FromJson(item.End), item.TimeSpan, item)
            } else {
                $scope.TripItem = null;
                $scope.DriverName = "";
                $scope.DriverTel = "";
                $scope.InitView();
            }
            $scope.ResetMap();
        },
        columns: [
            { field: 'Status', width: 125, title: 'Tình trạng', sortorder: 0, configurable: true, isfunctionalHidden: false },
            { field: 'VehicleNo', width: 100, title: 'Số xe', sortorder: 1, configurable: true, isfunctionalHidden: false },
            { field: 'Code', width: 100, title: 'Số chuyến', sortorder: 2, configurable: true, isfunctionalHidden: false },
            { field: 'LocationFromName', width: 150, title: 'Điểm đi', sortorder: 3, configurable: true, isfunctionalHidden: false },
            { field: 'LocationToName', width: 250, title: 'Điểm đến', sortorder: 4, configurable: true, isfunctionalHidden: false },
            { field: 'GroupList', width: 150, title: 'Loại hàng/Số con.', sortorder: 5, configurable: true, isfunctionalHidden: false },
            {
                field: 'TotalTon', width: 100, title: 'Tấn', template: '#=TotalTon==null?" ":Common.Number.ToNumber3(TotalTon)#',
                sortorder: 6, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'TotalCBM', width: 100, title: 'Khối', template: '#=TotalCBM==null?" ":Common.Number.ToNumber3(TotalCBM)#',
                sortorder: 7, configurable: true, isfunctionalHidden: false
            },
            { field: 'TotalQuantity', width: 100, title: 'S.lượng', sortorder: 8, configurable: true, isfunctionalHidden: false },
            { title: ' ', filterable: false, sortable: false, sortorder: 100, configurable: false, isfunctionalHidden: false }
        ]
    }

    openMapV2.Init({
        Element: 'map',
        Tooltip_Show: true,
        Tooltip_Element: 'map_tooltip',
        MapBoxLightDefault: false,
        DefinedLayer: [{
            Name: 'VectorMarker',
            zIndex: 100
        }, {
            Name: 'VectorRoute',
            zIndex: 90
        }]
    });
    
    $scope.ResetMap = function () {
        openMapV2.ClearVector("VectorMarker");
        openMapV2.ClearVector("VectorRoute");

        if ($scope.TripItem != null) {
            var tmp = [];
            var flg = false;
            var idG = 1;
            var idD = 1;
            for (var i = 0 ; i < $scope.TripItem.ListLocation.length; i++) {
                var item = $scope.TripItem.ListLocation[i];
                if (Common.HasValue(item) && item.Lat > 0 && item.Lng > 0) {
                    var img = Common.String.Format(openMapV2.NewImage.Start);
                    if (item.Status == 0 && i > 0) {
                        img = Common.String.Format(openMapV2.NewImage.End);
                    }
                    if (item.Status == 1) {
                        img = Common.String.Format(openMapV2.NewImage.Get);
                    } else if (item.Status == 2) {
                        img = Common.String.Format(openMapV2.NewImage.Delivery);
                    }
                    var icon = openMapV2.NewStyle.Icon(img, 1);
                    if (item.Status == 0) {
                        if (!flg) {
                            flg = true;
                            tmp[i] = openMapV2.NewMarker(item.Lat, item.Lng, item.Code, item.LocationName, icon, item, "VectorMarker");
                        }
                    } else {
                        tmp[i] = openMapV2.NewMarker(item.Lat, item.Lng, item.Code, item.LocationName, icon, item, "VectorMarker");
                    }
                }
            }

            if (!$scope.TripItem.IsComplete && Common.HasValue(tmp[0]) && Common.HasValue(tmp[1]))
                openMapV2.NewRoute(tmp[0], tmp[1], "", "", openMapV2.NewStyle.Line(4, 'rgba(3, 169, 244, 0.6)'), null, "VectorRoute");
            else {
                var vehicleCode = $scope.TripItem.VehicleNo;
                var dtfrom = $scope.TripItem.ATD;
                var dtto = $scope.TripItem.ATA;
                if (Common.HasValue(tmp[0]))
                    dtfrom = tmp[0].DateTime;
                if (Common.HasValue(tmp[1]))
                    dtto = tmp[1].DateTime;

                if (vehicleCode != "" && Common.HasValue(dtfrom) && Common.HasValue(dtto)) {
                    Common.Services.Call($http, {
                        url: Common.Services.url.ORD,
                        method: _ORDDashboard_MapHistory.URL.VehiclePosition_Get,
                        data: { vehicleCode: vehicleCode, dtfrom: dtfrom, dtto: dtto },
                        success: function (ListPoint) {
                            for (var i = 0; i < ListPoint.length - 1; i++) {
                                var l1 = ListPoint[i];
                                var l2 = ListPoint[i + 1];

                                var style = openMapV2.NewStyle.Line(5, 'rgba(150,50,50, 0.7)', [15, 10], "", '#fff');
                                if (Common.HasValue(l1) && Common.HasValue(l2)) {
                                    var x = openMapV2.NewPolyLine([openMapV2.NewPoint(l1.Lat, l1.Lng), openMapV2.NewPoint(l2.Lat, l2.Lng)], 1, "", style, {}, "VectorRoute")
                                }
                            }
                        }
                    });
                }
            }

            openMapV2.FitBound("VectorMarker", 15);
        }
    }
    
    $scope.SetNow = function () {
        $scope.Now = new Date();
        angular.forEach($('.v-time .time-span-act'), function (o) {
            if ($(o).find('.timeNow').length > 0) {
                o.removeChild($(o).find('.timeNow')[0]);
            }
            var item = $(o).data('item');
            if (item != null && Common.Date.FromJson(item.SDate) <= $scope.Now && Common.Date.FromJson(item.EDate) > $scope.Now) {
                var span = ($scope.Now - Common.Date.FromJson(item.SDate)) / (Common.Date.FromJson(item.EDate) - Common.Date.FromJson(item.SDate));
                $(o).append("<span class='timeNow' style='left:calc(" + span * 100 + "% - 1px);position:absolute;width:2px;height:300%;background:red;top:0;z-index:9999;'></span>");
            }
        })
    }

    $scope.InitView = function () {
        var pNow = new Date(new Date().getFullYear(), new Date().getMonth(), new Date().getDate(), new Date().getHours());

        var sDate = 2;
        var fDate = pNow.addDays(-10 / 24);
        var tDate = pNow.addDays(10 / 24);
        $scope.ViewDate = Common.Date.ToString(fDate, 'dd/MM/yy');
        
        $scope.CellX = [];
        $scope.CellY = [];
        $scope.CellZ = [];

        for (var i = fDate; i < tDate; i = i.addDays(2 / 24)) {
            if (i.addDays(3 / 24) <= tDate) {
                $scope.CellX.push({ SDate: i.addDays(1 / 24), EDate: i.addDays(3 / 24) })
            }
            $scope.CellZ.push({ SDate: i, EDate: i.addDays(1 / 24) })
            $scope.CellZ.push({ SDate: i.addDays(1 / 24), EDate: i.addDays(2 / 24) })
            $scope.CellY.push({ SDate: i, EDate: i.addDays(2 / 24), Text: Common.Date.ToString(i.addDays(1 / 24), 'HH:mm tt') })
        }

        $scope.ResetMap();
        $timeout(function () {
            $scope.SetNow();
        }, 100)
    }

    $scope.InitView();

    $scope.ResetView = function (fDate, tDate, sDate, data) {
        $scope.ViewDate = Common.Date.ToString(fDate, 'dd/MM/yy');
        
        $scope.CellX = [];
        $scope.CellY = [];
        $scope.CellZ = [];

        if (fDate.getMinutes() >= 30)
            fDate = fDate.addDays(-1 / 24);
        fDate = new Date(new Date(new Date(fDate.setMinutes(0)).setSeconds(0)).setMilliseconds(0));
        if (tDate.getMinutes() >= 30)
            tDate = tDate.addDays(1 / 24);
        tDate = new Date(new Date(new Date(tDate.setMinutes(0)).setSeconds(0)).setMilliseconds(0));

        for (var i = fDate; i < tDate; i = i.addDays(sDate * 2 / 24)) {
            if (i.addDays(sDate / 24) <= tDate) {
                $scope.CellX.push({ SDate: i.addDays(sDate / 24), EDate: i.addDays(sDate * 3 / 24) })
            }
            $scope.CellZ.push({ SDate: i, EDate: i.addDays(sDate / 24) })
            $scope.CellZ.push({ SDate: i.addDays(sDate / 24), EDate: i.addDays(sDate * 2 / 24) })
            $scope.CellY.push({ SDate: i, EDate: i.addDays(sDate * 2 / 24), Text: Common.Date.ToString(i.addDays(sDate / 24), 'HH:mm tt') })
        }

        if ($scope.CellX.length == $scope.CellY.length) {
            $scope.CellX.pop();
        }

        $timeout(function () {
            angular.forEach($('.v-time .time-span-ets'), function (o) {
                $(o).empty();
                var item = $(o).data('item');
                if (Common.HasValue(item)) {
                    if (Common.Date.FromJson(item.SDate) <= Common.Date.FromJson(data.ETD) && Common.Date.FromJson(item.EDate) > Common.Date.FromJson(data.ETD)) {
                        var span = (Common.Date.FromJson(data.ETD) - Common.Date.FromJson(item.SDate)) / (Common.Date.FromJson(item.EDate) - Common.Date.FromJson(item.SDate));
                        $(o).append("<span class='timePlan' style='left: calc(" + span * 100 + "% - 16px)'>ETD</span>");
                    }
                    if (Common.Date.FromJson(item.SDate) <= Common.Date.FromJson(data.ETA) && Common.Date.FromJson(item.EDate) > Common.Date.FromJson(data.ETA)) {
                        var span = (Common.Date.FromJson(data.ETA) - Common.Date.FromJson(item.SDate)) / (Common.Date.FromJson(item.EDate) - Common.Date.FromJson(item.SDate));
                        $(o).append("<span class='timePlan' style='left: calc(" + span * 100 + "% - 16px)'>ETA</span>");
                    }
                }
            })

            var sStk = 1, sDis = 1;
            angular.forEach($('.v-time .time-span-act'), function (o) {
                $(o).empty();
                var item = $(o).data('item');
                if (Common.HasValue(item)) {
                    if (Common.Date.FromJson(item.SDate) <= Common.Date.FromJson(data.RequestDate) && Common.Date.FromJson(item.EDate) > Common.Date.FromJson(data.RequestDate)) {
                        var span = (Common.Date.FromJson(data.RequestDate) - Common.Date.FromJson(item.SDate)) / (Common.Date.FromJson(item.EDate) - Common.Date.FromJson(item.SDate));
                        $(o).append("<span class='timeActual requestDate' style='left: calc(" + span * 100 + "% - 56px)'>Mới lập đơn hàng</span>");
                    }
                    if (Common.Date.FromJson(item.SDate) <= Common.Date.FromJson(data.CreatedDate) && Common.Date.FromJson(item.EDate) > Common.Date.FromJson(data.CreatedDate)) {
                        var span = (Common.Date.FromJson(data.CreatedDate) - Common.Date.FromJson(item.SDate)) / (Common.Date.FromJson(item.EDate) - Common.Date.FromJson(item.SDate));
                        $(o).append("<span class='timeActual createdDate' style='left: calc(" + span * 100 + "% - 56px)'>Hoàn tất kế hoạch</span>");
                    }
                    if (Common.HasValue(data.ATD) && Common.Date.FromJson(item.SDate) <= Common.Date.FromJson(data.ATD) && Common.Date.FromJson(item.EDate) > Common.Date.FromJson(data.ATD)) {
                        var span = (Common.Date.FromJson(data.ATD) - Common.Date.FromJson(item.SDate)) / (Common.Date.FromJson(item.EDate) - Common.Date.FromJson(item.SDate));
                        $(o).append("<span class='timeActual atdDate' style='left: calc(" + span * 100 + "% - 56px)'>Bắt đầu v/chuyển</span>");
                    }
                    if (Common.HasValue(data.ATA) && Common.Date.FromJson(item.SDate) <= Common.Date.FromJson(data.ATA) && Common.Date.FromJson(item.EDate) > Common.Date.FromJson(data.ATA)) {
                        var span = (Common.Date.FromJson(data.ATA) - Common.Date.FromJson(item.SDate)) / (Common.Date.FromJson(item.EDate) - Common.Date.FromJson(item.SDate));
                        $(o).append("<span class='timeActual ataDate' style='left: calc(" + span * 100 + "% - 56px)'>Hoàn tất v/chuyển</span>");
                    }
                    angular.forEach(data.ListLocation, function (obj) {
                        if (Common.HasValue(obj) && Common.HasValue(obj.DateTime) && Common.Date.FromJson(item.SDate) <= Common.Date.FromJson(obj.DateTime) && Common.Date.FromJson(item.EDate) > Common.Date.FromJson(obj.DateTime)) {
                            var span = (Common.Date.FromJson(obj.DateTime) - Common.Date.FromJson(item.SDate)) / (Common.Date.FromJson(item.EDate) - Common.Date.FromJson(item.SDate));
                            var className = '';
                            if ($(o).find('.timeActual').length > 0)
                                className = 'topLine';
                            if (obj.Status == 1) {
                                $(o).append("<span class='timeActual " + className + " stkDate' style='left: calc(" + span * 100 + "% - 36px)'>Đến kho " + sStk + "</span>");
                                sStk++;
                            } else {
                                $(o).append("<span class='timeActual " + className + " disDate' style='left: calc(" + span * 100 + "% - 36px)'>Đến npp " + sDis + "</span>");
                                sDis++;
                            }
                        }
                    })

                    if (Common.Date.FromJson(item.SDate) <= $scope.Now && Common.Date.FromJson(item.EDate) > $scope.Now) {
                        var span = ($scope.Now - Common.Date.FromJson(item.SDate)) / (Common.Date.FromJson(item.EDate) - Common.Date.FromJson(item.SDate));
                        $(o).append("<span class='timeNow' style='left:calc(" + span * 100 + "% - 1px);position:absolute;width:2px;height:300%;background:red;top:0;z-index:9999;'></span>");
                    }
                }
            });
        }, 100)
    }

    setInterval(function () {
        $scope.SetNow();
    }, 60000)

    $scope.Call_Click = function ($event) {
        $event.preventDefault();
     
    }

    $scope.ShowSetting = function ($event, grid) {
        $event.preventDefault();
        $rootScope.ShowSetting({
            grid: grid, event: $event,
            current: $state.current,
            ListView: views.ORDOrder
        });
    };

    $scope.HideSetting = function ($event) {
        $event.preventDefault();
        $rootScope.HideSetting();
    }
}]);