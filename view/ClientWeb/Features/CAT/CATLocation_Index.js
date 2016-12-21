
/// <reference path="~/Scripts/jquery-1.9.1.intellisense.js" />
/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />
/// <reference path="~/Scripts/Default.js" />
/// <reference path="~/Scripts/openMapV2.js" />

//#region URL
var _CATLocation = {
    URL: {
        Read: 'CATLocation_Read',
        Update: 'CATLocation_Update',
        Destroy: 'CATLocation_Destroy',
        Get: 'CATLocation_Get',
        GetDetail: 'Location_GetDetail',
        GetLatLng: 'CATLocation_GetLatLngByAddress',
        UpdateLatLng: 'Location_UpdateLatLng',

        Check_Excel: 'CATLocation_Excel_Check',
        Save_Excel: 'CATLocation_Excel_Save',
        Export_Excel: 'CATLocation_Excel_Export',
    },
    Data: {
        Country: [],
        Province: [],
        District: [],
        Tmp: {}
    },
}

angular.module('myapp').controller('CATLocation_IndexCtrl', ['$rootScope', '$scope', '$http', '$location', '$state', '$timeout', '$compile', 'openMapV2', function ($rootScope, $scope, $http, $location, $state, $timeout, $compile, openMapV2) {
    if ($rootScope.IsPageComplete != true) return;
    Common.Log('CATLocation_IndexCtrl');
    $rootScope.IsLoading = false;

    $scope.Auth = $rootScope.GetAuth();
    $scope.locationMap = { Lat: 0, Lng: 0 };
    $scope.mapEdited = false;

    $scope.LocationHasChoose = false;

    //#region Map
    openMapV2.Init({
        Element: 'map',
        Tooltip_Show: true,
        Tooltip_Element: 'map_tooltip',
        InfoWin_Show: true,
        InfoWin_Element: null,
        ClickMap: function (res) {
            Common.Log("Map click");
            openMapV2.ClearVector("LayerMarker");
            var img = Common.String.Format(openMapV2.NewImage.Location);
            var icon = openMapV2.NewStyle.Icon(img, 1);
            var o = openMapV2._to4326(res);
            $scope.locationMap.Lat = o[1];
            $scope.locationMap.Lng = o[0];
            $scope.mapEdited = true;
            openMapV2.NewMarker(o[1], o[0], $scope.LocationItem.Code, $scope.LocationItem.Location, icon, $scope.LocationItem, "LayerMarker");
            openMapV2.Center(o[1], o[0])
        },
        ClickMarker: null,
        DefinedLayer: [{
            Name: 'LayerMarker',
            zIndex: 100
        }]
    });

    //#endregion
    $scope.CATLocation_gridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.CAT,
            method: _CATLocation.URL.Read,
            pageSize: 20,
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    Code: { type: 'string' },
                    Location: { type: 'string' },
                    Address: { type: 'string' },
                    Lat: { type: 'number' },
                    Lng: { type: 'number' },
                    CountryName: { type: 'string' },
                    IsChoose: { type: 'boolean'},
                }
            }
        }),
        height: '100%', pageable: true, sortable: true, columnMenu: false, resizable: true, reorderable: true, filterable: { mode: 'row' },
        toolbar: kendo.template($('#CATLocation_grid_toolbar').html()),
        columns: [
            {
                title: ' ', width: '40px',
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,CATLocation_grid,CATLocation_gridChange)" />',
                headerAttributes: { style: 'text-align: center;' },
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,CATLocation_grid,CATLocation_gridChange)" />',
                attributes: { style: 'text-align: center;' },
                filterable: false, sortable: false
            },
            {
                title: ' ', width: '155px',
                template: '<a href="/" ng-click="CATLocationEdit_Click($event,CATLocationEdit_win,CATLocation_grid)" class="k-button"><i class="fa fa-pencil"></i></a>' +
                    '<a href="/" ng-click="CATLocationMap_Click($event,CATLocationMap_win,dataItem)" class="k-button"><i class="fa fa-map-marker"></i></a>' +
                    '<a href="/" ng-click="CATLocationDetail_Click($event,dataItem, location_detail_win)" class="k-button"><i class="fa fa-file"></i></a>' +
                    '<a href="/" ng-click="CATLocationDestroy_Click($event,dataItem)" class="k-button"><i class="fa fa-trash"></i></a>',
                filterable: false, sortable: false
            },
            {
                field: 'Code', title: '{{RS.CATLocation.Code}}', width: 100,
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'Location', title: '{{RS.CATLocation.Location}}', width: 200,
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'Address', title: '{{RS.CATLocation.Address}}', width: 250,
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'EconomicZone', title: '{{RS.CATLocation.EconomicZone}}', width: 150,
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'DistrictName', title: '{{RS.CATDistrict.DistrictName}}', width: 150,
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'ProvinceName', title: '{{RS.CATProvince.ProvinceName}}', width: 150,
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'Note', title: '{{RS.CATLocation.Note}}', width: 150,
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'Note1', title: '{{RS.CATLocation.Note1}}', width: 150,
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'Lat', title: '{{RS.CATLocation.Lat}}', width: 150,
                template: "#=Lat==null?\"\":Lat#",
                filterable: {
                    cell: {
                        template: function (container) {
                            container.element.kendoNumericTextBox({
                                format: '#,##0.00000', spinners: false, culture: 'en-US', min: 0, step: 1, decimals: 5,
                            })
                        },
                        operator: "gte",
                        showOperators: false
                    }
                }
            },
            {
                field: 'Lng', title: '{{RS.CATLocation.Lng}}', width: 150,
                template: "#=Lng==null?\"\":Lng#",
                filterable: {
                    cell: {
                        template: function (container) {
                            container.element.kendoNumericTextBox({
                                format: '#,##0.00000', spinners: false, culture: 'en-US', min: 0, step: 1, decimals: 5,
                            })
                        },
                        operator: "gte",
                        showOperators: false
                    }
                }
            },
            {
                field: 'CreatedBy', title: '{{RS.CATLocation.CreateBy}}', width: 150,
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'CreatedDate', title: '{{RS.CATLocation.CreateDate}}', width: 150,
                template: '#=CreatedDate==null?"":Common.Date.FromJsonDMYHM(CreatedDate)#',
                filterable: { cell: { operator: 'gte', showOperators: false } }
            },
            {
                field: 'GroupOfLocationName', title: 'Loại địa điểm', width: 150,
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'LoadTimeCO', title: 'LoadTimeCO', width: 150,
                filterable: { cell: { operator: 'equal', showOperators: false } }
            },
            {
                field: 'UnLoadTimeCO', title: 'UnLoadTimeCO', width: 150,
                filterable: { cell: { operator: 'equal', showOperators: false } }
            },
            {
                field: 'LoadTimeDI', title: 'LoadTimeDI', width: 150,
                filterable: { cell: { operator: 'equal', showOperators: false } }
            },
            {
                field: 'UnLoadTimeDI', title: 'UnLoadTimeDI', width: 150,
                filterable: { cell: { operator: 'equal', showOperators: false } }
            },
        ]
    };

    $scope.CATLocationMap_Click = function ($event, win, data) {
        $event.preventDefault();
        $rootScope.IsLoading = true;
        Common.Services.Call($http, {
            url: Common.Services.url.CAT,
            method: _CATLocation.URL.Get,
            data: { 'ID': data.id },
            success: function (res) {
                $scope.LocationItem = res;
                $scope.mapEdited = false;
                $rootScope.IsLoading = false;
                win.open().center();
                $timeout(function () {
                    openMapV2.Resize();
                    openMapV2.ClearVector("LayerMarker");
                    if (Common.HasValue(res) && res.Lat > 0 && res.Lng > 0) {
                        var img = Common.String.Format(openMapV2.NewImage.Location);
                        var icon = openMapV2.NewStyle.Icon(img, 1);
                        openMapV2.NewMarker(res.Lat, res.Lng, res.Code, res.Location, icon, res, "LayerMarker");
                        openMapV2.Center(res.Lat, res.Lng)
                    }
                }, 200)
            }
        });
    }

    $scope.CATLocation_gridChange = function ($event, grid, haschoose) {
        $scope.LocationHasChoose = haschoose;
    };

    $scope.CATLocationMapSave_Click = function ($event, win) {
        $event.preventDefault();
        if ($scope.mapEdited) {
            $rootScope.Message({
                Type: Common.Message.Type.Confirm,
                NotifyType: Common.Message.NotifyType.SUCCESS,
                Title: 'Thông báo',
                Msg: 'Bạn muốn lưu địa điểm đã chọn?',
                Close: null,
                Ok: function () {
                    $rootScope.IsLoading = true;
                    $scope.LocationItem.Lat = $scope.locationMap.Lat;
                    $scope.LocationItem.Lng = $scope.locationMap.Lng;
                    Common.Services.Call($http, {
                        url: Common.Services.url.CAT,
                        method: _CATLocation.URL.Update,
                        data: { item: $scope.LocationItem },
                        success: function (res) {
                            win.close();
                            $scope.CATLocation_gridOptions.dataSource.read();
                            $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                            $rootScope.IsLoading = false;

                            Common.Services.Call($http, {
                                url: Common.Services.url.CAT,
                                method: _CATRouting.URL.Refresh_Address,
                                data: {},
                                success: function (res) {
                                }
                            });
                        }
                    });
                }
            })
        }
    }

    $scope.CATLocationEdit_Click = function ($event, win, grid) {
        $event.preventDefault();

        var tr = $($event.target).closest('tr');
        var id = 0;
        var item = grid.dataItem(tr);
        if (Common.HasValue(item)) id = item.ID;
        $scope.LoadItem(win, id);
    }

    $scope.GetLoc = function (i, data, now) {
        Common.Log("GetLoc " + i);
        var obj1 = data[i];
        var j = 0;
        for (j = i + 1; j < data.length; j++) {
            var obj2 = data[j];
            openMap.Distance(obj1, obj2, function (p1, p2, o, fi, fj) {
                _CATLocation.Data.Tmp.Count++;
                //Common.Log(_CATLocation.Data.Tmp.Count);
                Common.Log(fj + "___" + _CATLocation.Data.Tmp.Count + ":   " + p1.ID + "-" + p2.ID + " D:" + o.D + 'km' + ", T:" + o.T * 60 + 's');
                //_CATLocation.Data.Tmp[p1.ID + "-" + p2.ID] = o;   
                if (fi == data.length - 2) {
                    if (fj == data.length - 1) {
                        Common.Log(new Date());
                        Common.Log((new Date().getTime() - now.getTime()) / 1000 + "s");
                    }
                }
                else {
                    if (fj == data.length - 1) {
                        $scope.GetLoc(fi + 1, data, now);
                    }
                }
            }, i, j);
        }
    }

    $scope.LoadItem = function (win, id) {
        Common.Services.Call($http, {
            url: Common.Services.url.CAT,
            method: _CATLocation.URL.Get,
            data: { 'ID': id },
            success: function (res) {
                $scope.LocationItem = res;
                $scope.LoadRegionData($scope.LocationItem);
                win.center();
                win.open();
            }
        });
    }

    $scope.CATLocationDestroy_Click = function ($event, item) {
        $event.preventDefault();
        $rootScope.IsLoading = true;
        if (Common.HasValue(item)) {
            if (confirm('Bạn muốn xóa các dữ liệu đã chọn ?')) {
                Common.Services.Call($http, {
                    url: Common.Services.url.CAT,
                    method: _CATLocation.URL.Destroy,
                    data: { 'item': item },
                    success: function (res) {
                        $scope.CATLocation_gridOptions.dataSource.read();
                        $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS })
                        $rootScope.IsLoading = false;
                    }
                });
            }
        }
    }

    $scope.CATLocationSave_Click = function ($event, win, vform) {
        $event.preventDefault();
        if (vform()) {
            if (Common.HasValue($scope.LocationItem.CountryID) && $scope.LocationItem.CountryID > 0) {
                if (Common.HasValue($scope.LocationItem.ProvinceID) && $scope.LocationItem.ProvinceID > 0) {
                    if (Common.HasValue($scope.LocationItem.DistrictID) && $scope.LocationItem.DistrictID > 0) {
                        Common.Services.Call($http, {
                            url: Common.Services.url.CAT,
                            method: _CATLocation.URL.Update,
                            data: { item: $scope.LocationItem },
                            success: function (res) {
                                win.close();
                                $scope.CATLocation_gridOptions.dataSource.read();
                                $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS })
                            }
                        });
                    } else $rootScope.Message({ Msg: 'Quận huyện không chính xác', Type: Common.Message.Type.Alert, NotifyType: Common.Message.NotifyType.ERROR });
                } else $rootScope.Message({ Msg: 'Tỉnh thành không chính xác', Type: Common.Message.Type.Alert, NotifyType: Common.Message.NotifyType.ERROR });
            }
            else $rootScope.Message({ Msg: 'Quốc gia không chính xác', Type: Common.Message.Type.Alert, NotifyType: Common.Message.NotifyType.ERROR });
        }
    }

    $scope.Window_Close_Click = function ($event, win) {
        $event.preventDefault();

        win.close();
    };

    $scope.CATLocation_Excel_Click = function ($event, win) {
        $event.preventDefault()
        $rootScope.UploadExcel({
            Path: Common.FolderUpload.Import,
            columns: [
                { field: 'Code', width: 100, title: '{{RS.CATLocation.Code}}', filterable: { cell: { showOperators: false, operator: "contains" } } },
                { field: 'Location', width: 150, title: '{{RS.CATLocation.Location}}', filterable: { cell: { showOperators: false, operator: "contains" } } },
                { field: 'Address', width: 200, title: '{{RS.CATLocation.Address}}', filterable: { cell: { showOperators: false, operator: "contains" } } }
            ],
            Download: function () {
                $rootScope.IsLoading = true;
                Common.Services.Call($http, {
                    url: Common.Services.url.CAT,
                    method: _CATLocation.URL.Export_Excel,
                    data: {},
                    success: function (res) {
                        $rootScope.DownloadFile(res);
                        $rootScope.IsLoading = false;
                    },
                    error: function (res) {
                        $rootScope.IsLoading = false;
                    }
                })
            },
            Upload: function (e, callback) {
                $rootScope.IsLoading = true;
                Common.Services.Call($http, {
                    url: Common.Services.url.CAT,
                    method: _CATLocation.URL.Check_Excel,
                    data: { item: e },
                    success: function (data) {
                        callback(data);
                        $rootScope.IsLoading = false;
                    },
                    error: function (res) {
                        $rootScope.IsLoading = false;
                    }
                })
            },
            Window: win,
            Complete: function (e, data) {
                $rootScope.IsLoading = true;
                Common.Services.Call($http, {
                    url: Common.Services.url.CAT,
                    method: _CATLocation.URL.Save_Excel,
                    data: { lst: data },
                    success: function (res) {
                        $scope.CATLocation_gridOptions.dataSource.read();
                        $rootScope.IsLoading = false;
                        $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });

                        Common.Services.Call($http, {
                            url: Common.Services.url.CAT,
                            method: _CATRouting.URL.Refresh_Address,
                            data: {},
                            success: function (res) {
                            }
                        });
                    },
                    error: function (res) {
                        $rootScope.IsLoading = false;
                    }
                })
            }
        })
    }

    $scope.CATLocationAddNew_Click = function ($event, win) {
        $event.preventDefault();
        $scope.LoadItem(win, 0);
    }

    $scope.CATLocationEdit_win_numLatOptions = { format: 'n5', spinners: false, culture: 'en-US', min: 0, step: 1, decimals: 5, }

    $scope.CATLocationEdit_win_numLngOptions = { format: 'n5', spinners: false, culture: 'en-US', min: 0, step: 1, decimals: 5, }

    $scope.CATLocationEdit_win_numLoadTimeCOOptions = { format: 'n2', spinners: false, culture: 'en-US', min: 0, step: 1, decimals: 2, }
    $scope.CATLocationEdit_win_numUnLoadTimeCOOptions = { format: 'n2', spinners: false, culture: 'en-US', min: 0, step: 1, decimals: 2, }
    $scope.CATLocationEdit_win_numLoadTimeDIOptions = { format: 'n2', spinners: false, culture: 'en-US', min: 0, step: 1, decimals: 2, }
    $scope.CATLocationEdit_win_numUnLoadTimeDIOptions = { format: 'n2', spinners: false, culture: 'en-US', min: 0, step: 1, decimals: 2, }

    $scope.CATLocationEdit_win_cboCountryOptions = {
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
                $scope.LocationItem.ProvinceID = -1;
                $scope.LocationItem.DistrictID = -1;
                $scope.LocationItem.WardID = "";
                $scope.LoadRegionData($scope.LocationItem);
            }
            else {
                $scope.LocationItem.CountryID = "";
                $scope.LocationItem.ProvinceID = "";
                $scope.LocationItem.DistrictID = "";
                $scope.LocationItem.WardID = "";
                $scope.LoadRegionData($scope.LocationItem);
            }
        }
    }

    Common.ALL.Get($http, {
        url: Common.ALL.URL.Country,
        success: function (data) {
            _CATLocation.Data.Country = data;
            $scope.CATLocationEdit_win_cboCountryOptions.dataSource.data(data);
        }
    })

    $scope.CATLocationEdit_win_cboProvinceOptions = {
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
                $scope.LocationItem.DistrictID = -1;
                $scope.LocationItem.WardID = "";
                $scope.LoadRegionData($scope.LocationItem);
            }
            else {
                $scope.LocationItem.ProvinceID = "";
                $scope.LocationItem.DistrictID = "";
                $scope.LocationItem.WardID = "";
                $scope.LoadRegionData($scope.LocationItem);
            }
        }
    }

    Common.ALL.Get($http, {
        url: Common.ALL.URL.Province,
        success: function (data) {
            _CATLocation.Data.Province = {};
            angular.forEach(data, function (obj, indx) {
                if (Common.HasValue(_CATLocation.Data.Province[obj.CountryID]))
                    _CATLocation.Data.Province[obj.CountryID].push(obj);
                else _CATLocation.Data.Province[obj.CountryID] = [obj];
            })
        }
    })

    $scope.CATLocationEdit_win_cboDistrictOptions = {
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
                $scope.LocationItem.WardID = "";
                $scope.LoadRegionData($scope.LocationItem);
            }
            else {
                $scope.LocationItem.DistrictID = "";
                $scope.LocationItem.WardID = "";
                $scope.LoadRegionData($scope.LocationItem);
            }
        }
    }

    Common.ALL.Get($http, {
        url: Common.ALL.URL.District,
        success: function (data) {
            _CATLocation.Data.District = {};
            angular.forEach(data, function (obj, indx) {
                if (Common.HasValue(_CATLocation.Data.District[obj.ProvinceID]))
                    _CATLocation.Data.District[obj.ProvinceID].push(obj);
                else _CATLocation.Data.District[obj.ProvinceID] = [obj];
            })
        }
    })


    $scope.CATLocationEdit_win_cboWardOptions = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: "contains", suggest: true,
        dataTextField: 'WardName', dataValueField: 'ID',
        dataSource: Common.DataSource.Local([], {
            id: 'ID',
            fields: {
                WardName: { type: 'string' },
                ID: { type: 'number' },
            }
        }),
        change: function (e) {
            var cbo = this;
            if (e.sender.selectedIndex >= 0) {
            }
            else {
                $scope.LocationItem.WardID = "";
            }
        }
    }

    $scope.CATLocationEdit_win_cboGOLOptions = {
        autoBind: true,
        valuePrimitive: true,
        ignoreCase: true,
        filter: 'contains',
        suggest: true,
        dataTextField: 'GroupName',
        dataValueField: 'ID',
        minLength: 3,
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    GroupName: { type: 'string' },
                }
            }
        }),
        change: function (e) {
        }
    }

    Common.ALL.Get($http, {
        url: Common.ALL.URL.CATGroupOfLocation,
        success: function (data) {
            var item = { ID: -1, GroupName: '' };
            data.unshift(item);
            $scope.CATLocationEdit_win_cboGOLOptions.dataSource.data(data)
        }
    })


    //#region Detail
    $scope.TabIndex = 0;
    $scope.lcoation_detail_tabstripOptions = {
        animation: {
            open: { effects: "fadeIn" }
        },
        select: function (e) {
            $timeout(function () {
                $scope.TabIndex = angular.element(e.item).data('tabindex');
                Common.Log("Select_Tab_" + $scope.TabIndex);
            }, 1)
        }
    }

    $scope.CATLocationDetail_Click = function ($event, data, win) {
        $event.preventDefault();
        $rootScope.IsLoading = true;
        Common.Services.Call($http, {
            url: Common.Services.url.CAT,
            method: _CATLocation.URL.GetDetail,
            data: { 'ID': data.ID },
            success: function (res) {
                $rootScope.IsLoading = false;
                win.center();
                win.open();
                $scope.locationPartner_GridOptions.dataSource.data(res.ListPartner);
                $scope.locationCustomer_GridOptions.dataSource.data(res.DTOCATLocationCustomer);
            }
        });
    }

    $scope.locationPartner_GridOptions = {
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
            },
            pageSize: 20,
        }),
        height: '100%', pageable: true, sortable: true, columnMenu: false, filterable: { mode: 'row' }, resizable: true, editable: false,
        columns: [
            { field: 'Code', title: "Mã nhà phân phối", width: 120, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'PartnerName', title: "Nhà phân phối", width: 150, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'Address', title: "Địa chỉ", width: 150, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'CountryName', title: "Quốc gia", width: 150, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'DistrictName', title: "Quận huyện", width: 150, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'ProvinceName', title: "Tỉnh/ Thành", width: 150, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'WardName', title: "Phường", width: 150, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'TypeOfPartnerName', title: "Loại nhà phân phối", width: 150, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { title: ' ', filterable: false, sortable: false }
        ]
    }

    $scope.locationCustomer_GridOptions = {
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
            },
            pageSize: 20,
        }),
        height: '100%', pageable: true, sortable: true, columnMenu: false, filterable: { mode: 'row' }, resizable: true, editable: false,
        columns: [
            { field: 'Code', title: "Mã khách hàng", width: 120, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'CustomerName', title: "Khách hàng", width: 120, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'CUSLocationCode', title: "Mã điểm", width: 120, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'CUSLocationName', title: "Địa điểm", width: 120, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'PartnerCode', title: "Mã nhà phân phối", width: 120, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'PartnerName', title: "Nhà phân phối", width: 150, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'PartnerAddress', title: "Nhà phân phối", width: 150, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'PartnerCountryName', title: "Quốc gia", width: 150, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'PartnerDistrictName', title: "Quận huyện", width: 150, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'PartnerProvinceName', title: "Tỉnh/ Thành", width: 150, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'PartnerWardName', title: "Phường", width: 150, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'TypeOfPartnerName', title: "Loại nhà phân phối", width: 150, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { title: ' ', filterable: false, sortable: false }
        ]
    }
    //#endregion

    //#region MyRegion
    $scope.getLatLng_gridOptions = {
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number', editable: false },
                    Code: { type: 'string', editable: false },
                    Location: { type: 'string', editable: false },
                    Address: { type: 'string', editable: false },
                    Lat: { type: 'number', editable: false },
                    Lng: { type: 'number', editable: false },
                }
            },
            pageSize: 100,
        }),
        height: '100%', pageable: true, sortable: true, columnMenu: false, filterable: { mode: 'row' }, resizable: true, editable: false,
        columns: [
            { field: 'Code', title: '{{RS.CATLocation.Code}}', width: 100, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'Location', title: '{{RS.CATLocation.Location}}', width: 150, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'Address', title: '{{RS.CATLocation.Address}}', width: 150, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'DistrictName', title: '{{RS.CATDistrict.DistrictName}}', width: 150, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'ProvinceName', title: '{{RS.CATProvince.ProvinceName}}', width: 150, filterable: { cell: { operator: 'contains', showOperators: false } } },
            {
                field: 'Lat', title: '{{RS.CATLocation.Lat}}', width: 150,
                template: '<input kendo-numeric-text-box k-ng-model="dataItem.Lat" k-options="numLatLngOptions" style="width:100%" />',
                filterable: {
                    cell: {
                        template: function (container) {
                            container.element.kendoNumericTextBox({
                                format: '#,##0.00000', spinners: false, culture: 'en-US', min: 0, step: 1, decimals: 5,
                            })
                        },
                        operator: "gte",
                        showOperators: false
                    }
                }
            },
            {
                field: 'Lng', title: '{{RS.CATLocation.Lng}}', width: 150,
                template: '<input kendo-numeric-text-box k-ng-model="dataItem.Lng" k-options="numLatLngOptions" style="width:100%" />',
                filterable: {
                    cell: {
                        template: function (container) {
                            container.element.kendoNumericTextBox({
                                format: '#,##0.00000', spinners: false, culture: 'en-US', min: 0, step: 1, decimals: 5,
                            })
                        },
                        operator: "gte",
                        showOperators: false
                    }
                }
            },
            { title: ' ', filterable: false, sortable: false }
        ]
    }

    $scope.CATLocation_GetLatLng_Click = function ($event, win) {
        $event.preventDefault();
        
        var data = $.grep($scope.CATLocation_grid.dataSource.data(), function (o) { return o.IsChoose == true });
        if (data.length > 0) {
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.CAT,
                method: _CATLocation.URL.GetLatLng,
                data: { lst: data },
                success: function (res) {
                    $rootScope.IsLoading = false;
                    win.center();
                    win.open();
                    $scope.getLatLng_grid.dataSource.data(res);
                },
                error: function (res) {
                    $rootScope.IsLoading = false;
                }
            });
        }
    };

    $scope.numLatLngOptions = { format: 'n5', spinners: false, culture: 'en-US', min: -180, step: 1, decimals: 5, }

    $scope.UpdateLatLng_Click = function ($event, win,grid) {
        $event.preventDefault();
        var data = grid.dataSource.data();
        if (data.length > 0) {
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.CAT,
                method: _CATLocation.URL.UpdateLatLng,
                data: { lst: data },
                success: function (res) {
                    $rootScope.IsLoading = false;
                    $rootScope.Message({ Msg: 'Đã cập nhật', Type: Common.Message.Type.Notify, NotifyType: Common.Message.NotifyType.SUCCESS });
                    win.close();
                    $scope.CATLocation_gridOptions.dataSource.read();
                },
                error: function (res) {
                    $rootScope.IsLoading = false;
                }
            });
        }
    };
    //#endregion

    //#region Action

    $scope.ShowSetting = function ($event, grid) {
        $event.preventDefault();

        $rootScope.ShowSetting({
            ListView: views.CATLocation,
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

            var data = _CATLocation.Data.Province[countryID];
            $scope.CATLocationEdit_win_cboProvinceOptions.dataSource.data(data);
            if (Common.HasValue(provinceID)) {
                if (provinceID < 0) {
                    provinceID = data[0].ID;
                }
            }
            $timeout(function () {
                item.ProvinceID = provinceID;
            }, 1)

            data = _CATLocation.Data.District[provinceID];
            $scope.CATLocationEdit_win_cboDistrictOptions.dataSource.data(data);
            if (Common.HasValue(districtID)) {
                if (districtID < 0) {
                    districtID = data[0].ID;
                }
            }
            $timeout(function () {
                item.DistrictID = districtID;
            }, 1)

            //data = _SYSCustomer_Index.Data.Ward[districtID];
            //$scope.cboWardOptions.dataSource.data(data);
            //if (wardID < 1 && data.length > 0)
            //    wardID = data[0].ID;
            //$timeout(function () {
            //    item.WardID = wardID;
            //}, 1)
        }
        catch (e) { }
    }
}]);