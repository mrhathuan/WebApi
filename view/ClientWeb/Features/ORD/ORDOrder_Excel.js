/// <reference path="~/Scripts/jquery-1.9.1.intellisense.js" />
/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />

//#region URL

var _ORDOrder_Excel = {
    URL: {
        Check: 'ORDOrder_Excel_Check',
        Import: 'ORDOrder_Excel_Import',
        Export: 'ORDOrder_Excel_Export',
        Template_List: 'ORDOrder_Excel_Setting_List',
        Template_Download: 'ORDOrder_Excel_Setting_Download',
        Location_Create: 'ORDOrder_Excel_Location_Create',
        Template_Get: 'ORDOrder_Excel_Setting_Get',

        //GOP
        GOPRead: "GroupOfProduct_Read",
        Product_Create: "ORDOrder_Excel_Product_Create",
    },
    Data: {
        Province: [],
        District: [],
        FromFile: [],
        Service: [
            {
                ID: 26,
                ValueOfVar: 'Nhập khẩu',
                ListMode: [
                    {
                        ID: 31,
                        ValueOfVar: 'FCL'
                    }
                ]
            },
            {
                ID: 27,
                ValueOfVar: 'Xuất khẩu',
                ListMode: [
                    {
                        ID: 31,
                        ValueOfVar: 'FCL'
                    }
                ]
            },
            {
                ID: 28,
                ValueOfVar: 'Nội địa',
                ListMode: [
                    {
                        ID: 31,
                        ValueOfVar: 'FCL'
                    },
                    {
                        ID: 33,
                        ValueOfVar: 'FTL'
                    },
                    {
                        ID: 34,
                        ValueOfVar: 'LTL'
                    }
                ]
            }
        ],

        ListGOP: [],
        ListPacking: [],

        CookieSearch: "_ORDOrder_Excel_Template",
    },
    iFCL: 31,
    iFTL: 33, 
    iLTL: 34,
    ExcelKey: {
        Resource: "ORDOrder_Excel",
        Order: "ORDOrder_Excel"
    }
}

//#endregion

