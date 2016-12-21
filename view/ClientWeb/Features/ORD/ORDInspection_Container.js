/// <reference path="~/Scripts/jquery-1.9.1.intellisense.js" />
/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />

//#region Data
var _ORDOrder_Container = {
    URL: {
        List: 'ORDOrder_DocumentContainer_Read',
        Document_Get: 'ORDOrder_Document_Get',
        Document_Read: 'ORDOrder_TypeOfDocument_Read',
        Document_Save: 'ORDOrder_Document_Save',
        DeleteList: 'ORDOrder_DocumentContainer_DeleteList',
        DocumentService_List: 'ORDOrder_DocumentService_List',
        Service_NotInList: 'ORDOrder_DocumentService_NotInList',
        NotInList_Save: 'ORDOrder_DocumentService_NotInList_Save',
        Service_DeleteList: 'ORDOrder_DocumentService_DeleteList',
        Container_List: 'ORDOrder_DocumentContainer_List',
        Container_NotInList: 'ORDOrder_DocumentContainer_NotInList',
        Container_NotInList_Save: 'ORDOrder_DocumentContainer_NotInList_Save',
        Container_DeleteList: 'ORDOrder_DocumentContainer_DeleteList',
        ContainerInService_List: 'ORDOrder_ContainerInService_List',
        CustomerList: 'ORDOrder_CustomerList',
        VendorList: 'ORDOrder_VendorList',
        ContainerInService_Save: 'ORDOrder_ContainerInService_NotInList_Save',

        Check_Excel: 'ORDOrder_DocumentDetail_ExcelCheck',
        Save_Excel: 'ORDOrder_DocumentDetail_ExcelSave',
        Export_Excel: 'ORDOrder_DocumentDetail_ExcelExport',

        OrderList: 'ORDOrder_Document_OrderList',
    },
    Data: {
        ListContainer: [],
        ListContainerService: [],
        ListService: [],
    }
};
//#endregion

