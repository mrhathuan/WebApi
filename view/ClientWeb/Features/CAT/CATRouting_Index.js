/// <reference path="~/Scripts/jquery-1.9.1.intellisense.js" />
/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />

var _CATRouting = {
    URL: {
        AllCustomerForNote: 'CATRouting_AllCustomerList',
        Read_Customer: 'CATDistributor_Customer_Read',
        Read: 'CATRouting_RoutingTree_Read',
        Get: 'CATRouting_RoutingTree_Get',
        Save: 'CATRouting_Routing_Save',
        Delete: 'CATRouting_Routing_Delete',
        Read_LocationNotIn: 'CATRouting_RoutingLocationNotIn_List',
        Read_AreaNotIn: 'CATRouting_RoutingAreaNotIn_List',
        Save_Area: 'CATRouting_RoutingArea_Save',
        Get_Area: 'CATRouting_RoutingArea_Get',
        Delete_Area: 'CATRouting_RoutingArea_Delete',
        Read_AreaDetail: 'CATRouting_RoutingAreaDetail_List',
        Get_AreaDetail: 'CATRouting_RoutingAreaDetail_Get',
        Save_AreaDetail: 'CATRouting_RoutingAreaDetail_Save',
        Delete_AreaDetail: 'CATRouting_RoutingAreaDetail_Delete',
        GetData_CboParent: 'CATRouting_RoutingCBB_List',
        Refresh_RoutingArea: 'CATRouting_RoutingArea_Refresh',
        Refresh_Address: 'CATRouting_Routing_RefreshAddress',
        Read_RoutingCost: 'CATRouting_RoutingCost_List',
        Get_RoutingCost: 'CATRouting_RoutingCost_Get',
        Save_RoutingCost: 'CATRouting_RoutingCost_Save',
        Delete_RoutingCost: 'CATRouting_RoutingCost_Delete',
        GetData_Cost: 'CATRouting_Cost_List',
        Update_ToCus: 'CATRouting_Routing_SaveAllCustomer',
        Update_AllRouting: 'CATRouting_Routing_UpdateLocationForAllRouting',
        ExcelExport_Route: 'CATRouting_RouteExcel_Export',
        ExcelExport_RouteArea: 'CATRouting_AreaExcel_Export',
        ExcelCheck_Route: 'CATRouting_RouteExcel_Check',
        ExcelCheck_RouteArea: 'CATRouting_AreaExcel_Check',
        ExcelSave_Route: 'CATRouting_RouteExcel_Save',
        ExcelSave_RouteArea: 'CATRouting_AreaExcel_Save',
        ExcelExport_RouteAreaLocation: 'CATRouting_ExcelRouteAreaLocation_Export',
        ExcelExport_Routing: 'CATRouting_Excel_Export',
    },
    Data: {
        Country: {},
        Province: {},
        District: {},
    }
}

