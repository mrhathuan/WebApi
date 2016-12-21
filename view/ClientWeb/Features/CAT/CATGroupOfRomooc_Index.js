/// <reference path="~/Scripts/jquery-1.9.1.intellisense.js" />
/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />

//#region URL
var _CATGroupOfRomooc = {
    URL: {
        Read: 'CATGroupOfRomooc_Read',
        Delete: 'CATGroupOfRomooc_Destroy',
        Save: 'CATGroupOfRomooc_Update',
        Get: 'CATGroupOfRomooc_Get',
        Get_DataParent: 'CATGroupOfRomooc_DropdownList_Read',

        ExcelInit: 'CATGroupOfRomooc_ExcelInit',
        ExcelChange: 'CATGroupOfRomooc_ExcelChange',
        ExcelImport: 'CATGroupOfRomooc_ExcelImport',
        ExcelApprove: 'CATGroupOfRomooc_ExcelApprove',

        CATGroupOfRomoocPacking_List: 'CATGroupOfRomoocPacking_List',
        CATGroupOfRomoocPacking_NotInList: 'CATGroupOfRomoocPacking_NotInList',
        CATGroupOfRomoocPacking_Save: 'CATGroupOfRomoocPacking_Save',
        CATGroupOfRomoocPacking_Delete: 'CATGroupOfRomoocPacking_Delete',
    },
    Data: {
        Country: [],
        Province: []
    }
}

//#endregion

