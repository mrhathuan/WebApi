/// <reference path="~/Scripts/jquery-1.9.1.intellisense.js" />
/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />

var _CATStation = {
    URL: {
        Read: 'CATStation_Data',
        Get: 'CATStation_Get',
        Save: 'CATStation_Save',
        Delete: 'CATStation_Delete',

        LocationList: 'LocationInStation_List',
        LocationSave: 'LocationInStation_Save',
        LocationGet: 'LocationInStation_Get',
        LocationDelete: 'LocationInStation_Delete',
        LocationNotInList: 'LocationNotInStation_List',
        LocationNotInSave: 'LocationNotInStation_SaveList',

        Excel_Export: 'CATStation_Export_GetData',
        Excel_Import: 'CATStation_Excel_Save',
        Excel_Check: 'CATStation_Excel_Check',
    },
    Data: {
        Country: [],
        Province: [],
        District: [],
        Ward: [],
        ItemBackUp: null,
    }
}

angular.module('myapp').controller('CATStation_IndexCtrl', ['$rootScope', '$scope', '$http', '$location', '$state', '$timeout', function ($rootScope, $scope, $http, $location, $state, $timeout) {
    if ($rootScope.IsPageComplete != true) return;
    Common.Log('CATStation_IndexCtrl');
    $rootScope.IsLoading = false;

    $scope.Auth = $rootScope.GetAuth();
    $scope.StationItem = {ID:0};

    $scope.CreateGrid = function () {
        Common.Services.Call($http, {
            url: Common.Services.url.CAT,
            method: _CATStation.URL.Read,
            data: {},
            success: function (res) {

                _CATStation.Data.Stations = []
                if (Common.HasValue(res)) {

                    $scope.HasCustomer = false;
                    var data = [];
                    var columns = [];

                    Main_Columns = [
                        {
                            title: ' ', width: '45px', filterable: false, sortable: false,
                            template: '<a href="/" ng-click="CATStation_EditClick($event,CATStation_win,dataItem)" ng-show="dataItem.IsPartner" class="k-button"><i class="fa fa-pencil"></i></a>' +
                            '<a href="/" ng-click="CATStation_GridDestroy_Click($event,dataItem)" ng-show="dataItem.IsPartner" class="k-button"><i class="fa fa-trash"></i></a>',
                        },
                        {
                            field: 'GroupOfPartnerName', title: 'Loại trạm', template: '#=IsPartner?"Trạm":""#', width: 100,
                            filterable: { cell: { operator: 'contains', showOperators: false } }
                        },
                        { field: 'PartnerName', title: 'Tên', width: '125px', filterable: { cell: { operator: 'contains', showOperators: false } } },
                        { field: 'Address', title: 'Địa chỉ', width: '250px', filterable: { cell: { operator: 'contains', showOperators: false } } },
                        { field: 'Code', title: 'Mã hệ thống', width: '100px', filterable: { cell: { operator: 'contains', showOperators: false } } },
                    ];

                    angular.forEach(Main_Columns, function (item, idx) {
                        columns.push(item)
                    })

                    var model = {
                        id: 'ID',
                        fields: {
                            ID: { type: 'number' },
                            PartnerName: { type: 'string', editable: false },
                            Address: { type: 'string', editable: false },
                            WardName: { type: 'string', editable: false },
                            DistrictName: { type: 'string', editable: false },
                            ProvinceName: { type: 'string', editable: false },
                            CountryName: { type: 'string', editable: false },
                            GroupOfPartnerName: { type: 'string', editable: false },
                            Code: { type: 'string', editable: false },
                            IsPartner: { type: 'boolean', editable: false }
                        }
                    }
                    //var lstStation = $.extend(true, [], _CATStation.Data.Stations)
                    var myID = 1;
                    
                    for (var i = 0; i < res.length; i++) {
                        var item = res[i];
                        item['myID'] = myID;
                        item['IsPartner'] = true;
                        data.push(item);
                        myID++;
                        for (var j = 0; j < item.lstPartnerLocation.length; j++) {
                            var itemLo = item.lstPartnerLocation[j];
                            var itempush = {
                                myID: myID,
                                Address: itemLo.Address,
                                Code: itemLo.PartnerCode,
                                CountryID: itemLo.CountryID,
                                CountryName: itemLo.CountryName,
                                DistrictID: itemLo.DistrictID,
                                DistrictName: itemLo.DistrictName,
                                GroupOfPartnerName: itemLo.GroupOfPartnerName,
                                ID: itemLo.ID,
                                IsPartner: itemLo.IsPartner,
                                PartnerID: itemLo.PartnerID,
                                PartnerName: itemLo.PartnerName,
                                ProvinceID: itemLo.ProvinceID,
                                ProvinceName: itemLo.ProvinceName,
                                WardID: itemLo.WardID,
                                WardName: itemLo.WardName
                            };
                            data.push(itempush)
                            myID++;
                        }
                    }
                    var options = {
                        dataSource: Common.DataSource.Local({
                            data: data,
                            model: model,
                            pageSize: 20,
                        }),
                        height: '100%', pageable: true, sortable: true, columnMenu: false, resizable: true, reorderable: true, filterable: false,
                        columns: columns
                    }
                    
                    if ($scope.CATStation_MainGrid_Options != null) {
                        $scope.CATStation_MainGrid_Options.dataSource.data(data);
                        $scope.CATStation_MainGrid.dataSource.data(data);
                        $scope.CATStation_MainGrid.refresh();
                    }
                    $scope.CATStation_MainGrid_Options = options;
                    $rootScope.IsLoading = false;
                }
            },
            error: function (res) {
                $rootScope.IsLoading = false;
            }
        });

    }

    $scope.CreateGrid();

    $scope.CATStation_AddClick = function ($event, win) {
        $event.preventDefault();
        $scope.LoadItem(win, 0);
    }

    $scope.CATStation_EditClick = function ($event, win, data) {
        $event.preventDefault();
        $scope.LoadItem(win, data.ID);
    }

    $scope.LoadItem = function (win, id) {
        $rootScope.IsLoading = true;
        Common.Services.Call($http, {
            url: Common.Services.url.CAT,
            method: _CATStation.URL.Get,
            data: { id: id },
            success: function (res) {
                $scope.StationItem = res;
                $scope.LoadRegionDataStation($scope.StationItem);
                $scope.CATStation_Location_GridOptions.dataSource.read();
                $rootScope.IsLoading = false;
                $scope.CATStation_Tab.select(0);
                win.center();
                win.open()
            }
        })
    }

    $scope.CATStation_GridDestroy_Click = function ($event, data) {
        $event.preventDefault();
        $rootScope.Message({
            Type: Common.Message.Type.Confirm,
            NotifyType: Common.Message.NotifyType.SUCCESS,
            Title: 'Thông báo',
            Msg: 'Bạn muốn xóa dữ liệu đã chọn?',
            Close: null,
            Ok: function () {
                Common.Services.Call($http, {
                    url: Common.Services.url.CAT,
                    method: _CATStation.URL.Delete,
                    data: { 'item': data },
                    success: function (res) {
                        $scope.CreateGrid();
                    },
                    error: function (res) {
                        $rootScope.IsLoading = false;
                    }
                })
            }
        })
    }

    //#region  popup main
    $scope.CATStation_TabIndex = 0;
    $scope.CATStation_TabOptions = {
        animation: {
            open: { effects: "fadeIn" }
        },
        select: function (e) {
            $timeout(function () {
                $scope.CATStation_TabIndex = angular.element(e.item).data('tabindex');
                Common.Log("Select_Tab_" + $scope.CATStation_TabIndex);
            }, 1)
        }
    }

    //#region cbx Station
    $scope.CATStationEdit_win_cboCountryOptions = {
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
                $scope.StationItem.ProvinceID = -1;
                $scope.StationItem.DistrictID = -1;
                $scope.StationItem.WardID = "";
                $scope.LoadRegionDataStation($scope.StationItem);
            }
            else {
                $scope.StationItem.CountryID = "";
                $scope.StationItem.ProvinceID = "";
                $scope.StationItem.DistrictID = "";
                $scope.StationItem.WardID = "";
                $scope.LoadRegionDataStation($scope.StationItem);
            }
        }
    }

    Common.ALL.Get($http, {
        url: Common.ALL.URL.Country,
        success: function (data) {
            _CATStation.Data.Country = data;
            $scope.CATStationEdit_win_cboCountryOptions.dataSource.data(data);
        }
    })

    $scope.CATStationEdit_win_cboProvinceOptions = {
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
                $scope.StationItem.DistrictID = -1;
                $scope.StationItem.WardID = "";
                $scope.LoadRegionDataStation($scope.StationItem);
            }
            else {
                $scope.StationItem.ProvinceID = "";
                $scope.StationItem.DistrictID = "";
                $scope.StationItem.WardID = "";
                $scope.LoadRegionDataStation($scope.StationItem);
            }
        }
    }

    Common.ALL.Get($http, {
        url: Common.ALL.URL.Province,
        success: function (data) {
            _CATStation.Data.Province = {};
            angular.forEach(data, function (obj, indx) {
                if (Common.HasValue(_CATStation.Data.Province[obj.CountryID]))
                    _CATStation.Data.Province[obj.CountryID].push(obj);
                else _CATStation.Data.Province[obj.CountryID] = [obj];
            })
        }
    })

    $scope.CATStationEdit_win_cboDistrictOptions = {
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
                $scope.StationItem.WardID = "";
                $scope.LoadRegionDataStation($scope.StationItem);
            }
            else {
                $scope.StationItem.DistrictID = "";
                $scope.StationItem.WardID = "";
                $scope.LoadRegionDataStation($scope.StationItem);
            }
        }
    }

    Common.ALL.Get($http, {
        url: Common.ALL.URL.District,
        success: function (data) {
            _CATStation.Data.District = {};
            angular.forEach(data, function (obj, indx) {
                if (Common.HasValue(_CATStation.Data.District[obj.ProvinceID]))
                    _CATStation.Data.District[obj.ProvinceID].push(obj);
                else _CATStation.Data.District[obj.ProvinceID] = [obj];
            })
        }
    })


    $scope.CATStationEdit_win_cboWardOptions = {
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
                $scope.StationItem.WardID = "";
            }
        }
    }

    $scope.LoadRegionDataStation = function (item) {
        Common.Log("LoadRegionDataStation");
        try {
            var countryID = item.CountryID;
            var provinceID = item.ProvinceID;
            var districtID = item.DistrictID;
            var wardID = item.WardID;

            var data = _CATStation.Data.Province[countryID];
            $scope.CATStationEdit_win_cboProvinceOptions.dataSource.data(data);
            if (Common.HasValue(provinceID)) {
                if (provinceID < 0) {
                    provinceID = data[0].ID;
                }
            }
            $timeout(function () {
                item.ProvinceID = provinceID;
            }, 1)

            data = _CATStation.Data.District[provinceID];
            $scope.CATStationEdit_win_cboDistrictOptions.dataSource.data(data);
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
    //#endregion
    $scope.CATStation_SaveClick = function ($event, win, vform) {
        $event.preventDefault();
        if (vform()) {
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.CAT,
                method: _CATStation.URL.Save,
                data: { item: $scope.StationItem, partnerid: $scope.StationItem.ID },
                success: function (res) {
                    $rootScope.Message({
                        Type: Common.Message.Type.Notify,
                        NotifyType: Common.Message.NotifyType.SUCCESS,
                        Title: 'Thông báo',
                        Msg: 'Thành công',
                        Ok: null,
                        close: null,
                    })
                    $scope.CreateGrid();
                    $scope.LoadItem(win, res);
                }
            });
        }
    }

    $scope.CATStation_DeleteClick = function ($event, win) {
        $event.preventDefault();
        $rootScope.Message({
            Type: Common.Message.Type.Confirm,
            NotifyType: Common.Message.NotifyType.SUCCESS,
            Title: 'Thông báo',
            Msg: 'Bạn muốn xóa dữ liệu đã chọn?',
            Close: null,
            Ok: function () {
                $rootScope.IsLoading = true;
                Common.Services.Call($http, {
                    url: Common.Services.url.CAT,
                    method: _CATStation.URL.Delete,
                    data: { item: $scope.StationItem },
                    success: function (res) {
                        $rootScope.Message({
                            Type: Common.Message.Type.Notify,
                            NotifyType: Common.Message.NotifyType.SUCCESS,
                            Title: 'Thông báo',
                            Msg: 'Thành công',
                            Ok: null,
                            close: null,
                        })
                        win.close();
                        $scope.CreateGrid();
                    }
                });
            }
        })
    }

    $scope.CATStation_CloseClick = function ($event, win) {
        $event.preventDefault();
        win.close();
    }
    //tab2
    $scope.CATStation_Location_GridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.CAT,
            method: _CATStation.URL.LocationList,
            readparam: function () { return { partnerid: $scope.StationItem.ID } },
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
                }
            }
        }),
        height: '100%', pageable: true, sortable: true, columnMenu: false, resizable: true, reorderable: true, filterable: false,
        columns: [
            {
                title: ' ', width: '45px', filterable: false, sortable: false,
                template: '<a href="/" ng-click="CATStation_Location_EditClick($event,Location_EditWin,dataItem)" class="k-button"><i class="fa fa-pencil"></i></a>' +
                    '<a href="/" ng-click="CATStation_Location_DestroyClick($event,dataItem)" class="k-button"><i class="fa fa-trash"></i></a>',
            },
            { field: 'Location', title: '{{RS.CATLocation.Location}}', width: 150 },
            { field: 'PartnerCode', title: '{{RS.CATPartnerLocation.PartnerCode}}', width: 150 },
            { field: 'Address', title: '{{RS.CATLocation.Address}}', width: 150 },
            { field: 'CountryName', title: '{{RS.CATCountry.CountryName}}', width: 150 },
            { field: 'ProvinceName', title: '{{RS.CATProvince.ProvinceName}}', width: 150 },
            { field: 'DistrictName', title: '{{RS.CATDistrict.DistrictName}}', width: 150 },
            { field: 'WardName', title: '{{RS.CATWard.WardName}}', width: 150 },
            { field: 'Lat', title: '{{RS.CATLocation.Lat}}', width: 100 },
             { field: 'Lng', title: '{{RS.CATLocation.Lng}}', width: 100 },
        ]
    }


    $scope.CATStation_Location_DestroyClick = function ($event, data) {
        $event.preventDefault();
        if (Common.HasValue(data)) {
            $rootScope.Message({
                Type: Common.Message.Type.Confirm,
                NotifyType: Common.Message.NotifyType.SUCCESS,
                Title: 'Thông báo',
                Msg: 'Bạn muốn xóa các dữ liệu đã chọn?',
                Close: null,
                Ok: function () {
                    $rootScope.IsLoading = true;
                    Common.Services.Call($http, {
                        url: Common.Services.url.CAT,
                        method: _CATStation.URL.LocationDelete,
                        data: { 'item': data },
                        success: function (res) {
                            $scope.CATStation_Location_GridOptions.dataSource.read();
                            $scope.CreateGrid();
                        }
                    });
                }
            })
        }
    }

    $scope.CATStation_Location_SearchClick = function ($event, win) {
        $event.preventDefault()

        win.center();
        win.open();
        $scope.CATStation_Location_GridOptions.dataSource.read();
        $scope.CATStation_LocationSearch_Grid.refresh();
    }

    $scope.CATLocationAddNew_Click = function ($event, win) {
        $event.preventDefault();
        $scope.LoadItem(win, 0);
    }

    
    //#endregion

    //#region popup edit location

    //$scope.StationLocation_cboCountryOptions.dataSource.data(_CATStation.Data.Country)
    
    $scope.StationLocation_numLatOptions = { format: '#,##0.00000', spinners: false, culture: 'en-US', min: 0, step: 1, decimals: 5, }
    $scope.StationLocation_numLngOptions = { format: '#,##0.00000', spinners: false, culture: 'en-US', min: 0, step: 1, decimals: 5, }
    $scope.CATStation_Location_AddClick = function ($event, win) {
        $event.preventDefault();
        $scope.LoadLocationItem(win, 0);
    }

    $scope.CATStation_Location_EditClick = function ($event, win, data) {
        $event.preventDefault();
        $scope.LoadLocationItem(win, data.ID);
    }

    $scope.LoadLocationItem = function (win, id) {
        $rootScope.IsLoading = true;
        Common.Services.Call($http, {
            url: Common.Services.url.CAT,
            method: _CATStation.URL.LocationGet,
            data: { id: id },
            success: function (res) {
                $scope.LocationData = res;
                $scope.LoadRegionData($scope.LocationData);
                $rootScope.IsLoading = false;
                win.center();
                win.open()
            }
        })
    }
    $scope.Location_Save = function ($event, win, vform) {
        $event.preventDefault();debugger
        if (vform()) {
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.CAT,
                method: _CATStation.URL.LocationSave,
                data: { item: $scope.LocationData, partnerid: $scope.StationItem.ID },
                success: function (res) {
                    $rootScope.IsLoading = false;
                    $scope.CATStation_Location_GridOptions.dataSource.read();
                    win.close();
                    $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                }
            })
        }
    }

    $scope.Win_Close = function ($event, win) {
        $event.preventDefault();
        win.close();
    }
    //#endregion

    //#region cbx Location
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
                $scope.LoadRegionData($scope.LocationData);
            }
            else {
                $scope.LocationItem.CountryID = "";
                $scope.LocationItem.ProvinceID = "";
                $scope.LocationItem.DistrictID = "";
                $scope.LocationItem.WardID = "";
                $scope.LoadRegionData($scope.LocationData);
            }
        }
    }

    Common.ALL.Get($http, {
        url: Common.ALL.URL.Country,
        success: function (data) {
            _CATStation.Data.Country = data;
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
                $scope.LocationData.DistrictID = -1;
                $scope.LocationData.WardID = "";
                $scope.LoadRegionData($scope.LocationData);
            }
            else {
                $scope.LocationData.ProvinceID = "";
                $scope.LocationData.DistrictID = "";
                $scope.LocationData.WardID = "";
                $scope.LoadRegionData($scope.LocationData);
            }
        }
    }

    Common.ALL.Get($http, {
        url: Common.ALL.URL.Province,
        success: function (data) {
            _CATStation.Data.Province = {};
            angular.forEach(data, function (obj, indx) {
                if (Common.HasValue(_CATStation.Data.Province[obj.CountryID]))
                    _CATStation.Data.Province[obj.CountryID].push(obj);
                else _CATStation.Data.Province[obj.CountryID] = [obj];
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
                $scope.LocationData.WardID = "";
                $scope.LoadRegionData($scope.LocationData);
            }
            else {
                $scope.LocationData.DistrictID = "";
                $scope.LocationData.WardID = "";
                $scope.LoadRegionData($scope.LocationData);
            }
        }
    }

    Common.ALL.Get($http, {
        url: Common.ALL.URL.District,
        success: function (data) {
            _CATStation.Data.District = {};
            angular.forEach(data, function (obj, indx) {
                if (Common.HasValue(_CATStation.Data.District[obj.ProvinceID]))
                    _CATStation.Data.District[obj.ProvinceID].push(obj);
                else _CATStation.Data.District[obj.ProvinceID] = [obj];
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
                $scope.LocationData.WardID = "";
            }
        }
    }

    $scope.LoadRegionData = function (item) {
        Common.Log("LoadRegionData");
        try {
            var countryID = item.CountryID;
            var provinceID = item.ProvinceID;
            var districtID = item.DistrictID;
            var wardID = item.WardID;

            var data = _CATStation.Data.Province[countryID];
            $scope.CATLocationEdit_win_cboProvinceOptions.dataSource.data(data);
            if (Common.HasValue(provinceID)) {
                if (provinceID < 0) {
                    provinceID = data[0].ID;
                }
            }
            $timeout(function () {
                item.ProvinceID = provinceID;
            }, 1)

            data = _CATStation.Data.District[provinceID];
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
    //#endregion
    //#region popup search
    $scope.CATStation_LocationSearch_GridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.CAT,
            method: _CATStation.URL.LocationNotInList,
            readparam: function () { return { partnerid: $scope.StationItem.ID } },
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
                    IsChoose: { type: 'boolean' }
                }
            }
        }),
        height: '100%', pageable: true, sortable: true, columnMenu: false, resizable: true, reorderable: true, filterable: { mode: 'row' },
        columns: [
            {
                title: ' ', width: '45px',
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,CATStation_LocationSearch_Grid,CATStation_LocationSearch_ChooseChange)" />',
                headerAttributes: { style: 'text-align: center;' }, attributes: { style: 'text-align: center;' },
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,CATStation_LocationSearch_Grid,CATStation_LocationSearch_ChooseChange)" />',
                filterable: false, sortable: false
            },
            { field: 'Location', title: '{{RS.CATLocation.Location}}', width: 150, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'PartnerCode', title: '{{RS.CATLocation.Code}}', width: 150, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'Address', title: '{{RS.CATLocation.Address}}', width: 150, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'CountryName', title: '{{RS.CATCountry.CountryName}}', width: 150, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'ProvinceName', title: '{{RS.CATProvince.ProvinceName}}', width: 150, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'DistrictName', title: '{{RS.CATDistrict.DistrictName}}', width: 150, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'WardName', title: '{{RS.CATWard.WardName}}', width: 150, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'Lat', title: '{{RS.CATLocation.Lat}}', width: 100, filterable: { cell: { operator: 'equal', showOperators: false } } },
             { field: 'Lng', title: '{{RS.CATLocation.Lng}}', width: 100, filterable: { cell: { operator: 'equal', showOperators: false } } },
        ]
    }
    $scope.CATStation_LocationSearch_ChooseChange = function ($event, grid, haschoose) {
        $scope.HasChoose = haschoose;
    };

    $scope.CATStation_Location_ChooseClick = function ($event, win, grid) {
        $event.preventDefault()
        var data = grid.dataSource.data();
        var dataSend = $.grep(data, function (o) { return o.IsChoose == true })
        if (dataSend.length > 0) {
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.CAT,
                method: _CATStation.URL.LocationNotInSave,
                data: { lst: dataSend, partnerid: $scope.StationItem.ID },
                success: function (res) {
                    $rootScope.Message({
                        Type: Common.Message.Type.Notify,
                        NotifyType: Common.Message.NotifyType.SUCCESS,
                        Title: 'Thông báo',
                        Msg: 'Thành công',
                        Close: null,
                        Ok: null
                    })
                    //
                    $scope.CATStation_Location_GridOptions.dataSource.read();
                    $scope.CreateGrid();
                    win.close()
                }
            });
        }
        else {
            $rootScope.Message({
                Type: Common.Message.Type.Alert,
                NotifyType: Common.Message.NotifyType.ERROR,
                Title: 'Thông báo',
                Msg: 'Chưa chọn địa điểm',
                Close: null,
                Ok: null
            })
        }
    }
    $scope.CATStation_LocationNotIn_win_CloseClick = function ($event, win) {
        $event.preventDefault();
        win.close()
    }
    //#endregion
    //#endregion
    //#region Action
    $scope.ShowSetting = function ($event, grid) {
        $event.preventDefault();

        $rootScope.ShowSetting({
            ListView: views.CATStation,
            event: $event,
            grid: grid,
            current: $state.current
        });
    };

    //#endregion

    $scope.CATStationExcel_Click = function ($event, win) {
        $event.preventDefault();
        $rootScope.UploadExcel({
            Path: Common.FolderUpload.Import,
            columns: [
                { field: 'Code', width: 100, title: '{{RS.CATSeaPort.Code}}', filterable: { cell: { showOperators: false, operator: "contains" } } }
            ],
            Download: function () {
                Common.Services.Call($http, {
                    url: Common.Services.url.CAT,
                    method: _CATStation.URL.Excel_Export,
                    data: {},
                    success: function (res) {
                        $rootScope.DownloadFile(res);
                    }
                })
            },
            Upload: function (e, callback) {
                Common.Services.Call($http, {
                    url: Common.Services.url.CAT,
                    method: _CATStation.URL.Excel_Check,
                    data: { item: e },
                    success: function (data) {
                        callback(data);
                    }
                })
            },
            Window: win,
            Complete: function (e, data) {
                $rootScope.IsLoading = true;
                Common.Services.Call($http, {
                    url: Common.Services.url.CAT,
                    method: _CATStation.URL.Excel_Import,
                    data: { lst: data },
                    success: function (res) {
                        $rootScope.IsLoading = false;
                        $rootScope.Message({
                            Type: Common.Message.Type.Notify,
                            NotifyType: Common.Message.NotifyType.SUCCESS,
                            Title: 'Thông báo',
                            Msg: 'Thành công',
                            Ok: null,
                            close: null,
                        })
                        $scope.CreateGrid('cus');
                    }
                })
            }
        })
    }
}]);