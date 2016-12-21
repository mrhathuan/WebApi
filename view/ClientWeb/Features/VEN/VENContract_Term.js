/// <reference path="~/Scripts/jquery-1.9.1.intellisense.js" />
/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />
var _VENContract_Term = {
    URL: {
        Get: 'VENContract_ContractTerm_Get',
        Save: 'VENContract_ContractTerm_Save',
        Delete: 'VENContract_ContractTerm_Delete',
        Price_List: 'VENContract_ContractTerm_Price_List',

        Get_Material: 'MaterialAll_List',

        Price_Copy: 'VENContract_Price_Copy',
        Price_Delete: 'VENContract_Price_Delete',
        Price_Get: 'VENContract_Price_Get',
        Price_Save: 'VENContract_Price_Save',

        TermKPITime_List: 'VENContract_ContractTerm_KPITime_List',
        TermKPITime_Save: 'VENContract_ContractTerm_KPITime_SaveExpr',
        TermKPITime_NoTInList: 'VENContract_ContractTerm_KPITime_NotInList',
        TermKPITime_SaveNoiInList: 'VENContract_ContractTerm_KPITime_SaveNotInList',
        KPI_Check_Expression: 'VENContract_KPITime_Check_Expression',
        KPI_Check_KPI: 'VENContract_KPITime_Check_Hit',

        TermKPIQuantity_List: 'VENContract_ContractTerm_KPIQuantity_List',
        TermKPIQuantity_Save: 'VENContract_ContractTerm_KPIQuantity_SaveExpr',
        TermKPIQuantity_NoTInList: 'VENContract_ContractTerm_KPIQuantity_NotInList',
        TermKPIQuantity_SaveNoiInList: 'VENContract_ContractTerm_KPIQuantity_SaveNotInList',
        KPIQuantity_Check_Expression: 'VENContract_KPIQuantity_Check_Expression',
        KPIQuantity_Check_KPI: 'VENContract_KPIQuantity_Check_Hit',
        VENContract_KPIQuantity_Get: 'VENContract_KPIQuantity_Get',
    },
    Param: {
        ContractID: -1,
        TermID: -1,
    },
    Data: {
        Material: [],
    }
}

