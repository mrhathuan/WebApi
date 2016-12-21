
/// <reference path="~/Scripts/jquery-1.9.1.intellisense.js" />
/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />

var _FLMSchedule = {
    URL: {
        Read: 'FLM_Schedule_List',
        Get: 'FLM_Schedule_Get',
        Save: 'FLM_Schedule_Save',
        Delete: 'FLM_Schedule_Delete',
    },
}

angular.module('myapp').controller('FLMSchedule_IndexCtrl', ['$rootScope', '$scope', '$http', '$location', '$state', '$timeout', function ($rootScope, $scope, $http, $location, $state, $timeout) {
    Common.Log('FLMSchedule_IndexCtrl');
    $rootScope.IsLoading = false;

    $scope.Item = null;
    $scope.HasChoose = false;
    $scope.isEdited = false;
    $scope.DateFromOld = null;
    $scope.DateToOld = null;

    $scope.FLMSchedule_gridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.FLM,
            method: _FLMSchedule.URL.Read,
            model: {
                id: 'ID',
                fields:
                    {
                        ID: { field: 'ID', type: 'number', editable: false, nullable: false },
                        TotalDays: { field: 'TotalDays', type: 'number', editable: true, nullable: false, defaultValue: 1 },
                        DateFrom: { type: 'date' }, DateTo: { type: 'date' }, IsClosed: { type: 'boolean' },
                        IsChoose: { type: 'boolean' },
                        TotalDays: { type: 'number' },
                        Month: { type: 'number' },
                        Year: { type: 'number' },
                    }
            }
        }),
        height: '100%', pageable: true, sortable: true, columnMenu: false, resizable: true, reorderable: true, filterable: { mode: 'row' },
        columns: [
            //{
            //    title: ' ', width: '40px',
            //    headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,FLMSchedule_grid,FLMSchedule_gridChooseChange)" />',
            //    headerAttributes: { style: 'text-align: center;' },
            //    template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,FLMSchedule_grid,FLMSchedule_gridChooseChange)" />',
            //    templateAttributes: { style: 'text-align: center;' },
            //    filterable: false, sortable: false
            //},
            {
                title: ' ', width: '45px',
                template: //'<a href="/" ng-click="Edit_Click($event,FLMSchedule_win,FLMSchedule_win_vform,dataItem)" class="k-button"><i class="fa fa-pencil"></i></a>' +
                    //'<a href="/" ng-click="Delete_Click($event,dataItem)" class="k-button"><i class="fa fa-trash"></i></a>' +
                    '<a href="/" ng-click="Detail_Click($event)" class="k-button"><i class="fa fa-calendar"></i></a>',
                filterable: false, sortable: false
            },
            {
                field: 'DateFrom', title: 'Ngày bắt đầu', width: '150px',
                template: "#=Common.Date.FromJsonDDMMYY(DateFrom)#",
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'DateTo', title: 'Ngày kết thúc', width: '150px',
                template: "#=Common.Date.FromJsonDDMMYY(DateTo)#",
                filterable: {
                    cell: {
                        template: function (e) { e.element.kendoDatePicker({ format: Common.Date.Format.DMY }); }, operator: 'equal', showOperators: false
                    }
                },
            },
            {
                field: 'TotalDays', title: 'Số ngày', width: '150px',
                filterable: { cell: { operator: 'eq', showOperators: false } }
            },
            {
                field: 'Month', title: 'Tháng', width: '150px',
                filterable: { cell: { operator: 'eq', showOperators: false } }
            },
            {
                field: 'Year', title: 'Năm', width: '150px',
                filterable: { cell: { operator: 'eq', showOperators: false } }
            },
            {
                field: 'IsClosed', title: 'Đã đóng', width: '70px',
                template: '<input type="checkbox" #= IsClosed ? "checked=checked" : "" # disabled="disabled" />',
                filterable: false, sortable: false
            },
            {
                field: 'Note', title: 'Ghi chú', width: '200px',
                filterable: false, sortable: false
            },
            { title: ' ', sortable: false, filterable: false, menu: false }
        ]
    };

    $scope.FLMSchedule_dtpDateFromOptions = {
        format: Common.Date.Format.DDMMYY,
        parseFormats: Common.Date.ParseFormat,
        change: function () {
            
            var month = this.value().getMonth() + 1;
            $scope.Item.Month = month;

            var year = this.value().getFullYear();
            $scope.Item.Year = year;

            var DateFromOld = new Date($scope.DateFromOld);
            if ($scope.Item.ID > 0 && $scope.Item.DateFrom.getTime() != DateFromOld.getTime()) {
                $scope.isEdited = true;
            }
        }
    };

    $scope.FLMSchedule_dtpDateToOptions = {
        format: Common.Date.Format.DDMMYY,
        parseFormats: Common.Date.ParseFormat,
        change: function () {
            
            var DateToOld = new Date($scope.DateToOld);
            if ($scope.Item.ID > 0 && $scope.Item.DateTo.getTime() != DateToOld.getTime()) {
                
                $scope.isEdited = true;
            }
        }
    };
    $scope.FLMSchedule_gridChooseChange = function ($event, grid, haschoose) {
        $scope.HasChoose = haschoose;
    };

    $scope.Add_Click = function ($event, win, vform) {
        $event.preventDefault();

        $scope.LoadItem(win, -1, vform);
    };

    $scope.Edit_Click = function ($event, win, vform, data) {
        $event.preventDefault();
        $scope.LoadItem(win, data.id, vform);
    };

    $scope.LoadItem = function (win, id, vform) {
        vform({ clear: true });
        $scope.isEdited = false;
        $rootScope.IsLoading = true;
        Common.Services.Call($http, {
            url: Common.Services.url.FLM,
            method: _FLMSchedule.URL.Get,
            data: { id: id },
            success: function (res) {
                if (Common.HasValue(res)) {
                    $scope.Item = res;
                    if (res.ID > 0) {
                        $scope.DateFromOld = res.DateFrom;
                        $scope.DateToOld = res.DateTo;
                    }
                }
                $rootScope.IsLoading = false;
            }
        });

        win.center();
        win.open();
    }

    $scope.Delete_Click = function ($event, data) {
        $event.preventDefault();

        $rootScope.Message({
            Type: Common.Message.Type.Confirm,
            Msg: 'Bạn muốn xóa các dữ liệu đã chọn ?',
            Ok: function () {
                $rootScope.IsLoading = true;
                Common.Services.Call($http, {
                    url: Common.Services.url.FLM,
                    method: _FLMSchedule.URL.Delete,
                    data: {id: data.id},
                    success: function (res) {
                        $scope.FLMSchedule_gridOptions.dataSource.read();
                        $rootScope.IsLoading = false;
                        $rootScope.Message({ Msg: 'Đã xóa', NotifyType: Common.Message.NotifyType.SUCCESS });
                    }
                });
            }
        });
    };

    $scope.Save_Click = function ($event, win, grid, vform) {
        $event.preventDefault();
        if ($scope.isEdited) {
            $rootScope.Message({
                Type: Common.Message.Type.Confirm,
                Msg: 'Dữ liệu ngày đã thay đổi bạn có muốn lưu?',
                Ok: function () {
                    $scope.Save_Schedule(win, grid, vform);
                },
                Close: null
            });
        } else {
            $scope.Save_Schedule(win, grid, vform);
        }
    };

    $scope.Save_Schedule = function (win, grid, vform) {
        if (vform()) {
            var DateFrom = new Date($scope.Item.DateFrom);
            var DateTo = new Date($scope.Item.DateTo);
            var error = false; 
            var _MS_PER_DAY = 1000 * 3600 * 24;
            var length = DateTo.getTime() - DateFrom.getTime();
            if (length > 0) {
                
                if (((Math.round(length / _MS_PER_DAY)) + 1) < 7) {
                    $rootScope.Message({ Msg: 'Ngày kết thúc phải cách ngày bắt đầu 7 ngày', NotifyType: Common.Message.NotifyType.ERROR });
                    error = true;
                }
            } else {
                
                $rootScope.Message({ Msg: 'Ngày kết thúc phải lớn hơn ngày bắt đầu', NotifyType: Common.Message.NotifyType.ERROR });
                error = true;
            }
            if (!error) {
                $rootScope.IsLoading = true;
                Common.Services.Call($http, {
                    url: Common.Services.url.FLM,
                    method: _FLMSchedule.URL.Save,
                    data: { item: $scope.Item },
                    success: function (res) {
                        win.close();
                        $scope.FLMSchedule_gridOptions.dataSource.read();
                        $rootScope.IsLoading = false;
                        $rootScope.Message({ Msg: 'Đã cập nhật', NotifyType: Common.Message.NotifyType.SUCCESS });
                    }
                });
            }
        }
    }
    $scope.Close_Click = function ($event, win, vform) {
        $event.preventDefault();
        win.close();
    };

    $scope.Detail_Click = function ($event) {
        $event.preventDefault();
        
        $state.go("main.FLMSchedule.Detail", { ScheduleID: this.dataItem.ID });
    };

    $timeout(function () {
        if (!$state.current.name == 'main.FLMSchedule' && !$state.current.name == 'main.FLMSchedule.Index')
            $state.go("main.FLMSchedule.Detail");
    }, 100);

    $scope.numYear_options = { format: '#', spinners: false, culture: 'en-US', min: 0, max: 3000, step: 1, decimals: 0, }
    $scope.numMonth_options = { format: '#', spinners: false, culture: 'en-US', min: 1, max: 12, step: 1, decimals: 0, }

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

    $scope.HideSetting = function ($event) {
        $event.preventDefault();

        $rootScope.HideSetting();
    }
    //#endregion
}]);

