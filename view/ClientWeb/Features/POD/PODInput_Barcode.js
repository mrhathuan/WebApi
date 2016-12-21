/// <reference path="~/Scripts/jquery-1.9.1.intellisense.js" />
/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />

//#region URL
var _PODInputDIDN_Barcode = {
    URL: {
        PODList: 'PODInputDI_DN_Barcode_List',
        PODSave: 'PODInputDI_DN_Barcode_Save',
    },
    Data: {
        Cookie: 'PODInputDIDN_Barcode',
        gridColumns: [
            { field: 'RegNo', width: '100px', title: 'Số xe', filterable: false },
            { field: 'DriverName', width: '200px', title: 'Tài xế', filterable: false },
            { field: 'Kg', width: '150px', title: 'KG (G.Weight)', template: "#=Common.Number.ToNumber3(Kg)#", filterable: false },
            { field: 'InvoiceNote', width: '200px', title: 'Số chứng từ', filterable: false },
            { field: 'Note2', width: '100px', title: 'Số lít', filterable: false },
            { field: 'Note1', width: '100px', title: 'Số kg', filterable: false },
            {
                field: 'IsInvoice', width: 100, title: 'Đã nhận', attributes: { style: "text-align: center; " },
                template: "<input class='chkIsInvoice' disabled='disabled' type='checkbox' ng-model='dataItem.IsInvoice' ></input>", filterable: false,
            },
            { title: ' ', filterable: false, menu: false, sortable: false }
        ],
        gridModel: {
            id: 'ID',
            fields: {
                ID: { type: 'number' },
                Kg: { type: 'number' },
                Note1: { type: 'string' },
                Note2: { type: 'string' },
                Kg: { type: 'number' },
                DriverName: { type: 'string' },
                RegNo: { type: 'string' },
                InvoiceNote: { type: 'string' },
            }
        }
    }
};
//endregion URL

