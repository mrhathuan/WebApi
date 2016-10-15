/// <reference path="../../angular.min.js" />

'use strict'

app.controller('CONTACT_IndexCtr', ['$http', '$scope', '$rootScope', function ($http, $scope, $rootScope,$location) {
    $rootScope.title = 'Trang liên hệ';
    $scope.Item = null;

    $scope.Back_Click = function ($event) {
        $event.preventDefault();
        window.location.href = '/Pn/#/CONTENT_Index';
    }

    $scope.Contact_gridOptions = {
        height: 550, pageable: true, autoSync: true, sortable: true, columnMenu: false, resizable: true, reorderable: true, filterable: { mode: 'row' },
        toolbar: kendo.template($('#CONTACTGridTemplate').html()),
        excel: {
            fileName: "Bai_Viet.xlsx",
            filterable: true,
            //proxyURL: "/Pn/Pn/PRO_Excel_Export_Save",
            allPages: true
        },
        columns: [
                 {
                     title: ' ', width: '110px',
                     template: '<a href="\\#" class="event-button" ng-click="CONTACT_WinClick($event,#=id#)"><i class="fa fa-pencil"></i></a>',                         
                     filterable: false, sortable: false
                 },
                 {
                     field: "ID", width: "100px", title: "Mã",
                     filterable: { cell: { operator: 'eq', showOperators: false } }
                 },
                {
                    field: "name", width: "300px", title: "Tên",
                    filterable: { cell: { operator: 'contains', showOperators: false } }
                },
                {
                    field: "map", width: "400px", title: "Link bản đồ",
                    filterable: { cell: { operator: 'contains', showOperators: false } }
                },
                 {
                     field: "metatTitle", width: "400px", title: "metaTitle",
                     filterable: { cell: { operator: 'contains', showOperators: false } }
                 },
                {
                    field: "createByID", width: "200px", title: "Người up", editable: false, filterable: false,
                    filterable: { cell: { operator: 'contains', showOperators: false } }
                },
                {
                    field: "createDate", width: "130px", title: "Ngày up", filterable: false, template: "#= kendo.toString(kendo.parseDate(createDate, 'yyyy-MM-dd'), 'dd/MM/yyyy')#",
                    filterable: { cell: { template: function (e) { e.element.kendoDatePicker({ format: 'dd/MM/yyyy' }); }, operator: 'contains', showOperators: false } },
                },
                 {
                     field: "modifiedByID", width: "200px", title: "Người sửa", editable: false, filterable: false,
                     filterable: { cell: { operator: 'contains', showOperators: false } }
                 },
                 {
                     field: "modifiedByDate", width: "130px", title: "Ngày sửa", filterable: false, template: "#= kendo.toString(kendo.parseDate(modifiedByDate, 'yyyy-MM-dd'), 'dd/MM/yyyy')#",
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
                    $http.post("/Pn/Contact/CONTACT_Read")
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
                        tag: { type: "string" }
                    }
                }
            }
        }
    }

  
    $scope.ChangeStatus = function ($event, id) {
        $event.preventDefault();
        $rootScope.IsLoading = true;
        $http.post("/Pn/contact/CONTACT_ChangeStatus", { id: id }).then(function success(res) {
            if (res.data.status == true) {
                $rootScope.IsLoading = false;
                $scope.CONTACT_Grid.dataSource.read();
                toastr.success('Thành công', '');
            }
        })
    }

    $scope.CONTACT_WinClick = function ($event, id) {
        $event.preventDefault();
        $rootScope.IsLoading = true;
        $http.post("/Pn/contact/CONTACT_Get", { id: id }).then(function success(res) {
            $rootScope.IsLoading = false;
            $scope.Item = res.data.contact;
            $scope.showModal_CONTACT = true;
        })
    }


    $scope.CONTACT_SaveClick = function ($event, vform) {
        $event.preventDefault();
        vform({ clear: true });
        if (vform()) {
            $rootScope.IsLoading = true;
            $http.post("/Pn/contact/CONTACT_Save", { item: JSON.stringify($scope.Item) }).then(function success(res) {
                $rootScope.IsLoading = false;
                if (res.data.status == true) {
                    vform({ clear: true });
                    $scope.CONTACT_Grid.dataSource.read();
                    toastr.success(res.data.msg, '');
                    $scope.showModal_CONTACT = false;
                } else {
                    toastr.error(res.data.msg, '');
                }
            })
        }
    }

}]);