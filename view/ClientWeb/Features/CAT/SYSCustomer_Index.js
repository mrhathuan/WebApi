/// <reference path="~/Scripts/jquery-1.9.1.intellisense.js" />
/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />

var _SYSCustomer_Index = {
    URL: {
        Read: 'SYSCustomer_List',
        Save: 'SYSCustomer_Save',
        Delete: 'SYSCustomer_Delete',

        Goal_List: 'SYSCustomer_Goal_List',
        Goal_Save: 'SYSCustomer_Goal_Save',
        Goal_Delete: 'SYSCustomer_Goal_Delete',
        Goal_Reset: 'SYSCustomer_Goal_Reset',

        Goal_Detail_List: 'SYSCustomer_GoalDetail_List',
        Goal_Detail_Save: 'SYSCustomer_GoalDetail_Save'
    },
    Item: {
        SYSCus: {
            ID: 0, Code: '', CustomerName: "", Address: "", ParentID: 1,
            WardID: "", DistrictID: 1, ProvinceID: 1, CountryID: 1,
            TelNo: "", Fax: "", Email: "", BillingName: "", BillingAddress: "",
            TaxCode: "", Note: "", Image: ""
        }
    },
    Data: {
        Province: null,
        District: null,
        Ward: null
    }
}

angular.module('myapp').controller('SYSCustomer_IndexCtrl', ['$rootScope', '$scope', '$http', '$location', '$state', '$timeout', function ($rootScope, $scope, $http, $location, $state, $timeout) {
    if ($rootScope.IsPageComplete != true) return;
    Common.Log('SYSCustomer_IndexCtrl');
    $rootScope.IsLoading = false;

    $scope.Auth = $rootScope.GetAuth();
    
    $scope.Item = null;
    $scope.syscus_grid = null;
    $scope.syscus_gridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.CAT,
            method: _SYSCustomer_Index.URL.Read,
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' }
                }
            }
        }),
        height: '99%', groupable: false, pageable: true, sortable: true, columnMenu: false, resizable: true,
        selectable: 'row', filterable: { mode: 'row' }, reorderable: true,
        columns: [
            {
                title: ' ', width: '50px',
                template: '<a href="/" ng-click="Delete_Click($event,dataItem)" class="k-button"><i class="fa fa-trash"></i></a>',
                filterable: false, sortable: false
            },
            {
                field: 'Code', width: 125, title: '{{RS.SYSCustomer.Code}}',
                template: '<a href="/" ng-click="Detail_Click($event,dataItem,info_win)">#=Code#</a>',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'CustomerName', width: 250, title: '{{RS.SYSCustomer.CustomerName}}',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'Address', title: '{{RS.SYSCustomer.Address}}',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'ProvinceName', width: 150, title: '{{RS.SYSCustomer.ProvinceName}}',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'DistrictName', width: 150, title: '{{RS.SYSCustomer.DistrictName}}',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'TelNo', width: 150, title: '{{RS.SYSCustomer.TelNo}}',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            }            
        ]
    };

    $scope.$watch('syscus_grid', function (newValue, oldValue) {
        if (newValue != null)
            Common.Controls.Grid.ReorderColumns({ Grid: newValue, CookieName: 'SYSCustomer_Grid' });
    });
    
    $scope.Delete_Click = function ($event, item) {
        $event.preventDefault();

        Common.Services.Call($http, {
            url: Common.Services.url.CAT,
            method: _SYSCustomer_Index.URL.Delete,
            data: { item: item },
            success: function (res) {
                $rootScope.Message({
                    Msg: 'Thành công!'
                });
                $scope.syscus_gridOptions.dataSource.read();
            }
        })
    }

    $scope.Detail_Click = function ($event, item, win) {
        $event.preventDefault();

        $scope.Item = $.extend({}, true, item);
        $scope.LoadRegionData($scope.Item);
        win.center().open();
    }
    
    $scope.New_Click = function ($event, win) {
        $event.preventDefault();

        $scope.TabIndex = 0;
        $scope.Item = $.extend({}, true, _SYSCustomer_Index.Item.SYSCus);
        $scope.LoadRegionData($scope.Item);
        win.center().open();
    }

    $scope.TabIndex = 0;
    $scope.info_tabstripOptions = {
        animation: {
            open: { effects: "fadeIn" }
        },
        select: function (e) {
            $timeout(function () {
                $scope.TabIndex = angular.element(e.item).data('tabindex');
            }, 1);
        }
    }

    $scope.info_winOptions = {
        close: function () {
            $scope.info_tabstrip.select(0);
        },
        open: function () {
            $scope.goal_gridOptions.dataSource.read();
        }
    }

    $scope.ImageClick = function ($event, win) {
        $event.preventDefault();

        $rootScope.UploadFile({
            IsImage: true,
            ID: $scope.Item.ID,
            AllowChange: true,
            ShowChoose: false,
            Type: Common.CATTypeOfFileCode.CUSTOMER,
            Window: win,
            Complete: function (image) {
                $scope.Item.Image = image.FilePath;
            }
        });
    }
    
    $scope.cboCountryOptions = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true,
        dataTextField: 'CountryName', dataValueField: 'ID', minLength: 3,
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
        change: function () {
            $scope.Item.ProvinceID = "";
            $scope.Item.DistrictID = "";
            $scope.Item.WardID = "";
            $scope.LoadRegionData($scope.Item);
        }
    }

    $scope.cboProvinceOptions = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true,
        dataTextField: 'ProvinceName', dataValueField: 'ID', minLength: 3,
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
        change: function () {
            $scope.Item.DistrictID = "";
            $scope.Item.WardID = "";
            $scope.LoadRegionData($scope.Item);
        }
    }

    $scope.cboDistrictOptions = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true,
        dataTextField: 'DistrictName', dataValueField: 'ID', minLength: 3,
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
        change: function () {
            $scope.Item.WardID = "";
            $scope.LoadRegionData($scope.Item);
        }
    }

    $scope.cboWardOptions = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true,
        dataTextField: 'WardName', dataValueField: 'ID', minLength: 3,
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    WardName: { type: 'string' },
                }
            }
        })
    }

    Common.ALL.Get($http, {
        url: Common.ALL.URL.Country,
        success: function (res) {
            _SYSCustomer_Index.Data.Province = {};
            $scope.cboCountryOptions.dataSource.data(res);
            Common.Data.Each(res, function (o) {
                _SYSCustomer_Index.Data.Province[o.ID] = [];

                if (_SYSCustomer_Index.Item.SYSCus.CountryID < 1)
                    _SYSCustomer_Index.Item.SYSCus.CountryID = o.ID;
            })

            Common.ALL.Get($http, {
                url: Common.ALL.URL.Province,
                success: function (res) {
                    _SYSCustomer_Index.Data.District = {};
                    Common.Data.Each(res, function (o) {
                        _SYSCustomer_Index.Data.District[o.ID] = [];
                        if (Common.HasValue(_SYSCustomer_Index.Data.Province[o.CountryID]))
                            _SYSCustomer_Index.Data.Province[o.CountryID].push(o);
                        else
                            _SYSCustomer_Index.Data.Province[o.CountryID] = [o];

                        if (_SYSCustomer_Index.Item.SYSCus.ProvinceID < 1 && _SYSCustomer_Index.Item.SYSCus.CountryID == o.CountryID)
                            _SYSCustomer_Index.Item.SYSCus.ProvinceID = o.ID;
                    })

                    Common.ALL.Get($http, {
                        url: Common.ALL.URL.District,
                        success: function (res) {
                            _SYSCustomer_Index.Data.Ward = {};
                            Common.Data.Each(res, function (o) {
                                _SYSCustomer_Index.Data.Ward[o.ID] = [];
                                if (Common.HasValue(_SYSCustomer_Index.Data.District[o.ProvinceID]))
                                    _SYSCustomer_Index.Data.District[o.ProvinceID].push(o);
                                else
                                    _SYSCustomer_Index.Data.District[o.ProvinceID] = [o];

                                if (_SYSCustomer_Index.Item.SYSCus.DistrictID < 1 && _SYSCustomer_Index.Item.SYSCus.ProvinceID == o.ProvinceID)
                                    _SYSCustomer_Index.Item.SYSCus.DistrictID = o.ID;
                            })

                            Common.ALL.Get($http, {
                                url: Common.ALL.URL.Ward,
                                success: function (res) {
                                    Common.Data.Each(res, function (o) {
                                        if (Common.HasValue(_SYSCustomer_Index.Data.Ward[o.DistrictID]))
                                            _SYSCustomer_Index.Data.Ward[o.DistrictID].push(o);
                                        else
                                            _SYSCustomer_Index.Data.Ward[o.DistrictID] = [o];

                                        if (_SYSCustomer_Index.Item.SYSCus.WardID < 1 && _SYSCustomer_Index.Item.SYSCus.DistrictID == o.DistrictID)
                                            _SYSCustomer_Index.Item.SYSCus.WardID = o.ID;
                                    })
                                }
                            })
                        }
                    })
                }
            })
        }
    })
    //#region Action
    $scope.ShowSetting = function ($event, grid) {
        $event.preventDefault();

        $rootScope.ShowSetting({
            ListView: views.SYSCustomer,
            event: $event,
            grid: grid,
            current: $state.current
        });
    };

    $scope.HideSetting = function ($event) {
        $event.preventDefault();

        $rootScope.HideSetting();
    }
    //#endregion

    $scope.LoadRegionData = function (item) {
        Common.Log("LoadRegionData");
        try {
            var countryID = item.CountryID;
            var provinceID = item.ProvinceID;
            var districtID = item.DistrictID;
            var wardID = item.WardID;

            var data = _SYSCustomer_Index.Data.Province[countryID];
            $scope.cboProvinceOptions.dataSource.data(data);
            if (provinceID < 1)
                provinceID = data[0].ID;
            $timeout(function () {
                item.ProvinceID = provinceID;
            }, 1)

            data = _SYSCustomer_Index.Data.District[provinceID];
            $scope.cboDistrictOptions.dataSource.data(data);
            if (districtID < 1)
                districtID = data[0].ID;
            $timeout(function () {
                item.DistrictID = districtID;
            }, 1)

            data = _SYSCustomer_Index.Data.Ward[districtID];
            $scope.cboWardOptions.dataSource.data(data);
            if (wardID < 1 && data.length > 0)
                wardID = data[0].ID;
            $timeout(function () {
                item.WardID = wardID;
            }, 1)
        }
        catch (e) { }
    }

    $scope.Info_Submit_Click = function ($event, grid, win) {
        $event.preventDefault();
        $rootScope.IsLoading = true;
        Common.Services.Call($http, {
            url: Common.Services.url.CAT,
            method: _SYSCustomer_Index.URL.Save,
            data: { item: $scope.Item },
            success: function (res) {
                Common.Services.Error(res, function (res) {
                    $rootScope.Message({ Msg: "Đã cập nhật!" });
                    grid.dataSource.read();
                    $scope.Item.ID = res;
                    win.close();
                    $rootScope.IsLoading = false;
                })
            }
        });
    }

    $scope.Window_Close_Click = function ($event, win) {
        $event.preventDefault();

        win.close();
    }

    $scope.GoalEdit = null;
    $scope.IsGoalEdit = false;
    $scope.IsGoalSaved = false;

    $scope.goal_gridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.CAT,
            method: _SYSCustomer_Index.URL.Goal_List,
            readparam: function () {
                return { branchID: Common.HasValue($scope.Item) ? $scope.Item.ID : -1 };
            },
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' }
                }
            }
        }),
        height: '99%', groupable: false, pageable: false, sortable: false, columnMenu: false, resizable: true,
        selectable: false, filterable: false, reorderable: true, editable: 'inline',
        toolbar: kendo.template($("#goal_grid_toolbar").html()),
        columns: [
             {
                 title: ' ', width: '120px',
                 template: '<a ng-show="!IsGoalEdit && dataItem.ID > 0" href="/" ng-click="Goal_Detail_Click($event,dataItem,goal_win)" class="k-button"><i class="fa fa-history"></i></a>' +
                     '<a ng-show="!IsGoalEdit" href="/" ng-click="Goal_Edit_Click($event,dataItem)" class="k-button"><i class="fa fa-pencil"></i></a>' +
                     '<a ng-show="!IsGoalEdit" href="/" ng-click="Goal_Delete_Click($event,dataItem)" class="k-button"><i class="fa fa-times"></i></a>' +
                     '<a ng-show="IsGoalEdit && GoalEdit.ID==#=ID#?true:false" href="/" ng-click="Goal_Save_Click($event,dataItem)" class="k-button"><i class="fa fa-check"></i></a>' +
                     '<a ng-show="IsGoalEdit && GoalEdit.ID==#=ID#?true:false" href="/" ng-click="Goal_Cancel_Click($event,goal_grid)" class="k-button"><i class="fa fa-ban"></i></a>',
                 filterable: false, sortable: false, attributes: { style: 'text-align: center;' }
             },
            {
                field: 'Code', width: 125, title: '{{RS.SYSCustomer.Code}}',
                editor: "<input type='text' class='k-textbox' ng-model='GoalEdit.Code'>"
            },
            {
                field: 'GoalName', title: '{{RS.SYSCustomer.GoalName}}',
                editor: "<input type='text' class='k-textbox' ng-model='GoalEdit.GoalName'>"
            },
            {
                field: 'Year', width: 150, title: '{{RS.SYSCustomer.Year}}',
                editor: "<input type='number' min:'2000' step='1' class='k-textbox' ng-model='GoalEdit.Year'>"
            },
            {
                field: 'Income', width: 150, title: '{{RS.SYSCustomer.Income}}',
                editor: "<input type='number' min:'0' step='1' class='k-textbox' ng-model='GoalEdit.Income'>"
            },
            {
                field: 'Cost', width: 150, title: '{{RS.SYSCustomer.Cost}}',
                editor: "<input type='number' min:'0' step='1' class='k-textbox' ng-model='GoalEdit.Cost'>"
            }
        ]
    }
    
    $scope.Year = new Date().getFullYear();

    $scope.Goal_AddNew_Click = function ($event, grid) {
        $event.preventDefault();

        var data = grid.dataSource.data();
        data.splice(0, 0, $.extend(true, {}, {
            ID: -1, Code: '', GoalName: '', Cost: 0, Year: $scope.Year, Income: 0
        }));
        grid.dataSource.data(data);

        $timeout(function () {
            $scope.IsGoalSaved = false;
            $scope.IsGoalEdit = true;

            var items = grid.items();
            $scope.GoalEdit = grid.dataItem(items[0]);
            grid.editRow(items[0]);

            $timeout(function () {
                var td = $(items[0]).find('input');
                td[0].focus();
            }, 1);
        }, 1);
    }

    $scope.Goal_Delete_Click = function ($event, item) {
        $event.preventDefault();

        if (Common.HasValue(item)) {
            Common.Services.Call($http, {
                url: Common.Services.url.CAT,
                method: _SYSCustomer_Index.URL.Goal_Delete,
                data: { item: item },
                success: function (res) {
                    Common.Services.Error(res, function (res) {
                        $rootScope.Message({ Msg: "Đã xóa!" });

                        $scope.goal_gridOptions.dataSource.read();
                    })
                }
            });
        }
    }

    $scope.Goal_Edit_Click = function ($event, item) {
        $event.preventDefault();

        $timeout(function () {
            $scope.IsGoalSaved = false;
            $scope.IsGoalEdit = true;

            $scope.GoalEdit = item;

            var tr = $event.target.closest('tr');
            $scope.goal_grid.editRow(tr);

            var td = $(tr).find('input');
            td[0].focus();
        }, 1)
    }

    $scope.Goal_Save_Click = function ($event, item) {
        $event.preventDefault();

        if ($scope.GoalEdit.Code.length > 0 && $scope.GoalEdit.GoalName.length > 0) {
            if (!$scope.IsGoalSaved) {
                $scope.IsGoalSaved = true;

                Common.Services.Call($http, {
                    url: Common.Services.url.CAT,
                    method: _SYSCustomer_Index.URL.Goal_Save,
                    data: { item: $scope.GoalEdit, branchID: $scope.Item.ID },
                    success: function (res) {
                        Common.Services.Error(res, function (res) {
                            $scope.IsGoalEdit = false;

                            $scope.goal_gridOptions.dataSource.read();
                            $rootScope.Message({ Msg: "Đã cập nhật!" });
                        })
                    },
                    error: function () {
                        $scope.IsGoalSaved = false;
                        $rootScope.Message({ Msg: "Lỗi!" });
                    }
                });
            }
        }
        else {
            $rootScope.Message({ Msg: "Thiếu dữ liệu!" });
        }
    }

    $scope.Goal_Cancel_Click = function ($event, grid) {
        $event.preventDefault();
        if ($scope.IsGoalEdit) {
            $scope.IsGoalEdit = false;
            Common.Log("Cancel");
            grid.dataSource.read();
        }
    }

    $scope.GoalID = -1;

    $scope.Goal_Detail_Click = function ($event, item, win) {
        $event.preventDefault();
        $scope.GoalID = item.ID;
        $timeout(function () {
            $scope.goal_detail_gridOptions.dataSource.read();
        }, 1)
        win.center().open();

        $scope.GoalDetailEdit = null;
        $scope.IsGoalDetailEdit = false;
        $scope.IsGoalDetailSaved = false;
    }

    $scope.GoalDetailEdit = null;
    $scope.IsGoalDetailEdit = false;
    $scope.IsGoalDetailSaved = false;

    $scope.goal_detail_gridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.CAT,
            method: _SYSCustomer_Index.URL.Goal_Detail_List,
            readparam: function () {
                return { goalID: $scope.GoalID };
            },
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    Month: { type: 'number', editable: false },
                    DateFrom: { type: 'date', editable: false },
                    DateTo: { type: 'date', editable: false }
                }
            }
        }),
        height: '99%', groupable: false, pageable: false, sortable: false, columnMenu: false, resizable: true,
        selectable: false, filterable: false, reorderable: true, editable: 'inline',
        columns: [
            {
                title: ' ', width: '85px',
                template: '<a ng-show="!IsGoalDetailEdit" href="/" ng-click="Goal_Detail_Edit_Click($event,dataItem)" class="k-button"><i class="fa fa-pencil"></i></a>' +
                    '<a ng-show="IsGoalDetailEdit && GoalDetailEdit.ID==#=ID#?true:false" href="/" ng-click="Goal_Detail_Save_Click($event,dataItem)" class="k-button"><i class="fa fa-check"></i></a>' +
                    '<a ng-show="IsGoalDetailEdit && GoalDetailEdit.ID==#=ID#?true:false" href="/" ng-click="Goal_Detail_Cancel_Click($event,goal_detail_grid)" class="k-button"><i class="fa fa-times"></i></a>',
                filterable: false, sortable: false, attributes: { style: 'text-align: center;' }
            },
            { field: 'Month', width: 125, title: '{{RS.SYSCustomer.Month}}' },
            { field: 'DateFrom', width: 150, title: '{{RS.SYSCustomer.DateFrom}}', template: "#=DateFrom==null?' ':kendo.toString(DateFrom, '" + Common.Date.Format.DMY + "')#" },
            { field: 'DateTo', width: 150, title: '{{RS.SYSCustomer.DateTo}}', template: "#=DateTo==null?' ':kendo.toString(DateTo, '" + Common.Date.Format.DMY + "')#" },
            {
                field: 'Income', width: 150, title: '{{RS.SYSCustomer.Income}}',
                editor: "<input type='number' min:'0' step='1' class='k-textbox' ng-model='GoalDetailEdit.Income'>"
            },
            {
                field: 'Cost', title: '{{RS.SYSCustomer.Cost}}',
                editor: "<input type='number' min:'0' step='1' class='k-textbox' ng-model='GoalDetailEdit.Cost'>"
            }
        ]
    }

    $scope.Goal_Detail_Edit_Click = function ($event, item) {
        $event.preventDefault();

        $timeout(function () {
            $scope.IsGoalDetailSaved = false;
            $scope.IsGoalDetailEdit = true;

            $scope.GoalDetailEdit = item;

            var tr = $event.target.closest('tr');
            $scope.goal_detail_grid.editRow(tr);

            var td = $(tr).find('input');
            td[0].focus();
        }, 1)
    }

    $scope.Goal_Detail_Save_Click = function ($event, item) {
        $event.preventDefault();

        if (!$scope.IsGoalDetailSaved) {
            $scope.IsGoalDetailSaved = true;

            Common.Services.Call($http, {
                url: Common.Services.url.CAT,
                method: _SYSCustomer_Index.URL.Goal_Detail_Save,
                data: { item: $scope.GoalDetailEdit, goalID: $scope.GoalID },
                success: function (res) {
                    Common.Services.Error(res, function (res) {
                        $scope.IsGoalDetailEdit = false;

                        $scope.goal_detail_gridOptions.dataSource.read();
                        $rootScope.Message({ Msg: "Đã cập nhật!" });
                    })
                },
                error: function () {
                    $scope.IsGoalDetailSaved = false;
                    $rootScope.Message({ Msg: "Lỗi!" });
                }
            });
        }
    }

    $scope.Goal_Detail_Cancel_Click = function ($event, grid) {
        $event.preventDefault();
        if ($scope.IsGoalDetailEdit) {
            $scope.IsGoalDetailEdit = false;
            Common.Log("Cancel");
            grid.dataSource.read();
        }
    }

    $scope.Goal_Reset_Click = function ($event, grid) {
        $event.preventDefault();

        Common.Services.Call($http, {
            url: Common.Services.url.CAT,
            method: _SYSCustomer_Index.URL.Goal_Reset,
            data: { goalID: $scope.GoalID },
            success: function (res) {
                Common.Services.Error(res, function (res) {
                    $scope.goal_detail_gridOptions.dataSource.read();
                    $rootScope.Message({ Msg: "Đã cập nhật!" });
                })
            }
        });
    }

}]);