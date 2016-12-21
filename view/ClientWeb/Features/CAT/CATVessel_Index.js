

/// <reference path="~/Scripts/jquery-1.9.1.intellisense.js" />
/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />

var _CATVessel = {
    URL: {
        Read: 'CATVessel_List',
        Get: 'CATVessel_Get',
        Save: 'CATVessel_Save',
        Cbo: 'CboCATPartner_List',
        Delete: "CATVessel_Delete",
    },
    Data: {
        
    },
}

angular.module('myapp').controller('CATVessel_IndexCtrl', ['$rootScope', '$scope', '$http', '$location', '$state', '$timeout', function ($rootScope, $scope, $http, $location, $state, $timeout) {
    if ($rootScope.IsPageComplete != true) return;
    Common.Log('CATVessel_IndexCtrl');
    $rootScope.IsLoading = false;
    $scope.GetPartnerID = 0;
    $scope.Auth = $rootScope.GetAuth();
    $scope.CATVessel_gridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.CAT,
            method: _CATVessel.URL.Read,
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    Code: { type: 'string' },
                    CATGroupOfLocationName: { type: 'string' },
                }
            }
        }),
        toolbar: kendo.template($('#CATVessel_grid_toolbar').html()),
        columns: [
            {
                field: '', title: ' ', width: '90px', sortorder: 0, configurable: false, isfunctionalHidden: false,
                template: '<a href="/" ng-click="CATVesselEdit_Click($event,CATVessel_winPopup,CATVessel_grid)" class="k-button"><i class="fa fa-pencil"></i></a>' +
                    '<a href="/" ng-click="CATVesselDestroy_Click($event,CATVessel_grid)" class="k-button"><i class="fa fa-trash"></i></a>',
                filterable: false, sortable: false
            },
            {
                field: 'Code', title: 'Mã', width: 200, sortorder: 1, configurable: true, isfunctionalHidden: false,
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'VesselName', title: 'Tên', width: 200, sortorder: 2, configurable: true, isfunctionalHidden: false,
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'PartnerName', title: 'Tên đối tác', sortorder: 3, configurable: true, isfunctionalHidden: false,
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
        ],
    },
    $scope.CATVesselEdit_Click = function ($event, win, grid) {
        $event.preventDefault();
        var tr = $($event.target).closest('tr');
        var id = 0;
        var item = grid.dataItem(tr);
        if (Common.HasValue(item)) id = item.ID;
        $scope.LoadItem(win, item.ID);
    }
    $scope.CATVesselSave_Click = function ($event, win, vform) {
        $event.preventDefault();
        if (vform()) {
            if ($scope.Item.PartnerID > 0) {
                $rootScope.IsLoading = true;
                Common.Services.Call($http, {
                    url: Common.Services.url.CAT,
                    method: _CATVessel.URL.Save,
                    data: { item: $scope.Item },
                    success: function (res) {
                        $scope.CATVessel_gridOptions.dataSource.read();
                        $rootScope.IsLoading = false;
                        win.close();
                        $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                    }
                });
            }
            else {
                $rootScope.Message({ Msg: 'Chưa chọn đối tac', NotifyType: Common.Message.NotifyType.ERROR });
            }
        }
    }

    $scope.CATVesselDestroy_Click = function ($event, grid) {
        $event.preventDefault();
        var tr = $($event.target).closest('tr');
        var item = grid.dataItem(tr);
        if (Common.HasValue(item)) {
            $rootScope.Message({
                Type: Common.Message.Type.Confirm,
                NotifyType: Common.Message.NotifyType.SUCCESS,
                Title: 'Thông báo',
                Msg: 'Bạn muốn xóa dữ liệu đã chọn?',
                Close: null,
                Ok: function () {
                    $rootScope.IsLoading = true;
                    Common.Services.Call($http, {
                        url: Common.Services.url.CAT,
                        method: _CATVessel.URL.Delete,
                        data: { 'item': item },
                        success: function (res) {
                            $scope.CATVessel_gridOptions.dataSource.read();
                            $rootScope.IsLoading = false;
                            $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                        }
                    });
                }
            })
        }
    }

    $scope.CATVesselClose_Click = function ($event, win) {
        $event.preventDefault();

        win.close();
    };
    $scope.CATVesselAddNew_Click = function ($event, win) {
        $event.preventDefault();
        $scope.LoadItem(win, 0);
    };

    $scope.cboVessel_Options = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: "contains", suggest: true,
        dataTextField: 'PartnerName', dataValueField: 'ID',
        dataSource: Common.DataSource.Local([], {
            id: 'ID',
            fields: {
                PartnerName: { type: 'string' },
                ID: { type: 'number' },
            }
        })
    }
    Common.Services.Call($http, {
        url: Common.Services.url.CAT,
        method: _CATVessel.URL.Cbo,
        data: {},
        success: function (res) {
            if (Common.HasValue(res) && res.length > 0) {
                $scope.cboVessel_Options.dataSource.data(res);
                $scope.GetPartnerID = res[0].ID;
            }
        }
    });

    $scope.LoadItem = function (win, id) {
        $rootScope.IsLoading = true;
        Common.Services.Call($http, {
            url: Common.Services.url.CAT,
            method: _CATVessel.URL.Get,
            data: { 'ID': id },
            success: function (res) {
                $scope.Item = res;
                if (id == 0) {
                    $scope.Item.PartnerID = $scope.GetPartnerID;
                }
                $rootScope.IsLoading = false;
                win.center();
                win.open();
            }
        });
    }
    //#endregion
    //#region Action
    $scope.ShowSetting = function ($event, grid) {
        $event.preventDefault();

        $rootScope.ShowSetting({
            ListView: [],
            event: $event,
            grid: grid,
            current: $state.current
        });
    };

    //#endregion

}]);