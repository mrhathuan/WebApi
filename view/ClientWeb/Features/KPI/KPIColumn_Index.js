/// <reference path="~/Scripts/jquery-1.9.1.intellisense.js" />
/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />

var _KIPColumn = {
    URL: {
        Read: 'KPIColumn_List',
        Delete: 'KPIColumn_Delete',
        Save: 'KPIColumn_Save',
        Get: 'KPIColumn_Get',
        KPIFieldList: 'KPIField_List',
        GetKPPI:'KPIKPI_Get',
    },
    Param: {
        ID: -1
    },

}
angular.module('myapp').controller('KPIColumn_IndexCtrl', ['$rootScope', '$scope', '$http', '$location', '$state', '$timeout', '$stateParams', function ($rootScope, $scope, $http, $location, $state, $timeout, $stateParams) {
    if ($rootScope.IsPageComplete != true) return;
    Common.Log('KPIColumn_IndexCtrl');
    $rootScope.IsLoading = false;

    $scope.Auth = $rootScope.GetAuth();
    $scope.ParamEdit = { ID: -1 }
    $scope.KPIColumnItem = null;
    _KIPColumn.Param = $.extend(true, _KIPColumn.Param, $state.params);
    $scope.Item = { ID: -1 };
    Common.Services.Call($http, {
       
        url: Common.Services.url.KPI,
        method: _KIPColumn.URL.GetKPPI,
        data: { ID: _KIPColumn.Param.ID },
        success: function (res) {
            $scope.Item = res;
            $scope.LoadFieldList(res.TypeOfKPIID);
        }
    });

    $scope.KPIColumn_gridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.KPI,
            method: _KIPColumn.URL.Read,
            readparam: function () { return { ID: _KIPColumn.Param.ID } },
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number', editable: false, nullable: true },
                    Code: { type: 'string' },
                    ColumnName: { type: 'string' },
                    FieldID: { type: 'number' },
                    FieldName: { type: 'string' },
                    KPIColumnTypeName: { type: 'string' },
                    KPIColumnTypeID: { type: 'string' },
                    ExprData: { type: 'string' },
                }
            }
        }),

        

        height: '100%', pageable: true, sortable: true, columnMenu: false, resizable: true, reorderable: true, filterable: { mode: 'row' },
     
        columns: [
            {
                
                title: ' ', width: '90px',
                template: '<a href="/" ng-click="KIPColumnEdit_Click($event,KPIColumn_win,dataItem)" class="k-button"><i class="fa fa-pencil"></i></a>' +
                '<a href="/" ng-click="KIPColumnDestroy_Click($event,KPIColumn_grid)" class="k-button"><i class="fa fa-trash"></i></a>',
                filterable: false, sortable: false
            },
            {
                field: 'Code', title: 'Mã', width: 200,
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'ColumnName', title: 'Tên', width: 200,
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'FieldName', title: 'Tên trường', width: 200,
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            { title: '', filterable: false, sortable: false }
        ]
    };

    // thêm mới
    $scope.KPIColumn_AddNew_Click = function ($event, win, vform) {

        $event.preventDefault();
        $scope.LoadItem(win, 0);
    }
    $scope.LoadItem = function (win, id) {
        $rootScope.IsLoading = true;
        Common.Services.Call($http, {
            url: Common.Services.url.KPI,
            method: _KIPColumn.URL.Get,
          
            data: { 'ID': id },
            success: function (res) {
                $rootScope.IsLoading = false;
                
                $scope.KPIColumnItem = res;
                win.center();
                win.open();
            }
        });
    }



    $scope.KPIColumn_cboColumnTypeID_Options = {
       
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
        
        url: Common.ALL.URL.ALL_SYSVarColumnType,
        success: function (data) {
           
            $scope.KPIColumn_cboColumnTypeID_Options.dataSource.data(data);
        }
    })
   

    $scope.KPIColumn_cboColumnFieldID_Options =
    {
        autoBind: true,
        valuePrimitive: true,
        ignoreCase: true,
        filter: 'contains',
        suggest: true,
        dataTextField: 'FieldName',
        dataValueField: 'ID',
        dataSource: Common.DataSource.Local({

            data: [],
            model: {
                id: 'ID',
                fields: {

                    ID: { type: 'number' },
                    FieldName: { type: 'string' },
                }
            }
        }),
        change: function (e) {
        }
    }
    $scope.LoadFieldList = function (_typeID) {
        Common.Services.Call($http, {

            url: Common.Services.url.KPI,
            method: _KIPColumn.URL.KPIFieldList,
            data: { typeID: _typeID },
            success: function (data) {
                $scope.KPIColumn_cboColumnFieldID_Options.dataSource.data(data);
            }
        });
    }
    //#region sữa, xóa
    $scope.KIPColumnEdit_Click = function ($event, win, data) {
        
        $event.preventDefault();
        $scope.LoadItem(win, data.ID);
    }
    $scope.KIPColumnDestroy_Click = function ($event, grid) {
        $event.preventDefault();
        var tr = $($event.target).closest('tr');
        var item = grid.dataItem(tr);
        if (Common.HasValue(item)) {
            if (confirm('Bạn muốn xóa các dữ liệu đã chọn ?')) {
                $rootScope.IsLoading = true;
                Common.Services.Call($http, {
                    url: Common.Services.url.KPI,
                    method: _KIPColumn.URL.Delete,
                    data: { 'ID': item.ID },
                    success: function (res) {
                        $rootScope.IsLoading = false;
                        Common.Services.Error(res, function (res) {
                            $rootScope.Message({ Msg: 'Đã xóa thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                            $scope.KPIColumn_gridOptions.dataSource.read();
                        })
                    }
                });
            }
        }
    }

    //#endregion

    //#region lưu, đóng
    $scope.KPIColumn_win_SaveClick = function ($event, win, vform) {
        $event.preventDefault();
        if (vform()) {
            $scope.KPIColumnItem.KPIColumnTypeName = $scope.KPIColmn_cboTypeOfKPIID.text()
            $scope.KPIColumnItem.FieldName = $scope.KPIColmn_cboFieldID.text()
            
            if (Common.HasValue($scope.KPIColumnItem.KPIColumnTypeID) && $scope.KPIColumnItem.KPIColumnTypeID > 0) {
                if (Common.HasValue($scope.KPIColumnItem.FieldID) && $scope.KPIColumnItem.FieldID > 0) {
                    $rootScope.IsLoading = true;
                    Common.Services.Call($http, {
                        url: Common.Services.url.KPI,
                        method: _KIPColumn.URL.Save,
                        data: { item: $scope.KPIColumnItem, KPIID: $scope.Item.ID },
                        success: function (res) {
                            $rootScope.IsLoading = false;
                            Common.Services.Error(res, function (res) {
                                $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                                win.close();
                                $scope.KPIColumn_gridOptions.dataSource.read();
                            })
                        }
                    });
                }
            }
            else {
                $rootScope.Message({ Msg: 'Chưa chọn Field Name', NotifyType: Common.Message.NotifyType.ERROR });
            }
        }
        else {
            $rootScope.Message({ Msg: 'Chưa chọn  Column Type', NotifyType: Common.Message.NotifyType.ERROR });
        }

    }
    $scope.KPIColumn_win_CloseClick = function ($event, win) {
        $event.preventDefault();

        win.close();
    };
    //#endregion

}]);
