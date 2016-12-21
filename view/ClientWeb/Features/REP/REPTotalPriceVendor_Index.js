
/// <reference path="~/Scripts/jquery-1.9.1.intellisense.js" />
/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />

var _REPTotalPriceVendor_Index = {
    URL: {
        List: 'REPTotalPriceVendor_Data',

        Read_Vendor: 'ALL_Vendor',
        Read_Customer: 'CATDistributor_Customer_Read',
        VenList: 'REPTotalPriceVendor_ListVendor',

        ExcelColumn: 'REPTotalPriceVendor_ExportColumn',
    },
    Data: {
        DataCustomer: [],
    },

    //function flatten the tree of tuples that datasource returns
    flattenTree: function (tuples) {
        tuples = tuples.slice();
        var result = [];
        var tuple = tuples.shift();
        var idx, length, spliceIndex, children, member;
        while (tuple) {
            //required for multiple measures
            if (tuple.dataIndex !== undefined) {
                result.push(tuple);
            }
            spliceIndex = 0;
            for (idx = 0, length = tuple.members.length; idx < length; idx++) {
                member = tuple.members[idx];
                children = member.children;
                if (member.measure) {
                    [].splice.apply(tuples, [0, 0].concat(children));
                } else {
                    [].splice.apply(tuples, [spliceIndex, 0].concat(children));
                }
                spliceIndex += children.length;
            }
            tuple = tuples.shift();
        }
        return result;
    },

    //Check whether the tuple has been collapsed
    isCollapsed: function (tuple, collapsed) {
        var name = tuple.members[0].parentName;
        for (var idx = 0, length = collapsed.length; idx < length; idx++) {
            if (collapsed[idx] === name) {
                console.log(name);
                return true;
            }
        }
        return false;
    },

    //the main function that convert PivotDataSource data into understandable for the Chart widget format
    convertData: function (dataSource, collapsed) {
        var columnTuples = this.flattenTree(dataSource.axes().columns.tuples || [], collapsed.columns);
        var rowTuples = this.flattenTree(dataSource.axes().rows.tuples || [], collapsed.rows);
        var data = dataSource.data();
        var rowTuple, columnTuple;
        var idx = 0;
        var result = [];
        var columnsLength = columnTuples.length;
        for (var i = 0; i < rowTuples.length; i++) {
            rowTuple = rowTuples[i];
            if (!this.isCollapsed(rowTuple, collapsed.rows)) {
                for (var j = 0; j < columnsLength; j++) {
                    columnTuple = columnTuples[j];

                    if (!this.isCollapsed(columnTuple, collapsed.columns)) {
                        if (idx > columnsLength && idx % columnsLength !== 0) {
                            result.push({
                                measure: Number(data[idx].value),
                                column: columnTuple.members[0].caption,
                                row: rowTuple.members[0].caption
                            });
                        }
                    }
                    idx += 1;
                }
            }
        }
        return result;
    }

}

var collapsed = {
    columns: [],
    rows: []
};

