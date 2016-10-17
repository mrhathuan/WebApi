/// <reference path="../../angular.min.js" />

'use strict'

app.controller('SEO_IndexCtr', ['$http', '$scope', '$rootScope', function ($http, $scope, $rootScope) {
    $rootScope.title = 'Quản trị SEO';
    $scope.Item = null;
    $scope.BACK_Click = function ($event) {
        $event.preventDefault();
        window.location.href = '/Pn/#/CONTENT_Index';
    }

    $scope.SEO_gridOptions = {
        height: '500px', pageable: true, autoSync: true, sortable: true, columnMenu: false, resizable: true, reorderable: true, filterable: { mode: 'row' },
        toolbar: kendo.template($('#SEOGridTemplate').html()),
        columns: [
                   {
                       title: ' ', width: '110px',
                       template: '<a href="\\#" class="event-button" ng-click="SEO_WinClick($event,#=id#)"><i class="fa fa-pencil"></i></a>',                          
                       filterable: false, sortable: false
                   },
                 {
                     field: "metaTitle", width: "300px", title: "Tiêu đề",
                     filterable: { cell: {operator: 'contains', showOperators: false } }
                 },
                  {
                      field: "metaKeyword", width: "300px", title: "Từ khóa",
                      filterable: { cell: { operator: 'contains', showOperators: false } }
                  },
                   {
                       field: "metaDescription", width: "300px", title: "Mô tả",
                       filterable: { cell: { operator: 'contains', showOperators: false } }
                   },
                
        ],      
        dataSource: {
            pageSize: 10,
            transport: {
                read: function (e) {
                    $http.post("/Pn/Content/SEO_Read")
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
                        ID: { type: 'number', editable: false, nullable: true }
                    }
                }
            }
        }
    }


    $scope.SEO_WinClick = function ($event, id) {
        $event.preventDefault();
        $rootScope.IsLoading = true;
        $http.post("/Pn/Content/SEO_Get", { id: id }).then(function success(res) {
            $rootScope.IsLoading = false;
            $scope.Item = res.data.seo;
            $scope.showModal_SEO = true;
        })
    }

    $scope.SEO_SaveClick = function ($event) {
        $event.preventDefault();
        $rootScope.IsLoading = true;
        $http.post("/Pn/Content/SEO_Save", { item: JSON.stringify($scope.Item) }).then(function success(res) {
            if (res.data.status == true) {
                $rootScope.IsLoading = false;
                $scope.SEO_Grid.dataSource.read();
                toastr.success(res.data.msg, '');
                $scope.showModal_SEO = false;
            } else {
                toastr.error(res.data.msg, '');
            }
        })
    }

}]);