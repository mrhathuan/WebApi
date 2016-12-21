/// <reference path="~/Scripts/jquery-1.9.1.intellisense.js" />
/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />

//#region URL
var _OPSAppointment_DIRouteBarcode = {
    URL: {
        SOList: 'OPS_DIAppointment_RouteBarcode_SOList',
        SOSave: 'OPS_DIAppointment_RouteBarcode_SOSave',
        SOCancel: 'OPS_DIAppointment_RouteDN_Delete',
    },
    Data: {
        Cookie: 'OPS_DIAppointment_RouteBarcode',
        gridModel: {
            id: 'DITOGroupProductID',
            fields: {
                DITOGroupProductID: { type: 'number' },
                OrderCode: { type: 'string' },
                SOCode: { type: 'string' },
                DNCode: { type: 'string' },
                DateDN: { type: 'date' },
                Kg: { type: 'number' },
                IsNew: { type: 'bool' },
                Note1: { type: 'string' },
                Note2: { type: 'string' },
            }
        }
    }
};
//endregion URL
angular.module('myapp').controller('OPSAppointment_DIRouteBarcodeCtrl', ['$rootScope', '$scope', '$http', '$location', '$state', '$timeout', function ($rootScope, $scope, $http, $location, $state, $timeout) {
    if ($rootScope.IsPageComplete != true) return;
    Common.Log('OPSAppointment_DIRouteBarcodeCtrl');
    $rootScope.IsLoading = false;

    $scope.Item = null;
    $scope.IsShowNote = false;

    $scope.Init_LoadCookie = function () {
        Common.Log('Init_LoadCookie');

        var strCookie = Common.Cookie.Get(_OPSAppointment_DIRouteBarcode.Data.Cookie);
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

        $state.go('main.OPSAppointment.DIRouteDN');
    };
    
    $scope.Error_Click = function ($event) {
        $event.preventDefault();
    };

    $scope.InputNote_Click = function ($event) {
        var objCookie = { IsShowNote: $scope.IsShowNote };
        Common.Cookie.Set(_OPSAppointment_DIRouteBarcode.Data.Cookie, JSON.stringify(objCookie));
    };

    $scope.OPSAppointment_DIRouteBarcode_win_cboKgOptions = {
        autoBind: true, valuePrimitive: true, ignoreCase: false, filter: 'eq', suggest: true, minLength: 1,
        dataTextField: 'Kg', dataValueField: 'DITOGroupProductID',
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'DITOGroupProductID',
                fields: {
                    DITOGroupProductID: { type: 'number' },
                    Kg: { type: 'number' },
                }
            }
        })
    };

    $scope.OPSAppointment_DIRouteBarcode_gridOptions = {
        dataSource: Common.DataSource.Local({
            data: [],
            model: _OPSAppointment_DIRouteBarcode.Data.gridModel,
        }),
        height: '100%', pageable: false, sortable: { mode: 'multiple' }, groupable: false, columnMenu: false, resizable: true, reorderable: true, filterable: false, selectable: 'row', navigatable: true,
        columns: [
            { field: 'OrderCode', width: '200px', title: 'Loadlist', filterable: false },
            { field: 'SOCode', width: '200px', title: 'Số SO', filterable: false },
            { field: 'DNCode', width: '200px', title: 'Số DN', filterable: false },
            { field: 'Kg', width: '200px', title: 'KG (G.Weight)', template: "#=Common.Number.ToNumber3(Kg)#", filterable: false },
            { field: 'DateDN', title: 'Ngày DN', width: '120px', template: '#=Common.Date.FromJsonDMYHM(DateDN)#', },
            { title: ' ', filterable: false, menu: false, sortable: false }
        ],
        change: function (e) {
        },
    };

    $scope.OPSAppointment_DIRouteBarcode_Cancel_gridOptions = {
        dataSource: Common.DataSource.Local({
            data: [],
            model: _OPSAppointment_DIRouteBarcode.Data.gridModel,
        }),
        height: '100%', pageable: false, sortable: { mode: 'multiple' }, groupable: false, columnMenu: false, resizable: true, reorderable: true, filterable: false, selectable: 'row', navigatable: true,
        columns: [
            { field: 'OrderCode', width: '200px', title: 'Loadlist', filterable: false },
            { field: 'SOCode', width: '200px', title: 'Số SO', filterable: false },
            { field: 'DNCode', width: '150px', title: 'Số DN', filterable: false },
            { field: 'Kg', width: '200px', title: 'KG (G.Weight)', template: "#=Common.Number.ToNumber3(Kg)#", filterable: false },
            { field: 'DateDN', title: 'Ngày DN', width: '120px', template: '#=Common.Date.FromJsonDMYHM(DateDN)#', },
            { title: ' ', filterable: false, menu: false, sortable: false }
        ],
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
            $(".OPSAppointment_DIRouteBarcode_Form").find("#txtBarcode").focus();
            $scope.Item.ErrorString = "";
            $scope.Item.Kg = "";
            $scope.Item.DNCode = "";
            $scope.Item.SOCode = "";
            $scope.Item.Barcode = "";
            $scope.Item.Note1 = "";
            $scope.Item.Note2 = "";
            $scope.Item.ListGroup = [];
        }, 10);
    }

    $scope.BarcodeEnter_Click = function ($event) {
        Common.Log("BarcodeEnter_Click");
        $scope.Item.ErrorString = "";

        Common.Services.Call($http, {
            url: Common.Services.url.OPS,
            method: _OPSAppointment_DIRouteBarcode.URL.SOList,
            data: { Barcode: $scope.Item.Barcode },
            success: function (res) {
                $timeout(function () {
                    $scope.Item = res;
                    $scope.Item.Kg = 0;
                    $scope.OPSAppointment_DIRouteBarcode_win_cboKgOptions.dataSource.data([]);
                    $scope.OPSAppointment_DIRouteBarcode_gridOptions.dataSource.data([]);
                    $scope.OPSAppointment_DIRouteBarcode_Cancel_gridOptions.dataSource.data([]);
                    if (Common.HasValue($scope.Item.ListGroup)) {
                        $scope.OPSAppointment_DIRouteBarcode_win_cboKgOptions.dataSource.data($scope.Item.ListGroup);
                        $scope.OPSAppointment_DIRouteBarcode_gridOptions.dataSource.data($scope.Item.ListGroup);
                    }
                    // Focus vào barcode khi có lỗi
                    if (Common.HasValue($scope.Item.ErrorString)) {
                        // Nếu SO và DN này đã có => hiện popup confirm để cancel các SO chưa nhập DN này
                        if ($scope.Item.IsCancel) {
                            $rootScope.Message({
                                Type: Common.Message.Type.Confirm,
                                Msg: 'Số SO và DN này đã có, bạn có muốn hủy các SO thừa không?',
                                Ok: function () {
                                    $scope.OPSAppointment_DIRouteBarcode_Cancel_win.center().open();
                                    $scope.OPSAppointment_DIRouteBarcode_Cancel_gridOptions.dataSource.data($scope.Item.ListCancel);
                                },
                                Close: function () {
                                    $(".OPSAppointment_DIRouteBarcode_Form").find("#txtBarcode").focus();
                                },
                            });
                        } else
                            $(".OPSAppointment_DIRouteBarcode_Form").find("#txtBarcode").focus();
                    } else {
                        // Gán dữ liệu vào combobox Kg
                        if (Common.HasValue($scope.Item.ListGroup)) {
                            $timeout(function () {
                                $scope.OPSAppointment_DIRouteBarcode_win_cboKg.text($scope.Item.Kg);
                                $scope.Item.Kg = $scope.Item.ListGroup[0].Kg;
                                $(".OPSAppointment_DIRouteBarcode_Form").find(".k-input.cboKg").focus();
                            }, 100);
                        }
                    }
                }, 10);
            },
        });
    };

    $scope.KgEnter_Click = function () {
        Common.Log("KgEnter_Click");
        $scope.SetError("");
        var totalKg = 0;
        $.each($scope.Item.ListGroup, function (i, v) {
            totalKg += v.Kg;
        });

        if (!isNaN(parseFloat($scope.Item.Kg))) {
            if ($scope.Item.Kg <= 0)
                $scope.SetError("Số Kg phải lớn hơn 0");
            else {
                if ($scope.Item.Kg > totalKg && !$scope.IsShowNote) {
                    $rootScope.Message({
                        Type: Common.Message.Type.Confirm,
                        Msg: 'Vượt quá số Kg, bạn muốn lưu số Kg mới?',
                        Ok: function () {
                            var data = [];
                            $scope.Item.ListGroup[0].Kg = $scope.Item.ListGroup[0].Kg + parseFloat($scope.Item.Kg) - totalKg;
                            $.each($scope.Item.ListGroup, function (i, v) {
                                v.DNCode = $scope.Item.DNCode;
                                v.DateDN = $scope.Item.DateDN;
                                data.push(v);
                            });
                            $scope.SaveData(data);
                        },
                        Close: function () {
                            $(".OPSAppointment_DIRouteBarcode_Form").find(".k-input.cboKg").focus();
                        },
                    });
                } else {
                    // Ko nhập Số lít, số kg
                    if (!$scope.IsShowNote) {
                        var bMatch = false;
                        var bMinimun = false;
                        var count = 0;
                        var data = [];
                        var kgMax = 0;
                        // Ktra xem có nhập số Kg bằng vs SO trong loadlist
                        $.each($scope.Item.ListGroup, function (i, v) {
                            if ($scope.Item.Kg == v.Kg) {
                                bMatch = true;
                                v.DNCode = $scope.Item.DNCode;
                                v.DateDN = $scope.Item.DateDN;
                                v.IsNew = false;
                                v.Note2 = $scope.Item.Note2;
                                v.Note1 = $scope.Item.Note1;
                                data.push(v);
                                return;
                            }
                            if ($scope.Item.Kg < v.Kg)
                                count++;
                            if (kgMax < v.Kg)
                                kgMax = v.Kg;
                        });
                        bMinimun = count == $scope.Item.ListGroup.length;
                        // Nhập số Kg ko bằng với SO trong loadlist => Nhỏ hơn => chọn SO muốn nhập
                        if (!bMatch) {
                            // Nếu nhỏ nhất => Chọn dòng để nhập
                            if (bMinimun) {
                                // Nếu chỉ có 1 dòng => tạo thêm 1 dòng mới, update lại dòng cũ
                                if ($scope.Item.ListGroup.length == 1) {
                                    var itemsub = $.extend(true, {}, $scope.Item.ListGroup[0]);
                                    var item = $scope.Item.ListGroup[0];
                                    itemsub.DNCode = "";
                                    itemsub.Kg = item.Kg - $scope.Item.Kg;
                                    itemsub.IsNew = true;
                                    item.Kg = item.Kg - itemsub.Kg;
                                    item.Note2 = $scope.Item.Note2;
                                    item.Note1 = $scope.Item.Note1;
                                    item.DNCode = $scope.Item.DNCode;
                                    item.DateDN = $scope.Item.DateDN;
                                    data.push(item);
                                    data.push(itemsub);
                                    $scope.SaveData(data);
                                } else {
                                    // Nhiều hơn 1 dòng => chọn dòng để nhập
                                    $scope.SetError("Chọn dòng để nhập");
                                    $scope.OPSAppointment_DIRouteBarcode_grid.select("tr:eq(1)");
                                    $scope.OPSAppointment_DIRouteBarcode_grid.table.focus();
                                }
                            } else {
                                // Nếu số Kg nhập vào lớn hơn số Kg lớn nhất => Nhập cho nhiều SO
                                if (kgMax < $scope.Item.Kg) {
                                    var temp = 0;
                                    var data = [];
                                    $.each($scope.Item.ListGroup, function (i, v) {
                                        temp += v.Kg;
                                        v.DNCode = $scope.Item.DNCode;
                                        v.DateDN = $scope.Item.DateDN;
                                        v.IsNew = false;
                                        data.push(v);
                                        if (temp > $scope.Item.Kg) {
                                            var itemsub = $.extend(true, {}, v);
                                            var tempKg = v.Kg;
                                            v.Kg = $scope.Item.Kg - (temp - v.Kg);
                                            itemsub.DNCode = "";
                                            itemsub.Kg = tempKg - v.Kg;
                                            itemsub.IsNew = true;
                                            data.push(itemsub);
                                            return;
                                        } else {
                                            if (temp == $scope.Item.Kg)
                                                return;
                                        }
                                    });
                                    $scope.SaveData(data);
                                } else {
                                    $scope.SetError("Chọn dòng để nhập");
                                    $scope.OPSAppointment_DIRouteBarcode_grid.select("tr:eq(1)");
                                    $scope.OPSAppointment_DIRouteBarcode_grid.table.focus();
                                }
                            }
                        } else {
                            $scope.SaveData(data);
                        }
                    } else {
                        // Nhập số lít, số kg
                        $(".OPSAppointment_DIRouteBarcode_Form").find("#txtNote2").focus();
                    }
                }
            }
        } else
            $scope.SetError("Kg phải là số");
    };

    $scope.DateDNEnter_Click = function () {
        Common.Log("DateDNEnter_Click");

        $(".OPSAppointment_DIRouteBarcode_Form").find(".k-input.cboKg").focus();
    };

    $scope.Note2Enter_Click = function ($event) {
        Common.Log("Note2Enter_Click");
        $scope.SetError("");

        if ($scope.Item.Note2 != null && $scope.Item.Note2 != "") {
            if (!isNaN(parseFloat($scope.Item.Note2)))
                $(".OPSAppointment_DIRouteBarcode_Form").find("#txtNote1").focus();
            else
                $scope.SetError("Số Lít phải là số");
        } else {
            $(".OPSAppointment_DIRouteBarcode_Form").find("#txtNote1").focus();
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
            var totalKg = 0;
            $.each($scope.Item.ListGroup, function (i, v) {
                totalKg += v.Kg;
            });

            if (!isNaN(parseFloat($scope.Item.Kg))) {
                if ($scope.Item.Kg <= 0)
                    $scope.SetError("Số Kg phải lớn hơn 0");
                else {
                    if ($scope.Item.Kg > totalKg) {
                        $rootScope.Message({
                            Type: Common.Message.Type.Confirm,
                            Msg: 'Vượt quá số Kg, bạn muốn lưu số Kg mới?',
                            Ok: function () {
                                var data = [];
                                $scope.Item.ListGroup[0].Kg = $scope.Item.ListGroup[0].Kg + parseFloat($scope.Item.Kg) - totalKg;
                                $.each($scope.Item.ListGroup, function (i, v) {
                                    v.DNCode = $scope.Item.DNCode;
                                    if ($scope.IsShowNote) {
                                        v.Note2 = $scope.Item.Note2;
                                        v.Note1 = $scope.Item.Note1;
                                    }
                                    data.push(v);
                                });
                                $scope.SaveData(data);
                            },
                            Close: function () {
                                $(".OPSAppointment_DIRouteBarcode_Form").find(".k-input.cboKg").focus();
                            },
                        });
                    } else {
                        // Ko nhập Số lít, số kg
                        var bMatch = false;
                        var bMinimun = false;
                        var count = 0;
                        var data = [];
                        var kgMax = 0;
                        // Ktra xem có nhập số Kg bằng vs SO trong loadlist
                        $.each($scope.Item.ListGroup, function (i, v) {
                            if ($scope.Item.Kg == v.Kg) {
                                bMatch = true;
                                v.DNCode = $scope.Item.DNCode;
                                v.IsNew = false;
                                v.Note2 = $scope.Item.Note2;
                                v.Note1 = $scope.Item.Note1;
                                data.push(v);
                                return;
                            }
                            if ($scope.Item.Kg < v.Kg)
                                count++;
                            if (kgMax < v.Kg)
                                kgMax = v.Kg;
                        });
                        bMinimun = count == $scope.Item.ListGroup.length;
                        // Nhập số Kg ko bằng với SO trong loadlist => Nhỏ hơn => chọn SO muốn nhập
                        if (!bMatch) {
                            // Nếu nhỏ nhất => Chọn dòng để nhập
                            if (bMinimun) {
                                // Nếu chỉ có 1 dòng => tạo thêm 1 dòng mới, update lại dòng cũ
                                if ($scope.Item.ListGroup.length == 1) {
                                    var itemsub = $.extend(true, {}, $scope.Item.ListGroup[0]);
                                    var item = $scope.Item.ListGroup[0];
                                    itemsub.DNCode = "";
                                    itemsub.Kg = item.Kg - $scope.Item.Kg;
                                    itemsub.IsNew = true;
                                    item.Kg = item.Kg - itemsub.Kg;
                                    item.DNCode = $scope.Item.DNCode;
                                    item.Note2 = $scope.Item.Note2;
                                    item.Note1 = $scope.Item.Note1;
                                    data.push(item);
                                    data.push(itemsub);
                                    $scope.SaveData(data);
                                } else {
                                    // Nhiều hơn 1 dòng => chọn dòng để nhập
                                    $scope.SetError("Chọn dòng để nhập");
                                    $scope.OPSAppointment_DIRouteBarcode_grid.select("tr:eq(1)");
                                    $scope.OPSAppointment_DIRouteBarcode_grid.table.focus();
                                }
                            } else {
                                // Nếu số Kg nhập vào lớn hơn số Kg lớn nhất => Nhập cho nhiều SO
                                if (kgMax < $scope.Item.Kg) {
                                    var temp = 0;
                                    var data = [];
                                    $.each($scope.Item.ListGroup, function (i, v) {
                                        temp += v.Kg;
                                        v.DNCode = $scope.Item.DNCode;
                                        v.IsNew = false;
                                        data.push(v);
                                        if (temp > $scope.Item.Kg) {
                                            var itemsub = $.extend(true, {}, v);
                                            var tempKg = v.Kg;
                                            v.Kg = $scope.Item.Kg - (temp - v.Kg);
                                            itemsub.DNCode = "";
                                            itemsub.Kg = tempKg - v.Kg;
                                            itemsub.IsNew = true;
                                            data.push(itemsub);
                                            return;
                                        } else {
                                            if (temp == $scope.Item.Kg)
                                                return;
                                        }
                                    });
                                    $scope.SaveData(data);
                                } else {
                                    $scope.SetError("Chọn dòng để nhập");
                                    $scope.OPSAppointment_DIRouteBarcode_grid.select("tr:eq(1)");
                                    $scope.OPSAppointment_DIRouteBarcode_grid.table.focus();
                                }
                            }
                        } else {
                            $scope.SaveData(data);
                        }
                    }
                }
            } else
                $scope.SetError("Kg phải là số");
        } else
            $scope.SetError("Số Kg phải là số");
    };

    $scope.SaveData = function (data) {
        Common.Services.Call($http, {
            url: Common.Services.url.OPS,
            method: _OPSAppointment_DIRouteBarcode.URL.SOSave,
            data: { lst: data, IsNote: $scope.IsShowNote },
            success: function (res) {
                Common.Services.Error(res, function (res) {
                    var barcode = $scope.Item.Barcode;
                    $scope.ClearAll();
                    $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                    Common.Services.Call($http, {
                        url: Common.Services.url.OPS,
                        method: _OPSAppointment_DIRouteBarcode.URL.SOList,
                        data: { Barcode: barcode },
                        success: function (res) {
                            $timeout(function () {
                                $scope.OPSAppointment_DIRouteBarcode_gridOptions.dataSource.data([]);
                                if(Common.HasValue(res.ListGroup))
                                    $scope.OPSAppointment_DIRouteBarcode_gridOptions.dataSource.data(res.ListGroup);
                            });
                        },
                    });
                });
            },
        });
    };

    $scope.CancelData_Click = function ($event, win) {
        $event.preventDefault();
        var lstid = [];
        if (Common.HasValue($scope.Item.ListCancel)) {
            angular.forEach($scope.Item.ListCancel, function (v, i) {
                lstid.push(v.DITOGroupProductID);
            });
            Common.Services.Call($http, {
                url: Common.Services.url.OPS,
                method: _OPSAppointment_DIRouteBarcode.URL.SOCancel,
                data: { lstid: lstid },
                success: function (res) {
                    $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                    win.close();
                    $(".OPSAppointment_DIRouteBarcode_Form").find("#txtBarcode").focus();
                },
            });
        }
    };

    $scope.CancelClose_Click = function ($event, win) {
        $event.preventDefault();
        win.close();
        $(".OPSAppointment_DIRouteBarcode_Form").find("#txtBarcode").focus();
    };

    $timeout(function () {
        $(".OPSAppointment_DIRouteBarcode_Form").find("#txtBarcode").focus(function () {
            this.select();
        });
        $(".OPSAppointment_DIRouteBarcode_Form").find("#txtNote2").focus(function () {
            this.select();
        });
        $(".OPSAppointment_DIRouteBarcode_Form").find("#txtNote1").focus(function () {
            this.select();
        });
        $(".OPSAppointment_DIRouteBarcode_Form").find("#dtDateDN").focus(function () {
            this.select();
        });
        $(".OPSAppointment_DIRouteBarcode_Form").find(".k-input.cboKg").focus(function () {
            this.select();
        });
        $(".OPSAppointment_DIRouteBarcode_Form").find(".k-input.cboKg").keypress(function (event) {
            var keycode = (event.keyCode ? event.keyCode : event.which);
            if (keycode == '13') {
                $scope.KgEnter_Click();
            }
        });
        $scope.OPSAppointment_DIRouteBarcode_grid.table.on("keydown", function (event) {
            var arrows = [38, 40];
            var keycode = (event.keyCode ? event.keyCode : event.which);
            if (arrows.indexOf(keycode) >= 0) {
                $timeout(function () {
                    $scope.OPSAppointment_DIRouteBarcode_grid.select($("#OPSAppointment_DIRouteBarcode_grid_active_cell").closest("tr"));
                }, 1);
            } else {
                if (keycode == '13') {
                    var tr = $scope.OPSAppointment_DIRouteBarcode_grid.select();
                    if (Common.HasValue(tr)) {
                        var item = $scope.OPSAppointment_DIRouteBarcode_grid.dataItem(tr);
                        if (item.Kg < $scope.Item.Kg) {
                            $rootScope.Message({
                                Type: Common.Message.Type.Confirm,
                                Msg: 'Vượt quá số Kg, bạn muốn lưu số Kg mới?',
                                Ok: function () {
                                    var data = [];
                                    item.Kg = $scope.Item.Kg;
                                    item.DNCode = $scope.Item.DNCode;
                                    item.IsNew = false;
                                    item.Note2 = $scope.Item.Note2;
                                    item.Note1 = $scope.Item.Note1;
                                    item.DateDN = $scope.Item.DateDN;
                                    data.push(item);
                                    $scope.SaveData(data);
                                },
                                Close: function () {
                                    $scope.OPSAppointment_DIRouteBarcode_grid.table.focus();
                                },
                            });
                        } else {
                            if (item.Kg > $scope.Item.Kg) {
                                var data = [];
                                item.DNCode = $scope.Item.DNCode;
                                item.DateDN = $scope.Item.DateDN;
                                item.IsNew = false;
                                var itemsub = $.extend(true, {}, item);
                                itemsub.DNCode = "";
                                itemsub.Kg = item.Kg - $scope.Item.Kg;
                                itemsub.IsNew = true;
                                item.Kg = $scope.Item.Kg;
                                item.Note2 = $scope.Item.Note2;
                                item.Note1 = $scope.Item.Note1;
                                data.push(item);
                                data.push(itemsub);
                                $scope.SaveData(data);
                            } else {
                                var data = [];
                                item.DNCode = $scope.Item.DNCode;
                                item.DateDN = $scope.Item.DateDN;
                                item.IsNew = false;
                                item.Kg = $scope.Item.Kg;
                                item.Note2 = $scope.Item.Note2;
                                item.Note1 = $scope.Item.Note1;
                                data.push(item);
                                $scope.SaveData(data);
                            }
                        }
                    }
                }
            }
        });
    }, 100);

}]);