angular.module('myapp').controller('PODInput_BarcodeCtrl', ['$rootScope', '$scope', '$http', '$location', '$state', '$timeout', function ($rootScope, $scope, $http, $location, $state, $timeout) {

    Common.Log('PODInput_BarcodeCtrl');
    $rootScope.IsLoading = false;

    $scope.Item = null;
    $scope.IsShowNote = false;

    $scope.Init_LoadCookie = function () {
        Common.Log('Init_LoadCookie');

        var strCookie = Common.Cookie.Get(_PODInputDIDN_Barcode.Data.Cookie);
        if (Common.HasValue(strCookie) && strCookie != '') {
            try {
                var objCookie = eval('[' + strCookie + ']')[0];
                $scope.IsShowNote = objCookie.IsShowNote;
            } catch (e) { }
        }
        if (!Common.HasValue($scope.IsShowNote)) {
            $scope.IsShowNote = false;
        }
    };

    $scope.Init_LoadCookie();

    $scope.Close_Click = function ($event) {
        $event.preventDefault();

        $state.go("main.PODInput.Index");
    };

    $scope.Error_Click = function ($event) {
        $event.preventDefault();
    };

    $scope.InputNote_Click = function ($event) {
        var objCookie = { IsShowNote: $scope.IsShowNote };
        Common.Cookie.Set(_PODInputDIDN_Barcode.Data.Cookie, JSON.stringify(objCookie));
    };

    $scope.PODInputDIDN_Barcode_gridOptions = {
        dataSource: Common.DataSource.Local({
            data: [],
            model: _PODInputDIDN_Barcode.Data.gridModel,
        }),
        height: '100%', pageable: false, sortable: { mode: 'multiple' }, groupable: false, columnMenu: false, sortable: true, resizable: true, reorderable: true, filterable: false, selectable: 'row', navigatable: true,
        columns: _PODInputDIDN_Barcode.Data.gridColumns,
        change: function (e) {
        },
    };

    $scope.SetError = function (error) {
        $timeout(function () {
            $scope.Item.ErrorString = error;
        }, 10);
    }

    $scope.ClearAll = function () {
        $timeout(function () {
            $(".PODInputDIDN_Barcode_Form").find("#txtBarcode").focus();
            $scope.Item.ErrorString = "";
            $scope.Item.DNCode = "";
            $scope.Item.SOCode = "";
            $scope.Item.Barcode = "";
            $scope.Item.Note1 = "";
            $scope.Item.Note2 = "";
            $scope.Item.InvoiceNote = "";
            $scope.Item.lstGroup = [];
        }, 10);
    }

    $scope.BarcodeEnter_Click = function ($event) {
        Common.Log("BarcodeEnter_Click");
        $scope.Item.ErrorString = "";

        Common.Services.Call($http, {
            url: Common.Services.url.POD,
            method: _PODInputDIDN_Barcode.URL.PODList,
            data: { Barcode: $scope.Item.Barcode },
            success: function (res) {
                $timeout(function () {
                    $scope.Item = res;
                    $scope.PODInputDIDN_Barcode_gridOptions.dataSource.data([]);
                    if (Common.HasValue($scope.Item.lstGroup)) {
                        $scope.PODInputDIDN_Barcode_gridOptions.dataSource.data($scope.Item.lstGroup);
                    }
                    // Focus vào barcode khi có lỗi
                    if (Common.HasValue($scope.Item.ErrorString)) {
                        $(".PODInputDIDN_Barcode_Form").find("#txtBarcode").focus();
                    } else {
                        $(".PODInputDIDN_Barcode_Form").find("#txtInvoiceNote").focus();
                    }
                }, 10);
            },
        });
    };

    $scope.InvoiceEnter_Click = function () {
        Common.Log("InvoiceEnter_Click");
        $scope.SetError("");

        // Nếu nhập số lít, số kg => focus vào số lít
        if ($scope.IsShowNote) {
            $(".PODInputDIDN_Barcode_Form").find("#txtNote2").focus();
        } else {
            if ($scope.Item.lstGroup.length > 1) {
                $scope.SetError("Chọn dòng để nhập");
                $scope.PODInputDIDN_Barcode_grid.select("tr:eq(1)");
                $scope.PODInputDIDN_Barcode_grid.table.focus();
            } else {
                if ($scope.Item.lstGroup.length == 1) {
                    var item = $scope.Item.lstGroup[0];
                    item.InvoiceNote = $scope.Item.InvoiceNote;
                    item.Note1 = $scope.Item.Note1;
                    item.Note2 = $scope.Item.Note2;
                    $scope.SaveData(item);
                }
            }
        }
    };

    $scope.Note2Enter_Click = function ($event) {
        Common.Log("Note2Enter_Click");
        $scope.SetError("");

        if ($scope.Item.Note2 != null && $scope.Item.Note2 != "") {
            if (!isNaN(parseFloat($scope.Item.Note2)))
                $(".PODInputDIDN_Barcode_Form").find("#txtNote1").focus();
            else
                $scope.SetError("Số Lít phải là số");
        } else {
            $(".PODInputDIDN_Barcode_Form").find("#txtNote1").focus();
        }
    };

    $scope.Note1Enter_Click = function ($event) {
        Common.Log("Note1Enter_Click");

        $scope.SetError("");
        var check = false;
        if ($scope.Item.Note1 != null && $scope.Item.Note1 != "") {
            if (!isNaN(parseFloat($scope.Item.Note1))) {
                check = true;
            }
        } else {
            check = true;
        }
        if (check) {
            if ($scope.Item.lstGroup.length > 1) {
                $scope.SetError("Chọn dòng để nhập");
                $scope.PODInputDIDN_Barcode_grid.select("tr:eq(1)");
                $scope.PODInputDIDN_Barcode_grid.table.focus();
            } else {
                if ($scope.Item.lstGroup.length == 1) {
                    var item = $scope.Item.lstGroup[0];
                    item.InvoiceNote = $scope.Item.InvoiceNote;
                    item.Note1 = $scope.Item.Note1;
                    item.Note2 = $scope.Item.Note2;
                    $scope.SaveData(item);
                }
            }
        } else
            $scope.SetError("Số Kg phải là số");
    };

    $scope.SaveData = function (item) {
        Common.Services.Call($http, {
            url: Common.Services.url.POD,
            method: _PODInputDIDN_Barcode.URL.PODSave,
            data: { item: item, IsNote: $scope.IsShowNote },
            success: function (res) {
                Common.Services.Error(res, function (res) {
                    var barcode = $scope.Item.Barcode;
                    $scope.ClearAll();
                    $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                    Common.Services.Call($http, {
                        url: Common.Services.url.POD,
                        method: _PODInputDIDN_Barcode.URL.PODList,
                        data: { Barcode: barcode },
                        success: function (res) {
                            $timeout(function () {
                                $scope.PODInputDIDN_Barcode_gridOptions.dataSource.data([]);
                                if (Common.HasValue(res.lstGroup))
                                    $scope.PODInputDIDN_Barcode_gridOptions.dataSource.data(res.lstGroup);
                            });
                        },
                    });
                });
            },
        });
    };

    $timeout(function () {
        $(".PODInputDIDN_Barcode_Form").find("#txtBarcode").focus(function () {
            this.select();
        });
        $(".PODInputDIDN_Barcode_Form").find("#txtInvoiceNote").focus(function () {
            this.select();
        });
        $(".PODInputDIDN_Barcode_Form").find(".txtNote2").focus(function () {
            this.select();
        });
        $(".PODInputDIDN_Barcode_Form").find(".txtNote1").focus(function () {
            this.select();
        });
        $scope.PODInputDIDN_Barcode_grid.table.on("keydown", function (event) {
            var arrows = [38, 40];
            var keycode = (event.keyCode ? event.keyCode : event.which);
            if (arrows.indexOf(keycode) >= 0) {
                $timeout(function () {
                    $scope.PODInputDIDN_Barcode_grid.select($("#PODInputDIDN_Barcode_grid_active_cell").closest("tr"));
                }, 1);
            } else {
                if (keycode == '13') {
                    var tr = $scope.PODInputDIDN_Barcode_grid.select();
                    if (Common.HasValue(tr)) {
                        var item = $scope.PODInputDIDN_Barcode_grid.dataItem(tr);
                        var data = [];
                        item.Note2 = $scope.Item.Note2;
                        item.Note1 = $scope.Item.Note1;
                        item.InvoiceNote = $scope.Item.InvoiceNote;
                        data.push(item);
                        $scope.SaveData(data);
                    }
                }
            }
        });
    }, 100);

    //Actions
    $scope.ShowSetting = function ($event, grid) {
        $event.preventDefault();

        $rootScope.ShowSetting({
            ListView: views.PODInput,
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