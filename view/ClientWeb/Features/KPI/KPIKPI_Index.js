/// <reference path="~/Scripts/jquery-1.9.1.intellisense.js" />
/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />

//#region URL
var _KPIKPI_Index = {
    URL: {
        Read: 'KPIKPI_GetList',
        Delete: 'KPIKPI_Destroy',
        Save: 'KPIKPI_Update',
        Get: 'KPIKPI_Get',

    },
}

//#endregion

angular.module('myapp').controller('KPIKPI_IndexCtrl', ['$rootScope', '$scope', '$http', '$location', '$state', '$timeout', '$stateParams', function ($rootScope, $scope, $http, $location, $state, $timeout, $stateParams) {
    if ($rootScope.IsPageComplete != true) return;
    Common.Log('KPIKPI_IndexCtrl');
    $rootScope.IsLoading = false;

    $scope.Auth = $rootScope.GetAuth();
    $scope.KPIKPIItem = null;
    $scope.ParamEdit = { ID: -1 };

    $scope.KPIKPI_gridOptions = {
        dataSource: Common.DataSource.Grid($http,
            {
                url: Common.Services.url.KPI,
                method: _KPIKPI_Index.URL.Read,
                model: {
                    id: 'ID',
                    fields: {
                        ID: { type: 'number' },
                        Code: { type: 'string' },
                        KPIName: { type: 'string' },
                    }
                }
            }),
        height: '100%', pageable: true, sortable: true, columnMenu: false, resizable: true, reorderable: true, filterable: { mode: 'row' },
        columns: [
            {
                title: ' ', width: '120px',
                template: '<a href="/" ng-click="KPIKPIEdit_Click($event,KPIKPI_win,dataItem)" class="k-button"><i class="fa fa-pencil"></i></a>' +
                '<a href="/" ng-click="KPIKPIDestroy_Click($event,KPIKPI_grid)" class="k-button"><i class="fa fa-trash"></i></a>' +
                '<a href="/" ng-click="KPIKPIDetail_Click($event,dataItem)" class="k-button"><i class="fa fa-file-text"></i></a>',
                filterable: false, sortable: false
            },
            {
                field: 'Code', title: 'Mã', width: 200,
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'KPIName', title: 'Tên', width: 200,
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            { title: '', filterable: false, sortable: false }
        ]

    };


    //#region Chỉnh sữa, xóa, detail
    $scope.KPIKPIEdit_Click = function ($event, win, data) {
        $event.preventDefault();
        $scope.LoadItem(win, data.ID);
    }
    $scope.LoadItem = function (win, id) {

        Common.Services.Call($http, {
            url: Common.Services.url.KPI,
            method: _KPIKPI_Index.URL.Get,
            data: { 'ID': id },
            success: function (res) {
                
                $scope.KPIKPIItem = res;
                win.center();
                win.open();
            }
        });
    }


    $scope.KPIKPIDestroy_Click = function ($event, grid) {
        $event.preventDefault();
        var tr = $($event.target).closest('tr');
        var item = grid.dataItem(tr);
        if (Common.HasValue(item)) {
            $rootScope.Message({
                Type: Common.Message.Type.Confirm,
                NotifyType: Common.Message.NotifyType.SUCCESS,
                title: 'Thông báo',
                Msg: 'Bạn có muốn xóa',
                Close: null,
                Ok: function () {
                    $rootScope.IsLoading = true;
                    Common.Services.Call($http, {
                        url: Common.Services.url.KPI,
                        method: _KPIKPI_Index.URL.Delete,
                        data: { 'item': item },
                        success: function (res) {
                            $scope.KPIKPI_gridOptions.dataSource.read();
                            $rootScope.IsLoading = false;
                            $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                        }
                    })
                }
            })
        }
    }

    $scope.KPIKPIDetail_Click = function ($event, data) {
        $event.preventDefault();
        
        $scope.ParamEdit.ID = data.ID ;
        $state.go('main.KPIKPI.Column', $scope.ParamEdit)
    }
    //#endrgion

    $scope.KPIKPI_win_SaveClick = function ($event, win, vform) {
        $event.preventDefault();
        if (vform()) {
            if (Common.HasValue($scope.KPIKPIItem.TypeOfKPIID) && $scope.KPIKPIItem.TypeOfKPIID > 0) {
                Common.Services.Call($http, {
                    url: Common.Services.url.KPI,
                    method: _KPIKPI_Index.URL.Save,
                    data: { item: $scope.KPIKPIItem },
                    success: function (res) {
                        Common.Services.Error(res, function (res) {
                            $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                            win.close();
                            $scope.KPIKPI_gridOptions.dataSource.read();
                        })
                    }
                });
            }
        }
        else {
            $rootScope.Message({ Msg: "Thiếu dữ liệu, dữ liệu chưa nhập!" });
        }

    }
    $scope.KPIKPI_win_CloseClick = function ($event, win) {
        $event.preventDefault();

        win.close();
    };


    //#region Thêm mới

    $scope.KPIKPI_AddNew_Click = function ($event, win, vform) {
       
        $event.preventDefault();
        $scope.LoadItem(win, 0);
    }
    //#endregion
    $scope.KPIKPI_cboTypeOfKPIID_Options = {
        autoBind: true,
        valuePrimitive: true,
        ignoreCase: true,
        filter: 'contains',
        suggest: true,
        dataTextField: 'ValueOfVar',
        dataValueField: 'ID',

        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    ValueOfVar: { type: 'string' },
                }
            }
        }),
        change: function (e) {
        }
    }

    Common.ALL.Get($http, {
        url: Common.ALL.URL.ALL_SYSVarTypeOfKPI,
        success: function (data) {
            $scope.KPIKPI_cboTypeOfKPIID_Options.dataSource.data(data);
        }
    })

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

