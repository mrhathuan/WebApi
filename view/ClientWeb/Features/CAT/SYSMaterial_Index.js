/// <reference path="~/Scripts/jquery-1.9.1.intellisense.js" />
/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />

var _SYSMaterial_Index = {
    URL: {
        Get: 'SYSMaterial_Get',
        Save: 'SYSMaterial_Save'
    }
}

angular.module('myapp').controller('SYSMaterial_IndexCtrl', ['$rootScope', '$scope', '$http', '$location', '$state', '$timeout', function ($rootScope, $scope, $http, $location, $state, $timeout) {
    if ($rootScope.IsPageComplete != true) return;
    Common.Log('SYSMaterial_IndexCtrl');
    $rootScope.IsLoading = false;

    $scope.Auth = $rootScope.GetAuth();
    
    $scope.Item = null;

    Common.Services.Call($http, {
        url: Common.Services.url.CAT,
        method: _SYSMaterial_Index.URL.Get,
        data: { syscusID: null },
        success: function (res) {
            $scope.Item = res;            
        }
    })

    $scope.cboMaterialOptions = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true,
        dataTextField: 'MaterialName', dataValueField: 'ID', minLength: 3,
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    MaterialName: { type: 'string' }
                }
            }
        })
    }

    Common.ALL.Get($http, {
        url: Common.ALL.URL.Material,
        success: function (res) {
            $timeout(function () {
                $scope.cboMaterialOptions.dataSource.data(res);
            }, 1);
        }
    });

    $scope.Save_Click = function ($event) {
        $event.preventDefault();
        $rootScope.IsLoading = true;
        Common.Services.Call($http, {
            url: Common.Services.url.CAT,
            method: _SYSMaterial_Index.URL.Save,
            data: { item: $scope.Item },
            success: function (res) {
                $rootScope.IsLoading = false;
                $rootScope.Message({
                    Msg: 'Thành công!'
                });
            }
        })
    }
    $scope.nummoney_options = { format: 'n0', spinners: false, culture: 'en-US', min: 0, step: 1, decimals: 0, }
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