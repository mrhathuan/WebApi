/// <reference path="~/Scripts/jquery-1.9.1.intellisense.js" />
/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />
var _CUSContract_Material = {
    URL: {
        Data: 'CUSContract_MaterialChange_Data',
        Save: 'CUSContract_MaterialChange_Save',
    },
    Data: {
        PriceCurrent: null,
        PriceChange: null,
        PriceNoChange: null,
        ContractID: -1,
        customerID:-1,
    },
    Params: {
        //ContractMaterialID: -1,
        //StatusID: -1,
        TermID: -1,
    }
}

angular.module('myapp').controller('CUSContract_MaterialCtrl', ['$rootScope', '$scope', '$http', '$location', '$state', '$timeout', '$stateParams', function ($rootScope, $scope, $http, $location, $state, $timeout, $stateParams) {
    Common.Log('CUSContract_MaterialCtrl');
    $rootScope.IsLoading = false;

    _CUSContract_Material.Params = $.extend(true, _CUSContract_Material.Params, $state.params);
    $scope.TabIndex = 0;

    $scope.IsFrame = false;
    $scope.ItemPriceCurrent = null;
    $scope.ItemMaterial = null;
    $scope.ItemPriceChange = null;
    $scope.ItemPriceNoChange = null;


    $scope.contract_tabstripOptions = {
        animation: {
            open: { effects: "fadeIn" }
        },
        select: function (e) {
            $timeout(function () {
                $scope.TabIndex = angular.element(e.item).data('tabindex');
                Common.Log("Select_Tab_" + $scope.TabIndex);
            }, 1)
        }
    };

    Common.Services.Call($http, {
        url: Common.Services.url.CUS,
        method: _CUSContract_Material.URL.Data,
        data: {  contractTermID: _CUSContract_Material.Params.TermID },
        success: function (res) {
            $scope.ItemMaterial = res.TermInfo;
            _CUSContract_Material.Data.ContractID = res.TermInfo.ContractID
            //_CUSContract_Material.Data.customerID = res.CustomerID
            $scope.ItemPriceChange = res.PriceChange;
            $scope.ItemPriceNoChange = res.PriceNoChange;
            $scope.ItemPriceCurrent = res.PriceCurrent;

            $scope.IsFrame = res.PriceCurrent.ItemPrice.TypeOfContract == 1 ? false : true;

            if (res.PriceCurrent.ItemPrice.TypeOfMode == 1) {//fcl
                $scope.CreateFCL($scope.IsFrame);
            }
            else if (res.PriceCurrent.ItemPrice.TypeOfMode == 2) {//ftl
                if (res.PriceCurrent.ItemPrice.CheckPrice.HasLevel)
                    $scope.CreateFTLLevel($scope.IsFrame)
                if (res.PriceCurrent.ItemPrice.CheckPrice.HasNormal)
                    $scope.CreateFTLNormal($scope.IsFrame)
            }
            else if (res.PriceCurrent.ItemPrice.TypeOfMode == 3) {//ltl
                if (res.PriceCurrent.ItemPrice.CheckPrice.HasLevel)
                    $scope.CreateLTLLevel()
                if (res.PriceCurrent.ItemPrice.CheckPrice.HasNormal)
                    $scope.CreateLTLNormal($scope.IsFrame)
            }
        },
        error: function (res) {
            $rootScope.IsLoading = false;
        }
    });

    //#region ftl
    $scope.CreateFTLNormal = function (isframe) {
        Common.Log("CreateFTLNormal");


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
            Common.Data.Each($scope.ItemPriceCurrent.FTLNormal.ListGOV, function (gov) {
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
            Common.Data.Each($scope.ItemPriceCurrent.FTLNormal.ListGOV, function (gov) {
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

        $scope.FTL_Grid.setOptions({
            dataSource: Common.DataSource.Local({
                data: [],
                model: Model,
                sort: [{ field: "SortOrder", dir: "asc" }]
            }),
            height: '100%', pageable: false, sortable: true, columnMenu: false, resizable: true, reorderable: false, filterable: false, editable: false,
            columns: GridColumn
        })

        $scope.FTLChange_Grid.setOptions({
            dataSource: Common.DataSource.Local({
                data: [],
                model: Model,
                sort: [{ field: "SortOrder", dir: "asc" }]
            }),
            height: '100%', pageable: false, sortable: true, columnMenu: false, resizable: true, reorderable: false, filterable: false, editable: false,
            columns: GridColumn
        })

        $scope.FTLNoChange_Grid.setOptions({
            dataSource: Common.DataSource.Local({
                data: [],
                model: Model,
                sort: [{ field: "SortOrder", dir: "asc" }]
            }),
            height: '100%', pageable: false, sortable: true, columnMenu: false, resizable: true, reorderable: false, filterable: false, editable: false,
            columns: GridColumn
        })

        //tao data source
        //#region data source current
        var dataCheckCurrent = {};
        Common.Data.Each($scope.ItemPriceCurrent.FTLNormal.ListDetail, function (o) {
            if (!Common.HasValue(dataCheckCurrent["R" + o.RouteID + "_L" + o.GroupOfVehicleID]))
                dataCheckCurrent["R" + o.RouteID + "_L" + o.GroupOfVehicleID] = o;
        })
        var dataGridCurrent = [];
        if (isframe) {
            Common.Data.Each($scope.ItemPriceCurrent.ListRouting, function (route) {
                var item = {};
                item["RuoteCode"] = route.Code;
                item["RuoteName"] = route.RoutingName;
                item["RouteID"] = route.ID;
                item["SortOrder"] = route.SortOrder;
                Common.Data.Each($scope.ItemPriceCurrent.FTLNormal.ListGOV, function (level) {
                    var fieldMax = "L" + level.ID + "MAX";
                    var fieldMin = "L" + level.ID + "MIN";
                    item[fieldMax] = 0;
                    item[fieldMin] = 0;
                    if (Common.HasValue(dataCheckCurrent["R" + route.ID + "_L" + level.ID])) {
                        item[fieldMax] = dataCheckCurrent["R" + route.ID + "_L" + level.ID].PriceMax > 0 ? dataCheckCurrent["R" + route.ID + "_L" + level.ID].PriceMax : 0;
                        item[fieldMin] = dataCheckCurrent["R" + route.ID + "_L" + level.ID].PriceMin > 0 ? dataCheckCurrent["R" + route.ID + "_L" + level.ID].PriceMin : 0;
                    }
                });
                dataGridCurrent.push(item);
            })
        }
        else {
            Common.Data.Each($scope.ItemPriceCurrent.ListRouting, function (route) {
                var item = {};
                item["RuoteCode"] = route.Code;
                item["RuoteName"] = route.RoutingName;
                item["RouteID"] = route.ID;
                item["SortOrder"] = route.SortOrder;
                Common.Data.Each($scope.ItemPriceCurrent.FTLNormal.ListGOV, function (level) {
                    var field = "L" + level.ID;
                    item[field] = 0;
                    if (Common.HasValue(dataCheckCurrent["R" + route.ID + "_L" + level.ID])) {
                        item[field] = dataCheckCurrent["R" + route.ID + "_L" + level.ID].Price > 0 ? dataCheckCurrent["R" + route.ID + "_L" + level.ID].Price : 0;
                    }
                });
                dataGridCurrent.push(item);
            })
        }

        $scope.FTL_Grid.dataSource.data(dataGridCurrent)
        //#endregion

        //#region data source change
        var dataCheckChange = {};
        Common.Data.Each($scope.ItemPriceChange.FTLNormal.ListDetail, function (o) {
            if (!Common.HasValue(dataCheckChange["R" + o.RouteID + "_L" + o.GroupOfVehicleID]))
                dataCheckChange["R" + o.RouteID + "_L" + o.GroupOfVehicleID] = o;
        })
        var dataGridChange = [];
        if (isframe) {
            Common.Data.Each($scope.ItemPriceChange.ListRouting, function (route) {
                var item = {};
                item["RuoteCode"] = route.Code;
                item["RuoteName"] = route.RoutingName;
                item["RouteID"] = route.ID;
                item["SortOrder"] = route.SortOrder;
                Common.Data.Each($scope.ItemPriceChange.FTLNormal.ListGOV, function (level) {
                    var fieldMax = "L" + level.ID + "MAX";
                    var fieldMin = "L" + level.ID + "MIN";
                    item[fieldMax] = 0;
                    item[fieldMin] = 0;
                    if (Common.HasValue(dataCheckChange["R" + route.ID + "_L" + level.ID])) {
                        item[fieldMax] = dataCheckChange["R" + route.ID + "_L" + level.ID].PriceMax > 0 ? dataCheckChange["R" + route.ID + "_L" + level.ID].PriceMax : 0;
                        item[fieldMin] = dataCheckChange["R" + route.ID + "_L" + level.ID].PriceMin > 0 ? dataCheckChange["R" + route.ID + "_L" + level.ID].PriceMin : 0;
                    }
                });
                dataGridChange.push(item);
            })
        }
        else {
            Common.Data.Each($scope.ItemPriceChange.ListRouting, function (route) {
                var item = {};
                item["RuoteCode"] = route.Code;
                item["RuoteName"] = route.RoutingName;
                item["RouteID"] = route.ID;
                item["SortOrder"] = route.SortOrder;
                Common.Data.Each($scope.ItemPriceChange.FTLNormal.ListGOV, function (level) {
                    var field = "L" + level.ID;
                    item[field] = 0;
                    if (Common.HasValue(dataCheckChange["R" + route.ID + "_L" + level.ID])) {
                        item[field] = dataCheckChange["R" + route.ID + "_L" + level.ID].Price > 0 ? dataCheckChange["R" + route.ID + "_L" + level.ID].Price : 0;
                    }
                });
                dataGridChange.push(item);
            })
        }

        $scope.FTLChange_Grid.dataSource.data(dataGridChange)
        //#endregion
    }

    $scope.CreateFTLLevel = function (isframe) {
        Common.Log("CreateFTLLevel");
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
            Common.Data.Each($scope.ItemPriceCurrent.FTLLevel.ListLevel, function (level) {
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
            Common.Data.Each($scope.ItemPriceCurrent.FTLLevel.ListLevel, function (level) {
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

        $scope.FTL_Grid.setOptions({
            dataSource: Common.DataSource.Local({
                data: [],
                model: Model
            }),
            height: '100%', pageable: false, sortable: true, columnMenu: false, resizable: true, reorderable: false, filterable: false, editable: false,
            columns: GridColumn
        })
        $scope.FTLChange_Grid.setOptions({
            dataSource: Common.DataSource.Local({
                data: [],
                model: Model
            }),
            height: '100%', pageable: false, sortable: true, columnMenu: false, resizable: true, reorderable: false, filterable: false, editable: false,
            columns: GridColumn
        })
        $scope.FTLNoChange_Grid.setOptions({
            dataSource: Common.DataSource.Local({
                data: [],
                model: Model
            }),
            height: '100%', pageable: false, sortable: true, columnMenu: false, resizable: true, reorderable: false, filterable: false, editable: false,
            columns: GridColumn
        })
        //tao data source

        //#region Current
        var dataCheckCurrent = {};
        Common.Data.Each($scope.ItemPriceCurrent.FTLLevel.ListDetail, function (o) {
            if (!Common.HasValue(dataCheckCurrent["R" + o.RoutingID + "_L" + o.ContractLevelID]))
                dataCheckCurrent["R" + o.RoutingID + "_L" + o.ContractLevelID] = o;
        })
        var dataGridCurrent = [];
        if (isframe) {
            Common.Data.Each($scope.ItemPriceCurrent.ListRouting, function (route) {
                var item = {};
                item["RuoteCode"] = route.Code;
                item["RuoteName"] = route.RoutingName;
                item["SortOrder"] = route.SortOrder;
                item["RuoteID"] = route.ID;
                Common.Data.Each($scope.ItemPriceCurrent.FTLLevel.ListLevel, function (level) {
                    var fieldMax = "L" + level.ID + "MAX";
                    var fieldMin = "L" + level.ID + "MIN";
                    item[fieldMax] = 0;
                    item[fieldMin] = 0;
                    if (Common.HasValue(dataCheckCurrent["R" + route.ID + "_L" + level.ID])) {
                        item[fieldMax] = dataCheckCurrent["R" + route.ID + "_L" + level.ID].PriceMax > 0 ? dataCheckCurrent["R" + route.ID + "_L" + level.ID].PriceMax : 0;
                        item[fieldMin] = dataCheckCurrent["R" + route.ID + "_L" + level.ID].PriceMin > 0 ? dataCheckCurrent["R" + route.ID + "_L" + level.ID].PriceMin : 0;
                    }
                });
                dataGridCurrent.push(item);
            })
        }
        else {
            Common.Data.Each($scope.ItemPriceCurrent.ListRouting, function (route) {
                var item = {};
                item["RuoteCode"] = route.Code;
                item["RuoteName"] = route.RoutingName;
                item["SortOrder"] = route.SortOrder;
                item["RuoteID"] = route.ID;
                Common.Data.Each($scope.ItemPriceCurrent.FTLLevel.ListLevel, function (level) {
                    var field = "L" + level.ID;
                    item[field] = 0;
                    if (Common.HasValue(dataCheckCurrent["R" + route.ID + "_L" + level.ID])) {
                        item[field] = dataCheckCurrent["R" + route.ID + "_L" + level.ID].Price > 0 ? dataCheckCurrent["R" + route.ID + "_L" + level.ID].Price : 0;
                    }
                });
                dataGridCurrent.push(item);
            })
        }

        $scope.FTL_Grid.dataSource.data(dataGridCurrent)
        //#endregion

        //#region Change
        var dataCheckChange = {};
        Common.Data.Each($scope.ItemPriceChange.FTLLevel.ListDetail, function (o) {
            if (!Common.HasValue(dataCheckChange["R" + o.RoutingID + "_L" + o.LevelID]))
                dataCheckChange["R" + o.RoutingID + "_L" + o.LevelID] = o;
        })
        var dataGridChange = [];
        if (isframe) {
            Common.Data.Each($scope.ItemPriceChange.ListRouting, function (route) {
                var item = {};
                item["RuoteCode"] = route.Code;
                item["RuoteName"] = route.RoutingName;
                item["SortOrder"] = route.SortOrder;
                item["RuoteID"] = route.ID;
                Common.Data.Each($scope.ItemPriceChange.FTLLevel.ListLevel, function (level) {
                    var fieldMax = "L" + level.ID + "MAX";
                    var fieldMin = "L" + level.ID + "MIN";
                    item[fieldMax] = 0;
                    item[fieldMin] = 0;
                    if (Common.HasValue(dataCheckChange["R" + route.ID + "_L" + level.ID])) {
                        item[fieldMax] = dataCheckChange["R" + route.ID + "_L" + level.ID].PriceMax > 0 ? dataCheckChange["R" + route.ID + "_L" + level.ID].PriceMax : 0;
                        item[fieldMin] = dataCheckChange["R" + route.ID + "_L" + level.ID].PriceMin > 0 ? dataCheckChange["R" + route.ID + "_L" + level.ID].PriceMin : 0;
                    }
                });
                dataGridChange.push(item);
            })
        }
        else {
            Common.Data.Each($scope.ItemPriceChange.ListRouting, function (route) {
                var item = {};
                item["RuoteCode"] = route.Code;
                item["RuoteName"] = route.RoutingName;
                item["SortOrder"] = route.SortOrder;
                item["RuoteID"] = route.ID;
                Common.Data.Each($scope.ItemPriceChange.FTLLevel.ListLevel, function (level) {
                    var field = "L" + level.ID;
                    item[field] = 0;
                    if (Common.HasValue(dataCheckChange["R" + route.ID + "_L" + level.ID])) {
                        item[field] = dataCheckChange["R" + route.ID + "_L" + level.ID].Price > 0 ? dataCheckChange["R" + route.ID + "_L" + level.ID].Price : 0;
                    }
                });
                dataGridChange.push(item);
            })
        }

        $scope.FTLChange_Grid.dataSource.data(dataGridChange)
        //#endregion
    }
    //#endregion

    //#region LTL
    $scope.CreateLTLNormal = function (isframe) {
        Common.Log("CreateLTLNormal")
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
            Common.Data.Each($scope.ItemPriceCurrent.LTLNormal.ListGOP, function (gop) {
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
            Common.Data.Each($scope.ItemPriceCurrent.LTLNormal.ListGOP, function (gov) {
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

        $scope.LTL_Grid.setOptions({
            dataSource: Common.DataSource.Local({
                data: [], model: Model,
                sort: [{ field: "SortOrder", dir: "asc" }]
            }),
            columns: GridColumn,
            height: '99%', scrollable: true, pageable: false, filterable: { mode: 'row' }, editable: false, resizable: true,
            dataBound: function () {
                $rootScope.ExpandKGrid(this.element);
            }
        })
        $scope.LTLChange_Grid.setOptions({
            dataSource: Common.DataSource.Local({
                data: [], model: Model,
                sort: [{ field: "SortOrder", dir: "asc" }]
            }),
            columns: GridColumn,
            height: '99%', scrollable: true, pageable: false, filterable: { mode: 'row' }, editable: false, resizable: true,
            dataBound: function () {
                $rootScope.ExpandKGrid(this.element);
            }
        })
        $scope.LTLNoChange_Grid.setOptions({
            dataSource: Common.DataSource.Local({
                data: [], model: Model,
                sort: [{ field: "SortOrder", dir: "asc" }]
            }),
            columns: GridColumn,
            height: '99%', scrollable: true, pageable: false, filterable: { mode: 'row' }, editable: false, resizable: true,
            dataBound: function () {
                $rootScope.ExpandKGrid(this.element);
            }
        })

        //datasource

        //#region Current
        var dataGridCurrent = [];
        var dataCheckCurrent = {};
        Common.Data.Each($scope.ItemPriceCurrent.LTLNormal.ListDetail, function (o) {
            if (!Common.HasValue(dataCheckCurrent["R" + o.ContractRoutingID + "_L" + o.GroupOfProductID]))
                dataCheckCurrent["R" + o.ContractRoutingID + "_L" + o.GroupOfProductID] = o;
        })
        var dataGridCurrent = [];
        if (isframe) {
            Common.Data.Each($scope.ItemPriceCurrent.ListRouting, function (route) {
                var item = {};
                item["RoutingCode"] = route.Code;
                item["RoutingName"] = route.RoutingName;
                item["RoutingID"] = route.ID;
                item["SortOrder"] = route.SortOrder;
                Common.Data.Each($scope.ItemPriceCurrent.LTLNormal.ListGOP, function (level) {
                    var fieldMax = "L" + level.ID + "MAX";
                    var fieldMin = "L" + level.ID + "MIN";
                    item[fieldMax] = 0;
                    item[fieldMin] = 0;
                    if (Common.HasValue(dataCheckCurrent["R" + route.ID + "_L" + level.ID])) {
                        item[fieldMax] = dataCheckCurrent["R" + route.ID + "_L" + level.ID].PriceMax > 0 ? dataCheckCurrent["R" + route.ID + "_L" + level.ID].PriceMax : 0;
                        item[fieldMin] = dataCheckCurrent["R" + route.ID + "_L" + level.ID].PriceMin > 0 ? dataCheckCurrent["R" + route.ID + "_L" + level.ID].PriceMin : 0;
                    }
                });
                dataGridCurrent.push(item);
            })
        }
        else {
            Common.Data.Each($scope.ItemPriceCurrent.ListRouting, function (route) {
                var item = {};
                item["RoutingCode"] = route.Code;
                item["RoutingName"] = route.RoutingName;
                item["RoutingID"] = route.ID;
                item["SortOrder"] = route.SortOrder;
                Common.Data.Each($scope.ItemPriceCurrent.LTLNormal.ListGOP, function (level) {
                    var field = "L" + level.ID;
                    item[field] = 0;
                    if (Common.HasValue(dataCheckCurrent["R" + route.ID + "_L" + level.ID])) {
                        item[field] = dataCheckCurrent["R" + route.ID + "_L" + level.ID].Price > 0 ? dataCheckCurrent["R" + route.ID + "_L" + level.ID].Price : 0;
                    }
                });
                dataGridCurrent.push(item);
            })
        }
        $scope.LTL_Grid.dataSource.data(dataGridCurrent);
        //#endregion

        //#region Change
        var dataGridChange = [];
        var dataCheckChange = {};
        Common.Data.Each($scope.ItemPriceChange.LTLNormal.ListDetail, function (o) {
            if (!Common.HasValue(dataCheckChange["R" + o.ContractRoutingID + "_L" + o.GroupOfProductID]))
                dataCheckChange["R" + o.ContractRoutingID + "_L" + o.GroupOfProductID] = o;
        })
        var dataGridChange = [];
        if (isframe) {
            Common.Data.Each($scope.ItemPriceChange.ListRouting, function (route) {
                var item = {};
                item["RoutingCode"] = route.Code;
                item["RoutingName"] = route.RoutingName;
                item["RoutingID"] = route.ID;
                item["SortOrder"] = route.SortOrder;
                Common.Data.Each($scope.ItemPriceChange.LTLNormal.ListGOP, function (level) {
                    var fieldMax = "L" + level.ID + "MAX";
                    var fieldMin = "L" + level.ID + "MIN";
                    item[fieldMax] = 0;
                    item[fieldMin] = 0;
                    if (Common.HasValue(dataCheckChange["R" + route.ID + "_L" + level.ID])) {
                        item[fieldMax] = dataCheckChange["R" + route.ID + "_L" + level.ID].PriceMax > 0 ? dataCheckChange["R" + route.ID + "_L" + level.ID].PriceMax : 0;
                        item[fieldMin] = dataCheckChange["R" + route.ID + "_L" + level.ID].PriceMin > 0 ? dataCheckChange["R" + route.ID + "_L" + level.ID].PriceMin : 0;
                    }
                });
                dataGridChange.push(item);
            })
        }
        else {
            Common.Data.Each($scope.ItemPriceChange.ListRouting, function (route) {
                var item = {};
                item["RoutingCode"] = route.Code;
                item["RoutingName"] = route.RoutingName;
                item["RoutingID"] = route.ID;
                item["SortOrder"] = route.SortOrder;
                Common.Data.Each($scope.ItemPriceChange.LTLNormal.ListGOP, function (level) {
                    var field = "L" + level.ID;
                    item[field] = 0;
                    if (Common.HasValue(dataCheckChange["R" + route.ID + "_L" + level.ID])) {
                        item[field] = dataCheckChange["R" + route.ID + "_L" + level.ID].Price > 0 ? dataCheckChange["R" + route.ID + "_L" + level.ID].Price : 0;
                    }
                });
                dataGridChange.push(item);
            })
        }
        $scope.LTLChange_Grid.dataSource.data(dataGridChange);
        //#endregion

    }

    $scope.CreateLTLLevel = function () {
        Common.Log("CreateLTLLevel")
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
        Common.Data.Each($scope.ItemPriceCurrent.LTLLevel.ListLevel, function (level) {
            if ($scope.ItemPriceCurrent.ListGroupOfProduct.length > 0) {
                var listCol = [];
                Common.Data.Each($scope.ItemPriceCurrent.ListGroupOfProduct, function (pro) {
                    var field = "L" + level.ID + "_G" + pro.ID;
                    Model.fields[field] = { type: "number", editable: true };
                    _CUSContract_Price_DI_PriceLevel.Data.objModel[field] = { Level: level.ID, Pro: pro.ID };
                    _CUSContract_Price_DI_PriceLevel.Data.ListFieldLevel.push(field);
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

        $scope.LTL_Grid.setOptions({
            dataSource: Common.DataSource.Local({
                data: [],
                model: Model,
                sort: [{ field: "SortOrder", dir: "asc" }]
            }),
            height: '100%', pageable: false, sortable: true, columnMenu: false, resizable: true, reorderable: false, filterable: false, editable: false,
            columns: GridColumn
        })

        $scope.LTLChange_Grid.setOptions({
            dataSource: Common.DataSource.Local({
                data: [],
                model: Model,
                sort: [{ field: "SortOrder", dir: "asc" }]
            }),
            height: '100%', pageable: false, sortable: true, columnMenu: false, resizable: true, reorderable: false, filterable: false, editable: false,
            columns: GridColumn
        })

        $scope.LTLNoChange_Grid.setOptions({
            dataSource: Common.DataSource.Local({
                data: [],
                model: Model,
                sort: [{ field: "SortOrder", dir: "asc" }]
            }),
            height: '100%', pageable: false, sortable: true, columnMenu: false, resizable: true, reorderable: false, filterable: false, editable: false,
            columns: GridColumn
        })

        //tao data source

        //#region Current
        var dataCheckCurrent = {};
        Common.Data.Each($scope.ItemPriceCurrent.LTLLevel.ListDetail, function (o) {
            if (!Common.HasValue(dataCheckCurrent["R" + o.RoutingID + "_L" + o.LevelID + "_G" + o.GroupProductID]))
                dataCheckCurrent["R" + o.RoutingID + "_L" + o.LevelID + "_G" + o.GroupProductID] = o;
        })
        var dataGridCurrent = [];

        Common.Data.Each($scope.ItemPriceCurrent.ListRouting, function (route) {
            var item = {};
            item["RuoteCode"] = route.Code;
            item["RuoteName"] = route.RoutingName;
            item["RuoteID"] = route.ID;
            item["SortOrder"] = route.SortOrder;
            Common.Data.Each($scope.ItemPriceCurrent.LTLLevel.ListLevel, function (level) {
                Common.Data.Each($scope.ItemPriceCurrent.ListGroupOfProduct, function (pro) {
                    var field = "L" + level.ID + "_G" + pro.ID;
                    item[field] = 0;
                    if (Common.HasValue(dataCheckCurrent["R" + route.ID + "_L" + level.ID + "_G" + pro.ID]))
                        item[field] = dataCheckCurrent["R" + route.ID + "_L" + level.ID + "_G" + pro.ID].Price;
                })
            });
            dataGridCurrent.push(item);
        })
        $scope.LTL_Grid.dataSource.data(dataGridCurrent)
        //#endregion

        //#region Change
        var dataCheckChange = {};
        Common.Data.Each($scope.ItemPriceChange.LTLLevel.ListDetail, function (o) {
            if (!Common.HasValue(dataCheckChange["R" + o.RoutingID + "_L" + o.LevelID + "_G" + o.GroupProductID]))
                dataCheckChange["R" + o.RoutingID + "_L" + o.LevelID + "_G" + o.GroupProductID] = o;
        })
        var dataGridChange = [];

        Common.Data.Each($scope.ItemPriceChange.ListRouting, function (route) {
            var item = {};
            item["RuoteCode"] = route.Code;
            item["RuoteName"] = route.RoutingName;
            item["RuoteID"] = route.ID;
            item["SortOrder"] = route.SortOrder;
            Common.Data.Each($scope.ItemPriceChange.LTLLevel.ListLevel, function (level) {
                Common.Data.Each($scope.ItemPriceChange.ListGroupOfProduct, function (pro) {
                    var field = "L" + level.ID + "_G" + pro.ID;
                    item[field] = 0;
                    if (Common.HasValue(dataCheckChange["R" + route.ID + "_L" + level.ID + "_G" + pro.ID]))
                        item[field] = dataCheckChange["R" + route.ID + "_L" + level.ID + "_G" + pro.ID].Price;
                })
            });
            dataGridChange.push(item);
        })
        $scope.LTLChange_Grid.dataSource.data(dataGridChange)
        //#endregion

    }
    //#endregion

    //#region FCL
    $scope.CreateFCL = function (isframe) {
        Common.Log("CreateFCL")
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
            Common.Data.Each($scope.ItemPriceCurrent.FCLData.ListPacking, function (gov) {
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
            Common.Data.Each($scope.ItemPriceCurrent.FCLData.ListPacking, function (gov) {
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
        $scope.FCL_Grid.setOptions({
            dataSource: Common.DataSource.Local({
                data: [],
                model: Model,
                sort: [{ field: "SortOrder", dir: "asc" }]
            }),
            height: '100%', pageable: false, sortable: true, columnMenu: false, resizable: true, reorderable: false, filterable: false, editable: { mode: 'incell' },
            columns: GridColumn
        })
        $scope.FCLChange_Grid.setOptions({
            dataSource: Common.DataSource.Local({
                data: [],
                model: Model,
                sort: [{ field: "SortOrder", dir: "asc" }]
            }),
            height: '100%', pageable: false, sortable: true, columnMenu: false, resizable: true, reorderable: false, filterable: false, editable: { mode: 'incell' },
            columns: GridColumn
        })
        $scope.FCLNoChange_Grid.setOptions({
            dataSource: Common.DataSource.Local({
                data: [],
                model: Model,
                sort: [{ field: "SortOrder", dir: "asc" }]
            }),
            height: '100%', pageable: false, sortable: true, columnMenu: false, resizable: true, reorderable: false, filterable: false, editable: { mode: 'incell' },
            columns: GridColumn
        })

        //tao data source

        //#region Current
        var dataCheckCurrent = {};
        Common.Data.Each($scope.ItemPriceCurrent.FCLData.ListDetail, function (o) {
            if (!Common.HasValue(dataCheckCurrent["R" + o.ContractRoutingID + "_L" + o.PackingID]))
                dataCheckCurrent["R" + o.ContractRoutingID + "_L" + o.PackingID] = o;
        })
        var dataGridCurrent = [];
        if (isframe) {
            Common.Data.Each($scope.ItemPriceCurrent.ListRouting, function (route) {
                var item = {};
                item["RuoteCode"] = route.Code;
                item["RuoteName"] = route.RoutingName;
                item["RouteID"] = route.ID;
                item["SortOrder"] = route.SortOrder;
                Common.Data.Each($scope.ItemPriceCurrent.FCLData.ListPacking, function (level) {
                    var fieldMax = "L" + level.ID + "MAX";
                    var fieldMin = "L" + level.ID + "MIN";
                    item[fieldMax] = 0;
                    item[fieldMin] = 0;
                    if (Common.HasValue(dataCheckCurrent["R" + route.ID + "_L" + level.ID])) {
                        item[fieldMax] = dataCheckCurrent["R" + route.ID + "_L" + level.ID].PriceMax > 0 ? dataCheckCurrent["R" + route.ID + "_L" + level.ID].PriceMax : 0;
                        item[fieldMin] = dataCheckCurrent["R" + route.ID + "_L" + level.ID].PriceMin > 0 ? dataCheckCurrent["R" + route.ID + "_L" + level.ID].PriceMin : 0;
                    }
                });
                dataGridCurrent.push(item);
            })
        }
        else {
            Common.Data.Each($scope.ItemPriceCurrent.ListRouting, function (route) {
                var item = {};
                item["RuoteCode"] = route.Code;
                item["RuoteName"] = route.RoutingName;
                item["RouteID"] = route.ID;
                item["SortOrder"] = route.SortOrder;
                Common.Data.Each($scope.ItemPriceCurrent.FCLData.ListPacking, function (level) {
                    var field = "L" + level.ID;
                    item[field] = 0;
                    if (Common.HasValue(dataCheckCurrent["R" + route.ID + "_L" + level.ID])) {
                        item[field] = dataCheckCurrent["R" + route.ID + "_L" + level.ID].Price > 0 ? dataCheckCurrent["R" + route.ID + "_L" + level.ID].Price : 0;
                    }
                });
                dataGridCurrent.push(item);
            })
        }

        $scope.FCL_Grid.dataSource.data(dataGridCurrent)
        //#endregion

        //#region Change
        var dataCheckChange = {};
        Common.Data.Each($scope.ItemPriceChange.FCLData.ListDetail, function (o) {
            if (!Common.HasValue(dataCheckChange["R" + o.ContractRoutingID + "_L" + o.PackingID]))
                dataCheckChange["R" + o.ContractRoutingID + "_L" + o.PackingID] = o;
        })
        var dataGridChange = [];
        if (isframe) {
            Common.Data.Each($scope.ItemPriceChange.ListRouting, function (route) {
                var item = {};
                item["RuoteCode"] = route.Code;
                item["RuoteName"] = route.RoutingName;
                item["RouteID"] = route.ID;
                item["SortOrder"] = route.SortOrder;
                Common.Data.Each($scope.ItemPriceChange.FCLData.ListPacking, function (level) {
                    var fieldMax = "L" + level.ID + "MAX";
                    var fieldMin = "L" + level.ID + "MIN";
                    item[fieldMax] = 0;
                    item[fieldMin] = 0;
                    if (Common.HasValue(dataCheckChange["R" + route.ID + "_L" + level.ID])) {
                        item[fieldMax] = dataCheckChange["R" + route.ID + "_L" + level.ID].PriceMax > 0 ? dataCheckChange["R" + route.ID + "_L" + level.ID].PriceMax : 0;
                        item[fieldMin] = dataCheckChange["R" + route.ID + "_L" + level.ID].PriceMin > 0 ? dataCheckChange["R" + route.ID + "_L" + level.ID].PriceMin : 0;
                    }
                });
                dataGridChange.push(item);
            })
        }
        else {
            Common.Data.Each($scope.ItemPriceChange.ListRouting, function (route) {
                var item = {};
                item["RuoteCode"] = route.Code;
                item["RuoteName"] = route.RoutingName;
                item["RouteID"] = route.ID;
                item["SortOrder"] = route.SortOrder;
                Common.Data.Each($scope.ItemPriceChange.FCLData.ListPacking, function (level) {
                    var field = "L" + level.ID;
                    item[field] = 0;
                    if (Common.HasValue(dataCheckChange["R" + route.ID + "_L" + level.ID])) {
                        item[field] = dataCheckChange["R" + route.ID + "_L" + level.ID].Price > 0 ? dataCheckChange["R" + route.ID + "_L" + level.ID].Price : 0;
                    }
                });
                dataGridChange.push(item);
            })
        }

        $scope.FCLChange_Grid.dataSource.data(dataGridChange)
        //#endregion

    }
    //#endregion


    $scope.Save_Click = function ($event) {
        $event.preventDefault();
        Common.Log("Save_Click")
        if (!Common.HasValue($scope.ItemPriceChange.ItemPrice.Code) || $scope.ItemPriceChange.ItemPrice.Code == '') {
            $rootScope.Message({ Msg: 'Mã bảng giá không thể trống', NotifyType: Common.Message.NotifyType.ERROR });
        } else if ($scope.ItemPriceChange.ItemPrice.Code.length > 50 ) {
            $rootScope.Message({ Msg: 'Mã bảng giá phải nhỏ hơn 50 kí tự', NotifyType: Common.Message.NotifyType.ERROR });
        }
        else if (!Common.HasValue($scope.ItemPriceChange.ItemPrice.Name) || $scope.ItemPriceChange.ItemPrice.Name == '') {
            $rootScope.Message({ Msg: 'Tên bảng giá không thể trống', NotifyType: Common.Message.NotifyType.ERROR });
        } else if ($scope.ItemPriceChange.ItemPrice.Name.length > 500) {
            $rootScope.Message({ Msg: 'Tên bảng giá phải nhỏ hơn 500 kí tự', NotifyType: Common.Message.NotifyType.ERROR });
        }
        else if (!Common.HasValue($scope.ItemPriceChange.ItemPrice.EffectDate)) {
            $rootScope.Message({ Msg: 'Ngày hiệu lực bảng giá không chính xác', NotifyType: Common.Message.NotifyType.ERROR });
        }
        else {
            $rootScope.Message({
                Type: Common.Message.Type.Confirm,
                NotifyType: Common.Message.NotifyType.SUCCESS,
                Title: 'Thông báo',
                Msg: 'Bạn đồng ý với giá thay đổi hiện tại?',
                Close: null,
                Ok: function () {
                    var item = {
                        PriceChange: $scope.ItemPriceChange,
                        PriceCurrent: $scope.ItemPriceCurrent,
                        PriceMaterial: $scope.ItemMaterial,
                        PriceNoChange: $scope.ItemPriceNoChange,
                    };
                    $rootScope.IsLoading = true;
                    Common.Services.Call($http, {
                        url: Common.Services.url.CUS,
                        method: _CUSContract_Material.URL.Save,
                        data: { item: item, contractMaterialID: $scope.ItemMaterial.ID },
                        success: function (res) {
                            $rootScope.IsLoading = false;
                            $state.go("main.CUSCustomer.Contract", { ID: $scope.ItemMaterial.ContractID, CustomerID: 0 })
                        },
                        error: function (res) {
                            $rootScope.IsLoading = false;
                        }
                    });

                }
            })
        }
    }

    //$scope.IsShowBackClick = false;
    //$scope.loadButtonBack = function () {
    //    if (_CUSContract_Material.Params.StatusID > 0) {
    //        $scope.IsShowBackClick = true;
    //    }
    //    else {
    //        $scope.IsShowBackClick = false;
    //    }
    //};
    //$scope.loadButtonBack();

    $scope.Back_Click = function ($event) {
        $event.preventDefault();

        //if (_CUSContract_Material.Params.StatusID == 1) {
        //    $state.go("main.CUSContract.Detail", { ID: _CUSContract_Material.Data.ContractID })
        //}
        //else if (_CUSContract_Material.Params.StatusID == 2) {
        $state.go("main.CUSCustomer.Contract", { ID: $scope.ItemMaterial.ContractID, CustomerID: 0 })
    };

}]);