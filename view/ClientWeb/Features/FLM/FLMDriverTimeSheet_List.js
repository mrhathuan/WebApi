
/// <reference path="~/Scripts/jquery-1.9.1.intellisense.js" />
/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />

var _FLMDriverTimeSheet_List = {
    URL: {
        VehicleList: 'FLMDriverTimeSheet_VehicleList',
        VehicleTimeList: 'FLMDriverTimeSheet_VehicleTimeList',
        VehicleTimeGet: 'FLMDriverTimeSheet_VehicleTimeGet',
        VehicleTimeSave: 'FLMDriverTimeSheet_VehicleTimeSave',
        VehicleTimeDelete: 'FLMDriverTimeSheet_VehicleTimeDelete',
    },
    Data: {
        
        _itemBackUp: null,
        _itemNew: null,
        _dataTypeOfActivity: [],
        _dataCostType: [],
        _dataVehicle: [],
        _vehicleListSelect: [],
        _currentTimeline: [],
        _typeID: null,
        _assetID: -1,
        _assetNo: '',
        _typeOfActivityName: [],
        _activityRepeat: [],
        CookieMaintenance: 'FLMDriverTimeSheet',
    },
    Timeline: null
}

angular.module('myapp').controller('FLMDriverTimeSheet_ListCtrl', ['$rootScope', '$scope', '$http', '$location', '$state', '$timeout', function ($rootScope, $scope, $http, $location, $state, $timeout) {
    Common.Log('FLMDriverTimeSheet_ListCtrl');
    $rootScope.IsLoading = false;

    $scope.ShowSetting = function ($event, grid) {
        $event.preventDefault();

        $rootScope.ShowSetting({
            ListView: views.FLMDriverTimeSheet,
            event: $event,
            current: $state.current
        });
    };

    $scope.HasChoose = false;
    $scope.IsEdit = false;
    $scope.ItemEdit = null;
    $scope.IsShowDelete = false;

    $scope.Item = null;
    $scope.Search = {};
    $scope.Search.IsShowConfig = false;

    $scope.FLMDriverTimeSheet_TabOptions = {
        animation: { open: { effects: "fadeIn" } },
    };

    $scope.FLMDriverTimeSheet_splitterOptions = {
        orientation: "vertical",
        panes: [
            { collapsible: false, resizable: false, size: '25px' },
            { collapsible: false, resizable: true },
        ],
    };

    $scope.FLMDriverTimeSheet_Driver_gridOptions = {
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    Birthday: { type: 'date' }
                }
            },
            pageSize: 0
        }),
        height: '99%', pageable: true, sortable: true, columnMenu: false, filterable: false, resizable: true, editable: false,
        columns: [
            { field: "EmployeeCode", title: 'Mã tài xế', width: 100, filterable: { cell: { showOperators: false, operator: "contains" } } },
            { field: 'LastName', title: 'Họ', width: 100, filterable: { cell: { showOperators: false, operator: "contains" } } },
            { field: 'FirstName', title: 'Tên', width: 100, filterable: { cell: { showOperators: false, operator: "contains" } } },
            {
                field: 'Birthday', title: 'Ngày sinh', template: "#=Birthday==null?' ':Common.Date.FromJsonDMY(Birthday)#", width: 120,
                filterable: { cell: { showOperators: false, operator: "gte" } }
            },
            { field: 'Cellphone', title: 'Điện thoại', width: 100, filterable: { cell: { showOperators: false, operator: "contains" } } },
            { field: 'CardNo', title: 'CMND', width: 100, filterable: { cell: { showOperators: false, operator: "contains" } } },
            { field: 'Note', title: 'Ghi chú', width: 100, filterable: { cell: { showOperators: false, operator: "contains" } } },
            { title: '', sortable: false, menu: false, filterable: false }
        ]
    }


    $scope.Init_LoadCookie = function () {
        Common.Log('Init_LoadCookie');

        var strCookie = Common.Cookie.Get(_FLMDriverTimeSheet.Data.CookieMaintenance);
        if (Common.HasValue(strCookie) && strCookie != '') {
            try {
                var objCookie = eval('[' + strCookie + ']')[0];
                $scope.Search.DateFrom = Common.Date.FromJson(objCookie.DateFrom);
                $scope.Search.DateTo = Common.Date.FromJson(objCookie.DateTo);
                $scope.Search.HourFrom = Common.Date.FromJson(objCookie.HourFrom);
                $scope.Search.HourTo = Common.Date.FromJson(objCookie.HourTo);
                $scope.Search.RouteInDay = objCookie.RouteInDay;
                $scope.Search.isTrk = objCookie.isTrk;
                $scope.Search.isTrr = objCookie.isTrr;
                $scope.Search.isCo = objCookie.isCo;
                $scope.Search.isRmc = objCookie.isRmc;
            } catch (e) { }
        }
        if (!Common.HasValue($scope.Search.DateFrom) || !Common.HasValue($scope.Search.DateTo)) {
            $scope.Search.DateFrom = new Date().addDays(-2);
            $scope.Search.DateTo = new Date().addDays(2);
            $scope.Search.HourFrom = new Date(2015, 1, 1, 6, 0);
            $scope.Search.HourTo = new Date(2015, 1, 1, 18, 0);
            $scope.Search.RouteInDay = 3;
            $scope.Search.isTrk = true;
            $scope.Search.isTrr = true;
            $scope.Search.isCo = true;
            $scope.Search.isRmc = true;
        }
    };

    $scope.Init_LoadCookie();

    $scope.LoadLabel = function () {
        Common.Log("LoadLabel");
        $scope.Search.ConfigString = '';
        var lst = _FLMDriverTimeSheet.Timeline.GetListRouteInDay();
        var str = '';
        $.each(lst, function (i, v) {
            str += '[' + v.Name + ']:' + Common.Date.ToString(v.HourFrom, Common.Date.Format.HM) + '-' + Common.Date.ToString(v.HourTo, Common.Date.Format.HM) + '&nbsp;&nbsp;';
        });

        if (str != '')
            str = 'Ngày: ' + Common.Date.ToString($scope.Search.DateFrom, Common.Date.Format.DDMMYY) + ' - ' +
                Common.Date.ToString($scope.Search.DateTo, Common.Date.Format.DDMMYY) + '&nbsp;&nbsp;&nbsp;' + str;
        else
            str = 'Ngày: ' + Common.Date.ToString($scope.Search.DateFrom, Common.Date.Format.DDMMYY) + '&nbsp;&nbsp;&nbsp;' +
                Common.Date.ToString($scope.Search.HourFrom, Common.Date.Format.HM) + ' - ' + Common.Date.ToString($scope.Search.HourTo, Common.Date.Format.HM);
        $scope.Search.ConfigString = str;
    }

    $timeout(function () {
        _FLMDriverTimeSheet.Timeline = Common.Timeline({
            grid: $scope.FLMDriverTimeSheet_grid,
            model: {
                id: 'ID',
                fields:
                    {
                        ID: { type: 'number', editable: false },
                        RegNo: { type: 'string' },
                        TypeOfVehicleName: { type: 'string', editable: false }
                    }
            },
            modelGroup: [],
            modelSort: { field: 'ID', dir: 'asc' },
            columns: [
                { field: 'RegNo', width: '90px', title: 'Xe', template: '<div class="bgtruck allowdrop"><span class="fa fa-truck"></span>&nbsp;#=RegNo#</div>', sortable: true, locked: true },
                { field: 'TypeOfVehicleName', width: '70px', template: '<div class="allowdrop">#=TypeOfVehicleName==null?"":TypeOfVehicleName#</div>', title: 'Loại xe', sortable: true, locked: true }
            ],
            search: $scope.Search,
            eventMainData: function () {
                Common.Log('VehicleMainData');

                Common.Services.Call($http, {
                    url: Common.Services.url.FLM,
                    method: _FLMDriverTimeSheet.URL.VehicleList,
                    data: {},
                    success: function (res) {
                        Common.Services.Error(res, function (res) {
                            _FLMDriverTimeSheet.Data._dataVehicle = [];
                            $.each(res, function (i, v) {
                                _FLMDriverTimeSheet.Data._dataVehicle.push(v);
                            });
                            _FLMDriverTimeSheet.Timeline.SetMainData(res);
                            $scope.LoadLabel();
                        });
                    }
                });
            },
            eventDetailData: function (dtFrom, dtTo) {
                Common.Log('VehicleDetailData');

                _FLMDriverTimeSheet.Data._vehicleListSelect = [];

                var lst = _FLMDriverTimeSheet.Timeline.GetListRouteInDay();
                var str = '';
                $.each(lst, function (i, v) {
                    str += '[' + v.Name + ']:' + Common.Date.ToString(v.HourFrom, Common.Date.Format.HM) + '-' + Common.Date.ToString(v.HourTo, Common.Date.Format.HM) + '&nbsp;&nbsp;';
                });
                if (str != '')
                    str = 'Ngày: ' + Common.Date.ToString($scope.Search.DateFrom, Common.Date.Format.DDMMYY) + ' - ' +
                        Common.Date.ToString($scope.Search.DateTo, Common.Date.Format.DDMMYY) + '&nbsp;&nbsp;&nbsp;' + str;
                else
                    str = 'Ngày: ' + Common.Date.ToString($scope.Search.DateFrom, Common.Date.Format.DDMMYY) + '&nbsp;&nbsp;&nbsp;' +
                        Common.Date.ToString($scope.Search.HourFrom, Common.Date.Format.HM) + ' - ' + Common.Date.ToString($scope.Search.HourTo, Common.Date.Format.HM);

                $scope.Search.ConfigString = str;

                var param = Common.Request.Create({
                    Sorts: [], Filters: [
                        Common.Request.FilterParamWithAnd('DateFrom', Common.Request.FilterType.GreaterThanOrEqual, dtFrom),
                        Common.Request.FilterParamWithAnd('DateFrom', Common.Request.FilterType.LessThanOrEqual, dtTo)
                    ]
                });

                Common.Services.Call($http, {
                    url: Common.Services.url.FLM,
                    method: _FLMDriverTimeSheet.URL.VehicleTimeList,
                    data: { request: param },
                    success: function (res) {
                        Common.Services.Error(res, function (res) {
                            $.each(res, function (i, v) {
                                v.DateFrom = Common.Date.FromJson(v.DateFrom);
                                v.DateTo = Common.Date.FromJson(v.DateTo);
                            });

                            _FLMDriverTimeSheet.Timeline.SetDetailData(res);
                        });
                    }
                });
            },
            eventClickTime: function (id, item, typeid) {
                Common.Log('VehicleClickTime');
                $scope.IsShowDelete = true;
                _FLMDriverTimeSheet.Data._currentTimeline = item;
                _FLMDriverTimeSheet.Data._typeID = typeid;

               // if (typeid < 0) {
                    Common.Services.Call($http, {
                        url: Common.Services.url.FLM,
                        method: _FLMDriverTimeSheet.URL.VehicleTimeGet,
                        data: { actID: id },
                        success: function (res) {
                            Common.Services.Error(res, function (res) {
                                $timeout(function () {
                                    $scope.FLMDriverTimeSheet_Driver_grid.dataSource.data(res.ListDriver)
                                    $scope.Item = res;
                                    $scope.FLMDriverTimeSheet_win.center().open();
                                }, 10);
                            });
                        }
                    });
                //}
            },
            eventSelect: function (lst) {
                _FLMDriverTimeSheet.Data._assetID = lst[0].AssetID;
                _FLMDriverTimeSheet.Data._assetNo = lst[0].RegNo;
            }
        });
        _FLMDriverTimeSheet.Timeline.Init();
    }, 500);

    $scope.Config_Click = function ($event) {
        Common.Log('Config_Click');
        if (Common.HasValue($event))
            $event.preventDefault();

        _FLMDriverTimeSheet.Data._assetID = -1;
        _FLMDriverTimeSheet.Data._assetNO = '';
        $scope.Search.IsShowConfig = !$scope.Search.IsShowConfig;

        if (!$scope.Search.IsShowConfig) {
            // Load lại data
            _FLMDriverTimeSheet.Timeline.ChangeTime({
                search: {
                    DateFrom: $scope.Search.DateFrom,
                    DateTo: $scope.Search.DateTo,
                    HourFrom: $scope.Search.HourFrom,
                    HourTo: $scope.Search.HourTo
                }
            });
            _FLMDriverTimeSheet.Timeline.RefreshMain();
            // Set cookie
            Common.Cookie.Set(_FLMDriverTimeSheet.Data.CookieMaintenance, JSON.stringify($scope.Search));
            // Resize
            $scope.FLMDriverTimeSheet_splitter.size(".k-pane:first", "25px");
        }
        else {
            $scope.FLMDriverTimeSheet_splitter.size(".k-pane:first", "100px");
        }
        $scope.LoadLabel();
    };

    $scope.Close_Click = function ($event, win) {
        Common.Log("Close_Click");
        $event.preventDefault();

        win.close();
    };

}]);

