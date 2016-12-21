/// <reference path="~/Scripts/jquery-1.9.1.intellisense.js" />
/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />

var _FLMContract_PriceDI_PriceNormal = {
    URL: {
        Get: 'FLMContract_Price_Get',

        Data: 'FLMContract_Price_Data',

        GroupVehicle_GetData: 'FLMPrice_DI_GroupVehicle_GetData',
        GroupVehicle_SaveList: 'FLMPrice_DI_GroupVehicle_SaveList',

        GroupVehicle_Export: 'FLMPrice_DI_GroupVehicle_ExcelExport',
        GroupVehicle_Check: 'FLMPrice_DI_GroupVehicle_ExcelCheck',
        GroupVehicle_Import: 'FLMPrice_DI_GroupVehicle_ExcelImport',

        GroupProduct_Data: 'FLMPrice_DI_GroupProduct_List',
        GroupProduct_SaveList: 'FLMPrice_DI_GroupProduct_SaveList',

        GroupProduct_Export: 'FLMPrice_DI_GroupProduct_Export',
        GroupProduct_Check: 'FLMPrice_DI_GroupProduct_Check',
        GroupProduct_Import: 'FLMPrice_DI_GroupProduct_Import',

        GroupVehicle_ExcelInit: 'FLMPrice_DI_GroupVehicle_ExcelInit',
        GroupVehicle_ExcelChange: 'FLMPrice_DI_GroupVehicle_ExcelChange',
        GroupVehicle_ExcelImport: 'FLMPrice_DI_GroupVehicle_ExcelOnImport',
        GroupVehicle_ExcelApprove: 'FLMPrice_DI_GroupVehicle_ExcelApprove',

        ExcelInit: 'FLMPrice_DI_GroupProduct_ExcelInit',
        ExcelChange: 'FLMPrice_DI_GroupProduct_ExcelChange',
        ExcelImport: 'FLMPrice_DI_GroupProduct_ExcelOnImport',
        ExcelApprove: 'FLMPrice_DI_GroupProduct_ExcelApprove',

    },
    Data: {
        ListGroupVehicle: null,
        ListGroupProduct: null,
        ListRouting: [],
    },
    Params: {
        PriceID: -1,
        TermID: -1,
        //CustomerID: -1,
        //ContractID: -1
    }
}