angular.module('myapp').controller('CATGroupOfRomooc_IndexCtrl', ['$rootScope', '$scope', '$http', '$location', '$state', '$timeout', function ($rootScope, $scope, $http, $location, $state, $timeout) {
    if ($rootScope.IsPageComplete != true) return;
    Common.Log('CATGroupOfRomooc_IndexCtrl');
    $rootScope.IsLoading = false;
    $scope.GroupOfRomoocID = 0;
    $scope.TypeOfPackageName = null;
    $scope.Auth = $rootScope.GetAuth();
    $scope.CATGroupOfRomoocItem = null;
    $scope.HasChoose = false;

    $scope.CATGroupOfRomooc_gridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.CAT,
            method: _CATGroupOfRomooc.URL.Read,
            model: {
                id: 'ID',
                fields: {
                    parentId: { from: 'ParentID', type: 'number', editable: true, nullable: true },
                    ID: { type: 'number' },
                    TypeName: { type: 'string' },
                },
                expanded: false
            }
        }),
        height: '100%', pageable: true, sortable: true, columnMenu: false, resizable: true, reorderable: true, filterable: false,
        columns: [
          
            {
                field: "Command", title: ' ', width: '100px', sortorder: 0, configurable: false, isfunctionalHidden: false,
                template: '<a href="/" ng-click="CATGroupOfRomoocEdit_Click($event,CATGroupOfRomooc_win,CATGroupOfRomooc_grid)" class="k-button"><i class="fa fa-pencil"></i></a>' +
                '<a href="/" ng-click="CATGroupOfRomoocDestroy_Click($event,CATGroupOfRomooc_grid)" class="k-button"><i class="fa fa-trash"></i></a>',
                filterable: false, sortable: false
            },
             {
                 field: 'Code', title: 'Mã Loại', width: '150px', sortorder: 1, configurable: true, isfunctionalHidden: false
                 // filterable: { cell: { operator: 'contains', showOperators: false } }
             },
            {
                field: 'GroupName', title: '{{RS.CATGroupOfRomooc.TypeName}}', width: '150px', sortorder: 2, configurable: true, isfunctionalHidden: false
                // filterable: { cell: { operator: 'contains', showOperators: false } }
            },
             { title: '', filterable: false, sortable: false, sortorder: 3, configurable: false, isfunctionalHidden: false }
        ]
    };

    $scope.CATGroupOfRomoocEdit_Click = function ($event, win, grid) {
        $event.preventDefault();
        var tr = $($event.target).closest('tr');
        var id = 0;
        var item = grid.dataItem(tr);
        if (Common.HasValue(item)) id = item.ID;
        $scope.GroupOfRomoocID = item.ID;
        $scope.LoadItem(win, id);
    }
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
    $scope.LoadItem = function (win, id) {

        Common.Services.Call($http, {
            url: Common.Services.url.CAT,
            method: _CATGroupOfRomooc.URL.Get,
            data: { 'ID': id },
            success: function (res) {
                $scope.GetDataCboParent(res.ID);
                $scope.CATGroupOfRomoocItem = res;

                $scope.GroupOfRomoocPacking_Grid.dataSource.read();
                win.center();
                win.open();
                $timeout(function () {
                    $scope.GroupOfRomoocPacking_Grid.resize;
                }, 100);
            }
        });
    }

    $scope.CATGroupOfRomoocDestroy_Click = function ($event, grid) {
        $event.preventDefault();
        var tr = $($event.target).closest('tr');
        var item = grid.dataItem(tr);
        if (Common.HasValue(item)) {
            $rootScope.Message({
                Msg: "Bạn muốn xóa các dữ liệu đã chọn ?",
                Type: Common.Message.Type.Confirm,
                Ok: function () {
                    Common.Services.Call($http, {
                        url: Common.Services.url.CAT,
                        method: _CATGroupOfRomooc.URL.Delete,
                        data: { 'item': item },
                        success: function (res) {
                            Common.Services.Error(res, function (res) {
                                $rootScope.Message({ Msg: 'Đã xóa thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                                $scope.CATGroupOfRomooc_gridOptions.dataSource.read();
                            })
                        }
                    });
                }
            });
        }
    }

    $scope.CATGroupOfRomooc_win_SaveClick = function ($event, win, vform) {
        $event.preventDefault();
        if (vform()) {
                Common.Services.Call($http, {
                    url: Common.Services.url.CAT,
                    method: _CATGroupOfRomooc.URL.Save,
                    data: { item: $scope.CATGroupOfRomoocItem },
                    success: function (res) {
                        Common.Services.Error(res, function (res) {
                            $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                            win.close();
                            $scope.CATGroupOfRomooc_gridOptions.dataSource.read();
                        })
                    }
                });
        }
    }

    $scope.CATGroupOfRomooc_win_CloseClick = function ($event, win) {
        $event.preventDefault();
        $scope.Rommoc_win_tab.select(0);
        $scope.TabIndex = 1;
        win.close();
    };

    $scope.CATGroupOfRomooc_AddNew_Click = function ($event, win, vform) {
        $event.preventDefault();
        $scope.Rommoc_win_tab.select(0);
        $scope.TabIndex = 1;
        $scope.LoadItem(win, 0);
    }

    $scope.CATGroupOfRomooc_cboParent_Options = {
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
                    TypeName: { type: 'string' },
                }
            }
        }),
        change: function (e) {
        }
    }

    $scope.GetDataCboParent = function (id) {
        
        Common.Services.Call($http, {
            url: Common.Services.url.CAT,
            method: _CATGroupOfRomooc.URL.Get_DataParent,
            data: { 'id': id },
            success: function (data) {
                $scope.CATGroupOfRomooc_cboParent_Options.dataSource.data(data);
            }
        })
    }

    $scope.Back_Click = function ($event) {
        $event.preventDefault();

        $state.go("main.PUBAdminFleet.Index", _CUSContract_Price_DI_LoadMOQ.Params)
    }

    $scope.CATGroupOfRomooc_Excel_Click = function ($event) {
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
            functionkey: 'CATGroupOfRomooc_Index',
            params: {},
            rowStart: 1,
            colCheckChange: 3,
            url: Common.Services.url.CAT,
            methodInit: _CATGroupOfRomooc.URL.ExcelInit,
            methodChange: _CATGroupOfRomooc.URL.ExcelChange,
            methodImport: _CATGroupOfRomooc.URL.ExcelImport,
            methodApprove: _CATGroupOfRomooc.URL.ExcelApprove,
            lstMessageError: lstMessageError,

            Changed: function () {

            },
            Approved: function () {
                $scope.CATGroupOfRomooc_gridOptions.dataSource.read();
                $rootScope.Message({ Msg: 'Đã lưu', NotifyType: Common.Message.NotifyType.ERROR });
            }
        });
    };

    //container
    $scope.TabIndex = 1;
    $scope.Rommoc_win_tabOptions = {
        animation: {
            open: { effects: "fadeIn" }
        },
        select: function (e) {
            $timeout(function () {
                $scope.TabIndex = angular.element(e.item).data('tabindex'); //or
            }, 1);
        }
    }

    $scope.GroupOfRomoocPacking_Grid_Options = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.CAT,
            method: _CATGroupOfRomooc.URL.CATGroupOfRomoocPacking_List,
            readparam: function () {
                return { GroupOfRomoocID: $scope.GroupOfRomoocID }
            },
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                },
            }
        }),
        height: '100%', pageable: true, sortable: true, columnMenu: false, resizable: true, reorderable: true, filterable: { mode: 'row' },
        toolbar: kendo.template($('#CATGroupOfPacking_grid_toolbar').html()),
        columns: [
            {
                title: ' ', width: '45px',
                template: '<a href="/" ng-click="GroupOfRomoocPackingDestroy_Click($event,dataItem)" class="k-button"><i class="fa fa-trash"></i></a>',
                filterable: false, sortable: false
            },
            { field: 'TypeOfPackageName', title: 'Loại con', width: 200, filterable: { cell: { operator: 'contains', showOperators: false } } },
            {
                field: 'Quantity', title: 'Số lượng',
                template:'<input type="number" class="k-textbox" maxlength="50" ng-model="dataItem.Quantity" />',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
        ]
    };

    $scope.GroupOfRomoocPackingNotList_Grid_Options = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.CAT,
            method: _CATGroupOfRomooc.URL.CATGroupOfRomoocPacking_NotInList,
            readparam: function () {
                return { GroupOfRomoocID: $scope.GroupOfRomoocID }
            },
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    IsChoose: { type: 'bool', defaultValue: false },
                },
            }
        }),
        height: '100%', pageable: true, sortable: true, columnMenu: false, resizable: true, reorderable: true, filterable: { mode: 'row' },
        toolbar: kendo.template($('#CATGroupOfPacking_grid_toolbar').html()),
        columns: [
           {
               title: ' ', width: '40px',
               headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,GroupOfRomoocPackingNotList_Grid,GroupOfRomoocPackingNotList_GridChooseChange)" />',
               headerAttributes: { style: 'text-align: center;' },
               template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,GroupOfRomoocPackingNotList_Grid,GroupOfRomoocPackingNotList_GridChooseChange)" />',
               templateAttributes: { style: 'text-align: center;' },
               filterable: false, sortable: false
           },
            { field: 'TypeOfPackageName', title: 'Loại con', width: 200, filterable: { cell: { operator: 'contains', showOperators: false } } },
            {
                field: 'Quantity', title: 'Số lượng',
                template: '<input type="number" class="k-textbox" maxlength="50" ng-model="dataItem.Quantity" />',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
        ]
    };

    $scope.GroupOfRomoocPackingNotList_GridChooseChange = function ($event, grid, haschoose) {
        $scope.HasChoose = haschoose;
    };

    $scope.GroupOfPacking_cboParent_Options = {
        autoBind: true,
        valuePrimitive: true,
        ignoreCase: true,
        filter: 'contains',
        suggest: true,
        dataTextField: 'TypeOfPackageName',
        dataValueField: 'ID',
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    TypeOfPackageName: { type: 'string' },
                }
            }
        }),
        change: function (e) {
        }
    }

    $scope.CATGroupOfPackingAddNew_Click = function ($event, win) {
        $event.preventDefault();
        $scope.GroupOfRomoocPackingNotList_Grid.dataSource.read();
        win.open().center();
        $timeout(function () {
            $scope.GroupOfRomoocPackingNotList_Grid.resize();
        }, 100);
    }

    $scope.CATGroupOfPackingNoList_SaveClick = function ($event, grid, win) {
        $event.preventDefault();
        var lst = [];
        var data = grid.dataSource.data();
        $.each(data, function (i, v) {
            if (v.IsChoose == true)
                lst.push(v);
        });
        if (Common.HasValue(lst)) {
            $scope.SavePacking(lst);
        }
    }
    $scope.CATGroupOfPackingList_SaveClick = function ($event, grid) {
        $event.preventDefault();
        var data = grid.dataSource.data();
        if(Common.HasValue(data))
            $scope.SavePacking(data);
    }

    $scope.SavePacking = function (data) {
        $rootScope.IsLoading = true;
        Common.Services.Call($http, {
            url: Common.Services.url.CAT,
            method: _CATGroupOfRomooc.URL.CATGroupOfRomoocPacking_Save,
            data: { item: data, GroupOfRomoocID: $scope.GroupOfRomoocID },
            success: function (res) {
                Common.Services.Error(res, function (res) {
                    $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                    $scope.GroupOfRomoocPacking_Grid.dataSource.read();
                    $rootScope.IsLoading = false;
                    $scope.CATGroupOfPacking_win.close();
                })
            }
        });
    }

    $scope.GroupOfRomoocPackingDestroy_Click = function ($event, data) {
        $event.preventDefault();

        $rootScope.IsLoading = true;
        Common.Services.Call($http, {
            url: Common.Services.url.CAT,
            method: _CATGroupOfRomooc.URL.CATGroupOfRomoocPacking_Delete,
            data: { item: data },
            success: function (res) {
                Common.Services.Error(res, function (res) {
                    $rootScope.Message({ Msg: 'Xóa thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                    $scope.GroupOfRomoocPacking_Grid.dataSource.read();
                    $rootScope.IsLoading = false;
                })
            }
        });
    }

}]);