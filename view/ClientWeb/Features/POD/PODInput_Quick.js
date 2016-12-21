/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />

//#region const

var _PODInput_Quick = {
    URL: {
        Read: 'PODDIInput_Quick_List',
        Update: 'PODDIInput_Quick_Save',
        DNGet: "PODDIInput_Quick_DNGet",
        DNSave: "PODDIInput_Quick_DNSave",
    },
    Data: {
        DIPODStatus: []
    }
}

//#endregion

angular.module('myapp').controller('PODInput_QuickCtrl', ['$rootScope', '$scope', '$http', '$location', '$state', '$timeout', function ($rootScope, $scope, $http, $location, $state, $timeout) {

    Common.Log('PODInput_QuickCtrl');
    $rootScope.IsLoading = false;

    //$scope.ItemSearch = {
    //    DateFrom: (new Date()).addDays(-7),
    //    DateTo: (new Date()),
    //}

    //tam de test 
    $scope.ItemSearch = {
        DateFrom: (new Date(2016, 5, 1)),
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
            $scope.PODInput_quick_gridOptions.dataSource.read();
        }
    }


    $scope.DITOGroupProductStatusPOD_datasource = [];

    $scope.cboDITOGroupProductStatusPOD_Options = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true, dataTextField: 'ValueOfVar', dataValueField: 'ID',
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    ValueOfVar: { type: 'string' },
                }
            }
        })
    }

    Common.ALL.Get($http, {
        url: Common.ALL.URL.SYSVarTypeOfDIPODStatus,
        success: function (data) {
            $scope.cboDITOGroupProductStatusPOD_Options.dataSource.data(data)
            _PODInput.Data.DIPODStatus = data;
        }
    })

    var CS = { OrderCode: 110,  InvoiceNote: 150, InvoiceDateString: 85, InvoiceDate: 200, 
        RegNo: 110, ProductCode: 100,  TonBBGN: 120, TonTranfer: 120,  CBMBBGN: 120, CBMTranfer: 120,
        Quantity: 120, QuantityTranfer: 110,  TonReturn: 120, CBMReturn: 120, QuantityReturn: 120, TypeOfDITOGroupProductReturnID: 120, 
        InvoiceReturnDateString: 85, InvoiceReturnNote: 150
    };

    $scope.PODInput_quick_gridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.POD,
            method: _PODInput_Quick.URL.Read,
            readparam: function () {
                return {
                    'dtFrom': $scope.ItemSearch.DateFrom,
                    'dtTo': $scope.ItemSearch.DateTo,
                    'listCustomerID': $scope.ItemSearch.ListCustomerID,
                };
            },
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number', editable: false, nullable: false },
                    IsOrigin: { type: 'boolean', },
                    IsInvoice: { type: 'boolean', },
                    IsComplete: { type: 'number', },
                    DITOGroupProductStatusPODID: { type: 'number', },
                    InvoiceDate: { type: 'date' },
                    TonBBGN: { type: 'number', },
                    TonTranfer: { type: 'number', },
                    CBMBBGN: { type: 'number', },
                    CBMTranfer: { type: 'number', },
                    QuantityBBGN: { type: 'number', },
                    QuantityTranfer: { type: 'number', },
                    InvoiceReturnDate: { type: 'date' },
                    InvoiceReturnDateString: { type: 'string' },
                    InvoiceDateString: { type: 'string' },
                }
            },
            pageSize: 20
        }),
        selectable: false, reorderable: false, editable: false,
        height: '100%', pageable: true, sortable: false, columnMenu: false, filterable: { mode: 'row' }, resizable: true,
        columns: [
            
            { field: 'OrderCode', width: CS['OrderCode'], title: 'Đơn hàng', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'RegNo', width: CS['RegNo'], title: 'Số xe', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'GroupOfProductCode', width: CS['GroupOfProductCode'], title: 'Mã nhóm hàng', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'ProductCode', width: CS['ProductCode'], title: 'Mã hàng', filterable: { cell: { operator: 'contains', showOperators: false } } },
            {
                width: 50, title: ' ', filterable:   false  ,sortable: false,
                template: '<a  href="/" ng-click="PODInput_Quick_DNClick($event,dataItem,DN_win)" class="k-button" ><i class="fa fa-ellipsis-h"></i></a>',
            },
            {
                field: 'InvoiceNote', width: CS['InvoiceNote'],
                title: 'Số chứng từ',
                template: '<input type="text"  class="k-textbox txtInvoiceNote "  ng-model="dataItem.InvoiceNote" ng-keydown="QuickSave($event,dataItem)" ></input>',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'InvoiceDateString', width: CS['InvoiceDateString'],
                title: '{{RS.OPSDITOGroupProduct.InvoiceDate}}',
                template: '<input type="text" class="k-textbox txtInvoiceDate" ng-model="dataItem.InvoiceDateString" ng-keydown="ChangeInvoiceDate($event,dataItem)"></input>',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'QuantityBBGN', width: CS['QuantityBBGN'], title: '{{RS.OPSDITOGroupProduct.QuantityBBGN}}', filterable: { cell: { operator: 'gte', showOperators: false } },
                template: '<input  kendo-numeric-text-box k-options="Quantity_Options"  ng-model="dataItem.QuantityBBGN"  ng-keydown="QuickSave($event,dataItem)" style="width: 100%" ></input>',
            },
            {
                field: 'TonBBGN', width: CS['TonBBGN'], title: '{{RS.OPSDITOGroupProduct.TonBBGN}}', filterable: { cell: { operator: 'gte', showOperators: false } },
                template: '<input  kendo-numeric-text-box k-options="Ton_Options" ng-model="dataItem.TonBBGN"  ng-keydown="QuickSave($event,dataItem)" style="width: 100%"  ></input>',
            },
            {
                field: 'CBMBBGN', width: CS['CBMBBGN'], title: '{{RS.OPSDITOGroupProduct.CBMBBGN}}', filterable: { cell: { operator: 'gte', showOperators: false } },
                template: '<input kendo-numeric-text-box k-options="CBM_Options"  ng-model="dataItem.CBMBBGN"  ng-keydown="QuickSave($event,dataItem)" style="width: 100%"  ></input>',
            },
            {
                field: 'QuantityTranfer', width: CS['QuantityTranfer'], title: '{{RS.OPSDITOGroupProduct.QuantityTranfer}}', filterable: { cell: { operator: 'contains', showOperators: false } },
                template: '<input  kendo-numeric-text-box k-options="Quantity_Options"  ng-model="dataItem.QuantityTranfer"  ng-keydown="QuickSave($event,dataItem)" style="width: 100%"  ></input>',
            },
            {
                field: 'TonTranfer', width: CS['TonTranfer'], title: '{{RS.OPSDITOGroupProduct.TonTranfer}}', filterable: { cell: { operator: 'gte', showOperators: false } },
                template: '<input  kendo-numeric-text-box k-options="Ton_Options" ng-model="dataItem.TonTranfer"  ng-keydown="QuickSave($event,dataItem)" style="width: 100%" ></input>',
            },
            {
                field: 'CBMTranfer', width: CS['CBMTranfer'], title: '{{RS.OPSDITOGroupProduct.CBMTranfer}}', filterable: { cell: { operator: 'gte', showOperators: false } },
                template: '<input  kendo-numeric-text-box k-options="CBM_Options"  ng-model="dataItem.CBMTranfer"  ng-keydown="QuickSave($event,dataItem)" style="width: 100%" ></input>',
            },
            {
                field: 'QuantityReturn', width: CS['QuantityReturn'], title: '{{RS.OPSDITOGroupProduct.QuantityReturn}}', filterable: { cell: { operator: 'gte', showOperators: false } },
                template: '<input  kendo-numeric-text-box k-options="Quantity_Options"  ng-model="dataItem.QuantityReturn"  ng-keydown="QuickSave($event,dataItem)" style="width: 100%" ></input>',
            },
            {
                field: 'TonReturn', width: CS['TonReturn'], title: '{{RS.OPSDITOGroupProduct.TonReturn}}', filterable: { cell: { operator: 'gte', showOperators: false } },
                template: '<input  kendo-numeric-text-box k-options="Ton_Options" ng-model="dataItem.TonReturn"  ng-keydown="QuickSave($event,dataItem)" style="width: 100%" ></input>',
            },
            {
                field: 'CBMReturn', width: CS['CBMReturn'], title: '{{RS.OPSDITOGroupProduct.CBMReturn}}', filterable: { cell: { operator: 'gte', showOperators: false } },
                template: '<input kendo-numeric-text-box k-options="CBM_Options"  ng-model="dataItem.CBMReturn"  ng-keydown="QuickSave($event,dataItem)" style="width: 100%" ></input>',
            },
            {
                field: 'InvoiceReturnNote', width: CS['InvoiceReturnNote'],
                title: '{{RS.OPSDITOGroupProduct.InvoiceReturnNote}}', template: '<input  class="k-textbox "  ng-model="dataItem.InvoiceReturnNote"  ng-keydown="QuickSave($event,dataItem)" style="width: 100%"></input>',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'InvoiceReturnDateString', width: CS['InvoiceReturnDateString'],
                title: '{{RS.OPSDITOGroupProduct.InvoiceReturnDate}}',
                template: '<input type="text" class="k-textbox txtInvoiceReturnDate" ng-model="dataItem.InvoiceReturnDateString" ng-keydown="ChangeInvoiceReturnDate($event,dataItem)"></input>',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'TypeOfDITOGroupProductReturnID', width: CS['TypeOfDITOGroupProductReturnID'], title: '{{RS.OPSTypeOfDITOGroupProductReturn.TypeName}}', filterable: false,
                template: '<input class="cus-combobox" kendo-combo-box k-options="TypeOfReturn_Options"  ng-model="dataItem.TypeOfDITOGroupProductReturnID" value="dataItem.TypeOfDITOGroupProductReturnID"  style="width: 100%"  ></input>',
            },
            {
                title: ' ', width: '85px', field: 'F_Command',
                template: '<a href="/" ng-click="PODInput_Quick_SaveClick($event,PODInput_quick_grid)" class="k-button" data-title="Lưu"><i class="fa fa-check"></i></a>'
                + '<a href="/" ng-click="PODInput_Quick_UpLoadClick($event,winfile,dataItem)" class="k-button" data-title="Chứng từ"><i class="fa fa-file"></i></a>',
                filterable: false, sortable: false, locked: false,
            },
            { title: ' ', field: 'F_Empty', sortable: false, filterable: false, menu: false }
        ],
        dataBound: function (e) {
            var grid = this;
            if (Common.HasValue(grid.tbody) && Common.HasValue(grid)) {
                var lst = grid.tbody.find('tr');
                if (lst.length > 0) {

                    lstTd = $(lst[0]).find('.txtInvoiceNote');
                    if (lstTd.length > 0)
                        $timeout(function () {
                            $(lstTd[0]).focus();
                        }, 100)

                }
            }
        }
    }

    $scope.ChangeInvoiceDate = function ($event, data) {
        if ($event.which === 38) {
            //up arrow
            data.InvoiceDate = Common.Date.AddDay(data.InvoiceDate, 1);
            data.InvoiceDateString = Common.Date.ToString(data.InvoiceDate, Common.Date.Format.DDMM)
        }
        if ($event.which === 40) {
            //down arrow
            data.InvoiceDate = Common.Date.AddDay(data.InvoiceDate, -1);
            data.InvoiceDateString = Common.Date.ToString(data.InvoiceDate, Common.Date.Format.DDMM)
        }
        if ($event.which === 13) {
            $scope.QuickSave($event,data);
        }
    }

    $scope.ChangeInvoiceReturnDate = function ($event, data) {
        if ($event.which === 38) {
            //up arrow
            data.InvoiceReturnDate = Common.Date.AddDay(data.InvoiceReturnDate, 1);
            data.InvoiceReturnDateString = Common.Date.ToString(data.InvoiceReturnDate, Common.Date.Format.DDMM)
        }
        if ($event.which === 40) {
            //down arrow
            data.InvoiceReturnDate = Common.Date.AddDay(data.InvoiceReturnDate, -1);
            data.InvoiceReturnDateString = Common.Date.ToString(data.InvoiceReturnDate, Common.Date.Format.DDMM)
        }
        if ($event.which === 13) {
            $scope.QuickSave($event, data);
        }
    }

    $timeout(function () {
        $rootScope.ApplyGridSettings($scope.PODInput_quick_grid);
    }, 100);

    $scope.Ton_Options = { format: 'n3', spinners: false, culture: 'en-US', min: 0, step: 1, decimals: 3 }
    $scope.CBM_Options = { format: 'n3', spinners: false, culture: 'en-US', min: 0, step: 1, decimals: 3 }
    $scope.Quantity_Options = { format: 'n0', spinners: false, culture: 'en-US', min: 0, step: 1, decimals: 0 }

    $scope.TypeOfReturn_Options = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: "contains", suggest: true,
        dataTextField: 'TypeName', dataValueField: 'ID',
        dataSource: Common.DataSource.Local([], {
            id: 'ID',
            fields: {
                TypeName: { type: 'string' },
                ID: { type: 'number' },
            }
        })
    }

    Common.ALL.Get($http, {
        url: Common.ALL.URL.OPSTypeOfDITOGroupProductReturn,
        success: function (data) {

            var xdata = [];
            xdata.push({ TypeName: ' ', ID: -1 });
            Common.Data.Each(data, function (o) {
                xdata.push(o)
            })
            $scope.TypeOfReturn_Options.dataSource.data(xdata)
        }
    })

    $scope.PODInput_Quick_SaveClick = function ($event, grid) {
        $event.preventDefault();
        var tr = $($event.target).closest('tr');
        var item = grid.dataItem(tr);
        if (Common.HasValue(item)) {
            //$rootScope.IsLoading = true;
            //Common.Services.Call($http, {
            //    url: Common.Services.url.POD,
            //    method: _PODInput.URL.Update,
            //    data: { 'item': item },
            //    success: function (res) {
            //        $rootScope.IsLoading = false;
            //        $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
            //        $scope.PODInput_gridOptions.dataSource.read();
            //    },
            //    error: function (res) {
            //        $rootScope.IsLoading = false;
            //    }
            //})
        }
    }

    $scope.PODInput_Quick_UpLoadClick = function ($event, win, data) {
        $event.preventDefault();
        //$rootScope.UploadFile({
        //    IsImage: false,
        //    ID: data.ID,
        //    AllowChange: true,
        //    ShowChoose: false,
        //    Type: Common.CATTypeOfFileCode.DIPOD,
        //    Window: win,
        //    Complete: function (image) {
        //        $scope.Item.Image = image.FilePath;
        //    }
        //});
    }


    $scope.QuickSave = function ($event, data) {
        if ($event.which === 13) {
            Common.Log("QuickSave");
            var error = true;
            var str = data.InvoiceDateString.split("/");

            if (str.length == 2) {
                if (Common.HasValue(data.InvoiceNote)&&data.InvoiceNote!='') {
                    var day = parseInt(str[0]);
                    var month = parseInt(str[1]);
                    if (day > 0 && day <= 31 && month > 0 && month < 13) {
                        data.InvoiceDate = new Date(new Date().getFullYear, month - 1, day);

                        if (data.QuantityReturn > 0) {
                            if (Common.HasValue(data.InvoiceReturnNote) && data.InvoiceReturnNote != '') {
                                var strReturn = data.InvoiceReturnDateString.split("/");
                                if (strReturn.length == 2) {
                                    var dayReturn = parseInt(strReturn[0]);
                                    var monthReturn = parseInt(strReturn[1]);
                                    if (dayReturn > 0 && dayReturn <= 31 && monthReturn > 0 && monthReturn < 13) {
                                        data.InvoiceReturnDate = new Date(new Date().getFullYear, monthReturn - 1, dayReturn);
                                        error = false;
                                    }
                                    else $rootScope.Message({ Msg: 'Ngày chứng từ trả về không chính xác', NotifyType: Common.Message.NotifyType.ERROR });
                                }
                                else $rootScope.Message({ Msg: 'Ngày chứng từ trả về không chính xác', NotifyType: Common.Message.NotifyType.ERROR });
                            } else $rootScope.Message({ Msg: 'Thiếu chứng từ hàng trả về', NotifyType: Common.Message.NotifyType.ERROR });
                        }

                        if (error) {
                            $rootScope.IsLoading = true;
                            Common.Services.Call($http, {
                                url: Common.Services.url.POD,
                                method: _PODInput_Quick.URL.Update,
                                data: { 'item': data },
                                success: function (res) {
                                    $rootScope.IsLoading = false;
                                    $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                                    $scope.PODInput_quick_gridOptions.dataSource.read();
                                }
                            })
                        }
                    }
                    else {
                        $rootScope.Message({ Msg: 'Ngày chứng từ không chính xác', NotifyType: Common.Message.NotifyType.ERROR });
                    }
                } else $rootScope.Message({ Msg: 'Số chứng từ không được trống', NotifyType: Common.Message.NotifyType.ERROR });
            }
            else {
                $rootScope.Message({ Msg: 'Ngày chứng từ không chính xác', NotifyType: Common.Message.NotifyType.ERROR });
            }
        }
    }

    $scope.PODInput_Quick_DNClick = function ($event,data,win) {
        $event.preventDefault();
        $rootScope.IsLoading = true;
        Common.Services.Call($http, {
            url: Common.Services.url.POD,
            method: _PODInput_Quick.URL.DNGet,
            data: {DITOGroupProductID:data.ID},
            success: function (res) {
                $scope.Item = res;
                $scope.Item.InvoiceDate = Common.Date.FromJson($scope.Item.InvoiceDate)
                $scope
                win.center();
                win.open();
                $rootScope.IsLoading = false;
            },
            error: function (res) {
                $rootScope.IsLoading = false;
            }
        });

    }
    $scope.numQuantityTranfer_options = { format: 'n3', spinners: false, culture: 'en-US', min: 1, step: 1, decimals: 3, }

    $scope.DNChangeInvoiceDate = function ($event) {
        if ($event.which === 38) {
            //up arrow
            
            $scope.Item.InvoiceDate = Common.Date.AddDay($scope.Item.InvoiceDate, 1);
            $scope.Item.InvoiceDateString = Common.Date.ToString($scope.Item.InvoiceDate, Common.Date.Format.DDMM)
        }
        if ($event.which === 40) {
            //down arrow
            $scope.Item.InvoiceDate = Common.Date.AddDay($scope.Item.InvoiceDate, -1);
            $scope.Item.InvoiceDateString = Common.Date.ToString($scope.Item.InvoiceDate, Common.Date.Format.DDMM)
        }
    }

    $scope.DN_SaveClick = function ($event,win) {
        $event.preventDefault();
        var str = $scope.Item.InvoiceDateString.split("/");

        if (str.length == 2) {
            if (Common.HasValue($scope.Item.InvoiceNote) && $scope.Item.InvoiceNote != '') {
                var day = parseInt(str[0]);
                var month = parseInt(str[1]);
                if (day > 0 && day <= 31 && month > 0 && month < 13) {
                    $scope.Item.InvoiceDate = new Date(new Date().getFullYear, month - 1, day);
                    if ($scope.Item.QuantityTranfer <= $scope.Item.KMax) {
                        $rootScope.IsLoading = true;
                        Common.Services.Call($http, {
                            url: Common.Services.url.POD,
                            method: _PODInput_Quick.URL.DNSave,
                            data: { 'item': $scope.Item },
                            success: function (res) {
                                $rootScope.IsLoading = false;
                                win.close();
                                $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                                $scope.PODInput_quick_gridOptions.dataSource.read();
                            }
                        })
                    }
                    else $rootScope.Message({ Msg: 'Số lượng không chính xác không chính xác', NotifyType: Common.Message.NotifyType.ERROR });
                }
                else {
                    $rootScope.Message({ Msg: 'Ngày chứng từ không chính xác', NotifyType: Common.Message.NotifyType.ERROR });
                }
            } else $rootScope.Message({ Msg: 'Số chứng từ không được trống', NotifyType: Common.Message.NotifyType.ERROR });
        }
        else {
            $rootScope.Message({ Msg: 'Ngày chứng từ không chính xác', NotifyType: Common.Message.NotifyType.ERROR });
        }
    }
    //actions
    $scope.ShowSetting = function ($event, grid) {
        $event.preventDefault();

        $rootScope.ShowSetting({
            ListView: views.PODInput_Quick,
            event: $event,
            grid: grid,
            current: $state.current
        });
    };

    $scope.HideSetting = function ($event) {
        $event.preventDefault();

        $rootScope.HideSetting();
    }

    $scope.Window_Close_Click = function ($event, win) {
        $event.preventDefault();

        win.close();
    }
}]);