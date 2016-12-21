/// <reference path="~/Scripts/jquery-1.9.1.intellisense.js" />
/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />

var _SYSSettingCode_Index = {
    URL: {
        Get: 'SYSSetting_Get',
        Save: 'SYSSetting_Save',
    }
}

angular.module('myapp').controller('SYSSettingCode_IndexCtrl', ['$rootScope', '$scope', '$http', '$location', '$state', '$timeout', function ($rootScope, $scope, $http, $location, $state, $timeout) {
    if ($rootScope.IsPageComplete != true) return;
    Common.Log('SYSSettingCode_IndexCtrl');
    $rootScope.IsLoading = false;

    $scope.Auth = $rootScope.GetAuth();

    $scope.Item = null;
    $scope.IsLocationFrom = false;

    Common.Services.Call($http, {
        url: Common.Services.url.CAT,
        method: _SYSSettingCode_Index.URL.Get,
        data: { syscusID: null },
        success: function (res) {
            $scope.Item = res;
        }
    })

    $scope.TruckCon_Click = function ($event) {
        
        if ($scope.Item.TypeMasterCode == 1) {
            $scope.Item.ExprMasterCodeDI = "";
            $scope.Item.ExprMasterCodeCO = "";
        } else if ($scope.Item.TypeMasterCode == 2) {
            $scope.Item.ExprMasterCodeAll = "";
        }
    }

    $scope.Save_Click = function ($event) {
        $event.preventDefault();
      
        var flag = false;
        if ($scope.checkData($scope.Item.ExprMasterCodeAll) && $scope.checkData($scope.Item.ExprMasterCodeDI) && $scope.checkData($scope.Item.ExprMasterCodeCO)
            && $scope.checkData($scope.Item.ExprFuelCode) && $scope.checkData($scope.Item.ExprRepairCode) && $scope.checkData($scope.Item.ExprDiposalCode)
            && $scope.checkData($scope.Item.ExprTransferCode)) {
                flag = true;
            }
        
        if (flag == true) {
            Common.Services.Call($http, {
                url: Common.Services.url.CAT,
                method: _SYSSettingCode_Index.URL.Save,
                data: { item: $scope.Item },
                success: function (res) {
                    $rootScope.Message({
                        Msg: 'Thành công!'
                    });
                }
            });
        } else {
            $rootScope.Message({
                Msg: 'Mã phiếu phải có một trong các công thức.',
                NotifyType: Common.Message.NotifyType.ERROR
            });
        }
    }

    $scope.checkData = function (item) {
        var check = ["[Sort-0000000]", "[Sort-00000]", "[Sort-000]"];
        if (Common.HasValue(item) && item != "") {
            for (var i = 0; i < check.length; i++)
            {
                var n =item.split(check[i]).length; 
                if ( n >= 2) {
                    return true;
                }
            }
            return false;
        }
        return true;
    }
}]);