angular.module('myapp').controller('CATRouting_IndexCtrl', ['$rootScope', '$scope', '$http', '$location', '$state', '$timeout', function ($rootScope, $scope, $http, $location, $state, $timeout) {

    if ($rootScope.IsPageComplete != true) return;
    Common.Log('CATRouting_IndexCtrl');
    $rootScope.IsLoading = false;

    $scope.Auth = $rootScope.GetAuth();
    $scope.HasChoose = false;
    $scope.IsShowTab2 = false;

    $scope.IsDisableLocation = false;
    $scope.IsDisableArea = false;

    $scope.isLocationFrom = true;
    $scope.mts_Customer = [];
    $scope.CurrentAreaID = 0;

    $scope.CatRoutingItem = {
        ID: 0,
        IsArea: false
    }
    $scope.PointLocationItem = { LocationFrom: '', LocationTo: '' }
    $scope.AreaLocationItem = { LocationFrom: '', LocationTo: '' }

    //#region Action
    $scope.ShowSetting = function ($event, grid) {
        $event.preventDefault();

        $rootScope.ShowSetting({
            ListView: [],
            event: $event,
            grid: grid,
            current: $state.current
        });
    };

    //#region main

    //$scope.CatRouting_treeOptions = {
    //    dataSource: Common.DataSource.TreeList($http, {
    //        url: Common.Services.url.CAT,
    //        method: _CATRouting.URL.Read,
    //        model: {
    //            id: 'ID',
    //            fields: {
    //                parentId: { from: 'ParentID', type: 'number', editable: false, nullable: true },
    //                ID: { type: 'number', editable: false, nullable: true },
    //                IsUse: { type: 'bool' },
    //                IsAreaLast: { type: 'bool' },
    //                IsChoose: { type: 'boolean', defaultValue: false },
    //            },
    //            expanded: true
    //        }
    //    }),
    //    height: '100%', pageable: true, sortable: true, columnMenu: false, resizable: true, reorderable: true, filterable: false,
    //    toolbar: kendo.template($('#CatRouting_treetoolbar').html()),
    //    columns: [
    //        {
    //            title: ' ', width: '45px',
    //            headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,CatRouting_tree,CatRouting_treeChooseChange)" />',
    //            headerAttributes: { style: 'text-align: center;' },
    //            template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,CatRouting_tree,CatRouting_treeChooseChange)" />',
    //            filterable: false, sortable: false
    //        },
    //        {
    //            field: 'Code', title: '{{RS.CATRouting.Code}}', width: 120,
    //            template: '<a style=\"cursor:pointer\" href=\"\" ng-click="CatRouting_treeEdit_Click($event,CatRoutingEdit_win,CatRouting_tree)" style="text-decoration:underline; color:blue" >#=Code#</a>'
    //        },
    //        {
    //            field: 'RoutingName', title: '{{RS.CATRouting.RoutingName}}', width: 120,
    //        },
    //        {
    //            field: 'EDistance', title: '{{RS.CATRouting.EDistance}}', width: 120,
    //        },
    //        {
    //            field: 'EHours',
    //            title: '{{RS.CATRouting.EHours}}',
    //            width: 120
    //        },
    //        {
    //            field: 'IsAreaLast',
    //            title: '{{RS.CATRouting.IsAreaLast}}',
    //            width: 120,
    //            template: "<input disabled type='checkbox' #= IsAreaLast ? 'checked=checked' : '' # ></input>",
    //            attributes: { style: "text-align: center;" },
    //        },
    //        {
    //            field: 'IsUse',
    //            title: '{{RS.CATRouting.IsUse}}',
    //            width: 120,
    //            template: "<input disabled type='checkbox' #= IsUse ? 'checked=checked' : '' # ></input>",
    //            attributes: { style: "text-align: center;" },
    //        },
    //    ]
    //};

    $scope.CatRouting_gridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.CAT,
            method: _CATRouting.URL.Read,
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number', editable: false, nullable: true },
                    IsUse: { type: 'bool' },
                    IsAreaLast: { type: 'bool' },
                    IsChoose: { type: 'boolean', defaultValue: false },
                },
                expanded: true
            }
        }),
        height: '100%', pageable: true, sortable: true, columnMenu: false, resizable: true, reorderable: true, filterable: { mode: 'row' },
        toolbar: kendo.template($('#CatRouting_treetoolbar').html()),
        columns: [
            {
                title: ' ', width: '45px',
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,CatRouting_grid,CatRouting_treeChooseChange)" />',
                headerAttributes: { style: 'text-align: center;' }, Attributes: { style: 'text-align: center;' },
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,CatRouting_grid,CatRouting_treeChooseChange)" />',
                filterable: false, sortable: false
            },
            {
                field: 'Code', title: '{{RS.CATRouting.Code}}', width: 120,
                filterable: { cell: { operator: 'contains', showOperators: false } },
                template: '<a style=\"cursor:pointer\" href=\"\" ng-click="CatRouting_treeEdit_Click($event,CatRoutingEdit_win,CatRouting_grid)" style="text-decoration:underline; color:blue" >#=Code#</a>'
            },
            {
                field: 'RoutingName', title: '{{RS.CATRouting.RoutingName}}', width: 120,
                filterable: { cell: { operator: 'contains', showOperators: false } },
            },
            {
                field: 'EDistance', title: '{{RS.CATRouting.EDistance}}', width: 120,
            },
            {
                field: 'EHours',
                title: '{{RS.CATRouting.EHours}}',
                width: 120
            },
            {
                field: 'IsAreaLast',  title: '{{RS.CATRouting.IsAreaLast}}',  width: 120,
                template: "<input disabled type='checkbox' #= IsAreaLast ? 'checked=checked' : '' # ></input>",
                filterable: {
                    cell: {
                        template: function (container) {
                            container.element.kendoComboBox({
                                dataSource: [{ Text: 'Không CĐ con', Value: false }, { Text: 'Có CĐ con', Value: true }, { Text: 'Tất cả', Value: '' }],
                                dataTextField: "Text", dataValueField: "Value"
                            });
                        },
                        showOperators: false
                    }
                },
                attributes: { style: "text-align: center;" },
            },
            {
                field: 'IsUse',title: '{{RS.CATRouting.IsUse}}',width: 120,
                template: "<input disabled type='checkbox' #= IsUse ? 'checked=checked' : '' # ></input>",
                filterable: {
                    cell: {
                        template: function (container) {
                            container.element.kendoComboBox({
                                dataSource: [{ Text: 'Không sử dụng', Value: false }, { Text: 'Sử dụng', Value: true }, { Text: 'Tất cả', Value: '' }],
                                dataTextField: "Text", dataValueField: "Value"
                            });
                        },
                        showOperators: false
                    }
                },
                attributes: { style: "text-align: center;" },
            },
            {
                field: 'Note', title: 'Ghi chú', width: 120,
                filterable: { cell: { operator: 'contains', showOperators: false } },
            },
        ]
    };

    $scope.CatRouting_treeChooseChange = function ($event, grid, haschoose) {
        $scope.HasChoose = haschoose;
    };

    $scope.CatRoutingAdd_Click = function ($event, win, vform) {
        $event.preventDefault();
        $scope.PointLocationItem = { LocationFrom: '', LocationTo: '' }
        $scope.AreaLocationItem = { LocationFrom: '', LocationTo: '' }
        $scope.LoadItem(win, 0);
    }

    $scope.ExcelLocationArea_Click = function ($event) {
        $event.preventDefault();
        $rootScope.IsLoading = true;
        Common.Services.Call($http, {
            url: Common.Services.url.CAT,
            method: _CATRouting.URL.ExcelExport_RouteAreaLocation,
            data: {},
            success: function (res) {
                $rootScope.IsLoading = false;
                $rootScope.DownloadFile(res);
            },
            error: function (res) {
                $rootScope.IsLoading = false;
            }
        });
    }

    $scope.ExcelRouteArea_Click = function ($event, win) {
        $event.preventDefault();
        $rootScope.UploadExcel({
            Path: Common.FolderUpload.Import,
            columns: [
                { field: 'Code', width: 100, title: 'Mã', filterable: { cell: { showOperators: false, operator: "contains" } } },
                { field: 'Name', width: 250, title: 'Tên', filterable: { cell: { showOperators: false, operator: "contains" } } }
            ],
            Download: function () {
                $rootScope.IsLoading = true;
                Common.Services.Call($http, {
                    url: Common.Services.url.CAT,
                    method: _CATRouting.URL.ExcelExport_RouteArea,
                    data: {},
                    success: function (res) {
                        $rootScope.IsLoading = false;
                        $rootScope.DownloadFile(res);
                    },
                    error: function (res) {
                        $rootScope.IsLoading = false;
                    }
                })
            },
            Upload: function (e, callback) {
                $rootScope.IsLoading = true;
                Common.Services.Call($http, {
                    url: Common.Services.url.CAT,
                    method: _CATRouting.URL.ExcelCheck_RouteArea,
                    data: { item: e },
                    success: function (data) {
                        $rootScope.IsLoading = false;
                        callback(data);
                    },
                    error: function (res) {
                        $rootScope.IsLoading = false;
                    }
                })
            },
            Window: win,
            Complete: function (e, data) {
                $rootScope.IsLoading = true;
                Common.Services.Call($http, {
                    url: Common.Services.url.CAT,
                    method: _CATRouting.URL.ExcelSave_RouteArea,
                    data: { lst: data },
                    success: function (res) {
                        $rootScope.IsLoading = false;
                        $rootScope.Message({
                            Type: Common.Message.Type.Notify,
                            NotifyType: Common.Message.NotifyType.SUCCESS,
                            Title: 'Thông báo',
                            Msg: 'Import Thành công',
                            Ok: null,
                            close: null,
                        })
                        $scope.CatRouting_gridOptions.dataSource.read();
                    },
                    error: function (res) {
                        $rootScope.IsLoading = false;
                    }
                })
            }
        })
    }

    $scope.CATRouting_Excel_Click = function ($event) {
        $event.preventDefault();
        $rootScope.IsLoading = true;
        Common.Services.Call($http, {
            url: Common.Services.url.CAT,
            method: _CATRouting.URL.ExcelExport_Routing,
            data: {},
            success: function (res) {

                $rootScope.DownloadFile(res);
                $rootScope.IsLoading = false;
            },
            error: function (res) {
                $rootScope.IsLoading = false;
            }
        })
    }
    $scope.ExcelRoute_Click = function ($event, win) {
        $event.preventDefault();
        $rootScope.UploadExcel({
            Path: Common.FolderUpload.Import,
            columns: [
                { field: 'Code', width: 100, title: '{{RS.CATRouting.Code}}', filterable: { cell: { showOperators: false, operator: "contains" } } },
                { field: 'Name', width: 250, title: '{{RS.CATRouting.RoutingName}}', filterable: { cell: { showOperators: false, operator: "contains" } } }
            ],
            Download: function () {
                $rootScope.IsLoading = true;
                Common.Services.Call($http, {
                    url: Common.Services.url.CAT,
                    method: _CATRouting.URL.ExcelExport_Route,
                    data: {},
                    success: function (res) {
                        
                        $rootScope.DownloadFile(res);
                        $rootScope.IsLoading = false;
                    },
                    error: function (res) {
                        $rootScope.IsLoading = false;
                    }
                })
            },
            Upload: function (e, callback) {
                $rootScope.IsLoading = true;
                Common.Services.Call($http, {
                    url: Common.Services.url.CAT,
                    method: _CATRouting.URL.ExcelCheck_Route,
                    data: { item: e },
                    success: function (data) {
                        $rootScope.IsLoading = false;
                        callback(data);
                    },
                    error: function (res) {
                        $rootScope.IsLoading = false;
                    }
                })
            },
            Window: win,
            Complete: function (e, data) {
                $rootScope.IsLoading = true;
                Common.Services.Call($http, {
                    url: Common.Services.url.CAT,
                    method: _CATRouting.URL.ExcelSave_Route,
                    data: { lst: data },
                    success: function (res) {
                        $rootScope.IsLoading = false;
                        $rootScope.Message({
                            Type: Common.Message.Type.Notify,
                            NotifyType: Common.Message.NotifyType.SUCCESS,
                            Title: 'Thông báo',
                            Msg: 'Import Thành công',
                            Ok: null,
                            close: null,
                        })
                        $scope.CatRouting_gridOptions.dataSource.read();
                    },
                    error: function (res) {
                        $rootScope.IsLoading = false;
                    }
                })
            }
        })
    }

    $scope.CatRoutingUpLocationForAll_Click = function ($event) {
        $event.preventDefault();
        $rootScope.IsLoading = true;
        Common.Services.Call($http, {
            url: Common.Services.url.CAT,
            method: _CATRouting.URL.Update_AllRouting,
            data: {},
            success: function (res) {

                $rootScope.IsLoading = false;
                $rootScope.Message({
                    Type: Common.Message.Type.Notify,
                    NotifyType: Common.Message.NotifyType.SUCCESS,
                    Title: 'Thông báo',
                    Msg: 'Thành công',
                    Close: null,
                    Ok: null
                })
                //$scope.AreaNotIn_win_gridOptions.dataSource.read();
            }
        });
    }

    $scope.CatRoutingAddressRefresh_Click = function ($event) {
        $event.preventDefault();
        $rootScope.IsLoading = true;
        Common.Services.Call($http, {
            url: Common.Services.url.CAT,
            method: _CATRouting.URL.Refresh_Address,
            data: {},
            success: function (res) {
                $rootScope.IsLoading = false;
                $rootScope.Message({
                    Type: Common.Message.Type.Notify,
                    NotifyType: Common.Message.NotifyType.SUCCESS,
                    Title: 'Thông báo',
                    Msg: 'Thành công',
                    Close: null,
                    Ok: null
                })
            }
        });
    }

    $scope.CatRoutingUpdateToCus_Click = function ($event) {
        $event.preventDefault();
        var lst = [];
        var data = $scope.CatRouting_gridOptions.dataSource.data();
        Common.Data.Each(data, function (o) {
            if (o.IsChoose) lst.push(o.ID);
        })
        if (lst.length > 0) {
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.CAT,
                method: _CATRouting.URL.Update_ToCus,
                data: { lst: lst },
                success: function (res) {

                    $rootScope.Message({
                        Type: Common.Message.Type.Notify,
                        NotifyType: Common.Message.NotifyType.SUCCESS,
                        Title: 'Thông báo',
                        Msg: 'Thành công',
                        Close: null,
                        Ok: null
                    })
                    $scope.CatRouting_gridOptions.dataSource.read()
                    $rootScope.IsLoading = false;
                    //$scope.AreaNotIn_win_gridOptions.dataSource.read();
                }
            });
        }
        else {
            $rootScope.Message({
                Type: Common.Message.Type.Alert,
                NotifyType: Common.Message.NotifyType.WARNING,
                Title: 'Thông báo',
                Msg: 'Chọn ít nhất một cung đường',
                Close: null,
                Ok: null
            })
        }
    }

    $scope.CatRouting_treeEdit_Click = function ($event, win, tree) {
        $event.preventDefault();
        var tr = $($event.target).closest('tr');
        var id = 0;
        var item = tree.dataItem(tr);
        if (Common.HasValue(item)) id = item.ID;
        $scope.LoadItem(win, id);
    }

    $scope.LoadItem = function (win, id) {
        Common.Services.Call($http, {
            url: Common.Services.url.CAT,
            method: _CATRouting.URL.Get,
            data: { 'ID': id },
            success: function (res) {
                $scope.CatRoutingItem = res;

                $scope.IsShowTab2 = false;
                $scope.IsDisableArea = false;
                $scope.IsDisableLocation = false;

                $scope.CatRoutingEdit_win_tab.select(0)
                if (id > 0) {
                    $scope.CatRoutingEdit_win_gridOptions.dataSource.read();
                    $scope.IsShowTab2 = true;
                    //debugger
                    if (res.IsArea) {
                        $scope.IsDisableArea = false;
                        $scope.IsDisableLocation = true;
                        $scope.AreaLocationItem.LocationTo = res.AreaToName
                        $scope.AreaLocationItem.LocationFrom = res.AreaFromName
                    }

                    if (res.IsLocation) {
                        $scope.IsDisableArea = true;
                        $scope.IsDisableLocation = false;
                        $scope.PointLocationItem.LocationTo = res.LocationToName
                        $scope.PointLocationItem.LocationFrom = res.LocationFromName
                    }
                }

                $scope.AreaNotIn_win_gridOptions.dataSource.read();
                $scope.LocationNotIn_win_gridOptions.dataSource.read();

                $scope.mts_Customer = [];
                if (Common.HasValue(res.Note)&&res.Note!="") {
                    $scope.mts_Customer = res.Note.split(";");
                }

                win.center();
                win.open();
            }
        });
    }

    //#endregion

    //#region popup edit routing

    $scope.CatRoutingEdit_win_CloseClick = function ($event, win) {
        $event.preventDefault();
        win.close()
    }

    //#region tab 1

    $scope.CatRoutingEdit_win_tabIndex = 1;

    $scope.CatRoutingEdit_win_tabOptions = {
        animation: {
            open: { effects: "fadeIn" }
        },
        select: function (e) {
            $scope.CatRoutingEdit_win_tabIndex = 1;
            $scope.CatRoutingEdit_win_tabIndex = $(e.item).data('tabindex');
        }
    }
    $scope.CATLocationEdit_win_cboParentIDOptions = {
        autoBind: true,
        valuePrimitive: true,
        ignoreCase: true,
        filter: 'contains',
        suggest: true,
        dataTextField: 'RoutingName',
        dataValueField: 'ID',
        minLength: 3,
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    RoutingName: { type: 'string' },
                }
            }
        })
    }
    Common.Services.Call($http, {
        url: Common.Services.url.CAT,
        method: _CATRouting.URL.GetData_CboParent,
        data: {},
        success: function (res) {
            $scope.CATLocationEdit_win_cboParentIDOptions.dataSource.data(res.Data);
        }
    });

    $scope.CatRoutingEditSave_Click = function ($event, win, vform) {
        $event.preventDefault();
        if (vform()) {
            $rootScope.IsLoading = true;
            var note = "";
            if ($scope.mts_Customer != null && $scope.mts_Customer.length > 0) {
                note = $scope.mts_Customer.join(';');
            }
            $scope.CatRoutingItem.Note = note;

            Common.Services.Call($http, {
                url: Common.Services.url.CAT,
                method: _CATRouting.URL.Save,
                data: { item: $scope.CatRoutingItem },
                success: function (res) {
                    $rootScope.IsLoading = false;
                    $scope.CatRouting_gridOptions.dataSource.read();
                    win.close()
                    $rootScope.Message({
                        Type: Common.Message.Type.Notify,
                        NotifyType: Common.Message.NotifyType.SUCCESS,
                        Title: 'Thông báo',
                        Msg: 'Thành công',
                        Close: null,
                        Ok: null
                    })
                }
            });
        }
    }

    $scope.CatRoutingEditDelete_Click = function ($event, win) {
        $event.preventDefault();
        $rootScope.IsLoading = true;
        Common.Services.Call($http, {
            url: Common.Services.url.CAT,
            method: _CATRouting.URL.Delete,
            data: { item: $scope.CatRoutingItem },
            success: function (res) {
                $rootScope.IsLoading = false;
                $scope.CatRouting_gridOptions.dataSource.read();
                win.close()
                $rootScope.Message({
                    Type: Common.Message.Type.Notify,
                    NotifyType: Common.Message.NotifyType.SUCCESS,
                    Title: 'Thông báo',
                    Msg: 'Thành công',
                    Close: null,
                    Ok: null
                })
            }
        });
    }

    $scope.CATLocationEdit_win_numEDistanceOptions = { format: '#,##0.00', spinners: false, culture: 'en-US', min: 0, step: 1, decimals: 2, }
    $scope.CATLocationEdit_win_numEHoursOptions = { format: '#,##0.00', spinners: false, culture: 'en-US', min: 0, step: 1, decimals: 2, }

    $scope.mts_CustomerOption = {
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
        valuePrimitive: true, dataTextField: "Code", dataValueField: "Code", placeholder: "Chọn khách hàng...", filter: "contains", ignoreCase: true,
        highlightFirst: true, autoClose: false,
        itemTemplate: '<span>#= Code #</span><span style="float:right;">#= CustomerName #</span>',
        headerTemplate: '<strong><span> Mã khách hàng </span><span style="float:right;"> Tên khách hàng </span></strong>',
        change: function (e) {
            //$scope.CATDistributor_ListCustomer = this.value();
            // if ($scope.CATDistributor_ListCustomer.length>0) 
            //$scope.CreateGrid('cus')
            // else $scope.CreateGrid('new')
        },
        dataBound: function (e) {
            
        }
    }


    Common.Services.Call($http, {
        url: Common.Services.url.CAT,
        method: _CATRouting.URL.AllCustomerForNote,
        data: {},
        success: function (res) {
            $scope.mts_CustomerOption.dataSource.data(res.Data);
        }
    });


    //Common.Services.Call($http, {
    //    url: Common.Services.url.CAT,
    //    method: _CATDistributor.URL.Read_Customer,
    //    data: {},
    //    success: function (res) {
    //        if (Common.HasValue(res)) {

    //            _CATDistributor.Data.DataCustomer = res.Data;
    //            $scope.mts_CustomerOption.dataSource.data(res.Data);
    //        }
    //    }
    //});

    $scope.ChooseLocation_Click = function ($event, win, choice) {
        $event.preventDefault();
        $scope.isLocationFrom = choice;
        win.center();
        win.open();
        $scope.LocationNotIn_win_grid.refresh()
    }
    $scope.ChooseArea_Click = function ($event, win, choice) {
        $event.preventDefault();
        $scope.isLocationFrom = choice;
        win.center();
        win.open();
        $scope.AreaNotIn_win_grid.refresh();
    }
    //#endregion

    //#region tab 2
    $scope.CatRoutingEdit_win_gridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.CAT,
            method: _CATRouting.URL.Read_RoutingCost,
            readparam: function () { return { routingID: $scope.CatRoutingItem.ID } },
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number', editable: false },
                    Cost: { type: 'number' },
                    CostName: { type: 'string' }
                }
            }
        }),
        height: '100%', pageable: true, sortable: false, columnMenu: false, resizable: true, reorderable: true, filterable: { mode: 'row' },
        toolbar: kendo.template($('#CatRoutingEdit_win_gridtoolbar').html()),
        columns: [
            {
                title: ' ', width: '85px',
                template: '<a href="/" ng-click="GridCost_Edit_Click($event,GridCost_winPopup,CatRoutingEdit_win_grid)" class="k-button"><i class="fa fa-pencil"></i></a><a href="/" ng-click="GridCost_Destroy_Click($event,CatRoutingEdit_win_grid)" class="k-button"><i class="fa fa-times"></i></a>',
                filterable: false, sortable: false
            },
            {
                field: 'CostName', title: '{{RS.CATCost.CostName}}', width: 200,
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'Cost', title: '{{RS.CATRoutingCost.Cost}}', width: '160px',
                template: "#=Cost != null ? Common.Number.ToMoney(Cost) : \"0\"#",
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'Note', title: '{{RS.CATRoutingCost.Note}}', width: 200,
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
        ]
    }
    $scope.GridCost_Add_Click = function ($event, win) {
        $event.preventDefault();

        $scope.CostLoadItem(win, 0)
    }
    $scope.GridCost_Edit_Click = function ($event, win, grid) {
        $event.preventDefault();
        var tr = $($event.target).closest('tr');
        var id = 0;
        var item = grid.dataItem(tr);
        if (Common.HasValue(item)) id = item.ID;
        $scope.CostLoadItem(win, id);
    }
    $scope.GridCost_Destroy_Click = function ($event, grid) {
        $event.preventDefault();
        var tr = $($event.target).closest('tr');
        var item = grid.dataItem(tr);
        if (Common.HasValue(item)) {
            if (confirm('Bạn muốn xóa các dữ liệu đã chọn ?')) {
                Common.Services.Call($http, {
                    url: Common.Services.url.CAT,
                    method: _CATRouting.URL.Delete_RoutingCost,
                    data: { 'item': item },
                    success: function (res) {
                        $scope.CatRoutingEdit_win_gridOptions.dataSource.read();
                    }
                });
            }
        }
    }
    $scope.CostLoadItem = function (win, id) {
        Common.Services.Call($http, {
            url: Common.Services.url.CAT,
            method: _CATRouting.URL.Get_RoutingCost,
            data: { 'ID': id },
            success: function (res) {
                $scope.CostItem = res;
                win.center();
                win.open();
            }
        });
    }
    //#endregion

    //#endregion

    //#region popup choose location
    $scope.LocationNotIn_win_gridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.CAT,
            method: _CATRouting.URL.Read_LocationNotIn,
            readparam: function () { return { fromID: $scope.CatRoutingItem.LocationFromID, toID: $scope.CatRoutingItem.LocationToID } },
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number', editable: false },
                }
            }
        }),
        height: '100%', pageable: true, sortable: true, columnMenu: false, resizable: true, reorderable: true, filterable: { mode: 'row' },
        editable: false, selectable: true,
        columns: [
            { field: 'Code', title: '{{RS.CATRouting.Code}}', width: '115px', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'Location', title: '{{RS.CATRouting.Location}}', width: '235px', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'Address', title: '{{RS.CATRouting.Address}}', width: '500px', filterable: { cell: { operator: 'contains', showOperators: false } } }
        ]
    }

    $scope.LocationNotIn_RefreshClick = function ($event, win, grid) {
        $event.preventDefault()
    }

    $scope.LocationNotIn_AddChooseClick = function ($event, win, grid) {
        $event.preventDefault();
        var item = grid.dataItem(grid.select());
        if (Common.HasValue(item)) {
            if ($scope.isLocationFrom) {
                $scope.CatRoutingItem.LocationFromID = item.ID;
                $scope.PointLocationItem.LocationFrom = item.Code + " - " + item.Location;
            }
            else {
                $scope.CatRoutingItem.LocationToID = item.ID;
                $scope.PointLocationItem.LocationTo = item.Code + " - " + item.Location;
            }
            win.close();
        }
    }

    $scope.LocationNotIn_win_CloseClick = function ($event, win) {
        $event.preventDefault();
        win.close()
    }
    //#endregion

    //#region popup choose area
    $scope.AreaNotIn_win_gridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.CAT,
            method: _CATRouting.URL.Read_AreaNotIn,
            readparam: function () { return { fromID: $scope.CatRoutingItem.RoutingAreaFromID, toID: $scope.CatRoutingItem.RoutingAreaToID } },
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number', editable: false },
                }
            }
        }),
        height: '100%', pageable: true, sortable: true, columnMenu: false, resizable: true, reorderable: true, filterable: { mode: 'row' },
        selectable: 'row',
        //toolbar: kendo.template($('#AreaNotIn_win_gridtoolbar').html()),
        columns: [
            {
                title: ' ', width: '150px',
                template: '<a href="/" ng-click="AreaNotInEdit_Click($event,AreaEdit_winPopup,AreaNotIn_win_grid)" class="k-button"><i class="fa fa-pencil"></i></a>' +
                    '<a href="/" ng-click="AreaNotInDestroy_Click($event,AreaNotIn_win_grid)" class="k-button"><i class="fa fa-times"></i></a>' +
                    '<a href="/" ng-click="AreaNotInRefresh_Click($event,AreaNotIn_win_grid)" class="k-button"><i class="fa fa-refresh"></i></a>' +
                    '<a href="/" ng-click="AreaNotInShowDetail_Click($event,AreaNotInDetail_win,AreaNotIn_win_grid)" class="k-button"><i class="fa fa-info-circle"></i></a>',
                filterable: false, sortable: false
            },
            { field: 'Code', title: '{{RS.CATRouting.Code}}', width: '160px', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'AreaName', title: '{{RS.CATRouting.AreaName}}', width: '450px', filterable: { cell: { operator: 'contains', showOperators: false } } }
        ]
    }

    $scope.AreaNotInEdit_Click = function ($event, win, grid) {
        $event.preventDefault();
        var tr = $($event.target).closest('tr');
        var id = 0;
        var item = grid.dataItem(tr);
        if (Common.HasValue(item)) id = item.ID;
        $scope.AreaLoadItem(win, id);
    }

    $scope.AreaNotIn_AddNewClick = function ($event, win) {
        $event.preventDefault();

        $scope.AreaLoadItem(win, 0)
    }

    $scope.AreaNotIn_AddChooseClick = function ($event, win, grid) {
        $event.preventDefault();

        var item = grid.dataItem(grid.select());
        if (Common.HasValue(item)) {
            if ($scope.isLocationFrom) {
                $scope.CatRoutingItem.RoutingAreaFromID = item.ID;
                $scope.AreaLocationItem.LocationFrom = item.Code + " - " + item.AreaName;
            }
            else {
                $scope.CatRoutingItem.RoutingAreaToID = item.ID;
                $scope.AreaLocationItem.LocationTo = item.Code + " - " + item.AreaName;
            }
            win.close();
        }
    }

    $scope.AreaNotIn_win_CloseClick = function ($event, win) {
        $event.preventDefault()
        win.close()
    }

    $scope.AreaNotInShowDetail_Click = function ($event, win, grid) {
        $event.preventDefault();
        var tr = $($event.target).closest('tr');
        var item = grid.dataItem(tr);
        if (Common.HasValue(item)) {

            $scope.CurrentAreaID = item.ID;
            win.center();
            win.open();
            $scope.AreaNotInDetail_gridOptions.dataSource.read()
            $scope.AreaNotInDetail_grid.refresh();
        }
    }

    $scope.AreaNotInRefresh_Click = function ($event, grid) {
        $event.preventDefault();

        var tr = $($event.target).closest('tr');
        var item = grid.dataItem(tr);

        if (Common.HasValue(item)) {

            $rootScope.Message({
                Type: Common.Message.Type.Confirm,
                NotifyType: Common.Message.NotifyType.SUCCESS,
                Title: 'Thông báo',
                Msg: 'Bạn muốn làm mới vị trí',
                Close: null,
                Ok: function () {
                    $rootScope.IsLoading = true;
                    Common.Services.Call($http, {
                        url: Common.Services.url.CAT,
                        method: _CATRouting.URL.Refresh_RoutingArea,
                        data: { 'item': item },
                        success: function (res) {
                            $rootScope.IsLoading = false;
                            $rootScope.Message({
                                Type: Common.Message.Type.Notify,
                                NotifyType: Common.Message.NotifyType.SUCCESS,
                                Title: 'Thông báo',
                                Msg: 'Thành công',
                                Close: null,
                                Ok: null
                            })
                        }
                    });
                }
            })
        }
    }

    $scope.AreaNotInDestroy_Click = function ($event, grid) {
        $event.preventDefault();
        var tr = $($event.target).closest('tr');
        var item = grid.dataItem(tr);
        if (Common.HasValue(item)) {
            if (confirm('Bạn muốn xóa các dữ liệu đã chọn ?')) {
                Common.Services.Call($http, {
                    url: Common.Services.url.CAT,
                    method: _CATRouting.URL.Delete_Area,
                    data: { 'item': item },
                    success: function (res) {
                        $scope.AreaNotIn_win_gridOptions.dataSource.read();
                    }
                });
            }
        }
    }

    $scope.AreaLoadItem = function (win, id) {
        Common.Services.Call($http, {
            url: Common.Services.url.CAT,
            method: _CATRouting.URL.Get_Area,
            data: { 'ID': id },
            success: function (res) {
                $scope.AreaEditItem = res;
                win.center();
                win.open();
            }
        });
    }
    //#endregion 

    //#region popup detail area
    $scope.AreaNotInDetail_gridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.CAT,
            method: _CATRouting.URL.Read_AreaDetail,
            readparam: function () { return { areaID: $scope.CurrentAreaID } },
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number', editable: false },
                }
            }
        }),
        height: '100%', pageable: true, sortable: true, columnMenu: false, resizable: true, reorderable: true, filterable: false,
        columns: [
            {
                title: ' ', width: '90px',
                template: '<a href="/" ng-click="AreaNotInDetail_EditClick($event,AreaDetailEdit_winPopup,AreaNotInDetail_grid)" class="k-button"><i class="fa fa-pencil"></i></a>' +
                    '<a href="/" ng-click="AreaNotInDetail_DestroyClick($event,AreaNotInDetail_grid)" class="k-button"><i class="fa fa-times"></i></a>',
                filterable: false, sortable: false
            },
            { field: 'CountryName', title: '{{RS.CATRouting.CountryName}}', width: '160px' },
            { field: 'ProvinceName', title: '{{RS.CATRouting.ProvinceName}}', width: '160px' },
            { field: 'DistrictName', title: '{{RS.CATRouting.DistrictName}}', width: '150px' }
        ]

    }

    $scope.AreaNotInDetail_EditClick = function ($event, win, grid) {
        $event.preventDefault();
        var tr = $($event.target).closest('tr');
        var id = 0;
        var item = grid.dataItem(tr);
        if (Common.HasValue(item)) id = item.ID;
        $scope.AreaDetailLoadItem(win, id);
    }

    $scope.AreaNotInDetail_DestroyClick = function ($event, grid) {
        $event.preventDefault();
        var tr = $($event.target).closest('tr');
        var item = grid.dataItem(tr);

        if (Common.HasValue(item)) {

            $rootScope.Message({
                Type: Common.Message.Type.Confirm,
                NotifyType: Common.Message.NotifyType.SUCCESS,
                Title: 'Thông báo',
                Msg: 'Bạn muốn xóa dữ liệu đã chọn',
                Close: null,
                Ok: function () {
                    $rootScope.IsLoading = true;
                    Common.Services.Call($http, {
                        url: Common.Services.url.CAT,
                        method: _CATRouting.URL.Delete_AreaDetail,
                        data: { 'item': item },
                        success: function (res) {
                            $scope.AreaNotInDetail_gridOptions.dataSource.read()
                            $rootScope.IsLoading = false;
                            $rootScope.Message({
                                Type: Common.Message.Type.Notify,
                                NotifyType: Common.Message.NotifyType.SUCCESS,
                                Title: 'Thông báo',
                                Msg: 'Thành công',
                                Close: null,
                                Ok: null
                            })
                        },
                        error: function (e) {
                            $rootScope.IsLoading = false;
                        }
                    });
                }
            })
        }
    }

    $scope.AreaNotInDetail_AddNewClick = function ($event, win) {
        $event.preventDefault();
        $scope.AreaDetailLoadItem(win, 0)
    }

    $scope.AreaNotInDetail_CloseClick = function ($event, win) {
        $event.preventDefault();
        win.close();
    }

    $scope.AreaEdit_winPopupSave_Click = function ($event, win, vform) {
        $event.preventDefault();
        $rootScope.Message({
            Type: Common.Message.Type.Confirm,
            NotifyType: Common.Message.NotifyType.SUCCESS,
            Title: 'Thông báo',
            Msg: 'Bạn muốn lưu dữ liệu đã chọn ?',
            Close: null,
            Ok: function () {
                if (vform()) {
                    $rootScope.IsLoading = true;
                    Common.Services.Call($http, {
                        url: Common.Services.url.CAT,
                        method: _CATRouting.URL.Save_Area,
                        data: { item: $scope.AreaEditItem },
                        success: function (res) {
                            $scope.AreaNotIn_win_gridOptions.dataSource.read();
                            win.close()
                            $rootScope.IsLoading = false;
                            $rootScope.Message({
                                Type: Common.Message.Type.Notify,
                                NotifyType: Common.Message.NotifyType.SUCCESS,
                                Title: 'Thông báo',
                                Msg: 'Thành công',
                                Close: null,
                                Ok: null
                            })
                        }
                    });
                }
            }
        })
    }

    $scope.AreaEdit_winPopupClose_Click = function ($event, win) {
        $event.preventDefault();
        win.close();
    }

    $scope.AreaDetailLoadItem = function (win, id) {
        Common.Services.Call($http, {
            url: Common.Services.url.CAT,
            method: _CATRouting.URL.Get_AreaDetail,
            data: { 'ID': id },
            success: function (res) {
                $scope.AreaDetailItem = res;
                $scope.LoadRegionData($scope.AreaDetailItem);
                win.center();
                win.open();
            }
        });
    }
    //#endregion 

    //#region popup edit detai area
    $scope.AreaDetailEdit_winPopupSave_Click = function ($event, win, vform) {
        $event.preventDefault();
        $rootScope.IsLoading = true;
        Common.Services.Call($http, {
            url: Common.Services.url.CAT,
            method: _CATRouting.URL.Save_AreaDetail,
            data: { item: $scope.AreaDetailItem, areaID: $scope.CurrentAreaID },
            success: function (res) {
                Common.Services.Error(res, function (res) {
                    $scope.AreaNotInDetail_gridOptions.dataSource.read();
                    win.close()
                    $rootScope.IsLoading = false;
                    $rootScope.Message({
                        Type: Common.Message.Type.Notify,
                        NotifyType: Common.Message.NotifyType.SUCCESS,
                        Title: 'Thông báo',
                        Msg: 'Thành công',
                        Close: null,
                        Ok: null
                    })
                })
            }
        });
    }

    $scope.AreaDetailEdit_win_cboCountryOptions = {
        autoBind: true,
        valuePrimitive: true,
        ignoreCase: true,
        filter: 'contains',
        suggest: true,
        dataTextField: 'CountryName',
        dataValueField: 'ID',
        minLength: 3,
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    CountryName: { type: 'string' },
                }
            }
        }),
        change: function (e) {
            var cbo = this;
            if (e.sender.selectedIndex >= 0) {
                $scope.AreaDetailItem.ProvinceID = -1;
                $scope.AreaDetailItem.DistrictID = -1;
                $scope.AreaDetailItem.WardID = -1;
                $scope.LoadRegionData($scope.AreaDetailItem);
            }
            else {
                $scope.AreaDetailItem.CountryID = "";
                $scope.AreaDetailItem.ProvinceID = "";
                $scope.AreaDetailItem.DistrictID = "";
                $scope.AreaDetailItem.WardID = "";
                $scope.LoadRegionData($scope.AreaDetailItem);
            }
        }
    }


    Common.ALL.Get($http, {
        url: Common.ALL.URL.Country,
        success: function (data) {
            _CATRouting.Data.Country = data;
            $scope.AreaDetailEdit_win_cboCountryOptions.dataSource.data(data);
        }
    })

    $scope.AreaDetailEdit_win_cboProvinceOptions = {
        autoBind: true,
        valuePrimitive: true,
        ignoreCase: true,
        filter: 'contains',
        suggest: true,
        dataTextField: 'ProvinceName',
        dataValueField: 'ID',
        minLength: 3,
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    ProvinceName: { type: 'string' },
                }
            }
        }),
        change: function (e) {
            var cbo = this;
            if (e.sender.selectedIndex >= 0) {
                $scope.AreaDetailItem.DistrictID = -1;
                $scope.AreaDetailItem.WardID = -1;
                $scope.LoadRegionData($scope.AreaDetailItem);
            }
            else {
                $scope.AreaDetailItem.ProvinceID = "";
                $scope.AreaDetailItem.DistrictID = "";
                $scope.AreaDetailItem.WardID = "";
                $scope.LoadRegionData($scope.AreaDetailItem);
            }
        }
    }

    Common.ALL.Get($http, {
        url: Common.ALL.URL.Province,
        success: function (data) {
            _CATRouting.Data.Province = {};
            angular.forEach(data, function (obj, indx) {
                if (Common.HasValue(_CATRouting.Data.Province[obj.CountryID]))
                    _CATRouting.Data.Province[obj.CountryID].push(obj);
                else _CATRouting.Data.Province[obj.CountryID] = [obj];

            })
        }
    })

    $scope.AreaDetailEdit_win_cboDistrictOptions = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true, dataTextField: 'DistrictName', dataValueField: 'ID',
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    DistrictName: { type: 'string' },
                }
            }
        }),
        change: function (e) {
            var cbo = this;
            if (e.sender.selectedIndex >= 0) {
                $scope.AreaDetailItem.WardID = "";
                // $scope.LoadRegionData($scope.AreaDetailItem);
            }
            else {
                $scope.AreaDetailItem.DistrictID = "";
                $scope.AreaDetailItem.WardID = "";
                $scope.LoadRegionData($scope.AreaDetailItem);
            }
        }
    }

    Common.ALL.Get($http, {
        url: Common.ALL.URL.District,
        success: function (data) {
            _CATRouting.Data.District = {};
            angular.forEach(data, function (obj, indx) {
                if (Common.HasValue(_CATRouting.Data.District[obj.ProvinceID])) {
                    _CATRouting.Data.District[obj.ProvinceID].push(obj);
                }
                else {
                    _CATRouting.Data.District[obj.ProvinceID] = [{ 'ID': -1, 'DistrictName': ' ' }, obj]
                    
                }
            })
        }
    })
    $scope.AreaDetailEdit_winPopupClose_Click = function ($event, win) {
        $event.preventDefault()
        win.close()
    }

    $scope.LoadRegionData = function (item) {
        Common.Log("LoadRegionData");
        try {
            var countryID = item.CountryID;
            var provinceID = item.ProvinceID;
            var districtID = item.DistrictID;
            var wardID = item.WardID;

            var data = _CATRouting.Data.Province[countryID];
            $scope.AreaDetailEdit_win_cboProvinceOptions.dataSource.data(data);
            if (Common.HasValue(provinceID)) {
                if (provinceID < 0) {
                    provinceID = data[0].ID;
                }
            }
            $timeout(function () {
                item.ProvinceID = provinceID;
            }, 1)
            
            data = _CATRouting.Data.District[provinceID];
            
            $scope.AreaDetailEdit_win_cboDistrictOptions.dataSource.data(data);
            if (Common.HasValue(districtID)) {
                if (districtID < 0) {
                    districtID = data[0].ID;
                }
            }
            $timeout(function () {
                item.DistrictID = districtID;
            }, 1)

            //data = _SYSCustomer_Index.Data.Ward[districtID];
            //$scope.cboWardOptions.dataSource.data(data);
            //if (wardID < 1 && data.length > 0)
            //    wardID = data[0].ID;
            //$timeout(function () {
            //    item.WardID = wardID;
            //}, 1)
        }
        catch (e) { }
    }
    //#endregion

    //#region popup edit cost
    $scope.GridCost_winPopup_numCostOptions = {
        format: '#,##0', spinners: false, culture: 'en-US', min: 0, step: 1, decimals: 0,
    }
    $scope.GridCost_winPopup_cboCostOptions = {
        autoBind: true,
        valuePrimitive: true,
        ignoreCase: true,
        filter: 'contains',
        suggest: true,
        dataTextField: 'CostName',
        dataValueField: 'ID',
        minLength: 3,
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    CostName: { type: 'string' },
                }
            }
        })
    }

    Common.Services.Call($http, {
        url: Common.Services.url.CAT,
        method: _CATRouting.URL.GetData_Cost,
        data: {},
        success: function (res) {
            $scope.GridCost_winPopup_cboCostOptions.dataSource.data(res.Data);
        }
    });

    $scope.GridCost_winPopupSave_Click = function ($event, win) {
        $event.preventDefault();
        if (confirm('Bạn muốn lưu dữ liệu đã chọn ?')) {
            Common.Services.Call($http, {
                url: Common.Services.url.CAT,
                method: _CATRouting.URL.Save_RoutingCost,
                data: { item: $scope.CostItem, routingID: $scope.CatRoutingItem.ID },
                success: function (res) {
                    $scope.CatRoutingEdit_win_gridOptions.dataSource.read();
                    win.close()
                }
            });
        }
    }

    $scope.GridCost_winPopupClose_Click = function ($event, win) {
        $event.preventDefault();
        win.close();
    }
    //#endregion

    
}]);

