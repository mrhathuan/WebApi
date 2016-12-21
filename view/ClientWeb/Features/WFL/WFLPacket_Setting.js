//ban moi

/// <reference path="~/Scripts/jquery-1.9.1.intellisense.js" />
/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />
//#region Data
var _WFLPacket_Setting = {
    URL: {
        Read: 'WFLPacket_SettingRead',
        Get: 'WFLPacket_SettingGet',
        EventGet: "WFLPacketSetting_EventGet",
        EventSave:"WFLPacketSetting_EventSave",
        SettingType:'WFLPacket_SettingType',
        Save: 'WFLPacketSetting_Save',
        Delete: 'WFLPacket_SettingDelete',
        TemplateRead: 'WFLPacket_SettingTemplateRead',
        SettingUserRead: 'WFLPacket_SettingUserRead',
        CustomerRead: 'WFLPacket_SettingCustomerRead',
        PacketCheck: 'Packet_Check',
        CUSSettingList: 'CUSSettingsPlan_AllList',

        EventTableRead: "WFLSetting_EventTableRead",
        EventFieldRead: 'WFLSetting_EventFieldRead',
        EventSysVar: 'WFLSettingEvent_SysVar',
        EventDelete: "WFLSetting_EventDelete",
    },
    Data:{
        _dataTables: [],
        _dataFields: [],
        _dataTableFields: [],
        _dataAndOr: [],
        _dataOperators: [],
        _dataValues: [],
        _dataTemplates: [],
        _dataTypeOfAction: [],
        _dataTableID: [],
        _dataCustomer: [],
        _dataUser: [],
        _dataStatusOfOrder: [],
        _dataStatusOfPlan: [],
        _dataStatusOfDITOMaster: [],
        _dataStatusOfCOTOMaster: [],
        _dataKPIReason: [],
        _dataStatusOfAssetTimeSheet: [],
        _dataTypeOfAssetTimeSheet: [],
        _dataDITOGroupProductStatus: [],
        _dataDITOGroupProductStatusPOD: [],
        _dataDITOGroupProductStatusPOD: [],
        _dataTypeOfPaymentDITOMaster: [],
        _dataTroubleCostStatus: [],
        _dataDITOLocationStatus: [],
        _dataCOTOLocationStatus: [],
    }

};
//#endregion

