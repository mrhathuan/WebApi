/// <reference path="~/Scripts/jquery-1.9.1.intellisense.js" />
/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />
var _VEN_VENIndex = {
    URL: {
        Read: 'Vendor_Read',
        Get: 'Vendor_Get',
        Save: 'Vendor_Update',

        ApproveList: 'VENVendor_ApprovedList',
        UnApproveList: 'VENVendor_UnApprovedList',
    },
    Data: {
        Country: [],
        Province: [],
        District: [],
        Tmp: {}
    },
    Params: {
        id: 0,
    }
}

angular.module('myapp').controller('VENVendor_IndexCtrl', ['$rootScope', '$scope', '$http', '$location', '$state', '$timeout', '$stateParams', function ($rootScope, $scope, $http, $location, $state, $timeout, $stateParams) {
    if ($rootScope.IsPageComplete != true) return;
    Common.Log('VENVendor_IndexCtrl');
    $rootScope.IsLoading = false;
    $scope.Item = null;
    //#region Auth
    $scope.Auth = $rootScope.GetAuth();
    //#endregion

    $scope.CusHasChoose = false;
    $scope.Ven_GridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.VEN,
            method: _VEN_VENIndex.URL.Read,
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number', editable: false, nullable: true },
                    IsChoose: { type: 'boolean' },
                    IsApproved: { type: 'string' },
                },
            }
        }),
        height: '100%', pageable: true, sortable: true, columnMenu: false, resizable: true, reorderable: true, filterable: { mode: 'row' },
        reorderable: true,
        filterable: { mode: 'row' },
        columns: [
            {
                title: ' ', width: '35px',
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,Ven_Grid,ven_GridChoose_Change)" />',
                headerAttributes: { style: 'text-align: center;' }, attributes: { style: 'text-align: center;' },
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,Ven_Grid,ven_GridChoose_Change)" />',
                filterable: false, sortable: false
            },
            {
                field: 'Code', width: "135px", title: 'Mã nhà xe', filterable: { cell: { operator: 'contains', showOperators: false } },
                template: "<a class='text' ui-sref='main.VENVendor.Detail({id:#=ID#})' style='text-decoration: underline;'>#=Code#</a>"
            },
            {
                field: 'VendorName', width: "200px", title: 'Nhà xe', filterable: { cell: { operator: 'contains', showOperators: false } },
                template: "<a class='text' ui-sref='main.VENVendor.Detail({id:#=ID#})'>#=VendorName#</a>"
            },
            {
                field: 'Address', width: "263px", title: 'Địa chỉ liên hệ', filterable: { cell: { operator: 'contains', showOperators: false } },
                template: "<a class='text' ui-sref='main.VENVendor.Detail({id:#=ID#})'>#=Address#</a>"
            },
            {
                field: 'DistrictName', width: "150px", title: 'Quận huyện', filterable: { cell: { operator: 'contains', showOperators: false } },
                template: "<a class='text' ui-sref='main.VENVendor.Detail({id:#=ID#})'>#=DistrictName#</a>"
            },
            {
                field: 'ProvinceName', width: "120px", title: 'Tỉnh thành', filterable: { cell: { operator: 'contains', showOperators: false } },
                template: "<a class='text' ui-sref='main.VENVendor.Detail({id:#=ID#})'>#=ProvinceName#</a>"
            },
            {
                field: 'TelNo', width: "190px", title: 'Số điện thoại', filterable: { cell: { operator: 'contains', showOperators: false } },
                template: "<a class='text' ui-sref='main.VENVendor.Detail({id:#=ID#})'>#=TelNo#</a>"
            },
            {
                field: 'IsApproved', title: 'Đã duyệt', width: 100,
                template: '<input type="checkbox" #= IsApproved=="true" ? "checked=checked" : "" # disabled="disabled" />',
                attributes: { style: "text-align: center; " },
                filterable: {
                    cell: {
                        template: function (container) {
                            container.element.kendoComboBox({
                                dataSource: [{ Text: 'Tất cả', Value: '' }, { Text: 'Đã duyệt', Value: true }, { Text: 'Chưa duyệt', Value: false }],
                                dataTextField: "Text", dataValueField: "Value",
                            });
                        }, showOperators: false
                    }
                }
            },
            { title: '', filterable: false, sortable: false }
        ]
    }

    $scope.cus_GridChoose_Change = function ($event, grid, hasChoose) {
        $scope.CusHasChoose = hasChoose;
    }

    $scope.Approve_Click = function ($event, grid) {
        $event.preventDefault();
        var data = grid.dataSource.data();
        var datasend = [];
        Common.Data.Each(data, function (o) {
            if (o.IsChoose && o.IsApproved == "false") datasend.push(o.ID);
        })
        if (datasend.length > 0) {
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.VEN,
                method: _VEN_VENIndex.URL.ApproveList,
                data: { lst: datasend },
                success: function (res) {
                    $rootScope.IsLoading = false;
                    $scope.Ven_GridOptions.dataSource.read();
                    $rootScope.Message({ Msg: 'Duyệt thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                },
                error: function (res) {
                    $rootScope.IsLoading = false;
                }
            });

        }
    }

    $scope.UnApprove_Click = function ($event, grid) {
        $event.preventDefault();
        var data = grid.dataSource.data();
        var datasend = [];
        Common.Data.Each(data, function (o) {
            if (o.IsChoose && o.IsApproved == "true") datasend.push(o.ID);
        })
        if (datasend.length > 0) {
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.VEN,
                method: _VEN_VENIndex.URL.UnApproveList,
                data: { lst: datasend },
                success: function (res) {
                    $rootScope.IsLoading = false;
                    $scope.Ven_GridOptions.dataSource.read();
                    $rootScope.Message({ Msg: 'Hủy duyệt thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                },
                error: function (res) {
                    $rootScope.IsLoading = false;
                }
            });

        }
    }

    $scope.VENVendor_win_cboCountryOptions = {
        autoBind: true,
        valuePrimitive: true,
        ignoreCase: true,
        filter: 'contains',
        suggest: true,
        dataTextField: 'CountryName',
        dataValueField: 'ID',
        minLength: 3,
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    CountryName: { type: 'string' },
                }
            }
        }),
        change: function (e) {
            var cbo = this;
            if (e.sender.selectedIndex >= 0) {
                $scope.Item.ProvinceID = -1;
                $scope.Item.DistrictID = -1;
                $scope.LoadRegionData($scope.Item);
            }
            else {
                $scope.Item.CountryID = "";
                $scope.Item.ProvinceID = "";
                $scope.Item.DistrictID = "";
                $scope.LoadRegionData($scope.Item);
            }
        }
    }

    Common.ALL.Get($http, {
        url: Common.ALL.URL.Country,
        success: function (data) {
            _VEN_VENIndex.Data.Country = data;
            $scope.VENVendor_win_cboCountryOptions.dataSource.data(data);
        }
    })

    $scope.VENVendor_win_cboProvinceOptions = {
        autoBind: true,
        valuePrimitive: true,
        ignoreCase: true,
        filter: 'contains',
        suggest: true,
        dataTextField: 'ProvinceName',
        dataValueField: 'ID',
        minLength: 3,
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    ProvinceName: { type: 'string' },
                }
            }
        }),
        change: function (e) {
            var cbo = this;
            if (e.sender.selectedIndex >= 0) {
                $scope.Item.DistrictID = -1;
                $scope.LoadRegionData($scope.Item);
            }
            else {
                $scope.Item.ProvinceID = "";
                $scope.Item.DistrictID = "";
                $scope.LoadRegionData($scope.Item);
            }
        }
    }

    Common.ALL.Get($http, {
        url: Common.ALL.URL.Province,
        success: function (data) {
            _VEN_VENIndex.Data.Province = {};
            angular.forEach(data, function (obj, indx) {
                if (Common.HasValue(_VEN_VENIndex.Data.Province[obj.CountryID]))
                    _VEN_VENIndex.Data.Province[obj.CountryID].push(obj);
                else _VEN_VENIndex.Data.Province[obj.CountryID] = [obj];
            })
        }
    })

    $scope.VENVendor_win_cboDistrictOptions = {
        autoBind: true,
        valuePrimitive: true,
        ignoreCase: true,
        filter: 'contains',
        suggest: true,
        dataTextField: 'DistrictName',
        dataValueField: 'ID',
        minLength: 3,
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    DistrictName: { type: 'string' },
                }
            }
        }),
        change: function (e) {
            var cbo = this;
            if (e.sender.selectedIndex >= 0) {
                $scope.LoadRegionData($scope.Item);
            }
            else {
                $scope.Item.DistrictID = "";
                $scope.LoadRegionData($scope.Item);
            }
        }
    }

    Common.ALL.Get($http, {
        url: Common.ALL.URL.District,
        success: function (data) {
            _VEN_VENIndex.Data.District = {};
            angular.forEach(data, function (obj, indx) {
                if (Common.HasValue(_VEN_VENIndex.Data.District[obj.ProvinceID]))
                    _VEN_VENIndex.Data.District[obj.ProvinceID].push(obj);
                else _VEN_VENIndex.Data.District[obj.ProvinceID] = [obj];
            })
        }
    })

    $scope.LoadRegionData = function (item) {
        Common.Log("LoadRegionData");
        try {
            var countryID = item.CountryID;
            var provinceID = item.ProvinceID;
            var districtID = item.DistrictID;

            var data = _VEN_VENIndex.Data.Province[countryID];
            $scope.VENVendor_win_cboProvinceOptions.dataSource.data(data);
            if (Common.HasValue(provinceID)) {
                if (provinceID < 0) {
                    provinceID = data[0].ID;
                }
            }
            $timeout(function () {
                item.ProvinceID = provinceID;
            }, 1)

            data = _VEN_VENIndex.Data.District[provinceID];
            $scope.VENVendor_win_cboDistrictOptions.dataSource.data(data);
            if (Common.HasValue(districtID)) {
                if (districtID < 0) {
                    districtID = data[0].ID;
                }
            }
            $timeout(function () {
                item.DistrictID = districtID;
            }, 1)
        }
        catch (e) { }
    }

    $scope.Add_Click = function ($event, win) {
        $event.preventDefault();

        Common.Services.Call($http, {
            url: Common.Services.url.VEN,
            method: _VEN_VENIndex.URL.Get,
            data: { 'id': -1 },
            success: function (res) {
                $scope.Item = res;
                $scope.LoadRegionData($scope.Item);
                win.center();
                win.open();
            }
        });
    }

    $scope.change_NameCus = function ($event) {
        $scope.Item.ShortName = $scope.Item.VendorName;
    }

    $scope.Save_Click = function ($event, win, vform) {
        $event.preventDefault();
        $rootScope.IsLoading = true;

        if (vform()) {
            Common.Services.Call($http, {
                url: Common.Services.url.VEN,
                method: _VEN_VENIndex.URL.Save,
                data: { item: $scope.Item },
                success: function (res) {

                    win.close();
                    $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                    $rootScope.IsLoading = false;
                    if (res > 0) {
                        _VEN_VENIndex.Params.id = res;
                        debugger
                        $state.go("main.VENVendor.Detail", _VEN_VENIndex.Params);
                    }
                }
            });
        }
    }

    //#region Action
    $scope.ShowSetting = function ($event) {
        $event.preventDefault();

        $rootScope.ShowSetting({
            ListView: views.VENVendor,
            event: $event,
            current: $state.current
        });
    };
    //#endregion
}]);