angular.module('myapp').controller('REPTotalPriceVendor_IndexCtrl', ['$rootScope', '$scope', '$http', '$location', '$state', '$timeout', '$stateParams', function ($rootScope, $scope, $http, $location, $state, $timeout, $stateParams) {
    if ($rootScope.IsPageComplete != true) return;
    Common.Log('REPTotalPriceVendor_IndexCtrl');
    $rootScope.IsLoading = false;

    $scope.Auth = $rootScope.GetAuth();

    $scope.REPTotalPriceVendor_Splitter_Options = {
        panes: [
            { collapsible: true, size: '350px' },
            { collapsible: true }
        ]
    };

    $scope.Item = {
        CustomerID: -1,
        EffectDate: new Date(),
        TypePrice: 0,
        ListVendor: [],
    }

    //#region search
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
        valuePrimitive: true, dataTextField: "Code", dataValueField: "ID", placeholder: "Chọn nhà xe...", filter: "contains", ignoreCase: true,
        highlightFirst: true, autoClose: false,
        itemTemplate: '<span>#= Code #</span><span style="float:right;">#= CustomerName #</span>',
        headerTemplate: '<strong><span> Mã nhà xe </span><span style="float:right;"> Tên nhà xe </span></strong>',
        change: function (e) {
            $scope.Item.ListVendor = this.value();
            var lst = this;
            $timeout(function () {
                $scope.SetWidth_Select(lst);
            }, 1);
            if ($scope.Item.ListVendor.length == 0) {
                $scope.Item.CustomerID = -1;
            }
        }
    }

    $scope.isWidth = false;
    $scope.SetWidth_Select = function (lst) {
        var list = lst;
        var lst1 = lst.wrapper.find('.k-floatwrap:first ul li');
        var widthDiv = lst.wrapper.find('.k-floatwrap:first').width();
        var w = 0;
        var obj = null;
        var lst2 = [];
        if (lst1.length > 1) {
            $.each(lst1, function (i, v) {
                if ($(v).attr('data-show') != 'unshow') {
                    lst2.push(v);
                }
            });
        }
        else {
            lst2 = lst1;
        }

        $.each(lst2, function (i, v) {
            w += $(v).outerWidth(true);
            $(v).attr('data-show', 'show')
            if (w >= widthDiv) {
                $(v).hide();
                $(v).attr('data-show', 'unshow');
            }
            obj = v;
        });
        if (obj == null) {
            $scope.isWidth = false;
        }
        if (w >= widthDiv && !$scope.isWidth) {
            $scope.isWidth = true;
            $(obj).show();
            $(obj).html('...');
        }
        if (w > widthDiv) {
            $scope.SetWidth_Select(list);
        }
    }

    Common.Services.Call($http, {
        url: Common.Services.url.CAT,
        method: _REPTotalPriceVendor_Index.URL.Read_Vendor,
        data: {},
        success: function (res) {
            if (Common.HasValue(res)) {

                _REPTotalPriceVendor_Index.Data.DataCustomer = res.Data;
                $scope.mts_CustomerOption.dataSource.data(res.Data);
            }
        }
    });

    $scope.cboCustomerOptions = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true, placeholder: "Chọn khách hàng...",
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
        change: function () {
            
            if ($scope.Item.CustomerID == -1) {
                $scope.Item.ListVendor = [];
            } else if (Common.HasValue($scope.Item.CustomerID) && $scope.Item.CustomerID > 0) {
                $timeout(function () {
                    $rootScope.IsLoading = true;
                    Common.Services.Call($http, {
                        url: Common.Services.url.REP,
                        method: _REPTotalPriceVendor_Index.URL.VenList,
                        data: { cusId: $scope.Item.CustomerID },
                        success: function (res) {
                            $rootScope.IsLoading = false;
                            $scope.Item.ListVendor = res;
                        }
                    });
                }, 1)


            }
            //this.refresh();
        }
    }

    Common.Services.Call($http, {
        url: Common.Services.url.CAT,
        method: _REPTotalPriceVendor_Index.URL.Read_Customer,
        data: {},
        success: function (res) {
            var item = { ID: -1, CustomerName: '' };
            res.Data.unshift(item);
            $scope.cboCustomerOptions.dataSource.data(res.Data);
        }
    });

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
        }),
        change: function (e) {
        }
    }

    Common.ALL.Get($http, {
        url: Common.ALL.URL.SYSVarTransportMode,
        success: function (res) {
            $timeout(function () {
                $scope.cboTransportOptions.dataSource.data(res);
                if (Common.HasValue(res) && res.length > 0) {
                    $scope.Item.TransportModeID = res[0].ID;
                }
            }, 1);
        }
    });

    $scope.cboTypePriceOptions = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: "contains",
        suggest: true, dataTextField: 'ValueName', dataValueField: 'ID',
        dataSource: [
            { ID: 0, ValueName: 'Thường' },
            { ID: 1, ValueName: 'Bậc thang' },
        ],
        change: function (e) {
        }
    }
    //#endregion

    $scope.PivotExcel_Click = function ($event, cbo) {
        $event.preventDefault();

        if ($('#REPTotalPriceVendor_pivot_grid').data('kendoPivotGrid') != null)
            $timeout(function () {
                $('#REPTotalPriceVendor_pivot_grid').data('kendoPivotGrid').saveAsExcel();
            }, 1);
    };

    $scope.REPTotalPriceVendor_Search = function ($event, vform) {
        $event.preventDefault();
        var error = false;
        if ($scope.Item.ListVendor.length == 0 || !Common.HasValue($scope.Item.CustomerID) || $scope.Item.CustomerID <= 0) {
            error = true;
            $rootScope.Message({ Msg: 'Chưa chọn khách hàng hoặc nhà xe', NotifyType: Common.Message.NotifyType.ERROR });
        }
        if (!Common.HasValue($scope.Item.TransportModeID) || $scope.Item.TransportModeID < 0) {
            error = true;
            $rootScope.Message({ Msg: 'Chưa chọn hình thức v/c', NotifyType: Common.Message.NotifyType.ERROR });
        }
        if (!Common.HasValue($scope.Item.TypePrice) || $scope.Item.TypePrice < 0) {
            error = true;
            $rootScope.Message({ Msg: 'Chưa chọn loại bảng giá', NotifyType: Common.Message.NotifyType.ERROR });
        }
        if (!Common.HasValue($scope.Item.EffectDate)) {
            error = true;
            $rootScope.Message({ Msg: 'Chưa chọn ngày hiệu lực', NotifyType: Common.Message.NotifyType.ERROR });
        }
        if (vform() && !error) {
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.REP,
                method: _REPTotalPriceVendor_Index.URL.List,
                data: { cusId: $scope.Item.CustomerID, lstVenId: $scope.Item.ListVendor, transportModeID: $scope.Item.TransportModeID, typePrice: $scope.Item.TypePrice, effectDate: $scope.Item.EffectDate },
                success: function (res) {
                    $rootScope.IsLoading = false;

                    angular.forEach(res, function (v, i) {
                        v.EffectDate = Common.Date.FromJsonDDMMYY(v.EffectDate);
                    });

                    $timeout(function () {
                        var pivotgrid = $('#REPTotalPriceVendor_pivot_grid').data('kendoPivotGrid');
                        if (pivotgrid != null) {
                            pivotgrid.destroy();
                            $('#REPTotalPriceVendor_pivot_grid').empty();
                        }

                        var pivotgrid = $('#REPTotalPriceVendor_pivot_grid').kendoPivotGrid({
                            excel: { fileName: 'pivotpl.xlsx' },
                            filterable: true, sortable: false, columnWidth: '150',
                            collapseMember: function (e) {
                                var axis = collapsed[e.axis];
                                var path = e.path[0];
                                if (axis.indexOf(path) === -1) axis.push(path);
                            },
                            expandMember: function (e) {
                                var axis = collapsed[e.axis];
                                var index = axis.indexOf(e.path[0]);
                                if (index !== -1) axis.splice(index, 1);
                            },
                            dataSource: {
                                data: res,
                                schema: {
                                    model: {
                                        fields: {
                                            CustomerCode: { type: "string" },
                                            CustomerName: { type: "string" },
                                            EffectDate: { type: "date" },
                                            RoutingCode: { type: "string" },
                                            RoutingName: { type: "string" },
                                            PackingCode: { type: "string" },
                                            PackingName: { type: "string" },
                                            LevelCode: { type: "string" },
                                            LevelName: { type: "string" },
                                            GroupOfProductCode: { type: "string" },
                                            GroupOfProductName: { type: "string" },
                                            GroupOfVehicleCode: { type: "string" },
                                            GroupOfVehicleName: { type: "string" },
                                        }
                                    },
                                    cube: {
                                        dimensions: {
                                            CustomerCode: { caption: "Mã KH/đối tác" },
                                            CustomerName: { caption: "Tên KH/đối tác" },
                                            EffectDate: { caption: "Ngày hiệu lực" },
                                            RoutingCode: { caption: "Mã cung đường" },
                                            RoutingName: { caption: "Tên cung đường" },
                                            PackingCode: { caption: "Mã loại Cont" },
                                            PackingName: { caption: "Tên loại Cont" },
                                            LevelCode: { caption: "Mã bậc" },
                                            LevelName: { caption: "Tên bậc" },
                                            GroupOfProductCode: { caption: "Mã nhóm sản phẩm" },
                                            GroupOfProductName: { caption: "Tên nhóm sản phẩm" },
                                            GroupOfVehicleCode: { caption: "Mã loại xe" },
                                            GroupOfVehicleName: { caption: "Tên loại xe" },
                                        },
                                        measures: {
                                            "Giá": { field: "Price", format: "{0:n2}", aggregate: "sum" },
                                        }
                                    }
                                },
                                columns: [{ name: "CustomerCode", title: "Mã KH/đối tác", expand: true }],
                                rows: [{ name: "RoutingCode", title: "Mã cung đường", expand: true }],
                                measures: ['Giá']
                            },
                            dataBound: function (e) {
                                //var datasource = this.dataSource;
                                //$timeout(function () {
                                //    try {
                                //        initChart(_REPTotalPriceVendor.convertData(datasource, collapsed));
                                //    } catch (e) {
                                //        var chart = $("#pivotChart").data("kendoChart");
                                //        chart.dataSource.data([]);
                                //        chart.redraw();
                                //    }
                                //}, 1);
                            }
                        }).data("kendoPivotGrid");

                        var config = $("#REPTotalPriceVendor_pivot_config").data('kendoPivotConfigurator');
                        if (config == null) {
                            $("#REPTotalPriceVendor_pivot_config").kendoPivotConfigurator({
                                dataSource: pivotgrid.dataSource,
                                filterable: true,
                                sortable: false
                            });
                        }
                        else {
                            config.setDataSource(pivotgrid.dataSource);
                        }
                    }, 1);

                },
                error: function (res) {
                    $rootScope.IsLoading = false;
                }
            });
        }
    };

    $scope.ExcelColumn_Click = function ($event, vform) {
        $event.preventDefault();
        var error = false;
        if ($scope.Item.ListVendor.length == 0 || !Common.HasValue($scope.Item.CustomerID) || $scope.Item.CustomerID <= 0) {
            error = true;
            $rootScope.Message({ Msg: 'Chưa chọn khách hàng hoặc nhà xe', NotifyType: Common.Message.NotifyType.ERROR });
        }
        if (!Common.HasValue($scope.Item.TransportModeID) || $scope.Item.TransportModeID < 0) {
            error = true;
            $rootScope.Message({ Msg: 'Chưa chọn hình thức v/c', NotifyType: Common.Message.NotifyType.ERROR });
        }
        if (!Common.HasValue($scope.Item.TypePrice) || $scope.Item.TypePrice < 0) {
            error = true;
            $rootScope.Message({ Msg: 'Chưa chọn loại bảng giá', NotifyType: Common.Message.NotifyType.ERROR });
        }
        if (!Common.HasValue($scope.Item.EffectDate)) {
            error = true;
            $rootScope.Message({ Msg: 'Chưa chọn ngày hiệu lực', NotifyType: Common.Message.NotifyType.ERROR });
        }
        if (vform() && !error) {
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.REP,
                method: _REPTotalPriceVendor_Index.URL.ExcelColumn,
                data: { cusId: $scope.Item.CustomerID, lstVenId: $scope.Item.ListVendor, transportModeID: $scope.Item.TransportModeID, typePrice: $scope.Item.TypePrice, effectDate: $scope.Item.EffectDate },
                success: function (res) {
                    $rootScope.IsLoading = false;
                    $rootScope.DownloadFile(res);
                }
            })
        }
    }
}]);