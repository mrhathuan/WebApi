/// <reference path="~/Scripts/jquery-1.9.1.intellisense.js" />
/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />

//#region Data
var _FINFreightAuditCUS_Excel = {
    URL: {
        List_Customer_Save: "Customer_List",
        Check_Excel: 'FINFreightAuditCus_ExcelCheck',
        Export_Excel: 'FINFreightAuditCus_ExcelExport',
        ImportReject: 'FINFreightAuditCus_ImportReject',
        ImportAccept: 'FINFreightAuditCus_ImportAccept'
    },
}
//#endregion

angular.module('myapp').controller('FINFreightAuditCustomer_ExcelCtrl', ['$rootScope', '$scope', '$http', '$location', '$state', '$timeout', function ($rootScope, $scope, $http, $location, $state, $timeout, $stateParams) {
    if ($rootScope.IsPageComplete != true) return;
    Common.Log('FINFreightAuditCustomer_ExcelCtrl');
    $rootScope.IsLoading = false;
    $scope.HasChoose = false;
    $scope.File = "";
    $scope.HasChoose = false;

    //#region Auth
    $scope.Auth = $rootScope.GetAuth();
    //#endregion

    //Excel
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
            method: _FINFreightAuditCUS_Excel.URL.Export_Excel,
            data: {},
            success: function (res) {
                $rootScope.DownloadFile(res);
                $rootScope.IsLoading = false;
            }
        })
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
            $scope.Excel_Check();
        }
    };
    
    $scope.Excel_Check = function () {
        $rootScope.IsLoading = true;
        Common.Services.Call($http, {
            url: Common.Services.url.FIN,
            method: _FINFreightAuditCUS_Excel.URL.Check_Excel,
            data: { File: $scope.File.FilePath, customerID: $scope.customerID },
            success: function (data) {
                $rootScope.IsLoading = false;
                $.each(data, function (i, v) {
                    v.IsChoose = false;
                })
                $scope.excel_FreightAuditCus_gridOptions.dataSource.data(data);
            }
        })
    }

    $scope.excel_FreightAuditCus_gridOptions = {
        dataSource: Common.DataSource.Local({
            model: {
                id: 'ID',
                fields: {
                    ExcelSuccess: { type: 'boolane' },
                    IsChoose: { type: 'bool', defaultValue: false },
                }
            }
        }),
        height: '100%', pageable: false, sortable: true, columnMenu: false, filterable: { mode: 'row' }, resizable: true,
        dataBound: function () {
            this.expandRow(this.tbody.find("tr.k-master-row").first());
        },
        columns: [
               {
                   title: ' ', width: '40px',
                   headerTemplate: '<input class="chkSelectAll"  type="checkbox" ng-click="gridChooseAll_Check($event,excel_FreightAuditCus_grid,excel_FreightAuditCus_gridChooseChange)" />',
                   headerAttributes: { style: 'text-align: center;' },
                   template: '<input class="chkChoose" ng-show="dataItem.ExcelSuccess == true" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,excel_FreightAuditCus_grid,excel_FreightAuditCus_gridChooseChange)" />',
                   templateAttributes: { style: 'text-align: center;' },
                   filterable: false, sortable: false
               },
             {
                 field: 'ExcelSuccess', title: 'Thành công', template: '<input type="checkbox" #= ExcelSuccess == true ? "checked=checked" : "" #  disabled >', width: '100px',
                 filterable: {
                     cell: {
                         template: function (container) {
                             container.element.kendoComboBox({
                                 dataSource: [{ Text: 'Không thành công', Value: false }, { Text: 'Thành công', Value: true }, { Text: 'Tất cả', Value: '' }],
                                 dataTextField: "Text", dataValueField: "Value",
                             });
                         },
                         showOperators: false
                     }
                 }
             },
              {
                  field: 'ExcelError', title: 'Thông báo', width: '150px',
                  filterable: { cell: { operator: 'contains', showOperators: false } }
              },
             {
                 field: 'OrderCode', title: 'Mã đơn hàng', width: '125px',
                 filterable: { cell: { operator: 'contains', showOperators: false } }
             },
            {
                field: 'Credit', title: 'Chi phí', width: '125px', template: '#=Credit==null?" ":Common.Number.ToMoney(Credit)#',
                filterable: { cell: { operator: 'gte', showOperators: false } }
            },
            {
                field: 'SysCredit', title: 'Chi phí hệ thống', width: '125px', template: '#=SysCredit==null?" ":Common.Number.ToMoney(SysCredit)#',
                filterable: { cell: { operator: 'gte', showOperators: false } }
            },
            {
                field: 'Result', title: 'Kết quả', width: '125px', template: '#=Result==null?" ":Common.Number.ToMoney(Result)#',
                filterable: { cell: { operator: 'gte', showOperators: false } }
            },
            { title: ' ', filterable: false, sortable: false }
        ],
    }

    $scope.excel_FreightAuditCus_gridChooseChange = function ($event, grid, haschoose) {
        $scope.HasChoose = haschoose;
    };

    $scope.cboCustomer_Options = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true, dataTextField: 'CustomerName', dataValueField: 'ID',
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    CustomerName: { type: 'string' },
                }
            }
        }),
        change: function (e) {
        }
    };
    
    Common.Services.Call($http, {
        url: Common.Services.url.FIN,
        method: _FINFreightAuditCUS_Excel.URL.List_Customer_Save,
        success: function (res) {
            $scope.cboCustomer_Options.dataSource.data(res.Data);
            $scope.customerID = res.Data[0].ID;
        }
    })

    $scope.ImportReject_Click = function ($event, win) {
        $event.preventDefault();
        win.center().open();
        $scope.reject_Error = "";
    }

    $scope.ExcelRejec_Click = function ($event, grid, win) {
        $event.preventDefault();
        var IDText = "";
        var lst = [];
        $rootScope.IsLoading = true;
        $.each(grid.dataSource.data(), function (i, v) {
            if (v.IsChoose == true) {
                IDText = IDText + " " + v.OrderID;
                lst.push(v.OrderID);
            }
        });
        if (Common.HasValue(lst)) {
            Common.Services.Call($http, {
                url: Common.Services.url.FIN,
                method: _FINFreightAuditCUS_Excel.URL.ImportReject,
                data: { lst: lst, Note: IDText +" "+ $scope.reject_Error },
                success: function (res) {
                    $rootScope.IsLoading = false;
                    $rootScope.Message({
                        Msg: 'Đã báo kiểm tra lại.',
                        NotifyType: Common.Message.NotifyType.SUCCESS
                    });
                    win.close();
                }
            })
        }
        else {
            $rootScope.IsLoading = false;
        }

    }

    $scope.ImportAccept_Click = function($event, grid){
        $event.preventDefault();
        var lst = [];
        $.each(grid.dataSource.data(), function (i, v) {
            if (v.IsChoose == true) {
                lst.push(v.OrderID);
            }
        });
        if (Common.HasValue(lst)) {
            $rootScope.Message({
                Msg: "Xác nhận hoàn tất ?",
                Type: Common.Message.Type.Confirm,
                Ok: function () {
                    $rootScope.IsLoading = true;
                    Common.Services.Call($http, {
                        url: Common.Services.url.FIN,
                        method: _FINFreightAuditCUS_Excel.URL.ImportAccept,
                        data: { lst: lst, Note: "" },
                        success: function (res) {
                            $rootScope.IsLoading = false;
                            $rootScope.Message({
                                Msg: 'Đã cập nhật.',
                                NotifyType: Common.Message.NotifyType.SUCCESS
                            });
                            win.close();
                        }
                    })
                }
            })
        }
    }

    $scope.Window_Close_Click = function ($event, win) {
        $event.preventDefault();
        win.close();
    }

    $scope.ShowSetting = function ($event) {
        $event.preventDefault();

        $rootScope.ShowSetting({
            ListView: views.FINFreightAuditCustomer,
            event: $event,
            current: $state.current
        });
    };

    $scope.HideSetting = function ($event) {
        $event.preventDefault();

        $rootScope.HideSetting();
    };
}]);