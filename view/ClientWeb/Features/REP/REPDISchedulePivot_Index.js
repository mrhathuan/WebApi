
/// <reference path="~/Scripts/jquery-1.9.1.intellisense.js" />
/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />

var _REPDISchedulePivot_Index = {
    URL: {
        List: 'REPDISchedulePivot_List',
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

angular.module('myapp').controller('REPDISchedulePivot_IndexCtrl', ['$rootScope', '$scope', '$http', '$location', '$state', '$timeout', '$stateParams', function ($rootScope, $scope, $http, $location, $state, $timeout, $stateParams) {
    if ($rootScope.IsPageComplete != true) return;
    Common.Log('REPDISchedulePivot_IndexCtrl');
    $rootScope.IsLoading = false;

    $scope.Auth = $rootScope.GetAuth();

    $scope.REPDISchedulePivot_Splitter_Options = {
        panes: [
            { collapsible: true, size: '350px' },
            { collapsible: true }
        ]
    };

    $scope.Item = {
        lstCustomerID: [],
        DateFrom: Common.Date.AddDay(new Date(), -5),
        DateTo: new Date(),
    }

    $scope.mulCustomer_Options = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: "contains",
        suggest: true, dataTextField: 'CustomerName', dataValueField: 'ID',
        itemTemplate: '<span>[#= Code #] #= CustomerName #</span>',
        headerTemplate: '<strong>Khách hàng</span></strong>',
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields: {
                    CustomerName: { type: 'string' },
                    ID: { type: 'number' },
                }
            }
        }),
        change: function (e) {
            var lst = this;
            $timeout(function () {
                $scope.SetWidth_Select(lst);
            }, 1);
        }
    };
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

    Common.ALL.Get($http, {
        url: Common.ALL.URL.Customer,
        success: function (res) {
            $scope.mulCustomer_Options.dataSource.data(res);
        }
    });

    $scope.REPDISchedulePivot_PivotExcel_Click = function ($event, cbo) {
        $event.preventDefault();

        $timeout(function () {
            $('#REPDISchedulePivot_pivot_grid').data('kendoPivotGrid').saveAsExcel();
        }, 1);
    };

    $scope.REPDISchedulePivot_Search = function ($event, cbo) {
        $event.preventDefault();

        $rootScope.IsLoading = true;
        Common.Services.Call($http, {
            url: Common.Services.url.REP,
            method: _REPDISchedulePivot_Index.URL.List,
            data: { lstid: $scope.Item.lstCustomerID, dtfrom: $scope.Item.DateFrom, dtto: $scope.Item.DateTo },
            success: function (res) {
                $rootScope.IsLoading = false;

                angular.forEach(res, function (v, i) {
                    v.RequestDate = Common.Date.FromJsonDDMMYY(v.RequestDate);
                    v.DateConfig = Common.Date.FromJsonDDMMYY(v.DateConfig);
                });

                $timeout(function () {
                    var pivotgrid = $('#REPDISchedulePivot_pivot_grid').data('kendoPivotGrid');
                    if (pivotgrid != null) {
                        pivotgrid.destroy();
                        $('#REPDISchedulePivot_pivot_grid').empty();
                    }

                    var pivotgrid = $('#REPDISchedulePivot_pivot_grid').kendoPivotGrid({
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
                                        OrderCode: { type: "string" },
                                        DNCode: { type: "string" },
                                        SOCode: { type: "string" },
                                        RequestDate: { type: 'string' },
                                        DateConfig: { type: 'string' },

                                        StockCode: { type: "string" },
                                        StockName: { type: "string" },
                                        StockAddress: { type: "string" },

                                        PartnerCode: { type: "string" },
                                        PartnerName: { type: "string" },
                                        PartnerCodeName: { type: "string" },
                                        Address: { type: "string" },

                                        CUSRoutingCode: { type: "string" },
                                        CUSRoutingName: { type: "string" },
                                        GroupOfProductCode: { type: "string" },
                                        GroupOfProductName: { type: "string" },
                                        VehicleCode: { type: "string" },
                                        VendorCode: { type: "string" },
                                        VendorName: { type: "string" },
                                        CustomerCode: { type: "string" },
                                        CustomerName: { type: "string" },

                                        TonTranfer: { type: "number" },
                                        CBMTranfer: { type: "number" },
                                        QuantityTranfer: { type: "number" },
                                        TonBBGN: { type: "number" },
                                        CBMBBGN: { type: "number" },
                                        QuantityBBGN: { type: "number" },

                                        IntReturn: { type: "number" },
                                        TonReturn: { type: "number" },
                                        CBMReturn: { type: "number" },
                                        QuantityReturn: { type: "number" },
                                        ETD: { type: "date" },
                                        VehicleWeight: { type: "number" },
                                        InvoiceNote: { type: "string" },
                                    }
                                },
                                cube: {
                                    dimensions: {
                                        OrderCode: { caption: "Đơn hàng" },
                                        DNCode: { caption: "Số DN" },
                                        RequestDate: { caption: "Ngày y/c v/c" },
                                        DateConfig: { caption: "Ngày tính giá" },
                                        SOCode: { caption: "Số SO" },

                                        StockCode: { caption: "Mã điểm lấy" },
                                        StockName: { caption: "Tên điểm lấy" },
                                        StockAddress: { caption: "Điểm lấy" },

                                        PartnerCode: { caption: "Mã NPP" },
                                        PartnerName: { caption: "Điểm giao" },
                                        PartnerCodeName: { caption: "Mã + Tên phân phối" },
                                        Address: { caption: "Điểm giao" },

                                        CUSRoutingCode: { caption: "Mã cung đường" },
                                        CUSRoutingName: { caption: "Cung đường" },
                                        GroupOfProductCode: { caption: "Mã nhóm hàng" },
                                        GroupOfProductName: { caption: "Nhóm hàng" },
                                        VehicleCode: { caption: "Số xe" },
                                        VendorCode: { caption: "Mã nhà vận tải" },
                                        VendorName: { caption: "Nhà vận tải" },
                                        CustomerCode: { caption: "Mã khách hàng" },
                                        CustomerName: { caption: "Khách hàng" },

                                        ETD: { caption: "Ngày vận chuyển" },
                                        VehicleWeight: { caption: "Trọng tải xe" },
                                        InvoiceNote: { caption: "Số chứng từ" },
                                    },
                                    measures: {
                                        "Tấn lấy": { field: "TonTranfer", format: "{0:n2}", aggregate: "sum" },
                                        "Khối lấy": { field: "CBMTranfer", format: "{0:n2}", aggregate: "sum" },
                                        "Số lượng lấy": { field: "TonTranfer", format: "{0:n0}", aggregate: "sum" },
                                        "Tấn giao": { field: "TonBBGN", format: "{0:n2}", aggregate: "sum" },
                                        "Khối giao": { field: "CBMBBGN", format: "{0:n2}", aggregate: "sum" },
                                        "Số lượng giao": { field: "QuantityBBGN", format: "{0:n0}", aggregate: "sum" },
                                        "Tấn trả về": { field: "TonReturn", format: "{0:n2}", aggregate: "sum" },
                                        "Khối trả về": { field: "CBMReturn", format: "{0:n2}", aggregate: "sum" },
                                        "Số lượng trả về": { field: "QuantityReturn", format: "{0:n0}", aggregate: "sum" },
                                        "Điểm trả về": { field: "IntReturn", format: "{0:n0}", aggregate: "sum" },
                                    }
                                }
                            },
                            columns: [{ name: "VehicleCode", title: "Số xe", expand: true }],
                            rows: [{ name: "OrderCode", title: "Đơn hàng", expand: true }],
                            measures: ['Tấn lấy']
                        },
                        dataBound: function (e) {
                            //var datasource = this.dataSource;
                            //$timeout(function () {
                            //    try {
                            //        initChart(_REPDISchedulePivot.convertData(datasource, collapsed));
                            //    } catch (e) {
                            //        var chart = $("#pivotChart").data("kendoChart");
                            //        chart.dataSource.data([]);
                            //        chart.redraw();
                            //    }
                            //}, 1);
                        }
                    }).data("kendoPivotGrid");

                    var config = $("#REPDISchedulePivot_pivot_config").data('kendoPivotConfigurator');
                    if (config == null) {
                        $("#REPDISchedulePivot_pivot_config").kendoPivotConfigurator({
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
    };
}]);