
/// <reference path="~/Scripts/jquery-1.9.1.intellisense.js" />
/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />

var _FLMMaintenance = {
    URL: {
        Data: 'FLMMaintenance_Data',
        ListAsset: 'FLMMaintenance_AssetList',

        VehicleTimeGet: 'FLMMaintenance_VehicleTimeGet',
        VehicleTimeSave: 'FLMMaintenance_VehicleTimeSave',
        VehicleTimeDelete: 'FLMMaintenance_VehicleTimeDelete',
        CostList: 'FLMMaintenance_CostList',
        DriverList: 'FLMMaintenance_DriverList',

        LocationList: 'FLMMaintenance_LocationList',
    },
    Data: {
        toDay: null,
    },
}

angular.module('myapp').controller('FLMMaintenance_IndexCtrl', ['$rootScope', '$scope', '$http', '$location', '$state', '$timeout', function ($rootScope, $scope, $http, $location, $state, $timeout) {
    Common.Log('FLMMaintenance_IndexCtrl');
    $rootScope.IsLoading = false;

    $scope.HasChoose = false;
    $scope.IsEdit = false;
    $scope.ItemEdit = null;
    $scope.IsShowDelete = false;
    $scope.TypeRepeatNo = true;
    $scope.Item = null;
    _FLMMaintenance.Data.toDay = new Date();

    $scope.ItemSearch = {
        ListFLMAssetID: [],
        dateFrom: new Date(_FLMMaintenance.Data.toDay.getFullYear(), _FLMMaintenance.Data.toDay.getMonth(), _FLMMaintenance.Data.toDay.getDate() - _FLMMaintenance.Data.toDay.getDay()),
        dateTo: new Date(_FLMMaintenance.Data.toDay.getFullYear(), _FLMMaintenance.Data.toDay.getMonth(), _FLMMaintenance.Data.toDay.getDate() + -_FLMMaintenance.Data.toDay.getDay() + 6)
    };

    $scope.mtsFLMAsset_Options = {
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'AssetID',
                fields: {
                    AssetID: { type: 'number' },
                    RegNo: { type: 'string' },
                    TypeOfAssetName: { type: 'string' }
                }
            }
        }),
        valuePrimitive: true, dataTextField: "RegNo", dataValueField: "AssetID", placeholder: "Chọn xe...", filter: "contains", ignoreCase: true,
        highlightFirst: true, autoClose: false,
        itemTemplate: '<span>#= RegNo #</span><span style="float:right;">#= TypeOfAssetName #</span>',
        headerTemplate: '<strong><span> Số xe </span><span style="float:right;"> Loại </span></strong>',
        change: function (e) {
        }
    }


    Common.Services.Call($http, {
        url: Common.Services.url.FLM,
        method: _FLMMaintenance.URL.ListAsset,
        data: {},
        success: function (res) {
            $scope.mtsFLMAsset_Options.dataSource.data(res.Data)
        }
    });

    $scope.main_schedulerOptions = {
        date: new Date(),
        majorTimeHeaderTemplate: kendo.template("<strong>#=kendo.toString(date, 'HH')#</strong>"),
        footer: false, snap: false,
        eventHeight: 20, majorTick: 60,
        height: '99%',
        messages: {
            today: "Hôm nay"
        },
        editable: {
            destroy: false, create: false, update: false
        },
        views: [
            {
                type: "timelineWeek",
                title: "Tuần",
                columnWidth: 50,
                selectedDateFormat: "{0:dd/MM/yyyy} - {1:dd/MM/yyyy}",
                dateHeaderTemplate: kendo.template("<strong>#=kendo.toString(date, 'dd/MM')#</strong>"),
                majorTick: 720,
                group: {
                    orientation: "vertical"
                },
            },
            {
                type: "timelineMonth",
                title: "Tháng",
                columnWidth: 50,
                selectedDateFormat: "{0:dd/MM/yyyy} - {1:dd/MM/yyyy}",
                dateHeaderTemplate: kendo.template("<strong>#=kendo.toString(date, 'dd/MM')#</strong>"),
                majorTick: 1440,
                group: {
                    orientation: "vertical"
                }
            }
        ],
        dataSource: {
            data: [],
            schema: {
                model: {
                    id: "ID",
                    fields: {
                        meetingID: { from: "ID", type: "number" },
                        title: { from: "Note", defaultValue: "No title", validation: { required: true } },
                        start: { type: "date", from: "DateFrom" },
                        end: { type: "date", from: "DateTo" },
                        attendees: { from: "AssetID" },
                    }
                }
            }
        },
        eventTemplate: $("#task-template").html(),
        group: {
            resources: ["AssetID"],
            orientation: "vertical"
        },

        dataBound: function (e) {

        },
        resources: [
            {
                field: "attendees",
                name: "AssetID",
                dataTextField: 'RegNo',
                dataValueField: 'AssetID',
                dataSource: [
                    { AssetID: -1, RegNo: " " },
                ],
                multiple: true,
            }
        ],
        navigate: function (e) {
            var schedule = this;
            $rootScope.IsLoading = true;
            $timeout(function () {
                var view = schedule.view();
                var viewName = schedule.viewName();
                var date = $scope.main_scheduler.date();
                $scope.ItemSearch.dateFrom = view.startDate();
                $scope.ItemSearch.dateTo = view.endDate();

                Common.Services.Call($http, {
                    url: Common.Services.url.FLM,
                    method: _FLMMaintenance.URL.Data,
                    data: {
                        lstAssetID: $scope.ItemSearch.ListFLMAssetID,
                        dateFrom: $scope.ItemSearch.dateFrom,
                        dateTo: $scope.ItemSearch.dateTo
                    },
                    success: function (res) {
                        var dataTask = [];
                        Common.Data.Each(res.ListTask, function (o) {
                            o.DateFrom = Common.Date.FromJson(o.DateFrom);
                            o.DateTo = Common.Date.FromJson(o.DateTo);
                            if (o.DateFrom <= o.DateTo) {
                                dataTask.push(o)
                            }
                        })

                        $scope.main_schedulerOptions.dataSource.data = dataTask;
                        if (res.ListResource.length > 0)
                            $scope.main_schedulerOptions.resources[0].dataSource = res.ListResource;
                        else $scope.main_schedulerOptions.resources[0].dataSource = [{ AssetID: -1, RegNo: " " }];
                        $rootScope.IsLoading = false;

                        $timeout(function () {
                            $scope.main_scheduler.date(date);
                            $scope.main_scheduler.view(viewName);
                            //$scope.main_scheduler.refresh();
                        }, 10)
                    },
                    error: function (e) {
                        $rootScope.IsLoading = false;
                    }
                });
            }, 10)

        },
        save: function (e) {

        }
    }

    Common.Services.Call($http, {
        url: Common.Services.url.FLM,
        method: _FLMMaintenance.URL.Data,
        data: {
            lstAssetID: $scope.ItemSearch.ListFLMAssetID,
            dateFrom: $scope.ItemSearch.dateFrom,
            dateTo: $scope.ItemSearch.dateTo
        },
        success: function (res) {
            var dataTask = [];
            var flag = 0;
            var data = [];
            Common.Data.Each(res.ListTask, function (o) {
                o.DateFrom = Common.Date.FromJson(o.DateFrom);
                o.DateTo = Common.Date.FromJson(o.DateTo);
                if (o.DateFrom <= o.DateTo) {
                    dataTask.push(o)
                }
            })
            $scope.main_schedulerOptions.dataSource.data = dataTask;
            if (res.ListResource.length>0)
                $scope.main_schedulerOptions.resources[0].dataSource = res.ListResource;
            else $scope.main_schedulerOptions.resources[0].dataSource = [{ AssetID: -1, RegNo: " " }];
        }
    });

    $scope.Event_Click = function (timeID, typeID, win, vform) {
        Common.Log("Event_Click TimeID:" + timeID + " Type:" + typeID)
        if (typeID >0) {
            $scope.LoadItem(win, timeID, vform)
        }
    }

    $scope.Search_Click = function ($event) {
        $event.preventDefault();
        $rootScope.IsLoading = true;
        var date = $scope.main_scheduler.date();
        var view = $scope.main_scheduler.view();
        var viewName = $scope.main_scheduler.viewName();
        Common.Services.Call($http, {
            url: Common.Services.url.FLM,
            method: _FLMMaintenance.URL.Data,
            data: {
                lstAssetID: $scope.ItemSearch.ListFLMAssetID,
                dateFrom: view.startDate(),
                dateTo: view.endDate(),
            },
            success: function (res) {
                var dataTask = [];
                Common.Data.Each(res.ListTask, function (o) {
                    o.DateFrom = Common.Date.FromJson(o.DateFrom);
                    o.DateTo = Common.Date.FromJson(o.DateTo);
                    if (o.DateFrom <= o.DateTo) {
                        dataTask.push(o)
                    }
                })

                $scope.main_schedulerOptions.dataSource.data = dataTask;
                if (res.ListResource.length > 0)
                    $scope.main_schedulerOptions.resources[0].dataSource = res.ListResource;
                else $scope.main_schedulerOptions.resources[0].dataSource = [{ AssetID: -1, RegNo: " " }];
                $rootScope.IsLoading = false;

                $timeout(function () {
                    $scope.main_scheduler.date(date);
                    $scope.main_scheduler.view(viewName);
                    // $scope.main_scheduler.refresh();
                }, 1)
            },
            error: function (e) {
                $rootScope.IsLoading = false;
            }
        });
    }

    $scope.FLMMaintenance_win_cbbTypeActivityOptions = {
        index: 0, autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true,
        dataTextField: 'ValueOfVar', dataValueField: 'ID',
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'Value',
                fields: {
                    ID: { type: 'number' },
                    ValueOfVar: { type: 'string' }
                }
            }
        }),
        change: function (e) {
            if (e.sender.selectedIndex >= 0) {
                var dataItem = this.dataItem(e.item);
                $timeout(function (e) {
                    $scope.Item.TypeOfActivityName = dataItem.ValueOfVar;
                }, 10);
            }
        }
    };

    $scope.FLMMaintenance_win_cbbActivityRepeatOptions = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: "contains", suggest: true,
        dataTextField: 'ValueOfVar', dataValueField: 'ID',
        dataSource: Common.DataSource.Local([], {
            id: 'ID',
            fields: {
                ValueOfVar: { type: 'string' },
                ID: { type: 'number' },
            }
        })
    }

    $scope.FLMMaintenance_win_cbbAssetOptions = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: "contains", suggest: true,
        dataTextField: 'RegNo', dataValueField: 'AssetID',
        template: '<span>#= RegNo #</span><span style="float:right;">#= TypeOfAssetName #</span>',
        headerTemplate: '<strong><span> Số xe </span><span style="float:right;"> Loại </span></strong>',
        dataSource: Common.DataSource.Local([], {
            id: 'AssetID',
            fields: {
                RegNo: { type: 'string' },
                AssetID: { type: 'number' },
                TypeOfAssetName: { type: 'string' }
            }
        }),
        change: function (e) {
            if (e.sender.selectedIndex >= 0) {
                var dataItem = this.dataItem(e.item);
                $timeout(function (e) {
                    $scope.Item.RegNo = dataItem.RegNo;
                }, 10);
            }
        }
    }

    Common.Services.Call($http, {
        url: Common.Services.url.FLM,
        method: _FLMMaintenance.URL.ListAsset,
        data: {},
        success: function (res) {
            $scope.FLMMaintenance_win_cbbAssetOptions.dataSource.data(res.Data)
        }
    });

    $scope.numTotalRepeat_Options = { format: 'n0', spinners: false, culture: 'en-US', min: 1, step: 1, decimals: 0, }

    $scope.numTotalRecall_Options = { format: 'n0', spinners: false, culture: 'en-US', min: 1, step: 1, decimals: 0, }

    Common.Services.Call($http, {
        url: Common.Services.url.CAT,
        method: Common.ALL.URL.SYSVarActivityRepeat,
        data: {},
        success: function (res) {
            _FLMMaintenance.Data._activityRepeat = res.Data;
            $scope.FLMMaintenance_win_cbbActivityRepeatOptions.dataSource.data(res.Data)
        }
    });

    Common.Services.Call($http, {
        url: Common.Services.url.CAT,
        method: Common.ALL.URL.SLI_SYSVarTypeOfActivity,
        success: function (res) {
            Common.Services.Error(res, function (res) {
                _FLMMaintenance.Data._dataTypeOfActivity = res.Data;
                $scope.FLMMaintenance_win_cbbTypeActivityOptions.dataSource.data(res.Data);
            });
        }
    });


    $scope.Add_Click = function ($event, win, vform) {
        Common.Log("Add_Click");
        $event.preventDefault();
        $scope.LoadItem(win, -1, vform);
    };

    $scope.LoadItem = function (win, id, vform) {
        Common.Log("LoadItem timeID:" + id);
        vform({ clear: true });
        $rootScope.IsLoading = true;
        Common.Services.Call($http, {
            url: Common.Services.url.FLM,
            method: _FLMMaintenance.URL.VehicleTimeGet,
            data: { actID: id },
            success: function (res) {
                $rootScope.IsLoading = false;
                $scope.Item = res;
                $scope.Item.PlanDateFrom = Common.Date.FromJson($scope.Item.PlanDateFrom)
                $scope.Item.PlanDateTo = Common.Date.FromJson($scope.Item.PlanDateTo)
                $scope.TypeRepeatNo = false;
                if (res.TotalRecall < 1) $scope.TypeRepeatNo = true;
                win.center();
                win.open();
            },
            error: function (res) {
                $rootScope.IsLoading = false;
            }
        });

    }

    $scope.Save_Click = function ($event, win, vform) {
        Common.Log("Save_Click");
        $event.preventDefault();

        if (vform()) {
            

            if ($scope.Item.PlanDateFrom == null || $scope.Item.PlanDateFrom == null ) {
                $rootScope.Message({ Msg: 'Nhập ngày sai', NotifyType: Common.Message.NotifyType.ERROR });
            } else {

                if ($scope.Item.IsLessThanDay) {
                    $scope.Item.PlanDateTo.setYear($scope.Item.PlanDateFrom.getFullYear());
                    $scope.Item.PlanDateTo.setMonth($scope.Item.PlanDateFrom.getMonth());
                    $scope.Item.PlanDateTo.setDate($scope.Item.PlanDateFrom.getDate());
                }

                if ($scope.Item.PlanDateFrom.getTime() >= $scope.Item.PlanDateTo.getTime())
                    $rootScope.Message({ Msg: 'Thời gian bắt đầu và kết thúc không chính xác', NotifyType: Common.Message.NotifyType.ERROR });
                else {
                    $rootScope.IsLoading = true;
                    Common.Services.Call($http, {
                        url: Common.Services.url.FLM,
                        method: _FLMMaintenance.URL.VehicleTimeSave,
                        data: { item: $scope.Item },
                        success: function (res) {
                            $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                            Common.Services.Call($http, {
                                url: Common.Services.url.FLM,
                                method: _FLMMaintenance.URL.Data,
                                data: {
                                    lstAssetID: $scope.ItemSearch.ListFLMAssetID,
                                    dateFrom: $scope.ItemSearch.dateFrom,
                                    dateTo: $scope.ItemSearch.dateTo
                                },
                                success: function (res) {
                                    var dataTask = [];
                                    Common.Data.Each(res.ListTask, function (o) {
                                        o.DateFrom = Common.Date.FromJson(o.DateFrom);
                                        o.DateTo = Common.Date.FromJson(o.DateTo);
                                        if (o.DateFrom <= o.DateTo) {
                                            dataTask.push(o)
                                        }
                                    })

                                    $scope.main_schedulerOptions.dataSource.data = dataTask;
                                    if (res.ListResource.length > 0)
                                        $scope.main_schedulerOptions.resources[0].dataSource = res.ListResource;
                                    else $scope.main_schedulerOptions.resources[0].dataSource = [{ AssetID: -1, RegNo: " " }];
                                    win.close();
                                    $rootScope.IsLoading = false;
                                },
                                error: function (res) {
                                    $rootScope.IsLoading = false;
                                }
                            });
                        },
                        error: function (res) {
                            $rootScope.IsLoading = false;
                        }
                    });
                }
            }
        }
    };

    $scope.Delete_Click = function ($event, win) {
        Common.Log("Delete_Click");
        $event.preventDefault();

        $rootScope.Message({
            Type: Common.Message.Type.Confirm,
            Msg: 'Bạn muốn xóa sự kiện này ?',
            Ok: function () {
                Common.Services.Call($http, {
                    url: Common.Services.url.FLM,
                    method: _FLMMaintenance.URL.VehicleTimeDelete,
                    data: { actID: $scope.Item.ID },
                    success: function (res) {
                        Common.Services.Error(res, function (res) {
                            $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                            Common.Services.Call($http, {
                                url: Common.Services.url.FLM,
                                method: _FLMMaintenance.URL.Data,
                                data: {
                                    lstAssetID: $scope.ItemSearch.ListFLMAssetID,
                                    dateFrom: $scope.ItemSearch.dateFrom,
                                    dateTo: $scope.ItemSearch.dateTo
                                },
                                success: function (res) {
                                    var dataTask = [];
                                    Common.Data.Each(res.ListTask, function (o) {
                                        o.DateFrom = Common.Date.FromJson(o.DateFrom);
                                        o.DateTo = Common.Date.FromJson(o.DateTo);
                                        if (o.DateFrom <= o.DateTo) {
                                            dataTask.push(o)
                                        }
                                    })

                                    $scope.main_schedulerOptions.dataSource.data = dataTask;
                                    if (res.ListResource.length > 0)
                                        $scope.main_schedulerOptions.resources[0].dataSource = res.ListResource;
                                    else $scope.main_schedulerOptions.resources[0].dataSource = [{ AssetID: -1, RegNo: " " }];
                                    win.close();
                                    $rootScope.IsLoading = false;
                                }
                            });
                        });
                    }
                });
            }
        });
    };

    $scope.Close_Click = function ($event, win) {
        Common.Log("Close_Click");
        $event.preventDefault();

        win.close();
    };


    $scope.Win_CloseClick = function ($event, win) {
        $event.preventDefault();
        win.close();
    }


    $scope.FLMMaintenance_ChooseLocationOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.FLM,
            method: _FLMMaintenance.URL.LocationList,
            model: {
                id: 'ID',
                fields: {
                    ID: { field: 'ID', type: 'number', editable: false, nullable: false },
                    
                }
            }
        }),
        height: '100%', pageable: true, sortable: true, columnMenu: false, filterable: { mode: 'row' }, resizable: true,selectable:"row",
        columns: [
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
                field: 'GroupOfLocationName', title: 'Loại địa điểm', width: 150,
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            { title: "", sortable: false, filterable: false }
        ]
    }

    $scope.LocationChoose_SaveClick = function ($event, win, grid) {
        $event.preventDefault();
        var item = grid.dataItem(grid.select());
        if (Common.HasValue(item)) {
           
            $scope.Item.LocationEndPlanID = item.ID;
            $scope.Item.LocationEndPlanCode = item.Code;
            $scope.Item.LocationEndPlanAddress = item.Address;
            win.close();
        }

    }

    $scope.Choose_LocationClick = function ($event,win,grid) {
        $event.preventDefault();
        win.center();
        win.open();
        grid.dataSource.read();
    }
}]);