/// <reference path="~/Scripts/jquery-1.9.1.intellisense.js" />
/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />

//#region Data
var _WFLSetting_Function = {
    URL: {
        StepParent: 'WFLSetting_StepParentAllList',
        Data: 'WFLSetting_FunctionData',

        FunctionList: 'WFLSetting_FunctionList',
        FunctionNotInList: 'WFLSetting_FunctionNotInList',
        FunctionNotInSave: 'WFLSetting_FunctionNotInSave',
        FunctionDelete: 'WFLSetting_FunctionDelete',
        ActionList: 'WFLSetting_ActionList',
        ActionSave: 'WFLSetting_ActionSave',
        FunctionChildren: 'WFLSetting_ChildrenFlow',
        ChildrenFlow_Save: 'WFLSetting_ChildrenFlow_Save',
        ChildrenFlow_Get: 'WFLSetting_ChildrenFlow_Get',
        ChildrenFlow_Delete: 'WFLSetting_ChildrenFlow_Delete',

        EventList: 'WFLSetting_WFEventList',
        EventNotInList: 'WFLSetting_WFEvent_NotInList',
        EventNotInSaveList: 'WFLSetting_WFEvent_NotInSaveList',
        EventDeleteList: 'WFLSetting_WFEvent_DeleteList',
        EventTemplateRead: 'WFLSetting_EventTemplateRead',

        EventTableRead: 'WFLSetting_EventTableRead',
        EventFieldRead: 'WFLSetting_EventFieldRead',

        EventSysVar: 'WFLSettingEvent_SysVar',
    },
    Data: {
        _listStep: [],
        _listFunction: [],

        _dataTableFields: [],
        _dataAndOr: [],
        _dataTables: [],
        _dataOperators: [],
        _dataStatusOfOrder: [],
        _dataStatusOfPlan: [],
        _dataStatusOfDITOMaster: [],
        _dataStatusOfCOTOMaster: [],
        _dataKPIReason: [],
        _dataStatusOfAssetTimeSheet: [],
        _dataTypeOfAssetTimeSheet: [],
        _dataDITOGroupProductStatus: [],
        _dataDITOGroupProductStatusPOD: [],
        _dataDITOGroupProductStatusPOD: [],
        _dataTypeOfPaymentDITOMaster: [],
        _dataTroubleCostStatus: [],
        _dataDITOLocationStatus: [],
        _dataCOTOLocationStatus: [],
        _gridExpressionModel: {
            id: 'ID',
            fields: {
                ID: { type: 'number' },
                EventName: { type: 'string' },
                Code: { type: 'string' },
                IsApproved: { type: 'bool' },
            }
        },
    },
    Param: {
        ID: -1
    },
};
//#endregion

