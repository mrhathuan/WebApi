/// <reference path="../../angular.min.js" />

'use strict'

app.controller('MENU_IndexCtr', ['$http', '$scope', '$rootScope', function ($http, $scope, $rootScope) {
    $rootScope.title = 'Quản trị Menu';
    $scope.Item = null;

    $scope.MENU_gridOptions = {
        height: 550, pageable: true, autoSync: true, sortable: true, columnMenu: false, resizable: true, reorderable: true, filterable: { mode: 'row' },
        toolbar: kendo.template($('#MENUGridTemplate').html()),
        excel: {
            fileName: "Menu.xlsx",
            filterable: true,
            //proxyURL: "/Pn/Pn/PRO_Excel_Export_Save",
            allPages: true
        },
        columns: [
                 {
                     title: ' ', width: '110px',
                     template: '<a href="\\#" class="event-button" ng-click="MENU_WinClick($event,#=id#)"><i class="fa fa-pencil"></i></a>' +
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
                    field: "typeID", width: "100px", title: "Thuộc",template:"#=MenuType.name#",
                    filterable: { cell: { operator: 'contains', showOperators: false } }
                },
                 {
                     field: "dislayOrder", width: "100px", title: "Thứ tự",
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
                    $http.post("/Pn/Menu/MENU_Read")
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
        var cf = confirm('Bạn chắc chắn muốn xóa Menu này?');
        if (cf) {
            $http.post("/Pn/Menu/MENU_Delete", { id: id }).then(function success(res) {
                if (res.data.status == true) {
                    $scope.MENU_Grid.dataSource.read();
                    $scope.MENU_Grid.refresh();
                    toastr.success('Thành công', '');
                }
            })
        }      
    }

    $scope.BACK_Click = function ($event) {
        $event.preventDefault();
        window.location.href = '/Pn/#/CONTENT_Index';
    }

    $scope.Type_drdlOptions = {
        dataTextField: "name",
        dataValueField: "ID",
        autoBind: false,
        dataSource: {
            severFiltering: true,
            transport: {
                read: {
                    url: "/Pn/Menu/GET_Type",
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
        $http.post("/Pn/Menu/MENU_ChangeStatus", { id: id }).then(function success(res) {
            $scope.MENU_Grid.dataSource.read();
            toastr.success('Thành công', '');
        })
    }

    $scope.MENU_WinClick = function ($event, id) {
        $event.preventDefault();
        $rootScope.IsLoading = true;
        $http.post("/Pn/Menu/MENU_Get", { id: id }).then(function success(res) {
            $rootScope.IsLoading = false;
            $scope.Item = res.data.menu;
            $scope.showModal_MENU = true;
        })
    }

    $scope.MENU_SaveClick = function ($event, vform) {
        $event.preventDefault();
        vform({ clear: true });
        if (vform()) {
            $http.post("/Pn/Menu/MENU_Save", { item: JSON.stringify($scope.Item) }).then(function success(res) {
                if (res.data.status == true) {
                    vform({ clear: true });
                    $scope.MENU_Grid.dataSource.read();
                    toastr.success(res.data.msg, '');
                    $scope.showModal_MENU = false;
                } else {
                    toastr.error(res.data.msg, '');
                }
            })
        }
    }

}]);