
'use strict'

app.controller('PRO_IndexCtr', ['$http', '$scope', '$rootScope', function ($http, $scope, $rootScope) {
    $rootScope.title = 'Quản lý sản phẩm';
    $scope.Item = null;
    $scope.showModal = false;
   // $scope.images = [];
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
                    template: '<a href="\\#" class="event-button" ng-click="PRO_WinClick($event,PRO_Win,#=id#)"><i class="fa fa-pencil"></i></a>' +
                        '<a href="\\#" class="event-button" ng-click="RemoveItem($event,#=id#)"><i class="fa fa-trash"></i></a>' +
                        '<a href="\\#" data-id="#=id#" ng-click="ManageImages($event,ManageImages_Win,#=id#)" class="event-button btn-image"><i class="fa fa-picture-o"></i></a>',
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
                { 
                    field: "status", width: "130px", title: "Trạng thái", editable: false, filterable: false, 
                    template: '<a ng-click="ChangeStatus($event,#=id#)" href="/"><span class="btn_success" ng-show="#=status#">Đã về</span> ' +
                        '<span class="btn_info" ng-show="!#=status#">Sắp về</span></a>'
                },
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
            if (res.data.status == true) {
                $scope.Pro_Grid.dataSource.read();
                toastr.success('Thành công', '');               
            }                         
        })
    }

    //quản lý ảnh
    $scope.ManageImages = function ($event,win, id) {
        $event.preventDefault();
        $scope.productId = id;
        $http.post("/Pn/Pn/PRO_LoadImages", { id: id }).then(function success(res) {                                 
            if (res.data.status == true) {
                $scope.showModal = true;               
                $scope.images = res.data.producImages;                                        
            } else {                
                $scope.showModal = true;
                $scope.images = [];
            }            
        })
    }

    $scope.ChooseImages_Mannage = function () {
        var finder = new CKFinder();
        finder.selectActionFunction = function (url) {
            $scope.imageUrl = url;
        };
        finder.popup();       
    }
    
   // $scope.images.push($scope.imageUrl);

    $scope.DellImage = function ($event) {
        $event.preventDefault();
        $(this).parent().remove();
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
    $scope.editor_DetailOptions = {
        language: 'en'
    }
    //win
    $scope.PRO_WinClick = function ($event, win,id) {
        $event.preventDefault();
        //vform({ clear: true });
        $http.post("/Pn/Pn/PRO_Get", { id: id }).then(function success(response) {
           // vform({ clear: true });
            $scope.Item = response.data.product;
            if ($scope.Item.code != null)
                $scope.Item.code = $scope.Item.code.trim();
            win.center();
            win.open();
            win.toFront();           
            
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

    $scope.PRO_SaveClick = function ($event,win,vform) {
        $event.preventDefault();       
        if (vform()) {
            $rootScope.IsLoading = true;
            $http.post("/Pn/Pn/PRO_Save", { item: JSON.stringify($scope.Item) }).then(function success(res) {
                if (res.data.status == true) {
                    $rootScope.IsLoading = false;
                    $scope.Pro_Grid.dataSource.read();                   
                    toastr.success(res.data.msg, '');
                    win.close();
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