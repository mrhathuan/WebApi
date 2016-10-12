/// <reference path="../../angular.min.js" />

'use strict'

app.controller('ORD_IndexCtr', ['$http', '$scope', '$rootScope', function ($http, $scope, $rootScope) {
    $rootScope.title = 'Quản lý đơn hàng';
    $scope.Item = null;
    $scope.OrderID = 0;
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
                    field: "payID", width: "300px", title: "Phương thức thanh toán", filterable: false, template: "#=Pay.name#"                   
                },
                {
                    field: "totalAmount", width: "150px", title: "Tổng tiền", filterable: false, template: "#:kendo.toString(totalAmount,'n0')#"
                },
                {
                    field: "status", width: "130px", title: "Trạng thái", editable: false, filterable: false,
                    template: kendo.template($('#statusTpl').html())
                },
                {
                    field: "Payment", width: "130px", title: "Tình trạng", editable: false, filterable: false,
                    template: kendo.template($('#paymentTpl').html())
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
                        Payment: { type: "boolean" },
                        totalAmount:{type:"number"}
                    }
                }
            }
        }
    }

    $scope.RemoveItem = function ($event, id) {
        $event.preventDefault();
        $http.post("/Pn/Pn/ORD_Remove", { id: id }).then(function success(res) {
            if (res.data.status == true) {
                $scope.Ord_Grid.dataSource.read();
                $scope.Ord_Grid.refresh();
                toastr.success('Thành công', '');
            }
        })
    }


    $scope.ORDDetail_WinClick = function ($event,win, id) {
        $event.preventDefault();
        if (id > 0) {
            $http.post("/Pn/Pn/ORD_ChangeStatus", { id: id }).then(function success(res) {
                if (res.data.status == true) {
                    $scope.OrderID = id;
                    win.center();
                    win.open();
                    $scope.ORDDetail_grid.dataSource.read();
                    $scope.ORDDetail_grid.refresh();
                }
            })           
        }       
    }

    $scope.ORDDetail_gridOptions = {
        height: '100%', pageable: true, autoSync: true, sortable: true, columnMenu: false, resizable: true, reorderable: true, filterable: { mode: 'row' },        
        columns: [                
                {
                    field: "orderID", width: "130px", title: "Mã đơn hàng", editable: false,
                    filterable: { cell: { showOperators: false } }
                },
                {
                    field: "productCode", width: "130px", title: "Mã sản phẩm",
                    filterable: { cell: { operator: 'contains', showOperators: false } }
                },
                {
                    field: "productName", width: "400px", title: "Tên sản phẩm",
                    filterable: { cell: { operator: 'contains', showOperators: false } }
                },
                {
                    field: "productPrice", width: "130px", title: "Giá sản phẩm", template: "#:kendo.toString(productPrice,'n0')#",
                    filterable: { cell: { showOperators: false } }
                },
                {
                    field: "quantity", width: "100px", title: "Số lượng",
                    filterable: { cell: { showOperators: false } }
                },
                {
                    field: "Amount", width: "150px", title: "Thành tiền", filterable: false, template: "#:kendo.toString(Amount,'n0')#",
                    filterable: { cell: { showOperators: false } }
                }                            
        ],
        dataSource: {
            pageSize: 10,
            readparam: function () {
                return {
                    id: $scope.OrderID
                }
            },
            transport: {
                read: function (e) {
                    $http.post("/Pn/Pn/ORD_Detail", { id: $scope.OrderID })
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
                    id: "orderID",
                    fields: {
                        orderID: { type: 'number', editable: false, nullable: true },
                        productCode: { type: "string", editable: false },
                        productName: { type: "string", editable: false },
                        productPrice: { type: "number" },
                        quantity: { type: "number" },
                        Amount: { type: "number" }                       
                    }
                }
            }
        }
    }


    $scope.ORD_ChangePayment = function (id) {
        $http.post("/Pn/Pn/ORD_ChangePayment", { id: id }).then(function success(res) {
            $scope.Ord_Grid.dataSource.read();
            toastr.success('Duyệt đơn hàng thành công', '');
        })
    }

    $scope.Close_Click = function ($event, win) {
        $event.preventDefault();
        win.close();
    }

}]);