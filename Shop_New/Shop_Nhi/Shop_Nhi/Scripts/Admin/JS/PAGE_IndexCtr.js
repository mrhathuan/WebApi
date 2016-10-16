/// <reference path="../../angular.min.js" />

'use strict'

app.controller('PAGE_IndexCtr', ['$http', '$scope', '$rootScope', function ($http, $scope, $rootScope) {
    $rootScope.title = 'Quản trị trang';
    $scope.Item = null;

    $scope.PAGE_gridOptions = {
        height: 550, pageable: true, autoSync: true, sortable: true, columnPAGE: false, resizable: true, reorderable: true, filterable: { mode: 'row' },
        toolbar: kendo.template($('#PAGEGridTemplate').html()),
        excel: {
            fileName: "PAGE.xlsx",
            filterable: true,
            //proxyURL: "/Pn/Pn/PRO_Excel_Export_Save",
            allPages: true
        },
        columns: [
                 {
                     title: ' ', width: '110px',
                     template: '<a href="\\#" class="event-button" ng-click="PAGE_WinClick($event,#=id#)"><i class="fa fa-pencil"></i></a>' +
                         '<a href="\\#" class="event-button" ng-click="RemoveItem($event,#=id#)"><i class="fa fa-trash"></i></a>',
                     filterable: false, sortable: false
                 },
                {
                    field: "Name", width: "300px", title: "Tên", editable: false,
                    filterable: { cell: { operator: 'contains', showOperators: false } }
                },
                {
                    field: "link", width: "400px", title: "Link",
                    filterable: { cell: { operator: 'contains', showOperators: false } }
                },
                {
                    field: "typeID", width: "100", title: "Thuộc", template: "#=PAGEType.name#",
                    filterable: { cell: { operator: 'contains', showOperators: false } }
                },
                 {
                     field: "dislayOrder", width: "100", title: "Thứ tự",
                     filterable: { cell: { operator: 'contains', showOperators: false } }
                 },
                {
                    field: "status", width: "130px", title: "Trạng thái", editable: false, filterable: false,
                    template: kendo.template($("#statusTpl").html())
                }
        ],
        dataSource: {
            pageSize: 10,
            transport: {
                read: function (e) {
                    $http.post("/Pn/PAGE/PAGE_Read")
                    .then(function (response) {
                        e.success(response.data)
                    }, function error(response) {
                        console.log(response);
                    })
                }
            },
            schema: {
                data: "Data",
                total: "Total",
                model: { // define the model of the data source. Required for validation and property types.
                    id: "ID",
                    fields: {
                        ID: { type: 'number', editable: false, nullable: true },
                        status: { type: "boolean" }
                    }
                }
            }
        }
    }

    $scope.RemoveItem = function ($event, id) {
        $event.preventDefault();
        var cf = confirm('Bạn chắc chắn muốn xóa PAGE này?');
        if (cf) {
            $http.post("/Pn/PAGE/PAGE_Delete", { id: id }).then(function success(res) {
                if (res.data.status == true) {
                    $scope.PAGE_Grid.dataSource.read();
                    $scope.PAGE_Grid.refresh();
                    toastr.success('Thành công', '');
                }
            })
        }
    }

    $scope.Type_drdlOptions = {
        dataTextField: "name",
        dataValueField: "ID",
        autoBind: false,
        dataSource: {
            severFiltering: true,
            transport: {
                read: {
                    url: "/Pn/PAGE/GET_Type",
                    contentType: "application/json",
                    type: "GET"
                }

            }
        }, change: function (e) {

        }
    }

    $scope.numFeeBase_options = { format: 'n0', spinners: false, culture: 'en-US', min: 0, step: 0.01 }

    $scope.ChangeStatus = function ($event, id) {
        $event.preventDefault();
        $http.post("/Pn/PAGE/PAGE_ChangeStatus", { id: id }).then(function success(res) {
            $scope.PAGE_Grid.dataSource.read();
            toastr.success('Thành công', '');
        })
    }

    $scope.PAGE_WinClick = function ($event, id) {
        $event.preventDefault();
        $http.post("/Pn/PAGE/PAGE_Get", { id: id }).then(function success(res) {
            $scope.Item = res.data.PAGE;
            $scope.showModal_PAGE = true;
        })
    }

    $scope.PAGE_SaveClick = function ($event, vform) {
        $event.preventDefault();
        vform({ clear: true });
        if (vform()) {
            $http.post("/Pn/PAGE/PAGE_Save", { item: JSON.stringify($scope.Item) }).then(function success(res) {
                if (res.data.status == true) {
                    vform({ clear: true });
                    $scope.PAGE_Grid.dataSource.read();
                    toastr.success(res.data.msg, '');
                    $scope.showModal_PAGE = false;
                } else {
                    toastr.error(res.data.msg, '');
                }
            })
        }
    }

}]);