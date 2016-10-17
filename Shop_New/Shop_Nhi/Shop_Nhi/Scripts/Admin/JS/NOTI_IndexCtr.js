/// <reference path="../../angular.min.js" />

'use strict'

app.controller('NOTI_IndexCtr', ['$http', '$scope', '$rootScope', function ($http, $scope, $rootScope) {
    $rootScope.title = 'Quản trị thông báo';
    $scope.Item = null;
    $scope.Back_Click = function ($event) {
        $event.preventDefault();
        window.location.href = '/Pn/#/CONTENT_Index';
    }

    $scope.NOTI_gridOptions = {
        height: '500px', pageable: true, autoSync: true, sortable: true, columnMenu: false, resizable: true, reorderable: true, filterable: { mode: 'row' },
        toolbar: kendo.template($('#NOTIGridTemplate').html()),
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
                    field: "satus", width: "130px", title: "Trạng thái", editable: false, filterable: false,
                    template: kendo.template($("#statusNotiTpl").html())
                }
        ],
        detailTemplate: kendo.template($("#detailNotiTpl").html()),
        dataSource: {
            pageSize: 10,
            transport: {
                read: function (e) {
                    $http.post("/Pn/Content/NOTI_Read")
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

    $scope.ChangeStatus_NOTI = function ($event, id) {
        $event.preventDefault();
        $rootScope.IsLoading = true;
        $http.post("/Pn/Content/NOTI_ChangeStatus", { id: id }).then(function success(res) {
            if (res.data.status == true) {
                $rootScope.IsLoading = false;
                $scope.NOTI_grid.dataSource.read();
                toastr.success('Thành công', '');
            }
        })
    }
    $scope.NOTI_SaveClick = function ($event, detail) {
        $event.preventDefault();
        $rootScope.IsLoading = true;
        $http.post("/Pn/Content/NOTI_Save", { detail: detail }).then(function success(res) {
            if (res.data.status == true) {
                $rootScope.IsLoading = false;
                $scope.NOTI_grid.dataSource.read();
                toastr.success(res.data.msg, '');
            } else {
                toastr.error(res.data.msg, '');
            }
        })
    }

}]);