angular.module('myapp').controller('VENContract_TermCtrl', ['$rootScope', '$scope', '$http', '$location', '$state', '$timeout', '$stateParams', function ($rootScope, $scope, $http, $location, $state, $timeout, $stateParams) {
    Common.Log('VENContract_TermCtrl');
    $rootScope.IsLoading = false;
    $scope.dataexpr = null;
    $scope.ItemIsClosed = false;

    $scope.KPIItemCheck = {
        ID: -1,
        ContractTermID: -1,
        TypeOfKPIID: -1,
        DateRequest: new Date(),
        DateDN: new Date(),
        DateFromCome: new Date(),
        DateFromLeave: new Date(),
        DateFromLoadStart: new Date(),
        DateFromLoadEnd: new Date(),
        DateToCome: new Date(),
        DateToLeave: new Date(),
        DateToLoadStart: new Date(),
        DateToLoadEnd: new Date(),
        DateInvoice: new Date(),
        ETARequest: new Date(),
        DateTOMasterETD: new Date(),
        DateTOMasterETA: new Date(),
        DateTOMasterATD: new Date(),
        DateTOMasterATA: new Date(),
        DateOrderETD: new Date(),
        DateOrderETA: new Date(),
        DateOrderETDRequest: new Date(),
        DateOrderETARequest: new Date(),
        DateOrderCutOfTime: new Date(),
        Expression: '',
        ExpressionInput: '',
        Zone: 0,
        LeadTime: 0,
        CompareField: ''
    }

    $scope.KPIQuantityCheck = {
        ID: -1,
        ContractTermID: -1,
        TypeOfKPIID: -1,
        DateOrderRequest: new Date(),
        DateTOMasterETD: new Date(),
        DateTOMasterETA: new Date(),
        DateTOMasterATD: new Date(),
        DateTOMasterATA: new Date(),
        TonOrder: 0,
        CBMOrder: 0,
        QuantityOrder: 0,
        TonReceive: 0,
        CBMReceive: 0,
        QuantityReceive: 0,
        TonReturn: 0,
        CBMReturn: 0,
        QuantiyReturn: 0,
        ExpressionTon: '',
        ExpressionCBM: '',
        ExpressionQuantity: '',
        Zone: 0,
        LeadTime: 0,
        CompareFieldTon: '',
        CompareFieldCBM: '',
        CompareFieldQuantity: '',
    }

    _VENContract_Term.Param = $.extend(true, _VENContract_Term.Param, $state.params);
    $scope.TabIndex = 0;
    $scope.Item = { ID: 0, MaterialID: -1 };

    Common.Services.Call($http, {
        url: Common.Services.url.VEN,
        method: _VENContract_Term.URL.Get,
        data: { id: _VENContract_Term.Param.TermID, contractID: _VENContract_Term.Param.ContractID },
        success: function (res) {
            $scope.ItemIsClosed = res.IsClosed;
            $scope.Item = res;
            if (res.ID > 0)
                $scope.price_gridOptions.dataSource.read();
        }
    });

    $scope.term_tabstripOptions = {
        animation: {
            open: { effects: "fadeIn" }
        },
        select: function (e) {
            $timeout(function () {
                $scope.TabIndex = angular.element(e.item).data('tabindex');
            }, 1);
        }
    };

    $scope.Update_Click = function ($event, vform) {
        $event.preventDefault();
        if (vform()) {
            $rootScope.IsLoading = true;
            $scope.Item.IsClosed = $scope.ItemIsClosed;
            Common.Services.Call($http, {
                url: Common.Services.url.VEN,
                method: _VENContract_Term.URL.Save,
                data: { item: $scope.Item, contractID: _VENContract_Term.Param.ContractID },
                success: function (res) {
                    $rootScope.IsLoading = false;
                    $rootScope.Message({ Msg: "Đã cập nhật!" });
                    $state.go('main.VENContract.Term', { TermID: res, ContractID: _VENContract_Term.Param.ContractID });
                }
            });
        }
    };

    $scope.UpdateDetail_Click = function ($event, vform) {
        $event.preventDefault();
        $scope.Item.IsClosed = $scope.ItemIsClosed;
        $rootScope.IsLoading = true;
        Common.Services.Call($http, {
            url: Common.Services.url.VEN,
            method: _VENContract_Term.URL.Save,
            data: { item: $scope.Item, contractID: _VENContract_Term.Param.ContractID },
            success: function (res) {
                debugger
                $rootScope.IsLoading = false;
                $rootScope.Message({ Msg: "Đã cập nhật!" });
                $state.go('main.VENContract.Term', { TermID: res, ContractID: _VENContract_Term.Param.ContractID });
            }
        });
    };

    $scope.Delete_Click = function ($event) {
        $event.preventDefault();

        $rootScope.Message({
            Type: Common.Message.Type.Confirm,
            Msg: 'Bạn muốn xóa phụ lục?',
            Ok: function () {
                Common.Services.Call($http, {
                    url: Common.Services.url.VEN,
                    method: _VENContract_Term.URL.Delete,
                    data: { ID: $scope.Item.ID },
                    success: function (res) {
                        Common.Services.Error(res, function (res) {
                            $rootScope.Message({ Msg: "Đã xóa!" });
                            $state.go('main.VENVendor.Contract', { ID: _VENContract_Term.Param.ContractID, CustomerID: 0 });
                        })
                    }
                });
            }
        })
    };

    $scope.cboServiceOptions = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true,
        dataTextField: 'ValueOfVar', dataValueField: 'ID', minLength: 3,
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    ValueOfVar: { type: 'string' },
                }
            }
        })
    };

    Common.ALL.Get($http, {
        url: Common.ALL.URL.SYSVarServiceOfOrder,
        success: function (res) {
            $timeout(function () {
                var item = { ID: -1, ValueOfVar: "" };
                var data = [];
                data.push(item);
                angular.forEach(res, function (value, key) {
                    data.push(value);
                });
                $scope.cboServiceOptions.dataSource.data(data);
            }, 1);
        }
    });
    $scope.cboMaterial_Options = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true, dataTextField: 'MaterialName', dataValueField: 'MaterialID',
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'MaterialID',
                fields: {
                    MaterialID: { type: 'number' },
                    MaterialName: { type: 'string' },
                }
            }
        }),
        change: function (e) {
            var val = this.value();
            if ($scope.Item.ID <= 0) {
                if ($scope.Item.MaterialID > 0) {
                    $scope.Item.Note = "Date: ngày giá nhiên liệu thay đổi \nPriceContract: giá nhiên liệu hợp đồng \nPriceCurrent: giá nhiên liệu hiện tại \nPrice: giá vận chuyển chính hợp đồng";
                } else {
                    $scope.Item.Note = "";
                }
            }
        }
    }
    Common.Services.Call($http, {
        url: Common.Services.url.FLM,
        method: _VENContract_Term.URL.Get_Material,
        success: function (res) {
            var item = { MaterialID: -1, MaterialName: '' };
            var data = [];
            data.push(item);
            angular.forEach(res.Data, function (value, key) {
                data.push(value);
            });
            $scope.cboMaterial_Options.dataSource.data(data);
            _VENContract_Term.Data.Material = data;
        }
    });

    //#region price
    $scope.HasPriceChoose = false;
    $scope.price_gridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.VEN,
            method: _VENContract_Term.URL.Price_List,
            readparam: function () {
                return {
                    contractTermID: _VENContract_Term.Param.TermID
                }
            },
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    IsChoose: { type: 'boolean' }
                }
            },
            pageSize: 100
        }),
        height: '99%', groupable: false, pageable: true, sortable: true, columnMenu: false, resizable: true,
        selectable: false, filterable: { mode: 'row' }, reorderable: false,

        columns: [
            {
                title: ' ', width: '35px',
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,price_grid,price_gridChoose_Change)" />',
                headerAttributes: { style: 'text-align: center;' }, attributes: { style: 'text-align: center;' },
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,price_grid,price_gridChoose_Change)" />',
                filterable: false, sortable: false
            },
            {
                title: ' ', width: '110px',
                template: '<a href="/" ng-click="Price_Edit_Click($event,dataItem,price_win,price_win_vform)" class="k-button"><i class="fa fa-pencil"></i></a>' +
                    '<a href="/" ng-click="Price_Delete_Click($event,dataItem)" class="k-button"><i class="fa fa-trash"></i></a>' +
                    '<a href="/" ng-click="Price_Detail_Click($event,dataItem)" class="k-button"><i class="fa fa-file"></i></a>',
                filterable: false, sortable: false
            },
            {
                field: 'Code', title: 'Mã bảng giá', width: 150,
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'Name', title: 'Bảng giá', width: 150,
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'EffectDate', width: 120, title: 'Ngày hiệu lực', template: "#=Common.Date.FromJsonDMY(EffectDate)#",
                filterable: { cell: { template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); }, operator: 'equal', showOperators: false } },
            },
            { title: '', filterable: false, sortable: false },
        ]
    };

    $scope.price_copy_gridOptions = {
        dataSource: Common.DataSource.Local({
            data: []
        }),
        height: '100%', groupable: false, pageable: false, sortable: true, columnMenu: false, filterable: false, resizable: false,
        columns: [
            { field: 'Code', title: 'Mã BG', },
            { field: 'NewCode', title: 'Mã BG mới', template: '<input type="text" class="k-textbox" ng-model="dataItem.NewCode" style="width:100%"/>' },
            { field: 'NewName', title: 'Tên BG mới', template: '<input type="text" class="k-textbox" ng-model="dataItem.NewName" style="width:100%" />' }
        ]
    }

    $scope.price_gridChoose_Change = function ($event, grid, hasChoose) {
        $scope.HasPriceChoose = hasChoose;
    }

    $scope.Price_Add_Click = function ($event, win, vform) {
        $event.preventDefault()
        $scope.PriceLoadItem(0, win, vform)
    }

    $scope.Price_Detail_Click = function ($event, item) {
        $event.preventDefault();
        if (item.TypeOfMode == 1) {
            $scope.getLinkCODetail(item.ID);
        } else {
            $rootScope.IsLoading = true;
            $scope.getLinkDIDetail(item.ID)
        }
    }

    $scope.getLinkDIDetail = function (ID) {
        Common.Services.Call($http, {
            url: Common.Services.url.VEN,
            method: _VENContract_Term.URL.Price_Get,
            data: { priceID: ID },
            success: function (res) {
                if (res.CheckPrice.HasNormal && !res.IsAllRouting) {
                    $state.go("main.VENContract.PriceDINormal", { PriceID: ID, TermID: res.ContractTermID })
                }
                else {
                    if (res.CheckPrice.HasLevel && !res.IsAllRouting) {
                        $state.go("main.VENContract.PriceDILevel", { PriceID: ID, TermID: res.ContractTermID })
                    }
                    else {
                        $state.go('main.VENContract.PriceDI', {
                            PriceID: ID,
                            TermID: res.ContractTermID
                        });
                    }
                }
                $scope.IsLoading = false;
            },
            error: function (res) {
                $rootScope.IsLoading = false;
            }
        })
    }

    $scope.getLinkCODetail = function (ID) {
        Common.Services.Call($http, {
            url: Common.Services.url.VEN,
            method: _VENContract_Term.URL.Price_Get,
            data: { priceID: ID },
            success: function (res) {
                if (res.CheckPrice.HasNormal && !res.IsAllRouting) {
                    $state.go("main.VENContract.PriceCONormal", { PriceID: ID, TermID: res.ContractTermID })
                }
                else {
                    $state.go('main.VENContract.PriceCO', {
                        PriceID: ID,
                        TermID: _VENContract_Term.Param.TermID
                    });
                }
                $scope.IsLoading = false;
            },
            error: function (res) {
                $rootScope.IsLoading = false;
            }
        })
    }

    $scope.Price_Copy_Click = function ($event, grid, win) {
        $event.preventDefault();

        var data = [];;
        Common.Data.Each($.grep(grid.dataSource.data(), function (o) { return o.IsChoose == true }), function (item) {
            data.push({
                ID: item.ID, Code: item.Code, NewCode: item.Code + "_Copy", NewName: item.Name + "_Copy"
            })
        })
        if (data.length > 0) {
            $scope.price_copy_gridOptions.dataSource.data(data);
            $timeout(function () {
                win.center().open();
            }, 1)
        }
    }

    $scope.Price_Edit_Click = function ($event, item, win, vform) {
        $event.preventDefault();
        $scope.PriceLoadItem(item.ID, win, vform)
    }

    $scope.PriceLoadItem = function (id, win, vform) {
        vform({ clear: true });
        $rootScope.IsLoading = true;
        Common.Services.Call($http, {
            url: Common.Services.url.VEN,
            method: _VENContract_Term.URL.Price_Get,
            data: { priceID: id },
            success: function (res) {
                $rootScope.IsLoading = false;
                $scope.ItemPrice = res;
                win.center().open();
            },
            error: function (res) {
                $rootScope.IsLoading = false;
            }
        });

    }

    $scope.Price_Delete_Click = function ($event, item) {
        $event.preventDefault();
        $rootScope.Message({
            Type: Common.Message.Type.Confirm,
            NotifyType: Common.Message.NotifyType.SUCCESS,
            Title: 'Thông báo',
            Msg: 'Bạn muốn xóa bảng giá đã chọn?',
            Close: null,
            Ok: function () {
                $rootScope.IsLoading = true;
                Common.Services.Call($http, {
                    url: Common.Services.url.VEN,
                    method: _VENContract_Term.URL.Price_Delete,
                    data: { ID: item.ID },
                    success: function (res) {
                        Common.Services.Call($http, {
                            url: Common.Services.url.VEN,
                            method: _VENContract_Term.URL.Get,
                            data: { id: _VENContract_Term.Param.TermID, contractID: _VENContract_Term.Param.ContractID },
                            success: function (res) {
                                $scope.Item = res;
                                if (res.ID > 0)
                                    $scope.price_gridOptions.dataSource.read();
                                $rootScope.IsLoading = false;
                                $rootScope.Message({ Msg: "Đã xóa!" });
                            }
                        });
                    }, error: function (res) {
                        $rootScope.IsLoading = false;
                    }
                });
            }
        })

    }

    $scope.Price_Copy_Submit_Click = function ($event, grid, win) {
        $event.preventDefault();

        var data = grid.dataSource.data();
        var flag = true;
        var lstCode = [];
        $.each(data, function (i, v) {
            v.Value = v.NewCode;
            if (v.NewCode == "" || v.NewCode == null) {
                flag = false;
                lstCode.push(v.Code);
            }
        })
        if (!flag) {
            $rootScope.Message({
                Type: Common.Message.Type.Alert,
                Msg: "Nhập mã mới cho các BD " + lstCode.join(", ")
            });
        } else {
            $rootScope.Message({
                Type: Common.Message.Type.Confirm,
                Msg: "Bạn muốn sao chép các BG đã chọn?",
                Ok: function () {
                    $rootScope.IsLoading = true;
                    Common.Services.Call($http, {
                        url: Common.Services.url.VEN,
                        method: _VENContract_Term.URL.Price_Copy,
                        data: { data: data },
                        success: function (res) {
                            $rootScope.IsLoading = false;
                            Common.Services.Error(res, function (res) {
                                $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                                $scope.price_gridOptions.dataSource.read();
                                win.close();
                            })
                        },
                        error: function (res) {
                            $rootScope.IsLoading = false;
                        }
                    })
                }
            });
        }
    }

    $scope.Price_win_Submit_Click = function ($event, win) {
        $event.preventDefault();
        $rootScope.IsLoading = true;
        Common.Services.Call($http, {
            url: Common.Services.url.VEN,
            method: _VENContract_Term.URL.Price_Save,
            data: { contractTermID: _VENContract_Term.Param.TermID, item: $scope.ItemPrice },
            success: function (res) {
                Common.Services.Call($http, {
                    url: Common.Services.url.VEN,
                    method: _VENContract_Term.URL.Get,
                    data: { id: _VENContract_Term.Param.TermID, contractID: _VENContract_Term.Param.ContractID },
                    success: function (res) {
                        $scope.Item = res;
                        if (res.ID > 0)
                            $scope.price_gridOptions.dataSource.read();

                        $rootScope.IsLoading = false;
                        win.close();
                        $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                    }
                });
            },
            error: function (res) {
                $rootScope.IsLoading = false;
            }
        });

    }

    $scope.cboOrderTypeOptions = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true,
        dataTextField: 'ValueOfVar', dataValueField: 'ID', minLength: 3,
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    ValueOfVar: { type: 'string' },
                }
            }
        })
    }


    Common.ALL.Get($http, {
        url: Common.ALL.URL.SYSVarTypeOfOrder,
        success: function (res) {
            if (res.length > 0) {
                $scope.cboOrderTypeOptions.dataSource.data(res);
            }
        }
    });
    $scope.numValue_options = { format: 'n0', spinners: false, culture: 'en-US', min: 0, step: 1, decimals: 0, }
    //#endregion

    //#region KPI Term  
    $scope.kpi_gridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.VEN,
            method: _VENContract_Term.URL.TermKPITime_List,
            readparam: function () { return { contractTermID: _VENContract_Term.Param.TermID } },
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number', editable: false },
                }
            }
        }),
        height: '100%', groupable: false, pageable: false, sortable: true, columnMenu: false, resizable: true,
        selectable: false, filterable: false, reorderable: true,
        columns: [
            {
                title: ' ', width: '50px', filterable: false, sortable: false,
                template: '<a href="/" title="Kiểm tra" ng-click="KPI_Check_Click($event,dataItem,kpi_check_win)" class="k-button"><i class="fa fa-bolt"></i></a>'
            },
            { field: 'Code', title: 'Mã', width: 100 },
            { field: 'TypeName', title: 'Tên KPI', width: 200 },
            { field: 'Expression', title: 'Biểu thức' },
            { field: 'CompareField', title: 'TG so sánh', width: 120 },
            { title: '', filterable: false, sortable: false },
        ]
    }

    $scope.KpiNotInList_gridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.VEN,
            method: _VENContract_Term.URL.TermKPITime_NoTInList,
            readparam: function () { return { contractTermID: _VENContract_Term.Param.TermID } },
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number', editable: false },
                    IsChoose: { type: 'bool', defaultValue: false },
                }
            }
        }),
        height: '100%', groupable: false, pageable: false, sortable: true, columnMenu: false, resizable: true,
        selectable: false, filterable: false, reorderable: true,
        columns: [
             {
                 field: '', title: ' ', width: '30px',
                 headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,KpiNotInList_grid,KpiNotInList_gridChooseChange)" />',
                 headerAttributes: { style: 'text-align: left;' },
                 template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,KpiNotInList_grid,KpiNotInList_gridChooseChange)" />',
                 templateAttributes: { style: 'text-align: left;' },
                 filterable: false, sortable: false
             },
            { field: 'Code', title: 'Mã', width: 100 },
            { field: 'TypeName', title: 'Tên KPI', width: 200 },
            { title: '', filterable: false, sortable: false },
        ]
    }
    $scope.KPITimeHasChoose = false;
    $scope.KpiNotInList_gridChooseChange = function ($event, grid, haschoose) {
        $scope.KPITimeHasChoose = haschoose;
    };

    $scope.KPI_Check_Click = function ($event, item, win) {
        $event.preventDefault();
        $scope.KPIItemCheck.ID = item.ID;
        $scope.KPIItemCheck.ContractTermID = item.ContractTermID;
        $scope.KPIItemCheck.TypeOfKPIID = item.TypeOfKPIID;
        $scope.KPIItemCheck.Expression = item.Expression;
        $scope.KPIItemCheck.CompareField = item.CompareField;

        win.center().open();
    }

    $scope.KPITime_Add_Click = function ($event, win) {
        $event.preventDefault();
        win.center().open();
        $scope.KpiNotInList_grid.dataSource.read();
        $timeout(function () {
            $scope.KpiNotInList_grid.resize();
        }, 100);
    }

    $scope.KPITime_NotInList_Save_Click = function ($event, grid, win) {
        $event.preventDefault();

        var lst = [];
        $.each(grid.dataSource.data(), function (i, v) {
            if (v.IsChoose)
                lst.push(v);
        });
        if (Common.HasValue(lst)) {
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.VEN,
                method: _VENContract_Term.URL.TermKPITime_SaveNoiInList,
                data: { lst: lst, contractTermID: _VENContract_Term.Param.TermID },
                success: function (res) {
                    $scope.kpi_gridOptions.dataSource.read();
                    $rootScope.Message({
                        Msg: 'Đã cập nhật.',
                        NotifyType: Common.Message.NotifyType.SUCCESS
                    });
                    win.close();
                    $rootScope.IsLoading = false;
                }
            })
        }
    }

    $scope.KPI_Check_Expression_Click = function ($event) {
        $event.preventDefault();
        $rootScope.IsLoading = true;
        Common.Services.Call($http, {
            url: Common.Services.url.VEN,
            method: _VENContract_Term.URL.KPI_Check_Expression,
            data: {
                item: $scope.KPIItemCheck,
                lst: $scope.kpi_gridOptions.dataSource.data()
            },
            success: function (res) {
                $rootScope.IsLoading = false;
                Common.Services.Error(res, function (res) {
                    $rootScope.Message({
                        Type: Common.Message.Type.Alert, Msg: Common.Date.FromJsonDMYHMS(res)
                    });
                })
            },
            error: function () {
                $rootScope.IsLoading = false;
                $rootScope.Message({
                    Type: Common.Message.Type.Alert, Msg: "Sai công thức."
                });
            }
        });
    }

    $scope.KPI_Check_KPI_Click = function ($event) {
        $event.preventDefault();
        $rootScope.IsLoading = true;
        var data = [];
        $.each($scope.kpi_gridOptions.dataSource.data(), function (i, v) {
            data.push({ KPICode: v.Code, Expression: v.Expression })
        });
        Common.Services.Call($http, {
            url: Common.Services.url.VEN,
            method: _VENContract_Term.URL.KPI_Check_KPI,
            data: {
                item: $scope.KPIItemCheck,
                lst: data
            },
            success: function (res) {
                $rootScope.IsLoading = false;
                Common.Services.Error(res, function (res) {
                    $rootScope.Message({
                        Type: Common.Message.Type.Alert, Msg: res
                    });
                })
            },
            error: function () {
                $rootScope.IsLoading = false;
                $rootScope.Message({
                    Type: Common.Message.Type.Alert, Msg: "Lỗi!"
                });
            }
        });
    }

    $scope.KPI_Save_Click = function ($event, grid, win) {
        $event.preventDefault();
        $rootScope.IsLoading = true;
        var data = [];
        Common.Services.Call($http, {
            url: Common.Services.url.VEN,
            method: _VENContract_Term.URL.TermKPITime_Save,
            data: { item: $scope.KPIItemCheck, contractTermID: _VENContract_Term.Param.TermID },
            success: function (res) {
                grid.dataSource.read();
                win.close();
                $rootScope.IsLoading = false;
            }
        });


    }

    $scope.KpiTimeDate_gridOptions = {
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                }
            }
        }),
        height: '100%', groupable: false, pageable: false, sortable: true, columnMenu: false, resizable: true,
        selectable: false, filterable: false, reorderable: true,
        columns: [
            { field: 'KPIName', title: 'Tên', width: "200px" },
            { field: 'Expression', title: 'Công thức', template: '<input class="k-textbox" type="text" ng-model="dataItem.Expression">', width: "150px" },
            { title: '', filterable: false, sortable: false },
        ]
    }
    //#endregion

    //#region KPI Quantity  
    $scope.kpiQuantity_gridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.VEN,
            method: _VENContract_Term.URL.TermKPIQuantity_List,
            readparam: function () { return { contractTermID: _VENContract_Term.Param.TermID } },
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number', editable: false },
                }
            }
        }),
        height: '100%', groupable: false, pageable: false, sortable: true, columnMenu: false, resizable: true,
        selectable: false, filterable: false, reorderable: true,
        columns: [
            {
                title: ' ', width: '50px', filterable: false, sortable: false,
                template: '<a href="/" title="Kiểm tra" ng-click="KPIQuantity_Check_Click($event,dataItem,kpiQuantity_check_win)" class="k-button"><i class="fa fa-bolt"></i></a>'
            },
            { field: 'Code', title: 'Mã', width: 100 },
            { field: 'TypeName', title: 'Tên KPI', width: 200 },
            { field: 'ExpressionTon', title: 'Biểu thức tấn', width: 200 },
            { field: 'ExpressionCBM', title: 'Biểu thức khối', width: 200 },
            { field: 'ExpressionQuantity', title: 'Biểu thức số lượng', width: 200 },
            { field: 'CompareFieldTon', title: 'TG so sánh tấn', width: 120 },
            { field: 'CompareFieldCBM', title: 'TG so sánh khối', width: 120 },
            { field: 'CompareFieldQuantity', title: 'TG so sánh số lượng', width: 120 },
            { title: '', filterable: false, sortable: false },
        ]
    }

    $scope.KpiQuantityNotInList_gridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.VEN,
            method: _VENContract_Term.URL.TermKPIQuantity_NoTInList,
            readparam: function () { return { contractTermID: _VENContract_Term.Param.TermID } },
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number', editable: false },
                    IsChoose: { type: 'bool', defaultValue: false },
                }
            }
        }),
        height: '100%', groupable: false, pageable: false, sortable: true, columnMenu: false, resizable: true,
        selectable: false, filterable: false, reorderable: true,
        columns: [
             {
                 field: '', title: ' ', width: '30px',
                 headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,KpiQuantityNotInList_grid,KpiQuantityNotInList_gridChooseChange)" />',
                 headerAttributes: { style: 'text-align: left;' },
                 template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,KpiQuantityNotInList_grid,KpiQuantityNotInList_gridChooseChange)" />',
                 templateAttributes: { style: 'text-align: left;' },
                 filterable: false, sortable: false
             },
            { field: 'Code', title: 'Mã', width: 100 },
            { field: 'TypeName', title: 'Tên KPI', width: 200 },
            { title: '', filterable: false, sortable: false },
        ]
    }

    $scope.KpiQuantityEx_gridOptions = {
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                }
            }
        }),
        height: '100%', groupable: false, pageable: false, sortable: true, columnMenu: false, resizable: true,
        selectable: false, filterable: false, reorderable: true,
        columns: [
            { field: 'Code', title: 'Công thức', width: "200px" },
            {
                field: 'Value', title: 'Giá trị',
                template: function (e) {
                    if (e.isCheckBox) {
                        return '<input class="chkChoose" type="checkbox" ng-model="dataItem.Value">';
                    }
                    else {
                        return '<input class="k-textbox" type="number" ng-model="dataItem.Value">';
                    }
                }, width: "150px"
            },
            { title: '', filterable: false, sortable: false },
        ]
    }

    $scope.KPIQuantityHasChoose = false;
    $scope.KpiQuantityNotInList_gridChooseChange = function ($event, grid, haschoose) {
        $scope.KPIQuantityHasChoose = haschoose;
    };

    $scope.KPIQuantity_Check_Click = function ($event, item, win) {
        $event.preventDefault();
        $scope.KPIQuantityCheck.ID = item.ID;
        $scope.KPIQuantityCheck.ContractTermID = item.ContractTermID;
        $scope.KPIQuantityCheck.TypeOfKPIID = item.TypeOfKPIID;
        $scope.KPIQuantityCheck.ExpressionTon = item.ExpressionTon;
        $scope.KPIQuantityCheck.ExpressionCBM = item.ExpressionCBM;
        $scope.KPIQuantityCheck.ExpressionQuantity = item.ExpressionQuantity;
        $scope.KPIQuantityCheck.CompareFieldTon = item.CompareFieldTon;
        $scope.KPIQuantityCheck.CompareFieldCBM = item.CompareFieldCBM;
        $scope.KPIQuantityCheck.CompareFieldQuantity = item.CompareFieldQuantity;

        Common.Services.Call($http, {
            url: Common.Services.url.VEN,
            method: _VENContract_Term.URL.VENContract_KPIQuantity_Get,
            data: { id: item.ID },
            success: function (res) {
                $scope.dataexpr = res;
                var data = [];
                var array = ['KPITon', 'KPICBM', 'KPIQuantity', 'IsKPI'];
                $.each(res, function (i, v) {
                    $.each(array, function (j, m) {
                        var code = v.TypeOfKPICode + '.' + m;
                        if (m == 'IsKPI') {
                            data.push({ Code: code, Value: false, isCheckBox: true });
                        }
                        else {
                            data.push({ Code: code, Value: 0, isCheckBox: false });
                        }
                    });
                });
                $scope.KpiQuantityEx_gridOptions.dataSource.data(data);
            }
        })

        win.center().open();
    }

    $scope.KPIQuantity_Add_Click = function ($event, win) {
        $event.preventDefault();
        win.center().open();
        $scope.KpiQuantityNotInList_grid.dataSource.read();
        $timeout(function () {
            $scope.KpiQuantityNotInList_grid.resize();
        }, 100);
    }

    $scope.KPIQuantity_NotInList_Save_Click = function ($event, grid, win) {
        $event.preventDefault();

        var lst = [];
        $.each(grid.dataSource.data(), function (i, v) {
            if (v.IsChoose)
                lst.push(v);
        });
        if (Common.HasValue(lst)) {
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.VEN,
                method: _VENContract_Term.URL.TermKPIQuantity_SaveNoiInList,
                data: { lst: lst, contractTermID: _VENContract_Term.Param.TermID },
                success: function (res) {
                    $scope.kpiQuantity_gridOptions.dataSource.read();
                    $rootScope.Message({
                        Msg: 'Đã cập nhật.',
                        NotifyType: Common.Message.NotifyType.SUCCESS
                    });
                    win.close();
                    $rootScope.IsLoading = false;
                }
            })
        }
    }

    $scope.KPIQuantity_Check_Expression_Click = function ($event) {
        $event.preventDefault();
        $rootScope.IsLoading = true;
        var data = [];
        var datagrid = $scope.KpiQuantityEx_gridOptions.dataSource.data();
        if (Common.HasValue(datagrid)) {
            $.each($scope.dataexpr, function (j, m) {
                $.each(datagrid, function (i, v) {
                    if (m.TypeOfKPICode + '.KPITon' == v.Code) {
                        m.KPITon = v.Value
                    }
                    if (m.TypeOfKPICode + '.KPICBM' == v.Code) {
                        m.KPICBM = v.Value
                    }
                    if (m.TypeOfKPICode + '.KPIQuantity' == v.Code) {
                        m.KPIQuantity = v.Value
                    }
                    if (m.TypeOfKPICode + '.IsKPI' == v.Code) {
                        m.IsKPI = v.Value
                    }
                })
            })
        }
        Common.Services.Call($http, {
            url: Common.Services.url.VEN,
            method: _VENContract_Term.URL.KPIQuantity_Check_Expression,
            data: {
                item: $scope.KPIQuantityCheck,
                lst: $scope.dataexpr,
            },
            success: function (res) {
                $rootScope.IsLoading = false;
                Common.Services.Error(res, function (res) {
                    var notice = "Tấn :" + res.KPITon + " Khối :" + res.KPICBM + " Số lựơng :" + res.KPIQuantity;
                    $rootScope.Message({
                        Type: Common.Message.Type.Alert, Msg: notice
                    });
                })
            },
            error: function () {
                $rootScope.IsLoading = false;
                $rootScope.Message({
                    Type: Common.Message.Type.Alert, Msg: "Sai công thức."
                });
            }
        });
    }

    $scope.KPIQuantity_Check_KPI_Click = function ($event) {
        $event.preventDefault();
        $rootScope.IsLoading = true;
        var data = [];
        var datagrid = $scope.KpiQuantityEx_gridOptions.dataSource.data();
        if (Common.HasValue(datagrid)) {
            $.each($scope.dataexpr, function (j, m) {
                $.each(datagrid, function (i, v) {
                    if (m.TypeOfKPICode + '.KPITon' == v.Code) {
                        m.KPITon = v.Value
                    }
                    if (m.TypeOfKPICode + '.KPICBM' == v.Code) {
                        m.KPICBM = v.Value
                    }
                    if (m.TypeOfKPICode + '.KPIQuantity' == v.Code) {
                        m.KPIQuantity = v.Value
                    }
                    if (m.TypeOfKPICode + '.IsKPI' == v.Code) {
                        m.IsKPI = v.Value
                    }
                })
            })
        }
        Common.Services.Call($http, {
            url: Common.Services.url.VEN,
            method: _VENContract_Term.URL.KPIQuantity_Check_KPI,
            data: {
                item: $scope.KPIQuantityCheck,
                lst: $scope.dataexpr,
            },
            success: function (res) {
                $rootScope.IsLoading = false;
                Common.Services.Error(res, function (res) {
                    $rootScope.Message({
                        Type: Common.Message.Type.Alert, Msg: res
                    });
                })
            },
            error: function () {
                $rootScope.IsLoading = false;
                $rootScope.Message({
                    Type: Common.Message.Type.Alert, Msg: "Lỗi!"
                });
            }
        });
    }

    $scope.KPIQuantity_Save_Click = function ($event, grid, win) {
        $event.preventDefault();
        $rootScope.IsLoading = true;
        var data = [];
        Common.Services.Call($http, {
            url: Common.Services.url.VEN,
            method: _VENContract_Term.URL.TermKPIQuantity_Save,
            data: { item: $scope.KPIQuantityCheck, contractTermID: _VENContract_Term.Param.TermID },
            success: function (res) {
                grid.dataSource.read();
                win.close();
                $rootScope.IsLoading = false;
            }
        });


    }
    //#endregion
    $scope.Back_Click = function ($event) {
        $event.preventDefault();

        $state.go('main.VENVendor.Contract', { ID: _VENContract_Term.Param.ContractID, CustomerID: 0 });
    };

    $scope.Window_Close_Click = function ($event, win) {
        $event.preventDefault();

        win.close();
    };
}]);