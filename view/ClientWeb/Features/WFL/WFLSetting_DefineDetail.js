/// <reference path="~/Scripts/jquery-1.9.1.intellisense.js" />
/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />

//#region Data
var _WFLSetting_DefineDetail = {
    URL: {
        Get: 'WFLSetting_DefineGet',
        StepParent: 'WFLSetting_StepParentList',
        StepChild: 'WFLSetting_StepChildList',
        Data: 'WFLSetting_DefineData',
        Save: 'WFLSetting_DefineDetailSave',
        SaveWFL: 'WFLSetting_DefineWFLSave',

        StepDefineList: 'WFLSetting_StepDefineList',
        StepDefineNotInList: 'WFLSetting_StepDefineNotInList',
        StepDefineNotInSave: 'WFLSetting_StepDefineNotInSave',
        StepDefineDelete: 'WFLSetting_StepDefineDelete',

        DefineGroupList: 'WFLSetting_DefineGroupList',
        DefineGroupNotInList: 'WFLSetting_DefineGroupNotInList',
        DefineGroupNotInSave: 'WFLSetting_DefineGroupNotInSave',
        DefineGroupDelete: 'WFLSetting_DefineGroupDelete',

        FunctionList: 'WFLSetting_DefineFunctionList',
        ActionList: 'WFLSetting_DefineActionList',

        EventList: 'WFLSetting_DefineEventList',
        EventNotInList: 'WFLSetting_DefineEventNotInList',
        EventNotInSave: 'WFLSetting_DefineEventNotInSave',
        EventDelete: 'WFLSetting_DefineEventDelete',
        EventGet: 'WFLSetting_DefineEventGet',
        EventSave: 'WFLSetting_DefineEventSave',
        EventUserRead: 'WFLSetting_DefineEventUserRead',
    },
    Data: {
        _listStep: [],
        _listFunction: [],

        _dataCustomer: [],
        _gridUserModel: {
            id: 'UserID',
            fields: {
                UserID: { type: 'number' },
                UserName: { type: 'string' },
            }
        },
        _dataUser: [],
    },
    Param: {
        ID: -1
    },
};
//#endregion

