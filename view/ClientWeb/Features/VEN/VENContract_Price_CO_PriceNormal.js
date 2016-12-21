/// <reference path="~/Scripts/jquery-1.9.1.intellisense.js" />
/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />

var _VENContract_PriceCO_PriceNormal = {
    URL: {
        Get: 'VENContract_Price_Get',

        Data: 'VENContract_PriceCO_Data',

        GroupContainer_GetData: 'VENPrice_CO_COContainer_Data',
        GroupContainer_SaveList: 'VENPrice_CO_COContainer_SaveList',

        GroupContainer_NotInList: 'VENPrice_CO_COContainer_ContainerNotInList',
        GroupContainer_NotInSave: 'VENPrice_CO_COContainer_ContainerNotInSave',

        GroupContainer: 'VENPrice_CO_COContainer_ContainerList',
        GroupContainerDelete: 'VENPrice_CO_COContainer_ContainerDelete',

        GroupContainer_Export: 'VENPrice_CO_GroupContainer_ExcelExport',
        GroupContainer_Check: 'VENPrice_CO_GroupContainer_ExcelCheck',
        GroupContainer_Import: 'VENPrice_CO_GroupContainer_ExcelImport',

        ExcelInit: 'VENPrice_CO_GroupContainer_ExcelInit',
        ExcelChange: 'VENPrice_CO_GroupContainer_ExcelChange',
        ExcelImport: 'VENPrice_CO_GroupContainer_ExcelOnImport',
        ExcelApprove: 'VENPrice_CO_GroupContainer_ExcelApprove',
    },
    Data: {
        ListPackingTU: null,
        ListPackingCO: null,
        ListService: null,
        ListRouting:null,

        ListPriceOfGOP: null,
        ListGroupContainer: null,
        ListGroupProduct: null,
        ListGroupPartner: null,
        ListTypeOfPriceEX: null,
        ListRoutingParent: [],
        ListLocation: [],
        LisAllRoute: [],
        ListTypeOfPriceCOEx: [],
        objModel: {},
        ListLevel: [],
        ListFieldLevel: [],
        ItemPriceBackup: null,
        ItemGOVBackup: null,
        objModelGov: {},
        ListGovLevel: [],
        //8/5
        ListGroupLocation: null,
    },
    Params: {
        //ID: -1,
        //CustomerID: -1,
        //ContractID: -1
        PriceID: -1,
        TermID: -1,
    }
}

