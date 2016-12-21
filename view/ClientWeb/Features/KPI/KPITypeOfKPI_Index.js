/// <reference path="~/Scripts/jquery-1.9.1.intellisense.js" />
/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />

//#region URL
var KPITypeOfKPI_Index = {
    URL: {
        Read: 'KPITypeOfKPI_List',
        Delete: 'KPITypeOfKPI_Delete',
        Save: 'KPITypeOfKPI_Save',
        Get: 'KPITypeOfKPI_Get',
        get_KPITypeOfKPIID: 'KPIGet_KPITypeOfKPIID'

    },
}

//#endregion

angular.module('myapp').controller('KPITypeOfKPI_IndexCtrl', ['$rootScope', '$scope', '$http', '$location', '$state', '$timeout', '$stateParams', function ($rootScope, $scope, $http, $location, $state, $timeout, $stateParams) {
    if ($rootScope.IsPageComplete != true) return;
    Common.Log('KPITypeOfKPI_IndexCtrl');
    $rootScope.IsLoading = false;

    $scope.Auth = $rootScope.GetAuth();
    $scope.KPITypeOfKPIItem = null;
    $scope.ParamEdit = { ID: -1 };
    $scope.KPITypeOfKPIID = 0;

    $scope.KPITypeOfKPI_gridOptions = {
        dataSource: Common.DataSource.Grid($http,
            {
                url: Common.Services.url.KPI,
                method: KPITypeOfKPI_Index.URL.Read,
                model: {
                    id: 'ID',
                    fields: {
                        ID: { type: 'number' },
                        Level:{type:'number'},
                    }
                }
            }),
        height: '100%', pageable: true, sortable: true, columnMenu: false, resizable: true, reorderable: true, filterable: { mode: 'row' },
        columns: [
            {
                title: ' ', width: '120px',
                template: '<a href="/" ng-click="KPITypeOfKPIEdit_Click($event,KPITypeOfKPI_win,dataItem)" class="k-button"><i class="fa fa-pencil"></i></a>' +
                '<a href="/" ng-click="KPITypeOfKPIDestroy_Click($event,KPITypeOfKPI_grid)" class="k-button"><i class="fa fa-trash"></i></a>',
                filterable: false, sortable: false
            },
            {
                field: 'Code', title: 'Mã', width: 200,
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'TypeName', title: 'Tên', width: 200,
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'KPITypeOfKPIName', title: 'Loại', width: 200,
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'Level', title: 'Thứ tự', width: 200,
                filterable: { cell: { operator: 'lte', showOperators: false } }
            },
            { title: '', filterable: false, sortable: false }
        ]

    };

    //#region Chỉnh sữa, xóa, detail
    $scope.KPITypeOfKPIEdit_Click = function ($event, win, data) {
        $event.preventDefault();
        $scope.LoadItem(win, data.ID);
    }
    $scope.LoadItem = function (win, id) {

        Common.Services.Call($http, {
            url: Common.Services.url.KPI,
            method: KPITypeOfKPI_Index.URL.Get,
            data: { 'ID': id },
            success: function (res) {
                $scope.KPITypeOfKPIItem = res;
                if (id == 0)
                {
                    $scope.KPITypeOfKPIItem.KPITypeOfKPIID = $scope.KPITypeOfKPIID;
                }
                win.center();
                win.open();
            }
        });
    }


    $scope.KPITypeOfKPIDestroy_Click = function ($event, grid) {
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
                        method: KPITypeOfKPI_Index.URL.Delete,
                        data: { id: item.ID },
                        success: function (res) {
                            $scope.KPITypeOfKPI_gridOptions.dataSource.read();
                            $rootScope.IsLoading = false;
                            $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                        }
                    })
                }
            })
        }
    }

    //#endrgion

    $scope.KPITypeOfKPI_win_SaveClick = function ($event, win, vform) {
        $event.preventDefault();
        if (vform()) {
            if ($scope.KPITypeOfKPIItem.KPITypeOfKPIID > 0 && $scope.KPITypeOfKPIItem.Level > 0) {
                $rootScope.IsLoading = true;
                Common.Services.Call($http, {
                    url: Common.Services.url.KPI,
                    method: KPITypeOfKPI_Index.URL.Save,
                    data: { item: $scope.KPITypeOfKPIItem },
                    success: function (res) {
                        Common.Services.Error(res, function (res) {
                            $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                            win.close();
                            $scope.KPITypeOfKPI_gridOptions.dataSource.read();
                            $rootScope.IsLoading = false;
                        })
                    }
                });
            }
            else {
                $rootScope.Message({ Msg: "Dữ liệu sai, thứ tự lớn hơn 0!" });
            }
        }
        else {
            $rootScope.Message({ Msg: "Thiếu dữ liệu, dữ liệu chưa nhập!" });
        }

    }
    $scope.KPITypeOfKPI_win_CloseClick = function ($event, win) {
        $event.preventDefault();

        win.close();
    };


    //#region Thêm mới

    $scope.KPITypeOfKPI_AddNew_Click = function ($event, win, vform) {
       
        $event.preventDefault();
        $scope.LoadItem(win, 0);
    }
    //#endregion
    $scope.KPITypeOfKPI_cboTypeOfKPIID_Options = {
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

    Common.Services.Call($http, {
        url: Common.Services.url.KPI,
        method: KPITypeOfKPI_Index.URL.get_KPITypeOfKPIID,
        success: function (res) {
            if (Common.HasValue(res.Data))
                $scope.KPITypeOfKPIID = res.Data[0].ID;
            $scope.KPITypeOfKPI_cboTypeOfKPIID_Options.dataSource.data(res.Data);
        }
    })

    $scope.ShowSetting = function ($event, grid) {
        $event.preventDefault();
        $rootScope.ShowSetting({
            ListView: [],
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

