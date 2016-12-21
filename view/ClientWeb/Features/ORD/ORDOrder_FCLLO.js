/// <reference path="~/Scripts/jquery-1.9.1.intellisense.js" />
/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />
/// <reference path="~/Scripts/map.js" />

//#region URL

var _ORDOrder_FCLLO = {
    URL: {
        GetItem: 'ORDOrder_GetItem',
        Location: 'ORDOrder_CusStock_List',
        GroupProduct: 'ORDOrder_GroupOfProduct_List',
        PriceContainer: 'ORDOrder_PriceContainer',
        TonCODefaul: 'ORDOrder_CODefault_List',
        COService: 'ORDOrder_ContainerService_List',

        Delete: 'ORDOrder_Delete',
        Update: 'ORDOrder_FCLLO_Save',
        
        Contract: 'ORDOrder_Contract_List',
        Contract_Change: 'ORDOrder_Contract_Change',

        OPS: 'ORDOrder_ToOPS',
        OPS_Check: 'ORDOrder_ToOPSCheck',
        Container_OPS_Check: 'ORDOrderContainer_ToOPSCheck',
        GetDate: 'ORDOrder_GetDate'
    },
    Data: {
        ItemProductNew: {
            ID: "", GroupOfProductID: "", GroupOfProductName: '', Description: ''
        },
        ItemProductBackUp: null,
        ItemContainerBackUp: null,
        ListGroupProduct: null,
        ListLocation: null,
        ListContainer: null,
        ListService: null
    },
    Map: {
        Map: null,
        Info: null,
        Line: [],
        Marker: [],
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

angular.module('myapp').controller('ORDOrder_FCLLOCtrl', ['$rootScope', '$scope', '$http', '$location', '$state', '$timeout' , 'openMapV2', function ($rootScope, $scope, $http, $location, $state, $timeout, openMapV2) {
    if ($rootScope.IsPageComplete != true) return;
    Common.Log('ORDOrder_FCLLOCtrl');
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
    _ORDOrder_FCLLO.Param = $.extend(true, _ORDOrder_FCLLO.Param, $state.params);
    if (_ORDOrder_FCLLO.Param.OrderID == "" || !angular.isNumber(parseInt(_ORDOrder_FCLLO.Param.OrderID))
        || _ORDOrder_FCLLO.Param.CustomerID == "" || !angular.isNumber(parseInt(_ORDOrder_FCLLO.Param.CustomerID))
        || _ORDOrder_FCLLO.Param.ServiceID == "" || !angular.isNumber(parseInt(_ORDOrder_FCLLO.Param.ServiceID))
        || _ORDOrder_FCLLO.Param.TransportID == "" || !angular.isNumber(parseInt(_ORDOrder_FCLLO.Param.TransportID))
        || _ORDOrder_FCLLO.Param.ContractID == "" || !angular.isNumber(parseInt(_ORDOrder_FCLLO.Param.ContractID))
        || _ORDOrder_FCLLO.Param.TermID == "" || !angular.isNumber(parseInt(_ORDOrder_FCLLO.Param.TermID))) {
        $rootScope.Message({ Msg: 'Đường dẫn ko đúng! Quay về trang đơn hàng!' });
        $state.go("main.ORDOrder.Index");
    }
    $scope.Item = null;
    $scope.ProductEdit = null;
    $scope.ContainerEdit = null;
    $scope.NoContainer = 1;
    $scope.ContainerNew = {
        ID: "", LocationFromID: "", LocationToID: "", PackingID: "",
        LocationFromName: "", LocationToName: "", PackingName: "",LoadingTime:2,UnLoadingTime:2,
        ETD: new Date(), ETA: new Date().addDays(1), DateGetEmpty: null, DateReturnEmpty: null,
        ContainerNo: "", SealNo1: "", SealNo2: "", Note: "", Ton: 0,Note1:'',Note2:''
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
        method: _ORDOrder_FCLLO.URL.Location,
        data: { CusID: _ORDOrder_FCLLO.Param.CustomerID },
        success: function (res) {
            $rootScope.Loading.Change("Lấy dữ liệu đơn hàng...", 50);
            if (Common.HasValue(res)) {
                _ORDOrder_FCLLO.Data.ListLocation = res.Data;

                Common.Services.Call($http, {
                    url: Common.Services.url.ORD,
                    method: _ORDOrder_FCLLO.URL.GetItem,
                    data: { ID: _ORDOrder_FCLLO.Param.OrderID, CustomerID: _ORDOrder_FCLLO.Param.CustomerID, ServiceOfOrderID: _ORDOrder_FCLLO.Param.ServiceID, TransportModeID: _ORDOrder_FCLLO.Param.TransportID, ContractID: _ORDOrder_FCLLO.Param.ContractID, TermID: _ORDOrder_FCLLO.Param.TermID },
                    success: function (res) {
                        $rootScope.Loading.Change("Lấy dữ liệu đơn hàng...", 80);
                        $scope.Item = res;
                        $scope.Item.ETA = Common.Date.FromJson(res.ETA);
                        $scope.Item.ETD = Common.Date.FromJson(res.ETD);
                        $scope.Item.ETARequest = Common.Date.FromJson(res.ETARequest);
                        $scope.Item.ETDRequest = Common.Date.FromJson(res.ETDRequest);
                        $scope.Item.RequestDate = Common.Date.FromJson(res.RequestDate);

                        $scope.ContainerNew.ETA = Common.Date.FromJson(res.ETA);
                        $scope.ContainerNew.ETD = Common.Date.FromJson(res.ETD);
                        
                        //Binding
                        if ($scope.Item.ID < 1) {
                            $scope.Item.LocationFromID = -1;
                            $scope.Item.LocationFromName = "";
                            $scope.Item.LocationToID = -1;
                            $scope.Item.LocationToName = "";
                            $scope.ContainerNew.LocationFromID = -1;
                            $scope.ContainerNew.LocationFromName = "";
                            $scope.ContainerNew.LocationToID =-1;
                            $scope.ContainerNew.LocationToName = -1;

                            var obj = _ORDOrder_FCLLO.Data.ListLocation[0];
                            if (Common.HasValue(obj)) {
                                $scope.Item.LocationFromID = obj.CUSLocationID;
                                $scope.Item.LocationFromName = obj.LocationName;
                                $scope.ContainerNew.LocationFromID = obj.CUSLocationID;
                                $scope.ContainerNew.LocationFromName = obj.LocationName;

                                obj = _ORDOrder_FCLLO.Data.ListLocation[1];
                                if (Common.HasValue(obj)) {
                                    $scope.Item.LocationToID = obj.CUSLocationID;
                                    $scope.Item.LocationToName = obj.LocationName;

                                    $scope.ContainerNew.LocationToID = obj.CUSLocationID;
                                    $scope.ContainerNew.LocationToName = obj.LocationName;
                                }
                            }
                            Common.Log("2" + $scope.Item.LocationFromID)
                        } else {                            
                            $timeout(function () {
                                $scope.service_gridOptions.dataSource.data(_ORDOrder_FCLLO.Data.ListService);
                                $scope.product_gridOptions.dataSource.data($scope.Item.ListGroupProduct);
                                $scope.price_gridOptions.dataSource.data($scope.Item.ListContainerPrice);
                                $scope.container_gridOptions.dataSource.data($scope.Item.ListContainer);
                            }, 1);
                        }
                        var tmp1 = $scope.Item.LocationFromID;
                        var tmp2 = $scope.Item.LocationToID;
                        if (_ORDOrder_FCLLO.Data.ListLocation.length == 0)
                            $scope.fcllo_cboLocationFromOptions.dataSource.data([{ CUSLocationID: -1, LocationName: "" }]);
                        else
                            $scope.fcllo_cboLocationFromOptions.dataSource.data(_ORDOrder_FCLLO.Data.ListLocation);
                        if (_ORDOrder_FCLLO.Data.ListLocation.length == 0)
                            $scope.fcllo_cboLocationToOptions.dataSource.data([{ CUSLocationID: -1, LocationName: "" }]);
                        else
                            $scope.fcllo_cboLocationToOptions.dataSource.data(_ORDOrder_FCLLO.Data.ListLocation);

                        $scope.Item.LocationFromID = tmp1;
                        $scope.Item.LocationToID = tmp2;

                        if (_ORDOrder_FCLLO.Data.ListLocation.length == 0)
                            $scope.cbo_CO_LocationFromOptions.dataSource.data([{ CUSLocationID: -1, LocationName: "" }]);
                        else
                            $scope.cbo_CO_LocationFromOptions.dataSource.data(_ORDOrder_FCLLO.Data.ListLocation);
                        if (_ORDOrder_FCLLO.Data.ListLocation.length == 0)
                            $scope.cbo_CO_LocationToOptions.dataSource.data([{ CUSLocationID: -1, LocationName: "" }]);
                        else
                            $scope.cbo_CO_LocationToOptions.dataSource.data(_ORDOrder_FCLLO.Data.ListLocation);

                        $scope.ContainerNew.LocationFromID = tmp1;
                        $scope.ContainerNew.LocationToID = tmp2;

                        $timeout(function () {
                            $scope.ReloadMap();
                            
                        }, 1);
                        $rootScope.Loading.Hide()
                    }
                });

                    //CreateMap
                    $timeout(function () {
                        $scope.hor_splitter.resize();
                        $scope.CreateMap();
                    }, 1)
            }
        }
    });

    Common.Services.Call($http, {
        url: Common.Services.url.ORD,
        method: _ORDOrder_FCLLO.URL.GroupProduct,
        data: { CusID: _ORDOrder_FCLLO.Param.CustomerID },
        success: function (res) {
            _ORDOrder_FCLLO.Data.ListGroupProduct = res.Data;
            _ORDOrder_FCLLO.Data.ListGroupProduct.splice(0, 0, { ID: "", GroupName: "" });
            $timeout(function () {
                $scope.cbbGroupOfProductOptions.dataSource.data(_ORDOrder_FCLLO.Data.ListGroupProduct);
            }, 1)
        }
    });
    
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
                    data: { item:$scope.Item },
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
                        var data = $scope.container_gridOptions.dataSource.data();
                        Common.Data.Each(data, function (o) {
                            o.ETA = res.ETA;
                            o.DateReturnEmpty = res.ETA.addDays(1 / 24);
                            o.ETD = res.ETD;
                            o.DateGetEmpty = res.ETD.addDays(-1 / 24);
                        })
                        $scope.container_gridOptions.dataSource.sync();
                        
                        $rootScope.IsLoading = false;
                    },
                    error: function (res) {
                        $rootScope.IsLoading = false;
                    }
                });
            }

            //var data = $scope.container_gridOptions.dataSource.data();
            //Common.Data.Each(data, function (o) {
            //    o.ETD = date;
            //    o.DateGetEmpty = date.addDays(-1 / 24);
            //})
            //$scope.container_gridOptions.dataSource.sync();
        }
    }
    $scope.fcllo_dtpETDOptions = {
        format: 'dd/MM/yyyy HH:mm',
        timeFormat: "HH:mm",
        parseFormats: ["yyyy-MM-ddTHH:mm:ss"],
        change: function (e) {
            var date = this.value();
            var data = $scope.container_gridOptions.dataSource.data();
            $scope.ContainerNew.ETD = date;
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
                        $scope.ContainerNew.ETA = res.ETA;
                        $scope.Item.ETARequest = res.ETARequest;
                        $scope.Item.ETDRequest = res.ETDRequest;
                        Common.Data.Each(data, function (o) {
                            o.ETA = res.ETA;
                            o.DateReturnEmpty = res.ETA.addDays(1 / 24);
                            o.ETD = date;
                            o.DateGetEmpty = date.addDays(-1 / 24);
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
                    o.DateGetEmpty = date.addDays(-1 / 24);
                })
                $scope.container_gridOptions.dataSource.sync();
            }
        }
    }

    $scope.fcllo_dtpETAOptions = {
        format: 'dd/MM/yyyy HH:mm',
        timeFormat: "HH:mm",
        parseFormats: ["yyyy-MM-ddTHH:mm:ss"],
        change: function (e) {
            var date = this.value();
            var data = $scope.container_gridOptions.dataSource.data();
            $scope.ContainerNew.ETA = date;
            Common.Data.Each(data, function (o) {
                o.ETA = date;
                o.DateReturnEmpty = date.addDays(1 / 24);
            })
            $scope.container_gridOptions.dataSource.sync();
        }
    }

    $scope.fcllo_cboLocationFromOptions = {
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
                if (obj.CUSLocationID == $scope.Item.LocationToID) {
                    Common.Log("cbo location from change")
                    var data = $.grep(_ORDOrder_FCLLO.Data.ListLocation, function (o) { return o.CUSLocationID != obj.CUSLocationID });
                    if (data.length == 0) $scope.Item.LocationToID = -1;
                    else $scope.Item.LocationToID = data[0].CUSLocationID;
                    $timeout(function () {
                        $scope.fcllo_cboLocationTo.trigger("change")
                    },100)
                }
                    $scope.Item.LocationFromName = obj.LocationName;

                    $scope.ContainerNew.LocationFromID = obj.CUSLocationID;
                    $scope.ContainerNew.LocationFromName = obj.LocationName;

                    $timeout(function () {
                        $scope.ReloadMap();
                        Common.Data.Each($scope.container_grid.dataSource.data(), function (o) {
                            o.LocationFromID = $scope.Item.LocationFromID;
                            o.LocationFromName = $scope.Item.LocationFromName;
                            o.LocationToID = $scope.Item.LocationToID;
                            o.LocationToName = $scope.Item.LocationToName;
                        })
                        $scope.container_grid.dataSource.sync();
                    }, 100)
            }
        }
    }

    $scope.fcllo_cboLocationToOptions = {
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
                if (obj.CUSLocationID == $scope.Item.LocationFromID) {
                    Common.Log("cbo location to change")
                    var data = $.grep(_ORDOrder_FCLLO.Data.ListLocation, function (o) { return o.CUSLocationID != obj.CUSLocationID });
                    if (data.length == 0) $scope.Item.LocationFromID = -1;
                    else $scope.Item.LocationFromID = data[0].CUSLocationID;
                    $timeout(function () {
                        $scope.fcllo_cboLocationFrom.trigger("change")
                    }, 100)
                } 
                    $scope.Item.LocationToName = obj.LocationName;

                    $scope.ContainerNew.LocationToID = obj.CUSLocationID;
                    $scope.ContainerNew.LocationToName = obj.LocationName;

                    $timeout(function () {
                        $scope.ReloadMap();

                        Common.Data.Each($scope.container_grid.dataSource.data(), function (o) {
                            o.LocationFromID = $scope.Item.LocationFromID;
                            o.LocationFromName = $scope.Item.LocationFromName;
                            o.LocationToID = $scope.Item.LocationToID;
                            o.LocationToName = $scope.Item.LocationToName;
                        })
                        $scope.container_grid.dataSource.sync();
                    }, 100)
            }
        }
    }

    $scope.fcllo_dtpETARequestOptions = {
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

    $scope.fcllo_dtpETDRequestOptions = {
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
                    Choose: { type: 'string', editable: false },
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
                field: 'Choose', title: ' ', width: '85px',
                template: '<a ng-show="!IsContainerEdit" href="/" ng-click="ORD_FCLLO_Container_Edit($event,container_grid)" class="k-button"><i class="fa fa-pencil"></i></a>' +
                    '<a ng-show="!IsContainerEdit" href="/" ng-click="ORD_FCLLO_Container_Delete($event,container_grid)" class="k-button"><i class="fa fa-trash"></i></a>' +
                    '<a ng-show="IsContainerEdit && ContainerEdit.ID==#=ID#?true:false" href="/" ng-click="ORD_FCLLO_Container_Save($event,container_grid)" class="k-button"><i class="fa fa-check"></i></a>' +
                    '<a ng-show="IsContainerEdit && ContainerEdit.ID==#=ID#?true:false" href="/" ng-click="ORD_FCLLO_Container_Cancel($event,container_grid)" class="k-button"><i class="fa fa-ban"></i></a>',
                filterable: false, sortable: false
            },
            {
                field: 'PackingID', width: 120, title: '{{RS.ORDContainer.PackingID}}', template: '#=PackingName#', sortable: true,
                editor: '<input class="cus-combobox" focus-k-combobox kendo-combobox k-options="cbo_coGrid_PackingOptions" data-bind="value:PackingID" ng-model="ContainerEdit.PackingID"/>'
            },
            {
                field: 'LocationFromID', width: 170, title: '{{RS.ORDContainer.LocationFromID}}', template: '#=LocationFromName#', sortable: true,
                editor: '<input class="cus-combobox" focus-k-combobox kendo-combobox k-options="cbo_coGrid_LocationFromOptions" data-bind="value:LocationFromID" ng-model="ContainerEdit.LocationFromID" k-data-source="ContainerEdit.ListLocationFrom"/>'
            },
            {
                field: 'LocationToID', width: 170, title: '{{RS.ORDContainer.LocationToID}}', template: '#=LocationToName#', sortable: true,
                editor: '<input class="cus-combobox" focus-k-combobox kendo-combobox k-options="cbo_coGrid_LocationToOptions" data-bind="value:LocationToID" ng-model="ContainerEdit.LocationToID" k-data-source="ContainerEdit.ListLocationTo"/>'
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
                field: 'Ton', width: 100, title: '{{RS.ORDContainer.Ton}}', editor: '<input ng-model="ContainerEdit.Ton" class="cus-number" kendo-numeric-text-box k-options="Number" min="0" step="0.01" style="width: 100%;" ></input>'
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

    $scope.Number = { format: 'n0', spinners: false, culture: 'en-US' }

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
        select: function (e) {
            var obj = this.dataItem(e.item.index());
            if (Common.HasValue(obj)) {
                if (obj.CUSLocationID == $scope.ContainerNew.LocationToID) {
                    $rootScope.Message({
                        Type: Common.Message.Type.Alert,
                        Msg: 'Trùng địa chỉ. Vui lòng chọn địa chỉ khác.'
                    })
                    e.preventDefault();
                }
                else {
                    var data = $.grep(_ORDOrder_FCLLO.Data.ListLocation, function (o) { return o.CUSLocationID != obj.CUSLocationID });
                    if (data.length == 0) data = [{ CUSLocationID: -1, LocationName: "" }]
                    $scope.cbo_CO_LocationToOptions.dataSource.data(data);
                }
            }
        },
        change: function (e) {
            var obj = this.dataItem(this.select());
            if (Common.HasValue(obj)) {
                $scope.ContainerNew.LocationFromName = obj.LocationName;
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
        select: function (e) {
            var obj = this.dataItem(e.item.index());
            if (Common.HasValue(obj)) {
                if (obj.CUSLocationID == $scope.ContainerNew.LocationToID) {
                    $rootScope.Message({
                        Type: Common.Message.Type.Alert,
                        Msg: 'Trùng địa chỉ. Vui lòng chọn địa chỉ khác.'
                    })
                    e.preventDefault();
                }
                else {
                    var data = $.grep(_ORDOrder_FCLLO.Data.ListLocation, function (o) { return o.CUSLocationID != obj.CUSLocationID });
                    if (data.length == 0) data = [{ CUSLocationID: -1, LocationName: "" }]
                    $scope.cbo_CO_LocationFromOptions.dataSource.data(data);
                }
            }
        },
        change: function (e) {
            var obj = this.dataItem(this.select());
            if (Common.HasValue(obj)) {
                $scope.ContainerNew.LocationToName = obj.LocationName;
            }
        }
    }
    
    $scope.ORD_FCLLO_Container_AddNew = function ($event, grid) {
        $event.preventDefault();

        var data = $.extend([], true, grid.dataSource.data());
        if ($scope.NoContainer > 0) {
            var objNew = $.extend({}, true, $scope.ContainerNew);
            var data0 = $.grep(_ORDOrder_FCLLO.Data.ListLocation, function (o) { return o.CUSLocationID != $scope.ContainerNew.LocationToID });
            if (data0.length == 0) data0 = [{ CUSLocationID: -1, LocationName: "" }]
            objNew.ListLocationFrom = $.extend(true, [], data0);

            var data1 = $.grep(_ORDOrder_FCLLO.Data.ListLocation, function (o) { return o.CUSLocationID != $scope.ContainerNew.LocationFromID });
            if (data1.length == 0) data1 = [{ CUSLocationID: -1, LocationName: "" }]
            objNew.ListLocationTo = $.extend(true, [], data1);
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
    
    $scope.ORD_FCLLO_Container_Edit = function ($event, grid) {
        $event.preventDefault();

        $timeout(function () { $scope.IsContainerEdit = true;
           
            var tr = $event.target.closest('tr');
            $scope.ContainerEdit = grid.dataItem(tr);
            _ORDOrder_FCLLO.Data.ItemContainerBackUp = $.extend(true, {}, $scope.ContainerEdit);

            var data0 = $.grep(_ORDOrder_FCLLO.Data.ListLocation, function (o) { return o.CUSLocationID != $scope.ContainerEdit.LocationToID });
            if (data0.length == 0) data0 = [{ CUSLocationID: -1, LocationName: "" }]
            $scope.ContainerEdit.ListLocationFrom = $.extend(true, [], data0);

            var data1 = $.grep(_ORDOrder_FCLLO.Data.ListLocation, function (o) { return o.CUSLocationID != $scope.ContainerEdit.LocationFromID });
            if (data1.length == 0) data1 = [{ CUSLocationID: -1, LocationName: "" }]
            $scope.ContainerEdit.ListLocationTo = $.extend(true, [], data1);

            grid.editRow(tr);
            var td = $(tr).find('input');
            td[0].focus();
        }, 1)
    }

    $scope.ORD_FCLLO_Container_Save = function ($event, grid) {
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

    $scope.ORD_FCLLO_Container_Cancel = function ($event, grid) {
        $event.preventDefault();

        $rootScope.IsLoading = true;
        $scope.IsContainerEdit = false;
        var i = 0;
        var data = grid.dataSource.data();
        while (i < data.length) {
            var obj = data[i];
            if (obj.ID == _ORDOrder_FCLLO.Data.ItemContainerBackUp.ID) {
                data.splice(i, 1);
                data.splice(i, 0, $.extend(true, {}, _ORDOrder_FCLLO.Data.ItemContainerBackUp))
            }
            i++;
        }
        $rootScope.IsLoading = false;
    }

    $scope.ORD_FCLLO_Container_Delete = function ($event, grid) {
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
        //dataSource: Common.DataSource.Local({
        //    data: [],
        //    model: {
        //        id: 'CUSLocationID',
        //        fields: {
        //            CUSLocationID: { type: 'number' },
        //            LocationName: { type: 'string' },
        //        }
        //    }
        //}),
        select: function (e) {
            var obj = this.dataItem(e.item.index());
            if (Common.HasValue(obj)) {
                if (obj.CUSLocationID == $scope.ContainerEdit.LocationToID) {
                    $rootScope.Message({
                        Type: Common.Message.Type.Alert,
                        Msg: 'Trùng địa chỉ. Vui lòng chọn địa chỉ khác.'
                    })
                    e.preventDefault();
                }
                else {
                    var data = $.grep(_ORDOrder_FCLLO.Data.ListLocation, function (o) { return o.CUSLocationID != obj.CUSLocationID });
                    if (data.length == 0) data = [{ CUSLocationID: -1, LocationName: "" }]
                    $scope.ContainerEdit.ListLocationTo = $.extend(true, [], data);
                }
            }
        },
        change: function (e) {
            var obj = this.dataItem(this.select());
            if (Common.HasValue(obj)) {
                $scope.ContainerEdit.LocationFromName = obj.LocationName;
            }
        }
    }

    $scope.cbo_coGrid_LocationToOptions = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true,
        dataTextField: 'LocationName', dataValueField: 'CUSLocationID', minLength: 3,
        //dataSource: Common.DataSource.Local({
        //    data: [],
        //    model: {
        //        id: 'CUSLocationID',
        //        fields: {
        //            CUSLocationID: { type: 'number' },
        //            LocationName: { type: 'string' },
        //        }
        //    }
        //}),
        select: function (e) {
            var obj = this.dataItem(e.item.index());
            var obj = this.dataItem(e.item.index());
            if (Common.HasValue(obj)) {
                if (obj.CUSLocationID == $scope.ContainerEdit.LocationFromID) {
                    $rootScope.Message({
                        Type: Common.Message.Type.Alert,
                        Msg: 'Trùng địa chỉ. Vui lòng chọn địa chỉ khác.'
                    })
                    e.preventDefault();
                }
                else {
                    var data = $.grep(_ORDOrder_FCLLO.Data.ListLocation, function (o) { return o.CUSLocationID != obj.CUSLocationID });
                    if (data.length == 0) data = [{ CUSLocationID: -1, LocationName: "" }]
                    $scope.ContainerEdit.ListLocationFrom = $.extend(true, [], data);
                }
            }
        },
        change: function (e) {
            var obj = this.dataItem(this.select());
            if (Common.HasValue(obj)) {
                $scope.ContainerEdit.LocationToName = obj.LocationName;
            }
        }
    }

    $scope.coNumLoadingTime_Options = { format: 'n2', spinners: false, culture: 'en-US', min: 0, step: 1, decimals: 2, }

    $scope.coNumUnLoadingTime_Options = { format: 'n2', spinners: false, culture: 'en-US', min: 0, step: 1, decimals: 2, }

    //Tab sản phẩm
    $scope.product_gridOptions = {
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    GroupOfProductName: { type: 'string' },
                    Description: { type: 'string', defaultValue: '' }
                }
            },
            pageSize: 0
        }),
        editable: { mode: 'inline' }, height: '100%', pageable: false, sortable: false, columnMenu: false, filterable: false, resizable: true,
        toolbar: kendo.template($('#product_grid_toolbar').html()),
        columns: [
            {
                title: ' ', width: '85px',
                template: '<a ng-show="!IsProductEdit" href="/" ng-click="ORD_FCLLO_Product_Edit($event,product_grid)" class="k-button"><i class="fa fa-pencil"></i></a>' +
                    '<a ng-show="!IsProductEdit" href="/" ng-click="ORD_FCLLO_Product_Delete($event,product_grid)" class="k-button"><i class="fa fa-trash"></i></a>' +
                    '<a ng-show="cbbGroupOfProductName.select()>=0 && IsProductEdit && ProductEdit.ID==#=ID#?true:false" href="/" ng-click="ORD_FCLLO_Product_Save($event,product_grid)" class="k-button"><i class="fa fa-check"></i></a>' +
                    '<a ng-show="IsProductEdit && ProductEdit.ID==#=ID#?true:false" href="/" ng-click="ORD_FCLLO_Product_Cancel($event,product_grid)" class="k-button"><i class="fa fa-ban"></i></a>',
                filterable: false, sortable: false
            },
            {
                field: 'GroupOfProductID', width: 150, title: '{{RS.ORDGroupProduct.GroupOfProductID}}', template: '#=GroupOfProductName#', sortable: true,
                editor: '<input class="cus-combobox" focus-k-combobox kendo-combobox="cbbGroupOfProductName" k-options="cbbGroupOfProductOptions" data-bind="value:GroupOfProductID" ng-model="ProductEdit.GroupOfProductID"/>'
            },
            {
                field: 'Description', title: '{{RS.ORDGroupProduct.Description}}',
                editor: '<input ng-model="ProductEdit.Description" class="k-textbox" type="text" style="width: 100%;" ></input>'
            }
        ]
    }

    $scope.IsProductEdit = false;
    
    $scope.ORD_FCLLO_Product_AddNew = function ($event, grid) {
        $event.preventDefault();

        var data = $scope.product_gridOptions.dataSource.data();
        _ORDOrder_FCLLO.Data.ItemProductNew.ID = data.length > 0 ? -(data.length + 1) : -1;
        data.splice(0, 0, $.extend(true, {}, _ORDOrder_FCLLO.Data.ItemProductNew));
        $scope.product_gridOptions.dataSource.data(data);

        $timeout(function () {
            $scope.IsProductEdit = true;
            var items = grid.items();
            $scope.ProductEdit = grid.dataItem(items[0]);
            _ORDOrder_FCLLO.Data.ItemProductBackUp = $.extend(true, {}, $scope.ProductEdit);
            grid.editRow(items[0]);
            var td = $(items[0]).find('input');
            td[0].focus();
        }, 1);
    }

    $scope.ORD_FCLLO_Product_Edit = function ($event, grid) {
        $event.preventDefault();

        $scope.IsProductEdit = true;
        var tr = $event.target.closest('tr');
        $scope.ProductEdit = grid.dataItem(tr);
        _ORDOrder_FCLLO.Data.ItemProductBackUp = $.extend(true, {}, $scope.ProductEdit);
        grid.editRow(tr);
        var td = $(tr).find('input');
        td[0].focus();
    }

    $scope.ORD_FCLLO_Product_Save = function ($event, grid) {
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

    $scope.ORD_FCLLO_Product_Cancel = function ($event, grid) {
        $event.preventDefault();

        $rootScope.IsLoading = true;
        $scope.IsProductEdit = false;
        var i = 0;
        var data = grid.dataSource.data();
        while (i < data.length) {
            var obj = data[i];
            if (obj.ID == _ORDOrder_FCLLO.Data.ItemProductBackUp.ID) {
                data.splice(i, 1);
                data.splice(i, 0, $.extend(true, {}, _ORDOrder_FCLLO.Data.ItemProductBackUp))
            }
            i++;
        }
        $rootScope.IsLoading = false;
    }

    $scope.ORD_FCLLO_Product_Delete = function ($event, grid) {
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
                $scope.ProductEdit.GroupOfProductID = obj.ID
            } else {
                $scope.ProductEdit.GroupOfProductName = null;
                $scope.ProductEdit.GroupOfProductID = null;
            }
        }
    };

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
                title: 'Chọn', width: '50px',
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
   

    Common.Services.Call($http, {
        url: Common.Services.url.ORD,
        method: _ORDOrder_FCLLO.URL.COService,
        data: { ContractID: _ORDOrder_FCLLO.Param.ContractID },
        success: function (res) {
            _ORDOrder_FCLLO.Data.ListService = res.Data;
            $.each(_ORDOrder_FCLLO.Data.ListService, function (i, v) {
                v.IsChoose = false;
                v.ServiceID = v.ID;
            })
            $timeout(function () {
                $scope.service_gridOptions.dataSource.data(_ORDOrder_FCLLO.Data.ListService);
            }, 1);
        }
    })
    
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
                    Price: { type: 'number', editable: true, format: '{n0}', validation: { required: true, min: 0 } },
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
            { field: 'Price', width: 200, title: '{{RS.ORDContainerPrice.Price}}' },
            { field: 'Quantity', width: 200, title: '{{RS.ORDOrder_FCLLO.Quantity}}' },
            { field: 'TotalPrice', title: '{{RS.ORDOrder_FCLLO.TotalPrice}}' }
        ],
        save: function (e) {
            e.preventDefault();
            e.model.Price = Math.round(StringToNumber(e.values.Price));
            e.model.TotalPrice = e.model.Price * e.model.Quantity;
            this.dataSource.sync();
        }
    }
    
    $scope.Number = { format: 'n6', spinners: false, culture: 'en-US', min: 0, step: 0.00001, decimals: 6 }

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
                    method: _ORDOrder_FCLLO.URL.PriceContainer,
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
        Common.Log("openMapV2 has map: " + openMapV2.hasMap);
        openMapV2.Init({
            Element: 'fcllo_map',
            Tooltip_Show: true,
            Tooltip_Element: 'fcllo_tooltip',
            DefinedLayer: [{
                Name: 'VectorMarker',
                zIndex: 100
            }, {
                Name: 'VectorRoute',
                zIndex: 90
            }]
        });
        openMapV2.ClearVector("VectorMarker");
        var icon = openMapV2.NewStyle.Icon(Common.String.Format(openMapV2.NewImage.NewLocation(openMapV2.NewImage.Color.Green)), 1);
        Common.Data.Each(_ORDOrder_FCLLO.Data.ListLocation, function (o) {
            if (Common.HasValue(o.Lat) && Common.HasValue(o.Lng)) {
                openMapV2.NewMarker(o.Lat, o.Lng, o.CUSLocationID, o.LocationName, icon, o, "VectorMarker");
            }
        })
        openMapV2.Visible("VectorMarker", false);
    }

    $scope.ReloadMap = function () {
        Common.Log("ReloadMap");

        openMapV2.Close();
        openMapV2.ClearVector("VectorRoute");
        openMapV2.Visible("VectorMarker", false);
        var f1 = openMapV2.GetFeature($scope.Item.LocationFromID, "VectorMarker");
        var f2 = openMapV2.GetFeature($scope.Item.LocationToID, "VectorMarker");
        if (Common.HasValue(f1) && Common.HasValue(f2)) {
            openMapV2.Visible([f1, f2], true);
            openMapV2.NewRoute(f1, f2, "", "", openMapV2.NewStyle.Line(6, 'rgba(3, 169, 244, 0.6)'), null, "VectorRoute", null, function () {
                openMapV2.FitBound("VectorRoute", 15);
            });
        }
    }

    $scope.fcllo_cboTypeOfOrderOptions = {
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

    $scope.fcllo_dtpExternalDateOptions = {
        format: Common.Date.Format.DMYHM,
        parseFormats: Common.Date.ParseFormat
    }

    Common.ALL.Get($http, {
        url: Common.ALL.URL.SYSVarTypeOfOrder,
        success: function (res) {
            $scope.fcllo_cboTypeOfOrderOptions.dataSource.data(res);
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
                if (Common.HasValue(obj) && Common.HasValue($scope.Item) &&obj.ID == $scope.Item.ContractID) {
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
        method: _ORDOrder_FCLLO.URL.Contract,
        data: _ORDOrder_FCLLO.Param,
        success: function (res) {
            $scope.contract_gridOptions.dataSource.data(res.Data);
        }
    });
    
    $scope.ORD_FCLLO_ChangeContract = function ($event, win) {
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
                    _ORDOrder_FCLLO.Param.ContractID = pars.ID;
                    if ($scope.Item.ID > 0) {
                        Common.Services.Call($http, {
                            url: Common.Services.url.ORD,
                            method: _ORDOrder_FCLLO.URL.Contract_Change,
                            data: _ORDOrder_FCLLO.Param,
                            success: function (res) {
                                Common.Services.Error(res, function (res) {
                                    $state.go("main.ORDOrder.FCLLO", _ORDOrder_FCLLO.Param);
                                })
                            }
                        });
                    } else {
                        $state.go("main.ORDOrder.FCLLO", _ORDOrder_FCLLO.Param);
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
                _ORDOrder_FCLLO.Param.ContractID = -1;
                if ($scope.Item.ID > 0) {
                    Common.Services.Call($http, {
                        url: Common.Services.url.ORD,
                        method: _ORDOrder_FCLLO.URL.Contract_Change,
                        data: _ORDOrder_FCLLO.Param,
                        success: function (res) {
                            Common.Services.Error(res, function (res) {
                                $state.go("main.ORDOrder.FCLLO", _ORDOrder_FCLLO.Param);
                            })
                        }
                    });
                } else {
                    $state.go("main.ORDOrder.FCLLO", _ORDOrder_FCLLO.Param);
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
            _ORDOrder_FCLLO.Data.ListContainer = res;
            $scope.cbo_CO_PackingOptions.dataSource.data(_ORDOrder_FCLLO.Data.ListContainer);
            $scope.cbo_coGrid_PackingOptions.dataSource.data(_ORDOrder_FCLLO.Data.ListContainer);
            var obj = _ORDOrder_FCLLO.Data.ListContainer[0];

            if (Common.HasValue(obj)) {
                $scope.ContainerNew.PackingID = obj.ID;
                $scope.ContainerNew.PackingName = obj.PackingName;
            }
        }
    });

    //#endregion
    
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
        if ($scope.fcllo_cboLocationTo.text().length == 0 || $scope.fcllo_cboLocationFrom.text().length == 0) {
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

    $scope.ORD_FCLLO_Update = function ($event) {
        $event.preventDefault();
        if ($scope.CheckItem()) {
            $rootScope.Message({
                Type: Common.Message.Type.Confirm,
                Msg: 'Bạn muốn lưu đơn hàng?',
                Ok: function () {
                    $rootScope.IsLoading = true;
                    $scope.Item.ListContainer = $scope.container_gridOptions.dataSource.data();
                    $scope.Item.ListGroupProduct = $scope.product_gridOptions.dataSource.data();
                    $scope.Item.ListContainerPrice = $scope.price_gridOptions.dataSource.data();
                    $scope.Item.ListService = $.grep($scope.service_gridOptions.dataSource.data(), function (o) { return o.IsChoose == true; });
                    Common.Services.Call($http, {
                        url: Common.Services.url.ORD,
                        method: _ORDOrder_FCLLO.URL.Update,
                        data: { item: $scope.Item },
                        success: function (res) {
                            $rootScope.IsLoading = false;                            
                            Common.Services.Error(res, function (res) {                                
                                $rootScope.Message({ Msg: "Đã cập nhật!" });
                                if (_ORDOrder_FCLLO.Param.OrderID != res) {
                                    _ORDOrder_FCLLO.Param.OrderID = res;
                                    if ($scope.Auth.ActAddAndApproved) {
                                        $rootScope.Message({
                                            Msg: "Bạn muốn gủi điều phối đơn hàng?",
                                            Type: Common.Message.Type.Confirm,
                                            Ok: function () {
                                                $rootScope.IsLoading = true;
                                                Common.Services.Call($http, {
                                                    url: Common.Services.url.ORD,
                                                    method: _ORDOrder_FCLLO.URL.Container_OPS_Check,
                                                    data: { data: [_ORDOrder_FCLLO.Param.OrderID] },
                                                    success: function (res) {
                                                        Common.Services.Error(res, function (res) {
                                                            $rootScope.IsLoading = false;
                                                            if (res.length > 0) {
                                                                $rootScope.Message({ Msg: "Thiếu dữ liệu container! Không thể gửi!" });
                                                                $state.go("main.ORDOrder.FCLLO", {
                                                                    OrderID: _ORDOrder_FCLLO.Param.OrderID,
                                                                    CustomerID: _ORDOrder_FCLLO.Param.CustomerID,
                                                                    ServiceID: _ORDOrder_FCLLO.Param.ServiceID,
                                                                    TransportID: _ORDOrder_FCLLO.Param.TransportID,
                                                                    ContractID: _ORDOrder_FCLLO.Param.ContractID != null ? _ORDOrder_FCLLO.Param.ContractID : -1,
                                                                    TermID: _ORDOrder_FCLLO.Param.TermID != null ? _ORDOrder_FCLLO.Param.TermID : -1
                                                                });
                                                            } else {
                                                                $rootScope.IsLoading = true;
                                                                Common.Services.Call($http, {
                                                                    url: Common.Services.url.ORD,
                                                                    method: _ORDOrder_FCLLO.URL.OPS_Check,
                                                                    data: { data: [_ORDOrder_FCLLO.Param.OrderID] },
                                                                    success: function (res) {
                                                                        Common.Services.Error(res, function (res) {
                                                                            $rootScope.IsLoading = false;
                                                                            if (res.length > 0) {
                                                                                $rootScope.Message({ Msg: "Thiếu dữ liệu cung đường! Không thể gửi!" });
                                                                                $state.go("main.ORDOrder.FCLLO", {
                                                                                    OrderID: _ORDOrder_FCLLO.Param.OrderID,
                                                                                    CustomerID: _ORDOrder_FCLLO.Param.CustomerID,
                                                                                    ServiceID: _ORDOrder_FCLLO.Param.ServiceID,
                                                                                    TransportID: _ORDOrder_FCLLO.Param.TransportID,
                                                                                    ContractID: _ORDOrder_FCLLO.Param.ContractID != null ? _ORDOrder_FCLLO.Param.ContractID : -1,
                                                                                    TermID: _ORDOrder_FCLLO.Param.TermID != null ? _ORDOrder_FCLLO.Param.TermID : -1
                                                                                });
                                                                            } else {
                                                                                Common.Services.Call($http, {
                                                                                    url: Common.Services.url.ORD,
                                                                                    method: _ORDOrder_FCLLO.URL.ToOPS,
                                                                                    data: { lst: [_ORDOrder_FCLLO.Param.OrderID] },
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
                                                $state.go("main.ORDOrder.FCLLO", {
                                                    OrderID: res,
                                                    CustomerID: _ORDOrder_FCLLO.Param.CustomerID,
                                                    ServiceID: _ORDOrder_FCLLO.Param.ServiceID,
                                                    TransportID: _ORDOrder_FCLLO.Param.TransportID,
                                                    ContractID: _ORDOrder_FCLLO.Param.ContractID != null ? _ORDOrder_FCLLO.Param.ContractID : -1,
                                                    TermID: _ORDOrder_FCLLO.Param.TermID != null ? _ORDOrder_FCLLO.Param.TermID : -1
                                                });
                                            }
                                        })
                                    } else {
                                        $rootScope.Message({ Msg: "Thành công!" });
                                        $state.go("main.ORDOrder.FCLLO", {
                                            OrderID: res,
                                            CustomerID: _ORDOrder_FCLLO.Param.CustomerID,
                                            ServiceID: _ORDOrder_FCLLO.Param.ServiceID,
                                            TransportID: _ORDOrder_FCLLO.Param.TransportID,
                                            ContractID: _ORDOrder_FCLLO.Param.ContractID != null ? _ORDOrder_FCLLO.Param.ContractID : -1,
                                            TermID: _ORDOrder_FCLLO.Param.TermID != null ? _ORDOrder_FCLLO.Param.TermID : -1
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

    $scope.ORD_FCLLO_Delete = function ($event) {
        $event.preventDefault();
        $rootScope.Message({
            Type: Common.Message.Type.Confirm,
            Msg: 'Bạn muốn xóa đơn hàng?',
            Ok: function () {
                Common.Services.Call($http, {
                    url: Common.Services.url.ORD,
                    method: _ORDOrder_FCLLO.URL.Delete,
                    data: { id: _ORDOrder_FCLLO.Param.OrderID },
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

    $scope.ORD_FCLLO_Back = function ($event) {
        $event.preventDefault();
        if (_ORDOrder_FCLLO.Param.OrderID > 0) {
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