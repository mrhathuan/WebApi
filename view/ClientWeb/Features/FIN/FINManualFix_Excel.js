/// <reference path="~/Scripts/jquery-1.9.1.intellisense.js" />
/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />

//#region URL

var _FINManualFix_Excel = {
    URL: {
        Template_List: 'FINSettingManual_List',
        Template_Download: 'FINManualFix_ExcelDownload',
        Import: 'FINManualFix_ExcelImport',
        Check: 'FINManualFix_ExcelCheck'
    },
    Data: {
        FromFile: [],
    },
}

//#endregion

angular.module('myapp').controller('FINManualFix_ExcelCtrl', ['$rootScope', '$scope', '$http', '$location', '$state', '$timeout', function ($rootScope, $scope, $http, $location, $state, $timeout) {
    Common.Log('FINManualFix_ExcelCtrl');
    $rootScope.IsLoading = false;

    $scope.Item = {
        DateFrom: Common.Date.AddDay(new Date(), -5),
        DateTo: new Date(),
    }

    $scope.HasTemplate = false;
    $scope.Template = {
        ID: -1, Name: '', CustomerName: '', CustomerID: -1
    }
    $scope.IsExcelChecked = false;

    $scope.excel_gridOptions = {
        dataSource: Common.DataSource.Local({
            data: [],
            pageSize: 0,
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number', editable: false },
                    ExcelSuccess: { type: 'boolean' }
                }
            }
        }),
        height: '100%', groupable: false, pageable: false, sortable: true, columnMenu: false, filterable: false, resizable: true, selectable: true,
        columns: [
            {
                field: 'ExcelSuccess', title: 'TC', width: '40px',
                template: '<input class="chkChoose" type="checkbox" #= ExcelSuccess ? "checked=checked" : "" # disabled="disabled" />',
                attributes: { style: 'text-align: center;'}, headerAttributes: { style: 'text-align: center;' }
            },
            {
                field: 'ExcelError', width: '250px', title: 'Thông báo', filterable: false,
            },
            {
                field: 'ID', title: 'Dữ liệu hệ thống', width: 150, sortable: false,
                template: '<select k-ng-model="dataItem.ID" kendo-combo-box k-data-text-field="\'Value\'" '
                    + 'k-data-value-field="\'ID\'"  k-filter="\'contains\'" k-auto-bind="true" k-min-length="3" '
                    + 'k-on-data-bound="cboIDDataBound(kendoEvent, dataItem)" k-data-source="dataItem.ListID" style="width: 100%" focus-k-combobox></select>'
            },
            {
                title: ' ', width: '45px',
                template: '<a href="/" ng-click="ManualFix_Click($event,manualFix_win, dataItem)" class="k-button classcolor"><i class="fa fa-info-circle"></i></a>',
                filterable: false, sortable: false
            },
            {
                title: ' ', width: '45px',
                template: '<a href="/" ng-click="Detail_Click($event,datasys_win, dataItem)" class="k-button"><i class="fa fa-file"></i></a>',
                filterable: false, sortable: false
            },
            {
                field: 'CustomerCode', width: '120px', title: 'Mã KH', filterable: { cell: { operator: 'contains', showOperators: false } },
            },
            {
                field: 'SOCode', width: '100px', title: 'Số SO', filterable: { cell: { operator: 'contains', showOperators: false } },
            },
            {
                field: 'DNCode', width: '100px', title: 'Số DN', filterable: { cell: { operator: 'contains', showOperators: false } },
            },
            {
                field: 'RegNo', width: '100px', title: 'Xe', filterable: { cell: { operator: 'contains', showOperators: false } },
            },
            {
                field: 'OrderCode', width: '100px', title: 'Mã đơn hàng', filterable: { cell: { operator: 'contains', showOperators: false } },
            },
            {
                field: 'RequestDate', width: '130px', title: 'Ngày gửi y/c', template: "#=RequestDate==null?' ':Common.Date.FromJsonDMY(RequestDate)#",
                filterable: {
                    cell: {
                        template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); },
                        operator: 'equal', showOperators: false,
                    }
                },
            },
            {
                field: 'LocationFromCode', width: '100px', title: 'Mã kho', filterable: { cell: { operator: 'contains', showOperators: false } },
            },
            {
                field: 'TonTranfer', width: '100px', title: 'Tấn lấy', filterable: { cell: { operator: 'gte', showOperators: false } }, template: '#=TonTranfer==null?" ":Common.Number.ToNumber3(TonTranfer)#'
            },
            {
                field: 'CBMTranfer', width: '100px', title: 'Khối lấy', filterable: { cell: { operator: 'gte', showOperators: false } }, template: '#=CBMTranfer==null?" ":Common.Number.ToNumber3(CBMTranfer)#'
            },
            {
                field: 'QuantityTranfer', width: '100px', title: 'Số lượng lấy', filterable: { cell: { operator: 'gte', showOperators: false } }, template: '#=QuantityTranfer==null?" ":Common.Number.ToNumber3(QuantityTranfer)#'
            },
            {
                field: 'KgTranfer', width: '100px', title: 'Kg lấy', filterable: { cell: { operator: 'gte', showOperators: false } }, template: '#=KgTranfer==null?" ":Common.Number.ToNumber3(KgTranfer)#'
            },
            {
                field: 'TonManual', width: '100px', title: 'Tấn nhập tay', filterable: { cell: { operator: 'equal', showOperators: false } },
            },
            {
                field: 'CBMManual', width: '100px', title: 'Khối nhập tay', filterable: { cell: { operator: 'equal', showOperators: false } },
            },
            {
                field: 'QuantityManual', width: '100px', title: 'Số lượng nhập tay', filterable: { cell: { operator: 'equal', showOperators: false } },
            },
            {
                field: 'UnitPrice', width: '100px', title: 'Đơn giá', filterable: { cell: { operator: 'equal', showOperators: false } },
            },
            {
                field: 'Credit', width: '100px', title: 'Thu', filterable: { cell: { operator: 'gte', showOperators: false } }, template: '#=Credit==null?" ":Common.Number.ToMoney(Credit)#',
            },
            {
                field: 'Debit', width: '100px', title: 'Chi', filterable: { cell: { operator: 'gte', showOperators: false } }, template: '#=Debit==null?" ":Common.Number.ToMoney(Debit)#',
            },
            {
                field: 'ManualNote', width: '100px', title: 'Ghi chú nhâp tay', filterable: { cell: { operator: 'contains', showOperators: false } },
            },
            { title: '', filterable: false, sortable: false }
        ],
        dataBound: function (e) {
            Common.Log("dataBound")
            var grid = this;

            if (Common.HasValue(grid.tbody) && Common.HasValue(grid)) {
                var data = grid.dataSource.data();
                angular.forEach(grid.items(), function (tr) {
                    var o = grid.dataItem(tr);
                    if(Common.HasValue(o)){
                        if (Common.HasValue(o.ListID) && o.ListID.length > 1 && !o.IsDuplicate)
                            $(tr).css({ "background-color": "#fda" });
                        if (o.IsDuplicate)
                            $(tr).css({ "background-color": "#48BA52; color:white;" });
                        if (Common.HasValue(o.ListManualFix) && o.ListManualFix.length > 0)
                            $(tr).find('.classcolor').css({ "color":"#ED5D09 !important;" });
                    }
                })
            }
        }
    }

    $scope.cboIDDataBound = function ($kendoEvent, item) {
        if (item && !item.dirty) {
            $kendoEvent.sender.select(function (o) { return o.ID == item.ID });
            item.dirty = true;
        }
    }

    $scope.item_uid = "";
    $scope.Detail_Click = function ($event, win, data) {
        $event.preventDefault();
        $scope.item_uid = "";
        if (Common.HasValue(data.ListDetail) && data.ListDetail.length > 0) {
            win.center();
            win.open();
            $scope.datasys_gridOptions.dataSource.data(data.ListDetail);
            $scope.item_uid = data.uid;
        } else {
            $rootScope.Message({ Msg: 'Không có dữ liệu', NotifyType: Common.Message.NotifyType.WARNING });
        }
    }
    //#region Detail
    $scope.datasys_gridOptions = {
        dataSource: Common.DataSource.Local({
            data: [],
            pageSize: 20,
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number', editable: false },
                }
            }
        }),
        height: '100%', groupable: false, pageable: true, sortable: { mode: 'multiple', allowUnsort: true }, columnMenu: false, filterable: { mode: 'row' }, resizable: true, selectable: true,
        columns: [
            {
                title: ' ', width: '45px',
                template: '<a href="/" ng-click="ManualFix_Click($event,manualFix_win, dataItem)" class="k-button classcolor"><i class="fa fa-info-circle"></i></a>',
                filterable: false, sortable: false
            },
            {
                field: 'ID', width: '100px', title: 'ID', filterable: { cell: { operator: 'equal', showOperators: false } },
            },
            {
                field: 'CustomerCode', width: '120px', title: 'Mã KH', filterable: { cell: { operator: 'contains', showOperators: false } },
            },
            {
                field: 'SOCode', width: '100px', title: 'Số SO', filterable: { cell: { operator: 'contains', showOperators: false } },
            },
            {
                field: 'DNCode', width: '100px', title: 'Số DN', filterable: { cell: { operator: 'contains', showOperators: false } },
            },
            {
                field: 'RegNo', width: '100px', title: 'Xe', filterable: { cell: { operator: 'contains', showOperators: false } },
            },
            {
                field: 'OrderCode', width: '100px', title: 'Mã đơn hàng', filterable: { cell: { operator: 'contains', showOperators: false } },
            },
            {
                field: 'RequestDate', width: '130px', title: 'Ngày gửi y/c', template: "#=RequestDate==null?' ':Common.Date.FromJsonDMY(RequestDate)#",
                filterable: {
                    cell: {
                        template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); },
                        operator: 'equal', showOperators: false,
                    }
                },
            },
            {
                field: 'LocationFromCode', width: '100px', title: 'Mã kho', filterable: { cell: { operator: 'contains', showOperators: false } },
            },
            {
                field: 'LocationToAddress', width: '250px', title: 'Đ/c giao', filterable: { cell: { operator: 'contains', showOperators: false } },
            },
            {
                field: 'Ton', width: '100px', title: 'Tấn', filterable: { cell: { operator: 'gte', showOperators: false } }, template: '#=Ton==null?" ":Common.Number.ToNumber3(Ton)#'
            },
            {
                field: 'CBM', width: '100px', title: 'Khối', filterable: { cell: { operator: 'gte', showOperators: false } }, template: '#=CBM==null?" ":Common.Number.ToNumber3(CBM)#'
            },
            {
                field: 'Quantity', width: '100px', title: 'Số lượng', filterable: { cell: { operator: 'gte', showOperators: false } }, template: '#=Quantity==null?" ":Common.Number.ToNumber3(Quantity)#'
            },
            {
                field: 'Kg', width: '100px', title: 'Kg', filterable: { cell: { operator: 'gte', showOperators: false } }, template: '#=Kg==null?" ":Common.Number.ToNumber3(Kg)#'
            },
            {
                field: 'TonTranfer', width: '100px', title: 'Tấn lấy', filterable: { cell: { operator: 'gte', showOperators: false } }, template: '#=TonTranfer==null?" ":Common.Number.ToNumber3(TonTranfer)#'
            },
            {
                field: 'CBMTranfer', width: '100px', title: 'Khối lấy', filterable: { cell: { operator: 'gte', showOperators: false } }, template: '#=CBMTranfer==null?" ":Common.Number.ToNumber3(CBMTranfer)#'
            },
            {
                field: 'QuantityTranfer', width: '100px', title: 'Số lượng lấy', filterable: { cell: { operator: 'gte', showOperators: false } }, template: '#=QuantityTranfer==null?" ":Common.Number.ToNumber3(QuantityTranfer)#'
            },
            {
                field: 'KgTranfer', width: '100px', title: 'Kg lấy', filterable: { cell: { operator: 'gte', showOperators: false } }, template: '#=KgTranfer==null?" ":Common.Number.ToNumber3(KgTranfer)#'
            },
            {
                field: 'TonBBGN', width: '100px', title: 'Tấn giao', filterable: { cell: { operator: 'gte', showOperators: false } }, template: '#=TonBBGN==null?" ":Common.Number.ToNumber3(TonBBGN)#'
            },
            {
                field: 'CBMBBGN', width: '100px', title: 'Khối giao', filterable: { cell: { operator: 'gte', showOperators: false } }, template: '#=CBMBBGN==null?" ":Common.Number.ToNumber3(CBMBBGN)#'
            },
            {
                field: 'QuantityBBGN', width: '100px', title: 'Số lượng giao', filterable: { cell: { operator: 'gte', showOperators: false } }, template: '#=QuantityBBGN==null?" ":Common.Number.ToNumber3(QuantityBBGN)#'
            },
            {
                field: 'KgBBGN', width: '100px', title: 'Kg giao', filterable: { cell: { operator: 'gte', showOperators: false } }, template: '#=KgBBGN==null?" ":Common.Number.ToNumber3(KgBBGN)#'
            },
            {
                field: 'TonReturn', width: '100px', title: 'Tấn trả về', filterable: { cell: { operator: 'gte', showOperators: false } }, template: '#=TonReturn==null?" ":Common.Number.ToNumber3(TonReturn)#'
            },
            {
                field: 'CBMReturn', width: '100px', title: 'Khối trả về', filterable: { cell: { operator: 'gte', showOperators: false } }, template: '#=CBMReturn==null?" ":Common.Number.ToNumber3(CBMReturn)#'
            },
            {
                field: 'QuantityReturn', width: '100px', title: 'Số lượng trả về', filterable: { cell: { operator: 'gte', showOperators: false } }, template: '#=QuantityReturn==null?" ":Common.Number.ToNumber3(QuantityReturn)#'
            },
            {
                field: 'KgReturn', width: '100px', title: 'Kg trả về', filterable: { cell: { operator: 'gte', showOperators: false } }, template: '#=KgReturn==null?" ":Common.Number.ToNumber3(KgReturn)#'
            },
            {
                field: 'InvoiceReturnNote', width: '100px', title: 'Ghi chú chứng từ trả về', filterable: { cell: { operator: 'contains', showOperators: false } },
            },
            {
                field: 'InvoiceReturnDate', width: '130px', title: 'Ngày chứng từ trả về', template: "#=InvoiceReturnDate==null?' ':Common.Date.FromJsonDMY(InvoiceReturnDate)#",
                filterable: {
                    cell: {
                        template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); },
                        operator: 'equal', showOperators: false,
                    }
                },
            },
            {
                field: 'ReasonCancelNote', width: '100px', title: 'Ghi chú lí do', filterable: { cell: { operator: 'contains', showOperators: false } },
            },
            {
                field: 'OPSGroupNote1', width: '100px', title: 'Ghi chú 1', filterable: { cell: { operator: 'contains', showOperators: false } },
            },
            {
                field: 'OPSGroupNote2', width: '100px', title: 'Ghi chú 2', filterable: { cell: { operator: 'contains', showOperators: false } },
            },
            {
                field: 'ORDGroupNote1', width: '100px', title: 'Ghi chú Đ/h 1', filterable: { cell: { operator: 'contains', showOperators: false } },
            },
            {
                field: 'ORDGroupNote2', width: '100px', title: 'Ghi chú Đ/h 2', filterable: { cell: { operator: 'contains', showOperators: false } },
            },
            {
                field: 'TOMasterNote1', width: '100px', title: 'Ghi chú chuyến 1', filterable: { cell: { operator: 'contains', showOperators: false } },
            },
            {
                field: 'TOMasterNote2', width: '100px', title: 'Ghi chú chuyến 2', filterable: { cell: { operator: 'contains', showOperators: false } },
            },
            {
                field: 'InvoiceNote', width: '100px', title: 'Ghi chú chứng từ', filterable: { cell: { operator: 'contains', showOperators: false } },
            },
            { title: '', filterable: false, sortable: false }
        ],
        dataBound: function (e) {
            Common.Log("dataBoundSys");
            var grid = this;

            if (Common.HasValue(grid.tbody) && Common.HasValue(grid)) {
                var data = grid.dataSource.data();
                angular.forEach(grid.items(), function (tr) {
                    var o = grid.dataItem(tr);
                    if (Common.HasValue(o)) {;
                        if (Common.HasValue(o.ListManualFix) && o.ListManualFix.length > 0)
                            $(tr).find('.classcolor').css({ "color": "#ED5D09 !important;" });
                    }
                })
            }
        }
    }

    $scope.DataSys_Accept_Click = function ($event, win, gridsys, gridexcel) {
        $event.preventDefault();

        var item = gridsys.dataItem(gridsys.select());

        if (Common.HasValue(item)) {
            if (Common.HasValue(gridexcel.tbody) && Common.HasValue(gridexcel)) {
                $rootScope.IsLoading = true;
                var data = gridexcel.dataSource.data();
                Common.Data.Each(data, function (o) {
                    if (Common.HasValue(o.uid) && o.uid == $scope.item_uid) {
                        o.ID = { ID: item.ID, Value: item.ID };
                        o.ListManualFix = item.ListManualFix;

                        o.CustomerCode = item.CustomerCode;
                        o.SOCode = item.SOCode;
                        o.DNCode = item.DNCode;
                        o.RegNo = item.RegNo;
                        o.OrderCode = item.OrderCode;
                        o.RequestDate = item.RequestDate;
                        o.LocationFromCode = item.LocationFromCode;
                        o.TonTranfer = item.TonTranfer;
                        o.CBMTranfer = item.CBMTranfer;
                        o.QuantityTranfer = item.QuantityTranfer;
                        o.KgTranfer = item.KgTranfer;
                    } else {
                        if (Common.HasValue(o.ID.ID)) {
                            o.ID = { ID: o.ID.ID, Value: o.ID.ID };
                        }
                        else {
                            o.ID = { ID: o.ID, Value: o.ID };
                        }
                    }
                })

                $scope.excel_gridOptions.dataSource.data(data);
                $rootScope.IsLoading = false;
            }
        }

        win.close();
    }
    //#endregion

    //#region manualFix
    //#region Detail
    $scope.manualFix_gridOptions = {
        dataSource: Common.DataSource.Local({
            data: [],
            pageSize: 0,
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number', editable: false },
                }
            }
        }),
        height: '100%', groupable: false, pageable: false, sortable: true, columnMenu: false, filterable: false, resizable: true, selectable: true,
        columns: [
            {
                field: 'Ton', width: '100px', title: 'Tấn', filterable: { cell: { operator: 'gte', showOperators: false } }, template: '#=Ton==null?" ":Common.Number.ToNumber3(Ton)#'
            },
            {
                field: 'CBM', width: '100px', title: 'Khối', filterable: { cell: { operator: 'gte', showOperators: false } }, template: '#=CBM==null?" ":Common.Number.ToNumber3(CBM)#'
            },
            {
                field: 'Quantity', width: '100px', title: 'Số lượng', filterable: { cell: { operator: 'gte', showOperators: false } }, template: '#=Quantity==null?" ":Common.Number.ToNumber3(Quantity)#'
            },
            {
                field: 'UnitPrice', width: '100px', title: 'Đơn giá', filterable: { cell: { operator: 'equal', showOperators: false } },
            },
            {
                field: 'Credit', width: '100px', title: 'Thu', filterable: { cell: { operator: 'gte', showOperators: false } }, template: '#=Credit==null?" ":Common.Number.ToMoney(Credit)#',
            },
            {
                field: 'Debit', width: '100px', title: 'Chi', filterable: { cell: { operator: 'gte', showOperators: false } }, template: '#=Debit==null?" ":Common.Number.ToMoney(Debit)#',
            },
            {
                field: 'Note', width: '100px', title: 'Ghi chú', filterable: { cell: { operator: 'contains', showOperators: false } },
            },
            { title: '', filterable: false, sortable: false }
        ],
    }

    $scope.ManualFix_Click = function ($event, win, data) {
        $event.preventDefault();
        if (Common.HasValue(data.ListManualFix) && data.ListManualFix.length > 0) {
            win.center();
            win.open();
            $scope.manualFix_gridOptions.dataSource.data(data.ListManualFix);
        } else {
            //$rootScope.Message({ Msg: 'Không có dữ liệu', NotifyType: Common.Message.NotifyType.WARNING });
        }
    }
    //#endregion

    $scope.excel_template_gridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.FIN,
            method: _FINManualFix_Excel.URL.Template_List,
            pageSize: 0,
            model: {
                id: 'SettingID',
                fields: {
                    SettingID: { type: 'number' }
                }
            }
        }),
        height: '100%', groupable: false, pageable: false, sortable: true, columnMenu: false,
        filterable: { mode: 'row' }, resizable: true, selectable: true,
        dataBound: function () {
            var grid = this;
            $(grid.items()).on('dblclick', function (e) {
                var tr = e.currentTarget;
                var item = grid.dataItem(tr);
                if (Common.HasValue(item)) {
                    $timeout(function () {
                        $scope.HasTemplate = true;

                        $scope.Template.ID = item.SettingID;
                        $scope.Template.Name = item.Name;
                        $scope.Template.CustomerName = item.SettingCustomerName;
                        $scope.Template.CustomerID = item.CustomerID;

                        $scope.template_win.close();
                    }, 1)
                }
            })
        },
        columns: [
            {
                field: 'Name', title: 'Tên', sortable: true,
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'SettingCustomerName', title: 'Khách hàng', sortable: true, width: "150px",
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                title: "", sortable: false, filterable: false,
            }
        ]
    }

    $scope.Template_Click = function ($event, win) {
        if (Common.HasValue($event))
            $event.preventDefault();

        $scope.excel_template_gridOptions.dataSource.read();
        if (win.wrapper.length > 0)
            win.center().open();
    }

    $scope.Excel_Template_Accept_Click = function ($event, win, grid) {
        $event.preventDefault();

        $scope.HasTemplate = false;
        $scope.Template = {
            ID: -1, Name: ''
        }

        var item = grid.dataItem(grid.select());
        if (Common.HasValue(item)) {
            $scope.HasTemplate = true;

            $scope.Template.ID = item.SettingID;
            $scope.Template.Name = item.Name;
            $scope.Template.CustomerName = item.SettingCustomerName;
            $scope.Template.CustomerID = item.CustomerID;
        }

        win.close();
    }

    $scope.UpFile_Click = function ($event, file) {
        $event.preventDefault();

        $timeout(function () {
            angular.element(file.element[0]).trigger('click');
        }, 1);
    }

    $scope.DownFile_Click = function ($event) {
        $event.preventDefault();

        $rootScope.IsLoading = true;
        Common.Services.Call($http, {
            url: Common.Services.url.FIN,
            method: _FINManualFix_Excel.URL.Template_Download,
            data: {
                templateID: $scope.Template.ID, customerID: $scope.Template.CustomerID, dtfrom: $scope.Item.DateFrom, dtto: $scope.Item.DateTo
            },
            success: function (res) {
                $rootScope.IsLoading = false;
                Common.Services.Error(res, function (res) {
                    $rootScope.DownloadFile(res);
                })
            }
        });
    }

    $scope.excel_fileOptions = {
        async: {
            saveUrl: '/Handler/File.ashx',
            autoUpload: true
        },
        multiple: false,
        showFileList: false,
        upload: function (e) {
            var xhr = e.XMLHttpRequest;
            xhr.addEventListener('readystatechange', function (e) {
                if (xhr.readyState == 1)
                    xhr.setRequestHeader('auth', Common.Auth.HeaderKey);
            });
            e.data = { 'folderPath': Common.FolderUpload.Import }
        },
        success: function (e) {
            $scope.File = e.response;
            $scope.File.ReferID = $rootScope.FunctionItem.ID;
            $scope.File.TypeOfFileCode = Common.CATTypeOfFileCode.DIPOD;

            $scope.Excel_Check();
        }
    };

    $scope.Excel_Check = function () {
        $rootScope.IsLoading = true;

        Common.Services.Call($http, {
            url: Common.Services.url.FIN,
            method: _FINManualFix_Excel.URL.Check,
            data: {
                file: $scope.File.FilePath,
                templateID: $scope.Template.ID,
                customerID: $scope.Template.CustomerID,
                dtfrom: $scope.Item.DateFrom, dtto: $scope.Item.DateTo
            },
            success: function (res) {
                $rootScope.IsLoading = false;
                Common.Services.Error(res, function (res) {
                    $rootScope.Message({
                        Msg: "Đã kiểm tra."
                    })
                    $scope.IsExcelChecked = true;
                    _FINManualFix_Excel.Data.FromFile = res;
                    $scope.excel_gridOptions.dataSource.data(res);
                })
            }
        });
    }

    $scope.Import_Click = function ($event) {
        $event.preventDefault();

        var grid = $scope.excel_grid;

        var importData = grid.dataSource.data();
        var lstError = [];
        var errorID = false;
        var i = 0;
        $.each(importData, function (i, item) {
            if (!item.ExcelSuccess) {
                if (lstError.indexOf(item.ID) < 0)
                    lstError.push(item.ID);
            } else if (!Common.HasValue(item.ID) || item.ID == "" || item.ID < 0) {
                errorID = true;
            } else if (item.ID.ID > 0) {
                _FINManualFix_Excel.Data.FromFile[i].ID = item.ID.ID;
            }
        })

        if (errorID) {
            $rootScope.Message({ Msg: 'Chưa chọn ID', NotifyType: Common.Message.NotifyType.ERROR });
        }
        else {
            var strConfirm = 'Xác nhận lưu?';
            if (lstError.length > 0) {
                var strError = lstError.join(", ");
                strConfirm = "Dòng ID: (" + strError + ") có lỗi, tiếp tục lưu?";
            }
            $rootScope.Message({
                Type: Common.Message.Type.Confirm,
                Msg: strConfirm,
                Ok: function () {
                    $rootScope.IsLoading = true;
                    var data = _FINManualFix_Excel.Data.FromFile;

                    Common.Services.Call($http, {
                        url: Common.Services.url.FIN,
                        method: _FINManualFix_Excel.URL.Import,
                        data: {
                            Data: data,
                            TemplateID: $scope.Template.ID,
                        },
                        success: function (res) {
                            $rootScope.IsLoading = false;
                            Common.Services.Error(res, function () {
                                $rootScope.Message({ Msg: "Thành công!" });
                                grid.dataSource.data([]);
                                $scope.IsExcelChecked = false;
                            })
                        }
                    });
                }
            })
        }
    }


    $timeout(function () {
        $scope.Template_Click(null, $scope.template_win);
    }, 100)


    $scope.Back_Click = function ($event) {
        $event.preventDefault();
        $state.go("main.FINManualFix.Index");
    }

    $scope.Close_Click = function ($event, win) {
        $event.preventDefault();

        win.close();
    }
}]);