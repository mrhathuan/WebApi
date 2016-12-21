/// <reference path="~/Scripts/jquery-1.9.1.intellisense.js" />
/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />

//#region URL
var _KPIVENTender_Index = {
    URL: {
        VENTender_List: 'KPIVENTenderLTL_List',
        VENTender_Save: 'KPIVENTenderLTL_Save',
        VENTender_Excel: 'KPIVENTenderLTL_Excel',
        VENTender_Generate: 'KPIVENTenderLTL_Generate',
        Vendor_List: 'Vendor_List',
    }
}

//#endregion

angular.module('myapp').controller('KPIVENTenderLTL_IndexCtrl', ['$rootScope', '$scope', '$http', '$location', '$state', '$timeout', '$stateParams', function ($rootScope, $scope, $http, $location, $state, $timeout, $stateParams) {
    if ($rootScope.IsPageComplete != true) return;
    Common.Log('KPIVENTenderLTL_IndexCtrl');
    $rootScope.IsLoading = false;

    $scope.Auth = $rootScope.GetAuth();
    $scope.HasVENTenderSearch = false;
    $scope.ViewItem = {
        VenID: -1, DateFrom: new Date().addDays(-3), DateTo: new Date().addDays(3)
    };

    $scope.kpi_tabstripOptions = {
        animation: {
            open: { effects: "fadeIn" }
        },
        select: function (e) {
            $timeout(function () {
                $scope.TabIndex = angular.element(e.item).data('tabindex');
            }, 1);
        }
    }

    //#region KPIVENTender
    $scope.cboVenTender_VenOptions = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true,
        dataTextField: 'CustomerName', dataValueField: 'ID', minLength: 3,
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    CustomerName: { type: 'string' },
                }
            }
        })
    }


    Common.Services.Call($http, {
        url: Common.Services.url.KPI,
        method: _KPIVENTenderFTL_Index.URL.Vendor_List,
        success: function (res) {
            var data = [{ ID: -1, CustomerName: "Tất cả Vendor" }];
            Common.Data.Each(res.Data, function (o) {
                data.push(o);
                if ($scope.ViewItem.VenID < 1)
                    $scope.ViewItem.VenID = o.ID;
            })
            $timeout(function () {
                $scope.cboVenTender_VenOptions.dataSource.data(data);
            }, 1);
        }
    })

    var kpi_grid = null;

    $scope.kpi_gridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.KPI,
            method: _KPIVENTender_Index.URL.VENTender_List,
            readparam: function () {
                return {
                    venID: $scope.ViewItem.VenID,
                    from: $scope.ViewItem.DateFrom, to: $scope.ViewItem.DateTo
                }
            },
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number', editable: false },
                    DateData: { type: 'date', editable: false },
                    IsKPI: { type: 'boolean', editable: false },
                    KPICode: { type: 'string', editable: false },
                    KPIID: { type: 'number', editable: false },
                    Note: { type: 'string', editable: false },
                    ReasonName: { type: 'string', editable: false },
                    ReasonCode: { type: 'string', editable: false },
                    ReasonID: { type: 'number', editable: false },
                    DIPacketDetailID: { type: 'number', editable: false },
                    TonOrder: { type: 'number', editable: false },
                    CBMOrder: { type: 'number', editable: false },
                    QuantityOrder: { type: 'number', editable: false },
                    TonAccept: { type: 'number', editable: false },
                    CBMAccept: { type: 'number', editable: false },
                    QuantityAccept: { type: 'number', editable: false },
                    TonKPI: { type: 'number', editable: false },
                    CBMKPI: { type: 'number', editable: false },
                    QuantityKPI: { type: 'number', editable: false },
                    VendorCode: { type: 'string', editable: false },
                    VendorName: { type: 'string', editable: false },
                    VendorID: { type: 'number', editable: false },
                }
            }
        }),
        height: '99%', pageable: true, sortable: true, columnMenu: false, resizable: true, reorderable: true, filterable: { mode: 'row' },
        toolbar: kendo.template($('#kpi_gridToolbar').html()),
        columns: [
            {
                field: 'VendorCode', title: 'Mã đối tác', width: '100px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'VendorName', title: 'Tên đối tác', width: '150px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'DateData', title: 'Ngày y/c ĐH', width: '120px', template: '#=Common.Date.FromJsonDMYHM(DateData)#',
                filterable: { cell: { template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); }, operator: 'gte', showOperators: false } },
            },
            {
                field: 'IsKPI', title: 'Đạt KPI', width: '60px',
                template: '<input ng-show="dataItem.IsKPI != null" disabled type="checkbox" #= IsKPI ? "checked=checked" : ""# />', attributes: { style: 'text-align: center;' },
                sortable: true, filterable: false, menu: false
            },
            {
                field: 'KPICode', title: 'Mã KPI', width: '100px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'TonOrder', title: 'Tấn yêu cầu', width: '100px',
                filterable: { cell: { operator: 'equal', showOperators: false } }
            },
            {
                field: 'CBMOrder', title: 'Khối yêu cầu', width: '100px',
                filterable: { cell: { operator: 'equal', showOperators: false } }
            },
            {
                field: 'QuantityOrder', title: 'Số lượng yêu cầu', width: '100px',
                filterable: { cell: { operator: 'equal', showOperators: false } }
            },
            {
                field: 'TonAccept', title: 'Tấn chấp nhận', width: '100px',
                filterable: { cell: { operator: 'equal', showOperators: false } }
            },
            {
                field: 'CBMAccept', title: 'Khối chấp nhận', width: '100px',
                filterable: { cell: { operator: 'equal', showOperators: false } }
            },
            {
                field: 'QuantityAccept', title: 'Số lượng chấp nhận', width: '100px',
                filterable: { cell: { operator: 'equal', showOperators: false } }
            },
            {
                field: 'TonKPI', title: 'Tấn KPI', width: '100px',
                filterable: { cell: { operator: 'equal', showOperators: false } }
            },
            {
                field: 'CBMKPI', title: 'Khối KPI', width: '100px',
                filterable: { cell: { operator: 'equal', showOperators: false } }
            },
            {
                field: 'QuantityKPI', title: 'Số lượng KPI', width: '100px',
                filterable: { cell: { operator: 'equal', showOperators: false } }
            },
            {
                field: 'ReasonName', title: 'Lý do', width: '150px',
                template: '<div style="height:100%;width:100%;"><a style="width: 100%;text-align:left;" href="/" ng-show="dataItem.IsKPI == false" ng-click="VENTenderReason_Click($event,dataItem,ventender_reason_win)" class="k-button">#=ReasonName!=""?ReasonName:"Chọn"#</a></div>',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'Note', title: 'Ghi chú', width: '300px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            { title: ' ', sortable: false, filterable: false, menu: false }
        ]
    };

    $scope.$watch('kpi_grid', function (newValue, oldValue) {
        if (newValue != null)
            Common.Controls.Grid.ReorderColumns({ Grid: newValue, CookieName: 'KPIVENTender_Grid' });
    });

    $scope.View_Click = function ($event) {
        $event.preventDefault();

        $scope.kpi_gridOptions.dataSource.read();
        $scope.HasVENTenderSearch = true;
    };

    $scope.Generate_Click = function ($event) {
        $event.preventDefault();

        $rootScope.Message({
            Type: Common.Message.Type.Confirm,
            Title: 'Thông báo', Msg: 'Bạn muốn tính lại KPI?',
            Action: true, Close: null,
            Ok: function (pars) {
                $rootScope.IsLoading = true;
                Common.Services.Call($http, {
                    url: Common.Services.url.KPI,
                    method: _KPIVENTender_Index.URL.VENTender_Generate,
                    data: pars,
                    success: function (res) {
                        $rootScope.IsLoading = false;
                        $rootScope.Message({ Msg: "Đã cập nhật!" });
                        if (Common.HasValue(res)) {
                            $scope.kpi_gridOptions.dataSource.read();
                        }
                    }
                });
            },
            pars: { dtfrom: $scope.ViewItem.DateFrom, dtto: $scope.ViewItem.DateTo, cusID: $scope.ViewItem.CusID }
        });
    };

    $scope.VENTenderReason_Click = function ($event, item, win) {
        $event.preventDefault();
        $scope.LoadData_Reason();
        $scope.VenTenderItem = $.extend({}, true, item);
        win.center().open();
    }

    $scope.cboKPIReason_Options = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true,
        dataTextField: 'ReasonName', dataValueField: 'ID', minLength: 3,
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    ReasonName: { type: 'string' },
                }
            }
        })
    }

    $scope.LoadData_Reason = function () {
        Common.Services.Call($http, {
            url: Common.Services.url.KPI,
            method: _KPIVENTender_Index.URL.Time_Reason_List,
            data: { kpiID: $scope.ViewItem.KPIID },
            success: function (res) {
                $timeout(function () {
                    $scope.cboKPIReason_Options.dataSource.data(res);
                }, 1)
            }
        });
    }

    $scope.VenTender_Reason_Submit_Click = function ($event, win) {
        $event.preventDefault();

        Common.Services.Call($http, {
            url: Common.Services.url.KPI,
            method: _KPIVENTender_Index.URL.VENTender_Save,
            data: { item: $scope.VenTenderItem },
            success: function (res) {
                Common.Services.Error(res, function () {
                    win.close();
                    $scope.kpi_gridOptions.dataSource.read();
                    $rootScope.Message({ Msg: "Đã cập nhật!" });
                })
            }
        });
    }

    $scope.KPI_Excel_Click = function ($event) {
        $event.preventDefault();

        Common.Services.Call($http, {
            url: Common.Services.url.KPI,
            method: _KPIVENTender_Index.URL.VENTender_Excel,
            data: {
                venID: $scope.ViewItem.VenID,
                from: $scope.ViewItem.DateFrom, to: $scope.ViewItem.DateTo
            },
            success: function (res) {
                $rootScope.DownloadFile(res);
            }
        })
    }
    //#endregion
    $scope.Window_Close_Click = function ($event, win) {
        $event.preventDefault();

        win.close();
    }
}]);