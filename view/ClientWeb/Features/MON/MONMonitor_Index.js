/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />
/// <reference path="~/Scripts/map.js" />


angular.module('myapp').controller('MONMonitor_IndexCtrl', ['$rootScope', '$scope', '$http', '$location', '$state', '$timeout', 'openMapV2', '$compile', '$interval', function ($rootScope, $scope, $http, $location, $state, $timeout, openMapV2, $compile, $interval) {
    Common.Log('MONMonitor_IndexCtrl');
    $rootScope.IsLoading = false;
    $scope.dataShapes = [];
    //$state.go('main.MONMonitor.ControlTowerCOTimeline');
    if ($rootScope.IsPageComplete != true) return;
    $scope.Auth = $rootScope.GetAuth();

    $scope.opsDiagram_Options = {
        layout: { type: "tree", subtype: "down", horizontalSeparation: 30, verticalSeparation: 20 },
        editable: false, pannable: false, zoomMax: 1, zoomMin: 1, selectable: false,
        dataBound: function () { this.resize(); },
        click: function (e) {
            if (e.item instanceof kendo.dataviz.diagram.Shape) {
                if (e.item.dataItem.Code != "#") {
                    $state.go(e.item.dataItem.Code);
                }
            }
        },
        mouseEnter: function (e) {
            if (e.item instanceof kendo.dataviz.diagram.Shape) {
                var dataItem = [];
                $.each($scope.dataShapes, function (i, v) {
                    if (e.item.id == v.ID) {
                        dataItem = v;
                    }
                });
                e.item.shapeVisual.children[0].redraw({
                    stroke: {
                        width: 0.5,
                        color: "#31B6FC"
                    },
                });
                e.item.shapeVisual.children[1].redraw({
                    source: "../Images/mon/" + dataItem.ImageHover,
                    x: dataItem.imgx, y: dataItem.imgy, width: dataItem.imgw, height: dataItem.imgh
                });
                if (Common.HasValue(e.item.shapeVisual.children[2])) {
                    e.item.shapeVisual.children[2].redraw({
                        fill: "#31B6FC",
                    });
                }
            }
        },
        mouseLeave: function (e) {
            if (e.item instanceof kendo.dataviz.diagram.Shape) {
                var dataItem = [];
                $.each($scope.dataShapes, function (i, v) {
                    if (e.item.id == v.ID) {
                        dataItem = v;
                    }
                });
                e.item.shapeVisual.children[0].redraw({
                    stroke: {
                        width: 0.5,
                        color: "#afafaf",
                        radius: 5,
                    },
                });
                e.item.shapeVisual.children[1].redraw({
                    source: "../Images/mon/" + dataItem.Image,
                    x: dataItem.imgx, y: dataItem.imgy, width: dataItem.imgw, height: dataItem.imgh
                });
                if (Common.HasValue(e.item.shapeVisual.children[2])) {
                    e.item.shapeVisual.children[2].redraw({
                        fill: "#0000",
                    });
                }
            }
        }
    };

    $timeout(function () {
        var dataConnections = [];
        var pLeft = 100;
        var cleft = 400;
        if ($scope.Auth.ActTruck) {
            cleft = 820;
            $scope.dataShapes = $scope.dataShapes.concat([
                { X: 230 + pLeft, ID: 1, Title: "G.s phân phối", Code: 'main.MONMonitor.ControlTowerDI', Y: 30, W: 80, Image: "icon_dp_01.png", ImageHover: "icon_dp_01_acti.png", imgx: 40, imgy: 10, imgw: 30, imgh: 20 },
                { X: 70 + pLeft, ID: 11, Title: "Nhập sản lượng", Code: '#', Y: 160, W: 70, Image: "ico_04.png", ImageHover: "ico_04_acti.png", imgx: 42, imgy: 10, imgw: 23, imgh: 23 },
                { X: 90, ID: 111, Title: "Manual", Code: 'main.MONMonitor.Input_InputProduction', Y: 300, W: 70, Image: "icon_dp_04.png", ImageHover: "icon_dp_04_acti.png", imgx: 43, imgy: 10, imgw: 22, imgh: 22 },
                { X: 160 + pLeft, ID: 112, Title: "Excel", Code: 'main.MONMonitor.Input_ImportExt', Y: 300, W: 70, Image: "ico_05.png", ImageHover: "ico_05_acti.png", imgx: 45, imgy: 10, imgw: 20, imgh: 22 },
                { X: 10, ID: 1111, Title: "Công nợ", Code: 'main.MONMonitor.Input_ExtReturn', Y: 400, W: 70, Image: "ico_06.png", ImageHover: "ico_06_acti.png", imgx: 40, imgy: 10, imgw: 25, imgh: 22 },
                { X: 170, ID: 1112, Title: "Thời gian", Code: '#', Y: 400, W: 70, Image: "ico_07.png", ImageHover: "ico_07_acti.png", imgx: 43, imgy: 10, imgw: 22, imgh: 22 },
                { X: 400 + pLeft, ID: 12, Title: "Khác", Code: '#', Y: 160, W: 70, Image: "ico_01.png", ImageHover: "ico_01_acti.png", imgx: 42, imgy: 10, imgw: 23, imgh: 23 },
                { X: 320 + pLeft, ID: 1211, Title: "Excel", Code: 'main.MONMonitor.Addition', Y: 400, W: 70, Image: "ico_05.png", ImageHover: "ico_05_acti.png", imgx: 45, imgy: 10, imgw: 20, imgh: 22 },
                { X: 320 + pLeft, ID: 121, Title: "Hàng b.sung", Code: '#', Y: 300, W: 70, Image: "ico_01.png", ImageHover: "ico_01_acti.png", imgx: 42, imgy: 10, imgw: 23, imgh: 23 },
                { X: 480 + pLeft, ID: 122, Title: "Nhập chi phí", Code: 'main.MONMonitor.Input_DIFLMFee', Y: 300, W: 70, Image: "ico_02.png", ImageHover: "ico_02_acti.png", imgx: 43, imgy: 10, imgw: 22, imgh: 22 },
                { X: 480 + pLeft, ID: 1221, Title: "Duyệt chi phí", Code: 'main.MONMonitor.Approve_DICost', Y: 400, W: 70, Image: "ico_03.png", ImageHover: "ico_03_acti.png", imgx: 44, imgy: 10, imgw: 23, imgh: 20 },
            ]);

            dataConnections = dataConnections.concat([
                { ID: 1, FShape: 1, TShape: 11 },
                { ID: 2, FShape: 1, TShape: 12 },
                { ID: 3, FShape: 11, TShape: 111 },
                { ID: 4, FShape: 11, TShape: 112 },
                { ID: 4, FShape: 111, TShape: 1111 },
                { ID: 5, FShape: 111, TShape: 1112 },
                { ID: 6, FShape: 12, TShape: 121 },
                { ID: 6, FShape: 12, TShape: 122 },
                { ID: 8, FShape: 121, TShape: 1211 },
                { ID: 6, FShape: 122, TShape: 1221 },
            ])
        }

        if ($scope.Auth.ActContainer) {
            if ($scope.Auth.ViewAdmin) {
                $scope.dataShapes = $scope.dataShapes.concat([
                    { ID: 2, Title: "VT container", Code: 'main.MONMonitor.ControlTowerCOTimeline', X: cleft, Y: 30, W: 70, Image: "icon_dp_02.png", ImageHover: "icon_dp_02_active.png", imgx: 45, imgy: 10, imgw: 40, imgh: 20 },
                    { ID: 21, Title: "Nhập chi phí", Code: 'main.MONMonitor.Input_COFLMFee', X: cleft - 70, Y: 160, W: 70, Image: "ico_02.png", ImageHover: "ico_02.png", imgx: 43, imgy: 10, imgw: 22, imgh: 22 },
                    { ID: 211, Title: "Duyệt chi phí", Code: 'main.MONMonitor.Approve_COCost', X: cleft - 70, Y: 300, W: 70, Image: "ico_03.png", ImageHover: "ico_03_acti.png", imgx: 44, imgy: 10, imgw: 23, imgh: 20 },
                    { ID: 22, Title: "Manual", Code: 'main.MONMonitor.Input_Container', X: cleft + 80, Y: 160, W: 70, Image: "icon_dp_04.png", ImageHover: "icon_dp_04.png", imgx: 44, imgy: 10, imgw: 23, imgh: 20 },
                    { ID: 2222, Title: "Vận hành", Code: 'main.MONMonitor.Container_Operation', X: cleft + 80, Y: 400, W: 70, Image: "icon_dp_04.png", ImageHover: "icon_dp_04.png", imgx: 44, imgy: 10, imgw: 23, imgh: 20 },

                ]);
                dataConnections = dataConnections.concat([
                    { ID: 9, FShape: 2, TShape: 21 },
                    { ID: 10, FShape: 21, TShape: 211 },
                    { ID: 11, FShape: 2, TShape: 22 },

                ]);
                if ($scope.Auth.ViewVendor) {
                    $scope.dataShapes.push({ ID: 221, Title: "Tender", Code: 'main.MONMonitor.Tender_Container', X: cleft + 80, Y: 300, W: 70, Image: "icon_dp_04.png", ImageHover: "icon_dp_04.png", imgx: 44, imgy: 10, imgw: 23, imgh: 20 });
                    dataConnections.push({ ID: 12, FShape: 22, TShape: 221 });
                    dataConnections.push({ ID: 13, FShape: 221, TShape: 2222 });
                }
                else {
                    dataConnections.push({ ID: 13, FShape: 22, TShape: 2222 });
                }
            }
            else if ($scope.Auth.ViewVendor) {
                $scope.dataShapes = $scope.dataShapes.concat([
                    { ID: 221, Title: "Tender", Code: 'main.MONMonitor.Tender_Container', X: cleft, Y: 160, W: 70, Image: "icon_dp_04.png", ImageHover: "icon_dp_04.png", imgx: 44, imgy: 10, imgw: 23, imgh: 20 },
                ]);
                dataConnections = dataConnections.concat([]);
            }
        }

        angular.forEach($scope.dataShapes, function (value, key) {
            var dataviz = kendo.dataviz;
            var shape = new dataviz.diagram.Shape({
                type: 'circle', id: value.ID, stroke: { width: 1, color: value.Code == "#" ? "#c3c3c3" : "#c3c3c3" }, editable: false,
                x: value.X, y: value.Y, fill: "transparent", width: value.W, height: value.W, dataItem: value,
                visual: function (e) {
                    var g = new dataviz.diagram.Group();
                    var dataItem = e.dataItem;
                    g.append(new dataviz.diagram.Rectangle({
                        width: 110,
                        height: 60,
                        stroke: {
                            width: 0.5,
                            radius: 5,
                            color: "#afafaf",
                        },
                    }));
                    var tx = 20;
                    var ty = 35;
                    switch (dataItem.ID) {
                        case 1111:
                        case 1112:
                        case 111:
                        case 221:
                            tx = 35;
                            break;
                        case 112:
                        case 1211:
                            tx = 40;
                            break;
                        case 12:
                            tx = 45;
                            break;
                        case 121:
                            tx = 17;
                            break;
                        case 11:
                            tx = 10;
                            break;
                        case 1:
                            tx = 18;
                            break;
                        case 22:
                            tx = 38;
                            break;
                    }

                    g.append(new dataviz.diagram.Image({
                        source: "../Images/mon/" + dataItem.Image,
                        x: dataItem.imgx, y: dataItem.imgy, width: dataItem.imgw, height: dataItem.imgh
                    }));

                    g.append(new dataviz.diagram.TextBlock({
                        text: dataItem.Title,
                        x: tx,
                        y: ty,
                        fontSize: 13,
                        fill: "#0000"
                    }));
                    return g;
                }

            })
            $scope.opsDiagram.addShape(shape);
        });
        angular.forEach(dataConnections, function (value, key) {
            var fShape = $scope.opsDiagram.getShapeById(value.FShape);
            var tShape = $scope.opsDiagram.getShapeById(value.TShape);
            //var connection = new kendo.dataviz.diagram.Connection(fShape, tShape, {
            //    type: "cascading", stroke: { color: "#c3c3c3" }
            //});
            //$scope.opsDiagram.addConnection(connection);
            var posShape1 = fShape.getPosition("bottom");
            var posShape2 = tShape.getPosition("top");

            var point1 = {
                x: posShape1.x,
                y: posShape1.y + ((posShape2.y - posShape1.y) / 2)
            };

            var point2 = {
                x: posShape2.x,
                y: point1.y
            };

            $scope.opsDiagram.connect(fShape.connectors[1], tShape.connectors[0], {
                stroke: {
                    width: 0.5,
                    color: "#8a8a8a"
                },
                points: [point1, point2]
            });
        });
    }, 1000)

    $scope.ShowSetting = function ($event, grid) {
        $event.preventDefault();

        $rootScope.ShowSetting({
            ListView: views.MONMonitor,
            event: $event,
            grid: grid,
            current: $state.current
        });
    };

    $scope.HideSetting = function ($event) {
        $event.preventDefault();

        $rootScope.HideSetting();
    }
}])