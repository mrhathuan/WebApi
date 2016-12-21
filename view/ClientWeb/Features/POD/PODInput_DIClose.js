/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />

//#region const

var _PODDIClose = {
    URL: {
        Read: 'PODDIInput_CloseList',
        Approved: 'PODDIInput_Approved',
        UnApproved: 'PODDIInput_UnApproved'
    },
}

//#endregion

angular.module('myapp').controller('PODInput_DICloseCtrl', ['$rootScope', '$scope', '$http', '$location', '$state', '$timeout', function ($rootScope, $scope, $http, $location, $state, $timeout) {

    Common.Log('PODInput_DICloseCtrl');
    $rootScope.IsLoading = false;
    $scope.HasChoose=false;
    $scope.PODInputItem = {
        DateFrom: (new Date()).addDays(-7),
        DateTo: (new Date()),
        ListCustomerID:[]
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

    var CS = $.extend({
        IsClosed: 100,
        IsOrigin: 100, IsInvoice: 100, OrderCode: 110, SOCode: 110, StatusSOPOD: 110, Note2: 110, Note2: 110,
        Note: 100, Description: 100, InvoiceBy: 150, InvoiceBy: 150, InvoiceDate: 200, StatusOPSMaster: 120, LocationToCode: 120,
        LocationToName: 120, LocationToAddress: 120, LocationToProvince: 110, LocationToDistrict: 110, EconomicZone: 110, LocationFromCode: 110,
        RegNo: 110, DriverName: 100, DriverTel: 100, MasterCode: 100, RequestDate: 120, DateFromCome: 120, DateFromLoadStart: 120, DateToLoadEnd: 120,
        DateToLeave: 120, CustomerCode: 120, VendorName: 120, GroupOfProductCode: 120, GroupOfProductName: 120, DNABA: 120,
        ChipNo: 120, Temperature: 120, Ton: 120, TonBBGN: 120, TonTranfer: 120, CBM: 120, CBMBBGN: 120, CBMTranfer: 120,
        Quantity: 120, QuantityTranfer: 110, StatusOrder: 200, KPIOPSDate: 120, IsKPIOPS: 120, KPIPODDate: 120,
        IsKPIPOD: 120, TonReturn: 120, CBMReturn: 120, QuantityReturn: 120, TypeOfDITOGroupProductReturnName: 120, HasReturn: 80, TotalReturn: 120, InvoiceReturnBy: 150,
        InvoiceReturnDate: 200, InvoiceReturnNote: 150
    }, true, $rootScope.GetColumnSettings('PODInput_grid'));

    $scope.PODInput_gridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.POD,
            method: _PODDIClose.URL.Read,
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
                    IsChoose:{type:'boolean'}
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
            {
                field: 'IsClosed', width: CS['IsClosed'], title: '{{RS.OPSDITOGroupProduct.IsClosed}}', attributes: { style: "text-align: center; " },
                template: "<input  disabled='disabled' type='checkbox' ng-model='dataItem.IsClosed' ></input>",
                filterable: {
                    cell: {
                        template: function (container) {
                            container.element.kendoComboBox({
                                dataSource: [{ Text: 'Đã duyệt', Value: true }, { Text: 'Chưa duyệt', Value: false }, { Text: 'Tất cả', Value: '' }],
                                dataTextField: "Text",
                                dataValueField: "Value",
                            });
                        },
                        showOperators: false
                    }
                }
            },
            {
                field: 'IsOrigin', width: CS['IsOrigin'], title: '{{RS.OPSDITOGroupProduct.IsOrigin}}', attributes: { style: "text-align: center; " },
                template: "<input class='chkIsOrigin' disabled='disabled' type='checkbox' ng-model='dataItem.IsOrigin' ></input>",
                filterable: {
                    cell: {
                        template: function (container) {
                            container.element.kendoComboBox({
                                dataSource: [{ Text: 'Đúng', Value: true }, { Text: 'Sai', Value: false }, { Text: 'Tất cả', Value: '' }],
                                dataTextField: "Text",
                                dataValueField: "Value",
                            });
                        },
                        showOperators: false
                    }
                }
            },
            {
                field: 'IsInvoice', width: CS['IsInvoice'], title: 'Đã nhận', attributes: { style: "text-align: center; " },
                template: "<input type='checkbox' ng-model='dataItem.IsInvoice' ng-disabled='dataItem.IsClosed' ></input>",
                filterable: {
                    cell: {
                        template: function (container) {
                            container.element.kendoComboBox({
                                dataSource: [{ Text: 'Đã nhận', Value: true }, { Text: 'Chưa nhận', Value: false }, { Text: 'Tất cả', Value: '' }],
                                dataTextField: "Text",
                                dataValueField: "Value",
                            });
                        },
                        showOperators: false
                    }
                }
            },
            { field: 'OrderCode', width: CS['OrderCode'], title: 'Đơn hàng', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'SOCode', width: CS['SOCode'], title: 'Số SO',  filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'StatusSOPOD', width: CS['StatusSOPOD'], title: 'Tình trạng c.từ theo SO', sortable: false, filterable: false, menu: true },
            { field: 'DNCode', width: CS['DNCode'], title: 'Số DN',  filterable: { cell: { operator: 'contains', showOperators: false } } },
            {
                field: 'Note1', width: CS['Note1'], title: '{{RS.OPSDITOGroupProduct.Note1}}',
                template: '#=Note1!=null?Note1:""#',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'Note2', width: CS['Note2'], title: '{{RS.OPSDITOGroupProduct.Note2}}',
                template: '#=Note2!=null?Note2:""#',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'Note', width: CS['Note'], title: '{{RS.OPSDITOGroupProduct.Note}}',
                template: '#=Note!=null?Note:""#',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'Description', width: CS['Description'], title: 'Ghi chú SO',
                template: '#=Description!=null?Description:""#',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'InvoiceNote', width: CS['InvoiceNote'],
                title: '{{RS.OPSDITOGroupProduct.InvoiceNote}}', template: '#=InvoiceNote!=null?InvoiceNote:""#',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'InvoiceBy', width: CS['InvoiceBy'],
                title: '{{RS.OPSDITOGroupProduct.InvoiceBy}}', template: '#=InvoiceBy!=null?InvoiceBy:""#',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'InvoiceDate', width: CS['InvoiceDate'],
                title: '{{RS.OPSDITOGroupProduct.InvoiceDate}}', template: '#=InvoiceDate==null?" ":Common.Date.FromJsonDMYHM(InvoiceDate)#',
                filterable: { cell: { operator: 'gte', showOperators: false } }
            },
           // { field: 'DatePODContract', width: CS[''], title: 'Ngày nhận chứng tứ theo HĐ', template: "#=DatePODContract==null?' ':Common.Date.FromJsonDMY(DatePODContract)#", filterable: false },
            { field: 'StatusOPSMaster', width: CS['StatusOPSMaster'], title: 'Trạng thái chuyến', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'LocationToCode', width: CS['LocationToCode'], title: 'Mã NPP', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'LocationToName', width: CS['LocationToName'], title: 'Tên NPP', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'LocationToAddress', width: CS['LocationToAddress'], title: 'Địa chỉ', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'LocationToProvince', width: CS['LocationToProvince'], title: 'Tỉnh thành', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'LocationToDistrict', width: CS['LocationToDistrict'], title: 'Quận huyện', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'EconomicZone', width: CS['EconomicZone'], title: 'RouteID', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'LocationFromCode', width: CS['LocationFromCode'], title: 'Mã kho', filterable: { cell: { operator: 'contains', showOperators: false } } },

            { field: 'RegNo', width: CS['RegNo'], title: 'Số xe', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'DriverName', width: CS['DriverName'], title: 'Tên tài xế', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'DriverTel', width: CS['DriverTel'], title: 'SĐT tài xế', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'MasterCode', width: CS['MasterCode'], title: 'Lệnh v.c', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'RequestDate', width: CS['RequestDate'], title: 'Ngày gửi', template: "#=RequestDate==null?' ':Common.Date.FromJsonDMYHMS(RequestDate)#", filterable: false },
            { field: 'DateFromCome', width: CS['DateFromCome'], title: '{{RS.OPSDITOGroupProduct.DateFromCome}}', template: "#=DateFromCome==null?' ':Common.Date.FromJsonDMY(DateFromCome)#", filterable: false },
            { field: 'DateFromLoadStart', width: CS['DateFromLoadStart'], title: '{{RS.OPSDITOGroupProduct.DateFromLoadStart}}', template: "#=DateFromLoadStart==null?' ':Common.Date.FromJsonHM(DateFromLoadStart)#", filterable: false },
            { field: 'DateFromLoadEnd', width: CS['DateFromLoadEnd'], title: '{{RS.OPSDITOGroupProduct.DateFromLoadEnd}}', template: "#=DateFromLoadEnd==null?' ':Common.Date.FromJsonHM(DateFromLoadEnd)#", filterable: false },
            { field: 'DateFromLeave', width: CS['DateFromLeave'], title: '{{RS.OPSDITOGroupProduct.DateFromLeave}}', template: "#=DateFromLeave==null?' ':Common.Date.FromJsonDMY(DateFromLeave)#", filterable: false },
            { field: 'DateToCome', width: CS['DateToCome'], title: '{{RS.OPSDITOGroupProduct.DateToCome}}', template: "#=DateToCome==null?' ':Common.Date.FromJsonDMY(DateToCome)#", filterable: false },
            { field: 'DateToLoadStart', width: CS['DateToLoadStart'], title: '{{RS.OPSDITOGroupProduct.DateToLoadStart}}', template: "#=DateToLoadStart==null?' ':Common.Date.FromJsonHM(DateToLoadStart)#", filterable: false },
            { field: 'DateToLoadEnd', width: CS['DateToLoadEnd'], title: '{{RS.OPSDITOGroupProduct.DateToLoadEnd}}', template: "#=DateToLoadEnd==null?' ':Common.Date.FromJsonHM(DateToLoadEnd)#", filterable: false },
            { field: 'DateToLeave', width: CS['DateToLeave'], title: '{{RS.OPSDITOGroupProduct.DateToLeave}}', template: "#=DateToLeave==null?' ':Common.Date.FromJsonDMY(DateToLeave)#", filterable: false },
            { field: 'CustomerCode', width: CS['CustomerCode'], title: 'Mã khách hàng', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'CustomerName', width: CS['CustomerName'], title: 'Tên khách hàng', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'VendorName', width: CS['VendorName'], title: 'Nhà vận tải', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'GroupOfProductCode', width: CS['GroupOfProductCode'], title: 'Mã hàng', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'GroupOfProductName', width: CS['GroupOfProductName'], title: 'Nhóm hàng', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'DNABA', width: CS['DNABA'], title: 'Mã chuyến', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'ChipNo', width: CS['ChipNo'], title: 'Số chíp', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'Temperature', width: CS['Temperature'], title: 'Nhiệt độ', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'Ton', width: CS['Ton'], title: '{{RS.OPSDITOGroupProduct.Ton}}', filterable: { cell: { operator: 'gte', showOperators: false } } },
            {
                field: 'TonBBGN', width: CS['TonBBGN'], title: '{{RS.OPSDITOGroupProduct.TonBBGN}}', filterable: { cell: { operator: 'gte', showOperators: false } },
                template: '#=TonBBGN==null?" ": Common.Number.ToNumber5(TonBBGN)#',
            },
            {
                field: 'TonTranfer', width: CS['TonTranfer'], title: '{{RS.OPSDITOGroupProduct.TonTranfer}}', filterable: { cell: { operator: 'gte', showOperators: false } },
                template: '#=TonTranfer==null?" ": Common.Number.ToNumber5(TonTranfer)#',
            },
            {
                field: 'CBM', width: CS['CBM'], title: '{{RS.OPSDITOGroupProduct.CBM}}', filterable: { cell: { operator: 'gte', showOperators: false } },
                template: '#=CBM==null?" ": Common.Number.ToNumber5(CBM)#',
            },
            {
                field: 'CBMBBGN', width: CS['CBMBBGN'], title: '{{RS.OPSDITOGroupProduct.CBMBBGN}}', filterable: { cell: { operator: 'gte', showOperators: false } },
                template: '#=CBMBBGN==null?" ": Common.Number.ToNumber5(CBMBBGN)#',
            },
            {
                field: 'CBMTranfer', width: CS['CBMTranfer'], title: '{{RS.OPSDITOGroupProduct.CBMTranfer}}', filterable: { cell: { operator: 'gte', showOperators: false } },
                template: '#=CBMTranfer==null?" ": Common.Number.ToNumber5(CBMTranfer)#',
            },
            {
                field: 'Quantity', width: CS['Quantity'], title: '{{RS.OPSDITOGroupProduct.Quantity}}', filterable: { cell: { operator: 'gte', showOperators: false } },
                template: '#=Quantity==null?" ": Common.Number.ToNumber5(Quantity)#',
            },
            {
                field: 'QuantityBBGN', width: CS['QuantityBBGN'], title: '{{RS.OPSDITOGroupProduct.QuantityBBGN}}', filterable: { cell: { operator: 'gte', showOperators: false } },
                template: '#=QuantityBBGN==null?" ": Common.Number.ToNumber5(QuantityBBGN)#',
            },
            {
                field: 'QuantityTranfer', width: CS['QuantityTranfer'], title: '{{RS.OPSDITOGroupProduct.QuantityTranfer}}', filterable: { cell: { operator: 'contains', showOperators: false } },
                template: '#=QuantityTranfer==null?" ": Common.Number.ToNumber5(QuantityTranfer)#',
            },
            { field: 'StatusOrder', width: CS['StatusOrder'], title: 'Tình trạng đơn hàng', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'KPIOPSDate', width: CS['KPIOPSDate'], title: 'Ngày v.c hợp đồng', template: "#=KPIOPSDate==null?' ':Common.Date.FromJsonDMY(KPIOPSDate)#", sortable: false, filterable: false, menu: true },
            {
                field: 'IsKPIOPS', width: CS['IsKPIOPS'], title: 'Đạt KPI v.c', sortable: false, filterable: false, menu: true,
                template: '<input ng-show="dataItem.IsKPIOPS != null" disabled type="checkbox" #= IsKPIOPS ? "checked=checked" : ""# />', attributes: { style: 'text-align: center;' },
            },
            { field: 'KPIPODDate', width: CS['KPIPODDate'], title: 'Ngày c.t hợp đồng', template: "#=KPIPODDate==null?' ':Common.Date.FromJsonDMY(KPIPODDate)#", sortable: false, filterable: false, menu: true },
            {
                field: 'IsKPIPOD', width: CS['IsKPIPOD'], title: 'Đạt KPI c.t', sortable: false, filterable: false, menu: true,
                template: '<input ng-show="dataItem.IsKPIPOD != null" disabled type="checkbox" #= IsKPIPOD ? "checked=checked" : ""# />', attributes: { style: 'text-align: center;' },
            },
            {
                field: 'TonReturn', width: CS['TonReturn'], title: '{{RS.OPSDITOGroupProduct.TonReturn}}', filterable: { cell: { operator: 'gte', showOperators: false } },
                template: '#=TonReturn==null?" ": Common.Number.ToNumber5(TonReturn)#',
            },
            {
                field: 'CBMReturn', width: CS['CBMReturn'], title: '{{RS.OPSDITOGroupProduct.CBMReturn}}', filterable: { cell: { operator: 'gte', showOperators: false } },
                template: '#=CBMReturn==null?" ": Common.Number.ToNumber5(CBMReturn)#',
            },
            {
                field: 'QuantityReturn', width: CS['QuantityReturn'], title: '{{RS.OPSDITOGroupProduct.QuantityReturn}}', filterable: { cell: { operator: 'gte', showOperators: false } },
                template: '#=QuantityReturn==null?" ": Common.Number.ToNumber5(QuantityReturn)#',
            },
            {
                field: 'TypeOfDITOGroupProductReturnName', width: CS['TypeOfDITOGroupProductReturnName'], title: '{{RS.OPSTypeOfDITOGroupProductReturn.TypeName}}', filterable: false,
            },
            {
                field: 'HasReturn', width: CS['HasReturn'], title: 'Có trả về', sortable: false, filterable: false, menu: true,
                template: '<input ng-show="dataItem.HasReturn != null" disabled type="checkbox" #= HasReturn ? "checked=checked" : ""# />', attributes: { style: 'text-align: center;' },
            },
            { field: 'TotalReturn', width: CS['TotalReturn'], title: 'S.l khay trả về', filterable: { cell: { operator: 'equal', showOperators: false } } },
            {
                field: 'InvoiceReturnBy', width: CS['InvoiceReturnBy'],
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'InvoiceReturnDate', width: CS['InvoiceReturnDate'],
                title: '{{RS.OPSDITOGroupProduct.InvoiceReturnDate}}', template: "#=InvoiceReturnDate==null?' ':Common.Date.FromJsonDMY(InvoiceReturnDate)#",
                filterable: { cell: { operator: 'gte', showOperators: false } }
            },
            {
                field: 'InvoiceReturnNote', width: CS['InvoiceReturnNote'],
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            { title: ' ', field: 'F_Empty', sortable: false, filterable: false, menu: false }
        ],
        dataBound: function (e) {
            
        }
    }
    $scope.PODInput_gridChoose_Change = function ($event, grid, hasChoose) {
        $scope.HasChoose = hasChoose;
    }

    $timeout(function () {
        $rootScope.ApplyGridSettings($scope.PODInput_grid);
    }, 100);

    $scope.PODInput_ApprovedClick = function ($event,grid) {
        $event.preventDefault();
        var data = [];
        Common.Data.Each(grid.dataSource.data(), function (o) {
            if (o.IsChoose) {
                data.push(o.ID);
            }
        })

        if (data.length > 0) {
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.POD,
                method: _PODDIClose.URL.Approved,
                data: { lst:data },
                success: function (res) {
                    grid.dataSource.read();
                    $rootScope.Message({ NotifyType: Common.Message.NotifyType.SUCCESS, Msg: 'Thành công', })
                    $rootScope.IsLoading = false;
                }
            });
        }
        else $rootScope.Message({ NotifyType: Common.Message.NotifyType.ERROR,  Msg: 'chưa chọn dữ liệu',})
    }

    $scope.PODInput_UnApprovedClick = function ($event, grid) {
        $event.preventDefault();
        var data = [];
        Common.Data.Each(grid.dataSource.data(), function (o) {
            if (o.IsChoose) {
                data.push(o.ID);
            }
        })

        if (data.length > 0) {
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.POD,
                method: _PODDIClose.URL.UnApproved,
                data: { lst: data },
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