angular.module('myapp').factory('dataService', function ($http) {
	
    var dataService = function () {
	    //#region sys Index
	    this.App_ListFunctionMobile = function () {
	        var App_ListFunctionMobilePromise = new Promise(function (resolve, reject) {
	            Common.Services.Call($http, {
	                url: Common.Services.url.SYS,
	                method: "App_ListFunctionMobile",
	                data: {},
	                success: function (res) { resolve(res); }
	            })
	        });
	        return App_ListFunctionMobilePromise;
	    }

	    this.App_GetAuthorization = function () {
	        var App_GetAuthorizationPromise = new Promise(function (resolve, reject) {
	            Common.Services.Call($http, {
	                url: Common.Services.url.SYS,
	                method: "App_GetAuthorization",
	                data: {},
	                success: function (res) { resolve(res); }
	            })
	        });

	        return App_GetAuthorizationPromise;
	    }
	    //#endregion sys Index


	    //#region GPS

	    this.Extend_VehiclePosition_Add = function (vehicleCode, lat, lng, deviceDate) {
	        var Extend_VehiclePosition_AddPromise = new Promise(function (resolve, reject) {
	            Common.Services.Call($http, {
	                url: Common.Services.url.MOBI,
	                method: "Extend_VehiclePosition_Add",
	                data: {
	                    vehicleCode: vehicleCode,
	                    lat: lat,
	                    lng: lng,
	                    deviceDate: new Date(),
	                },
	                success: function (res) { resolve(res); }
	            })
	        });

	        return Extend_VehiclePosition_AddPromise;
	    }

	    this.MessageCall = function () {
	        var MessageCallPromise = new Promise(function (resolve, reject) {
	            Common.Services.Call($http, {
	                url: Common.Services.url.MOBI,
	                method: "MessageCall",
	                data: {},	               
	                success: function (res) { resolve(res); }
	            })
	        });
	        return MessageCallPromise;
	    }

        //endregion GPS

	    //#region time sheet
		this.FLMMobileScheduleOpen_List =function(){
			
			var FLMMobileScheduleOpen_ListPromise= new Promise(function(resolve, reject) {
				Common.Services.Call($http, {
				    url: Common.Services.url.MOBI,
				    method: "FLMMobileScheduleOpen_List",
				    data: {},
				    success: function (res) {resolve(res);}
				    })
			    });
			
			return FLMMobileScheduleOpen_ListPromise;	
		}
		
		this.FLMMobileScheduleAcceptList=function(){
			
			var FLMMobileScheduleAcceptListPromise = new Promise(function(resolve, reject){
				Common.Services.Call($http, {
					url: Common.Services.url.MOBI,
					method: "FLMMobileScheduleAccept_List",
					data: {},
					success: function (res) {resolve(res);}
					})
			});
			
			return FLMMobileScheduleAcceptListPromise;
		}

		
		this.FLMMobileRejectList=function(){
			
			var FLMMobileRejectListPromise = new Promise(function(resolve, reject){
				Common.Services.Call($http, {
					url: Common.Services.url.MOBI,
					method: "FLMMobileReject_List",
					data: {},
					success: function (res) {resolve(res);}
					})
			});
			
			return FLMMobileRejectListPromise;
		}
		
		
		this.FLMMobileScheduleGetList=function(){
			
			var FLMMobileScheduleGetListPromise = new Promise(function(resolve, reject){
				Common.Services.Call($http, {
					url: Common.Services.url.MOBI,
					method: "FLMMobileScheduleGet_List",
					data: {},
					success: function (res) {resolve(res);}
					})
			});
			
			return FLMMobileScheduleGetListPromise ;
		}
		
		this.FLMMobileReasonList=function(){
			
			var FLMMobileReasonListPromise = new Promise(function(resolve, reject){
				Common.Services.Call($http, {
					url: Common.Services.url.MOBI,
					method: "FLMMobileReason_List",
					data: {},
					success: function (res) {resolve(res);}
					})
			});
			
			return FLMMobileReasonListPromise ;
		}
		
		this.FLMMobileRomoocList=function(){
			
			var FLMMobileRomoocListPromise = new Promise(function(resolve, reject){
				Common.Services.Call($http, {
					url: Common.Services.url.MOBI,
					method: "FLMMobile_RomoocList",
					data: {},
					success: function (res) {resolve(res);}
					})
			});
			
			return FLMMobileRomoocListPromise ;
		}
		
		this.FLMMobileScheduleRunningList=function(){
			
			var FLMMobileScheduleRunningListPromise = new Promise(function(resolve, reject){
				Common.Services.Call($http, {
					url: Common.Services.url.MOBI,
					method: "FLMMobileScheduleRunning_List",
					data: {},
					success: function (res) {resolve(res);}
					})
			});
			
			return FLMMobileScheduleRunningListPromise ;
		}

		this.FLMMobileDriver_COStationlist = function (masterID) {
		    var FLMMobileDriver_COStationlistPromise = new Promise(function (resolve, reject) {
		        Common.Services.Call($http, {
		            url: Common.Services.url.MOBI,
		            method: "FLMMobileDriver_COStationlist",
		            data: { masterID :masterID},
		            success: function (res) { resolve(res); }
		        })
		    });
		    return FLMMobileDriver_COStationlistPromise;
		}

		this.FLMMobileStatus_Complete = function (masterID) {
		    var FLMMobileDriver_COStationlistPromise = new Promise(function (resolve, reject) {
		        Common.Services.Call($http, {
		            url: Common.Services.url.MOBI,
		            method: "FLMMobileStatus_Complete",
		            data: {
						 masterID :masterID},
		            success: function (res) { resolve(res); }
		        })
		    });
		    return FLMMobileDriver_COStationlistPromise;
		}

		this.FLMMobileDriver_Stationlist = function (masterID) {
		    var FLMMobileDriver_StationlistPromise = new Promise(function (resolve, reject) {
		        Common.Services.Call($http, {
		            url: Common.Services.url.MOBI,
		            method: "FLMMobileDriver_Stationlist",
		            data: { masterID: masterID },
		            success: function (res) { resolve(res); }
		        })
		    });
		    return FLMMobileDriver_StationlistPromise;
		}


		this.FLMMobileDriver_StationPass = function (masterID, stationID) {
		    var FLMMobileDriver_StationPassPromise = new Promise(function (resolve, reject) {
		        Common.Services.Call($http, {
		            url: Common.Services.url.MOBI,
		            method: "FLMMobileDriver_StationPass",
		            data: {
		                masterID: masterID,
		                stationID: stationID
		            },
		            success: function (res) { resolve(res); }
		        })
		    });
		    return FLMMobileDriver_StationPassPromise;
		}
        

		this.FLMMobileStatus_Save = function (timesheetID, timedriverID, masterID, locationID, temp,Lat,Lng) {
		    var FLMMobileStatus_SavePromise = new Promise(function (resolve, reject) {
		        Common.Services.Call($http, {
		            url: Common.Services.url.MOBI,
		            method: "FLMMobileStatus_Save",
		            data: {
		                timesheetID: timesheetID,
		                timedriverID: timedriverID,
		                masterID: masterID,
		                locationID: locationID,
                        temp:temp,
						Lat :Lat,
						Lng:Lng
		            },
		            success: function (res) { resolve(res); }
		        })
		    });
		    return FLMMobileStatus_SavePromise;
		}

		this.FLMMobileDriver_COStationPass = function (masterID, stationID) {
		    var FLMMobileDriver_COStationPassPromise = new Promise(function (resolve, reject) {
		        Common.Services.Call($http, {
		            url: Common.Services.url.MOBI,
		            method: "FLMMobileDriver_COStationPass",
		            data: {
		                masterID: masterID,
		                stationID: stationID
		                
		            },
		            success: function (res) { resolve(res); }
		        })
		    });
		    return FLMMobileDriver_COStationPassPromise;
		}

		this.FLMMobileStatus_COSave = function (timesheetID, masterID, locationID, romoocID, Lat, Lng) {
		    var FLMMobileStatus_COSavePromise = new Promise(function (resolve, reject) {
		        Common.Services.Call($http, {
		            url: Common.Services.url.MOBI,
		            method: "FLMMobileStatus_COSave",
		            data: {
		                timesheetID: timesheetID,
		                masterID: masterID,
		                locationID: locationID,
		                romoocID:romoocID,
						Lat: Lat,
						Lng:Lng
		            },
		            success: function (res) { resolve(res); }
		        })
		    });
		    return FLMMobileStatus_COSavePromise;
		}

		this.FLMMobileMaster_Run = function (timeID) {
		    var FLMMobileMaster_RunPromise = new Promise(function (resolve, reject) {
		        Common.Services.Call($http, {
		            url: Common.Services.url.MOBI,
		            method: "FLMMobileMaster_Run",
		            data: {
                        timeID:timeID
		            },
		            success: function (res) { resolve(res); }
		        })
		    });
		    return FLMMobileMaster_RunPromise;
		}

		this.FLMMobileMaster_Reject = function (timesheetID, timedriverID, reasonID, reasonNote) {
		    var FLMMobileMaster_RejectPromise = new Promise(function (resolve, reject) {
		        Common.Services.Call($http, {
		            url: Common.Services.url.MOBI,
		            method: "FLMMobileMaster_Reject",
		            data: {
		                timesheetID: timesheetID,
		                timedriverID: timedriverID,
		                reasonID: reasonID,
		                reasonNote:reasonNote
		            },
		            success: function (res) { resolve(res); }
		        })
		    });
		    return FLMMobileMaster_RejectPromise;
		}

		this.FLMMobileMaster_Accept = function (timesheetID, timedriverID) {
		    var FLMMobileMaster_AcceptPromise = new Promise(function (resolve, reject) {
		        Common.Services.Call($http, {
		            url: Common.Services.url.MOBI,
		            method: "FLMMobileMaster_Accept",
		            data: {
		                timesheetID: timesheetID,
		                timedriverID: timedriverID
		            },
		            success: function (res) { resolve(res); }
		        })
		    });
		    return FLMMobileMaster_AcceptPromise;
		}

		this.FLMMobileMaster_ReAccept = function (timesheetID) {
		    var FLMMobileMaster_ReAcceptPromise = new Promise(function (resolve, reject) {
		        Common.Services.Call($http, {
		            url: Common.Services.url.MOBI,
		            method: "FLMMobileMaster_ReAccept",
		            data: {
		                timesheetID: timesheetID
		            },
		            success: function (res) { resolve(res); }
		        })
		    });
		    return FLMMobileMaster_ReAcceptPromise;
		}

		this.FLMMobile_SOList = function (masterID, locationID) {
		    var FLMMobile_SOListPromise = new Promise(function (resolve, reject) {
		        Common.Services.Call($http, {
		            url: Common.Services.url.MOBI,
		            method: "FLMMobile_SOList",
		            data: {
		                masterID: masterID,
		                locationID: locationID
		            },
		            success: function (res) { resolve(res); }
		        })
		    });
		    return FLMMobile_SOListPromise;
		}


		this.FLMMobileDriver_FileList = function (id, code) {
		    var FLMMobileDriver_FileListPromise = new Promise(function (resolve, reject) {
		        Common.Services.Call($http, {
		            url: Common.Services.url.MOBI,
		            method: "FLMMobileDriver_FileList",
		            data: {
		                id: id,
                        code:code
		            },
		            success: function (res) { resolve(res); }
		        })
		    });
		    return FLMMobileDriver_FileListPromise;
		}

		this.Reject = function (timeSheetDriverID) {
		    var RejectPromise = new Promise(function (resolve, reject) {
		        Common.Services.Call($http, {
		            url: Common.Services.url.MOBI,
		            method: "Reject",
		            data: {
		                timeSheetDriverID:timeSheetDriverID
		            },
		            success: function (res) { resolve(res); }
		        })
		    });
		    return RejectPromise;
		}

		this.Mobile_GOPReturnList = function (masterID, locationID) {
		    var Mobile_GOPReturnListPromise = new Promise(function (resolve, reject) {
		        Common.Services.Call($http, {
		            url: Common.Services.url.MOBI,
		            method: "Mobile_GOPReturnList",
		            data: {
		                masterID: masterID,
                        locationID:locationID
		            },
		            success: function (res) { resolve(res); }
		        })
		    });
		    return Mobile_GOPReturnListPromise;
		}

		this.Mobile_DITOGroupProductList = function (masterID, locationID) {
		    var Mobile_DITOGroupProductListPromise = new Promise(function (resolve, reject) {
		        Common.Services.Call($http, {
		            url: Common.Services.url.MOBI,
		            method: "Mobile_DITOGroupProductList",
		            data: {
		                masterID: masterID,
		                locationID: locationID
		            },
		            success: function (res) { resolve(res); }
		        })
		    });
		    return Mobile_DITOGroupProductListPromise;
		}
		
		this.Mobile_CUSGOPList = function (masterID) {
		    var Mobile_CUSGOPListPromise = new Promise(function (resolve, reject) {
		        Common.Services.Call($http, {
		            url: Common.Services.url.MOBI,
		            method: "Mobile_CUSGOPList",
		            data: {
		                masterID: masterID	                
		            },
		            success: function (res) { resolve(res); }
		        })
		    });
		    return Mobile_CUSGOPListPromise;
		}

		this.Mobile_CUSProductList = function (masterID) {
		    var Mobile_CUSProductListPromise = new Promise(function (resolve, reject) {
		        Common.Services.Call($http, {
		            url: Common.Services.url.MOBI,
		            method: "Mobile_CUSProductList",
		            data: {
		                masterID: masterID
		            },
		            success: function (res) { resolve(res); }
		        })
		    });
		    return Mobile_CUSProductListPromise;
		}

		this.Mobile_GOPReturnSave = function (item) {
		    var Mobile_GOPReturnSavePromise = new Promise(function (resolve, reject) {
		        Common.Services.Call($http, {
		            url: Common.Services.url.MOBI,
		            method: "Mobile_GOPReturnSave",
		            data: {
		                item: item
		            },
		            success: function (res) { resolve(res); }
		        })
		    });
		    return Mobile_GOPReturnSavePromise;
		}

		this.Mobile_GOPReturnEdit = function (id, quantity) {
		    var Mobile_GOPReturnEditPromise = new Promise(function (resolve, reject) {
		        Common.Services.Call($http, {
		            url: Common.Services.url.MOBI,
		            method: "Mobile_GOPReturnEdit",
		            data: {
		                id: id,
                        quantity:quantity
		            },
		            success: function (res) { resolve(res); }
		        })
		    });
		    return Mobile_GOPReturnEditPromise;
		}

		this.FLMMobileDriverStationPassed = function (masterID, strMethod) {
		    var FLMMobileDriverStationPassedPromise = new Promise(function (resolve, reject) {
		        Common.Services.Call($http, {
		            url: Common.Services.url.MOBI,
		            method: strMethod,
		            data: {
		                masterID: masterID
		            },
		            success: function (res) {
		                resolve(res);
		            }
		        })
		    });
		    return FLMMobileDriverStationPassedPromise;
		}

		this.FLMMobileDriver_TroubleList = function (masterID, isCO) {
		    var FLMMobileDriver_TroubleListPromise = new Promise(function (resolve, reject) {
		        Common.Services.Call($http, {
		            url: Common.Services.url.MOBI,
		            method: "FLMMobileDriver_TroubleList",
		            data: {
		                masterID: masterID,
		                isCO: isCO
		            },
		            success: function (res) {
		                resolve(res);
		            }
		        })
		    });
		    return FLMMobileDriver_TroubleListPromise;
		}

		this.FLMMobile_GroupTroubleList = function (isCO) {
		    var FLMMobile_GroupTroubleListPromise = new Promise(function (resolve, reject) {
		        Common.Services.Call($http, {
		            url: Common.Services.url.MOBI,
		            method: "FLMMobile_GroupTroubleList",
		            data: {	               
		                isCo: isCO
		            },
		            success: function (res) {
		                resolve(res);
		            }
		        })
		    });
		    return FLMMobile_GroupTroubleListPromise;
		}

		this.FLMMobile_TroubleSave = function (item) {
		    var FLMMobile_TroubleSavePromise = new Promise(function (resolve, reject) {
		        Common.Services.Call($http, {
		            url: Common.Services.url.MOBI,
		            method: "FLMMobile_TroubleSave",
		            data: {
		                item: item
		            },
		            success: function (res) {
		                resolve(res);
		            }
		        })
		    });
		    return FLMMobile_TroubleSavePromise;
		}

		this.FLMMobileDriverHistory_List = function (driverID, dtfrom, dtto) {
		    var FLMMobileDriverHistory_ListPromise = new Promise(function (resolve, reject) {
		        Common.Services.Call($http, {
		            url: Common.Services.url.MOBI,
		            method: "FLMMobileDriverHistory_List",
		            data: {
		                driverID: driverID,
		                dtfrom: dtfrom,
		                dtto: dtto
		            },
		            success: function (res) {
		                resolve(res);
		            }
		        })
		    });
		    return FLMMobileDriverHistory_ListPromise;
		}

		this.FLMMobileDriverSalary_List = function (dtfrom, dtto) {
		    var FLMMobileDriverSalary_ListPromise = new Promise(function (resolve, reject) {
		        Common.Services.Call($http, {
		            url: Common.Services.url.MOBI,
		            method: "FLMMobileDriverSalary_List",
		            data: {
		                dtfrom: dtfrom,
		                dtto: dtto
		            },
		            success: function (res) {
		                resolve(res);
		            }
		        })
		    });
		    return FLMMobileDriverSalary_ListPromise;
		}

		this.FLMMobileDriver_ProblemTypeList = function () {
		    var FLMMobileDriver_ProblemTypeListPromise = new Promise(function (resolve, reject) {
		        Common.Services.Call($http, {
		            url: Common.Services.url.MOBI,
		            method: "FLMMobileDriver_ProblemTypeList",
		            data: {
		            },
		            success: function (res) {
		                resolve(res);
		            }
		        })
		    });
		    return FLMMobileDriver_ProblemTypeListPromise;
		}

		this.ProblemList = function () {
		    var ProblemListPromise = new Promise(function (resolve, reject) {
		        Common.Services.Call($http, {
		            url: Common.Services.url.MOBI,
		            method: "ProblemList",
		            data: {
		            },
		            success: function (res) {
		                resolve(res);
		            }
		        })
		    });
		    return ProblemListPromise;
		}


		this.FLMMobileDriver_ProblemSave = function (item) {
		    var FLMMobileDriver_ProblemSavePromise = new Promise(function (resolve, reject) {
		        Common.Services.Call($http, {
		            url: Common.Services.url.MOBI,
		            method: "FLMMobileDriver_ProblemSave",
		            data: {
                        item:item
		            },
		            success: function (res) {
		                resolve(res);
		            }
		        })
		    });
		    return FLMMobileDriver_ProblemSavePromise;
		}

		this.FLMMobile_SummarySOList = function (timesheetID) {
		    var FLMMobile_SummarySOListPromise = new Promise(function (resolve, reject) {
		        Common.Services.Call($http, {
		            url: Common.Services.url.MOBI,
		            method: "FLMMobile_SummarySOList",
		            data: {
		                timesheetID: timesheetID
		            },
		            success: function (res) {
		                resolve(res);
		            }
		        })
		    });
		    return FLMMobile_SummarySOListPromise;
		}

		this.FLMMobile_COList = function (masterID) {
		    var FLMMobile_COListPromise = new Promise(function (resolve, reject) {
		        Common.Services.Call($http, {
		            url: Common.Services.url.MOBI,
		            method: "FLMMobile_COList",
		            data: {
		                masterID: masterID
		            },
		            success: function (res) {
		                resolve(res);
		            }
		        })
		    });
		    return FLMMobile_COListPromise;
		}

		this.GetDriverInfo = function (id) {
		    var GetDriverInfoPromise = new Promise(function (resolve, reject) {
		        Common.Services.Call($http, {
		            url: Common.Services.url.MOBI,
		            method: "GetDriverInfo",
		            data: {
		                id: id
		            },
		            success: function (res) {
		                resolve(res);
		            }
		        })
		    });
		    return GetDriverInfoPromise;
		}

		this.FLMMobileSchedule_Get = function (timeSheetDriverID) {
		    var FLMMobileSchedule_GetPromise = new Promise(function (resolve, reject) {
		        Common.Services.Call($http, {
		            url: Common.Services.url.MOBI,
		            method: "FLMMobileSchedule_Get",
		            data: {
		                timeSheetDriverID: timeSheetDriverID
		            },
		            success: function (res) {
		                resolve(res);
		            }
		        })
		    });
		    return FLMMobileSchedule_GetPromise;
		}


		this.Mobile_SL_Save = function (item) {
		    var Mobile_SL_SavePromise = new Promise(function (resolve, reject) {
		        Common.Services.Call($http, {
		            url: Common.Services.url.MOBI,
		            method: "Mobile_SL_Save",
		            data: {
		                item: item
		            },
		            success: function (res) {
		                resolve(res);
		            }
		        })
		    });
		    return Mobile_SL_SavePromise;
		}

		this.Mobile_AddGroupProductFromDN = function (opsGroupID,item) {
		    var Mobile_AddGroupProductFromDNPromise = new Promise(function (resolve, reject) {
		        Common.Services.Call($http, {
		            url: Common.Services.url.MOBI,
		            method: "Mobile_AddGroupProductFromDN",
		            data: {
		                opsGroupID:opsGroupID,
		                item: item
		            },
		            success: function (res) {
		                resolve(res);
		            }
		        })
		    });
		    return Mobile_AddGroupProductFromDNPromise;
		}

		this.Mobile_GroupProductOfTOGroup = function (TOGroupID) {
		    var Mobile_GroupProductOfTOGroupPromise = new Promise(function (resolve, reject) {
		        Common.Services.Call($http, {
		            url: Common.Services.url.MOBI,
		            method: "Mobile_GroupProductOfTOGroup",
		            data: {
		                TOGroupID: TOGroupID
		            },
		            success: function (res) {
		                resolve(res);
		            }
		        })
		    });
		    return Mobile_GroupProductOfTOGroupPromise;
		}


	} 
	
	return new dataService();
});