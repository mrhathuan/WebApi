/// <reference path="~/Scripts/jquery-1.9.1.intellisense.js" />
/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />

var _REPTotalPL_Index = {
    URL: {

        List: 'REPTotalPL_List',
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
angular.module('myapp').controller('REPTotalPL_IndexCtrl', ['$rootScope', '$scope', '$http', '$location', '$state', '$timeout', '$stateParams', function ($rootScope, $scope, $http, $location, $state, $timeout, $stateParams) {
    if ($rootScope.IsPageComplete != true) return;
    Common.Log('REPTotalPL_IndexCtrl');
    $rootScope.IsLoading = false;

    $scope.Auth = $rootScope.GetAuth();

    $scope.REPTotalPL_Splitter_Options = {
        panes: [
            { collapsible: true, size: '350px' },
            { collapsible: true }
        ]
    };

    $scope.Item = {
        
        DateFrom: Common.Date.AddDay(new Date(), -5),
        DateTo: new Date(),
        typeOfView:1,
    }

    $scope.cbotypeOfView = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: "contains",
        suggest: true, dataTextField: 'ValueName', dataValueField: 'ID',
        dataSource: [
            { ID: 1, ValueName: 'Khách hàng' },
            { ID: 2, ValueName: 'Đối tác' },
        ],
        change: function (e) { }
    }
    $scope.REPTotalPL_Search = function ($event, vform) {
        $event.preventDefault();
        //$rootscope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.REP,
                method: _REPTotalPL_Index.URL.List,
                data: { dtfrom:$scope.Item.DateFrom, dtto:$scope.Item.DateTo, typeOfView:$scope.Item.typeOfView },
                success: function (res) {
                    $rootScope.IsLoading = false;

                    angular.forEach(res, function (v, i) {
                        v.RequestDate = Common.Date.FromJsonDDMMYY(v.RequestDate);
                        v.DateConfig = Common.Date.FromJsonDDMMYY(v.DateConfig);
                    });
                    $timeout(function () {
                        var pivotgrid = $('#REPTotalPL_pivot_grid').data('kendoPivotGrid');
                        if (pivotgrid != null) {
                            pivotgrid.destroy();
                            $('#REPTotalPL_pivot_grid').empty();
                        }
                        var pivotgrid = $('#REPTotalPL_pivot_grid').kendoPivotGrid({
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
                                            TypeOfTransport: { type: "string" },
                                            TransportMode: { type: "string" },
                                        }
                                    },
                                    cube: {
                                        dimensions: {
                                            CustomerCode: { caption: "Mã KH/đối tác" },
                                            CustomerName: { caption: "Tên KH/đối tác" },
                                            TypeOfTransport: { caption: "Vận chuyển" },
                                            TransportMode: { caption: "Loại vận chuyển" },
                                        },
                                        measures: {
                                            "Doanh thu": { field: "Credit", format: "{0:n2}", aggregate: "sum" },
                                            "Chi phí": { field: "Debit", format: "{0:n2}", aggregate: "sum" },
                                            "Lợi nhuận": { field: "PL", format: "{0:n2}", aggregate: "sum" },
                                        }
                                    }
                                },
                                columns: [{ name: "CustomerCode", title: "Mã KH/đối tác", expand: true }],
                                rows: [{ name: "TypeOfTransport", title: "Vận chuyển", expand: true }],
                                measures: ['Doanh thu']
                            },
                            dataBound: function (e) { }
                        }).data("kendoPivotGrid");

                        var config = $("#REPTotalPL_pivot_config").data('kendoPivotConfigurator');
                        if (config == null) {
                            $("#REPTotalPL_pivot_config").kendoPivotConfigurator({
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
