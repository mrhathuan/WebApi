/// <reference path="~/Scripts/jquery-1.9.1.intellisense.js" />
/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />

var _ORDOrderTypeOfDocument = {
    URL: {
        Read_List: 'ORDOrder_TypeOfDocument_List',
        
        Save: 'ORDOrder_TypeOfDocument_Save',
        Get: 'ORDOrder_TypeOfDocument_Get',
        Destroy_List: 'ORDOrder_TypeOfDocument_DeleteList',
    },
}


angular.module('myapp').controller('ORDInspection_TypeOfCtrl', ['$rootScope', '$scope', '$http', '$location', '$state', '$timeout', '$stateParams', function ($rootScope, $scope, $http, $location, $state, $timeout, $stateParams) {
    Common.Log('ORDInspection_TypeOfCtrl');

    $rootScope.IsLoading = false;
    $scope.Search = {},
    $scope.HasChoose = false;
    $scope.Item = null


    $scope.ORDOrderTypeOfDocumen_gridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.ORD,
            method: _ORDOrderTypeOfDocument.URL.Read_List,
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    Code: { type: 'string' },
                    TypeName: { type: 'string' },
                    TypeOfWAInspectionStatusName: {type:'string'},
                    SortOrder: { type: 'number' },
                    IsChoose: { type: 'boolean' },
                }
            }
        }),
        
        height: '100%', pageable: true, sortable: true, columnMenu: false, resizable: true, reorderable: true, filterable: { mode: 'row' },
        columns: [
            {
                title: ' ', width: '35px',
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,ORDOrderTypeOfDocumen_grid,settingReport_GridChoose_Change)" />',
                headerAttributes: { style: 'text-align: center;' }, attributes: { style: 'text-align: center;' },
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,ORDOrderTypeOfDocumen_grid,settingReport_GridChoose_Change)" />',
                filterable: false, sortable: false
            },
            {
                title: ' ', width: '120px',
                template: '<a href="/" ng-click="ORDOrderTypeOfDocumentEdit_Click($event,SettingReport_win,dataItem,Setting_vform)" class="k-button"><i class="fa fa-pencil"></i></a>',
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
                 field: 'TypeOfWAInspectionStatusName', title: 'Trạng thái', width: 200,
                 filterable: { cell: { operator: 'contains', showOperators: false } }
             },
             {
                 field: 'SortOrder', title: 'Thứ tự', width: 200,
                 filterable: { cell: { operator: 'contains', showOperators: false } }
             },
              { title: '', filterable: false, sortable: false },

        ]
    };
    $scope.settingReport_GridChoose_Change = function ($event, grid, hasChoose) {
        $scope.SettingHasChoose = hasChoose;
    }
    $scope.ORDOrderTypeOfDocumen_AddNew_Click = function ($event, win, vform) {
        $event.preventDefault();
        $scope.LoadItem(win, 0, vform);
    }
    $scope.LoadItem = function (win, id, vform) {
        Common.Services.Call($http, {
            url: Common.Services.url.ORD,
            method: _ORDOrderTypeOfDocument.URL.Get,
            data: { 'id': id },
            success: function (res) {
                vform({ clear: true });
                $scope.Item = res;
                win.center();
                win.open();
            }
        });

    }
    $scope.ORDOrderTypeOfDocumentEdit_Click = function ($event, win, data, vform) {
        $event.preventDefault();
        $scope.LoadItem(win, data.ID, vform);
    }


   
    $scope.SettingReport_SaveClick = function ($event, win, vform) {
        $event.preventDefault();
        if (vform()) {
            if (Common.HasValue($scope.Item.TypeOfWAInspectionStatusID) && $scope.Item.TypeOfWAInspectionStatusID > 0) {
                Common.Services.Call($http, {
                    url: Common.Services.url.ORD,
                    method: _ORDOrderTypeOfDocument.URL.Save,
                    data: { item: $scope.Item },
                    success: function (res) {
                        Common.Services.Error(res, function (res) {
                            $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                            win.close();
                            $scope.ORDOrderTypeOfDocumen_gridOptions.dataSource.read();
                        })
                    }
                });
            }
        }
        else {
            $rootScope.Message({ Msg: "Thiếu dữ liệu, dữ liệu chưa nhập!" });
        }

    }
    $scope.ORDOrder_cboTypeOfDocumentID_Options = {
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
        url: Common.ALL.URL.SYSVarTypeOfWAInspectionStatus,
        success: function (data) {
            $scope.ORDOrder_cboTypeOfDocumentID_Options.dataSource.data(data);
        }
    })

    $scope.RDOrderTypeOfDocumen_GridDestroy_Click = function ($event, grid) {
        $event.preventDefault();
        debugger
        var data = grid.dataSource.data();
        var datasend = [];
        Common.Data.Each(data, function (o) {
            if (o.IsChoose) datasend.push(o.ID);
        })
        if (datasend.length > 0) {
            $scope.SettingDelete(null, datasend);
        }
    }
    $scope.SettingDelete = function (win, datasend) {
       
        $rootScope.Message({
            Type: Common.Message.Type.Confirm,
            NotifyType: Common.Message.NotifyType.SUCCESS,
            Title: 'Thông báo',
            Msg: 'Bạn muốn xóa dữ liệu đã chọn?',
            Close: null,
            Ok: function () {
                $rootScope.IsLoading = true;
                Common.Services.Call($http, {
                    url: Common.Services.url.ORD,
                    method: _ORDOrderTypeOfDocument.URL.Destroy_List,
                    data: { lstID: datasend },
                    success: function (res) {
                        $rootScope.IsLoading = false;
                        $rootScope.Message({
                            Type: Common.Message.Type.Notify,
                            NotifyType: Common.Message.NotifyType.SUCCESS,
                            Title: 'Thông báo',
                            Msg: 'Thành công',
                            Ok: null,
                            close: null,
                        })
                        $scope.ORDOrderTypeOfDocumen_gridOptions.dataSource.read();
                    }
                });
               
            }
        })
    }

    $scope.ShowSetting = function ($event, grid) {
        $event.preventDefault();

        $rootScope.ShowSetting({
            ListView: views.ORDOrder_Inspection,
            event: $event,
            grid: grid,
            current: $state.current
        });
    };

    $scope.HideSetting = function ($event) {
        $event.preventDefault();

        $rootScope.HideSetting();
    }
    $scope.window_Close_Click = function ($event, win) {
        $event.preventDefault();

        win.close();
    };
}]);