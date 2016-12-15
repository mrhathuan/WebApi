var featuresV = '?v=0.1';
var featuresA = '';
var features = [];

features.push({ name: 'driver', url: '^/driver', templateUrl: featuresA + 'features/driver/index.html' + featuresV, controller: 'driverController', cache: false });
features.push({ name: 'driver.truck', url: '^/driver/truck', templateUrl: featuresA + 'features/driver/truck.html' + featuresV, controller: 'driver_truckController', cache: false });
features.push({ name: 'driver.truck_detail', url: '^/driver_truck_detail/{masterID}&{locationID}&{statusID}&{sheetDriverID}&{sheetID}', templateUrl: featuresA + 'features/driver/truck_detail.html' + featuresV, controller: 'driver_truckDetailController', cache: false });
features.push({ name: 'driver.container_detail', url: '^/driver_container_detail/{masterID}', templateUrl: featuresA + 'features/driver/container_detail.html' + featuresV, controller: 'driver_containerDetailController', cache: false });
features.push({ name: 'driver.truck_trouble', url: '^/driver_truck_trouble/:masterID/:type', templateUrl: featuresA + 'features/driver/truck_trouble.html' + featuresV, controller: 'driver_trucktroubleController', cache: false });
features.push({ name: 'driver.truck_station', url: '^/driver_truck_station/:masterID/:type', templateUrl: featuresA + 'features/driver/truck_station.html' + featuresV, controller: 'driver_truckstationController', cache: false });
features.push({ name: 'driver.info', url: '^/driver/driver_info', templateUrl: featuresA + 'features/driver/info.html' + featuresV, controller: 'driver_infoController', cache: false });
features.push({ name: 'driver.summary', url: '^/driver_summary', templateUrl: featuresA + 'features/driver/summary.html' + featuresV, controller: 'driver_summaryController', cache: false });
features.push({ name: 'driver.summary_detail', url: '^/summary_detail/:timesheetID', templateUrl: featuresA + 'features/driver/summary_detail.html' + featuresV, controller: 'driver_summaryDetailController', cache: false });
features.push({ name: 'driver.summary_master', url: '^/summary_master/:timeSheetDriverID', templateUrl: featuresA + 'features/driver/summary_master.html' + featuresV, controller: 'driver_summaryMasterController', cache: false });
features.push({ name: 'driver.problem', url: '^/driver_problem', templateUrl: featuresA + 'features/driver/problem.html' + featuresV, controller: 'driver_problemController', cache: false });


features.push({ name: 'vendor', url: '^/vendor', templateUrl: featuresA + 'features/vendor/index.html' + featuresV, controller: 'vendorController', cache: false });
features.push({ name: 'vendor.list', url: '^/vendor/list', templateUrl: featuresA + 'features/vendor/list_vendor.html' + featuresV, controller: 'list_vendorController', cache: false });
features.push({ name: 'vendor.home', url: '^/vendor/home', templateUrl: featuresA + 'features/vendor/home.html' + featuresV, controller: 'vendor_homeController', cache: false });
features.push({ name: 'vendor.home_detail', url: '^/vendor/detail/{id}&{venid}', templateUrl: featuresA + 'features/vendor/home_detail.html' + featuresV, controller: 'vendor_homeDetailController', cache: false });
features.push({ name: 'vendor.home_acceptDetail', url: '^/vendor/accept/{id}', templateUrl: featuresA + 'features/vendor/home_acceptDetail.html' + featuresV, controller: 'vendor_homeAcceptDetailController', cache: false });
features.push({ name: 'vendor.home_SODetail', url: '^/vendor/SO/{masterID}&{locationID}&{statusID}', templateUrl: featuresA + 'features/vendor/home_SODetail.html' + featuresV, controller: 'vendor_homeSODetailController', cache: false });
features.push({ name: 'vendor.home_trouble', url: '^/vendor/trouble/{masterID}', templateUrl: featuresA + 'features/vendor/home_trouble.html' + featuresV, controller: 'vendor_homeTroubleController', cache: false });
features.push({ name: 'vendor.info', url: '^/vendor/info', templateUrl: featuresA + 'features/vendor/info.html' + featuresV, controller: 'vendor_infoCtrl', cache: false });

features.push({ name: 'manage', url: '^/manage', templateUrl: featuresA + 'features/manager/index.html' + featuresV, controller: 'manageController', cache: false });
features.push({ name: 'manage.dashboard', url: '^/manage/dashboard', templateUrl: featuresA + 'features/manager/dashboard.html' + featuresV, controller: 'manage_dashboardController', cache: false });
features.push({ name: 'manage.order', url: '^/manage/order', templateUrl: featuresA + 'features/manager/order.html' + featuresV, controller: 'manage_orderController', cache: false });
features.push({ name: 'manage.ops', url: '^/manage/ops', templateUrl: featuresA + 'features/manager/ops.html' + featuresV, controller: 'manage_opsController', cache: false });
features.push({ name: 'manage.cost', url: '^/manage/cost', templateUrl: featuresA + 'features/manager/cost.html' + featuresV, controller: 'manage_costController', cache: false });
features.push({ name: 'manage.finance', url: '^/manage/finance', templateUrl: featuresA + 'features/manager/finance.html' + featuresV, controller: 'manage_financeController', cache: false });
features.push({ name: 'manage.charts', url: '^/manage/charts', templateUrl: featuresA + 'features/manager/charts.html' + featuresV, controller: 'manage_chartsController', cache: false });
