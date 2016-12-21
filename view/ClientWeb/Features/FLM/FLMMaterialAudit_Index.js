/// <reference path="~/Scripts/jquery-1.9.1.intellisense.js" />
/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />

//#region URL
var _FLMMaterialAudit = {
    URL: {
        Read: 'FLMMaterialAudit_List',
        Delete: 'FLMMaterialAudit_Delete',
        Save: 'FLMMaterialAudit_Save',
        Get: 'FLMMaterialAudit_Get',
        FLMMaterialAuditStatus: 'ALL_FLMMaterialAuditStatus',


    },
}


angular.module('myapp').controller('FLMMaterialAudit_IndexCtrl', ['$rootScope', '$scope', '$http', '$location', '$state', '$timeout', function ($rootScope, $scope, $http, $location, $state, $timeout) {
    if ($rootScope.IsPageComplete != true) return;
    Common.Log('FLMMaterialAudit_IndexCtrl');
    $rootScope.IsLoading = false;

    $scope.Auth = $rootScope.GetAuth();

    $scope.Item = { ID: 0, MaterialAuditStatus: [], DateFrom: new Date().addDays(-5), DateTo: new Date().addDays(0) };

    $scope.SettingHasChoose = false;
    $scope.ParamEdit = { ID: -1 }

    $scope.FLMMaterialAuditgridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.FLM,
            method: _FLMMaterialAudit.URL.Read,
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    Code: { type: 'string' },
                    AuditName: { type: 'string' },
                    MaterialAuditStatus: { type: 'string' },
                    DateFrom: { type: 'date' },
                    DateTo: { type: 'date' },
                }
            }
        }),
        height: '100%', pageable: true, sortable: true, columnMenu: false, resizable: true, reorderable: true, filterable: { mode: 'row' },
        columns: [
            {
                title: ' ', width: '50px',
                template: '<a href="/" ng-click="FLMMaterialAuditEdit_Click($event,dataItem)" class="k-button"><i class="fa fa-pencil"></i></a>' +
                '<a href="/" ng-click="FLMMaterialAuditDestroy_Click($event,dataItem)" class="k-button"><i class="fa fa-trash"></i></a>',
                filterable: false, sortable: false
            },
            {
                field: 'Code', title: 'Mã đợt', width: '150px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'AuditName', title: 'Tên đợt', width: '150px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'MaterialAuditStatus', title: 'Trạng thái', width: '150px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'DateFrom', title: 'Từ ngày', width: '150px', template: '#=Common.Date.FromJsonDDMMYY(DateFrom)#',
                filterable: {
                    cell: {
                        template: function (e) {
                            var element = e.element.parent();
                            element.empty();
                            $("<input class='dtp-filter-from' data-field='DateFrom' style='width:50%; float:left;' />").appendTo(element);
                            $("<input class='dtp-filter-to' data-field='DateFrom' style='width:50%; float:left;' />").appendTo(element);
                        }, showOperators: false
                    }
                },
            },
            {
                field: 'DateTo', title: 'Đến ngày', width: '150px', template: '#=Common.Date.FromJsonDDMMYY(DateTo)#',
                filterable: {
                    cell: {
                        template: function (e) {
                            var element = e.element.parent();
                            element.empty();
                            $("<input class='dtp-filter-from' data-field='DateTo' style='width:50%; float:left;' />").appendTo(element);
                            $("<input class='dtp-filter-to' data-field='DateTo' style='width:50%; float:left;' />").appendTo(element);
                        }, showOperators: false
                    }
                },
            },
        ]
    };
    $scope.FLMMaterialAuditEdit_Click = function ($event, data) {
        $event.preventDefault();
        $scope.ParamEdit.ID = data.ID;
        $state.go('main.FLMMaterialAudit.Collect', $scope.ParamEdit)
    }
    $timeout(function () {
        $('.dtp-filter-from').kendoDatePicker({
            format: Common.Date.Format.DDMMYY,
            change: function (e) {
                try {
                    var element = this.wrapper.parent();
                    var field = e.sender.element.data('field');
                    var dtp_To = $(element).find('.dtp-filter-to[data-field=' + field + ']').data('kendoDatePicker');
                    var f = this.value();
                    var t = dtp_To.value();

                    var f_filter = { field: field, operator: "gte", value: f };
                    var t_filter = { field: field, operator: "lte", value: t };
                    var filters = [];
                    if (Common.HasValue($scope.FLMMaterialAuditgridOptions.dataSource.filter())) {
                        filters = $scope.FLMMaterialAuditgridOptions.dataSource.filter().filters;
                        if (Common.HasValue(f)) {
                            var index = 0;
                            var isNew = true;
                            var field = f_filter.field;
                            var operator = f_filter.operator;
                            for (index = 0; index < filters.length; index++) {
                                if (filters[index].field == field && filters[index].operator == operator) {
                                    isNew = false;
                                    break;
                                }
                            }

                            if (isNew) {
                                filters.push(f_filter);
                            }
                            else {
                                filters[index] = f_filter;
                            }
                        }
                        else {
                            var field = f_filter.field;
                            var operator = f_filter.operator;
                            for (index = 0; index < filters.length; index++) {
                                if (filters[index].field == field && filters[index].operator == operator) {
                                    filters.splice(index, 1);
                                    break;
                                }
                            }
                        }
                        if (Common.HasValue(t)) {
                            var isNew = true;
                            var index = 0;
                            var field = t_filter.field;
                            var operator = t_filter.operator;
                            for (index = 0; index < filters.length; index++) {
                                if (filters[index].field == field && filters[index].operator == operator) {
                                    isNew = false;
                                    break;
                                }
                            }

                            if (isNew) {
                                filters.push(t_filter);
                            }
                            else {
                                filters[index] = t_filter;
                            }
                        }
                        else {
                            var field = t_filter.field;
                            var operator = t_filter.operator;
                            for (index = 0; index < filters.length; index++) {
                                if (filters[index].field == field && filters[index].operator == operator) {
                                    filters.splice(index, 1);
                                    break;
                                }
                            }
                        }
                    }
                    else {
                        if (Common.HasValue(f))
                            filters.push(f_filter);
                        if (Common.HasValue(t))
                            filters.push(t_filter);

                    }
                    $scope.FLMMaterialAuditgridOptions.dataSource.filter(filters);
                }
                catch (e) {
                    $rootScope.Message({ Msg: 'Sai dữ liệu!' });
                }
            }
        })

        $('.dtp-filter-to').kendoDatePicker({
            format: Common.Date.Format.DDMMYY,
            change: function (e) {
                try {
                    var element = this.wrapper.parent();
                    var field = e.sender.element.data('field');
                    var dtp_From = $(element).find('.dtp-filter-from[data-field=' + field + ']').data('kendoDatePicker');
                    var f = dtp_From.value();
                    var t = this.value();

                    var f_filter = { field: field, operator: "gte", value: f };
                    var t_filter = { field: field, operator: "lte", value: t };

                    var filters = [];
                    if (Common.HasValue($scope.FLMMaterialAuditgridOptions.dataSource.filter())) {
                        filters = $scope.FLMMaterialAuditgridOptions.dataSource.filter().filters;
                        if (Common.HasValue(f)) {
                            var index = 0;
                            var isNew = true;
                            var field = f_filter.field;
                            var operator = f_filter.operator;
                            for (index = 0; index < filters.length; index++) {
                                if (filters[index].field == field && filters[index].operator == operator) {
                                    isNew = false;
                                    break;
                                }
                            }

                            if (isNew) {
                                filters.push(f_filter);
                            }
                            else {
                                filters[index] = f_filter;
                            }
                        }
                        else {
                            var field = f_filter.field;
                            var operator = f_filter.operator;
                            for (index = 0; index < filters.length; index++) {
                                if (filters[index].field == field && filters[index].operator == operator) {
                                    filters.splice(index, 1);
                                    break;
                                }
                            }
                        }
                        if (Common.HasValue(t)) {
                            var isNew = true;
                            var index = 0;
                            var field = t_filter.field;
                            var operator = t_filter.operator;
                            for (index = 0; index < filters.length; index++) {
                                if (filters[index].field == field && filters[index].operator == operator) {
                                    isNew = false;
                                    break;
                                }
                            }

                            if (isNew) {
                                filters.push(t_filter);
                            }
                            else {
                                filters[index] = t_filter;
                            }
                        }
                        else {
                            var field = t_filter.field;
                            var operator = t_filter.operator;
                            for (index = 0; index < filters.length; index++) {
                                if (filters[index].field == field && filters[index].operator == operator) {
                                    filters.splice(index, 1);
                                    break;
                                }
                            }
                        }
                    }
                    else {
                        if (Common.HasValue(f))
                            filters.push(f_filter);
                        if (Common.HasValue(t))
                            filters.push(t_filter);
                    }
                    $scope.FLMMaterialAuditgridOptions.dataSource.filter(filters);
                }
                catch (e) {
                    $rootScope.Message({ Msg: 'Sai dữ liệu!' });
                }
            }
        })
    }, 500);

    $scope.cboFLMMaterialAuditStatus_Options = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: "contains", suggest: true,
        dataTextField: 'ValueOfVar', dataValueField: 'ID',
        dataSource: Common.DataSource.Local([], {
            id: 'ID',
            fields: {
                ValueOfVar: { type: 'string' },
                ID: { type: 'number' },
            }
        }),
        change: function (e) {
            $scope.Item.MaterialAuditStatusID = this.value();
            if ($scope.Item.MaterialAuditStatusID.length == 0) {
                $scope.Item.ID = -1;
            }
        }
    }

    Common.ALL.Get($http, {
        url: Common.ALL.URL.SYSVarMaterialAuditStatus,
        success: function (res) {
            $scope.cboFLMMaterialAuditStatus_Options.dataSource.data(res);
        }
    });
    $scope.LoadItem = function (win, id) {

        Common.Services.Call($http, {
            url: Common.Services.url.FLM,
            method: _FLMMaterialAudit.URL.Get,
            data: { 'ID': id },
            success: function (res) {
                $scope.Item = res;
                $scope.Item.MaterialAuditStatus = 509;
                win.center();
                win.open();
            }
        });
    }

    $scope.FLMMaterialAuditDestroy_Click = function ($event, item) {
        $event.preventDefault();
        Common.Log("FLMStandDestroy_Click");
        $rootScope.IsLoading = true;
        if (Common.HasValue(item)) {
            Common.Services.Call($http, {
                url: Common.Services.url.FLM,
                method: _FLMMaterialAudit.URL.Delete,
                data: { 'item': item },
                success: function (res) {
                    $rootScope.Message({ Msg: "Đã xóa!", NotifyType: Common.Message.NotifyType.INFO });
                    $rootScope.IsLoading = false;
                    $scope.FLMMaterialAuditgridOptions.dataSource.read();
                }
            });
        }
    }

    $scope.FLMMaterialAudit_SaveClick = function ($event, win, vform) {
        $event.preventDefault();
        if (vform()) {
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.FLM,
                method: _FLMMaterialAudit.URL.Save,
                data: { item: $scope.Item },
                success: function (res) {
                    debugger
                    win.close();
                    $rootScope.Message({ Msg: "Đã cập nhật!", NotifyType: Common.Message.NotifyType.SUCCESS });
                    $rootScope.IsLoading = false;
                    $scope.FLMMaterialAuditgridOptions.dataSource.read();
                }
            });
        }
    }

    $scope.FLMMaterialAudit_win_CloseClick = function ($event, win) {
        $event.preventDefault();

        win.close();
    };

    $scope.FLMMaterialAudit_AddNew_Click = function ($event, win, vform) {
        $event.preventDefault();
        $scope.IsEdit = false;
        $scope.LoadItem(win, 0);
    }



    $scope.ShowSetting = function ($event, grid) {
        $event.preventDefault();

        $rootScope.ShowSetting({
            ListView: [],
            event: $event,
            grid: grid,
            current: $state.current
        });
    };
}]);