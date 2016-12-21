
/// <reference path="~/Scripts/jquery-1.9.1.intellisense.js" />
/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />

var _REPOwnerDriverRole_Detail = {
    URL: {
        DriverRole: 'REPOwner_DriverRole',
    },
}

angular.module('myapp').controller('REPOwnerDriverRole_DetailCtrl', ['$rootScope', '$scope', '$http', '$location', '$state', '$timeout', '$stateParams', function ($rootScope, $scope, $http, $location, $state, $timeout, $stateParams) {
    if ($rootScope.IsPageComplete != true) return;
    Common.Log('REPOwnerDriverRole_DetailCtrl');
    $rootScope.IsLoading = false;

    $scope.Auth = $rootScope.GetAuth();
    $scope.REPOwnerItem = { DateFrom: new Date().addDays(-6), DateTo: new Date() };

    $scope.REPOwnerDriverRole_gridOptions = {
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number', editable: false },
                    IsMain: { type: 'boolean' },
                    IsReject: { type: 'boolean' },
                }
            },
            pageSize: 20
        }),
        height: '100%', pageable: true, sortable: true, columnMenu: false, filterable: { mode: 'row' }, resizable: true, editable: false, selectable: "row",
        columns: [
            {
                field: 'MasterCode', title: 'Mã chuyến', width: '100px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'DriverCode', title: 'Mã tài xế', width: '150px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'DriverName', title: 'Tên tài xế', width: '150px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'IsMain', title: 'Tài xế chính', width: '100px',attributes: { style: "text-align: center; " }, templateAttributes: { style: 'text-align: center;' }, template: '<input type="checkbox" #= IsMain ? "checked=checked" : "" # disabled />',
                filterable: {
                    cell: {
                        template: function (container) {
                            container.element.kendoComboBox({
                                dataSource: [{ Text: 'Tài xế phụ', Value: false }, { Text: 'Tài xế chính', Value: true }, { Text: 'Tất cả', Value: '' }],
                                dataTextField: "Text", dataValueField: "Value",
                            });
                        },
                        showOperators: false
                    }
                }
            },
            {
                field: 'IsReject', title: 'Từ chối', width: '100px', attributes: { style: "text-align: center; " }, templateAttributes: { style: 'text-align: center;' }, template: '<input type="checkbox" #= IsReject ? "checked=checked" : "" # disabled/>',
                filterable: {
                    cell: {
                        template: function (container) {
                            container.element.kendoComboBox({
                                dataSource: [{ Text: 'Không từ chối', Value: false }, { Text: 'Từ chối', Value: true }, { Text: 'Tất cả', Value: '' }],
                                dataTextField: "Text", dataValueField: "Value",
                            });
                        },
                        showOperators: false
                    }
                }
            },
            {
                field: 'ReasonName', title: 'Lý do', width: '100px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'Note', title: 'Ghi chú', width: '150px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'StatusOfAssetTimeSheetName', title: 'Loại hình vận chuyển', width: '150px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'LocationToCode', title: 'Mã địa chỉ', width: '100px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'LocationToName', title: 'Tên địa chỉ', width: '130px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'LocationToAddress', title: 'Vị trí', width: '150px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            { title: '', filterable: false, sortable: false }
        ]
    }

    $scope.Search_Click = function ($event) {
        $event.preventDefault();
        $scope.LoadData();
    }
    $scope.LoadData = function () {
        $rootScope.IsLoading = true;
        Common.Services.Call($http, {
            url: Common.Services.url.REP,
            method: _REPOwnerDriverRole_Detail.URL.DriverRole,
            data: { dtfrom: $scope.REPOwnerItem.DateFrom, dtto: $scope.REPOwnerItem.DateTo },
            success: function (res) {
                $scope.REPOwnerDriverRole_gridOptions.dataSource.data(res);
                $rootScope.IsLoading = false;
            }
        });
    }
    $scope.LoadData();

    $scope.ShowSetting = function ($event, grid) {
        $event.preventDefault();

        $rootScope.ShowSetting({
            ListView: views.REPOwnerDriverRole,
            event: $event,
            grid:grid,
            current: $state.current
        });
    };
}]);