//ban moi

/// <reference path="~/Scripts/jquery-1.9.1.intellisense.js" />
/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />

//#region Data
var _WFLSetting_Template = {
    URL: {
        Read: 'WFLSetting_TemplateRead',
        Get: 'WFLSetting_TemplateGet',
        Save: 'WFLSetting_TemplateSave',
        Delete: 'WFLSetting_TemplateDelete',
    },
    Data: {
        _gridModel: {
            id: 'ID',
            fields: {
                ID: { type: 'number' },
                Name: { type: 'string' },
                Code: { type: 'string' },
            }
        },
    }
};
//#endregion

angular.module('myapp').controller('WFLSetting_TemplateCtrl', ['$rootScope', '$scope', '$http', '$location', '$state', '$timeout', '$stateParams', function ($rootScope, $scope, $http, $location, $state, $timeout, $stateParams) {
    if ($rootScope.IsPageComplete != true) return;
    Common.Log('WFLSetting_TemplateCtrl');
    $rootScope.IsLoading = false;
    $scope.Auth = $rootScope.GetAuth();

    $scope.HasChoose = false;
    $scope.Item = null;

    $scope.WFLSetting_Template_gridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.WFL,
            method: _WFLSetting_Template.URL.Read,
            model: _WFLSetting_Template.Data._gridModel,
        }),
        height: '100%', pageable: true, sortable: true, columnMenu: false, resizable: true, reorderable: true, filterable: { mode: 'row' },
        toolbar: kendo.template($('#WFLSetting_Template_gridToolbar').html()),
        columns: [
            {
                title: ' ', width: '40px',
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,WFLSetting_Template_grid,gridChooseChange)" />',
                headerAttributes: { style: 'text-align: center;' },
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,WFLSetting_Template_grid,gridChooseChange)" />',
                templateAttributes: { style: 'text-align: center;' },
                filterable: false, sortable: false
            },
            {
                title: ' ', width: '50px',
                template: '<a href="/" ng-click="Edit_Click($event,WFLSetting_Template_win,WFLSetting_Template_grid,WFLSetting_Template_win_vform)" class="k-button"><i class="fa fa-pencil"></i></a>',
                filterable: false, sortable: false
            },
            {
                field: 'Code', title: 'Mã',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'Name', title: 'Tên',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            { title: ' ', sortable: false, filterable: false, menu: false }
        ],
    };

    $scope.WFLSetting_Template_win_tabOptions = {
        animation: {
            open: {
                effects: "fadeIn"
            }
        }
    };

    $scope.gridChooseChange = function ($event, grid, haschoose) {
        $scope.HasChoose = haschoose;
    };

    $scope.Add_Click = function ($event, win, vform) {
        $event.preventDefault();

        $scope.LoadItem(win, -1, vform);
    };

    $scope.Edit_Click = function ($event, win, grid, vform) {
        $event.preventDefault();

        var tr = $($event.target).closest('tr');
        var item = grid.dataItem(tr);

        $scope.LoadItem(win, item.ID, vform);
    };

    $scope.LoadItem = function (win, id, vform) {
        vform({ clear: true });

        Common.Services.Call($http, {
            url: Common.Services.url.WFL,
            method: _WFLSetting_Template.URL.Get,
            data: { id: id },
            success: function (res) {
                if (Common.HasValue(res)) {
                    $timeout(function () {
                        $scope.Item = res;
                    }, 1);
                }
            }
        });

        win.center();
        win.open();
    }

    $scope.Del_Click = function ($event, grid) {
        $event.preventDefault();

        var lstid = [];
        var data = grid.dataSource.data();
        $.each(data, function (i, v) {
            if (v.IsChoose == true)
                lstid.push(v.ID);
        });
        if (lstid.length > 0) {
            $rootScope.Message({
                Type: Common.Message.Type.Confirm,
                Msg: 'Bạn muốn xóa các dữ liệu đã chọn ?',
                pars: { lstid: lstid },
                Ok: function (pars) {
                    Common.Services.Call($http, {
                        url: Common.Services.url.WFL,
                        method: _WFLSetting_Template.URL.Delete,
                        data: pars,
                        success: function (res) {
                            $scope.WFLSetting_Template_gridOptions.dataSource.read();

                            $rootScope.Message({ Msg: 'Đã xóa', NotifyType: Common.Message.NotifyType.ERROR });
                        }
                    });
                }
            });
        }
    };

    $scope.Save_Click = function ($event, win, grid, vform) {
        $event.preventDefault();

        if (vform()) {
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.WFL,
                method: _WFLSetting_Template.URL.Save,
                data: { item: $scope.Item },
                success: function (res) {
                    $rootScope.IsLoading = false;
                    win.close();
                    $scope.WFLSetting_Template_gridOptions.dataSource.read();
                    $rootScope.Message({ Msg: 'Đã cập nhật' });
                }
            });
        }

    };

    $scope.Setting_AddFileClick = function ($event) {
        $event.preventDefault();
        var functionID = $rootScope.FunctionItem.ID;
        $rootScope.UploadFile({
            IsImage: false, ID: functionID, AllowChange: true, ShowChoose: true,
            Type: Common.CATTypeOfFileCode.TEMPLATEREPORT,
            Complete: function (file) {
                if (file != null) {
                    $scope.Item.FileID = file.ID;
                    $scope.Item.FileName = file.FileName;
                }
            }
        });
    };

    $scope.Close_Click = function ($event, win) {
        $event.preventDefault();
        win.close();
    };

    $scope.Window_Close_Click = function ($event, win) {
        $event.preventDefault();
        win.close();
    }

    $scope.ShowSetting = function ($event, grid) {
        $event.preventDefault();

        $rootScope.ShowSetting({
            ListView: views.WFLSetting,
            event: $event,
            grid: grid,
            current: $state.current
        });
    };

    $scope.HideSetting = function ($event) {
        $event.preventDefault();

        $rootScope.HideSetting();
    }

    $scope.ViewHTML_Click = function ($event, win) {
        $event.preventDefault();
        $('#content_template').empty().append($scope.Item.Template);
        win.center();
        win.open();
    }

    $scope.ViewHTMLDetail_Click = function ($event, win) {
        $event.preventDefault();
        $('#content_templatedetail').empty().append($scope.Item.TemplateDetail);
        win.center();
        win.open();
    }
}]);
