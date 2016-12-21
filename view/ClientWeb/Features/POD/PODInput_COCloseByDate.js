/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />

//#region const

var _PODCOCloseByDate = {
    URL: {
        Read: 'PODCO_CloseDataList',
        Approved: 'PODCO_CloseDataByDate',
    },
}

//#endregion

angular.module('myapp').controller('PODInput_COCloseByDateCtrl', ['$rootScope', '$scope', '$http', '$location', '$state', '$timeout', function ($rootScope, $scope, $http, $location, $state, $timeout) {

    Common.Log('PODInput_COCloseByDateCtrl');
    $rootScope.IsLoading = false;
    $scope.HasChoose = false;
    $scope.PODInputItem = {
        DateFrom: (new Date()).addDays(-7),
        DateTo: (new Date()),
        ListCustomerID: []
    }

    $scope.mts_CustomerOptions = {
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    Code: { type: 'string' },
                    CustomerName: { type: 'string' }
                }
            }
        }),
        valuePrimitive: true, dataTextField: "Code", dataValueField: "ID", placeholder: "Chọn khách hàng...", filter: "contains", ignoreCase: true,
        highlightFirst: true, autoClose: false,
        itemTemplate: '<span>#= Code #</span><span style="float:right;">#= CustomerName #</span>',
        headerTemplate: '<strong><span> Mã khách hàng </span><span style="float:right;"> Tên khách hàng </span></strong>',
        change: function (e) {
        }
    }

    Common.ALL.Get($http, {
        url: Common.ALL.URL.Customer,
        success: function (data) {
            $scope.mts_CustomerOptions.dataSource.data(data)
        }
    })

    $scope.PODInput_SearchClick = function ($event) {
        $event.preventDefault();
        if (!Common.HasValue($scope.PODInputItem.DateFrom) || !Common.HasValue($scope.PODInputItem.DateTo)) {
            $rootScope.Message({
                Type: Common.Message.Type.Alert,
                NotifyType: Common.Message.NotifyType.ERROR,
                Title: 'Thông báo',
                Msg: 'Vui lòng chọn Từ ngày- Đến ngày chính xác',
                Close: null,
                Ok: null
            })
        }
        else if ($scope.PODInputItem.DateFrom > $scope.PODInputItem.DateTo) {
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
            $scope.PODInput_gridOptions.dataSource.read();
        }
    }

    $scope.PODInput_gridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.POD,
            method: _PODCOCloseByDate.URL.Read,
            readparam: function () {
                return {
                    'dtFrom': $scope.PODInputItem.DateFrom,
                    'dtTo': $scope.PODInputItem.DateTo,
                    'listCustomerID': $scope.PODInputItem.ListCustomerID,
                };
            },
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number', editable: false, nullable: false },
                    IsOrigin: { type: 'boolean', },
                    IsInvoice: { type: 'boolean', },
                    IsClosed: { type: 'boolean', },
                    IsComplete: { type: 'number', },
                    DITOGroupProductStatusPODID: { type: 'number', },
                    DateFromCome: { type: 'date' },
                    DateFromLeave: { type: 'date' },
                    ReceivedDate: { type: 'date' },
                    DateFromLoadEnd: { type: 'date' },
                    DateFromLoadStart: { type: 'date' },
                    DateToCome: { type: 'date' },
                    DateToLeave: { type: 'date' },
                    DateToLoadEnd: { type: 'date' },
                    DateToLoadStart: { type: 'date' },
                    ETARequest: { type: 'date' },
                    ETD: { type: 'date' },
                    InvoiceDate: { type: 'date' },
                    DatePODContract: { type: 'date' },
                    Ton: { type: 'number', },
                    TonBBGN: { type: 'number', },
                    TonTranfer: { type: 'number', },
                    CBM: { type: 'number', },
                    CBMBBGN: { type: 'number', },
                    CBMTranfer: { type: 'number', },
                    Quantity: { type: 'number', },
                    QuantityBBGN: { type: 'number', },
                    QuantityTranfer: { type: 'number', },
                    KPIOPSDate: { type: 'date' },
                    KPIPODDate: { type: 'date' },
                    IsChoose: { type: 'boolean' }
                }
            },
            pageSize: 20
        }),
        selectable: false, reorderable: false, editable: false,
        height: '100%', pageable: true, sortable: false, columnMenu: false, filterable: { mode: 'row' }, resizable: true,
        columns: [
            {
                field: "F_Cmd", title: ' ', width: '35px',
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,PODInput_grid,PODInput_gridChoose_Change)" />',
                headerAttributes: { style: 'text-align: center;' }, attributes: { style: 'text-align: center;' },
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,PODInput_grid,PODInput_gridChoose_Change)" />',
                filterable: false, sortable: false
            },
            { field: 'Date', width: 100, title: 'Ngày', template: "#=Date == null ? ' ' : Common.Date.FromJsonDMY(Date)#", filterable: false },
            { field: 'NumberOfClosed', width: 150, title: 'Tổng DN đã đóng', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'NumberOfNonClosed', width: 150, title: 'Tông DN chưa đóng', filterable: { cell: { operator: 'contains', showOperators: false } } },

            { title: ' ', field: 'F_Empty', sortable: false, filterable: false, menu: false }
        ],
        dataBound: function (e) {

        }
    }

    $scope.PODInput_gridChoose_Change = function ($event, grid, hasChoose) {
        $scope.HasChoose = hasChoose;
    }

    $scope.PODInput_ApprovedClick = function ($event, grid) {
        $event.preventDefault();
        var data = [];
        Common.Data.Each(grid.dataSource.data(), function (o) {
            if (o.IsChoose) {
                data.push(o.Date);
            }
        })

        if (data.length > 0) {
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.POD,
                method: _PODCOCloseByDate.URL.Approved,
                data: {
                    listCustomerID: $scope.PODInputItem.ListCustomerID,
                    lst: data,
                    isOpen: false,
                },
                success: function (res) {
                    grid.dataSource.read();
                    $rootScope.Message({ NotifyType: Common.Message.NotifyType.SUCCESS, Msg: 'Thành công', })
                    $rootScope.IsLoading = false;
                }
            });
        }
        else $rootScope.Message({ NotifyType: Common.Message.NotifyType.ERROR, Msg: 'chưa chọn dữ liệu', })
    }

    $scope.PODInput_UnApprovedClick = function ($event, grid) {
        $event.preventDefault();
        var data = [];
        Common.Data.Each(grid.dataSource.data(), function (o) {
            if (o.IsChoose) {
                data.push(o.Date);
            }
        })

        if (data.length > 0) {
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.POD,
                method: _PODCOCloseByDate.URL.Approved,
                data: {
                    listCustomerID: $scope.PODInputItem.ListCustomerID,
                    lst: data,
                    isOpen: true,
                },
                success: function (res) {
                    grid.dataSource.read();
                    $rootScope.Message({ NotifyType: Common.Message.NotifyType.SUCCESS, Msg: 'Thành công', })
                    $rootScope.IsLoading = false;
                }
            });
        }
        else $rootScope.Message({ NotifyType: Common.Message.NotifyType.ERROR, Msg: 'chưa chọn dữ liệu', })
    }

    //actions
    $scope.ShowSetting = function ($event, grid) {
        $event.preventDefault();

        $rootScope.ShowSetting({
            ListView: views.PODInput_DIClose,
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