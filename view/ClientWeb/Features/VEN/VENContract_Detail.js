/// <reference path="~/Scripts/jquery-1.9.1.intellisense.js" />
/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />

var _VENContract_Detail = {
    URL: {
        Get: 'VENContract_Get',
        Save: 'VENContract_Save',
        Delete: 'VENContract_Delete',

        Data: 'VENContract_Data',

        Material_List: 'VENContract_Material_List',
        Material_Get: 'VENContract_Material_Get',
        Material_Save: 'VENContract_Material_Save',
        Material_Delete: 'VENContract_Material_Delete',
        Material_RoutingList: 'VENContract_Material_RoutingList',
        Material_RoutingNotList: 'VENContract_Material_RoutingNotInList',
        Material_RoutingNotInSave: 'VENContract_Material_RoutingNotInSaveList',
        Material_RoutingDelete: 'VENContract_Material_RoutingDeleteList',

        Routing_List: 'VENContract_Routing_List',
        Routing_Save: 'VENContract_Routing_Save',
        Routing_Insert: 'VENContract_Routing_Insert',
        Routing_Delete: 'VENContract_Routing_Delete',
        Routing_NotIn_List: 'VENContract_Routing_NotIn_List',

        KPI_Save: 'VENContract_KPI_Save',
        KPI_Check_Expression: 'VENContract_KPI_Check_Expression',
        KPI_Check_KPI: 'VENContract_KPI_Check_Hit',
        KPI_Routing_List: 'VENContract_KPI_Routing_List',
        KPI_Routing_Apply: 'VENContract_KPI_Routing_Apply',

        Routing_Excel_Check: 'VENContract_Routing_Excel_Check',
        Routing_Excel_Export: 'VENContract_Routing_Export',
        Routing_Excel_Import: 'VENContract_Routing_Import',

        Price_List: 'VENContract_Price_List',
        Price_Delete: 'VENContract_Price_Delete',
        Price_Copy: 'VENContract_Price_Copy',
        Price_Get: 'VENContract_Price_Get',
        Price_Save: 'VENContract_Price_Save',

        GroupOfProduct_List: 'VENContract_GroupOfProduct_List',
        GroupOfProduct_Get: 'VENContract_GroupOfProduct_Get',
        GroupOfProduct_Save: 'VENContract_GroupOfProduct_Save',
        GroupOfProduct_Delete: 'VENContract_GroupOfProduct_Delete',
        GroupOfProduct_Check: 'VENContract_GroupOfProduct_Check',
        Routing_search_List: 'VENContract_Routing_CATNotIn_List',
        Routing_search_save: 'VENContract_Routing_CATNotIn_Save'

    },
    Data: {
        ListCustomer: null,
        ListMaterial: null,
        ListKPI: null,
    },
    Item: {

    },
    Const: {
        CustomerID: -1,
        CompanyID: -1,
    }
}