angular.module('myapp').controller('WFLSetting_FunctionCtrl', ['$rootScope', '$scope', '$http', '$location', '$state', '$timeout', '$stateParams', '$compile', function ($rootScope, $scope, $http, $location, $state, $timeout, $stateParams, $compile) {
    if ($rootScope.IsPageComplete != true) return;
    Common.Log('WFLSetting_FunctionCtrl');
    $rootScope.IsLoading = false;
    $scope.Auth = $rootScope.GetAuth();
    $scope.Item = { StepID: -1 };
    $scope.WorkFlowID = 0;
    $scope.FunctionID = 0;
    $scope.ChildrenID = 0;
    $scope.ChildrenItem = null;

    $scope.InitDiagram = function (stepID) {
        Common.Services.Call($http, {
            url: Common.Services.url.WFL,
            method: _WFLSetting_Function.URL.Data,
            data: { stepID: stepID },
            success: function (res) {

                _WFLSetting_Function.Data._listStep = res.ListWLStep;
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
                            var shape = new kendo.dataviz.diagram.Shape({
                                id: data.ID,
                                id: data.ID,
                                x: xShape, y: yShape, fill: unSFillColor, width: 160, height: 50, dataItem: data,
                                content: {
                                    text: data.WorkFlowName, fontSize: 12, color: unSTextColor,
                                },
                                stroke: { width: 1, color: sFillColor },
                                editable: {
                                    tools: [
                                    {
                                        type: "button",
                                        text: "Thiết lập",
                                        click: function (e) {
                                            var diagram = $("#diagram").data("kendoDiagram");
                                            if (diagram == null)
                                                this.element.hide();
                                            else {
                                                var item = diagram.select();

                                                $scope.WorkFlowID = item[0].dataItem.ID;
                                                $scope.Function_win.open().center();
                                                $timeout(function () {
                                                    $scope.functionChildrenOptions.dataSource.read();
                                                    $scope.functionInOptions.dataSource.read();
                                                    $scope.event_GridOptions.dataSource.read();
                                                }, 1);
                                            }
                                        }
                                    }],
                                    connect:false,
                                }
                            });

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
                            //editable: {
                            //    tools: [{
                            //        name: "delete"
                            //    },
                            //    {
                            //        name: "edit"
                            //    }]
                            //},
                            hover: {
                                stroke: { color: "#31B6FC" },
                            },
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
        change: function (e) {
            $rootScope.IsLoading = true;
            $scope.InitDiagram(this.value());
        }
    }

    $scope.loadStepParent = function () {
        Common.Services.Call($http, {
            url: Common.Services.url.WFL,
            method: _WFLSetting_Function.URL.StepParent,
            data: {},
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
    
    //#region Setting
    $scope.setting_tabstripOptions = {
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
    //#region Function
    $scope.functionInOptions = {
        dataSource: Common.DataSource.TreeList($http, {
            url: Common.Services.url.WFL,
            method: _WFLSetting_Function.URL.FunctionList,
            readparam: function () {
                return { workflowID: $scope.ChildrenID };
            },
            model: {
                id: 'ID',
                fields: {
                    parentId: { from: 'ParentID', type: 'number', editable: true, nullable: true },
                    ID: { type: 'number' },
                    IsChoose: { type: 'bool', defaultValue: false },
                    IsAdmin: { type: 'bool', defaultValue: false },
                    IsApproved: { type: 'bool', defaultValue: true }
                },
                expanded: false
            }
        }),
        pageable: true, sortable: false, columnMenu: false, resizable: true, reorderable: true, filterable: { mode: 'row' },
        columns: [
            {
                title: ' ', width: '100px',
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,functionIn,sysconfig_functioninChooseChange)" />',
                headerAttributes: { style: 'padding-left:21px;' },
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,functionIn,sysconfig_functioninChooseChange)" />',
                filterable: false, sortable: false
            },
            {
                title: ' ', width: '45px',
                template: '<a href="/" ng-click="Detail_Click($event,Action_win,dataItem)" class="k-button"><i class="fa fa-pencil"></i></a>',
                filterable: false, sortable: false
            },
            {
                field: 'Code', title: '{{RS.SYSFunction.Code}}', width: '120px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'FunctionName', title: '{{RS.SYSFunction.FunctionName}}', width: '200px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'Icon', title: '{{RS.SYSFunction.Icon}}', width: '100px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'IsApproved', title: '{{RS.SYSFunction.IsApproved}}', width: '100px',
                template: '<input type="checkbox" #= IsApproved ? "checked=checked" : "" # disabled="disabled" />'
            },
            {
                field: 'IsAdmin', title: '{{RS.SYSFunction.IsAdmin}}', width: '100px',
                template: '<input type="checkbox" #= IsAdmin ? "checked=checked" : "" # disabled="disabled" />'
            },
            {
                field: 'SortOrder', title: '{{RS.SYSFunction.SortOrder}}', width: '100px',
                sortable: true, filterable: false, menu: false
            },
            { title: ' ', sortable: false, filterable: false, menu: false }
        ],
        dataBound: function (e) {
        }
    };

   
    $scope.IsDel = false;
    $scope.sysconfig_functioninChooseChange = function ($event, tree, ischoose) {
        $scope.IsDel = ischoose;

        var chk = $event.target;
        var tr = $(chk).closest('tr');
        var item = tree.dataItem(tr);

        var lstID = [];
        var choose = $(chk).prop('checked');
        lstID.push(item.ID);
        angular.forEach(tree.items(), function (v, i) {
            chk = $(v).find('.chkChoose');
            tr = $(chk).closest('tr');
            item = tree.dataItem(tr);

            if (item.parentId > 0 && lstID.indexOf(item.parentId) >= 0) {
                if (choose == true) {
                    $(chk).prop('checked', true);
                    item.IsChoose = true;
                    lstID.push(item.ID);

                    if (!tr.hasClass('IsChoose'))
                        tr.addClass('IsChoose');
                }
                else {
                    $(chk).prop('checked', false);
                    item.IsChoose = false;
                    lstID.push(item.ID);

                    if (tr.hasClass('IsChoose'))
                        tr.removeClass('IsChoose');
                }
            }
        });
    };


    $scope.Detail_Click = function ($event, win, data) {
        $event.preventDefault();
        $rootScope.IsLoading = true;
        $scope.FunctionID = data.ID;
        Common.Services.Call($http, {
            url: Common.Services.url.WFL,
            method: _WFLSetting_Function.URL.ActionList,
            data: { workflowID: $scope.ChildrenID, functionID: data.ID },
            success: function (res) {
                $rootScope.IsLoading = false;
                win.open().center();
                $scope.action_GridOptions.dataSource.data(res);
            }
        });
    }


   

    $scope.Function_AddNew = function ($event, win) {
        $event.preventDefault();
        win.center().open();
        $scope.functionNotInOptions.dataSource.read();
    }

    // Danh sách các bước con
    $scope.functionChildrenOptions = {
        dataSource: Common.DataSource.TreeList($http, {
            url: Common.Services.url.WFL,
            method: _WFLSetting_Function.URL.FunctionChildren,
            readparam: function () {
                return { workflowID: $scope.WorkFlowID };
            },
            model: {
                id: 'ID',
                fields: {
                    parentId: { from: 'ParentID', type: 'number', editable: true, nullable: true },
                    ID: { type: 'number' },
                    WorkFlowName: { type: 'string' },
                    IsChoose: { type: 'bool', defaultValue: false },
                },
                expanded: false
            }
        }),
        pageable: true, sortable: false, columnMenu: false, resizable: true, reorderable: true, filterable: { mode: 'row' },
        columns: [
            {
                title: ' ', width: '45px',
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,functionChildren,sysconfig_functionchildrenChooseChange)" />',
                headerAttributes: { style: 'padding-left:21px;' },
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,functionChildren,sysconfig_functionchildrenChooseChange)" />',
                filterable: false, sortable: false
            },
            {
                title: ' ', width: '120px',
                template: '<a href="/" ng-click="Edit_Click_Children($event,children_win,dataItem,children_win_vform)" class="k-button"><i class="fa fa-pencil"></i></a>' +
                    '<a href="/" ng-click="Delete_Click_Children($event,dataItem)" class="k-button"><i class="fa fa-trash"></i></a>' +
                    '<a href="/" ng-click="Detail_Click_Children($event,FunctionIn_win,dataItem)" class="k-button"><i class="fa fa-file"></i></a>',
                filterable: false, sortable: false
            },

            {
                field: 'Code', title: 'Mã bước', width: '120px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'WorkFlowName', title: 'Tên bước', width: '100px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },

            { title: ' ', sortable: false, filterable: false, menu: false }
        ],
        dataBound: function (e) {
        }
    };

    $scope.Edit_Click_Children = function ($event, win, data, vform) {
        $event.preventDefault();
        $scope.LoadItemChildren(win, data.ID, vform);
    }

    $scope.ChildrenAddNew_Click = function ($event, win, vform) {
        $event.preventDefault();
        $scope.LoadItemChildren(win, 0, vform);
    }

    $scope.LoadItemChildren = function (win, id, vform) {
        vform({ clear: true });
        $rootScope.IsLoading = true;
        Common.Services.Call($http, {
            url: Common.Services.url.WFL,
            method: _WFLSetting_Function.URL.ChildrenFlow_Get,
            data: { 'id': id },
            success: function (res) {
                $rootScope.IsLoading = false;
                $scope.ChildrenItem = res;
                win.center();
                win.open();
            }
        });
    }

    $scope.Delete_Click_Children = function ($event, data) {
        $event.preventDefault();
        if (Common.HasValue(data)) {
            $rootScope.Message({
                Type: Common.Message.Type.Confirm,
                NotifyType: Common.Message.NotifyType.SUCCESS,
                Title: 'Thông báo',
                Msg: 'Bạn muốn xóa dữ liệu đã chọn?',
                Close: null,
                Ok: function () {
                    $rootScope.IsLoading = true;
                    Common.Services.Call($http, {
                        url: Common.Services.url.WFL,
                        method: _WFLSetting_Function.URL.ChildrenFlow_Delete,
                        data: { 'id': data.ID },
                        success: function (res) {
                            $rootScope.IsLoading = false;
                            $scope.functionChildrenOptions.dataSource.read();
                        }
                    });
                }
            })
        }
    }

    $scope.ChildrenSave_Click = function ($event, win, vform) {
        $event.preventDefault();
        if (vform()) {
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.WFL,
                method: _WFLSetting_Function.URL.ChildrenFlow_Save,
                data: { item: $scope.ChildrenItem, workflowID: $scope.WorkFlowID },
                success: function (res) {
                    win.close();
                    $rootScope.IsLoading = false;
                    $scope.functionChildrenOptions.dataSource.read();
                }
            });
        }
    }

    $scope.Detail_Click_Children = function ($event, win, data) {
        $event.preventDefault();
        $scope.ChildrenID = data.ID;
        win.open().center();
        $scope.functionInOptions.dataSource.read();
    }
    $scope.IsAdd = false;
    $scope.sysconfig_functionchildrenChooseChange = function ($event, tree, ischoose) {
        $scope.IsAdd = ischoose;

        var chk = $event.target;
        var tr = $(chk).closest('tr');
        var item = tree.dataItem(tr);

        var lstID = [];
        var choose = $(chk).prop('checked');
        lstID.push(item.ID);
        //check parent
        var parentId = item.parentId;
        if (ischoose) {
            while (Common.HasValue(parentId) && parentId > 0) {
                var parent;
                var keepGoing = true;
                angular.forEach(tree.items(), function (v, i) {
                    if (keepGoing) {
                        chk = $(v).find('.chkChoose');
                        tr = $(chk).closest('tr');
                        parent = tree.dataItem(tr);

                        if (parent.ID == parentId) {
                            $(chk).prop('checked', true);
                            parent.IsChoose = true;
                            //lstID.push(parent.ID);

                            if (!tr.hasClass('IsChoose'))
                                tr.addClass('IsChoose');
                            keepGoing = false;
                        }
                    }
                });
                parentId = parent.parentId;
            }
        }
        //check child
        angular.forEach(tree.items(), function (v, i) {
            chk = $(v).find('.chkChoose');
            tr = $(chk).closest('tr');
            item = tree.dataItem(tr);

            if (item.parentId > 0 && lstID.indexOf(item.parentId) >= 0) {
                if (choose == true) {
                    $(chk).prop('checked', true);
                    item.IsChoose = true;
                    lstID.push(item.ID);

                    if (!tr.hasClass('IsChoose'))
                        tr.addClass('IsChoose');
                }
                else {
                    $(chk).prop('checked', false);
                    item.IsChoose = false;
                    lstID.push(item.ID);

                    if (tr.hasClass('IsChoose'))
                        tr.removeClass('IsChoose');
                }
            }
        });
    };

    $scope.FunctionChildren_AddNew = function ($event, win) {
        $event.preventDefault();
        win.center().open();
        $scope.functionInOptions.dataSource.read();
    }


    $scope.functionNotInOptions = {
        dataSource: Common.DataSource.TreeList($http, {
            url: Common.Services.url.WFL,
            method: _WFLSetting_Function.URL.FunctionNotInList,
            readparam: function () {
                return { workflowID: $scope.ChildrenID };
            },
            model: {
                id: 'ID',
                fields: {
                    parentId: { from: 'ParentID', type: 'number', editable: true, nullable: true },
                    ID: { type: 'number' },
                    IsChoose: { type: 'bool', defaultValue: false },
                    IsAdmin: { type: 'bool', defaultValue: false },
                    IsApproved: { type: 'bool', defaultValue: true }
                },
                expanded: false
            }
        }),
        pageable: true, sortable: false, columnMenu: false, resizable: true, reorderable: true, filterable: { mode: 'row' },
        columns: [
            {
                title: ' ', width: '100px',
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,functionNotIn,sysconfig_functionnotinChooseChange)" />',
                headerAttributes: { style: 'padding-left:21px;' },
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,functionNotIn,sysconfig_functionnotinChooseChange)" />',
                filterable: false, sortable: false
            },
            {
                field: 'Code', title: '{{RS.SYSFunction.Code}}', width: '120px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'FunctionName', title: '{{RS.SYSFunction.FunctionName}}', width: '200px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'Icon', title: '{{RS.SYSFunction.Icon}}', width: '100px',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'IsApproved', title: '{{RS.SYSFunction.IsApproved}}', width: '100px',
                template: '<input type="checkbox" #= IsApproved ? "checked=checked" : "" # disabled="disabled" />'
            },
            {
                field: 'IsAdmin', title: '{{RS.SYSFunction.IsAdmin}}', width: '100px',
                template: '<input type="checkbox" #= IsAdmin ? "checked=checked" : "" # disabled="disabled" />'
            },
            {
                field: 'SortOrder', title: '{{RS.SYSFunction.SortOrder}}', width: '100px',
                sortable: true, filterable: false, menu: false
            },
            { title: ' ', sortable: false, filterable: false, menu: false }
        ]
    };

    $scope.IsAdd = false;
    $scope.sysconfig_functionnotinChooseChange = function ($event, tree, ischoose) {
        $scope.IsAdd = ischoose;

        var chk = $event.target;
        var tr = $(chk).closest('tr');
        var item = tree.dataItem(tr);

        var lstID = [];
        var choose = $(chk).prop('checked');
        lstID.push(item.ID);
        //check parent
        var parentId = item.parentId;
        if (ischoose) {
            while (Common.HasValue(parentId) && parentId > 0) {
                var parent;
                var keepGoing = true;
                angular.forEach(tree.items(), function (v, i) {
                    if (keepGoing) {
                        chk = $(v).find('.chkChoose');
                        tr = $(chk).closest('tr');
                        parent = tree.dataItem(tr);

                        if (parent.ID == parentId) {
                            $(chk).prop('checked', true);
                            parent.IsChoose = true;
                            //lstID.push(parent.ID);

                            if (!tr.hasClass('IsChoose'))
                                tr.addClass('IsChoose');
                            keepGoing = false;
                        }
                    }
                });
                parentId = parent.parentId;
            }
        }
        //check child
        angular.forEach(tree.items(), function (v, i) {
            chk = $(v).find('.chkChoose');
            tr = $(chk).closest('tr');
            item = tree.dataItem(tr);

            if (item.parentId > 0 && lstID.indexOf(item.parentId) >= 0) {
                if (choose == true) {
                    $(chk).prop('checked', true);
                    item.IsChoose = true;
                    lstID.push(item.ID);

                    if (!tr.hasClass('IsChoose'))
                        tr.addClass('IsChoose');
                }
                else {
                    $(chk).prop('checked', false);
                    item.IsChoose = false;
                    lstID.push(item.ID);

                    if (tr.hasClass('IsChoose'))
                        tr.removeClass('IsChoose');
                }
            }
        });
    };

    $scope.functionNotIn_Save_Click = function ($event, win, tree) {
        $event.preventDefault();

        var lst = [];
        angular.forEach(tree.dataItems(), function (v, i) {
            if (v.IsChoose)
                lst.push(v);
        });
        if (lst.length > 0) {
            $rootScope.Message({
                Type: Common.Message.Type.Confirm,
                NotifyType: Common.Message.NotifyType.SUCCESS,
                Title: 'Thông báo',
                Msg: 'Bạn muốn thêm các chức năng đã chọn?',
                Close: null,
                Ok: function () {
                    $rootScope.IsLoading = true;
                    Common.Services.Call($http, {
                        url: Common.Services.url.WFL,
                        method: _WFLSetting_Function.URL.FunctionNotInSave,
                        data: { lst: lst, workflowID: $scope.ChildrenID },
                        success: function (res) {
                            $rootScope.IsLoading = false;
                            $scope.functionInOptions.dataSource.read();
                            $scope.functionNotInOptions.dataSource.read();
                            win.close();
                        }
                    });
                }
            })
        }
    };

    $scope.Function_Delete = function ($event, tree) {
        $event.preventDefault();

        var lst = [];
        angular.forEach(tree.dataItems(), function (v, i) {
            if (v.IsChoose)
                lst.push(v);
        });
        if (lst.length > 0) {
            $rootScope.Message({
                Type: Common.Message.Type.Confirm,
                NotifyType: Common.Message.NotifyType.SUCCESS,
                Title: 'Thông báo',
                Msg: 'Bạn muốn xóa các chức năng đã chọn?',
                Close: null,
                Ok: function () {
                    $rootScope.IsLoading = true;
                    Common.Services.Call($http, {
                        url: Common.Services.url.WFL,
                        method: _WFLSetting_Function.URL.FunctionDelete,
                        data: { lst: lst, workflowID: $scope.ChildrenID, },
                        success: function (res) {
                            $rootScope.IsLoading = false;
                            $scope.functionInOptions.dataSource.read();
                            $scope.functionNotInOptions.dataSource.read();
                            $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                        }
                    });
                }
            })
        }
    };

    $scope.ActionHasChoose = false;
    $scope.action_GridOptions = {
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number', editable: false },
                }
            },
            pageSize: 20
        }),
        height: '100%', pageable: true, sortable: true, columnMenu: false, filterable: { mode: 'row' }, resizable: true, editable: false,
        columns: [
            {
                title: ' ', width: '35px',
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,action_Grid,action_GridChoose_Change)" />',
                headerAttributes: { style: 'text-align: center;' }, attributes: { style: 'text-align: center;' },
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,action_Grid,action_GridChoose_Change)" />',
                filterable: false, sortable: false
            },
            { field: 'Code', title: "Mã", width: 120, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'ActionName', title: "Action", width: 150, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { title: ' ', filterable: false, sortable: false }
        ]
    }

    $scope.action_GridChoose_Change = function ($event, grid, hasChoose) {
        $scope.ActionHasChoose = hasChoose;
    }

    $scope.Action_SaveClick = function ($event, grid, win) {
        $event.preventDefault();
        $event.preventDefault();
        var data = grid.dataSource.data();
        var datasend = [];
        Common.Data.Each(data, function (o) {
            if (o.IsChoose) datasend.push(o.ID);
        })

        $rootScope.IsLoading = true;
        Common.Services.Call($http, {
            url: Common.Services.url.WFL,
            method: _WFLSetting_Function.URL.ActionSave,
            data: { workflowID: $scope.ChildrenID, functionID: $scope.FunctionID, lst: datasend },
            success: function (res) {
                $rootScope.IsLoading = false;
                win.close();
                $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
            },
            error: function (res) {
                $rootScope.IsLoading = false;
            }
        });
    }
    //#endregion

    //#region Event
    $scope.EventTabIndex = 0;
    $scope.event_tabstripOptions = {
        animation: {
            open: { effects: "fadeIn" }
        },
        select: function (e) {
            $timeout(function () {
                $scope.EventTabIndex = angular.element(e.item).data('tabindex');
                Common.Log("Select_Tab_" + $scope.EventTabIndex);
            }, 1)
        }
    }

    $scope.EventHasChoose = false;
    $scope.event_GridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.WFL,
            method: _WFLSetting_Function.URL.EventList,
            readparam: function () { return { workflowID: $scope.WorkFlowID } },
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
            { field: 'Code', title: "Mã", width: 200, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'EventName', title: "Event", filterable: { cell: { operator: 'contains', showOperators: false } } },
        ]
    }

    $scope.event_GridChoose_Change = function ($event, grid, hasChoose) {
        $scope.EventHasChoose = hasChoose;
    }

    $scope.Event_AddNew = function ($event, win) {
        $event.preventDefault();
        win.open();
        win.center();
        $scope.eventNotIn_GridOptions.dataSource.read();
    }

    $scope.EventNotInHasChoose = false;
    $scope.eventNotIn_GridOptions = {
        dataSource: Common.DataSource.Grid($http, {
            url: Common.Services.url.WFL,
            method: _WFLSetting_Function.URL.EventNotInList,
            readparam: function () { return { workflowID: $scope.WorkFlowID } },
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
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,eventNotIn_Grid,event_NotInGridChoose_Change)" />',
                headerAttributes: { style: 'text-align: center;' }, attributes: { style: 'text-align: center;' },
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,eventNotIn_Grid,event_NotInGridChoose_Change)" />',
                filterable: false, sortable: false
            },
            { field: 'Code', title: "Mã", width: 200, filterable: { cell: { operator: 'contains', showOperators: false } } },
            { field: 'EventName', title: "Event", filterable: { cell: { operator: 'contains', showOperators: false } } },
        ]
    }

    $scope.event_NotInGridChoose_Change = function ($event, grid, hasChoose) {
        $scope.EventNotInHasChoose = hasChoose;
    }

    $scope.eventNotIn_Save_Click = function ($event, win, grid) {
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
                method: _WFLSetting_Function.URL.EventNotInSaveList,
                data: { workflowID: $scope.WorkFlowID, lst: datasend },
                success: function (res) {
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

    $scope.event_DeleteList = function ($event, grid) {
        $event.preventDefault();
        var data = grid.dataSource.data();
        var datasend = [];
        Common.Data.Each(data, function (o) {
            if (o.IsChoose) datasend.push(o.ID);
        })
        if (datasend.length > 0) {
            $rootScope.Message({
                Type: Common.Message.Type.Confirm,
                Msg: 'Bạn muốn xóa các dữ liệu đã chọn ?',
                pars: { lst: datasend },
                Ok: function (pars) {
                    $rootScope.IsLoading = true;
                    Common.Services.Call($http, {
                        url: Common.Services.url.WFL,
                        method: _WFLSetting_Function.URL.EventDeleteList,
                        data: { lst: datasend },
                        success: function (res) {
                            $rootScope.IsLoading = false;
                            $scope.event_GridOptions.dataSource.read();
                            $rootScope.Message({ Msg: 'Thành công', NotifyType: Common.Message.NotifyType.SUCCESS });
                        },
                        error: function (res) {
                            $rootScope.IsLoading = false;
                        }
                    });
                }
            });
        }
    }
    // Load danh sách Table
    Common.Services.Call($http, {
        url: Common.Services.url.WFL,
        method: _WFLSetting_Function.URL.EventTableRead,
        success: function (res) {
            if (Common.HasValue(res)) {
                _WFLSetting_Function.Data._dataTables = [];
                _WFLSetting_Function.Data._dataTableID = [];
                $.each(res, function (i, v) {
                    if (v.TableName != "") {
                        _WFLSetting_Function.Data._dataTables.push({ Code: v.TableName, Name: v.TableDescription });
                        _WFLSetting_Function.Data._dataTableID[i] = v.TableName;
                    }
                });

                if (_WFLSetting_Function.Data._dataTables.length > 0 && Common.HasValue(_WFLSetting_Function.Data._dataFields) && _WFLSetting_Function.Data._dataFields.length > 0)
                    $scope.InitTableFieldData();
            }
        }
    });

    // Load danh sách Field
    Common.Services.Call($http, {
        url: Common.Services.url.WFL,
        method: _WFLSetting_Function.URL.EventFieldRead,
        success: function (res) {
            if (Common.HasValue(res)) {
                _WFLSetting_Function.Data._dataFields = [];
                $.each(res, function (i, v) {
                    _WFLSetting_Function.Data._dataFields.push({ TableCode: v.TableName, Code: v.ColumnName, Name: v.ColumnDescription, Type: v.ColumnType, IsApproved: v.IsApproved, ID: v.ID });
                });
                if (_WFLSetting_Function.Data._dataTables.length > 0 && _WFLSetting_Function.Data._dataFields.length > 0)
                    $scope.InitTableFieldData();
            }
        }
    });

    $scope.InitBaseData = function () {
        Common.Log("InitBaseData");
        // Clear data
        _WFLSetting_Function.Data._dataAndOr = [];
        _WFLSetting_Function.Data._dataBool = [];
        _WFLSetting_Function.Data._dataOperators = [];

        // Tạo data cho And/Or
        _WFLSetting_Function.Data._dataAndOr.push({ ID: 1, Code: "", Name: "" });
        _WFLSetting_Function.Data._dataAndOr.push({ ID: 2, Code: "And", Name: "And" });
        _WFLSetting_Function.Data._dataAndOr.push({ ID: 3, Code: "Or", Name: "Or" });

        //Tao data cho bool
        _WFLSetting_Function.Data._dataBool.push({ ID: 1, Code: "null", Name: "null" });
        _WFLSetting_Function.Data._dataBool.push({ ID: 2, Code: "true", Name: "true" });
        _WFLSetting_Function.Data._dataBool.push({ ID: 3, Code: "false", Name: "false" });

        // Tạo data cho Operator
        _WFLSetting_Function.Data._dataOperators.push({ ID: 1, Code: "Equal", Name: "=", Description: "Bằng" });
        _WFLSetting_Function.Data._dataOperators.push({ ID: 2, Code: "NotEqual", Name: "<>", Description: "Khác" });
        _WFLSetting_Function.Data._dataOperators.push({ ID: 3, Code: "Great", Name: ">", Description: "Lớn hơn" });
        _WFLSetting_Function.Data._dataOperators.push({ ID: 4, Code: "Less", Name: "<", Description: "Nhỏ hơn" });
        _WFLSetting_Function.Data._dataOperators.push({ ID: 5, Code: "GreaterOrEqual", Name: ">=", Description: "Lớn hơn or bằng" });
        _WFLSetting_Function.Data._dataOperators.push({ ID: 6, Code: "LesserOrEqual", Name: "<=", Description: "Bé hơn or bằng" });
        _WFLSetting_Function.Data._dataOperators.push({ ID: 7, Code: "EqualField", Name: "= [Field]", Description: "Bằng với" });
        _WFLSetting_Function.Data._dataOperators.push({ ID: 8, Code: "NotEqualField", Name: "<> [Field]", Description: "Khác với" });
        _WFLSetting_Function.Data._dataOperators.push({ ID: 9, Code: "GreatField", Name: "> [Field]", Description: "Lớn hơn so với" });
        _WFLSetting_Function.Data._dataOperators.push({ ID: 10, Code: "LessField", Name: "< [Field]", Description: "Nhỏ hơn so với" });
        _WFLSetting_Function.Data._dataOperators.push({ ID: 11, Code: "GreaterOrEqualField", Name: ">= [Field]", Description: "Lớn hơn or bằng so với" });
        _WFLSetting_Function.Data._dataOperators.push({ ID: 12, Code: "LesserOrEqualField", Name: "<= [Field]", Description: "Nhỏ hơn or bằng so với" });
    };

    $scope.InitBaseData();

    $scope.InitTableFieldData = function () {
        Common.Log("InitTableFieldData");
        _WFLSetting_Function.Data._dataTableFields = [];
        var lstTableID = _WFLSetting_Function.Data._dataTableID;
        var lstField = _WFLSetting_Function.Data._dataFields;
        var lstTable = _WFLSetting_Function.Data._dataTables;
        if (lstTable.length > 0 && lstField.length > 0) {
            $.each(lstField, function (i, v) {
                var tableCode = v.TableCode;
                var idx = lstTableID.indexOf(tableCode);
                if (idx >= 0) {
                    if (!Common.HasValue(_WFLSetting_Function.Data._dataTableFields[idx]))
                        _WFLSetting_Function.Data._dataTableFields[idx] = [];
                    _WFLSetting_Function.Data._dataTableFields[idx].push(v);
                }
            });
        }
        //$scope.cboTableOptions.dataSource.data(_WFLSetting_Function.Data._dataTables);
    };

    $scope.gridExpressionOptions = {
        dataSource: Common.DataSource.Local({
            data: [],
            model: _WFLSetting_Function.Data._gridExpressionModel,
        }),
        height: '99%', pageable: false, sortable: true, columnMenu: false, resizable: true, reorderable: true, filterable: { mode: 'row' },
        toolbar: kendo.template($('#WFLSetting_win_gridExpressionToolbar').html()),
        columns: [
            {
                title: ' ', width: '40px',
                headerTemplate: '<input class="chkSelectAll" type="checkbox" ng-click="gridChooseAll_Check($event,gridExpression,gridExpressionChooseChange)" />',
                headerAttributes: { style: 'text-align: center;' },
                template: '<input class="chkChoose" type="checkbox" #= IsChoose ? "checked=checked" : "" # ng-click="gridChoose_Check($event,gridExpression,gridExpressionChooseChange)" />',
                templateAttributes: { style: 'text-align: center;' },
                filterable: false, sortable: false
            },
            {
                field: 'OperatorCode', width: '80px', headerTemplate: '<span title="Lựa chọn And/Or">And/Or</span>',
                template: '<input focus-k-combobox class="cus-combobox cboOperator" data-bind="value:OperatorCode" value="#=OperatorCode#"/>',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'TableCode', headerTemplate: '<span title="Lựa chọn bảng">Bảng dữ liệu</span>', width: '150px',
                template: '<input focus-k-combobox class="cus-combobox cboTable" data-bind="value:TableCode" value="#=TableCode#"/>',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'FieldID', headerTemplate: '<span title="Lựa chọn trường trong bảng">Trường dữ liệu</span>', width: '150px',
                template: '<input focus-k-combobox class="cus-combobox cboField"  data-bind="value:FieldID" value="#=FieldID#"/>',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'OperatorValue', headerTemplate: '<span title="Lựa chọn kiểu so sánh">Kiểu so sánh</span>', width: '100px',
                template: '<input focus-k-combobox class="cus-combobox cboOperatorValue" data-bind="value:OperatorValue" value="#=OperatorValue#"/>',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'CompareValue', headerTemplate: '<span title="Lựa chọn giá trị so sánh">Giá trị so sánh</span>', width: '180px',
                template: '<input class="clText k-textbox" type="text" k-ng-model="dataItem.CompareValue" style="width:100%; display:none"/>' +
                    '<input kendo-numeric-text-box class="clNumber" k-min="0" k-ng-model="dataItem.CompareValue" style="width:100%; display:none"/>' +
                    '<input focus-k-combobox class="cus-combobox clBool" data-bind="value:CompareValue" value="#=CompareValue#" style="width:100%; display:none"/>' +
                    '<input focus-k-combobox class="cus-combobox clFieldChoose" data-bind="value:CompareValue" value="#=CompareValue#" style="width:100%; display:none"/>' +
                    '<input focus-k-combobox class="cus-datetimepicker clDate" kendo-date-picker k-options="DateDMY" ng-model="dataItem.CompareValue" style="width:100%"/>' +
                    '<input focus-k-combobox class="cus-combobox clSysVar" data-bind="value:CompareValue" value="#=CompareValue#" style="width:100%; display:none"/>',
                filterable: { cell: { operator: 'contains', showOperators: false } }
            },
            {
                field: 'IsModified', title: 'Thay đổi', width: '90px',
                template: '<input type="checkbox" #= IsModified ? "checked=checked" : "" # ng-model="dataItem.IsModified" />',
                filterable: false, sortable: false
            },
            { title: ' ', sortable: false, filterable: false, menu: false }
        ],
        dataBound: function () {
            this.items().each(function () {
                var itemRow = $scope.gridExpression.dataItem($(this));
                var lstTableID = _WFLSetting_Function.Data._dataTableID;
                var idx = lstTableID.indexOf(itemRow.TableCode);
                var dataSysVar = $scope.loadDataCompareValue(this, itemRow);
                $(this).find('input.cboOperator').kendoComboBox({
                    autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true, minLength: 3,
                    dataTextField: 'Name', dataValueField: 'Code',
                    dataSource: Common.DataSource.Local({
                        data: _WFLSetting_Function.Data._dataAndOr,
                        model: {
                            id: 'Code',
                            fields: {
                                Code: { type: 'string' },
                                Name: { type: 'string' },
                            }
                        }
                    }),
                    change: function () {
                        var tr = $(this.wrapper).closest('tr');
                        var item = $scope.gridExpression.dataItem(tr);
                        item.OperatorCode = this.value();
                    }
                });
                $(this).find('input.cboTable').kendoComboBox({
                    autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true, minLength: 3,
                    dataTextField: 'Name', dataValueField: 'Code',
                    dataSource: Common.DataSource.Local({
                        data: _WFLSetting_Function.Data._dataTables,
                        model: {
                            id: 'Code',
                            fields: {
                                Code: { type: 'string' },
                                Name: { type: 'string' },
                            }
                        }
                    }),
                    change: function () {
                        var tr = $(this.wrapper).closest('tr');
                        var item = $scope.gridExpression.dataItem(tr);
                        item.TableCode = this.value();
                        var lstTableID = _WFLSetting_Function.Data._dataTableID;
                        var idx = lstTableID.indexOf(item.TableCode);
                        if (idx >= 0) {
                            var cboField = $($(tr).find('input.cboField')[1]).data("kendoComboBox");
                            cboField.dataSource.data(_WFLSetting_Function.Data._dataTableFields[idx]);
                            item.FieldID = _WFLSetting_Function.Data._dataTableFields[idx][0].ID;
                            item.FieldCode = _WFLSetting_Function.Data._dataTableFields[idx][0].Code;
                            cboField.text(_WFLSetting_Function.Data._dataTableFields[idx][0].Name);
                            cboField.trigger("change");
                        }
                    }
                });

                $(this).find('input.cboField').kendoComboBox({
                    autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true, minLength: 3,
                    dataTextField: 'Name', dataValueField: 'ID',
                    dataSource: Common.DataSource.Local({
                        data: _WFLSetting_Function.Data._dataTableFields[idx],
                        model: {
                            id: 'ID',
                            fields: {
                                ID: { type: 'number' },
                                Name: { type: 'string' },
                            }
                        }
                    }),
                    change: function () {
                        var tr = $(this.wrapper).closest('tr');
                        var item = $scope.gridExpression.dataItem(tr);
                        item.FieldID = this.value();
                        item.FieldCode = this.dataItem().Code;
                        item.CompareValue = null;
                        item.Type = this.dataItem().Type;
                        var lstTableID = _WFLSetting_Function.Data._dataTableID;
                        var idx = lstTableID.indexOf(item.TableCode);

                        if (!$scope.CheckFieldValue(item.OperatorValue)) {
                            $scope.setCheckValueInput(tr, item);
                            $scope.getCompareValue(tr, item)
                        }
                        else {
                            $(tr).find('input.clFieldChoose').val("");
                            $scope.getFieldValFalse(_WFLSetting_Function.Data._dataTableFields[idx], type, tr);
                        }
                    }
                });

                $(this).find('input.cboOperatorValue').kendoComboBox({
                    autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true, minLength: 3,
                    dataTextField: 'Name', dataValueField: 'Code',
                    dataSource: Common.DataSource.Local({
                        data: _WFLSetting_Function.Data._dataOperators,
                        model: {
                            id: 'Code',
                            fields: {
                                Code: { type: 'string' },
                                Name: { type: 'string' },
                            }
                        }
                    }),
                    change: function () {
                        var tr = $(this.wrapper).closest('tr');
                        var item = $scope.gridExpression.dataItem(tr);
                        item.OperatorValue = this.value();
                        var lstTableID = _WFLSetting_Function.Data._dataTableID;
                        var idx = lstTableID.indexOf(item.TableCode);
                        //kiem tra du lieu dau vao
                        if ($scope.CheckFieldValue(item.OperatorValue)) {
                            $(tr).find('input.clFieldChoose').val("");
                            $scope.getFieldValFalse(_WFLSetting_Function.Data._dataTableFields[idx], type, tr);
                        }
                        else {
                            $scope.setCheckValueInput(tr, item);
                            $scope.getCompareValue(tr, item);
                        }
                    }
                });

                $(this).find('input.clBool').kendoComboBox({
                    autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true, minLength: 3,
                    dataTextField: 'Name', dataValueField: 'Code',
                    dataSource: Common.DataSource.Local({
                        data: _WFLSetting_Function.Data._dataBool,
                        model: {
                            id: 'Code',
                            fields: {
                                Code: { type: 'string' },
                                Name: { type: 'string' },
                            }
                        }
                    }),
                    change: function () {
                        var tr = $(this.wrapper).closest('tr');
                        var item = $scope.gridExpression.dataItem(tr);
                        item.CompareValue = this.value();
                    }
                });

                $(this).find('input.clFieldChoose').kendoComboBox({
                    autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true, minLength: 3,
                    dataTextField: 'Name', dataValueField: 'Code',
                    dataSource: Common.DataSource.Local({
                        data: [],
                        model: {
                            id: 'Code',
                            fields: {
                                Code: { type: 'string' },
                                Name: { type: 'string' },
                            }
                        }
                    }),
                    change: function () {
                        var tr = $(this.wrapper).closest('tr');
                        var item = $scope.gridExpression.dataItem(tr);
                        item.CompareValue = this.value();
                    }
                });

                $(this).find('input.clSysVar').kendoComboBox({
                    autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true, minLength: 3,
                    dataTextField: 'ValueOfVar', dataValueField: 'ID',
                    dataSource: Common.DataSource.Local({
                        data: dataSysVar,
                        model: {
                            id: 'ID',
                            fields: {
                                ID: { type: 'number' },
                                ValueOfVar: { type: 'string' },
                            }
                        }
                    }),
                    change: function () {
                        var tr = $(this.wrapper).closest('tr');
                        var item = $scope.gridExpression.dataItem(tr);
                        item.CompareValue = this.value();
                    }
                });

                if (Common.HasValue($scope.gridExpression)) {
                    var item = $scope.gridExpression.dataItem(this);
                    //kiem tra du lieu vao comparevalue
                    if (Common.HasValue(item)) {
                        if (!$scope.CheckFieldValue(item.OperatorValue)) {
                            $scope.getCompareValue(this, item);
                        }
                        else {
                            $scope.getFieldValFalse(_WFLSetting_Function.Data._dataTableFields[idx], item.Type, this);
                        }
                    }
                }
            });
        }
    };

    $scope.gridExpressionChooseChange = function ($event, grid, haschoose) {
        $scope.ExpressionHasChoose = haschoose;
    };

    $scope.ExpressionAdd_Click = function ($event, grid) {
        $event.preventDefault();
        $timeout(function () {
            var item = { ID: -1, Type: _WFLSetting_Function.Data._dataTableFields[0][0].Type, OperatorCode: _WFLSetting_Function.Data._dataAndOr[0].Code, TableCode: _WFLSetting_Function.Data._dataTables[0].Code, FieldCode: _WFLSetting_Function.Data._dataTableFields[0][0].Code, FieldID: _WFLSetting_Function.Data._dataTableFields[0][0].ID, OperatorValue: _WFLSetting_Function.Data._dataOperators[0].Code, CompareValue: '', IsChoose: false, IsApproved: true, IsModified: false };
            if ($scope.gridExpressionOptions.dataSource.data().length > 0)
                item.OperatorCode = _WFLSetting_Function.Data._dataAndOr[1].Code;

            $scope.gridExpressionOptions.dataSource.insert($scope.gridExpressionOptions.dataSource.data().length, item);
        }, 10);
    };

    $scope.ExpressionDel_Click = function ($event, grid) {
        $event.preventDefault();
        var items = [];
        var data = grid.dataSource.data();
        $.each(data, function (i, v) {
            if (v.IsChoose == true) {
                items.push(v);
            }
        });
        $.each(items, function () {
            grid.dataSource.remove(this);
        });
    };

    // Load danh sách status sysvar
    Common.Services.Call($http, {
        url: Common.Services.url.WFL,
        method: _WFLSetting_Function.URL.EventSysVar,
        success: function (res) {
            if (Common.HasValue(res)) {
                _WFLSetting_Function.Data._dataStatusOfOrder = res.ListStatusOfOrder;
                _WFLSetting_Function.Data._dataStatusOfPlan = res.ListStatusOfPlan;
                _WFLSetting_Function.Data._dataStatusOfDITOMaster = res.ListStatusOfDITOMaster;
                _WFLSetting_Function.Data._dataStatusOfCOTOMaster = res.ListStatusOfCOTOMaster;
                _WFLSetting_Function.Data._dataKPIReason = res.ListKPIReason;
                _WFLSetting_Function.Data._dataStatusOfAssetTimeSheet = res.ListStatusOfAssetTimeSheet;
                _WFLSetting_Function.Data._dataTypeOfAssetTimeSheet = res.ListTypeOfAssetTimeSheet;
                _WFLSetting_Function.Data._dataDITOGroupProductStatus = res.ListDITOGroupProductStatus;
                _WFLSetting_Function.Data._dataDITOGroupProductStatusPOD = res.ListDITOGroupProductStatusPOD;
                _WFLSetting_Function.Data._dataTypeOfPaymentDITOMaster = res.ListTypeOfPaymentDITOMaster;
                _WFLSetting_Function.Data._dataTroubleCostStatus = res.ListTroubleCostStatus;
                _WFLSetting_Function.Data._dataDITOLocationStatus = res.ListDITOLocationStatus;
                _WFLSetting_Function.Data._dataCOTOLocationStatus = res.ListCOTOLocationStatus;
            }
        }
    });

    // Expression
    $scope.loadDataCompareValue = function (tr, item) {
        var data = []
        if (item.FieldCode == "StatusOfOrderID" || item.FieldCode == "StatusOfPlanID" || item.FieldCode == "StatusOfDITOMasterID" || item.FieldCode == "StatusOfCOTOMasterID" || item.FieldCode == "ReasonID" || item.FieldCode == "StatusOfAssetTimeSheetID" || item.FieldCode == "TypeOfAssetTimeSheetID"
            || item.FieldCode == "DITOGroupProductStatusID" || item.FieldCode == "DITOGroupProductStatusPODID" || item.FieldCode == "TypeOfPaymentDITOMasterID"
            || item.FieldCode == "TroubleCostStatusID" || item.FieldCode == "DITOLocationStatusID" || item.FieldCode == "COTOLocationStatusID") {
            var cbo = $($(tr).find('input.clSysVar')[1]).data("kendoComboBox");
            switch (item.FieldCode) {
                case "StatusOfOrderID":
                    data = _WFLSetting_Function.Data._dataStatusOfOrder;
                    break;
                case "StatusOfPlanID":
                    data = _WFLSetting_Function.Data._dataStatusOfPlan;
                    break;
                case "StatusOfDITOMasterID":
                    data = _WFLSetting_Function.Data._dataStatusOfDITOMaster;
                    break;
                case "StatusOfCOTOMasterID":
                    data = _WFLSetting_Function.Data._dataStatusOfCOTOMaster;
                    break;
                case "ReasonID":
                    data = _WFLSetting_Function.Data._dataKPIReason;
                    break;
                case "StatusOfAssetTimeSheetID":
                    data = _WFLSetting_Function.Data._dataStatusOfAssetTimeSheet;
                    break;
                case "TypeOfAssetTimeSheetID":
                    data = _WFLSetting_Function.Data._dataTypeOfAssetTimeSheet;
                    break;
                case "DITOGroupProductStatusID":
                    data = _WFLSetting_Function.Data._dataDITOGroupProductStatus;
                    break;
                case "DITOGroupProductStatusPODID":
                    data = _WFLSetting_Function.Data._dataDITOGroupProductStatusPOD;
                    break;
                case "TypeOfPaymentDITOMasterID":
                    data = _WFLSetting_Function.Data._dataTypeOfPaymentDITOMaster;
                    break;
                case "TroubleCostStatusID":
                    data = _WFLSetting_Function.Data._dataTroubleCostStatus;
                    break;
                case "DITOLocationStatusID":
                    data = _WFLSetting_Function.Data._dataDITOLocationStatus;
                    break;
                case "COTOLocationStatusID":
                    data = _WFLSetting_Function.Data._dataCOTOLocationStatus;
                    break;
            }
        }
        return data;
    };

    $scope.getFieldValFalse = function (data, type, eplement) {
        var Item = [];
        $.each(data, function (i, v) {
            if (type == v.Type) {
                Item.push(v);
            }
        });
        try {
            $(eplement).find('input.clFieldChoose').eq(1).data('kendoComboBox').dataSource.data(Item);
        }
        catch (e) { }
        $(eplement).find('input.clFieldChoose').closest("span").show();
        $(eplement).find('input.clText').hide();
        $(eplement).find('.k-numerictextbox').hide()
        $(eplement).find('input.clBool').closest("span").hide();
        $(eplement).find('input.clDate').closest("span").hide();
    };

    $scope.setCheckValueInput = function (tr, item) {
        if (item.FieldCode == "StatusOfOrderID" || item.FieldCode == "StatusOfPlanID" || item.FieldCode == "StatusOfDITOMasterID" || item.FieldCode == "StatusOfCOTOMasterID" || item.FieldCode == "ReasonID" || item.FieldCode == "StatusOfAssetTimeSheetID" || item.FieldCode == "TypeOfAssetTimeSheetID"
            || item.FieldCode == "DITOGroupProductStatusID" || item.FieldCode == "DITOGroupProductStatusPODID" || item.FieldCode == "TypeOfPaymentDITOMasterID"
            || item.FieldCode == "TroubleCostStatusID" || item.FieldCode == "DITOLocationStatusID" || item.FieldCode == "COTOLocationStatusID") {
            var cbo = $($(tr).find('input.clSysVar')[1]).data("kendoComboBox");
            switch (item.FieldCode) {
                case "StatusOfOrderID":
                    cbo.dataSource.data(_WFLSetting_Function.Data._dataStatusOfOrder);
                    break;
                case "StatusOfPlanID":
                    cbo.dataSource.data(_WFLSetting_Function.Data._dataStatusOfPlan);
                    break;
                case "StatusOfDITOMasterID":
                    cbo.dataSource.data(_WFLSetting_Function.Data._dataStatusOfDITOMaster);
                    break;
                case "StatusOfCOTOMasterID":
                    data = _WFLSetting_Function.Data._dataStatusOfCOTOMaster;
                case "ReasonID":
                    cbo.dataSource.data(_WFLSetting_Function.Data._dataKPIReason);
                    break;
                case "StatusOfAssetTimeSheetID":
                    cbo.dataSource.data(_WFLSetting_Function.Data._dataStatusOfAssetTimeSheet);
                    break;
                case "TypeOfAssetTimeSheetID":
                    cbo.dataSource.data(_WFLSetting_Function.Data._dataTypeOfAssetTimeSheet);
                    break;
                case "DITOGroupProductStatusID":
                    cbo.dataSource.data(_WFLSetting_Function.Data._dataDITOGroupProductStatus);
                    break;
                case "DITOGroupProductStatusPODID":
                    cbo.dataSource.data(_WFLSetting_Function.Data._dataDITOGroupProductStatusPOD);
                    break;
                case "TypeOfPaymentDITOMasterID":
                    cbo.dataSource.data(_WFLSetting_Function.Data._dataTypeOfPaymentDITOMaster);
                    break;
                case "TroubleCostStatusID":
                    cbo.dataSource.data(_WFLSetting_Function.Data._dataTroubleCostStatus);
                    break;
                case "DITOLocationStatusID":
                    cbo.dataSource.data(_WFLSetting_Function.Data._dataDITOLocationStatus);
                    break;
                case "COTOLocationStatusID":
                    cbo.dataSource.data(_WFLSetting_Function.Data._dataCOTOLocationStatus);
                    break;
            }
            item.CompareValue = cbo.dataSource.data()[0].ID;
            cbo.text(cbo.dataSource.data()[0].ValueOfVar);
        } else {
            switch (item.Type) {
                case "int": $(tr).find('input.clNumber').val(0); break;
                case "bool":
                    var cbo = $($(tr).find('input.clBool')[1]).data("kendoComboBox");
                    item.CompareValue = cbo.dataSource.data()[0].Code;
                    cbo.text(cbo.dataSource.data()[0].Code);
                case "datetime": $(tr).find('input.clDate').val(""); break;
                case "string": $(tr).find('input.clText').val(""); break;
            }
        }
    };

    $scope.getCompareValue = function (tr, item) {
        $(tr).find('.clFieldChoose').hide();
        $(tr).find('.clText').hide();
        $(tr).find('.k-numerictextbox').hide();
        $(tr).find('.clBool').closest("span").hide();
        $(tr).find('.clDate').closest("span").hide();
        $(tr).find('.clSysVar').closest("span").hide();

        if (item.FieldCode == "StatusOfOrderID" || item.FieldCode == "StatusOfPlanID" || item.FieldCode == "StatusOfDITOMasterID" || item.FieldCode == "StatusOfCOTOMasterID" || item.FieldCode == "ReasonID" || item.FieldCode == "StatusOfAssetTimeSheetID" || item.FieldCode == "TypeOfAssetTimeSheetID"
            || item.FieldCode == "DITOGroupProductStatusID" || item.FieldCode == "DITOGroupProductStatusPODID" || item.FieldCode == "TypeOfPaymentDITOMasterID" || item.FieldCode == "TroubleCostStatusID" || item.FieldCode == "DITOLocationStatusID" || item.FieldCode == "COTOLocationStatusID") {
            $(tr).find('.clSysVar').closest("span").show();
        } else {
            switch (item.Type) {
                case "int":
                    $(tr).find('.k-numerictextbox').show();
                    break;
                case "bool":
                    $(tr).find('.clBool').closest("span").show();
                    break;
                case "datetime":
                    $(tr).find('.clDate').closest("span").show();
                    break;
                case "string":
                    $(tr).find('.clText').show();
                    break;
            }
        }
    };

    $scope.CheckFieldValue = function (OperatorValue) {
        var ArrayFieldValue = ["EqualField", "NotEqualField", "GreatField", "LessField", "GreaterOrEqualField", "LesserOrEqualField"];
        var check = false;
        for (var i = 0; i < ArrayFieldValue.length; i++) {
            if (ArrayFieldValue[i] == OperatorValue) {
                check = true;
                break;
            }
        }
        return check;
    };
    //#endregion
    
    //#endregion
    //#region cb
    $scope.templateMail_Options =
    {

        autoBind: true,
        valuePrimitive: true,
        ignoreCase: true,
        filter: 'contains',
        suggest: true,
        dataTextField: 'Name',
        dataValueField: 'ID',
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields: {

                    ID: { type: 'number' },
                    Name: { type: 'string' },
                }
            }
        }),
        change: function (e) {
        }
    }

    $scope.templateTMS_Options =
    {

        autoBind: true,
        valuePrimitive: true,
        ignoreCase: true,
        filter: 'contains',
        suggest: true,
        dataTextField: 'Name',
        dataValueField: 'ID',
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields: {

                    ID: { type: 'number' },
                    Name: { type: 'string' },
                }
            }
        }),
        change: function (e) {
        }
    }

    $scope.templateSMS_Options =
    {

        autoBind: true,
        valuePrimitive: true,
        ignoreCase: true,
        filter: 'contains',
        suggest: true,
        dataTextField: 'Name',
        dataValueField: 'ID',
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields: {

                    ID: { type: 'number' },
                    Name: { type: 'string' },
                }
            }
        }),
        change: function (e) {
        }
    }

    Common.Services.Call($http, {
        url: Common.Services.url.WFL,
        method: _WFLSetting_Function.URL.EventTemplateRead,

        success: function (data) {
            var item = { ID: 0, Name: '' };
            data.unshift(item);
            $scope.templateMail_Options.dataSource.data(data);
            $scope.templateTMS_Options.dataSource.data(data);
            $scope.templateSMS_Options.dataSource.data(data);
        }
    });
    //#endregion
    $scope.window_Close_Click = function ($event, win) {
        $event.preventDefault();
        win.close();
    }

    //#region action
    $scope.ShowSetting = function ($event) {
        $event.preventDefault();

        $rootScope.ShowSetting({
            ListView: views.WFLSetting,
            event: $event,
            current: $state.current
        });
    };

    $scope.HideSetting = function ($event) {
        $event.preventDefault();

        $rootScope.HideSetting();
    }
    //#endregion

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