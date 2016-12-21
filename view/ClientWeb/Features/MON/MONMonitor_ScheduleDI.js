/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />
/// <reference path="~/Scripts/map.js" />

var _MONMonitor_ScheduleDI = {
    URL: {
        List: "DTOMONMonitorScheduleDI_List",
        Accept: "DTOMONMonitorScheduleDI_Accept",
    },
    Data: {
        _dataVehicle: null,

    },
}

angular.module('myapp').controller('MONMonitor_ScheduleDICtrl', ['$rootScope', '$scope', '$http', '$location', '$state', '$timeout', 'openMap', 'signalRHubProxy', function ($rootScope, $scope, $http, $location, $state, $timeout, openMap, signalRHubProxy) {
    Common.Log('MONMonitor_ScheduleDICtrl');
    $rootScope.IsLoading = false;
    Common.ClientHub.Registry('ditoMasterChangeStatus', function (data) {
        var grid = $scope.grid;
        var lst = grid.tbody.find('tr');
        $.each(lst, function (i, tr) {
            var item = grid.dataItem(tr);
            if (item.StatusReferID == data.id) {
                var monstatus = '';
                var monstatusname = '';
                switch (data.statusid) {
                    case 1: monstatus = 'open'; monstatusname = ''; break;
                    case 2: monstatus = 'reject'; monstatusname = 'từ chối lệnh'; break;
                    case 3: monstatus = 'get'; monstatusname = 'nhận lệnh'; break;
                    case 4: monstatus = 'accept'; monstatusname = 'chấp nhận lệnh'; break;
                    case 5: monstatus = 'complete'; monstatusname = 'hoàn tất lệnh'; break;
                }
                $rootScope.Message({ Msg: 'Xe ' + item.VehicleCode + '[' + item.TOMasterCode + '] đã ' + monstatusname });
                var current = $(tr).find('i.fa.monstatus');
                $(current).removeClass('open').removeClass('reject').removeClass('get').removeClass('accept').removeClass('complete');
                $(current).addClass(monstatus);
                if (data.statusid == 3) {
                    item.StatusID = 3;
                    $(tr).find('span.DriverNameGet').html(data.drivername);
                    var td = $(current[0]).closest('td');
                    $(td).find('a.monstatusspan').hide();
                    $(td).find('a.monstatuslink').show();
                }
                else {
                    item.StatusID = data.statusid;
                    $(tr).find('span.DriverNameGet').html('');
                    var td = $(current[0]).closest('td');
                    $(td).find('a.monstatusspan').show();
                    $(td).find('a.monstatuslink').hide();
                }
            }
        });
    });

    $scope.gridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.MON,
            method: _MONMonitor_ScheduleDI.URL.List,
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    ETD: { type: 'date' },
                    ETA: { type: 'date' },
                }
            },
            sort: [{ field: 'ETD', dir: 'desc' }],
            pageSize: 30
        }),
        height: '99%', pageable: true, sortable: true, columnMenu: false,
        filterable: { mode: 'row' }, resizable: false, reorderable: true,
        columns: [
            {
                width: '30px', title: 'TT', filterable: false, sortable: false,
                template: '<a><i class="fa monstatusto"></i></a>',
            },
            {
                width: '30px', title: 'TX', filterable: false, sortable: false,
                template: '<a class="monstatusspan" ng-show="dataItem.StatusID!=3"><i class="fa monstatus"></i></a><a class="monstatuslink" ng-show="dataItem.StatusID==3" href="/" ng-click="gridStatus_Click($event,dataItem)"><i class="fa monstatus"></i></a>',
            },
            {
                field: 'TOMasterCode', width: '120px', title: 'Lệnh',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'DriverName', width: '150px', title: 'Tài xế',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'DriverNameGet', width: '150px', title: 'Tài xế nhận', template: '<span class="DriverNameGet">#=DriverNameGet#</span>',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'AssistantName', width: '150px', title: 'Phụ lái',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'VehicleCode', width: '150px', title: 'Số xe',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'VendorOfVehicleName', width: '150px', title: 'Nhà vận tải',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'ETD', width: '120px', title: 'Ngày lấy hàng', template: "#=ETD==null?' ':kendo.toString(ETD, '" + Common.Date.Format.DMYHM + "')#",
                filterable: { cell: { template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); }, operator: 'equal', showOperators: false } },
            },
            {
                field: 'ETA', width: '120px', title: 'Ngày giao hàng', template: "#=ETD==null?' ':kendo.toString(ETA, '" + Common.Date.Format.DMYHM + "')#",
                filterable: { cell: { template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); }, operator: 'equal', showOperators: false } },
            },
            { title: '', filterable: false, sortable: false }
        ],
        dataBound: function (e) {
            var grid = this;
            if (Common.HasValue(grid) && Common.HasValue(grid.tbody)) {
                var lst = grid.tbody.find('tr');
                $.each(lst, function (i, tr) {
                    var item = grid.dataItem(tr);
                    var monstatus = '';
                    switch (item.StatusID) {
                        case 1: monstatus = 'open'; break;
                        case 2: monstatus = 'reject'; break;
                        case 3: monstatus = 'get'; break;
                        case 4: monstatus = 'accept'; break;
                        case 5: monstatus = 'complete'; break;
                    }
                    $(tr).find('i.fa.monstatus').addClass(monstatus);
                    var monstatusto = '';
                    switch (item.StatusOfDITOMasterID) {
                        case 1: monstatusto = 'waiting'; break;
                        case 2: monstatusto = 'running'; break;
                        case 3: monstatusto = 'complete'; break;
                    }
                    $(tr).find('i.fa.monstatusto').addClass(monstatusto);
                })
            }
        }
    };

    $scope.gridStatus_Click = function ($event, item) {
        $event.preventDefault();
        $rootScope.Message({
            Type: Common.Message.Type.Confirm,
            Msg: "Bạn chấp nhận lệnh này ?",
            Ok: function () {
                $rootScope.IsLoading = true;
                Common.Services.Call($http, {
                    url: Common.Services.url.MON,
                    method: _MONMonitor_ScheduleDI.URL.Accept,
                    data: { 'id': item.ID, 'statusreferid': item.StatusReferID },
                    success: function (res) {
                        $rootScope.IsLoading = false;
                        $scope.gridOptions.dataSource.read();
                    },
                    error: function (res) {
                        $rootScope.IsLoading = false;
                    }
                });
            }
        });
    };
    $scope.OPSAppointment_CO_BackClick = function ($event) {
        $event.preventDefault();
        $state.go("main.MONMonitor.Index");
    }
}]);