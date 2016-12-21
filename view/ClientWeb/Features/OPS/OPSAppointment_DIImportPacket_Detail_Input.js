/// <reference path="~/Scripts/jquery-1.9.1.intellisense.js" />
/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />

//#region URL

var _DIImportPacket_Detail_Input = {
    URL: {
        Get: 'OPS_DI_Import_Packet_Get',
        Download: 'OPS_DI_Import_Packet_DownLoad',
        List: 'OPS_DI_Import_Packet_ORDGroupProduct_List',
        NotIn_List: 'OPS_DI_Import_Packet_ORDGroupProduct_NotIn_List',
        Delete: 'OPS_DI_Import_Packet_ORDGroupProduct_DeleteList',
        Insert: 'OPS_DI_Import_Packet_ORDGroupProduct_SaveList'
    }
}

//#endregion

angular.module('myapp').controller('OPSAppointment_DIImportPacket_Detail_InputCtrl', ['$rootScope', '$scope', '$http', '$location', '$state', '$timeout', function ($rootScope, $scope, $http, $location, $state, $timeout) {
    if ($rootScope.IsPageComplete != true) return;
    Common.Log('OPSAppointment_DIImportPacket_Detail_InputCtrl');
    $rootScope.IsLoading = false;

    $scope.IsExcelChecked = false;
    $scope.HasTemplate = false;
    $scope.HasChoose = false;
    $scope.IsClose = true;
    $scope.Template = {
        ID: -1, Name: ''
    }
    $scope.PacketID = parseInt($state.params.ID);

    try {
        Common.Services.Call($http, {
            url: Common.Services.url.OPS,
            method: _DIImportPacket_Detail_Input.URL.Get,
            data: {
                pID: $scope.PacketID
            },
            success: function (res) {
                Common.Services.Error(res, function (res) {
                    if (res.ID != $scope.PacketID) {
                        $rootScope.Message({
                            Msg: "Không tìm thấy Packet. Quay về trang trước."
                        });
                        $timeout(function () {
                            $state.go("main.OPSAppointment.DIImportPacket");
                        }, 1000)
                    } else {
                        $scope.IsClose = res.IsCreated;
                        $scope.HasTemplate = res.CUSSettingID > 0;
                        $scope.Template.ID = res.CUSSettingID;
                        $scope.Template.Name = res.CUSSettingName;
                    }
                })
            }
        })
    }
    catch (e) { }

    //MainView

    $scope.gop_GridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.OPS,
            method: _DIImportPacket_Detail_Input.URL.List,
            readparam: function () {
                return {
                    pID: $scope.PacketID
                }
            },
            pageSize: 100,
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    ETD: { type: 'date' },
                    ETA: { type: 'date' },
                    IsChoose: { type: 'bool' }
                }
            }
        }),
        height: '100%', groupable: false, pageable: Common.PageSize, sortable: true, columnMenu: false, filterable: false, resizable: true,
        dataBound: function () { $scope.HasChoose = false; },
        columns: [
            {
                title: ' ', width: '35px', filterable: false, sortable: false,
                headerAttributes: { style: 'text-align: center;' }, attributes: { style: 'text-align: center;' },
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,gop_Grid,gop_Grid_ChooseChange)" />',
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,gop_Grid,gop_Grid_ChooseChange)" />',
            },
            {
                field: 'CustomerCode', width: '150px', title: 'Khách hàng',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'OrderCode', width: '150px', title: 'Mã ĐH',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'TypeOfTransportMode', width: '100px', title: 'Loại hình v/c',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'GroupProductCode', width: '150px', title: 'Nhóm sản phẩm',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'DNCode', width: '100px', title: 'Số DN',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'SOCode', width: '100px', title: 'Số SO',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'ETD', width: '130px', title: 'ETD',
                template: "#=ETD==null?' ':Common.Date.FromJsonDMYHM(ETD)#",
                filterable: {
                    cell: {
                        template: function (e) {
                            e.element.kendoDatePicker({ format: Common.Date.Format.DMY });
                        },
                        operator: 'equal', showOperators: false
                    }
                },
            },
            {
                field: 'ETA', width: '130px', title: 'ETA',
                template: "#=ETA==null?' ':Common.Date.FromJsonDMYHM(ETA)#",
                filterable: {cell: { template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); }, operator: 'equal', showOperators: false}},
            },
            {
                field: 'LocationFromName', width: '150px', title: 'Điểm nhận hàng',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'LocationToName', width: '150px', title: 'Điểm giao hàng',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            { title: ' ', filterable: false, menu: false, sortable: false }
        ]
    }

    $scope.gop_Grid_ChooseChange = function ($event, grid, hasChoose) {
        $scope.HasChoose = hasChoose;
    }

    $scope.gop_NotIn_GridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.OPS,
            method: _DIImportPacket_Detail_Input.URL.NotIn_List,
            readparam: function () {
                return { sID: $scope.Template.ID, pID:$scope.PacketID }
            },
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number', editable: false, nullable: true },
                    IsChoose: { type: 'boolean' },
                    RequestDate: { type: 'date' },
                    CreatedDate: { type: 'date' }
                }
            }
        }),
        height: '100%', pageable: Common.PageSize, sortable: true, columnMenu: false, filterable: { mode: 'row' }, resizable: true, editable: false,
        columns: [
            {
                title: ' ', width: '35px', filterable: false, sortable: false,
                headerAttributes: { style: 'text-align: center;' }, attributes: { style: 'text-align: center;' },
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,gop_NotIn_Grid)" />',
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,gop_NotIn_Grid)" />',
            },
            {
                field: 'OrderCode', title: "Mã đơn hàng", width: 120,
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            //{
            //    field: 'ServiceOfOrder', width: 120, title: 'Loại dịch vụ',
            //    filterable: { cell: { operator: 'contains', showOperators: false } }
            //},
            {
                field: 'TypeOfTransportMode', width: 120, title: 'Loại hình v/c',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'CustomerName', width: 120, title: 'Khách hàng',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'DistributorCode', width: 120, title: 'Nhà phân phối',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'LocationFromCode', width: 120, title: 'Mã điểm nhận',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'LocationFromName', width: 120, title: 'Điểm nhận',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'LocationToCode', width: 120, title: 'Mã điểm giao',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'LocationToName', width: 120, title: 'Điểm giao',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'GroupProductCode', width: 120, title: 'Nhóm sản phẩm',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'Packing', width: 120, title: 'Sản phẩm/ĐVT',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'RequestDate', width: 160, title: 'Ngày gửi yêu cầu',
                template: "#=RequestDate==null?' ':Common.Date.FromJsonDMYHM(RequestDate)#",
                filterable: { cell: { template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); }, operator: 'equal', showOperators: false } },
            },
            {
                field: 'DNCode', width: 120, title: 'Số DN',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'SOCode', width: 120, title: 'Số SO',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            { title: ' ', filterable: false, sortable: false }
        ]
    }

    $scope.DownLoadFile_Click = function ($event, grid) {
        $event.preventDefault();
        
        var data = [];
        Common.Data.Each(grid.dataSource.data(), function (o) {
            if (o.IsChoose)
                data.push(o.ID);
        })

        $rootScope.IsLoading = true;
        Common.Services.Call($http, {
            url: Common.Services.url.OPS,
            method: _DIImportPacket_Detail_Input.URL.Download,
            data: { sID: $scope.Template.ID, pID: $scope.PacketID, data: data },
            success: function (res) {
                $rootScope.IsLoading = false;
                $rootScope.DownloadFile(res);
            },
            error: function (res) {
                $rootScope.IsLoading = false;
            }
        });
    }

    $scope.Delete_Click = function ($event, grid) {
        $event.preventDefault();

        var data = [];
        Common.Data.Each(grid.dataSource.data(), function (o) {
            if (o.IsChoose)
                data.push(o.ID);
        })

        if (data.length > 0) {
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.OPS,
                method: _DIImportPacket_Detail_Input.URL.Delete,
                data: { pID: $scope.PacketID, data: data },
                success: function (res) {
                    grid.dataSource.read();
                    $rootScope.IsLoading = false;
                },
                error: function (res) {
                    $rootScope.IsLoading = false;
                }
            });
        }
    }

    $scope.Insert_Click = function ($event, grid, win) {
        $event.preventDefault();
        grid.dataSource.read();
        win.center().open();
    }

    $scope.Insert_OK_Click = function ($event, grid, win) {
        $event.preventDefault();

        var data = [];
        Common.Data.Each(grid.dataSource.data(), function (o) {
            if (o.IsChoose)
                data.push(o.ID);
        })

        if (data.length > 0) {
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.OPS,
                method: _DIImportPacket_Detail_Input.URL.Insert,
                data: { pID: $scope.PacketID, data: data },
                success: function (res) {
                    win.close();
                    $rootScope.IsLoading = false;
                    $scope.gop_Grid.dataSource.read();
                },
                error: function (res) {
                    $rootScope.IsLoading = false;
                }
            });
        } else {
            win.close();
        }
    }

    $scope.Close_Click = function ($event, win) {
        $event.preventDefault();

        win.close();
    }
}]);