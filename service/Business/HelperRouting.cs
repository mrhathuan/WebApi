using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kendo.Mvc.Extensions;
using Data;
using DTO;
using System.ServiceModel;
using Kendo.Mvc.UI;
using Newtonsoft.Json;

namespace Business
{
    public class HelperRouting
    {
        #region CATRouting
        public static void ORDOrder_CUSRouting_Check(DataEntities model, AccountItem Account, List<int> data)
        {
            var dataOrder = model.ORD_Order.Where(c => data.Contains(c.ID)).Select(c => new { c.ID, c.ContractID, c.ContractTermID, c.CustomerID }).ToList();
            var dataGroupProduct = model.ORD_GroupProduct.Where(c => data.Contains(c.OrderID) && c.CUSRoutingID == null).Select(c => new { c.ID, c.OrderID, c.LocationFromID, c.LocationToID }).ToList();
            var dataContainer = model.ORD_Container.Where(c => data.Contains(c.OrderID) && c.CUSRoutingID == null).Select(c => new { c.ID, c.OrderID, c.LocationFromID, c.LocationToID, c.LocationDepotID, c.LocationDepotReturnID }).ToList();
            Dictionary<int, int> dicGop = new Dictionary<int, int>();
            Dictionary<int, int> dicCon = new Dictionary<int, int>();
            foreach (var item in dataOrder)
            {
                foreach (var gop in dataGroupProduct.Where(c => c.OrderID == item.ID && c.LocationFromID.HasValue && c.LocationToID.HasValue))
                {
                    var cusRoutingID = ORDOrder_CUSRouting_FindDI(model, Account, item.CustomerID, item.ContractID, gop.LocationFromID.Value, gop.LocationToID.Value);
                    if (cusRoutingID > 0)
                        dicGop.Add(gop.ID, cusRoutingID);
                }
                foreach (var con in dataContainer.Where(c => c.OrderID == item.ID))
                {
                    var cusRoutingID = ORDOrder_CUSRouting_FindCO(model, Account, item.CustomerID, item.ContractID, item.ContractTermID, con.LocationFromID.Value, con.LocationToID.Value, con.LocationDepotID, con.LocationDepotReturnID);
                    if (cusRoutingID > 0)
                        dicCon.Add(con.ID, cusRoutingID);
                }
            }
            if (dicGop.Count > 0 || dicCon.Count > 0)
            {
                foreach (var item in dicGop)
                {
                    var obj = model.ORD_GroupProduct.FirstOrDefault(c => c.ID == item.Key);
                    if (obj != null)
                    {
                        obj.ModifiedBy = Account.UserName;
                        obj.ModifiedDate = DateTime.Now;
                        obj.CUSRoutingID = item.Value;
                    }
                }
                foreach (var item in dicCon)
                {
                    var obj = model.ORD_Container.FirstOrDefault(c => c.ID == item.Key);
                    if (obj != null)
                    {
                        obj.ModifiedBy = Account.UserName;
                        obj.ModifiedDate = DateTime.Now;
                        obj.CUSRoutingID = item.Value;
                    }
                }
                model.SaveChanges();
            }
        }

        public static int ORDOrder_CUSRouting_FindDI(DataEntities model, AccountItem Account, int cusID, int? conID, int fromID, int toID)
        {
            int result = -1;

            var fLocation = model.CUS_Location.Where(c => c.ID == fromID).Select(c => new { c.CAT_Location.ID, c.CAT_Location.ProvinceID, ProvinceCode = c.CAT_Location.CAT_Province.Code, c.CAT_Location.DistrictID, DistrictCode = c.CAT_Location.CAT_District.Code }).FirstOrDefault();
            var tLocation = model.CUS_Location.Where(c => c.ID == toID).Select(c => new { c.CAT_Location.ID, c.CAT_Location.ProvinceID, ProvinceCode = c.CAT_Location.CAT_Province.Code, c.CAT_Location.DistrictID, DistrictCode = c.CAT_Location.CAT_District.Code }).FirstOrDefault();
            if (fLocation != null && tLocation != null)
            {
                //Nếu có hợp đồng => Tìm cung đường trong hợp đồng
                if (conID > 0)
                {
                    var dataRouting = model.CAT_ContractRouting.Where(c => c.ContractID == conID && c.ContractRoutingTypeID == -(int)SYSVarType.ContractRoutingTypePrice && c.CAT_Routing.LocationFromID == fLocation.ID && c.CAT_Routing.LocationToID == tLocation.ID)
                        .OrderByDescending(c => c.ContractTermID).Select(c => c.RoutingID).ToList();
                    if (dataRouting.Count > 0)
                    {
                        var objRoutePrice = model.CAT_PriceDIGroupProduct.Where(c => dataRouting.Contains(c.CAT_ContractRouting.RoutingID) && c.Price > 0).Select(c => new { c.CAT_ContractRouting.RoutingID }).FirstOrDefault();
                        if (objRoutePrice == null)
                            objRoutePrice = model.CAT_PriceDILevelGroupProduct.Where(c => dataRouting.Contains(c.CAT_ContractRouting.RoutingID) && c.Price > 0).Select(c => new { c.CAT_ContractRouting.RoutingID }).FirstOrDefault();
                        if (objRoutePrice != null)
                        {
                            var cusRoute = model.CUS_Routing.FirstOrDefault(c => c.CustomerID == cusID && c.RoutingID == objRoutePrice.RoutingID);
                            if (cusRoute != null)
                                result = cusRoute.ID;
                        }
                        else
                        {
                            int tmp = dataRouting[0];
                            var cusRoute = model.CUS_Routing.FirstOrDefault(c => c.CustomerID == cusID && c.RoutingID == tmp);
                            if (cusRoute != null)
                                result = cusRoute.ID;
                        }
                    }
                    else
                    {
                        foreach (var objRoutingArea in model.CAT_ContractRouting.Where(c => c.ContractID == conID && c.ContractRoutingTypeID == -(int)SYSVarType.ContractRoutingTypePrice && c.CAT_Routing.RoutingAreaFromID != null && c.CAT_Routing.RoutingAreaToID != null).Select(c => new
                            {
                                c.RoutingID,
                                RoutingAreaFromID = c.CAT_Routing.RoutingAreaFromID.Value,
                                RoutingAreaToID = c.CAT_Routing.RoutingAreaToID.Value,
                                ContractTermID = c.ContractTermID > 0 ? c.ContractTermID.Value : -1
                            }).OrderByDescending(c => c.ContractTermID))
                        {
                            if (model.CAT_RoutingAreaLocation.Where(c => c.RoutingAreaID == objRoutingArea.RoutingAreaFromID && c.LocationID == fLocation.ID).Count() > 0 &&
                                model.CAT_RoutingAreaLocation.Where(c => c.RoutingAreaID == objRoutingArea.RoutingAreaToID && c.LocationID == tLocation.ID).Count() > 0)
                            {
                                var cusRoute = model.CUS_Routing.FirstOrDefault(c => c.CustomerID == cusID && c.RoutingID == objRoutingArea.RoutingID);
                                if (cusRoute != null)
                                    result = cusRoute.ID;
                                break;
                            }
                        }
                    }
                }
                else
                {
                    var objRouting = model.CUS_Routing.Where(c => c.CustomerID == cusID && c.CAT_Routing.LocationFromID == fromID && c.CAT_Routing.LocationToID == toID).Select(c => new { c.ID }).FirstOrDefault();
                    if (objRouting != null)
                    {
                        result = objRouting.ID;
                    }
                    else
                    {
                        foreach (var objRoutingArea in model.CUS_Routing.Where(c => c.CustomerID == cusID && c.CAT_Routing.RoutingAreaFromID != null && c.CAT_Routing.RoutingAreaToID != null)
                            .OrderByDescending(c => c.CAT_Routing.ParentID.HasValue).ThenByDescending(c => c.CAT_Routing.ParentID).Select(c => new
                            {
                                c.ID,
                                RoutingAreaFromID = c.CAT_Routing.RoutingAreaFromID.Value,
                                RoutingAreaToID = c.CAT_Routing.RoutingAreaToID.Value
                            }))
                        {
                            if (model.CAT_RoutingAreaLocation.Where(c => c.RoutingAreaID == objRoutingArea.RoutingAreaFromID && c.LocationID == fromID).Count() > 0 &&
                                model.CAT_RoutingAreaLocation.Where(c => c.RoutingAreaID == objRoutingArea.RoutingAreaToID && c.LocationID == toID).Count() > 0)
                            {
                                result = objRoutingArea.ID;
                                break;
                            }
                        }
                    }
                }
            }

            if (result < 1)
            {
                bool flag = false;
                string strTemp = "__temp__";
                var objCATRouting = model.CAT_Routing.FirstOrDefault(c => c.Code == strTemp);
                if (objCATRouting == null)
                {
                    objCATRouting = new CAT_Routing();
                    objCATRouting.CreatedBy = Account.UserName;
                    objCATRouting.CreatedDate = DateTime.Now;
                    objCATRouting.IsAreaLast = false;
                    objCATRouting.EDistance = 0;
                    objCATRouting.EHours = 0;
                    objCATRouting.Code = strTemp;
                    objCATRouting.RoutingName = strTemp;
                    objCATRouting.IsUse = true;
                    objCATRouting.Note = "";
                    objCATRouting.IsLocation = false;
                    model.CAT_Routing.Add(objCATRouting);
                    flag = true;
                }
                var cusRouting = model.CUS_Routing.FirstOrDefault(c => c.RoutingID == objCATRouting.ID && c.CustomerID == cusID);
                if (cusRouting == null)
                {
                    cusRouting = new CUS_Routing();
                    cusRouting.CustomerID = cusID;
                    cusRouting.Code = objCATRouting.Code;
                    cusRouting.RoutingName = objCATRouting.RoutingName;
                    cusRouting.CreatedBy = Account.UserName;
                    cusRouting.CreatedDate = DateTime.Now;
                    cusRouting.CAT_Routing = objCATRouting;
                    model.CUS_Routing.Add(cusRouting);
                    flag = true;
                }
                if (flag)
                    model.SaveChanges();
                result = cusRouting.ID;
            }
            return result;
        }

        public static int ORDOrder_CUSRouting_FindCO(DataEntities model, AccountItem Account, int cusID, int? contractid, int? contracttermid, int fromid, int toid, int? getid = null, int? returnid = null)
        {
            int result = -1;

            var cusLocationFrom = model.CUS_Location.Where(c => c.ID == fromid).Select(c => new { c.CAT_Location.ID, ProvinceCode = c.CAT_Location.CAT_Province.Code, DistrictCode = c.CAT_Location.CAT_District.Code }).FirstOrDefault();
            var cusLocationTo = model.CUS_Location.Where(c => c.ID == toid).Select(c => new { c.CAT_Location.ID, ProvinceCode = c.CAT_Location.CAT_Province.Code, DistrictCode = c.CAT_Location.CAT_District.Code }).FirstOrDefault();
            var cusLocationGet = model.CUS_Location.Where(c => c.ID == getid).Select(c => new { c.CAT_Location.ID, ProvinceCode = c.CAT_Location.CAT_Province.Code, DistrictCode = c.CAT_Location.CAT_District.Code }).FirstOrDefault();
            var cusLocationReturn = model.CUS_Location.Where(c => c.ID == returnid).Select(c => new { c.CAT_Location.ID, ProvinceCode = c.CAT_Location.CAT_Province.Code, DistrictCode = c.CAT_Location.CAT_District.Code }).FirstOrDefault();
            var lstCATRoutingID = new List<int>();
            if (cusLocationFrom != null && cusLocationTo != null)
            {
                if (contractid > 0)
                {
                    var queryRoutingLocation = model.CAT_ContractRouting.Where(c => c.ContractID == contractid.Value && c.ContractRoutingTypeID == -(int)SYSVarType.ContractRoutingTypePrice && c.CAT_Routing.LocationFromID == cusLocationFrom.ID && c.CAT_Routing.LocationToID == cusLocationTo.ID)
                        .OrderByDescending(c => c.ContractTermID).Select(c => c.RoutingID);
                    if (contracttermid > 0)
                        queryRoutingLocation = model.CAT_ContractRouting.Where(c => c.ContractID == contractid.Value && c.ContractTermID == contracttermid.Value && c.ContractRoutingTypeID == -(int)SYSVarType.ContractRoutingTypePrice && c.CAT_Routing.LocationFromID == cusLocationFrom.ID && c.CAT_Routing.LocationToID == cusLocationTo.ID)
                        .OrderByDescending(c => c.ContractTermID).Select(c => c.RoutingID);
                    var lstRoutingLocationID = queryRoutingLocation.ToList();
                    if (lstRoutingLocationID.Count > 0)
                    {
                        var objRoutePrice = model.CAT_PriceCOContainer.Where(c => lstRoutingLocationID.Contains(c.CAT_ContractRouting.RoutingID) && c.Price > 0).Select(c => new { c.CAT_ContractRouting.RoutingID, c.CAT_ContractRouting.Code, c.CAT_ContractRouting.RoutingName }).FirstOrDefault();
                        if (objRoutePrice != null)
                        {
                            var cusRouting = model.CUS_Routing.FirstOrDefault(c => c.CustomerID == cusID && c.RoutingID == objRoutePrice.RoutingID);
                            if (cusRouting == null)
                                lstCATRoutingID.Add(objRoutePrice.RoutingID);

                            if (cusRouting != null)
                                result = cusRouting.ID;
                        }
                        else
                        {
                            int firstid = lstRoutingLocationID[0];
                            var cusRouting = model.CUS_Routing.FirstOrDefault(c => c.CustomerID == cusID && c.RoutingID == firstid);
                            if (cusRouting == null)
                                lstCATRoutingID.Add(firstid);
                            if (cusRouting != null)
                                result = cusRouting.ID;
                        }
                    }
                    else
                    {
                        var queryRoutingArea = model.CAT_ContractRouting.Where(c => c.ContractID == contractid.Value && c.ContractRoutingTypeID == -(int)SYSVarType.ContractRoutingTypePrice && c.CAT_Routing.RoutingAreaFromID != null && c.CAT_Routing.RoutingAreaToID != null).Select(c => new
                        {
                            c.RoutingID,
                            c.Code,
                            c.RoutingName,
                            RoutingAreaFromID = c.CAT_Routing.RoutingAreaFromID.Value,
                            RoutingAreaToID = c.CAT_Routing.RoutingAreaToID.Value,
                            ContractTermID = c.ContractTermID > 0 ? c.ContractTermID.Value : -1
                        }).OrderByDescending(c => c.ContractTermID);
                        if (contracttermid > 0)
                            queryRoutingArea = model.CAT_ContractRouting.Where(c => c.ContractID == contractid.Value && c.ContractTermID == contracttermid.Value && c.ContractRoutingTypeID == -(int)SYSVarType.ContractRoutingTypePrice && c.CAT_Routing.RoutingAreaFromID != null && c.CAT_Routing.RoutingAreaToID != null).Select(c => new
                            {
                                c.RoutingID,
                                c.Code,
                                c.RoutingName,
                                RoutingAreaFromID = c.CAT_Routing.RoutingAreaFromID.Value,
                                RoutingAreaToID = c.CAT_Routing.RoutingAreaToID.Value,
                                ContractTermID = c.ContractTermID > 0 ? c.ContractTermID.Value : -1
                            }).OrderByDescending(c => c.ContractTermID);
                        //Use 3 point
                        foreach (var contractRouting in queryRoutingArea)
                        {
                            var queryFrom = model.CAT_RoutingAreaLocation.Where(c => c.RoutingAreaID == contractRouting.RoutingAreaFromID && c.LocationID == cusLocationFrom.ID);
                            var queryTo = model.CAT_RoutingAreaLocation.Where(c => c.RoutingAreaID == contractRouting.RoutingAreaToID && c.LocationID == cusLocationTo.ID);
                            var flagFrom = false;
                            var flagTo = false;
                            if (cusLocationGet != null)
                            {
                                queryFrom = model.CAT_RoutingAreaLocation.Where(c => c.RoutingAreaID == contractRouting.RoutingAreaFromID && (c.LocationID == cusLocationFrom.ID || c.LocationID == cusLocationGet.ID));
                                flagFrom = queryFrom.Count() > 1;
                            }
                            else
                                flagFrom = queryFrom.Count() > 0;
                            if (cusLocationReturn != null)
                            {
                                queryTo = model.CAT_RoutingAreaLocation.Where(c => c.RoutingAreaID == contractRouting.RoutingAreaToID && (c.LocationID == cusLocationTo.ID || c.LocationID == cusLocationReturn.ID));
                                flagTo = queryTo.Count() > 1;
                            }
                            else
                                flagTo = queryTo.Count() > 0;
                            if (flagFrom && flagTo)
                            {
                                var cusRouting = model.CUS_Routing.FirstOrDefault(c => c.CustomerID == cusID && c.RoutingID == contractRouting.RoutingID);
                                if (cusRouting == null)
                                    lstCATRoutingID.Add(contractRouting.RoutingID);
                                if (cusRouting != null)
                                    result = cusRouting.ID;
                                break;
                            }
                        }
                        if (result > 0)
                            return result;
                        //Use 2 point
                        foreach (var contractRouting in queryRoutingArea)
                        {
                            var queryFrom = model.CAT_RoutingAreaLocation.Where(c => c.RoutingAreaID == contractRouting.RoutingAreaFromID && c.LocationID == cusLocationFrom.ID);
                            var queryTo = model.CAT_RoutingAreaLocation.Where(c => c.RoutingAreaID == contractRouting.RoutingAreaToID && c.LocationID == cusLocationTo.ID);

                            if (queryFrom.Count() > 0 &&
                                queryTo.Count() > 0)
                            {
                                var cusRouting = model.CUS_Routing.FirstOrDefault(c => c.CustomerID == cusID && c.RoutingID == contractRouting.RoutingID);
                                if (cusRouting == null)
                                    lstCATRoutingID.Add(contractRouting.RoutingID);
                                if (cusRouting != null)
                                    result = cusRouting.ID;
                                break;
                            }
                        }
                        if (result > 0)
                            return result;
                    }
                }
                else
                {
                    var cusRoutingLocation = model.CUS_Routing.Where(c => c.CustomerID == cusID && c.CAT_Routing.LocationFromID == fromid && c.CAT_Routing.LocationToID == toid).Select(c => new { c.ID }).FirstOrDefault();
                    if (cusRoutingLocation != null)
                    {
                        result = cusRoutingLocation.ID;
                    }
                    else
                    {
                        foreach (var cusRoutingArea in model.CUS_Routing.Where(c => c.CustomerID == cusID && c.CAT_Routing.RoutingAreaFromID != null && c.CAT_Routing.RoutingAreaToID != null)
                            .OrderByDescending(c => c.CAT_Routing.ParentID.HasValue).ThenByDescending(c => c.CAT_Routing.ParentID).Select(c => new
                            {
                                c.ID,
                                RoutingAreaFromID = c.CAT_Routing.RoutingAreaFromID.Value,
                                RoutingAreaToID = c.CAT_Routing.RoutingAreaToID.Value
                            }))
                        {
                            var queryFrom = model.CAT_RoutingAreaLocation.Where(c => c.RoutingAreaID == cusRoutingArea.RoutingAreaFromID && c.LocationID == cusLocationFrom.ID);
                            var queryTo = model.CAT_RoutingAreaLocation.Where(c => c.RoutingAreaID == cusRoutingArea.RoutingAreaToID && c.LocationID == cusLocationTo.ID);
                            var flagFrom = false;
                            var flagTo = false;
                            if (cusLocationGet != null)
                            {
                                queryFrom = model.CAT_RoutingAreaLocation.Where(c => c.RoutingAreaID == cusRoutingArea.RoutingAreaFromID && (c.LocationID == cusLocationFrom.ID || c.LocationID == cusLocationGet.ID));
                                flagFrom = queryFrom.Count() > 1;
                            }
                            else
                                flagFrom = queryFrom.Count() > 0;
                            if (cusLocationReturn != null)
                            {
                                queryTo = model.CAT_RoutingAreaLocation.Where(c => c.RoutingAreaID == cusRoutingArea.RoutingAreaToID && (c.LocationID == cusLocationTo.ID || c.LocationID == cusLocationReturn.ID));
                                flagTo = queryTo.Count() > 1;
                            }
                            else
                                flagTo = queryTo.Count() > 0;
                            if (flagFrom && flagTo)
                            {
                                result = cusRoutingArea.ID;
                                break;
                            }
                            else
                            {
                                queryFrom = model.CAT_RoutingAreaLocation.Where(c => c.RoutingAreaID == cusRoutingArea.RoutingAreaFromID && c.LocationID == cusLocationFrom.ID);
                                queryTo = model.CAT_RoutingAreaLocation.Where(c => c.RoutingAreaID == cusRoutingArea.RoutingAreaToID && c.LocationID == cusLocationTo.ID);

                                if (queryFrom.Count() > 0 &&
                                    queryTo.Count() > 0)
                                {
                                    result = cusRoutingArea.ID;
                                    break;
                                }
                            }
                        }
                    }
                }

                // Tạo CUSRouting
                if (lstCATRoutingID.Count > 0)
                {
                    foreach (var item in lstCATRoutingID)
                    {
                        var routing = model.CAT_Routing.FirstOrDefault(c => c.ID == item);
                        if (routing != null)
                        {
                            CUS_Routing obj = new CUS_Routing();
                            obj.CreatedBy = Account.UserName;
                            obj.CreatedDate = DateTime.Now;
                            obj.RoutingID = routing.ID;
                            obj.Code = routing.Code;
                            obj.RoutingName = routing.RoutingName;
                            obj.CustomerID = cusID;
                            model.CUS_Routing.Add(obj);
                        }
                    }
                    model.SaveChanges();

                    var firstID = lstCATRoutingID.FirstOrDefault();
                    var cusRouting = model.CUS_Routing.FirstOrDefault(c => c.CustomerID == cusID && c.RoutingID == firstID);
                    if (cusRouting != null)
                        result = cusRouting.ID;
                }
            }

            if (result < 1)
            {
                bool flag = false;
                string strTemp = "__temp__";
                var objCATRouting = model.CAT_Routing.FirstOrDefault(c => c.Code == strTemp);
                if (objCATRouting == null)
                {
                    objCATRouting = new CAT_Routing();
                    objCATRouting.CreatedBy = Account.UserName;
                    objCATRouting.CreatedDate = DateTime.Now;
                    objCATRouting.IsAreaLast = false;
                    objCATRouting.EDistance = 0;
                    objCATRouting.EHours = 0;
                    objCATRouting.Code = strTemp;
                    objCATRouting.RoutingName = strTemp;
                    objCATRouting.IsUse = true;
                    objCATRouting.Note = "";
                    objCATRouting.IsLocation = false;
                    model.CAT_Routing.Add(objCATRouting);
                    flag = true;
                }
                var cusRouting = model.CUS_Routing.FirstOrDefault(c => c.RoutingID == objCATRouting.ID);
                if (cusRouting == null)
                {
                    cusRouting = new CUS_Routing();
                    cusRouting.CustomerID = cusID;
                    cusRouting.Code = objCATRouting.Code;
                    cusRouting.RoutingName = objCATRouting.RoutingName;
                    cusRouting.CreatedBy = Account.UserName;
                    cusRouting.CreatedDate = DateTime.Now;
                    cusRouting.CAT_Routing = objCATRouting;
                    model.CUS_Routing.Add(cusRouting);
                    flag = true;
                }
                if (flag)
                    model.SaveChanges();
                result = cusRouting.ID;
            }

            return result;
        }

