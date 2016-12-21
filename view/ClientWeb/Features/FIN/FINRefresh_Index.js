/// <reference path="~/Scripts/jquery-1.9.1.intellisense.js" />
/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />

//#region Data
var _FINRefresh_Index = {
    URL: {
        List: 'FINRefresh_List',
        Refresh: 'FINRefresh_Refresh',
        Refresh_Order: 'FINRefresh_RefreshRoute_Order',
        Refresh_TO: 'FINRefresh_RefreshRoute_TO',

        ORD_GroupList: 'FINRefresh_ORD_Group_List',
        ORD_GroupSave: 'FINRefresh_ORD_Group_Save',

        OPS_GroupList: 'FINRefresh_OPS_Group_List',
        OPS_GroupSave: 'FINRefresh_OPS_Group_Save',
                                                                                      
        OPS_MasterList: 'FINRefresh_OPS_Master_List',
        OPS_MasterSave: 'FINRefresh_OPS_Master_Save',

        ORD_RoutingList: 'FINRefresh_Routing_List',                             
        ORD_ContractList: 'FINRefresh_Contract_List',

        OPS_RoutingMasterList: 'FINRefresh_Routing_Master_List',           
        OPS_ContractMasterList: 'FINRefresh_Contract_Master_List',
                                                                          
        ORD_OPSRoutingList: 'FINRefresh_OPSRouting_List',
        OPS_OPSGroupRoutingList: 'FINRefresh_OPSGroupRouting_List',
        ORD_ContainerList: 'FINRefresh_ORD_Container_List',
        ORD_ContainerSave: 'FINRefresh_ORD_Container_Save',
        ORD_ContractTerm_List: 'FINRefresh_ContractTerm_List',

        OPS_GroupContainerList: 'FINRefresh_OPS_Container_List',
        OPS_GroupContainerSave: 'FINRefresh_OPS_Container_Save',

        CUS_List: 'Customer_List',
    },
    Data: {
        RefreshData: [],
        RefreshIndex: -1
    }
};
//#endregion

