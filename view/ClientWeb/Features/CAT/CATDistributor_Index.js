

/// <reference path="~/Scripts/jquery-1.9.1.intellisense.js" />
/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />

var _CATDistributor = {
    URL: {
        Read_Customer: 'CATDistributor_Customer_Read',
        Read_Distributor: 'CATDistributor_DistributorCustom_Read',
        Get_Distributor: 'CATDistributor_Distributor_Get',
        Update_Distributor: 'CATDistributor_Distributor_Update',
        Delete_Distributor: 'CATDistributor_Distributor_Destroy',
        UpdateListCode_Distributor: 'CATDistributor_DistributorCustom_SaveList',
        Read_Location: 'CATDistributor_Location_Read',
        Update_Location: 'CATDistributor_Location_Update',
        Get_Location: 'CATDistributor_Location_Get',
        Delete_Location: 'CATDistributor_Location_Destroy',
        Read_LocationNotIn: 'CATDistributor_LocationNotIn_Read',
        Update_LocationNotIn: 'CATDistributor_LocationNotIn_SaveList',
        Get_CustomerCode: 'CATDistributor_DistributorCustomer_List',
        Excel_Export: 'CATDistributor_Export_GetData',
        Excel_Import: 'CATDistributor_Excel_Save',
        Excel_Check: 'CATDistributor_Excel_Check',
        Excel_ExportCode: 'CATDistributor_Excel_ExportCode',
        Excel_ImportCode: 'CATDistributor_Excel_SaveCode',
        Excel_CheckCode: 'CATDistributor_Excel_CheckCode',
    },
    Data: {
        Country: [],
        Province: [],
        District: [],
        Ward: [],
        ItemBackUp: null,
        Distributor: [],
        DataCustomer: [],
        Main_Columns: [
            {
                title: ' ', width: '90px', filterable: false, sortable: false, locked: true,
                template:
                    '<a ng-show="!IsEditCode&&HasCustomer" href="/" ng-click="CATDistributor_CreateCode_Click($event,CATDistributor_MainGrid)" class="k-button"><i class="fa fa-cog"></i></a>' +
                    '<a href="/" ng-click="CATDistributor_EditClick($event,CATDistributor_win,CATDistributor_MainGrid)" ng-show="dataItem.IsPartner&&!IsEditCode" class="k-button"><i class="fa fa-pencil"></i></a>' +
                    '<a ng-show="IsEditCode && CATDistributorCodeEdit.myID==#=myID#?true:false&&HasCustomer" href="/" ng-click="CATDistributor_SaveCode($event,CATDistributor_MainGrid)" class="k-button"><i class="fa fa-check"></i></a>' +
                    '<a ng-show="IsEditCode && CATDistributorCodeEdit.myID==#=myID#?true:false&&HasCustomer" href="/" ng-click="CATDistributor_CancelCode($event,CATDistributor_MainGrid)" class="k-button"><i class="fa fa-ban"></i></a>',
            },
            {
                field: 'GroupOfPartnerName', title: 'Loại NPP', template: '#=IsPartner?"Nhà phân phối":""#', width: 100, locked: true,
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            { field: 'PartnerName', title: '{{RS.CATPartner.PartnerName}}', width: '125px', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'Address', title: '{{RS.CATPartner.Address}}', width: '250px', filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'Code', title: '{{RS.CATPartner.Code}}', width: '100px', filterable: { cell: { operator: 'contains', showOperators: false } } },
        ]
    },
}

angular.module('myapp').controller('CATDistributor_IndexCtrl', ['$rootScope', '$scope', '$http', '$location', '$state', '$timeout', function ($rootScope, $scope, $http, $location, $state, $timeout) {
    if ($rootScope.IsPageComplete != true) return;
    Common.Log('CATDistributor_IndexCtrl');
    $rootScope.IsLoading = false;

    $scope.Auth = $rootScope.GetAuth();

    $scope.DistributorID = 0;
    $scope.DistributorItem = null;
    $scope.DistributorLocationItem = null;
    $scope.HasChoose = false;
    $scope.IsEditCode = false;
    $scope.HasCustomer = false;

    $scope.CATDistributorCodeEdit = null;

    $scope.DistributorPartnerID = 0;

    $scope.CATDistributor_ListCustomer = [];

    $scope.CATDistributor_AddClick = function ($event, win) {
        $event.preventDefault();
        $scope.DistributorID = 0;
        $scope.LoadItem(win, 0);
    }

    $scope.CATDistributor_EditClick = function ($event, win, grid) {
        $event.preventDefault();
        var tr = $($event.target).closest('tr');
        var id = 0;
        var item = grid.dataItem(tr);
        if (Common.HasValue(item)) id = item.PartnerID;
        $scope.LoadItem(win, id);
    }

    $scope.CATDistributorExcel_Click = function ($event, win) {
        $event.preventDefault();
        $rootScope.UploadExcel({
            Path: Common.FolderUpload.Import,
            columns: [
                { field: 'Code', width: 100, title: '{{RS.CATDistributor.Code}}', filterable: { cell: { showOperators: false, operator: "contains" } } }
            ],
            Download: function () {
                $rootScope.IsLoading = true;
                Common.Services.Call($http, {
                    url: Common.Services.url.CAT,
                    method: _CATDistributor.URL.Excel_Export,
                    data: {},
                    success: function (res) {
                        $rootScope.IsLoading = false;
                        $rootScope.DownloadFile(res);
                    }
                })
            },
            Upload: function (e, callback) {
                $rootScope.IsLoading = true;
                Common.Services.Call($http, {
                    url: Common.Services.url.CAT,
                    method: _CATDistributor.URL.Excel_Check,
                    data: { item: e },
                    success: function (data) {
                        $rootScope.IsLoading = false;
                        $rootScope.Message({ Title: 'Thông báo', Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS })
                        callback(data);
                    }
                })
            },
            Window: win,
            Complete: function (e, data) {
                $rootScope.IsLoading = true;
                Common.Services.Call($http, {
                    url: Common.Services.url.CAT,
                    method: _CATDistributor.URL.Excel_Import,
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

    $scope.CATDistributorExcelCode_Click = function ($event, win) {
        $event.preventDefault();
        $rootScope.UploadExcel({
            Path: Common.FolderUpload.Import,
            columns: [
                { field: 'SysCode', width: 120, title: '{{RS.CATDistributor.SysCode}}', filterable: { cell: { showOperators: false, operator: "contains" } } }
            ],
            Download: function () {

                if (Common.HasValue($scope.CATDistributor_ListCustomer) && $scope.CATDistributor_ListCustomer.length > 0) {
                    Common.Services.Call($http, {
                        url: Common.Services.url.CAT,
                        method: _CATDistributor.URL.Excel_ExportCode,
                        data: { lst: $scope.CATDistributor_ListCustomer },
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
                    method: _CATDistributor.URL.Excel_CheckCode,
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
                    method: _CATDistributor.URL.Excel_ImportCode,
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
                        $rootScope.IsLoading = false;

                        $scope.CreateGrid('cus')
                    }
                })
            }
        })
    }

    $scope.CATDistributor_mts_CancelClick = function ($event) {
        $event.preventDefault();
        if ($scope.CATDistributor_ListCustomer.length > 0) {
            $scope.CATDistributor_ListCustomer = [];
            $scope.CreateGrid('cus')
        }
    }

    $scope.LoadItem = function (win, id) {
        $scope.DistributorPartnerID = id;
        Common.Services.Call($http, {
            url: Common.Services.url.CAT,
            method: _CATDistributor.URL.Get_Distributor,
            data: { id: id },
            success: function (res) {

                $scope.IsShowTab2 = false;

                if (id > 0) { $scope.IsShowTab2 = true; }

                $scope.CreateDataSourceComboBox('Province', res.CountryID, status)
                $scope.CreateDataSourceComboBox('District', res.ProvinceID, status)
                $scope.DistributorItem = res;
                $scope.CATDistributor_Location_GridOptions.dataSource.read();

                $scope.CATDistributor_Tab.select(0);
                win.center();
                win.open()
            }
        })
    }

    $scope.CATDistributor_mts_CustomerOption = {
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
            $scope.CATDistributor_ListCustomer = this.value();
            // if ($scope.CATDistributor_ListCustomer.length>0) 
            $scope.CreateGrid('cus')
            // else $scope.CreateGrid('new')
        }
    }

    Common.Services.Call($http, {
        url: Common.Services.url.CAT,
        method: _CATDistributor.URL.Read_Customer,
        data: {},
        success: function (res) {
            if (Common.HasValue(res)) {

                _CATDistributor.Data.DataCustomer = res.Data;
                $scope.CATDistributor_mts_CustomerOption.dataSource.data(res.Data);
            }
        }
    });

    $scope.Main_Columns = [];

    angular.forEach(_CATDistributor.Data.Main_Columns, function (item, idx) {
        $scope.Main_Columns.push(item)
    })


    $scope.CATDistributor_CreateCode_Click = function ($event, grid) {
        $event.preventDefault();
        $timeout(function () {
            $scope.IsEditCode = true;
            var tr = $event.target.closest('tr');

            $scope.CATDistributorCodeEdit = grid.dataItem(tr);
            _CATDistributor.Data.ItemBackUp = $.extend(true, {}, $scope.CATDistributorCodeEdit);
            grid.editRow(tr);
        }, 1)
    }

    $scope.CATDistributor_SaveCode = function ($event, grid) {
        $event.preventDefault();
        $rootScope.IsLoading = true;
        $scope.IsEditCode = false;
        //todo code
        $scope.CATDistributorCodeEdit.lstPartnerLocation = [];
        Common.Data.Each($scope.CATDistributor_ListCustomer, function (o) {
            var fieldname = 'CUS' + o;
            $scope.CATDistributorCodeEdit.lstPartnerLocation.push({ Code: $scope.CATDistributorCodeEdit[fieldname], CustomerID: o })
        })

        Common.Services.Call($http, {
            url: Common.Services.url.CAT,
            method: _CATDistributor.URL.UpdateListCode_Distributor,
            data: { item: $scope.CATDistributorCodeEdit },
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

    $scope.CATDistributor_CancelCode = function ($event, grid) {
        $event.preventDefault();

        $rootScope.IsLoading = true;
        $scope.IsEditCode = false;
        var i = 0;
        var data = grid.dataSource.data();
        while (i < data.length) {
            var obj = data[i];
            if (obj.myID == _CATDistributor.Data.ItemBackUp.myID) {
                data.splice(i, 1);
                data.splice(i, 0, $.extend(true, {}, _CATDistributor.Data.ItemBackUp))
            }
            i++;
        }
        $rootScope.IsLoading = false;
    }

    Common.Services.Call($http, {
        url: Common.Services.url.CAT,
        method: _CATDistributor.URL.Read_Distributor,
        data: {},
        success: function (res) {

            _CATDistributor.Data.Distributor = []
            if (Common.HasValue(res)) {
                _CATDistributor.Data.Distributor = res.Data;
                $scope.CreateGrid('new');
            }
        }
    });

    $scope.CreateGrid = function (status) {
        $rootScope.IsLoading = true;
        Common.Services.Call($http, {
            url: Common.Services.url.CAT,
            method: _CATDistributor.URL.Read_Distributor,
            data: {},
            success: function (res) {

                _CATDistributor.Data.Distributor = []
                if (Common.HasValue(res)) {
                    _CATDistributor.Data.Distributor = res.Data;

                    switch (status) {
                        default: break;
                        case 'new':
                            $rootScope.IsLoading = true;
                            $scope.HasCustomer = false;
                            var data = [];
                            var columns = [];

                            angular.forEach(_CATDistributor.Data.Main_Columns, function (item, idx) {
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
                            var lstDistributor = $.extend(true, [], _CATDistributor.Data.Distributor)
                            var myID = 1;

                            for (var i = 0; i < lstDistributor.length; i++) {
                                var item = lstDistributor[i];
                                item['myID'] = myID;
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
                                height: '100%', pageable: true, sortable: true, columnMenu: false, resizable: true, reorderable: true, filterable: { mode: 'row' }, editable: { mode: 'inline' },
                                columns: columns
                            }
                            //debugger
                            $scope.CATDistributor_MainGrid_Options = options;


                            $rootScope.IsLoading = false;
                            break;
                        case 'cus':
                            $rootScope.IsLoading = true;
                            $scope.HasCustomer = false;
                            if ($scope.CATDistributor_ListCustomer.length > 0) $scope.HasCustomer = true;
                            Common.Services.Call($http, {
                                url: Common.Services.url.CAT,
                                method: _CATDistributor.URL.Get_CustomerCode,
                                data: { lst: $scope.CATDistributor_ListCustomer },
                                success: function (res) {
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
                                        angular.forEach(_CATDistributor.Data.Main_Columns, function (item, idx) {
                                            columns.push(item)
                                        })

                                        angular.forEach($scope.CATDistributor_ListCustomer, function (cus, idx) {
                                            var checkCus = $.grep(_CATDistributor.Data.DataCustomer, function (o) { return o.ID == cus; })
                                            if (checkCus.length > 0) {
                                                var fieldname = 'CUS' + checkCus[0].ID;
                                                NewModel.fields[fieldname] = { type: 'string', editable: true };
                                                NewModel.fields[fieldname + "_dirty"] = { type: 'boolean', defaultValue: false };
                                                var col = {
                                                    field: fieldname, title: checkCus[0].Code, width: 120, locked: false, hidden: false,
                                                    filterable: { cell: { operator: 'contains', showOperators: false } },
                                                    editor: '<input class="k-textbox" data-bind="value:' + fieldname + '"  ng-model="CATDistributorCodeEdit.' + fieldname + '" />'
                                                };
                                                columns.push(col);
                                            }
                                        })

                                        var data = [];

                                        var lstDistributor = $.extend(true, [], _CATDistributor.Data.Distributor)
                                        var myID = 1;
                                        angular.forEach(lstDistributor, function (distributor, idx) {
                                            angular.forEach($scope.CATDistributor_ListCustomer, function (cus, idx) {
                                                var checkCus = $.grep(res.lstPartner, function (o) { return o.CustomerID == cus && o.ID == distributor.PartnerID; })
                                                if (checkCus.length > 0) distributor['CUS' + checkCus[0].CustomerID] = checkCus[0].Code;
                                            })
                                            distributor['myID'] = myID;
                                            data.push(distributor);
                                            myID++;
                                            angular.forEach(distributor.lstPartnerLocation, function (itemLo, idx) {
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
                                                angular.forEach($scope.CATDistributor_ListCustomer, function (cus, idx) {
                                                    var checkCus = $.grep(res.lstLocation, function (o) { return o.CustomerID == cus && o.CATPartnerID == distributor.PartnerID && o.LocationID == itemLo.ID })
                                                    if (checkCus.length > 0) itemPush['CUS' + checkCus[0].CustomerID] = checkCus[0].Code;
                                                })
                                                data.push(itemPush);
                                                myID++;
                                            })
                                        })

                                        var options = {
                                            dataSource: Common.DataSource.Local({
                                                data: data,
                                                model: NewModel,
                                                pageSize: 20,
                                            }),
                                            height: '100%', pageable: true, sortable: true, columnMenu: false, resizable: true, reorderable: true, filterable: { mode: 'row' }, editable: { mode: 'inline' },
                                            columns: columns
                                        }

                                        $scope.CATDistributor_MainGrid.setOptions(options)
                                    }
                                    $rootScope.IsLoading = false;
                                },
                                error: function (res) {
                                    $rootScope.IsLoading = false;
                                }
                            });
                            break;

                    }

                }
            },
            error: function (res) {
                $rootScope.IsLoading = false;
            }
        });

    }


    //#region  popup main
    $scope.CATDistributor_TabIndex = 1;
    $scope.CATDistributor_TabOptions = {
        animation:
            {
                open: { effects: "fadeIn" },
                select: function (e) {
                    $scope.CATDistributor_TabIndex = 1;
                    $scope.CATDistributor_TabIndex = $(e.item).data('tabindex');
                }
            }
    }

    $scope.Distributor_cboGroupOfPartnerOptions = {
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

            $scope.Distributor_cboGroupOfPartnerOptions.dataSource.data(data);
        }
    })

    $scope.Distributor_cboCountryOptions = {
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
                $scope.CreateDataSourceComboBox('Province', cbo.value(), 'change')
            }
        }
    }


    $scope.Distributor_cboProvinceOptions = {
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
                $scope.CreateDataSourceComboBox('District', cbo.value(), 'change')
            }
        }
    }
    $scope.Distributor_cboDistrictOptions = {
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
                //$scope.CreateDataSourceComboBox('Ward', cbo.value(),'change')
            }
        }
    }
    $scope.Distributor_cboWardOptions = {
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
            _CATDistributor.Data.Province = data;
        }
    })
    Common.ALL.Get($http, {
        url: Common.ALL.URL.District,
        success: function (data) {
            _CATDistributor.Data.District = data;
        }
    })
    Common.ALL.Get($http, {
        url: Common.ALL.URL.Ward,
        success: function (data) {
            _CATDistributor.Data.Ward = data;
        }
    })
    $scope.CreateDataSourceComboBox = function (target, value, status) {
        switch (target) {
            default: break;
            case 'Province': var result = $.grep(_CATDistributor.Data.Province, function (o) { return o.CountryID == value; });

                $scope.Distributor_cboProvinceOptions.dataSource.data(result);
                //$scope.LocationItem.ProvinceID = null;
                if (result.length > 0) {
                    $scope.DistributorItem.ProvinceID = result[0].ID;
                    $timeout(function () {
                        $scope.Distributor_cboProvince.select(0)
                    }, 1)
                }
                break;
            case 'District': var result = $.grep(_CATDistributor.Data.District, function (o) { return o.ProvinceID == value; });

                $scope.Distributor_cboDistrictOptions.dataSource.data(result);
                //$scope.LocationItem.DistrictID = null;
                if (result.length > 0) {
                    $scope.DistributorItem.DistrictID = result[0].ID;
                    $timeout(function () {
                        $scope.Distributor_cboDistrict.select(0)
                    }, 1)
                }
                break;
            case 'Ward': var result = $.grep(_CATDistributor.Data.Ward, function (o) { return o.DistrictID == value; });
                $scope.Distributor_cboWardOptions.dataSource.data(result);
                //$scope.LocationItem.DistrictID = null;
                if (result.length > 0) {
                    $scope.DistributorItem.WardID = result[0].ID;
                    $timeout(function () {
                        $scope.Distributor_cboWard.select(0)
                    }, 1)

                } break;
        }
    }

    $scope.CATDistributor_SaveClick = function ($event, win, vform) {
        $event.preventDefault();
        if (vform()) {
            Common.Services.Call($http, {
                url: Common.Services.url.CAT,
                method: _CATDistributor.URL.Update_Distributor,
                data: { item: $scope.DistributorItem, partnerid: $scope.DistributorPartnerID },
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
                    $scope.CreateGrid('cus')
                }
            });
        }
    }

    $scope.CATDistributor_DeleteClick = function ($event, win) {
        $event.preventDefault();
        Common.Services.Call($http, {
            url: Common.Services.url.CAT,
            method: _CATDistributor.URL.Delete_Distributor,
            data: { item: $scope.DistributorItem },
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
                $scope.CreateGrid('cus')
            }
        });
    }

    $scope.CATDistributor_CloseClick = function ($event, win) {
        $event.preventDefault();
        win.close();
    }
    //tab2
    $scope.CATDistributor_Location_GridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.CAT,
            method: _CATDistributor.URL.Read_Location,
            readparam: function () { return { partnerid: $scope.DistributorPartnerID } },
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
                template: '<a href="/" ng-click="CATDistributor_Location_EditClick($event,CATDistributor_Location_win,CATDistributor_Location_Grid)" class="k-button"><i class="fa fa-pencil"></i></a>' +
                    '<a href="/" ng-click="CATDistributor_Location_DestroyClick($event,CATDistributor_Location_Grid)" class="k-button"><i class="fa fa-trash"></i></a>',
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

    $scope.CATDistributor_Location_EditClick = function myfunction($event, win, grid) {
        $event.preventDefault();
        var tr = $($event.target).closest('tr');
        var id = 0;
        var item = grid.dataItem(tr);
        if (Common.HasValue(item)) id = item.ID;
        $scope.LoadLocationItem(win, id);
    }

    $scope.CATDistributor_Location_DestroyClick = function ($event, grid) {
        $event.preventDefault();
        var tr = $($event.target).closest('tr');
        var item = grid.dataItem(tr);
        if (Common.HasValue(item)) {
            if (confirm('Bạn muốn xóa các dữ liệu đã chọn ?')) {
                Common.Services.Call($http, {
                    url: Common.Services.url.CAT,
                    method: _CATDistributor.URL.Delete_Location,
                    data: { 'item': item },
                    success: function (res) {
                        $scope.CATDistributor_Location_GridOptions.dataSource.read();
                    }
                });
            }
        }
    }

    $scope.CATDistributor_Location_AddClick = function ($event, win) {
        $event.preventDefault();
        $scope.LoadLocationItem(win, 0);
    }

    $scope.LoadLocationItem = function (win, id) {
        Common.Services.Call($http, {
            url: Common.Services.url.CAT,
            method: _CATDistributor.URL.Get_Location,
            data: { 'id': id },
            success: function (res) {
                $scope.CreateDataSourceComboBoxDistributorLocation('Province', res.CountryID, status)
                $scope.CreateDataSourceComboBoxDistributorLocation('District', res.ProvinceID, status)
                $scope.DistributorLocationItem = res;
                win.center();
                win.open();
            }
        });
    }

    $scope.CATDistributor_Location_SearchClick = function ($event, win) {
        $event.preventDefault()

        win.center();
        win.open();
        $scope.CATDistributor_LocationSearch_Grid.dataSource.read();
    }

    //#endregion

    //#region popup edit location
    $scope.DistributorLocation_cboCountryOptions = {
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
                $scope.CreateDataSourceComboBoxDistributorLocation('Province', cbo.value())
            }
        }
    }

    Common.ALL.Get($http, {
        url: Common.ALL.URL.Country,
        success: function (data) {

            $scope.Distributor_cboCountryOptions.dataSource.data(data);
            $scope.DistributorLocation_cboCountryOptions.dataSource.data(data)
            _CATDistributor.Data.Country = data;
        }
    })

    //$scope.DistributorLocation_cboCountryOptions.dataSource.data(_CATDistributor.Data.Country)
    $scope.DistributorLocation_cboProvinceOptions = {
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
                $scope.CreateDataSourceComboBoxDistributorLocation('District', cbo.value())
            }
        }
    }
    $scope.DistributorLocation_cboDistrictOptions = {
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
                //$scope.CreateDataSourceComboBox('Ward', cbo.value())
            }
        }
    }
    $scope.DistributorLocation_cboWardOptions = {
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
    $scope.CreateDataSourceComboBoxDistributorLocation = function (target, value, status) {
        switch (target) {
            default: break;
            case 'Province': var result = $.grep(_CATDistributor.Data.Province, function (o) { return o.CountryID == value; });

                $scope.DistributorLocation_cboProvinceOptions.dataSource.data(result);
                //$scope.LocationItem.ProvinceID = null;
                if (result.length > 0) {
                    $scope.DistributorLocationItem.ProvinceID = result[0].ID;
                    $timeout(function () {
                        $scope.DistributorLocation_cboProvince.select(0)
                    }, 1)
                }
                break;
            case 'District': var result = $.grep(_CATDistributor.Data.District, function (o) { return o.ProvinceID == value; });

                $scope.DistributorLocation_cboDistrictOptions.dataSource.data(result);
                //$scope.LocationItem.DistrictID = null;
                if (result.length > 0) {
                    $scope.DistributorLocationItem.DistrictID = result[0].ID;
                    $timeout(function () {
                        $scope.DistributorLocation_cboDistrict.select(0)
                    }, 1)
                }
                break;
            case 'Ward': var result = $.grep(_CATDistributor.Data.Ward, function (o) { return o.DistrictID == value; });
                $scope.DistributorLocation_cboWardOptions.dataSource.data(result);
                //$scope.LocationItem.DistrictID = null;
                if (result.length > 0) {
                    $scope.DistributorLocationItem.WardID = result[0].ID;
                    $timeout(function () {
                        $scope.DistributorLocation_cboWard.select(0)
                    }, 1)

                } break;
        }
    }
    $scope.DistributorLocation_numLatOptions = { format: '#,##0.00000', spinners: false, culture: 'en-US', min: 0, step: 1, decimals: 5, }
    $scope.DistributorLocation_numLngOptions = { format: '#,##0.00000', spinners: false, culture: 'en-US', min: 0, step: 1, decimals: 5, }
    $scope.CATDistributor_Location_SaveClick = function ($event, win, vform) {
        $event.preventDefault();

        if (vform()) {
            Common.Services.Call($http, {
                url: Common.Services.url.CAT,
                method: _CATDistributor.URL.Update_Location,
                data: { item: $scope.DistributorLocationItem, partnerid: $scope.DistributorPartnerID },
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
                    $scope.CATDistributor_Location_GridOptions.dataSource.read();

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

    $scope.CATDistributor_Location_win_CloseClick = function ($event,win) {
        $event.preventDefault();
        win.close()
    }
    //#endregion

    //#region popup search
    $scope.CATDistributor_LocationSearch_GridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.CAT,
            method: _CATDistributor.URL.Read_LocationNotIn,
            readparam: function () { return { partnerid: $scope.DistributorPartnerID } },
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
        height: '100%', pageable: true, sortable: true, columnMenu: false, resizable: true, reorderable: true, filterable: {mode: 'row'},
        columns: [
            {
                title: ' ', width: '45px',
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,CATDistributor_LocationSearch_Grid,CATDistributor_LocationSearch_ChooseChange)" />',
                headerAttributes: { style: 'text-align: center;' }, attributes: { style: 'text-align: center;' },
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,CATDistributor_LocationSearch_Grid,CATDistributor_LocationSearch_ChooseChange)" />',
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
    $scope.CATDistributor_LocationSearch_ChooseChange = function ($event, grid, haschoose) {
        $scope.HasChoose = haschoose;
    };

    $scope.CATDistributor_Location_ChooseClick = function ($event, win, grid) {
        $event.preventDefault()
        var data = grid.dataSource.data();
        var dataSend = $.grep(data, function (o) { return o.IsChoose == true })
        if (dataSend.length > 0) {
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.CAT,
                method: _CATDistributor.URL.Update_LocationNotIn,
                data: { lst: dataSend, partnerid: $scope.DistributorPartnerID },
                success: function (res) {
                    $rootScope.IsLoading = false;
                    $rootScope.Message({
                        Type: Common.Message.Type.Notify,
                        NotifyType: Common.Message.NotifyType.SUCCESS,
                        Title: 'Thông báo',
                        Msg: 'Thành công',
                        Close: null,
                        Ok: null
                    })
                    //debugger
                    $scope.CATDistributor_Location_GridOptions.dataSource.read();
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
    $scope.CATDistributor_LocationNotIn_win_CloseClick = function ($event, win) {
        $event.preventDefault();
        win.close()
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