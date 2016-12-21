/// <reference path="~/Scripts/jquery-1.9.1.intellisense.js" />
/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />
/// <reference path="~/Scripts/map.js" />

//#region URL

var _ORDOrder_LCLIMEX = {
    URL: {
        GetItem: 'ORDOrder_GetItem',
        Data: 'ORDOrder_IMEX_Data',

        Delete: 'ORDOrder_Delete',
        Update: '',

        Contract: 'ORDOrder_Contract_List',
        Contract_Change: 'ORDOrder_Contract_Change',

        OPS: 'ORDOrder_ToOPS',
        OPS_Check: 'ORDOrder_ToOPSCheck',
        Container_OPS_Check: 'ORDOrderContainer_ToOPSCheck',
        GetDate: 'ORDOrder_GetDate'
    },
    Data: {
        ItemProductNew: {
            ID: "", GroupOfProductID: "", GroupOfProductName: '', ProductID: "", ProductName: "", CBM: 0, Quantity: 0, Weight: 0, WeightKG: 0, SOCode: '', TempMin: null, TempMax: null
        },
        ItemProductBackUp: null,
        ItemContainerBackUp: null,
        ListGroupProduct: null,
        ListProduct:null,

        ListDepot: [],
        ListStock: null,
        ListSeaPort: null,
        ListCarrier: null,

        ListContainer: null,
        ListService: null
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

angular.module('myapp').controller('ORDOrder_LCLIMEXCtrl', ['$rootScope', '$scope', '$http', '$location', '$state', '$timeout', 'openMapV2', function ($rootScope, $scope, $http, $location, $state, $timeout, openMapV2) {
    if ($rootScope.IsPageComplete != true) return;
    Common.Log('ORDOrder_LCLIMEXCtrl');
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

    _ORDOrder_LCLIMEX.Param = $.extend(true, _ORDOrder_LCLIMEX.Param, $state.params);
    if (_ORDOrder_LCLIMEX.Param.OrderID == "" || !angular.isNumber(parseInt(_ORDOrder_LCLIMEX.Param.OrderID))
        || _ORDOrder_LCLIMEX.Param.CustomerID == "" || !angular.isNumber(parseInt(_ORDOrder_LCLIMEX.Param.CustomerID))
        || _ORDOrder_LCLIMEX.Param.ServiceID == "" || !angular.isNumber(parseInt(_ORDOrder_LCLIMEX.Param.ServiceID))
        || _ORDOrder_LCLIMEX.Param.TransportID == "" || !angular.isNumber(parseInt(_ORDOrder_LCLIMEX.Param.TransportID))
        || _ORDOrder_LCLIMEX.Param.ContractID == "" || !angular.isNumber(parseInt(_ORDOrder_LCLIMEX.Param.ContractID))
        || _ORDOrder_LCLIMEX.Param.TermID == "" || !angular.isNumber(parseInt(_ORDOrder_LCLIMEX.Param.TermID))) {
        $rootScope.Message({ Msg: 'Đường dẫn ko đúng! Quay về trang đơn hàng!' });
        $state.go("main.ORDOrder.Index");
    }
    $scope.Item = null;
    $scope.ProductEdit = null;
    $scope.ObjGroupProduct = null;
    $scope.ContainerEdit = null;
    $scope.NoContainer = 1;
    $scope.ContainerNew = {
        ID: "",  PackingID: "",  PackingName: "",ContainerNo: "", SealNo1: "", SealNo2: "", Note: "", Ton: 0,  Note1: '', Note2: ''
    }

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
        method: _ORDOrder_LCLIMEX.URL.Data,
        data: { CustomerID: _ORDOrder_LCLIMEX.Param.CustomerID },
        success: function (res) {
            $rootScope.Loading.Change("Lấy dữ liệu đơn hàng...", 50);
            $scope.hor_splitter.resize();
            if (Common.HasValue(res)) {
                _ORDOrder_LCLIMEX.Data.ListStock = res.ListStock;
                _ORDOrder_LCLIMEX.Data.ListSeaPort = res.ListSeaPort;

                _ORDOrder_LCLIMEX.Data.ListGroupProduct = res.ListGroupProduct;
                if (res.ListGroupProduct.length > 0) {
                    _ORDOrder_LCLIMEX.Data.ItemProductNew.GroupOfProductID = res.ListGroupProduct[0].ID;
                    _ORDOrder_LCLIMEX.Data.ItemProductNew.GroupOfProductName = res.ListGroupProduct[0].GroupName;
                }

                _ORDOrder_LCLIMEX.Data.ListProduct = {};
                Common.Data.Each(res.ListProduct, function (o) {
                    if (!Common.HasValue(_ORDOrder_LCLIMEX.Data.ListProduct[o.GroupOfProductID])) {
                        _ORDOrder_LCLIMEX.Data.ListProduct[o.GroupOfProductID] = [o];
                    }
                    else {
                        _ORDOrder_LCLIMEX.Data.ListProduct[o.GroupOfProductID].push(o);
                    }
                })
                var dataDoc = [];
                dataDoc.push({ ID: -1, ValueOfVar: ' ' });
                Common.Data.Each(res.ListTypeOfDoc, function (o) {
                    dataDoc.push(o);
                })
                $scope.DataDoc = dataDoc;
                

                _ORDOrder_LCLIMEX.Data.ListCarrier = res.ListCarrier;
                Common.Data.Each(_ORDOrder_LCLIMEX.Data.ListCarrier, function (o) {
                    _ORDOrder_LCLIMEX.Data.ListDepot[o.CUSPartnerID] = [{
                        CUSLocationID: "", LocationName: ''
                    }];
                })
                Common.Data.Each(res.ListDepot, function (o) {
                    _ORDOrder_LCLIMEX.Data.ListDepot[o.CusPartID].push(o);
                })

                $timeout(function () {
                    $scope.fclimex_cboTypeOfDocOptions.dataSource.data(dataDoc);
                    $scope.cbbGroupOfProductOptions.dataSource.data(_ORDOrder_LCLIMEX.Data.ListGroupProduct);
                    $scope.fclimex_cboShippingCoOptions.dataSource.data(_ORDOrder_LCLIMEX.Data.ListCarrier);
                }, 1)
                Common.Services.Call($http, {
                    url: Common.Services.url.ORD,
                    method: _ORDOrder_LCLIMEX.URL.GetItem,
                    data: { ID: _ORDOrder_LCLIMEX.Param.OrderID, CustomerID: _ORDOrder_LCLIMEX.Param.CustomerID, ServiceOfOrderID: _ORDOrder_LCLIMEX.Param.ServiceID, TransportModeID: _ORDOrder_LCLIMEX.Param.TransportID, ContractID: _ORDOrder_LCLIMEX.Param.ContractID, TermID: _ORDOrder_LCLIMEX.Param.TermID },
                    success: function (res) {
                        $rootScope.Loading.Change("Lấy dữ liệu đơn hàng...", 80);
                        switch (res.ViewID) {
                            default:
                            case 1://IM
                                $scope.fclimex_cboLocationFromOptions.dataSource.data(_ORDOrder_LCLIMEX.Data.ListSeaPort);
                                $scope.fclimex_cboLocationToOptions.dataSource.data(_ORDOrder_LCLIMEX.Data.ListStock);

                                break;
                            case 2://EX
                                $scope.fclimex_cboLocationFromOptions.dataSource.data(_ORDOrder_LCLIMEX.Data.ListStock);
                                $scope.fclimex_cboLocationToOptions.dataSource.data(_ORDOrder_LCLIMEX.Data.ListSeaPort);

                                break;
                        }

                        $scope.Item = res;
                        $scope.Item.ETA = Common.Date.FromJson(res.ETA);
                        $scope.Item.ETD = Common.Date.FromJson(res.ETD);
                        $scope.Item.ETARequest = Common.Date.FromJson(res.ETARequest);
                        $scope.Item.ETDRequest = Common.Date.FromJson(res.ETDRequest);
                        $scope.Item.RequestDate = Common.Date.FromJson(res.RequestDate);
                        //Binding
                        if ($scope.Item.ID < 1) {
                            obj = _ORDOrder_LCLIMEX.Data.ListCarrier[0];
                            if (Common.HasValue(obj)) {
                                $scope.Item.PartnerID = obj.CUSPartnerID;
                            }
                            switch ($scope.Item.ViewID) {
                                default:
                                case 1://IM
                                    var obj = _ORDOrder_LCLIMEX.Data.ListSeaPort[0];
                                    if (Common.HasValue(obj)) {
                                        $scope.Item.LocationFromID = obj.CUSLocationID;
                                        $scope.Item.LocationFromName = obj.LocationName;
                                    }
                                    obj = _ORDOrder_LCLIMEX.Data.ListStock[0];
                                    if (Common.HasValue(obj)) {
                                        $scope.Item.LocationToID = obj.CUSLocationID;
                                        $scope.Item.LocationToName = obj.LocationName;
                                    }
                                    break;
                                case 2://EX
                                    var obj = _ORDOrder_LCLIMEX.Data.ListStock[0];
                                    if (Common.HasValue(obj)) {
                                        $scope.Item.LocationFromID = obj.CUSLocationID;
                                        $scope.Item.LocationFromName = obj.LocationName;
                                    }
                                    obj = _ORDOrder_LCLIMEX.Data.ListSeaPort[0];
                                    if (Common.HasValue(obj)) {
                                        $scope.Item.LocationToID = obj.CUSLocationID;
                                        $scope.Item.LocationToName = obj.LocationName;
                                    }
                                    break;
                            }
                        } else {
                            
                            $timeout(function () {
                                $scope.product_gridOptions.dataSource.data($scope.Item.ListGroupProduct);
                                $scope.container_gridOptions.dataSource.data($scope.Item.ListContainer);
                                Common.Log($scope.Item.ListContainer)
                            }, 1);
                            
                        }
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
            $timeout(function () {
                $scope.ReloadMap();
            }, 1)
        }
    }

    $scope.LoadDepot = function (flag) {
        Common.Log("LoadDepot")
        var partnerID = $scope.Item.PartnerID;
        $timeout(function () {
            var data = _ORDOrder_LCLIMEX.Data.ListDepot[partnerID];
            $scope.fclimex_cboLocationDepotOptions.dataSource.data(data);
            if (data.length > 1) {
                
                if (flag) {
                    var obj = data[1];
                    $scope.Item.LocationDepotID = obj.CUSLocationID;
                    
                }
                else {
                    if ($scope.Item.ID < 1) {
                        var objF = data[1];
                        $scope.Item.LocationDepotID = objF.CUSLocationID;
                    }
                }
            } else {
                
                if (flag) {
                    var obj = data[0];
                    $scope.Item.LocationDepotID = obj.CUSLocationID;
                }
                else {
                    if ($scope.Item.ID < 1) {
                        var obj = data[0];
                        $scope.Item.LocationDepotID = obj.CUSLocationID;
                    }
                }
            }
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
                $timeout(function () {
                    $scope.ReloadMap();
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
                $timeout(function () {
                    $scope.ReloadMap();
                }, 100)
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
                    method: _ORDOrder_FCLLO.URL.GetDate,
                    data: { item: $scope.Item },
                    success: function (res) {
                        res.ETA = Common.Date.FromJson(res.ETA);
                        res.ETD = Common.Date.FromJson(res.ETD);
                        res.ETARequest = Common.Date.FromJson(res.ETARequest);
                        res.ETDRequest = Common.Date.FromJson(res.ETDRequest);

                        $scope.Item.ETA = res.ETA;
                        $scope.Item.ETARequest = res.ETARequest;
                        $scope.Item.ETD = res.ETD;
                        $scope.Item.ETDRequest = res.ETDRequest;
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
            var data = $scope.container_gridOptions.dataSource.data();
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

    $scope.fclimex_dtpETAOptions = {
        format: 'dd/MM/yyyy HH:mm',
        timeFormat: "HH:mm",
        parseFormats: ["yyyy-MM-ddTHH:mm:ss"],
        change: function (e) {
            
        }
    }

    $scope.fclimex_dtpETARequestOptions = {
        format: 'dd/MM/yyyy HH:mm',
        timeFormat: "HH:mm",
        parseFormats: ["yyyy-MM-ddTHH:mm:ss"],
        change: function (e) {
           
        }
    }

    $scope.fclimex_dtpETDRequestOptions = {
        format: 'dd/MM/yyyy HH:mm',
        timeFormat: "HH:mm",
        parseFormats: ["yyyy-MM-ddTHH:mm:ss"],
        change: function (e) {
           
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

    //Tab container
    $scope.container_gridOptions = {
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    PackingName: { type: 'string' },
                    PackingID: { type: 'number' }
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

    $scope.ORD_FCLIMEX_Container_AddNew = function ($event, grid) {
        $event.preventDefault();

        var data = $.extend([], true, grid.dataSource.data());
        if ($scope.NoContainer > 0) {
            var objNew = $.extend({}, true, $scope.ContainerNew);

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
            _ORDOrder_LCLIMEX.Data.ItemContainerBackUp = $.extend(true, {}, $scope.ContainerEdit);
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
            if (obj.ID == _ORDOrder_LCLIMEX.Data.ItemContainerBackUp.ID) {
                data.splice(i, 1);
                data.splice(i, 0, $.extend(true, {}, _ORDOrder_LCLIMEX.Data.ItemContainerBackUp))
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
                field: 'GroupOfProductID', width: 150, title: '{{RS.ORDGroupProduct.GroupOfProductID}}', sortable: false, template: '#=GroupOfProductName#',
                editor: '<input class="cus-combobox" focus-k-combobox kendo-combobox k-options="cbbGroupOfProductOptions" data-bind="value:GroupOfProductID" k-ng-model="ProductEdit.GroupOfProductID"/>',
                sortorder: 2, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'ProductID', width: 150, title: '{{RS.ORDProduct.ProductID}}', sortable: false,template:'#=ProductName#',
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
        _ORDOrder_LCLIMEX.Data.ItemProductNew.ID = data.length > 0 ? -(data.length + 1) : -1;
        data.splice(0, 0, $.extend(true, {}, _ORDOrder_LCLIMEX.Data.ItemProductNew));
        $scope.product_gridOptions.dataSource.data(data);

        $timeout(function () {
            $scope.IsProductEdit = true;
            var items = grid.items();
            $scope.ProductEdit = grid.dataItem(items[0]);
            _ORDOrder_LCLIMEX.Data.ItemProductBackUp = $.extend(true, {}, $scope.ProductEdit);
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
        _ORDOrder_LCLIMEX.Data.ItemProductBackUp = $.extend(true, {}, $scope.ProductEdit);
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
            if (obj.ID == _ORDOrder_LCLIMEX.Data.ItemProductBackUp.ID) {
                data.splice(i, 1);
                data.splice(i, 0, $.extend(true, {}, _ORDOrder_LCLIMEX.Data.ItemProductBackUp))
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
                $scope.ProductEdit.ProductName = _ORDOrder_LCLIMEX.Data.ListProduct[$scope.ProductEdit.GroupOfProductID][0].ProductName;
            } catch (e) {
            }
        }
    };

    $scope.LoadProduct = function () {
        Common.Log("LoadProduct");
        var gopID = $scope.ProductEdit.GroupOfProductID;
        var productID = $scope.ProductEdit.ProductID;
        var productName = $scope.ProductEdit.ProductName;
        var data = _ORDOrder_LCLIMEX.Data.ListProduct[gopID];
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
        Common.Data.Each(_ORDOrder_LCLIMEX.Data.ListStock, function (o) {
            var icon = openMapV2.NewStyle.Icon(Common.String.Format(openMapV2.NewImage.NewStock(openMapV2.NewImage.Color.Green)), 1);
            if (Common.HasValue(o.Lat) && Common.HasValue(o.Lng)) {
                openMapV2.NewMarker(o.Lat, o.Lng, o.CUSLocationID, o.LocationName, icon, o, "VectorMarkerStock");
            }
        })
        Common.Data.Each(_ORDOrder_LCLIMEX.Data.ListSeaPort, function (o) {
            var icon = openMapV2.NewStyle.Icon(Common.String.Format(openMapV2.NewImage.NewSeaport(openMapV2.NewImage.Color.Green)), 1);
            if (Common.HasValue(o.Lat) && Common.HasValue(o.Lng)) {
                openMapV2.NewMarker(o.Lat, o.Lng, o.CUSLocationID, o.LocationName, icon, o, "VectorMarkerSeaport");
            }
        })
        Common.Data.Each(_ORDOrder_LCLIMEX.Data.ListDepot, function (v) {
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
        method: _ORDOrder_LCLIMEX.URL.Contract,
        data: _ORDOrder_LCLIMEX.Param,
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
                    _ORDOrder_LCLIMEX.Param.ContractID = pars.ID;
                    if ($scope.Item.ID > 0) {
                        Common.Services.Call($http, {
                            url: Common.Services.url.ORD,
                            method: _ORDOrder_LCLIMEX.URL.Contract_Change,
                            data: _ORDOrder_LCLIMEX.Param,
                            success: function (res) {
                                Common.Services.Error(res, function (res) {
                                    $state.go("main.ORDOrder.FCLIMEX", _ORDOrder_LCLIMEX.Param);
                                })
                            }
                        });
                    } else {
                        $state.go("main.ORDOrder.FCLIMEX", _ORDOrder_LCLIMEX.Param);
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
                _ORDOrder_LCLIMEX.Param.ContractID = -1;
                if ($scope.Item.ID > 0) {
                    Common.Services.Call($http, {
                        url: Common.Services.url.ORD,
                        method: _ORDOrder_LCLIMEX.URL.Contract_Change,
                        data: _ORDOrder_LCLIMEX.Param,
                        success: function (res) {
                            Common.Services.Error(res, function (res) {
                                $state.go("main.ORDOrder.FCLIMEX", _ORDOrder_LCLIMEX.Param);
                            })
                        }
                    });
                } else {
                    $state.go("main.ORDOrder.FCLIMEX", _ORDOrder_LCLIMEX.Param);
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
            _ORDOrder_LCLIMEX.Data.ListContainer = res;
            $scope.cbo_CO_PackingOptions.dataSource.data(_ORDOrder_LCLIMEX.Data.ListContainer);
            $scope.cbo_coGrid_PackingOptions.dataSource.data(_ORDOrder_LCLIMEX.Data.ListContainer);
            var obj = _ORDOrder_LCLIMEX.Data.ListContainer[0];

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
                Msg: 'Không có thông tin chi tiết container.'
            })
            return false;
        }
        if ($scope.product_gridOptions.dataSource.data().length == 0) {
            $rootScope.Message({
                Type: Common.Message.Type.Alert,
                Msg: 'Không có thông tin chi tiết hàng hóa.'
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
                    $scope.Item.ListGroupProduct = $scope.product_gridOptions.dataSource.data();
                    Common.Services.Call($http, {
                        url: Common.Services.url.ORD,
                        method: _ORDOrder_LCLIMEX.URL.Update,
                        data: { item: $scope.Item },
                        success: function (res) {
                            $rootScope.IsLoading = false;
                            Common.Services.Error(res, function (res) {
                                $rootScope.Message({ Msg: "Đã cập nhật!" });
                                if (_ORDOrder_LCLIMEX.Param.OrderID != res) {
                                    _ORDOrder_LCLIMEX.Param.OrderID = res;
                                    if ($scope.Auth.ActAddAndApproved) {
                                        $rootScope.Message({
                                            Msg: "Bạn muốn gủi điều phối đơn hàng?",
                                            Type: Common.Message.Type.Confirm,
                                            Ok: function () {
                                                $rootScope.IsLoading = true;
                                                Common.Services.Call($http, {
                                                    url: Common.Services.url.ORD,
                                                    method: _ORDOrder_LCLIMEX.URL.Container_OPS_Check,
                                                    data: { data: [_ORDOrder_LCLIMEX.Param.OrderID] },
                                                    success: function (res) {
                                                        Common.Services.Error(res, function (res) {
                                                            $rootScope.IsLoading = false;
                                                            if (res.length > 0) {
                                                                $rootScope.Message({ Msg: "Thiếu dữ liệu container! Không thể gửi!" });
                                                                $state.go("main.ORDOrder.LCLIMEX", {
                                                                    OrderID: _ORDOrder_LCLIMEX.Param.OrderID,
                                                                    CustomerID: _ORDOrder_LCLIMEX.Param.CustomerID,
                                                                    ServiceID: _ORDOrder_LCLIMEX.Param.ServiceID,
                                                                    TransportID: _ORDOrder_LCLIMEX.Param.TransportID,
                                                                    ContractID: _ORDOrder_LCLIMEX.Param.ContractID != null ? _ORDOrder_LCLIMEX.Param.ContractID : -1,
                                                                    TermID: _ORDOrder_LCLIMEX.Param.TermID != null ? _ORDOrder_LCLIMEX.Param.TermID : -1
                                                                });
                                                            } else {
                                                                $rootScope.IsLoading = true;
                                                                Common.Services.Call($http, {
                                                                    url: Common.Services.url.ORD,
                                                                    method: _ORDOrder_LCLIMEX.URL.OPS_Check,
                                                                    data: { data: [_ORDOrder_LCLIMEX.Param.OrderID] },
                                                                    success: function (res) {
                                                                        Common.Services.Error(res, function (res) {
                                                                            $rootScope.IsLoading = false;
                                                                            if (res.length > 0) {
                                                                                $rootScope.Message({ Msg: "Thiếu dữ liệu cung đường! Không thể gửi!" });
                                                                                $state.go("main.ORDOrder.FCLIMEX", {
                                                                                    OrderID: _ORDOrder_LCLIMEX.Param.OrderID,
                                                                                    CustomerID: _ORDOrder_LCLIMEX.Param.CustomerID,
                                                                                    ServiceID: _ORDOrder_LCLIMEX.Param.ServiceID,
                                                                                    TransportID: _ORDOrder_LCLIMEX.Param.TransportID,
                                                                                    ContractID: _ORDOrder_LCLIMEX.Param.ContractID != null ? _ORDOrder_LCLIMEX.Param.ContractID : -1,
                                                                                    TermID: _ORDOrder_LCLIMEX.Param.TermID != null ? _ORDOrder_LCLIMEX.Param.TermID : -1
                                                                                });
                                                                            } else {
                                                                                Common.Services.Call($http, {
                                                                                    url: Common.Services.url.ORD,
                                                                                    method: _ORDOrder_LCLIMEX.URL.ToOPS,
                                                                                    data: { lst: [_ORDOrder_LCLIMEX.Param.OrderID] },
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
                                                    CustomerID: _ORDOrder_LCLIMEX.Param.CustomerID,
                                                    ServiceID: _ORDOrder_LCLIMEX.Param.ServiceID,
                                                    TransportID: _ORDOrder_LCLIMEX.Param.TransportID,
                                                    ContractID: _ORDOrder_LCLIMEX.Param.ContractID != null ? _ORDOrder_LCLIMEX.Param.ContractID : -1,
                                                    TermID: _ORDOrder_LCLIMEX.Param.TermID != null ? _ORDOrder_LCLIMEX.Param.TermID : -1
                                                });
                                            }
                                        })
                                    } else {
                                        $rootScope.Message({ Msg: "Thành công!" });
                                        $state.go("main.ORDOrder.FCLIMEX", {
                                            OrderID: res,
                                            CustomerID: _ORDOrder_LCLIMEX.Param.CustomerID,
                                            ServiceID: _ORDOrder_LCLIMEX.Param.ServiceID,
                                            TransportID: _ORDOrder_LCLIMEX.Param.TransportID,
                                            ContractID: _ORDOrder_LCLIMEX.Param.ContractID != null ? _ORDOrder_LCLIMEX.Param.ContractID : -1,
                                            TermID: _ORDOrder_LCLIMEX.Param.TermID != null ? _ORDOrder_LCLIMEX.Param.TermID : -1
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
                    method: _ORDOrder_LCLIMEX.URL.Delete,
                    data: { id: _ORDOrder_LCLIMEX.Param.OrderID },
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
        if (_ORDOrder_LCLIMEX.Param.OrderID > 0) {
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