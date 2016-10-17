/// <reference path="../../angular.min.js" />

'use strict'

app.controller('SLIDE_IndexCtr', ['$http', '$scope', '$rootScope', function ($http, $scope, $rootScope) {
    $rootScope.title = 'Quản trị slide';
    $scope.Item = null;

    $scope.BACK_Click = function ($event) {
        $event.preventDefault();
        window.location.href = '/Pn/#/CONTENT_Index';
    }

    $scope.SLIDE_gridOptions = {
        height: '550px', pageable: true, autoSync: true, sortable: true, columnMenu: false, resizable: true, reorderable: true, filterable: { mode: 'row' },
        toolbar: kendo.template($('#SLIDEGridTemplate').html()),
        columns: [
             {
                 title: ' ', width: '110px',
                 template: '<a href="\\#" class="event-button" ng-click="SLIDE_WinClick($event,#=id#)"><i class="fa fa-pencil"></i></a>' +
                     '<a href="\\#" class="event-button" ng-click="RemoveItem($event,#=id#)"><i class="fa fa-trash"></i></a>',
                 filterable: false, sortable: false
             },
               { field: "image", width: "60px", title: "Ảnh", editable: false, filterable: false, template: "<img src='#=image#' class='product-img'/>" },
               {
                   field: "name", width: "300px", title: "Tên",
                   filterable: { cell: { operator: 'contains', showOperators: false } }
               },
               {
                   field: "dislayOrder", width: "100px", title: "Thứ tự",
                   filterable: { cell: { operator: 'contains', showOperators: false } }
               },
                {
                    field: "detail", width: "300px", title: "Chi tiết",
                    filterable: { cell: { operator: 'contains', showOperators: false } }
                },
                 {
                     field: "title", width: "300px", title: "Tiêu đề",
                     filterable: { cell: { operator: 'contains', showOperators: false } }
                 },
                  {
                      field: "status", width: "130px", title: "Trạng thái", editable: false, filterable: false,
                      template: kendo.template($('#statusTpl').html())
                  },
           
        ],
        dataSource: {
            pageSize: 10,
            transport: {
                read: function (e) {
                    $http.post("/Pn/Content/SLIDE_Read")
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
                        status: { type: "boolean" }
                    }
                }
            }
        }
    }

    $scope.RemoveItem = function ($event, id) {
        $event.preventDefault();
        var cf = confirm('Bạn chắc chắn muốn xóa?');
        if (cf) {
            $http.post("/Pn/Content/SLIDE_Delete", { id: id }).then(function success(res) {
                if (res.data.status == true) {
                    $scope.SLIDE_Grid.dataSource.read();
                    toastr.success('Thành công', '');
                }
            })
        }

    }

    $scope.numFeeBase_options = { format: 'n0', spinners: false, culture: 'en-US', min: 0, step: 0.01 };

    $scope.SLIDE_WinClick = function ($event, id) {
        $event.preventDefault();
        //vform({ clear: true });
        $rootScope.IsLoading = true;
        $http.post("/Pn/Content/SLIDE_Get", { id: id }).then(function success(response) {
            // vform({ clear: true });
            $rootScope.IsLoading = false;
            $scope.Item = response.data.slide;           
            $scope.showModal_SLIDE = true;
        })
    };

    $scope.ChooseImage = function ($event) {
        $event.preventDefault();
        var finder = new CKFinder();
        finder.selectActionFunction = function (url) {
            $('#image').val(url);
            $scope.Item.image = url;
        };
        finder.popup();

    }

    $scope.ChangeStatus = function ($event, id) {
        $event.preventDefault();
        $rootScope.IsLoading = true;
        $http.post("/Pn/Content/SLIDE_ChangeStatus", { id: id }).then(function success(res) {
            if (res.data.status == true) {
                $rootScope.IsLoading = false;
                $scope.SLIDE_Grid.dataSource.read();
                toastr.success('Thành công', '');
            }
        })
    }
    $scope.SLIDE_SaveClick = function ($event, vform) {
        $event.preventDefault();
        vform({ clear: true });
        if (vform()) {
            $rootScope.IsLoading = true;
            $http.post("/Pn/Content/SLIDE_Save", { item: JSON.stringify($scope.Item) }).then(function success(res) {
                vform({ clear: true });
                if (res.data.status == true) {
                    $rootScope.IsLoading = false;
                    $scope.SLIDE_Grid.dataSource.read();
                    toastr.success(res.data.msg, '');
                    $scope.showModal_SLIDE = false;
                } else {
                    toastr.error(res.data.msg, '');
                }
            })
        }
        
    }

}]);