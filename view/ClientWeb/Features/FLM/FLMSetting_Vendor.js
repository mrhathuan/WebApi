/// <reference path="~/Scripts/jquery-1.9.1.intellisense.js" />
/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />

var _FLMSettingVendor = {
    URL: {
        CompanyUrlRead: "FLMSetting_Vendor_List",
        CompanyUrlDelete: "FLMSetting_Vendor_DeleteList",
        CompanyNotInUrlSave: "FLMSetting_Vendor_SaveList",
        CompanyNotInUrlRead: 'FLMSetting_VendorNotIn_List',
    }
}

angular.module('myapp').controller('FLMSetting_VendorCtrl', ['$rootScope', '$scope', '$http', '$location', '$state', '$timeout', '$stateParams', function ($rootScope, $scope, $http, $location, $state, $timeout, $stateParams) {
    Common.Log('FLMSetting_VendorCtrl');
    $rootScope.IsLoading = false;


    $scope.Company_GridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.FLM,
            method: _FLMSettingVendor.URL.CompanyUrlRead,
            readparam: function () { },
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number', nullable: true }
                }
            }
        }),
        height: '100%', pageable: true, sortable: false, columnMenu: false, filterable: { mode: 'row' }, resizable: true,
        columns: [
            {
                title: ' ', width: '45px',
                template: '<a href="/" ng-click="Company_Delete($event, dataItem)" class="k-button" ><i class="fa fa-trash"></i></a>',
                filterable: false, sortable: false
            },
            {
                field: 'CustomerRelateCode', title: 'Mã đối tác', width: '150px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'CustomerRelateName', title: 'Tên đối tác', width: '300px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
             { title: '', filterable: false, sortable: false },
        ]
    }

    $scope.CompanyNotin_GridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.FLM,
            method: _FLMSettingVendor.URL.CompanyNotInUrlRead,
            readparam: function () { },
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number', nullable: true },
                    IsChoose: { type: 'boolean' },
                }
            }
        }),
        height: '99%', pageable: true, sortable: false, columnMenu: false, filterable: { mode: 'row' }, resizable: true,
        columns: [
            {
                title: ' ', width: '40px',
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,CompanyNotin_Grid,CompanyNotin_GridChange)" />',
                headerAttributes: { style: 'text-align: center;' },
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,CompanyNotin_Grid,CompanyNotin_GridChange)" />',
                templateAttributes: { style: 'text-align: center;' },
                filterable: false, sortable: false
            },
            { field: 'CustomerRelateCode', title: 'Mã đối tác', width: '150px', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'CustomerRelateName', title: 'Tên đối tác', width: '150px', filterable: { cell: { operator: 'contains', showOperators: false } } },
        ]
    }

    $scope.Company_Delete = function ($event, data) {
        $event.preventDefault();
        $rootScope.Message({
            Type: Common.Message.Type.Confirm,
            Msg: 'Bạn muốn xóa các dữ liệu đã chọn ?',
            pars: { item: data },
            Ok: function (pars) {
                $rootScope.IsLoading = true;
                Common.Services.Call($http, {
                    url: Common.Services.url.FLM,
                    method: _FLMSettingVendor.URL.CompanyUrlDelete,
                    data: { item: data },
                    success: function (res) {
                        $scope.Company_Grid.dataSource.read();
                        $rootScope.IsLoading = false;
                        $rootScope.Message({
                            Msg: 'Đã cập nhật.',
                            NotifyType: Common.Message.NotifyType.SUCCESS
                        });
                    }
                })
            }
        });
    }

    $scope.Company_Add = function ($event) {
        $event.preventDefault();
        $scope.Company_Win.center().open();
        $timeout(function () {
            $scope.CompanyNotin_Grid.resize();
        }, 10)
    }

    $scope.Company_SaveList = function ($event) {
        $event.preventDefault();

        var grid = $scope.CompanyNotin_Grid;
        var data = $.grep(grid.dataSource.data(), function (item) {
            return (item.IsChoose == true);
        })

        if (data.length > 0) {
            Common.Services.Call($http, {
                url: Common.Services.url.FLM,
                method: _FLMSettingVendor.URL.CompanyNotInUrlSave,
                data: { lst: data },
                success: function (res) {
                    $scope.Company_Grid.dataSource.read();
                    $scope.CompanyNotin_Grid.dataSource.read();
                    $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                }
            })
        }
    }

    $scope.cboCompanyOptions = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true,
        dataTextField: 'CustomerRelateName', dataValueField: 'ID', minLength: 3,
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    CustomerRelateName: { type: 'string' },
                }
            }
        })
    }

    $scope.Window_Close_Click = function ($event, win) {
        $event.preventDefault();
        win.close();
    }

    //#region Action
    $scope.ShowSetting = function ($event, grid) {
        $event.preventDefault();

        $rootScope.ShowSetting({
            ListView: views.FLMSetting,
            event: $event,
            grid: grid,
            current: $state.current
        });
    };
    //#endregion
}]);