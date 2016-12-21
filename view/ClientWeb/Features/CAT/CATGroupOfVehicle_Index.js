/// <reference path="~/Scripts/jquery-1.9.1.intellisense.js" />
/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />

//#region URL
var _CATGroupOfVehicle = {
    URL: {
        Read: 'CATGroupOfVehicle_Read',
        Delete: 'CATGroupOfVehicle_Destroy',
        Save: 'CATGroupOfVehicle_Update',
        Get: 'CATGroupOfVehicle_Get',
        Get_DataParent: 'CATGroupOfVehicle_DropdownList_Read',

        ExcelInit: 'CATGroupOfVehicle_ExcelInit',
        ExcelChange: 'CATGroupOfVehicle_ExcelChange',
        ExcelImport: 'CATGroupOfVehicle_ExcelImport',
        ExcelApprove: 'CATGroupOfVehicle_ExcelApprove',
    },
    Data: {
        Country: [],
        Province: []
    }
}

//#endregion

angular.module('myapp').controller('CATGroupOfVehicle_IndexCtrl', ['$rootScope', '$scope', '$http', '$location', '$state', '$timeout', function ($rootScope, $scope, $http, $location, $state, $timeout) {
    if ($rootScope.IsPageComplete != true) return;
    Common.Log('CATGroupOfVehicle_IndexCtrl');
    $rootScope.IsLoading = false;

    $scope.Auth = $rootScope.GetAuth();

    $scope.CATGroupOfVehicleItem = null

    $scope.CATGroupOfVehicle_gridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.CAT,
            method: _CATGroupOfVehicle.URL.Read,
            model: {
                id: 'ID',
                fields: {
                    parentId: { from: 'ParentID', type: 'number', editable: true, nullable: true },
                    ID: { type: 'number' },
                    Ton: { type: 'number' },
                    SortOrder: { type: 'number' },
                },
                expanded: false
            }
        }),
        height: '100%', pageable: true, sortable: true, columnMenu: false, resizable: true, reorderable: true, filterable: false,
        columns: [
            {
                field: "Command", title: ' ', width: '100px', sortorder: 0, configurable: false, isfunctionalHidden: false,
                template: '<a href="/" ng-click="CATGroupOfVehicleEdit_Click($event,CATGroupOfVehicle_win,CATGroupOfVehicle_grid)" class="k-button"><i class="fa fa-pencil"></i></a>' +
                '<a href="/" ng-click="CATGroupOfVehicleDestroy_Click($event,CATGroupOfVehicle_grid)" class="k-button"><i class="fa fa-trash"></i></a>',
                filterable: false, sortable: false
            },
            //{ title: ' ', width: 50, filterable: false, sortable: false },
            {
                field: 'Code', title: '{{RS.CATGroupOfVehicle.Code}}', width: 200, sortorder: 1, configurable: true, isfunctionalHidden: false
                //filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'GroupName', title: '{{RS.CATGroupOfVehicle.GroupName}}', sortorder: 2, configurable: true, isfunctionalHidden: false
                // filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'Ton', sortorder: 3, configurable: true, isfunctionalHidden: false, title: '{{RS.CATGroupOfVehicle.Ton}}', template: '#=Ton==null?"":Common.Number.ToNumber3(Ton)#'
                // filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'SortOrder', sortorder: 4, configurable: true, isfunctionalHidden: false, title: '{{RS.CATGroupOfVehicle.SortOrder}}', template: '#=SortOrder==null?"":SortOrder#'
                // filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            { title: '', filterable: false, sortable: false, sortorder: 5, configurable: false, isfunctionalHidden: false }
            
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
    $scope.CATGroupOfVehicleEdit_Click = function ($event, win, grid) {
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
            method: _CATGroupOfVehicle.URL.Get,
            data: { 'ID': id },
            success: function (res) {
                $scope.GetDataCboParent(res.ID);
                $scope.CATGroupOfVehicleItem = res;
                win.center();
                win.open();
            }
        });
    }

    $scope.CATGroupOfVehicleDestroy_Click = function ($event, grid) {
        $event.preventDefault();
        var tr = $($event.target).closest('tr');
        var item = grid.dataItem(tr);
        if (Common.HasValue(item)) {
            $rootScope.Message({
                Type: Common.Message.Type.Confirm,
                NotifyType: Common.Message.NotifyType.SUCCESS,
                Title: 'Thông báo',
                Msg: 'Bạn có muốn xóa không',
                Close: null,
                Ok: function () {
                    $rootScope.IsLoading = true;
                    Common.Services.Call($http, {
                        url: Common.Services.url.CAT,
                        method: _CATGroupOfVehicle.URL.Delete,
                        data: { 'item': item },
                        success: function (res) {
                            $scope.CATGroupOfVehicle_gridOptions.dataSource.read();
                            $rootScope.IsLoading = false;
                            $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                        }
                    })
                }
            })
        }
    }

    $scope.CATGroupOfVehicle_win_SaveClick = function ($event, win, vform) {
        $event.preventDefault();
        if (vform()) {
                Common.Services.Call($http, {
                    url: Common.Services.url.CAT,
                    method: _CATGroupOfVehicle.URL.Save,
                    data: { item: $scope.CATGroupOfVehicleItem },
                    success: function (res) {
                        Common.Services.Error(res, function (res) {
                            $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                            win.close();
                            $scope.CATGroupOfVehicle_gridOptions.dataSource.read();
                        })
                    }
                });
        }
    }

    $scope.CATGroupOfVehicle_win_CloseClick = function ($event, win) {
        $event.preventDefault();

        win.close();
    };

    $scope.CATGroupOfVehicle_AddNew_Click = function ($event, win, vform) {
        $event.preventDefault();
        $scope.LoadItem(win, 0);
    }

    $scope.CATGroupOfVehicle_cboParent_Options = {
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
            method: _CATGroupOfVehicle.URL.Get_DataParent,
            data: { 'id': id },
            success: function (data) {
                $scope.CATGroupOfVehicle_cboParent_Options.dataSource.data(data);
            }
        })
    }

    $scope.CATGroupOfVehicle_numTon_Options = { format: 'n2', spinners: false, culture: 'en-US', min: 0, step: 1, decimals: 2, }
    $scope.CATGroupOfVehicle_numSortOrder_Options = { format: 'n0', spinners: false, culture: 'en-US', min: 0, step: 1, decimals: 2, }

    $scope.Back_Click = function ($event) {
        $event.preventDefault();

        $state.go("main.PUBAdminFleet.Index", _CUSContract_Price_DI_LoadMOQ.Params)
    }

    $scope.CATGroupOfVehicle_Excel_Click = function ($event) {
        $event.preventDefault();


        var lstMessageError = [];
        for (var i = 0; i < 3; i++) {
            var resource = $rootScope.RS[_FLMDriver.ExcelKey.Resource + '_' + i];
            if (Common.HasValue(resource))
                lstMessageError.push(resource);
        }
        if (lstMessageError.length == 0) {
            lstMessageError = [
                '[Mã] không được trống và > 50 ký tự',
                '[Mã] đã bị trùng',
                '[Tên] không được trống và > 50 ký tự',
            ];
        }


        $rootScope.excelShare.Init({
            functionkey: 'CATGroupOfVehicle_Index',
            params: {},
            rowStart: 1,
            colCheckChange: 3,
            url: Common.Services.url.CAT,
            methodInit: _CATGroupOfVehicle.URL.ExcelInit,
            methodChange: _CATGroupOfVehicle.URL.ExcelChange,
            methodImport: _CATGroupOfVehicle.URL.ExcelImport,
            methodApprove: _CATGroupOfVehicle.URL.ExcelApprove,
            lstMessageError: lstMessageError,

            Changed: function () {

            },
            Approved: function () {
                $scope.CATGroupOfVehicle_gridOptions.dataSource.read();
                $rootScope.Message({ Msg: 'Đã lưu', NotifyType: Common.Message.NotifyType.ERROR });
            }
        });
    };
}]);