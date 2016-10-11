/// <reference path="../../angular.min.js" />

'use strict'

app.controller('ORD_IndexCtr', ['$http', '$scope', '$rootScope', function ($http, $scope, $rootScope) {
    $rootScope.title = 'Quản lý đơn hàng';
    $scope.Item = null;
    $scope.showModal = false;
    // $scope.images = [];
    $scope.Ord_gridOptions = {
        height: 550, pageable: true, autoSync: true, sortable: true, columnMenu: false, resizable: true, reorderable: true, filterable: { mode: 'row' },
        toolbar: kendo.template($('#ORDGridTemplate').html()),
        excel: {
            fileName: "Don_Hang.xlsx",
            filterable: true,
            //proxyURL: "/Pn/Pn/PRO_Excel_Export_Save",
            allPages: true
        },
        columns: [
                {
                    title: ' ', width: '110px',
                    template: '<a href="\\#" class="event-button" ng-click="ORDDetail_WinClick($event,ORDDetail_win,#=id#)"><i class="fa fa-eye"></i></a>' +
                        '<a href="\\#" class="event-button" ng-click="RemoveItem($event,#=id#)"><i class="fa fa-trash"></i></a>',
                    filterable: false, sortable: false
                },
                {
                    field: "ID", width: "100px", title: "Mã", editable: false,
                    filterable: { cell: { showOperators: false } }
                },
                {
                    field: "fullName", width: "300px", title: "Khách hàng",
                    filterable: { cell: { operator: 'contains', showOperators: false } }
                },
                {
                    field: "email", width: "300px", title: "Email",
                    filterable: { cell: {operator: 'contains', showOperators: false } }
                },
                {
                    field: "phone", width: "100px", title: "Điện thoại",
                    filterable: { cell: { operator: 'contains', showOperators: false } }
                },
                {
                    field: "address", width: "400px", title: "Địa chỉ",
                    filterable: { cell: { operator: 'contains', showOperators: false } }
                },
                {
                    field: "dateSet", width: "130px", title: "Ngày đặt", filterable: false, template: "#= kendo.toString(kendo.parseDate(dateSet, 'yyyy-MM-dd'), 'dd/MM/yyyy')#",
                    filterable: { cell: { template: function (e) { e.element.kendoDatePicker({ format: 'dd/MM/yyyy' }); }, operator: 'equal', showOperators: false } },
                },
                {
                    field: "status", width: "130px", title: "Trạng thái", editable: false, filterable: false,
                    template: kendo.template($('#statusTpl').html())
                },
                {
                    field: "Payment", width: "130px", title: "Tình trạng", editable: false, filterable: false                    
                }
        ],
        dataSource: {
            pageSize: 10,
            transport: {
                read: function (e) {
                    $http.post("/Pn/Pn/ORD_Read")
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
                        dateSet: { type: "date" },
                        phone: { type: "string" },
                        status: { type: "boolean" },
                        Payment: { type: "boolean" }
                    }
                }
            }
        }
    }

    $scope.RemoveItem = function ($event, id) {
        $event.preventDefault();
        $http.post("/Pn/Pn/CAT_Remove", { id: id }).then(function success(res) {
            if (res.data.status == true) {
                $scope.Cat_Grid.dataSource.read();
                toastr.success('Thành công', '');
            }
        })
    }


    $scope.ORDDetail_WinClick = function ($event,win, id) {
        $event.preventDefault();
        $scope.orderID = id;
        win.center();
        win.open();
        $scope.ORDDetail_gridOptions.dataSource.read();
        $scope.AreaDetail_grid.refresh();
    }

    $scope.CAT_SaveClick = function ($event, vform) {
        $event.preventDefault();
        vform({ clear: true });
        if (vform()) {
            $http.post("/Pn/Pn/CAT_Save", { item: JSON.stringify($scope.Item) }).then(function success(res) {
                if (res.data.status == true) {
                    vform({ clear: true });
                    $scope.Cat_Grid.dataSource.read();
                    toastr.success(res.data.msg, '');
                    $scope.showModal_Cat = false;
                } else {
                    toastr.error(res.data.msg, '');
                }

            })
        }
    }

    $scope.ChangeStatus = function ($event, id) {
        $event.preventDefault();
        $http.post("/Pn/Pn/CAT_ChangeStatus", { id: id }).then(function success(res) {
            $scope.Cat_Grid.dataSource.read();
            toastr.success('Thành công', '');
        })
    }

    $scope.ChangeShowhome = function ($event, id) {
        $event.preventDefault();
        $http.post("/Pn/Pn/CAT_ShowHome", { id: id }).then(function success(res) {
            $scope.Cat_Grid.dataSource.read();
            toastr.success('Thành công', '');
        })
    }

}]);