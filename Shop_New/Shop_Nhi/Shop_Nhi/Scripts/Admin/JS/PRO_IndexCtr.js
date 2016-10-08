/// <reference path="../../angular.min.js" />

'use strict'

app.controller('PRO_IndexCtr', ['$http', '$scope', '$rootScope', function ($http, $scope, $rootScope) {
    $rootScope.title = 'Quản lý sản phẩm';
    $scope.Item = null;
    $scope.showModal = false;
   // $scope.images = [];
    $scope.Pro_gridOptions = {
        height: 550, pageable: true, autoSync: true, sortable: true, columnMenu: false, resizable: true, reorderable: true, filterable: { mode: 'row' },
        toolbar: kendo.template($('#ProGridTemplate').html()),
        excel: {
            fileName: "San_pham.xlsx",
            filterable: true,
            proxyURL: "/Pn/Pn/Excel_Export_Save",
            allPages: true
        },
        columns: [
                {
                    title: ' ', width: '130px',
                    template: '<a href="\\#" class="event-button" ng-click="PRO_WinClick($event,#=id#)"><i class="fa fa-pencil"></i></a>' +
                        '<a href="\\#" class="event-button" ng-click="RemoveItem($event,#=id#)"><i class="fa fa-trash"></i></a>' +
                        '<a href="\\#" data-id="#=id#" ng-click="ManageImages($event,#=id#)" class="event-button btn-image"><i class="fa fa-picture-o"></i></a>',
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
                    field: "createDate", width: "130px", title: "Ngày up", filterable: false, template: "#= kendo.toString(kendo.parseDate(createDate, 'yyyy-MM-dd'), 'dd/MM/yyyy')#",
                    filterable: { cell: { template: function (e) { e.element.kendoDatePicker({ format: 'dd/MM/yyyy' }); }, operator: 'equal', showOperators: false } },
                },
                {
                    field: "categoryID", width: "300px", title: "Danh mục", template: "#=Category.name#",
                    filterable: false
                },
                { 
                    field: "status", width: "130px", title: "Trạng thái", editable: false, filterable: false, 
                    template: kendo.template($('#statusTpl').html())
                        
                },               
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
                        createDate: { type: "date" },
                        like: { editable: false },
                        viewCount: { editable: false }
                    }
                }
            }
          }
    }
   
    $scope.DropD_PROCateOptions={
        dataTextField: "name",
        dataValueField: "ID",
        autoBind: false,
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

    $scope.RemoveItem = function ($event, id) {
        $event.preventDefault();
        $http.post("/Pn/Pn/PRO_Delete", { id: id }).then(function success(res) {
            if (res.data.status == true) {
                $scope.Pro_Grid.dataSource.read();
                toastr.success('Thành công', '');                
            }
        })
    }

    $scope.ChangeStatus = function ($event,id) {
        $event.preventDefault();
        $http.post("/Pn/Pn/PRO_ChangeStatus", { id: id }).then(function success(res) {            
            $scope.Pro_Grid.dataSource.read();
            toastr.success('Thành công', '');                                      
        })
    }

    //quản lý ảnh
    $scope.ManageImages = function ($event, id) {
        $event.preventDefault();
        $scope.productId = id;
        $http.post("/Pn/Pn/PRO_LoadImages", { id: id }).then(function success(res) {                                 
            if (res.data.status == true) {
                $scope.showModalImg = true;
                $scope.images = res.data.producImages;                                        
            } else {                
                $scope.showModalImg = true;
                $scope.images = [];
            }            
        })
    }

    $scope.ChooseImages_Mannage = function () {      
        var finder = new CKFinder();
        finder.selectActionFunction = function (url) {
            $scope.images.push(url);                                             
        };
        finder.popup();     
    }
    $scope.RefeshImages = function ($event) {
        $event.preventDefault();
        $scope.images;
    }

    $scope.DellImage = function ($event, item) {
        $event.preventDefault();
        $scope.images.splice($scope.images.indexOf(item), 1);
    }

    $scope.SaveImages = function () {
        $rootScope.IsLoading = true;
        $http.post("/Pn/Pn/PRO_SaveImages", { id: $scope.productId, images: JSON.stringify($scope.images) }).then(function success(res) {
            if (res.data.status == true) {
                $rootScope.IsLoading = false;
                $scope.showModalImg = false;
                toastr.success('Thành công', '');
            } 
        })
    }

    $scope.Cate_Options={
        dataTextField: "name",
        dataValueField: "ID",
        autoBind: false,       
        dataSource: {
            severFiltering: true,
            transport: {
                read: {
                    url: "/Pn/Pn/PRO_Categories_Filter",
                    contentType: "application/json",
                    type: "GET"
                }

            }
        }, change: function (e) {
            
        }
    }

    $scope.numFeeBase_options = { format: 'n0', spinners: false, culture: 'en-US', min: 0, step: 0.01 }
    //$scope.editor_DetailOptions = {
    //    language: 'en'
    //}
    //win
    $scope.PRO_WinClick = function ($event,id) {
        $event.preventDefault();
        //vform({ clear: true });
        $http.post("/Pn/Pn/PRO_Get", { id: id }).then(function success(response) {
           // vform({ clear: true });
            $scope.Item = response.data.product;
            if ($scope.Item.code != null)
                $scope.Item.code = $scope.Item.code.trim();
            $scope.showModal_pro = true;
            
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

    $scope.PRO_SaveClick = function ($event,vform) {
        $event.preventDefault();
        vform({ clear: true });
        if (vform()) {            
            $http.post("/Pn/Pn/PRO_Save", { item: JSON.stringify($scope.Item) }).then(function success(res) {
                if (res.data.status == true) {
                    vform({ clear: true });
                    $scope.Pro_Grid.dataSource.read();                   
                    toastr.success(res.data.msg, '');
                    $scope.showModal_pro = false;
                } else {
                    toastr.error(res.data.msg, '');
                }
                
            })
        }
    }

    $scope.Close_Click = function ($event, win) {
        $event.preventDefault();
        win.close();
    }
}]);