/// <reference path="~/Scripts/jquery-1.9.1.intellisense.js" />
/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />
/// <reference path="~/Scripts/map.js" />

//#region URL

var _ORDOrder_FCLIMEX = {
    URL: {
        GetItem: 'ORDOrder_GetItem',
        Data: 'ORDOrder_IMEX_Data',

        PriceContainer: 'ORDOrder_PriceContainer',
        TonCODefaul: 'ORDOrder_CODefault_List',
        COService: 'ORDOrder_ContainerService_List',

        Delete: 'ORDOrder_Delete',
        Update: 'ORDOrder_FCLIMEX_Save',

        Contract: 'ORDOrder_Contract_List',
        Contract_Change: 'ORDOrder_Contract_Change',

        OPS: 'ORDOrder_ToOPS',
        OPS_Check: 'ORDOrder_ToOPSCheck',
        Container_OPS_Check: 'ORDOrderContainer_ToOPSCheck',
        GetDate: 'ORDOrder_GetDate'
    },
    Data: {
        ItemProductNew: {
            ID: "", GroupOfProductID: "", GroupOfProductName: '', ProductID: "", ProductName: "", CBM: 0, Quantity: 0, Weight: 0, WeightKG: 0, SOCode: '',
            TempMin: null, TempMax: null,ContainerID:"",ContainerNo:""
        },
        ItemProductBackUp: null,
        ItemContainerBackUp: null,
        ListGroupProduct: null,
        ListProduct: null,

        ListDepot: [],
        ListStock: null,
        ListSeaPort: null,
        ListCarrier: null,

        ListContainer: null,
        ListService: null,
        ListVessel: null,
        ListContainerChoose:null,
    },
    Map: {
        Map: null,
        Info: null,
        Line: [],
        Marker: {
            Depot: [],
            Stock: [],
            SeaPort: []
        },
        VisibleLayers: []
    },
    Param: {
        OrderID: -1,
        CustomerID: -1,
        ServiceID: -1,
        TransportID: -1,
        ContractID: -1,
        IsExpired: false
    }
};

//#endregion

