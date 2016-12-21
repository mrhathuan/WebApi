/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />
/// <reference path="~/Scripts/map.js" />


angular.module('myapp').controller('PODInput_IndexCtrl', ['$rootScope', '$scope', '$http', '$location', '$state', '$timeout', 'openMapV2', '$compile', '$interval', function ($rootScope, $scope, $http, $location, $state, $timeout, openMapV2, $compile, $interval) {
    Common.Log('PODInput_IndexCtrl');
    $rootScope.IsLoading = false;

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
                e.item.shapeVisual.children[0].redraw({
                    stroke: {
                        width: 1,
                        color: "#31B6FC"
                    },
                });
                if (e.item.id == 1) {
                    e.item.shapeVisual.children[1].redraw({
                        source: "../Images/pod/ico_pp_down.png",
                        x: 60, y: 10, width: 25, height: 25
                    });
                }
                if (e.item.id == 2) {
                    e.item.shapeVisual.children[1].redraw({
                        source: "../Images/pod/ico_container_down.png",
                        x: 48, y: 10, width: 51, height: 24
                    });
                }
                if (e.item.id == 11 || e.item.id == 21) {
                    e.item.shapeVisual.children[1].redraw({
                        source: "../Images/pod/ico_ctu_down.png",
                        x: 60, y: 10, width: 34, height: 29
                    });
                }
                if (Common.HasValue(e.item.shapeVisual.children[2])) {
                    e.item.shapeVisual.children[2].redraw({
                        fill: "#31B6FC",
                    });
                }
            }
        },
        mouseLeave: function (e) {
            if (e.item instanceof kendo.dataviz.diagram.Shape) {
                e.item.shapeVisual.children[0].redraw({
                    stroke: {
                        width: 1,
                        color: "#808080"
                    },
                });
                if (e.item.id == 1) {
                    e.item.shapeVisual.children[1].redraw({
                        source: "../Images/pod/ico_pp_up.png",
                        x: 60, y: 10, width: 25, height: 25
                    });
                }
                if (e.item.id == 2) {
                    e.item.shapeVisual.children[1].redraw({
                        source: "../Images/pod/ico_container_up.png",
                        x: 48, y: 10, width: 51, height: 24
                    });
                }
                if (e.item.id == 11 || e.item.id == 21) {
                    e.item.shapeVisual.children[1].redraw({
                        source: "../Images/pod/ico_ctu_up.png",
                        x: 60, y: 10, width: 34, height: 29
                    });
                }
                if (Common.HasValue(e.item.shapeVisual.children[2])) {
                    e.item.shapeVisual.children[2].redraw({
                        fill: "#0000",
                    });
                }
            }
        },
    };

    $timeout(function () {
        var dataShapes = [], dataConnections = [];
        var pLeft = 0;

        if ($scope.Auth.ActTruck) {
            dataShapes.push({ ID: 1, Title: "VT phân phối", Code: '#', X: 200, Y: 30, W: 80, Image: "ico_di.png" });
            dataShapes.push({ ID: 11, Title: "Nhận chứng từ", Code: 'main.PODInput.Check', X: 100, Y: 160, W: 70, Image: "ico_auto.png" });
            dataShapes.push({ ID: 12, Title: "Khóa chứng từ", Code: 'main.PODInput.DICloseByDate', X: 300, Y: 160, W: 70, Image: "ico_auto.png" });
            dataShapes.push({ ID: 1111, Title: "Excel hóa đơn", Code: 'main.PODInput.Map', X: 100, Y: 420, W: 70, Image: "ico_auto.png" });

            dataConnections.push({ ID: 1, FShape: 1, TShape: 11 })
            dataConnections.push({ ID: 1, FShape: 1, TShape: 12 })
            
            if ($scope.Auth.ActOPS) {
                dataShapes.push({ ID: 111, Title: "Upload Đơn hàng", Code: 'main.PODInput.UploadOrder', X: 100, Y: 290, W: 80, Image: "ico_di.png" });
                dataConnections.push({ ID: 3, FShape: 11, TShape: 111 })
                dataConnections.push({ ID: 3, FShape: 111, TShape: 1111 })
            }
            else {
                dataConnections.push({ ID: 3, FShape: 11, TShape: 1111 })
            }

        }

        if ($scope.Auth.ActContainer) {
            dataShapes.push({ ID: 2, Title: "VT container", Code: '#', X: 750, Y: 30, W: 70, Image: "ico_no-tender.png" });
            dataShapes.push({ ID: 21, Title: "Nhận chứng từ", Code: 'main.PODInput.COCheck', X: 650, Y: 160, W: 70, Image: "ico_tender.png" });
            dataShapes.push({ ID: 22, Title: "Khóa chứng từ", Code: 'main.PODInput.COCloseByDate', X: 850, Y: 160, W: 70, Image: "ico_tender.png" });

            dataConnections.push({ ID: 2, FShape: 2, TShape: 21 });
            dataConnections.push({ ID: 2, FShape: 2, TShape: 22 });
        }

        angular.forEach(dataShapes, function (value, key) {
            var dataviz = kendo.dataviz;
            var shape = new dataviz.diagram.Shape({
                type: 'circle', id: value.ID, stroke: { width: 1, color: value.Code == "#" ? "#acb8c4" : "#31B6FC" }, editable: false,
                x: value.X, y: value.Y, fill: "transparent", width: value.W, height: value.W, dataItem: value,
                visual: function (e) {
                    var g = new dataviz.diagram.Group();
                    var dataItem = e.dataItem;

                    g.append(new dataviz.diagram.Rectangle({
                        width: 150,
                        height: 70,
                        stroke: {
                            width: 1
                        },
                    }));

                    if (dataItem.ID == 1) {
                        g.append(new dataviz.diagram.Image({
                            source: "../Images/pod/ico_pp_up.png",
                            x: 60, y: 10, width: 25, height: 25
                        }));
                    }
                    if (dataItem.ID == 2) {
                        g.append(new dataviz.diagram.Image({
                            source: "../Images/pod/ico_container_up.png",
                            x: 48, y: 10, width: 51, height: 24
                        }));
                    }
                    if (dataItem.ID == 11 || dataItem.ID == 21) {
                        g.append(new dataviz.diagram.Image({
                            source: "../Images/pod/ico_ctu_up.png",
                            x: 60, y: 10, width: 34, height: 29
                        }));
                    }



                    g.append(new dataviz.diagram.TextBlock({
                        text: dataItem.Title,
                        x: 27,
                        y: 45,
                        fontSize: 15,
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
            var connection = new kendo.dataviz.diagram.Connection(fShape, tShape, {
                type: "cascading", stroke: { color: "#c0c0c0" }
            });
            $scope.opsDiagram.addConnection(connection);
        });
    }, 1000)
}])