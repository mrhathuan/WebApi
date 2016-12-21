using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Security;
using DTO;
using CacheManager.Core;
using System.Web;
using Microsoft.Practices.Unity;
using Newtonsoft.Json;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using Presentation;
using Microsoft.SqlServer;
using System.IO;
using OfficeOpenXml;
using IServices;
using System.ServiceModel;
using System.Drawing;
using OfficeOpenXml.Style;
using System.Xml.Linq;

namespace ClientWeb
{
    public class CATController : BaseController
    {
        #region ALL Data
        public DTOResult ALL_Country()
        {
            try
            {
                var result = default(DTOResult);
                ServiceFactory.SVCategory((ISVCategory sv) =>
                {
                    result = sv.ALL_Country();
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DTOResult ALL_District()
        {
            try
            {
                var result = default(DTOResult);
                ServiceFactory.SVCategory((ISVCategory sv) =>
                {
                    result = sv.ALL_District();
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DTOResult ALL_Province()
        {
            try
            {
                var result = default(DTOResult);
                ServiceFactory.SVCategory((ISVCategory sv) =>
                {
                    result = sv.ALL_Province();
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DTOResult ALL_Ward()
        {
            try
            {
                var result = default(DTOResult);
                ServiceFactory.SVCategory((ISVCategory sv) =>
                {
                    result = sv.ALL_Ward();
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DTOResult ALL_Customer()
        {
            try
            {
                var result = default(DTOResult);
                ServiceFactory.SVCategory((ISVCategory sv) =>
                {
                    result = sv.ALL_Customer();
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DTOResult ALL_CustomerInUser()
        {
            try
            {
                var result = default(DTOResult);
                ServiceFactory.SVCategory((ISVCategory sv) =>
                {
                    result = sv.ALL_CustomerInUser();
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DTOResult ALL_Vendor()
        {
            try
            {
                var result = default(DTOResult);
                ServiceFactory.SVCategory((ISVCategory sv) =>
                {
                    result = sv.ALL_Vendor();
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DTOResult ALL_VendorInUser()
        {
            try
            {
                var result = default(DTOResult);
                ServiceFactory.SVCategory((ISVCategory sv) =>
                {
                    result = sv.ALL_VendorInUser();
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DTOResult ALL_GroupOfVehicle()
        {
            try
            {
                var result = default(DTOResult);
                ServiceFactory.SVCategory((ISVCategory sv) =>
                {
                    result = sv.ALL_GroupOfVehicle();
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DTOResult ALL_Service()
        {
            try
            {
                var result = default(DTOResult);
                ServiceFactory.SVCategory((ISVCategory sv) =>
                {
                    result = sv.ALL_Service();
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DTOResult ALL_SYSVarServiceOfOrder()
        {
            try
            {
                var result = default(DTOResult);
                ServiceFactory.SVCategory((ISVCategory sv) =>
                {
                    result = sv.ALL_CATServiceOfOrder();
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DTOResult ALL_SYSVarTransportMode()
        {
            try
            {
                var result = default(DTOResult);
                ServiceFactory.SVCategory((ISVCategory sv) =>
                {
                    result = sv.ALL_CATTransportMode();
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DTOResult ALL_SYSVarTypeOfOrder()
        {
            try
            {
                var result = default(DTOResult);
                ServiceFactory.SVCategory((ISVCategory sv) =>
                {
                    result = sv.ALL_SysVar(SYSVarType.TypeOfOrder);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DTOResult ALL_SYSVarTypeOfContractQuantity()
        {
            try
            {
                var result = default(DTOResult);
                ServiceFactory.SVCategory((ISVCategory sv) =>
                {
                    result = sv.ALL_SysVar(SYSVarType.TypeOfContractQuantity);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DTOResult ALL_SYSVarActivityRepeat()
        {
            try
            {
                var result = default(DTOResult);
                ServiceFactory.SVCategory((ISVCategory sv) =>
                {
                    result = sv.ALL_SysVar(SYSVarType.ActivityRepeat);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DTOResult ALL_SYSVarStatusOfAssetTimeSheet()
        {
            try
            {
                var result = default(DTOResult);
                ServiceFactory.SVCategory((ISVCategory sv) =>
                {
                    result = sv.ALL_SysVar(SYSVarType.StatusOfAssetTimeSheet);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DTOResult ALL_SYSVarScheduleDetailType()
        {
            try
            {
                var result = default(DTOResult);
                ServiceFactory.SVCategory((ISVCategory sv) =>
                {
                    result = sv.ALL_SysVar(SYSVarType.ScheduleDetailType);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DTOResult ALL_CATTypeOfPriceDIEx()
        {
            try
            {
                var result = default(DTOResult);
                ServiceFactory.SVCategory((ISVCategory sv) =>
                {
                    result = sv.ALL_CATTypeOfPriceDIEx();
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DTOResult ALL_CATPackingCO()
        {
            try
            {
                var result = default(DTOResult);
                ServiceFactory.SVCategory((ISVCategory sv) =>
                {
                    result = sv.ALL_CATPackingCO();
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DTOResult ALL_CATGroupOfCost()
        {
            try
            {
                var result = default(DTOResult);
                ServiceFactory.SVCategory((ISVCategory sv) =>
                {
                    result = sv.ALL_CATGroupOfCost();
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DTOResult ALL_CATPackingService()
        {
            try
            {
                var result = default(DTOResult);
                ServiceFactory.SVCategory((ISVCategory sv) =>
                {
                    result = sv.ALL_CATPacking(SYSVarType.TypeOfPackingService);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DTOResult ALL_CATGroupOfRomooc()
        {
            try
            {
                var result = default(DTOResult);
                ServiceFactory.SVCategory((ISVCategory sv) =>
                {
                    result = sv.ALL_CATGroupOfRomooc();
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DTOResult ALL_CATGroupOfEquipment()
        {
            try
            {
                var result = default(DTOResult);
                ServiceFactory.SVCategory((ISVCategory sv) =>
                {
                    result = sv.ALL_CATGroupOfEquipment();
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DTOResult ALL_CATGroupOfMaterial()
        {
            try
            {
                var result = default(DTOResult);
                ServiceFactory.SVCategory((ISVCategory sv) =>
                {
                    result = sv.ALL_CATGroupOfMaterial();
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DTOResult ALL_OPSTypeOfDITOGroupProductReturn()
        {
            try
            {
                var result = default(DTOResult);
                ServiceFactory.SVCategory((ISVCategory sv) =>
                {
                    result = sv.ALL_OPSTypeOfDITOGroupProductReturn();
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DTOResult ALL_SYSVarTypeOfContract()
        {
            try
            {
                var result = default(DTOResult);
                ServiceFactory.SVCategory((ISVCategory sv) =>
                {
                    result = sv.ALL_SysVar(SYSVarType.TypeOfContract);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DTOResult ALL_SYSVarTypeOfContractDate()
        {
            try
            {
                var result = default(DTOResult);
                ServiceFactory.SVCategory((ISVCategory sv) =>
                {
                    result = sv.ALL_SysVar(SYSVarType.TypeOfContractDate);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DTOResult ALL_SYSVarPriceOfGOP()
        {
            try
            {
                var result = default(DTOResult);
                ServiceFactory.SVCategory((ISVCategory sv) =>
                {
                    result = sv.ALL_SysVar(SYSVarType.PriceOfGOP);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DTOResult ALL_SYSVarTypeOfPriceEX()
        {
            try
            {
                var result = default(DTOResult);
                ServiceFactory.SVCategory((ISVCategory sv) =>
                {
                    result = sv.ALL_SysVar(SYSVarType.TypeOfPriceEX);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DTOResult ALL_SYSVarTypeOfReason()
        {
            try
            {
                var result = default(DTOResult);
                ServiceFactory.SVCategory((ISVCategory sv) =>
                {
                    result = sv.ALL_SysVar(SYSVarType.TypeOfReason);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DTOResult ALL_SYSVarTypeOfTOLocation()
        {
            try
            {
                var result = default(DTOResult);
                ServiceFactory.SVCategory((ISVCategory sv) =>
                {
                    result = sv.ALL_SysVar(SYSVarType.TypeOfTOLocation);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DTOResult SLI_SYSVarTypeOfGroupTrouble()
        {
            try
            {
                var result = default(DTOResult);
                ServiceFactory.SVCategory((ISVCategory sv) =>
                {
                    result = sv.ALL_SysVar(SYSVarType.TypeOfGroupTrouble);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DTOResult ALL_SYSVarTypeOfDriverRouteFee()
        {
            try
            {
                var result = default(DTOResult);
                ServiceFactory.SVCategory((ISVCategory sv) =>
                {
                    result = sv.ALL_SysVar(SYSVarType.TypeOfDriverRouteFee);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DTOResult All_CATGroupOfPartner()
        {
            try
            {
                var result = default(DTOResult);
                ServiceFactory.SVCategory((ISVCategory sv) =>
                {
                    result = sv.All_CATGroupOfPartner();
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DTOResult SLI_SYSVarPriceOfGOP()
        {
            try
            {
                var result = default(DTOResult);
                ServiceFactory.SVCategory((ISVCategory sv) =>
                {
                    result = sv.ALL_SysVar(SYSVarType.PriceOfGOP);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DTOResult ALL_CATPackingGOP()
        {
            try
            {
                var result = new DTOResult();
                ServiceFactory.SVCategory((ISVCategory sv) =>
                {
                    result = sv.ALL_CATPackingGOP();
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DTOResult ALL_TroubleCostStatus()
        {
            try
            {
                var result = new DTOResult();
                ServiceFactory.SVCategory((ISVCategory sv) =>
                {
                    result = sv.ALL_TroubleCostStatus();
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DTOResult SLI_SYSVarTransportModeOPSDI()
        {
            try
            {
                var result = new DTOResult();
                ServiceFactory.SVCategory((ISVCategory sv) =>
                {
                    result = sv.ALL_CATTransportMode();
                });

                var lst = result.Data.Cast<SYSVar>().Where(c => c.TypeOfVar != -(int)SYSVarType.TransportModeFTL || c.TypeOfVar != -(int)SYSVarType.TransportModeLTL).ToList();
                DTOResult res = new DTOResult();
                res.Total = lst.Count;
                res.Data = lst;
                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DTOResult SLI_SYSVarTypeOfActivity()
        {
            try
            {
                var result = new DTOResult();
                ServiceFactory.SVCategory((ISVCategory sv) =>
                {
                    result = sv.ALL_SysVar(SYSVarType.TypeOfActivity);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DTOResult ALL_TypeOfVehicle()
        {
            try
            {
                var result = default(DTOResult);
                ServiceFactory.SVCategory((ISVCategory sv) =>
                {
                    result = sv.ALL_SysVar(SYSVarType.TypeOfVehicle);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DTOResult ALL_Material()
        {
            try
            {
                var result = default(DTOResult);
                ServiceFactory.SVCategory((ISVCategory sv) =>
                {
                    result = sv.ALL_Material();
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DTOResult SLI_SYSVarTypeOfCost()
        {
            try
            {
                List<SYSVar> result = new List<SYSVar>();
                ServiceFactory.SVCategory((ISVCategory sv) =>
                {
                    result = sv.ALL_SysVar(SYSVarType.TypeOfCost).Data.Cast<SYSVar>().Where(c => c.ID != -(int)SYSVarType.TypeOfCostHidden && c.ID != -(int)SYSVarType.TypeOfCostFixedCost).ToList();
                });
                DTOResult newresult = new DTOResult();
                newresult.Data = result;
                newresult.Total = result.Count();
                return newresult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DTOResult ALL_SYSVarTypeOfDIPODStatus()
        {
            try
            {
                var result = default(DTOResult);
                ServiceFactory.SVCategory((ISVCategory sv) =>
                {
                    result = sv.ALL_SysVar(SYSVarType.DITOGroupProductStatusPOD);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DTOResult ALL_SYSVarTypeOfPriceDIEx()
        {
            try
            {
                var result = default(DTOResult);
                ServiceFactory.SVCategory((ISVCategory sv) =>
                {
                    result = sv.ALL_SysVar(SYSVarType.TypeOfPriceEX);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DTOResult ALL_SYSVarConstraintRequireType()
        {
            try
            {
                var result = default(DTOResult);
                ServiceFactory.SVCategory((ISVCategory sv) =>
                {
                    result = sv.ALL_SysVar(SYSVarType.ConstraintRequireType);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DTOResult ALL_SYSVarTypeOfWAInspectionStatus()
        {
            try
            {
                var result = default(DTOResult);
                ServiceFactory.SVCategory((ISVCategory sv) =>
                {
                    result = sv.ALL_SysVar(SYSVarType.TypeOfWAInspectionStatus);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DTOResult ALL_CATTypeOfPriceCOEx()
        {
            try
            {
                var result = default(DTOResult);
                ServiceFactory.SVCategory((ISVCategory sv) =>
                {
                    result = sv.ALL_CATTypeOfPriceCOEx();
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DTOResult ALL_SYSVarCOExSum()
        {
            try
            {
                var result = default(DTOResult);
                ServiceFactory.SVCategory((ISVCategory sv) =>
                {
                    result = sv.ALL_SysVar(SYSVarType.COExSum);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DTOResult ALL_SYSVarDIExSum()
        {
            try
            {
                var result = default(DTOResult);
                ServiceFactory.SVCategory((ISVCategory sv) =>
                {
                    result = sv.ALL_SysVar(SYSVarType.DIExSum);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DTOResult ALL_SYSVarDIMOQSum()
        {
            try
            {
                var result = default(DTOResult);
                ServiceFactory.SVCategory((ISVCategory sv) =>
                {
                    result = sv.ALL_SysVar(SYSVarType.DIMOQSum);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DTOResult ALL_SYSVarDIMOQLoadSum()
        {
            try
            {
                var result = default(DTOResult);
                ServiceFactory.SVCategory((ISVCategory sv) =>
                {
                    result = sv.ALL_SysVar(SYSVarType.DIMOQLoadSum);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DTOResult ALL_CATGroupOfLocation()
        {
            try
            {
                var result = default(DTOResult);
                ServiceFactory.SVCategory((ISVCategory sv) =>
                {
                    result = sv.ALL_CATGroupOfLocation();
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DTOResult ALL_SYSVarTypeOfDITOGroupProductReturnStatus()
        {
            try
            {
                var result = default(DTOResult);
                ServiceFactory.SVCategory((ISVCategory sv) =>
                {
                    result = sv.ALL_SysVar(SYSVarType.TypeOfDITOGroupProductReturnStatus);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DTOResult ALL_SYSVarTypeOfRunLevel()
        {
            try
            {
                var result = default(DTOResult);
                ServiceFactory.SVCategory((ISVCategory sv) =>
                {
                    result = sv.ALL_SysVar(SYSVarType.TypeOfRunLevel);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DTOResult ALL_TypeOfSGroupProductChange()
        {
            try
            {
                var result = default(DTOResult);
                ServiceFactory.SVCategory((ISVCategory sv) =>
                {
                    result = sv.ALL_SysVar(SYSVarType.TypeOfSGroupProductChange);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DTOResult ALL_CATDrivingLicence()
        {
            try
            {
                var result = default(DTOResult);
                ServiceFactory.SVCategory((ISVCategory sv) =>
                {
                    result = sv.ALL_CATDrivingLicence();
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DTOResult ALL_SYSVarTypeOfDriver()
        {
            try
            {
                var result = default(DTOResult);
                ServiceFactory.SVCategory((ISVCategory sv) =>
                {
                    result = sv.ALL_SysVar(SYSVarType.TypeOfDriver);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DTOResult ALL_CATShift()
        {
            try
            {
                var result = default(DTOResult);
                ServiceFactory.SVCategory((ISVCategory sv) =>
                {
                    result = sv.ALL_CATShift();
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DTOResult ALL_ExtReturnStatus()
        {
            try
            {
                var result = default(DTOResult);
                ServiceFactory.SVCategory((ISVCategory sv) =>
                {
                    result = sv.ALL_SysVar(SYSVarType.ExtReturnStatus);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DTOResult ALL_TypeOfWAInspectionStatus()
        {
            try
            {
                var result = default(DTOResult);
                ServiceFactory.SVCategory((ISVCategory sv) =>
                {
                    result = sv.ALL_SysVar(SYSVarType.TypeOfWAInspectionStatus);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DTOResult ALL_TypeScheduleDate()
        {
            try
            {
                var result = default(DTOResult);
                ServiceFactory.SVCategory((ISVCategory sv) =>
                {
                    result = sv.ALL_SysVar(SYSVarType.TypeScheduleDate);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DTOResult ALL_FLMTypeOfScheduleFee()
        {
            try
            {
                var result = default(DTOResult);
                ServiceFactory.SVCategory((ISVCategory sv) =>
                {
                    result = sv.ALL_FLMTypeOfScheduleFee();
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DTOResult ALL_SYSVarTypeOfScheduleFeeStatus()
        {
            try
            {
                var result = default(DTOResult);
                ServiceFactory.SVCategory((ISVCategory sv) =>
                {
                    result = sv.ALL_SysVar(SYSVarType.TypeOfScheduleFeeStatus);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DTOResult ALL_SYSVarTypeOfKPI()
        {
            try
            {
                var result = default(DTOResult);
                ServiceFactory.SVCategory((ISVCategory sv) =>
                {
                    result = sv.ALL_SysVar(SYSVarType.KPIType);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DTOResult ALL_SYSVarColumnType()
        {
            try
            {
                var result = default(DTOResult);
                ServiceFactory.SVCategory((ISVCategory sv) =>
                {
                    result = sv.ALL_SysVar(SYSVarType.KPIColumnType);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DTOResult ALL_CATTypeOfDriverFee(dynamic dynParam)
        {
            try
            {
                DTOResult result = new DTOResult();
                ServiceFactory.SVCategory((ISVCategory sv) =>
                {
                    result = sv.ALL_CATTypeOfDriverFee();
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DTOResult ALL_SYSVarPacketProcessType()
        {
            try
            {
                var result = default(DTOResult);
                ServiceFactory.SVCategory((ISVCategory sv) =>
                {
                    result = sv.ALL_SysVar(SYSVarType.PacketProcessType);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DTOResult ALL_SYSVarPacketSettingType()
        {
            try
            {
                var result = default(DTOResult);
                ServiceFactory.SVCategory((ISVCategory sv) =>
                {
                    result = sv.ALL_SysVar(SYSVarType.PacketSettingType);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DTOResult ALL_SYSVarContractRoutingType()
        {
            try
            {
                var result = default(DTOResult);
                ServiceFactory.SVCategory((ISVCategory sv) =>
                {
                    result = sv.ALL_SysVar(SYSVarType.ContractRoutingType);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DTOResult ALL_SYSVarFLMTypeWarning()
        {
            try
            {
                var result = default(DTOResult);
                ServiceFactory.SVCategory((ISVCategory sv) =>
                {
                    result = sv.ALL_SYSVarFLMTypeWarning();
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DTOResult ALL_SYSVarREPOwnerAsset()
        {
            try
            {
                var result = default(DTOResult);
                ServiceFactory.SVCategory((ISVCategory sv) =>
                {
                    result = sv.ALL_SYSVarREPOwnerAsset();
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DTOResult ALL_SYSVarRouteDetailStatusMode()
        {
            try
            {
                var result = default(DTOResult);
                ServiceFactory.SVCategory((ISVCategory sv) =>
                {
                    result = sv.ALL_SysVar(SYSVarType.RouteDetailStatusMode);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DTOResult ALL_SYSVarExtReturnStatus()
        {
            try
            {
                var result = default(DTOResult);
                ServiceFactory.SVCategory((ISVCategory sv) =>
                {
                    result = sv.ALL_SysVar(SYSVarType.ExtReturnStatus);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DTOResult ALL_SYSVarMaterialAuditStatus()
        {
            try
            {
                var result = default(DTOResult);
                ServiceFactory.SVCategory((ISVCategory sv) =>
                {
                    result = sv.ALL_SysVar(SYSVarType.MaterialAuditStatus);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region CATCarrier

        #region Main
        public DTOResult CATCarrier_Customer_Read()
        {
            try
            {
                var result = default(DTOResult);
                ServiceFactory.SVCategory((IServices.ISVCategory sv) =>
                {
                    result = sv.Customer_List();
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DTOResult CATCarrier_CarrierCustom_Read()
        {
            try
            {
                var result = default(DTOResult);
                ServiceFactory.SVCategory((IServices.ISVCategory sv) =>
                {
                    result = sv.CarrierCustom_List();
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DTOPartnerLocationResult CATCarrier_CarrierCustomer_List(dynamic dynParam)
        {
            try
            {
                List<int> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.lst.ToString());
                var result = default(DTOPartnerLocationResult);
                if (lst == null)
                    lst = new List<int>();
                ServiceFactory.SVCategory((IServices.ISVCategory sv) =>
                {
                    result = sv.CarrierCustomer_List(lst);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public void CATCarrier_CarrierCustom_SaveList(dynamic d)
        {
            try
            {
                DTOCATCarrierCustom item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOCATCarrierCustom>(d.item.ToString());
               
                    ServiceFactory.SVCategory((IServices.ISVCategory sv) =>
                    {
                        sv.CarrierCustom_Save(item);
                    });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOCATCarrier CATCarrier_Carrier_Get(dynamic d)
        {
            try
            {
                int id = (int)d.id;
                var result = new DTOCATCarrier();
                ServiceFactory.SVCategory((IServices.ISVCategory sv) =>
                {
                    result = sv.Carrier_Get(id);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public bool CATCarrier_Carrier_Update(dynamic d)
        {
            try
            {
                CATPartner item = Newtonsoft.Json.JsonConvert.DeserializeObject<CATPartner>(d.item.ToString());
                ServiceFactory.SVCategory((IServices.ISVCategory sv) =>
                {
                    item.ID = sv.Carrier_Save(item);
                });
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public bool CATCarrier_Carrier_Destroy(dynamic d)
        {
            try
            {
                DTOCATCarrier item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOCATCarrier>(d.item.ToString());
                if (item != null)
                {
                    ServiceFactory.SVCategory((IServices.ISVCategory sv) =>
                    {
                        sv.Carrier_Delete(item);
                    });
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Location
        public DTOResult CATCarrier_Location_Read(dynamic d)
        {
            try
            {
                string request = d.request.ToString();
                int carrierID = d.partnerid;
                var result = default(DTOResult);
                ServiceFactory.SVCategory((IServices.ISVCategory sv) =>
                {
                    result = sv.LocationInCarrier_List(request, carrierID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public DTOCATLocationInPartner CATCarrier_Location_Get(dynamic d)
        {
            try
            {
                int id = (int)d.id;
                DTOCATLocationInPartner result = new DTOCATLocationInPartner();
                ServiceFactory.SVCategory((IServices.ISVCategory sv) =>
                {
                    result = sv.LocationInDistributor_Get(id);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public bool CATCarrier_Location_Update(dynamic d)
        {
            try
            {
                DTOCATLocationInPartner item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOCATLocationInPartner>(d.item.ToString());
                int carrierID = d.partnerid;
                if (item != null)
                {
                    ServiceFactory.SVCategory((IServices.ISVCategory sv) =>
                    {
                        item = sv.LocationInCarrier_Save(item, carrierID);
                    });
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public bool CATCarrier_Location_Destroy(dynamic d)
        {
            try
            {
                DTOCATLocationInPartner item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOCATLocationInPartner>(d.item.ToString());
                if (item != null && ModelState.IsValid)
                {
                    ServiceFactory.SVCategory((IServices.ISVCategory sv) =>
                    {
                        sv.LocationInCarrier_Delete(item);
                    });
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Location Not In
        [HttpPost]
        public DTOResult CATCarrier_LocationNotIn_Read(dynamic d)
        {
            try
            {
                string request = d.request.ToString();
                int carrierID = d.partnerid;
                var result = default(DTOResult);
                ServiceFactory.SVCategory((IServices.ISVCategory sv) =>
                {
                    result = sv.LocationNotInCarrier_List(request, carrierID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public bool CATCarrier_LocationNotIn_SaveList(dynamic d)
        {
            try
            {
                IEnumerable<DTOCATLocationInPartner> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<IEnumerable<DTOCATLocationInPartner>>(d.lst.ToString());
                int partnerid = d.partnerid;
                if (lst != null)
                {
                    ServiceFactory.SVCategory((IServices.ISVCategory sv) =>
                    {
                        sv.LocationNotInCarrier_SaveList(lst.ToList(), partnerid);
                    });
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Import Export

        #region imex
        public string CATCarrier_Export_GetData()
        {
            try
            {
                List<DTOCATPartnerImport> result = new List<DTOCATPartnerImport>();
                ServiceFactory.SVCategory((IServices.ISVCategory sv) =>
                {
                    result = sv.Carrier_Export();
                });

                string file = "/" + FolderUpload.Export + "ExportCarrier_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx";

                if (System.IO.File.Exists(HttpContext.Current.Server.MapPath(file)))
                    System.IO.File.Delete(HttpContext.Current.Server.MapPath(file));
                FileInfo exportfile = new FileInfo(HttpContext.Current.Server.MapPath(file));
                using (ExcelPackage package = new ExcelPackage(exportfile))
                {
                    // Sheet 1
                    ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Sheet 1");

                    int col = 1, row = 1, sttlv1 = 1, sttlv2 = 1;
                    #region header
                    worksheet.Cells[row, col].Value = "STT";
                    col++; worksheet.Cells[row, col].Value = "Mã hệ thống"; worksheet.Column(col).Width = 20;
                    col++; worksheet.Cells[row, col].Value = "Mã địa chỉ"; worksheet.Column(col).Width = 20;
                    col++; worksheet.Cells[row, col].Value = "Tên hãng tàu"; worksheet.Column(col).Width = 15;
                    col++; worksheet.Cells[row, col].Value = "Địa chỉ giao hàng"; worksheet.Column(col).Width = 15;
                    col++; worksheet.Cells[row, col].Value = "Tỉnh thành"; worksheet.Column(col).Width = 15;
                    col++; worksheet.Cells[row, col].Value = "Quận/Huyện"; worksheet.Column(col).Width = 15;
                    col++; worksheet.Cells[row, col].Value = "Khu công nghiệp"; worksheet.Column(col).Width = 15;
                    col++; worksheet.Cells[row, col].Value = "SĐT"; worksheet.Column(col).Width = 15;
                    col++; worksheet.Cells[row, col].Value = "Fax"; worksheet.Column(col).Width = 15;
                    col++; worksheet.Cells[row, col].Value = "Email"; worksheet.Column(col).Width = 15;
                    col++; worksheet.Cells[row, col].Value = "Kinh độ"; worksheet.Column(col).Width = 15;
                    col++; worksheet.Cells[row, col].Value = "Vĩ dộ"; worksheet.Column(col).Width = 15;

                    ExcelHelper.CreateCellStyle(worksheet, row, 1, row, col, false, true, ExcelHelper.ColorGreen, ExcelHelper.ColorWhite, 0, "");

                    worksheet.Cells[row, 1, row, col].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    worksheet.Cells[row, 1, row, col].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    #endregion

                    #region data

                    col = 1;
                    row = 2;
                    foreach (var item in result)
                    {
                        col = 1;
                        worksheet.Cells[row, col].Value = sttlv1;
                        col++; worksheet.Cells[row, col].Value = item.Code;
                        col++; worksheet.Cells[row, col].Value = string.Empty;
                        col++; worksheet.Cells[row, col].Value = item.PartnerName;
                        col++; worksheet.Cells[row, col].Value = item.Address;
                        col++; worksheet.Cells[row, col].Value = item.ProvinceName;
                        col++; worksheet.Cells[row, col].Value = item.DistrictName;
                        col++; worksheet.Cells[row, col].Value = string.Empty;
                        col++; worksheet.Cells[row, col].Value = item.TelNo;
                        col++; worksheet.Cells[row, col].Value = item.Fax;
                        col++; worksheet.Cells[row, col].Value = item.Email;
                        col++; worksheet.Cells[row, col].Value = string.Empty;
                        col++; worksheet.Cells[row, col].Value = string.Empty;

                        worksheet.Row(row).OutlineLevel = 0;

                        ExcelHelper.CreateCellStyle(worksheet, row, 1, row, col, false, false, ExcelHelper.ColorYellow, "", 0, "");

                        sttlv2 = 1;
                        foreach (var detail in item.lstLocation)
                        {
                            col = 1;
                            row++;
                            worksheet.Cells[row, col].Value = sttlv2;
                            col++; worksheet.Cells[row, col].Value = detail.Code;//ma he thong
                            col++; worksheet.Cells[row, col].Value = detail.PartnerCode; //ma dia chi
                            col++; worksheet.Cells[row, col].Value = detail.LocationName;
                            col++; worksheet.Cells[row, col].Value = detail.Address;
                            col++; worksheet.Cells[row, col].Value = detail.ProvinceName;
                            col++; worksheet.Cells[row, col].Value = detail.DistrictName;
                            col++; worksheet.Cells[row, col].Value = detail.EconomicZone;
                            col++; worksheet.Cells[row, col].Value = string.Empty;
                            col++; worksheet.Cells[row, col].Value = string.Empty;
                            col++; worksheet.Cells[row, col].Value = string.Empty;
                            col++; worksheet.Cells[row, col].Value = detail.Lat;
                            col++; worksheet.Cells[row, col].Value = detail.Lng;

                            worksheet.Row(row).OutlineLevel = 1;
                            sttlv2++;
                        }
                        sttlv1++;
                        row++;
                    }

                    #endregion
                    ExcelHelper.CreateCellStyle(worksheet, 2, 2, worksheet.Dimension.End.Row, 2, false, false, ExcelHelper.ColorOrange, "", 0, "");

                    for (int i = 1; i <= worksheet.Dimension.End.Row; i++)
                    {
                        for (int j = 1; j <= worksheet.Dimension.End.Column; j++)
                        {
                            worksheet.Cells[i, j].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                        }
                    }
                    package.Save();
                }
                return file;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void CATCarrier_Excel_Save(dynamic dynParam)
        {
            try
            {
                List<DTOCATPartnerImport> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DTOCATPartnerImport>>(dynParam.lst.ToString());
                ServiceFactory.SVCategory((IServices.ISVCategory sv) =>
                {
                    sv.Carrier_Import(lst);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public List<DTOCATPartnerImport> CATCarrier_Excel_Check(dynamic dynParam)
        {
            try
            {
                CATFile file = Newtonsoft.Json.JsonConvert.DeserializeObject<CATFile>(dynParam.item.ToString());
                List<DTOCATPartnerImport> result = new List<DTOCATPartnerImport>();

                if (file != null && !string.IsNullOrEmpty(file.FilePath))
                {
                    List<DTOCATPartnerItem> listCarrier = new List<DTOCATPartnerItem>();
                    List<DTOCATLocationItem> listCarrierLocation = new List<DTOCATLocationItem>();
                    List<CATProvince> listProvince = new List<CATProvince>();
                    List<CATDistrict> listDistrict = new List<CATDistrict>();

                    ServiceFactory.SVCategory((IServices.ISVCategory sv) =>
                    {
                        listCarrier = sv.Carrier_AllList().Data.Cast<DTOCATPartnerItem>().ToList();
                        listCarrierLocation = sv.CarrierLocation_AllList().Data.Cast<DTOCATLocationItem>().ToList();
                        listProvince = sv.ExcelProvince_List();
                        listDistrict = sv.ExcelDistrict_List();
                    });

                    using (System.IO.FileStream fs = new System.IO.FileStream(HttpContext.Current.Server.MapPath("/" + file.FilePath), System.IO.FileMode.Open, System.IO.FileAccess.Read))
                    {
                        using (var package = new ExcelPackage(fs))
                        {
                            ExcelWorksheet worksheet = ExcelHelper.GetWorksheetByIndex(package, 1);

                            int col = 2, row;
                            string Code, PartnerCode, PartnerName, Address, ProvinceName, DistrictName, Zone, TellNo, Fax, Email, Lat, Lng;
                            if (worksheet != null)
                            {
                                row = 2;
                                while (row <= worksheet.Dimension.End.Row)
                                {
                                    List<string> lstError = new List<string>();

                                    #region đọc dữ liệu
                                    col = 2;
                                    Code = ExcelHelper.GetValue(worksheet, row, col); Code = Code.Trim();
                                    col++; PartnerCode = ExcelHelper.GetValue(worksheet, row, col); PartnerCode = PartnerCode.Trim();
                                    col++; PartnerName = ExcelHelper.GetValue(worksheet, row, col); PartnerName = PartnerName.Trim();
                                    col++; Address = ExcelHelper.GetValue(worksheet, row, col); Address = Address.Trim();
                                    col++; ProvinceName = ExcelHelper.GetValue(worksheet, row, col); ProvinceName = ProvinceName.Trim();
                                    col++; DistrictName = ExcelHelper.GetValue(worksheet, row, col); DistrictName = DistrictName.Trim();
                                    col++; Zone = ExcelHelper.GetValue(worksheet, row, col); Zone = Zone.Trim();
                                    col++; TellNo = ExcelHelper.GetValue(worksheet, row, col); TellNo = TellNo.Trim();
                                    col++; Fax = ExcelHelper.GetValue(worksheet, row, col); Fax = Fax.Trim();
                                    col++; Email = ExcelHelper.GetValue(worksheet, row, col); Email = Email.Trim();
                                    col++; Lat = ExcelHelper.GetValue(worksheet, row, col); Lat = Lat.Trim();
                                    col++; Lng = ExcelHelper.GetValue(worksheet, row, col); Lng = Lng.Trim();
                                    #endregion

                                    if (row == 2 && !string.IsNullOrEmpty(PartnerCode))
                                        throw new Exception("Không tìm thấy Partner");

                                    if (string.IsNullOrEmpty(PartnerCode))// nut cha
                                    {
                                        DTOCATPartnerImport obj = new DTOCATPartnerImport();
                                        obj.lstLocation = new List<DTOCATLocationImport>();
                                        obj.ExcelRow = row;
                                        if (!string.IsNullOrEmpty(Code)) //
                                        {
                                            #region data parent
                                            var checkCode = listCarrier.FirstOrDefault(c => c.Code == Code);
                                            if (checkCode != null)
                                                obj.ID = checkCode.ID;
                                            else
                                                obj.ID = 0;
                                            var checkCodeOnFile = result.FirstOrDefault(c => c.Code == Code);
                                            if (checkCodeOnFile != null) lstError.Add("Mã hãng tàu [" + Code + "] bị trùng");
                                            obj.Code = Code;
                                            obj.Address = Address;
                                            obj.PartnerName = PartnerName;
                                            obj.TelNo = TellNo;
                                            obj.Fax = Fax;
                                            obj.Email = Email;

                                            if (!string.IsNullOrEmpty(ProvinceName))
                                            {
                                                var checkProvince = listProvince.FirstOrDefault(c => c.ProvinceName == ProvinceName);

                                                if (checkProvince == null)
                                                    lstError.Add("[" + ProvinceName + "] không tồn tại trong hệ thống");
                                                else
                                                {
                                                    obj.ProvinceID = checkProvince.ID; obj.ProvinceName = checkProvince.ProvinceName;
                                                    obj.CountryID = checkProvince.CountryID; obj.CountryName = checkProvince.CountryName;
                                                }
                                            }
                                            else
                                            {
                                                obj.ProvinceID = null;
                                                obj.CountryID = null;
                                            }

                                            if (!string.IsNullOrEmpty(DistrictName))
                                            {
                                                var checkDistrict = listDistrict.FirstOrDefault(c => c.DistrictName == DistrictName);
                                                if (checkDistrict == null)
                                                    lstError.Add("[" + DistrictName + "] không tồn tại trong hệ thống");
                                                else
                                                {
                                                    obj.DistrictID = checkDistrict.ID; obj.DistrictName = DistrictName;
                                                }
                                            }
                                            else
                                            {
                                                obj.DistrictID = null;
                                            }
                                            #endregion

                                        }
                                        else//ko co code
                                        {
                                            obj.Code = string.Empty;
                                            obj.Address = Address;
                                            obj.PartnerName = PartnerName;
                                            obj.TelNo = TellNo;
                                            obj.Fax = Fax;
                                            obj.Email = Email;
                                            lstError.Add("Thiếu mã hãng tàu");
                                        }

                                        obj.ExcelSuccess = true; obj.ExcelError = string.Empty;
                                        if (lstError.Count > 0) { obj.ExcelSuccess = false; obj.ExcelError = string.Join(" ,", lstError); }
                                        result.Add(obj);
                                    }
                                    else//nut con
                                    {
                                        var obj = result.LastOrDefault();//lay parent cuối
                                        DTOCATLocationImport objLocation = new DTOCATLocationImport();
                                        if (obj == null)
                                            lstError.Add("Không tìm thấy cha");
                                        else
                                        {
                                            objLocation.EconomicZone = Zone;
                                            var checkSysCodeLocation = listCarrierLocation.FirstOrDefault(c => c.Code == Code);
                                            if (obj.ID > 0)//parent da co
                                            {
                                                if (checkSysCodeLocation == null)//location moi
                                                {
                                                    objLocation.ID = 0;
                                                    objLocation.Code = Code;
                                                }
                                                else//location cu
                                                {
                                                    objLocation.ID = checkSysCodeLocation.ID;
                                                    objLocation.Code = Code;
                                                }
                                            }
                                            else// parent moi
                                            {
                                                if (checkSysCodeLocation == null)
                                                {
                                                    objLocation.ID = 0;
                                                    objLocation.Code = Code;
                                                }
                                                else//location cu
                                                {
                                                    objLocation.ID = checkSysCodeLocation.ID;
                                                    objLocation.Code = Code;
                                                }
                                            }

                                            var chkOnFile = obj.lstLocation.FirstOrDefault(c => c.Code == Code);
                                            if (chkOnFile != null) lstError.Add("Mã [" + Code + "] bị trùng");

                                            var checkCodeLocation = listCarrierLocation.FirstOrDefault(c => c.PartnerCode == PartnerCode);

                                            if (obj.ID > 0)//parent da co
                                            {
                                                if (checkCodeLocation == null)//location mo
                                                    objLocation.PartnerCode = PartnerCode;
                                                else//location cu
                                                {
                                                    if (checkCodeLocation.ID == objLocation.ID)
                                                        objLocation.PartnerCode = PartnerCode;
                                                    else lstError.Add("Mã địa chỉ [" + PartnerCode + "] Đã sử dụng");
                                                }
                                            }
                                            else// parent moi
                                            {
                                                if (checkCodeLocation == null)
                                                    objLocation.PartnerCode = PartnerCode;
                                                else lstError.Add("Mã địa chỉ [" + PartnerCode + "] Đã sử dụng");
                                            }

                                            var chkPartnerCodeOnFile = result.Count(c => c.lstLocation.Any(d => d.PartnerCode == PartnerCode)) > 0;
                                            if (chkPartnerCodeOnFile) lstError.Add("Mã địa chỉ [" + PartnerCode + "] Đã bị trùng");



                                            objLocation.LocationName = PartnerName;
                                            objLocation.Address = Address;
                                            var checkProvinceLocation = listProvince.FirstOrDefault(c => c.ProvinceName == ProvinceName);
                                            if (!string.IsNullOrEmpty(ProvinceName))
                                            {
                                                if (checkProvinceLocation == null)
                                                    lstError.Add("[" + ProvinceName + "] không tồn tại trong hệ thống");
                                                else
                                                {
                                                    objLocation.ProvinceID = checkProvinceLocation.ID;
                                                    objLocation.ProvinceName = checkProvinceLocation.ProvinceName;
                                                    objLocation.CountryID = checkProvinceLocation.CountryID; objLocation.CountryName = checkProvinceLocation.CountryName;
                                                }
                                            }
                                            else lstError.Add("[Tỉnh thành] không được trống");
                                            if (!string.IsNullOrEmpty(DistrictName))
                                            {
                                                var checkDistrictLocation = listDistrict.FirstOrDefault(c => c.DistrictName == DistrictName);
                                                if (checkDistrictLocation == null)
                                                    lstError.Add("[" + DistrictName + "] không tồn tại trong hệ thống");
                                                else
                                                {
                                                    objLocation.DistrictID = checkDistrictLocation.ID;
                                                    objLocation.DistrictName = checkDistrictLocation.DistrictName;
                                                }
                                            }
                                            else lstError.Add("[Quận huyện] không được trống");
                                            if (string.IsNullOrEmpty(Lat))
                                                objLocation.Lat = null;
                                            else
                                            {
                                                try
                                                {
                                                    objLocation.Lat = Convert.ToDouble(Lat);
                                                }
                                                catch
                                                {
                                                    lstError.Add("[Vĩ độ] không chính xác");
                                                }
                                            }

                                            if (string.IsNullOrEmpty(Lng))
                                                objLocation.Lng = null;
                                            else
                                            {
                                                try
                                                {
                                                    objLocation.Lng = Convert.ToDouble(Lng);
                                                }
                                                catch
                                                {
                                                    lstError.Add("[Kinh độ] không chính xác");
                                                }
                                            }
                                        }
                                        objLocation.IsSuccess = true; objLocation.Error = string.Empty;
                                        if (lstError.Count > 0) { objLocation.IsSuccess = false; objLocation.Error = string.Join(" ,", lstError); }

                                        if (!obj.ExcelSuccess) { objLocation.IsSuccess = false; }
                                        if (!objLocation.IsSuccess)
                                        {
                                            if (string.IsNullOrEmpty(obj.ExcelError)) obj.ExcelError = objLocation.Error;
                                            else obj.ExcelError = obj.ExcelError + ", " + objLocation.Error;
                                        }
                                        obj.lstLocation.Add(objLocation);
                                    }
                                    row++;
                                }
                            }
                        }
                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region phân bổ mã
        public string CATCarrier_Excel_ExportCode(dynamic dynParam)
        {
            try
            {
                List<int> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.lst.ToString());
                DTOCUSPartnerLocationExport result = new DTOCUSPartnerLocationExport();
                ServiceFactory.SVCategory((IServices.ISVCategory sv) =>
                {
                    result = sv.PartnerLocation_Carrier_Export(lst);
                });
                string file = "/" + FolderUpload.Export + "ExportPartnerLocation_Carrier_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx";


                if (System.IO.File.Exists(HttpContext.Current.Server.MapPath(file)))
                    System.IO.File.Delete(HttpContext.Current.Server.MapPath(file));
                FileInfo exportfile = new FileInfo(HttpContext.Current.Server.MapPath(file));
                using (ExcelPackage package = new ExcelPackage(exportfile))
                {
                    // Sheet 1
                    ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Sheet 1");

                    int col = 1, row = 1, sttlv1 = 1, sttlv2 = 1;
                    #region header
                    worksheet.Cells[row, col].Value = "STT";
                    col++; worksheet.Cells[row, col].Value = "Loại"; worksheet.Column(col).Width = 15;
                    col++; worksheet.Cells[row, col].Value = "Hãng tàu"; worksheet.Column(col).Width = 35;
                    col++; worksheet.Cells[row, col].Value = "Địa chỉ"; worksheet.Column(col).Width = 40;
                    col++; worksheet.Cells[row, col].Value = "Hệ thống"; worksheet.Column(col).Width = 15;

                    #region header dong
                    if (result.lstCustomer.Count > 0)
                    {
                        foreach (var cus in result.lstCustomer)
                        {
                            col++; worksheet.Cells[row, col].Value = cus.Code; worksheet.Column(col).Width = 15;
                        }
                    }

                    #endregion

                    worksheet.Cells[row, 1, row, col].Style.Font.Bold = true;
                    worksheet.Cells[row, 1, row, col].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    worksheet.Cells[row, 1, row, col].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    worksheet.Cells[row, 1, row, col].Style.Font.Color.SetColor(Color.White);
                    worksheet.Cells[row, 1, row, col].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    worksheet.Cells[row, 1, row, col].Style.Fill.BackgroundColor.SetColor(Color.Green);

                    #endregion

                    #region data

                    if (result.lstPartner.Count > 0)
                    {
                        row = 2; col = 1; sttlv1 = 1;
                        foreach (var partner in result.lstPartner)
                        {
                            col = 1;
                            worksheet.Cells[row, col].Value = sttlv1;
                            col++; worksheet.Cells[row, col].Value = partner.GroupOfPartnerName;
                            col++; worksheet.Cells[row, col].Value = partner.Name;
                            col++; worksheet.Cells[row, col].Value = partner.Address;
                            col++; worksheet.Cells[row, col].Value = partner.Code;
                            var lstCusCode = result.lstCusPartner.Where(c => c.PartnerID == partner.ID).ToList();
                            if (lstCusCode.Count > 0)
                            {
                                foreach (var cus in result.lstCustomer)
                                {
                                    var code = lstCusCode.FirstOrDefault(c => c.CustomerID == cus.ID);
                                    col++;
                                    if (code != null)
                                    {
                                        worksheet.Cells[row, col].Value = code.Code;
                                    }
                                }
                            }
                            ExcelHelper.CreateCellStyle(worksheet, row, 1, row, worksheet.Dimension.End.Column, false, false, ExcelHelper.ColorYellow, "", 0, "");
                            if (partner.lstLocation.Count > 0)
                            {
                                col = 2;
                                sttlv2 = 1;
                                foreach (var location in partner.lstLocation)
                                {
                                    col = 2;
                                    row++;
                                    worksheet.Cells[row, col].Value = sttlv2;
                                    col++; worksheet.Cells[row, col].Value = location.Name;
                                    col++; worksheet.Cells[row, col].Value = location.Address;
                                    col++; worksheet.Cells[row, col].Value = location.PartnerCode;
                                    var lstLocationCode = result.lstCusLocation.Where(c => c.LocationID == location.ID && c.PartnerID == partner.ID).ToList();
                                    if (lstLocationCode.Count > 0)
                                    {
                                        foreach (var cus in result.lstCustomer)
                                        {
                                            var code = lstLocationCode.FirstOrDefault(c => c.CustomerID == cus.ID);
                                            col++;
                                            if (code != null)
                                            {
                                                worksheet.Cells[row, col].Value = code.Code;
                                            }
                                        }
                                    }
                                    sttlv2++;
                                }
                            }
                            row++;
                            sttlv1++;
                        }
                    }

                    #endregion

                    for (int i = 1; i <= worksheet.Dimension.End.Row; i++)
                    {
                        for (int j = 1; j <= worksheet.Dimension.End.Column; j++)
                        {
                            worksheet.Cells[i, j].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                        }
                    }
                    package.Save();
                }
                return file;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void CATCarrier_Excel_SaveCode(dynamic dynParam)
        {
            try
            {
                List<DTOCUSPartnerLocationResult> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DTOCUSPartnerLocationResult>>(dynParam.lst.ToString());
                DTOCUSPartnerLocationImport data = new DTOCUSPartnerLocationImport();
                data.lstCusLocation = new List<DTOCustomerLocationImport>();
                data.lstCusPartner = new List<DTOCustomerPartnerImport>();
                foreach (var item in lst)
                {
                    if (item.IsPartner) data.lstCusPartner.AddRange(item.lstCusPartner);
                    else data.lstCusLocation.AddRange(item.lstCusLocation);
                }
                ServiceFactory.SVCategory((IServices.ISVCategory sv) =>
                {
                    sv.CarrierLocation_Import(data);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public List<DTOCUSPartnerLocationResult> CATCarrier_Excel_CheckCode(dynamic dynParam)
        {
            try
            {
                CATFile file = Newtonsoft.Json.JsonConvert.DeserializeObject<CATFile>(dynParam.item.ToString());
                List<DTOCUSPartnerLocationResult> result = new List<DTOCUSPartnerLocationResult>();

                if (file != null && !string.IsNullOrEmpty(file.FilePath))
                {
                    DTOCUSPartnerLocationExport data = new DTOCUSPartnerLocationExport();
                    List<CUSCustomer> lstCustomer = new List<CUSCustomer>();
                    List<DTOCustomerPartnerImport> lstCusPartner = new List<DTOCustomerPartnerImport>();
                    List<DTOCUSPartnerImport> lstPartner = new List<DTOCUSPartnerImport>();
                    List<DTOCustomerLocationImport> lstCusLocation = new List<DTOCustomerLocationImport>();

                    List<int> lstCustomerID = new List<int>();

                    ServiceFactory.SVCategory((IServices.ISVCategory sv) =>
                    {
                        lstCustomer = sv.Customer_List().Data.Cast<CUSCustomer>().ToList();
                    });

                    using (System.IO.FileStream fs = new System.IO.FileStream(HttpContext.Current.Server.MapPath("/" + file.FilePath), System.IO.FileMode.Open, System.IO.FileAccess.Read))
                    {
                        using (var package = new ExcelPackage(fs))
                        {
                            ExcelWorksheet worksheet = ExcelHelper.GetWorksheetByIndex(package, 1);

                            int col = 2, row;
                            string Code, SysCode, strCheckPartner, strCheckBreak;
                            if (worksheet != null)
                            {
                                row = 1;
                                if (worksheet.Dimension.End.Column >= 6)
                                {
                                    Dictionary<int, int> dicCusIDCol = new Dictionary<int, int>();
                                    Dictionary<int, string> dicCusIDCode = new Dictionary<int, string>();
                                    for (int i = 6; i <= worksheet.Dimension.End.Column; i++)
                                    {
                                        Code = ExcelHelper.GetValue(worksheet, row, i);
                                        Code = Code.Trim();
                                        var checkCusName = lstCustomer.FirstOrDefault(c => c.Code == Code);
                                        if (checkCusName != null)
                                        {
                                            if (!dicCusIDCode.ContainsValue(Code))
                                            {
                                                dicCusIDCol.Add(checkCusName.ID, i);
                                                dicCusIDCode.Add(checkCusName.ID, Code);
                                                lstCustomerID.Add(checkCusName.ID);
                                            }
                                            else throw new Exception("Mã khách hàng: [" + Code + "] bị trùng");
                                        }
                                        else throw new Exception("Mã khách hàng: [" + Code + "] không tốn tại");
                                    }

                                    ServiceFactory.SVCategory((IServices.ISVCategory sv) =>
                                    {
                                        data = sv.PartnerLocation_Carrier_Export(lstCustomerID);
                                    });

                                    lstPartner = data.lstPartner;
                                    // lstCustomer = data.lstCustomer;
                                    lstCusPartner = data.lstCusPartner;
                                    lstCusLocation = data.lstCusLocation;

                                    row = 2;
                                    while (row <= worksheet.Dimension.End.Row)
                                    {
                                        List<string> lstError = new List<string>();
                                        List<string> lstErrorLocation = new List<string>();

                                        col = 1;
                                        DTOCUSPartnerLocationResult obj = new DTOCUSPartnerLocationResult();
                                        obj.ExcelRow = row;
                                        obj.lstCusLocation = new List<DTOCustomerLocationImport>();
                                        obj.lstCusPartner = new List<DTOCustomerPartnerImport>();

                                        strCheckPartner = ExcelHelper.GetValue(worksheet, row, col); strCheckPartner = strCheckPartner.Trim();
                                        if (!string.IsNullOrEmpty(strCheckPartner))// la partner
                                        {
                                            #region partner
                                            obj.IsPartner = true;
                                            SysCode = ExcelHelper.GetValue(worksheet, row, 5); SysCode = SysCode.Trim();
                                            obj.SysCode = SysCode;
                                            if (!string.IsNullOrEmpty(SysCode))
                                            {
                                                var checkPartner = lstPartner.FirstOrDefault(c => c.Code == SysCode);
                                                if (checkPartner != null)
                                                {
                                                    obj.PartnerID = checkPartner.ID;
                                                    obj.lstLocation = checkPartner.lstLocation;
                                                    if (dicCusIDCol.Keys.Count > 0)
                                                    {
                                                        foreach (var word in dicCusIDCol)
                                                        {
                                                            DTOCustomerPartnerImport objCus = new DTOCustomerPartnerImport();
                                                            var Column = word.Value;
                                                            var Cus_id = word.Key;
                                                            string Cus_code;
                                                            dicCusIDCode.TryGetValue(Cus_id, out Cus_code);

                                                            Code = ExcelHelper.GetValue(worksheet, row, Column); Code = Code.Trim();
                                                            var CusPartnerExist = lstCusPartner.FirstOrDefault(c => c.PartnerID == checkPartner.ID && c.CustomerID == Cus_id);
                                                            if (!string.IsNullOrEmpty(Code))
                                                            {
                                                                objCus.CustomerID = Cus_id;
                                                                objCus.Code = Code;
                                                                objCus.PartnerID = checkPartner.ID;
                                                                objCus.IsSuccess = true;
                                                                obj.lstCusPartner.Add(objCus);
                                                            }
                                                            else
                                                            {
                                                                if (CusPartnerExist != null)
                                                                {
                                                                    if (CusPartnerExist.IsDelete)
                                                                    {
                                                                        objCus.CustomerID = Cus_id;
                                                                        objCus.Code = string.Empty;
                                                                        objCus.PartnerID = checkPartner.ID;
                                                                        objCus.IsSuccess = true;
                                                                        obj.lstCusPartner.Add(objCus);
                                                                    }
                                                                    else
                                                                    {
                                                                        objCus.CustomerID = Cus_id;
                                                                        objCus.Code = CusPartnerExist.Code;
                                                                        objCus.PartnerID = checkPartner.ID;
                                                                        objCus.IsSuccess = false;
                                                                        objCus.IsDelete = true;
                                                                        obj.lstCusPartner.Add(objCus);
                                                                        lstError.Add("Mã của [" + Cus_code + "] không được xóa");
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                                else
                                                    lstError.Add("Mã hệ thống [" + SysCode + "] không tồn tại");
                                            }
                                            else
                                                lstError.Add("Thiếu mã hệ thống");
                                            #endregion
                                        }
                                        else//location
                                        {
                                            #region location
                                            strCheckBreak = ExcelHelper.GetValue(worksheet, row, 2);
                                            if (!string.IsNullOrEmpty(strCheckBreak))
                                            {
                                                obj.IsPartner = false;
                                                SysCode = ExcelHelper.GetValue(worksheet, row, 5); SysCode = SysCode.Trim();
                                                obj.SysCode = SysCode;
                                                if (!string.IsNullOrEmpty(SysCode))
                                                {
                                                    var LastPartner = result.LastOrDefault(c => c.IsPartner == true);
                                                    if (LastPartner == null) lstError.Add("Không tìm thấy partner cho địa điểm ");
                                                    else
                                                    {
                                                        #region check
                                                        obj.PartnerID = LastPartner.PartnerID;
                                                        var checkLO = LastPartner.lstLocation.FirstOrDefault(c => c.PartnerCode == SysCode);
                                                        if (checkLO != null)
                                                        {
                                                            if (dicCusIDCol.Keys.Count > 0)
                                                            {
                                                                foreach (var word in dicCusIDCol)
                                                                {
                                                                    DTOCustomerLocationImport objLo = new DTOCustomerLocationImport();
                                                                    var Column = word.Value;
                                                                    var Cus_id = word.Key;
                                                                    string Cus_code;
                                                                    dicCusIDCode.TryGetValue(Cus_id, out Cus_code);

                                                                    Code = ExcelHelper.GetValue(worksheet, row, Column); Code = Code.Trim();
                                                                    var CusLocationExist = lstCusLocation.FirstOrDefault(c => c.CustomerID == Cus_id && c.PartnerID == LastPartner.PartnerID && c.LocationID == checkLO.ID);
                                                                    if (!string.IsNullOrEmpty(Code))
                                                                    {
                                                                        if (CusLocationExist != null)
                                                                            objLo.ID = CusLocationExist.ID;
                                                                        else objLo.ID = 0;
                                                                        objLo.LocationID = checkLO.ID;
                                                                        objLo.PartnerID = LastPartner.PartnerID;
                                                                        objLo.Code = Code;
                                                                        objLo.CustomerID = Cus_id;
                                                                        objLo.IsSuccess = true;
                                                                        //check trùng trong database
                                                                        var check = lstCusLocation.Where(c => c.CustomerID == Cus_id && c.PartnerID == LastPartner.PartnerID && c.LocationID != checkLO.ID && c.Code == Code).FirstOrDefault();
                                                                        if (check != null)
                                                                        {
                                                                            objLo.IsSuccess = false;
                                                                            lstError.Add("Mã [" + Code + "] của [" + SysCode + "]-KH [" + Cus_code + "] đã sử dụng");
                                                                        }
                                                                        else
                                                                        {
                                                                            var lstLoInPartner = result.Where(c => c.PartnerID == LastPartner.PartnerID && c.IsPartner == false).SelectMany(c => c.lstCusLocation, (c, d) => d).ToList();
                                                                            var checkDup = lstLoInPartner.Where(c => c.CustomerID == Cus_id && c.Code == Code).FirstOrDefault();
                                                                            if (checkDup != null)
                                                                            {
                                                                                objLo.IsSuccess = false;
                                                                                lstError.Add("Mã [" + Code + "] của [" + SysCode + "]-KH [" + Cus_code + "] bị trùng trong cùng đối tác");
                                                                            }
                                                                        }
                                                                        obj.lstCusLocation.Add(objLo);
                                                                    }
                                                                    else
                                                                    {
                                                                        if (CusLocationExist != null)
                                                                        {
                                                                            if (CusLocationExist.IsDelete)
                                                                            {
                                                                                objLo.ID = CusLocationExist.ID;
                                                                                objLo.LocationID = checkLO.ID;
                                                                                objLo.PartnerID = LastPartner.PartnerID;
                                                                                objLo.Code = string.Empty;
                                                                                objLo.CustomerID = Cus_id;
                                                                                objLo.IsSuccess = true;
                                                                                obj.lstCusLocation.Add(objLo);
                                                                            }
                                                                            else
                                                                            {
                                                                                objLo.ID = CusLocationExist.ID;
                                                                                objLo.LocationID = checkLO.ID;
                                                                                objLo.PartnerID = LastPartner.PartnerID;
                                                                                objLo.Code = CusLocationExist.Code;
                                                                                objLo.IsSuccess = false;
                                                                                objLo.IsDelete = true;
                                                                                objLo.CustomerID = Cus_id;
                                                                                obj.lstCusLocation.Add(objLo);
                                                                                lstError.Add("Mã của [" + Cus_code + "] không được xóa");
                                                                            }
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                        }
                                                        else lstError.Add("Mã hệ thống [" + SysCode + "] không tồn tại");
                                                        #endregion
                                                    }
                                                }
                                                else
                                                    lstError.Add("Thiếu mã hệ thống");
                                            }
                                            else
                                            {
                                                //neu o thu 2 cung rong thi thoat ko doc nua
                                                break;
                                            }
                                            #endregion
                                        }

                                        if (lstError.Count > 0) { obj.ExcelSuccess = false; obj.ExcelError = string.Join(", ", lstError); }
                                        else { obj.ExcelSuccess = true; obj.ExcelError = string.Empty; }
                                        result.Add(obj);
                                        row++;
                                    }
                                }
                                else throw new Exception("Thiếu dữ liệu khách hàng");
                            }
                        }
                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #endregion

        #endregion

        #region CATConstraint

        #region Constraint

        public DTOResult CATConstraint_List(dynamic d)
        {
            try
            {
                string request = d.request.ToString();
                var result = default(DTOResult);
                ServiceFactory.SVCategory((IServices.ISVCategory sv) =>
                {
                    result = sv.CATConstraint_List(request);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DTOCATConstraint CATConstraint_Get(dynamic d)
        {
            try
            {
                int id = d.id;
                var result = default(DTOCATConstraint);
                ServiceFactory.SVCategory((IServices.ISVCategory sv) =>
                {
                    result = sv.CATConstraint_Get(id);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public int CATConstraint_Save(dynamic d)
        {
            try
            {
                DTOCATConstraint item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOCATConstraint>(d.item.ToString());
                int result = -1;
                ServiceFactory.SVCategory((IServices.ISVCategory sv) =>
                {
                    result = sv.CATConstraint_Save(item);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void CATConstraint_UpdateConstraint(dynamic d)
        {
            try
            {
                List<int> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(d.lst.ToString());
                ServiceFactory.SVCategory((IServices.ISVCategory sv) =>
                {
                    sv.CATConstraint_UpdateConstraint(lst);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public bool CATConstraint_Delete(dynamic d)
        {
            try
            {
                DTOCATConstraint item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOCATConstraint>(d.item.ToString());
                ServiceFactory.SVCategory((IServices.ISVCategory sv) =>
                {
                    sv.CATConstraint_Delete(item);
                });
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region ConstraintAllocation

        public DTOResult CATConstraint_Route_List(dynamic d)
        {
            try
            {
                string request = d.request.ToString();
                int id = d.id;
                var result = default(DTOResult);
                ServiceFactory.SVCategory((IServices.ISVCategory sv) =>
                {
                    result = sv.CATConstraint_Route_List(request, id);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public void CATConstraint_RouteNotIn_Save(dynamic d)
        {
            try
            {
                List<int> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(d.lst.ToString());
                int id = d.id;
                ServiceFactory.SVCategory((IServices.ISVCategory sv) =>
                {
                    sv.CATConstraint_RouteNotIn_Save(lst, id);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public bool CATConstraint_Route_Delete(dynamic d)
        {
            try
            {
                DTOCATConstraintAllocation item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOCATConstraintAllocation>(d.item.ToString());
                ServiceFactory.SVCategory((IServices.ISVCategory sv) =>
                {
                    sv.CATConstraint_Route_Delete(item);
                });
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DTOResult CATConstraint_RouteNotIn_List(dynamic d)
        {
            try
            {
                string request = d.request.ToString();
                int id = d.id;
                var result = default(DTOResult);
                ServiceFactory.SVCategory((IServices.ISVCategory sv) =>
                {
                    result = sv.CATConstraint_RouteNotIn_List(request, id);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DTOResult CATConstraint_Location_List(dynamic d)
        {
            try
            {
                string request = d.request.ToString();
                int id = d.id;
                var result = default(DTOResult);
                ServiceFactory.SVCategory((IServices.ISVCategory sv) =>
                {
                    result = sv.CATConstraint_Location_List(request, id);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public void CATConstraint_LocationNotIn_Save(dynamic d)
        {
            try
            {
                List<int> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(d.lst.ToString());
                int id = d.id;
                ServiceFactory.SVCategory((IServices.ISVCategory sv) =>
                {
                    sv.CATConstraint_LocationNotIn_Save(lst, id);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public void CATConstraint_Location_Delete(dynamic d)
        {
            try
            {
                DTOCATConstraintAllocation item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOCATConstraintAllocation>(d.item.ToString());
                ServiceFactory.SVCategory((IServices.ISVCategory sv) =>
                {
                    sv.CATConstraint_Location_Delete(item);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DTOResult CATConstraint_LocationNotIn_List(dynamic d)
        {
            try
            {
                string request = d.request.ToString();
                int id = d.id;
                var result = default(DTOResult);
                ServiceFactory.SVCategory((IServices.ISVCategory sv) =>
                {
                    result = sv.CATConstraint_LocationNotIn_List(request, id);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOResult CATConstraint_Truck_List(dynamic d)
        {
            try
            {
                string request = d.request.ToString();
                int id = d.id;
                var result = default(DTOResult);
                ServiceFactory.SVCategory((IServices.ISVCategory sv) =>
                {
                    result = sv.CATConstraint_Vehicle_List(request, id, SYSVarType.TypeOfAssetTruck);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public DTOResult CATConstraint_Tractor_List(dynamic d)
        {
            try
            {
                string request = d.request.ToString();
                int id = d.id;
                var result = default(DTOResult);
                ServiceFactory.SVCategory((IServices.ISVCategory sv) =>
                {
                    result = sv.CATConstraint_Vehicle_List(request, id, SYSVarType.TypeOfAssetTractor);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public void CATConstraint_VehicleNotIn_Save(dynamic d)
        {
            try
            {
                List<int> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(d.lst.ToString());
                int id = d.id;
                ServiceFactory.SVCategory((IServices.ISVCategory sv) =>
                {
                    sv.CATConstraint_VehicleNotIn_Save(lst, id);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public void CATConstraint_Vehicle_Delete(dynamic d)
        {
            try
            {
                DTOCATConstraintAllocation item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOCATConstraintAllocation>(d.item.ToString());
                ServiceFactory.SVCategory((IServices.ISVCategory sv) =>
                {
                    sv.CATConstraint_Vehicle_Delete(item);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public DTOResult CATConstraint_TruckNotIn_List(dynamic d)
        {
            try
            {
                string request = d.request.ToString();
                int id = d.id;
                var result = default(DTOResult);
                ServiceFactory.SVCategory((IServices.ISVCategory sv) =>
                {
                    result = sv.CATConstraint_VehicleNotIn_List(request, id, SYSVarType.TypeOfAssetTruck);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public DTOResult CATConstraint_TractorNotIn_List(dynamic d)
        {
            try
            {
                string request = d.request.ToString();
                int id = d.id;
                var result = default(DTOResult);
                ServiceFactory.SVCategory((IServices.ISVCategory sv) =>
                {
                    result = sv.CATConstraint_VehicleNotIn_List(request, id, SYSVarType.TypeOfAssetTractor);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region ConstraintRequire
        [HttpPost]
        public DTOResult CATConstraint_OpenHour_List(dynamic d)
        {
            try
            {
                string request = d.request.ToString();
                int id = d.id;
                var result = default(DTOResult);
                ServiceFactory.SVCategory((IServices.ISVCategory sv) =>
                {
                    result = sv.CATConstraint_OpenHour_List(request, id);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public DTOCATConstraintRequire CATConstraint_OpenHour_Get(dynamic d)
        {
            try
            {
                int id = d.id;
                var result = default(DTOCATConstraintRequire);
                ServiceFactory.SVCategory((IServices.ISVCategory sv) =>
                {
                    result = sv.CATConstraint_OpenHour_Get(id);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public void CATConstraint_OpenHour_Save(dynamic d)
        {
            try
            {
                DTOCATConstraintRequire item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOCATConstraintRequire>(d.item.ToString());
                int id = d.id;
                ServiceFactory.SVCategory((IServices.ISVCategory sv) =>
                {
                    sv.CATConstraint_OpenHour_Save(item, id);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public void CATConstraint_OpenHour_Delete(dynamic d)
        {
            try
            {
                DTOCATConstraintRequire item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOCATConstraintRequire>(d.item.ToString());
                ServiceFactory.SVCategory((IServices.ISVCategory sv) =>
                {
                    sv.CATConstraint_OpenHour_Delete(item);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOResult CATConstraint_Weight_List(dynamic d)
        {
            try
            {
                string request = d.request.ToString();
                int id = d.id;
                var result = default(DTOResult);
                ServiceFactory.SVCategory((IServices.ISVCategory sv) =>
                {
                    result = sv.CATConstraint_Weight_List(request, id);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public DTOCATConstraintRequire CATConstraint_Weight_Get(dynamic d)
        {
            try
            {
                int id = d.id;
                var result = default(DTOCATConstraintRequire);
                ServiceFactory.SVCategory((IServices.ISVCategory sv) =>
                {
                    result = sv.CATConstraint_Weight_Get(id);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public void CATConstraint_Weight_Save(dynamic d)
        {
            try
            {
                DTOCATConstraintRequire item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOCATConstraintRequire>(d.item.ToString());
                int id = d.id;
                ServiceFactory.SVCategory((IServices.ISVCategory sv) =>
                {
                    sv.CATConstraint_Weight_Save(item, id);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public void CATConstraint_Weight_Delete(dynamic d)
        {
            try
            {
                DTOCATConstraintRequire item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOCATConstraintRequire>(d.item.ToString());
                ServiceFactory.SVCategory((IServices.ISVCategory sv) =>
                {
                    sv.CATConstraint_Weight_Delete(item);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOResult CATConstraint_KM_List(dynamic d)
        {
            try
            {
                string request = d.request.ToString();
                int id = d.id;
                var result = default(DTOResult);
                ServiceFactory.SVCategory((IServices.ISVCategory sv) =>
                {
                    result = sv.CATConstraint_KM_List(request, id);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public DTOCATConstraintRequire CATConstraint_KM_Get(dynamic d)
        {
            try
            {
                int id = d.id;
                var result = default(DTOCATConstraintRequire);
                ServiceFactory.SVCategory((IServices.ISVCategory sv) =>
                {
                    result = sv.CATConstraint_KM_Get(id);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public void CATConstraint_KM_Save(dynamic d)
        {
            try
            {
                DTOCATConstraintRequire item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOCATConstraintRequire>(d.item.ToString());
                int id = d.id;
                ServiceFactory.SVCategory((IServices.ISVCategory sv) =>
                {
                    sv.CATConstraint_KM_Save(item, id);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public void CATConstraint_KM_Delete(dynamic d)
        {
            try
            {
                DTOCATConstraintRequire item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOCATConstraintRequire>(d.item.ToString());
                ServiceFactory.SVCategory((IServices.ISVCategory sv) =>
                {
                    sv.CATConstraint_KM_Delete(item);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #endregion

        #region CATDistributor
        #region Main
        public DTOResult CATDistributor_Customer_Read()
        {
            try
            {
                var result = default(DTOResult);
                ServiceFactory.SVCategory((IServices.ISVCategory sv) =>
                {
                    result = sv.Customer_List();
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DTOResult CATDistributor_DistributorCustom_Read()
        {
            try
            {
                var result = default(DTOResult);
                ServiceFactory.SVCategory((IServices.ISVCategory sv) =>
                {
                    result = sv.DistributorCustom_List();
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DTOPartnerLocationResult CATDistributor_DistributorCustomer_List(dynamic d)
        {
            try
            {
                List<int> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(d.lst.ToString());
                var result = default(DTOPartnerLocationResult);
                if (lst == null)
                    lst = new List<int>();
                ServiceFactory.SVCategory((IServices.ISVCategory sv) =>
                {
                    result = sv.DistributorCustomer_List(lst);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public void CATDistributor_DistributorCustom_SaveList(dynamic d)
        {
            try
            {
                DTOCATDistributorCustom item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOCATDistributorCustom>(d.item.ToString());
                ServiceFactory.SVCategory((IServices.ISVCategory sv) =>
                {
                    sv.DistributorCustom_Save(item);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOCATDistributor CATDistributor_Distributor_Get(dynamic d)
        {
            try
            {
                int id = d.id;
                var result = new DTOCATDistributor();
                ServiceFactory.SVCategory((IServices.ISVCategory sv) =>
                {
                    result = sv.Distributor_Get(id);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public bool CATDistributor_Distributor_Update(dynamic d)
        {
            try
            {
                DTOCATDistributor item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOCATDistributor>(d.item.ToString());
                ServiceFactory.SVCategory((IServices.ISVCategory sv) =>
                {
                    item.ID = sv.Distributor_Save(item);
                });
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public bool CATDistributor_Distributor_Destroy(dynamic d)
        {
            try
            {
                DTOCATDistributor item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOCATDistributor>(d.item.ToString());
                if (item != null)
                {
                    ServiceFactory.SVCategory((IServices.ISVCategory sv) =>
                    {
                        sv.Distributor_Delete(item);
                    });
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Location
        public DTOResult CATDistributor_Location_Read(dynamic d)
        {
            try
            {
                string request = d.request.ToString();
                int partnerid = (int)d.partnerid;
                var result = default(DTOResult);
                ServiceFactory.SVCategory((IServices.ISVCategory sv) =>
                {
                    result = sv.LocationInDistributor_List(request, partnerid);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public DTOCATLocationInPartner CATDistributor_Location_Get(dynamic d)
        {
            try
            {
                int id = (int)d.id;
                DTOCATLocationInPartner result = new DTOCATLocationInPartner();
                ServiceFactory.SVCategory((IServices.ISVCategory sv) =>
                {
                    result = sv.LocationInDistributor_Get(id);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public bool CATDistributor_Location_Update(dynamic d)
        {
            try
            {
                DTOCATLocationInPartner item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOCATLocationInPartner>(d.item.ToString());
                int partnerid = (int)d.partnerid;
                if (item != null)
                {
                    ServiceFactory.SVCategory((IServices.ISVCategory sv) =>
                    {
                        item = sv.LocationInDistributor_Save(item, partnerid);
                    });
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public bool CATDistributor_Location_Destroy(dynamic d)
        {
            try
            {
                DTOCATLocationInPartner item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOCATLocationInPartner>(d.item.ToString());
                if (item != null && ModelState.IsValid)
                {
                    ServiceFactory.SVCategory((IServices.ISVCategory sv) =>
                    {
                        sv.LocationInDistributor_Delete(item);
                    });
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Location Not In
        [HttpPost]
        public DTOResult CATDistributor_LocationNotIn_Read(dynamic d)
        {
            try
            {
                string request = d.request.ToString();
                int partnerid = (int)d.partnerid;
                var result = default(DTOResult);
                ServiceFactory.SVCategory((IServices.ISVCategory sv) =>
                {
                    result = sv.LocationNotInDistributor_List(request, partnerid);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public bool CATDistributor_LocationNotIn_SaveList(dynamic d)
        {
            try
            {
                List<DTOCATLocationInPartner> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DTOCATLocationInPartner>>(d.lst.ToString());
                int partnerid = (int)d.partnerid;
                if (lst != null)
                {
                    ServiceFactory.SVCategory((IServices.ISVCategory sv) =>
                    {
                        sv.LocationNotInDistributor_SaveList(lst, partnerid);
                    });

                    var lstAddress = new List<AddressSearchItem>();
                    ServiceFactory.SVCategory((ISVCategory sv) =>
                    {
                        lstAddress = sv.AddressSearch_List();
                    });
                    //var obj = lst.Where(c => c.CUSLocationID == 10616).ToList();
                    lstAddress = lstAddress.Where(c => c.Address != null && c.PartnerCode != null).ToList();
                    AddressSearchHelper.Create(lstAddress);
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Import Export

        #region imex
        public string CATDistributor_Export_GetData()
        {
            try
            {
                List<DTOCATPartnerImport> result = new List<DTOCATPartnerImport>();
                ServiceFactory.SVCategory((IServices.ISVCategory sv) =>
                {
                    result = sv.Distributor_Export();
                });
                //string file = Path.Combine(Server.MapPath("~/" + FolderUpload.Export) + "ExportDistributor_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx");
                string file = "/" + FolderUpload.Export + "ExportDistributor_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx";

                if (System.IO.File.Exists(HttpContext.Current.Server.MapPath(file)))
                    System.IO.File.Delete(HttpContext.Current.Server.MapPath(file));
                FileInfo exportfile = new FileInfo(HttpContext.Current.Server.MapPath(file));
                using (ExcelPackage package = new ExcelPackage(exportfile))
                {
                    // Sheet 1
                    ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Sheet 1");

                    int col = 1, row = 1, sttlv1 = 1, sttlv2 = 1;
                    #region header
                    worksheet.Cells[row, col].Value = "STT";
                    col++; worksheet.Cells[row, col].Value = "Mã hệ thống"; worksheet.Column(col).Width = 20;
                    col++; worksheet.Cells[row, col].Value = "Mã địa chỉ"; worksheet.Column(col).Width = 20;
                    col++; worksheet.Cells[row, col].Value = "Tên nhà phân phối"; worksheet.Column(col).Width = 15;
                    col++; worksheet.Cells[row, col].Value = "Địa chỉ giao hàng"; worksheet.Column(col).Width = 15;
                    col++; worksheet.Cells[row, col].Value = "Tỉnh thành"; worksheet.Column(col).Width = 15;
                    col++; worksheet.Cells[row, col].Value = "Quận/Huyện"; worksheet.Column(col).Width = 15;
                    col++; worksheet.Cells[row, col].Value = "Khu công nghiệp"; worksheet.Column(col).Width = 15;
                    col++; worksheet.Cells[row, col].Value = "SĐT"; worksheet.Column(col).Width = 15;
                    col++; worksheet.Cells[row, col].Value = "Fax"; worksheet.Column(col).Width = 15;
                    col++; worksheet.Cells[row, col].Value = "Email"; worksheet.Column(col).Width = 15;
                    col++; worksheet.Cells[row, col].Value = "Kinh độ"; worksheet.Column(col).Width = 15;
                    col++; worksheet.Cells[row, col].Value = "Vĩ độ"; worksheet.Column(col).Width = 15;
                    col++; worksheet.Cells[row, col].Value = "Loại"; worksheet.Column(col).Width = 15;

                    ExcelHelper.CreateCellStyle(worksheet, row, 1, row, col, false, true, ExcelHelper.ColorGreen, ExcelHelper.ColorWhite, 0, "");

                    //worksheet.Cells[row, 1, row, col].Style.Font.Bold = true;
                    worksheet.Cells[row, 1, row, col].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    worksheet.Cells[row, 1, row, col].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    //worksheet.Cells[row, 1, row, col].Style.Font.Color.SetColor(Color.White);
                    //worksheet.Cells[row, 1, row, col].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    //worksheet.Cells[row, 1, row, col].Style.Fill.BackgroundColor.SetColor(Color.Green);
                    #endregion

                    #region data

                    if (result.Count > 0)
                    {
                        col = 1;
                        row = 2;
                        foreach (var item in result)
                        {
                            col = 1;
                            worksheet.Cells[row, col].Value = sttlv1;
                            col++; worksheet.Cells[row, col].Value = item.Code;
                            col++; worksheet.Cells[row, col].Value = string.Empty;
                            col++; worksheet.Cells[row, col].Value = item.PartnerName;
                            col++; worksheet.Cells[row, col].Value = item.Address;
                            col++; worksheet.Cells[row, col].Value = item.ProvinceName;
                            col++; worksheet.Cells[row, col].Value = item.DistrictName;
                            col++; worksheet.Cells[row, col].Value = string.Empty;
                            col++; worksheet.Cells[row, col].Value = item.TelNo;
                            col++; worksheet.Cells[row, col].Value = item.Fax;
                            col++; worksheet.Cells[row, col].Value = item.Email;
                            col++; worksheet.Cells[row, col].Value = string.Empty;
                            col++; worksheet.Cells[row, col].Value = string.Empty;
                            col++; worksheet.Cells[row, col].Value = item.GroupOfPartnerCode;

                            worksheet.Row(row).OutlineLevel = 0;

                            ExcelHelper.CreateCellStyle(worksheet, row, 1, row, col, false, false, ExcelHelper.ColorYellow, "", 0, "");
                            //worksheet.Cells[row, 1, row, col].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                            //worksheet.Cells[row, 1, row, col].Style.Fill.BackgroundColor.SetColor(Color.GreenYellow);
                            sttlv2 = 1;
                            foreach (var detail in item.lstLocation)
                            {
                                col = 1;
                                row++;
                                worksheet.Cells[row, col].Value = sttlv2;
                                col++; worksheet.Cells[row, col].Value = detail.Code;
                                col++; worksheet.Cells[row, col].Value = detail.PartnerCode;
                                col++; worksheet.Cells[row, col].Value = detail.LocationName;
                                col++; worksheet.Cells[row, col].Value = detail.Address;
                                col++; worksheet.Cells[row, col].Value = detail.ProvinceName;
                                col++; worksheet.Cells[row, col].Value = detail.DistrictName;
                                col++; worksheet.Cells[row, col].Value = detail.EconomicZone;
                                col++; worksheet.Cells[row, col].Value = string.Empty;
                                col++; worksheet.Cells[row, col].Value = string.Empty;
                                col++; worksheet.Cells[row, col].Value = string.Empty;
                                col++; worksheet.Cells[row, col].Value = detail.Lat;
                                col++; worksheet.Cells[row, col].Value = detail.Lng;

                                worksheet.Row(row).OutlineLevel = 1;
                                sttlv2++;
                            }
                            sttlv1++;
                            row++;
                        }
                    }

                    #endregion
                    if (result.Count > 0)
                    {
                        ExcelHelper.CreateCellStyle(worksheet, 2, 2, worksheet.Dimension.End.Row, 2, false, false, ExcelHelper.ColorOrange, "", 0, "");
                    }
                    for (int i = 1; i <= worksheet.Dimension.End.Row; i++)
                    {
                        for (int j = 1; j <= worksheet.Dimension.End.Column; j++)
                        {
                            worksheet.Cells[i, j].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                        }
                    }
                    package.Save();
                }
                return file;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public void CATDistributor_Excel_Save(dynamic dynPram)
        {
            try
            {
                List<DTOCATPartnerImport> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DTOCATPartnerImport>>(dynPram.lst.ToString());
                ServiceFactory.SVCategory((IServices.ISVCategory sv) =>
                {
                    sv.Distributor_Import(lst);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public List<DTOCATPartnerImport> CATDistributor_Excel_Check(dynamic dynPram)
        {
            try
            {
                CATFile file = Newtonsoft.Json.JsonConvert.DeserializeObject<CATFile>(dynPram.item.ToString());
                List<DTOCATPartnerImport> result = new List<DTOCATPartnerImport>();

                if (file != null && !string.IsNullOrEmpty(file.FilePath))
                {
                    List<DTOCATPartnerItem> listDistributor = new List<DTOCATPartnerItem>();
                    List<DTOCATLocationItem> listDistributorLocation = new List<DTOCATLocationItem>();
                    List<CATProvince> listProvince = new List<CATProvince>();
                    List<CATDistrict> listDistrict = new List<CATDistrict>();
                    List<CATGroupOfPartner> listGoPCode = new List<CATGroupOfPartner>();

                    ServiceFactory.SVCategory((IServices.ISVCategory sv) =>
                    {
                        listDistributor = sv.Distributor_AllList().Data.Cast<DTOCATPartnerItem>().ToList();
                        listDistributorLocation = sv.DistributorLocation_AllList().Data.Cast<DTOCATLocationItem>().ToList();
                        listProvince = sv.ExcelProvince_List();
                        listDistrict = sv.ExcelDistrict_List();
                        listGoPCode = sv.GroupOfPartner_List().Data.Cast<CATGroupOfPartner>().ToList();
                    });

                    using (System.IO.FileStream fs = new System.IO.FileStream(HttpContext.Current.Server.MapPath("/" + file.FilePath), System.IO.FileMode.Open, System.IO.FileAccess.Read))
                    {
                        using (var package = new ExcelPackage(fs))
                        {
                            ExcelWorksheet worksheet = ExcelHelper.GetWorksheetByIndex(package, 1);

                            int col = 2, row;
                            string Code, PartnerCode, PartnerName, Address, ProvinceName, DistrictName, Zone, TellNo, Fax, Email, Lat, Lng, GoPCode;
                            if (worksheet != null)
                            {
                                row = 2;
                                while (row <= worksheet.Dimension.End.Row)
                                {
                                    List<string> lstError = new List<string>();


                                    #region đọc dữ liệu
                                    col = 2;
                                    Code = ExcelHelper.GetValue(worksheet, row, col); Code = Code.Trim();
                                    col++; PartnerCode = ExcelHelper.GetValue(worksheet, row, col); PartnerCode = PartnerCode.Trim();
                                    col++; PartnerName = ExcelHelper.GetValue(worksheet, row, col); PartnerName = PartnerName.Trim();
                                    col++; Address = ExcelHelper.GetValue(worksheet, row, col); Address = Address.Trim();
                                    col++; ProvinceName = ExcelHelper.GetValue(worksheet, row, col); ProvinceName = ProvinceName.Trim();
                                    col++; DistrictName = ExcelHelper.GetValue(worksheet, row, col); DistrictName = DistrictName.Trim();
                                    col++; Zone = ExcelHelper.GetValue(worksheet, row, col); Zone = Zone.Trim();
                                    col++; TellNo = ExcelHelper.GetValue(worksheet, row, col); TellNo = TellNo.Trim();
                                    col++; Fax = ExcelHelper.GetValue(worksheet, row, col); Fax = Fax.Trim();
                                    col++; Email = ExcelHelper.GetValue(worksheet, row, col); Email = Email.Trim();
                                    col++; Lat = ExcelHelper.GetValue(worksheet, row, col); Lat = Lat.Trim();
                                    col++; Lng = ExcelHelper.GetValue(worksheet, row, col); Lng = Lng.Trim();
                                    col++; GoPCode = ExcelHelper.GetValue(worksheet, row, col); GoPCode = GoPCode.Trim();
                                    #endregion

                                    if (row == 2)
                                        if (!string.IsNullOrEmpty(PartnerCode))
                                            throw new Exception("Không tìm thấy Partner");

                                    if (string.IsNullOrEmpty(PartnerCode))// nut cha
                                    {
                                        DTOCATPartnerImport obj = new DTOCATPartnerImport();
                                        obj.lstLocation = new List<DTOCATLocationImport>();
                                        obj.ExcelRow = row;
                                        if (!string.IsNullOrEmpty(Code)) //
                                        {
                                            #region data parent
                                            var checkCode = listDistributor.FirstOrDefault(c => c.Code == Code);
                                            if (checkCode != null)
                                                obj.ID = checkCode.ID;
                                            else
                                                obj.ID = 0;
                                            var checkCodeOnFile = result.FirstOrDefault(c => c.Code == Code);
                                            if (checkCodeOnFile != null) lstError.Add("Mã nhà phân phối [" + Code + "] bị trùng");
                                            obj.Code = Code;
                                            obj.Address = Address;
                                            obj.PartnerName = PartnerName;
                                            obj.TelNo = TellNo;
                                            obj.Fax = Fax;
                                            obj.Email = Email;

                                            if (!string.IsNullOrEmpty(ProvinceName))
                                            {
                                                var checkProvince = listProvince.FirstOrDefault(c => c.ProvinceName == ProvinceName);

                                                if (checkProvince == null)
                                                    lstError.Add("[" + ProvinceName + "] không tồn tại trong hệ thống");
                                                else
                                                {
                                                    obj.ProvinceID = checkProvince.ID; obj.ProvinceName = checkProvince.ProvinceName;
                                                    obj.CountryID = checkProvince.CountryID; obj.CountryName = checkProvince.CountryName;
                                                }
                                            }
                                            else
                                            {
                                                obj.ProvinceID = null;
                                                obj.CountryID = null;
                                            }

                                            if (!string.IsNullOrEmpty(DistrictName))
                                            {
                                                var checkDistrict = listDistrict.FirstOrDefault(c => c.DistrictName == DistrictName);
                                                if (checkDistrict == null)
                                                    lstError.Add("[" + DistrictName + "] không tồn tại trong hệ thống");
                                                else
                                                {
                                                    obj.DistrictID = checkDistrict.ID;
                                                    obj.DistrictName = DistrictName;
                                                }
                                            }
                                            else
                                                obj.DistrictID = null;

                                            if (!string.IsNullOrEmpty(GoPCode))
                                            {
                                                var checkGoPCode = listGoPCode.FirstOrDefault(c => c.Code == GoPCode);
                                                if (checkGoPCode == null)
                                                    lstError.Add("Loại [" + GoPCode + "] không tồn tại trong hệ thống");
                                                else
                                                {
                                                    obj.GroupOfPartnerID = checkGoPCode.ID;
                                                    obj.GroupOfPartnerCode = checkGoPCode.Code;
                                                }
                                            }


                                            #endregion
                                        }
                                        if (string.IsNullOrEmpty(Code))//ko co code
                                        {
                                            obj.Code = string.Empty;
                                            obj.Address = Address;
                                            obj.PartnerName = PartnerName;
                                            obj.TelNo = TellNo;
                                            obj.Fax = Fax;
                                            obj.Email = Email;
                                            lstError.Add("Thiếu mã hãng tàu");
                                        }
                                        obj.ExcelError = string.Join(", ", lstError);
                                        if (lstError.Count > 0)
                                            obj.ExcelSuccess = false;
                                        else obj.ExcelSuccess = true;
                                        result.Add(obj);
                                    }
                                    else//nut con
                                    {
                                        DTOCATLocationImport objLocation = new DTOCATLocationImport();
                                        var obj = result.LastOrDefault();
                                        if (string.IsNullOrEmpty(obj.Code))
                                        {
                                            obj.Code = Code;
                                            obj.Address = Address;
                                            obj.PartnerName = PartnerName;
                                            obj.TelNo = TellNo;
                                            obj.Fax = Fax;
                                            obj.Email = Email;
                                            lstError.Add("Không thể xác định cha");
                                        }
                                        else
                                        {
                                            #region data lstLocation

                                            objLocation.EconomicZone = Zone;
                                            var checkSysCodeLocation = listDistributorLocation.FirstOrDefault(c => c.Code == Code);
                                            if (obj.ID > 0)//parent da co
                                            {
                                                if (checkSysCodeLocation == null)//location moi
                                                {
                                                    objLocation.ID = 0;
                                                    objLocation.Code = Code;
                                                }
                                                else//location cu
                                                {
                                                    objLocation.ID = checkSysCodeLocation.ID;
                                                    objLocation.Code = Code;
                                                }
                                            }
                                            else// parent moi
                                            {
                                                if (checkSysCodeLocation == null)
                                                {
                                                    objLocation.ID = 0;
                                                    objLocation.Code = Code;
                                                }
                                                else//location cu
                                                {
                                                    objLocation.ID = checkSysCodeLocation.ID;
                                                    objLocation.Code = Code;
                                                }
                                            }

                                            var chkOnFile = obj.lstLocation.FirstOrDefault(c => c.Code == Code);
                                            if (chkOnFile != null) lstError.Add("Mã [" + Code + "] bị trùng");


                                            var checkCodeLocation = listDistributorLocation.FirstOrDefault(c => c.PartnerCode == PartnerCode);

                                            if (obj.ID > 0)//parent da co
                                            {
                                                if (checkCodeLocation == null)//location mo
                                                    objLocation.PartnerCode = PartnerCode;
                                                else//location cu
                                                {
                                                    if (checkCodeLocation.ID == objLocation.ID)
                                                        objLocation.PartnerCode = PartnerCode;
                                                    else lstError.Add("Mã địa chỉ [" + PartnerCode + "] Đã sử dụng");
                                                }
                                            }
                                            else// parent moi
                                            {
                                                if (checkCodeLocation == null)
                                                    objLocation.PartnerCode = PartnerCode;
                                                else lstError.Add("Mã địa chỉ [" + PartnerCode + "] Đã sử dụng");
                                            }

                                            var chkPartnerCodeOnFile = result.Count(c => c.lstLocation.Any(d => d.PartnerCode == PartnerCode)) > 0;
                                            if (chkPartnerCodeOnFile) lstError.Add("Mã địa chỉ [" + PartnerCode + "] Đã bị trùng");

                                            objLocation.LocationName = PartnerName;
                                            objLocation.Address = Address;

                                            if (!string.IsNullOrEmpty(ProvinceName))
                                            {
                                                var checkProvinceLocation = listProvince.FirstOrDefault(c => c.ProvinceName == ProvinceName);
                                                if (checkProvinceLocation == null)
                                                    lstError.Add("[" + ProvinceName + "] không tồn tại trong hệ thống");
                                                else
                                                {
                                                    objLocation.ProvinceID = checkProvinceLocation.ID;
                                                    objLocation.ProvinceName = checkProvinceLocation.ProvinceName;
                                                    objLocation.CountryID = checkProvinceLocation.CountryID;
                                                    objLocation.CountryName = checkProvinceLocation.CountryName;
                                                }
                                            }
                                            else lstError.Add("[Tỉnh thành] không được trống");

                                            if (!string.IsNullOrEmpty(DistrictName))
                                            {
                                                var checkDistrictLocation = listDistrict.FirstOrDefault(c => c.DistrictName == DistrictName);
                                                if (checkDistrictLocation == null)
                                                    lstError.Add("[" + DistrictName + "] không tồn tại trong hệ thống");
                                                else
                                                {
                                                    objLocation.DistrictID = checkDistrictLocation.ID;
                                                    objLocation.DistrictName = checkDistrictLocation.DistrictName;
                                                }
                                            }
                                            else lstError.Add("[Quận huyện] không được trống");
                                            if (string.IsNullOrEmpty(Lat))
                                                objLocation.Lat = null;
                                            else
                                            {
                                                try
                                                {
                                                    objLocation.Lat = Convert.ToDouble(Lat);
                                                }
                                                catch
                                                {
                                                    lstError.Add("[Vĩ độ] không chính xác");
                                                }
                                            }

                                            if (string.IsNullOrEmpty(Lng))
                                                objLocation.Lng = null;
                                            else
                                            {
                                                try
                                                {
                                                    objLocation.Lng = Convert.ToDouble(Lng);
                                                }
                                                catch
                                                {
                                                    lstError.Add("[Kinh độ] không chính xác");
                                                }
                                            }

                                            #endregion
                                        }

                                        objLocation.IsSuccess = true; objLocation.Error = string.Empty;
                                        if (lstError.Count > 0) { objLocation.IsSuccess = false; objLocation.Error = string.Join(" ,", lstError); }

                                        if (!obj.ExcelSuccess) { objLocation.IsSuccess = false; }
                                        if (!objLocation.IsSuccess)
                                        {
                                            if (string.IsNullOrEmpty(obj.ExcelError)) obj.ExcelError = objLocation.Error;
                                            else obj.ExcelError = obj.ExcelError + ", " + objLocation.Error;
                                        }
                                        obj.lstLocation.Add(objLocation);
                                    }
                                    row++;
                                }
                            }
                        }
                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region phân bổ mã
        public string CATDistributor_Excel_ExportCode(dynamic dynPram)
        {
            try
            {
                List<int> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynPram.lst.ToString());
                DTOCUSPartnerLocationExport result = new DTOCUSPartnerLocationExport();
                ServiceFactory.SVCategory((IServices.ISVCategory sv) =>
                {
                    result = sv.PartnerLocation_Distributor_Export(lst);
                });

                string file = "/" + FolderUpload.Export + "ExportPartnerLocation_Distributor_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx";

                if (System.IO.File.Exists(HttpContext.Current.Server.MapPath(file)))
                    System.IO.File.Delete(HttpContext.Current.Server.MapPath(file));
                FileInfo exportfile = new FileInfo(HttpContext.Current.Server.MapPath(file));
                using (ExcelPackage package = new ExcelPackage(exportfile))
                {
                    // Sheet 1
                    ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Sheet 1");

                    int col = 1, row = 1, sttlv1 = 1, sttlv2 = 1;
                    #region header
                    worksheet.Cells[row, col].Value = "STT";
                    col++; worksheet.Cells[row, col].Value = "Loại"; worksheet.Column(col).Width = 15;
                    col++; worksheet.Cells[row, col].Value = "Nhà phân phối"; worksheet.Column(col).Width = 35;
                    col++; worksheet.Cells[row, col].Value = "Địa chỉ"; worksheet.Column(col).Width = 40;
                    col++; worksheet.Cells[row, col].Value = "Hệ thống"; worksheet.Column(col).Width = 15;

                    #region header dong
                    if (result.lstCustomer.Count > 0)
                    {
                        foreach (var cus in result.lstCustomer)
                        {
                            col++; worksheet.Cells[row, col].Value = cus.Code; worksheet.Column(col).Width = 15;
                        }
                    }

                    #endregion

                    ExcelHelper.CreateCellStyle(worksheet, row, 1, row, col, false, true, ExcelHelper.ColorGreen, ExcelHelper.ColorWhite, 0, "");

                    worksheet.Cells[row, 1, row, col].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    worksheet.Cells[row, 1, row, col].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                    #endregion

                    #region data

                    if (result.lstPartner.Count > 0)
                    {
                        row = 2; col = 1; sttlv1 = 1;
                        foreach (var partner in result.lstPartner)
                        {
                            col = 1;
                            worksheet.Cells[row, col].Value = sttlv1;
                            col++; worksheet.Cells[row, col].Value = partner.GroupOfPartnerName;
                            col++; worksheet.Cells[row, col].Value = partner.Name;
                            col++; worksheet.Cells[row, col].Value = partner.Address;
                            col++; worksheet.Cells[row, col].Value = partner.Code;
                            var lstCusCode = result.lstCusPartner.Where(c => c.PartnerID == partner.ID).ToList();
                            if (lstCusCode.Count > 0)
                            {
                                foreach (var cus in result.lstCustomer)
                                {
                                    var code = lstCusCode.FirstOrDefault(c => c.CustomerID == cus.ID);
                                    col++;
                                    if (code != null)
                                    {
                                        worksheet.Cells[row, col].Value = code.Code;
                                    }
                                }
                            }
                            ExcelHelper.CreateCellStyle(worksheet, row, 1, row, worksheet.Dimension.End.Column, false, false, ExcelHelper.ColorYellow, "", 0, "");
                            //worksheet.Cells[row, 1, row, worksheet.Dimension.End.Column].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                            //worksheet.Cells[row, 1, row, worksheet.Dimension.End.Column].Style.Fill.BackgroundColor.SetColor(Color.YellowGreen);
                            if (partner.lstLocation.Count > 0)
                            {
                                col = 2;
                                sttlv2 = 1;
                                foreach (var location in partner.lstLocation)
                                {
                                    col = 2;
                                    row++;
                                    worksheet.Cells[row, col].Value = sttlv2;
                                    col++; worksheet.Cells[row, col].Value = location.Name;
                                    col++; worksheet.Cells[row, col].Value = location.Address;
                                    col++; worksheet.Cells[row, col].Value = location.PartnerCode;
                                    var lstLocationCode = result.lstCusLocation.Where(c => c.LocationID == location.ID && c.PartnerID == partner.ID).ToList();
                                    if (lstLocationCode.Count > 0)
                                    {
                                        foreach (var cus in result.lstCustomer)
                                        {
                                            var code = lstLocationCode.FirstOrDefault(c => c.CustomerID == cus.ID);
                                            col++;
                                            if (code != null)
                                            {
                                                worksheet.Cells[row, col].Value = code.Code;
                                            }
                                        }
                                    }
                                    sttlv2++;
                                }
                            }
                            row++;
                            sttlv1++;
                        }
                    }

                    #endregion

                    for (int i = 1; i <= worksheet.Dimension.End.Row; i++)
                    {
                        for (int j = 1; j <= worksheet.Dimension.End.Column; j++)
                        {
                            worksheet.Cells[i, j].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                        }
                    }
                    package.Save();
                }
                return file;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void CATDistributor_Excel_SaveCode(dynamic dynParam)
        {
            try
            {
                List<DTOCUSPartnerLocationResult> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DTOCUSPartnerLocationResult>>(dynParam.lst.ToString());
                DTOCUSPartnerLocationImport data = new DTOCUSPartnerLocationImport();
                data.lstCusLocation = new List<DTOCustomerLocationImport>();
                data.lstCusPartner = new List<DTOCustomerPartnerImport>();
                foreach (var item in lst)
                {
                    if (item.IsPartner) data.lstCusPartner.AddRange(item.lstCusPartner);
                    else data.lstCusLocation.AddRange(item.lstCusLocation);
                }
                ServiceFactory.SVCategory((IServices.ISVCategory sv) =>
                {
                    sv.DistributorLocation_Import(data);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public List<DTOCUSPartnerLocationResult> CATDistributor_Excel_CheckCode(dynamic dynParam)
        {
            try
            {
                CATFile file = Newtonsoft.Json.JsonConvert.DeserializeObject<CATFile>(dynParam.item.ToString());
                List<DTOCUSPartnerLocationResult> result = new List<DTOCUSPartnerLocationResult>();

                if (file != null && !string.IsNullOrEmpty(file.FilePath))
                {
                    DTOCUSPartnerLocationExport data = new DTOCUSPartnerLocationExport();
                    List<CUSCustomer> lstCustomer = new List<CUSCustomer>();
                    List<DTOCustomerPartnerImport> lstCusPartner = new List<DTOCustomerPartnerImport>();
                    List<DTOCUSPartnerImport> lstPartner = new List<DTOCUSPartnerImport>();
                    List<DTOCustomerLocationImport> lstCusLocation = new List<DTOCustomerLocationImport>();

                    List<int> lstCustomerID = new List<int>();

                    ServiceFactory.SVCategory((IServices.ISVCategory sv) =>
                    {
                        lstCustomer = sv.Customer_List().Data.Cast<CUSCustomer>().ToList();
                    });

                    using (System.IO.FileStream fs = new System.IO.FileStream(HttpContext.Current.Server.MapPath("/" + file.FilePath), System.IO.FileMode.Open, System.IO.FileAccess.Read))
                    {
                        using (var package = new ExcelPackage(fs))
                        {
                            ExcelWorksheet worksheet = ExcelHelper.GetWorksheetByIndex(package, 1);

                            int col = 2, row;
                            string Code, SysCode, strCheckPartner, strCheckBreak;
                            if (worksheet != null)
                            {
                                row = 1;
                                if (worksheet.Dimension.End.Column >= 6)
                                {
                                    Dictionary<int, int> dicCusIDCol = new Dictionary<int, int>();
                                    Dictionary<int, string> dicCusIDCode = new Dictionary<int, string>();
                                    for (int i = 6; i <= worksheet.Dimension.End.Column; i++)
                                    {
                                        Code = ExcelHelper.GetValue(worksheet, row, i);
                                        Code = Code.Trim();
                                        var checkCusName = lstCustomer.FirstOrDefault(c => c.Code == Code);
                                        if (checkCusName != null)
                                        {
                                            if (!dicCusIDCode.ContainsValue(Code))
                                            {
                                                dicCusIDCol.Add(checkCusName.ID, i);
                                                dicCusIDCode.Add(checkCusName.ID, Code);
                                                lstCustomerID.Add(checkCusName.ID);
                                            }
                                            else throw new Exception("Mã khách hàng: [" + Code + "] bị trùng");
                                        }
                                        else throw new Exception("Mã khách hàng: [" + Code + "] không tốn tại");
                                    }

                                    ServiceFactory.SVCategory((IServices.ISVCategory sv) =>
                                    {
                                        data = sv.PartnerLocation_Distributor_Export(lstCustomerID);
                                    });

                                    lstPartner = data.lstPartner;
                                    // lstCustomer = data.lstCustomer;
                                    lstCusPartner = data.lstCusPartner;
                                    lstCusLocation = data.lstCusLocation;

                                    row = 2;
                                    while (row <= worksheet.Dimension.End.Row)
                                    {
                                        List<string> lstError = new List<string>();
                                        List<string> lstErrorLocation = new List<string>();

                                        col = 1;
                                        DTOCUSPartnerLocationResult obj = new DTOCUSPartnerLocationResult();
                                        obj.ExcelRow = row;
                                        obj.lstCusLocation = new List<DTOCustomerLocationImport>();
                                        obj.lstCusPartner = new List<DTOCustomerPartnerImport>();

                                        strCheckPartner = ExcelHelper.GetValue(worksheet, row, col); strCheckPartner = strCheckPartner.Trim();
                                        if (!string.IsNullOrEmpty(strCheckPartner))// la partner
                                        {
                                            #region partner
                                            obj.IsPartner = true;
                                            SysCode = ExcelHelper.GetValue(worksheet, row, 5); SysCode = SysCode.Trim();
                                            obj.SysCode = SysCode;
                                            if (!string.IsNullOrEmpty(SysCode))
                                            {
                                                var checkPartner = lstPartner.FirstOrDefault(c => c.Code == SysCode);
                                                if (checkPartner != null)
                                                {
                                                    obj.PartnerID = checkPartner.ID;
                                                    obj.lstLocation = checkPartner.lstLocation;
                                                    if (dicCusIDCol.Keys.Count > 0)
                                                    {
                                                        foreach (var word in dicCusIDCol)
                                                        {
                                                            DTOCustomerPartnerImport objCus = new DTOCustomerPartnerImport();
                                                            var Column = word.Value;
                                                            var Cus_id = word.Key;
                                                            string Cus_code;
                                                            dicCusIDCode.TryGetValue(Cus_id, out Cus_code);

                                                            Code = ExcelHelper.GetValue(worksheet, row, Column); Code = Code.Trim();
                                                            var CusPartnerExist = lstCusPartner.FirstOrDefault(c => c.PartnerID == checkPartner.ID && c.CustomerID == Cus_id);
                                                            if (!string.IsNullOrEmpty(Code))
                                                            {
                                                                objCus.CustomerID = Cus_id;
                                                                objCus.Code = Code;
                                                                objCus.PartnerID = checkPartner.ID;
                                                                objCus.IsSuccess = true;
                                                                obj.lstCusPartner.Add(objCus);
                                                            }
                                                            else
                                                            {
                                                                if (CusPartnerExist != null)
                                                                {
                                                                    if (CusPartnerExist.IsDelete)
                                                                    {
                                                                        objCus.CustomerID = Cus_id;
                                                                        objCus.Code = string.Empty;
                                                                        objCus.PartnerID = checkPartner.ID;
                                                                        objCus.IsSuccess = true;
                                                                        obj.lstCusPartner.Add(objCus);
                                                                    }
                                                                    else
                                                                    {
                                                                        objCus.CustomerID = Cus_id;
                                                                        objCus.Code = CusPartnerExist.Code;
                                                                        objCus.PartnerID = checkPartner.ID;
                                                                        objCus.IsSuccess = false;
                                                                        objCus.IsDelete = true;
                                                                        obj.lstCusPartner.Add(objCus);
                                                                        lstError.Add("Mã của [" + Cus_code + "] không được xóa");
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                                else
                                                    lstError.Add("Mã hệ thống [" + SysCode + "] không tồn tại");
                                            }
                                            else
                                                lstError.Add("Thiếu mã hệ thống");
                                            #endregion
                                        }
                                        else//location
                                        {
                                            #region location
                                            strCheckBreak = ExcelHelper.GetValue(worksheet, row, 2);
                                            if (!string.IsNullOrEmpty(strCheckBreak))
                                            {
                                                obj.IsPartner = false;
                                                SysCode = ExcelHelper.GetValue(worksheet, row, 5); SysCode = SysCode.Trim();
                                                obj.SysCode = SysCode;
                                                if (!string.IsNullOrEmpty(SysCode))
                                                {
                                                    var LastPartner = result.LastOrDefault(c => c.IsPartner == true);
                                                    if (LastPartner == null) lstError.Add("Không tìm thấy partner cho địa điểm ");
                                                    else
                                                    {
                                                        #region check
                                                        obj.PartnerID = LastPartner.PartnerID;
                                                        var checkLO = LastPartner.lstLocation.FirstOrDefault(c => c.PartnerCode == SysCode);
                                                        if (checkLO != null)
                                                        {
                                                            if (dicCusIDCol.Keys.Count > 0)
                                                            {
                                                                foreach (var word in dicCusIDCol)
                                                                {
                                                                    DTOCustomerLocationImport objLo = new DTOCustomerLocationImport();
                                                                    var Column = word.Value;
                                                                    var Cus_id = word.Key;
                                                                    string Cus_code;
                                                                    dicCusIDCode.TryGetValue(Cus_id, out Cus_code);

                                                                    Code = ExcelHelper.GetValue(worksheet, row, Column); Code = Code.Trim();
                                                                    var CusLocationExist = lstCusLocation.FirstOrDefault(c => c.CustomerID == Cus_id && c.PartnerID == LastPartner.PartnerID && c.LocationID == checkLO.ID);
                                                                    if (!string.IsNullOrEmpty(Code))
                                                                    {
                                                                        if (CusLocationExist != null)
                                                                            objLo.ID = CusLocationExist.ID;
                                                                        else objLo.ID = 0;
                                                                        objLo.LocationID = checkLO.ID;
                                                                        objLo.PartnerID = LastPartner.PartnerID;
                                                                        objLo.Code = Code;
                                                                        objLo.CustomerID = Cus_id;
                                                                        objLo.IsSuccess = true;
                                                                        //check trùng trong database
                                                                        var check = lstCusLocation.Where(c => c.CustomerID == Cus_id && c.PartnerID == LastPartner.PartnerID && c.LocationID != checkLO.ID && c.Code == Code).FirstOrDefault();
                                                                        if (check != null)
                                                                        {
                                                                            objLo.IsSuccess = false;
                                                                            lstError.Add("Mã [" + Code + "] của [" + SysCode + "]-KH [" + Cus_code + "] đã sử dụng");
                                                                        }
                                                                        else
                                                                        {
                                                                            var lstLoInPartner = result.Where(c => c.PartnerID == LastPartner.PartnerID && c.IsPartner == false).SelectMany(c => c.lstCusLocation, (c, d) => d).ToList();
                                                                            var checkDup = lstLoInPartner.Where(c => c.CustomerID == Cus_id && c.Code == Code).FirstOrDefault();
                                                                            if (checkDup != null)
                                                                            {
                                                                                objLo.IsSuccess = false;
                                                                                lstError.Add("Mã [" + Code + "] của [" + SysCode + "]-KH [" + Cus_code + "] bị trùng trong cùng đối tác");
                                                                            }
                                                                        }
                                                                        obj.lstCusLocation.Add(objLo);
                                                                    }
                                                                    else
                                                                    {
                                                                        if (CusLocationExist != null)
                                                                        {
                                                                            if (CusLocationExist.IsDelete)
                                                                            {
                                                                                objLo.ID = CusLocationExist.ID;
                                                                                objLo.LocationID = checkLO.ID;
                                                                                objLo.PartnerID = LastPartner.PartnerID;
                                                                                objLo.Code = string.Empty;
                                                                                objLo.CustomerID = Cus_id;
                                                                                objLo.IsSuccess = true;
                                                                                obj.lstCusLocation.Add(objLo);
                                                                            }
                                                                            else
                                                                            {
                                                                                objLo.ID = CusLocationExist.ID;
                                                                                objLo.LocationID = checkLO.ID;
                                                                                objLo.PartnerID = LastPartner.PartnerID;
                                                                                objLo.Code = CusLocationExist.Code;
                                                                                objLo.IsSuccess = false;
                                                                                objLo.IsDelete = true;
                                                                                objLo.CustomerID = Cus_id;
                                                                                obj.lstCusLocation.Add(objLo);
                                                                                lstError.Add("Mã của [" + Cus_code + "] không được xóa");
                                                                            }
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                        }
                                                        else lstError.Add("Mã hệ thống [" + SysCode + "] không tồn tại");
                                                        #endregion
                                                    }
                                                }
                                                else
                                                    lstError.Add("Thiếu mã hệ thống");
                                            }
                                            else
                                            {
                                                //neu o thu 2 cung rong thi thoat ko doc nua
                                                break;
                                            }
                                            #endregion
                                        }

                                        if (lstError.Count > 0) { obj.ExcelSuccess = false; obj.ExcelError = string.Join(", ", lstError); }
                                        else { obj.ExcelSuccess = true; obj.ExcelError = string.Empty; }
                                        result.Add(obj);
                                        row++;
                                    }
                                }
                                else throw new Exception("Thiếu dữ liệu khách hàng");
                            }
                        }
                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #endregion
        #endregion

        #region CATSeaPort

        #region Main
        public DTOResult CATSeaPort_Customer_Read()
        {
            try
            {
                var result = default(DTOResult);
                ServiceFactory.SVCategory((IServices.ISVCategory sv) =>
                {
                    result = sv.Customer_List();
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DTOResult CATSeaPort_SeaPortCustom_Read()
        {
            try
            {
                var result = default(DTOResult);
                ServiceFactory.SVCategory((IServices.ISVCategory sv) =>
                {
                    result = sv.SeaPortCustom_List();
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DTOPartnerLocationResult CATSeaPort_SeaPortCustomer_List(dynamic d)
        {
            try
            {
                List<int> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(d.lst.ToString());
                var result = default(DTOPartnerLocationResult);
                if (lst == null)
                    lst = new List<int>();
                ServiceFactory.SVCategory((IServices.ISVCategory sv) =>
                {
                    result = sv.SeaPortCustomer_List(lst);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public void CATSeaPort_SeaPortCustom_SaveList(dynamic d)
        {
            try
            {
                DTOCATSeaPortCustom item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOCATSeaPortCustom>(d.item.ToString());

                ServiceFactory.SVCategory((IServices.ISVCategory sv) =>
                {
                    sv.SeaPortCustom_Save(item);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public CATPartner CATSeaPort_SeaPort_Get(dynamic d)
        {
            try
            {
                int id = (int)d.id;
                var result = new CATPartner();
                ServiceFactory.SVCategory((IServices.ISVCategory sv) =>
                {
                    result = sv.SeaPort_Get(id);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public int CATSeaPort_SeaPort_Update(dynamic d)
        {
            try
            {
                CATPartner item = Newtonsoft.Json.JsonConvert.DeserializeObject<CATPartner>(d.item.ToString());
                var result = -1;
                ServiceFactory.SVCategory((IServices.ISVCategory sv) =>
                {
                    result = sv.SeaPort_Save(item);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void CATSeaPort_SeaPort_Destroy(dynamic d)
        {
            try
            {
                CATPartner item = Newtonsoft.Json.JsonConvert.DeserializeObject<CATPartner>(d.item.ToString());
                if (item != null)
                {
                    ServiceFactory.SVCategory((IServices.ISVCategory sv) =>
                    {
                        sv.SeaPort_Delete(item);
                    });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Location
        public DTOResult CATSeaPort_Location_Read(dynamic d)
        {
            try
            {
                string request = d.request.ToString();
                int partnerid = (int)d.partnerid;
                var result = default(DTOResult);
                ServiceFactory.SVCategory((IServices.ISVCategory sv) =>
                {
                    result = sv.LocationInSeaport_List(request, partnerid);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public DTOCATLocationInPartner CATSeaPort_Location_Get(dynamic d)
        {
            try
            {
                int id = (int)d.id;
                DTOCATLocationInPartner result = new DTOCATLocationInPartner();
                ServiceFactory.SVCategory((IServices.ISVCategory sv) =>
                {
                    result = sv.LocationInSeaport_Get(id);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public bool CATSeaPort_Location_Update(dynamic d)
        {
            try
            {
                DTOCATLocationInPartner item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOCATLocationInPartner>(d.item.ToString());
                int partnerid = (int)d.partnerid;
                if (item != null)
                {
                    ServiceFactory.SVCategory((IServices.ISVCategory sv) =>
                    {
                        item = sv.LocationInSeaport_Save(item, partnerid);
                    });
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void CATSeaPort_Location_Destroy(dynamic d)
        {
            try
            {
                DTOCATLocationInPartner item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOCATLocationInPartner>(d.item.ToString());
                if (item != null && ModelState.IsValid)
                {
                    ServiceFactory.SVCategory((IServices.ISVCategory sv) =>
                    {
                        sv.LocationInSeaport_Delete(item);
                    });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Location Not In
        [HttpPost]
        public DTOResult CATSeaPort_LocationNotIn_Read(dynamic d)
        {
            try
            {
                string request = d.request.ToString();
                int partnerid = (int)d.partnerid;
                var result = default(DTOResult);
                ServiceFactory.SVCategory((IServices.ISVCategory sv) =>
                {
                    result = sv.LocationNotInSeaport_List(request, partnerid);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public bool CATSeaPort_LocationNotIn_SaveList(dynamic d)
        {
            try
            {
                List<DTOCATLocationInPartner> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DTOCATLocationInPartner>>(d.lst.ToString());
                int partnerid = (int)d.partnerid;
                if (lst != null)
                {
                    ServiceFactory.SVCategory((IServices.ISVCategory sv) =>
                    {
                        sv.LocationNotInSeaport_SaveList(lst, partnerid);
                    });
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Import Export
        public string CATSeaPort_Export_GetData()
        {
            try
            {
                List<DTOCATPartnerImport> result = new List<DTOCATPartnerImport>();
                ServiceFactory.SVCategory((IServices.ISVCategory sv) =>
                {
                    result = sv.SeaPort_Export();
                });

                string file = "/" + FolderUpload.Export + "ExportSeaport_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx";

                if (System.IO.File.Exists(HttpContext.Current.Server.MapPath(file)))
                    System.IO.File.Delete(HttpContext.Current.Server.MapPath(file));
                FileInfo exportfile = new FileInfo(HttpContext.Current.Server.MapPath(file));
                using (ExcelPackage package = new ExcelPackage(exportfile))
                {
                    // Sheet 1
                    ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Sheet 1");

                    int col = 1, row = 1, sttlv1 = 1, sttlv2 = 1;
                    #region header
                    worksheet.Cells[row, col].Value = "STT";
                    col++; worksheet.Cells[row, col].Value = "Mã hệ thống"; worksheet.Column(col).Width = 20;
                    col++; worksheet.Cells[row, col].Value = "Mã địa chỉ"; worksheet.Column(col).Width = 20;
                    col++; worksheet.Cells[row, col].Value = "Tên cảng biển"; worksheet.Column(col).Width = 15;
                    col++; worksheet.Cells[row, col].Value = "Địa chỉ giao hàng"; worksheet.Column(col).Width = 15;
                    col++; worksheet.Cells[row, col].Value = "Tỉnh thành"; worksheet.Column(col).Width = 15;
                    col++; worksheet.Cells[row, col].Value = "Quận/Huyện"; worksheet.Column(col).Width = 15;
                    col++; worksheet.Cells[row, col].Value = "Khu công nghiệp"; worksheet.Column(col).Width = 15;
                    col++; worksheet.Cells[row, col].Value = "SĐT"; worksheet.Column(col).Width = 15;
                    col++; worksheet.Cells[row, col].Value = "Fax"; worksheet.Column(col).Width = 15;
                    col++; worksheet.Cells[row, col].Value = "Email"; worksheet.Column(col).Width = 15;
                    col++; worksheet.Cells[row, col].Value = "Kinh độ"; worksheet.Column(col).Width = 15;
                    col++; worksheet.Cells[row, col].Value = "Vĩ dộ"; worksheet.Column(col).Width = 15;

                    ExcelHelper.CreateCellStyle(worksheet, row, 1, row, col, false, true, ExcelHelper.ColorGreen, ExcelHelper.ColorWhite, 0, "");

                    worksheet.Cells[row, 1, row, col].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    worksheet.Cells[row, 1, row, col].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    #endregion

                    #region data

                    col = 1;
                    row = 2;
                    foreach (var item in result)
                    {
                        col = 1;
                        worksheet.Cells[row, col].Value = sttlv1;
                        col++; worksheet.Cells[row, col].Value = item.Code;
                        col++; worksheet.Cells[row, col].Value = string.Empty;
                        col++; worksheet.Cells[row, col].Value = item.PartnerName;
                        col++; worksheet.Cells[row, col].Value = item.Address;
                        col++; worksheet.Cells[row, col].Value = item.ProvinceName;
                        col++; worksheet.Cells[row, col].Value = item.DistrictName;
                        col++; worksheet.Cells[row, col].Value = string.Empty;
                        col++; worksheet.Cells[row, col].Value = item.TelNo;
                        col++; worksheet.Cells[row, col].Value = item.Fax;
                        col++; worksheet.Cells[row, col].Value = item.Email;
                        col++; worksheet.Cells[row, col].Value = string.Empty;
                        col++; worksheet.Cells[row, col].Value = string.Empty;

                        worksheet.Row(row).OutlineLevel = 0;

                        ExcelHelper.CreateCellStyle(worksheet, row, 1, row, col, false, false, ExcelHelper.ColorYellow, "", 0, "");

                        sttlv2 = 1;
                        foreach (var detail in item.lstLocation)
                        {
                            col = 1;
                            row++;
                            worksheet.Cells[row, col].Value = sttlv2;
                            col++; worksheet.Cells[row, col].Value = detail.Code;
                            col++; worksheet.Cells[row, col].Value = detail.PartnerCode;
                            col++; worksheet.Cells[row, col].Value = detail.LocationName;
                            col++; worksheet.Cells[row, col].Value = detail.Address;
                            col++; worksheet.Cells[row, col].Value = detail.ProvinceName;
                            col++; worksheet.Cells[row, col].Value = detail.DistrictName;
                            col++; worksheet.Cells[row, col].Value = detail.EconomicZone;
                            col++; worksheet.Cells[row, col].Value = string.Empty;
                            col++; worksheet.Cells[row, col].Value = string.Empty;
                            col++; worksheet.Cells[row, col].Value = string.Empty;
                            col++; worksheet.Cells[row, col].Value = detail.Lat;
                            col++; worksheet.Cells[row, col].Value = detail.Lng;

                            worksheet.Row(row).OutlineLevel = 1;
                            sttlv2++;
                        }
                        sttlv1++;
                        row++;
                    }

                    #endregion
                    ExcelHelper.CreateCellStyle(worksheet, 2, 2, worksheet.Dimension.End.Row, 2, false, false, ExcelHelper.ColorOrange, "", 0, "");

                    for (int i = 1; i <= worksheet.Dimension.End.Row; i++)
                    {
                        for (int j = 1; j <= worksheet.Dimension.End.Column; j++)
                        {
                            worksheet.Cells[i, j].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                        }
                    }
                    package.Save();
                }
                return file;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void CATSeaPort_Excel_Save(dynamic dynPram)
        {
            try
            {
                List<DTOCATPartnerImport> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DTOCATPartnerImport>>(dynPram.lst.ToString());
                ServiceFactory.SVCategory((IServices.ISVCategory sv) =>
                {
                    sv.SeaPort_Import(lst);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public List<DTOCATPartnerImport> CATSeaPort_Excel_Check(dynamic dynPram)
        {
            try
            {
                CATFile file = Newtonsoft.Json.JsonConvert.DeserializeObject<CATFile>(dynPram.item.ToString());
                List<DTOCATPartnerImport> result = new List<DTOCATPartnerImport>();

                if (file != null && !string.IsNullOrEmpty(file.FilePath))
                {
                    List<DTOCATPartnerItem> listSeaport = new List<DTOCATPartnerItem>();
                    List<DTOCATLocationItem> listSeaportLocation = new List<DTOCATLocationItem>();
                    List<CATProvince> listProvince = new List<CATProvince>();
                    List<CATDistrict> listDistrict = new List<CATDistrict>();

                    ServiceFactory.SVCategory((IServices.ISVCategory sv) =>
                    {
                        listSeaport = sv.SeaPort_AllList().Data.Cast<DTOCATPartnerItem>().ToList();
                        listSeaportLocation = sv.SeaPortLocation_AllList().Data.Cast<DTOCATLocationItem>().ToList();
                        listProvince = sv.ExcelProvince_List();
                        listDistrict = sv.ExcelDistrict_List();
                    });

                    using (System.IO.FileStream fs = new System.IO.FileStream(HttpContext.Current.Server.MapPath("/" + file.FilePath), System.IO.FileMode.Open, System.IO.FileAccess.Read))
                    {
                        using (var package = new ExcelPackage(fs))
                        {
                            ExcelWorksheet worksheet = ExcelHelper.GetWorksheetByIndex(package, 1);

                            int col = 2, row;
                            string Code, PartnerCode, PartnerName, Address, ProvinceName, DistrictName, Zone, TellNo, Fax, Email, Lat, Lng;
                            if (worksheet != null)
                            {
                                row = 2;
                                while (row <= worksheet.Dimension.End.Row)
                                {
                                    List<string> lstError = new List<string>();

                                    #region read data
                                    col = 2;
                                    Code = ExcelHelper.GetValue(worksheet, row, col); Code = Code.Trim();
                                    col++; PartnerCode = ExcelHelper.GetValue(worksheet, row, col); PartnerCode = PartnerCode.Trim();
                                    col++; PartnerName = ExcelHelper.GetValue(worksheet, row, col); PartnerName = PartnerName.Trim();
                                    col++; Address = ExcelHelper.GetValue(worksheet, row, col); Address = Address.Trim();
                                    col++; ProvinceName = ExcelHelper.GetValue(worksheet, row, col); ProvinceName = ProvinceName.Trim();
                                    col++; DistrictName = ExcelHelper.GetValue(worksheet, row, col); DistrictName = DistrictName.Trim();
                                    col++; Zone = ExcelHelper.GetValue(worksheet, row, col); Zone = Zone.Trim();
                                    col++; TellNo = ExcelHelper.GetValue(worksheet, row, col); TellNo = TellNo.Trim();
                                    col++; Fax = ExcelHelper.GetValue(worksheet, row, col); Fax = Fax.Trim();
                                    col++; Email = ExcelHelper.GetValue(worksheet, row, col); Email = Email.Trim();
                                    col++; Lat = ExcelHelper.GetValue(worksheet, row, col); Lat = Lat.Trim();
                                    col++; Lng = ExcelHelper.GetValue(worksheet, row, col); Lng = Lng.Trim();
                                    #endregion

                                    if (row == 2)
                                        if (!string.IsNullOrEmpty(PartnerCode))
                                            throw new Exception("Không tìm thấy Partner");

                                    if (string.IsNullOrEmpty(PartnerCode))// nut cha
                                    {
                                        DTOCATPartnerImport obj = new DTOCATPartnerImport();
                                        obj.lstLocation = new List<DTOCATLocationImport>();

                                        obj.ExcelRow = row;

                                        if (!string.IsNullOrEmpty(Code))
                                        {
                                            #region data parent
                                            var checkCode = listSeaport.FirstOrDefault(c => c.Code == Code);
                                            if (checkCode != null)
                                                obj.ID = checkCode.ID;
                                            else
                                                obj.ID = 0;
                                            var checkCodeOnFile = result.FirstOrDefault(c => c.Code == Code);
                                            if (checkCodeOnFile != null) lstError.Add("Mã cảng biển [" + Code + "] bị trùng");
                                            obj.Code = Code;
                                            obj.Address = Address;
                                            obj.PartnerName = PartnerName;
                                            obj.TelNo = TellNo;
                                            obj.Fax = Fax;
                                            obj.Email = Email;
                                            if (!string.IsNullOrEmpty(ProvinceName))
                                            {
                                                var checkProvince = listProvince.FirstOrDefault(c => c.ProvinceName == ProvinceName);
                                                if (checkProvince == null)
                                                    lstError.Add("[" + ProvinceName + "] không tồn tại trong hệ thống");
                                                else
                                                {
                                                    obj.ProvinceID = checkProvince.ID; obj.ProvinceName = checkProvince.ProvinceName;
                                                    obj.CountryID = checkProvince.CountryID; obj.CountryName = checkProvince.CountryName;
                                                }
                                            }
                                            else
                                            {
                                                obj.ProvinceID = null;
                                                obj.CountryID = null;
                                            }
                                            if (!string.IsNullOrEmpty(DistrictName))
                                            {
                                                var checkDistrict = listDistrict.FirstOrDefault(c => c.DistrictName == DistrictName);
                                                if (checkDistrict == null)
                                                    lstError.Add("[" + DistrictName + "] không tồn tại trong hệ thống");
                                                else
                                                    obj.DistrictID = checkDistrict.ID; obj.DistrictName = DistrictName;
                                            }
                                            else
                                                obj.DistrictID = null;
                                            #endregion
                                        }
                                        else//ko co code
                                        {
                                            obj.Code = string.Empty;
                                            obj.Address = Address;
                                            obj.PartnerName = PartnerName;
                                            obj.TelNo = TellNo;
                                            obj.Fax = Fax;
                                            obj.Email = Email;
                                            lstError.Add("Thiếu mã hãng tàu");
                                        }

                                        obj.ExcelError = string.Empty; obj.ExcelSuccess = true;
                                        if (lstError.Count > 0)
                                        {
                                            obj.ExcelSuccess = false;
                                            obj.ExcelError = string.Join(", ", lstError);
                                        }
                                        result.Add(obj);
                                    }
                                    else
                                    {

                                        DTOCATLocationImport objLocation = new DTOCATLocationImport();
                                        var obj = result.LastOrDefault();
                                        if (string.IsNullOrEmpty(obj.Code))
                                        {
                                            obj.Code = Code;
                                            obj.Address = Address;
                                            obj.PartnerName = PartnerName;
                                            obj.TelNo = TellNo;
                                            obj.Fax = Fax;
                                            obj.Email = Email;
                                            lstError.Add("Không thể xác định cha");
                                        }
                                        else
                                        {
                                            #region data lstLocation

                                            objLocation.EconomicZone = Zone;
                                            var checkSysCodeLocation = listSeaportLocation.FirstOrDefault(c => c.Code == Code);
                                            if (obj.ID > 0)//parent da co
                                            {
                                                if (checkSysCodeLocation == null)//location moi
                                                {
                                                    objLocation.ID = 0;
                                                    objLocation.Code = Code;
                                                }
                                                else//location cu
                                                {
                                                    objLocation.ID = checkSysCodeLocation.ID;
                                                    objLocation.Code = Code;
                                                }
                                            }
                                            else// parent moi
                                            {
                                                if (checkSysCodeLocation == null)
                                                {
                                                    objLocation.ID = 0;
                                                    objLocation.Code = Code;
                                                }
                                                else//location cu
                                                {
                                                    objLocation.ID = checkSysCodeLocation.ID;
                                                    objLocation.Code = Code;
                                                }
                                            }

                                            var chkOnFile = obj.lstLocation.FirstOrDefault(c => c.Code == Code);
                                            if (chkOnFile != null) lstError.Add("Mã [" + Code + "] bị trùng");


                                            var checkCodeLocation = listSeaportLocation.FirstOrDefault(c => c.PartnerCode == PartnerCode);

                                            if (obj.ID > 0)//parent da co
                                            {
                                                if (checkCodeLocation == null)//location mo
                                                    objLocation.PartnerCode = PartnerCode;
                                                else//location cu
                                                {
                                                    if (checkCodeLocation.ID == objLocation.ID)
                                                        objLocation.PartnerCode = PartnerCode;
                                                    else lstError.Add("Mã địa chỉ [" + PartnerCode + "] Đã sử dụng");
                                                }
                                            }
                                            else// parent moi
                                            {
                                                if (checkCodeLocation == null)
                                                    objLocation.PartnerCode = PartnerCode;
                                                else lstError.Add("Mã địa chỉ [" + PartnerCode + "] Đã sử dụng");
                                            }

                                            var chkPartnerCodeOnFile = result.Count(c => c.lstLocation.Any(d => d.PartnerCode == PartnerCode)) > 0;
                                            if (chkPartnerCodeOnFile) lstError.Add("Mã địa chỉ [" + PartnerCode + "] Đã bị trùng");

                                            objLocation.LocationName = PartnerName;
                                            objLocation.Address = Address;

                                            if (!string.IsNullOrEmpty(ProvinceName))
                                            {
                                                var checkProvinceLocation = listProvince.FirstOrDefault(c => c.ProvinceName == ProvinceName);
                                                if (checkProvinceLocation == null)
                                                    lstError.Add("[" + ProvinceName + "] không tồn tại trong hệ thống");
                                                else
                                                {
                                                    objLocation.ProvinceID = checkProvinceLocation.ID;
                                                    objLocation.ProvinceName = checkProvinceLocation.ProvinceName;
                                                    objLocation.CountryID = checkProvinceLocation.CountryID;
                                                    objLocation.CountryName = checkProvinceLocation.CountryName;
                                                }
                                            }
                                            else lstError.Add("[Tỉnh thành] không được trống");

                                            if (!string.IsNullOrEmpty(DistrictName))
                                            {
                                                var checkDistrictLocation = listDistrict.FirstOrDefault(c => c.DistrictName == DistrictName);
                                                if (checkDistrictLocation == null)
                                                    lstError.Add("[" + DistrictName + "] không tồn tại trong hệ thống");
                                                else
                                                {
                                                    objLocation.DistrictID = checkDistrictLocation.ID;
                                                    objLocation.DistrictName = checkDistrictLocation.DistrictName;
                                                }
                                            }
                                            else lstError.Add("[Quận huyện] không được trống");
                                            if (string.IsNullOrEmpty(Lat))
                                                objLocation.Lat = null;
                                            else
                                            {
                                                try
                                                {
                                                    objLocation.Lat = Convert.ToDouble(Lat);
                                                }
                                                catch
                                                {
                                                    lstError.Add("[Vĩ độ] không chính xác");
                                                }
                                            }

                                            if (string.IsNullOrEmpty(Lng))
                                                objLocation.Lng = null;
                                            else
                                            {
                                                try
                                                {
                                                    objLocation.Lng = Convert.ToDouble(Lng);
                                                }
                                                catch
                                                {
                                                    lstError.Add("[Kinh độ] không chính xác");
                                                }
                                            }

                                            #endregion
                                        }

                                        objLocation.IsSuccess = true; objLocation.Error = string.Empty;
                                        if (lstError.Count > 0) { objLocation.IsSuccess = false; objLocation.Error = string.Join(" ,", lstError); }

                                        if (!obj.ExcelSuccess) { objLocation.IsSuccess = false; }
                                        if (!objLocation.IsSuccess)
                                        {
                                            if (string.IsNullOrEmpty(obj.ExcelError)) obj.ExcelError = objLocation.Error;
                                            else obj.ExcelError = obj.ExcelError + ", " + objLocation.Error;
                                        }
                                        obj.lstLocation.Add(objLocation);
                                    }
                                    row++;
                                }
                            }
                        }
                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region phân bổ mã
        public string CATSeaPort_Excel_ExportCode(dynamic dynPram)
        {
            try
            {
                List<int> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynPram.lst.ToString());
                DTOCUSPartnerLocationExport result = new DTOCUSPartnerLocationExport();
                ServiceFactory.SVCategory((IServices.ISVCategory sv) =>
                {
                    result = sv.PartnerLocation_SeaPort_Export(lst);
                });

                string file = "/" + FolderUpload.Export + "ExportPartnerLocation_SeaPort_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx";

                if (System.IO.File.Exists(HttpContext.Current.Server.MapPath(file)))
                    System.IO.File.Delete(HttpContext.Current.Server.MapPath(file));
                FileInfo exportfile = new FileInfo(HttpContext.Current.Server.MapPath(file));
                using (ExcelPackage package = new ExcelPackage(exportfile))
                {
                    // Sheet 1
                    ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Sheet 1");

                    int col = 1, row = 1, sttlv1 = 1, sttlv2 = 1;
                    #region header
                    worksheet.Cells[row, col].Value = "STT";
                    col++; worksheet.Cells[row, col].Value = "Loại"; worksheet.Column(col).Width = 15;
                    col++; worksheet.Cells[row, col].Value = "Cảng biển"; worksheet.Column(col).Width = 35;
                    col++; worksheet.Cells[row, col].Value = "Địa chỉ"; worksheet.Column(col).Width = 40;
                    col++; worksheet.Cells[row, col].Value = "Hệ thống"; worksheet.Column(col).Width = 15;

                    #region header dong
                    if (result.lstCustomer.Count > 0)
                    {
                        foreach (var cus in result.lstCustomer)
                        {
                            col++; worksheet.Cells[row, col].Value = cus.Code; worksheet.Column(col).Width = 15;
                        }
                    }

                    #endregion
                    ExcelHelper.CreateCellStyle(worksheet, row, 1, row, col, false, true, ExcelHelper.ColorGreen, ExcelHelper.ColorWhite, 0, "");
                    worksheet.Cells[row, 1, row, col].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    worksheet.Cells[row, 1, row, col].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                    #endregion

                    #region data

                    if (result.lstPartner.Count > 0)
                    {
                        row = 2; col = 1; sttlv1 = 1;
                        foreach (var partner in result.lstPartner)
                        {
                            col = 1;
                            worksheet.Cells[row, col].Value = sttlv1;
                            col++; worksheet.Cells[row, col].Value = partner.GroupOfPartnerName;
                            col++; worksheet.Cells[row, col].Value = partner.Name;
                            col++; worksheet.Cells[row, col].Value = partner.Address;
                            col++; worksheet.Cells[row, col].Value = partner.Code;
                            var lstCusCode = result.lstCusPartner.Where(c => c.PartnerID == partner.ID).ToList();
                            if (lstCusCode.Count > 0)
                            {
                                foreach (var cus in result.lstCustomer)
                                {
                                    var code = lstCusCode.FirstOrDefault(c => c.CustomerID == cus.ID);
                                    col++;
                                    if (code != null)
                                        worksheet.Cells[row, col].Value = code.Code;
                                }
                            }
                            ExcelHelper.CreateCellStyle(worksheet, row, 1, row, worksheet.Dimension.End.Column, false, false, ExcelHelper.ColorYellow, "", 0, "");

                            if (partner.lstLocation.Count > 0)
                            {
                                col = 2;
                                sttlv2 = 1;
                                foreach (var location in partner.lstLocation)
                                {
                                    col = 2;
                                    row++;
                                    worksheet.Cells[row, col].Value = sttlv2;
                                    col++; worksheet.Cells[row, col].Value = location.Name;
                                    col++; worksheet.Cells[row, col].Value = location.Address;
                                    col++; worksheet.Cells[row, col].Value = location.PartnerCode;
                                    var lstLocationCode = result.lstCusLocation.Where(c => c.LocationID == location.ID && c.PartnerID == partner.ID).ToList();
                                    if (lstLocationCode.Count > 0)
                                    {
                                        foreach (var cus in result.lstCustomer)
                                        {
                                            var code = lstLocationCode.FirstOrDefault(c => c.CustomerID == cus.ID);
                                            col++;
                                            if (code != null)
                                                worksheet.Cells[row, col].Value = code.Code;
                                        }
                                    }
                                    sttlv2++;
                                }
                            }
                            row++;
                            sttlv1++;
                        }
                    }

                    #endregion

                    for (int i = 1; i <= worksheet.Dimension.End.Row; i++)
                        for (int j = 1; j <= worksheet.Dimension.End.Column; j++)
                            worksheet.Cells[i, j].Style.Border.BorderAround(ExcelBorderStyle.Thin);


                    package.Save();
                }
                return file;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void CATSeaPort_Excel_SaveCode(dynamic dynParam)
        {
            try
            {
                List<DTOCUSPartnerLocationResult> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DTOCUSPartnerLocationResult>>(dynParam.lst.ToString());
                DTOCUSPartnerLocationImport data = new DTOCUSPartnerLocationImport();
                data.lstCusLocation = new List<DTOCustomerLocationImport>();
                data.lstCusPartner = new List<DTOCustomerPartnerImport>();
                foreach (var item in lst)
                {
                    if (item.IsPartner) data.lstCusPartner.AddRange(item.lstCusPartner);
                    else data.lstCusLocation.AddRange(item.lstCusLocation);
                }
                ServiceFactory.SVCategory((IServices.ISVCategory sv) =>
                {
                    sv.SeaPortLocation_Import(data);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public List<DTOCUSPartnerLocationResult> CATSeaPort_Excel_CheckCode(dynamic dynParam)
        {
            try
            {
                CATFile file = Newtonsoft.Json.JsonConvert.DeserializeObject<CATFile>(dynParam.item.ToString());
                List<DTOCUSPartnerLocationResult> result = new List<DTOCUSPartnerLocationResult>();

                if (file != null && !string.IsNullOrEmpty(file.FilePath))
                {
                    DTOCUSPartnerLocationExport data = new DTOCUSPartnerLocationExport();
                    List<CUSCustomer> lstCustomer = new List<CUSCustomer>();
                    List<DTOCustomerPartnerImport> lstCusPartner = new List<DTOCustomerPartnerImport>();
                    List<DTOCUSPartnerImport> lstPartner = new List<DTOCUSPartnerImport>();
                    List<DTOCustomerLocationImport> lstCusLocation = new List<DTOCustomerLocationImport>();

                    List<int> lstCustomerID = new List<int>();

                    ServiceFactory.SVCategory((IServices.ISVCategory sv) =>
                    {
                        lstCustomer = sv.Customer_List().Data.Cast<CUSCustomer>().ToList();
                    });

                    using (System.IO.FileStream fs = new System.IO.FileStream(HttpContext.Current.Server.MapPath("/" + file.FilePath), System.IO.FileMode.Open, System.IO.FileAccess.Read))
                    {
                        using (var package = new ExcelPackage(fs))
                        {
                            ExcelWorksheet worksheet = ExcelHelper.GetWorksheetByIndex(package, 1);

                            int col = 2, row;
                            string Code, SysCode, strCheckPartner, strCheckBreak;
                            if (worksheet != null)
                            {
                                row = 1;
                                if (worksheet.Dimension.End.Column >= 6)
                                {
                                    Dictionary<int, int> dicCusIDCol = new Dictionary<int, int>();
                                    Dictionary<int, string> dicCusIDCode = new Dictionary<int, string>();
                                    for (int i = 6; i <= worksheet.Dimension.End.Column; i++)
                                    {
                                        Code = ExcelHelper.GetValue(worksheet, row, i);
                                        Code = Code.Trim();
                                        var checkCusName = lstCustomer.FirstOrDefault(c => c.Code == Code);
                                        if (checkCusName != null)
                                        {
                                            if (!dicCusIDCode.ContainsValue(Code))
                                            {
                                                dicCusIDCol.Add(checkCusName.ID, i);
                                                dicCusIDCode.Add(checkCusName.ID, Code);
                                                lstCustomerID.Add(checkCusName.ID);
                                            }
                                            else throw new Exception("Mã khách hàng: [" + Code + "] bị trùng");
                                        }
                                        else throw new Exception("Mã khách hàng: [" + Code + "] không tốn tại");
                                    }

                                    ServiceFactory.SVCategory((IServices.ISVCategory sv) =>
                                    {
                                        data = sv.PartnerLocation_SeaPort_Export(lstCustomerID);
                                    });

                                    lstPartner = data.lstPartner;
                                    // lstCustomer = data.lstCustomer;
                                    lstCusPartner = data.lstCusPartner;
                                    lstCusLocation = data.lstCusLocation;

                                    row = 2;
                                    while (row <= worksheet.Dimension.End.Row)
                                    {
                                        List<string> lstError = new List<string>();
                                        List<string> lstErrorLocation = new List<string>();

                                        col = 1;
                                        DTOCUSPartnerLocationResult obj = new DTOCUSPartnerLocationResult();
                                        obj.ExcelRow = row;
                                        obj.lstCusLocation = new List<DTOCustomerLocationImport>();
                                        obj.lstCusPartner = new List<DTOCustomerPartnerImport>();

                                        strCheckPartner = ExcelHelper.GetValue(worksheet, row, col); strCheckPartner = strCheckPartner.Trim();
                                        if (!string.IsNullOrEmpty(strCheckPartner))// la partner
                                        {
                                            #region partner
                                            obj.IsPartner = true;
                                            SysCode = ExcelHelper.GetValue(worksheet, row, 5); SysCode = SysCode.Trim();
                                            obj.SysCode = SysCode;
                                            if (!string.IsNullOrEmpty(SysCode))
                                            {
                                                var checkPartner = lstPartner.FirstOrDefault(c => c.Code == SysCode);
                                                if (checkPartner != null)
                                                {
                                                    obj.PartnerID = checkPartner.ID;
                                                    obj.lstLocation = checkPartner.lstLocation;
                                                    if (dicCusIDCol.Keys.Count > 0)
                                                    {
                                                        foreach (var word in dicCusIDCol)
                                                        {
                                                            DTOCustomerPartnerImport objCus = new DTOCustomerPartnerImport();
                                                            var Column = word.Value;
                                                            var Cus_id = word.Key;
                                                            string Cus_code;
                                                            dicCusIDCode.TryGetValue(Cus_id, out Cus_code);

                                                            Code = ExcelHelper.GetValue(worksheet, row, Column); Code = Code.Trim();
                                                            var CusPartnerExist = lstCusPartner.FirstOrDefault(c => c.PartnerID == checkPartner.ID && c.CustomerID == Cus_id);
                                                            if (!string.IsNullOrEmpty(Code))
                                                            {
                                                                objCus.CustomerID = Cus_id;
                                                                objCus.Code = Code;
                                                                objCus.PartnerID = checkPartner.ID;
                                                                objCus.IsSuccess = true;
                                                                obj.lstCusPartner.Add(objCus);
                                                            }
                                                            else
                                                            {
                                                                if (CusPartnerExist != null)
                                                                {
                                                                    if (CusPartnerExist.IsDelete)
                                                                    {
                                                                        objCus.CustomerID = Cus_id;
                                                                        objCus.Code = string.Empty;
                                                                        objCus.PartnerID = checkPartner.ID;
                                                                        objCus.IsSuccess = true;
                                                                        obj.lstCusPartner.Add(objCus);
                                                                    }
                                                                    else
                                                                    {
                                                                        objCus.CustomerID = Cus_id;
                                                                        objCus.Code = CusPartnerExist.Code;
                                                                        objCus.PartnerID = checkPartner.ID;
                                                                        objCus.IsSuccess = false;
                                                                        objCus.IsDelete = true;
                                                                        obj.lstCusPartner.Add(objCus);
                                                                        lstError.Add("Mã của [" + Cus_code + "] không được xóa");
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                                else
                                                    lstError.Add("Mã hệ thống [" + SysCode + "] không tồn tại");
                                            }
                                            else
                                                lstError.Add("Thiếu mã hệ thống");
                                            #endregion
                                        }
                                        else//location
                                        {
                                            #region location
                                            strCheckBreak = ExcelHelper.GetValue(worksheet, row, 2);
                                            if (!string.IsNullOrEmpty(strCheckBreak))
                                            {
                                                obj.IsPartner = false;
                                                SysCode = ExcelHelper.GetValue(worksheet, row, 5); SysCode = SysCode.Trim();
                                                obj.SysCode = SysCode;
                                                if (!string.IsNullOrEmpty(SysCode))
                                                {
                                                    var LastPartner = result.LastOrDefault(c => c.IsPartner == true);
                                                    if (LastPartner == null) lstError.Add("Không tìm thấy partner cho địa điểm ");
                                                    else
                                                    {
                                                        #region check
                                                        obj.PartnerID = LastPartner.PartnerID;
                                                        var checkLO = LastPartner.lstLocation.FirstOrDefault(c => c.PartnerCode == SysCode);
                                                        if (checkLO != null)
                                                        {
                                                            if (dicCusIDCol.Keys.Count > 0)
                                                            {
                                                                foreach (var word in dicCusIDCol)
                                                                {
                                                                    DTOCustomerLocationImport objLo = new DTOCustomerLocationImport();
                                                                    var Column = word.Value;
                                                                    var Cus_id = word.Key;
                                                                    string Cus_code;
                                                                    dicCusIDCode.TryGetValue(Cus_id, out Cus_code);

                                                                    Code = ExcelHelper.GetValue(worksheet, row, Column); Code = Code.Trim();
                                                                    var CusLocationExist = lstCusLocation.FirstOrDefault(c => c.CustomerID == Cus_id && c.PartnerID == LastPartner.PartnerID && c.LocationID == checkLO.ID);
                                                                    if (!string.IsNullOrEmpty(Code))
                                                                    {
                                                                        if (CusLocationExist != null)
                                                                            objLo.ID = CusLocationExist.ID;
                                                                        else objLo.ID = 0;
                                                                        objLo.LocationID = checkLO.ID;
                                                                        objLo.PartnerID = LastPartner.PartnerID;
                                                                        objLo.Code = Code;
                                                                        objLo.CustomerID = Cus_id;
                                                                        objLo.IsSuccess = true;
                                                                        //check trùng trong database
                                                                        var check = lstCusLocation.Where(c => c.CustomerID == Cus_id && c.PartnerID == LastPartner.PartnerID && c.LocationID != checkLO.ID && c.Code == Code).FirstOrDefault();
                                                                        if (check != null)
                                                                        {
                                                                            objLo.IsSuccess = false;
                                                                            lstError.Add("Mã [" + Code + "] của [" + SysCode + "]-KH ["+Cus_code+"] đã sử dụng");
                                                                        }
                                                                        else
                                                                        {
                                                                            var lstLoInPartner = result.Where(c => c.PartnerID == LastPartner.PartnerID && c.IsPartner == false).SelectMany(c => c.lstCusLocation, (c, d) => d).ToList();
                                                                            var checkDup = lstLoInPartner.Where(c => c.CustomerID == Cus_id && c.Code == Code).FirstOrDefault();
                                                                            if (checkDup != null)
                                                                            {
                                                                                objLo.IsSuccess = false;
                                                                                lstError.Add("Mã [" + Code + "] của [" + SysCode + "]-KH [" + Cus_code + "] bị trùng trong cùng đối tác");
                                                                            }
                                                                        }
                                                                        obj.lstCusLocation.Add(objLo);
                                                                    }
                                                                    else
                                                                    {
                                                                        if (CusLocationExist != null)
                                                                        {
                                                                            if (CusLocationExist.IsDelete)
                                                                            {
                                                                                objLo.ID = CusLocationExist.ID;
                                                                                objLo.LocationID = checkLO.ID;
                                                                                objLo.PartnerID = LastPartner.PartnerID;
                                                                                objLo.Code = string.Empty;
                                                                                objLo.CustomerID = Cus_id;
                                                                                objLo.IsSuccess = true;
                                                                                obj.lstCusLocation.Add(objLo);
                                                                            }
                                                                            else
                                                                            {
                                                                                objLo.ID = CusLocationExist.ID;
                                                                                objLo.LocationID = checkLO.ID;
                                                                                objLo.PartnerID = LastPartner.PartnerID;
                                                                                objLo.Code = CusLocationExist.Code;
                                                                                objLo.IsSuccess = false;
                                                                                objLo.IsDelete = true;
                                                                                objLo.CustomerID = Cus_id;
                                                                                obj.lstCusLocation.Add(objLo);
                                                                                lstError.Add("Mã của [" + Cus_code + "] không được xóa");
                                                                            }
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                        }
                                                        else lstError.Add("Mã hệ thống [" + SysCode + "] không tồn tại");
                                                        #endregion
                                                    }
                                                }
                                                else
                                                    lstError.Add("Thiếu mã hệ thống");
                                            }
                                            else
                                            {
                                                //neu o thu 2 cung rong thi thoat ko doc nua
                                                break;
                                            }
                                            #endregion
                                        }

                                        if (lstError.Count > 0) { obj.ExcelSuccess = false; obj.ExcelError = string.Join(", ", lstError); }
                                        else { obj.ExcelSuccess = true; obj.ExcelError = string.Empty; }
                                        result.Add(obj);
                                        row++;
                                    }
                                }
                                else throw new Exception("Thiếu dữ liệu khách hàng");
                            }
                        }
                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #endregion

        #region CATLocation

        public DTOResult CATLocation_Read(dynamic dynParam)
        {
            try
            {
                string request = dynParam.request.ToString();
                var result = default(DTOResult);
                ServiceFactory.SVCategory((ISVCategory sv) =>
                {
                    result = sv.Location_List(request);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public int CATLocation_Update(dynamic dynParam)
        {
            try
            {
                CATLocation item = Newtonsoft.Json.JsonConvert.DeserializeObject<CATLocation>(dynParam.item.ToString());
                int result = -1;
                ServiceFactory.SVCategory((ISVCategory sv) =>
                {
                    result = sv.Location_Save(item);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public CATLocation CATLocation_Get(dynamic dynParam)
        {
            try
            {
                int id = (int)dynParam.ID;
                var result = default(CATLocation);
                ServiceFactory.SVCategory((ISVCategory sv) =>
                {
                    result = sv.Location_Get(id);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOCATLocationDetail Location_GetDetail(dynamic dynParam)
        {
            try
            {
                int id = (int)dynParam.ID;
                var result = default(DTOCATLocationDetail);
                ServiceFactory.SVCategory((ISVCategory sv) =>
                {
                    result = sv.Location_GetDetail(id);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void CATLocation_Destroy(dynamic dynParam)
        {
            try
            {
                CATLocation item = Newtonsoft.Json.JsonConvert.DeserializeObject<CATLocation>(dynParam.item.ToString());
                ServiceFactory.SVCategory((ISVCategory sv) =>
                {
                    sv.Location_Delete(item);
                    AddressSearchHelper.Delete(sv.CATAddressSearch_List(item.ID));
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string CATLocation_Excel_Export()
        {
            try
            {
                var resBody = new List<CATLocation>();
                ServiceFactory.SVCategory((ISVCategory sv) =>
                {
                    resBody = sv.ExcelLocation_List();
                });
                string file = "/" + FolderUpload.Export + "DanhSachDiaDiem_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx";

                if (System.IO.File.Exists(HttpContext.Current.Server.MapPath(file)))
                    System.IO.File.Delete(HttpContext.Current.Server.MapPath(file));
                FileInfo exportfile = new FileInfo(HttpContext.Current.Server.MapPath(file));
                using (ExcelPackage pk = new ExcelPackage(exportfile))
                {
                    ExcelWorksheet worksheet = pk.Workbook.Worksheets.Add("Sheet1");
                    int col = 0, row = 1;

                    #region Header
                    col++; worksheet.Cells[row, col].Value = "STT"; worksheet.Column(col).Width = 5;
                    col++; worksheet.Cells[row, col].Value = "Mã địa chỉ"; worksheet.Column(col).Width = 15;
                    col++; worksheet.Cells[row, col].Value = "Tên địa chỉ"; worksheet.Column(col).Width = 20;
                    col++; worksheet.Cells[row, col].Value = "Địa chỉ giao hàng"; worksheet.Column(col).Width = 35;
                    col++; worksheet.Cells[row, col].Value = "Tỉnh thành"; worksheet.Column(col).Width = 20;
                    col++; worksheet.Cells[row, col].Value = "Quận huyện"; worksheet.Column(col).Width = 20;
                    col++; worksheet.Cells[row, col].Value = "RouteID"; worksheet.Column(col).Width = 20;
                    col++; worksheet.Cells[row, col].Value = "Ghi chú"; worksheet.Column(col).Width = 20;
                    col++; worksheet.Cells[row, col].Value = "Ghi chú 1"; worksheet.Column(col).Width = 20;
                    col++; worksheet.Cells[row, col].Value = "Vĩ độ"; worksheet.Column(col).Width = 15;
                    col++; worksheet.Cells[row, col].Value = "Kinh độ"; worksheet.Column(col).Width = 15;

                    worksheet.Cells[1, 1, row, col].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    worksheet.Cells[1, 1, row, col].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                    ExcelHelper.CreateCellStyle(worksheet, 1, 1, 1, col, false, true, ExcelHelper.ColorGreen, ExcelHelper.ColorWhite);

                    #endregion

                    #region Body
                    int stt = 0;
                    foreach (CATLocation item in resBody)
                    {
                        row++; col = 0; stt++;
                        col++; worksheet.Cells[row, col].Value = stt;
                        col++; worksheet.Cells[row, col].Value = item.Code;
                        col++; worksheet.Cells[row, col].Value = item.Location;
                        col++; worksheet.Cells[row, col].Value = item.Address;
                        col++; worksheet.Cells[row, col].Value = item.ProvinceName;
                        col++; worksheet.Cells[row, col].Value = item.DistrictName;
                        col++; worksheet.Cells[row, col].Value = item.EconomicZone;
                        col++; worksheet.Cells[row, col].Value = item.Note;
                        col++; worksheet.Cells[row, col].Value = item.Note1;

                        col++; worksheet.Cells[row, col].Value = item.Lat;
                        ExcelHelper.CreateFormat(worksheet, row, col, ExcelHelper.FormatNumber);
                        col++; worksheet.Cells[row, col].Value = item.Lng;
                        ExcelHelper.CreateFormat(worksheet, row, col, ExcelHelper.FormatNumber);
                    }
                    #endregion

                    pk.Save();
                }

                return file;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public List<CATLocationImport> CATLocation_Excel_Check(dynamic dynPram)
        {
            try
            {
                CATFile item = Newtonsoft.Json.JsonConvert.DeserializeObject<CATFile>(dynPram.item.ToString());
                List<CATLocationImport> sData = new List<CATLocationImport>();
                if (item != null && !string.IsNullOrEmpty(item.FilePath))
                {
                    List<CATLocation> resLocation = new List<CATLocation>();
                    List<CATProvince> resProv = new List<CATProvince>();
                    List<CATDistrict> resDist = new List<CATDistrict>();
                    ServiceFactory.SVCategory((ISVCategory sv) =>
                    {
                        resLocation = sv.ExcelLocation_List();
                        resProv = sv.ExcelProvince_List();
                        resDist = sv.ExcelDistrict_List();
                    });
                    using (System.IO.FileStream fs = new System.IO.FileStream(HttpContext.Current.Server.MapPath("/" + item.FilePath), System.IO.FileMode.Open, System.IO.FileAccess.Read))
                    {
                        using (var package = new ExcelPackage(fs))
                        {
                            ExcelWorksheet worksheet = ExcelHelper.GetWorksheetByIndex(package, 1);
                            if (worksheet != null)
                            {
                                var totalCol = worksheet.Dimension.End.Column;
                                var totalRow = worksheet.Dimension.End.Row;

                                for (int row = 2; row <= totalRow; row++)
                                {
                                    CATLocationImport obj = new CATLocationImport();
                                    obj.ExcelRow = row;
                                    List<string> ListError = new List<string>();
                                    int col = 1; col++;

                                    var str = ExcelHelper.GetValue(worksheet, row, col);
                                    if (string.IsNullOrEmpty(str))
                                        ListError.Add("Mã địa chỉ không được để trống.");
                                    else
                                    {
                                        obj.Code = str;
                                        var checkCode = resLocation.FirstOrDefault(c => c.Code == str);
                                        if (checkCode == null) obj.ID = 0;
                                        else obj.ID = checkCode.ID;
                                        var objCheckOnFile = sData.FirstOrDefault(c => c.Code == str);
                                        if (objCheckOnFile != null)
                                            ListError.Add("Trùng mã địa chỉ trên file.");
                                    }

                                    col++; str = ExcelHelper.GetValue(worksheet, row, col);
                                    if (string.IsNullOrEmpty(str))
                                        ListError.Add("Tên địa chỉ không được để trống.");
                                    else
                                    {
                                        obj.Location = str;
                                    }
                                    col++; str = ExcelHelper.GetValue(worksheet, row, col);
                                    if (string.IsNullOrEmpty(str))
                                    {
                                        ListError.Add("Địa chỉ không được để trống.");
                                    }
                                    else
                                    {
                                        obj.Address = str;
                                    }
                                    col++; str = ExcelHelper.GetValue(worksheet, row, col);
                                    if (string.IsNullOrEmpty(str))
                                    {
                                        ListError.Add("Tỉnh thành không được để trống.");
                                    }
                                    else
                                    {
                                        obj.ProvinceName = str;
                                        var objProv = resProv.FirstOrDefault(c => c.ProvinceName == str);
                                        if (objProv != null)
                                        {
                                            obj.ProvinceID = objProv.ID;
                                            obj.CountryID = objProv.CountryID;
                                        }
                                        else
                                            ListError.Add("Tỉnh thành không tồn tại.");
                                    }
                                    col++; str = ExcelHelper.GetValue(worksheet, row, col);
                                    if (string.IsNullOrEmpty(str))
                                    {
                                        ListError.Add("Quận huyện không được để trống.");
                                    }
                                    else
                                    {
                                        obj.DistrictName = str;
                                        var objDist = resDist.FirstOrDefault(c => c.DistrictName == str);
                                        if (objDist != null)
                                        {
                                            obj.DistrictID = objDist.ID;
                                        }
                                        else
                                            ListError.Add("Quận huyện không tồn tại.");
                                    }

                                    col++; str = ExcelHelper.GetValue(worksheet, row, col);
                                    obj.EconomicZone = str;
                                    if (string.IsNullOrEmpty(str)) obj.EconomicZone = null;
                                    col++; str = ExcelHelper.GetValue(worksheet, row, col);
                                    obj.Note = str;
                                    if (string.IsNullOrEmpty(str)) obj.Note = null;
                                    col++; str = ExcelHelper.GetValue(worksheet, row, col);
                                    obj.Note1 = str;
                                    if (string.IsNullOrEmpty(str)) obj.Note1 = null;

                                    col++; str = ExcelHelper.GetValue(worksheet, row, col);
                                    if (!string.IsNullOrEmpty(str))
                                    {
                                        try
                                        {
                                            obj.Lat = Convert.ToDouble(str);
                                        }
                                        catch
                                        {
                                            ListError.Add("Nhập kinh độ sai.");
                                        }
                                    }
                                    col++; str = ExcelHelper.GetValue(worksheet, row, col);
                                    if (!string.IsNullOrEmpty(str))
                                    {
                                        try
                                        {
                                            obj.Lng = Convert.ToDouble(str);
                                        }
                                        catch
                                        {
                                            ListError.Add("Nhập vĩ độ sai.");
                                        }
                                    }
                                    obj.ExcelSuccess = true; obj.ExcelError = string.Empty;
                                    if (ListError.Count > 0) { obj.ExcelError = string.Join(" ,", ListError); obj.ExcelSuccess = false; }
                                    sData.Add(obj);
                                }
                            }
                        }
                    }
                }

                return sData;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void CATLocation_Excel_Save(dynamic dynParam)
        {
            try
            {
                List<CATLocationImport> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<CATLocationImport>>(dynParam.lst.ToString());
                ServiceFactory.SVCategory((ISVCategory sv) =>
                {
                    sv.ExcelLocation_Save(lst);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public List<CATLocation> CATLocation_GetLatLngByAddress(dynamic dynParam)
        {
            try
            {
               List< CATLocation> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<CATLocation>>(dynParam.lst.ToString());
               const string url = "http://maps.googleapis.com/maps/api/geocode/xml?sensor=true&address=";
               foreach (var location in lst)
               {
                   if (!string.IsNullOrEmpty(location.Address))
                   {
                       var request = WebRequest.Create(url+location.Address);
                       var response = request.GetResponse();
                       var xdoc = XDocument.Load(response.GetResponseStream());

                       var status = xdoc.Element("GeocodeResponse").Element("status");
                       if (!string.IsNullOrEmpty(status.Value) && status.Value == "OK")
                       {
                           var result = xdoc.Element("GeocodeResponse").Element("result");
                           var locationElement = result.Element("geometry").Element("location");
                           var lat = locationElement.Element("lat");
                           var lng = locationElement.Element("lng");
                           location.Lat = 0;
                           location.Lng = 0;
                           try
                           {
                               if (!string.IsNullOrEmpty(lat.Value)) location.Lat = Convert.ToDouble(lat.Value);
                               if (!string.IsNullOrEmpty(lng.Value)) location.Lng = Convert.ToDouble(lng.Value);
                           }
                           catch { }
                       }
                       System.Threading.Thread.Sleep(500);
                   }
               }

               return lst;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public void Location_UpdateLatLng(dynamic dynParam)
        {
            try
            {
                List<CATLocation> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<CATLocation>>(dynParam.lst.ToString());
                ServiceFactory.SVCategory((ISVCategory sv) =>
                {
                    sv.Location_UpdateLatLng(lst);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region CATRouting

        #region Route
        public DTOResult CATRouting_RoutingTree_Read(dynamic dynParam)
        {
            try
            {
                string request = dynParam.request.ToString();
                var result = default(DTOResult);
                ServiceFactory.SVCategory((ISVCategory sv) =>
                {
                    result = sv.Routing_List(request);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DTOCATRouting CATRouting_RoutingTree_Get(dynamic dynParam)
        {
            try
            {
                int id = (int)dynParam.ID;
                var result = default(DTOCATRouting);
                ServiceFactory.SVCategory((ISVCategory sv) =>
                {
                    result = sv.Routing_Get(id);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public int CATRouting_Routing_Save(dynamic dynParam)
        {
            try
            {
                DTOCATRouting item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOCATRouting>(dynParam.item.ToString());
                int result = -1;
                ServiceFactory.SVCategory((ISVCategory sv) =>
                {
                    result = sv.Routing_Save(item);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void CATRouting_Routing_Delete(dynamic dynParam)
        {
            try
            {
                DTOCATRouting item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOCATRouting>(dynParam.item.ToString());
                ServiceFactory.SVCategory((ISVCategory sv) =>
                {
                    sv.Routing_Delete(item);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DTOResult CATRouting_RoutingLocationNotIn_List(dynamic dynParam)
        {
            try
            {
                string request = dynParam.request.ToString();
                int? fromID = (int?)dynParam.fromID;
                int? toID = (int?)dynParam.toID;
                var result = default(DTOResult);
                ServiceFactory.SVCategory((ISVCategory sv) =>
                {
                    result = sv.RoutingLocationNotIn_List(request, fromID, toID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DTOResult CATRouting_RoutingAreaNotIn_List(dynamic dynParam)
        {
            try
            {
                string request = dynParam.request.ToString();
                int? fromID = (int?)dynParam.fromID;
                int? toID = (int?)dynParam.toID;
                var result = default(DTOResult);
                ServiceFactory.SVCategory((ISVCategory sv) =>
                {
                    result = sv.RoutingAreaNotIn_List(request, fromID, toID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int CATRouting_RoutingArea_Save(dynamic dynParam)
        {
            try
            {
                CATRoutingArea item = Newtonsoft.Json.JsonConvert.DeserializeObject<CATRoutingArea>(dynParam.item.ToString());
                int result = -1;
                ServiceFactory.SVCategory((ISVCategory sv) =>
                {
                    result = sv.RoutingArea_Save(item);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public CATRoutingArea CATRouting_RoutingArea_Get(dynamic dynParam)
        {
            try
            {
                int id = (int)dynParam.ID;
                var result = default(CATRoutingArea);
                ServiceFactory.SVCategory((ISVCategory sv) =>
                {
                    result = sv.RoutingArea_Get(id);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void CATRouting_RoutingArea_Delete(dynamic dynParam)
        {
            try
            {
                CATRoutingArea item = Newtonsoft.Json.JsonConvert.DeserializeObject<CATRoutingArea>(dynParam.item.ToString());
                ServiceFactory.SVCategory((ISVCategory sv) =>
                {
                    sv.RoutingArea_Delete(item);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DTOResult CATRouting_RoutingAreaDetail_List(dynamic dynParam)
        {
            try
            {
                int areaID = (int)dynParam.areaID;
                var result = default(DTOResult);
                ServiceFactory.SVCategory((ISVCategory sv) =>
                {
                    result = sv.RoutingAreaDetail_List(areaID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DTOCATRoutingAreaDetail CATRouting_RoutingAreaDetail_Get(dynamic dynParam)
        {
            try
            {
                int id = (int)dynParam.ID;
                var result = default(DTOCATRoutingAreaDetail);
                ServiceFactory.SVCategory((ISVCategory sv) =>
                {
                    result = sv.RoutingAreaDetail_Get(id);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public int CATRouting_RoutingAreaDetail_Save(dynamic dynParam)
        {
            try
            {
                DTOCATRoutingAreaDetail item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOCATRoutingAreaDetail>(dynParam.item.ToString());
                int areaID = (int)dynParam.areaID;
                int result = -1;
                ServiceFactory.SVCategory((ISVCategory sv) =>
                {
                    result = sv.RoutingAreaDetail_Save(item, areaID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void CATRouting_RoutingAreaDetail_Delete(dynamic dynParam)
        {
            try
            {
                DTOCATRoutingAreaDetail item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOCATRoutingAreaDetail>(dynParam.item.ToString());
                ServiceFactory.SVCategory((ISVCategory sv) =>
                {
                    sv.RoutingAreaDetail_Delete(item);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void CATRouting_RoutingArea_Refresh(dynamic dynParam)
        {
            try
            {
                CATRoutingArea item = Newtonsoft.Json.JsonConvert.DeserializeObject<CATRoutingArea>(dynParam.item.ToString());
                ServiceFactory.SVCategory((ISVCategory sv) =>
                {
                    sv.RoutingAreaDetail_Refresh(item);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        private void CATRouting_DropdownList_Read_Create(List<DTOCATRouting> lstTarget, IEnumerable<DTOCATRouting> lstSource, int? parentid, int level)
        {
            foreach (var item in lstSource.Where(c => c.ParentID == parentid))
            {
                item.RoutingName = new string('.', 3 * level) + item.RoutingName;
                lstTarget.Add(item);
                CATRouting_DropdownList_Read_Create(lstTarget, lstSource, item.ID, level + 1);
            }
        }

        public DTOResult CATRouting_RoutingCBB_List()
        {
            try
            {
                var result = default(DTOResult);
                ServiceFactory.SVCategory((ISVCategory sv) =>
                {
                    result = sv.RoutingAll_List();
                });
                var lst = new List<DTOCATRouting>();
                CATRouting_DropdownList_Read_Create(lst, result.Data.Cast<DTOCATRouting>(), null, 0);
                result.Data = lst;
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void CATRouting_Routing_UpdateLocationForAllRouting()
        {
            try
            {
                ServiceFactory.SVCategory((ISVCategory sv) =>
                {
                    sv.Routing_UpdateLocationForAllRouting();
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void CATRouting_Routing_SaveAllCustomer(List<int> lst)
        {
            try
            {
                if (lst != null)
                {
                    ServiceFactory.SVCategory((ISVCategory sv) =>
                    {
                        sv.Routing_SaveAllCustomer(lst);
                    });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void CATRouting_Routing_RefreshAddress()
        {
            try
            {
                var lst = new List<AddressSearchItem>();
                ServiceFactory.SVCategory((ISVCategory sv) =>
                {
                    lst = sv.AddressSearch_List();
                });
                //var obj = lst.Where(c => c.CUSLocationID == 10616).ToList();
                lst = lst.Where(c => c.Address != null && c.PartnerCode != null).ToList();
                AddressSearchHelper.Create(lst);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Cost

        public DTOResult CATRouting_RoutingCost_List(dynamic dynParam)
        {
            try
            {
                var result = default(DTOResult);
                string request = dynParam.request.ToString();
                int routingID = (int)dynParam.routingID;
                ServiceFactory.SVCategory((ISVCategory sv) =>
                {
                    result = sv.RoutingCost_List(request, routingID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int CATRouting_RoutingCost_Save(dynamic dynParam)
        {
            try
            {
                int routingID = (int)dynParam.routingID;
                DTOCATRoutingCost item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOCATRoutingCost>(dynParam.item.ToString());
                int result = -1;
                ServiceFactory.SVCategory((ISVCategory sv) =>
                {
                    result = sv.RoutingCost_Save(item, routingID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DTOCATRoutingCost CATRouting_RoutingCost_Get(dynamic dynParam)
        {
            try
            {
                int id = (int)dynParam.ID;
                DTOCATRoutingCost result = new DTOCATRoutingCost();
                ServiceFactory.SVCategory((ISVCategory sv) =>
                {
                    result = sv.RoutingCost_Get(id);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void CATRouting_RoutingCost_Delete(dynamic dynParam)
        {
            try
            {
                DTOCATRoutingCost item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOCATRoutingCost>(dynParam.item.ToString());
                ServiceFactory.SVCategory((ISVCategory sv) =>
                {
                    sv.RoutingCost_Delete(item);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DTOResult CATRouting_Cost_List()
        {
            try
            {
                var result = default(DTOResult);
                ServiceFactory.SVCategory((ISVCategory sv) =>
                {
                    result = sv.Cost_List();
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Excel

        private void CATRouting_Excel_Level_Create(List<DTOExcelRouteArea> lstBody, DTOExcelRouteArea item, ref ExcelWorksheet worksheet, ref int row, ref int stt, ref int lvl, int totalCol)
        {
            int col = 0; row++; stt++;
            col++; worksheet.Cells[row, col].Value = stt;
            col++; worksheet.Cells[row, col].Value = item.ParentID > 0 ? lstBody.FirstOrDefault(c => c.ID == item.ParentID).Code : string.Empty;
            col++; worksheet.Cells[row, col].Value = item.Code;
            col++; worksheet.Cells[row, col].Value = item.Name;

            foreach (var o in item.ListArea)
            {
                col++; worksheet.Cells[row, col].Value = o.ProvinceName;
                col++; worksheet.Cells[row, col].Value = o.DistrictName;
            }

            worksheet.Row(row).OutlineLevel = lvl;
            if (lvl > 0)
            {
                worksheet.Cells[row, 1, row, totalCol].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                worksheet.Cells[row, 1, row, totalCol].Style.Fill.BackgroundColor.SetColor(Color.GreenYellow);
            }

            var lstChild = lstBody.Where(c => c.ParentID == item.ID).ToList();
            if (lstChild.Count > 0)
            {
                lvl++;
                foreach (var child in lstChild)
                {
                    CATRouting_Excel_Level_Create(lstBody, child, ref worksheet, ref row, ref stt, ref lvl, totalCol);
                }
            }
        }

        public string CATRouting_RouteExcel_Export()
        {
            try
            {
                DTOResult resBody = new DTOResult();
                DTOResult resHeader = new DTOResult();
                ServiceFactory.SVCategory((ISVCategory sv) =>
                {
                    resBody = sv.ExcelRoutingCost_List();
                    resHeader = sv.ExcelRoutingCost_HeaderList();
                });
                string file = "/Uploads/" + "BangGiaTheoCungDuong_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx";

                if (System.IO.File.Exists(HttpContext.Current.Server.MapPath(file)))
                    System.IO.File.Delete(HttpContext.Current.Server.MapPath(file));
                FileInfo exportfile = new FileInfo(HttpContext.Current.Server.MapPath(file));
                using (ExcelPackage pk = new ExcelPackage(exportfile))
                {
                    ExcelWorksheet worksheet = pk.Workbook.Worksheets.Add("Sheet 1");
                    int col = 0, row = 1;

                    #region Header
                    col++; worksheet.Cells[row, col].Value = "STT"; worksheet.Column(col).Width = 5;
                    col++; worksheet.Cells[row, col].Value = "Mã nhóm"; worksheet.Column(col).Width = 15;
                    col++; worksheet.Cells[row, col].Value = "Tên nhóm"; worksheet.Column(col).Width = 25;
                    col++; worksheet.Cells[row, col].Value = "Mã cung đường"; worksheet.Column(col).Width = 15;
                    col++; worksheet.Cells[row, col].Value = "Tên cung đường"; worksheet.Column(col).Width = 25;

                    col++; worksheet.Cells[row, col].Value = "Ghi chú"; worksheet.Column(col).Width = 25;
                    col++; worksheet.Cells[row, col].Value = "Từ"; worksheet.Column(col).Width = 10;
                    col++; worksheet.Cells[row, col].Value = "Từ"; worksheet.Column(col).Width = 20;
                    worksheet.Cells[row, col - 1, row, col].Merge = true;
                    col++; worksheet.Cells[row, col].Value = "Đến"; worksheet.Column(col).Width = 10;
                    col++; worksheet.Cells[row, col].Value = "Đến"; worksheet.Column(col).Width = 20;
                    worksheet.Cells[row, col - 1, row, col].Merge = true;

                    row++; col = 6;
                    col++; worksheet.Cells[row, col].Value = "Mã";
                    col++; worksheet.Cells[row, col].Value = "Tên";
                    col++; worksheet.Cells[row, col].Value = "Mã";
                    col++; worksheet.Cells[row, col].Value = "Tên";

                    int sCol = col;
                    row--;
                    foreach (DTOExcelRouteCost item in resHeader.Data)
                    {
                        col++; worksheet.Cells[row, col].Value = item.GroupCode;
                        worksheet.Column(col).Width = 10;
                    }
                    row++; col = sCol;

                    if (resHeader.Data != null)
                    {
                        var grHeader = resHeader.Data.Cast<DTOExcelRouteCost>().GroupBy(c => c.GroupID).ToList();
                        sCol++;
                        foreach (var gr in grHeader)
                        {
                            foreach (var o in gr)
                            {
                                col++; worksheet.Cells[row, col].Value = o.CostCode;
                            }
                            if (gr.Count() > 1)
                                worksheet.Cells[1, sCol, 1, col].Merge = true;
                            sCol = col + 1;
                        }
                    }

                    worksheet.Cells[1, 1, 2, 1].Merge = true;
                    worksheet.Cells[1, 2, 2, 2].Merge = true;
                    worksheet.Cells[1, 3, 2, 3].Merge = true;
                    worksheet.Cells[1, 4, 2, 4].Merge = true;
                    worksheet.Cells[1, 5, 2, 5].Merge = true;
                    worksheet.Cells[1, 6, 2, 6].Merge = true;

                    worksheet.Cells[1, 1, row, col].Style.Font.Bold = true;
                    worksheet.Cells[1, 1, row, col].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    worksheet.Cells[1, 1, row, col].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    worksheet.Cells[1, 1, row, col].Style.Font.Color.SetColor(Color.White);
                    worksheet.Cells[1, 1, row, col].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    worksheet.Cells[1, 1, row, col].Style.Fill.BackgroundColor.SetColor(Color.Green);

                    worksheet.Cells[1, 1, row, col].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    worksheet.Cells[1, 1, row, col].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    worksheet.Cells[1, 1, row, col].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    worksheet.Cells[1, 1, row, col].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                    worksheet.Cells[1, 1, row, col].Style.Border.Top.Color.SetColor(Color.White);
                    worksheet.Cells[1, 1, row, col].Style.Border.Bottom.Color.SetColor(Color.White);
                    worksheet.Cells[1, 1, row, col].Style.Border.Left.Color.SetColor(Color.White);
                    worksheet.Cells[1, 1, row, col].Style.Border.Right.Color.SetColor(Color.White);

                    #endregion

                    #region Body
                    int stt = 0;
                    foreach (DTOExcelRoute item in resBody.Data)
                    {
                        row++; col = 0; stt++;
                        col++; worksheet.Cells[row, col].Value = stt;
                        col++; worksheet.Cells[row, col].Value = item.ParentCode;
                        col++; worksheet.Cells[row, col].Value = item.ParentName;
                        col++; worksheet.Cells[row, col].Value = item.Code;
                        col++; worksheet.Cells[row, col].Value = item.Name;
                        col++; worksheet.Cells[row, col].Value = item.Note;
                        col++; worksheet.Cells[row, col].Value = item.AreaFromCode;
                        col++; worksheet.Cells[row, col].Value = item.AreaFromName;
                        col++; worksheet.Cells[row, col].Value = item.AreaToCode;
                        col++; worksheet.Cells[row, col].Value = item.AreaToName;
                        if (item.ListCost != null && item.ListCost.Count > 0)
                        {
                            foreach (DTOExcelRouteCost o in resHeader.Data)
                            {
                                col++;
                                var obj = item.ListCost.FirstOrDefault(c => c.ID == o.ID && c.GroupID == o.GroupID);
                                if (obj != null)
                                {
                                    worksheet.Cells[row, col].Value = obj.Cost;
                                }
                            }
                        }
                    }
                    #endregion

                    pk.Save();
                }

                return file;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string CATRouting_AreaExcel_Export()
        {
            try
            {
                DTOResult resBody = new DTOResult();
                ServiceFactory.SVCategory((ISVCategory sv) =>
                {
                    resBody = sv.CATRoutingAreaExcel_List();
                });
                string file = "/Uploads/" + "ChiTietKhuVuc_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx";

                if (System.IO.File.Exists(HttpContext.Current.Server.MapPath(file)))
                    System.IO.File.Delete(HttpContext.Current.Server.MapPath(file));
                FileInfo exportfile = new FileInfo(HttpContext.Current.Server.MapPath(file));
                using (ExcelPackage pk = new ExcelPackage(exportfile))
                {
                    ExcelWorksheet worksheet = pk.Workbook.Worksheets.Add("Sheet1");
                    int col = 0, row = 1;

                    #region Header
                    col++; worksheet.Cells[row, col].Value = "STT"; worksheet.Column(col).Width = 5;
                    col++; worksheet.Cells[row, col].Value = "Khu vực cha"; worksheet.Column(col).Width = 15;
                    col++; worksheet.Cells[row, col].Value = "Mã khu vực"; worksheet.Column(col).Width = 15;
                    col++; worksheet.Cells[row, col].Value = "Tên khu vực"; worksheet.Column(col).Width = 25;
                    col++; worksheet.Cells[row, col].Value = "Mã Follow"; worksheet.Column(col).Width = 25;

                    int noDetail = 0, sCol = col;
                    if (resBody.Data != null)
                        noDetail = resBody.Data.Cast<DTOExcelRouteArea>().ToList().Max(c => c.ListArea.Count);

                    for (var i = 0; i < noDetail; i++)
                    {
                        col++; worksheet.Cells[row, col].Value = "Chi tiết " + (i + 1); worksheet.Column(col).Width = 15;
                        worksheet.Cells[row + 1, col].Value = "Tỉnh thành";
                        col++; worksheet.Cells[row, col].Value = "Chi tiết " + (i + 1); worksheet.Column(col).Width = 15;
                        worksheet.Cells[row + 1, col].Value = "Quận huyện";
                        worksheet.Cells[row, col - 1, row, col].Merge = true;
                    }

                    row++;
                    worksheet.Cells[1, 1, 2, 1].Merge = true;
                    worksheet.Cells[1, 2, 2, 2].Merge = true;
                    worksheet.Cells[1, 3, 2, 3].Merge = true;
                    worksheet.Cells[1, 4, 2, 4].Merge = true;

                    worksheet.Cells[1, 1, row, col].Style.Font.Bold = true;
                    worksheet.Cells[1, 1, row, col].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    worksheet.Cells[1, 1, row, col].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    worksheet.Cells[1, 1, row, col].Style.Font.Color.SetColor(Color.White);
                    worksheet.Cells[1, 1, row, col].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    worksheet.Cells[1, 1, row, col].Style.Fill.BackgroundColor.SetColor(Color.Green);

                    worksheet.Cells[1, 1, row, col].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    worksheet.Cells[1, 1, row, col].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    worksheet.Cells[1, 1, row, col].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    worksheet.Cells[1, 1, row, col].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                    worksheet.Cells[1, 1, row, col].Style.Border.Top.Color.SetColor(Color.White);
                    worksheet.Cells[1, 1, row, col].Style.Border.Bottom.Color.SetColor(Color.White);
                    worksheet.Cells[1, 1, row, col].Style.Border.Left.Color.SetColor(Color.White);
                    worksheet.Cells[1, 1, row, col].Style.Border.Right.Color.SetColor(Color.White);

                    #endregion

                    #region Body
                    int stt = 0;
                    foreach (DTOExcelRouteArea item in resBody.Data.Cast<DTOExcelRouteArea>().ToList().Where(c => c.ParentID < 1).ToList())
                    {
                        int lvl = 0;
                        CATRouting_Excel_Level_Create(resBody.Data.Cast<DTOExcelRouteArea>().ToList(), item, ref worksheet, ref row, ref stt, ref lvl, col);
                    }
                    #endregion

                    pk.Save();
                }

                return file;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public List<DTOExcelRoute> CATRouting_RouteExcel_Check(dynamic dynParam)
        {
            try
            {
                CATFile item = Newtonsoft.Json.JsonConvert.DeserializeObject<CATFile>(dynParam.item.ToString());
                List<DTOExcelRoute> sData = new List<DTOExcelRoute>();
                List<DTOExcelHeader> sHeader = new List<DTOExcelHeader>();
                if (item != null && !string.IsNullOrEmpty(item.FilePath))
                {
                    List<DTOCATCost> resCost = new List<DTOCATCost>();
                    List<CATRoutingArea> resArea = new List<CATRoutingArea>();
                    List<DTOCATRouting> resRoute = new List<DTOCATRouting>();
                    ServiceFactory.SVCategory((ISVCategory sv) =>
                    {
                        resArea = sv.ExcelArea_List();
                        resCost = sv.ExcelCost_List();
                        resRoute = sv.RoutingAll_List().Data.Cast<DTOCATRouting>().ToList();
                    });
                    using (System.IO.FileStream fs = new System.IO.FileStream(HttpContext.Current.Server.MapPath("/" + item.FilePath), System.IO.FileMode.Open, System.IO.FileAccess.Read))
                    {
                        using (var package = new ExcelPackage(fs))
                        {
                            ExcelWorksheet worksheet = ExcelHelper.GetWorksheetByIndex(package, 1);
                            if (worksheet != null)
                            {
                                var totalCol = worksheet.Dimension.End.Column;
                                var totalRow = worksheet.Dimension.End.Row;

                                int row = 2, sGCID = 0;

                                Dictionary<int, int> dicCost = new Dictionary<int, int>();

                                for (int col = 11; col <= totalCol; col++)
                                {
                                    var strCode = ExcelHelper.GetValue(worksheet, row, col);
                                    if (!string.IsNullOrEmpty(strCode))
                                    {
                                        var objCost = resCost.FirstOrDefault(c => c.Code == strCode);
                                        if (objCost != null)
                                        {
                                            strCode = ExcelHelper.GetValue(worksheet, row - 1, col);
                                            if ((string.IsNullOrEmpty(strCode) && objCost.GroupOfCostID != sGCID) || (!string.IsNullOrEmpty(strCode) && objCost.GroupOfCostCode != strCode))
                                            {
                                                //throw new Exception( "Chi phí " + objCost.Code + " không thuộc nhóm " + strCode + ".");
                                            }
                                            if (!dicCost.ContainsValue(objCost.ID))
                                            {
                                                dicCost.Add(col, objCost.ID);
                                                DTOExcelHeader o = new DTOExcelHeader();
                                                o.Level1_ID = objCost.GroupOfCostID;
                                                o.Level1_Text = objCost.GroupOfCostCode;
                                                o.Level2_ID = objCost.ID;
                                                o.Level2_Text = objCost.Code;
                                                sHeader.Add(o);
                                            }
                                            else
                                            {
                                                throw new Exception("Chi phí " + objCost.Code + " trùng.");
                                            }
                                        }
                                        else
                                        {
                                            throw new Exception("Loại chi phí " + strCode + " không tồn tại.");
                                        }
                                    }
                                }
                                for (row = 3; row <= totalRow; row++)
                                {
                                    DTOExcelRoute obj = new DTOExcelRoute();
                                    obj.ExcelRow = row;
                                    obj.ListCost = new List<DTOExcelRouteCost>();
                                    List<string> ListError = new List<string>();
                                    int col = 1; col++;

                                    var str = ExcelHelper.GetValue(worksheet, row, col);
                                    if (!string.IsNullOrEmpty(str))
                                    {
                                        obj.ParentCode = str;
                                        var objCheck = resRoute.FirstOrDefault(c => c.Code == str);
                                        if (objCheck != null)
                                            obj.ParentID = objCheck.ID;
                                    }
                                    col++;

                                    col++; str = ExcelHelper.GetValue(worksheet, row, col);
                                    if (string.IsNullOrEmpty(str))
                                        ListError.Add("Mã cung đường không được để trống.");
                                    else
                                    {
                                        obj.Code = str;
                                        var objCheck = sData.FirstOrDefault(c => c.Code == str);
                                        if (objCheck != null)
                                            ListError.Add("Trùng mã cung đường.");
                                    }

                                    col++; str = ExcelHelper.GetValue(worksheet, row, col);
                                    if (string.IsNullOrEmpty(str))
                                        ListError.Add("Tên cung đường không được để trống.");
                                    else
                                    {
                                        obj.Name = str;
                                        var objCheck = sData.FirstOrDefault(c => c.Name == str);
                                        if (objCheck != null)
                                        {
                                            //obj.ListError.Add("Trùng tên cung đường.");
                                        }
                                    }
                                    col++; str = ExcelHelper.GetValue(worksheet, row, col);
                                    obj.Note = str;

                                    col++; str = ExcelHelper.GetValue(worksheet, row, col);
                                    if (string.IsNullOrEmpty(str))
                                    {
                                        ListError.Add("Mã khu vực đi không được để trống.");
                                    }
                                    else
                                    {
                                        var objArea = resArea.FirstOrDefault(c => c.Code == str);
                                        if (objArea != null)
                                            obj.AreaFromCode = str;
                                        else
                                            ListError.Add("Mã khu vực đi không tồn tại.");
                                    }
                                    col++;
                                    col++; str = ExcelHelper.GetValue(worksheet, row, col);
                                    if (string.IsNullOrEmpty(str))
                                    {
                                        ListError.Add("Mã khu vực đến không được để trống.");
                                    }
                                    else
                                    {
                                        var objArea = resArea.FirstOrDefault(c => c.Code == str);
                                        if (objArea != null)
                                            obj.AreaToCode = str;
                                        else
                                            ListError.Add("Mã khu vực đến không tồn tại.");
                                    }

                                    col++; int s = 0;
                                    //Chi phí
                                    for (; col <= totalCol; )
                                    {
                                        col++; str = ExcelHelper.GetValue(worksheet, row, col);
                                        if (!string.IsNullOrEmpty(str))
                                        {
                                            DTOExcelRouteCost o = new DTOExcelRouteCost();
                                            o.ID = dicCost[col];
                                            o.ColID = s; s++;
                                            var objCost = resCost.FirstOrDefault(c => c.ID == o.ID);

                                            o.CostCode = objCost.Code;
                                            o.GroupCode = objCost.GroupOfCostCode;
                                            o.GroupID = objCost.GroupOfCostID;

                                            try
                                            {
                                                o.Cost = Convert.ToDouble(str);
                                                obj.ListCost.Add(o);
                                            }
                                            catch
                                            {
                                                if (ListError.IndexOf("Nhập giá sai.") > -1)
                                                    ListError.Add("Nhập giá sai.");
                                            }
                                        }
                                    }
                                    obj.ExcelError = string.Empty; obj.ExcelSuccess = true;
                                    if (ListError.Count > 0) { obj.ExcelSuccess = false; obj.ExcelError = string.Join(" ,", ListError); }
                                    sData.Add(obj);
                                }
                            }
                        }
                    }
                }
                return sData;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public List<DTOExcelRouteArea> CATRouting_AreaExcel_Check(dynamic dynParam)
        {
            try
            {
                CATFile item = Newtonsoft.Json.JsonConvert.DeserializeObject<CATFile>(dynParam.item.ToString());
                List<DTOExcelRouteArea> sData = new List<DTOExcelRouteArea>();
                int sRow = 0;
                if (item != null && !string.IsNullOrEmpty(item.FilePath))
                {
                    List<CATProvince> resPro = new List<CATProvince>();
                    List<CATDistrict> resDis = new List<CATDistrict>();
                    ServiceFactory.SVCategory((ISVCategory sv) =>
                    {
                        resPro = sv.ExcelProvince_List();
                        resDis = sv.ExcelDistrict_List();
                    });
                    using (System.IO.FileStream fs = new System.IO.FileStream(HttpContext.Current.Server.MapPath("/" + item.FilePath), System.IO.FileMode.Open, System.IO.FileAccess.Read))
                    {
                        using (var package = new ExcelPackage(fs))
                        {
                            ExcelWorksheet worksheet = ExcelHelper.GetWorksheetByIndex(package, 1);
                            if (worksheet != null)
                            {
                                var totalCol = worksheet.Dimension.End.Column;
                                var totalRow = worksheet.Dimension.End.Row;

                                //sRow = (totalCol - 4) / 2;

                                for (int row = 3; row <= totalRow; row++)
                                {
                                    DTOExcelRouteArea obj = new DTOExcelRouteArea();
                                    obj.ExcelRow = row;
                                    obj.ListArea = new List<DTOCATRoutingAreaDetail>();
                                    List<string> ListError = new List<string>();
                                    int col = 1; col++;

                                    var str = ExcelHelper.GetValue(worksheet, row, col);
                                    if (!string.IsNullOrEmpty(str))
                                    {
                                        obj.ParentCode = str;
                                        var objCheck = sData.FirstOrDefault(c => c.Code == str);
                                        if (objCheck == null)
                                            ListError.Add("Khu vực cha không tồn tại.");
                                    }

                                    col++; str = ExcelHelper.GetValue(worksheet, row, col);
                                    if (string.IsNullOrEmpty(str))
                                        ListError.Add("Mã khu vực không được để trống.");
                                    else
                                    {
                                        obj.Code = str;
                                        var objCheck = sData.FirstOrDefault(c => c.Code == str);
                                        if (objCheck != null)
                                            ListError.Add("Trùng mã khu vực.");
                                    }

                                    col++; str = ExcelHelper.GetValue(worksheet, row, col);
                                    if (string.IsNullOrEmpty(str))
                                        ListError.Add("Tên khu vực không được để trống.");
                                    else
                                    {
                                        obj.Name = str;
                                        var objCheck = sData.FirstOrDefault(c => c.Name == str);
                                        if (objCheck != null)
                                        {
                                            //obj.ListError.Add("Trùng tên khu vực.");
                                        }

                                    }

                                    //Chi tiết
                                    int s = -1;
                                    for (; col <= totalCol; col++)
                                    {
                                        s++;
                                        col++; str = ExcelHelper.GetValue(worksheet, row, col);
                                        if (!string.IsNullOrEmpty(str))
                                        {
                                            DTOCATRoutingAreaDetail o = new DTOCATRoutingAreaDetail();
                                            o.ColID = s;
                                            var objProv = resPro.FirstOrDefault(c => c.ProvinceName == str);
                                            if (objProv == null && ListError.IndexOf("Sai tỉnh thành.") > -1)
                                                ListError.Add("Sai tỉnh thành.");
                                            o.ProvinceName = str;
                                            str = ExcelHelper.GetValue(worksheet, row, col + 1);
                                            if (!string.IsNullOrEmpty(str))
                                            {
                                                o.DistrictName = str;
                                                var objDis = resDis.FirstOrDefault(c => c.DistrictName == str);
                                                if (objDis == null && ListError.IndexOf("Sai quận huyện.") > -1)
                                                    ListError.Add("Sai quận huyện.");
                                            }
                                            obj.ListArea.Add(o);
                                        }
                                    }
                                    obj.ExcelError = string.Empty; obj.ExcelSuccess = true;
                                    if (ListError.Count > 0) { obj.ExcelSuccess = false; obj.ExcelError = string.Join(", ", ListError); }

                                    sData.Add(obj);
                                }
                            }
                        }
                    }
                }

                return sData;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void CATRouting_RouteExcel_Save(dynamic dynParam)
        {
            try
            {
                List<DTOExcelRoute> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DTOExcelRoute>>(dynParam.lst.ToString());
                ServiceFactory.SVCategory((ISVCategory sv) =>
                {
                    sv.ExcelRoutingCost_Save(lst);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void CATRouting_AreaExcel_Save(dynamic dynParam)
        {
            try
            {
                List<DTOExcelRouteArea> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DTOExcelRouteArea>>(dynParam.lst.ToString());
                ServiceFactory.SVCategory((ISVCategory sv) =>
                {
                    sv.CATRoutingAreaExcel_Save(lst);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string CATRouting_ExcelRouteAreaLocation_Export()
        {
            try
            {
                List<DTOExcelRouteAreaLocation> result = new List<DTOExcelRouteAreaLocation>();
                ServiceFactory.SVCategory((ISVCategory sv) =>
                {
                    result = sv.CATRouteAreaLocationExcel_List();
                });
                string file = "/Uploads/" + "ChiTietKhuVuc" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx";

                if (System.IO.File.Exists(HttpContext.Current.Server.MapPath(file)))
                    System.IO.File.Delete(HttpContext.Current.Server.MapPath(file));
                FileInfo exportfile = new FileInfo(HttpContext.Current.Server.MapPath(file));
                using (ExcelPackage pk = new ExcelPackage(exportfile))
                {
                    ExcelWorksheet worksheet = pk.Workbook.Worksheets.Add("Sheet 1");
                    int col = 0, row = 1;

                    #region Header
                    col++; worksheet.Cells[row, col].Value = "STT"; worksheet.Column(col).Width = 5;
                    col++; worksheet.Cells[row, col].Value = "Mã khu vực"; worksheet.Column(col).Width = 15;
                    col++; worksheet.Cells[row, col].Value = "Tên khu vực"; worksheet.Column(col).Width = 25;
                    col++; worksheet.Cells[row, col].Value = "Mã Follow"; worksheet.Column(col).Width = 15;
                    col++; worksheet.Cells[row, col].Value = "Mã địa điểm"; worksheet.Column(col).Width = 25;
                    col++; worksheet.Cells[row, col].Value = "Tên địa điểm"; worksheet.Column(col).Width = 25;
                    col++; worksheet.Cells[row, col].Value = "Địa chỉ"; worksheet.Column(col).Width = 30;
                    col++; worksheet.Cells[row, col].Value = "Tỉnh/Thành phố"; worksheet.Column(col).Width = 20;
                    col++; worksheet.Cells[row, col].Value = "Quận/Huyện"; worksheet.Column(col).Width = 20;

                    ExcelHelper.CreateCellStyle(worksheet, 1, 1, 1, col, false, true, ExcelHelper.ColorGreen, ExcelHelper.ColorBlack, 0, "");

                    #endregion

                    #region data
                    int stt = 1;
                    row = 2;
                    if (result.Count > 0)
                    {
                        foreach (var item in result)
                        {
                            col = 1;
                            worksheet.Cells[row, col].Value = stt;
                            col++; worksheet.Cells[row, col].Value = item.AreaCode;
                            col++; worksheet.Cells[row, col].Value = item.AreaName;
                            col++; worksheet.Cells[row, col].Value = item.FollowID;
                            col++; worksheet.Cells[row, col].Value = item.LocationCode;
                            col++; worksheet.Cells[row, col].Value = item.LocationName;
                            col++; worksheet.Cells[row, col].Value = item.LocationAdress;
                            col++; worksheet.Cells[row, col].Value = item.ProvinceName;
                            col++; worksheet.Cells[row, col].Value = item.DistrictName;
                            row++;
                            stt++;
                        }
                    }
                    for (int i = 1; i <= worksheet.Dimension.End.Row; i++)
                    {
                        for (int j = 1; j <= worksheet.Dimension.End.Column; j++)
                        {
                            worksheet.Cells[i, j].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                        }
                    }
                    #endregion

                    pk.Save();
                }

                return file;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string CATRouting_Excel_Export()
        {
            try
            {
                DTOCATRoutingExcelData result = new DTOCATRoutingExcelData();
                ServiceFactory.SVCategory((ISVCategory sv) =>
                {
                    result = sv.CATRouting_ExcelData();
                });

                List<DTOCATRouting> ListRou = result.ListRoute;
                List<DTOCustomer> ListCus = result.ListCustomer;
                List<DTOCATContractRouting> ListContractRouting = result.ListContractRouting;
                List<DTOCATContract> ListContract = result.ListContract;

                string file = "/Uploads/" + "CATRouting_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx";

                if (System.IO.File.Exists(HttpContext.Current.Server.MapPath(file)))
                    System.IO.File.Delete(HttpContext.Current.Server.MapPath(file));
                FileInfo exportfile = new FileInfo(HttpContext.Current.Server.MapPath(file));
                using (ExcelPackage pk = new ExcelPackage(exportfile))
                {
                    ExcelWorksheet worksheet = pk.Workbook.Worksheets.Add("Sheet 1");
                    int col = 0, row = 1;

                    #region Header
                    col++; worksheet.Cells[row, col].Value = "STT"; worksheet.Column(col).Width = 5;
                    col++; worksheet.Cells[row, col].Value = "Mã cung đường"; worksheet.Column(col).Width = 25;
                    col++; worksheet.Cells[row, col].Value = "Tên cung đường"; worksheet.Column(col).Width = 35;
                    col++; worksheet.Cells[row, col].Value = "Độ dài"; worksheet.Column(col).Width = 15;
                    col++; worksheet.Cells[row, col].Value = "Thời gian"; worksheet.Column(col).Width = 15;
                    col++; worksheet.Cells[row, col].Value = "Điểm đi"; worksheet.Column(col).Width = 15;
                    col++; worksheet.Cells[row, col].Value = "Điểm đến"; worksheet.Column(col).Width = 15;
                    col++; worksheet.Cells[row, col].Value = "Khu vực đi"; worksheet.Column(col).Width = 20;
                    col++; worksheet.Cells[row, col].Value = "Khu vực đến"; worksheet.Column(col).Width = 20;

                    int contractCount = 0;
                    int col_cus = col;
                    int rowTitleCon = 2;

                    foreach (var cus in ListCus)
                    {
                        List<DTOCATContract> listCon = ListContract.Where(c => c.CustomerID == cus.ID).ToList();
                        if (listCon != null && listCon.Count() > 0)
                        {
                            contractCount = listCon.Count() - 1;
                            col_cus++; worksheet.Cells[row, col_cus].Value = cus.CustomerName; worksheet.Column(col_cus).Width = 20;
                            worksheet.Cells[row, col_cus, row, col_cus + contractCount].Merge = true;
                            col_cus += contractCount;
                            foreach (var con in listCon)
                            {
                                col++; worksheet.Cells[rowTitleCon, col].Value = con.ContractNo; worksheet.Column(col).Width = 20;
                            }
                        }
                    }
                    for (int i = 1; i < 10; i++)
                    {
                        worksheet.Cells[1, i, 2, i].Merge = true;
                    }

                    ExcelHelper.CreateCellStyle(worksheet, 1, 1, 2, col, false, true, ExcelHelper.ColorGreen, ExcelHelper.ColorWhite, 0, "");


                    worksheet.Cells[1, 1, 2, col].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    worksheet.Cells[1, 1, 2, col].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    #endregion

                    #region Body
                    row = 2;
                    int stt = 0;
                    foreach (DTOCATRouting rou in ListRou)
                    {
                        row++; col = 0; stt++;
                        col++; worksheet.Cells[row, col].Value = stt;
                        col++; worksheet.Cells[row, col].Value = rou.Code;
                        col++; worksheet.Cells[row, col].Value = rou.RoutingName;
                        col++; worksheet.Cells[row, col].Value = rou.EDistance;
                        col++; worksheet.Cells[row, col].Value = rou.EHours;
                        col++; worksheet.Cells[row, col].Value = rou.LocationFromName;
                        col++; worksheet.Cells[row, col].Value = rou.LocationToName;
                        col++; worksheet.Cells[row, col].Value = rou.AreaFromName;
                        col++; worksheet.Cells[row, col].Value = rou.AreaToName;

                        foreach (var cus in ListCus)
                        {
                            List<DTOCATContract> listCon = ListContract.Where(c => c.CustomerID == cus.ID).ToList();
                            if (listCon != null && listCon.Count() > 0)
                            {
                                foreach (var con in listCon)
                                {
                                    DTOCATContractRouting conRouting = ListContractRouting.Where(c => c.ContractID == con.ID && c.RoutingID == rou.ID).FirstOrDefault();

                                    col++;
                                    if (conRouting != null)
                                    {
                                        worksheet.Cells[row, col].Value = conRouting.Code; worksheet.Column(col).Width = 20;
                                    }
                                }
                            }
                        }

                    }
                    #endregion

                    for (int i = 1; i <= worksheet.Dimension.End.Row; i++)
                    {
                        for (int j = 1; j <= worksheet.Dimension.End.Column; j++)
                        {
                            worksheet.Cells[i, j].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                        }
                    }
                    pk.Save();
                }
                return file;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DTOResult CATRouting_AllCustomerList()
        {
            try
            {
                DTOResult result = new DTOResult();
                ServiceFactory.SVCategory((ISVCategory sv) =>
                {
                    result = sv.CATRouting_AllCustomerList();
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #endregion

        #region CATTransportMode

        public DTOResult TransportMode_List(dynamic dynParam)
        {
            try
            {
                string request = dynParam.request.ToString();
                var result = new DTOResult();
                ServiceFactory.SVCategory((ISVCategory sv) =>
                {
                    result = sv.TransportMode_List(request);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public CATTransportMode TransportMode_Get(dynamic dynParam)
        {
            try
            {
                int id = (int)dynParam.ID;
                var result = default(CATTransportMode);
                ServiceFactory.SVCategory((ISVCategory sv) =>
                {
                    result = sv.TransportMode_Get(id);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public int TransportMode_Save(dynamic dynParam)
        {
            try
            {
                CATTransportMode item = Newtonsoft.Json.JsonConvert.DeserializeObject<CATTransportMode>(dynParam.item.ToString());
                int result = -1;
                ServiceFactory.SVCategory((ISVCategory sv) =>
                {
                    result = sv.TransportMode_Save(item);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void TransportMode_Delete(dynamic dynParam)
        {
            try
            {
                int id = (int)dynParam.ID;
                ServiceFactory.SVCategory((ISVCategory sv) =>
                {
                    sv.TransportMode_Delete(id);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DTOResult TransportMode_SYSVarTransportMode()
        {
            try
            {
                var result = default(DTOResult);
                ServiceFactory.SVCategory((ISVCategory sv) =>
                {
                    result = sv.ALL_SysVar(SYSVarType.TransportMode);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public SYSExcel TransportMode_ExcelInit(dynamic dynParam)
        {
            try
            {
                var functionid = (int)dynParam.functionid;
                var functionkey = dynParam.functionkey.ToString();
                var isreload = (bool)dynParam.isreload;

                var result = default(SYSExcel);

                ServiceFactory.SVCategory((ISVCategory sv) =>
                {
                    result = sv.TransportMode_ExcelInit(functionid, functionkey, isreload);
                });
                if (result != null && !string.IsNullOrEmpty(result.Data))
                {
                    result.Worksheets = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Worksheet>>(result.Data);
                }
                else
                {
                    result = new SYSExcel();
                    result.Worksheets = new List<Worksheet>();
                }
                result.Data = "";
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public Row TransportMode_ExcelChange(dynamic dynParam)
        {
            try
            {
                var id = (long)dynParam.id;
                var row = (int)dynParam.row;
                List<Cell> cells = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Cell>>(dynParam.cells.ToString());
                List<string> lstMessageError = Newtonsoft.Json.JsonConvert.DeserializeObject<List<string>>(dynParam.lstMessageError.ToString());

                var result = new Row();
                if (id > 0 && cells.Count > 0 && row > 0)
                {
                    ServiceFactory.SVCategory((ISVCategory sv) =>
                    {
                        result = sv.TransportMode_ExcelChange(id, row, cells, lstMessageError);
                    });
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public SYSExcel TransportMode_ExcelImport(dynamic dynParam)
        {
            try
            {
                var id = (long)dynParam.id;
                List<Worksheet> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Worksheet>>(dynParam.worksheets.ToString());
                List<string> lstMessageError = Newtonsoft.Json.JsonConvert.DeserializeObject<List<string>>(dynParam.lstMessageError.ToString());

                var result = default(SYSExcel);
                if (id > 0 && lst.Count > 0 && lst[0].Rows.Count > 1)
                {
                    ServiceFactory.SVCategory((ISVCategory sv) =>
                    {
                        result = sv.TransportMode_ExcelImport(id, lst[0].Rows, lstMessageError);
                    });
                }
                if (result != null && !string.IsNullOrEmpty(result.Data))
                {
                    result.Worksheets = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Worksheet>>(result.Data);
                }
                else
                {
                    result = new SYSExcel();
                    result.Worksheets = new List<Worksheet>();
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public bool TransportMode_ExcelApprove(dynamic dynParam)
        {
            try
            {
                var id = (long)dynParam.id;

                var result = false;
                if (id > 0)
                {
                    ServiceFactory.SVCategory((ISVCategory sv) =>
                    {
                        result = sv.TransportMode_ExcelApprove(id);
                    });

                    //if (result != null && !string.IsNullOrEmpty(result.Data))
                    //{
                    //    result.Worksheets = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Worksheet>>(result.Data);
                    //}
                    //else
                    //{
                    //    result = new SYSExcel();
                    //    result.Worksheets = new List<Worksheet>();
                    //}
                    //result.Data = "";
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region CATServiceOfOrder

        public DTOResult ServiceOfOrder_List(dynamic dynParam)
        {
            try
            {
                string request = dynParam.request.ToString();
                var result = new DTOResult();
                ServiceFactory.SVCategory((ISVCategory sv) =>
                {
                    result = sv.ServiceOfOrder_List(request);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public CATServiceOfOrder ServiceOfOrder_Get(dynamic dynParam)
        {
            try
            {
                int id = (int)dynParam.ID;
                var result = default(CATServiceOfOrder);
                ServiceFactory.SVCategory((ISVCategory sv) =>
                {
                    result = sv.ServiceOfOrder_Get(id);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public int ServiceOfOrder_Save(dynamic dynParam)
        {
            try
            {
                CATServiceOfOrder item = Newtonsoft.Json.JsonConvert.DeserializeObject<CATServiceOfOrder>(dynParam.item.ToString());
                int result = -1;
                ServiceFactory.SVCategory((ISVCategory sv) =>
                {
                    result = sv.ServiceOfOrder_Save(item);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void ServiceOfOrder_Delete(dynamic dynParam)
        {
            try
            {
                int id = (int)dynParam.ID;
                ServiceFactory.SVCategory((ISVCategory sv) =>
                {
                    sv.ServiceOfOrder_Delete(id);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DTOResult ServiceOfOrder_SYSVarServiceOfOrder()
        {
            try
            {
                var result = default(DTOResult);
                ServiceFactory.SVCategory((ISVCategory sv) =>
                {
                    result = sv.ALL_SysVar(SYSVarType.ServiceOfOrder);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public SYSExcel ServiceOfOrder_ExcelInit(dynamic dynParam)
        {
            try
            {
                var functionid = (int)dynParam.functionid;
                var functionkey = dynParam.functionkey.ToString();
                var isreload = (bool)dynParam.isreload;

                var result = default(SYSExcel);

                ServiceFactory.SVCategory((ISVCategory sv) =>
                {
                    result = sv.ServiceOfOrder_ExcelInit(functionid, functionkey, isreload);
                });
                if (result != null && !string.IsNullOrEmpty(result.Data))
                {
                    result.Worksheets = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Worksheet>>(result.Data);
                }
                else
                {
                    result = new SYSExcel();
                    result.Worksheets = new List<Worksheet>();
                }
                result.Data = "";
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public Row ServiceOfOrder_ExcelChange(dynamic dynParam)
        {
            try
            {
                var id = (long)dynParam.id;
                var row = (int)dynParam.row;
                List<Cell> cells = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Cell>>(dynParam.cells.ToString());
                List<string> lstMessageError = Newtonsoft.Json.JsonConvert.DeserializeObject<List<string>>(dynParam.lstMessageError.ToString());

                var result = new Row();
                if (id > 0 && cells.Count > 0 && row > 0)
                {
                    ServiceFactory.SVCategory((ISVCategory sv) =>
                    {
                        result = sv.ServiceOfOrder_ExcelChange(id, row, cells, lstMessageError);
                    });
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public SYSExcel ServiceOfOrder_ExcelImport(dynamic dynParam)
        {
            try
            {
                var id = (long)dynParam.id;
                List<Worksheet> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Worksheet>>(dynParam.worksheets.ToString());
                List<string> lstMessageError = Newtonsoft.Json.JsonConvert.DeserializeObject<List<string>>(dynParam.lstMessageError.ToString());

                var result = default(SYSExcel);
                if (id > 0 && lst.Count > 0 && lst[0].Rows.Count > 1)
                {
                    ServiceFactory.SVCategory((ISVCategory sv) =>
                    {
                        result = sv.ServiceOfOrder_ExcelImport(id, lst[0].Rows, lstMessageError);
                    });
                }
                if (result != null && !string.IsNullOrEmpty(result.Data))
                {
                    result.Worksheets = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Worksheet>>(result.Data);
                }
                else
                {
                    result = new SYSExcel();
                    result.Worksheets = new List<Worksheet>();
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public bool ServiceOfOrder_ExcelApprove(dynamic dynParam)
        {
            try
            {
                var id = (long)dynParam.id;

                var result = false;
                if (id > 0)
                {
                    ServiceFactory.SVCategory((ISVCategory sv) =>
                    {
                        result = sv.ServiceOfOrder_ExcelApprove(id);
                    });

                    //if (result != null && !string.IsNullOrEmpty(result.Data))
                    //{
                    //    result.Worksheets = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Worksheet>>(result.Data);
                    //}
                    //else
                    //{
                    //    result = new SYSExcel();
                    //    result.Worksheets = new List<Worksheet>();
                    //}
                    //result.Data = "";
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region CATDriver

        public DTOResult Driver_List(dynamic dynParam)
        {
            try
            {
                string request = dynParam.request.ToString();
                var result = new DTOResult();
                ServiceFactory.SVCategory((ISVCategory sv) =>
                {
                    result = sv.Driver_List(request);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public CATDriver Driver_Get(dynamic dynParam)
        {
            try
            {
                int id = (int)dynParam.ID;
                var result = default(CATDriver);
                ServiceFactory.SVCategory((ISVCategory sv) =>
                {
                    result = sv.Driver_Get(id);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public int Driver_Save(dynamic dynParam)
        {
            try
            {
                CATDriver item = Newtonsoft.Json.JsonConvert.DeserializeObject<CATDriver>(dynParam.item.ToString());
                int result = -1;
                ServiceFactory.SVCategory((ISVCategory sv) =>
                {
                    result = sv.Driver_Save(item);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void Driver_Delete(dynamic dynParam)
        {
            try
            {
                int id = (int)dynParam.ID;
                ServiceFactory.SVCategory((ISVCategory sv) =>
                {
                    sv.Driver_Delete(id);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public SYSExcel Driver_ExcelInit(dynamic dynParam)
        {
            try
            {
                var functionid = (int)dynParam.functionid;
                var functionkey = dynParam.functionkey.ToString();
                var isreload = (bool)dynParam.isreload;

                var result = default(SYSExcel);

                ServiceFactory.SVCategory((ISVCategory sv) =>
                {
                    result = sv.Driver_ExcelInit(functionid, functionkey, isreload);
                });
                if (result != null && !string.IsNullOrEmpty(result.Data))
                {
                    result.Worksheets = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Worksheet>>(result.Data);
                }
                else
                {
                    result = new SYSExcel();
                    result.Worksheets = new List<Worksheet>();
                }
                result.Data = "";
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public Row Driver_ExcelChange(dynamic dynParam)
        {
            try
            {
                var id = (long)dynParam.id;
                var row = (int)dynParam.row;
                List<Cell> cells = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Cell>>(dynParam.cells.ToString());
                List<string> lstMessageError = Newtonsoft.Json.JsonConvert.DeserializeObject<List<string>>(dynParam.lstMessageError.ToString());

                var result = default(Row);
                if (id > 0 && cells.Count > 0 && row > 0 && row > 0)
                {
                    ServiceFactory.SVCategory((ISVCategory sv) =>
                    {
                        result = sv.Driver_ExcelChange(id, row, cells, lstMessageError);
                    });
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public SYSExcel Driver_ExcelImport(dynamic dynParam)
        {
            try
            {
                var id = (long)dynParam.id;
                List<Worksheet> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Worksheet>>(dynParam.worksheets.ToString());
                List<string> lstMessageError = Newtonsoft.Json.JsonConvert.DeserializeObject<List<string>>(dynParam.lstMessageError.ToString());

                var result = default(SYSExcel);
                if (id > 0 && lst.Count > 0 && lst[0].Rows.Count > 1)
                {
                    ServiceFactory.SVCategory((ISVCategory sv) =>
                    {
                        result = sv.Driver_ExcelImport(id, lst[0].Rows, lstMessageError);
                    });
                }
                if (result != null && !string.IsNullOrEmpty(result.Data))
                {
                    result.Worksheets = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Worksheet>>(result.Data);
                }
                else
                {
                    result = new SYSExcel();
                    result.Worksheets = new List<Worksheet>();
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public bool Driver_ExcelApprove(dynamic dynParam)
        {
            try
            {
                var id = (long)dynParam.id;
                var result = false;

                if (id > 0)
                {
                    ServiceFactory.SVCategory((ISVCategory sv) =>
                    {
                        result = sv.Driver_ExcelApprove(id);
                    });
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region CATCountry

        public DTOResult CATCountry_Read(dynamic dynParam)
        {
            try
            {
                string request = dynParam.request.ToString();
                var result = new DTOResult();
                ServiceFactory.SVCategory((ISVCategory sv) =>
                {
                    result = sv.CATCountry_List(request);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public int CATCountry_Update(dynamic dynParam)
        {
            try
            {
                CATCountry item = Newtonsoft.Json.JsonConvert.DeserializeObject<CATCountry>(dynParam.item.ToString());
                int result = -1;
                ServiceFactory.SVCategory((ISVCategory sv) =>
                {
                    result = sv.CATCountry_Save(item);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public CATCountry CATCountry_Get(dynamic dynParam)
        {
            try
            {
                int id = (int)dynParam.ID;
                var result = default(CATCountry);
                ServiceFactory.SVCategory((ISVCategory sv) =>
                {
                    result = sv.CATCountry_Get(id);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void CATCountry_Destroy(dynamic dynParam)
        {
            try
            {
                CATCountry item = Newtonsoft.Json.JsonConvert.DeserializeObject<CATCountry>(dynParam.item.ToString());
                ServiceFactory.SVCategory((ISVCategory sv) =>
                {
                    sv.CATCountry_Delete(item);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public SYSExcel CATCountry_ExcelInit(dynamic dynParam)
        {
            try
            {
                var functionid = (int)dynParam.functionid;
                var functionkey = dynParam.functionkey.ToString();

                var result = default(SYSExcel);

                ServiceFactory.SVCategory((ISVCategory sv) =>
                {
                    result = sv.CATCountry_ExcelInit(functionid, functionkey);
                });
                if (result != null && !string.IsNullOrEmpty(result.Data))
                {
                    result.Worksheets = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Worksheet>>(result.Data);
                }
                else
                {
                    result = new SYSExcel();
                    result.Worksheets = new List<Worksheet>();
                }
                result.Data = "";
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public List<Row> CATCountry_ExcelChange(dynamic dynParam)
        {
            try
            {
                var id = (long)dynParam.id;
                var row = (int)dynParam.row;
                var col = (int)dynParam.col;
                var val = dynParam.val.ToString();
                List<Cell> cells = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Cell>>(dynParam.cells.ToString());

                var result = new List<Row>();
                if (id > 0 && cells.Count > 0 && row > 0 && col > 0)
                {
                    ServiceFactory.SVCategory((ISVCategory sv) =>
                    {
                        result = sv.CATCountry_ExcelChange(id, row, col, val);
                    });
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public SYSExcel CATCountry_ExcelImport(dynamic dynParam)
        {
            try
            {
                var id = (long)dynParam.id;
                List<Worksheet> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Worksheet>>(dynParam.worksheets.ToString());

                var result = default(SYSExcel);
                if (id > 0 && lst.Count > 0 && lst[0].Rows.Count > 1)
                {
                    ServiceFactory.SVCategory((ISVCategory sv) =>
                    {
                        result = sv.CATCountry_ExcelImport(id, lst[0].Rows);
                    });
                }
                if (result != null && !string.IsNullOrEmpty(result.Data))
                {
                    result.Worksheets = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Worksheet>>(result.Data);
                }
                else
                {
                    result = new SYSExcel();
                    result.Worksheets = new List<Worksheet>();
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public SYSExcel CATCountry_ExcelApprove(dynamic dynParam)
        {
            try
            {
                var id = (long)dynParam.id;

                var result = default(SYSExcel);
                if (id > 0)
                {
                    ServiceFactory.SVCategory((ISVCategory sv) =>
                    {
                        result = sv.CATCountry_ExcelApprove(id);
                    });

                    if (result != null && !string.IsNullOrEmpty(result.Data))
                    {
                        result.Worksheets = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Worksheet>>(result.Data);
                    }
                    else
                    {
                        result = new SYSExcel();
                        result.Worksheets = new List<Worksheet>();
                    }
                    result.Data = "";
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region CATDistrict
        public DTOResult CATDistrict_Read(dynamic dynParam)
        {
            try
            {
                string request = dynParam.request.ToString();
                var result = new DTOResult();
                ServiceFactory.SVCategory((ISVCategory sv) =>
                {
                    result = sv.CATDistrict_List(request);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public int CATDistrict_Update(dynamic dynParam)
        {
            try
            {
                CATDistrict item = Newtonsoft.Json.JsonConvert.DeserializeObject<CATDistrict>(dynParam.item.ToString());
                int result = -1;
                ServiceFactory.SVCategory((ISVCategory sv) =>
                {
                    result = sv.CATDistrict_Save(item);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public CATDistrict CATDistrict_Get(dynamic dynParam)
        {
            try
            {
                int id = (int)dynParam.ID;
                var result = default(CATDistrict);
                ServiceFactory.SVCategory((ISVCategory sv) =>
                {
                    result = sv.CATDistrict_Get(id);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void CATDistrict_Destroy(dynamic dynParam)
        {
            try
            {
                CATDistrict item = Newtonsoft.Json.JsonConvert.DeserializeObject<CATDistrict>(dynParam.item.ToString());
                ServiceFactory.SVCategory((ISVCategory sv) =>
                {
                    sv.CATDistrict_Delete(item);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public SYSExcel CATDistrict_ExcelInit(dynamic dynParam)
        {
            try
            {
                var functionid = (int)dynParam.functionid;
                var functionkey = dynParam.functionkey.ToString();

                var result = default(SYSExcel);

                ServiceFactory.SVCategory((ISVCategory sv) =>
                {
                    result = sv.CATDistrict_ExcelInit(functionid, functionkey);
                });
                if (result != null && !string.IsNullOrEmpty(result.Data))
                {
                    result.Worksheets = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Worksheet>>(result.Data);
                }
                else
                {
                    result = new SYSExcel();
                    result.Worksheets = new List<Worksheet>();
                }
                result.Data = "";
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public List<Row> CATDistrict_ExcelChange(dynamic dynParam)
        {
            try
            {
                var id = (long)dynParam.id;
                var row = (int)dynParam.row;
                var col = (int)dynParam.col;
                var val = dynParam.val.ToString();
                List<Cell> cells = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Cell>>(dynParam.cells.ToString());

                var result = new List<Row>();
                if (id > 0 && cells.Count > 0 && row > 0 && col > 0)
                {
                    ServiceFactory.SVCategory((ISVCategory sv) =>
                    {
                        result = sv.CATDistrict_ExcelChange(id, row, col, val);
                    });
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public SYSExcel CATDistrict_ExcelImport(dynamic dynParam)
        {
            try
            {
                var id = (long)dynParam.id;
                List<Worksheet> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Worksheet>>(dynParam.worksheets.ToString());

                var result = default(SYSExcel);
                if (id > 0 && lst.Count > 0 && lst[0].Rows.Count > 1)
                {
                    ServiceFactory.SVCategory((ISVCategory sv) =>
                    {
                        result = sv.CATDistrict_ExcelImport(id, lst[0].Rows);
                    });
                }
                if (result != null && !string.IsNullOrEmpty(result.Data))
                {
                    result.Worksheets = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Worksheet>>(result.Data);
                }
                else
                {
                    result = new SYSExcel();
                    result.Worksheets = new List<Worksheet>();
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public SYSExcel CATDistrict_ExcelApprove(dynamic dynParam)
        {
            try
            {
                var id = (long)dynParam.id;

                var result = default(SYSExcel);
                if (id > 0)
                {
                    ServiceFactory.SVCategory((ISVCategory sv) =>
                    {
                        result = sv.CATDistrict_ExcelApprove(id);
                    });

                    if (result != null && !string.IsNullOrEmpty(result.Data))
                    {
                        result.Worksheets = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Worksheet>>(result.Data);
                    }
                    else
                    {
                        result = new SYSExcel();
                        result.Worksheets = new List<Worksheet>();
                    }
                    result.Data = "";
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region CATProvince
        public DTOResult CATProvince_Read(dynamic dynParam)
        {
            try
            {
                string request = dynParam.request.ToString();
                var result = new DTOResult();
                ServiceFactory.SVCategory((ISVCategory sv) =>
                {
                    result = sv.CATProvince_List(request);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public int CATProvince_Update(dynamic dynParam)
        {
            try
            {
                CATProvince item = Newtonsoft.Json.JsonConvert.DeserializeObject<CATProvince>(dynParam.item.ToString());
                int result = -1;
                ServiceFactory.SVCategory((ISVCategory sv) =>
                {
                    result = sv.CATProvince_Save(item);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public CATProvince CATProvince_Get(dynamic dynParam)
        {
            try
            {
                int id = (int)dynParam.ID;
                var result = default(CATProvince);
                ServiceFactory.SVCategory((ISVCategory sv) =>
                {
                    result = sv.CATProvince_Get(id);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void CATProvince_Destroy(dynamic dynParam)
        {
            try
            {
                CATProvince item = Newtonsoft.Json.JsonConvert.DeserializeObject<CATProvince>(dynParam.item.ToString());
                ServiceFactory.SVCategory((ISVCategory sv) =>
                {
                    sv.CATProvince_Delete(item);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public SYSExcel CATProvince_ExcelInit(dynamic dynParam)
        {
            try
            {
                var functionid = (int)dynParam.functionid;
                var functionkey = dynParam.functionkey.ToString();

                var result = default(SYSExcel);

                ServiceFactory.SVCategory((ISVCategory sv) =>
                {
                    result = sv.CATProvince_ExcelInit(functionid, functionkey);
                });
                if (result != null && !string.IsNullOrEmpty(result.Data))
                {
                    result.Worksheets = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Worksheet>>(result.Data);
                }
                else
                {
                    result = new SYSExcel();
                    result.Worksheets = new List<Worksheet>();
                }
                result.Data = "";
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public List<Row> CATProvince_ExcelChange(dynamic dynParam)
        {
            try
            {
                var id = (long)dynParam.id;
                var row = (int)dynParam.row;
                var col = (int)dynParam.col;
                var val = dynParam.val.ToString();
                List<Cell> cells = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Cell>>(dynParam.cells.ToString());

                var result = new List<Row>();
                if (id > 0 && cells.Count > 0 && row > 0 && col > 0)
                {
                    ServiceFactory.SVCategory((ISVCategory sv) =>
                    {
                        result = sv.CATProvince_ExcelChange(id, row, col, val);
                    });
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public SYSExcel CATProvince_ExcelImport(dynamic dynParam)
        {
            try
            {
                var id = (long)dynParam.id;
                List<Worksheet> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Worksheet>>(dynParam.worksheets.ToString());

                var result = default(SYSExcel);
                if (id > 0 && lst.Count > 0 && lst[0].Rows.Count > 1)
                {
                    ServiceFactory.SVCategory((ISVCategory sv) =>
                    {
                        result = sv.CATProvince_ExcelImport(id, lst[0].Rows);
                    });
                }
                if (result != null && !string.IsNullOrEmpty(result.Data))
                {
                    result.Worksheets = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Worksheet>>(result.Data);
                }
                else
                {
                    result = new SYSExcel();
                    result.Worksheets = new List<Worksheet>();
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public SYSExcel CATProvince_ExcelApprove(dynamic dynParam)
        {
            try
            {
                var id = (long)dynParam.id;

                var result = default(SYSExcel);
                if (id > 0)
                {
                    ServiceFactory.SVCategory((ISVCategory sv) =>
                    {
                        result = sv.CATProvince_ExcelApprove(id);
                    });

                    if (result != null && !string.IsNullOrEmpty(result.Data))
                    {
                        result.Worksheets = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Worksheet>>(result.Data);
                    }
                    else
                    {
                        result = new SYSExcel();
                        result.Worksheets = new List<Worksheet>();
                    }
                    result.Data = "";
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region CATCurrency

        public DTOResult CATCurrency_List(dynamic dynParam)
        {
            try
            {
                string request = dynParam.request.ToString();
                var result = new DTOResult();
                ServiceFactory.SVCategory((ISVCategory sv) =>
                {
                    result = sv.CATCurrency_List(request);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public int CATCurrency_Save(dynamic dynParam)
        {
            try
            {
                CATCurrency item = Newtonsoft.Json.JsonConvert.DeserializeObject<CATCurrency>(dynParam.item.ToString());
                ServiceFactory.SVCategory((ISVCategory sv) =>
                {
                    item.ID = sv.CATCurrency_Save(item);
                });
                return item.ID;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public CATCurrency CATCurrency_Get(dynamic dynParam)
        {
            try
            {
                int id = (int)dynParam.ID;
                var result = default(CATCurrency);
                ServiceFactory.SVCategory((ISVCategory sv) =>
                {
                    result = sv.CATCurrency_Get(id);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void CATCurrency_Delete(dynamic dynParam)
        {
            try
            {
                CATCurrency item = Newtonsoft.Json.JsonConvert.DeserializeObject<CATCurrency>(dynParam.item.ToString());
                ServiceFactory.SVCategory((ISVCategory sv) =>
                {
                    sv.CATCurrency_Delete(item);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public List<CATCurrency> CATCurrency_AllList()
        {
            try
            {
                var result = new List<CATCurrency>();
                ServiceFactory.SVCategory((ISVCategory sv) =>
                {
                    result = sv.CATCurrency_AllList();
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public SYSExcel CATCurrency_ExcelInit(dynamic dynParam)
        {
            try
            {
                var functionid = (int)dynParam.functionid;
                var functionkey = dynParam.functionkey.ToString();

                var result = default(SYSExcel);

                ServiceFactory.SVCategory((ISVCategory sv) =>
                {
                    result = sv.CATCurrency_ExcelInit(functionid, functionkey);
                });
                if (result != null && !string.IsNullOrEmpty(result.Data))
                {
                    result.Worksheets = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Worksheet>>(result.Data);
                }
                else
                {
                    result = new SYSExcel();
                    result.Worksheets = new List<Worksheet>();
                }
                result.Data = "";
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public List<Row> CATCurrency_ExcelChange(dynamic dynParam)
        {
            try
            {
                var id = (long)dynParam.id;
                var row = (int)dynParam.row;
                var col = (int)dynParam.col;
                var val = dynParam.val.ToString();
                List<Cell> cells = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Cell>>(dynParam.cells.ToString());

                var result = new List<Row>();
                if (id > 0 && cells.Count > 0 && row > 0 && col > 0)
                {
                    ServiceFactory.SVCategory((ISVCategory sv) =>
                    {
                        result = sv.CATCurrency_ExcelChange(id, row, col, val);
                    });
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public SYSExcel CATCurrency_ExcelImport(dynamic dynParam)
        {
            try
            {
                var id = (long)dynParam.id;
                List<Worksheet> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Worksheet>>(dynParam.worksheets.ToString());

                var result = default(SYSExcel);
                if (id > 0 && lst.Count > 0 && lst[0].Rows.Count > 1)
                {
                    ServiceFactory.SVCategory((ISVCategory sv) =>
                    {
                        result = sv.CATCurrency_ExcelImport(id, lst[0].Rows);
                    });
                }
                if (result != null && !string.IsNullOrEmpty(result.Data))
                {
                    result.Worksheets = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Worksheet>>(result.Data);
                }
                else
                {
                    result = new SYSExcel();
                    result.Worksheets = new List<Worksheet>();
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public SYSExcel CATCurrency_ExcelApprove(dynamic dynParam)
        {
            try
            {
                var id = (long)dynParam.id;

                var result = default(SYSExcel);
                if (id > 0)
                {
                    ServiceFactory.SVCategory((ISVCategory sv) =>
                    {
                        result = sv.CATCurrency_ExcelApprove(id);
                    });

                    if (result != null && !string.IsNullOrEmpty(result.Data))
                    {
                        result.Worksheets = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Worksheet>>(result.Data);
                    }
                    else
                    {
                        result = new SYSExcel();
                        result.Worksheets = new List<Worksheet>();
                    }
                    result.Data = "";
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion
                                                                             
        #region CATReason

        public DTOResult CATReason_List(dynamic dynParam)
        {
            try
            {
                string request = dynParam.request.ToString();
                var result = new DTOResult();
                ServiceFactory.SVCategory((ISVCategory sv) =>
                {
                    result = sv.CATReason_List(request);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public int CATReason_Save(dynamic dynParam)
        {
            try
            {
                DTOCATReason item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOCATReason>(dynParam.item.ToString());
                ServiceFactory.SVCategory((ISVCategory sv) =>
                {
                    item.ID = sv.CATReason_Save(item);
                });
                return item.ID;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOCATReason CATReason_Get(dynamic dynParam)
        {
            try
            {
                int id = (int)dynParam.ID;
                var result = default(DTOCATReason);
                ServiceFactory.SVCategory((ISVCategory sv) =>
                {
                    result = sv.CATReason_Get(id);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void CATReason_Delete(dynamic dynParam)
        {
            try
            {
                DTOCATReason item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOCATReason>(dynParam.item.ToString());
                ServiceFactory.SVCategory((ISVCategory sv) =>
                {
                    sv.CATReason_Delete(item);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public SYSExcel CATReason_ExcelInit(dynamic dynParam)
        {
            try
            {
                var functionid = (int)dynParam.functionid;
                var functionkey = dynParam.functionkey.ToString();
                var isreload = (bool)dynParam.isreload;

                var result = default(SYSExcel);

                ServiceFactory.SVCategory((ISVCategory sv) =>
                {
                    result = sv.CATReason_ExcelInit(functionid, functionkey, isreload);
                });
                if (result != null && !string.IsNullOrEmpty(result.Data))
                {
                    result.Worksheets = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Worksheet>>(result.Data);
                }
                else
                {
                    result = new SYSExcel();
                    result.Worksheets = new List<Worksheet>();
                }
                result.Data = "";
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public Row CATReason_ExcelChange(dynamic dynParam)
        {
            try
            {
                var id = (long)dynParam.id;
                var row = (int)dynParam.row;
                List<Cell> cells = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Cell>>(dynParam.cells.ToString());

                var result = new Row();
                if (id > 0 && cells.Count > 0 && row > 0 )
                {
                    ServiceFactory.SVCategory((ISVCategory sv) =>
                    {
                        result = sv.CATReason_ExcelChange(id, row, cells);
                    });
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public SYSExcel CATReason_ExcelImport(dynamic dynParam)
        {
            try
            {
                var id = (long)dynParam.id;
                List<Worksheet> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Worksheet>>(dynParam.worksheets.ToString());

                var result = default(SYSExcel);
                if (id > 0 && lst.Count > 0 && lst[0].Rows.Count > 1)
                {
                    ServiceFactory.SVCategory((ISVCategory sv) =>
                    {
                        result = sv.CATReason_ExcelImport(id, lst[0].Rows);
                    });
                }
                if (result != null && !string.IsNullOrEmpty(result.Data))
                {
                    result.Worksheets = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Worksheet>>(result.Data);
                }
                else
                {
                    result = new SYSExcel();
                    result.Worksheets = new List<Worksheet>();
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public bool CATReason_ExcelApprove(dynamic dynParam)
        {
            try
            {
                var id = (long)dynParam.id;

                var result = false;
                if (id > 0)
                {
                    ServiceFactory.SVCategory((ISVCategory sv) =>
                    {
                        result = sv.CATReason_ExcelApprove(id);
                    });

                    //if (result != null && !string.IsNullOrEmpty(result.Data))
                    //{
                    //    result.Worksheets = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Worksheet>>(result.Data);
                    //}
                    //else
                    //{
                    //    result = new SYSExcel();
                    //    result.Worksheets = new List<Worksheet>();
                    //}
                    //result.Data = "";
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region CATGroupOfVehicle
        public DTOResult CATGroupOfVehicle_Read(dynamic dynParam)
        {
            try
            {
                string request = dynParam.request.ToString();
                var result = default(DTOResult);
                ServiceFactory.SVCategory((IServices.ISVCategory sv) =>
                {
                    result = sv.CATGroupOfVehicle_List(request);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public int CATGroupOfVehicle_Update(dynamic dynParam)
        {
            try
            {
                CATGroupOfVehicle item = Newtonsoft.Json.JsonConvert.DeserializeObject<CATGroupOfVehicle>(dynParam.item.ToString());
                int result = -1;
                if (item != null)
                {
                    ServiceFactory.SVCategory((ISVCategory sv) =>
                    {
                        result = sv.CATGroupOfVehicle_Save(item);
                    });
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public CATGroupOfVehicle CATGroupOfVehicle_Get(dynamic dynParam)
        {
            try
            {
                int id = (int)dynParam.ID;
                var result = default(CATGroupOfVehicle);
                ServiceFactory.SVCategory((ISVCategory sv) =>
                {
                    result = sv.CATGroupOfVehicle_Get(id);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void CATGroupOfVehicle_Destroy(dynamic dynParam)
        {
            try
            {
                CATGroupOfVehicle item = Newtonsoft.Json.JsonConvert.DeserializeObject<CATGroupOfVehicle>(dynParam.item.ToString());
                if (item != null)
                {
                    ServiceFactory.SVCategory((ISVCategory sv) =>
                    {
                        sv.CATGroupOfVehicle_Delete(item);
                    });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<CATGroupOfVehicle> CATGroupOfVehicle_DropdownList_Read(dynamic dynParam)
        {
            try
            {
                int id = (int)dynParam.id;
                DTOResult result = new DTOResult();
                ServiceFactory.SVCategory((ISVCategory sv) =>
                {
                    result = sv.CATGroupOfVehicle_List("");
                });
                var lst = new List<CATGroupOfVehicle>();
                CATGroupOfVehicle_DropdownList_Create(lst, result.Data.Cast<CATGroupOfVehicle>(), null, 0, id);
                return lst;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void CATGroupOfVehicle_DropdownList_Create(List<CATGroupOfVehicle> lstTarget, IEnumerable<CATGroupOfVehicle> lstSource, int? parentid, int level, int id)
        {
            foreach (var item in lstSource.Where(c => c.ParentID == parentid && c.ID != id))
            {
                item.GroupName = new string('.', 3 * level) + item.GroupName;
                lstTarget.Add(item);
                CATGroupOfVehicle_DropdownList_Create(lstTarget, lstSource, item.ID, level + 1, id);
            }
        }

        [HttpPost]
        public SYSExcel CATGroupOfVehicle_ExcelInit(dynamic dynParam)
        {
            try
            {
                var functionid = (int)dynParam.functionid;
                var functionkey = dynParam.functionkey.ToString();
                var isreload = (bool)dynParam.isreload;

                var result = default(SYSExcel);

                ServiceFactory.SVCategory((ISVCategory sv) =>
                {
                    result = sv.CATGroupOfVehicle_ExcelInit(functionid, functionkey, isreload);
                });
                if (result != null && !string.IsNullOrEmpty(result.Data))
                {
                    result.Worksheets = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Worksheet>>(result.Data);
                }
                else
                {
                    result = new SYSExcel();
                    result.Worksheets = new List<Worksheet>();
                }
                result.Data = "";
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public Row CATGroupOfVehicle_ExcelChange(dynamic dynParam)
        {
            try
            {
                var id = (long)dynParam.id;
                var row = (int)dynParam.row;
                List<Cell> cells = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Cell>>(dynParam.cells.ToString());
                List<string> lstMessageError = Newtonsoft.Json.JsonConvert.DeserializeObject<List<string>>(dynParam.lstMessageError.ToString());

                var result = new Row();
                if (id > 0 && cells.Count > 0 && row > 0)
                {
                    ServiceFactory.SVCategory((ISVCategory sv) =>
                    {
                        result = sv.CATGroupOfVehicle_ExcelChange(id, row, cells, lstMessageError);
                    });
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public SYSExcel CATGroupOfVehicle_ExcelImport(dynamic dynParam)
        {
            try
            {
                var id = (long)dynParam.id;
                List<Worksheet> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Worksheet>>(dynParam.worksheets.ToString());
                List<string> lstMessageError = Newtonsoft.Json.JsonConvert.DeserializeObject<List<string>>(dynParam.lstMessageError.ToString());

                var result = default(SYSExcel);
                if (id > 0 && lst.Count > 0 && lst[0].Rows.Count > 1)
                {
                    ServiceFactory.SVCategory((ISVCategory sv) =>
                    {
                        result = sv.CATGroupOfVehicle_ExcelImport(id, lst[0].Rows, lstMessageError);
                    });
                }
                if (result != null && !string.IsNullOrEmpty(result.Data))
                {
                    result.Worksheets = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Worksheet>>(result.Data);
                }
                else
                {
                    result = new SYSExcel();
                    result.Worksheets = new List<Worksheet>();
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public bool CATGroupOfVehicle_ExcelApprove(dynamic dynParam)
        {
            try
            {
                var id = (long)dynParam.id;

                var result = false;
                if (id > 0)
                {
                    ServiceFactory.SVCategory((ISVCategory sv) =>
                    {
                        result = sv.CATGroupOfVehicle_ExcelApprove(id);
                    });

                    //if (result != null && !string.IsNullOrEmpty(result.Data))
                    //{
                    //    result.Worksheets = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Worksheet>>(result.Data);
                    //}
                    //else
                    //{
                    //    result = new SYSExcel();
                    //    result.Worksheets = new List<Worksheet>();
                    //}
                    //result.Data = "";
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region CATGroupOfRomooc

        public DTOResult CATGroupOfRomoocPacking_List(dynamic dynParam)
        {
            try
            {
                string request = dynParam.request.ToString();
                int GroupOfRomoocID = (int)dynParam.GroupOfRomoocID;
                var result = default(DTOResult);
                ServiceFactory.SVCategory((IServices.ISVCategory sv) =>
                {
                    result = sv.CATGroupOfRomoocPacking_List(request, GroupOfRomoocID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DTOResult CATGroupOfRomoocPacking_NotInList(dynamic dynParam)
        {
            try
            {
                string request = dynParam.request.ToString();
                int GroupOfRomoocID = (int)dynParam.GroupOfRomoocID;
                var result = default(DTOResult);
                ServiceFactory.SVCategory((IServices.ISVCategory sv) =>
                {
                    result = sv.CATGroupOfRomoocPacking_NotInList(request, GroupOfRomoocID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void CATGroupOfRomoocPacking_Save(dynamic dynParam)
        {
            try
            {
                List<CATGroupOfRomoocPacking> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<CATGroupOfRomoocPacking>>(dynParam.item.ToString());
                int GroupOfRomoocID = (int)dynParam.GroupOfRomoocID;
                ServiceFactory.SVCategory((ISVCategory sv) =>
                {
                    sv.CATGroupOfRomoocPacking_Save(lst, GroupOfRomoocID);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void CATGroupOfRomoocPacking_Delete(dynamic dynParam)
        {
            try
            {
                CATGroupOfRomoocPacking item = Newtonsoft.Json.JsonConvert.DeserializeObject<CATGroupOfRomoocPacking>(dynParam.item.ToString());
                if (item != null)
                {
                    ServiceFactory.SVCategory((ISVCategory sv) =>
                    {
                        sv.CATGroupOfRomoocPacking_Delete(item);
                    });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public DTOResult CATGroupOfRomooc_Read(dynamic dynParam)
        {
            try
            {
                string request = dynParam.request.ToString();
                var result = default(DTOResult);
                ServiceFactory.SVCategory((IServices.ISVCategory sv) =>
                {
                    result = sv.CATGroupOfRomooc_List(request);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public int CATGroupOfRomooc_Update(dynamic dynParam)
        {
            try
            {
                CATGroupOfRomooc item = Newtonsoft.Json.JsonConvert.DeserializeObject<CATGroupOfRomooc>(dynParam.item.ToString());
                int result = -1;
                if (item != null)
                {
                    ServiceFactory.SVCategory((ISVCategory sv) =>
                    {
                        result = sv.CATGroupOfRomooc_Save(item);
                    });
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public CATGroupOfRomooc CATGroupOfRomooc_Get(dynamic dynParam)
        {
            try
            {
                int id = (int)dynParam.ID;
                var result = default(CATGroupOfRomooc);
                ServiceFactory.SVCategory((ISVCategory sv) =>
                {
                    result = sv.CATGroupOfRomooc_Get(id);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void CATGroupOfRomooc_Destroy(dynamic dynParam)
        {
            try
            {
                CATGroupOfRomooc item = Newtonsoft.Json.JsonConvert.DeserializeObject<CATGroupOfRomooc>(dynParam.item.ToString());
                if (item != null)
                {
                    ServiceFactory.SVCategory((ISVCategory sv) =>
                    {
                        sv.CATGroupOfRomooc_Delete(item);
                    });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<CATGroupOfRomooc> CATGroupOfRomooc_DropdownList_Read(dynamic dynParam)
        {
            try
            {
                int id = (int)dynParam.id;
                DTOResult result = new DTOResult();
                ServiceFactory.SVCategory((ISVCategory sv) =>
                {
                    result = sv.CATGroupOfRomooc_List("");
                });
                var lst = new List<CATGroupOfRomooc>();
                CATGroupOfRomooc_DropdownList_Create(lst, result.Data.Cast<CATGroupOfRomooc>(), null, 0, id);
                return lst;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void CATGroupOfRomooc_DropdownList_Create(List<CATGroupOfRomooc> lstTarget, IEnumerable<CATGroupOfRomooc> lstSource, int? parentid, int level, int id)
        {
            foreach (var item in lstSource.Where(c => c.ParentID == parentid && c.ID != id))
            {
                item.GroupName = new string('.', 3 * level) + item.GroupName;
                lstTarget.Add(item);
                CATGroupOfRomooc_DropdownList_Create(lstTarget, lstSource, item.ID, level + 1, id);
            }
        }

        [HttpPost]
        public SYSExcel CATGroupOfRomooc_ExcelInit(dynamic dynParam)
        {
            try
            {
                var functionid = (int)dynParam.functionid;
                var functionkey = dynParam.functionkey.ToString();
                var isreload = (bool)dynParam.isreload;

                var result = default(SYSExcel);

                ServiceFactory.SVCategory((ISVCategory sv) =>
                {
                    result = sv.CATGroupOfRomooc_ExcelInit(functionid, functionkey, isreload);
                });
                if (result != null && !string.IsNullOrEmpty(result.Data))
                {
                    result.Worksheets = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Worksheet>>(result.Data);
                }
                else
                {
                    result = new SYSExcel();
                    result.Worksheets = new List<Worksheet>();
                }
                result.Data = "";
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public Row CATGroupOfRomooc_ExcelChange(dynamic dynParam)
        {
            try
            {
                var id = (long)dynParam.id;
                var row = (int)dynParam.row;
                List<Cell> cells = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Cell>>(dynParam.cells.ToString());
                List<string> lstMessageError = Newtonsoft.Json.JsonConvert.DeserializeObject<List<string>>(dynParam.lstMessageError.ToString());

                var result = new Row();
                if (id > 0 && cells.Count > 0 && row > 0)
                {
                    ServiceFactory.SVCategory((ISVCategory sv) =>
                    {
                        result = sv.CATGroupOfRomooc_ExcelChange(id, row, cells, lstMessageError);
                    });
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public SYSExcel CATGroupOfRomooc_ExcelImport(dynamic dynParam)
        {
            try
            {
                var id = (long)dynParam.id;
                List<Worksheet> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Worksheet>>(dynParam.worksheets.ToString());
                List<string> lstMessageError = Newtonsoft.Json.JsonConvert.DeserializeObject<List<string>>(dynParam.lstMessageError.ToString());

                var result = default(SYSExcel);
                if (id > 0 && lst.Count > 0 && lst[0].Rows.Count > 1)
                {
                    ServiceFactory.SVCategory((ISVCategory sv) =>
                    {
                        result = sv.CATGroupOfRomooc_ExcelImport(id, lst[0].Rows, lstMessageError);
                    });
                }
                if (result != null && !string.IsNullOrEmpty(result.Data))
                {
                    result.Worksheets = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Worksheet>>(result.Data);
                }
                else
                {
                    result = new SYSExcel();
                    result.Worksheets = new List<Worksheet>();
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public bool CATGroupOfRomooc_ExcelApprove(dynamic dynParam)
        {
            try
            {
                var id = (long)dynParam.id;

                var result = false;
                if (id > 0)
                {
                    ServiceFactory.SVCategory((ISVCategory sv) =>
                    {
                        result = sv.CATGroupOfRomooc_ExcelApprove(id);
                    });

                    //if (result != null && !string.IsNullOrEmpty(result.Data))
                    //{
                    //    result.Worksheets = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Worksheet>>(result.Data);
                    //}
                    //else
                    //{
                    //    result = new SYSExcel();
                    //    result.Worksheets = new List<Worksheet>();
                    //}
                    //result.Data = "";
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region CATGroupOfEquipment
        public DTOResult CATGroupOfEquipment_Read(dynamic dynParam)
        {
            try
            {
                string request = dynParam.request.ToString();
                var result = default(DTOResult);
                ServiceFactory.SVCategory((IServices.ISVCategory sv) =>
                {
                    result = sv.CATGroupOfEquipment_List(request);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public int CATGroupOfEquipment_Update(dynamic dynParam)
        {
            try
            {
                CATGroupOfEquipment item = Newtonsoft.Json.JsonConvert.DeserializeObject<CATGroupOfEquipment>(dynParam.item.ToString());
                int result = -1;
                if (item != null)
                {
                    ServiceFactory.SVCategory((ISVCategory sv) =>
                    {
                        result = sv.CATGroupOfEquipment_Save(item);
                    });
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public CATGroupOfEquipment CATGroupOfEquipment_Get(dynamic dynParam)
        {
            try
            {
                int id = (int)dynParam.ID;
                var result = default(CATGroupOfEquipment);
                ServiceFactory.SVCategory((ISVCategory sv) =>
                {
                    result = sv.CATGroupOfEquipment_Get(id);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void CATGroupOfEquipment_Destroy(dynamic dynParam)
        {
            try
            {
                CATGroupOfEquipment item = Newtonsoft.Json.JsonConvert.DeserializeObject<CATGroupOfEquipment>(dynParam.item.ToString());
                if (item != null)
                {
                    ServiceFactory.SVCategory((ISVCategory sv) =>
                    {
                        sv.CATGroupOfEquipment_Delete(item);
                    });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<CATGroupOfEquipment> CATGroupOfEquipment_DropdownList_Read(dynamic dynParam)
        {
            try
            {
                int id = (int)dynParam.id;
                DTOResult result = new DTOResult();
                ServiceFactory.SVCategory((ISVCategory sv) =>
                {
                    result = sv.CATGroupOfEquipment_List("");
                });
                var lst = new List<CATGroupOfEquipment>();
                CATGroupOfEquipment_DropdownList_Create(lst, result.Data.Cast<CATGroupOfEquipment>(), null, 0, id);
                return lst;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void CATGroupOfEquipment_DropdownList_Create(List<CATGroupOfEquipment> lstTarget, IEnumerable<CATGroupOfEquipment> lstSource, int? parentid, int level, int id)
        {
            foreach (var item in lstSource.Where(c => c.ParentID == parentid && c.ID != id))
            {
                item.GroupName = new string('.', 3 * level) + item.GroupName;
                lstTarget.Add(item);
                CATGroupOfEquipment_DropdownList_Create(lstTarget, lstSource, item.ID, level + 1, id);
            }
        }

        [HttpPost]
        public SYSExcel CATGroupOfEquipment_ExcelInit(dynamic dynParam)
        {
            try
            {
                var functionid = (int)dynParam.functionid;
                var functionkey = dynParam.functionkey.ToString();
                var isreload = (bool)dynParam.isreload;

                var result = default(SYSExcel);

                ServiceFactory.SVCategory((ISVCategory sv) =>
                {
                    result = sv.CATGroupOfEquipment_ExcelInit(functionid, functionkey, isreload);
                });
                if (result != null && !string.IsNullOrEmpty(result.Data))
                {
                    result.Worksheets = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Worksheet>>(result.Data);
                }
                else
                {
                    result = new SYSExcel();
                    result.Worksheets = new List<Worksheet>();
                }
                result.Data = "";
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public Row CATGroupOfEquipment_ExcelChange(dynamic dynParam)
        {
            try
            {
                var id = (long)dynParam.id;
                var row = (int)dynParam.row;
                List<Cell> cells = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Cell>>(dynParam.cells.ToString());
                List<string> lstMessageError = Newtonsoft.Json.JsonConvert.DeserializeObject<List<string>>(dynParam.lstMessageError.ToString());

                var result = new Row();
                if (id > 0 && cells.Count > 0 && row > 0)
                {
                    ServiceFactory.SVCategory((ISVCategory sv) =>
                    {
                        result = sv.CATGroupOfEquipment_ExcelChange(id, row, cells, lstMessageError);
                    });
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public SYSExcel CATGroupOfEquipment_ExcelImport(dynamic dynParam)
        {
            try
            {
                var id = (long)dynParam.id;
                List<Worksheet> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Worksheet>>(dynParam.worksheets.ToString());
                List<string> lstMessageError = Newtonsoft.Json.JsonConvert.DeserializeObject<List<string>>(dynParam.lstMessageError.ToString());

                var result = default(SYSExcel);
                if (id > 0 && lst.Count > 0 && lst[0].Rows.Count > 1)
                {
                    ServiceFactory.SVCategory((ISVCategory sv) =>
                    {
                        result = sv.CATGroupOfEquipment_ExcelImport(id, lst[0].Rows, lstMessageError);
                    });
                }
                if (result != null && !string.IsNullOrEmpty(result.Data))
                {
                    result.Worksheets = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Worksheet>>(result.Data);
                }
                else
                {
                    result = new SYSExcel();
                    result.Worksheets = new List<Worksheet>();
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public bool CATGroupOfEquipment_ExcelApprove(dynamic dynParam)
        {
            try
            {
                var id = (long)dynParam.id;

                var result = false;
                if (id > 0)
                {
                    ServiceFactory.SVCategory((ISVCategory sv) =>
                    {
                        result = sv.CATGroupOfEquipment_ExcelApprove(id);
                    });

                    //if (result != null && !string.IsNullOrEmpty(result.Data))
                    //{
                    //    result.Worksheets = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Worksheet>>(result.Data);
                    //}
                    //else
                    //{
                    //    result = new SYSExcel();
                    //    result.Worksheets = new List<Worksheet>();
                    //}
                    //result.Data = "";
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region CATGroupOfMaterial
        public DTOResult CATGroupOfMaterial_Read(dynamic dynParam)
        {
            try
            {
                string request = dynParam.request.ToString();
                var result = default(DTOResult);
                ServiceFactory.SVCategory((IServices.ISVCategory sv) =>
                {
                    result = sv.CATGroupOfMaterial_List(request);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public int CATGroupOfMaterial_Update(dynamic dynParam)
        {
            try
            {
                CATGroupOfMaterial item = Newtonsoft.Json.JsonConvert.DeserializeObject<CATGroupOfMaterial>(dynParam.item.ToString());
                int result = -1;
                if (item != null)
                {
                    ServiceFactory.SVCategory((ISVCategory sv) =>
                    {
                        result = sv.CATGroupOfMaterial_Save(item);
                    });
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public CATGroupOfMaterial CATGroupOfMaterial_Get(dynamic dynParam)
        {
            try
            {
                int id = (int)dynParam.ID;
                var result = default(CATGroupOfMaterial);
                ServiceFactory.SVCategory((ISVCategory sv) =>
                {
                    result = sv.CATGroupOfMaterial_Get(id);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void CATGroupOfMaterial_Destroy(dynamic dynParam)
        {
            try
            {
                CATGroupOfMaterial item = Newtonsoft.Json.JsonConvert.DeserializeObject<CATGroupOfMaterial>(dynParam.item.ToString());
                if (item != null)
                {
                    ServiceFactory.SVCategory((ISVCategory sv) =>
                    {
                        sv.CATGroupOfMaterial_Delete(item);
                    });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<CATGroupOfMaterial> CATGroupOfMaterial_DropdownList_Read(dynamic dynParam)
        {
            try
            {
                int id = (int)dynParam.id;
                DTOResult result = new DTOResult();
                ServiceFactory.SVCategory((ISVCategory sv) =>
                {
                    result = sv.CATGroupOfMaterial_List("");
                });
                var lst = new List<CATGroupOfMaterial>();
                CATGroupOfMaterial_DropdownList_Create(lst, result.Data.Cast<CATGroupOfMaterial>(), null, 0, id);
                return lst;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void CATGroupOfMaterial_DropdownList_Create(List<CATGroupOfMaterial> lstTarget, IEnumerable<CATGroupOfMaterial> lstSource, int? parentid, int level, int id)
        {
            foreach (var item in lstSource.Where(c => c.ParentID == parentid && c.ID != id))
            {
                item.GroupName = new string('.', 3 * level) + item.GroupName;
                lstTarget.Add(item);
                CATGroupOfMaterial_DropdownList_Create(lstTarget, lstSource, item.ID, level + 1, id);
            }
        }

        [HttpPost]
        public SYSExcel CATGroupOfMaterial_ExcelInit(dynamic dynParam)
        {
            try
            {
                var functionid = (int)dynParam.functionid;
                var functionkey = dynParam.functionkey.ToString();
                var isreload = (bool)dynParam.isreload;

                var result = default(SYSExcel);

                ServiceFactory.SVCategory((ISVCategory sv) =>
                {
                    result = sv.CATGroupOfMaterial_ExcelInit(functionid, functionkey, isreload);
                });
                if (result != null && !string.IsNullOrEmpty(result.Data))
                {
                    result.Worksheets = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Worksheet>>(result.Data);
                }
                else
                {
                    result = new SYSExcel();
                    result.Worksheets = new List<Worksheet>();
                }
                result.Data = "";
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public Row CATGroupOfMaterial_ExcelChange(dynamic dynParam)
        {
            try
            {
                var id = (long)dynParam.id;
                var row = (int)dynParam.row;
                List<Cell> cells = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Cell>>(dynParam.cells.ToString());
                List<string> lstMessageError = Newtonsoft.Json.JsonConvert.DeserializeObject<List<string>>(dynParam.lstMessageError.ToString());
                var result = new Row();
                if (id > 0 && cells.Count > 0 && row > 0)
                {
                    ServiceFactory.SVCategory((ISVCategory sv) =>
                    {
                        result = sv.CATGroupOfMaterial_ExcelChange(id, row, cells, lstMessageError);
                    });
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public SYSExcel CATGroupOfMaterial_ExcelImport(dynamic dynParam)
        {
            try
            {
                var id = (long)dynParam.id;
                List<Worksheet> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Worksheet>>(dynParam.worksheets.ToString());
                List<string> lstMessageError = Newtonsoft.Json.JsonConvert.DeserializeObject<List<string>>(dynParam.lstMessageError.ToString());
                var result = default(SYSExcel);
                if (id > 0 && lst.Count > 0 && lst[0].Rows.Count > 1)
                {
                    ServiceFactory.SVCategory((ISVCategory sv) =>
                    {
                        result = sv.CATGroupOfMaterial_ExcelImport(id, lst[0].Rows, lstMessageError);
                    });
                }
                //if (result != null && !string.IsNullOrEmpty(result.Data))
                //{
                //    result.Worksheets = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Worksheet>>(result.Data);
                //}
                //else
                //{
                //    result = new SYSExcel();
                //    result.Worksheets = new List<Worksheet>();
                //}
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public bool CATGroupOfMaterial_ExcelApprove(dynamic dynParam)
        {
            try
            {
                var id = (long)dynParam.id;

                var result = false;
                if (id > 0)
                {
                    ServiceFactory.SVCategory((ISVCategory sv) =>
                    {
                        result = sv.CATGroupOfMaterial_ExcelApprove(id);
                    });

                    //if (result != null && !string.IsNullOrEmpty(result.Data))
                    //{
                    //    result.Worksheets = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Worksheet>>(result.Data);
                    //}
                    //else
                    //{
                    //    result = new SYSExcel();
                    //    result.Worksheets = new List<Worksheet>();
                    //}
                    //result.Data = "";
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region CATMaterial
        public DTOResult CATMaterial_Read(dynamic dynParam)
        {
            try
            {
                string request = dynParam.request.ToString();
                var result = default(DTOResult);
                ServiceFactory.SVCategory((IServices.ISVCategory sv) =>
                {
                    result = sv.FLMMaterial_List(request);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public int CATMaterial_Update(dynamic dynParam)
        {
            try
            {
                FLMMaterial item = Newtonsoft.Json.JsonConvert.DeserializeObject<FLMMaterial>(dynParam.item.ToString());
                int result = -1;
                if (item != null)
                {
                    ServiceFactory.SVCategory((ISVCategory sv) =>
                    {
                        result = sv.FLMMaterial_Save(item);
                    });
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public FLMMaterial CATMaterial_Get(dynamic dynParam)
        {
            try
            {
                int id = (int)dynParam.ID;
                var result = default(FLMMaterial);
                ServiceFactory.SVCategory((ISVCategory sv) =>
                {
                    result = sv.FLMMaterial_Get(id);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void CATMaterial_Destroy(dynamic dynParam)
        {
            try
            {
                FLMMaterial item = Newtonsoft.Json.JsonConvert.DeserializeObject<FLMMaterial>(dynParam.item.ToString());
                if (item != null)
                {
                    ServiceFactory.SVCategory((ISVCategory sv) =>
                    {
                        sv.FLMMaterial_Delete(item);
                    });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public SYSExcel CATMaterial_ExcelInit(dynamic dynParam)
        {
            try
            {
                var functionid = (int)dynParam.functionid;
                var functionkey = dynParam.functionkey.ToString();
                var isreload = (bool)dynParam.isreload;

                var result = default(SYSExcel);

                ServiceFactory.SVCategory((ISVCategory sv) =>
                {
                    result = sv.CATMaterial_ExcelInit(functionid, functionkey, isreload);
                });
                if (result != null && !string.IsNullOrEmpty(result.Data))
                {
                    result.Worksheets = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Worksheet>>(result.Data);
                }
                else
                {
                    result = new SYSExcel();
                    result.Worksheets = new List<Worksheet>();
                }
                result.Data = "";
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public Row CATMaterial_ExcelChange(dynamic dynParam)
        {
            try
            {
                var id = (long)dynParam.id;
                var row = (int)dynParam.row;
                List<Cell> cells = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Cell>>(dynParam.cells.ToString());
                List<string> lstMessageError = Newtonsoft.Json.JsonConvert.DeserializeObject<List<string>>(dynParam.lstMessageError.ToString());

                var result = new Row();
                if (id > 0 && cells.Count > 0 && row > 0)
                {
                    ServiceFactory.SVCategory((ISVCategory sv) =>
                    {
                        result = sv.CATMaterial_ExcelChange(id, row, cells, lstMessageError);
                    });
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public SYSExcel CATMaterial_ExcelImport(dynamic dynParam)
        {
            try
            {
                var id = (long)dynParam.id;
                List<Worksheet> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Worksheet>>(dynParam.worksheets.ToString());
                List<string> lstMessageError = Newtonsoft.Json.JsonConvert.DeserializeObject<List<string>>(dynParam.lstMessageError.ToString());

                var result = default(SYSExcel);
                if (id > 0 && lst.Count > 0 && lst[0].Rows.Count > 1)
                {
                    ServiceFactory.SVCategory((ISVCategory sv) =>
                    {
                        result = sv.CATMaterial_ExcelImport(id, lst[0].Rows, lstMessageError);
                    });
                }
                if (result != null && !string.IsNullOrEmpty(result.Data))
                {
                    result.Worksheets = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Worksheet>>(result.Data);
                }
                else
                {
                    result = new SYSExcel();
                    result.Worksheets = new List<Worksheet>();
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public bool CATMaterial_ExcelApprove(dynamic dynParam)
        {
            try
            {
                var id = (long)dynParam.id;

                var result = false;
                if (id > 0)
                {
                    ServiceFactory.SVCategory((ISVCategory sv) =>
                    {
                        result = sv.CATMaterial_ExcelApprove(id);
                    });

                    //if (result != null && !string.IsNullOrEmpty(result.Data))
                    //{
                    //    result.Worksheets = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Worksheet>>(result.Data);
                    //}
                    //else
                    //{
                    //    result = new SYSExcel();
                    //    result.Worksheets = new List<Worksheet>();
                    //}
                    //result.Data = "";
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region CATStock
        public DTOResult CATStock_Read(dynamic dynParam)
        {
            try
            {
                string request = dynParam.request.ToString();
                var result = default(DTOResult);
                ServiceFactory.SVCategory((IServices.ISVCategory sv) =>
                {
                    result = sv.FLMStock_List(request);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public int CATStock_Update(dynamic dynParam)
        {
            try
            {
                FLMStock item = Newtonsoft.Json.JsonConvert.DeserializeObject<FLMStock>(dynParam.item.ToString());
                int result = -1;
                if (item != null)
                {
                    ServiceFactory.SVCategory((ISVCategory sv) =>
                    {
                        result = sv.FLMStock_Save(item);
                    });
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public FLMStock CATStock_Get(dynamic dynParam)
        {
            try
            {
                int id = (int)dynParam.ID;
                var result = default(FLMStock);
                ServiceFactory.SVCategory((ISVCategory sv) =>
                {
                    result = sv.FLMStock_Get(id);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void CATStock_Destroy(dynamic dynParam)
        {
            try
            {
                FLMStock item = Newtonsoft.Json.JsonConvert.DeserializeObject<FLMStock>(dynParam.item.ToString());
                if (item != null)
                {
                    ServiceFactory.SVCategory((ISVCategory sv) =>
                    {
                        sv.FLMStock_Delete(item);
                    });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public SYSExcel FLMStock_ExcelInit(dynamic dynParam)
        {
            try
            {
                var functionid = (int)dynParam.functionid;
                var functionkey = dynParam.functionkey.ToString();
                var isreload = (bool)dynParam.isreload;

                var result = default(SYSExcel);

                ServiceFactory.SVCategory((ISVCategory sv) =>
                {
                    result = sv.FLMStock_ExcelInit(functionid, functionkey, isreload);
                });
                if (result != null && !string.IsNullOrEmpty(result.Data))
                {
                    result.Worksheets = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Worksheet>>(result.Data);
                }
                else
                {
                    result = new SYSExcel();
                    result.Worksheets = new List<Worksheet>();
                }
                result.Data = "";
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public Row FLMStock_ExcelChange(dynamic dynParam)
        {
            try
            {
                var id = (long)dynParam.id;
                var row = (int)dynParam.row;
                List<Cell> cells = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Cell>>(dynParam.cells.ToString());
                List<string> lstMessageError = Newtonsoft.Json.JsonConvert.DeserializeObject<List<string>>(dynParam.lstMessageError.ToString());

                var result = new Row();
                if (id > 0 && cells.Count > 0 && row > 0)
                {
                    ServiceFactory.SVCategory((ISVCategory sv) =>
                    {
                        result = sv.FLMStock_ExcelChange(id, row, cells, lstMessageError);
                    });
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public SYSExcel FLMStock_ExcelImport(dynamic dynParam)
        {
            try
            {
                var id = (long)dynParam.id;
                List<Worksheet> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Worksheet>>(dynParam.worksheets.ToString());
                List<string> lstMessageError = Newtonsoft.Json.JsonConvert.DeserializeObject<List<string>>(dynParam.lstMessageError.ToString());

                var result = default(SYSExcel);
                if (id > 0 && lst.Count > 0 && lst[0].Rows.Count > 1)
                {
                    ServiceFactory.SVCategory((ISVCategory sv) =>
                    {
                        result = sv.FLMStock_ExcelImport(id, lst[0].Rows, lstMessageError);
                    });
                }
                if (result != null && !string.IsNullOrEmpty(result.Data))
                {
                    result.Worksheets = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Worksheet>>(result.Data);
                }
                else
                {
                    result = new SYSExcel();
                    result.Worksheets = new List<Worksheet>();
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public bool FLMStock_ExcelApprove(dynamic dynParam)
        {
            try
            {
                var id = (long)dynParam.id;

                var result = false;
                if (id > 0)
                {
                    ServiceFactory.SVCategory((ISVCategory sv) =>
                    {
                        result = sv.FLMStock_ExcelApprove(id);
                    });

                    //if (result != null && !string.IsNullOrEmpty(result.Data))
                    //{
                    //    result.Worksheets = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Worksheet>>(result.Data);
                    //}
                    //else
                    //{
                    //    result = new SYSExcel();
                    //    result.Worksheets = new List<Worksheet>();
                    //}
                    //result.Data = "";
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region CATRomooc
        public DTOResult CATRomooc_Read(dynamic dynParam)
        {
            try
            {
                string request = dynParam.request.ToString();
                var result = default(DTOResult);
                ServiceFactory.SVCategory((IServices.ISVCategory sv) =>
                {
                    result = sv.CATRomooc_List(request);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public int CATRomooc_Update(dynamic dynParam)
        {
            try
            {
                CATRomooc item = Newtonsoft.Json.JsonConvert.DeserializeObject<CATRomooc>(dynParam.item.ToString());
                int result = -1;
                if (item != null)
                {
                    ServiceFactory.SVCategory((ISVCategory sv) =>
                    {
                        result = sv.CATRomooc_Save(item);
                    });
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public CATRomooc CATRomooc_Get(dynamic dynParam)
        {
            try
            {
                int id = (int)dynParam.ID;
                var result = default(CATRomooc);
                ServiceFactory.SVCategory((ISVCategory sv) =>
                {
                    result = sv.CATRomooc_Get(id);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void CATRomooc_Destroy(dynamic dynParam)
        {
            try
            {
                CATRomooc item = Newtonsoft.Json.JsonConvert.DeserializeObject<CATRomooc>(dynParam.item.ToString());
                if (item != null)
                {
                    ServiceFactory.SVCategory((ISVCategory sv) =>
                    {
                        sv.CATRomooc_Delete(item);
                    });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region CATVehicle
        public DTOResult CATVehicle_Read(dynamic dynParam)
        {
            try
            {
                string request = dynParam.request.ToString();
                var result = default(DTOResult);
                ServiceFactory.SVCategory((IServices.ISVCategory sv) =>
                {
                    result = sv.CATVehicle_List(request);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public int CATVehicle_Update(dynamic dynParam)
        {
            try
            {
                DTOCATVehicle item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOCATVehicle>(dynParam.item.ToString());
                int result = -1;
                if (item != null)
                {
                    ServiceFactory.SVCategory((ISVCategory sv) =>
                    {
                        result = sv.CATVehicle_Save(item);
                    });
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOCATVehicle CATVehicle_Get(dynamic dynParam)
        {
            try
            {
                int id = (int)dynParam.ID;
                var result = default(DTOCATVehicle);
                ServiceFactory.SVCategory((ISVCategory sv) =>
                {
                    result = sv.CATVehicle_Get(id);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void CATVehicle_Destroy(dynamic dynParam)
        {
            try
            {
                DTOCATVehicle item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOCATVehicle>(dynParam.item.ToString());
                if (item != null)
                {
                    ServiceFactory.SVCategory((ISVCategory sv) =>
                    {
                        sv.CATVehicle_Delete(item);
                    });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        #endregion

        #region CATGroupOfService
        public DTOResult CATGroupOfService_Read(dynamic dynParam)
        {
            try
            {
                string request = dynParam.request.ToString();
                var result = default(DTOResult);
                ServiceFactory.SVCategory((IServices.ISVCategory sv) =>
                {
                    result = sv.CATGroupOfService_List(request);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public int CATGroupOfService_Update(dynamic dynParam)
        {
            try
            {
                CATGroupOfService item = Newtonsoft.Json.JsonConvert.DeserializeObject<CATGroupOfService>(dynParam.item.ToString());
                int result = -1;
                if (item != null)
                {
                    ServiceFactory.SVCategory((ISVCategory sv) =>
                    {
                        result = sv.CATGroupOfService_Save(item);
                    });
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public CATGroupOfService CATGroupOfService_Get(dynamic dynParam)
        {
            try
            {
                int id = (int)dynParam.ID;
                var result = default(CATGroupOfService);
                ServiceFactory.SVCategory((ISVCategory sv) =>
                {
                    result = sv.CATGroupOfService_Get(id);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void CATGroupOfService_Destroy(dynamic dynParam)
        {
            try
            {
                CATGroupOfService item = Newtonsoft.Json.JsonConvert.DeserializeObject<CATGroupOfService>(dynParam.item.ToString());
                if (item != null)
                {
                    ServiceFactory.SVCategory((ISVCategory sv) =>
                    {
                        sv.CATGroupOfService_Delete(item);
                    });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        #endregion

        #region CATService
        public DTOResult CATService_List(dynamic dynParam)
        {
            try
            {
                string request = dynParam.request.ToString();
                var result = default(DTOResult);
                ServiceFactory.SVCategory((IServices.ISVCategory sv) =>
                {
                    result = sv.CATService_List(request);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public int CATService_Save(dynamic dynParam)
        {
            try
            {
                CATService item = Newtonsoft.Json.JsonConvert.DeserializeObject<CATService>(dynParam.item.ToString());
                int result = -1;
                if (item != null)
                {
                    ServiceFactory.SVCategory((ISVCategory sv) =>
                    {
                        result = sv.CATService_Save(item);
                    });
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public CATService CATService_Get(dynamic dynParam)
        {
            try
            {
                int id = (int)dynParam.ID;
                var result = default(CATService);
                ServiceFactory.SVCategory((ISVCategory sv) =>
                {
                    result = sv.CATService_Get(id);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void CATService_Destroy(dynamic dynParam)
        {
            try
            {
                CATService item = Newtonsoft.Json.JsonConvert.DeserializeObject<CATService>(dynParam.item.ToString());
                if (item != null)
                {
                    ServiceFactory.SVCategory((ISVCategory sv) =>
                    {
                        sv.CATService_Delete(item);
                    });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DTOResult CATService_ServiceType()
        {
            try
            {
                var result = default(DTOResult);
                ServiceFactory.SVCategory((IServices.ISVCategory sv) =>
                {
                    result = sv.ALL_SysVar(SYSVarType.ServiceType);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public SYSExcel CATService_ExcelInit(dynamic dynParam)
        {
            try
            {
                var functionid = (int)dynParam.functionid;
                var functionkey = dynParam.functionkey.ToString();
                var isreload = (bool)dynParam.isreload;

                var result = default(SYSExcel);

                ServiceFactory.SVCategory((ISVCategory sv) =>
                {
                    result = sv.CATService_ExcelInit(functionid, functionkey, isreload);
                });
                if (result != null && !string.IsNullOrEmpty(result.Data))
                {
                    result.Worksheets = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Worksheet>>(result.Data);
                }
                else
                {
                    result = new SYSExcel();
                    result.Worksheets = new List<Worksheet>();
                }
                result.Data = "";
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public Row CATService_ExcelChange(dynamic dynParam)
        {
            try
            {
                var id = (long)dynParam.id;
                var row = (int)dynParam.row;
                List<Cell> cells = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Cell>>(dynParam.cells.ToString());
                List<string> lstMessageError = Newtonsoft.Json.JsonConvert.DeserializeObject<List<string>>(dynParam.lstMessageError.ToString());

                var result = default(Row);
                if (id > 0 && cells.Count > 0 && row > 0 && row > 0)
                {
                    ServiceFactory.SVCategory((ISVCategory sv) =>
                    {
                        result = sv.CATService_ExcelChange(id, row, cells,lstMessageError);
                    });
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public SYSExcel CATService_ExcelImport(dynamic dynParam)
        {
            try
            {
                var id = (long)dynParam.id;
                List<Worksheet> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Worksheet>>(dynParam.worksheets.ToString());
                List<string> lstMessageError = Newtonsoft.Json.JsonConvert.DeserializeObject<List<string>>(dynParam.lstMessageError.ToString());

                var result = default(SYSExcel);
                if (id > 0 && lst.Count > 0 && lst[0].Rows.Count > 1)
                {
                    ServiceFactory.SVCategory((ISVCategory sv) =>
                    {
                        result = sv.CATService_ExcelImport(id, lst[0].Rows,lstMessageError);
                    });
                }
                if (result != null && !string.IsNullOrEmpty(result.Data))
                {
                    result.Worksheets = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Worksheet>>(result.Data);
                }
                else
                {
                    result = new SYSExcel();
                    result.Worksheets = new List<Worksheet>();
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public bool CATService_ExcelApprove(dynamic dynParam)
        {
            try
            {
                var id = (long)dynParam.id;
                var result = false;

                if (id > 0)
                {
                    ServiceFactory.SVCategory((ISVCategory sv) =>
                    {
                        result = sv.CATService_ExcelApprove(id);
                    });
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region CATGroupOfPartner
        public DTOResult CATGroupOfPartner_Read(dynamic dynParam)
        {
            try
            {
                string request = dynParam.request.ToString();
                var result = default(DTOResult);
                ServiceFactory.SVCategory((IServices.ISVCategory sv) =>
                {
                    result = sv.CATGroupOfPartner_List(request);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public int CATGroupOfPartner_Update(dynamic dynParam)
        {
            try
            {
                CATGroupOfPartner item = Newtonsoft.Json.JsonConvert.DeserializeObject<CATGroupOfPartner>(dynParam.item.ToString());
                int result = -1;
                if (item != null)
                {
                    ServiceFactory.SVCategory((ISVCategory sv) =>
                    {
                        result = sv.CATGroupOfPartner_Save(item);
                    });
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public CATGroupOfPartner CATGroupOfPartner_Get(dynamic dynParam)
        {
            try
            {
                int id = (int)dynParam.ID;
                var result = default(CATGroupOfPartner);
                ServiceFactory.SVCategory((ISVCategory sv) =>
                {
                    result = sv.CATGroupOfPartner_Get(id);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void CATGroupOfPartner_Destroy(dynamic dynParam)
        {
            try
            {
                CATGroupOfPartner item = Newtonsoft.Json.JsonConvert.DeserializeObject<CATGroupOfPartner>(dynParam.item.ToString());
                if (item != null)
                {
                    ServiceFactory.SVCategory((ISVCategory sv) =>
                    {
                        sv.CATGroupOfPartner_Delete(item);
                    });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
       
        [HttpPost]
        public SYSExcel CATGroupOfPartner_ExcelInit(dynamic dynParam)
        {
            try
            {
                var functionid = (int)dynParam.functionid;
                var functionkey = dynParam.functionkey.ToString();
                var isreload = (bool)dynParam.isreload;

                var result = default(SYSExcel);

                ServiceFactory.SVCategory((ISVCategory sv) =>
                {
                    result = sv.CATGroupOfPartner_ExcelInit(functionid, functionkey, isreload);
                });
                if (result != null && !string.IsNullOrEmpty(result.Data))
                {
                    result.Worksheets = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Worksheet>>(result.Data);
                }
                else
                {
                    result = new SYSExcel();
                    result.Worksheets = new List<Worksheet>();
                }
                result.Data = "";
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public Row CATGroupOfPartner_ExcelChange(dynamic dynParam)
        {
            try
            {
                var id = (long)dynParam.id;
                var row = (int)dynParam.row;
                List<Cell> cells = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Cell>>(dynParam.cells.ToString());
                List<string> lstMessageError = Newtonsoft.Json.JsonConvert.DeserializeObject<List<string>>(dynParam.lstMessageError.ToString());

                var result = default(Row);
                if (id > 0 && cells.Count > 0 && row > 0 && row > 0)
                {
                    ServiceFactory.SVCategory((ISVCategory sv) =>
                    {
                        result = sv.CATGroupOfPartner_ExcelChange(id, row, cells,lstMessageError);
                    });
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public SYSExcel CATGroupOfPartner_ExcelImport(dynamic dynParam)
        {
            try
            {
                var id = (long)dynParam.id;
                List<Worksheet> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Worksheet>>(dynParam.worksheets.ToString());
                List<string> lstMessageError = Newtonsoft.Json.JsonConvert.DeserializeObject<List<string>>(dynParam.lstMessageError.ToString());

                var result = default(SYSExcel);
                if (id > 0 && lst.Count > 0 && lst[0].Rows.Count > 1)
                {
                    ServiceFactory.SVCategory((ISVCategory sv) =>
                    {
                        result = sv.CATGroupOfPartner_ExcelImport(id, lst[0].Rows,lstMessageError);
                    });
                }
                if (result != null && !string.IsNullOrEmpty(result.Data))
                {
                    result.Worksheets = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Worksheet>>(result.Data);
                }
                else
                {
                    result = new SYSExcel();
                    result.Worksheets = new List<Worksheet>();
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public bool CATGroupOfPartner_ExcelApprove(dynamic dynParam)
        {
            try
            {
                var id = (long)dynParam.id;
                var result = false;

                if (id > 0)
                {
                    ServiceFactory.SVCategory((ISVCategory sv) =>
                    {
                        result = sv.CATGroupOfPartner_ExcelApprove(id);
                    });
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region CATShift
        public DTOResult CATShift_Read(dynamic dynParam)
        {
            try
            {
                string request = dynParam.request.ToString();
                var result = default(DTOResult);
                ServiceFactory.SVCategory((IServices.ISVCategory sv) =>
                {
                    result = sv.CATShift_List(request);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public int CATShift_Update(dynamic dynParam)
        {
            try
            {
                CATShift item = Newtonsoft.Json.JsonConvert.DeserializeObject<CATShift>(dynParam.item.ToString());
                int result = -1;
                if (item != null)
                {
                    ServiceFactory.SVCategory((ISVCategory sv) =>
                    {
                        result = sv.CATShift_Save(item);
                    });
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public CATShift CATShift_Get(dynamic dynParam)
        {
            try
            {
                int id = (int)dynParam.ID;
                var result = default(CATShift);
                ServiceFactory.SVCategory((ISVCategory sv) =>
                {
                    result = sv.CATShift_Get(id);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void CATShift_Destroy(dynamic dynParam)
        {
            try
            {
                CATShift item = Newtonsoft.Json.JsonConvert.DeserializeObject<CATShift>(dynParam.item.ToString());
                if (item != null)
                {
                    ServiceFactory.SVCategory((ISVCategory sv) =>
                    {
                        sv.CATShift_Delete(item);
                    });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        #endregion

        #region CATGroupOfTrouble
        public DTOResult CATGroupOfTrouble_Read(dynamic dynParam)
        {
            try
            {
                string request = dynParam.request.ToString();
                var result = default(DTOResult);
                ServiceFactory.SVCategory((IServices.ISVCategory sv) =>
                {
                    result = sv.CATGroupOfTrouble_List(request);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public int CATGroupOfTrouble_Update(dynamic dynParam)
        {
            try
            {
                CATGroupOfTrouble item = Newtonsoft.Json.JsonConvert.DeserializeObject<CATGroupOfTrouble>(dynParam.item.ToString());
                int result = -1;
                if (item != null)
                {
                    ServiceFactory.SVCategory((ISVCategory sv) =>
                    {
                        result = sv.CATGroupOfTrouble_Save(item);
                    });
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public CATGroupOfTrouble CATGroupOfTrouble_Get(dynamic dynParam)
        {
            try
            {
                int id = (int)dynParam.ID;
                var result = default(CATGroupOfTrouble);
                ServiceFactory.SVCategory((ISVCategory sv) =>
                {
                    result = sv.CATGroupOfTrouble_Get(id);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void CATGroupOfTrouble_Destroy(dynamic dynParam)
        {
            try
            {
                CATGroupOfTrouble item = Newtonsoft.Json.JsonConvert.DeserializeObject<CATGroupOfTrouble>(dynParam.item.ToString());
                if (item != null)
                {
                    ServiceFactory.SVCategory((ISVCategory sv) =>
                    {
                        sv.CATGroupOfTrouble_Delete(item);
                    });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public SYSExcel CATGroupOfTrouble_ExcelInit(dynamic dynParam)
        {
            try
            {
                var functionid = (int)dynParam.functionid;
                var functionkey = dynParam.functionkey.ToString();
                var isreload = (bool)dynParam.isreload;

                var result = default(SYSExcel);

                ServiceFactory.SVCategory((ISVCategory sv) =>
                {
                    result = sv.CATGroupOfTrouble_ExcelInit(functionid, functionkey, isreload);
                });
                if (result != null && !string.IsNullOrEmpty(result.Data))
                {
                    result.Worksheets = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Worksheet>>(result.Data);
                }
                else
                {
                    result = new SYSExcel();
                    result.Worksheets = new List<Worksheet>();
                }
                result.Data = "";
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public Row CATGroupOfTrouble_ExcelChange(dynamic dynParam)
        {
            try
            {
                var id = (long)dynParam.id;
                var row = (int)dynParam.row;
                List<Cell> cells = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Cell>>(dynParam.cells.ToString());
                List<string> lstMessageError = Newtonsoft.Json.JsonConvert.DeserializeObject<List<string>>(dynParam.lstMessageError.ToString());
                var result = new Row();
                if (id > 0 && cells.Count > 0 && row > 0)
                {
                    ServiceFactory.SVCategory((ISVCategory sv) =>
                    {
                        result = sv.CATGroupOfTrouble_ExcelChange(id, row, cells, lstMessageError);
                    });
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public SYSExcel CATGroupOfTrouble_ExcelImport(dynamic dynParam)
        {
            try
            {
                var id = (long)dynParam.id;
                List<Worksheet> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Worksheet>>(dynParam.worksheets.ToString());
                List<string> lstMessageError = Newtonsoft.Json.JsonConvert.DeserializeObject<List<string>>(dynParam.lstMessageError.ToString());
                var result = default(SYSExcel);
                if (id > 0 && lst.Count > 0 && lst[0].Rows.Count > 1)
                {
                    ServiceFactory.SVCategory((ISVCategory sv) =>
                    {
                        result = sv.CATGroupOfTrouble_ExcelImport(id, lst[0].Rows, lstMessageError);
                    });
                }
                if (result != null && !string.IsNullOrEmpty(result.Data))
                {
                    result.Worksheets = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Worksheet>>(result.Data);
                }
                else
                {
                    result = new SYSExcel();
                    result.Worksheets = new List<Worksheet>();
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public bool CATGroupOfTrouble_ExcelApprove(dynamic dynParam)
        {
            try
            {
                var id = (long)dynParam.id;

                var result = false;
                if (id > 0)
                {
                    ServiceFactory.SVCategory((ISVCategory sv) =>
                    {
                        result = sv.CATGroupOfTrouble_ExcelApprove(id);
                    });

                    //if (result != null && !string.IsNullOrEmpty(result.Data))
                    //{
                    //    result.Worksheets = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Worksheet>>(result.Data);
                    //}
                    //else
                    //{
                    //    result = new SYSExcel();
                    //    result.Worksheets = new List<Worksheet>();
                    //}
                    //result.Data = "";
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region CATTypeBusiness
        public DTOResult CATTypeBusiness_Read(dynamic dynParam)
        {
            try
            {
                string request = dynParam.request.ToString();
                var result = default(DTOResult);
                ServiceFactory.SVCategory((IServices.ISVCategory sv) =>
                {
                    result = sv.CATTypeBusiness_List(request);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public int CATTypeBusiness_Update(dynamic dynParam)
        {
            try
            {
                CATTypeBusiness item = Newtonsoft.Json.JsonConvert.DeserializeObject<CATTypeBusiness>(dynParam.item.ToString());
                int result = -1;
                if (item != null)
                {
                    ServiceFactory.SVCategory((ISVCategory sv) =>
                    {
                        result = sv.CATTypeBusiness_Save(item);
                    });
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public CATTypeBusiness CATTypeBusiness_Get(dynamic dynParam)
        {
            try
            {
                int id = (int)dynParam.ID;
                var result = default(CATTypeBusiness);
                ServiceFactory.SVCategory((ISVCategory sv) =>
                {
                    result = sv.CATTypeBusiness_Get(id);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void CATTypeBusiness_Destroy(dynamic dynParam)
        {
            try
            {
                CATTypeBusiness item = Newtonsoft.Json.JsonConvert.DeserializeObject<CATTypeBusiness>(dynParam.item.ToString());
                if (item != null)
                {
                    ServiceFactory.SVCategory((ISVCategory sv) =>
                    {
                        sv.CATTypeBusiness_Delete(item);
                    });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public SYSExcel CATTypeBusiness_ExcelInit(dynamic dynParam)
        {
            try
            {
                var functionid = (int)dynParam.functionid;
                var functionkey = dynParam.functionkey.ToString();
                var isreload = (bool)dynParam.isreload;

                var result = default(SYSExcel);

                ServiceFactory.SVCategory((ISVCategory sv) =>
                {
                    result = sv.CATTypeBusiness_ExcelInit(functionid, functionkey, isreload);
                });
                if (result != null && !string.IsNullOrEmpty(result.Data))
                {
                    result.Worksheets = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Worksheet>>(result.Data);
                }
                else
                {
                    result = new SYSExcel();
                    result.Worksheets = new List<Worksheet>();
                }
                result.Data = "";
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public Row CATTypeBusiness_ExcelChange(dynamic dynParam)
        {
            try
            {
                var id = (long)dynParam.id;
                var row = (int)dynParam.row;
                List<Cell> cells = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Cell>>(dynParam.cells.ToString());
                List<string> lstMessageError = Newtonsoft.Json.JsonConvert.DeserializeObject<List<string>>(dynParam.lstMessageError.ToString());

                var result = default(Row);
                if (id > 0 && cells.Count > 0 && row > 0 && row > 0)
                {
                    ServiceFactory.SVCategory((ISVCategory sv) =>
                    {
                        result = sv.CATTypeBusiness_ExcelChange(id, row, cells,lstMessageError);
                    });
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public SYSExcel CATTypeBusiness_ExcelImport(dynamic dynParam)
        {
            try
            {
                var id = (long)dynParam.id;
                List<Worksheet> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Worksheet>>(dynParam.worksheets.ToString());
                List<string> lstMessageError = Newtonsoft.Json.JsonConvert.DeserializeObject<List<string>>(dynParam.lstMessageError.ToString());

                var result = default(SYSExcel);
                if (id > 0 && lst.Count > 0 && lst[0].Rows.Count > 1)
                {
                    ServiceFactory.SVCategory((ISVCategory sv) =>
                    {
                        result = sv.CATTypeBusiness_ExcelImport(id, lst[0].Rows,lstMessageError);
                    });
                }
                if (result != null && !string.IsNullOrEmpty(result.Data))
                {
                    result.Worksheets = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Worksheet>>(result.Data);
                }
                else
                {
                    result = new SYSExcel();
                    result.Worksheets = new List<Worksheet>();
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public bool CATTypeBusiness_ExcelApprove(dynamic dynParam)
        {
            try
            {
                var id = (long)dynParam.id;
                var result = false;

                if (id > 0)
                {
                    ServiceFactory.SVCategory((ISVCategory sv) =>
                    {
                        result = sv.CATTypeBusiness_ExcelApprove(id);
                    });
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region CATField
        public DTOResult CATField_Read(dynamic dynParam)
        {
            try
            {
                string request = dynParam.request.ToString();
                var result = default(DTOResult);
                ServiceFactory.SVCategory((IServices.ISVCategory sv) =>
                {
                    result = sv.CATField_List(request);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public int CATField_Update(dynamic dynParam)
        {
            try
            {
                CATField item = Newtonsoft.Json.JsonConvert.DeserializeObject<CATField>(dynParam.item.ToString());
                int result = -1;
                if (item != null)
                {
                    ServiceFactory.SVCategory((ISVCategory sv) =>
                    {
                        result = sv.CATField_Save(item);
                    });
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public CATField CATField_Get(dynamic dynParam)
        {
            try
            {
                int id = (int)dynParam.ID;
                var result = default(CATField);
                ServiceFactory.SVCategory((ISVCategory sv) =>
                {
                    result = sv.CATField_Get(id);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void CATField_Destroy(dynamic dynParam)
        {
            try
            {
                CATField item = Newtonsoft.Json.JsonConvert.DeserializeObject<CATField>(dynParam.item.ToString());
                if (item != null)
                {
                    ServiceFactory.SVCategory((ISVCategory sv) =>
                    {
                        sv.CATField_Delete(item);
                    });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        [HttpPost]
        public SYSExcel CATField_ExcelInit(dynamic dynParam)
        {
            try
            {
                var functionid = (int)dynParam.functionid;
                var functionkey = dynParam.functionkey.ToString();

                var result = default(SYSExcel);

                ServiceFactory.SVCategory((ISVCategory sv) =>
                {
                    result = sv.CATField_ExcelInit(functionid, functionkey);
                });
                if (result != null && !string.IsNullOrEmpty(result.Data))
                {
                    result.Worksheets = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Worksheet>>(result.Data);
                }
                else
                {
                    result = new SYSExcel();
                    result.Worksheets = new List<Worksheet>();
                }
                result.Data = "";
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public List<Row> CATField_ExcelChange(dynamic dynParam)
        {
            try
            {
                var id = (long)dynParam.id;
                var row = (int)dynParam.row;
                var col = (int)dynParam.col;
                var val = dynParam.val.ToString();
                List<Cell> cells = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Cell>>(dynParam.cells.ToString());

                var result = new List<Row>();
                if (id > 0 && cells.Count > 0 && row > 0 && col > 0)
                {
                    ServiceFactory.SVCategory((ISVCategory sv) =>
                    {
                        result = sv.CATField_ExcelChange(id, row, col, val);
                    });
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public SYSExcel CATField_ExcelImport(dynamic dynParam)
        {
            try
            {
                var id = (long)dynParam.id;
                List<Worksheet> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Worksheet>>(dynParam.worksheets.ToString());

                var result = default(SYSExcel);
                if (id > 0 && lst.Count > 0 && lst[0].Rows.Count > 1)
                {
                    ServiceFactory.SVCategory((ISVCategory sv) =>
                    {
                        result = sv.CATField_ExcelImport(id, lst[0].Rows);
                    });
                }
                if (result != null && !string.IsNullOrEmpty(result.Data))
                {
                    result.Worksheets = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Worksheet>>(result.Data);
                }
                else
                {
                    result = new SYSExcel();
                    result.Worksheets = new List<Worksheet>();
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public SYSExcel CATField_ExcelApprove(dynamic dynParam)
        {
            try
            {
                var id = (long)dynParam.id;

                var result = default(SYSExcel);
                if (id > 0)
                {
                    ServiceFactory.SVCategory((ISVCategory sv) =>
                    {
                        result = sv.CATField_ExcelApprove(id);
                    });

                    if (result != null && !string.IsNullOrEmpty(result.Data))
                    {
                        result.Worksheets = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Worksheet>>(result.Data);
                    }
                    else
                    {
                        result = new SYSExcel();
                        result.Worksheets = new List<Worksheet>();
                    }
                    result.Data = "";
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region CATScale
        public DTOResult CATScale_Read(dynamic dynParam)
        {
            try
            {
                string request = dynParam.request.ToString();
                var result = default(DTOResult);
                ServiceFactory.SVCategory((IServices.ISVCategory sv) =>
                {
                    result = sv.CATScale_List(request);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public int CATScale_Update(dynamic dynParam)
        {
            try
            {
                CATScale item = Newtonsoft.Json.JsonConvert.DeserializeObject<CATScale>(dynParam.item.ToString());
                int result = -1;
                if (item != null)
                {
                    ServiceFactory.SVCategory((ISVCategory sv) =>
                    {
                        result = sv.CATScale_Save(item);
                    });
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public CATScale CATScale_Get(dynamic dynParam)
        {
            try
            {
                int id = (int)dynParam.ID;
                var result = default(CATScale);
                ServiceFactory.SVCategory((ISVCategory sv) =>
                {
                    result = sv.CATScale_Get(id);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void CATScale_Destroy(dynamic dynParam)
        {
            try
            {
                CATScale item = Newtonsoft.Json.JsonConvert.DeserializeObject<CATScale>(dynParam.item.ToString());
                if (item != null)
                {
                    ServiceFactory.SVCategory((ISVCategory sv) =>
                    {
                        sv.CATScale_Delete(item);
                    });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public SYSExcel CATScale_ExcelInit(dynamic dynParam)
        {
            try
            {
                var functionid = (int)dynParam.functionid;
                var functionkey = dynParam.functionkey.ToString();
                var isreload = (bool)dynParam.isreload;

                var result = default(SYSExcel);

                ServiceFactory.SVCategory((ISVCategory sv) =>
                {
                    result = sv.CATScale_ExcelInit(functionid, functionkey, isreload);
                });
                if (result != null && !string.IsNullOrEmpty(result.Data))
                {
                    result.Worksheets = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Worksheet>>(result.Data);
                }
                else
                {
                    result = new SYSExcel();
                    result.Worksheets = new List<Worksheet>();
                }
                result.Data = "";
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public Row CATScale_ExcelChange(dynamic dynParam)
        {
            try
            {
                var id = (long)dynParam.id;
                var row = (int)dynParam.row;
                List<Cell> cells = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Cell>>(dynParam.cells.ToString());
                List<string> lstMessageError = Newtonsoft.Json.JsonConvert.DeserializeObject<List<string>>(dynParam.lstMessageError.ToString());

                var result = default(Row);
                if (id > 0 && cells.Count > 0 && row > 0 && row > 0)
                {
                    ServiceFactory.SVCategory((ISVCategory sv) =>
                    {
                        result = sv.CATScale_ExcelChange(id, row, cells,lstMessageError);
                    });
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public SYSExcel CATScale_ExcelImport(dynamic dynParam)
        {
            try
            {
                var id = (long)dynParam.id;
                List<Worksheet> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Worksheet>>(dynParam.worksheets.ToString());
                List<string> lstMessageError = Newtonsoft.Json.JsonConvert.DeserializeObject<List<string>>(dynParam.lstMessageError.ToString());

                var result = default(SYSExcel);
                if (id > 0 && lst.Count > 0 && lst[0].Rows.Count > 1)
                {
                    ServiceFactory.SVCategory((ISVCategory sv) =>
                    {
                        result = sv.CATScale_ExcelImport(id, lst[0].Rows,lstMessageError);
                    });
                }
                if (result != null && !string.IsNullOrEmpty(result.Data))
                {
                    result.Worksheets = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Worksheet>>(result.Data);
                }
                else
                {
                    result = new SYSExcel();
                    result.Worksheets = new List<Worksheet>();
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public bool CATScale_ExcelApprove(dynamic dynParam)
        {
            try
            {
                var id = (long)dynParam.id;
                var result = false;

                if (id > 0)
                {
                    ServiceFactory.SVCategory((ISVCategory sv) =>
                    {
                        result = sv.CATScale_ExcelApprove(id);
                    });
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region CATGroupOfCost
        public DTOResult CATGroupOfCost_Read(dynamic dynParam)
        {
            try
            {
                string request = dynParam.request.ToString();
                var result = default(DTOResult);
                ServiceFactory.SVCategory((IServices.ISVCategory sv) =>
                {
                    result = sv.CATGroupOfCost_List(request);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public int CATGroupOfCost_Update(dynamic dynParam)
        {
            try
            {
                CATGroupOfCost item = Newtonsoft.Json.JsonConvert.DeserializeObject<CATGroupOfCost>(dynParam.item.ToString());
                int result = -1;
                if (item != null)
                {
                    ServiceFactory.SVCategory((ISVCategory sv) =>
                    {
                        result = sv.CATGroupOfCost_Save(item);
                    });
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public CATGroupOfCost CATGroupOfCost_Get(dynamic dynParam)
        {
            try
            {
                int id = (int)dynParam.ID;
                var result = default(CATGroupOfCost);
                ServiceFactory.SVCategory((ISVCategory sv) =>
                {
                    result = sv.CATGroupOfCost_Get(id);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void CATGroupOfCost_Destroy(dynamic dynParam)
        {
            try
            {
                CATGroupOfCost item = Newtonsoft.Json.JsonConvert.DeserializeObject<CATGroupOfCost>(dynParam.item.ToString());
                if (item != null)
                {
                    ServiceFactory.SVCategory((ISVCategory sv) =>
                    {
                        sv.CATGroupOfCost_Delete(item);
                    });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public DTOResult CATGroupOfCost_GroupList(dynamic dynParam)
        {
            try
            {
                int id = (int)dynParam.ID;
                DTOResult result = new DTOResult();
                ServiceFactory.SVCategory((ISVCategory sv) =>
                {
                    result = sv.CATGroupOfCost_GroupList(id);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public SYSExcel CATGroupOfCost_ExcelInit(dynamic dynParam)
        {
            try
            {
                var functionid = (int)dynParam.functionid;
                var functionkey = dynParam.functionkey.ToString();
                var isreload = (bool)dynParam.isreload;

                var result = default(SYSExcel);

                ServiceFactory.SVCategory((ISVCategory sv) =>
                {
                    result = sv.CATGroupOfCost_ExcelInit(functionid, functionkey, isreload);
                });
                if (result != null && !string.IsNullOrEmpty(result.Data))
                {
                    result.Worksheets = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Worksheet>>(result.Data);
                }
                else
                {
                    result = new SYSExcel();
                    result.Worksheets = new List<Worksheet>();
                }
                result.Data = "";
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public Row CATGroupOfCost_ExcelChange(dynamic dynParam)
        {
            try
            {
                var id = (long)dynParam.id;
                var row = (int)dynParam.row;
                List<Cell> cells = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Cell>>(dynParam.cells.ToString());
                List<string> lstMessageError = Newtonsoft.Json.JsonConvert.DeserializeObject<List<string>>(dynParam.lstMessageError.ToString());

                var result = default(Row);
                if (id > 0 && cells.Count > 0 && row > 0 && row > 0)
                {
                    ServiceFactory.SVCategory((ISVCategory sv) =>
                    {
                        result = sv.CATGroupOfCost_ExcelChange(id, row, cells, lstMessageError);
                    });
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public SYSExcel CATGroupOfCost_ExcelImport(dynamic dynParam)
        {
            try
            {
                var id = (long)dynParam.id;
                List<Worksheet> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Worksheet>>(dynParam.worksheets.ToString());
                List<string> lstMessageError = Newtonsoft.Json.JsonConvert.DeserializeObject<List<string>>(dynParam.lstMessageError.ToString());

                var result = default(SYSExcel);
                if (id > 0 && lst.Count > 0 && lst[0].Rows.Count > 1)
                {
                    ServiceFactory.SVCategory((ISVCategory sv) =>
                    {
                        result = sv.CATGroupOfCost_ExcelImport(id, lst[0].Rows, lstMessageError);
                    });
                }
                if (result != null && !string.IsNullOrEmpty(result.Data))
                {
                    result.Worksheets = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Worksheet>>(result.Data);
                }
                else
                {
                    result = new SYSExcel();
                    result.Worksheets = new List<Worksheet>();
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public bool CATGroupOfCost_ExcelApprove(dynamic dynParam)
        {
            try
            {
                var id = (long)dynParam.id;
                var result = false;

                if (id > 0)
                {
                    ServiceFactory.SVCategory((ISVCategory sv) =>
                    {
                        result = sv.CATGroupOfCost_ExcelApprove(id);
                    });
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region CATCost
        public DTOResult CATCost_Read(dynamic dynParam)
        {
            try
            {
                string request = dynParam.request.ToString();
                var result = default(DTOResult);
                ServiceFactory.SVCategory((IServices.ISVCategory sv) =>
                {
                    result = sv.CATCost_List(request);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public int CATCost_Update(dynamic dynParam)
        {
            try
            {
                DTOCATCost item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOCATCost>(dynParam.item.ToString());
                int result = -1;
                if (item != null)
                {
                    ServiceFactory.SVCategory((ISVCategory sv) =>
                    {
                        result = sv.CATCost_Save(item);
                    });
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOCATCost CATCost_Get(dynamic dynParam)
        {
            try
            {
                int id = (int)dynParam.ID;
                var result = default(DTOCATCost);
                ServiceFactory.SVCategory((ISVCategory sv) =>
                {
                    result = sv.CATCost_Get(id);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void CATCost_Destroy(dynamic dynParam)
        {
            try
            {
                DTOCATCost item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOCATCost>(dynParam.item.ToString());
                if (item != null)
                {
                    ServiceFactory.SVCategory((ISVCategory sv) =>
                    {
                        sv.CATCost_Delete(item);
                    });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public SYSExcel CATCost_ExcelInit(dynamic dynParam)
        {
            try
            {
                var functionid = (int)dynParam.functionid;
                var functionkey = dynParam.functionkey.ToString();
                var isreload = (bool)dynParam.isreload;

                var result = default(SYSExcel);

                ServiceFactory.SVCategory((ISVCategory sv) =>
                {
                    result = sv.CATCost_ExcelInit(functionid, functionkey, isreload);
                });
                if (result != null && !string.IsNullOrEmpty(result.Data))
                {
                    result.Worksheets = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Worksheet>>(result.Data);
                }
                else
                {
                    result = new SYSExcel();
                    result.Worksheets = new List<Worksheet>();
                }
                result.Data = "";
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public Row CATCost_ExcelChange(dynamic dynParam)
        {
            try
            {
                var id = (long)dynParam.id;
                var row = (int)dynParam.row;
                List<Cell> cells = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Cell>>(dynParam.cells.ToString());
                List<string> lstMessageError = Newtonsoft.Json.JsonConvert.DeserializeObject<List<string>>(dynParam.lstMessageError.ToString());

                var result = default(Row);
                if (id > 0 && cells.Count > 0 && row > 0 && row > 0)
                {
                    ServiceFactory.SVCategory((ISVCategory sv) =>
                    {
                        result = sv.CATCost_ExcelChange(id, row, cells, lstMessageError);
                    });
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public SYSExcel CATCost_ExcelImport(dynamic dynParam)
        {
            try
            {
                var id = (long)dynParam.id;
                List<Worksheet> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Worksheet>>(dynParam.worksheets.ToString());
                List<string> lstMessageError = Newtonsoft.Json.JsonConvert.DeserializeObject<List<string>>(dynParam.lstMessageError.ToString());

                var result = default(SYSExcel);
                if (id > 0 && lst.Count > 0 && lst[0].Rows.Count > 1)
                {
                    ServiceFactory.SVCategory((ISVCategory sv) =>
                    {
                        result = sv.CATCost_ExcelImport(id, lst[0].Rows, lstMessageError);
                    });
                }
                if (result != null && !string.IsNullOrEmpty(result.Data))
                {
                    result.Worksheets = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Worksheet>>(result.Data);
                }
                else
                {
                    result = new SYSExcel();
                    result.Worksheets = new List<Worksheet>();
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public bool CATCost_ExcelApprove(dynamic dynParam)
        {
            try
            {
                var id = (long)dynParam.id;
                var result = false;

                if (id > 0)
                {
                    ServiceFactory.SVCategory((ISVCategory sv) =>
                    {
                        result = sv.CATCost_ExcelApprove(id);
                    });
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region SYSCustomer

        [HttpPost]
        public DTOResult SYSCustomer_List(dynamic dynParam)
        {
            try
            {
                string request = dynParam.request.ToString();
                var result = default(DTOResult);
                ServiceFactory.SVCategory((IServices.ISVCategory sv) =>
                {
                    result = sv.SYSCustomer_List(request);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public int SYSCustomer_Save(dynamic dynParam)
        {
            try
            {
                CUSCustomer item = Newtonsoft.Json.JsonConvert.DeserializeObject<CUSCustomer>(dynParam.item.ToString());
                if (item != null)
                {
                    ServiceFactory.SVCategory((ISVCategory sv) =>
                    {
                        item.ID = sv.SYSCustomer_Save(item);
                    });
                }
                return item.ID;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void SYSCustomer_Delete(dynamic dynParam)
        {
            try
            {
                CUSCustomer item = Newtonsoft.Json.JsonConvert.DeserializeObject<CUSCustomer>(dynParam.item.ToString());
                if (item != null)
                {
                    ServiceFactory.SVCategory((ISVCategory sv) =>
                    {
                        sv.SYSCustomer_Delete(item);
                    });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOResult SYSCustomer_Goal_List(dynamic dynParam)
        {
            try
            {
                int branchID = (int)dynParam.branchID;
                string request = dynParam.request.ToString();
                var result = default(DTOResult);
                ServiceFactory.SVCategory((IServices.ISVCategory sv) =>
                {
                    result = sv.SYSCustomer_Goal_List(request, branchID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public int SYSCustomer_Goal_Save(dynamic dynParam)
        {
            try
            {
                int branchID = (int)dynParam.branchID;
                CUSGoal item = Newtonsoft.Json.JsonConvert.DeserializeObject<CUSGoal>(dynParam.item.ToString());
                if (item != null)
                {
                    ServiceFactory.SVCategory((ISVCategory sv) =>
                    {
                        item.ID = sv.SYSCustomer_Goal_Save(item, branchID);
                    });
                }
                return item.ID;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void SYSCustomer_Goal_Delete(dynamic dynParam)
        {
            try
            {
                CUSGoal item = Newtonsoft.Json.JsonConvert.DeserializeObject<CUSGoal>(dynParam.item.ToString());
                if (item != null)
                {
                    ServiceFactory.SVCategory((ISVCategory sv) =>
                    {
                        sv.SYSCustomer_Goal_Delete(item);
                    });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void SYSCustomer_Goal_Reset(dynamic dynParam)
        {
            try
            {
                int goalID = (int)dynParam.goalID;
                ServiceFactory.SVCategory((ISVCategory sv) =>
                {
                    sv.SYSCustomer_Goal_Reset(goalID);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOResult SYSCustomer_GoalDetail_List(dynamic dynParam)
        {
            try
            {
                int goalID = (int)dynParam.goalID;
                string request = dynParam.request.ToString();
                var result = default(DTOResult);
                ServiceFactory.SVCategory((IServices.ISVCategory sv) =>
                {
                    result = sv.SYSCustomer_GoalDetail_List(request, goalID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public int SYSCustomer_GoalDetail_Save(dynamic dynParam)
        {
            try
            {
                int goalID = (int)dynParam.goalID;
                CUSGoalDetail item = Newtonsoft.Json.JsonConvert.DeserializeObject<CUSGoalDetail>(dynParam.item.ToString());
                if (item != null)
                {
                    ServiceFactory.SVCategory((ISVCategory sv) =>
                    {
                        item.ID = sv.SYSCustomer_GoalDetail_Save(item, goalID);
                    });
                }
                return item.ID;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region SYSSetting

        [HttpPost]
        public DTOSYSSetting SYSSetting_Get(dynamic dynParam)
        {
            try
            {
                var result = default(DTOSYSSetting);
                int? syscusID = (int?)dynParam.syscusID;
                ServiceFactory.SVCategory((IServices.ISVCategory sv) =>
                {
                    result = sv.SYSSetting_Get(syscusID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void SYSSetting_Save(dynamic dynParam)
        {
            try
            {
                DTOSYSSetting item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOSYSSetting>(dynParam.item.ToString());
                if (item != null)
                {
                    ServiceFactory.SVCategory((ISVCategory sv) =>
                    {
                        sv.SYSSetting_Save(item);
                    });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public void SYSSetting_CheckApplySeaportCarrier(dynamic dynParam)
        {
            try
            {
                ServiceFactory.SVCategory((ISVCategory sv) =>
                {
                    sv.SYSSetting_CheckApplySeaportCarrier();
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region SYSMaterial

        [HttpPost]
        public DTOTriggerMaterial SYSMaterial_Get(dynamic dynParam)
        {
            try
            {
                var result = default(DTOTriggerMaterial);
                int? syscusID = (int?)dynParam.syscusID;
                ServiceFactory.SVCategory((IServices.ISVCategory sv) =>
                {
                    result = sv.SYSMaterial_Get(syscusID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void SYSMaterial_Save(dynamic dynParam)
        {
            try
            {
                DTOTriggerMaterial item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOTriggerMaterial>(dynParam.item.ToString());
                if (item != null)
                {
                    ServiceFactory.SVCategory((ISVCategory sv) =>
                    {
                        sv.SYSMaterial_Save(item);
                    });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region CATSYSCustomerTrouble
        public DTOResult CATSYSCustomerTrouble_SysCustomer_List(dynamic dynParam)
        {
            try
            {
                string request = dynParam.request.ToString();
                var result = default(DTOResult);
                ServiceFactory.SVCategory((IServices.ISVCategory sv) =>
                {
                    result = sv.CATSYSCustomerTrouble_SysCustomer_List(request);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DTOResult CATSYSCustomerTrouble_Trouble_List(dynamic dynParam)
        {
            try
            {
                int id = (int)dynParam.id;
                string request = dynParam.request.ToString();
                DTOResult result = new DTOResult();
                ServiceFactory.SVCategory((IServices.ISVCategory sv) =>
                {
                    result = sv.CATSYSCustomerTrouble_Trouble_List(request, id);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void CATSYSCustomerTrouble_Trouble_Save(dynamic dynParam)
        {
            try
            {
                int id = (int)dynParam.id;
                List<DTOCATSYSCustomerTrouble> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DTOCATSYSCustomerTrouble>>(dynParam.lst.ToString());
                ServiceFactory.SVCategory((IServices.ISVCategory sv) =>
                {
                    sv.CATSYSCustomerTrouble_Trouble_Save(lst, id);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Container Packing
        [HttpPost]
        public DTOResult CATPackingCO_List(dynamic dynParam)
        {
            try
            {
                string request = string.Empty;
                var result = default(DTOResult);
                ServiceFactory.SVCategory((IServices.ISVCategory sv) =>
                {
                    result = sv.CATPackingCO_List(request);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DTOResult ContainerPacking_List()
        {
            try
            {
                var result = default(DTOResult);
                ServiceFactory.SVCategory((IServices.ISVCategory sv) =>
                {
                    result = sv.ContainerPacking_List();
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public int ContainerPacking_Save(dynamic dynParam)
        {
            try
            {
                DTOCATPacking item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOCATPacking>(dynParam.item.ToString());
                int result = -1;
                if (item != null)
                {
                    ServiceFactory.SVCategory((ISVCategory sv) =>
                    {
                        result = sv.ContainerPacking_Save(item);
                    });
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        [HttpPost]
        public DTOCATPacking ContainerPacking_Get(dynamic dynParam)
        {
            try
            {
                int id = (int)dynParam.id;
                var result = default(DTOCATPacking);
                ServiceFactory.SVCategory((ISVCategory sv) =>
                {
                    result = sv.ContainerPacking_Get(id);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void ContainerPacking_Delete(dynamic dynParam)
        {
            try
            {
                DTOCATPacking item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOCATPacking>(dynParam.item.ToString());
                ServiceFactory.SVCategory((ISVCategory sv) =>
                {
                    sv.ContainerPacking_Delete(item);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region CATPacking

        public DTOResult CATPackingGOPTU_Read(dynamic dynParam)
        {
            try
            {
                string request = dynParam.request.ToString();
                var result = default(DTOResult);
                ServiceFactory.SVCategory((IServices.ISVCategory sv) =>
                {
                    result = sv.CATPackingGOPTU_List(request);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public int CATPackingGOPTU_Update(dynamic dynParam)
        {
            try
            {
                DTOCATPacking item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOCATPacking>(dynParam.item.ToString());
                int result = -1;
                if (item != null)
                {
                    ServiceFactory.SVCategory((ISVCategory sv) =>
                    {
                        result = sv.CATPackingGOPTU_Save(item);
                    });
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOResult CATPackingGOP_List(dynamic dynParam)
        {
            try
            {
                string request = string.Empty;
                var result = default(DTOResult);
                ServiceFactory.SVCategory((IServices.ISVCategory sv) =>
                {
                    result = sv.CATPackingGOP_List(request);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public DTOCATPacking CATPackingGOPTU_Get(dynamic dynParam)
        {
            try
            {
                int id = (int)dynParam.id;
                var result = default(DTOCATPacking);
                ServiceFactory.SVCategory((ISVCategory sv) =>
                {
                    result = sv.CATPackingGOPTU_Get(id);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void CATPackingGOPTU_Destroy(dynamic dynParam)
        {
            try
            {
                DTOCATPacking item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOCATPacking>(dynParam.item.ToString());
                ServiceFactory.SVCategory((ISVCategory sv) =>
                {
                    sv.CATPackingGOPTU_Delete(item);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public SYSExcel CATPackingGOPTU_ExcelInit(dynamic dynParam)
        {
            try
            {
                var functionid = (int)dynParam.functionid;
                var functionkey = dynParam.functionkey.ToString();
                var isreload = (bool)dynParam.isreload;

                var result = default(SYSExcel);

                ServiceFactory.SVCategory((ISVCategory sv) =>
                {
                    result = sv.CATPackingGOPTU_ExcelInit(functionid, functionkey, isreload);
                });
                if (result != null && !string.IsNullOrEmpty(result.Data))
                {
                    result.Worksheets = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Worksheet>>(result.Data);
                }
                else
                {
                    result = new SYSExcel();
                    result.Worksheets = new List<Worksheet>();
                }
                result.Data = "";
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public Row CATPackingGOPTU_ExcelChange(dynamic dynParam)
        {
            try
            {
                var id = (long)dynParam.id;
                var row = (int)dynParam.row;
                List<Cell> cells = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Cell>>(dynParam.cells.ToString());
                List<string> lstMessageError = Newtonsoft.Json.JsonConvert.DeserializeObject<List<string>>(dynParam.lstMessageError.ToString());

                var result = new Row();
                if (id > 0 && cells.Count > 0 && row > 0)
                {
                    ServiceFactory.SVCategory((ISVCategory sv) =>
                    {
                        result = sv.CATPackingGOPTU_ExcelChange(id, row, cells, lstMessageError);
                    });
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public SYSExcel CATPackingGOPTU_ExcelImport(dynamic dynParam)
        {
            try
            {
                var id = (long)dynParam.id;
                List<Worksheet> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Worksheet>>(dynParam.worksheets.ToString());
                List<string> lstMessageError = Newtonsoft.Json.JsonConvert.DeserializeObject<List<string>>(dynParam.lstMessageError.ToString());

                var result = default(SYSExcel);
                if (id > 0 && lst.Count > 0 && lst[0].Rows.Count > 1)
                {
                    ServiceFactory.SVCategory((ISVCategory sv) =>
                    {
                        result = sv.CATPackingGOPTU_ExcelImport(id, lst[0].Rows, lstMessageError);
                    });
                }
                if (result != null && !string.IsNullOrEmpty(result.Data))
                {
                    result.Worksheets = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Worksheet>>(result.Data);
                }
                else
                {
                    result = new SYSExcel();
                    result.Worksheets = new List<Worksheet>();
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public bool CATPackingGOPT_ExcelApprove(dynamic dynParam)
        {
            try
            {
                var id = (long)dynParam.id;

                var result = false;
                if (id > 0)
                {
                    ServiceFactory.SVCategory((ISVCategory sv) =>
                    {
                        result = sv.CATPackingGOPT_ExcelApprove(id);
                    });

                    //if (result != null && !string.IsNullOrEmpty(result.Data))
                    //{
                    //    result.Worksheets = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Worksheet>>(result.Data);
                    //}
                    //else
                    //{
                    //    result = new SYSExcel();
                    //    result.Worksheets = new List<Worksheet>();
                    //}
                    //result.Data = "";
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region CATTypeOfRouteProblem

        public DTOResult CATTypeOfRouteProblem_Read(dynamic dynParam)
        {
            try
            {
                string request = dynParam.request.ToString();
                var result = default(DTOResult);
                ServiceFactory.SVCategory((IServices.ISVCategory sv) =>
                {
                    result = sv.CATTypeOfRouteProblem_List(request);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public int CATTypeOfRouteProblem_Save(dynamic dynParam)
        {
            try
            {
                DTOCATTypeOfRouteProblem item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOCATTypeOfRouteProblem>(dynParam.item.ToString());
                int result = -1;
                if (item != null)
                {
                    ServiceFactory.SVCategory((ISVCategory sv) =>
                    {
                        result = sv.CATTypeOfRouteProblem_Save(item);
                    });
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOCATTypeOfRouteProblem CATTypeOfRouteProblem_Get(dynamic dynParam)
        {
            try
            {
                int id = (int)dynParam.id;
                var result = default(DTOCATTypeOfRouteProblem);
                ServiceFactory.SVCategory((ISVCategory sv) =>
                {
                    result = sv.CATTypeOfRouteProblem_Get(id);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void CATTypeOfRouteProblem_Destroy(dynamic dynParam)
        {
            try
            {
                DTOCATTypeOfRouteProblem item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOCATTypeOfRouteProblem>(dynParam.item.ToString());
                if (item != null)
                {
                    ServiceFactory.SVCategory((ISVCategory sv) =>
                    {
                        sv.CATTypeOfRouteProblem_Delete(item);
                    });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public SYSExcel CATTypeOfRouteProblem_ExcelInit(dynamic dynParam)
        {
            try
            {
                var functionid = (int)dynParam.functionid;
                var functionkey = dynParam.functionkey.ToString();
                var isreload = (bool)dynParam.isreload;

                var result = default(SYSExcel);

                ServiceFactory.SVCategory((ISVCategory sv) =>
                {
                    result = sv.CATTypeOfRouteProblem_ExcelInit(functionid, functionkey, isreload);
                });
                if (result != null && !string.IsNullOrEmpty(result.Data))
                {
                    result.Worksheets = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Worksheet>>(result.Data);
                }
                else
                {
                    result = new SYSExcel();
                    result.Worksheets = new List<Worksheet>();
                }
                result.Data = "";
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public Row CATTypeOfRouteProblem_ExcelChange(dynamic dynParam)
        {
            try
            {
                var id = (long)dynParam.id;
                var row = (int)dynParam.row;
                List<Cell> cells = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Cell>>(dynParam.cells.ToString());
                List<string> lstMessageError = Newtonsoft.Json.JsonConvert.DeserializeObject<List<string>>(dynParam.lstMessageError.ToString());

                var result = default(Row);
                if (id > 0 && cells.Count > 0 && row > 0 && row > 0)
                {
                    ServiceFactory.SVCategory((ISVCategory sv) =>
                    {
                        result = sv.CATTypeOfRouteProblem_ExcelChange(id, row, cells,lstMessageError);
                    });
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public SYSExcel CATTypeOfRouteProblem_ExcelImport(dynamic dynParam)
        {
            try
            {
                var id = (long)dynParam.id;
                List<Worksheet> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Worksheet>>(dynParam.worksheets.ToString());
                List<string> lstMessageError = Newtonsoft.Json.JsonConvert.DeserializeObject<List<string>>(dynParam.lstMessageError.ToString());

                var result = default(SYSExcel);
                if (id > 0 && lst.Count > 0 && lst[0].Rows.Count > 1)
                {
                    ServiceFactory.SVCategory((ISVCategory sv) =>
                    {
                        result = sv.CATTypeOfRouteProblem_ExcelImport(id, lst[0].Rows,lstMessageError);
                    });
                }
                if (result != null && !string.IsNullOrEmpty(result.Data))
                {
                    result.Worksheets = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Worksheet>>(result.Data);
                }
                else
                {
                    result = new SYSExcel();
                    result.Worksheets = new List<Worksheet>();
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public bool CATTypeOfRouteProblem_ExcelApprove(dynamic dynParam)
        {
            try
            {
                var id = (long)dynParam.id;
                var result = false;

                if (id > 0)
                {
                    ServiceFactory.SVCategory((ISVCategory sv) =>
                    {
                        result = sv.CATTypeOfRouteProblem_ExcelApprove(id);
                    });
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region CATTypeOfPriceDIEx

        public DTOResult CATTypeOfPriceDIEx_List(dynamic dynParam)
        {
            try
            {
                string request = dynParam.request.ToString();
                var result = default(DTOResult);
                ServiceFactory.SVCategory((IServices.ISVCategory sv) =>
                {
                    result = sv.CATTypeOfPriceDIEx_List(request);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public int CATTypeOfPriceDIEx_Save(dynamic dynParam)
        {
            try
            {
                DTOCATTypeOfPriceDIEx item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOCATTypeOfPriceDIEx>(dynParam.item.ToString());
                int result = -1;
                if (item != null)
                {
                    ServiceFactory.SVCategory((ISVCategory sv) =>
                    {
                        result = sv.CATTypeOfPriceDIEx_Save(item);
                    });
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOCATTypeOfPriceDIEx CATTypeOfPriceDIEx_Get(dynamic dynParam)
        {
            try
            {
                int id = (int)dynParam.id;
                var result = default(DTOCATTypeOfPriceDIEx);
                ServiceFactory.SVCategory((ISVCategory sv) =>
                {
                    result = sv.CATTypeOfPriceDIEx_Get(id);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void CATTypeOfPriceDIEx_Delete(dynamic dynParam)
        {
            try
            {
                DTOCATTypeOfPriceDIEx item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOCATTypeOfPriceDIEx>(dynParam.item.ToString());
                if (item != null)
                {
                    ServiceFactory.SVCategory((ISVCategory sv) =>
                    {
                        sv.CATTypeOfPriceDIEx_Delete(item);
                    });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public SYSExcel CATTypeOfPriceDIEx_ExcelInit(dynamic dynParam)
        {
            try
            {
                var functionid = (int)dynParam.functionid;
                var functionkey = dynParam.functionkey.ToString();
                var isreload = (bool)dynParam.isreload;

                var result = default(SYSExcel);

                ServiceFactory.SVCategory((ISVCategory sv) =>
                {
                    result = sv.CATTypeOfPriceDIEx_ExcelInit(functionid, functionkey, isreload);
                });
                if (result != null && !string.IsNullOrEmpty(result.Data))
                {
                    result.Worksheets = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Worksheet>>(result.Data);
                }
                else
                {
                    result = new SYSExcel();
                    result.Worksheets = new List<Worksheet>();
                }
                result.Data = "";
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public Row CATTypeOfPriceDIEx_ExcelChange(dynamic dynParam)
        {
            try
            {
                var id = (long)dynParam.id;
                var row = (int)dynParam.row;
                List<Cell> cells = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Cell>>(dynParam.cells.ToString());
                List<string> lstMessageError = Newtonsoft.Json.JsonConvert.DeserializeObject<List<string>>(dynParam.lstMessageError.ToString());

                var result = default(Row);
                if (id > 0 && cells.Count > 0 && row > 0 && row > 0)
                {
                    ServiceFactory.SVCategory((ISVCategory sv) =>
                    {
                        result = sv.CATTypeOfPriceDIEx_ExcelChange(id, row, cells,lstMessageError);
                    });
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public SYSExcel CATTypeOfPriceDIEx_ExcelImport(dynamic dynParam)
        {
            try
            {
                var id = (long)dynParam.id;
                List<Worksheet> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Worksheet>>(dynParam.worksheets.ToString());
                List<string> lstMessageError = Newtonsoft.Json.JsonConvert.DeserializeObject<List<string>>(dynParam.lstMessageError.ToString());
                var result = default(SYSExcel);
                if (id > 0 && lst.Count > 0 && lst[0].Rows.Count > 1)
                {
                    ServiceFactory.SVCategory((ISVCategory sv) =>
                    {
                        result = sv.CATTypeOfPriceDIEx_ExcelImport(id, lst[0].Rows,lstMessageError);
                    });
                }
                if (result != null && !string.IsNullOrEmpty(result.Data))
                {
                    result.Worksheets = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Worksheet>>(result.Data);
                }
                else
                {
                    result = new SYSExcel();
                    result.Worksheets = new List<Worksheet>();
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public bool CATTypeOfPriceDIEx_ExcelApprove(dynamic dynParam)
        {
            try
            {
                var id = (long)dynParam.id;
                var result = false;

                if (id > 0)
                {
                    ServiceFactory.SVCategory((ISVCategory sv) =>
                    {
                        result = sv.CATTypeOfPriceDIEx_ExcelApprove(id);
                    });
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion 

        #region CATTypeOfPriceCOEx
        public DTOResult CATTypeOfPriceCOEx_List(dynamic dynParam)
        {
            try
            {
                string request = dynParam.request.ToString();
                var result = default(DTOResult);
                ServiceFactory.SVCategory((IServices.ISVCategory sv) =>
                {
                    result = sv.CATTypeOfPriceCOEx_List(request);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public int CATTypeOfPriceCOEx_Save(dynamic dynParam)
        {
            try
            {
                DTOCATTypeOfPriceCOEx item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOCATTypeOfPriceCOEx>(dynParam.item.ToString());
                int result = -1;
                if (item != null)
                {
                    ServiceFactory.SVCategory((ISVCategory sv) =>
                    {
                        result = sv.CATTypeOfPriceCOEx_Save(item);
                    });
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOCATTypeOfPriceCOEx CATTypeOfPriceCOEx_Get(dynamic dynParam)
        {
            try
            {
                int id = (int)dynParam.id;
                var result = default(DTOCATTypeOfPriceCOEx);
                ServiceFactory.SVCategory((ISVCategory sv) =>
                {
                    result = sv.CATTypeOfPriceCOEx_Get(id);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void CATTypeOfPriceCOEx_Delete(dynamic dynParam)
        {
            try
            {
                DTOCATTypeOfPriceCOEx item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOCATTypeOfPriceCOEx>(dynParam.item.ToString());
                if (item != null)
                {
                    ServiceFactory.SVCategory((ISVCategory sv) =>
                    {
                        sv.CATTypeOfPriceCOEx_Delete(item);
                    });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region CATGroupOfLocation
        public DTOResult CATGroupOfLocation_Read(dynamic dynParam)
        {
            try
            {
                string request = dynParam.request.ToString();
                var result = default(DTOResult);
                ServiceFactory.SVCategory((IServices.ISVCategory sv) =>
                {
                    result = sv.CATGroupOfLocation_List(request);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public int CATGroupOfLocation_Save(dynamic dynParam)
        {
            try
            {
                DTOCATGroupOfLocation item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOCATGroupOfLocation>(dynParam.item.ToString());
                int result = -1;
                if (item != null)
                {
                    ServiceFactory.SVCategory((ISVCategory sv) =>
                    {
                        result = sv.CATGroupOfLocation_Save(item);
                    });
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOCATGroupOfLocation CATGroupOfLocation_Get(dynamic dynParam)
        {
            try
            {
                int id = (int)dynParam.id;
                var result = default(DTOCATGroupOfLocation);
                ServiceFactory.SVCategory((ISVCategory sv) =>
                {
                    result = sv.CATGroupOfLocation_Get(id);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void CATGroupOfLocation_Destroy(dynamic dynParam)
        {
            try
            {
                DTOCATGroupOfLocation item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOCATGroupOfLocation>(dynParam.item.ToString());
                if (item != null)
                {
                    ServiceFactory.SVCategory((ISVCategory sv) =>
                    {
                        sv.CATGroupOfLocation_Delete(item);
                    });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public SYSExcel CATGroupOfLocation_ExcelInit(dynamic dynParam)
        {
            try
            {
                var functionid = (int)dynParam.functionid;
                var functionkey = dynParam.functionkey.ToString();
                var isreload = (bool)dynParam.isreload;

                var result = default(SYSExcel);

                ServiceFactory.SVCategory((ISVCategory sv) =>
                {
                    result = sv.CATGroupOfLocation_ExcelInit(functionid, functionkey, isreload);
                });
                if (result != null && !string.IsNullOrEmpty(result.Data))
                {
                    result.Worksheets = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Worksheet>>(result.Data);
                }
                else
                {
                    result = new SYSExcel();
                    result.Worksheets = new List<Worksheet>();
                }
                result.Data = "";
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public Row CATGroupOfLocation_ExcelChange(dynamic dynParam)
        {
            try
            {
                var id = (long)dynParam.id;
                var row = (int)dynParam.row;
                List<Cell> cells = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Cell>>(dynParam.cells.ToString());
                List<string> lstMessageError = Newtonsoft.Json.JsonConvert.DeserializeObject<List<string>>(dynParam.lstMessageError.ToString());

                var result = default(Row);
                if (id > 0 && cells.Count > 0 && row > 0 && row > 0)
                {
                    ServiceFactory.SVCategory((ISVCategory sv) =>
                    {
                        result = sv.CATGroupOfLocation_ExcelChange(id, row, cells, lstMessageError);
                    });
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public SYSExcel CATGroupOfLocation_ExcelImport(dynamic dynParam)
        {
            try
            {
                var id = (long)dynParam.id;
                List<Worksheet> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Worksheet>>(dynParam.worksheets.ToString());
                List<string> lstMessageError = Newtonsoft.Json.JsonConvert.DeserializeObject<List<string>>(dynParam.lstMessageError.ToString());

                var result = default(SYSExcel);
                if (id > 0 && lst.Count > 0 && lst[0].Rows.Count > 1)
                {
                    ServiceFactory.SVCategory((ISVCategory sv) =>
                    {
                        result = sv.CATGroupOfLocation_ExcelImport(id, lst[0].Rows, lstMessageError);
                    });
                }
                if (result != null && !string.IsNullOrEmpty(result.Data))
                {
                    result.Worksheets = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Worksheet>>(result.Data);
                }
                else
                {
                    result = new SYSExcel();
                    result.Worksheets = new List<Worksheet>();
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public bool CATGroupOfLocation_ExcelApprove(dynamic dynParam)
        {
            try
            {
                var id = (long)dynParam.id;
                var result = false;

                if (id > 0)
                {
                    ServiceFactory.SVCategory((ISVCategory sv) =>
                    {
                        result = sv.CATGroupOfLocation_ExcelApprove(id);
                    });
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region OPSTypeOfDITOGroupProductReturn
        public DTOResult OPSTypeOfDITOGroupProductReturn_List(dynamic dynParam)
        {
            try
            {
                string request = dynParam.request.ToString();
                var result = default(DTOResult);
                ServiceFactory.SVCategory((IServices.ISVCategory sv) =>
                {
                    result = sv.OPSTypeOfDITOGroupProductReturn_List(request);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public int OPSTypeOfDITOGroupProductReturn_Save(dynamic dynParam)
        {
            try
            {
                DTOOPSTypeOfDITOGroupProductReturn item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOOPSTypeOfDITOGroupProductReturn>(dynParam.item.ToString());
                int result = -1;
                if (item != null)
                {
                    ServiceFactory.SVCategory((ISVCategory sv) =>
                    {
                        result = sv.OPSTypeOfDITOGroupProductReturn_Save(item);
                    });
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOOPSTypeOfDITOGroupProductReturn OPSTypeOfDITOGroupProductReturn_Get(dynamic dynParam)
        {
            try
            {
                int id = (int)dynParam.id;
                var result = default(DTOOPSTypeOfDITOGroupProductReturn);
                ServiceFactory.SVCategory((ISVCategory sv) =>
                {
                    result = sv.OPSTypeOfDITOGroupProductReturn_Get(id);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void OPSTypeOfDITOGroupProductReturn_Delete(dynamic dynParam)
        {
            try
            {
                DTOOPSTypeOfDITOGroupProductReturn item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOOPSTypeOfDITOGroupProductReturn>(dynParam.item.ToString());
                if (item != null)
                {
                    ServiceFactory.SVCategory((ISVCategory sv) =>
                    {
                        sv.OPSTypeOfDITOGroupProductReturn_Delete(item);
                    });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public SYSExcel OPSTypeOfDITOGroupProductReturn_ExcelInit(dynamic dynParam)
        {
            try
            {
                var functionid = (int)dynParam.functionid;
                var functionkey = dynParam.functionkey.ToString();
                var isreload = (bool)dynParam.isreload;
                var result = default(SYSExcel);

                ServiceFactory.SVCategory((ISVCategory sv) =>
                {
                    result = sv.OPSTypeOfDITOGroupProductReturn_ExcelInit(functionid, functionkey, isreload);
                });
                if (result != null && !string.IsNullOrEmpty(result.Data))
                {
                    result.Worksheets = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Worksheet>>(result.Data);
                }
                else
                {
                    result = new SYSExcel();
                    result.Worksheets = new List<Worksheet>();
                }
                result.Data = "";
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public Row OPSTypeOfDITOGroupProductReturn_ExcelChange(dynamic dynParam)
        {
            try
            {
                var id = (long)dynParam.id;
                var row = (int)dynParam.row;
                List<Cell> cells = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Cell>>(dynParam.cells.ToString());
                List<string> lstMessageError = Newtonsoft.Json.JsonConvert.DeserializeObject<List<string>>(dynParam.lstMessageError.ToString());

                var result = new Row();
                if (id > 0 && cells.Count > 0 && row > 0)
                {
                    ServiceFactory.SVCategory((ISVCategory sv) =>
                    {
                        result = sv.OPSTypeOfDITOGroupProductReturn_ExcelChange(id, row, cells, lstMessageError);
                    });
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public SYSExcel OPSTypeOfDITOGroupProductReturn_ExcelImport(dynamic dynParam)
        {
            try
            {
                var id = (long)dynParam.id;
                List<Worksheet> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Worksheet>>(dynParam.worksheets.ToString());
                List<string> lstMessageError = Newtonsoft.Json.JsonConvert.DeserializeObject<List<string>>(dynParam.lstMessageError.ToString());

                var result = default(SYSExcel);
                if (id > 0 && lst.Count > 0 && lst[0].Rows.Count > 1)
                {
                    ServiceFactory.SVCategory((ISVCategory sv) =>
                    {
                        result = sv.OPSTypeOfDITOGroupProductReturn_ExcelImport(id, lst[0].Rows, lstMessageError);
                    });
                }
                if (result != null && !string.IsNullOrEmpty(result.Data))
                {
                    result.Worksheets = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Worksheet>>(result.Data);
                }
                else
                {
                    result = new SYSExcel();
                    result.Worksheets = new List<Worksheet>();
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public bool OPSTypeOfDITOGroupProductReturn_ExcelApprove(dynamic dynParam)
        {
            try
            {
                var id = (long)dynParam.id;

                var result = false;
                if (id > 0)
                {
                    ServiceFactory.SVCategory((ISVCategory sv) =>
                    {
                        result = sv.OPSTypeOfDITOGroupProductReturn_ExcelApprove(id);
                    });

                    //if (result != null && !string.IsNullOrEmpty(result.Data))
                    //{
                    //    result.Worksheets = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Worksheet>>(result.Data);
                    //}
                    //else
                    //{
                    //    result = new SYSExcel();
                    //    result.Worksheets = new List<Worksheet>();
                    //}
                    //result.Data = "";
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region CATTypeOfDriverFee
        public DTOResult CATTypeOfDriverFee_List(dynamic dynParam)
        {
            try
            {
                string request = dynParam.request.ToString();
                var result = default(DTOResult);
                ServiceFactory.SVCategory((IServices.ISVCategory sv) =>
                {
                    result = sv.CATTypeOfDriverFee_List(request);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public int CATTypeOfDriverFee_Save(dynamic dynParam)
        {
            try
            {
                DTOCATTypeOfDriverFee item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOCATTypeOfDriverFee>(dynParam.item.ToString());
                int result = -1;
                if (item != null)
                {
                    ServiceFactory.SVCategory((ISVCategory sv) =>
                    {
                        result = sv.CATTypeOfDriverFee_Save(item);
                    });
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOCATTypeOfDriverFee CATTypeOfDriverFee_Get(dynamic dynParam)
        {
            try
            {
                int id = (int)dynParam.id;
                var result = default(DTOCATTypeOfDriverFee);
                ServiceFactory.SVCategory((ISVCategory sv) =>
                {
                    result = sv.CATTypeOfDriverFee_Get(id);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void CATTypeOfDriverFee_Delete(dynamic dynParam)
        {
            try
            {
                DTOCATTypeOfDriverFee item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOCATTypeOfDriverFee>(dynParam.item.ToString());
                if (item != null)
                {
                    ServiceFactory.SVCategory((ISVCategory sv) =>
                    {
                        sv.CATTypeOfDriverFee_Delete(item);
                    });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //FLMSetting
        public DTOResult CATDriverFeeSum_List(dynamic dynParam)
        {
            try
            {
                DTOResult result = new DTOResult();
                ServiceFactory.SVCategory((ISVCategory sv) =>
                {
                    result = sv.ALL_SysVar(SYSVarType.DriverFeeSum);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public SYSExcel CATTypeOfDriverFee_ExcelInit(dynamic dynParam)
        {
            try
            {
                var functionid = (int)dynParam.functionid;
                var functionkey = dynParam.functionkey.ToString();
                var isreload = (bool)dynParam.isreload;

                var result = default(SYSExcel);

                ServiceFactory.SVCategory((ISVCategory sv) =>
                {
                    result = sv.CATTypeOfDriverFee_ExcelInit(functionid, functionkey, isreload);
                });
                if (result != null && !string.IsNullOrEmpty(result.Data))
                {
                    result.Worksheets = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Worksheet>>(result.Data);
                }
                else
                {
                    result = new SYSExcel();
                    result.Worksheets = new List<Worksheet>();
                }
                result.Data = "";
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public Row CATTypeOfDriverFee_ExcelChange(dynamic dynParam)
        {
            try
            {
                var id = (long)dynParam.id;
                var row = (int)dynParam.row;
                List<Cell> cells = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Cell>>(dynParam.cells.ToString());
                List<string> lstMessageError = Newtonsoft.Json.JsonConvert.DeserializeObject<List<string>>(dynParam.lstMessageError.ToString());

                var result = new Row();
                if (id > 0 && cells.Count > 0 && row > 0)
                {
                    ServiceFactory.SVCategory((ISVCategory sv) =>
                    {
                        result = sv.CATTypeOfDriverFee_ExcelChange(id, row, cells, lstMessageError);
                    });
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public SYSExcel CATTypeOfDriverFee_ExcelImport(dynamic dynParam)
        {
            try
            {
                var id = (long)dynParam.id;
                List<Worksheet> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Worksheet>>(dynParam.worksheets.ToString());
                List<string> lstMessageError = Newtonsoft.Json.JsonConvert.DeserializeObject<List<string>>(dynParam.lstMessageError.ToString());

                var result = default(SYSExcel);
                if (id > 0 && lst.Count > 0 && lst[0].Rows.Count > 1)
                {
                    ServiceFactory.SVCategory((ISVCategory sv) =>
                    {
                        result = sv.CATTypeOfDriverFee_ExcelImport(id, lst[0].Rows, lstMessageError);
                    });
                }
                if (result != null && !string.IsNullOrEmpty(result.Data))
                {
                    result.Worksheets = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Worksheet>>(result.Data);
                }
                else
                {
                    result = new SYSExcel();
                    result.Worksheets = new List<Worksheet>();
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public bool CATTypeOfDriverFee_ExcelApprove(dynamic dynParam)
        {
            try
            {
                var id = (long)dynParam.id;

                var result = false;
                if (id > 0)
                {
                    ServiceFactory.SVCategory((ISVCategory sv) =>
                    {
                        result = sv.CATTypeOfDriverFee_ExcelApprove(id);
                    });

                    //if (result != null && !string.IsNullOrEmpty(result.Data))
                    //{
                    //    result.Worksheets = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Worksheet>>(result.Data);
                    //}
                    //else
                    //{
                    //    result = new SYSExcel();
                    //    result.Worksheets = new List<Worksheet>();
                    //}
                    //result.Data = "";
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region DrivingLicence
        public DTOResult DrivingLicence_List(dynamic dynParam)
        {
            try
            {
                string request = dynParam.request.ToString();
                var result = default(DTOResult);
                ServiceFactory.SVCategory((IServices.ISVCategory sv) =>
                {
                    result = sv.DrivingLicence_List(request);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void DrivingLicence_Save(dynamic dynParam)
        {
            try
            {
                CATDrivingLicence item = Newtonsoft.Json.JsonConvert.DeserializeObject<CATDrivingLicence>(dynParam.item.ToString());
                if (item != null)
                {
                    ServiceFactory.SVCategory((ISVCategory sv) =>
                    {
                        sv.DrivingLicence_Save(item);
                    });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public CATDrivingLicence DrivingLicence_Get(dynamic dynParam)
        {
            try
            {
                int id = (int)dynParam.id;
                var result = default(CATDrivingLicence);
                ServiceFactory.SVCategory((ISVCategory sv) =>
                {
                    result = sv.DrivingLicence_Get(id);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void DrivingLicence_Delete(dynamic dynParam)
        {
            try
            {
                CATDrivingLicence item = Newtonsoft.Json.JsonConvert.DeserializeObject<CATDrivingLicence>(dynParam.item.ToString());
                if (item != null)
                {
                    ServiceFactory.SVCategory((ISVCategory sv) =>
                    {
                        sv.DrivingLicence_Delete(item);
                    });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public SYSExcel DrivingLicence_ExcelInit(dynamic dynParam)
        {
            try
            {
                var functionid = (int)dynParam.functionid;
                var functionkey = dynParam.functionkey.ToString();
                var isreload = (bool)dynParam.isreload;
                var result = default(SYSExcel);

                ServiceFactory.SVCategory((ISVCategory sv) =>
                {
                    result = sv.DrivingLicence_ExcelInit(functionid, functionkey, isreload);
                });
                if (result != null && !string.IsNullOrEmpty(result.Data))
                {
                    result.Worksheets = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Worksheet>>(result.Data);
                }
                else
                {
                    result = new SYSExcel();
                    result.Worksheets = new List<Worksheet>();
                }
                result.Data = "";
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public Row DrivingLicence_ExcelChange(dynamic dynParam)
        {
            try
            {
                var id = (long)dynParam.id;
                var row = (int)dynParam.row;
                List<Cell> cells = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Cell>>(dynParam.cells.ToString());
                List<string> lstMessageError = Newtonsoft.Json.JsonConvert.DeserializeObject<List<string>>(dynParam.lstMessageError.ToString());

                var result = new Row();
                if (id > 0 && cells.Count > 0 && row > 0)
                {
                    ServiceFactory.SVCategory((ISVCategory sv) =>
                    {
                        result = sv.DrivingLicence_ExcelChange(id, row, cells, lstMessageError);
                    });
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public SYSExcel DrivingLicence_ExcelImport(dynamic dynParam)
        {
            try
            {
                var id = (long)dynParam.id;
                List<Worksheet> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Worksheet>>(dynParam.worksheets.ToString());
                List<string> lstMessageError = Newtonsoft.Json.JsonConvert.DeserializeObject<List<string>>(dynParam.lstMessageError.ToString());

                var result = default(SYSExcel);
                if (id > 0 && lst.Count > 0 && lst[0].Rows.Count > 1)
                {
                    ServiceFactory.SVCategory((ISVCategory sv) =>
                    {
                        result = sv.DrivingLicence_ExcelImport(id, lst[0].Rows, lstMessageError);
                    });
                }
                if (result != null && !string.IsNullOrEmpty(result.Data))
                {
                    result.Worksheets = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Worksheet>>(result.Data);
                }
                else
                {
                    result = new SYSExcel();
                    result.Worksheets = new List<Worksheet>();
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public bool DrivingLicence_ExcelApprove(dynamic dynParam)
        {
            try
            {
                var id = (long)dynParam.id;

                var result = false;
                if (id > 0)
                {
                    ServiceFactory.SVCategory((ISVCategory sv) =>
                    {
                        result = sv.DrivingLicence_ExcelApprove(id);
                    });

                    //if (result != null && !string.IsNullOrEmpty(result.Data))
                    //{
                    //    result.Worksheets = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Worksheet>>(result.Data);
                    //}
                    //else
                    //{
                    //    result = new SYSExcel();
                    //    result.Worksheets = new List<Worksheet>();
                    //}
                    //result.Data = "";
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        #endregion

        #region CAT_ORDTypeOfDocField
        public DTOResult ORDTypeOfDoc_List(dynamic dynParam)
        {
            try
            {
                string request = dynParam.request.ToString();
                var result = default(DTOResult);
                ServiceFactory.SVCategory((IServices.ISVCategory sv) =>
                {
                    result = sv.ORDTypeOfDoc_List(request);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public int ORDTypeOfDoc_Save(dynamic dynParam)
        {
            try
            {
                DTOORDTypeOfDoc item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOORDTypeOfDoc>(dynParam.item.ToString());
                int result = -1;
                if (item != null)
                {
                    ServiceFactory.SVCategory((ISVCategory sv) =>
                    {
                        result = sv.ORDTypeOfDoc_Save(item);
                    });
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOORDTypeOfDoc ORDTypeOfDoc_Get(dynamic dynParam)
        {
            try
            {
                int id = (int)dynParam.ID;
                var result = default(DTOORDTypeOfDoc);
                ServiceFactory.SVCategory((ISVCategory sv) =>
                {
                    result = sv.ORDTypeOfDoc_Get(id);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void ORDTypeOfDoc_Delete(dynamic dynParam)
        {
            try
            {
                DTOORDTypeOfDoc item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOORDTypeOfDoc>(dynParam.item.ToString());
                if (item != null)
                {
                    ServiceFactory.SVCategory((ISVCategory sv) =>
                    {
                        sv.ORDTypeOfDoc_Delete(item);
                    });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region CAT_FLMTypeOfScheduleFee
        [HttpPost]
        public DTOResult FLMTypeOfScheduleFee_List(dynamic dynParam)
        {
            try
            {
                string request = dynParam.request.ToString();
                var result = default(DTOResult);
                ServiceFactory.SVCategory((IServices.ISVCategory sv) =>
                {
                    result = sv.FLMTypeOfScheduleFee_List(request);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public int FLMTypeOfScheduleFee_Save(dynamic dynParam)
        {
            try
            {
                DTOFLMTypeOfScheduleFee item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOFLMTypeOfScheduleFee>(dynParam.item.ToString());
                int result = -1;
                if (item != null)
                {
                    ServiceFactory.SVCategory((ISVCategory sv) =>
                    {
                        result = sv.FLMTypeOfScheduleFee_Save(item);
                    });
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOFLMTypeOfScheduleFee FLMTypeOfScheduleFee_Get(dynamic dynParam)
        {
            try
            {
                int id = (int)dynParam.ID;
                var result = default(DTOFLMTypeOfScheduleFee);
                ServiceFactory.SVCategory((ISVCategory sv) =>
                {
                    result = sv.FLMTypeOfScheduleFee_Get(id);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void FLMTypeOfScheduleFee_Delete(dynamic dynParam)
        {
            try
            {
                DTOFLMTypeOfScheduleFee item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOFLMTypeOfScheduleFee>(dynParam.item.ToString());
                if (item != null)
                {
                    ServiceFactory.SVCategory((ISVCategory sv) =>
                    {
                        sv.FLMTypeOfScheduleFee_Delete(item);
                    });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public SYSExcel FLMTypeOfScheduleFee_ExcelInit(dynamic dynParam)
        {
            try
            {
                var functionid = (int)dynParam.functionid;
                var functionkey = dynParam.functionkey.ToString();
                var isreload = (bool)dynParam.isreload;

                var result = default(SYSExcel);

                ServiceFactory.SVCategory((ISVCategory sv) =>
                {
                    result = sv.FLMTypeOfScheduleFee_ExcelInit(functionid, functionkey, isreload);
                });
                if (result != null && !string.IsNullOrEmpty(result.Data))
                {
                    result.Worksheets = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Worksheet>>(result.Data);
                }
                else
                {
                    result = new SYSExcel();
                    result.Worksheets = new List<Worksheet>();
                }
                result.Data = "";
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public Row FLMTypeOfScheduleFee_ExcelChange(dynamic dynParam)
        {
            try
            {
                var id = (long)dynParam.id;
                var row = (int)dynParam.row;
                List<Cell> cells = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Cell>>(dynParam.cells.ToString());
                List<string> lstMessageError = Newtonsoft.Json.JsonConvert.DeserializeObject<List<string>>(dynParam.lstMessageError.ToString());

                var result = new Row();
                if (id > 0 && cells.Count > 0 && row > 0)
                {
                    ServiceFactory.SVCategory((ISVCategory sv) =>
                    {
                        result = sv.FLMTypeOfScheduleFee_ExcelChange(id, row, cells, lstMessageError);
                    });
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public SYSExcel FLMTypeOfScheduleFee_ExcelImport(dynamic dynParam)
        {
            try
            {
                var id = (long)dynParam.id;
                List<Worksheet> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Worksheet>>(dynParam.worksheets.ToString());
                List<string> lstMessageError = Newtonsoft.Json.JsonConvert.DeserializeObject<List<string>>(dynParam.lstMessageError.ToString());

                var result = default(SYSExcel);
                if (id > 0 && lst.Count > 0 && lst[0].Rows.Count > 1)
                {
                    ServiceFactory.SVCategory((ISVCategory sv) =>
                    {
                        result = sv.FLMTypeOfScheduleFee_ExcelImport(id, lst[0].Rows, lstMessageError);
                    });
                }
                if (result != null && !string.IsNullOrEmpty(result.Data))
                {
                    result.Worksheets = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Worksheet>>(result.Data);
                }
                else
                {
                    result = new SYSExcel();
                    result.Worksheets = new List<Worksheet>();
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public bool FLMTypeOfScheduleFee_ExcelApprove(dynamic dynParam)
        {
            try
            {
                var id = (long)dynParam.id;

                var result = false;
                if (id > 0)
                {
                    ServiceFactory.SVCategory((ISVCategory sv) =>
                    {
                        result = sv.FLMTypeOfScheduleFee_ExcelApprove(id);
                    });

                    //if (result != null && !string.IsNullOrEmpty(result.Data))
                    //{
                    //    result.Worksheets = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Worksheet>>(result.Data);
                    //}
                    //else
                    //{
                    //    result = new SYSExcel();
                    //    result.Worksheets = new List<Worksheet>();
                    //}
                    //result.Data = "";
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region CATLocationMatrix_List

        [HttpPost]
        public DTOResult CATLocationMatrix_List(dynamic dynParam)
        {
            try
            {
                string request = dynParam.request.ToString();
                var result = default(DTOResult);
                ServiceFactory.SVCategory((IServices.ISVCategory sv) =>
                {
                    result = sv.CATLocationMatrix_List(request);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public CATLocationMatrix CATLocationMatrix_Get(dynamic dynParam)
        {
            try
            {
                int id = (int)dynParam.id;
                var result = default(CATLocationMatrix);
                ServiceFactory.SVCategory((IServices.ISVCategory sv) =>
                {
                    result = sv.CATLocationMatrix_Get(id);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void CATLocationMatrix_Delete(dynamic dynParam)
        {
            try
            {
                int id = (int)dynParam.id;
                ServiceFactory.SVCategory((IServices.ISVCategory sv) =>
                {
                    sv.CATLocationMatrix_Delete(id);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void CATLocationMatrix_Generate()
        {
            try
            {
                ServiceFactory.SVCategory((IServices.ISVCategory sv) =>
                {
                    sv.CATLocationMatrix_Generate();
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void CATLocationMatrix_GenerateByList(dynamic dynParam)
        {
            try
            {
                List<int> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.lst.ToString());
                ServiceFactory.SVCategory((IServices.ISVCategory sv) =>
                {
                    sv.CATLocationMatrix_GenerateByList(lst);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public void CATLocationMatrix_GenerateLimit(dynamic dynParam)
        {
            try
            {
                List<int> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.lst.ToString());
                ServiceFactory.SVCategory((IServices.ISVCategory sv) =>
                {
                    sv.CATLocationMatrix_GenerateLimit(lst);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void CATLocationMatrix_Save(dynamic dynParam)
        {
            try
            {
                CATLocationMatrix item = Newtonsoft.Json.JsonConvert.DeserializeObject<CATLocationMatrix>(dynParam.item.ToString());
                ServiceFactory.SVCategory((ISVCategory sv) =>
                {
                    sv.CATLocationMatrix_Save(item);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public void CATLocationMatrix_SaveList(dynamic dynParam)
        {
            try
            {
                List<CATLocationMatrix> item = Newtonsoft.Json.JsonConvert.DeserializeObject<List<CATLocationMatrix>>(dynParam.item.ToString());
                ServiceFactory.SVCategory((ISVCategory sv) =>
                {
                    sv.CATLocationMatrix_SaveList(item);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public void CATLocationMatrix_CreateFromOPS(dynamic dynParam)
        {
            try
            {
                bool isDI = Convert.ToBoolean(dynParam.isDI.ToString());
                ServiceFactory.SVCategory((IServices.ISVCategory sv) =>
                {
                    sv.CATLocationMatrix_CreateFromOPS(isDI);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region CATLocationMatrixDetail
        [HttpPost]
        public DTOResult CATLocationMatrixDetail_List(dynamic dynParam)
        {
            try
            {
                string request = dynParam.request.ToString();
                long locationMatrixID = (long)dynParam.locationMatrixID;

                var result = default(DTOResult);
                ServiceFactory.SVCategory((IServices.ISVCategory sv) =>
                {


                    result = sv.CATLocationMatrixDetail_List(request, locationMatrixID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public long CATLocationMatrixDetail_Save(dynamic dynParam)
        {
            try
            {
                CATLocationMatrixDetail item = Newtonsoft.Json.JsonConvert.DeserializeObject<CATLocationMatrixDetail>(dynParam.item.ToString());
                long result = -1;
                ServiceFactory.SVCategory((IServices.ISVCategory sv) =>
                {
                    result = sv.CATLocationMatrixDetail_Save(item);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public CATLocationMatrixDetail CATLocationMatrixDetail_Get(dynamic dynParam)
        {
            try
            {
                long id = (int)dynParam.id;
                CATLocationMatrixDetail result = new CATLocationMatrixDetail();
                ServiceFactory.SVCategory((IServices.ISVCategory sv) =>
                {
                    result = sv.CATLocationMatrixDetail_Get(id);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void CATLocationMatrixDetail_Detele(dynamic dynParam)
        {
            try
            {
                long id = (int)dynParam.id;
                ServiceFactory.SVCategory((IServices.ISVCategory sv) =>
                {
                    sv.CATLocationMatrixDetail_Detele(id);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #region CAT_LocationMatrixDetailStation
        [HttpPost]
        public DTOResult CATLocationMatrixDetailStation_List(dynamic dynParam)
        {
            try
            {
                string request = dynParam.request.ToString();
                long LocationMaxtrixDetailID = (long)dynParam.locationMaxtrixDetailID;

                var result = default(DTOResult);
                ServiceFactory.SVCategory((IServices.ISVCategory sv) =>
                {
                    result = sv.CATLocationMatrixDetailStation_List(request, LocationMaxtrixDetailID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void CATLocationMatrixDetailStationNotIn_SaveList(dynamic dynParam)
        {
            try
            {
                long LocationMaxtrixDetailID = (long)dynParam.LocationMaxtrixDetailID;
                List<int> lstStaionID = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.lst.ToString());
                ServiceFactory.SVCategory((IServices.ISVCategory sv) =>
                {
                    sv.CATLocationMatrixDetailStationNotIn_SaveList(lstStaionID, LocationMaxtrixDetailID);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void CATLocationMatrixDetailStation_SaveList(dynamic dynParam)
        {
            try
            {
                List<DTOCATLocationMatrixStation> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DTOCATLocationMatrixStation>>(dynParam.lst.ToString());
                ServiceFactory.SVCategory((IServices.ISVCategory sv) =>
                {
                    sv.CATLocationMatrixDetailStation_SaveList(lst);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void CATLocationMatrixDetailStation_DeleteList(dynamic dynParam)
        {
            try
            {
                List<int> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.lst.ToString());
                ServiceFactory.SVCategory((IServices.ISVCategory sv) =>
                {
                    sv.CATLocationMatrixDetailStation_DeleteList(lst);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOResult CATLocationMatrixDetailStation_LocationList(dynamic dynParam)
        {
            try
            {
                string request = dynParam.request.ToString();
                long LocationMaxtrixDetailID = (long)dynParam.locationMaxtrixDetailID;

                var result = default(DTOResult);
                ServiceFactory.SVCategory((IServices.ISVCategory sv) =>
                {
                    result = sv.CATLocationMatrixDetailStation_LocationList(request, LocationMaxtrixDetailID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #endregion

        #region excel mới
        [HttpPost]
        public SYSExcel CATLocationMatrix_ExcelInit(dynamic dynParam)
        {
            try
            {
                var functionid = (int)dynParam.functionid;
                var functionkey = dynParam.functionkey.ToString();
                var isreload = (bool)dynParam.isreload;

                var result = default(SYSExcel);

                ServiceFactory.SVCategory((ISVCategory sv) =>
                {
                    result = sv.CATLocationMatrix_ExcelInit(functionid, functionkey, isreload);
                });
                if (result != null && !string.IsNullOrEmpty(result.Data))
                {
                    result.Worksheets = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Worksheet>>(result.Data);
                }
                else
                {
                    result = new SYSExcel();
                    result.Worksheets = new List<Worksheet>();
                }
                result.Data = "";
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public Row CATLocationMatrix_ExcelChange(dynamic dynParam)
        {
            try
            {
                var id = (long)dynParam.id;
                var row = (int)dynParam.row;
                List<Cell> cells = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Cell>>(dynParam.cells.ToString());
                List<string> lstMessageError = Newtonsoft.Json.JsonConvert.DeserializeObject<List<string>>(dynParam.lstMessageError.ToString());

                var result = default(Row);
                if (id > 0 && cells.Count > 0 && row > 0 )
                {
                    ServiceFactory.SVCategory((ISVCategory sv) =>
                    {
                        result = sv.CATLocationMatrix_ExcelChange(id, row, cells, lstMessageError);
                    });
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public SYSExcel CATLocationMatrix_ExcelImport(dynamic dynParam)
        {
            try
            {
                var id = (long)dynParam.id;
                List<Worksheet> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Worksheet>>(dynParam.worksheets.ToString());
                List<string> lstMessageError = Newtonsoft.Json.JsonConvert.DeserializeObject<List<string>>(dynParam.lstMessageError.ToString());

                var result = default(SYSExcel);
                if (id > 0 && lst.Count > 0 && lst[0].Rows.Count > 1)
                {
                    ServiceFactory.SVCategory((ISVCategory sv) =>
                    {
                        result = sv.CATLocationMatrix_ExcelImport(id, lst[0].Rows, lstMessageError);
                    });
                }
                if (result != null && !string.IsNullOrEmpty(result.Data))
                {
                    result.Worksheets = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Worksheet>>(result.Data);
                }
                else
                {
                    result = new SYSExcel();
                    result.Worksheets = new List<Worksheet>();
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public bool CATLocationMatrix_ExcelApprove(dynamic dynParam)
        {
            try
            {
                var id = (long)dynParam.id;
                var result = false;

                if (id > 0)
                {
                    ServiceFactory.SVCategory((ISVCategory sv) =>
                    {
                        result = sv.CATLocationMatrix_ExcelApprove(id);
                    });
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #endregion

        #region StationPrice
        [HttpPost]
        public DTOResult CATStationPrice_List(dynamic dynamic)
        {
            try
            {
                string request = dynamic.request.ToString();
                var result = new DTOResult();
                ServiceFactory.SVCategory((IServices.ISVCategory sv) =>
                {
                    result = sv.CATStationPrice_List(request);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOCATStationPrice CATStationPrice_Get(dynamic dynamic)
        {
            try
            {
                int id = (int)dynamic.id;
                var result = new DTOCATStationPrice();
                ServiceFactory.SVCategory((IServices.ISVCategory sv) =>
                {
                    result = sv.CATStationPrice_Get(id);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void CATStationPrice_Save(dynamic dynamic)
        {
            try
            {
                DTOCATStationPrice item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOCATStationPrice>(dynamic.item.ToString());
                ServiceFactory.SVCategory((IServices.ISVCategory sv) =>
                {
                    sv.CATStationPrice_Save(item);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void CATStationPrice_Delete(dynamic dynamic)
        {
            try
            {
                DTOCATStationPrice item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOCATStationPrice>(dynamic.item.ToString());
                ServiceFactory.SVCategory((IServices.ISVCategory sv) =>
                {
                    sv.CATStationPrice_Delete(item);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public List<DTOCATLocation> CATStationPrice_LocationList()
        {
            try
            {
                var result = new List<DTOCATLocation>();
                ServiceFactory.SVCategory((IServices.ISVCategory sv) =>
                {
                    result = sv.CATStationPrice_LocationList();
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public List<DTOFLMVehicle> CATStationPrice_AssetList()
        {
            try
            {
                var result = new List<DTOFLMVehicle>();
                ServiceFactory.SVCategory((IServices.ISVCategory sv) =>
                {
                    result = sv.CATStationPrice_AssetList();
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region Excel
        public string CATStationPrice_Excel_Export()
        {
            try
            {
                List<DTOCATStationPriceImport> result = new List<DTOCATStationPriceImport>();
                ServiceFactory.SVCategory((IServices.ISVCategory sv) =>
                {
                    result = sv.CATStationPriceExport();
                });

                string file = "/" + FolderUpload.Export + "ExportCATStation_Price_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx";

                if (System.IO.File.Exists(HttpContext.Current.Server.MapPath(file)))
                    System.IO.File.Delete(HttpContext.Current.Server.MapPath(file));
                FileInfo exportfile = new FileInfo(HttpContext.Current.Server.MapPath(file));
                using (ExcelPackage package = new ExcelPackage(exportfile))
                {
                    // Sheet 1
                    ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Sheet 1");

                    int col = 1, row = 1, stt = 1;
                    #region header
                    worksheet.Cells[row, col].Value = "STT";
                    col++; worksheet.Cells[row, col].Value = "Mã trạm"; worksheet.Column(col).Width = 20;
                    col++; worksheet.Cells[row, col].Value = "Tên trạm"; worksheet.Column(col).Width = 20;
                    col++; worksheet.Cells[row, col].Value = "Địa chỉ"; worksheet.Column(col).Width = 20;
                    col++; worksheet.Cells[row, col].Value = "Trọng tải"; worksheet.Column(col).Width = 20;
                    col++; worksheet.Cells[row, col].Value = "Giá"; worksheet.Column(col).Width = 20;
                    col++; worksheet.Cells[row, col].Value = "Giá theo km"; worksheet.Column(col).Width = 20;
                    col++; worksheet.Cells[row, col].Value = "Ghi chú"; worksheet.Column(col).Width = 20;

                    ExcelHelper.CreateCellStyle(worksheet, row, 1, row, col, false, true, ExcelHelper.ColorGreen, ExcelHelper.ColorWhite, 0, "");


                    worksheet.Cells[row, 1, row, col].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    worksheet.Cells[row, 1, row, col].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    #endregion

                    #region data
                    col = 1;
                    row = 2;
                    if (result.Count > 0)
                    {
                        foreach (var station in result)
                        {
                            col = 1;
                            worksheet.Cells[row, col].Value = stt;
                            col++; worksheet.Cells[row, col].Value = station.LocationCode;
                            col++; worksheet.Cells[row, col].Value = station.LocationName;
                            col++; worksheet.Cells[row, col].Value = station.LocationAddress;
                            col++; worksheet.Cells[row, col].Value = station.Ton;
                            col++; worksheet.Cells[row, col].Value = station.Price;
                            col++; worksheet.Cells[row, col].Value = station.PriceKM;
                            ExcelHelper.CreateFormat(worksheet, row, col, ExcelHelper.FormatMoney);
                            col++; worksheet.Cells[row, col].Value = station.Note;
                            row++;
                            stt++;
                        }
                    }

                    #endregion
                    for (int i = 1; i <= worksheet.Dimension.End.Row; i++)
                    {
                        for (int j = 1; j <= worksheet.Dimension.End.Column; j++)
                        {
                            worksheet.Cells[i, j].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                        }
                    }
                    package.Save();
                }
                return file;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public List<DTOCATStationPriceImport> CATStationPrice_Excel_Check(dynamic dynParam)
        {
            try
            {
                CATFile file = Newtonsoft.Json.JsonConvert.DeserializeObject<CATFile>(dynParam.file.ToString());
                List<DTOCATStationPriceImport> result = new List<DTOCATStationPriceImport>();

                if (file != null && !string.IsNullOrEmpty(file.FilePath))
                {
                    DTOCATStationPriceData data = new DTOCATStationPriceData();

                    ServiceFactory.SVCategory((IServices.ISVCategory sv) =>
                    {
                        data = sv.CATStationPrice_Data();
                    });

                    using (System.IO.FileStream fs = new System.IO.FileStream(HttpContext.Current.Server.MapPath("/" + file.FilePath), System.IO.FileMode.Open, System.IO.FileAccess.Read))
                    {
                        using (var package = new ExcelPackage(fs))
                        {
                            ExcelWorksheet worksheet = ExcelHelper.GetWorksheetByIndex(package, 1);

                            int col = 2, row;
                            string LocationCode, LocationName, LocationAddress, Ton, Price, PriceKM, Note;
                            if (worksheet != null)
                            {
                                row = 2;
                                while (row <= worksheet.Dimension.End.Row)
                                {
                                    List<string> lstError = new List<string>();
                                    col = 2;
                                    LocationCode = ExcelHelper.GetValue(worksheet, row, col);
                                    col++; LocationName = ExcelHelper.GetValue(worksheet, row, col);
                                    col++; LocationAddress = ExcelHelper.GetValue(worksheet, row, col);
                                    col++; Ton = ExcelHelper.GetValue(worksheet, row, col);
                                    col++; Price = ExcelHelper.GetValue(worksheet, row, col);
                                    col++; PriceKM = ExcelHelper.GetValue(worksheet, row, col);
                                    col++; Note = ExcelHelper.GetValue(worksheet, row, col);

                                    DTOCATStationPriceImport obj = new DTOCATStationPriceImport();
                                    obj.ExcelRow = row;

                                    if (string.IsNullOrEmpty(LocationCode))
                                    {
                                        lstError.Add("[Mã trạm] không được trống");
                                    }
                                    else
                                    {
                                        var station = data.ListStation.FirstOrDefault(c => c.Code == LocationCode);
                                        if (station == null)
                                        {
                                            lstError.Add("[Mã trạm] không tồn tại");
                                        }
                                        else
                                        {
                                            obj.LocationID = station.ID;
                                            obj.LocationCode = LocationCode;
                                            obj.LocationName = station.Location;
                                        }
                                    }

                                    obj.LocationName = LocationName;
                                    obj.LocationAddress = LocationAddress;

                                    if (string.IsNullOrEmpty(Ton))
                                    {
                                        lstError.Add("[Trọng tải] không được trống");
                                    }
                                    else
                                    {
                                        try
                                        {
                                            obj.Ton = Convert.ToDouble(Ton);
                                        }
                                        catch
                                        {
                                            lstError.Add("Trọng tải [" + Ton + "]không chính xác");
                                        }
                                    }

                                    obj.ID = 0;

                                    if (!string.IsNullOrEmpty(LocationCode) && obj.Ton >= 0)
                                    {
                                        var check = data.ListStationPrice.FirstOrDefault(c => c.LocationCode == LocationCode && c.Ton == obj.Ton);
                                        if (check != null)
                                            obj.ID = check.ID;
                                    }


                                    if (string.IsNullOrEmpty(Price))
                                    {
                                        lstError.Add("[Giá] không được trống");
                                    }
                                    else
                                    {
                                        try
                                        {
                                            obj.Price = Convert.ToDecimal(Price);
                                        }
                                        catch
                                        {
                                            lstError.Add("Giá [" + Price + "]không chính xác");
                                        }
                                    }

                                    if (string.IsNullOrEmpty(PriceKM))
                                    {
                                        lstError.Add("[Giá theo km] không được trống");
                                    }
                                    else
                                    {
                                        try
                                        {
                                            obj.PriceKM = Convert.ToDecimal(PriceKM);
                                        }
                                        catch
                                        {
                                            lstError.Add("Giá theo km [" + PriceKM + "]không chính xác");
                                        }
                                    }

                                    if (string.IsNullOrEmpty(Note))
                                        obj.Note = string.Empty;
                                    else
                                        obj.Note = Note;

                                    obj.ExcelSuccess = true; obj.ExcelError = string.Empty;
                                    if (lstError.Count > 0)
                                    {
                                        obj.ExcelSuccess = false; obj.ExcelError = string.Join(" ,", lstError);
                                    }
                                    result.Add(obj);
                                    row++;
                                }
                            }
                        }
                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void CATStationPrice_Excel_Save(dynamic dynParam)
        {
            try
            {
                List<DTOCATStationPriceImport> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DTOCATStationPriceImport>>(dynParam.lst.ToString());
                ServiceFactory.SVCategory((IServices.ISVCategory sv) =>
                {
                    sv.CATStationPriceImport(lst);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #endregion

        #region StationMonth
        [HttpPost]
        public DTOResult CATStationMonth_List(dynamic dynamic)
        {
            try
            {
                string request = dynamic.request.ToString();
                var result = new DTOResult();
                ServiceFactory.SVCategory((IServices.ISVCategory sv) =>
                {
                    result = sv.CATStationMonth_List(request);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void CATStationMonth_Save(dynamic dynamic)
        {
            try
            {
                DTOCATStationMonth item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOCATStationMonth>(dynamic.item.ToString());
                ServiceFactory.SVCategory((IServices.ISVCategory sv) =>
                {
                    sv.CATStationMonth_Save(item);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void CATStationMonth_Delete(dynamic dynamic)
        {
            try
            {
                DTOCATStationMonth item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOCATStationMonth>(dynamic.item.ToString());
                ServiceFactory.SVCategory((IServices.ISVCategory sv) =>
                {
                    sv.CATStationMonth_Delete(item);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public List<DTOCATLocation> CATStationMonth_LocationList()
        {
            try
            {
                var result = new List<DTOCATLocation>();
                ServiceFactory.SVCategory((IServices.ISVCategory sv) =>
                {
                    result = sv.CATStationMonth_LocationList();
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public List<DTOFLMVehicle> CATStationMonth_AssetList()
        {
            try
            {
                var result = new List<DTOFLMVehicle>();
                ServiceFactory.SVCategory((IServices.ISVCategory sv) =>
                {
                    result = sv.CATStationMonth_AssetList();
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #region Excel
        public string CATStationMonth_Excel_Export()
        {
            try
            {
                List<DTOCATStationMonthExcel> result = new List<DTOCATStationMonthExcel>();
                ServiceFactory.SVCategory((IServices.ISVCategory sv) =>
                {
                    result = sv.CATStationMonthExport();
                });

                string file = "/" + FolderUpload.Export + "ExportCATStation_Month_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx";

                if (System.IO.File.Exists(HttpContext.Current.Server.MapPath(file)))
                    System.IO.File.Delete(HttpContext.Current.Server.MapPath(file));
                FileInfo exportfile = new FileInfo(HttpContext.Current.Server.MapPath(file));
                using (ExcelPackage package = new ExcelPackage(exportfile))
                {
                    // Sheet 1
                    ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Sheet 1");

                    int col = 1, row = 1, stt = 1;
                    #region header
                    worksheet.Cells[row, col].Value = "STT";
                    col++; worksheet.Cells[row, col].Value = "Mã trạm"; worksheet.Column(col).Width = 20;
                    col++; worksheet.Cells[row, col].Value = "Tên trạm"; worksheet.Column(col).Width = 20;
                    col++; worksheet.Cells[row, col].Value = "Số xe"; worksheet.Column(col).Width = 20;
                    col++; worksheet.Cells[row, col].Value = "Giá"; worksheet.Column(col).Width = 20;
                    col++; worksheet.Cells[row, col].Value = "Từ ngày"; worksheet.Column(col).Width = 20;
                    col++; worksheet.Cells[row, col].Value = "Đến ngày"; worksheet.Column(col).Width = 20;
                    col++; worksheet.Cells[row, col].Value = "Ghi chú"; worksheet.Column(col).Width = 20;

                    ExcelHelper.CreateCellStyle(worksheet, row, 1, row, col, false, true, ExcelHelper.ColorGreen, ExcelHelper.ColorWhite, 0, "");


                    worksheet.Cells[row, 1, row, col].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    worksheet.Cells[row, 1, row, col].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    #endregion

                    #region data
                    col = 1;
                    row = 2;
                    if (result.Count > 0)
                    {
                        foreach (var station in result)
                        {
                            col = 1;
                            worksheet.Cells[row, col].Value = stt;
                            col++; worksheet.Cells[row, col].Value = station.LocationCode;
                            col++; worksheet.Cells[row, col].Value = station.LocationName;
                            col++; worksheet.Cells[row, col].Value = station.AssetNo;
                            col++; worksheet.Cells[row, col].Value = station.Price;
                            ExcelHelper.CreateFormat(worksheet, row, col, ExcelHelper.FormatMoney);
                            col++; worksheet.Cells[row, col].Value = station.DateFrom;
                            ExcelHelper.CreateFormat(worksheet, row, col, ExcelHelper.FormatDDMMYYYY);
                            col++; worksheet.Cells[row, col].Value = station.DateTo;
                            ExcelHelper.CreateFormat(worksheet, row, col, ExcelHelper.FormatDDMMYYYY);
                            col++; worksheet.Cells[row, col].Value = station.Note;
                            row++;
                            stt++;
                        }
                    }

                    #endregion
                    for (int i = 1; i <= worksheet.Dimension.End.Row; i++)
                    {
                        for (int j = 1; j <= worksheet.Dimension.End.Column; j++)
                        {
                            worksheet.Cells[i, j].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                        }
                    }
                    package.Save();
                }
                return file;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public List<DTOCATStationMonthExcel> CATStationMonth_Excel_Check(dynamic dynParam)
        {
            try
            {
                CATFile file = Newtonsoft.Json.JsonConvert.DeserializeObject<CATFile>(dynParam.file.ToString());
                List<DTOCATStationMonthExcel> result = new List<DTOCATStationMonthExcel>();

                if (file != null && !string.IsNullOrEmpty(file.FilePath))
                {
                    DTOCATStationMonthData data = new DTOCATStationMonthData();

                    ServiceFactory.SVCategory((IServices.ISVCategory sv) =>
                    {
                        data = sv.CATStationMonth_Data();
                    });

                    using (System.IO.FileStream fs = new System.IO.FileStream(HttpContext.Current.Server.MapPath("/" + file.FilePath), System.IO.FileMode.Open, System.IO.FileAccess.Read))
                    {
                        using (var package = new ExcelPackage(fs))
                        {
                            ExcelWorksheet worksheet = ExcelHelper.GetWorksheetByIndex(package, 1);

                            int col = 2, row;
                            string Code, Name, AssetNo, Price, DateFrom, DateTo, Note;
                            if (worksheet != null)
                            {
                                row = 2;
                                while (row <= worksheet.Dimension.End.Row)
                                {
                                    List<string> lstError = new List<string>();
                                    DTOCATStationMonthExcel obj = new DTOCATStationMonthExcel();
                                    obj.ExcelRow = row;

                                    col = 2;
                                    Code = ExcelHelper.GetValue(worksheet, row, col);
                                    if (string.IsNullOrEmpty(Code))
                                    {
                                        lstError.Add("[Mã trạm] không được trống");
                                    }
                                    else
                                    {
                                        var station = data.ListStation.FirstOrDefault(c => c.Code == Code);
                                        if (station == null)
                                        {
                                            lstError.Add("[Mã trạm] không tồn tại");
                                        }
                                        else
                                        {
                                            obj.LocationID = station.ID;
                                            obj.LocationCode = Code;
                                            obj.LocationName = station.Location;
                                        }
                                    }

                                    col = 4;
                                    AssetNo = ExcelHelper.GetValue(worksheet, row, col);
                                    if (string.IsNullOrEmpty(AssetNo))
                                    {
                                        lstError.Add("[Số xe] không được trống");
                                    }
                                    else
                                    {
                                        var vehicle = data.ListVehicle.FirstOrDefault(c => c.RegNo == AssetNo);
                                        if (vehicle == null)
                                        {
                                            lstError.Add("[Số xe] không tồn tại");
                                        }
                                        else
                                        {
                                            obj.AssetNo = AssetNo;
                                            obj.AssetID = vehicle.ID;
                                        }
                                    }

                                    //if (obj.LocationID != null && obj.AssetID != null)
                                    //{
                                    //    var price = data.ListStationMonth.FirstOrDefault(c => c.LocationID == obj.LocationID && c.AssetID == obj.AssetID);
                                    //    if (price != null)
                                    //    {
                                    //        obj.ID = price.ID;
                                    //    }

                                    //}
                                    col++;
                                    Price = ExcelHelper.GetValue(worksheet, row, col);
                                    if (string.IsNullOrEmpty(Price))
                                    {
                                        lstError.Add("[Giá] không được trống");
                                    }
                                    else
                                    {
                                        try
                                        {
                                            obj.Price = Convert.ToDecimal(Price);
                                        }
                                        catch
                                        {
                                            lstError.Add("Giá [" + Price + "]không chính xác");
                                        }
                                    }

                                    col++;
                                    DateFrom = ExcelHelper.GetValue(worksheet, row, col);
                                    if (string.IsNullOrEmpty(DateFrom))
                                        lstError.Add("[Ngày bắt đầu] không được trống");
                                    else
                                    {
                                        try
                                        {
                                            obj.DateFrom = ExcelHelper.ValueToDate(DateFrom);
                                        }
                                        catch
                                        {
                                            try
                                            {
                                                obj.DateFrom = Convert.ToDateTime(DateFrom);
                                            }
                                            catch
                                            {
                                                lstError.Add("Ngày bắt đầu [" + DateFrom + "] không đúng định dạng");
                                            }
                                        }
                                    }

                                    col++;
                                    DateTo = ExcelHelper.GetValue(worksheet, row, col);
                                    if (string.IsNullOrEmpty(DateTo))
                                        lstError.Add("[Ngày kết thúc] không được trống");
                                    else
                                    {
                                        try
                                        {
                                            obj.DateTo = ExcelHelper.ValueToDate(DateTo);
                                        }
                                        catch
                                        {
                                            try
                                            {
                                                obj.DateTo = Convert.ToDateTime(DateTo);
                                            }
                                            catch
                                            {
                                                lstError.Add("Ngày kết thúc [" + DateTo + "] không đúng định dạng");
                                            }
                                        }
                                    }

                                    if (obj.DateTo != null && obj.DateFrom != null)
                                    {
                                        int rComp = DateTime.Compare(obj.DateFrom, obj.DateTo);
                                        if (rComp > 0)
                                        {
                                            lstError.Add("Ngày bắt đầu [" + DateFrom + "] lớn hơn ngày kết thúc [" + DateTo + "]");
                                        }
                                    }
                                    col++;
                                    Note = ExcelHelper.GetValue(worksheet, row, col);
                                    if (string.IsNullOrEmpty(Note))
                                        obj.Note = string.Empty;
                                    else
                                        obj.Note = Note;

                                    obj.ExcelSuccess = true; obj.ExcelError = string.Empty;
                                    if (lstError.Count > 0)
                                    {
                                        obj.ExcelSuccess = false; obj.ExcelError = string.Join(" ,", lstError);
                                    }
                                    result.Add(obj);
                                    row++;
                                }
                            }
                        }
                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void CATStationMonth_Excel_Save(dynamic dynParam)
        {
            try
            {
                List<DTOCATStationMonthExcel> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DTOCATStationMonthExcel>>(dynParam.lst.ToString());
                ServiceFactory.SVCategory((IServices.ISVCategory sv) =>
                {
                    sv.CATStationMonthImport(lst);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #endregion

        #region CATLocationMatrixStation
        [HttpPost]
        public DTOResult CATLocationMatrixStation_List(dynamic dynamic)
        {
            try
            {
                string request = dynamic.request.ToString();
                var result = new DTOResult();
                ServiceFactory.SVCategory((IServices.ISVCategory sv) =>
                {
                    result = sv.CATLocationMatrixStation_List(request);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOCATLocationMatrixStation CATLocationMatrixStation_Get(dynamic dynamic)
        {
            try
            {
                int id = (int)dynamic.id;
                var result = new DTOCATLocationMatrixStation();
                ServiceFactory.SVCategory((IServices.ISVCategory sv) =>
                {
                    result = sv.CATLocationMatrixStation_Get(id);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void CATLocationMatrixStation_Save(dynamic dynamic)
        {
            try
            {
                DTOCATLocationMatrixStation item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOCATLocationMatrixStation>(dynamic.item.ToString());
                ServiceFactory.SVCategory((IServices.ISVCategory sv) =>
                {
                    sv.CATLocationMatrixStation_Save(item);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void CATLocationMatrixStation_Delete(dynamic dynamic)
        {
            try
            {
                DTOCATLocationMatrixStation item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOCATLocationMatrixStation>(dynamic.item.ToString());
                ServiceFactory.SVCategory((IServices.ISVCategory sv) =>
                {
                    sv.CATLocationMatrixStation_Delete(item);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOResult CATLocationMatrixStation_LocationMatrixList(dynamic dynamic)
        {
            try
            {
                string request = dynamic.request.ToString();
                var result = new DTOResult();
                ServiceFactory.SVCategory((IServices.ISVCategory sv) =>
                {
                    result = sv.CATLocationMatrixStation_LocationMatrixList(request);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOResult CATLocationMatrixStation_LocationList(dynamic dynamic)
        {
            try
            {
                string request = dynamic.request.ToString();
                var result = new DTOResult();
                ServiceFactory.SVCategory((IServices.ISVCategory sv) =>
                {
                    result = sv.CATLocationMatrixStation_LocationList(request);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Station
        public List<CATPartner> CATStation_Data()
        {
            try
            {
                List<CATPartner> result = default(List<CATPartner>);
                ServiceFactory.SVCategory((IServices.ISVCategory sv) =>
                {
                    result = sv.CATStation_Data();
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public CATPartner CATStation_Get(dynamic d)
        {
            try
            {
                int id = d.id;
                var result = new CATPartner();
                ServiceFactory.SVCategory((IServices.ISVCategory sv) =>
                {
                    result = sv.CATStation_Get(id);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public int CATStation_Save(dynamic d)
        {
            try
            {
                CATPartner item = Newtonsoft.Json.JsonConvert.DeserializeObject<CATPartner>(d.item.ToString());
                int result = -1;
                ServiceFactory.SVCategory((IServices.ISVCategory sv) =>
                {
                    result = sv.CATStation_Save(item);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void CATStation_Delete(dynamic d)
        {
            try
            {
                CATPartner item = Newtonsoft.Json.JsonConvert.DeserializeObject<CATPartner>(d.item.ToString());
                ServiceFactory.SVCategory((IServices.ISVCategory sv) =>
                {
                    sv.CATStation_Delete(item);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string CATStation_Export_GetData()
        {
            try
            {
                List<DTOCATPartnerImport> result = new List<DTOCATPartnerImport>();
                ServiceFactory.SVCategory((IServices.ISVCategory sv) =>
                {
                    result = sv.Station_Export();
                });

                string file = "/" + FolderUpload.Export + "ExportStation_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx";

                if (System.IO.File.Exists(HttpContext.Current.Server.MapPath(file)))
                    System.IO.File.Delete(HttpContext.Current.Server.MapPath(file));
                FileInfo exportfile = new FileInfo(HttpContext.Current.Server.MapPath(file));
                using (ExcelPackage package = new ExcelPackage(exportfile))
                {
                    // Sheet 1
                    ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Sheet 1");

                    int col = 1, row = 1, sttlv1 = 1, sttlv2 = 1;
                    #region header
                    worksheet.Cells[row, col].Value = "STT";
                    col++; worksheet.Cells[row, col].Value = "Mã hệ thống"; worksheet.Column(col).Width = 20;
                    col++; worksheet.Cells[row, col].Value = "Mã địa chỉ"; worksheet.Column(col).Width = 20;
                    col++; worksheet.Cells[row, col].Value = "Tên trạm"; worksheet.Column(col).Width = 15;
                    col++; worksheet.Cells[row, col].Value = "Địa chỉ"; worksheet.Column(col).Width = 15;
                    col++; worksheet.Cells[row, col].Value = "Tỉnh thành"; worksheet.Column(col).Width = 15;
                    col++; worksheet.Cells[row, col].Value = "Quận/Huyện"; worksheet.Column(col).Width = 15;
                    col++; worksheet.Cells[row, col].Value = "SĐT"; worksheet.Column(col).Width = 15;
                    col++; worksheet.Cells[row, col].Value = "Fax"; worksheet.Column(col).Width = 15;
                    col++; worksheet.Cells[row, col].Value = "Email"; worksheet.Column(col).Width = 15;
                    col++; worksheet.Cells[row, col].Value = "Kinh độ"; worksheet.Column(col).Width = 15;
                    col++; worksheet.Cells[row, col].Value = "Vĩ dộ"; worksheet.Column(col).Width = 15;

                    ExcelHelper.CreateCellStyle(worksheet, row, 1, row, col, false, true, ExcelHelper.ColorGreen, ExcelHelper.ColorWhite, 0, "");

                    worksheet.Cells[row, 1, row, col].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    worksheet.Cells[row, 1, row, col].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    #endregion

                    #region data

                    col = 1;
                    row = 2;
                    foreach (var item in result)
                    {
                        col = 1;
                        worksheet.Cells[row, col].Value = sttlv1;
                        col++; worksheet.Cells[row, col].Value = item.Code;
                        col++; worksheet.Cells[row, col].Value = string.Empty;
                        col++; worksheet.Cells[row, col].Value = item.PartnerName;
                        col++; worksheet.Cells[row, col].Value = item.Address;
                        col++; worksheet.Cells[row, col].Value = item.ProvinceName;
                        col++; worksheet.Cells[row, col].Value = item.DistrictName;
                        col++; worksheet.Cells[row, col].Value = item.TelNo;
                        col++; worksheet.Cells[row, col].Value = item.Fax;
                        col++; worksheet.Cells[row, col].Value = item.Email;
                        col++; worksheet.Cells[row, col].Value = string.Empty;
                        col++; worksheet.Cells[row, col].Value = string.Empty;

                        worksheet.Row(row).OutlineLevel = 0;

                        ExcelHelper.CreateCellStyle(worksheet, row, 1, row, col, false, false, ExcelHelper.ColorYellow, "", 0, "");

                        sttlv2 = 1;
                        foreach (var detail in item.lstLocation)
                        {
                            col = 1;
                            row++;
                            worksheet.Cells[row, col].Value = sttlv2;
                            col++; worksheet.Cells[row, col].Value = detail.Code;
                            col++; worksheet.Cells[row, col].Value = detail.PartnerCode;
                            col++; worksheet.Cells[row, col].Value = detail.LocationName;
                            col++; worksheet.Cells[row, col].Value = detail.Address;
                            col++; worksheet.Cells[row, col].Value = detail.ProvinceName;
                            col++; worksheet.Cells[row, col].Value = detail.DistrictName;
                            col++; worksheet.Cells[row, col].Value = string.Empty;
                            col++; worksheet.Cells[row, col].Value = string.Empty;
                            col++; worksheet.Cells[row, col].Value = string.Empty;
                            col++; worksheet.Cells[row, col].Value = detail.Lat;
                            col++; worksheet.Cells[row, col].Value = detail.Lng;

                            worksheet.Row(row).OutlineLevel = 1;
                            sttlv2++;
                        }
                        sttlv1++;
                        row++;
                    }

                    #endregion
                    if (result.Count > 0)
                    {
                        ExcelHelper.CreateCellStyle(worksheet, 2, 2, worksheet.Dimension.End.Row, 2, false, false, ExcelHelper.ColorOrange, "", 0, "");
                    }

                    for (int i = 1; i <= worksheet.Dimension.End.Row; i++)
                    {
                        for (int j = 1; j <= worksheet.Dimension.End.Column; j++)
                        {
                            worksheet.Cells[i, j].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                        }
                    }
                    package.Save();
                }
                return file;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<DTOCATPartnerImport> CATStation_Excel_Check(dynamic dynPram)
        {
            try
            {
                CATFile file = Newtonsoft.Json.JsonConvert.DeserializeObject<CATFile>(dynPram.item.ToString());
                List<DTOCATPartnerImport> result = new List<DTOCATPartnerImport>();

                if (file != null && !string.IsNullOrEmpty(file.FilePath))
                {
                    List<DTOCATPartnerItem> listStation = new List<DTOCATPartnerItem>();
                    List<DTOCATLocationItem> listStationLocation = new List<DTOCATLocationItem>();
                    List<CATProvince> listProvince = new List<CATProvince>();
                    List<CATDistrict> listDistrict = new List<CATDistrict>();

                    ServiceFactory.SVCategory((IServices.ISVCategory sv) =>
                    {
                        listStation = sv.Station_AllList().Data.Cast<DTOCATPartnerItem>().ToList();
                        listStationLocation = sv.StationLocation_AllList().Data.Cast<DTOCATLocationItem>().ToList();
                        listProvince = sv.ExcelProvince_List();
                        listDistrict = sv.ExcelDistrict_List();
                    });

                    using (System.IO.FileStream fs = new System.IO.FileStream(HttpContext.Current.Server.MapPath("/" + file.FilePath), System.IO.FileMode.Open, System.IO.FileAccess.Read))
                    {
                        using (var package = new ExcelPackage(fs))
                        {
                            ExcelWorksheet worksheet = ExcelHelper.GetWorksheetByIndex(package, 1);

                            int col = 2, row;
                            string Code, PartnerCode, PartnerName, Address, ProvinceName, DistrictName, TellNo, Fax, Email, Lat, Lng;
                            if (worksheet != null)
                            {
                                row = 2;
                                while (row <= worksheet.Dimension.End.Row)
                                {
                                    List<string> lstError = new List<string>();

                                    #region read data
                                    col = 2;
                                    Code = ExcelHelper.GetValue(worksheet, row, col); Code = Code.Trim();
                                    col++; PartnerCode = ExcelHelper.GetValue(worksheet, row, col); PartnerCode = PartnerCode.Trim();
                                    col++; PartnerName = ExcelHelper.GetValue(worksheet, row, col); PartnerName = PartnerName.Trim();
                                    col++; Address = ExcelHelper.GetValue(worksheet, row, col); Address = Address.Trim();
                                    col++; ProvinceName = ExcelHelper.GetValue(worksheet, row, col); ProvinceName = ProvinceName.Trim();
                                    col++; DistrictName = ExcelHelper.GetValue(worksheet, row, col); DistrictName = DistrictName.Trim();
                                    col++; TellNo = ExcelHelper.GetValue(worksheet, row, col); TellNo = TellNo.Trim();
                                    col++; Fax = ExcelHelper.GetValue(worksheet, row, col); Fax = Fax.Trim();
                                    col++; Email = ExcelHelper.GetValue(worksheet, row, col); Email = Email.Trim();
                                    col++; Lat = ExcelHelper.GetValue(worksheet, row, col); Lat = Lat.Trim();
                                    col++; Lng = ExcelHelper.GetValue(worksheet, row, col); Lng = Lng.Trim();
                                    #endregion

                                    if (row == 2)
                                        if (!string.IsNullOrEmpty(PartnerCode))
                                            throw new Exception("Không tìm thấy Partner");

                                    if (string.IsNullOrEmpty(PartnerCode))// nut cha
                                    {
                                        DTOCATPartnerImport obj = new DTOCATPartnerImport();
                                        obj.lstLocation = new List<DTOCATLocationImport>();

                                        obj.ExcelRow = row;

                                        if (!string.IsNullOrEmpty(Code))
                                        {
                                            #region data parent
                                            var checkCode = listStation.FirstOrDefault(c => c.Code == Code);
                                            if (checkCode != null)
                                                obj.ID = checkCode.ID;
                                            else
                                                obj.ID = 0;
                                            var checkCodeOnFile = result.FirstOrDefault(c => c.Code == Code);
                                            if (checkCodeOnFile != null) lstError.Add("Mã trạm [" + Code + "] bị trùng");
                                            obj.Code = Code;
                                            obj.Address = Address;
                                            obj.PartnerName = PartnerName;
                                            obj.TelNo = TellNo;
                                            obj.Fax = Fax;
                                            obj.Email = Email;
                                            if (!string.IsNullOrEmpty(ProvinceName))
                                            {
                                                var checkProvince = listProvince.FirstOrDefault(c => c.ProvinceName == ProvinceName);
                                                if (checkProvince == null)
                                                    lstError.Add("[" + ProvinceName + "] không tồn tại trong hệ thống");
                                                else
                                                {
                                                    obj.ProvinceID = checkProvince.ID; obj.ProvinceName = checkProvince.ProvinceName;
                                                    obj.CountryID = checkProvince.CountryID; obj.CountryName = checkProvince.CountryName;
                                                }
                                            }
                                            else
                                            {
                                                obj.ProvinceID = null;
                                                obj.CountryID = null;
                                            }
                                            if (!string.IsNullOrEmpty(DistrictName))
                                            {
                                                var checkDistrict = listDistrict.FirstOrDefault(c => c.DistrictName == DistrictName);
                                                if (checkDistrict == null)
                                                    lstError.Add("[" + DistrictName + "] không tồn tại trong hệ thống");
                                                else
                                                    obj.DistrictID = checkDistrict.ID; obj.DistrictName = DistrictName;
                                            }
                                            else
                                                obj.DistrictID = null;
                                            #endregion
                                        }
                                        else//ko co code
                                        {
                                            obj.Code = string.Empty;
                                            obj.Address = Address;
                                            obj.PartnerName = PartnerName;
                                            obj.TelNo = TellNo;
                                            obj.Fax = Fax;
                                            obj.Email = Email;
                                            lstError.Add("Thiếu mã trạm");
                                        }

                                        obj.ExcelError = string.Empty; obj.ExcelSuccess = true;
                                        if (lstError.Count > 0)
                                        {
                                            obj.ExcelSuccess = false;
                                            obj.ExcelError = string.Join(", ", lstError);
                                        }
                                        result.Add(obj);
                                    }
                                    else
                                    {

                                        DTOCATLocationImport objLocation = new DTOCATLocationImport();
                                        var obj = result.LastOrDefault();
                                        if (string.IsNullOrEmpty(obj.Code))
                                        {
                                            obj.Code = Code;
                                            obj.Address = Address;
                                            obj.PartnerName = PartnerName;
                                            obj.TelNo = TellNo;
                                            obj.Fax = Fax;
                                            obj.Email = Email;
                                            lstError.Add("Không thể xác định cha");
                                        }
                                        else
                                        {
                                            #region data lstLocation

                                            var checkSysCodeLocation = listStationLocation.FirstOrDefault(c => c.Code == Code);
                                            if (obj.ID > 0)//parent da co
                                            {
                                                if (checkSysCodeLocation == null)//location moi
                                                {
                                                    objLocation.ID = 0;
                                                    objLocation.Code = Code;
                                                }
                                                else//location cu
                                                {
                                                    objLocation.ID = checkSysCodeLocation.ID;
                                                    objLocation.Code = Code;
                                                }
                                            }
                                            else// parent moi
                                            {
                                                if (checkSysCodeLocation == null)
                                                {
                                                    objLocation.ID = 0;
                                                    objLocation.Code = Code;
                                                }
                                                else//location cu
                                                {
                                                    objLocation.ID = checkSysCodeLocation.ID;
                                                    objLocation.Code = Code;
                                                }
                                            }

                                            var chkOnFile = obj.lstLocation.FirstOrDefault(c => c.Code == Code);
                                            if (chkOnFile != null) lstError.Add("Mã [" + Code + "] bị trùng");


                                            var checkCodeLocation = listStationLocation.FirstOrDefault(c => c.PartnerCode == PartnerCode);

                                            if (obj.ID > 0)//parent da co
                                            {
                                                if (checkCodeLocation == null)//location mo
                                                    objLocation.PartnerCode = PartnerCode;
                                                else//location cu
                                                {
                                                    if (checkCodeLocation.ID == objLocation.ID)
                                                        objLocation.PartnerCode = PartnerCode;
                                                    else lstError.Add("Mã địa chỉ [" + PartnerCode + "] Đã sử dụng");
                                                }
                                            }
                                            else// parent moi
                                            {
                                                if (checkCodeLocation == null)
                                                    objLocation.PartnerCode = PartnerCode;
                                                else lstError.Add("Mã địa chỉ [" + PartnerCode + "] Đã sử dụng");
                                            }

                                            //var chkPartnerCodeOnFile = result.Count(c => c.lstLocation.Any(d => d.PartnerCode == PartnerCode)) > 0;
                                            //if (chkPartnerCodeOnFile) lstError.Add("Mã địa chỉ 3 [" + PartnerCode + "] Đã bị trùng");

                                            objLocation.LocationName = PartnerName;
                                            objLocation.Address = Address;

                                            if (!string.IsNullOrEmpty(ProvinceName))
                                            {
                                                var checkProvinceLocation = listProvince.FirstOrDefault(c => c.ProvinceName == ProvinceName);
                                                if (checkProvinceLocation == null)
                                                    lstError.Add("[" + ProvinceName + "] không tồn tại trong hệ thống");
                                                else
                                                {
                                                    objLocation.ProvinceID = checkProvinceLocation.ID;
                                                    objLocation.ProvinceName = checkProvinceLocation.ProvinceName;
                                                    objLocation.CountryID = checkProvinceLocation.CountryID;
                                                    objLocation.CountryName = checkProvinceLocation.CountryName;
                                                }
                                            }
                                            else lstError.Add("[Tỉnh thành] không được trống");

                                            if (!string.IsNullOrEmpty(DistrictName))
                                            {
                                                var checkDistrictLocation = listDistrict.FirstOrDefault(c => c.DistrictName == DistrictName);
                                                if (checkDistrictLocation == null)
                                                    lstError.Add("[" + DistrictName + "] không tồn tại trong hệ thống");
                                                else
                                                {
                                                    objLocation.DistrictID = checkDistrictLocation.ID;
                                                    objLocation.DistrictName = checkDistrictLocation.DistrictName;
                                                }
                                            }
                                            else lstError.Add("[Quận huyện] không được trống");
                                            if (string.IsNullOrEmpty(Lat))
                                                objLocation.Lat = null;
                                            else
                                            {
                                                try
                                                {
                                                    objLocation.Lat = Convert.ToDouble(Lat);
                                                }
                                                catch
                                                {
                                                    lstError.Add("[Vĩ độ] không chính xác");
                                                }
                                            }

                                            if (string.IsNullOrEmpty(Lng))
                                                objLocation.Lng = null;
                                            else
                                            {
                                                try
                                                {
                                                    objLocation.Lng = Convert.ToDouble(Lng);
                                                }
                                                catch
                                                {
                                                    lstError.Add("[Kinh độ] không chính xác");
                                                }
                                            }

                                            #endregion
                                        }

                                        objLocation.IsSuccess = true; objLocation.Error = string.Empty;
                                        if (lstError.Count > 0) { objLocation.IsSuccess = false; objLocation.Error = string.Join(" ,", lstError); }

                                        if (!obj.ExcelSuccess) { objLocation.IsSuccess = false; }
                                        if (!objLocation.IsSuccess)
                                        {
                                            if (string.IsNullOrEmpty(obj.ExcelError)) obj.ExcelError = objLocation.Error;
                                            else obj.ExcelError = obj.ExcelError + ", " + objLocation.Error;
                                        }
                                        obj.lstLocation.Add(objLocation);
                                    }
                                    row++;
                                }
                            }
                        }
                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void CATStation_Excel_Save(dynamic dynPram)
        {
            try
            {
                List<DTOCATPartnerImport> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DTOCATPartnerImport>>(dynPram.lst.ToString());
                ServiceFactory.SVCategory((IServices.ISVCategory sv) =>
                {
                    sv.Station_Import(lst);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Location In Station
        public DTOResult LocationInStation_List(dynamic d)
        {
            try
            {
                string request = d.request.ToString();
                int partnerid = (int)d.partnerid;
                var result = default(DTOResult);
                ServiceFactory.SVCategory((IServices.ISVCategory sv) =>
                {
                    result = sv.LocationInStation_List(request, partnerid);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public bool LocationInStation_Save(dynamic d)
        {
            try
            {
                DTOCATLocationInPartner item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOCATLocationInPartner>(d.item.ToString());
                int partnerid = (int)d.partnerid;
                if (item != null)
                {
                    ServiceFactory.SVCategory((IServices.ISVCategory sv) =>
                    {
                        item = sv.LocationInStation_Save(item, partnerid);
                    });
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOCATLocationInPartner LocationInStation_Get(dynamic d)
        {
            try
            {
                DTOCATLocationInPartner item = new DTOCATLocationInPartner();
                int id = (int)d.id;
                if (item != null)
                {
                    ServiceFactory.SVCategory((IServices.ISVCategory sv) =>
                    {
                        item = sv.LocationInStation_Get(id);
                    });
                }
                return item;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public bool LocationInStation_Delete(dynamic d)
        {
            try
            {
                DTOCATLocationInPartner item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOCATLocationInPartner>(d.item.ToString());
                if (item != null && ModelState.IsValid)
                {
                    ServiceFactory.SVCategory((IServices.ISVCategory sv) =>
                    {
                        sv.LocationInStation_Delete(item);
                    });
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DTOResult LocationNotInStation_List(dynamic d)
        {
            try
            {
                string request = d.request.ToString();
                int partnerid = (int)d.partnerid;
                var result = default(DTOResult);
                ServiceFactory.SVCategory((IServices.ISVCategory sv) =>
                {
                    result = sv.LocationNotInStation_List(request, partnerid);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void LocationNotInStation_SaveList(dynamic d)
        {
            try
            {
                List<DTOCATLocationInPartner> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DTOCATLocationInPartner>>(d.lst.ToString());
                int partnerid = (int)d.partnerid;
                ServiceFactory.SVCategory((IServices.ISVCategory sv) =>
                {
                    sv.LocationNotInStation_SaveList(lst, partnerid);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region FLMSupplier
        public DTOResult FLMSupplier_List(dynamic dynParam)
        {
            try
            {
                string request = dynParam.request.ToString();
                var result = default(DTOResult);
                ServiceFactory.SVCategory((IServices.ISVCategory sv) =>
                {
                    result = sv.FLMSupplier_List(request);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public int FLMSupplier_Save(dynamic dynParam)
        {
            try
            {
                FLMSupplier item = Newtonsoft.Json.JsonConvert.DeserializeObject<FLMSupplier>(dynParam.item.ToString());
                int result = -1;
                if (item != null)
                {
                    ServiceFactory.SVCategory((ISVCategory sv) =>
                    {
                        result = sv.FLMSupplier_Save(item);
                    });
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public FLMSupplier FLMSupplier_Get(dynamic dynParam)
        {
            try
            {
                int id = (int)dynParam.id;
                var result = default(FLMSupplier);
                ServiceFactory.SVCategory((ISVCategory sv) =>
                {
                    result = sv.FLMSupplier_Get(id);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void FLMSupplier_Delete(dynamic dynParam)
        {
            try
            {
                FLMSupplier item = Newtonsoft.Json.JsonConvert.DeserializeObject<FLMSupplier>(dynParam.item.ToString());
                if (item != null)
                {
                    ServiceFactory.SVCategory((ISVCategory sv) =>
                    {
                        sv.FLMSupplier_Delete(item);
                    });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public SYSExcel FLMSupplier_ExcelInit(dynamic dynParam)
        {
            try
            {
                var functionid = (int)dynParam.functionid;
                var functionkey = dynParam.functionkey.ToString();
                var isreload = (bool)dynParam.isreload;
                var result = default(SYSExcel);

                ServiceFactory.SVCategory((ISVCategory sv) =>
                {
                    result = sv.FLMSupplier_ExcelInit(functionid, functionkey, isreload);
                });
                if (result != null && !string.IsNullOrEmpty(result.Data))
                {
                    result.Worksheets = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Worksheet>>(result.Data);
                }
                else
                {
                    result = new SYSExcel();
                    result.Worksheets = new List<Worksheet>();
                }
                result.Data = "";
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public Row FLMSupplier_ExcelChange(dynamic dynParam)
        {
            try
            {
                var id = (long)dynParam.id;
                var row = (int)dynParam.row;
                List<Cell> cells = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Cell>>(dynParam.cells.ToString());
                List<string> lstMessageError = Newtonsoft.Json.JsonConvert.DeserializeObject<List<string>>(dynParam.lstMessageError.ToString());
                var result = new Row();
                if (id > 0 && cells.Count > 0 && row > 0)
                {
                    ServiceFactory.SVCategory((ISVCategory sv) =>
                    {
                        result = sv.FLMSupplier_ExcelChange(id, row, cells, lstMessageError);
                    });
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public SYSExcel FLMSupplier_ExcelImport(dynamic dynParam)
        {
            try
            {
                var id = (long)dynParam.id;
                List<Worksheet> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Worksheet>>(dynParam.worksheets.ToString());
                List<string> lstMessageError = Newtonsoft.Json.JsonConvert.DeserializeObject<List<string>>(dynParam.lstMessageError.ToString());

                var result = default(SYSExcel);
                if (id > 0 && lst.Count > 0 && lst[0].Rows.Count > 1)
                {
                    ServiceFactory.SVCategory((ISVCategory sv) =>
                    {
                        result = sv.FLMSupplier_ExcelImport(id, lst[0].Rows, lstMessageError);
                    });
                }
                if (result != null && !string.IsNullOrEmpty(result.Data))
                {
                    result.Worksheets = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Worksheet>>(result.Data);
                }
                else
                {
                    result = new SYSExcel();
                    result.Worksheets = new List<Worksheet>();
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public bool FLMSupplier_ExcelApprove(dynamic dynParam)
        {
            try
            {
                var id = (long)dynParam.id;

                var result = false;
                if (id > 0)
                {
                    ServiceFactory.SVCategory((ISVCategory sv) =>
                    {
                        result = sv.FLMSupplier_ExcelApprove(id);
                    });

                    //if (result != null && !string.IsNullOrEmpty(result.Data))
                    //{
                    //    result.Worksheets = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Worksheet>>(result.Data);
                    //}
                    //else
                    //{
                    //    result = new SYSExcel();
                    //    result.Worksheets = new List<Worksheet>();
                    //}
                    //result.Data = "";
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region FLMMaterialPrice
        public DTOResult FLMMaterialPrice_List(dynamic dynParam)
        {
            try
            {
                string request = dynParam.request.ToString();
                int supplierID = (int)dynParam.supplierID;
                var result = default(DTOResult);
                ServiceFactory.SVCategory((IServices.ISVCategory sv) =>
                {
                    result = sv.FLMMaterialPrice_List(supplierID, request);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public int FLMMaterialPrice_Save(dynamic dynParam)
        {
            try
            {
                DTOFLMMaterialPrice item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOFLMMaterialPrice>(dynParam.item.ToString());
                int supplierID = (int)dynParam.supplierID;
                item.SupplierID = supplierID;
                int result = -1;
                if (item != null)
                {
                    ServiceFactory.SVCategory((ISVCategory sv) =>
                    {
                        result = sv.FLMMaterialPrice_Save(item);
                    });
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOFLMMaterialPrice FLMMaterialPrice_Get(dynamic dynParam)
        {
            try
            {
                int id = (int)dynParam.id;
                var result = default(DTOFLMMaterialPrice);
                ServiceFactory.SVCategory((ISVCategory sv) =>
                {
                    result = sv.FLMMaterialPrice_Get(id);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void FLMaterialPrice_Delete(dynamic dynParam)
        {
            try
            {
                DTOFLMMaterialPrice item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOFLMMaterialPrice>(dynParam.item.ToString());
                if (item != null)
                {
                    ServiceFactory.SVCategory((ISVCategory sv) =>
                    {
                        sv.FLMaterialPrice_Delete(item);
                    });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion


        #region CATRoutingArea
        public DTOResult CATRoutingArea_List(dynamic dynParam)
        {
            try
            {
                string request = dynParam.request.ToString();
                var result = default(DTOResult);
                ServiceFactory.SVCategory((IServices.ISVCategory sv) =>
                {
                    result = sv.CATRoutingArea_List(request);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public int CATRoutingArea_Save(dynamic dynParam)
        {
            try
            {
                CATRoutingArea item = Newtonsoft.Json.JsonConvert.DeserializeObject<CATRoutingArea>(dynParam.item.ToString());
                int result = -1;
                if (item != null)
                {
                    ServiceFactory.SVCategory((ISVCategory sv) =>
                    {
                        result = sv.CATRoutingArea_Save(item);
                    });
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public CATRoutingArea CATRoutingArea_Get(dynamic dynParam)
        {
            try
            {
                int id = (int)dynParam.ID;
                var result = default(CATRoutingArea);
                ServiceFactory.SVCategory((ISVCategory sv) =>
                {
                    result = sv.CATRoutingArea_Get(id);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void CATRoutingArea_Delete(dynamic dynParam)
        {
            try
            {
                CATRoutingArea item = Newtonsoft.Json.JsonConvert.DeserializeObject<CATRoutingArea>(dynParam.item.ToString());
                if (item != null)
                {
                    ServiceFactory.SVCategory((ISVCategory sv) =>
                    {
                        sv.CATRoutingArea_Delete(item);
                    });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DTOResult CATRoutingAreaDetail_List(dynamic dynParam)
        {
            try
            {
                string request = dynParam.request.ToString();
                int routingAreaID = dynParam.routingAreaID;
                var result = default(DTOResult);
                ServiceFactory.SVCategory((IServices.ISVCategory sv) =>
                {
                    result = sv.CATRoutingAreaDetail_List(request, routingAreaID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int CATRoutingAreaDetail_Save(dynamic dynParam)
        {
            try
            {
                DTOCATRoutingAreaDetail item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOCATRoutingAreaDetail>(dynParam.item.ToString());
                int id = (int)dynParam.ID;
                int result = -1;
                if (item != null)
                {
                    ServiceFactory.SVCategory((ISVCategory sv) =>
                    {
                        result = sv.CATRoutingAreaDetail_Save(item, id);
                    });
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DTOCATRoutingAreaDetail CATRoutingAreaDetail_Get(dynamic dynParam)
        {
            try
            {
                int id = (int)dynParam.id;
                var result = default(DTOCATRoutingAreaDetail);
                ServiceFactory.SVCategory((ISVCategory sv) =>
                {
                    result = sv.CATRoutingAreaDetail_Get(id);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void CATRoutingAreaDetail_Delete(dynamic dynParam)
        {
            try
            {
                DTOCATRoutingAreaDetail item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOCATRoutingAreaDetail>(dynParam.item.ToString());
                if (item != null)
                {
                    ServiceFactory.SVCategory((ISVCategory sv) =>
                    {
                        sv.CATRoutingAreaDetail_Delete(item);
                    });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DTOResult CATRoutingArea_Location_List(dynamic dynParam)
        {
            try
            {
                string request = dynParam.request.ToString();
                int routingAreaID = dynParam.routingAreaID;
                var result = default(DTOResult);
                ServiceFactory.SVCategory((IServices.ISVCategory sv) =>
                {
                    result = sv.CATRoutingArea_Location_List(request, routingAreaID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DTOResult CATRoutingArea_LocationNotIn_List(dynamic dynParam)
        {
            try
            {
                string request = dynParam.request.ToString();
                int routingAreaID = dynParam.routingAreaID;
                var result = default(DTOResult);
                ServiceFactory.SVCategory((IServices.ISVCategory sv) =>
                {
                    result = sv.CATRoutingArea_LocationNotIn_List(request, routingAreaID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void CATRoutingArea_LocationNotIn_Save(dynamic dynParam)
        {
            try
            {
                int routingAreaID = dynParam.routingAreaID;
                List<int> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.lst.ToString());
                ServiceFactory.SVCategory((IServices.ISVCategory sv) =>
                {
                    sv.CATRoutingArea_LocationNotIn_Save(routingAreaID, lst);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void CATRoutingArea_Location_Delete(dynamic dynParam)
        {
            try
            {
                List<int> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.lst.ToString());
                ServiceFactory.SVCategory((IServices.ISVCategory sv) =>
                {
                    sv.CATRoutingArea_Location_Delete(lst);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void CATRoutingAreaLocation_Refresh(dynamic dynParam)
        {
            try
            {
                int id = dynParam.id;
                ServiceFactory.SVCategory((IServices.ISVCategory sv) =>
                {
                    sv.CATRoutingAreaLocation_Refresh(id);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DTOResult CATRoutingArea_AreaNotIn_AreaList(dynamic dynParam)
        {
            try
            {
                string request = dynParam.request.ToString();
                int id = (int)dynParam.id;
                var result = default(DTOResult);
                ServiceFactory.SVCategory((IServices.ISVCategory sv) =>
                {
                    result = sv.CATRoutingArea_AreaNotIn_AreaList(request, id);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void CATRoutingArea_AreaLocation_Copy(dynamic dynParam)
        {
            try
            {
                int id = dynParam.id;
                List<int> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.lst.ToString());
                ServiceFactory.SVCategory((IServices.ISVCategory sv) =>
                {
                    sv.CATRoutingArea_AreaLocation_Copy(id, lst);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        [HttpPost]
        public SYSExcel CATRoutingArea_ExcelInit(dynamic dynParam)
        {
            try
            {
                var functionid = (int)dynParam.functionid;
                var functionkey = dynParam.functionkey.ToString();
                var isreload = (bool)dynParam.isreload;

                var result = default(SYSExcel);

                ServiceFactory.SVCategory((ISVCategory sv) =>
                {
                    result = sv.CATRoutingArea_ExcelInit(functionid, functionkey, isreload);
                });
                if (result != null && !string.IsNullOrEmpty(result.Data))
                {
                    result.Worksheets = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Worksheet>>(result.Data);
                }
                else
                {
                    result = new SYSExcel();
                    result.Worksheets = new List<Worksheet>();
                }
                result.Data = "";
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public Row CATRoutingArea_ExcelChange(dynamic dynParam)
        {
            try
            {
                var id = (long)dynParam.id;
                var row = (int)dynParam.row;
                List<Cell> cells = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Cell>>(dynParam.cells.ToString());
                List<string> lstMessageError = Newtonsoft.Json.JsonConvert.DeserializeObject<List<string>>(dynParam.lstMessageError.ToString());

                var result = default(Row);
                if (id > 0 && cells.Count > 0 && row > 0)
                {
                    ServiceFactory.SVCategory((ISVCategory sv) =>
                    {
                        result = sv.CATRoutingArea_ExcelChange(id, row, cells, lstMessageError);
                    });
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public SYSExcel CATRoutingArea_ExcelImport(dynamic dynParam)
        {
            try
            {
                var id = (long)dynParam.id;
                List<Worksheet> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Worksheet>>(dynParam.worksheets.ToString());
                List<string> lstMessageError = Newtonsoft.Json.JsonConvert.DeserializeObject<List<string>>(dynParam.lstMessageError.ToString());

                var result = default(SYSExcel);
                if (id > 0 && lst.Count > 0 && lst[0].Rows.Count > 1)
                {
                    ServiceFactory.SVCategory((ISVCategory sv) =>
                    {
                        result = sv.CATRoutingArea_ExcelImport(id, lst[0].Rows, lstMessageError);
                    });
                }
                if (result != null && !string.IsNullOrEmpty(result.Data))
                {
                    result.Worksheets = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Worksheet>>(result.Data);
                }
                else
                {
                    result = new SYSExcel();
                    result.Worksheets = new List<Worksheet>();
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public bool CATRoutingArea_ExcelApprove(dynamic dynParam)
        {
            try
            {
                var id = (long)dynParam.id;
                var result = false;

                if (id > 0)
                {
                    ServiceFactory.SVCategory((ISVCategory sv) =>
                    {
                        result = sv.CATRoutingArea_ExcelApprove(id);
                    });
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion


        #region DTOFLMTypeWarning

        public DTOResult FLMTypeWarning_List(dynamic dynParam)
        {
            try
            {
                string request = dynParam.request.ToString();
                var result = new DTOResult();
                ServiceFactory.SVCategory((ISVCategory sv) =>
                {
                    result = sv.FLMTypeWarning_List(request);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public int FLMTypeWarning_Save(dynamic dynParam)
        {
            try
            {
                DTOFLMTypeWarning item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOFLMTypeWarning>(dynParam.item.ToString());
                int result = -1;
                ServiceFactory.SVCategory((ISVCategory sv) =>
                {
                    result = sv.FLMTypeWarning_Save(item);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public DTOFLMTypeWarning FLMTypeWarning_Get(dynamic dynParam)
        {
            try
            {
                int id = (int)dynParam.ID;
                var result = default(DTOFLMTypeWarning);
                ServiceFactory.SVCategory((ISVCategory sv) =>
                {
                    result = sv.FLMTypeWarning_Get(id);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void FLMTypeWarning_Delete(dynamic dynParam)
        {
            try
            {
                DTOFLMTypeWarning item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOFLMTypeWarning>(dynParam.item.ToString());
                ServiceFactory.SVCategory((ISVCategory sv) =>
                {
                    sv.FLMTypeWarning_Delete(item);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        #endregion

        #region CATVessel

        public DTOResult CATVessel_List(dynamic dynParam)
        {
            try
            {
                string request = dynParam.request.ToString();
                var result = new DTOResult();
                ServiceFactory.SVCategory((ISVCategory sv) =>
                {
                    result = sv.CATVessel_List(request);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void CATVessel_Save(dynamic dynParam)
        {
            try
            {
                DTOCATVessel item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOCATVessel>(dynParam.item.ToString());
                ServiceFactory.SVCategory((ISVCategory sv) =>
                {
                    sv.CATVessel_Save(item);
                });                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOCATVessel CATVessel_Get(dynamic dynParam)
        {
            try
            {
                int id = (int)dynParam.ID;
                var result = default(DTOCATVessel);
                ServiceFactory.SVCategory((ISVCategory sv) =>
                {
                    result = sv.CATVessel_Get(id);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void CATVessel_Delete(dynamic dynParam)
        {
            try
            {
                DTOCATVessel item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOCATVessel>(dynParam.item.ToString());
                ServiceFactory.SVCategory((ISVCategory sv) =>
                {
                    sv.CATVessel_Delete(item);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public List<CATPartner> CboCATPartner_List()
        {
            try
            {
                var result = default(List<CATPartner>);
                ServiceFactory.SVCategory((ISVCategory sv) =>
                {
                    result = sv.CboCATPartner_List();
                });
                return result;
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region CATLocation Test
        [HttpPost]
        public List<DTOOPSDI_Map_Schedule_Event> CATLocationTest_Read(dynamic dynParam)
        {
            try
            {
                List<DTOOPSDI_Map_Schedule_Event> result = new List<DTOOPSDI_Map_Schedule_Event>();
                DateTime dtStart= Convert.ToDateTime(dynParam.dtStart.ToString());
                DateTime dtEnd= Convert.ToDateTime(dynParam.dtEnd.ToString());
                ServiceFactory.SVCategory((ISVCategory sv) =>
                {
                    result = sv.CATLocationTest_Read(dtStart, dtEnd);
                });

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public List<DTOOPSDI_Map_Schedule_Group> CATLocationTest_Resource(dynamic dynParam)
        {
            try
            {
                List<DTOOPSDI_Map_Schedule_Group> result = new List<DTOOPSDI_Map_Schedule_Group>();
                ServiceFactory.SVCategory((ISVCategory sv) =>
                {
                    result = sv.CATLocationTest_Resource();
                });

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}