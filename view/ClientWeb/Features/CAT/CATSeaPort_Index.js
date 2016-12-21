

/// <reference path="~/Scripts/jquery-1.9.1.intellisense.js" />
/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />

var _CATSeaPort = {
    URL: {
        Read_Customer: 'CATSeaPort_Customer_Read',
        Read_SeaPort: 'CATSeaPort_SeaPortCustom_Read',
        Get_SeaPort: 'CATSeaPort_SeaPort_Get',
        Update_SeaPort: 'CATSeaPort_SeaPort_Update',
        Delete_SeaPort: 'CATSeaPort_SeaPort_Destroy',
        UpdateListCode_SeaPort: 'CATSeaPort_SeaPortCustom_SaveList',
        Read_Location: 'CATSeaPort_Location_Read',
        Update_Location: 'CATSeaPort_Location_Update',
        Get_Location: 'CATSeaPort_Location_Get',
        Delete_Location: 'CATSeaPort_Location_Destroy',
        Read_LocationNotIn: 'CATSeaPort_LocationNotIn_Read',
        Update_LocationNotIn: 'CATSeaPort_LocationNotIn_SaveList',
        Get_CustomerCode: 'CATSeaPort_SeaPortCustomer_List',
        Excel_Export: 'CATSeaPort_Export_GetData',
        Excel_Import: 'CATSeaPort_Excel_Save',
        Excel_Check: 'CATSeaPort_Excel_Check',
        Excel_ExportCode: 'CATSeaPort_Excel_ExportCode',
        Excel_ImportCode: 'CATSeaPort_Excel_SaveCode',
        Excel_CheckCode: 'CATSeaPort_Excel_CheckCode',
    },
    Data: {
        Country: [],
        Province: [],
        District: [],
        Ward: [],
        ItemBackUp: null,
        SeaPort: [],
        DataCustomer: [],
        Main_Columns: [
            {
                title: ' ', width: '90px', filterable: false, sortable: false, locked: true,
                template:
                    '<a ng-show="!IsEditCode&&HasCustomer" href="/" ng-click="CATSeaPort_CreateCode_Click($event,CATSeaPort_MainGrid)" class="k-button"><i class="fa fa-cog" aria-hidden="true"></i></a>' +
                    '<a href="/" ng-click="CATSeaPort_EditClick($event,CATSeaPort_win,CATSeaPort_MainGrid)" ng-show="dataItem.IsPartner&&!IsEditCode" class="k-button"><i class="fa fa-pencil"></i></a>' +
                    '<a ng-show="IsEditCode && CATSeaPortCodeEdit.myID==#=myID#?true:false&&HasCustomer" href="/" ng-click="CATSeaPort_SaveCode($event,CATSeaPort_MainGrid)" class="k-button"><i class="fa fa-check"></i></a>' +
                    '<a ng-show="IsEditCode && CATSeaPortCodeEdit.myID==#=myID#?true:false&&HasCustomer" href="/" ng-click="CATSeaPort_CancelCode($event,CATSeaPort_MainGrid)" class="k-button"><i class="fa fa-ban"></i></a>',
            },
            {
                field: 'GroupOfPartnerName', title: 'Loại cảng biển', template: '#=IsPartner?"Cảng biển":""#', width: 100, locked: true,
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            { field: 'PartnerName', title: '{{RS.CATPartner.PartnerName}}', width: '125px', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'Address', title: '{{RS.CATPartner.Address}}', width: '250px', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'Code', title: '{{RS.CATPartner.Code}}', width: '100px', filterable: { cell: { operator: 'contains', showOperators: false } } },
        ]
    },
}

angular.module('myapp').controller('CATSeaPort_IndexCtrl', ['$rootScope', '$scope', '$http', '$location', '$state', '$timeout', function ($rootScope, $scope, $http, $location, $state, $timeout) {
    if ($rootScope.IsPageComplete != true) return;
    Common.Log('CATSeaPort_IndexCtrl');
    $rootScope.IsLoading = true;

    $scope.Auth = $rootScope.GetAuth();

    $scope.SeaPortID = 0;
    $scope.SeaPortItem = null;
    $scope.SeaPortLocationItem = null;
    $scope.HasChoose = false;
    $scope.IsEditCode = false;
    $scope.HasCustomer = false;

    $scope.CATSeaPortCodeEdit = null;

    $scope.SeaPortPartnerID = 0;

    $scope.CATSeaPort_ListCustomer = [];

    $scope.CATSeaPort_AddClick = function ($event, win) {
        $event.preventDefault();
        $scope.SeaPortID = 0;
        $scope.LoadItem(win, 0);
    }

    $scope.CATSeaPort_EditClick = function ($event, win, grid) {
        $event.preventDefault();
        var tr = $($event.target).closest('tr');
        var id = 0;
        var item = grid.dataItem(tr);
        if (Common.HasValue(item)) id = item.PartnerID;
        $scope.LoadItem(win, id);
    }

    $scope.CATSeaPortExcel_Click = function ($event, win) {
        $event.preventDefault();
        $rootScope.UploadExcel({
            Path: Common.FolderUpload.Import,
            columns: [
                { field: 'Code', width: 100, title: '{{RS.CATSeaPort.Code}}', filterable: { cell: { showOperators: false, operator: "contains" } } }
            ],
            Download: function () {
                Common.Services.Call($http, {
                    url: Common.Services.url.CAT,
                    method: _CATSeaPort.URL.Excel_Export,
                    data: {},
                    success: function (res) {
                        $rootScope.DownloadFile(res);
                    }
                })
            },
            Upload: function (e, callback) {
                Common.Services.Call($http, {
                    url: Common.Services.url.CAT,
                    method: _CATSeaPort.URL.Excel_Check,
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
                    method: _CATSeaPort.URL.Excel_Import,
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

                        Common.Services.Call($http, {
                            url: Common.Services.url.CAT,
                            method: _CATRouting.URL.Refresh_Address,
                            data: {},
                            success: function (res) {
                            }
                        });
                    }
                })
            }
        })
    }

    $scope.CATSeaPortExcelCode_Click = function ($event, win) {
        $event.preventDefault();
        $rootScope.UploadExcel({
            Path: Common.FolderUpload.Import,
            columns: [
                { field: 'SysCode', width: 120, title: '{{RS.CATSeaPort.SysCode}}', filterable: { cell: { showOperators: false, operator: "contains" } } }
            ],
            Download: function () {

                if (Common.HasValue($scope.CATSeaPort_ListCustomer) && $scope.CATSeaPort_ListCustomer.length > 0) {
                    Common.Services.Call($http, {
                        url: Common.Services.url.CAT,
                        method: _CATSeaPort.URL.Excel_ExportCode,
                        data: { lst: $scope.CATSeaPort_ListCustomer },
                        success: function (res) {
                            $rootScope.DownloadFile(res);
                        }
                    })
                } else {
                    $rootScope.Message({
                        Type: Common.Message.Type.Alert,
                        NotifyType: Common.Message.NotifyType.ERROR,
                        Title: 'Thông báo',
                        Msg: 'Bạn chưa chọn khách hàng',
                        Ok: null,
                        close: null,
                    })
                }
            },
            Upload: function (e, callback) {
                $rootScope.IsLoading = true
                Common.Services.Call($http, {
                    url: Common.Services.url.CAT,
                    method: _CATSeaPort.URL.Excel_CheckCode,
                    data: { item: e },
                    success: function (data) {

                        callback(data);
                        $rootScope.IsLoading = false;
                    }
                })
            },
            Window: win,
            Complete: function (e, data) {
                $rootScope.IsLoading = true
                Common.Services.Call($http, {
                    url: Common.Services.url.CAT,
                    method: _CATSeaPort.URL.Excel_ImportCode,
                    data: { lst: data },
                    success: function (res) {
                        $rootScope.Message({
                            Type: Common.Message.Type.Notify,
                            NotifyType: Common.Message.NotifyType.SUCCESS,
                            Title: 'Thông báo',
                            Msg: 'Thành công',
                            Ok: null,
                            close: null,
                        })
                        $scope.CreateGrid('cus')
                        $rootScope.IsLoading = false;
                    }
                })
            }
        })
    }

    $scope.CATSeaPort_mts_CancelClick = function ($event) {
        $event.preventDefault();
        if ($scope.CATSeaPort_ListCustomer.length > 0) {
            $scope.CATSeaPort_ListCustomer = [];
            $scope.CreateGrid('new')
        }
    }

    $scope.LoadItem = function (win, id) {
        $scope.SeaPortPartnerID = id;
        Common.Services.Call($http, {
            url: Common.Services.url.CAT,
            method: _CATSeaPort.URL.Get_SeaPort,
            data: { id: id },
            success: function (res) {

                $scope.IsShowTab2 = false;

                if (id > 0) { $scope.IsShowTab2 = true; }

                $scope.SeaPortItem = res;
                $scope.LoadRegionDataGeneralTab($scope.SeaPortItem);
                $scope.CATSeaPort_Location_GridOptions.dataSource.read();

                $scope.CATSeaPort_Tab.select(0);
                win.center();
                win.open()
            }
        })
    }

    $scope.CATSeaPort_mts_CustomerOption = {
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    Code: { type: 'string' },
                    CustomerName: { type: 'string' }
                }
            }
        }),
        valuePrimitive: true, dataTextField: "Code", dataValueField: "ID", placeholder: "Chọn khách hàng...", filter: "contains", ignoreCase: true,
        highlightFirst: true, autoClose: false,
        itemTemplate: '<span>#= Code #</span><span style="float:right;">#= CustomerName #</span>',
        headerTemplate: '<strong><span> Mã khách hàng </span><span style="float:right;"> Tên khách hàng </span></strong>',
        change: function (e) {
            $scope.CATSeaPort_ListCustomer = this.value();
            // if ($scope.CATSeaPort_ListCustomer.length>0) 
            $scope.CreateGrid('cus')
            // else $scope.CreateGrid('new')
        }
    }

    Common.Services.Call($http, {
        url: Common.Services.url.CAT,
        method: _CATSeaPort.URL.Read_Customer,
        data: {},
        success: function (res) {
            if (Common.HasValue(res)) {

                _CATSeaPort.Data.DataCustomer = res.Data;
                $scope.CATSeaPort_mts_CustomerOption.dataSource.data(res.Data);
            }
        }
    });

    $scope.Main_Columns = [];

    angular.forEach(_CATSeaPort.Data.Main_Columns, function (item, idx) {
        $scope.Main_Columns.push(item)
    })

    //$scope.CATSeaPort_MainGrid_Options = {
    //    dataSource: Common.DataSource.Local({
    //        data: [],
    //        model: {},
    //        pageSize:20,
    //    }),
    //    height: '100%', pageable: true, sortable: true, columnMenu: false, resizable: true, reorderable: true, filterable: { mode: 'row' },editable:false,
    //    columns: []
    //};

    $scope.CATSeaPort_CreateCode_Click = function ($event, grid) {
        $event.preventDefault();
        $timeout(function () {
            $scope.IsEditCode = true;
            var tr = $event.target.closest('tr');

            $scope.CATSeaPortCodeEdit = grid.dataItem(tr);
            _CATSeaPort.Data.ItemBackUp = $.extend(true, {}, $scope.CATSeaPortCodeEdit);
            grid.editRow(tr);
        }, 1)
    }

    $scope.CATSeaPort_SaveCode = function ($event, grid) {
        $event.preventDefault();

        $rootScope.IsLoading = true;
        $scope.IsEditCode = false;
        //todo code
        $scope.CATSeaPortCodeEdit.lstPartnerLocation = [];
        Common.Data.Each($scope.CATSeaPort_ListCustomer, function (o) {
            var fieldname = 'CUS' + o;
            $scope.CATSeaPortCodeEdit.lstPartnerLocation.push({ Code: $scope.CATSeaPortCodeEdit[fieldname], CustomerID: o })
        })

        Common.Services.Call($http, {
            url: Common.Services.url.CAT,
            method: _CATSeaPort.URL.UpdateListCode_SeaPort,
            data: { item: $scope.CATSeaPortCodeEdit },
            success: function (res) {
                Common.Services.Error(res, function (res) {
                    $rootScope.Message({
                        Type: Common.Message.Type.Notify,
                        NotifyType: Common.Message.NotifyType.SUCCESS,
                        Title: 'Thông báo',
                        Msg: 'Thành công',
                        Ok: null,
                        close: null,
                    })
                    $scope.CreateGrid('cus');
                })
            }
        });

        $rootScope.IsLoading = false;
    }

    $scope.CATSeaPort_CancelCode = function ($event, grid) {
        $event.preventDefault();

        $rootScope.IsLoading = true;
        $scope.IsEditCode = false;
        var i = 0;
        var data = grid.dataSource.data();
        while (i < data.length) {
            var obj = data[i];
            if (obj.myID == _CATSeaPort.Data.ItemBackUp.myID) {
                data.splice(i, 1);
                data.splice(i, 0, $.extend(true, {}, _CATSeaPort.Data.ItemBackUp))
            }
            i++;
        }
        $rootScope.IsLoading = false;
    }

    

    $scope.CreateGrid = function (status) {
        Common.Services.Call($http, {
            url: Common.Services.url.CAT,
            method: _CATSeaPort.URL.Read_SeaPort,
            data: {},
            success: function (res) {
                _CATSeaPort.Data.SeaPort = []
                if (Common.HasValue(res)) {
                    _CATSeaPort.Data.SeaPort = res.Data;
                    switch (status) {
                        default: break;
                        case 'new':
                            $rootScope.IsLoading = true;
                            $scope.HasCustomer = false;
                            var data = [];
                            var columns = [];

                            angular.forEach(_CATSeaPort.Data.Main_Columns, function (item, idx) {
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
                            var lstSeaPort = $.extend(true, [], _CATSeaPort.Data.SeaPort)
                            var myID = 1;
                            angular.forEach(lstSeaPort, function (item, idx) {
                                item['myID'] = myID;
                                data.push(item);
                                myID++;
                                angular.forEach(item.lstPartnerLocation, function (itemLo, index) {
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
                                })
                            })

                            var options = {
                                dataSource: Common.DataSource.Local({
                                    data: data,
                                    model: model,
                                    pageSize: 20,
                                }),
                                height: '100%', pageable: true, sortable: true, columnMenu: false, resizable: true, reorderable: true, filterable: { mode: 'row' }, editable: { mode: 'inline' },
                                columns: columns
                            }

                            $scope.CATSeaPort_MainGrid.setOptions(options)

                            $rootScope.IsLoading = false;
                            break;
                        case 'cus':
                            $rootScope.IsLoading = true;
                            $scope.HasCustomer = false;
                            if ($scope.CATSeaPort_ListCustomer.length > 0) $scope.HasCustomer = true;
                            
                            Common.Services.Call($http, {
                                url: Common.Services.url.CAT,
                                method: _CATSeaPort.URL.Get_CustomerCode,
                                data: { lst: $scope.CATSeaPort_ListCustomer },
                                success: function (res) {
                                    //res= data
                                    if (Common.HasValue(res)) {

                                        var NewModel = {
                                            fields: {
                                                ID: { type: 'number' },
                                                PartnerName: { type: 'string', editable: false },
                                                Address: { type: 'string', editable: false },
                                                WardName: { type: 'string', editable: false },
                                                DistrictName: { type: 'string', editable: false },
                                                ProvinceName: { type: 'string', editable: false },
                                                CountryName: { type: 'string', editable: false },
                                                GroupOfPartnerName: { type: 'string', editable: false },
                                                Code: { type: 'string', editable: false }
                                            }
                                        }
                                        var columns = [];
                                        angular.forEach(_CATSeaPort.Data.Main_Columns, function (item, idx) {
                                            columns.push(item)
                                        })

                                        angular.forEach($scope.CATSeaPort_ListCustomer, function (cus, idx) {
                                            var checkCus = $.grep(_CATSeaPort.Data.DataCustomer, function (o) { return o.ID == cus; })
                                            if (checkCus.length > 0) {
                                                var fieldname = 'CUS' + checkCus[0].ID;
                                                NewModel.fields[fieldname] = { type: 'string', editable: true };
                                                NewModel.fields[fieldname + "_dirty"] = { type: 'boolean', defaultValue: false };
                                                var col = {
                                                    field: fieldname, title: checkCus[0].Code, width: 120, locked: false, hidden: false,
                                                    filterable: { cell: { operator: 'contains', showOperators: false } },
                                                    editor: '<input class="k-textbox" data-bind="value:' + fieldname + '"  ng-model="CATSeaPortCodeEdit.' + fieldname + '" />'
                                                };
                                                columns.push(col);
                                            }
                                        })

                                        // $scope.CATSeaPort_MainGrid_Options.columns = columns;
                                        // $scope.CATSeaPort_MainGrid_Options.editable = 'incell';
                                        // $scope.CATSeaPort_MainGrid_Options.dataSource.model = NewModel;

                                        var data = [];

                                        var lstSeaPort = $.extend(true, [], _CATSeaPort.Data.SeaPort)
                                        var myID = 1;
                                        angular.forEach(lstSeaPort, function (SeaPort, idx) {
                                            angular.forEach($scope.CATSeaPort_ListCustomer, function (cus, idx) {
                                                var checkCus = $.grep(res.lstPartner, function (o) { return o.CustomerID == cus && o.ID == SeaPort.PartnerID; })
                                                if (checkCus.length > 0) SeaPort['CUS' + checkCus[0].CustomerID] = checkCus[0].Code;
                                            })
                                            SeaPort['myID'] = myID;
                                            data.push(SeaPort);
                                            myID++;
                                            angular.forEach(SeaPort.lstPartnerLocation, function (itemLo, idx) {
                                                var itemPush = {
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
                                                }
                                                angular.forEach($scope.CATSeaPort_ListCustomer, function (cus, idx) {
                                                    var checkCus = $.grep(res.lstLocation, function (o) { return o.CustomerID == cus && o.CATPartnerID == SeaPort.PartnerID && o.LocationID == itemLo.ID })
                                                    if (checkCus.length > 0) itemPush['CUS' + checkCus[0].CustomerID] = checkCus[0].Code;
                                                })
                                                data.push(itemPush);
                                                myID++;
                                            })
                                        })

                                        // $scope.CATSeaPort_MainGrid_Options.dataSource.data(data)

                                        var options = {
                                            dataSource: Common.DataSource.Local({
                                                data: data,
                                                model: NewModel,
                                                pageSize: 20,
                                            }),
                                            height: '100%', pageable: true, sortable: true, columnMenu: false, resizable: true, reorderable: true, filterable: { mode: 'row' }, editable: { mode: 'inline' },
                                            columns: columns
                                        }

                                        $scope.CATSeaPort_MainGrid.setOptions(options)
                                    }
                                    $rootScope.IsLoading = false;
                                }
                            });
                            break;

                    }
                }
            }
        });
        
    }

    $scope.CreateGrid('new');

    //#region  popup main

    $scope.CATSeaPort_TabOptions = { animation: { open: { effects: "fadeIn" } } }

    $scope.SeaPort_cboGroupOfPartnerOptions = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true, dataTextField: 'GroupName', dataValueField: 'ID', minLength: 3,
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    GroupName: { type: 'string' },
                }
            }
        })
    }

    Common.ALL.Get($http, {
        url: Common.ALL.URL.CATGroupOfPartner,
        success: function (data) {

            $scope.SeaPort_cboGroupOfPartnerOptions.dataSource.data(data);
        }
    })

    $scope.SeaPort_cboCountryOptions = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true, dataTextField: 'CountryName', dataValueField: 'ID', minLength: 3,
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
                $scope.SeaPortItem.ProvinceID = "";
                $scope.SeaPortItem.DistrictID = "";
                $scope.SeaPortItem.WardID = "";
                $scope.LoadRegionDataGeneralTab($scope.SeaPortItem);
            }
        }
    }


    $scope.SeaPort_cboProvinceOptions = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true, dataTextField: 'ProvinceName', dataValueField: 'ID', index: 0,
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
                $scope.SeaPortItem.DistrictID = "";
                $scope.SeaPortItem.WardID = "";
                $scope.LoadRegionDataGeneralTab($scope.SeaPortItem);
            }
        }
    }
    $scope.SeaPort_cboDistrictOptions = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true, dataTextField: 'DistrictName', dataValueField: 'ID',
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
                //$scope.SeaPortItem.DistrictID = "";
                $scope.SeaPortItem.WardID = "";
                $scope.LoadRegionDataGeneralTab($scope.SeaPortItem);
            }
        }
    }
    $scope.SeaPort_cboWardOptions = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true, dataTextField: 'WardName', dataValueField: 'ID',
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    WardName: { type: 'string' },
                }
            }
        })
    }


    Common.ALL.Get($http, {
        url: Common.ALL.URL.Province,
        success: function (data) {
            _CATSeaPort.Data.Province = {};
            angular.forEach(data, function (obj, indx) {
                if (Common.HasValue(_CATSeaPort.Data.Province[obj.CountryID]))
                    _CATSeaPort.Data.Province[obj.CountryID].push(obj);
                else _CATSeaPort.Data.Province[obj.CountryID] = [obj];
            })
        }
    })
    Common.ALL.Get($http, {
        url: Common.ALL.URL.District,
        success: function (data) {
            _CATSeaPort.Data.District = {};
            angular.forEach(data, function (obj, indx) {
                if (Common.HasValue(_CATSeaPort.Data.District[obj.ProvinceID]))
                    _CATSeaPort.Data.District[obj.ProvinceID].push(obj);
                else _CATSeaPort.Data.District[obj.ProvinceID] = [obj];
            })
        }
    })
    Common.ALL.Get($http, {
        url: Common.ALL.URL.Ward,
        success: function (data) {
            _CATSeaPort.Data.Ward = {};
            angular.forEach(data, function (obj, indx) {
                if (Common.HasValue(_CATSeaPort.Data.Ward[obj.DistrictID]))
                    _CATSeaPort.Data.Ward[obj.DistrictID].push(obj);
                else _CATSeaPort.Data.Ward[obj.DistrictID] = [obj];
            })
        }
    })
    $scope.LoadRegionDataGeneralTab = function (item) {
        Common.Log("LoadRegionDataGeneralTab");
        try {
            var countryID = item.CountryID;
            var provinceID = item.ProvinceID;
            var districtID = item.DistrictID;
            var wardID = item.WardID;

            var data = _CATSeaPort.Data.Province[countryID];
            $scope.SeaPort_cboProvinceOptions.dataSource.data(data);
            if (provinceID < 1)
                provinceID = data[0].ID;
            $timeout(function () {
                item.ProvinceID = provinceID;
            }, 1)

            data = _CATSeaPort.Data.District[provinceID];
            $scope.SeaPort_cboDistrictOptions.dataSource.data(data);
            if (districtID < 1)
                districtID = data[0].ID;
            $timeout(function () {
                item.DistrictID = districtID;
            }, 1)

            data = _CATSeaPort.Data.Ward[districtID];
            $scope.SeaPort_cboWardOptions.dataSource.data(data);
            if (wardID < 1 && data.length > 0)
                wardID = data[0].ID;
            $timeout(function () {
                item.WardID = wardID;
            }, 1)
        }
        catch (e) { }
    }

    $scope.CATSeaPort_SaveClick = function ($event, win, vform) {
        $event.preventDefault();
        if (vform()) {
            $scope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.CAT,
                method: _CATSeaPort.URL.Update_SeaPort,
                data: { item: $scope.SeaPortItem, partnerid: $scope.SeaPortPartnerID },
                success: function (res) {
                    
                    $rootScope.Message({
                        Type: Common.Message.Type.Notify,
                        NotifyType: Common.Message.NotifyType.SUCCESS,
                        Title: 'Thông báo',
                        Msg: 'Thành công',
                        Ok: null,
                        close: null,
                    })
                    $scope.CreateGrid('cus')
                    
                    $scope.SeaPortPartnerID = res;
                    Common.Services.Call($http, {
                        url: Common.Services.url.CAT,
                        method: _CATSeaPort.URL.Get_SeaPort,
                        data: { id: res },
                        success: function (res2) {

                            $scope.IsShowTab2 = false;

                            if (res > 0) { $scope.IsShowTab2 = true; }

                            $scope.SeaPortItem = res2;
                            $scope.LoadRegionDataGeneralTab($scope.SeaPortItem);
                            $scope.CATSeaPort_Location_GridOptions.dataSource.read();

                            $scope.CATSeaPort_Tab.select(0);
                            $scope.IsLoading = false;
                        }
                    })
                }
            });
        }
    }

    $scope.CATSeaPort_DeleteClick = function ($event, win) {
        $event.preventDefault();
        Common.Services.Call($http, {
            url: Common.Services.url.CAT,
            method: _CATSeaPort.URL.Delete_SeaPort,
            data: { item: $scope.SeaPortItem },
            success: function (res) {
                $rootScope.Message({
                    Type: Common.Message.Type.Notify,
                    NotifyType: Common.Message.NotifyType.SUCCESS,
                    Title: 'Thông báo',
                    Msg: 'Thành công',
                    Ok: null,
                    close: null,
                })
                win.close()
                $scope.CreateGrid('new')
            }
        });
    }

    $scope.CATSeaPort_CloseClick = function ($event, win) {
        $event.preventDefault();
        win.close();
    }
    //tab2
    $scope.CATSeaPort_Location_GridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.CAT,
            method: _CATSeaPort.URL.Read_Location,
            readparam: function () { return { partnerid: $scope.SeaPortPartnerID } },
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
                title: ' ', width: '90px', filterable: false, sortable: false,
                template: '<a href="/" ng-click="CATSeaPort_Location_EditClick($event,CATSeaPort_Location_win,CATSeaPort_Location_Grid)" class="k-button"><i class="fa fa-pencil"></i></a>' +
                    '<a href="/" ng-click="CATSeaPort_Location_DestroyClick($event,CATSeaPort_Location_Grid)" class="k-button"><i class="fa fa-times"></i></a>',
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

    $scope.CATSeaPort_Location_EditClick = function myfunction($event, win, grid) {
        $event.preventDefault();
        var tr = $($event.target).closest('tr');
        var id = 0;
        var item = grid.dataItem(tr);
        if (Common.HasValue(item)) id = item.ID;
        $scope.LoadLocationItem(win, id);
    }

    $scope.CATSeaPort_Location_DestroyClick = function ($event, grid) {
        $event.preventDefault();
        var tr = $($event.target).closest('tr');
        var item = grid.dataItem(tr);
        if (Common.HasValue(item)) {
            if (confirm('Bạn muốn xóa các dữ liệu đã chọn ?')) {
                Common.Services.Call($http, {
                    url: Common.Services.url.CAT,
                    method: _CATSeaPort.URL.Delete_Location,
                    data: { 'item': item },
                    success: function (res) {
                        $scope.CATSeaPort_Location_GridOptions.dataSource.read();
                    }
                });
            }
        }
    }

    $scope.CATSeaPort_Location_AddClick = function ($event, win) {
        $event.preventDefault();
        $scope.LoadLocationItem(win, 0);
    }

    $scope.LoadLocationItem = function (win, id) {
        Common.Services.Call($http, {
            url: Common.Services.url.CAT,
            method: _CATSeaPort.URL.Get_Location,
            data: { 'id': id },
            success: function (res) {
                $scope.SeaPortLocationItem = res;
                $scope.LoadRegionDataLocationTab($scope.SeaPortLocationItem);
                win.center();
                win.open();
            }
        });
    }

    $scope.CATSeaPort_Location_SearchClick = function ($event, win) {
        $event.preventDefault()

        win.center();
        win.open();
        $scope.CATSeaPort_LocationSearch_GridOptions.dataSource.read();
    }

    //#endregion

    //#region popup edit location
    $scope.SeaPortLocation_cboCountryOptions = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true, dataTextField: 'CountryName', dataValueField: 'ID',
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
                $scope.SeaPortLocationItem.ProvinceID = "";
                $scope.SeaPortLocationItem.DistrictID = "";
                $scope.SeaPortLocationItem.WardID = "";
                $scope.LoadRegionDataLocationTab($scope.SeaPortLocationItem);
            }
        }
    }

    Common.ALL.Get($http, {
        url: Common.ALL.URL.Country,
        success: function (data) {

            $scope.SeaPort_cboCountryOptions.dataSource.data(data);
            $scope.SeaPortLocation_cboCountryOptions.dataSource.data(data)
            _CATSeaPort.Data.Country = data;
        }
    })

    $scope.SeaPortLocation_cboProvinceOptions = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true, dataTextField: 'ProvinceName', dataValueField: 'ID',
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
                $scope.SeaPortLocationItem.DistrictID = "";
                $scope.SeaPortLocationItem.WardID = "";
                $scope.LoadRegionDataLocationTab($scope.SeaPortLocationItem);
            }
        }
    }
    $scope.SeaPortLocation_cboDistrictOptions = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true, dataTextField: 'DistrictName', dataValueField: 'ID',
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
                $scope.SeaPortLocationItem.WardID = "";
                $scope.LoadRegionDataLocationTab($scope.SeaPortLocationItem);
            }
        }
    }
    $scope.SeaPortLocation_cboWardOptions = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true, dataTextField: 'WardName', dataValueField: 'ID', minLength: 3,
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    WardName: { type: 'string' },
                }
            }
        })
    }
    $scope.LoadRegionDataLocationTab = function (item) {
        Common.Log("LoadRegionDataLocationTab");
        try {
            var countryID = item.CountryID;
            var provinceID = item.ProvinceID;
            var districtID = item.DistrictID;
            var wardID = item.WardID;

            var data = _CATSeaPort.Data.Province[countryID];
            $scope.SeaPortLocation_cboProvinceOptions.dataSource.data(data);
            if (provinceID < 1)
                provinceID = data[0].ID;
            $timeout(function () {
                item.ProvinceID = provinceID;
            }, 1)

            data = _CATSeaPort.Data.District[provinceID];
            $scope.SeaPortLocation_cboDistrictOptions.dataSource.data(data);
            if (districtID < 1)
                districtID = data[0].ID;
            $timeout(function () {
                item.DistrictID = districtID;
            }, 1)

            data = _CATSeaPort.Data.Ward[districtID];
            $scope.SeaPortLocation_cboWardOptions.dataSource.data(data);
            if (wardID < 1 && data.length > 0)
                wardID = data[0].ID;
            $timeout(function () {
                item.WardID = wardID;
            }, 1)
        }
        catch (e) { }
    }
    $scope.SeaPortLocation_numLatOptions = { format: '#,##0.00000', spinners: false, culture: 'en-US', min: 0, step: 1, decimals: 5, }
    $scope.SeaPortLocation_numLngOptions = { format: '#,##0.00000', spinners: false, culture: 'en-US', min: 0, step: 1, decimals: 5, }
    $scope.CATSeaPort_Location_SaveClick = function ($event, win, vform) {
        $event.preventDefault();

        if (vform()) {
            Common.Services.Call($http, {
                url: Common.Services.url.CAT,
                method: _CATSeaPort.URL.Update_Location,
                data: { item: $scope.SeaPortLocationItem, partnerid: $scope.SeaPortPartnerID },
                success: function (res) {
                    $rootScope.Message({
                        Type: Common.Message.Type.Notify,
                        NotifyType: Common.Message.NotifyType.SUCCESS,
                        Title: 'Thông báo',
                        Msg: 'Thành công',
                        Close: null,
                        Ok: null
                    })

                    win.close();
                    $scope.CATSeaPort_Location_GridOptions.dataSource.read();

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
    }
    $scope.CATSeaPort_Location_CloseClick = function ($event,win) {
        $event.preventDefault();
        win.close();
    }
    //#endregion

    //#region popup search
    $scope.CATSeaPort_LocationSearch_GridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.CAT,
            method: _CATSeaPort.URL.Read_LocationNotIn,
            readparam: function () { return { partnerid: $scope.SeaPortPartnerID } },
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
        height: '100%', pageable: true, sortable: true, columnMenu: false, resizable: true, reorderable: true, filterable: {mode:'row'},
        columns: [
            {
                title: ' ', width: '100px',
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,CATSeaPort_LocationSearch_Grid,CATSeaPort_LocationSearch_ChooseChange)" />',
                headerAttributes: { style: 'text-align: center;' },
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,CATSeaPort_LocationSearch_Grid,CATSeaPort_LocationSearch_ChooseChange)" />',
                filterable: false, sortable: false
            },
            { field: 'Location', title: '{{RS.CATLocation.Location}}', width: 150 },
            { field: 'PartnerCode', title: '{{RS.CATLocation.Code}}', width: 150 },
            { field: 'Address', title: '{{RS.CATLocation.Address}}', width: 150 },
            { field: 'CountryName', title: '{{RS.CATCountry.CountryName}}', width: 150 },
            { field: 'ProvinceName', title: '{{RS.CATProvince.ProvinceName}}', width: 150 },
            { field: 'DistrictName', title: '{{RS.CATDistrict.DistrictName}}', width: 150 },
            { field: 'WardName', title: '{{RS.CATWard.WardName}}', width: 150 },
            { field: 'Lat', title: '{{RS.CATLocation.Lat}}', width: 100 },
             { field: 'Lng', title: '{{RS.CATLocation.Lng}}', width: 100 },
        ]
    }
    $scope.CATSeaPort_LocationSearch_ChooseChange = function ($event, grid, haschoose) {
        $scope.HasChoose = haschoose;
    };

    $scope.CATSeaPort_LocationSearch_ChooseClick = function ($event, win, grid) {
        $event.preventDefault()
        var data = grid.dataSource.data();
        var dataSend = $.grep(data, function (o) { return o.IsChoose == true })
        if (dataSend.length > 0) {
            Common.Services.Call($http, {
                url: Common.Services.url.CAT,
                method: _CATSeaPort.URL.Update_LocationNotIn,
                data: { lst: dataSend, partnerid: $scope.SeaPortPartnerID },
                success: function (res) {
                    $rootScope.Message({
                        Type: Common.Message.Type.Notify,
                        NotifyType: Common.Message.NotifyType.SUCCESS,
                        Title: 'Thông báo',
                        Msg: 'Thành công',
                        Close: null,
                        Ok: null
                    })
                     
                    $scope.CATSeaPort_Location_GridOptions.dataSource.read();
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
    $scope.CATSeaPort_LocationSearch_CloseClick = function ($event, win) {
        $event.preventDefault();
        win.close();
    }
    //#endregion

    //#region Action
    $scope.ShowSetting = function ($event, grid) {
        $event.preventDefault();

        $rootScope.ShowSetting({
            ListView: [],
            event: $event,
            grid: grid,
            current: $state.current
        });
    };
    //#endregion


}]);