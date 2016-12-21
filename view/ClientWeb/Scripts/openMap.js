/// <reference path="~/Scripts/jquery-1.9.1.intellisense.js" />
/// <reference path="~/Scripts/common.js" />
/// <reference path="~/Scripts/ol3/ol.js" />
angular.module('myapp').factory('openMap', function () {
    var apiUrl = {
        viaroute: 'http://router.project-osrm.org/viaroute?loc=',
        geojson: 'http://global.mapit.mysociety.org/area/',
        suffix: '.geojson?simplify_tolerance=0.0001',
        instructions: '&instructions=true'
    }
    var mapEPSG = { E4326: 'EPSG:4326', E3857: 'EPSG:3857' }       
    var mapFeature = { marker: 1, route: 2, polyline: 3, polygon: 4 }
    
    var openMap = function () {
        this.map = null;
        this.hasMap = true;
        this.windowInfo = null;
        this.currentFeature = null;
        this.maxZoom = 20;

        this.mapRoute = [];
        this.mapMarker = [];
        this.mapPolygon = [];
        this.mapPolyline = [];
        this.provinceCode = {
            '01': 793923, '02': 794014, '03': 793918, '04': 793912, '05': 687794, '06': 508457, '07': 687792, '08': 793922, '09': 15143, '10': 795293,
            '11': 15144, '12': 508527, '13': 15138, '14': 15139, '15': 15132, '16': 508524, '17': 793921, '18': 793920, '19': 793919, '20': 0,
            '21': 508523, '22': 508521, '23': 687791, '24': 793917, '25': 793916, '26': 687787, '27': 687785, '28': 687780, '29': 687779, '30': 687778,
            '31': 687776, '32': 687775, '33': 687774, '34': 793911, '35': 793909, '36': 793904, '37': 793908, '38': 793905, '39': 687768, '40': 508498,
            '41': 793907, '42': 508500, '43': 793913, '44': 793925, '45': 793906, '46': 793914, '47': 687796, '48': 687798, '49': 793902, '50': 793899,
            '51': 793898, '52': 793924, '53': 793901, '54': 793897, '55': 793895, '56': 687762, '57': 687761, '58': 687758, '59': 687757, '60': 687756,
            '61': 793894, '62': 364265, '63': 508499, '64': 508488
        }
        
        this.mapTile = {
            osm: new ol.layer.Tile({
                source: new ol.source.OSM(), visible: true
            }),
            mapQuest: new ol.layer.Tile({
                source: new ol.source.MapQuest({ layer: 'osm' })
            }),
            google: new olgm.layer.Google(), //Google maps token required
            mapBox: new ol.layer.Tile({
                source: new ol.source.XYZ({
                    url: 'https://api.tiles.mapbox.com/v4/mapbox.streets/{z}/{x}/{y}.png?access_token=pk.eyJ1IjoiaHVuZ2hvYW5nc21sIiwiYSI6ImNpcWx5OWhoMTAwYXZmcG5oOWZvcHI3eTYifQ.hKLiBbe5enCLWitk9OsKtQ'
                }),
                visible: true
            }),
            mapBoxLight: new ol.layer.Tile({
                source: new ol.source.XYZ({
                    url: 'https://api.tiles.mapbox.com/v4/mapbox.light/{z}/{x}/{y}.png?access_token=pk.eyJ1IjoiaHVuZ2hvYW5nc21sIiwiYSI6ImNpcWx5OWhoMTAwYXZmcG5oOWZvcHI3eTYifQ.hKLiBbe5enCLWitk9OsKtQ'
                }),
                visible: false
            })
        }
        this.mapColor = {
            aqua: '#00FFFF', black: '#000000', blue: '#0000FF', brown: '#A52A2A', coral: '#FF7F50',
            green: '#4CC417', olive: '#808000', orangeRed: '#FF4500', purple: '#800080', red: '#F70D1A',
            seaGreen: '#2E8B57', tan: '#D2B48C', teal: '#008080', yellow: '#FFFF00', yellowGreen: '#9ACD32',
            random: function () {
                var me = this;

                var count = 0;
                var color = me.aqua;
                try {
                    for (var prop in me) {
                        if (Math.random() < 1 / ++count) {
                            color = me[prop];
                        }
                    }
                }
                catch (e) { }
                return color;
            }
        }
        this.mapStyle = {
            _Default: new ol.style.Style({
                fill: new ol.style.Fill({
                    color: 'rgba(0, 0, 255, 0.2)'
                }),
                stroke: new ol.style.Stroke({
                    color: 'rgba(0, 0, 255, 0.8)', width: 4
                }),
                image: new ol.style.Circle({
                    radius: 10,
                    fill: new ol.style.Fill({
                        color: 'rgba(0, 0, 255, 0.8)'
                    }),
                    stroke: new ol.style.Stroke({
                        color: 'white', width: 2
                    })
                }),
                zIndex: 4
            }),
            Icon: function (src, opacity, scale, text, font, textcolor, offsetY, textBaseLine, zindex) {
                return new ol.style.Style({
                    image: new ol.style.Icon({
                        scale: scale || 1,
                        anchor: [0.5, 1], opacity: opacity, src: src
                    }),
                    text: new ol.style.Text({
                        text: text || "",
                        font: font || 'bold 13px Arial',
                        fill: new ol.style.Fill({ color: textcolor || '#31B6FC' }),
                        offsetY: offsetY || -30, //5: Bottom. -30: Center.
                        textBaseline: textBaseLine || 'top'
                    }),
                    zIndex: zindex || 4
                });
            },
            IconCircle: function (radius, fillcolor, strokecolor, strokewidth, text, textcolor, zindex) {
                return new ol.style.Style({
                    image: new ol.style.Circle({
                        radius: radius || 10,
                        stroke: new ol.style.Stroke({
                            color: strokecolor || 'white',
                            width: strokewidth || 2,
                        }),
                        fill: new ol.style.Fill({
                            color: fillcolor || 'rgba(0, 0, 255, 0.8)'
                        })
                    }),
                    text: new ol.style.Text({
                        text: text,
                        font: 'bold 13px Arial',
                        fill: new ol.style.Fill({ color: textcolor || 'white' })
                    }),
                    zIndex: zindex || Infinity
                });
            },
            Route: function (width, color, dash, zindex) {
                return new ol.style.Style({
                    stroke: new ol.style.Stroke({
                    width: width, color: color, lineDash: dash
                    }),
                    zIndex: zindex || Infinity
                });
            },
            Line: function (width, color, opacity, text, textcolor, rotation, dash, zindex) {
                return new ol.style.Style({
                    stroke: new ol.style.Stroke({
                        color: color, width: width, lineDash: dash
                    }),
                    text: new ol.style.Text({
                        text: text,
                        font: 'bold 13px Arial',
                        fill: new ol.style.Fill({ color: textcolor || 'white' }),
                        offsetY: 5,
                        rotation: rotation || 0,
                        textBaseline: 'top'
                    }),
                    zIndex: zindex || Infinity
                });
            },            
            Polygon: function (width, color, opacity, text, textcolor, zindex) {
                var fill;
                if (opacity != null && opacity != undefined)
                    fill = new ol.style.Fill({
                        color: 'rgba(0, 0, 255, ' + opacity + ')'
                    });

                return new ol.style.Style({
                    fill: fill,
                    stroke: new ol.style.Stroke({
                        color: color, width: width
                    }),
                    text: new ol.style.Text({
                        text: text,
                        font: 'bold 13px Arial',
                        fill: new ol.style.Fill({ color: textcolor || 'white' })
                    }),
                    zIndex: zindex || Infinity
                });
            }
        }
        this.mapImage = {
            Stock_Green: '/Images/map/stock_green.png',
            Stock_Red: '/Images/map/stock_red.png',
            Stock_Yellow: '/Images/map/stock_yellow.png',
            Depot_Green: '/Images/map/depot_green.png',
            Depot_Red: '/Images/map/depot_red.png',
            Depot_Yellow: '/Images/map/depot_yellow.png',
            Dock_Green: '/Images/map/dock_green.png',
            Dock_Red: '/Images/map/dock_red.png',
            Dock_Yellow: '/Images/map/dock_yellow.png',
            Pin_Green: '/Images/map/pin_green.png',
            Pin_Red: '/Images/map/pin_red.png',
            Pin_Yellow: '/Images/map/pin_yellow.png',
            Marker_Green: '/Images/map/marker_green.png',
            Marker_Red: '/Images/map/marker_red.png',
            Marker_Yellow: '/Images/map/marker_yellow.png',
            NumericPoint_Green: '/Images/map/numeric_green/point/{0}.png',
            NumericPoint_Red: '/Images/map/numeric_red/point/{0}.png',
            NumericPoint_Yellow: '/Images/map/numeric_yellow/point/{0}.png',
            NumericPoint_Platinum: '/Images/map/numeric_platinum/point/{0}.png',
            Truck_Orange: '/Images/map/truck_orange.png',
            Truck_Purple: '/Images/map/truck_purple.png',

            //Location
            Empty: '/Images/map/icon/green/ico_empty.png',
            Stock: '/Images/map/icon/blue/ico_stock.png',
            Depot: '/Images/map/icon/blue/ico_depot.png',
            Seaport: '/Images/map/icon/blue/ico_seaport.png',
            Location: '/Images/map/icon/blue/ico_location.png',
            Distributor: '/Images/map/icon/blue/ico_distributor.png',

            //Vehicle
            Truck: '/Images/map/icon/orange/ico_truck.png',
            Tractor: '/Images/map/icon/blue/ico_tractor.png',
            Romooc_20: '/Images/map/icon/blue/ico_romooc_20.png',
            Romooc_40: '/Images/map/icon/blue/ico_romooc_40.png',
            Con_20E: '/Images/map/icon/blue/ico_con_20.png',
            Con_40E: '/Images/map/icon/blue/ico_con_40.png',
            Con_20F: '/Images/map/icon/blue/ico_con_20f.png',
            Con_40F: '/Images/map/icon/blue/ico_con_40f.png',
            Romooc_Con_20E: '/Images/map/icon/blue/ico_ro_co_20e.png',
            Romooc_Con_20F: '/Images/map/icon/blue/ico_ro_co_20f.png',
            Romooc_Con_20E: '/Images/map/icon/blue/ico_ro_co_40e.png',
            Romooc_Con_20F: '/Images/map/icon/blue/ico_ro_co_40f.png',
            Tractor_Romooc_20: '/Images/map/icon/blue/ico_tr_ro_20.png',
            Tractor_Romooc_40: '/Images/map/icon/blue/ico_tr_ro_40.png',
            Tractor_Romooc_Con_20E: '/Images/map/icon/blue/ico_tr_ro_co_20e.png',
            Tractor_Romooc_Con_20F: '/Images/map/icon/blue/ico_tr_ro_co_20f.png',
            Tractor_Romooc_Con_40E: '/Images/map/icon/blue/ico_tr_ro_co_40e.png',
            Tractor_Romooc_Con_40F: '/Images/map/icon/blue/ico_tr_ro_co_40f.png',
            Tractor_Romooc_Con_40_20x2E: '/Images/map/icon/blue/ico_tr_ro_co_2x20e.png',
            Tractor_Romooc_Con_40_20x2F: '/Images/map/icon/blue/ico_tr_ro_co_2x20f.png',
            Tractor_Romooc_Con_40_20x2EF: '/Images/map/icon/blue/ico_tr_ro_co_2x20ef.png'
        }
        this.mapFeature = { marker: 1, route: 2, polyline: 3, polygon: 4 }

        this._to3875 = function (val) {
            return ol.proj.transform(val, mapEPSG.E4326, mapEPSG.E3857)
        }
        this._to4326 = function (val) {
            return ol.proj.transform(val, mapEPSG.E3857, mapEPSG.E4326)
        }
        this._toJson = function (url, pData) {
            var xhr = new XMLHttpRequest(),
                when = {},
                onload = function () {
                    if (xhr.status === 200) {
                        when.ready.call(undefined, JSON.parse(xhr.response));
                    } else {
                        console.error("Status: " + xhr.status);
                    }
                },
                onerror = function () {
                    console.error("Can't XHR " + JSON.stringify(url));
                },
                onprogress = function () { }
            ;

            xhr.open("GET", url, true);
            xhr.setRequestHeader("Accept", "application/json");
            xhr.onload = onload;
            xhr.onerror = onerror;
            xhr.onprogress = onprogress;
            xhr.send(null);
            return {
                when: function (obj) {
                    when.ready = obj.ready;
                }
            };
        }

        this.Point = function (pLat, pLng) {
            return this._to3875([pLng, pLat]);
        }
        this.Create = function (pOptions) {
            var me = this;
            
            var options = $.extend({
                Element: '',
                Tooltip_Show: false,
                Tooltip_Element: "",
                InfoWin_Show: false,
                InfoWin_ShowWhenClick: false,
                InfoWin_Element: "",
                InfoWin_Width: null,
                InfoWin_Height: null,
                Zoom: 11, //DefaultZoom
                Lat: 10.8018735, //Center
                Lng: 106.7200799, //Center
                Tile: me.mapTile.osm,
                ClickMarker: null, 
                ClickMap: null,
                HoverLayer: null
            }, true, pOptions);

            if (me.hasMap) {
                //Clear viewport if exists.
                try {
                    var viewPort = document.getElementById(options.Element);
                    viewPort.innerHTML = "";
                }
                catch (e) { }
                var mapView = new ol.View({
                    center: this.Point(options.Lat, options.Lng), zoom: options.Zoom, maxZoom: me.maxZoom
                });

                var vPoint = new ol.layer.Vector({
                    source: new ol.source.Vector({
                        features: []
                    })
                });
                vPoint.setZIndex(99);
                vPoint.set('Name', 'MapMarker');

                var vRoute = new ol.layer.Vector({
                    source: new ol.source.Vector({
                        features: []
                    })
                });
                vRoute.setZIndex(89);
                vRoute.set('Name', 'MapRoute');
                
                var mLayer = [me.mapTile.mapBox, me.mapTile.mapBoxLight, vPoint, vRoute];
                me.map = new ol.Map({
                    view: mapView,
                    layers: mLayer,
                    target: options.Element,
                    //interactions: olgm.interaction.defaults()//Recommended
                });

                //Active Google maps
                //var olGM = new olgm.OLGoogleMaps({ map: me.map });
                //olGM.activate();
                
                var button = document.createElement('button');
                button.innerHTML = 'L';
                button.title = 'Toggle map';
                var fn = function (e) {                    
                    me.mapTile.mapBox.setVisible(!me.mapTile.mapBox.getVisible());
                    me.mapTile.mapBoxLight.setVisible(!me.mapTile.mapBoxLight.getVisible());
                };
                button.addEventListener('click', fn, false);
                var element = document.createElement('div');
                element.className = 'map-view-button ol-unselectable ol-control';
                element.appendChild(button);
                me.map.addControl(new ol.control.Control({
                    element: element
                }));
                
                me.map.on('click', function (e) {
                    var flag = false;
                    var orEv = e.coordinate;
                    var pixel = e.pixel;

                    me.map.forEachFeatureAtPixel(e.pixel, function (feature) {
                        if (flag == false) {
                            flag = true;
                            var type = feature.get('type');
                            if (type == mapFeature.marker) {
                                if (Common.HasValue(options.ClickMarker)) {
                                    if (Common.HasValue(me.windowInfo)) {
                                        me.Close();
                                        me.windowInfo.setPositioning('top-left');
                                        me.windowInfo.getElement().style.display = "";
                                        me.windowInfo.getElement().parentElement.style.display = "";
                                        options.ClickMarker(feature.get('data'), feature);
                                        setTimeout(function () {
                                            var winelement = $(me.windowInfo.getElement());
                                            var w = $(winelement).width();
                                            var h = $(winelement).height();
                                            var wm = $(me.map.getViewport()).width();
                                            var hm = $(me.map.getViewport()).height();
                                            if (hm - pixel[1] < h) {
                                                if (wm - pixel[0] < w) {
                                                    me.windowInfo.setPositioning('bottom-right');
                                                } else {
                                                    me.windowInfo.setPositioning('bottom-left');
                                                }
                                            }
                                            else if (wm - pixel[0] < w) {
                                                me.windowInfo.setPositioning('top-right');
                                            }
                                            me.windowInfo.setPosition(feature.getGeometry().getCoordinates());
                                        }, 1)                                        
                                    } else {
                                        options.ClickMarker(feature.get('data'), feature);
                                    }
                                }
                            } else if (type == mapFeature.polyline) {

                            }
                        }
                    })
                    if (!flag && Common.HasValue(options.ClickMap)) {
                        if (Common.HasValue(me.windowInfo)) {
                            me.Close();
                            if (options.InfoWin_ShowWhenClick) {
                                me.windowInfo.setPositioning('top-left');
                                me.windowInfo.getElement().style.display = "";
                                me.windowInfo.getElement().parentElement.style.display = "";
                                options.ClickMap(orEv);
                                setTimeout(function () {
                                    var winelement = $(me.windowInfo.getElement());
                                    var w = $(winelement).width();
                                    var h = $(winelement).height();
                                    var wm = $(me.map.getViewport()).width();
                                    var hm = $(me.map.getViewport()).height();
                                    if (hm - pixel[1] < h) {
                                        if (wm - pixel[0] < w) {
                                            me.windowInfo.setPositioning('bottom-right');
                                        } else {
                                            me.windowInfo.setPositioning('bottom-left');
                                        }
                                    }
                                    else if (wm - pixel[0] < w) {
                                        me.windowInfo.setPositioning('top-right');
                                    }
                                    me.windowInfo.setPosition(orEv);
                                }, 1)
                            }                            
                        }
                        options.ClickMap(orEv);
                    }
                })

                //Tooltip
                if (options.Tooltip_Show && Common.HasValue(options.Tooltip_Element)) {
                    var tooltip_element = document.getElementById(options.Tooltip_Element);
                    var tooltip = new ol.Overlay({
                        element: tooltip_element, positioning: 'bottom-left', stopEvent: false
                    });
                    me.map.addOverlay(tooltip);
                    try {
                        angular.element('#' + options.Tooltip_Element).data("mapTooltip", tooltip);
                    } catch (e) { }

                    $(me.map.getViewport()).on('mousemove', function (e) {
                        var feature = me.map.forEachFeatureAtPixel(me.map.getEventPixel(e.originalEvent), function (feature) {
                            return feature;
                        });
                        if (Common.HasValue(feature)) {
                            var geometry = feature.getGeometry();
                            var coord = geometry.getCoordinates();
                            if (feature.get('type') == 2 || feature.get('type') == 3) {
                                tooltip.setPosition(me.map.getCoordinateFromPixel(me.map.getEventPixel(e.originalEvent)));
                            } else {
                                tooltip.setPosition(coord);
                            }
                            var text = feature.get('name');
                            if (text != null && text != '') {
                                $(tooltip_element).show();
                                $(tooltip_element).text(text);
                            }
                            if (Common.HasValue(options.HoverLayer)) {
                                try {
                                    if (Common.HasValue(me.currentFeature))
                                        options.HoverLayer(false, me.currentFeature.get('type'), me.currentFeature);
                                    me.currentFeature = feature;
                                    options.HoverLayer(true, feature.get('type'), feature);
                                }
                                catch (e) {
                                    Common.Log(e.message);
                                }
                            }
                        }
                        else {
                            $(tooltip_element).hide();
                            if (Common.HasValue(options.HoverLayer) && Common.HasValue(me.currentFeature)) {
                                try {
                                    options.HoverLayer(false, me.currentFeature.get('type'), me.currentFeature);
                                    me.currentFeature = null;
                                }
                                catch (e) {
                                    Common.Log(e.message);
                                }
                            }
                        }
                    });
                }                

                if (options.InfoWin_Show) {
                    var infoWin = new ol.Overlay({
                        element: document.getElementById(options.InfoWin_Element)
                    });
                    me.map.addOverlay(infoWin);
                    try {
                        angular.element('#' + options.InfoWin_Element).data("mapInfo", infoWin);
                    } catch (e) { }

                    setTimeout(function () {
                        var info_element = infoWin.getElement().parentElement;
                        if (options.InfoWin_Width > 0) {
                            info_element.style.width = options.InfoWin_Width + "px";
                        }
                        if (options.InfoWin_Height > 0) {
                            info_element.style.height = options.InfoWin_Height + "px";
                        }
                    }, 1000)
                    me.windowInfo = infoWin;
                }

                try {
                    angular.element('#' + options.Element).data("openMap", me);
                } catch (e) { }

                return me.map;
            }
            else
                return null;
        }

        this.NewMarker = function (pLat, pLng, pTitle, pStyle, pData) {
            var me = this;

            var feature = new ol.Feature({
                geometry: new ol.geom.Point(me.Point(pLat, pLng)), name: pTitle, data: pData, type: mapFeature.marker
            });
            feature.setStyle(pStyle || me.mapStyle._Default);
            
            var vectorLayer = new ol.layer.Vector({
                source: new ol.source.Vector({
                    features: []
                })
            });
            
            me.map.getLayers().forEach(function (o, i) {
                if (o.get('Name') == 'MapMarker') {
                    vectorLayer = o;
                }
            });

            vectorLayer.getSource().addFeature(feature);

            return feature;
        }
        this.NewRoute = function (pTitle, pPointFrom, pPointTo, pStyle, pData, pMap) {
            var me = this;
            try {
                var p1 = me._to4326(pPointFrom.getGeometry().getCoordinates());
                var p2 = me._to4326(pPointTo.getGeometry().getCoordinates());
                var sF = Math.round(p1[1] * 1000000) / 1000000 + "," + Math.round(p1[0] * 1000000) / 1000000;
                var sT = Math.round(p2[1] * 1000000) / 1000000 + "," + Math.round(p2[0] * 1000000) / 1000000;

                me._toJson(apiUrl.viaroute + sF + "&loc=" + sT + apiUrl.instructions).when({
                    ready: function (res) {
                        Common.Log(res);
                        if (res.status != 200) {
                            return;
                        }

                        if (!Common.HasValue(pStyle)) {
                            pStyle = me.mapStyle._Default;
                        }

                        var feature = new ol.Feature({
                            name: pTitle, type: mapFeature.route, geometry: new ol.format.Polyline({
                                factor: 1e6
                            }).readGeometry(res.route_geometry, {
                                dataProjection: 'EPSG:4326', featureProjection: 'EPSG:3857'
                            })
                        });
                        feature.setStyle(pStyle);
                        
                        var vectorLayer = new ol.layer.Vector();
                        vectorLayer.setSource(new ol.source.Vector({
                            features: []
                        }))
                        
                        if (Common.HasValue(pMap))
                            me.map = pMap;

                        me.map.getLayers().forEach(function (o, i) {
                            if (o.get('Name') == 'MapRoute') {
                                vectorLayer = o;
                            }
                        });

                        vectorLayer.getSource().addFeature(feature);

                        if (Common.HasValue(pData))
                            pData.push(feature);
                    }
                })
            }
            catch (e) { }
        }
        
        this.Marker = function (pLat, pLng, pTitle, pStyle, pData) {
            var me = this;
            
            var feature = new ol.Feature({
                geometry: new ol.geom.Point(me.Point(pLat, pLng)), name: pTitle, data: pData, type: mapFeature.marker
            });
            feature.setStyle(pStyle || me.mapStyle._Default);
            
            var vectorLayer = new ol.layer.Vector({
                source: new ol.source.Vector({
                    features: [feature]
                })
            });
            
            if (Common.HasValue(me.map))
                me.map.addControl(vectorLayer);
            me.mapMarker.push(vectorLayer);
            
            return vectorLayer;
        }
        this.Polygon = function (pTitle, pPoints, pStyle) {
            var me = this;

            var feature = new ol.Feature({
                geometry: new ol.geom.Polygon(pPoints), name: pTitle, type: mapFeature.polygon
            });
            feature.setStyle(pStyle || me.mapStyle._Default);

            var vectorLayer = new ol.layer.Vector({
                source: new ol.source.Vector({
                    features: [feature]
                })
            });

            if (Common.HasValue(me.map))
                me.map.addControl(vectorLayer);
            me.mapPolygon.push(vectorLayer);

            return vectorLayer;
        }
        this.MultiPolygon = function (pTitle, pPoints, pStyle) {
            var me = this;

            var feature = new ol.Feature({
                geometry: new ol.geom.MultiPolygon(pPoints), name: pTitle, type: mapFeature.polygon
            });
            feature.setStyle(pStyle || me.mapStyle._Default);

            var vectorLayer = new ol.layer.Vector({
                source: new ol.source.Vector({
                    features: [feature]
                })
            });

            if (Common.HasValue(me.map))
                me.map.addControl(vectorLayer);
            me.mapPolygon.push(vectorLayer);

            return vectorLayer;
        }
        this.Polyline = function (pTitle, pPoints, pStyle) {
            var me = this;

            var feature = new ol.Feature({
                geometry: new ol.geom.LineString(pPoints, 'XY'), name: pTitle, type: mapFeature.polyline
            });
            feature.setStyle(pStyle || me.mapStyle._Default);

            var vectorLayer = new ol.layer.Vector({
                source: new ol.source.Vector({
                    features: [feature]
                })
            });

            if (Common.HasValue(me.map))
                me.map.addControl(vectorLayer);
            me.mapPolyline.push(vectorLayer);

            return vectorLayer;
        }
        this.Route = function (pTitle, pPointFrom, pPointTo, pStyle, pData, pMap) {
            var me = this;
            try {
                var p1 = me._to4326(pPointFrom.getSource().getFeatures()[0].getGeometry().getCoordinates());
                var p2 = me._to4326(pPointTo.getSource().getFeatures()[0].getGeometry().getCoordinates());
                var sF = Math.round(p1[1] * 1000000) / 1000000 + "," + Math.round(p1[0] * 1000000) / 1000000;
                var sT = Math.round(p2[1] * 1000000) / 1000000 + "," + Math.round(p2[0] * 1000000) / 1000000;

                me._toJson(apiUrl.viaroute + sF + "&loc=" + sT + apiUrl.instructions).when({
                    ready: function (res) {
                        Common.Log(res);
                        if (res.status != 200) {
                            return;
                        }

                        if (!Common.HasValue(pStyle)) {
                            pStyle = me.mapStyle._Default;
                        }

                        var feature = new ol.Feature({
                            name: pTitle, type: mapFeature.route, geometry: new ol.format.Polyline({
                                factor: 1e6
                            }).readGeometry(res.route_geometry, {
                                dataProjection: 'EPSG:4326', featureProjection: 'EPSG:3857'
                            })
                        });
                        feature.setStyle(pStyle);

                        var vectorLayer = new ol.layer.Vector();
                        vectorLayer.setSource(new ol.source.Vector({
                            features: [feature]
                        }))

                        //vectorLayer.setZIndex(1000);

                        if (!Common.HasValue(pMap))
                            pMap = me.map;
                        pMap.addControl(vectorLayer);
                        me.mapRoute.push(vectorLayer);
                        
                        if (Common.HasValue(pData))
                            pData.push(vectorLayer);
                    }
                })
            }
            catch (e) { }
        }
        this.Distance = function (p1, p2, callback, o) {
            ///o: callbackParam
            var me = this;
            try {
                var sF = Math.round(p1.Lat * 1000000) / 1000000 + "," + Math.round(p1.Lng * 1000000) / 1000000;
                var sT = Math.round(p2.Lat * 1000000) / 1000000 + "," + Math.round(p2.Lng * 1000000) / 1000000;

                Common.Log("Request: " + apiUrl.viaroute + sF + "&loc=" + sT + apiUrl.instructions);
                me._toJson(apiUrl.viaroute + sF + "&loc=" + sT + apiUrl.instructions).when({
                    ready: function (res) {
                        if (res.status != 200) {
                            return;
                        }
                        else {
                            if (Common.HasValue(callback)) {
                                var obj = {
                                    Distance: res.route_summary.total_distance,
                                    Time: res.route_summary.total_time,
                                    Data: o
                                }
                                callback(p1, p2, obj);
                            }
                        }
                    }
                })
            }
            catch (e) { }
        }
        this.Close = function () {
            var me = this;
            if (Common.HasValue(me.windowInfo)) {
                var element = me.windowInfo.getElement().parentElement;
                element.style.display = "none";
            }
        }
        this.FitBound = function (pData, pZoom) {
            var me = this;

            var ex = ol.extent.createEmpty();
            try {
                Common.Data.Each(pData, function (o) {
                    if (Common.HasValue(o)) {
                        ol.extent.extend(ex, o.getSource().getExtent());
                    }
                })                
            } catch (e) {
            }

            if (ex.length > 0) {
                var size = me.map.getSize();

                me.map.getView().fit(ex, [size[0] - 50, size[1] - 50]);
                if (pData.length == 1)
                    me.map.getView().setZoom(pZoom || 12);
            }
        }
        this.SetVisible = function (mapLayers, visible) {
            var me = this;
            if (Common.HasValue(mapLayers)) {
                $.each(mapLayers, function (i, v) {
                    if (v != null)
                        try {
                            v.setVisible(visible);
                        } catch (e) { }
                })
                me.map.render();
            } else {
                $.each(me.mapRoute, function (i, v) {
                    if (v != null)
                        v.setVisible(visible);
                })
                $.each(me.mapMarker, function (i, v) {
                    if (v != null)
                        v.setVisible(visible);
                })
                $.each(me.mapPolygon, function (i, v) {
                    if (v != null)
                        v.setVisible(visible);
                })
                $.each(me.mapPolyline, function (i, v) {
                    if (v != null)
                        v.setVisible(visible);
                })
                me.map.render();
            }
        }
        this.GetDataFromLayer = function (mapLayer) {
            try {
                return mapLayer.getSource().getFeatures()[0].getProperties();
            }
            catch (e) { return {} }
        }
        this.GetFeatureFromLayer = function (mapLayer) {
            try {
                return mapLayer.getSource().getFeatures()[0];
            }
            catch (e) { return {} }
        }
        this.Clear = function () {
            var me = this;
            me.ClearMarker();
            me.ClearRoute();
            me.ClearPolygon();
            me.ClearPolyline();
        }
        this.ClearRoute = function () {
            var me = this;
            $.each(me.mapRoute, function (i, v) {
                if (v != null)
                    v.setVisible(false);
            })
            me.mapRoute = [];
            me.map.render();
        }
        this.ClearMarker = function () {
            var me = this;
            $.each(me.mapMarker, function (i, v) {
                if (v != null)
                    v.setVisible(false);
            })
            me.mapMarker = [];
            me.map.render();
        }

        this.ClearPolygon = function () {
            var me = this;
            $.each(me.mapPolygon, function (i, v) {
                if (v != null)
                    v.setVisible(false);
            })
            me.mapPolygon = [];
            me.map.render();
        }
        this.ClearPolyline = function () {
            var me = this;
            $.each(me.mapPolyline, function (i, v) {
                if (v != null)
                    v.setVisible(false);
            })
            me.mapPolyline = [];
            me.map.render();
        }
        this.Resize = function () {
            var me = this;
            try {
                me.map.updateSize();
                me.map.render()
            } catch (e) { }
        }
        this.HighLight = function (feature, style) {
            var me = this;
            try {
                if (Common.HasValue(feature))
                    feature.setStyle(style || me.mapStyle._Default);
            } catch (e) {
                Common.Log(e.message);
            }
        }
        this.MarkerAnimate = function (feature, timeout, total, skip, max, color) {
            var me = this;

            timeout = timeout || 200;
            if (Common.HasValue(feature)) {
                feature.set('markerAnimate', true);
                var style = feature.getStyle();
                feature.set('createStyle', style);
                var r = style.getImage().getRadius();
                var f = style.getImage().getFill().getColor();
                var s = style.getImage().getStroke().getColor();
                var sw = style.getImage().getStroke().getWidth();

                var interval = 0, w = sw;
                var myVar = setInterval(function () {
                    interval++;
                    var a = feature.get('markerAnimate');
                    if (a && interval != total) {
                        w = w + skip;
                        if (w > sw + max)
                            w = sw;
                        feature.setStyle(me.mapStyle.IconCircle(r, f, color, w));
                    } else if (total != null) {
                        clearInterval(myVar);
                        feature.set('markerAnimate', false);
                        feature.setStyle(feature.get('createStyle'));
                    }
                }, timeout);
                feature.set('intervalId', myVar);
            }
        }
        this.MarkerAnimateStop = function (feature) {
            try {
                clearInterval(feature.get('intervalId'));
                feature.set('markerAnimate', false);
                feature.setStyle(feature.get('createStyle'));
            }
            catch (e) {
                Common.Log(e.message);
            }
        },
        this.ProvincePolygon = function (code, name, map) {
            this.code = code;
            this.name = name;
            this.item = null;

            this.Create = function (style, callback) {
                var me = this;
                var id = map.provinceCode[me.code];

                map._toJson(apiUrl.geojson + id + apiUrl.suffix).when({
                    ready: function (res) {
                        if (res.type == 'Polygon') {
                            var data = [];
                            Common.Data.Each(res.coordinates[0], function (o) {
                                var p = map.Point(o[1], o[0]);
                                data.push(p);
                            })
                            me.item = map.Polygon(me.name, [data], style);
                        } else if (res.type == 'MultiPolygon') {
                            var data = [];
                            Common.Data.Each(res.coordinates, function (coordinates) {
                                var coor = [];
                                Common.Data.Each(coordinates[0], function (o) {
                                    var p = map.Point(o[1], o[0]);
                                    coor.push(p);
                                })
                                data.push(coor);
                            })
                            me.item = map.MultiPolygon(me.name, [data], style);
                        }

                        if (Common.HasValue(callback))
                            callback();
                    }
                })
            }
            this.Delete = function () {
                var me = this;
                if (Common.HasValue(me.item)) {
                    me.item.setVisible(false);
                    map.map.render();
                }
            }
            this.Center = function () {
                var me = this;
                if (Common.HasValue(me.item)) {
                    map.FitBound([me.item], 9);
                }
            }
        }
        this.GetRotation = function(cx, cy, ex, ey) {
            var dy = ey - cy;
            var dx = ex - cx;
            var val = Math.atan2(dy, dx);
            if (val > Math.PI / 2)
                return val - Math.PI / 2;
            else
                return Math.PI / 2 + val;
        }
        this.AddControl = function (pOptions) {
            var me = this;

            var options = $.extend({
                Text: '',
                Title: '',
                Click: null,
                ClassName: ''
            }, true, pOptions);

            var button = document.createElement('button');
            button.innerHTML = options.Text;
            button.title = options.Title;
            if (Common.HasValue(options.Click)) {
                button.addEventListener('click', function (e) {
                    setTimeout(function () {
                        options.Click(e, this, me);
                    }, 1)
                }, false);
            }
            var element = document.createElement('div');
            element.className = options.ClassName + ' ol-unselectable ol-control';
            element.appendChild(button);
            me.map.addControl(new ol.control.Control({
                element: element
            }));
        }
        this.ActiveMap = function (map) {
            var me = this;

            me.map = map;
        }
        this.SetCenter = function (pLat, pLng, pZoom) {
            var me = this;
            try {
                me.map.getView().setCenter(me.Point(pLat, pLng));
                if (Common.HasValue(pZoom))
                    me.map.getView().setZoom(pZoom);
            } catch (e) {
            }
        }
    }

    return new openMap();
});