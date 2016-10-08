﻿/// <reference path="../../angular.min.js" />

'use strict'

app.controller('CAT_IndexCtr', ['$http', '$scope', '$rootScope', function ($http, $scope, $rootScope) {
    $rootScope.title = 'Quản lý danh mục';
    $scope.Item = null;
    $scope.showModal = false;
    // $scope.images = [];
    $scope.Cat_gridOptions = {
        height: 550, pageable: true, autoSync: true, sortable: true, columnMenu: false, resizable: true, reorderable: true, filterable: { mode: 'row' },
        toolbar: kendo.template($('#CatGridTemplate').html()),
        excel: {
            fileName: "Danh_muc.xlsx",
            filterable: true,
            //proxyURL: "/Pn/Pn/PRO_Excel_Export_Save",
            allPages: true
        },
        columns: [
                {
                    title: ' ', width: '110px',
                    template: '<a href="\\#" class="event-button" ng-click="CAT_WinClick($event,#=id#)"><i class="fa fa-pencil"></i></a>' +
                        '<a href="\\#" class="event-button" ng-click="RemoveItem($event,#=id#)"><i class="fa fa-trash"></i></a>',                        
                    filterable: false, sortable: false
                },
                { field: "ID", width: "60px", title: "Mã", editable: false, filterable: false},
                {
                    field: "name", width: "250px", title: "Tên",
                    filterable: { cell: { operator: 'contains', showOperators: false } }
                },
                {
                    field: "parentID", width: "60px", title: "Thuộc",
                    filterable: { cell: { showOperators: false } }
                },
                {
                    field: "status", width: "130px", title: "Trạng thái", editable: false, filterable: false
                },
                {
                    field: "showOnHome", width: "130px", title: "Hiện Menu", editable: false, filterable: false
                },
                {
                    field: "createDate", width: "130px", title: "Ngày up", filterable: false, template: "#= kendo.toString(kendo.parseDate(createDate, 'yyyy-MM-dd'), 'dd/MM/yyyy')#",
                    filterable: { cell: { template: function (e) { e.element.kendoDatePicker({ format: 'dd/MM/yyyy' }); }, operator: 'equal', showOperators: false } },
                },
                {
                    field: "createByID", width: "250px", title: "Người up",
                    filterable: { cell: { showOperators: false } }
                },
                 {
                     field: "modifiedByDate", width: "130px", title: "Ngày sửa", filterable: false, template: "#= kendo.toString(kendo.parseDate(modifiedByDate, 'yyyy-MM-dd'), 'dd/MM/yyyy')#",
                     filterable: { cell: { template: function (e) { e.element.kendoDatePicker({ format: 'dd/MM/yyyy' }); }, operator: 'equal', showOperators: false } },
                 },
                 {
                     field: "modifiedByID", width: "250px", title: "Người sửa",
                     filterable: { cell: { showOperators: false } }
                 }
                
        ],
        dataSource: {
            pageSize: 10,
            transport: {
                read: function (e) {
                    $http.post("/Pn/Pn/CAT_Read")
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
                        name: { type: "string", editable: false, filterable: false },
                        parentID: { type: "number", editable: false },                     
                        createDate: { type: "date" },
                        modifiedByDate: { type: "date" },
                        status: { type: "boolean" },
                        showOnHome:{type:"boolean"}
                    }
                }
            }
        }
    }
  
}]);