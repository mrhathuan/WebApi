
/// <reference path="~/Scripts/jquery-1.9.1.intellisense.js" />
/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />

var _FLMAsset_Warning = {
    URL: {
        TypeWarning: "Get_TypeWarning",
        FLMAsset_Warning_List: "FLMAsset_Warning_List",
        FLMAsset_Warning_NoInList: "FLMAsset_Warning_NoInList",
        FLMAsset_Warning_SaveNoInList: "FLMAsset_Warning_SaveNoInList",
        FLMAsset_Warning_SaveList: "FLMAsset_Warning_SaveList",
        FLMAsset_Warning_Delete: "FLMAsset_Warning_Delete",

        ExcelInit: 'FLMAssetWarning_ExcelInit',
        ExcelChange: 'FLMAssetWarning_ExcelChange',
        ExcelImport: 'FLMAssetWarning_ExcelImport',
        ExcelApprove: 'FLMAssetWarning_ExcelApprove',
    },
}

angular.module('myapp').controller('FLMAssetWarning_IndexCtrl', ['$rootScope', '$scope', '$http', '$location', '$state', '$timeout', function ($rootScope, $scope, $http, $location, $state, $timeout) {
    Common.Log('FLMAssetWarning_IndexCtrl');
    if ($rootScope.IsPageComplete != true) return;
    Common.Log('ORDOrder_IndexCtrl');
    $rootScope.IsLoading = false;
    $scope.TypeWarningID = 0;
    $scope.Auth = $rootScope.GetAuth();
    $scope.HasChoose = false;

    $scope.cboTypeWarning_Options = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true, dataTextField: 'WarningName', dataValueField: 'ID',
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
    };

    Common.Services.Call($http, {
        url: Common.Services.url.FLM,
        method: _FLMAsset_Warning.URL.TypeWarning,
        success: function (res) {
            $scope.TypeWarningID = res[0].ID;
            $scope.cboTypeWarning_Options.dataSource.data(res);
        }
    });

    $scope.Search_Click = function ($event) {
        $event.preventDefault();
        $scope.FLMAssetWarning_Grid.dataSource.read();
    }

    $scope.FLMAssetWarning_GridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.FLM,
            method: _FLMAsset_Warning.URL.FLMAsset_Warning_List,
            model: {
                ID: { type: 'number' },
                DateData:{type:'date'},
                DateCompare: { type: 'date' },
                NumberData: { type: 'number' },
                NumberCompare: { type: 'number' },
            },
            readparam: function () { //input truyen vao
                return {
                    TypeWarningID: $scope.TypeWarningID
                }
            },
        }),
        height: '100%', pageable: false, sortable: true, columnMenu: false, filterable: { mode: 'row' }, resizable: true,
        columns: [
            {
                title: ' ', width: 45, template: '<a href="/" ng-click="FLMAssetWarning_DeleteClick($event,dataItem)" class="k-button" ><i class="fa fa-trash"></i></a>',
                filterable: false, sorable: false        
            },
            {
                field: 'Code', title: 'Mã', template: "#=Code#", width: 150,
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'Regno', title: 'Số xe', template: "#=Regno#", width: 150,
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'TypeOfAssetName', title: 'Loại', template: "#=TypeOfAssetName#", width: 150,
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'DateData', title: 'Ngày', template: '<input class="cus-datepicker" kendo-date-picker focus-k-datepicker k-options="DateDMY" k-ng-model="dataItem.DateData"/>', width: 150,
                filterable: {
                    cell: {
                        template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); }, operator: 'equal', showOperators: false
                    }
                },
            },
            {
                field: 'DateCompare', title: 'Ngày so sánh', template: '<input class="cus-datepicker" kendo-date-picker focus-k-datepicker k-options="DateDMY" k-ng-model="dataItem.DateCompare"/>', width: 150,
                filterable: {
                    cell: {
                        template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); }, operator: 'equal', showOperators: false
                    }
                },
            },
            {
                field: 'NumberData', title: 'Giá trị', template: '<input ng-model="dataItem.NumberData" kendo-numeric-text-box k-options="money_options" k-min="0" k-spinners="false" style="width:100%" />', width: 150,
                filterable: { cell: { operator: 'eq', showOperators: false } }
            },
            {
                field: 'NumberCompare', title: 'Giá trị so sánh', template: '<input ng-model="dataItem.NumberCompare" kendo-numeric-text-box k-options="money_options" k-min="0" k-spinners="false" style="width:100%" />', width: 150,
                filterable: { cell: { operator: 'eq', showOperators: false } }
            },
            { title: ' ', filterable: false, sortable: false }
        ]
    }
    $scope.money_options = { format: 'n3', spinners: false, culture: 'en-US', min: 0, step: 1, decimals: 3, };

    $scope.FLMAssetWarning_NotIn_GridOPtions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.FLM,
            method: _FLMAsset_Warning.URL.FLMAsset_Warning_NoInList,
            model: {
                ID: { type: 'number' },
                IsChoose: { type: 'bool', defaultValue: false },
            },
            readparam: function () { //input truyen vao
                return {
                    TypeWarningID: $scope.TypeWarningID
                }
            },
        }),
        height: '100%', pageable: false, sortable: true, columnMenu: false, filterable: { mode: 'row' }, resizable: true,
        columns: [
            {
                title: ' ', width: '40px',
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,FLMAssetWarning_NotIn_Grid,FLMAssetWarning_NotIn_GridChooseChange)" />',
                headerAttributes: { style: 'text-align: center;' },
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,FLMAssetWarning_NotIn_Grid,FLMAssetWarning_NotIn_GridChooseChange)" />',
                templateAttributes: { style: 'text-align: center;' },
                filterable: false, sortable: false
            },
            {
                field: 'Code', title: 'Mã', template: "#=Code#", width: 150,
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'RegNo', title: 'Số xe', template: "#=RegNo#", width: 150,
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'TypeOfAssetName', title: 'Loại', template: "#=TypeOfAssetName#", width: 150,
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            { title: ' ', filterable: false, sortable: false }
        ]
    }

    $scope.FLMAssetWarning_NotIn_GridChooseChange = function ($event, grid, haschoose) {
        $scope.HasChoose = haschoose;
    };

    $scope.Add_Click = function ($event, win) {
        $event.preventDefault();
        $scope.FLMAssetWarning_NotIn_Grid.dataSource.read();
        win.center().open();
        $timeout(function () {
            $scope.FLMAssetWarning_NotIn_Grid.resize();
        }, 100);
    }

    $scope.Save_Click = function ($event, grid) {
        $event.preventDefault();
        $rootScope.IsLoading = true;
        Common.Services.Call($http, {
            url: Common.Services.url.FLM,
            method: _FLMAsset_Warning.URL.FLMAsset_Warning_SaveList,
            data: { lst: grid.dataSource.data()},
            success: function (res) {
                $rootScope.IsLoading = false;
                $scope.FLMAssetWarning_Grid.dataSource.read();
                $rootScope.Message({
                    Msg: 'Đã cập nhật.',
                    NotifyType: Common.Message.NotifyType.SUCCESS
                });
            }
        })
    }

    $scope.TypeWarning_AddNotInClick = function ($event, grid, win) {
        $event.preventDefault();
        var data = [];
        $.each(grid.dataSource.data(), function (i, v) {
            if (v.IsChoose == true)
                data.push(v);
        });

        if (Common.HasValue(data))
        {
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.FLM,
                method: _FLMAsset_Warning.URL.FLMAsset_Warning_SaveNoInList,
                data: { lst: data, TypeWarningID: $scope.TypeWarningID},
                success: function (res) {
                    $rootScope.IsLoading = false;
                    win.close();
                    $scope.FLMAssetWarning_Grid.dataSource.read();
                    $rootScope.Message({
                        Msg: 'Đã cập nhật.',
                        NotifyType: Common.Message.NotifyType.SUCCESS
                    });
                }
            })
        }
    }

    $scope.FLMAssetWarning_DeleteClick = function ($event, item) {
        $event.preventDefault();
        $rootScope.IsLoading = true;
        Common.Services.Call($http, {
            url: Common.Services.url.FLM,
            method: _FLMAsset_Warning.URL.FLMAsset_Warning_Delete,
            data: { item: item },
            success: function (res) {
                $rootScope.IsLoading = false;
                $scope.FLMAssetWarning_Grid.dataSource.read();
                $rootScope.Message({
                    Msg: 'Đã cập nhật.',
                    NotifyType: Common.Message.NotifyType.SUCCESS
                });
            }
        })
    }

    $scope.ExcelOn_Click = function ($event) {
        $event.preventDefault();

        var lstMessageError = [];
        for (var i = 0; i < 7; i++) {
            var resource = $rootScope.RS[_FLMDriver.ExcelKey.Resource + '_' + i];
            if (Common.HasValue(resource))
                lstMessageError.push(resource);
        }
        if (lstMessageError.length == 0) {
            lstMessageError = [
                '[Mã] không được trống',
                '[Mã] đã bị trùng',
                '[Ngày] không đúng định dạng (ngày/tháng/năm)',
                '[Ngày so sánh] không đúng định dạng (ngày/tháng/năm)',
                '[Tiền] là số',
                '[Tiền so sánh] là số',
                '[Mã] không tồn tại hoặc đã được sử dụng',
            ];
        }

        $rootScope.excelShare.Init({
            functionkey: 'FLMAssetWarning_Index',
            params: {
                TypeWarningID: $scope.TypeWarningID,
                isreload: 'true',
            },
            rowStart: 1,
            colCheckChange:8,
            url: Common.Services.url.FLM,
            methodInit: _FLMAsset_Warning.URL.ExcelInit,
            methodChange: _FLMAsset_Warning.URL.ExcelChange,
            methodImport: _FLMAsset_Warning.URL.ExcelImport,
            methodApprove: _FLMAsset_Warning.URL.ExcelApprove,
            lstMessageError: lstMessageError,

            Changed: function () {

            },
            Approved: function () {
                $scope.FLMAssetWarning_Grid.dataSource.read();
            }
        });
    };

    $scope.Close_Click = function ($event, win) {
        $event.preventDefault();
        win.close();
    }
}])