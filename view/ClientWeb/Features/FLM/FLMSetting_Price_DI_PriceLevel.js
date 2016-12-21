/// <reference path="~/Scripts/jquery-1.9.1.intellisense.js" />
/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />

var _FLMContract_Price_DI_PriceLevel = {
    URL: {
        Get: 'FLMContract_Price_Get',

        Data: 'FLMContract_Price_Data',


        GroupVehicleLV_Save: 'FLMPrice_DI_PriceGVLevel_Save',
        GroupVehicle_DetailData: 'FLMPrice_DI_PriceGVLevel_DetailData',
        GroupVehicle_ExcelExport: 'FLMPrice_DI_PriceGVLevel_ExcelExport',
        GroupVehicle_ExcelCheck: 'FLMPrice_DI_PriceGVLevel_ExcelCheck',
        GroupVehicle_ExcelImport: 'FLMPrice_DI_PriceGVLevel_ExcelImport',

        GOV_ExcelInit: 'FLMPrice_DI_PriceGVLevel_ExcelInit',
        GOV_ExcelChange: 'FLMPrice_DI_PriceGVLevel_ExcelChange',
        GOV_ExcelImport: 'FLMPrice_DI_PriceGVLevel_ExcelOnImport',
        GOV_ExcelApprove: 'FLMPrice_DI_PriceGVLevel_ExcelApprove',

        PriceLevel_Save: 'FLMPrice_DI_PriceLevel_Save',
        PriceLevel_DetailData: 'FLMPrice_DI_PriceLevel_DetailData',
        PriceLevel_ExcelExport: 'FLMPrice_DI_PriceLevel_ExcelExport',
        PriceLevel_ExcelImport: 'FLMPrice_DI_PriceLevel_ExcelImport',
        PriceLevel_ExcelCheck: 'FLMPrice_DI_PriceLevel_ExcelCheck',

        ExcelInit: 'FLMPrice_DI_PriceLevel_ExcelInit',
        ExcelChange: 'FLMPrice_DI_PriceLevel_ExcelChange',
        ExcelImport: 'FLMPrice_DI_PriceLevel_OnExcelImport',
        ExcelApprove: 'FLMPrice_DI_PriceLevel_ExcelApprove',
    },
    Data: {
        ListPriceOfGOP: null,
        ListGroupVehicle: null,
        ListGroupProduct: null,
        ListLevel: [],
        ListRouting: [],
    },
    Params: {
        PriceID: -1,
        TermID: -1,
        //CustomerID: -1,
        //ContractID: -1
    }
}