angular.module('myapp').controller('VENContract_DetailCtrl', ['$rootScope', '$scope', '$http', '$location', '$state', '$timeout', '$stateParams', function ($rootScope, $scope, $http, $location, $state, $timeout, $stateParams) {
    Common.Log('VENContract_DetailCtrl');
    $rootScope.IsLoading = false;
    $scope.TabIndex = 0;
    $scope.TabIndexM = 0;
    $scope.ID = $state.params.ID;
    $scope.Item = null;
    $scope.HasPriceChoose = false;
    $scope.GOPItem = null;
    $scope.ItemMaterial = { ID: 0 }

    $scope.gop_cbo_GroupOfProductOptions = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true,
        dataTextField: 'GroupName', dataValueField: 'ID',
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    GroupName: { type: 'string' },
                }
            }
        })
    }

    $rootScope.IsLoading = true;
    Common.Services.Call($http, {
        url: Common.Services.url.VEN,
        method: _VENContract_Detail.URL.Data,
        data: { ID: $scope.ID },
        success: function (res) {
            _VENContract_Detail.Data.ListCustomer = res.ListCustomer;
            _VENContract_Detail.Data.ListCompany = {};
            _VENContract_Detail.Data.ListKPI = res.ListKPI;
            $timeout(function () {
                $scope.cboCustomerOptions.dataSource.data(res.ListCustomer);
            }, 1)
            $scope.gop_cbo_GroupOfProductOptions.dataSource.data(res.ListGroupOfProduct);

            Common.Data.Each(res.ListCustomer, function (o) {
                _VENContract_Detail.Data.ListCompany[o.ID] = [];
                _VENContract_Detail.Data.ListCompany[o.ID].push({ CustomerRelateName: ' ', ID: -1 })
            })
            Common.Data.Each(res.ListCompany, function (o) {
                _VENContract_Detail.Data.ListCompany[o.CustomerOwnID].push(o);
            })
            _VENContract_Detail.Data.ListMaterial = res.ListMaterial;
            $scope.cboMaterial_Options.dataSource.data(_VENContract_Detail.Data.ListMaterial);
            Common.Services.Call($http, {
                url: Common.Services.url.VEN,
                method: _VENContract_Detail.URL.Get,
                data: { ID: $scope.ID },
                success: function (res) {
                    $scope.Item = res;

                    if ($scope.Item.ID < 1) {
                        $scope.Item.CustomerID = _VENContract_Detail.Data.ListCustomer[0].ID;
                    } else {
                        _VENContract_Detail.Const.CustomerID = $scope.Item.CustomerID;
                        _VENContract_Detail.Const.CompanyID = $scope.Item.CompanyID;
                        $scope.material_gridOptions.dataSource.read();
                    }
                    $scope.LoadDataCompany();
                    $timeout(function () {
                        $scope.Item.CompanyID = _VENContract_Detail.Const.CompanyID;
                    }, 1)
                    $rootScope.IsLoading = false;

                    $scope.CreateGridRouting();
                }
            })
        }
    })

    //#region Info
    $scope.cboCustomerOptions = {
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
        }),
        change: function (e) {
            $scope.LoadDataCompany();
        }
    };
    $scope.cboCompanyOptions = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true,
        dataTextField: 'CustomerRelateName', dataValueField: 'ID', minLength: 3,
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    CustomerRelateName: { type: 'string' },
                }
            }
        })
    }

    $scope.LoadDataCompany = function () {
        Common.Log("LoadDataCompany");
        var cusID = $scope.Item.CustomerID;
        var data = _VENContract_Detail.Data.ListCompany[cusID]
        $scope.cboCompanyOptions.dataSource.data(data);
        $scope.Item.CustomerID = cusID;
        $timeout(function () {
            if (Common.HasValue(data) && data.length > 0) {
                $scope.Item.CompanyID = data[0].ID;
            }
        }, 1)
    }
    $scope.cboServiceOptions = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true,
        dataTextField: 'ValueOfVar', dataValueField: 'ID', minLength: 3,
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
        url: Common.ALL.URL.SYSVarServiceOfOrder,
        success: function (res) {
            $timeout(function () {
                $scope.cboServiceOptions.dataSource.data(res);
            }, 1);
        }
    });

    $scope.contract_tabstripOptions = {
        animation: {
            open: { effects: "fadeIn" }
        },
        select: function (e) {
            $timeout(function () {
                $scope.TabIndex = angular.element(e.item).data('tabindex');
            }, 1);
        }
    };

    $scope.cboTransportOptions = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true,
        dataTextField: 'ValueOfVar', dataValueField: 'ID', minLength: 3,
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
    };

    Common.ALL.Get($http, {
        url: Common.ALL.URL.SYSVarTransportMode,
        success: function (res) {
            $timeout(function () {
                $scope.cboTransportOptions.dataSource.data(res);
            }, 1);
        }
    });

    $scope.cboContractTypeOptions = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true,
        dataTextField: 'ValueOfVar', dataValueField: 'ID', minLength: 3,
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
    };

    Common.ALL.Get($http, {
        url: Common.ALL.URL.SYSVarTypeOfContract,
        success: function (res) {
            $timeout(function () {
                $scope.cboContractTypeOptions.dataSource.data(res);
            }, 1);
        }
    });

    $scope.cboContractDateOptions = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true,
        dataTextField: 'ValueOfVar', dataValueField: 'ID', minLength: 3,
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
    };

    Common.ALL.Get($http, {
        url: Common.ALL.URL.SYSVarTypeOfContractDate,
        success: function (res) {
            $timeout(function () {
                $scope.cboContractDateOptions.dataSource.data(res);
            }, 1);
        }
    });

    //#endregion

    //#region Routing

    $scope.CreateGridRouting = function () {

        Common.Services.Call($http, {
            url: Common.Services.url.VEN,
            method: _VENContract_Detail.URL.Routing_List,
            data: { contractID: $scope.ID },
            success: function (res) {

                var columns = [
                    {
                        title: ' ', width: '120px',
                        template: '<a href="/" ng-click="Routing_Edit_Click($event,dataItem,routing_info_win)" class="k-button"><i class="fa fa-pencil"></i></a>' +
                            '<a href="/" ng-click="Routing_KPI_Click($event,dataItem,kpi_info_win)" class="k-button"><i class="fa fa-info"></i></a>' +
                            '<a href="/" ng-click="Routing_Delete_Click($event,dataItem)" class="k-button"><i class="fa fa-trash"></i></a>',
                        filterable: false, sortable: false
                    },
                    {
                        field: 'CATCode', width: 150, title: 'Mã hệ thống', filterable: { cell: { operator: 'contains', showOperators: false } }
                    },
                    {
                        field: 'Code', title: 'Mã cung đường', width: 150, filterable: { cell: { operator: 'contains', showOperators: false } }
                    },
                    {
                        field: 'RoutingName', title: 'Tên cung đường', width: 150, filterable: { cell: { operator: 'contains', showOperators: false } }
                    },
                    {
                        field: 'SortOrder', title: 'STT', template: '#= SortOrder != 0 ? SortOrder : "" #', width: 60, filterable: { cell: { operator: 'equal', showOperators: false } }
                    }
                ];

                var model = {
                    id: 'ID',
                    fields: {
                        ID: { type: 'number' },
                        SortOrder: { type: 'number' },
                    }
                };


                var objCol = {};
                Common.Data.Each(_VENContract_Detail.Data.ListKPI, function (o) {
                    columns.push({
                        field: 'F_' + o.ID + "_" + o.Code, width: 150, title: o.KPIName, filterable: false, sortable: false
                    })
                    objCol[o.ID] = 'F_' + o.ID + "_" + o.Code;
                })
                var dataGrid = [];
                Common.Data.Each(res.Data, function (row) {
                    Common.Data.Each(row.ListKPI, function (kpi) {
                        if (Common.HasValue(objCol[kpi.KPIID]))
                            row[objCol[kpi.KPIID]] = kpi.Expression;
                    })
                    dataGrid.push(row);
                })

                $scope.routing_grid.setOptions({
                    dataSource: Common.DataSource.Local({
                        data: dataGrid,
                        model: model,
                        pageSize: 20,
                    }),
                    height: '99%', groupable: false, pageable: true, sortable: true, columnMenu: false, resizable: true,
                    selectable: true, filterable: { mode: 'row' }, reorderable: true,
                    toolbar: kendo.template($('#routing_grid_toolbar').html()),
                    columns: columns
                })

            },
            error: function (res) {
                $rootScope.IsLoading = false;
            }
        });

    }

    $scope.RoutingItem = null;

   

    $scope.Routing_Edit_Click = function ($event, item, win) {
        $event.preventDefault();
        $scope.RoutingItem = null;
        $scope.RoutingItem = $.extend({}, true, item);

        win.center().open();
    };

    $scope.Routing_Delete_Click = function ($event, item) {
        $event.preventDefault();

        Common.Services.Call($http, {
            url: Common.Services.url.VEN,
            method: _VENContract_Detail.URL.Routing_Delete,
            data: { ID: item.ID },
            success: function (res) {
                Common.Services.Error(res, function (res) {
                    $rootScope.Message({ Msg: "Đã xóa!" });
                    $scope.routing_gridOptions.dataSource.read();
                })
            }
        });
    };

    $scope.Routing_Add_Click = function ($event, grid, win) {
        $event.preventDefault();

        grid.dataSource.read();
        win.center().open();
    };

    $scope.RoutingCus_Add_Click = function ($event, grid, win) {
        $event.preventDefault();

        grid.dataSource.read();
        win.center().open();
    };

    $scope.routing_notin_gridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.VEN,
            method: _VENContract_Detail.URL.Routing_NotIn_List,
            readparam: function () {
                return {
                    contractID: $scope.ID
                }
            },
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    IsChoose: { type: 'boolean' }
                }
            },
            pageSize: 100
        }),
        height: '99%', groupable: false, pageable: true, sortable: true, columnMenu: false, resizable: true,
        selectable: true, filterable: { mode: 'row' }, reorderable: true,
        columns: [
            {
                title: 'Chọn', width: '50px',
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,routing_notin_grid)" />',
                headerAttributes: { style: 'text-align: center;' }, attributes: { style: 'text-align: center;' },
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,routing_notin_grid)" />',
                filterable: false, sortable: false
            },
            {
                field: 'Code', title: 'Mã cung đường', width: 150, filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'RoutingName', title: 'Tên cung đường', filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'EDistance', title: 'Khoảng cách (km)', width: 120, filterable: { cell: { operator: 'gte', showOperators: false } }
            },
            {
                field: 'EHours', title: 'Thời gian (giờ)', width: 120, filterable: { cell: { operator: 'gte', showOperators: false } }
            },
            {
                field: 'Note', title: 'Ghi chú', width: 150, filterable: { cell: { operator: 'contains', showOperators: false } }
            },
        ]
    };

    $scope.Routing_NotIn_Submit_Click = function ($event, grid, win) {
        $event.preventDefault();

        var data = $.grep(grid.dataSource.data(), function (o) { return o.IsChoose == true; });
        if (data.length > 0) {
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.VEN,
                method: _VENContract_Detail.URL.Routing_Insert,
                data: { data: data, contractID: $scope.ID },
                success: function (res) {
                    $rootScope.IsLoading = false;
                        $rootScope.Message({ Msg: "Đã cập nhật." });
                        $scope.CreateGridRouting()
                        win.close();
                },
                error: function (e) {
                    $rootScope.IsLoading = false;
                }
            })
        } else {
            win.close();
        }
    };

    $scope.Routing_Info_Submit_Click = function ($event, grid, win) {
        $event.preventDefault();

        Common.Services.Call($http, {
            url: Common.Services.url.VEN,
            method: _VENContract_Detail.URL.Routing_Save,
            data: { item: $scope.RoutingItem },
            success: function (res) {
                Common.Services.Error(res, function (res) {
                    $rootScope.Message({ Msg: "Đã cập nhật!" });
                    grid.dataSource.read();
                    win.close();
                })
            }
        });
    };

    $scope.Routing_Excel_Click = function ($event, win) {
        $event.preventDefault();

        $rootScope.UploadExcel({
            Path: Common.FolderUpload.Import,
            columns: [
                { field: 'CATCode', width: 150, title: 'Mã hệ thống', filterable: { cell: { showOperators: false, operator: "contains" } } },
                { field: 'Code', width: 150, title: 'Mã cung đường', filterable: { cell: { showOperators: false, operator: "contains" } } },
                { field: 'RoutingName', width: 250, title: 'Tên cung đường', filterable: { cell: { showOperators: false, operator: "contains" } } },
                { field: 'LeadTimeETD', title: 'ETD (giờ)', width: 100, filterable: { cell: { operator: 'gte', showOperators: false } } },
                { field: 'LeadTimeETA', title: 'ETA (giờ)', width: 100, filterable: { cell: { operator: 'gte', showOperators: false } } }
            ],
            Download: function () {
                $rootScope.IsLoading = true;
                Common.Services.Call($http, {
                    url: Common.Services.url.VEN,
                    method: _VENContract_Detail.URL.Routing_Excel_Export,
                    data: { contractID: $scope.ID },
                    success: function (res) {
                        $rootScope.IsLoading = false;
                        $rootScope.DownloadFile(res);
                    }
                })
            },
            Upload: function (e, callback) {
                $rootScope.IsLoading = true;
                Common.Services.Call($http, {
                    url: Common.Services.url.VEN,
                    method: _VENContract_Detail.URL.Routing_Excel_Check,
                    data: { file: e.FilePath, contractID: $scope.ID, customerID: $scope.Item.CustomerID },
                    success: function (data) {
                        $rootScope.IsLoading = false;
                        callback(data);
                    }
                })
            },
            Window: win,
            Complete: function (e, data) {
                $rootScope.IsLoading = true;
                Common.Services.Call($http, {
                    url: Common.Services.url.VEN,
                    method: _VENContract_Detail.URL.Routing_Excel_Import,
                    data: { data: data, contractID: $scope.ID },
                    success: function (res) {
                        $rootScope.IsLoading = false;
                        $rootScope.Message({ Msg: "Đã cập nhật!" });
                        $scope.routing_gridOptions.dataSource.read();
                    }
                })
            }
        })
    };

    //#region kpi
    $scope.KPIItemCheck = {
        ID: -1,
        KPICode: '',
        DateRequest: new Date(),
        DateDN: new Date(),
        DateFromCome: new Date(),
        DateFromLeave: new Date(),
        DateFromLoadStart: new Date(),
        DateFromLoadEnd: new Date(),
        DateToCome: new Date(),
        DateToLeave: new Date(),
        DateToLoadStart: new Date(),
        DateToLoadEnd: new Date(),
        DateInvoice: new Date(),
        ETARequest: new Date(),
        Expression: '',
        Zone: 0,
        LeadTime: 0,
        CompareField: ''
    }

    $scope.kpi_gridOptions = {
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    KPIName: { type: 'string', editable: false }
                }
            },
            pageSize: 0
        }),
        height: '100%', groupable: false, pageable: false, sortable: true, columnMenu: false, resizable: true,
        selectable: false, filterable: false, reorderable: true, editable: 'incell',
        columns: [
            {
                title: ' ', width: '50px', filterable: false, sortable: false,
                template: '<a href="/" title="Kiểm tra" ng-click="KPI_Check_Click($event,dataItem,kpi_check_win)" class="k-button"><i class="fa fa-bolt"></i></a>'
            },
            { field: 'KPICode', title: 'Mã', width: 100 },
            { field: 'KPIName', title: 'Tên KPI', width: 200 },
            { field: 'Expression', title: 'Biểu thức' },
            { field: 'CompareField', title: 'TG so sánh', width: 120 }
        ]
    }
    $scope.Routing_KPI_Click = function ($event, item, win) {
        $event.preventDefault();

        $scope.RoutingItem = $.extend({}, true, item);
        $scope.KPIItemCheck.Zone = item.Zone > 0 ? item.Zone : 0;
        $scope.KPIItemCheck.LeadTime = item.LeadTime > 0 ? item.LeadTime : 0;
        

        var expTemp = {};
        var comTemp = {};
        var data = $scope.kpi_gridOptions.dataSource.data();
        Common.Data.Each(item.ListKPI, function (o) {
            expTemp[o.KPIID] = o.Expression;
            comTemp[o.KPIID] = o.CompareField;
        })
        Common.Data.Each(data, function (o) {
            if (Common.HasValue(expTemp[o.KPIID])) {
                o.Expression = expTemp[o.KPIID];
            }
            else {
                o.Expression = "";
            }
            if (Common.HasValue(comTemp[o.KPIID])) {
                o.CompareField = comTemp[o.KPIID];
            }
            else {
                o.CompareField = "";
            }
        })
        win.center().open();
        $scope.kpi_gridOptions.dataSource.data(data);
    }
    $scope.KPI_SaveChanges_Click = function ($event, grid) {
        $event.preventDefault();

        var data = grid.dataSource.data();
        Common.Services.Call($http, {
            url: Common.Services.url.VEN,
            method: _VENContract_Detail.URL.KPI_Save,
            data: { data: data, routingID: $scope.RoutingItem.ID },
            success: function (res) {
                Common.Services.Error(res, function (res) {
                    $rootScope.Message({ Msg: "Đã cập nhật!" });
                    Common.Data.Each(grid.dataSource.data(), function (o) { o.dirty = false });
                    grid.dataSource.sync();
                    $scope.CreateGridRouting();
                })
            }
        });
    }

    $scope.KPI_Save_Click = function ($event, grid, win) {
        $event.preventDefault();

        var data = grid.dataSource.data();
        Common.Data.Each(data, function (o) {
            if (o.ID == $scope.KPIItemCheck.ID) {
                o.Expression = $scope.KPIItemCheck.Expression;
                o.CompareField = $scope.KPIItemCheck.CompareField;
            }
        })
        grid.dataSource.sync();
        win.close();
    }

    $scope.KPI_Check_Click = function ($event, item, win) {
        $event.preventDefault();

        $scope.KPIItemCheck.ID = item.ID;
        $scope.KPIItemCheck.KPICode = item.KPICode;
        $scope.KPIItemCheck.Expression = item.Expression;
        $scope.KPIItemCheck.CompareField = item.CompareField;
        win.center().open();
    }

    $scope.KPI_Check_Expression_Click = function ($event) {
        $event.preventDefault();

        Common.Services.Call($http, {
            url: Common.Services.url.VEN,
            method: _VENContract_Detail.URL.KPI_Check_Expression,
            data: {
                Expression: $scope.KPIItemCheck.Expression,
                zone: $scope.KPIItemCheck.Zone,
                leadTime: $scope.KPIItemCheck.LeadTime,
                item: $scope.KPIItemCheck,
                lst: $scope.kpi_gridOptions.dataSource.data()
            },
            success: function (res) {
                Common.Services.Error(res, function (res) {
                    $rootScope.Message({
                        Type: Common.Message.Type.Alert, Msg: Common.Date.FromJsonDMYHMS(res)
                    });
                })
            },
            error: function () {
                $rootScope.Message({
                    Type: Common.Message.Type.Alert, Msg: "Sai công thức."
                });
            }
        });
    }

    $scope.KPI_Check_KPI_Click = function ($event) {
        $event.preventDefault();

        Common.Services.Call($http, {
            url: Common.Services.url.VEN,
            method: _VENContract_Detail.URL.KPI_Check_KPI,
            data: {
                Expression: $scope.KPIItemCheck.Expression,
                Field: $scope.KPIItemCheck.CompareField,
                zone: $scope.KPIItemCheck.Zone,
                leadTime: $scope.KPIItemCheck.LeadTime,
                item: $scope.KPIItemCheck,
                lst: $scope.kpi_gridOptions.dataSource.data()
            },
            success: function (res) {
                Common.Services.Error(res, function (res) {
                    $rootScope.Message({
                        Type: Common.Message.Type.Alert, Msg: res
                    });
                })
            },
            error: function () {
                $rootScope.Message({
                    Type: Common.Message.Type.Alert, Msg: "Lỗi!"
                });
            }
        });
    }

    $scope.KPI_Apply_ToAll_Click = function ($event, grid, win) {
        $event.preventDefault();

        var data = grid.dataSource.data();
        var flag = false;
        Common.Data.Each(data, function (o) {
            if (o.dirty)
                flag = true;
        })
        if (flag) {
            $rootScope.Message({
                Type: Common.Message.Type.Alert, Msg: "Lưu thay đổi trước."
            })
        }
        else {
            win.center().open();
            $scope.kpi_routing_gridOptions.dataSource.read();
        }
    }

    $scope.kpi_routing_gridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.VEN,
            method: _VENContract_Detail.URL.KPI_Routing_List,
            readparam: function () {
                return {
                    contractID: $scope.ID, routingID: Common.HasValue($scope.RoutingItem) ? $scope.RoutingItem.ID : -1
                }
            },
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    IsChoose: { type: 'boolean' }
                }
            },
            pageSize: 100
        }),
        height: '99%', groupable: false, pageable: true, sortable: true, columnMenu: false, resizable: true,
        selectable: true, filterable: { mode: 'row' }, reorderable: true,
        columns: [
            {
                title: 'Chọn', width: '50px',
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,kpi_routing_grid)" />',
                headerAttributes: { style: 'text-align: center;' }, attributes: { style: 'text-align: center;' },
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,kpi_routing_grid)" />',
                filterable: false, sortable: false
            },
            {
                field: 'Code', title: 'Mã cung đường', width: 150, filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'RoutingName', title: 'Tên cung đường', filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'Zone', title: 'Zone', filterable: { cell: { operator: 'eq', showOperators: false } }
            },
            {
                field: 'LeadTime', title: 'Thời gian', filterable: { cell: { operator: 'eq', showOperators: false } }
            }
        ]
    }

    $scope.KPI_Routing_Submit_Click = function ($event, grid, win) {
        $event.preventDefault();

        var data = $.grep(grid.dataSource.data(), function (o) { return o.IsChoose == true; });
        if (data.length > 0) {
            Common.Services.Call($http, {
                url: Common.Services.url.VEN,
                method: _VENContract_Detail.URL.KPI_Routing_Apply,
                data: { data: data, routingID: $scope.RoutingItem.ID },
                success: function (res) {
                    Common.Services.Error(res, function (res) {
                        $rootScope.Message({ Msg: "Đã cập nhật." });
                        win.close();
                        $scope.CreateGridRouting();
                    })
                }
            })
        } else {
            $rootScope.Message({ Msg: "Không có dữ liệu được chọn." });
            win.close();
        }
    }
    //#endregion

    //#endregion

    //#region Material
    $scope.material_tabstripOptions = {
        animation: {
            open: { effects: "fadeIn" }
        },
        select: function (e) {
            $timeout(function () {
                $scope.TabIndexM = angular.element(e.item).data('tabindex');
                Common.Log("Select_TabM_" + $scope.TabIndexM);
            }, 1)
        }
    }
    $scope.material_gridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.VEN,
            method: _VENContract_Detail.URL.Material_List,
            readparam: function () {
                return {
                    contractID: $scope.ID
                }
            },
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    IsWarning: { type: 'boolean' }
                }
            },
        }),
        height: '99%', groupable: false, pageable: true, sortable: true, columnMenu: false, resizable: true,
        selectable: false, filterable: { mode: 'row' }, reorderable: false, editable: 'false',
        columns: [
            {
                title: ' ', width: '120px',
                template: '<a href="/" ng-show="!dataItem.IsClosed" ng-click="Material_Edit_Click($event,dataItem,material_win,material_vform)" class="k-button"><i class="fa fa-pencil"></i></a>' +
                    '<a href="/" ng-show="!dataItem.IsClosed" ng-click="Material_Delete_Click($event,dataItem,material_grid)" class="k-button"><i class="fa fa-trash"></i></a>' +
                    '<a href="/" ng-show="dataItem.IsWarning" ng-show="dataItem.IsWarning" ng-click="Material_Detail_Click($event,dataItem)" class="k-button"><i class="fa fa-file"></i></a>',
                filterable: false, sortable: false
            },
            {
                field: 'MaterialCode', width: 300, title: 'Mã Vật tư/nhiên liệu', filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'PriceContract', title: 'Giá hợp đồng', width: 150, filterable: { cell: { operator: 'gte', showOperators: false } }
            },
            {
                field: 'PriceCurrent', title: 'Giá hiện tại', width: 150, filterable: { cell: { operator: 'gte', showOperators: false } }
            },
            {
                field: 'PriceWarning', width: 120, title: 'Giá cảnh báo',
                filterable: { cell: { operator: 'gte', showOperators: false } }
            },
            {
                field: 'IsWarning', title: 'Cảnh báo', width: 150,
                template: '<input type="checkbox" ng-model="dataItem.IsWarning" disabled="disabled" />',
                attributes: { style: "text-align: center; " },
                filterable: {
                    cell: {
                        template: function (container) {
                            container.element.kendoComboBox({
                                dataSource: [{ Text: 'Có thay đổi', Value: true }, { Text: 'Không thay đổi', Value: false }, { Text: 'Tất cả', Value: '' }],
                                dataTextField: "Text", dataValueField: "Value",
                            });
                        }, showOperators: false
                    }
                }
            },
            
            { title: '', filterable: false, sortable: false }
        ]
    }

    $scope.PriceContract_Options = { format: 'n0', spinners: false, culture: 'en-US', min: 0, step: 1000, decimals: 0, }
    $scope.PriceCurrent_Options = { format: 'n0', spinners: false, culture: 'en-US', min: 0, step: 1000, decimals: 0, }

    $scope.cboMaterial_Options = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true,
        dataTextField: 'MaterialName', dataValueField: 'ID', minLength: 3,
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    MaterialName: { type: 'string' },
                }
            }
        }),
        change: function () {

        }
    }

    $scope.Material_Add_Click = function ($event, win, vform) {
        $event.preventDefault();
        $scope.MaterialLoadItem(0, win, vform)
    }
    $scope.Material_Edit_Click = function ($event, data, win, vform) {
        $event.preventDefault();
        $scope.MaterialLoadItem(data.ID, win, vform)
    }
    $scope.Material_Delete_Click = function ($event, item, grid) {
        $event.preventDefault();
        $rootScope.IsLoading = true;
        Common.Services.Call($http, {
            url: Common.Services.url.VEN,
            method: _VENContract_Detail.URL.Material_Delete,
            data: { id: item.ID },
            success: function (res) {
                $rootScope.IsLoading = false;
                $scope.material_gridOptions.dataSource.read();
                $rootScope.Message({ Msg: 'Đã xóa', NotifyType: Common.Message.NotifyType.SUCCESS });
            }
        });
    }


    $scope.MaterialLoadItem = function (id, win, vform) {
        $rootScope.IsLoading = true;
        Common.Services.Call($http, {
            url: Common.Services.url.VEN,
            method: _VENContract_Detail.URL.Material_Get,
            data: { id: id },
            success: function (res) {
                vform
                $rootScope.IsLoading = false;
                $scope.ItemMaterial = res;
                $scope.material_tabstrip.select(0);
                if (res.ID > 0) {
                    $scope.material_routing_GridOptions.dataSource.read();
                }
                win.center();
                win.open();
            },
            error: function (res) {
                $rootScope.IsLoading = false;
            }
        });
    }

    $scope.RouteHasChoose = false;
    $scope.material_routing_GridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.VEN,
            method: _VENContract_Detail.URL.Material_RoutingList,
            readparam: function () { return { contractMaterialID: $scope.ItemMaterial.ID } },
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number', editable: false, nullable: true },
                    IsChoose: { type: 'boolean', },
                }
            }
        }),
        height: '100%', pageable: true, sortable: true, columnMenu: false, filterable: { mode: 'row' }, resizable: true, editable: false,
        columns: [
            {
                title: ' ', width: '45px',
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,material_routing_Grid,material_routing_GridChoose_Change)" />',
                headerAttributes: { style: 'text-align: center;' }, attributes: { style: 'text-align: center;' },
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,material_routing_Grid,material_routing_GridChoose_Change)" />',
                filterable: false, sortable: false
            },
            { field: 'RoutingCode', title: "Mã cung đường", width: 120, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'RoutingName', title: "Tên cung đường", width: 150, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { title: ' ', filterable: false, sortable: false }
        ]
    }
    $scope.material_routing_GridChoose_Change = function ($event, grid, hasChoose) {
        $scope.RouteHasChoose = hasChoose;
    }

    $scope.RouteNotInHasChoose = false;
    $scope.material_routing_notin_GridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.VEN,
            method: _VENContract_Detail.URL.Material_RoutingNotList,
            readparam: function () { return { contractMaterialID: $scope.ItemMaterial.ID } },
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number', editable: false, nullable: true },
                    IsChoose: { type: 'boolean' },
                }
            }
        }),
        height: '100%', pageable: true, sortable: true, columnMenu: false, filterable: { mode: 'row' }, resizable: true, editable: false,
        columns: [
            {
                title: ' ', width: '35px',
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,material_routing_notin_Grid,material_routing_notin_GridChoose_Change)" />',
                headerAttributes: { style: 'text-align: center;' }, attributes: { style: 'text-align: center;' },
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,material_routing_notin_Grid,material_routing_notin_GridChoose_Change)" />',
                filterable: false, sortable: false
            },
            { field: 'Code', title: "Mã cung đường", width: 120, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'RoutingName', title: "Tên cung đường", width: 150, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { title: ' ', filterable: false, sortable: false }
        ]
    }
    $scope.material_routing_notin_GridChoose_Change = function ($event, grid, hasChoose) {
        $scope.RouteNotInHasChoose = hasChoose;
    }

    $scope.material_SaveInfo_Click = function ($event, vform) {
        $event.preventDefault();
        if (vform()) {
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.VEN,
                method: _VENContract_Detail.URL.Material_Save,
                data: { item: $scope.ItemMaterial, contractID: $scope.ID },
                success: function (res) {
                    $scope.material_gridOptions.dataSource.read()
                    Common.Services.Call($http, {
                        url: Common.Services.url.VEN,
                        method: _VENContract_Detail.URL.Material_Get,
                        data: { id: res },
                        success: function (ress) {
                            $rootScope.IsLoading = false;
                            $scope.ItemMaterial = ress;
                            $scope.material_routing_GridOptions.dataSource.read()
                        },
                        error: function (res) {
                            $rootScope.IsLoading = false;
                        }
                    });

                },
                error: function (res) {
                    $rootScope.IsLoading = false;
                }
            });

        }
    }

    $scope.material_routing_AddNew = function ($event, win) {
        $event.preventDefault();
        $scope.material_routing_notin_GridOptions.dataSource.read();
        win.center();
        win.open();
        $scope.material_routing_notin_Grid.resize();
    }

    $scope.material_routing_Delete = function ($event, grid) {
        $event.preventDefault();
        var data = [];
        Common.Data.Each(grid.dataSource.data(), function (o) {
            if (o.IsChoose == true) data.push(o.ID);
        })
        if (data.length > 0) {
            $rootScope.Message({
                Msg: "Xác nhận xóa cung đường?",
                Type: Common.Message.Type.Confirm,
                Ok: function () {
                    $rootScope.IsLoading = true;
                    Common.Services.Call($http, {
                        url: Common.Services.url.VEN,
                        method: _VENContract_Detail.URL.Material_RoutingDelete,
                        data: { lst: data },
                        success: function (res) {
                            $rootScope.IsLoading = false;
                            $scope.material_routing_GridOptions.dataSource.read();
                            $rootScope.Message({
                                Msg: 'Đã xóa.',
                                NotifyType: Common.Message.NotifyType.SUCCESS
                            });
                            $scope.RouteHasChoose = false;
                        },
                        error: function (res) {
                            $rootScope.IsLoading = false;
                        }
                    });
                }
            });
        }
    }

    $scope.material_routing_notin_Submit_Click = function ($event, grid, win) {
        $event.preventDefault();
        var data = [];
        Common.Data.Each(grid.dataSource.data(), function (o) {
            if (o.IsChoose == true) data.push(o.ID);
        })
        if (data.length > 0) {
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.VEN,
                method: _VENContract_Detail.URL.Material_RoutingNotInSave,
                data: { contractMaterialID: $scope.ItemMaterial.ID, lst: data },
                success: function (res) {
                    $rootScope.IsLoading = false;
                    $scope.material_routing_GridOptions.dataSource.read();
                    win.close();
                },
                error: function (res) {
                    $rootScope.IsLoading = false;
                }
            });

        }
    }
    //#endregion

    //#region Price

    $scope.price_gridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.VEN,
            method: _VENContract_Detail.URL.Price_List,
            readparam: function () {
                return {
                    contractID: $scope.ID
                }
            },
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    EffectDate: { type: 'date' },
                    IsChoose: { type: 'boolean' }
                }
            },
            pageSize: 100
        }),
        height: '99%', groupable: false, pageable: true, sortable: true, columnMenu: false, resizable: true,
        selectable: false, filterable: { mode: 'row' }, reorderable: false,
        toolbar: kendo.template($('#price_grid_toolbar').html()),
        dataBound: function () {
            if (Common.HasValue($scope.Item)) {
                $scope.Item.IsDisabled = false;
                if (this.dataSource.data().length > 0) {
                    $scope.Item.IsDisabled = true;
                }
            }
        },
        columns: [
            {
                title: ' ', width: '35px',
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,price_grid,price_gridChoose_Change)" />',
                headerAttributes: { style: 'text-align: center;' }, attributes: { style: 'text-align: center;' },
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,price_grid,price_gridChoose_Change)" />',
                filterable: false, sortable: false
            },
            {
                title: ' ', width: '100px',
                template: '<a href="/" ng-click="Price_Edit_Click($event,dataItem,price_win,price_win_vform)" class="k-button"><i class="fa fa-pencil"></i></a>' +
                    '<a href="/" ng-click="Price_Delete_Click($event,dataItem)" class="k-button"><i class="fa fa-trash"></i></a>' +
                    '<a href="/" ng-click="Price_Detail_Click($event,dataItem)" class="k-button"><i class="fa fa-file"></i></a>',
                filterable: false, sortable: false
            },
            {
                field: 'Code', title: 'Tên bảng giá', width: 150,
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'EffectDate', width: 120, title: 'Ngày hiệu lực', template: "#=EffectDate==null?' ':kendo.toString(EffectDate, '" + Common.Date.Format.DMY + "')#",
                filterable: { cell: { template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); }, operator: 'equal', showOperators: false } },
            },
            {
                field: 'Name', title: 'Mô tả',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            }
        ]
    };

    $scope.price_copy_gridOptions = {
        dataSource: Common.DataSource.Local({
            data: []
        }),
        height: '100%', groupable: false, pageable: false, sortable: true, columnMenu: false, filterable: false, resizable: false,
        columns: [
            { field: 'Code', title: 'Mã BG', },
            { field: 'NewCode', title: 'Mã BG mới', template: '<input type="text" class="k-textbox" ng-model="dataItem.NewCode" style="width:100%"/>' },
            { field: 'NewName', title: 'Tên BG mới', template: '<input type="text" class="k-textbox" ng-model="dataItem.NewName" style="width:100%" />' }
        ]
    };

    $scope.price_gridChoose_Change = function ($event, grid, hasChoose) {
        $scope.HasPriceChoose = hasChoose;
    };

    //$scope.Price_Add_Click = function ($event) {
    //    $event.preventDefault();
    //    if ($scope.Item.TypeOfMode == 1) {
    //        $state.go('main.VENContract.PriceCO', {
    //            ID: -1, ContractID: $scope.ID, CustomerID: _VENContract_Detail.Const.CustomerID
    //        });
    //    } else {
    //        $state.go('main.VENContract.PriceDI', {
    //            ID: -1, ContractID: $scope.ID, CustomerID: _VENContract_Detail.Const.CustomerID
    //        });
    //    }
    //};

    $scope.Price_Add_Click = function ($event, win, vform) {
        $event.preventDefault()
        $scope.PriceLoadItem(0, win, vform)
    };

    $scope.Price_Edit_Click = function ($event, item, win, vform) {
        $event.preventDefault();
        $scope.PriceLoadItem(item.ID, win, vform)
    };

    $scope.PriceLoadItem = function (id, win, vform) {
        vform({ clear: true });
        $rootScope.IsLoading = true;
        Common.Services.Call($http, {
            url: Common.Services.url.VEN,
            method: _VENContract_Detail.URL.Price_Get,
            data: { priceID: id },
            success: function (res) {
                $rootScope.IsLoading = false;
                $scope.ItemPrice = res;
                win.center().open();
            },
            error: function (res) {
                $rootScope.IsLoading = false;
            }
        });

    }

    $scope.Price_win_Submit_Click = function ($event, win) {
        $event.preventDefault();
        $rootScope.IsLoading = true;
        Common.Services.Call($http, {
            url: Common.Services.url.VEN,
            method: _VENContract_Detail.URL.Price_Save,
            data: { contractID: $scope.ID, item: $scope.ItemPrice },
            success: function (res) {
                $scope.price_gridOptions.dataSource.read();
                $rootScope.IsLoading = false;
                win.close();
                $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
            },
            error: function (res) {
                $rootScope.IsLoading = false;
            }
        });

    }

    $scope.Price_Detail_Click = function ($event, item) {
        $event.preventDefault();
        if ($scope.Item.TypeOfMode == 1) {
            $state.go('main.VENContract.PriceCO', {
                ID: item.ID, ContractID: $scope.ID, CustomerID: _VENContract_Detail.Const.CustomerID
            });
        } else {
            $state.go('main.VENContract.PriceDI', {
                ID: item.ID, ContractID: $scope.ID, CustomerID: _VENContract_Detail.Const.CustomerID
            });
        }
    }

    //$scope.Price_Edit_Click = function ($event, item) {
    //    $event.preventDefault();

    //    if ($scope.Item.TypeOfMode == 1) {
    //        $state.go('main.VENContract.PriceCO', {
    //            ID: item.ID, ContractID: $scope.ID, CustomerID: _VENContract_Detail.Const.CustomerID
    //        });
    //    } else {
    //        $state.go('main.VENContract.PriceDI', {
    //            ID: item.ID, ContractID: $scope.ID, CustomerID: _VENContract_Detail.Const.CustomerID
    //        });
    //    }
    //};

    $scope.Price_Delete_Click = function ($event, item) {
        $event.preventDefault();
        $rootScope.Message({
            Type: Common.Message.Type.Confirm,
            NotifyType: Common.Message.NotifyType.SUCCESS,
            Title: 'Thông báo',
            Msg: 'Bạn muốn xóa bảng giá đã chọn?',
            Close: null,
            Ok: function () {
                $rootScope.IsLoading = true;
                Common.Services.Call($http, {
                    url: Common.Services.url.VEN,
                    method: _VENContract_Detail.URL.Price_Delete,
                    data: { ID: item.ID },
                    success: function (res) {

                        $scope.price_gridOptions.dataSource.read();
                        $rootScope.IsLoading = false;
                        $rootScope.Message({ Msg: "Đã xóa!" });
                    },
                    error: function (res) {
                        $rootScope.IsLoading = false;
                    }
                });
            }
        })

    };

    $scope.Window_Close_Click = function ($event, win) {
        $event.preventDefault();

        win.close();
    };

    $scope.Price_Copy_Click = function ($event, grid, win) {
        $event.preventDefault();

        var data = [];;
        Common.Data.Each($.grep(grid.dataSource.data(), function (o) { return o.IsChoose == true }), function (item) {
            data.push({
                ID: item.ID, Code: item.Code, NewCode: item.Code + "_Copy", NewName: item.Name + "_Copy"
            })
        })
        if (data.length > 0) {

            $timeout(function () {
                win.center().open();
                $scope.price_copy_gridOptions.dataSource.data(data);
            }, 1)
        }
    };

    $scope.Price_Copy_Submit_Click = function ($event, grid, win) {
        $event.preventDefault();

        var data = grid.dataSource.data();
        var flag = true;
        var lstCode = [];
        $.each(data, function (i, v) {
            v.Value = v.NewCode;
            if (v.NewCode == "" || v.NewCode == null) {
                flag = false;
                lstCode.push(v.Code);
            }
        })
        if (!flag) {
            $rootScope.Message({
                Type: Common.Message.Type.Alert,
                Msg: "Nhập mã mới cho các BD " + lstCode.join(", ")
            });
        } else {
            $rootScope.Message({
                Type: Common.Message.Type.Confirm,
                Msg: "Bạn muốn sao chép các BG đã chọn?",
                Ok: function () {
                    $rootScope.IsLoading = true;
                    Common.Services.Call($http, {
                        url: Common.Services.url.VEN,
                        method: _VENContract_Detail.URL.Price_Copy,
                        data: { data: data },
                        success: function (res) {
                            $rootScope.IsLoading = false;
                            Common.Services.Error(res, function (res) {
                                $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                                $scope.price_gridOptions.dataSource.read();
                                win.close();
                            })
                        },
                        error: function (res) {
                            $rootScope.IsLoading = false;
                        }
                    })
                }
            });
        }
    };

    $scope.cboOrderTypeOptions = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true,
        dataTextField: 'ValueOfVar', dataValueField: 'ID', minLength: 3,
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

    $scope.cboTypeRunOptions = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true,
        dataTextField: 'ValueOfVar', dataValueField: 'ID', minLength: 3,
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
        url: Common.ALL.URL.SYSVarTypeOfOrder,
        success: function (res) {
            if (res.length > 0) {
                $scope.cboOrderTypeOptions.dataSource.data(res);
            }
        }
    })

    Common.ALL.Get($http, {
        url: Common.ALL.URL.TypeOfRunLevel,
        success: function (res) {
            if (res.length > 0) {
                $scope.cboTypeRunOptions.dataSource.data(res);
            }
        }
    });
    //#endregion

    //#region Action

    $scope.Update_Click = function ($event) {
        $event.preventDefault();

        if (Common.HasValue($scope.Item.CustomerID) && $scope.Item.CustomerID > 0) {
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.VEN,
                method: _VENContract_Detail.URL.Save,
                data: { item: $scope.Item },
                success: function (res) {
                    Common.Services.Error(res, function (res) {
                        $rootScope.IsLoading = false;
                        $rootScope.Message({ Msg: "Đã cập nhật!" });

                        $state.go('main.VENContract.Detail', { ID: res });
                    })
                }
            });
        }
        else
            $rootScope.Message({ Msg: "Không tìm thấy khách hàng" });
    };

    $scope.Delete_Click = function ($event) {
        $event.preventDefault();

        $rootScope.Message({
            Type: Common.Message.Type.Confirm,
            Msg: 'Bạn muốn xóa hợp đồng?',
            Ok: function () {
                Common.Services.Call($http, {
                    url: Common.Services.url.VEN,
                    method: _VENContract_Detail.URL.Delete,
                    data: { ID: $scope.Item.ID },
                    success: function (res) {
                        Common.Services.Error(res, function (res) {
                            $rootScope.Message({ Msg: "Đã xóa!" });
                            $state.go('main.VENContract.Index');
                        })
                    }
                });
            }
        })
    };

    $scope.Back_Click = function ($event) {
        $event.preventDefault();

        $state.go('main.VENContract.Index');
    };

    $scope.Window_Close_Click = function ($event, win) {
        $event.preventDefault();

        win.close();
    };
    //#endregion

    //#region GOP
    $scope.HasGOPChoose = false;

    $scope.gop_gridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.VEN,
            method: _VENContract_Detail.URL.GroupOfProduct_List,
            readparam: function () {
                return {
                    contractID: $scope.ID
                }
            },
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                   // IsChoose: { type: 'bool' },
                }
            },
            pageSize: 100
        }),
        height: '99%', groupable: false, pageable: true, sortable: true, columnMenu: false, resizable: true,
        selectable: false, filterable: { mode: 'row' }, reorderable: false,
        toolbar: kendo.template($('#gop_grid_toolbar').html()),
        columns: [
            {
                title: ' ', width: '85px',
                template: '<a href="/" ng-click="GOP_Edit_Click($event,gop_win,gop_grid,gop_win_vform)" class="k-button"><i class="fa fa-pencil"></i></a>' +
                    '<a href="/" ng-click="GOP_Delete_Click($event,dataItem,gop_grid)" class="k-button"><i class="fa fa-trash"></i></a>',
                filterable: false, sortable: false
            },
            {
                field: 'GroupOfProductName', title: 'Nhóm hàng', width: 150,
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'Expression', title: 'Công thức',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
        ]
    }

    $scope.gop_gridChoose_Change = function ($event, grid, hasChoose) {
        $scope.HasGOPChoose = hasChoose;
    }

    $scope.GOP_Add_Click = function ($event, win, vform) {
        $event.preventDefault();

        $scope.GOP_LoadItem(win, -1, vform);
    };

    $scope.GOP_Edit_Click = function ($event, win, grid, vform) {
        $event.preventDefault();

        var tr = $($event.target).closest('tr');
        var item = grid.dataItem(tr);

        $scope.GOP_LoadItem(win, item.ID, vform);
    };

    $scope.GOP_Delete_Click = function ($event, dataItem, grid) {
        $event.preventDefault();
        if (dataItem.ID > 0) {
            var lstid = [];
            lstid.push(dataItem.ID);
            Common.Services.Call($http, {
                url: Common.Services.url.VEN,
                method: _VENContract_Detail.URL.GroupOfProduct_Delete,
                data: { lstid: lstid },
                success: function (res) {
                    Common.Services.Error(res, function (res) {
                        grid.dataSource.read();
                        $rootScope.Message({ Msg: 'Đã xóa' });
                    })
                }
            });
        }
    };

    $scope.GOP_LoadItem = function (win, id, vform) {
        vform({ clear: true });

        Common.Services.Call($http, {
            url: Common.Services.url.VEN,
            method: _VENContract_Detail.URL.GroupOfProduct_Get,
            data: { id: id, contractID: $scope.ID },
            success: function (res) {
                if (Common.HasValue(res)) {
                    $timeout(function () {
                        $scope.GOPItem = res;
                    }, 1);
                }
            }
        });

        win.center();
        win.open();
    };

    $scope.GOP_Save_Click = function ($event, win, grid, vform) {
        $event.preventDefault();

        if (vform()) {
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.VEN,
                method: _VENContract_Detail.URL.GroupOfProduct_Save,
                data: { item: $scope.GOPItem, contractID: $scope.ID },
                success: function (res) {
                    $rootScope.IsLoading = false;
                    win.close();
                    grid.dataSource.read();
                    $rootScope.Message({ Msg: 'Đã cập nhật' });
                }
            });
        }

    };

    $scope.GOP_Check_Expression_Click = function ($event, win, grid, vform) {
        $event.preventDefault();

        if (vform()) {
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.VEN,
                method: _VENContract_Detail.URL.GroupOfProduct_Check,
                data: { item: $scope.GOPItem },
                success: function (res) {
                    $rootScope.IsLoading = false;
                    Common.Services.Error(res, function (res) {
                        $rootScope.Message({
                            Type: Common.Message.Type.Alert, Msg: res
                        });
                    })
                }
            });
        }

    };
    //#endregion

    $scope.Material_Detail_Click = function ($event, data) {
        $event.preventDefault();
        $state.go("main.VENContract.Material", { ContractMaterialID: data.ID, StatusID: 1 });
    };

    $scope.Routing_Search_Click = function ($event, win) {
        $event.preventDefault();
        $scope.routing_search_gridOptions.dataSource.data();
        $timeout(function () { $scope.routing_search_grid.resize(); }, 1);
        win.center().open();
    };

    $scope.routing_search_gridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.VEN,
            method: _VENContract_Detail.URL.Routing_search_List,
            readparam: function () {
                return {
                    contractID: $scope.ID
                }
            },
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    IsChoose: { type: 'bool' }
                }
            },
            pageSize: 100
        }),
        height: '99%', groupable: false, pageable: true, sortable: true, columnMenu: false, resizable: true,
        selectable: true, filterable: { mode: 'row' }, reorderable: true,
        columns: [
            {
                title: 'Chọn', width: '45px',
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,routing_search_grid)" />',
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,routing_search_grid)" />',
                filterable: false, sortable: false
            },
            {
                field: 'Code', title: 'Mã cung đường', width: 150, filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'RoutingName', title: 'Tên cung đường', filterable: { cell: { operator: 'contains', showOperators: false } }
            },
        ]
    };

    $scope.Routing_Search_Save_Click = function ($event, grid, win) {
        $event.preventDefault();

        var data = [];
        Common.Data.Each(grid.dataSource.data(), function (o) {
            if (o.IsChoose == true) data.push(o);
        })
        if (data.length > 0) {
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.VEN,
                method: _VENContract_Detail.URL.Routing_search_save,
                data: { contractID: $scope.ID, data: data },
                success: function (res) {
                    $rootScope.IsLoading = false;
                    $rootScope.Message({
                        Msg: 'Đã cập nhật.',
                        NotifyType: Common.Message.NotifyType.SUCCESS
                    });
                    $scope.CreateGridRouting();
                    win.close();
                },
                error: function (res) {
                    $rootScope.IsLoading = false;
                }
            });

        }
    }

    $scope.numSortOrder_options = { format: 'n0', spinners: false, culture: 'en-US', min: 1, step: 1, decimals: 0, }
}]);