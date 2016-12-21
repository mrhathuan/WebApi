//ban moi

/// <reference path="~/Scripts/jquery-1.9.1.intellisense.js" />
/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />

//#region Data
var _WFLSetting_Index = {
    URL: {
        EventRead: 'WFLSetting_EventRead',
        EventGet: 'WFLSetting_EventGet',
        EventSave: 'WFLSetting_EventSave',
        EventDelete: 'WFLSetting_EventDelete',
        EventTableRead: 'WFLSetting_EventTableRead',
        EventFieldRead: 'WFLSetting_EventFieldRead',
        EventTemplateRead: 'WFLSetting_EventTemplateRead',
        EventCustomerRead: 'WFLSetting_EventCustomerRead',
        EventUserRead: 'WFLSetting_EventUserRead',
        EventTypeOfActionRead: 'WFLSetting_EventTypeOfActionRead',
        EventStatusOfOrderRead: 'WFLSetting_EventStatusOfOrderRead',
        EventStatusOfPlanRead: 'WFLSetting_EventStatusOfPlanRead',
        EventStatusOfDITOMasterRead: 'WFLSetting_EventStatusOfDITOMasterRead',
        EventKPIReasonRead: 'WFLSetting_EventKPIReasonRead',
        EventStatusOfAssetTimeSheetRead: 'WFLSetting_EventStatusOfAssetTimeSheetRead',
        EventTypeOfAssetTimeSheetRead: 'WFLSetting_EventTypeOfAssetTimeSheetRead',
        EventSysVar: 'WFLSettingEvent_SysVar',
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
//#endregion

angular.module('myapp').controller('WFLSetting_IndexCtrl', ['$rootScope', '$scope', '$http', '$location', '$state', '$timeout', '$stateParams', function ($rootScope, $scope, $http, $location, $state, $timeout, $stateParams) {
    if ($rootScope.IsPageComplete != true) return;
    Common.Log('WFLSetting_IndexCtrl');
    $rootScope.IsLoading = false;
    $scope.Auth = $rootScope.GetAuth();

    $scope.HasChoose = false;
    $scope.ExpressionHasChoose = false;
    $scope.Item = null;

    $scope.HasNumber = false;
    $scope.HasBool = false;
    $scope.HasDate = false;
    $scope.ItemField = [];

    $scope.UserMailHasChoose = false;
    $scope.UserTMSHasChoose = false;
    $scope.UserSMSHasChoose = false;

    $scope.UserAddHasChoose = false;

    $scope.CusHasChoose = false;
    $scope.CusAddHasChoose = false;

    $scope.CurrentWinUser = null;

    $scope.WFLSetting_gridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.WFL,
            method: _WFLSetting_Index.URL.EventRead,
            model: _WFLSetting_Index.Data._gridModel,
        }),
        height: '100%', pageable: true, sortable: true, columnMenu: false, resizable: true, reorderable: true, filterable: { mode: 'row' },
        toolbar: kendo.template($('#WFLSetting_gridToolbar').html()),
        columns: [
            {
                title: ' ', width: '40px',
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,WFLSetting_grid,gridChooseChange)" />',
                headerAttributes: { style: 'text-align: center;' },
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,WFLSetting_grid,gridChooseChange)" />',
                templateAttributes: { style: 'text-align: center;' },
                filterable: false, sortable: false
            },
            {
                title: ' ', width: '50px',
                template: '<a href="/" ng-click="Edit_Click($event,WFLSetting_win,WFLSetting_grid,WFLSetting_win_vform)" class="k-button"><i class="fa fa-pencil"></i></a>',
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

    $scope.gridExpressionOptions = {
        dataSource: Common.DataSource.Local({
            data: [],
            model: _WFLSetting_Index.Data._gridExpressionModel,
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
                var lstTableID = _WFLSetting_Index.Data._dataTableID;
                var idx = lstTableID.indexOf(itemRow.TableCode);
                var dataSysVar = $scope.loadDataCompareValue(this, itemRow);
                $(this).find('input.cboOperator').kendoComboBox({
                    autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true, minLength: 3,
                    dataTextField: 'Name', dataValueField: 'Code',
                    dataSource: Common.DataSource.Local({
                        data: _WFLSetting_Index.Data._dataAndOr,
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
                        data: _WFLSetting_Index.Data._dataTables,
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
                        var lstTableID = _WFLSetting_Index.Data._dataTableID;
                        var idx = lstTableID.indexOf(item.TableCode);
                        if (idx >= 0) {
                            var cboField = $($(tr).find('input.cboField')[1]).data("kendoComboBox");
                            cboField.dataSource.data(_WFLSetting_Index.Data._dataTableFields[idx]);
                            item.FieldID = _WFLSetting_Index.Data._dataTableFields[idx][0].ID;
                            item.FieldCode = _WFLSetting_Index.Data._dataTableFields[idx][0].Code;
                            cboField.text(_WFLSetting_Index.Data._dataTableFields[idx][0].Name);
                            cboField.trigger("change");
                        }
                    }
                });

                $(this).find('input.cboField').kendoComboBox({
                    autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true, minLength: 3,
                    dataTextField: 'Name', dataValueField: 'ID',
                    dataSource: Common.DataSource.Local({
                        data: _WFLSetting_Index.Data._dataTableFields[idx],
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
                        var lstTableID = _WFLSetting_Index.Data._dataTableID;
                        var idx = lstTableID.indexOf(item.TableCode);

                        if (!$scope.CheckFieldValue(item.OperatorValue)) {
                            $scope.setCheckValueInput(tr, item);
                            $scope.getCompareValue(tr, item)
                        }
                        else {
                            $(tr).find('input.clFieldChoose').val("");
                            $scope.getFieldValFalse(_WFLSetting_Index.Data._dataTableFields[idx], type, tr);
                        }
                    }
                });

                $(this).find('input.cboOperatorValue').kendoComboBox({
                    autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true, minLength: 3,
                    dataTextField: 'Name', dataValueField: 'Code',
                    dataSource: Common.DataSource.Local({
                        data: _WFLSetting_Index.Data._dataOperators,
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
                        var lstTableID = _WFLSetting_Index.Data._dataTableID;
                        var idx = lstTableID.indexOf(item.TableCode);
                        //kiem tra du lieu dau vao
                        if ($scope.CheckFieldValue(item.OperatorValue))
                        {
                            $(tr).find('input.clFieldChoose').val("");
                            $scope.getFieldValFalse(_WFLSetting_Index.Data._dataTableFields[idx], type, tr);
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
                        data: _WFLSetting_Index.Data._dataBool,
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
                            $scope.getFieldValFalse(_WFLSetting_Index.Data._dataTableFields[idx], item.Type, this);
                        }
                    }
                }
            });
        }
    };

    $scope.mail_user_gridOptions = {
        dataSource: Common.DataSource.Local({
            data: [],
            model: _WFLSetting_Index.Data._gridUserModel,
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

    $scope.tms_user_gridOptions = {
        dataSource: Common.DataSource.Local({
            data: [],
            model: _WFLSetting_Index.Data._gridUserModel,
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

    $scope.sms_user_gridOptions = {
        dataSource: Common.DataSource.Local({
            data: [],
            model: _WFLSetting_Index.Data._gridUserModel,
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

    $scope.cus_gridOptions = {
        dataSource: Common.DataSource.Local({
            data: [],
            model: _WFLSetting_Index.Data._gridCusModel,
        }),
        height: '99%', pageable: false, sortable: true, columnMenu: false, resizable: true, reorderable: true, filterable: { mode: 'row' },
        toolbar: kendo.template($('#WFLSetting_win_gridCusToolbar').html()),
        columns: [
             {
                 title: ' ', width: '40px',
                 headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,cus_grid,cusgridChooseChange)" />',
                 headerAttributes: { style: 'text-align: center;' },
                 template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,cus_grid,cusgridChooseChange)" />',
                 templateAttributes: { style: 'text-align: center;' },
                 filterable: false, sortable: false
             },
             {
                 field: 'CustomerName', title: 'Tên khách hàng',
                 filterable: { cell: { operator: 'contains', showOperators: false } }
             },
        ],
    };

    $scope.add_user_gridOptions = {
        dataSource: Common.DataSource.Local({
            data: [],
            model: _WFLSetting_Index.Data._gridUserModel,
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

    $scope.add_cus_gridOptions = {
        dataSource: Common.DataSource.Local({
            data: [],
            model: _WFLSetting_Index.Data._gridCusModel,
        }),
        height: '99%', pageable: false, sortable: true, columnMenu: false, resizable: true, reorderable: true, filterable: { mode: 'row' },
        columns: [
            {
                title: ' ', width: '40px',
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,add_cus_grid,cus_add_gridChooseChange)" />',
                headerAttributes: { style: 'text-align: center;' },
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,add_cus_grid,cus_add_gridChooseChange)" />',
                templateAttributes: { style: 'text-align: center;' },
                filterable: false, sortable: false
            },
            {
                field: 'CustomerName', title: 'Tên khách hàng',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
        ],
    };

    $scope.clFieldChoose_win_cboGroupOptions =
    {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true, minLength: 3,
        dataTextField: 'Name', dataValueField: 'Code',
        dataSource: Common.DataSource.Local({
            data: $scope.ItemField,
            model: {
                id: 'Code',
                fields: {
                    Code: { type: 'string' },
                    Name: { type: 'string' },
                }
            }
        }),
    };

    $scope.WFLSetting_win_tabOptions = {
        animation: {
            open: {
                effects: "fadeIn"
            }
        }
    };

    $scope.WFLSetting_win_cboTableOptions = {
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
        })
    };

    $scope.WFLSetting_win_cboFieldOptions = {
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
        })
    };

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

    $scope.WFLSetting_win_cboTypeOfActionOptions = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true, minLength: 3,
        dataTextField: 'TypeName', dataValueField: 'ID',
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    TypeName: { type: 'string' },
                }
            }
        })
    };

    $scope.hor_splitterOptions = {
        orientation: "horizontal",
        panes: [
            { collapsible: true, size: "50%" },
            { collapsible: true, size: "50%" }
        ]
    };

    $scope.gridChooseChange = function ($event, grid, haschoose) {
        $scope.HasChoose = haschoose;
    };

    $scope.gridExpressionChooseChange = function ($event, grid, haschoose) {
        $scope.ExpressionHasChoose = haschoose;
    };

    $scope.mail_user_gridChooseChange = function ($event, grid, haschoose) {
        $scope.UserMailHasChoose = haschoose;
    };

    $scope.tms_user_gridChooseChange = function ($event, grid, haschoose) {
        $scope.UserTMSHasChoose = haschoose;
    };

    $scope.sms_user_gridChooseChange = function ($event, grid, haschoose) {
        $scope.UserSMSHasChoose = haschoose;
    };

    $scope.user_add_gridChooseChange = function ($event, grid, haschoose) {
        $scope.UserAddHasChoose = haschoose;
    };

    $scope.cus_add_gridChooseChange = function ($event, grid, haschoose) {
        $scope.CusAddHasChoose = haschoose;
    };

    // Load danh sách Table
    Common.Services.Call($http, {
        url: Common.Services.url.WFL,
        method: _WFLSetting_Index.URL.EventTableRead,
        success: function (res) {
            if (Common.HasValue(res)) {
                _WFLSetting_Index.Data._dataTables = [];
                _WFLSetting_Index.Data._dataTableID = [];
                $.each(res, function (i, v) {
                    if (v.TableName != "")
                    {
                        _WFLSetting_Index.Data._dataTables.push({ Code: v.TableName, Name: v.TableDescription });
                        _WFLSetting_Index.Data._dataTableID[i] = v.TableName;
                    }
                });

                if (_WFLSetting_Index.Data._dataTables.length > 0 && _WFLSetting_Index.Data._dataFields.length > 0)
                    $scope.InitTableFieldData();
            }
        }
    });

    // Load danh sách Field
    Common.Services.Call($http, {
        url: Common.Services.url.WFL,
        method: _WFLSetting_Index.URL.EventFieldRead,
        success: function (res) {
            if (Common.HasValue(res)) {
                _WFLSetting_Index.Data._dataFields = [];
                $.each(res, function (i, v) {
                    _WFLSetting_Index.Data._dataFields.push({ TableCode: v.TableName, Code: v.ColumnName, Name: v.ColumnDescription, Type: v.ColumnType, IsApproved: v.IsApproved, ID: v.ID });
                });
                if (_WFLSetting_Index.Data._dataTables.length > 0 && _WFLSetting_Index.Data._dataFields.length > 0)
                    $scope.InitTableFieldData();
            }
        }
    });

    // Load danh sách Template
    Common.Services.Call($http, {
        url: Common.Services.url.WFL,
        method: _WFLSetting_Index.URL.EventTemplateRead,
        success: function (res) {
            if (Common.HasValue(res)) {
                _WFLSetting_Index.Data._dataTemplates = res;
                $timeout(function () {
                    $scope.WFLSetting_win_cboTemplateOptions.dataSource.data(_WFLSetting_Index.Data._dataTemplates);
                }, 10);
            }
        }
    });

    // Load danh sách TypeOfAction
    Common.Services.Call($http, {
        url: Common.Services.url.WFL,
        method: _WFLSetting_Index.URL.EventTypeOfActionRead,
        success: function (res) {
            if (Common.HasValue(res)) {
                _WFLSetting_Index.Data._dataTypeOfAction = res;
                $timeout(function () {
                    $scope.WFLSetting_win_cboTypeOfActionOptions.dataSource.data(_WFLSetting_Index.Data._dataTypeOfAction);
                }, 10);
            }
        }
    });

    // Load danh sách Customer
    Common.Services.Call($http, {
        url: Common.Services.url.WFL,
        method: _WFLSetting_Index.URL.EventCustomerRead,
        success: function (res) {
            if (Common.HasValue(res)) {
                _WFLSetting_Index.Data._dataCustomer = res;
            }
        }
    });

    // Load danh sách User
    Common.Services.Call($http, {
        url: Common.Services.url.WFL,
        method: _WFLSetting_Index.URL.EventUserRead,
        success: function (res) {
            if (Common.HasValue(res)) {
                _WFLSetting_Index.Data._dataUser = res;
            }
        }
    });

    // Load danh sách status sysvar
    Common.Services.Call($http, {
        url: Common.Services.url.WFL,
        method: _WFLSetting_Index.URL.EventSysVar,
        success: function (res) {
            if (Common.HasValue(res)) {
                _WFLSetting_Index.Data._dataStatusOfOrder = res.ListStatusOfOrder;
                _WFLSetting_Index.Data._dataStatusOfPlan = res.ListStatusOfPlan;
                _WFLSetting_Index.Data._dataStatusOfDITOMaster = res.ListStatusOfDITOMaster;
                _WFLSetting_Index.Data._dataStatusOfCOTOMaster = res.ListStatusOfCOTOMaster;
                _WFLSetting_Index.Data._dataKPIReason = res.ListKPIReason;
                _WFLSetting_Index.Data._dataStatusOfAssetTimeSheet = res.ListStatusOfAssetTimeSheet;
                _WFLSetting_Index.Data._dataTypeOfAssetTimeSheet = res.ListTypeOfAssetTimeSheet;
                _WFLSetting_Index.Data._dataDITOGroupProductStatus = res.ListDITOGroupProductStatus;
                _WFLSetting_Index.Data._dataDITOGroupProductStatusPOD = res.ListDITOGroupProductStatusPOD;
                _WFLSetting_Index.Data._dataTypeOfPaymentDITOMaster = res.ListTypeOfPaymentDITOMaster;
                _WFLSetting_Index.Data._dataTroubleCostStatus = res.ListTroubleCostStatus;
                _WFLSetting_Index.Data._dataDITOLocationStatus = res.ListDITOLocationStatus;
                _WFLSetting_Index.Data._dataCOTOLocationStatus = res.ListCOTOLocationStatus;
            }
        }
    });

    $scope.InitBaseData = function () {
        Common.Log("InitBaseData");
        // Clear data
        _WFLSetting_Index.Data._dataAndOr = [];
        _WFLSetting_Index.Data._dataBool = [];
        _WFLSetting_Index.Data._dataOperators = [];

        // Tạo data cho And/Or
        _WFLSetting_Index.Data._dataAndOr.push({ ID: 1, Code: "", Name: "" });
        _WFLSetting_Index.Data._dataAndOr.push({ ID: 2, Code: "And", Name: "And" });
        _WFLSetting_Index.Data._dataAndOr.push({ ID: 3, Code: "Or", Name: "Or" });

        //Tao data cho bool
        _WFLSetting_Index.Data._dataBool.push({ ID: 1, Code: "null", Name: "null" });
        _WFLSetting_Index.Data._dataBool.push({ ID: 2, Code: "true", Name: "true" });
        _WFLSetting_Index.Data._dataBool.push({ ID: 3, Code: "false", Name: "false" });

        // Tạo data cho Operator
        _WFLSetting_Index.Data._dataOperators.push({ ID: 1, Code: "Equal", Name: "=", Description: "Bằng" });
        _WFLSetting_Index.Data._dataOperators.push({ ID: 2, Code: "NotEqual", Name: "<>", Description: "Khác" });
        _WFLSetting_Index.Data._dataOperators.push({ ID: 3, Code: "Great", Name: ">", Description: "Lớn hơn" });
        _WFLSetting_Index.Data._dataOperators.push({ ID: 4, Code: "Less", Name: "<", Description: "Nhỏ hơn" });
        _WFLSetting_Index.Data._dataOperators.push({ ID: 5, Code: "GreaterOrEqual", Name: ">=", Description: "Lớn hơn or bằng" });
        _WFLSetting_Index.Data._dataOperators.push({ ID: 6, Code: "LesserOrEqual", Name: "<=", Description: "Bé hơn or bằng" });
        _WFLSetting_Index.Data._dataOperators.push({ ID: 7, Code: "EqualField", Name: "= [Field]", Description: "Bằng với" });
        _WFLSetting_Index.Data._dataOperators.push({ ID: 8, Code: "NotEqualField", Name: "<> [Field]", Description: "Khác với" });
        _WFLSetting_Index.Data._dataOperators.push({ ID: 9, Code: "GreatField", Name: "> [Field]", Description: "Lớn hơn so với" });
        _WFLSetting_Index.Data._dataOperators.push({ ID: 10, Code: "LessField", Name: "< [Field]", Description: "Nhỏ hơn so với" });
        _WFLSetting_Index.Data._dataOperators.push({ ID: 11, Code: "GreaterOrEqualField", Name: ">= [Field]", Description: "Lớn hơn or bằng so với" });
        _WFLSetting_Index.Data._dataOperators.push({ ID: 12, Code: "LesserOrEqualField", Name: "<= [Field]", Description: "Nhỏ hơn or bằng so với" });
    };

    $scope.InitBaseData();

    $scope.InitTableFieldData = function () {
        Common.Log("InitTableFieldData");
        _WFLSetting_Index.Data._dataTableFields = [];
        var lstTableID = _WFLSetting_Index.Data._dataTableID;
        var lstField = _WFLSetting_Index.Data._dataFields;
        var lstTable = _WFLSetting_Index.Data._dataTables;
        if (lstTable.length > 0 && lstField.length > 0) {
            $.each(lstField, function (i, v) {
                var tableCode = v.TableCode;
                var idx = lstTableID.indexOf(tableCode);
                if (idx >= 0) {
                    if (!Common.HasValue(_WFLSetting_Index.Data._dataTableFields[idx]))
                        _WFLSetting_Index.Data._dataTableFields[idx] = [];
                    _WFLSetting_Index.Data._dataTableFields[idx].push(v);
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

    $scope.LoadItem = function (win, id, vform) {
        vform({ clear: true });

        Common.Services.Call($http, {
            url: Common.Services.url.WFL,
            method: _WFLSetting_Index.URL.EventGet,
            data: { id: id },
            success: function (res) {
                if (Common.HasValue(res)) {
                    $timeout(function () {
                        $scope.Item = res;
                        $scope.gridExpressionOptions.dataSource.data(res.lstField);
                        $scope.mail_user_grid.dataSource.data(res.lstUserMail);
                        $scope.tms_user_grid.dataSource.data(res.lstUserTMS);
                        $scope.sms_user_grid.dataSource.data(res.lstUserSMS);
                    }, 1);
                }
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
                        url: Common.Services.url.WFL,
                        method: _WFLSetting_Index.URL.EventDelete,
                        data: pars,
                        success: function (res) {
                            $scope.WFLSetting_gridOptions.dataSource.read();

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
            $scope.Item.lstUserMail = $scope.mail_user_gridOptions.dataSource.data();
            $scope.Item.lstUserTMS = $scope.tms_user_gridOptions.dataSource.data();
            $scope.Item.lstUserSMS = $scope.sms_user_gridOptions.dataSource.data();
            Common.Services.Call($http, {
                url: Common.Services.url.WFL,
                method: _WFLSetting_Index.URL.EventSave,
                data: { item: $scope.Item },
                success: function (res) {
                    $rootScope.IsLoading = false;
                    win.close();
                    $scope.WFLSetting_gridOptions.dataSource.read();
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
            var item = { ID: -1, Type: _WFLSetting_Index.Data._dataTableFields[0][0].Type, OperatorCode: _WFLSetting_Index.Data._dataAndOr[0].Code, TableCode: _WFLSetting_Index.Data._dataTables[0].Code, FieldCode: _WFLSetting_Index.Data._dataTableFields[0][0].Code, FieldID: _WFLSetting_Index.Data._dataTableFields[0][0].ID, OperatorValue: _WFLSetting_Index.Data._dataOperators[0].Code, CompareValue: '', IsChoose: false, IsApproved: true, IsModified: false };
            if ($scope.gridExpressionOptions.dataSource.data().length > 0)
                item.OperatorCode = _WFLSetting_Index.Data._dataAndOr[1].Code;

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

    // User
    $scope.User_Add_Click = function ($event, grid, win, win_user) {
        $event.preventDefault();
        var lstCurrent = grid.dataSource.data();
        var lstNew = [];
        $.each(_WFLSetting_Index.Data._dataUser, function (i, v) {
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

    $scope.User_Delete_Click = function ($event, grid) {
        $event.preventDefault();
        var lstCurrent = grid.dataSource.data();
        var lstNew = [];
        $.each(lstCurrent, function (i, v) {
            if (!v.IsChoose)
                lstNew.push({ UserID: v.UserID, UserName: v.UserName, IsChoose: v.IsChoose, UserMail: v.UserMail, TelNo: v.TelNo });
        });
        $timeout(function () {
            grid.dataSource.data(lstNew);
        }, 10);
    }

    $scope.Add_User_Add_Click = function ($event, grid, win) {
        $event.preventDefault();
        var lstCurrent = grid.dataSource.data();
        var lstNew = [];

        switch($scope.CurrentWinUser)
        {
            case 'Mail': lstNew = $scope.mail_user_gridOptions.dataSource.data(); break;
            case 'TMS': lstNew = $scope.tms_user_gridOptions.dataSource.data(); break;
            case 'SMS': lstNew = $scope.sms_user_gridOptions.dataSource.data(); break;
        }
        
        $.each(lstCurrent, function (i, v) {
            if (v.IsChoose == true) {
                lstNew.push({ UserID: v.UserID, UserName: v.UserName, Email: v.Email,TelNo:v.TelNo, IsChoose: false });
            }
        });

        $timeout(function () {
            switch ($scope.CurrentWinUser) {
                case 'Mail': lstNew = $scope.mail_user_gridOptions.dataSource.data(lstNew); break;
                case 'TMS': lstNew = $scope.tms_user_gridOptions.dataSource.data(lstNew); break;
                case 'SMS': lstNew = $scope.sms_user_gridOptions.dataSource.data(lstNew); break;
            }
            win.close();
        }, 10);
    };
    
    // Cus
    $scope.Cus_Add_Click = function ($event, grid, win) {
        $event.preventDefault();
        var lstCurrent = grid.dataSource.data();
        var lstNew = [];
        $.each(_WFLSetting_Index.Data._dataCustomer, function (i, v) {
            var check = false;
            $.each(lstCurrent, function (j, m) {
                if (v.ID == m.CustomerID) {
                    check = true;
                }
            });
            if (!check) {
                lstNew.push({ CustomerID: v.ID, CustomerName: v.CustomerName, IsChoose: false });
            }
        });
        $scope.add_cus_gridOptions.dataSource.data(lstNew);
        win.center().open();
        $timeout(function () {
            $scope.add_cus_grid.resize();
        }, 10);

    }

    $scope.Cus_Delete_Click = function ($event, grid) {
        $event.preventDefault();
        var lstCurrent = grid.dataSource.data();
        var lstNew = [];
        $.each(lstCurrent, function (i, v) {
            if (!v.IsChoose)
                lstNew.push({ CustomerID: v.CustomerID, CustomerName: v.CustomerName, IsChoose: v.IsChoose });
        });
        $timeout(function () {
            $scope.cus_gridOptions.dataSource.data(lstNew);
        }, 10);
    }

    $scope.Add_Cus_Add_Click = function ($event, grid, win) {
        $event.preventDefault();
        var lstCurrent = grid.dataSource.data();
        var lstNew = $scope.cus_gridOptions.dataSource.data();
        $.each(lstCurrent, function (i, v) {
            if (v.IsChoose == true) {
                v.IsChoose = false;
                lstNew.push({ CustomerID: v.CustomerID, CustomerName: v.CustomerName, IsChoose: v.IsChoose });
            }
        });

        $timeout(function () {
            $scope.cus_gridOptions.dataSource.data(lstCurrent);
            win.close();
        }, 10);
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
                    data = _WFLSetting_Index.Data._dataStatusOfOrder;
                    break;
                case "StatusOfPlanID":
                    data = _WFLSetting_Index.Data._dataStatusOfPlan;
                    break;
                case "StatusOfDITOMasterID":
                    data = _WFLSetting_Index.Data._dataStatusOfDITOMaster;
                    break;
                case "StatusOfCOTOMasterID":
                    data = _WFLSetting_Index.Data._dataStatusOfCOTOMaster;
                    break;
                case "ReasonID":
                    data = _WFLSetting_Index.Data._dataKPIReason;
                    break;
                case "StatusOfAssetTimeSheetID":
                    data = _WFLSetting_Index.Data._dataStatusOfAssetTimeSheet;
                    break;
                case "TypeOfAssetTimeSheetID":
                    data = _WFLSetting_Index.Data._dataTypeOfAssetTimeSheet;
                    break;
                case "DITOGroupProductStatusID":
                    data = _WFLSetting_Index.Data._dataDITOGroupProductStatus;
                    break;
                case "DITOGroupProductStatusPODID":
                    data = _WFLSetting_Index.Data._dataDITOGroupProductStatusPOD;
                    break;
                case "TypeOfPaymentDITOMasterID":
                    data = _WFLSetting_Index.Data._dataTypeOfPaymentDITOMaster;
                    break;
                case "TroubleCostStatusID":
                    data = _WFLSetting_Index.Data._dataTroubleCostStatus;
                    break;
                case "DITOLocationStatusID":
                    data = _WFLSetting_Index.Data._dataDITOLocationStatus;
                    break;
                case "COTOLocationStatusID":
                    data = _WFLSetting_Index.Data._dataCOTOLocationStatus;
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
                    cbo.dataSource.data(_WFLSetting_Index.Data._dataStatusOfOrder);
                    break;
                case "StatusOfPlanID":
                    cbo.dataSource.data(_WFLSetting_Index.Data._dataStatusOfPlan);
                    break;
                case "StatusOfDITOMasterID":
                    cbo.dataSource.data(_WFLSetting_Index.Data._dataStatusOfDITOMaster);
                    break;
                case "StatusOfCOTOMasterID":
                    data = _WFLSetting_Index.Data._dataStatusOfCOTOMaster;
                case "ReasonID":
                    cbo.dataSource.data(_WFLSetting_Index.Data._dataKPIReason);
                    break;
                case "StatusOfAssetTimeSheetID":
                    cbo.dataSource.data(_WFLSetting_Index.Data._dataStatusOfAssetTimeSheet);
                    break;
                case "TypeOfAssetTimeSheetID":
                    cbo.dataSource.data(_WFLSetting_Index.Data._dataTypeOfAssetTimeSheet);
                    break;
                case "DITOGroupProductStatusID":
                    cbo.dataSource.data(_WFLSetting_Index.Data._dataDITOGroupProductStatus);
                    break;
                case "DITOGroupProductStatusPODID":
                    cbo.dataSource.data(_WFLSetting_Index.Data._dataDITOGroupProductStatusPOD);
                    break;
                case "TypeOfPaymentDITOMasterID":
                    cbo.dataSource.data(_WFLSetting_Index.Data._dataTypeOfPaymentDITOMaster);
                    break;
                case "TroubleCostStatusID":
                    cbo.dataSource.data(_WFLSetting_Index.Data._dataTroubleCostStatus);
                    break;
                case "DITOLocationStatusID":
                    cbo.dataSource.data(_WFLSetting_Index.Data._dataDITOLocationStatus);
                    break;
                case "COTOLocationStatusID":
                    cbo.dataSource.data(_WFLSetting_Index.Data._dataCOTOLocationStatus);
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
