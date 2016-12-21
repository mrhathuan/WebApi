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
using IServices;
using OfficeOpenXml;
using System.Globalization;
using System.IO;
using OfficeOpenXml.Style;
using ServicesExtend;
using System.Text;
using System.Text.RegularExpressions;
using Newtonsoft.Json.Linq;

namespace ClientWeb
{
    public class ORDController : BaseController
    {
        #region Common
        const int iLO = -(int)SYSVarType.ServiceOfOrderLocal;
        const int iLOEmpty = -(int)SYSVarType.ServiceOfOrderLocalEmpty;
        const int iLOLaden = -(int)SYSVarType.ServiceOfOrderLocalLaden;
        const int iIM = -(int)SYSVarType.ServiceOfOrderImport;
        const int iEx = -(int)SYSVarType.ServiceOfOrderExport;
        const int iFCL = -(int)SYSVarType.TransportModeFCL;
        const int iFTL = -(int)SYSVarType.TransportModeFTL;
        const int iLTL = -(int)SYSVarType.TransportModeLTL;

        [HttpPost]
        public DTOResult ORDOrder_CustomerList()
        {
            try
            {
                var result = new DTOResult();
                ServiceFactory.SVOrder((ISVOrder sv) =>
                {
                    result = sv.ORDOrder_CustomerList();
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOResult ORDOrder_VendorList()
        {
            try
            {
                var result = new DTOResult();
                ServiceFactory.SVOrder((ISVOrder sv) =>
                {
                    result = sv.ORDOrder_VendorList();
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOResult ORDOrder_Contract_List(dynamic dynParam)
        {
            try
            {
                int cusID = (int)Convert.ToInt64(dynParam.CustomerID);
                int serID = (int)Convert.ToInt64(dynParam.ServiceID);
                int transID = (int)Convert.ToInt64(dynParam.TransportID);
                bool isExpired = (bool)dynParam.IsExpired;
                var result = new DTOResult();
                ServiceFactory.SVOrder((ISVOrder sv) =>
                {
                    result = sv.ORDOrder_Contract_List(cusID, serID, transID, isExpired);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOResult ORDOrder_ContractTemp_List(dynamic dynParam)
        {
            try
            {
                int contractID = (int)Convert.ToInt64(dynParam.ContractID);
                var result = new DTOResult();
                ServiceFactory.SVOrder((ISVOrder sv) =>
                {
                    result = sv.ORDOrder_ContractTemp_List(contractID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOResult ORDOrder_GroupOfProduct_List(dynamic dynParam)
        {
            try
            {
                int cusID = (int)dynParam.CusID;
                var result = new DTOResult();
                ServiceFactory.SVOrder((ISVOrder sv) =>
                {
                    result = sv.ORDOrder_GroupOfProduct_List(cusID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOResult ORDOrder_Stock_List(dynamic dynParam)
        {
            try
            {
                int cusID = (int)dynParam.CusID;
                var result = new DTOResult();
                ServiceFactory.SVOrder((ISVOrder sv) =>
                {
                    result = sv.ORDOrder_Stock_List(cusID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOResult ORDOrder_CusLocation_List(dynamic dynParam)
        {
            try
            {
                int cusID = (int)dynParam.CusID;
                var result = new DTOResult();
                ServiceFactory.SVOrder((ISVOrder sv) =>
                {
                    result = sv.ORDOrder_CusLocation_List(cusID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public DTOResult ORDOrder_CusStock_List(dynamic dynParam)
        {
            try
            {
                int cusID = (int)dynParam.CusID;
                var result = new DTOResult();
                ServiceFactory.SVOrder((ISVOrder sv) =>
                {
                    result = sv.ORDOrder_CusStock_List(cusID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void ORDOrder_Delete(dynamic dynParam)
        {
            int id = (int)dynParam.id;
            ServiceFactory.SVOrder((ISVOrder sv) =>
            {
                sv.ORDOrder_Delete(id);
            });
        }

        [HttpPost]
        public DTOORDOrder ORDOrder_GetItem(dynamic dynParam)
        {
            try
            {
                int id = (int)dynParam.ID;
                int cusID = (int)dynParam.CustomerID;
                int serID = (int)dynParam.ServiceOfOrderID;
                int transID = (int)dynParam.TransportModeID;
                int? contractID = (int?)dynParam.ContractID;
                int? contractTermID = (int?)dynParam.TermID;
                if (contractID.HasValue && contractID < 1)
                    contractID = null;
                if (contractTermID.HasValue && contractTermID < 1)
                    contractTermID = null;

                var result = new DTOORDOrder();
                ServiceFactory.SVOrder((ISVOrder sv) =>
                {
                    result = sv.ORDOrder_GetItem(id, cusID, serID, transID, contractID, contractTermID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void ORDOrder_Contract_Change(dynamic dynParam)
        {
            int orderID = (int)dynParam.OrderID;
            int? contractID = (int?)dynParam.ContractID;
            ServiceFactory.SVOrder((ISVOrder sv) =>
            {
                sv.ORDOrder_Contract_Change(orderID, contractID);
            });
        }

        [HttpPost]
        public decimal ORDOrder_PriceGroupVehicle(dynamic dynParam)
        {
            int toID = (int)dynParam.ToID;
            int govID = (int)dynParam.GOVID;
            int fromID = (int)dynParam.FromID;
            int typeID = (int)dynParam.TypeID;
            int contractID = (int)dynParam.ContractID;

            decimal result = 0;
            ServiceFactory.SVOrder((ISVOrder sv) =>
            {
                result = sv.ORDOrder_PriceGroupVehicle(contractID, fromID, toID, govID, typeID);
            });
            return result;
        }

        [HttpPost]
        public decimal ORDOrder_PriceGroupProduct(dynamic dynParam)
        {
            int contractID = (int)dynParam.ContractID;
            int fromID = (int)dynParam.FromID;
            int toID = (int)dynParam.ToID;
            int gopID = (int)dynParam.GOPID;
            int typeID = (int)dynParam.TypeID;

            decimal result = 0;
            ServiceFactory.SVOrder((ISVOrder sv) =>
            {
                result = sv.ORDOrder_PriceGroupProduct(contractID, fromID, toID, gopID, typeID);
            });
            return result;
        }

        [HttpPost]
        public decimal ORDOrder_PriceContainer(dynamic dynParam)
        {
            int contractID = (int)dynParam.ContractID;
            int fromID = (int)dynParam.FromID;
            int toID = (int)dynParam.ToID;
            int conID = (int)dynParam.ConID;
            int typeID = (int)dynParam.TypeID;

            decimal result = 0;
            ServiceFactory.SVOrder((ISVOrder sv) =>
            {
                result = sv.ORDOrder_PriceContainer(contractID, fromID, toID, conID, typeID);
            });
            return result;
        }

        //o
        [HttpPost]
        public DTOResult ORDOrder_ContainerService_List(dynamic dynParam)
        {
            try
            {
                int? contractID = (int?)dynParam.ContractID;
                var result = new DTOResult();
                ServiceFactory.SVOrder((ISVOrder sv) =>
                {
                    result = sv.ORDOrder_ContainerService_List(contractID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //o
        [HttpPost]
        public DTOResult ORDOrder_CODefault_List(dynamic dynParam)
        {
            try
            {
                int contractID = (int)dynParam.ContractID;
                var result = new DTOResult();
                ServiceFactory.SVOrder((ISVOrder sv) =>
                {
                    result = sv.ORDOrder_ContractCODefault_List(contractID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOORDOrder ORDOrder_GetDate(dynamic dynParam)
        {
            try
            {
                DTOORDOrder item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOORDOrder>(dynParam.item.ToString());
                var result = new DTOORDOrder();
                ServiceFactory.SVOrder((ISVOrder sv) =>
                {
                    result = sv.ORDOrder_GetDate(item);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public List<DTOORDCATTransportMode> ORDOrder_TransportMode_List()
        {
            try
            {
                var result = new List<DTOORDCATTransportMode>();
                ServiceFactory.SVOrder((ISVOrder sv) =>
                {
                    result = sv.ORDOrder_TransportMode_List();
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Common Detail

        [HttpPost]
        public DTOORDTruckLocalData ORDOrder_TruckLocal_Data(dynamic dynParam)
        {
            try
            {
                int cusID = (int)dynParam.CustomerID;
                int termID = (int)dynParam.TermID;
                var result = default(DTOORDTruckLocalData);
                ServiceFactory.SVOrder((ISVOrder sv) =>
                {
                    result = sv.ORDOrder_TruckLocal_Data(cusID, termID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOORDIMEXData ORDOrder_IMEX_Data(dynamic dynParam)
        {
            try
            {
                int cusID = (int)dynParam.CustomerID;
                var result = default(DTOORDIMEXData);
                ServiceFactory.SVOrder((ISVOrder sv) =>
                {
                    result = sv.ORDOrder_IMEX_Data(cusID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public DTOORDLadenEmptyData ORDOrder_LadenEmpty_Data(dynamic dynParam)
        {
            try
            {
                int cusID = (int)dynParam.CusID;
                var result = default(DTOORDLadenEmptyData);
                ServiceFactory.SVOrder((ISVOrder sv) =>
                {
                    result = sv.ORDOrder_LadenEmpty_Data(cusID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region ORDOrder_DashBoard
        [HttpPost]
        public List<MAP_Vehicle> Dashboard_Truck_List(dynamic dynParam)
        {
            try
            {
                var result = new List<MAP_Vehicle>();
                ServiceFactory.SVSystem((ISVSystem sv) =>
                {
                    result = sv.Dashboard_Truck_List();
                });

                return result;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public List<MAP_Vehicle> Dashboard_Tractor_List(dynamic dynParam)
        {
            try
            {
                var result = new List<MAP_Vehicle>();
                ServiceFactory.SVSystem((ISVSystem sv) =>
                {
                    result = sv.Dashboard_Tractor_List();
                });

                return result;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public Dashboard_UserSetting Dashboard_UserSetting_Get(dynamic dynParam)
        {
            try
            {
                int functionID = Convert.ToInt32(dynParam.functionID.ToString());
                var result = new Dashboard_UserSetting();
                ServiceFactory.SVSystem((ISVSystem sv) =>
                {
                    result = sv.Dashboard_UserSetting_Get(functionID);
                });

                return result;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void Dashboard_UserSetting_Save(dynamic dynParam)
        {
            try
            {
                Dashboard_UserSetting item = Newtonsoft.Json.JsonConvert.DeserializeObject<Dashboard_UserSetting>(dynParam.item.ToString());
                ServiceFactory.SVSystem((ISVSystem sv) =>
                {
                    sv.Dashboard_UserSetting_Save(item);
                });
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public Chart_Summary Chart_Summary_Data(dynamic dynParam)
        {
            try
            {
                DateTime dtFrom = Convert.ToDateTime(dynParam.dtfrom.ToString());
                DateTime dtTo = Convert.ToDateTime(dynParam.dtto.ToString());
                int? customerid = null;
                try
                {
                    customerid = Convert.ToInt32(dynParam.customerid.ToString());
                }
                catch { }
                var result = new Chart_Summary();
                ServiceFactory.SVSystem((ISVSystem sv) =>
                {
                    result = sv.Chart_Summary_Data(dtFrom, dtTo, customerid);
                });

                return result;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public Chart_Customer_Order Chart_Customer_Order_Data(dynamic dynParam)
        {
            try
            {
                DateTime dtFrom = Convert.ToDateTime(dynParam.dtfrom.ToString());
                DateTime dtTo = Convert.ToDateTime(dynParam.dtto.ToString());
                int? customerID = null;
                try
                {
                    customerID = Convert.ToInt32(dynParam.customerid.ToString());
                }
                catch { }

                var result = new Chart_Customer_Order();
                ServiceFactory.SVSystem((ISVSystem sv) =>
                {
                    result = sv.Chart_Customer_Order_Data(dtFrom, dtTo, customerID);
                });

                return result;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public Chart_Customer_OPS Chart_Customer_OPS_Data(dynamic dynParam)
        {
            try
            {
                DateTime dtFrom = Convert.ToDateTime(dynParam.dtfrom.ToString());
                DateTime dtTo = Convert.ToDateTime(dynParam.dtto.ToString());
                int? customerID = null;
                try
                {
                    customerID = Convert.ToInt32(dynParam.customerid.ToString());
                }
                catch { } int statusID = Convert.ToInt32(dynParam.statusid.ToString());

                var result = new Chart_Customer_OPS();
                ServiceFactory.SVSystem((ISVSystem sv) =>
                {
                    result = sv.Chart_Customer_OPS_Data(dtFrom, dtTo, customerID, statusID);
                });

                return result;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public Chart_Customer_Order_Rate Chart_Customer_Order_Rate_Data(dynamic dynParam)
        {
            try
            {
                DateTime dtFrom = Convert.ToDateTime(dynParam.dtfrom.ToString());
                DateTime dtTo = Convert.ToDateTime(dynParam.dtto.ToString());
                int quantity = Convert.ToInt32(dynParam.quantity.ToString());

                var result = new Chart_Customer_Order_Rate();
                ServiceFactory.SVSystem((ISVSystem sv) =>
                {
                    result = sv.Chart_Customer_Order_Rate_Data(dtFrom, dtTo, quantity);
                });

                return result;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public Chart_Owner_Capacity Chart_Owner_Capacity_Data(dynamic dynParam)
        {
            try
            {
                DateTime dtFrom = Convert.ToDateTime(dynParam.dtfrom.ToString());
                DateTime dtTo = Convert.ToDateTime(dynParam.dtto.ToString());

                var result = new Chart_Owner_Capacity();
                ServiceFactory.SVSystem((ISVSystem sv) =>
                {
                    result = sv.Chart_Owner_Capacity_Data(dtFrom, dtTo);
                });

                return result;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public Chart_Owner_KM Chart_Owner_KM_Data(dynamic dynParam)
        {
            try
            {
                DateTime dtFrom = Convert.ToDateTime(dynParam.dtfrom.ToString());
                DateTime dtTo = Convert.ToDateTime(dynParam.dtto.ToString());

                var result = new Chart_Owner_KM();
                ServiceFactory.SVSystem((ISVSystem sv) =>
                {
                    result = sv.Chart_Owner_KM_Data(dtFrom, dtTo);
                });

                return result;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public Chart_Owner_Operation Chart_Owner_Operation_Data(dynamic dynParam)
        {
            try
            {
                DateTime dtFrom = Convert.ToDateTime(dynParam.dtfrom.ToString());
                DateTime dtTo = Convert.ToDateTime(dynParam.dtto.ToString());

                var result = new Chart_Owner_Operation();
                ServiceFactory.SVSystem((ISVSystem sv) =>
                {
                    result = sv.Chart_Owner_Operation_Data(dtFrom, dtTo);
                });

                return result;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public Chart_Owner_CostRate Chart_Owner_CostRate_Data(dynamic dynParam)
        {
            try
            {
                DateTime dtFrom = Convert.ToDateTime(dynParam.dtfrom.ToString());
                DateTime dtTo = Convert.ToDateTime(dynParam.dtto.ToString());

                var result = new Chart_Owner_CostRate();
                ServiceFactory.SVSystem((ISVSystem sv) =>
                {
                    result = sv.Chart_Owner_CostRate_Data(dtFrom, dtTo);
                });

                return result;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public Chart_Owner_CostChange Chart_Owner_CostChange_Data(dynamic dynParam)
        {
            try
            {
                DateTime dtFrom = Convert.ToDateTime(dynParam.dtfrom.ToString());
                DateTime dtTo = Convert.ToDateTime(dynParam.dtto.ToString());

                var result = new Chart_Owner_CostChange();
                ServiceFactory.SVSystem((ISVSystem sv) =>
                {
                    result = sv.Chart_Owner_CostChange_Data(dtFrom, dtTo);
                });

                return result;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public Chart_Owner_OnTime Chart_Owner_OnTime_PickUp_Data(dynamic dynParam)
        {
            try
            {
                DateTime dtFrom = Convert.ToDateTime(dynParam.dtfrom.ToString());
                DateTime dtTo = Convert.ToDateTime(dynParam.dtto.ToString());
                int? customerID = null;
                try
                {
                    customerID = Convert.ToInt32(dynParam.customerid.ToString());
                }
                catch { }

                var result = new Chart_Owner_OnTime();
                ServiceFactory.SVSystem((ISVSystem sv) =>
                {
                    result = sv.Chart_Owner_OnTime_PickUp_Data(dtFrom, dtTo, customerID);
                });

                return result;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public Chart_Owner_OnTime Chart_Owner_OnTime_Delivery_Data(dynamic dynParam)
        {
            try
            {
                DateTime dtFrom = Convert.ToDateTime(dynParam.dtfrom.ToString());
                DateTime dtTo = Convert.ToDateTime(dynParam.dtto.ToString());
                int? customerID = null;
                try
                {
                    customerID = Convert.ToInt32(dynParam.customerid.ToString());
                }
                catch { }

                var result = new Chart_Owner_OnTime();
                ServiceFactory.SVSystem((ISVSystem sv) =>
                {
                    result = sv.Chart_Owner_OnTime_Delivery_Data(dtFrom, dtTo, customerID);
                });

                return result;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public Chart_Owner_OnTime Chart_Owner_OnTime_POD_Data(dynamic dynParam)
        {
            try
            {
                DateTime dtFrom = Convert.ToDateTime(dynParam.dtfrom.ToString());
                DateTime dtTo = Convert.ToDateTime(dynParam.dtto.ToString());
                int? customerID = null;
                try
                {
                    customerID = Convert.ToInt32(dynParam.customerid.ToString());
                }
                catch { }

                var result = new Chart_Owner_OnTime();
                ServiceFactory.SVSystem((ISVSystem sv) =>
                {
                    result = sv.Chart_Owner_OnTime_POD_Data(dtFrom, dtTo, customerID);
                });

                return result;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public Chart_Owner_Return Chart_Owner_Return_Data(dynamic dynParam)
        {
            try
            {
                DateTime dtFrom = Convert.ToDateTime(dynParam.dtfrom.ToString());
                DateTime dtTo = Convert.ToDateTime(dynParam.dtto.ToString());
                int? customerID = null;
                try
                {
                    customerID = Convert.ToInt32(dynParam.customerid.ToString());
                }
                catch { }

                var result = new Chart_Owner_Return();
                ServiceFactory.SVSystem((ISVSystem sv) =>
                {
                    result = sv.Chart_Owner_Return_Data(dtFrom, dtTo, customerID);
                });

                return result;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public Chart_Owner_Profit Chart_Owner_Profit_Data(dynamic dynParam)
        {
            try
            {
                DateTime dtFrom = Convert.ToDateTime(dynParam.dtfrom.ToString());
                DateTime dtTo = Convert.ToDateTime(dynParam.dtto.ToString());
                int? customerID = null;
                try
                {
                    customerID = Convert.ToInt32(dynParam.customerid.ToString());
                }
                catch { }

                var result = new Chart_Owner_Profit();
                ServiceFactory.SVSystem((ISVSystem sv) =>
                {
                    result = sv.Chart_Owner_Profit_Data(dtFrom, dtTo, customerID);
                });

                return result;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public Chart_Owner_Profit_Customer Chart_Owner_Profit_Customer_Data(dynamic dynParam)
        {
            try
            {
                DateTime dtFrom = Convert.ToDateTime(dynParam.dtfrom.ToString());
                DateTime dtTo = Convert.ToDateTime(dynParam.dtto.ToString());
                int? customerid = null;
                try
                {
                    customerid = Convert.ToInt32(dynParam.customerid.ToString());
                }
                catch { }
                var result = new Chart_Owner_Profit_Customer();
                ServiceFactory.SVSystem((ISVSystem sv) =>
                {
                    result = sv.Chart_Owner_Profit_Customer_Data(dtFrom, dtTo, customerid);
                });

                return result;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public Chart_Owner_OnTime Chart_Owner_OnTime_PickUp_Radial_Data(dynamic dynParam)
        {
            try
            {
                DateTime dtFrom = Convert.ToDateTime(dynParam.dtfrom.ToString());
                DateTime dtTo = Convert.ToDateTime(dynParam.dtto.ToString());
                int? customerID = null;
                try
                {
                    customerID = Convert.ToInt32(dynParam.customerid.ToString());
                }
                catch { }

                var result = new Chart_Owner_OnTime();
                ServiceFactory.SVSystem((ISVSystem sv) =>
                {
                    result = sv.Chart_Owner_OnTime_PickUp_Radial_Data(dtFrom, dtTo, customerID);
                });

                return result;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public Chart_Owner_OnTime Chart_Owner_OnTime_Delivery_Radial_Data(dynamic dynParam)
        {
            try
            {
                DateTime dtFrom = Convert.ToDateTime(dynParam.dtfrom.ToString());
                DateTime dtTo = Convert.ToDateTime(dynParam.dtto.ToString());
                int? customerID = null;
                try
                {
                    customerID = Convert.ToInt32(dynParam.customerid.ToString());
                }
                catch { }

                var result = new Chart_Owner_OnTime();
                ServiceFactory.SVSystem((ISVSystem sv) =>
                {
                    result = sv.Chart_Owner_OnTime_Delivery_Radial_Data(dtFrom, dtTo, customerID);
                });

                return result;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public Chart_Owner_OnTime Chart_Owner_OnTime_POD_Radial_Data(dynamic dynParam)
        {
            try
            {
                DateTime dtFrom = Convert.ToDateTime(dynParam.dtfrom.ToString());
                DateTime dtTo = Convert.ToDateTime(dynParam.dtto.ToString());
                int? customerID = null;
                try
                {
                    customerID = Convert.ToInt32(dynParam.customerid.ToString());
                }
                catch { }

                var result = new Chart_Owner_OnTime();
                ServiceFactory.SVSystem((ISVSystem sv) =>
                {
                    result = sv.Chart_Owner_OnTime_POD_Radial_Data(dtFrom, dtTo, customerID);
                });

                return result;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public Chart_Owner_Profit_Customer Chart_Owner_Profit_Vendor_Data(dynamic dynParam)
        {
            try
            {
                DateTime dtFrom = Convert.ToDateTime(dynParam.dtfrom.ToString());
                DateTime dtTo = Convert.ToDateTime(dynParam.dtto.ToString());

                var result = new Chart_Owner_Profit_Customer();
                ServiceFactory.SVSystem((ISVSystem sv) =>
                {
                    result = sv.Chart_Owner_Profit_Vendor_Data(dtFrom, dtTo);
                });

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region excel Dashboard

        //1
        [HttpPost]
        public string Summary_ExcelExport(dynamic dynParam)
        {
            try
            {
                var result = string.Empty;
                DateTime dtFrom = Convert.ToDateTime(dynParam.dtfrom.ToString());
                DateTime dtTo = Convert.ToDateTime(dynParam.dtto.ToString());
                int? customerid = null;
                try
                {
                    customerid = Convert.ToInt32(dynParam.customerid.ToString());
                }
                catch { }
                var lst = new Chart_Summary();
                ServiceFactory.SVSystem((ISVSystem sv) =>
                {
                    lst = sv.Chart_Summary_Data(dtFrom, dtTo, customerid);
                });
                string filepath = "/" + FolderUpload.Export + "exportSummary" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx";
                if (System.IO.File.Exists(HttpContext.Current.Server.MapPath(filepath)))
                    System.IO.File.Delete(HttpContext.Current.Server.MapPath(filepath));
                FileInfo exportfile = new FileInfo(HttpContext.Current.Server.MapPath(filepath));
                using (ExcelPackage package = new ExcelPackage(exportfile))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Sheet 1");

                    int col = 0;
                    int row = 0;
                    row = 1;
                    col = 1;
                    worksheet.Cells[row, col].Value = "STT";
                    col++; worksheet.Cells[row, col].Value = "Ngày"; worksheet.Column(col).Width = 20;
                    col++; worksheet.Cells[row, col].Value = "LTL"; worksheet.Column(col).Width = 20;
                    col++; worksheet.Cells[row, col].Value = "FTL"; worksheet.Column(col).Width = 20;
                    col++; worksheet.Cells[row, col].Value = "FCL"; worksheet.Column(col).Width = 20;
                    col++; worksheet.Cells[row, col].Value = "LCL"; worksheet.Column(col).Width = 20;
                    for (int i = 1; i <= col; i++)
                        ExcelHelper.CreateCellStyle(worksheet, row, i, row, i, true, true, ExcelHelper.ColorGreen, ExcelHelper.ColorWhite, 0, "");
                    row++;
                    int stt = 1;
                    foreach (var item in lst.ListData)
                    {
                        col = 1;
                        worksheet.Cells[row, col].Value = stt;
                        col++; worksheet.Cells[row, col].Value = item.Date;
                        ExcelHelper.CreateFormat(worksheet, row, col, ExcelHelper.FormatDDMMYYYY);
                        col++; worksheet.Cells[row, col].Value = item.TotalLTL;
                        col++; worksheet.Cells[row, col].Value = item.TotalFTL;
                        col++; worksheet.Cells[row, col].Value = item.TotalFCL;
                        col++; worksheet.Cells[row, col].Value = item.TotalLCL;
                        stt++;
                        row++;
                    }

                    //trungbinh
                    col = 1;
                    col++; worksheet.Cells[row, col].Value = "Trung bình";
                    col++; worksheet.Cells[row, col].Value = lst.ListData.Where(c => c.TotalLTL > 0).Sum(c => c.TotalLTL) / (double)lst.ListData.Count;
                    col++; worksheet.Cells[row, col].Value = lst.ListData.Where(c => c.TotalFTL > 0).Sum(c => c.TotalFTL) / (double)lst.ListData.Count;
                    col++; worksheet.Cells[row, col].Value = lst.ListData.Where(c => c.TotalFCL > 0).Sum(c => c.TotalFCL) / (double)lst.ListData.Count;
                    col++; worksheet.Cells[row, col].Value = lst.ListData.Where(c => c.TotalLCL > 0).Sum(c => c.TotalLCL) / (double)lst.ListData.Count;

                    for (int i = 1; i <= worksheet.Dimension.End.Row; i++)
                    {
                        for (int j = 1; j <= worksheet.Dimension.End.Column; j++)
                        {
                            worksheet.Cells[i, j].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                        }
                    }
                    package.Save();
                }
                result = filepath;
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //3
        [HttpPost]
        public string Customer_OPS_ExcelExport(dynamic dynParam)
        {
            try
            {
                var result = string.Empty;
                DateTime dtFrom = Convert.ToDateTime(dynParam.dtfrom.ToString());
                DateTime dtTo = Convert.ToDateTime(dynParam.dtto.ToString());
                int? customerID = null;
                try
                {
                    customerID = Convert.ToInt32(dynParam.customerid.ToString());
                }
                catch { } int statusID = Convert.ToInt32(dynParam.statusid.ToString());

                var lst = new Chart_Customer_OPS();
                ServiceFactory.SVSystem((ISVSystem sv) =>
                {
                    lst = sv.Chart_Customer_OPS_Data(dtFrom, dtTo, customerID, statusID);
                });

                string filepath = "/" + FolderUpload.Export + "exportCustomerOPS" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx";
                if (System.IO.File.Exists(HttpContext.Current.Server.MapPath(filepath)))
                    System.IO.File.Delete(HttpContext.Current.Server.MapPath(filepath));
                FileInfo exportfile = new FileInfo(HttpContext.Current.Server.MapPath(filepath));
                using (ExcelPackage package = new ExcelPackage(exportfile))
                {
                    ExcelWorksheet worksheetLTL = package.Workbook.Worksheets.Add("LTL");

                    int col = 0;
                    int row = 0;
                    row = 1;
                    col = 1;
                    worksheetLTL.Cells[row, col].Value = "STT";
                    col++; worksheetLTL.Cells[row, col].Value = "Ngày"; worksheetLTL.Column(col).Width = 20;
                    col++; worksheetLTL.Cells[row, col].Value = "Tấn"; worksheetLTL.Column(col).Width = 20;
                    col++; worksheetLTL.Cells[row, col].Value = "Khối"; worksheetLTL.Column(col).Width = 20;
                    for (int i = 1; i <= col; i++)
                        ExcelHelper.CreateCellStyle(worksheetLTL, row, i, row, i, true, true, ExcelHelper.ColorGreen, ExcelHelper.ColorWhite, 0, "");
                    row++;
                    int stt = 1;
                    foreach (var item in lst.ListLTL)
                    {
                        col = 1;
                        worksheetLTL.Cells[row, col].Value = stt;
                        col++; worksheetLTL.Cells[row, col].Value = item.Date;
                        ExcelHelper.CreateFormat(worksheetLTL, row, col, ExcelHelper.FormatDDMMYYYY);
                        col++; worksheetLTL.Cells[row, col].Value = item.Ton;
                        col++; worksheetLTL.Cells[row, col].Value = item.CBM;
                        stt++;
                        row++;
                    }


                    col = 1;
                    col++; worksheetLTL.Cells[row, col].Value = "Trung bình";
                    col++; worksheetLTL.Cells[row, col].Value = lst.ListLTL.Where(c => c.Ton > 0).Sum(c => c.Ton) / (double)lst.ListLTL.Count;
                    col++; worksheetLTL.Cells[row, col].Value = lst.ListLTL.Where(c => c.CBM > 0).Sum(c => c.CBM) / (double)lst.ListLTL.Count;

                    for (int i = 1; i <= worksheetLTL.Dimension.End.Row; i++)
                    {
                        for (int j = 1; j <= worksheetLTL.Dimension.End.Column; j++)
                        {
                            worksheetLTL.Cells[i, j].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                        }
                    }

                    //FTL
                    ExcelWorksheet worksheetFTL = package.Workbook.Worksheets.Add("FTL");

                    col = 0;
                    row = 0;
                    row = 1;
                    col = 1;
                    worksheetFTL.Cells[row, col].Value = "STT";
                    col++; worksheetFTL.Cells[row, col].Value = "Ngày"; worksheetFTL.Column(col).Width = 20;
                    col++; worksheetFTL.Cells[row, col].Value = "Tổng"; worksheetFTL.Column(col).Width = 20;
                    for (int i = 1; i <= col; i++)
                        ExcelHelper.CreateCellStyle(worksheetFTL, row, i, row, i, true, true, ExcelHelper.ColorGreen, ExcelHelper.ColorWhite, 0, "");
                    row++;
                    stt = 1;
                    foreach (var item in lst.ListFTL)
                    {
                        col = 1;
                        worksheetFTL.Cells[row, col].Value = stt;
                        col++; worksheetFTL.Cells[row, col].Value = item.Date;
                        ExcelHelper.CreateFormat(worksheetFTL, row, col, ExcelHelper.FormatDDMMYYYY);
                        col++; worksheetFTL.Cells[row, col].Value = item.Total;
                        stt++;
                        row++;
                    }

                    for (int i = 1; i <= worksheetFTL.Dimension.End.Row; i++)
                    {
                        for (int j = 1; j <= worksheetFTL.Dimension.End.Column; j++)
                        {
                            worksheetFTL.Cells[i, j].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                        }
                    }

                    //FCL
                    ExcelWorksheet worksheetFCL = package.Workbook.Worksheets.Add("FCL");

                    col = 0;
                    row = 0;
                    row = 1;
                    col = 1;
                    worksheetFCL.Cells[row, col].Value = "STT";
                    col++; worksheetFCL.Cells[row, col].Value = "Ngày"; worksheetFCL.Column(col).Width = 20;
                    col++; worksheetFCL.Cells[row, col].Value = "Tổng 20DC"; worksheetFCL.Column(col).Width = 20;
                    col++; worksheetFCL.Cells[row, col].Value = "Tổng 40DC"; worksheetFCL.Column(col).Width = 20;
                    col++; worksheetFCL.Cells[row, col].Value = "Tổng 40HC"; worksheetFCL.Column(col).Width = 20;
                    for (int i = 1; i <= col; i++)
                        ExcelHelper.CreateCellStyle(worksheetFCL, row, i, row, i, true, true, ExcelHelper.ColorGreen, ExcelHelper.ColorWhite, 0, "");
                    row++;
                    stt = 1;
                    foreach (var item in lst.ListFCL)
                    {
                        col = 1;
                        worksheetFCL.Cells[row, col].Value = stt;
                        col++; worksheetFCL.Cells[row, col].Value = item.Date;
                        ExcelHelper.CreateFormat(worksheetFCL, row, col, ExcelHelper.FormatDDMMYYYY);
                        col++; worksheetFCL.Cells[row, col].Value = item.Total20DC;
                        col++; worksheetFCL.Cells[row, col].Value = item.Total40DC;
                        col++; worksheetFCL.Cells[row, col].Value = item.Total40HC;
                        stt++;
                        row++;
                    }

                    col = 1;
                    col++; worksheetFCL.Cells[row, col].Value = "Trung bình";
                    col++; worksheetFCL.Cells[row, col].Value = lst.ListFCL.Where(c => c.Total20DC > 0).Sum(c => c.Total20DC) / (double)lst.ListFCL.Count;
                    col++; worksheetFCL.Cells[row, col].Value = lst.ListFCL.Where(c => c.Total40DC > 0).Sum(c => c.Total40DC) / (double)lst.ListFCL.Count;
                    col++; worksheetFCL.Cells[row, col].Value = lst.ListFCL.Where(c => c.Total40HC > 0).Sum(c => c.Total40HC) / (double)lst.ListFCL.Count;

                    for (int i = 1; i <= worksheetFCL.Dimension.End.Row; i++)
                    {
                        for (int j = 1; j <= worksheetFCL.Dimension.End.Column; j++)
                        {
                            worksheetFCL.Cells[i, j].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                        }
                    }

                    //LCL
                    ExcelWorksheet worksheetLCL = package.Workbook.Worksheets.Add("LCL");

                    col = 0;
                    row = 0;
                    row = 1;
                    col = 1;
                    worksheetLCL.Cells[row, col].Value = "STT";
                    col++; worksheetLCL.Cells[row, col].Value = "Ngày"; worksheetLCL.Column(col).Width = 20;
                    col++; worksheetLCL.Cells[row, col].Value = "Tổng 20DC"; worksheetLCL.Column(col).Width = 20;
                    col++; worksheetLCL.Cells[row, col].Value = "Tổng 40DC"; worksheetLCL.Column(col).Width = 20;
                    col++; worksheetLCL.Cells[row, col].Value = "Tổng 40HC"; worksheetLCL.Column(col).Width = 20;
                    for (int i = 1; i <= col; i++)
                        ExcelHelper.CreateCellStyle(worksheetLCL, row, i, row, i, true, true, ExcelHelper.ColorGreen, ExcelHelper.ColorWhite, 0, "");
                    row++;
                    stt = 1;
                    foreach (var item in lst.ListLCL)
                    {
                        col = 1;
                        worksheetLCL.Cells[row, col].Value = stt;
                        col++; worksheetLCL.Cells[row, col].Value = item.Date;
                        ExcelHelper.CreateFormat(worksheetLCL, row, col, ExcelHelper.FormatDDMMYYYY);
                        col++; worksheetLCL.Cells[row, col].Value = item.Total20DC;
                        col++; worksheetLCL.Cells[row, col].Value = item.Total40DC;
                        col++; worksheetLCL.Cells[row, col].Value = item.Total40HC;
                        stt++;
                        row++;
                    }

                    col = 1;
                    col++; worksheetLCL.Cells[row, col].Value = "Trung bình";
                    col++; worksheetLCL.Cells[row, col].Value = lst.ListLCL.Where(c => c.Total20DC > 0).Sum(c => c.Total20DC) / (double)lst.ListLCL.Count;
                    col++; worksheetLCL.Cells[row, col].Value = lst.ListLCL.Where(c => c.Total40DC > 0).Sum(c => c.Total40DC) / (double)lst.ListLCL.Count;
                    col++; worksheetLCL.Cells[row, col].Value = lst.ListLCL.Where(c => c.Total40HC > 0).Sum(c => c.Total40HC) / (double)lst.ListLCL.Count;

                    for (int i = 1; i <= worksheetLCL.Dimension.End.Row; i++)
                    {
                        for (int j = 1; j <= worksheetLCL.Dimension.End.Column; j++)
                        {
                            worksheetLCL.Cells[i, j].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                        }
                    }
                    package.Save();
                }
                result = filepath;
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //4
        [HttpPost]
        public string Customer_Order_Rate_ExcelExport(dynamic dynParam)
        {
            try
            {
                var result = string.Empty;
                DateTime dtFrom = Convert.ToDateTime(dynParam.dtfrom.ToString());
                DateTime dtTo = Convert.ToDateTime(dynParam.dtto.ToString());
                int quantity = Convert.ToInt32(dynParam.quantity.ToString());

                var lst = new Chart_Customer_Order_Rate();
                ServiceFactory.SVSystem((ISVSystem sv) =>
                {
                    lst = sv.Chart_Customer_Order_Rate_Data(dtFrom, dtTo, quantity);
                });
                string filepath = "/" + FolderUpload.Export + "exportCustomerOrderRate" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx";
                if (System.IO.File.Exists(HttpContext.Current.Server.MapPath(filepath)))
                    System.IO.File.Delete(HttpContext.Current.Server.MapPath(filepath));
                FileInfo exportfile = new FileInfo(HttpContext.Current.Server.MapPath(filepath));
                using (ExcelPackage package = new ExcelPackage(exportfile))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Sheet 1");

                    int col = 0;
                    int row = 0;
                    row = 1;
                    col = 1;
                    worksheet.Cells[row, col].Value = "STT";
                    col++; worksheet.Cells[row, col].Value = "Tên khách hàng"; worksheet.Column(col).Width = 20;
                    col++; worksheet.Cells[row, col].Value = "Tổng đơn"; worksheet.Column(col).Width = 20;
                    col++; worksheet.Cells[row, col].Value = "Tỉ lệ (%)"; worksheet.Column(col).Width = 20;
                    for (int i = 1; i <= col; i++)
                        ExcelHelper.CreateCellStyle(worksheet, row, i, row, i, true, true, ExcelHelper.ColorGreen, ExcelHelper.ColorWhite, 0, "");
                    row++;
                    int stt = 1;
                    foreach (var item in lst.ListData)
                    {
                        col = 1;
                        worksheet.Cells[row, col].Value = stt;
                        col++; worksheet.Cells[row, col].Value = item.CustomerName;
                        col++; worksheet.Cells[row, col].Value = item.Total;
                        col++; worksheet.Cells[row, col].Value = item.Percent;
                        stt++;
                        row++;
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
                result = filepath;
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //5
        [HttpPost]
        public string Owner_Capacity_ExcelExport(dynamic dynParam)
        {
            try
            {
                var result = string.Empty;
                DateTime dtFrom = Convert.ToDateTime(dynParam.dtfrom.ToString());
                DateTime dtTo = Convert.ToDateTime(dynParam.dtto.ToString());

                var lst = new Chart_Owner_Capacity();
                ServiceFactory.SVSystem((ISVSystem sv) =>
                {
                    lst = sv.Chart_Owner_Capacity_Data(dtFrom, dtTo);
                });
                string filepath = "/" + FolderUpload.Export + "exportOwnerCapacity" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx";
                if (System.IO.File.Exists(HttpContext.Current.Server.MapPath(filepath)))
                    System.IO.File.Delete(HttpContext.Current.Server.MapPath(filepath));
                FileInfo exportfile = new FileInfo(HttpContext.Current.Server.MapPath(filepath));
                using (ExcelPackage package = new ExcelPackage(exportfile))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Sheet 1");

                    int col = 0;
                    int row = 0;
                    row = 1;
                    col = 1;
                    worksheet.Cells[row, col].Value = "STT";
                    col++; worksheet.Cells[row, col].Value = "Loại xe"; worksheet.Column(col).Width = 20;
                    col++; worksheet.Cells[row, col].Value = "Tỉ lệ tấn"; worksheet.Column(col).Width = 20;
                    col++; worksheet.Cells[row, col].Value = "Tỉ lệ khối"; worksheet.Column(col).Width = 20;
                    for (int i = 1; i <= col; i++)
                        ExcelHelper.CreateCellStyle(worksheet, row, i, row, i, true, true, ExcelHelper.ColorGreen, ExcelHelper.ColorWhite, 0, "");
                    row++;
                    int stt = 1;
                    foreach (var item in lst.ListData)
                    {
                        col = 1;
                        worksheet.Cells[row, col].Value = stt;
                        col++; worksheet.Cells[row, col].Value = item.GroupOfVehicleCode;
                        col++; worksheet.Cells[row, col].Value = item.ValueTon;
                        col++; worksheet.Cells[row, col].Value = item.ValueCBM;
                        stt++;
                        row++;
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
                result = filepath;
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //6
        [HttpPost]
        public string Owner_KM_ExcelExport(dynamic dynParam)
        {
            try
            {
                var result = string.Empty;
                DateTime dtFrom = Convert.ToDateTime(dynParam.dtfrom.ToString());
                DateTime dtTo = Convert.ToDateTime(dynParam.dtto.ToString());

                var lst = new Chart_Owner_KM();
                ServiceFactory.SVSystem((ISVSystem sv) =>
                {
                    lst = sv.Chart_Owner_KM_Data(dtFrom, dtTo);
                });
                string filepath = "/" + FolderUpload.Export + "exportOwnerKM" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx";
                if (System.IO.File.Exists(HttpContext.Current.Server.MapPath(filepath)))
                    System.IO.File.Delete(HttpContext.Current.Server.MapPath(filepath));
                FileInfo exportfile = new FileInfo(HttpContext.Current.Server.MapPath(filepath));
                using (ExcelPackage package = new ExcelPackage(exportfile))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Sheet 1");

                    int col = 0;
                    int row = 0;
                    row = 1;
                    col = 1;
                    worksheet.Cells[row, col].Value = "STT";
                    col++; worksheet.Cells[row, col].Value = "Ngày"; worksheet.Column(col).Width = 20;
                    col++; worksheet.Cells[row, col].Value = "Không có hàng"; worksheet.Column(col).Width = 20;
                    col++; worksheet.Cells[row, col].Value = "Có hàng"; worksheet.Column(col).Width = 20;
                    for (int i = 1; i <= col; i++)
                        ExcelHelper.CreateCellStyle(worksheet, row, i, row, i, true, true, ExcelHelper.ColorGreen, ExcelHelper.ColorWhite, 0, "");
                    row++;
                    int stt = 1;
                    foreach (var item in lst.ListData)
                    {
                        col = 1;
                        worksheet.Cells[row, col].Value = stt;
                        col++; worksheet.Cells[row, col].Value = item.Date;
                        ExcelHelper.CreateFormat(worksheet, row, col, ExcelHelper.FormatDDMMYYYY);
                        col++; worksheet.Cells[row, col].Value = item.KMEmpty;
                        col++; worksheet.Cells[row, col].Value = item.KMLaden;
                        stt++;
                        row++;
                    }

                    col = 1;
                    col++; worksheet.Cells[row, col].Value = "Trung bình";
                    col++; worksheet.Cells[row, col].Value = lst.ListData.Where(c => c.KMEmpty > 0).Sum(c => c.KMEmpty) / (double)lst.ListData.Count;
                    col++; worksheet.Cells[row, col].Value = lst.ListData.Where(c => c.KMLaden > 0).Sum(c => c.KMLaden) / (double)lst.ListData.Count;

                    for (int i = 1; i <= worksheet.Dimension.End.Row; i++)
                    {
                        for (int j = 1; j <= worksheet.Dimension.End.Column; j++)
                        {
                            worksheet.Cells[i, j].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                        }
                    }
                    package.Save();
                }
                result = filepath;
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //7
        [HttpPost]
        public string Owner_Operation_ExcelExport(dynamic dynParam)
        {
            try
            {
                var result = string.Empty;
                DateTime dtFrom = Convert.ToDateTime(dynParam.dtfrom.ToString());
                DateTime dtTo = Convert.ToDateTime(dynParam.dtto.ToString());

                var lst = new Chart_Owner_Operation();
                ServiceFactory.SVSystem((ISVSystem sv) =>
                {
                    lst = sv.Chart_Owner_Operation_Data(dtFrom, dtTo);
                });
                string filepath = "/" + FolderUpload.Export + "exportOwnerOperation" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx";
                if (System.IO.File.Exists(HttpContext.Current.Server.MapPath(filepath)))
                    System.IO.File.Delete(HttpContext.Current.Server.MapPath(filepath));
                FileInfo exportfile = new FileInfo(HttpContext.Current.Server.MapPath(filepath));
                using (ExcelPackage package = new ExcelPackage(exportfile))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Sheet 1");

                    int col = 0;
                    int row = 0;
                    row = 1;
                    col = 1;
                    worksheet.Cells[row, col].Value = "STT";
                    col++; worksheet.Cells[row, col].Value = "Số xe"; worksheet.Column(col).Width = 20;
                    col++; worksheet.Cells[row, col].Value = "Tổng giờ"; worksheet.Column(col).Width = 20;
                    col++; worksheet.Cells[row, col].Value = "Thời gian chạy"; worksheet.Column(col).Width = 20;
                    col++; worksheet.Cells[row, col].Value = "Thời gian bốc xếp"; worksheet.Column(col).Width = 20;
                    col++; worksheet.Cells[row, col].Value = "Thời gian chờ"; worksheet.Column(col).Width = 20;
                    col++; worksheet.Cells[row, col].Value = "Thời gian thừa"; worksheet.Column(col).Width = 20;
                    for (int i = 1; i <= col; i++)
                        ExcelHelper.CreateCellStyle(worksheet, row, i, row, i, true, true, ExcelHelper.ColorGreen, ExcelHelper.ColorWhite, 0, "");
                    row++;
                    int stt = 1;
                    foreach (var item in lst.ListData)
                    {
                        col = 1;
                        worksheet.Cells[row, col].Value = stt;
                        col++; worksheet.Cells[row, col].Value = item.VehicleCode;
                        col++; worksheet.Cells[row, col].Value = item.TotalTime;
                        col++; worksheet.Cells[row, col].Value = item.RunningTime;
                        col++; worksheet.Cells[row, col].Value = item.LoadingTime;
                        col++; worksheet.Cells[row, col].Value = item.WaittingTime;
                        col++; worksheet.Cells[row, col].Value = item.OtherTime;
                        stt++;
                        row++;
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
                result = filepath;
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //8
        [HttpPost]
        public string Owner_CostRate_ExcelExport(dynamic dynParam)
        {
            try
            {
                var result = string.Empty;
                DateTime dtFrom = Convert.ToDateTime(dynParam.dtfrom.ToString());
                DateTime dtTo = Convert.ToDateTime(dynParam.dtto.ToString());

                var lst = new Chart_Owner_CostRate();
                ServiceFactory.SVSystem((ISVSystem sv) =>
                {
                    lst = sv.Chart_Owner_CostRate_Data(dtFrom, dtTo);
                });
                string filepath = "/" + FolderUpload.Export + "exportOwnerCostRate" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx";
                if (System.IO.File.Exists(HttpContext.Current.Server.MapPath(filepath)))
                    System.IO.File.Delete(HttpContext.Current.Server.MapPath(filepath));
                FileInfo exportfile = new FileInfo(HttpContext.Current.Server.MapPath(filepath));
                using (ExcelPackage package = new ExcelPackage(exportfile))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Sheet 1");

                    int col = 0;
                    int row = 0;
                    row = 1;
                    col = 1;
                    worksheet.Cells[row, col].Value = "STT";
                    col++; worksheet.Cells[row, col].Value = "Số xe"; worksheet.Column(col).Width = 20;
                    col++; worksheet.Cells[row, col].Value = "C.phí mỗi Km"; worksheet.Column(col).Width = 20;
                    col++; worksheet.Cells[row, col].Value = "C.phí mỗi tấn"; worksheet.Column(col).Width = 20;
                    col++; worksheet.Cells[row, col].Value = "C.phí mỗi khối"; worksheet.Column(col).Width = 20;
                    for (int i = 1; i <= col; i++)
                        ExcelHelper.CreateCellStyle(worksheet, row, i, row, i, true, true, ExcelHelper.ColorGreen, ExcelHelper.ColorWhite, 0, "");
                    row++;
                    int stt = 1;
                    foreach (var item in lst.ListData)
                    {
                        col = 1;
                        worksheet.Cells[row, col].Value = stt;
                        col++; worksheet.Cells[row, col].Value = item.VehicleCode;
                        col++; worksheet.Cells[row, col].Value = item.KMIndex;
                        col++; worksheet.Cells[row, col].Value = item.TonIndex;
                        col++; worksheet.Cells[row, col].Value = item.CBMIndex;
                        stt++;
                        row++;
                    }

                    col = 1;
                    col++; worksheet.Cells[row, col].Value = "Trung bình";
                    col++; worksheet.Cells[row, col].Value = lst.ListData.Where(c => c.KMIndex > 0).Sum(c => c.KMIndex) / (double)lst.ListData.Count;
                    col++; worksheet.Cells[row, col].Value = lst.ListData.Where(c => c.TonIndex > 0).Sum(c => c.TonIndex) / (double)lst.ListData.Count;
                    col++; worksheet.Cells[row, col].Value = lst.ListData.Where(c => c.CBMIndex > 0).Sum(c => c.CBMIndex) / (double)lst.ListData.Count;

                    for (int i = 1; i <= worksheet.Dimension.End.Row; i++)
                    {
                        for (int j = 1; j <= worksheet.Dimension.End.Column; j++)
                        {
                            worksheet.Cells[i, j].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                        }
                    }
                    package.Save();
                }
                result = filepath;
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //9
        [HttpPost]
        public string Owner_CostChange_ExcelExport(dynamic dynParam)
        {
            try
            {
                var result = string.Empty;

                DateTime dtFrom = Convert.ToDateTime(dynParam.dtfrom.ToString());
                DateTime dtTo = Convert.ToDateTime(dynParam.dtto.ToString());

                var lst = new Chart_Owner_CostChange();
                ServiceFactory.SVSystem((ISVSystem sv) =>
                {
                    lst = sv.Chart_Owner_CostChange_Data(dtFrom, dtTo);
                });
                string filepath = "/" + FolderUpload.Export + "exportOwnerCostChange" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx";
                if (System.IO.File.Exists(HttpContext.Current.Server.MapPath(filepath)))
                    System.IO.File.Delete(HttpContext.Current.Server.MapPath(filepath));
                FileInfo exportfile = new FileInfo(HttpContext.Current.Server.MapPath(filepath));
                using (ExcelPackage package = new ExcelPackage(exportfile))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Sheet 1");

                    int col = 0;
                    int row = 0;
                    row = 1;
                    col = 1;
                    worksheet.Cells[row, col].Value = "STT";
                    col++; worksheet.Cells[row, col].Value = "Loại"; worksheet.Column(col).Width = 20;
                    col++; worksheet.Cells[row, col].Value = "Tổng"; worksheet.Column(col).Width = 20;
                    ExcelHelper.CreateFormat(worksheet, row, col, ExcelHelper.FormatMoney);
                    col++; worksheet.Cells[row, col].Value = "Tỉ lệ"; worksheet.Column(col).Width = 20;
                    for (int i = 1; i <= col; i++)
                        ExcelHelper.CreateCellStyle(worksheet, row, i, row, i, true, true, ExcelHelper.ColorGreen, ExcelHelper.ColorWhite, 0, "");
                    row++;
                    int stt = 1;
                    foreach (var item in lst.ListData)
                    {
                        col = 1;
                        worksheet.Cells[row, col].Value = stt;
                        col++; worksheet.Cells[row, col].Value = item.Category;
                        col++; worksheet.Cells[row, col].Value = item.Total;
                        col++; worksheet.Cells[row, col].Value = item.Percent;
                        stt++;
                        row++;
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
                result = filepath;
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //10
        [HttpPost]
        public string Owner_OnTime_PickUp_ExcelExport(dynamic dynParam)
        {
            try
            {
                var result = string.Empty;

                DateTime dtFrom = Convert.ToDateTime(dynParam.dtfrom.ToString());
                DateTime dtTo = Convert.ToDateTime(dynParam.dtto.ToString());
                int? customerID = null;
                try
                {
                    customerID = Convert.ToInt32(dynParam.customerid.ToString());
                }
                catch { }

                var lst = new Chart_Owner_OnTime();
                ServiceFactory.SVSystem((ISVSystem sv) =>
                {
                    lst = sv.Chart_Owner_OnTime_PickUp_Data(dtFrom, dtTo, customerID);
                });
                string filepath = "/" + FolderUpload.Export + "exportOwnerOnTimePickUp" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx";
                if (System.IO.File.Exists(HttpContext.Current.Server.MapPath(filepath)))
                    System.IO.File.Delete(HttpContext.Current.Server.MapPath(filepath));
                FileInfo exportfile = new FileInfo(HttpContext.Current.Server.MapPath(filepath));
                using (ExcelPackage package = new ExcelPackage(exportfile))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Sheet 1");

                    int col = 0;
                    int row = 0;
                    row = 1;
                    col = 1;
                    worksheet.Cells[row, col].Value = "STT";
                    col++; worksheet.Cells[row, col].Value = "Ngày"; worksheet.Column(col).Width = 20;
                    col++; worksheet.Cells[row, col].Value = "Tổng đơn"; worksheet.Column(col).Width = 20;
                    col++; worksheet.Cells[row, col].Value = "Tỉ lệ đúng giờ"; worksheet.Column(col).Width = 20;
                    for (int i = 1; i <= col; i++)
                        ExcelHelper.CreateCellStyle(worksheet, row, i, row, i, true, true, ExcelHelper.ColorGreen, ExcelHelper.ColorWhite, 0, "");
                    row++;
                    int stt = 1;
                    foreach (var item in lst.ListData)
                    {
                        col = 1;
                        worksheet.Cells[row, col].Value = stt;
                        col++; worksheet.Cells[row, col].Value = item.Date;
                        ExcelHelper.CreateFormat(worksheet, row, col, ExcelHelper.FormatDDMMYYYY);
                        col++; worksheet.Cells[row, col].Value = item.Total;
                        col++; worksheet.Cells[row, col].Value = item.Percent;
                        stt++;
                        row++;
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
                result = filepath;
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //11
        [HttpPost]
        public string Owner_OnTime_Delivery_ExcelExport(dynamic dynParam)
        {
            try
            {
                var result = string.Empty;

                DateTime dtFrom = Convert.ToDateTime(dynParam.dtfrom.ToString());
                DateTime dtTo = Convert.ToDateTime(dynParam.dtto.ToString());
                int? customerID = null;
                try
                {
                    customerID = Convert.ToInt32(dynParam.customerid.ToString());
                }
                catch { }

                var lst = new Chart_Owner_OnTime();
                ServiceFactory.SVSystem((ISVSystem sv) =>
                {
                    lst = sv.Chart_Owner_OnTime_Delivery_Data(dtFrom, dtTo, customerID);
                });

                string filepath = "/" + FolderUpload.Export + "exportOwnerOnTimePickUp" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx";
                if (System.IO.File.Exists(HttpContext.Current.Server.MapPath(filepath)))
                    System.IO.File.Delete(HttpContext.Current.Server.MapPath(filepath));
                FileInfo exportfile = new FileInfo(HttpContext.Current.Server.MapPath(filepath));
                using (ExcelPackage package = new ExcelPackage(exportfile))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Sheet 1");

                    int col = 0;
                    int row = 0;
                    row = 1;
                    col = 1;
                    worksheet.Cells[row, col].Value = "STT";
                    col++; worksheet.Cells[row, col].Value = "Ngày"; worksheet.Column(col).Width = 20;
                    col++; worksheet.Cells[row, col].Value = "Tổng đơn"; worksheet.Column(col).Width = 20;
                    col++; worksheet.Cells[row, col].Value = "Tỉ lệ đúng giờ"; worksheet.Column(col).Width = 20;
                    for (int i = 1; i <= col; i++)
                        ExcelHelper.CreateCellStyle(worksheet, row, i, row, i, true, true, ExcelHelper.ColorGreen, ExcelHelper.ColorWhite, 0, "");
                    row++;
                    int stt = 1;
                    foreach (var item in lst.ListData)
                    {
                        col = 1;
                        worksheet.Cells[row, col].Value = stt;
                        col++; worksheet.Cells[row, col].Value = item.Date;
                        ExcelHelper.CreateFormat(worksheet, row, col, ExcelHelper.FormatDDMMYYYY);
                        col++; worksheet.Cells[row, col].Value = item.Total;
                        col++; worksheet.Cells[row, col].Value = item.Percent;
                        stt++;
                        row++;
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
                result = filepath;
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //12
        [HttpPost]
        public string Owner_OnTime_POD_ExcelExport(dynamic dynParam)
        {
            try
            {
                var result = string.Empty;

                DateTime dtFrom = Convert.ToDateTime(dynParam.dtfrom.ToString());
                DateTime dtTo = Convert.ToDateTime(dynParam.dtto.ToString());
                int? customerID = null;
                try
                {
                    customerID = Convert.ToInt32(dynParam.customerid.ToString());
                }
                catch { }

                var lst = new Chart_Owner_OnTime();
                ServiceFactory.SVSystem((ISVSystem sv) =>
                {
                    lst = sv.Chart_Owner_OnTime_POD_Data(dtFrom, dtTo, customerID);
                });

                string filepath = "/" + FolderUpload.Export + "exportOwnerOnTimePOD" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx";
                if (System.IO.File.Exists(HttpContext.Current.Server.MapPath(filepath)))
                    System.IO.File.Delete(HttpContext.Current.Server.MapPath(filepath));
                FileInfo exportfile = new FileInfo(HttpContext.Current.Server.MapPath(filepath));
                using (ExcelPackage package = new ExcelPackage(exportfile))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Sheet 1");

                    int col = 0;
                    int row = 0;
                    row = 1;
                    col = 1;
                    worksheet.Cells[row, col].Value = "STT";
                    col++; worksheet.Cells[row, col].Value = "Ngày"; worksheet.Column(col).Width = 20;
                    col++; worksheet.Cells[row, col].Value = "Tổng c.từ"; worksheet.Column(col).Width = 20;
                    col++; worksheet.Cells[row, col].Value = "Tỉ lệ đúng giờ"; worksheet.Column(col).Width = 20;
                    for (int i = 1; i <= col; i++)
                        ExcelHelper.CreateCellStyle(worksheet, row, i, row, i, true, true, ExcelHelper.ColorGreen, ExcelHelper.ColorWhite, 0, "");
                    row++;
                    int stt = 1;
                    foreach (var item in lst.ListData)
                    {
                        col = 1;
                        worksheet.Cells[row, col].Value = stt;
                        col++; worksheet.Cells[row, col].Value = item.Date;
                        ExcelHelper.CreateFormat(worksheet, row, col, ExcelHelper.FormatDDMMYYYY);
                        col++; worksheet.Cells[row, col].Value = item.Total;
                        col++; worksheet.Cells[row, col].Value = item.Percent;
                        stt++;
                        row++;
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
                result = filepath;
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //13
        [HttpPost]
        public string Owner_Return_ExcelExport(dynamic dynParam)
        {
            try
            {
                var result = string.Empty;

                DateTime dtFrom = Convert.ToDateTime(dynParam.dtfrom.ToString());
                DateTime dtTo = Convert.ToDateTime(dynParam.dtto.ToString());
                int? customerID = null;
                try
                {
                    customerID = Convert.ToInt32(dynParam.customerid.ToString());
                }
                catch { }

                var lst = new Chart_Owner_Return();
                ServiceFactory.SVSystem((ISVSystem sv) =>
                {
                    lst = sv.Chart_Owner_Return_Data(dtFrom, dtTo, customerID);
                });

                string filepath = "/" + FolderUpload.Export + "exportOwnerReturn" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx";
                if (System.IO.File.Exists(HttpContext.Current.Server.MapPath(filepath)))
                    System.IO.File.Delete(HttpContext.Current.Server.MapPath(filepath));
                FileInfo exportfile = new FileInfo(HttpContext.Current.Server.MapPath(filepath));
                using (ExcelPackage package = new ExcelPackage(exportfile))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Sheet 1");

                    int col = 0;
                    int row = 0;
                    row = 1;
                    col = 1;
                    worksheet.Cells[row, col].Value = "STT";
                    col++; worksheet.Cells[row, col].Value = "Ngày"; worksheet.Column(col).Width = 20;
                    col++; worksheet.Cells[row, col].Value = "Tấn"; worksheet.Column(col).Width = 20;
                    col++; worksheet.Cells[row, col].Value = "Khối"; worksheet.Column(col).Width = 20;
                    col++; worksheet.Cells[row, col].Value = "Số lượng"; worksheet.Column(col).Width = 20;
                    for (int i = 1; i <= col; i++)
                        ExcelHelper.CreateCellStyle(worksheet, row, i, row, i, true, true, ExcelHelper.ColorGreen, ExcelHelper.ColorWhite, 0, "");
                    row++;
                    int stt = 1;
                    foreach (var item in lst.ListData)
                    {
                        col = 1;
                        worksheet.Cells[row, col].Value = stt;
                        col++; worksheet.Cells[row, col].Value = item.Date;
                        ExcelHelper.CreateFormat(worksheet, row, col, ExcelHelper.FormatDDMMYYYY);
                        col++; worksheet.Cells[row, col].Value = item.Ton;
                        col++; worksheet.Cells[row, col].Value = item.CBM;
                        col++; worksheet.Cells[row, col].Value = item.Quantity;
                        stt++;
                        row++;
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
                result = filepath;
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //14
        [HttpPost]
        public string Owner_Profit_ExcelExport(dynamic dynParam)
        {
            try
            {
                var result = string.Empty;

                DateTime dtFrom = Convert.ToDateTime(dynParam.dtfrom.ToString());
                DateTime dtTo = Convert.ToDateTime(dynParam.dtto.ToString());
                int? customerID = null;
                try
                {
                    customerID = Convert.ToInt32(dynParam.customerid.ToString());
                }
                catch { }

                var lst = new Chart_Owner_Profit();
                ServiceFactory.SVSystem((ISVSystem sv) =>
                {
                    lst = sv.Chart_Owner_Profit_Data(dtFrom, dtTo, customerID);
                });
                string filepath = "/" + FolderUpload.Export + "exportOwnerProfit" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx";
                if (System.IO.File.Exists(HttpContext.Current.Server.MapPath(filepath)))
                    System.IO.File.Delete(HttpContext.Current.Server.MapPath(filepath));
                FileInfo exportfile = new FileInfo(HttpContext.Current.Server.MapPath(filepath));
                using (ExcelPackage package = new ExcelPackage(exportfile))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Sheet 1");

                    int col = 0;
                    int row = 0;
                    row = 1;
                    col = 1;
                    worksheet.Cells[row, col].Value = "STT";
                    col++; worksheet.Cells[row, col].Value = "Loại"; worksheet.Column(col).Width = 20;
                    col++; worksheet.Cells[row, col].Value = "Doanh thu"; worksheet.Column(col).Width = 20;
                    col++; worksheet.Cells[row, col].Value = "Chi phí"; worksheet.Column(col).Width = 20;

                    for (int i = 1; i <= col; i++)
                        ExcelHelper.CreateCellStyle(worksheet, row, i, row, i, true, true, ExcelHelper.ColorGreen, ExcelHelper.ColorWhite, 0, "");
                    row++;
                    int stt = 1;
                    foreach (var item in lst.ListData)
                    {
                        col = 1;
                        worksheet.Cells[row, col].Value = stt;
                        col++; worksheet.Cells[row, col].Value = item.Category;
                        col++; worksheet.Cells[row, col].Value = item.Credit;
                        ExcelHelper.CreateFormat(worksheet, row, col, ExcelHelper.FormatMoney);
                        col++; worksheet.Cells[row, col].Value = item.Debit;
                        ExcelHelper.CreateFormat(worksheet, row, col, ExcelHelper.FormatMoney);
                        stt++;
                        row++;
                    }

                    col = 1;
                    col++; worksheet.Cells[row, col].Value = "Tổng";
                    col++; worksheet.Cells[row, col].Value = lst.ListData.Where(c => c.Credit > 0).Sum(c => c.Credit);
                    ExcelHelper.CreateFormat(worksheet, row, col, ExcelHelper.FormatMoney);
                    col++; worksheet.Cells[row, col].Value = lst.ListData.Where(c => c.Debit > 0).Sum(c => c.Debit);
                    ExcelHelper.CreateFormat(worksheet, row, col, ExcelHelper.FormatMoney);

                    for (int i = 1; i <= worksheet.Dimension.End.Row; i++)
                    {
                        for (int j = 1; j <= worksheet.Dimension.End.Column; j++)
                        {
                            worksheet.Cells[i, j].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                        }
                    }
                    package.Save();
                }
                result = filepath;
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //16
        [HttpPost]
        public string Owner_OnTime_PickUp_Radial_ExcelExport(dynamic dynParam)
        {
            try
            {
                var result = string.Empty;

                DateTime dtFrom = Convert.ToDateTime(dynParam.dtfrom.ToString());
                DateTime dtTo = Convert.ToDateTime(dynParam.dtto.ToString());
                int? customerID = null;
                try
                {
                    customerID = Convert.ToInt32(dynParam.customerid.ToString());
                }
                catch { }

                var lst = new Chart_Owner_OnTime();
                ServiceFactory.SVSystem((ISVSystem sv) =>
                {
                    lst = sv.Chart_Owner_OnTime_PickUp_Radial_Data(dtFrom, dtTo, customerID);
                });

                string filepath = "/" + FolderUpload.Export + "exportOwnerOnTimePickUpRadial" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx";
                if (System.IO.File.Exists(HttpContext.Current.Server.MapPath(filepath)))
                    System.IO.File.Delete(HttpContext.Current.Server.MapPath(filepath));
                FileInfo exportfile = new FileInfo(HttpContext.Current.Server.MapPath(filepath));
                using (ExcelPackage package = new ExcelPackage(exportfile))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Sheet 1");

                    int col = 0;
                    int row = 0;
                    row = 1;
                    col = 1;
                    worksheet.Cells[row, col].Value = "STT";
                    col++; worksheet.Cells[row, col].Value = "Tổng"; worksheet.Column(col).Width = 20;
                    col++; worksheet.Cells[row, col].Value = "Tổng KPI"; worksheet.Column(col).Width = 20;
                    col++; worksheet.Cells[row, col].Value = "Tỉ lệ"; worksheet.Column(col).Width = 20;
                    for (int i = 1; i <= col; i++)
                        ExcelHelper.CreateCellStyle(worksheet, row, i, row, i, true, true, ExcelHelper.ColorGreen, ExcelHelper.ColorWhite, 0, "");
                    row++;
                    int stt = 1;
                    foreach (var item in lst.ListData)
                    {
                        col = 1;
                        worksheet.Cells[row, col].Value = stt;
                        col++; worksheet.Cells[row, col].Value = item.Total;
                        col++; worksheet.Cells[row, col].Value = item.TotalKPI;
                        col++; worksheet.Cells[row, col].Value = item.Percent;
                        stt++;
                        row++;
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
                result = filepath;
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //17
        [HttpPost]
        public string Owner_OnTime_Delivery_Radial_ExcelExport(dynamic dynParam)
        {
            try
            {
                var result = string.Empty;
                DateTime dtFrom = Convert.ToDateTime(dynParam.dtfrom.ToString());
                DateTime dtTo = Convert.ToDateTime(dynParam.dtto.ToString());
                int? customerID = null;
                try
                {
                    customerID = Convert.ToInt32(dynParam.customerid.ToString());
                }
                catch { }

                var lst = new Chart_Owner_OnTime();
                ServiceFactory.SVSystem((ISVSystem sv) =>
                {
                    lst = sv.Chart_Owner_OnTime_Delivery_Radial_Data(dtFrom, dtTo, customerID);
                });
                string filepath = "/" + FolderUpload.Export + "exportOwnerOnTimeDeliveryRadial" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx";
                if (System.IO.File.Exists(HttpContext.Current.Server.MapPath(filepath)))
                    System.IO.File.Delete(HttpContext.Current.Server.MapPath(filepath));
                FileInfo exportfile = new FileInfo(HttpContext.Current.Server.MapPath(filepath));
                using (ExcelPackage package = new ExcelPackage(exportfile))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Sheet 1");

                    int col = 0;
                    int row = 0;
                    row = 1;
                    col = 1;
                    worksheet.Cells[row, col].Value = "STT";
                    col++; worksheet.Cells[row, col].Value = "Ngày"; worksheet.Column(col).Width = 20;
                    col++; worksheet.Cells[row, col].Value = "Tổng"; worksheet.Column(col).Width = 20;
                    col++; worksheet.Cells[row, col].Value = "Tổng KPI"; worksheet.Column(col).Width = 20;
                    col++; worksheet.Cells[row, col].Value = "Tỉ lệ"; worksheet.Column(col).Width = 20;
                    for (int i = 1; i <= col; i++)
                        ExcelHelper.CreateCellStyle(worksheet, row, i, row, i, true, true, ExcelHelper.ColorGreen, ExcelHelper.ColorWhite, 0, "");
                    row++;
                    int stt = 1;
                    foreach (var item in lst.ListData)
                    {
                        col = 1;
                        worksheet.Cells[row, col].Value = stt;
                        col++; worksheet.Cells[row, col].Value = item.Date;
                        ExcelHelper.CreateFormat(worksheet, row, col, ExcelHelper.FormatDDMMYYYY);
                        col++; worksheet.Cells[row, col].Value = item.Total;
                        col++; worksheet.Cells[row, col].Value = item.TotalKPI;
                        col++; worksheet.Cells[row, col].Value = item.Percent;
                        stt++;
                        row++;
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
                result = filepath;
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //18
        [HttpPost]
        public string Owner_OnTime_POD_Radial_ExcelExport(dynamic dynParam)
        {
            try
            {
                var result = string.Empty;
                DateTime dtFrom = Convert.ToDateTime(dynParam.dtfrom.ToString());
                DateTime dtTo = Convert.ToDateTime(dynParam.dtto.ToString());
                int? customerID = null;
                try
                {
                    customerID = Convert.ToInt32(dynParam.customerid.ToString());
                }
                catch { }

                var lst = new Chart_Owner_OnTime();
                ServiceFactory.SVSystem((ISVSystem sv) =>
                {
                    lst = sv.Chart_Owner_OnTime_POD_Radial_Data(dtFrom, dtTo, customerID);
                });

                string filepath = "/" + FolderUpload.Export + "exportOwnerOnTimePODRadial" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx";
                if (System.IO.File.Exists(HttpContext.Current.Server.MapPath(filepath)))
                    System.IO.File.Delete(HttpContext.Current.Server.MapPath(filepath));
                FileInfo exportfile = new FileInfo(HttpContext.Current.Server.MapPath(filepath));
                using (ExcelPackage package = new ExcelPackage(exportfile))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Sheet 1");

                    int col = 0;
                    int row = 0;
                    row = 1;
                    col = 1;
                    worksheet.Cells[row, col].Value = "STT";
                    col++; worksheet.Cells[row, col].Value = "Ngày"; worksheet.Column(col).Width = 20;
                    col++; worksheet.Cells[row, col].Value = "Tổng"; worksheet.Column(col).Width = 20;
                    col++; worksheet.Cells[row, col].Value = "Tổng KPI"; worksheet.Column(col).Width = 20;
                    col++; worksheet.Cells[row, col].Value = "Tỉ lệ"; worksheet.Column(col).Width = 20;
                    for (int i = 1; i <= col; i++)
                        ExcelHelper.CreateCellStyle(worksheet, row, i, row, i, true, true, ExcelHelper.ColorGreen, ExcelHelper.ColorWhite, 0, "");
                    row++;
                    int stt = 1;
                    foreach (var item in lst.ListData)
                    {
                        col = 1;
                        worksheet.Cells[row, col].Value = stt;
                        col++; worksheet.Cells[row, col].Value = item.Date;
                        ExcelHelper.CreateFormat(worksheet, row, col, ExcelHelper.FormatDDMMYYYY);
                        col++; worksheet.Cells[row, col].Value = item.Total;
                        col++; worksheet.Cells[row, col].Value = item.TotalKPI;
                        col++; worksheet.Cells[row, col].Value = item.Percent;
                        stt++;
                        row++;
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
                result = filepath;
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //19 //20
        [HttpPost]
        public string Owner_Profit_Vendor_ExcelExport(dynamic dynParam)
        {
            try
            {
                DateTime dtFrom = Convert.ToDateTime(dynParam.dtfrom.ToString());
                DateTime dtTo = Convert.ToDateTime(dynParam.dtto.ToString());
                int TypeOfChart = (int)dynParam.TypeOfChart;
                var result = string.Empty;
                var lst = new Chart_Owner_Profit_Customer();
                ServiceFactory.SVSystem((ISVSystem sv) =>
                {
                    lst = sv.Chart_Owner_Profit_Vendor_Data(dtFrom, dtTo);
                });
                string filepath = "/" + FolderUpload.Export + "exportOwnerProfitVendor" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx";
                if (System.IO.File.Exists(HttpContext.Current.Server.MapPath(filepath)))
                    System.IO.File.Delete(HttpContext.Current.Server.MapPath(filepath));
                FileInfo exportfile = new FileInfo(HttpContext.Current.Server.MapPath(filepath));
                using (ExcelPackage package = new ExcelPackage(exportfile))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Sheet 1");

                    int col = 0;
                    int row = 0;
                    row = 1;
                    col = 1;
                    if (TypeOfChart == 19)
                    {
                        worksheet.Cells[row, col].Value = "STT";
                        col++; worksheet.Cells[row, col].Value = "Mã nhà thầu"; worksheet.Column(col).Width = 20;
                        col++; worksheet.Cells[row, col].Value = "Tên nhà thầu"; worksheet.Column(col).Width = 20;
                        col++; worksheet.Cells[row, col].Value = "Chi phí"; worksheet.Column(col).Width = 20;
                        for (int i = 1; i <= col; i++)
                            ExcelHelper.CreateCellStyle(worksheet, row, i, row, i, true, true, ExcelHelper.ColorGreen, ExcelHelper.ColorWhite, 0, "");
                        row++;
                        int stt = 1;
                        foreach (var item in lst.ListData)
                        {
                            col = 1;
                            worksheet.Cells[row, col].Value = stt;
                            col++; worksheet.Cells[row, col].Value = item.CustomerCode;
                            col++; worksheet.Cells[row, col].Value = item.CustomerName;
                            col++; worksheet.Cells[row, col].Value = item.Debit;
                            ExcelHelper.CreateFormat(worksheet, row, col, ExcelHelper.FormatMoney);
                            stt++;
                            row++;
                        }

                        col = 2;
                        col++; worksheet.Cells[row, col].Value = "Tổng";
                        col++; worksheet.Cells[row, col].Value = lst.ListData.Where(c => c.Debit > 0).Sum(c => c.Debit);
                    }
                    else
                    {
                        worksheet.Cells[row, col].Value = "STT";
                        col++; worksheet.Cells[row, col].Value = "Mã nhà thầu"; worksheet.Column(col).Width = 20;
                        col++; worksheet.Cells[row, col].Value = "Tên nhà thầu"; worksheet.Column(col).Width = 20;
                        col++; worksheet.Cells[row, col].Value = "Chi"; worksheet.Column(col).Width = 20;
                        col++; worksheet.Cells[row, col].Value = "Tỉ lệ chi (%)"; worksheet.Column(col).Width = 20;
                        for (int i = 1; i <= col; i++)
                            ExcelHelper.CreateCellStyle(worksheet, row, i, row, i, true, true, ExcelHelper.ColorGreen, ExcelHelper.ColorWhite, 0, "");
                        row++;
                        int stt = 1;
                        foreach (var item in lst.ListData)
                        {
                            col = 1;
                            worksheet.Cells[row, col].Value = stt;
                            col++; worksheet.Cells[row, col].Value = item.CustomerCode;
                            col++; worksheet.Cells[row, col].Value = item.CustomerName;
                            col++; worksheet.Cells[row, col].Value = item.Debit;
                            ExcelHelper.CreateFormat(worksheet, row, col, ExcelHelper.FormatMoney);
                            col++; worksheet.Cells[row, col].Value = item.DebitPercent;
                            stt++;
                            row++;
                        }
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
                result = filepath;
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //15//21
        [HttpPost]
        public string Owner_Profit_Customer_ExcelExport(dynamic dynParam)
        {
            try
            {
                var result = string.Empty;
                int TypeOfChart = (int)dynParam.TypeOfChart;
                DateTime dtFrom = Convert.ToDateTime(dynParam.dtfrom.ToString());
                DateTime dtTo = Convert.ToDateTime(dynParam.dtto.ToString());
                int? customerid = null;
                try
                {
                    customerid = Convert.ToInt32(dynParam.customerid.ToString());
                }
                catch { }
                var lst = new Chart_Owner_Profit_Customer();
                ServiceFactory.SVSystem((ISVSystem sv) =>
                {
                    lst = sv.Chart_Owner_Profit_Customer_Data(dtFrom, dtTo, customerid);
                });
                string filepath = "/" + FolderUpload.Export + "exportOwnerProfitCustomer" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx";
                if (System.IO.File.Exists(HttpContext.Current.Server.MapPath(filepath)))
                    System.IO.File.Delete(HttpContext.Current.Server.MapPath(filepath));
                FileInfo exportfile = new FileInfo(HttpContext.Current.Server.MapPath(filepath));
                using (ExcelPackage package = new ExcelPackage(exportfile))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Sheet 1");

                    int col = 0;
                    int row = 0;
                    row = 1;
                    col = 1;
                    if (TypeOfChart == 15)
                    {
                        worksheet.Cells[row, col].Value = "STT";
                        col++; worksheet.Cells[row, col].Value = "Mã khách hàng"; worksheet.Column(col).Width = 20;
                        col++; worksheet.Cells[row, col].Value = "Tên khách hàng"; worksheet.Column(col).Width = 20;
                        col++; worksheet.Cells[row, col].Value = "Doanh thu"; worksheet.Column(col).Width = 20;
                        col++; worksheet.Cells[row, col].Value = "Chi phí"; worksheet.Column(col).Width = 20;
                        for (int i = 1; i <= col; i++)
                            ExcelHelper.CreateCellStyle(worksheet, row, i, row, i, true, true, ExcelHelper.ColorGreen, ExcelHelper.ColorWhite, 0, "");
                        row++;
                        int stt = 1;
                        foreach (var item in lst.ListData)
                        {
                            col = 1;
                            worksheet.Cells[row, col].Value = stt;
                            col++; worksheet.Cells[row, col].Value = item.CustomerCode;
                            col++; worksheet.Cells[row, col].Value = item.CustomerName;
                            col++; worksheet.Cells[row, col].Value = item.Credit;
                            ExcelHelper.CreateFormat(worksheet, row, col, ExcelHelper.FormatMoney);
                            col++; worksheet.Cells[row, col].Value = item.Debit;
                            ExcelHelper.CreateFormat(worksheet, row, col, ExcelHelper.FormatMoney);
                            stt++;
                            row++;
                        }

                        col = 2;
                        col++; worksheet.Cells[row, col].Value = "Tổng";
                        col++; worksheet.Cells[row, col].Value = lst.ListData.Where(c => c.Credit > 0).Sum(c => c.Credit);
                        col++; worksheet.Cells[row, col].Value = lst.ListData.Where(c => c.Debit > 0).Sum(c => c.Debit);
                    }
                    else
                    {
                        worksheet.Cells[row, col].Value = "STT";
                        col++; worksheet.Cells[row, col].Value = "Mã khách hàng"; worksheet.Column(col).Width = 20;
                        col++; worksheet.Cells[row, col].Value = "Tên khách hàng"; worksheet.Column(col).Width = 20;
                        col++; worksheet.Cells[row, col].Value = "Doanh thu"; worksheet.Column(col).Width = 20;
                        col++; worksheet.Cells[row, col].Value = "Tỉ lệ (%)"; worksheet.Column(col).Width = 20;
                        for (int i = 1; i <= col; i++)
                            ExcelHelper.CreateCellStyle(worksheet, row, i, row, i, true, true, ExcelHelper.ColorGreen, ExcelHelper.ColorWhite, 0, "");
                        row++;
                        int stt = 1;
                        foreach (var item in lst.ListData)
                        {
                            col = 1;
                            worksheet.Cells[row, col].Value = stt;
                            col++; worksheet.Cells[row, col].Value = item.CustomerCode;
                            col++; worksheet.Cells[row, col].Value = item.CustomerName;
                            col++; worksheet.Cells[row, col].Value = item.Credit;
                            ExcelHelper.CreateFormat(worksheet, row, col, ExcelHelper.FormatMoney);
                            col++; worksheet.Cells[row, col].Value = item.CreditPercent;
                            stt++;
                            row++;
                        }
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
                result = filepath;
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        [HttpPost]
        public Widget_Data Widget_Data(dynamic dynParam)
        {
            try
            {
                DateTime dtFrom = Convert.ToDateTime(dynParam.dtfrom.ToString());
                DateTime dtTo = Convert.ToDateTime(dynParam.dtto.ToString());
                int typeofchart = Convert.ToInt32(dynParam.typeofchart.ToString());
                int? customerid = null;
                try
                {
                    customerid = Convert.ToInt32(dynParam.customerid.ToString());
                }
                catch { }
                var result = new Widget_Data();
                ServiceFactory.SVSystem((ISVSystem sv) =>
                {
                    result = sv.Widget_Data(dtFrom, dtTo, typeofchart, customerid);
                });

                return result;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public MAP_Summary MAP_Summary_Data(dynamic dynParam)
        {
            try
            {
                DateTime dtfrom = Convert.ToDateTime(dynParam.dtfrom.ToString());
                DateTime dtto = Convert.ToDateTime(dynParam.dtto.ToString());
                string request = string.Empty;
                int provinceID = 0;
                int typeoflocationID = 0;
                List<int> lstCustomerID = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.lstCustomerID.ToString());
                try
                {
                    provinceID = Convert.ToInt32(dynParam.provinceID.ToString());
                }
                catch { }
                try
                {
                    typeoflocationID = Convert.ToInt32(dynParam.typeoflocationID.ToString());
                }
                catch { }

                var result = new MAP_Summary();
                ServiceFactory.SVSystem((ISVSystem sv) =>
                {
                    result = sv.MAP_Summary_Data(request, lstCustomerID, dtfrom, dtto, provinceID, typeoflocationID);
                });

                return result;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public MAP_Summary MAP_Summary_Vehicle_Data(dynamic dynParam)
        {
            try
            {
                DateTime dtfrom = Convert.ToDateTime(dynParam.dtfrom.ToString());
                DateTime dtto = Convert.ToDateTime(dynParam.dtto.ToString());
                string request = string.Empty;
                int vehicleID = 0;

                try
                {
                    vehicleID = Convert.ToInt32(dynParam.vehicleID.ToString());
                }
                catch { }

                var result = new MAP_Summary();
                ServiceFactory.SVSystem((ISVSystem sv) =>
                {
                    result = sv.MAP_Summary_Vehicle_Data(request, dtfrom, dtto, vehicleID);
                });

                return result;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public MAP_Summary_Master MAP_Summary_Master_Data(dynamic dynParam)
        {
            try
            {
                int masterID = 0;
                try
                {
                    masterID = Convert.ToInt32(dynParam.masterID.ToString());
                }
                catch { }
                var result = new MAP_Summary_Master();
                ServiceFactory.SVSystem((ISVSystem sv) =>
                {
                    result = sv.MAP_Summary_Master_Data(masterID);
                });

                return result;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public MAP_Summary_Master MAP_Summary_Master_DataList(dynamic dynParam)
        {
            try
            {
                List<int> lstMasterID = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.lstMasterID.ToString());
                var result = new MAP_Summary_Master();
                ServiceFactory.SVSystem((ISVSystem sv) =>
                {
                    result = sv.MAP_Summary_Master_DataList(lstMasterID);
                });

                return result;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public List<DTOOtherVehiclePosition> MAP_Summary_VehiclePosition_Get(dynamic dynParam)
        {
            try
            {
                string vehicleCode = dynParam.vehicleCode.ToString();
                DateTime dtfrom = Convert.ToDateTime(dynParam.dtfrom.ToString());
                DateTime dtto = Convert.ToDateTime(dynParam.dtto.ToString());
                var result = new List<DTOOtherVehiclePosition>();
                ServiceFactory.SVOther((ISVOther sv) =>
                {
                    var ListTemp = sv.VehiclePosition_Get(vehicleCode, dtfrom, dtto).OrderBy(c => c.GPSDate).Select(c => new
                    {
                        c.Lat,
                        c.Lng,
                    }).Distinct().ToList();

                    result = ListTemp.Select(c => new DTOOtherVehiclePosition
                    {
                        VehicleCode = vehicleCode,
                        Lat = c.Lat,
                        Lng = c.Lng,
                    }).ToList();
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public List<MAP_Master_Route> MAP_Summary_VehiclePosition_GetList(dynamic dynParam)
        {
            try
            {
                List<MAP_Master_Route> lstMaster = Newtonsoft.Json.JsonConvert.DeserializeObject<List<MAP_Master_Route>>(dynParam.lstMaster.ToString());
                var result = new List<MAP_Master_Route>();
                ServiceFactory.SVOther((ISVOther sv) =>
                {
                    foreach (var item in lstMaster)
                    {
                        var ListTemp = sv.VehiclePosition_Get(item.VehicleCode, item.ATD, item.ATA).OrderBy(c => c.GPSDate).Select(c => new
                        {
                            c.Lat,
                            c.Lng,
                        }).Distinct().ToList();

                        MAP_Master_Route obj = new MAP_Master_Route();
                        obj.VehicleCode = item.VehicleCode;
                        obj.ListPoint = new List<MAP_Master_Position>();
                        obj.ListPoint = ListTemp.Select(c => new MAP_Master_Position
                        {
                            Lat = c.Lat,
                            Lng = c.Lng,
                        }).ToList();

                        result.Add(obj);
                    }
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public MAP_SummaryCO MAP_SummaryCO_Data(dynamic dynParam)
        {
            try
            {
                DateTime dtfrom = Convert.ToDateTime(dynParam.dtfrom.ToString());
                DateTime dtto = Convert.ToDateTime(dynParam.dtto.ToString());
                string request = string.Empty;
                int provinceID = 0;
                int typeoflocationID = 0;
                List<int> lstCustomerID = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.lstCustomerID.ToString());
                try
                {
                    provinceID = Convert.ToInt32(dynParam.provinceID.ToString());
                }
                catch { }
                try
                {
                    typeoflocationID = Convert.ToInt32(dynParam.typeoflocationID.ToString());
                }
                catch { }

                var result = new MAP_SummaryCO();
                ServiceFactory.SVSystem((ISVSystem sv) =>
                {
                    result = sv.MAP_SummaryCO_Data(request, lstCustomerID, dtfrom, dtto, provinceID, typeoflocationID);
                });

                return result;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public MAP_SummaryCO MAP_SummaryCO_Vehicle_Data(dynamic dynParam)
        {
            try
            {
                DateTime dtfrom = Convert.ToDateTime(dynParam.dtfrom.ToString());
                DateTime dtto = Convert.ToDateTime(dynParam.dtto.ToString());
                string request = string.Empty;
                int vehicleID = 0;

                try
                {
                    vehicleID = Convert.ToInt32(dynParam.vehicleID.ToString());
                }
                catch { }

                var result = new MAP_SummaryCO();
                ServiceFactory.SVSystem((ISVSystem sv) =>
                {
                    result = sv.MAP_SummaryCO_Vehicle_Data(request, dtfrom, dtto, vehicleID);
                });

                return result;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public MAP_SummaryCO_Master MAP_SummaryCO_Master_Data(dynamic dynParam)
        {
            try
            {
                int masterID = 0;
                try
                {
                    masterID = Convert.ToInt32(dynParam.masterID.ToString());
                }
                catch { }
                var result = new MAP_SummaryCO_Master();
                ServiceFactory.SVSystem((ISVSystem sv) =>
                {
                    result = sv.MAP_SummaryCO_Master_Data(masterID);
                });

                return result;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public MAP_SummaryCO_Master MAP_SummaryCO_Master_DataList(dynamic dynParam)
        {
            try
            {
                List<int> lstMasterID = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.lstMasterID.ToString());
                var result = new MAP_SummaryCO_Master();
                ServiceFactory.SVSystem((ISVSystem sv) =>
                {
                    result = sv.MAP_SummaryCO_Master_DataList(lstMasterID);
                });

                return result;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region ORDOrder
        [HttpPost]
        public DTOResult ORDOrder_List(dynamic dynParam)
        {
            try
            {
                string request = dynParam.request.ToString();
                bool aDate = (bool)dynParam.aDate;
                DateTime fDate = (DateTime)dynParam.fDate;
                DateTime tDate = (DateTime)dynParam.tDate;
                List<int> sStatus = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.sStatus.ToString());
                var result = new DTOResult();
                ServiceFactory.SVOrder((ISVOrder sv) =>
                {
                    result = sv.ORDOrder_List(request, sStatus, fDate.Date, tDate.Date, aDate);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void ORDOrder_Copy(dynamic dynParam)
        {
            try
            {
                List<DTOORDOrder_Copy> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DTOORDOrder_Copy>>(dynParam.lst.ToString());
                ServiceFactory.SVOrder((ISVOrder sv) =>
                {
                    sv.ORDOrder_Copy(lst);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void ORDOrder_DeleteList(dynamic dynParam)
        {
            try
            {
                List<int> lstID = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.lstID.ToString());
                ServiceFactory.SVOrder((ISVOrder sv) =>
                {
                    sv.ORDOrder_DeleteList(lstID);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public List<DTOORDRoutingRefresh> ORDOrder_ToOPSCheck(dynamic dynParam)
        {
            try
            {
                List<DTOORDRoutingRefresh> result = new List<DTOORDRoutingRefresh>();
                List<int> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.lst.ToString());
                ServiceFactory.SVOrder((ISVOrder sv) =>
                {
                    result = sv.ORDOrder_ToOPSCheck(lst);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void ORDOrder_RoutingArea_Refresh(dynamic dynParam)
        {
            try
            {
                List<int> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.lst.ToString());
                ServiceFactory.SVOrder((ISVOrder sv) =>
                {
                    sv.ORDOrder_RoutingArea_Refresh(lst);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public List<DTOORDContainer_ToTender> ORDOrder_ToOPS(dynamic dynParam)
        {
            try
            {
                List<int> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.lst.ToString());
                var result = default(List<DTOORDContainer_ToTender>);
                ServiceFactory.SVOrder((ISVOrder sv) =>
                {
                    result = sv.ORDOrder_ToOPS(lst);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public List<DTOORDContainer_ToOPS> ORDOrderContainer_ToOPSCheck(dynamic dynParam)
        {
            try
            {
                List<DTOORDContainer_ToOPS> result = new List<DTOORDContainer_ToOPS>();
                List<int> data = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.data.ToString());
                ServiceFactory.SVOrder((ISVOrder sv) =>
                {
                    result = sv.ORDOrderContainer_ToOPSCheck(data);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void ORDOrderContainer_ToOPSUpdate(dynamic dynParam)
        {
            try
            {
                List<DTOORDContainer_ToOPS> data = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DTOORDContainer_ToOPS>>(dynParam.data.ToString());
                ServiceFactory.SVOrder((ISVOrder sv) =>
                {
                    sv.ORDOrderContainer_ToOPSUpdate(data);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void ORDOrder_ToTender(dynamic dynParam)
        {
            try
            {
                List<DTOORDContainer_ToTender> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DTOORDContainer_ToTender>>(dynParam.lst.ToString());
             
                ServiceFactory.SVOrder((ISVOrder sv) =>
                {
                    sv.ORDOrder_ToTender(lst);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void ORDOrder_UpdateWarning(dynamic dynParam)
        {
            try
            {
                ServiceFactory.SVOrder((ISVOrder sv) =>
                {
                    sv.ORDOrder_UpdateWarning();
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public List<AddressSearchItem> ORDOrder_Location_Create(dynamic dynParam)
        {
            try
            {
                var data = new List<AddressSearchItem>();
                List<DTOORDOrder_Import_PartnerLocation> dataLocation = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DTOORDOrder_Import_PartnerLocation>>(dynParam.dataLocation.ToString());
                ServiceFactory.SVOrder((ISVOrder sv) =>
                {
                    data = sv.ORDOrder_Excel_Location_Create(dataLocation);
                });
                foreach (var item in data)
                {
                    AddressSearchHelper.Update(item);
                }
                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOORDData_Location ORDOrder_NewLocation_Save(dynamic dynParam)
        {
            try
            {
                var result = new DTOORDData_Location();
                DTOORDOrderNewLocation dataLocation = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOORDOrderNewLocation>(dynParam.item.ToString());
                ServiceFactory.SVOrder((ISVOrder sv) =>
                {
                    result = sv.ORDOrder_NewLocation_Save(dataLocation);

                    AddressSearchHelper.Update(sv.AddressSearch_List(result.CUSLocationID));
                });

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region ORDOrder_New
        [HttpPost]
        public int ORDOrder_New_GetView(dynamic dynParam)
        {
            int serID = (int)dynParam.ServiceID;
            int transID = (int)dynParam.TransportID;

            int result = 0;
            ServiceFactory.SVOrder((ISVOrder sv) =>
            {
                result = sv.ORDOrder_GetView(serID, transID);
            });
            return result;
        }
        [HttpPost]
        public int ORDOrder_GetViewFromCAT(dynamic dynParam)
        {
            int serID = (int)dynParam.ServiceID;
            int transID = (int)dynParam.TransportID;

            int result = 0;
            ServiceFactory.SVOrder((ISVOrder sv) =>
            {
                result = sv.ORDOrder_GetViewFromCAT(serID, transID);
            });
            return result;
        }
        #endregion

        #region  ORDOrder_DN

        [HttpPost]
        public DTOResult ORDOrder_DN_List(dynamic dynParam)
        {
            try
            {
                string request = dynParam.request.ToString();
                int cusID = (int)dynParam.CusID;
                var result = new DTOResult();
                ServiceFactory.SVOrder((ISVOrder sv) =>
                {
                    result = sv.ORDOrder_DN_List(request, cusID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public string ORDOrder_DN_Rest_SO_Download(dynamic dynParam)
        {
            try
            {
                int cusID = (int)dynParam.CusID;
                bool IsAll = (bool)dynParam.IsAll;
                DateTime? dtFrom = (DateTime?)dynParam.DateFrom;
                DateTime? dtTo = (DateTime?)dynParam.DateTo;

                var result = string.Empty;
                var lst = new List<DTOORDOrderDN>();
                ServiceFactory.SVOrder((ISVOrder sv) =>
                {
                    lst = sv.ORDOrder_DN_SORest_List(cusID, IsAll, dtFrom, dtTo);
                });
                result = "/" + FolderUpload.Export + "SORemain" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx";
                if (System.IO.File.Exists(HttpContext.Current.Server.MapPath(result)))
                    System.IO.File.Delete(HttpContext.Current.Server.MapPath(result));
                FileInfo file = new FileInfo(HttpContext.Current.Server.MapPath(result));

                using (ExcelPackage package = new ExcelPackage(file))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Sheet 1");

                    int col = 0;
                    int row = 1;
                    col++; worksheet.Cells[row, col].Value = "STT"; worksheet.Column(col).Width = 5;
                    col++; worksheet.Cells[row, col].Value = "Loadlist"; worksheet.Column(col).Width = 20;
                    col++; worksheet.Cells[row, col].Value = "Số SO"; worksheet.Column(col).Width = 15;
                    col++; worksheet.Cells[row, col].Value = "Mã NPP"; worksheet.Column(col).Width = 10;
                    col++; worksheet.Cells[row, col].Value = "NPP"; worksheet.Column(col).Width = 20;
                    col++; worksheet.Cells[row, col].Value = "Địa chỉ"; worksheet.Column(col).Width = 30;
                    col++; worksheet.Cells[row, col].Value = "Tỉnh thành"; worksheet.Column(col).Width = 13;
                    col++; worksheet.Cells[row, col].Value = "Quận huyện"; worksheet.Column(col).Width = 13;
                    col++; worksheet.Cells[row, col].Value = "RouteID"; worksheet.Column(col).Width = 13;
                    col++; worksheet.Cells[row, col].Value = "Ngày gửi"; worksheet.Column(col).Width = 20;
                    col++; worksheet.Cells[row, col].Value = "Mã kho"; worksheet.Column(col).Width = 13;
                    col++; worksheet.Cells[row, col].Value = "Số lít"; worksheet.Column(col).Width = 13;
                    col++; worksheet.Cells[row, col].Value = "Số kg"; worksheet.Column(col).Width = 13;
                    col++; worksheet.Cells[row, col].Value = "Gross Weight"; worksheet.Column(col).Width = 13;
                    col++; worksheet.Cells[row, col].Value = "Ghi chú"; worksheet.Column(col).Width = 13;
                    worksheet.Cells[row, 1, row, col].Style.Font.Bold = true;
                    row++;
                    int stt = 1;
                    foreach (var item in lst)
                    {
                        col = 1;
                        worksheet.Cells[row, col].Value = stt;
                        col++; worksheet.Cells[row, col].Value = item.OrderCode;
                        col++; worksheet.Cells[row, col].Value = item.SOCode;
                        col++; worksheet.Cells[row, col].Value = item.LocationToCode;
                        col++; worksheet.Cells[row, col].Value = item.LocationToName;
                        col++; worksheet.Cells[row, col].Value = item.LocationToAddress;
                        col++; worksheet.Cells[row, col].Value = item.LocationToProvince;
                        col++; worksheet.Cells[row, col].Value = item.LocationToDistrict;
                        col++; worksheet.Cells[row, col].Value = item.EconomicZone;
                        col++; worksheet.Cells[row, col].Value = item.RequestDate;
                        ExcelHelper.CreateFormat(worksheet, row, col, ExcelHelper.FormatDMYHM);
                        col++; worksheet.Cells[row, col].Value = item.LocationFromCode;


                        col++; worksheet.Cells[row, col].Value = item.Note2;
                        col++; worksheet.Cells[row, col].Value = item.Note1;
                        col++; worksheet.Cells[row, col].Value = item.QuantityTransfer;
                        ExcelHelper.CreateFormat(worksheet, row, col, ExcelHelper.FormatNumber);
                        col++; worksheet.Cells[row, col].Value = item.Note;
                        stt++;
                        row++;
                    }
                    package.Save();
                }

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public string ORDOrder_DN_Rest_DN_Download(dynamic dynParam)
        {
            try
            {
                int cusID = (int)dynParam.CusID;
                bool IsAll = (bool)dynParam.IsAll;
                DateTime? dtFrom = (DateTime?)dynParam.DateFrom;
                DateTime? dtTo = (DateTime?)dynParam.DateTo;

                var result = string.Empty;
                var lst = new List<DTOORDOrderDN>();
                ServiceFactory.SVOrder((ISVOrder sv) =>
                {
                    lst = sv.ORDOrder_DN_DNRest_List(cusID, IsAll, dtFrom, dtTo);
                });
                result = "/" + FolderUpload.Export + "DNRemain" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx";
                if (System.IO.File.Exists(HttpContext.Current.Server.MapPath(result)))
                    System.IO.File.Delete(HttpContext.Current.Server.MapPath(result));
                FileInfo file = new FileInfo(HttpContext.Current.Server.MapPath(result));

                using (ExcelPackage package = new ExcelPackage(file))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Sheet 1");

                    int col = 0;
                    int row = 1;
                    col++; worksheet.Cells[row, col].Value = "STT"; worksheet.Column(col).Width = 5;
                    col++; worksheet.Cells[row, col].Value = "Loadlist"; worksheet.Column(col).Width = 20;
                    col++; worksheet.Cells[row, col].Value = "Số SO"; worksheet.Column(col).Width = 15;
                    col++; worksheet.Cells[row, col].Value = "Số DN"; worksheet.Column(col).Width = 15;
                    col++; worksheet.Cells[row, col].Value = "Mã NPP"; worksheet.Column(col).Width = 10;
                    col++; worksheet.Cells[row, col].Value = "NPP"; worksheet.Column(col).Width = 20;
                    col++; worksheet.Cells[row, col].Value = "Địa chỉ"; worksheet.Column(col).Width = 30;
                    col++; worksheet.Cells[row, col].Value = "Tỉnh thành"; worksheet.Column(col).Width = 13;
                    col++; worksheet.Cells[row, col].Value = "Quận huyện"; worksheet.Column(col).Width = 13;
                    col++; worksheet.Cells[row, col].Value = "RouteID"; worksheet.Column(col).Width = 13;
                    col++; worksheet.Cells[row, col].Value = "Ngày gửi"; worksheet.Column(col).Width = 20;
                    col++; worksheet.Cells[row, col].Value = "Mã kho"; worksheet.Column(col).Width = 13;
                    col++; worksheet.Cells[row, col].Value = "Số lít"; worksheet.Column(col).Width = 13;
                    col++; worksheet.Cells[row, col].Value = "Số kg"; worksheet.Column(col).Width = 13;
                    col++; worksheet.Cells[row, col].Value = "Gross Weight"; worksheet.Column(col).Width = 13;
                    col++; worksheet.Cells[row, col].Value = "Ghi chú"; worksheet.Column(col).Width = 13;
                    worksheet.Cells[row, 1, row, col].Style.Font.Bold = true;
                    row++;
                    int stt = 1;
                    foreach (var item in lst)
                    {
                        col = 1;
                        worksheet.Cells[row, col].Value = stt;
                        col++; worksheet.Cells[row, col].Value = item.OrderCode;
                        col++; worksheet.Cells[row, col].Value = item.SOCode;
                        col++; worksheet.Cells[row, col].Value = item.DNCode;
                        col++; worksheet.Cells[row, col].Value = item.LocationToCode;
                        col++; worksheet.Cells[row, col].Value = item.LocationToName;
                        col++; worksheet.Cells[row, col].Value = item.LocationToAddress;
                        col++; worksheet.Cells[row, col].Value = item.LocationToProvince;
                        col++; worksheet.Cells[row, col].Value = item.LocationToDistrict;
                        col++; worksheet.Cells[row, col].Value = item.EconomicZone;
                        col++; worksheet.Cells[row, col].Value = item.RequestDate;
                        ExcelHelper.CreateFormat(worksheet, row, col, ExcelHelper.FormatDMYHM);
                        col++; worksheet.Cells[row, col].Value = item.LocationFromCode;


                        col++; worksheet.Cells[row, col].Value = item.Note2;
                        col++; worksheet.Cells[row, col].Value = item.Note1;
                        col++; worksheet.Cells[row, col].Value = item.QuantityTransfer;
                        ExcelHelper.CreateFormat(worksheet, row, col, ExcelHelper.FormatNumber);
                        col++; worksheet.Cells[row, col].Value = item.Note;
                        stt++;
                        row++;
                    }
                    package.Save();
                }

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public string ORDOrder_DN_Rest_All_Download(dynamic dynParam)
        {
            try
            {
                int cusID = (int)dynParam.CusID;
                bool IsAll = (bool)dynParam.IsAll;
                DateTime? dtFrom = (DateTime?)dynParam.DateFrom;
                DateTime? dtTo = (DateTime?)dynParam.DateTo;

                var result = string.Empty;
                var lst = new List<DTOORDOrderDN>();
                ServiceFactory.SVOrder((ISVOrder sv) =>
                {
                    lst = sv.ORDOrder_DN_AllRest_List(cusID, IsAll, dtFrom, dtTo);
                });
                result = "/" + FolderUpload.Export + "AllRemain" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx";
                if (System.IO.File.Exists(HttpContext.Current.Server.MapPath(result)))
                    System.IO.File.Delete(HttpContext.Current.Server.MapPath(result));
                FileInfo file = new FileInfo(HttpContext.Current.Server.MapPath(result));

                using (ExcelPackage package = new ExcelPackage(file))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Sheet 1");

                    int col = 0;
                    int row = 1;
                    col++; worksheet.Cells[row, col].Value = "STT"; worksheet.Column(col).Width = 5;
                    col++; worksheet.Cells[row, col].Value = "Loadlist"; worksheet.Column(col).Width = 20;
                    col++; worksheet.Cells[row, col].Value = "Số SO"; worksheet.Column(col).Width = 15;
                    col++; worksheet.Cells[row, col].Value = "Số DN"; worksheet.Column(col).Width = 15;
                    col++; worksheet.Cells[row, col].Value = "Mã NPP"; worksheet.Column(col).Width = 10;
                    col++; worksheet.Cells[row, col].Value = "NPP"; worksheet.Column(col).Width = 20;
                    col++; worksheet.Cells[row, col].Value = "Địa chỉ"; worksheet.Column(col).Width = 30;
                    col++; worksheet.Cells[row, col].Value = "Tỉnh thành"; worksheet.Column(col).Width = 13;
                    col++; worksheet.Cells[row, col].Value = "Quận huyện"; worksheet.Column(col).Width = 13;
                    col++; worksheet.Cells[row, col].Value = "RouteID"; worksheet.Column(col).Width = 13;
                    col++; worksheet.Cells[row, col].Value = "Ngày gửi"; worksheet.Column(col).Width = 20;
                    col++; worksheet.Cells[row, col].Value = "Mã kho"; worksheet.Column(col).Width = 13;
                    col++; worksheet.Cells[row, col].Value = "Số lít"; worksheet.Column(col).Width = 13;
                    col++; worksheet.Cells[row, col].Value = "Số kg"; worksheet.Column(col).Width = 13;
                    col++; worksheet.Cells[row, col].Value = "Gross Weight"; worksheet.Column(col).Width = 13;
                    col++; worksheet.Cells[row, col].Value = "Ghi chú"; worksheet.Column(col).Width = 13;
                    worksheet.Cells[row, 1, row, col].Style.Font.Bold = true;
                    row++;
                    int stt = 1;
                    foreach (var item in lst)
                    {
                        col = 1;
                        worksheet.Cells[row, col].Value = stt;
                        col++; worksheet.Cells[row, col].Value = item.OrderCode;
                        col++; worksheet.Cells[row, col].Value = item.SOCode;
                        col++; worksheet.Cells[row, col].Value = item.DNCode;
                        col++; worksheet.Cells[row, col].Value = item.LocationToCode;
                        col++; worksheet.Cells[row, col].Value = item.LocationToName;
                        col++; worksheet.Cells[row, col].Value = item.LocationToAddress;
                        col++; worksheet.Cells[row, col].Value = item.LocationToProvince;
                        col++; worksheet.Cells[row, col].Value = item.LocationToDistrict;
                        col++; worksheet.Cells[row, col].Value = item.EconomicZone;
                        col++; worksheet.Cells[row, col].Value = item.RequestDate;
                        ExcelHelper.CreateFormat(worksheet, row, col, ExcelHelper.FormatDMYHM);
                        col++; worksheet.Cells[row, col].Value = item.LocationFromCode;

                        col++; worksheet.Cells[row, col].Value = item.Note2;
                        col++; worksheet.Cells[row, col].Value = item.Note1;
                        col++; worksheet.Cells[row, col].Value = item.QuantityTransfer;
                        ExcelHelper.CreateFormat(worksheet, row, col, ExcelHelper.FormatNumber);
                        col++; worksheet.Cells[row, col].Value = item.Note;
                        stt++;
                        row++;
                    }
                    package.Save();
                }

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public string ORDOrder_DN_DownLoadExcel(dynamic dynParam)
        {
            try
            {
                var result = string.Empty;
                var lst = new List<DTOORDOrderDN>();
                int cusID = (int)dynParam.CusID;
                string request = dynParam.request.ToString();
                ServiceFactory.SVOrder((ISVOrder sv) =>
                {
                    lst = sv.ORDOrder_DN_List(request, cusID).Data.Cast<DTOORDOrderDN>().ToList();
                });
                result = "/" + FolderUpload.Export + "export" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx";
                if (System.IO.File.Exists(HttpContext.Current.Server.MapPath(result)))
                    System.IO.File.Delete(HttpContext.Current.Server.MapPath(result));
                FileInfo exportfile = new FileInfo(HttpContext.Current.Server.MapPath(result));

                using (ExcelPackage package = new ExcelPackage(exportfile))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Sheet 1");

                    int col = 0;
                    int row = 1;
                    col++; worksheet.Cells[row, col].Value = "STT"; worksheet.Column(col).Width = 5;
                    col++; worksheet.Cells[row, col].Value = "Ngày giao hàng"; worksheet.Column(col).Width = 13;
                    col++; worksheet.Cells[row, col].Value = "Số SO"; worksheet.Column(col).Width = 8;
                    col++; worksheet.Cells[row, col].Value = "Số DN"; worksheet.Column(col).Width = 10;
                    col++; worksheet.Cells[row, col].Value = "Đơn hàng"; worksheet.Column(col).Width = 20;
                    col++; worksheet.Cells[row, col].Value = "Mã NPP"; worksheet.Column(col).Width = 10;
                    col++; worksheet.Cells[row, col].Value = "NPP"; worksheet.Column(col).Width = 20;
                    col++; worksheet.Cells[row, col].Value = "Địa chỉ"; worksheet.Column(col).Width = 30;
                    col++; worksheet.Cells[row, col].Value = "Tỉnh thành"; worksheet.Column(col).Width = 13;
                    col++; worksheet.Cells[row, col].Value = "Quận huyện"; worksheet.Column(col).Width = 13;
                    col++; worksheet.Cells[row, col].Value = "RouteID"; worksheet.Column(col).Width = 13;
                    col++; worksheet.Cells[row, col].Value = "Mã kho"; worksheet.Column(col).Width = 13;
                    col++; worksheet.Cells[row, col].Value = "S.Lượng"; worksheet.Column(col).Width = 13;
                    col++; worksheet.Cells[row, col].Value = "S.lượng thực"; worksheet.Column(col).Width = 13;
                    col++; worksheet.Cells[row, col].Value = "Số xe"; worksheet.Column(col).Width = 13;
                    col++; worksheet.Cells[row, col].Value = "Tên tài xế"; worksheet.Column(col).Width = 13;
                    col++; worksheet.Cells[row, col].Value = "SĐT tài xế"; worksheet.Column(col).Width = 13;
                    col++; worksheet.Cells[row, col].Value = "Lệnh"; worksheet.Column(col).Width = 13;
                    col++; worksheet.Cells[row, col].Value = "Ngày yêu cầu"; worksheet.Column(col).Width = 13;
                    col++; worksheet.Cells[row, col].Value = "T/g y/c giao hàng"; worksheet.Column(col).Width = 13;
                    col++; worksheet.Cells[row, col].Value = "Ngày đến kho"; worksheet.Column(col).Width = 15;
                    col++; worksheet.Cells[row, col].Value = "Ngày rời kho"; worksheet.Column(col).Width = 13;
                    col++; worksheet.Cells[row, col].Value = "T/g vào máng"; worksheet.Column(col).Width = 13;
                    col++; worksheet.Cells[row, col].Value = "T/g ra máng"; worksheet.Column(col).Width = 13;
                    col++; worksheet.Cells[row, col].Value = "Ngày đến NPP"; worksheet.Column(col).Width = 15;
                    col++; worksheet.Cells[row, col].Value = "T/g BĐ giỡ hàng"; worksheet.Column(col).Width = 13;
                    col++; worksheet.Cells[row, col].Value = "T/g KT giỡ hàng"; worksheet.Column(col).Width = 13;
                    col++; worksheet.Cells[row, col].Value = "Ghi chú"; worksheet.Column(col).Width = 13;
                    col++; worksheet.Cells[row, col].Value = "Số lít"; worksheet.Column(col).Width = 13;
                    col++; worksheet.Cells[row, col].Value = "Số kg"; worksheet.Column(col).Width = 13;
                    worksheet.Cells[row, 1, row, col].Style.Font.Bold = true;
                    row++;
                    int stt = 1;
                    foreach (var item in lst)
                    {
                        col = 1; worksheet.Cells[row, col].Value = stt;
                        col++; worksheet.Cells[row, col].Value = item.DateToLeave;
                        ExcelHelper.CreateFormat(worksheet, row, col, ExcelHelper.FormatDMYHM);
                        col++; worksheet.Cells[row, col].Value = item.SOCode;
                        col++; worksheet.Cells[row, col].Value = item.DNCode;
                        col++; worksheet.Cells[row, col].Value = item.OrderCode;
                        col++; worksheet.Cells[row, col].Value = item.LocationToCode;
                        col++; worksheet.Cells[row, col].Value = item.LocationToName;
                        col++; worksheet.Cells[row, col].Value = item.LocationToAddress;
                        col++; worksheet.Cells[row, col].Value = item.LocationToProvince;
                        col++; worksheet.Cells[row, col].Value = item.LocationToDistrict;
                        col++; worksheet.Cells[row, col].Value = item.EconomicZone;
                        col++; worksheet.Cells[row, col].Value = item.LocationFromCode;
                        col++; worksheet.Cells[row, col].Value = item.Quantity;
                        ExcelHelper.CreateFormat(worksheet, row, col, ExcelHelper.FormatNumber);
                        col++; worksheet.Cells[row, col].Value = item.QuantityTransfer;
                        ExcelHelper.CreateFormat(worksheet, row, col, ExcelHelper.FormatNumber);
                        col++; worksheet.Cells[row, col].Value = item.RegNo;
                        col++; worksheet.Cells[row, col].Value = item.DriverName;
                        col++; worksheet.Cells[row, col].Value = item.DriverTel;
                        col++; worksheet.Cells[row, col].Value = item.MasterCode;
                        col++; worksheet.Cells[row, col].Value = item.RequestDate;
                        ExcelHelper.CreateFormat(worksheet, row, col, ExcelHelper.FormatDMYHM);
                        col++; worksheet.Cells[row, col].Value = item.ETARequest;
                        ExcelHelper.CreateFormat(worksheet, row, col, ExcelHelper.FormatDMYHM);
                        col++; worksheet.Cells[row, col].Value = item.DateFromCome;
                        ExcelHelper.CreateFormat(worksheet, row, col, ExcelHelper.FormatDMYHM);
                        col++; worksheet.Cells[row, col].Value = item.DateFromLeave;
                        ExcelHelper.CreateFormat(worksheet, row, col, ExcelHelper.FormatDMYHM);
                        col++; worksheet.Cells[row, col].Value = item.DateFromLoadStart;
                        ExcelHelper.CreateFormat(worksheet, row, col, ExcelHelper.FormatDMYHM);
                        col++; worksheet.Cells[row, col].Value = item.DateFromLoadEnd;
                        ExcelHelper.CreateFormat(worksheet, row, col, ExcelHelper.FormatDMYHM);
                        col++; worksheet.Cells[row, col].Value = item.DateToCome;
                        ExcelHelper.CreateFormat(worksheet, row, col, ExcelHelper.FormatHHMM);
                        col++; worksheet.Cells[row, col].Value = item.DateToLoadStart;
                        ExcelHelper.CreateFormat(worksheet, row, col, ExcelHelper.FormatDMYHM);
                        col++; worksheet.Cells[row, col].Value = item.DateToLoadEnd;
                        ExcelHelper.CreateFormat(worksheet, row, col, ExcelHelper.FormatDMYHM);
                        col++; worksheet.Cells[row, col].Value = item.Note;
                        col++; worksheet.Cells[row, col].Value = item.Note2;
                        col++; worksheet.Cells[row, col].Value = item.Note1;
                        stt++;
                        row++;
                    }
                    package.Save();
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region ORDOrder_Excel

        [HttpPost]
        public DTOResult ORDOrder_Excel_Setting_List(dynamic dynParam)
        {
            try
            {
                string request = dynParam.request.ToString();
                var result = new DTOResult();
                ServiceFactory.SVOrder((ISVOrder sv) =>
                {
                    result = sv.ORDOrder_Excel_Setting_List(request);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public string ORDOrder_Excel_Setting_Download(dynamic dynParam)
        {
            try
            {
                string file = "";
                int templateID = (int)dynParam.templateID;
                int customerID = (int)dynParam.customerID;

                DTOCUSSettingOrder objSetting = new DTOCUSSettingOrder();
                DTOORDOrder_ImportCheck data = new DTOORDOrder_ImportCheck();
                ServiceFactory.SVOrder((ISVOrder sv) =>
                {
                    data = sv.ORDOrder_Excel_Import_Data(customerID);
                    objSetting = sv.ORDOrder_Excel_Setting_Get(templateID);
                });

                string[] aValue = { "CustomerID", "SYSCustomerID", "ID", "CreateBy", "CreateDate", "HasStock", "ListStock", "Name", "ContractID", "RowStart", "HasStockProduct",
                                        "StockID", "GroupOfProductID", "ProductID", "ListStockWithProduct", "ServiceOfOrderName", "SettingCustomerName", "TypeOfTransportModeName", "TypeOfTransportModeID", "ServiceOfOrderID", "TotalColumn" };
                List<string> sValue = new List<string>(aValue);
                Dictionary<string, string> dicName = GetDataName();
                if (objSetting != null)
                {
                    file = "/Uploads/temp/" + objSetting.Name.Replace(' ', '-') + DateTime.Now.ToString("ddMMyyyyHHmmss") + ".xlsx";
                    if (System.IO.File.Exists(HttpContext.Current.Server.MapPath(file)))
                        System.IO.File.Delete(HttpContext.Current.Server.MapPath(file));
                    FileInfo exportfile = new FileInfo(HttpContext.Current.Server.MapPath(file));
                    using (ExcelPackage package = new ExcelPackage(exportfile))
                    {
                        ExcelWorksheet worksheet = package.Workbook.Worksheets.Add(objSetting.Name);
                        if (objSetting.RowStart > 1)
                        {
                            int row = 1;
                            foreach (var prop in objSetting.GetType().GetProperties())
                            {
                                try
                                {
                                    var p = prop.Name;
                                    if (!sValue.Contains(p))
                                    {
                                        var v = (int)prop.GetValue(objSetting, null);
                                        if (v > 0)
                                        {
                                            if (dicName.ContainsKey(p))
                                                worksheet.Cells[row, v].Value = dicName[p];
                                            else
                                                worksheet.Cells[row, v].Value = p;
                                        }
                                    }
                                }
                                catch (Exception)
                                {
                                }
                            }
                            if (objSetting.HasStock && objSetting.ListStock != null && objSetting.ListStock.Count > 0)
                            {
                                foreach (var obj in objSetting.ListStock)
                                {
                                    var cusStock = data.ListStock.FirstOrDefault(c => c.CUSLocationID == obj.StockID && c.CustomerID == customerID);
                                    if (cusStock != null)
                                    {
                                        if (obj.Ton > 0)
                                            worksheet.Cells[row, obj.Ton].Value = cusStock.LocationCode + "_Tấn";
                                        if (obj.CBM > 0)
                                            worksheet.Cells[row, obj.CBM].Value = cusStock.LocationCode + "_Khối";
                                        if (obj.Quantity > 0)
                                            worksheet.Cells[row, obj.Quantity].Value = cusStock.LocationCode + "_SL";
                                    }
                                }
                            }
                            if (objSetting.HasStockProduct && objSetting.ListStockWithProduct != null && objSetting.ListStockWithProduct.Count > 0)
                            {
                                foreach (var obj in objSetting.ListStockWithProduct)
                                {
                                    var cusStock = data.ListStock.FirstOrDefault(c => c.CUSLocationID == obj.StockID && c.CustomerID == customerID);
                                    var cusGroup = data.ListGroupOfProduct.FirstOrDefault(c => c.ID == obj.GroupOfProductID && c.CUSStockID == obj.StockID);
                                    var cusProduct = data.ListProduct.FirstOrDefault(c => c.ID == obj.ProductID && c.GroupOfProductID == obj.GroupOfProductID);
                                    if (cusStock != null && cusGroup != null && cusProduct != null)
                                    {
                                        if (obj.Ton > 0)
                                            worksheet.Cells[row, obj.Ton].Value = cusStock.LocationCode + "_" + cusGroup.Code + "_" + cusProduct.Code + "_Tấn";
                                        if (obj.CBM > 0)
                                            worksheet.Cells[row, obj.CBM].Value = cusStock.LocationCode + "_" + cusGroup.Code + "_" + cusProduct.Code + "_Khối";
                                        if (obj.Quantity > 0)
                                            worksheet.Cells[row, obj.Quantity].Value = cusStock.LocationCode + "_" + cusGroup.Code + "_" + cusProduct.Code + "_SL";
                                    }
                                }
                            }
                            else
                            {
                                if (objSetting.HasContainer && objSetting.ListContainer != null && objSetting.ListContainer.Count > 0)
                                {
                                    foreach (var obj in objSetting.ListContainer)
                                    {
                                        var cusPacking = data.ListPackingCO.FirstOrDefault(c => c.ID == obj.PackingID);
                                        if (cusPacking != null)
                                        {
                                            if (obj.PackingID > 0)
                                                worksheet.Cells[row, obj.PackingCOQuantity].Value = cusPacking.Code + "_Loại Cont";
                                        }
                                    }
                                }
                            }
                        }
                        package.Save();
                    }
                }
                return file;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void ORDOrder_Excel_Location_Create(dynamic dynParam)
        {
            try
            {
                var data = new List<AddressSearchItem>();
                List<DTOORDOrder_Import_PartnerLocation> dataLocation = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DTOORDOrder_Import_PartnerLocation>>(dynParam.dataLocation.ToString());
                ServiceFactory.SVOrder((ISVOrder sv) =>
                {
                    data = sv.ORDOrder_Excel_Location_Create(dataLocation);
                });
                foreach (var item in data)
                {
                    AddressSearchHelper.Update(item);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void ORDOrder_Excel_Product_Create(dynamic dynParam)
        {
            try
            {
                List<DTOORDOrder_Import_ProductNew> data = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DTOORDOrder_Import_ProductNew>>(dynParam.data.ToString());
                ServiceFactory.SVOrder((ISVOrder sv) =>
                {
                    sv.ORDOrder_Excel_Product_Create(data);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public int ORDOrder_Excel_Import(dynamic dynParam)
        {
            try
            {
                var result = -1;
                var templateID = (int)dynParam.TemplateID;
                var objSettingOrder = new DTOCUSSettingOrder();
                CATFile file = Newtonsoft.Json.JsonConvert.DeserializeObject<CATFile>(dynParam.File.ToString());
                List<DTOORDOrder_Import> data = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DTOORDOrder_Import>>(dynParam.Data.ToString());
                ServiceFactory.SVOrder((ISVOrder sv) =>
                {
                    result = sv.ORDOrder_Excel_Import(templateID, file, data, false);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public string ORDOrder_Excel_Export(dynamic dynParam)
        {
            try
            {
                var result = string.Empty;
                var pID = (int)dynParam.pID;
                var templateID = (int)dynParam.TemplateID;
                var objSettingOrder = new DTOCUSSettingOrder();
                var dataExport = new List<DTOORDOrder_Export>();
                CATFile file = Newtonsoft.Json.JsonConvert.DeserializeObject<CATFile>(dynParam.File.ToString());
                ServiceFactory.SVOrder((ISVOrder sv) =>
                {
                    dataExport = sv.ORDOrder_Excel_Export(templateID, pID);
                    objSettingOrder = sv.ORDOrder_Excel_Setting_Get(templateID);
                });

                string[] name = file.FileName.Split('.').Reverse().Skip(1).Reverse().ToArray();
                result = "/" + FolderUpload.Export + string.Join(".", name) + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx";
                if (System.IO.File.Exists(HttpContext.Current.Server.MapPath("/" + file.FilePath)))
                {
                    System.IO.File.Copy(HttpContext.Current.Server.MapPath("/" + file.FilePath), HttpContext.Current.Server.MapPath(result), true);

                    FileInfo exportFile = new FileInfo(HttpContext.Current.Server.MapPath(result));
                    using (var package = new ExcelPackage(exportFile))
                    {
                        ExcelWorksheet ws = ExcelHelper.GetWorksheetByIndex(package, 1);
                        if (ws != null)
                        {
                            var sValue = new List<string>(new string[]{ "CustomerID", "SYSCustomerID", "ID", "CreateBy", "CreateDate", "HasStock", "ListStock", "Name", "ContractID", "RowStart",
                                                "ServiceOfOrderName", "SettingCustomerName", "TypeOfTransportModeName", "TypeOfTransportModeID", "ServiceOfOrderID", "TotalColumn" });
                            //Empty WS
                            var iRow = ws.Dimension.End.Row;
                            if (iRow > objSettingOrder.RowStart)
                            {
                                for (var row = iRow; row >= objSettingOrder.RowStart; row--)
                                {
                                    ws.DeleteRow(row);
                                }
                            }

                            var cRow = objSettingOrder.RowStart;
                            List<string> timeProps = new List<string>(new string[] { "RequestTime", "ETARequestTime", "TimeGetEmpty", "TimeReturnEmpty" });
                            if (objSettingOrder.HasStock)
                            {
                                var dataGop = dataExport.GroupBy(c => new { c.OrderCode, c.GroupProductCode, c.Packing, c.DistributorCode, c.LocationToCode, c.ETD, c.ETA }).ToList();
                                foreach (var gop in dataGop)
                                {
                                    int max = 1;
                                    var item = gop.FirstOrDefault();
                                    foreach (var sto in objSettingOrder.ListStock)
                                    {
                                        var o = gop.Count(c => c.StockID == sto.StockID);
                                        if (o > max)
                                            max = o;
                                    }
                                    var dataContains = new List<int>();
                                    for (var i = 0; i < max; i++)
                                    {
                                        foreach (var prop in objSettingOrder.GetType().GetProperties())
                                        {
                                            try
                                            {
                                                var p = prop.Name;
                                                if (!sValue.Contains(p))
                                                {
                                                    var v = (int)prop.GetValue(objSettingOrder, null);
                                                    var val = item.GetType().GetProperty(p).GetValue(item, null);
                                                    var txt = string.Empty;
                                                    if (val != null)
                                                    {
                                                        if (val.GetType() == typeof(DateTime))
                                                        {
                                                            if (timeProps.Contains(p))
                                                            {
                                                                txt = String.Format("{0:HH:mm}", val);
                                                            }
                                                            else
                                                            {
                                                                txt = String.Format("{0:dd/MM/yyyy HH:mm}", val);
                                                            }
                                                        }
                                                        else if (val.GetType() == typeof(TimeSpan))
                                                        {
                                                            txt = val.ToString();
                                                        }
                                                        else
                                                        {
                                                            txt = val.ToString();
                                                        }
                                                    }
                                                    ws.Cells[cRow, v].Value = txt;
                                                }
                                            }
                                            catch (Exception)
                                            {
                                            }
                                        }
                                        foreach (var stock in objSettingOrder.ListStock)
                                        {
                                            var objGopInStock = gop.FirstOrDefault(c => c.StockID == stock.StockID && !dataContains.Contains(c.ID));
                                            if (objGopInStock != null)
                                            {
                                                dataContains.Add(objGopInStock.ID);
                                                foreach (var prop in stock.GetType().GetProperties())
                                                {
                                                    try
                                                    {
                                                        var p = prop.Name;
                                                        if (p != "StockID")
                                                        {
                                                            var v = (int)prop.GetValue(stock, null);
                                                            var val = objGopInStock.GetType().GetProperty(p).GetValue(objGopInStock, null);
                                                            if (val != null)
                                                            {
                                                                ws.Cells[cRow, v].Value = val.ToString();
                                                            }
                                                        }
                                                    }
                                                    catch (Exception)
                                                    {
                                                    }
                                                }
                                            }
                                        }
                                        cRow++;
                                    }
                                }
                            }
                            else if (objSettingOrder.HasStockProduct)
                            {
                                var dataGop = dataExport.GroupBy(c => new { c.OrderCode, c.DistributorCode, c.LocationToCode, c.ETD, c.ETA }).ToList();
                                foreach (var gop in dataGop)
                                {
                                    int max = 1;
                                    var item = gop.FirstOrDefault();
                                    foreach (var sto in objSettingOrder.ListStockWithProduct)
                                    {
                                        var o = gop.Count(c => c.StockID == sto.StockID && c.GroupProductID == sto.GroupOfProductID && c.PackingID == sto.ProductID);
                                        if (o > max)
                                            max = o;
                                    }
                                    var dataContains = new List<int>();
                                    for (var i = 0; i < max; i++)
                                    {
                                        foreach (var prop in objSettingOrder.GetType().GetProperties())
                                        {
                                            try
                                            {
                                                var p = prop.Name;
                                                if (!sValue.Contains(p))
                                                {
                                                    var v = (int)prop.GetValue(objSettingOrder, null);
                                                    var val = item.GetType().GetProperty(p).GetValue(item, null);
                                                    var txt = string.Empty;
                                                    if (val != null)
                                                    {
                                                        if (val.GetType() == typeof(DateTime))
                                                        {
                                                            if (timeProps.Contains(p))
                                                            {
                                                                txt = String.Format("{0:HH:mm}", val);
                                                            }
                                                            else
                                                            {
                                                                txt = String.Format("{0:dd/MM/yyyy HH:mm}", val);
                                                            }
                                                        }
                                                        else if (val.GetType() == typeof(TimeSpan))
                                                        {
                                                            txt = val.ToString();
                                                        }
                                                        else
                                                        {
                                                            txt = val.ToString();
                                                        }
                                                    }
                                                    ws.Cells[cRow, v].Value = txt;
                                                }
                                            }
                                            catch (Exception)
                                            {
                                            }
                                        }
                                        foreach (var stock in objSettingOrder.ListStockWithProduct)
                                        {
                                            var objGopInStock = gop.FirstOrDefault(c => c.StockID == stock.StockID && c.GroupProductID == stock.GroupOfProductID && c.PackingID == stock.ProductID && !dataContains.Contains(c.ID));
                                            if (objGopInStock != null)
                                            {
                                                dataContains.Add(objGopInStock.ID);
                                                foreach (var prop in stock.GetType().GetProperties())
                                                {
                                                    try
                                                    {
                                                        var p = prop.Name;
                                                        if (p != "StockID" && p != "GroupOfProductID" && p != "ProductID")
                                                        {
                                                            var v = (int)prop.GetValue(stock, null);
                                                            var val = objGopInStock.GetType().GetProperty(p).GetValue(objGopInStock, null);
                                                            if (val != null)
                                                            {
                                                                ws.Cells[cRow, v].Value = val.ToString();
                                                            }
                                                        }
                                                    }
                                                    catch (Exception)
                                                    {
                                                    }
                                                }
                                            }
                                        }
                                        cRow++;
                                    }
                                }
                            }
                            else
                            {
                                if (objSettingOrder.HasContainer)
                                {

                                }
                                else
                                {
                                    foreach (var item in dataExport)
                                    {
                                        item.Quantity_SKU = item.Quantity;
                                        item.Ton_SKU = item.Ton;
                                        item.CBM_SKU = item.CBM;
                                        foreach (var prop in objSettingOrder.GetType().GetProperties())
                                        {
                                            try
                                            {
                                                var p = prop.Name;
                                                if (!sValue.Contains(p))
                                                {
                                                    var v = (int)prop.GetValue(objSettingOrder, null);
                                                    var val = item.GetType().GetProperty(p).GetValue(item, null);
                                                    var txt = string.Empty;
                                                    if (val != null)
                                                    {
                                                        if (val.GetType() == typeof(DateTime))
                                                        {
                                                            if (timeProps.Contains(p))
                                                            {
                                                                txt = String.Format("{0:HH:mm}", val);
                                                            }
                                                            else
                                                            {
                                                                txt = String.Format("{0:dd/MM/yyyy HH:mm}", val);
                                                            }
                                                        }
                                                        else if (val.GetType() == typeof(TimeSpan))
                                                        {
                                                            txt = val.ToString();
                                                        }
                                                        else
                                                        {
                                                            txt = val.ToString();
                                                        }
                                                    }
                                                    ws.Cells[cRow, v].Value = txt;
                                                }
                                            }
                                            catch (Exception)
                                            {
                                            }
                                        }
                                        cRow++;
                                    }
                                }
                            }
                            package.Save();
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
        public List<DTOORDOrder_Import> ORDOrder_Excel_Check(dynamic dynParam)
        {
            try
            {
                string file = "/" + dynParam.file.ToString();

                int templateID = (int)dynParam.templateID;
                int customerID = (int)dynParam.customerID;
                int SYSCustomerID = -1;
                var dataRes = new List<DTOORDOrder_Import>();

                DTOCUSSettingOrder objSetting = new DTOCUSSettingOrder();
                List<DTOCUSSettingOrderCode> dataSettingCode = new List<DTOCUSSettingOrderCode>();
                DTOORDOrder_ImportCheck data = new DTOORDOrder_ImportCheck();

                ServiceFactory.SVOrder((ISVOrder sv) =>
                {
                    data = sv.ORDOrder_Excel_Import_Data(customerID);
                    objSetting = sv.ORDOrder_Excel_Setting_Get(templateID);
                    dataSettingCode = sv.ORDOrder_Excel_Setting_Code_Get();
                });
                SYSCustomerID = data.SYSCustomerID;
                if (objSetting != null)
                {
                    //Check các required.
                    ORDOrder_Excel_ValidateSetting(objSetting);

                    string[] aValue = { "CustomerID", "SYSCustomerID", "ID", "CreateBy", "CreateDate", "HasStock", "ListStock", "Name", "ContractID", "RowStart", "HasStockProduct",
                                         "StockID", "GroupOfProductID", "ProductID", "ListStockWithProduct", "ServiceOfOrderName", "SettingCustomerName", "TypeOfTransportModeName", "TypeOfTransportModeID", "ServiceOfOrderID", "TotalColumn" };
                    var sValue = new List<string>(aValue);

                    using (System.IO.FileStream fs = new System.IO.FileStream(HttpContext.Current.Server.MapPath(file), System.IO.FileMode.Open, System.IO.FileAccess.Read))
                    {
                        using (var package = new ExcelPackage(fs))
                        {
                            ExcelWorksheet worksheet = ExcelHelper.GetWorksheetByIndex(package, 1);
                            if (worksheet != null)
                            {
                                int row = 0, sortOrder = 1;
                                for (row = objSetting.RowStart; row <= worksheet.Dimension.End.Row; row++)
                                {
                                    int serviceID = -1;
                                    int svID = -1;
                                    int transportID = -1;
                                    int tmID = -1;
                                    int ctID = -1;
                                    int ctTermID = -1;
                                    var excelError = new List<string>();

                                    var excelInput = GetDataValue(worksheet, objSetting, row, sValue);
                                    if (excelInput.Count(c => !string.IsNullOrEmpty(c.Value)) > 0)
                                    {
                                        ////Nếu điểm giao trống => Break
                                        if (string.IsNullOrEmpty(excelInput["LocationToAddress"]) && string.IsNullOrEmpty(excelInput["LocationToCode"])
                                            && string.IsNullOrEmpty(excelInput["LocationToCodeName"]) && string.IsNullOrEmpty(excelInput["LocationToName"]))
                                            throw new Exception("Dòng " + row + " không có điểm giao.");

                                        #region Check TransportMode && ServiceOfOrder

                                        if (objSetting.TypeOfTransportModeID > 0)
                                        {
                                            tmID = objSetting.TypeOfTransportModeID;
                                            var objTM = data.ListTransportMode.FirstOrDefault(c => c.ID == tmID);
                                            if (objTM != null)
                                                transportID = objTM.TransportModeID;
                                        }
                                        else
                                        {
                                            if (objSetting.TypeOfTransportMode < 1)
                                                throw new Exception("Chưa thiết lập cột loại vận chuyển.");

                                            var str = excelInput["TypeOfTransportMode"].Trim().ToLower();
                                            if (!string.IsNullOrEmpty(str))
                                            {
                                                var objTM = data.ListTransportMode.FirstOrDefault(c => c.Code.ToLower() == str.Trim().ToLower());
                                                if (objTM != null)
                                                {
                                                    tmID = objTM.ID;
                                                    transportID = objTM.TransportModeID;
                                                }
                                                else
                                                    throw new Exception("Dòng [" + row + "], loại vận chuyển [" + str + "]  không xác định.");
                                            }
                                            else
                                            {
                                                throw new Exception("Dòng [" + row + "] không xác định loại vận chuyển.");
                                            }
                                        }

                                        if (objSetting.ServiceOfOrderID > 0)
                                        {
                                            svID = objSetting.ServiceOfOrderID;
                                            var objSV = data.ListServiceOfOrder.FirstOrDefault(c => c.ID == svID);
                                            if (objSV != null)
                                                serviceID = objSV.ServiceOfOrderID;
                                        }
                                        else
                                        {
                                            if (objSetting.ServiceOfOrder < 1)
                                                throw new Exception("Chưa thiết lập cột dịch vụ vận chuyển.");

                                            var str = excelInput["ServiceOfOrder"].Trim().ToLower();
                                            if (!string.IsNullOrEmpty(str))
                                            {
                                                var objSV = data.ListServiceOfOrder.FirstOrDefault(c => c.Code.ToLower() == str.Trim().ToLower());
                                                if (objSV != null)
                                                {
                                                    svID = objSV.ID;
                                                    serviceID = objSV.ServiceOfOrderID;
                                                }
                                                else
                                                    throw new Exception("Dòng [" + row + "], dịch vụ vận chuyển [" + str + "]  không xác định.");
                                            }
                                            else
                                            {
                                                throw new Exception("Dòng [" + row + "] không xác định dịch vụ vận chuyển.");
                                            }
                                        }

                                        if (transportID == iFTL && objSetting.GroupVehicle < 1)
                                            throw new Exception("Chưa thiết lập cột loại xe.");

                                        #endregion

                                        //Xe tải
                                        if ((transportID == iFTL || transportID == iLTL) && serviceID == iLO)
                                        {
                                            #region ĐH xe tải
                                            var cusID = -1;
                                            var cusCode = string.Empty;
                                            var cusSKU = false;

                                            #region Check tgian
                                            DateTime? requestDate = null;
                                            DateTime? eTD = null;
                                            DateTime? eTA = null;
                                            DateTime? eTARequest = null;
                                            DateTime? eTDRequest = null;

                                            if (objSetting.RequestDate > 0)
                                            {
                                                try
                                                {
                                                    requestDate = ExcelHelper.ValueToDate(excelInput["RequestDate"]);
                                                }
                                                catch
                                                {
                                                    try
                                                    {
                                                        requestDate = Convert.ToDateTime(excelInput["RequestDate"], new CultureInfo("vi-VN"));
                                                    }
                                                    catch { }
                                                }
                                                if (objSetting.RequestTime > 0 && requestDate != null)
                                                {
                                                    if (!string.IsNullOrEmpty(excelInput["RequestTime"]))
                                                    {
                                                        try
                                                        {
                                                            requestDate = requestDate.Value.Date.Add(TimeSpan.Parse(excelInput["RequestTime"]));
                                                        }
                                                        catch
                                                        {
                                                            excelError.Add("Sai giờ gửi yêu cầu.");
                                                        }
                                                    }
                                                }
                                            }

                                            if (objSetting.RequestDate_Time > 0)
                                            {
                                                try
                                                {
                                                    requestDate = ExcelHelper.ValueToDate(excelInput["RequestDate_Time"]);
                                                }
                                                catch
                                                {
                                                    try
                                                    {
                                                        requestDate = Convert.ToDateTime(excelInput["RequestDate_Time"], new CultureInfo("vi-VN"));
                                                    }
                                                    catch { }
                                                }
                                            }

                                            if (requestDate == null)
                                            {
                                                excelError.Add("Sai ngày gửi yêu cầu");
                                            }

                                            if (!string.IsNullOrEmpty(excelInput["ETD"]))
                                            {
                                                try
                                                {
                                                    eTD = ExcelHelper.ValueToDate(excelInput["ETD"]);
                                                }
                                                catch
                                                {
                                                    try
                                                    {
                                                        eTD = Convert.ToDateTime(excelInput["ETD"], new CultureInfo("vi-VN"));
                                                    }
                                                    catch
                                                    {
                                                        if (eTD == null)
                                                        {
                                                            excelError.Add("Sai ETD");
                                                        }
                                                    }
                                                }
                                            }
                                            else if (objSetting.ETDTime_RequestDate > 0 && requestDate != null)
                                            {
                                                if (!string.IsNullOrEmpty(excelInput["ETDTime_RequestDate"]))
                                                {
                                                    try
                                                    {
                                                        eTD = requestDate.Value.Date.Add(TimeSpan.Parse(excelInput["ETDTime_RequestDate"]));
                                                    }
                                                    catch
                                                    {
                                                        excelError.Add("Sai giờ ETD.");
                                                    }
                                                }
                                                else
                                                {
                                                    eTD = requestDate;
                                                }
                                            }

                                            if (!string.IsNullOrEmpty(excelInput["ETA"]))
                                            {
                                                try
                                                {
                                                    eTA = ExcelHelper.ValueToDate(excelInput["ETA"]);
                                                }
                                                catch
                                                {
                                                    try
                                                    {
                                                        eTA = Convert.ToDateTime(excelInput["ETA"], new CultureInfo("vi-VN"));
                                                    }
                                                    catch
                                                    {
                                                        if (eTA == null)
                                                        {
                                                            excelError.Add("Sai ETA");
                                                        }
                                                    }
                                                }
                                            }
                                            else if (objSetting.ETATime_RequestDate > 0 && requestDate != null)
                                            {
                                                if (!string.IsNullOrEmpty(excelInput["ETATime_RequestDate"]))
                                                {
                                                    try
                                                    {
                                                        eTA = requestDate.Value.Date.Add(TimeSpan.Parse(excelInput["ETATime_RequestDate"]));
                                                    }
                                                    catch
                                                    {
                                                        excelError.Add("Sai giờ ETA.");
                                                    }
                                                }
                                                else
                                                {
                                                    eTA = requestDate;
                                                }
                                            }

                                            if (!string.IsNullOrEmpty(excelInput["ETARequest"]))
                                            {
                                                try
                                                {
                                                    eTARequest = ExcelHelper.ValueToDate(excelInput["ETARequest"]);
                                                }
                                                catch
                                                {
                                                    try
                                                    {
                                                        eTARequest = Convert.ToDateTime(excelInput["ETARequest"], new CultureInfo("vi-VN"));
                                                    }
                                                    catch
                                                    {
                                                        if (eTARequest == null)
                                                        {
                                                            excelError.Add("Sai ngày y.c giao hàng.");
                                                        }
                                                    }
                                                }
                                                if (eTARequest != null && objSetting.ETARequestTime > 0)
                                                {
                                                    if (!string.IsNullOrEmpty(excelInput["ETARequestTime"]))
                                                    {
                                                        try
                                                        {
                                                            eTARequest = eTARequest.Value.Date.Add(TimeSpan.Parse(excelInput["ETARequestTime"]));
                                                        }
                                                        catch
                                                        {
                                                            excelError.Add("Sai giờ y.c giao hàng.");
                                                        }
                                                    }
                                                }
                                            }

                                            if (!string.IsNullOrEmpty(excelInput["ETDRequest"]))
                                            {
                                                try
                                                {
                                                    eTDRequest = ExcelHelper.ValueToDate(excelInput["ETDRequest"]);
                                                }
                                                catch
                                                {
                                                    try
                                                    {
                                                        eTDRequest = Convert.ToDateTime(excelInput["ETDRequest"], new CultureInfo("vi-VN"));
                                                    }
                                                    catch
                                                    {
                                                        if (eTDRequest == null)
                                                        {
                                                            excelError.Add("Sai ngày y.c đến kho.");
                                                        }
                                                    }
                                                }
                                                if (eTDRequest != null && objSetting.ETDRequestTime > 0)
                                                {
                                                    if (!string.IsNullOrEmpty(excelInput["ETDRequestTime"]))
                                                    {
                                                        try
                                                        {
                                                            eTDRequest = eTDRequest.Value.Date.Add(TimeSpan.Parse(excelInput["ETDRequestTime"]));
                                                        }
                                                        catch
                                                        {
                                                            excelError.Add("Sai giờ y.c đến kho.");
                                                        }
                                                    }
                                                }
                                            }

                                            #endregion

                                            #region Check Customer, Contract và Code

                                            if (objSetting.CustomerID == objSetting.SYSCustomerID)
                                            {
                                                if (string.IsNullOrEmpty(excelInput["CustomerCode"]))
                                                {
                                                    excelError.Add("Thiếu mã KH.");
                                                }
                                                else
                                                {
                                                    var objCheck = data.ListCustomer.FirstOrDefault(c => c.Code.Trim().ToLower() == excelInput["CustomerCode"].Trim().ToLower());
                                                    if (objCheck == null)
                                                    {
                                                        excelError.Add("KH [" + excelInput["CustomerCode"] + "] không tồn tại.");
                                                    }
                                                    else
                                                    {
                                                        cusID = objCheck.ID;
                                                        cusCode = objCheck.Code;
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                cusID = objSetting.CustomerID;
                                                var objCheck = data.ListCustomer.FirstOrDefault(c => c.ID == cusID);
                                                if (objCheck == null)
                                                {
                                                    excelError.Add("KH không tồn tại.");
                                                }
                                                else
                                                {
                                                    cusCode = objCheck.Code;
                                                }
                                            }

                                            if (cusID > 0 && objSetting.HasUseContract)
                                            {
                                                var dataC = data.ListContract.Where(c => c.CustomerID == cusID && c.TransportModeID == tmID).ToList();
                                                if (dataC.Count == 0)
                                                    excelError.Add("Không có hợp đồng.");
                                                else if (dataC.Count > 1)
                                                    excelError.Add("Có hơn 1 hợp đồng.");
                                                else
                                                {
                                                    ctID = dataC.FirstOrDefault().ID;
                                                }
                                            }

                                            if (string.IsNullOrEmpty(excelInput["OrderCode"]))
                                            {
                                                var objCode = dataSettingCode.FirstOrDefault(c => c.CustomerID == cusID);
                                                if (objCode == null || objCode.ActionType < 2) //Ko thiết lập hoặc theo cột mã.
                                                {
                                                    excelError.Add("Thiếu mã ĐH.");
                                                }
                                            }

                                            #endregion

                                            #region Check nhà phân phối

                                            var isLocationToFail = false;
                                            var sLocation = new List<AddressSearchItem>();
                                            var sPartnerLocation = new List<DTOORDOrder_Import_PartnerLocation>();

                                            var pID = -1;
                                            var toID = -1;
                                            var toCode = string.Empty;
                                            var toName = string.Empty;

                                            string dName = excelInput["DistributorName"];
                                            string dCode = excelInput["DistributorCode"];

                                            if (!string.IsNullOrEmpty(excelInput["DistributorCodeName"]))
                                            {
                                                string[] s = excelInput["DistributorCodeName"].Split('-');
                                                dCode = s[0];
                                                if (s.Length > 1)
                                                {
                                                    dName = excelInput["DistributorCodeName"].Substring(dCode.Length + 1);
                                                }
                                            }

                                            if (!string.IsNullOrEmpty(dCode))
                                            {
                                                var objCheck = data.ListDistributor.FirstOrDefault(c => !string.IsNullOrEmpty(c.PartnerCode) && c.PartnerCode.Trim().ToLower() == dCode.Trim().ToLower() && c.CustomerID == cusID);
                                                if (objCheck != null)
                                                {
                                                    pID = objCheck.CUSPartnerID;
                                                    dCode = objCheck.PartnerCode;

                                                    if (!string.IsNullOrEmpty(dName))
                                                        dName = objCheck.PartnerName;

                                                    toCode = excelInput["LocationToCode"];
                                                    toName = excelInput["LocationToName"];
                                                    if (objSetting.LocationToCodeName > 0)
                                                    {
                                                        if (!string.IsNullOrEmpty(excelInput["LocationToCodeName"]))
                                                        {
                                                            toCode = excelInput["LocationToCodeName"].Split('-').FirstOrDefault();
                                                            toName = excelInput["LocationToCodeName"].Split('-').Skip(1).FirstOrDefault();
                                                        }
                                                        else
                                                        {
                                                            toCode = string.Empty;
                                                            toName = string.Empty;
                                                        }
                                                    }
                                                    if (objSetting.SuggestLocationToCode == true)
                                                        toCode = string.Empty;

                                                    //Tìm theo code
                                                    var objTo = data.ListDistributorLocation.FirstOrDefault(c => c.CusPartID == pID && c.LocationCode.Trim().ToLower() == toCode.Trim().ToLower());
                                                    if (objTo != null)
                                                    {
                                                        toID = objTo.CUSLocationID;
                                                        var objSearch = new AddressSearchItem();
                                                        objSearch.CUSLocationID = toID;
                                                        objSearch.CustomerID = cusID;
                                                        objSearch.LocationCode = objTo.LocationCode;
                                                        objSearch.Address = objTo.Address;
                                                        objSearch.CUSPartnerID = pID;
                                                        sLocation.Add(objSearch);
                                                    }
                                                    else
                                                    {
                                                        //Tìm và gợi ý địa chỉ.
                                                        try
                                                        {
                                                            int total = 0;
                                                            sLocation = AddressSearchHelper.Search(cusID, pID, excelInput["EconomicZone"], excelInput["LocationToAddress"], 0, 100, ref total);
                                                            if (sLocation.Count == 0)
                                                            {
                                                                excelError.Add("Địa chỉ (" + excelInput["LocationToAddress"] + ") ko tồn tại");
                                                                isLocationToFail = true;
                                                            }
                                                            else if (sLocation[0].Address != excelInput["LocationToAddress"])
                                                            {
                                                                var flag = true;
                                                                for (int i = 1; i < sLocation.Count; i++)
                                                                {
                                                                    if (sLocation[i].Address == excelInput["LocationToAddress"])
                                                                    {
                                                                        var o = sLocation[i];
                                                                        sLocation.RemoveAt(i);
                                                                        sLocation.Insert(0, o);
                                                                        flag = false;
                                                                        break;
                                                                    }
                                                                }
                                                                isLocationToFail = flag;
                                                            }
                                                        }
                                                        catch (Exception)
                                                        {
                                                            isLocationToFail = true;
                                                            excelError.Add("Địa chỉ (" + excelInput["LocationToAddress"] + ") ko tồn tại");
                                                        }
                                                    }

                                                    DTOORDOrder_Import_PartnerLocation objPartner = new DTOORDOrder_Import_PartnerLocation();
                                                    objPartner.PartnerID = 0;
                                                    if (pID > 0)
                                                        objPartner.PartnerID = pID;
                                                    objPartner.CustomerID = cusID;
                                                    objPartner.PartnerCode = dCode;
                                                    objPartner.PartnerName = dName;
                                                    objPartner.LocationAddress = excelInput["LocationToAddress"];
                                                    objPartner.EconomicZone = excelInput["EconomicZone"];
                                                    objPartner.RoutingAreaCode = excelInput["RoutingAreaCode"];
                                                    objPartner.RouteDescription = excelInput["LocationToNote"];
                                                    objPartner.LocationCode = toCode;

                                                    sPartnerLocation.Add(objPartner);

                                                    if (!string.IsNullOrEmpty(excelInput["EconomicZone"]))
                                                    {
                                                        var objCus = data.ListCustomer.FirstOrDefault(c => c.ID == cusID);
                                                        if (objCus != null && objCus.IsFindEconomicZone == true)
                                                        {
                                                            var objRoute = data.ListRoute.FirstOrDefault(c => c.CustomerID == cusID && c.Code.Trim().ToLower() == excelInput["EconomicZone"].Trim().ToLower());
                                                            if (objRoute != null && objRoute.RoutingAreaToID > 0)
                                                            {
                                                                var objRouteArea = data.ListRouteArea.FirstOrDefault(c => c.ProvinceID > 0 && c.DistrictID > 0 && c.RoutingAreaID == objRoute.RoutingAreaToID);
                                                                if (objRouteArea == null)
                                                                    objRouteArea = data.ListRouteArea.FirstOrDefault(c => c.ProvinceID > 0 && c.RoutingAreaID == objRoute.RoutingAreaToID);
                                                                if (objRouteArea != null)
                                                                {
                                                                    objPartner.ProvinceID = objRouteArea.ProvinceID.Value;
                                                                    if (objRouteArea.DistrictID.HasValue)
                                                                        objPartner.DistrictID = objRouteArea.DistrictID.Value;
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    isLocationToFail = true;
                                                    excelError.Add("Npp [" + dCode + "] không tồn tại.");
                                                    toCode = excelInput["LocationToCode"];
                                                    toName = excelInput["LocationToName"];
                                                }
                                            }
                                            else
                                            {
                                                excelError.Add("Npp không xác định.");
                                                toCode = excelInput["LocationToCode"];
                                                toName = excelInput["LocationToName"];
                                            }

                                            #endregion

                                            #region Check Cung đường

                                            int? cusRoutingID = null;
                                            if (objSetting.RoutingCode > 0)
                                            {
                                                if (!string.IsNullOrEmpty(excelInput["RoutingCode"]))
                                                {
                                                    var objRoute = data.ListRoute.FirstOrDefault(c => c.CustomerID == cusID && c.Code.Trim().ToLower() == excelInput["RoutingCode"].Trim().ToLower());
                                                    if (objRoute != null)
                                                    {
                                                        cusRoutingID = objRoute.ID;
                                                    }
                                                }
                                            }

                                            #endregion

                                            #region Check sản lượng, kho, nhóm sản phẩm và đơn vị tính, loại xe, nhiệt độ
                                            var sProductNew = new List<DTOORDOrder_Import_ProductNew>();
                                            //Dictionary quantity theo kho. [Q = Quantity]
                                            Dictionary<int, Dictionary<int, double>> dicQ = new Dictionary<int, Dictionary<int, double>>();
                                            //Dictionary chi tiết kho. [L = Location]
                                            Dictionary<int, DTOORDData_Location> dicL = new Dictionary<int, DTOORDData_Location>();
                                            //Dictionary chi tiết nhóm sản phẩm đầu tiên/chỉ định trong kho. [GS = GroupProductInStock]
                                            Dictionary<int, DTOORDData_GroupProduct> dicGS = new Dictionary<int, DTOORDData_GroupProduct>();

                                            //Dictionary quantity theo kho-nhóm hàng-hàng hóa. [QP = QuantityProduct]
                                            Dictionary<string, Dictionary<int, double>> dicQP = new Dictionary<string, Dictionary<int, double>>();

                                            //Nếu thiết lập kho theo cột, check kho, lấy sản lượng theo excel.
                                            if (objSetting.HasStock)
                                            {
                                                if (objSetting.ListStock == null || objSetting.ListStock.Count == 0)
                                                    throw new Exception("Chưa thiết lập kho [Hiện kho].");

                                                foreach (var stock in objSetting.ListStock)
                                                {
                                                    int sID = stock.StockID;
                                                    var objCheck = data.ListStock.FirstOrDefault(c => c.CUSLocationID == sID && c.CustomerID == cusID);
                                                    if (objCheck == null)
                                                    {
                                                        throw new Exception("Kho thiết lập không xác định.");
                                                    }
                                                    else
                                                    {
                                                        dicL.Add(sID, objCheck);
                                                    }

                                                    Dictionary<int, double> dicV = new Dictionary<int, double>();
                                                    var dicS = GetDataValue(worksheet, stock, row, sValue);
                                                    if (!dicS.Values.All(c => string.IsNullOrEmpty(c)))
                                                    {
                                                        try
                                                        {
                                                            if (!string.IsNullOrEmpty(dicS["Ton"]))
                                                                dicV.Add(1, Convert.ToDouble(dicS["Ton"]));
                                                            else
                                                                dicV.Add(1, 0);
                                                        }
                                                        catch
                                                        {
                                                            dicV.Add(1, 0);
                                                            if (!excelError.Contains("Sai tấn."))
                                                                excelError.Add("Sai tấn.");
                                                        }
                                                        try
                                                        {
                                                            if (!string.IsNullOrEmpty(dicS["CBM"]))
                                                                dicV.Add(2, Convert.ToDouble(dicS["CBM"]));
                                                            else
                                                                dicV.Add(2, 0);
                                                        }
                                                        catch
                                                        {
                                                            dicV.Add(2, 0);
                                                            if (!excelError.Contains("Sai số khối."))
                                                                excelError.Add("Sai số khối.");
                                                        }
                                                        try
                                                        {
                                                            if (!string.IsNullOrEmpty(dicS["Quantity"]))
                                                                dicV.Add(3, Convert.ToDouble(dicS["Quantity"]));
                                                            else
                                                                dicV.Add(3, 0);
                                                        }
                                                        catch
                                                        {
                                                            dicV.Add(3, 0);
                                                            if (!excelError.Contains("Sai số lượng."))
                                                                excelError.Add("Sai số lượng.");
                                                        }
                                                        dicQ.Add(sID, dicV);
                                                    }
                                                }

                                                if (dicQ.Count == 0)
                                                {
                                                    excelError.Add("Thiếu sản lượng.");
                                                }
                                            }
                                            else if (objSetting.HasStockProduct)
                                            {
                                                if (objSetting.ListStockWithProduct == null || objSetting.ListStockWithProduct.Count == 0)
                                                    throw new Exception("Chưa thiết lập kho và nhóm hàng [Hiện Kho-Hàng].");

                                                foreach (var stock in objSetting.ListStockWithProduct)
                                                {
                                                    var objCheck = data.ListStock.FirstOrDefault(c => c.CUSLocationID == stock.StockID && c.CustomerID == cusID);
                                                    if (objCheck == null)
                                                    {
                                                        throw new Exception("Kho thiết lập không xác định.");
                                                    }
                                                    else
                                                    {
                                                        var objGop = data.ListGroupOfProduct.FirstOrDefault(c => c.CUSStockID == stock.StockID && c.ID == stock.GroupOfProductID);
                                                        if (objGop == null)
                                                        {
                                                            throw new Exception("Kho thiết lập không xác định nhóm hàng. Kho [" + objCheck.LocationName + "]");
                                                        }
                                                        else
                                                        {
                                                            var objPro = data.ListProduct.FirstOrDefault(c => c.GroupOfProductID == objGop.ID && c.CustomerID == cusID);
                                                            if (objPro == null)
                                                            {
                                                                throw new Exception("Kho thiết lập không xác định hàng hóa. Nhóm [" + objGop.GroupName + "]");
                                                            }
                                                        }
                                                    }

                                                    Dictionary<int, double> dicV = new Dictionary<int, double>();
                                                    var dicS = GetDataValue(worksheet, stock, row, sValue);
                                                    if (!dicS.Values.All(c => string.IsNullOrEmpty(c)))
                                                    {
                                                        try
                                                        {
                                                            if (!string.IsNullOrEmpty(dicS["Ton"]))
                                                                dicV.Add(1, Convert.ToDouble(dicS["Ton"]));
                                                            else
                                                                dicV.Add(1, 0);
                                                        }
                                                        catch
                                                        {
                                                            dicV.Add(1, 0);
                                                            if (!excelError.Contains("Sai tấn."))
                                                                excelError.Add("Sai tấn.");
                                                        }
                                                        try
                                                        {
                                                            if (!string.IsNullOrEmpty(dicS["CBM"]))
                                                                dicV.Add(2, Convert.ToDouble(dicS["CBM"]));
                                                            else
                                                                dicV.Add(2, 0);
                                                        }
                                                        catch
                                                        {
                                                            dicV.Add(2, 0);
                                                            if (!excelError.Contains("Sai số khối."))
                                                                excelError.Add("Sai số khối.");
                                                        }
                                                        try
                                                        {
                                                            if (!string.IsNullOrEmpty(dicS["Quantity"]))
                                                                dicV.Add(3, Convert.ToDouble(dicS["Quantity"]));
                                                            else
                                                                dicV.Add(3, 0);
                                                        }
                                                        catch
                                                        {
                                                            dicV.Add(3, 0);
                                                            if (!excelError.Contains("Sai số lượng."))
                                                                excelError.Add("Sai số lượng.");
                                                        }
                                                        var key = stock.StockID + "-" + stock.GroupOfProductID + "-" + stock.ProductID;
                                                        dicQP.Add(key, dicV);
                                                    }
                                                }
                                            }
                                            //Mỗi dòng 1 kho, check kho, lấy sản lượng theo excel.
                                            else
                                            {
                                                int sID = -1;
                                                if (objSetting.LocationFromCode < 1 && objSetting.LocationFromCodeName < 1)
                                                {
                                                    if (data.ListStock.Count(c => c.CustomerID == cusID) == 1)
                                                    {
                                                        var objCheck = data.ListStock.FirstOrDefault(c => c.CustomerID == cusID);

                                                        sID = objCheck.CUSLocationID;
                                                        dicL.Add(sID, objCheck);
                                                    }
                                                    else
                                                    {
                                                        throw new Exception("Chưa thiết lập điểm bốc hàng [LocationFromCode].");
                                                    }
                                                }
                                                else
                                                {
                                                    var sCode = excelInput["LocationFromCode"];
                                                    if (objSetting.LocationFromCodeName > 0)
                                                    {
                                                        if (!string.IsNullOrEmpty(excelInput["LocationFromCodeName"]))
                                                        {
                                                            sCode = excelInput["LocationFromCodeName"].Split('-').FirstOrDefault();
                                                        }
                                                        else
                                                        {
                                                            sCode = string.Empty;
                                                        }
                                                    }
                                                    var objCheck = data.ListStock.FirstOrDefault(c => c.CustomerID == cusID && c.LocationCode.ToLower().Trim() == sCode.ToLower().Trim());
                                                    if (objCheck != null)
                                                    {
                                                        sID = objCheck.CUSLocationID;
                                                        dicL.Add(sID, objCheck);
                                                    }
                                                    else
                                                    {
                                                        excelError.Add("Kho [" + sCode + "] không tồn tại.");
                                                        dicL.Add(-1, new DTOORDData_Location());
                                                    }
                                                }

                                                if (!string.IsNullOrEmpty(excelInput["Ton_SKU"]) || !string.IsNullOrEmpty(excelInput["CBM_SKU"]) || !string.IsNullOrEmpty(excelInput["Quantity_SKU"]))
                                                {
                                                    cusSKU = true;
                                                    Dictionary<int, double> dicV = new Dictionary<int, double>();
                                                    try
                                                    {
                                                        if (!string.IsNullOrEmpty(excelInput["Ton_SKU"]))
                                                            dicV.Add(1, Convert.ToDouble(excelInput["Ton_SKU"]));
                                                        else
                                                            dicV.Add(1, 0);
                                                    }
                                                    catch
                                                    {
                                                        dicV.Add(1, 0);
                                                        if (!excelError.Contains("Sai tấn."))
                                                            excelError.Add("Sai tấn.");
                                                    }
                                                    try
                                                    {
                                                        if (!string.IsNullOrEmpty(excelInput["CBM_SKU"]))
                                                            dicV.Add(2, Convert.ToDouble(excelInput["CBM_SKU"]));
                                                        else
                                                            dicV.Add(2, 0);
                                                    }
                                                    catch
                                                    {
                                                        dicV.Add(2, 0);
                                                        if (!excelError.Contains("Sai số khối."))
                                                            excelError.Add("Sai số khối.");
                                                    }
                                                    try
                                                    {
                                                        if (!string.IsNullOrEmpty(excelInput["Quantity_SKU"]))
                                                            dicV.Add(3, Convert.ToDouble(excelInput["Quantity_SKU"]));
                                                        else
                                                            dicV.Add(3, 0);
                                                    }
                                                    catch
                                                    {
                                                        dicV.Add(3, 0);
                                                        if (!excelError.Contains("Sai số lượng."))
                                                            excelError.Add("Sai số lượng.");
                                                    }
                                                    dicQ.Add(sID, dicV);
                                                }
                                                else
                                                {
                                                    Dictionary<int, double> dicV = new Dictionary<int, double>();
                                                    try
                                                    {
                                                        if (!string.IsNullOrEmpty(excelInput["Ton"]))
                                                            dicV.Add(1, Convert.ToDouble(excelInput["Ton"]));
                                                        else
                                                            dicV.Add(1, 0);
                                                    }
                                                    catch
                                                    {
                                                        dicV.Add(1, 0);
                                                        if (!excelError.Contains("Sai tấn."))
                                                            excelError.Add("Sai tấn.");
                                                    }
                                                    try
                                                    {
                                                        if (!string.IsNullOrEmpty(excelInput["CBM"]))
                                                            dicV.Add(2, Convert.ToDouble(excelInput["CBM"]));
                                                        else
                                                            dicV.Add(2, 0);
                                                    }
                                                    catch
                                                    {
                                                        dicV.Add(2, 0);
                                                        if (!excelError.Contains("Sai số khối."))
                                                            excelError.Add("Sai số khối.");
                                                    }
                                                    try
                                                    {
                                                        if (!string.IsNullOrEmpty(excelInput["Quantity"]))
                                                            dicV.Add(3, Convert.ToDouble(excelInput["Quantity"]));
                                                        else
                                                            dicV.Add(3, 0);
                                                    }
                                                    catch
                                                    {
                                                        dicV.Add(3, 0);
                                                        if (!excelError.Contains("Sai số lượng."))
                                                            excelError.Add("Sai số lượng.");
                                                    }
                                                    dicQ.Add(sID, dicV);
                                                }
                                            }

                                            string strGopCode = string.Empty;
                                            //Dictionary Product theo GroupProduct. [P = Product] - Key: GroupOfProductID
                                            Dictionary<int, int> dicP = new Dictionary<int, int>();
                                            //Dictionary ProductCode theo GroupProduct. [PCode = ProductCode] - Key: GroupOfProductID
                                            Dictionary<int, string> dicPCode = new Dictionary<int, string>();
                                            //Dictionary Product PackingType theo GroupProduct. [Packing = Product Packing] - Key: GroupOfProductID
                                            Dictionary<int, int> dicPacking = new Dictionary<int, int>();

                                            //Nếu không có cột nhóm SP, check sản phẩm ko nhóm (ProductCodeWithoutGroup)
                                            //Nếu không có cột nhóm SP, kiểm tra kho có duy nhất nhóm SP => Lấy
                                            if (objSetting.GroupProductCode == 0 && objSetting.GroupProductCodeNotUnicode == 0)
                                            {
                                                if (objSetting.ProductCodeWithoutGroup > 0)
                                                {
                                                    if (!string.IsNullOrEmpty(excelInput["ProductCodeWithoutGroup"]))
                                                    {
                                                        var objP = data.ListProduct.FirstOrDefault(c => c.Code == excelInput["ProductCodeWithoutGroup"] && c.CustomerID == cusID);
                                                        if (objP != null)
                                                        {
                                                            foreach (var st in dicQ)
                                                            {
                                                                var objGS = data.ListGroupOfProduct.FirstOrDefault(c => c.ID == objP.GroupOfProductID && c.CUSStockID == st.Key);
                                                                if (objGS != null)
                                                                {
                                                                    strGopCode = objGS.Code;
                                                                    dicGS.Add(st.Key, objGS);
                                                                    dicP.Add(objGS.ID, objP.ID);
                                                                    dicPCode.Add(objGS.ID, objP.Code);
                                                                    dicPacking.Add(objGS.ID, objP.PackingTypeGOP);
                                                                }
                                                                else
                                                                {
                                                                    objGS = new DTOORDData_GroupProduct();
                                                                    objGS.ID = -1;
                                                                    dicGS.Add(st.Key, objGS);
                                                                    if (!dicP.ContainsKey(-1))
                                                                    {
                                                                        dicP.Add(objGS.ID, objP.ID);
                                                                        dicPCode.Add(objGS.ID, objP.Code);
                                                                        dicPacking.Add(objGS.ID, objP.PackingTypeGOP);
                                                                    }
                                                                    excelError.Add("Kho [" + dicL[st.Key].LocationCode + "] không có sản phẩm [" + excelInput["ProductCodeWithoutGroup"] + "].");
                                                                }
                                                            }
                                                        }
                                                        else
                                                        {
                                                            foreach (var st in dicQ)
                                                            {
                                                                var objGS = new DTOORDData_GroupProduct();
                                                                objGS.ID = -1;
                                                                dicGS.Add(st.Key, objGS);
                                                                if (!dicP.ContainsKey(-1))
                                                                {
                                                                    dicP.Add(objGS.ID, -1);
                                                                    dicPCode.Add(objGS.ID, excelInput["ProductCodeWithoutGroup"]);
                                                                    dicPacking.Add(objGS.ID, 1);
                                                                }
                                                            }
                                                            excelError.Add("Sản phẩm [" + excelInput["ProductCodeWithoutGroup"] + "] không tồn tại.");
                                                        }
                                                    }
                                                    else
                                                    {
                                                        foreach (var st in dicQ)
                                                        {
                                                            var objGS = new DTOORDData_GroupProduct();
                                                            objGS.ID = -1;
                                                            dicGS.Add(st.Key, objGS);
                                                            if (!dicP.ContainsKey(-1))
                                                            {
                                                                dicP.Add(objGS.ID, -1);
                                                                dicPCode.Add(objGS.ID, excelInput["ProductCodeWithoutGroup"]);
                                                                dicPacking.Add(objGS.ID, 1);
                                                            }
                                                        }
                                                        excelError.Add("Không có thông tin sản phẩm.");
                                                    }
                                                }
                                                else
                                                {
                                                    foreach (var st in dicQ)
                                                    {
                                                        var dataGS = data.ListGroupOfProduct.Where(c => c.CustomerID == cusID && c.CUSStockID == st.Key).ToList();
                                                        if (dataGS.Count == 0)
                                                        {
                                                            excelError.Add("Kho [" + dicL[st.Key].LocationCode + "] không có nhóm sản phẩm.");
                                                            dicGS.Add(st.Key, new DTOORDData_GroupProduct());
                                                        }
                                                        else if (dataGS.Count == 1)
                                                        {
                                                            var objCheck = dataGS.FirstOrDefault();
                                                            strGopCode = objCheck.Code;
                                                            dicGS.Add(st.Key, objCheck);
                                                        }
                                                        else
                                                        {
                                                            var objCheck = dataGS.FirstOrDefault(c => c.IsDefault == true);
                                                            if (objCheck != null)
                                                            {
                                                                strGopCode = objCheck.Code;
                                                                dicGS.Add(st.Key, objCheck);
                                                            }
                                                            else
                                                            {
                                                                excelError.Add("Kho [" + dicL[st.Key].LocationCode + "] có nhiều hơn 1 nhóm sản phẩm.");
                                                                dicGS.Add(st.Key, new DTOORDData_GroupProduct());
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                            //Kiểm tra nhóm SP có tồn tại + có trong kho.
                                            else if (objSetting.ProductCodeWithoutGroup == 0)
                                            {
                                                if (objSetting.GroupProductCode > 0)
                                                    strGopCode = excelInput["GroupProductCode"];
                                                else
                                                    strGopCode = StringHelper.RemoveSign4VietnameseString(excelInput["GroupProductCodeNotUnicode"]);

                                                if (!string.IsNullOrEmpty(strGopCode))
                                                {
                                                    var objGop = data.ListGroupOfProduct.FirstOrDefault(c => c.CustomerID == cusID && c.Code.Trim().ToLower() == strGopCode.Trim().ToLower());
                                                    if (objGop != null)
                                                    {
                                                        foreach (var st in dicQ)
                                                        {
                                                            if (data.ListGroupOfProduct.Count(c => c.CustomerID == cusID && c.ID == objGop.ID && c.CUSStockID == st.Key) == 0)
                                                            {
                                                                excelError.Add("Nhóm sp[" + strGopCode + "] không có trong kho " + dicL[st.Key].LocationCode + ".");
                                                                dicGS.Add(st.Key, new DTOORDData_GroupProduct());
                                                            }
                                                            else
                                                            {
                                                                var objCheck = data.ListGroupOfProduct.FirstOrDefault(c => c.CustomerID == cusID && c.ID == objGop.ID && c.CUSStockID == st.Key);
                                                                dicGS.Add(st.Key, objCheck);
                                                            }
                                                        }
                                                    }
                                                    else
                                                    {
                                                        excelError.Add("Nhóm sp[" + strGopCode + "] không tồn tại hoặc chưa thiết lập kho.");
                                                        dicGS.Add(-1, new DTOORDData_GroupProduct());
                                                    }
                                                }
                                                else
                                                {
                                                    foreach (var st in dicQ)
                                                    {
                                                        var dataGS = data.ListGroupOfProduct.Where(c => c.CustomerID == cusID && c.CUSStockID == st.Key).ToList();
                                                        if (dataGS.Count == 0)
                                                        {
                                                            excelError.Add("Nhóm sp không xác định.");
                                                            dicGS.Add(st.Key, new DTOORDData_GroupProduct());
                                                        }
                                                        else if (dataGS.Count == 1)
                                                        {
                                                            var objCheck = dataGS.FirstOrDefault();
                                                            strGopCode = objCheck.Code;
                                                            dicGS.Add(st.Key, objCheck);
                                                        }
                                                        else
                                                        {
                                                            var objCheck = dataGS.FirstOrDefault(c => c.IsDefault == true);
                                                            if (objCheck != null)
                                                            {
                                                                strGopCode = objCheck.Code;
                                                                dicGS.Add(st.Key, objCheck);
                                                            }
                                                            else
                                                            {
                                                                excelError.Add("Kho [" + dicL[st.Key].LocationCode + "] có nhiều hơn 1 nhóm sản phẩm.");
                                                                dicGS.Add(st.Key, new DTOORDData_GroupProduct());
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                if (!string.IsNullOrEmpty(excelInput["ProductCodeWithoutGroup"]))
                                                {
                                                    var objP = data.ListProduct.FirstOrDefault(c => c.Code == excelInput["ProductCodeWithoutGroup"] && c.CustomerID == cusID);
                                                    if (objP != null)
                                                    {
                                                        foreach (var st in dicQ)
                                                        {
                                                            var objGS = data.ListGroupOfProduct.FirstOrDefault(c => c.ID == objP.GroupOfProductID && c.CUSStockID == st.Key);
                                                            if (objGS != null)
                                                            {
                                                                strGopCode = objGS.Code;
                                                                dicGS.Add(st.Key, objGS);
                                                                dicP.Add(objGS.ID, objP.ID);
                                                                dicPCode.Add(objGS.ID, objP.Code);
                                                                dicPacking.Add(objGS.ID, objP.PackingTypeGOP);
                                                            }
                                                            else
                                                            {
                                                                objGS = new DTOORDData_GroupProduct();
                                                                objGS.ID = -1;
                                                                dicGS.Add(st.Key, objGS);
                                                                if (!dicP.ContainsKey(-1))
                                                                {
                                                                    dicP.Add(objGS.ID, objP.ID);
                                                                    dicPCode.Add(objGS.ID, objP.Code);
                                                                    dicPacking.Add(objGS.ID, objP.PackingTypeGOP);
                                                                }
                                                                excelError.Add("Kho [" + dicL[st.Key].LocationCode + "] không có sản phẩm [" + excelInput["ProductCodeWithoutGroup"] + "].");
                                                            }
                                                        }
                                                    }
                                                    else
                                                    {
                                                        foreach (var st in dicQ)
                                                        {
                                                            var objGS = new DTOORDData_GroupProduct();
                                                            objGS.ID = -1;
                                                            dicGS.Add(st.Key, objGS);
                                                            if (!dicP.ContainsKey(-1))
                                                            {
                                                                dicP.Add(objGS.ID, -1);
                                                                dicPCode.Add(objGS.ID, excelInput["ProductCodeWithoutGroup"]);
                                                                dicPacking.Add(objGS.ID, 1);
                                                            }
                                                        }
                                                        excelError.Add("Sản phẩm [" + excelInput["ProductCodeWithoutGroup"] + "] không tồn tại.");
                                                    }
                                                }
                                                else
                                                {
                                                    foreach (var st in dicQ)
                                                    {
                                                        var objGS = new DTOORDData_GroupProduct();
                                                        objGS.ID = -1;
                                                        dicGS.Add(st.Key, objGS);
                                                        if (!dicP.ContainsKey(-1))
                                                        {
                                                            dicP.Add(objGS.ID, -1);
                                                            dicPCode.Add(objGS.ID, excelInput["ProductCodeWithoutGroup"]);
                                                            dicPacking.Add(objGS.ID, 1);
                                                        }
                                                    }
                                                    excelError.Add("Không có thông tin sản phẩm.");
                                                }
                                            }

                                            if (objSetting.ProductCodeWithoutGroup == 0)
                                            {
                                                if (objSetting.Packing == 0 && objSetting.PackingNotUnicode == 0)
                                                {
                                                    foreach (var gop in dicGS)
                                                    {
                                                        if (!dicP.ContainsKey(gop.Value.ID))
                                                        {
                                                            var dataProduct = data.ListProduct.Where(c => c.GroupOfProductID == gop.Value.ID && c.CustomerID == cusID).ToList();
                                                            if (dataProduct.Count == 1)
                                                            {
                                                                dicP.Add(gop.Value.ID, dataProduct[0].ID);
                                                                dicPCode.Add(gop.Value.ID, dataProduct[0].Code);
                                                                dicPacking.Add(gop.Value.ID, dataProduct[0].PackingTypeGOP);
                                                            }
                                                            else
                                                            {
                                                                var objDefault = dataProduct.FirstOrDefault(c => c.IsDefault == true);
                                                                if (objDefault != null)
                                                                {
                                                                    dicP.Add(gop.Value.ID, objDefault.ID);
                                                                    dicPCode.Add(gop.Value.ID, objDefault.Code);
                                                                    dicPacking.Add(gop.Value.ID, objDefault.PackingTypeGOP);
                                                                }
                                                                else
                                                                {
                                                                    dicP.Add(gop.Value.ID, -1);
                                                                    dicPCode.Add(gop.Value.ID, "");
                                                                    dicPacking.Add(gop.Value.ID, 0);
                                                                    excelError.Add("Nhóm sp[" + (gop.Value.ID > 0 ? gop.Value.Code : strGopCode) + "] có nhiều hơn 1 ĐVT");
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    foreach (var gop in dicGS)
                                                    {
                                                        if (!dicP.ContainsKey(gop.Value.ID))
                                                        {
                                                            var dataProduct = data.ListProduct.Where(c => c.GroupOfProductID == gop.Value.ID && c.CustomerID == cusID).ToList();
                                                            if (dataProduct.Count > 0)
                                                            {
                                                                var str = string.Empty;
                                                                if (objSetting.Packing > 0)
                                                                    str = excelInput["Packing"];
                                                                else if (objSetting.PackingNotUnicode > 0)
                                                                    str = StringHelper.RemoveSign4VietnameseString(excelInput["PackingNotUnicode"]);

                                                                if (string.IsNullOrEmpty(str))
                                                                {
                                                                    var objDefault = dataProduct.FirstOrDefault(c => c.IsDefault == true);
                                                                    if (objDefault != null)
                                                                    {
                                                                        dicP.Add(gop.Value.ID, objDefault.ID);
                                                                        dicPCode.Add(gop.Value.ID, objDefault.Code);
                                                                        dicPacking.Add(gop.Value.ID, objDefault.PackingTypeGOP);
                                                                    }
                                                                    else
                                                                    {
                                                                        dicP.Add(gop.Value.ID, -1);
                                                                        dicPCode.Add(gop.Value.ID, "");
                                                                        dicPacking.Add(gop.Value.ID, 0);
                                                                        excelError.Add("Nhóm sp[" + (gop.Value.ID > 0 ? gop.Value.Code : strGopCode) + "] có nhiều hơn 1 ĐVT");
                                                                    }
                                                                }
                                                                else
                                                                {
                                                                    var product = dataProduct.FirstOrDefault(c => c.Code.ToLower().Trim() == str.ToLower().Trim());
                                                                    if (product != null)
                                                                    {
                                                                        dicP.Add(gop.Value.ID, product.ID);
                                                                        dicPCode.Add(gop.Value.ID, product.Code);
                                                                        dicPacking.Add(gop.Value.ID, product.PackingTypeGOP);
                                                                    }
                                                                    else
                                                                    {
                                                                        dicP.Add(gop.Value.ID, -1);
                                                                        dicPCode.Add(gop.Value.ID, "");
                                                                        dicPacking.Add(gop.Value.ID, 0);
                                                                        excelError.Add("Nhóm sp[" + (gop.Value.ID > 0 ? gop.Value.Code : strGopCode) + "] chưa thiết lập Hàng hóa/ĐVT [" + str + "].");

                                                                        DTOORDOrder_Import_ProductNew objProductNew = new DTOORDOrder_Import_ProductNew();
                                                                        objProductNew.GroupOfProductID = gop.Value.ID;
                                                                        objProductNew.GroupName = gop.Value.GroupName;
                                                                        objProductNew.ProductCode = str;
                                                                        objProductNew.ProductName = str;
                                                                        objProductNew.PackingID = gop.Value.DefaultPackingID;
                                                                        objProductNew.PackingName = gop.Value.DefaultPackingName;
                                                                        sProductNew.Add(objProductNew);
                                                                    }
                                                                }
                                                            }
                                                            else
                                                            {
                                                                dicP.Add(gop.Value.ID, -1);
                                                                dicPCode.Add(gop.Value.ID, "");
                                                                dicPacking.Add(gop.Value.ID, 0);
                                                                excelError.Add("Nhóm sp[" + (gop.Value.ID > 0 ? gop.Value.Code : strGopCode) + "] chưa thiết lập ĐVT.");
                                                            }
                                                        }
                                                    }
                                                }
                                            }

                                            var groupVehicleID = -1;
                                            if (transportID == iFTL)
                                            {
                                                if (!string.IsNullOrEmpty(excelInput["GroupVehicle"]))
                                                {
                                                    var objCheck = data.ListGroupOfVehicle.FirstOrDefault(c => c.Code.Trim().ToLower() == excelInput["GroupVehicle"].Trim().ToLower());
                                                    if (objCheck == null)
                                                    {
                                                        excelError.Add("Loại xe[" + excelInput["GroupVehicle"] + "] không tồn tại.");
                                                    }
                                                    else
                                                    {
                                                        groupVehicleID = objCheck.ID;
                                                    }
                                                }
                                                else
                                                {
                                                    excelError.Add("Loại xe không xác định.");
                                                }
                                            }

                                            double? tmpMin = null;
                                            double? tmpMax = null;
                                            if (objSetting.TemperatureMin > 0 && !string.IsNullOrEmpty(excelInput["TemperatureMin"]))
                                            {
                                                try
                                                {
                                                    tmpMin = Convert.ToDouble(excelInput["TemperatureMin"]);
                                                }
                                                catch
                                                {
                                                    excelError.Add("Sai nhiệt độ tối thiểu.");
                                                }
                                            }
                                            if (objSetting.TemperatureMax > 0 && !string.IsNullOrEmpty(excelInput["TemperatureMax"]))
                                            {
                                                try
                                                {
                                                    tmpMax = Convert.ToDouble(excelInput["TemperatureMax"]);
                                                }
                                                catch
                                                {
                                                    excelError.Add("Sai nhiệt độ tối đa.");
                                                }
                                            }

                                            #endregion

                                            #region Lưu dữ liệu

                                            var obj = new DTOORDOrder_Import();
                                            obj.SortOrder = sortOrder;
                                            obj.TypeOfOrderID = -(int)SYSVarType.TypeOfOrderDirect;
                                            if (ctID > 0)
                                                obj.ContractID = ctID;
                                            if (ctTermID > 0)
                                                obj.ContractTermID = ctTermID;
                                            obj.ServiceOfOrderID = svID;
                                            obj.ServiceOfOrderIDTemp = serviceID;
                                            obj.TransportModeID = tmID;
                                            obj.TransportModeIDTemp = transportID;
                                            var objTMName = data.ListTransportMode.FirstOrDefault(c => c.ID == tmID);
                                            if (objTMName != null)
                                                obj.TransportModeName = objTMName.Name;
                                            obj.CustomerID = cusID;
                                            obj.CustomerCode = cusCode;
                                            obj.IsHot = objSetting.IsHot > 0 && !string.IsNullOrEmpty(excelInput["IsHot"]) && excelInput["IsHot"].Trim().ToLower() == "true";
                                            obj.ExcelSuccess = true;
                                            obj.Note = excelInput["Note"];
                                            obj.IsLocationToFail = isLocationToFail;
                                            obj.LocationToID = toID;
                                            obj.LocationToAddress = excelInput["LocationToAddress"];
                                            if (obj.LocationToID < 0)
                                                obj.LocationToID = null;
                                            obj.PartnerID = pID;
                                            if (requestDate != null)
                                                obj.RequestDate = requestDate.Value;
                                            obj.ETARequest = eTARequest;
                                            obj.ETDRequest = eTDRequest;
                                            obj.ETD = eTD;
                                            obj.ETA = eTA;
                                            obj.Code = excelInput["OrderCode"];
                                            obj.CUSRoutingID = cusRoutingID;
                                            obj.UserDefined1 = excelInput["UserDefine1"];
                                            obj.UserDefined2 = excelInput["UserDefine2"];
                                            obj.UserDefined3 = excelInput["UserDefine3"];
                                            obj.UserDefined4 = excelInput["UserDefine4"];
                                            obj.UserDefined5 = excelInput["UserDefine5"];
                                            obj.UserDefined6 = excelInput["UserDefine6"];
                                            obj.UserDefined7 = excelInput["UserDefine7"];
                                            obj.UserDefined8 = excelInput["UserDefine8"];
                                            obj.UserDefined9 = excelInput["UserDefine9"];

                                            if (transportID == iFTL && groupVehicleID > 0)
                                                obj.GroupOfVehicleID = groupVehicleID;

                                            if (obj.ContractID == null)
                                            {
                                                try
                                                {
                                                    if (!string.IsNullOrEmpty(excelInput["PriceTOMaster"]))
                                                        obj.RoutePrice = Convert.ToDecimal(excelInput["PriceTOMaster"]);
                                                }
                                                catch (Exception)
                                                {
                                                    excelError.Add("Sai giá chuyến");
                                                }
                                            }

                                            obj.ListPartnerLocation = new List<DTOORDOrder_Import_PartnerLocation>();
                                            obj.ListPartnerLocation.AddRange(sPartnerLocation);
                                            obj.ListProduct = new List<DTOORDOrder_Import_Product>();
                                            obj.ListProductNew = new List<DTOORDOrder_Import_ProductNew>();
                                            obj.ListProductNew.AddRange(sProductNew);
                                            foreach (var dic in dicQ)
                                            {
                                                var gop = new DTOORDData_GroupProduct();
                                                try
                                                {
                                                    gop = dicGS[dic.Key];
                                                }
                                                catch
                                                {
                                                    gop.Code = excelInput["GroupProductCode"];
                                                }

                                                var objProduct = new DTOORDOrder_Import_Product();
                                                objProduct.SortOrder = sortOrder;
                                                objProduct.SOCode = excelInput["SOCode"];
                                                objProduct.DNCode = excelInput["DNCode"];
                                                objProduct.CUSRoutingID = cusRoutingID > 0 ? cusRoutingID : null;
                                                objProduct.GroupOfProductID = gop.ID;
                                                objProduct.GroupOfProductCode = gop.Code;
                                                objProduct.ProductID = dicP[gop.ID];
                                                objProduct.ProductCode = dicPCode[gop.ID];
                                                objProduct.PartnerID = pID;
                                                objProduct.PartnerCode = dCode;
                                                objProduct.PartnerName = dName;
                                                objProduct.LocationToID = toID;
                                                objProduct.HasCashCollect = !string.IsNullOrEmpty(excelInput["HasCashCollect"]) && excelInput["HasCashCollect"].Trim().ToLower() == "x";
                                                objProduct.LocationFromID = dic.Key;
                                                objProduct.LocationFromCode = dicL[dic.Key].LocationCode;
                                                objProduct.LocationFromName = dicL[dic.Key].LocationName;
                                                objProduct.LocationToCode = toCode;
                                                objProduct.LocationToName = toName;
                                                objProduct.EconomicZone = excelInput["EconomicZone"];
                                                objProduct.Note1 = excelInput["Note1"];
                                                objProduct.Note2 = excelInput["Note2"];

                                                objProduct.TempMax = tmpMax;
                                                objProduct.TempMin = tmpMin;
                                                objProduct.ETD = eTD;
                                                objProduct.ETARequest = eTARequest;
                                                objProduct.ETDRequest = eTDRequest;
                                                objProduct.ETA = eTA;
                                                if (objProduct.ETA != null && objProduct.ETD != null && objProduct.ETD >= objProduct.ETA)
                                                {
                                                    excelError.Add("Sai ràng buộc ETD-ETA.");
                                                }
                                                objProduct.ListLocationToAddress = sLocation;
                                                objProduct.LocationToAddress = excelInput["LocationToAddress"];

                                                var cusProduct = data.ListProduct.FirstOrDefault(c => c.ID == dicP[gop.ID] && c.CustomerID == cusID);
                                                if (cusProduct != null)
                                                {
                                                    if (!cusSKU)
                                                    {
                                                        objProduct.Ton = dic.Value[1];
                                                        objProduct.CBM = dic.Value[2];
                                                        objProduct.Quantity = dic.Value[3];
                                                        if (cusProduct.IsKg)
                                                            objProduct.Ton = objProduct.Ton / 1000;
                                                    }
                                                    else
                                                    {
                                                        switch (cusProduct.PackingTypeGOP)
                                                        {
                                                            case 1:
                                                                objProduct.Ton = dic.Value[1];
                                                                if (cusProduct.Weight.HasValue && cusProduct.Weight != 0)
                                                                    objProduct.Quantity = objProduct.Ton / cusProduct.Weight.Value;
                                                                if (cusProduct.IsKg)
                                                                    objProduct.Quantity = objProduct.Quantity / 1000;
                                                                if (cusProduct.CBM.HasValue)
                                                                    objProduct.CBM = objProduct.Quantity * cusProduct.CBM.Value;
                                                                break;
                                                            case 2:
                                                                objProduct.CBM = dic.Value[2];
                                                                if (cusProduct.CBM.HasValue && cusProduct.CBM != 0)
                                                                    objProduct.Quantity = objProduct.CBM / cusProduct.CBM.Value;
                                                                if (cusProduct.Weight.HasValue)
                                                                    objProduct.Ton = objProduct.Quantity * cusProduct.Weight.Value;
                                                                break;
                                                            case 3:
                                                                objProduct.Quantity = dic.Value[3];
                                                                if (cusProduct.CBM.HasValue)
                                                                    objProduct.CBM = objProduct.Quantity * cusProduct.CBM.Value;
                                                                if (cusProduct.Weight.HasValue)
                                                                    objProduct.Ton = objProduct.Quantity * cusProduct.Weight.Value;
                                                                break;
                                                            default:
                                                                break;
                                                        }
                                                        objProduct.Ton = Math.Round(objProduct.Ton, 5, MidpointRounding.AwayFromZero);
                                                        objProduct.CBM = Math.Round(objProduct.CBM, 5, MidpointRounding.AwayFromZero);
                                                        objProduct.Quantity = Math.Round(objProduct.Quantity, 5, MidpointRounding.AwayFromZero);
                                                    }
                                                }

                                                //Kiểm tra thông tin sản lượng nếu là LTL.
                                                if (transportID == iLTL)
                                                {
                                                    if (gop.ID > 0 && dicPacking.ContainsKey(gop.ID))
                                                    {
                                                        switch (dicPacking[gop.ID])
                                                        {
                                                            case 1://Ton
                                                                if (objProduct.Ton <= 0)
                                                                {
                                                                    objProduct.Ton = 0;
                                                                    excelError.Add("Không có số tấn.");
                                                                }
                                                                break;
                                                            case 2://CBM
                                                                if (objProduct.CBM <= 0)
                                                                {
                                                                    objProduct.CBM = 0;
                                                                    excelError.Add("Không có số khối.");
                                                                }
                                                                break;
                                                            case 3://Quantity
                                                                if (objProduct.Quantity <= 0)
                                                                {
                                                                    objProduct.Quantity = 0;
                                                                    excelError.Add("Không có số lượng.");
                                                                }
                                                                break;
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    //if (product.Ton == 0 && product.CBM == 0)
                                                    //    excelError.Add("Không có thông tin tấn/khối.");
                                                }

                                                //Lấy giá theo import.
                                                if (gop.ID > 0 && obj.ContractID == null)
                                                {
                                                    switch (gop.PackingType)
                                                    {
                                                        case 1://Ton
                                                            if (objSetting.PriceTon > 0 && !string.IsNullOrEmpty(excelInput["PriceTon"]))
                                                            {
                                                                try
                                                                {
                                                                    objProduct.Price = Convert.ToDecimal(excelInput["PriceTon"]);
                                                                }
                                                                catch (Exception)
                                                                {
                                                                    excelError.Add("Sai giá tấn");
                                                                }
                                                            }
                                                            break;
                                                        case 2://CBM
                                                            if (objSetting.PriceCBM > 0 && !string.IsNullOrEmpty(excelInput["PriceCBM"]))
                                                            {
                                                                try
                                                                {
                                                                    objProduct.Price = Convert.ToDecimal(excelInput["PriceCBM"]);
                                                                }
                                                                catch (Exception)
                                                                {
                                                                    excelError.Add("Sai giá khối");
                                                                }
                                                            }
                                                            break;
                                                        case 3://Quantity
                                                            if (objSetting.PriceQuantity > 0 && !string.IsNullOrEmpty(excelInput["PriceQuantity"]))
                                                            {
                                                                try
                                                                {
                                                                    objProduct.Price = Convert.ToDecimal(excelInput["PriceQuantity"]);
                                                                }
                                                                catch (Exception)
                                                                {
                                                                    excelError.Add("Sai giá SL");
                                                                }
                                                            }
                                                            break;
                                                    }
                                                }

                                                obj.ListProduct.Add(objProduct);
                                            }
                                            foreach (var dic in dicQP)
                                            {
                                                var tmp = dic.Key.ToString().Split('-').ToList();
                                                var objProduct = new DTOORDOrder_Import_Product();
                                                objProduct.SortOrder = sortOrder;
                                                objProduct.SOCode = excelInput["SOCode"];
                                                objProduct.DNCode = excelInput["DNCode"];

                                                var cusStock = data.ListStock.FirstOrDefault(c => c.CUSLocationID.ToString() == tmp[0] && c.CustomerID == cusID);
                                                var cusGroup = data.ListGroupOfProduct.FirstOrDefault(c => c.CUSStockID.ToString() == tmp[0] && c.ID.ToString() == tmp[1]);
                                                var cusProduct = data.ListProduct.FirstOrDefault(c => c.GroupOfProductID.ToString() == tmp[1] && c.ID.ToString() == tmp[2]);
                                                if (cusStock != null && cusGroup != null && cusProduct != null)
                                                {
                                                    objProduct.GroupOfProductID = cusGroup.ID;
                                                    objProduct.GroupOfProductCode = cusGroup.Code;
                                                    objProduct.ProductID = cusProduct.ID;
                                                    objProduct.ProductCode = cusProduct.Code;
                                                    objProduct.CUSRoutingID = cusRoutingID > 0 ? cusRoutingID : null;
                                                    objProduct.PartnerID = pID;
                                                    objProduct.PartnerCode = dCode;
                                                    objProduct.PartnerName = dName;
                                                    objProduct.LocationToID = toID;
                                                    objProduct.HasCashCollect = !string.IsNullOrEmpty(excelInput["HasCashCollect"]) && excelInput["HasCashCollect"].Trim().ToLower() == "x";
                                                    objProduct.LocationFromID = cusStock.CUSLocationID;
                                                    objProduct.LocationFromCode = cusStock.LocationCode;
                                                    objProduct.LocationFromName = cusStock.LocationName;
                                                    objProduct.LocationToCode = toCode;
                                                    objProduct.LocationToName = toName;
                                                    objProduct.EconomicZone = excelInput["EconomicZone"];
                                                    objProduct.Note1 = excelInput["Note1"];
                                                    objProduct.Note2 = excelInput["Note2"];

                                                    objProduct.TempMax = tmpMax;
                                                    objProduct.TempMin = tmpMin;
                                                    objProduct.ETD = eTD;
                                                    objProduct.ETARequest = eTARequest;
                                                    objProduct.ETDRequest = eTDRequest;
                                                    objProduct.ETA = eTA;
                                                    if (objProduct.ETA != null && objProduct.ETD != null && objProduct.ETD >= objProduct.ETA)
                                                    {
                                                        excelError.Add("Sai ràng buộc ETD-ETA.");
                                                    }
                                                    objProduct.ListLocationToAddress = sLocation;
                                                    objProduct.LocationToAddress = excelInput["LocationToAddress"];

                                                    if (!cusSKU)
                                                    {
                                                        objProduct.Ton = dic.Value[1];
                                                        objProduct.CBM = dic.Value[2];
                                                        objProduct.Quantity = dic.Value[3];
                                                        if (cusProduct.IsKg)
                                                            objProduct.Ton = objProduct.Ton / 1000;
                                                    }
                                                    else
                                                    {
                                                        switch (cusProduct.PackingTypeGOP)
                                                        {
                                                            case 1:
                                                                objProduct.Ton = dic.Value[1];
                                                                if (cusProduct.Weight.HasValue && cusProduct.Weight != 0)
                                                                    objProduct.Quantity = objProduct.Ton / cusProduct.Weight.Value;
                                                                if (cusProduct.IsKg)
                                                                    objProduct.Quantity = objProduct.Quantity / 1000;
                                                                if (cusProduct.CBM.HasValue)
                                                                    objProduct.CBM = objProduct.Quantity * cusProduct.CBM.Value;
                                                                break;
                                                            case 2:
                                                                objProduct.CBM = dic.Value[2];
                                                                if (cusProduct.CBM.HasValue && cusProduct.CBM != 0)
                                                                    objProduct.Quantity = objProduct.CBM / cusProduct.CBM.Value;
                                                                if (cusProduct.Weight.HasValue)
                                                                    objProduct.Ton = objProduct.Quantity * cusProduct.Weight.Value;
                                                                break;
                                                            case 3:
                                                                objProduct.Quantity = dic.Value[3];
                                                                if (cusProduct.CBM.HasValue)
                                                                    objProduct.CBM = objProduct.Quantity * cusProduct.CBM.Value;
                                                                if (cusProduct.Weight.HasValue)
                                                                    objProduct.Ton = objProduct.Quantity * cusProduct.Weight.Value;
                                                                break;
                                                            default:
                                                                break;
                                                        }
                                                        objProduct.Ton = Math.Round(objProduct.Ton, 5, MidpointRounding.AwayFromZero);
                                                        objProduct.CBM = Math.Round(objProduct.CBM, 5, MidpointRounding.AwayFromZero);
                                                        objProduct.Quantity = Math.Round(objProduct.Quantity, 5, MidpointRounding.AwayFromZero);
                                                    }
                                                    //Kiểm tra thông tin sản lượng nếu là LTL.
                                                    if (transportID == iLTL)
                                                    {
                                                        if (cusGroup.ID > 0 && dicPacking.ContainsKey(cusGroup.ID))
                                                        {
                                                            switch (dicPacking[cusGroup.ID])
                                                            {
                                                                case 1://Ton
                                                                    if (objProduct.Ton <= 0)
                                                                    {
                                                                        objProduct.Ton = 0;
                                                                        excelError.Add("Không có số tấn.");
                                                                    }
                                                                    break;
                                                                case 2://CBM
                                                                    if (objProduct.CBM <= 0)
                                                                    {
                                                                        objProduct.CBM = 0;
                                                                        excelError.Add("Không có số khối.");
                                                                    }
                                                                    break;
                                                                case 3://Quantity
                                                                    if (objProduct.Quantity <= 0)
                                                                    {
                                                                        objProduct.Quantity = 0;
                                                                        excelError.Add("Không có số lượng.");
                                                                    }
                                                                    break;
                                                            }
                                                        }
                                                    }

                                                    //Lấy giá theo import.
                                                    if (cusGroup.ID > 0 && obj.ContractID == null)
                                                    {
                                                        switch (cusGroup.PackingType)
                                                        {
                                                            case 1://Ton
                                                                if (objSetting.PriceTon > 0 && !string.IsNullOrEmpty(excelInput["PriceTon"]))
                                                                {
                                                                    try
                                                                    {
                                                                        objProduct.Price = Convert.ToDecimal(excelInput["PriceTon"]);
                                                                    }
                                                                    catch (Exception)
                                                                    {
                                                                        excelError.Add("Sai giá tấn");
                                                                    }
                                                                }
                                                                break;
                                                            case 2://CBM
                                                                if (objSetting.PriceCBM > 0 && !string.IsNullOrEmpty(excelInput["PriceCBM"]))
                                                                {
                                                                    try
                                                                    {
                                                                        objProduct.Price = Convert.ToDecimal(excelInput["PriceCBM"]);
                                                                    }
                                                                    catch (Exception)
                                                                    {
                                                                        excelError.Add("Sai giá khối");
                                                                    }
                                                                }
                                                                break;
                                                            case 3://Quantity
                                                                if (objSetting.PriceQuantity > 0 && !string.IsNullOrEmpty(excelInput["PriceQuantity"]))
                                                                {
                                                                    try
                                                                    {
                                                                        objProduct.Price = Convert.ToDecimal(excelInput["PriceQuantity"]);
                                                                    }
                                                                    catch (Exception)
                                                                    {
                                                                        excelError.Add("Sai giá SL");
                                                                    }
                                                                }
                                                                break;
                                                        }
                                                    }

                                                    obj.ListProduct.Add(objProduct);
                                                }
                                            }
                                            sortOrder++;

                                            excelError.Distinct();
                                            obj.ExcelError = string.Join(" ", excelError);
                                            if (!string.IsNullOrEmpty(obj.ExcelError))
                                                obj.ExcelSuccess = false;

                                            dataRes.Add(obj);
                                            #endregion

                                            #endregion
                                        }
                                        else if (transportID == iFCL)
                                        {
                                            #region ĐH Xe container
                                            int cusID = -1, fID = -1, tID = -1;
                                            int? crID = null, dpID = null, dprID = null;
                                            string cusCode = string.Empty;
                                            string frCode = string.Empty, frName = string.Empty, toCode = string.Empty, toName = string.Empty;
                                            string dpCode = string.Empty, dpName = string.Empty, dprCode = string.Empty, dprName = string.Empty;
                                            string crCode = string.Empty, crName = string.Empty, crCodeName = string.Empty;
                                            double ton = 0; bool? isInspect = null;

                                            #region Check Customer và Code

                                            if (objSetting.CustomerID == objSetting.SYSCustomerID)
                                            {
                                                if (string.IsNullOrEmpty(excelInput["CustomerCode"]))
                                                {
                                                    excelError.Add("Thiếu mã KH.");
                                                }
                                                else
                                                {
                                                    var objCheck = data.ListCustomer.FirstOrDefault(c => c.Code.Trim().ToLower() == excelInput["CustomerCode"].Trim().ToLower());
                                                    if (objCheck == null)
                                                    {
                                                        excelError.Add("KH [" + excelInput["CustomerCode"] + "] không tồn tại.");
                                                    }
                                                    else
                                                    {
                                                        cusID = objCheck.ID;
                                                        cusCode = objCheck.Code;
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                cusID = objSetting.CustomerID;
                                                var objCheck = data.ListCustomer.FirstOrDefault(c => c.ID == cusID);
                                                if (objCheck == null)
                                                {
                                                    excelError.Add("KH không tồn tại.");
                                                }
                                                else
                                                {
                                                    cusCode = objCheck.Code;
                                                }
                                            }

                                            if (!string.IsNullOrEmpty(excelInput["OrderCode"]))
                                            {
                                                //var objCheck = data.ListCode.FirstOrDefault(c => c.Code == excelInput["OrderCode"]);
                                                //if (objCheck != null)
                                                //{
                                                //    excelError.Add("Mã ĐH đã sử dụng.");
                                                //}
                                            }
                                            else
                                            {
                                                var objCode = dataSettingCode.FirstOrDefault(c => c.CustomerID == cusID);
                                                if (objCode == null)
                                                    objCode = dataSettingCode.FirstOrDefault(c => c.CustomerID == SYSCustomerID);
                                                if (objCode == null || objCode.ActionType < 2) //Ko thiết lập hoặc theo cột mã.
                                                {
                                                    excelError.Add("Thiếu mã ĐH.");
                                                }
                                            }

                                            if (cusID > 0 && objSetting.HasUseContract)
                                            {
                                                var dataC = data.ListContractTerm.Where(c => c.CustomerID == cusID && c.TransportModeID == tmID && c.ServiceOfOrderID == svID).ToList();
                                                if (dataC.Count == 0)
                                                    excelError.Add("Không có hợp đồng.");
                                                else if (dataC.Count > 1)
                                                    excelError.Add("Có hơn 1 hợp đồng.");
                                                else
                                                {
                                                    ctID = dataC.FirstOrDefault().ID;
                                                    ctTermID = dataC.FirstOrDefault().ContractTermID;
                                                }
                                            }

                                            #endregion

                                            #region Check tgian

                                            DateTime? requestDate = null;
                                            DateTime? eTD = null;
                                            DateTime? eTA = null;
                                            DateTime? eTARequest = null;
                                            DateTime? eTDRequest = null;
                                            DateTime? cutOffTime = null;
                                            DateTime? getDate = null;
                                            DateTime? returnDate = null;
                                            DateTime? inspectDate = null;

                                            if (objSetting.RequestDate > 0)
                                            {
                                                try
                                                {
                                                    requestDate = ExcelHelper.ValueToDate(excelInput["RequestDate"]);
                                                }
                                                catch
                                                {
                                                    try
                                                    {
                                                        requestDate = Convert.ToDateTime(excelInput["RequestDate"], new CultureInfo("vi-VN"));
                                                    }
                                                    catch { }
                                                }
                                                if (objSetting.RequestTime > 0 && requestDate != null)
                                                {
                                                    if (!string.IsNullOrEmpty(excelInput["RequestTime"]))
                                                    {
                                                        try
                                                        {
                                                            requestDate = requestDate.Value.Date.Add(TimeSpan.Parse(excelInput["RequestTime"]));
                                                        }
                                                        catch
                                                        {
                                                            excelError.Add("Sai giờ gửi yêu cầu.");
                                                        }
                                                    }
                                                }
                                            }

                                            if (objSetting.RequestDate_Time > 0)
                                            {
                                                try
                                                {
                                                    requestDate = ExcelHelper.ValueToDate(excelInput["RequestDate_Time"]);
                                                }
                                                catch
                                                {
                                                    try
                                                    {
                                                        requestDate = Convert.ToDateTime(excelInput["RequestDate_Time"], new CultureInfo("vi-VN"));
                                                    }
                                                    catch { }
                                                }
                                            }

                                            if (requestDate == null)
                                            {
                                                excelError.Add("Sai ngày gửi yêu cầu");
                                            }

                                            if (!string.IsNullOrEmpty(excelInput["ETD"]))
                                            {
                                                try
                                                {
                                                    eTD = ExcelHelper.ValueToDate(excelInput["ETD"]);
                                                }
                                                catch
                                                {
                                                    try
                                                    {
                                                        eTD = Convert.ToDateTime(excelInput["ETD"], new CultureInfo("vi-VN"));
                                                    }
                                                    catch
                                                    {
                                                        if (eTD == null)
                                                        {
                                                            excelError.Add("Sai ETD");
                                                        }
                                                    }
                                                }
                                            }
                                            else if (objSetting.ETDTime_RequestDate > 0 && requestDate != null)
                                            {
                                                if (!string.IsNullOrEmpty(excelInput["ETDTime_RequestDate"]))
                                                {
                                                    try
                                                    {
                                                        eTD = requestDate.Value.Date.Add(TimeSpan.Parse(excelInput["ETDTime_RequestDate"]));
                                                    }
                                                    catch
                                                    {
                                                        excelError.Add("Sai giờ ETD.");
                                                    }
                                                }
                                                else
                                                {
                                                    eTD = requestDate;
                                                }
                                            }

                                            if (!string.IsNullOrEmpty(excelInput["ETA"]))
                                            {
                                                try
                                                {
                                                    eTA = ExcelHelper.ValueToDate(excelInput["ETA"]);
                                                }
                                                catch
                                                {
                                                    try
                                                    {
                                                        eTA = Convert.ToDateTime(excelInput["ETA"], new CultureInfo("vi-VN"));
                                                    }
                                                    catch
                                                    {
                                                        if (eTA == null)
                                                        {
                                                            excelError.Add("Sai ETA");
                                                        }
                                                    }
                                                }
                                            }
                                            else if (objSetting.ETATime_RequestDate > 0 && requestDate != null)
                                            {
                                                if (!string.IsNullOrEmpty(excelInput["ETATime_RequestDate"]))
                                                {
                                                    try
                                                    {
                                                        eTA = requestDate.Value.Date.Add(TimeSpan.Parse(excelInput["ETATime_RequestDate"]));
                                                    }
                                                    catch
                                                    {
                                                        excelError.Add("Sai giờ ETA.");
                                                    }
                                                }
                                                else
                                                {
                                                    eTA = requestDate;
                                                }
                                            }

                                            if (!string.IsNullOrEmpty(excelInput["ETARequest"]))
                                            {
                                                try
                                                {
                                                    eTARequest = ExcelHelper.ValueToDate(excelInput["ETARequest"]);
                                                }
                                                catch
                                                {
                                                    try
                                                    {
                                                        eTARequest = Convert.ToDateTime(excelInput["ETARequest"], new CultureInfo("vi-VN"));
                                                    }
                                                    catch
                                                    {
                                                        if (eTARequest == null)
                                                        {
                                                            excelError.Add("Sai ngày y.c giao hàng.");
                                                        }
                                                    }
                                                }
                                                if (eTARequest != null && objSetting.ETARequestTime > 0)
                                                {
                                                    if (!string.IsNullOrEmpty(excelInput["ETARequestTime"]))
                                                    {
                                                        try
                                                        {
                                                            eTARequest = eTARequest.Value.Date.Add(TimeSpan.Parse(excelInput["ETARequestTime"]));
                                                        }
                                                        catch
                                                        {
                                                            excelError.Add("Sai giờ y.c giao hàng.");
                                                        }
                                                    }
                                                }
                                            }

                                            if (!string.IsNullOrEmpty(excelInput["ETDRequest"]))
                                            {
                                                try
                                                {
                                                    eTDRequest = ExcelHelper.ValueToDate(excelInput["ETDRequest"]);
                                                }
                                                catch
                                                {
                                                    try
                                                    {
                                                        eTDRequest = Convert.ToDateTime(excelInput["ETDRequest"], new CultureInfo("vi-VN"));
                                                    }
                                                    catch
                                                    {
                                                        if (eTDRequest == null)
                                                        {
                                                            excelError.Add("Sai ngày y.c lấy hàng.");
                                                        }
                                                    }
                                                }
                                                if (eTDRequest != null && objSetting.ETDRequestTime > 0)
                                                {
                                                    if (!string.IsNullOrEmpty(excelInput["ETDRequestTime"]))
                                                    {
                                                        try
                                                        {
                                                            eTDRequest = eTDRequest.Value.Date.Add(TimeSpan.Parse(excelInput["ETDRequestTime"]));
                                                        }
                                                        catch
                                                        {
                                                            excelError.Add("Sai giờ y.c lấy hàng.");
                                                        }
                                                    }
                                                }
                                            }

                                            if (!string.IsNullOrEmpty(excelInput["CutOffTime"]))
                                            {
                                                try
                                                {
                                                    cutOffTime = ExcelHelper.ValueToDate(excelInput["CutOffTime"]);
                                                }
                                                catch
                                                {
                                                    try
                                                    {
                                                        cutOffTime = Convert.ToDateTime(excelInput["CutOffTime"], new CultureInfo("vi-VN"));
                                                    }
                                                    catch
                                                    {
                                                        if (cutOffTime == null)
                                                        {
                                                            excelError.Add("Sai cut-off-time.");
                                                        }
                                                    }
                                                }
                                            }

                                            // Ngày lấy rỗng
                                            if (!string.IsNullOrEmpty(excelInput["Date_TimeGetEmpty"]))
                                            {
                                                try
                                                {
                                                    getDate = ExcelHelper.ValueToDate(excelInput["Date_TimeGetEmpty"]);
                                                }
                                                catch
                                                {
                                                    try
                                                    {
                                                        getDate = Convert.ToDateTime(excelInput["Date_TimeGetEmpty"], new CultureInfo("vi-VN"));
                                                    }
                                                    catch
                                                    {
                                                        excelError.Add("Sai ngày lấy rỗng.");
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                if (!string.IsNullOrEmpty(excelInput["DateGetEmpty"]))
                                                {
                                                    try
                                                    {
                                                        getDate = ExcelHelper.ValueToDate(excelInput["DateGetEmpty"]);
                                                    }
                                                    catch
                                                    {
                                                        try
                                                        {
                                                            getDate = Convert.ToDateTime(excelInput["DateGetEmpty"], new CultureInfo("vi-VN"));
                                                        }
                                                        catch
                                                        {
                                                            excelError.Add("Sai ngày lấy rỗng.");
                                                        }
                                                    }
                                                }
                                                if (getDate != null && !string.IsNullOrEmpty(excelInput["TimeGetEmpty"]))
                                                {
                                                    try
                                                    {
                                                        getDate = getDate.Value.Date.Add(TimeSpan.Parse(excelInput["TimeGetEmpty"]));
                                                    }
                                                    catch
                                                    {
                                                        excelError.Add("Sai giờ lấy rỗng.");
                                                    }
                                                }
                                            }

                                            // Nếu ko nhập Ngày lấy rỗng => ETD-12h
                                            if (getDate == null && string.IsNullOrEmpty(excelInput["Date_TimeGetEmpty"]) && string.IsNullOrEmpty(excelInput["DateGetEmpty"]) && string.IsNullOrEmpty(excelInput["TimeGetEmpty"]))
                                            {
                                                if (eTD != null)
                                                    getDate = eTD.Value.AddHours(-12);
                                            }

                                            // Ngày trả rỗng
                                            if (!string.IsNullOrEmpty(excelInput["Date_TimeReturnEmpty"]))
                                            {
                                                try
                                                {
                                                    returnDate = ExcelHelper.ValueToDate(excelInput["Date_TimeReturnEmpty"]);
                                                }
                                                catch
                                                {
                                                    try
                                                    {
                                                        returnDate = Convert.ToDateTime(excelInput["Date_TimeReturnEmpty"], new CultureInfo("vi-VN"));
                                                    }
                                                    catch
                                                    {
                                                        excelError.Add("Sai ngày trả rỗng.");
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                if (!string.IsNullOrEmpty(excelInput["DateReturnEmpty"]))
                                                {
                                                    try
                                                    {
                                                        returnDate = ExcelHelper.ValueToDate(excelInput["DateReturnEmpty"]);
                                                    }
                                                    catch
                                                    {
                                                        try
                                                        {
                                                            returnDate = Convert.ToDateTime(excelInput["DateReturnEmpty"], new CultureInfo("vi-VN"));
                                                        }
                                                        catch
                                                        {
                                                            excelError.Add("Sai ngày trả rỗng.");
                                                        }
                                                    }
                                                }
                                                if (returnDate != null && !string.IsNullOrEmpty(excelInput["TimeReturnEmpty"]))
                                                {
                                                    try
                                                    {
                                                        returnDate = returnDate.Value.Date.Add(TimeSpan.Parse(excelInput["TimeReturnEmpty"]));
                                                    }
                                                    catch
                                                    {
                                                        excelError.Add("Sai giờ trả rỗng.");
                                                    }
                                                }
                                            }

                                            // Nếu ko nhập Ngày trả rỗng => ETA + 12h
                                            if (returnDate == null && string.IsNullOrEmpty(excelInput["Date_TimeReturnEmpty"]) && string.IsNullOrEmpty(excelInput["DateReturnEmpty"]) && string.IsNullOrEmpty(excelInput["TimeReturnEmpty"]))
                                            {
                                                if (eTA != null)
                                                    returnDate = eTA.Value.AddHours(12);
                                            }

                                            #endregion

                                            #region Check Carrier, Depot, From, To

                                            frCode = excelInput["LocationFromCode"];
                                            frName = excelInput["LocationFromName"];
                                            if (objSetting.LocationFromCodeName > 0)
                                            {
                                                if (!string.IsNullOrEmpty(excelInput["LocationFromCodeName"]))
                                                {
                                                    frCode = excelInput["LocationFromCodeName"].Split('-').FirstOrDefault();
                                                    frName = excelInput["LocationFromCodeName"].Split('-').Skip(1).FirstOrDefault();
                                                }
                                                else
                                                {
                                                    frCode = string.Empty;
                                                    frName = string.Empty;
                                                }
                                            }

                                            toCode = excelInput["LocationToCode"];
                                            toName = excelInput["LocationToName"];
                                            if (objSetting.LocationToCodeName > 0)
                                            {
                                                if (!string.IsNullOrEmpty(excelInput["LocationToCodeName"]))
                                                {
                                                    toCode = excelInput["LocationToCodeName"].Split('-').FirstOrDefault();
                                                    toName = excelInput["LocationToCodeName"].Split('-').Skip(1).FirstOrDefault();
                                                }
                                                else
                                                {
                                                    toCode = string.Empty;
                                                    toName = string.Empty;
                                                }
                                            }

                                            #region Nội địa
                                            if (serviceID == iLO || serviceID == iLOEmpty || serviceID == iLOLaden)
                                            {
                                                var dataDepot = data.ListDepot.Where(c => c.CustomerID == cusID).ToList();

                                                if (objSetting.LocationDepotCode > 0 && !string.IsNullOrEmpty(excelInput["LocationDepotCode"]))
                                                {
                                                    dpCode = excelInput["LocationDepotCode"].Trim();
                                                    dpName = excelInput["LocationDepotName"].Trim();
                                                    var objCheck = dataDepot.FirstOrDefault(c => c.LocationCode.Trim().ToLower() == dpCode.Trim().ToLower());
                                                    if (objCheck != null)
                                                    {
                                                        dpID = objCheck.CUSLocationID;
                                                        if (string.IsNullOrEmpty(dpName))
                                                            dpName = objCheck.LocationName;
                                                    }
                                                    else
                                                    {
                                                        excelError.Add("Bãi container [" + dpCode + "] không tồn tại.");
                                                    }
                                                }
                                                if (objSetting.LocationReturnCode > 0 && !string.IsNullOrEmpty(excelInput["LocationReturnCode"]))
                                                {
                                                    dprCode = excelInput["LocationReturnCode"].Trim();
                                                    dprName = excelInput["LocationReturnName"].Trim();
                                                    var objCheck = dataDepot.FirstOrDefault(c => c.LocationCode.Trim().ToLower() == dprCode.Trim().ToLower());
                                                    if (objCheck != null)
                                                    {
                                                        dprID = objCheck.CUSLocationID;
                                                        if (string.IsNullOrEmpty(dprName))
                                                            dprName = objCheck.LocationName;
                                                    }
                                                    else
                                                    {
                                                        excelError.Add("Bãi container [" + dprCode + "] không tồn tại.");
                                                    }
                                                }

                                                var objF = data.ListCUSLocation.FirstOrDefault(c => c.CustomerID == cusID && c.LocationCode.Trim().ToLower() == frCode.Trim().ToLower());
                                                if (objF != null)
                                                {
                                                    fID = objF.CUSLocationID;
                                                    if (string.IsNullOrEmpty(frName))
                                                        frName = objF.LocationName;
                                                }
                                                else
                                                {
                                                    excelError.Add("Điểm nhận hàng [" + frCode + "] không tồn tại.");
                                                }

                                                var objT = data.ListCUSLocation.FirstOrDefault(c => c.CustomerID == cusID && c.LocationCode.Trim().ToLower() == toCode.Trim().ToLower());
                                                if (objT != null)
                                                {
                                                    tID = objT.CUSLocationID;
                                                    if (string.IsNullOrEmpty(toName))
                                                        toName = objT.LocationName;
                                                }
                                                else
                                                {
                                                    excelError.Add("Điểm giao hàng [" + toCode + "] không tồn tại.");
                                                }

                                                if (fID == tID && fID > 0)
                                                {
                                                    excelError.Add("Điểm nhận hàng và điểm giao hàng trùng nhau.");
                                                }

                                                if (dpID == fID && fID > 0 && dpID > 0)
                                                {
                                                    excelError.Add("Điểm nhận hàng và điểm lấy rỗng trùng nhau.");
                                                }
                                                if (dprID == tID && tID > 0 && dprID > 0)
                                                {
                                                    excelError.Add("Điểm giao hàng và điểm trả rỗng trùng nhau.");
                                                }
                                            }
                                            #endregion

                                            #region Xuất nhập khẩu
                                            else
                                            {
                                                //Nếu không có Depot và DepotReturn => Mặc định depot đầu tiên của hãng tàu.
                                                if (objSetting.TypeOfWAInspectionStatus > 0 && !string.IsNullOrEmpty(excelInput["TypeOfWAInspectionStatus"]))
                                                {
                                                    if (excelInput["TypeOfWAInspectionStatus"].Trim().ToLower() == "x")
                                                    {
                                                        isInspect = true;
                                                    }
                                                    else
                                                    {
                                                        isInspect = false;
                                                    }
                                                }

                                                if (!string.IsNullOrEmpty(excelInput["InspectionDate"]))
                                                {
                                                    try
                                                    {
                                                        inspectDate = ExcelHelper.ValueToDate(excelInput["InspectionDate"]);
                                                    }
                                                    catch
                                                    {
                                                        try
                                                        {
                                                            inspectDate = Convert.ToDateTime(excelInput["InspectionDate"], new CultureInfo("vi-VN"));
                                                        }
                                                        catch
                                                        {
                                                            excelError.Add("Sai ngày kiểm hóa.");
                                                        }
                                                    }
                                                }

                                                // Hãng tàu
                                                crCode = excelInput["CarrierCode"].Trim();
                                                crName = excelInput["CarrierName"].Trim();
                                                crCodeName = excelInput["CarrierCodeName"].Trim();
                                                if (!string.IsNullOrEmpty(crCodeName))
                                                {
                                                    string[] s = crCodeName.Split('-');
                                                    crCode = s[0].Trim();
                                                    if (s.Length > 1)
                                                    {
                                                        crName = crCodeName.Substring(crCode.Length + 1);
                                                    }
                                                }

                                                if (!string.IsNullOrEmpty(crCode))
                                                {
                                                    var objCarrier = data.ListCarrier.FirstOrDefault(c => c.CustomerID == cusID && c.PartnerCode.Trim().ToLower() == crCode.Trim().ToLower());
                                                    if (objCarrier != null)
                                                    {
                                                        crID = objCarrier.CUSPartnerID;
                                                        if (string.IsNullOrEmpty(crName))
                                                            crName = objCarrier.PartnerName;

                                                        var dataDepot = data.ListDepot.Where(c => c.CusPartID == crID && c.CustomerID == cusID).ToList();
                                                        if (dataDepot.Count == 0)
                                                        {
                                                            excelError.Add("Hãng tàu [" + crCode + "] chưa thiết lập bãi container.");
                                                        }
                                                        else
                                                        {
                                                            //Nhập khẩu (Cảng-Kho-Depot)
                                                            if (serviceID == iIM)
                                                            {
                                                                if (objSetting.LocationReturnCode > 0 && !string.IsNullOrEmpty(excelInput["LocationReturnCode"]))
                                                                {
                                                                    dprCode = excelInput["LocationReturnCode"].Trim();
                                                                    dprName = excelInput["LocationReturnName"].Trim();
                                                                    var objCheck = dataDepot.FirstOrDefault(c => c.LocationCode.Trim().ToLower() == dprCode.ToLower());
                                                                    if (objCheck != null)
                                                                    {
                                                                        dprID = objCheck.CUSLocationID;
                                                                        if (string.IsNullOrEmpty(dprName))
                                                                            dprName = objCheck.LocationName;
                                                                    }
                                                                    else
                                                                    {
                                                                        excelError.Add("Bãi container [" + dprCode + "] không tồn tại.");
                                                                    }
                                                                }

                                                                if (objSetting.LocationDepotCode > 0 && !string.IsNullOrEmpty(excelInput["LocationDepotCode"].Trim()))
                                                                {
                                                                    dprCode = excelInput["LocationDepotCode"].Trim();
                                                                    dprName = excelInput["LocationDepotName"].Trim();
                                                                    var objCheck = dataDepot.FirstOrDefault(c => c.LocationCode.Trim().ToLower() == dprCode.Trim().ToLower());
                                                                    if (objCheck != null)
                                                                    {
                                                                        dprID = objCheck.CUSLocationID;
                                                                        if (string.IsNullOrEmpty(dprName))
                                                                            dprName = objCheck.LocationName;
                                                                    }
                                                                    else
                                                                    {
                                                                        excelError.Add("Bãi container [" + dprCode + "] không tồn tại.");
                                                                    }
                                                                }

                                                                // Nếu ko có depot thì mặc định lấy depot đầu tiên của hãng tàu
                                                                if (string.IsNullOrEmpty(dprCode))
                                                                {
                                                                    dprID = dataDepot.FirstOrDefault().CUSLocationID;
                                                                    dprCode = dataDepot.FirstOrDefault().LocationCode;
                                                                    dprName = dataDepot.FirstOrDefault().LocationName;
                                                                }

                                                                var objF = data.ListSeaPort.FirstOrDefault(c => c.CustomerID == cusID && c.LocationCode.Trim().ToLower() == frCode.Trim().ToLower());
                                                                if (objF != null)
                                                                {
                                                                    fID = objF.CUSLocationID;
                                                                    if (string.IsNullOrEmpty(frName))
                                                                        frName = objF.LocationName;
                                                                }
                                                                else
                                                                {
                                                                    excelError.Add("Điểm nhận hàng [" + frCode + "] không tồn tại.");
                                                                }

                                                                var objT = data.ListStock.FirstOrDefault(c => c.CustomerID == cusID && c.LocationCode.Trim().ToLower() == toCode.Trim().ToLower());
                                                                if (objT != null)
                                                                {
                                                                    tID = objT.CUSLocationID;
                                                                    if (string.IsNullOrEmpty(toName))
                                                                        toName = objT.LocationName;
                                                                }
                                                                else
                                                                {
                                                                    excelError.Add("Điểm giao hàng [" + toCode + "] không tồn tại.");
                                                                }

                                                                if (fID == tID && fID > 0)
                                                                {
                                                                    excelError.Add("Điểm nhận hàng vả điểm giao hàng trùng nhau.");
                                                                }
                                                                if (dprID == tID && tID > 0 && dprID > 0)
                                                                {
                                                                    excelError.Add("Điểm giao hàng và điểm trả rỗng trùng nhau.");
                                                                }
                                                            }
                                                            else
                                                            {
                                                                //Nhập khẩu (Depot-Kho-Cảng)
                                                                if (serviceID == iEx)
                                                                {
                                                                    if (objSetting.LocationDepotCode > 0 && !string.IsNullOrEmpty(excelInput["LocationDepotCode"]))
                                                                    {
                                                                        dpCode = excelInput["LocationDepotCode"];
                                                                        dpName = excelInput["LocationDepotName"];
                                                                        var objCheck = dataDepot.FirstOrDefault(c => c.LocationCode.Trim().ToLower() == excelInput["LocationDepotCode"].Trim().ToLower());
                                                                        if (objCheck != null)
                                                                        {
                                                                            dpID = objCheck.CUSLocationID;
                                                                            if (string.IsNullOrEmpty(dpName))
                                                                                dpName = objCheck.LocationName;
                                                                        }
                                                                        else
                                                                        {
                                                                            excelError.Add("Bãi container [" + excelInput["LocationDepotCode"] + "] không tồn tại.");
                                                                        }
                                                                    }

                                                                    if (objSetting.LocationReturnCode > 0 && !string.IsNullOrEmpty(excelInput["LocationReturnCode"]))
                                                                    {
                                                                        dpCode = excelInput["LocationReturnCode"].Trim();
                                                                        dpName = excelInput["LocationReturnName"].Trim();
                                                                        var objCheck = dataDepot.FirstOrDefault(c => c.LocationCode.Trim().ToLower() == dpCode.Trim().ToLower());
                                                                        if (objCheck != null)
                                                                        {
                                                                            dpID = objCheck.CUSLocationID;
                                                                            if (string.IsNullOrEmpty(dpName))
                                                                                dpName = objCheck.LocationName;
                                                                        }
                                                                        else
                                                                        {
                                                                            excelError.Add("Bãi container [" + dpCode + "] không tồn tại.");
                                                                        }
                                                                    }

                                                                    // Nếu ko có depot thì mặc định lấy depot đầu tiên của hãng tàu
                                                                    if (string.IsNullOrEmpty(dpCode))
                                                                    {
                                                                        dpID = dataDepot.FirstOrDefault().CUSLocationID;
                                                                        dpCode = dataDepot.FirstOrDefault().LocationCode;
                                                                        dpName = dataDepot.FirstOrDefault().LocationName;
                                                                    }

                                                                    var objF = data.ListStock.FirstOrDefault(c => c.CustomerID == cusID && c.LocationCode.Trim().ToLower() == frCode.Trim().ToLower());
                                                                    if (objF != null)
                                                                    {
                                                                        fID = objF.CUSLocationID;
                                                                        if (string.IsNullOrEmpty(frName))
                                                                            frName = objF.LocationName;
                                                                    }
                                                                    else
                                                                    {
                                                                        excelError.Add("Điểm nhận hàng [" + frCode + "] không tồn tại.");
                                                                    }

                                                                    var objT = data.ListSeaPort.FirstOrDefault(c => c.CustomerID == cusID && c.LocationCode.Trim().ToLower() == toCode.Trim().ToLower());
                                                                    if (objT != null)
                                                                    {
                                                                        tID = objT.CUSLocationID;
                                                                        if (string.IsNullOrEmpty(toName))
                                                                            toName = objT.LocationName;
                                                                    }
                                                                    else
                                                                    {
                                                                        excelError.Add("Điểm giao hàng [" + toCode + "] không tồn tại.");
                                                                    }

                                                                    if (fID == tID && fID > 0)
                                                                    {
                                                                        excelError.Add("Điểm nhận hàng vả điểm giao hàng trùng nhau.");
                                                                    }

                                                                    if (dpID == fID && fID > 0 && dpID > 0)
                                                                    {
                                                                        excelError.Add("Điểm nhận hàng và điểm lấy rỗng trùng nhau.");
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                    else
                                                    {
                                                        excelError.Add("Hãng tàu [" + crCode + "] không tồn tại.");
                                                    }
                                                }
                                                else
                                                {
                                                    excelError.Add("Hãng tàu không xác định.");
                                                }
                                            }
                                            #endregion
                                            #endregion

                                            #region Check Container
                                            Dictionary<int, int> dicCQ = new Dictionary<int, int>();
                                            // Chỉ thiết lập 1 cột loại cont
                                            if (!objSetting.HasContainer)
                                            {
                                                var Quantity = 1;
                                                if (objSetting.Quantity > 0)
                                                {
                                                    try
                                                    {
                                                        Quantity = Convert.ToInt32(excelInput["Quantity"]);
                                                        if (Quantity <= 0)
                                                        {
                                                            excelError.Add("Số lượng phải lớn hơn 0.");
                                                        }
                                                    }
                                                    catch
                                                    {
                                                        excelError.Add("Số lượng không hợp lệ.");
                                                    }
                                                }
                                                if (objSetting.TypeOfContainerName < 1)
                                                {
                                                    var objCheck = data.ListPackingCO.FirstOrDefault();
                                                    if (objCheck != null)
                                                    {
                                                        dicCQ.Add(objCheck.ID, Quantity);
                                                    }
                                                }
                                                else
                                                {
                                                    var objCheck = data.ListPackingCO.FirstOrDefault(c => c.Code.Trim().ToLower() == excelInput["TypeOfContainerName"].Trim().ToLower());
                                                    if (objCheck != null)
                                                    {
                                                        dicCQ.Add(objCheck.ID, Quantity);
                                                    }
                                                    else
                                                    {
                                                        excelError.Add("Loại container [" + excelInput["TypeOfContainerName"] + "] không tồn tại.");
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                #region Cột loại cont động
                                                if (objSetting.ListContainer == null || objSetting.ListContainer.Count == 0)
                                                    throw new Exception("Chưa thiết lập cột các loại cont [Loại cont].");

                                                foreach (var stock in objSetting.ListContainer)
                                                {
                                                    var objPacking = data.ListPackingCO.FirstOrDefault(c => c.ID == stock.PackingID);
                                                    if (objPacking == null)
                                                        throw new Exception("Loại cont thiết lập không xác định.");

                                                    var dicS = GetDataValue(worksheet, stock, row, sValue);
                                                    if (!dicS.Values.All(c => string.IsNullOrEmpty(c)))
                                                    {
                                                        try
                                                        {
                                                            if (!string.IsNullOrEmpty(dicS["PackingCOQuantity"]))
                                                            {
                                                                var countCO = Convert.ToInt32(dicS["PackingCOQuantity"]);
                                                                if (countCO <= 0)
                                                                {
                                                                    excelError.Add("Loại cont " + objPacking.Code + " số lượng phải lớn hơn 0.");
                                                                }
                                                                dicCQ.Add(stock.PackingID, Convert.ToInt32(dicS["PackingCOQuantity"]));
                                                            }
                                                        }
                                                        catch
                                                        {
                                                            if (!excelError.Contains("Loại cont " + objPacking.Code + " sai số lượng."))
                                                                excelError.Add("Loại cont " + objPacking.Code + " sai số lượng.");
                                                        }
                                                    }
                                                }
                                                #endregion
                                            }


                                            if (objSetting.Ton > 0 && !string.IsNullOrEmpty(excelInput["Ton"]))
                                            {
                                                try
                                                {
                                                    ton = Convert.ToDouble(excelInput["Ton"]);
                                                    if (ton < 0)
                                                        excelError.Add("Sai trọng tải.");
                                                }
                                                catch (Exception)
                                                {
                                                    excelError.Add("Sai trọng tải.");
                                                }
                                            }

                                            #endregion

                                            #region Lưu dữ liệu

                                            var obj = new DTOORDOrder_Import();
                                            obj.SortOrder = sortOrder;
                                            obj.TypeOfOrderID = -(int)SYSVarType.TypeOfOrderDirect;
                                            if (ctID > 0)
                                                obj.ContractID = ctID;
                                            if (ctTermID > 0)
                                                obj.ContractTermID = ctTermID;
                                            obj.ServiceOfOrderID = svID;
                                            obj.ServiceOfOrderIDTemp = serviceID;
                                            obj.TransportModeID = tmID;
                                            obj.TransportModeIDTemp = transportID;
                                            var objTMName = data.ListTransportMode.FirstOrDefault(c => c.ID == tmID);
                                            if (objTMName != null)
                                                obj.TransportModeName = objTMName.Name;
                                            var objSVName = data.ListServiceOfOrder.FirstOrDefault(c => c.ID == svID);
                                            if (objSVName != null)
                                                obj.ServiceOfOrderName = objSVName.Name;
                                            obj.CustomerID = cusID;
                                            obj.CustomerCode = cusCode;
                                            obj.IsHot = objSetting.IsHot > 0 && !string.IsNullOrEmpty(excelInput["IsHot"]) && (excelInput["IsHot"].Trim().ToLower() == "true" || excelInput["IsHot"].Trim().ToLower() == "x");
                                            obj.ExcelSuccess = true;
                                            obj.Note = excelInput["Note"];
                                            obj.VesselName = excelInput["VesselName"];
                                            obj.VesselNo = excelInput["VesselNo"];
                                            obj.TripNo = excelInput["TripNo"];
                                            obj.LocationToID = tID;
                                            obj.LocationToAddress = excelInput["LocationToAddress"];
                                            obj.TypeOfWAInspectionStatus = isInspect;
                                            obj.DateInspection = inspectDate;
                                            if (obj.LocationToID < 0)
                                                obj.LocationToID = null;
                                            obj.PartnerID = crID;
                                            obj.PartnerCode = crCode;
                                            obj.PartnerName = crName;
                                            if (requestDate != null)
                                                obj.RequestDate = requestDate.Value;
                                            obj.ETARequest = eTARequest;
                                            obj.ETDRequest = eTDRequest;
                                            obj.ETD = eTD;
                                            obj.ETA = eTA;

                                            obj.Code = excelInput["OrderCode"];
                                            obj.UserDefined1 = excelInput["UserDefine1"];
                                            obj.UserDefined2 = excelInput["UserDefine2"];
                                            obj.UserDefined3 = excelInput["UserDefine3"];
                                            obj.UserDefined4 = excelInput["UserDefine4"];
                                            obj.UserDefined5 = excelInput["UserDefine5"];
                                            obj.UserDefined6 = excelInput["UserDefine6"];
                                            obj.UserDefined7 = excelInput["UserDefine7"];
                                            obj.UserDefined8 = excelInput["UserDefine8"];
                                            obj.UserDefined9 = excelInput["UserDefine9"];

                                            // Import đơn hàng nếu có nhập ETDRequest thì CutOfTime = ETDRequest + giờ gợi ý
                                            if (cutOffTime == null && eTDRequest != null)
                                            {
                                                if (data.CUSSetting.CutOfTimeSuggest > 0)
                                                {
                                                    cutOffTime = eTDRequest.Value.AddHours(data.CUSSetting.CutOfTimeSuggest);
                                                }
                                            }
                                            obj.CutOffTime = cutOffTime;

                                            obj.ListContainer = new List<DTOORDOrder_Import_Container>();
                                            foreach (var con in dicCQ)
                                            {
                                                DTOORDOrder_Import_Container objCo = new DTOORDOrder_Import_Container();
                                                objCo.ContainerNo = excelInput["ContainerNo"];
                                                objCo.SealNo1 = excelInput["SealNo1"];
                                                objCo.SealNo2 = excelInput["SealNo2"];
                                                objCo.Quantity = con.Value;
                                                objCo.PackingID = con.Key;
                                                var objPacking = data.ListPackingCO.FirstOrDefault(c => c.ID == con.Key);
                                                objCo.PackingName = objPacking.PackingName;
                                                objCo.ETA = eTA;
                                                objCo.ETD = eTD;
                                                if (objCo.ETA != null && objCo.ETD != null && objCo.ETD >= objCo.ETA)
                                                {
                                                    excelError.Add("Sai ràng buộc ETD-ETA.");
                                                }
                                                objCo.Ton = ton;
                                                objCo.LocationDepotID = dpID > 0 ? dpID : null;
                                                objCo.LocationDepotCode = dpCode;
                                                objCo.LocationDepotName = dpName;
                                                objCo.LocationDepotReturnID = dprID > 0 ? dprID : null;
                                                objCo.LocationDepotReturnCode = dprCode;
                                                objCo.LocationDepotReturnName = dprName;
                                                objCo.LocationFromID = fID;
                                                objCo.LocationFromCode = frCode;
                                                objCo.LocationFromName = frName;
                                                objCo.LocationToID = tID;
                                                objCo.LocationToCode = toCode;
                                                objCo.LocationToName = toName;
                                                objCo.DateGetEmpty = getDate;
                                                objCo.DateReturnEmpty = returnDate;
                                                objCo.ETARequest = eTARequest;
                                                objCo.ETDRequest = eTDRequest;
                                                obj.ListContainer.Add(objCo);


                                                if (objSetting.ServiceOfOrderID > 0 || serviceID > 0)
                                                {
                                                    if (serviceID == -(int)SYSVarType.ServiceOfOrderImport && objCo.LocationDepotReturnID < 1)
                                                        if (objCo.LocationDepotID > 1)
                                                            objCo.LocationDepotReturnID = objCo.LocationDepotID;
                                                }
                                            }

                                            sortOrder++;

                                            excelError.Distinct();
                                            obj.ExcelError = string.Join(" ", excelError);
                                            if (!string.IsNullOrEmpty(obj.ExcelError))
                                                obj.ExcelSuccess = false;

                                            dataRes.Add(obj);
                                            #endregion

                                            #endregion
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                #region Tạo mã đơn hàng
                List<DTOORDOrder> ListCode = new List<DTOORDOrder>();
                ListCode.AddRange(data.ListCode);
                foreach (var group in dataRes.Where(c => c.ExcelSuccess).GroupBy(c => new { c.CustomerID, c.TransportModeID, c.ServiceOfOrderID, c.ContractID, c.RequestDate }).ToList())
                {
                    var oCode = dataSettingCode.FirstOrDefault(c => c.CustomerID == group.Key.CustomerID);
                    if (oCode == null)
                        oCode = dataSettingCode.FirstOrDefault(c => c.CustomerID == SYSCustomerID);
                    if (oCode != null && oCode.ActionType > 0)
                    {
                        switch (oCode.ActionType)
                        {
                            case 1: //Cột Code
                                foreach (var cGroup in group.GroupBy(c => c.Code).ToList())
                                {
                                    var ETD = cGroup.Min(c => c.ETD);
                                    var ETA = cGroup.Max(c => c.ETA);
                                    foreach (var c in cGroup)
                                    {
                                        c.ETA = ETA;
                                        c.ETD = ETD;
                                    }
                                    if (oCode.Expr.Contains("ETD") && !ETD.HasValue)
                                    {
                                        foreach (var c in cGroup)
                                        {
                                            c.ExcelError += " Không thể tạo mã ĐH. Sai ETD";
                                            c.ExcelSuccess = false;
                                        }
                                    }
                                    else
                                    {
                                        DTOORDOrder_Import_Code param = new DTOORDOrder_Import_Code();
                                        param.CustomerID = cGroup.FirstOrDefault().CustomerID;
                                        param.CustomerCode = cGroup.FirstOrDefault().CustomerCode;
                                        param.TypeOfOrder = cGroup.FirstOrDefault().TransportModeName.ToUpper();
                                        param.ServiceOfOrderID = cGroup.FirstOrDefault().ServiceOfOrderIDTemp;
                                        param.ETD = ETD;
                                        param.UserDefine1 = cGroup.FirstOrDefault().UserDefined1;
                                        param.UserDefine2 = cGroup.FirstOrDefault().UserDefined2;
                                        param.UserDefine3 = cGroup.FirstOrDefault().UserDefined3;
                                        param.UserDefine4 = cGroup.FirstOrDefault().UserDefined4;
                                        param.UserDefine5 = cGroup.FirstOrDefault().UserDefined5;
                                        param.UserDefine6 = cGroup.FirstOrDefault().UserDefined6;
                                        param.UserDefine7 = cGroup.FirstOrDefault().UserDefined7;
                                        param.UserDefine8 = cGroup.FirstOrDefault().UserDefined8;
                                        param.UserDefine9 = cGroup.FirstOrDefault().UserDefined9;
                                        var txt = ORDOrder_Excel_GenCodeFromSetting(ref oCode, ref ListCode, param);
                                        foreach (var c in cGroup)
                                        {
                                            c.Code = txt;
                                        }
                                    }
                                }
                                break;
                            case 2: //Mỗi dòng
                                var aETD = group.Min(c => c.ETD);
                                var aETA = group.Max(c => c.ETA);
                                foreach (var c in group)
                                {
                                    c.ETA = aETA;
                                    c.ETD = aETD;
                                    if (oCode.Expr.Contains("ETD") && !c.ETD.HasValue)
                                    {
                                        c.ExcelError += " Không thể tạo mã ĐH. Sai ETD";
                                        c.ExcelSuccess = false;
                                    }
                                    else
                                    {
                                        DTOORDOrder_Import_Code param = new DTOORDOrder_Import_Code();
                                        param.CustomerID = c.CustomerID;
                                        param.CustomerCode = c.CustomerCode;
                                        param.TypeOfOrder = c.TransportModeName.ToUpper();
                                        param.ServiceOfOrderID = c.ServiceOfOrderIDTemp;
                                        param.ETD = aETD;
                                        param.UserDefine1 = c.UserDefined1;
                                        param.UserDefine2 = c.UserDefined2;
                                        param.UserDefine3 = c.UserDefined3;
                                        param.UserDefine4 = c.UserDefined4;
                                        param.UserDefine5 = c.UserDefined5;
                                        param.UserDefine6 = c.UserDefined6;
                                        param.UserDefine7 = c.UserDefined7;
                                        param.UserDefine8 = c.UserDefined8;
                                        param.UserDefine9 = c.UserDefined9;
                                        var txt = ORDOrder_Excel_GenCodeFromSetting(ref oCode, ref ListCode, param);
                                        c.Code = txt;
                                    }
                                }
                                break;
                            case 3: //Tất cả                                
                                var cETD = group.Min(c => c.ETD);
                                var cETA = group.Max(c => c.ETA);
                                foreach (var c in group)
                                {
                                    c.ETA = cETA;
                                    c.ETD = cETD;
                                }
                                if (oCode.Expr.Contains("ETD") && !cETD.HasValue)
                                {
                                    foreach (var c in group)
                                    {
                                        c.ExcelError += " Không thể tạo mã ĐH. Sai ETD";
                                        c.ExcelSuccess = false;
                                    }
                                }
                                else
                                {
                                    DTOORDOrder_Import_Code param = new DTOORDOrder_Import_Code();
                                    param.CustomerID = group.FirstOrDefault().CustomerID;
                                    param.CustomerCode = group.FirstOrDefault().CustomerCode;
                                    param.TypeOfOrder = group.FirstOrDefault().TransportModeName.ToUpper();
                                    param.ServiceOfOrderID = group.FirstOrDefault().ServiceOfOrderIDTemp;
                                    param.ETD = cETD;
                                    param.UserDefine1 = group.FirstOrDefault().UserDefined1;
                                    param.UserDefine2 = group.FirstOrDefault().UserDefined2;
                                    param.UserDefine3 = group.FirstOrDefault().UserDefined3;
                                    param.UserDefine4 = group.FirstOrDefault().UserDefined4;
                                    param.UserDefine5 = group.FirstOrDefault().UserDefined5;
                                    param.UserDefine6 = group.FirstOrDefault().UserDefined6;
                                    param.UserDefine7 = group.FirstOrDefault().UserDefined7;
                                    param.UserDefine8 = group.FirstOrDefault().UserDefined8;
                                    param.UserDefine9 = group.FirstOrDefault().UserDefined9;
                                    var tCode = ORDOrder_Excel_GenCodeFromSetting(ref oCode, ref ListCode, param);
                                    foreach (var c in group)
                                    {
                                        c.Code = tCode;
                                    }
                                }
                                break;
                        }
                    }
                    else
                    {
                        foreach (var cGroup in group.GroupBy(c => c.Code).ToList())
                        {
                            var ETD = cGroup.Min(c => c.ETD);
                            var ETA = cGroup.Max(c => c.ETA);
                            foreach (var c in cGroup)
                            {
                                c.ETA = ETA;
                                c.ETD = ETD;
                            }
                        }
                    }
                }
                foreach (var item in dataRes)
                {
                    if (!StringHelper.IsValidCode(item.Code) || item.Code.Length > 256)
                    {
                        if (item.ExcelSuccess)
                        {
                            item.ExcelSuccess = false;
                            item.ExcelError = "Mã ĐH không hợp lệ.";
                        }
                        else
                        {
                            item.ExcelError += "Mã ĐH không hợp lệ.";
                        }
                    }
                    var objCheck = data.ListCode.FirstOrDefault(c => c.CustomerID == item.CustomerID && c.Code == item.Code);
                    if (objCheck != null)
                    {
                        if (data.Setting.IsUniqueOrderCode)
                        {
                            item.ExcelSuccess = false;
                            item.ExcelError += " Trùng mã ĐH";
                        }
                        else
                        {
                            if (data.ListCode.Count(c => c.CustomerID == item.CustomerID && c.Code == item.Code && c.RequestDate == item.RequestDate) > 0)
                            {
                                item.ExcelSuccess = false;
                                item.ExcelError += " Trùng mã ĐH và ngày gửi yêu cầu.";
                            }
                        }
                    }
                }
                #endregion

                return dataRes;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private string ORDOrder_Excel_GenCodeFromSetting(ref DTOCUSSettingOrderCode obj, ref List<DTOORDOrder> ListCode, DTOORDOrder_Import_Code param)
        {
            var exp = obj.Expr;
            var idx = 1;

            if (obj.DicCounter == null)
                obj.DicCounter = new Dictionary<DateTime, int>();

            // Tất cả
            if (obj.ActionReset == 0)
            {
                idx = obj.SortOrder + 1;
                obj.SortOrder = idx;
            }
            else
            {
                // Theo thứ tự trong ngày
                if (obj.ActionReset == 1)
                {
                    if (exp.Contains("ETD") && param.ETD.HasValue)
                    {
                        if (obj.DicCounter.ContainsKey(param.ETD.Value.Date))
                        {
                            var dic = obj.DicCounter.FirstOrDefault(c => c.Key == param.ETD.Value.Date);
                            idx = dic.Value + 1;
                            obj.DicCounter.Remove(param.ETD.Value.Date);
                            obj.DicCounter.Add(param.ETD.Value.Date, idx);
                        }
                        else
                        {
                            obj.DicCounter.Add(param.ETD.Value.Date, idx);
                        }
                    }
                    else
                    {
                        if (obj.DicCounter.ContainsKey(DateTime.Now.Date))
                        {
                            var dic = obj.DicCounter.FirstOrDefault(c => c.Key == DateTime.Now.Date);
                            idx = dic.Value + 1;
                            obj.DicCounter.Remove(DateTime.Now.Date);
                            obj.DicCounter.Add(DateTime.Now.Date, idx);
                        }
                        else
                        {
                            obj.DicCounter.Add(DateTime.Now.Date, idx);
                        }
                    }
                }
            }

            var txt = string.Empty;
            txt = exp;
            txt = txt.Replace("[Day-D]", DateTime.Now.Day.ToString());
            txt = txt.Replace("[Day-DD]", DateTime.Now.Day.ToString("00"));
            txt = txt.Replace("[Month-M]", DateTime.Now.Month.ToString());
            txt = txt.Replace("[Month-MM]", DateTime.Now.Month.ToString("00"));
            txt = txt.Replace("[Year-YY]", (DateTime.Now.Year % 100).ToString("00"));
            txt = txt.Replace("[Year-YYYY]", DateTime.Now.Year.ToString("0000"));
            if (param.ETD.HasValue)
            {
                txt = txt.Replace("[DayETD-D]", param.ETD.Value.Day.ToString());
                txt = txt.Replace("[DayETD-DD]", param.ETD.Value.Day.ToString("00"));
                txt = txt.Replace("[MonthETD-M]", param.ETD.Value.Month.ToString());
                txt = txt.Replace("[MonthETD-MM]", param.ETD.Value.Month.ToString("00"));
                txt = txt.Replace("[YearETD-YY]", (param.ETD.Value.Year % 100).ToString("00"));
                txt = txt.Replace("[YearETD-YYYY]", param.ETD.Value.Year.ToString("0000"));
            }
            txt = txt.Replace("[CustomerCode]", param.CustomerCode);
            txt = txt.Replace("[TypeOrder]", param.TypeOfOrder);
            txt = txt.Replace("[UserDefine1]", param.UserDefine1);
            txt = txt.Replace("[UserDefine2]", param.UserDefine2);
            txt = txt.Replace("[UserDefine3]", param.UserDefine3);
            txt = txt.Replace("[UserDefine4]", param.UserDefine4);
            txt = txt.Replace("[UserDefine5]", param.UserDefine5);
            txt = txt.Replace("[UserDefine6]", param.UserDefine6);
            txt = txt.Replace("[UserDefine7]", param.UserDefine7);
            txt = txt.Replace("[UserDefine8]", param.UserDefine8);
            txt = txt.Replace("[UserDefine9]", param.UserDefine9);
            switch (param.ServiceOfOrderID)
            {
                case iIM:
                    txt = txt.Replace("[ServiceOfOrderEng]", "IM");
                    txt = txt.Replace("[ServiceOfOrderVi]", "T");
                    break;
                case iEx:
                    txt = txt.Replace("[ServiceOfOrderEng]", "EX");
                    txt = txt.Replace("[ServiceOfOrderVi]", "D");
                    break;
                case iLO:
                    txt = txt.Replace("[ServiceOfOrderEng]", "LO");
                    txt = txt.Replace("[ServiceOfOrderVi]", "C");
                    break;
                case iLOEmpty:
                    txt = txt.Replace("[ServiceOfOrderEng]", "LOEmpty");
                    txt = txt.Replace("[ServiceOfOrderVi]", "CR");
                    break;
                case iLOLaden:
                    txt = txt.Replace("[ServiceOfOrderEng]", "LOLaden");
                    txt = txt.Replace("[ServiceOfOrderVi]", "CD");
                    break;
            }

            // Tất cả theo đúng mã
            // [CustomerCode][ServiceOfOrderEng][Sort-000]
            // BIAEX-001
            // BIAIM-001
            string temp = txt.Replace("[Sort-000]", "");
            temp = txt.Replace("[Sort-00000]", "");
            temp = txt.Replace("[Sort-0000000]", "");
            if (obj.ActionReset == 2)
            {
                // Áp dụng tất cả khách hàng
                var lstCode = ListCode.Where(c => c.Code.ToLower().StartsWith(temp.ToLower()));
                // Áp dụng cho từng khách hàng
                if (obj.CustomerID != param.SYSCustomerID)
                    lstCode = lstCode.Where(c => c.CustomerID == param.CustomerID).OrderByDescending(c => c.Code);
                if (lstCode != null && lstCode.Count() > 0)
                {
                    var last = lstCode.FirstOrDefault();
                    // Lấy index 
                    string strIndex = last.Code.Split('-')[last.Code.Split('-').Length - 1];
                    if (!string.IsNullOrEmpty(strIndex))
                    {
                        try
                        {
                            idx = Convert.ToInt32(strIndex);
                            idx += 1;
                        }
                        catch { }
                    }
                }
            }

            txt = txt.Replace("[Sort-000]", "-" + idx.ToString("000"));
            txt = txt.Replace("[Sort-00000]", "-" + idx.ToString("00000"));
            txt = txt.Replace("[Sort-0000000]", "-" + idx.ToString("0000000"));
            if (obj.ActionReset == 2)
            {
                DTOORDOrder objCode = new DTOORDOrder();
                objCode.Code = txt;
                objCode.CustomerID = param.CustomerID;
                ListCode.Add(objCode);
            }

            return txt;
        }

        private void ORDOrder_Excel_ValidateSetting(DTOCUSSettingOrder obj)
        {
            if (obj.RequestDate < 1)
                throw new Exception("Chưa thiết lập ngày yêu cầu [RequestDate].");
            if (obj.SYSCustomerID == obj.CustomerID && obj.CustomerCode < 1)
                throw new Exception("Chưa thiết lập mã KH [CustomerCode].");
            if (obj.LocationToAddress < 1 && obj.LocationToCode < 1 && obj.LocationToCodeName < 1 && obj.LocationToName < 1)
                throw new Exception("Chưa thiết lập điểm giao hàng.");
        }

        private Dictionary<string, string> GetDataValue(ExcelWorksheet ws, object obj, int row, List<string> sValue)
        {
            Dictionary<string, string> result = new Dictionary<string, string>();
            foreach (var prop in obj.GetType().GetProperties())
            {
                try
                {
                    var p = prop.Name;
                    if (!sValue.Contains(p))
                    {
                        var v = (int)prop.GetValue(obj, null);
                        result.Add(p, v > 0 ? ExcelHelper.GetValue(ws, row, v) : string.Empty);
                    }
                }
                catch (Exception)
                {
                }
            }
            return result;
        }

        private Dictionary<string, string> GetDataName()
        {
            Dictionary<string, string> result = new Dictionary<string, string>();

            result.Add("OrderCode", "Mã ĐH");
            result.Add("UniqueOrderCode", "Mã ĐH duy nhất");
            result.Add("SOCode", "Số SO");
            result.Add("DNCode", "Số DN");
            result.Add("RequestDate", "Ngày yêu cầu");
            result.Add("ETD", "ETD");
            result.Add("ETDTime_RequestDate", "ETD theo ngày yêu cầu");
            result.Add("ETA", "ETA");
            result.Add("ETATime_RequestDate", "ETA theo ngày yêu cầu");
            result.Add("ETARequest", "Ngày yc giao hàng");
            result.Add("ETDRequest", "Ngày yc đến kho");
            result.Add("CustomerCode", "Mã khách hàng");
            result.Add("CustomerName", "Tên khách hàng");
            result.Add("CustomerCodeName", "Mã-Tên khách hàng");
            result.Add("DistributorCode", "Mã NPP");
            result.Add("DistributorName", "Tên NPP");
            result.Add("DistributorCodeName", "Mã-Tên NPP");
            result.Add("LocationFromCode", "Mã điểm nhận");
            result.Add("LocationFromName", "Tên điểm nhận");
            result.Add("LocationFromCodeName", "Mã-Tên điểm nhận");
            result.Add("LocationToCode", "Mã điểm giao");
            result.Add("LocationToCodeName", "Mã-Tên điểm giao");
            result.Add("LocationToName", "Tên điểm giao");
            result.Add("LocationToAddress", "Địa chỉ giao");
            result.Add("GroupProductCode", "Nhóm sản phẩm");
            result.Add("Packing", "Mã hàng hóa/ĐVT");
            result.Add("Ton", "Tấn");
            result.Add("CBM", "Khối");
            result.Add("Quantity", "Số lượng");
            result.Add("GroupVehicle", "Loại xe");
            result.Add("Note", "Ghi chú");
            result.Add("TypeOfTransportMode", "Loại vận chuyển");
            result.Add("ServiceOfOrder", "Dịch vụ");
            result.Add("EconomicZone", "EconomicZone");
            result.Add("RoutingAreaCode", "Mã khu vực");
            result.Add("UserDefine1", "Tùy chọn 1");
            result.Add("UserDefine2", "Tùy chọn 2");
            result.Add("UserDefine3", "Tùy chọn 3");
            result.Add("UserDefine4", "Tùy chọn 4");
            result.Add("UserDefine5", "Tùy chọn 5");
            result.Add("UserDefine6", "Tùy chọn 6");
            result.Add("UserDefine7", "Tùy chọn 7");
            result.Add("UserDefine8", "Tùy chọn 8");
            result.Add("UserDefine9", "Tùy chọn 9");
            result.Add("RequestTime", "Giờ yêu cầu ĐH");
            result.Add("ETARequestTime", "Giờ yêu cầu giao hàng");
            result.Add("ETDRequestTime", "Giờ yêu cầu đến kho");
            result.Add("RequestDate_Time", "Ngày giờ yêu cầu ĐH");
            result.Add("LocationToNote", "Ghi chú điểm giao");
            result.Add("LocationToNote1", "Ghi chú điểm giao 1");
            result.Add("GroupProductCodeNotUnicode", "Nhóm sản phẩm(ko dấu)");
            result.Add("PackingNotUnicode", "Sản phẩm(ko dấu)");
            result.Add("RoutingCode", "Mã cung đường");
            result.Add("IsHot", "Gấp");
            result.Add("CutOffTime", "CutOffTime");
            result.Add("CarrierCode", "Mã hãng tàu");
            result.Add("CarrierCodeName", "Mã-têụ hãng tàu");
            result.Add("CarrierName", "Tên hãng tàu");
            result.Add("VesselNo", "Số tàu");
            result.Add("VesselName", "Tên tàu");
            result.Add("TripNo", "Số chuyến");
            result.Add("ContainerNo", "Số con.");
            result.Add("SealNo1", "SealNo1");
            result.Add("SealNo2", "SealNo2");
            result.Add("TypeOfContainerName", "Loại container");
            result.Add("LocationDepotCode", "Mã depot");
            result.Add("LocationDepotName", "Tên depot");
            result.Add("LocationReturnCode", "Mã depot trả");
            result.Add("LocationReturnName", "Tên depot trả");
            result.Add("HasCashCollect", "Thu hộ");
            result.Add("PriceTOMaster", "Giá chuyến");
            result.Add("PriceTon", "Giá theo tấn");
            result.Add("PriceCBM", "Giá theo khối");
            result.Add("PriceQuantity", "Giá theo số lượng");
            result.Add("TemperatureMax", "NĐ tối đa");
            result.Add("TemperatureMin", "NĐ tối thiểu");
            result.Add("DateGetEmpty", "Ngày lấy rỗng");
            result.Add("Date_TimeGetEmpty", "Ngày/giờ lấy rỗng");
            result.Add("TimeGetEmpty", "Giờ lấy rỗng");
            result.Add("DateReturnEmpty", "Ngày trả rỗng");
            result.Add("Date_TimeReturnEmpty", "Ngày/giờ trả rỗng");
            result.Add("TimeReturnEmpty", "Giờ trả rỗng");
            result.Add("TypeOfWAInspectionStatus", "Miễn kiểm");
            result.Add("InspectionDate", "Tgian kiểm hóa");
            result.Add("ProductCodeWithoutGroup", "Mã SP không nhóm");
            result.Add("Note1", "Ghi chú 1");
            result.Add("Note2", "Ghi chú 2");
            result.Add("Ton_SKU", "Tấn SKU");
            result.Add("CBM_SKU", "Khối SKU");
            result.Add("Quantity_SKU", "Số lượng SKU");

            return result;
        }

        public List<DTOCUSGroupOfProduct> GroupOfProduct_Read(dynamic d)
        {
            try
            {
                int id = d.id;
                var result = new List<DTOCUSGroupOfProduct>();
                ServiceFactory.SVOrder((ISVOrder sv) =>
                {
                    result = sv.GroupOfProduct_List(id);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public SYSExcel ORDOrder_ExcelOnline_Init(dynamic dynParam)
        {
            try
            {
                var functionid = (int)dynParam.functionid;
                var functionkey = dynParam.functionkey.ToString();
                var templateID = (int)dynParam.templateID;
                var customerID = (int)dynParam.customerID;
                var pID = (int)dynParam.pID;
                var isreload = (bool)dynParam.isreload;

                var result = default(SYSExcel);

                ServiceFactory.SVOrder((ISVOrder sv) =>
                {
                    result = sv.ORDOrder_ExcelOnline_Init(templateID, customerID, pID, functionid, functionkey, isreload);
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
        public DTOORDOrder_ImportRowResult ORDOrder_ExcelOnline_Change(dynamic dynParam)
        {
            try
            {
                var id = (long)dynParam.id;
                var row = (int)dynParam.row;
                var templateID = (int)dynParam.templateID;
                var customerID = (int)dynParam.customerID;
                List<string> lstMessageError = Newtonsoft.Json.JsonConvert.DeserializeObject<List<string>>(dynParam.lstMessageError.ToString());
                List<Cell> cells = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Cell>>(dynParam.cells.ToString());

                DTOCUSSettingOrder objSetting = new DTOCUSSettingOrder();
                DTOORDOrder_ImportCheck data = new DTOORDOrder_ImportCheck();

                ServiceFactory.SVOrder((ISVOrder sv) =>
                {
                    objSetting = sv.ORDOrder_Excel_Setting_Get(templateID);
                    data = sv.ORDOrder_ExcelOnline_Import_Data(customerID);
                });

                int transportID = -1;
                int tmID = -1;
                var address = "";
                var economicZone = "";
                var customerCode = "";
                var distributorName = "";
                var distributorCode = "";
                var distributorCodeName = "";
                var locationToCode = "";
                var locationToName = "";
                var locationToCodeName = "";
                var locationToNote = "";
                var routingAreaCode = "";
                var transportModeCode = "";
                List<int> excelErrorCustomer = new List<int>();
                List<int> excelErrorLocation = new List<int>();
                DTOORDOrder_ImportOnline objImport = new DTOORDOrder_ImportOnline();

                #region Get Data
                var isFailed = false;
                List<int> lstRowError = new List<int>();
                foreach (var item in cells)
                {
                    //Check value in cell
                    if (item.Value != null)
                    {
                        Type t = item.Value.GetType();

                        if (t == typeof(JObject))
                        {
                            lstRowError.Add(item.Index.Value + 1);
                            isFailed = true;
                        }
                        if (item.Index + 1 == objSetting.TypeOfTransportMode)
                        {
                            transportModeCode = item.Value.ToString().Trim();
                        }
                        else if (item.Index + 1 == objSetting.LocationToAddress)
                        {
                            address = item.Value.ToString().Trim();
                        }
                        else if (item.Index + 1 == objSetting.EconomicZone)
                        {
                            economicZone = item.Value.ToString().Trim();
                        }
                        else if (item.Index + 1 == objSetting.RoutingAreaCode)
                        {
                            routingAreaCode = item.Value.ToString().Trim();
                        }
                        else if (item.Index + 1 == objSetting.CustomerCode)
                        {
                            customerCode = item.Value.ToString().Trim();
                        }
                        else if (item.Index + 1 == objSetting.DistributorName)
                        {
                            distributorName = item.Value.ToString().Trim();
                        }
                        else if (item.Index + 1 == objSetting.DistributorCode)
                        {
                            distributorCode = item.Value.ToString().Trim();
                        }
                        else if (item.Index + 1 == objSetting.DistributorCodeName)
                        {
                            distributorCodeName = item.Value.ToString().Trim();
                        }
                        else if (item.Index + 1 == objSetting.LocationToCode)
                        {
                            locationToCode = item.Value.ToString().Trim();
                        }
                        else if (item.Index + 1 == objSetting.LocationToName)
                        {
                            locationToName = item.Value.ToString().Trim();
                        }
                        else if (item.Index + 1 == objSetting.LocationToCodeName)
                        {
                            locationToCodeName = item.Value.ToString().Trim();
                        }
                        else if (item.Index + 1 == objSetting.LocationToNote)
                        {
                            locationToNote = item.Value.ToString().Trim();
                        }
                    }
                }
                if (isFailed)
                {
                    throw new Exception("Dòng " + (row + 1) + " cell: " + string.Join(",", lstRowError) + " sai dữ liệu.");
                }
                #endregion

                #region Check TransportMode
                //TM của setting
                if (objSetting.TypeOfTransportModeID > 0)
                {
                    tmID = objSetting.TypeOfTransportModeID;
                    var objTM = data.ListTransportMode.FirstOrDefault(c => c.ID == tmID);
                    if (objTM != null)
                        transportID = objTM.TransportModeID;
                }
                else
                {
                    if (objSetting.TypeOfTransportMode < 1)
                    {
                        //throw new Exception("Chưa thiết lập cột loại vận chuyển.");
                        excelErrorCustomer.Add(5);
                    }

                    var str = transportModeCode.Trim().ToLower();
                    if (!string.IsNullOrEmpty(str))
                    {
                        var objTM = data.ListTransportMode.FirstOrDefault(c => c.Code.ToLower() == str.Trim().ToLower());
                        if (objTM != null)
                        {
                            tmID = objTM.ID;
                            transportID = objTM.TransportModeID;
                        }
                        else
                        {
                            //throw new Exception("Dòng [" + row + "], loại vận chuyển [" + str + "]  không xác định.");
                            excelErrorCustomer.Add(6);
                        }
                    }
                    else
                    {
                        //throw new Exception("Dòng [" + row + "] không xác định loại vận chuyển.");
                        excelErrorCustomer.Add(7);
                    }
                }
                #endregion

                #region Check Customer
                if (objSetting.CustomerID == objSetting.SYSCustomerID)
                {
                    if (string.IsNullOrEmpty(customerCode))
                    {
                        //excelErrorCustomer.Add("Thiếu mã KH.");
                        excelErrorCustomer.Add(0);
                        objImport.CustomerID = -1;
                    }
                    else
                    {
                        var objCheck = data.ListCustomer.FirstOrDefault(c => c.Code.Trim().ToLower() == customerCode.Trim().ToLower());
                        if (objCheck == null)
                        {
                            //excelErrorCustomer.Add("KH [" + customerCode + "] không tồn tại.");
                            excelErrorCustomer.Add(1);
                            objImport.CustomerID = -1;
                        }
                        else
                        {
                            objImport.CustomerID = objCheck.ID;
                            objImport.CustomerCode = objCheck.Code;
                            objImport.IsCreateLocation = objCheck.IsCreateLocation;
                            objImport.IsCreatePartner = objCheck.IsCreatePartner;
                        }
                    }
                }
                else
                {
                    objImport.CustomerID = objSetting.CustomerID;
                    var objCheck = data.ListCustomer.FirstOrDefault(c => c.ID == objImport.CustomerID);
                    if (objCheck == null)
                    {
                        //excelErrorCustomer.Add("KH không tồn tại.");
                        excelErrorCustomer.Add(1);
                        objImport.CustomerID = -1;
                    }
                    else
                    {
                        objImport.CustomerCode = objCheck.Code;
                        objImport.IsCreateLocation = objCheck.IsCreateLocation;
                        objImport.IsCreatePartner = objImport.IsCreatePartner;
                    }
                }
                #endregion

                if (transportID > 0 && transportID != iFCL)
                {
                    #region Check nhà phân phối
                    ////Nếu điểm giao trống
                    if (string.IsNullOrEmpty(address) && string.IsNullOrEmpty(locationToCode) && string.IsNullOrEmpty(locationToCodeName) && string.IsNullOrEmpty(locationToName))
                        excelErrorLocation.Add(57);

                    var isLocationToFail = false;
                    var sLocation = new List<AddressSearchItem>();
                    var sPartnerLocation = new List<DTOORDOrder_Import_PartnerLocation>();

                    var pID = -1;//
                    var toID = -1;//
                    var toCode = string.Empty;//
                    var toName = string.Empty;//

                    string dName = distributorName;//
                    string dCode = distributorCode;//

                    if (!string.IsNullOrEmpty(distributorCodeName))
                    {
                        string[] s = distributorCodeName.Split('-');
                        dCode = s[0];
                        if (s.Length > 1)
                        {
                            dName = distributorCodeName.Substring(dCode.Length + 1);
                        }
                    }

                    if (!string.IsNullOrEmpty(dCode))
                    {
                        var objCheck = data.ListDistributor.FirstOrDefault(c => !string.IsNullOrEmpty(c.PartnerCode) && c.PartnerCode.Trim().ToLower() == dCode.Trim().ToLower() && c.CustomerID == objImport.CustomerID);
                        if (objCheck != null)
                        {
                            pID = objCheck.CUSPartnerID;
                            dCode = objCheck.PartnerCode;

                            if (!string.IsNullOrEmpty(dName))
                                dName = objCheck.PartnerName;

                            toCode = locationToCode;
                            toName = locationToName;
                            if (objSetting.LocationToCodeName > 0)
                            {
                                if (!string.IsNullOrEmpty(locationToCodeName))
                                {
                                    toCode = locationToCodeName.Split('-').FirstOrDefault();
                                    toName = locationToCodeName.Split('-').Skip(1).FirstOrDefault();
                                }
                                else
                                {
                                    toCode = string.Empty;
                                    toName = string.Empty;
                                }
                            }
                            if (objSetting.SuggestLocationToCode == true)
                                toCode = string.Empty;

                            //Tìm theo code
                            var objTo = data.ListDistributorLocation.FirstOrDefault(c => c.CusPartID == pID && c.LocationCode.Trim().ToLower() == toCode.Trim().ToLower());
                            if (objTo != null)
                            {
                                toID = objTo.CUSLocationID;
                                var objSearch = new AddressSearchItem();
                                objSearch.CUSLocationID = toID;
                                objSearch.CustomerID = objImport.CustomerID;
                                objSearch.LocationCode = objTo.LocationCode;
                                objSearch.Address = objTo.Address;
                                objSearch.CUSPartnerID = pID;
                                sLocation.Add(objSearch);
                            }
                            else if (objSetting.SuggestLocationToCode || objSetting.LocationToCode <= 0)
                            {
                                //Tìm và gợi ý địa chỉ.
                                try
                                {
                                    int total = 0;
                                    sLocation = AddressSearchHelper.Search(objImport.CustomerID, pID, economicZone, address, 0, 100, ref total);

                                    if (sLocation.Count != 0)
                                    {
                                        var isCheckLocationFailed = false;
                                        foreach (var location in sLocation)
                                        {
                                            var objLocation = data.ListDistributorLocation.FirstOrDefault(c => c.CUSLocationID == location.CUSLocationID);
                                            if (objLocation == null)
                                            {
                                                isCheckLocationFailed = true;
                                                break;
                                            }
                                        }
                                        if (isCheckLocationFailed)
                                        {
                                            var lstAddressSearchItem = new List<AddressSearchItem>();
                                            ServiceFactory.SVCategory((ISVCategory sv) =>
                                            {
                                                lstAddressSearchItem = sv.AddressSearch_List();
                                            });
                                            //var obj = lst.Where(c => c.CUSLocationID == 10616).ToList();
                                            lstAddressSearchItem = lstAddressSearchItem.Where(c => c.Address != null && c.PartnerCode != null).ToList();
                                            AddressSearchHelper.Create(lstAddressSearchItem);

                                            sLocation = AddressSearchHelper.Search(objImport.CustomerID, pID, economicZone, address, 0, 100, ref total);
                                        }
                                    }

                                    if (sLocation.Count == 0)
                                    {
                                        //excelErrorLocation.Add("Địa chỉ (" + address + ") ko tồn tại");
                                        excelErrorLocation.Add(2);
                                        isLocationToFail = true;
                                    }
                                    else if (sLocation[0].Address != address)
                                    {
                                        var flag = true;
                                        for (int i = 1; i < sLocation.Count; i++)
                                        {
                                            if (sLocation[i].Address == address)
                                            {
                                                var o = sLocation[i];
                                                sLocation.RemoveAt(i);
                                                sLocation.Insert(0, o);
                                                flag = false;
                                                break;
                                            }
                                        }
                                        isLocationToFail = flag;
                                        //if (isLocationToFail)
                                        //{
                                        //    excelErrorLocation.Add(2);
                                        //}
                                    }
                                }
                                catch (Exception)
                                {
                                    isLocationToFail = true;
                                    //excelErrorLocation.Add("Địa chỉ (" + address + ") ko tồn tại");
                                    excelErrorLocation.Add(2);
                                }
                            }
                            else
                            {
                                isLocationToFail = true;
                                //Sai ma dia chi
                                excelErrorLocation.Add(92);
                            }

                            DTOORDOrder_Import_PartnerLocation objPartner = new DTOORDOrder_Import_PartnerLocation();
                            objPartner.PartnerID = 0;
                            if (pID > 0)
                                objPartner.PartnerID = pID;
                            objPartner.CustomerID = objImport.CustomerID;
                            objPartner.PartnerCode = dCode;
                            objPartner.PartnerName = dName;
                            objPartner.LocationAddress = address;
                            objPartner.EconomicZone = economicZone;
                            objPartner.RoutingAreaCode = routingAreaCode;
                            objPartner.RouteDescription = locationToNote;
                            objPartner.LocationCode = locationToCode;

                            sPartnerLocation.Add(objPartner);

                            if (!string.IsNullOrEmpty(economicZone))
                            {
                                var objCus = data.ListCustomer.FirstOrDefault(c => c.ID == objImport.CustomerID);
                                if (objCus != null && objCus.IsFindEconomicZone == true)
                                {
                                    var objRoute = data.ListRoute.FirstOrDefault(c => c.CustomerID == objImport.CustomerID && c.Code.Trim().ToLower() == economicZone.Trim().ToLower());
                                    if (objRoute != null && objRoute.RoutingAreaToID > 0)
                                    {
                                        var objRouteArea = data.ListRouteArea.FirstOrDefault(c => c.ProvinceID > 0 && c.DistrictID > 0 && c.RoutingAreaID == objRoute.RoutingAreaToID);
                                        if (objRouteArea == null)
                                            objRouteArea = data.ListRouteArea.FirstOrDefault(c => c.ProvinceID > 0 && c.RoutingAreaID == objRoute.RoutingAreaToID);
                                        if (objRouteArea != null)
                                        {
                                            objPartner.ProvinceID = objRouteArea.ProvinceID.Value;
                                            if (objRouteArea.DistrictID.HasValue)
                                                objPartner.DistrictID = objRouteArea.DistrictID.Value;
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            isLocationToFail = true;
                            //excelErrorLocation.Add("Npp [" + dCode + "] không tồn tại.");
                            excelErrorLocation.Add(3);
                            toCode = locationToCode;
                            toName = locationToName;
                        }
                    }
                    else
                    {
                        //excelErrorLocation.Add("Npp không xác định.");
                        excelErrorLocation.Add(4);
                        toCode = locationToCode;
                        toName = locationToName;
                    }

                    objImport.LocationToID = toID;
                    objImport.LocationToAddress = address;
                    if (objImport.LocationToID < 0)
                    {
                        objImport.LocationToID = -1;
                        if (string.IsNullOrEmpty(toCode))
                        {
                            toCode = locationToCode;
                        }
                    }
                    if (objImport.LocationToID < 1 && !isLocationToFail)
                    {
                        if (sLocation.Count > 0)
                        {
                            objImport.LocationToID = sLocation.FirstOrDefault().CUSLocationID;
                            toCode = sLocation.FirstOrDefault().LocationCode;
                            toName = "";
                        }
                        else
                            objImport.LocationToID = -1;
                    }
                    objImport.LocationToCode = toCode;
                    objImport.LocationToName = toName;
                    objImport.PartnerID = pID;
                    objImport.PartnerCode = dCode;
                    objImport.PartnerName = dName;
                    objImport.IsLocationToFail = isLocationToFail;
                    objImport.ListPartnerLocation = new List<DTOORDOrder_Import_PartnerLocation>();
                    objImport.ListPartnerLocation.AddRange(sPartnerLocation);
                    objImport.sLocation = new List<AddressSearchItem>();
                    objImport.sLocation.AddRange(sLocation);
                    #endregion
                }

                objImport.TransportModeID = tmID;
                objImport.ExcelErrorCustomer = new List<int>();
                objImport.ExcelErrorCustomer.AddRange(excelErrorCustomer);

                objImport.ExcelErrorLocation = new List<int>();
                objImport.ExcelErrorLocation.AddRange(excelErrorLocation);

                var result = default(DTOORDOrder_ImportRowResult);
                if (id > 0 && cells.Count > 0 && row > 0)
                {
                    ServiceFactory.SVOrder((ISVOrder sv) =>
                    {
                        result = sv.ORDOrder_ExcelOnline_Change(templateID, customerID, objImport, id, row, cells, lstMessageError);
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
        public DTOORDOrder_ImportResult ORDOrder_ExcelOnline_Import(dynamic dynParam)
        {
            try
            {
                var id = (long)dynParam.id;
                var templateID = (int)dynParam.templateID;
                var customerID = (int)dynParam.customerID;
                List<Worksheet> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Worksheet>>(dynParam.worksheets.ToString());

                List<string> lstMessageError = Newtonsoft.Json.JsonConvert.DeserializeObject<List<string>>(dynParam.lstMessageError.ToString());
                //List<Cell> cells = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Cell>>(dynParam.cells.ToString());

                DTOCUSSettingOrder objSetting = new DTOCUSSettingOrder();
                DTOORDOrder_ImportCheck data = new DTOORDOrder_ImportCheck();

                ServiceFactory.SVOrder((ISVOrder sv) =>
                {
                    objSetting = sv.ORDOrder_Excel_Setting_Get(templateID);
                    data = sv.ORDOrder_ExcelOnline_Import_Data(customerID);
                });

                List<DTOORDOrder_ImportOnline> lstDetail = new List<DTOORDOrder_ImportOnline>();
                var lstRows = lst[0].Rows.Where(c => c.Cells != null).ToList();
                foreach (var eRow in lstRows)
                {
                    if (eRow.Index == 0 || eRow.Index < objSetting.RowStart - 1) continue;
                    var cells = eRow.Cells;
                    int transportID = -1;
                    int tmID = -1;
                    var address = "";
                    var economicZone = "";
                    var customerCode = "";
                    var distributorName = "";
                    var distributorCode = "";
                    var distributorCodeName = "";
                    var locationToCode = "";
                    var locationToName = "";
                    var locationToCodeName = "";
                    var locationToNote = "";
                    var routingAreaCode = "";
                    var transportModeCode = "";
                    List<int> excelErrorCustomer = new List<int>();
                    List<int> excelErrorLocation = new List<int>();
                    DTOORDOrder_ImportOnline objImport = new DTOORDOrder_ImportOnline();

                    #region Get Data
                    var isFailed = false;
                    List<int> lstRowError = new List<int>();
                    foreach (var item in cells)
                    {
                        if (item.Value != null)
                        {
                            Type t = item.Value.GetType();

                            if (t == typeof(JObject))
                            {
                                lstRowError.Add(item.Index.Value + 1);
                                isFailed = true;
                            }
                            if (item.Index + 1 == objSetting.TypeOfTransportMode)
                            {
                                transportModeCode = item.Value.ToString().Trim();
                            }
                            else if (item.Index + 1 == objSetting.LocationToAddress)
                            {
                                address = item.Value.ToString().Trim();
                            }
                            else if (item.Index + 1 == objSetting.EconomicZone)
                            {
                                economicZone = item.Value.ToString().Trim();
                            }
                            else if (item.Index + 1 == objSetting.RoutingAreaCode)
                            {
                                routingAreaCode = item.Value.ToString().Trim();
                            }
                            else if (item.Index + 1 == objSetting.CustomerCode)
                            {
                                customerCode = item.Value.ToString().Trim();
                            }
                            else if (item.Index + 1 == objSetting.DistributorName)
                            {
                                distributorName = item.Value.ToString().Trim();
                            }
                            else if (item.Index + 1 == objSetting.DistributorCode)
                            {
                                distributorCode = item.Value.ToString().Trim();
                            }
                            else if (item.Index + 1 == objSetting.DistributorCodeName)
                            {
                                distributorCodeName = item.Value.ToString().Trim();
                            }
                            else if (item.Index + 1 == objSetting.LocationToCode)
                            {
                                locationToCode = item.Value.ToString().Trim();
                            }
                            else if (item.Index + 1 == objSetting.LocationToName)
                            {
                                locationToName = item.Value.ToString().Trim();
                            }
                            else if (item.Index + 1 == objSetting.LocationToCodeName)
                            {
                                locationToCodeName = item.Value.ToString().Trim();
                            }
                            else if (item.Index + 1 == objSetting.LocationToNote)
                            {
                                locationToNote = item.Value.ToString().Trim();
                            }
                        }
                    }

                    if (isFailed)
                    {
                        throw new Exception("Dòng " + (eRow.Index.Value + 1) + " cell: " + string.Join(",", lstRowError) + " sai dữ liệu.");
                    }
                    #endregion

                    #region Check TransportMode
                    //TM của setting
                    if (objSetting.TypeOfTransportModeID > 0)
                    {
                        tmID = objSetting.TypeOfTransportModeID;
                        var objTM = data.ListTransportMode.FirstOrDefault(c => c.ID == tmID);
                        if (objTM != null)
                            transportID = objTM.TransportModeID;
                    }
                    else
                    {
                        if (objSetting.TypeOfTransportMode < 1)
                        {
                            //throw new Exception("Chưa thiết lập cột loại vận chuyển.");
                            excelErrorCustomer.Add(5);
                        }

                        var str = transportModeCode.Trim().ToLower();
                        if (!string.IsNullOrEmpty(str))
                        {
                            var objTM = data.ListTransportMode.FirstOrDefault(c => c.Code.ToLower() == str.Trim().ToLower());
                            if (objTM != null)
                            {
                                tmID = objTM.ID;
                                transportID = objTM.TransportModeID;
                            }
                            else
                            {
                                //throw new Exception("Dòng [" + row + "], loại vận chuyển [" + str + "]  không xác định.");
                                excelErrorCustomer.Add(6);
                            }
                        }
                        else
                        {
                            //throw new Exception("Dòng [" + row + "] không xác định loại vận chuyển.");
                            excelErrorCustomer.Add(7);
                        }
                    }
                    #endregion

                    #region Check Customer
                    if (objSetting.CustomerID == objSetting.SYSCustomerID)
                    {
                        if (string.IsNullOrEmpty(customerCode))
                        {
                            //excelErrorCustomer.Add("Thiếu mã KH.");
                            excelErrorCustomer.Add(0);
                            objImport.CustomerID = -1;
                        }
                        else
                        {
                            var objCheck = data.ListCustomer.FirstOrDefault(c => c.Code.Trim().ToLower() == customerCode.Trim().ToLower());
                            if (objCheck == null)
                            {
                                //excelErrorCustomer.Add("KH [" + customerCode + "] không tồn tại.");
                                excelErrorCustomer.Add(1);
                                objImport.CustomerID = -1;
                            }
                            else
                            {
                                objImport.CustomerID = objCheck.ID;
                                objImport.CustomerCode = objCheck.Code;
                                objImport.IsCreateLocation = objCheck.IsCreateLocation;
                                objImport.IsCreatePartner = objCheck.IsCreatePartner;
                            }
                        }
                    }
                    else
                    {
                        objImport.CustomerID = objSetting.CustomerID;
                        var objCheck = data.ListCustomer.FirstOrDefault(c => c.ID == objImport.CustomerID);
                        if (objCheck == null)
                        {
                            //excelErrorCustomer.Add("KH không tồn tại.");
                            excelErrorCustomer.Add(1);
                            objImport.CustomerID = -1;
                        }
                        else
                        {
                            objImport.CustomerCode = objCheck.Code;
                            objImport.IsCreateLocation = objCheck.IsCreateLocation;
                            objImport.IsCreatePartner = objCheck.IsCreatePartner;
                        }
                    }
                    #endregion

                    if (transportID > 0 && transportID != iFCL)
                    {
                        #region Check nhà phân phối
                        ////Nếu điểm giao trống
                        if (string.IsNullOrEmpty(address) && string.IsNullOrEmpty(locationToCode) && string.IsNullOrEmpty(locationToCodeName) && string.IsNullOrEmpty(locationToName))
                            excelErrorLocation.Add(57);

                        var isLocationToFail = false;
                        var sLocation = new List<AddressSearchItem>();
                        var sPartnerLocation = new List<DTOORDOrder_Import_PartnerLocation>();

                        var pID = -1;//
                        var toID = -1;//
                        var toCode = string.Empty;//
                        var toName = string.Empty;//

                        string dName = distributorName;//
                        string dCode = distributorCode;//

                        if (!string.IsNullOrEmpty(distributorCodeName))
                        {
                            string[] s = distributorCodeName.Split('-');
                            dCode = s[0];
                            if (s.Length > 1)
                            {
                                dName = distributorCodeName.Substring(dCode.Length + 1);
                            }
                        }

                        if (!string.IsNullOrEmpty(dCode))
                        {
                            var objCheck = data.ListDistributor.FirstOrDefault(c => !string.IsNullOrEmpty(c.PartnerCode) && c.PartnerCode.Trim().ToLower() == dCode.Trim().ToLower() && c.CustomerID == objImport.CustomerID);
                            if (objCheck != null)
                            {
                                pID = objCheck.CUSPartnerID;
                                dCode = objCheck.PartnerCode;

                                if (!string.IsNullOrEmpty(dName))
                                    dName = objCheck.PartnerName;

                                toCode = locationToCode;
                                toName = locationToName;
                                if (objSetting.LocationToCodeName > 0)
                                {
                                    if (!string.IsNullOrEmpty(locationToCodeName))
                                    {
                                        toCode = locationToCodeName.Split('-').FirstOrDefault();
                                        toName = locationToCodeName.Split('-').Skip(1).FirstOrDefault();
                                    }
                                    else
                                    {
                                        toCode = string.Empty;
                                        toName = string.Empty;
                                    }
                                }
                                if (objSetting.SuggestLocationToCode == true)
                                    toCode = string.Empty;

                                //Tìm theo code
                                var objTo = data.ListDistributorLocation.FirstOrDefault(c => c.CusPartID == pID && c.LocationCode.Trim().ToLower() == toCode.Trim().ToLower());
                                if (objTo != null)
                                {
                                    toID = objTo.CUSLocationID;
                                    var objSearch = new AddressSearchItem();
                                    objSearch.CUSLocationID = toID;
                                    objSearch.CustomerID = objImport.CustomerID;
                                    objSearch.LocationCode = objTo.LocationCode;
                                    objSearch.Address = objTo.Address;
                                    objSearch.CUSPartnerID = pID;
                                    sLocation.Add(objSearch);
                                }
                                else if (objSetting.SuggestLocationToCode || objSetting.LocationToCode <= 0)
                                {
                                    //Tìm và gợi ý địa chỉ.
                                    try
                                    {
                                        int total = 0;
                                        sLocation = AddressSearchHelper.Search(objImport.CustomerID, pID, economicZone, address, 0, 100, ref total);

                                        if (sLocation.Count != 0)
                                        {
                                            var isCheckLocationFailed = false;
                                            foreach (var location in sLocation)
                                            {
                                                var objLocation = data.ListDistributorLocation.FirstOrDefault(c => c.CUSLocationID == location.CUSLocationID);
                                                if (objLocation == null)
                                                {
                                                    isCheckLocationFailed = true;
                                                    break;
                                                }
                                            }
                                            if (isCheckLocationFailed)
                                            {
                                                var lstAddressSearchItem = new List<AddressSearchItem>();
                                                ServiceFactory.SVCategory((ISVCategory sv) =>
                                                {
                                                    lstAddressSearchItem = sv.AddressSearch_List();
                                                });
                                                //var obj = lst.Where(c => c.CUSLocationID == 10616).ToList();
                                                lstAddressSearchItem = lstAddressSearchItem.Where(c => c.Address != null && c.PartnerCode != null).ToList();
                                                AddressSearchHelper.Create(lstAddressSearchItem);

                                                sLocation = AddressSearchHelper.Search(objImport.CustomerID, pID, economicZone, address, 0, 100, ref total);
                                            }
                                        }

                                        if (sLocation.Count == 0)
                                        {
                                            //excelErrorLocation.Add("Địa chỉ (" + address + ") ko tồn tại");
                                            excelErrorLocation.Add(2);
                                            isLocationToFail = true;
                                        }
                                        else if (sLocation[0].Address != address)
                                        {
                                            var flag = true;
                                            for (int i = 1; i < sLocation.Count; i++)
                                            {
                                                if (sLocation[i].Address == address)
                                                {
                                                    var o = sLocation[i];
                                                    sLocation.RemoveAt(i);
                                                    sLocation.Insert(0, o);
                                                    flag = false;
                                                    break;
                                                }
                                            }
                                            isLocationToFail = flag;
                                            //if (isLocationToFail)
                                            //{
                                            //    excelErrorLocation.Add(2);
                                            //}
                                        }
                                    }
                                    catch (Exception)
                                    {
                                        isLocationToFail = true;
                                        //excelErrorLocation.Add("Địa chỉ (" + address + ") ko tồn tại");
                                        excelErrorLocation.Add(2);
                                    }
                                }
                                else
                                {
                                    isLocationToFail = true;
                                    //Sai ma dia chi
                                    excelErrorLocation.Add(92);
                                }

                                DTOORDOrder_Import_PartnerLocation objPartner = new DTOORDOrder_Import_PartnerLocation();
                                objPartner.PartnerID = 0;
                                if (pID > 0)
                                    objPartner.PartnerID = pID;
                                objPartner.CustomerID = objImport.CustomerID;
                                objPartner.PartnerCode = dCode;
                                objPartner.PartnerName = dName;
                                objPartner.LocationAddress = address;
                                objPartner.EconomicZone = economicZone;
                                objPartner.RoutingAreaCode = routingAreaCode;
                                objPartner.RouteDescription = locationToNote;
                                objPartner.LocationCode = locationToCode;

                                sPartnerLocation.Add(objPartner);

                                if (!string.IsNullOrEmpty(economicZone))
                                {
                                    var objCus = data.ListCustomer.FirstOrDefault(c => c.ID == objImport.CustomerID);
                                    if (objCus != null && objCus.IsFindEconomicZone == true)
                                    {
                                        var objRoute = data.ListRoute.FirstOrDefault(c => c.CustomerID == objImport.CustomerID && c.Code.Trim().ToLower() == economicZone.Trim().ToLower());
                                        if (objRoute != null && objRoute.RoutingAreaToID > 0)
                                        {
                                            var objRouteArea = data.ListRouteArea.FirstOrDefault(c => c.ProvinceID > 0 && c.DistrictID > 0 && c.RoutingAreaID == objRoute.RoutingAreaToID);
                                            if (objRouteArea == null)
                                                objRouteArea = data.ListRouteArea.FirstOrDefault(c => c.ProvinceID > 0 && c.RoutingAreaID == objRoute.RoutingAreaToID);
                                            if (objRouteArea != null)
                                            {
                                                objPartner.ProvinceID = objRouteArea.ProvinceID.Value;
                                                if (objRouteArea.DistrictID.HasValue)
                                                    objPartner.DistrictID = objRouteArea.DistrictID.Value;
                                            }
                                        }
                                    }
                                }
                            }
                            else
                            {
                                isLocationToFail = true;
                                //excelErrorLocation.Add("Npp [" + dCode + "] không tồn tại.");
                                excelErrorLocation.Add(3);
                                toCode = locationToCode;
                                toName = locationToName;
                            }
                        }
                        else
                        {
                            //excelErrorLocation.Add("Npp không xác định.");
                            excelErrorLocation.Add(4);
                            toCode = locationToCode;
                            toName = locationToName;
                        }

                        objImport.LocationToID = toID;
                        objImport.LocationToAddress = address;

                        if (objImport.LocationToID < 0)
                        {
                            objImport.LocationToID = -1;
                            if (string.IsNullOrEmpty(toCode))
                            {
                                toCode = locationToCode;
                            }
                        }

                        if (objImport.LocationToID < 1 && !isLocationToFail)
                        {
                            if (sLocation.Count > 0)
                            {
                                objImport.LocationToID = sLocation.FirstOrDefault().CUSLocationID;
                                toCode = sLocation.FirstOrDefault().LocationCode;
                                toName = "";
                            }
                            else
                                objImport.LocationToID = -1;
                        }

                        objImport.LocationToCode = toCode;
                        objImport.LocationToName = toName;
                        objImport.PartnerID = pID;
                        objImport.PartnerCode = dCode;
                        objImport.PartnerName = dName;
                        objImport.IsLocationToFail = isLocationToFail;
                        objImport.ListPartnerLocation = new List<DTOORDOrder_Import_PartnerLocation>();
                        objImport.ListPartnerLocation.AddRange(sPartnerLocation);
                        objImport.sLocation = new List<AddressSearchItem>();
                        objImport.sLocation.AddRange(sLocation);
                        #endregion
                    }

                    objImport.TransportModeID = tmID;
                    objImport.ExcelErrorCustomer = new List<int>();
                    objImport.ExcelErrorCustomer.AddRange(excelErrorCustomer);

                    objImport.ExcelErrorLocation = new List<int>();
                    objImport.ExcelErrorLocation.AddRange(excelErrorLocation);

                    objImport.Index = eRow.Index.GetValueOrDefault();
                    lstDetail.Add(objImport);
                }


                var result = default(DTOORDOrder_ImportResult);
                if (id > 0 && lst.Count > 0 && lstRows.Count > 1)
                {
                    ServiceFactory.SVOrder((ISVOrder sv) =>
                    {
                        result = sv.ORDOrder_ExcelOnline_Import(templateID, customerID, id, lstRows, lstDetail, lstMessageError);
                    });
                }
                if (result != null && result.SYSExcel != null && !string.IsNullOrEmpty(result.SYSExcel.Data))
                {
                    result.SYSExcel.Worksheets = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Worksheet>>(result.SYSExcel.Data);
                }
                else
                {
                    result.SYSExcel = new SYSExcel();
                    result.SYSExcel.Worksheets = new List<Worksheet>();
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DTOORDOrder_ExcelOnline_ApproveResult ORDOrder_ExcelOnline_Approve(dynamic dynParam)
        {
            try
            {
                var id = (long)dynParam.id;
                var templateID = (int)dynParam.templateID;
                CATFile file = Newtonsoft.Json.JsonConvert.DeserializeObject<CATFile>(dynParam.File.ToString());

                DTOORDOrder_ExcelOnline_ApproveResult result = new DTOORDOrder_ExcelOnline_ApproveResult();
                if (id > 0)
                {
                    ServiceFactory.SVOrder((ISVOrder sv) =>
                    {
                        result = sv.ORDOrder_ExcelOnline_Approve(id, templateID, file);
                    });
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ORDOrder_ExcelOnline_LocationToSave(dynamic dynParam)
        {
            try
            {
                var id = (long)dynParam.id;
                var templateID = (int)dynParam.templateID;
                List<DTOORDOrder_ImportRowResult> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DTOORDOrder_ImportRowResult>>(dynParam.lst.ToString());

                if (id > 0)
                {
                    ServiceFactory.SVOrder((ISVOrder sv) =>
                    {
                        sv.ORDOrder_ExcelOnline_LocationToSave(id, templateID, lst);
                    });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public string ORDOrder_ExcelOnline_Export(dynamic dynParam)
        {
            try
            {
                //var result = string.Empty;
                var pID = (int)dynParam.pID;
                var templateID = (int)dynParam.TemplateID;
                var objSettingOrder = new DTOCUSSettingOrder();
                var dataExport = new List<DTOORDOrder_Export>();
                var customerID = (int)dynParam.customerID;
                DTOORDOrder_ImportCheck data = new DTOORDOrder_ImportCheck();
                //CATFile file = Newtonsoft.Json.JsonConvert.DeserializeObject<CATFile>(dynParam.File.ToString());
                ServiceFactory.SVOrder((ISVOrder sv) =>
                {
                    dataExport = sv.ORDOrder_Excel_Export(templateID, pID);
                    objSettingOrder = sv.ORDOrder_Excel_Setting_Get(templateID);
                    data = sv.ORDOrder_Excel_Import_Data(customerID);
                });

                //string[] name = file.FileName.Split('.').Reverse().Skip(1).Reverse().ToArray();
                //result = "/" + FolderUpload.Export + string.Join(".", name) + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx";
                //if (System.IO.File.Exists(HttpContext.Current.Server.MapPath("/" + file.FilePath)))
                //{
                //    System.IO.File.Copy(HttpContext.Current.Server.MapPath("/" + file.FilePath), HttpContext.Current.Server.MapPath(result), true);

                //    FileInfo exportFile = new FileInfo(HttpContext.Current.Server.MapPath(result));
                string file = "/" + FolderUpload.Export + "ORDOrder_ExcelOnline_" + objSettingOrder.Name + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx";

                if (System.IO.File.Exists(HttpContext.Current.Server.MapPath(file)))
                    System.IO.File.Delete(HttpContext.Current.Server.MapPath(file));
                FileInfo exportFile = new FileInfo(HttpContext.Current.Server.MapPath(file));

                using (var package = new ExcelPackage(exportFile))
                {
                    ExcelWorksheet ws = package.Workbook.Worksheets.Add(objSettingOrder.Name);
                    if (ws != null)
                    {
                        var sValue = new List<string>(new string[]{ "CustomerID", "SYSCustomerID", "ID", "CreateBy", "CreateDate", "HasStock", "ListStock", "Name", "ContractID", "RowStart",
                                            "ServiceOfOrderName", "SettingCustomerName", "TypeOfTransportModeName", "TypeOfTransportModeID", "ServiceOfOrderID", "TotalColumn" });

                        Dictionary<string, string> dicName = GetDataName();

                        #region header
                        int row = 1;
                        foreach (var prop in objSettingOrder.GetType().GetProperties())
                        {
                            try
                            {
                                var p = prop.Name;
                                if (!sValue.Contains(p))
                                {
                                    var v = (int)prop.GetValue(objSettingOrder, null);
                                    if (v > 0)
                                    {
                                        if (dicName.ContainsKey(p))
                                            ws.Cells[row, v].Value = dicName[p];
                                        else
                                            ws.Cells[row, v].Value = p;
                                    }
                                }
                            }
                            catch (Exception)
                            {
                            }
                        }
                        if (objSettingOrder.HasStock && objSettingOrder.ListStock != null && objSettingOrder.ListStock.Count > 0)
                        {
                            foreach (var obj in objSettingOrder.ListStock)
                            {
                                var cusStock = data.ListStock.FirstOrDefault(c => c.CUSLocationID == obj.StockID && c.CustomerID == customerID);
                                if (cusStock != null)
                                {
                                    if (obj.Ton > 0)
                                        ws.Cells[row, obj.Ton].Value = cusStock.LocationCode + "_Tấn";
                                    if (obj.CBM > 0)
                                        ws.Cells[row, obj.CBM].Value = cusStock.LocationCode + "_Khối";
                                    if (obj.Quantity > 0)
                                        ws.Cells[row, obj.Quantity].Value = cusStock.LocationCode + "_SL";
                                }
                            }
                        }
                        if (objSettingOrder.HasStockProduct && objSettingOrder.ListStockWithProduct != null && objSettingOrder.ListStockWithProduct.Count > 0)
                        {
                            foreach (var obj in objSettingOrder.ListStockWithProduct)
                            {
                                var cusStock = data.ListStock.FirstOrDefault(c => c.CUSLocationID == obj.StockID && c.CustomerID == customerID);
                                var cusGroup = data.ListGroupOfProduct.FirstOrDefault(c => c.ID == obj.GroupOfProductID && c.CUSStockID == obj.StockID);
                                var cusProduct = data.ListProduct.FirstOrDefault(c => c.ID == obj.ProductID && c.GroupOfProductID == obj.GroupOfProductID);
                                if (cusStock != null && cusGroup != null && cusProduct != null)
                                {
                                    if (obj.Ton > 0)
                                        ws.Cells[row, obj.Ton].Value = cusStock.LocationCode + "_" + cusGroup.Code + "_" + cusProduct.Code + "_Tấn";
                                    if (obj.CBM > 0)
                                        ws.Cells[row, obj.CBM].Value = cusStock.LocationCode + "_" + cusGroup.Code + "_" + cusProduct.Code + "_Khối";
                                    if (obj.Quantity > 0)
                                        ws.Cells[row, obj.Quantity].Value = cusStock.LocationCode + "_" + cusGroup.Code + "_" + cusProduct.Code + "_SL";
                                }
                            }
                        }
                        else
                        {
                            if (objSettingOrder.HasContainer && objSettingOrder.ListContainer != null && objSettingOrder.ListContainer.Count > 0)
                            {
                                foreach (var obj in objSettingOrder.ListContainer)
                                {
                                    var cusPacking = data.ListPackingCO.FirstOrDefault(c => c.ID == obj.PackingID);
                                    if (cusPacking != null)
                                    {
                                        if (obj.PackingID > 0)
                                            ws.Cells[row, obj.PackingCOQuantity].Value = cusPacking.Code + "_Loại Cont";
                                    }
                                }
                            }
                        }
                        ExcelHelper.CreateCellStyle(ws, 1, 1, 1, objSettingOrder.TotalColumn.Value, false, true, ExcelHelper.ColorGreen, ExcelHelper.ColorWhite, 0, "");
                        #endregion

                        var cRow = objSettingOrder.RowStart;
                        List<string> timeProps = new List<string>(new string[] { "RequestTime", "ETARequestTime", "TimeGetEmpty", "TimeReturnEmpty" });
                        if (objSettingOrder.HasStock)
                        {
                            var dataGop = dataExport.GroupBy(c => new { c.OrderCode, c.GroupProductCode, c.Packing, c.DistributorCode, c.LocationToCode, c.ETD, c.ETA }).ToList();
                            foreach (var gop in dataGop)
                            {
                                int max = 1;
                                var item = gop.FirstOrDefault();
                                foreach (var sto in objSettingOrder.ListStock)
                                {
                                    var o = gop.Count(c => c.StockID == sto.StockID);
                                    if (o > max)
                                        max = o;
                                }
                                var dataContains = new List<int>();
                                for (var i = 0; i < max; i++)
                                {
                                    foreach (var prop in objSettingOrder.GetType().GetProperties())
                                    {
                                        try
                                        {
                                            var p = prop.Name;
                                            if (!sValue.Contains(p))
                                            {
                                                var v = (int)prop.GetValue(objSettingOrder, null);
                                                var val = item.GetType().GetProperty(p).GetValue(item, null);
                                                var txt = string.Empty;
                                                if (val != null)
                                                {
                                                    if (val.GetType() == typeof(DateTime))
                                                    {
                                                        if (timeProps.Contains(p))
                                                        {
                                                            txt = String.Format("{0:HH:mm}", val);
                                                        }
                                                        else
                                                        {
                                                            txt = String.Format("{0:dd/MM/yyyy HH:mm}", val);
                                                        }
                                                    }
                                                    else if (val.GetType() == typeof(TimeSpan))
                                                    {
                                                        txt = val.ToString();
                                                    }
                                                    else
                                                    {
                                                        txt = val.ToString();
                                                    }
                                                }
                                                ws.Cells[cRow, v].Value = txt;
                                            }
                                        }
                                        catch (Exception)
                                        {
                                        }
                                    }
                                    foreach (var stock in objSettingOrder.ListStock)
                                    {
                                        var objGopInStock = gop.FirstOrDefault(c => c.StockID == stock.StockID && !dataContains.Contains(c.ID));
                                        if (objGopInStock != null)
                                        {
                                            dataContains.Add(objGopInStock.ID);
                                            foreach (var prop in stock.GetType().GetProperties())
                                            {
                                                try
                                                {
                                                    var p = prop.Name;
                                                    if (p != "StockID")
                                                    {
                                                        var v = (int)prop.GetValue(stock, null);
                                                        var val = objGopInStock.GetType().GetProperty(p).GetValue(objGopInStock, null);
                                                        if (val != null)
                                                        {
                                                            ws.Cells[cRow, v].Value = val.ToString();
                                                        }
                                                    }
                                                }
                                                catch (Exception)
                                                {
                                                }
                                            }
                                        }
                                    }
                                    cRow++;
                                }
                            }
                        }
                        else if (objSettingOrder.HasStockProduct)
                        {
                            var dataGop = dataExport.GroupBy(c => new { c.OrderCode, c.DistributorCode, c.LocationToCode, c.ETD, c.ETA }).ToList();
                            foreach (var gop in dataGop)
                            {
                                int max = 1;
                                var item = gop.FirstOrDefault();
                                foreach (var sto in objSettingOrder.ListStockWithProduct)
                                {
                                    var o = gop.Count(c => c.StockID == sto.StockID && c.GroupProductID == sto.GroupOfProductID && c.PackingID == sto.ProductID);
                                    if (o > max)
                                        max = o;
                                }
                                var dataContains = new List<int>();
                                for (var i = 0; i < max; i++)
                                {
                                    foreach (var prop in objSettingOrder.GetType().GetProperties())
                                    {
                                        try
                                        {
                                            var p = prop.Name;
                                            if (!sValue.Contains(p))
                                            {
                                                var v = (int)prop.GetValue(objSettingOrder, null);
                                                var val = item.GetType().GetProperty(p).GetValue(item, null);
                                                var txt = string.Empty;
                                                if (val != null)
                                                {
                                                    if (val.GetType() == typeof(DateTime))
                                                    {
                                                        if (timeProps.Contains(p))
                                                        {
                                                            txt = String.Format("{0:HH:mm}", val);
                                                        }
                                                        else
                                                        {
                                                            txt = String.Format("{0:dd/MM/yyyy HH:mm}", val);
                                                        }
                                                    }
                                                    else if (val.GetType() == typeof(TimeSpan))
                                                    {
                                                        txt = val.ToString();
                                                    }
                                                    else
                                                    {
                                                        txt = val.ToString();
                                                    }
                                                }
                                                ws.Cells[cRow, v].Value = txt;
                                            }
                                        }
                                        catch (Exception)
                                        {
                                        }
                                    }
                                    foreach (var stock in objSettingOrder.ListStockWithProduct)
                                    {
                                        var objGopInStock = gop.FirstOrDefault(c => c.StockID == stock.StockID && c.GroupProductID == stock.GroupOfProductID && c.PackingID == stock.ProductID && !dataContains.Contains(c.ID));
                                        if (objGopInStock != null)
                                        {
                                            dataContains.Add(objGopInStock.ID);
                                            foreach (var prop in stock.GetType().GetProperties())
                                            {
                                                try
                                                {
                                                    var p = prop.Name;
                                                    if (p != "StockID" && p != "GroupOfProductID" && p != "ProductID")
                                                    {
                                                        var v = (int)prop.GetValue(stock, null);
                                                        var val = objGopInStock.GetType().GetProperty(p).GetValue(objGopInStock, null);
                                                        if (val != null)
                                                        {
                                                            ws.Cells[cRow, v].Value = val.ToString();
                                                        }
                                                    }
                                                }
                                                catch (Exception)
                                                {
                                                }
                                            }
                                        }
                                    }
                                    cRow++;
                                }
                            }
                        }
                        else
                        {
                            if (objSettingOrder.HasContainer)
                            {

                            }
                            else
                            {
                                foreach (var item in dataExport)
                                {
                                    item.Quantity_SKU = item.Quantity;
                                    item.Ton_SKU = item.Ton;
                                    item.CBM_SKU = item.CBM;
                                    foreach (var prop in objSettingOrder.GetType().GetProperties())
                                    {
                                        try
                                        {
                                            var p = prop.Name;
                                            if (!sValue.Contains(p))
                                            {
                                                var v = (int)prop.GetValue(objSettingOrder, null);
                                                var val = item.GetType().GetProperty(p).GetValue(item, null);
                                                var txt = string.Empty;
                                                if (val != null)
                                                {
                                                    if (val.GetType() == typeof(DateTime))
                                                    {
                                                        if (timeProps.Contains(p))
                                                        {
                                                            txt = String.Format("{0:HH:mm}", val);
                                                        }
                                                        else
                                                        {
                                                            txt = String.Format("{0:dd/MM/yyyy HH:mm}", val);
                                                        }
                                                    }
                                                    else if (val.GetType() == typeof(TimeSpan))
                                                    {
                                                        txt = val.ToString();
                                                    }
                                                    else
                                                    {
                                                        txt = val.ToString();
                                                    }
                                                }
                                                ws.Cells[cRow, v].Value = txt;
                                            }
                                        }
                                        catch (Exception)
                                        {
                                        }
                                    }
                                    cRow++;
                                }
                            }
                        }
                        package.Save();
                    }
                }

                return file;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOCUSSettingOrder ORDOrder_Excel_Setting_Get(dynamic dynParam)
        {
            try
            {
                var templateID = (int)dynParam.templateID;

                var result = default(DTOCUSSettingOrder);

                ServiceFactory.SVOrder((ISVOrder sv) =>
                {
                    result = sv.ORDOrder_Excel_Setting_Get(templateID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region ORDPlan
        [HttpPost]
        public DTOResult ORDOrder_Plan_Excel_Setting_List(dynamic dynParam)
        {
            try
            {
                string request = dynParam.request.ToString();
                var result = new DTOResult();
                ServiceFactory.SVOrder((ISVOrder sv) =>
                {
                    result = sv.ORDOrder_Plan_Excel_Setting_List(request);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOCUSSettingORDPlan ORDOrder_Plan_Excel_Setting_Get(dynamic dynParam)
        {
            try
            {
                var templateID = (int)dynParam.templateID;

                var result = default(DTOCUSSettingORDPlan);

                ServiceFactory.SVOrder((ISVOrder sv) =>
                {
                    result = sv.ORDOrder_Plan_Excel_Setting_Get(templateID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public SYSExcel ORDOrder_Plan_ExcelOnline_Init(dynamic dynParam)
        {
            try
            {
                var functionid = (int)dynParam.functionid;
                var functionkey = dynParam.functionkey.ToString();
                var templateID = (int)dynParam.templateID;
                var customerID = (int)dynParam.customerID;
                var pID = (int)dynParam.pID;
                var isreload = (bool)dynParam.isreload;

                var result = default(SYSExcel);

                ServiceFactory.SVOrder((ISVOrder sv) =>
                {
                    result = sv.ORDOrder_Plan_ExcelOnline_Init(templateID, customerID, pID, functionid, functionkey, isreload);
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
        public DTOORDOrder_Plan_ImportRowResult ORDOrder_Plan_ExcelOnline_Change(dynamic dynParam)
        {
            try
            {
                var id = (long)dynParam.id;
                var row = (int)dynParam.row;
                var templateID = (int)dynParam.templateID;
                var customerID = (int)dynParam.customerID;
                List<string> lstMessageError = Newtonsoft.Json.JsonConvert.DeserializeObject<List<string>>(dynParam.lstMessageError.ToString());
                List<Cell> cells = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Cell>>(dynParam.cells.ToString());

                DTOCUSSettingORDPlan objSetting = new DTOCUSSettingORDPlan();
                DTOORDOrder_ImportCheck data = new DTOORDOrder_ImportCheck();

                ServiceFactory.SVOrder((ISVOrder sv) =>
                {
                    objSetting = sv.ORDOrder_Plan_Excel_Setting_Get(templateID);
                    data = sv.ORDOrder_ExcelOnline_Import_Data(customerID);
                });

                int transportID = -1;
                int tmID = -1;
                var address = "";
                var economicZone = "";
                var customerCode = "";
                var distributorName = "";
                var distributorCode = "";
                var distributorCodeName = "";
                var locationToCode = "";
                var locationToName = "";
                var locationToCodeName = "";
                var locationToNote = "";
                var routingAreaCode = "";
                var transportModeCode = "";
                List<int> excelErrorCustomer = new List<int>();
                List<int> excelErrorLocation = new List<int>();
                DTOORDOrder_ImportOnline objImport = new DTOORDOrder_ImportOnline();

                #region Get Data
                var isFailed = false;
                List<int> lstRowError = new List<int>();
                foreach (var item in cells)
                {
                    //Check value in cell
                    if (item.Value != null)
                    {
                        Type t = item.Value.GetType();

                        if (t == typeof(JObject))
                        {
                            lstRowError.Add(item.Index.Value + 1);
                            isFailed = true;
                        }
                        if (item.Index + 1 == objSetting.TypeOfTransportMode)
                        {
                            transportModeCode = item.Value.ToString().Trim();
                        }
                        else if (item.Index + 1 == objSetting.LocationToAddress)
                        {
                            address = item.Value.ToString().Trim();
                        }
                        else if (item.Index + 1 == objSetting.EconomicZone)
                        {
                            economicZone = item.Value.ToString().Trim();
                        }
                        else if (item.Index + 1 == objSetting.RoutingAreaCode)
                        {
                            routingAreaCode = item.Value.ToString().Trim();
                        }
                        else if (item.Index + 1 == objSetting.CustomerCode)
                        {
                            customerCode = item.Value.ToString().Trim();
                        }
                        else if (item.Index + 1 == objSetting.DistributorName)
                        {
                            distributorName = item.Value.ToString().Trim();
                        }
                        else if (item.Index + 1 == objSetting.DistributorCode)
                        {
                            distributorCode = item.Value.ToString().Trim();
                        }
                        else if (item.Index + 1 == objSetting.DistributorCodeName)
                        {
                            distributorCodeName = item.Value.ToString().Trim();
                        }
                        else if (item.Index + 1 == objSetting.LocationToCode)
                        {
                            locationToCode = item.Value.ToString().Trim();
                        }
                        else if (item.Index + 1 == objSetting.LocationToName)
                        {
                            locationToName = item.Value.ToString().Trim();
                        }
                        else if (item.Index + 1 == objSetting.LocationToCodeName)
                        {
                            locationToCodeName = item.Value.ToString().Trim();
                        }
                        else if (item.Index + 1 == objSetting.LocationToNote)
                        {
                            locationToNote = item.Value.ToString().Trim();
                        }
                    }
                }
                if (isFailed)
                {
                    throw new Exception("Dòng " + (row + 1) + " cell: " + string.Join(",", lstRowError) + " sai dữ liệu.");
                }
                #endregion

                #region Check TransportMode
                //TM của setting
                if (objSetting.TypeOfTransportModeID > 0)
                {
                    tmID = objSetting.TypeOfTransportModeID;
                    var objTM = data.ListTransportMode.FirstOrDefault(c => c.ID == tmID);
                    if (objTM != null)
                        transportID = objTM.TransportModeID;
                }
                else
                {
                    if (objSetting.TypeOfTransportMode < 1)
                    {
                        //throw new Exception("Chưa thiết lập cột loại vận chuyển.");
                        excelErrorCustomer.Add(5);
                    }

                    var str = transportModeCode.Trim().ToLower();
                    if (!string.IsNullOrEmpty(str))
                    {
                        var objTM = data.ListTransportMode.FirstOrDefault(c => c.Code.ToLower() == str.Trim().ToLower());
                        if (objTM != null)
                        {
                            tmID = objTM.ID;
                            transportID = objTM.TransportModeID;
                        }
                        else
                        {
                            //throw new Exception("Dòng [" + row + "], loại vận chuyển [" + str + "]  không xác định.");
                            excelErrorCustomer.Add(6);
                        }
                    }
                    else
                    {
                        //throw new Exception("Dòng [" + row + "] không xác định loại vận chuyển.");
                        excelErrorCustomer.Add(7);
                    }
                }
                #endregion

                #region Check Customer
                if (objSetting.CustomerID == objSetting.SYSCustomerID)
                {
                    if (string.IsNullOrEmpty(customerCode))
                    {
                        //excelErrorCustomer.Add("Thiếu mã KH.");
                        excelErrorCustomer.Add(0);
                        objImport.CustomerID = -1;
                    }
                    else
                    {
                        var objCheck = data.ListCustomer.FirstOrDefault(c => c.Code.Trim().ToLower() == customerCode.Trim().ToLower());
                        if (objCheck == null)
                        {
                            //excelErrorCustomer.Add("KH [" + customerCode + "] không tồn tại.");
                            excelErrorCustomer.Add(1);
                            objImport.CustomerID = -1;
                        }
                        else
                        {
                            objImport.CustomerID = objCheck.ID;
                            objImport.CustomerCode = objCheck.Code;
                            objImport.IsCreateLocation = objCheck.IsCreateLocation;
                            objImport.IsCreatePartner = objCheck.IsCreatePartner;
                        }
                    }
                }
                else
                {
                    objImport.CustomerID = objSetting.CustomerID;
                    var objCheck = data.ListCustomer.FirstOrDefault(c => c.ID == objImport.CustomerID);
                    if (objCheck == null)
                    {
                        //excelErrorCustomer.Add("KH không tồn tại.");
                        excelErrorCustomer.Add(1);
                        objImport.CustomerID = -1;
                    }
                    else
                    {
                        objImport.CustomerCode = objCheck.Code;
                        objImport.IsCreateLocation = objCheck.IsCreateLocation;
                        objImport.IsCreatePartner = objImport.IsCreatePartner;
                    }
                }
                #endregion

                if (transportID > 0 && transportID != iFCL)
                {
                    #region Check nhà phân phối
                    ////Nếu điểm giao trống
                    if (string.IsNullOrEmpty(address) && string.IsNullOrEmpty(locationToCode) && string.IsNullOrEmpty(locationToCodeName) && string.IsNullOrEmpty(locationToName))
                        excelErrorLocation.Add(57);

                    var isLocationToFail = false;
                    var sLocation = new List<AddressSearchItem>();
                    var sPartnerLocation = new List<DTOORDOrder_Import_PartnerLocation>();

                    var pID = -1;//
                    var toID = -1;//
                    var toCode = string.Empty;//
                    var toName = string.Empty;//

                    string dName = distributorName;//
                    string dCode = distributorCode;//

                    if (!string.IsNullOrEmpty(distributorCodeName))
                    {
                        string[] s = distributorCodeName.Split('-');
                        dCode = s[0];
                        if (s.Length > 1)
                        {
                            dName = distributorCodeName.Substring(dCode.Length + 1);
                        }
                    }

                    if (!string.IsNullOrEmpty(dCode))
                    {
                        var objCheck = data.ListDistributor.FirstOrDefault(c => !string.IsNullOrEmpty(c.PartnerCode) && c.PartnerCode.Trim().ToLower() == dCode.Trim().ToLower() && c.CustomerID == objImport.CustomerID);
                        if (objCheck != null)
                        {
                            pID = objCheck.CUSPartnerID;
                            dCode = objCheck.PartnerCode;

                            if (!string.IsNullOrEmpty(dName))
                                dName = objCheck.PartnerName;

                            toCode = locationToCode;
                            toName = locationToName;
                            if (objSetting.LocationToCodeName > 0)
                            {
                                if (!string.IsNullOrEmpty(locationToCodeName))
                                {
                                    toCode = locationToCodeName.Split('-').FirstOrDefault();
                                    toName = locationToCodeName.Split('-').Skip(1).FirstOrDefault();
                                }
                                else
                                {
                                    toCode = string.Empty;
                                    toName = string.Empty;
                                }
                            }
                            if (objSetting.SuggestLocationToCode == true)
                                toCode = string.Empty;

                            //Tìm theo code
                            var objTo = data.ListDistributorLocation.FirstOrDefault(c => c.CusPartID == pID && c.LocationCode.Trim().ToLower() == toCode.Trim().ToLower());
                            if (objTo != null)
                            {
                                toID = objTo.CUSLocationID;
                                var objSearch = new AddressSearchItem();
                                objSearch.CUSLocationID = toID;
                                objSearch.CustomerID = objImport.CustomerID;
                                objSearch.LocationCode = objTo.LocationCode;
                                objSearch.Address = objTo.Address;
                                objSearch.CUSPartnerID = pID;
                                sLocation.Add(objSearch);
                            }
                            else if (objSetting.SuggestLocationToCode || objSetting.LocationToCode <= 0)
                            {
                                //Tìm và gợi ý địa chỉ.
                                try
                                {
                                    int total = 0;
                                    sLocation = AddressSearchHelper.Search(objImport.CustomerID, pID, economicZone, address, 0, 100, ref total);

                                    if (sLocation.Count != 0)
                                    {
                                        var isCheckLocationFailed = false;
                                        foreach (var location in sLocation)
                                        {
                                            var objLocation = data.ListDistributorLocation.FirstOrDefault(c => c.CUSLocationID == location.CUSLocationID);
                                            if (objLocation == null)
                                            {
                                                isCheckLocationFailed = true;
                                                break;
                                            }
                                        }
                                        if (isCheckLocationFailed)
                                        {
                                            var lstAddressSearchItem = new List<AddressSearchItem>();
                                            ServiceFactory.SVCategory((ISVCategory sv) =>
                                            {
                                                lstAddressSearchItem = sv.AddressSearch_List();
                                            });
                                            //var obj = lst.Where(c => c.CUSLocationID == 10616).ToList();
                                            lstAddressSearchItem = lstAddressSearchItem.Where(c => c.Address != null && c.PartnerCode != null).ToList();
                                            AddressSearchHelper.Create(lstAddressSearchItem);

                                            sLocation = AddressSearchHelper.Search(objImport.CustomerID, pID, economicZone, address, 0, 100, ref total);
                                        }
                                    }

                                    if (sLocation.Count == 0)
                                    {
                                        //excelErrorLocation.Add("Địa chỉ (" + address + ") ko tồn tại");
                                        excelErrorLocation.Add(2);
                                        isLocationToFail = true;
                                    }
                                    else if (sLocation[0].Address != address)
                                    {
                                        var flag = true;
                                        for (int i = 1; i < sLocation.Count; i++)
                                        {
                                            if (sLocation[i].Address == address)
                                            {
                                                var o = sLocation[i];
                                                sLocation.RemoveAt(i);
                                                sLocation.Insert(0, o);
                                                flag = false;
                                                break;
                                            }
                                        }
                                        isLocationToFail = flag;
                                        //if (isLocationToFail)
                                        //{
                                        //    excelErrorLocation.Add(2);
                                        //}
                                    }
                                }
                                catch (Exception)
                                {
                                    isLocationToFail = true;
                                    //excelErrorLocation.Add("Địa chỉ (" + address + ") ko tồn tại");
                                    excelErrorLocation.Add(2);
                                }
                            }
                            else
                            {
                                isLocationToFail = true;
                                //Sai ma dia chi
                                excelErrorLocation.Add(92);
                            }

                            DTOORDOrder_Import_PartnerLocation objPartner = new DTOORDOrder_Import_PartnerLocation();
                            objPartner.PartnerID = 0;
                            if (pID > 0)
                                objPartner.PartnerID = pID;
                            objPartner.CustomerID = objImport.CustomerID;
                            objPartner.PartnerCode = dCode;
                            objPartner.PartnerName = dName;
                            objPartner.LocationAddress = address;
                            objPartner.EconomicZone = economicZone;
                            objPartner.RoutingAreaCode = routingAreaCode;
                            objPartner.RouteDescription = locationToNote;
                            objPartner.LocationCode = locationToCode;

                            sPartnerLocation.Add(objPartner);

                            if (!string.IsNullOrEmpty(economicZone))
                            {
                                var objCus = data.ListCustomer.FirstOrDefault(c => c.ID == objImport.CustomerID);
                                if (objCus != null && objCus.IsFindEconomicZone == true)
                                {
                                    var objRoute = data.ListRoute.FirstOrDefault(c => c.CustomerID == objImport.CustomerID && c.Code.Trim().ToLower() == economicZone.Trim().ToLower());
                                    if (objRoute != null && objRoute.RoutingAreaToID > 0)
                                    {
                                        var objRouteArea = data.ListRouteArea.FirstOrDefault(c => c.ProvinceID > 0 && c.DistrictID > 0 && c.RoutingAreaID == objRoute.RoutingAreaToID);
                                        if (objRouteArea == null)
                                            objRouteArea = data.ListRouteArea.FirstOrDefault(c => c.ProvinceID > 0 && c.RoutingAreaID == objRoute.RoutingAreaToID);
                                        if (objRouteArea != null)
                                        {
                                            objPartner.ProvinceID = objRouteArea.ProvinceID.Value;
                                            if (objRouteArea.DistrictID.HasValue)
                                                objPartner.DistrictID = objRouteArea.DistrictID.Value;
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            isLocationToFail = true;
                            //excelErrorLocation.Add("Npp [" + dCode + "] không tồn tại.");
                            excelErrorLocation.Add(3);
                            toCode = locationToCode;
                            toName = locationToName;
                        }
                    }
                    else
                    {
                        //excelErrorLocation.Add("Npp không xác định.");
                        excelErrorLocation.Add(4);
                        toCode = locationToCode;
                        toName = locationToName;
                    }

                    objImport.LocationToID = toID;
                    objImport.LocationToAddress = address;
                    if (objImport.LocationToID < 0)
                    {
                        objImport.LocationToID = -1;
                        if (string.IsNullOrEmpty(toCode))
                        {
                            toCode = locationToCode;
                        }
                    }
                    if (objImport.LocationToID < 1 && !isLocationToFail)
                    {
                        if (sLocation.Count > 0)
                        {
                            objImport.LocationToID = sLocation.FirstOrDefault().CUSLocationID;
                            toCode = sLocation.FirstOrDefault().LocationCode;
                            toName = "";
                        }
                        else
                            objImport.LocationToID = -1;
                    }
                    objImport.LocationToCode = toCode;
                    objImport.LocationToName = toName;
                    objImport.PartnerID = pID;
                    objImport.PartnerCode = dCode;
                    objImport.PartnerName = dName;
                    objImport.IsLocationToFail = isLocationToFail;
                    objImport.ListPartnerLocation = new List<DTOORDOrder_Import_PartnerLocation>();
                    objImport.ListPartnerLocation.AddRange(sPartnerLocation);
                    objImport.sLocation = new List<AddressSearchItem>();
                    objImport.sLocation.AddRange(sLocation);
                    #endregion
                }

                objImport.TransportModeID = tmID;
                objImport.ExcelErrorCustomer = new List<int>();
                objImport.ExcelErrorCustomer.AddRange(excelErrorCustomer);

                objImport.ExcelErrorLocation = new List<int>();
                objImport.ExcelErrorLocation.AddRange(excelErrorLocation);

                var result = default(DTOORDOrder_Plan_ImportRowResult);
                if (id > 0 && cells.Count > 0 && row > 0)
                {
                    ServiceFactory.SVOrder((ISVOrder sv) =>
                    {
                        result = sv.ORDOrder_Plan_ExcelOnline_Change(templateID, customerID, objImport, id, row, cells, lstMessageError);
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
        public DTOORDOrder_Plan_ImportResult ORDOrder_Plan_ExcelOnline_Import(dynamic dynParam)
        {
            try
            {
                var id = (long)dynParam.id;
                var templateID = (int)dynParam.templateID;
                var customerID = (int)dynParam.customerID;
                List<Worksheet> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Worksheet>>(dynParam.worksheets.ToString());

                List<string> lstMessageError = Newtonsoft.Json.JsonConvert.DeserializeObject<List<string>>(dynParam.lstMessageError.ToString());
                //List<Cell> cells = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Cell>>(dynParam.cells.ToString());

                DTOCUSSettingORDPlan objSetting = new DTOCUSSettingORDPlan();
                DTOORDOrder_ImportCheck data = new DTOORDOrder_ImportCheck();

                ServiceFactory.SVOrder((ISVOrder sv) =>
                {
                    objSetting = sv.ORDOrder_Plan_Excel_Setting_Get(templateID);
                    data = sv.ORDOrder_ExcelOnline_Import_Data(customerID);
                });

                List<DTOORDOrder_ImportOnline> lstDetail = new List<DTOORDOrder_ImportOnline>();
                var lstRows = lst[0].Rows.Where(c => c.Cells != null).ToList();
                foreach (var eRow in lstRows)
                {
                    if (eRow.Index == 0 || eRow.Index < objSetting.RowStart - 1) continue;
                    var cells = eRow.Cells;
                    int transportID = -1;
                    int tmID = -1;
                    var address = "";
                    var economicZone = "";
                    var customerCode = "";
                    var distributorName = "";
                    var distributorCode = "";
                    var distributorCodeName = "";
                    var locationToCode = "";
                    var locationToName = "";
                    var locationToCodeName = "";
                    var locationToNote = "";
                    var routingAreaCode = "";
                    var transportModeCode = "";
                    List<int> excelErrorCustomer = new List<int>();
                    List<int> excelErrorLocation = new List<int>();
                    DTOORDOrder_ImportOnline objImport = new DTOORDOrder_ImportOnline();

                    #region Get Data
                    var isFailed = false;
                    List<int> lstRowError = new List<int>();
                    foreach (var item in cells)
                    {
                        if (item.Value != null)
                        {
                            Type t = item.Value.GetType();

                            if (t == typeof(JObject))
                            {
                                lstRowError.Add(item.Index.Value + 1);
                                isFailed = true;
                            }
                            if (item.Index + 1 == objSetting.TypeOfTransportMode)
                            {
                                transportModeCode = item.Value.ToString().Trim();
                            }
                            else if (item.Index + 1 == objSetting.LocationToAddress)
                            {
                                address = item.Value.ToString().Trim();
                            }
                            else if (item.Index + 1 == objSetting.EconomicZone)
                            {
                                economicZone = item.Value.ToString().Trim();
                            }
                            else if (item.Index + 1 == objSetting.RoutingAreaCode)
                            {
                                routingAreaCode = item.Value.ToString().Trim();
                            }
                            else if (item.Index + 1 == objSetting.CustomerCode)
                            {
                                customerCode = item.Value.ToString().Trim();
                            }
                            else if (item.Index + 1 == objSetting.DistributorName)
                            {
                                distributorName = item.Value.ToString().Trim();
                            }
                            else if (item.Index + 1 == objSetting.DistributorCode)
                            {
                                distributorCode = item.Value.ToString().Trim();
                            }
                            else if (item.Index + 1 == objSetting.DistributorCodeName)
                            {
                                distributorCodeName = item.Value.ToString().Trim();
                            }
                            else if (item.Index + 1 == objSetting.LocationToCode)
                            {
                                locationToCode = item.Value.ToString().Trim();
                            }
                            else if (item.Index + 1 == objSetting.LocationToName)
                            {
                                locationToName = item.Value.ToString().Trim();
                            }
                            else if (item.Index + 1 == objSetting.LocationToCodeName)
                            {
                                locationToCodeName = item.Value.ToString().Trim();
                            }
                            else if (item.Index + 1 == objSetting.LocationToNote)
                            {
                                locationToNote = item.Value.ToString().Trim();
                            }
                        }
                    }

                    if (isFailed)
                    {
                        throw new Exception("Dòng " + (eRow.Index.Value + 1) + " cell: " + string.Join(",", lstRowError) + " sai dữ liệu.");
                    }
                    #endregion

                    #region Check TransportMode
                    //TM của setting
                    if (objSetting.TypeOfTransportModeID > 0)
                    {
                        tmID = objSetting.TypeOfTransportModeID;
                        var objTM = data.ListTransportMode.FirstOrDefault(c => c.ID == tmID);
                        if (objTM != null)
                            transportID = objTM.TransportModeID;
                    }
                    else
                    {
                        if (objSetting.TypeOfTransportMode < 1)
                        {
                            //throw new Exception("Chưa thiết lập cột loại vận chuyển.");
                            excelErrorCustomer.Add(5);
                        }

                        var str = transportModeCode.Trim().ToLower();
                        if (!string.IsNullOrEmpty(str))
                        {
                            var objTM = data.ListTransportMode.FirstOrDefault(c => c.Code.ToLower() == str.Trim().ToLower());
                            if (objTM != null)
                            {
                                tmID = objTM.ID;
                                transportID = objTM.TransportModeID;
                            }
                            else
                            {
                                //throw new Exception("Dòng [" + row + "], loại vận chuyển [" + str + "]  không xác định.");
                                excelErrorCustomer.Add(6);
                            }
                        }
                        else
                        {
                            //throw new Exception("Dòng [" + row + "] không xác định loại vận chuyển.");
                            excelErrorCustomer.Add(7);
                        }
                    }
                    #endregion

                    #region Check Customer
                    if (objSetting.CustomerID == objSetting.SYSCustomerID)
                    {
                        if (string.IsNullOrEmpty(customerCode))
                        {
                            //excelErrorCustomer.Add("Thiếu mã KH.");
                            excelErrorCustomer.Add(0);
                            objImport.CustomerID = -1;
                        }
                        else
                        {
                            var objCheck = data.ListCustomer.FirstOrDefault(c => c.Code.Trim().ToLower() == customerCode.Trim().ToLower());
                            if (objCheck == null)
                            {
                                //excelErrorCustomer.Add("KH [" + customerCode + "] không tồn tại.");
                                excelErrorCustomer.Add(1);
                                objImport.CustomerID = -1;
                            }
                            else
                            {
                                objImport.CustomerID = objCheck.ID;
                                objImport.CustomerCode = objCheck.Code;
                                objImport.IsCreateLocation = objCheck.IsCreateLocation;
                                objImport.IsCreatePartner = objCheck.IsCreatePartner;
                            }
                        }
                    }
                    else
                    {
                        objImport.CustomerID = objSetting.CustomerID;
                        var objCheck = data.ListCustomer.FirstOrDefault(c => c.ID == objImport.CustomerID);
                        if (objCheck == null)
                        {
                            //excelErrorCustomer.Add("KH không tồn tại.");
                            excelErrorCustomer.Add(1);
                            objImport.CustomerID = -1;
                        }
                        else
                        {
                            objImport.CustomerCode = objCheck.Code;
                            objImport.IsCreateLocation = objCheck.IsCreateLocation;
                            objImport.IsCreatePartner = objCheck.IsCreatePartner;
                        }
                    }
                    #endregion

                    if (transportID > 0 && transportID != iFCL)
                    {
                        #region Check nhà phân phối
                        ////Nếu điểm giao trống
                        if (string.IsNullOrEmpty(address) && string.IsNullOrEmpty(locationToCode) && string.IsNullOrEmpty(locationToCodeName) && string.IsNullOrEmpty(locationToName))
                            excelErrorLocation.Add(57);

                        var isLocationToFail = false;
                        var sLocation = new List<AddressSearchItem>();
                        var sPartnerLocation = new List<DTOORDOrder_Import_PartnerLocation>();

                        var pID = -1;//
                        var toID = -1;//
                        var toCode = string.Empty;//
                        var toName = string.Empty;//

                        string dName = distributorName;//
                        string dCode = distributorCode;//

                        if (!string.IsNullOrEmpty(distributorCodeName))
                        {
                            string[] s = distributorCodeName.Split('-');
                            dCode = s[0];
                            if (s.Length > 1)
                            {
                                dName = distributorCodeName.Substring(dCode.Length + 1);
                            }
                        }

                        if (!string.IsNullOrEmpty(dCode))
                        {
                            var objCheck = data.ListDistributor.FirstOrDefault(c => !string.IsNullOrEmpty(c.PartnerCode) && c.PartnerCode.Trim().ToLower() == dCode.Trim().ToLower() && c.CustomerID == objImport.CustomerID);
                            if (objCheck != null)
                            {
                                pID = objCheck.CUSPartnerID;
                                dCode = objCheck.PartnerCode;

                                if (!string.IsNullOrEmpty(dName))
                                    dName = objCheck.PartnerName;

                                toCode = locationToCode;
                                toName = locationToName;
                                if (objSetting.LocationToCodeName > 0)
                                {
                                    if (!string.IsNullOrEmpty(locationToCodeName))
                                    {
                                        toCode = locationToCodeName.Split('-').FirstOrDefault();
                                        toName = locationToCodeName.Split('-').Skip(1).FirstOrDefault();
                                    }
                                    else
                                    {
                                        toCode = string.Empty;
                                        toName = string.Empty;
                                    }
                                }
                                if (objSetting.SuggestLocationToCode == true)
                                    toCode = string.Empty;

                                //Tìm theo code
                                var objTo = data.ListDistributorLocation.FirstOrDefault(c => c.CusPartID == pID && c.LocationCode.Trim().ToLower() == toCode.Trim().ToLower());
                                if (objTo != null)
                                {
                                    toID = objTo.CUSLocationID;
                                    var objSearch = new AddressSearchItem();
                                    objSearch.CUSLocationID = toID;
                                    objSearch.CustomerID = objImport.CustomerID;
                                    objSearch.LocationCode = objTo.LocationCode;
                                    objSearch.Address = objTo.Address;
                                    objSearch.CUSPartnerID = pID;
                                    sLocation.Add(objSearch);
                                }
                                else if (objSetting.SuggestLocationToCode || objSetting.LocationToCode <= 0)
                                {
                                    //Tìm và gợi ý địa chỉ.
                                    try
                                    {
                                        int total = 0;
                                        sLocation = AddressSearchHelper.Search(objImport.CustomerID, pID, economicZone, address, 0, 100, ref total);

                                        if (sLocation.Count != 0)
                                        {
                                            var isCheckLocationFailed = false;
                                            foreach (var location in sLocation)
                                            {
                                                var objLocation = data.ListDistributorLocation.FirstOrDefault(c => c.CUSLocationID == location.CUSLocationID);
                                                if (objLocation == null)
                                                {
                                                    isCheckLocationFailed = true;
                                                    break;
                                                }
                                            }
                                            if (isCheckLocationFailed)
                                            {
                                                var lstAddressSearchItem = new List<AddressSearchItem>();
                                                ServiceFactory.SVCategory((ISVCategory sv) =>
                                                {
                                                    lstAddressSearchItem = sv.AddressSearch_List();
                                                });
                                                //var obj = lst.Where(c => c.CUSLocationID == 10616).ToList();
                                                lstAddressSearchItem = lstAddressSearchItem.Where(c => c.Address != null && c.PartnerCode != null).ToList();
                                                AddressSearchHelper.Create(lstAddressSearchItem);

                                                sLocation = AddressSearchHelper.Search(objImport.CustomerID, pID, economicZone, address, 0, 100, ref total);
                                            }
                                        }

                                        if (sLocation.Count == 0)
                                        {
                                            //excelErrorLocation.Add("Địa chỉ (" + address + ") ko tồn tại");
                                            excelErrorLocation.Add(2);
                                            isLocationToFail = true;
                                        }
                                        else if (sLocation[0].Address != address)
                                        {
                                            var flag = true;
                                            for (int i = 1; i < sLocation.Count; i++)
                                            {
                                                if (sLocation[i].Address == address)
                                                {
                                                    var o = sLocation[i];
                                                    sLocation.RemoveAt(i);
                                                    sLocation.Insert(0, o);
                                                    flag = false;
                                                    break;
                                                }
                                            }
                                            isLocationToFail = flag;
                                            //if (isLocationToFail)
                                            //{
                                            //    excelErrorLocation.Add(2);
                                            //}
                                        }
                                    }
                                    catch (Exception)
                                    {
                                        isLocationToFail = true;
                                        //excelErrorLocation.Add("Địa chỉ (" + address + ") ko tồn tại");
                                        excelErrorLocation.Add(2);
                                    }
                                }
                                else
                                {
                                    isLocationToFail = true;
                                    //Sai ma dia chi
                                    excelErrorLocation.Add(92);
                                }

                                DTOORDOrder_Import_PartnerLocation objPartner = new DTOORDOrder_Import_PartnerLocation();
                                objPartner.PartnerID = 0;
                                if (pID > 0)
                                    objPartner.PartnerID = pID;
                                objPartner.CustomerID = objImport.CustomerID;
                                objPartner.PartnerCode = dCode;
                                objPartner.PartnerName = dName;
                                objPartner.LocationAddress = address;
                                objPartner.EconomicZone = economicZone;
                                objPartner.RoutingAreaCode = routingAreaCode;
                                objPartner.RouteDescription = locationToNote;
                                objPartner.LocationCode = locationToCode;

                                sPartnerLocation.Add(objPartner);

                                if (!string.IsNullOrEmpty(economicZone))
                                {
                                    var objCus = data.ListCustomer.FirstOrDefault(c => c.ID == objImport.CustomerID);
                                    if (objCus != null && objCus.IsFindEconomicZone == true)
                                    {
                                        var objRoute = data.ListRoute.FirstOrDefault(c => c.CustomerID == objImport.CustomerID && c.Code.Trim().ToLower() == economicZone.Trim().ToLower());
                                        if (objRoute != null && objRoute.RoutingAreaToID > 0)
                                        {
                                            var objRouteArea = data.ListRouteArea.FirstOrDefault(c => c.ProvinceID > 0 && c.DistrictID > 0 && c.RoutingAreaID == objRoute.RoutingAreaToID);
                                            if (objRouteArea == null)
                                                objRouteArea = data.ListRouteArea.FirstOrDefault(c => c.ProvinceID > 0 && c.RoutingAreaID == objRoute.RoutingAreaToID);
                                            if (objRouteArea != null)
                                            {
                                                objPartner.ProvinceID = objRouteArea.ProvinceID.Value;
                                                if (objRouteArea.DistrictID.HasValue)
                                                    objPartner.DistrictID = objRouteArea.DistrictID.Value;
                                            }
                                        }
                                    }
                                }
                            }
                            else
                            {
                                isLocationToFail = true;
                                //excelErrorLocation.Add("Npp [" + dCode + "] không tồn tại.");
                                excelErrorLocation.Add(3);
                                toCode = locationToCode;
                                toName = locationToName;
                            }
                        }
                        else
                        {
                            //excelErrorLocation.Add("Npp không xác định.");
                            excelErrorLocation.Add(4);
                            toCode = locationToCode;
                            toName = locationToName;
                        }

                        objImport.LocationToID = toID;
                        objImport.LocationToAddress = address;

                        if (objImport.LocationToID < 0)
                        {
                            objImport.LocationToID = -1;
                            if (string.IsNullOrEmpty(toCode))
                            {
                                toCode = locationToCode;
                            }
                        }

                        if (objImport.LocationToID < 1 && !isLocationToFail)
                        {
                            if (sLocation.Count > 0)
                            {
                                objImport.LocationToID = sLocation.FirstOrDefault().CUSLocationID;
                                toCode = sLocation.FirstOrDefault().LocationCode;
                                toName = "";
                            }
                            else
                                objImport.LocationToID = -1;
                        }

                        objImport.LocationToCode = toCode;
                        objImport.LocationToName = toName;
                        objImport.PartnerID = pID;
                        objImport.PartnerCode = dCode;
                        objImport.PartnerName = dName;
                        objImport.IsLocationToFail = isLocationToFail;
                        objImport.ListPartnerLocation = new List<DTOORDOrder_Import_PartnerLocation>();
                        objImport.ListPartnerLocation.AddRange(sPartnerLocation);
                        objImport.sLocation = new List<AddressSearchItem>();
                        objImport.sLocation.AddRange(sLocation);
                        #endregion
                    }

                    objImport.TransportModeID = tmID;
                    objImport.ExcelErrorCustomer = new List<int>();
                    objImport.ExcelErrorCustomer.AddRange(excelErrorCustomer);

                    objImport.ExcelErrorLocation = new List<int>();
                    objImport.ExcelErrorLocation.AddRange(excelErrorLocation);

                    objImport.Index = eRow.Index.GetValueOrDefault();
                    lstDetail.Add(objImport);
                }


                var result = default(DTOORDOrder_Plan_ImportResult);
                if (id > 0 && lst.Count > 0 && lstRows.Count > 1)
                {
                    ServiceFactory.SVOrder((ISVOrder sv) =>
                    {
                        result = sv.ORDOrder_Plan_ExcelOnline_Import(templateID, customerID, id, lstRows, lstDetail, lstMessageError);
                    });
                }
                if (result != null && result.SYSExcel != null && !string.IsNullOrEmpty(result.SYSExcel.Data))
                {
                    result.SYSExcel.Worksheets = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Worksheet>>(result.SYSExcel.Data);
                }
                else
                {
                    result.SYSExcel = new SYSExcel();
                    result.SYSExcel.Worksheets = new List<Worksheet>();
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DTOORDOrder_Plan_ExcelOnline_ApproveResult ORDOrder_Plan_ExcelOnline_Approve(dynamic dynParam)
        {
            try
            {
                var id = (long)dynParam.id;
                var templateID = (int)dynParam.templateID;
                CATFile file = Newtonsoft.Json.JsonConvert.DeserializeObject<CATFile>(dynParam.File.ToString());

                DTOORDOrder_Plan_ExcelOnline_ApproveResult result = new DTOORDOrder_Plan_ExcelOnline_ApproveResult();
                if (id > 0)
                {
                    ServiceFactory.SVOrder((ISVOrder sv) =>
                    {
                        result = sv.ORDOrder_Plan_ExcelOnline_Approve(id, templateID, file);
                    });
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ORDOrder_Plan_ExcelOnline_LocationToSave(dynamic dynParam)
        {
            try
            {
                var id = (long)dynParam.id;
                var templateID = (int)dynParam.templateID;
                List<DTOORDOrder_Plan_ImportRowResult> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DTOORDOrder_Plan_ImportRowResult>>(dynParam.lst.ToString());

                if (id > 0)
                {
                    ServiceFactory.SVOrder((ISVOrder sv) =>
                    {
                        sv.ORDOrder_Plan_ExcelOnline_LocationToSave(id, templateID, lst);
                    });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region ORDOrder_FTLLO

        [HttpPost]
        public int ORDOrder_FTLLO_Save(dynamic dynParam)
        {
            try
            {
                int result = -1;
                DTOORDOrder item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOORDOrder>(dynParam.item.ToString());
                if (!StringHelper.IsValidCode(item.Code) || item.Code.Length > 50)
                    throw new Exception("Mã đơn hàng không hợp lệ. Không thể lưu.");
                if (item.ListProduct == null || item.ListProduct.Count == 0)
                    throw new Exception("Không có thông tin chi tiết. Không thể lưu.");
                if (item.ETA <= item.ETD)
                    throw new Exception("ĐH sai ràng buộc thời gian ETD-ETA. Không thể lưu.");
                for (var i = 0; i < item.ListProduct.Count; i++)
                {
                    var gop = item.ListProduct[i];
                    if (gop.ETA <= gop.ETD)
                        throw new Exception("Nhóm sản phẩm sai ràng buộc thời gian ETD-ETA. Không thể lưu. Dòng: " + i + 1);
                }
                ServiceFactory.SVOrder((ISVOrder sv) =>
                {
                    result = sv.ORDOrder_FTLLO_Save(item);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region ORDOrder_LTLLO

        [HttpPost]
        public int ORDOrder_LTLLO_Save(dynamic dynParam)
        {
            try
            {
                int result = -1;
                DTOORDOrder item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOORDOrder>(dynParam.item.ToString());
                item.PartnerID = null;
                item.LocationToID = null;
                if (!StringHelper.IsValidCode(item.Code) || item.Code.Length > 50)
                    throw new Exception("Mã đơn hàng không hợp lệ. Không thể lưu.");
                if (item.ListProduct == null || item.ListProduct.Count == 0)
                    throw new Exception("Không có thông tin chi tiết. Không thể lưu.");
                if (item.ETA <= item.ETD)
                    throw new Exception("ĐH sai ràng buộc thời gian ETD-ETA. Không thể lưu.");
                for (var i = 0; i < item.ListProduct.Count; i++)
                {
                    var gop = item.ListProduct[i];
                    if (gop.ETA <= gop.ETD)
                        throw new Exception("Nhóm sản phẩm sai ràng buộc thời gian ETD-ETA. Không thể lưu. Dòng: " + i + 1);
                }
                ServiceFactory.SVOrder((ISVOrder sv) =>
                {
                    result = sv.ORDOrder_LTLLO_Save(item);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region ORDOrder_FCLLO

        [HttpPost]
        public int ORDOrder_FCLLO_Save(dynamic dynParam)
        {
            try
            {
                DTOORDOrder item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOORDOrder>(dynParam.item.ToString());
                if (!StringHelper.IsValidCode(item.Code) || item.Code.Length > 50)
                    throw new Exception("Mã đơn hàng không hợp lệ. Không thể lưu.");
                if (item.ListContainer == null || item.ListContainer.Count == 0)
                    throw new Exception("Không có thông tin chi tiết. Không thể lưu.");
                if (item.ETA <= item.ETD)
                    throw new Exception("ĐH sai ràng buộc thời gian ETD-ETA. Không thể lưu.");
                for (var i = 0; i < item.ListContainer.Count; i++)
                {
                    var con = item.ListContainer[i];
                    if (con.ETA <= con.ETD)
                        throw new Exception("Container sai ràng buộc thời gian ETD-ETA. Không thể lưu. Dòng: " + i + 1);
                }
                int result = -1;
                ServiceFactory.SVOrder((ISVOrder sv) =>
                {
                    result = sv.ORDOrder_FCLLO_Save(item);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region ORDOrder_FCLLOEmpty

        [HttpPost]
        public int ORDOrder_FCLLOEmpty_Save(dynamic dynParam)
        {
            try
            {
                DTOORDOrder item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOORDOrder>(dynParam.item.ToString());
                if (!StringHelper.IsValidCode(item.Code) || item.Code.Length > 50)
                    throw new Exception("Mã đơn hàng không hợp lệ. Không thể lưu.");
                if (item.ListContainer == null || item.ListContainer.Count == 0)
                    throw new Exception("Không có thông tin chi tiết. Không thể lưu.");
                //if (item.ETA == null)
                //    throw new Exception("ĐH thiếu ETA. Không thể lưu.");
                if (item.ETA <= item.ETD)
                    throw new Exception("ĐH sai ràng buộc thời gian ETD-ETA. Không thể lưu.");
                int result = -1;
                ServiceFactory.SVOrder((ISVOrder sv) =>
                {
                    result = sv.ORDOrder_FCLLOEmpty_Save(item);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region ORDOrder_FCLLOLaden

        [HttpPost]
        public int ORDOrder_FCLLOLaden_Save(dynamic dynParam)
        {
            try
            {
                DTOORDOrder item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOORDOrder>(dynParam.item.ToString());
                if (!StringHelper.IsValidCode(item.Code) || item.Code.Length > 50)
                    throw new Exception("Mã đơn hàng không hợp lệ. Không thể lưu.");
                if (item.ListContainer == null || item.ListContainer.Count == 0)
                    throw new Exception("Không có thông tin chi tiết. Không thể lưu.");
                if (item.ETA <= item.ETD)
                    throw new Exception("ĐH sai ràng buộc thời gian ETD-ETA. Không thể lưu.");
                for (var i = 0; i < item.ListContainer.Count; i++)
                {
                    var con = item.ListContainer[i];
                    if (con.ETA <= con.ETD)
                        throw new Exception("Container sai ràng buộc thời gian ETD-ETA. Không thể lưu. Dòng: " + i + 1);
                }
                int result = -1;
                ServiceFactory.SVOrder((ISVOrder sv) =>
                {
                    result = sv.ORDOrder_FCLLOLaden_Save(item);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region ORDOrder_FCLIMEX

        [HttpPost]
        public int ORDOrder_FCLIMEX_Save(dynamic dynParam)
        {
            try
            {
                int result = -1;
                DTOORDOrder item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOORDOrder>(dynParam.item.ToString());
                if (!StringHelper.IsValidCode(item.Code) || item.Code.Length > 50)
                    throw new Exception("Mã đơn hàng không hợp lệ. Không thể lưu.");
                if (item.ListContainer == null || item.ListContainer.Count == 0)
                    throw new Exception("Không có thông tin chi tiết. Không thể lưu.");
                if (item.ETA <= item.ETD)
                    throw new Exception("ĐH sai ràng buộc thời gian ETD-ETA. Không thể lưu.");
                for (var i = 0; i < item.ListContainer.Count; i++)
                {
                    var con = item.ListContainer[i];
                    if (con.ETA <= con.ETD)
                        throw new Exception("Container sai ràng buộc thời gian ETD-ETA. Không thể lưu. Dòng: " + i + 1);
                }
                ServiceFactory.SVOrder((ISVOrder sv) =>
                {
                    result = sv.ORDOrder_FCLIMEX_Save(item);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Tracking

        public List<DTOORDTracking_Order> ORD_Tracking_Order_List(dynamic dynParam)
        {
            try
            {
                List<DTOORDTracking_Order> result = new List<DTOORDTracking_Order>();
                List<int> dataCus = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.dataCus.ToString());
                ServiceFactory.SVOrder((ISVOrder sv) =>
                {
                    result = sv.ORD_Tracking_Order_List(dataCus);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<DTOORDTracking_TOMaster> ORD_Tracking_TripByOrder_List(dynamic dynParam)
        {
            try
            {
                List<DTOORDTracking_TOMaster> result = new List<DTOORDTracking_TOMaster>();
                int orderID = (int)dynParam.orderID;
                ServiceFactory.SVOrder((ISVOrder sv) =>
                {
                    result = sv.ORD_Tracking_TripByOrder_List(orderID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<DTOORDTracking_Location> ORD_Tracking_LocationByTrip_List(dynamic dynParam)
        {
            try
            {
                List<DTOORDTracking_Location> result = new List<DTOORDTracking_Location>();
                int orderID = (int)dynParam.orderID;
                int tripID = (int)dynParam.tripID;
                ServiceFactory.SVOrder((ISVOrder sv) =>
                {
                    result = sv.ORD_Tracking_LocationByTrip_List(orderID, tripID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Cancel

        [HttpPost]
        public DTOResult ORD_DI_Cancel_List(dynamic dynParam)
        {
            try
            {
                var result = new DTOResult();
                string request = dynParam.request.ToString();
                DateTime fDate = Convert.ToDateTime(dynParam.fDate);
                DateTime tDate = Convert.ToDateTime(dynParam.tDate);
                List<int> dataCus = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.dataCus.ToString());

                ServiceFactory.SVOrder((ISVOrder sv) =>
                {
                    result = sv.ORD_DI_Cancel_List(request, dataCus, fDate, tDate);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOResult ORD_CO_Cancel_List(dynamic dynParam)
        {
            try
            {
                var result = new DTOResult();
                string request = dynParam.request.ToString();
                DateTime fDate = Convert.ToDateTime(dynParam.fDate);
                DateTime tDate = Convert.ToDateTime(dynParam.tDate);
                List<int> dataCus = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.dataCus.ToString());

                ServiceFactory.SVOrder((ISVOrder sv) =>
                {
                    result = sv.ORD_CO_Cancel_List(request, dataCus, fDate, tDate);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public List<DTOCATReason> ORD_DI_Cancel_Reason_List(dynamic dynParam)
        {
            try
            {
                var result = new List<DTOCATReason>();
                ServiceFactory.SVOrder((ISVOrder sv) =>
                {
                    result = sv.ORD_DI_Cancel_Reason_List();
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void ORD_DI_Cancel_Change(dynamic dynParam)
        {
            try
            {
                int gopID = (int)dynParam.gopID;
                int reasonID = (int)dynParam.reasonID;
                string reasonNote = dynParam.reasonNote.ToString();
                ServiceFactory.SVOrder((ISVOrder sv) =>
                {
                    sv.ORD_DI_Cancel_Change(gopID, reasonID, reasonNote);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void ORD_CO_Cancel_Change(dynamic dynParam)
        {
            try
            {
                int gopID = (int)dynParam.gopID;
                int reasonID = (int)dynParam.reasonID;
                string reasonNote = dynParam.reasonNote.ToString();
                ServiceFactory.SVOrder((ISVOrder sv) =>
                {
                    sv.ORD_CO_Cancel_Change(gopID, reasonID, reasonNote);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region COTemp

        [HttpPost]
        public DTOResult ORD_COTemp_List(dynamic dynParam)
        {
            try
            {
                var result = new DTOResult();
                string request = dynParam.request.ToString();
                ServiceFactory.SVOrder((ISVOrder sv) =>
                {
                    result = sv.ORD_COTemp_List(request);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOResult ORD_COTemp_Location_List(dynamic dynParam)
        {
            try
            {
                var result = new DTOResult();
                var request = dynParam.request.ToString();
                int cusID = -1, carID = -1, serID = -1, traID = -1, nLocation = 0;
                try
                {
                    cusID = (int)dynParam.cusID;
                }
                catch { }
                try
                {
                    carID = (int)dynParam.carID;
                }
                catch { }
                try
                {
                    serID = (int)dynParam.serID;
                }
                catch { }
                try
                {
                    traID = (int)dynParam.transID;
                }
                catch { }
                try
                {
                    nLocation = (int)dynParam.nLocation;
                }
                catch { }
                ServiceFactory.SVOrder((ISVOrder sv) =>
                {
                    result = sv.ORD_COTemp_Location_List(request, cusID, carID, serID, traID, nLocation);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOORDContainer_TempData ORD_COTemp_Data(dynamic dynParam)
        {
            try
            {
                var result = new DTOORDContainer_TempData();
                ServiceFactory.SVOrder((ISVOrder sv) =>
                {
                    result = sv.ORD_COTemp_Data();
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public List<DTOORDData_Partner> ORD_COTemp_Carrier_List(dynamic dynParam)
        {
            try
            {
                var result = new List<DTOORDData_Partner>();
                var cusID = (int)dynParam.cusID;
                ServiceFactory.SVOrder((ISVOrder sv) =>
                {
                    result = sv.ORD_COTemp_Carrier_List(cusID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void ORD_COTemp_SaveList(dynamic dynParam)
        {
            try
            {
                int total = (int)dynParam.total;
                DTOORDContainer_Temp item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOORDContainer_Temp>(dynParam.item.ToString());
                ServiceFactory.SVOrder((ISVOrder sv) =>
                {
                    sv.ORD_COTemp_SaveList(total, item);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void ORD_COTemp_Update(dynamic dynParam)
        {
            try
            {
                DTOORDContainer_Temp item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOORDContainer_Temp>(dynParam.item.ToString());
                ServiceFactory.SVOrder((ISVOrder sv) =>
                {
                    sv.ORD_COTemp_Update(item);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void ORD_COTemp_DeleteList(dynamic dynParam)
        {
            try
            {
                List<int> data = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.data.ToString());
                ServiceFactory.SVOrder((ISVOrder sv) =>
                {
                    sv.ORD_COTemp_DeleteList(data);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void ORD_COTemp_ToORD(dynamic dynParam)
        {
            try
            {
                List<int> data = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.data.ToString());
                bool isGenCode = (bool)dynParam.isGenCode;
                ServiceFactory.SVOrder((ISVOrder sv) =>
                {
                    sv.ORD_COTemp_ToORD(data, isGenCode);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region ORDPacket

        [HttpPost]
        public List<DTOORDPAK> ORD_PAK_List()
        {
            try
            {
                var result = new List<DTOORDPAK>();
                ServiceFactory.SVOrder((ISVOrder sv) =>
                {
                    result = sv.ORD_PAK_List();
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOResult ORD_PAK_Order_List(dynamic dynParam)
        {
            try
            {
                var result = new DTOResult();
                var pID = (int)dynParam.pID;
                var request = dynParam.request.ToString();
                ServiceFactory.SVOrder((ISVOrder sv) =>
                {
                    result = sv.ORD_PAK_Order_List(request, pID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Inspection
        [HttpPost]
        public DTOResult ORDOrder_Document_OrderList(dynamic dynParam)
        {
            try
            {
                string request = Convert.ToString(dynParam.request);
                var result = new DTOResult();
                ServiceFactory.SVOrder((ISVOrder sv) =>
                {
                    result = sv.ORDOrder_Document_OrderList(request);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public DTOResult ORDOrder_TypeOfDocument_List(dynamic dynParam)
        {
            try
            {
                string request = Convert.ToString(dynParam.request);
                var result = new DTOResult();
                ServiceFactory.SVOrder((ISVOrder sv) =>
                {
                    result = sv.ORDOrder_TypeOfDocument_List(request);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOORDTypeOfDocument ORDOrder_TypeOfDocument_Get(dynamic dynParam)
        {
            try
            {
                int id = (int)dynParam.id;
                var result = new DTOORDTypeOfDocument();
                ServiceFactory.SVOrder((ISVOrder sv) =>
                {
                    result = sv.ORDOrder_TypeOfDocument_Get(id);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void ORDOrder_TypeOfDocument_Save(dynamic dynParam)
        {
            try
            {
                DTOORDTypeOfDocument item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOORDTypeOfDocument>(dynParam.item.ToString());
                ServiceFactory.SVOrder((ISVOrder sv) =>
                {
                    sv.ORDOrder_TypeOfDocument_Save(item);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void ORDOrder_TypeOfDocument_DeleteList(dynamic dynParam)
        {
            try
            {
                List<int> lstID = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.lstID.ToString());
                ServiceFactory.SVOrder((ISVOrder sv) =>
                {
                    sv.ORDOrder_TypeOfDocument_DeleteList(lstID);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOResult ORDOrder_Document_List(dynamic dynParam)
        {
            try
            {
                DateTime dtfrom = Convert.ToDateTime(dynParam.DateFrom.ToString());
                DateTime dtto = Convert.ToDateTime(dynParam.DateTo.ToString());
                List<int> lstCustomerID = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.lstCustomerID.ToString());
                string request = Convert.ToString(dynParam.request);
                var result = new DTOResult();
                ServiceFactory.SVOrder((ISVOrder sv) =>
                {
                    result = sv.ORDOrder_Document_List(request, dtfrom, dtto, lstCustomerID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOORDDocument ORDOrder_Document_Get(dynamic dynParam)
        {
            try
            {
                int id = (int)dynParam.ID;
                var result = new DTOORDDocument();
                ServiceFactory.SVOrder((ISVOrder sv) =>
                {
                    result = sv.ORDOrder_Document_Get(id);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public List<DTOORDTypeOfDocument> ORDOrder_TypeOfDocument_Read()
        {
            try
            {
                var result = new List<DTOORDTypeOfDocument>();
                ServiceFactory.SVOrder((ISVOrder sv) =>
                {
                    result = sv.ORDOrder_TypeOfDocument_Read();
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void ORDOrder_Document_Save(dynamic dynParam)
        {
            try
            {
                DTOORDDocument item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOORDDocument>(dynParam.item.ToString());
                ServiceFactory.SVOrder((ISVOrder sv) =>
                {
                    sv.ORDOrder_Document_Save(item);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void ORDOrder_Document_DeleteList(dynamic dynParam)
        {
            try
            {
                List<int> lstID = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.lstID.ToString());
                ServiceFactory.SVOrder((ISVOrder sv) =>
                {
                    sv.ORDOrder_Document_DeleteList(lstID);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        [HttpPost]
        public List<DTOORDDocumentService> ORDOrder_DocumentService_List(dynamic dynParam)
        {
            try
            {
                int documentID = (int)dynParam.documentID;
                var result = new List<DTOORDDocumentService>();
                ServiceFactory.SVOrder((ISVOrder sv) =>
                {
                    result = sv.ORDOrder_DocumentService_List(documentID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void ORDOrder_DocumentService_Save(dynamic dynParam)
        {
            try
            {
                int documentID = (int)dynParam.documentID;
                List<DTOORDDocumentService> item = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DTOORDDocumentService>>(dynParam.lstService.ToString());
                ServiceFactory.SVOrder((ISVOrder sv) =>
                {
                    sv.ORDOrder_DocumentService_Save(documentID, item);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public List<DTOORDDocumentService> ORDOrder_DocumentService_NotInList(dynamic dynParam)
        {
            try
            {
                int documentID = (int)dynParam.documentID;
                var result = new List<DTOORDDocumentService>();
                ServiceFactory.SVOrder((ISVOrder sv) =>
                {
                    result = sv.ORDOrder_DocumentService_NotInList(documentID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        [HttpPost]
        public void ORDOrder_DocumentService_NotInList_Save(dynamic dynParam)
        {
            try
            {
                int documentID = (int)dynParam.documentID;
                List<int> lstServiceID = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.lstServiceID.ToString());
                ServiceFactory.SVOrder((ISVOrder sv) =>
                {
                    sv.ORDOrder_DocumentService_NotInList_Save(documentID, lstServiceID);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void ORDOrder_DocumentService_DeleteList(dynamic dynParam)
        {
            try
            {
                List<int> lstID = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.lstID.ToString());
                ServiceFactory.SVOrder((ISVOrder sv) =>
                {
                    sv.ORDOrder_DocumentService_DeleteList(lstID);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public List<DTOORDDocumentDetail> ORDOrder_DocumentDetail_List(dynamic dynParam)
        {
            try
            {
                int documentServiceID = (int)dynParam.documentServiceID;
                var result = new List<DTOORDDocumentDetail>();
                ServiceFactory.SVOrder((ISVOrder sv) =>
                {
                    result = sv.ORDOrder_DocumentDetail_List(documentServiceID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void ORDOrder_DocumentDetail_SaveList(dynamic dynParam)
        {
            try
            {
                int documentServiceID = (int)dynParam.documentServiceID;
                List<DTOORDDocumentDetail> lstDetail = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DTOORDDocumentDetail>>(dynParam.lstDetail.ToString());
                ServiceFactory.SVOrder((ISVOrder sv) =>
                {
                    sv.ORDOrder_DocumentDetail_SaveList(documentServiceID, lstDetail);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void ORDOrder_DocumentDetail_DeleteList(dynamic dynParam)
        {
            try
            {
                List<int> lstID = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.lstID.ToString());
                ServiceFactory.SVOrder((ISVOrder sv) =>
                {
                    sv.ORDOrder_DocumentDetail_DeleteList(lstID);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public string ORDOrder_DocumentDetail_ExcelExport(dynamic dynParam)
        {
            try
            {
                int documentServiceID = (int)dynParam.documentServiceID;
                var result = string.Empty;
                var lst = new List<DTOORDDocumentDetail>();
                ServiceFactory.SVOrder((ISVOrder sv) =>
                {
                    lst = sv.ORDOrder_DocumentDetail_Export(documentServiceID);
                });
                string filepath = "/" + FolderUpload.Export + "export" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx";
                if (System.IO.File.Exists(HttpContext.Current.Server.MapPath(filepath)))
                    System.IO.File.Delete(HttpContext.Current.Server.MapPath(filepath));
                FileInfo exportfile = new FileInfo(HttpContext.Current.Server.MapPath(filepath));
                using (ExcelPackage package = new ExcelPackage(exportfile))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Sheet 1");

                    int col = 0;
                    int row = 0;
                    row = 1;
                    col = 1;
                    worksheet.Cells[row, col].Value = "STT";
                    col++; worksheet.Cells[row, col].Value = "Mã";
                    col++; worksheet.Cells[row, col].Value = "Ngày gửi";
                    col++; worksheet.Cells[row, col].Value = "Ghi chú";
                    col++; worksheet.Cells[row, col].Value = "Hoàn thành";
                    col++; worksheet.Cells[row, col].Value = "Người hoàn thành";
                    col++; worksheet.Cells[row, col].Value = "Ngày hoàn thành";
                    for (int i = 1; i <= col; i++)
                        ExcelHelper.CreateCellStyle(worksheet, row, i, row, i, true, true, ExcelHelper.ColorGreen, ExcelHelper.ColorWhite, 0, "");
                    row++;
                    int stt = 1;
                    foreach (var item in lst)
                    {
                        col = 1;
                        worksheet.Cells[row, col].Value = stt;
                        col++; worksheet.Cells[row, col].Value = item.Code; worksheet.Column(col).Width = 20;
                        col++; worksheet.Cells[row, col].Value = item.SendDate; worksheet.Column(col).Width = 20;
                        ExcelHelper.CreateFormat(worksheet, row, col, ExcelHelper.FormatDDMMYYYY);
                        col++; worksheet.Cells[row, col].Value = item.Note; worksheet.Column(col).Width = 20;
                        col++; worksheet.Cells[row, col].Value = item.IsComplete ? "X" : "";
                        col++; worksheet.Cells[row, col].Value = item.CompleteBy; worksheet.Column(col).Width = 20;
                        col++; worksheet.Cells[row, col].Value = item.CompleteDate; worksheet.Column(col).Width = 20;
                        ExcelHelper.CreateFormat(worksheet, row, col, ExcelHelper.FormatDDMMYYYY);
                        stt++;
                        row++;
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
                result = filepath;
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public List<DTOORDDocumentDetail> ORDOrder_DocumentDetail_ExcelCheck(dynamic dynParam)
        {
            try
            {
                CATFile item = Newtonsoft.Json.JsonConvert.DeserializeObject<CATFile>(dynParam.item.ToString());
                var result = new List<DTOORDDocumentDetail>();
                if (item != null && !string.IsNullOrEmpty(item.FilePath))
                {
                    using (System.IO.FileStream fs = new System.IO.FileStream(HttpContext.Current.Server.MapPath("/" + item.FilePath), System.IO.FileMode.Open, System.IO.FileAccess.Read))
                    {
                        using (var package = new ExcelPackage(fs))
                        {
                            ExcelWorksheet worksheet = ExcelHelper.GetWorksheetByIndex(package, 1);
                            if (worksheet != null)
                            {
                                int col = 0;
                                int row = 0;
                                for (row = 2; row <= worksheet.Dimension.End.Row; row++)
                                {
                                    DTOORDDocumentDetail obj = new DTOORDDocumentDetail();
                                    List<string> lstError = new List<string>();
                                    col = 1;
                                    col++; string strCode = ExcelHelper.GetValue(worksheet, row, col);
                                    col++; string SendDate = ExcelHelper.GetValue(worksheet, row, col);
                                    col++; string strNote = ExcelHelper.GetValue(worksheet, row, col);
                                    col++; string IsComplete = ExcelHelper.GetValue(worksheet, row, col);
                                    col++; string CompleteBy = ExcelHelper.GetValue(worksheet, row, col);
                                    col++; string CompleteDate = ExcelHelper.GetValue(worksheet, row, col);
                                    obj.Code = strCode;
                                    obj.ExcelRow = row;
                                    obj.ExcelSuccess = true;
                                    if (!string.IsNullOrEmpty(SendDate))
                                    {
                                        try
                                        {
                                            obj.SendDate = ExcelHelper.ValueToDate(SendDate);
                                        }
                                        catch
                                        {
                                            try
                                            {
                                                obj.SendDate = Convert.ToDateTime(SendDate);
                                            }
                                            catch
                                            {
                                                lstError.Add("Ngày gửi[" + SendDate + "] không chính xác");
                                            }

                                        }
                                    }
                                    else obj.SendDate = null;
                                    obj.Note = strNote;
                                    if (!string.IsNullOrEmpty(IsComplete))
                                    {
                                        if (IsComplete == "X")
                                        {
                                            obj.IsComplete = true;
                                        }
                                        else
                                        {
                                            lstError.Add("Hoàn thành[" + IsComplete + "] không chính xác");
                                        }
                                    }
                                    else obj.IsComplete = false;
                                    obj.CompleteBy = CompleteBy;
                                    if (!string.IsNullOrEmpty(CompleteDate))
                                    {
                                        try
                                        {
                                            obj.CompleteDate = ExcelHelper.ValueToDate(CompleteDate);
                                        }
                                        catch
                                        {
                                            try
                                            {
                                                obj.CompleteDate = Convert.ToDateTime(CompleteDate);
                                            }
                                            catch
                                            {
                                                lstError.Add("Ngày hoàn thành[" + CompleteDate + "] không chính xác");
                                            }

                                        }
                                    }
                                    else obj.SendDate = null;
                                    if (lstError.Count > 0) obj.ExcelSuccess = false;
                                    else obj.ExcelSuccess = true;
                                    result.Add(obj);
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
        public void ORDOrder_DocumentDetail_ExcelSave(dynamic dynParam)
        {
            try
            {
                int documentServiceID = (int)dynParam.documentServiceID;
                List<DTOORDDocumentDetail> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DTOORDDocumentDetail>>(dynParam.lst.ToString());
                ServiceFactory.SVOrder((ISVOrder sv) =>
                {
                    sv.ORDOrder_DocumentDetail_Import(documentServiceID, lst);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public List<DTOORDDocumentContainer> ORDOrder_DocumentContainer_List(dynamic dynParam)
        {
            try
            {
                int documentID = Convert.ToInt32(dynParam.documentID);
                var result = new List<DTOORDDocumentContainer>();
                ServiceFactory.SVOrder((ISVOrder sv) =>
                {
                    result = sv.ORDOrder_DocumentContainer_List(documentID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOResult ORDOrder_DocumentContainer_NotInList(dynamic dynParam)
        {
            try
            {
                int documentID = Convert.ToInt32(dynParam.documentID);
                string request = Convert.ToString(dynParam.request);
                var result = new DTOResult();
                ServiceFactory.SVOrder((ISVOrder sv) =>
                {
                    result = sv.ORDOrder_DocumentContainer_NotInList(documentID, request);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void ORDOrder_DocumentContainer_NotInList_Save(dynamic dynParam)
        {
            try
            {
                List<int> lstContainerID = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.lstContainerID.ToString());
                int documentID = Convert.ToInt32(dynParam.documentID);
                ServiceFactory.SVOrder((ISVOrder sv) =>
                {
                    sv.ORDOrder_DocumentContainer_NotInList_Save(documentID, lstContainerID);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void ORDOrder_DocumentContainer_DeleteList(dynamic dynParam)
        {
            try
            {
                List<int> lstID = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.lstID.ToString());
                ServiceFactory.SVOrder((ISVOrder sv) =>
                {
                    sv.ORDOrder_DocumentContainer_DeleteList(lstID);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        [HttpPost]
        public DTOORDContainerService_Data ORDOrder_ContainerInService_List(dynamic dynParam)
        {
            try
            {
                int documentID = (int)dynParam.documentID;
                var result = new DTOORDContainerService_Data();
                ServiceFactory.SVOrder((ISVOrder sv) =>
                {
                    result = sv.ORDOrder_ContainerInService_List(documentID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void ORDOrder_ContainerInService_NotInList_Save(dynamic dynParam)
        {
            try
            {
                List<DTOORDContainerService_Detail> lstContainerService = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DTOORDContainerService_Detail>>(dynParam.lstContainerService.ToString());
                int documentID = Convert.ToInt32(dynParam.documentID);
                ServiceFactory.SVOrder((ISVOrder sv) =>
                {
                    sv.ORDOrder_ContainerInService_NotInList_Save(documentID, lstContainerService);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        [HttpPost]
        public DTOResult ORDOrder_DocumentContainer_Read(dynamic dynParam)
        {
            try
            {
                DateTime dtfrom = Convert.ToDateTime(dynParam.DateFrom.ToString());
                DateTime dtto = Convert.ToDateTime(dynParam.DateTo.ToString());
                List<int> lstCustomerID = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.lstCustomerID.ToString());
                string request = Convert.ToString(dynParam.request);
                var result = new DTOResult();
                ServiceFactory.SVOrder((ISVOrder sv) =>
                {
                    result = sv.ORDOrder_DocumentContainer_Read(request, dtfrom, dtto, lstCustomerID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region ORDOrder route
        [HttpPost]
        public DTOResult ORDOrderRoute_List(dynamic dynParam)
        {
            try
            {
                string request = dynParam.request.ToString();
                bool isClosed = Convert.ToBoolean(dynParam.isClosed.ToString());
                var result = new DTOResult();
                ServiceFactory.SVOrder((ISVOrder sv) =>
                {
                    result = sv.ORDOrderRoute_List(request, isClosed);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public DTOORDRoute ORDOrderRoute_Get(dynamic dynParam)
        {
            try
            {
                int id = (int)dynParam.id;
                var result = default(DTOORDRoute);
                ServiceFactory.SVOrder((ISVOrder sv) =>
                {
                    result = sv.ORDOrderRoute_Get(id);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public int ORDOrderRoute_Save(dynamic dynParam)
        {
            try
            {
                DTOORDRoute item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOORDRoute>(dynParam.item.ToString());
                var result = -1;
                ServiceFactory.SVOrder((ISVOrder sv) =>
                {
                    result = sv.ORDOrderRoute_Save(item);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public void ORDOrderRoute_Delete(dynamic dynParam)
        {
            try
            {
                int id = (int)dynParam.id;
                ServiceFactory.SVOrder((ISVOrder sv) =>
                {
                    sv.ORDOrderRoute_Delete(id);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOResult ORDOrderRoute_OrderList(dynamic dynParam)
        {
            try
            {
                string request = dynParam.request.ToString();
                int ordRouteId = (int)dynParam.ordRouteId;
                var result = new DTOResult();
                ServiceFactory.SVOrder((ISVOrder sv) =>
                {
                    result = sv.ORDOrderRoute_OrderList(request, ordRouteId);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public void ORDOrderRoute_OrderSaveList(dynamic dynParam)
        {
            try
            {
                List<int> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.lst.ToString());
                int ordRouteId = (int)dynParam.ordRouteId;
                ServiceFactory.SVOrder((ISVOrder sv) =>
                {
                    sv.ORDOrderRoute_OrderSaveList(lst, ordRouteId);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public void ORDOrderRoute_OrderDelete(dynamic dynParam)
        {
            try
            {
                int id = (int)dynParam.id;
                ServiceFactory.SVOrder((ISVOrder sv) =>
                {
                    sv.ORDOrderRoute_OrderDelete(id);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public DTOResult ORDOrderRoute_OrderNotInList(dynamic dynParam)
        {
            try
            {
                string request = dynParam.request.ToString();
                int ordRouteId = (int)dynParam.ordRouteId;
                var result = new DTOResult();
                ServiceFactory.SVOrder((ISVOrder sv) =>
                {
                    result = sv.ORDOrderRoute_OrderNotInList(request, ordRouteId);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void ORDOrderRoute_OrderApproved(dynamic dynParam)
        {
            try
            {
                int id = (int)dynParam.id;
                ServiceFactory.SVOrder((ISVOrder sv) =>
                {
                    sv.ORDOrderRoute_OrderApproved(id);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public void ORDOrderRoute_OrderUnApproved(dynamic dynParam)
        {
            try
            {
                int id = (int)dynParam.id;
                ServiceFactory.SVOrder((ISVOrder sv) =>
                {
                    sv.ORDOrderRoute_OrderUnApproved(id);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOResult ORDOrderRoute_RouteDetailList(dynamic dynParam)
        {
            try
            {
                string request = dynParam.request.ToString();
                int ordRouteId = (int)dynParam.ordRouteId;
                var result = new DTOResult();
                ServiceFactory.SVOrder((ISVOrder sv) =>
                {
                    result = sv.ORDOrderRoute_RouteDetailList(request, ordRouteId);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public DTOORDRouteDetail ORDOrderRoute_RouteDetailGet(dynamic dynParam)
        {
            try
            {
                int id = (int)dynParam.id;
                var result = default(DTOORDRouteDetail);
                ServiceFactory.SVOrder((ISVOrder sv) =>
                {
                    result = sv.ORDOrderRoute_RouteDetailGet(id);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public int ORDOrderRoute_RouteDetailSave(dynamic dynParam)
        {
            try
            {
                DTOORDRouteDetail item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOORDRouteDetail>(dynParam.item.ToString());
                int ordRouteId = (int)dynParam.ordRouteId;
                var result = -1;
                ServiceFactory.SVOrder((ISVOrder sv) =>
                {
                    result = sv.ORDOrderRoute_RouteDetailSave(item, ordRouteId);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public void ORDOrderRoute_RouteDetailDelete(dynamic dynParam)
        {
            try
            {
                int id = (int)dynParam.id;
                ServiceFactory.SVOrder((ISVOrder sv) =>
                {
                    sv.ORDOrderRoute_RouteDetailDelete(id);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public void ORDOrderRoute_RouteDetailComplete(dynamic dynParam)
        {
            try
            {
                int id = (int)dynParam.id;
                ServiceFactory.SVOrder((ISVOrder sv) =>
                {
                    sv.ORDOrderRoute_RouteDetailComplete(id);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public void ORDOrderRoute_RouteDetailRun(dynamic dynParam)
        {
            try
            {
                int id = (int)dynParam.id;
                ServiceFactory.SVOrder((ISVOrder sv) =>
                {
                    sv.ORDOrderRoute_RouteDetailRun(id);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public DTOCATVessel ORDOrderRoute_RouteDetail_AddVessel(dynamic dynParam)
        {
            try
            {
                DTOCATVessel item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOCATVessel>(dynParam.item.ToString());
                int cuspartnerid = (int)dynParam.cuspartnerid;
                var result = default(DTOCATVessel);
                ServiceFactory.SVOrder((ISVOrder sv) =>
                {
                    result = sv.ORDOrderRoute_RouteDetail_AddVessel(item, cuspartnerid);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOORDRouteData ORDOrderRoute_LocationData()
        {
            try
            {
                var result = new DTOORDRouteData();
                ServiceFactory.SVOrder((ISVOrder sv) =>
                {
                    result = sv.ORDOrderRoute_LocationData();
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //STMS-2404
        [HttpPost]
        public void ORDOrderRoute_CreateOrderChilds(dynamic dynParam)
        {
            try
            {
                int id = (int)dynParam.id;
                ServiceFactory.SVOrder((ISVOrder sv) =>
                {
                    sv.ORDOrderRoute_CreateOrderChilds(id);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public void ORDOrderRoute_ClearOrderChilds(dynamic dynParam)
        {
            try
            {
                int id = (int)dynParam.id;
                ServiceFactory.SVOrder((ISVOrder sv) =>
                {
                    sv.ORDOrderRoute_ClearOrderChilds(id);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        [HttpPost]
        public DTOResult ORDOrderRoute_RouteDetail_RouteContainerList(dynamic dynParam)
        {
            try
            {
                string request = dynParam.request.ToString();
                int routeDetailID = (int)dynParam.routeDetailID;
                var result = new DTOResult();
                ServiceFactory.SVOrder((ISVOrder sv) =>
                {
                    result = sv.ORDOrderRoute_RouteDetail_RouteContainerList(request, routeDetailID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public DTOORDRouteContainer ORDOrderRoute_RouteDetail_RouteContainerGet(dynamic dynParam)
        {
            try
            {
                int id = (int)dynParam.id;
                var result = default(DTOORDRouteContainer);
                ServiceFactory.SVOrder((ISVOrder sv) =>
                {
                    result = sv.ORDOrderRoute_RouteDetail_RouteContainerGet(id);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public long ORDOrderRoute_RouteDetail_RouteContainerSave(dynamic dynParam)
        {
            try
            {
                DTOORDRouteContainer item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOORDRouteContainer>(dynParam.item.ToString());
                int routeDetailID = (int)dynParam.routeDetailID;
                long result = -1;
                ServiceFactory.SVOrder((ISVOrder sv) =>
                {
                    result = sv.ORDOrderRoute_RouteDetail_RouteContainerSave(item, routeDetailID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public void ORDOrderRoute_RouteDetail_RouteContainerDetele(dynamic dynParam)
        {
            try
            {
                int id = (int)dynParam.id;
                ServiceFactory.SVOrder((ISVOrder sv) =>
                {
                    sv.ORDOrderRoute_RouteDetail_RouteContainerDetele(id);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOResult ORDOrderRoute_RouteDetail_RouteProductList(dynamic dynParam)
        {
            try
            {
                string request = dynParam.request.ToString();
                int routeDetailID = (int)dynParam.routeDetailID;
                var result = new DTOResult();
                ServiceFactory.SVOrder((ISVOrder sv) =>
                {
                    result = sv.ORDOrderRoute_RouteDetail_RouteProductList(request, routeDetailID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public DTOResult ORDOrderRoute_RouteDetail_RouteProductNotInList(dynamic dynParam)
        {
            try
            {
                string request = dynParam.request.ToString();
                int routeDetailID = (int)dynParam.routeDetailID;
                int ordRouteId = (int)dynParam.ordRouteId;
                var result = new DTOResult();
                ServiceFactory.SVOrder((ISVOrder sv) =>
                {
                    result = sv.ORDOrderRoute_RouteDetail_RouteProductNotInList(request, ordRouteId, routeDetailID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public void ORDOrderRoute_RouteDetail_RouteProductNotInSaveList(dynamic dynParam)
        {
            try
            {
                List<int> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.lst.ToString());
                int routeDetailID = (int)dynParam.routeDetailID;
                ServiceFactory.SVOrder((ISVOrder sv) =>
                {
                    sv.ORDOrderRoute_RouteDetail_RouteProductNotInSaveList(lst, routeDetailID);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public DTOResult ORDOrderRoute_RouteDetail_RouteProduct_ContainerList(dynamic dynParam)
        {
            try
            {
                string request = dynParam.request.ToString();
                int routeDetailID = (int)dynParam.routeDetailID;
                var result = new DTOResult();
                ServiceFactory.SVOrder((ISVOrder sv) =>
                {
                    result = sv.ORDOrderRoute_RouteDetail_RouteProduct_ContainerList(request, routeDetailID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public void ORDOrderRoute_RouteDetail_RouteProduct_UpdateContainer(dynamic dynParam)
        {
            try
            {
                int routeGOPID = (int)dynParam.routeGOPID;
                int routeContID = (int)dynParam.routeContID;
                ServiceFactory.SVOrder((ISVOrder sv) =>
                {
                    sv.ORDOrderRoute_RouteDetail_RouteProduct_UpdateContainer(routeGOPID, routeContID);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}