angular.module('myapp').controller('ORDOrder_ExcelCtrl', ['$rootScope', '$scope', '$http', '$location', '$state', '$timeout', function ($rootScope, $scope, $http, $location, $state, $timeout) {
    if ($rootScope.IsPageComplete != true) return;
    Common.Log('ORDOrder_ExcelCtrl');
    $rootScope.IsLoading = false;

    if (!$rootScope.CheckView("ActExcel", "main.ORDOrder.Index")) return;
    $scope.Auth = $rootScope.GetAuth();

    $scope.IsCO = false;
    $scope.IsDI = false;
    $scope.IsExcelChecked = false;
    $scope.IsNewLocation = false;
    $scope.IsNewProduct = false;
    $scope.HasTemplate = false;
    $scope.IsFullScreen = false;

    $scope.Template = {
        ID: -1, Name: '', CustomerName: '', CustomerID: -1, SYSCustomerID: -1
    }

    //MainView
    $scope.excel_container_gridOptions = {
        dataSource: Common.DataSource.Local({
            data: [],
            pageSize: 0,
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number', editable: false },
                    ExcelSuccess: { type: 'boolean' }
                }
            }
        }),
        height: '100%', groupable: false, pageable: false, sortable: true, columnMenu: false, filterable: false, resizable: true,
        columns: [
            {
                field: 'ExcelSuccess', title: '{{RS.ORDOrder_Excel.ExcelSuccess}}', width: '40px',
                template: '<input class="chkChoose" type="checkbox" #= ExcelSuccess ? "checked=checked" : "" # disabled="disabled" />',
                attributes: { style: 'text-align: center;' }, headerAttributes: { style: 'text-align: center;' },
                sortorder: 0, configurable: false, isfunctionalHidden: false
            },
            {
                field: 'ExcelError', width: '250px', title: '{{RS.ORDOrder_Excel.ExcelError}}',
                sortorder: 1, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'OrderCode', width: '100px', title: '{{RS.ORDOrder.Code}}',
                sortorder: 2, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'CustomerCode', width: '120px', title: '{{RS.CUSCustomer.Code}}',
                sortorder: 3, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'TransportModeName', width: '100px', title: '{{RS.ORDOrder.TransportModeID}}',
                sortorder: 4, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'ServiceOfOrderName', width: '100px', title: '{{RS.ORDOrder.ServiceOfOrderID}}',
                sortorder: 5, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'RequestDate', width: '110px', title: '{{RS.ORDOrder.RequestDate}}',
                template: "#=RequestDate==null?' ':Common.Date.FromJsonDMY(RequestDate)#",
                sortorder: 6, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'LocationFromCode', width: '120px', title: '{{RS.ORDOrder_Excel.LocationFromCode}}',
                sortorder: 7, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'LocationFromName', width: '150px', title: '{{RS.ORDOrder_Excel.LocationFromName}}',
                sortorder: 8, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'LocationToCode', width: '120px', title: '{{RS.ORDOrder_Excel.LocationToCode}}',
                sortorder: 9, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'LocationToName', width: '150px', title: '{{RS.ORDOrder_Excel.LocationToName}}',
                sortorder: 10, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'PackingName', width: '120px', title: '{{RS.ORDContainer.PackingID}}',
                sortorder: 11, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'ContainerNo', width: '120px', title: '{{RS.ORDContainer.ContainerNo}}',
                sortorder: 12, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'SealNo1', width: '120px', title: '{{RS.ORDContainer.SealNo1}}',
                sortorder: 13, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'SealNo2', width: '120px', title: '{{RS.ORDContainer.SealNo2}}',
                sortorder: 14, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'PartnerCode', width: '120px', title: '{{RS.CUSPartner.PartnerCode}}',
                sortorder: 15, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'PartnerName', width: '120px', title: '{{RS.CATPartner.PartnerName}}',
                sortorder: 16, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'Ton', width: '100px', title: '{{RS.ORDContainer.Ton}}',
                sortorder: 17, configurable: true, isfunctionalHidden: false
            },
            {
                field: 'Note', width: '250px', title: '{{RS.ORDContainer.Note}}',
                sortorder: 18, configurable: true, isfunctionalHidden: false
            },
            { title: '', filterable: false, sortable: false, sortorder: 100, configurable: false, isfunctionalHidden: false }
        ],
        dataBound: function (e) {
            Common.Log('excel_container_gridOptionsBound');
        }
    }

    $scope.excel_truck_gridOptions = {
        dataSource: Common.DataSource.Local({
            data: [],
            pageSize: 0,
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number', editable: false },
                    PartnerID: { type: 'number', editable: false },
                    ExcelSuccess: { type: 'boolean' }
                }
            }
        }),
        height: '100%', groupable: false, pageable: false, sortable: true, columnMenu: false, filterable: false, resizable: true,
        columns: [
            {
                field: 'ExcelSuccess', title: '{{RS.ORDOrder_Excel.ExcelSuccess}}', width: '40px',
                template: '<input class="chkChoose" type="checkbox" #= ExcelSuccess ? "checked=checked" : "" # disabled="disabled" />',
                attributes: { style: 'text-align: center;' }, headerAttributes: { style: 'text-align: center;' }
            },
            {
                field: 'ExcelError', width: '250px', title: '{{RS.ORDOrder_Excel.ExcelError}}'
            },
            {
                field: 'OrderCode', width: '100px', title: '{{RS.ORDOrder.Code}}'
            },
            {
                field: 'CustomerCode', width: '120px', title: '{{RS.CUSCustomer.Code}}'
            },
            {
                field: 'SOCode', width: '100px', title: '{{RS.ORDGroupProduct.SOCode}}'
            },
            {
                field: 'DNCode', width: '100px', title: '{{RS.ORDGroupProduct.DNCode}}'
            },
            {
                field: 'TransportModeName', width: '100px', title: '{{RS.ORDOrder.TransportModeID}}'
            },
            {
                field: 'RequestDate', width: '110px', title: '{{RS.ORDOrder.RequestDate}}',
                template: "#=RequestDate==null?' ':Common.Date.FromJsonDMY(RequestDate)#",
            },
            {
                field: 'LocationFromCode', width: '100px', title: '{{RS.ORDOrder_Excel.LocationFromCode}}'
            },
            {
                field: 'LocationFromName', width: '150px', title: '{{RS.ORDOrder_Excel.LocationFromName}}'
            },
            {
                field: 'GroupOfProductCode', width: '120px', title: '{{RS.CUSGroupOfProduct.Code}}'
            },
            {
                field: 'ProductCode', width: '120px', title: '{{RS.CUSProduct.Code}}'
            },
            {
                field: 'PartnerCode', width: '120px', title: '{{RS.CUSPartner.PartnerCode}}'
            },
            {
                field: 'LocationToAddress', width: '250px', title: '{{RS.ORDOrder_Excel.LocationToAddress}}'
            },
            {
                field: 'ListLocation', width: '250px', title: '{{RS.ORDOrder_Excel.ListLocation}}', sortable: false,
                template: '<select ng-model="dataItem.LocationToID" kendo-combo-box k-data-text-field="\'Address\'" '
                    + 'k-data-value-field="\'CUSLocationID\'"  k-filter="\'contains\'" k-auto-bind="true" k-min-length="3" '
                    + 'k-on-data-bound="cboLocationToDataBound(kendoEvent, dataItem)" k-data-source="dataItem.ListLocationToAddress" style="width: 100%"></select>'
            },
            {
                field: 'IsLocationToFail', width: '110px', title: '{{RS.ORDOrder_Excel.IsLocationToFail}}',
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAllExcelTruck_Check($event,excel_truck_grid,excelTruck_GridChoose_Change)" />',
                headerAttributes: { style: 'text-align: center;' }, attributes: { style: 'text-align: center;' },
                template: '<input type="checkbox" class="chkNew chkChooseExcelTruck" #= PartnerID > 0 && IsLocationToFail ? checked="checked" : "" #/>',
                attributes: { style: 'text-align: center;' }, headerAttributes: { style: 'text-align: center;' },
                filterable: false, sortable: false
            },
            {
                field: 'Ton', width: '100px', title: '{{RS.ORDGroupProduct.Ton}}'
            },
            {
                field: 'CBM', width: '100px', title: '{{RS.ORDGroupProduct.CBM}}'
            },
            {
                field: 'Quantity', width: '100px', title: '{{RS.ORDGroupProduct.Quantity}}'
            },
            {
                field: 'Description', width: '250px', title: '{{RS.ORDGroupProduct.Description}}'
            },
            { title: '', filterable: false, sortable: false }
        ],
        dataBound: function (e) {
            Common.Log('excel_truck_gridOptionsBound');

            $rootScope.IsLoading = true;

            var grid = this;
            $.each(grid.items(), function (i, tr) {
                var item = grid.dataItem(tr);
                if (Common.HasValue(item)) {
                    if (item.IsLocationToFail && item.PartnerID > 0)
                        $(tr).addClass('newlocation');
                }
            })

            $rootScope.IsLoading = false;
        }
    }

    $rootScope.gridChooseAllExcelTruck_Check = function ($event, grid, callback) {
        if ($event.target.checked == true) {
            grid.items().each(function () {

                var tr = this;
                var item = grid.dataItem(tr);
                if (item.IsLocationToFail != true) {
                    $(tr).find('td input.chkChooseExcelTruck').prop('checked', true);
                    item.IsLocationToFail = true;
                }
            });
        }
        else {
            grid.items().each(function () {
                var tr = this;
                var item = grid.dataItem(tr);
                if (item.IsLocationToFail == true) {
                    $(tr).find('td input.chkChooseExcelTruck').prop('checked', false);
                    item.IsLocationToFail = false;
                }
            });
        }

        if (Common.HasValue(callback)) {
            callback($event, grid, $event.target.checked);
        }
    };

    $scope.cboLocationFromDataBound = function ($kendoEvent, item) {
        if (item && !item.dirty) {
            $kendoEvent.sender.select(function (o) { return o.CUSLocationID == item.LocationFromID });
            item.dirty = true;
        }
    }

    $scope.cboLocationToDataBound = function ($kendoEvent, item) {
        if (item && !item.dirty) {
            $kendoEvent.sender.select(function (o) { return o.CUSLocationID == item.LocationToID });
            item.dirty = true;
        }
    }

    $scope.excel_template_gridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.ORD,
            method: _ORDOrder_Excel.URL.Template_List,
            pageSize: 0,
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' }
                }
            }
        }),
        height: '100%', groupable: false, pageable: false, sortable: true, columnMenu: false,
        filterable: { mode: 'row' }, resizable: true, selectable: true,
        dataBound: function () {
            var grid = this;
            $(grid.items()).on('dblclick', function (e) {
                var tr = e.currentTarget;
                var item = grid.dataItem(tr);
                if (Common.HasValue(item)) {
                    $timeout(function () {
                        $scope.HasTemplate = true;

                        $scope.Template.ID = item.ID;
                        $scope.Template.Name = item.Name;
                        $scope.Template.CustomerName = item.SettingCustomerName;
                        $scope.Template.CustomerID = item.CustomerID;
                        $scope.Template.SYSCustomerID = item.SYSCustomerID;
                        Common.Cookie.Set(_ORDOrder_Excel.Data.CookieSearch, JSON.stringify($scope.Template));
                        $scope.template_win.close();
                    }, 1)
                }
            })
        },
        columns: [ { field: 'Name', title: '{{RS.CUSSetting.Name}}', sortable: true, filterable: { cell: { operator: 'contains', showOperators: false } } } ]
    }
    
    $scope.Template_Click = function ($event, win) {
        if (Common.HasValue($event))
            $event.preventDefault();

        $scope.excel_template_gridOptions.dataSource.read();
        if (win.wrapper.length > 0)
            win.center().open();
    }
    
    $scope.Excel_Template_Accept_Click = function ($event, win, grid) {
        $event.preventDefault();
        
        $scope.HasTemplate = false;
        $scope.Template = {
            ID: -1, Name: '', CustomerName: '', CustomerID: -1, SYSCustomerID: -1
        }

        var item = grid.dataItem(grid.select());
        if (Common.HasValue(item)) {
            $scope.HasTemplate = true;

            $scope.Template.ID = item.ID;
            $scope.Template.Name = item.Name;
            $scope.Template.CustomerName = item.SettingCustomerName;
            $scope.Template.CustomerID = item.CustomerID;
            $scope.Template.SYSCustomerID = item.SYSCustomerID;

            Common.Cookie.Set(_ORDOrder_Excel.Data.CookieSearch, JSON.stringify($scope.Template));
        }

        win.close();
    }

    $scope.UpFile_Click = function ($event, file) {
        $event.preventDefault();

        $timeout(function () {
            angular.element(file.element[0]).trigger('click');
        }, 1);
    }

    $scope.DownFile_Click = function ($event) {
        $event.preventDefault();

        $rootScope.IsLoading = true;
        Common.Services.Call($http, {
            url: Common.Services.url.ORD,
            method: _ORDOrder_Excel.URL.Template_Download,
            data: {
                templateID: $scope.Template.ID, customerID: $scope.Template.CustomerID
            },
            success: function (res) {
                $rootScope.IsLoading = false;
                Common.Services.Error(res, function (res) {
                    $rootScope.DownloadFile(res);
                })
            }
        });
    }

    $scope.excel_fileOptions = {
        async: {
            saveUrl: '/Handler/File.ashx',
            autoUpload: true
        },
        multiple: false,
        showFileList: false,
        upload: function (e) {
            var xhr = e.XMLHttpRequest;
            xhr.addEventListener('readystatechange', function (e) {
                if (xhr.readyState == 1)
                    xhr.setRequestHeader('auth', Common.Auth.HeaderKey);
            });
            e.data = { 'folderPath': Common.FolderUpload.Import }
        },
        success: function (e) {
            $scope.File = e.response;
            $scope.File.ReferID = $rootScope.FunctionItem.ID;
            $scope.File.TypeOfFileCode = Common.CATTypeOfFileCode.ORD;

            $scope.Excel_Check();
        }
    };
    
    $scope.Excel_Check = function () {
        $rootScope.IsLoading = true;

        Common.Services.Call($http, {
            url: Common.Services.url.ORD,
            method: _ORDOrder_Excel.URL.Check,
            data: {
                file: $scope.File.FilePath,
                templateID: $scope.Template.ID,
                customerID: $scope.Template.CustomerID
            },
            success: function (res) {
                $rootScope.IsLoading = false;
                Common.Services.Error(res, function (res) {
                    $rootScope.Message({
                        Msg: "Đã kiểm tra."
                    })

                    _ORDOrder_Excel.Data.FromFile = res;
                    var data = [];
                    if (res.length > 0) {
                        switch (res[0].TransportModeIDTemp) {
                            case _ORDOrder_Excel.iFCL:
                                $scope.IsCO = true;
                                $scope.IsDI = false;

                                data = $scope.Create_COLocalData(res);
                                break;
                            case _ORDOrder_Excel.iFTL:
                            case _ORDOrder_Excel.iLTL:
                                $scope.IsCO = false;
                                $scope.IsDI = true;

                                data = $scope.Create_DILocalData(res);
                                break;
                            default:
                                break;
                        }
                    }

                    if ($scope.IsCO) {
                        $scope.excel_container_gridOptions.dataSource.data(data);
                        $scope.IsExcelChecked = true;
                    } else {
                        $scope.excel_truck_gridOptions.dataSource.data(data);
                        $timeout(function () {
                            $scope.excel_truck_gridOptions.dataSource.sort({ field: "IsLocationToFail", dir: "desc" });
                        }, 1)
                        $scope.IsExcelChecked = true;

                        if ($scope.IsNewLocation) {
                            $rootScope.Message({
                                Msg: "Có địa chỉ mới, bạn muốn thêm địa chỉ này?",
                                Type: Common.Message.Type.Confirm,
                                Ok: function () {
                                    $scope.Location_Click(null, $scope.excel_location_win);
                                }
                            })
                        }

                        if ($scope.IsNewProduct) {
                            $rootScope.Message({
                                Msg: "Có sản phẩm mới, bạn muốn thêm sản phẩm này?",
                                Type: Common.Message.Type.Confirm,
                                Ok: function () {
                                    $scope.Product_Click(null, $scope.excel_product_win);
                                }
                            })
                        }
                        
                    }                    
                })
            }
        });
    }
        
    $scope.Create_DILocalData = function (res) {
        Common.Log("CreateDIData");

        var data = [];
        $scope.IsNewLocation = false;
        $scope.IsNewProduct = false;
        $.each(res, function (i, o) {
            if (Common.HasValue(o.RequestDate))
                o.RequestDate = Common.Date.FromJson(o.RequestDate);
            if (Common.HasValue(o.ETD))
                o.ETD = Common.Date.FromJson(o.ETD);
            if (Common.HasValue(o.ETA))
                o.ETD = Common.Date.FromJson(o.ETA);
            if (Common.HasValue(o.ETARequest))
                o.ETARequest = Common.Date.FromJson(o.ETARequest);

            if (Common.HasValue(o.ListProductNew) && o.ListProductNew.length > 0) {
                $scope.IsNewProduct = true;
            }

            if (o.IsLocationToFail && o.PartnerID > 0)
                $scope.IsNewLocation = true;
            var item = {
                'ID': i + '-1', 'sID': i, 'pID': -1,
                'ExcelError': o.ExcelError, 'ExcelSuccess': o.ExcelSuccess,
                'CustomerCode': o.CustomerCode, 'OrderCode': o.Code, 'TransportModeName': o.TransportModeName, 'RequestDate': o.RequestDate,
                'LocationFromID': null, 'LocationToID': null, 'LocationToAddress': '', 'ListLocationToAddress': [], 'IsLocationToFail': o.IsLocationToFail
            };

            if (o.ListProduct.length > 0) {
                var idx = 1;
                Common.Data.Each(o.ListProduct, function (p) {
                    if (Common.HasValue(p.RequestDate))
                        p.RequestDate = Common.Date.FromJson(p.RequestDate);
                    if (Common.HasValue(p.ETD))
                        p.ETD = Common.Date.FromJson(p.ETD);
                    if (Common.HasValue(p.ETARequest))
                        p.ETARequest = Common.Date.FromJson(p.ETARequest);

                    var detail = $.extend(true, {}, item);

                    p.TmpID = i + '_' + idx++;
                    detail.ID = p.TmpID;
                    detail.pID = 0;

                    detail.LocationFromID = p.LocationFromID;
                    detail.LocationFromCode = p.LocationFromCode;
                    detail.LocationFromName = p.LocationFromName;

                    detail.LocationToID = p.LocationToID;
                    detail.LocationToCode = p.LocationToCode;
                    detail.LocationToAddress = p.LocationToAddress;
                    detail.ListLocationToAddress = p.ListLocationToAddress;
                    detail.EconomicZone = p.EconomicZone;
                    if (detail.LocationToID < 1) {
                        if (detail.ListLocationToAddress.length > 0)
                            detail.LocationToID = detail.ListLocationToAddress[0].CUSLocationID;
                        else
                            detail.LocationToID = -1;
                    }
                    detail.Ton = p.Ton;
                    detail.CBM = p.CBM;
                    detail.SOCode = p.SOCode;
                    detail.DNCode = p.DNCode;
                    detail.Quantity = p.Quantity;

                    detail.ProductCode = p.ProductCode;
                    detail.GroupOfProductCode = p.GroupOfProductCode;

                    detail.PartnerID = p.PartnerID;
                    detail.Description = p.Description;
                    detail.PartnerCode = p.PartnerCode;
                    detail.PartnerName = p.PartnerName;
                    detail.ListPartnerLocation = o.ListPartnerLocation;

                    data.push(detail);
                })              
            }
        });
        return data;
    }

    $scope.Create_COLocalData = function (res) {
        Common.Log("CreateDIData");

        var data = [];
        $scope.IsNewLocation = false;
        $.each(res, function (i, o) {
            if (Common.HasValue(o.RequestDate))
                o.RequestDate = Common.Date.FromJson(o.RequestDate);
            if (Common.HasValue(o.ETD))
                o.ETD = Common.Date.FromJson(o.ETD);
            if (Common.HasValue(o.ETA))
                o.ETA = Common.Date.FromJson(o.ETA);
            if (Common.HasValue(o.CutOffTime))
                o.CutOffTime = Common.Date.FromJson(o.CutOffTime);
            if (Common.HasValue(o.ETARequest))
                o.ETARequest = Common.Date.FromJson(o.ETARequest);

            var item = {
                'ID': i + '-1', 'sID': i, 'pID': -1,
                'ExcelError': o.ExcelError, 'ExcelSuccess': o.ExcelSuccess,
                'CustomerCode': o.CustomerCode, 'OrderCode': o.Code, 'TransportModeName': o.TransportModeName, 'ServiceOfOrderName': o.ServiceOfOrderName,
                'RequestDate': o.RequestDate, 'CutOffTime': o.CutOffTime, 'PartnerCode': o.PartnerCode, 'PartnerName': o.PartnerName, PartnerID: o.PartnerID
            };

            if (o.ListContainer.length > 0) {
                Common.Data.Each(o.ListContainer, function (c) {
                    if (Common.HasValue(c.ETD))
                        c.ETD = Common.Date.FromJson(c.ETD);
                    if (Common.HasValue(c.ETA))
                        c.ETA = Common.Date.FromJson(c.ETA);

                    var co = $.extend(true, {}, item);

                    co.ID = i + '' + 1;
                    co.pID = 0;

                    co.LocationFromID = c.LocationFromID;
                    co.LocationFromCode = c.LocationFromCode;
                    co.LocationFromName = c.LocationFromName;
                    co.LocationToID = c.LocationToID;
                    co.LocationToCode = c.LocationToCode;
                    co.LocationToName = c.LocationToName;
                    co.Ton = c.Ton;
                    co.Quantity = c.Quantity;
                    co.PackingName = c.PackingName;
                    co.ContainerNo = c.ContainerNo;
                    co.SealNo1 = c.SealNo1;
                    co.SealNo2 = c.SealNo2;
                    data.push(co);
                })
            }
        });
        return data;
    }

    $scope.Import_Click = function ($event) {
        $event.preventDefault();
        
        var grid = null;
        if ($scope.IsCO)
            grid = $scope.excel_container_grid;
        if ($scope.IsDI)
            grid = $scope.excel_truck_grid;
        if ($scope.IsDI) {
            $.each(grid.items(), function (i, tr) {
                var item = grid.dataItem(tr);
                for (var j = 0; j < _ORDOrder_Excel.Data.FromFile[item.sID].ListProduct.length; j++) {
                    if (_ORDOrder_Excel.Data.FromFile[item.sID].ListProduct[j].TmpID == item.ID) {
                        _ORDOrder_Excel.Data.FromFile[item.sID].ListProduct[j].LocationToID = item.LocationToID;
                        _ORDOrder_Excel.Data.FromFile[item.sID].ListProduct[j].LocationFromID = item.LocationFromID;
                    }
                }
            })
        }

        var importData = grid.dataSource.data();
        var lstError = [];
        $.each(importData, function (i, item) {
            if (!item.ExcelSuccess) {
                if (lstError.indexOf(item.OrderCode) < 0)
                    lstError.push(item.OrderCode);
            }
        })

        var strConfirm = 'Xác nhận lưu đơn hàng?';
        if (lstError.length > 0) {
            var strError = lstError.join(", ");
            strConfirm = "Đơn hàng (" + strError + ") có lỗi, tiếp tục lưu những đơn thành công?";
        }
        $rootScope.Message({
            Type: Common.Message.Type.Confirm, Msg: strConfirm,
            Ok: function () {
                $rootScope.IsLoading = true;
                var data = [];
                var dataSource = new kendo.data.DataSource({
                    data: _ORDOrder_Excel.Data.FromFile,
                    group: { field: "Code" }
                });
                dataSource.fetch(function () { });
                $.each(dataSource.view(), function (i, group) {
                    var item = group.items[0];
                    for (var j = 1; j < group.items.length; j++) {
                        if ($scope.IsCO) {
                            for (var k = 0; k < group.items[j].ListContainer.length; k++) {
                                item.ListContainer.push(group.items[j].ListContainer[k]);
                                if (item.ExcelSuccess && group.items[j].ExcelSuccess == false)
                                    item.ExcelSuccess = false;
                            }
                        } else {
                            for (var k = 0; k < group.items[j].ListProduct.length; k++) {
                                item.ListProduct.push(group.items[j].ListProduct[k]);
                                if (item.ExcelSuccess && group.items[j].ExcelSuccess == false)
                                    item.ExcelSuccess = false;
                            }
                        }
                    }
                    data.push(item);
                });
                //for (var i = 0; i < data.length; i++) {
                //    //data[i].ListProduct.sort($scope.SortItemBySortOrder);
                //}

                data.sort($scope.SortArray);
                
                Common.Services.Call($http, {
                    url: Common.Services.url.ORD,
                    method: _ORDOrder_Excel.URL.Import,
                    data: {
                        Data: data, File: $scope.File,
                        TemplateID: $scope.Template.ID,
                    },
                    success: function (res) {
                        $rootScope.IsLoading = false;
                        Common.Services.Error(res, function () {
                            $rootScope.Message({ Msg: "Thành công!" });
                            grid.dataSource.data([]);
                            $scope.IsExcelChecked = false;
                            if (res > 0) {
                                var pID = res;
                                $rootScope.Message({
                                    Type: Common.Message.Type.Confirm,
                                    Msg: "Tải về bản cập nhật dữ liệu vừa import?",
                                    Ok: function () {
                                        $rootScope.IsLoading = true;
                                        Common.Services.Call($http, {
                                            url: Common.Services.url.ORD,
                                            method: _ORDOrder_Excel.URL.Export,
                                            data: {
                                                pID: pID, File: $scope.File,
                                                TemplateID: $scope.Template.ID,
                                            },
                                            success: function (res) {
                                                $rootScope.IsLoading = false;
                                                Common.Services.Error(res, function () {
                                                    $rootScope.DownloadFile(res);
                                                })
                                            }
                                        })
                                    }
                                })
                            }
                        })
                    }
                });
            }
        })
    }

    $scope.SortArray = function (a, b) {
        if (a.SortOrder < b.SortOrder)
            return -1;
        if (a.SortOrder > b.SortOrder)
            return 1;
        return 0;
    }

    //#region cookie
    $scope.Init_LoadCookie = function () {
        Common.Log('Init_LoadCookie');
        var strCookie = Common.Cookie.Get(_ORDOrder_Excel.Data.CookieSearch);
        if (Common.HasValue(strCookie) && strCookie != '') {
            try {
                var objCookie = eval('[' + strCookie + ']')[0];
                $scope.Template.ID = objCookie.ID;
            } catch (e) { }
        }
        
        if ($scope.Template.ID >= 0) {
            Common.Services.Call($http, {
                url: Common.Services.url.ORD,
                method: _ORDOrder_Excel.URL.Template_List,
                data: { request: "" },
                success: function (res) {
                    if (res != null && res.Data != null) {
                        angular.forEach(res.Data, function (item, key) {
                            if (item.ID == $scope.Template.ID && $scope.HasTemplate == false) {
                                $scope.Template.Name = item.Name;
                                $scope.Template.CustomerName = item.SettingCustomerName;
                                $scope.Template.CustomerID = item.CustomerID;
                                $scope.HasTemplate = true;
                            } 
                        });
                        if ($scope.HasTemplate == false) {
                                $scope.Template = {
                                    ID: -1, Name: '', CustomerName: '', CustomerID: -1
                                };
                            $scope.Template_Click(null, $scope.template_win);
                        }
                    }
                }
            });
        } else {
            $scope.Template = {
                ID: -1, Name: '', CustomerName: '', CustomerID: -1
            };
            $scope.Template_Click(null, $scope.template_win);
        }
    };
    //#endregion

    $timeout(function () {
        $scope.Init_LoadCookie();
    }, 100)

    //#region Win Location    
    $scope.Location_Click = function ($event, win) {
        if (Common.HasValue($event))
            $event.preventDefault();

        $rootScope.IsLoading = true;
        var grid = $scope.IsCO ? $scope.excel_container_grid : $scope.excel_truck_grid;
        var options = $scope.IsCO ? $scope.excel_container_gridOptions : $scope.excel_truck_gridOptions;

        var data = [];
        var temp = [];
        $.each(grid.items(), function (i, tr) {
            var o = grid.dataItem(tr);

            if (Common.HasValue(o)) {
                var isNew = $(tr).find(".chkNew").prop("checked");
                if (isNew) {
                    var objOrder = _ORDOrder_Excel.Data.FromFile[o.sID];
                    if (Common.HasValue(objOrder)) {
                        if (objOrder.ListPartnerLocation.length > 0) {
                            $.each(objOrder.ListPartnerLocation, function (j, partner) {
                                if (partner.PartnerID > 0) {
                                    var item = {
                                        'PartnerID': partner.PartnerID, 'PartnerCode': partner.PartnerCode, 'PartnerName': partner.PartnerName, 'LocationCode': partner.LocationCode, CustomerID: partner.CustomerID,
                                        'LocationAddress': partner.LocationAddress, 'EconomicZone': partner.EconomicZone, 'RoutingAreaCode': o.RoutingAreaCode, 'RouteDescription': partner.RouteDescription, 'ProvinceID': partner.ProvinceID, 'DistrictID': partner.DistrictID
                                    };
                                    temp.push(item);
                                }
                            });
                        }
                    }
                }
            }
        });
        if (temp.length > 0)
            data.push(temp[0]);
        for (var i = 0; i < temp.length; i++) {
            var flag = false;
            for (var j = 0; j < data.length; j++) {
                if (data[j].PartnerCode == temp[i].PartnerCode && data[j].LocationAddress == temp[i].LocationAddress) {
                    flag = true;
                    break;
                }
            }
            if (!flag)
            data.push(temp[i]);
    }
        if (_ORDOrder_Excel.Data.Province.length > 0) {
            $.each(data, function (i, o) {
                o.DataProvince = _ORDOrder_Excel.Data.Province;
                if (o.ProvinceID == null || o.ProvinceID == 0)
                    o.ProvinceID = _ORDOrder_Excel.Data.Province[0].ID;
                o.DataDistrict = $.grep(_ORDOrder_Excel.Data.District, function (v) { return o.ProvinceID == v.ProvinceID });
                if (o.DataDistrict.length > 0) {
                    if (o.DistrictID == null || o.DistrictID == 0)
                        o.DistrictID = o.DataDistrict[0].ID;
                }
            })
        }

        $timeout(function () {
            win.center().open();
            $scope.excel_location_gridOptions.dataSource.data(data);
            $rootScope.IsLoading = false;
        }, 1)
    }
    
    $scope.excel_location_gridOptions = {
        dataSource: Common.DataSource.Local({
            data: [],
            pageSize: 100000,
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number', editable: false }
                }
            }
        }),
        height: '100%', groupable: false, pageable: false, sortable: true, columnMenu: false, filterable: { mode: 'row' }, resizable: true,
        columns: [
            { field: 'PartnerCode', width: '80px', title: '{{RS.CUSPartner.PartnerCode}}', sortable: true, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'PartnerName', width: '150px', title: '{{RS.CATPartner.PartnerName}}', sortable: true, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'LocationCode', width: '150px', title: '{{RS.CUSLocation.Code}}', sortable: true, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'LocationAddress', width: '200px', title: '{{RS.CATLocation.Address}}', sortable: true, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { width: '150px', title: '{{RS.CATProvince.ProvinceName}}', template: '<select ng-model="dataItem.ProvinceID" k-index="0" kendo-combo-box k-data-text-field="\'ProvinceName\'" k-data-value-field="\'ID\'" k-filter="\'contains\'" k-auto-bind="true" k-min-length="3" k-data-source="dataItem.DataProvince" style="width: 100%" k-on-change="onLocation_ProvinceChange(kendoEvent,dataItem)"></select>', sortable: false },
            { width: '150px', title: '{{RS.CATDistrict.DistrictName}}', template: '<select ng-model="dataItem.DistrictID" k-index="0" kendo-combo-box k-data-text-field="\'DistrictName\'" k-data-value-field="\'ID\'" k-filter="\'contains\'" k-auto-bind="true" k-min-length="3" k-data-source="dataItem.DataDistrict" style="width: 100%" k-on-data-bound="onLocation_DistrictBound(kendoEvent,dataItem)"></select>', sortable: false },
            { field: 'EconomicZone', width: '200px', title: '{{RS.CATLocation.EconomicZone}}', sortable: true, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'RoutingAreaCode', width: '200px', title: '{{RS.CATLocation.RoutingAreaCode}}', template: '<input type="text" class="k-textbox "  ng-model="dataItem.RoutingAreaCode"  style="width: 100%"></input>', sortable: true, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'RouteDescription', width: '200px', title: '{{RS.ORDOrder_Excel.RouteDescription}}', sortable: true, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { title: ' ', filterable: false, sortable: false }
        ]
    }

    $scope.onLocation_ProvinceChange = function (event, item) {
        var cbb = event.sender;
        var obj = cbb.dataItem(cbb.select());
        if (Common.HasValue(obj)) {
            var data = $.grep(_ORDOrder_Excel.Data.District, function (o) { return o.ProvinceID == obj.ID });
            item.DataDistrict = data;
            item.ChangeProvice = true;
        } else {
            item.DataDistrict = [];
            item.ChangeProvice = true;
        }
    }

    $scope.onLocation_DistrictBound = function (event, item) {
        if (item.ChangeProvice) {
            item.ChangeProvice = false;
            var cbb = event.sender;
            cbb.select(0);
            $timeout(function () {
                var obj = cbb.dataItem(cbb.select());
                if (Common.HasValue(obj)) {
                    item.DistrictID = obj.ID;
                } else {
                    item.DistrictID = -1;
                }
            }, 50)
        }
    }

    Common.ALL.Get($http, {
        url: Common.ALL.URL.Province,
        success: function (res) {
            _ORDOrder_Excel.Data.Province = res;
        }
    });

    Common.ALL.Get($http, {
        url: Common.ALL.URL.District,
        success: function (res) {
            _ORDOrder_Excel.Data.District = res;
        }
    });
    
    $scope.Excel_Location_Update_Click = function ($event, win, grid) {
        $event.preventDefault();

        var data = grid.dataSource.data();
        var isValid = true;
        $.each(data, function (i, v) {
            if (!(v.DistrictID > 0 && v.ProvinceID > 0))
                isValid = false;
        })
        if (isValid) {
            $rootScope.Message({
                Type: Common.Message.Type.Confirm,
                Msg: "Xác nhận lưu?",
                Ok: function () {
                    $rootScope.IsLoading = true;
                    Common.Services.Call($http, {
                        url: Common.Services.url.ORD,
                        method: _ORDOrder_Excel.URL.Location_Create,
                        data: {
                            dataLocation: data
                        },
                        success: function (res) {
                            $rootScope.IsLoading = false;
                            Common.Services.Error(res, function () {
                                win.close();
                                $rootScope.Message({ Msg: 'Thành công! Tải lại dữ liệu excel', NotifyType: Common.Message.NotifyType.INFO });
                                $scope.Excel_Check();
                            })
                        }
                    });
                }
            });
        } else {
            $rootScope.Message({ Msg: 'Kiểm tra lại dữ liệu tỉnh thành, quận huyện!', NotifyType: Common.Message.NotifyType.WARNING });
        }
    }

    $scope.Location_Zoom_Click = function ($event, win) {
        $event.preventDefault();
        $scope.IsFullScreen = true;

        win.setOptions({ draggable: false });
        win.maximize();
        win.center().open();
    }

    $scope.Location_Minimize_Click = function ($event, win) {
        $event.preventDefault();
        $scope.IsFullScreen = false;
        win.setOptions({ draggable: true });
        win.center().open();
    }
    //#endregion
    
    //#region Product

    //Cbo GOP
    $scope.GOP_CbbOptions = {
        autoBind: true,
        valuePrimitive: true,
        ignoreCase: true,
        dataTextField: 'GroupName',
        dataValueField: 'ID',
        index: 0,
        change: function (e) {
            var val = this.value();
            var name = this.text();
            var gridEditElement = $scope.excel_product_grid.editable.element;
            var dataItem = $scope.excel_product_grid.dataItem(gridEditElement.closest('tr'))
            if (dataItem != null) {
                dataItem.GroupOfProductID = val;
                dataItem.GroupName = name;
            }
        },
        dataSource: Common.DataSource.Local({ data: [] })
    };

    //CBo UOM
    $scope.Packing_CbbOptions = {
        autoBind: true,
        valuePrimitive: true,
        ignoreCase: true,
        dataTextField: 'Code',
        dataValueField: 'ID',
        index: 0,
        change: function (e) {
            if (!Common.HasValue(e.sender.dataItem(e.item)) || this.value() == "") {
                $rootScope.Message({ Msg: 'Dữ liệu không thể thiếu', NotifyType: Common.Message.NotifyType.SUCCESS });
                $scope.ProductData.PackingID = "";
                this.open();
                this.select(0);
            } else {
                var val = this.value();
                var name = this.text();
                var gridEditElement = $scope.excel_product_grid.editable.element;
                var dataItem = $scope.excel_product_grid.dataItem(gridEditElement.closest('tr'))
                if (dataItem != null) {
                    dataItem.PackingID = val;
                    dataItem.PackingName = name;
                }
            }

        },
        dataSource: Common.DataSource.Local({ data: [] })
    };

    Common.ALL.Get($http, {
        url: Common.ALL.URL.ALL_CATPackingGOP,
        success: function (res) {
            $scope.Packing_CbbOptions.dataSource.data(res);
            $scope.IsLoading = false;
        }
    });

    //win
    $scope.Product_Click = function ($event, win) {
        if (Common.HasValue($event))
            $event.preventDefault();

        $rootScope.IsLoading = true;
        var grid = $scope.IsCO ? $scope.excel_container_grid : $scope.excel_truck_grid;
        var options = $scope.IsCO ? $scope.excel_container_gridOptions : $scope.excel_truck_gridOptions;

        var data = [];
        var temp = [];
        $.each(grid.items(), function (i, tr) {
            var o = grid.dataItem(tr);

            if (Common.HasValue(o)) {
                var objOrder = _ORDOrder_Excel.Data.FromFile[o.sID];
                if (Common.HasValue(objOrder)) {
                    if (objOrder.ListProductNew.length > 0) {
                        $.each(objOrder.ListProductNew, function (j, product) {
                            temp.push(product);
                        });
                    }
                }
            }
        });
        if (temp.length > 0)
            data.push(temp[0]);
        for (var i = 0; i < temp.length; i++) {
            var flag = false;
            for (var j = 0; j < data.length; j++) {
                if (data[j].ProductCode == temp[i].ProductCode && data[j].GroupOfProductID == temp[i].GroupOfProductID) {
                    flag = true;
                    break;
                }
            }
            if (!flag)
                data.push(temp[i]);
        }
        //cbo GOP
        var cusID = -1;
        if ($scope.Template.CustomerID != $scope.Template.SYSCustomerID) {
            cusID = $scope.Template.CustomerID;
        }
        Common.Services.Call($http, {
            url: Common.Services.url.ORD,
            method: _ORDOrder_Excel.URL.GOPRead,
            data: {
                id: cusID,
            },
            success: function (res) {
                $scope.GOP_CbbOptions.dataSource.data(res);
            }
        });

        $timeout(function () {
            win.center().open();
            $scope.excel_product_gridOptions.dataSource.data(data);
            $rootScope.IsLoading = false;
        }, 1)
    }

    $scope.excel_product_gridOptions = {
        dataSource: Common.DataSource.Local({
            data: [],
            pageSize: 0,
            model: {
                id: 'ID',
                fields: {
                    GroupOfProductID: { type: 'number'},
                    ProductCode: { type: 'string' },
                    ProductName: { type: 'string' },
                    Description: { type: 'string' },
                    PackingID: { type: 'number' },
                    Length: { type: 'number' },
                    Height: { type: 'number' },
                    Width: { type: 'number' },
                    CBM: { type: 'number' },
                    Weight: { type: 'number' },
                    IsKg: { type: 'boolean', },
                    IsDefault: { type: 'boolean', },
                    TempMin: { type: 'number' },
                    TempMax: { type: 'number' },
                }
            }
        }),
        height: '100%', groupable: false, pageable: false, sortable: true, columnMenu: false, filterable: false, resizable: true, editable: 'incell',
        columns: [
            {
                field: 'GroupOfProductID', title: 'Nhóm sản phẩm',template: "#=GroupName#", width: 150, sortable: false,
                editor: function (container, options) {
                    $('<input  class="cus-combobox" focus-k-combobox name="' + options.field + '" kendo-combo-box k-options="GOP_CbbOptions"/>').appendTo(container)
                }
            },
            {
                field: 'ProductCode', width: 150,
                title: 'Mã sản phẩm', template: '<input type="text" class="k-textbox "  ng-model="dataItem.ProductCode"  style="width: 100%"></input>',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'ProductName', width: 150,
                title: 'Sản phẩm', template: '<input type="text" class="k-textbox "  ng-model="dataItem.ProductName"  style="width: 100%"></input>',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'PackingID', title: 'UOM', template: "#=PackingName#", width: 150, sortable: false,
                editor: function (container, options) {
                    $('<input class="cus-combobox" focus-k-combobox  name="' + options.field + '" kendo-combo-box k-options="Packing_CbbOptions"/>').appendTo(container)
                }
            },
            {
                field: 'IsKg', width: 90, title: 'Tính theo Kg', attributes: { style: "text-align: center; " },
                template: "<input type='checkbox' ng-model='dataItem.IsKg' ></input>",
                filterable: {
                    cell: {
                        template: function (container) {
                            container.element.kendoComboBox({
                                dataSource: [{ Text: 'Có', Value: true }, { Text: 'Không', Value: false }, { Text: 'Tất cả', Value: '' }],
                                dataTextField: "Text",
                                dataValueField: "Value",
                            });
                        },
                        showOperators: false
                    }
                }
            },
            {
                field: 'IsDefault', width: 100, title: 'Hàng mặc định', attributes: { style: "text-align: center; " },
                template: "<input type='checkbox' ng-model='dataItem.IsDefault' ></input>",
                filterable: {
                    cell: {
                        template: function (container) {
                            container.element.kendoComboBox({
                                dataSource: [{ Text: 'Có', Value: true }, { Text: 'Không', Value: false }, { Text: 'Tất cả', Value: '' }],
                                dataTextField: "Text",
                                dataValueField: "Value",
                            });
                        },
                        showOperators: false
                    }
                }
            },
            {
                field: 'Length', width: 90, title: 'Dài (cm)', filterable: { cell: { operator: 'gte', showOperators: false } },
                template: '<input ng-model="dataItem.Length" class="k-textbox" type="number" style="width:100%" />'
            },
            {
                field: 'Width', width: 90, title: 'Rộng (cm)', filterable: { cell: { operator: 'gte', showOperators: false } },
                template: '<input ng-model="dataItem.Width" class="k-textbox" type="number" style="width:100%" />'
            },
            {
                field: 'Height', width: 90, title: 'Cao (cm)', filterable: { cell: { operator: 'gte', showOperators: false } },
                template: '<input ng-model="dataItem.Height" class="k-textbox" type="number" style="width:100%" />'
            },
            {
                field: 'CBM', width: 90, title: 'Thể tích (cm3)', filterable: { cell: { operator: 'gte', showOperators: false } },
                template: '<input ng-model="dataItem.CBM" class="k-textbox" type="number" style="width:100%" />'
            },
            {
                field: 'Weight', width: 90, title: 'Trọng tải (kg)', filterable: { cell: { operator: 'gte', showOperators: false } },
                template: '<input ng-model="dataItem.Weight" class="k-textbox" type="number" style="width:100%" />'
            },

            {
                field: 'Description', width: 150,
                title: 'Mô tả', template: '<input type="text" class="k-textbox "  ng-model="dataItem.Description"  style="width: 100%"></input>',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'TempMin', width: 120, title: 'Nhiệt độ tối đa (độ C)', filterable: { cell: { operator: 'gte', showOperators: false } },
                template: '<input ng-model="dataItem.TempMin" class="k-textbox" type="number" style="width:100%" />'
            },
            {
                field: 'TempMax', width: 120, title: 'Nhiệt độ tối thiểu (độ C)', filterable: { cell: { operator: 'gte', showOperators: false } },
                template: '<input ng-model="dataItem.TempMax" class="k-textbox" type="number" style="width:100%" />'
            },
            { title: ' ', filterable: false, sortable: false }
        ]
    }

    $scope.Excel_Product_Update_Click = function ($event, win, grid) {
        $event.preventDefault();

        var data = grid.dataSource.data();
        var isValid = true;
        $.each(data, function (i, v) {
            if (!Common.HasValue(v.GroupOfProductID) || v.GroupOfProductID < 0) {
                $rootScope.Message({ Msg: 'Chưa chọn nhóm sản phẩm', NotifyType: Common.Message.NotifyType.ERROR });
                isValid = false;
            }

            if (v.ProductCode == "") {
                $rootScope.Message({ Msg: 'Chưa nhập mã sản phẩm', NotifyType: Common.Message.NotifyType.ERROR });
                isValid = false;
            }

            if (v.ProductCode == "") {
                $rootScope.Message({ Msg: 'Chưa nhập tên sản phẩm', NotifyType: Common.Message.NotifyType.ERROR });
                isValid = false;
            }

            if (!Common.HasValue(v.PackingID) || v.PackingID < 0) {
                $rootScope.Message({ Msg: 'Chưa chọn UOM', NotifyType: Common.Message.NotifyType.ERROR });
                isValid = false;
            }
        })
        if (isValid) {
            $rootScope.Message({
                Type: Common.Message.Type.Confirm,
                Msg: "Xác nhận lưu?",
                Ok: function () {
                    $rootScope.IsLoading = true;
                    Common.Services.Call($http, {
                        url: Common.Services.url.ORD,
                        method: _ORDOrder_Excel.URL.Product_Create,
                        data: {
                            data: data
                        },
                        success: function (res) {
                            $rootScope.IsLoading = false;
                            Common.Services.Error(res, function () {
                                win.close();
                                $rootScope.Message({ Msg: 'Thành công! Tải lại dữ liệu excel', NotifyType: Common.Message.NotifyType.INFO });
                                $scope.Excel_Check();
                            })
                        }
                    });
                }
            });
        } else {
            $rootScope.Message({ Msg: 'Kiểm tra lại dữ liệu hàng hóa!', NotifyType: Common.Message.NotifyType.WARNING });
        }
    }
    //#endregion

    $scope.ShowSetting = function ($event, grid) {
        $event.preventDefault();

        $rootScope.ShowSetting({
            ListView: views.ORDOrder,
            event: $event,
            grid: grid,
            current: $state.current
        });
    };

    $scope.HideSetting = function ($event) {
        $event.preventDefault();

        $rootScope.HideSetting();
    }

    $scope.Close_Click = function ($event, win) {
        $event.preventDefault();

        win.close();
    }
}]);