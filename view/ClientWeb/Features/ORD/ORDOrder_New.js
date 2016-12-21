/// <reference path="~/Scripts/jquery-1.9.1.intellisense.js" />
/// <reference path="~/Scripts/kendo/2015.1.429/kendo.all-vsdoc.js" />
/// <reference path="~/Scripts/common.js" />

//#region URL
var _ORDOrder_New = {
    URL: {
        GetView: 'ORDOrder_GetViewFromCAT',
        Customer_Read: 'ORDOrder_CustomerList',
        Contract_Read: 'ORDOrder_Contract_List',
        ContractTemp_Read: 'ORDOrder_ContractTemp_List',
        Transport_List: 'ORDOrder_TransportMode_List'
    },
    Data: {

    }
}

//#endregion

angular.module('myapp').controller('ORDOrder_NewCtrl', ['$rootScope', '$scope', '$http', '$location', '$state', '$timeout', function ($rootScope, $scope, $http, $location, $state, $timeout) {
    if ($rootScope.IsPageComplete != true) return;
    Common.Log('ORDOrder_NewCtrl');
    $rootScope.IsLoading = false;

    if (!$rootScope.CheckView("ActAdd", "main.ORDOrder.Index")) return;

    var AuthItem = Common.Auth.Item;

    $scope.IsShowService = false;

    $scope.ItemFromCookie = "";

    $scope.ItemParam = {
        OrderID: -1,
        CustomerID: "",
        ServiceID: -1,
        TransportID: "",
        ContractID: -1,
        TermID: -1,
        IsExpired: true
    };

    var cookie = Common.Cookie.Get("ORDOrder_New_LastChoose" + AuthItem.SYSCustomerID + "_" + AuthItem.UserID);
    if (!Common.HasValue(cookie) || cookie == '') {
        var val = JSON.stringify($scope.ItemParam)
        Common.Cookie.Set("ORDOrder_New_LastChoose" + AuthItem.SYSCustomerID + "_" + AuthItem.UserID, val)
    }
    else {
        $scope.ItemFromCookie = JSON.parse(cookie);
    }
    Common.Log("Load Cookie:" + cookie)

    //#region View
    $scope.cboCustomerOptions = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true,
        dataTextField: 'CustomerName', dataValueField: 'ID', minLength: 3,
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    CustomerName: { type: 'string' },
                }
            }
        }),
        change: function (e) {
            $scope.ItemParam.ContractID = -1;
            $scope.ItemParam.TermID = -1;
            $scope.LoadDataContract(false);
        }
    };

    Common.Services.Call($http, {
        url: Common.Services.url.ORD,
        method: _ORDOrder_New.URL.Customer_Read,
        success: function (res) {
            var data = res.Data;
            $timeout(function () {
                $scope.cboCustomerOptions.dataSource.data(data);
                if (!Common.HasValue($scope.ItemFromCookie.CustomerID) || $scope.ItemFromCookie.CustomerID == "")
                    $scope.ItemParam.CustomerID = data[0].ID;
                else $scope.ItemParam.CustomerID = $scope.ItemFromCookie.CustomerID;
                
                $scope.LoadDataContract(true);
            }, 1);

        }
    });

    $scope.cboTransportModeOptions = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true,
        dataTextField: 'Name', dataValueField: 'ID', minLength: 3,
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
            var obj = this.dataItem(this.select());
            $scope.ItemFromCookie = "";
            if (Common.HasValue(obj)) {
                $scope.IsShowService = !obj.IsTruck;
                $scope.LoadDataContract(false);
            }
            else {
                $scope.IsShowService = false;
                $scope.ItemParam.ServiceID = -1;
                $scope.LoadDataContract(false);
            }
        }
    };

    Common.Services.Call($http, {
        url: Common.Services.url.ORD,
        method: _ORDOrder_New.URL.Transport_List,
        data: {},
        success: function (res) {
            $scope.cboTransportModeOptions.dataSource.data(res);
            if (Common.HasValue($scope.ItemFromCookie.TransportID) && $scope.ItemFromCookie.TransportID != "") {
                $scope.ItemParam.TransportID = $scope.ItemFromCookie.TransportID;
                var obj = {};

                angular.forEach(res, function (o,i) {
                    if (o.ID == $scope.ItemParam.TransportID) {
                        obj = o;
                    }
                })

                if (Common.HasValue(obj) && obj != '') {
                    $scope.IsShowService = !obj.IsTruck;
                }
            }
            else if (res.length > 0) {
                var o = res[0];
                $scope.IsShowService = !o.IsTruck;
                $scope.ItemParam.TransportID = o.ID;
            }
            else {

            }
        }
    });

    $scope.cboServiceOptions = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true,
        dataTextField: 'ValueOfVar', dataValueField: 'ID', minLength: 3,index:0,
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    ValueOfVar: { type: 'string' },
                }
            }
        }),
        change: function (e) {
            $scope.ItemFromCookie = "";
            $scope.LoadDataContract(false);
        }
    };

    Common.ALL.Get($http, {
        url: Common.ALL.URL.SYSVarServiceOfOrder,
        success: function (res) {
            $scope.cboServiceOptions.dataSource.data(res);
            if (Common.HasValue($scope.ItemFromCookie.ServiceID) && $scope.ItemFromCookie.ServiceID != "")
                $scope.ItemParam.ServiceID = $scope.ItemFromCookie.ServiceID;
            else if (res.length > 0) {
                var o = res[0];
                $scope.ItemParam.ServiceID = o.ID;
            }
        }
    })

    $scope.cboContractOptions = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true,
        dataTextField: 'DisplayName', dataValueField: 'ID', minLength: 3,
        dataSource: Common.DataSource.Local({
            data: [],
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    DisplayName: { type: 'string' },
                }
            }
        }),
        change: function (e) {
            $scope.LoadDataContractTemp(false);
        }
    };

    $scope.cboContractTempOptions = {
        autoBind: true, valuePrimitive: true, ignoreCase: true, filter: 'contains', suggest: true,
        dataTextField: 'DisplayName', dataValueField: 'ID', minLength: 3,
        dataSource: Common.DataSource.Local({
            data: [{ ID: -1, DisplayName: '' }],
            model: {
                id: 'ID',
                fields: {
                    ID: { type: 'number' },
                    DisplayName: { type: 'string' },
                }
            }
        }),
        change: function (e) {
        }
    };

    $scope.LoadDataContract = function (flag) {
        if ($scope.ItemParam.CustomerID > 0 && $scope.ItemParam.TransportID > 0) {
            Common.Log("LoadDataContract")
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.ORD,
                method: _ORDOrder_New.URL.Contract_Read,
                data: $scope.ItemParam,
                success: function (res) {
                    var data = res.Data;
                    $rootScope.IsLoading = false;
                    $timeout(function () {
                        $scope.cboContractOptions.dataSource.data(data);
                        if (flag) {
                            if (!Common.HasValue($scope.ItemFromCookie.ContractID) || $scope.ItemFromCookie.ContractID == "")
                                $scope.ItemParam.ContractID = data[0].ID;
                            else $scope.ItemParam.ContractID = $scope.ItemFromCookie.ContractID;
                        }
                        else {
                            $scope.ItemParam.ContractID = -1;
                        }
                        $scope.LoadDataContractTemp(flag);
                    }, 1);
                }
            });
        } else {
            $scope.cboContract.dataSource.data([{ ID: -1, DisplayName: '' }]);
            $scope.ItemParam.ContractID = -1;
            $scope.cboContractTemp.dataSource.data([{ ID: -1, DisplayName: '' }]);
            $scope.ItemParam.TermID = -1;
        }
    }

    $scope.LoadDataContractTemp = function (flag) {
        if ($scope.ItemParam.ContractID > 0) {
            Common.Log("LoadDataContractTemp")
            $rootScope.IsLoading = true;
            Common.Services.Call($http, {
                url: Common.Services.url.ORD,
                method: _ORDOrder_New.URL.ContractTemp_Read,
                data: $scope.ItemParam,
                success: function (res) {
                    var data = res.Data;
                    $rootScope.IsLoading = false;
                    $timeout(function () {
                        $scope.cboContractTempOptions.dataSource.data(data);
                        if (flag) {
                            if (!Common.HasValue($scope.ItemFromCookie.TermID) || $scope.ItemFromCookie.TermID == "")
                                $scope.ItemParam.TermID = data[0].ID;
                            else $scope.ItemParam.TermID = $scope.ItemFromCookie.TermID;
                        }
                        else {
                            $scope.ItemParam.TermID = -1;
                        }
                    }, 1);
                }
            });
        } else {
            $scope.cboContractTempOptions.dataSource.data([{ ID: -1, DisplayName: '' }]);
            $scope.ItemParam.TermID = -1;
        }
    }

    //#endregion

    //#region Action
    $scope.ORDNew_Click = function ($event, vform) {
        $event.preventDefault();
        if ($scope.ItemParam.TermID == "")
            $scope.ItemParam.TermID == -1;
        if (vform()) {
            Common.Services.Call($http, {
                url: Common.Services.url.ORD,
                method: _ORDOrder_New.URL.GetView,
                data: $scope.ItemParam,
                success: function (res) {
                    var view = '';
                    var flag = false;
                    switch (res) {
                        case 0:
                            flag = true;
                            view = "main.ORDOrder.Index"
                            break;
                        case 1:
                            flag = true;
                            view = "main.ORDOrder.FCLIMEX"
                            break;
                        case 2:
                            flag = true;
                            view = "main.ORDOrder.FCLLO"
                            break;
                        case 3:
                            flag = true;
                            view = "main.ORDOrder.FTLLO"
                            break;
                        case 4:
                            flag = true;
                            view = "main.ORDOrder.LTLLO"
                            break;
                        case 5:
                            flag = true;
                            view = "main.ORDOrder.FCLLOEmpty"
                            break;
                        case 6:
                            flag = true;
                            view = "main.ORDOrder.FCLLOLaden"
                            break;
                        case 7:
                            break;
                        case 8:
                            break;
                        case 9:
                            flag = true;
                            view = "main.ORDOrder.LCLIMEX"
                            break;
                        default:
                            break;
                    }
                    if (flag) {
                        Common.Cookie.Clear("ORDOrder_New_LastChoose" + AuthItem.SYSCustomerID + "_" + AuthItem.UserID);
                        var val = JSON.stringify($scope.ItemParam)
                        Common.Cookie.Set("ORDOrder_New_LastChoose" + AuthItem.SYSCustomerID + "_" + AuthItem.UserID, val)
                        //Common.Log("Set Cookie: " + val)
                        $state.go(view, $scope.ItemParam);
                    }

                }
            });
        }

    }

    //#endregion

}]);