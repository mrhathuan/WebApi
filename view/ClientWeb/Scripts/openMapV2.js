/// <reference path="~/Scripts/jquery-1.9.1.intellisense.js" />
/// <reference path="~/Scripts/common.js" />
/// <reference path="~/Scripts/ol3/ol.js" />
angular.module('myapp').factory('openMapV2', function () {
    var apiUrl = {
        viaroute: 'http://router.project-osrm.org/route/v1/driving/',
        viamapbox: 'https://api.mapbox.com/directions/v5/mapbox/driving/',
        geojson: 'http://global.mapit.mysociety.org/area/',
        geocoding: 'https://api.mapbox.com/geocoding/v5/mapbox.places/',
        suffix: '.geojson?simplify_tolerance=0.0001',
        instructions: '.json?geometries=geojson&overview=simplified&alternatives=false&steps=false&access_token=pk.eyJ1IjoiaHVuZ2hvYW5nc21sIiwiYSI6ImNpcWx5OWhoMTAwYXZmcG5oOWZvcHI3eTYifQ.hKLiBbe5enCLWitk9OsKtQ'
    }
    var mapEPSG = { E4326: 'EPSG:4326', E3857: 'EPSG:3857' }

    var openMap = function () {
        this.map = null;
        this.hasMap = true;
        this.windowInfo = null;
        this.maxZoom = 20;

        this._mapTile = {
            osm: new ol.layer.Tile({
                source: new ol.source.OSM(), visible: true
            }),
            mapBox: new ol.layer.Tile({
                source: new ol.source.XYZ({
                    url: 'https://api.tiles.mapbox.com/v4/mapbox.streets/{z}/{x}/{y}.png?access_token=pk.eyJ1IjoiaHVuZ2hvYW5nc21sIiwiYSI6ImNpcWx5OWhoMTAwYXZmcG5oOWZvcHI3eTYifQ.hKLiBbe5enCLWitk9OsKtQ'
                }),
                visible: false
            }),
            mapBoxLight: new ol.layer.Tile({
                source: new ol.source.XYZ({
                    url: 'https://api.tiles.mapbox.com/v4/mapbox.light/{z}/{x}/{y}.png?access_token=pk.eyJ1IjoiaHVuZ2hvYW5nc21sIiwiYSI6ImNpcWx5OWhoMTAwYXZmcG5oOWZvcHI3eTYifQ.hKLiBbe5enCLWitk9OsKtQ'
                }),
                visible: true
            })
        }
        this._mapStyle = new ol.style.Style({
            fill: new ol.style.Fill({ color: 'rgba(0, 0, 255, 0.2)' }),
            image: new ol.style.Circle({ radius: 10, fill: new ol.style.Fill({ color: 'rgba(0, 0, 255, 0.8)' }), stroke: new ol.style.Stroke({ color: 'white', width: 2 }) }),
            stroke: new ol.style.Stroke({ color: 'rgba(0, 0, 255, 0.8)', width: 4 })
        })
        this._mapStyleHidden = new ol.style.Style({
            fill: new ol.style.Fill({ color: 'rgba(0, 0, 0, 0)' }),
            image: new ol.style.Circle({ radius: 10, fill: new ol.style.Fill({ color: 'rgba(0, 0, 0, 0)' }), stroke: new ol.style.Stroke({ color: 'rgba(0, 0, 0, 0)', width: 0 }) }),
            stroke: new ol.style.Stroke({ color: 'rgba(0, 0, 0, 0)', width: 0 })
        })
        this._mapFeature = { marker: 1, route: 2, polyline: 3, polygon: 4 }
        this._mapProvince = {
            '01': 793923, '02': 794014, '03': 793918, '04': 793912, '05': 687794, '06': 508457, '07': 687792, '08': 793922, '09': 15143, '10': 795293,
            '11': 15144, '12': 508527, '13': 15138, '14': 15139, '15': 15132, '16': 508524, '17': 793921, '18': 793920, '19': 793919, '20': 0,
            '21': 508523, '22': 508521, '23': 687791, '24': 793917, '25': 793916, '26': 687787, '27': 687785, '28': 687780, '29': 687779, '30': 687778,
            '31': 687776, '32': 687775, '33': 687774, '34': 793911, '35': 793909, '36': 793904, '37': 793908, '38': 793905, '39': 687768, '40': 508498,
            '41': 793907, '42': 508500, '43': 793913, '44': 793925, '45': 793906, '46': 793914, '47': 687796, '48': 687798, '49': 793902, '50': 793899,
            '51': 793898, '52': 793924, '53': 793901, '54': 793897, '55': 793895, '56': 687762, '57': 687761, '58': 687758, '59': 687757, '60': 687756,
            '61': 793894, '62': 364265, '63': 508499, '64': 508488
        }

        this._to3875 = function (val) {
            return ol.proj.transform(val, mapEPSG.E4326, mapEPSG.E3857)
        }
        this._to4326 = function (val) {
            return ol.proj.transform(val, mapEPSG.E3857, mapEPSG.E4326)
        }
        this._toJson = function (url) {
            var xhr = new XMLHttpRequest(),
                when = {},
                onload = function () {
                    if (xhr.status === 200) {
                        when.ready.call(undefined, JSON.parse(xhr.response));
                    } else {
                        console.error("Status: " + xhr.status);
                        when.retry.call(undefined, xhr.response);
                    }
                },
                onerror = function (ex) {
                    console.error("Status: " + xhr.status);
                    when.retry.call(undefined, xhr.response);
                },
                onprogress = function () { };
            xhr.open("GET", url, true);
            xhr.setRequestHeader("Accept", "application/json");
            xhr.onload = onload;
            xhr.onerror = onerror;
            xhr.onprogress = onprogress;
            try {
                xhr.send(null);
                return {
                    when: function (obj) {
                        when.ready = obj.ready;
                        when.retry = obj.retry;
                    }
                };
            } catch (e) {
            }
        }
        this._toJsonSync = function (url) {
            var xhr = new XMLHttpRequest(),
                when = {},
                onload = function () { if (xhr.status !== 200) { console.error("Status: " + xhr.status); } },
                onerror = function () { console.error("Can't XHR " + JSON.stringify(url)); },
                onprogress = function () { };
            xhr.open("GET", url, false);
            xhr.setRequestHeader("Accept", "application/json");
            xhr.onload = onload;
            xhr.onerror = onerror;
            xhr.onprogress = onprogress;
            xhr.send(null);
            return JSON.parse(xhr.response);
        }

        this._isValidLatLng = function (pLat, pLng) {
            if (Math.sqrt(Math.pow(pLat, 2)) > 90 || Math.sqrt(Math.pow(pLng, 2)) > 180) {
                return false;
            } else {
                return true;
            }
        }

        this.Init = function (pOptions) {
            var me = this;

            var options = $.extend({
                Element: '',
                Zoom: 11,
                MaxZoom: 20,
                Lat: 10.8018735,
                Lng: 106.7200799,
                ClickMap: null,
                HoverLayer: null,
                ClickMarker: null,
                DefinedLayer: [],
                ProvincePolygon: false,
                Tooltip_Show: false,
                Tooltip_Element: "",
                InfoWin_Show: false,
                InfoWin_ShowWhenClick: false,
                InfoWin_Element: "",
                InfoWin_Width: null,
                InfoWin_Height: null,
                MapBoxLightDefault: true
            }, true, pOptions);
            me.maxZoom = options.MaxZoom;
            if (me.hasMap) {
                try {
                    var viewPort = document.getElementById(options.Element);
                    viewPort.innerHTML = "";
                }
                catch (e) { }

                var mapView = new ol.View({
                    center: this.NewPoint(options.Lat, options.Lng), zoom: options.Zoom, maxZoom: me.maxZoom
                });

                var vLine = me.NewVector("MapLine", 89);
                var vRoute = me.NewVector("MapRoute", 89);
                var vMarker = me.NewVector("MapMarker", 99);
                var vPolygon = me.NewVector("MapPolygon", 79);
                var vProvincePolygon = me.NewVector("ProvincePolygon", 69);

                if (!options.MapBoxLightDefault) {
                    me._mapTile.mapBox.setVisible(true);
                    me._mapTile.mapBoxLight.setVisible(false);
                }
                var mLayer = [me._mapTile.mapBoxLight, me._mapTile.mapBox, vMarker, vRoute, vLine, vPolygon, vProvincePolygon];

                Common.Data.Each(options.DefinedLayer, function (o) {
                    var v = me.NewVector(o.Name, o.zIndex);
                    mLayer.push(v);
                })

                me.map = new ol.Map({
                    view: mapView, layers: mLayer, target: options.Element
                });

                var button = document.createElement('button');
                button.innerHTML = 'L';
                button.title = 'Toggle map';
                var fn = function (e) {
                    me._mapTile.mapBox.setVisible(!me._mapTile.mapBox.getVisible());
                    me._mapTile.mapBoxLight.setVisible(!me._mapTile.mapBoxLight.getVisible());
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
                            var type = feature.get('type');
                            if (type == me._mapFeature.marker) {
                                flag = true;
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
                            } else if (type == me._mapFeature.polyline) {

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
                        if (Common.HasValue(feature) && feature.get('type') != 4) {
                            var geometry = feature.getGeometry(),
                                coord = geometry.getCoordinates(),
                                distance = feature.get('Distance'),
                                title = feature.get('name'),
                                time = feature.get('Time'),
                                showInfo = false;

                            if (feature.get('type') == 2 || feature.get('type') == 3) {
                                showInfo = feature.get('ShowInfo');
                                tooltip.setPosition(me.map.getCoordinateFromPixel(me.map.getEventPixel(e.originalEvent)));
                            } else {
                                tooltip.setPosition(coord);
                            }
                            if (showInfo) {
                                if (title == null || title == "")
                                    title = "Khoảng cách: " + distance;// + ", Thời gian: " + time;
                                else
                                    title = title + ": " + distance;// + ", Thời gian: " + time;
                            }
                            if (title != null && title != '') {
                                $(tooltip_element).show();
                                $(tooltip_element).text(title);
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

                if (options.InfoWin_Show && Common.HasValue(options.InfoWin_Element)) {
                    var infoWin = new ol.Overlay({
                        element: document.getElementById(options.InfoWin_Element)
                    });
                    me.map.addOverlay(infoWin);
                    try {
                        angular.element('#' + options.InfoWin_Element).data("mapInfo", infoWin);
                    } catch (e) { }

                    setTimeout(function () {
                        var ele = infoWin.getElement().parentElement;
                        if (options.InfoWin_Width > 0) ele.style.width = options.InfoWin_Width + "px";
                        if (options.InfoWin_Height > 0) ele.style.height = options.InfoWin_Height + "px";
                    }, 1000)

                    me.windowInfo = infoWin;
                }

                if (options.ProvincePolygon && false) {
                    $.each(me._mapProvince, function (id, code) {
                        if (Common.HasValue(id)) {
                            var res = me._toJson(apiUrl.geojson + code + apiUrl.suffix);

                            me._toJson(apiUrl.geojson + code + apiUrl.suffix).when({
                                ready: function (res) {
                                    if (res.type == 'Polygon') {
                                        var data = [];
                                        Common.Data.Each(res.coordinates[0], function (o) {
                                            data.push(me.NewPoint(o[1], o[0]));
                                        })
                                        me.NewPolygon([data], "" + id, "", me._mapStyle, "ProvincePolygon");
                                    } else if (res.type == 'MultiPolygon') {
                                        var data = [];
                                        Common.Data.Each(res.coordinates, function (coordinates) {
                                            var coor = [];
                                            Common.Data.Each(coordinates[0], function (o) {
                                                var p = me.NewPoint(o[1], o[0]);
                                                coor.push(p);
                                            })
                                            data.push(coor);
                                        })
                                        me.NewMultiPolygon([data], "" + id, "", me._mapStyle, "ProvincePolygon");
                                    }
                                }
                            })
                        }
                    })
                }

                try {
                    angular.element('#' + options.Element).data("openMap", me);
                } catch (e) { }

                return me.map;
            }
            else { }
        }
        this.Destroy = function (element) {
            try {
                var viewPort = document.getElementById(element);
                viewPort.innerHTML = "";
                this.map = null;
            }
            catch (e) { }
        }
        this.NewPoint = function (pLat, pLng) {
            return this._to3875([pLng, pLat]);
        }
        this.NewStyle = {
            Icon: function (img, opacity, text, textColor, textInside) {
                return new ol.style.Style({
                    text: new ol.style.Text({ text: text || "", font: "bold 13px Arial", fill: new ol.style.Fill({ color: textColor || '#31B6FC' }), offsetY: textInside ? -30 : 5, textBaseline: 'top' }),
                    image: new ol.style.Icon({ scale: 0.8, anchor: [0.5, 1], opacity: opacity, src: img })
                });
            },
            Line: function (width, sColor, lineDash, text, textColor) {
                return new ol.style.Style({
                    text: new ol.style.Text({ text: text, font: "bold 13px Arial", fill: new ol.style.Fill({ color: textColor || 'white' }), textBaseline: 'top' }),
                    stroke: new ol.style.Stroke({ color: sColor, width: width, lineDash: lineDash })
                });
            },
            Circle: function (width, fColor, sColor, sWidth, text, textColor) {
                return new ol.style.Style({
                    text: new ol.style.Text({ text: text, font: "bold 13px Arial", fill: new ol.style.Fill({ color: textColor || 'white' }) }),
                    image: new ol.style.Circle({ radius: width || 10, fill: new ol.style.Fill({ color: fColor || 'rgba(0, 0, 255, 0.8)' }), stroke: new ol.style.Stroke({ color: sColor || 'white', width: sWidth || 2 }) })
                });
            },
            Polygon: function (width, fColor, sColor, lineDash, text, textColor) {
                return new ol.style.Style({
                    fill: new ol.style.Fill({ color: fColor || 'transparent' }),
                    text: new ol.style.Text({ text: text, font: "bold 13px Arial", fill: new ol.style.Fill({ color: textColor || 'white' }) }),
                    stroke: new ol.style.Stroke({ color: sColor, width: width })
                });
            }
        }
        this.NewImage = {
            Color: {
                Dark: 'dark',
                Blue: 'blue',
                Green: 'green',
                Orange: 'orange',
                Purple: 'purple',
                Deepblue: 'deepblue',
                Deepgreen: 'deepgreen'
            },
            
            Get: '/Images/map/icon/blue/ico_get.png',
            End: '/Images/map/icon/blue/ico_end.png',
            Start: '/Images/map/icon/blue/ico_start.png',
            Empty: '/Images/map/icon/blue/ico_empty.png',
            Stock: '/Images/map/icon/blue/ico_stock.png',
            Depot: '/Images/map/icon/blue/ico_depot.png',
            Seaport: '/Images/map/icon/blue/ico_seaport.png',
            Delivery: '/Images/map/icon/blue/ico_delivery.png',
            Location: '/Images/map/icon/blue/ico_location.png',
            RomoocGet: '/Images/map/icon/blue/ico_get_romooc.png',
            Distributor: '/Images/map/icon/blue/ico_distributor.png',
            RomoocReturn: '/Images/map/icon/blue/ico_return_romooc.png',

            //#region Vehicle
            Truck: '/Images/map/icon/orange/ico_truck.png',
            Tractor: '/Images/map/icon/orange/ico_tractor.png',
            Romooc_20: '/Images/map/icon/orange/ico_romooc_20.png',
            Romooc_40: '/Images/map/icon/orange/ico_romooc_40.png',
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
            Tractor_Romooc_Con_40_20x2EF: '/Images/map/icon/blue/ico_tr_ro_co_2x20ef.png',
            //#endregion

            NewGet: function (color) { return '/Images/map/icon/' + color + '/ico_get.png' },
            NewEnd: function (color) { return '/Images/map/icon/' + color + '/ico_end.png' },
            NewStart: function (color) { return '/Images/map/icon/' + color + '/ico_start.png' },
            NewEmpty: function (color) { return '/Images/map/icon/' + color + '/ico_empty.png' },
            NewStock: function (color) { return '/Images/map/icon/' + color + '/ico_stock.png' },
            NewDepot: function (color) { return '/Images/map/icon/' + color + '/ico_depot.png' },
            NewSeaport: function (color) { return '/Images/map/icon/' + color + '/ico_seaport.png' },
            NewDelivery: function (color) { return '/Images/map/icon/' + color + '/ico_delivery.png' },
            NewLocation: function (color) { return '/Images/map/icon/' + color + '/ico_location.png' },
            NewRomoocGet: function (color) { return '/Images/map/icon/' + color + '/ico_get_romooc.png' },
            NewDistributor: function (color) { return '/Images/map/icon/' + color + '/ico_distributor.png' },
            NewRomoocReturn: function (color) { return '/Images/map/icon/' + color + '/ico_return_romooc.png' },

            NewTruck: function (color) { return '/Images/map/icon/' + color + '/ico_truck.png' },
            NewTractor: function (color) { return '/Images/map/icon/' + color + '/ico_tractor.png' },
            NewRomooc_20: function (color) { return '/Images/map/icon/' + color + '/ico_romooc_20.png' },
            NewRomooc_40: function (color) { return '/Images/map/icon/' + color + '/ico_romooc_40.png' },
            NewCon_20E: function (color) { return '/Images/map/icon/' + color + '/ico_con_20.png' },
            NewCon_40E: function (color) { return '/Images/map/icon/' + color + '/ico_con_40.png' },
            NewCon_20F: function (color) { return '/Images/map/icon/' + color + '/ico_con_20f.png' },
            NewCon_40F: function (color) { return '/Images/map/icon/' + color + '/ico_con_40f.png' },
            NewRomooc_Con_20E: function (color) { return '/Images/map/icon/' + color + '/ico_ro_co_20e.png' },
            NewRomooc_Con_20F: function (color) { return '/Images/map/icon/' + color + '/ico_ro_co_20f.png' },
            NewRomooc_Con_20E: function (color) { return '/Images/map/icon/' + color + '/ico_ro_co_40e.png' },
            NewRomooc_Con_20F: function (color) { return '/Images/map/icon/' + color + '/ico_ro_co_40f.png' },
            NewTractor_Romooc_20: function (color) { return '/Images/map/icon/' + color + '/ico_tr_ro_20.png' },
            NewTractor_Romooc_40: function (color) { return '/Images/map/icon/' + color + '/ico_tr_ro_40.png' },
            NewTractor_Romooc_Con_20E: function (color) { return '/Images/map/icon/' + color + '/ico_tr_ro_co_20e.png' },
            NewTractor_Romooc_Con_20F: function (color) { return '/Images/map/icon/' + color + '/ico_tr_ro_co_20f.png' },
            NewTractor_Romooc_Con_40E: function (color) { return '/Images/map/icon/' + color + '/ico_tr_ro_co_40e.png' },
            NewTractor_Romooc_Con_40F: function (color) { return '/Images/map/icon/' + color + '/ico_tr_ro_co_40f.png' },
            NewTractor_Romooc_Con_40_20x2E: function (color) { return '/Images/map/icon/' + color + '/ico_tr_ro_co_2x20e.png' },
            NewTractor_Romooc_Con_40_20x2F: function (color) { return '/Images/map/icon/' + color + '/ico_tr_ro_co_2x20f.png' },
            NewTractor_Romooc_Con_40_20x2EF: function (color) { return '/Images/map/icon/' + color + '/ico_tr_ro_co_2x20ef.png' },
        }
        this.NewVector = function (name, zindex) {
            var me = this;
            if (!me.hasMap) return;
            var v = new ol.layer.Vector({
                source: new ol.source.Vector({
                    features: []
                })
            });
            v.set('Name', name);
            v.setZIndex(zindex);
            return v;
        }
        this.NewMarker = function (pLat, pLng, pCode, pTitle, pStyle, pData, pVector) {
            var me = this;
            if (!Common.HasValue(me.map) || !me.hasMap) return;
            try {
                if (!me._isValidLatLng(pLat, pLng))
                    return;
                if (!Common.HasValue(me.map)) return;
                pVector = pVector || 'MapMarker';
                var feature = new ol.Feature({
                    geometry: new ol.geom.Point(me.NewPoint(pLat, pLng)), name: pTitle, code: pCode, data: pData, type: me._mapFeature.marker
                });
                feature.setStyle(pStyle || me._mapStyle);
                feature.set("zStyle", pStyle || me._mapStyle);

                var flag = false;
                var vectorLayer = me.NewVector(pVector, Infinity);
                me.map.getLayers().forEach(function (o, i) {
                    if (o.get('Name') == pVector) {
                        vectorLayer = o;
                        flag = true;
                    }
                });
                vectorLayer.getSource().addFeature(feature);

                if (!flag)
                    me.map.addControl(vectorLayer);

                return feature;
            } catch (e) {
            }
        }
        this.NewRoute = function (pPointFrom, pPointTo, pCode, pTitle, pStyle, pData, pVector, pMap, pCallback, pShowDistance) {
            var me = this;
            if (!Common.HasValue(me.map) || !me.hasMap) return;

            try {
                var p1, p2;
                if (pPointFrom instanceof ol.Feature) {
                    p1 = me._to4326(pPointFrom.getGeometry().getCoordinates());
                } else {
                    p1 = pPointFrom;
                }
                if (pPointTo instanceof ol.Feature) {
                    p2 = me._to4326(pPointTo.getGeometry().getCoordinates());
                } else {
                    p2 = pPointTo;
                }
                var sF = Math.round(p1[0] * 1000000) / 1000000 + "," + Math.round(p1[1] * 1000000) / 1000000;
                var sT = Math.round(p2[0] * 1000000) / 1000000 + "," + Math.round(p2[1] * 1000000) / 1000000;

                me._toJson(apiUrl.viaroute + sF + ";" + sT).when({
                    ready: function (res) {
                        if (res.code !== "Ok") {
                            me._toJson(apiUrl.viamapbox + sF + ";" + sT + apiUrl.instructions).when({
                                ready: function (res) {
                                    if (res.code !== "Ok") {
                                        return;
                                    }
                                    var dataLocation = [];
                                    if (res.routes[0].geometry.type === "LineString") {
                                        Common.Data.Each(res.routes[0].geometry.coordinates, function (o) { dataLocation.push(me._to3875(o)); })
                                    }
                                    var feature = new ol.Feature({
                                        code: pCode, name: pTitle, data: pData, type: me._mapFeature.route, geometry: new ol.geom.LineString(dataLocation)
                                    });
                                    feature.setStyle(pStyle || me._mapStyle);
                                    feature.set("zStyle", pStyle || me._mapStyle);
                                    feature.set("Distance", Math.round(res.routes[0].distance / 100) / 10 + " km");
                                    feature.set("Time", Math.round(res.routes[0].duration / 60) + " phút");
                                    feature.set("ShowInfo", pShowDistance);

                                    pVector = pVector || 'MapRoute';

                                    if (Common.HasValue(pMap))
                                        me.map = pMap;

                                    var flag = false;
                                    var vectorLayer = me.NewVector(pVector, Infinity);

                                    me.map.getLayers().forEach(function (o, i) {
                                        if (o.get('Name') == pVector) {
                                            vectorLayer = o;
                                            flag = true;
                                        }
                                    });
                                    vectorLayer.getSource().addFeature(feature);

                                    if (!flag)
                                        me.map.addControl(vectorLayer);

                                    if (Common.HasValue(pCallback)) {
                                        pCallback(feature);
                                    }
                                },
                                retry: function () { }
                            })
                        }
                        var feature = new ol.Feature({
                            code: pCode, name: pTitle, data: pData, type: me._mapFeature.route, geometry: new ol.format.Polyline({
                                factor: 1e5
                            }).readGeometry(res.routes[0].geometry, {
                                dataProjection: 'EPSG:4326', featureProjection: 'EPSG:3857'
                            })
                        });
                        feature.setStyle(pStyle || me._mapStyle);
                        feature.set("zStyle", pStyle || me._mapStyle);
                        feature.set("Distance", Math.round(res.routes[0].distance / 100) / 10 + " km");
                        feature.set("Time", Math.round(res.routes[0].duration / 60) + " phút");
                        feature.set("ShowInfo", pShowDistance);

                        pVector = pVector || 'MapRoute';

                        if (Common.HasValue(pMap))
                            me.map = pMap;

                        var flag = false;
                        var vectorLayer = me.NewVector(pVector, Infinity);

                        me.map.getLayers().forEach(function (o, i) {
                            if (o.get('Name') == pVector) {
                                vectorLayer = o;
                                flag = true;
                            }
                        });
                        vectorLayer.getSource().addFeature(feature);

                        if (!flag)
                            me.map.addControl(vectorLayer);

                        if (Common.HasValue(pCallback)) {
                            pCallback(feature);
                        }
                    },
                    retry: function (res) {
                        me._toJson(apiUrl.viamapbox + sF + ";" + sT + apiUrl.instructions).when({
                            ready: function (res) {
                                if (res.code !== "Ok") {
                                    return;
                                }
                                var dataLocation = [];
                                if (res.routes[0].geometry.type === "LineString") {
                                    Common.Data.Each(res.routes[0].geometry.coordinates, function (o) { dataLocation.push(me._to3875(o)); })
                                }
                                var feature = new ol.Feature({
                                    code: pCode, name: pTitle, data: pData, type: me._mapFeature.route, geometry: new ol.geom.LineString(dataLocation)
                                });
                                feature.setStyle(pStyle || me._mapStyle);
                                feature.set("zStyle", pStyle || me._mapStyle);
                                feature.set("Distance", Math.round(res.routes[0].distance / 100) / 10 + " km");
                                feature.set("Time", Math.round(res.routes[0].duration / 60) + " phút");
                                feature.set("ShowInfo", pShowDistance);

                                pVector = pVector || 'MapRoute';

                                if (Common.HasValue(pMap))
                                    me.map = pMap;

                                var flag = false;
                                var vectorLayer = me.NewVector(pVector, Infinity);

                                me.map.getLayers().forEach(function (o, i) {
                                    if (o.get('Name') == pVector) {
                                        vectorLayer = o;
                                        flag = true;
                                    }
                                });
                                vectorLayer.getSource().addFeature(feature);

                                if (!flag)
                                    me.map.addControl(vectorLayer);

                                if (Common.HasValue(pCallback)) {
                                    pCallback(feature);
                                }
                            },
                            retry: function () { }
                        })
                    }
                })
            }
            catch (e) { }
        }
        this.NewPolyLine = function (pPoints, pCode, pTitle, pStyle, pData, pVector) {
            var me = this;
            if (!Common.HasValue(me.map) || !me.hasMap) return;

            pVector = pVector || 'MapLine';
            var feature = new ol.Feature({
                geometry: new ol.geom.LineString(pPoints, 'XY'), name: pTitle, code: pCode, data: pData, type: me._mapFeature.polyline
            });
            feature.setStyle(pStyle || me._mapStyle);
            feature.set("zStyle", pStyle || me._mapStyle);

            var flag = false;
            var vectorLayer = me.NewVector(pVector, Infinity);

            me.map.getLayers().forEach(function (o, i) {
                if (o.get('Name') == pVector) {
                    vectorLayer = o;
                    flag = true;
                }
            });
            vectorLayer.getSource().addFeature(feature);

            if (!flag)
                me.map.addControl(vectorLayer);

            return feature;
        }
        this.NewMultiPolyLine = function (pPoints, pCode, pTitle, pStyle, pData, pVector) {
            var me = this;
            if (!Common.HasValue(me.map) || !me.hasMap) return;

            pVector = pVector || 'MapLine';
            var feature = new ol.Feature({
                geometry: new ol.geom.MultiLineString(pPoints, 'XY'), name: pTitle, code: pCode, data: pData, type: me._mapFeature.polyline
            });
            feature.setStyle(pStyle || me._mapStyle);
            feature.set("zStyle", pStyle || me._mapStyle);

            var flag = false;
            var vectorLayer = me.NewVector(pVector, Infinity);

            me.map.getLayers().forEach(function (o, i) {
                if (o.get('Name') == pVector) {
                    vectorLayer = o;
                    flag = true;
                }
            });
            vectorLayer.getSource().addFeature(feature);

            if (!flag)
                me.map.addControl(vectorLayer);

            return feature;
        }
        this.NewPolygon = function (pPoints, pCode, pTitle, pStyle, pData, pVector) {
            var me = this;
            if (!Common.HasValue(me.map) || !me.hasMap) return;

            pVector = pVector || 'MapPolygon';
            var feature = new ol.Feature({
                geometry: new ol.geom.Polygon(pPoints), name: pTitle, code: pCode, data: pData, type: me._mapFeature.polygon
            });
            feature.setStyle(pStyle || me._mapStyle);
            feature.set("zStyle", pStyle || me._mapStyle);

            var flag = false;
            var vectorLayer = me.NewVector(pVector, Infinity);

            me.map.getLayers().forEach(function (o, i) {
                if (o.get('Name') == pVector) {
                    vectorLayer = o;
                    flag = true;
                }
            });
            vectorLayer.getSource().addFeature(feature);

            if (!flag)
                me.map.addControl(vectorLayer);

            return feature;
        }
        this.NewMultiPolygon = function (pPoints, pCode, pTitle, pStyle, pData, pVector) {
            var me = this;
            if (!Common.HasValue(me.map) || !me.hasMap) return;

            pVector = pVector || 'MapPolygon';
            var feature = new ol.Feature({
                geometry: new ol.geom.MultiPolygon(pPoints), name: pTitle, code: pCode, data: pData, type: me._mapFeature.polygon
            });
            feature.setStyle(pStyle || me._mapStyle);
            feature.set("zStyle", pStyle || me._mapStyle);

            var flag = false;
            var vectorLayer = me.NewVector(pVector, Infinity);

            me.map.getLayers().forEach(function (o, i) {
                if (o.get('Name') == pVector) {
                    vectorLayer = o;
                    flag = true;
                }
            });
            vectorLayer.getSource().addFeature(feature);

            if (!flag)
                me.map.addControl(vectorLayer);

            return feature;
        }
        this.Distance = function (pPointFrom, pPointTo, pCallback, oCallback) {
            var me = this;
            try {
                var sF = Math.round(pPointFrom.Lng * 1000000) / 1000000 + "," + Math.round(pPointFrom.Lat * 1000000) / 1000000;
                var sT = Math.round(pPointTo.Lng * 1000000) / 1000000 + "," + Math.round(pPointTo.Lat * 1000000) / 1000000;
                me._toJson(apiUrl.viaroute + sF + ";" + sT).when({
                    ready: function (res) {
                        if (res.code !== "Ok") {
                            me._toJson(apiUrl.viamapbox + sF + ";" + sT + apiUrl.instructions).when({
                                ready: function (res) {
                                    if (res.code !== "Ok") {
                                        return;
                                    }
                                    if (Common.HasValue(pCallback)) {
                                        var obj = {
                                            Distance: res.routes[0].distance,
                                            Time: res.routes[0].duration,
                                            Data: oCallback
                                        }
                                        pCallback(pPointFrom, pPointTo, obj);
                                    }
                                },
                                retry: function () { }
                            })
                        }
                        else {
                            if (Common.HasValue(pCallback)) {
                                var obj = {
                                    Distance: res.routes[0].distance,
                                    Time: res.routes[0].duration,
                                    Data: oCallback
                                }
                                pCallback(pPointFrom, pPointTo, obj);
                            }
                        }
                    },
                    retry: function () {
                        me._toJson(apiUrl.viamapbox + sF + ";" + sT + apiUrl.instructions).when({
                            ready: function (res) {
                                if (res.code !== "Ok") {
                                    return;
                                }
                                if (Common.HasValue(pCallback)) {
                                    var obj = {
                                        Distance: res.routes[0].distance,
                                        Time: res.routes[0].duration,
                                        Data: oCallback
                                    }
                                    pCallback(pPointFrom, pPointTo, obj);
                                }
                            },
                            retry: function () { }
                        })
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
        this.FitBound = function (pData, pZoom, pSize) {
            var me = this;
            if (!Common.HasValue(me.map) || !me.hasMap) return;

            var flag = false;
            var ex = ol.extent.createEmpty();
            if (pData instanceof ol.layer.Vector) {
                try { ol.extent.extend(ex, pData.getSource().getExtent()); } catch (e) { }
            } else if (typeof (pData) === "string") {
                Common.Data.Each(me.map.getLayers().getArray(), function (o) {
                    if (o instanceof ol.layer.Vector && o.get('Name') == pData) {
                        try { ol.extent.extend(ex, o.getSource().getExtent()); } catch (e) { }
                    }
                })
            }
            else {
                flag = true;
                Common.Data.Each(pData, function (o) {
                    if (o instanceof ol.layer.Vector) {
                        if (Common.HasValue(o)) try { ol.extent.extend(ex, o.getSource().getExtent()); } catch (e) { }
                    } else if (o instanceof ol.Feature) {

                    }
                })
            }

            if (ex.length > 0 && ex.indexOf(Infinity) == -1) {
                pSize = pSize || 50;
                var size = me.map.getSize();
                var bounce = ol.animation.bounce({
                    resolution: me.map.getView().getResolution() * 2
                });
                var pan = ol.animation.pan({
                    source: me.map.getView().getCenter()
                });
                me.map.beforeRender(bounce);
                me.map.beforeRender(pan);
                me.map.getView().fit(ex, [size[0] - pSize, size[1] - pSize]);
                if (pData.length == 1 && flag) me.map.getView().setZoom(pZoom || 12);
            }
        }
        this.GetVector = function (pName) {
            var me = this;
            if (!Common.HasValue(me.map) || !me.hasMap) return;

            var vLayer = null;
            Common.Data.Each(me.map.getLayers().getArray(), function (o) {
                if (o instanceof ol.layer.Vector && o.get('Name') == pName) {
                    vLayer = o;
                }
            })
            return vLayer;
        }
        this.GetFeature = function (pCode, vName) {
            var me = this;
            if (!Common.HasValue(me.map) || !me.hasMap) return;

            var vFeature = null;
            Common.Data.Each(me.map.getLayers().getArray(), function (o) {
                if (o instanceof ol.layer.Vector && o.get('Name') == vName) {
                    o.getSource().forEachFeature(function (feature) {
                        if (feature.get('code') == pCode) vFeature = feature;
                    })
                }
            })
            return vFeature;
        }
        this.Visible = function (mapLayers, visible) {
            var me = this;
            if (!Common.HasValue(me.map) || !me.hasMap) return;

            if (!Common.HasValue(visible)) {
                if (Common.HasValue(mapLayers) && mapLayers instanceof ol.Feature) {
                    return mapLayers.get("zStyle") == mapLayers.getStyle();
                }
            }
            else {
                if (Common.HasValue(mapLayers)) {
                    if (mapLayers instanceof ol.layer.Vector) {
                        mapLayers.setVisible(visible);
                    } else if (mapLayers instanceof ol.Feature) {
                        if (visible) mapLayers.setStyle(mapLayers.get('zStyle'));
                        else mapLayers.setStyle(me._mapStyleHidden);
                    } else if (typeof (mapLayers) == 'string') {
                        Common.Data.Each(me.map.getLayers().getArray(), function (o) {
                            if (o instanceof ol.layer.Vector && o.get('Name') == mapLayers) {
                                o.getSource().forEachFeature(function (feature) {
                                    if (visible) feature.setStyle(feature.get('zStyle'));
                                    else feature.setStyle(me._mapStyleHidden);
                                })
                            }
                        })
                    }
                    else {
                        Common.Data.Each(mapLayers, function (o) {
                            if (o instanceof ol.Feature) {
                                if (visible) o.setStyle(o.get('zStyle'));
                                else o.setStyle(me._mapStyleHidden);
                            } else if (o instanceof ol.layer.Vector) {
                                o.setVisible(visible);
                            }
                        })
                    }
                    me.map.render();
                }
            }
        }
        this.VisibleVector = function (pName, visible) {
            var me = this;
            if (!Common.HasValue(me.map) || !me.hasMap) return;

            var flag = visible;
            Common.Data.Each(me.map.getLayers().getArray(), function (o) {
                if (o instanceof ol.layer.Vector && o.get('Name') == pName) {
                    if (visible != undefined && visible != null)
                        o.setVisible(visible);
                    else
                        flag = o.getVisible();
                }
            })
            return flag;
        }
        this.ClearMap = function () {
            var me = this;
            if (!Common.HasValue(me.map) || !me.hasMap) return;

            Common.Data.Each(me.map.getLayers().getArray(), function (o) {
                if (o instanceof ol.layer.Vector) {
                    o.getSource().clear();
                }
            })
        }
        this.ClearVector = function (pName) {
            var me = this;
            if (!Common.HasValue(me.map) || !me.hasMap) return;

            Common.Data.Each(me.map.getLayers().getArray(), function (o) {
                if (o instanceof ol.layer.Vector && o.get('Name') == pName) {
                    o.getSource().clear();
                }
            })
            me.map.render();
        }
        this.ClearFeature = function (pVector, pCode) {
            var me = this;
            if (!Common.HasValue(me.map) || !me.hasMap) return;

            Common.Data.Each(me.map.getLayers().getArray(), function (o) {
                if (o instanceof ol.layer.Vector && o.get('Name') == pVector) {
                    var f = null;
                    o.getSource().forEachFeature(function (feature) {
                        if (feature.get('code') == pCode) f = feature;
                    })
                    if (f != null) { o.getSource().removeFeature(f); }
                }
            })
        }
        this.Viewport = function () {
            if (!Common.HasValue(me.map) || !me.hasMap) return;
            return me.map.getViewport();
        }
        this.Resize = function () {
            var me = this;
            if (!Common.HasValue(me.map) || !me.hasMap) return;
            try {
                me.map.updateSize();
                me.map.render();
            } catch (e) { }
        }
        this.Style = function (mapLayers, pStyle) {
            var me = this;

            if (mapLayers instanceof ol.Feature) {
                if (Common.HasValue(pStyle)) {
                    mapLayers.setStyle(pStyle);
                } else {
                    return mapLayers.getStyle();
                }
            } else if (mapLayers instanceof ol.layer.Vector) {

            }
        }
        this.NewControl = function (pText, pTitle, pClassName, pClick) {
            var me = this;
            if (!Common.HasValue(me.map) || !me.hasMap) return;

            var button = document.createElement('button');
            button.innerHTML = pText; button.title = pTitle;
            if (Common.HasValue(pClick)) {
                button.addEventListener('click', function (e) {
                    var o = this;
                    setTimeout(function () {
                        pClick(e, o, me);
                    }, 1)
                }, false);
            }
            var element = document.createElement('div');
            element.className = pClassName + ' ol-unselectable ol-control';
            element.appendChild(button);
            me.map.addControl(new ol.control.Control({
                element: element
            }));
        }
        this.Active = function (pMap) {
            if (Common.HasValue(pMap)) {
                this.map = pMap;
            } else {
                return this.map;
            }
        }
        this.Center = function (pLat, pLng, pZoom) {
            var me = this;
            if (!Common.HasValue(me.map) || !me.hasMap) return;
            try {
                var bounce = ol.animation.bounce({
                    resolution: me.map.getView().getResolution() * 2
                });
                var pan = ol.animation.pan({
                    source: me.map.getView().getCenter()
                });
                me.map.beforeRender(bounce);
                me.map.beforeRender(pan);
                me.map.getView().setCenter(me.NewPoint(pLat, pLng));
                if (Common.HasValue(pZoom))
                    me.map.getView().setZoom(pZoom);
            } catch (e) {
            }
        }
        this.ZIndex = function (pVector, zIndex) {
            var me = this;
            if (!Common.HasValue(me.map) || !me.hasMap) return;

            if (Common.HasValue(zIndex)) {
                if (pVector instanceof ol.layer.Vector) {
                    pVector.setZIndex(zIndex);
                }
            }
            else {
                if (pVector instanceof ol.layer.Vector) {
                    return pVector.getStyle().zindex;
                }
            }
        }
        this.NewProvincePolygon = function (code, name, vect) {
            var myMap = this;
            if (!Common.HasValue(myMap.map) || !myMap.hasMap) return;
            var mapProvince = function (c, n, v) {
                this.Code = c;
                this.Name = n;
                this.Vect = v;

                this.Init = function (style, callback) {
                    var me = this;
                    myMap._toJson(apiUrl.geojson + myMap._mapProvince[me.Code] + apiUrl.suffix).when({
                        ready: function (res) {
                            var f = null;
                            if (res.type == 'Polygon') {
                                var data = [];
                                Common.Data.Each(res.coordinates[0], function (o) {
                                    data.push(myMap.NewPoint(o[1], o[0]));
                                })
                                f = myMap.NewPolygon([data], myMap._mapProvince[me.Code], "", style || myMap._mapStyle, null, me.Vect);
                            } else if (res.type == 'MultiPolygon') {
                                var data = [];
                                Common.Data.Each(res.coordinates, function (cs) {
                                    var x = [];
                                    Common.Data.Each(cs[0], function (o) { x.push(myMap.NewPoint(o[1], o[0])); })
                                    data.push(x);
                                })
                                f = myMap.NewMultiPolygon([data], myMap._mapProvince[me.Code], "", style || myMap._mapStyle, null, me.Vect);
                            }
                            if (Common.HasValue(callback))
                                callback(f);
                        }
                    })
                }
                this.Delete = function () {
                    var me = this;
                    Common.Data.Each(myMap.map.getLayers().getArray(), function (o) {
                        if (o instanceof ol.layer.Vector && o.get('Name') == me.Vect) {
                            o.getSource().clear();
                        }
                    })
                }
                this.Center = function () {
                    var me = this;

                    Common.Data.Each(myMap.map.getLayers().getArray(), function (o) {
                        if (o instanceof ol.layer.Vector && o.get('Name') == me.Vect) {
                            myMap.FitBound(o, 9);
                        }
                    })
                }
            }
            return new mapProvince(code, name, vect);
        }

        this.MarkerAnimate = function (feature, timeout, total, skip, max, color) {
            var me = this;
            if (!Common.HasValue(me.map) || !me.hasMap) return;

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
                        feature.setStyle(me.NewStyle.Circle(r, f, color, w));
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
                if (Common.HasValue(feature)) {
                    clearInterval(feature.get('intervalId'));
                    feature.set('markerAnimate', false);
                    feature.setStyle(feature.get('createStyle'));
                }
            }
            catch (e) {
                Common.Log(e.message);
            }
        }

        this.NewRouteDirection = function (code, feature, distance, vector) {
            var myMap = this;
            if (!Common.HasValue(myMap.map) || !myMap.hasMap) return;
            var routeDirection = function (c, f, d, v) {
                this.Code = c || "DirectionPoint";
                this.Vector = v;
                this.Feature = f;
                this.Distance = d;
                this.Marker = null;
                this.ListPoint = [];

                this.Init = function () {
                    var me = this;

                    if (me.Feature instanceof ol.Feature) {
                        var type = me.Feature.get('type');
                        if (type == myMap._mapFeature.polyline) {
                            var geo = me.Feature.getGeometry();
                            me.ListPoint.push(geo.getFirstCoordinate());
                            var length = Math.round(geo.getLength() / me.Distance);
                            for (var i = 1; i < length; i++) { me.ListPoint.push(geo.getCoordinateAt(i / length)); }
                            me.ListPoint.push(geo.getLastCoordinate());
                        } else {

                        }
                        var firstCoor = myMap._to4326(me.ListPoint[0]);
                        me.Marker = myMap.NewMarker(firstCoor[1], firstCoor[0], me.Code, "", myMap.NewStyle.Icon(Common.String.Format(myMap.NewImage.Truck), 1), null, me.Vector);
                        myMap.Visible(me.Marker, false);
                    }
                }
                this.Start = function (callback, p, dataPoint) {
                    var me = this;

                    var idx = 0;
                    myMap.Visible(me.Marker, true);
                    var myInterval = setInterval(function () {
                        me.Marker.getGeometry().setCoordinates(me.ListPoint[idx]);
                        idx++;
                        if (idx == me.ListPoint.length) {
                            clearInterval(myInterval);
                            myMap.Visible(me.Marker, false);
                            if (Common.HasValue(callback)) {
                                callback(p);
                            }
                            var pointend = dataPoint[dataPoint.length - 1].ListPoint[dataPoint[dataPoint.length - 1].ListPoint.length - 1];
                            var pointStart = dataPoint[0].ListPoint[0];
                            if (me.ListPoint[idx - 1][0] == pointend[0] && me.ListPoint[idx - 1][1] == pointend[1]) {
                                var firstCoor = myMap._to4326(pointStart);
                                myMap.NewMarker(firstCoor[1], firstCoor[0], "DirectionPoint", "", myMap.NewStyle.Icon(Common.String.Format(myMap.NewImage.Truck), 1), null, "VectorVehicleMove");
                            }
                        }
                    }, 100)
                }
                this.Stop = function () {
                    var me = this;
                    myMap.Visible(me.Marker, false);
                }
            }
            return new routeDirection(code, feature, distance, vector);
        }
    }

    return new openMap();
});