angular.module('myapp').controller('ORDOrder_FCLIMEXCtrl', ['$rootScope', '$scope', '$http', '$location', '$state', '$timeout', 'openMapV2', function ($rootScope, $scope, $http, $location, $state, $timeout, openMapV2) {
    if ($rootScope.IsPageComplete != true) return;
    Common.Log('ORDOrder_FCLIMEXCtrl');
    $rootScope.IsLoading = false;
    $rootScope.Loading.Show();
    $rootScope.Loading.Change("Khởi tạo...", 20);
    //QuickButton
    $rootScope.QuickAdd({
        Show: true,
        Call: function ($event) {
            $event.preventDefault();
            $state.go('main.ORDOrder.New');
            $scope.IsDetail = true;
        }
    });

    $scope.Auth = $rootScope.GetAuth();
    _ORDOrder_FCLIMEX.Param = $.extend(true, _ORDOrder_FCLIMEX.Param, $state.params);
    if (_ORDOrder_FCLIMEX.Param.OrderID == "" || !angular.isNumber(parseInt(_ORDOrder_FCLIMEX.Param.OrderID))
        || _ORDOrder_FCLIMEX.Param.CustomerID == "" || !angular.isNumber(parseInt(_ORDOrder_FCLIMEX.Param.CustomerID))
        || _ORDOrder_FCLIMEX.Param.ServiceID == "" || !angular.isNumber(parseInt(_ORDOrder_FCLIMEX.Param.ServiceID))
        || _ORDOrder_FCLIMEX.Param.TransportID == "" || !angular.isNumber(parseInt(_ORDOrder_FCLIMEX.Param.TransportID))
        || _ORDOrder_FCLIMEX.Param.ContractID == "" || !angular.isNumber(parseInt(_ORDOrder_FCLIMEX.Param.ContractID))
        || _ORDOrder_FCLIMEX.Param.TermID == "" || !angular.isNumber(parseInt(_ORDOrder_FCLIMEX.Param.TermID))) {
        $rootScope.Message({ Msg: 'Đường dẫn ko đúng! Quay về trang đơn hàng!' });
        $state.go("main.ORDOrder.Index");
    }
    $scope.Item = null;
    $scope.ProductEdit = null;
    $scope.ObjGroupProduct = null;
    $scope.ContainerEdit = null;
    $scope.NoContainer = 1;
    $scope.ContainerNew = {
        ID: "", LocationFromID: "", LocationToID: "", PackingID: "", LocationFromName: "", LocationToName: "", PackingName: "",
        ETD: new Date(), ETA: new Date().addDays(1), DateLoading: null, DateUnloading: null,
        LocationDepotID: null, LocationDepotName: '', LocationDepotReturnID: null, LocationDepotReturnName: '', VesselID: -1, VesselCode: "", VesselMasterCode: "",
        ContainerNo: "", SealNo1: "", SealNo2: "", Note: "", Ton: 0, LoadingTime: 2, UnLoadingTime: 2, ETARequest: null, ETDRequest: null,
        DateGetEmpty: new Date().addDays(-5), DateReturnEmpty: new Date().addDays(5), Note1: '', Note2: ''
    }
    $scope.DemurrageTime = 3;
    $scope.DetentionTime = 3;
    $scope.LoadingTime = 2;
    $scope.UnloadingTime = 2;

    $scope.IsAdmin = false;

    //#region View

    $scope.order_tabstripOptions = {
        animation: {
            open: {
                effects: "fadeIn"
            }
        },
        select: function (e) {
            var index = angular.element(e.item).data('tabindex');
            switch (index) {
                case 0:

                    break;

                case 1:
                    break;

                case 2:

                    break;
                case 3:

                    break;
                case 4:
                    $scope.LoadPriceContainer();
                    break;
                case 5:

                    break;
                default:
                    break;
            }
        },
        activate: function (e) {
            var index = angular.element(e.item).data('tabindex');
            switch (index) {
                case 0:

                    break;

                case 1:
                    break;

                case 2:

                    break;
                case 3:

                    break;
                case 4:

                    break;
                case 5:

                    break;
                default:
                    break;
            }
        }
    }

    $scope.hor_splitterOptions = {
        orientation: "horizontal",
        panes: [
            { collapsible: false, resizable: false, size: "500px" },
            { collapsible: false, resizable: false }
        ]
    };

    $rootScope.Loading.Change("Lấy dữ liệu địa điểm...", 20);

    Common.Services.Call($http, {
        url: Common.Services.url.ORD,
        method: _ORDOrder_FCLIMEX.URL.Data,
        data: { CustomerID: _ORDOrder_FCLIMEX.Param.CustomerID },
        success: function (res) {
            $scope.hor_splitter.resize();

            $rootScope.Loading.Change("Lấy dữ liệu đơn hàng...", 50);

            if (Common.HasValue(res)) {
                _ORDOrder_FCLIMEX.Data.ListStock = res.ListStock;
                _ORDOrder_FCLIMEX.Data.ListSeaPort = res.ListSeaPort;
                _ORDOrder_FCLIMEX.Data.ListGroupProduct = res.ListGroupProduct;
                if (res.ListGroupProduct.length > 0) {
                    _ORDOrder_FCLIMEX.Data.ItemProductNew.GroupOfProductID = res.ListGroupProduct[0].ID;
                    _ORDOrder_FCLIMEX.Data.ItemProductNew.GroupOfProductName = res.ListGroupProduct[0].GroupName;
                }

                _ORDOrder_FCLIMEX.Data.ListProduct = {};
                Common.Data.Each(res.ListProduct, function (o) {
                    if (!Common.HasValue(_ORDOrder_FCLIMEX.Data.ListProduct[o.GroupOfProductID])) {
                        _ORDOrder_FCLIMEX.Data.ListProduct[o.GroupOfProductID] = [o];
                    }
                    else {
                        _ORDOrder_FCLIMEX.Data.ListProduct[o.GroupOfProductID].push(o);
                    }
                })
                _ORDOrder_FCLIMEX.Data.ListVessel = res.ListVessel;
                var dataDoc = [];
                dataDoc.push({ ID: -1, ValueOfVar: ' ' });
                Common.Data.Each(res.ListTypeOfDoc, function (o) {
                    dataDoc.push(o);
                })
                $scope.DataDoc = dataDoc;
                $timeout(function () {
                    $scope.fclimex_cboTypeOfDocOptions.dataSource.data(dataDoc);
                }, 1)
                _ORDOrder_FCLIMEX.Data.ListGroupProduct.splice(0, 0, { ID: "", GroupName: "" });
                $timeout(function () {
                    $scope.cbbGroupOfProductOptions.dataSource.data(_ORDOrder_FCLIMEX.Data.ListGroupProduct);
                }, 1)
                _ORDOrder_FCLIMEX.Data.ListCarrier = res.ListCarrier;
                $timeout(function () {
                    $scope.fclimex_cboShippingCoOptions.dataSource.data(_ORDOrder_FCLIMEX.Data.ListCarrier);
                }, 1)
                Common.Data.Each(_ORDOrder_FCLIMEX.Data.ListCarrier, function (o) {
                    _ORDOrder_FCLIMEX.Data.ListDepot[o.CUSPartnerID] = [{
                        CUSLocationID: "", LocationName: ''
                    }];
                })
                Common.Data.Each(res.ListDepot, function (o) {
                    _ORDOrder_FCLIMEX.Data.ListDepot[o.CusPartID].push(o);
                })

                $timeout(function () {
                    $scope.cbo_coGrid_VesselOptions.dataSource.data(_ORDOrder_FCLIMEX.Data.ListVessel);
                }, 1)

                Common.Services.Call($http, {
                    url: Common.Services.url.ORD,
                    method: _ORDOrder_FCLIMEX.URL.GetItem,
                    data: { ID: _ORDOrder_FCLIMEX.Param.OrderID, CustomerID: _ORDOrder_FCLIMEX.Param.CustomerID, ServiceOfOrderID: _ORDOrder_FCLIMEX.Param.ServiceID, TransportModeID: _ORDOrder_FCLIMEX.Param.TransportID, ContractID: _ORDOrder_FCLIMEX.Param.ContractID, TermID: _ORDOrder_FCLIMEX.Param.TermID },
                    success: function (res) {
                        $rootScope.Loading.Change("Lấy dữ liệu đơn hàng...", 80);
                        switch (res.ViewID) {
                            default:
                            case 1://IM
                                $scope.fclimex_cboLocationFromOptions.dataSource.data(_ORDOrder_FCLIMEX.Data.ListSeaPort);
                                $scope.fclimex_cboLocationToOptions.dataSource.data(_ORDOrder_FCLIMEX.Data.ListStock);

                                $scope.cbo_CO_LocationFromOptions.dataSource.data(_ORDOrder_FCLIMEX.Data.ListSeaPort);
                                $scope.cbo_CO_LocationToOptions.dataSource.data(_ORDOrder_FCLIMEX.Data.ListStock);
                                var obj = _ORDOrder_FCLIMEX.Data.ListSeaPort[0];
                                if (Common.HasValue(obj)) {
                                    $scope.ContainerNew.LocationFromID = obj.CUSLocationID;
                                    $scope.ContainerNew.LocationFromName = obj.LocationName;
                                }
                                obj = _ORDOrder_FCLIMEX.Data.ListStock[0];
                                if (Common.HasValue(obj)) {
                                    $scope.ContainerNew.LocationToID = obj.CUSLocationID;
                                    $scope.ContainerNew.LocationToName = obj.LocationName;
                                }

                                $scope.cbo_coGrid_LocationFromOptions.dataSource.data(_ORDOrder_FCLIMEX.Data.ListSeaPort);
                                $scope.cbo_coGrid_LocationToOptions.dataSource.data(_ORDOrder_FCLIMEX.Data.ListStock);

                                $scope.container_grid.hideColumn("LocationFromID");
                                $scope.container_grid.hideColumn("LoadingTime");
                                $scope.container_grid.hideColumn("LocationDepotID");
                                $scope.container_grid.hideColumn("DateGetEmpty");
                                $scope.container_grid.hideColumn("DateLoading");
                                $scope.container_grid.hideColumn("ETD");
                                break;
                            case 2://EX
                                $scope.fclimex_cboLocationFromOptions.dataSource.data(_ORDOrder_FCLIMEX.Data.ListStock);
                                $scope.fclimex_cboLocationToOptions.dataSource.data(_ORDOrder_FCLIMEX.Data.ListSeaPort);

                                $scope.cbo_CO_LocationFromOptions.dataSource.data(_ORDOrder_FCLIMEX.Data.ListStock);
                                $scope.cbo_CO_LocationToOptions.dataSource.data(_ORDOrder_FCLIMEX.Data.ListSeaPort);
                                var obj = _ORDOrder_FCLIMEX.Data.ListStock[0];
                                if (Common.HasValue(obj)) {
                                    $scope.ContainerNew.LocationFromID = obj.CUSLocationID;
                                    $scope.ContainerNew.LocationFromName = obj.LocationName;
                                }
                                obj = _ORDOrder_FCLIMEX.Data.ListSeaPort[0];
                                if (Common.HasValue(obj)) {
                                    $scope.ContainerNew.LocationToID = obj.CUSLocationID;
                                    $scope.ContainerNew.LocationToName = obj.LocationName;
                                }

                                $scope.cbo_coGrid_LocationFromOptions.dataSource.data(_ORDOrder_FCLIMEX.Data.ListStock);
                                $scope.cbo_coGrid_LocationToOptions.dataSource.data(_ORDOrder_FCLIMEX.Data.ListSeaPort);

                                $scope.container_grid.hideColumn("LocationToID");
                                $scope.container_grid.hideColumn("UnLoadingTime");
                                $scope.container_grid.hideColumn("LocationDepotReturnID");
                                $scope.container_grid.hideColumn("DateReturnEmpty");
                                $scope.container_grid.hideColumn("DateUnloading");
                                $scope.container_grid.hideColumn("ETA");
                                break;
                        }


                        $scope.Item = res;
                        $scope.Item.CutOffTime = Common.Date.FromJson(res.CutOffTime);

                        $scope.Item.ETA = Common.Date.FromJson(res.ETA);
                        $scope.Item.ETD = Common.Date.FromJson(res.ETD);
                        $scope.Item.ETARequest = Common.Date.FromJson(res.ETARequest);
                        $scope.Item.ETDRequest = Common.Date.FromJson(res.ETDRequest);
                        $scope.Item.RequestDate = Common.Date.FromJson(res.RequestDate);

                        $scope.ContainerNew.ETA = Common.Date.FromJson(res.ETA);
                        $scope.ContainerNew.ETD = Common.Date.FromJson(res.ETD);
                        _ORDOrder_FCLIMEX.Data.ListContainerChoose=[{Text:' ',Value:-1}]
                        //Binding
                        if ($scope.Item.ID < 1) {
                            var objC = _ORDOrder_FCLIMEX.Data.ListCarrier[0];
                            if (Common.HasValue(objC)) {
                                $scope.Item.PartnerID = objC.CUSPartnerID;
                                if ($scope.Item.ViewID == 1) {
                                    $scope.ContainerNew.DateReturnEmpty = $scope.ContainerNew.ETD.addDays(objC.DemurrageTime);
                                    if (Common.HasValue($scope.Item.ETD) && $scope.Item.ETD != '')
                                        $scope.Item.CutOffTime = $scope.Item.ETD.addDays(objC.DemurrageTime);
                                }
                                if ($scope.Item.ViewID == 2) {
                                    $scope.ContainerNew.DateGetEmpty = null;
                                    if (Common.HasValue($scope.Item.CutOffTime) && $scope.Item.CutOffTime != '')
                                        $scope.ContainerNew.DateGetEmpty = $scope.Item.CutOffTime.addDays(-objC.DemurrageTime);
                                }
                            }
                            switch ($scope.Item.ViewID) {
                                default:
                                case 1://IM
                                    var obj = _ORDOrder_FCLIMEX.Data.ListSeaPort[0];
                                    if (Common.HasValue(obj)) {
                                        $scope.Item.LocationFromID = obj.CUSLocationID;
                                        $scope.Item.LocationFromName = obj.LocationName;
                                    }
                                    obj = _ORDOrder_FCLIMEX.Data.ListStock[0];
                                    if (Common.HasValue(obj)) {
                                        $scope.Item.LocationToID = obj.CUSLocationID;
                                        $scope.Item.LocationToName = obj.LocationName;
                                    }
                                    break;
                                case 2://EX
                                    var obj = _ORDOrder_FCLIMEX.Data.ListStock[0];
                                    if (Common.HasValue(obj)) {
                                        $scope.Item.LocationFromID = obj.CUSLocationID;
                                        $scope.Item.LocationFromName = obj.LocationName;
                                    }
                                    obj = _ORDOrder_FCLIMEX.Data.ListSeaPort[0];
                                    if (Common.HasValue(obj)) {
                                        $scope.Item.LocationToID = obj.CUSLocationID;
                                        $scope.Item.LocationToName = obj.LocationName;
                                    }
                                    break;
                            }
                            $scope.product_grid.hideColumn("ContainerID");
                        } else {
                            //SER
                            //var objService = [];
                            //$.each($scope.Item.ListService, function (i, v) {
                            //    objService[v.ServiceID] = v.Price || 0;
                            //});
                            //$.each(_ORDOrder_FCLIMEX.Data.ListService, function (i, v) {
                            //    if (objService[v.ID] >= 0) {
                            //        v.IsChoose = true;
                            //        v.Price = objService[v.ID];
                            //    }
                            //});
                            //$timeout(function () {
                            //    $scope.service_gridOptions.dataSource.data(_ORDOrder_FCLIMEX.Data.ListService);
                            //}, 1);
                            //GOP

                            Common.Data.Each(res.ListContainer, function (o) {
                                _ORDOrder_FCLIMEX.Data.ListContainerChoose.push({ Text: o.ContainerNo, Value: o.ID });
                            })

                            $timeout(function () {
                                $scope.product_gridOptions.dataSource.data($scope.Item.ListProduct);
                            }, 1);
                            //RATE
                            $timeout(function () {
                                $scope.price_gridOptions.dataSource.data($scope.Item.ListContainerPrice);
                            }, 1);
                            //CON
                            $timeout(function () {
                                $scope.container_gridOptions.dataSource.data($scope.Item.ListContainer);
                                Common.Log($scope.Item.ListContainer)
                            }, 1);
                            $scope.product_grid.showColumn("ContainerID");
                        }

                        $scope.cbbContainerOptions.dataSource.data(_ORDOrder_FCLIMEX.Data.ListContainerChoose);

                        //CreateMap
                        $scope.CreateMap();
                        $scope.LoadDepot(false);
                        $timeout(function () {
                            openMapV2.FitBound("VectorRoute", 15);
                            
                        }, 100)
                        $rootScope.Loading.Hide();
                    }
                });
            }
        }
    });

    $scope.fclimex_cboShippingCoOptions = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true,
        dataTextField: 'PartnerName', dataValueField: 'CUSPartnerID', minLength: 3,
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'CUSPartnerID',
                fields: {
                    CUSPartnerID: { type: 'number' },
                    PartnerName: { type: 'string' },
                }
            }
        }),
        change: function (e) {
            var item = this.dataItem(this.select());
            if (Common.HasValue(item)) {
                $scope.DemurrageTime = item.DemurrageTime > 0 ? item.DemurrageTime : 3;
                $scope.DetentionTime = item.DetentionTime > 0 ? item.DetentionTime : 3;

                var data = $scope.container_gridOptions.dataSource.data();

                if ($scope.Item.ViewID == 1) {
                    $scope.ContainerNew.DateReturnEmpty = $scope.ContainerNew.ETD.addDays(item.DemurrageTime);
                    if (Common.HasValue($scope.Item.ETD) && $scope.Item.ETD != '')
                        $scope.Item.CutOffTime = $scope.Item.ETD.addDays(item.DemurrageTime);
                    Common.Data.Each(data, function (o) {
                        if (Common.HasValue(o.ETD) && $o.ETD != '')
                            o.DateReturnEmpty = o.ETD.addDays(item.DemurrageTime);
                    })
                    $scope.container_gridOptions.dataSource.sync();
                }
                if ($scope.Item.ViewID == 2) {
                    $scope.ContainerNew.DateGetEmpty = null;
                    if (Common.HasValue($scope.Item.CutOffTime) && $scope.Item.CutOffTime != '') {
                        var dateGet = $scope.Item.CutOffTime.addDays(-item.DemurrageTime);
                        $scope.ContainerNew.DateGetEmpty = dateGet;

                        Common.Data.Each(data, function (o) {
                                o.DateGetEmpty = dateGet;
                        })
                        $scope.container_gridOptions.dataSource.sync();
                    }
                }
                $scope.LoadDepot(true);
                $timeout(function () {
                    $scope.ReloadMap();
                }, 10)
            }
            else {
                $rootScope.Message({ Msg: "Không xác định." });
            }
        }
    }

    $scope.fclimex_cboLocationDepotOptions = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true,
        dataTextField: 'LocationName', dataValueField: 'CUSLocationID', minLength: 3,
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'CUSLocationID',

                fields: {
                    CUSLocationID: { type: 'number' },
                    LocationName: { type: 'string' },
                }
            }

        }),
        change: function (e) {
            var obj = this.dataItem(this.select());
            if (!Common.HasValue(obj)) {
                $rootScope.Message({ Msg: "Không xác định." });
            } else {
                $scope.ContainerNew.LocationDepotID = obj.CUSLocationID;
                $scope.ContainerNew.LocationDepotReturnID = obj.CUSLocationID;
            }
            $timeout(function () {
                $scope.ReloadMap();
            }, 1)
        }
    }

    $scope.LoadDepot = function (flag) {
        Common.Log("LoadDepot")
        var partnerID = $scope.Item.PartnerID;
        $timeout(function () {
            var data = _ORDOrder_FCLIMEX.Data.ListDepot[partnerID];
            if (!Common.HasValue(data) || data == '')
                data = [];
            $scope.fclimex_cboLocationDepotOptions.dataSource.data(data);
            $scope.cbo_coGrid_LocationDepotOptions.dataSource.data(data);
            $scope.cbo_coGrid_LocationDepotReturnOptions.dataSource.data(data);
            $scope.cbo_CO_LocationDepotOptions.dataSource.data(data);
            $scope.cbo_CO_LocationDepotReturnOptions.dataSource.data(data);
            $scope.cbo_CO_LocationDepot.select(1);
            if (data.length > 1) {

                if (flag) {
                    // $scope.fclimex_cboLocationDepot.select(1);
                    var obj = data[1];
                    $scope.Item.LocationDepotID = obj.CUSLocationID;
                    var dataG = $scope.container_gridOptions.dataSource.data();
                    try {
                        switch ($scope.Item.ViewID) {
                            default:
                            case 1:
                                Common.Data.Each(dataG, function (o) {
                                    o.LocationDepotReturnID = $scope.Item.LocationDepotID;
                                    o.LocationDepotReturnName = obj.LocationName;
                                    if (Common.HasValue(o.ETD)) {
                                        if (typeof (o.ETD) == 'string') {
                                            o.ETD = Common.Date.FromJson(o.ETD);
                                        }
                                        o.DateReturnEmpty = o.ETD.addDays($scope.DemurrageTime);
                                    }
                                })
                                break;
                            case 2:
                                Common.Data.Each(dataG, function (o) {
                                    o.LocationDepotID = $scope.Item.LocationDepotID;
                                    o.LocationDepotName = obj.LocationName;
                                    if (Common.HasValue(o.ETA)) {
                                        if (typeof (o.ETA) == 'string') {
                                            o.ETA = Common.Date.FromJson(o.ETA);
                                        }
                                        o.DateGetEmpty = o.ETA.addDays(0 - $scope.DetentionTime);
                                    }
                                })
                                break;
                        }
                    } catch (e) { }
                    $scope.container_gridOptions.dataSource.sync();
                }
                else {
                    if ($scope.Item.ID < 1) {
                        var objF = data[1];
                        $scope.Item.LocationDepotID = objF.CUSLocationID;
                    }
                }
            } else {

                if (flag) {
                    // $scope.fclimex_cboLocationDepot.select(0);
                    var obj = data[0];
                    $scope.Item.LocationDepotID = obj.CUSLocationID;
                    var data = $scope.container_gridOptions.dataSource.data();
                    try {
                        switch ($scope.Item.ViewID) {
                            default:
                            case 1:
                                Common.Data.Each(data, function (o) {
                                    o.LocationDepotReturnID = $scope.Item.LocationDepotID;
                                    o.LocationDepotReturnName = obj.LocationName;
                                    if (Common.HasValue(o.ETD)) {
                                        if (typeof (o.ETD) == 'string') {
                                            o.ETD = Common.Date.FromJson(o.ETD);
                                        }
                                        o.DateReturnEmpty = o.ETD.addDays($scope.DemurrageTime);
                                    }
                                })
                                break;
                            case 2:
                                Common.Data.Each(data, function (o) {
                                    o.LocationDepotID = $scope.Item.LocationDepotID;
                                    o.LocationDepotName = obj.LocationName;
                                    if (Common.HasValue(o.ETA)) {
                                        if (typeof (o.ETA) == 'string') {
                                            o.ETA = Common.Date.FromJson(o.ETA);
                                        }
                                        o.DateGetEmpty = o.ETA.addDays(0 - $scope.DetentionTime);
                                    }
                                })
                                break;
                        }
                    } catch (e) { }
                    $scope.container_gridOptions.dataSource.sync();
                }
                else {
                    if ($scope.Item.ID < 1) {
                        var obj = data[0];
                        if (Common.HasValue(obj) && obj != '')
                            $scope.Item.LocationDepotID = obj.CUSLocationID;
                        else $scope.Item.LocationDepotID = "";
                    }
                }
            }
            $scope.cbo_CO_LocationDepotReturn.select(1);
        }, 100)
    }

    $scope.fclimex_cboLocationFromOptions = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true,
        dataTextField: 'LocationName', dataValueField: 'CUSLocationID', minLength: 3,
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'CUSLocationID',
                fields: {
                    CUSLocationID: { type: 'number' },
                    LocationName: { type: 'string' },
                }
            }
        }),
        change: function (e) {
            var obj = this.dataItem(this.select());
            if (!Common.HasValue(obj)) {
                $rootScope.Message({ Msg: "Không xác định." });
            } else {
                $scope.ContainerNew.LocationFromID = obj.CUSLocationID;
                $scope.Item.LocationFromName = obj.LocationName;

                var locationDepotReturnID = -1;
                var locationDepotReturnName = "";
                if ($scope.Item.ViewID == 1) {
                    var data = _ORDOrder_FCLIMEX.Data.ListDepot[$scope.Item.PartnerID];
                    if (Common.HasValue(data) && data.length > 0) {
                        for (var i = 0; i < data.length; i++) {
                            var temp = data[i];
                            if (temp.LocationID == obj.LocationID) {
                                locationDepotReturnID = temp.CUSLocationID;
                                locationDepotReturnName = temp.LocationName;
                                break;
                            }
                        }
                    }
                    if (locationDepotReturnID > 0) {
                        $scope.ContainerNew.LocationDepotReturnID = locationDepotReturnID;
                    }
                }

                $timeout(function () {
                    $scope.ReloadMap();

                    Common.Data.Each($scope.container_grid.dataSource.data(), function (o) {
                        o.LocationFromID = $scope.Item.LocationFromID;
                        o.LocationFromName = $scope.Item.LocationFromName;
                        o.LocationToID = $scope.Item.LocationToID;
                        o.LocationToName = $scope.Item.LocationToName;
                        if ($scope.Item.ViewID == 1 && locationDepotReturnID > 0) {
                            o.LocationDepotReturnID = locationDepotReturnID;
                            o.LocationDepotReturnName = locationDepotReturnName;
                        }
                    })

                    $scope.container_grid.dataSource.sync();
                }, 100)
            }
        }
    }

    $scope.fclimex_cboLocationToOptions = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true,
        dataTextField: 'LocationName', dataValueField: 'CUSLocationID', minLength: 3,
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'CUSLocationID',
                fields: {
                    CUSLocationID: { type: 'number' },
                    LocationName: { type: 'string' },
                }
            }
        }),
        change: function (e) {
            var obj = this.dataItem(this.select());
            if (!Common.HasValue(obj)) {
                $rootScope.Message({ Msg: "Không xác định." });
            } else {
                $scope.ContainerNew.LocationToID = obj.CUSLocationID;
                $scope.Item.LocationToName = obj.LocationName;

                var locationDepotID = -1;
                var locationDepotName = "";
                if ($scope.Item.ViewID == 2) {
                    var data = _ORDOrder_FCLIMEX.Data.ListDepot[$scope.Item.PartnerID];
                    if (Common.HasValue(data) && data.length > 0) {
                        for (var i = 0; i < data.length; i++) {
                            var temp = data[i];
                            if (temp.LocationID == obj.LocationID) {
                                locationDepotID = temp.CUSLocationID;
                                locationDepotName = temp.LocationName;
                                break;
                            }
                        }
                    }
                    if (locationDepotID > 0) {
                        $scope.ContainerNew.LocationDepotID = locationDepotID;
                    }
                }

                $timeout(function () {
                    $scope.ReloadMap();

                    Common.Data.Each($scope.container_grid.dataSource.data(), function (o) {
                        o.LocationFromID = $scope.Item.LocationFromID;
                        o.LocationFromName = $scope.Item.LocationFromName;
                        o.LocationToID = $scope.Item.LocationToID;
                        o.LocationToName = $scope.Item.LocationToName;
                        if ($scope.Item.ViewID == 2 && locationDepotID > 0) {
                            o.LocationDepotID = locationDepotID;
                            o.LocationDepotName = locationDepotName;
                        }
                    })
                    $scope.container_grid.dataSource.sync();
                }, 100)
            }

        }
    }

    $scope.fclimex_dtpCutOffTimeOptions = {
        format: 'dd/MM/yyyy HH:mm',
        timeFormat: "HH:mm",
        parseFormats: ["yyyy-MM-ddTHH:mm:ss"],
        change: function (e) {
            var date = this.value();
            var data = $scope.container_gridOptions.dataSource.data();
            var objCarrier = $scope.fclimex_cboShippingCo.dataItem($scope.fclimex_cboShippingCo.select());

            if (Common.HasValue(objCarrier) && objCarrier != '') {
                if ($scope.Item.ViewID == 2) {
                    $scope.ContainerNew.DateGetEmpty = null;
                    if (Common.HasValue($scope.Item.CutOffTime) && $scope.Item.CutOffTime != '')
                        $scope.ContainerNew.DateGetEmpty = $scope.Item.CutOffTime.addDays(-objCarrier.DemurrageTime);
                }
            }

            Common.Data.Each(data, function (o) {
                if ($scope.Item.ViewID == 2&&Common.HasValue(objCarrier) && objCarrier != '') {
                    o.DateGetEmpty = date.addDays(-objCarrier.DemurrageTime);
                }
            })
            $scope.container_gridOptions.dataSource.sync();
        }
    }

    $scope.fcllo_dtpRequestDateOptions = {
        format: 'dd/MM/yyyy HH:mm',
        timeFormat: "HH:mm",
        parseFormats: ["yyyy-MM-ddTHH:mm:ss"],
        change: function (e) {
            var date = this.value();

            if ($scope.Item.ContractTermID > 0) {
                $rootScope.IsLoading = true;
                Common.Services.Call($http, {
                    url: Common.Services.url.ORD,
                    method: _ORDOrder_FCLLO.URL.GetDate,
                    data: { item: $scope.Item },
                    success: function (res) {
                        res.ETA = Common.Date.FromJson(res.ETA);
                        res.ETD = Common.Date.FromJson(res.ETD);
                        res.ETARequest = Common.Date.FromJson(res.ETARequest);
                        res.ETDRequest = Common.Date.FromJson(res.ETDRequest);

                        $scope.Item.ETA = res.ETA;
                        $scope.Item.ETD = res.ETD;
                        $scope.Item.ETARequest = res.ETARequest;
                        $scope.Item.ETDRequest = res.ETDRequest;

                        $scope.ContainerNew.ETA = res.ETA;
                        $scope.ContainerNew.ETD = res.ETD;

                        var objCarrier = $scope.fclimex_cboShippingCo.dataItem($scope.fclimex_cboShippingCo.select());

                        if (Common.HasValue(objCarrier) && objCarrier != '') {
                            if ($scope.Item.ViewID == 1) {
                                $scope.ContainerNew.DateReturnEmpty = $scope.ContainerNew.ETD.addDays(objCarrier.DemurrageTime);
                                if (Common.HasValue($scope.Item.ETD) && $scope.Item.ETD != '')
                                    $scope.Item.CutOffTime = $scope.Item.ETD.addDays(objCarrier.DemurrageTime);
                            }
                            if ($scope.Item.ViewID == 2) {
                                $scope.ContainerNew.DateGetEmpty = null;
                                if (Common.HasValue($scope.Item.CutOffTime) && $scope.Item.CutOffTime != '')
                                    $scope.ContainerNew.DateGetEmpty = $scope.Item.CutOffTime.addDays(-objCarrier.DemurrageTime);
                            }
                        }

                        var data = $scope.container_gridOptions.dataSource.data();
                        Common.Data.Each(data, function (o) {
                            o.ETA = res.ETA;
                            o.ETD = res.ETD;
                            if (Common.HasValue(objCarrier) && objCarrier != '') {
                                if ($scope.Item.ViewID == 1) {
                                    o.DateReturnEmpty = o.ETD.addDays(objCarrier.DemurrageTime)
                                }
                                if ($scope.Item.ViewID == 1) {
                                    o.DateGetEmpty = $scope.Item.CutOffTime.addDays(-objCarrier.DemurrageTime);
                                }
                            }
                            // o.DateReturnEmpty = res.ETA.addDays(1 / 24);
                        })
                        $scope.container_gridOptions.dataSource.sync();
                        $rootScope.IsLoading = false;
                    },
                    error: function (res) {
                        $rootScope.IsLoading = false;
                    }
                });
            }
        }
    }

    $scope.fclimex_dtpETDOptions = {
        format: 'dd/MM/yyyy HH:mm',
        timeFormat: "HH:mm",
        parseFormats: ["yyyy-MM-ddTHH:mm:ss"],
        change: function (e) {
            var date = this.value();
            $scope.ContainerNew.ETD = date;

            var data = $scope.container_gridOptions.dataSource.data();
            var objCarrier = $scope.fclimex_cboShippingCo.dataItem($scope.fclimex_cboShippingCo.select());

            if (Common.HasValue(objCarrier) && objCarrier != '') {
                if ($scope.Item.ViewID == 1) {
                    $scope.ContainerNew.DateReturnEmpty = $scope.ContainerNew.ETD.addDays(objCarrier.DemurrageTime);
                    if (Common.HasValue(date) && date != '')
                        $scope.Item.CutOffTime = date.addDays(objCarrier.DemurrageTime);
                }
            }

            if ($scope.Item.ContractTermID > 0) {
                $rootScope.IsLoading = true;
                Common.Services.Call($http, {
                    url: Common.Services.url.ORD,
                    method: _ORDOrder_FCLLO.URL.GetDate,
                    data: { item: $scope.Item },
                    success: function (res) {

                        res.ETA = Common.Date.FromJson(res.ETA);
                        res.ETD = Common.Date.FromJson(res.ETD);
                        res.ETARequest = Common.Date.FromJson(res.ETARequest);
                        res.ETDRequest = Common.Date.FromJson(res.ETDRequest);

                        $scope.Item.ETA = res.ETA;
                        $scope.Item.ETARequest = res.ETARequest;
                        $scope.Item.ETDRequest = res.ETDRequest;
                        $scope.ContainerNew.ETA = res.ETA;


                        Common.Data.Each(data, function (o) {
                            o.ETA = res.ETA;
                            o.ETD = date;
                            if ($scope.Item.ViewID == 1 && Common.HasValue(objCarrier) && objCarrier != '') {
                                o.DateReturnEmpty = o.ETD.addDays(objCarrier.DemurrageTime)
                            }
                        })
                        $scope.container_gridOptions.dataSource.sync();
                        $rootScope.IsLoading = false;
                    },
                    error: function (res) {
                        $rootScope.IsLoading = false;
                    }
                });
            }
            else {
                Common.Data.Each(data, function (o) {
                    o.ETD = date;
                    if ($scope.Item.ViewID == 1 && Common.HasValue(objCarrier) && objCarrier != '') {
                        o.DateReturnEmpty = o.ETD.addDays(objCarrier.DemurrageTime)
                    }
                })
                $scope.container_gridOptions.dataSource.sync();
            }
            
        }
    }

    $scope.fclimex_dtpETAOptions = {
        format: 'dd/MM/yyyy HH:mm',
        timeFormat: "HH:mm",
        parseFormats: ["yyyy-MM-ddTHH:mm:ss"],
        change: function (e) {
            var date = this.value();
            var data = $scope.container_gridOptions.dataSource.data();
            Common.Data.Each(data, function (o) {
                o.ETA = date;
            })
            $scope.ContainerNew.ETA = date;
            $scope.container_gridOptions.dataSource.sync();
        }
    }

    $scope.fclimex_dtpETARequestOptions = {
        format: 'dd/MM/yyyy HH:mm',
        timeFormat: "HH:mm",
        parseFormats: ["yyyy-MM-ddTHH:mm:ss"],
        change: function (e) {
            var date = this.value();
            var data = $scope.container_gridOptions.dataSource.data();
            Common.Data.Each(data, function (o) {
                o.ETARequest = date;
            })
            $scope.ContainerNew.ETARequest = date;
            $scope.container_gridOptions.dataSource.sync();
        }
    }

    $scope.fclimex_dtpETDRequestOptions = {
        format: 'dd/MM/yyyy HH:mm',
        timeFormat: "HH:mm",
        parseFormats: ["yyyy-MM-ddTHH:mm:ss"],
        change: function (e) {
            var date = this.value();
            var data = $scope.container_gridOptions.dataSource.data();
            Common.Data.Each(data, function (o) {
                o.ETDRequest = date;
            })
            $scope.ContainerNew.ETDRequest = date;
            $scope.container_gridOptions.dataSource.sync();
        }
    }

    $scope.fclimex_cboTypeOfDocOptions = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true,
        dataTextField: 'ValueOfVar', dataValueField: 'ID', minLength: 3,
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    ValueOfVar: { type: 'string' },
                }
            }
        }), change: function (e) {
            if (this.select() == -1) {
                $rootScope.Message({ Msg: "Kiểm hóa không xác định" });
                $scope.Item.TypeOfWAInspectionID = -1;
            }

        }
    }

    $scope.numUnLoadingTime_Options = {
        format: 'n2', spinners: false, culture: 'en-US', min: 0, step: 1, decimals: 2,
        change: function (e) {
            var val = this.value();
            $scope.ContainerNew.UnLoadingTime = val;
            var data = $scope.container_gridOptions.dataSource.data();
            Common.Data.Each(data, function (o) {
                o.UnLoadingTime = val;
            })
            $scope.container_gridOptions.dataSource.sync();
        }
    }
    $scope.numLoadingTime_Options = {
        format: 'n2', spinners: false, culture: 'en-US', min: 0, step: 1, decimals: 2,
        change: function (e) {
            var val = this.value();
            $scope.ContainerNew.LoadingTime = val;
            var data = $scope.container_gridOptions.dataSource.data();
            Common.Data.Each(data, function (o) {
                o.LoadingTime = val;
            })
            $scope.container_gridOptions.dataSource.sync();
        }
    }

    //Tab container
    $scope.container_gridOptions = {
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    PackingName: { type: 'string' },
                    ETD: { type: 'date' },
                    ETA: { type: 'date' },
                    ETARequest: { type: 'date' },
                    ETDRequest: { type: 'date' },
                    DateGetEmpty: { type: 'date' },
                    DateReturnEmpty: { type: 'date' },
                    LocationFromName: { type: 'string' },
                    LocationToName: { type: 'string' },
                    LoadingTime: { type: 'number' },
                    UnLoadingTime: { type: 'number' },
                }
            },
            pageSize: 0
        }),
        editable: { mode: 'inline' }, height: '100%', pageable: false, sortable: false, columnMenu: false, filterable: false, resizable: false,
        toolbar: kendo.template($('#container_grid_toolbar').html()),
        columns: [
            {
                title: ' ', width: '85px',
                template: '<a ng-show="!IsContainerEdit" href="/" ng-click="ORD_FCLIMEX_Container_Edit($event,container_grid)" class="k-button"><i class="fa fa-pencil"></i></a>' +
                    '<a ng-show="!IsContainerEdit" href="/" ng-click="ORD_FCLIMEX_Container_Delete($event,container_grid)" class="k-button"><i class="fa fa-trash"></i></a>' +
                    '<a ng-show="IsContainerEdit && ContainerEdit.ID==#=ID#?true:false" href="/" ng-click="ORD_FCLIMEX_Container_Save($event,container_grid)" class="k-button"><i class="fa fa-check"></i></a>' +
                    '<a ng-show="IsContainerEdit && ContainerEdit.ID==#=ID#?true:false" href="/" ng-click="ORD_FCLIMEX_Container_Cancel($event,container_grid)" class="k-button"><i class="fa fa-ban"></i></a>',
                filterable: false, sortable: false
            },
            {
                field: 'PackingID', width: 120, title: '{{RS.ORDContainer.PackingID}}', template: '#=PackingName#', sortable: true,
                editor: '<input class="cus-combobox" focus-k-combobox kendo-combobox k-options="cbo_coGrid_PackingOptions" data-bind="value:PackingID" k-ng-model="ContainerEdit.PackingID"/>'
            },
            {
                field: 'LocationFromID', width: 170, title: '{{RS.ORDContainer.LocationFromID}}', template: '#=LocationFromName#', sortable: true,
                editor: '<input class="cus-combobox" focus-k-combobox kendo-combobox k-options="cbo_coGrid_LocationFromOptions" data-bind="value:LocationFromID" k-ng-model="ContainerEdit.LocationFromID"/>'
            },
            {
                field: 'LocationToID', width: 170, title: '{{RS.ORDContainer.LocationToID}}', template: '#=LocationToName#', sortable: true,
                editor: '<input class="cus-combobox" focus-k-combobox kendo-combobox k-options="cbo_coGrid_LocationToOptions" data-bind="value:LocationToID" k-ng-model="ContainerEdit.LocationToID"/>'
            },
            {
                field: 'LocationDepotID', width: 170, title: '{{RS.ORDContainer.LocationDepotID}}', template: '#=LocationDepotName#', sortable: true,
                editor: '<input class="cus-combobox" focus-k-combobox kendo-combobox k-options="cbo_coGrid_LocationDepotOptions" data-bind="value:LocationDepotID" k-ng-model="ContainerEdit.LocationDepotID"/>'
            },
            {
                field: 'LocationDepotReturnID', width: 170, title: '{{RS.ORDContainer.LocationDepotReturnID}}', template: '#=LocationDepotReturnName#', sortable: true,
                editor: '<input class="cus-combobox" focus-k-combobox kendo-combobox k-options="cbo_coGrid_LocationDepotReturnOptions" data-bind="value:LocationDepotReturnID" k-ng-model="ContainerEdit.LocationDepotReturnID"/>'
            },
            {
                field: 'DateGetEmpty', width: 170, title: '{{RS.ORDContainer.DateGetEmpty}}', template: '#=Common.Date.FromJsonDMYHM(DateGetEmpty)#', sortable: true,
                editor: '<input class="cus-datetimepicker" focus-k-datetimepicker kendo-date-time-picker k-options="DateDMYHM" data-bind="value:DateGetEmpty" k-ng-model="ContainerEdit.DateGetEmpty"/>'
            },
            {
                field: 'DateReturnEmpty', width: 170, title: '{{RS.ORDContainer.DateReturnEmpty}}', template: '#=Common.Date.FromJsonDMYHM(DateReturnEmpty)#', sortable: true,
                editor: '<input class="cus-datetimepicker" focus-k-datetimepicker kendo-date-time-picker k-options="DateDMYHM" data-bind="value:DateReturnEmpty" k-ng-model="ContainerEdit.DateReturnEmpty"/>'
            },
            {
                field: 'DateLoading', width: 170, title: '{{RS.ORDContainer.DateLoading}}', template: '#=Common.Date.FromJsonDMYHM(DateLoading)#', sortable: true,
                editor: '<input class="cus-datetimepicker" focus-k-datetimepicker kendo-date-time-picker k-options="DateDMYHM" data-bind="value:DateLoading" k-ng-model="ContainerEdit.DateLoading"/>'
            },
            {
                field: 'DateUnloading', width: 170, title: '{{RS.ORDContainer.DateUnloading}}', template: '#=DateUnloading==null?"":Common.Date.FromJsonDMYHM(DateUnloading)#', sortable: true,
                editor: '<input class="cus-datetimepicker" focus-k-datetimepicker kendo-date-time-picker k-options="DateDMYHM" data-bind="value:DateUnloading" k-ng-model="ContainerEdit.DateUnloading"/>'
            },
            {
                field: 'LoadingTime', width: 100, title: '{{RS.ORDContainer.LoadingTime}}', editor: '<input k-ng-model="ContainerEdit.LoadingTime" kendo-numeric-text-box k-options="coNumLoadingTime_Options" style="width: 100%;" ></input>'
            },
            {
                field: 'UnLoadingTime', width: 100, title: '{{RS.ORDContainer.UnLoadingTime}}', editor: '<input k-ng-model="ContainerEdit.UnLoadingTime" kendo-numeric-text-box k-options="coNumUnLoadingTime_Options" style="width: 100%;" ></input>'
            },
            {
                field: 'ETD', width: 170, title: '{{RS.ORDContainer.ETD}}', template: '#=Common.Date.FromJsonDMYHM(ETD)#', sortable: true,
                editor: '<input class="cus-datetimepicker" focus-k-datetimepicker kendo-date-time-picker k-options="DateDMYHM" data-bind="value:ETD" k-ng-model="ContainerEdit.ETD"/>'
            },
            {
                field: 'ETA', width: 170, title: '{{RS.ORDContainer.ETA}}', template: '#=Common.Date.FromJsonDMYHM(ETA)#', sortable: true,
                editor: '<input class="cus-datetimepicker" focus-k-datetimepicker kendo-date-time-picker k-options="DateDMYHM" data-bind="value:ETA" k-ng-model="ContainerEdit.ETA"/>'
            },
            {
                field: 'ContainerNo', width: 100, title: '{{RS.ORDContainer.ContainerNo}}', editor: '<input ng-model="ContainerEdit.ContainerNo" class="k-textbox" type="text" style="width: 100%;" ></input>'
            },
            {
                field: 'Ton', width: 100, title: '{{RS.ORDContainer.Ton}}', editor: '<input ng-model="ContainerEdit.Ton" class="cus-number" kendo-numeric-text-box k-options="Number" min="0" style="width: 100%;" ></input>'
            },
            {
                field: 'SealNo1', width: 100, title: '{{RS.ORDContainer.SealNo1}}', editor: '<input ng-model="ContainerEdit.SealNo1" class="k-textbox" type="text" style="width: 100%;" ></input>'
            },
            {
                field: 'SealNo2', width: 100, title: '{{RS.ORDContainer.SealNo2}}', editor: '<input ng-model="ContainerEdit.SealNo2" class="k-textbox" type="text" style="width: 100%;" ></input>'
            },
            {
                field: 'VesselID', width: 170, title: '{{RS.ORDContainer.VesselID}}', template: '#=VesselCode#', sortable: true,
                editor: '<input class="cus-combobox" focus-k-combobox kendo-combobox k-options="cbo_coGrid_VesselOptions" data-bind="value:VesselID" k-ng-model="ContainerEdit.VesselID"/>'
            },
            {
                field: 'VesselMasterCode', title: '{{RS.ORDContainer.VesselMasterCode}}', width: 150, editor: '<input ng-model="ContainerEdit.VesselMasterCode" class="k-textbox" type="text" style="width: 100%;" ></input>'
            },
            {
                field: 'Note', title: '{{RS.ORDContainer.Note}}', width: 300, editor: '<input ng-model="ContainerEdit.Note" class="k-textbox" type="text" style="width: 100%;" ></input>'
            },
            {
                field: 'Note1', title: '{{RS.ORDContainer.Note1}}', width: 300, editor: '<input ng-model="ContainerEdit.Note1" class="k-textbox" type="text" style="width: 100%;" ></input>'
            },
            {
                field: 'Note2', title: '{{RS.ORDContainer.Note2}}', width: 300, editor: '<input ng-model="ContainerEdit.Note2" class="k-textbox" type="text" style="width: 100%;" ></input>'
            },
            { title: ' ', filterable: false, sortable: false }
        ]
    }

    $scope.IsContainerEdit = false;

    $scope.Number = { format: 'n6', spinners: false, culture: 'en-US', min: 0, step: 0.00001, decimals: 6 }

    $scope.cbo_CO_PackingOptions = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true,
        dataTextField: 'PackingName', dataValueField: 'ID', minLength: 3,
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    PackingName: { type: 'string' },
                }
            }
        }),
        change: function (e) {
            var obj = this.dataItem(this.select());
            if (Common.HasValue(obj)) {
                $scope.ContainerNew.PackingName = obj.PackingName;
            }
        }
    }

    $scope.cbo_CO_LocationFromOptions = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true,
        dataTextField: 'LocationName', dataValueField: 'CUSLocationID', minLength: 3,
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'CUSLocationID',
                fields: {
                    CUSLocationID: { type: 'number' },
                    LocationName: { type: 'string' },
                }
            }
        }),
        change: function (e) {
            var obj = this.dataItem(this.select());
            if (Common.HasValue(obj)) {
                $scope.ContainerNew.LocationFromName = obj.LocationName;
                $scope.ContainerNew.LoadingTime = obj.LoadTimeCO;
            }
        }
    }

    $scope.cbo_CO_LocationToOptions = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true,
        dataTextField: 'LocationName', dataValueField: 'CUSLocationID', minLength: 3,
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'CUSLocationID',
                fields: {
                    CUSLocationID: { type: 'number' },
                    LocationName: { type: 'string' },
                }
            }
        }),
        change: function (e) {
            var obj = this.dataItem(this.select());
            if (Common.HasValue(obj)) {
                $scope.ContainerNew.LocationToName = obj.LocationName;
                $scope.ContainerNew.UnLoadingTime = obj.UnLoadTimeCO;
            }
        }
    }

    $scope.cbo_CO_LocationDepotOptions = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true,
        dataTextField: 'LocationName', dataValueField: 'CUSLocationID', minLength: 3,
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'CUSLocationID',
                fields: {
                    CUSLocationID: { type: 'number' },
                    LocationName: { type: 'string' },
                }
            }
        }),
        change: function (e) {
            var obj = this.dataItem(this.select());
            if (Common.HasValue(obj)) {
                $scope.ContainerNew.LocationDepotName = obj.LocationName;
            }
        }
    }

    $scope.cbo_CO_LocationDepotReturnOptions = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true,
        dataTextField: 'LocationName', dataValueField: 'CUSLocationID', minLength: 3,
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'CUSLocationID',
                fields: {
                    CUSLocationID: { type: 'number' },
                    LocationName: { type: 'string' },
                }
            }
        }),
        change: function (e) {
            var obj = this.dataItem(this.select());
            if (Common.HasValue(obj)) {
                $scope.ContainerNew.LocationDepotReturnName = obj.LocationName;
            }
        }
    }

    $scope.ORD_FCLIMEX_Container_AddNew = function ($event, grid) {
        $event.preventDefault();

        var data = $.extend([], true, grid.dataSource.data());
        if ($scope.NoContainer > 0) {
            var objNew = $.extend({}, true, $scope.ContainerNew);
            switch ($scope.Item.ViewID) {
                default:
                case 1:
                    var objTo = $scope.cbo_CO_LocationTo.dataItem($scope.cbo_CO_LocationTo.select());
                    if (Common.HasValue(objTo)) {
                        objNew.LocationToID = objTo.CUSLocationID;
                        objNew.LocationToName = objTo.LocationName;
                    }
                    else {
                        objNew.LocationToID = -1;
                        objNew.LocationToName = "";
                    }
                    objNew.LocationDepotID = null;
                    objNew.DateGetEmpty = null;
                    objNew.DateReturnEmpty = objNew.ETD.addDays($scope.DemurrageTime);
                    objNew.DateUnloading = objNew.ETA.addDays(0 - objNew.UnLoadingTime / 24);
                    var objD = $scope.cbo_CO_LocationDepotReturn.dataItem($scope.cbo_CO_LocationDepotReturn.select());
                    if (Common.HasValue(objD)) {
                        objNew.LocationDepotReturnID = objD.CUSLocationID;
                        objNew.LocationDepotReturnName = objD.LocationName;
                    } else {
                        objNew.LocationDepotReturnID = -1;
                        objNew.LocationDepotReturnName = "";
                    }
                    break;
                case 2:
                    var objFrom = $scope.cbo_CO_LocationFrom.dataItem($scope.cbo_CO_LocationFrom.select());
                    if (Common.HasValue(objFrom)) {
                        objNew.LocationFromID = objFrom.CUSLocationID;
                        objNew.LocationFromName = objFrom.LocationName;
                    }
                    else {
                        objNew.LocationFromID = -1;
                        objNew.LocationFromName = "";
                    }
                    objNew.LocationDepotReturnID = null;
                    objNew.DateReturnEmpty = null;
                    objNew.DateGetEmpty = objNew.ETA.addDays(0 - $scope.DetentionTime);
                    objNew.DateLoading = objNew.ETD;
                    var objD = $scope.cbo_CO_LocationDepot.dataItem($scope.cbo_CO_LocationDepot.select());
                    if (Common.HasValue(objD)) {
                        objNew.LocationDepotID = objD.CUSLocationID;
                        objNew.LocationDepotName = objD.LocationName;
                    } else {
                        objNew.LocationDepotID = -1;
                        objNew.LocationDepotName = "";
                    }
                    break;
            }
            for (var i = 0; i < $scope.NoContainer; i++) {
                objNew.ID = data.length + 1;
                data.splice(0, 0, objNew);
            }
            $timeout(function () {
                $scope.container_gridOptions.dataSource.data(data);
                $rootScope.Message({ Msg: "Đã thêm." });
            }, 1)
        } else {
            $scope.NoContainer = 0;
            $rootScope.Message({ Msg: "Nhập số lượng. 0<SL<100" });
        }
    }

    $scope.ORD_FCLIMEX_Container_Edit = function ($event, grid) {
        $event.preventDefault();

        $timeout(function () {
            $scope.IsContainerEdit = true;
            var tr = $event.target.closest('tr');
            $scope.ContainerEdit = grid.dataItem(tr);
            _ORDOrder_FCLIMEX.Data.ItemContainerBackUp = $.extend(true, {}, $scope.ContainerEdit);
            grid.editRow(tr);
            var td = $(tr).find('input');
            td[0].focus();
        }, 1)
    }

    $scope.ORD_FCLIMEX_Container_Save = function ($event, grid) {
        $event.preventDefault();

        $rootScope.IsLoading = true;
        $scope.IsContainerEdit = false;
        var i = 0;
        var data = grid.dataSource.data();
        while (i < data.length) {
            var obj = data[i];
            if (obj.ID == $scope.ContainerEdit.ID) {
                obj = $.extend(true, {}, $scope.ContainerEdit);
            }
            i++;
        }
        grid.dataSource.sync();
        $rootScope.IsLoading = false;
    }

    $scope.ORD_FCLIMEX_Container_Cancel = function ($event, grid) {
        $event.preventDefault();

        $rootScope.IsLoading = true;
        $scope.IsContainerEdit = false;
        var i = 0;
        var data = grid.dataSource.data();
        while (i < data.length) {
            var obj = data[i];
            if (obj.ID == _ORDOrder_FCLIMEX.Data.ItemContainerBackUp.ID) {
                data.splice(i, 1);
                data.splice(i, 0, $.extend(true, {}, _ORDOrder_FCLIMEX.Data.ItemContainerBackUp))
            }
            i++;
        }
        $rootScope.IsLoading = false;
    }

    $scope.ORD_FCLIMEX_Container_Delete = function ($event, grid) {
        $event.preventDefault();

        var obj = grid.dataItem($event.target.closest('tr'));
        if (Common.HasValue(obj)) {
            grid.dataSource.remove(obj);
            $rootScope.Message({ Msg: 'Đã xóa', NotifyType: Common.Message.NotifyType.INFO });
        }
    }

    $scope.cbo_coGrid_PackingOptions = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true,
        dataTextField: 'PackingName', dataValueField: 'ID', minLength: 3,
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    PackingName: { type: 'string' },
                }
            }
        }),
        change: function (e) {
            var obj = this.dataItem(this.select());
            if (Common.HasValue(obj)) {
                $scope.ContainerEdit.PackingName = obj.PackingName;
            }
        }
    }

    $scope.cbo_coGrid_LocationFromOptions = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true,
        dataTextField: 'LocationName', dataValueField: 'CUSLocationID', minLength: 3,
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'CUSLocationID',
                fields: {
                    CUSLocationID: { type: 'number' },
                    LocationName: { type: 'string' },
                }
            }
        }),
        change: function (e) {
            var obj = this.dataItem(this.select());
            if (Common.HasValue(obj)) {
                $scope.ContainerEdit.LocationFromName = obj.LocationName;
                if ($scope.ContainerEdit.ETD != null) {
                    //$scope.ContainerEdit.DateLoading = $scope.ContainerEdit.ETD.addDays(obj.LoadTimeCO / 24);
                }
            }
        }
    }

    $scope.cbo_coGrid_LocationToOptions = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true,
        dataTextField: 'LocationName', dataValueField: 'CUSLocationID', minLength: 3,
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'CUSLocationID',
                fields: {
                    CUSLocationID: { type: 'number' },
                    LocationName: { type: 'string' },
                }
            }
        }),
        change: function (e) {
            var obj = this.dataItem(this.select());
            if (Common.HasValue(obj)) {
                $scope.ContainerEdit.LocationToName = obj.LocationName;
                if ($scope.ContainerEdit.ETA != null) {
                    $scope.ContainerEdit.DateUnloading = $scope.ContainerEdit.ETA.addDays(0 - obj.UnLoadTimeCO / 24);
                }
            }
        }
    }

    $scope.cbo_coGrid_LocationDepotOptions = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true,
        dataTextField: 'LocationName', dataValueField: 'CUSLocationID', minLength: 3,
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'CUSLocationID',
                fields: {
                    CUSLocationID: { type: 'number' },
                    LocationName: { type: 'string' },
                }
            }
        }),
        change: function (e) {
            var obj = this.dataItem(this.select());
            if (Common.HasValue(obj)) {
                $scope.ContainerEdit.LocationDepotName = obj.LocationName;
            }
        }
    }

    $scope.cbo_coGrid_LocationDepotReturnOptions = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true,
        dataTextField: 'LocationName', dataValueField: 'CUSLocationID', minLength: 3,
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'CUSLocationID',
                fields: {
                    CUSLocationID: { type: 'number' },
                    LocationName: { type: 'string' },
                }
            }
        }),
        change: function (e) {
            var obj = this.dataItem(this.select());
            if (Common.HasValue(obj)) {
                $scope.ContainerEdit.LocationDepotReturnName = obj.LocationName;
            }
        }
    }

    $scope.cbo_coGrid_VesselOptions = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true, dataTextField: 'Code', dataValueField: 'ID',
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    Code: { type: 'string' },
                }
            }
        }),
        change: function (e) {
            var obj = this.dataItem(this.select());
            if (Common.HasValue(obj) && obj != '') {
                $scope.ContainerEdit.VesselCode = obj.Code;
            }
            else {
                $scope.ContainerEdit.VesselCode = obj.Code;
            }
        }
    }

    $scope.coNumLoadingTime_Options = { format: 'n2', spinners: false, culture: 'en-US', min: 0, step: 1, decimals: 2 }

    $scope.coNumUnLoadingTime_Options = { format: 'n2', spinners: false, culture: 'en-US', min: 0, step: 1, decimals: 2 }

    //Tab sản phẩm
    $scope.product_gridOptions = {
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    CBM: { type: 'number', defaultValue: 0 },
                    Quantity: { type: 'number', defaultValue: 0 },
                    Weight: { type: 'number', defaultValue: 0 },
                    WeightKG: { type: 'number', defaultValue: 0 },
                    GroupOfProductID: { type: 'number' },
                    ProductID: { type: 'number' },
                    GroupOfProductName: { type: 'string' },
                    ProductName: { type: 'string' },
                    PartnerName: { type: 'string' },
                    SOCode: { type: 'string' },
                    DNCode: { type: 'string' },
                    Command: { editable: false }
                }
            },
            pageSize: 0
        }),
        height: '99%', groupable: false, pageable: false, sortable: false, columnMenu: false, resizable: true,
        selectable: false, filterable: false, reorderable: true,
        toolbar: kendo.template($('#product_grid_toolbar').html()),
        editable: { mode: 'inline' },
        dataBound: function () {
            $scope.IsProductEdit = false;
        },
        columns: [
            {
                field: 'Command', title: ' ', width: '85px',
                template: '<a ng-show="!IsProductEdit" href="/" ng-click="ORD_FCLIMEX_Product_Edit($event,product_grid)" class="k-button"><i class="fa fa-pencil"></i></a>' +
                    '<a ng-show="!IsProductEdit" href="/" ng-click="ORD_FCLIMEX_Product_Delete($event,product_grid)" class="k-button"><i class="fa fa-trash"></i></a>' +
                    '<a ng-show="IsProductEdit && ProductEdit.ID==#=ID#?true:false" href="/" ng-click="ORD_FCLIMEX_Product_Save($event,product_grid)" class="k-button"><i class="fa fa-check"></i></a>' +
                    '<a ng-show="IsProductEdit && ProductEdit.ID==#=ID#?true:false" href="/" ng-click="ORD_FCLIMEX_Product_Cancel($event,product_grid)" class="k-button"><i class="fa fa-ban"></i></a>',
                filterable: false, sortable: false, sortorder: 0, configurable: false, isfunctionalHidden: false
            },
            {
                field: 'ContainerID', width: 150, title: '{{RS.ORDContainer.ContainerNo}}', sortable: false, template: '#=ContainerNo#',
                editor: '<input class="cus-combobox" focus-k-combobox kendo-combobox k-options="cbbContainerOptions" data-bind="value:ContainerID" k-ng-model="ProductEdit.ContainerID"/>',
                sortorder: 2, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'GroupOfProductID', width: 150, title: '{{RS.ORDGroupProduct.GroupOfProductID}}', sortable: false, template: '#=GroupOfProductName#',
                editor: '<input class="cus-combobox" focus-k-combobox kendo-combobox k-options="cbbGroupOfProductOptions" data-bind="value:GroupOfProductID" k-ng-model="ProductEdit.GroupOfProductID"/>',
                sortorder: 2, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'ProductID', width: 150, title: '{{RS.ORDProduct.ProductID}}', sortable: false, template: '#=ProductName#',
                editor: '<input class="cus-combobox" focus-k-combobox kendo-combobox k-options="cbbProductOptions" data-bind="value:ProductID" k-ng-model="ProductEdit.ProductID"/>',
                sortorder: 3, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'Quantity', width: 100, title: '{{RS.ORDGroupProduct.Quantity}}',
                editor: '<input ng-model="ProductEdit.Quantity"class="cus-number" kendo-numeric-text-box k-options="Number" style="width: 100%;" ></input>',
                sortorder: 9, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'CBM', width: 100, title: '{{RS.ORDGroupProduct.CBM}}',
                editor: '<input ng-model="ProductEdit.CBM" ng-disabled="ProductEdit.CBMDisabled" class="cus-number" kendo-numeric-text-box k-options="Number" style="width: 100%;" ></input>',
                sortorder: 10, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'Weight', width: 100, title: '{{RS.ORDGroupProduct.Ton}}',
                editor: '<input ng-model="ProductEdit.Weight" ng-disabled="ProductEdit.WeightDisabled" class="cus-number" kendo-numeric-text-box k-options="NumberTon" style="width: 100%;" ></input>',
                sortorder: 11, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'WeightKG', width: 100, title: 'Kg',
                editor: '<input ng-model="ProductEdit.WeightKG" ng-disabled="ProductEdit.WeightDisabled" class="cus-number" kendo-numeric-text-box k-options="NumberKG" style="width: 100%;" ></input>',
                sortorder: 12, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'TempMin', width: 120, title: '{{RS.ORDGroupProduct.TempMin}}',
                editor: '<input ng-model="ProductEdit.TempMin" class="cus-number" kendo-numeric-text-box k-options="Number" style="width: 100%;" ></input>',
                sortorder: 13, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'TempMax', width: 120, title: '{{RS.ORDGroupProduct.TempMax}}',
                editor: '<input ng-model="ProductEdit.TempMax" class="cus-number" kendo-numeric-text-box k-options="Number" style="width: 100%;" ></input>',
                sortorder: 15, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'SOCode', width: 100, title: '{{RS.ORDGroupProduct.SOCode}}',
                editor: '<input ng-model="ProductEdit.SOCode" class="k-textbox" type="text" style="width: 100%;" ></input>',
                sortorder: 14, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'DNCode', width: 100, title: '{{RS.ORDGroupProduct.DNCode}}',
                editor: '<input ng-model="ProductEdit.DNCode" class="k-textbox" type="text" style="width: 100%;" ></input>',
                sortorder: 15, configurable: true, isfunctionalHidden: false
            },
            {
                title: ' ', filterable: false, sortable: false, sortorder: 100, configurable: true, isfunctionalHidden: false
            }
        ]
    }

    $scope.IsProductEdit = false;

    $scope.ORD_FCLIMEX_Product_AddNew = function ($event, grid) {
        $event.preventDefault();

        var data = $scope.product_gridOptions.dataSource.data();
        _ORDOrder_FCLIMEX.Data.ItemProductNew.ID = data.length > 0 ? -(data.length + 1) : -1;
        data.splice(0, 0, $.extend(true, {}, _ORDOrder_FCLIMEX.Data.ItemProductNew));
        $scope.product_gridOptions.dataSource.data(data);

        $timeout(function () {
            $scope.IsProductEdit = true;
            var items = grid.items();
            $scope.ProductEdit = grid.dataItem(items[0]);
            _ORDOrder_FCLIMEX.Data.ItemProductBackUp = $.extend(true, {}, $scope.ProductEdit);
            grid.editRow(items[0]);
            var td = $(items[0]).find('input');
            td[0].focus();
            $timeout(function () {
                $scope.LoadProduct();
            }, 1)
        }, 1);
    }

    $scope.ORD_FCLIMEX_Product_Edit = function ($event, grid) {
        $event.preventDefault();

        $scope.IsProductEdit = true;
        var tr = $event.target.closest('tr');
        $scope.ProductEdit = grid.dataItem(tr);
        _ORDOrder_FCLIMEX.Data.ItemProductBackUp = $.extend(true, {}, $scope.ProductEdit);
        grid.editRow(tr);
        var td = $(tr).find('input');
        td[0].focus();
        $timeout(function () {
            $scope.LoadProduct();
        }, 1)
    }

    $scope.ORD_FCLIMEX_Product_Save = function ($event, grid) {
        $event.preventDefault();

        $rootScope.IsLoading = true;
        $scope.IsProductEdit = false;
        var i = 0;
        var data = grid.dataSource.data();
        while (i < data.length) {
            var obj = data[i];
            if (obj.ID == $scope.ProductEdit.ID) {
                obj = $.extend(true, {}, $scope.ProductEdit);
            }
            i++;
        }
        grid.dataSource.sync();
        $rootScope.IsLoading = false;

    }

    $scope.ORD_FCLIMEX_Product_Cancel = function ($event, grid) {
        $event.preventDefault();

        $rootScope.IsLoading = true;
        $scope.IsProductEdit = false;
        var i = 0;
        var data = grid.dataSource.data();
        while (i < data.length) {
            var obj = data[i];
            if (obj.ID == _ORDOrder_FCLIMEX.Data.ItemProductBackUp.ID) {
                data.splice(i, 1);
                data.splice(i, 0, $.extend(true, {}, _ORDOrder_FCLIMEX.Data.ItemProductBackUp))
            }
            i++;
        }
        $rootScope.IsLoading = false;
    }

    $scope.ORD_FCLIMEX_Product_Delete = function ($event, grid) {
        $event.preventDefault();

        var obj = grid.dataItem($event.target.closest('tr'));
        if (Common.HasValue(obj)) {
            grid.dataSource.remove(obj);
            $rootScope.Message({ Msg: 'Đã xóa', NotifyType: Common.Message.NotifyType.INFO });
        }
    }

    $scope.cbbGroupOfProductOptions = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true, minLength: 2,
        dataTextField: 'GroupName', dataValueField: 'ID',
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    GroupName: { type: 'string' }
                }
            }
        }),
        change: function (e) {
            var obj = this.dataItem(this.select());
            if (Common.HasValue(obj)) {
                $scope.ProductEdit.GroupOfProductName = obj.GroupName;
            }
            $scope.ProductEdit.ProductID = "";
            $scope.ProductEdit.ProductName = "";

            $timeout(function () {
                $scope.LoadProduct();
            }, 1)
        }
    };

    $scope.cbbProductOptions = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true, minLength: 2,
        dataTextField: 'ProductName', dataValueField: 'ID',
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    ProductName: { type: 'string' },
                    PackingID: { type: 'number' },
                    Weight: { type: 'number' },
                    CBM: { type: 'number' },
                    IsKg: { type: 'boolean' }
                }
            }
        }),
        change: function (e) {
            try {
                $scope.ProductEdit.ProductName = _ORDOrder_FCLIMEX.Data.ListProduct[$scope.ProductEdit.GroupOfProductID][0].ProductName;
            } catch (e) {
            }
        }
    };
    $scope.cbbContainerOptions = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true, minLength: 2,
        dataTextField: 'Text', dataValueField: 'Value',
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'Value',
                fields: {
                    Value: { type: 'number' },
                    Text: { type: 'string' },
                }
            }
        }),
        change: function (e) {
            var obj = this.dataItem(this.select())
            if (Common.HasValue(obj))
            {
                $scope.ProductEdit.ContainerNo = obj.Text;
            }
            else {
                $scope.ProductEdit.ContainerNo = "";
            }
        }
    };

    $scope.LoadProduct = function () {
        Common.Log("LoadProduct");
        var gopID = $scope.ProductEdit.GroupOfProductID;
        var productID = $scope.ProductEdit.ProductID;
        var productName = $scope.ProductEdit.ProductName;
        var data = _ORDOrder_FCLIMEX.Data.ListProduct[gopID];
        if (!Common.HasValue(data) && data != '')
            data = [];
        $scope.cbbProductOptions.dataSource.data(data);
        if (!(productID > 0) && data.length > 0) {
            productID = data[0].ID;
            productName = data[0].ProductName;
        }
        $timeout(function () {
            $scope.ProductEdit.ProductID = productID;
            $scope.ProductEdit.ProductName = productName;
        }, 1)
    };

    $scope.Number = { format: 'n6', spinners: false, culture: 'en-US', min: 0, step: 0.00001, decimals: 6 }
    $scope.NumberTon = { format: 'n6', spinners: false, culture: 'en-US', min: 0, step: 0.000001, decimals: 6, change: function (o) { var val = this.value(); $scope.ProductEdit.WeightKG = val * 1000; } }
    $scope.NumberKG = { format: 'n3', spinners: false, culture: 'en-US', min: 0, step: 0.001, decimals: 3, change: function (o) { var val = this.value(); $scope.ProductEdit.Weight = val / 1000; } }


    //Tab dịch vụ
    $scope.service_gridOptions = {
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    IsChoose: { type: 'bool', editable: false },
                    ServiceName: { type: 'string', editable: false },
                    PackingName: { type: 'string', editable: false },
                    TypeOfPackingName: { type: 'string', editable: false },
                    Price: { type: 'string', editable: true, defaultValue: '0' }
                }
            },
            pageSize: 0
        }),
        editable: { mode: 'incell' }, height: '100%', pageable: false, sortable: false, columnMenu: false, filterable: false, resizable: true,
        columns: [
            {
                field: 'Choose', title: ' ', width: '50px',
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,service_grid,service_gridChoose_Change)" />',
                headerAttributes: { style: 'text-align: center;' }, attributes: { style: 'text-align: center;' },
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,service_grid,service_gridChoose_Change)" />',
                filterable: false, sortable: false
            },
            { field: 'ServiceName', title: '{{RS.ORDService.ServiceID}}' },
            { field: 'PackingName', width: 200, title: '{{RS.ORDService.PackingID}}' },
            { field: 'TypeOfPackingName', width: 300, title: '{{RS.CATService.ServiceTypeID}}' },
            { field: 'Price', width: 300, title: '{{RS.ORDService.Price}}' }
        ],
        dataBound: function () {
            var grid = this;
            Common.Data.Each(grid.items(), function (tr) {
                var obj = grid.dataItem(tr);
                if (obj.IsChoose) {
                    $(tr).addClass('IsChoose');
                }
            })
            if (Common.HasValue($scope.Item)) {
                if ($scope.Item.IsMain) {
                    grid.hideColumn('Price');
                    grid.hideColumn('PackingName');
                } else {
                    grid.showColumn('Price');
                    grid.showColumn('PackingName');
                }
            }
        }
    }

    //Common.Services.Call($http, {
    //    url: Common.Services.url.ORD,
    //    method: _ORDOrder_FCLIMEX.URL.COService,
    //    data: { ContractID: _ORDOrder_FCLIMEX.Param.ContractID },
    //    success: function (res) {
    //        _ORDOrder_FCLIMEX.Data.ListService = res.Data;
    //        $.each(_ORDOrder_FCLIMEX.Data.ListService, function (i, v) {
    //            v.IsChoose = false;
    //            v.ServiceID = v.ID;
    //        })
    //        $timeout(function () {
    //            $scope.service_gridOptions.dataSource.data(_ORDOrder_FCLIMEX.Data.ListService);
    //        }, 1);
    //    }
    //})

    //Tab đơn giá
    $scope.price_gridOptions = {
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    PackingName: { type: 'string', editable: false },
                    LocationFromName: { type: 'string', editable: false },
                    LocationToName: { type: 'string', editable: false },
                    Price: { type: 'number', editable: true, validation: { required: true, min: 0 } },
                    Quantity: { type: 'number', editable: false },
                    TotalPrice: { type: 'number', editable: false },
                    HasOffer: { type: 'bool', editable: false }
                }
            },
            pageSize: 0
        }),
        editable: { mode: 'incell' }, height: '100%', pageable: false, sortable: false, columnMenu: false, filterable: false, resizable: true,
        columns: [
            { field: 'LocationFromName', width: 300, title: '{{RS.ORDContainerPrice.LocationFromID}}' },
            { field: 'LocationToName', width: 300, title: '{{RS.ORDContainerPrice.LocationToID}}' },
            { field: 'PackingName', width: 200, title: '{{RS.ORDContainerPrice.PackingID}}' },
            {
                field: 'Price', width: 200, title: '{{RS.ORDContainerPrice.Price}}',
            },
            { field: 'Quantity', width: 200, title: '{{RS.ORDOrder_FCLIMEX.Quantity}}' },
            { field: 'TotalPrice', title: '{{RS.ORDOrder_FCLIMEX.TotalPrice}}' }
        ],
        save: function (e) {
            e.preventDefault();
            e.model.Price = Math.round(StringToNumber(e.values.Price));
            e.model.TotalPrice = e.model.Price * e.model.Quantity;
            this.dataSource.sync();
        }
    }

    $scope.LoadPriceContainer = function () {
        var dataPrice = $scope.price_gridOptions.dataSource.data();
        var objPrice = {};
        $.each(dataPrice, function (i, v) {
            var f = "_" + v.LocationFromID + "_" + v.LocationToID + "_" + v.PackingID;
            objPrice[f] = v.Price;
        });
        var dataCO = $scope.container_gridOptions.dataSource.data();
        var lst = [];
        var data = [];
        $.each(dataCO, function (i, v) {
            var f = "_" + v.LocationFromID + "_" + v.LocationToID + "_" + v.PackingID;
            if (Common.HasValue(lst[f])) {
                lst[f].Quantity++;
            } else {
                lst[f] = {
                    PackingID: v.PackingID,
                    PackingName: v.PackingName,
                    LocationFromID: v.LocationFromID,
                    LocationFromName: v.LocationFromName,
                    LocationToID: v.LocationToID,
                    LocationToName: v.LocationToName,
                    Quantity: 1,
                    Price: 0,
                    TotalPrice: 0,
                    HasOffer: false
                }
                data.push(lst[f]);
            }
        })

        $.each(data, function (i, v) {
            v.ID = i || -1;
            var f = "_" + v.LocationFromID + "_" + v.LocationToID + "_" + v.PackingID;
            if (Common.HasValue(objPrice[f]) && objPrice[f] >= 0) {
                v.HasOffer = true;
                v.Price = objPrice[f];
                v.TotalPrice = v.Price * v.Quantity;
            } else if (!$scope.Item.IsMain && $scope.Item.ContractID > 0) {
                Common.Services.Call($http, {
                    url: Common.Services.url.ORD,
                    method: _ORDOrder_FCLIMEX.URL.PriceContainer,
                    data: {
                        ContractID: $scope.Item.ContractID,
                        FromID: v.LocationFromID,
                        ToID: v.LocationToID,
                        ConID: v.PackingID,
                        TypeID: $scope.Item.TypeOfOrderID
                    },
                    success: function (res) {
                        v.HasOffer = true;
                        v.Price = res;
                        v.TotalPrice = v.Price * v.Quantity;
                    }
                });
            }
        })

        $timeout(function () {
            $scope.price_gridOptions.dataSource.data(data);
        });
    }

    //Map

    $scope.CreateMap = function () {
        Common.Log("CreateMap");
        openMapV2.hasMap = true;
        Common.Log(openMapV2.hasMap);

        openMapV2.Init({
            Element: 'fclimex_map',
            Tooltip_Show: true,
            Tooltip_Element: 'fclimex_tooltip',
            DefinedLayer: [{
                Name: 'VectorMarkerSeaport',
                zIndex: 100
            }, {
                Name: 'VectorMarkerStock',
                zIndex: 100
            }, {
                Name: 'VectorMarkerDepot',
                zIndex: 100
            }, {
                Name: 'VectorRoute',
                zIndex: 90
            }]
        });

        openMapV2.ClearVector("VectorMarkerDepot");
        openMapV2.ClearVector("VectorMarkerStock");
        openMapV2.ClearVector("VectorMarkerSeaport");
        Common.Data.Each(_ORDOrder_FCLIMEX.Data.ListStock, function (o) {
            var icon = openMapV2.NewStyle.Icon(Common.String.Format(openMapV2.NewImage.NewStock(openMapV2.NewImage.Color.Green)), 1);
            if (Common.HasValue(o.Lat) && Common.HasValue(o.Lng)) {
                openMapV2.NewMarker(o.Lat, o.Lng, o.CUSLocationID, o.LocationName, icon, o, "VectorMarkerStock");
            }
        })
        Common.Data.Each(_ORDOrder_FCLIMEX.Data.ListSeaPort, function (o) {
            var icon = openMapV2.NewStyle.Icon(Common.String.Format(openMapV2.NewImage.NewSeaport(openMapV2.NewImage.Color.Green)), 1);
            if (Common.HasValue(o.Lat) && Common.HasValue(o.Lng)) {
                openMapV2.NewMarker(o.Lat, o.Lng, o.CUSLocationID, o.LocationName, icon, o, "VectorMarkerSeaport");
            }
        })
        Common.Data.Each(_ORDOrder_FCLIMEX.Data.ListDepot, function (v) {
            Common.Data.Each(v, function (o) {
                var icon = openMapV2.NewStyle.Icon(Common.String.Format(openMapV2.NewImage.NewDepot(openMapV2.NewImage.Color.Green)), 1);
                if (Common.HasValue(o.Lat) && Common.HasValue(o.Lng)) {
                    openMapV2.NewMarker(o.Lat, o.Lng, o.CUSLocationID, o.LocationName, icon, o, "VectorMarkerDepot");
                }
            })
        })
        $timeout(function () {
            $scope.ReloadMap();
        }, 100)
    }

    $scope.ReloadMap = function () {
        Common.Log("ReloadMap");

        openMapV2.Close();
        openMapV2.ClearVector("VectorRoute");
        openMapV2.Visible("VectorMarkerDepot", false);
        openMapV2.Visible("VectorMarkerStock", false);
        openMapV2.Visible("VectorMarkerSeaport", false);
        var objX = openMapV2.GetFeature($scope.Item.LocationDepotID, "VectorMarkerDepot");
        switch ($scope.Item.ViewID) {
            default:
            case 1://IM
                var f1 = openMapV2.GetFeature($scope.Item.LocationFromID, "VectorMarkerSeaport");
                var f2 = openMapV2.GetFeature($scope.Item.LocationToID, "VectorMarkerStock");
                try {
                    openMapV2.Visible([f1, f2], true);
                } catch (e) { }
                if (Common.HasValue(f1) && Common.HasValue(f2)) {
                    openMapV2.NewRoute(f1, f2, "", "", openMapV2.NewStyle.Line(6, 'rgba(3, 169, 244, 0.6)'), null, "VectorRoute", null, function () {
                        openMapV2.FitBound("VectorRoute", 15);
                    });

                    if (Common.HasValue(objX)) {
                        openMapV2.Visible([objX], true);
                        openMapV2.NewRoute(f2, objX, "", "", openMapV2.NewStyle.Line(6, 'rgba(3, 169, 244, 0.6)'), null, "VectorRoute", null, function () {
                            openMapV2.FitBound("VectorRoute", 15);
                        });
                    }
                }
                break;
            case 2:
                var f1 = openMapV2.GetFeature($scope.Item.LocationFromID, "VectorMarkerStock");
                var f2 = openMapV2.GetFeature($scope.Item.LocationToID, "VectorMarkerSeaport");
                try {
                    openMapV2.Visible([f1, f2], true);
                } catch (e) { }
                if (Common.HasValue(f1) && Common.HasValue(f2)) {
                    openMapV2.NewRoute(f1, f2, "", "", openMapV2.NewStyle.Line(6, 'rgba(3, 169, 244, 0.6)'), null, "VectorRoute", null, function () {
                        openMapV2.FitBound("VectorRoute", 15);
                    });

                    if (Common.HasValue(objX)) {
                        openMapV2.Visible([objX], true);
                        openMapV2.NewRoute(objX, f1, "", "", openMapV2.NewStyle.Line(6, 'rgba(3, 169, 244, 0.6)'), null, "VectorRoute", null, function () {
                            openMapV2.FitBound("VectorRoute", 15);
                        });
                    }
                }
                break;
        }
    }

    $scope.fclimex_cboTypeOfOrderOptions = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true,
        dataTextField: 'ValueOfVar', dataValueField: 'ID', minLength: 3,
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    ValueOfVar: { type: 'string' },
                }
            }
        })
    }

    $scope.fclimex_dtpExternalDateOptions = {
        format: Common.Date.Format.DMYHM,
        parseFormats: Common.Date.ParseFormat
    }

    Common.ALL.Get($http, {
        url: Common.ALL.URL.SYSVarTypeOfOrder,
        success: function (res) {
            $scope.fclimex_cboTypeOfOrderOptions.dataSource.data(res);
        }
    })

    $scope.contract_gridOptions = {
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    EffectDate: { type: 'date' },
                    ExpiredDate: { type: 'date' }
                }
            },
            pageSize: 0
        }),
        height: '99%', groupable: false, pageable: false, sortable: false, columnMenu: false, resizable: true,
        selectable: true, filterable: false, reorderable: true,
        dataBound: function () {
            var grid = this;
            $.each(grid.items(), function (i, tr) {
                var obj = grid.dataItem(tr);
                if (Common.HasValue(obj) && Common.HasValue($scope.Item) && obj.ID == $scope.Item.ContractID) {
                    grid.select(tr);
                }
            })
        },
        columns: [
            { field: 'DisplayName', width: 150, title: '{{RS.CATContract.DisplayName}}' },
            { field: 'ServiceOfOrderName', width: 100, title: '{{RS.CATContract.ServiceOfOrderID}}' },
            { field: 'TransportModeName', width: 100, title: '{{RS.CATContract.TransportModeID}}' },
            { field: 'TypeOfContractName', width: 100, title: '{{RS.CATContract.TypeOfContractID}}' },
            { field: 'EffectDate', width: 110, title: '{{RS.CATContract.EffectDate}}', template: "#=Common.Date.FromJsonDMY(EffectDate)#" },
            { field: 'ExpiredDate', width: 110, title: '{{RS.CATContract.ExpiredDate}}', template: "#=Common.Date.FromJsonDMY(ExpiredDate)#" },
            { field: 'Content', width: 500, title: '{{RS.CATContract.Content}}' },
            { title: ' ', filterable: false, sortable: false }
        ]
    }

    Common.Services.Call($http, {
        url: Common.Services.url.ORD,
        method: _ORDOrder_FCLIMEX.URL.Contract,
        data: _ORDOrder_FCLIMEX.Param,
        success: function (res) {
            $scope.contract_gridOptions.dataSource.data(res.Data);
        }
    });

    $scope.ORD_FCLIMEX_ChangeContract = function ($event, win) {
        $event.preventDefault();

        win.center().open();
        $scope.contract_grid.refresh();
    }

    $scope.Contract_Update_Click = function ($event, win, grid) {
        $event.preventDefault();

        var obj = grid.dataItem(grid.select());
        if (Common.HasValue(obj)) {
            $rootScope.Message({
                Type: Common.Message.Type.Confirm,
                Msg: 'Xác nhận hợp đồng? Sau khi lưu sẽ tải lại trang.',
                pars: obj,
                Ok: function (pars) {
                    $scope.Item.ContractID = pars.ID;
                    _ORDOrder_FCLIMEX.Param.ContractID = pars.ID;
                    if ($scope.Item.ID > 0) {
                        Common.Services.Call($http, {
                            url: Common.Services.url.ORD,
                            method: _ORDOrder_FCLIMEX.URL.Contract_Change,
                            data: _ORDOrder_FCLIMEX.Param,
                            success: function (res) {
                                Common.Services.Error(res, function (res) {
                                    $state.go("main.ORDOrder.FCLIMEX", _ORDOrder_FCLIMEX.Param);
                                })
                            }
                        });
                    } else {
                        $state.go("main.ORDOrder.FCLIMEX", _ORDOrder_FCLIMEX.Param);
                    }
                    win.close();
                }
            });
        }
    }

    $scope.Contract_Update_None_Click = function ($event, win) {
        $event.preventDefault();
        $rootScope.Message({
            Type: Common.Message.Type.Confirm,
            Msg: 'Xác nhận hợp đồng? Sau khi lưu sẽ tải lại trang.',
            Ok: function () {
                $scope.Item.ContractID = null;
                _ORDOrder_FCLIMEX.Param.ContractID = -1;
                if ($scope.Item.ID > 0) {
                    Common.Services.Call($http, {
                        url: Common.Services.url.ORD,
                        method: _ORDOrder_FCLIMEX.URL.Contract_Change,
                        data: _ORDOrder_FCLIMEX.Param,
                        success: function (res) {
                            Common.Services.Error(res, function (res) {
                                $state.go("main.ORDOrder.FCLIMEX", _ORDOrder_FCLIMEX.Param);
                            })
                        }
                    });
                } else {
                    $state.go("main.ORDOrder.FCLIMEX", _ORDOrder_FCLIMEX.Param);
                }
                win.close();
            }
        });
    }

    $scope.Contract_Close_Click = function ($event, win) {
        $event.preventDefault();

        win.close();
    }

    Common.ALL.Get($http, {
        url: Common.ALL.URL.CATPackingCO,
        success: function (res) {
            _ORDOrder_FCLIMEX.Data.ListContainer = res;
            $scope.cbo_CO_PackingOptions.dataSource.data(_ORDOrder_FCLIMEX.Data.ListContainer);
            $scope.cbo_coGrid_PackingOptions.dataSource.data(_ORDOrder_FCLIMEX.Data.ListContainer);
            var obj = _ORDOrder_FCLIMEX.Data.ListContainer[0];

            if (Common.HasValue(obj)) {
                $scope.ContainerNew.PackingID = obj.ID;
                $scope.ContainerNew.PackingName = obj.PackingName;
            }
        }
    });
    //#endregion

    //#region Action
    $scope.CheckItem = function () {
        if ($scope.Item.Code == null || $scope.Item.Code.length >= 50 || !$scope.Item.Code.isValidCode()) {
            $rootScope.Message({
                Type: Common.Message.Type.Alert,
                Msg: 'Mã không hợp lệ.'
            })
            return false;
        }
        if ($scope.container_gridOptions.dataSource.data().length == 0) {
            $rootScope.Message({
                Type: Common.Message.Type.Alert,
                Msg: 'Không có thông tin chi tiết đơn hàng.'
            })
            return false;
        }
        if ($scope.Item.LocationDepotID.length > 0 && $scope.fclimex_cboLocationDepot.select() == -1) {
            $rootScope.Message({
                Type: Common.Message.Type.Alert,
                Msg: 'Bãi chứa con không hợp lệ'
            })
            return false;
        }

        if ($scope.fclimex_cboLocationFrom.text().length == 0 || $scope.fclimex_cboLocationTo.text().length == 0 || $scope.fclimex_cboShippingCo.text().length == 0) {
            $rootScope.Message({
                Type: Common.Message.Type.Alert,
                Msg: 'Thiếu thông tin bắt buộc.'
            })
            return false;
        }

        if (!$scope.IsValidDate($scope.Item.ETD, "Ngày ETD không đúng.")) {
            return false;
        }
        if (!$scope.IsValidDate($scope.Item.ETA, "Ngày ETA không đúng.")) {
            return false;
        }
        if (!$scope.IsValidDate($scope.Item.RequestDate, "Ngày gởi yêu cẩu không đúng.")) {
            return false;
        }
        if (!$scope.IsValidDate($scope.Item.ETARequest, "Ngày yêu cầu giao hàng không đúng.")) {
            return false;
        }
        return true;
    }

    $scope.IsValidDate = function (item, msg) {
        if (item != null && item != "" && !(angular.isDate(item) || angular.isDate(Common.Date.FromJson(item)))) {
            $rootScope.Message({
                Type: Common.Message.Type.Alert, Msg: msg
            })
            return false;
        }
        return true;
    }

    $scope.isDateOrNull = function (item) {
        if (item == null || item == "" || !(angular.isDate(item) || angular.isDate(Common.Date.FromJson(item)))) {
            return "";
        }
        return item;
    }

    $scope.ORD_FCLIMEX_Update = function ($event) {
        $event.preventDefault();
        if ($scope.CheckItem()) {
            $rootScope.Message({
                Type: Common.Message.Type.Confirm,
                Msg: 'Bạn muốn lưu đơn hàng?',
                Ok: function () {
                    $rootScope.IsLoading = true;
                    $scope.Item.ListContainer = $scope.container_gridOptions.dataSource.data();
                    $scope.Item.ListProduct = $scope.product_gridOptions.dataSource.data();
                    $scope.Item.ListContainerPrice = $scope.price_gridOptions.dataSource.data();
                    $scope.Item.ListService = $.grep($scope.service_gridOptions.dataSource.data(), function (o) { return o.IsChoose == true; });
                    Common.Services.Call($http, {
                        url: Common.Services.url.ORD,
                        method: _ORDOrder_FCLIMEX.URL.Update,
                        data: { item: $scope.Item },
                        success: function (res) {
                            $rootScope.IsLoading = false;
                            Common.Services.Error(res, function (res) {
                                $rootScope.Message({ Msg: "Đã cập nhật!" });
                                if (_ORDOrder_FCLIMEX.Param.OrderID != res) {
                                    _ORDOrder_FCLIMEX.Param.OrderID = res;
                                    if ($scope.Auth.ActAddAndApproved) {
                                        $rootScope.Message({
                                            Msg: "Bạn muốn gủi điều phối đơn hàng?",
                                            Type: Common.Message.Type.Confirm,
                                            Ok: function () {
                                                $rootScope.IsLoading = true;
                                                Common.Services.Call($http, {
                                                    url: Common.Services.url.ORD,
                                                    method: _ORDOrder_FCLIMEX.URL.Container_OPS_Check,
                                                    data: { data: [_ORDOrder_FCLIMEX.Param.OrderID] },
                                                    success: function (res) {
                                                        Common.Services.Error(res, function (res) {
                                                            $rootScope.IsLoading = false;
                                                            if (res.length > 0) {
                                                                $rootScope.Message({ Msg: "Thiếu dữ liệu container! Không thể gửi!" });
                                                                $state.go("main.ORDOrder.FCLIMEX", {
                                                                    OrderID: _ORDOrder_FCLIMEX.Param.OrderID,
                                                                    CustomerID: _ORDOrder_FCLIMEX.Param.CustomerID,
                                                                    ServiceID: _ORDOrder_FCLIMEX.Param.ServiceID,
                                                                    TransportID: _ORDOrder_FCLIMEX.Param.TransportID,
                                                                    ContractID: _ORDOrder_FCLIMEX.Param.ContractID != null ? _ORDOrder_FCLIMEX.Param.ContractID : -1,
                                                                    TermID: _ORDOrder_FCLIMEX.Param.TermID != null ? _ORDOrder_FCLIMEX.Param.TermID : -1
                                                                });
                                                            } else {
                                                                $rootScope.IsLoading = true;
                                                                Common.Services.Call($http, {
                                                                    url: Common.Services.url.ORD,
                                                                    method: _ORDOrder_FCLIMEX.URL.OPS_Check,
                                                                    data: { data: [_ORDOrder_FCLIMEX.Param.OrderID] },
                                                                    success: function (res) {
                                                                        Common.Services.Error(res, function (res) {
                                                                            $rootScope.IsLoading = false;
                                                                            if (res.length > 0) {
                                                                                $rootScope.Message({ Msg: "Thiếu dữ liệu cung đường! Không thể gửi!" });
                                                                                $state.go("main.ORDOrder.FCLIMEX", {
                                                                                    OrderID: _ORDOrder_FCLIMEX.Param.OrderID,
                                                                                    CustomerID: _ORDOrder_FCLIMEX.Param.CustomerID,
                                                                                    ServiceID: _ORDOrder_FCLIMEX.Param.ServiceID,
                                                                                    TransportID: _ORDOrder_FCLIMEX.Param.TransportID,
                                                                                    ContractID: _ORDOrder_FCLIMEX.Param.ContractID != null ? _ORDOrder_FCLIMEX.Param.ContractID : -1,
                                                                                    TermID: _ORDOrder_FCLIMEX.Param.TermID != null ? _ORDOrder_FCLIMEX.Param.TermID : -1
                                                                                });
                                                                            } else {
                                                                                Common.Services.Call($http, {
                                                                                    url: Common.Services.url.ORD,
                                                                                    method: _ORDOrder_FCLIMEX.URL.ToOPS,
                                                                                    data: { lst: [_ORDOrder_FCLIMEX.Param.OrderID] },
                                                                                    success: function (res) {
                                                                                        Common.Services.Error(res, function (res) {
                                                                                            $rootScope.IsLoading = false;
                                                                                            $state.go('main.ORDOrder.Index');
                                                                                        })
                                                                                    }
                                                                                })
                                                                            }
                                                                        })
                                                                    }
                                                                })
                                                            }
                                                        })
                                                    }
                                                })
                                            },
                                            Close: function () {
                                                $state.go("main.ORDOrder.FCLIMEX", {
                                                    OrderID: res,
                                                    CustomerID: _ORDOrder_FCLIMEX.Param.CustomerID,
                                                    ServiceID: _ORDOrder_FCLIMEX.Param.ServiceID,
                                                    TransportID: _ORDOrder_FCLIMEX.Param.TransportID,
                                                    ContractID: _ORDOrder_FCLIMEX.Param.ContractID != null ? _ORDOrder_FCLIMEX.Param.ContractID : -1,
                                                    TermID: _ORDOrder_FCLIMEX.Param.TermID != null ? _ORDOrder_FCLIMEX.Param.TermID : -1
                                                });
                                            }
                                        })
                                    } else {
                                        $rootScope.Message({ Msg: "Thành công!" });
                                        $state.go("main.ORDOrder.FCLIMEX", {
                                            OrderID: res,
                                            CustomerID: _ORDOrder_FCLIMEX.Param.CustomerID,
                                            ServiceID: _ORDOrder_FCLIMEX.Param.ServiceID,
                                            TransportID: _ORDOrder_FCLIMEX.Param.TransportID,
                                            ContractID: _ORDOrder_FCLIMEX.Param.ContractID != null ? _ORDOrder_FCLIMEX.Param.ContractID : -1,
                                            TermID: _ORDOrder_FCLIMEX.Param.TermID != null ? _ORDOrder_FCLIMEX.Param.TermID : -1
                                        });
                                    }
                                }
                                $scope.Item.ETA = $scope.isDateOrNull($scope.Item.ETA);
                                $scope.Item.ETD = $scope.isDateOrNull($scope.Item.ETD);
                                $scope.Item.RequestDate = $scope.isDateOrNull($scope.Item.RequestDate);
                                $scope.Item.ETARequest = $scope.isDateOrNull($scope.Item.ETARequest);
                            })
                        }
                    });
                }
            });
        }
    }

    $scope.ORD_FCLIMEX_Delete = function ($event) {
        $event.preventDefault();
        $rootScope.Message({
            Type: Common.Message.Type.Confirm,
            Msg: 'Bạn muốn xóa đơn hàng?',
            Ok: function () {
                Common.Log("Delete");
                Common.Services.Call($http, {
                    url: Common.Services.url.ORD,
                    method: _ORDOrder_FCLIMEX.URL.Delete,
                    data: { id: _ORDOrder_FCLIMEX.Param.OrderID },
                    success: function (res) {
                        Common.Services.Error(res, function (res) {
                            $rootScope.Message({ Msg: "Đã cập nhật!" });
                            $state.go("main.ORDOrder.Index");
                        })
                    }
                });
            }
        });
    }

    $scope.ORD_FCLIMEX_Back = function ($event) {
        $event.preventDefault();
        Common.Log("Back");
        if (_ORDOrder_FCLIMEX.Param.OrderID > 0) {
            $state.go('main.ORDOrder.Index');
        } else {
            $state.go("main.ORDOrder.New");
        }
    }

    $scope.resetOrderCode = function ($event) {
        if ($event.target.checked == true) {
            $scope.Item.Code = "";
        }
    };
    //#endregion
}]);

//#region Convert Functions
function StringToNumber(string) {
    return parseFloat(string.toString().replace(',', '.'));
}


//#endregion