        public static int OPSMaster_Routing_FindCO(DataEntities model, AccountItem Account, int customerid, int? contractid, int? contracttermid, int fromid, int toid, int? getid = null, int? returnid = null)
        {
            var result = -1;
            if (contractid > 0)
            {
                var queryRoutingLocation = model.CAT_ContractRouting.Where(c => c.ContractID == contractid.Value && c.ContractRoutingTypeID == -(int)SYSVarType.ContractRoutingTypePrice && c.CAT_Routing.LocationFromID == fromid && c.CAT_Routing.LocationToID == toid)
                    .OrderByDescending(c => c.ContractTermID).Select(c => c.RoutingID);
                if (contracttermid > 0)
                    queryRoutingLocation = model.CAT_ContractRouting.Where(c => c.ContractID == contractid.Value && c.ContractTermID == contracttermid.Value && c.ContractRoutingTypeID == -(int)SYSVarType.ContractRoutingTypePrice && c.CAT_Routing.LocationFromID == fromid && c.CAT_Routing.LocationToID == toid)
                    .OrderByDescending(c => c.ContractTermID).Select(c => c.RoutingID);
                var lstRoutingLocationID = queryRoutingLocation.ToList();
                if (lstRoutingLocationID.Count > 0)
                {
                    var objRoutePrice = model.CAT_PriceCOContainer.Where(c => lstRoutingLocationID.Contains(c.CAT_ContractRouting.RoutingID) && c.Price > 0).Select(c => new { c.CAT_ContractRouting.RoutingID }).FirstOrDefault();
                    if (objRoutePrice != null)
                    {
                        result = objRoutePrice.RoutingID;
                        //return objRoutePrice.RoutingID;
                        //var cusRouting = model.CUS_Routing.FirstOrDefault(c => c.CustomerID == customerid && c.RoutingID == objRoutePrice.RoutingID);
                        //if (cusRouting != null)
                        //    result = cusRouting.ID;
                    }
                    else
                    {
                        result = lstRoutingLocationID[0];
                        //int firstid = lstRoutingLocationID[0];
                        //return firstid;
                        //var cusRouting = model.CUS_Routing.FirstOrDefault(c => c.CustomerID == customerid && c.RoutingID == firstid);
                        //if (cusRouting != null)
                        //    result = cusRouting.ID;
                    }
                }
                else
                {
                    var queryRoutingArea = model.CAT_ContractRouting.Where(c => c.ContractID == contractid.Value && c.ContractRoutingTypeID == -(int)SYSVarType.ContractRoutingTypePrice && c.CAT_Routing.RoutingAreaFromID != null && c.CAT_Routing.RoutingAreaToID != null).Select(c => new
                    {
                        c.RoutingID,
                        RoutingAreaFromID = c.CAT_Routing.RoutingAreaFromID.Value,
                        RoutingAreaToID = c.CAT_Routing.RoutingAreaToID.Value,
                        ContractTermID = c.ContractTermID > 0 ? c.ContractTermID.Value : -1
                    }).OrderByDescending(c => c.ContractTermID);
                    if (contracttermid > 0)
                        queryRoutingArea = model.CAT_ContractRouting.Where(c => c.ContractID == contractid.Value && c.ContractTermID == contracttermid.Value && c.ContractRoutingTypeID == -(int)SYSVarType.ContractRoutingTypePrice && c.CAT_Routing.RoutingAreaFromID != null && c.CAT_Routing.RoutingAreaToID != null).Select(c => new
                        {
                            c.RoutingID,
                            RoutingAreaFromID = c.CAT_Routing.RoutingAreaFromID.Value,
                            RoutingAreaToID = c.CAT_Routing.RoutingAreaToID.Value,
                            ContractTermID = c.ContractTermID > 0 ? c.ContractTermID.Value : -1
                        }).OrderByDescending(c => c.ContractTermID);
                    //Use 3 point
                    foreach (var contractRouting in queryRoutingArea)
                    {
                        var queryFrom = model.CAT_RoutingAreaLocation.Where(c => c.RoutingAreaID == contractRouting.RoutingAreaFromID && c.LocationID == fromid);
                        var queryTo = model.CAT_RoutingAreaLocation.Where(c => c.RoutingAreaID == contractRouting.RoutingAreaToID && c.LocationID == toid);
                        var flagFrom = false;
                        var flagTo = false;
                        if (getid != null)
                        {
                            queryFrom = model.CAT_RoutingAreaLocation.Where(c => c.RoutingAreaID == contractRouting.RoutingAreaFromID && (c.LocationID == fromid || c.LocationID == getid));
                            flagFrom = queryFrom.Count() > 1;
                        }
                        else
                            flagFrom = queryFrom.Count() > 0;
                        if (returnid != null)
                        {
                            queryTo = model.CAT_RoutingAreaLocation.Where(c => c.RoutingAreaID == contractRouting.RoutingAreaToID && (c.LocationID == toid || c.LocationID == returnid));
                            flagTo = queryTo.Count() > 1;
                        }
                        else
                            flagTo = queryTo.Count() > 0;
                        if (flagFrom && flagTo)
                        {
                            result = contractRouting.RoutingID;
                            break;
                        }
                    }
                    if (result > 0)
                        return result;

                    //Use 2 point
                    foreach (var contractRouting in queryRoutingArea)
                    {
                        var queryFrom = model.CAT_RoutingAreaLocation.Where(c => c.RoutingAreaID == contractRouting.RoutingAreaFromID && c.LocationID == fromid);
                        var queryTo = model.CAT_RoutingAreaLocation.Where(c => c.RoutingAreaID == contractRouting.RoutingAreaToID && c.LocationID == toid);

                        queryFrom = model.CAT_RoutingAreaLocation.Where(c => c.RoutingAreaID == contractRouting.RoutingAreaFromID && c.LocationID == fromid);
                        queryTo = model.CAT_RoutingAreaLocation.Where(c => c.RoutingAreaID == contractRouting.RoutingAreaToID && c.LocationID == toid);

                        if (queryFrom.Count() > 0 &&
                            queryTo.Count() > 0)
                        {
                            result = contractRouting.RoutingID;
                            break;
                            //var cusRouting = model.CUS_Routing.FirstOrDefault(c => c.CustomerID == customerid && c.RoutingID == contractRouting.RoutingID);
                            //if (cusRouting != null)
                            //    result = cusRouting.ID;
                            //break;
                        }
                    }
                    if (result > 0)
                        return result;
                }
            }
            return result;
        }

        public static void Routing_Update(DataEntities model, AccountItem Account, int locationID)
        {
            var location = model.CAT_Location.FirstOrDefault(c => c.ID == locationID);
            if (location != null)
            {
                // Xóa area có thiết lập detail và chứa location hiện tại
                var lstAreaLocation = model.CAT_RoutingAreaLocation.Where(c => c.LocationID == locationID && c.CAT_RoutingArea.CAT_RoutingAreaDetail.Count > 0);
                // Xóa hết tất cả Area chứa location hiện tại
                foreach (var item in lstAreaLocation)
                    model.CAT_RoutingAreaLocation.Remove(item);
                // Ds area chứa location theo quận
                var lstRoutingAreaDistrict = model.CAT_RoutingAreaDetail.Where(c => c.DistrictID == location.DistrictID).Select(c => c.RoutingAreaID).Distinct().ToList();
                // Ds area chứa location theo tỉnh
                var lstRoutingAreaProvince = model.CAT_RoutingAreaDetail.Where(c => c.ProvinceID == location.ProvinceID && c.DistrictID == null).Select(c => c.RoutingAreaID).Distinct().ToList();
                // Ds area cần thêm location
                var lstRoutingAreaID = new List<int>();
                lstRoutingAreaID.AddRange(lstRoutingAreaDistrict);
                lstRoutingAreaID.AddRange(lstRoutingAreaProvince);
                lstRoutingAreaID = lstRoutingAreaID.Distinct().ToList();
                foreach (var item in lstRoutingAreaID)
                {
                    CAT_RoutingAreaLocation obj = new CAT_RoutingAreaLocation();
                    obj.CreatedBy = Account.UserName;
                    obj.CreatedDate = DateTime.Now;
                    obj.RoutingAreaID = item;
                    obj.LocationID = location.ID;
                    model.CAT_RoutingAreaLocation.Add(obj);
                }
            }
        }


        #endregion

        #region CATLocation
        public static DTOResult CATLocation_ListInCATPartner(DataEntities model, AccountItem Account, int partnerid, string request)
        {
            DTOResult result = new DTOResult();
            var query = model.CAT_PartnerLocation.Where(c => c.PartnerID == partnerid).Select(c => new CATLocation
            {
                ID = c.ID,
                Code = c.PartnerCode,
                Location = c.CAT_Location.Location,
                Address = c.CAT_Location.Address,
                WardID = c.CAT_Location.WardID,
                WardName = c.CAT_Location.WardID > 0 ? c.CAT_Location.CAT_Ward.WardName : "",
                DistrictID = c.CAT_Location.DistrictID,
                DistrictName = c.CAT_Location.CAT_District.DistrictName,
                ProvinceID = c.CAT_Location.ProvinceID,
                ProvinceName = c.CAT_Location.CAT_Province.ProvinceName,
                CountryID = c.CAT_Location.CountryID,
                CountryName = c.CAT_Location.CAT_Country.CountryName,
                GroupOfLocationID = c.CAT_Location.GroupOfLocationID,
                GroupOfLocationCode = c.CAT_Location.GroupOfLocationID > 0 ? c.CAT_Location.CAT_GroupOfLocation.Code : "",
                GroupOfLocationName = c.CAT_Location.GroupOfLocationID > 0 ? c.CAT_Location.CAT_GroupOfLocation.GroupName : "",
                Lat = c.CAT_Location.Lat,
                Lng = c.CAT_Location.Lng,
                Note = c.CAT_Location.Note,
                Note1 = c.CAT_Location.Note1
            }).ToDataSourceResult(CreateRequest(request));
            result.Total = query.Total;
            result.Data = query.Data as IEnumerable<CATLocation>;
            return result;
        }

        public static DTOResult CATLocation_ListNotCATPartner(DataEntities model, AccountItem Account, int partnerid, string request)
        {
            DTOResult result = new DTOResult();
            var lstLocationID = model.CAT_PartnerLocation.Where(c => c.PartnerID == partnerid).Select(c => c.LocationID).ToList();
            var query = model.CAT_Location.Where(c => !lstLocationID.Contains(c.ID)).Select(c => new CATLocation
            {
                ID = c.ID,
                Code = c.Code,
                Location = c.Location,
                Address = c.Address,
                WardID = c.WardID,
                WardName = c.WardID > 0 ? c.CAT_Ward.WardName : "",
                DistrictID = c.DistrictID,
                DistrictName = c.CAT_District.DistrictName,
                ProvinceID = c.ProvinceID,
                ProvinceName = c.CAT_Province.ProvinceName,
                CountryID = c.CountryID,
                CountryName = c.CAT_Country.CountryName,
                GroupOfLocationID = c.GroupOfLocationID,
                GroupOfLocationCode = c.GroupOfLocationID > 0 ? c.CAT_GroupOfLocation.Code : "",
                GroupOfLocationName = c.GroupOfLocationID > 0 ? c.CAT_GroupOfLocation.GroupName : "",
                Lat = c.Lat,
                Lng = c.Lng,
                Note = c.Note,
                Note1 = c.Note1
            }).ToDataSourceResult(CreateRequest(request));
            result.Total = query.Total;
            result.Data = query.Data as IEnumerable<CATLocation>;
            return result;
        }

        public static DTOResult CATLocation_ListInCUSPartner(DataEntities model, AccountItem Account, int partnerid, string request)
        {
            DTOResult result = new DTOResult();
            var query = model.CUS_Location.Where(c => c.CusPartID == partnerid).Select(c => new CUSLocation
            {
                ID = c.ID,
                LocationID = c.LocationID,
                Code = c.Code,
                LocationName = c.LocationName,
                Address = c.CAT_Location.Address,
                WardID = c.CAT_Location.WardID,
                WardName = c.CAT_Location.WardID > 0 ? c.CAT_Location.CAT_Ward.WardName : "",
                DistrictID = c.CAT_Location.DistrictID,
                DistrictName = c.CAT_Location.CAT_District.DistrictName,
                ProvinceID = c.CAT_Location.ProvinceID,
                ProvinceName = c.CAT_Location.CAT_Province.ProvinceName,
                CountryID = c.CAT_Location.CountryID,
                CountryName = c.CAT_Location.CAT_Country.CountryName,
                GroupOfLocationID = c.CAT_Location.GroupOfLocationID,
                GroupOfLocationCode = c.CAT_Location.GroupOfLocationID > 0 ? c.CAT_Location.CAT_GroupOfLocation.Code : "",
                GroupOfLocationName = c.CAT_Location.GroupOfLocationID > 0 ? c.CAT_Location.CAT_GroupOfLocation.GroupName : "",
                Lat = c.CAT_Location.Lat,
                Lng = c.CAT_Location.Lng,
                Note = c.CAT_Location.Note,
                Note1 = c.CAT_Location.Note1
            }).ToDataSourceResult(CreateRequest(request));
            result.Total = query.Total;
            result.Data = query.Data as IEnumerable<CUSLocation>;
            return result;
        }

        public static DTOResult CATLocation_ListNotInCUSPartner(DataEntities model, AccountItem Account, int partnerid, string request)
        {
            DTOResult result = new DTOResult();

            var query = model.CUS_Location.Where(c => c.CusPartID == partnerid).Select(c => new CUSLocation
            {
                ID = c.ID,
                LocationID = c.LocationID,
                Code = c.Code,
                LocationName = c.LocationName,
                Address = c.CAT_Location.Address,
                WardID = c.CAT_Location.WardID,
                WardName = c.CAT_Location.WardID > 0 ? c.CAT_Location.CAT_Ward.WardName : "",
                DistrictID = c.CAT_Location.DistrictID,
                DistrictName = c.CAT_Location.CAT_District.DistrictName,
                ProvinceID = c.CAT_Location.ProvinceID,
                ProvinceName = c.CAT_Location.CAT_Province.ProvinceName,
                CountryID = c.CAT_Location.CountryID,
                CountryName = c.CAT_Location.CAT_Country.CountryName,
                GroupOfLocationID = c.CAT_Location.GroupOfLocationID,
                GroupOfLocationCode = c.CAT_Location.GroupOfLocationID > 0 ? c.CAT_Location.CAT_GroupOfLocation.Code : "",
                GroupOfLocationName = c.CAT_Location.GroupOfLocationID > 0 ? c.CAT_Location.CAT_GroupOfLocation.GroupName : "",
                Lat = c.CAT_Location.Lat,
                Lng = c.CAT_Location.Lng,
                Note = c.CAT_Location.Note,
                Note1 = c.CAT_Location.Note1
            }).ToDataSourceResult(CreateRequest(request));
            result.Total = query.Total;
            result.Data = query.Data as IEnumerable<CUSLocation>;
            return result;
        }


        public static CAT_Location CATLocation_Save(DataEntities model, AccountItem Account, CATLocation item, bool checkUseMore)
        {
            if (string.IsNullOrEmpty(item.Code))
                throw FaultHelper.BusinessFault(null, null, "Vui lòng nhập mã");
            item.Code = item.Code.Trim();
            if (model.CAT_Location.Where(c => c.ID != item.ID && c.Code == item.Code).Count() > 0)
                throw FaultHelper.BusinessFault(null, null, "Mã đã sử dụng");

            bool flag = true;
            if (checkUseMore && item.ID > 0)
            {
                if (model.CAT_PartnerLocation.Where(c => c.LocationID == item.ID).Count() > 0)
                    flag = false;
            }
            if (flag)
            {
                var obj = model.CAT_Location.FirstOrDefault(c => c.ID == item.ID);
                if (obj == null)
                {
                    obj = new CAT_Location();
                    obj.CreatedBy = Account.UserName;
                    obj.CreatedDate = DateTime.Now;
                }

                obj.Code = item.Code;
                obj.Location = item.Location;
                obj.Address = !string.IsNullOrEmpty(item.Address) ? item.Address : string.Empty;
                obj.WardID = item.WardID;
                if (obj.WardID < 1) obj.WardID = null;
                obj.DistrictID = item.DistrictID;
                obj.ProvinceID = item.ProvinceID;
                obj.CountryID = item.CountryID;
                obj.Lat = item.Lat;
                obj.Lng = item.Lng;
                obj.Note = item.Note;
                obj.Note1 = item.Note1;
                obj.GroupOfLocationID = item.GroupOfLocationID > 0 ? item.GroupOfLocationID : null;
                obj.LoadTimeCO = item.LoadTimeCO;
                obj.LoadTimeDI = item.LoadTimeDI;
                obj.UnLoadTimeCO = item.UnLoadTimeCO;
                obj.UnLoadTimeDI = item.UnLoadTimeDI;
                obj.EconomicZone = item.EconomicZone;
                if (obj.ID < 1)
                    model.CAT_Location.Add(obj);
                model.SaveChanges();

                if (item.IsAllCarrier)
                {
                    int iCarrier = -(int)SYSVarType.TypeOfPartnerCarrier;
                    List<int> lstCarrierID = model.CAT_Partner.Where(c => c.TypeOfPartnerID == iCarrier).Select(c => c.ID).ToList();

                    foreach (var carrierID in lstCarrierID)
                    {
                        var objPartnerLocation = model.CAT_PartnerLocation.FirstOrDefault(c => c.PartnerID == carrierID && c.LocationID == obj.ID);
                        if (objPartnerLocation == null)
                        {
                            objPartnerLocation = new CAT_PartnerLocation();
                            objPartnerLocation.CreatedBy = Account.UserName;
                            objPartnerLocation.CreatedDate = DateTime.Now;
                            objPartnerLocation.PartnerID = carrierID;
                            objPartnerLocation.LocationID = obj.ID;
                            model.CAT_PartnerLocation.Add(objPartnerLocation);
                        }
                    }
                    model.SaveChanges();
                }
                return obj;
            }
            else
                return model.CAT_Location.FirstOrDefault(c => c.ID == item.ID);
        }
        public static void CATLocation_Delete(DataEntities model, AccountItem Account, int locationID)
        {
            if (model.OPS_COTOLocation.Count(c => c.LocationID > 0 && c.LocationID == locationID) > 0 || model.OPS_DITOLocation.Count(c => c.LocationID > 0 && c.LocationID == locationID) > 0)
                throw new Exception("Địa điểm đã tạo chuyến, không thể xóa");
            var obj = model.CAT_Location.FirstOrDefault(c => c.ID == locationID);
            if (obj == null) throw FaultHelper.BusinessFault(null, null, "Địa điểm không tồn tại");

            foreach (var detail in model.CAT_PartnerLocation.Where(c => c.LocationID == obj.ID))
                model.CAT_PartnerLocation.Remove(detail);
            foreach (var detail in model.CUS_Location.Where(c => c.LocationID == obj.ID))
                model.CUS_Location.Remove(detail);
            foreach (var detail in model.CAT_RoutingAreaLocation.Where(c => c.LocationID == obj.ID))
                model.CAT_RoutingAreaLocation.Remove(detail);

            model.CAT_Location.Remove(obj);
            model.SaveChanges();

        }

        public static List<CATRoutingArea> CATLocation_ListRoutingArea(DataEntities model, AccountItem Account, int locationid)
        {
            var result = new List<CATRoutingArea>();
            result = model.CAT_RoutingAreaLocation.Where(c => c.LocationID == locationid).Select(c => new CATRoutingArea
            {
                ID = c.CAT_RoutingArea.ID,
                Code = c.CAT_RoutingArea.Code,
                AreaName = c.CAT_RoutingArea.AreaName
            }).ToList();
            return result;
        }

        public static DTOResult CATLocation_ListRoutingAreaNotIn(DataEntities model, AccountItem Account, int locationid, string request)
        {
            DTOResult result = new DTOResult();
            var query = model.CAT_RoutingArea.Select(c => new CATRoutingArea
            {
                ID = c.ID,
                Code = c.Code,
                AreaName = c.AreaName
            }).ToDataSourceResult(CreateRequest(request));
            result.Total = query.Total;
            result.Data = query.Data as IEnumerable<CATRoutingArea>;
            return result;
        }

        public static void CATLocation_AddRoutingArea(DataEntities model, AccountItem Account, List<int> lstAreaID, List<int> lstLocationID)
        {
            if (lstAreaID.Count > 0 && lstLocationID.Count > 0)
            {
                var lstAreaUse = model.CAT_RoutingAreaLocation.Where(c => lstAreaID.Contains(c.RoutingAreaID)).Select(c => new { c.RoutingAreaID, c.LocationID }).ToList();

                foreach (var areaid in lstAreaID.Distinct())
                {
                    foreach (var locationid in lstLocationID.Distinct())
                    {
                        if (lstAreaUse.Where(c => c.RoutingAreaID == areaid && c.LocationID == locationid).Count() == 0)
                        {
                            var obj = new CAT_RoutingAreaLocation();
                            obj.RoutingAreaID = areaid;
                            obj.LocationID = locationid;
                            obj.CreatedBy = Account.UserName;
                            obj.CreatedDate = DateTime.Now;
                            model.CAT_RoutingAreaLocation.Add(obj);
                        }
                    }
                }
                model.SaveChanges();
            }
        }

        public static void CATLocation_RemoveRoutingArea(DataEntities model, AccountItem Account, List<int> lstAreaID, List<int> lstLocationID)
        {
            if (lstAreaID.Count > 0 && lstLocationID.Count > 0)
            {
                foreach (var item in model.CAT_RoutingAreaLocation.Where(c => lstAreaID.Contains(c.RoutingAreaID) && lstLocationID.Contains(c.LocationID)))
                    model.CAT_RoutingAreaLocation.Remove(item);
                model.SaveChanges();
            }
        }

