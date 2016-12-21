/// <reference path="~/Scripts/jquery-1.9.1.intellisense.js" />
/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />

//#region URL
var _CATArea = {
    URL: {
        Read: 'CATRoutingArea_List',
        Delete: 'CATRoutingArea_Delete',
        Save: 'CATRoutingArea_Save',
        Get: 'CATRoutingArea_Get',

        ReadDetail: 'CATRoutingAreaDetail_List',
        DeleteDetail: 'CATRoutingAreaDetail_Delete',
        SaveDetail: 'CATRoutingAreaDetail_Save',
        GetDetail: 'CATRoutingAreaDetail_Get',

        ReadLocation: 'CATRoutingArea_Location_List',

        ExcelExport_RouteArea: 'CATRouting_AreaExcel_Export',
        ExcelCheck_RouteArea: 'CATRouting_AreaExcel_Check',
        ExcelSave_RouteArea: 'CATRouting_AreaExcel_Save',
        ExcelExport_RouteAreaLocation: 'CATRouting_ExcelRouteAreaLocation_Export',

        LocationNotIn_List: 'CATRoutingArea_LocationNotIn_List',
        LocationNotIn_Save: 'CATRoutingArea_LocationNotIn_Save',
        Location_Delete: 'CATRoutingArea_Location_Delete',
        RoutingAreaLocation_Refresh: 'CATRoutingAreaLocation_Refresh',

        Read_AreaNotIn: 'CATRoutingArea_AreaNotIn_AreaList',
        AreaLocation_Copy: 'CATRoutingArea_AreaLocation_Copy',

        ExcelInit: 'CATRoutingArea_ExcelInit',
        ExcelChange: 'CATRoutingArea_ExcelChange',
        ExcelImport: 'CATRoutingArea_ExcelImport',
        ExcelApprove: 'CATRoutingArea_ExcelApprove',
    },
    Data: {
        Province: {},
        District: {}
    },
    ExcelKey: {
        Resource: "CATRoutingAera",
        Area: "CATRoutingAera",
    }
}

//#endregion

