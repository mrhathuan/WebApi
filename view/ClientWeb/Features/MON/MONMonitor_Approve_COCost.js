/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />
/// <reference path="~/Scripts/map.js" />

angular.module('myapp').controller('MONMonitor_Approve_COCostCtrl', ['$rootScope', '$scope', '$http', '$location', '$state', '$timeout', function ($rootScope, $scope, $http, $location, $state, $timeout) {
    if ($rootScope.IsPageComplete != true) return;
    Common.Log('MONMonitor_Approve_COCostCtrl');
    $rootScope.IsLoading = false;
    $scope.Auth = $rootScope.GetAuth();

    $scope.ItemSearch = {
        DateFrom: (new Date()).addDays(-7),
        DateTo: (new Date()),
    }

    //#region toolbar

    $scope.Back_Click = function ($event, win) {
        $event.preventDefault();
        $state.go("main.MONMonitor.Index")
    };

    $scope.ReloadClick = function ($event) {
        $event.preventDefault();
        if (!Common.HasValue($scope.ItemSearch.DateFrom) || !Common.HasValue($scope.ItemSearch.DateTo)) {
            $rootScope.Message({
                Type: Common.Message.Type.Alert,
                NotifyType: Common.Message.NotifyType.ERROR,
                Title: 'Thông báo',
                Msg: 'Vui lòng chọn Từ ngày- Đến ngày chính xác',
                Close: null,
                Ok: null
            })
        }
        else if ($scope.ItemSearch.DateFrom > $scope.ItemSearch.DateTo) {
            $rootScope.Message({
                Type: Common.Message.Type.Alert,
                NotifyType: Common.Message.NotifyType.ERROR,
                Title: 'Thông báo',
                Msg: 'Vui lòng chọn Từ ngày nhỏ hơn Đến ngày',
                Close: null,
                Ok: null
            })
        }
        else {
            $scope.trouble_grid.dataSource.read();
        }
    };

    //#endregion
   
    //#region Chi phi phat sinh

    $scope.trouble_gridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.MON,
            method: "MONCO_TroubleList",
            readparam: function () {
                return {
                    'DateFrom': $scope.ItemSearch.DateFrom,
                    'DateTo': $scope.ItemSearch.DateTo,
                };
            },
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number', editable: false, nullable: false },
                    ETD: { type: 'date' },
                    ETA: { type: 'date' },
                    IsChoose: { type: 'bool', defaultValue: false },
                }
            },
        }),
        selectable: true, reorderable: true, editable: false, pageable: Common.PageSize,
        height: '100%', sortable: true, columnMenu: false, filterable: { mode: 'row' }, resizable: true,
        columns: [
            {
                title: ' ', width: '40px', field: '',
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,trouble_grid,trouble_gridChooseChange)" />',
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,trouble_grid,trouble_gridChooseChange)" />',
                filterable: false, sortable: false
            },
            { field: 'RegNo', width: 100, title: 'Số xe', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'MasterCode', width: 100, title: 'Mã chuyến', template: '<a ng-click="LoadMasterDetail($event,dataItem.COTOMasterID)" title="Xem chi tiết" style="cursor:pointer;font-weight: bold;">{{dataItem.MasterCode}}</a>', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'ETD', width: 100, title: 'ETD', filterable: false, template: '#=Common.Date.FromJsonDMYHM(ETD)#', },
            { field: 'ETA', width: 100, title: 'ETA', filterable: false, template: '#=Common.Date.FromJsonDMYHM(ETA)#', },
            { field: 'GroupOfTroubleName', width: 150, title: '{{RS.CATRouting.ParentName}}', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'Description', width: "150px", title: '{{RS.SYSGroup.Description}}', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'Cost', width: 100, title: '{{RS.CATCost.CostValue}}', filterable: false },
            { field: 'CostOfCustomer', width: 100, title: '{{RS.CATTrouble.CostOfCustomer}}', filterable: false },
            { field: 'CostOfVendor', width: 100, title: '{{RS.CATTrouble.CostOfVendor}}', filterable: false },
            { field: 'TroubleCostStatusName', width: 130, title: '{{RS.SYSVar1.ValueOfVar}}', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'ApprovedBy', width: 120, title: 'Người duyệt', filterable: { cell: { operator: 'contains', showOperators: false } } },
            {
                field: 'ApprovedDate', width: 120, title: 'Ngày duyệt', template: "#=ApprovedDate==null?' ':Common.Date.FromJsonDMYHM(ApprovedDate)#",
                filterable: false,
            },
            { title: ' ', field: '', sortable: false, filterable: false, menu: false }
        ],
    }

    $scope.troubleCost_Approve = function ($event, grid) {
        $event.preventDefault();
        var data = grid.dataSource.data();
        var datasend = [];
        Common.Data.Each(data, function (o) {
            if (o.IsChoose) datasend.push(o.ID);
        })
        if (datasend.length > 0) {
            $rootScope.Message({
                Type: Common.Message.Type.Confirm,
                Msg: 'Xác nhận duyệt chi phí ?',
                pars: {},
                Ok: function (pars) {
                    $rootScope.IsLoading = true;
                    Common.Services.Call($http, {
                        url: Common.Services.url.MON,
                        method: "MON_ApprovedTrouble",
                        data: { lst: datasend },
                        success: function (res) {
                            $rootScope.IsLoading = false;
                            $scope.trouble_grid.dataSource.read();
                            $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                        }
                    });
                }
            });

        }
    }

    $scope.troubleCost_Revert = function ($event, grid) {
        $event.preventDefault();
        var data = grid.dataSource.data();
        var datasend = [];
        Common.Data.Each(data, function (o) {
            if (o.IsChoose) datasend.push(o.ID);
        })
        if (datasend.length > 0) {
            $rootScope.Message({
                Type: Common.Message.Type.Confirm,
                Msg: 'Xác nhận hủy duyệt chi phí ?',
                pars: {},
                Ok: function (pars) {
                    $rootScope.IsLoading = true;
                    Common.Services.Call($http, {
                        url: Common.Services.url.MON,
                        method: "MON_RevertApprovedTrouble",
                        data: { lst: datasend },
                        success: function (res) {
                            $rootScope.IsLoading = false;
                            $scope.trouble_grid.dataSource.read();
                            $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                        }
                    });
                }
            });

        }
    }

    //#endregion

    //#region Thông tin chuyến

    $scope.Show_DriverGrid = false;
    $scope.CurrentMaster = {};
    $scope.masterID = 0;
    var LoadingStep = 20;

    $scope.TO_WinOptions = {
        width: '1025px', height: '550px',
        draggable: true, modal: true, resizable: false, title: false,
        open: function () {
            $timeout(function () {
                $rootScope.Loading.Show();
                $scope.TODetail_ReloadAllGrid();
                $scope.TO_Splitter.resize();
            }, 100)
        },
        close: function () {
        }
    };

    $scope.TO_SplitterOptions = {
        orientation: 'vertical',
        panes: [
            { collapsible: false, resizable: false, size: '150px' },
            { collapsible: false, resizable: false, },
        ]
    };

    $scope.TO_TabstripOptions = {
        height: "100%", animation: { open: { effects: "fadeIn" } },
    };

    $scope.TODetail_ReloadAllGrid = function () {
        $scope.Trouble_Grid.dataSource.read();
        $scope.Station_Grid.dataSource.read();
        $scope.Location_Grid.dataSource.read();
        $scope.COContainer_Grid.dataSource.read();
        $scope.TOContainer_Grid.dataSource.read();
    }

    $scope.LoadMasterDetail = function (e, masterID) {
        e.preventDefault();
        $rootScope.Loading.Change("Thông tin vận chuyển...", 0);
        $rootScope.Loading.Show();
        Common.Services.Call($http, {
            url: Common.Services.url.MON,
            method: "MONControlTowerCO_MasterGet",
            data: { id: masterID },
            success: function (res) {
                $rootScope.Loading.Change("Thông tin vận chuyển...", 100);
                $scope.TO_Win.center().open();
                $scope.CurrentMaster = res;
                $scope.masterID = res.ID;
                $scope.TO_Win_Title = 'Thông tin chi tiết. Chuyến ' + $scope.CurrentMaster.Code + ' - ETD: ' + $scope.CurrentMaster.ETD + ' ETA: ' + $scope.CurrentMaster.ETA;
                if ($scope.CurrentMaster.IsVehicleVendor) {
                    $scope.Show_DriverGrid = true;
                } else {
                    $scope.Show_DriverGrid = false;
                }

            }
        });   
    }

    //grid

    $scope.Trouble_GridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.MON,
            method: "MONControlTowerCO_TroubleList",
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' }, RoutingName: { type: 'string' },
                    GroupOfTroubleName: { type: 'string', editable: false },
                    COTOID: { type: 'number', editable: true },
                    TroubleCostStatusID: { type: 'number', editable: false },
                    RoutingName: { type: 'string' },
                    Cost: { type: 'number', defaultValue: 0 },
                    CostOfCustomer: { type: 'number', defaultValue: 0 },
                    CostOfVendor: { type: 'number', defaultValue: 0 },
                    TroubleCostStatusName: { type: 'string' },

                }
            },
            readparam: function () { return { masterID: $scope.masterID } }
        }),
        height: '99%', pageable: false, sortable: false, columnMenu: false, filterable: false, resizable: false, editable: 'incell', columns: [
           { field: 'GroupOfTroubleName', width: 150, title: '{{RS.CATRouting.ParentName}}',},
           { field: 'Description', title: '{{RS.SYSGroup.Description}}', width: "150px", },
           { field: 'Cost', width: 100, title: '{{RS.CATCost.CostValue}}'},
           { field: 'CostOfCustomer', width: 100, title: '{{RS.CATTrouble.CostOfCustomer}}', },
           { field: 'CostOfVendor', width: 100, title: '{{RS.CATTrouble.CostOfVendor}}',},
           { field: 'TroubleCostStatusID', width: 130, title: '{{RS.SYSVar1.ValueOfVar}}', template: '#=TroubleCostStatusName#',},
           { title: ' ', filterable: false, sortable: false }
        ],
        dataBound: function () {
            $rootScope.Loading.Change("Thông tin cung đường...", $rootScope.Loading.Progress + LoadingStep);
        }
    };

    $scope.Station_GridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.MON,
            method: "MONControlTower_COStationList",
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    IsChoose: { type: 'bool' },
                    LocationName: { editable: false },
                    Address: { editable: false },
                    KM: { editable: false },
                    Price: { editable: false },
                    Note: { editable: false },
                    IsApproved: { type: 'bool', editable: false },
                    DateCome: { type: 'date' }
                }
            },
            readparam: function () { return { masterID: $scope.masterID } }
        }),
        height: '99%', pageable: false, sortable: false, columnMenu: false, editable: false, resizable: false, filterable: { mode: 'row' },
        columns: [
           {
               field: 'LocationName', width: 170, title: '{{RS.CATLocation.Location}}', filterable: { cell: { operator: 'contains', showOperators: false } }
           },
           {
               field: 'Address', width: 170, title: '{{RS.CATLocation.Address}}', filterable: { cell: { operator: 'contains', showOperators: false } }
           },
           {
               field: 'KM', width: 80, title: '{{RS.OPSDITOStation.KM}}', filterable: { cell: { operator: 'contains', showOperators: false } }
           },
           {
               field: 'Price', width: 100, title: '{{RS.OPSDITOStation.Price}}', filterable: { cell: { operator: 'contains', showOperators: false } }
           },
           {
               field: 'Note', width: 150, title: '{{RS.OPSDITOStation.Note}}', filterable: { cell: { operator: 'contains', showOperators: false } }
           },
           { title: ' ', filterable: false, sortable: false }
        ],
        dataBound: function () {
            $rootScope.Loading.Change("Thông tin trạm...", $rootScope.Loading.Progress + LoadingStep);
        }
    };

    $scope.Location_GridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.MON,
            method: "MONControlTowerCO_MasterLocation",
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    SortOrderReal: { type: 'number' },
                    SortOrder: { type: 'number', editable: false },
                    LocationAddress: { type: 'text', editable: false },
                    LocationProvince: { type: 'text', editable: false },
                    LocationDistrict: { type: 'text', editable: false },
                    DITOLocationStatusName: { type: 'text', editable: false },
                    Comment: { type: 'text', editable: false },
                    DateComeEstimate: { type: 'date', editable: false },
                    DateLeaveEstimate: { type: 'date', editable: false },
                    DateCome: { type: 'date' },
                    DateLeave: { type: 'date' },
                }
            },
            sort: [{ field: "SortOrder", dir: "asc" }],
            readparam: function () { return { masterID: $scope.masterID } }
        }),
        height: '99%', pageable: false, sortable: true, columnMenu: false, filterable: false, resizable: true, editable: false,
        dataBound: function (e) {
            $rootScope.Loading.Change("Thông tin địa điểm...", $rootScope.Loading.Progress + LoadingStep);
        },
        columns: [
           { title: 'STT', field: 'SortOrder', width: "40px", },
           {
               field: 'SortOrderReal', width: 80, title: 'STT thực',
           },
           {
               field: 'TypeOfTOLocationName', width: 100, title: '{{RS.TypeOfTOLocationName}}',
           },
           {
               field: 'DITOLocationStatusName', width: 100, title: '{{RS.MONMonitorIndex.WarningCount}}',
           },
           {
               field: 'LocationCode', width: 120, title: '{{RS.CATLocation.Code}}',
           },
           {
               field: 'LocationName', width: 150, title: '{{RS.CATLocation.Location}}',
           },
           {
               field: 'LocationAddress', width: 200, title: '{{RS.CATLocation.Address}}',
           },
           {
               field: 'DateCome', width: 170, title: '{{RS.FLMAssetTimeSheet.DateTo}}', template: '#=Common.Date.FromJsonDMYHM(DateCome)#',
               editor: function (container, options) {
                   var input = $("<input kendo-date-time-picker k-options='DateDMYHM'/>");
                   input.attr("name", options.field);
                   input.appendTo(container);
               }
           },
           {
               field: 'DateLeave', width: 170, title: '{{RS.FLMAssetTimeSheet.DateLeave}}', template: '#=Common.Date.FromJsonDMYHM(DateLeave)#',
               editor: function (container, options) {
                   var input = $("<input kendo-date-time-picker k-options='DateDMYHM'/>");
                   input.attr("name", options.field);
                   input.appendTo(container);
               }
           },
           {
               field: 'DateComeEstimate', width: 120, title: '{{RS.FLMAssetTimeSheet.DateComeEstimate}}', template: '#=Common.Date.FromJsonDMYHM(DateComeEstimate)#',
           },
           {
               field: 'DateLeaveEstimate', width: 120, title: '{{RS.FLMAssetTimeSheet.DateLeaveEstimate}}', template: '#=Common.Date.FromJsonDMYHM(DateLeaveEstimate)#',
           },
           {
               field: 'Comment', width: 300, title: '{{RS.OPSDITOLocation.Comment}}',
           },
           { title: ' ', filterable: false, sortable: false }
        ],
    };

    $scope.COContainer_GridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.MON,
            method: "MONControlTowerCO_COList",
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    IsChoose: { type: 'bool' },
                    OrderCode: { editable: false },
                    PackingCode: { editable: false },
                    LocationFromCode: { editable: false },
                    ReasonChangeName: { editable: false },
                    ReasonChangeNote: { editable: false },
                }
            },
            readparam: function () { return { masterID: $scope.masterID } }
        }),
        height: '99%', pageable: false, sortable: false, columnMenu: false, filterable: false, resizable: true, editable: false,
        columns: [
            { field: 'OrderCode', width: 150, title: '{{RS.ORDOrder.Code}}', },
            { field: 'PackingCode', width: 150, title: '{{RS.CATPacking.Code}}', },
            { field: 'ContainerNo', width: 150, title: '{{RS.ORDOrder.ContainerNo}}', },
            { field: 'Ton', width: 150, title: '{{RS.ORDContainer.Ton}}', },
            { field: 'SealNo1', width: 150, title: '{{RS.OPSContainer.SealNo1}}', },
            { field: 'SealNo2', width: 150, title: '{{RS.OPSContainer.SealNo2}}', },
            { field: 'DepotCode', width: 150, title: 'RS.MONMonitorIndex.DepotCode', },
            { field: 'DepotAddress', width: 150, title: 'RS.MONMonitorIndex.DepotAddress', },
            { field: 'DepotReturnCode', width: 150, title: 'RS.MONMonitorIndex.DepotReturnCode', },
            { field: 'DepotReturnAddress', width: 150, title: 'RS.MONMonitorIndex.DepotReturnAddress', },
            { field: 'ReasonChangeName', width: 150, title: '{{RS.CATReason1.ReasonName}}', },
            { field: 'ReasonChangeNote', width: 150, title: '{{RS.CATReason.ReasonChangeNote}}', },
            { title: ' ', filterable: false, sortable: false }
        ],
        dataBound: function () {
            $rootScope.Loading.Change("Thông tin đơn hàng...", $rootScope.Loading.Progress + LoadingStep);
        }
    };

    $scope.TOContainer_GridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.MON,
            method: "MONControlTowerCO_TOContainerList",
            readparam: function () {
                return {
                    masterID: $scope.masterID,
                };
            },
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    MasterID: { type: 'number' },
                    RequestDate: { type: 'date' },
                    ETD: { type: 'date' },
                    ETA: { type: 'date' },
                    ETARequest: { type: 'date' },
                    ColorClass: { type: 'string' },
                    Quantity: { type: 'string' },
                    IsChoose: { type: 'bool' },
                    IsAvailable: { type: 'bool' },
                    STT: { type: 'number' },
                }
            },
        }),
        height: '99%', pageable: false, sortable: true, columnMenu: false, selectable: true,
        filterable: { mode: 'row' }, resizable: true, reorderable: true,
        columns: [
            {
                field: 'COTOSort', width: 60, sortable: false, title: 'STT',
                filterable: { cell: { operator: 'eq', showOperators: false } }
            },
            {
                field: 'OrderCode', width: 100,
                title: '{{RS.ORDOrder.Code}}',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'ContainerNo', width: 100,
                title: '{{RS.ORDOrder.ContainerNo}}',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'RomoocNo', width: 100,
                title: '{{RS.OPSCOTOMaster.RomoocNo}}',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'ServiceOfOrder', width: 100,
                title: '{{RS.ORDOrder.ServiceOfOrderName}}',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'StatusOfCOContainer', width: 100,
                title: '{{RS.StatusOfCOContainer}}',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'TypeOfContainer', width: 100,
                title: '{{RS.CATPacking.Code}}',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'TypeOfStatusContainerName', width: 100,
                title: '{{RS.SYSVar1.ValueOfVar}}',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'IsAvailable', width: 100,
                title: ' ', template: '#=IsAvailable == true ? "Nhận việc khác" : ""#',
                filterable: false
            },
            {
                field: 'LocationFromAddress', width: 150,
                title: '{{RS.ORDOrder.TextFrom}}',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'LocationToAddress', width: 150,
                title: '{{RS.ORDOrder.TextTo}}',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'ETD', width: 120,
                title: '{{RS.OPSDITOMaster.ETD}}',
                template: '#=Common.Date.FromJsonDMYHM(ETD)#',
                filterable: { cell: { operator: 'gte', showOperators: false } }
            },
            {
                field: 'ETA', width: 120,
                title: '{{RS.OPSDITOMaster.ETA}}',
                template: '#=Common.Date.FromJsonDMYHM(ETA)#',
                filterable: { cell: { operator: 'gte', showOperators: false } }
            },
           { title: ' ', filterable: false, sortable: false }
        ],
        dataBound: function () {
            var grid = this;
            angular.forEach(grid.tbody.find('tr'), function (tr, i) {
                var item = grid.dataItem($(tr));
                if (item.IsRunning) {
                    $(tr).css("background-color", "#F28126");
                    angular.forEach($(tr).find('td'), function (td, i) {
                        $(td).css("color", "#000");
                    })
                }
                if (item.IsCompleteCO) {

                    $(tr).css("background-color", "#73C95F");
                    angular.forEach($(tr).find('td'), function (td, i) {
                        $(td).css("color", "#000");
                    })
                }
            });
            $rootScope.Loading.Change("Thông tin chặng...", $rootScope.Loading.Progress + LoadingStep);
        }
    };
    
    $rootScope.$watch('Loading.Progress', function (n, o) {
        if (n >= 100) {
            $rootScope.Loading.Progress = 0;
            $rootScope.Loading.Hide();
        }
    })
    //#endregion

    $scope.Win_Close = function (e, win) {
        e.preventDefault();
        win.close();
    }
}]);