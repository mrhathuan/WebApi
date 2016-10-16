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
                     template: '<a href="\\#" class="event-button" ng-click="PAGE_WinClick($event,#=id#)"><i class="fa fa-folder-open-o"></i></a>',
                     filterable: false, sortable: false
                 },
                {
                    field: "menuID", width: "300px", title: "Trang", template: "#=Menu.Name#", editable: false,
                    filterable: { cell: { operator: 'contains', showOperators: false } }
                },
                {
                    field: "createByID", width: "400px", title: "Người tạo",
                    filterable: { cell: { operator: 'contains', showOperators: false } }
                },
                {
                    field: "createDate", width: "150px", title: "Ngày tạo", filterable: false, template: "#= kendo.toString(kendo.parseDate(createDate, 'yyyy-MM-dd'), 'dd/MM/yyyy')#",
                    filterable: { cell: { template: function (e) { e.element.kendoDatePicker({ format: 'dd/MM/yyyy' }); }, operator: 'equal', showOperators: false } },
                },
                 {
                    field: "modifiedByDate", width: "130px", title: "Ngày sửa", filterable: false, template: "#= kendo.toString(kendo.parseDate(modifiedByDate, 'yyyy-MM-dd'), 'dd/MM/yyyy')#",
                    filterable: { cell: { template: function (e) { e.element.kendoDatePicker({ format: 'dd/MM/yyyy' }); }, operator: 'equal', showOperators: false } },
                 },
                 {
                     field: "modifiedByID", width: "250px", title: "Người sửa",
                     filterable: { cell: { showOperators: false, operator: 'contains' }}
                 }
        ],
        dataSource: {
            pageSize: 10,
            transport: {
                read: function (e) {
                    $http.post("/Pn/Page/PAGE_Read")
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

    $scope.BACK_Click = function ($event) {
        $event.preventDefault();
        window.location.href = '/Pn/#/CONTENT_Index';
    }

    $scope.PAGE_WinClick = function ($event, id) {
        $event.preventDefault();
        $rootScope.IsLoading = true;
        $http.post("/Pn/Page/PAGE_Get", { id: id }).then(function success(res) {
            $rootScope.IsLoading = false;
            $scope.Item = res.data.page;
            $scope.showModal_PAGE = true;
        })
    }

    $scope.PAGE_SaveClick = function ($event, vform) {
        $event.preventDefault();
        vform({ clear: true });
        if (vform()) {
            $http.post("/Pn/Page/PAGE_Save", { item: JSON.stringify($scope.Item) }).then(function success(res) {
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