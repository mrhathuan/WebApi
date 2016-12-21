/// <reference path="~/Scripts/jquery-1.9.1.intellisense.js" />
/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />

var _UserProfile = {
    URL: {
        Get: 'UserProfile_GetUser',
        Save: 'UserProfile_Save',
    }
}

myapp.controller('profileController', function ($rootScope, $scope, $http, $location, $state, $timeout) {
    Common.Log('profileController');
    $rootScope.IsLoading = false;
    $scope.Save_Click = function ($event) {
        $event.preventDefault();
        $rootScope.IsLoading = true;
        Common.Services.Call($http, {
            url: Common.Services.url.SYS,
            method: _UserProfile.URL.Save,
            data: { item: $scope.Item },
            success: function (res) {
                $rootScope.IsLoading = false;
                $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
            },
            error: function (res) {
                $rootScope.IsLoading = false;
            }
        });
    }
    $scope.Return_Data = function($event)
    {
        $event.preventDefault();
        $scope.LoadData();
    }
    $scope.Close_Click = function ($event) {
        $event.preventDefault();
        $state.go("main")
    }

    $scope.LoadData = function()
    {
        Common.Services.Call($http, {
            url: Common.Services.url.SYS,
            method: _UserProfile.URL.Get,
            data: {},
            success: function (res) {
                $scope.Item = res;
            }
        });
    }
    $scope.LoadData();
    $scope.Room_CbbOptions = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: "contains",
        suggest: true, dataTextField: 'DepartmentName', dataValueField: 'ID',
        dataSource: Common.DataSource.Local([], {
            id: 'ID',
            fields: {
                DepartmentName: { type: 'string' },
                ID: { type: 'number' },
            }
        })
    }
    $scope.Change_Passwword = function($event)
    {
        $state.go("changepassword");
    }
    $scope.Image_Click = function ($event) {
        $event.preventDefault();
        $rootScope.UploadFile({
            IsImage: true,
            ID: $scope.Item.ID,
            Type: Common.CATTypeOfFileCode.USER,
            Complete: function (image) {
                debugger
                $scope.Item.Image = image.FilePath;
            }
        });
    };

});