angular.module('myapp').controller('WFLPacket_SettingCtrl', ['$rootScope', '$scope', '$http', '$location', '$state', '$timeout', '$stateParams', function ($rootScope, $scope, $http, $location, $state, $timeout, $stateParams) {
    if ($rootScope.IsPageComplete != true) return;
    Common.Log('WFLPacket_IndexCtrl');
    $rootScope.IsLoading = false;
    $scope.ItemEvent = null;
    $scope.Auth = $rootScope.GetAuth();

    $scope.TypePacket = 0;
    $scope.HasChoose = false;
    $scope.UserSMSHasChoose = false;
    $scope.UserMailHasChoose = false;
    $scope.UserAddHasChoose = false;
    $scope.CurrentWinUser = "";
    $scope.CurrentWinEventUser = "";
    $scope.cusId = -1;
    $scope.IsShowTypeOfVar = true;
    $scope.IsShowEmailSms = true;
    $scope.UserEventMailHasChoose = false;
    $scope.UserAddEventMailHasChoose = false;
    $scope.UserTMSHasChoose = false;
    $scope.PacketSettingID = 0;
    $scope.UserEventSMSHasChoose = false;
    $scope.HasEventChoose = false;
    $scope.packetID = 0;

    $scope.PacketSetting_GridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.WFL,
            method: _WFLPacket_Setting.URL.Read,
            model: {
                    id: 'ID',
                    fields: {
                        ID: { type: 'number' },
                        PacketDate: { type: 'DateTime' },
                        IsChoose: { type: 'bool', defaultValue: false },
                    }
                },
            readparam: function () {return {} },
        }),
        height: '100%', pageable: false, sortable: true, columnMenu: false, resizable: true, reorderable: true, filterable: { mode: 'row' },
        columns: [
            {
                title: ' ', width: '80px',
                headerField: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,PacketSetting_Grid,gridChooseChange)" />',
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,PacketSetting_Grid,gridChooseChange)" />' +
                    '<a href="/" ng-click="PacketSetting_Edit_Click($event,dataItem,WFLPaking_win)" class="k-button"><i class="fa fa-pencil"></i></a>',
                filterable: false, sortable: false
            },
            {
                field: 'Code', title: 'Mã', width: '100px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'SettingName', title: 'Tên', width: '150px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'PacketSettingType', title: 'Loại', width: '150px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'CustomerCode', title: 'Mã khách hàng', width: '150px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'CustomerName', title: 'Khách hàng', width: '150px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            { title: '', sortable: false, filterable: false, menu: false }
        ],
    };


    $scope.gridChooseChange = function ($event, grid, hasChoose) {
        $scope.HasChoose = hasChoose;
    }

    $scope.cboCustomer_Options = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true, dataTextField: 'CustomerName', dataValueField: 'ID',
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    CustomerName: { type: 'string' },
                }
            }
        }),
        change: function (e) {
            var ID = this.value();
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.WFL,
                method: _WFLPacket_Setting.URL.CUSSettingList,
                data: { cusId: ID },
                success: function (res) {
                    res.unshift({ ID: -1, Name: '' });
                    $scope.cboCUSSetting_Options.dataSource.data(res);
                    $rootScope.IsLoading = false;
                }
            });
        }
    };

    Common.Services.Call($http, {
        url: Common.Services.url.WFL,
        method: _WFLPacket_Setting.URL.CustomerRead,
        success: function (res) {
            res.unshift({ ID: -1, CustomerName: '' });
            _WFLPacket_Setting.Data._dataCus = res;
            $scope.cboCustomer_Options.dataSource.data(res);
        }
    });

    $scope.cboPacketSetting_Options = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true, dataTextField: 'ValueOfVar', dataValueField: 'ID',
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    ValueOfVar: { type: 'string' },
                }
            }
        }),
        change: function (e) {
            var cbx = this;
            if (e.sender.selectedIndex >= 0) {
                var object = cbx.dataItem(cbx.select());
                if (object != null) {
                    if (object.TypeOfVar == 1 || object.TypeOfVar == 2) {
                        if (Common.HasValue(_WFLPacket_Setting.Data._dataCus) && _WFLPacket_Setting.Data._dataCus.length > 0)
                            $scope.Item.CustomerID = _WFLPacket_Setting.Data._dataCus[0].ID;
                    } else
                        $scope.Item.CustomerID = -1;

                    $scope.TypePacket = object.TypeOfVar;
                    $rootScope.IsLoading = true;
                    Common.Services.Call($http, {
                        url: Common.Services.url.WFL,
                        method: _WFLPacket_Setting.URL.CUSSettingList,
                        data: { cusId: $scope.Item.CustomerID },
                        success: function (res) {
                            res.unshift({ ID: -1, Name: '' });
                            $scope.cboCUSSetting_Options.dataSource.data(res);
                            $rootScope.IsLoading = false;
                        }
                    });

                    if (object.TypeOfVar == 5) {
                        $scope.IsShowTypeOfVar = false;
                    }
                    else {
                        $scope.IsShowTypeOfVar = true;
                    }
                }
            }
        }
    };

    Common.Services.Call($http, {
        url: Common.Services.url.WFL,
        method: _WFLPacket_Setting.URL.SettingType,
        success: function (res) {
            _WFLPacket_Setting.Data._dataPacketSettingType = res;
            $scope.cboPacketSetting_Options.dataSource.data(res);
        }
    }); 

    $scope.cboDriverSMSTemplate_Options = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true, dataTextField: 'Name', dataValueField: 'ID',
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    Name: { type: 'string' },
                }
            }
        }),
        change: function (e) {
        }
    };

    $scope.cboDriverMailTemplate_Options = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true, dataTextField: 'Name', dataValueField: 'ID',
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    Name: { type: 'string' },
                }
            }
        }),
        change: function (e) {
        }
    };

    Common.Services.Call($http, {
        url: Common.Services.url.WFL,
        method: _WFLPacket_Setting.URL.TemplateRead,
        success: function (res) {
            var Driver = res;
            Driver.unshift({ ID: 0, Name: '' });

            $scope.cboDriverSMSTemplate_Options.dataSource.data(Driver);
            $scope.cboDriverMailTemplate_Options.dataSource.data(Driver);
            $scope.WFLSetting_win_cboTemplateOptions.dataSource.data(Driver);
        }
    })

    $scope.PacketSetting_Add_Click = function ($event, win) {
        $event.preventDefault();
        $scope.LoadData_Packet(0, win);
    };

    $scope.PacketSetting_Edit_Click = function ($event, data, win) {
        $event.preventDefault();
        $scope.PacketSettingID = data.ID;
        $scope.LoadData_Packet(data.ID, win);
        var flag = 0;
        $scope.IsShowEmailSms = true;
        $scope.IsShowTypeOfVar = true;
        $.each(_WFLPacket_Setting.Data._dataPacketSettingType, function (i, v) {
            if (v.ID == data.PacketSettingTypeID) {
                if (v.TypeOfVar == 5) {
                    flag = 1;
                }
            }
        });
        if (flag == 1) {
            $scope.IsShowEmailSms = false;
            $scope.IsShowTypeOfVar = false;
        }
    };

    $scope.LoadData_Packet = function (ID, win) {
        $rootScope.IsLoading = true;
        $scope.packetID = ID;
        Common.Services.Call($http, {
            url: Common.Services.url.WFL,
            method: _WFLPacket_Setting.URL.Get,
            data: { id: ID },
            success: function (res) {
                if (Common.HasValue(res)) {
                    $scope.Item = res;
                    $.each(_WFLPacket_Setting.Data._dataPacketSettingType, function (i, m) {
                        if (res.PacketSettingTypeID == m.ID) {
                            $scope.TypePacket = m.TypeOfVar;
                        }
                    });
                    if (Common.HasValue(res.lstEvent)) {
                        $scope.gridEventOptions.dataSource.data(res.lstEvent);
                    }
                    $scope.mail_user_grid.dataSource.data(res.lstUserMail);
                    $scope.sms_user_grid.dataSource.data(res.lstUserSMS);

                    //Load CusSetting
                    Common.Services.Call($http, {
                        url: Common.Services.url.WFL,
                        method: _WFLPacket_Setting.URL.CUSSettingList,
                        data: { cusId: $scope.Item.CustomerID },
                        success: function (res) {
                            res.unshift({ ID: -1, Name: '' });
                            $scope.cboCUSSetting_Options.dataSource.data(res);
                        }
                    });

                    win.center().open();
                    $rootScope.IsLoading = false;

                }
            }
        });
    }

    $scope.Save_Click = function ($event, win, vform) {
        $event.preventDefault();
        if (vform()) {
            $scope.Item.lstUserMail = $scope.mail_user_gridOptions.dataSource.data();
            $scope.Item.lstUserSMS = $scope.sms_user_gridOptions.dataSource.data();
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.WFL,
                method: _WFLPacket_Setting.URL.Save,
                data: { item: $scope.Item},
                success: function (res) {
                    if (Common.HasValue(res)) {
                        $rootScope.Message({
                            Msg: 'Đã cập nhật.',
                            NotifyType: Common.Message.NotifyType.SUCCESS
                        });
                        $scope.PacketSetting_Grid.dataSource.read();
                        $rootScope.IsLoading = false;
                        win.close();
                    }
                }
            });
        } else {
            $rootScope.Message({
                Msg: 'Lỗi dữ liệu.',
                NotifyType: Common.Message.NotifyType.ERROR
            });
        }
    }

    $scope.Check_Click = function ($event, win, vform) {
        $event.preventDefault();
        if (vform()) {
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.Trigger,
                method: _WFLPacket_Setting.URL.PacketCheck,
                success: function (res) {
                    $rootScope.IsLoading = false;
                }
            });
        } else {
            $rootScope.Message({
                Msg: 'Lỗi dữ liệu.',
                NotifyType: Common.Message.NotifyType.ERROR
            });
        }
    }

    $scope.PacketSetting_Delete_Click = function ($event, grid) {
        $event.preventDefault();
        var data = grid.dataSource.data();
        var lst = [];
        $.each(data, function (i, v) {
            if (v.IsChoose == true) {
                lst.push(v.ID);
            }
        });
        $rootScope.Message({
            Msg: "Xác nhận xóa?",
            Type: Common.Message.Type.Confirm,
            Ok: function () {
                $rootScope.IsLoading = true;
                Common.Services.Call($http, {
                    url: Common.Services.url.WFL,
                    method: _WFLPacket_Setting.URL.Delete,
                    data: { lst: lst},
                    success: function (res) {
                        if (Common.HasValue(res)) {
                            $rootScope.Message({
                                Msg: 'Đã cập nhật.',
                                NotifyType: Common.Message.NotifyType.SUCCESS
                            });
                            $scope.PacketSetting_Grid.dataSource.read();
                            $rootScope.IsLoading = false;
                        }
                    }
                });
            }
        })

    }

    $scope.TabIndex = 1;
    $scope.PacketSetting_win_tabOptions = {
        animation: {
            open: { effects: "fadeIn" }
        },
        select: function (e) {
            $timeout(function () {
                $scope.TabIndex = angular.element(e.item).data('tabindex'); //or
            }, 1);
        }
    }

    $scope.WFLSetting_win_cboTemplateOptions = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true, minLength: 3,
        dataTextField: 'Name', dataValueField: 'ID',
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    Name: { type: 'string' },
                }
            }
        })
    };
    
    $scope.mail_user_gridOptions = {
        dataSource: Common.DataSource.Local({
            data: [],
            model: _WFLPacket_Setting.Data._gridUserModel,
        }),
        height: '99%', pageable: false, sortable: true, columnMenu: false, resizable: true, reorderable: true, filterable: { mode: 'row' },
        toolbar: kendo.template($('#WFLSetting_win_gridUserMailToolbar').html()),
        columns: [
          {
              title: ' ', width: '40px',
              headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,mail_user_grid,mail_user_gridChooseChange)" />',
              headerAttributes: { style: 'text-align: center;' },
              template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,mail_user_grid,mail_user_gridChooseChange)" />',
              templateAttributes: { style: 'text-align: center;' },
              filterable: false, sortable: false
          },
          {
              field: 'UserName', title: 'Tên người dùng',
              filterable: { cell: { operator: 'contains', showOperators: false } }
          },
          {
              field: 'Email', title: 'Email',
              filterable: { cell: { operator: 'contains', showOperators: false } }
          },
          { title: ' ', sortable: false, filterable: false, menu: false }
        ],
    };

    $scope.sms_user_gridOptions = {
        dataSource: Common.DataSource.Local({
            data: [],
            model: _WFLPacket_Setting.Data._gridUserModel,
        }),
        height: '99%', pageable: false, sortable: true, columnMenu: false, resizable: true, reorderable: true, filterable: { mode: 'row' },
        toolbar: kendo.template($('#WFLSetting_win_gridUserSMSToolbar').html()),
        columns: [
          {
              title: ' ', width: '40px',
              headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,sms_user_grid,sms_user_gridChooseChange)" />',
              headerAttributes: { style: 'text-align: center;' },
              template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,sms_user_grid,sms_user_gridChooseChange)" />',
              templateAttributes: { style: 'text-align: center;' },
              filterable: false, sortable: false
          },
          {
              field: 'UserName', title: 'Tên người dùng',
              filterable: { cell: { operator: 'contains', showOperators: false } }
          },
          {
              field: 'Email', title: 'Email',
              filterable: { cell: { operator: 'contains', showOperators: false } }
          },
          {
              field: 'TelNo', title: 'Số điện thoại',
              filterable: { cell: { operator: 'contains', showOperators: false } }
          },
          { title: ' ', sortable: false, filterable: false, menu: false }
        ],
    };

    $scope.sms_user_gridChooseChange = function ($event, grid, haschoose) {
        $scope.UserSMSHasChoose = haschoose;
    };

    $scope.mail_user_gridChooseChange = function ($event, grid, haschoose) {
        $scope.UserMailHasChoose = haschoose;
    };

    // Load danh sách User
    Common.Services.Call($http, {
        url: Common.Services.url.WFL,
        method: _WFLPacket_Setting.URL.SettingUserRead,
        success: function (res) {
            if (Common.HasValue(res)) {
                _WFLPacket_Setting.Data._dataUser = res;
            }
        }
    });

    $scope.User_Add_Click = function ($event, grid, win, win_user) {
        $event.preventDefault();
        var lstCurrent = grid.dataSource.data();
        var lstNew = [];
        $.each(_WFLPacket_Setting.Data._dataUser, function (i, v) {
            var check = false;
            $.each(lstCurrent, function (j, m) {
                if (v.ID == m.UserID) {
                    check = true;
                }
            });
            if (!check)
                lstNew.push({ UserID: v.ID, UserName: v.UserName, Email: v.Email, IsChoose: false });
        });

        $scope.add_user_gridOptions.dataSource.data(lstNew);
        win.center().open();
        $timeout(function () {
            $scope.add_user_grid.resize();
            $scope.CurrentWinUser = win_user;
        }, 10);
    }

    $scope.add_user_gridOptions = {
        dataSource: Common.DataSource.Local({
            data: [],
            model: _WFLPacket_Setting.Data._gridUserModel,
        }),
        height: '99%', pageable: false, sortable: true, columnMenu: false, resizable: true, reorderable: true, filterable: { mode: 'row' },
        columns: [
            {
                title: ' ', width: '40px',
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,add_user_grid,user_add_gridChooseChange)" />',
                headerAttributes: { style: 'text-align: center;' },
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,add_user_grid,user_add_gridChooseChange)" />',
                templateAttributes: { style: 'text-align: center;' },
                filterable: false, sortable: false
            },
            {
                field: 'UserName', title: 'Tên người dùng',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'Email', title: 'Email',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'TelNo', title: 'Số điện thoại',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            { title: ' ', sortable: false, filterable: false, menu: false }
        ],
    };

    $scope.user_add_gridChooseChange = function ($event, grid, haschoose) {
        $scope.UserAddHasChoose = haschoose;
    };

    $scope.User_Delete_Click = function ($event, grid) {
        $event.preventDefault();
        var lstCurrent = grid.dataSource.data();
        var lstNew = [];
        $.each(lstCurrent, function (i, v) {
            if (!v.IsChoose)
                lstNew.push({ UserID: v.UserID, UserName: v.UserName, IsChoose: v.IsChoose, UserMail: v.UserMail });
        });
        $timeout(function () {
            grid.dataSource.data(lstNew);
            $rootScope.Message({
                Msg: 'Đã Xóa.',
                NotifyType: Common.Message.NotifyType.SUCCESS
            });
        }, 10);
    }

    $scope.Add_User_Add_Click = function ($event, grid, win) {
        $event.preventDefault();
        var lstCurrent = grid.dataSource.data();
        var lstNew = [];

        switch ($scope.CurrentWinUser) {
            case 'Mail': lstNew = $scope.mail_user_gridOptions.dataSource.data(); break;
            case 'SMS': lstNew = $scope.sms_user_gridOptions.dataSource.data(); break;
        }

        $.each(lstCurrent, function (i, v) {
            if (v.IsChoose == true) {
                lstNew.push({ UserID: v.UserID, UserName: v.UserName, Email: v.Email, IsChoose: false });
            }
        });

        $timeout(function () {
            switch ($scope.CurrentWinUser) {
                case 'Mail': $scope.mail_user_gridOptions.dataSource.data(lstNew); break;
                case 'SMS': $scope.sms_user_gridOptions.dataSource.data(lstNew); break;
            }
            win.close();
        }, 10);
    };

    $scope.Win_Close = function ($event, win) {
        $event.preventDefault();
        win.close();
    }

    $scope.cboCUSSetting_Options = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true, dataTextField: 'Name', dataValueField: 'ID',
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    Name: { type: 'string' },
                }
            }
        }),
        change: function (e) {
        }
    };

    Common.Services.Call($http, {
        url: Common.Services.url.WFL,
        method: _WFLPacket_Setting.URL.CUSSettingList,
        data: { cusId: $scope.cusId },
        success: function (res) {
            res.unshift({ ID: -1, Name:''});
            $scope.cboCUSSetting_Options.dataSource.data(res);
        }
    });

    $scope.DriverSMS_Click = function ($event) {
        if ($scope.Item.IsDriverSMS == false) {
            $scope.cboDriverSMSTemplate_cboGroup.select(0);
        }
    }

    $scope.DriverMail_Click = function ($event) {
        if ($scope.Item.IsDriverMail == false) {
            $scope.cboDriverMailTemplatecboGroup.select(0);
        }
    }

    //#region Event
    $scope.gridEventOptions = {
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    EventName: { type: 'string' },
                    Code: { type: 'string' },
                    IsApproved: { type: 'bool' },
                    IsSystem: { type: 'bool' },
                }
            }
        }),
        height: '100%', pageable: false, sortable: true, columnMenu: false, resizable: true, reorderable: true, filterable: { mode: 'row' },
        toolbar: kendo.template($('#WFLEvent_gridToolbar').html()),
        columns: [
            {
                title: ' ', width: '40px',
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,gridevent,gridEventChooseChange)" />',
                headerAttributes: { style: 'text-align: center;' },
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,gridevent,gridEventChooseChange)" />',
                templateAttributes: { style: 'text-align: center;' },
                filterable: false, sortable: false
            },
            {
                title: ' ', width: '50px',
                template: '<a href="/" ng-click="Edit_Click($event,WFLEvent_win,gridevent,WFLEvent_win_vform)" class="k-button"><i class="fa fa-pencil"></i></a>',
                filterable: false, sortable: false
            },
            {
                field: 'Code', title: 'Mã event', width: '200px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'EventName', title: 'Event', width: '300px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'IsApproved', title: 'Hoạt động', width: '90px',
                template: '<input type="checkbox" #= IsApproved ? "checked=checked" : "" # disabled="disabled" />',
                filterable: false, sortable: false
            },
            {
                field: 'IsSystem', title: 'Hệ thống', width: '90px',
                template: '<input type="checkbox" #= IsSystem ? "checked=checked" : "" # disabled="disabled" />',
                filterable: false, sortable: false
            },
            { title: ' ', sortable: false, filterable: false, menu: false }
        ],
    };

    $scope.gridEventChooseChange = function ($event, grid, haschoose) {
        $scope.HasEventChoose = haschoose;
    };
   

    $scope.DelEvent_Click = function ($event, grid) {
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
                    $rootScope.IsLoading = true;
                    Common.Services.Call($http, {
                        url: Common.Services.url.WFL,
                        method: _WFLPacket_Setting.URL.EventDelete,
                        data: pars,
                        success: function (res) {
                            Common.Services.Call($http, {
                                url: Common.Services.url.WFL,
                                method: _WFLPacket_Setting.URL.Get,
                                data: { id: $scope.packetID },
                                success: function (res) {
                                    if (Common.HasValue(res)) {
                                        $scope.Item = res;
                                        $scope.Item.IsAutoSend = true;
                                        if (Common.HasValue(res.lstEvent)) {
                                            $scope.gridEventOptions.dataSource.data(res.lstEvent);
                                        }
                                        $rootScope.IsLoading = false;

                                    }
                                }
                            });
                            $scope.HasEventChoose = false;
                            $rootScope.Message({ Msg: 'Đã xóa', NotifyType: Common.Message.NotifyType.ERROR });
                        }
                    });
                }
            });
        }
    };

    // Load danh sách Field
    Common.Services.Call($http, {
        url: Common.Services.url.WFL,
        method: _WFLPacket_Setting.URL.EventFieldRead,
        success: function (res) {
            if (Common.HasValue(res)) {
                _WFLPacket_Setting.Data._dataFields = [];
                $.each(res, function (i, v) {
                    _WFLPacket_Setting.Data._dataFields.push({ TableCode: v.TableName, Code: v.ColumnName, Name: v.ColumnDescription, Type: v.ColumnType, IsApproved: v.IsApproved, ID: v.ID });
                });

                // Load danh sách Table
                Common.Services.Call($http, {
                    url: Common.Services.url.WFL,
                    method: _WFLPacket_Setting.URL.EventTableRead,
                    success: function (res) {
                        if (Common.HasValue(res)) {
                            _WFLPacket_Setting.Data._dataTables = [];
                            _WFLPacket_Setting.Data._dataTableID = [];
                            $.each(res, function (i, v) {
                                if (v.TableName != "") {
                                    _WFLPacket_Setting.Data._dataTables.push({ Code: v.TableName, Name: v.TableDescription });
                                    _WFLPacket_Setting.Data._dataTableID[i] = v.TableName;
                                }
                            });

                            if (_WFLPacket_Setting.Data._dataTables.length > 0 && _WFLPacket_Setting.Data._dataFields.length > 0)
                                $scope.InitTableFieldData();
                        }
                    }
                });

            }
        }
    });


    $scope.InitBaseData = function () {
        Common.Log("InitBaseData");
        // Clear data
        _WFLPacket_Setting.Data._dataAndOr = [];
        _WFLPacket_Setting.Data._dataBool = [];
        _WFLPacket_Setting.Data._dataOperators = [];

        // Tạo data cho And/Or
        _WFLPacket_Setting.Data._dataAndOr.push({ ID: 1, Code: "", Name: "" });
        _WFLPacket_Setting.Data._dataAndOr.push({ ID: 2, Code: "And", Name: "And" });
        _WFLPacket_Setting.Data._dataAndOr.push({ ID: 3, Code: "Or", Name: "Or" });

        //Tao data cho bool
        _WFLPacket_Setting.Data._dataBool.push({ ID: 1, Code: "null", Name: "null" });
        _WFLPacket_Setting.Data._dataBool.push({ ID: 2, Code: "true", Name: "true" });
        _WFLPacket_Setting.Data._dataBool.push({ ID: 3, Code: "false", Name: "false" });

        // Tạo data cho Operator
        _WFLPacket_Setting.Data._dataOperators.push({ ID: 1, Code: "Equal", Name: "=", Description: "Bằng" });
        _WFLPacket_Setting.Data._dataOperators.push({ ID: 2, Code: "NotEqual", Name: "<>", Description: "Khác" });
        _WFLPacket_Setting.Data._dataOperators.push({ ID: 3, Code: "Great", Name: ">", Description: "Lớn hơn" });
        _WFLPacket_Setting.Data._dataOperators.push({ ID: 4, Code: "Less", Name: "<", Description: "Nhỏ hơn" });
        _WFLPacket_Setting.Data._dataOperators.push({ ID: 5, Code: "GreaterOrEqual", Name: ">=", Description: "Lớn hơn or bằng" });
        _WFLPacket_Setting.Data._dataOperators.push({ ID: 6, Code: "LesserOrEqual", Name: "<=", Description: "Bé hơn or bằng" });
        _WFLPacket_Setting.Data._dataOperators.push({ ID: 7, Code: "EqualField", Name: "= [Field]", Description: "Bằng với" });
        _WFLPacket_Setting.Data._dataOperators.push({ ID: 8, Code: "NotEqualField", Name: "<> [Field]", Description: "Khác với" });
        _WFLPacket_Setting.Data._dataOperators.push({ ID: 9, Code: "GreatField", Name: "> [Field]", Description: "Lớn hơn so với" });
        _WFLPacket_Setting.Data._dataOperators.push({ ID: 10, Code: "LessField", Name: "< [Field]", Description: "Nhỏ hơn so với" });
        _WFLPacket_Setting.Data._dataOperators.push({ ID: 11, Code: "GreaterOrEqualField", Name: ">= [Field]", Description: "Lớn hơn or bằng so với" });
        _WFLPacket_Setting.Data._dataOperators.push({ ID: 12, Code: "LesserOrEqualField", Name: "<= [Field]", Description: "Nhỏ hơn or bằng so với" });
    };

    $scope.InitBaseData();

    $scope.InitTableFieldData = function () {
        Common.Log("InitTableFieldData");
        _WFLPacket_Setting.Data._dataTableFields = [];
        var lstTableID = _WFLPacket_Setting.Data._dataTableID;
        var lstField = _WFLPacket_Setting.Data._dataFields;
        var lstTable = _WFLPacket_Setting.Data._dataTables;
        if (lstTable.length > 0 && lstField.length > 0) {
            $.each(lstField, function (i, v) {
                var tableCode = v.TableCode;
                var idx = lstTableID.indexOf(tableCode);
                if (idx >= 0) {
                    if (!Common.HasValue(_WFLPacket_Setting.Data._dataTableFields[idx]))
                        _WFLPacket_Setting.Data._dataTableFields[idx] = [];
                    _WFLPacket_Setting.Data._dataTableFields[idx].push(v);
                }
            });
        }
        //$scope.cboTableOptions.dataSource.data(_WFLPacket_Setting.Data._dataTables);
    };

    $scope.AddEvent_Click = function ($event, win, vform) {
        $event.preventDefault();

        $scope.LoadItem(win, -1, vform);
    };

    $scope.Edit_Click = function ($event, win, grid, vform) {
        $event.preventDefault();

        var tr = $($event.target).closest('tr');
        var item = grid.dataItem(tr);

        $scope.LoadItem(win, item.ID, vform);
    };

    $scope.LoadItem = function (win, id, vform) {
        vform({ clear: true });

        Common.Services.Call($http, {
            url: Common.Services.url.WFL,
            method: _WFLPacket_Setting.URL.EventGet,
            data: { id: id },
            success: function (res) {
                if (Common.HasValue(res)) {
                    $timeout(function () {
                        $scope.ItemEvent = res;
                        $scope.gridExpressionOptions.dataSource.data(res.lstField);
                        $scope.event_mail_user_grid.dataSource.data(res.lstUserMail);
                        $scope.tms_user_grid.dataSource.data(res.lstUserTMS);
                        $scope.event_sms_user_grid.dataSource.data(res.lstUserSMS);
                    }, 1);
                }
            }
        });

        win.center();
        win.open();
    }

    $scope.gridExpressionOptions = {
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    EventName: { type: 'string' },
                    Code: { type: 'string' },
                    IsApproved: { type: 'bool' },
                }
            },
        }),
        height: '99%', pageable: false, sortable: true, columnMenu: false, resizable: true, reorderable: true, filterable: { mode: 'row' },
        toolbar: kendo.template($('#WFLSetting_win_gridExpressionToolbar').html()),
        columns: [
            {
                title: ' ', width: '40px',
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,gridExpression,gridExpressionChooseChange)" />',
                headerAttributes: { style: 'text-align: center;' },
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,gridExpression,gridExpressionChooseChange)" />',
                templateAttributes: { style: 'text-align: center;' },
                filterable: false, sortable: false
            },
            {
                field: 'OperatorCode', width: '80px', headerTemplate: '<span title="Lựa chọn And/Or">And/Or</span>',
                template: '<input focus-k-combobox class="cus-combobox cboOperator" data-bind="value:OperatorCode" value="#=OperatorCode#"/>',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'TableCode', headerTemplate: '<span title="Lựa chọn bảng">Bảng dữ liệu</span>', width: '150px',
                template: '<input focus-k-combobox class="cus-combobox cboTable" data-bind="value:TableCode" value="#=TableCode#"/>',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'FieldID', headerTemplate: '<span title="Lựa chọn trường trong bảng">Trường dữ liệu</span>', width: '150px',
                template: '<input focus-k-combobox class="cus-combobox cboField"  data-bind="value:FieldID" value="#=FieldID#"/>',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'OperatorValue', headerTemplate: '<span title="Lựa chọn kiểu so sánh">Kiểu so sánh</span>', width: '100px',
                template: '<input focus-k-combobox class="cus-combobox cboOperatorValue" data-bind="value:OperatorValue" value="#=OperatorValue#"/>',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'CompareValue', headerTemplate: '<span title="Lựa chọn giá trị so sánh">Giá trị so sánh</span>', width: '180px',
                template: '<input class="clText k-textbox" type="text" k-ng-model="dataItem.CompareValue" style="width:100%; display:none"/>' +
                    '<input kendo-numeric-text-box class="clNumber" k-min="0" k-ng-model="dataItem.CompareValue" style="width:100%; display:none"/>' +
                    '<input focus-k-combobox class="cus-combobox clBool" data-bind="value:CompareValue" value="#=CompareValue#" style="width:100%; display:none"/>' +
                    '<input focus-k-combobox class="cus-combobox clFieldChoose" data-bind="value:CompareValue" value="#=CompareValue#" style="width:100%; display:none"/>' +
                    '<input class="k-textbox clDate" ng-model="dataItem.CompareValue" style="width:100%"/>' +
                    '<input focus-k-combobox class="cus-combobox clSysVar" data-bind="value:CompareValue" value="#=CompareValue#" style="width:100%; display:none"/>',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            { title: ' ', sortable: false, filterable: false, menu: false }
        ],
        dataBound: function () {
            this.items().each(function () {
                var itemRow = $scope.gridExpression.dataItem($(this));
                var lstTableID = _WFLPacket_Setting.Data._dataTableID;
                var idx = lstTableID.indexOf(itemRow.TableCode);
                var dataSysVar = $scope.loadDataCompareValue(this, itemRow);
                $(this).find('input.cboOperator').kendoComboBox({
                    autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true, minLength: 3,
                    dataTextField: 'Name', dataValueField: 'Code',
                    dataSource: Common.DataSource.Local({
                        data: _WFLPacket_Setting.Data._dataAndOr,
                        model: {
                            id: 'Code',
                            fields: {
                                Code: { type: 'string' },
                                Name: { type: 'string' },
                            }
                        }
                    }),
                    change: function () {
                        var tr = $(this.wrapper).closest('tr');
                        var item = $scope.gridExpression.dataItem(tr);
                        item.OperatorCode = this.value();
                    }
                });
                $(this).find('input.cboTable').kendoComboBox({
                    autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true, minLength: 3,
                    dataTextField: 'Name', dataValueField: 'Code',
                    dataSource: Common.DataSource.Local({
                        data: _WFLPacket_Setting.Data._dataTables,
                        model: {
                            id: 'Code',
                            fields: {
                                Code: { type: 'string' },
                                Name: { type: 'string' },
                            }
                        }
                    }),
                    change: function () {
                        var tr = $(this.wrapper).closest('tr');
                        var item = $scope.gridExpression.dataItem(tr);
                        item.TableCode = this.value();
                        var lstTableID = _WFLPacket_Setting.Data._dataTableID;
                        var idx = lstTableID.indexOf(item.TableCode);
                        if (idx >= 0) {
                            var cboField = $($(tr).find('input.cboField')[1]).data("kendoComboBox");
                            cboField.dataSource.data(_WFLPacket_Setting.Data._dataTableFields[idx]);
                            item.FieldID = _WFLPacket_Setting.Data._dataTableFields[idx][0].ID;
                            item.FieldCode = _WFLPacket_Setting.Data._dataTableFields[idx][0].Code;
                            cboField.text(_WFLPacket_Setting.Data._dataTableFields[idx][0].Name);
                            cboField.trigger("change");
                        }
                    }
                });

                $(this).find('input.cboField').kendoComboBox({
                    autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true, minLength: 3,
                    dataTextField: 'Name', dataValueField: 'ID',
                    dataSource: Common.DataSource.Local({
                        data: _WFLPacket_Setting.Data._dataTableFields[idx],
                        model: {
                            id: 'ID',
                            fields: {
                                ID: { type: 'number' },
                                Name: { type: 'string' },
                            }
                        }
                    }),
                    change: function () {
                        var tr = $(this.wrapper).closest('tr');
                        var item = $scope.gridExpression.dataItem(tr);
                        item.FieldID = this.value();
                        item.FieldCode = this.dataItem().Code;
                        item.CompareValue = null;
                        item.Type = this.dataItem().Type;
                        var lstTableID = _WFLPacket_Setting.Data._dataTableID;
                        var idx = lstTableID.indexOf(item.TableCode);

                        if (!$scope.CheckFieldValue(item.OperatorValue)) {
                            $scope.setCheckValueInput(tr, item);
                            $scope.getCompareValue(tr, item)
                        }
                        else {
                            $(tr).find('input.clFieldChoose').val("");
                            $scope.getFieldValFalse(_WFLPacket_Setting.Data._dataTableFields[idx], type, tr);
                        }
                    }
                });

                $(this).find('input.cboOperatorValue').kendoComboBox({
                    autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true, minLength: 3,
                    dataTextField: 'Name', dataValueField: 'Code',
                    dataSource: Common.DataSource.Local({
                        data: _WFLPacket_Setting.Data._dataOperators,
                        model: {
                            id: 'Code',
                            fields: {
                                Code: { type: 'string' },
                                Name: { type: 'string' },
                            }
                        }
                    }),
                    change: function () {
                        var tr = $(this.wrapper).closest('tr');
                        var item = $scope.gridExpression.dataItem(tr);
                        item.OperatorValue = this.value();
                        var lstTableID = _WFLPacket_Setting.Data._dataTableID;
                        var idx = lstTableID.indexOf(item.TableCode);
                        //kiem tra du lieu dau vao
                        if ($scope.CheckFieldValue(item.OperatorValue)) {
                            $(tr).find('input.clFieldChoose').val("");
                            $scope.getFieldValFalse(_WFLPacket_Setting.Data._dataTableFields[idx], type, tr);
                        }
                        else {
                            $scope.setCheckValueInput(tr, item);
                            $scope.getCompareValue(tr, item);
                        }
                    }
                });

                $(this).find('input.clBool').kendoComboBox({
                    autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true, minLength: 3,
                    dataTextField: 'Name', dataValueField: 'Code',
                    dataSource: Common.DataSource.Local({
                        data: _WFLPacket_Setting.Data._dataBool,
                        model: {
                            id: 'Code',
                            fields: {
                                Code: { type: 'string' },
                                Name: { type: 'string' },
                            }
                        }
                    }),
                    change: function () {
                        var tr = $(this.wrapper).closest('tr');
                        var item = $scope.gridExpression.dataItem(tr);
                        item.CompareValue = this.value();
                    }
                });

                $(this).find('input.clFieldChoose').kendoComboBox({
                    autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true, minLength: 3,
                    dataTextField: 'Name', dataValueField: 'Code',
                    dataSource: Common.DataSource.Local({
                        data: [],
                        model: {
                            id: 'Code',
                            fields: {
                                Code: { type: 'string' },
                                Name: { type: 'string' },
                            }
                        }
                    }),
                    change: function () {
                        var tr = $(this.wrapper).closest('tr');
                        var item = $scope.gridExpression.dataItem(tr);
                        item.CompareValue = this.value();
                    }
                });

                $(this).find('input.clSysVar').kendoComboBox({
                    autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true, minLength: 3,
                    dataTextField: 'ValueOfVar', dataValueField: 'ID',
                    dataSource: Common.DataSource.Local({
                        data: dataSysVar,
                        model: {
                            id: 'ID',
                            fields: {
                                ID: { type: 'number' },
                                ValueOfVar: { type: 'string' },
                            }
                        }
                    }),
                    change: function () {
                        var tr = $(this.wrapper).closest('tr');
                        var item = $scope.gridExpression.dataItem(tr);
                        item.CompareValue = this.value();
                    }
                });

                if (Common.HasValue($scope.gridExpression)) {
                    var item = $scope.gridExpression.dataItem(this);
                    //kiem tra du lieu vao comparevalue
                    if (Common.HasValue(item)) {
                        if (!$scope.CheckFieldValue(item.OperatorValue)) {
                            $scope.getCompareValue(this, item);
                        }
                        else {
                            $scope.getFieldValFalse(_WFLPacket_Setting.Data._dataTableFields[idx], item.Type, this);
                        }
                    }
                }
            });
        }
    };
    $scope.ExpressionHasChoose = false;
    $scope.gridExpressionChooseChange = function ($event, grid, haschoose) {
        $scope.ExpressionHasChoose = haschoose;
    };

    $scope.ExpressionDel_Click = function ($event, grid) {
        $event.preventDefault();
        var items = [];
        var data = grid.dataSource.data();
        $.each(data, function (i, v) {
            if (v.IsChoose == true) {
                items.push(v);
            }
        });
        $.each(items, function () {
            grid.dataSource.remove(this);
        });
    };

    $scope.ExpressionAdd_Click = function ($event, grid) {
        $event.preventDefault();
        $timeout(function () {
            var item = { ID: -1, Type: _WFLPacket_Setting.Data._dataTableFields[0][0].Type, OperatorCode: _WFLPacket_Setting.Data._dataAndOr[0].Code, TableCode: _WFLPacket_Setting.Data._dataTables[0].Code, FieldCode: _WFLPacket_Setting.Data._dataTableFields[0][0].Code, FieldID: _WFLPacket_Setting.Data._dataTableFields[0][0].ID, OperatorValue: _WFLPacket_Setting.Data._dataOperators[0].Code, CompareValue: '', IsChoose: false, IsApproved: true, IsModified: false };
            if ($scope.gridExpressionOptions.dataSource.data().length > 0)
                item.OperatorCode = _WFLPacket_Setting.Data._dataAndOr[1].Code;

            $scope.gridExpressionOptions.dataSource.insert($scope.gridExpressionOptions.dataSource.data().length, item);
        }, 10);
    };

    $scope.SaveEvent_Click = function ($event, win, grid, vform) {
        $event.preventDefault();

        if (vform()) {
            $rootScope.IsLoading = true;
            $scope.ItemEvent.lstField = $scope.gridExpression.dataSource.data();
            $scope.ItemEvent.lstUserMail = $scope.event_mail_user_gridOptions.dataSource.data();
            $scope.ItemEvent.lstUserTMS = $scope.tms_user_gridOptions.dataSource.data();
            $scope.ItemEvent.lstUserSMS = $scope.event_sms_user_gridOptions.dataSource.data();
            Common.Services.Call($http, {
                url: Common.Services.url.WFL,
                method: _WFLPacket_Setting.URL.EventSave,
                data: { item: $scope.ItemEvent, packetSettingID: $scope.PacketSettingID },
                success: function (res) {
                    Common.Services.Call($http, {
                        url: Common.Services.url.WFL,
                        method: _WFLPacket_Setting.URL.Get,
                        data: { id: $scope.packetID },
                        success: function (res) {
                            if (Common.HasValue(res)) {
                                $scope.Item = res;
                                if (Common.HasValue(res.lstEvent)) {
                                    $scope.gridEventOptions.dataSource.data(res.lstEvent);
                                }
                                $rootScope.IsLoading = false;
                            }
                        }
                    });
                    $rootScope.IsLoading = false;
                    win.close();
                    $rootScope.Message({ Msg: 'Đã cập nhật' });
                }
            });
        }

    };

    $scope.PacketEvent_win_tabOptions = {
        animation: {
            open: {
                effects: "fadeIn"
            }
        }
    };

    // Expression
    $scope.loadDataCompareValue = function (tr, item) {
        var data = []
        if (item.FieldCode == "StatusOfOrderID" || item.FieldCode == "StatusOfPlanID" || item.FieldCode == "StatusOfDITOMasterID" || item.FieldCode == "StatusOfCOTOMasterID" || item.FieldCode == "ReasonID" || item.FieldCode == "StatusOfAssetTimeSheetID" || item.FieldCode == "TypeOfAssetTimeSheetID"
            || item.FieldCode == "DITOGroupProductStatusID" || item.FieldCode == "DITOGroupProductStatusPODID" || item.FieldCode == "TypeOfPaymentDITOMasterID"
            || item.FieldCode == "TroubleCostStatusID" || item.FieldCode == "DITOLocationStatusID" || item.FieldCode == "COTOLocationStatusID") {
            var cbo = $($(tr).find('input.clSysVar')[1]).data("kendoComboBox");
            switch (item.FieldCode) {
                case "StatusOfOrderID":
                    data = _WFLPacket_Setting.Data._dataStatusOfOrder;
                    break;
                case "StatusOfPlanID":
                    data = _WFLPacket_Setting.Data._dataStatusOfPlan;
                    break;
                case "StatusOfDITOMasterID":
                    data = _WFLPacket_Setting.Data._dataStatusOfDITOMaster;
                    break;
                case "StatusOfCOTOMasterID":
                    data = _WFLPacket_Setting.Data._dataStatusOfCOTOMaster;
                    break;
                case "ReasonID":
                    data = _WFLPacket_Setting.Data._dataKPIReason;
                    break;
                case "StatusOfAssetTimeSheetID":
                    data = _WFLPacket_Setting.Data._dataStatusOfAssetTimeSheet;
                    break;
                case "TypeOfAssetTimeSheetID":
                    data = _WFLPacket_Setting.Data._dataTypeOfAssetTimeSheet;
                    break;
                case "DITOGroupProductStatusID":
                    data = _WFLPacket_Setting.Data._dataDITOGroupProductStatus;
                    break;
                case "DITOGroupProductStatusPODID":
                    data = _WFLPacket_Setting.Data._dataDITOGroupProductStatusPOD;
                    break;
                case "TypeOfPaymentDITOMasterID":
                    data = _WFLPacket_Setting.Data._dataTypeOfPaymentDITOMaster;
                    break;
                case "TroubleCostStatusID":
                    data = _WFLPacket_Setting.Data._dataTroubleCostStatus;
                    break;
                case "DITOLocationStatusID":
                    data = _WFLPacket_Setting.Data._dataDITOLocationStatus;
                    break;
                case "COTOLocationStatusID":
                    data = _WFLPacket_Setting.Data._dataCOTOLocationStatus;
                    break;
            }
        }
        return data;
    };

    // Load danh sách status sysvar
    Common.Services.Call($http, {
        url: Common.Services.url.WFL,
        method: _WFLPacket_Setting.URL.EventSysVar,
        success: function (res) {
            if (Common.HasValue(res)) {
                _WFLPacket_Setting.Data._dataStatusOfOrder = res.ListStatusOfOrder;
                _WFLPacket_Setting.Data._dataStatusOfPlan = res.ListStatusOfPlan;
                _WFLPacket_Setting.Data._dataStatusOfDITOMaster = res.ListStatusOfDITOMaster;
                _WFLPacket_Setting.Data._dataStatusOfCOTOMaster = res.ListStatusOfCOTOMaster;
                _WFLPacket_Setting.Data._dataKPIReason = res.ListKPIReason;
                _WFLPacket_Setting.Data._dataStatusOfAssetTimeSheet = res.ListStatusOfAssetTimeSheet;
                _WFLPacket_Setting.Data._dataTypeOfAssetTimeSheet = res.ListTypeOfAssetTimeSheet;
                _WFLPacket_Setting.Data._dataDITOGroupProductStatus = res.ListDITOGroupProductStatus;
                _WFLPacket_Setting.Data._dataDITOGroupProductStatusPOD = res.ListDITOGroupProductStatusPOD;
                _WFLPacket_Setting.Data._dataTypeOfPaymentDITOMaster = res.ListTypeOfPaymentDITOMaster;
                _WFLPacket_Setting.Data._dataTroubleCostStatus = res.ListTroubleCostStatus;
                _WFLPacket_Setting.Data._dataDITOLocationStatus = res.ListDITOLocationStatus;
                _WFLPacket_Setting.Data._dataCOTOLocationStatus = res.ListCOTOLocationStatus;
            }
        }
    });

    $scope.getCompareValue = function (tr, item) {
        $(tr).find('.clFieldChoose').hide();
        $(tr).find('.clText').hide();
        $(tr).find('.k-numerictextbox').hide();
        $(tr).find('.clBool').closest("span").hide();
        $(tr).find('.clDate').closest("span").hide();
        $(tr).find('.clSysVar').closest("span").hide();

        if (item.FieldCode == "StatusOfOrderID" || item.FieldCode == "StatusOfPlanID" || item.FieldCode == "StatusOfDITOMasterID" || item.FieldCode == "StatusOfCOTOMasterID" || item.FieldCode == "ReasonID" || item.FieldCode == "StatusOfAssetTimeSheetID" || item.FieldCode == "TypeOfAssetTimeSheetID"
            || item.FieldCode == "DITOGroupProductStatusID" || item.FieldCode == "DITOGroupProductStatusPODID" || item.FieldCode == "TypeOfPaymentDITOMasterID" || item.FieldCode == "TroubleCostStatusID" || item.FieldCode == "DITOLocationStatusID" || item.FieldCode == "COTOLocationStatusID") {
            $(tr).find('.clSysVar').closest("span").show();
        } else {
            switch (item.Type) {
                case "int":
                    $(tr).find('.k-numerictextbox').show();
                    break;
                case "bool":
                    $(tr).find('.clBool').closest("span").show();
                    break;
                case "datetime":
                    $(tr).find('.clDate').closest("span").show();
                    break;
                case "string":
                    $(tr).find('.clText').show();
                    break;
            }
        }
    };

    $scope.getFieldValFalse = function (data, type, eplement) {
        var Item = [];
        $.each(data, function (i, v) {
            if (type == v.Type) {
                Item.push(v);
            }
        });
        try {
            $(eplement).find('input.clFieldChoose').eq(1).data('kendoComboBox').dataSource.data(Item);
        }
        catch (e) { }
        $(eplement).find('input.clFieldChoose').closest("span").show();
        $(eplement).find('input.clText').hide();
        $(eplement).find('.k-numerictextbox').hide()
        $(eplement).find('input.clBool').closest("span").hide();
        $(eplement).find('input.clDate').closest("span").hide();
    };

    $scope.setCheckValueInput = function (tr, item) {
        if (item.FieldCode == "StatusOfOrderID" || item.FieldCode == "StatusOfPlanID" || item.FieldCode == "StatusOfDITOMasterID" || item.FieldCode == "StatusOfCOTOMasterID" || item.FieldCode == "ReasonID" || item.FieldCode == "StatusOfAssetTimeSheetID" || item.FieldCode == "TypeOfAssetTimeSheetID"
            || item.FieldCode == "DITOGroupProductStatusID" || item.FieldCode == "DITOGroupProductStatusPODID" || item.FieldCode == "TypeOfPaymentDITOMasterID"
            || item.FieldCode == "TroubleCostStatusID" || item.FieldCode == "DITOLocationStatusID" || item.FieldCode == "COTOLocationStatusID") {
            var cbo = $($(tr).find('input.clSysVar')[1]).data("kendoComboBox");
            switch (item.FieldCode) {
                case "StatusOfOrderID":
                    cbo.dataSource.data(_WFLPacket_Setting.Data._dataStatusOfOrder);
                    break;
                case "StatusOfPlanID":
                    cbo.dataSource.data(_WFLPacket_Setting.Data._dataStatusOfPlan);
                    break;
                case "StatusOfDITOMasterID":
                    cbo.dataSource.data(_WFLPacket_Setting.Data._dataStatusOfDITOMaster);
                    break;
                case "StatusOfCOTOMasterID":
                    data = _WFLPacket_Setting.Data._dataStatusOfCOTOMaster;
                case "ReasonID":
                    cbo.dataSource.data(_WFLPacket_Setting.Data._dataKPIReason);
                    break;
                case "StatusOfAssetTimeSheetID":
                    cbo.dataSource.data(_WFLPacket_Setting.Data._dataStatusOfAssetTimeSheet);
                    break;
                case "TypeOfAssetTimeSheetID":
                    cbo.dataSource.data(_WFLPacket_Setting.Data._dataTypeOfAssetTimeSheet);
                    break;
                case "DITOGroupProductStatusID":
                    cbo.dataSource.data(_WFLPacket_Setting.Data._dataDITOGroupProductStatus);
                    break;
                case "DITOGroupProductStatusPODID":
                    cbo.dataSource.data(_WFLPacket_Setting.Data._dataDITOGroupProductStatusPOD);
                    break;
                case "TypeOfPaymentDITOMasterID":
                    cbo.dataSource.data(_WFLPacket_Setting.Data._dataTypeOfPaymentDITOMaster);
                    break;
                case "TroubleCostStatusID":
                    cbo.dataSource.data(_WFLPacket_Setting.Data._dataTroubleCostStatus);
                    break;
                case "DITOLocationStatusID":
                    cbo.dataSource.data(_WFLPacket_Setting.Data._dataDITOLocationStatus);
                    break;
                case "COTOLocationStatusID":
                    cbo.dataSource.data(_WFLPacket_Setting.Data._dataCOTOLocationStatus);
                    break;
            }
            item.CompareValue = cbo.dataSource.data()[0].ID;
            cbo.text(cbo.dataSource.data()[0].ValueOfVar);
        } else {
            switch (item.Type) {
                case "int": $(tr).find('input.clNumber').val(0); break;
                case "bool":
                    var cbo = $($(tr).find('input.clBool')[1]).data("kendoComboBox");
                    item.CompareValue = cbo.dataSource.data()[0].Code;
                    cbo.text(cbo.dataSource.data()[0].Code);
                case "string": $(tr).find('input.clText').val(""); break;
                default:
                    $(tr).find('input.clDate').val(""); break;
            }
        }
    };

    $scope.CheckFieldValue = function (OperatorValue) {
        var ArrayFieldValue = ["EqualField", "NotEqualField", "GreatField", "LessField", "GreaterOrEqualField", "LesserOrEqualField"];
        var check = false;
        for (var i = 0; i < ArrayFieldValue.length; i++) {
            if (ArrayFieldValue[i] == OperatorValue) {
                check = true;
                break;
            }
        }
        return check;
    };

    $scope.ExpressionDel_Click = function ($event, grid) {
        $event.preventDefault();
        var items = [];
        var data = grid.dataSource.data();
        $.each(data, function (i, v) {
            if (v.IsChoose == true) {
                items.push(v);
            }
        });
        $.each(items, function () {
            grid.dataSource.remove(this);
        });
    };

    $scope.event_mail_user_gridOptions = {
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'UserID',
                fields: {
                    UserID: { type: 'number' },
                    UserName: { type: 'string' },
                    IsChoose: { type: 'bool', defaultValue: false },
                }
            },
        }),
        height: '99%', pageable: false, sortable: true, columnMenu: false, resizable: true, reorderable: true, filterable: { mode: 'row' },
        toolbar: kendo.template($('#EVENTWFLSetting_win_gridUserMailToolbar').html()),
        columns: [
          {
              title: ' ', width: '40px',
              headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,event_mail_user_grid,event_mail_user_gridChooseChange)" />',
              headerAttributes: { style: 'text-align: center;' },
              template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,event_mail_user_grid,event_mail_user_gridChooseChange)" />',
              templateAttributes: { style: 'text-align: center;' },
              filterable: false, sortable: false
          },
          {
              field: 'UserName', title: 'Tên người dùng',
              filterable: { cell: { operator: 'contains', showOperators: false } }
          },
          {
              field: 'Email', title: 'Email',
              filterable: { cell: { operator: 'contains', showOperators: false } }
          },
          { title: ' ', sortable: false, filterable: false, menu: false }
        ],
    };

    $scope.User_event_Add_Click = function ($event, grid, win, win_user) {
        $event.preventDefault();
        var lstCurrent = grid.dataSource.data();
        var lstNew = [];
        $.each(_WFLPacket_Setting.Data._dataUser, function (i, v) {
            var check = false;
            $.each(lstCurrent, function (j, m) {
                if (v.ID == m.UserID) {
                    check = true;
                }
            });
            if (!check)
                lstNew.push({ UserID: v.ID, UserName: v.UserName, Email: v.Email, IsChoose: false });
        });

        $scope.event_add_user_grid.dataSource.data(lstNew);
        win.center().open();
        $timeout(function () {
            $scope.event_add_user_grid.resize();
            $scope.CurrentWinEventUser = win_user;
        }, 10);
    }

    $scope.event_add_user_gridOptions = {
        dataSource: Common.DataSource.Local({
            data: [],
            model: _WFLPacket_Setting.Data._gridUserModel,
        }),
        height: '99%', pageable: false, sortable: true, columnMenu: false, resizable: true, reorderable: true, filterable: { mode: 'row' },
        columns: [
            {
                title: ' ', width: '40px',
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,event_add_user_grid,event_add_user_gridChooseChange)" />',
                headerAttributes: { style: 'text-align: center;' },
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,event_add_user_grid,event_add_user_gridChooseChange)" />',
                templateAttributes: { style: 'text-align: center;' },
                filterable: false, sortable: false
            },
            {
                field: 'UserName', title: 'Tên người dùng',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'Email', title: 'Email',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'TelNo', title: 'Số điện thoại',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            { title: ' ', sortable: false, filterable: false, menu: false }
        ],
    };

    $scope.event_add_user_gridChooseChange = function ($event, grid, haschoose) {
        $scope.UserAddEventMailHasChoose = haschoose;
    };

    $scope.event_mail_user_gridChooseChange = function ($event, grid, haschoose) {
        $scope.UserEventMailHasChoose = haschoose;
    };

    $scope.User_event_Delete_Click = function ($event, grid) {
        $event.preventDefault();
        var lstCurrent = grid.dataSource.data();
        var lstNew = [];
        $.each(lstCurrent, function (i, v) {
            if (!v.IsChoose)
                lstNew.push({ UserID: v.UserID, UserName: v.UserName, IsChoose: v.IsChoose, UserMail: v.UserMail });
        });
        $timeout(function () {
            grid.dataSource.data(lstNew);
            $rootScope.Message({
                Msg: 'Đã Xóa.',
                NotifyType: Common.Message.NotifyType.SUCCESS
            });
        }, 10);
    }

    $scope.event_Add_User_Add_Click = function ($event, grid, win) {
        $event.preventDefault();
        var lstCurrent = grid.dataSource.data();
        var lstNew = [];

        switch ($scope.CurrentWinEventUser) {
            case 'Mail': lstNew = $scope.event_mail_user_gridOptions.dataSource.data(); break;
            case 'TMS': lstNew = $scope.tms_user_gridOptions.dataSource.data(); break;
            case 'SMS': lstNew = $scope.event_sms_user_gridOptions.dataSource.data(); break;
        }

        $.each(lstCurrent, function (i, v) {
            if (v.IsChoose == true) {
                lstNew.push({ UserID: v.UserID, UserName: v.UserName, Email: v.Email, IsChoose: false });
            }
        });

        $timeout(function () {
            switch ($scope.CurrentWinEventUser) {
                case 'Mail': $scope.event_mail_user_gridOptions.dataSource.data(lstNew); break;
                case 'TMS': lstNew = $scope.tms_user_gridOptions.dataSource.data(lstNew); break;
                case 'SMS': $scope.event_sms_user_gridOptions.dataSource.data(lstNew); break;
            }
            win.close();
        }, 10);
    };

    $scope.tms_user_gridOptions = {
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'UserID',
                fields: {
                    UserID: { type: 'number' },
                    UserName: { type: 'string' },
                }
            },
        }),
        height: '99%', pageable: false, sortable: true, columnMenu: false, resizable: true, reorderable: true, filterable: { mode: 'row' },
        toolbar: kendo.template($('#WFLSetting_win_gridUserTMSToolbar').html()),
        columns: [
          {
              title: ' ', width: '40px',
              headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,tms_user_grid,tms_user_gridChooseChange)" />',
              headerAttributes: { style: 'text-align: center;' },
              template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,tms_user_grid,tms_user_gridChooseChange)" />',
              templateAttributes: { style: 'text-align: center;' },
              filterable: false, sortable: false
          },
          {
              field: 'UserName', title: 'Tên người dùng',
              filterable: { cell: { operator: 'contains', showOperators: false } }
          },
          {
              field: 'Email', title: 'Email',
              filterable: { cell: { operator: 'contains', showOperators: false } }
          },
          { title: ' ', sortable: false, filterable: false, menu: false }
        ],
    };

    $scope.tms_user_gridChooseChange = function ($event, grid, haschoose) {
        $scope.UserTMSHasChoose = haschoose;
    };

    $scope.event_sms_user_gridOptions = {
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'UserID',
                fields: {
                    UserID: { type: 'number' },
                    UserName: { type: 'string' },
                    IsChoose: { type: 'bool', defaultValue: false },
                }
            },
        }),
        height: '99%', pageable: false, sortable: true, columnMenu: false, resizable: true, reorderable: true, filterable: { mode: 'row' },
        toolbar: kendo.template($('#WFLSetting_win_gridUserEventSMSToolbar').html()),
        columns: [
          {
              title: ' ', width: '40px',
              headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,event_sms_user_grid,event_sms_user_gridChooseChange)" />',
              headerAttributes: { style: 'text-align: center;' },
              template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,event_sms_user_grid,event_sms_user_gridChooseChange)" />',
              templateAttributes: { style: 'text-align: center;' },
              filterable: false, sortable: false
          },
          {
              field: 'UserName', title: 'Tên người dùng',
              filterable: { cell: { operator: 'contains', showOperators: false } }
          },
          {
              field: 'Email', title: 'Email',
              filterable: { cell: { operator: 'contains', showOperators: false } }
          },
          {
              field: 'TelNo', title: 'Số điện thoại',
              filterable: { cell: { operator: 'contains', showOperators: false } }
          },
          { title: ' ', sortable: false, filterable: false, menu: false }
        ],
    };

    $scope.event_sms_user_gridChooseChange = function ($event, grid, haschoose) {
        $scope.UserEventSMSHasChoose = haschoose;
    };
    //#endregion

    //#region action
    $scope.ShowSetting = function ($event, grid) {
        $event.preventDefault();

        $rootScope.ShowSetting({
            ListView: views.WFLPacket,
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
}]);
