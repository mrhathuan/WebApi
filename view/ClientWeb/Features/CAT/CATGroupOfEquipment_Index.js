/// <reference path="~/Scripts/jquery-1.9.1.intellisense.js" />
/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />

//#region URL
var _CATGroupOfEquipment = {
    URL: {
        Read: 'CATGroupOfEquipment_Read',
        Delete: 'CATGroupOfEquipment_Destroy',
        Save: 'CATGroupOfEquipment_Update',
        Get: 'CATGroupOfEquipment_Get',
        Get_DataParent: 'CATGroupOfEquipment_DropdownList_Read',

        ExcelInit: 'CATGroupOfEquipment_ExcelInit',
        ExcelChange: 'CATGroupOfEquipment_ExcelChange',
        ExcelImport: 'CATGroupOfEquipment_ExcelImport',
        ExcelApprove: 'CATGroupOfEquipment_ExcelApprove',
    },
    Data: {
        Country: [],
        Province: []
    }
}

//#endregion

angular.module('myapp').controller('CATGroupOfEquipment_IndexCtrl', ['$rootScope', '$scope', '$http', '$location', '$state', '$timeout', function ($rootScope, $scope, $http, $location, $state, $timeout) {
    if ($rootScope.IsPageComplete != true) return;
    Common.Log('CATGroupOfEquipment_IndexCtrl');
    $rootScope.IsLoading = false;

    $scope.Auth = $rootScope.GetAuth();

    $scope.CATGroupOfEquipmentItem = null

    $scope.CATGroupOfEquipment_gridOptions = {
        dataSource: Common.DataSource.TreeList($http, {
            url: Common.Services.url.CAT,
            method: _CATGroupOfEquipment.URL.Read,
            model: {
                id: 'ID',
                fields: {
                    parentId: { from: 'ParentID', type: 'number', editable: true, nullable: true },
                    ID: { type: 'number' },
                    GroupName: { type: 'string' },
                },
                expanded: false
            }
        }),
        height: '100%', pageable: true, sortable: true, columnMenu: false, resizable: true, reorderable: true, filterable: false,
        columns: [
            { title: ' ', width: '45px', filterable: false, sortable: false },
            {
                field: "Command", title: ' ', width: '85px', sortorder: 0, configurable: false, isfunctionalHidden: false,
                template: '<a href="/" ng-click="CATGroupOfEquipmentEdit_Click($event,CATGroupOfEquipment_win,CATGroupOfEquipment_grid)" class="k-button"><i class="fa fa-pencil"></i></a>' +
                '<a href="/" ng-click="CATGroupOfEquipmentDestroy_Click($event,CATGroupOfEquipment_grid)" class="k-button"><i class="fa fa-trash"></i></a>',
                filterable: false, sortable: false
            },
            {
                field: 'GroupName', title: '{{RS.CATGroupOfEquipment.GroupName}}', sortorder: 1, configurable: true, isfunctionalHidden: false
            },
            { title: '', filterable: false, sortable: false, sortorder: 2, configurable: false, isfunctionalHidden: false }
        ]
    };
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
    $scope.CATGroupOfEquipmentEdit_Click = function ($event, win, grid) {
        $event.preventDefault();
        var tr = $($event.target).closest('tr');
        var id = 0;
        var item = grid.dataItem(tr);
        if (Common.HasValue(item)) id = item.ID;
        
        $scope.LoadItem(win, id);
    }

    $scope.LoadItem = function (win, id) {

        Common.Services.Call($http, {
            url: Common.Services.url.CAT,
            method: _CATGroupOfEquipment.URL.Get,
            data: { 'ID': id },
            success: function (res) {
                $scope.GetDataCboParent(res.ID);
                $scope.CATGroupOfEquipmentItem = res;
                win.center();
                win.open();
            }
        });
    }

    $scope.CATGroupOfEquipmentDestroy_Click = function ($event, grid) {
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
                    $scope.IsLoading = true;
                    Common.Services.Call($http, {
                        url: Common.Services.url.CAT,
                        method: _CATGroupOfEquipment.URL.Delete,
                        data:{'item':item},
                        success: function (res) {
                            $scope.CATGroupOfEquipment_gridOptions.dataSource.read();
                            $scope.IsLoading = false;
                            $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                        }
                    })
                }
            })
        }
    }

    $scope.CATGroupOfEquipment_win_SaveClick = function ($event, win, vform) {
        $event.preventDefault();
        if (vform()) {
                Common.Services.Call($http, {
                    url: Common.Services.url.CAT,
                    method: _CATGroupOfEquipment.URL.Save,
                    data: { item: $scope.CATGroupOfEquipmentItem },
                    success: function (res) {
                        Common.Services.Error(res, function (res) {
                            $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                            win.close();
                            $scope.CATGroupOfEquipment_gridOptions.dataSource.read();
                        })
                    }
                });
        }
    }

    $scope.CATGroupOfEquipment_win_CloseClick = function ($event, win) {
        $event.preventDefault();

        win.close();
    };

    $scope.CATGroupOfEquipment_AddNew_Click = function ($event, win, vform) {
        $event.preventDefault();
        $scope.LoadItem(win, 0);
    }

    $scope.CATGroupOfEquipment_cboParent_Options = {
        autoBind: true,
        valuePrimitive: true,
        ignoreCase: true,
        filter: 'contains',
        suggest: true,
        dataTextField: 'GroupName',
        dataValueField: 'ID',
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    GroupName: { type: 'string' },
                }
            }
        }),
        change: function (e) {
        }
    }

    $scope.GetDataCboParent = function (id) {
        
        Common.Services.Call($http, {
            url: Common.Services.url.CAT,
            method: _CATGroupOfEquipment.URL.Get_DataParent,
            data: { 'id': id },
            success: function (data) {
                $scope.CATGroupOfEquipment_cboParent_Options.dataSource.data(data);
            }
        })
    }

    $scope.CATGroupOfEquipment_Excel_Click = function ($event) {
        $event.preventDefault();

        var lstMessageError = [];
        for (var i = 0; i < 2; i++) {
            var resource = $rootScope.RS[_FLMDriver.ExcelKey.Resource + '_' + i];
            if (Common.HasValue(resource))
                lstMessageError.push(resource);
        }
        if (lstMessageError.length == 0) {
            lstMessageError = [
                '[Tên] không được trống và > 50 ký tự',
                '[Mã] Không chính xác',
            ];
        }

        $rootScope.excelShare.Init({
            functionkey: 'CATGroupOfEquipment_Index',
            params: {},
            rowStart: 1,
            colCheckChange: 3,
            url: Common.Services.url.CAT,
            methodInit: _CATGroupOfEquipment.URL.ExcelInit,
            methodChange: _CATGroupOfEquipment.URL.ExcelChange,
            methodImport: _CATGroupOfEquipment.URL.ExcelImport,
            methodApprove: _CATGroupOfEquipment.URL.ExcelApprove,
            lstMessageError: lstMessageError,

            Changed: function () {

            },
            Approved: function () {
                $scope.CATGroupOfLocation_gridOptions.dataSource.read();
                $rootScope.Message({ Msg: 'Đã lưu', NotifyType: Common.Message.NotifyType.ERROR });
            }
        });
    };

    $scope.Back_Click = function ($event) {
        $event.preventDefault();

        $state.go("main.PUBAdminFleet.Index", _CUSContract_Price_DI_LoadMOQ.Params)
    }
}]);