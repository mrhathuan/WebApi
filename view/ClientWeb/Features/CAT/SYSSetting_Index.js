/// <reference path="~/Scripts/jquery-1.9.1.intellisense.js" />
/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />

var _SYSSetting_Index = {
    URL: {
        Get: 'SYSSetting_Get',
        Save: 'SYSSetting_Save',
        Location_Read: 'CATLocation_Read',
        CheckApply: 'SYSSetting_CheckApplySeaportCarrier'
    }
}

angular.module('myapp').controller('SYSSetting_IndexCtrl', ['$rootScope', '$scope', '$http', '$location', '$state', '$timeout', function ($rootScope, $scope, $http, $location, $state, $timeout) {
    if ($rootScope.IsPageComplete != true) return;
    Common.Log('SYSSetting_IndexCtrl');
    $rootScope.IsLoading = false;

    $scope.Auth = $rootScope.GetAuth();
    $scope.Item = null;
    $scope.LocationType = 1;

    Common.Services.Call($http, {
        url: Common.Services.url.CAT,
        method: _SYSSetting_Index.URL.Get,
        data: { syscusID: null },
        success: function (res) {
            $scope.Item = res;
            if (res.LocationFromAddress == "") $scope.Item.LocationFromAddress = "Chọn địa điểm bắt đầu";
            if (res.LocationToAddress == "") $scope.Item.LocationToAddress = "Chọn địa điểm kết thúc";
        }
    })

    $scope.Save_Click = function ($event) {
        $event.preventDefault();
        if (Common.HasValue($scope.Item.TimeFromCalculate) && Common.HasValue($scope.Item.TimeToCalculate))
        {

        }
        Common.Services.Call($http, {
            url: Common.Services.url.CAT,
            method: _SYSSetting_Index.URL.Save,
            data: { item: $scope.Item },
            success: function (res) {
                $rootScope.Message({
                    Msg: 'Thành công!'
                });
            }
        })
    }

    $scope.Location_win_GridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.CAT,
            method: _SYSSetting_Index.URL.Location_Read,
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number', editable: false, nullable: true },
                    Lat: { type: 'number' },
                    Lng: { type: 'number' },
                }
            }
        }),
        height: '100%', pageable: true, sortable: true, columnMenu: false, filterable: { mode: 'row' }, resizable: true, editable: false,selectable:'row',
        columns: [
            {
                field: 'Code', title: '{{RS.CATLocation.Code}}', width: 100,
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'Location', title: '{{RS.CATLocation.Location}}', width: 200,
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'Address', title: '{{RS.CATLocation.Address}}', width: 250,
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'EconomicZone', title: '{{RS.CATLocation.EconomicZone}}', width: 150,
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'DistrictName', title: '{{RS.CATDistrict.DistrictName}}', width: 150,
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'ProvinceName', title: '{{RS.CATProvince.ProvinceName}}', width: 150,
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'Note', title: '{{RS.CATLocation.Note}}', width: 150,
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'Note1', title: '{{RS.CATLocation.Note1}}', width: 150,
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'Lat', title: '{{RS.CATLocation.Lat}}', width: 150, template: '#=Common.Number.ToNumber5(Lat)#',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'Lng', title: '{{RS.CATLocation.Lng}}', width: 150,template:'#=Common.Number.ToNumber5(Lng)#',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
        ]
    }

    $scope.Change_Location_Click = function ($event, win, type) {
        $event.preventDefault()
        $scope.Location_win_GridOptions.dataSource.read();
        $scope.LocationType = type;
        win.center().open();
    }

    $scope.Location_win_CloseClick = function ($event,win) {
        $event.preventDefault();
        win.close();
    }

    $scope.numTimeDelayWarning_Options = { format: 'n2', spinners: false, culture: 'en-US', min: 0, step: 1, decimals: 2 }

    $scope.cboCollectDataKM_Options = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: "contains", suggest: true,
        dataTextField: 'Text', dataValueField: 'ID',
        dataSource: Common.DataSource.Local([], {
            id: 'ID',
            fields: {
                Text: { type: 'string' },
                ID: { type: 'number' },
            }
        })
    }

    $scope.cboCollectDataKM_Options.dataSource.data([
    { 'Text': 'Nhập tay', ID: 1 },
    { 'Text': 'Dữ liệu GPS', ID: 3 },
    { 'Text': 'Dữ liệu vận chuyển', ID: 2 },
    ])

    $scope.Location_win_SaveClick = function ($event,win,grid) {
        $event.preventDefault();
        var data = grid.dataItem(grid.select());
        if (Common.HasValue(data)) {
            switch($scope.LocationType) {
                case 1:
                    $scope.Item.LocationFromAddress = data.Address;
                    $scope.Item.LocationFromID = data.ID;
                case 2:
                    $scope.Item.LocationToAddress = data.Address;
                    $scope.Item.LocationToID = data.ID;
                case 3:
                    $scope.Item.LocationRomoocReturnAddress = data.Address;
                    $scope.Item.LocationRomoocReturnID = data.ID;
                default:
                    break;
            }
            win.close();
        }

        else $rootScope.Message({ Msg: 'Chưa chọn địa điểm', NotifyType: Common.Message.NotifyType.ERROR });
    }

    $scope.CheckApplySeaportCarrier_click = function ($event) {
        $event.preventDefault();
        $rootScope.Message({
            Type: Common.Message.Type.Confirm,
            NotifyType: Common.Message.NotifyType.SUCCESS,
            Title: 'Thông báo',
            Msg: 'Bạn muốn thay đổi dữ liệu hãng tàu, cảng biển?',
            Close: null,
            Ok: function () {
                $rootScope.IsLoading = true;
                Common.Services.Call($http, {
                    url: Common.Services.url.CAT,
                    method: _SYSSetting_Index.URL.CheckApply,
                    data: { },
                    success: function (res) {
                        $rootScope.IsLoading = false;
                        $rootScope.Message({ Msg: 'Đã cập nhật', Type: Common.Message.Type.Notify, NotifyType: Common.Message.NotifyType.SUCCESS });
                    },
                    error: function (res) {
                        $rootScope.IsLoading = false;
                    }
                });
            }
        })
    };


    $scope.Change_BarCode_Click = function ($event, win) {
        $event.preventDefault();
        win.center().open();
    }
    $scope.BarCode_win_CloseClick = function ($event, win) {
        $event.preventDefault();
        win.close();
    }
    $scope.BarCode_win_SaveClick = function ($event, win) {
        $event.preventDefault();
        if (!Common.HasValue($scope.Item.PODBarcode.DNLength))
            $scope.Item.PODBarcode.DNLength = 0;
        if (!Common.HasValue($scope.Item.PODBarcode.DNIndex))
            $scope.Item.PODBarcode.DNIndex = 0;
        if (!Common.HasValue($scope.Item.PODBarcode.SOIndex))
            $scope.Item.PODBarcode.SOIndex = 0;
        if (!Common.HasValue($scope.Item.PODBarcode.SOLength))
            $scope.Item.PODBarcode.SOLength = 0;

        Common.Services.Call($http, {
            url: Common.Services.url.CAT,
            method: _SYSSetting_Index.URL.Save,
            data: { item: $scope.Item },
            success: function (res) {
                $rootScope.Message({

                    Msg: 'Thành công!'
                });
            }
        })

        win.close();

    }
}]);