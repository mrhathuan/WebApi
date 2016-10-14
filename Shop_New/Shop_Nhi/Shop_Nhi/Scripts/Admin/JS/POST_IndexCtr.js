/// <reference path="../../angular.min.js" />

'use strict'

app.controller('POST_IndexCtr', ['$http', '$scope', '$rootScope', function ($http, $scope, $rootScope) {
    $rootScope.title = 'Quản lý bài viết';
    $scope.Item = null;

    $scope.Post_gridOptions = {
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
                     field: "image", width: "60px", title: "Ảnh bài viết", editable: false, filterable: false,
                     template: "<img src='#=image#' class='product-img'/>"
                 },
                {
                    field: "name", width: "300px", title: "Tên bài viết",
                    filterable: { cell: { operator: 'contains', showOperators: false } }
                },
                {
                    field: "tag", width: "300px", title: "Tags",
                    filterable: { cell: { operator: 'contains', showOperators: false } }
                },
                {
                    field: "createByID", width: "200px", title: "Người up", editable: false, filterable: false,
                    filterable: { cell: { operator: 'contains', showOperators: false } }
                },
                {
                    field: "createDate", width: "130px", title: "Ngày up", filterable: false,
                    filterable: { cell: { template: function (e) { e.element.kendoDatePicker({ format: 'dd/MM/yyyy' }); }, operator: 'contains', showOperators: false } },
                },
                 {
                     field: "modifiedByID", width: "200px", title: "Người sửa", editable: false, filterable: false,
                     filterable: { cell: { operator: 'contains', showOperators: false } }
                 },
                 {
                     field: "modifiedByDate", width: "130px", title: "Ngày sửa", filterable: false,
                     filterable: { cell: { template: function (e) { e.element.kendoDatePicker({ format: 'dd/MM/yyyy' }); }, operator: 'contains', showOperators: false } },
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
                    $http.post("/Pn/Post/POST_Read")
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
                        name: { type: "string", editable: false },
                        createByID: { type: "string", editable: false },
                        tag:{type:"string"}
                    }
                }
            }
        }
    }

    $scope.RemoveItem = function ($event, id) {
        $event.preventDefault();
        $http.post("/Pn/Post/POST_Delete", { id: id }).then(function success(res) {
            if (res.data.status == true) {
                $scope.Post_Grid.dataSource.read();
                $scope.Post_Grid.refresh();
                toastr.success('Thành công', '');
            }
        })
    }

    $scope.ChangeStatus = function ($event, id) {
        $event.preventDefault();
        $rootScope.IsLoading = true;
        $http.post("/Pn/Post/POST_ChangeStatus", { id: id }).then(function success(res) {
            if (res.data.status == true) {
                $rootScope.IsLoading = false;
                $scope.Post_Grid.dataSource.read();
                toastr.success('Thành công', '');
            }
        })
    }

    $scope.POST_WinClick = function ($event, id) {
        $event.preventDefault();
        $rootScope.IsLoading = true;
        $http.post("/Pn/Post/POST_Get", { id: id }).then(function success(res) {
            $rootScope.IsLoading = false;
            $scope.Item = res.data.data;            
            $scope.showModal_POST = true;            
        })
    }

    $scope.ChooseImage = function ($event) {
        $event.preventDefault();
        var finder = new CKFinder();
        finder.selectActionFunction = function (url) {
            $('#image').val(url);
            $scope.Item.image = url;
        };
        finder.popup();

    }

    $scope.POST_SaveClick = function ($event, vform) {
        $event.preventDefault();
        vform({ clear: true });        
        if (vform()) {
            $rootScope.IsLoading = true;
            $http.post("/Pn/Post/POST_Save", { item: JSON.stringify($scope.Item) }).then(function success(res) {
                $rootScope.IsLoading = false;
                if (res.data.status == true) {
                    vform({ clear: true });
                    $scope.Post_Grid.dataSource.read();
                    toastr.success(res.data.msg, '');
                    $scope.showModal_POST = false;
                } else {
                    toastr.error(res.data.msg, '');
                }
            })
        }
    }

}]);