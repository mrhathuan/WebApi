'use strict'

app.controller('PRO_IndexCtr', ['$http', '$scope', function ($http, $scope) {
    //$http.post("/Pn/Pn/PRO_Read").then(function (response) {
    //    debugger
    //    $scope.data = response.data;
    //});
    var product = new kendo.data.DataSource({
        data: $scope.data       
    });

    $scope.Pro_gridOptions = {
          columns: [ { field: "ID" }, { field: "productName" } ],
          pageable: true,
          dataSource: {
            pageSize: 5,
            transport: {
              read: function (e) {
                  $http.post("/Pn/Pn/PRO_Read")
                  .then(function (response) {
                      debugger
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
                        image: { type: "string", editable: false, filterable: false },
                        code: { type: "string", editable: false },
                        productName: { type: "string", editable: false },
                        price: { type: "number", format: "n0", editable: false },
                        quantity: { type: "number" },
                        categoryID: { type: "number" },
                        status: { type: "boolean" },
                        createDate: { editable: false, type: "date" },
                        like: { editable: false },
                        viewCount: { editable: false }
                    }
                }
            }
          }
        }
}]);