angular.module('myapp').controller('FLMSetting_Price_DI_PriceLevelCtrl', ['$rootScope', '$scope', '$http', '$location', '$state', '$timeout', '$stateParams', function ($rootScope, $scope, $http, $location, $state, $timeout, $stateParams) {
    Common.Log('FLMSetting_Price_DI_PriceLevelCtrl');
    $rootScope.IsLoading = false;

    $scope.Item = null;
    _FLMContract_Price_DI_PriceLevel.Params = $.extend(true, _FLMContract_Price_DI_PriceLevel.Params, $state.params);
    $scope.IsFrame = false;


    Common.Services.Call($http, {
        url: Common.Services.url.FLM,
        method: _FLMContract_Price_DI_PriceLevel.URL.Data,
        data: { contractTermID: _FLMContract_Price_DI_PriceLevel.Params.TermID },
        success: function (res) {
            _FLMContract_Price_DI_PriceLevel.Data.ListGroupVehicle = res.ListGroupOfVehicle;
            _FLMContract_Price_DI_PriceLevel.Data.ListGroupProduct = res.ListGroupOfProduct;
            _FLMContract_Price_DI_PriceLevel.Data.ListRouting = res.ListRouting;
            _FLMContract_Price_DI_PriceLevel.Data.ListLevel = res.ListLevel;

            Common.Services.Call($http, {
                url: Common.Services.url.FLM,
                method: _FLMContract_Price_DI_PriceLevel.URL.Get,
                data: { priceID: _FLMContract_Price_DI_PriceLevel.Params.PriceID },
                success: function (res) {
                    $scope.Item = res;
                    if ($scope.Item.ID > 0) {
                        $timeout(function () {

                            var isframe = $scope.Item.TypeOfContract == 1 ? false : true;
                            $scope.IsFrame = $scope.Item.TypeOfContract == 1 ? false : true;
                            if ($scope.Item.TypeOfMode == 2) {
                                $scope.InitView_GroupVehicleLV(isframe);
                            } else if ($scope.Item.TypeOfMode == 3) {
                                $scope.InitView_PriceLevel();
                            }


                        }, 1)
                    }
                }
            })
        }
    });

    $scope.Back_Click = function ($event) {
        $event.preventDefault();
        $state.go("main.FLMSetting.Term", { TermID: _FLMContract_Price_DI_PriceLevel.Params.TermID, ContractID: $scope.Item.ContractID })
    }

    //#region price ltl level
    $scope.InitView_PriceLevel = function () {
        Common.Services.Call($http, {
            url: Common.Services.url.FLM,
            method: _FLMContract_Price_DI_PriceLevel.URL.PriceLevel_DetailData,
            data: { priceID: _FLMContract_Price_DI_PriceLevel.Params.PriceID },
            success: function (res) {

                var Model = {
                    id: 'RuoteID',
                    fields: {
                        RuoteCode: { type: 'string', editable: false },
                        RuoteName: { type: 'string', editable: false },
                        RuoteID: { type: 'number', editable: false },
                        SortOrder: { type: 'number', editable: false }
                    }
                }
                var GridColumn = [
                    { field: 'RuoteCode', title: "Mã cung đường", width: 120, locked: true, filterable: { cell: { operator: 'contains', showOperators: false } } },
                    { field: 'RuoteName', title: "Tên cung đường", width: 150, locked: true, filterable: { cell: { operator: 'contains', showOperators: false } } },
                    { field: 'SortOrder', title: "Thứ tự CĐ", width: 100, locked: true, filterable: { cell: { operator: 'equal', showOperators: false } } }
                ]
                Common.Data.Each(_FLMContract_Price_DI_PriceLevel.Data.ListLevel, function (level) {
                    if (_FLMContract_Price_DI_PriceLevel.Data.ListGroupProduct.length > 0) {
                        var listCol = [];
                        Common.Data.Each(_FLMContract_Price_DI_PriceLevel.Data.ListGroupProduct, function (pro) {
                            var field = "L" + level.ID + "_G" + pro.ID;
                            Model.fields[field] = { type: "number", editable: true };
                            listCol.push({
                                field: field, title: pro.Code, width: 120, locked: false,
                                template: '#=' + field + '==null?"": Common.Number.ToMoney(' + field + ')#',
                                editor: function (container, options) {
                                    var input = $('<input name="' + options.field + '"  />');
                                    input.appendTo(container);
                                    input.kendoNumericTextBox({
                                        format: 'n0', spinners: false, culture: 'en-US', min: 0, step: 1, decimals: 0
                                    });
                                }
                            })
                        })
                        GridColumn.push({ title: level.Code, columns: listCol, headerAttributes: { style: "text-align: center;" }, })
                    }
                })
                GridColumn.push({ title: ' ', filterable: false, sortable: false })
                $scope.IsPriceLevelEdit = false;

                $scope.price_level_grid.setOptions({
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
                    if (!Common.HasValue(dataCheck["R" + o.RoutingID + "_L" + o.LevelID + "_G" + o.GroupProductID]))
                        dataCheck["R" + o.RoutingID + "_L" + o.LevelID + "_G" + o.GroupProductID] = o;
                })
                var dataGrid = [];

                Common.Data.Each(_FLMContract_Price_DI_PriceLevel.Data.ListRouting, function (route) {
                    var item = {};
                    item["RuoteCode"] = route.Code;
                    item["RuoteName"] = route.RoutingName;
                    item["RuoteID"] = route.ID;
                    item["SortOrder"] = route.SortOrder;
                    Common.Data.Each(_FLMContract_Price_DI_PriceLevel.Data.ListLevel, function (level) {
                        Common.Data.Each(_FLMContract_Price_DI_PriceLevel.Data.ListGroupProduct, function (pro) {
                            var field = "L" + level.ID + "_G" + pro.ID;
                            item[field] = 0;
                            if (Common.HasValue(dataCheck["R" + route.ID + "_L" + level.ID + "_G" + pro.ID]))
                                item[field] = dataCheck["R" + route.ID + "_L" + level.ID + "_G" + pro.ID].Price;
                        })
                    });
                    dataGrid.push(item);
                })
                $scope.price_level_grid.dataSource.data(dataGrid)
                $rootScope.IsLoading = false;
            }
        })
    }

    $scope.GOPLevel_Save_Click = function ($event, grid) {
        $event.preventDefault();
        var data = grid.dataSource.data();
        var dataSave = [];

        Common.Data.Each(data, function (row) {
            Common.Data.Each(_FLMContract_Price_DI_PriceLevel.Data.ListLevel, function (level) {
                Common.Data.Each(_FLMContract_Price_DI_PriceLevel.Data.ListGroupProduct, function (pro) {
                    debugger
                    if (row["L" + level.ID + "_G" + pro.ID] >= 0) {
                        dataSave.push({
                            RoutingID: row.RuoteID,
                            LevelID: level.ID,
                            GroupProductID: pro.ID,
                            Price: row["L" + level.ID + "_G" + pro.ID],
                        })
                    }
                })

            })
        })


        $rootScope.IsLoading = true;
        Common.Services.Call($http, {
            url: Common.Services.url.FLM,
            method: _FLMContract_Price_DI_PriceLevel.URL.PriceLevel_Save,
            data: { lst: dataSave, priceID: _FLMContract_Price_DI_PriceLevel.Params.PriceID },
            success: function (res) {
                $scope.InitView_PriceLevel();
                $rootScope.IsLoading = false;
                $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
            },
            error: function (res) {
                $rootScope.IsLoading = false;
            }
        });
    }

    $scope.GOPLevel_Cancel_Click = function ($event, grid) {
        $event.preventDefault();
        grid.cancelChanges();
    }

    $scope.PriceLevel_Excel_Click = function ($event, win) {
        $event.preventDefault();
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
                    method: _FLMContract_Price_DI_PriceLevel.URL.PriceLevel_ExcelExport,
                    data: { priceID: _FLMContract_Price_DI_PriceLevel.Params.PriceID, contractTermID: _FLMContract_Price_DI_PriceLevel.Params.TermID },
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
                    method: _FLMContract_Price_DI_PriceLevel.URL.PriceLevel_ExcelCheck,
                    data: { file: e, priceID: _FLMContract_Price_DI_PriceLevel.Params.PriceID, contractTermID: _FLMContract_Price_DI_PriceLevel.Params.TermID },
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
                        method: _FLMContract_Price_DI_PriceLevel.URL.PriceLevel_ExcelImport,
                        data: { lst: data, priceID: _FLMContract_Price_DI_PriceLevel.Params.PriceID },
                        success: function (res) {
                            $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                            $scope.InitView_PriceLevel()
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
    //#endregion

    //#region van chuyen FTL level
    $scope.InitView_GroupVehicleLV = function (isframe) {
        Common.Services.Call($http, {
            url: Common.Services.url.FLM,
            method: _FLMContract_Price_DI_PriceLevel.URL.GroupVehicle_DetailData,
            data: { id: _FLMContract_Price_DI_PriceLevel.Params.PriceID },
            success: function (res) {

                var Model = {
                    id: 'RuoteID',
                    fields: {
                        RuoteCode: { type: 'string', editable: false },
                        RuoteName: { type: 'string', editable: false },
                        RuoteID: { type: 'number', editable: false },
                        SortOrder: { type: 'number', editable: false }
                    }
                }
                var GridColumn = [
                    { field: 'RuoteCode', title: "Mã cung đường", width: 120, locked: true, filterable: { cell: { operator: 'contains', showOperators: false } } },
                    { field: 'RuoteName', title: "Tên cung đường", width: 150, locked: true, filterable: { cell: { operator: 'contains', showOperators: false } } },
                    { field: 'SortOrder', title: "Thứ tự CĐ", width: 100, locked: true, filterable: { cell: { operator: 'equal', showOperators: false } } }
                ]
                if (isframe) {//hợp đồng khung
                    Common.Data.Each(_FLMContract_Price_DI_PriceLevel.Data.ListLevel, function (level) {
                        var listCol = [];
                        var fieldMax = "L" + level.ID + "MAX";
                        var fieldMin = "L" + level.ID + "MIN";
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
                        GridColumn.push({ title: level.Code, columns: listCol, headerAttributes: { style: "text-align: center;" }, })
                    })
                }
                else {//ko có khung chi hien 1 cot gia
                    Common.Data.Each(_FLMContract_Price_DI_PriceLevel.Data.ListLevel, function (level) {
                        var listCol = [];
                        var field = "L" + level.ID;
                        Model.fields[field] = { type: "number", editable: true };
                        GridColumn.push({
                            field: field, title: level.Code, width: 120, locked: false,
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
                $scope.IsPriceLevelEdit = false;

                $scope.govlv_grid.setOptions({
                    dataSource: Common.DataSource.Local({
                        data: [],
                        model: Model,
                        pageSize: 0,
                    }),
                    height: '100%', pageable: false, sortable: true, columnMenu: false, resizable: true, reorderable: false, filterable: false, editable: { mode: 'incell' },
                    columns: GridColumn
                })

                //tao data source

                var dataCheck = {};
                Common.Data.Each(res, function (o) {
                    if (!Common.HasValue(dataCheck["R" + o.RoutingID + "_L" + o.ContractLevelID]))
                        dataCheck["R" + o.RoutingID + "_L" + o.ContractLevelID] = o;
                })
                var dataGrid = [];
                if (isframe) {
                    Common.Data.Each(_FLMContract_Price_DI_PriceLevel.Data.ListRouting, function (route) {
                        var item = {};
                        item["RuoteCode"] = route.Code;
                        item["RuoteName"] = route.RoutingName;
                        item["SortOrder"] = route.SortOrder;
                        item["RuoteID"] = route.ID;
                        Common.Data.Each(_FLMContract_Price_DI_PriceLevel.Data.ListLevel, function (level) {
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
                    Common.Data.Each(_FLMContract_Price_DI_PriceLevel.Data.ListRouting, function (route) {
                        var item = {};
                        item["RuoteCode"] = route.Code;
                        item["RuoteName"] = route.RoutingName;
                        item["SortOrder"] = route.SortOrder;
                        item["RuoteID"] = route.ID;
                        Common.Data.Each(_FLMContract_Price_DI_PriceLevel.Data.ListLevel, function (level) {
                            var field = "L" + level.ID;
                            item[field] = 0;
                            if (Common.HasValue(dataCheck["R" + route.ID + "_L" + level.ID])) {
                                item[field] = dataCheck["R" + route.ID + "_L" + level.ID].Price > 0 ? dataCheck["R" + route.ID + "_L" + level.ID].Price : 0;
                            }
                        });
                        dataGrid.push(item);
                    })
                }

                $scope.govlv_grid.dataSource.data(dataGrid)

            }
        })
    }

    $scope.GOVLevel_Save_Click = function ($event, grid) {
        $event.preventDefault();
        var data = grid.dataSource.data();
        var dataSave = [];
        if ($scope.IsFrame) {
            Common.Data.Each(data, function (row) {
                Common.Data.Each(_FLMContract_Price_DI_PriceLevel.Data.ListLevel, function (level) {
                    if (row["L" + level.ID + "MAX"] > 0 || row["L" + level.ID + "MIN"] >= 0) {
                        dataSave.push({
                            RoutingID: row.RuoteID,
                            ContractLevelID: level.ID,
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
                Common.Data.Each(_FLMContract_Price_DI_PriceLevel.Data.ListLevel, function (level) {

                    if (row["L" + level.ID] >= 0) {
                        dataSave.push({
                            RoutingID: row.RuoteID,
                            ContractLevelID: level.ID,
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
            method: _FLMContract_Price_DI_PriceLevel.URL.GroupVehicleLV_Save,
            data: { lst: dataSave, priceID: _FLMContract_Price_DI_PriceLevel.Params.PriceID },
            success: function (res) {
                $scope.InitView_GroupVehicleLV($scope.IsFrame);
                $rootScope.IsLoading = false;
                $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
            },
            error: function (res) {
                $rootScope.IsLoading = false;
            }
        });
    }

    $scope.GOVLevel_Cancel_Click = function ($event, grid) {
        $event.preventDefault();
        grid.cancelChanges();
    }
    $scope.GOV_Excel_Click = function ($event, win) {
        $event.preventDefault();
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
                    method: _FLMContract_Price_DI_PriceLevel.URL.GroupVehicle_ExcelExport,
                    data: { priceID: _FLMContract_Price_DI_PriceLevel.Params.PriceID, contractTermID: _FLMContract_Price_DI_PriceLevel.Params.TermID, isFrame: $scope.IsFrame },
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
                    method: _FLMContract_Price_DI_PriceLevel.URL.GroupVehicle_ExcelCheck,
                    data: { file: e, priceID: _FLMContract_Price_DI_PriceLevel.Params.PriceID, contractTermID: _FLMContract_Price_DI_PriceLevel.Params.TermID, isFrame: $scope.IsFrame },
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
                        method: _FLMContract_Price_DI_PriceLevel.URL.GroupVehicle_ExcelImport,
                        data: { lst: data, priceID: _FLMContract_Price_DI_PriceLevel.Params.PriceID },
                        success: function (res) {
                            $rootScope.IsLoading = false;
                            $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                            $scope.InitView_GroupVehicleLV($scope.IsFrame)
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

    //#endregion

    //#endregion

    $scope.GOV_ExcelOnline_Click = function ($event) {
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
            functionkey: 'CUSContract_Price_DI_PriceLevel',
            params: {
                priceID: _FLMContract_Price_DI_PriceLevel.Params.PriceID,
                contractTermID: _FLMContract_Price_DI_PriceLevel.Params.TermID,
                isFrame: $scope.IsFrame
            },
            rowStart: 1,
            colCheckChange: 6,
            url: Common.Services.url.FLM,
            methodInit: _FLMContract_Price_DI_PriceLevel.URL.GOV_ExcelInit,
            methodChange: _FLMContract_Price_DI_PriceLevel.URL.GOV_ExcelChange,
            methodImport: _FLMContract_Price_DI_PriceLevel.URL.GOV_ExcelImport,
            methodApprove: _FLMContract_Price_DI_PriceLevel.URL.GOV_ExcelApprove,
            lstMessageError: lstMessageError,
            Changed: function () {

            },
            Approved: function () {
                $scope.InitView_GroupVehicleLV($scope.isframe);
                $rootScope.Message({ Msg: 'Đã lưu', NotifyType: Common.Message.NotifyType.ERROR });
            }
        });
    };

    $scope.PriceLevel_ExcelOnline_Click = function ($event) {
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
                '[Giá] không được trống và là số',
            ];
        }

        $rootScope.excelShare.Init({
            functionkey: 'CUSContract_Price_DI_PriceLevel',
            params: {
                priceID: _FLMContract_Price_DI_PriceLevel.Params.PriceID,
                contractTermID: _FLMContract_Price_DI_PriceLevel.Params.TermID
            },
            rowStart: 2,
            colCheckChange: 6,
            url: Common.Services.url.FLM,
            methodInit: _FLMContract_Price_DI_PriceLevel.URL.ExcelInit,
            methodChange: _FLMContract_Price_DI_PriceLevel.URL.ExcelChange,
            methodImport: _FLMContract_Price_DI_PriceLevel.URL.ExcelImport,
            methodApprove: _FLMContract_Price_DI_PriceLevel.URL.ExcelApprove,
            lstMessageError: lstMessageError,
            Changed: function () {

            },
            Approved: function () {
                $scope.InitView_PriceLevel();
                $rootScope.Message({ Msg: 'Đã lưu', NotifyType: Common.Message.NotifyType.ERROR });
            }
        });
    };
}]);