angular.module('myapp').controller('WFLSetting_DefineDetailCtrl', ['$rootScope', '$scope', '$http', '$location', '$state', '$timeout', '$stateParams', '$compile', function ($rootScope, $scope, $http, $location, $state, $timeout, $stateParams, $compile) {
    if ($rootScope.IsPageComplete != true) return;
    Common.Log('WFLSetting_DefineDetailCtrl');
    $rootScope.IsLoading = false;
    $scope.Auth = $rootScope.GetAuth();
    $scope.Item = { StepID: -1 };
    $scope.WorkFlowID = 0;
    $scope.ListFunctionID = [];
    $scope.ListActionID = [];
    $scope.EventItem = null;

    $scope.UserMailHasChoose = false;
    $scope.UserTMSHasChoose = false;
    $scope.UserSMSHasChoose = false;

    $scope.UserAddHasChoose = false;

    $scope.CusHasChoose = false;
    $scope.CusAddHasChoose = false;

    $scope.CurrentWinUser = null;


    _WFLSetting_DefineDetail.Param = $.extend(true, _WFLSetting_DefineDetail.Param, $state.params);

    Common.Services.Call($http, {
        url: Common.Services.url.WFL,
        method: _WFLSetting_DefineDetail.URL.Get,
        data: { id: _WFLSetting_DefineDetail.Param.ID },
        success: function (res) {
            $scope.Item = res;
        }
    });

    $scope.InitDiagram = function (stepID) {
        Common.Services.Call($http, {
            url: Common.Services.url.WFL,
            method: _WFLSetting_DefineDetail.URL.Data,
            data: { defineID: _WFLSetting_DefineDetail.Param.ID, stepID: stepID },
            success: function (res) {
                
                _WFLSetting_DefineDetail.Data._listStep = res.ListWLStep;
                //#region resize
                var _width = (res.ListWLStep.length + 1) * 250;
                $scope.width = _width;

                if (res.maxWFLInStep > 0) {
                    var _height = (res.maxWFLInStep + 1) * 120;
                    $scope.height = _height;
                }
                //#endregion
                $timeout(function () {
                    //diagram
                    $("#diagram").kendoDiagram({
                        layout: {
                            horizontalSeparation: 30,
                            verticalSeparation: 20,
                            width: 2000,
                        },
                        dataBound: function () {
                            this.resize();
                        },
                        zoomMax: 1,
                        zoomMin: 1,
                    });
                    var diagram = $("#diagram").data("kendoDiagram");


                    var rangeX = 250;
                    var xShape = 20;
                    //WFL
                    angular.forEach(res.ListWLStep, function (value, key) {
                        var rangeY = 80;
                        var yShape = 80;
                        var Point = kendo.dataviz.diagram.Point;
                        //#region step title
                        var data = { ID: 0 };
                        var shape = new kendo.dataviz.diagram.Shape({
                            editable: false,
                            x: xShape, y: 20, fill: "white", width: 160, height: 50, dataItem: data,
                            content: {
                                text: value.StepName, fontSize: 12,
                            },
                            stroke: { width: 0 },
                        });
                        diagram.addShape(shape);
                        var sFillColor = '#31B6FC';
                        var sTextColor = 'white';

                        var unSFillColor = 'white';
                        var unSTextColor = '#31B6FC';
                        //#endregion
                        for (var i = 0; i < value.ListWorkFlow.length; i++) {
                            var data = value.ListWorkFlow[i];
                            var Point = kendo.dataviz.diagram.Point;
                            var dataviz = kendo.dataviz;
                            if (data.IsSelected == true) {
                                var shape = new dataviz.diagram.Shape({
                                    id: data.ID,
                                    x: xShape, y: yShape, fill: sFillColor, width: 160, height: 50, dataItem: data,
                                    content: {
                                        text: data.WorkFlowName, fontSize: 12, color: sTextColor,
                                    },
                                    stroke: { width: 1, color: sFillColor },
                                    editable: {
                                        tools: [{
                                            type: "button",
                                            text: "Chọn",
                                            click: function (e) {
                                                var diagram = $("#diagram").data("kendoDiagram");
                                                if (diagram == null)
                                                    this.element.hide();
                                                else {
                                                    var item = diagram.select();
                                                    
                                                    if (item[0].dataItem.IsSelected == false) {
                                                        $scope.Save_WorkFlow(item[0],true);
                                                        //redraw
                                                        //if (item[0].dataItem.UseAllWorkFlow == false) {
                                                        //    $rootScope.Message({
                                                        //        Type: Common.Message.Type.Confirm,
                                                        //        NotifyType: Common.Message.NotifyType.SUCCESS,
                                                        //        Title: 'Thông báo',
                                                        //        Msg: 'Step chỉ chọn 1Bạn có muốn chọn? Các thiết lập sẽ bị xóa',
                                                        //        Close: null,
                                                        //        Ok: function () {
                                                        //            $scope.Save_WorkFlow(item[0].dataItem);
                                                        //        }
                                                        //    })
                                                        //    angular.forEach(_WFLSetting_DefineDetail.Data._listStep, function (value, key) {
                                                        //        if (value.StepID == item[0].dataItem.StepID) {
                                                        //            angular.forEach(value.ListWorkFlow, function (data, key) {
                                                        //                if (data.ID != item[0].dataItem.ID) {
                                                        //                    var diagram = $("#diagram").data("kendoDiagram");
                                                        //                    var shape = diagram.getShapeById(data.ID);
                                                        //                    shape.redraw({
                                                        //                        fill: {
                                                        //                            color: "white"
                                                        //                        },
                                                        //                        content: {
                                                        //                            color: "#31B6FC",
                                                        //                        },
                                                        //                    });
                                                        //                    shape.dataItem.IsSelected = false;
                                                        //                }
                                                        //            });
                                                        //        }
                                                        //    });
                                                        //}
                                                    }
                                                }
                                            }
                                        },
                                        {
                                            type: "button",
                                            text: "Hủy",
                                            click: function (e) {
                                                var diagram = $("#diagram").data("kendoDiagram");
                                                if (diagram == null)
                                                    this.element.hide();
                                                else {
                                                    var item = diagram.select();
                                                    if (item[0].dataItem.IsSelected == true) {
                                                        $rootScope.Message({
                                                            Type: Common.Message.Type.Confirm,
                                                            NotifyType: Common.Message.NotifyType.SUCCESS,
                                                            Title: 'Thông báo',
                                                            Msg: 'Bạn có muốn hủy chọn? Các thiết lập sẽ bị xóa',
                                                            Close: null,
                                                            Ok: function () {
                                                                $scope.Save_WorkFlow(item[0],false);
                                                            }
                                                        })
                                                    }
                                                }
                                            }
                                        },
                                        {
                                            type: "button",
                                            text: "Thiết lập",
                                            click: function (e) {
                                                var diagram = $("#diagram").data("kendoDiagram");
                                                if (diagram == null)
                                                    this.element.hide();
                                                else {
                                                    var item = diagram.select();
                                                    
                                                    if (item[0].dataItem.IsSelected == true) {
                                                        $scope.WorkFlowID = item[0].dataItem.ID;
                                                        $scope.Event_win.open().center();
                                                        $scope.event_GridOptions.dataSource.read();
                                                    } else {
                                                        $rootScope.Message({ Msg: 'WorkFlow chưa chọn không thể thiết lập', NotifyType: Common.Message.NotifyType.INFO });
                                                    }
                                                }
                                            }
                                        }],
                                        connect: false,
                                    },
                                    visual: function (e) {
                                        var g = new dataviz.diagram.Group();
                                        var dataItem = e.dataItem;
                                        g.append(new dataviz.diagram.Rectangle({
                                            x: 0, y: 0, fill: sFillColor, width: 160, height: 50,
                                            stroke: { width: 1, color: sFillColor },
                                            content: {
                                                text: data.WorkFlowName, fontSize: 12, color: sTextColor,
                                            },
                                        }));
                                        if (data.HasEvent) {
                                            g.append(new dataviz.diagram.Image({
                                                source: "../Images/wfl/event-calendar.png",
                                                x: 140, y: 4, width: 15, height: 15
                                            }));
                                        }
                                        return g;
                                    }
                                });
                            } else {
                                var shape = new dataviz.diagram.Shape({
                                    id: data.ID,
                                    x: xShape, y: yShape, fill: unSFillColor, width: 160, height: 50, dataItem: data,
                                    content: {
                                        text: data.WorkFlowName, fontSize: 12, color: unSTextColor,
                                    },
                                    stroke: { width: 1, color: unSTextColor },
                                    editable: {
                                        tools: [{
                                            type: "button",
                                            text: "Chọn",
                                            click: function (e) {
                                                var diagram = $("#diagram").data("kendoDiagram");
                                                if (diagram == null)
                                                    this.element.hide();
                                                else {
                                                    var item = diagram.select();
                                                    if (item[0].dataItem.IsSelected == false) {
                                                        $scope.Save_WorkFlow(item[0], true);
                                                    }
                                                    //redraw
                                                    //if (item[0].dataItem.UseAllWorkFlow == false) {
                                                    //    angular.forEach(_WFLSetting_DefineDetail.Data._listStep, function (value, key) {
                                                    //        if (value.StepID == item[0].dataItem.StepID) {
                                                    //            angular.forEach(value.ListWorkFlow, function (data, key) {
                                                    //                if (data.ID != item[0].dataItem.ID) {
                                                    //                    var diagram = $("#diagram").data("kendoDiagram");
                                                    //                    var shape = diagram.getShapeById(data.ID);
                                                    //                    shape.redraw({
                                                    //                        fill: {
                                                    //                            color: "white"
                                                    //                        },
                                                    //                        content: {
                                                    //                            color: "#31B6FC",
                                                    //                        },
                                                    //                    });
                                                    //                    shape.dataItem.IsSelected = false;
                                                    //                }
                                                    //            });
                                                    //        }
                                                    //    });
                                                    //}
                                                }
                                            }
                                        },
                                        {
                                            type: "button",
                                            text: "Hủy",
                                            click: function (e) {
                                                var diagram = $("#diagram").data("kendoDiagram");
                                                if (diagram == null)
                                                    this.element.hide();
                                                else {
                                                    var item = diagram.select();
                                                    if (item[0].dataItem.IsSelected == true) {
                                                        $rootScope.Message({
                                                            Type: Common.Message.Type.Confirm,
                                                            NotifyType: Common.Message.NotifyType.SUCCESS,
                                                            Title: 'Thông báo',
                                                            Msg: 'Bạn có muốn hủy chọn? Các thiết lập sẽ bị xóa',
                                                            Close: null,
                                                            Ok: function () {
                                                                $scope.Save_WorkFlow(item[0], false);
                                                            }
                                                        })
                                                    }
                                                }
                                            }
                                        },
                                        {
                                            type: "button",
                                            text: "Thiết lập",
                                            click: function (e) {
                                                var diagram = $("#diagram").data("kendoDiagram");
                                                if (diagram == null)
                                                    this.element.hide();
                                                else {
                                                    var item = diagram.select();

                                                    if (item[0].dataItem.IsSelected == true) {
                                                        $scope.WorkFlowID = item[0].dataItem.ID;
                                                        $scope.Event_win.open().center();
                                                        $scope.event_GridOptions.dataSource.read();
                                                    } else {
                                                        $rootScope.Message({ Msg: 'WorkFlow chưa chọn không thể thiết lập', NotifyType: Common.Message.NotifyType.INFO });
                                                    }
                                                }
                                            }
                                        }],
                                        connect: false,
                                    },
                                    visual: function (e) {
                                        var g = new dataviz.diagram.Group();
                                        var dataItem = e.dataItem;
                                        g.append(new dataviz.diagram.Rectangle({
                                            x: 0, y: 0, fill: unSFillColor, width: 160, height: 50,
                                            content: {
                                                text: data.WorkFlowName, fontSize: 12, color: unSTextColor,
                                            },
                                            stroke: { width: 1, color: unSTextColor },
                                        }));
                                        if (data.HasEvent) {
                                            g.append(new dataviz.diagram.Image({
                                                source: "../Images/wfl/event-calendar-title.png",
                                                x: 140, y: 4, width: 15, height: 15
                                            }));
                                        }
                                        return g;
                                    }
                                });
                            }
                            diagram.addShape(shape);
                            yShape = yShape + rangeY;
                        }

                        xShape = xShape + rangeX;
                    });

                    //Link
                    angular.forEach(res.ListWorkFlowLink, function (value, key) {
                        var fromId = value.WorkFlowFromID;
                        var toId = value.WorkFlowToID;
                        var shape1 = diagram.getShapeById(fromId);
                        var shape2 = diagram.getShapeById(toId);

                        var connection = new kendo.dataviz.diagram.Connection(shape1, shape2, {
                            type: "cascading",
                            stroke: { color: "Orange" },
                            hover: {
                                stroke: { color: "#31B6FC" },
                            },
                            //editable: {
                            //    tools: [{
                            //        name: "delete"
                            //    },
                            //    {
                            //        name: "edit"
                            //    }]
                            //},
                            endCap: {
                                type: "ArrowEnd",
                                fill: {
                                    color: "Orange"
                                },
                                stroke: {
                                    width: 0
                                }
                            }
                        });
                        diagram.addConnection(connection);
                    });

                    $rootScope.IsLoading = false;
                }, 20);
            }
        });
    }

    $scope.Save_WorkFlow = function (data, isSelect) {
        var sFillColor = '#31B6FC';
        var sTextColor = 'white';

        var unSFillColor = 'white';
        var unSTextColor = '#31B6FC';
        $rootScope.IsLoading = true;
        Common.Services.Call($http, {
            url: Common.Services.url.WFL,
            method: _WFLSetting_DefineDetail.URL.SaveWFL,
            data: { defineID: _WFLSetting_DefineDetail.Param.ID, item: data.dataItem },
            success: function (res) {
                $rootScope.IsLoading = false;
                $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                if (isSelect) {
                    data.redraw({
                        fill: {
                            color: "#31B6FC"
                        },
                        content: {
                            color: "white",
                        },
                    });
                    data.shapeVisual.children[0].redraw({
                        x: 0, y: 0, fill: sFillColor, width: 160, height: 50,
                        stroke: { width: 1, color: sFillColor },
                        content: {
                            text: data.dataItem.WorkFlowName, fontSize: 12, color: sTextColor,
                        },
                    });
                    if (data.dataItem.HasEvent) {
                        data.shapeVisual.children[1].redraw({
                            source: "../Images/wfl/event-calendar.png",
                            x: 140, y: 4, width: 15, height: 15
                        });
                    }
                    data.dataItem.IsSelected = true;
                } else {
                    data.redraw({
                        fill: {
                            color: "white"
                        },
                        content: {
                            color: "#31B6FC",
                        },
                    });
                    data.shapeVisual.children[0].redraw({
                        x: 0, y: 0, fill: unSFillColor, width: 160, height: 50,
                        content: {
                            text: data.WorkFlowName, fontSize: 12, color: unSTextColor,
                        },
                        stroke: { width: 1, color: unSTextColor },
                    });
                    if (data.dataItem.HasEvent) {
                        data.shapeVisual.children[1].redraw({
                            source: "../Images/wfl/event-calendar-title.png",
                            x: 140, y: 4, width: 15, height: 15
                        });
                    }
                    data.dataItem.IsSelected = false;
                }
            }
        });
    }
    $scope.Save_Click = function () {

        
        var diagram = $("#diagram").data("kendoDiagram");
        if (Common.HasValue(diagram.shapes)) {
            $rootScope.IsLoading = true;
            var list = [];
            angular.forEach(diagram.shapes, function (value, key) {
                if (value.dataItem.ID > 0) {
                    list.push(value.dataItem);
                }
            });
            Common.Services.Call($http, {
                url: Common.Services.url.WFL,
                method: _WFLSetting_DefineDetail.URL.Save,
                data: { defineID: _WFLSetting_DefineDetail.Param.ID, stepID: $scope.Item.StepID, lst: list },
                success: function (res) {
                    $rootScope.IsLoading = false;
                    $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                }
            });
        }
    }

    $scope.cboStep_options = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: "contains",
        suggest: true, dataTextField: 'StepName', dataValueField: 'ID',
        dataSource: Common.DataSource.Local([], {
            id: 'ID',
            fields: {
                StepName: { type: 'string' },
                ID: { type: 'number' },
            }
        }),
        change: function(e){
            $rootScope.IsLoading = true;
            $scope.InitDiagram(this.value());
        }
    }

    $scope.loadStepParent = function () {
        Common.Services.Call($http, {
            url: Common.Services.url.WFL,
            method: _WFLSetting_DefineDetail.URL.StepParent,
            data: { defineID: _WFLSetting_DefineDetail.Param.ID },
            success: function (res) {
                
                if (Common.HasValue(res) && res.length > 0) {
                    $scope.Item.StepID = res[0].ID;
                    $rootScope.IsLoading = true;
                    $scope.InitDiagram(res[0].ID);
                } else {
                    var item = { ID: -1, StepName: '' };
                    res = [];
                    res.push(item)
                    $scope.Item.StepID = -1;
                    $rootScope.IsLoading = true;
                    $scope.InitDiagram(0);
                }
                $scope.cboStep_options.dataSource.data(res);
            }
        });
    }
    $timeout(function () {
        $scope.loadStepParent();
    }, 1);

    $scope.TabIndex = 0;
    $scope.cog_tabstripOptions = {
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

    $scope.Config_Click = function ($event, win) {
        $event.preventDefault();
        win.center().open();
        $scope.step_GridOptions.dataSource.read();
        $scope.group_GridOptions.dataSource.read();
    }
    //#region Step
    $scope.StepHasChoose = false;
    $scope.step_GridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.WFL,
            method: _WFLSetting_DefineDetail.URL.StepDefineList,
            readparam: function () { return { defineID: _WFLSetting_DefineDetail.Param.ID } },
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number', editable: false, nullable: true },
                    IsChoose: { type: 'boolean', },
                }
            }
        }),
        height: '100%', pageable: true, sortable: true, columnMenu: false, filterable: { mode: 'row' }, resizable: true, editable: false,
        columns: [
            {
                title: ' ', width: '35px',
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,step_Grid,step_GridChoose_Change)" />',
                headerAttributes: { style: 'text-align: center;' }, attributes: { style: 'text-align: center;' },
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,step_Grid,step_GridChoose_Change)" />',
                filterable: false, sortable: false
            },
            { field: 'Code', title: "Mã bước", width: 120, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'StepName', title: "Tên bước", width: 150, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { title: ' ', filterable: false, sortable: false }
        ]
    }

    $scope.step_GridChoose_Change = function ($event, grid, hasChoose) {
        $scope.StepHasChoose = hasChoose;
    }

    $scope.step_AddNew = function ($event, win) {
        $event.preventDefault();
        win.center().open();
        $scope.stepNotIn_GridOptions.dataSource.read();
    }

    $scope.stepNotIn_GridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.WFL,
            method: _WFLSetting_DefineDetail.URL.StepDefineNotInList,
            readparam: function () { return { defineID: _WFLSetting_DefineDetail.Param.ID } },
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number', editable: false, nullable: true },
                    IsChoose: { type: 'boolean' },
                }
            }
        }),
        height: '100%', pageable: true, sortable: true, columnMenu: false, filterable: { mode: 'row' }, resizable: true, editable: false,
        columns: [
            {
                title: ' ', width: '35px',
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,stepNotIn_Grid,stepNotIn_GridChoose_Change)" />',
                headerAttributes: { style: 'text-align: center;' }, attributes: { style: 'text-align: center;' },
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,stepNotIn_Grid,stepNotIn_GridChoose_Change)" />',
                filterable: false, sortable: false
            },
            { field: 'Code', title: "Mã bước", width: 120, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'StepName', title: "Tên bước", width: 150, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { title: ' ', filterable: false, sortable: false }
        ]
    }
    $scope.StepNotInHasChoose = false;
    $scope.stepNotIn_GridChoose_Change = function ($event, grid, hasChoose) {
        $scope.StepNotInHasChoose = hasChoose;
    }

    $scope.stepNotIn_Save_Click = function ($event, win, grid) {
        $event.preventDefault();
        var data = grid.dataSource.data();
        var datasend = [];
        Common.Data.Each(data, function (o) {
            if (o.IsChoose) datasend.push(o.ID);
        })
        if (datasend.length > 0) {
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.WFL,
                method: _WFLSetting_DefineDetail.URL.StepDefineNotInSave,
                data: { defineID: _WFLSetting_DefineDetail.Param.ID, lst: datasend },
                success: function (res) {
                    $rootScope.IsLoading = false;
                    $scope.step_GridOptions.dataSource.read();
                    win.close();
                    $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                    $scope.loadStepParent();
                },
                error: function (res) {
                    $rootScope.IsLoading = false;
                }
            });

        }
    }

    $scope.step_Delete = function ($event, grid) {
        $event.preventDefault();
        var data = grid.dataSource.data();
        var datasend = [];
        Common.Data.Each(data, function (o) {
            if (o.IsChoose) datasend.push(o.ID);
        })
        if (datasend.length > 0) {
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.WFL,
                method: _WFLSetting_DefineDetail.URL.StepDefineDelete,
                data: { defineID: _WFLSetting_DefineDetail.Param.ID,lst: datasend },
                success: function (res) {
                    $rootScope.IsLoading = false;
                    $scope.step_GridOptions.dataSource.read();
                    $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                    $scope.loadStepParent();
                },
                error: function (res) {
                    $rootScope.IsLoading = false;
                }
            });

        }
    }
    //#endregion

    //#region Group
    $scope.GroupHasChoose = false;
    $scope.group_GridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.WFL,
            method: _WFLSetting_DefineDetail.URL.DefineGroupList,
            readparam: function () { return { defineID: _WFLSetting_DefineDetail.Param.ID } },
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number', editable: false, nullable: true },
                    IsChoose: { type: 'boolean', },
                }
            }
        }),
        height: '100%', pageable: true, sortable: true, columnMenu: false, filterable: { mode: 'row' }, resizable: true, editable: false,
        columns: [
            {
                title: ' ', width: '35px',
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,group_Grid,group_GridChoose_Change)" />',
                headerAttributes: { style: 'text-align: center;' }, attributes: { style: 'text-align: center;' },
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,group_Grid,group_GridChoose_Change)" />',
                filterable: false, sortable: false
            },
            { field: 'Code', title: "Mã nhóm", width: 120, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'GroupName', title: "Tên nhóm", width: 150, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { title: ' ', filterable: false, sortable: false }
        ]
    }

    $scope.group_GridChoose_Change = function ($event, grid, hasChoose) {
        $scope.GroupHasChoose = hasChoose;
    }

    $scope.group_AddNew = function ($event, win) {
        $event.preventDefault();
        win.center().open();
        $scope.groupNotIn_GridOptions.dataSource.read();
    }

    $scope.groupNotIn_GridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.WFL,
            method: _WFLSetting_DefineDetail.URL.DefineGroupNotInList,
            readparam: function () { return { defineID: _WFLSetting_DefineDetail.Param.ID } },
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number', editable: false, nullable: true },
                    IsChoose: { type: 'boolean' },
                }
            }
        }),
        height: '100%', pageable: true, sortable: true, columnMenu: false, filterable: { mode: 'row' }, resizable: true, editable: false,
        columns: [
            {
                title: ' ', width: '35px',
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,groupNotIn_Grid,groupNotIn_GridChoose_Change)" />',
                headerAttributes: { style: 'text-align: center;' }, attributes: { style: 'text-align: center;' },
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,groupNotIn_Grid,groupNotIn_GridChoose_Change)" />',
                filterable: false, sortable: false
            },
            { field: 'Code', title: "Mã nhóm", width: 120, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'GroupName', title: "Tên nhóm", width: 150, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { title: ' ', filterable: false, sortable: false }
        ]
    }
    $scope.GroupNotInHasChoose = false;
    $scope.groupNotIn_GridChoose_Change = function ($event, grid, hasChoose) {
        $scope.GroupNotInHasChoose = hasChoose;
    }

    $scope.groupNotIn_Save_Click = function ($event, win, grid) {
        $event.preventDefault();
        var data = grid.dataSource.data();
        var datasend = [];
        Common.Data.Each(data, function (o) {
            if (o.IsChoose) datasend.push(o.ID);
        })
        if (datasend.length > 0) {
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.WFL,
                method: _WFLSetting_DefineDetail.URL.DefineGroupNotInSave,
                data: { defineID: _WFLSetting_DefineDetail.Param.ID, lst: datasend },
                success: function (res) {
                    $scope.group_GridOptions.dataSource.read();
                    win.close();
                    $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                    // Load danh sách User
                    Common.Services.Call($http, {
                        url: Common.Services.url.WFL,
                        data: { defineID: _WFLSetting_DefineDetail.Param.ID },
                        method: _WFLSetting_DefineDetail.URL.EventUserRead,
                        success: function (res) {
                            if (Common.HasValue(res)) {
                                _WFLSetting_DefineDetail.Data._dataUser = res;
                            }
                            $rootScope.IsLoading = false;
                        }
                    });
                    //$scope.loadStepParent();
                },
                error: function (res) {
                    $rootScope.IsLoading = false;
                }
            });

        }
    }

    $scope.group_Delete = function ($event, grid) {
        $event.preventDefault();
        var data = grid.dataSource.data();
        var datasend = [];
        Common.Data.Each(data, function (o) {
            if (o.IsChoose) datasend.push(o.ID);
        })
        if (datasend.length > 0) {
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.WFL,
                method: _WFLSetting_DefineDetail.URL.DefineGroupDelete,
                data: { defineID: _WFLSetting_DefineDetail.Param.ID, lst: datasend },
                success: function (res) {
                    $rootScope.IsLoading = false;
                    $scope.group_GridOptions.dataSource.read();
                    $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                    //$scope.loadStepParent();
                },
                error: function (res) {
                    $rootScope.IsLoading = false;
                }
            });

        }
    }
    //#endregion

    //#region Event
    $scope.WFLSetting_win_tabOptions = {
        animation: {
            open: {
                effects: "fadeIn"
            }
        }
    };
    $scope.EventHasChoose = false;
    $scope.event_GridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.WFL,
            method: _WFLSetting_DefineDetail.URL.EventList,
            readparam: function () { return { workflowID: $scope.WorkFlowID , defineID: _WFLSetting_DefineDetail.Param.ID} },
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number', editable: false, nullable: true },
                    IsChoose: { type: 'boolean', },
                }
            }
        }),
        height: '100%', pageable: true, sortable: true, columnMenu: false, filterable: { mode: 'row' }, resizable: true, editable: false,
        columns: [
            {
                title: ' ', width: '35px',
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,event_Grid,event_GridChoose_Change)" />',
                headerAttributes: { style: 'text-align: center;' }, attributes: { style: 'text-align: center;' },
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,event_Grid,event_GridChoose_Change)" />',
                filterable: false, sortable: false
            },
            {
                title: ' ', width: '45px',
                template: '<a href="/" ng-click="EventDetail_Click($event,WFLSetting_win,dataItem)" class="k-button"><i class="fa fa-pencil"></i></a>',
                filterable: false, sortable: false
            },
            { field: 'EventCode', title: "Mã", width: 120, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'EventName', title: "Event", width: 150, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { title: ' ', filterable: false, sortable: false }
        ]
    }

    $scope.event_GridChoose_Change = function ($event, grid, hasChoose) {
        $scope.EventHasChoose = hasChoose;
    }

    $scope.EventDetail_Click = function ($event, win, data) {
        $event.preventDefault();

        $rootScope.IsLoading = true;
        Common.Services.Call($http, {
            url: Common.Services.url.WFL,
            method: _WFLSetting_DefineDetail.URL.EventGet,
            data: { id: data.ID },
            success: function (res) {
                if (Common.HasValue(res)) {
                    win.open().center();
                    $timeout(function () {
                        $rootScope.IsLoading = false;
                        $scope.EventItem = res;
                        $scope.mail_user_grid.dataSource.data(res.ListUserMail);
                        $scope.tms_user_grid.dataSource.data(res.ListUserTMS);
                        $scope.sms_user_grid.dataSource.data(res.ListUserSMS);
                        _WFLSetting_DefineDetail.Data._dataCustomer = res.ListCustomer;
                    }, 1);
                }
            }
        });
    }

    $scope.Event_AddNew = function ($event, win) {
        $event.preventDefault();
        $scope.eventNotIn_GridOptions.dataSource.read();
        win.open().center();
    }

    $scope.Event_DelClick = function ($event, grid) {
        $event.preventDefault();

        var lstid = [];
        var data = grid.dataSource.data();
        $.each(data, function (i, v) {
            if (v.IsChoose == true)
                lstid.push(v.ID);
        });
        if (lstid.length > 0) {
            $rootScope.Message({
                Type: Common.Message.Type.Confirm,
                Msg: 'Bạn muốn xóa các dữ liệu đã chọn ?',
                pars: { lst: lstid },
                Ok: function (pars) {
                    $rootScope.IsLoading = true;
                    Common.Services.Call($http, {
                        url: Common.Services.url.WFL,
                        method: _WFLSetting_DefineDetail.URL.EventDelete,
                        data: pars,
                        success: function (res) {
                            $scope.InitDiagram($scope.Item.StepID);
                            $rootScope.IsLoading = false;
                            $scope.event_GridOptions.dataSource.read();
                            $scope.event_Grid.refresh();
                            $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                        }
                    });
                }
            });
        }
    };

    $scope.EventSave_Click = function ($event, win) {
        $event.preventDefault();

        $rootScope.IsLoading = true;
        $scope.EventItem.ListUserMail = $scope.mail_user_gridOptions.dataSource.data();
        $scope.EventItem.ListUserTMS = $scope.tms_user_gridOptions.dataSource.data();
        $scope.EventItem.ListUserSMS = $scope.sms_user_gridOptions.dataSource.data();
        Common.Services.Call($http, {
            url: Common.Services.url.WFL,
            method: _WFLSetting_DefineDetail.URL.EventSave,
            data: { item: $scope.EventItem },
            success: function (res) {
                $rootScope.IsLoading = false;
                win.close();
                $scope.event_GridOptions.dataSource.read();
                $rootScope.Message({ Msg: 'Đã cập nhật' });
            }
        });

    };

    $scope.EventNotInHasChoose = false;
    $scope.eventNotIn_GridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.WFL,
            method: _WFLSetting_DefineDetail.URL.EventNotInList,
            readparam: function () { return { workflowID: $scope.WorkFlowID , defineID: _WFLSetting_DefineDetail.Param.ID} },
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number', editable: false, nullable: true },
                    IsChoose: { type: 'boolean', },
                }
            }
        }),
        height: '100%', pageable: true, sortable: true, columnMenu: false, filterable: { mode: 'row' }, resizable: true, editable: false,
        columns: [
            {
                title: ' ', width: '35px',
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,eventNotIn_Grid,eventNotIn_GridChoose_Change)" />',
                headerAttributes: { style: 'text-align: center;' }, attributes: { style: 'text-align: center;' },
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,eventNotIn_Grid,eventNotIn_GridChoose_Change)" />',
                filterable: false, sortable: false
            },
            { field: 'EventCode', title: "Mã", width: 120, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'EventName', title: "Event", width: 150, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { title: ' ', filterable: false, sortable: false }
        ]
    }

    $scope.eventNotIn_GridChoose_Change = function ($event, grid, hasChoose) {
        $scope.EventNotInHasChoose = hasChoose;
    }

    $scope.EventNotIn_Save = function ($event, win, grid) {
        $event.preventDefault();
        var data = grid.dataSource.data();
        var datasend = [];
        Common.Data.Each(data, function (o) {
            if (o.IsChoose) datasend.push(o.EventID);
        })
        if (datasend.length > 0) {
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.WFL,
                method: _WFLSetting_DefineDetail.URL.EventNotInSave,
                data: {  workflowID: $scope.WorkFlowID , defineID: _WFLSetting_DefineDetail.Param.ID, lst: datasend },
                success: function (res) {
                    $scope.InitDiagram($scope.Item.StepID);
                    $rootScope.IsLoading = false;
                    $scope.event_GridOptions.dataSource.read();
                    win.close();
                    $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                },
                error: function (res) {
                    $rootScope.IsLoading = false;
                }
            });

        }
    }

    $scope.mail_user_gridOptions = {
        dataSource: Common.DataSource.Local({
            data: [],
            model: _WFLSetting_DefineDetail.Data._gridUserModel,
        }),
        height: '99%', pageable: false, sortable: true, columnMenu: false, resizable: true, reorderable: true, filterable: { mode: 'row' },
        toolbar: kendo.template($('#WFLSetting_win_gridUserMailToolbar').html()),
        columns: [
          {
              title: ' ', width: '40px',
              headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,mail_user_grid,mail_user_gridChooseChange)" />',
              headerAttributes: { style: 'text-align: center;' },
              template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,mail_user_grid,mail_user_gridChooseChange)" />',
              templateAttributes: { style: 'text-align: center;' },
              filterable: false, sortable: false
          },
          {
              field: 'UserName', title: 'Tên người dùng',
              filterable: { cell: { operator: 'contains', showOperators: false } }
          },
          {
              field: 'Email', title: 'Email',
              filterable: { cell: { operator: 'contains', showOperators: false } }
          },
          { title: ' ', sortable: false, filterable: false, menu: false }
        ],
    };

    $scope.tms_user_gridOptions = {
        dataSource: Common.DataSource.Local({
            data: [],
            model: _WFLSetting_DefineDetail.Data._gridUserModel,
        }),
        height: '99%', pageable: false, sortable: true, columnMenu: false, resizable: true, reorderable: true, filterable: { mode: 'row' },
        toolbar: kendo.template($('#WFLSetting_win_gridUserTMSToolbar').html()),
        columns: [
          {
              title: ' ', width: '40px',
              headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,tms_user_grid,tms_user_gridChooseChange)" />',
              headerAttributes: { style: 'text-align: center;' },
              template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,tms_user_grid,tms_user_gridChooseChange)" />',
              templateAttributes: { style: 'text-align: center;' },
              filterable: false, sortable: false
          },
          {
              field: 'UserName', title: 'Tên người dùng',
              filterable: { cell: { operator: 'contains', showOperators: false } }
          },
          {
              field: 'Email', title: 'Email',
              filterable: { cell: { operator: 'contains', showOperators: false } }
          },
          { title: ' ', sortable: false, filterable: false, menu: false }
        ],
    };

    $scope.sms_user_gridOptions = {
        dataSource: Common.DataSource.Local({
            data: [],
            model: _WFLSetting_DefineDetail.Data._gridUserModel,
        }),
        height: '99%', pageable: false, sortable: true, columnMenu: false, resizable: true, reorderable: true, filterable: { mode: 'row' },
        toolbar: kendo.template($('#WFLSetting_win_gridUserSMSToolbar').html()),
        columns: [
          {
              title: ' ', width: '40px',
              headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,sms_user_grid,sms_user_gridChooseChange)" />',
              headerAttributes: { style: 'text-align: center;' },
              template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,sms_user_grid,sms_user_gridChooseChange)" />',
              templateAttributes: { style: 'text-align: center;' },
              filterable: false, sortable: false
          },
          {
              field: 'UserName', title: 'Tên người dùng',
              filterable: { cell: { operator: 'contains', showOperators: false } }
          },
          {
              field: 'Email', title: 'Email',
              filterable: { cell: { operator: 'contains', showOperators: false } }
          },
          { title: ' ', sortable: false, filterable: false, menu: false }
        ],
    };

    $scope.cus_gridOptions = {
        dataSource: Common.DataSource.Local({
            data: [],
            model: _WFLSetting_DefineDetail.Data._gridCusModel,
        }),
        height: '99%', pageable: false, sortable: true, columnMenu: false, resizable: true, reorderable: true, filterable: { mode: 'row' },
        toolbar: kendo.template($('#WFLSetting_win_gridCusToolbar').html()),
        columns: [
             {
                 title: ' ', width: '40px',
                 headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,cus_grid,cusgridChooseChange)" />',
                 headerAttributes: { style: 'text-align: center;' },
                 template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,cus_grid,cusgridChooseChange)" />',
                 templateAttributes: { style: 'text-align: center;' },
                 filterable: false, sortable: false
             },
             {
                 field: 'CustomerName', title: 'Tên khách hàng',
                 filterable: { cell: { operator: 'contains', showOperators: false } }
             },
        ],
    };

    $scope.add_user_gridOptions = {
        dataSource: Common.DataSource.Local({
            data: [],
            model: _WFLSetting_DefineDetail.Data._gridUserModel,
        }),
        height: '99%', pageable: false, sortable: true, columnMenu: false, resizable: true, reorderable: true, filterable: { mode: 'row' },
        columns: [
            {
                title: ' ', width: '40px',
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,add_user_grid,user_add_gridChooseChange)" />',
                headerAttributes: { style: 'text-align: center;' },
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,add_user_grid,user_add_gridChooseChange)" />',
                templateAttributes: { style: 'text-align: center;' },
                filterable: false, sortable: false
            },
            {
                field: 'UserName', title: 'Tên người dùng',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'Email', title: 'Email',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            { title: ' ', sortable: false, filterable: false, menu: false }
        ],
    };

    $scope.add_cus_gridOptions = {
        dataSource: Common.DataSource.Local({
            data: [],
            model: _WFLSetting_DefineDetail.Data._gridCusModel,
        }),
        height: '99%', pageable: false, sortable: true, columnMenu: false, resizable: true, reorderable: true, filterable: { mode: 'row' },
        columns: [
            {
                title: ' ', width: '40px',
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,add_cus_grid,cus_add_gridChooseChange)" />',
                headerAttributes: { style: 'text-align: center;' },
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,add_cus_grid,cus_add_gridChooseChange)" />',
                templateAttributes: { style: 'text-align: center;' },
                filterable: false, sortable: false
            },
            {
                field: 'CustomerName', title: 'Tên khách hàng',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
        ],
    };

    // Load danh sách User
    Common.Services.Call($http, {
        url: Common.Services.url.WFL,
        data: { defineID: _WFLSetting_DefineDetail.Param.ID },
        method: _WFLSetting_DefineDetail.URL.EventUserRead,
        success: function (res) {
            if (Common.HasValue(res)) {
                _WFLSetting_DefineDetail.Data._dataUser = res;
            }
        }
    });

    // User
    $scope.User_Add_Click = function ($event, grid, win, win_user) {
        $event.preventDefault();
        var lstCurrent = grid.dataSource.data();
        var lstNew = [];
        $.each(_WFLSetting_DefineDetail.Data._dataUser, function (i, v) {
            var check = false;
            $.each(lstCurrent, function (j, m) {
                if (v.ID == m.UserID) {
                    check = true;
                }
            });
            if (!check)
                lstNew.push({ UserID: v.ID, UserName: v.UserName, Email: v.Email, IsChoose: false });
        });

        $scope.add_user_gridOptions.dataSource.data(lstNew);
        win.center().open();
        $timeout(function () {
            $scope.add_user_grid.resize();
            $scope.CurrentWinUser = win_user;
        }, 10);
    }

    $scope.User_Delete_Click = function ($event, grid) {
        $event.preventDefault();
        var lstCurrent = grid.dataSource.data();
        var lstNew = [];
        $.each(lstCurrent, function (i, v) {
            if (!v.IsChoose)
                lstNew.push({ UserID: v.UserID, UserName: v.UserName, IsChoose: v.IsChoose, UserMail: v.UserMail });
        });
        $timeout(function () {
            grid.dataSource.data(lstNew);
        }, 10);
    }

    $scope.Add_User_Add_Click = function ($event, grid, win) {
        $event.preventDefault();
        var lstCurrent = grid.dataSource.data();
        var lstNew = [];

        switch ($scope.CurrentWinUser) {
            case 'Mail': lstNew = $scope.mail_user_gridOptions.dataSource.data(); break;
            case 'TMS': lstNew = $scope.tms_user_gridOptions.dataSource.data(); break;
            case 'SMS': lstNew = $scope.sms_user_gridOptions.dataSource.data(); break;
        }

        $.each(lstCurrent, function (i, v) {
            if (v.IsChoose == true) {
                lstNew.push({ UserID: v.UserID, UserName: v.UserName, Email: v.Email, IsChoose: false });
            }
        });

        $timeout(function () {
            switch ($scope.CurrentWinUser) {
                case 'Mail': lstNew = $scope.mail_user_gridOptions.dataSource.data(lstNew); break;
                case 'TMS': lstNew = $scope.tms_user_gridOptions.dataSource.data(lstNew); break;
                case 'SMS': lstNew = $scope.sms_user_gridOptions.dataSource.data(lstNew); break;
            }
            win.close();
        }, 10);
    };

    // Cus
    $scope.Cus_Add_Click = function ($event, grid, win) {
        $event.preventDefault();
        var lstCurrent = grid.dataSource.data();
        var lstNew = [];
        $.each(_WFLSetting_DefineDetail.Data._dataCustomer, function (i, v) {
            var check = false;
            $.each(lstCurrent, function (j, m) {
                if (v.ID == m.CustomerID) {
                    check = true;
                }
            });
            if (!check) {
                lstNew.push({ CustomerID: v.ID, CustomerName: v.CustomerName, IsChoose: false });
            }
        });
        $scope.add_cus_gridOptions.dataSource.data(lstNew);
        win.center().open();
        $timeout(function () {
            $scope.add_cus_grid.resize();
        }, 10);

    }

    $scope.Cus_Delete_Click = function ($event, grid) {
        $event.preventDefault();
        var lstCurrent = grid.dataSource.data();
        var lstNew = [];
        $.each(lstCurrent, function (i, v) {
            if (!v.IsChoose)
                lstNew.push({ CustomerID: v.CustomerID, CustomerName: v.CustomerName, IsChoose: v.IsChoose });
        });
        $timeout(function () {
            $scope.cus_gridOptions.dataSource.data(lstNew);
        }, 10);
    }

    $scope.Add_Cus_Add_Click = function ($event, grid, win) {
        $event.preventDefault();
        var lstCurrent = grid.dataSource.data();
        var lstNew = $scope.cus_gridOptions.dataSource.data();
        $.each(lstCurrent, function (i, v) {
            if (v.IsChoose == true) {
                v.IsChoose = false;
                lstNew.push({ CustomerID: v.CustomerID, CustomerName: v.CustomerName, IsChoose: v.IsChoose });
            }
        });

        $timeout(function () {
            $scope.cus_gridOptions.dataSource.data(lstCurrent);
            win.close();
        }, 10);
    };

    $scope.mail_user_gridChooseChange = function ($event, grid, haschoose) {
        $scope.UserMailHasChoose = haschoose;
    };

    $scope.tms_user_gridChooseChange = function ($event, grid, haschoose) {
        $scope.UserTMSHasChoose = haschoose;
    };

    $scope.sms_user_gridChooseChange = function ($event, grid, haschoose) {
        $scope.UserSMSHasChoose = haschoose;
    };

    $scope.user_add_gridChooseChange = function ($event, grid, haschoose) {
        $scope.UserAddHasChoose = haschoose;
    };

    $scope.cus_add_gridChooseChange = function ($event, grid, haschoose) {
        $scope.CusAddHasChoose = haschoose;
    };
    //#endregion


    $scope.window_Close_Click = function ($event,win) {
        $event.preventDefault();
        win.close();
    }
    $scope.Back_Click = function ($event) {
        $event.preventDefault();

        $state.go("main.WFLSetting.Index")
    }

    $scope.DiagramClick = function ($event) {
        $event.preventDefault();
        $timeout(function () {
            $(".page-body").css("height", "calc(100% - 100px)");
        }, 1);
        $timeout(function () {
            $(".page-body").css("height", "calc(100% - 59px)");
        }, 100);
    }
}]);