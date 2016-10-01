'use strict'

app.controller('PRO_IndexCtr', ['$http', '$scope', function ($http, $scope) {

    $scope.Pro_gridOptions = {
        height: 500, pageable: true, autoSync: true, sortable: true, columnMenu: false, resizable: true, reorderable: true, filterable: { mode: 'row' },
        excel: {
            fileName: "San_pham.xlsx",
            filterable: true,
            proxyURL: "/Pn/Pn/Excel_Export_Save",
            allPages: true
        },
        columns: [
                {
                    title: ' ', width: '130px',
                    template: '<a href="\\#" class="event-button" ng-click="PRO_WinClick($event,PRO_Win)"><i class="fa fa-pencil"></i></a>' +
                        '<a href="\\#" class="event-button" onclick="RemoveItem(#=id#)"><i class="fa fa-trash"></i></a>' +
                        '<a href="\\#" data-id="#=id#" onclick="ManageImages(#=id#)" class="event-button btn-image"><i class="fa fa-picture-o"></i></a>',
                    filterable: false, sortable: false
                },
                { field: "image", width: "60px", title: "Ảnh", editable: false, filterable: false, template: "<img src='#=image#' class='product-img'/>" },
                {
                    field: "code", width: "130px", title: "Mã",
                    filterable: { cell: { operator: 'contains', showOperators: false } }
                },
                {
                    field: "productName", width: "400px", title: "Tên sản phẩm",
                    filterable: { cell: { operator: 'contains', showOperators: false } }
                },
                {
                    field: "price", width: "130px", title: "Giá", template: "#:kendo.toString(price,'n0')#",
                    filterable: { cell: { showOperators: false } }
                },
                {
                    field: "promotionPrice", width: "130px", title: "Giá KM", template: "#:kendo.toString(promotionPrice,'n0')#",
                    filterable: { cell: { showOperators: false } }
                },
                {
                    field: "quantity", width: "130px", title: "Số lượng",
                    filterable: { cell: { showOperators: false } }
                },
                {
                    field: "categoryID", width: "300px", title: "Danh mục", template: "#=Category.name#",
                    filterable: false
                },
                { field: "status", width: "130px", title: "Trạng thái", editable: false, filterable: false, template: "#=status#" },
                { field: "createDate", width: "130px", title: "Ngày up", filterable: false, template: "#= kendo.toString(kendo.parseDate(createDate, 'yyyy-MM-dd'), 'dd/MM/yyyy')#" },
                { field: "like", width: "100px", filterable: false, title: "Like" },
                { field: "viewCount", width: "130px", filterable: false, title: "Lượt mua" },
        ],
          dataSource: {
            pageSize: 10,
            transport: {
              read: function (e) {
                  $http.post("/Pn/Pn/PRO_Read")
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
    $scope.Pro_GridTemplate = $('#ProGridTemplate').html();

    $scope.DropD_PROCateOptions={
        dataTextField: "name",
        dataValueField: "ID",
        autoBind: true,
        optionLabel: "Tất cả",
        dataSource: {
            type: "Data",
            severFiltering: true,
            transport: {
                read: {
                    url: "/Pn/Pn/PRO_Categories_Filter",
                    contentType: "application/json",
                    type: "GET"
            }

        }
    },
    change: function () {
        var value = this.value();
        if (value) {
            $scope.Pro_Grid.dataSource.filter({ field: "categoryID", operator: "eq", value: parseInt(value) });
        } else {
            $scope.Pro_Grid.dataSource.filter({});
        }
        }
    }

    $scope.numFeeBase_options = { format: 'n0', spinners: false, culture: 'en-US', min: 0, step: 0.01 }
    $scope.editor_DetailOptions = {
        language: 'en'
    }
    //win
    $scope.PRO_WinClick = function ($event, win) {
        $event.preventDefault();
        win.center();
        win.open()
    }

    $scope.Close_Click = function ($event, win) {
        $event.preventDefault();
        win.close();
    }
}]);