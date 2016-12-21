/// <reference path="~/Scripts/jquery-1.9.1.intellisense.js" />
/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />

//#region Data
var _FINFreightAuditVendorCON = {
    URL: {
        Read: "FINFreightAuditCON_List",
        Detail: "FINFreightAudit_CONDetail_List",
        Reject: "FINFreightAuditCON_Reject",
        Accept: "FINFreightAuditCON_Accept",
        Approved: "FINFreightAuditCON_Approved",
        Waiting: "FINFreightAuditCON_Waiting",
        StatusList: "FINFreightAuditCus_OrderCON_StatusList",
        FreightAuditExport: 'FIN_FreightAudit_Export',
        ContractTerm_List: 'FINFreightAudit_ContractTerm_List',
        Routing_List: 'FINFreightAudit_Routing_List',
        CONDetail_Save: 'FINFreightAudit_CONDetail_Save',
    },
    Data: {
        CookieSearch: "FINFreightAuditVendorCON_CookieSearch",
        _dataStatus: [],
        _defStatusID: -1,
        _approvedStatusID: -1,
        _waitingStatusID: -1,
    },
}
//#endregion

angular.module('myapp').controller('FINFreightAuditVendorCON_IndexCtrl', ['$rootScope', '$scope', '$http', '$location', '$state', '$timeout', function ($rootScope, $scope, $http, $location, $state, $timeout, $stateParams) {
    if ($rootScope.IsPageComplete != true) return;
    Common.Log('FINFreightAuditVendorCON_IndexCtrl');
    $rootScope.IsLoading = false;
    $scope.ItemFilter = {};
    $scope.HasChoose = false;
    $scope.IsDefaultStatus = false;
    $scope.IsApprovedStatus = false;
    $scope.IsWaitingStatus = false;
    $scope.NoteName = '';

    $scope.ItemContainer = [];
    $scope.ItemFilter.StatusID = -1;
    $scope.ItemFilter.IsOwner = false;
    $scope.ItemFilter.DateFrom = new Date().addDays(-2);
    $scope.ItemFilter.DateTo = new Date().addDays(0);
    $scope.Item = {};
    $scope.Method = '';

    //#region Auth
    $scope.Auth = $rootScope.GetAuth();
    //#endregion

    $scope.FINFreightAuditVendor_gridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.FIN,
            method: _FINFreightAuditVendorCON.URL.Read,
            readparam: function () { return { statusID: $scope.ItemFilter.StatusID, DateFrom: $scope.ItemFilter.DateFrom, DateTo: $scope.ItemFilter.DateTo,IsOwner:$scope.ItemFilter.IsOwner  } },
            model: {
                id: 'TOMasterID',
                fields: {
                    TOMasterID: { type: 'number', nullable: true },
                    Credit: { type: 'number' },
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
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,FINFreightAuditVendor_grid,FINFreightAuditVendor_gridChooseChange)" />',
                headerAttributes: { style: 'text-align: center;' }, attributes: { style: 'text-align: center;' },
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,FINFreightAuditVendor_grid,FINFreightAuditVendor_gridChooseChange)" />',
                filterable: false, sortable: false
            },
            {
                title: ' ', width: '40px',
                template: '<a href="/" ng-click="Detail_Click($event,FINFreightAuditVendor_grid,FINFreightAuditVendor_win)"><i class="fa fa-file-text"></i></a>',
                filterable: false, sortable: false
            },
            {
                field: 'StatusName', title: 'Trạng thái', width: '125px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'TOMasterCode', title: 'Số chuyến', width: '125px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'RegNo', title: 'Số xe', width: '125px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'DriverName', title: 'Tài xế', width: '125px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'VendorName', title: 'Nhà vận tải', width: '125px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'DateConfig', title: 'Ngày', width: '125px',
                template: "#=DateConfig==null?' ':kendo.toString(DateConfig, '" + Common.Date.Format.DMY + "')#",
                filterable: { cell: { template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); }, operator: 'equal', showOperators: false } },
            },
            {
                field: 'Credit', title: 'Doanh thu', width: '125px',
                template: '#=Common.Number.ToMoney(Credit)#', filterable: { cell: { operator: 'eq', showOperators: false } },
            },
            {
                field: 'Debit', title: 'Chi phí', width: '125px',
                template: '#=Common.Number.ToMoney(Debit)#', filterable: { cell: { operator: 'eq', showOperators: false } },
            },
            {
                field: 'PayVendorModified', title: 'Vendor duyệt', width: '150px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'PayVendorNote', title: 'Ghi chú Vendor', width: '200px',
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

    $scope.FINFreightAuditVendor_gridChooseChange = function ($event, grid, haschoose) {
        $scope.HasChoose = haschoose;
    };

    $scope.FINFreightTrouble_gridOptions = {
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number', nullable: true },
                    Cost: { type: 'number' }
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

    $scope.FINFreightAuditVendorCredit_gridOptions = {
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'CostID',
                fields: {
                    CostID: { type: 'number', nullable: true },
                    UnitPrice: { type: 'number' },
                    Cost: { type: 'number' },
                    Quantity: { type: 'number' },
                }
            },
            group: { field: "CostName" }
        }),
        height: '99%', groupable: true, pageable: false, sortable: true, columnMenu: false, resizable: true,
        selectable: 'row', filterable: { mode: 'row' }, reorderable: true,
        columns: [
            {
                field: 'CostName', title: 'Loại phí', width: '125px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'SOCode', title: 'Số SO', width: '125px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'GroupOfProductNameCUS', title: 'Nhóm hàng chở', width: '125px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'GroupOfProductNameVEN', title: 'Nhóm hàng định nghĩa', width: '125px',
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
        change: function (e) {
            //var cboval = $scope.ItemFilter.StatusID;
            var cboval = e.sender.dataItem().ID;

            $timeout(function () {
                $scope.FINFreightAuditVendor_gridOptions.dataSource.data([]);

            }, 10);

            if (cboval == _FINFreightAuditVendorCON.Data._defStatusID)
                $scope.IsDefaultStatus = true;
            else
                $scope.IsDefaultStatus = false;

            if (cboval == _FINFreightAuditVendorCON.Data._approvedStatusID)
                $scope.IsApprovedStatus = true;
            else
                $scope.IsApprovedStatus = false;

            if (cboval == _FINFreightAuditVendorCON.Data._waitingStatusID)
                $scope.IsWaitingStatus = true;
            else
                $scope.IsWaitingStatus = false;
        }

    };

    $scope.FINFreight_win_tabOptions = {
        animation: {
            open: {
                effects: "fadeIn"
            }
        }
    };

    Common.Services.Call($http, {
        url: Common.Services.url.FIN,
        method: _FINFreightAuditVendorCON.URL.StatusList,
        success: function (res) {
            if (Common.HasValue(res)) {
                $scope.cboFINFreight_Options.dataSource.data(res);
                _FINFreightAuditVendorCON.Data._dataStatus = res;
                _FINFreightAuditVendorCON.Data._defStatusID = res[0].ID;
                _FINFreightAuditVendorCON.Data._approvedStatusID = res[res.length - 2].ID;
                _FINFreightAuditVendorCON.Data._waitingStatusID = res[res.length - 3].ID;
                $scope.Init_LoadCookie();
            }
        }
    });

    $scope.Search_Click = function ($event) {
        $event.preventDefault();
        Common.Cookie.Set(_FINFreightAuditVendorCON.Data.CookieSearch, JSON.stringify($scope.ItemFilter));

        $scope.FINFreightAuditVendor_gridOptions.dataSource.read();
        $scope.HasChoose = false;
    };

    $scope.FreightAudit_Excel = function ($event, grid, win) {
        $event.preventDefault();

        $rootScope.UploadExcel({
            Path: Common.FolderUpload.Import,
            columns: [
                
            ],
            Download: function () {
                Common.Services.Call($http, {
                    url: Common.Services.url.FIN,
                    method: _FINFreightAuditVendorCON.URL.FreightAuditExport,
                    data: { pDateFrom: $scope.ItemFilter.DateFrom, pDateTo: $scope.ItemFilter.DateTo, statusID: $scope.ItemFilter.StatusID },
                    success: function (res) {
                        $rootScope.DownloadFile(res);
                    }
                })
            },
            Upload: function (e, callback) {
                Common.Services.Call($http, {
                    url: Common.Services.url.FIN,
                    method: _FINFreightAuditVendorCON.URL.FreightAuditCheck,
                    data: { file: e.FilePath },
                    success: function (data) {
                        callback(data);
                    }
                })
            },
            Window: win,
            Complete: function (e, data) {
                var check = true;
                for (var i = 0; i < data.length; i++) {
                    if (data[i].ExcelSuccess == false) {
                        check = false;
                        break;
                    }
                }
                if (check)
                    Common.Services.Call($http, {
                        url: Common.Services.url.FIN,
                        method: _FINFreightAuditVendorCON.URL.FreightAuditImport,
                        data: { data: data },
                        success: function (res) {
                            $rootScope.Message({ Msg: "Đã cập nhật!" });
                            grid.dataSource.read();
                        }
                    })
                else {
                    $rootScope.Message({ Msg: 'Có dữ liệu lỗi, không thể lưu', NotifyType: Common.Message.NotifyType.ERROR });
                }
            }
        })
    }

    $scope.Init_LoadCookie = function () {
        Common.Log('Init_LoadCookie');
        var strCookie = Common.Cookie.Get(_FINFreightAuditVendorCON.Data.CookieSearch);
        var statusID = -1;
        if (Common.HasValue(strCookie) && strCookie != '') {
            try {
                var objCookie = eval('[' + strCookie + ']')[0];
                $scope.ItemFilter.DateFrom = Common.Date.FromJson(objCookie.DateFrom);
                $scope.ItemFilter.DateTo = Common.Date.FromJson(objCookie.DateTo);
                statusID = objCookie.StatusID;
                if (statusID != _FINFreightAuditVendorCON.Data._defStatusID)
                    $scope.IsDefaultStatus = false;
                else
                    $scope.IsDefaultStatus = true;
            } catch (e) { }
        }
        if (!Common.HasValue($scope.ItemFilter.DateFrom) || !Common.HasValue($scope.ItemFilter.DateTo)) {
            $scope.IsDefaultStatus = true;
            $scope.ItemFilter.DateFrom = new Date().addDays(-2);
            $scope.ItemFilter.DateTo = new Date().addDays(0);
            statusID = _FINFreightAuditVendorCON.Data._defStatusID;
        }

        if (statusID == -1)
            statusID = _FINFreightAuditVendorCON.Data._defStatusID;

        $timeout(function () {
            $scope.ItemFilter.StatusID = statusID;
            if (statusID == _FINFreightAuditVendorCON.Data._approvedStatusID)
                $scope.IsApprovedStatus = true;
            else
                $scope.IsApprovedStatus = false;

            if (statusID == _FINFreightAuditVendorCON.Data._waitingStatusID)
                $scope.IsWaitingStatus = true;
            else
                $scope.IsWaitingStatus = false;

            $scope.FINFreightAuditVendor_gridOptions.dataSource.read();
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
            method: _FINFreightAuditVendorCON.URL.Detail,
            data: { id: item.id },
            success: function (res) {
                if (Common.HasValue(res)) {
                    $scope.ItemContainer = res;
                    $scope.ItemContainer = item.id;
                    $scope.NameFreight = item.TOMasterCode + " - " + item.RegNo + " - " + kendo.toString(item.DateConfig, Common.Date.Format.DMY);
                    $scope.FINFreightAuditVendorCredit_gridOptions.dataSource.data(res.lstCredit);
                    $scope.FINFreightTrouble_gridOptions.dataSource.data(res.lstTrouble);
                    $scope.LoadDataRouting($scope.cboRoutingOptions, res.CustomerID, res.ContractTermID, res.ContractID);
                    win.center().open();
                    $timeout(function () {
                        $scope.FINFreightAuditVendorCredit_grid.resize();
                        $scope.FINFreightTrouble_grid.resize();
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
                lst.push(v.TOMasterID);
            }
        });

        Common.Services.Call($http, {
            url: Common.Services.url.FIN,
            method: $scope.Method,
            data: { lst: lst, Note: $scope.Item.Note },
            success: function (res) {
                $scope.FINFreightAuditVendor_gridOptions.dataSource.read();
            }
        });

        win.close();
    };

    $scope.Reject_Click = function ($event, win, grid) {
        $event.preventDefault();
        $scope.Method = _FINFreightAuditVendorCON.URL.Reject;
        $scope.Item.Note = "";
        $scope.NoteName = "từ chối";
        var lst = [];
        var item = grid.dataSource.data();

        $.each(item, function (i, v) {
            if (v.IsChoose == true) {
                lst.push(v.TOMasterID);
            }
        });

        if (lst.length > 0) {
            win.center().open();
        }
    };

    $scope.Accept_Click = function ($event, win, grid) {
        $event.preventDefault();
        $scope.Method = _FINFreightAuditVendorCON.URL.Accept;
        $scope.Item.Note = "";
        $scope.NoteName = "đồng ý";

        $scope.Save_Click($event, win, grid);
    };

    $scope.Waiting_Click = function ($event, win, grid) {
        $event.preventDefault();
        $scope.Method = _FINFreightAuditVendorCON.URL.Waiting;
        $scope.Item.Note = "";
        $scope.NoteName = "đồng ý";

        $scope.Save_Click($event, win, grid);
    };

    $scope.Approved_Click = function ($event, win, grid) {
        $event.preventDefault();
        $scope.Method = _FINFreightAuditVendorCON.URL.Approved;
        $scope.Item.Note = "";
        $scope.NoteName = "duyệt";
        $scope.Save_Click($event, win, grid);
    };

    $scope.cboContractTermOptions = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true,
        dataTextField: 'TermName', dataValueField: 'ID', minLength: 3,
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
        change: function (e) {
            $scope.ItemContainer.RoutingID = "";
            $scope.LoadDataRouting($scope.cboRoutingOptions, $scope.ItemContainer.CustomerID, $scope.ItemContainer.ContractTermID, $scope.ItemContainer.ContractID);
        }
    };

    $scope.LoadDataContractTerm = function (cbo, contractID) {
        Common.Services.Call($http, {
            url: Common.Services.url.FIN,
            method: _FINFreightAuditVendorCON.URL.ContractTerm_List,
            data: { contractID: contractID },
            success: function (res) {
                $timeout(function () {
                    cbo.dataSource.data(res);
                }, 1);
            },
        });
    }

    $scope.LoadDataRouting = function (cbo, cusID, termID, contractID) {
        Common.Services.Call($http, {
            url: Common.Services.url.FIN,
            method: _FINFreightAuditVendorCON.URL.Routing_List,
            data: { customerID: cusID, termID: termID, contractID: contractID },
            success: function (res) {
                $timeout(function () {
                    cbo.dataSource.data(res);
                }, 1);
            }
        });
    }

    $scope.cboRoutingOptions = {
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

    $scope.Change_Container_Submit_Click = function ($event, win) {
        $event.preventDefault();

        $rootScope.Message({
            Type: Common.Message.Type.Confirm,
            Msg: 'Xác nhận lưu?',
            Ok: function () {
                $rootScope.IsLoading = true;
                Common.Services.Call($http, {
                    url: Common.Services.url.FIN,
                    method: _FINFreightAuditVendorCON.URL.CONDetail_Save,
                    data: { item: $scope.ItemContainer },
                    success: function (res) {
                        Common.Services.Error(res, function (res) {
                            $rootScope.Message({ Msg: "Đã cập nhật!" });
                            $rootScope.IsLoading = false;
                            $scope.FINFreightAuditVendor_grid.dataSource.read();
                            win.close();
                        })
                    }
                });
            }
        })
    }

    $scope.ShowSetting = function ($event, grid) {
        $event.preventDefault();
        $rootScope.ShowSetting({
            ListView:views.FreightAuditVendor,
            event: $event,
            grid: grid,
            current: $state.current
        });
    };

    $scope.HideSetting = function ($event) {
        $event.preventDefault();

        $rootScope.HideSetting();
    }
}]);