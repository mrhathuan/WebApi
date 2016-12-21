﻿/// <reference path="~/Scripts/jquery-1.9.1.intellisense.js" />
/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />

var _CUSContract_Price_DI_LoadTypeOfPartner = {
    URL: {
        Get: 'CUSContract_Price_Get',
        Data: 'CUSContract_Price_Data',


        Loading_List: 'CUSPrice_DI_LoadPartner_List',
        Loading_Delete: 'CUSPrice_DI_Load_Delete',
        Loading_SaveList: 'CUSPrice_DI_LoadPartner_SaveList',
        Loading_DeleteList: 'CUSPrice_DI_Load_DeleteList',
        Loading_Location_NotIn_List: 'CUSPrice_DI_LoadPartner_PartnerNotIn_List',
        Loading_Location_NotIn_SaveList: 'CUSPrice_DI_LoadPartner_PartnerNotIn_SaveList',

        Excel_Export: "CUSPrice_DI_LoadPartner_ExcelExport",
        Excel_Check: "CUSPrice_DI_LoadPartner_ExcelCheck",
        Excel_Save: "CUSPrice_DI_LoadPartner_Import",

    },
    Data: {
        ListPriceOfGOP: null,
        ListGroupProduct: null,
    },
    Params: {
        PriceID: -1,
        TermID: -1,
    }
}

