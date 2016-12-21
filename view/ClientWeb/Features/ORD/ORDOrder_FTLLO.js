/// <reference path="~/Scripts/jquery-1.9.1.intellisense.js" />
/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />
/// <reference path="~/Scripts/map.js" />

//#region URL

var _ORDOrder_FTLLO = {
    URL: {
        GetItem: 'ORDOrder_GetItem',
        Data: 'ORDOrder_TruckLocal_Data',
        PriceRoute: 'ORDOrder_PriceGroupVehicle',

        Delete: 'ORDOrder_Delete',
        Update: 'ORDOrder_FTLLO_Save',

        Contract: 'ORDOrder_Contract_List',
        Contract_Change: 'ORDOrder_Contract_Change',

        NewLocation: 'ORDOrder_NewLocation_Save',

        ToOPS: 'ORDOrder_ToOPS',
        ToOPS_Check: 'ORDOrder_ToOPSCheck',
        GetDate: 'ORDOrder_GetDate'
    },
    Data: {
        ItemNew: {
            ID: "", PartnerID: "", LocationFromID: "", GroupOfProductID: "", ProductID: "", CUSRoutingID: null, CUSRoutingCode: "",
            CBM: 0, Quantity: 0, Weight: 0, WeightKG: 0, LocationToID: "", SOCode: '', TempMin: null, TempMax: null,
            ETD: new Date(), ETA: new Date().addDays(1), ETARequest: new Date().addDays(1), IsReturn: null, TempMin: null, TempMax: null
        },
        ItemBackUp: null,
        ListStock: null,
        ListGroupProduct: null,
        ListProduct: null,
        ListDistributor: null,
        ListLocation: null,
        ListGroupVehicle: null,
        ListDistributorLocation: null,
        ListCountry:[]
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

angular.module('myapp').controller('ORDOrder_FTLLOCtrl', ['$rootScope', '$scope', '$http', '$location', '$state', '$timeout', 'openMapV2', '$compile', function ($rootScope, $scope, $http, $location, $state, $timeout, openMapV2, $compile) {
    if ($rootScope.IsPageComplete != true) return;
    Common.Log('ORDOrder_FTLLOCtrl');
    $rootScope.IsLoading = false;

    $rootScope.Loading.Show();
    $rootScope.Loading.Change("Khởi tạo...", 20);

    $scope.Auth = $rootScope.GetAuth();
    $rootScope.QuickAdd({
        Show: true,
        Call: function ($event) {
            $event.preventDefault();
            $state.go('main.ORDOrder.New');
            $scope.IsDetail = true;
        }
    });

    _ORDOrder_FTLLO.Param = $.extend(true, _ORDOrder_FTLLO.Param, $state.params);
    if (_ORDOrder_FTLLO.Param.OrderID == "" || !angular.isNumber(parseInt(_ORDOrder_FTLLO.Param.OrderID))
       || _ORDOrder_FTLLO.Param.CustomerID == "" || !angular.isNumber(parseInt(_ORDOrder_FTLLO.Param.CustomerID))
       || _ORDOrder_FTLLO.Param.ServiceID == "" || !angular.isNumber(parseInt(_ORDOrder_FTLLO.Param.ServiceID))
       || _ORDOrder_FTLLO.Param.TransportID == "" || !angular.isNumber(parseInt(_ORDOrder_FTLLO.Param.TransportID))
       || _ORDOrder_FTLLO.Param.ContractID == "" || !angular.isNumber(parseInt(_ORDOrder_FTLLO.Param.ContractID))
       || _ORDOrder_FTLLO.Param.TermID == "" || !angular.isNumber(parseInt(_ORDOrder_FTLLO.Param.TermID))) {
        $rootScope.Message({ Msg: 'Đường dẫn ko đúng! Quay về trang đơn hàng!' });
        $state.go("main.ORDOrder.Index");
    }
    $scope.Item = null;
    $scope.ProductEdit = null;
    $scope.HasCreatePartner = false;
    $scope.HasCreateLocation = false;
    //#region View

    $scope.ftllo_tabstripOptions = {
        animation: {
            open: {
                effects: "fadeIn"
            }
        },
        select: function (e) {
            var index = angular.element(e.item).data('tabindex');
            switch (index) {
                case 0:
                    //ReDrawMap
                    if (Common.HasValue($scope.ProductEdit)) {
                        $scope.ReloadMap();
                    }
                    break;

                case 1:
                    break;

                case 2:

                    break;
                default:
                    break;
            }
        },
        activate: function (e) {
            var index = angular.element(e.item).data('tabindex');
            switch (index) {
                case 0:
                    //ReDrawMap
                    if (Common.HasValue($scope.ProductEdit)) {
                        openMapV2.FitBound("VectorRoute", 15);
                    }
                    break;

                case 1:
                    break;

                case 2:

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
        method: _ORDOrder_FTLLO.URL.Data,
        data: { CustomerID: _ORDOrder_FTLLO.Param.CustomerID, TermID: _ORDOrder_FTLLO.Param.TermID },
        success: function (res) {
            $rootScope.Loading.Change("Lấy dữ liệu đơn hàng...", 50);

            $scope.hor_splitter.resize();

            if (Common.HasValue(res)) {
                $scope.HasCreatePartner = res.IsCusCreatePartner;
                $scope.HasCreateLocation = res.IsCusCreateLocation;

                _ORDOrder_FTLLO.Data.ListStock = [];
                _ORDOrder_FTLLO.Data.ListGroupProduct = [];
                _ORDOrder_FTLLO.Data.ListProduct = [];
                _ORDOrder_FTLLO.Data.ListDistributor = [];
                _ORDOrder_FTLLO.Data.ListLocation = [];
                _ORDOrder_FTLLO.Data.ListGroupVehicle = [];

                _ORDOrder_FTLLO.Data.ItemNew.LocationFromID = '';
                _ORDOrder_FTLLO.Data.ItemNew.LocationFromName = '';
                _ORDOrder_FTLLO.Data.ItemNew.GroupOfProductID = '';
                _ORDOrder_FTLLO.Data.ItemNew.GroupOfProductName = '';
                _ORDOrder_FTLLO.Data.ItemNew.ProductID = '';
                _ORDOrder_FTLLO.Data.ItemNew.ProductName = '';
                _ORDOrder_FTLLO.Data.ItemNew.PartnerID = '';
                _ORDOrder_FTLLO.Data.ItemNew.LocationToID = '';
                _ORDOrder_FTLLO.Data.ItemNew.LocationToName = '';
                _ORDOrder_FTLLO.Data.ItemNew.LocationToAddress = '';

                $scope.cbbRoutingOptions.dataSource.data(res.ListContractRouting);
                _ORDOrder_FTLLO.Data.ListStock = res.ListStock;
                $scope.cbbLocationFromOptions.dataSource.data(_ORDOrder_FTLLO.Data.ListStock);
                $.each(_ORDOrder_FTLLO.Data.ListStock, function (i, v) {
                    if (_ORDOrder_FTLLO.Data.ItemNew.LocationFromID < 1) {
                        _ORDOrder_FTLLO.Data.ItemNew.LocationFromID = v.CUSLocationID;
                        _ORDOrder_FTLLO.Data.ItemNew.LocationFromName = v.LocationName;
                    }

                    _ORDOrder_FTLLO.Data.ListGroupProduct[v.CUSLocationID] = [];
                });

                $.each(res.ListGroupProduct, function (i, v) {
                    if (Common.HasValue(_ORDOrder_FTLLO.Data.ListGroupProduct[v.CUSStockID])) {
                        _ORDOrder_FTLLO.Data.ListGroupProduct[v.CUSStockID].push(v);
                        if (v.Products.length == 0)
                            v.Products.push({ ID: "", Code: '', ProductName: '' });
                        _ORDOrder_FTLLO.Data.ListProduct[v.CUSStockID + "_" + v.GroupID] = v.Products;

                        if (_ORDOrder_FTLLO.Data.ItemNew.GroupOfProductID < 1 && v.Products.length > 0 && v.CUSStockID == _ORDOrder_FTLLO.Data.ItemNew.LocationFromID) {
                            _ORDOrder_FTLLO.Data.ItemNew.GroupOfProductID = v.GroupID;
                            _ORDOrder_FTLLO.Data.ItemNew.GroupOfProductName = v.GroupName;
                            _ORDOrder_FTLLO.Data.ItemNew.ProductID = v.Products[0].ID;
                            _ORDOrder_FTLLO.Data.ItemNew.ProductName = v.Products[0].ProductName;
                        }
                    }
                });
                $.each(_ORDOrder_FTLLO.Data.ListStock, function (i, v) {
                    if (_ORDOrder_FTLLO.Data.ListGroupProduct[v.CUSLocationID].length == 0)
                        _ORDOrder_FTLLO.Data.ListGroupProduct[v.CUSLocationID].push({ ID: "", Code: '', GroupName: '', GroupID: "" });
                });

                _ORDOrder_FTLLO.Data.ListDistributor = [
                    {
                        CUSPartnerID: -1, PartnerID: -1, PartnerCode: 'Kho khách hàng', PartnerName: "Kho khách hàng"
                    }
                ];
                $.each(res.ListDistributor, function (i, v) {
                    _ORDOrder_FTLLO.Data.ListDistributor.push(v);
                });

                //_ORDOrder_FTLLO.Data.ListDistributor = res.ListDistributor;
                _ORDOrder_FTLLO.Data.ListDistributorLocation = res.ListLocation;
                $scope.cbbPartnerOptions.dataSource.data(_ORDOrder_FTLLO.Data.ListDistributor);

                $.each(_ORDOrder_FTLLO.Data.ListDistributor, function (i, v) {
                    _ORDOrder_FTLLO.Data.ListLocation[v.CUSPartnerID] = [];
                });

                //Kho khách hàng
                _ORDOrder_FTLLO.Data.ListLocation["-1"] = [];
                $.each(_ORDOrder_FTLLO.Data.ListStock, function (i, v) {
                    _ORDOrder_FTLLO.Data.ListLocation["-1"].push(v);
                });

                $.each(res.ListLocation, function (i, v) {
                    if (Common.HasValue(_ORDOrder_FTLLO.Data.ListLocation[v.CusPartID])) {
                        _ORDOrder_FTLLO.Data.ListLocation[v.CusPartID].push(v);

                        if (_ORDOrder_FTLLO.Data.ItemNew.LocationToID < 1) {
                            _ORDOrder_FTLLO.Data.ItemNew.PartnerID = v.CusPartID;

                            _ORDOrder_FTLLO.Data.ItemNew.LocationToID = v.CUSLocationID;
                            _ORDOrder_FTLLO.Data.ItemNew.LocationToName = v.LocationName;
                            _ORDOrder_FTLLO.Data.ItemNew.LocationToAddress = v.Address;
                        }
                    }
                });
                $.each(_ORDOrder_FTLLO.Data.ListDistributor, function (i, v) {
                    if (v.CUSPartnerID == _ORDOrder_FTLLO.Data.ItemNew.PartnerID) {
                        _ORDOrder_FTLLO.Data.ItemNew.PartnerName = v.PartnerName;
                        _ORDOrder_FTLLO.Data.ItemNew.PartnerCode = v.PartnerCode;
                    }
                    if (_ORDOrder_FTLLO.Data.ListLocation[v.CUSPartnerID].length == 0)
                        _ORDOrder_FTLLO.Data.ListLocation[v.CUSPartnerID].push({ LocationID: "", CUSLocationID: "", LocationCode: '', LocationName: '', Address: '' });
                });

                _ORDOrder_FTLLO.Data.ListGroupVehicle = res.ListGroupVehicle;
                $scope.ftllo_cboGroupOfVehicleOptions.dataSource.data(_ORDOrder_FTLLO.Data.ListGroupVehicle);
                
                
                Common.Services.Call($http, {
                    url: Common.Services.url.ORD,
                    method: _ORDOrder_FTLLO.URL.GetItem,
                    data: { ID: _ORDOrder_FTLLO.Param.OrderID, CustomerID: _ORDOrder_FTLLO.Param.CustomerID, ServiceOfOrderID: _ORDOrder_FTLLO.Param.ServiceID, TransportModeID: _ORDOrder_FTLLO.Param.TransportID, ContractID: _ORDOrder_FTLLO.Param.ContractID, TermID: _ORDOrder_FTLLO.Param.TermID },
                    success: function (res) {
                        $rootScope.Loading.Change("Lấy dữ liệu đơn hàng...", 80);
                        $scope.Item = res;
                        $scope.Item.ETA = Common.Date.FromJson(res.ETA);
                        $scope.Item.ETD = Common.Date.FromJson(res.ETD);
                        $scope.Item.ETARequest = Common.Date.FromJson(res.ETARequest);
                        $scope.Item.RequestDate = Common.Date.FromJson(res.RequestDate);
                        if ($scope.product_gridOptions != null) {                            
                            $scope.product_gridOptions.dataSource.data($scope.Item.ListProduct);
                            $scope.CreateMap();
                        }
                        $rootScope.Loading.Hide()
                    }
                });
            }
        }
    });

    $scope.ftllo_cboGroupOfVehicleOptions = {        
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true,
        dataTextField: 'GroupName', dataValueField: 'ID', minLength: 3,
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    GroupName: { type: 'string' },
                }
            }
        }), change: function (e) {            
            if (this.text().length > 0 && this.select() == -1) {
                $rootScope.Message({ Msg: "Loại xe không xác định" });
                $scope.Item.GroupOfVehicleID = null;
            }
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
                    method: _ORDOrder_FTLLO.URL.GetDate,
                    data: { item: $scope.Item },
                    success: function (res) {
                        res.ETA = Common.Date.FromJson(res.ETA);
                        res.ETD = Common.Date.FromJson(res.ETD);
                        res.ETARequest = Common.Date.FromJson(res.ETARequest);
                        res.ETDRequest = Common.Date.FromJson(res.ETDRequest);

                        $scope.Item.ETA = res.ETA;
                        $scope.Item.ETD = res.ETD;
                        _ORDOrder_FTLLO.Data.ItemNew.ETA = res.ETA;
                        _ORDOrder_FTLLO.Data.ItemNew.ETD = res.ETD;
                        $scope.Item.ETARequest = res.ETARequest;
                        $scope.Item.ETDRequest = res.ETDRequest;
                        var data = $scope.product_gridOptions.dataSource.data();
                        Common.Data.Each(data, function (o) {
                            o.ETA = res.ETA;
                            o.ETD= res.ETD;
                        })
                        $scope.product_gridOptions.dataSource.sync();
                        $rootScope.IsLoading = false;
                    },
                    error: function (res) {
                        $rootScope.IsLoading = false;
                    }
                });
            }
        }
    }

    $scope.OnETDChange = function ($event, grid) {
        var item = $scope.Item;
        var data = grid.dataSource.data();
        _ORDOrder_FTLLO.Data.ItemNew.ETD = $scope.Item.ETD;

        if ($scope.Item.ContractTermID > 0) {
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.ORD,
                method: _ORDOrder_FTLLO.URL.GetDate,
                data: { item: $scope.Item },
                success: function (res) {

                    res.ETA = Common.Date.FromJson(res.ETA);
                    res.ETD = Common.Date.FromJson(res.ETD);
                    res.ETARequest = Common.Date.FromJson(res.ETARequest);
                    res.ETDRequest = Common.Date.FromJson(res.ETDRequest);

                    $scope.Item.ETA = res.ETA;
                    $scope.Item.ETARequest = res.ETARequest;
                    $scope.Item.ETDRequest = res.ETDRequest;
                    Common.Data.Each(data, function (o) {
                        o.ETA = res.ETA;
                        o.ETD = item.ETD;
                    })
                    grid.dataSource.sync();
                    $rootScope.IsLoading = false;
                },
                error: function (res) {
                    $rootScope.IsLoading = false;
                }
            });
        }
        else {
            Common.Data.Each(data, function (o) {
                o.ETD = item.ETD;
            })
            grid.dataSource.sync();
        }
    }

    $scope.OnETAChange = function ($event, grid) {
        var item = $scope.Item;
        var data = grid.dataSource.data();
        _ORDOrder_FTLLO.Data.ItemNew.ETA = $scope.Item.ETA;

        Common.Data.Each(data, function (o) {
            o.ETA = item.ETA;
        })
        grid.dataSource.sync();
    }

    $scope.OnETARequestChange = function ($event, grid) {
        var item = $scope.Item;
        var data = grid.dataSource.data();
        _ORDOrder_FTLLO.Data.ItemNew.ETARequest = $scope.Item.ETARequest;

        Common.Data.Each(data, function (o) {
            o.ETARequest = item.ETARequest;
        })
        grid.dataSource.sync();
    }
 
    $scope.Number = { format: 'n6', spinners: false, culture: 'en-US', min: 0, step: 0.00001 }

    $scope.OnPriceRouteChange = function (item) {
        Common.Log("ChangeValue");
        if (item.RoutePrice < 0 || item.RoutePrice > 99999999999) {
            item.RoutePrice = 0;
            $rootScope.Message({
                Msg: "Nhập số sai!"
            });
        }
    }

    $scope.product_gridOptions = {
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    Command: { editable: false },
                    CBM: { type: 'number', defaultValue: 0 },
                    Quantity: { type: 'number', defaultValue: 0 },
                    Weight: { type: 'number', defaultValue: 0 },
                    WeightKG: { type: 'number', defaultValue: 0 },
                    LocationFromName: { type: 'string' },
                    GroupOfProductName: { type: 'string' },
                    ProductName: { type: 'string' },
                    PartnerName: { type: 'string' },
                    LocationToName: { type: 'string' },
                    SOCode: { type: 'string' },
                    DNCode: { type: 'string' },
                    LocationToAddress: { type: 'string' },
                    ETD: { type: 'date' },
                    ETA: { type: 'date' },
                    ETARequest: { type: 'date' },
                    IsReturn: { type: 'boolean', editable: false },
                    CUSRoutingCode: { type: 'string' }
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
                field: 'Command', title: ' ', width: '85px', sortorder: 1,
                template: '<a ng-show="!IsProductEdit" href="/" ng-click="ORD_FTLLO_Product_Edit($event,product_grid)" class="k-button"><i class="fa fa-pencil"></i></a>' +
                    '<a ng-show="!IsProductEdit" href="/" ng-click="ORD_FTLLO_Product_Delete($event,product_grid)" class="k-button"><i class="fa fa-trash"></i></a>' +
                    '<a ng-show="IsProductEdit && ProductEdit.ID==#=ID#?true:false" href="/" ng-click="ORD_FTLLO_Product_Save($event,product_grid)" class="k-button"><i class="fa fa-check"></i></a>' +
                    '<a ng-show="IsProductEdit && ProductEdit.ID==#=ID#?true:false" href="/" ng-click="ORD_FTLLO_Product_Cancel($event,product_grid)" class="k-button"><i class="fa fa-ban"></i></a>',
                filterable: false, sortable: false, sortorder: 0, configurable: false, isfunctionalHidden: false
            },
            {
                field: 'LocationFromID', width: 150, title: '{{RS.ORDGroupProduct.LocationFromID}}', template: '#=LocationFromName#', sortable: false,
                editor: '<input class="cus-combobox" focus-k-combobox kendo-combobox k-options="cbbLocationFromOptions" data-bind="value:LocationFromID" ng-model="ProductEdit.LocationFromID"/>',
                sortorder: 1, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'GroupOfProductID', width: 150, title: '{{RS.ORDGroupProduct.GroupOfProductID}}', template: '#=GroupOfProductName#', sortable: false,
                editor: '<input class="cus-combobox" focus-k-combobox kendo-combobox k-options="cbbGroupOfProductOptions" data-bind="value:GroupOfProductID" ng-model="ProductEdit.GroupOfProductID"/>',
                sortorder: 2, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'ProductID', width: 150, title: '{{RS.ORDProduct.ProductID}}', template: '#=ProductName#', sortable: false,
                editor: '<input class="cus-combobox" focus-k-combobox kendo-combobox k-options="cbbProductOptions" data-bind="value:ProductID" ng-model="ProductEdit.ProductID"/>',
                sortorder: 3, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'PartnerID', width: 150, title: '{{RS.ORDGroupProduct.PartnerID}}', template: '#=PartnerID > 0 ? PartnerName : "Kho khách hàng"#', sortable: false,
                editor: '<input class="cus-combobox" focus-k-combobox kendo-combobox k-options="cbbPartnerOptions" data-bind="value:PartnerID" ng-model="ProductEdit.PartnerID"/>',
                sortorder: 4, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'LocationToAddress', width: 250, title: '{{RS.CATLocation.Address}}', sortable: false,
                editor: '<input class="cus-combobox" focus-k-combobox kendo-combobox k-options="cbbLocationToOptions" data-bind="value:LocationToID" ng-model="ProductEdit.LocationToID" style="width:calc(100% - 30px)"/>' +
                    '<a class="k-button" ng-show="ProductEdit.PartnerID>0&&HasCreateLocation" href="/" ng-click="NewLocation_Click($event,dataItem,address_win)" title="Thêm mới địa chỉ"><i class="fa fa-plus"></i></a>',
                sortorder: 5, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'ETD', width: 170, title: '{{RS.ORDGroupProduct.ETD}}', template: '#=Common.Date.FromJsonDMYHM(ETD)#', sortable: false,
                editor: '<input class="cus-datetimepicker" focus-k-datetimepicker kendo-date-time-picker k-options="DateDMYHM" data-bind="value:ETD" k-ng-model="ProductEdit.ETD"/>',
                sortorder: 6, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'ETA', width: 170, title: '{{RS.ORDGroupProduct.ETA}}', template: '#=Common.Date.FromJsonDMYHM(ETA)#', sortable: false,
                editor: '<input class="cus-datetimepicker" focus-k-datetimepicker kendo-date-time-picker k-options="DateDMYHM" data-bind="value:ETA" k-ng-model="ProductEdit.ETA"/>',
                sortorder: 7, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'ETARequest', width: 170, title: '{{RS.ORDGroupProduct.ETARequest}}', template: '#=Common.Date.FromJsonDMYHM(ETARequest)#', sortable: false,
                editor: '<input class="cus-datetimepicker" focus-k-datetimepicker kendo-date-time-picker k-options="DateDMYHM" data-bind="value:ETARequest" k-ng-model="ProductEdit.ETARequest"/>',
                sortorder: 8, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'Quantity', width: 100, title: '{{RS.ORDGroupProduct.Quantity}}',
                editor: '<input ng-model="ProductEdit.Quantity" class="cus-number" kendo-numeric-text-box k-options="Number" min="0" style="width: 100%;" ></input>',
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
                sortorder: 14, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'SOCode', width: 100, title: '{{RS.ORDGroupProduct.SOCode}}',
                editor: '<input ng-model="ProductEdit.SOCode" class="k-textbox" type="text" style="width: 100%;" ></input>',
                sortorder: 15, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'DNCode', width: 100, title: '{{RS.ORDGroupProduct.DNCode}}',
                editor: '<input ng-model="ProductEdit.DNCode" class="k-textbox" type="text" style="width: 100%;" ></input>',
                sortorder: 16, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'IsReturn', width: 100, title: '{{RS.ORDGroupProduct.IsReturn}}', template: '<input type="checkbox" disabled #=IsReturn==true?"checked":""# />',
                sortorder: 17, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'CUSRoutingID', width: 300, title: '{{RS.ORDGroupProduct.CUSRoutingID}}', template: '#=CUSRoutingCode#',
                editor: '<input class="cus-combobox" focus-k-combobox kendo-combobox k-options="cbbRoutingOptions" data-bind="value:CUSRoutingID" ng-model="ProductEdit.CUSRoutingID"/>',
                sortorder: 18, configurable: true, isfunctionalHidden: true, hidden: _ORDOrder_FTLLO.Param.TermID < 0
            },
            {
                title: ' ', filterable: false, sortable: false,
                sortorder: 100, configurable: false, isfunctionalHidden: false
            }
        ]
    }

    $scope.NewLocation_Click = function ($event, item, win) {
        $event.preventDefault();

        $scope.NewLocationItem = {
            CustomerID: _ORDOrder_FTLLO.Param.CustomerID,
            CusPartnerID: item.PartnerID,
            CATLocationID: -1,
            CATLocationCode: "",
            CATLocationName: "",
            CATLocationAddress: "",
            CUSLocationID:-1,
            CUSLocationCode: "",
            CountryID: -1,
            ProvinceID: -1,
            DistrictID: -1,
            AreaCode: "",
        }
        if (_ORDOrder_FTLLO.Data.ListCountry.length > 0)
            $scope.NewLocationItem.CountryID = _ORDOrder_FTLLO.Data.ListCountry[0].ID;
        $scope.LoadRegionData($scope.NewLocationItem);
        win.center().open();
    }

    $scope.IsProductEdit = false;
    $scope.Number = { format: 'n6', spinners: false, culture: 'en-US', min: 0, step: 0.00001, decimals: 6 }
    $scope.NumberTon = { format: 'n6', spinners: false, culture: 'en-US', min : 0, step: 0.000001, decimals: 6, change: function (o) { var val = this.value(); $scope.ProductEdit.WeightKG = val * 1000; } }
    $scope.NumberKG = { format: 'n3', spinners: false, culture: 'en-US', min: 0, step: 0.001, decimals: 3, change: function (o) { var val = this.value(); $scope.ProductEdit.Weight = val / 1000; } }

    $scope.ORD_FTLLO_Product_AddNew = function ($event, grid) {
        $event.preventDefault();

        var data = $scope.product_gridOptions.dataSource.data();
        _ORDOrder_FTLLO.Data.ItemNew.ID = data.length > 0 ? -(data.length + 1) : -1;
        data.splice(0, 0, $.extend(true, {}, _ORDOrder_FTLLO.Data.ItemNew));
        $scope.product_gridOptions.dataSource.data(data);

        $timeout(function () {
            $scope.IsProductEdit = true;
            var items = grid.items();
            $scope.ProductEdit = grid.dataItem(items[0]);
            _ORDOrder_FTLLO.Data.ItemBackUp = $.extend(true, {}, $scope.ProductEdit);
            grid.editRow(items[0]);
            var td = $(items[0]).find('input');
            td[0].focus();
            $timeout(function () {
                $scope.LoadDataFrom();
                $scope.LoadDataTo();
            }, 1)
        }, 1);
    }

    $scope.ORD_FTLLO_Product_Edit = function ($event, grid) {
        $event.preventDefault();

        $timeout(function () {
            $scope.IsProductEdit = true;
            var tr = $event.target.closest('tr');
            $scope.ProductEdit = grid.dataItem(tr);
            if ($scope.ProductEdit.PartnerID == null)
                $scope.ProductEdit.PartnerID = -1;
            _ORDOrder_FTLLO.Data.ItemBackUp = $.extend(true, {}, $scope.ProductEdit);
            grid.editRow(tr);
            var td = $(tr).find('input');
            td[0].focus();
            $scope.LoadDataFrom();
            $scope.LoadDataTo();
        }, 1)
    }

    $scope.ORD_FTLLO_Product_Save = function ($event, grid) {
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

    $scope.ORD_FTLLO_Product_Cancel = function ($event, grid) {
        $event.preventDefault();

        $rootScope.IsLoading = true;
        $scope.IsProductEdit = false;
        var i = 0;
        var data = grid.dataSource.data();
        while (i < data.length) {
            var obj = data[i];
            if (obj.ID == _ORDOrder_FTLLO.Data.ItemBackUp.ID) {
                data.splice(i, 1);
                data.splice(i, 0, $.extend(true, {}, _ORDOrder_FTLLO.Data.ItemBackUp))
            }
            i++;
        }
        $rootScope.IsLoading = false;
    }

    $scope.ORD_FTLLO_Product_Delete = function ($event, grid) {
        $event.preventDefault();

        var obj = grid.dataItem($event.target.closest('tr'));
        if (Common.HasValue(obj)) {
            grid.dataSource.remove(obj);
        }
    }

    $scope.cbbLocationFromOptions = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true, minLength: 2,
        dataTextField: 'LocationName', dataValueField: 'CUSLocationID',
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'CUSLocationID',
                fields: {
                    CUSLocationID: { type: 'number' },
                    LocationName: { type: 'string' }
                }
            }
        }),
        change: function (e) {
            var obj = this.dataItem(this.select());
            if (Common.HasValue(obj)) {
                $scope.ProductEdit.LocationFromName = obj.LocationName;
            }

            try {
                obj = _ORDOrder_FTLLO.Data.ListGroupProduct[$scope.ProductEdit.LocationFromID][0];
                if (Common.HasValue(obj)) {
                    $scope.ProductEdit.GroupOfProductID = obj.GroupID;
                    $scope.ProductEdit.GroupOfProductName = obj.GroupName;
                } else {
                    $scope.ProductEdit.GroupOfProductID = "";
                    $scope.ProductEdit.GroupOfProductName = "";
                }
                if ($scope.ProductEdit.GroupOfProductID > 0) {
                    try {
                        obj = _ORDOrder_FTLLO.Data.ListProduct[$scope.ProductEdit.LocationFromID + "_" + $scope.ProductEdit.GroupOfProductID][0]
                        if (Common.HasValue(obj)) {
                            $scope.ProductEdit.ProductID = obj.ID;
                            $scope.ProductEdit.ProductName = obj.ProductName;
                        } else {
                            $scope.ProductEdit.ProductID = "";
                            $scope.ProductEdit.ProductName = "";
                        }
                    }
                    catch (e) {
                        $scope.ProductEdit.ProductID = "";
                        $scope.ProductEdit.ProductName = "";
                    }
                } else {
                    $scope.ProductEdit.ProductID = "";
                    $scope.ProductEdit.ProductName = "";
                }
                $timeout(function () {
                    $scope.LoadDataFrom();
                }, 1)
            } catch (e) {
            }
        }
    };

    $scope.cbbGroupOfProductOptions = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true, minLength: 2,
        dataTextField: 'GroupName', dataValueField: 'GroupID',
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'GroupID',
                fields: {
                    GroupID: { type: 'number' },
                    GroupName: { type: 'string' }
                }
            }
        }),
        change: function (e) {
            var obj = this.dataItem(this.select());
            if (Common.HasValue(obj)) {
                $scope.ProductEdit.GroupOfProductName = obj.GroupName;
            }

            try {
                obj = _ORDOrder_FTLLO.Data.ListProduct[$scope.ProductEdit.LocationFromID + "_" + $scope.ProductEdit.GroupOfProductID][0]
                if (Common.HasValue(obj)) {
                    $scope.ProductEdit.ProductID = obj.ID;
                    $scope.ProductEdit.ProductName = obj.ProductName;
                }
            }
            catch (e) {
                $scope.ProductEdit.ProductID = "";
                $scope.ProductEdit.ProductName = "";
            }

            $timeout(function () {
                $scope.LoadDataFrom();
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
                $scope.ProductEdit.ProductName = _ORDOrder_FTLLO.Data.ListProduct[$scope.ProductEdit.LocationFromID + "_" + $scope.ProductEdit.GroupOfProductID][0].ProductName;
            } catch (e) {
            }
        }
    };

    $scope.cbbPartnerOptions = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true, minLength: 2,
        dataTextField: 'PartnerName', dataValueField: 'CUSPartnerID',
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'CUSPartnerID',
                fields: {
                    CUSPartnerID: { type: 'number' },
                    PartnerName: { type: 'string' }
                }
            }
        }),
        change: function (e) {
            var obj = this.dataItem(this.select());
            if (Common.HasValue(obj)) {
                $scope.ProductEdit.PartnerName = obj.PartnerName;
                $scope.ProductEdit.PartnerCode = obj.PartnerCode;
            }
            try {
                obj = _ORDOrder_FTLLO.Data.ListLocation[$scope.ProductEdit.PartnerID][0]
                if (Common.HasValue(obj)) {
                    $scope.ProductEdit.LocationToID = obj.CUSLocationID;
                    $scope.ProductEdit.LocationToName = obj.LocationName;
                    $scope.ProductEdit.LocationToAddress = obj.Address;
                }

                $scope.LoadDataTo();
                $timeout(function () { $scope.LoadPriceRoute(); }, 1);
            } catch (e) {
            }            
        }
    };

    $scope.cbbLocationToOptions = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true, minLength: 2,
        dataTextField: 'Address', dataValueField: 'CUSLocationID',
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'CUSLocationID',
                fields: {
                    CUSLocationID: { type: 'number' },
                    Address: { type: 'string' }
                }
            }
        }),
        change: function (e) {
            var obj = this.dataItem(this.select());
            if (Common.HasValue(obj)) {
                $scope.ProductEdit.LocationToAddress = obj.Address;
                $scope.ProductEdit.LocationToName = obj.LocationName;
            }
            else {
                $scope.ProductEdit.LocationToAddress = "";
                $scope.ProductEdit.LocationToName = "";
            }
            $timeout(function () { $scope.LoadPriceRoute(); }, 1);
        }
    };

    $scope.LoadDataFrom = function () {
        Common.Log("LoadDataFrom");
        var locationID = $scope.ProductEdit.LocationFromID;
        var gopID = $scope.ProductEdit.GroupOfProductID;
        var productID = $scope.ProductEdit.ProductID;
        $timeout(function () {
            $scope.cbbGroupOfProductOptions.dataSource.data(_ORDOrder_FTLLO.Data.ListGroupProduct[locationID]);
            $scope.ProductEdit.GroupOfProductID = gopID;
            if (gopID > 0)
                $scope.cbbProductOptions.dataSource.data(_ORDOrder_FTLLO.Data.ListProduct[locationID + "_" + gopID]);
            else
                $scope.cbbProductOptions.dataSource.data([]);
            $scope.ProductEdit.ProductID = productID;
        }, 1)
    };

    $scope.LoadDataTo = function () {
        Common.Log("LoadDataTo");
        var partnerID = $scope.ProductEdit.PartnerID;
        var locationID = $scope.ProductEdit.LocationToID;
        $timeout(function () {
            $scope.cbbLocationToOptions.dataSource.data(_ORDOrder_FTLLO.Data.ListLocation[partnerID]);
            $scope.ProductEdit.LocationToID = locationID;
        }, 1);
    };
    
    $scope.cbbRoutingOptions = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true, minLength: 2,
        dataTextField: 'Code', dataValueField: 'ID',
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' }, Code: { type: 'string' }
                }
            }
        }),
        change: function (e) {
            var obj = this.dataItem(this.select());
            if (Common.HasValue(obj)) {
                $scope.ProductEdit.CusRoutingCode = obj.Code;
            }
            else {
                $scope.ProductEdit.CusRoutingCode = "";
            }
        }
    };

    $scope.cboCountryOptions = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true, minLength: 2,
        dataTextField: 'CountryName', dataValueField: 'ID',
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' }, CountryName: { type: 'string' }
                }
            }
        }),
        change: function (e) {
            var cbo = this;
            if (e.sender.selectedIndex >= 0) {
                $scope.NewLocationItem.ProvinceID = -1;
                $scope.NewLocationItem.DistrictID = -1;
                $scope.LoadRegionData($scope.NewLocationItem);
            }
            else {
                $scope.NewLocationItem.CountryID = "";
                $scope.NewLocationItem.ProvinceID = "";
                $scope.NewLocationItem.DistrictID = "";
                $scope.LoadRegionData($scope.NewLocationItem);
            }
        }
    }

    $scope.cboProvinceOptions = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true, minLength: 2,
        dataTextField: 'ProvinceName', dataValueField: 'ID',
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' }, ProvinceName: { type: 'string' }
                }
            }
        }),
        change: function (e) {
            var cbo = this;
            if (e.sender.selectedIndex >= 0) {
                $scope.NewLocationItem.DistrictID = -1;
                $scope.LoadRegionData($scope.NewLocationItem);
            }
            else {
                $scope.NewLocationItem.ProvinceID = "";
                $scope.NewLocationItem.DistrictID = "";
                $scope.LoadRegionData($scope.NewLocationItem);
            }
        }
    }

    $scope.cboDistrictOptions = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true, minLength: 2,
        dataTextField: 'DistrictName', dataValueField: 'ID',
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' }, DistrictName: { type: 'string' }
                }
            }
        })
    }

    $scope.DataProvince = {};
    $scope.DataDistrict = {};

    Common.Services.Call($http, {
        url: Common.Services.url.CAT,
        method: Common.ALL.URL.Country,
        success: function (res) {
            $scope.cboCountryOptions.dataSource.data(res.Data);
            _ORDOrder_FTLLO.Data.ListCountry = res.Data;
            $scope.DataProvince = {};
            Common.Data.Each(res.Data, function (o) {
                if ($scope.DataProvince[o.ID] == null)
                    $scope.DataProvince[o.ID] = [];
            })
            Common.Services.Call($http, {
                url: Common.Services.url.CAT,
                method: Common.ALL.URL.Province,
                data: {},
                success: function (res) {
                    $scope.DataDistrict = {};
                    Common.Data.Each(res.Data, function (o) {
                        if ($scope.DataDistrict[o.ID] == null)
                            $scope.DataDistrict[o.ID] = [];
                        $scope.DataProvince[o.CountryID].push(o);
                    })
                    Common.Services.Call($http, {
                        url: Common.Services.url.CAT,
                        method: Common.ALL.URL.District,
                        data: {},
                        success: function (res) {
                            Common.Data.Each(res.Data, function (o) {
                                $scope.DataDistrict[o.ProvinceID].push(o);
                            })
                        }
                    });
                }
            });
        }
    });

    $scope.LoadRegionData = function (item) {
        Common.Log("LoadRegionData");
        try {
            var countryID = item.CountryID;
            var provinceID = item.ProvinceID;
            var districtID = item.DistrictID;
            var wardID = item.WardID;

            var data = $scope.DataProvince[countryID];
            $scope.cboProvinceOptions.dataSource.data(data);
            if (Common.HasValue(provinceID)) {
                if (provinceID < 0) {
                    provinceID = data[0].ID;
                }
            }
            $timeout(function () {
                item.ProvinceID = provinceID;
            }, 1)

            data = $scope.DataDistrict[provinceID];

            $scope.cboDistrictOptions.dataSource.data(data);
            if (Common.HasValue(districtID)) {
                if (districtID < 0) {
                    districtID = data[0].ID;
                }
            }
            $timeout(function () {
                item.DistrictID = districtID;
            }, 1)

            //data = _SYSCustomer_Index.Data.Ward[districtID];
            //$scope.cboWardOptions.dataSource.data(data);
            //if (wardID < 1 && data.length > 0)
            //    wardID = data[0].ID;
            //$timeout(function () {
            //    item.WardID = wardID;
            //}, 1)
        }
        catch (e) { }
    }

    $scope.NewLocationSave = function ($event, vform, win) {
        $event.preventDefault();

        if (vform()) {
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.ORD,
                method: _ORDOrder_FTLLO.URL.NewLocation,
                data: { item: $scope.NewLocationItem},
                success: function (res) {
                    if(Common.HasValue(res)&&res!='')
                        _ORDOrder_FTLLO.Data.ListLocation[$scope.NewLocationItem.CusPartnerID].push(res);
                    
                    $scope.LoadDataTo();
                    $rootScope.IsLoading = false;
                    $rootScope.Message({ Msg: "Thành công!" });
                    win.close();
                }
            });
        }
    }

    $scope.LoadPriceRoute = function () {
        if ($scope.Item.IsMain && $scope.Item.ContractID > 0) {
            var fID = 0, tID = 0;
            var data = $scope.product_gridOptions.dataSource.data();
            if (data.length > 0) {
                fID = data[0].LocationFromID;
                tID = data[0].LocationToID;
            }
            Common.Services.Call($http, {
                url: Common.Services.url.ORD,
                method: _ORDOrder_FTLLO.URL.PriceRoute,
                data: { ContractID: $scope.Item.ContractID, FromID: fID, ToID: tID, GOVID: $scope.Item.GroupOfVehicleID, TypeID: $scope.Item.TypeOfOrderID },
                success: function (res) {
                    $timeout(function () {
                        $scope.Item.RoutePrice = res;
                    }, 1);
                }
            });
        }
    }

    $scope.SuggestVehicle = function () {
        //var data = $scope.product_gridOptions.dataSource.data(), ton = 0;
        //for (var i = 0; i < data.length; i++) {
        //    ton += data[i].Weight;
        //}

        //var length = _ORDOrder_FTLLO.Data.ListGroupVehicle.length;
        //if (length > 0) {
        //    var obj = null, flag = false;
        //    for (var i = 0; i < length; i++) {
        //        obj = _ORDOrder_FTLLO.Data.ListGroupVehicle[i];
        //        if (obj.Ton >= ton) {
        //            flag = true;
        //            $scope.Item.GroupOfVehicleID = obj.ID;
        //            break;
        //        }
        //    }
        //    if (!flag)
        //        $scope.Item.GroupOfVehicleID = obj.ID;

        //    $scope.LoadPriceRoute();
        //}
    }

    $scope.CreateMap = function () {
        Common.Log("CreateMap");
        openMapV2.hasMap = true;
        openMapV2.Init({
            Element: 'ftllo_map',
            Tooltip_Show: true,
            Tooltip_Element: 'ftllo_tooltip',
            DefinedLayer: [{
                Name: 'VectorMarkerStock',
                zIndex: 100
            }, {
                Name: 'VectorMarkerDistributorLocation',
                zIndex: 100
            }, {
                Name: 'VectorRoute',
                zIndex: 90
            }]
        });
        openMapV2.ClearVector("VectorMarkerStock");
        openMapV2.ClearVector("VectorMarkerDistributorLocation");
        var icon = openMapV2.NewStyle.Icon(Common.String.Format(openMapV2.NewImage.NewStock(openMapV2.NewImage.Color.Green)), 1);
        Common.Data.Each(_ORDOrder_FTLLO.Data.ListStock, function (o) {
            if (Common.HasValue(o.Lat) && Common.HasValue(o.Lng)) {
                openMapV2.NewMarker(o.Lat, o.Lng, o.CUSLocationID, o.LocationName, icon, o, "VectorMarkerStock");
            }
        })

        icon = openMapV2.NewStyle.Icon(Common.String.Format(openMapV2.NewImage.NewDistributor(openMapV2.NewImage.Color.Green)), 1);
        Common.Data.Each(_ORDOrder_FTLLO.Data.ListDistributorLocation, function (o) {
            if (Common.HasValue(o.Lat) && Common.HasValue(o.Lng)) {
                openMapV2.NewMarker(o.Lat, o.Lng, o.CUSLocationID, o.LocationName, icon, o, "VectorMarkerDistributorLocation");
            }
        })
        $scope.ReloadMap();
    }

    $scope.ReloadMap = function () {
        Common.Log("ReloadMap");

        openMapV2.Close();
        openMapV2.ClearVector("VectorRoute");
        openMapV2.Visible("VectorMarkerStock", false);
        openMapV2.Visible("VectorMarkerDistributorLocation", false);
        $.each($scope.product_gridOptions.dataSource.data(), function (i, v) {
            var f1 = openMapV2.GetFeature(v.LocationFromID, "VectorMarkerStock");
            var f2 = openMapV2.GetFeature(v.LocationToID, "VectorMarkerDistributorLocation");
            if (Common.HasValue(f1) && Common.HasValue(f2)) {
                openMapV2.Visible([f1, f2], true);
                openMapV2.NewRoute(f1, f2, "", v.LocationFromName + "-" + v.LocationToName, openMapV2.NewStyle.Line(6, 'rgba(3, 169, 244, 0.6)'), null, "VectorRoute", null, function () {
                    openMapV2.FitBound("VectorRoute", 15);
                });
            }
        })
    }
    
    $scope.ftllo_cboTypeOfOrderOptions = {
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

    Common.ALL.Get($http, {
        url: Common.ALL.URL.SYSVarTypeOfOrder,
        success: function (res) {
            $scope.ftllo_cboTypeOfOrderOptions.dataSource.data(res);
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
        method: _ORDOrder_FTLLO.URL.Contract,
        data: _ORDOrder_FTLLO.Param,
        success: function (res) {
            $scope.contract_gridOptions.dataSource.data(res.Data);
        }
    });

    $scope.ORD_FTLLO_ChangeContract = function ($event, win) {
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
                Ok: function () {
                    $scope.Item.ContractID = obj.ID;
                    _ORDOrder_FTLLO.Param.ContractID = obj.ID;

                    if ($scope.Item.ID > 0) {
                        Common.Services.Call($http, {
                            url: Common.Services.url.ORD,
                            method: _ORDOrder_FTLLO.URL.Contract_Change,
                            data: _ORDOrder_FTLLO.Param,
                            success: function (res) {
                                Common.Services.Error(res, function (res) {
                                    $state.go("main.ORDOrder.FTLLO", _ORDOrder_FTLLO.Param);
                                })
                            }
                        });
                    } else {
                        $state.go("main.ORDOrder.FTLLO", _ORDOrder_FTLLO.Param);
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
                _ORDOrder_FTLLO.Param.ContractID = -1;

                if ($scope.Item.ID > 0) {
                    Common.Services.Call($http, {
                        url: Common.Services.url.ORD,
                        method: _ORDOrder_FTLLO.URL.Contract_Change,
                        data: _ORDOrder_FTLLO.Param,
                        success: function (res) {
                            Common.Services.Error(res, function (res) {
                                $state.go("main.ORDOrder.FTLLO", _ORDOrder_FTLLO.Param);
                            })
                        }
                    });
                } else {
                    $state.go("main.ORDOrder.FTLLO", _ORDOrder_FTLLO.Param);
                }

                win.close();
            }
        });
    }

    $scope.Contract_Close_Click = function ($event, win) {
        $event.preventDefault();

        win.close();
    }

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
        if ($scope.product_gridOptions.dataSource.data().length == 0) {
            $rootScope.Message({
                Type: Common.Message.Type.Alert,
                Msg: 'Không có thông tin chi tiết đơn hàng.'
            })
            return false;
        }       
        if (!$scope.IsValidDate($scope.Item.ETD, "Ngày ETD không đúng.")) {
            $scope.Item.ETD = null;
            return false;
        }
        if (!$scope.IsValidDate($scope.Item.ETA, "Ngày ETA không đúng.")) {
            return false;
        }
        if (!$scope.IsValidDate($scope.Item.RequestDate, "Ngày gởi yêu cẩu không đúng.")) {
            return false;
        }
        if (!$scope.IsValidDate($scope.Item.ETARequest, "Ngày giao hàng không đúng.")) {
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

    $scope.ORD_FTLLO_Update = function ($event) {
        $event.preventDefault();
        if ($scope.CheckItem()) {
            $rootScope.Message({
                Type: Common.Message.Type.Confirm,
                Msg: 'Bạn muốn lưu đơn hàng?',
                Ok: function () {
                    $rootScope.IsLoading = true;
                    $scope.Item.ListProduct = $scope.product_gridOptions.dataSource.data();
                    Common.Services.Call($http, {
                        url: Common.Services.url.ORD,
                        method: _ORDOrder_FTLLO.URL.Update,
                        data: { item: $scope.Item },
                        success: function (res) {
                            $rootScope.IsLoading = false;                            
                            Common.Services.Error(res, function (res) {
                                $rootScope.Message({ Msg: "Đã cập nhật!" });
                                if (_ORDOrder_FTLLO.Param.OrderID != res) {
                                    _ORDOrder_FTLLO.Param.OrderID = res;
                                    if ($scope.Auth.ActAddAndApproved) {
                                        $rootScope.Message({
                                            Msg: "Bạn muốn gủi điều phối đơn hàng?",
                                            Type: Common.Message.Type.Confirm,
                                            Ok: function () {
                                                $rootScope.IsLoading = true;
                                                Common.Services.Call($http, {
                                                    url: Common.Services.url.ORD,
                                                    method: _ORDOrder_FTLLO.URL.ToOPS_Check,
                                                    data: { lst: [_ORDOrder_FTLLO.Param.OrderID] },
                                                    success: function (res) {
                                                        Common.Services.Error(res, function (res) {
                                                            $rootScope.IsLoading = false;
                                                            if (res.length > 0) {
                                                                $rootScope.Message({ Msg: "Thiếu dữ liệu thiết lập cung đường!", Type: Common.Message.Type.Alert });
                                                                $state.go("main.ORDOrder.FTLLO", {
                                                                    OrderID: _ORDOrder_FTLLO.Param.OrderID,
                                                                    CustomerID: _ORDOrder_FTLLO.Param.CustomerID,
                                                                    ServiceID: _ORDOrder_FTLLO.Param.ServiceID,
                                                                    TransportID: _ORDOrder_FTLLO.Param.TransportID,
                                                                    ContractID: _ORDOrder_FTLLO.Param.ContractID != null ? _ORDOrder_FTLLO.Param.ContractID : -1,
                                                                    TermID: _ORDOrder_FTLLO.Param.TermID != null ? _ORDOrder_FTLLO.Param.TermID : -1
                                                                });
                                                            } else {
                                                                $rootScope.IsLoading = true;
                                                                Common.Services.Call($http, {
                                                                    url: Common.Services.url.ORD,
                                                                    method: _ORDOrder_FTLLO.URL.ToOPS,
                                                                    data: { lst: [_ORDOrder_FTLLO.Param.OrderID] },
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
                                            },
                                            Close: function () {
                                                $state.go("main.ORDOrder.FTLLO", {
                                                    OrderID: res,
                                                    CustomerID: _ORDOrder_FTLLO.Param.CustomerID,
                                                    ServiceID: _ORDOrder_FTLLO.Param.ServiceID,
                                                    TransportID: _ORDOrder_FTLLO.Param.TransportID,
                                                    ContractID: _ORDOrder_FTLLO.Param.ContractID != null ? _ORDOrder_FTLLO.Param.ContractID : -1,
                                                    TermID: _ORDOrder_FTLLO.Param.TermID != null ? _ORDOrder_FTLLO.Param.TermID : -1
                                                });
                                            }
                                        })
                                    } else {
                                        $rootScope.Message({ Msg: "Thành công!" });
                                        $state.go("main.ORDOrder.FTLLO", {
                                            OrderID: res,
                                            CustomerID: _ORDOrder_FTLLO.Param.CustomerID,
                                            ServiceID: _ORDOrder_FTLLO.Param.ServiceID,
                                            TransportID: _ORDOrder_FTLLO.Param.TransportID,
                                            ContractID: _ORDOrder_FTLLO.Param.ContractID != null ? _ORDOrder_FTLLO.Param.ContractID : -1,
                                            TermID: _ORDOrder_FTLLO.Param.TermID != null ? _ORDOrder_FTLLO.Param.TermID : -1
                                        });
                                    }
                                }
                            })
                            $scope.Item.ETA = $scope.isDateOrNull($scope.Item.ETA);
                            $scope.Item.ETD = $scope.isDateOrNull($scope.Item.ETD);
                            $scope.Item.RequestDate = $scope.isDateOrNull($scope.Item.RequestDate);
                            $scope.Item.ETARequest = $scope.isDateOrNull($scope.Item.ETARequest);
                        }
                    });
                }
            });
        }      
    }
    
    $scope.ORD_FTLLO_Delete = function ($event) {
        $event.preventDefault();

        $rootScope.Message({
            Type: Common.Message.Type.Confirm,
            Msg: 'Bạn muốn xóa đơn hàng?',
            Ok: function () {
                Common.Services.Call($http, {
                    url: Common.Services.url.ORD,
                    method: _ORDOrder_FTLLO.URL.Delete,
                    data: { id: _ORDOrder_FTLLO.Param.OrderID },
                    success: function (res) {
                        Common.Services.Error(res, function (res) {
                            $rootScope.Message({ Msg: "Đã xóa!" });
                            $state.go("main.ORDOrder.Index");
                        })
                    }
                });
            }
        });
    }

    $scope.ORD_FTLLO_Back = function ($event) {
        $event.preventDefault();
        if (_ORDOrder_FTLLO.Param.OrderID > 0) {
            $state.go('main.ORDOrder.Index');
        } else {
            $state.go("main.ORDOrder.New");
        }
    }

    $scope.ShowSetting = function ($event, grid) {
        $event.preventDefault();
        $rootScope.ShowSetting({
            ListView: [],
            event: $event,
            grid: grid,
            current: $state.current
        });
    };

    $scope.HideSetting = function ($event) {
        $event.preventDefault();
        $rootScope.HideSetting();
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