angular.module('myapp').controller('FLMSetting_Price_DI_PriceNormalCtrl', ['$rootScope', '$scope', '$http', '$location', '$state', '$timeout', '$stateParams', function ($rootScope, $scope, $http, $location, $state, $timeout, $stateParams) {
    Common.Log('FLMSetting_Price_DI_PriceNormalCtrl');
    $rootScope.IsLoading = false;

    $scope.Item = null;
    _FLMContract_PriceDI_PriceNormal.Params = $.extend(true, _FLMContract_PriceDI_PriceNormal.Params, $state.params);
    $scope.GroupVehicleHasChoose = false;
    $scope.ItemPriceEx = null;

    $scope.IsFTL = true;
    $scope.IsFrame = false;

    $rootScope.IsLoading = true;
    Common.Services.Call($http, {
        url: Common.Services.url.FLM,
        method: _FLMContract_PriceDI_PriceNormal.URL.Data,
        data: { contractTermID: _FLMContract_PriceDI_PriceNormal.Params.TermID },
        success: function (res) {
            _FLMContract_PriceDI_PriceNormal.Data.ListGroupVehicle = res.ListGroupOfVehicle;
            _FLMContract_PriceDI_PriceNormal.Data.ListGroupProduct = res.ListGroupOfProduct;
            _FLMContract_PriceDI_PriceNormal.Data.ListRouting = res.ListRouting;

            Common.Services.Call($http, {
                url: Common.Services.url.FLM,
                method: _FLMContract_PriceDI_PriceNormal.URL.Get,
                data: { priceID: _FLMContract_PriceDI_PriceNormal.Params.PriceID },
                success: function (res) {
                    $scope.Item = res;

                    if ($scope.Item.ID > 0) {
                        $timeout(function () {
                            var isframe = $scope.Item.TypeOfContract == 1 ? false : true;
                            $scope.IsFrame = $scope.Item.TypeOfContract == 1 ? false : true;
                            if ($scope.Item.TypeOfMode == 2) {
                                $scope.InitView_GroupVehicle(isframe);
                            } else if ($scope.Item.TypeOfMode == 3) {
                                $scope.InitView_GroupProduct(isframe);
                            }

                        }, 1)
                    }
                }
            })
        }
    });

    $scope.Back_Click = function ($event) {
        $event.preventDefault();
        $state.go("main.FLMSetting.Term", { TermID: _FLMContract_PriceDI_PriceNormal.Params.TermID, ContractID: $scope.Item.ContractID })
    }

    //#region code van chuyen FTL thuong
    $scope.InitView_GroupVehicle = function (isframe) {
        Common.Log("InitView_GroupVehicle");

        Common.Services.Call($http, {
            url: Common.Services.url.FLM,
            method: _FLMContract_PriceDI_PriceNormal.URL.GroupVehicle_GetData,
            data: { priceID: _FLMContract_PriceDI_PriceNormal.Params.PriceID },
            success: function (res) {
                var Model = {
                    id: 'RouteID',
                    fields: {
                        RuoteCode: { type: 'string', editable: false },
                        RuoteName: { type: 'string', editable: false },
                        RouteID: { type: 'number', editable: false },
                        SortOrder: { type: 'number', editable: false },
                    }
                }
                var GridColumn = [
                    { field: 'RuoteCode', title: "Mã cung đường", width: 120, locked: true, filterable: { cell: { operator: 'contains', showOperators: false } } },
                    { field: 'RuoteName', title: "Tên cung đường", width: 150, locked: true, filterable: { cell: { operator: 'contains', showOperators: false } } },
                    { field: 'SortOrder', title: "Thứ tự CĐ", width: 100, locked: true, filterable: { cell: { operator: 'equal', showOperators: false } } }
                ]
                if (isframe) {//hợp đồng khung
                    Common.Data.Each(_FLMContract_PriceDI_PriceNormal.Data.ListGroupVehicle, function (gov) {
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
                    Common.Data.Each(_FLMContract_PriceDI_PriceNormal.Data.ListGroupVehicle, function (gov) {
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
                $scope.gov_grid.setOptions({
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
                Common.Data.Each(res, function (o) {
                    if (!Common.HasValue(dataCheck["R" + o.RouteID + "_L" + o.GroupOfVehicleID]))
                        dataCheck["R" + o.RouteID + "_L" + o.GroupOfVehicleID] = o;
                })

                var dataGrid = [];
                if (isframe) {
                    Common.Data.Each(_FLMContract_PriceDI_PriceNormal.Data.ListRouting, function (route) {
                        var item = {};
                        item["RuoteCode"] = route.Code;
                        item["RuoteName"] = route.RoutingName;
                        item["RouteID"] = route.ID;
                        item["SortOrder"] = route.SortOrder;
                        Common.Data.Each(_FLMContract_PriceDI_PriceNormal.Data.ListGroupVehicle, function (level) {
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
                    Common.Data.Each(_FLMContract_PriceDI_PriceNormal.Data.ListRouting, function (route) {
                        var item = {};
                        item["RuoteCode"] = route.Code;
                        item["RuoteName"] = route.RoutingName;
                        item["RouteID"] = route.ID;
                        item["SortOrder"] = route.SortOrder;
                        Common.Data.Each(_FLMContract_PriceDI_PriceNormal.Data.ListGroupVehicle, function (level) {
                            var field = "L" + level.ID;
                            item[field] = 0;
                            if (Common.HasValue(dataCheck["R" + route.ID + "_L" + level.ID])) {
                                item[field] = dataCheck["R" + route.ID + "_L" + level.ID].Price > 0 ? dataCheck["R" + route.ID + "_L" + level.ID].Price : 0;
                            }
                        });
                        dataGrid.push(item);
                    })
                }

                $scope.gov_grid.dataSource.data(dataGrid)
                $rootScope.IsLoading = false;
            },
            error: function (res) {
                $rootScope.IsLoading = false;
            }
        });
    }
    $scope.GroupVehicle_Excel_Click = function ($event, win) {
        $rootScope.UploadExcel({
            Path: Common.FolderUpload.Import,
            columns: [
                { field: 'RouteCode', title: 'Mã cung đường', width: 120 },
                { field: 'RouteName', title: 'Tên cung đường', width: 120 }
            ],
            Download: function () {
                $rootScope.IsLoading = true;
                Common.Services.Call($http, {
                    url: Common.Services.url.FLM,
                    method: _FLMContract_PriceDI_PriceNormal.URL.GroupVehicle_Export,
                    data: { priceID: _FLMContract_PriceDI_PriceNormal.Params.PriceID, contractTermID: _FLMContract_PriceDI_PriceNormal.Params.TermID, isFrame: $scope.IsFrame },
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
                    url: Common.Services.url.FLM,
                    method: _FLMContract_PriceDI_PriceNormal.URL.GroupVehicle_Check,
                    data: { file: e, priceID: _FLMContract_PriceDI_PriceNormal.Params.PriceID, contractTermID: _FLMContract_PriceDI_PriceNormal.Params.TermID, isFrame: $scope.IsFrame },
                    success: function (data) {
                        $rootScope.IsLoading = false;
                        callback(data);
                    }
                })
            },
            Window: win,
            Complete: function (e, data) {
                if (!$scope.Item.TermClosed) {
                    $rootScope.IsLoading = true;
                    Common.Services.Call($http, {
                        url: Common.Services.url.FLM,
                        method: _FLMContract_PriceDI_PriceNormal.URL.GroupVehicle_Import,
                        data: { lst: data, priceID: _FLMContract_PriceDI_PriceNormal.Params.PriceID },
                        success: function (res) {
                            $rootScope.IsLoading = false;
                            $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                            $scope.InitView_GroupVehicle($scope.IsFrame);
                        },
                        error: function (res) {
                            $rootScope.IsLoading = false;
                        }
                    })
                } else {
                    $rootScope.Message({ Msg: 'Phụ lục đã đóng', NotifyType: Common.Message.NotifyType.ERROR });
                }
            }
        })
    }

    $scope.GroupVehicle_Save_Click = function ($event, grid) {
        $event.preventDefault();
        var data = grid.dataSource.data();
        var dataSave = [];
        if ($scope.IsFrame) {
            Common.Data.Each(data, function (row) {
                Common.Data.Each(_FLMContract_PriceDI_PriceNormal.Data.ListGroupVehicle, function (level) {
                    if (row["L" + level.ID + "MAX"] >= 0 || row["L" + level.ID + "MIN"] >= 0) {
                        dataSave.push({
                            RouteID: row.RouteID,
                            GroupOfVehicleID: level.ID,
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
                Common.Data.Each(_FLMContract_PriceDI_PriceNormal.Data.ListGroupVehicle, function (level) {
                    if (row["L" + level.ID] >= 0) {
                        dataSave.push({
                            RouteID: row.RouteID,
                            GroupOfVehicleID: level.ID,
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
            url: Common.Services.url.FLM,
            method: _FLMContract_PriceDI_PriceNormal.URL.GroupVehicle_SaveList,
            data: { data: dataSave, priceID: _FLMContract_PriceDI_PriceNormal.Params.PriceID },
            success: function (res) {
                $scope.InitView_GroupVehicle($scope.IsFrame);
                $rootScope.IsLoading = false;
                $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
            },
            error: function (res) {
                $rootScope.IsLoading = false;
            }
        });
    }

    $scope.GroupVehicle_CancelChange_Click = function ($event, grid) {
        $event.preventDefault();

        grid.cancelChanges();
    };
    //#endregion

    $scope.InitView_GroupProduct = function (isframe) {
        Common.Log("InitView_GroupProduct");

        Common.Services.Call($http, {
            url: Common.Services.url.FLM,
            method: _FLMContract_PriceDI_PriceNormal.URL.GroupProduct_Data,
            data: { priceID: _FLMContract_PriceDI_PriceNormal.Params.PriceID },
            success: function (res) {
                var Model = {
                    id: "RouteID",
                    fields: {
                        RoutingID: { type: 'number', nullable: true },
                        RoutingCode: { editable: false, type: 'string' },
                        RoutingName: { editable: false, type: 'string' },
                        SortOrder: { type: 'number', editable: false },
                    }
                }

                var GridColumn = [
                    { field: 'RoutingCode', title: 'Mã cung đường', width: 150, headerAttributes: { style: "vertical-align: middle;" }, filterable: { cell: { operator: 'contains', showOperators: false } } },
                    { field: 'RoutingName', title: 'Tên cung đường', width: 250, headerAttributes: { style: "vertical-align: middle;" }, filterable: { cell: { operator: 'contains', showOperators: false } } },
                    { field: 'SortOrder', title: 'Thứ tự CĐ', width: 100, headerAttributes: { style: "vertical-align: middle;" }, filterable: { cell: { operator: 'equal', showOperators: false } } }
                ]

                if (isframe) {//hợp đồng khung
                    Common.Data.Each(_FLMContract_PriceDI_PriceNormal.Data.ListGroupProduct, function (gop) {
                        var listCol = [];
                        var fieldMax = "L" + gop.ID + "MAX";
                        var fieldMin = "L" + gop.ID + "MIN";
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
                        GridColumn.push({ title: gop.Code, columns: listCol, headerAttributes: { style: "text-align: center;" }, })
                    })
                }
                else {//ko có khung chi hien 1 cot gia
                    Common.Data.Each(_FLMContract_PriceDI_PriceNormal.Data.ListGroupProduct, function (gov) {
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
                $scope.gop_grid.setOptions({
                    dataSource: Common.DataSource.Local({
                        data: [],
                        model: Model,
                        pageSize: 0,
                        sort: [{ field: "SortOrder", dir: "asc" }]
                    }),
                    height: '100%', pageable: false, sortable: true, columnMenu: false, resizable: true, reorderable: false, filterable: false, editable: { mode: 'incell' },
                    columns: GridColumn
                })

                //tao dataSource
                var dataCheck = {};
                Common.Data.Each(res, function (o) {
                    if (!Common.HasValue(dataCheck["R" + o.ContractRoutingID + "_L" + o.GroupOfProductID]))
                        dataCheck["R" + o.ContractRoutingID + "_L" + o.GroupOfProductID] = o;
                })
                var dataGrid = [];
                if (isframe) {
                    Common.Data.Each(_FLMContract_PriceDI_PriceNormal.Data.ListRouting, function (route) {
                        var item = {};
                        item["RoutingCode"] = route.Code;
                        item["RoutingName"] = route.RoutingName;
                        item["RoutingID"] = route.ID;
                        item["SortOrder"] = route.SortOrder;
                        Common.Data.Each(_FLMContract_PriceDI_PriceNormal.Data.ListGroupProduct, function (level) {
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
                    Common.Data.Each(_FLMContract_PriceDI_PriceNormal.Data.ListRouting, function (route) {
                        var item = {};
                        item["RoutingCode"] = route.Code;
                        item["RoutingName"] = route.RoutingName;
                        item["RoutingID"] = route.ID;
                        item["SortOrder"] = route.SortOrder;
                        Common.Data.Each(_FLMContract_PriceDI_PriceNormal.Data.ListGroupProduct, function (level) {
                            var field = "L" + level.ID;
                            item[field] = 0;
                            if (Common.HasValue(dataCheck["R" + route.ID + "_L" + level.ID])) {
                                item[field] = dataCheck["R" + route.ID + "_L" + level.ID].Price > 0 ? dataCheck["R" + route.ID + "_L" + level.ID].Price : 0;
                            }
                        });
                        dataGrid.push(item);
                    })
                }

                $scope.gop_grid.dataSource.data(dataGrid)
                $rootScope.IsLoading = false;
            }
        });
    }
    $scope.GroupProduct_SaveChanges_Click = function ($event, grid) {
        $event.preventDefault();
        var data = grid.dataSource.data();
        var dataSave = [];
        if ($scope.IsFrame) {
            Common.Data.Each(data, function (row) {
                Common.Data.Each(_FLMContract_PriceDI_PriceNormal.Data.ListGroupProduct, function (level) {
                    if ((Common.HasValue(row["L" + level.ID + "MAX"]) && row["L" + level.ID + "MAX"] >= 0) || (Common.HasValue(row["L" + level.ID + "MIN"]) && row["L" + level.ID + "MIN"] >= 0)) {
                        dataSave.push({
                            ContractRoutingID: row.RoutingID,
                            GroupOfProductID: level.ID,
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
                Common.Data.Each(_FLMContract_PriceDI_PriceNormal.Data.ListGroupProduct, function (level) {
                    if (Common.HasValue(row["L" + level.ID]) && row["L" + level.ID] >= 0) {
                        dataSave.push({
                            ContractRoutingID: row.RoutingID,
                            GroupOfProductID: level.ID,
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
            url: Common.Services.url.FLM,
            method: _FLMContract_PriceDI_PriceNormal.URL.GroupProduct_SaveList,
            data: { data: dataSave, priceID: _FLMContract_PriceDI_PriceNormal.Params.PriceID },
            success: function (res) {
                $scope.InitView_GroupProduct($scope.IsFrame);
                $rootScope.IsLoading = false;
                $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
            },
            error: function (res) {
                $rootScope.IsLoading = false;
            }
        });
    }
    $scope.GroupVehicle_CancelChange_Click = function ($event, grid) {
        $event.preventDefault();

        grid.cancelChanges();
    };
    $scope.GroupProduct_Excel_Click = function ($event, grid, win) {
        $event.preventDefault();

        $rootScope.UploadExcel({
            Path: Common.FolderUpload.Import,
            columns: [
                { field: 'RouteCode', width: 150, title: 'Mã cung đường', filterable: { cell: { showOperators: false, operator: "contains" } } },
                { field: 'RouteName', width: 250, title: 'Tên cung đường', filterable: { cell: { showOperators: false, operator: "contains" } } },
            ],
            Download: function () {
                $rootScope.IsLoading = true;
                Common.Services.Call($http, {
                    url: Common.Services.url.FLM,
                    method: _FLMContract_PriceDI_PriceNormal.URL.GroupProduct_Export,
                    data: { priceID: _FLMContract_PriceDI_PriceNormal.Params.PriceID, isFrame: $scope.IsFrame },
                    success: function (res) {
                        $rootScope.DownloadFile(res);
                        $rootScope.IsLoading = false;
                    },
                    error: function (e) {
                        $rootScope.IsLoading = false;
                    }
                })
            },
            Upload: function (e, callback) {
                $rootScope.IsLoading = true;
                Common.Services.Call($http, {
                    url: Common.Services.url.FLM,
                    method: _FLMContract_PriceDI_PriceNormal.URL.GroupProduct_Check,
                    data: { file: e.FilePath, priceID: _FLMContract_PriceDI_PriceNormal.Params.PriceID, contractTermID: _FLMContract_PriceDI_PriceNormal.Params.TermID, isFrame: $scope.IsFrame },
                    success: function (data) {
                        callback(data);
                        $rootScope.IsLoading = false;
                    },
                    error: function (e) {
                        $rootScope.IsLoading = false;
                    }
                })
            },
            Window: win,
            Complete: function (e, data) {
                if (!$scope.Item.TermClosed) {
                    $rootScope.IsLoading = true;
                    Common.Services.Call($http, {
                        url: Common.Services.url.FLM,
                        method: _FLMContract_PriceDI_PriceNormal.URL.GroupProduct_Import,
                        data: { data: data, priceID: _FLMContract_PriceDI_PriceNormal.Params.PriceID },
                        success: function (res) {
                            $rootScope.IsLoading = false;
                            $rootScope.Message({ Msg: "Đã cập nhật!" });

                            $scope.InitView_GroupProduct($scope.IsFrame);
                        },
                        error: function (e) {
                            $rootScope.IsLoading = false;
                        }
                    })
                } else {
                    $rootScope.Message({ Msg: 'Phụ lục đã đóng', NotifyType: Common.Message.NotifyType.ERROR });
                }
            }
        })
    }

    $scope.GroupVehicle_ExcelOn_Click = function ($event) {
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
                priceID: _FLMContract_PriceDI_PriceNormal.Params.PriceID,
                contractTermID: _FLMContract_PriceDI_PriceNormal.Params.TermID,
                isFrame: $scope.IsFrame
            },
            rowStart: 1,
            colCheckChange: 6,
            url: Common.Services.url.FLM,
            methodInit: _FLMContract_PriceDI_PriceNormal.URL.GroupVehicle_ExcelInit,
            methodChange: _FLMContract_PriceDI_PriceNormal.URL.GroupVehicle_ExcelChange,
            methodImport: _FLMContract_PriceDI_PriceNormal.URL.GroupVehicle_ExcelImport,
            methodApprove: _FLMContract_PriceDI_PriceNormal.URL.GroupVehicle_ExcelApprove,
            lstMessageError: lstMessageError,
            Changed: function () {

            },
            Approved: function () {
                $scope.InitView_GroupVehicle($scope.IsFrame);
                $rootScope.Message({ Msg: 'Đã lưu', NotifyType: Common.Message.NotifyType.ERROR });
            }
        });
    };
   

    $scope.GroupProduct_ExcelOn_Click = function ($event) {
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
                priceID: _FLMContract_PriceDI_PriceNormal.Params.PriceID,
                isFrame: $scope.IsFrame
            },
            rowStart: 1,
            colCheckChange: 6,
            url: Common.Services.url.FLM,
            methodInit: _FLMContract_PriceDI_PriceNormal.URL.ExcelInit,
            methodChange: _FLMContract_PriceDI_PriceNormal.URL.ExcelChange,
            methodImport: _FLMContract_PriceDI_PriceNormal.URL.ExcelImport,
            methodApprove: _FLMContract_PriceDI_PriceNormal.URL.ExcelApprove,
            lstMessageError: lstMessageError,
            Changed: function () {

            },
            Approved: function () {
                $scope.InitView_GroupProduct($scope.IsFrame);
                $rootScope.Message({ Msg: 'Đã lưu', NotifyType: Common.Message.NotifyType.ERROR });
            }
        });
    };
}]);