angular.module('myapp').controller('VENContract_Price_CO_PriceNormalCtrl', ['$rootScope', '$scope', '$http', '$location', '$state', '$timeout', '$stateParams', function ($rootScope, $scope, $http, $location, $state, $timeout, $stateParams) {
    Common.Log('VENContract_Price_CO_PriceNormalCtrl');
    $rootScope.IsLoading = false;

    $scope.Item = null;
    _VENContract_PriceCO_PriceNormal.Params = $.extend({}, true, $state.params);
    $scope.GroupContainerHasChoose = false;

    $scope.IsFrame = false;

    $rootScope.IsLoading = true;
    Common.Services.Call($http, {
        url: Common.Services.url.VEN,
        method: _VENContract_PriceCO_PriceNormal.URL.Data,
        data: { contractTermID: _VENContract_PriceCO_PriceNormal.Params.TermID },
        success: function (res) {
            _VENContract_PriceCO_PriceNormal.Data.ListPackingCO = res.ListPackingCO;
            _VENContract_PriceCO_PriceNormal.Data.ListPackingTU = res.ListPackingTU;
            _VENContract_PriceCO_PriceNormal.Data.ListService = res.ListService;
            _VENContract_PriceCO_PriceNormal.Data.ListRouting = res.ListRouting;

            Common.Services.Call($http, {
                url: Common.Services.url.VEN,
                method: _VENContract_PriceCO_PriceNormal.URL.Get,
                data: { priceID: _VENContract_PriceCO_PriceNormal.Params.PriceID },
                success: function (res) {
                    $scope.Item = res;

                    if ($scope.Item.ID > 0) {
                        $timeout(function () {
                            var isframe = $scope.Item.TypeOfContract == 1 ? false : true;
                            $scope.IsFrame = $scope.Item.TypeOfContract == 1 ? false : true;
                            if ($scope.Item.TypeOfMode == 1) 
                               $scope.InitView_GroupContainer(isframe);
                            //} else if ($scope.Item.TypeOfMode == 3) {
                            //    $scope.InitView_GroupProduct(isframe);
                            //    $timeout(function () {
                            //        $scope.LoadData_GroupProduct();
                            //    }, 1)
                            //}
                            
                        }, 1)
                    }
                }
            })
        }
    });

    //#region code van chuyen FTL cu
    $scope.InitView_GroupContainer = function (isframe) {
        Common.Log("InitView_GroupContainer");

        Common.Services.Call($http, {
            url: Common.Services.url.VEN,
            method: _VENContract_PriceCO_PriceNormal.URL.GroupContainer_GetData,
            data: { priceID: _VENContract_PriceCO_PriceNormal.Params.PriceID },
            success: function (res) {
                var Model = {
                    id: 'RouteID',
                    fields: {
                        RuoteCode: { type: 'string', editable: false },
                        RuoteName: { type: 'string', editable: false },
                        ContractRoutingID: { type: 'number', editable: false },
                        SortOrder: { type: 'number', editable: false },
                    }
                }
                var GridColumn = [
                    { field: 'RuoteCode', title: "Mã cung đường", width: 120, locked: true, filterable: { cell: { operator: 'contains', showOperators: false } } },
                    { field: 'RuoteName', title: "Tên cung đường", width: 150, locked: true, filterable: { cell: { operator: 'contains', showOperators: false } } },
                    { field: 'SortOrder', title: "Thứ tự CĐ", width: 100, locked: true, filterable: { cell: { operator: 'equal', showOperators: false } } }
                ]
                if (isframe) {//hợp đồng khung
                    Common.Data.Each(res.ListPacking, function (gov) {
                        var listCol = [];
                        var fieldMax = "L" + gov.ID + "MAX";
                        var fieldMin = "L" + gov.ID + "MIN";
                        Model.fields[fieldMax] = { type: "number", editable: true };
                        Model.fields[fieldMin] = { type: "number", editable: true };
                        listCol.push({
                            field: fieldMin, title: "Giá từ", width: 120, locked: false,
                            template: '#=' + fieldMin + '==null?"": Common.Number.ToMoney(' + fieldMin + ')#',
                            editor: function (container, options) {
                                var input = $('<input name="' + options.field + '"  />');
                                input.appendTo(container);
                                input.kendoNumericTextBox({
                                    format: 'n0', spinners: false, culture: 'en-US', min: 0, step: 1, decimals: 0
                                });
                            }
                        });
                        listCol.push({
                            field: fieldMax, title: "Đến giá", width: 120, locked: false,
                            template: '#=' + fieldMax + '==null?"": Common.Number.ToMoney(' + fieldMax + ')#',
                            editor: function (container, options) {
                                var input = $('<input name="' + options.field + '"  />');
                                input.appendTo(container);
                                input.kendoNumericTextBox({
                                    format: 'n0', spinners: false, culture: 'en-US', min: 0, step: 1, decimals: 0
                                });
                            }
                        })
                        GridColumn.push({ title: gov.Code, columns: listCol, headerAttributes: { style: "text-align: center;" }, })
                    })
                }
                else {//ko có khung chi hien 1 cot gia
                    Common.Data.Each(res.ListPacking, function (gov) {
                        var listCol = [];
                        var field = "L" + gov.ID;
                        Model.fields[field] = { type: "number", editable: true };
                        GridColumn.push({
                            field: field, title: gov.Code, width: 120, locked: false,
                            template: '#=' + field + '==null?"": Common.Number.ToMoney(' + field + ')#',
                            editor: function (container, options) {
                                var input = $('<input name="' + options.field + '"  />');
                                input.appendTo(container);
                                input.kendoNumericTextBox({
                                    format: 'n0', spinners: false, culture: 'en-US', min: 0, step: 1, decimals: 0
                                });
                            }
                        });
                    })
                }

                GridColumn.push({ title: ' ', filterable: false, sortable: false })
                $scope.goc_grid.setOptions({
                    dataSource: Common.DataSource.Local({
                        data: [],
                        model: Model,
                        pageSize: 0,
                        sort: [{ field: "SortOrder", dir: "asc" }]
                    }),
                    height: '100%', pageable: false, sortable: true, columnMenu: false, resizable: true, reorderable: false, filterable: false, editable: { mode: 'incell' },
                    columns: GridColumn
                })

                //tao data source
                var dataCheck = {};
                Common.Data.Each(res.ListDetail, function (o) {
                    if (!Common.HasValue(dataCheck["R" + o.ContractRoutingID + "_L" + o.PackingID]))
                        dataCheck["R" + o.ContractRoutingID + "_L" + o.PackingID] = o;
                })
                var dataGrid = [];
                if (isframe) {
                    Common.Data.Each(_VENContract_PriceCO_PriceNormal.Data.ListRouting, function (route) {
                        var item = {};
                        item["RuoteCode"] = route.Code;
                        item["RuoteName"] = route.RoutingName;
                        item["RouteID"] = route.ID;
                        item["SortOrder"] = route.SortOrder;
                        Common.Data.Each(res.ListPacking, function (level) {
                            var fieldMax = "L" + level.ID + "MAX";
                            var fieldMin = "L" + level.ID + "MIN";
                            item[fieldMax] = 0;
                            item[fieldMin] = 0;
                            if (Common.HasValue(dataCheck["R" + route.ID + "_L" + level.ID])) {
                                item[fieldMax] = dataCheck["R" + route.ID + "_L" + level.ID].PriceMax > 0 ? dataCheck["R" + route.ID + "_L" + level.ID].PriceMax : 0;
                                item[fieldMin] = dataCheck["R" + route.ID + "_L" + level.ID].PriceMin > 0 ? dataCheck["R" + route.ID + "_L" + level.ID].PriceMin : 0;
                            }
                        });
                        dataGrid.push(item);
                    })
                }
                else {
                    Common.Data.Each(_VENContract_PriceCO_PriceNormal.Data.ListRouting, function (route) {
                        var item = {};
                        item["RuoteCode"] = route.Code;
                        item["RuoteName"] = route.RoutingName;
                        item["RouteID"] = route.ID;
                        item["SortOrder"] = route.SortOrder;
                        Common.Data.Each(res.ListPacking, function (level) {
                            var field = "L" + level.ID;
                            item[field] = 0;
                            if (Common.HasValue(dataCheck["R" + route.ID + "_L" + level.ID])) {
                                item[field] = dataCheck["R" + route.ID + "_L" + level.ID].Price > 0 ? dataCheck["R" + route.ID + "_L" + level.ID].Price : 0;
                            }
                        });
                        dataGrid.push(item);
                    })
                }

                $scope.goc_grid.dataSource.data(dataGrid)
                $rootScope.IsLoading = false;
            },
            error: function (res) {
                $rootScope.IsLoading = false;
            }
        });
    }

    $scope.GroupContainer_Save_Click = function ($event, grid) {
        $event.preventDefault();
        var data = grid.dataSource.data();
        var dataSave = [];
        if ($scope.IsFrame) {
            Common.Data.Each(data, function (row) {
                Common.Data.Each(_VENContract_PriceCO_PriceNormal.Data.ListPackingCO, function (level) {
                    if (row["L" + level.ID + "MAX"] >= 0 || row["L" + level.ID + "MIN"] >= 0) {
                        dataSave.push({
                            ContractRoutingID: row.RouteID,
                            PackingID: level.ID,
                            Price: 0,
                            PriceMax: row["L" + level.ID + "MAX"],
                            PriceMin: row["L" + level.ID + "MIN"],
                        })
                    }
                })
            })
        }
        else {
            Common.Data.Each(data, function (row) {
                Common.Data.Each(_VENContract_PriceCO_PriceNormal.Data.ListPackingCO, function (level) {
                    if (row["L" + level.ID] >= 0) {
                        dataSave.push({
                            ContractRoutingID: row.RouteID,
                            PackingID: level.ID,
                            Price: row["L" + level.ID],
                            PriceMax: null,
                            PriceMin: null,
                        })
                    }
                })
            })
        }


        $rootScope.IsLoading = true;
        Common.Services.Call($http, {
            url: Common.Services.url.VEN,
            method: _VENContract_PriceCO_PriceNormal.URL.GroupContainer_SaveList,
            data: { data: dataSave, priceID: _VENContract_PriceCO_PriceNormal.Params.PriceID },
            success: function (res) {
                $scope.InitView_GroupContainer($scope.IsFrame);
                $rootScope.IsLoading = false;
                $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
            },
            error: function (res) {
                $rootScope.IsLoading = false;
            }
        });
    }

    $scope.GroupContainer_CancelChange_Click = function ($event, grid) {
        $event.preventDefault();

        grid.cancelChanges();
    };
    //#region Group Of Container

    $scope.GroupContainer_Choose_Click = function ($event, win) {
        $event.preventDefault();
        $scope.groupOfContainer_GridOptions.dataSource.read();
        $scope.groupOfContainer_Grid.refresh();
        win.center().open();
    }
    $scope.groupOfContainer_GridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.VEN,
            method: _VENContract_PriceCO_PriceNormal.URL.GroupContainer,
            readparam: function () { return { priceID: _VENContract_PriceCO_PriceNormal.Params.PriceID } },
            model: {
                id: 'ID',
                fields: {
                    parentId: { from: 'ParentID', type: 'number', editable: true, nullable: true },
                    ID: { type: 'number' },
                    Ton: { type: 'number' },
                    IsChoose: { type: 'boolean' },
                }
            }
        }),
        height: '100%', pageable: true, sortable: true, columnMenu: false, resizable: true, reorderable: true, filterable: false,
        columns: [
            {
                title: ' ', width: '35px',
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,groupOfContainer_Grid,groupOfContainer_GridChoose_Change)" />',
                headerAttributes: { style: 'text-align: center;' }, attributes: { style: 'text-align: center;' },
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,groupOfContainer_Grid,groupOfContainer_GridChoose_Change)" />',
                filterable: false, sortable: false
            },
            {
                field: 'Code', title: 'Mã loại container', width: 200,
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'PackingName', title: 'Tên loại container', width: 200,
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            { title: '', filterable: false, sortable: false },
        ]
    };

    $scope.groupOfContainer_GridChoose_Change = function ($event, grid, hasChoose) {
        $scope.GroupContainerHasChoose = hasChoose;
    }

    $scope.GroupContainer_Choose_DeleteClick = function ($event, grid) {
        $event.preventDefault();
        var data = grid.dataSource.data();
        var datasend = [];
        Common.Data.Each(data, function (o) {
            if (o.IsChoose) datasend.push(o.ID);
        })
        if (datasend.length > 0) {
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.VEN,
                method: _VENContract_PriceCO_PriceNormal.URL.GroupContainerDelete,
                data: { lst: datasend, priceID: _VENContract_PriceCO_PriceNormal.Params.PriceID },
                success: function (res) {
                    $scope.InitView_GroupContainer();
                    $rootScope.IsLoading = false;
                    $scope.groupOfContainer_GridOptions.dataSource.read();
                    $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                },
                error: function (res) {
                    $rootScope.IsLoading = false;
                }
            });

        }
    }

    $scope.GroupContainer_ChooseNotIn_Click = function ($event, win) {
        $event.preventDefault();

        $scope.groupOfContainer_NotIn_GridOptions.dataSource.read();
        win.center().open();
        $scope.groupOfContainer_NotIn_Grid.refresh();
    }
    $scope.groupOfContainer_NotIn_GridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.VEN,
            method: _VENContract_PriceCO_PriceNormal.URL.GroupContainer_NotInList,
            readparam: function () { return { priceID: _VENContract_PriceCO_PriceNormal.Params.PriceID } },
            model: {
                id: 'ID',
                fields: {
                    parentId: { from: 'ParentID', type: 'number', editable: true, nullable: true },
                    ID: { type: 'number' },
                    Ton: { type: 'number' },
                    IsChoose: { type: 'boolean' },
                }
            }
        }),
        height: '100%', pageable: true, sortable: true, columnMenu: false, resizable: true, reorderable: true, filterable: false,
        columns: [
            {
                title: ' ', width: '35px',
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,groupOfContainer_NotIn_Grid,groupOfContainer_GridChooseNotIn_Change)" />',
                headerAttributes: { style: 'text-align: center;' }, attributes: { style: 'text-align: center;' },
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,groupOfContainer_NotIn_Grid,groupOfContainer_GridChooseNotIn_Change)" />',
                filterable: false, sortable: false
            },
            {
                field: 'Code', title: 'Mã loại container', width: 200,
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'PackingName', title: 'Tên loại container',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            { title: '', filterable: false, sortable: false },
        ]
    };

    $scope.groupOfContainer_GridChooseNotIn_Change = function ($event, grid, hasChoose) {
    }

    $scope.groupOfContainer_Save_Click = function ($event, win, grid) {
        $event.preventDefault();
        var data = grid.dataSource.data();
        var datasend = [];
        Common.Data.Each(data, function (o) {
            if (o.IsChoose) datasend.push(o.ID);
        })
        if (datasend.length > 0) {
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.VEN,
                method: _VENContract_PriceCO_PriceNormal.URL.GroupContainer_NotInSave,
                data: { priceID: _VENContract_PriceCO_PriceNormal.Params.PriceID, lst: datasend },
                success: function (res) {
                    $rootScope.IsLoading = false;
                    $scope.groupOfContainer_GridOptions.dataSource.read();
                    $scope.InitView_GroupContainer($scope.IsFrame);
                    win.close();
                    $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                },
                error: function (res) {
                    $rootScope.IsLoading = false;
                }
            });
        } else {
            $rootScope.Message({ Msg: 'Chưa chọn loại container', NotifyType: Common.Message.NotifyType.ERROR });
        }
    }
    //#endregion

    //#region excel
    $scope.GroupContainer_Excel_Click = function ($event, win) {
        $rootScope.UploadExcel({
            Path: Common.FolderUpload.Import,
            columns: [
                { field: 'RouteCode', title: 'Mã cung đường', width: 120 },
                { field: 'RouteName', title: 'Tên cung đường', width: 120 },
                { field: 'SortOrder', title: 'Thứ tự CĐ', width: 120 }
            ],
            Download: function () {
                $rootScope.IsLoading = true;
                Common.Services.Call($http, {
                    url: Common.Services.url.VEN,
                    method: _VENContract_PriceCO_PriceNormal.URL.GroupContainer_Export,
                    data: { priceID: _VENContract_PriceCO_PriceNormal.Params.PriceID, contractTermID: _VENContract_PriceCO_PriceNormal.Params.TermID, isFrame: $scope.IsFrame },
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
                    url: Common.Services.url.VEN,
                    method: _VENContract_PriceCO_PriceNormal.URL.GroupContainer_Check,
                    data: { file: e, priceID: _VENContract_PriceCO_PriceNormal.Params.PriceID, contractTermID: _VENContract_PriceCO_PriceNormal.Params.TermID, isFrame: $scope.IsFrame },
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
                    method: _VENContract_PriceCO_PriceNormal.URL.GroupContainer_Import,
                    data: { lst: data, priceID: _VENContract_PriceCO_PriceNormal.Params.PriceID },
                    success: function (res) {
                        $rootScope.IsLoading = false;
                        $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                        $scope.InitView_GroupContainer($scope.IsFrame);
                    },
                    error: function (res) {
                        $rootScope.IsLoading = false;
                    }
                })
            }
        })
    }

    //#endregion

    $scope.GroupContainer_ExcelOn_Click = function ($event) {
        $event.preventDefault();

        var lstMessageError = [];
        for (var i = 0; i < 2; i++) {
            var resource = $rootScope.RS[_FLMDriver.ExcelKey.Resource + '_' + i];
            if (Common.HasValue(resource))
                lstMessageError.push(resource);
        }
        if (lstMessageError.length == 0) {
            lstMessageError = [
                '[Mã cung đường] không được trống và > 50',
                '[Mã cung đường] không tồn tại trong hệ thống',
                '[Giá] là số',
            ];
        }

        $rootScope.excelShare.Init({
            functionkey: 'CUSContract_Price_DI_PriceNormal',
            params: {
                priceID: _VENContract_PriceCO_PriceNormal.Params.PriceID,
                contractTermID: _VENContract_PriceCO_PriceNormal.Params.TermID,
                isFrame: $scope.IsFrame
            },
            rowStart: 1,
            colCheckChange: 6,
            url: Common.Services.url.VEN,
            methodInit: _VENContract_PriceCO_PriceNormal.URL.ExcelInit,
            methodChange: _VENContract_PriceCO_PriceNormal.URL.ExcelChange,
            methodImport: _VENContract_PriceCO_PriceNormal.URL.ExcelImport,
            methodApprove: _VENContract_PriceCO_PriceNormal.URL.ExcelApprove,
            lstMessageError: lstMessageError,
            Changed: function () {

            },
            Approved: function () {
                $scope.InitView_GroupContainer($scope.IsFrame);
            }
        });
    };

    $scope.Back_Click = function ($event) {
        $event.preventDefault();
        $state.go("main.VENContract.Term", { TermID: _VENContract_PriceCO_PriceNormal.Params.TermID, ContractID: $scope.Item.ContractID })
    }
    $scope.window_Close_Click = function ($event, win) {
        $event.preventDefault();
        win.close();
    }
}]);