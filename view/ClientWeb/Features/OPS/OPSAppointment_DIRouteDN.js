/// <reference path="~/Scripts/jquery-1.9.1.intellisense.js" />
/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />

//#region URL
var _OPSAppointment_DIRouteDN = {
    URL: {
        CustomerList: 'OPS_DIAppointment_Route_CustomerList',
        Read: 'OPS_DIAppointment_RouteDN_OrderList',
        OrderDNCodeChange: 'OPS_DIAppointment_RouteDN_OrderDNCodeChange',
        Delete: 'OPS_DIAppointment_RouteDN_Delete',
        Revert: 'OPS_DIAppointment_RouteDN_Revert',
    },
    Data: {
        CookieSearch: 'OPS_Appointment_DIRouteDN_Search',
        dataStatus: [{ ID: 1, Name: 'Chưa lập lệnh' }, { ID: 2, Name: 'Đã lập lệnh' }, {ID: 3, Name: 'Đã hủy' }],
        gridModel: {
            id: 'GenID',
            fields: {
                GenID: { type: 'string', editable: false },
                ID: { type: 'number' },
                CustomerCode: { type: 'string' },
                CustomerName: { type: 'string' },
                OrderCode: { type: 'string' },
                SOCode: { type: 'string' },
                DNCode: { type: 'string' },
                ProvinceName: { type: 'string' },
                DistrictName: { type: 'string' },
                Address: { type: 'string' },
                Ton: { type: 'number' },
                Kg: { type: 'number' },
                CBM: { type: 'number' },
                RequestDate: { type: 'date' },
                DateDN: { type: 'date' },
                ETD: { type: 'date' },

                TranferItem: { type: 'string' },
                PartnerCode: { type: 'string' },
                PartnerName: { type: 'string' },
                FromCode: { type: 'string' },
                FromAddress: { type: 'string' },
                FromProvince: { type: 'string' },
                FromDistrict: { type: 'string' },
                Description: { type: 'string' },
                CUSRoutingCode: { type: 'string' },
                TOMasterCode: { type: 'string' },

                Note1: { type: 'string' },
                Note2: { type: 'string' },
                IsChoose: { type: 'bool', defaultValue: false },
            }
        }
    }
};
//endregion URL
angular.module('myapp').controller('OPSAppointment_DIRouteDNCtrl', ['$rootScope', '$scope', '$http', '$location', '$state', '$timeout', function ($rootScope, $scope, $http, $location, $state, $timeout) {
    if ($rootScope.IsPageComplete != true) return;
    Common.Log('OPSAppointment_DIRouteDNCtrl');
    $rootScope.IsLoading = false;

    $scope.HasChoose = false;

    $scope.Search = {};
    $scope.Search.StatusID = 1;
    $scope.Search.ListCustomerID = [];

    $scope.OPSAppointment_DIRouteDN_gridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.OPS,
            method: _OPSAppointment_DIRouteDN.URL.Read,

            model: _OPSAppointment_DIRouteDN.Data.gridModel,
            readparam: function () {
                return {
                    DateFrom: $scope.Search.DateFrom,
                    DateTo: $scope.Search.DateTo,
                    lstCustomerID: $scope.Search.ListCustomerID,
                    statusID: $scope.Search.StatusID
                }
            },
            pageSize: 20,
        }),
        height: '99%', pageable: true, sortable: { mode: 'multiple' }, groupable: false, columnMenu: false, resizable: true, reorderable: true, filterable: { mode: 'row' }, selectable: 'multiple',
        columns: [
                {
                    field: 'Command', title: ' ', width: '35px',
                    headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,OPSAppointment_DIRouteDN_grid,OPSAppointment_DIRouteDN_gridChoose_Change)" />',
                    headerAttributes: { style: 'text-align: center;' }, attributes: { style: 'text-align: center;' },
                    template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,OPSAppointment_DIRouteDN_grid,OPSAppointment_DIRouteDN_gridChoose_Change)" />',
                    filterable: false, sortable: false, sortorder: 0, configurable: false, isfunctionalHidden: false
                },
            {
                field: 'CustomerCode', width: '100px', title: 'Mã k.hàng', filterable: { cell: { operator: 'contains', showOperators: false } },
                sortorder: 1, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'CustomerName', width: '150px', title: 'K.hàng', filterable: { cell: { operator: 'contains', showOperators: false } },
                sortorder: 2, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'OrderCode', width: '150px', title: 'Đ.hàng', filterable: { cell: { operator: 'contains', showOperators: false } },
                sortorder: 3, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'UserDefine1', width: '150px', title: 'Định nghĩa 1', template: '<form class="cus-form-enter" ng-submit="DNEnter_Click($event)"><input type="text" class="k-textbox" style="width: 100%" ng-model="dataItem.UserDefine1"/></form>', filterable: { cell: { operator: 'contains', showOperators: false } },
                sortorder: 4, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'SOCode', width: '150px', title: 'Số SO', template: '<form class="cus-form-enter" ng-submit="DNEnter_Click($event)"><input type="text" class="k-textbox" style="width: 100%" ng-model="dataItem.SOCode" /></form>', filterable: { cell: { operator: 'contains', showOperators: false } },
                sortorder: 5, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'DNCode', width: '150px', template: '<form class="cus-form-enter" ng-submit="DNEnter_Click($event)"><input type="text" class="k-textbox" style="width: 100%" ng-model="dataItem.DNCode" /></form>', title: 'Số DN', filterable: { cell: { operator: 'contains', showOperators: false } },
                sortorder: 6, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'Ton', width: '120px', title: 'Tấn', template: "#=Common.Number.ToNumber5(Ton)#", filterable: { cell: { operator: 'eq', showOperators: false } },
                sortorder: 7, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'CBM', width: '120px', title: 'Khối', template: "#=Common.Number.ToNumber5(CBM)#", filterable: { cell: { operator: 'eq', showOperators: false } },
                sortorder: 8, configurable: true, isfunctionalHidden: false
            },
                { field: 'TOMasterCode', width: '120px', title: 'Số chuyến', filterable: false, sortorder: 9, configurable: true, isfunctionalHidden: false },
                { field: 'VehicleCode', width: '120px', title: 'Số xe', filterable: false, sortorder: 10, configurable: true, isfunctionalHidden: false },
                { field: 'DriverName', width: '120px', title: 'Tài xế', filterable: false, sortorder: 11, configurable: true, isfunctionalHidden: false },
                { field: 'DriverTelNo', width: '120px', title: 'SĐT', filterable: false, sortorder: 12, configurable: true, isfunctionalHidden: false },
            {
                field: 'Kg', width: '120px', title: 'KG (G.Weight)', template: '# if(IsOrigin){# <form class="cus-form-enter" ng-submit="DNEnter_Click($event)"><input type="number" step="0.01" min="0" class="k-textbox" data-k-max="dataItem.Kg" ng-model="dataItem.Kg" style="width:100%"/></form>#}else{# #:Kg# #}#', filterable: { cell: { operator: 'eq', showOperators: false } },
                sortorder: 13, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'Note2', width: '80px', title: 'Số lít', template: '<form class="cus-form-enter" ng-submit="DNEnter_Click($event)"><input type="text" class="k-textbox" ng-model="dataItem.Note2" style="width:100%"/></form>', filterable: { cell: { operator: 'contains', showOperators: false } },
                sortorder: 14, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'Note1', width: '80px', title: 'Số kg', template: '<form class="cus-form-enter" ng-submit="DNEnter_Click($event)"><input type="text" class="k-textbox" ng-model="dataItem.Note1" style="width:100%"/></form>', filterable: { cell: { operator: 'contains', showOperators: false } },
                sortorder: 15, configurable: true, isfunctionalHidden: false
            },
                {
                    field: 'DateDN', width: 120, title: 'Ngày ra DN', sortable: false, filterable: { cell: { operator: 'eq', showOperators: false } },
                    template: '<input class="dtpDateDN" name="DateDN" value="#=DateDN!=null?DateDN:\"\"#" style="width: 100%;" ></input>',
                    sortorder: 16, configurable: true, isfunctionalHidden: false
                },
            {
                field: 'TranferItem', width: '100px', title: 'Vận chuyển', filterable: { cell: { operator: 'contains', showOperators: false } },
                sortorder: 17, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'ProvinceName', width: '110px', title: 'Tỉnh / Thành', filterable: { cell: { operator: 'contains', showOperators: false } },
                sortorder: 18, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'DistrictName', width: '110px', title: 'Quận / Huyện', filterable: { cell: { operator: 'contains', showOperators: false } },
                sortorder: 19, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'Address', width: '150px', title: 'Địa chỉ', filterable: { cell: { operator: 'contains', showOperators: false } },
                sortorder: 20, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'RequestDate', width: '100px', title: 'Ngày gửi', template: "#=Common.Date.FromJsonDDMMHM(RequestDate)#", filterable: { cell: { operator: 'eq', showOperators: false } },
                sortorder: 21, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'ETD', width: '100px', title: 'ETD', template: "#=Common.Date.FromJsonDDMMHM(ETD)#", filterable: { cell: { operator: 'eq', showOperators: false } },
                sortorder: 22, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'PartnerCode', width: '100px', title: 'Mã NPP', filterable: { cell: { operator: 'contains', showOperators: false } },
                sortorder: 23, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'PartnerName', width: '100px', title: 'NPP', filterable: { cell: { operator: 'contains', showOperators: false } },
                sortorder: 24, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'FromCode', width: '100px', title: 'Mã l.hàng', filterable: { cell: { operator: 'contains', showOperators: false } },
                sortorder: 25, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'FromAddress', width: '150px', title: 'Điểm l.hàng', filterable: { cell: { operator: 'contains', showOperators: false } },
                sortorder: 26, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'FromProvince', width: '100px', title: 'Tỉnh thành l.hàng', filterable: { cell: { operator: 'contains', showOperators: false } },
                sortorder: 27, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'FromDistrict', width: '100px', title: 'Quận huyện l.hàng', filterable: { cell: { operator: 'contains', showOperators: false } },
                sortorder: 28, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'Description', width: '100px', title: 'Ghi chú', filterable: { cell: { operator: 'contains', showOperators: false } },
                sortorder: 29, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'CUSRoutingCode', width: '180px', title: 'Cung đường', filterable: { cell: { operator: 'contains', showOperators: false } },
                sortorder: 30, configurable: true, isfunctionalHidden: false
            },
                { title: ' ', filterable: false, menu: false, sortable: false, sortorder: 100, configurable: false, isfunctionalHidden: false }
        ],
        change: function (e) {
        },
        dataBound: function (e) {
            $.each($(this.wrapper).find('.dtpDateDN'), function (idx, item) {
                var time = $scope.OPSAppointment_DIRouteDN_grid.dataItem($(item).closest('tr')).DateDN;
                $(item).kendoDatePicker({
                    format: 'dd/MM/yyyy',
                    parseFormats: ["yyyy-MM-ddTHH:mm:ss"],
                    value: time,
                    change: function (e) {
                        var val = this.value();
                        var dataItem = $scope.OPSAppointment_DIRouteDN_grid.dataItem($(item).closest('tr'));
                        if (dataItem) {
                            dataItem.DateDN = val;
                        }
                    }
                })
            });
        }
    };

    $scope.OPSAppointment_DIRouteDN_mltCustomerOptions = {
        autoBind: true,
        valuePrimitive: true,
        dataTextField: 'CustomerName',
        dataValueField: 'ID',
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    CustomerName: { type: 'string' }
                }
            }
        }),
    };

    $scope.OPSAppointment_DIRouteDN_cboStatusOptions = {
        autoBind: true,
        valuePrimitive: true,
        dataTextField: 'Name',
        dataValueField: 'ID',
        dataSource: Common.DataSource.Local({
            data: _OPSAppointment_DIRouteDN.Data.dataStatus,
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    Name: { type: 'string' }
                }
            }
        }),
    };

    // Load danh sách khách hàng
    Common.Services.Call($http, {
        url: Common.Services.url.OPS,
        method: _OPSAppointment_DIRouteDN.URL.CustomerList,
        data: {},
        success: function (res) {
            if (Common.HasValue(res)) {
                $timeout(function () {
                    $scope.OPSAppointment_DIRouteDN_mltCustomerOptions.dataSource.data(res);
                }, 1);
            }
        }
    });

    $scope.LoadLabel = function () {
        $scope.Search.ConfigString = '';
        var str = 'Ngày: ' + Common.Date.ToString($scope.Search.DateFrom, Common.Date.Format.DDMMYY) + ' - ' +
        Common.Date.ToString($scope.Search.DateTo, Common.Date.Format.DDMMYY) + '&nbsp;&nbsp;&nbsp;';
        if (Common.HasValue($scope.OPSAppointment_DIRouteDN_mltCustomer)) {
            var lstCUS = $scope.OPSAppointment_DIRouteDN_mltCustomer.dataItems();
            if (lstCUS.length > 0) {
                str += 'Khách hàng: ';
                var strCus = '';
                $.each(lstCUS, function (i, v) {
                    strCus += ',' + v.Code;
                });
                str += strCus.substr(1);
            }
        }
        $scope.Search.ConfigString = str;
    }

    $scope.Init_LoadCookie = function () {
        Common.Log('Init_LoadCookie');

        var strCookie = Common.Cookie.Get(_OPSAppointment_DIRouteDN.Data.CookieSearch);
        if (Common.HasValue(strCookie) && strCookie != '') {
            try {
                var objCookie = eval('[' + strCookie + ']')[0];
                $scope.Search.DateFrom = Common.Date.FromJson(objCookie.DateFrom);
                $scope.Search.DateTo = Common.Date.FromJson(objCookie.DateTo);
                $scope.Search.StatusID = Common.HasValue(objCookie.StatusID) ? objCookie.StatusID : 1;
            } catch (e) { }
        }
        if (!Common.HasValue($scope.Search.DateFrom) || !Common.HasValue($scope.Search.DateTo)) {
            $scope.Search.DateFrom = new Date().addDays(-2);
            $scope.Search.DateTo = new Date().addDays(2);
            $scope.Search.StatusID = 1;
        }

        $scope.LoadLabel();
    };

    $scope.Init_LoadCookie();

    $scope.Close_Click = function ($event) {
        $event.preventDefault();

        $state.go('main.OPSAppointment.DI');
    };

    $scope.CancelView_Click = function ($event) {

        $scope.OPSAppointment_DIRouteDN_gridOptions.dataSource.read();
    };

    $scope.Search_Click = function ($event) {
        Common.Log('Search_Click');
        $event.preventDefault();

        $scope.OPSAppointment_DIRouteDN_grid.dataSource.read();
        Common.Cookie.Set(_OPSAppointment_DIRouteDN.Data.CookieSearch, JSON.stringify($scope.Search));
    };

    $scope.Del_Click = function ($event, grid) {
        $event.preventDefault();

        var lstid = [];
        var data = grid.dataSource.data();
        $.each(data, function (i, v) {
            if (v.IsChoose == true)
                lstid.push(v.ID);
        });
        if (lstid.length > 0) {
            $rootScope.Message({
                Type: Common.Message.Type.Confirm,
                Msg: 'Bạn muốn hủy các DN đã chọn ?',
                pars: { lstid: lstid },
                Ok: function (pars) {
                    Common.Services.Call($http, {
                        url: Common.Services.url.OPS,
                        method: _OPSAppointment_DIRouteDN.URL.Delete,
                        data: pars,
                        success: function (res) {
                            $scope.OPSAppointment_DIRouteDN_grid.dataSource.read();
                            $rootScope.Message({ Msg: 'Đã hủy', NotifyType: Common.Message.NotifyType.ERROR });
                        }
                    });
                }
            });
        }
    };

    $scope.Revert_Click = function ($event, grid) {
        $event.preventDefault();

        var lstid = [];
        var data = grid.dataSource.data();
        $.each(data, function (i, v) {
            if (v.IsChoose == true)
                lstid.push(v.ID);
        });
        if (lstid.length > 0) {
            $rootScope.Message({
                Type: Common.Message.Type.Confirm,
                Msg: 'Bạn muốn phục hồi các DN đã chọn ?',
                pars: { lstid: lstid },
                Ok: function (pars) {
                    Common.Services.Call($http, {
                        url: Common.Services.url.OPS,
                        method: _OPSAppointment_DIRouteDN.URL.Revert,
                        data: pars,
                        success: function (res) {
                            $scope.OPSAppointment_DIRouteDN_grid.dataSource.read();
                            $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.ERROR });
                        }
                    });
                }
            });
        }
    };

    $scope.OrderDNEnter_Click = function ($event) {
        $event.preventDefault();


    };

    $scope.DNEnter_Click = function ($event) {
        Common.Log('DNEnter_Click');
        if (Common.HasValue($event))
            $event.preventDefault();

        if (Common.HasValue(this.dataItem)) {
            var item = this.dataItem;
            if (item.Kg >= 0) {
                item.Ton = item.Kg / 1000;

                var data = $scope.OPSAppointment_DIRouteDN_gridOptions.dataSource.data();
                var str = '';
                
                if (item.DNCode != '' && item.DNCode != null) {
                    $.each(data, function (i, v) {
                        if (v.ID > 0 && v.ID != item.ID && v.DNCode == item.DNCode)
                            str += ', ' + v.SOCode;
                    });
                }
                if (str != '') {
                    $rootScope.Message({
                        Type: Common.Message.Type.Confirm,
                        Msg: 'Các SO: ' + str.substr(1) + ' đã có số DN này. Bạn có muốn tiếp tục lưu ?',
                        pars: { item: item },
                        Ok: function (pars) {
                            Common.Services.Call($http, {
                                url: Common.Services.url.OPS,
                                method: _OPSAppointment_DIRouteDN.URL.OrderDNCodeChange,
                                data: pars,
                                success: function (res) {
                                    Common.Services.Error(res, function (res) {
                                        $scope.OPSAppointment_DIRouteDN_grid.dataSource.read();
                                        $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                                    });
                                }
                            });
                        }
                    });
                }
                else {
                    Common.Services.Call($http, {
                        url: Common.Services.url.OPS,
                        method: _OPSAppointment_DIRouteDN.URL.OrderDNCodeChange,
                        data: { item: item },
                        success: function (res) {
                            Common.Services.Error(res, function (res) {
                                $scope.OPSAppointment_DIRouteDN_grid.dataSource.read();
                                $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                            });
                        }
                    });
                }
            } else {
                $rootScope.Message({ Msg: 'Số Kg không được âm', NotifyType: Common.Message.NotifyType.ERROR });
            }
        }
    };

    $scope.Barcode_Click = function ($event) {
        $event.preventDefault();

        $state.go('main.OPSAppointment.DIRouteBarcode');
    };

    $scope.OPSAppointment_DIRouteDN_gridChoose_Change = function ($event, grid, haschoose) {
        $scope.HasChoose = haschoose;
    };

    // Reorder columns
    $timeout(function () {
        Common.Log("ReorderColumns");
        Common.Controls.Grid.ReorderColumns({ Grid: $scope.OPSAppointment_DIRouteDN_grid, CookieName: _OPSAppointment_DIRouteDN.Data.CookieGrid });
    }, 100);

    //#region Action
    $scope.ShowSetting = function ($event, grid) {
        $event.preventDefault();

        $rootScope.ShowSetting({
            ListView: views.OPSAppointment,
            event: $event,
            grid: grid,
            current: $state.current
        });
    };
}]);