angular.module('myapp').controller('CUSContract_Price_DI_LoadTypeOfPartnerCtrl', ['$rootScope', '$scope', '$http', '$location', '$state', '$timeout', '$stateParams', function ($rootScope, $scope, $http, $location, $state, $timeout, $stateParams) {
    Common.Log('CUSContract_Price_DI_LoadTypeOfPartnerCtrl');
    $rootScope.IsLoading = false;

    $scope.Item = null;
    _CUSContract_Price_DI_LoadTypeOfPartner.Params = $.extend({}, true, $state.params);

    $scope.IsFrame = false;


    $scope.Excel_Click = function ($event, win) {
        $event.preventDefault();
        $rootScope.UploadExcel({
            Path: Common.FolderUpload.Import,
            columns: [
                { field: 'GroupOfLocationCode', width: 150, title: 'Mã loại địa điểm', filterable: { cell: { showOperators: false, operator: "contains" } } },
                { field: 'GroupOfLocationName', width: 250, title: 'Tên loại địa điểm', filterable: { cell: { showOperators: false, operator: "contains" } } }
            ],
            Download: function () {
                $rootScope.IsLoading = true;
                Common.Services.Call($http, {
                    url: Common.Services.url.CUS,
                    method: _CUSContract_Price_DI_LoadTypeOfPartner.URL.Excel_Export,
                    data: { contractTermID: _CUSContract_Price_DI_LoadTypeOfPartner.Params.TermID, priceID: _CUSContract_Price_DI_LoadTypeOfPartner.Params.PriceID },
                    success: function (res) {
                        $rootScope.IsLoading = false;
                        $rootScope.DownloadFile(res);
                    }
                })
            },
            Upload: function (e, callback) {
                $rootScope.IsLoading = true;
                Common.Services.Call($http, {
                    url: Common.Services.url.CUS,
                    method: _CUSContract_Price_DI_LoadTypeOfPartner.URL.Excel_Check,
                    data: { file: e, contractTermID: _CUSContract_Price_DI_LoadTypeOfPartner.Params.TermID, priceID: _CUSContract_Price_DI_LoadTypeOfPartner.Params.PriceID },
                    success: function (data) {
                        callback(data);
                        $rootScope.IsLoading = false;
                    }
                })
            },
            Window: win,
            Complete: function (e, data) {
                if (!$scope.Item.TermClosed) {
                    $rootScope.IsLoading = true;
                    Common.Services.Call($http, {
                        url: Common.Services.url.CUS,
                        method: _CUSContract_Price_DI_LoadTypeOfPartner.URL.Excel_Save,
                        data: { lst: data, priceID: _CUSContract_Price_DI_LoadTypeOfPartner.Params.PriceID },
                        success: function (res) {
                            $rootScope.Message({
                                Msg: 'Đã cập nhật.',
                                NotifyType: Common.Message.NotifyType.SUCCESS
                            });
                            $scope.LoadData_Loading();
                        }
                    })
                } else {
                    $rootScope.Message({ Msg: 'Phụ lục đã đóng', NotifyType: Common.Message.NotifyType.ERROR });
                }
            }
        })
    }

    Common.ALL.Get($http, {
        url: Common.ALL.URL.SYSVarPriceOfGOP,
        success: function (res) {
            _CUSContract_Price_DI_LoadTypeOfPartner.Data.ListPriceOfGOP = res;
        }
    })

    $rootScope.IsLoading = true;
    Common.Services.Call($http, {
        url: Common.Services.url.CUS,
        method: _CUSContract_Price_DI_LoadTypeOfPartner.URL.Data,
        data: { contractTermID: _CUSContract_Price_DI_LoadTypeOfPartner.Params.TermID },
        success: function (res) {
            _CUSContract_Price_DI_LoadTypeOfPartner.Data.ListGroupProduct = res.ListGroupOfProduct;

            Common.Services.Call($http, {
                url: Common.Services.url.CUS,
                method: _CUSContract_Price_DI_LoadTypeOfPartner.URL.Get,
                data: { priceID: _CUSContract_Price_DI_LoadTypeOfPartner.Params.PriceID },
                success: function (res) {
                    $scope.Item = res;

                    if ($scope.Item.ID > 0) {
                        $timeout(function () {
                            var isframe = $scope.Item.TypeOfContract == 1 ? false : true;
                            $scope.IsFrame = $scope.Item.TypeOfContract == 1 ? false : true;

                            $scope.InitView_Loading();
                            $timeout(function () {
                                $scope.LoadData_Loading();
                            }, 1)
                        }, 1)
                    }
                }
            })
        }
    });

    $scope.Back_Click = function ($event) {
        $event.preventDefault();

        $state.go("main.CUSContract.PriceDI", _CUSContract_Price_DI_LoadTypeOfPartner.Params)
    }

    $scope.InitView_Loading = function () {
        Common.Log("InitView_Loading");

        var model = {
            id: "ID",
            fields: {
                ID: { type: 'number', nullable: true },
                PriceID: { type: 'string' },
                Code: { editable: false, type: 'string' },
                LocationName: { editable: false, type: 'string' }
            }
        }

        var columns = [
            {
                title: ' ', width: '50px',
                template: '<a href="/" ng-click="Loading_Delete_Click($event,dataItem,loading_grid)" class="k-button"><i class="fa fa-trash"></i></a>',
                filterable: false, sortable: false
            },
            {
                field: 'GroupOfLocationCode', title: 'Mã loại địa điểm', width: 250,
                headerAttributes: { style: "vertical-align: middle;" },
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'GroupOfLocationName', title: 'Tên loại địa điểm', width: 250,
                headerAttributes: { style: "vertical-align: middle;" },
                filterable: { cell: { operator: 'contains', showOperators: false } }
            }
        ];

        for (var i = 0; i < _CUSContract_Price_DI_LoadTypeOfPartner.Data.ListGroupProduct.length; i++) {
            var item = _CUSContract_Price_DI_LoadTypeOfPartner.Data.ListGroupProduct[i];

            var field = "F_" + item.ID
            model.fields[field + "_PriceGOPID"] = { type: 'number' };
            model.fields[field + "_Price"] = { type: 'number' };
            model.fields[field + "_PriceGOPName"] = { type: 'string' };
            model.fields[field + "_GOPID"] = { type: 'number' };

            columns.push({
                title: item.GroupName, headerTemplate: '<span title="' + item.GroupName + '">' + item.GroupName + '</span>',
                headerAttributes: { style: 'text-align: center;' },
                columns: [
                    {
                        field: field + "_PriceGOPID", title: 'Đơn vị tính', width: 150, filterable: false,
                        template: "#=" + field + "_PriceGOPName" + "==null?' ':" + field + "_PriceGOPName" + " #",
                        editor: function (container, options) {
                            $('<input data-bind="value:' + options.field + '" />').appendTo(container)
                                .kendoComboBox({
                                    autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true,
                                    dataTextField: 'ValueOfVar', dataValueField: 'ID', minLength: 3,
                                    dataSource: Common.DataSource.Local({
                                        data: _CUSContract_Price_DI_LoadTypeOfPartner.Data.ListPriceOfGOP,
                                        model: {
                                            id: 'ID',
                                            fields: {
                                                ID: { type: 'number' },
                                                ValueOfVar: { type: 'string' },
                                            }
                                        }
                                    }),
                                    change: function (e) {
                                        if (Common.HasValue(e.sender.dataItem(e.item))) {
                                            var selectedValue = e.sender.dataItem(e.item).ID;
                                            var selectedText = e.sender.dataItem(e.item).ValueOfVar;
                                            var a = options.field;
                                            var b = a.split('_');
                                            var c = b.slice(0, 2);
                                            var d = c.join('_');
                                            options.model.set(d + "_PriceGOPName", selectedText)
                                        }
                                    }
                                });
                        }
                    },
                    {
                        field: field + "_Price", title: 'Giá', width: 120,
                        template: "#=" + field + "_Price" + "!=null?Common.Number.ToMoney(" + field + "_Price" + "):\"0\"#",
                        editor: function (container, options) {
                            $('<input data-bind="value:' + options.field + '" />').appendTo(container)
                                .kendoNumericTextBox({
                                    format: 'n0', spinners: false, culture: 'en-US', min: 0, step: 1, decimals: 0
                                })
                        },
                        filterable: false, sortable: false
                    }
                ]
            })
        }

        columns.push({ title: ' ', filterable: false, sortable: false });

        $scope.loading_gridOptions = {
            dataSource: Common.DataSource.Local({
                data: [], model: model
            }),
            columns: columns,
            height: '99%', scrollable: true, pageable: false, filterable: { mode: 'row' }, editable: 'incell', resizable: true,

            dataBound: function () {
                $rootScope.ExpandKGrid(this.element);
            }
        };


    }
    $scope.Loading_Add_Click = function ($event, win) {
        $event.preventDefault();

        win.center().open();
        $timeout(function () {
            $scope.loading_location_notin_gridOptions.dataSource.read();
            $scope.loading_location_notin_grid.refresh();
        }, 1)
    }

    $scope.Loading_Delete_Click = function ($event, item, grid) {
        $event.preventDefault();
        $rootScope.IsLoading = true;
        Common.Services.Call($http, {
            url: Common.Services.url.CUS,
            method: _CUSContract_Price_DI_LoadTypeOfPartner.URL.Loading_Delete,
            data: { ID: item.ID },
            success: function (res) {
                $rootScope.Message({ Msg: "Đã xóa!" });

                $scope.LoadData_Loading();
            }
        })
    }

    $scope.Loading_DeleteAll_Click = function ($event, grid) {
        $event.preventDefault();

        var data = [];
        Common.Data.Each(grid.dataSource.data(), function (o) {
            data.push(o.ID);
        })

        $rootScope.Message({
            Type: Common.Message.Type.Confirm,
            Msg: 'Bạn muốn xóa tất cả dữ liệu?',
            Ok: function () {
                $rootScope.IsLoading = true;
                Common.Services.Call($http, {
                    url: Common.Services.url.CUS,
                    method: _CUSContract_Price_DI_LoadTypeOfPartner.URL.Loading_DeleteList,
                    data: { priceID: _CUSContract_Price_DI_LoadPartner.Params.PriceID },
                    success: function (res) {

                        $scope.LoadData_Loading();
                        $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                    },
                    error: function (res) {
                        $rootScope.IsLoading = false;
                    }
                });
            }
        })
    }

    $scope.Loading_Location_NotIn_Submit_Click = function ($event, grid, win) {
        $event.preventDefault();

        var data = [];
        Common.Data.Each(grid.dataSource.data(), function (o) {
            if (o.IsChoose)
                data.push(o.ID);
        })
        if (data.length > 0) {
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.CUS,
                method: _CUSContract_Price_DI_LoadTypeOfPartner.URL.Loading_Location_NotIn_SaveList,
                data: { data: data, priceID: $scope.Item.ID },
                success: function (res) {
                    win.close();

                    $scope.LoadData_Loading();

                    $rootScope.Message({ Msg: "Đã cập nhật!" });
                },
                error: function (res) {
                    $rootScope.IsLoading = false;
                }
            });
        } else {
            win.close();
        }
    }

    $scope.Loading_Update_Click = function ($event, grid) {
        $event.preventDefault();
        var dataSource = grid.dataSource.data();
        var data = [];
        for (var i = 0; i < dataSource.length; i++) {
            var dataitem = dataSource[i];
            if (dataitem.dirty) {
                var item = {
                    ID: dataitem.ID,
                    GroupOfLocationID: dataitem.GroupOfLocationID,
                    ListPriceTruckLoadingDetail: []
                };
                for (var j = 0; j < _CUSContract_Price_DI_LoadTypeOfPartner.Data.ListGroupProduct.length; j++) {
                    var product = _CUSContract_Price_DI_LoadTypeOfPartner.Data.ListGroupProduct[j];
                    var field = "F_" + product.ID;
                    var s = {
                        'ID': dataitem[field + "_ID"],
                        'Price': dataitem[field + "_Price"],
                        'PriceOfGOPID': dataitem[field + "_PriceGOPID"],
                        'GroupOfProductID': dataitem[field + "_GOPID"]
                    };
                    if (s.Price >= 0 && s.PriceOfGOPID > 0)
                        item.ListPriceTruckLoadingDetail.push(s);
                }
                if (item.ListPriceTruckLoadingDetail.length > 0)
                    data.push(item);
            }
        }
        if (data.length > 0) {
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.CUS,
                method: _CUSContract_Price_DI_LoadTypeOfPartner.URL.Loading_SaveList,
                data: { data: data },
                success: function (res) {
                    $scope.LoadData_Loading();
                    $rootScope.Message({ Msg: "Đã cập nhật!" });
                }
            })
        }
    }

    $scope.LoadData_Loading = function () {
        Common.Log("LoadData_Loading");

        Common.Services.Call($http, {
            url: Common.Services.url.CUS,
            method: _CUSContract_Price_DI_LoadTypeOfPartner.URL.Loading_List,
            data: { priceID: $scope.Item.ID },
            success: function (res) {
                Common.Services.Error(res, function (res) {
                    var dataSource = [];
                    for (var i = 0; i < res.length; i++) {
                        var item = res[i];
                        var data = {
                            'ID': item.ID,
                            'GroupOfLocationID': item.GroupOfLocationID,
                            'GroupOfLocationCode': item.GroupOfLocationCode,
                            'GroupOfLocationName': item.GroupOfLocationName
                        };
                        var lst = item.ListPriceTruckLoadingDetail;
                        for (var j = 0; j < _CUSContract_Price_DI_LoadTypeOfPartner.Data.ListGroupProduct.length; j++) {

                            var gop = _CUSContract_Price_DI_LoadTypeOfPartner.Data.ListGroupProduct[j];
                            var field = "F_" + gop.ID

                            data[field + "_Price"] = 0;
                            data[field + "_PriceGOPID"] = null;
                            data[field + "_PriceGOPName"] = "";
                            data[field + "_GOPID"] = gop.ID;
                            for (var k = 0; k < lst.length; k++) {
                                if (lst[k].GroupOfProductID == gop.ID) {
                                    data[field + "_Price"] = lst[k].Price;
                                    data[field + "_PriceGOPID"] = lst[k].PriceOfGOPID;
                                    data[field + "_PriceGOPName"] = lst[k].PriceOfGOPName;
                                    data[field + "_GOPID"] = lst[k].GroupOfProductID;
                                }
                            }
                        }
                        dataSource.push(data);
                    }

                    $scope.loading_gridOptions.dataSource.data(dataSource);
                })

                $rootScope.IsLoading = false;
            }
        })
    }

    $scope.loading_location_notin_gridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.CUS,
            method: _CUSContract_Price_DI_LoadTypeOfPartner.URL.Loading_Location_NotIn_List,
            readparam: function () {
                return {
                    priceID: Common.HasValue($scope.Item) ? $scope.Item.ID : -1
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
                title: 'Chọn', width: '50px',
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,loading_location_notin_grid)" />',
                headerAttributes: { style: 'text-align: center;' }, attributes: { style: 'text-align: center;' },
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,loading_location_notin_grid)" />',
                filterable: false, sortable: false
            },
            {
                field: 'Code', title: 'Mã', width: 100,
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'GroupName', title: 'Loại địa điểm', width: 200,
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            { title: '', filterable: false, sortable: false }
        ]
    }

    $scope.Window_Close_Click = function ($event, win) {
        $event.preventDefault();

        win.close();
    }

    //#endregion
}]);