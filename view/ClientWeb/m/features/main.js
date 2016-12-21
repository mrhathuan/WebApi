
angular.module('myapp').controller('mainController', function ($rootScope, $scope, $state, $location, $http, $timeout, $ionicLoading) {
	Common.Log('mainController');
    
	$scope.TransferClick = function (e, item) {
		var str = '';
        if (item.URL == 'driver') {
			str = 'driver.truck';			
            $rootScope.MenuList = [
                { Href: 'driver.truck', Src: 'img/ipack.png', Text: 'ĐƠN HÀNG' }, 
                { Href: 'driver.summary', Src: 'img/isummary.png', Text: 'THỐNG KÊ' },
                { Href: 'driver.problem', Src: 'img/isummary.png', Text: 'SỰ CỐ' },
                { Href: 'driver.info', Src: 'img/iaccount.png', Text: 'TÀI KHOẢN' }
            ]
        }
        if (item.URL == 'vendor') {
			str = 'vendor';
            $rootScope.MenuList = [
                { Href: 'vendor.home', Src: 'img/ipack.png', Text: 'ĐƠN HÀNG' },
                { Href: 'vendor.info', Src: 'img/iaccount.png', Text: 'TÀI KHOẢN' }
            ]
        }
        if (item.URL == 'manager') {
            str = 'manage';
        }
        $state.go(str);
    };
	
	var itemTranfer = null;
	for (var i = 0; i < $rootScope.ViewTransfer.length; i++) {
		var v = $rootScope.ViewTransfer[i];
		if(v.IsUsed == true && itemTranfer == null){
			itemTranfer = v;
		}
		else if(v.IsUsed == true && itemTranfer != null){
			itemTranfer = null;
			break;
		}
	}
	if(itemTranfer != null){
		$scope.TransferClick(null, itemTranfer);
	}
});