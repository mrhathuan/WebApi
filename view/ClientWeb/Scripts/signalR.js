/// <reference path="~/Scripts/jquery-1.9.1.intellisense.js" />
/// <reference path="~/Scripts/common.js" />
myapp.factory('signalR', function () {
    var connection = $.connection;

    var signalR = function (hub) {
        this.hub = hub;
        this.isConnected = false;
        if (this.hub != null)
            this.isConnected = true;

        this.addFunction = function (fnname, callback) {
            var me = this;

            if (me.hub != null && me.isConnected) {
                me.hub.off(fnname);
                me.hub.on(fnname, callback);
            }
        }
        this.connectSever = function () {
            var me = this;
            try {
                if (me.hub != null) {
                    me.isConnected = true;

                    me.addFunction('fnnope', function () { });
                    connection.start();
                    return true;
                }
            }
            catch (e) {
                return false;
            }
        }
        this.callServer = function (callback, param, proxy) {
            var me = this;

            if (me.hub != null && me.isConnected) {
                me.hub.sever.call(callback, param, proxy);
            }
        }
    }

    return new signalR($.connection.dataHub);
});