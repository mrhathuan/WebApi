/// <reference path="~/Scripts/jquery-1.9.1.intellisense.js" />
/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />
var _CUS_CusIndex = {
    URL: {
        Read: 'Customer_Read',
        Get: 'Customer_Get',
        Save: 'Customer_Update',
        ApproveList: 'CUSCustomer_ApprovedList',
        UnApproveList: 'CUSCustomer_UnApprovedList',
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

angular.module('myapp').controller('CUSCustomerCtrl', ['$rootScope', '$scope', '$http', '$location', '$state', '$timeout', '$stateParams', function ($rootScope, $scope, $http, $location, $state, $timeout, $stateParams) {
    if ($rootScope.IsPageComplete != true) return;
    Common.Log('CUSCustomerCtrl');
    $rootScope.IsLoading = false;
    $scope.Item = null;
    //#region Auth
    $scope.Auth = $rootScope.GetAuth();
    //#endregion

    $scope.CusHasChoose = false;

    $scope.Cus_GridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.CUS,
            method: _CUS_CusIndex.URL.Read,
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
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,Cus_Grid,cus_GridChoose_Change)" />',
                headerAttributes: { style: 'text-align: center;' }, attributes: { style: 'text-align: center;' },
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,Cus_Grid,cus_GridChoose_Change)" />',
                filterable: false, sortable: false
            },
            {
                field: 'Code', width: "135px", title: 'Mã khách hàng', filterable: { cell: { operator: 'contains', showOperators: false } },
                template: "<a class='text' ui-sref='main.CUSCustomer.Detail({id:#=ID#})'>#=Code#</a>"
            },
            {
                field: 'CustomerName', width: "200px", title: 'Khách hàng', filterable: { cell: { operator: 'contains', showOperators: false } },
                template: "<a class='text' ui-sref='main.CUSCustomer.Detail({id:#=ID#})'>#=CustomerName#</a>"
            },
            {
                field: 'Address', width: "263px", title: 'Địa chỉ liên hệ', filterable: { cell: { operator: 'contains', showOperators: false } },
                template: "<a class='text' ui-sref='main.CUSCustomer.Detail({id:#=ID#})'>#=Address#</a>"
            },
            {
                field: 'DistrictName', width: "150px", title: 'Quận huyện', filterable: { cell: { operator: 'contains', showOperators: false } },
                template: "<a class='text' ui-sref='main.CUSCustomer.Detail({id:#=ID#})'>#=DistrictName==null?'':DistrictName#</a>"
            },
            {
                field: 'ProvinceName', width: "120px", title: 'Tỉnh thành', filterable: { cell: { operator: 'contains', showOperators: false } },
                template: "<a class='text' ui-sref='main.CUSCustomer.Detail({id:#=ID#})'>#=ProvinceName==null?'':ProvinceName#</a>"
            },
            {
                field: 'TelNo', width: "190px", title: 'Số điện thoại', filterable: { cell: { operator: 'contains', showOperators: false } },
                template: "<a class='text' ui-sref='main.CUSCustomer.Detail({id:#=ID#})'>#=TelNo==null?'':TelNo#</a>"
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
    };

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
                url: Common.Services.url.CUS,
                method: _CUS_CusIndex.URL.ApproveList,
                data: { lst: datasend },
                success: function (res) {
                    $rootScope.IsLoading = false;
                    $scope.Cus_GridOptions.dataSource.read();
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
                url: Common.Services.url.CUS,
                method: _CUS_CusIndex.URL.UnApproveList,
                data: { lst: datasend },
                success: function (res) {
                    $rootScope.IsLoading = false;
                    $scope.Cus_GridOptions.dataSource.read();
                    $rootScope.Message({ Msg: 'Hủy duyệt thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                },
                error: function (res) {
                    $rootScope.IsLoading = false;
                }
            });

        }
    }

    $scope.CUSCustomer_win_cboCountryOptions = {
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
            _CUS_CusIndex.Data.Country = data;
            $scope.CUSCustomer_win_cboCountryOptions.dataSource.data(data);
        }
    })

    $scope.CUSCustomer_win_cboProvinceOptions = {
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
            _CUS_CusIndex.Data.Province = {};
            angular.forEach(data, function (obj, indx) {
                if (Common.HasValue(_CUS_CusIndex.Data.Province[obj.CountryID]))
                    _CUS_CusIndex.Data.Province[obj.CountryID].push(obj);
                else _CUS_CusIndex.Data.Province[obj.CountryID] = [obj];
            })
        }
    })

    $scope.CUSCustomer_win_cboDistrictOptions = {
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
            _CUS_CusIndex.Data.District = {};
            angular.forEach(data, function (obj, indx) {
                if (Common.HasValue(_CUS_CusIndex.Data.District[obj.ProvinceID]))
                    _CUS_CusIndex.Data.District[obj.ProvinceID].push(obj);
                else _CUS_CusIndex.Data.District[obj.ProvinceID] = [obj];
            })
        }
    })

    $scope.LoadRegionData = function (item) {
        Common.Log("LoadRegionData");
        try {
            var countryID = item.CountryID;
            var provinceID = item.ProvinceID;
            var districtID = item.DistrictID;

            var data = _CUS_CusIndex.Data.Province[countryID];
            $scope.CUSCustomer_win_cboProvinceOptions.dataSource.data(data);
            if (Common.HasValue(provinceID)) {
                if (provinceID < 0) {
                    provinceID = data[0].ID;
                }
            }
            $timeout(function () {
                item.ProvinceID = provinceID;
            }, 1)

            data = _CUS_CusIndex.Data.District[provinceID];
            $scope.CUSCustomer_win_cboDistrictOptions.dataSource.data(data);
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
            url: Common.Services.url.CUS,
            method: _CUS_CusIndex.URL.Get,
            data: { 'id': -1 },
            success: function (res) {
                $scope.Item = res;
                if ($scope.Item.CountryID == null) {
                    $scope.Item.CountryID = _CUS_CusIndex.Data.Country[0].ID;
                    $scope.Item.ProvinceID = -1;
                    $scope.Item.DistrictID = -1;
                }
                $scope.LoadRegionData($scope.Item);
                win.center();
                win.open();
            }
        });
    }

    $scope.change_NameCus = function ($event) {
        $scope.Item.ShortName = $scope.Item.CustomerName;
    }

    $scope.Save_Click = function ($event, win, vform) {
        $event.preventDefault();
       

        if (vform()) {
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.CUS,
                method: _CUS_CusIndex.URL.Save,
                data: { item: $scope.Item },
                success: function (res) {
                    win.close();
                    $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                    $rootScope.IsLoading = false;
                    if (res.ID > 0) {
                        _CUS_CusIndex.Params.id = res.ID;

                        $state.go("main.CUSCustomer.Detail", _CUS_CusIndex.Params);
                    }

                }
            });
        }
    }

    //#region Action
    $scope.ShowSetting = function ($event, grid) {
        $event.preventDefault();

        $rootScope.ShowSetting({
            ListView: views.CUSCustomer,
            event: $event,
            grid: grid,
            current: $state.current
        });
    };
    //#endregion
}]);