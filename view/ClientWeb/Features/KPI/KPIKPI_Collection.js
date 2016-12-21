/// <reference path="~/Scripts/jquery-1.9.1.intellisense.js" />
/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />
var _KPIKPI_Collection = {
    URL: {
        Collection_GetList: 'KPICollection_GetList',
        GetKPIKPI_AllList:'KPIKPI_AllList',
    },
    Data: {
        _dataStatus:[],
    }
}

angular.module('myapp').controller('KPIKPI_CollectionCtrl', ['$rootScope', '$scope', '$http', '$location', '$state', '$timeout', '$stateParams', function ($rootScope, $scope, $http, $location, $state, $timeout, $stateParams) {
    if ($rootScope.IsPageComplete != true) return;
    Common.Log('KPIKPI_CollectionCtrl');
    $rootScope.IsLoading = false;

    $scope.Auth = $rootScope.GetAuth();
    $scope.HasKPICollevtionSearch = false;
    $scope.ItemCollection = { KPIID: -1, DateFrom: new Date().addDays(-2), DateTo: new Date().addDays(0) };
    
    $scope.KPIKPI_Collection_gridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.KPI,
            method: _KPIKPI_Collection.URL.Collection_GetList,
            readparam: function () {
                return {
                    KPIID: $scope.ItemCollection.KPIID,
                    from: $scope.ItemCollection.DateFrom,
                    to: $scope.ItemCollection.DateTo
                }
            },
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number', editable: false, nullable: true },
                    IsKPI:{type:'boolean'},
                    Note: { type: 'string' },
                    ReasonName: { type: 'string' },
                    OrderCode: { type: 'number' },
                    CustomerName: { type: 'string' },
                    DITOMasterCode: { type: 'string' },
                    COTOMasterCode: { type: 'string' },
                   
                            }
                        }
                    }),
        height: '100%', groupable: false, pageable: false, sortable: true, columnMenu: false, resizable: true,
        selectable: 'row', filterable: { mode: 'row' }, reorderable: true,
        columns: [
            {
                title: ' ', width: '90px',
                template: '<a href="/" ng-click="KIPKPICollectionEdit_Click($event,KPIKPI_grid,dataItem)" class="k-button"><i class="fa fa-pencil"></i></a>' +
                '<a href="/" ng-click="KIPKPICollectionDestroy_Click($event,KPIKPI_grid)" class="k-button"><i class="fa fa-trash"></i></a>',
                filterable: false, sortable: false
            },
            {
                field: 'IsKPI', title: 'Đạt KPI?', width: 120,
                template: '<input type="checkbox" #= IsKPI=="true" ? "checked=checked" : "" # disabled="disabled" />',
                attributes: { style: "text-align: center; " },
                filterable: {
                    cell: {
                        template: function (container) {
                            container.element.kendoComboBox({
                                dataSource: [{ Text: 'Đạt', Value: true }, { Text: 'Không đạt', Value: false }, { Text: 'Tấ cả', Value: '' }],
                                dataTextField: "Text", dataValueField: "Value",
                            });
                        }, showOperators: false
                    }
                }
            },
             {
                 field: 'Note', title: 'Ghi chú', width: 200,
                 filterable: { cell: { operator: 'contains', showOperators: false } }
             },
            {
                field: 'ReasonName', title: 'Lý do', width: 200,
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'OrderCode', title: 'OrderCode', width: 200,
                filterable: { cell: { operator: 'equal', showOperators: false } }
            },
            {
                field: 'CustomerName', title: 'CustomerName', width: 200,
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
        ]
    };

    $scope.Search_Click = function ($event) {
        $event.preventDefault();
      
        if (!Common.HasValue($scope.ItemCollection.DateFrom) || !Common.HasValue($scope.ItemCollection.DateTo) || !Common.HasValue($scope.ItemCollection.KPIID)) {
            
                $rootScope.Message({
                    Type: Common.Message.Type.Alert,
                    NotifyType: Common.Message.NotifyType.ERROR,
                    Title: 'Thông báo',
                    Msg: 'Vui lòng chọn Từ ngày- Đến ngày, Nhập KPI chính xác',
                    Close: null,
                    Ok: null
                })
            }
            else if ($scope.ItemCollection.DateFrom > $scope.ItemCollection.DateTo || !Common.HasValue($scope.ItemCollection.KPIID)) {
                $rootScope.Message({
                    Type: Common.Message.Type.Alert,
                    NotifyType: Common.Message.NotifyType.ERROR,
                    Title: 'Thông báo',
                    Msg: 'Vui lòng chọn Từ ngày nhỏ hơn Đến ngày',
                    Close: null,
                    Ok: null
                })
            }

        else {
            $scope.KPIKPI_Collection_gridOptions.dataSource.read();
        }
    };

    $scope.Generate_Click = function ($event) {
        $event.preventDefault();

            $rootScope.Message({
                Type: Common.Message.Type.Confirm,
                Title: 'Thông báo', Msg: 'Bạn muốn tính lại KPI?',
                Action: true, Close: null,
                Ok: function (pars) {
                    $rootScope.IsLoading = true;
                    Common.Services.Call($http, {
                        url: Common.Services.url.KPI,
                        method: _KPIKPI_Collection.URL.Collection_GetList,
                        data: pars,
                        success: function (res) {
                            $rootScope.IsLoading = false;
                            $rootScope.Message({ Msg: "Đã cập nhật!" });
                            if (Common.HasValue(res)) {
                                $scope.KPIKPI_Collection_gridOptions.dataSource.read();
                            }
                        }
                    });
                },
                pars: { DateFrom: $scope.ItemCollection.DateFrom, DateTo: $scope.ItemCollection.DateTo, KPIID: $scope.ItemCollection.KPIID }
            })
        
    };

    Common.Services.Call($http, {
        url: Common.Services.url.KPI,
        method: _KPIKPI_Collection.URL.GetKPIKPI_AllList,
        
        success: function (data) {

            $scope.KPIKPICollection_cboKPI_Options.dataSource.data(data);
        }
    });
    $scope.KPIKPICollection_cboKPI_Options =
    {
    
        autoBind: true,
        valuePrimitive: true,
        ignoreCase: true,
        filter: 'contains',
        suggest: true,
        dataTextField: 'KPIName',
        dataValueField: 'ID',
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields: {

                    ID: { type: 'number' },
                    KPIName: { type: 'string' },
                }
            }
        }),
        change: function (e) {
        }
    }
    $scope.ShowSetting = function ($event, grid) {
        $event.preventDefault();
        $rootScope.ShowSetting({
            ListView: views.KPIKPI,
            event: $event,
            grid: grid,
            current: $state.current
        });
    };

    $scope.HideSetting = function ($event) {
        $event.preventDefault();

        $rootScope.HideSetting();
    }

}]);