angular.module('myapp').controller('ORDInspection_ContainerCtrl', ['$rootScope', '$scope', '$http', '$location', '$state', '$timeout', '$stateParams', function ($rootScope, $scope, $http, $location, $state, $timeout, $stateParams) {
    Common.Log('ORDInspection_ContainerCtrl');
    $rootScope.IsLoading = false;
    $scope.Search = { DateFrom: new Date().addDays(-3), DateTo: new Date().addDays(3), ListCustomerID: [] };
    $scope.Item = {};
    $scope.TabIndex = 1;
    $scope.documentID = 0;
    $scope.HasChoose = false;
    $scope.HasDelete = false;
    $scope.HasDeleteContainer = false;
    $scope.HasChooseContainer = false;
    $scope.HasDeleteInspection = false;

    $scope.ORDDOCContainer_gridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.ORD,
            method: _ORDOrder_Container.URL.List,
            readparam: function () {
                return {
                    DateFrom: $scope.Search.DateFrom,
                    DateTo: $scope.Search.DateTo,
                    lstCustomerID: $scope.Search.ListCustomerID,
                }
            },
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    DateDocument: { type: 'date' },
                    IsChoose: { type: 'bool', defaultValue: false },
                }
            }
        }),
        height: '100%', pageable: true, sortable: true, columnMenu: false, resizable: true, reorderable: true, filterable: { mode: 'row' },
        columns: [
           {
                title: ' ', width: '85px',
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,ORDDOCContainer_grid,ORDInspection_gridChooseChange)" />',
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,ORDDOCContainer_grid,ORDInspection_gridChooseChange)" />' + '<a href="/" ng-click="Edit_Click($event,dataItem,ORDInspection_win)" class="k-button" data-title="Tìm"><i class="fa fa-pencil"></i></a>',
                filterable: false, sortable: false
          },
         {
             field: 'DateDocument', width: 120, title: 'Ngày dịch vụ', template: "#=DateDocument==null?' ':kendo.toString(DateDocument, '" + Common.Date.Format.DMY + "')#",
             filterable: { cell: { template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); }, operator: 'equal', showOperators: false } },
         },
         {
             field: 'OrderCode', width: 120, title: 'Mã đơn hàng',
             filterable: { cell: { operator: 'contains', showOperators: false } }
         },
          {
              field: 'PackingCode', width: 120, title: 'Loại Con',
              filterable: { cell: { operator: 'contains', showOperators: false } }
          },
           {
               field: 'ContainerNo', width: 120, title: 'Số Con',
                filterable: { cell: { operator: 'contains', showOperators: false } }
           },
           {
               field: 'SealNo1', width: 120, title: 'Số Seal1',
               filterable: { cell: { operator: 'contains', showOperators: false } }
           },
           {
               field: 'SealNo2', width: 120, title: 'Số Seal2',
               filterable: { cell: { operator: 'contains', showOperators: false } }
           },
           {
                field: 'CustomerCode', width: 120, title: 'Mã khách hàng',
                filterable: { cell: { operator: 'contains', showOperators: false } }
           },
           {
               field: 'CustomerName', width: 120, title: 'Tên khách hàng',
               filterable: { cell: { operator: 'contains', showOperators: false } }
           },
           { title: '', filterable: false, sortable: false }
        ]
    };

    $scope.Search_Click = function ($event) {
        $event.preventDefault();
        $scope.ORDDOCContainer_gridOptions.dataSource.read();
    };

    $scope.Edit_Click = function ($event, data, win) {
        $event.preventDefault();
        $scope.documentID = data.DocumentID;
        $scope.load_ContainerService();
        $scope.load_Service_List();
        $scope.LoadData_Container(data.DocumentID, win);
    };

    $scope.LoadData_Container = function (ID, win) {
        $rootScope.IsLoading = true;
        Common.Services.Call($http, {
            url: Common.Services.url.ORD,
            method: _ORDOrder_Container.URL.Document_Get,
            data: { ID: ID },
            success: function (res) {
                if (Common.HasValue(res)) {
                    $scope.Item = res;       
                    win.center().open();
                    $rootScope.IsLoading = false;

                }
            }
        });
    }

    $scope.Container_Delete_Click = function ($event, grid) {
        $event.preventDefault();
        $rootScope.Message({
            Msg: "Xác nhận xóa?",
            Type: Common.Message.Type.Confirm,
            Ok: function () {
                var data = [];
                $rootScope.IsLoading = true;
                $.each(grid.dataSource.data(), function (i, v) {
                    if (v.IsChoose == true) {
                        data.push(v.ID);
                    }
                });
                if (Common.HasValue(data)) {
                    Common.Services.Call($http, {
                        url: Common.Services.url.ORD,
                        method: _ORDOrder_Container.URL.DeleteList,
                        data: { lstID: data },
                        success: function (res) {
                            win.close();
                            $scope.ORDDOCContainer_grid.dataSource.read();
                            $rootScope.Message({
                                Msg: 'Đã xóa.',
                                NotifyType: Common.Message.NotifyType.SUCCESS
                            });
                            $rootScope.IsLoading = false;
                        }
                    });
                }
            }
        })
    }

    $scope.cboTypeOfDocument_Options = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true, dataTextField: 'TypeName', dataValueField: 'ID',
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    TypeName: { type: 'string' },
                }
            }
        }),
        change: function (e) {
        }
    }

    $scope.cboCustomer_Options = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true, dataTextField: 'CustomerName', dataValueField: 'ID',
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    TypeName: { type: 'string' },
                }
            }
        }),
        change: function (e) {
        }
    }

    $scope.cboVendor_Options = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true, dataTextField: 'CustomerName', dataValueField: 'ID',
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    TypeName: { type: 'string' },
                }
            }
        }),
        change: function (e) {
        }
    }

    $scope.Load_cboTypeOfDocument = function () {

        Common.Services.Call($http, {
            url: Common.Services.url.ORD,
            method: _ORDOrder_Container.URL.CustomerList,
            success: function (res) {
                $scope.cboCustomer_Options.dataSource.data(res.Data);
                $scope.mltCustomerOptions.dataSource.data(res.Data);
            }
        });
        Common.Services.Call($http, {
            url: Common.Services.url.ORD,
            method: _ORDOrder_Container.URL.Document_Read,
            success: function (res) {
                $scope.cboTypeOfDocument_Options.dataSource.data(res);
            }
        });
        Common.Services.Call($http, {
            url: Common.Services.url.ORD,
            method: _ORDOrder_Container.URL.VendorList,
            success: function (res) {
                $scope.cboVendor_Options.dataSource.data(res.Data);
            }
        });
    }
    $scope.Load_cboTypeOfDocument();

    $scope.Inspection_Save_Click = function ($event, win, grid, vform) {
        $event.preventDefault();
        if (vform()) {
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.ORD,
                method: _ORDOrder_Container.URL.Document_Save,
                data: { item: $scope.Item },
                success: function (res) {
                    win.close();
                    grid.dataSource.read();
                    $rootScope.Message({
                        Msg: 'Đã lưu.',
                        NotifyType: Common.Message.NotifyType.SUCCESS
                    });
                    $rootScope.IsLoading = false;
                }
            })
        } else {
            $rootScope.Message({
                Msg: 'Chưa nhập đầy đủ thông tin.',
                NotifyType: Common.Message.NotifyType.ERROR
            });
        }
    }

    //#region service

    $scope.ORDService_gridOptions = {
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    IsChoose: { type: 'bool', defaultValue: false },
                }
            }
        }),
        height: '100%', pageable: false, sortable: true, columnMenu: false, resizable: true, reorderable: true, filterable: { mode: 'row' },
        columns: [
            {
                title: ' ', width: '85px',
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,ORDService_grid,ORDService_gridChooseChange)" />',
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,ORDService_grid,ORDService_gridChooseChange)" />' +
                    '<a href="/" ng-click="Detail_Click($event,dataItem,DocumentDetail_win)" ng-show="dataItem.IsDetail == true" class="k-button" data-title="Tìm"><i class="fa fa-file-text"></i></a>',
                filterable: false, sortable: false
            },
            { field: 'ServiceName', title: 'Tên dịch vụ', width: 180, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'IsComplete', title: 'Tình trạng', width: 150, template: '<input class="cus-combobox" focus-k-combobox  kendo-combo-box="Complete_cboGroup" ng-model="dataItem.IsCompleteConvert" k-options="cboComplete_Options" value="dataItem.IsCompleteConvert"/>', filterable: { cell: { operator: 'contains', showOperators: false } } },
            {
                field: 'PriceCustomer', title: 'Giá thu', template: '<input kendo-numeric-text-box k-options="PriceCustomer_options" k-min="0" k-spinners="false" ng-model="dataItem.PriceCustomer" style="width:100%"/>', width: 150,
            },
            {
                field: 'PriceVendor', title: 'Giá chi', template: '<input kendo-numeric-text-box k-options="PriceVendor_options" k-min="0" k-spinners="false" ng-model="dataItem.PriceVendor" style="width:100%"/>', width: 150,
            },
            {
                field: 'Quantity', title: 'Số lượng', template: '<input kendo-numeric-text-box ng-show="dataItem.IsQuantity == true" ng-model="dataItem.Quantity" k-options="PriceVendor_options" k-min="0" k-spinners="false" style="width:100%"/>', width: 150,
            },
            { title: '', filterable: false, sortable: false },
        ]
    }

    $scope.PriceVendor_options = { format: 'n0', spinners: false, culture: 'en-US', min: 0, step: 1, decimals: 0, }
    $scope.PriceCustomer_options = { format: 'n0', spinners: false, culture: 'en-US', min: 0, step: 1, decimals: 0, }

    $scope.cboComplete_Options = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true, dataTextField: 'TypeName', dataValueField: 'ID',
        dataSource: Common.DataSource.Local({
            data: [
                { ID: 1, value: true, TypeName: 'Đã duyệt' },
                { ID: 2, value: false, TypeName: 'Chưa duyệt' }
            ],
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    value: { type: 'boolean' },
                    TypeName: { type: 'string' },
                }
            }
        }),
        change: function (e) {
            var DIVal = this.value();
            var tr = $(e.sender.element).closest('tr');
            var dataItem = $scope.ORDService_grid.dataItem(tr);
            var data = $scope.ORDService_grid.dataSource.data();
            $.each(data, function (i, v) {
                if (v.ID == dataItem.ID) {
                    if (DIVal == 2) {
                        v.IsComplete = false;
                    } else v.IsComplete = true;
                }
            });
            $scope.ORDService_grid.dataSource.data(data);
        }
    };

    $scope.load_Service_List = function () {
        Common.Services.Call($http, {
            url: Common.Services.url.ORD,
            method: _ORDInspection_Index.URL.DocumentService_List,
            data: { documentID: $scope.documentID },
            success: function (res) {
                $.each(res, function (i, v) {
                    v.IsChoose = false;
                    if (v.IsComplete == true) {
                        v.IsCompleteConvert = 1;
                    }
                    else {
                        v.IsCompleteConvert = 2;
                    }
                });
                $scope.ORDService_gridOptions.dataSource.data(res);
            }
        })
    }

    $scope.InspectionService_Save_Click = function ($event, grid) {
        $event.preventDefault();

        $rootScope.IsLoading = true;
        var data = grid.dataSource.data();
        Common.Services.Call($http, {
            url: Common.Services.url.ORD,
            method: _ORDInspection_Index.URL.DocumentService_Save,
            data: { documentID: $scope.documentID, lstService: data },
            success: function (res) {
                $scope.load_Service_List();
                $rootScope.IsLoading = false;
                $rootScope.Message({
                    Msg: 'Đã cập nhật.',
                    NotifyType: Common.Message.NotifyType.SUCCESS
                });
            }
        })
    }

    $scope.DocumentDetail_gridOptions = {
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    IsChoose: { type: 'bool', defaultValue: false },
                }
            }
        }),
        height: '100%', pageable: false, sortable: true, columnMenu: false, resizable: true, reorderable: true, filterable: { mode: 'row' },
        columns: [
            {
                title: ' ', width: '40px',
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,DocumentDetail_grid,DocumentDetail_gridChooseChange)" />',
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,DocumentDetail_grid,DocumentDetail_gridChooseChange)" />',
                filterable: false, sortable: false
            },
            {
                field: 'Code', title: 'Mã', width: 150,
                template: '<input type="text" class="k-textbox" style="width: 100%" ng-model="dataItem.Code" />'
            },
            {
                field: 'SendDate', title: 'Ngày gửi', width: 170,
                template: '<input class="cus-datepicker SendDate" />',
                filterable: {
                    cell: {
                        template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); }, operator: 'equal', showOperators: false
                    }
                },
            },
            {
                field: 'Note', title: 'Ghi chú', template: "#=Note == null ? '':Note #", width: 150,
                template: '<input type="text" class="k-textbox" style="width: 100%" ng-model="dataItem.Note" />'
            },
            { title: '', filterable: false, sortable: false },
        ], dataBound: function (e) {
            var grid = this;
            if (Common.HasValue(grid) && Common.HasValue(grid.tbody)) {
                var lst = grid.tbody.find('tr');
                $.each(lst, function (i, tr) {
                    var item = grid.dataItem(tr);

                    var classSendDate = $(tr).find('.SendDate');

                    var classSendDate = classSendDate.kendoDateTimePicker({
                        value: item.SendDate,
                        format: Common.Date.Format.DMYHM,
                        timeFormat: Common.Date.Format.HM,
                        change: function (e) {
                            item.SendDate = this.value();
                        }
                    }).data("kendoDateTimePicker");
                })
            }
        }
    }

    $scope.IsCompleteAll_Check = function ($event, grid) {
        var data = grid.dataSource.data();
        if ($event.target.checked == false) {
            $.each(data, function (i, v) {
                v.IsComplete = false;
            });
        } else {
            $.each(data, function (i, v) {
                v.IsComplete = true;
            });
        }
        grid.dataSource.data(data);
    }

    $scope.DocumentDetail_Save_Click = function ($event, gird, win) {
        $event.preventDefault();
        $rootScope.IsLoading = true;
        var data = gird.dataSource.data();
        Common.Services.Call($http, {
            url: Common.Services.url.ORD,
            method: _ORDInspection_Index.URL.DocumentDetail_SaveList,
            data: { documentServiceID: $scope.documentServiceID, lstDetail: data },
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

    $scope.DocumentDetail_Delete_Click = function ($event, win, grid) {
        $event.preventDefault();
        $rootScope.Message({
            Msg: "Xác nhận xóa?",
            Type: Common.Message.Type.Confirm,
            Ok: function () {
                $rootScope.IsLoading = true;
                var data = [];
                $.each(grid.dataSource.data(), function (i, v) {
                    if (v.IsChoose == true) {
                        data.push(v.ID);
                    }
                });
                Common.Services.Call($http, {
                    url: Common.Services.url.ORD,
                    method: _ORDInspection_Index.URL.DocumentDetail_DeleteList,
                    data: { lstID: data },
                    success: function (res) {
                        $rootScope.IsLoading = false;
                        $rootScope.Message({
                            Msg: 'Đã xóa.',
                            NotifyType: Common.Message.NotifyType.SUCCESS
                        });
                        win.close();
                    }
                })
            }
        })
    }

    $scope.DocumentDetail_ExcelClick = function ($event, win) {
        $event.preventDefault();

        var request = Common.Request.CreateFromGrid($scope.DocumentDetail_gridOptions.dataSource);

        $rootScope.UploadExcel({
            Path: Common.FolderUpload.Import,
            columns: [
              { field: 'Code', title: 'Mã', width: 150, },
              { field: 'SendDate', title: 'Ngày gửi', width: 150, template: "#=SendDate==null?' ':Common.Date.FromJsonDMY(SendDate)#" },
              { field: 'Note', title: 'Ghi chú', template: "#=Note == null ? '':Note #", width: 150, },
              { field: 'IsComplete', title: '', width: 100, template: "<input type='checkbox' class='checkComplete' ng-model='dataItem.IsComplete'></input>", },
              { field: 'CompleteBy', title: 'Người hoàn thành', width: 150, },
              { field: 'CompleteDate', title: 'Ngày hoàn thành', width: 150, template: "#=CompleteDate==null?' ':Common.Date.FromJsonDMY(CompleteDate)#" },
            ],
            Download: function () {
                $rootScope.IsLoading = true;
                Common.Services.Call($http, {
                    url: Common.Services.url.ORD,
                    method: _ORDInspection_Index.URL.Export_Excel,
                    data: { documentServiceID: $scope.documentServiceID },
                    success: function (res) {
                        $rootScope.DownloadFile(res);
                        $rootScope.IsLoading = false;
                    }
                })
            },
            Upload: function (e, callback) {
                $rootScope.IsLoading = true;
                Common.Services.Call($http, {
                    url: Common.Services.url.ORD,
                    method: _ORDInspection_Index.URL.Check_Excel,
                    data: { item: e },
                    success: function (data) {
                        callback(data);
                        $rootScope.IsLoading = false;
                    }
                })
            },
            Window: win,
            Complete: function (e, data) {
                $rootScope.IsLoading = true;
                Common.Services.Call($http, {
                    url: Common.Services.url.ORD,
                    method: _ORDInspection_Index.URL.Save_Excel,
                    data: { documentServiceID: $scope.documentServiceID, lst: data },
                    success: function (res) {
                        Common.Services.Call($http, {
                            url: Common.Services.url.ORD,
                            method: _ORDInspection_Index.URL.DocumentDetail_List,
                            data: { documentServiceID: $scope.documentServiceID },
                            success: function (res) {
                                $.each(res, function (i, v) {
                                    v.IsChoose = false;
                                });
                                $scope.DocumentDetail_grid.dataSource.data(res);
                            }
                        })
                        $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                        $rootScope.IsLoading = false;
                    }
                })
            }
        })
    }

    $scope.Detail_Click = function ($event, data, win) {
        $event.preventDefault();
        $scope.documentServiceID = data.ID;
        Common.Services.Call($http, {
            url: Common.Services.url.ORD,
            method: _ORDInspection_Index.URL.DocumentDetail_List,
            data: { documentServiceID: data.ID },
            success: function (res) {
                $.each(res, function (i, v) {
                    v.IsChoose = false;
                });
                $scope.DocumentDetail_grid.dataSource.data(res);
                $timeout(function () {
                    $scope.DocumentDetail_grid.resize();
                }, 1);
                win.center().open();
            }
        })
    }

    $scope.ORDServiceNotList_gridOptions = {
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    IsChoose: { type: 'bool', defaultValue: false },
                }
            }
        }),
        height: '100%', pageable: false, sortable: true, columnMenu: false, resizable: true, reorderable: true, filterable: { mode: 'row' },
        columns: [
           {
               title: ' ', width: '40px',
               headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,ORDServiceNotList_grid,ORDServiceNotList_gridChooseChange)" />',
               headerAttributes: { style: 'text-align: center;' },
               template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,ORDServiceNotList_grid,ORDServiceNotList_gridChooseChange)" />',
               templateAttributes: { style: 'text-align: center;' },
               filterable: false, sortable: false
           },
            { field: 'ServiceCode', title: 'Mã dịch vụ', width: 150, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'ServiceName', title: 'Tên dịch vụ', width: 150, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { title: '', filterable: false, sortable: false },
        ]
    }
    $scope.DocumentDetail_gridChooseChange = function ($event, grid, haschoose) {
        $scope.HasDocumentDetail = haschoose;
    };
    $scope.ORDServiceNotList_gridChooseChange = function ($event, grid, haschoose) {
        $scope.HasChoose = haschoose;
    };
    $scope.ORDService_gridChooseChange = function ($event, grid, haschoose) {
        $scope.HasDelete = haschoose;
    };
    $scope.ORDContainer_gridChooseChange = function ($event, grid, haschoose) {
        $scope.HasDeleteContainer = haschoose;
    };

    $scope.ORDInspection_gridChooseChange = function ($event, grid, haschoose) {
        $scope.HasDeleteInspection = haschoose;
    };

    $scope.InspectionService_Add_Click = function ($event, win, grid) {
        $event.preventDefault();
        $rootScope.IsLoading = true;
        Common.Services.Call($http, {
            url: Common.Services.url.ORD,
            method: _ORDInspection_Index.URL.Service_NotInList,
            data: { documentID: $scope.documentID },
            success: function (res) {
                $.each(res, function (i, v) {
                    v.IsChoose = false;
                });
                win.center().open();
                grid.dataSource.data(res);
                $rootScope.IsLoading = false;
            }
        });
    }

    $scope.InspectionService_Delete_Click = function ($event, grid) {
        $event.preventDefault();

        $rootScope.Message({
            Msg: "Xác nhận xóa?",
            Type: Common.Message.Type.Confirm,
            Ok: function () {
                $rootScope.IsLoading = true;
                var data = [];
                $.each(grid.dataSource.data(), function (i, v) {
                    if (v.IsChoose == true) {
                        data.push(v.ID);
                    }
                });
                if (Common.HasValue(data)) {
                    Common.Services.Call($http, {
                        url: Common.Services.url.ORD,
                        method: _ORDInspection_Index.URL.Service_DeleteList,
                        data: { lstID: data },
                        success: function (res) {
                            grid.dataSource.read();
                            $rootScope.Message({
                                Msg: 'Đã Xóa.',
                                NotifyType: Common.Message.NotifyType.SUCCESS
                            });
                            $rootScope.IsLoading = false;
                        }
                    });
                }
            }
        })
    }

    $scope.ServiceNotList_Save_Click = function ($event, win, gridnolist) {
        $event.preventDefault();
        var data = [];
        $rootScope.IsLoading = true;
        $.each(gridnolist.dataSource.data(), function (i, v) {
            if (v.IsChoose == true) {
                data.push(v.ID);
            }
        });

        if (Common.HasValue(data)) {
            Common.Services.Call($http, {
                url: Common.Services.url.ORD,
                method: _ORDInspection_Index.URL.NotInList_Save,
                data: { documentID: $scope.documentID, lstServiceID: data },
                success: function (res) {
                    win.close();
                    $scope.load_Service_List();
                    $rootScope.Message({
                        Msg: 'Đã lưu.',
                        NotifyType: Common.Message.NotifyType.SUCCESS
                    });
                    $rootScope.IsLoading = false;
                }
            });
        }

    }
    //#endregion

    //#region container
    $scope.load_ContainerService = function () {
        Common.Services.Call($http, {
            url: Common.Services.url.ORD,
            method: _ORDInspection_Index.URL.ContainerInService_List,
            data: { documentID: $scope.documentID },
            success: function (res) {
                _ORDInspection_Index.Data.ListContainer = res.ListContainer;
                _ORDInspection_Index.Data.ListContainerService = res.ListContainerService;
                _ORDInspection_Index.Data.ListService = res.ListService;
                $timeout(function () {
                    $scope.InitGrid();
                }, 100);
            }
        })
    }

    $scope.InitGrid = function () {
        Common.Log("InitGrid");
        var Model = {
            id: 'ID',
            fields: {
                ID: { type: 'number', editable: false },
            },
        }
        var GridColumn = [
            { field: 'OrderCode', title: 'Mã đơn hàng', width: 150, locked: true, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'ContainerNo', title: 'Mã container', width: '120px', template: '', locked: true, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'PackingCode', title: 'Loại Con', width: 150, locked: true, filterable: { cell: { operator: 'contains', showOperators: false } } },
        ]

        $scope.fieldGrid = [];
        Common.Data.Each(_ORDInspection_Index.Data.ListService, function (Service) {
            var listCol = [];
            var field = "Se" + Service.ServiceID;
            Model.fields[field] = { type: "boolean", editable: true };
            GridColumn.push({
                field: field, title: '', width: "150px",
                headerTemplate: '<input class="chkSelectAll ' + field + '" type="checkbox" ng-model="checkall" ng-click="IsCompleteAll_Check($event,ORDContainerService_grid)" />' + Service.ServiceName,
                template: "<input type='checkbox' class='chkSelect " + field + "' ng-model='dataItem." + field + "'></input>", templateAttributes: { style: 'text-align: center;' }, attributes: { style: 'text-align: center;' },
                filterable: {
                    cell: {
                        template: function (container) {
                            container.element.kendoComboBox({
                                dataSource: [{ Text: 'Không chọn', Value: false }, { Text: 'Chọn', Value: true }, { Text: 'Tất cả', Value: '' }],
                                dataTextField: "Text", dataValueField: "Value",
                            });
                        },
                        showOperators: false
                    }
                }, sortable: false
            });
        })
        GridColumn.push({ title: ' ', filterable: false, sortable: false })
        $scope.ORDContainerService_grid.setOptions({
            dataSource: Common.DataSource.Local({
                data: [],
                model: Model,
            }),
            height: '100%', pageable: false, sortable: true, columnMenu: false, resizable: true, reorderable: false, filterable: { mode: 'row' }, editable: false,
            columns: GridColumn
        })

        //tao data source
        var dataCheck = {};
        Common.Data.Each(_ORDInspection_Index.Data.ListContainerService, function (o) {
            if (!Common.HasValue(dataCheck["R" + o.ContainerID + "_L" + o.ServiceID]))
                dataCheck["R" + o.ContainerID + "_L" + o.ServiceID] = o;
        })
        var dataGrid = [];

        Common.Data.Each(_ORDInspection_Index.Data.ListContainer, function (Container) {
            var item = {};
            item["ID"] = Container.ContainerID;
            item["ContainerNo"] = Container.ContainerNo;
            item["OrderCode"] = Container.OrderCode;
            item["PackingCode"] = Container.OrderCode;

            Common.Data.Each(_ORDInspection_Index.Data.ListService, function (Service) {
                var field = "Se" + Service.ServiceID;
                item[field] = false;
                if (Common.HasValue(dataCheck["R" + Container.ContainerID + "_L" + Service.ServiceID])) {
                    item[field] = true;
                }
            });
            dataGrid.push(item);
        })

        $scope.ORDContainerService_grid.dataSource.data(dataGrid);
        $timeout(function () {
            $scope.ORDContainerService_grid.resize;
        }, 1);
        $rootScope.IsLoading = false;
    }

    $scope.IsCompleteAll_Check = function ($event, grid) {
        var fclass = $event.currentTarget.className;
        var field = fclass.split(" ");
        var data = grid.dataSource.data();
        var classchild = 'input.chkSelect.' + field[1];
        if ($event.target.checked == false) {
            $(".cus-item").find(classchild).each(function () {
                $(this).prop("checked", false);
            });
            for (var i = 0; i < data.length ; i++) {
                data[i][field[1]] = false;
            }
        } else {
            $(".cus-item").find(classchild).each(function () {
                $(this).prop("checked", true);
            });
            for (var i = 0; i < data.length ; i++) {
                data[i][field[1]] = true;
            }
        }
        grid.dataSource.data(data);
    }

    $scope.ContainerService_Save_Click = function ($event, grid) {
        $event.preventDefault();

        var data = grid.dataSource.data();
        var dataSave = [];
        Common.Data.Each(data, function (row) {
            Common.Data.Each(_ORDInspection_Index.Data.ListService, function (ListService) {

                if (row["Se" + ListService.ServiceID] == true) {
                    dataSave.push({
                        ServiceID: ListService.ServiceID,
                        ContainerID: row.ID,
                    })
                }
            })
        })
        $rootScope.IsLoading = true;
        Common.Services.Call($http, {
            url: Common.Services.url.ORD,
            method: _ORDInspection_Index.URL.ContainerInService_Save,
            data: { documentID: $scope.documentID, lstContainerService: dataSave },
            success: function (res) {
                $scope.load_ContainerService();
                $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                $rootScope.IsLoading = false;
            },
            error: function (res) {
                $rootScope.IsLoading = false;
            }
        });
    }
    //#endregion

    $scope.Inspection_win_tabOptions = {
        animation: {
            open: { effects: "fadeIn" }
        },
        select: function (e) {
            $timeout(function () {
                $scope.TabIndex = angular.element(e.item).data('tabindex'); //or
            }, 10);
        }
    }

    $scope.mltCustomerOptions = {
        autoBind: true,
        valuePrimitive: true, placeholder: 'Chọn khách hàng',
        dataTextField: 'CustomerName',
        dataValueField: 'ID',
        filter: 'contains',
        suggest: true,
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
        change: function (e) {
            $scope.Search.ListCustomerID = this.value();
        }
    };

    $scope.ShowSetting = function ($event, grid) {
        $event.preventDefault();

        $rootScope.ShowSetting({
            ListView: views.ORDOrder_Inspection,
            event: $event,
            grid: grid,
            current: $state.current
        });
    };

    $scope.DocumentOrderList_gridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.ORD,
            method: _ORDInspection_Index.URL.OrderList,
            pageSize: 100,
            readparam: function () { },
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    ETD: { type: 'date' },
                    ETA: { type: 'date' },
                    RequestDate: { type: 'date' },
                    StatusOfOrderName: { type: 'string' },
                    CutOffTime: { type: 'date' },
                    CreatedDate: { type: 'date' },
                    ExternalDate: { type: 'date' },
                    UserDefine1: { type: 'string' },
                    UserDefine2: { type: 'string' },
                    UserDefine3: { type: 'string' },
                    TotalTon: { type: 'number' },
                    TotalCBM: { type: 'number' },
                    Code: { type: 'string' },
                }
            }
        }),
        height: '99%', groupable: false, pageable: true, sortable: true, columnMenu: false, resizable: true,
        selectable: 'row', filterable: { mode: 'row', visible: false }, reorderable: false,
        columns: [
            {
                field: 'Code', width: 100, title: 'Mã ĐH',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'ServiceOfOrderName', width: 100, title: 'Loại dịch vụ',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'TransportModeName', width: 150, title: 'Loại hình v.chuyển',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'ExternalCode', width: 100, title: 'Mã giao dịch',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'ExternalDate', width: 120, title: 'Ngày giao dịch', template: "#=ExternalDate==null?' ':kendo.toString(ExternalDate, '" + Common.Date.Format.DMYHM + "')#",
                filterable: { cell: { template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); }, operator: 'equal', showOperators: false } },
            },
            {
                field: 'ETD', width: 120, title: 'ETD', template: "#=ETD==null?' ':kendo.toString(ETD, '" + Common.Date.Format.DMYHM + "')#",
                filterable: { cell: { template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); }, operator: 'equal', showOperators: false } },
            },
            {
                field: 'ETA', width: 120, title: 'ETA', template: "#=ETA==null?' ':kendo.toString(ETA, '" + Common.Date.Format.DMYHM + "')#",
                filterable: { cell: { template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); }, operator: 'equal', showOperators: false } },
            },
            {
                field: 'TotalTon', width: 100, title: 'Tổng tấn', template: "#=TotalTon > 0 ? TotalTon : 0#",
                filterable: { cell: { operator: 'gte', showOperators: false } }
            },
            {
                field: 'TotalCBM', width: 100, title: 'Tổng khối', template: "#=TotalCBM > 0 ? TotalCBM : 0#",
                filterable: { cell: { operator: 'gte', showOperators: false } }
            },
            {
                field: 'TotalQuantity', width: 100, title: 'Tổng SL', template: "#=TotalQuantity > 0 ? TotalQuantity : 0#",
                filterable: { cell: { operator: 'gte', showOperators: false } }
            },
            {
                field: 'TotalContainer20', width: 100, title: 'SL container 20',
                filterable: { cell: { operator: 'gte', showOperators: false } }
            },
            {
                field: 'TotalContainer40', width: 100, title: 'SL container 40',
                filterable: { cell: { operator: 'gte', showOperators: false } }
            },
            {
                field: 'LocationFrom', width: 100, title: 'Điểm giao',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'LocationTo', width: 100, title: 'Điểm nhận',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'LocationFromAddress', width: 150, title: 'Địa chỉ điểm giao',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'LocationToAddress', width: 150, title: 'Địa chỉ điểm nhận',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'CustomerName', width: 100, title: 'Tên khách hàng',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'RequestDate', width: 120, title: 'Ngày gửi ĐH', template: "#=RequestDate==null?' ':kendo.toString(RequestDate, '" + Common.Date.Format.DMYHM + "')#",
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'TypeOfContractName', width: 120, title: 'Loại hợp đồng',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'TripNo', width: 100, title: 'Số chuyến', template: "#=TripNo==null?' ':TripNo#",
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'VesselName', width: 100, title: 'Tên tàu', template: "#=VesselName==null?' ':VesselName#",
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'CutOffTime', width: 100, title: 'Cut-Off-Time', template: "#=CutOffTime==null?' ':kendo.toString(CutOffTime, '" + Common.Date.Format.DMYHM + "')#",
                filterable: { cell: { template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); }, operator: 'equal', showOperators: false } },
            },
            {
                field: 'CreatedDate', width: 120, title: 'Ngày tạo đơn hàng', template: "#=CreatedDate==null?' ':kendo.toString(CreatedDate, '" + Common.Date.Format.DMYHM + "')#",
                filterable: { cell: { template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); }, operator: 'equal', showOperators: false } },
            },
            {
                field: 'UserDefine1', width: 100, title: 'Ghi chú 1', template: "#=UserDefine1==null?' ':UserDefine1#",
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'UserDefine2', width: 100, title: 'Ghi chú 2', template: "#=UserDefine2==null?' ':UserDefine2#",
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'UserDefine3', width: 100, title: 'Ghi chú 3', template: "#=UserDefine3==null?' ':UserDefine3#",
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            { field: "", title: '', filterable: false, sortable: false }
        ]
    };

    $scope.DocumentOrder_Click = function ($event, win) {
        $event.preventDefault();
        $timeout(function () {
            $scope.DocumentOrderList_grid.resize();
        }, 1);
        win.center().open();
    };

    $scope.DocumentOrder_Add_Click = function ($event, win, grid) {
        $event.preventDefault();
        var itemSelect = grid.dataItem(grid.select());
        $scope.Item.OrderID = itemSelect.ID;
        $scope.Item.OrderCode = itemSelect.Code;
        $scope.Item.CustomerName = itemSelect.CustomerName;
        $scope.Item.CustomerID = itemSelect.CustomerID;
        win.close();
    }

    $scope.HideSetting = function ($event) {
        $event.preventDefault();

        $rootScope.HideSetting();
    }

    $scope.win_Close = function ($event, win) {
        $event.preventDefault();
        //$scope.Inspection_win_tab.select(0);
        win.close();
    };

}]);