        private static DataSourceRequest CreateRequest(string strRequest)
        {
            var result = new DataSourceRequest();

            try
            {
                var request = (DTOCustomRequest)JsonConvert.DeserializeObject<DTOCustomRequest>(strRequest);

                result.Page = Convert.ToInt32(request.Page);
                result.PageSize = Convert.ToInt32(request.PageSize);

                //"FirstName~contains~'a'~and~LastName~contains~'b'"
                var filters = new List<Kendo.Mvc.IFilterDescriptor>();
                if (request.Filter.Contains("~and~"))
                {
                    var strsAnd = request.Filter.Split(new string[] { "~and~" }, StringSplitOptions.None);
                    var fand = new Kendo.Mvc.CompositeFilterDescriptor();
                    fand.LogicalOperator = Kendo.Mvc.FilterCompositionLogicalOperator.And;
                    fand.FilterDescriptors = new Kendo.Mvc.Infrastructure.Implementation.FilterDescriptorCollection();
                    foreach (string strand in strsAnd)
                    {
                        var strs = strand.Split('~');
                        if (strs.Length > 2)
                        {
                            var f = new Kendo.Mvc.FilterDescriptor();
                            f.Member = strs[0].StartsWith("(") ? strs[0].Substring(1, strs[0].Length - 1) : strs[0];
                            switch (strs[1])
                            {
                                case "eq": f.Operator = Kendo.Mvc.FilterOperator.IsEqualTo; break;
                                case "neq": f.Operator = Kendo.Mvc.FilterOperator.IsNotEqualTo; break;
                                case "gt": f.Operator = Kendo.Mvc.FilterOperator.IsGreaterThan; break;
                                case "gte": f.Operator = Kendo.Mvc.FilterOperator.IsGreaterThanOrEqualTo; break;
                                case "lt": f.Operator = Kendo.Mvc.FilterOperator.IsLessThan; break;
                                case "lte": f.Operator = Kendo.Mvc.FilterOperator.IsLessThanOrEqualTo; break;
                                case "contains": f.Operator = Kendo.Mvc.FilterOperator.Contains; break;
                                case "startswith": f.Operator = Kendo.Mvc.FilterOperator.StartsWith; break;
                                case "endswith": f.Operator = Kendo.Mvc.FilterOperator.EndsWith; break;
                            }
                            string strVal = strs[2].EndsWith(")") ? strs[2].Substring(0, strs[2].Length - 1) : strs[2];
                            f.Value = strVal;
                            if (strVal.StartsWith("'"))
                                f.Value = strVal.Substring(1, strVal.Length - 2);
                            else
                            {
                                if (strVal.Contains("datetime"))
                                {
                                    var arr1 = strVal.Split('\'');
                                    var arr2 = arr1[1].Replace('T', '-').Split(new[] { '-' }, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
                                    DateTime dt = new DateTime(arr2[0], arr2[1], arr2[2], arr2[3], arr2[4], arr2[5]);
                                    f.Value = dt;
                                }
                            }
                            fand.FilterDescriptors.Add(f);
                        }
                    }
                    filters.Add(fand);
                }

                //"FirstName~eq~'a'~or~LastName~eq~'b'"
                if (request.Filter.Contains("~or~"))
                {
                    var strsOr = request.Filter.Split(new string[] { "~or~" }, StringSplitOptions.None);
                    var fOr = new Kendo.Mvc.CompositeFilterDescriptor();
                    fOr.LogicalOperator = Kendo.Mvc.FilterCompositionLogicalOperator.Or;
                    fOr.FilterDescriptors = new Kendo.Mvc.Infrastructure.Implementation.FilterDescriptorCollection();
                    foreach (string stror in strsOr)
                    {
                        var strs = stror.Split('~');
                        if (strs.Length > 2)
                        {
                            var f = new Kendo.Mvc.FilterDescriptor();
                            f.Member = strs[0].StartsWith("(") ? strs[0].Substring(1, strs[0].Length - 1) : strs[0];
                            switch (strs[1])
                            {
                                case "eq": f.Operator = Kendo.Mvc.FilterOperator.IsEqualTo; break;
                                case "neq": f.Operator = Kendo.Mvc.FilterOperator.IsNotEqualTo; break;
                                case "gt": f.Operator = Kendo.Mvc.FilterOperator.IsGreaterThan; break;
                                case "gte": f.Operator = Kendo.Mvc.FilterOperator.IsGreaterThanOrEqualTo; break;
                                case "lt": f.Operator = Kendo.Mvc.FilterOperator.IsLessThan; break;
                                case "lte": f.Operator = Kendo.Mvc.FilterOperator.IsLessThanOrEqualTo; break;
                                case "contains": f.Operator = Kendo.Mvc.FilterOperator.Contains; break;
                                case "startswith": f.Operator = Kendo.Mvc.FilterOperator.StartsWith; break;
                                case "endswith": f.Operator = Kendo.Mvc.FilterOperator.EndsWith; break;
                            }
                            string strVal = strs[2].EndsWith(")") ? strs[2].Substring(0, strs[2].Length - 1) : strs[2];
                            f.Value = strVal;
                            if (strVal.StartsWith("'"))
                                f.Value = strVal.Substring(1, strVal.Length - 2);
                            else
                            {
                                if (strVal.Contains("datetime"))
                                {
                                    var arr1 = strVal.Split('\'');
                                    var arr2 = arr1[1].Replace('T', '-').Split(new[] { '-' }, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
                                    DateTime dt = new DateTime(arr2[0], arr2[1], arr2[2], arr2[3], arr2[4], arr2[5]);
                                    f.Value = dt;
                                }
                            }
                            fOr.FilterDescriptors.Add(f);
                        }
                    }
                    filters.Add(fOr);
                }

                if (!request.Filter.Contains("~or~") && !request.Filter.Contains("~and~"))
                {
                    if (!string.IsNullOrEmpty(request.Filter))
                    {
                        var strs = request.Filter.Split('~');
                        if (strs.Length > 2)
                        {
                            var f = new Kendo.Mvc.FilterDescriptor();
                            f.Member = strs[0].StartsWith("(") ? strs[0].Substring(1, strs[0].Length - 1) : strs[0];
                            switch (strs[1])
                            {
                                case "eq": f.Operator = Kendo.Mvc.FilterOperator.IsEqualTo; break;
                                case "neq": f.Operator = Kendo.Mvc.FilterOperator.IsNotEqualTo; break;
                                case "gt": f.Operator = Kendo.Mvc.FilterOperator.IsGreaterThan; break;
                                case "gte": f.Operator = Kendo.Mvc.FilterOperator.IsGreaterThanOrEqualTo; break;
                                case "lt": f.Operator = Kendo.Mvc.FilterOperator.IsLessThan; break;
                                case "lte": f.Operator = Kendo.Mvc.FilterOperator.IsLessThanOrEqualTo; break;
                                case "contains": f.Operator = Kendo.Mvc.FilterOperator.Contains; break;
                                case "startswith": f.Operator = Kendo.Mvc.FilterOperator.StartsWith; break;
                                case "endswith": f.Operator = Kendo.Mvc.FilterOperator.EndsWith; break;
                            }
                            string strVal = strs[2].EndsWith(")") ? strs[2].Substring(0, strs[2].Length - 1) : strs[2];
                            f.Value = strVal;
                            if (strVal.StartsWith("'"))
                                f.Value = strVal.Substring(1, strVal.Length - 2);
                            else
                            {
                                if (strVal.Contains("datetime"))
                                {
                                    var arr1 = strVal.Split('\'');
                                    var arr2 = arr1[1].Replace('T', '-').Split(new[] { '-' }, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
                                    DateTime dt = new DateTime(arr2[0], arr2[1], arr2[2], arr2[3], arr2[4], arr2[5]);
                                    f.Value = dt;
                                }
                            }
                            filters.Add(f);
                        }
                    }
                }

                result.Filters = filters;

                //FirstName-asc~LastName-asc
                var sorts = new List<Kendo.Mvc.SortDescriptor>();
                var strsSort = request.Sort.Split('~');
                foreach (string strsort in strsSort)
                {
                    var strs = strsort.Split('-');
                    if (strs.Length > 1)
                    {
                        var s = new Kendo.Mvc.SortDescriptor();
                        s.Member = strs[0];
                        if (strs[1] == "asc")
                            s.SortDirection = System.ComponentModel.ListSortDirection.Ascending;
                        else
                            s.SortDirection = System.ComponentModel.ListSortDirection.Descending;
                        sorts.Add(s);
                    }
                }
                result.Sorts = sorts;
            }
            catch
            {
                result = new DataSourceRequest();
            }

            return result;
        }

        public static void CATLocation_ExcelImport(DataEntities model, AccountItem Account, List<CATLocationImport> lstLocation)
        {
            foreach (var item in lstLocation.Where(c => c.ExcelSuccess == true))
            {
                var obj = model.CAT_Location.FirstOrDefault(c => c.ID == item.ID);
                if (obj != null)
                {
                    obj.ModifiedBy = Account.UserName;
                    obj.ModifiedDate = DateTime.Now;
                }
                else
                {
                    obj = new CAT_Location();

                    obj.CreatedBy = Account.UserName;
                    obj.CreatedDate = DateTime.Now;
                    obj.Code = item.Code.Trim();
                    model.CAT_Location.Add(obj);
                }
                obj.Location = item.Location;
                obj.Address = !string.IsNullOrEmpty(item.Address) ? item.Address : string.Empty;
                obj.CountryID = item.CountryID;
                obj.ProvinceID = item.ProvinceID;
                obj.DistrictID = item.DistrictID;
                obj.WardID = item.WardID;
                if (obj.WardID < 1) obj.WardID = null;
                obj.Lat = item.Lat;
                obj.Lng = item.Lng;
                obj.Note = item.Note;
                obj.Note1 = item.Note1;
                obj.EconomicZone = item.EconomicZone;
                model.SaveChanges();

                Routing_Update(model, Account, obj.ID);
                model.SaveChanges();
            }
        }

        //chuyen tu BLMON sang
        public static int CATLocation_DIMonitorMaster_PartnerLocationSave(DataEntities model, AccountItem Account, DTOCUSPartnerLocation item, int cusPartID)
        {
            #region Kiểm tra CUS_PartnerID
            var objCheck = model.CUS_Partner.FirstOrDefault(c => c.ID == cusPartID);
            if (objCheck == null)
                throw FaultHelper.BusinessFault(null, null, "Dữ liệu không tồn tại!");
            #endregion

            #region Kiểm tra trùng Code và LocationID
            if (!string.IsNullOrEmpty(item.Code))
                item.Code = item.Code.Trim();
            var objCheckLocation = model.CUS_Location.FirstOrDefault(c => c.ID != item.ID && c.CusPartID == cusPartID && c.Code == item.Code);
            if (objCheckLocation != null)
                throw FaultHelper.BusinessFault(null, null, "Mã đã sử dụng!");
            #endregion

            #region Lưu thông tin CAT_Location
            var obj = model.CAT_Location.FirstOrDefault(c => c.ID == item.LocationID);
            if (obj == null)
            {
                obj = new CAT_Location();
                obj.CreatedBy = Account.UserName;
                obj.CreatedDate = DateTime.Now;
                item.IsEditable = true;
            }
            else
            {
                obj.ModifiedBy = Account.UserName;
                obj.ModifiedDate = DateTime.Now;
            }
            if (item.IsEditable)
            {
                if (model.CAT_Location.FirstOrDefault(c => c.ID != item.LocationID && c.Code == item.Code) != null)
                    throw FaultHelper.BusinessFault(null, null, "Mã đã sử dụng");
                obj.Code = item.Code;
                obj.Location = !string.IsNullOrEmpty(item.Location) ? item.Location : string.Empty;
                obj.Address = !string.IsNullOrEmpty(item.Address) ? item.Address : string.Empty;
                obj.WardID = item.WardID;
                obj.DistrictID = item.DistrictID;
                obj.ProvinceID = item.ProvinceID;
                obj.CountryID = item.CountryID;
                obj.Lat = item.Lat;
                obj.Lng = item.Lng;
                obj.GroupOfLocationID = item.GroupOfLocationID > 0 ? item.GroupOfLocationID : null;
            }

            if (obj.ID < 1)
                model.CAT_Location.Add(obj);

            model.SaveChanges();
            #endregion

            #region Lưu CAT_PartnerLocation
            var objPart = model.CAT_PartnerLocation.FirstOrDefault(c => c.LocationID == obj.ID && c.PartnerID == objCheck.PartnerID);
            if (objPart == null)
            {
                objPart = new CAT_PartnerLocation();
                objPart.CreatedBy = Account.UserName;
                objPart.CreatedDate = DateTime.Now;
                objPart.LocationID = obj.ID;
                objPart.PartnerID = objCheck.PartnerID;
                objPart.PartnerCode = item.Code;
                model.CAT_PartnerLocation.Add(objPart);
            }
            #endregion

            #region Lưu thông tin CUS_Location
            var objCus = model.CUS_Location.FirstOrDefault(c => c.ID == item.ID);
            if (objCus == null)
            {
                objCus = new CUS_Location();
                objCus.CreatedBy = Account.UserName;
                objCus.CreatedDate = DateTime.Now;
                objCus.CusPartID = cusPartID;
                objCus.CustomerID = objCheck.CustomerID;
                objCus.LocationID = obj.ID;
            }
            else
            {
                objCus.ModifiedBy = Account.UserName;
                objCus.ModifiedDate = DateTime.Now;
            }
            objCus.Code = item.Code;
            if (string.IsNullOrEmpty(item.Location))
                item.Location = string.Empty;

            objCus.LocationName = item.Location;
            if (objCus.ID < 1)
            {
                // Chỉ thêm mới khi Code != null
                if (!string.IsNullOrEmpty(item.Code))
                    model.CUS_Location.Add(objCus);
            }
            else
            {
                // Xóa khi Code == null
                //if (string.IsNullOrEmpty(item.Code))
                //if (model.ORD_DIOrder.FirstOrDefault(c => c.CustomerID == objCheck.CustomerID && c.LocationID == objCus.ID) == null)
                //    model.CUS_Location.Remove(objCus);
                //else
                //    throw FaultHelper.BusinessFault("", "", "Vui lòng xóa Đơn hàng trước");
            }
            #endregion

            Routing_Update(model, Account, obj.ID);
            model.SaveChanges();

            return objCus.ID;
        }


        #endregion

        #region CATPartner
        public static CAT_Partner CATPartner_Save(DataEntities model, AccountItem Account, CATPartner item, SYSVarType typeOfPartner)
        {
            int typeid = -(int)typeOfPartner;

            if (string.IsNullOrEmpty(item.Code))
                throw FaultHelper.BusinessFault(null, null, "Vui lòng nhập mã");
            if (typeOfPartner != SYSVarType.TypeOfPartnerSeaPort && typeOfPartner != SYSVarType.TypeOfPartnerCarrier && typeOfPartner != SYSVarType.TypeOfPartnerDistributor && typeOfPartner != SYSVarType.TypeOfPartnerStation)
                throw FaultHelper.BusinessFault(null, null, "Phân loại không đúng");
            item.Code = item.Code.Trim();
            if (model.CAT_Partner.Where(c => c.ID != item.ID && c.Code == item.Code && c.TypeOfPartnerID == typeid).Count() > 0)
                throw FaultHelper.BusinessFault(null, null, "Mã đã sử dụng");

            var obj = model.CAT_Partner.FirstOrDefault(c => c.ID == item.ID);
            if (obj == null)
            {
                obj = new CAT_Partner();
                obj.TypeOfPartnerID = typeid;
                obj.CreatedBy = Account.UserName;
                obj.CreatedDate = DateTime.Now;
            }
            else
            {
                obj.ModifiedBy = Account.UserName;
                obj.ModifiedDate = DateTime.Now;
            }
            obj.Code = item.Code;
            if (item.GroupOfPartnerID > 0)
                obj.GroupOfPartnerID = item.GroupOfPartnerID;
            else
                obj.GroupOfPartnerID = null;
            obj.PartnerName = !string.IsNullOrEmpty(item.PartnerName) ? item.PartnerName.Trim() : string.Empty;
            obj.Address = !string.IsNullOrEmpty(item.Address) ? item.Address.Trim() : string.Empty;
            obj.WardID = item.WardID;
            if (obj.WardID < 1) obj.WardID = null;
            obj.DistrictID = item.DistrictID;
            obj.ProvinceID = item.ProvinceID;
            obj.CountryID = item.CountryID;
            obj.TelNo = item.TelNo;
            obj.Fax = item.Fax;
            obj.Email = item.Email;
            obj.BillingName = item.BillingName;
            obj.BillingAddress = item.BillingAddress;
            obj.TaxCode = item.TaxCode;
            obj.Note = item.Note;
            if (obj.ID < 1)
                model.CAT_Partner.Add(obj);
            model.SaveChanges();

            if (typeOfPartner == SYSVarType.TypeOfPartnerSeaPort || typeOfPartner == SYSVarType.TypeOfPartnerCarrier)
                ApplyPartnerForAllCus(model, Account, new List<CAT_Partner> { obj });

            return obj;
        }
        public static void CATPartner_Delete(DataEntities model, AccountItem Account, int id)
        {
            var obj = model.CAT_Partner.FirstOrDefault(c => c.ID == id);
            if (obj == null)
                throw FaultHelper.BusinessFault(null, null, "dữ liệu không tồn tại");
            foreach (var detail in model.CUS_Partner.Where(c => c.PartnerID == obj.ID))
                model.CUS_Partner.Remove(detail);
            foreach (var detail in model.CAT_PartnerLocation.Where(c => c.PartnerID == obj.ID))
                model.CAT_PartnerLocation.Remove(detail);
            model.CAT_Partner.Remove(obj);
            model.SaveChanges();

        }
        public static CAT_Partner CATPartner_WithListLocationSave(DataEntities model, AccountItem Account, CATPartner item, SYSVarType typeOfPartner, List<CATLocation> lst)
        {
            var result = CATPartner_Save(model, Account, item, typeOfPartner);
            var locationtypeid = -1;
            if (result.GroupOfPartnerID > 0)
            {
                var locationtype = model.CAT_GroupOfLocation.Where(c => c.GroupOfPartnerID == result.GroupOfPartnerID).Select(c => new { c.ID }).FirstOrDefault();
                if (locationtype != null)
                    locationtypeid = locationtype.ID;
            }

            if (lst.Count > 0)
            {
                foreach (var detail in lst)
                {
                    var objDetail = CATLocation_Save(model, Account, detail, true);
                    if (objDetail.GroupOfLocationID == null && locationtypeid > 0)
                        objDetail.GroupOfLocationID = locationtypeid;
                }
                model.SaveChanges();
            }

            return result;
        }

        //import partner: luu cat partner -partner location, location
        public static void CATPartner_Import(DataEntities model, AccountItem Account, List<DTOCATPartnerImport> lst, int TypeOfPartnerID)
        {

            if (lst != null)
            {
                foreach (var partner in lst.Where(c => c.ExcelSuccess))
                {
                    if (string.IsNullOrEmpty(partner.Code))
                        throw FaultHelper.BusinessFault(null, null, "Mã không được trống!");

                    var catPartner = model.CAT_Partner.FirstOrDefault(c => c.ID == partner.ID);
                    if (catPartner == null)
                    {
                        catPartner = new CAT_Partner();
                        catPartner.CreatedBy = Account.UserName;
                        catPartner.CreatedDate = DateTime.Now;
                        catPartner.TypeOfPartnerID = TypeOfPartnerID;
                        model.CAT_Partner.Add(catPartner);
                    }
                    else
                    {
                        catPartner.ModifiedBy = Account.UserName;
                        catPartner.ModifiedDate = DateTime.Now;
                    }
                    catPartner.Code = partner.Code;
                    catPartner.PartnerName = string.IsNullOrEmpty(partner.PartnerName) ? string.Empty : partner.PartnerName;
                    catPartner.CountryID = partner.CountryID;
                    catPartner.ProvinceID = partner.ProvinceID;
                    catPartner.DistrictID = partner.DistrictID;
                    catPartner.Address = string.IsNullOrEmpty(partner.Address) ? string.Empty : partner.Address;
                    catPartner.TelNo = partner.TelNo;
                    catPartner.Fax = partner.Fax;
                    catPartner.Email = partner.Email;
                    catPartner.GroupOfPartnerID = partner.GroupOfPartnerID;
                    if (partner.lstLocation != null)
                    {
                        foreach (var location in partner.lstLocation.Where(c => c.IsSuccess && c.ProvinceID.HasValue && c.DistrictID.HasValue))
                        {
                            if (string.IsNullOrEmpty(location.Code))
                                throw FaultHelper.BusinessFault(null, null, "Mã không được trống!");
                            if (string.IsNullOrEmpty(location.PartnerCode))
                                throw FaultHelper.BusinessFault(null, null, "Mã địa chỉ của " + location.Code + " không được trống!");

                            var catLocation = model.CAT_Location.FirstOrDefault(c => c.ID == location.ID);
                            if (catLocation == null)
                            {
                                catLocation = new CAT_Location();
                                catLocation.CreatedBy = Account.UserName;
                                catLocation.CreatedDate = DateTime.Now;
                                model.CAT_Location.Add(catLocation);
                                // Lưu PartnerLocation
                                CAT_PartnerLocation partLocation = new CAT_PartnerLocation();
                                partLocation.CreatedBy = Account.UserName;
                                partLocation.CreatedDate = DateTime.Now;
                                partLocation.CAT_Partner = catPartner;
                                partLocation.CAT_Location = catLocation;
                                partLocation.PartnerCode = location.PartnerCode;
                                model.CAT_PartnerLocation.Add(partLocation);
                            }
                            else
                            {
                                catLocation.ModifiedBy = Account.UserName;
                                catLocation.ModifiedDate = DateTime.Now;
                                // Lưu PartnerLocation
                                var partLocation = model.CAT_PartnerLocation.FirstOrDefault(c => c.LocationID == catLocation.ID);
                                if (partLocation != null)
                                {
                                    partLocation.ModifiedBy = Account.UserName;
                                    partLocation.ModifiedDate = DateTime.Now;
                                    partLocation.CAT_Partner = catPartner;
                                    partLocation.PartnerCode = location.PartnerCode;
                                }
                            }
                            catLocation.EconomicZone = location.EconomicZone;
                            catLocation.Code = location.Code;
                            catLocation.Location = string.IsNullOrEmpty(location.LocationName) ? string.Empty : location.LocationName;
                            catLocation.CountryID = location.CountryID;
                            catLocation.ProvinceID = location.ProvinceID.Value;
                            catLocation.DistrictID = location.DistrictID.Value;
                            catLocation.Address = string.IsNullOrEmpty(location.Address) ? string.Empty : location.Address;
                            catLocation.Lng = location.Lng;
                            catLocation.Lat = location.Lat;

                        }
                    }
                }
            }
            model.SaveChanges();

        }

        //phan bổ mã cho partner
        public static void PartnerLocation_Import(DataEntities model, AccountItem Account, DTOCUSPartnerLocationImport item, int TypeOfPartnerID)
        {
            if (item.lstCusPartner != null)
            {
                if (item.lstCusLocation == null)
                    item.lstCusLocation = new List<DTOCustomerLocationImport>();
                // Ktra Partner trước
                var lstPartner = item.lstCusPartner.Where(c => c.IsSuccess);
                if (lstPartner.Count() > 0)
                {
                    // Xử lý theo từng khách hàng
                    var lstCustomer = lstPartner.GroupBy(c => c.CustomerID);
                    foreach (var customer in lstCustomer)
                    {
                        // Danh sách partner cũ của khách hàng
                        var lstCusPartner = model.CUS_Partner.Where(c => c.CustomerID == customer.Key && c.CAT_Partner.TypeOfPartnerID == TypeOfPartnerID);
                        foreach (var cuspart in lstCusPartner)
                        {
                            var partner = customer.FirstOrDefault(c => c.PartnerID == cuspart.PartnerID);
                            // Ko tồn tại or Code = null or Code = Empty => Xóa 
                            if (partner == null || string.IsNullOrEmpty(partner.Code))
                            {
                                // Ktra trước khi xóa partner
                                Partner_Delete(model, cuspart, item.lstCusLocation);
                                model.CUS_Partner.Remove(cuspart);
                            }
                            else
                            {
                                // Nếu partner tồn tại
                                cuspart.ModifiedBy = Account.UserName;
                                cuspart.ModifiedDate = DateTime.Now;
                                cuspart.PartnerCode = partner.Code;
                                // Lưu cuslocation
                                var lstLocation = item.lstCusLocation.Where(c => c.IsSuccess && c.PartnerID == partner.PartnerID && c.CustomerID == customer.Key);
                                var lstLocationError = item.lstCusLocation.Where(c => !c.IsSuccess && c.PartnerID == partner.PartnerID && c.CustomerID == customer.Key);
                                var lstCusLocation = model.CUS_Location.Where(c => c.CustomerID == customer.Key && c.CusPartID.HasValue && c.CUS_Partner.PartnerID == partner.PartnerID);
                                foreach (var cuslocation in lstCusLocation)
                                {
                                    var location = lstLocation.FirstOrDefault(c => c.LocationID == cuslocation.LocationID);
                                    // Ko tồn tại or Code = null or Code = Empty => Xóa 
                                    if (location == null || string.IsNullOrEmpty(location.Code))
                                    {
                                        Location_Delete(model, cuslocation);
                                    }
                                    else
                                    {
                                        cuslocation.ModifiedBy = Account.UserName;
                                        cuslocation.ModifiedDate = DateTime.Now;
                                        cuslocation.Code = location.Code;
                                    }
                                }
                                // Ds location cần thêm mới
                                foreach (var location in lstLocation.Where(c => !string.IsNullOrEmpty(c.Code)))
                                {
                                    if (lstCusLocation.Count(c => c.LocationID == location.LocationID) == 0)
                                    {
                                        CUS_Location cuslocation = new CUS_Location();
                                        cuslocation.CreatedBy = Account.UserName;
                                        cuslocation.CreatedDate = DateTime.Now;
                                        cuslocation.LocationID = location.LocationID;
                                        cuslocation.Code = location.Code;
                                        cuslocation.CustomerID = partner.CustomerID;
                                        cuslocation.CUS_Partner = cuspart;
                                        var catlocation = model.CAT_Location.FirstOrDefault(c => c.ID == location.LocationID);
                                        if (catlocation != null)
                                            cuslocation.LocationName = catlocation.Location;
                                        else
                                            cuslocation.LocationName = string.Empty;
                                        model.CUS_Location.Add(cuslocation);
                                    }
                                }
                            }
                        }
                        // Ds partner cần thêm mới
                        foreach (var partner in customer.Where(c => !string.IsNullOrEmpty(c.Code)))
                        {
                            if (lstCusPartner.Count(c => c.PartnerID == partner.PartnerID) == 0)
                            {
                                CUS_Partner cuspart = new CUS_Partner();
                                cuspart.CreatedBy = Account.UserName;
                                cuspart.CreatedDate = DateTime.Now;
                                cuspart.PartnerCode = partner.Code;
                                cuspart.CustomerID = partner.CustomerID;
                                cuspart.PartnerID = partner.PartnerID;
                                model.CUS_Partner.Add(cuspart);

                                var lstLocation = item.lstCusLocation.Where(c => c.IsSuccess && c.PartnerID == partner.PartnerID && c.CustomerID == customer.Key);
                                // Ds location cần thêm mới
                                foreach (var location in lstLocation.Where(c => !string.IsNullOrEmpty(c.Code)))
                                {

                                    CUS_Location cuslocation = new CUS_Location();
                                    cuslocation.CreatedBy = Account.UserName;
                                    cuslocation.CreatedDate = DateTime.Now;
                                    cuslocation.LocationID = location.LocationID;
                                    cuslocation.Code = location.Code;
                                    cuslocation.CustomerID = partner.CustomerID;
                                    cuslocation.CUS_Partner = cuspart;
                                    var catlocation = model.CAT_Location.FirstOrDefault(c => c.ID == location.LocationID);
                                    if (catlocation != null)
                                        cuslocation.LocationName = catlocation.Location;
                                    else
                                        cuslocation.LocationName = string.Empty;
                                    model.CUS_Location.Add(cuslocation);
                                }
                            }
                        }
                    }
                }
            }
            model.SaveChanges();

        }

        //xoa cus partner khi import phan bo mã
        private static void Partner_Delete(DataEntities model, CUS_Partner cuspart, List<DTOCustomerLocationImport> lstCusLocation)
        {
            if (model.ORD_Order.Count(c => c.PartnerID == cuspart.ID) > 0)
                throw FaultHelper.BusinessFault(null, null, "Khách hàng " + cuspart.CUS_Customer.CustomerName + " - " + cuspart.CAT_Partner.PartnerName + " đã thiết lập đơn hàng, không thể xóa!");
            // Ds các location cũ
            var lstOldLocation = model.CUS_Location.Where(c => c.CusPartID == cuspart.ID);
            if (lstOldLocation.Count() > 0)
            {
                // Ds các location mới
                var lstNewLocatonID = lstCusLocation.Where(c => c.CustomerID == cuspart.CustomerID && !string.IsNullOrEmpty(c.Code)).Select(c => c.LocationID).Distinct().ToList();
                // Nếu tất cả location đều bị xóa => Xóa hết location cũ
                var lstOldLocationID = lstOldLocation.Select(c => c.LocationID).Distinct().ToList();
                if (lstNewLocatonID.Count(c => lstOldLocationID.Contains(c)) == 0)
                {
                    foreach (var location in lstOldLocation)
                        Location_Delete(model, location);
                }
                else
                    throw FaultHelper.BusinessFault(null, null, "Khách hàng " + cuspart.CUS_Customer.CustomerName + " - " + cuspart.CAT_Partner.PartnerName + " không thể xóa, xóa địa điểm con trước!");
            }
            model.CUS_Partner.Remove(cuspart);
        }

        //xoa cuslocation khi import phan bo mã
        private static void Location_Delete(DataEntities model, CUS_Location cuslocation)
        {
            try
            {
                if (model.ORD_Order.Count(c => c.LocationDepotID == cuslocation.ID || c.LocationDepotReturnID == cuslocation.ID || c.LocationFromID == cuslocation.ID || c.LocationToID == cuslocation.ID) == 0)
                {
                    if (model.ORD_GroupProduct.Count(c => c.LocationFromID == cuslocation.ID || c.LocationToID == cuslocation.ID) == 0)
                    {
                        if (model.ORD_Container.Count(c => c.LocationDepotID == cuslocation.ID || c.LocationDepotReturnID == cuslocation.ID || c.LocationFromID == cuslocation.ID || c.LocationToID == cuslocation.ID) == 0)
                        {
                            if (model.CAT_PriceDILoad.Count(c => c.LocationID == cuslocation.ID) == 0)
                            {
                                model.CUS_Location.Remove(cuslocation);
                            }
                        }
                    }
                }
            }
            catch
            {
                throw FaultHelper.BusinessFault(null, null, "Khách hàng " + cuslocation.CUS_Customer.CustomerName + " - " + cuslocation.LocationName + " không thể xóa, địa điểm đang được sử dụng!");
            }
        }

        //xóa địa điểm của partner 
        public static void LocationInPartner_Delete(DataEntities model, AccountItem Account, int partnerLocationID)
        {

            var obj = model.CAT_PartnerLocation.FirstOrDefault(c => c.ID == partnerLocationID);
            if (obj == null)
                throw FaultHelper.BusinessFault(null, null, "Không tìm thấy dữ liệu PartnerLocationID:" + partnerLocationID);
            if (model.CUS_Location.Count(c => c.LocationID == obj.LocationID && c.CusPartID > 0 && c.CUS_Partner.PartnerID == obj.PartnerID) > 0)
                throw FaultHelper.BusinessFault(null, null, "Điểm đang được khách hàng sử dụng, ko thể xóa");


            model.CAT_PartnerLocation.Remove(obj);
            model.SaveChanges();
        }
        public static DTOCATLocationInPartner LocationInPartner_Save(DataEntities model, AccountItem Account, DTOCATLocationInPartner item, int partnerid)
        {

            if (item == null)
                throw FaultHelper.BusinessFault(null, null, "Dữ liệu cần lưu không chính xác");
            var objPartner = model.CAT_Partner.FirstOrDefault(c => c.ID == partnerid);
            if (objPartner == null)
                throw FaultHelper.BusinessFault(null, null, "Partner  không chính xác");
            if (string.IsNullOrEmpty(item.PartnerCode))
                throw FaultHelper.BusinessFault(null, null, "Mã không được trống");

            item.PartnerCode = item.PartnerCode.Trim();

            if (model.CAT_Location.Where(c => c.ID != item.LocationID && c.Code == item.PartnerCode).Count() > 0)
                throw FaultHelper.BusinessFault(null, null, "Mã đã sử dụng");
            if (model.CAT_PartnerLocation.Count(c => c.ID != item.ID && c.PartnerCode == item.PartnerCode) > 0)
                throw FaultHelper.BusinessFault(null, null, "Mã đã sử dụng trong " + objPartner.Code);

            var obj = model.CAT_Location.FirstOrDefault(c => c.ID == item.LocationID);
            if (obj == null)
            {
                obj = new CAT_Location();
                obj.CreatedBy = Account.UserName;
                obj.CreatedDate = DateTime.Now;
            }
            if (model.CAT_PartnerLocation.Where(c => c.LocationID == item.LocationID).Count() < 2)
            {
                if (obj != null)
                {
                    obj.ModifiedBy = Account.UserName;
                    obj.ModifiedDate = DateTime.Now;
                }
                obj.Code = item.PartnerCode;
                obj.Location = item.Location;
                obj.Address = !string.IsNullOrEmpty(item.Address) ? item.Address : string.Empty;
                obj.WardID = item.WardID;
                if (obj.WardID < 1) obj.WardID = null;
                obj.DistrictID = item.DistrictID;
                obj.ProvinceID = item.ProvinceID;
                obj.CountryID = item.CountryID;
                obj.Lat = item.Lat;
                obj.Lng = item.Lng;
                if (obj.ID < 1)
                    model.CAT_Location.Add(obj);
            }

            var detail = model.CAT_PartnerLocation.FirstOrDefault(c => c.ID == item.ID);
            if (detail == null)
            {
                detail = new CAT_PartnerLocation();
                detail.CreatedBy = Account.UserName;
                detail.CreatedDate = DateTime.Now;
                model.CAT_PartnerLocation.Add(detail);
            }
            else
            {
                detail.ModifiedBy = Account.UserName;
                detail.ModifiedDate = DateTime.Now;
            }
            detail.CAT_Location = obj;
            detail.PartnerID = partnerid;
            detail.PartnerCode = item.PartnerCode;

            model.SaveChanges();

            Routing_Update(model, Account, obj.ID);
            model.SaveChanges();
            item.ID = detail.ID;
            item.LocationID = obj.ID;

            if (objPartner.TypeOfPartnerID == -(int)SYSVarType.TypeOfPartnerSeaPort || objPartner.TypeOfPartnerID == -(int)SYSVarType.TypeOfPartnerCarrier)
            {
                if (objPartner.TypeOfPartnerID == -(int)SYSVarType.TypeOfPartnerCarrier)
                    ApplyDepotForAllCarrier(model, Account, new List<CAT_PartnerLocation> { detail });
                ApplyLocationForAllCus(model, Account, new List<CAT_PartnerLocation> { detail });
            }

            return item;
        }
        public static void LocationNotInPartner_Save(DataEntities model, AccountItem Account, List<DTOCATLocationInPartner> lst, int partnerid)
        {
            if (lst == null || lst.Count == 0)
                throw FaultHelper.BusinessFault(null, null, "Dữ liệu cần lưu không chính xác");
            var objPartner = model.CAT_Partner.FirstOrDefault(c => c.ID == partnerid);
            if (objPartner == null) throw FaultHelper.BusinessFault(null, null, "Không tìm thấy partner");
            List<CAT_PartnerLocation> lstCATPartLocation = new List<CAT_PartnerLocation>();
            foreach (var item in lst)
            {
                var obj = model.CAT_PartnerLocation.FirstOrDefault(c => c.PartnerID == partnerid && c.LocationID == item.ID);
                if (obj == null)
                {
                    obj = new CAT_PartnerLocation();
                    obj.PartnerID = partnerid;
                    obj.LocationID = item.ID;
                    obj.CreatedBy = Account.UserName;
                    obj.CreatedDate = DateTime.Now;
                    model.CAT_PartnerLocation.Add(obj);
                    lstCATPartLocation.Add(obj);
                }
                else
                {
                    obj.ModifiedBy = Account.UserName;
                    obj.ModifiedDate = DateTime.Now;
                }
                obj.PartnerCode = item.PartnerCode;
            }
            model.SaveChanges();
            if (objPartner.TypeOfPartnerID == -(int)SYSVarType.TypeOfPartnerSeaPort || objPartner.TypeOfPartnerID == -(int)SYSVarType.TypeOfPartnerCarrier)
            {
                if (objPartner.TypeOfPartnerID == -(int)SYSVarType.TypeOfPartnerCarrier)
                    ApplyDepotForAllCarrier(model, Account, lstCATPartLocation);
                ApplyLocationForAllCus(model, Account, lstCATPartLocation);
            }
        }

        //chuyển từ BLOrder
        public static List<AddressSearchItem> ORDOrder_Excel_Location_Create(DataEntities model, AccountItem Account, List<DTOORDOrder_Import_PartnerLocation> data)
        {
            List<AddressSearchItem> result = new List<AddressSearchItem>();
            int pHN = 1, pHCM = 2;
            var pData = new List<int>();
            var dData = new List<int>();
            var cDef = model.CAT_Country.FirstOrDefault();
            if (data != null && cDef != null)
            {
                //Thêm mới địa chỉ.
                data = data.Distinct().Where(c => c.ProvinceID > 0 && c.DistrictID > 0).ToList();
                foreach (var pObj in data.Where(c => !string.IsNullOrEmpty(c.PartnerCode)).GroupBy(c => new { c.PartnerCode, c.PartnerName, c.CustomerID }))
                {
                    var customer = model.CUS_Customer.FirstOrDefault(c => c.ID == pObj.Key.CustomerID);
                    var cusPartner = model.CUS_Partner.FirstOrDefault(c => c.PartnerCode == pObj.Key.PartnerCode && c.CustomerID == pObj.Key.CustomerID);
                    // Chỉ được tạo mới khi được thiết lập IsCreatePartner = true
                    if (customer.IsCreatePartner == true)
                    {
                        if (cusPartner == null)
                        {
                            //Kiểm tra CATPartner, nếu chưa có => tạo mới
                            var catPartner = model.CAT_Partner.FirstOrDefault(c => c.Code == pObj.Key.PartnerCode);
                            if (catPartner == null)
                            {
                                catPartner = new CAT_Partner();
                                catPartner.CreatedBy = Account.UserName;
                                catPartner.CreatedDate = DateTime.Now;

                                catPartner.Code = pObj.Key.PartnerCode;
                                catPartner.PartnerName = pObj.Key.PartnerName;
                                catPartner.TypeOfPartnerID = -(int)SYSVarType.TypeOfPartnerDistributor;

                                model.CAT_Partner.Add(catPartner);
                            }

                            cusPartner = new CUS_Partner();
                            cusPartner.CreatedBy = Account.UserName;
                            cusPartner.CreatedDate = DateTime.Now;
                            cusPartner.CustomerID = pObj.Key.CustomerID;
                            cusPartner.PartnerCode = pObj.Key.PartnerCode;
                            cusPartner.CAT_Partner = catPartner;
                            model.CUS_Partner.Add(cusPartner);

                            model.SaveChanges();
                        }
                    }
                    // Chỉ tạo mới khi có cusPartner và IsCreateLocation = true
                    if (cusPartner != null && customer.IsCreateLocation == true)
                    {
                        int idx = model.CAT_PartnerLocation.Where(c => c.PartnerID == cusPartner.PartnerID).Count() + 1;

                        foreach (var lObj in pObj)
                        {
                            if (string.IsNullOrEmpty(lObj.LocationCode))
                            {
                                lObj.LocationCode = pObj.Key.PartnerCode + (idx < 100 ? idx.ToString("00") : idx.ToString("000"));
                                while (model.CAT_Location.Count(c => c.Code == lObj.LocationCode) > 0)
                                {
                                    idx++;
                                    lObj.LocationCode = pObj.Key.PartnerCode + (idx < 100 ? idx.ToString("00") : idx.ToString("000"));
                                }
                            }

                            var catLocation = model.CAT_Location.FirstOrDefault(c => c.Code == lObj.LocationCode);
                            if (catLocation == null)
                            {
                                catLocation = new CAT_Location();
                                catLocation.CreatedBy = Account.UserName;
                                catLocation.CreatedDate = DateTime.Now;
                                catLocation.Code = lObj.LocationCode;
                                catLocation.Location = pObj.Key.PartnerName;
                                catLocation.CountryID = cDef.ID;
                                catLocation.ProvinceID = lObj.ProvinceID;
                                catLocation.DistrictID = lObj.DistrictID;
                                model.CAT_Location.Add(catLocation);
                            }
                            else
                            {
                                if (catLocation.ProvinceID != lObj.ProvinceID || catLocation.DistrictID != lObj.DistrictID)
                                    throw FaultHelper.BusinessFault(null, null, "Mã điểm giao [" + lObj.LocationCode + "] đã tồn tại");
                            }

                            if (cusPartner.CAT_Partner.GroupOfPartnerID > 0 && cusPartner.CAT_Partner.CAT_GroupOfPartner.CAT_GroupOfLocation.FirstOrDefault() != null)
                                catLocation.GroupOfLocationID = cusPartner.CAT_Partner.CAT_GroupOfPartner.CAT_GroupOfLocation.FirstOrDefault().ID;

                            catLocation.Address = lObj.LocationAddress;
                            catLocation.EconomicZone = lObj.EconomicZone;

                            if (catLocation.ProvinceID != pHCM && catLocation.ProvinceID != pHN)
                                pData.Add(catLocation.ProvinceID);
                            if (catLocation.ProvinceID == pHCM || catLocation.ProvinceID == pHN)
                                dData.Add(catLocation.DistrictID);

                            if (catLocation == null || model.CAT_PartnerLocation.Count(c => c.PartnerID == cusPartner.PartnerID && c.LocationID == catLocation.ID) == 0)
                            {
                                CAT_PartnerLocation catPartLocation = new CAT_PartnerLocation();
                                catPartLocation.CreatedBy = Account.UserName;
                                catPartLocation.CreatedDate = DateTime.Now;

                                catPartLocation.CAT_Partner = cusPartner.CAT_Partner;
                                catPartLocation.CAT_Location = catLocation;
                                catPartLocation.PartnerCode = lObj.LocationCode;

                                model.CAT_PartnerLocation.Add(catPartLocation);
                            }

                            if (cusPartner.ID > 0)
                            {
                                if (model.CUS_Location.Count(c => c.CusPartID == cusPartner.ID && c.Code == lObj.LocationCode) > 0)
                                    throw FaultHelper.BusinessFault(null, null, "Mã điểm giao [" + lObj.LocationCode + "] đã tồn tại");
                            }

                            if (cusPartner.ID > 0 && catLocation.ID > 0)
                            {
                                if (model.CUS_Location.Count(c => c.CusPartID == cusPartner.ID && c.LocationID == catLocation.ID) > 0)
                                    throw FaultHelper.BusinessFault(null, null, "Mã điểm giao [" + lObj.LocationCode + "] đã tồn tại");
                            }

                            CUS_Location cusLocation = new CUS_Location();
                            cusLocation.CreatedBy = Account.UserName;
                            cusLocation.CreatedDate = DateTime.Now;

                            cusLocation.Code = lObj.LocationCode;
                            cusLocation.LocationName = catLocation.Location;
                            cusLocation.CustomerID = pObj.Key.CustomerID;
                            cusLocation.CUS_Partner = cusPartner;
                            cusLocation.CAT_Location = catLocation;
                            cusLocation.RoutingAreaCode = lObj.RoutingAreaCode;
                            if (!string.IsNullOrEmpty(lObj.RoutingAreaCode))
                            {
                                cusLocation.RoutingAreaCodeNoUnicode = HelperString.RemoveSign4VietnameseString(lObj.RoutingAreaCode);
                            }

                            model.CUS_Location.Add(cusLocation);
                            try
                            {
                                model.SaveChanges();
                            }
                            catch (Exception ex)
                            {
                                throw;
                            }

                            AddressSearchItem _obj = new AddressSearchItem();
                            _obj.CUSLocationID = cusLocation.ID;
                            _obj.CustomerID = cusLocation.CustomerID;
                            _obj.CUSPartnerID = cusLocation.CusPartID;
                            _obj.PartnerCode = cusLocation.CusPartID > 0 ? cusLocation.CUS_Partner.PartnerCode : "";
                            _obj.LocationCode = cusLocation.Code;
                            _obj.Address = cusLocation.CAT_Location.Address;
                            _obj.EconomicZone = cusLocation.CAT_Location.EconomicZone;

                            result.Add(_obj);
                        }
                    }
                }

                //Cập nhật khu vực
                var aData = model.CAT_RoutingAreaDetail.Where(c => c.ProvinceID > 0 && pData.Contains(c.ProvinceID.Value)).Select(c => c.RoutingAreaID).ToList();
                aData.AddRange(model.CAT_RoutingAreaDetail.Where(c => c.DistrictID > 0 && dData.Contains(c.DistrictID.Value)).Select(c => c.RoutingAreaID).ToList());

                foreach (var aObj in aData.Distinct())
                {
                    var aW_Data = model.CAT_RoutingAreaDetail.Where(c => c.RoutingAreaID == aObj && c.WardID.HasValue).Select(c => c.WardID.Value).Distinct().ToArray();
                    var aD_Data = model.CAT_RoutingAreaDetail.Where(c => c.RoutingAreaID == aObj && c.DistrictID.HasValue && c.WardID == null).Select(c => c.DistrictID.Value).Distinct().ToArray();
                    var aP_Data = model.CAT_RoutingAreaDetail.Where(c => c.RoutingAreaID == aObj && c.ProvinceID.HasValue && c.DistrictID == null && c.WardID == null).Select(c => c.ProvinceID.Value).Distinct().ToArray();

                    var aL_Data = model.CAT_Location.Where(c => (c.WardID.HasValue && aW_Data.Contains(c.WardID.Value)) || aD_Data.Contains(c.DistrictID) || aP_Data.Contains(c.ProvinceID)).Select(c => c.ID).ToArray();
                    foreach (var i in aL_Data)
                    {
                        var objLocation = model.CAT_RoutingAreaLocation.FirstOrDefault(c => c.RoutingAreaID == aObj && c.LocationID == i);
                        if (objLocation == null)
                        {
                            objLocation = new CAT_RoutingAreaLocation();
                            objLocation.CreatedBy = Account.UserName;
                            objLocation.CreatedDate = DateTime.Now;

                            objLocation.LocationID = i;
                            objLocation.RoutingAreaID = aObj;

                            model.CAT_RoutingAreaLocation.Add(objLocation);
                        }
                    }
                }

                model.SaveChanges();
            }
            return result;
        }
        public static DTOORDData_Location ORDOrder_NewLocation_Save(DataEntities model, AccountItem Account, DTOORDOrderNewLocation item)
        {

            #region Kiểm tra trùng Code và LocationID
            if (!string.IsNullOrEmpty(item.CATLocationCode))
                item.CATLocationCode = item.CATLocationCode.Trim();
            else throw FaultHelper.BusinessFault(null, null, "Mã hệ thống Không được rỗng!");

            item.CATLocationName = string.IsNullOrEmpty(item.CATLocationName) ? string.Empty : item.CATLocationName.Trim();
            item.CATLocationAddress = string.IsNullOrEmpty(item.CATLocationAddress) ? string.Empty : item.CATLocationAddress.Trim();

            var objCUSPart = model.CUS_Partner.FirstOrDefault(c => c.ID == item.CusPartnerID);
            if (objCUSPart == null)
                throw FaultHelper.BusinessFault(null, null, "Không tìm thấy đối tác");

            //check mã cus
            var objCheckLocation = model.CUS_Location.FirstOrDefault(c => c.ID != item.CUSLocationID && c.CustomerID == item.CustomerID && c.CusPartID == item.CusPartnerID && c.Code == item.CUSLocationCode);
            if (objCheckLocation != null)
                throw FaultHelper.BusinessFault(null, null, "Mã đã sử dụng!");



            var type = objCUSPart.CAT_Partner.TypeOfPartnerID;

            item.AreaCode = item.AreaCode.Trim();

            #endregion

            #region Lưu thông tin CAT_Location
            var objCAT = model.CAT_Location.FirstOrDefault(c => c.ID == item.CATLocationID);
            if (objCAT == null)
            {
                objCAT = new CAT_Location();
                objCAT.CreatedBy = Account.UserName;
                objCAT.CreatedDate = DateTime.Now;
                model.CAT_Location.Add(objCAT);

                objCAT.Code = item.CATLocationCode;
                objCAT.Location = item.CATLocationName;
                objCAT.Address = item.CATLocationAddress;
                objCAT.WardID = null;
                objCAT.DistrictID = item.DistrictID;
                objCAT.ProvinceID = item.ProvinceID;
                objCAT.CountryID = item.CountryID;
            }

            #endregion

            #region Lưu CAT_PartnerLocation
            var objPart = model.CAT_PartnerLocation.FirstOrDefault(c => c.LocationID == objCAT.ID && c.PartnerID == objCUSPart.PartnerID);
            if (objPart == null)
            {
                objPart = new CAT_PartnerLocation();
                objPart.CreatedBy = Account.UserName;
                objPart.CreatedDate = DateTime.Now;
                objPart.LocationID = objCAT.ID;
                objPart.PartnerID = objCUSPart.PartnerID;
                objPart.PartnerCode = item.CATLocationCode;
                model.CAT_PartnerLocation.Add(objPart);
            }
            #endregion

            #region Lưu thông tin CUS_Location
            var objCus = model.CUS_Location.FirstOrDefault(c => c.ID == item.CUSLocationID);
            if (objCus == null)
            {
                objCus = new CUS_Location();
                objCus.CreatedBy = Account.UserName;
                objCus.CreatedDate = DateTime.Now;
                objCus.CusPartID = item.CusPartnerID;
                objCus.CustomerID = objCUSPart.CustomerID;
                objCus.LocationID = objCAT.ID;
                model.CUS_Location.Add(objCus);
            }
            else
            {
                objCus.ModifiedBy = Account.UserName;
                objCus.ModifiedDate = DateTime.Now;
            }
            objCus.Code = item.CUSLocationCode;
            objCus.LocationName = item.CUSLocationName;
            objCus.RoutingAreaCode = item.AreaCode;
            objCus.RoutingAreaCodeNoUnicode = string.IsNullOrEmpty(item.AreaCode) ? "" : HelperString.RemoveSign4VietnameseString(item.AreaCode);
            #endregion

            model.SaveChanges();

            Routing_Update(model, Account, objCAT.ID);

            model.SaveChanges();

            DTOORDData_Location result = new DTOORDData_Location();

            result.LocationID = objCAT.ID;
            result.CUSLocationID = objCus.ID;
            result.LocationCode = objCus.Code;
            result.LocationName = objCus.LocationName;
            result.Address = objCAT.Address;
            result.CusPartID = objCus.CusPartID;
            result.Lat = objCAT.Lat;
            result.Lng = objCAT.Lng;

            return result;
        }

        //chuyen từ BLCustomer
        public static List<int> CUSPartner_SaveList(DataEntities model, AccountItem Account, List<DTOCUSPartnerAll> lst, int customerid)
        {
            List<int> result = new List<int>();
            #region Kiểm tra trùng PartnerID or PartnerCode
            foreach (var item in lst)
            {
                item.PartnerCode = item.PartnerCode.Trim();
                var obj = model.CUS_Partner.FirstOrDefault(c => c.ID != item.ID && c.CustomerID == customerid && (c.PartnerID == item.PartnerID || c.PartnerCode == item.PartnerCode));
                if (obj != null)
                    throw FaultHelper.BusinessFault(null, null, "Mã đã sử dụng!");
            }
            #endregion

            List<CUS_Partner> lstReturn = new List<CUS_Partner>();
            List<int> lstCATPartnerID = new List<int>();
            foreach (var item in lst)
            {
                lstCATPartnerID.Add(item.PartnerID);
                var obj = model.CUS_Partner.FirstOrDefault(c => c.CustomerID == customerid && c.PartnerID == item.PartnerID);
                if (obj == null)
                {
                    obj = new CUS_Partner();
                    obj.CreatedBy = Account.UserName;
                    obj.CreatedDate = DateTime.Now;
                    obj.CustomerID = customerid;
                    obj.PartnerID = item.PartnerID;
                    model.CUS_Partner.Add(obj);
                }
                else
                {
                    obj.ModifiedBy = Account.UserName;
                    obj.ModifiedDate = DateTime.Now;
                }
                obj.PartnerCode = item.PartnerCode;

                lstReturn.Add(obj);
            }
            model.SaveChanges();

            var lstCATPartner = model.CAT_Partner.Where(c => lstCATPartnerID.Contains(c.ID)).ToList();
            if (lstCATPartner.Count > 0)
            {
                if (lstCATPartner[0].TypeOfPartnerID == -(int)SYSVarType.TypeOfPartnerCarrier || lstCATPartner[0].TypeOfPartnerID == -(int)SYSVarType.TypeOfPartnerSeaPort)
                {
                    ApplyPartnerForAllCus(model, Account, lstCATPartner);
                }
            }

            result = lstReturn.Select(c => c.ID).ToList();
            return result;
        }
        public static void CUSPartner_Delete(DataEntities model, AccountItem Account, int cuspartid)
        {
            var obj = model.CUS_Partner.FirstOrDefault(c => c.ID == cuspartid);
            if (obj == null)
                throw FaultHelper.BusinessFault(null, null, "Không tìm thấy dữ liệu partner");
            List<int> lstCUSLocationID = new List<int>();

            lstCUSLocationID.AddRange(obj.CUS_Location.Select(c => c.ID).ToList());

            //kiem tra xem có đon hàng nào xài>0 thì ko cho xóa
            var lstOrderCode = model.ORD_GroupProduct.Where(c => lstCUSLocationID.Contains(c.LocationFromID.Value) || lstCUSLocationID.Contains(c.LocationToID.Value)).Select(c => c.ORD_Order.Code).Distinct().ToList();
            if (lstOrderCode.Count > 0)
                throw FaultHelper.BusinessFault(null, null, "Xóa đơn hàng: " + String.Join(", ", lstOrderCode) + " trước!");

            #region remove Cuslocation
            foreach (var cusLo in model.CUS_Location.Where(c => c.CusPartID == obj.ID))
            {
                foreach (var o in model.CAT_PriceDIEx.Where(c => c.CUSLocationID == obj.ID))
                {
                    foreach (var oo in model.CAT_PriceDIExGroupLocation.Where(c => c.PriceDIExID == o.ID))
                    {
                        model.CAT_PriceDIExGroupLocation.Remove(oo);
                    }
                    foreach (var oo in model.CAT_PriceDIExGroupProduct.Where(c => c.PriceDIExID == o.ID))
                    {
                        model.CAT_PriceDIExGroupProduct.Remove(oo);
                    }
                    foreach (var oo in model.CAT_PriceDIExRouting.Where(c => c.PriceDIExID == o.ID))
                    {
                        model.CAT_PriceDIExRouting.Remove(oo);
                    }
                    model.CAT_PriceDIEx.Remove(o);
                }
                model.CUS_Location.Remove(cusLo);
            }
            #endregion

            model.CUS_Partner.Remove(obj);
            model.SaveChanges();

        }
        public static int CUSPartner_Save(DataEntities model, AccountItem Account, DTOCUSPartnerAllCustom item, int customerid, int sysTypePartner)
        {
            #region Kiểm tra trùng Partner
            if (!string.IsNullOrEmpty(item.CATCode))
                item.CATCode = item.CATCode.Trim();
            if (!string.IsNullOrEmpty(item.CUSCode))
                item.CUSCode = item.CUSCode.Trim();

            if (model.CAT_Partner.Count(c => c.ID != item.PartnerID && c.Code == item.CATCode) > 0)
                throw FaultHelper.BusinessFault(null, null, "Mã hệ thống đã sử dụng!");

            if (model.CUS_Partner.Count(c => c.ID != item.ID && c.CustomerID == customerid && c.PartnerCode == item.CUSCode) > 0)
                throw FaultHelper.BusinessFault(null, null, "Mã đã sử dụng cho khách hàng!");


            //List<string> lst = new List<string>();
            //foreach (var o in model.CUS_Partner.Where(c => c.PartnerID == item.PartnerID && c.CustomerID != customerid).Select(c => new { c.CustomerID, c.CUS_Customer.CustomerName }).Distinct().ToList())
            //{
            //    lst.Add(o.CustomerName);
            //}
            //if (lst.Count > 0)
            //{
            //    string err = string.Join(",", lst);
            //    throw FaultHelper.BusinessFault(null, null, "Khách hàng [" + err + "] đang sử dụng!");
            //}
            if (model.CUS_Customer.Count(c => c.ID == customerid) == 0)
                throw FaultHelper.BusinessFault(null, null, "Không tìm thấy khách hàng ID:" + customerid);
            #endregion

            int iTypeOfPartnerCarrier = -(int)SYSVarType.TypeOfPartnerCarrier;
            int iTypeOfPartnerSeaport = -(int)SYSVarType.TypeOfPartnerSeaPort;

            #region Lưu CAT_Partner
            var obj = model.CAT_Partner.FirstOrDefault(c => c.ID == item.PartnerID);
            if (obj == null)
            {
                obj = new CAT_Partner();
                obj.CreatedBy = Account.UserName;
                obj.CreatedDate = DateTime.Now;
                obj.TypeOfPartnerID = sysTypePartner;
                //item.IsEditable = true;
                obj.Code = item.CATCode;
                obj.PartnerName = item.CATPartnerName;
                model.CAT_Partner.Add(obj);
            }
            else
            {
                obj.ModifiedBy = Account.UserName;
                obj.ModifiedDate = DateTime.Now;
            }
            obj.Address = !string.IsNullOrEmpty(item.Address) ? item.Address : string.Empty;
            obj.WardID = item.WardID;
            obj.DistrictID = item.DistrictID;
            obj.ProvinceID = item.ProvinceID;
            obj.CountryID = item.CountryID;
            obj.TelNo = item.TelNo;
            obj.Fax = item.Fax;
            obj.Email = item.Email;
            obj.BillingName = item.BillingName;
            obj.BillingAddress = item.BillingAddress;
            obj.TaxCode = item.TaxCode;
            obj.Note = item.Note;
            obj.BiddingID = item.BiddingID;
            obj.GroupOfPartnerID = item.GroupOfPartnerID > 0 ? item.GroupOfPartnerID : null;

            #endregion

            #region Lưu CUS_Partner
            var objCus = model.CUS_Partner.FirstOrDefault(c => c.ID == item.ID);
            if (objCus == null)
            {
                objCus = new CUS_Partner();
                objCus.CreatedBy = Account.UserName;
                objCus.CreatedDate = DateTime.Now;
                objCus.CustomerID = customerid;
                objCus.CAT_Partner = obj;
                model.CUS_Partner.Add(objCus);
            }
            else
            {
                objCus.ModifiedBy = Account.UserName;
                objCus.ModifiedDate = DateTime.Now;
            }
            objCus.PartnerCode = item.CUSCode;
            objCus.RateReturnEmpty = null;
            objCus.RateGetEmpty = null;
            if (sysTypePartner == iTypeOfPartnerCarrier)
            {
                objCus.RateReturnEmpty = item.RateGetEmpty;
                objCus.RateGetEmpty = item.RateReturnEmpty;
            }

            #endregion

            model.SaveChanges();

            if (sysTypePartner == iTypeOfPartnerCarrier || sysTypePartner == iTypeOfPartnerSeaport)
                ApplyPartnerForAllCus(model, Account, new List<CAT_Partner> { obj });

            return objCus.ID;
        }
        public static List<DTOCUSLocation> CUStomer_PartnerLocation_SaveList(DataEntities model, AccountItem Account, List<DTOCUSLocation> lst, int cuspartnerid)
        {
            #region Kiểm tra CUS_PartnerID
            var objCheck = model.CUS_Partner.FirstOrDefault(c => c.ID == cuspartnerid);
            if (objCheck == null)
                throw FaultHelper.BusinessFault(null, null, "Dữ liệu không tồn tại!");
            #endregion

            #region Kiểm tra trùng Code và LocationID
            foreach (var item in lst)
            {
                if (!string.IsNullOrEmpty(item.Code))
                    item.Code = item.Code.Trim();

                var objFile = lst.Where(c => !string.IsNullOrEmpty(c.Code)).FirstOrDefault(c => c.ID != item.ID && c.Code == item.Code);
                if (objFile != null)
                    throw FaultHelper.BusinessFault(null, null, "Dữ liệu lưu có mã [" + item.Code + "] bị trùng!");

                var obj = model.CUS_Location.FirstOrDefault(c => c.ID != item.ID && c.CusPartID == cuspartnerid && c.Code == item.Code);
                if (obj != null)
                    throw FaultHelper.BusinessFault(null, null, "Mã [" + item.Code + "] đã sử dụng trong hệ thống!");
            }
            #endregion

            foreach (var item in lst)
            {
                var obj = model.CUS_Location.FirstOrDefault(c => c.ID == item.ID);
                if (obj == null)
                {
                    obj = new CUS_Location();
                    obj.CreatedBy = Account.UserName;
                    obj.CreatedDate = DateTime.Now;
                    obj.CusPartID = cuspartnerid;
                    obj.CustomerID = objCheck.CustomerID;
                    obj.LocationID = item.LocationID;
                }
                else
                {
                    obj.ModifiedBy = Account.UserName;
                    obj.ModifiedDate = DateTime.Now;
                }
                obj.Code = item.Code;
                // set string Empty khi LocationName = null
                if (string.IsNullOrEmpty(item.LocationName))
                    item.LocationName = string.Empty;
                obj.LocationName = item.LocationName;
                item.StatusAddressSearch = 1;//mac dinh status la update
                if (obj.ID < 1)
                {
                    // Chỉ thêm mới khi Code != null
                    if (!string.IsNullOrEmpty(item.Code))
                        model.CUS_Location.Add(obj);
                }
                else
                {
                    // Xóa khi Code == null
                    if (string.IsNullOrEmpty(item.Code))
                    {
                        model.CUS_Location.Remove(obj);
                        item.StatusAddressSearch = 2;
                    }
                }
            }
            model.SaveChanges();

            return lst;
        }
        public static void CUStomer_PartnerLocation_NotInSaveList(DataEntities model, AccountItem Account, List<int> lst, int cuspartnerid)
        {
            var cusPartner = model.CUS_Partner.FirstOrDefault(c => c.ID == cuspartnerid);
            if (cusPartner == null)
                throw FaultHelper.BusinessFault(null, null, "Dữ liệu không tồn tại!");

            int type = cusPartner.CAT_Partner.TypeOfPartnerID;

            List<CAT_PartnerLocation> lstCATPartLocationNew = new List<CAT_PartnerLocation>();
            foreach (var catlocationid in lst)
            {
                #region Lay thông tin CAT_Location
                var catLocation = model.CAT_Location.FirstOrDefault(c => c.ID == catlocationid);
                if (catLocation == null)
                    throw FaultHelper.BusinessFault(null, null, "Dữ liệu không tồn tại!");
                #endregion

                #region Lưu CAT_PartnerLocation
                var objPart = model.CAT_PartnerLocation.FirstOrDefault(c => c.LocationID == catlocationid && c.PartnerID == cusPartner.PartnerID);
                if (objPart == null)
                {
                    objPart = new CAT_PartnerLocation();
                    objPart.CreatedBy = Account.UserName;
                    objPart.CreatedDate = DateTime.Now;
                    objPart.LocationID = catlocationid;
                    objPart.PartnerID = cusPartner.PartnerID;
                    objPart.PartnerCode = cusPartner.PartnerCode;
                    model.CAT_PartnerLocation.Add(objPart);
                    lstCATPartLocationNew.Add(objPart);
                }

                #endregion

                #region Lưu thông tin CUS_Location
                var objCus = model.CUS_Location.FirstOrDefault(c => c.CusPartID == cusPartner.ID && c.LocationID == catlocationid);
                if (objCus != null)
                    throw FaultHelper.BusinessFault(null, null, "Điểm đã tồn tại");

                objCus = new CUS_Location();
                objCus.CreatedBy = Account.UserName;
                objCus.CreatedDate = DateTime.Now;
                objCus.CusPartID = cuspartnerid;
                objCus.CustomerID = cusPartner.CustomerID;
                objCus.LocationID = catlocationid;
                objCus.Code = catLocation.Code;
                objCus.LocationName = catLocation.Location;
                model.CUS_Location.Add(objCus);
                #endregion

            }

            model.SaveChanges();

            if (type == -(int)SYSVarType.TypeOfPartnerCarrier || type == -(int)SYSVarType.TypeOfPartnerSeaPort)
            {
                if (type == -(int)SYSVarType.TypeOfPartnerCarrier) ApplyDepotForAllCarrier(model, Account, lstCATPartLocationNew);

                ApplyLocationForAllCus(model, Account, lstCATPartLocationNew);
            }

            foreach (var catlocationid in lst)
            {
                Routing_Update(model, Account, catlocationid);
                model.SaveChanges();
            }
        }
        public static DTOCUSPartnerLocation CUStomer_PartnerLocation_Save(DataEntities model, AccountItem Account, DTOCUSPartnerLocation item, int cuspartnerid)
        {

            #region Kiểm tra trùng Code và LocationID
            if (!string.IsNullOrEmpty(item.Code))
                item.Code = item.Code.Trim();
            else throw FaultHelper.BusinessFault(null, null, "Mã Không được rỗng!");

            item.Location = string.IsNullOrEmpty(item.Location) ? string.Empty : item.Location.Trim();
            item.Address = string.IsNullOrEmpty(item.Address) ? string.Empty : item.Address.Trim();

            //check mã cus
            var objCheckLocation = model.CUS_Location.FirstOrDefault(c => c.ID != item.ID && c.CusPartID == cuspartnerid && c.Code == item.Code);
            if (objCheckLocation != null)
                throw FaultHelper.BusinessFault(null, null, "Mã đã sử dụng!");

            var objCUSPart = model.CUS_Partner.FirstOrDefault(c => c.ID == cuspartnerid);
            if (objCUSPart == null)
                throw FaultHelper.BusinessFault(null, null, "Không tìm thấy CUS_Partner");

            var type = objCUSPart.CAT_Partner.TypeOfPartnerID;
            var newCATPartLocation = false;
            #endregion

            #region Lưu thông tin CAT_Location
            var obj = model.CAT_Location.FirstOrDefault(c => c.ID == item.LocationID);
            if (obj == null)
            {
                obj = new CAT_Location();
                obj.CreatedBy = Account.UserName;
                obj.CreatedDate = DateTime.Now;
                item.IsEditable = true;
                model.CAT_Location.Add(obj);
            }
            else
            {
                obj.ModifiedBy = Account.UserName;
                obj.ModifiedDate = DateTime.Now;
            }
            if (item.IsEditable)
            {
                var itemass = model.CAT_Location.Where(c => c.ID != item.LocationID && c.Code == item.Code).FirstOrDefault();
                if (model.CAT_Location.FirstOrDefault(c => c.ID != item.LocationID && c.Code == item.Code) != null)
                    throw FaultHelper.BusinessFault(null, null, "Mã đã sử dụng");
                obj.Code = item.Code;
                obj.Location = item.Location;
                obj.Address = item.Address;
                obj.WardID = item.WardID;
                obj.DistrictID = item.DistrictID;
                obj.ProvinceID = item.ProvinceID;
                obj.CountryID = item.CountryID;
                obj.Lat = item.Lat;
                obj.Lng = item.Lng;
                obj.GroupOfLocationID = item.GroupOfLocationID > 0 ? item.GroupOfLocationID : null;
            }

            #endregion

            #region Lưu CAT_PartnerLocation
            var objPart = model.CAT_PartnerLocation.FirstOrDefault(c => c.LocationID == obj.ID && c.PartnerID == objCUSPart.PartnerID);
            if (objPart == null)
            {
                objPart = new CAT_PartnerLocation();
                objPart.CreatedBy = Account.UserName;
                objPart.CreatedDate = DateTime.Now;
                objPart.LocationID = obj.ID;
                objPart.PartnerID = objCUSPart.PartnerID;
                objPart.PartnerCode = item.Code;
                model.CAT_PartnerLocation.Add(objPart);
                newCATPartLocation = true;
            }
            #endregion

            #region Lưu thông tin CUS_Location
            var objCus = model.CUS_Location.FirstOrDefault(c => c.ID == item.ID);
            if (objCus == null)
            {
                objCus = new CUS_Location();
                objCus.CreatedBy = Account.UserName;
                objCus.CreatedDate = DateTime.Now;
                objCus.CusPartID = cuspartnerid;
                objCus.CustomerID = objCUSPart.CustomerID;
                objCus.LocationID = obj.ID;
                model.CUS_Location.Add(objCus);
            }
            else
            {
                objCus.ModifiedBy = Account.UserName;
                objCus.ModifiedDate = DateTime.Now;
            }
            objCus.Code = item.Code;
            objCus.LocationName = item.Location;
            objCus.RoutingAreaCode = item.RoutingAreaCode;
            objCus.RoutingAreaCodeNoUnicode = item.RoutingAreaCode == null ? "" : HelperString.RemoveSign4VietnameseString(item.RoutingAreaCode);
            #endregion

            model.SaveChanges();


            if (type == -(int)SYSVarType.TypeOfPartnerCarrier && item.ApplyForAllCarrier && newCATPartLocation)
                ApplyDepotForAllCarrier(model, Account, new List<CAT_PartnerLocation> { objPart });
            if (type == -(int)SYSVarType.TypeOfPartnerSeaPort && item.ApplyForAllSeaport && newCATPartLocation)
                ApplyPortForAllSeaport(model, Account, new List<CAT_PartnerLocation> { objPart });

            Routing_Update(model, Account, obj.ID);

            model.SaveChanges();

            item.LocationID = obj.ID;
            item.ID = objCus.ID;

            return item;
        }
        public static void CUStomer_PartnerLocation_DeleteList(DataEntities model, AccountItem Account, List<DTOCUSLocation> lst)
        {
            foreach (var item in lst)
            {
                var obj = model.CUS_Location.FirstOrDefault(c => c.ID == item.ID);
                if (obj != null)
                {
                    model.CUS_Location.Remove(obj);
                    if (model.CUS_Location.Count(c => c.LocationID == obj.LocationID && c.ID != obj.ID) == 0)
                    {
                        var catlocation = model.CAT_Location.FirstOrDefault(c => c.ID == obj.LocationID);

                        if (catlocation != null)
                        {
                            foreach (var partner in model.CAT_PartnerLocation.Where(c => c.LocationID == catlocation.ID))
                            {
                                model.CAT_PartnerLocation.Remove(partner);
                            }
                            model.CAT_Location.Remove(catlocation);
                        }
                    }
                    model.SaveChanges();
                }
            }
        }
        public static void CUStomer_PartnerLocation_Delete(DataEntities model, AccountItem Account, DTOCUSLocation item)
        {
            //kiem tra xem có đon hàng nào xài>0 thì ko cho xóa
            var lstOrderCode = model.ORD_GroupProduct.Where(c => c.LocationFromID == item.ID || c.LocationToID == item.ID).Select(c => c.ORD_Order.Code).Distinct().ToList();
            if (lstOrderCode.Count > 0)
                throw FaultHelper.BusinessFault(null, null, "Xóa đơn hàng: " + String.Join(", ", lstOrderCode) + " trước!");

            var lstOrder = model.ORD_Order.Where(c => c.LocationFromID == item.ID || c.LocationToID == item.ID).Select(c => c.Code).Distinct().ToList();
            if (lstOrder.Count > 0)
                throw FaultHelper.BusinessFault(null, null, "Xóa đơn hàng: " + String.Join(", ", lstOrder) + " trước!");

            var lstDITOLocation = model.OPS_DITOLocation.Where(c => c.LocationID == item.LocationID).Select(c => c.OPS_DITOMaster.Code).Distinct().ToList();
            if (lstDITOLocation.Count > 0)
                throw FaultHelper.BusinessFault(null, null, "Chuyến : " + String.Join(", ", lstDITOLocation) + "đã sử dụng không được xóa!");

            var obj = model.CUS_Location.FirstOrDefault(c => c.ID == item.ID);
            if (obj != null)
            {
                foreach (var o in model.CAT_LocationMatrix.Where(c => c.LocationFromID == item.ID || c.LocationToID == item.ID))
                {
                    foreach (var oo in model.CAT_LocationMatrixDetail.Where(c => c.LocationMatrixID == o.ID))
                    {
                        foreach (var ooo in model.CAT_LocationMatrixStation.Where(c => c.LocationMatrixDetailID == oo.ID))
                        {
                            model.CAT_LocationMatrixStation.Remove(ooo);
                        }
                        model.CAT_LocationMatrixDetail.Remove(oo);
                    }
                    model.CAT_LocationMatrix.Remove(o);
                }

                model.CUS_Location.Remove(obj);

                // neu location chỉ có 1 cus xài-> xóa luon catlocation
                if (model.CUS_Location.Count(c => c.LocationID == obj.LocationID && c.ID != obj.ID) == 0)
                {
                    var catlocation = model.CAT_Location.FirstOrDefault(c => c.ID == obj.LocationID);
                    if (catlocation != null)
                    {

                        foreach (var partner in model.CAT_PartnerLocation.Where(c => c.LocationID == catlocation.ID))
                        {
                            model.CAT_PartnerLocation.Remove(partner);
                        }
                        foreach (var partner in model.CAT_RoutingAreaLocation.Where(c => c.LocationID == catlocation.ID))
                        {
                            model.CAT_RoutingAreaLocation.Remove(partner);
                        }

                        //xoa bang  gia CAT_PriceDILoad

                        foreach (var o in model.CAT_PriceDILoad.Where(c => c.LocationID == catlocation.ID))
                        {
                            foreach (var oo in model.CAT_PriceDILoadDetail.Where(c => c.PriceDILoadID == o.ID))
                            {
                                model.CAT_PriceDILoadDetail.Remove(oo);
                            }
                            model.CAT_PriceDILoad.Remove(o);
                        }

                        //xoa bang  gia CAT_PriceDIMOQRouting

                        foreach (var o in model.CAT_PriceDIMOQRouting.Where(c => c.LocationID == catlocation.ID))
                        {
                            model.CAT_PriceDIMOQRouting.Remove(o);
                        }

                        //xoa bang  gia CAT_PriceDIMOQLoadRouting

                        foreach (var o in model.CAT_PriceDIMOQLoadRouting.Where(c => c.LocationID == catlocation.ID))
                        {
                            model.CAT_PriceDIMOQLoadRouting.Remove(o);
                        }

                        //xoa bang  gia CAT_PriceRouting

                        foreach (var o in model.CAT_PriceRouting.Where(c => c.AreaFromID == catlocation.ID || c.AreaToID == catlocation.ID))
                        {
                            foreach (var oo in model.CAT_PriceRoutingCost.Where(c => c.PriceRoutingID == o.ID))
                            {
                                model.CAT_PriceRoutingCost.Remove(oo);
                            }
                            model.CAT_PriceRouting.Remove(o);
                        }

                        foreach (var routing in model.CAT_PriceDIExRouting.Where(c => c.LocationID == catlocation.ID))
                        {
                            foreach (var o in model.CAT_PriceDIEx.Where(c => c.ID == routing.PriceDIExID))
                            {
                                foreach (var oo in model.CAT_PriceDIExGroupLocation.Where(c => c.PriceDIExID == o.ID))
                                {
                                    model.CAT_PriceDIExGroupLocation.Remove(oo);
                                }
                                foreach (var oo in model.CAT_PriceDIExGroupProduct.Where(c => c.PriceDIExID == o.ID))
                                {
                                    model.CAT_PriceDIExGroupProduct.Remove(oo);
                                }
                                model.CAT_PriceDIEx.Remove(o);
                            }
                            model.CAT_PriceDIExRouting.Remove(routing);
                        }

                        model.CAT_Location.Remove(catlocation);
                    }
                }

                model.SaveChanges();
            }
        }
        public static List<AddressSearchItem> CUStomer_PartnerLocation_Import(DataEntities model, AccountItem Account, List<DTOPartnerImport> lst, int customerid, bool isCarrier, bool isSeaport, bool isDistributor)
        {
            var lstChangeGroupLocation = model.CAT_GroupOfLocation.Select(c => new { c.GroupOfPartnerID, c.ID }).ToList();

            List<AddressSearchItem> result = new List<AddressSearchItem>();
            int iTypeOfPartnerCarrier = -(int)SYSVarType.TypeOfPartnerCarrier;
            int iTypeOfPartnerSeaport = -(int)SYSVarType.TypeOfPartnerSeaPort;
            int iTypeOfPartnerDistributor = -(int)SYSVarType.TypeOfPartnerDistributor;
            int iTypeOfPartner = 0;
            if (isCarrier)
                iTypeOfPartner = iTypeOfPartnerCarrier;
            else if (isSeaport)
                iTypeOfPartner = iTypeOfPartnerSeaport;
            else iTypeOfPartner = iTypeOfPartnerDistributor;

            //

            List<string> lstLocationCodeNew = new List<string>();
            foreach (var partner in lst.Where(c => c.ExcelSuccess))
            {
                foreach (var location in partner.ListLocation.Where(c => c.ExcelSuccess))
                {
                    if (location.CATLocationID < 0 && !lstLocationCodeNew.Contains(location.CUSLocationCode))
                        lstLocationCodeNew.Add(location.CUSLocationCode);
                }
            }
            List<CAT_Location> lstObjLocationNew = new List<CAT_Location>();
            List<CAT_Location> lstObjLocationUse = new List<CAT_Location>();
            List<CAT_Partner> lstObjPartnerUse = new List<CAT_Partner>();
            List<CUS_Location> lstObjCusLocationUse = new List<CUS_Location>();
            List<CAT_PartnerLocation> lstObjCATPartLocation = new List<CAT_PartnerLocation>();

            foreach (var partner in lst.Where(c => c.ExcelSuccess))
            {
                if (string.IsNullOrEmpty(partner.CUSPartnerCode))
                    throw FaultHelper.BusinessFault(null, null, "Mã partner không được trống");
                if (model.CAT_Partner.Where(c => c.ID != partner.CATParterID && c.Code == partner.CUSPartnerCode && c.TypeOfPartnerID == iTypeOfPartner).Count() > 0)
                    throw FaultHelper.BusinessFault(null, null, "Mã đã sử dụng");
                if (model.CUS_Partner.Where(c => c.ID != partner.CUSPartnerID && c.PartnerCode == partner.CUSPartnerCode && c.CustomerID == customerid).Count() > 0)
                    throw FaultHelper.BusinessFault(null, null, "Mã đã sử dụng");

                #region CAT_Partner
                var objCATPart = model.CAT_Partner.FirstOrDefault(c => c.ID == partner.CATParterID);
                if (objCATPart == null)
                {
                    objCATPart = new CAT_Partner();
                    objCATPart.CreatedBy = Account.UserName;
                    objCATPart.CreatedDate = DateTime.Now;
                    objCATPart.TypeOfPartnerID = iTypeOfPartner;
                    objCATPart.Code = partner.CUSPartnerCode;
                    objCATPart.PartnerName = !string.IsNullOrEmpty(partner.CUSPartnerName) ? partner.CUSPartnerName.Trim() : string.Empty; ;
                    objCATPart.Address = !string.IsNullOrEmpty(partner.CUSPartnerAddress) ? partner.CUSPartnerAddress.Trim() : string.Empty;
                    objCATPart.GroupOfPartnerID = partner.GroupOfPartnerID;
                    objCATPart.DistrictID = partner.DistrictID;
                    objCATPart.ProvinceID = partner.ProvinceID;
                    objCATPart.CountryID = partner.CountryID;
                    objCATPart.TelNo = partner.TellNo;
                    objCATPart.Fax = partner.Fax;
                    model.CAT_Partner.Add(objCATPart);
                }

                lstObjPartnerUse.Add(objCATPart);
                #endregion

                #region CUS_Partner
                var objCUSPart = model.CUS_Partner.FirstOrDefault(c => c.ID == partner.CUSPartnerID);
                if (objCUSPart == null)
                {
                    objCUSPart = new CUS_Partner();
                    objCUSPart.CreatedBy = Account.UserName;
                    objCUSPart.CreatedDate = DateTime.Now;
                    objCUSPart.CustomerID = customerid;
                    objCUSPart.CAT_Partner = objCATPart;
                    model.CUS_Partner.Add(objCUSPart);
                }
                else
                {
                    objCUSPart.ModifiedBy = Account.UserName;
                    objCUSPart.ModifiedDate = DateTime.Now;
                }
                objCUSPart.PartnerCode = partner.CUSPartnerCode;
                #endregion

                List<CAT_Location> lstObjLocationByPartner = new List<CAT_Location>();

                #region luu location

                foreach (var location in partner.ListLocation.Where(c => c.ExcelSuccess))
                {
                    #region CAT_Location
                    var objCATLocation = model.CAT_Location.FirstOrDefault(c => c.ID == location.CATLocationID);
                    if (objCATLocation == null)
                    {
                        var check = lstObjLocationNew.FirstOrDefault(c => c.Code == location.CUSLocationCode);
                        if (check == null)
                        {
                            objCATLocation = new CAT_Location();
                            objCATLocation.CreatedBy = Account.UserName;
                            objCATLocation.CreatedDate = DateTime.Now;
                            objCATLocation.Code = location.CUSLocationCode.Trim();
                            objCATLocation.Location = !string.IsNullOrEmpty(location.CUSLocationName) ? location.CUSLocationName.Trim() : string.Empty;
                            objCATLocation.Address = !string.IsNullOrEmpty(location.CUSLocationAddress) ? location.CUSLocationAddress.Trim() : string.Empty;
                            objCATLocation.WardID = null;
                            objCATLocation.DistrictID = location.DistrictID;
                            objCATLocation.ProvinceID = location.ProvinceID;
                            objCATLocation.CountryID = location.CountryID;
                            objCATLocation.Lat = location.Lat;
                            objCATLocation.Lng = location.Lng;
                            var objGroupLocation = lstChangeGroupLocation.FirstOrDefault(c => c.GroupOfPartnerID == partner.GroupOfPartnerID);
                            if (objGroupLocation != null)
                                objCATLocation.GroupOfLocationID = objGroupLocation.ID;
                            model.CAT_Location.Add(objCATLocation);
                            lstObjLocationNew.Add(objCATLocation);
                        }
                        else objCATLocation = check;
                    }
                    lstObjLocationUse.Add(objCATLocation);
                    lstObjLocationByPartner.Add(objCATLocation);
                    #endregion

                    #region CUS_Location
                    var objCUSLocation = model.CUS_Location.FirstOrDefault(c => c.ID == location.CUSLocationID);
                    if (objCUSLocation == null)
                    {
                        objCUSLocation = new CUS_Location();
                        objCUSLocation.CreatedBy = Account.UserName;
                        objCUSLocation.CreatedDate = DateTime.Now;
                        objCUSLocation.CAT_Location = objCATLocation;
                        objCUSLocation.CustomerID = customerid;
                        objCUSLocation.CUS_Partner = objCUSPart;
                        objCUSLocation.Code = objCATLocation.Code;
                        objCUSLocation.LocationName = objCATLocation.Location;
                        model.CUS_Location.Add(objCUSLocation);
                    }
                    else
                    {
                        objCUSLocation.ModifiedBy = Account.UserName;
                        objCUSLocation.ModifiedDate = DateTime.Now;
                        objCUSLocation.LocationName = !string.IsNullOrEmpty(location.CUSLocationName) ? location.CUSLocationName.Trim() : string.Empty;
                    }
                    lstObjCusLocationUse.Add(objCUSLocation);
                    #endregion
                }
                #endregion

                model.SaveChanges();
                foreach (var catLocation in lstObjLocationByPartner)
                {
                    var objCATPartLocation = model.CAT_PartnerLocation.FirstOrDefault(c => c.PartnerID == objCATPart.ID && c.LocationID == catLocation.ID);
                    if (objCATPartLocation == null)
                    {
                        objCATPartLocation = new CAT_PartnerLocation();
                        objCATPartLocation.CreatedBy = Account.UserName;
                        objCATPartLocation.CreatedDate = DateTime.Now;
                        objCATPartLocation.CAT_Location = catLocation;
                        objCATPartLocation.CAT_Partner = objCATPart;
                        objCATPartLocation.PartnerCode = catLocation.Code;
                        model.CAT_PartnerLocation.Add(objCATPartLocation);
                        lstObjCATPartLocation.Add(objCATPartLocation);
                    }
                }

                model.SaveChanges();
            }

            if (iTypeOfPartner == iTypeOfPartnerCarrier || iTypeOfPartner == iTypeOfPartnerSeaport)
            {
                ApplyPartnerForAllCus(model, Account, lstObjPartnerUse);
                if (iTypeOfPartner == iTypeOfPartnerCarrier)
                    ApplyDepotForAllCarrier(model, Account, lstObjCATPartLocation);
                ApplyLocationForAllCus(model, Account, lstObjCATPartLocation);
            }

            foreach (var loNew in lstObjLocationNew)
            {
                Routing_Update(model, Account, loNew.ID);
                model.SaveChanges();
            }
            foreach (var cusLo in lstObjCusLocationUse)
            {
                AddressSearchItem objSearch = new AddressSearchItem();
                objSearch.CUSLocationID = cusLo.ID;
                objSearch.CUSPartnerID = cusLo.CusPartID;
                objSearch.PartnerCode = cusLo.CusPartID > 0 ? cusLo.CUS_Partner.PartnerCode : "";
                objSearch.LocationCode = cusLo.Code;
                objSearch.Address = cusLo.CAT_Location.Address;
                objSearch.EconomicZone = cusLo.CAT_Location.EconomicZone;
                result.Add(objSearch);
            }
            return result;
        }
        public static int CUSPartner_CUSLocationSaveCode(DataEntities model, AccountItem Account, DTOCUSPartnerLocationAll item)
        {

            item.CUSCode = item.CUSCode.Trim();
            item.CUSLocationName = item.CUSLocationName.Trim();
            var isdel = string.IsNullOrEmpty(item.CUSCode);
            if (string.IsNullOrEmpty(item.CUSLocationName))
                item.CUSLocationName = item.CUSCode;

            //xoa cap nhat dia dia cua partner
            int result = 0;

            if (item.CUSPartnerID > 0)
            {
                #region xoa dia diem partner
                var objCUSPartner = model.CUS_Partner.FirstOrDefault(c => c.ID == item.CUSPartnerID);
                if (objCUSPartner == null)
                    throw FaultHelper.BusinessFault(null, null, "Không tìm thấy partner!");
                //cuscode rỗng => xóa
                if (string.IsNullOrEmpty(item.CUSCode))
                {
                    var lstOrderCode = model.ORD_GroupProduct.Where(c => c.LocationFromID == item.CUSLocationID || c.LocationToID == item.CUSLocationID).Select(c => c.ORD_Order.Code).Distinct().ToList();
                    lstOrderCode.AddRange(model.ORD_Container.Where(c => c.LocationFromID == item.CUSLocationID || c.LocationToID == item.CUSLocationID || c.LocationDepotID == item.CUSLocationID || c.LocationDepotReturnID == item.CUSLocationID).Select(c => c.ORD_Order.Code).Distinct().ToList());
                    lstOrderCode = lstOrderCode.Distinct().ToList();
                    if (lstOrderCode.Count > 0)
                        throw FaultHelper.BusinessFault(null, null, "Xóa đơn hàng: " + String.Join(", ", lstOrderCode) + " trước!");
                }
                else// update code
                {
                    if (model.CUS_Location.Count(c => c.ID != item.CUSLocationID && c.CusPartID == item.CUSPartnerID && c.Code == item.CUSCode) > 0)
                        throw FaultHelper.BusinessFault(null, null, "Mã [" + item.CUSCode + "] đã sử dụng trong hệ thống!");
                }

                if (!string.IsNullOrEmpty(item.CUSCode))
                {
                    var obj = model.CUS_Location.FirstOrDefault(c => c.ID == item.CUSLocationID);
                    if (obj == null)
                    {
                        obj = new CUS_Location();
                        obj.CreatedBy = Account.UserName;
                        obj.CreatedDate = DateTime.Now;
                        obj.CusPartID = item.CUSPartnerID;
                        obj.CustomerID = objCUSPartner.CustomerID;
                        obj.LocationID = item.CATLocationID;
                        model.CUS_Location.Add(obj);
                    }
                    else
                    {
                        obj.ModifiedBy = Account.UserName;
                        obj.ModifiedDate = DateTime.Now;
                    }
                    obj.Code = item.CUSCode;
                    obj.LocationName = item.CUSLocationName;

                    model.SaveChanges();
                    result = obj.ID;
                }
                else
                {
                    var obj = model.CUS_Location.FirstOrDefault(c => c.ID == item.CUSLocationID);
                    if (obj == null)
                        throw FaultHelper.BusinessFault(null, null, "Không tìm thấy location!");
                    result = obj.ID;
                    model.CUS_Location.Remove(obj);
                    model.SaveChanges();
                }
                #endregion
            }
            //xoa cap nhat kho
            else if (item.CUSPartnerID < 0)
            {
                var obj = model.CUS_Location.FirstOrDefault(c => c.ID == item.CUSLocationID);
                if (obj == null) throw FaultHelper.BusinessFault(null, null, "Không tìm thấy kho");
                if (obj.CAT_LocationAreaDetail.Count > 0)
                    throw FaultHelper.BusinessFault(null, null, "Có dữ liệu con, không thể xóa or chỉnh sửa");
                if (model.ORD_Container.Count(c => isdel == true && (c.LocationDepotID == obj.ID || c.LocationDepotReturnID == obj.ID || c.LocationFromID == obj.ID || c.LocationToID == obj.ID)) > 0)
                    throw FaultHelper.BusinessFault(null, null, "Có dữ liệu đơn hàng, không thể xóa or chỉnh sửa");
                if (model.ORD_GroupProduct.Count(c => isdel == true && (c.LocationToOldID == obj.ID || c.LocationFromID == obj.ID || c.LocationToID == obj.ID)) > 0)
                    throw FaultHelper.BusinessFault(null, null, "Có dữ liệu đơn hàng, không thể xóa");

                result = obj.ID;

                if (string.IsNullOrEmpty(item.CUSCode))
                {
                    foreach (var objChild in model.CUS_GroupOfProductInStock.Where(c => c.StockID == obj.ID))
                    {
                        model.CUS_GroupOfProductInStock.Remove(objChild);
                    }
                    #region  ko xóa CAT khi remove code STMS-2018
                    //if (model.CUS_Location.Count(c => c.CustomerID != obj.CustomerID && c.LocationID == obj.LocationID) == 0)
                    //{
                    //    var catlocation = model.CAT_Location.FirstOrDefault(c => c.ID == obj.LocationID);
                    //    if (catlocation != null)
                    //    {
                    //        foreach (var route in model.CAT_Routing.Where(c => c.LocationFromID == obj.LocationID || c.LocationToID == obj.LocationID))
                    //        {
                    //            model.CAT_Routing.Remove(route);
                    //        }

                    //        foreach (var area in model.CAT_RoutingAreaLocation.Where(c => c.LocationID == obj.LocationID))
                    //        {
                    //            model.CAT_RoutingAreaLocation.Remove(area);
                    //        }

                    //        foreach (var matrx in model.CAT_LocationMatrix.Where(c => c.LocationFromID == obj.LocationID || c.LocationToID == obj.LocationID))
                    //        {
                    //            foreach (var detai in model.CAT_LocationMatrixDetail.Where(c => c.LocationMatrixID == matrx.ID))
                    //            {
                    //                model.CAT_LocationMatrixDetail.Remove(detai);
                    //            }
                    //            model.CAT_LocationMatrix.Remove(matrx);
                    //        }

                    //        model.CAT_Location.Remove(catlocation);

                    //    }

                    //}
                    #endregion

                    model.CUS_Location.Remove(obj);
                }
                else
                {
                    obj.ModifiedBy = Account.UserName;
                    obj.ModifiedDate = DateTime.Now;
                    obj.Code = item.CUSCode;
                    obj.LocationName = item.CUSLocationName;
                }
                model.SaveChanges();
            }

            return result;
        }

        #endregion

        #region CATRoutingArea
        public static int CATRoutingArea_Save(DataEntities model, AccountItem Account, CATRoutingArea item)
        {
            try
            {
                if (model.CAT_RoutingArea.Count(c => c.ID != item.ID && c.Code == item.Code) > 0)
                    throw FaultHelper.BusinessFault(null, null, "Mã đã sử dụng!");

                var obj = model.CAT_RoutingArea.FirstOrDefault(c => c.ID == item.ID);
                if (obj == null)
                {
                    obj = new CAT_RoutingArea();
                    obj.CreatedBy = Account.UserName;
                    obj.CreatedDate = DateTime.Now;
                    model.CAT_RoutingArea.Add(obj);
                }
                else
                {
                    obj.ModifiedBy = Account.UserName;
                    obj.ModifiedDate = DateTime.Now;
                }
                obj.Code = item.Code;
                obj.AreaName = item.AreaName;
                obj.CodeNoUnicode = HelperString.RemoveSign4VietnameseString(obj.Code);
                model.SaveChanges();

                return obj.ID;
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.BusinessFault(ex);
            }
        }
        public static void CATRoutingArea_Delete(DataEntities model, AccountItem Account, CATRoutingArea item)
        {
            try
            {
                var obj = model.CAT_RoutingArea.FirstOrDefault(c => c.ID == item.ID);
                if (obj != null)
                {
                    var lstRouting = model.CAT_Routing.Where(c => c.RoutingAreaFromID == obj.ID || c.RoutingAreaToID == obj.ID).Select(c => c.Code).ToList();
                    if (lstRouting.Count > 0)
                        throw FaultHelper.BusinessFault(null, null, "Khu vực đang được sử dụng bởi các cung đường: " + string.Join(", ", lstRouting));

                    foreach (var temp in model.CAT_RoutingAreaDetail.Where(c => c.RoutingAreaID == obj.ID))
                        model.CAT_RoutingAreaDetail.Remove(temp);
                    foreach (var temp in model.CAT_RoutingAreaLocation.Where(c => c.RoutingAreaID == obj.ID))
                        model.CAT_RoutingAreaLocation.Remove(temp);

                    model.CAT_RoutingArea.Remove(obj);
                    model.SaveChanges();
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.BusinessFault(ex);
            }
        }
        public static int CATRoutingAreaDetail_Save(DataEntities model, AccountItem Account, DTOCATRoutingAreaDetail item, int areaID)
        {
            try
            {
                if (item.ProvinceID == null)
                    throw FaultHelper.BusinessFault(null, null, "Vui lòng nhập tỉnh thành!");

                if (item.ProvinceID > 0 && item.DistrictID > 0)
                {
                    if (model.CAT_RoutingAreaDetail.Count(c => c.RoutingAreaID == areaID && c.ProvinceID == item.ProvinceID && c.DistrictID == null) > 0)
                        throw FaultHelper.BusinessFault(null, null, "Đã có khu vực bao quát khu vực đã chọn"); ;
                    if (model.CAT_RoutingAreaDetail.Count(c => c.RoutingAreaID == areaID && c.ProvinceID == item.ProvinceID && c.DistrictID == item.DistrictID) > 0)
                        throw FaultHelper.BusinessFault(null, null, "Khu vực chi tiết này đã tồn tại!");
                }
                if (item.ProvinceID > 0 && !(item.DistrictID > 0))
                {
                    if (model.CAT_RoutingAreaDetail.Count(c => c.RoutingAreaID == areaID && c.ProvinceID == item.ProvinceID && c.DistrictID == null) > 0)
                        throw FaultHelper.BusinessFault(null, null, "Khu vực chi tiết này đã tồn tại!"); ;
                    if (model.CAT_RoutingAreaDetail.Count(c => c.RoutingAreaID == areaID && c.ProvinceID == item.ProvinceID && c.DistrictID > 0) > 0)
                        throw FaultHelper.BusinessFault(null, null, "Đã tồn tại khu vực chi tiết, không thể thêm khu vực bao quát");
                }

                var obj = model.CAT_RoutingAreaDetail.FirstOrDefault(c => c.ID == item.ID);
                if (obj == null)
                {
                    obj = new CAT_RoutingAreaDetail();
                    obj.CreatedBy = Account.UserName;
                    obj.CreatedDate = DateTime.Now;
                }
                else
                {
                    obj.ModifiedBy = Account.UserName;
                    obj.ModifiedDate = DateTime.Now;
                }
                obj.RoutingAreaID = areaID;
                obj.ProvinceID = item.ProvinceID > 0 ? item.ProvinceID : null;
                obj.DistrictID = item.DistrictID > 0 ? item.DistrictID : null;
                obj.WardID = item.WardID < 1 ? null : item.WardID;
                obj.CountryID = item.CountryID;

                if (obj.ID < 1)
                    model.CAT_RoutingAreaDetail.Add(obj);
                model.SaveChanges();

                if (model.CAT_Routing.Count(c => c.RoutingAreaFromID == areaID || c.RoutingAreaToID == areaID) > 0)
                {
                    CATRoutingAreaLocation_Update(model, Account, areaID);
                    model.SaveChanges();
                }

                return obj.ID;
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.BusinessFault(ex);
            }
        }
        public static void CATRoutingAreaDetail_Delete(DataEntities model, AccountItem Account, DTOCATRoutingAreaDetail item)
        {
            try
            {
                DTOResult result = new DTOResult();
                var obj = model.CAT_RoutingAreaDetail.FirstOrDefault(c => c.ID == item.ID);
                if (obj != null)
                {
                    model.CAT_RoutingAreaDetail.Remove(obj);
                    model.SaveChanges();
                    if (model.CAT_Routing.Count(c => c.RoutingAreaFromID == obj.RoutingAreaID || c.RoutingAreaToID == obj.RoutingAreaID) > 0)
                    {
                        CATRoutingAreaLocation_Update(model, Account, obj.RoutingAreaID);
                        model.SaveChanges();
                    }
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.BusinessFault(ex);
            }
        }
        public static void CATRoutingAreaLocation_Update(DataEntities model, AccountItem Account, int areaID)
        {
            try
            {
                // Ktra nếu ko có detail thì bỏ qua
                if (model.CAT_RoutingAreaDetail.Count(c => c.RoutingAreaID == areaID) > 0)
                {
                    // Xóa area location cũ
                    foreach (var item in model.CAT_RoutingAreaLocation.Where(c => c.RoutingAreaID == areaID))
                        model.CAT_RoutingAreaLocation.Remove(item);

                    var lstWardID = model.CAT_RoutingAreaDetail.Where(c => c.RoutingAreaID == areaID && c.WardID.HasValue).Select(c => c.WardID.Value).Distinct().ToArray();
                    var lstDistrictID = model.CAT_RoutingAreaDetail.Where(c => c.RoutingAreaID == areaID && c.DistrictID.HasValue && c.WardID == null).Select(c => c.DistrictID.Value).Distinct().ToArray();
                    var lstProvinceID = model.CAT_RoutingAreaDetail.Where(c => c.RoutingAreaID == areaID && c.ProvinceID.HasValue && c.DistrictID == null && c.WardID == null).Select(c => c.ProvinceID.Value).Distinct().ToArray();

                    var lstLocation = model.CAT_Location.Where(c => (c.WardID.HasValue && lstWardID.Contains(c.WardID.Value)) || lstDistrictID.Contains(c.DistrictID) || lstProvinceID.Contains(c.ProvinceID)).Select(c => c.ID).ToArray();
                    foreach (var location in lstLocation)
                    {
                        CAT_RoutingAreaLocation objLocation = new CAT_RoutingAreaLocation();
                        objLocation.CreatedBy = Account.UserName;
                        objLocation.CreatedDate = DateTime.Now;
                        objLocation.LocationID = location;
                        objLocation.RoutingAreaID = areaID;
                        model.CAT_RoutingAreaLocation.Add(objLocation);
                    }
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.BusinessFault(ex);
            }
        }

        public static DTOResult CATRoutingAreaExcel_List(DataEntities model, AccountItem Account)
        {
            try
            {
                DTOResult result = new DTOResult();
                var query = model.CAT_RoutingArea.Select(c => new DTOExcelRouteArea
                {
                    ID = c.ID,
                    Code = c.Code,
                    Name = c.AreaName,
                    ParentID = c.ParentID > 0 ? c.ParentID : -1
                }).ToList();

                foreach (DTOExcelRouteArea item in query)
                {
                    item.ListArea = model.CAT_RoutingAreaDetail.Where(c => c.RoutingAreaID == item.ID).Select(c => new DTOCATRoutingAreaDetail
                    {
                        ProvinceID = c.ProvinceID,
                        ProvinceName = c.ProvinceID > 0 ? c.CAT_Province.ProvinceName : string.Empty,
                        DistrictID = c.DistrictID,
                        DistrictName = c.DistrictID > 0 ? c.CAT_District.DistrictName : string.Empty
                    }).ToList();
                }
                result.Data = query;
                result.Total = query.Count;
                return result;
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.BusinessFault(ex);
            }
        }
        public static void CATRoutingAreaExcel_Save(DataEntities model, AccountItem Account, List<DTOExcelRouteArea> lstArea)
        {
            try
            {
                foreach (var o in lstArea.Where(c => c.ExcelSuccess == true))
                {
                    var obj = model.CAT_RoutingArea.FirstOrDefault(c => c.Code == o.Code);
                    if (obj == null)
                    {
                        obj = new CAT_RoutingArea();
                        obj.Code = o.Code;
                        obj.CodeNoUnicode = HelperString.RemoveSign4VietnameseString(o.Code);
                        obj.CreatedBy = Account.UserName;
                        obj.CreatedDate = DateTime.Now;

                        model.CAT_RoutingArea.Add(obj);
                    }
                    else
                    {
                        var lstDetail = model.CAT_RoutingAreaDetail.Where(c => c.RoutingAreaID == obj.ID).ToList();
                        if (lstDetail != null && lstDetail.Count > 0)
                            model.CAT_RoutingAreaDetail.RemoveRange(lstDetail);
                        obj.ModifiedBy = Account.UserName;
                        obj.ModifiedDate = DateTime.Now;
                    }

                    obj.AreaName = o.Name;

                    if (!string.IsNullOrEmpty(o.ParentCode))
                    {
                        var objParent = model.CAT_RoutingArea.FirstOrDefault(c => c.Code == o.ParentCode);
                        if (objParent == null)
                            throw FaultHelper.BusinessFault(null, null, "Không tìm thấy khu vực cha!");
                        obj.ParentID = objParent.ID;
                    }

                    model.SaveChanges();
                    if (o.ListArea != null && o.ListArea.Count > 0)
                    {
                        foreach (var area in o.ListArea.Distinct().ToList())
                        {
                            CAT_RoutingAreaDetail objDetail = new CAT_RoutingAreaDetail();
                            objDetail.RoutingAreaID = obj.ID;
                            objDetail.CountryID = 1;

                            objDetail.CreatedBy = Account.UserName;
                            objDetail.CreatedDate = DateTime.Now;

                            var p = model.CAT_Province.FirstOrDefault(c => c.ProvinceName == area.ProvinceName);
                            int districtID = -1;
                            if (!string.IsNullOrEmpty(area.DistrictName))
                            {
                                var d = model.CAT_District.FirstOrDefault(c => c.DistrictName == area.DistrictName);
                                if (d != null)
                                    districtID = d.ID;
                            }
                            if (p != null)
                                objDetail.ProvinceID = p.ID;
                            if (districtID > 0)
                                objDetail.DistrictID = districtID;
                            model.CAT_RoutingAreaDetail.Add(objDetail);
                        }
                        model.SaveChanges();
                    }
                    ////Tam khoa
                    //RoutingAreaLocation_Update(model, obj.ID);
                    model.SaveChanges();
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.BusinessFault(ex);
            }
        }

        public static List<DTOExcelRouteAreaLocation> CATRouteAreaLocationExcel_List(DataEntities model, AccountItem Account)
        {
            try
            {
                List<DTOExcelRouteAreaLocation> result = new List<DTOExcelRouteAreaLocation>();
                result = model.CAT_RoutingAreaLocation.Select(c => new DTOExcelRouteAreaLocation
                {
                    ID = c.ID,
                    LocationID = c.LocationID,
                    LocationCode = c.CAT_Location.Code,
                    LocationName = c.CAT_Location.Location,
                    LocationAdress = c.CAT_Location.Address,
                    AreaID = c.CAT_RoutingArea.ID,
                    AreaCode = c.CAT_RoutingArea.Code,
                    AreaName = c.CAT_RoutingArea.AreaName,
                    CountryID = c.CAT_Location.CountryID,
                    CountryName = c.CAT_Location.CAT_Country.CountryName,
                    ProvinceID = c.CAT_Location.ProvinceID,
                    ProvinceName = c.CAT_Location.CAT_Province.ProvinceName,
                    DistrictID = c.CAT_Location.DistrictID,
                    DistrictName = c.CAT_Location.CAT_District.DistrictName,
                    WardID = c.CAT_Location.WardID,
                    WardName = c.CAT_Location.WardID == null ? string.Empty : c.CAT_Location.CAT_Ward.WardName
                }).ToList();
                return result;
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.BusinessFault(ex);
            }
        }
        #endregion

        #region aply folow sysseting
        public static void RunApplySetting(DataEntities model, AccountItem Account)
        {
            string sKey = SYSSettingKey.System.ToString();
            var objSetting = model.SYS_Setting.FirstOrDefault(c => c.Key == sKey && c.SYSCustomerID == Account.SYSCustomerID);
            if (objSetting != null && !string.IsNullOrEmpty(objSetting.Setting))
            {
                var setting = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DTOSYSSetting>>(objSetting.Setting).FirstOrDefault();
                if (setting != null)
                {
                    var lstCustomerID = model.CUS_Customer.Where(c => !c.IsSystem && (c.TypeOfCustomerID == -(int)SYSVarType.TypeOfCustomerCUS || c.TypeOfCustomerID == -(int)SYSVarType.TypeOfCustomerBOTH)).Select(c => c.ID).ToList();
                    var lstCUSPartner = model.CUS_Partner.Select(c => new { c.ID, c.CustomerID, c.PartnerID }).ToList();

                    if (setting.ApplyCarrierForAllCus)
                    {
                        var lstPartner = model.CAT_Partner.Where(c => c.TypeOfPartnerID == -(int)SYSVarType.TypeOfPartnerCarrier).Select(c => new { c.ID, c.Code }).ToList();
                        foreach (var customerid in lstCustomerID)
                        {
                            var lstPartnerID = lstCUSPartner.Where(c => c.CustomerID == customerid).Select(c => c.PartnerID).ToList();
                            if (lstPartnerID.Count < lstPartner.Count)
                            {
                                foreach (var partner in lstPartner.Where(c => !lstPartnerID.Contains(c.ID)))
                                {
                                    var obj = new CUS_Partner();
                                    obj.CreatedBy = Account.UserName;
                                    obj.CreatedDate = DateTime.Now;
                                    obj.PartnerID = partner.ID;
                                    obj.CustomerID = customerid;
                                    obj.PartnerCode = partner.Code;
                                    model.CUS_Partner.Add(obj);
                                }
                            }
                        }
                        model.SaveChanges();
                    }
                    if (setting.ApplySeaportForAllCus)
                    {
                        var lstPartner = model.CAT_Partner.Where(c => c.TypeOfPartnerID == -(int)SYSVarType.TypeOfPartnerSeaPort).Select(c => new { c.ID, c.Code }).ToList();
                        foreach (var customerid in lstCustomerID)
                        {
                            var lstPartnerID = lstCUSPartner.Where(c => c.CustomerID == customerid).Select(c => c.PartnerID).ToList();
                            if (lstPartnerID.Count < lstPartner.Count)
                            {
                                foreach (var partner in lstPartner.Where(c => !lstPartnerID.Contains(c.ID)))
                                {
                                    var obj = new CUS_Partner();
                                    obj.CreatedBy = Account.UserName;
                                    obj.CreatedDate = DateTime.Now;
                                    obj.PartnerID = partner.ID;
                                    obj.CustomerID = customerid;
                                    obj.PartnerCode = partner.Code;
                                    model.CUS_Partner.Add(obj);
                                }
                            }
                        }
                        model.SaveChanges();
                    }

                    lstCUSPartner = model.CUS_Partner.Select(c => new { c.ID, c.CustomerID, c.PartnerID }).ToList();
                    var lstCUSLocation = model.CUS_Location.Where(c => c.CusPartID > 0).Select(c => new { c.CusPartID, c.LocationID }).ToList();
                    if (setting.ApplyDepotForAllCarrier)
                    {
                        var lstPartnerID = model.CAT_Partner.Where(c => c.TypeOfPartnerID == -(int)SYSVarType.TypeOfPartnerCarrier).Select(c => c.ID).ToList();
                        var lstPartnerLocation = model.CAT_PartnerLocation.Where(c => c.CAT_Partner.TypeOfPartnerID == -(int)SYSVarType.TypeOfPartnerCarrier).Select(c => new { c.PartnerID, c.LocationID }).ToList();
                        var lstLocation = model.CAT_PartnerLocation.Where(c => c.CAT_Partner.TypeOfPartnerID == -(int)SYSVarType.TypeOfPartnerCarrier).Select(c => new { c.LocationID, c.CAT_Location.Code }).Distinct().ToList();
                        foreach (var partnerid in lstPartnerID)
                        {
                            var lstLocationID = lstPartnerLocation.Where(c => c.PartnerID == partnerid).Select(c => c.LocationID).ToList();
                            if (lstLocationID.Count < lstLocation.Count)
                            {
                                foreach (var location in lstLocation.Where(c => !lstLocationID.Contains(c.LocationID)))
                                {
                                    var obj = new CAT_PartnerLocation();
                                    obj.CreatedBy = Account.UserName;
                                    obj.CreatedDate = DateTime.Now;
                                    obj.LocationID = location.LocationID;
                                    obj.PartnerID = partnerid;
                                    obj.PartnerCode = location.Code;
                                    model.CAT_PartnerLocation.Add(obj);
                                }
                            }
                        }
                        model.SaveChanges();
                    }
                    if (setting.ApplyDepotForAllCus)
                    {
                        var lstCATLocation = model.CAT_PartnerLocation.Where(c => c.CAT_Partner.TypeOfPartnerID == -(int)SYSVarType.TypeOfPartnerCarrier).Select(c => new { c.PartnerID, c.LocationID, c.PartnerCode, c.CAT_Location.Location }).ToList();
                        foreach (var cuspartner in lstCUSPartner)
                        {
                            var lstCAT = lstCATLocation.Where(c => c.PartnerID == cuspartner.PartnerID).ToList();
                            var lstCUSID = lstCUSLocation.Where(c => c.CusPartID == cuspartner.ID).Select(c => c.LocationID).ToList();
                            if (lstCUSID.Count < lstCAT.Count)
                            {
                                foreach (var location in lstCAT.Where(c => !lstCUSID.Contains(c.LocationID)))
                                {
                                    var obj = new CUS_Location();
                                    obj.CreatedBy = Account.UserName;
                                    obj.CreatedDate = DateTime.Now;
                                    obj.LocationID = location.LocationID;
                                    obj.CustomerID = cuspartner.CustomerID;
                                    obj.CusPartID = cuspartner.ID;
                                    obj.Code = location.PartnerCode;
                                    obj.LocationName = location.Location;
                                    model.CUS_Location.Add(obj);
                                }
                            }
                        }
                        model.SaveChanges();
                    }
                    if (setting.ApplySeaportPointForAllCus)
                    {
                        var lstCATLocation = model.CAT_PartnerLocation.Where(c => c.CAT_Partner.TypeOfPartnerID == -(int)SYSVarType.TypeOfPartnerSeaPort).Select(c => new { c.PartnerID, c.LocationID, c.PartnerCode, c.CAT_Location.Location }).ToList();
                        foreach (var cuspartner in lstCUSPartner)
                        {
                            var lstCAT = lstCATLocation.Where(c => c.PartnerID == cuspartner.PartnerID).ToList();
                            var lstCUSID = lstCUSLocation.Where(c => c.CusPartID == cuspartner.ID).Select(c => c.LocationID).ToList();
                            if (lstCUSID.Count < lstCAT.Count)
                            {
                                foreach (var location in lstCAT.Where(c => !lstCUSID.Contains(c.LocationID)))
                                {
                                    var obj = new CUS_Location();
                                    obj.CreatedBy = Account.UserName;
                                    obj.CreatedDate = DateTime.Now;
                                    obj.LocationID = location.LocationID;
                                    obj.CustomerID = cuspartner.CustomerID;
                                    obj.CusPartID = cuspartner.ID;
                                    obj.Code = location.PartnerCode;
                                    obj.LocationName = location.Location;
                                    model.CUS_Location.Add(obj);
                                }
                            }
                        }
                        model.SaveChanges();
                    }
                }
            }
        }

        public static void ApplyPartnerForAllCus(DataEntities model, AccountItem Account, List<CAT_Partner> lstPartner)
        {
            string sKey = SYSSettingKey.System.ToString();
            var itemSetting = model.SYS_Setting.FirstOrDefault(c => c.Key == sKey && c.SYSCustomerID == Account.SYSCustomerID);
            List<DTOSYSSetting> lstSetting = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DTOSYSSetting>>(itemSetting.Setting);

            var setting = lstSetting.FirstOrDefault();

            var lstCustomer = model.CUS_Customer.Where(c => (c.TypeOfCustomerID == -(int)SYSVarType.TypeOfCustomerCUS || c.TypeOfCustomerID == -(int)SYSVarType.TypeOfCustomerBOTH) && c.ParentID == null && !c.IsSystem).Select(c => new { c.ID, c.Code, }).ToList();

            var lstApplyCarrier = lstPartner.Where(c => c.TypeOfPartnerID == -(int)SYSVarType.TypeOfPartnerCarrier).ToList();
            var lstApplySeaport = lstPartner.Where(c => c.TypeOfPartnerID == -(int)SYSVarType.TypeOfPartnerSeaPort).ToList();

            if (setting.ApplyCarrierForAllCus)
            {
                foreach (var partner in lstApplyCarrier)
                {
                    foreach (var customer in lstCustomer)
                    {
                        var objPartner = model.CUS_Partner.FirstOrDefault(c => c.PartnerID == partner.ID && c.CustomerID == customer.ID);
                        if (objPartner == null)
                        {
                            objPartner = new CUS_Partner();
                            objPartner.CreatedBy = Account.UserName;
                            objPartner.CreatedDate = DateTime.Now;
                            objPartner.PartnerID = partner.ID;
                            objPartner.CustomerID = customer.ID;
                            objPartner.PartnerCode = partner.Code;
                            model.CUS_Partner.Add(objPartner);
                        }
                    }
                }
                model.SaveChanges();
            }

            if (setting.ApplySeaportForAllCus)
            {
                foreach (var partner in lstApplySeaport)
                {
                    foreach (var customer in lstCustomer)
                    {
                        var objPartner = model.CUS_Partner.FirstOrDefault(c => c.PartnerID == partner.ID && c.CustomerID == customer.ID);
                        if (objPartner == null)
                        {
                            objPartner = new CUS_Partner();
                            objPartner.CreatedBy = Account.UserName;
                            objPartner.CreatedDate = DateTime.Now;
                            objPartner.PartnerID = partner.ID;
                            objPartner.CustomerID = customer.ID;
                            objPartner.PartnerCode = partner.Code;
                            model.CUS_Partner.Add(objPartner);
                        }
                    }
                }
                model.SaveChanges();
            }
        }
        public static void ApplyLocationForAllCus(DataEntities model, AccountItem Account, List<CAT_PartnerLocation> lstPartnerLocation)
        {
            string sKey = SYSSettingKey.System.ToString();
            var itemSetting = model.SYS_Setting.FirstOrDefault(c => c.Key == sKey && c.SYSCustomerID == Account.SYSCustomerID);
            List<DTOSYSSetting> lstSetting = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DTOSYSSetting>>(itemSetting.Setting);

            var setting = lstSetting.FirstOrDefault();
            var lstApplyCarrier = lstPartnerLocation.Where(c => c.CAT_Partner.TypeOfPartnerID == -(int)SYSVarType.TypeOfPartnerCarrier).ToList();
            var lstApplySeaport = lstPartnerLocation.Where(c => c.CAT_Partner.TypeOfPartnerID == -(int)SYSVarType.TypeOfPartnerSeaPort).ToList();

            var lstCUSPartner = model.CUS_Partner.Where(c => c.CAT_Partner.TypeOfPartnerID == -(int)SYSVarType.TypeOfPartnerCarrier || c.CAT_Partner.TypeOfPartnerID == -(int)SYSVarType.TypeOfPartnerSeaPort);

            if (setting.ApplyDepotForAllCus)
            {
                var lstCUSPartnerCarrier = lstCUSPartner.Where(c => c.CAT_Partner.TypeOfPartnerID == -(int)SYSVarType.TypeOfPartnerCarrier).Select(c => new { c.ID, c.PartnerID, c.CustomerID }).ToList();

                foreach (var cusPart in lstCUSPartnerCarrier)
                {
                    foreach (var partLo in lstApplyCarrier)
                    {
                        var objCUSLocation = model.CUS_Location.FirstOrDefault(c => c.CustomerID == cusPart.CustomerID && c.LocationID == partLo.LocationID && c.CusPartID == cusPart.ID);
                        if (objCUSLocation == null)
                        {
                            objCUSLocation = new CUS_Location();
                            objCUSLocation.CreatedBy = Account.UserName;
                            objCUSLocation.CreatedDate = DateTime.Now;
                            objCUSLocation.LocationID = partLo.LocationID;
                            objCUSLocation.CustomerID = cusPart.CustomerID;
                            objCUSLocation.CusPartID = cusPart.ID;
                            objCUSLocation.Code = partLo.CAT_Location.Code;
                            objCUSLocation.LocationName = partLo.CAT_Location.Location;
                            model.CUS_Location.Add(objCUSLocation);
                        }
                    }
                }

                model.SaveChanges();
            }
            if (setting.ApplySeaportPointForAllCus)
            {
                var lstCUSPartnerSeaport = lstCUSPartner.Where(c => c.CAT_Partner.TypeOfPartnerID == -(int)SYSVarType.TypeOfPartnerSeaPort).Select(c => new { c.ID, c.PartnerID, c.CustomerID }).ToList();

                foreach (var cusPart in lstCUSPartnerSeaport)
                {
                    foreach (var partLo in lstApplySeaport)
                    {
                        var objCUSLocation = model.CUS_Location.FirstOrDefault(c => c.CustomerID == cusPart.CustomerID && c.LocationID == partLo.LocationID && c.CusPartID == cusPart.ID);
                        if (objCUSLocation == null)
                        {
                            objCUSLocation = new CUS_Location();
                            objCUSLocation.CreatedBy = Account.UserName;
                            objCUSLocation.CreatedDate = DateTime.Now;
                            objCUSLocation.LocationID = partLo.LocationID;
                            objCUSLocation.CustomerID = cusPart.CustomerID;
                            objCUSLocation.CusPartID = cusPart.ID;
                            objCUSLocation.Code = partLo.CAT_Location.Code;
                            objCUSLocation.LocationName = partLo.CAT_Location.Location;
                            model.CUS_Location.Add(objCUSLocation);
                        }
                    }
                }

                model.SaveChanges();
            }

        }
        public static void ApplyDepotForAllCarrier(DataEntities model, AccountItem Account, List<CAT_PartnerLocation> lstLocation)
        {
            var lstCarrier = model.CAT_Partner.Where(c => c.TypeOfPartnerID == -(int)SYSVarType.TypeOfPartnerCarrier).ToList();

            var lstApply = lstLocation.Where(c => c.CAT_Partner.TypeOfPartnerID == -(int)SYSVarType.TypeOfPartnerCarrier).ToList();
            //hãng tàu thêm 1 location mới => các hãng tàu khác cũng có
            foreach (var location in lstApply)
            {
                var objLocation = model.CAT_Location.FirstOrDefault(c => c.ID == location.LocationID);
                if (objLocation != null)
                {
                    foreach (var carrier in lstCarrier)
                    {
                        var objCATPartLocation = model.CAT_PartnerLocation.FirstOrDefault(c => c.PartnerID == carrier.ID && c.LocationID == location.LocationID);
                        if (objCATPartLocation == null)
                        {
                            objCATPartLocation = new CAT_PartnerLocation();
                            objCATPartLocation.CreatedBy = Account.UserName;
                            objCATPartLocation.CreatedDate = DateTime.Now;
                            objCATPartLocation.LocationID = location.LocationID;
                            objCATPartLocation.PartnerID = carrier.ID;
                            objCATPartLocation.PartnerCode = objLocation.Code;
                            model.CAT_PartnerLocation.Add(objCATPartLocation);
                        }
                    }
                }
            }
            model.SaveChanges();
        }
        public static void ApplyPortForAllSeaport(DataEntities model, AccountItem Account, List<CAT_PartnerLocation> lstLocation)
        {
            var lstPartner = model.CAT_Partner.Where(c => c.TypeOfPartnerID == -(int)SYSVarType.TypeOfPartnerSeaPort).Select(c => new { c.ID, c.Code }).ToList();

            var lstApply = lstLocation.Where(c => c.CAT_Partner.TypeOfPartnerID == -(int)SYSVarType.TypeOfPartnerSeaPort).ToList();
            //hãng tàu thêm 1 location mới => các hãng tàu khác cũng có
            foreach (var location in lstApply)
            {
                var objLocation = model.CAT_Location.FirstOrDefault(c => c.ID == location.LocationID);
                if (objLocation != null)
                {
                    foreach (var seaport in lstPartner)
                    {
                        var objCATPartLocation = model.CAT_PartnerLocation.FirstOrDefault(c => c.PartnerID == seaport.ID && c.LocationID == location.LocationID);
                        if (objCATPartLocation == null)
                        {
                            objCATPartLocation = new CAT_PartnerLocation();
                            objCATPartLocation.CreatedBy = Account.UserName;
                            objCATPartLocation.CreatedDate = DateTime.Now;
                            objCATPartLocation.LocationID = location.LocationID;
                            objCATPartLocation.PartnerID = seaport.ID;
                            objCATPartLocation.PartnerCode = objLocation.Code;
                            model.CAT_PartnerLocation.Add(objCATPartLocation);
                        }
                    }
                }
            }
            model.SaveChanges();
        }
        #endregion

        #region danh sách địa chỉ( vendor+ xe nhà)
        public static DTOResult CUSLocation_List(DataEntities model, AccountItem Account, int customerid, string request)
        {
            DTOResult result = new DTOResult();
            var query = model.CUS_Location.Where(c => c.CustomerID == customerid).Select(c => new DTOCUSLocationInVEN
            {
                ID = c.ID,
                CUSLocationCode = c.Code,
                CUSLocationName = c.LocationName,
                CATLocationCode = c.CAT_Location.Code,
                CATLocationName = c.CAT_Location.Location,
                Address = c.CAT_Location.Address,
                LocationID = c.LocationID,
                CountryName = c.CAT_Location.CAT_Country.CountryName,
                ProvinceName = c.CAT_Location.CAT_Province.ProvinceName,
                DistrictName = c.CAT_Location.CAT_District.DistrictName,
                RoutingAreaCode = c.RoutingAreaCode,
                RoutingAreaCodeNoUnicode = c.RoutingAreaCodeNoUnicode,
                IsVendorLoad = c.IsVendorLoad,
                IsVendorUnLoad = c.IsVendorUnLoad,
            }).ToDataSourceResult(CreateRequest(request));
            result.Total = query.Total;
            result.Data = query.Data as IEnumerable<DTOCUSLocationInVEN>;
            return result;
        }
        public static void CUSLocation_Delete(DataEntities model, AccountItem Account, int cuslocationid)
        {
            var obj = model.CUS_Location.FirstOrDefault(c => c.ID == cuslocationid);
            if (obj == null)
                throw FaultHelper.BusinessFault(null, null, "Không tìm thấy dữ liệu");
            if (obj.CAT_LocationAreaDetail.Count > 0)
                throw FaultHelper.BusinessFault(null, null, "Có dữ liệu khu vực, không thể xóa");
            if (model.ORD_Container.Count(c => c.LocationDepotID == obj.ID || c.LocationDepotReturnID == obj.ID || c.LocationFromID == obj.ID || c.LocationToID == obj.ID) > 0)
                throw FaultHelper.BusinessFault(null, null, "Có dữ liệu đơn hàng, không thể xóa");
            if (model.ORD_GroupProduct.Count(c => c.LocationToOldID == obj.ID || c.LocationFromID == obj.ID || c.LocationToID == obj.ID) > 0)
                throw FaultHelper.BusinessFault(null, null, "Có dữ liệu đơn hàng, không thể xóa");

            model.CUS_Location.Remove(obj);
            model.SaveChanges();
        }
        public static void CUSLocation_SaveList(DataEntities model, AccountItem Account, int customerid, List<CATLocation> lst)
        {
            foreach (var item in lst)
            {
                var obj = model.CUS_Location.FirstOrDefault(c => c.CustomerID == customerid && c.LocationID == item.ID);
                if (obj == null)
                {
                    obj = new CUS_Location();
                    obj.CreatedBy = Account.UserName;
                    obj.CreatedDate = DateTime.Now;
                    obj.CustomerID = customerid;
                    obj.LocationID = item.ID;
                    obj.Code = item.Code;
                    obj.LocationName = item.Location;
                    model.CUS_Location.Add(obj);
                }
            }
            model.SaveChanges();
        }
        public static DTOResult CUSLocation_NotInList(DataEntities model, AccountItem Account, int customerid, string request)
        {
            DTOResult result = new DTOResult();
            var lst = model.CUS_Location.Where(c => c.CustomerID == customerid).Select(c => c.LocationID).Distinct().ToList();
            var query = model.CAT_Location.Where(c => !string.IsNullOrEmpty(c.Code) && c.ID > 1 && !lst.Contains(c.ID)).Select(c => new CATLocation
            {
                ID = c.ID,
                Code = c.Code,
                Location = c.Location,
                Address = c.Address,
                CountryName = c.CAT_Country.CountryName,
                ProvinceName = c.CAT_Province.ProvinceName,
                DistrictName = c.CAT_District.DistrictName,
            }).ToDataSourceResult(CreateRequest(request));
            result.Total = query.Total;
            result.Data = query.Data as IEnumerable<CATLocation>;
            return result;
        }
        public static SYSExcel CUSLocation_ExcelInit(DataEntities model, AccountItem Account, int functionid, string functionkey, bool isreload, int customerid, int IsBl)
        {
            //IsBl 1: BlVEN, 2:BLFLM
            functionkey = functionkey + customerid;
            var result = default(SYSExcel);
            var id = HelperExcel.GetLastID(model, functionid, functionkey);
            if (id < 1 || isreload == true)
            {
                var lstLocation = model.CUS_Location.Where(c => c.CustomerID == customerid).Select(c => new
                {
                    c.ID,
                    c.LocationID,
                    c.Code,
                    c.LocationName,
                    c.CAT_Location.Address,
                    ProvinceName = c.CAT_Location.ProvinceID > 0 ? c.CAT_Location.CAT_Province.ProvinceName : string.Empty,
                    CountryName = c.CAT_Location.CountryID > 0 ? c.CAT_Location.CAT_Country.CountryName : string.Empty,
                    DistrictName = c.CAT_Location.DistrictID > 0 ? c.CAT_Location.CAT_District.DistrictName : string.Empty,
                    c.RoutingAreaCode,
                    c.RoutingAreaCodeNoUnicode,
                    c.IsVendorLoad,
                    c.IsVendorUnLoad,
                }).ToList();

                List<Worksheet> lstWorkSheet = HelperExcel.GetWorksheetByID(model, id);
                var ws = lstWorkSheet[0];
                ws.Rows.Clear();

                var cells = new List<Cell>();
                int col = 0;
                cells.Add(HelperExcel.NewCell(col++, "STT", HelperExcel.ColorWhite, HelperExcel.ColorGreen));
                cells.Add(HelperExcel.NewCell(col++, "Mã địa điểm", HelperExcel.ColorWhite, HelperExcel.ColorGreen));
                cells.Add(HelperExcel.NewCell(col++, "Tên địa điểm", HelperExcel.ColorWhite, HelperExcel.ColorGreen));
                cells.Add(HelperExcel.NewCell(col++, "Địa chỉ", HelperExcel.ColorWhite, HelperExcel.ColorGreen));
                cells.Add(HelperExcel.NewCell(col++, "Tỉnh thành", HelperExcel.ColorWhite, HelperExcel.ColorGreen));
                cells.Add(HelperExcel.NewCell(col++, "Quận/Huyện", HelperExcel.ColorWhite, HelperExcel.ColorGreen));
                cells.Add(HelperExcel.NewCell(col++, "Mã khu vực", HelperExcel.ColorWhite, HelperExcel.ColorGreen));
                cells.Add(HelperExcel.NewCell(col++, "Mã khu vực không Unicode", HelperExcel.ColorWhite, HelperExcel.ColorGreen));
                if (IsBl == 1)
                {
                    cells.Add(HelperExcel.NewCell(col++, "Bốc xếp lên", HelperExcel.ColorWhite, HelperExcel.ColorGreen));
                    cells.Add(HelperExcel.NewCell(col++, "Bốc xếp xuống", HelperExcel.ColorWhite, HelperExcel.ColorGreen));
                }
                ws.Rows.Add(HelperExcel.NewRow(ws.Rows.Count, cells));

                double[] arrColumnWidth = new double[col];
                for (int i = 0; i < col; i++)
                {
                    arrColumnWidth[i] = 100;
                }
                ws.Columns = HelperExcel.NewColumns(arrColumnWidth);

                int stt = 1;

                foreach (var itemLocation in lstLocation)
                {
                    cells = new List<Cell>();
                    col = 0;
                    cells.Add(HelperExcel.NewCell(col++, stt));
                    cells.Add(HelperExcel.NewCell(col++, itemLocation.Code));
                    cells.Add(HelperExcel.NewCell(col++, itemLocation.LocationName));
                    cells.Add(HelperExcel.NewCell(col++, itemLocation.Address, HelperExcel.ColorBlack, HelperExcel.ColorWhite));
                    cells.Add(HelperExcel.NewCell(col++, itemLocation.ProvinceName, HelperExcel.ColorBlack, HelperExcel.ColorWhite));
                    cells.Add(HelperExcel.NewCell(col++, itemLocation.DistrictName, HelperExcel.ColorBlack, HelperExcel.ColorWhite));
                    cells.Add(HelperExcel.NewCell(col++, itemLocation.RoutingAreaCode));
                    cells.Add(HelperExcel.NewCell(col++, itemLocation.RoutingAreaCodeNoUnicode));
                    if (IsBl == 1)
                    {
                        cells.Add(HelperExcel.NewCell(col++, itemLocation.IsVendorLoad == true ? "X" : ""));
                        cells.Add(HelperExcel.NewCell(col++, itemLocation.IsVendorUnLoad == true ? "X" : ""));
                    }

                    ws.Rows.Add(HelperExcel.NewRow(ws.Rows.Count, cells));
                    stt++;
                }

                result = HelperExcel.GetByKey(model, functionid, functionkey);
                result.Data = Newtonsoft.Json.JsonConvert.SerializeObject(lstWorkSheet);
                result = HelperExcel.Save(model, Account, result);
            }
            else
            {
                result = HelperExcel.GetByID(model, id);
            }

            return result;
        }
        public static Row CUSLocation_ExcelChange(DataEntities model, AccountItem Account, long id, int row, List<Cell> cells, List<string> lstMessageError, int customerid)
        {
            int rowStart = 1;
            int colData = 12;
            int colCheckChange = colData++;
            int colCheckNote = colData++;
            int colCUSLocationID = colData++;
            int failMax = 2;
            int failCurrent = 0;

            var lstCUSLocation = model.CUS_Location.Where(c => c.CustomerID == customerid).Select(c => new { c.ID, c.Code }).ToList();

            var lstWorksheet = HelperExcel.GetWorksheetByID(model, id);
            var ws = lstWorksheet[0];
            var result = default(Row);
            List<string> lstCode = new List<string>();

            var checkRow = ws.Rows.FirstOrDefault(c => c.Index == row);
            if (checkRow == null)
            {
                checkRow = HelperExcel.NewRow(row, cells);
                ws.Rows.Add(checkRow);
            }

            int colDataCUSLocation = 1;
            foreach (var eRow in ws.Rows)
            {
                if (eRow.Index >= rowStart)
                {
                    string strCode = HelperExcel.GetString(eRow, colDataCUSLocation);

                    if (!string.IsNullOrEmpty(strCode))
                    {
                        lstCode.Add(strCode);
                    }
                    else if (failCurrent >= failMax)
                    {
                        break;
                    }
                    else
                        failCurrent++;
                }
            }

            if (checkRow != null)
            {
                checkRow.Cells = cells;

                colData = 1;
                string dataCUSLocation = HelperExcel.GetString(checkRow, colData);
                colData = 7;
                string dataRoutingAreaCode = HelperExcel.GetString(checkRow, colData++);
                string dataRoutingAreaCodeNoUnicode = HelperExcel.GetString(checkRow, colData++);

                bool flag = true;
                int indexError = 0;
                if (flag)
                    flag = HelperExcel.Valid(dataCUSLocation, HelperExcel.ValidType.String, checkRow, colCheckChange, colCheckNote, indexError, lstMessageError, true);
                indexError++;
                if (flag && lstCUSLocation.Count(c => c.Code == dataCUSLocation) == 0)
                {
                    HelperExcel.CheckErrorFail(checkRow, colCheckChange, colCheckNote, HelperExcel.MessageError(indexError, lstMessageError));
                    flag = false;
                }
                indexError++;
                if (flag && lstCode.Count(c => c == dataCUSLocation) > 1)
                {
                    HelperExcel.CheckErrorFail(checkRow, colCheckChange, colCheckNote, HelperExcel.MessageError(indexError, lstMessageError));
                    flag = false;
                }
                if (flag)
                {
                    var valid = "-1";
                    var check = lstCUSLocation.FirstOrDefault(c => c.Code == dataCUSLocation);
                    if (check != null) valid = check.ID.ToString();
                    HelperExcel.CheckErrorSuccess(checkRow, colCheckChange, colCheckNote, colCUSLocationID, valid);
                }
                indexError++;
                if (flag)
                    flag = HelperExcel.Valid(dataRoutingAreaCode, HelperExcel.ValidType.String, checkRow, colCheckChange, colCheckNote, indexError, lstMessageError, false, 1000);
                indexError++;
                if (flag)
                    flag = HelperExcel.Valid(dataRoutingAreaCodeNoUnicode, HelperExcel.ValidType.String, checkRow, colCheckChange, colCheckNote, indexError, lstMessageError, false, 1000);

                HelperExcel.SaveData(model, id, lstWorksheet);
                HelperExcel.ClearData(checkRow, colCheckChange);
                result = checkRow;
            }

            return result;
        }
        public static SYSExcel CUSLocation_ExcelImport(DataEntities model, AccountItem Account, long id, List<Row> lst, List<string> lstMessageError, int customerid)
        {
            int rowStart = 1;
            int colData = 12;
            int colCheckChange = colData++;
            int colCheckNote = colData++;
            int colCUSLocationID = colData++;
            int failMax = 2;
            int failCurrent = 0;
            int rowEnd = lst.Count;

            var lstCUSLocation = model.CUS_Location.Where(c => c.CustomerID == customerid).Select(c => new { c.ID, c.Code }).ToList();
            var lstWorksheet = HelperExcel.GetWorksheetByID(model, id);
            var ws = lstWorksheet[0];
            List<string> lstCode = new List<string>();
            int colDataCUSLocation = 1;
            foreach (var eRow in ws.Rows)
            {
                if (eRow.Index >= rowStart)
                {
                    string strCode = HelperExcel.GetString(eRow, colDataCUSLocation);

                    if (!string.IsNullOrEmpty(strCode))
                    {
                        lstCode.Add(strCode);
                    }
                    else if (failCurrent >= failMax)
                    {
                        break;
                    }
                    else
                        failCurrent++;
                }
            }
            foreach (var checkRow in lst)
            {
                if (checkRow.Index < rowStart) continue;
                if (checkRow.Index >= rowEnd) break;

                colData = 1;
                string dataCUSLocation = HelperExcel.GetString(checkRow, colData);
                colData = 7;
                string dataRoutingAreaCode = HelperExcel.GetString(checkRow, colData++);
                string dataRoutingAreaCodeNoUnicode = HelperExcel.GetString(checkRow, colData++);

                bool flag = true;
                int indexError = 0;
                if (flag)
                    flag = HelperExcel.Valid(dataCUSLocation, HelperExcel.ValidType.String, checkRow, colCheckChange, colCheckNote, indexError, lstMessageError, true);
                indexError++;
                if (flag && lstCUSLocation.Count(c => c.Code == dataCUSLocation) == 0)
                {
                    HelperExcel.CheckErrorFail(checkRow, colCheckChange, colCheckNote, HelperExcel.MessageError(indexError, lstMessageError));
                    flag = false;
                }
                indexError++;
                if (flag && lstCode.Count(c => c == dataCUSLocation) > 1)
                {
                    HelperExcel.CheckErrorFail(checkRow, colCheckChange, colCheckNote, HelperExcel.MessageError(indexError, lstMessageError));
                    flag = false;
                }
                if (flag)
                {
                    var valid = "-1";
                    var check = lstCUSLocation.FirstOrDefault(c => c.Code == dataCUSLocation);
                    if (check != null) valid = check.ID.ToString();
                    HelperExcel.CheckErrorSuccess(checkRow, colCheckChange, colCheckNote, colCUSLocationID, valid);
                }
                indexError++;
                if (flag)
                    flag = HelperExcel.Valid(dataRoutingAreaCode, HelperExcel.ValidType.String, checkRow, colCheckChange, colCheckNote, indexError, lstMessageError, false, 1000);
                indexError++;
                if (flag)
                    flag = HelperExcel.Valid(dataRoutingAreaCodeNoUnicode, HelperExcel.ValidType.String, checkRow, colCheckChange, colCheckNote, indexError, lstMessageError, false, 1000);
            }
            HelperExcel.SaveData(model, id, lstWorksheet);

            return HelperExcel.GetByID(model, id);
        }
        public static bool CUSLocation_ExcelApprove(DataEntities model, AccountItem Account, long id, int customerid, int IsBl)
        {
            //IsBl 1: BlVEN, 2:BLFLM
            int rowStart = 1;
            int colData = 12;
            int colCheckChange = colData++;
            int colCheckNote = colData++;
            int colCUSLocationID = colData++;

            var lstRow = HelperExcel.GetSuccess(model, id, rowStart, colCheckChange, colCheckNote);
            if (lstRow.Count > 0)
            {
                foreach (var checkRow in lstRow)
                {
                    colData = 1;
                    colData = 1;
                    string dataCUSLocation = HelperExcel.GetString(checkRow, colData);
                    colData = 6;
                    string dataRoutingAreaCode = HelperExcel.GetString(checkRow, colData++);
                    string dataRoutingAreaCodeNoUnicode = HelperExcel.GetString(checkRow, colData++);
                    string dataIsVendorLoad = HelperExcel.GetString(checkRow, colData++);
                    string dataIsVendorUnLoad = HelperExcel.GetString(checkRow, colData++);

                    int cuslocationid = Convert.ToInt32(HelperExcel.GetString(checkRow, colCUSLocationID));

                    var objCUS = model.CUS_Location.FirstOrDefault(c => c.ID == cuslocationid);
                    if (objCUS != null)
                    {
                        objCUS.ModifiedBy = Account.UserName;
                        objCUS.ModifiedDate = DateTime.Now;
                        objCUS.RoutingAreaCode = dataRoutingAreaCode;
                        objCUS.RoutingAreaCodeNoUnicode = HelperString.RemoveSpecialCharacters(dataRoutingAreaCodeNoUnicode);
                        if (IsBl == 1)
                        {
                            if (dataIsVendorLoad.ToUpper() == "X")
                                objCUS.IsVendorLoad = true;
                            else objCUS.IsVendorLoad = false;

                            if (dataIsVendorUnLoad.ToUpper() == "X")
                                objCUS.IsVendorUnLoad = true;
                            else objCUS.IsVendorUnLoad = false;
                        }
                    }
                }
                model.SaveChanges();

                return true;
            }
            else
                return false;
        }
        public static DTOResult CUSLocation_HasRun(DataEntities model, AccountItem Account, int customerid, string request)
        {
            DTOResult result = new DTOResult();
            //var query = model.CUS_Location.Where(c => c.CustomerID == customerid).Select(c => new DTOCUSLocationInVEN
            //{
            //    ID = c.ID,
            //    CUSLocationCode = c.Code,
            //    CUSLocationName = c.LocationName,
            //    CATLocationCode = c.CAT_Location.Code,
            //    CATLocationName = c.CAT_Location.Location,
            //    Address = c.CAT_Location.Address,
            //    LocationID = c.LocationID,
            //    CountryName = c.CAT_Location.CAT_Country.CountryName,
            //    ProvinceName = c.CAT_Location.CAT_Province.ProvinceName,
            //    DistrictName = c.CAT_Location.CAT_District.DistrictName,
            //    RoutingAreaCode = c.RoutingAreaCode,
            //    RoutingAreaCodeNoUnicode = c.RoutingAreaCodeNoUnicode
            //}).ToDataSourceResult(CreateRequest(request));
            //result.Total = query.Total;
            //result.Data = query.Data as IEnumerable<DTOCUSLocationInVEN>;
            return result;
        }
        //cung đường -kv
        public static DTOResult RoutingContract_List(DataEntities model, AccountItem Account, string request, int customerid, int locationid)
        {
            DTOResult result = new DTOResult();
            var query = model.CAT_ContractRouting.Where(c => c.CAT_Contract.CustomerID == customerid && c.CAT_Routing.RoutingAreaFromID > 0 && c.CAT_Routing.RoutingAreaToID > 0).Select(c => new DTOCUSPartnerRouting
            {
                CATRoutingID = c.RoutingID,
                CATRoutingCode = c.CAT_Routing.Code,
                CATRoutingName = c.CAT_Routing.RoutingName,
                CUSRoutingID = c.ID,
                CUSRoutingCode = c.Code,
                CUSRoutingName = c.RoutingName,
                ContractID = c.ContractID,
                ContractCode = c.CAT_Contract.ContractNo,
                ContractName = c.CAT_Contract.DisplayName,
                TermID = c.ContractTermID > 0 ? c.ContractTermID.Value : -1,
                TermCode = c.ContractTermID > 0 ? c.CAT_ContractTerm.Code : string.Empty,
                TermName = c.ContractTermID > 0 ? c.CAT_ContractTerm.TermName : string.Empty,
                IsCheckFrom = false,
                IsCheckTo = false,
                AreaFromID = c.CAT_Routing.RoutingAreaFromID > 0 ? c.CAT_Routing.RoutingAreaFromID.Value : -1,
                AreaFromCode = c.CAT_Routing.RoutingAreaFromID > 0 ? c.CAT_Routing.CAT_RoutingArea.Code : string.Empty,
                AreaFromName = c.CAT_Routing.RoutingAreaFromID > 0 ? c.CAT_Routing.CAT_RoutingArea.AreaName : string.Empty,
                AreaToID = c.CAT_Routing.RoutingAreaToID > 0 ? c.CAT_Routing.RoutingAreaToID.Value : -1,
                AreaToCode = c.CAT_Routing.RoutingAreaToID > 0 ? c.CAT_Routing.CAT_RoutingArea1.Code : string.Empty,
                AreaToName = c.CAT_Routing.RoutingAreaToID > 0 ? c.CAT_Routing.CAT_RoutingArea1.AreaName : string.Empty,
            }).ToDataSourceResult(CreateRequest(request));

            var data = query.Data as IEnumerable<DTOCUSPartnerRouting>;

            var lstArea = model.CAT_RoutingAreaLocation.Where(c => c.LocationID == locationid).Select(c => new { c.ID, c.RoutingAreaID, c.LocationID }).ToList();
            foreach (var item in data)
            {
                if (lstArea.Count(c => c.RoutingAreaID == item.AreaFromID) > 0)
                    item.IsCheckFrom = true;
                if (lstArea.Count(c => c.RoutingAreaID == item.AreaToID) > 0)
                    item.IsCheckTo = true;
            }

            result.Total = query.Total;
            result.Data = data;
            return result;
        }
        public static void RoutingContract_SaveList(DataEntities model, AccountItem Account, List<int> lstAreaClear, List<int> lstAreaAdd, int locationid)
        {
            lstAreaClear = lstAreaClear.Distinct().ToList();
            lstAreaAdd = lstAreaAdd.Distinct().ToList();
            //clear kv-diem
            foreach (var item in model.CAT_RoutingAreaLocation.Where(c => lstAreaClear.Contains(c.RoutingAreaID) && c.LocationID == locationid))
            {
                model.CAT_RoutingAreaLocation.Remove(item);
            }
            //add mới
            foreach (var area in lstAreaAdd)
            {
                var obj = new CAT_RoutingAreaLocation();
                obj.CreatedBy = Account.UserName;
                obj.CreatedDate = DateTime.Now;
                obj.RoutingAreaID = area;
                obj.LocationID = locationid;
                model.CAT_RoutingAreaLocation.Add(obj);
            }
            model.SaveChanges();
        }
        public static void RoutingContract_NewRoutingSave(DataEntities model, AccountItem Account, DTOCUSPartnerNewRouting item, int customerid)
        {
            item.RoutingCode = item.RoutingCode.Trim();
            item.RoutingName = item.RoutingName.Trim();

            if (model.CAT_Routing.Count(c => c.Code == item.RoutingCode) > 0)
                throw FaultHelper.BusinessFault(null, null, "Mã cung đường đã tồn tại");
            if (item.AreaToID <= 1 && item.AreaFromID <= 1)
                throw FaultHelper.BusinessFault(null, null, "Khu vực đi, đến không chính xác");

            var objCATRouting = new CAT_Routing();
            objCATRouting.CreatedBy = Account.UserName;
            objCATRouting.CreatedDate = DateTime.Now;
            objCATRouting.Code = item.RoutingCode;
            objCATRouting.RoutingName = item.RoutingName;
            objCATRouting.IsAreaLast = false;
            objCATRouting.IsUse = true;
            objCATRouting.IsLocation = false;
            objCATRouting.IsChecked = false;
            objCATRouting.RoutingAreaFromID = item.AreaFromID;
            objCATRouting.RoutingAreaToID = item.AreaToID;
            model.CAT_Routing.Add(objCATRouting);

            var objCUSRouting = new CUS_Routing();
            objCUSRouting.CreatedBy = Account.UserName;
            objCUSRouting.CreatedDate = DateTime.Now;
            objCUSRouting.Code = item.RoutingCode;
            objCUSRouting.RoutingName = item.RoutingName;
            objCUSRouting.CustomerID = customerid;
            objCUSRouting.CAT_Routing = objCATRouting;
            model.CUS_Routing.Add(objCUSRouting);

            var last = model.CAT_ContractRouting.Where(c => c.ContractID == item.ContractID).OrderByDescending(c => c.SortOrder).Select(c => c.SortOrder).FirstOrDefault();
            if (last == null) last = 0;
            last++;

            var objContracRouting = new CAT_ContractRouting();
            objContracRouting.CreatedBy = Account.UserName;
            objContracRouting.CreatedDate = DateTime.Now;
            objContracRouting.Code = item.RoutingCode;
            objContracRouting.RoutingName = item.RoutingName;
            objContracRouting.ContractID = item.ContractID;
            objContracRouting.CAT_Routing = objCATRouting;
            objContracRouting.SortOrder = last;
            objContracRouting.ContractRoutingTypeID = -(int)SYSVarType.ContractRoutingTypePrice;
            model.CAT_ContractRouting.Add(objContracRouting);

            model.SaveChanges();
        }
        public static DTOCUSPartnerNewRouting RoutingContract_NewRoutingGet(DataEntities model, AccountItem Account, int customerid)
        {
            DTOCUSPartnerNewRouting result = new DTOCUSPartnerNewRouting();
            var contract = model.CAT_Contract.Where(c => c.CustomerID == customerid).FirstOrDefault();
            result.ContractID = -1;
            if (contract != null) result.ContractID = contract.ID;
            result.AreaFromCode_Name = "Chọn khu vực";
            result.AreaToCode_Name = "Chọn khu vực";
            return result;
        }
        public static List<DTOCATContract> RoutingContract_ContractData(DataEntities model, AccountItem Account, int customerid)
        {
            List<DTOCATContract> result = new List<DTOCATContract>();
            result = model.CAT_Contract.Where(c => c.CustomerID == customerid).Select(c => new DTOCATContract
            {
                ID = c.ID,
                ContractNo = c.ContractNo,
                DisplayName = c.DisplayName
            }).ToList();
            return result;
        }
        public static void RoutingContract_NewAreaSave(DataEntities model, AccountItem Account, CATRoutingArea item, int locationid)
        {
            item.Code = item.Code.Trim();
            item.AreaName = item.AreaName.Trim();

            if (model.CAT_RoutingArea.Count(c => c.Code == item.Code) > 0)
                throw FaultHelper.BusinessFault(null, null, "Mã khu vực đã tồn tại");


            var objCATRoutingArea = new CAT_RoutingArea();
            objCATRoutingArea.CreatedBy = Account.UserName;
            objCATRoutingArea.CreatedDate = DateTime.Now;
            objCATRoutingArea.Code = item.Code;
            objCATRoutingArea.AreaName = item.AreaName;
            objCATRoutingArea.CodeNoUnicode = HelperString.RemoveSign4VietnameseString(item.Code);
            model.CAT_RoutingArea.Add(objCATRoutingArea);

            var objCATRoutingAreaLocation = new CAT_RoutingAreaLocation();
            objCATRoutingAreaLocation.CreatedBy = Account.UserName;
            objCATRoutingAreaLocation.CreatedDate = DateTime.Now;
            objCATRoutingAreaLocation.CAT_RoutingArea = objCATRoutingArea;
            objCATRoutingAreaLocation.LocationID = locationid;
            model.CAT_RoutingAreaLocation.Add(objCATRoutingAreaLocation);

            model.SaveChanges();
        }
        public static DTOResult RoutingContract_AreaList(DataEntities model, AccountItem Account, string request)
        {
            DTOResult result = new DTOResult();
            var query = model.CAT_RoutingArea.Select(c => new CATRoutingArea
            {
                ID = c.ID,
                AreaName = c.AreaName,
                Code = c.Code
            }).ToDataSourceResult(CreateRequest(request));

            result.Total = query.Total;
            result.Data = query.Data as IEnumerable<CATRoutingArea>;
            return result;
        }

        #endregion
    }
}