angular.module('myapp').controller('FINRefresh_IndexCtrl', ['$rootScope', '$scope', '$http', '$location', '$state', '$timeout', '$stateParams', function ($rootScope, $scope, $http, $location, $state, $timeout, $stateParams) {
    if ($rootScope.IsPageComplete != true) return;
    Common.Log('FINRefresh_IndexCtrl');
    $rootScope.IsLoading = false;
    $scope.CusRefreshHasChoose = false;
    $scope.lstCUSID = [];

    $scope.Auth = $rootScope.GetAuth();

    $scope.Item = {
        DateFrom: new Date().addDays(-3),
        DateTo: new Date().addDays(3)
    };
    $scope.RefreshCol = {};
    $scope.RefreshCol.col1 = [];
    $scope.RefreshCol.col2 = [];
    $scope.RefreshCol.col3 = [];
    $scope.flag = 0;
    $scope.HasSearch = false;
    $scope.IsSuccess = false;
    $scope.countError = 0;
    $scope.ORDItemCon = null;
    $scope.OPSItemContainer = null;
    $scope.CUSItem = null;
    $scope.CusHasChoose = false;

    $scope.Search_Click = function ($event) {
        $event.preventDefault();
        $rootScope.IsLoading = true;
        Common.Services.Call($http, {
            url: Common.Services.url.FIN,
            method: _FINRefresh_Index.URL.List,
            data: { dtfrom: $scope.Item.DateFrom, dtto: $scope.Item.DateTo },
            success: function (res) {
                if (Common.HasValue(res)) {
                    $timeout(function () {
                        angular.forEach(res.ListPL, function (v, i) {
                            v.Effdate = Common.Date.FromJson(v.Effdate);
                        });
                        $scope.finrefresh_indexgridOptions.dataSource.data(res.ListPL);

                        var datafresh = [];
                        var data = [];
                        for (var dtCurrent = $scope.Item.DateFrom; dtCurrent.getTime() < $scope.Item.DateTo.getTime() + 1 ; dtCurrent = Common.Date.AddDay(dtCurrent, 1)) {
                            data.push({ IsChoose: false, Date: dtCurrent });
                            datafresh.push({ IsChoose: false, Date: dtCurrent });
                        }
                        $scope.flag = 0;
                        _FINRefresh_Index.Data.RefreshData = datafresh;
                        $scope.loadcol(data);

                        $scope.finrefresh_indexwin_gridOptions.dataSource.data(data);
                        $scope.HasSearch = true;
                        $rootScope.IsLoading = false;
                    }, 1);
                }
            }
        });
    };


    $scope.RoutingOrder_Click = function ($event,win) {
        $event.preventDefault();

        $scope.RoutingOrder_gridOptions.dataSource.read();
        win.center();
        win.open();
    };
    $scope.RoutingOrder_gridOptions = {
        dataSource: Common.DataSource.Grid($http,{
                url: Common.Services.url.FIN,
                method: _FINRefresh_Index.URL.CUS_List,
                pageSize: 20,
                model: {
                    id: 'ID',
                    fields: {
                        ID: { type: 'number', editable: false, nullable: true },
                        IsChoose: { type: 'boolean' },
                        CustomerName: { type: 'string' },
                    }
                },
        }),
        height: '100%', pageable: true, sortable: true, columnMenu: false, resizable: true, reorderable: true, filterable: { mode: 'row' },
        columns: [
             {
                 title: ' ', width: '35px',
                 headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,RoutingOrder_grid,RoutingOrder_Change)" />',
                 headerAttributes: { style: 'text-align: center;' }, attributes: { style: 'text-align: center;' },
                 template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,RoutingOrder_grid,RoutingOrder_Change)" />',
                 filterable: false, sortable: false
             },
           
            { field: 'Code', title: 'Mã khách hàng', width: '200px', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'CustomerName', title: 'Tên khách hàng', width: '200px', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { title: ' ', sortable: false, filterable: false, menu: false }
        ],
    };

    $scope.CusRefresh_gridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.FIN,
            method: _FINRefresh_Index.URL.CUS_List,
            pageSize: 20,
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number', editable: false, nullable: true },
                    IsChoose: { type: 'boolean' },
                    CustomerName: { type: 'string' },
                }
            },
        }),
        height: '100%', pageable: true, sortable: true, columnMenu: false, resizable: true, reorderable: true, filterable: { mode: 'row' },
        columns: [
             {
                 title: ' ', width: '35px',
                 headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,CusRefresh_grid,CusRefresh_grid_Change)" />',
                 headerAttributes: { style: 'text-align: center;' }, attributes: { style: 'text-align: center;' },
                 template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,CusRefresh_grid,CusRefresh_grid_Change)" />',
                 filterable: false, sortable: false
             },

            { field: 'Code', title: 'Mã khách hàng', width: '200px', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'CustomerName', title: 'Tên khách hàng', width: '200px', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { title: ' ', sortable: false, filterable: false, menu: false }
        ],
    };

    $scope.RoutingOrder_Change = function ($event, grid, hasChoose) {
        $scope.CusHasChoose = hasChoose;
    }

    $scope.CusRefresh_grid_Change = function ($event, grid, hasChoose) {
        $scope.CusRefreshHasChoose = hasChoose;
    }

    $scope.RoutingOrderCheck_Click = function ($event, win, grid) {
        $event.preventDefault();
        var lstID = [];
        var dataSource = grid.dataSource.data();
        for (var i = 0; i < dataSource.length; i++) {
            if (dataSource[i].IsChoose) {
                lstID.push(dataSource[i].ID);
            }
        }
        $rootScope.IsLoading = true;
        Common.Services.Call($http, {
            url: Common.Services.url.FIN,
            method: _FINRefresh_Index.URL.Refresh_Order,
            data: { dtfrom: $scope.Item.DateFrom, dtto: $scope.Item.DateTo, lstID: lstID },
            success: function (res) {
                $rootScope.IsLoading = false;
                $rootScope.Message({ Msg: "Đã cập nhật!" });
                if (Common.HasValue(res)) {
                    $scope.RoutingOrder_gridOptions.dataSource.read();
                    win.close();
                }
            }
        });
    };


    $scope.RoutingTO_Click = function ($event, win) {
        $event.preventDefault();

        $scope.RoutingTO_gridOptions.dataSource.read();
        win.center();
        win.open();
    };
    $scope.RoutingTO_gridOptions = {
        dataSource: Common.DataSource.Grid($http,
            {
                url: Common.Services.url.FIN,
                method: _FINRefresh_Index.URL.CUS_List,
                pageSize: 20,
                model: {
                    id: 'ID',
                    fields: {
                        ID: { type: 'number', editable: false, nullable: true },
                        IsChoose: { type: 'boolean' },
                        CustomerName: { type: 'string' },
                    }
                },
            }),
        height: '100%', pageable: true, sortable: true, columnMenu: false, resizable: true, reorderable: true, filterable: { mode: 'row' },
        columns: [
             {
                 title: ' ', width: '35px',
                 headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,RoutingTO_grid,RoutingTO_Change)" />',
                 headerAttributes: { style: 'text-align: center;' }, attributes: { style: 'text-align: center;' },
                 template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,RoutingTO_grid,RoutingTO_Change)" />',
                 filterable: false, sortable: false
             },

            { field: 'Code', title: 'Mã khách hàng', width: '200px', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'CustomerName', title: 'Tên khách hàng', width: '200px', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { title: ' ', sortable: false, filterable: false, menu: false }
        ],
    };
    $scope.RoutingTO_Change = function ($event, grid, hasChoose) {
        $scope.CusHasChoose = hasChoose;
    }
    $scope.RoutingTOCheck_Click = function ($event, win, grid) {
        $event.preventDefault();
        var lstID = [];
        var dataSource = grid.dataSource.data();
        for (var i = 0; i < dataSource.length; i++) {
            if (dataSource[i].IsChoose) {
                lstID.push(dataSource[i].ID);
            }
        }
        $rootScope.IsLoading = true;
        Common.Services.Call($http, {
            url: Common.Services.url.FIN,
            method: _FINRefresh_Index.URL.Refresh_TO,
            data: { dtfrom: $scope.Item.DateFrom, dtto: $scope.Item.DateTo, lstID: lstID },
            success: function (res) {
                $rootScope.IsLoading = false;
                $rootScope.Message({ Msg: "Đã cập nhật!" });
                if (Common.HasValue(res)) {
                    $scope.RoutingTO_gridOptions.dataSource.read();
                    win.close();
                }
            }
        });
    };


    $scope.loadcol = function (data) {

        $scope.RefreshCol.col1 = [];
        $scope.RefreshCol.col2 = [];
        $scope.RefreshCol.col3 = [];

        var count = 0;
        var length = data.length / 3;
       
        $.each(data, function (i, v) {
            v.idcount = count;
            v.Date = Common.Date.FromJsonDMY(v.Date);
            if (count < length) {
                $scope.RefreshCol.col1.push(v);
            } else {
                if (count < (length + length)) {
                    $scope.RefreshCol.col2.push(v);
                }
                else {
                    $scope.RefreshCol.col3.push(v);
                }
            }
            count++;
        })
    }

    $scope.Refresh_Click = function ($event, win) {
        $event.preventDefault();

        $scope.finrefresh_index_col_plwin.open().center();

       // win.open();
        //win.center();

        $timeout(function () {
            $scope.finrefresh_indexwin_grid.resize();
        }, 300);
    };

    $scope.Close_Click = function ($event, win) {
        $event.preventDefault();

        win.close();
    };

    $scope.PLAccept_Click = function ($event) {
        $event.preventDefault();
        $scope.CusRefresh_win.center().open();
        $timeout(function () {
            $scope.CusRefresh_grid.resize();
        }, 200);
    }

    $scope.CusRefresh_Choose_Click = function ($event, grid, win) {
        $event.preventDefault();
        var datalstCus = [];
        $.each(grid.dataSource.data(), function (i, v) {
            if (v.IsChoose)
                datalstCus.push(v.ID);
        });

        $timeout(function () {
            if (Common.HasValue(datalstCus) && datalstCus.length > 0) {
                $scope.lstCUSID = datalstCus;
                $rootScope.IsLoading = true;
                $scope.PLAccept_Refesh();
            }
        }, 200);
        win.close();
    }

    $scope.PLAccept_Refesh = function () {
        $scope.countError = 0;
        var data = [];
        var RefreshData = [];
        _FINRefresh_Index.Data.RefreshIndex = 0;
        //load grid popup
        for (var dtCurrent = $scope.Item.DateFrom; dtCurrent.getTime() < $scope.Item.DateTo.getTime() + 1 ; dtCurrent = Common.Date.AddDay(dtCurrent, 1)) {
            data.push({ IsChoose: false, Date: dtCurrent });
            RefreshData.push({ IsChoose: false, Date: dtCurrent });
        }
        _FINRefresh_Index.Data.RefreshData = RefreshData;
        $scope.loadcol(data);
        //run code fresh
        $timeout(function () {
            $scope.IsSuccess = false;
            $scope.PLAccept_Running();
        }, 400);
    };

    $scope.PLAccept_Running = function () {
        if (_FINRefresh_Index.Data.RefreshIndex < _FINRefresh_Index.Data.RefreshData.length) {
            var tr = $(".panel-col-body").find('tr[data-count="' + _FINRefresh_Index.Data.RefreshIndex + '"]');
            tr.find(".width-col-1 > div").removeClass("red green normal");
            tr.find(".width-col-1 > div").html('<i class="fa fa-spinner fa-spin"></i>');
            Common.Services.Call($http, {
                url: Common.Services.url.FIN,
                method: _FINRefresh_Index.URL.Refresh,
                data: { date: _FINRefresh_Index.Data.RefreshData[_FINRefresh_Index.Data.RefreshIndex].Date, lstID: $scope.lstCUSID },
                success: function (res) {
                    if (Common.HasValue(res)) {
                        if (res != '') {
                            tr.find(".width-col-1 > div").html('<i class="fa fa-exclamation-triangle"></i>');
                            tr.find(".width-col-1 > div").removeClass("red green normal");
                            tr.find(".width-col-1 > div").addClass("red");
                            tr.find('.lblError').html(res);
                            tr.find('.lblError').attr("title", res);
                            $scope.countError++;
                        }
                        else {
                            tr.find(".width-col-1 > div").html('<i class="fa fa-check"></i>');
                            tr.find(".width-col-1 > div").removeClass("red green normal");
                            tr.find(".width-col-1 > div").addClass("green");
                            tr.find('.lblError').html("");
                            tr.find('.lblError').attr("title", "");
                        }
                        _FINRefresh_Index.Data.RefreshIndex++;
                        $scope.PLAccept_Running();
                    }
                }
            });
        }
        else {
            $scope.IsSuccess = true;
            $(".text-notification").removeClass("red green");
            if ($scope.countError > 0) {
                $(".text-notification").addClass("red");
            }
            else {
                $(".text-notification").addClass("green");
            }
            $rootScope.IsLoading = false;
        }
    };

    $scope.finrefresh_indexgridOptions = {
        dataSource: Common.DataSource.Local({
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    VehicleCode: { type: 'string' },
                    VendorCode: { type: 'string' },
                    VendorName: { type: 'string' },
                    OrderCode: { type: 'string' },
                    CustomerCode: { type: 'string' },
                    CustomerName: { type: 'string' },
                    DriverCode: { type: 'string' },
                    DriverName: { type: 'string' },
                    Credit: { type: 'number' },
                    Debit: { type: 'number' },
                    Effdate: { type: 'date' }
                }
            },
            pageSize: 100
        }),
        height: '100%', pageable: true, sortable: true, columnMenu: false, resizable: true, reorderable: true, filterable: { mode: 'row' },
        columns: [
            { field: 'VehicleCode', title: 'Số xe', width: '150px', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'VendorCode', title: 'Mã vận tải', width: '150px', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'VendorName', title: 'Đối tác vận tải', width: '150px', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'OrderCode', title: 'Đơn hàng', width: '150px', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'CustomerCode', title: 'Mã khách hàng', width: '150px', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'CustomerName', title: 'Tên khách hàng', width: '150px', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'DriverCode', title: 'Mã tài xế', width: '150px', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'DriverName', title: 'Tên tài xế', width: '150px', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'Credit', title: 'Thu', width: '100px', template: '#=Common.Number.ToMoney(Credit)#', filterable: { cell: { operator: 'gte', showOperators: false } } },
            { field: 'Debit', title: 'Chi', width: '100px', template: '#=Common.Number.ToMoney(Debit)#', filterable: { cell: { operator: 'gte', showOperators: false } } },
            {
                field: 'Effdate', title: 'Ngày', width: '150px', template: '#=Common.Date.FromJsonDMY(Effdate)#',
                filterable: { cell: { template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); }, operator: 'equal', showOperators: false } },
            },
            { title: ' ', sortable: false, filterable: false, menu: false }
        ],
    };

    $scope.finrefresh_indexwin_gridOptions = {
        dataSource: Common.DataSource.Local({
            model: {
                id: 'Date',
                fields: {
                    IsChoose: { type: 'bool', defaultValue: false },
                    Date: { type: 'date' }
                }
            }
        }),
        height: '100%', pageable: false, sortable: true, columnMenu: false, resizable: false, reorderable: false,
        columns: [
            {
                title: ' ', width: '40px',
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # disabled="disabled" />',
                templateAttributes: { style: 'text-align: center;' },
                filterable: false, sortable: false
            },
            { field: 'Date', title: 'Ngày', width: '150px', template: '#=Common.Date.FromJsonDMY(Date)#' },
            { title: ' ', template: '<span class="lblError"></span>', sortable: false, filterable: false, menu: false }
        ],
    };

    $scope.ChangeORD_Click = function ($event, win) {
        $event.preventDefault();

        win.center().open();
        $scope.finrefreshContainer_index_ordwin_gridOptions.dataSource.read();
        $scope.finrefresh_index_ordwin_gridOptions.dataSource.read();
    }

    $scope.ChangeOPS_Click = function ($event, win) {
        $event.preventDefault();

        win.center().open();
        $scope.finrefresh_index_opswin_gridOptions.dataSource.read();
        $scope.finrefreshContainer_index_opswin_gridOptions.dataSource.read();
    }

    $scope.ORDItem = null
    $scope.OPSItem = null

    $scope.finrefresh_index_ordwin_gridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.FIN,
            method: _FINRefresh_Index.URL.ORD_GroupList,
            readparam: function () {
                return { from: $scope.Item.DateFrom, to: $scope.Item.DateTo }
            },
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    RequestDate: { type: 'date' },
                    ORDDateConfig: { type: 'date' },
                    DateConfig: { type: 'date' }
                }
            }
        }),
        height: '99%', groupable: false, pageable: true, sortable: true, columnMenu: false, resizable: true,
        selectable: false, filterable: { mode: 'row' }, reorderable: false,
        columns: [
            {
                title: ' ', width: '50px',
                template: '<a href="/" ng-click="ORD_Group_Change($event,dataItem,finrefresh_index_ord_groupwin)" class="k-button"><i class="fa fa-pencil"></i></a>',
                filterable: false, sortable: false
            },
            {
                field: 'OrderCode', width: 100, title: 'Mã ĐH',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'CustomerName', width: 250, title: 'Khách hàng',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'RoutingName', width: 150, title: 'Cung đường',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'RequestDate', width: 120, title: 'Ngày gửi', template: '#=Common.Date.FromJsonDMYHM(RequestDate)#',
                filterable: { cell: { template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); }, operator: 'equal', showOperators: false } },
            },
            {
                field: 'DateConfig', width: 120, title: 'Ngày tính giá', template: '#=Common.Date.FromJsonDMYHM(DateConfig)#',
                filterable: { cell: { template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); }, operator: 'equal', showOperators: false } },
            },
            {
                field: 'OrderDateConfig', width: 120, title: 'Ngày chi tiết ĐH', template: '#=Common.Date.FromJsonDMYHM(OrderDateConfig)#',
                filterable: { cell: { template: function (e) { e.element.kendoDateTimePicker({ format: Common.Date.Format.DMY, timeFormat: Common.Date.Format.HM }); }, operator: 'equal', showOperators: false } },
            },
            {
                field: 'GroupOfVehicleName', width: 100, title: 'Loại xe',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'ORDRoutingName', width: 150, title: 'Cung đường ĐH',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'LocationFromAddress', width: 100, title: 'Địa chỉ đi',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'LocationToAddress', width: 100, title: 'Địa chỉ đến',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'SOCode', width: 100, title: 'Số SO',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'DNCode', width: 100, title: 'Số DN',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'GroupCode', width: 100, title: 'Mã nhóm hàng',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'GroupName', width: 200, title: 'Nhóm hàng hóa',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'Ton', width: 100, title: 'Trọng tải',
                filterable: { cell: { operator: 'gte', showOperators: false } }
            },
            { title: '', filterable: false, sortable: false }
        ]
    }

    $scope.finrefresh_index_opswin_gridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.FIN,
            method: _FINRefresh_Index.URL.OPS_GroupList,
            readparam: function () {
                return { from: $scope.Item.DateFrom, to: $scope.Item.DateTo }
            },
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    ETA: { type: 'date' },
                    ETD: { type: 'date' },
                    RequestDate: { type: 'date' },
                    DateConfigMaster: { type: 'date' },
                    DateConfig: { type: 'date' },
                }
            }
        }),
        height: '99%', groupable: false, pageable: true, sortable: true, columnMenu: false, resizable: true,
        selectable: false, filterable: { mode: 'row' }, reorderable: false,
        columns: [
            {
                title: ' ', width: '50px',
                template: '<a href="/" ng-click="OPS_Group_Change($event,dataItem,finrefresh_index_ops_masterwin)" class="k-button"><i class="fa fa-pencil"></i></a>',
                filterable: false, sortable: false
            },
            {
                field: 'DateConfig', width: 150, title: 'Ngày tính giá chi tiết', template: '#=Common.Date.FromJsonDMY(DateConfig)#',
                filterable: { cell: { operator: 'gte', showOperators: false } }
            },
            {
                field: 'OrderCode', width: 150, title: 'Mã đơn hàng',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'OrderGroupCode', width: 150, title: 'Nhóm hàng',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'CustormerCode', width: 150, title: 'Mã KH',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'CustormerName', width: 150, title: 'Khách hàng',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'SOCode', width: 150, title: 'Số SO',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'DNCode', width: 150, title: 'Số DN',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'DateConfigMaster', width: 150, title: 'Ngày tính giá chuyến', template: '#=Common.Date.FromJsonDMY(DateConfigMaster)#',
                filterable: { cell: { operator: 'gte', showOperators: false } }
            },
            {
                field: 'ToMasterCode', width: 100, title: 'Số chuyến',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'VehicleNo', width: 100, title: 'Số xe',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'VendorName', width: 150, title: 'Nhà xe',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'ORDGroupRoutingCode', width: 150, title: 'Mã cung đường thu',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'OPSGroupRoutingCode', width: 150, title: 'Mã cung đường chi',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'GroupOfVehicleName', width: 150, title: 'Loại xe',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'TransportModeName', width: 100, title: 'Loại v/c',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'ContractNo', width: 150, title: 'Hợp đồng',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },            
            {
                field: 'RequestDate', width: 100, title: 'Ngày y/c', template: '#=Common.Date.FromJsonDMY(RequestDate)#',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'ETD', width: 120, title: 'ETD', template: '#=Common.Date.FromJsonDMYHM(ETD)#',
                filterable: { cell: { template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); }, operator: 'equal', showOperators: false } },
            },
            {
                field: 'ETA', width: 120, title: 'ETA', template: '#=Common.Date.FromJsonDMYHM(ETA)#',
                filterable: { cell: { template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); }, operator: 'equal', showOperators: false } },
            },
            { title: '', filterable: false, sortable: false }
        ]
    }

    $scope.ver_splitterOptions = {
        orientation: "vertical",
        panes: [
            { collapsible: false, resizable: false, size: "55%" },
            { collapsible: false, resizable: false }
        ]
    };

    $scope.cboORDContractOptions = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true,
        dataTextField: 'DisplayName', dataValueField: 'ID', minLength: 3,
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    DisplayName: { type: 'string' },
                }
            }
        }),
        change: function () {
            $scope.ORDItem.RoutingID = "";
            $scope.LoadDataRouting($scope.cboORDRoutingOptions, $scope.ORDItem.CustomerID, $scope.ORDItem.ContractID);
        }
    };


    $scope.cboORDGOVOptions = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: "contains", suggest: true,
        dataTextField: 'GroupName', dataValueField: 'ID',
        dataSource: Common.DataSource.Local([], {
            id: 'ID',
            fields: {
                GroupName: { type: 'string' },
                ID: { type: 'number' },
            }
        })
    }

    Common.ALL.Get($http, {
        url: Common.ALL.URL.GroupOfVehicle,
        success: function (data) {
            var newData = [];
            newData.push({ ID: -1, GroupName: ' ' });
            Common.Data.Each(data, function (o) {
                newData.push(o);
            })
            $scope.cboORDGOVOptions.dataSource.data(newData)
        }
    })


    $scope.cboORDRoutingOptions = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true,
        dataTextField: 'RoutingName', dataValueField: 'ID', minLength: 3,
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    RoutingName: { type: 'string' },
                }
            }
        })
    };

    $scope.cboRoutingORDOptions = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true,
        dataTextField: 'RoutingName', dataValueField: 'ID', minLength: 3,
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    RoutingName: { type: 'string' },
                }
            }
        })
    };


    $scope.LoadDataContract = function (cbo, cusID, serID, transID) {
        if (cusID > 0 && serID > 0 && transID > 0) {
            Common.Services.Call($http, {
                url: Common.Services.url.FIN,
                method: _FINRefresh_Index.URL.ORD_ContractList,
                data: { cusID: cusID, serID: serID, transID: transID },
                success: function (res) {
                    $timeout(function () {
                        cbo.dataSource.data(res);
                    }, 1);
                }
            });
        } else {
            cbo.dataSource.data([]);
        }
    }

    $scope.LoadDataContractMaster = function (cbo, vendorid, serID, transID) {
        Common.Log("LoadDataContractMaster")
        if (vendorid > 0 && transID > 0) {
            Common.Services.Call($http, {
                url: Common.Services.url.FIN,
                method: _FINRefresh_Index.URL.OPS_ContractMasterList,
                data: { vendorid: vendorid, transID: transID, serID: serID },
                success: function (res) {
                    $timeout(function () {
                        cbo.dataSource.data(res);
                    }, 1);
                }
            });
        } else {
            cbo.dataSource.data([]);
        }
    }

    $scope.LoadDataRouting = function (cbo, cusID, contractID) {
        Common.Services.Call($http, {
            url: Common.Services.url.FIN,
            method: _FINRefresh_Index.URL.ORD_RoutingList,
            data: { cusID: cusID, contractID: contractID },
            success: function (res) {
                $timeout(function () {
                    var data = [];
                    data.push({ RoutingName: "", RoutingID: -1 });
                    $.each(res, function (i, v) {
                        data.push(v);
                    });
                    cbo.dataSource.data(data);
                }, 1);
            }
        });
    }
    $scope.LoadDataRoutingORD = function (cbo, cusID, contractID) {
        Common.Services.Call($http, {
            url: Common.Services.url.FIN,
            method: _FINRefresh_Index.URL.ORD_RoutingList,
            data: { cusID: cusID, contractID: contractID },
            success: function (res) {
                var data = [];
                data.push({ RoutingName: ' ', ID: -1 })
                Common.Data.Each(res, function (o) {
                    data.push(o)
                })
                $timeout(function () {
                    cbo.dataSource.data(data);
                }, 1);
            }
        });
    }

    $scope.LoadDataOPSRoutingMaster = function (cbo, vendorid, contractID) {
        Common.Services.Call($http, {
            url: Common.Services.url.FIN,
            method: _FINRefresh_Index.URL.OPS_RoutingMasterList,
            data: { vendorid: vendorid, contractID: contractID },
            success: function (res) {
                $timeout(function () {
                    cbo.dataSource.data(res);
                }, 1);
            }
        });
    }

    $scope.LoadDataOPSRouting = function (cbo, routingID, cusID, contractID) {
        Common.Services.Call($http, {
            url: Common.Services.url.FIN,
            method: _FINRefresh_Index.URL.ORD_OPSRoutingList,
            data: {routingID:routingID, cusID: cusID, contractID: contractID },
            success: function (res) {
                $timeout(function () {
                    cbo.dataSource.data(res);
                }, 1);
            }
        });
    }
    $scope.LoadDataOPSGroupRouting = function (cbo, RoutingID, vendorid,contractid) {
        Common.Services.Call($http, {
            url: Common.Services.url.FIN,
            method: _FINRefresh_Index.URL.OPS_OPSGroupRoutingList,
            data: {routingID:RoutingID, vendorid: vendorid, contractid: contractid },
            success: function (res) {
                $timeout(function () {
                    cbo.dataSource.data(res);
                }, 1);
            }
        });
    }

    $scope.ORD_Group_Change = function ($event, item, win) {
        $event.preventDefault();
        $scope.ORDItem = null;
        $scope.ORDItem = $.extend({}, true, item);

        $scope.LoadDataContract($scope.cboORDContractOptions, $scope.ORDItem.CustomerID, $scope.ORDItem.ServiceOfOrderID, $scope.ORDItem.TransportModeID);
        $scope.LoadDataRouting($scope.cboORDRoutingOptions, $scope.ORDItem.CustomerID, $scope.ORDItem.ContractID);
        $scope.LoadDataRoutingORD($scope.cboRoutingORDOptions, $scope.ORDItem.CustomerID, $scope.ORDItem.ContractID);
        win.center().open();
        $scope.ver_splitter.resize();
    }

    $scope.ChangeORD_Submit_Click = function ($event, win) {
        $event.preventDefault();

        $rootScope.Message({
            Type: Common.Message.Type.Confirm,
            Msg: 'Xác nhận lưu?',
            Ok: function () {
                Common.Services.Call($http, {
                    url: Common.Services.url.FIN,
                    method: _FINRefresh_Index.URL.ORD_GroupSave,
                    data: { item: $scope.ORDItem },
                    success: function (res) {
                        Common.Services.Error(res, function (res) {
                            $rootScope.Message({ Msg: "Đã cập nhật!" });
                            $scope.finrefresh_index_ordwin_gridOptions.dataSource.read();
                            win.close();
                        })
                    }
                });
            }
        })
    }

    $scope.cboOPSTypeOfOrderOptions = {
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
    };

    Common.ALL.Get($http, {
        url: Common.ALL.URL.SYSVarTypeOfOrder,
        success: function (data) {
            $timeout(function () {
                $scope.cboOPSTypeOfOrderOptions.dataSource.data(data);
            }, 1);
        }
    });

    $scope.cboOPSTransportModeOptions = {
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
        }),
        change: function (e) {
            $scope.LoadDataContractMaster($scope.cboOPSContractOptions, $scope.OPSItem.VendorID, $scope.OPSItem.ServiceOfOrderID, $scope.OPSItem.TransportModeID);
        }
    };

    Common.ALL.Get($http, {
        url: Common.ALL.URL.SYSVarTransportMode,
        success: function (data) {
            $timeout(function () {
                $scope.cboOPSTransportModeOptions.dataSource.data(data);
                $scope.cboOPSTransportModeContainerOptions.dataSource.data(data);
            }, 1);
        }
    });

    $scope.cboOPSContractOptions = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true,
        dataTextField: 'DisplayName', dataValueField: 'ID', minLength: 3,
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    DisplayName: { type: 'string' },
                }
            }
        }),
        change: function () {
            $scope.LoadDataOPSRoutingMaster($scope.cboOPSRoutingOptions, $scope.OPSItem.VendorID, $scope.OPSItem.ContractID);
            $scope.LoadDataOPSGroupRouting($scope.cboOPSRoutingGroupOptions, $scope.OPSItem.$scope.OPSItem.RoutingIDGroup, $scope.OPSItem.VendorID, $scope.OPSItem.ContractID);
        }
    };

    $scope.cboOPSRoutingOptions = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true,
        dataTextField: 'RoutingName', dataValueField: 'RoutingID', minLength: 3,
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'RoutingID',
                fields: {
                    RoutingID: { type: 'number' },
                    RoutingName: { type: 'string' },
                }
            }
        })
    };

    $scope.cboOPSDriverRouteFeeOptions = {
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
    };

    $scope.cboOPSRoutingGroupOptions = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: "contains", suggest: true,
        dataTextField: 'RoutingName', dataValueField: 'ID',
        dataSource: Common.DataSource.Local([], {
            id: 'ID',
            fields: {
                RoutingName: { type: 'string' },
                ID: { type: 'number' },
            }
        })
    }

    $scope.cboOPSGOVOptions = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: "contains", suggest: true,
        dataTextField: 'GroupName', dataValueField: 'ID',
        dataSource: Common.DataSource.Local([], {
            id: 'ID',
            fields: {
                GroupName: { type: 'string' },
                ID: { type: 'number' },
            }
        })
    }


    $scope.cboOPSServiceOfOrderOptions = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: "contains", suggest: true,
        dataTextField: 'ValueOfVar', dataValueField: 'ID',
        dataSource: Common.DataSource.Local([], {
            id: 'ID',
            fields: {
                ValueOfVar: { type: 'string' },
                ID: { type: 'number' },
            }
        }),
        change: function (e) {
            $scope.LoadDataContractMaster($scope.cboOPSContractOptions, $scope.OPSItem.VendorID, $scope.OPSItem.ServiceOfOrderID, $scope.OPSItem.TransportModeID);
        }
    }

    Common.ALL.Get($http, {
        url: Common.ALL.URL.SYSVarServiceOfOrder,
        success: function (data) {
            $scope.cboOPSServiceOfOrderOptions.dataSource.data(data)
        }
    })


    Common.ALL.Get($http, {
        url: Common.ALL.URL.GroupOfVehicle,
        success: function (data) {
            var newData = [];
            newData.push({ ID: -1, GroupName: ' ' });
            Common.Data.Each(data, function (o) {
                newData.push(o);
            })
            $scope.cboOPSGOVOptions.dataSource.data(newData)
        }
    })

    Common.ALL.Get($http, {
        url: Common.ALL.URL.SYSVarTypeOfDriverRouteFee,
        success: function (data) {
            $timeout(function () {
                $scope.cboOPSDriverRouteFeeOptions.dataSource.data(data);
            }, 1);
        }
    });

    $scope.OPS_Group_Change = function ($event, item, win) {
        $event.preventDefault();
        $scope.OPSItem = null;
        $scope.OPSItem = $.extend({}, true, item);
        var vendorID = $scope.OPSItem.VendorID > 0 ? $scope.OPSItem.VendorID : -1;
        var contractID = $scope.OPSItem.ContractID > 0 ? $scope.OPSItem.ContractID : -1;
        
        $scope.LoadDataContractMaster($scope.cboOPSContractOptions, vendorID, $scope.OPSItem.ServiceOfOrderID, $scope.OPSItem.TransportModeID);

        $scope.LoadDataOPSRoutingMaster($scope.cboOPSRoutingOptions, vendorID, $scope.OPSItem.ContractID);
        $scope.LoadDataOPSGroupRouting($scope.cboOPSRoutingGroupOptions, $scope.OPSItem.RoutingIDGroup, vendorID, contractID);
        win.center().open();
        $scope.ver_splitter_ops.resize();
    }

    $scope.ChangeOPS_Submit_Click = function ($event, win) {
        $event.preventDefault();

        $rootScope.Message({
            Type: Common.Message.Type.Confirm,
            Msg: 'Xác nhận lưu?',
            Ok: function () {
                Common.Services.Call($http, {
                    url: Common.Services.url.FIN,
                    method: _FINRefresh_Index.URL.OPS_GroupSave,
                    data: { item: $scope.OPSItem },
                    success: function (res) {
                        Common.Services.Error(res, function (res) {
                            $rootScope.Message({ Msg: "Đã cập nhật!" });
                            $scope.finrefresh_index_opswin_gridOptions.dataSource.read();
                            win.close();
                        })
                    }
                });
            }
        })
    }

    $scope.ManualFix_Click = function ($event) {
        $event.preventDefault();
        $state.go('main.FINRefresh.ManualFix');
    };
    
    $scope.TabIndex = "1";
    $scope.finrefresh_tabOptions = {
        animation: {
            open: { effects: "fadeIn" }
        },
        select: function (e) {
            $timeout(function () {
                $scope.TabIndex = angular.element(e.item).data('tabindex'); //or
            }, 1);
        }
    }

    $scope.finrefreshContainer_index_ordwin_gridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.FIN,
            method: _FINRefresh_Index.URL.ORD_ContainerList,
            readparam: function () {
                return { from: $scope.Item.DateFrom, to: $scope.Item.DateTo }
            },
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    RequestDate: { type: 'date' },
                    ORDDateConfig: { type: 'date' },
                    DateConfig: { type: 'date' }
                }
            }
        }),
        height: '99%', groupable: false, pageable: true, sortable: true, columnMenu: false, resizable: true,
        selectable: false, filterable: { mode: 'row' }, reorderable: false,
        columns: [
            {
                title: ' ', width: '50px',
                template: '<a href="/" ng-click="ORD_Container_Change($event,dataItem,finrefreshcontainer_index_ord_groupwin)" class="k-button"><i class="fa fa-pencil"></i></a>',
                filterable: false, sortable: false
            },
            {
                field: 'OrderCode', width: 100, title: 'Mã ĐH',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'CustomerName', width: 250, title: 'Khách hàng',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'RoutingName', width: 150, title: 'Cung đường',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'RequestDate', width: 120, title: 'Ngày gửi', template: '#=Common.Date.FromJsonDMYHM(RequestDate)#',
                filterable: { cell: { template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); }, operator: 'equal', showOperators: false } },
            },
            {
                field: 'DateConfig', width: 120, title: 'Ngày tính giá', template: '#=Common.Date.FromJsonDMYHM(DateConfig)#',
                filterable: { cell: { template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); }, operator: 'equal', showOperators: false } },
            },
            {
                field: 'OrderDateConfig', width: 120, title: 'Ngày chi tiết ĐH', template: '#=Common.Date.FromJsonDMYHM(OrderDateConfig)#',
                filterable: { cell: { template: function (e) { e.element.kendoDateTimePicker({ format: Common.Date.Format.DMY, timeFormat: Common.Date.Format.HM }); }, operator: 'equal', showOperators: false } },
            },
            {
                field: 'GroupOfVehicleName', width: 100, title: 'Loại cont',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'ORDRoutingName', width: 150, title: 'Cung đường ĐH',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'LocationFromAddress', width: 100, title: 'Địa chỉ đi',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'LocationToAddress', width: 100, title: 'Địa chỉ đến',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'SOCode', width: 100, title: 'Số SO',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'Ton', width: 100, title: 'Trọng tải',
                filterable: { cell: { operator: 'gte', showOperators: false } }
            },
            {
                field: 'LocationDepotAddress', width: 100, title: 'Điểm lấy Rommoc',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'LocationDepotReturnAddress', width: 100, title: 'Điểm trả Rommoc',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            { title: '', filterable: false, sortable: false }
        ]
    }

    $scope.ver_splittercontainerOptions = {
        orientation: "vertical",
        panes: [
            { collapsible: false, resizable: false, size: "55%" },
            { collapsible: false, resizable: false }
        ]
    };

    $scope.ORD_Container_Change = function ($event, item, win) {
        $event.preventDefault();
        $scope.ORDItemCon = null;
        $scope.ORDItemCon = $.extend({}, true, item);

        $scope.LoadDataContract($scope.cboORDContractContainerOptions, $scope.ORDItemCon.CustomerID, $scope.ORDItemCon.ServiceOfOrderID, $scope.ORDItemCon.TransportModeID);
        $scope.LoadDataRouting($scope.cboORDRoutingOptions, $scope.ORDItemCon.CustomerID, $scope.ORDItemCon.ContractID);
        $scope.LoadDataRoutingORD($scope.cboRoutingORDOptions, $scope.ORDItemCon.CustomerID, $scope.ORDItemCon.ContractID);
        $scope.LoadDataContractTerm($scope.cboORDContractTermOptions, $scope.ORDItemCon.ContractID);
        win.center().open();
        $timeout(function () {
            $scope.ver_splittercontainer.resize();
        }, 100);
    }
    $scope.cboORDContractContainerOptions = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true,
        dataTextField: 'DisplayName', dataValueField: 'ID', minLength: 3,
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    DisplayName: { type: 'string' },
                }
            }
        }),
        change: function () {
            $scope.ORDItemCon.RoutingID = -1;
            $scope.ORDItemCon.ContractTermID = -1;
            $scope.LoadDataContractTerm($scope.cboORDContractTermOptions, $scope.ORDItemCon.ContractID);
            $scope.LoadDataRouting($scope.cboORDRoutingOptions, $scope.ORDItemCon.CustomerID, $scope.ORDItemCon.ContractID);
        }
    };

    $scope.LoadDataContractTerm = function (cbo,contractID) {
        Common.Services.Call($http, {
            url: Common.Services.url.FIN,
            method: _FINRefresh_Index.URL.ORD_ContractTerm_List,
            data: {contractID: contractID },
            success: function (res) {
                $timeout(function () {
                    cbo.dataSource.data(res);
                }, 1);
            }
        });
    }

    $scope.cboORDContractTermOptions = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true,
        dataTextField: 'DisplayName', dataValueField: 'ID', minLength: 3,
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    DisplayName: { type: 'string' },
                }
            }
        }),
        change: function () {
        }
    };
    $scope.ChangeORDContainer_Submit_Click = function ($event, win) {
        $event.preventDefault();

        $rootScope.Message({
            Type: Common.Message.Type.Confirm,
            Msg: 'Xác nhận lưu?',
            Ok: function () {
                $rootScope.IsLoading = true;
                Common.Services.Call($http, {
                    url: Common.Services.url.FIN,
                    method: _FINRefresh_Index.URL.ORD_ContainerSave,
                    data: { item: $scope.ORDItemCon },
                    success: function (res) {
                        Common.Services.Error(res, function (res) {
                            $rootScope.Message({ Msg: "Đã cập nhật!" });
                            $rootScope.IsLoading = false;
                            $scope.finrefreshContainer_index_ordwin_grid.dataSource.read();
                            win.close();
                        })
                    }
                });
            }
        })
    }


    $scope.finrefreshops_tabOptions = {
        animation: {
            open: { effects: "fadeIn" }
        },
        select: function (e) {
        }
    }

    $scope.finrefreshContainer_index_opswin_gridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.FIN,
            method: _FINRefresh_Index.URL.OPS_GroupContainerList,
            readparam: function () {
                return { from: $scope.Item.DateFrom, to: $scope.Item.DateTo }
            },
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    ETA: { type: 'date' },
                    ETD: { type: 'date' },
                    RequestDate: { type: 'date' },
                    DateConfigMaster: { type: 'date' },
                    DateConfig: { type: 'date' },
                }
            }
        }),
        height: '99%', groupable: false, pageable: true, sortable: true, columnMenu: false, resizable: true,
        selectable: false, filterable: { mode: 'row' }, reorderable: false,
        columns: [
            {
                title: ' ', width: '50px',
                template: '<a href="/" ng-click="OPS_GroupContainer_Change($event,dataItem,finrefreshconatiner_index_ops_masterwin)" class="k-button"><i class="fa fa-pencil"></i></a>',
                filterable: false, sortable: false
            },
            {
                field: 'DateConfig', width: 150, title: 'Ngày tính giá chi tiết', template: '#=Common.Date.FromJsonDMY(DateConfig)#',
                filterable: { cell: { operator: 'gte', showOperators: false } }
            },
            {
                field: 'CustormerCode', width: 150, title: 'Mã KH',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'CustormerName', width: 150, title: 'Khách hàng',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'DateConfigMaster', width: 150, title: 'Ngày tính giá chuyến', template: '#=Common.Date.FromJsonDMY(DateConfigMaster)#',
                filterable: { cell: { operator: 'gte', showOperators: false } }
            },
            {
                field: 'ToMasterCode', width: 100, title: 'Số chuyến',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'VehicleNo', width: 100, title: 'Số xe',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'VendorName', width: 150, title: 'Nhà xe',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'ORDGroupRoutingName', width: 150, title: 'Cung đường ĐH',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'GroupOfVehicleName', width: 150, title: 'Loại cont',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'TransportModeName', width: 100, title: 'Loại v/c',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'ContractNo', width: 150, title: 'Hợp đồng',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'RequestDate', width: 100, title: 'Ngày y/c', template: '#=Common.Date.FromJsonDMY(RequestDate)#',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'ETD', width: 120, title: 'ETD', template: '#=Common.Date.FromJsonDMYHM(ETD)#',
                filterable: { cell: { template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); }, operator: 'equal', showOperators: false } },
            },
            {
                field: 'ETA', width: 120, title: 'ETA', template: '#=Common.Date.FromJsonDMYHM(ETA)#',
                filterable: { cell: { template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); }, operator: 'equal', showOperators: false } },
            },
            {
                field: 'LocationDepotAddress', width: 100, title: 'Điểm lấy Rommoc',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'LocationDepotReturnAddress', width: 100, title: 'Điểm trả Rommoc',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            { title: '', filterable: false, sortable: false }
        ]
    }

    $scope.OPS_GroupContainer_Change = function ($event, item, win) {
        $event.preventDefault();
        $scope.OPSItemContainer = null;
        $scope.OPSItemContainer = $.extend({}, true, item);
        var vendorID = $scope.OPSItemContainer.VendorID > 0 ? $scope.OPSItemContainer.VendorID : -1;
        var contractID = $scope.OPSItemContainer.ContractID > 0 ? $scope.OPSItemContainer.ContractID : -1;

        $scope.LoadDataContractMaster($scope.cboOPSContractContainerOptions, vendorID, $scope.OPSItemContainer.ServiceOfOrderID, $scope.OPSItemContainer.TransportModeID);

        $scope.LoadDataOPSRoutingMaster($scope.cboOPSRoutingOptions, vendorID, contractID);
        $scope.LoadDataOPSGroupRouting($scope.cboOPSRoutingGroupOptions, $scope.OPSItemContainer.RoutingIDGroup, vendorID, contractID);
        $scope.LoadDataContractTerm($scope.cboOPSContractTermOptions, contractID);
        win.center().open();
        $scope.ver_splitter_ops_container.resize();
    }

    $scope.cboOPSContractContainerOptions = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true,
        dataTextField: 'DisplayName', dataValueField: 'ID', minLength: 3,
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    DisplayName: { type: 'string' },
                }
            }
        }),
        change: function () {
            $scope.LoadDataOPSRoutingMaster($scope.cboOPSRoutingOptions, $scope.OPSItemContainer.VendorID, $scope.OPSItemContainer.ContractID);
            $scope.LoadDataOPSGroupRouting($scope.cboOPSRoutingGroupOptions, $scope.OPSItemContainer.RoutingIDGroup, $scope.OPSItemContainer.VendorID, $scope.OPSItemContainer.ContractID);
            $scope.LoadDataContractTerm($scope.cboOPSContractTermOptions, $scope.OPSItemContainer.ContractID);
        }
    };

    $scope.cboOPSTransportModeContainerOptions = {
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
        }),
        change: function (e) {
            $scope.LoadDataContractMaster($scope.cboOPSContractContainerOptions, $scope.OPSItemContainer.VendorID, $scope.OPSItemContainer.ServiceOfOrderID, $scope.OPSItemContainer.TransportModeID);
        }
    };
    $scope.cboOPSContractTermOptions = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true,
        dataTextField: 'DisplayName', dataValueField: 'ID', minLength: 3,
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    DisplayName: { type: 'string' },
                }
            }
        }),
        change: function () {
        }
    };
    $scope.ver_ops_splittercontainerOptions = {
        orientation: "vertical",
        panes: [
            { collapsible: false, resizable: false, size: "55%" },
            { collapsible: false, resizable: false }
        ]
    };
    $scope.ChangeOPS_Container_Submit_Click = function ($event, win) {
        $event.preventDefault();

        $rootScope.Message({
            Type: Common.Message.Type.Confirm,
            Msg: 'Xác nhận lưu?',
            Ok: function () {
                $rootScope.IsLoading = true;
                Common.Services.Call($http, {
                    url: Common.Services.url.FIN,
                    method: _FINRefresh_Index.URL.OPS_GroupContainerSave,
                    data: { item: $scope.OPSItemContainer },
                    success: function (res) {
                        Common.Services.Error(res, function (res) {
                            $rootScope.Message({ Msg: "Đã cập nhật!" });
                            $rootScope.IsLoading = false;
                            $scope.finrefreshContainer_index_opswin_grid.dataSource.read();
                            win.close();
                        })
                    }
                });
            }
        })
    }

}]);