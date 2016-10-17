/// <reference path="../../angular.min.js" />

'use strict'

app.controller('FOOTER_IndexCtr', ['$http', '$scope', '$rootScope', function ($http, $scope, $rootScope) {
    $rootScope.title = 'Quản trị thông báo';
    $scope.Item = null;

    $scope.Back_Click = function ($event) {
        $event.preventDefault();
        window.location.href = '/Pn/#/CONTENT_Index';
    }

    $scope.FOOTER_gridOptions = {
        height: '500px', pageable: true, autoSync: true, sortable: true, columnMenu: false, resizable: true, reorderable: true, filterable: { mode: 'row' },
        toolbar: kendo.template($('#FOOTERGridTemplate').html()),
        columns: [
                 {
                     field: "ID", width: "100px", title: "Mã",
                     filterable: { cell: { showOperators: false } }
                 },
                {
                    field: "createByID", width: "200px", title: "Người tạo", editable: false, filterable: false,
                    filterable: { cell: { operator: 'contains', showOperators: false } }
                },
                {
                    field: "createDate", width: "130px", title: "Ngày tạo", filterable: false, template: "#= kendo.toString(kendo.parseDate(createDate, 'yyyy-MM-dd'), 'dd/MM/yyyy')#",
                    filterable: { cell: { template: function (e) { e.element.kendoDatePicker({ format: 'dd/MM/yyyy' }); }, operator: 'contains', showOperators: false } },
                },
                 {
                     field: "modifiedByDate", width: "130px", title: "Ngày sửa", filterable: false, template: "#= kendo.toString(kendo.parseDate(modifiedByDate, 'yyyy-MM-dd'), 'dd/MM/yyyy')#",
                     filterable: { cell: { template: function (e) { e.element.kendoDatePicker({ format: 'dd/MM/yyyy' }); }, operator: 'contains', showOperators: false } },
                 },
                {
                    field: "status", width: "130px", title: "Trạng thái", editable: false, filterable: false,
                    template: kendo.template($("#statusFOOTERTpl").html())
                }
        ],
        detailTemplate: kendo.template($("#detailFOOTERTpl").html()),
        dataSource: {
            pageSize: 10,
            transport: {
                read: function (e) {
                    $http.post("/Pn/Content/FOOTER_Read")
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
                model: {
                    id: "ID",
                    fields: {
                        ID: { type: 'number', editable: false, nullable: true },
                        createByID: { type: "string", editable: false },
                        detail: { type: "string" },
                        satus: { type: "boolean" }
                    }
                }
            }
        }
    }

    $scope.ChangeStatus_FOOTER = function ($event, id) {
        $event.preventDefault();
        $rootScope.IsLoading = true;
        $http.post("/Pn/Content/FOOTER_ChangeStatus", { id: id }).then(function success(res) {
            if (res.data.status == true) {
                $rootScope.IsLoading = false;
                $scope.FOOTER_grid.dataSource.read();
                toastr.success('Thành công', '');
            }
        })
    }
    $scope.FOOTER_SaveClick = function ($event, detail) {
        $event.preventDefault();
        $rootScope.IsLoading = true;
        $http.post("/Pn/Content/FOOTER_Save", { detail: detail }).then(function success(res) {
            if (res.data.status == true) {
                $rootScope.IsLoading = false;
                $scope.FOOTER_grid.dataSource.read();
                toastr.success(res.data.msg, '');
            } else {
                toastr.error(res.data.msg, '');
            }
        })
    }

}]);