angular.module('myapp').controller('CATRoutingArea_IndexCtrl', ['$rootScope', '$scope', '$http', '$location', '$state', '$timeout', function ($rootScope, $scope, $http, $location, $state, $timeout) {
    if ($rootScope.IsPageComplete != true) return;
    Common.Log('CATRoutingArea_IndexCtrl');
    $rootScope.IsLoading = false;

    $scope.Auth = $rootScope.GetAuth();

    $scope.Item = null;
    $scope.routingAreaID = 0;
    $scope.AreaDetailItem = {};
    $scope.FollowHasChoose = false;
    $scope.RoutingName = "";


    $scope.area_gridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.CAT,
            method: _CATArea.URL.Read,
            model: {
                id: 'ID',
                fields: {
                    Code: { type: 'string' },
                    AreaName: { type: 'string' },
                    FollowID: { type: 'number' }
                   
                }
            }
        }),
        height: '100%', pageable: Common.PageSize, sortable: true, columnMenu: false, resizable: true, reorderable: true, filterable: { mode: 'row' },
        toolbar: kendo.template($('#grid_toolbar').html()), editable: 'inline',
        columns: [
            {
                title: ' ', width: '182px',
                template: '<a href="/" ng-click="Edit_Click($event,win,area_grid,vform )" class="k-button"><i class="fa fa-pencil"></i></a>' +
                    '<a href="/" ng-click="Destroy_Click($event,dataItem)" class="k-button"><i class="fa fa-trash"></i></a>' +
                    '<a href="/" ng-click="AreaShowDetail_Click($event,AreaDetail_win,area_grid)" class="k-button"><i class="fa fa-info-circle"></i></a>' +
                    '<a href="/" ng-click="AreaShowLocation_Click($event,AreaLocation_win,area_grid)" class="k-button"><i class="fa fa-map-marker"></i></a>' +
                    '<a href="/" ng-click="AreaNotInRefresh_Click($event,dataItem)" class="k-button"><i class="fa fa-refresh"></i></a>',
                filterable: false, sortable: false
            },           
              {
                  field: 'Code', title: 'Mã khu vực', width: 200,
                  filterable: { cell: { operator: 'contains', showOperators: false } }
              },
            {
                field: 'AreaName', title: 'Tên khu vực',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
             {
                 field: 'FollowID', title: 'Mã Follow',
                 filterable: { cell: { operator: 'contains', showOperators: false } }
             },
        ]
    };
    //#region Action
    $scope.ShowSetting = function ($event, grid) {
        $event.preventDefault();

        $rootScope.ShowSetting({
            ListView: views.TypeOfReasonName,
            event: $event,
            grid: grid,
            current: $state.current
        });
    };
    //#endregion
    
    $scope.AreaNotInRefresh_Click = function ($event, item) {
        $event.preventDefault();
        if (Common.HasValue(item)) {

            $rootScope.Message({
                Type: Common.Message.Type.Confirm,
                NotifyType: Common.Message.NotifyType.SUCCESS,
                Title: 'Thông báo',
                Msg: 'Bạn muốn làm mới vị trí',
                Close: null,
                Ok: function () {
                    $rootScope.IsLoading = true;
                    Common.Services.Call($http, {
                        url: Common.Services.url.CAT,
                        method: _CATArea.URL.RoutingAreaLocation_Refresh,
                        data: { 'id': item.ID },
                        success: function (res) {
                            $rootScope.IsLoading = false;
                            $rootScope.Message({
                                Type: Common.Message.Type.Notify,
                                NotifyType: Common.Message.NotifyType.SUCCESS,
                                Title: 'Thông báo',
                                Msg: 'Thành công',
                                Close: null,
                                Ok: null
                            })
                        }
                    });
                }
            })
        }
    }

    $scope.AddNew_Click = function ($event, win, vform) {
        $event.preventDefault();
        $scope.LoadItem(win, 0, vform);
    }

    $scope.Edit_Click = function ($event, win, grid, vform) {
        $scope.RoutingName = "";
        $event.preventDefault();
        Common.Log("Edit_Click");
        var tr = $($event.target).closest('tr');
        var item = grid.dataItem(tr);
        if (Common.HasValue(item))
            $scope.RoutingName = item.AreaName;
            $scope.routingAreaID = item.ID;
            $scope.LoadItem(win, item.ID, vform);
    }
    $scope.LoadItem = function (win, id, vform) {
        $rootScope.IsLoading = true;
        vform({ clear: true });
        Common.Services.Call($http, {
            url: Common.Services.url.CAT,
            method: _CATArea.URL.Get,
            data: { 'ID': id },
            success: function (res) {
                $rootScope.IsLoading = false;
                $scope.Item = res;
                win.center().open();
            }
        });
    }

    $scope.cboTypeOptions = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true,
        dataTextField: 'ValueOfVar', dataValueField: 'ID', minLength: 3,
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    ValueOfVar: { type: 'string' },
                }
            }
        })
    }

    Common.ALL.Get($http, {
        url: Common.ALL.URL.SYSVarTypeOfReason,
        success: function (data) {
            $scope.cboTypeOptions.dataSource.data(data);
        }
    })

    $scope.Destroy_Click = function ($event, item) {
        $event.preventDefault();
        Common.Log("Destroy_Click");
        $rootScope.IsLoading = true;
        if (Common.HasValue(item)) {
            Common.Services.Call($http, {
                url: Common.Services.url.CAT,
                method: _CATArea.URL.Delete,
                data: { 'item': item },
                success: function (res) {
                    $rootScope.Message({ Msg: "Đã xóa!" });
                    $rootScope.IsLoading = false;
                    $scope.area_gridOptions.dataSource.read();
                }
            });
        }
    }

    $scope.Save_Click = function ($event, win, vform) {
        $event.preventDefault();      
        if (vform()) {
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.CAT,
                method: _CATArea.URL.Save,
                data: { item: $scope.Item },
                success: function (res) {
                    win.close();
                    $rootScope.Message({ Msg: "Đã cập nhật!" });
                    $rootScope.IsLoading = false;
                    $scope.area_gridOptions.dataSource.read();
                }
            });
        }
    }

    //region location
    $scope.AreaShowLocation_Click = function ($event,win, grid) {
        $event.preventDefault();
        var tr = $($event.target).closest('tr');
        var item = grid.dataItem(tr);
        if (Common.HasValue(item)) {
            $scope.routingAreaID = item.ID;
            win.center();
            win.open();
            $scope.AreaLocation_gridOptions.dataSource.read();
            $scope.AreaLocation_grid.refresh();
        }
    }

    $scope.LocationHasChoose = false;
    $scope.AreaLocation_gridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.CAT,
            method: _CATArea.URL.ReadLocation,
            readparam: function () {
                return {
                    routingAreaID: $scope.routingAreaID
                }
            },
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number', editable: false },
                    IsChoose: { type: 'boolean', },
                }
            }
        }),
        height: '100%', pageable: true, sortable: true, columnMenu: false, resizable: true, reorderable: true, filterable: { mode: 'row' },
        columns: [
            {
                title: ' ', width: '35px',
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,AreaLocation_grid,location_GridChoose_Change)" />',
                headerAttributes: { style: 'text-align: center;' }, attributes: { style: 'text-align: center;' },
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,AreaLocation_grid,location_GridChoose_Change)" />',
                filterable: false, sortable: false
            },
            { field: 'LocationCode', title: 'Mã', width: '300px', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'LocationName', title: 'Tên', width: '300px', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'Address', title: 'Địa chỉ', width: '350px', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'Country', title: 'Quốc gia', width: '150px', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'Province', title: 'Tỉnh thành', width: '150px', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'District', title: 'Quận huyện', width: '150px', filterable: { cell: { operator: 'contains', showOperators: false } } }
        ]
    };

    $scope.location_GridChoose_Change = function ($event, grid, hasChoose) {
        $scope.LocationHasChoose = hasChoose;
    }

    $scope.Location_AddNew = function ($event, win) {
        $event.preventDefault();
        win.center().open();
        $scope.locationNotIn_GridOptions.dataSource.read();
        $scope.locationNotIn_Grid.refresh();
    }
    $scope.Follow_Click = function ($event, win) {
        $event.preventDefault();
        win.center().open();
        $timeout(function () {
            $scope.Follow_GridOptions.dataSource.read();
            $scope.Follow_Grid.resize();
        }, 100);
    }
    $scope.Follow_GridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.CAT,
            method: _CATArea.URL.LocationNotIn_List,
            readparam: function () {
                return {
                    routingAreaID: $scope.routingAreaID
                }
            },
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number', editable: false },
                    IsChoose: { type: 'boolean', },
                }
            }
        }),
        height: '100%', pageable: true, sortable: true, columnMenu: false, resizable: true, reorderable: true, filterable: { mode: 'row' }, selectable: true,
        change:function(e){
            var grid = e.sender;
            var selectedData = grid.dataItem(grid.select());
            $scope.FollowHasChoose = true;
            console.log(selectedData.id);
        },
        columns: [
            { field: 'Code', title: 'Mã', width: '300px', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'Location', title: 'Tên', width: '300px', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'Address', title: 'Địa chỉ', width: '350px', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'CountryName', title: 'Quốc gia', width: '150px', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'ProvinceName', title: 'Tỉnh thành', width: '150px', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'DistrictName', title: 'Quận huyện', width: '150px', filterable: { cell: { operator: 'contains', showOperators: false } } }
        ]
    };

    $scope.Follow_Choose_Click = function ($event, grid, win) {
        $event.preventDefault();
        var item = grid.dataItem(grid.select()[0]);
        console.log(item);

        if (Common.HasValue(item)) {
            var lst = [];
            lst.push(item.ID)
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.CAT,
                method: _CATArea.URL.LocationNotIn_Save,
                data: { routingAreaID: $scope.routingAreaID, lst: lst },
                success: function (res) {
                    $rootScope.IsLoading = false;
                    $scope.FollowHasChoose = false;
                    $scope.Follow_GridOptions.dataSource.read();
                    win.close();
                    $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                },
                error: function (res) {
                    $rootScope.IsLoading = false;
                }
            });

        }
    }

    $scope.locationNotIn_GridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.CAT,
            method: _CATArea.URL.LocationNotIn_List,
            readparam: function () {
                return {
                    routingAreaID: $scope.routingAreaID
                }
            },
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number', editable: false },
                    IsChoose: { type: 'boolean', },
                }
            }
        }),
        height: '100%', pageable: Common.PageSize, sortable: true, columnMenu: false, resizable: true, reorderable: true, filterable: { mode: 'row' },
        columns: [
            {
                title: ' ', width: '35px',
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,locationNotIn_Grid,locationNotIn_GridChoose_Change)" />',
                headerAttributes: { style: 'text-align: center;' }, attributes: { style: 'text-align: center;' },
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,locationNotIn_Grid,locationNotIn_GridChoose_Change)" />',
                filterable: false, sortable: false
            },
            { field: 'Code', title: 'Mã', width: '300px', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'Location', title: 'Tên', width: '300px', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'Address', title: 'Địa chỉ', width: '350px', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'CountryName', title: 'Quốc gia', width: '150px', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'ProvinceName', title: 'Tỉnh thành', width: '150px', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'DistrictName', title: 'Quận huyện', width: '150px', filterable: { cell: { operator: 'contains', showOperators: false } } }
        ]
    };

    $scope.LocationNotInHasChoose = false;
    $scope.locationNotIn_GridChoose_Change = function ($event, grid, hasChoose) {
        $scope.LocationNotInHasChoose = hasChoose;
    }

    $scope.locationNotIn_Save_Click = function ($event, win, grid) {
        $event.preventDefault();
        var data = grid.dataSource.data();
        var datasend = [];
        Common.Data.Each(data, function (o) {
            if (o.IsChoose) datasend.push(o.ID);
        })
        if (datasend.length > 0) {
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.CAT,
                method: _CATArea.URL.LocationNotIn_Save,
                data: { routingAreaID: $scope.routingAreaID, lst: datasend },
                success: function (res) {
                    $rootScope.IsLoading = false;
                    $scope.AreaLocation_gridOptions.dataSource.read();
                    win.close();
                    $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                },
                error: function (res) {
                    $rootScope.IsLoading = false;
                }
            });

        }
    }

    $scope.Location_Delete = function ($event, grid) {
        $event.preventDefault();
        var data = grid.dataSource.data();
        var datasend = [];
        Common.Data.Each(data, function (o) {
            if (o.IsChoose) datasend.push(o.ID);
        })
        if (datasend.length > 0) {
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.CAT,
                method: _CATArea.URL.Location_Delete,
                data: { lst: datasend },
                success: function (res) {
                    $rootScope.IsLoading = false;
                    $scope.AreaLocation_gridOptions.dataSource.read();
                    $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                },
                error: function (res) {
                    $rootScope.IsLoading = false;
                }
            });

        }
    }

    $scope.AreaLocationAreaNotIn_win_gridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.CAT,
            method: _CATArea.URL.Read_AreaNotIn,
            readparam: function () { return { id: $scope.routingAreaID } },
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number', editable: false },
                }
            }
        }),
        height: '100%', pageable: true, sortable: true, columnMenu: false, resizable: true, reorderable: true, filterable: { mode: 'row' },
        selectable: 'row',
        columns: [
            { field: 'Code', title: '{{RS.CUSRouting.Code}}', width: '160px', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'AreaName', title: '{{RS.CUSRouting.AreaName}}', width: '450px', filterable: { cell: { operator: 'contains', showOperators: false } } }
        ]
    }

    $scope.AreaLocation_Copy = function ($event, win) {
        $event.preventDefault();
        win.center().open();
        $scope.AreaLocationAreaNotIn_win_gridOptions.dataSource.read();
        $scope.AreaLocationAreaNotIn_win_grid.refresh();
    }


    $scope.AreaLocationAreaNotIn_AddChooseClick = function ($event, win, grid) {
        $event.preventDefault();
        var item = grid.dataItem(grid.select());
        var datasend = [];
        if (Common.HasValue(item))
            datasend.push(item.ID);
        if (datasend.length > 0) {
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.CAT,
                method: _CATArea.URL.AreaLocation_Copy,
                data: { id: $scope.routingAreaID, lst: datasend },
                success: function (res) {
                    $rootScope.IsLoading = false;
                    $scope.AreaLocation_gridOptions.dataSource.read();
                    win.close();
                    $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                },
                error: function (res) {
                    $rootScope.IsLoading = false;
                }
            });
        }
    }
    //#endregion

    //#region Detail


    $scope.AreaShowDetail_Click = function ($event, win, grid) {
        $event.preventDefault();
        var tr = $($event.target).closest('tr');
        var item = grid.dataItem(tr);
        if (Common.HasValue(item)) {
            $scope.routingAreaID = item.ID;
            win.center();
            win.open();
            $scope.AreaDetail_gridOptions.dataSource.read();
            $scope.AreaDetail_grid.refresh();
        }
    }

    $scope.AreaDetail_gridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.CAT,           
            method: _CATArea.URL.ReadDetail,
            readparam: function () {
                return {
                    routingAreaID: $scope.routingAreaID
                }
            },
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number', editable: false },
                }
            }
        }),
        height: '100%', pageable: true, sortable: true, columnMenu: false, resizable: true, reorderable: true, filterable: { mode: 'row' },
        columns: [
            {
                title: ' ', width: '100px',
                template: '<a href="/" ng-click="EditAreaDetail_Click($event,AreaDetailEdit_winPopup,AreaDetail_grid,vform_AreaDetail)" class="k-button"><i class="fa fa-pencil"></i></a>' +
                    '<a href="/" ng-click="DestroyAreaDetail_Click($event,dataItem)" class="k-button"><i class="fa fa-trash"></i></a>',
                filterable: false, sortable: false
            },
            { field: 'CountryName', title: 'Quốc gia', width: '160px', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'ProvinceName', title: 'Tỉnh thành', width: '160px', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'DistrictName', title: 'Quận huyện', width: '150px', filterable: { cell: { operator: 'contains', showOperators: false } } }
        ]
    };
    

    $scope.EditAreaDetail_Click = function ($event, win, grid,vform) {
        $event.preventDefault();
        var tr = $($event.target).closest('tr');
        var id = 0;
        var item = grid.dataItem(tr);
        if (Common.HasValue(item)) id = item.ID;
        $scope.AreaDetailLoadItem(win, id, vform);
    }

    $scope.DestroyAreaDetail_Click = function ($event, item) {
        $event.preventDefault();        
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
                    method: _CATArea.URL.DeleteDetail,
                    data: { 'item': item },
                    success: function (res) {
                        $rootScope.IsLoading = false;
                        $rootScope.Message({ Msg: "Xóa thành công!" });
                        $scope.AreaDetail_gridOptions.dataSource.read();
                    }
                });
            }
        });
    }

    $scope.AreaDetailEdit_winPopupSave_Click = function ($event,win,vform) {
        $event.preventDefault();       
        if (vform()) {
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.CAT,
                method: _CATArea.URL.SaveDetail,
                data: { item: $scope.AreaDetailItem, ID: $scope.routingAreaID },
                success: function (res) {
                    win.close();
                    $rootScope.IsLoading = false;
                    $rootScope.Message({ Msg: 'Thanh cong', NotifyType: Common.Message.NotifyType.SUCCESS });
                    $scope.AreaDetail_gridOptions.dataSource.read();
                }
            });
        }
    }

    $scope.AreaNotInDetail_AddNewClick = function ($event, win, vform) {
        $event.preventDefault();
        $scope.AreaDetailLoadItem(win, 0, vform);
    }

    $scope.AreaDetailLoadItem = function (win, id, vform) {       
        vform({ clear: true });
        $rootScope.IsLoading = true;
        Common.Services.Call($http, {
            url: Common.Services.url.CAT,
            method: _CATArea.URL.GetDetail,
            data: { 'id': id },
            success: function (res) {
                $rootScope.IsLoading = false;
                $scope.AreaDetailItem = res;
                $scope.LoadRegionData($scope.AreaDetailItem);
                win.center();
                win.open();
            }
        });
    }

   

    $scope.LoadRegionData = function (item) {
        Common.Log("LoadRegionData");
        try {
            var countryID = item.CountryID;
            var provinceID = item.ProvinceID;
            var districtID = item.DistrictID;
            var wardID = item.WardID;

            var data = _CATArea.Data.Province[countryID];
            $scope.AreaDetailEdit_win_cboProvinceOptions.dataSource.data(data);
            if (Common.HasValue(provinceID)) {
                if (provinceID < 0) {
                    provinceID = data[0].ID;
                }
            }
            $timeout(function () {
                item.ProvinceID = provinceID;
            }, 1)

            data = _CATArea.Data.District[provinceID];
            $scope.AreaDetailEdit_win_cboDistrictOptions.dataSource.data(data);
            if (Common.HasValue(districtID)) {
                if (districtID < 0) {
                    districtID = data[0].ID;
                }
            }
            $timeout(function () {
                item.DistrictID = districtID;
            }, 1)
        }
        catch (e) { }
    }

    $scope.AreaDetailEdit_win_cboCountryOptions = {
        autoBind: true,
        valuePrimitive: true,
        ignoreCase: true,
        filter: 'contains',
        suggest: true,
        dataTextField: 'CountryName',
        dataValueField: 'ID',
        minLength: 3,
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    CountryName: { type: 'string' },
                }
            }
        }),
        change: function (e) {
            var cbo = this;
            if (e.sender.selectedIndex >= 0) {
                $scope.AreaDetailItem.ProvinceID = -1;
                $scope.AreaDetailItem.DistrictID = -1;
                $scope.AreaDetailItem.WardID = -1;
                $scope.LoadRegionData($scope.AreaDetailItem);
            }
            else {
                $scope.AreaDetailItem.CountryID = "";
                $scope.AreaDetailItem.ProvinceID = "";
                $scope.AreaDetailItem.DistrictID = "";
                $scope.AreaDetailItem.WardID = "";
                $scope.LoadRegionData($scope.AreaDetailItem);
            }
        }
    }


    Common.ALL.Get($http, {
        url: Common.ALL.URL.Country,
        success: function (data) {
            $scope.AreaDetailEdit_win_cboCountryOptions.dataSource.data(data);
        }
    })

    $scope.AreaDetailEdit_win_cboProvinceOptions = {
        autoBind: true,
        valuePrimitive: true,
        ignoreCase: true,
        filter: 'contains',
        suggest: true,
        dataTextField: 'ProvinceName',
        dataValueField: 'ID',
        minLength: 3,
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    ProvinceName: { type: 'string' },
                }
            }
        }),
        change: function (e) {
            var cbo = this;
            if (e.sender.selectedIndex >= 0) {
                $scope.AreaDetailItem.DistrictID = -1;
                $scope.AreaDetailItem.WardID = -1;
                $scope.LoadRegionData($scope.AreaDetailItem);
            }
            else {
                $scope.AreaDetailItem.ProvinceID = "";
                $scope.AreaDetailItem.DistrictID = "";
                $scope.AreaDetailItem.WardID = "";
                $scope.LoadRegionData($scope.AreaDetailItem);
            }
        }
    }

    Common.ALL.Get($http, {
        url: Common.ALL.URL.Province,
        success: function (data) {
            _CATArea.Data.Province = {};
            angular.forEach(data, function (obj, indx) {
                if (Common.HasValue(_CATArea.Data.Province[obj.CountryID]))
                    _CATArea.Data.Province[obj.CountryID].push(obj);
                else _CATArea.Data.Province[obj.CountryID] = [obj];
            })
        }
    })

    $scope.AreaDetailEdit_win_cboDistrictOptions = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true, dataTextField: 'DistrictName', dataValueField: 'ID',
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    DistrictName: { type: 'string' },
                }
            }
        }),
        change: function (e) {
            var cbo = this;
            if (e.sender.selectedIndex >= 0) {
                $scope.AreaDetailItem.WardID = "";
            }
            else {
                $scope.AreaDetailItem.DistrictID = "";
                $scope.AreaDetailItem.WardID = "";
                $scope.LoadRegionData($scope.AreaDetailItem);
            }
        }
    }

    Common.ALL.Get($http, {
        url: Common.ALL.URL.District,
        success: function (data) {
            _CATArea.Data.District = {};
            angular.forEach(data, function (obj, indx) {
                if (Common.HasValue(_CATArea.Data.District[obj.ProvinceID]))
                    _CATArea.Data.District[obj.ProvinceID].push(obj);
                else _CATArea.Data.District[obj.ProvinceID] = [obj];
            })
        }
    })
    //#endregion

    //#region excel
    $scope.ExcelLocationArea_Click = function ($event) {
        $event.preventDefault();
        $rootScope.IsLoading = true;
        Common.Services.Call($http, {
            url: Common.Services.url.CAT,
            method: _CATArea.URL.ExcelExport_RouteAreaLocation,
            data: {},
            success: function (res) {
                $rootScope.IsLoading = false;
                $rootScope.DownloadFile(res);
            },
            error: function (res) {
                $rootScope.IsLoading = false;
            }
        });
    }

    $scope.ExcelRouteArea_Click = function ($event) {
        $event.preventDefault();
        var lstMessageError = [];
        for (var i = 0; i < 30; i++) {
            var resource = $rootScope.RS[_CATArea.ExcelKey.Resource + '_' + i];
            if (Common.HasValue(resource))
                lstMessageError.push(resource);
        }
        if (lstMessageError.length == 0) {
            lstMessageError = [
                '[Mã khu vực] không được trống và > 1000 ký tự',
                'Mã khu vực bị trùng',
                '[Tên khu vực] không được trống và > 10000 ký tự',
                '[Chi tiết 1] sai tỉnh',
                '[Chi tiết 1] có huyện nhưng không có tỉnh',
                '[Chi tiết 1] có tỉnh nhưng sai huyện',
                '[Chi tiết 1] phải có khu vực bao quát khi thêm chi tiết',
                '[Chi tiết 1] phải có khu vực bao quát khi thêm chi tiết',
                '[Chi tiết 1] Trùng tỉnh huyện',
                '[Chi tiết 1] Phải có khu vực bao quát',
                '[Chi tiết 1] phải có khu vực chi tiết trên file',
                '[Chi tiết 2] sai tỉnh',
                '[Chi tiết 2] có huyện nhưng không có tỉnh',
                '[Chi tiết 2] có tỉnh nhưng sai huyện',
                '[Chi tiết 2] phải có khu vực bao quát khi thêm chi tiết',
                '[Chi tiết 2] phải có khu vực bao quát khi thêm chi tiết',
                '[Chi tiết 2] Trùng tỉnh huyện',
                '[Chi tiết 2] Phải có khu vực bao quát',
                '[Chi tiết 2] phải có khu vực chi tiết trên file',
                '[Chi tiết 3] sai tỉnh',
                '[Chi tiết 3] có huyện nhưng không có tỉnh',
                '[Chi tiết 3] có tỉnh nhưng sai huyện',
                '[Chi tiết 3] phải có khu vực bao quát khi thêm chi tiết',
                '[Chi tiết 3] phải có khu vực bao quát khi thêm chi tiết',
                '[Chi tiết 3] Trùng tỉnh huyện',
                '[Chi tiết 3] Phải có khu vực bao quát',
                '[Chi tiết 3] phải có khu vực chi tiết trên file',
                '[Chi tiết 4] sai tỉnh',
                '[Chi tiết 4] có huyện nhưng không có tỉnh',
                '[Chi tiết 4] có tỉnh nhưng sai huyện',
                '[Chi tiết 4] phải có khu vực bao quát khi thêm chi tiết',
                '[Chi tiết 4] phải có khu vực bao quát khi thêm chi tiết',
                '[Chi tiết 4] Trùng tỉnh huyện',
                '[Chi tiết 4] Phải có khu vực bao quát',
                '[Chi tiết 4] phải có khu vực chi tiết trên file',
                '[Chi tiết 5] sai tỉnh',
                '[Chi tiết 5] có huyện nhưng không có tỉnh',
                '[Chi tiết 5] có tỉnh nhưng sai huyện',
                '[Chi tiết 5] phải có khu vực bao quát khi thêm chi tiết',
                '[Chi tiết 5] phải có khu vực bao quát khi thêm chi tiết',
                '[Chi tiết 5] Trùng tỉnh huyện',
                '[Chi tiết 5] Phải có khu vực bao quát',
                '[Chi tiết 5] phải có khu vực chi tiết trên file',
                '[Chi tiết 6] sai tỉnh',
                '[Chi tiết 6] có huyện nhưng không có tỉnh',
                '[Chi tiết 6] có tỉnh nhưng sai huyện',
                '[Chi tiết 6] phải có khu vực bao quát khi thêm chi tiết',
                '[Chi tiết 6] phải có khu vực bao quát khi thêm chi tiết',
                '[Chi tiết 6] Trùng tỉnh huyện',
                '[Chi tiết 6] Phải có khu vực bao quát',
                '[Chi tiết 6] phải có khu vực chi tiết trên file',
                '[Chi tiết 7] sai tỉnh',
                '[Chi tiết 7] có huyện nhưng không có tỉnh',
                '[Chi tiết 7] có tỉnh nhưng sai huyện',
                '[Chi tiết 7] phải có khu vực bao quát khi thêm chi tiết',
                '[Chi tiết 7] phải có khu vực bao quát khi thêm chi tiết',
                '[Chi tiết 7] Trùng tỉnh huyện',
                '[Chi tiết 7] Phải có khu vực bao quát',
                '[Chi tiết 7] phải có khu vực chi tiết trên file',
                '[Chi tiết 8] sai tỉnh',
                '[Chi tiết 8] có huyện nhưng không có tỉnh',
                '[Chi tiết 8] có tỉnh nhưng sai huyện',
                '[Chi tiết 8] phải có khu vực bao quát khi thêm chi tiết',
                '[Chi tiết 8] phải có khu vực bao quát khi thêm chi tiết',
                '[Chi tiết 8] Trùng tỉnh huyện',
                '[Chi tiết 8] Phải có khu vực bao quát',
                '[Chi tiết 8] phải có khu vực chi tiết trên file',
                '[Chi tiết 9] sai tỉnh',
                '[Chi tiết 9] có huyện nhưng không có tỉnh',
                '[Chi tiết 9] có tỉnh nhưng sai huyện',
                '[Chi tiết 9] phải có khu vực bao quát khi thêm chi tiết',
                '[Chi tiết 9] phải có khu vực bao quát khi thêm chi tiết',
                '[Chi tiết 9] Trùng tỉnh huyện',
                '[Chi tiết 9] Phải có khu vực bao quát',
                '[Chi tiết 9] phải có khu vực chi tiết trên file',
                '[Chi tiết 10] sai tỉnh',
                '[Chi tiết 10] có huyện nhưng không có tỉnh',
                '[Chi tiết 10] có tỉnh nhưng sai huyện',
                '[Chi tiết 10] phải có khu vực bao quát khi thêm chi tiết',
                '[Chi tiết 10] phải có khu vực bao quát khi thêm chi tiết',
                '[Chi tiết 10] Trùng tỉnh huyện',
                '[Chi tiết 10] Phải có khu vực bao quát',
                '[Chi tiết 10] phải có khu vực chi tiết trên file',
            ];
        }

        $rootScope.excelShare.Init({
            functionkey: _CATArea.ExcelKey.Area,
            params: {},
            rowStart: 2,
            colCheckChange: 30,
            url: Common.Services.url.CAT,
            methodInit: _CATArea.URL.ExcelInit,
            methodChange: _CATArea.URL.ExcelChange,
            methodImport: _CATArea.URL.ExcelImport,
            methodApprove: _CATArea.URL.ExcelApprove,
            lstMessageError: lstMessageError,
            Changed: function () { },
            Approved: function () {
                $scope.area_gridOptions.dataSource.read();
            }
        });
    };
    //#endregion

    $scope.Close_Click = function ($event, win) {
        $event.preventDefault();
        win.close();
    };
  
}]);