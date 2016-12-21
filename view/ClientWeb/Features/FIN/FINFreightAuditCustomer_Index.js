/// <reference path="~/Scripts/jquery-1.9.1.intellisense.js" />
/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />

//#region Data
var _FINFreightAuditCUS = {
    URL: {
        Read: "FINFreightAuditCus_Order_List",
        //Detail: "FINFreightAuditCus_Order_DetailList",
        Detail: "FINFreightAuditCus_OrderDetail_List",
        Reject: "FINFreightAuditCus_Order_Reject",
        Accept: "FINFreightAuditCus_Order_Accept",
        Approved: "FINFreightAuditCus_Order_Approved",
        StatusList: "FINFreightAuditCus_Order_StatusList",
    },
    Data: {
        CookieSearch: "FINFreightAuditCus_Order_CookieSearch",
        _dataStatus: [],
        _defStatusID: -1,
        _approvedStatusID: -1,
     },
}
//#endregion

angular.module('myapp').controller('FINFreightAuditCustomer_IndexCtrl', ['$rootScope', '$scope', '$http', '$location', '$state', '$timeout', function ($rootScope, $scope, $http, $location, $state, $timeout, $stateParams) {
    if ($rootScope.IsPageComplete != true) return;
    Common.Log('FINFreightAuditCustomer_IndexCtrl');
    $rootScope.IsLoading = false;
    $scope.HasChoose = false;
    $scope.IsDefaultStatus = false;
    $scope.IsApprovedStatus = false;

    $scope.ItemFilter = {};
    $scope.ItemFilter.StatusID = -1;
    $scope.ItemFilter.DateFrom = new Date().addDays(-2);
    $scope.ItemFilter.DateTo = new Date().addDays(0);
    $scope.Item = {};
    $scope.Method = '';

    //#region Auth
    $scope.Auth = $rootScope.GetAuth();
    //#endregion

    $scope.FINFreight_win_tabOptions = {
        animation: {
            open: {
                effects: "fadeIn"
            }
        }
    };

    $scope.FINFreightAuditCus_gridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.FIN,
            method: _FINFreightAuditCUS.URL.Read,
            readparam: function () { return { statusID: $scope.ItemFilter.StatusID, DateFrom: $scope.ItemFilter.DateFrom, DateTo: $scope.ItemFilter.DateTo } },
            model: {
                id: 'OrderID',
                fields: {
                    OrderID: { type: 'number', nullable: false },
                    Debit: { type: 'number' },
                    DateConfig: { type: 'date' },
                }
            }
        }),
        height: '100%', groupable: false, pageable: false, sortable: true, columnMenu: false, resizable: true,
        selectable: 'row', filterable: { mode: 'row' }, reorderable: true,
        columns: [
            {
                title: ' ', width: '30px',
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,FINFreightAuditCus_grid,FINFreightAuditCus_gridChooseChange)" />',
                headerAttributes: { style: 'text-align: center;' }, attributes: { style: 'text-align: center;' },
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,FINFreightAuditCus_grid,FINFreightAuditCus_gridChooseChange)" />',
                filterable: false, sortable: false
            },
            {
                title: ' ', width: '40px',
                template: '<a href="/" ng-click="Detail_Click($event,FINFreightAuditCus_grid,FINFreightAuditCUS_win)"><i class="fa fa-file-text"></i></a>',
                filterable: false, sortable: false
            },
            {
                field: 'StatusName', title: 'Trạng thái', width: '125px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'OrderCode', title: 'Đơn hàng', width: '125px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'CustomerCode', title: 'Mã khách hàng', width: '125px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'CustomerName', title: 'Khách hàng', width: '125px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'DateConfig', title: 'Ngày tính giá', width: '125px',
                template: "#=DateConfig==null?' ':kendo.toString(DateConfig, '" + Common.Date.Format.DMY + "')#",
                filterable: { cell: { template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); }, operator: 'eq', showOperators: false } },
            },
            {
                field: 'Credit', title: 'Chi phí', width: '125px',
                template: '#=Common.Number.ToMoney(Credit)#', filterable: { cell: { operator: 'gte', showOperators: false } },
            },
            {
                field: 'PayCustomerModified', title: 'Khách hàng duyệt', width: '150px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'PayCustomerNote', title: 'Ghi chú khách hàng', width: '200px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'PayUserModified', title: 'Người duyệt', width: '150px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'PayUserNote', title: 'Ghi chú duyệt', width: '200px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
        ]
    }

    $scope.FINFreightAuditCus_gridChooseChange = function ($event, grid, haschoose) {
        $scope.HasChoose = haschoose;
    };

    $scope.FINFreightAuditCus_Credit_gridOptions = {
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields: {
                    CostID: { type: 'number', nullable: false },
                    Cost: { type: 'number' },
                    ID: { type: 'number' },
                }
            },
            group: { field: "CostName" }
        }),
        height: '99%', groupable: false, pageable: false, sortable: true, columnMenu: false, resizable: true,
        selectable: 'row', filterable: { mode: 'row' }, reorderable: true,
        columns: [
            {
                field: 'CostName', title: 'Loại phí', width: '175px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'SOCode', title: 'Số SO', width: '125px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'GroupOfProductNameCUS', title: 'Nhóm hàng', width: '125px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'LocationFromName', title: 'Điểm lấy', width: '125px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'LocationToName', title: 'Điểm giao', width: '125px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'RoutingCode', title: 'Mã cung đường', width: '125px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'RoutingName', title: 'Cung đường', width: '125px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'Unit', title: 'Đơn vị tính', width: '125px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'Quantity', title: 'Số lượng', width: '125px', template: '#=Quantity==null?" ":Common.Number.ToNumber5(Quantity)#',
                filterable: { cell: { operator: 'gte', showOperators: false } }
            },
            {
                field: 'UnitPrice', title: 'Đơn giá', width: '125px', template: '#=UnitPrice==null?" ":Common.Number.ToMoney(UnitPrice)#',
                filterable: { cell: { operator: 'gte', showOperators: false } }
            },
            {
                field: 'Cost', title: 'Thành tiền', width: '125px', template: '#=Cost==null?" ":Common.Number.ToMoney(Cost)#',
                filterable: { cell: { operator: 'gte', showOperators: false } }
            },
            {
                field: 'Note', title: 'Ghi chú 1', width: '125px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'Note1', title: 'Ghi chú 2', width: '125px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'DateFromCome', title: 'Ngày đến kho', width: '120px', template: "#=DateFromCome==null?' ':Common.Date.FromJsonDMY(DateFromCome)#",
                filterable: { cell: { template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); }, operator: 'equal', showOperators: false } },
            },
            {
                field: 'DateFromLoadStart', title: 'T/g bốc hàng', width: '120px', template: "#=DateFromLoadStart==null?' ':Common.Date.FromJsonHM(DateFromLoadStart)#",
                filterable: { cell: { template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); }, operator: 'equal', showOperators: false } },
            },
            {
                field: 'DateFromLoadEnd', title: 'T/g bốc hàng xong', width: '120px', template: "#=DateFromLoadEnd==null?' ':Common.Date.FromJsonHM(DateFromLoadEnd)#",
                filterable: { cell: { template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); }, operator: 'equal', showOperators: false } },
            },
            {
                field: 'DateFromLeave', title: 'Ngày rời kho', width: '120px', template: "#=DateFromLeave==null?' ':Common.Date.FromJsonDMY(DateFromLeave)#",
                filterable: { cell: { template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); }, operator: 'equal', showOperators: false } },
            },
            {
                field: 'DateToCome', title: 'Ngày đến giao hàng', width: '120px', template: "#=DateToCome==null?' ':Common.Date.FromJsonDMY(DateToCome)#",
                filterable: { cell: { template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); }, operator: 'equal', showOperators: false } },
            },
            {
                field: 'DateToLoadStart', title: 'T/g dỡ hàng', width: '120px', template: "#=DateToLoadStart==null?' ':Common.Date.FromJsonHM(DateToLoadStart)#",
                filterable: { cell: { template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); }, operator: 'equal', showOperators: false } },
            },
            {
                field: 'DateToLoadEnd', title: 'T/g dỡ hàng xong', width: '120px', template: "#=DateToLoadEnd==null?' ':Common.Date.FromJsonHM(DateToLoadEnd)#",
                filterable: { cell: { template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); }, operator: 'equal', showOperators: false } },
            },
            {
                field: 'DateToLeave', title: 'Ngày giao hàng xong', width: '120px', template: "#=DateToLeave==null?' ':Common.Date.FromJsonDMY(DateToLeave)#",
                filterable: { cell: { template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); }, operator: 'equal', showOperators: false } },
            },
        ]
    };

    $scope.FINFreightAuditCus_Trouble_gridOptions = {
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields: {
                    CostID: { type: 'number', nullable: false },
                    Cost: { type: 'number' },
                    ID: { type: 'number' },
                }
            }
        }),
        height: '99%', groupable: false, pageable: false, sortable: true, columnMenu: false, resizable: true,
        selectable: 'row', filterable: { mode: 'row' }, reorderable: true,
        columns: [
            {
                field: 'Cost', title: 'Chi phí', width: '125px', template: '#=Cost==null?" ":Common.Number.ToMoney(Cost)#',
                filterable: { cell: { operator: 'gte', showOperators: false } }
            },
            {
                field: 'Note', title: 'Ghi chú', width: '125px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
        ]
    };

    $scope.cboFINFreight_Options = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true, minLength: 3,
        dataTextField: 'ValueOfVar', dataValueField: 'ID', index: 0,
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
        change: function () {
            var cboval = $scope.ItemFilter.StatusID;
            $timeout(function () {
                $scope.FINFreightAuditCus_gridOptions.dataSource.data([]);
                if (cboval == _FINFreightAuditCUS.Data._defStatusID)
                    $scope.IsDefaultStatus = true;
                else
                    $scope.IsDefaultStatus = false;

                if (cboval == _FINFreightAuditCUS.Data._approvedStatusID)
                    $scope.IsApprovedStatus = true;
                else
                    $scope.IsApprovedStatus = false;
            }, 10);
        }
    };
    
    // Lấy danh sách status
    Common.Services.Call($http, {
        url: Common.Services.url.FIN,
        method: _FINFreightAuditCUS.URL.StatusList,
        success: function (res) {
            if (Common.HasValue(res)) {
                $scope.cboFINFreight_Options.dataSource.data(res);
                _FINFreightAuditCUS.Data._dataStatus = res;
                _FINFreightAuditCUS.Data._defStatusID = res[0].ID;
                _FINFreightAuditCUS.Data._approvedStatusID = res[res.length - 1].ID;
                $scope.Init_LoadCookie();
            }
        }
    });

    $scope.Search_Click = function ($event) {
        $event.preventDefault();
        Common.Cookie.Set(_FINFreightAuditCUS.Data.CookieSearch, JSON.stringify($scope.ItemFilter));

        $scope.FINFreightAuditCus_gridOptions.dataSource.read();
        $scope.HasChoose = false;
    };

    $scope.Init_LoadCookie = function () {
        Common.Log('Init_LoadCookie');
        var strCookie = Common.Cookie.Get(_FINFreightAuditCUS.Data.CookieSearch);
        var statusID = -1;
        if (Common.HasValue(strCookie) && strCookie != '') {
            try {
                var objCookie = eval('[' + strCookie + ']')[0];
                $scope.ItemFilter.DateFrom = Common.Date.FromJson(objCookie.DateFrom);
                $scope.ItemFilter.DateTo = Common.Date.FromJson(objCookie.DateTo);
                statusID = objCookie.StatusID;
                if (statusID != _FINFreightAuditCUS.Data._defStatusID)
                    $scope.HasShowItem = false;
                else
                    $scope.HasShowItem = true;
            } catch (e) { }
        }
        if (!Common.HasValue($scope.ItemFilter.DateFrom) || !Common.HasValue($scope.ItemFilter.StatusID) || !Common.HasValue($scope.ItemFilter.DateTo)) {
            $scope.HasShowItem = true;
            $scope.ItemFilter.DateFrom = new Date().addDays(-2);
            $scope.ItemFilter.DateTo = new Date().addDays(0);
            statusID = _FINFreightAuditCUS.Data._defStatusID;
        }

        if (statusID == -1)
            statusID = _FINFreightAuditCUS.Data._defStatusID;

        $timeout(function () {
            $scope.ItemFilter.StatusID = statusID;
            $scope.FINFreightAuditCus_gridOptions.dataSource.read();
        }, 10);
    };

    $scope.Window_Close_Click = function ($event, win) {
        $event.preventDefault();
        win.close();
    };

    $scope.Detail_Click = function ($event, grid, win) {
        $event.preventDefault();
        var tr = $($event.target).closest('tr');
        var item = grid.dataItem(tr);
        Common.Services.Call($http, {
            url: Common.Services.url.FIN,
            method: _FINFreightAuditCUS.URL.Detail,
            data: { id: item.id },
            success: function (res) {
                if (Common.HasValue(res)) {
                    $scope.NameFreight = item.OrderCode;
                    $scope.FINFreightAuditCus_Credit_grid.dataSource.data(res.lstCredit);
                    $scope.FINFreightAuditCus_Trouble_grid.dataSource.data(res.lstTrouble);
                    win.center().open();
                    $timeout(function () {
                        $scope.FINFreightAuditCus_Credit_grid.resize();
                        $scope.FINFreightAuditCus_Trouble_grid.resize();
                    }, 10);
                }
            }
        });
    };

    $scope.Save_Click = function ($event, win, grid) {
        var lst = [];
        var item = grid.dataSource.data();

        $.each(item, function (i, v) {
            if (v.IsChoose == true) {
                lst.push(v.OrderID);
            }
        });

        Common.Services.Call($http, {
            url: Common.Services.url.FIN,
            method: $scope.Method,
            data: { lst: lst, Note: $scope.Item.Note },
            success: function (res) {
                $scope.FINFreightAuditCus_gridOptions.dataSource.read();
            }
        });

        win.close();
    };

    $scope.Reject_Click = function ($event, win, grid) {
        $event.preventDefault();
        $scope.Method = _FINFreightAuditCUS.URL.Reject;
        $scope.Item.Note = "";

        var lst = [];
        var item = grid.dataSource.data();

        $.each(item, function (i, v) {
            if (v.IsChoose == true) {
                lst.push(v.OrderID);
            }
        });

        if (lst.length > 0) {
            win.center().open();
        }
    };

    $scope.Accept_Click = function ($event, win, grid) {
        $event.preventDefault();
        $scope.Method = _FINFreightAuditCUS.URL.Accept;
        $scope.Item.Note = "";

        $scope.Save_Click($event, win, grid);
    };

    $scope.Approved_Click = function ($event, win, grid) {
        $event.preventDefault();
        $scope.Method = _FINFreightAuditCUS.URL.Approved;
        $scope.Item.Note = "";

        $scope.Save_Click($event, win, grid);
    
    };

    $scope.ShowSetting = function ($event, grid) {
        $event.preventDefault();

        $rootScope.ShowSetting({
            ListView: views.FINFreightAuditCustomer,
            event: $event,
            grid: grid,
            current: $state.current
        });
    };

    $scope.HideSetting = function ($event) {
        $event.preventDefault();

        $rootScope.HideSetting();
    };
}]);