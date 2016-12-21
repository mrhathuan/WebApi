/// <reference path="~/Scripts/jquery-1.9.1.intellisense.js" />
/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />

//#region Data
var _SYSUserChild_Index = {
    URL: {
        Read: 'SYSUserChild_Read',
        Get: 'SYSUserChild_Get',
        Group: 'SYSUserChild_Group',
        Customer: 'SYSUserChild_Customer',
        CheckData: 'SYSUserChild_CheckData',
        Save: 'SYSUserChild_Save',
        Delete: 'SYSUserChild_Delete',

        Export: 'SYSUserChild_Export',
        ImportCheck: 'SYSUserChild_ImportCheck',
        Import: 'SYSUserChild_Import',
        DriverList: 'SYSUserChild_Driver',
        SetDefaultByUser: 'SYSUserChildResource_SetDefaultByUser',

        CheckAdmin: 'SYSUser_CheckIsAdmin',
    }
};
//#endregion

angular.module('myapp').controller('SYSUserChild_IndexCtrl', ['$rootScope', '$scope', '$http', '$location', '$state', '$timeout', '$stateParams', function ($rootScope, $scope, $http, $location, $state, $timeout, $stateParams) {
    if ($rootScope.IsPageComplete != true) return;
    Common.Log('SYSUserChild_IndexCtrl');
    $rootScope.IsLoading = false;

    $scope.Auth = $rootScope.GetAuth();

    $scope.HasChoose = false;
    $scope.Item = null;
    $scope.IsAdmin = false;

    //Check Admin
    Common.Services.Call($http, {
        url: Common.Services.url.SYS,
        method: _SYSUserChild_Index.URL.CheckAdmin,
        data: { },
        success: function (res) {
            debugger
            $scope.IsAdmin = res;
        },
        error: function (res) {
            $scope.IsAdmin = false;
        }
    });

    $scope.sysuser_gridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.SYS,
            method: _SYSUserChild_Index.URL.Read,
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    IsChoose: { type: 'bool', defaultValue: false },
                    IsAdmin: { type: 'bool', defaultValue: false },
                    IsApproved: { type: 'bool', defaultValue: true },
                    GroupName: { type: 'string', defaultValue: '' },
                    CreatedDate: { type: 'date' }
                }
            }
        }),
        height: '100%', pageable: true, sortable: true, columnMenu: false, resizable: true, reorderable: true, filterable: { mode: 'row' },
        toolbar: kendo.template($('#sysuser_gridToolbar').html()),
        columns: [
            {
                title: ' ', width: '40px',
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,sysuser_grid,sysuser_gridChooseChange)" />',
                headerAttributes: { style: 'text-align: center;' },
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,sysuser_grid,sysuser_gridChooseChange)" />',
                templateAttributes: { style: 'text-align: center;' },
                filterable: false, sortable: false
            },
            {
                title: ' ', width: '50px',
                template: '<a href="/" ng-click="Edit_Click($event,sysuser_win,sysuser_grid,sysuser_win_vform)" class="k-button"><i class="fa fa-pencil"></i></a>',
                filterable: false, sortable: false
            },
            {
                field: 'UserName', title: '{{RS.SYSUser.UserName}}', width: '150px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'LastName', title: '{{RS.SYSUser.LastName}}', width: '150px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'FirstName', title: '{{RS.SYSUser.FirstName}}', width: '150px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'GroupName', title: '{{RS.SYSGroup.GroupName}}', width: '150px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'IsApproved', title: '{{RS.SYSUser.IsApproved}}', width: '70px',
                template: '<input type="checkbox" #= IsApproved ? "checked=checked" : "" # disabled="disabled" />',
                filterable: false, sortable: false
            },
            {
                field: 'IsAdmin', title: '{{RS.SYSUser.IsAdmin}}', width: '70px',
                template: '<input type="checkbox" #= IsAdmin ? "checked=checked" : "" # disabled="disabled" />',
                filterable: false, sortable: false
            },
            {
                field: 'CreatedDate', title: '{{RS.SYSUser.CreatedDate}}', width: '150px',
                template: '#=Common.Date.FromJsonDMYHM(CreatedDate)#',
                filterable: { cell: { template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); }, operator: 'equal', showOperators: false } },
            },
            { title: ' ', sortable: false, filterable: false, menu: false }
        ],
        change: function (e) {
            //debugger;
        }
    };

    $scope.sysuser_gridChooseChange = function ($event, grid, haschoose) {
        $scope.HasChoose = haschoose;
    };

    $scope.sysuser_win_tabOptions = {
        animation: {
            open: {
                effects: "fadeIn"
            }
        }
    };

    $scope.Add_Click = function ($event, win, vform) {
        $event.preventDefault();

        $scope.LoadItem(win, -1, vform);
    };
    //#region Action

    $scope.ShowSetting = function ($event, grid) {
        $event.preventDefault();

        $rootScope.ShowSetting({
            ListView: views.SYSUser,
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
    $scope.Edit_Click = function ($event, win, grid, vform) {
        $event.preventDefault();

        var tr = $($event.target).closest('tr');
        var item = grid.dataItem(tr);

        $scope.LoadItem(win, item.ID, vform);
    };

    $scope.sysuser_win_cboGroupOptions = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true, minLength: 3,
        dataTextField: 'Text', dataValueField: 'ValueInt',
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ValueInt',
                fields: {
                    ValueInt: { type: 'number' },
                    ValueString: { type: 'string' },
                    Text: { type: 'string' }
                }
            }
        })
    };

    $scope.sysuser_win_cboCustomerOptions = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true, minLength: 3,
        dataTextField: 'Text', dataValueField: 'ValueInt',
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ValueInt',
                fields: {
                    ValueInt: { type: 'number' },
                    ValueString: { type: 'string' },
                    Text: { type: 'string' }
                }
            }
        })
    };

    Common.Services.Call($http, {
        url: Common.Services.url.SYS,
        method: _SYSUserChild_Index.URL.Customer,
        data: { },
        success: function (res) {
            if (Common.HasValue(res)) {
                $timeout(function () {
                    $scope.sysuser_win_cboCustomerOptions.dataSource.data(res);
                }, 1);
            }
        }
    });

    //load data cbb driver
    Common.Services.Call($http, {
        url: Common.Services.url.SYS,
        method: _SYSUserChild_Index.URL.DriverList,
        data: {},
        success: function (res) {
            if (Common.HasValue(res)) {
                $timeout(function () {
                    res.splice(0, 0, { ValueInt: null, ValueString: null, Text: "" });
                    $scope.driver_cbbOptions.dataSource.data(res);
                }, 1);
            }
        }
    });

    $scope.LoadItem = function (win, id, vform) {
        vform({ clear: true });

        Common.Services.Call($http, {
            url: Common.Services.url.SYS,
            method: _SYSUserChild_Index.URL.Group,
            data: { id: id },
            success: function (res) {
                if (Common.HasValue(res)) {
                    $timeout(function () {
                        $scope.sysuser_win_cboGroupOptions.dataSource.data(res);
                    }, 1);
                }

                Common.Services.Call($http, {
                    url: Common.Services.url.SYS,
                    method: _SYSUserChild_Index.URL.Get,
                    data: { id: id },
                    success: function (res) {
                        if (Common.HasValue(res)) {
                            if (res.GroupID == null)
                                res.GroupID = -1;
                            $scope.Item = res;
                            if (id < 1) {
                                $scope.Item.IsApproved = true;
                            }
                            var lstid = [];
                            if (Common.HasValue(res.ListCustomerID))
                                lstid = res.ListCustomerID.split(',');
                            angular.forEach(res.ListCustomer, function (v, i) {
                                if (lstid.indexOf(v.ID + '') >= 0)
                                    v.IsChoose = true;
                                else
                                    v.IsChoose = false;
                            });

                            $timeout(function () {
                                $scope.sysuser_win_gridOptions.dataSource.data(res.ListCustomer);
                            }, 1);
                        }
                    }
                });
            }
        });

        win.center();
        win.open();
    }

    $scope.Del_Click = function ($event, grid) {
        $event.preventDefault();

        var lstid = [];
        var data = grid.dataSource.data();
        $.each(data, function (i, v) {
            if (v.IsChoose == true)
                lstid.push(v.ID);
        });
        if (lstid.length > 0) {
            $rootScope.Message({
                Type: Common.Message.Type.Confirm,
                Msg: 'Bạn muốn xóa các dữ liệu đã chọn ?',
                pars: { lstid: lstid },
                Ok: function (pars) {
                    Common.Services.Call($http, {
                        url: Common.Services.url.SYS,
                        method: _SYSUserChild_Index.URL.Delete,
                        data: pars,
                        success: function (res) {
                            $scope.sysuser_gridOptions.dataSource.read();

                            $rootScope.Message({ Msg: 'Đã xóa', NotifyType: Common.Message.NotifyType.ERROR });
                        }
                    });
                }
            });
        }
    };

    $scope.Save_Click = function ($event, win, grid, vform) {
        $event.preventDefault();
        if (vform()) {
            var flag = true;
            if ($scope.Item.TelNo != "" && $scope.Item.TelNo != null) {
                if ($.isNumeric($scope.Item.TelNo) && $scope.Item.TelNo.length < 12 && $scope.Item.TelNo.length > 9) {
                    flag = true;
                }
                else {
                    flag = false;
                }
            }
            if (flag == true) {
                $rootScope.IsLoading = true;
                Common.Services.Call($http, {
                    url: Common.Services.url.SYS,
                    method: _SYSUserChild_Index.URL.CheckData,
                    data: { id: $scope.Item.ID, username: $scope.Item.UserName, email: $scope.Item.Email },
                    success: function (res) {
                        $rootScope.IsLoading = false;
                        if (res > 0) {
                            if (res == 1) {
                                vform({ model: 'Item.UserName', error: 'Đã có tài khoản này' });
                                flag = false;
                            }
                            if (res == 2) {
                                vform({ model: 'Item.Email', error: 'Đã có email này' });
                                flag = false;
                            }
                        }
                        else {
                            var flag = true;
                            if ($scope.Item.ID <= 0) {
                                if ($scope.Item.Password == '') {
                                    vform({ model: 'Item.Password', error: 'Chưa nhập mật khẩu' });
                                    flag = false;
                                }
                            }
                            if ($scope.Item.Password != '' && $scope.Item.Password != $scope.Item.RePassword) {
                                vform({ model: 'Item.RePassword', error: 'Mật khẩu khác mật khẩu nhập lại' });
                                flag = false;
                            }
                            if (flag) {
                                var strCustomer = '';
                                angular.forEach($scope.sysuser_win_gridOptions.dataSource.data(), function (v, i) {
                                    if (v.IsChoose == true)
                                        strCustomer += ',' + v.ID;
                                });
                                if (strCustomer != '')
                                    strCustomer = strCustomer.substr(1);
                                $scope.Item.ListCustomerID = strCustomer;
                                //debugger
                                $rootScope.IsLoading = true;
                                Common.Services.Call($http, {
                                    url: Common.Services.url.SYS,
                                    method: _SYSUserChild_Index.URL.Save,
                                    data: { item: $scope.Item },
                                    success: function (res) {
                                        $rootScope.IsLoading = false;
                                        win.close();
                                        $scope.sysuser_gridOptions.dataSource.read();
                                        $rootScope.Message({ Msg: 'Đã cập nhật' });
                                    }
                                });
                            }
                        }
                    }
                });
            }
            else {
                $rootScope.Message({
                    Msg: 'Số điện thoại không đúng.',
                    NotifyType: Common.Message.NotifyType.ERROR
                });
            }
        }
    };

    $scope.Close_Click = function ($event, win) {
        $event.preventDefault();

        win.close();
    };

    $scope.sysuser_win_gridOptions = {
        dataSource: Common.DataSource.Local({
            data: [],
            pageSize: 10,
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    IsChoose: { type: 'bool', defaultValue: false },
                }
            }
        }),
        height: '100%', pageable: true, sortable: true, columnMenu: false, resizable: true, reorderable: true, filterable: { mode: 'row' },
        columns: [
            {
                title: ' ', width: '40px',
                headerTemplate: '<input type="checkbox" ng-click="gridChooseAll_Check($event,sysuser_win_grid)" />',
                headerAttributes: { style: 'text-align: center;' },
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,sysuser_win_grid)" />',
                templateAttributes: { style: 'text-align: center;' },
                filterable: false, sortable: false
            },
            {
                field: 'Code', title: '{{RS.CUSCustomer.Code}}', width: '120px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'CustomerName', title: '{{RS.CUSCustomer.CustomerName}}', width: '200px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'TypeOfCustomerName', title: '{{RS.CUSCustomer.TypeOfCustomerName}}', width: '150px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                title: ' ', filterable: false, sortable: false
            }
        ]
    };

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

    $scope.Excel_Click = function ($event, grid) {
        $event.preventDefault();

        $rootScope.UploadExcel({
            width: '900px',
            height: '500px',
            columns: [
                { field: 'UserName', title: '{{RS.SYSUser.UserName}}', width: '80px', filterable: { cell: { operator: 'contains', showOperators: false } } },
                { field: 'LastName', title: '{{RS.SYSUser.LastName}}', width: '80px', filterable: { cell: { operator: 'contains', showOperators: false } } },
                { field: 'FirstName', title: '{{RS.SYSUser.FirstName}}', width: '80px', filterable: { cell: { operator: 'contains', showOperators: false } } },
                { field: 'Email', title: '{{RS.SYSUser.Email}}', width: '120px', filterable: { cell: { operator: 'contains', showOperators: false } } },
            ],
            Download: function ($event) {
                $rootScope.IsLoading = true;

                Common.Services.Call($http, {
                    url: Common.Services.url.SYS,
                    method: _SYSUserChild_Index.URL.Export,
                    data: {},
                    success: function (res) {
                        $rootScope.IsLoading = false;

                        $rootScope.DownloadFile(res);
                    }
                });
            },
            Upload: function (file, success) {
                $rootScope.IsLoading = true;
                Common.Services.Call($http, {
                    url: Common.Services.url.SYS,
                    method: _SYSUserChild_Index.URL.ImportCheck,
                    data: { file: file },
                    success: function (res) {
                        success(res);
                        $rootScope.IsLoading = false;
                    }
                });
            },
            Complete: function (file, data) {
                $rootScope.IsLoading = true;
                Common.Services.Call($http, {
                    url: Common.Services.url.SYS,
                    method: _SYSUserChild_Index.URL.Import,
                    data: { lst: data },
                    success: function (res) {
                        $rootScope.IsLoading = false;
                    }
                });
            }
        });
    };

    $scope.driver_cbbOptions = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true, minLength: 3,
        dataTextField: 'Text', dataValueField: 'ValueInt',
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ValueInt',
                fields: {
                    ValueInt: { type: 'number' },
                    ValueString: { type: 'string' },
                    Text: { type: 'string' }
                }
            }
        })
    }

    $scope.SetDefault = function ($event) {
        $event.preventDefault();
        $rootScope.IsLoading = true;
        Common.Services.Call($http, {
            url: Common.Services.url.SYS,
            method: _SYSUserChild_Index.URL.SetDefaultByUser,
            data: {userID: $scope.Item.ID},
            success: function (res) {
                $rootScope.IsLoading = false;
                //$scope.SYSUserResource_GridOptions.dataSource.read();
                $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                $rootScope.IsLoading = false;
            },
            error: function (res) {
                $rootScope.IsLoading = false;
            }
        });

    }
}]);