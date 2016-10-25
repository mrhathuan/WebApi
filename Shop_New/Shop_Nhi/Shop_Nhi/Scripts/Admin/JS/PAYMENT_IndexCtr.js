/// <reference path="../../angular.min.js" />

'use strict'

app.controller('PAYMENT_IndexCtr', ['$http', '$scope', '$rootScope', function ($http, $scope, $rootScope) {
    $rootScope.title = 'Quản lý phương thức thanh toán';
    $scope.Item = null;
    $scope.PAYMENT_gridOptions = {
        height: 550, pageable: true, autoSync: true, sortable: true, columnMenu: false, resizable: true, reorderable: true, filterable: { mode: 'row' },
        toolbar: kendo.template($('#PAYMENTGridTemplate').html()),
        columns: [
                {
                    title: ' ', width: '110px',
                    template: '<a href="\\#" class="event-button" ng-click="PAYMENT_WinClick($event,#=id#)"><i class="fa fa-pencil"></i></a>' +
                        '<a href="\\#" class="event-button" ng-click="RemoveItem($event,#=id#)"><i class="fa fa-trash"></i></a>',
                    filterable: false, sortable: false
                },
                {
                    field: "ID", width: "100px", title: "Mã", editable: false,
                    filterable: { cell: { showOperators: false } }
                },
                {
                    field: "name", width: "400px", title: "Tên",
                    filterable: { cell: { operator: 'contains', showOperators: false } }
                }                
        ],
        dataSource: {
            pageSize: 10,
            transport: {
                read: function (e) {
                    $http.post("/Pn/Pn/PAYMENT_Read")
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
                        name: { type: "string", editable: false }                      
                    }
                }
            }
        }
    }

    $scope.RemoveItem = function ($event, id) {
        $event.preventDefault();
        var cf = confirm('Bạn chắc chắn muốn xóa?');
        if (cf) {
            $http.post("/Pn/Pn/PAYMENT_Remove", { id: id }).then(function success(res) {
                if (res.data.status == true) {
                    $scope.PAYMENT_Grid.dataSource.read();
                    toastr.success(res.data.msg, '');
                } else {
                    toastr.error(res.data.msg, '');
                }
            })
        }

    }


    $scope.PAYMENT_WinClick = function ($event, id) {
        $event.preventDefault();
        $rootScope.IsLoading = true;
        $http.post("/Pn/Pn/PAYMENT_Get", { id: id }).then(function success(res) {
            $rootScope.IsLoading = false;
            $scope.Item = res.data.pay;
            $scope.showModal_PAYMENT = true;
        });
    }

    $scope.PAYMENT_SaveClick = function ($event, vform) {
        $event.preventDefault();
        vform({ clear: true });
        if (vform()) {
            $rootScope.IsLoading = true;
            $http.post("/Pn/Pn/PAYMENT_Save", { item: JSON.stringify($scope.Item) }).then(function success(res) {
                $rootScope.IsLoading = false;
                if (res.data.status == true) {
                    vform({ clear: true });
                    $scope.PAYMENT_Grid.dataSource.read();
                    toastr.success(res.data.msg, '');
                    $scope.showModal_PAYMENT = false;
                } else {
                    toastr.error(res.data.msg, '');
                }

            })
        }
    }
  

}]);