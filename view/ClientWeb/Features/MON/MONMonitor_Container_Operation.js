/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />
/// <reference path="~/Scripts/map.js" />

angular.module('myapp').controller('MONMonitor_Container_OperationCtrl', ['$rootScope', '$scope', '$http', '$location', '$state', '$timeout', function ($rootScope, $scope, $http, $location, $state, $timeout) {
    if ($rootScope.IsPageComplete != true) return;
    Common.Log('MONMonitor_Container_OperationCtrl');
    $rootScope.IsLoading = false;
    $scope.Auth = $rootScope.GetAuth();


    //#region Main Grid

    $scope.Main_gridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.MON,
            method: "MONCO_COTORead",
            readparam: function () {
                return {};
            },
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number', editable: false, nullable: false },
                    ApprovedDate: { type: 'date', editable: false },
                    Ton: { type: 'number', editable: false },
                    IsChoose: { type: 'bool', defaultValue: false },
                    MasterCode: { type: 'string', editable: false },
                    RegNo: { type: 'string', editable: false },
                    KMGPS: { type: 'string', editable: false },
                    Ton: { type: 'number', editable: false },
                }
            },
        }),
        selectable: true, reorderable: true, editable: true, pageable: Common.PageSize,
        height: '100%', sortable: true, columnMenu: false, filterable: { mode: 'row' }, resizable: true,
        columns: [
            {
                title: ' ', width: '40px', field: '',
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,Main_grid,Main_gridChooseChange)" />',
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,Main_grid,Main_gridChooseChange)" />',
                filterable: false, sortable: false
            },
            { field: 'StatusName', width: 120, title: 'Trạng thái', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'RegNo', width: 100, title: 'Số xe', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'MasterCode', width: 100, title: 'Mã chuyến', template: '<a ng-click="LoadMasterDetail($event,dataItem)" title="Xem chi tiết" style="cursor:pointer;font-weight: bold;">{{dataItem.MasterCode}}</a>', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'KM', width: 100, title: 'KM', filterable: false, },
            { field: 'KMGPS', width: 100, title: 'KMGPS', filterable: false, },
            { field: 'Ton', width: 150, title: '{{RS.CATRouting.ParentName}}', filterable: { cell: { operator: 'eq', showOperators: false } } },
            { field: 'LocationFromCode', width: 120, title: 'Mã điểm đi', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'LocationFromName', width: 120, title: 'Điểm đi', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'LocationFromAddress', width: 120, title: 'Địa chỉ đi', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'LocationToCode', width: 120, title: 'Mã điểm đến', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'LocationToName', width: 120, title: 'Điểm đến', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'LocationToAddress', width: 120, title: 'Địa chỉ đến', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'ApprovedBy', width: 120, title: 'Người duyệt', filterable: { cell: { operator: 'contains', showOperators: false } } },
            {
                field: 'ApprovedDate', width: 120, title: 'Ngày duyệt', template: "#=ApprovedDate==null?' ':Common.Date.FromJsonDMYHM(ApprovedDate)#",
                filterable: false,
            },
            { title: ' ', field: '', sortable: false, filterable: false, menu: false }
        ],
    }

    $scope.Update_Click = function ($event, type) {
        $event.preventDefault();
        var data = $scope.Main_grid.dataSource.data();
        var datasend = [];
        Common.Data.Each(data, function (o) {
            if (o.IsChoose) datasend.push(o);
        })

        var message = 'Xác nhận lưu tất cả ?';
        if (type == 1) message = 'Xác nhận duyệt ?';
        else if (type == 2) message = 'Xác nhận hủy duyệt ?';

        if (datasend.length > 0) {
            $rootScope.Message({
                Type: Common.Message.Type.Confirm,
                Msg: message,
                pars: {},
                Ok: function (pars) {
                    $rootScope.IsLoading = true;
                    Common.Services.Call($http, {
                        url: Common.Services.url.MON,
                        method: "MONCO_COTOUpdateList",
                        data: { lst: datasend, type: type },
                        success: function (res) {
                            $rootScope.IsLoading = false;
                            $scope.Main_grid.dataSource.read();
                            $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                        }
                    });
                }
            });

        }
    }


    //#endregion

    //#region popup
    $scope.masterID=0;
    $scope.COTOMasterGridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.MON,
            method: "MONCO_COTOByMaster",
            readparam: function () {
                return {
                    id: $scope.masterID,
                };
            },
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number', editable: false, nullable: false },
                    ApprovedDate: { type: 'date', editable: false },
                    Ton: { type: 'number', editable: false },
                    IsChoose: { type: 'bool', defaultValue: false },
                    MasterCode: { type: 'string', editable: false },
                    RegNo: { type: 'string', editable: false },
                    KMGPS: { type: 'string', editable: false },
                    Ton: { type: 'number', editable: false },
                }
            },
        }),
        selectable: true, reorderable: true, editable: true, pageable: Common.PageSize, autoBind: false,
        height: '100%', sortable: true, columnMenu: false, filterable: { mode: 'row' }, resizable: true,
        columns: [
            {
                title: ' ', width: '40px', field: '',
                template: '<a class="k-button" ng-click="COTORemove($event,dataItem)"><i class="fa fa-trash"></i></a>',
                filterable: false, sortable: false
            },
            { field: 'StatusName', width: 120, title: 'Trạng thái', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'RegNo', width: 100, title: 'Số xe', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'MasterCode', width: 100, title: 'Mã chuyến', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'KM', width: 100, title: 'KM', filterable: false, },
            { field: 'KMGPS', width: 100, title: 'KMGPS', filterable: false, },
            { field: 'Ton', width: 150, title: '{{RS.CATRouting.ParentName}}', filterable: { cell: { operator: 'eq', showOperators: false } } },
            { field: 'LocationFromCode', width: 120, title: 'Mã điểm đi', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'LocationFromName', width: 120, title: 'Điểm đi', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'LocationFromAddress', width: 120, title: 'Địa chỉ đi', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'LocationToCode', width: 120, title: 'Mã điểm đến', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'LocationToName', width: 120, title: 'Điểm đến', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'LocationToAddress', width: 120, title: 'Địa chỉ đến', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'ApprovedBy', width: 120, title: 'Người duyệt', filterable: { cell: { operator: 'contains', showOperators: false } } },
            {
                field: 'ApprovedDate', width: 120, title: 'Ngày duyệt', template: "#=ApprovedDate==null?' ':Common.Date.FromJsonDMYHM(ApprovedDate)#",
                filterable: false,
            },
            { title: ' ', field: '', sortable: false, filterable: false, menu: false }
        ],
    }

    $scope.LoadMasterDetail = function (e, item) {
        e.preventDefault();
        $scope.masterID = item.MasterID;
        $scope.COTOMasterGrid.dataSource.read();
        $scope.COTOWin.center().open();
    }

    $scope.COTOAdd = function (e) {
        e.preventDefault();
        $scope.COTO_EditWin.center().open();
    }

    $scope.COTOAdd_Accept = function (e) {
        e.preventDefault();

        $rootScope.Message({
            Type: Common.Message.Type.Confirm,
            Msg: message,
            pars: {},
            Ok: function (pars) {
                $rootScope.IsLoading = true;
                Common.Services.Call($http, {
                    url: Common.Services.url.MON,
                    method: "MONCO_COTOofMasterAdd",
                    data: { item: $scope.ItemT1, masterID: $scope.masterID },
                    success: function (res) {
                        $rootScope.IsLoading = false;
                        $scope.Main_grid.dataSource.read();
                        $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                    }
                });
            }
        });
    }

    $scope.COTORemove = function ($event,item) {
        $event.preventDefault();

        if (item.ID > 0) {
            $rootScope.Message({
                Type: Common.Message.Type.Confirm,
                Msg: "Xác nhận xóa chặng đã chọn",
                pars: {},
                Ok: function (pars) {
                    $rootScope.IsLoading = true;
                    Common.Services.Call($http, {
                        url: Common.Services.url.MON,
                        method: "MONCO_COTOofMasterRomove",
                        data: { id: item.ID },
                        success: function (res) {
                            $rootScope.IsLoading = false;
                            $scope.Main_grid.dataSource.read();
                            $scope.COTOMasterGrid.dataSource.read();
                            $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                        }
                    });
                }
            });

        }
    }
    //#endregion

    $scope.Win_Close = function (e, win) {
        e.preventDefault();
        win.close();
    }
}]);