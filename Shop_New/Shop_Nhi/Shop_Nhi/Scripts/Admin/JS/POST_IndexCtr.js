/// <reference path="../../angular.min.js" />

'use strict'

app.controller('POST_IndexCtr', ['$http', '$scope', '$rootScope', function ($http, $scope, $rootScope) {
    $rootScope.title = 'Quản lý bài viết';
    $scope.Item = null;

    $scope.User_gridOptions = {
        height: 550, pageable: true, autoSync: true, sortable: true, columnMenu: false, resizable: true, reorderable: true, filterable: { mode: 'row' },
        toolbar: kendo.template($('#PostGridTemplate').html()),
        excel: {
            fileName: "Bai_Viet.xlsx",
            filterable: true,
            //proxyURL: "/Pn/Pn/PRO_Excel_Export_Save",
            allPages: true
        },
        columns: [
                 {
                     title: ' ', width: '110px',
                     template: '<a href="\\#" class="event-button" ng-click="POST_WinClick($event,#=id#)"><i class="fa fa-pencil"></i></a>' +
                         '<a href="\\#" class="event-button" ng-click="RemoveItem($event,#=id#)"><i class="fa fa-trash"></i></a>',
                     filterable: false, sortable: false
                 },
                {
                    field: "userName", width: "300px", title: "Tài khoản", editable: false,
                    filterable: { cell: { operator: 'contains', showOperators: false } }
                },
                {
                    field: "fullName", width: "300px", title: "Họ tên",
                    filterable: { cell: { operator: 'contains', showOperators: false } }
                },
                {
                    field: "email", width: "300px", title: "Email",
                    filterable: { cell: { operator: 'contains', showOperators: false } }
                },
                {
                    field: "Role", width: "130px", title: "Quyền", editable: false, filterable: false,
                    template: "#=Role.Name#"
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
                    $http.post("/Pn/Users/USER_Read")
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
                        fullName: { type: "string", editable: false },
                        email: { type: "string", editable: false },
                        userName: { type: "string" },
                        status: { type: "boolean" }
                    }
                }
            }
        }
    }

    $scope.RemoveItem = function ($event, id) {
        $event.preventDefault();
        $http.post("/Pn/Users/USER_Delete", { id: id }).then(function success(res) {
            if (res.data.status == true) {
                $scope.User_Grid.dataSource.read();
                $scope.User_Grid.refresh();
                toastr.success('Thành công', '');
            }
        })
    }

    $scope.Role_drdlOptions = {
        dataTextField: "Name",
        dataValueField: "ID",
        autoBind: false,
        dataSource: {
            severFiltering: true,
            transport: {
                read: {
                    url: "/Pn/Users/GET_Role",
                    contentType: "application/json",
                    type: "GET"
                }

            }
        }, change: function (e) {

        }
    }

    $scope.ChangeStatus = function ($event, id) {
        $event.preventDefault();
        $http.post("/Pn/Users/USER_ChangeStatus", { id: id }).then(function success(res) {
            $scope.User_Grid.dataSource.read();
            toastr.success('Thành công', '');
        })
    }

    $scope.USER_WinClick = function ($event, id) {
        $event.preventDefault();
        $http.post("/Pn/Users/USER_Get", { id: id }).then(function success(res) {
            $scope.Item = res.data.user;
            $scope.showModal_User = true;
        })
    }

    $scope.USER_SaveClick = function ($event, vform) {
        $event.preventDefault();
        vform({ clear: true });
        if (vform()) {
            $http.post("/Pn/Users/USER_Save", { item: JSON.stringify($scope.Item) }).then(function success(res) {
                if (res.data.status == true) {
                    vform({ clear: true });
                    $scope.User_Grid.dataSource.read();
                    toastr.success(res.data.msg, '');
                    $scope.showModal_User = false;
                } else {
                    toastr.error(res.data.msg, '');
                }
            })
        }
    }

}]);