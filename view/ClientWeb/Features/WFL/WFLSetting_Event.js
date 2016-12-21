//ban moi

/// <reference path="~/Scripts/jquery-1.9.1.intellisense.js" />
/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />

var _WFLSetting_WFEvent = {
    URL: {
        WFEventEventRead: 'WFLSetting_WFEvent_Read',
        WFEventEventGet: 'WFLSetting_WFEvent_Get',
        WFEventEventSave: 'WFLSetting_WFEvent_Save',
        WFEventEventDelete: 'WFLSetting_WFEvent_Delete',
        WFEventEventTableRead: 'WFLSetting_WFEvent_TableRead',
        WFEventEventFieldRead: 'WFLSetting_WFEvent_FieldRead',
        WFEventEventTemplateRead: 'WFLSetting_WFEvent_TemplateRead',

        WFEventWFEventEventSysVar: 'WFLSetting_WFEvent_SysVar',
    },
    Data: {
        _gridModel: {
            id: 'ID',
            fields: {
                ID: { type: 'number' },
                EventName: { type: 'string' },
                Code: { type: 'string' },
                IsApproved: { type: 'bool' },
                IsSystem: { type: 'bool' },
                UseCustomer: { type: 'bool' },
                SortOrder: { type: 'number' },
                Expression: { type: 'string' },
            }
        },
        _gridUserModel: {
            id: 'UserID',
            fields: {
                UserID: { type: 'number' },
                UserName: { type: 'string' },
            }
        },
        _gridCustomerModel: {
            id: 'CustomerID',
            fields: {
                CustomerID: { type: 'number' },
                CustomerName: { type: 'string' },
            }
        },
        _gridExpressionModel: {
            id: 'ID',
            fields: {
                ID: { type: 'number' },
                EventName: { type: 'string' },
                Code: { type: 'string' },
                IsApproved: { type: 'bool' },
            }
        },
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


angular.module('myapp').controller('WFLSetting_EventCtrl', ['$rootScope', '$scope', '$http', '$location', '$state', '$timeout', '$stateParams', function ($rootScope, $scope, $http, $location, $state, $timeout, $stateParams) {
    if ($rootScope.IsPageComplete != true) return;
    Common.Log('WFLSetting_EventCtrl');
    $rootScope.IsLoading = false;
    $scope.Auth = $rootScope.GetAuth();


    $scope.HasChoose = false;
    $scope.ExpressionHasChoose = false;
    $scope.Item = null;

    $scope.HasNumber = false;
    $scope.HasBool = false;
    $scope.HasDate = false;
    $scope.ItemField = [];
   
    $scope.UserAddHasChoose = false;

    $scope.CusHasChoose = false;
    $scope.CusAddHasChoose = false;

    $scope.CurrentWinUser = null;

    $scope.WFLSettingWFEvent_win_tabOptions = {
        animation: {
            open: {
                effects: "fadeIn"
            }
        }
    };
    $scope.WFLSettingWFEvent_gridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.WFL,
            method: _WFLSetting_WFEvent.URL.WFEventEventRead,
            model: _WFLSetting_WFEvent.Data._gridModel,
        }),
        height: '100%', pageable: true, sortable: true, columnMenu: false, resizable: true, reorderable: true, filterable: { mode: 'row' },
        toolbar: kendo.template($('#WFLSettingWFEvent_gridToolbar').html()),
        columns: [
            {
                title: ' ', width: '40px',
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,WFLSettingWFEvent_grid,gridChooseChange)" />',
                headerAttributes: { style: 'text-align: center;' },
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,WFLSettingWFEvent_grid,gridChooseChange)" />',
                templateAttributes: { style: 'text-align: center;' },
                filterable: false, sortable: false
            },
            {
                title: ' ', width: '50px',
                template: '<a href="/" ng-click="Edit_Click($event,WFLSettingWFEvent_win,WFLSettingWFEvent_grid,WFLSettingWFEvent_win_vform)" class="k-button"><i class="fa fa-pencil"></i></a>',
                filterable: false, sortable: false
            },
            {
                field: 'Code', title: 'Mã', width: '200px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'EventName', title: 'Tên sự kiện',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'IsApproved', title: 'Hoạt động', width: '90px',
                template: '<input type="checkbox" #= IsApproved ? "checked=checked" : "" # disabled="disabled" />',
                filterable: false, sortable: false
            },
          
        ],
    };

    $scope.gridChooseChange = function ($event, grid, haschoose) {
        $scope.HasChoose = haschoose;
    };

    $scope.Add_Click = function ($event, win, vform) {
        $event.preventDefault();

        $scope.LoadItem(win, -1, vform);
    };

    $scope.Save_Click = function ($event, win, grid, vform) {
        $event.preventDefault();

        if (vform()) {
            $rootScope.IsLoading = true;
            $scope.Item.lstField = $scope.gridExpression.dataSource.data();
            //$scope.Item.lstUserMail = $scope.mail_user_gridOptions.dataSource.data();
            //$scope.Item.lstUserTMS = $scope.tms_user_gridOptions.dataSource.data();
            //$scope.Item.lstUserSMS = $scope.sms_user_gridOptions.dataSource.data();
            Common.Services.Call($http, {
                url: Common.Services.url.WFL,
                method: _WFLSetting_WFEvent.URL.WFEventEventSave,
                data: { item: $scope.Item },
                success: function (res) {
                    $rootScope.IsLoading = false;
                    win.close();
                    $scope.WFLSettingWFEvent_gridOptions.dataSource.read();
                    $rootScope.Message({ Msg: 'Đã cập nhật' });
                }
            });
        }

    };

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
                        url: Common.Services.url.WFL,
                        method: _WFLSetting_WFEvent.URL.WFEventEventDelete,
                        data: pars,
                        success: function (res) {
                            $scope.WFLSettingWFEvent_gridOptions.dataSource.read();

                            $rootScope.Message({ Msg: 'Đã xóa', NotifyType: Common.Message.NotifyType.ERROR });
                        }
                    });
                }
            });
        }
    };

    $scope.Edit_Click = function ($event, win, grid, vform) {
        $event.preventDefault();

        var tr = $($event.target).closest('tr');
        var item = grid.dataItem(tr);

        $scope.LoadItem(win, item.ID, vform);
    };

    $scope.Close_Click = function ($event, win) {
        $event.preventDefault();
        win.close();
    };

    $scope.LoadItem = function (win, id, vform) {
        vform({ clear: true });

        Common.Services.Call($http, {
            url: Common.Services.url.WFL,
            method: _WFLSetting_WFEvent.URL.WFEventEventGet,
            data: { id: id },
            success: function (res) {
                if (Common.HasValue(res)) {
                    $timeout(function () {
                        $scope.Item = res;
                        $scope.gridExpressionOptions.dataSource.data(res.lstField);
                       
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
            model: _WFLSetting_WFEvent.Data._gridExpressionModel,
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
                    '<input focus-k-combobox class="cus-datetimepicker clDate" kendo-date-picker k-options="DateDMY" ng-model="dataItem.CompareValue" style="width:100%"/>' +
                    '<input focus-k-combobox class="cus-combobox clSysVar" data-bind="value:CompareValue" value="#=CompareValue#" style="width:100%; display:none"/>',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'IsModified', title: 'Thay đổi', width: '90px',
                template: '<input type="checkbox" #= IsModified ? "checked=checked" : "" # ng-model="dataItem.IsModified" />',
                filterable: false, sortable: false
            },
            { title: ' ', sortable: false, filterable: false, menu: false }
        ],
        dataBound: function () {
            this.items().each(function () {
                var itemRow = $scope.gridExpression.dataItem($(this));
                var lstTableID = _WFLSetting_WFEvent.Data._dataTableID;
                var idx = lstTableID.indexOf(itemRow.TableCode);
                var dataSysVar = $scope.loadDataCompareValue(this, itemRow);
                $(this).find('input.cboOperator').kendoComboBox({
                    autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true, minLength: 3,
                    dataTextField: 'Name', dataValueField: 'Code',
                    dataSource: Common.DataSource.Local({
                        data: _WFLSetting_WFEvent.Data._dataAndOr,
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
                        data: _WFLSetting_WFEvent.Data._dataTables,
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
                        var lstTableID = _WFLSetting_WFEvent.Data._dataTableID;
                        var idx = lstTableID.indexOf(item.TableCode);
                        if (idx >= 0) {
                            var cboField = $($(tr).find('input.cboField')[1]).data("kendoComboBox");
                            cboField.dataSource.data(_WFLSetting_WFEvent.Data._dataTableFields[idx]);
                            item.FieldID = _WFLSetting_WFEvent.Data._dataTableFields[idx][0].ID;
                            item.FieldCode = _WFLSetting_WFEvent.Data._dataTableFields[idx][0].Code;
                            cboField.text(_WFLSetting_WFEvent.Data._dataTableFields[idx][0].Name);
                            cboField.trigger("change");
                        }
                    }
                });

                $(this).find('input.cboField').kendoComboBox({
                    autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true, minLength: 3,
                    dataTextField: 'Name', dataValueField: 'ID',
                    dataSource: Common.DataSource.Local({
                        data: _WFLSetting_WFEvent.Data._dataTableFields[idx],
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
                        var lstTableID = _WFLSetting_WFEvent.Data._dataTableID;
                        var idx = lstTableID.indexOf(item.TableCode);

                        if (!$scope.CheckFieldValue(item.OperatorValue)) {
                            $scope.setCheckValueInput(tr, item);
                            $scope.getCompareValue(tr, item)
                        }
                        else {
                            $(tr).find('input.clFieldChoose').val("");
                            $scope.getFieldValFalse(_WFLSetting_WFEvent.Data._dataTableFields[idx], type, tr);
                        }
                    }
                });

                $(this).find('input.cboOperatorValue').kendoComboBox({
                    autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true, minLength: 3,
                    dataTextField: 'Name', dataValueField: 'Code',
                    dataSource: Common.DataSource.Local({
                        data: _WFLSetting_WFEvent.Data._dataOperators,
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
                        var lstTableID = _WFLSetting_WFEvent.Data._dataTableID;
                        var idx = lstTableID.indexOf(item.TableCode);
                        //kiem tra du lieu dau vao
                        if ($scope.CheckFieldValue(item.OperatorValue)) {
                            $(tr).find('input.clFieldChoose').val("");
                            $scope.getFieldValFalse(_WFLSetting_WFEvent.Data._dataTableFields[idx], type, tr);
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
                        data: _WFLSetting_WFEvent.Data._dataBool,
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
                            $scope.getFieldValFalse(_WFLSetting_WFEvent.Data._dataTableFields[idx], item.Type, this);
                        }
                    }
                }
            });
        }
    };

    $scope.gridExpressionChooseChange = function ($event, grid, haschoose) {
        $scope.ExpressionHasChoose = haschoose;
    };

    // Load danh sách Table
    Common.Services.Call($http, {
        url: Common.Services.url.WFL,
        method: _WFLSetting_WFEvent.URL.WFEventEventTableRead,
        success: function (res) {
            if (Common.HasValue(res)) {
                _WFLSetting_WFEvent.Data._dataTables = [];
                _WFLSetting_WFEvent.Data._dataTableID = [];
                $.each(res, function (i, v) {
                    if (v.TableName != "") {
                        _WFLSetting_WFEvent.Data._dataTables.push({ Code: v.TableName, Name: v.TableDescription });
                        _WFLSetting_WFEvent.Data._dataTableID[i] = v.TableName;
                    }
                });

                if (_WFLSetting_WFEvent.Data._dataTables.length > 0 && _WFLSetting_WFEvent.Data._dataFields.length > 0)
                    $scope.InitTableFieldData();
            }
        }
    });

    // Load danh sách Field
    Common.Services.Call($http, {
        url: Common.Services.url.WFL,
        method: _WFLSetting_WFEvent.URL.WFEventEventFieldRead,
        success: function (res) {
            if (Common.HasValue(res)) {
                _WFLSetting_WFEvent.Data._dataFields = [];
                $.each(res, function (i, v) {
                    _WFLSetting_WFEvent.Data._dataFields.push({ TableCode: v.TableName, Code: v.ColumnName, Name: v.ColumnDescription, Type: v.ColumnType, IsApproved: v.IsApproved, ID: v.ID });
                });
                if (_WFLSetting_WFEvent.Data._dataTables.length > 0 && _WFLSetting_WFEvent.Data._dataFields.length > 0)
                    $scope.InitTableFieldData();
            }
        }
    });

    // Load danh sách status sysvar
    Common.Services.Call($http, {
        url: Common.Services.url.WFL,
        method: _WFLSetting_WFEvent.URL.WFEventWFEventEventSysVar,
        success: function (res) {
            if (Common.HasValue(res)) {
                _WFLSetting_WFEvent.Data._dataStatusOfOrder = res.ListStatusOfOrder;
                _WFLSetting_WFEvent.Data._dataStatusOfPlan = res.ListStatusOfPlan;
                _WFLSetting_WFEvent.Data._dataStatusOfDITOMaster = res.ListStatusOfDITOMaster;
                _WFLSetting_WFEvent.Data._dataStatusOfCOTOMaster = res.ListStatusOfCOTOMaster;
                _WFLSetting_WFEvent.Data._dataKPIReason = res.ListKPIReason;
                _WFLSetting_WFEvent.Data._dataStatusOfAssetTimeSheet = res.ListStatusOfAssetTimeSheet;
                _WFLSetting_WFEvent.Data._dataTypeOfAssetTimeSheet = res.ListTypeOfAssetTimeSheet;
                _WFLSetting_WFEvent.Data._dataDITOGroupProductStatus = res.ListDITOGroupProductStatus;
                _WFLSetting_WFEvent.Data._dataDITOGroupProductStatusPOD = res.ListDITOGroupProductStatusPOD;
                _WFLSetting_WFEvent.Data._dataTypeOfPaymentDITOMaster = res.ListTypeOfPaymentDITOMaster;
                _WFLSetting_WFEvent.Data._dataTroubleCostStatus = res.ListTroubleCostStatus;
                _WFLSetting_WFEvent.Data._dataDITOLocationStatus = res.ListDITOLocationStatus;
                _WFLSetting_WFEvent.Data._dataCOTOLocationStatus = res.ListCOTOLocationStatus;
            }
        }
    });

    $scope.InitBaseData = function () {
        Common.Log("InitBaseData");
        // Clear data
        _WFLSetting_WFEvent.Data._dataAndOr = [];
        _WFLSetting_WFEvent.Data._dataBool = [];
        _WFLSetting_WFEvent.Data._dataOperators = [];

        // Tạo data cho And/Or
        _WFLSetting_WFEvent.Data._dataAndOr.push({ ID: 1, Code: "", Name: "" });
        _WFLSetting_WFEvent.Data._dataAndOr.push({ ID: 2, Code: "And", Name: "And" });
        _WFLSetting_WFEvent.Data._dataAndOr.push({ ID: 3, Code: "Or", Name: "Or" });

        //Tao data cho bool
        _WFLSetting_WFEvent.Data._dataBool.push({ ID: 1, Code: "null", Name: "null" });
        _WFLSetting_WFEvent.Data._dataBool.push({ ID: 2, Code: "true", Name: "true" });
        _WFLSetting_WFEvent.Data._dataBool.push({ ID: 3, Code: "false", Name: "false" });

        // Tạo data cho Operator
        _WFLSetting_WFEvent.Data._dataOperators.push({ ID: 1, Code: "Equal", Name: "=", Description: "Bằng" });
        _WFLSetting_WFEvent.Data._dataOperators.push({ ID: 2, Code: "NotEqual", Name: "<>", Description: "Khác" });
        _WFLSetting_WFEvent.Data._dataOperators.push({ ID: 3, Code: "Great", Name: ">", Description: "Lớn hơn" });
        _WFLSetting_WFEvent.Data._dataOperators.push({ ID: 4, Code: "Less", Name: "<", Description: "Nhỏ hơn" });
        _WFLSetting_WFEvent.Data._dataOperators.push({ ID: 5, Code: "GreaterOrEqual", Name: ">=", Description: "Lớn hơn or bằng" });
        _WFLSetting_WFEvent.Data._dataOperators.push({ ID: 6, Code: "LesserOrEqual", Name: "<=", Description: "Bé hơn or bằng" });
        _WFLSetting_WFEvent.Data._dataOperators.push({ ID: 7, Code: "EqualField", Name: "= [Field]", Description: "Bằng với" });
        _WFLSetting_WFEvent.Data._dataOperators.push({ ID: 8, Code: "NotEqualField", Name: "<> [Field]", Description: "Khác với" });
        _WFLSetting_WFEvent.Data._dataOperators.push({ ID: 9, Code: "GreatField", Name: "> [Field]", Description: "Lớn hơn so với" });
        _WFLSetting_WFEvent.Data._dataOperators.push({ ID: 10, Code: "LessField", Name: "< [Field]", Description: "Nhỏ hơn so với" });
        _WFLSetting_WFEvent.Data._dataOperators.push({ ID: 11, Code: "GreaterOrEqualField", Name: ">= [Field]", Description: "Lớn hơn or bằng so với" });
        _WFLSetting_WFEvent.Data._dataOperators.push({ ID: 12, Code: "LesserOrEqualField", Name: "<= [Field]", Description: "Nhỏ hơn or bằng so với" });
    };

    $scope.InitBaseData();

    $scope.InitTableFieldData = function () {
        Common.Log("InitTableFieldData");
        _WFLSetting_WFEvent.Data._dataTableFields = [];
        var lstTableID = _WFLSetting_WFEvent.Data._dataTableID;
        var lstField = _WFLSetting_WFEvent.Data._dataFields;
        var lstTable = _WFLSetting_WFEvent.Data._dataTables;
        if (lstTable.length > 0 && lstField.length > 0) {
            $.each(lstField, function (i, v) {
                var tableCode = v.TableCode;
                var idx = lstTableID.indexOf(tableCode);
                if (idx >= 0) {
                    if (!Common.HasValue(_WFLSetting_WFEvent.Data._dataTableFields[idx]))
                        _WFLSetting_WFEvent.Data._dataTableFields[idx] = [];
                    _WFLSetting_WFEvent.Data._dataTableFields[idx].push(v);
                }
            });
        }
        //$scope.cboTableOptions.dataSource.data(_WFLSetting_Index.Data._dataTables);
    };

    $scope.Add_Click = function ($event, win, vform) {
        $event.preventDefault();

        $scope.LoadItem(win, -1, vform);
    };

    $scope.Edit_Click = function ($event, win, grid, vform) {
        $event.preventDefault();

        var tr = $($event.target).closest('tr');
        var item = grid.dataItem(tr);

        $scope.LoadItem(win, item.ID, vform);
    };

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
                        url: Common.Services.url.WFL,
                        method: _WFLSetting_WFEvent.URL.WFEventEventDelete,
                        data: pars,
                        success: function (res) {
                            $scope.WFLSettingWFEvent_gridOptions.dataSource.read();

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
            $rootScope.IsLoading = true;
            $scope.Item.lstField = $scope.gridExpression.dataSource.data();
            Common.Services.Call($http, {
                url: Common.Services.url.WFL,
                method: _WFLSetting_WFEvent.URL.WFEventEventSave,
                data: { item: $scope.Item },
                success: function (res) {
                    $rootScope.IsLoading = false;
                    win.close();
                    $scope.WFLSettingWFEvent_gridOptions.dataSource.read();
                    $rootScope.Message({ Msg: 'Đã cập nhật' });
                }
            });
        }

    };

    $scope.Close_Click = function ($event, win) {
        $event.preventDefault();
        win.close();
    };

    $scope.ExpressionAdd_Click = function ($event, grid) {
        $event.preventDefault();
        $timeout(function () {
            var item = { ID: -1, Type: _WFLSetting_WFEvent.Data._dataTableFields[0][0].Type, OperatorCode: _WFLSetting_WFEvent.Data._dataAndOr[0].Code, TableCode: _WFLSetting_WFEvent.Data._dataTables[0].Code, FieldCode: _WFLSetting_WFEvent.Data._dataTableFields[0][0].Code, FieldID: _WFLSetting_WFEvent.Data._dataTableFields[0][0].ID, OperatorValue: _WFLSetting_WFEvent.Data._dataOperators[0].Code, CompareValue: '', IsChoose: false, IsApproved: true, IsModified: false };
            if ($scope.gridExpressionOptions.dataSource.data().length > 0)
                item.OperatorCode = _WFLSetting_WFEvent.Data._dataAndOr[1].Code;

            $scope.gridExpressionOptions.dataSource.insert($scope.gridExpressionOptions.dataSource.data().length, item);
        }, 10);
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

    $scope.Window_Close_Click = function ($event, win) {
        $event.preventDefault();
        win.close();
    }

    // Expression
    $scope.loadDataCompareValue = function (tr, item) {
        var data = []
        if (item.FieldCode == "StatusOfOrderID" || item.FieldCode == "StatusOfPlanID" || item.FieldCode == "StatusOfDITOMasterID" || item.FieldCode == "StatusOfCOTOMasterID" || item.FieldCode == "ReasonID" || item.FieldCode == "StatusOfAssetTimeSheetID" || item.FieldCode == "TypeOfAssetTimeSheetID"
            || item.FieldCode == "DITOGroupProductStatusID" || item.FieldCode == "DITOGroupProductStatusPODID" || item.FieldCode == "TypeOfPaymentDITOMasterID"
            || item.FieldCode == "TroubleCostStatusID" || item.FieldCode == "DITOLocationStatusID" || item.FieldCode == "COTOLocationStatusID") {
            var cbo = $($(tr).find('input.clSysVar')[1]).data("kendoComboBox");
            switch (item.FieldCode) {
                case "StatusOfOrderID":
                    data = _WFLSetting_WFEvent.Data._dataStatusOfOrder;
                    break;
                case "StatusOfPlanID":
                    data = _WFLSetting_WFEvent.Data._dataStatusOfPlan;
                    break;
                case "StatusOfDITOMasterID":
                    data = _WFLSetting_WFEvent.Data._dataStatusOfDITOMaster;
                    break;
                case "StatusOfCOTOMasterID":
                    data = _WFLSetting_WFEvent.Data._dataStatusOfCOTOMaster;
                    break;
                case "ReasonID":
                    data = _WFLSetting_WFEvent.Data._dataKPIReason;
                    break;
                case "StatusOfAssetTimeSheetID":
                    data = _WFLSetting_WFEvent.Data._dataStatusOfAssetTimeSheet;
                    break;
                case "TypeOfAssetTimeSheetID":
                    data = _WFLSetting_WFEvent.Data._dataTypeOfAssetTimeSheet;
                    break;
                case "DITOGroupProductStatusID":
                    data = _WFLSetting_WFEvent.Data._dataDITOGroupProductStatus;
                    break;
                case "DITOGroupProductStatusPODID":
                    data = _WFLSetting_WFEvent.Data._dataDITOGroupProductStatusPOD;
                    break;
                case "TypeOfPaymentDITOMasterID":
                    data = _WFLSetting_WFEvent.Data._dataTypeOfPaymentDITOMaster;
                    break;
                case "TroubleCostStatusID":
                    data = _WFLSetting_WFEvent.Data._dataTroubleCostStatus;
                    break;
                case "DITOLocationStatusID":
                    data = _WFLSetting_WFEvent.Data._dataDITOLocationStatus;
                    break;
                case "COTOLocationStatusID":
                    data = _WFLSetting_WFEvent.Data._dataCOTOLocationStatus;
                    break;
            }
        }
        return data;
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
                    cbo.dataSource.data(_WFLSetting_WFEvent.Data._dataStatusOfOrder);
                    break;
                case "StatusOfPlanID":
                    cbo.dataSource.data(_WFLSetting_WFEvent.Data._dataStatusOfPlan);
                    break;
                case "StatusOfDITOMasterID":
                    cbo.dataSource.data(_WFLSetting_WFEvent.Data._dataStatusOfDITOMaster);
                    break;
                case "StatusOfCOTOMasterID":
                    data = _WFLSetting_WFEvent.Data._dataStatusOfCOTOMaster;
                case "ReasonID":
                    cbo.dataSource.data(_WFLSetting_WFEvent.Data._dataKPIReason);
                    break;
                case "StatusOfAssetTimeSheetID":
                    cbo.dataSource.data(_WFLSetting_WFEvent.Data._dataStatusOfAssetTimeSheet);
                    break;
                case "TypeOfAssetTimeSheetID":
                    cbo.dataSource.data(_WFLSetting_WFEvent.Data._dataTypeOfAssetTimeSheet);
                    break;
                case "DITOGroupProductStatusID":
                    cbo.dataSource.data(_WFLSetting_WFEvent.Data._dataDITOGroupProductStatus);
                    break;
                case "DITOGroupProductStatusPODID":
                    cbo.dataSource.data(_WFLSetting_WFEvent.Data._dataDITOGroupProductStatusPOD);
                    break;
                case "TypeOfPaymentDITOMasterID":
                    cbo.dataSource.data(_WFLSetting_Index.Data._dataTypeOfPaymentDITOMaster);
                    break;
                case "TroubleCostStatusID":
                    cbo.dataSource.data(_WFLSetting_WFEvent.Data._dataTroubleCostStatus);
                    break;
                case "DITOLocationStatusID":
                    cbo.dataSource.data(_WFLSetting_WFEvent.Data._dataDITOLocationStatus);
                    break;
                case "COTOLocationStatusID":
                    cbo.dataSource.data(_WFLSetting_WFEvent.Data._dataCOTOLocationStatus);
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
                case "datetime": $(tr).find('input.clDate').val(""); break;
                case "string": $(tr).find('input.clText').val(""); break;
            }
        }
    };

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

//#region cb
$scope.templateMail_Options =
{

    autoBind: true,
    valuePrimitive: true,
    ignoreCase: true,
    filter: 'contains',
    suggest: true,
    dataTextField: 'Name',
    dataValueField: 'ID',
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
}

$scope.templateTMS_Options =
{

    autoBind: true,
    valuePrimitive: true,
    ignoreCase: true,
    filter: 'contains',
    suggest: true,
    dataTextField: 'Name',
    dataValueField: 'ID',
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
}

$scope.templateSMS_Options =
{

    autoBind: true,
    valuePrimitive: true,
    ignoreCase: true,
    filter: 'contains',
    suggest: true,
    dataTextField: 'Name',
    dataValueField: 'ID',
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
}

Common.Services.Call($http, {
    url: Common.Services.url.WFL,
    method: _WFLSetting_WFEvent.URL.WFEventEventTemplateRead,

    success: function (data) {
        var item = { ID: 0, Name: '' };
        data.unshift(item);
        $scope.templateMail_Options.dataSource.data(data);
        $scope.templateTMS_Options.dataSource.data(data);
        $scope.templateSMS_Options.dataSource.data(data);
    }
});
//#endregion


$scope.ShowSetting = function ($event, grid) {
    $event.preventDefault();

    $rootScope.ShowSetting({
        ListView: views.WFLSetting,
        event: $event,
        grid: grid,
        current: $state.current
    });
};
$scope.HideSetting = function ($event) {
    $event.preventDefault();

    $rootScope.HideSetting();
}

}]);