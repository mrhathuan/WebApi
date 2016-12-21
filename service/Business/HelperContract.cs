using DTO;
using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OfficeOpenXml;
using System.Globalization;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Newtonsoft.Json;

namespace Business
{
    public class HelperContract
    {
        #region contract
        public static int Contract_Save(DataEntities model, AccountItem Account, DTOCATContract item, int TypeOfCustomer)
        {
            try
            {
                if (model.CAT_Contract.Where(c => c.SYSCustomerID == Account.SYSCustomerID && c.ID != item.ID && c.ContractNo == item.ContractNo && c.CustomerID == item.CustomerID).Count() > 0)
                    throw new Exception("Mã đã sử dụng");

                if (item.EffectDate == null || item.ExpiredDate == null)
                    throw new Exception("Ngày hết hạn, ngày hiệu lực không được trống");
                if (item.EffectDate.Date >= item.ExpiredDate.Value.Date)
                    throw new Exception("Ngày hết hạn phải lớn hơn ngày hiệu lực");

                var listTerm = model.CAT_ContractTerm.Where(c => c.ContractID == item.ID);
                if (item.ID > 0 && listTerm.Count() > 0)
                {
                    var start = listTerm.OrderBy(c => c.DateEffect).FirstOrDefault();
                    var end = listTerm.Where(c => c.DateExpire != null).OrderByDescending(c => c.DateExpire.Value).FirstOrDefault();

                    if (start == null || end == null)
                        throw new Exception("Tồn tại phụ lục không có thời gian hiệu lực hoặc thời gian hết hạn");
                    var dateStart = start.DateEffect.Date;
                    var dateEnd = end.DateExpire.Value.Date;

                    if (item.EffectDate.Date > dateStart || item.ExpiredDate.Value.Date < dateEnd)
                        throw new Exception("Thời gian hiệu lực của hợp đồng phải bao trùm thời gian hiệu lực của các phụ lục");
                }

                var obj = model.CAT_Contract.FirstOrDefault(c => c.ID == item.ID);
                if (obj == null)
                {
                    obj = new CAT_Contract();
                    obj.SYSCustomerID = Account.SYSCustomerID;
                    obj.TypeOfCustomerID = TypeOfCustomer;
                    obj.IsCreateNewTerm = false;
                    obj.CreatedBy = Account.UserName;
                    obj.CreatedDate = DateTime.Now;
                    model.CAT_Contract.Add(obj);
                }
                else
                {
                    obj.ModifiedBy = Account.UserName;
                    obj.ModifiedDate = DateTime.Now;
                }
                obj.CustomerID = item.CustomerID;
                obj.ContractNo = item.ContractNo;
                obj.EffectDate = item.EffectDate.Date;
                obj.ExpiredDate = item.ExpiredDate.HasValue ? item.ExpiredDate.Value.Date : item.ExpiredDate;
                obj.SignDate = item.SignDate;
                obj.SignBy = item.SignBy;
                obj.PostionName = item.PostionName;
                obj.Content = item.Content;
                obj.DisplayName = string.IsNullOrEmpty(item.DisplayName) ? string.Empty : item.DisplayName;
                obj.IsSKU = item.IsSKU;
                obj.UseRegion = item.UseRegion;
                obj.UseLoadLocation = item.UseLoadLocation;
                obj.TypeOfContractDateID = item.TypeOfContractDateID;
                obj.CompanyID = item.CompanyID > 0 ? item.CompanyID : null;
                obj.PriceInDay = item.PriceInDay;
                obj.TypeOfContractQuantityID = item.TypeOfContractQuantityID > 0 ? item.TypeOfContractQuantityID : null;
                obj.IsVENLTLLevelOrder = item.IsVENLTLLevelOrder;
                obj.ExprVENTenderLTL = item.ExprVENTenderLTL;
                obj.ExprVENTenderFTL = item.ExprVENTenderFTL;

                obj.ExprVENTenderFTLCompareField = item.ExprVENTenderFTLCompareField;
                obj.ExprVENTenderLTLCompareField = item.ExprVENTenderLTLCompareField;

                obj.AllowCoLoad = item.AllowCoLoad;
                obj.LeadTime = item.LeadTime;
                obj.IsCreateNewTerm = item.IsCreateNewTerm;
                //cap nhat neu thay doi tranport mode
                if (obj.ID > 0 && model.CAT_ContractTerm.Where(c => c.ContractID == item.ID).Count() == 0 && obj.TransportModeID != item.TransportModeID)
                {
                    foreach (var congov in model.CAT_ContractGroupVehicle.Where(c => c.ContractID == obj.ID))
                    {
                        model.CAT_ContractGroupVehicle.Remove(congov);
                    }
                    foreach (var conlv in model.CAT_ContractLevel.Where(c => c.ContractID == obj.ID))
                    {
                        model.CAT_ContractLevel.Remove(conlv);
                    }
                }
                if (obj.ID < 1 || model.CAT_ContractTerm.Where(c => c.ContractID == item.ID).Count() == 0)
                {
                    obj.TypeOfContractID = item.TypeOfContractID;
                    obj.TransportModeID = item.TransportModeID;
                }

                model.SaveChanges();
                return obj.ID;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void Contract_Delete(DataEntities model, AccountItem Account, int contractID)
        {
            try
            {
                if (model.CAT_ContractTerm.Count(c => c.ContractID == contractID) > 0)
                    throw new Exception("Có phụ lục, không thể xóa hợp đồng");

                var lstOrderCode = model.ORD_Order.Where(c => c.ContractID == contractID).Select(c => c.Code).Distinct().ToList();
                if (lstOrderCode.Count > 0)
                    throw new Exception("Hợp đồng đang được sử dụng cho các đơn " + string.Join(", ", lstOrderCode) + ", không thể xóa!");

                var lstMasterCode = model.OPS_DITOMaster.Where(c => c.ContractID == contractID).Select(c => c.Code).Distinct().ToList();
                if (lstMasterCode.Count > 0)
                    throw new Exception("Hợp đồng đang được sử dụng cho các chuyến " + string.Join(", ", lstMasterCode) + ", không thể xóa!");

                var lstOpsGroup = model.OPS_DITOGroupProduct.Where(c => c.DITOMasterID > 0 && (c.VendorLoadContractID == contractID || c.VendorUnLoadContractID == contractID)).Select(c => c.OPS_DITOMaster.Code).Distinct().ToList().Take(10);
                if (lstOpsGroup != null && lstOpsGroup.Count() > 0)
                    throw new Exception("Hợp đồng đang được sử dụng cho các chuyến " + string.Join(", ", lstOpsGroup) + ", không thể xóa!");

                var obj = model.CAT_Contract.FirstOrDefault(c => c.ID == contractID);
                if (obj == null) throw new Exception("Không tìm thấy hợp đồng ID:" + contractID);

                //xóa cung đường trong hợp đồng
                foreach (var temp in model.CAT_ContractRouting.Where(c => c.ContractID == obj.ID))
                    model.CAT_ContractRouting.Remove(temp);

                //xóa contract CO default
                foreach (var temp in model.CAT_ContractCODefault.Where(c => c.ContractID == obj.ID))
                    model.CAT_ContractCODefault.Remove(temp);

                // xóa phụ luc (bảng giá , chi tiết bảng giá)
                foreach (var term in model.CAT_ContractTerm.Where(c => c.ContractID == obj.ID))
                {
                    foreach (var temp in model.CAT_Price.Where(c => c.ContractTermID == term.ID))
                    {
                        //xóa giá container
                        foreach (var temp2 in model.CAT_PriceCOContainer.Where(c => c.PriceID == temp.ID))
                            model.CAT_PriceCOContainer.Remove(temp2);
                        //xóa servivce container
                        foreach (var temp2 in model.CAT_PriceCOService.Where(c => c.PriceID == temp.ID))
                            model.CAT_PriceCOService.Remove(temp2);
                        //xóa phụ thu
                        foreach (var temp2 in model.CAT_PriceDIEx.Where(c => c.PriceID == temp.ID))
                            model.CAT_PriceDIEx.Remove(temp2);
                        //xóa giá ltl thường
                        foreach (var temp2 in model.CAT_PriceDIGroupProduct.Where(c => c.PriceID == temp.ID))
                            model.CAT_PriceDIGroupProduct.Remove(temp2);
                        //xóa bốc xếp
                        foreach (var temp2 in model.CAT_PriceDILoad.Where(c => c.PriceID == temp.ID))
                        {
                            foreach (var temp3 in model.CAT_PriceDILoadDetail.Where(c => c.PriceDILoadID == temp2.ID))
                                model.CAT_PriceDILoadDetail.Remove(temp3);
                            model.CAT_PriceDILoad.Remove(temp2);
                        }
                        //xóa moq bốc xếp
                        foreach (var temp2 in temp.CAT_PriceDIMOQ.Where(c => c.PriceID == temp.ID))
                            model.CAT_PriceDIMOQ.Remove(temp2);
                        //xoa ftl thường
                        foreach (var temp2 in model.CAT_PriceGroupVehicle.Where(c => c.PriceID == temp.ID))
                            model.CAT_PriceGroupVehicle.Remove(temp2);
                        //xoa price routing
                        foreach (var temp2 in model.CAT_PriceRouting.Where(c => c.PriceID == temp.ID))
                        {
                            foreach (var temp3 in model.CAT_PriceRoutingCost.Where(c => c.PriceRoutingID == temp2.ID))
                            {
                                model.CAT_PriceRoutingCost.Remove(temp3);
                            }
                            model.CAT_PriceRouting.Remove(temp2);
                        }
                        model.CAT_Price.Remove(temp);
                    }
                    model.CAT_ContractTerm.Remove(term);
                }

                //xóa thiết lập bậc thang loại xe
                foreach (var level in model.CAT_ContractLevel.Where(c => c.ContractID == obj.ID))
                    model.CAT_ContractLevel.Remove(level);
                foreach (var gov in model.CAT_ContractGroupVehicle.Where(c => c.ContractID == obj.ID))
                    model.CAT_ContractGroupVehicle.Remove(gov);

                // Xóa pl
                foreach (var pl in model.FIN_PL.Where(c => c.ContractID == obj.ID))
                {
                    foreach (var detail in model.FIN_PLDetails.Where(c => c.PLID == pl.ID))
                    {
                        foreach (var gop in model.FIN_PLGroupOfProduct.Where(c => c.PLDetailID == detail.ID))
                        {
                            model.FIN_PLGroupOfProduct.Remove(gop);
                        }
                        model.FIN_PLDetails.Remove(detail);
                    }
                    model.FIN_PL.Remove(pl);
                }

                model.CAT_Contract.Remove(obj);

                model.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //Xóa cung đường trong bảng giá
        public static void CUSContract_Routing_RemoveInPrice(int ContractRoutingID, int ContractID, DataEntities model)
        {
            try
            {
                foreach (var term in model.CAT_ContractTerm.Where(c => c.ContractID == ContractID))
                {
                    foreach (var price in model.CAT_Price.Where(c => c.ContractTermID == term.ID))
                    {
                        #region xoa giá container
                        var con = model.CAT_PriceCOContainer.FirstOrDefault(c => c.PriceID == price.ID && c.ContractRoutingID == ContractRoutingID);
                        if (con != null)
                            model.CAT_PriceCOContainer.Remove(con);
                        #endregion

                        #region xoa phu thu
                        foreach (var service in model.CAT_PriceDIEx.Where(c => c.PriceID == price.ID))
                        {
                            foreach (var group in model.CAT_PriceDIExRouting.Where(c => c.PriceDIExID == service.ID
                                && (c.RoutingID == ContractRoutingID || c.ParentRoutingID == ContractRoutingID)))
                                model.CAT_PriceDIExRouting.Remove(group);
                        }
                        #endregion

                        //xoa bang gia thuong
                        var priceltl = model.CAT_PriceDIGroupProduct.FirstOrDefault(c => c.PriceID == price.ID && c.ContractRoutingID == ContractRoutingID);
                        if (priceltl != null)
                            model.CAT_PriceDIGroupProduct.Remove(priceltl);


                        var priceftl = model.CAT_PriceGroupVehicle.FirstOrDefault(c => c.PriceID == price.ID);
                        if (priceftl != null)
                            model.CAT_PriceGroupVehicle.Remove(priceftl);

                        //xoa bang gia bac thang
                        var detailltl = model.CAT_PriceDILevelGroupProduct.FirstOrDefault(c => c.PriceID == price.ID);
                        if (detailltl != null)
                            model.CAT_PriceDILevelGroupProduct.Remove(detailltl);

                        var detailftl = model.CAT_PriceGVLevelGroupVehicle.FirstOrDefault(c => c.PriceID == price.ID);
                        if (detailftl != null)
                            model.CAT_PriceGVLevelGroupVehicle.Remove(detailftl);

                        //xoa moq
                        foreach (var moq in model.CAT_PriceDIMOQ.Where(c => c.PriceID == price.ID))
                        {
                            foreach (var detail in model.CAT_PriceDIMOQRouting.Where(c => c.PriceDIMOQID == moq.ID
                                && (c.RoutingID == ContractRoutingID || c.ParentRoutingID == ContractRoutingID)))
                                model.CAT_PriceDIMOQRouting.Remove(detail);
                        }

                        //xóa boc xếp
                        foreach (var load in model.CAT_PriceDILoad.Where(c => c.PriceID == price.ID
                             && (c.RoutingID == ContractRoutingID || c.ParentRoutingID == ContractRoutingID)))
                        {
                            foreach (var detail in model.CAT_PriceDILoadDetail.Where(c => c.PriceDILoadID == load.ID))
                                model.CAT_PriceDILoadDetail.Remove(detail);

                            model.CAT_PriceDILoad.Remove(load);
                        }

                        //xoa moq boc xep
                        foreach (var moq in model.CAT_PriceDIMOQLoad.Where(c => c.PriceID == price.ID))
                        {
                            foreach (var detail in model.CAT_PriceDIMOQLoadRouting.Where(c => c.PriceDIMOQLoadID == moq.ID
                                && (c.RoutingID == ContractRoutingID || c.ParentRoutingID == ContractRoutingID)))
                                model.CAT_PriceDIMOQLoadRouting.Remove(detail);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Term
        public static DTOResult Term_List(DataEntities model, AccountItem Account, string request, int contractID)
        {
            DTOResult result = new DTOResult();
            var query = model.CAT_ContractTerm.Where(c => c.ContractID == contractID).Select(c => new DTOContractTerm
            {
                ID = c.ID,
                ContractID = c.ContractID,
                Code = c.Code,
                TermName = c.TermName,
                DisplayName = c.DisplayName,
                Note = c.Note,
                MaterialID = c.MaterialID > 0 ? c.MaterialID.Value : -1,
                MaterialCode = c.MaterialID > 0 ? c.FLM_Material.Code : string.Empty,
                MaterialName = c.MaterialID > 0 ? c.FLM_Material.MaterialName : string.Empty,
                PriceContract = c.PriceContract > 0 ? c.PriceContract.Value : 0,
                PriceCurrent = c.PriceCurrent > 0 ? c.PriceCurrent.Value : 0,
                PriceWarning = c.PriceWarning > 0 ? c.PriceWarning.Value : 0,
                DateEffect = c.DateEffect,
                DateExpire = c.DateExpire,
                DatePrice = c.DatePrice,
                ExprDatePrice = c.ExprDatePrice,
                ExprInput = c.ExprInput,
                ExprPrice = c.ExprPrice,
                DateWarning = c.DateWarning,
                IsAllRouting = c.IsAllRouting,
                IsWarning = c.IsWarning,
                IsClosed = c.IsClosed,
                ServiceOfOrderID = c.ServiceOfOrderID,
                ServiceOfOrderName = c.ServiceOfOrderID > 0 ? c.CAT_ServiceOfOrder.Name : "",
            }).ToDataSourceResult(CreateRequest(request));

            var list = query.Data.Cast<DTOContractTerm>().ToList();

            foreach (var item in list)
            {
                item.RateMaterial = 0;
                item.RatePrice = 0;

                if (item.PriceContract > 0 && item.PriceWarning > 0 && item.MaterialID > 0 && !string.IsNullOrEmpty(item.ExprPrice))
                {
                    item.RateMaterial = Math.Round((double)((item.PriceCurrent - item.PriceContract) / item.PriceContract) * 100, 2);
                    var itemCheck = new DTOMaterialChecking
                    {
                        PriceContract = item.PriceContract,
                        PriceCurrent = item.PriceCurrent,
                        PriceWarning = item.PriceWarning,
                        Price = 100,
                    };
                    var package = Term_Change_PricePacket(itemCheck, item.ExprPrice);
                    if (package != null)
                    {
                        var price = Term_Change_PriceCal(package, itemCheck);
                        if (price != null)
                        {
                            item.RatePrice = Math.Round((double)((price - itemCheck.Price) / itemCheck.Price) * 100, 2);
                        }
                    }
                }
            }

            result.Total = query.Total;
            result.Data = list;

            return result;
        }

        public static DTOContractTerm Term_Get(DataEntities model, AccountItem Account, int id, int contractID)
        {
            int iFCL = -(int)SYSVarType.TransportModeFCL;
            int iFTL = -(int)SYSVarType.TransportModeFTL;
            int iLTL = -(int)SYSVarType.TransportModeLTL;
            var objContract = model.CAT_Contract.FirstOrDefault(c => c.ID == contractID);
            if (objContract == null)
            {
                throw new Exception("Không tìm thấy hợp đồng.");
            }

            DTOContractTerm result = new DTOContractTerm();
            if (id > 0)
            {
                result = model.CAT_ContractTerm.Where(c => c.ID == id).Select(c => new DTOContractTerm
                {
                    ID = c.ID,
                    ContractID = c.ContractID,
                    Code = c.Code,
                    TermName = c.TermName,
                    DisplayName = c.DisplayName,
                    Note = c.Note,
                    MaterialID = c.MaterialID > 0 ? c.MaterialID.Value : -1,
                    MaterialCode = c.MaterialID > 0 ? c.FLM_Material.Code : string.Empty,
                    MaterialName = c.MaterialID > 0 ? c.FLM_Material.MaterialName : string.Empty,
                    PriceContract = c.PriceContract > 0 ? c.PriceContract.Value : 0,
                    PriceCurrent = c.PriceCurrent > 0 ? c.PriceCurrent.Value : 0,
                    PriceWarning = c.PriceWarning > 0 ? c.PriceWarning.Value : 0,
                    DateEffect = c.DateEffect,
                    DateExpire = c.DateExpire,
                    DatePrice = c.DatePrice,
                    ExprDatePrice = c.ExprDatePrice,
                    ExprInput = c.ExprInput,
                    ExprPrice = c.ExprPrice,
                    DateWarning = c.DateWarning,
                    IsAllRouting = c.IsAllRouting,
                    IsWarning = c.IsWarning,
                    IsEditAllRouting = c.CAT_Price.Count() > 0 ? false : true,
                    IsClosed = c.IsClosed,
                    ServiceOfOrderID = c.ServiceOfOrderID,
                    ServiceOfOrderName = c.ServiceOfOrderID.HasValue ? c.CAT_ServiceOfOrder.Name : string.Empty,
                }).FirstOrDefault();

                if (result != null)
                {
                    result.RateMaterial = 0;
                    result.RatePrice = 0;

                    if (result.PriceContract > 0 && result.PriceWarning > 0 && result.MaterialID > 0 && !string.IsNullOrEmpty(result.ExprPrice))
                    {
                        result.RateMaterial = Math.Round((double)((result.PriceCurrent - result.PriceContract) / result.PriceContract) * 100, 2);
                        var itemCheck = new DTOMaterialChecking
                        {
                            PriceContract = result.PriceContract,
                            PriceCurrent = result.PriceCurrent,
                            PriceWarning = result.PriceWarning,
                            Price = 100,
                        };
                        var package = Term_Change_PricePacket(itemCheck, result.ExprPrice);
                        if (package != null)
                        {
                            var price = Term_Change_PriceCal(package, itemCheck);
                            if (price != null)
                            {
                                result.RatePrice = Math.Round((double)((price - itemCheck.Price) / itemCheck.Price) * 100, 2);
                            }
                        }
                    }
                }
            }
            else
            {
                result.ID = -1;
                result.MaterialID = -1;
                result.DateEffect = DateTime.Now.Date;
                result.DateExpire = DateTime.Now.Date.AddDays(1);
                result.IsEditAllRouting = true;
                result.ServiceOfOrderID = -1;
            }
            result.TypeOfMode = objContract.CAT_TransportMode.TransportModeID == iFCL ? 1 : objContract.CAT_TransportMode.TransportModeID == iFTL ? 2 : objContract.CAT_TransportMode.TransportModeID == iLTL ? 3 : 0;
            return result;
        }

        public static int Term_Save(DataEntities model, AccountItem Account, DTOContractTerm item, int contractID)
        {
            try
            {
                int iFCL = -(int)SYSVarType.TransportModeFCL;

                var objContract = model.CAT_Contract.FirstOrDefault(c => c.ID == contractID);
                if (objContract == null) throw new Exception("Không tìm thấy hợp đồng ID:" + contractID);

                if (item.DateEffect == null || item.DateExpire == null)
                    throw new Exception("Ngày hết hạn, ngày hiệu lực không được trống");

                if (item.DateEffect.Date > item.DateExpire.Value.Date)
                    throw new Exception("Ngày hết hạn phải lớn hơn ngày hiệu lực");
                if (item.DateEffect.Date < objContract.EffectDate.Date || item.DateExpire.Value.Date > objContract.ExpiredDate.Value.Date)
                    throw new Exception("Thời gian hiệu lực của phụ lục không được nằm ngoài thời gian hiệu lực của hợp đồng");

                if (model.CAT_ContractTerm.Count(c => c.Code == item.Code && c.ContractID == contractID && c.ID != item.ID) > 0)
                    throw new Exception("Mã đã sử dụng");

                var obj = model.CAT_ContractTerm.FirstOrDefault(c => c.ID == item.ID);

                if (obj != null && obj.CAT_Price.Count() > 0 && obj.IsAllRouting != item.IsAllRouting)
                    throw new Exception("Phụ lục đã có bảng giá, không được thay đổi cách tính giá");
                if (obj != null && obj.CAT_Price.Count() > 0 && obj.ServiceOfOrderID != item.ServiceOfOrderID && objContract.TransportModeID == iFCL && item.ServiceOfOrderID > 0)
                    throw new Exception("Phụ lục đã có bảng giá, không được thay đổi loại dịch vụ");

                if (obj == null)
                {
                    obj = new CAT_ContractTerm();
                    obj.CreatedBy = Account.UserName;
                    obj.CreatedDate = DateTime.Now;
                    obj.ContractID = contractID;
                    if (item.TypeOfMode == 2)
                        obj.IsClosed = item.IsClosed;
                    else
                        obj.IsClosed = false;
                    model.CAT_ContractTerm.Add(obj);
                }
                else
                {
                    obj.ModifiedBy = Account.UserName;
                    obj.ModifiedDate = DateTime.Now;
                }
                obj.Code = item.Code;
                obj.TermName = item.TermName;
                obj.DisplayName = item.DisplayName;
                obj.Note = item.Note;
                obj.DateEffect = item.DateEffect;
                obj.DateExpire = item.DateExpire;
                obj.SortConfigDateStart = item.SortConfigDateStart;
                obj.DatePrice = item.DatePrice;
                obj.DEM = item.DEM;
                obj.DET = item.DET;
                obj.ExprETA = item.ExprETA;
                obj.ExprETARequest = item.ExprETARequest;
                obj.ExprETD = item.ExprETD;
                obj.ExprETDRequest = item.ExprETDRequest;
                if (!(obj.CAT_Price.Count() > 0))
                {
                    obj.IsAllRouting = item.IsAllRouting;
                    if(objContract.CAT_TransportMode.TransportModeID != iFCL)
                    {
                        obj.ServiceOfOrderID = null;
                    }
                    else
                        obj.ServiceOfOrderID = item.ServiceOfOrderID > 0 ? item.ServiceOfOrderID : null;
                }

                if (item.MaterialID > 0)
                {
                    obj.MaterialID = item.MaterialID;
                    obj.PriceContract = item.PriceContract;
                    obj.PriceCurrent = item.PriceCurrent;
                    obj.PriceWarning = item.PriceWarning;

                    obj.ExprDatePrice = item.ExprDatePrice;
                    obj.ExprInput = item.ExprInput;
                    obj.ExprPrice = item.ExprPrice;
                    obj.DateWarning = item.DateWarning;
                }
                else
                {
                    obj.MaterialID = null;

                    obj.PriceContract = null;
                    obj.PriceCurrent = null;
                    obj.PriceWarning = null;

                    obj.ExprDatePrice = null;
                    obj.ExprInput = null;
                    obj.ExprPrice = null;
                    obj.DateWarning = null;
                }

                model.SaveChanges();
                return obj.ID;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void Term_Delete(DataEntities model, AccountItem Account, int termID)
        {
            try
            {
                var lstOrder = model.ORD_Order.Where(c => c.ContractTermID == termID).Select(c => c.Code).Take(100);

                if (lstOrder != null && lstOrder.Count() > 0)
                {
                    var str = string.Join(",", lstOrder.ToList());
                    throw new Exception("Có đơn hàng, không thể xóa phụ lục (Các mã đơn hàng : " + str + ")");
                }

                if (model.CAT_Price.Count(c => c.ContractTermID == termID) > 0)
                    throw new Exception("Có bảng giá, không thể xóa phụ lục");

                var objTerm = model.CAT_ContractTerm.FirstOrDefault(c => c.ID == termID);
                if (objTerm == null) throw new Exception("Không tìm thấy phụ lục ID:" + termID);

                foreach (var price in model.CAT_Price.Where(c => c.ContractTermID == termID))
                {
                    //xoa service và giá container
                    foreach (var service in model.CAT_PriceCOService.Where(c => c.PriceID == price.ID))
                        model.CAT_PriceCOService.Remove(service);
                    foreach (var con in model.CAT_PriceCOContainer.Where(c => c.PriceID == price.ID))
                        model.CAT_PriceCOContainer.Remove(con);

                    //xoa phu thu
                    foreach (var service in model.CAT_PriceDIEx.Where(c => c.PriceID == price.ID))
                    {
                        foreach (var group in model.CAT_PriceDIExGroupLocation.Where(c => c.PriceDIExID == service.ID))
                            model.CAT_PriceDIExGroupLocation.Remove(group);
                        foreach (var gop in model.CAT_PriceDIExGroupProduct.Where(c => c.PriceDIExID == service.ID))
                            model.CAT_PriceDIExGroupProduct.Remove(gop);
                        foreach (var group in model.CAT_PriceDIExRouting.Where(c => c.PriceDIExID == service.ID))
                            model.CAT_PriceDIExRouting.Remove(group);

                        model.CAT_PriceDIEx.Remove(service);
                    }

                    //xoa bang gia thuong
                    foreach (var priceltl in model.CAT_PriceDIGroupProduct.Where(c => c.PriceID == price.ID))
                        model.CAT_PriceDIGroupProduct.Remove(priceltl);
                    foreach (var priceftl in model.CAT_PriceGroupVehicle.Where(c => c.PriceID == price.ID))
                        model.CAT_PriceGroupVehicle.Remove(priceftl);

                    //xoa bang gia bac thang
                    foreach (var detail in model.CAT_PriceDILevelGroupProduct.Where(c => c.PriceID == price.ID))
                        model.CAT_PriceDILevelGroupProduct.Remove(detail);
                    foreach (var detail in model.CAT_PriceGVLevelGroupVehicle.Where(c => c.PriceID == price.ID))
                        model.CAT_PriceGVLevelGroupVehicle.Remove(detail);

                    //xoa moq
                    foreach (var moq in model.CAT_PriceDIMOQ.Where(c => c.PriceID == price.ID))
                    {
                        foreach (var detail in model.CAT_PriceDIMOQGroupLocation.Where(c => c.PriceDIMOQID == moq.ID))
                            model.CAT_PriceDIMOQGroupLocation.Remove(detail);
                        foreach (var detail in model.CAT_PriceDIMOQGroupProduct.Where(c => c.PriceDIMOQID == moq.ID))
                            model.CAT_PriceDIMOQGroupProduct.Remove(detail);
                        foreach (var detail in model.CAT_PriceDIMOQRouting.Where(c => c.PriceDIMOQID == moq.ID))
                            model.CAT_PriceDIMOQRouting.Remove(detail);
                        model.CAT_PriceDIMOQ.Remove(moq);
                    }

                    //xóa boc xếp
                    foreach (var load in model.CAT_PriceDILoad.Where(c => c.PriceID == price.ID))
                    {
                        foreach (var detail in model.CAT_PriceDILoadDetail.Where(c => c.PriceDILoadID == load.ID))
                            model.CAT_PriceDILoadDetail.Remove(detail);

                        model.CAT_PriceDILoad.Remove(load);
                    }

                    //xoa moq boc xep
                    foreach (var moq in model.CAT_PriceDIMOQLoad.Where(c => c.PriceID == price.ID))
                    {
                        foreach (var detail in model.CAT_PriceDIMOQLoadGroupLocation.Where(c => c.PriceDIMOQLoadID == moq.ID))
                            model.CAT_PriceDIMOQLoadGroupLocation.Remove(detail);
                        foreach (var detail in model.CAT_PriceDIMOQLoadGroupProduct.Where(c => c.PriceDIMOQLoadID == moq.ID))
                            model.CAT_PriceDIMOQLoadGroupProduct.Remove(detail);
                        foreach (var detail in model.CAT_PriceDIMOQLoadRouting.Where(c => c.PriceDIMOQLoadID == moq.ID))
                            model.CAT_PriceDIMOQLoadRouting.Remove(detail);
                        model.CAT_PriceDIMOQLoad.Remove(moq);
                    }

                    model.CAT_Price.Remove(price);
                }

                //reset contract route
                foreach (var route in model.CAT_ContractRouting.Where(c => c.ContractTermID == termID && c.ContractTermID > 0))
                {
                    route.ContractTermID = null;
                }

                model.CAT_ContractTerm.Remove(objTerm);
                model.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void Term_Close(DataEntities model, AccountItem Account, int termID)
        {
            var obj = model.CAT_ContractTerm.FirstOrDefault(c => c.ID == termID);
            if (obj != null && obj.IsClosed == false)
            {
                foreach (var item in model.CAT_ContractRouting.Where(c => c.ContractTermID == obj.ID))
                {
                    item.ContractTermID = null;
                    item.ModifiedBy = Account.UserName;
                    item.ModifiedDate = DateTime.Now;
                }
                obj.IsClosed = true;
                model.SaveChanges();
            }
        }

        public static void Term_Open(DataEntities model, AccountItem Account, int termID)
        {
            var obj = model.CAT_ContractTerm.FirstOrDefault(c => c.ID == termID);
            if (obj != null && obj.IsClosed == true)
            {
                var lstRoutingID = model.CAT_PriceDIGroupProduct.Where(c => c.CAT_Price.ContractTermID == obj.ID).Select(c => c.ContractRoutingID).Distinct().ToList();
                lstRoutingID.AddRange(model.CAT_PriceDILevelGroupProduct.Where(c => c.CAT_Price.ContractTermID == obj.ID).Select(c => c.ContractRoutingID).Distinct().ToList());
                lstRoutingID.AddRange(model.CAT_PriceGroupVehicle.Where(c => c.CAT_Price.ContractTermID == obj.ID).Select(c => c.ContractRoutingID).Distinct().ToList());
                lstRoutingID.AddRange(model.CAT_PriceGVLevelGroupVehicle.Where(c => c.CAT_Price.ContractTermID == obj.ID).Select(c => c.ContractRoutingID).Distinct().ToList());
                var first = model.CAT_ContractRouting.Where(c => lstRoutingID.Contains(c.ID) && c.ContractTermID > 0 && c.ContractTermID != termID).FirstOrDefault();
                if (first != null)
                {
                    throw new Exception("Cung đường (" + first.Code + ")" + first.RoutingName + " đã dùng cho phụ lục khác");
                }
                else
                {
                    foreach (var item in model.CAT_ContractRouting.Where(c => c.ContractID == obj.ContractID && lstRoutingID.Contains(c.ID)))
                    {
                        item.ContractTermID = obj.ID;
                        item.ModifiedBy = Account.UserName;
                        item.ModifiedDate = DateTime.Now;
                    }
                    obj.IsClosed = false;
                    model.SaveChanges();
                }
            }
        }

        public static DTOCUSPrice_MaterialData Term_Change_Data(DataEntities model, AccountItem Account, int termID)
        {
            DTOCUSPrice_MaterialData result = new DTOCUSPrice_MaterialData();
            result.PriceChange = new DTOCUSPriceMaterial();
            result.PriceCurrent = new DTOCUSPriceMaterial();
            result.TermInfo = new DTOContractTerm();

            try
            {
                var objTerm = model.CAT_ContractTerm.Where(c => c.ID == termID && c.CAT_Contract.CustomerID > 0 && c.MaterialID > 0).Select(c => new
                {
                    TermID = c.ID,
                    TermCode = c.Code,
                    TermName = c.TermName,
                    c.ContractID,
                    ContractCode = c.CAT_Contract.ContractNo,
                    ContractName = c.CAT_Contract.DisplayName,
                    c.CAT_Contract.TransportModeID,
                    c.CAT_Contract.TypeOfContractID,
                    c.CAT_Contract.TypeOfRunLevelID,
                    CustomerID = c.CAT_Contract.CustomerID.Value,
                    CustomerCode = c.CAT_Contract.CUS_Customer.Code,
                    CustomerName = c.CAT_Contract.CUS_Customer.CustomerName,
                    MaterialID = c.MaterialID.Value,
                    MaterialCode = c.FLM_Material.Code,
                    MaterialName = c.FLM_Material.MaterialName,
                    PriceContract = c.PriceContract > 0 ? c.PriceContract.Value : 0,
                    PriceCurrent = c.PriceCurrent > 0 ? c.PriceCurrent.Value : 0,
                    PriceWarning = c.PriceWarning > 0 ? c.PriceWarning.Value : 0,
                    c.ExprInput,
                    c.ExprPrice,
                    c.ExprDatePrice,
                    c.DatePrice,
                    c.DateWarning,
                    c.IsWarning,
                    TransportModeIDTemp = c.CAT_Contract.TransportModeID > 0 ? c.CAT_Contract.CAT_TransportMode.TransportModeID : -1,
                }).FirstOrDefault();
                if (objTerm == null)
                    throw FaultHelper.BusinessFault(null, null, "Không tìm thấy phụ lục");

                var listGOPMapping = new List<int>();
                var objContract = model.CAT_Contract.FirstOrDefault(c => c.ID == objTerm.ContractID);
                if (objContract.CompanyID > 0)
                {
                    var CustomerRelateID = objContract.CUS_Company.CustomerRelateID;
                    listGOPMapping = model.CUS_GroupOfProductMapping.Where(c => c.VendorID == objContract.CustomerID && c.CustomerID == CustomerRelateID).Select(c => c.GroupOfProductVENID).ToList();
                }
                // result.CustomerID = objTerm.CustomerID;

                int iHot = -(int)SYSVarType.TypeOfOrderHot;
                int iDirect = -(int)SYSVarType.TypeOfOrderDirect;
                int iReturn = -(int)SYSVarType.TypeOfOrderReturn;
                int iReturnHot = -(int)SYSVarType.TypeOfOrderReturnHot;

                int iFCL = -(int)SYSVarType.TransportModeFCL;
                int iFTL = -(int)SYSVarType.TransportModeFTL;
                int iLTL = -(int)SYSVarType.TransportModeLTL;

                int iMain = -(int)SYSVarType.TypeOfContractMain;
                int iFrame = -(int)SYSVarType.TypeOfContractFrame;

                //var ListRouting = model.CAT_ContractRouting.Where(c => c.ContractTermID == objTerm.TermID).Select(c => new DTOCATContractRouting
                //{
                //    ID = c.ID,
                //    Code = c.Code,
                //    RoutingName = c.RoutingName,
                //    SortOrder = c.SortOrder
                //}).OrderBy(c => c.SortOrder).ToList();

                //var ListMaterialRoute = model.CAT_ContractMaterialRouting.Where(c => c.ContractMaterialID == contractMaterialID).Select(c => new DTOCATContractRouting
                //{
                //    ID = c.ID,
                //    Code = c.CAT_ContractRouting.Code,
                //    RoutingName = c.CAT_ContractRouting.RoutingName,
                //    SortOrder = c.CAT_ContractRouting.SortOrder
                //}).OrderBy(c => c.SortOrder).ToList();

                var objPrice = model.CAT_Price.Where(c => c.ContractTermID == objTerm.TermID).OrderByDescending(c => c.EffectDate).FirstOrDefault();
                if (objPrice != null)
                {
                    result.TermInfo = new DTOContractTerm();
                    result.PriceCurrent.ItemPrice = new DTOPrice();
                    result.PriceChange.ItemPrice = new DTOPrice();

                    var itemCheck = new DTOMaterialChecking();
                    itemCheck.DateWarning = objTerm.DateWarning;
                    itemCheck.PriceContract = objTerm.PriceContract;
                    itemCheck.PriceWarning = objTerm.PriceWarning;
                    DateTime? priceDate = null;
                    try
                    {
                        if (!string.IsNullOrEmpty(objTerm.ExprDatePrice))
                            priceDate = Term_Change_PriceDate(itemCheck, objTerm.ExprDatePrice);
                    }
                    catch { }
                    result.TermInfo.ID = objTerm.TermID;
                    result.TermInfo.ContractID = objTerm.ContractID;
                    result.TermInfo.MaterialID = objTerm.MaterialID;
                    result.TermInfo.MaterialCode = objTerm.MaterialCode;
                    result.TermInfo.MaterialName = objTerm.MaterialName;
                    result.TermInfo.PriceContract = objTerm.PriceContract;
                    result.TermInfo.PriceCurrent = objTerm.PriceCurrent;
                    result.TermInfo.ExprInput = objTerm.ExprInput;
                    result.TermInfo.ExprPrice = objTerm.ExprPrice;
                    result.TermInfo.IsWarning = objTerm.IsWarning;
                    result.TermInfo.DatePrice = priceDate;
                    result.TermInfo.DateWarning = objTerm.DateWarning;
                    result.TermInfo.ExprDatePrice = objTerm.ExprDatePrice;
                    result.TermInfo.PriceWarning = objTerm.PriceWarning;

                    result.PriceCurrent.ItemPrice.ID = objPrice.ID;
                    result.PriceCurrent.ItemPrice.ContractTermID = objPrice.ID;
                    result.PriceCurrent.ItemPrice.Code = objPrice.Code;
                    result.PriceCurrent.ItemPrice.Name = objPrice.Name;
                    result.PriceCurrent.ItemPrice.EffectDate = objPrice.EffectDate;
                    result.PriceCurrent.ItemPrice.TypeOfOrderID = objPrice.TypeOfOrderID;
                    result.PriceCurrent.ItemPrice.ContractNo = objTerm.ContractCode;
                    result.PriceCurrent.ItemPrice.TypeOfOrderName = objPrice.SYS_Var.ValueOfVar;
                    result.PriceCurrent.ItemPrice.TypeOfPrice = objPrice.TypeOfOrderID == iDirect ? 1 : objPrice.TypeOfOrderID == iHot ? 2 : objPrice.TypeOfOrderID == iReturn ? 3 : objPrice.TypeOfOrderID == iReturnHot ? 4 : 0;
                    result.PriceCurrent.ItemPrice.TypeOfMode = objTerm.TransportModeIDTemp == iFCL ? 1 : objTerm.TransportModeIDTemp == iFTL ? 2 : objTerm.TransportModeIDTemp == iLTL ? 3 : 0;
                    result.PriceCurrent.ItemPrice.TypeOfContract = objTerm.TypeOfContractID == iMain ? 1 : objTerm.TypeOfContractID == iFrame ? 2 : 0;


                    result.PriceCurrent.ItemPrice.CheckPrice = new DTOPriceCheck();

                    result.PriceCurrent.ItemPrice.CheckPrice.HasNormal = objTerm.TypeOfRunLevelID == null;
                    result.PriceCurrent.ItemPrice.CheckPrice.HasLevel = objTerm.TypeOfRunLevelID > 0;

                    result.PriceChange.ItemPrice.ID = objPrice.ID;
                    result.PriceChange.ItemPrice.ContractTermID = objPrice.ID;
                    result.PriceChange.ItemPrice.Code = objPrice.Code + DateTime.Now.ToString("ddMMyyyy");
                    result.PriceChange.ItemPrice.Name = objPrice.Name + DateTime.Now.ToString("ddMMyyyy");
                    result.PriceChange.ItemPrice.EffectDate = priceDate.Value;
                    result.PriceChange.ItemPrice.TypeOfOrderID = objPrice.TypeOfOrderID;
                    result.PriceChange.ItemPrice.ContractNo = objTerm.ContractCode;
                    result.PriceChange.ItemPrice.TypeOfOrderName = objPrice.SYS_Var.ValueOfVar;
                    result.PriceChange.ItemPrice.TypeOfPrice = objPrice.TypeOfOrderID == iDirect ? 1 : objPrice.TypeOfOrderID == iHot ? 2 : objPrice.TypeOfOrderID == iReturn ? 3 : objPrice.TypeOfOrderID == iReturnHot ? 4 : 0;
                    result.PriceChange.ItemPrice.TypeOfMode = objTerm.TransportModeIDTemp == iFCL ? 1 : objTerm.TransportModeIDTemp == iFTL ? 2 : objTerm.TransportModeIDTemp == iLTL ? 3 : 0;
                    result.PriceChange.ItemPrice.TypeOfContract = objTerm.TypeOfContractID == iMain ? 1 : objTerm.TypeOfContractID == iFrame ? 2 : 0;

                    result.PriceCurrent.ListRouting = model.CAT_ContractRouting.Where(c => c.ContractTermID == objTerm.TermID).Select(c => new DTOCATContractRouting
                    {
                        ID = c.ID,
                        Code = c.Code,
                        RoutingName = c.RoutingName,
                        SortOrder = c.SortOrder
                    }).OrderBy(c => c.SortOrder).ToList();

                    result.PriceChange.ListRouting = model.CAT_ContractRouting.Where(c => c.ContractTermID == objTerm.TermID).Select(c => new DTOCATContractRouting
                    {
                        ID = c.ID,
                        Code = c.Code,
                        RoutingName = c.RoutingName,
                        SortOrder = c.SortOrder
                    }).OrderBy(c => c.SortOrder).ToList();

                    var listRouteIDChange = result.PriceChange.ListRouting.Select(c => c.ID).ToList();

                    var listGOP = model.CUS_GroupOfProduct.Where(c => c.CustomerID == objTerm.CustomerID && (objContract.CompanyID == null ? true : listGOPMapping.Contains(c.ID))).Select(c => new DTOCUSGroupOfProduct
                    {
                        ID = c.ID,
                        Code = c.Code,
                        GroupName = c.GroupName,
                        PriceOfGOPID = c.PriceOfGOPID,
                        PriceOfGOPName = c.SYS_Var.ValueOfVar
                    }).ToList();
                    result.PriceCurrent.ListGroupOfProduct = new List<DTOCUSGroupOfProduct>();
                    result.PriceCurrent.ListGroupOfProduct.AddRange(listGOP);
                    result.PriceChange.ListGroupOfProduct = new List<DTOCUSGroupOfProduct>();
                    result.PriceChange.ListGroupOfProduct.AddRange(listGOP);

                    #region flt normal
                    result.PriceCurrent.FTLNormal = new DTOPriceGroupVehicleData();
                    result.PriceCurrent.FTLNormal.ListDetail = new List<DTOPriceGroupVehicle>();
                    result.PriceCurrent.FTLNormal.ListGOV = new List<CATGroupOfVehicle>();
                    result.PriceCurrent.FTLNormal.ListRoute = new List<DTOCATRouting>();
                    result.PriceChange.FTLNormal = new DTOPriceGroupVehicleData();
                    result.PriceChange.FTLNormal.ListDetail = new List<DTOPriceGroupVehicle>();
                    result.PriceChange.FTLNormal.ListGOV = new List<CATGroupOfVehicle>();
                    result.PriceChange.FTLNormal.ListRoute = new List<DTOCATRouting>();


                    var ListDetailCurrent = model.CAT_PriceGroupVehicle.Where(c => c.PriceID == objPrice.ID && listRouteIDChange.Contains(c.ContractRoutingID)).Select(c => new DTOPriceGroupVehicle
                    {
                        RouteID = c.ContractRoutingID,
                        GroupOfVehicleID = c.GroupOfVehicleID,
                        Price = c.Price,
                        PriceMax = c.PriceMax,
                        PriceMin = c.PriceMin
                    }).ToList();

                    var ListDetailChange = model.CAT_PriceGroupVehicle.Where(c => c.PriceID == objPrice.ID && listRouteIDChange.Contains(c.ContractRoutingID)).Select(c => new DTOPriceGroupVehicle
                    {
                        RouteID = c.ContractRoutingID,
                        GroupOfVehicleID = c.GroupOfVehicleID,
                        Price = c.Price,
                        PriceMax = c.PriceMax,
                        PriceMin = c.PriceMin
                    }).ToList();


                    var ListGOV = model.CAT_ContractGroupVehicle.Where(c => c.ContractID == objTerm.ContractID).Select(c => new CATGroupOfVehicle
                    {
                        ID = c.CAT_GroupOfVehicle.ID,
                        Code = c.CAT_GroupOfVehicle.Code,
                        Ton = c.CAT_GroupOfVehicle.Ton,
                        GroupName = c.CAT_GroupOfVehicle.GroupName,
                        SortOrder = c.SortOrder
                    }).Distinct().OrderBy(c => c.SortOrder).ToList();

                    result.PriceCurrent.FTLNormal.ListDetail.AddRange(ListDetailCurrent);
                    result.PriceCurrent.FTLNormal.ListGOV.AddRange(ListGOV);

                    result.PriceChange.FTLNormal.ListDetail.AddRange(ListDetailChange);
                    result.PriceChange.FTLNormal.ListGOV.AddRange(ListGOV);
                    #endregion

                    #region flt level
                    result.PriceCurrent.FTLLevel = new DTOPriceGVLevelDetail();
                    result.PriceCurrent.FTLLevel.ListDetail = new List<DTOPriceGVLevelGroupVehicle>();
                    result.PriceCurrent.FTLLevel.ListLevel = new List<DTOCATContractLevel>();
                    result.PriceChange.FTLLevel = new DTOPriceGVLevelDetail();
                    result.PriceChange.FTLLevel.ListDetail = new List<DTOPriceGVLevelGroupVehicle>();
                    result.PriceChange.FTLLevel.ListLevel = new List<DTOCATContractLevel>();

                    var ListFTLLevelDetailCurr = model.CAT_PriceGVLevelGroupVehicle.Where(c => c.PriceID == objPrice.ID && listRouteIDChange.Contains(c.ContractRoutingID)).Select(c => new DTOPriceGVLevelGroupVehicle
                    {
                        ID = c.ID,
                        RoutingID = c.ContractRoutingID,
                        ContractLevelID = c.ContractLevelID,
                        PriceMax = c.PriceMax,
                        PriceMin = c.PriceMin,
                        Price = c.Price
                    }).ToList();

                    var ListFTLLevelDetailChange = model.CAT_PriceGVLevelGroupVehicle.Where(c => c.PriceID == objPrice.ID && listRouteIDChange.Contains(c.ContractRoutingID)).Select(c => new DTOPriceGVLevelGroupVehicle
                    {
                        ID = c.ID,
                        RoutingID = c.ContractRoutingID,
                        ContractLevelID = c.ContractLevelID,
                        PriceMax = c.PriceMax,
                        PriceMin = c.PriceMin,
                        Price = c.Price
                    }).ToList();


                    var ListFTLLevel = model.CAT_ContractLevel.Where(c => c.ContractID == objTerm.ContractID).Select(c => new DTOCATContractLevel
                    {
                        ID = c.ID,
                        Code = c.Code,
                        LevelName = c.LevelName,
                        CBM = c.CBM,
                        Ton = c.Ton,
                        DateEnd = c.DateEnd,
                        DateStart = c.DateStart,
                        GroupOfVehicleID = c.GroupOfVehicleID,
                        SortOrder = c.SortOrder,
                        Quantity = c.Quantity,
                        GroupOfVehicleCode = c.CAT_GroupOfVehicle.Code,
                        GroupOfVehicleName = c.CAT_GroupOfVehicle.GroupName
                    }).OrderBy(c => c.SortOrder).ToList();

                    result.PriceCurrent.FTLLevel.ListDetail.AddRange(ListFTLLevelDetailCurr);
                    result.PriceChange.FTLLevel.ListDetail.AddRange(ListFTLLevelDetailChange);

                    result.PriceCurrent.FTLLevel.ListLevel.AddRange(ListFTLLevel);
                    result.PriceChange.FTLLevel.ListLevel.AddRange(ListFTLLevel);


                    #endregion

                    #region ltl nomal
                    result.PriceCurrent.LTLNormal = new DTOPriceDIGroupOfProductDetail();
                    result.PriceCurrent.LTLNormal.ListDetail = new List<DTOPriceDIGroupOfProduct>();
                    result.PriceCurrent.LTLNormal.ListGOP = new List<CUSGroupOfProduct>();

                    result.PriceChange.LTLNormal = new DTOPriceDIGroupOfProductDetail();
                    result.PriceChange.LTLNormal.ListDetail = new List<DTOPriceDIGroupOfProduct>();
                    result.PriceChange.LTLNormal.ListGOP = new List<CUSGroupOfProduct>();

                    var ListLTLNomarlCurr = model.CAT_PriceDIGroupProduct.Where(c => c.PriceID == objPrice.ID && listRouteIDChange.Contains(c.ContractRoutingID)).Select(c => new DTOPriceDIGroupOfProduct
                    {
                        ID = c.ID,
                        ContractRoutingID = c.ContractRoutingID,
                        GroupOfProductID = c.GroupOfProductID,
                        PriceID = c.PriceID,
                        Price = c.Price,
                        PriceMax = c.PriceMax,
                        PriceMin = c.PriceMin
                    }).ToList();

                    var ListLTLNomarlChange = model.CAT_PriceDIGroupProduct.Where(c => c.PriceID == objPrice.ID && listRouteIDChange.Contains(c.ContractRoutingID)).Select(c => new DTOPriceDIGroupOfProduct
                    {
                        ID = c.ID,
                        ContractRoutingID = c.ContractRoutingID,
                        GroupOfProductID = c.GroupOfProductID,
                        PriceID = c.PriceID,
                        Price = c.Price,
                        PriceMax = c.PriceMax,
                        PriceMin = c.PriceMin
                    }).ToList();

                    var listLTLGOP = model.CUS_GroupOfProduct.Where(c => c.CustomerID == objTerm.CustomerID && (objContract.CompanyID == null ? true : listGOPMapping.Contains(c.ID))).Select(c => new CUSGroupOfProduct
                    {
                        ID = c.ID,
                        Code = c.Code,
                        GroupName = c.GroupName,
                    }).ToList();

                    result.PriceCurrent.LTLNormal.ListDetail.AddRange(ListLTLNomarlCurr);
                    result.PriceCurrent.LTLNormal.ListGOP.AddRange(listLTLGOP);

                    result.PriceChange.LTLNormal.ListDetail.AddRange(ListLTLNomarlChange);
                    result.PriceChange.LTLNormal.ListGOP.AddRange(listLTLGOP);

                    #endregion

                    #region ltl level
                    result.PriceCurrent.LTLLevel = new DTOPriceDILevelDetail();
                    result.PriceCurrent.LTLLevel.ListDetail = new List<DTOPriceDILevelGroupProduct>();
                    result.PriceCurrent.LTLLevel.ListLevel = new List<DTOCATContractLevel>();

                    result.PriceChange.LTLLevel = new DTOPriceDILevelDetail();
                    result.PriceChange.LTLLevel.ListDetail = new List<DTOPriceDILevelGroupProduct>();
                    result.PriceChange.LTLLevel.ListLevel = new List<DTOCATContractLevel>();

                    var LTLListDetailCurr = model.CAT_PriceDILevelGroupProduct.Where(c => c.PriceID == objPrice.ID && listRouteIDChange.Contains(c.ContractRoutingID)).Select(c => new DTOPriceDILevelGroupProduct
                    {
                        ID = c.ID,
                        RoutingID = c.ContractRoutingID,
                        LevelID = c.ContractLevelID,
                        GroupProductID = c.GroupOfProductID,
                        Price = c.Price
                    }).ToList();
                    var LTLListDetailChange = model.CAT_PriceDILevelGroupProduct.Where(c => c.PriceID == objPrice.ID && listRouteIDChange.Contains(c.ContractRoutingID)).Select(c => new DTOPriceDILevelGroupProduct
                    {
                        ID = c.ID,
                        RoutingID = c.ContractRoutingID,
                        LevelID = c.ContractLevelID,
                        GroupProductID = c.GroupOfProductID,
                        Price = c.Price
                    }).ToList();

                    var LTLListLevel = model.CAT_ContractLevel.Where(c => c.ContractID == objTerm.ContractID).Select(c => new DTOCATContractLevel
                    {
                        ID = c.ID,
                        Code = c.Code,
                        LevelName = c.LevelName,
                        CBM = c.CBM,
                        Ton = c.Ton,
                        DateEnd = c.DateEnd,
                        DateStart = c.DateStart,
                        GroupOfVehicleID = c.GroupOfVehicleID,
                        SortOrder = c.SortOrder,
                        Quantity = c.Quantity,
                        GroupOfVehicleCode = c.CAT_GroupOfVehicle.Code,
                        GroupOfVehicleName = c.CAT_GroupOfVehicle.GroupName
                    }).OrderBy(c => c.SortOrder).ToList();

                    result.PriceCurrent.LTLLevel.ListDetail.AddRange(LTLListDetailCurr);
                    result.PriceCurrent.LTLLevel.ListLevel.AddRange(LTLListLevel);

                    result.PriceChange.LTLLevel.ListDetail.AddRange(LTLListDetailChange);
                    result.PriceChange.LTLLevel.ListLevel.AddRange(LTLListLevel);

                    #endregion

                    #region fcl
                    var lstDetailFCLCurr = model.CAT_PriceCOContainer.Where(c => c.PriceID == objPrice.ID && listRouteIDChange.Contains(c.ContractRoutingID.Value)).Select(c => new DTOPriceCOContainer
                    {
                        ID = c.ID,
                        PackingID = c.PackingID,
                        Price = c.Price,
                        PriceMax = c.PriceMax,
                        PriceMin = c.PriceMin,
                        PriceID = c.PriceID,
                        ContractRoutingID = c.ContractRoutingID.Value
                    }).ToList();
                    var lstDetailFCLChange = model.CAT_PriceCOContainer.Where(c => c.PriceID == objPrice.ID && listRouteIDChange.Contains(c.ContractRoutingID.Value)).Select(c => new DTOPriceCOContainer
                    {
                        ID = c.ID,
                        PackingID = c.PackingID,
                        Price = c.Price,
                        PriceMax = c.PriceMax,
                        PriceMin = c.PriceMin,
                        PriceID = c.PriceID,
                        ContractRoutingID = c.ContractRoutingID.Value
                    }).ToList();

                    var ListPacking = model.CAT_PriceCOContainer.Where(c => c.PriceID == objPrice.ID).Select(c => new CATPacking
                    {
                        ID = c.PackingID,
                        Code = c.CAT_Packing.Code,
                        PackingName = c.CAT_Packing.PackingName,
                        TypeOfPackageID = c.CAT_Packing.TypeOfPackageID
                    }).Distinct().ToList();

                    result.PriceCurrent.FCLData = new DTOPriceCOContainerData();
                    result.PriceCurrent.FCLData.ListDetail = new List<DTOPriceCOContainer>();
                    result.PriceCurrent.FCLData.ListPacking = new List<CATPacking>();
                    result.PriceChange.FCLData = new DTOPriceCOContainerData();
                    result.PriceChange.FCLData.ListDetail = new List<DTOPriceCOContainer>();
                    result.PriceChange.FCLData.ListPacking = new List<CATPacking>();

                    result.PriceCurrent.FCLData.ListDetail.AddRange(lstDetailFCLCurr);
                    result.PriceCurrent.FCLData.ListPacking.AddRange(ListPacking);
                    result.PriceChange.FCLData.ListDetail.AddRange(lstDetailFCLChange);
                    result.PriceChange.FCLData.ListPacking.AddRange(ListPacking);
                    #endregion
                }
                else result.PriceCurrent.ItemPrice = null;

                if (!string.IsNullOrEmpty(objTerm.ExprPrice))
                {
                    var itemExpr = new DTOMaterialChecking
                    {
                        PriceContract = objTerm.PriceContract,
                        PriceCurrent = objTerm.PriceCurrent,
                        PriceWarning = objTerm.PriceWarning
                    };

                    var packet = Term_Change_PricePacket(itemExpr, objTerm.ExprPrice);
                    if (packet != null)
                    {
                        if (result.PriceChange != null)
                        {
                            // flt normal
                            if (result.PriceChange.FTLNormal != null && result.PriceChange.FTLNormal.ListDetail != null)
                            {
                                foreach (var itemPrice in result.PriceChange.FTLNormal.ListDetail)
                                {
                                    itemExpr.Price = itemPrice.Price;
                                    itemPrice.Price = Term_Change_PriceCal(packet, itemExpr);
                                }
                            }
                            // flt level
                            if (result.PriceChange.FTLLevel != null && result.PriceChange.FTLLevel.ListDetail != null)
                            {
                                foreach (var itemPrice in result.PriceChange.FTLLevel.ListDetail)
                                {
                                    itemExpr.Price = itemPrice.Price;
                                    itemPrice.Price = Term_Change_PriceCal(packet, itemExpr);
                                }
                            }
                            // ltl nomal
                            if (result.PriceChange.LTLNormal != null)
                            {
                                foreach (var itemPrice in result.PriceChange.LTLNormal.ListDetail)
                                {
                                    itemExpr.Price = itemPrice.Price;
                                    itemPrice.Price = Term_Change_PriceCal(packet, itemExpr);
                                }
                            }
                            // ltl level
                            if (result.PriceChange.LTLLevel != null && result.PriceChange.LTLLevel.ListDetail != null)
                            {
                                foreach (var itemPrice in result.PriceChange.LTLLevel.ListDetail)
                                {
                                    itemExpr.Price = itemPrice.Price;
                                    itemPrice.Price = Term_Change_PriceCal(packet, itemExpr);
                                }
                            }
                            // fcl
                            if (result.PriceChange.FCLData != null && result.PriceChange.FCLData.ListDetail != null)
                            {
                                foreach (var itemPrice in result.PriceChange.FCLData.ListDetail)
                                {
                                    itemExpr.Price = itemPrice.Price;
                                    itemPrice.Price = Term_Change_PriceCal(packet, itemExpr);
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

        private static ExcelPackage Term_Change_PricePacket(DTOMaterialChecking item, string strExp)
        {
            try
            {
                ExcelPackage package = new ExcelPackage();
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Sheet 1");
                int row = 0, col = 1;
                string strCol = "A", strRow = "";

                row++;
                worksheet.Cells[row, col].Value = item.PriceContract;
                strExp = strExp.Replace("[PriceContract]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.PriceCurrent;
                strExp = strExp.Replace("[PriceCurrent]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.PriceWarning;
                strExp = strExp.Replace("[PriceWarning]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.Price;
                strExp = strExp.Replace("[Price]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Formula = strExp;

                return package;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Thiết lập sai công thức: " + strExp);
                throw ex;
            }
        }

        private static decimal Term_Change_PriceCal(ExcelPackage package, DTOMaterialChecking item)
        {
            try
            {
                decimal result = 0;
                int row = 0, col = 1;
                var worksheet = package.Workbook.Worksheets["Sheet 1"];

                row++;
                row++;
                row++;
                row++;

                worksheet.Cells[row, col].Value = item.Price;
                row++;
                package.Workbook.Calculate();
                var val = worksheet.Cells[row, col].Value.ToString().Trim();

                try
                { result = Convert.ToDecimal(val); }
                catch { }

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static DateTime? Term_Change_PriceDate(DTOMaterialChecking item, string strExp)
        {
            try
            {
                DateTime? result = null;
                ExcelPackage package = new ExcelPackage();
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Sheet 1");
                int row = 0, col = 1;
                string strCol = "A", strRow = "";

                row++;
                worksheet.Cells[row, col].Value = item.PriceContract;
                strExp = strExp.Replace("[PriceContract]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.PriceCurrent;
                strExp = strExp.Replace("[PriceCurrent]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = DateTime.Now;
                strExp = strExp.Replace("[Date]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.DateWarning;
                strExp = strExp.Replace("[DateWarning]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Formula = strExp;

                package.Workbook.Calculate();
                var val = worksheet.Cells[row, col].Value.ToString().Trim();

                try
                { result = DateTime.FromOADate(Convert.ToDouble(val)); }
                catch { }
                if (result == null)
                {
                    try
                    { result = Convert.ToDateTime(val, new CultureInfo("en-US")).Date; }
                    catch { }
                }

                return result;
            }
            catch
            {
                return null;
            }
        }

        public static void Term_Change_Save(DataEntities model, AccountItem Account, DTOCUSPrice_MaterialData item, int termID)
        {
            try
            {
                var objContact = model.CAT_ContractTerm.Where(c => c.ID == termID).Select(c => new { c.CAT_Contract.ID, c.CAT_Contract.IsCreateNewTerm }).FirstOrDefault();
                if (objContact == null)
                    throw FaultHelper.BusinessFault(null, null, "Không tìm thấy hợp đồng");

                var objTerm = model.CAT_ContractTerm.FirstOrDefault(c => c.ID == termID);
                if (objTerm == null)
                    throw FaultHelper.BusinessFault(null, null, "Không tìm thấy phụ lục ID:" + termID);

                var objPrice = model.CAT_Price.FirstOrDefault(c => c.ID == item.PriceCurrent.ItemPrice.ID);
                if (objPrice == null)
                    throw FaultHelper.BusinessFault(null, null, "Không tìm thấy bảng giá");

                //copy bảng giá
                CAT_Price obj = new CAT_Price();
                model.CAT_Price.Add(obj);

                obj.CreatedBy = Account.UserName;
                obj.CreatedDate = DateTime.Now;

                obj.Code = item.PriceChange.ItemPrice.Code;
                obj.Name = item.PriceChange.ItemPrice.Name;
                obj.ContractTermID = objPrice.ContractTermID;
                obj.EffectDate = item.PriceChange.ItemPrice.EffectDate.Date;
                obj.TypeOfOrderID = objPrice.TypeOfOrderID;
                obj.PriceContract = objTerm.PriceContract;
                obj.PriceWarning = objTerm.PriceWarning;
                obj.TypeOfRunLevelID = objPrice.TypeOfRunLevelID;

                #region copy chi tiết giá
                //var listRouteNoChange = item.PriceNoChange.ListRouting.Select(c => c.ID).ToList();
                var listRouteChange = item.PriceChange.ListRouting.Select(c => c.ID).ToList();

                #region CAT_PriceCOContainer
                var listCOContainer = model.CAT_PriceCOContainer.Where(c => c.PriceID == objPrice.ID).Select(c => new
                {
                    c.ContractRoutingID,
                    c.Price,
                    c.PriceMin,
                    c.PriceMax,
                    c.PackingID
                });
                foreach (var i in listCOContainer)
                {
                    if (listRouteChange.Contains(i.ContractRoutingID.Value))
                    {
                        var priceCOChange = item.PriceChange.FCLData.ListDetail.FirstOrDefault(c => c.ContractRoutingID == i.ContractRoutingID && c.PackingID == c.PackingID);
                        if (priceCOChange != null)
                        {
                            CAT_PriceCOContainer o = new CAT_PriceCOContainer();
                            model.CAT_PriceCOContainer.Add(o);

                            o.CreatedBy = Account.UserName;
                            o.CreatedDate = DateTime.Now;

                            o.CAT_Price = obj;
                            o.Price = priceCOChange.Price;
                            o.PriceMin = priceCOChange.PriceMin;
                            o.PriceMax = priceCOChange.PriceMax;
                            o.PackingID = i.PackingID;
                            o.ContractRoutingID = i.ContractRoutingID;
                        }
                    }
                    else
                    {
                        CAT_PriceCOContainer o = new CAT_PriceCOContainer();
                        model.CAT_PriceCOContainer.Add(o);

                        o.CreatedBy = Account.UserName;
                        o.CreatedDate = DateTime.Now;

                        o.CAT_Price = obj;
                        o.Price = i.Price;
                        o.PriceMin = i.PriceMin;
                        o.PriceMax = i.PriceMax;
                        o.PackingID = i.PackingID;
                        o.ContractRoutingID = i.ContractRoutingID;
                    }
                }
                #endregion


                #region CAT_PriceCOService
                var listCOService = model.CAT_PriceCOService.Where(c => c.PriceID == objPrice.ID).Select(c => new
                {
                    c.ServiceID,
                    c.Price,
                    c.PriceMin,
                    c.PriceMax,
                    c.PackingID,
                    c.CurrencyID
                }).ToList();
                foreach (var i in listCOService)
                {
                    CAT_PriceCOService o = new CAT_PriceCOService();
                    model.CAT_PriceCOService.Add(o);

                    o.CreatedBy = Account.UserName;
                    o.CreatedDate = DateTime.Now;

                    o.CAT_Price = obj;
                    o.Price = i.Price;
                    o.PriceMin = i.PriceMin;
                    o.PriceMax = i.PriceMax;
                    o.PackingID = i.PackingID;
                    o.CurrencyID = i.CurrencyID;
                    o.ServiceID = i.ServiceID;
                }
                #endregion


                #region CAT_PriceDIEx
                var listPriceEX = model.CAT_PriceDIEx.Where(c => c.PriceID == objPrice.ID).Select(c => new
                {
                    ID = c.ID,
                    TypeOfPriceDIExID = c.TypeOfPriceDIExID,
                    ExprCBM = c.ExprCBM,
                    ExprInput = c.ExprInput,
                    ExprPrice = c.ExprPrice,
                    ExprPriceFix = c.ExprPriceFix,
                    ExprQuan = c.ExprQuan,
                    ExprTon = c.ExprTon,
                    Note = c.Note,
                    DIExSumID = c.DIExSumID,
                    Price = c.Price
                }).ToList();
                foreach (var priceEx in listPriceEX)
                {
                    CAT_PriceDIEx o = new CAT_PriceDIEx();
                    model.CAT_PriceDIEx.Add(o);

                    o.CreatedBy = Account.UserName;
                    o.CreatedDate = DateTime.Now;

                    o.CAT_Price = obj;
                    o.TypeOfPriceDIExID = priceEx.TypeOfPriceDIExID;
                    o.Price = priceEx.Price;
                    o.ExprCBM = priceEx.ExprCBM;
                    o.ExprInput = priceEx.ExprInput;
                    o.ExprPrice = priceEx.ExprPrice;
                    o.ExprPriceFix = priceEx.ExprPriceFix;
                    o.ExprQuan = priceEx.ExprQuan;
                    o.ExprTon = priceEx.ExprTon;
                    o.Note = priceEx.Note;
                    o.DIExSumID = priceEx.DIExSumID;
                    foreach (var exGOL in model.CAT_PriceDIExGroupLocation.Where(c => c.PriceDIExID == priceEx.ID))
                    {
                        CAT_PriceDIExGroupLocation gol = new CAT_PriceDIExGroupLocation();
                        gol.CreatedBy = Account.UserName;
                        gol.CreatedDate = DateTime.Now;
                        gol.CAT_PriceDIEx = o;
                        gol.GroupOfLocationID = exGOL.GroupOfLocationID;
                        model.CAT_PriceDIExGroupLocation.Add(gol);
                    }
                    foreach (var exGOP in model.CAT_PriceDIExGroupProduct.Where(c => c.PriceDIExID == priceEx.ID))
                    {
                        CAT_PriceDIExGroupProduct gop = new CAT_PriceDIExGroupProduct();
                        gop.CreatedBy = Account.UserName;
                        gop.CreatedDate = DateTime.Now;
                        gop.CAT_PriceDIEx = o;
                        gop.GroupOfProductID = exGOP.GroupOfProductID;
                        gop.ExprPrice = exGOP.ExprPrice;
                        gop.ExprQuantity = exGOP.ExprQuantity;
                        model.CAT_PriceDIExGroupProduct.Add(gop);
                    }
                    foreach (var exRoute in model.CAT_PriceDIExRouting.Where(c => c.PriceDIExID == priceEx.ID))
                    {
                        CAT_PriceDIExRouting gop = new CAT_PriceDIExRouting();
                        gop.CreatedBy = Account.UserName;
                        gop.CreatedDate = DateTime.Now;
                        gop.CAT_PriceDIEx = o;
                        gop.RoutingID = exRoute.RoutingID;
                        gop.ParentRoutingID = exRoute.ParentRoutingID;
                        gop.LocationID = exRoute.LocationID;
                        gop.TypeOfTOLocationID = exRoute.TypeOfTOLocationID;
                        model.CAT_PriceDIExRouting.Add(gop);
                    }
                }
                #endregion



                #region CAT_PriceDIGroupProduct (ltl normal)
                foreach (var i in model.CAT_PriceDIGroupProduct.Where(c => c.PriceID == objPrice.ID).Select(c => new
                {
                    c.ContractRoutingID,
                    c.Price,
                    c.GroupOfProductID,
                    c.PriceMin,
                    c.PriceMax
                }).ToList())
                {
                    if (!listRouteChange.Contains(i.ContractRoutingID))
                    {
                        CAT_PriceDIGroupProduct o = new CAT_PriceDIGroupProduct();
                        model.CAT_PriceDIGroupProduct.Add(o);

                        o.CreatedBy = Account.UserName;
                        o.CreatedDate = DateTime.Now;

                        o.CAT_Price = obj;
                        o.Price = i.Price;
                        o.PriceMin = i.PriceMin;
                        o.PriceMax = i.PriceMax;
                        o.ContractRoutingID = i.ContractRoutingID;
                        o.GroupOfProductID = i.GroupOfProductID;
                    }
                    else
                    {
                        var priceChange = item.PriceChange.LTLNormal.ListDetail.FirstOrDefault(c => c.ContractRoutingID == i.ContractRoutingID && c.GroupOfProductID == i.GroupOfProductID);
                        if (priceChange != null)
                        {
                            CAT_PriceDIGroupProduct o = new CAT_PriceDIGroupProduct();
                            model.CAT_PriceDIGroupProduct.Add(o);

                            o.CreatedBy = Account.UserName;
                            o.CreatedDate = DateTime.Now;

                            o.CAT_Price = obj;
                            o.Price = priceChange.Price;
                            o.PriceMin = priceChange.PriceMin;
                            o.PriceMax = priceChange.PriceMax;
                            o.ContractRoutingID = i.ContractRoutingID;
                            o.GroupOfProductID = i.GroupOfProductID;
                        }
                    }
                }
                #endregion

                #region CAT_PriceDILevelGroupproduct


                var lstPriceDILevelGroupProduct = model.CAT_PriceDILevelGroupProduct.Where(c => c.PriceID == objPrice.ID).Select(c => new
                {
                    c.ContractRoutingID,
                    c.GroupOfProductID,
                    c.ContractLevelID,
                    c.Price
                }).ToList();

                foreach (var detail in lstPriceDILevelGroupProduct)
                {
                    if (listRouteChange.Contains(detail.ContractRoutingID))
                    {
                        var priceChange = item.PriceChange.LTLLevel.ListDetail.FirstOrDefault(c => c.LevelID == detail.ContractLevelID && c.GroupProductID == detail.GroupOfProductID && c.RoutingID == detail.ContractRoutingID);
                        if (priceChange != null)
                        {
                            CAT_PriceDILevelGroupProduct g = new CAT_PriceDILevelGroupProduct();
                            g.CreatedBy = Account.UserName;
                            g.CreatedDate = DateTime.Now;
                            g.ContractRoutingID = detail.ContractRoutingID;
                            g.GroupOfProductID = detail.GroupOfProductID;
                            g.ContractLevelID = detail.ContractLevelID;
                            g.Price = priceChange.Price;
                            g.CAT_Price = obj;
                            model.CAT_PriceDILevelGroupProduct.Add(g);
                        }
                    }
                    else
                    {
                        CAT_PriceDILevelGroupProduct g = new CAT_PriceDILevelGroupProduct();
                        g.CreatedBy = Account.UserName;
                        g.CreatedDate = DateTime.Now;
                        g.ContractRoutingID = detail.ContractRoutingID;
                        g.GroupOfProductID = detail.GroupOfProductID;
                        g.ContractLevelID = detail.ContractLevelID;
                        g.Price = detail.Price;
                        g.CAT_Price = obj;
                        model.CAT_PriceDILevelGroupProduct.Add(g);
                    }
                }

                #endregion

                #region CAT_PriceGroupVehicle
                foreach (var i in model.CAT_PriceGroupVehicle.Where(c => c.PriceID == objPrice.ID).Select(c => new
                {
                    c.ContractRoutingID,
                    c.Price,
                    c.GroupOfVehicleID,
                    c.PriceMin,
                    c.PriceMax,
                    c.DateStart,
                    c.DateEnd
                }).ToList())
                {
                    if (listRouteChange.Contains(i.ContractRoutingID))
                    {
                        var priceChange = item.PriceChange.FTLNormal.ListDetail.FirstOrDefault(c => c.RouteID == i.ContractRoutingID && c.GroupOfVehicleID == i.GroupOfVehicleID);
                        if (priceChange != null)
                        {
                            CAT_PriceGroupVehicle o = new CAT_PriceGroupVehicle();
                            model.CAT_PriceGroupVehicle.Add(o);

                            o.CreatedBy = Account.UserName;
                            o.CreatedDate = DateTime.Now;

                            o.CAT_Price = obj;
                            o.Price = priceChange.Price;
                            o.PriceMin = priceChange.PriceMin;
                            o.PriceMax = priceChange.PriceMax;
                            o.ContractRoutingID = i.ContractRoutingID;
                            o.GroupOfVehicleID = i.GroupOfVehicleID;
                            o.DateEnd = i.DateEnd;
                            o.DateStart = i.DateStart;
                        }
                    }
                    else
                    {
                        CAT_PriceGroupVehicle o = new CAT_PriceGroupVehicle();
                        model.CAT_PriceGroupVehicle.Add(o);

                        o.CreatedBy = Account.UserName;
                        o.CreatedDate = DateTime.Now;

                        o.CAT_Price = obj;
                        o.Price = i.Price;
                        o.PriceMin = i.PriceMin;
                        o.PriceMax = i.PriceMax;
                        o.ContractRoutingID = i.ContractRoutingID;
                        o.GroupOfVehicleID = i.GroupOfVehicleID;
                        o.DateEnd = i.DateEnd;
                        o.DateStart = i.DateStart;
                    }

                }
                #endregion

                #region CAT_PriceGVLevelGroupVehicle

                foreach (var detail in model.CAT_PriceGVLevelGroupVehicle.Where(c => c.PriceID == objPrice.ID))
                {
                    if (listRouteChange.Contains(detail.ContractRoutingID))
                    {
                        var priceChange = item.PriceChange.FTLLevel.ListDetail.FirstOrDefault(c => c.ContractLevelID == detail.ContractLevelID && c.RoutingID == detail.ContractRoutingID);
                        if (priceChange != null)
                        {
                            CAT_PriceGVLevelGroupVehicle g = new CAT_PriceGVLevelGroupVehicle();
                            g.CreatedBy = Account.UserName;
                            g.CreatedDate = DateTime.Now;
                            g.ContractRoutingID = detail.ContractRoutingID;
                            g.ContractLevelID = detail.ContractLevelID;
                            g.Price = priceChange.Price;
                            g.PriceMax = priceChange.PriceMax;
                            g.PriceMin = priceChange.PriceMin;
                            g.CAT_Price = obj;
                            model.CAT_PriceGVLevelGroupVehicle.Add(g);
                        }
                    }
                    else
                    {
                        CAT_PriceGVLevelGroupVehicle g = new CAT_PriceGVLevelGroupVehicle();
                        g.CreatedBy = Account.UserName;
                        g.CreatedDate = DateTime.Now;
                        g.ContractRoutingID = detail.ContractRoutingID;
                        g.ContractLevelID = detail.ContractLevelID;
                        g.Price = detail.Price;
                        g.PriceMax = detail.PriceMax;
                        g.PriceMin = detail.PriceMin;
                        g.CAT_Price = obj;
                        model.CAT_PriceGVLevelGroupVehicle.Add(g);
                    }

                }
                #endregion

                #region CAT_PriceDILoad
                var listPriceDILoad = model.CAT_PriceDILoad.Where(c => c.PriceID == objPrice.ID).ToList();
                foreach (var i in listPriceDILoad)
                {
                    CAT_PriceDILoad o = new CAT_PriceDILoad();
                    model.CAT_PriceDILoad.Add(o);

                    o.CreatedBy = Account.UserName;
                    o.CreatedDate = DateTime.Now;

                    o.CAT_Price = obj;
                    o.LocationID = i.LocationID;
                    o.IsLoading = i.IsLoading;
                    o.ParentRoutingID = i.ParentRoutingID;
                    o.RoutingID = i.RoutingID;
                    o.GroupOfLocationID = i.GroupOfLocationID;

                    foreach (var e in model.CAT_PriceDILoadDetail.Where(c => c.PriceDILoadID == i.ID).Select(c => new
                    {
                        c.PriceOfGOPID,
                        c.Price,
                        c.GroupOfProductID
                    }).ToList())
                    {
                        CAT_PriceDILoadDetail g = new CAT_PriceDILoadDetail();

                        g.CreatedBy = Account.UserName;
                        g.CreatedDate = DateTime.Now;

                        g.PriceOfGOPID = e.PriceOfGOPID;
                        g.GroupOfProductID = e.GroupOfProductID;
                        g.Price = e.Price;
                        g.CAT_PriceDILoad = o;
                        model.CAT_PriceDILoadDetail.Add(g);
                    }
                }
                #endregion

                #region price moq
                var listMOQ = model.CAT_PriceDIMOQ.Where(c => c.PriceID == objPrice.ID).ToList();
                foreach (var moq in listMOQ)
                {
                    CAT_PriceDIMOQ m = new CAT_PriceDIMOQ();
                    m.CreatedBy = Account.UserName;
                    m.CreatedDate = DateTime.Now;
                    m.MOQName = moq.MOQName;
                    m.ParentRoutingID = moq.ParentRoutingID;
                    m.DIMOQSumID = moq.DIMOQSumID;
                    m.ExprInput = moq.ExprInput;
                    m.ExprPrice = moq.ExprPrice;
                    m.ExprPriceFix = moq.ExprPriceFix;
                    m.ExprQuan = moq.ExprQuan;
                    m.ExprTon = moq.ExprTon;
                    m.ExprCBM = moq.ExprCBM;
                    m.TypeOfPriceDIExID = moq.TypeOfPriceDIExID;
                    m.CAT_Price = obj;
                    model.CAT_PriceDIMOQ.Add(m);

                    foreach (var detail in moq.CAT_PriceDIMOQGroupLocation.Where(c => c.PriceDIMOQID == moq.ID))
                    {
                        CAT_PriceDIMOQGroupLocation gol = new CAT_PriceDIMOQGroupLocation();

                        gol.CreatedBy = Account.UserName;
                        gol.CreatedDate = DateTime.Now;
                        gol.GroupOfLocationID = detail.GroupOfLocationID;
                        gol.CAT_PriceDIMOQ = m;

                        model.CAT_PriceDIMOQGroupLocation.Add(gol);
                    }

                    foreach (var detail in moq.CAT_PriceDIMOQGroupProduct.Where(c => c.PriceDIMOQID == moq.ID))
                    {
                        CAT_PriceDIMOQGroupProduct gol = new CAT_PriceDIMOQGroupProduct();

                        gol.CreatedBy = Account.UserName;
                        gol.CreatedDate = DateTime.Now;
                        gol.GroupOfProductID = detail.GroupOfProductID;
                        gol.ExprPrice = detail.ExprPrice;
                        gol.ExprQuantity = detail.ExprQuantity;

                        gol.CAT_PriceDIMOQ = m;

                        model.CAT_PriceDIMOQGroupProduct.Add(gol);
                    }

                    foreach (var detail in moq.CAT_PriceDIMOQRouting.Where(c => c.PriceDIMOQID == moq.ID))
                    {
                        CAT_PriceDIMOQRouting gol = new CAT_PriceDIMOQRouting();

                        gol.CreatedBy = Account.UserName;
                        gol.CreatedDate = DateTime.Now;
                        gol.RoutingID = detail.RoutingID;
                        gol.ParentRoutingID = detail.ParentRoutingID;
                        gol.LocationID = detail.LocationID;
                        gol.TypeOfTOLocationID = detail.TypeOfTOLocationID;

                        gol.CAT_PriceDIMOQ = m;

                        model.CAT_PriceDIMOQRouting.Add(gol);
                    }
                }
                #endregion

                #region DI load moq
                var listMoqLoad = model.CAT_PriceDIMOQLoad.Where(c => c.PriceID == objPrice.ID).ToList();
                foreach (var moq in listMoqLoad)
                {
                    CAT_PriceDIMOQLoad m = new CAT_PriceDIMOQLoad();
                    m.CreatedBy = Account.UserName;
                    m.CreatedDate = DateTime.Now;
                    m.MOQName = moq.MOQName;
                    m.ParentRoutingID = moq.ParentRoutingID;
                    m.IsLoading = moq.IsLoading;
                    m.ExprInput = moq.ExprInput;
                    m.ExprPrice = moq.ExprPrice;
                    m.ExprPriceFix = moq.ExprPriceFix;
                    m.ExprQuan = moq.ExprQuan;
                    m.ExprTon = moq.ExprTon;
                    m.ExprCBM = moq.ExprCBM;
                    m.TypeOfPriceDIExID = moq.TypeOfPriceDIExID;
                    m.ParentRoutingID = moq.ParentRoutingID;
                    m.DIMOQLoadSumID = moq.DIMOQLoadSumID;
                    m.CAT_Price = obj;
                    model.CAT_PriceDIMOQLoad.Add(m);

                    foreach (var detail in moq.CAT_PriceDIMOQLoadGroupLocation.Where(c => c.PriceDIMOQLoadID == moq.ID))
                    {
                        CAT_PriceDIMOQLoadGroupLocation gol = new CAT_PriceDIMOQLoadGroupLocation();

                        gol.CreatedBy = Account.UserName;
                        gol.CreatedDate = DateTime.Now;
                        gol.GroupOfLocationID = detail.GroupOfLocationID;
                        gol.CAT_PriceDIMOQLoad = m;

                        model.CAT_PriceDIMOQLoadGroupLocation.Add(gol);
                    }

                    foreach (var detail in moq.CAT_PriceDIMOQLoadGroupProduct.Where(c => c.PriceDIMOQLoadID == moq.ID))
                    {
                        CAT_PriceDIMOQLoadGroupProduct gol = new CAT_PriceDIMOQLoadGroupProduct();

                        gol.CreatedBy = Account.UserName;
                        gol.CreatedDate = DateTime.Now;
                        gol.GroupOfProductID = detail.GroupOfProductID;
                        gol.ExprPrice = detail.ExprPrice;
                        gol.ExprQuantity = detail.ExprQuantity;

                        gol.CAT_PriceDIMOQLoad = m;

                        model.CAT_PriceDIMOQLoadGroupProduct.Add(gol);
                    }

                    foreach (var detail in moq.CAT_PriceDIMOQLoadRouting.Where(c => c.PriceDIMOQLoadID == moq.ID))
                    {
                        CAT_PriceDIMOQLoadRouting gol = new CAT_PriceDIMOQLoadRouting();

                        gol.CreatedBy = Account.UserName;
                        gol.CreatedDate = DateTime.Now;
                        gol.RoutingID = detail.RoutingID;
                        gol.ParentRoutingID = detail.ParentRoutingID;
                        gol.LocationID = detail.LocationID;

                        gol.CAT_PriceDIMOQLoad = m;

                        model.CAT_PriceDIMOQLoadRouting.Add(gol);
                    }
                }
                #endregion
                #endregion

                model.SaveChanges();

                if (objContact.IsCreateNewTerm)
                {
                    var objNewTerm = new CAT_ContractTerm();
                    objNewTerm.CreatedBy = Account.UserName;
                    objNewTerm.CreatedDate = DateTime.Now;
                    objNewTerm.ContractID = objContact.ID;
                    objNewTerm.IsClosed = false;
                    objNewTerm.Code = obj.Code;
                    objNewTerm.TermName = obj.Name;
                    objNewTerm.DisplayName = objTerm.DisplayName;
                    objNewTerm.Note = objTerm.Note;
                    objNewTerm.DateEffect = objTerm.DatePrice.Value;
                    if (objNewTerm.DateEffect > objTerm.DateExpire)
                        objNewTerm.DateExpire = objNewTerm.DateEffect.AddDays(1);
                    else
                        objNewTerm.DateExpire = objTerm.DateExpire;
                    objNewTerm.DatePrice = objTerm.DatePrice;
                    objNewTerm.IsAllRouting = objTerm.IsAllRouting;
                    objNewTerm.MaterialID = objTerm.MaterialID;
                    objNewTerm.PriceContract = objTerm.PriceWarning;
                    objNewTerm.PriceCurrent = objTerm.PriceCurrent;
                    objNewTerm.PriceWarning = 0;
                    objNewTerm.ExprDatePrice = objTerm.ExprDatePrice;
                    objNewTerm.ExprInput = objTerm.ExprInput;
                    objNewTerm.ExprPrice = objTerm.ExprPrice;
                    objNewTerm.DateWarning = null;
                    objNewTerm.IsWarning = false;
                    objNewTerm.ServiceOfOrderID = objTerm.ServiceOfOrderID;
                    model.CAT_ContractTerm.Add(objNewTerm);
                    obj.CAT_ContractTerm = objNewTerm;
                    objTerm.DateExpire = objNewTerm.DateEffect.AddDays(-1);
                    objTerm.IsClosed = true;
                    foreach (var itemRouting in model.CAT_ContractRouting.Where(c => c.ContractTermID == termID))
                    {
                        itemRouting.CAT_ContractTerm = objNewTerm;
                        itemRouting.ModifiedBy = Account.UserName;
                        itemRouting.ModifiedDate = DateTime.Now;
                    }
                    model.SaveChanges();
                }
                else
                {
                    objTerm.PriceContract = objTerm.PriceWarning;
                    objTerm.IsWarning = false;
                    objTerm.DateWarning = null;
                    objTerm.ModifiedBy = Account.UserName;
                    objTerm.ModifiedDate = DateTime.Now;
                    model.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void Term_Change_RemoveWarning(DataEntities model, AccountItem Account, int termID)
        {
            try
            {
                var obj = model.CAT_ContractTerm.FirstOrDefault(c => c.ID == termID);
                if (obj != null && obj.IsWarning == true)
                {
                    obj.PriceWarning = null;
                    obj.IsWarning = false;
                    obj.ModifiedBy = Account.UserName;
                    obj.ModifiedDate = DateTime.Now;
                    model.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Price
        public static int Price_Save(DataEntities model, AccountItem Account, DTOPrice item, int termID)
        {
            try
            {
                if (string.IsNullOrEmpty(item.Code) || string.IsNullOrEmpty(item.Name))
                    throw new Exception("Mã, tên bảng giá không được trống");
                if (model.CAT_Price.Count(c => c.Code == item.Code && c.ID != item.ID && c.ContractTermID == termID && c.ID != item.ID) > 0)
                    throw new Exception("Mã bảng giá đã sử dụng");
                var objTerm = model.CAT_ContractTerm.FirstOrDefault(c => c.ID == termID);
                if (objTerm == null) throw new Exception("Không tìm thấy phụ lục ID:" + termID);

                var objContract = model.CAT_Contract.FirstOrDefault(c => c.ID == objTerm.ContractID);
                if (objContract == null) throw new Exception("Không tìm thấy Hợp đồng ID:" + objTerm.ContractID);

                //kiem tra neu là ltl level phải có hàng hóa mới dc luu
                bool IsLTLLevel = model.CAT_ContractLevel.Count(c => c.ContractID == objContract.ID) > 0;
                if (IsLTLLevel && objContract.CAT_TransportMode.TransportModeID == -(int)SYSVarType.TransportModeLTL && model.CUS_GroupOfProduct.Count(c => c.CustomerID == objContract.CustomerID) == 0)
                    throw new Exception("Khách hàng/Nhà xe không có nhóm hàng hóa, không thể tạo bảng giá cho LTL bậc thang");


                var obj = model.CAT_Price.FirstOrDefault(c => c.ID == item.ID);
                if (obj == null)
                {
                    obj = new CAT_Price();
                    obj.CreatedBy = Account.UserName;
                    obj.CreatedDate = DateTime.Now;
                    obj.ContractTermID = termID;
                    obj.TypeOfOrderID = item.TypeOfOrderID;
                    model.CAT_Price.Add(obj);
                }
                else
                {
                    obj.ModifiedBy = Account.UserName;
                    obj.ModifiedDate = DateTime.Now;
                    if (obj.CAT_PriceCOContainer.Count == 0 && obj.CAT_PriceCOService.Count == 0 && obj.CAT_PriceDIEx.Count == 0 && obj.CAT_PriceDIGroupProduct.Count == 0
                        && obj.CAT_PriceDILoad.Count == 0 && obj.CAT_PriceDIMOQ.Count == 0 && obj.CAT_PriceDIVendor.Count == 0 && obj.CAT_PriceGroupVehicle.Count == 0
                        && obj.CAT_PriceRouting.Count == 0)
                        obj.TypeOfOrderID = item.TypeOfOrderID;
                }

                obj.Code = item.Code;
                obj.Name = item.Name;
                obj.EffectDate = item.EffectDate.Date;

                model.SaveChanges();
                return obj.ID;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void Price_Delete(DataEntities model, AccountItem Account, int priceID)
        {
            try
            {
                var obj = model.CAT_Price.FirstOrDefault(c => c.ID == priceID);
                if (obj == null) throw new Exception("Không tìm thấy bảng giá ID:" + priceID);

                //xoa service và giá container
                foreach (var service in model.CAT_PriceCOService.Where(c => c.PriceID == obj.ID))
                    model.CAT_PriceCOService.Remove(service);
                foreach (var con in model.CAT_PriceCOContainer.Where(c => c.PriceID == obj.ID))
                    model.CAT_PriceCOContainer.Remove(con);

                //xoa phu thu
                foreach (var service in model.CAT_PriceDIEx.Where(c => c.PriceID == obj.ID))
                {
                    foreach (var group in model.CAT_PriceDIExGroupLocation.Where(c => c.PriceDIExID == service.ID))
                        model.CAT_PriceDIExGroupLocation.Remove(group);
                    foreach (var gop in model.CAT_PriceDIExGroupProduct.Where(c => c.PriceDIExID == service.ID))
                        model.CAT_PriceDIExGroupProduct.Remove(gop);
                    foreach (var group in model.CAT_PriceDIExRouting.Where(c => c.PriceDIExID == service.ID))
                        model.CAT_PriceDIExRouting.Remove(group);

                    model.CAT_PriceDIEx.Remove(service);
                }

                //xoa bang gia thuong
                foreach (var price in model.CAT_PriceDIGroupProduct.Where(c => c.PriceID == obj.ID))
                    model.CAT_PriceDIGroupProduct.Remove(price);
                foreach (var price in model.CAT_PriceGroupVehicle.Where(c => c.PriceID == obj.ID))
                    model.CAT_PriceGroupVehicle.Remove(price);

                //xoa bang gia bac thang
                foreach (var detail in model.CAT_PriceDILevelGroupProduct.Where(c => c.PriceID == obj.ID))
                    model.CAT_PriceDILevelGroupProduct.Remove(detail);
                foreach (var detail in model.CAT_PriceGVLevelGroupVehicle.Where(c => c.PriceID == obj.ID))
                    model.CAT_PriceGVLevelGroupVehicle.Remove(detail);

                //xoa moq
                foreach (var moq in model.CAT_PriceDIMOQ.Where(c => c.PriceID == obj.ID))
                {
                    foreach (var detail in model.CAT_PriceDIMOQGroupLocation.Where(c => c.PriceDIMOQID == moq.ID))
                        model.CAT_PriceDIMOQGroupLocation.Remove(detail);
                    foreach (var detail in model.CAT_PriceDIMOQGroupProduct.Where(c => c.PriceDIMOQID == moq.ID))
                        model.CAT_PriceDIMOQGroupProduct.Remove(detail);
                    foreach (var detail in model.CAT_PriceDIMOQRouting.Where(c => c.PriceDIMOQID == moq.ID))
                        model.CAT_PriceDIMOQRouting.Remove(detail);
                    model.CAT_PriceDIMOQ.Remove(moq);
                }

                //xóa boc xếp
                foreach (var load in model.CAT_PriceDILoad.Where(c => c.PriceID == obj.ID))
                {
                    foreach (var detail in model.CAT_PriceDILoadDetail.Where(c => c.PriceDILoadID == load.ID))
                        model.CAT_PriceDILoadDetail.Remove(detail);

                    model.CAT_PriceDILoad.Remove(load);
                }

                //xoa moq boc xep
                foreach (var moq in model.CAT_PriceDIMOQLoad.Where(c => c.PriceID == obj.ID))
                {
                    foreach (var detail in model.CAT_PriceDIMOQLoadGroupLocation.Where(c => c.PriceDIMOQLoadID == moq.ID))
                        model.CAT_PriceDIMOQLoadGroupLocation.Remove(detail);
                    foreach (var detail in model.CAT_PriceDIMOQLoadGroupProduct.Where(c => c.PriceDIMOQLoadID == moq.ID))
                        model.CAT_PriceDIMOQLoadGroupProduct.Remove(detail);
                    foreach (var detail in model.CAT_PriceDIMOQLoadRouting.Where(c => c.PriceDIMOQLoadID == moq.ID))
                        model.CAT_PriceDIMOQLoadRouting.Remove(detail);
                    model.CAT_PriceDIMOQLoad.Remove(moq);
                }

                model.CAT_Price.Remove(obj);
                model.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void Price_Copy(DataEntities model, AccountItem Account, DTOPriceCopyItem item)
        {
            try
            {
                model.EventAccount = Account; model.EventRunning = false;
                if (string.IsNullOrEmpty(item.NewCode) || string.IsNullOrEmpty(item.NewName))
                    throw new Exception("Mã, tên bảng giá mới không được trống");
                var objPrice = model.CAT_Price.FirstOrDefault(c => c.ID == item.ID);
                if (objPrice == null) throw new Exception("Không tìm thấy bảng giá ID:" + item.ID);

                CAT_Price obj = new CAT_Price();
                model.CAT_Price.Add(obj);

                obj.CreatedBy = Account.UserName;
                obj.CreatedDate = DateTime.Now;

                obj.Code = item.NewCode;
                obj.Name = item.NewName;
                obj.ContractTermID = objPrice.ContractTermID;
                obj.EffectDate = objPrice.EffectDate;
                obj.TypeOfOrderID = objPrice.TypeOfOrderID;
                obj.TypeOfRunLevelID = objPrice.TypeOfRunLevelID;

                #region CAT_PriceCOContainer
                var listCOContainer = model.CAT_PriceCOContainer.Where(c => c.PriceID == item.ID).Select(c => new
                {
                    c.ContractRoutingID,
                    c.Price,
                    c.PriceMin,
                    c.PriceMax,
                    c.PackingID
                }).ToList();
                foreach (var i in listCOContainer)
                {
                    CAT_PriceCOContainer o = new CAT_PriceCOContainer();
                    model.CAT_PriceCOContainer.Add(o);

                    o.CreatedBy = Account.UserName;
                    o.CreatedDate = DateTime.Now;

                    o.CAT_Price = obj;
                    o.Price = i.Price;
                    o.PriceMin = i.PriceMin;
                    o.PriceMax = i.PriceMax;
                    o.PackingID = i.PackingID;
                    o.ContractRoutingID = i.ContractRoutingID;
                }
                #endregion

                #region CAT_PriceCOService
                var listCOService = model.CAT_PriceCOService.Where(c => c.PriceID == item.ID).Select(c => new
                {
                    c.ServiceID,
                    c.Price,
                    c.PriceMin,
                    c.PriceMax,
                    c.PackingID,
                    c.CurrencyID
                }).ToList();
                foreach (var i in listCOService)
                {
                    CAT_PriceCOService o = new CAT_PriceCOService();
                    model.CAT_PriceCOService.Add(o);

                    o.CreatedBy = Account.UserName;
                    o.CreatedDate = DateTime.Now;

                    o.CAT_Price = obj;
                    o.Price = i.Price;
                    o.PriceMin = i.PriceMin;
                    o.PriceMax = i.PriceMax;
                    o.PackingID = i.PackingID;
                    o.CurrencyID = i.CurrencyID;
                    o.ServiceID = i.ServiceID;
                }
                #endregion

                #region CAT_PriceDIEx
                var listPriceEX = model.CAT_PriceDIEx.Where(c => c.PriceID == objPrice.ID).Select(c => new
                {
                    ID = c.ID,
                    TypeOfPriceDIExID = c.TypeOfPriceDIExID,
                    ExprCBM = c.ExprCBM,
                    ExprInput = c.ExprInput,
                    ExprPrice = c.ExprPrice,
                    ExprPriceFix = c.ExprPriceFix,
                    ExprQuan = c.ExprQuan,
                    ExprTon = c.ExprTon,
                    Note = c.Note,
                    DIExSumID = c.DIExSumID,
                }).ToList();
                foreach (var priceEx in listPriceEX)
                {
                    CAT_PriceDIEx o = new CAT_PriceDIEx();
                    model.CAT_PriceDIEx.Add(o);

                    o.CreatedBy = Account.UserName;
                    o.CreatedDate = DateTime.Now;

                    o.PriceID = obj.ID;
                    o.TypeOfPriceDIExID = priceEx.TypeOfPriceDIExID;
                    o.ExprCBM = priceEx.ExprCBM;
                    o.ExprInput = priceEx.ExprInput;
                    o.ExprPrice = priceEx.ExprPrice;
                    o.ExprPriceFix = priceEx.ExprPriceFix;
                    o.ExprQuan = priceEx.ExprQuan;
                    o.ExprTon = priceEx.ExprTon;
                    o.Note = priceEx.Note;
                    o.DIExSumID = priceEx.DIExSumID;
                    foreach (var exGOL in model.CAT_PriceDIExGroupLocation.Where(c => c.PriceDIExID == priceEx.ID))
                    {
                        CAT_PriceDIExGroupLocation gol = new CAT_PriceDIExGroupLocation();
                        gol.CreatedBy = Account.UserName;
                        gol.CreatedDate = DateTime.Now;
                        gol.CAT_PriceDIEx = o;
                        gol.GroupOfLocationID = exGOL.GroupOfLocationID;
                        model.CAT_PriceDIExGroupLocation.Add(gol);
                    }
                    foreach (var exGOP in model.CAT_PriceDIExGroupProduct.Where(c => c.PriceDIExID == priceEx.ID))
                    {
                        CAT_PriceDIExGroupProduct gop = new CAT_PriceDIExGroupProduct();
                        gop.CreatedBy = Account.UserName;
                        gop.CreatedDate = DateTime.Now;
                        gop.CAT_PriceDIEx = o;
                        gop.GroupOfProductID = exGOP.GroupOfProductID;
                        gop.ExprPrice = exGOP.ExprPrice;
                        gop.ExprQuantity = exGOP.ExprQuantity;
                        model.CAT_PriceDIExGroupProduct.Add(gop);
                    }
                    foreach (var exRoute in model.CAT_PriceDIExRouting.Where(c => c.PriceDIExID == priceEx.ID))
                    {
                        CAT_PriceDIExRouting gop = new CAT_PriceDIExRouting();
                        gop.CreatedBy = Account.UserName;
                        gop.CreatedDate = DateTime.Now;
                        gop.CAT_PriceDIEx = o;
                        gop.RoutingID = exRoute.RoutingID;
                        gop.ParentRoutingID = exRoute.ParentRoutingID;
                        gop.LocationID = exRoute.LocationID;
                        gop.TypeOfTOLocationID = exRoute.TypeOfTOLocationID;
                        model.CAT_PriceDIExRouting.Add(gop);
                    }
                }
                #endregion

                #region CAT_PriceDIGroupProduct
                foreach (var i in model.CAT_PriceDIGroupProduct.Where(c => c.PriceID == item.ID).Select(c => new
                {
                    c.ContractRoutingID,
                    c.Price,
                    c.GroupOfProductID,
                    c.PriceMin,
                    c.PriceMax
                }).ToList())
                {
                    CAT_PriceDIGroupProduct o = new CAT_PriceDIGroupProduct();
                    model.CAT_PriceDIGroupProduct.Add(o);

                    o.CreatedBy = Account.UserName;
                    o.CreatedDate = DateTime.Now;

                    o.CAT_Price = obj;
                    o.Price = i.Price;
                    o.PriceMin = i.PriceMin;
                    o.PriceMax = i.PriceMax;
                    o.ContractRoutingID = i.ContractRoutingID;
                    o.GroupOfProductID = i.GroupOfProductID;
                }
                #endregion

                #region CAT_PriceDILevel +CAT_PriceDILevelGroupproduct

                foreach (var detail in model.CAT_PriceDILevelGroupProduct.Where(c => c.PriceID == objPrice.ID))
                {
                    CAT_PriceDILevelGroupProduct g = new CAT_PriceDILevelGroupProduct();
                    g.CreatedBy = Account.UserName;
                    g.CreatedDate = DateTime.Now;
                    g.ContractRoutingID = detail.ContractRoutingID;
                    g.GroupOfProductID = detail.GroupOfProductID;
                    g.Price = detail.Price;
                    g.ContractLevelID = detail.ContractLevelID;
                    g.CAT_Price = obj;
                    model.CAT_PriceDILevelGroupProduct.Add(g);
                }
                #endregion

                #region CAT_PriceGroupVehicle
                foreach (var i in model.CAT_PriceGroupVehicle.Where(c => c.PriceID == item.ID).Select(c => new
                {
                    c.ContractRoutingID,
                    c.Price,
                    c.GroupOfVehicleID,
                    c.PriceMin,
                    c.PriceMax
                }).ToList())
                {
                    CAT_PriceGroupVehicle o = new CAT_PriceGroupVehicle();
                    model.CAT_PriceGroupVehicle.Add(o);

                    o.CreatedBy = Account.UserName;
                    o.CreatedDate = DateTime.Now;

                    o.CAT_Price = obj;
                    o.Price = i.Price;
                    o.PriceMin = i.PriceMin;
                    o.PriceMax = i.PriceMax;
                    o.ContractRoutingID = i.ContractRoutingID;
                    o.GroupOfVehicleID = i.GroupOfVehicleID;
                }
                #endregion

                #region CAT_PriceGVLevel +CAT_PriceGVLevelGroupVehicle

                foreach (var detail in model.CAT_PriceGVLevelGroupVehicle.Where(c => c.PriceID == objPrice.ID))
                {
                    CAT_PriceGVLevelGroupVehicle g = new CAT_PriceGVLevelGroupVehicle();
                    g.CreatedBy = Account.UserName;
                    g.CreatedDate = DateTime.Now;
                    g.ContractRoutingID = detail.ContractRoutingID;
                    g.Price = detail.Price;
                    g.PriceMax = detail.PriceMax;
                    g.PriceMin = detail.PriceMin;
                    g.ContractLevelID = detail.ContractLevelID;
                    g.CAT_Price = obj;
                    model.CAT_PriceGVLevelGroupVehicle.Add(g);
                }
                #endregion

                #region CAT_PriceDILoad
                var listPriceDILoad = model.CAT_PriceDILoad.Where(c => c.PriceID == objPrice.ID).ToList();
                foreach (var i in listPriceDILoad)
                {
                    CAT_PriceDILoad o = new CAT_PriceDILoad();
                    model.CAT_PriceDILoad.Add(o);

                    o.CreatedBy = Account.UserName;
                    o.CreatedDate = DateTime.Now;

                    o.CAT_Price = obj;
                    o.LocationID = i.LocationID;
                    o.IsLoading = i.IsLoading;
                    o.ParentRoutingID = i.ParentRoutingID;
                    o.RoutingID = i.RoutingID;
                    o.GroupOfLocationID = i.GroupOfLocationID;

                    foreach (var e in model.CAT_PriceDILoadDetail.Where(c => c.PriceDILoadID == i.ID).Select(c => new
                    {
                        c.PriceOfGOPID,
                        c.Price,
                        c.GroupOfProductID
                    }).ToList())
                    {
                        CAT_PriceDILoadDetail g = new CAT_PriceDILoadDetail();

                        g.CreatedBy = Account.UserName;
                        g.CreatedDate = DateTime.Now;

                        g.PriceOfGOPID = e.PriceOfGOPID;
                        g.GroupOfProductID = e.GroupOfProductID;
                        g.Price = e.Price;
                        g.CAT_PriceDILoad = o;
                        model.CAT_PriceDILoadDetail.Add(g);
                    }
                }
                #endregion

                #region price moq
                var listMOQ = model.CAT_PriceDIMOQ.Where(c => c.PriceID == objPrice.ID).ToList();
                foreach (var moq in listMOQ)
                {
                    CAT_PriceDIMOQ m = new CAT_PriceDIMOQ();
                    m.CreatedBy = Account.UserName;
                    m.CreatedDate = DateTime.Now;
                    m.MOQName = moq.MOQName;
                    m.ParentRoutingID = moq.ParentRoutingID;
                    m.DIMOQSumID = moq.DIMOQSumID;
                    m.ExprInput = moq.ExprInput;
                    m.ExprPrice = moq.ExprPrice;
                    m.ExprPriceFix = moq.ExprPriceFix;
                    m.ExprQuan = moq.ExprQuan;
                    m.ExprTon = moq.ExprTon;
                    m.ExprCBM = moq.ExprCBM;
                    m.TypeOfPriceDIExID = moq.TypeOfPriceDIExID;
                    m.CAT_Price = obj;
                    model.CAT_PriceDIMOQ.Add(m);

                    foreach (var detail in moq.CAT_PriceDIMOQGroupLocation.Where(c => c.PriceDIMOQID == moq.ID))
                    {
                        CAT_PriceDIMOQGroupLocation gol = new CAT_PriceDIMOQGroupLocation();

                        gol.CreatedBy = Account.UserName;
                        gol.CreatedDate = DateTime.Now;
                        gol.GroupOfLocationID = detail.GroupOfLocationID;
                        gol.CAT_PriceDIMOQ = m;

                        model.CAT_PriceDIMOQGroupLocation.Add(gol);
                    }

                    foreach (var detail in moq.CAT_PriceDIMOQGroupProduct.Where(c => c.PriceDIMOQID == moq.ID))
                    {
                        CAT_PriceDIMOQGroupProduct gol = new CAT_PriceDIMOQGroupProduct();

                        gol.CreatedBy = Account.UserName;
                        gol.CreatedDate = DateTime.Now;
                        gol.GroupOfProductID = detail.GroupOfProductID;
                        gol.ExprPrice = detail.ExprPrice;
                        gol.ExprQuantity = detail.ExprQuantity;

                        gol.CAT_PriceDIMOQ = m;

                        model.CAT_PriceDIMOQGroupProduct.Add(gol);
                    }

                    foreach (var detail in moq.CAT_PriceDIMOQRouting.Where(c => c.PriceDIMOQID == moq.ID))
                    {
                        CAT_PriceDIMOQRouting gol = new CAT_PriceDIMOQRouting();

                        gol.CreatedBy = Account.UserName;
                        gol.CreatedDate = DateTime.Now;
                        gol.RoutingID = detail.RoutingID;
                        gol.ParentRoutingID = detail.ParentRoutingID;
                        gol.LocationID = detail.LocationID;
                        gol.TypeOfTOLocationID = detail.TypeOfTOLocationID;

                        gol.CAT_PriceDIMOQ = m;

                        model.CAT_PriceDIMOQRouting.Add(gol);
                    }
                }
                #endregion

                #region DI load moq
                var listMoqLoad = model.CAT_PriceDIMOQLoad.Where(c => c.PriceID == objPrice.ID).ToList();
                foreach (var moq in listMoqLoad)
                {
                    CAT_PriceDIMOQLoad m = new CAT_PriceDIMOQLoad();
                    m.CreatedBy = Account.UserName;
                    m.CreatedDate = DateTime.Now;
                    m.MOQName = moq.MOQName;
                    m.ParentRoutingID = moq.ParentRoutingID;
                    m.IsLoading = moq.IsLoading;
                    m.ExprInput = moq.ExprInput;
                    m.ExprPrice = moq.ExprPrice;
                    m.ExprPriceFix = moq.ExprPriceFix;
                    m.ExprQuan = moq.ExprQuan;
                    m.ExprTon = moq.ExprTon;
                    m.ExprCBM = moq.ExprCBM;
                    m.TypeOfPriceDIExID = moq.TypeOfPriceDIExID;
                    m.ParentRoutingID = moq.ParentRoutingID;
                    m.DIMOQLoadSumID = moq.DIMOQLoadSumID;
                    m.CAT_Price = obj;
                    model.CAT_PriceDIMOQLoad.Add(m);

                    foreach (var detail in moq.CAT_PriceDIMOQLoadGroupLocation.Where(c => c.PriceDIMOQLoadID == moq.ID))
                    {
                        CAT_PriceDIMOQLoadGroupLocation gol = new CAT_PriceDIMOQLoadGroupLocation();

                        gol.CreatedBy = Account.UserName;
                        gol.CreatedDate = DateTime.Now;
                        gol.GroupOfLocationID = detail.GroupOfLocationID;
                        gol.CAT_PriceDIMOQLoad = m;

                        model.CAT_PriceDIMOQLoadGroupLocation.Add(gol);
                    }

                    foreach (var detail in moq.CAT_PriceDIMOQLoadGroupProduct.Where(c => c.PriceDIMOQLoadID == moq.ID))
                    {
                        CAT_PriceDIMOQLoadGroupProduct gol = new CAT_PriceDIMOQLoadGroupProduct();

                        gol.CreatedBy = Account.UserName;
                        gol.CreatedDate = DateTime.Now;
                        gol.GroupOfProductID = detail.GroupOfProductID;
                        gol.ExprPrice = detail.ExprPrice;
                        gol.ExprQuantity = detail.ExprQuantity;

                        gol.CAT_PriceDIMOQLoad = m;

                        model.CAT_PriceDIMOQLoadGroupProduct.Add(gol);
                    }

                    foreach (var detail in moq.CAT_PriceDIMOQLoadRouting.Where(c => c.PriceDIMOQLoadID == moq.ID))
                    {
                        CAT_PriceDIMOQLoadRouting gol = new CAT_PriceDIMOQLoadRouting();

                        gol.CreatedBy = Account.UserName;
                        gol.CreatedDate = DateTime.Now;
                        gol.RoutingID = detail.RoutingID;
                        gol.ParentRoutingID = detail.ParentRoutingID;
                        gol.LocationID = detail.LocationID;

                        gol.CAT_PriceDIMOQLoad = m;

                        model.CAT_PriceDIMOQLoadRouting.Add(gol);
                    }
                }
                #endregion

                model.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static DTOPrice Contract_Price_Get(DataEntities model, AccountItem Account, int id)
        {
            try
            {
                DTOPrice result = new DTOPrice();

                int iHot = -(int)SYSVarType.TypeOfOrderHot;
                int iDirect = -(int)SYSVarType.TypeOfOrderDirect;
                int iReturn = -(int)SYSVarType.TypeOfOrderReturn;
                int iReturnHot = -(int)SYSVarType.TypeOfOrderReturnHot;

                int iFCL = -(int)SYSVarType.TransportModeFCL;
                int iFTL = -(int)SYSVarType.TransportModeFTL;
                int iLTL = -(int)SYSVarType.TransportModeLTL;

                int iMain = -(int)SYSVarType.TypeOfContractMain;
                int iFrame = -(int)SYSVarType.TypeOfContractFrame;

                var obj = model.CAT_Price.FirstOrDefault(c => c.ID == id);
                if (obj != null)
                {
                    result.ID = obj.ID;
                    result.ContractTermID = obj.CAT_ContractTerm.ID;
                    result.Code = obj.Code;
                    result.Name = obj.Name;
                    result.IsAllRouting = obj.CAT_ContractTerm.IsAllRouting;
                    result.CustomerID = obj.CAT_ContractTerm.CAT_Contract.CustomerID > 0 ? obj.CAT_ContractTerm.CAT_Contract.CustomerID.Value : -1;
                    result.ContractID = obj.CAT_ContractTerm.ContractID > 0 ? obj.CAT_ContractTerm.ContractID : -1;
                    result.ContractTermID = obj.ContractTermID;
                    result.EffectDate = obj.EffectDate;
                    result.TypeOfOrderID = obj.TypeOfOrderID;
                    result.ContractNo = obj.CAT_ContractTerm.CAT_Contract.ContractNo;
                    result.TypeOfOrderName = obj.SYS_Var.ValueOfVar;
                    result.TypeOfPrice = obj.TypeOfOrderID == iDirect ? 1 : obj.TypeOfOrderID == iHot ? 2 : obj.TypeOfOrderID == iReturn ? 3 : obj.TypeOfOrderID == iReturnHot ? 4 : 0;
                    result.TypeOfMode = obj.CAT_ContractTerm.CAT_Contract.CAT_TransportMode.TransportModeID == iFCL ? 1 : obj.CAT_ContractTerm.CAT_Contract.CAT_TransportMode.TransportModeID == iFTL ? 2 : obj.CAT_ContractTerm.CAT_Contract.CAT_TransportMode.TransportModeID == iLTL ? 3 : 0;
                    result.TypeOfContract = obj.CAT_ContractTerm.CAT_Contract.TypeOfContractID == iMain ? 1 : obj.CAT_ContractTerm.CAT_Contract.TypeOfContractID == iFrame ? 2 : 0;

                    result.TermClosed = obj.CAT_ContractTerm.IsClosed;

                    result.CheckPrice = new DTOPriceCheck();


                    result.CheckPrice.HasLevel = obj.CAT_ContractTerm.CAT_Contract.TypeOfRunLevelID > 0;
                    result.CheckPrice.HasNormal = obj.CAT_ContractTerm.CAT_Contract.TypeOfRunLevelID == null;
                    result.CheckPrice.HasMOQ = model.CAT_PriceDIMOQ.Count(c => c.PriceID == obj.ID) > 0;
                    result.CheckPrice.HasPriceEx = model.CAT_PriceDIEx.Count(c => c.PriceID == obj.ID) > 0;

                    result.CheckPrice.HasLoadLocation = model.CAT_PriceDILoad.Count(c => c.PriceID == id && c.IsLoading && c.LocationID > 0) > 0;
                    result.CheckPrice.HasLoadRoute = model.CAT_PriceDILoad.Count(c => c.PriceID == id && c.IsLoading && c.RoutingID > 0) > 0;
                    result.CheckPrice.HasLoadGOL = model.CAT_PriceDILoad.Count(c => c.PriceID == id && c.IsLoading && c.GroupOfLocationID > 0) > 0;
                    result.CheckPrice.HasLoadMOQ = model.CAT_PriceDIMOQLoad.Count(c => c.PriceID == id && c.IsLoading) > 0;
                    result.CheckPrice.HasLoadPAR = model.CAT_PriceDILoad.Count(c => c.PriceID == id && c.IsLoading && c.PartnerID > 0) > 0;


                    result.CheckPrice.HasUnLoadLocation = model.CAT_PriceDILoad.Count(c => c.PriceID == id && !c.IsLoading && c.LocationID > 0) > 0;
                    result.CheckPrice.HasUnLoadRoute = model.CAT_PriceDILoad.Count(c => c.PriceID == id && !c.IsLoading && c.RoutingID > 0) > 0;
                    result.CheckPrice.HasUnLoadGOL = model.CAT_PriceDILoad.Count(c => c.PriceID == id && !c.IsLoading && c.GroupOfLocationID > 0) > 0;
                    result.CheckPrice.HasUnLoadMOQ = model.CAT_PriceDIMOQLoad.Count(c => c.PriceID == id && !c.IsLoading) > 0;
                    result.CheckPrice.HasUnLoadPAR = model.CAT_PriceDILoad.Count(c => c.PriceID == id && !c.IsLoading && c.PartnerID > 0) > 0;
                    //dem tiep
                }
                else
                {
                    result.ID = -1;
                    result.Code = "";
                    result.Name = "";
                    result.EffectDate = DateTime.Now.Date;
                    result.TypeOfOrderID = iDirect;
                    result.TypeOfPrice = 1;
                }
                return result;
            }
            catch (Exception ex)
            {
                throw FaultHelper.BusinessFault(ex);
            }
        }

        //Import Load và UnLoad
        public static void Price_DI_Load_Import(DataEntities model, AccountItem Account, List<DTOPriceTruckDILoad_Import> data, int priceID, int type, bool isLoading)
        {
            try
            {
                if (data != null)
                {
                    if (data.Count(c => !c.ExcelSuccess) > 0)
                    {
                        throw FaultHelper.BusinessFault(null, null, "Dữ liệu lỗi không thể lưu");
                    }
                    //type 1: location, 2 route, 3 GroupOfLocation
                    foreach (var item in data.Where(c => c.ExcelSuccess))
                    {
                        var obj = model.CAT_PriceDILoad.FirstOrDefault(c => c.PriceID == priceID 
                            && (type == 1 ? c.LocationID == item.LocationID : true)
                            && (type == 2 ? c.RoutingID == item.RoutingID : true)
                            && (type == 3 ? c.GroupOfLocationID == item.GroupOfLocationID : true) 
                            && c.IsLoading == isLoading);
                        if (obj == null)
                        {
                            obj = new CAT_PriceDILoad();
                            obj.CreatedBy = Account.UserName;
                            obj.CreatedDate = DateTime.Now;
                            obj.PriceID = priceID;
                            switch (type)
                            {
                                case 1:
                                    obj.LocationID = item.LocationID; 
                                    break;
                                case 2:
                                    obj.RoutingID = item.RoutingID;
                                    break;
                                case 3:
                                    obj.GroupOfLocationID = item.GroupOfLocationID;
                                    break;
                                default:
                                    break;
                            }
                            obj.IsLoading = isLoading;
                            model.CAT_PriceDILoad.Add(obj);
                        }
                        item.ID = obj.ID;
                    }
                    model.SaveChanges();

                    foreach (var item in data.Where(c => c.ExcelSuccess))
                    {
                        var obj = model.CAT_PriceDILoad.FirstOrDefault(c => (type == 1 ? c.LocationID == item.LocationID : true)
                            && (type == 2 ? c.RoutingID == item.RoutingID : true)
                            && (type == 3 ? c.GroupOfLocationID == item.GroupOfLocationID : true)  
                            && c.PriceID == priceID && c.IsLoading == isLoading);

                        foreach (var group in item.ListPriceTruckLoadingDetail)
                        {
                            //Đã có Detail => gán Price = 0; PriceOfGOPID = mặc định.
                            var objDetail = model.CAT_PriceDILoadDetail.FirstOrDefault(c => c.GroupOfProductID == group.GroupOfProductID && c.PriceDILoadID == obj.ID);
                            if (objDetail == null)
                            {
                                objDetail = new CAT_PriceDILoadDetail();
                                objDetail.CreatedBy = Account.UserName;
                                objDetail.CreatedDate = DateTime.Now;
                                objDetail.GroupOfProductID = group.GroupOfProductID;
                                objDetail.PriceDILoadID = obj.ID;
                                model.CAT_PriceDILoadDetail.Add(objDetail);
                            }
                            objDetail.PriceOfGOPID = group.PriceOfGOPID;
                            objDetail.Price = group.Price;
                        }
                    }

                    model.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw FaultHelper.BusinessFault(ex);
            }
        }
        #region Loading Up

        #region Location
        public static List<DTOPriceTruckDILoad> Price_DI_LoadLocation_List(DataEntities model, AccountItem Account, int priceID)
        {
            try
            {
                List<DTOPriceTruckDILoad> result = new List<DTOPriceTruckDILoad>();

                var query = model.CAT_PriceDILoad.Where(c => c.PriceID == priceID && c.IsLoading && c.LocationID.HasValue).ToList();
                foreach (var item in query)
                {
                    DTOPriceTruckDILoad obj = new DTOPriceTruckDILoad();
                    obj.ID = item.ID;
                    obj.IsLoading = item.IsLoading;
                    obj.PriceID = item.PriceID;
                    obj.LocationID = item.LocationID;
                    //fix 18/5 cus->cat
                    obj.LocationCode = item.LocationID.HasValue ? item.CAT_Location.Code : string.Empty;
                    obj.LocationName = item.LocationID.HasValue ? item.CAT_Location.Location : string.Empty;
                    obj.Address = item.LocationID.HasValue ? item.CAT_Location.Address : string.Empty;
                    obj.ListPriceTruckLoadingDetail = new List<DTOPriceTruckDILoadDetail>();
                    foreach (var loadGroup in item.CAT_PriceDILoadDetail)
                    {
                        DTOPriceTruckDILoadDetail detail = new DTOPriceTruckDILoadDetail();
                        detail.ID = loadGroup.ID;
                        detail.PriceDILoadID = loadGroup.PriceDILoadID;
                        detail.CustomerID = loadGroup.CUS_GroupOfProduct.CustomerID;
                        detail.CustomerName = loadGroup.CUS_GroupOfProduct.CUS_Customer.CustomerName;
                        detail.GroupOfProductID = loadGroup.GroupOfProductID;
                        detail.GroupOfProductCode = loadGroup.CUS_GroupOfProduct.Code;
                        detail.GroupOfProductName = loadGroup.CUS_GroupOfProduct.GroupName;
                        detail.PriceOfGOPCode = loadGroup.SYS_Var.Code;
                        detail.PriceOfGOPName = loadGroup.SYS_Var.ValueOfVar;
                        detail.PriceOfGOPID = loadGroup.PriceOfGOPID;
                        detail.Price = loadGroup.Price;
                        obj.ListPriceTruckLoadingDetail.Add(detail);
                    }

                    result.Add(obj);
                }
                return result;
            }
            catch (Exception ex)
            {
                throw FaultHelper.BusinessFault(ex);
            }
        }
        public static void Price_DI_LoadLocation_SaveList(DataEntities model, AccountItem Account, List<DTOPriceTruckDILoad> data)
        {
            try
            {
                foreach (var item in data)
                {
                    if (item.ListPriceTruckLoadingDetail != null && item.ListPriceTruckLoadingDetail.Count > 0)
                    {
                        foreach (var detail in item.ListPriceTruckLoadingDetail)
                        {
                            var obj = model.CAT_PriceDILoadDetail.FirstOrDefault(c => c.PriceDILoadID == item.ID && c.GroupOfProductID == detail.GroupOfProductID);
                            if (obj == null)
                            {
                                obj = new CAT_PriceDILoadDetail();
                                obj.CreatedBy = Account.UserName;
                                obj.CreatedDate = DateTime.Now;
                                obj.PriceDILoadID = item.ID;
                                obj.GroupOfProductID = detail.GroupOfProductID;
                                model.CAT_PriceDILoadDetail.Add(obj);
                            }
                            else
                            {
                                obj.ModifiedBy = Account.UserName;
                                obj.ModifiedDate = DateTime.Now;
                            }
                            obj.PriceOfGOPID = detail.PriceOfGOPID;
                            obj.Price = detail.Price;
                        }
                    }
                }
                model.SaveChanges();
            }
            catch (Exception ex)
            {
                throw FaultHelper.BusinessFault(ex);
            }
        }
        public static DTOResult Price_DI_LoadLocation_LocationNotIn_List(DataEntities model, AccountItem Account, string request, int priceID, bool isVen)
        {
            try
            {
                DTOResult result = new DTOResult();
                var objPrice = model.CAT_Price.FirstOrDefault(c => c.ID == priceID);
                if (objPrice != null)
                {
                    int CustomerID = objPrice.CAT_ContractTerm.CAT_Contract.CustomerID.Value;
                    if (isVen)
                    {
                        int CompanyID = objPrice.CAT_ContractTerm.CAT_Contract.CompanyID > 0 ? objPrice.CAT_ContractTerm.CAT_Contract.CUS_Company.CustomerRelateID : -1;
                        var lstID = new List<int>();

                        if (CompanyID > 0)
                        {
                            lstID.Add(CompanyID);
                        }
                        else
                        {
                            lstID = model.CUS_Company.Where(c => c.CustomerOwnID == CustomerID).Select(c => c.CustomerRelateID).Distinct().ToList();
                        }


                        var lstLocationID = objPrice.CAT_PriceDILoad.Where(c => c.IsLoading && c.LocationID.HasValue).Select(c => c.LocationID).Distinct().ToList();
                        var query = model.CUS_Location.Where(c => lstID.Contains(c.CustomerID) && !lstLocationID.Contains(c.LocationID)).Select(c => new DTOCATLocation
                        {
                            ID = c.LocationID,
                            Code = c.Code,
                            Location = c.LocationName,
                            Address = c.CAT_Location.Address,
                        }).Distinct().ToDataSourceResult(CreateRequest(request));

                        result.Total = query.Total;
                        result.Data = query.Data as IEnumerable<DTOCATLocation>;
                    }
                    else
                    {
                        var lstLocationID = objPrice.CAT_PriceDILoad.Where(c => c.IsLoading && c.LocationID.HasValue).Select(c => c.LocationID).Distinct().ToList();
                        var query = model.CUS_Location.Where(c => c.CustomerID == CustomerID && !lstLocationID.Contains(c.LocationID)).Select(c => new DTOCATLocation
                        {
                            ID = c.LocationID,
                            Code = c.Code,
                            Location = c.LocationName,
                            Address = c.CAT_Location.Address,
                        }).Distinct().ToDataSourceResult(CreateRequest(request));

                        result.Total = query.Total;
                        result.Data = query.Data as IEnumerable<DTOCATLocation>;
                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                throw FaultHelper.BusinessFault(ex);
            }
        }
        public static void Price_DI_LoadLocation_LocationNotIn_SaveList(DataEntities model, AccountItem Account, List<int> data, int priceID)
        {
            try
            {
                var customerID = model.CAT_Price.FirstOrDefault(c => c.ID == priceID).CAT_ContractTerm.CAT_Contract.CustomerID;
                var lstGroupOfProduct = model.CUS_GroupOfProduct.Where(c => c.CustomerID == customerID && c.PriceOfGOPID>0);
                foreach (var item in data)
                {
                    CAT_PriceDILoad obj = new CAT_PriceDILoad();
                    obj.CreatedBy = Account.UserName;
                    obj.CreatedDate = DateTime.Now;
                    obj.PriceID = priceID;
                    obj.LocationID = item;
                    obj.IsLoading = true;
                    model.CAT_PriceDILoad.Add(obj);

                    foreach (var group in lstGroupOfProduct)
                    {
                        CAT_PriceDILoadDetail objDetail = new CAT_PriceDILoadDetail();
                        objDetail.CreatedBy = Account.UserName;
                        objDetail.CreatedDate = DateTime.Now;
                        objDetail.GroupOfProductID = group.ID;
                        objDetail.PriceOfGOPID = group.PriceOfGOPID;
                        obj.CAT_PriceDILoadDetail.Add(objDetail);
                    }
                }
                model.SaveChanges();
            }
            catch (Exception ex)
            {
                throw FaultHelper.BusinessFault(ex);
            }
        }
        public static void Price_DI_LoadLocation_DeleteList(DataEntities model, AccountItem Account, int priceID)
        {
            try
            {
                var query = model.CAT_Price.Where(c => c.ID == priceID).FirstOrDefault();
                if (query != null)
                {
                    foreach (var diLoad in model.CAT_PriceDILoad.Where(c => c.PriceID == query.ID && c.IsLoading && c.LocationID > 0))
                    {
                        foreach (var detail in model.CAT_PriceDILoadDetail.Where(c => c.PriceDILoadID == diLoad.ID))
                            model.CAT_PriceDILoadDetail.Remove(detail);
                        model.CAT_PriceDILoad.Remove(diLoad);
                    }
                    model.SaveChanges();
                }
                else
                    throw FaultHelper.BusinessFault(null, null, "Không tìm thấy bảng giá");
            }
            catch (Exception ex)
            {
                throw FaultHelper.BusinessFault(ex);
            }
        }

        //Excel
        public static DTOPriceTruckDILoad_Export Price_DI_LoadLocation_Export(DataEntities model, AccountItem Account, int contractTermID, int priceID, bool isVen)
        {
            try
            {
                DTOPriceTruckDILoad_Export result = new DTOPriceTruckDILoad_Export();
                result.ListData = new List<DTOPriceTruckDILoad>();
                result.ListGroupProduct = new List<DTOCUSGroupOfProduct>();

                var objContractTerm = model.CAT_ContractTerm.FirstOrDefault(c => c.ID == contractTermID);
                if (objContractTerm == null) throw FaultHelper.BusinessFault(null, null, "Không tìm thấy phụ lục ID:" + contractTermID);

                var objContract = model.CAT_Contract.FirstOrDefault(c => c.ID == objContractTerm.ContractID);
                if (objContract == null) throw FaultHelper.BusinessFault(null, null, "Không tìm thấy hợp đồng ");

                int cusID = -1;
                if (objContract != null && objContract.CustomerID.HasValue)
                    cusID = objContract.CustomerID.Value;

                var listGOPMapping = new List<int>();
                if (isVen)
                {
                    if (objContract.CompanyID > 0)
                    {
                        var CustomerRelateID = objContract.CUS_Company.CustomerRelateID;
                        listGOPMapping = model.CUS_GroupOfProductMapping.Where(c => c.VendorID == objContract.CustomerID && c.CustomerID == CustomerRelateID).Select(c => c.GroupOfProductVENID).ToList();
                    }
                }

                result.ListGroupProduct = model.CUS_GroupOfProduct.Where(c => c.CustomerID == cusID && (!isVen ? true : (objContract.CompanyID == null ? true : listGOPMapping.Contains(c.ID)))).Select(c => new DTOCUSGroupOfProduct
                {
                    ID = c.ID,
                    Code = c.Code,
                    GroupName = c.GroupName,
                    PriceOfGOPID = c.PriceOfGOPID,
                    PriceOfGOPName = c.SYS_Var.ValueOfVar
                }).ToList();

                var query = model.CAT_PriceDILoad.Where(c => c.PriceID == priceID && c.IsLoading && c.LocationID.HasValue).ToList();
                foreach (var item in query)
                {
                    DTOPriceTruckDILoad obj = new DTOPriceTruckDILoad();
                    obj.ID = item.ID;
                    obj.IsLoading = item.IsLoading;
                    obj.PriceID = item.PriceID;
                    obj.LocationID = item.LocationID;
                    //fix 18/5 cus->cat
                    obj.LocationCode = item.LocationID.HasValue ? item.CAT_Location.Code : string.Empty;
                    obj.LocationName = item.LocationID.HasValue ? item.CAT_Location.Location : string.Empty;
                    obj.Address = item.LocationID.HasValue ? item.CAT_Location.Address : string.Empty;
                    obj.ListPriceTruckLoadingDetail = new List<DTOPriceTruckDILoadDetail>();
                    foreach (var loadGroup in item.CAT_PriceDILoadDetail)
                    {
                        DTOPriceTruckDILoadDetail detail = new DTOPriceTruckDILoadDetail();
                        detail.ID = loadGroup.ID;
                        detail.PriceDILoadID = loadGroup.PriceDILoadID;
                        detail.CustomerID = loadGroup.CUS_GroupOfProduct.CustomerID;
                        detail.CustomerName = loadGroup.CUS_GroupOfProduct.CUS_Customer.CustomerName;
                        detail.GroupOfProductID = loadGroup.GroupOfProductID;
                        detail.GroupOfProductCode = loadGroup.CUS_GroupOfProduct.Code;
                        detail.GroupOfProductName = loadGroup.CUS_GroupOfProduct.GroupName;
                        detail.PriceOfGOPCode = loadGroup.SYS_Var.Code;
                        detail.PriceOfGOPName = loadGroup.SYS_Var.ValueOfVar;
                        detail.PriceOfGOPID = loadGroup.PriceOfGOPID;
                        detail.Price = loadGroup.Price;
                        obj.ListPriceTruckLoadingDetail.Add(detail);
                    }

                    result.ListData.Add(obj);
                }
                return result;
            }
            catch (Exception ex)
            {
                throw FaultHelper.BusinessFault(ex);
            }
        }
        #endregion

        #region Route
        public static List<DTOPriceTruckDILoad> Price_DI_LoadRoute_List(DataEntities model, AccountItem Account, int priceID)
        {
            try
            {
                List<DTOPriceTruckDILoad> result = new List<DTOPriceTruckDILoad>();
                var query = model.CAT_PriceDILoad.Where(c => c.PriceID == priceID && c.IsLoading && c.RoutingID.HasValue).ToList();
                foreach (var item in query)
                {
                    DTOPriceTruckDILoad obj = new DTOPriceTruckDILoad();
                    obj.ID = item.ID;
                    obj.IsLoading = item.IsLoading;
                    obj.PriceID = item.PriceID;
                    obj.RoutingID = item.RoutingID;
                    obj.RoutingCode = item.RoutingID.HasValue ? item.CAT_Routing1.Code : string.Empty;
                    obj.RoutingName = item.RoutingID.HasValue ? item.CAT_Routing1.RoutingName : string.Empty;
                    obj.ListPriceTruckLoadingDetail = new List<DTOPriceTruckDILoadDetail>();
                    foreach (var loadGroup in item.CAT_PriceDILoadDetail)
                    {
                        DTOPriceTruckDILoadDetail detail = new DTOPriceTruckDILoadDetail();
                        detail.ID = loadGroup.ID;
                        detail.PriceDILoadID = loadGroup.PriceDILoadID;
                        detail.CustomerID = loadGroup.CUS_GroupOfProduct.CustomerID;
                        detail.CustomerName = loadGroup.CUS_GroupOfProduct.CUS_Customer.CustomerName;
                        detail.GroupOfProductID = loadGroup.GroupOfProductID;
                        detail.GroupOfProductCode = loadGroup.CUS_GroupOfProduct.Code;
                        detail.GroupOfProductName = loadGroup.CUS_GroupOfProduct.GroupName;
                        detail.PriceOfGOPCode = loadGroup.SYS_Var.Code;
                        detail.PriceOfGOPName = loadGroup.SYS_Var.ValueOfVar;
                        detail.PriceOfGOPID = loadGroup.PriceOfGOPID;
                        detail.Price = loadGroup.Price;
                        obj.ListPriceTruckLoadingDetail.Add(detail);
                    }

                    result.Add(obj);
                }
                return result;
            }
            catch (Exception ex)
            {
                throw FaultHelper.BusinessFault(ex);
            }
        }
        public static void Price_DI_LoadRoute_SaveList(DataEntities model, AccountItem Account, List<DTOPriceTruckDILoad> data)
        {
            try
            {
                foreach (var item in data)
                {
                    if (item.ListPriceTruckLoadingDetail != null && item.ListPriceTruckLoadingDetail.Count > 0)
                    {
                        foreach (var detail in item.ListPriceTruckLoadingDetail)
                        {
                            var obj = model.CAT_PriceDILoadDetail.FirstOrDefault(c => c.PriceDILoadID == item.ID && c.GroupOfProductID == detail.GroupOfProductID);
                            if (obj == null)
                            {
                                obj = new CAT_PriceDILoadDetail();
                                obj.CreatedBy = Account.UserName;
                                obj.CreatedDate = DateTime.Now;
                                obj.PriceDILoadID = item.ID;
                                obj.GroupOfProductID = detail.GroupOfProductID;
                                model.CAT_PriceDILoadDetail.Add(obj);
                            }
                            else
                            {
                                obj.ModifiedBy = Account.UserName;
                                obj.ModifiedDate = DateTime.Now;
                            }
                            obj.PriceOfGOPID = detail.PriceOfGOPID;
                            obj.Price = detail.Price;
                        }
                    }
                }
                model.SaveChanges();
            }
            catch (Exception ex)
            {
                throw FaultHelper.BusinessFault(ex);
            }
        }
        public static DTOResult Price_DI_LoadRoute_RouteNotIn_List(DataEntities model, AccountItem Account, string request, int priceID)
        {
            try
            {
                DTOResult result = new DTOResult();
                var objPrice = model.CAT_Price.FirstOrDefault(c => c.ID == priceID);
                if (objPrice != null)
                {
                    int ContractID = objPrice.CAT_ContractTerm.ContractID;
                    var lstRouteID = objPrice.CAT_PriceDILoad.Where(c => c.IsLoading && c.RoutingID.HasValue).Select(c => c.RoutingID).Distinct().ToList();
                    if (objPrice.CAT_ContractTerm.IsAllRouting)
                    {
                        var query = model.CAT_ContractRouting.Where(c => c.ContractID == ContractID && !lstRouteID.Contains(c.RoutingID) && c.ContractRoutingTypeID == -(int)SYSVarType.ContractRoutingTypePrice).Select(c => new DTOPriceRoute
                        {
                            RouteID = c.RoutingID,
                            RouteCode = c.Code,
                            RouteName = c.RoutingName,
                            CATRouteCode = c.CAT_Routing.Code,
                            CATRouteName = c.CAT_Routing.RoutingName,
                        }).ToDataSourceResult(CreateRequest(request));
                        result.Total = query.Total;
                        result.Data = query.Data as IEnumerable<DTOPriceRoute>;
                    }
                    else
                    {
                        var query = model.CAT_ContractRouting.Where(c => c.ContractTermID == objPrice.ContractTermID && !lstRouteID.Contains(c.RoutingID) && c.ContractRoutingTypeID == -(int)SYSVarType.ContractRoutingTypePrice).Select(c => new DTOPriceRoute
                        {
                            RouteID = c.RoutingID,
                            RouteCode = c.Code,
                            RouteName = c.RoutingName,
                            CATRouteCode = c.CAT_Routing.Code,
                            CATRouteName = c.CAT_Routing.RoutingName,
                        }).ToDataSourceResult(CreateRequest(request));
                        result.Total = query.Total;
                        result.Data = query.Data as IEnumerable<DTOPriceRoute>;
                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                throw FaultHelper.BusinessFault(ex);
            }
        }
        public static void Price_DI_LoadRoute_RouteNotIn_SaveList(DataEntities model, AccountItem Account, List<int> data, int priceID)
        {
            try
            {
                var customerID = model.CAT_Price.FirstOrDefault(c => c.ID == priceID).CAT_ContractTerm.CAT_Contract.CustomerID;
                var lstGroupOfProduct = model.CUS_GroupOfProduct.Where(c => c.CustomerID == customerID && c.PriceOfGOPID>0);
                foreach (var item in data)
                {
                    CAT_PriceDILoad obj = new CAT_PriceDILoad();
                    obj.CreatedBy = Account.UserName;
                    obj.CreatedDate = DateTime.Now;
                    obj.PriceID = priceID;
                    obj.RoutingID = item;
                    obj.IsLoading = true;
                    model.CAT_PriceDILoad.Add(obj);

                    foreach (var group in lstGroupOfProduct)
                    {
                        CAT_PriceDILoadDetail objDetail = new CAT_PriceDILoadDetail();
                        objDetail.CreatedBy = Account.UserName;
                        objDetail.CreatedDate = DateTime.Now;
                        objDetail.GroupOfProductID = group.ID;
                        objDetail.PriceOfGOPID = group.PriceOfGOPID;
                        obj.CAT_PriceDILoadDetail.Add(objDetail);
                    }
                }
                model.SaveChanges();
            }
            catch (Exception ex)
            {
                throw FaultHelper.BusinessFault(ex);
            }
        }
        public static void Price_DI_LoadRoute_DeleteList(DataEntities model, AccountItem Account, int priceID)
        {
            try
            {
                var query = model.CAT_Price.Where(c => c.ID == priceID).FirstOrDefault();
                if (query != null)
                {
                    foreach (var diLoad in model.CAT_PriceDILoad.Where(c => c.PriceID == query.ID && c.IsLoading && c.RoutingID > 0))
                    {
                        foreach (var detail in model.CAT_PriceDILoadDetail.Where(c => c.PriceDILoadID == diLoad.ID))
                            model.CAT_PriceDILoadDetail.Remove(detail);
                        model.CAT_PriceDILoad.Remove(diLoad);
                    }
                    model.SaveChanges();
                }
                else
                    throw FaultHelper.BusinessFault(null, null, "Không tìm thấy bảng giá");
            }
            catch (Exception ex)
            {
                throw FaultHelper.BusinessFault(ex);
            }
        }

        //Excel
        public static DTOPriceTruckDILoad_Export Price_DI_LoadRoute_Export(DataEntities model, AccountItem Account, int contractTermID, int priceID, bool isVen)
        {
            try
            {
                DTOPriceTruckDILoad_Export result = new DTOPriceTruckDILoad_Export();
                result.ListData = new List<DTOPriceTruckDILoad>();
                result.ListGroupProduct = new List<DTOCUSGroupOfProduct>();

                var objContractTerm = model.CAT_ContractTerm.FirstOrDefault(c => c.ID == contractTermID);
                if (objContractTerm == null) throw FaultHelper.BusinessFault(null, null, "Không tìm thấy phụ lục ID:" + contractTermID);

                var objContract = model.CAT_Contract.FirstOrDefault(c => c.ID == objContractTerm.ContractID);
                if (objContract == null) throw FaultHelper.BusinessFault(null, null, "Không tìm thấy hợp đồng ");

                int cusID = -1;
                if (objContract != null && objContract.CustomerID.HasValue)
                    cusID = objContract.CustomerID.Value;

                var listGOPMapping = new List<int>();
                if (isVen)
                {
                    if (objContract.CompanyID > 0)
                    {
                        var CustomerRelateID = objContract.CUS_Company.CustomerRelateID;
                        listGOPMapping = model.CUS_GroupOfProductMapping.Where(c => c.VendorID == objContract.CustomerID && c.CustomerID == CustomerRelateID).Select(c => c.GroupOfProductVENID).ToList();
                    }
                }

                result.ListGroupProduct = model.CUS_GroupOfProduct.Where(c => c.CustomerID == cusID && (!isVen ? true : (objContract.CompanyID == null ? true : listGOPMapping.Contains(c.ID)))).Select(c => new DTOCUSGroupOfProduct
                {
                    ID = c.ID,
                    Code = c.Code,
                    GroupName = c.GroupName,
                    PriceOfGOPID = c.PriceOfGOPID,
                    PriceOfGOPName = c.SYS_Var.ValueOfVar
                }).ToList();

                var query = model.CAT_PriceDILoad.Where(c => c.PriceID == priceID && c.IsLoading && c.RoutingID.HasValue).ToList();
                foreach (var item in query)
                {
                    DTOPriceTruckDILoad obj = new DTOPriceTruckDILoad();
                    obj.ID = item.ID;
                    obj.IsLoading = item.IsLoading;
                    obj.PriceID = item.PriceID;
                    obj.RoutingID = item.RoutingID;
                    obj.RoutingCode = item.RoutingID.HasValue ? item.CAT_Routing1.Code : string.Empty;
                    obj.RoutingName = item.RoutingID.HasValue ? item.CAT_Routing1.RoutingName : string.Empty;
                    obj.ListPriceTruckLoadingDetail = new List<DTOPriceTruckDILoadDetail>();
                    foreach (var loadGroup in item.CAT_PriceDILoadDetail)
                    {
                        DTOPriceTruckDILoadDetail detail = new DTOPriceTruckDILoadDetail();
                        detail.ID = loadGroup.ID;
                        detail.PriceDILoadID = loadGroup.PriceDILoadID;
                        detail.CustomerID = loadGroup.CUS_GroupOfProduct.CustomerID;
                        detail.CustomerName = loadGroup.CUS_GroupOfProduct.CUS_Customer.CustomerName;
                        detail.GroupOfProductID = loadGroup.GroupOfProductID;
                        detail.GroupOfProductCode = loadGroup.CUS_GroupOfProduct.Code;
                        detail.GroupOfProductName = loadGroup.CUS_GroupOfProduct.GroupName;
                        detail.PriceOfGOPCode = loadGroup.SYS_Var.Code;
                        detail.PriceOfGOPName = loadGroup.SYS_Var.ValueOfVar;
                        detail.PriceOfGOPID = loadGroup.PriceOfGOPID;
                        detail.Price = loadGroup.Price;
                        obj.ListPriceTruckLoadingDetail.Add(detail);
                    }

                    result.ListData.Add(obj);
                }
                return result;
            }
            catch (Exception ex)
            {
                throw FaultHelper.BusinessFault(ex);
            }
        }
        #endregion

        #region TypeOfPartner
        public static List<DTOPriceTruckDILoad> Price_DI_LoadPartner_List(DataEntities model, AccountItem Account, int priceID)
        {
            try
            {
                List<DTOPriceTruckDILoad> result = new List<DTOPriceTruckDILoad>();
                var query = model.CAT_PriceDILoad.Where(c => c.PriceID == priceID && c.IsLoading && c.GroupOfLocationID.HasValue).ToList();
                foreach (var item in query)
                {
                    DTOPriceTruckDILoad obj = new DTOPriceTruckDILoad();
                    obj.ID = item.ID;
                    obj.IsLoading = item.IsLoading;
                    obj.PriceID = item.PriceID;
                    obj.GroupOfLocationID = item.GroupOfLocationID;
                    obj.GroupOfLocationCode = item.GroupOfLocationID.HasValue ? item.CAT_GroupOfLocation.Code : string.Empty;
                    obj.GroupOfLocationName = item.GroupOfLocationID.HasValue ? item.CAT_GroupOfLocation.GroupName : string.Empty;
                    obj.ListPriceTruckLoadingDetail = new List<DTOPriceTruckDILoadDetail>();
                    foreach (var loadGroup in item.CAT_PriceDILoadDetail)
                    {
                        DTOPriceTruckDILoadDetail detail = new DTOPriceTruckDILoadDetail();
                        detail.ID = loadGroup.ID;
                        detail.PriceDILoadID = loadGroup.PriceDILoadID;
                        detail.CustomerID = loadGroup.CUS_GroupOfProduct.CustomerID;
                        detail.CustomerName = loadGroup.CUS_GroupOfProduct.CUS_Customer.CustomerName;
                        detail.GroupOfProductID = loadGroup.GroupOfProductID;
                        detail.GroupOfProductCode = loadGroup.CUS_GroupOfProduct.Code;
                        detail.GroupOfProductName = loadGroup.CUS_GroupOfProduct.GroupName;
                        detail.PriceOfGOPCode = loadGroup.SYS_Var.Code;
                        detail.PriceOfGOPName = loadGroup.SYS_Var.ValueOfVar;
                        detail.PriceOfGOPID = loadGroup.PriceOfGOPID;
                        detail.Price = loadGroup.Price;
                        obj.ListPriceTruckLoadingDetail.Add(detail);
                    }

                    result.Add(obj);
                }
                return result;
            }
            catch (Exception ex)
            {
                throw FaultHelper.BusinessFault(ex);
            }
        }
        public static void Price_DI_LoadPartner_SaveList(DataEntities model, AccountItem Account, List<DTOPriceTruckDILoad> data)
        {
            try
            {
                foreach (var item in data)
                {
                    if (item.ListPriceTruckLoadingDetail != null && item.ListPriceTruckLoadingDetail.Count > 0)
                    {
                        foreach (var detail in item.ListPriceTruckLoadingDetail)
                        {
                            var obj = model.CAT_PriceDILoadDetail.FirstOrDefault(c => c.PriceDILoadID == item.ID && c.GroupOfProductID == detail.GroupOfProductID);
                            if (obj == null)
                            {
                                obj = new CAT_PriceDILoadDetail();
                                obj.CreatedBy = Account.UserName;
                                obj.CreatedDate = DateTime.Now;
                                obj.PriceDILoadID = item.ID;
                                obj.GroupOfProductID = detail.GroupOfProductID;
                                model.CAT_PriceDILoadDetail.Add(obj);
                            }
                            else
                            {
                                obj.ModifiedBy = Account.UserName;
                                obj.ModifiedDate = DateTime.Now;
                            }
                            obj.PriceOfGOPID = detail.PriceOfGOPID;
                            obj.Price = detail.Price;
                        }
                    }
                }
                model.SaveChanges();
            }
            catch (Exception ex)
            {
                throw FaultHelper.BusinessFault(ex);
            }
        }
        public static DTOResult Price_DI_LoadPartner_PartnerNotIn_List(DataEntities model, AccountItem Account, string request, int priceID)
        {
            try
            {
                DTOResult result = new DTOResult();
                var objPrice = model.CAT_Price.FirstOrDefault(c => c.ID == priceID);
                if (objPrice != null)
                {
                    int ContractID = objPrice.CAT_ContractTerm.ContractID;
                    var lstRouteID = objPrice.CAT_PriceDILoad.Where(c => c.IsLoading && c.GroupOfLocationID.HasValue).Select(c => c.GroupOfLocationID).Distinct().ToList();
                    var query = model.CAT_GroupOfLocation.Where(c => !lstRouteID.Contains(c.ID)).Select(c => new DTOCATGroupOfLocation
                    {
                        ID = c.ID,
                        Code = c.Code,
                        GroupName = c.GroupName
                    }).ToDataSourceResult(CreateRequest(request));
                    result.Total = query.Total;
                    result.Data = query.Data as IEnumerable<DTOCATGroupOfLocation>;
                }
                return result;
            }
            catch (Exception ex)
            {
                throw FaultHelper.BusinessFault(ex);
            }
        }
        public static void Price_DI_LoadPartner_PartnerNotIn_SaveList(DataEntities model, AccountItem Account, List<int> data, int priceID)
        {
            try
            {
                var customerID = model.CAT_Price.FirstOrDefault(c => c.ID == priceID).CAT_ContractTerm.CAT_Contract.CustomerID;
                var lstGroupOfProduct = model.CUS_GroupOfProduct.Where(c => c.CustomerID == customerID && c.PriceOfGOPID>0);
                foreach (var item in data)
                {
                    CAT_PriceDILoad obj = new CAT_PriceDILoad();
                    obj.CreatedBy = Account.UserName;
                    obj.CreatedDate = DateTime.Now;
                    obj.PriceID = priceID;
                    obj.GroupOfLocationID = item;
                    obj.IsLoading = true;
                    model.CAT_PriceDILoad.Add(obj);

                    foreach (var group in lstGroupOfProduct)
                    {
                        CAT_PriceDILoadDetail objDetail = new CAT_PriceDILoadDetail();
                        objDetail.CreatedBy = Account.UserName;
                        objDetail.CreatedDate = DateTime.Now;
                        objDetail.GroupOfProductID = group.ID;
                        objDetail.PriceOfGOPID = group.PriceOfGOPID;
                        obj.CAT_PriceDILoadDetail.Add(objDetail);
                    }
                }
                model.SaveChanges();
            }
            catch (Exception ex)
            {
                throw FaultHelper.BusinessFault(ex);
            }
        }
        public static void Price_DI_LoadPartner_DeleteList(DataEntities model, AccountItem Account, int priceID)
        {
            try
            {
                var query = model.CAT_Price.Where(c => c.ID == priceID).FirstOrDefault();
                if (query != null)
                {
                    foreach (var diLoad in model.CAT_PriceDILoad.Where(c => c.PriceID == query.ID && c.IsLoading && c.GroupOfLocationID > 0))
                    {
                        foreach (var detail in model.CAT_PriceDILoadDetail.Where(c => c.PriceDILoadID == diLoad.ID))
                            model.CAT_PriceDILoadDetail.Remove(detail);
                        model.CAT_PriceDILoad.Remove(diLoad);
                    }
                    model.SaveChanges();
                }
                else
                    throw FaultHelper.BusinessFault(null, null, "Không tìm thấy bảng giá");
            }
            catch (Exception ex)
            {
                throw FaultHelper.BusinessFault(ex);
            }
        }

        //Excel
        public static DTOPriceTruckDILoad_Export Price_DI_LoadPartner_Export(DataEntities model, AccountItem Account, int contractTermID, int priceID, bool isVen)
        {
            try
            {
                DTOPriceTruckDILoad_Export result = new DTOPriceTruckDILoad_Export();
                result.ListData = new List<DTOPriceTruckDILoad>();
                result.ListGroupProduct = new List<DTOCUSGroupOfProduct>();

                var objContractTerm = model.CAT_ContractTerm.FirstOrDefault(c => c.ID == contractTermID);
                if (objContractTerm == null) throw FaultHelper.BusinessFault(null, null, "Không tìm thấy phụ lục ID:" + contractTermID);

                var objContract = model.CAT_Contract.FirstOrDefault(c => c.ID == objContractTerm.ContractID);
                if (objContract == null) throw FaultHelper.BusinessFault(null, null, "Không tìm thấy hợp đồng ");

                int cusID = -1;
                if (objContract != null && objContract.CustomerID.HasValue)
                    cusID = objContract.CustomerID.Value;

                var listGOPMapping = new List<int>();
                if (isVen)
                {
                    if (objContract.CompanyID > 0)
                    {
                        var CustomerRelateID = objContract.CUS_Company.CustomerRelateID;
                        listGOPMapping = model.CUS_GroupOfProductMapping.Where(c => c.VendorID == objContract.CustomerID && c.CustomerID == CustomerRelateID).Select(c => c.GroupOfProductVENID).ToList();
                    }
                }

                result.ListGroupProduct = model.CUS_GroupOfProduct.Where(c => c.CustomerID == cusID && (!isVen ? true : (objContract.CompanyID == null ? true : listGOPMapping.Contains(c.ID)))).Select(c => new DTOCUSGroupOfProduct
                {
                    ID = c.ID,
                    Code = c.Code,
                    GroupName = c.GroupName,
                    PriceOfGOPID = c.PriceOfGOPID,
                    PriceOfGOPName = c.SYS_Var.ValueOfVar
                }).ToList();

                var query = model.CAT_PriceDILoad.Where(c => c.PriceID == priceID && c.IsLoading && c.GroupOfLocationID.HasValue).ToList();
                foreach (var item in query)
                {
                    DTOPriceTruckDILoad obj = new DTOPriceTruckDILoad();
                    obj.ID = item.ID;
                    obj.IsLoading = item.IsLoading;
                    obj.PriceID = item.PriceID;
                    obj.GroupOfLocationID = item.GroupOfLocationID;
                    obj.GroupOfLocationCode = item.GroupOfLocationID.HasValue ? item.CAT_GroupOfLocation.Code : string.Empty;
                    obj.GroupOfLocationName = item.GroupOfLocationID.HasValue ? item.CAT_GroupOfLocation.GroupName : string.Empty;
                    obj.ListPriceTruckLoadingDetail = new List<DTOPriceTruckDILoadDetail>();
                    foreach (var loadGroup in item.CAT_PriceDILoadDetail)
                    {
                        DTOPriceTruckDILoadDetail detail = new DTOPriceTruckDILoadDetail();
                        detail.ID = loadGroup.ID;
                        detail.PriceDILoadID = loadGroup.PriceDILoadID;
                        detail.CustomerID = loadGroup.CUS_GroupOfProduct.CustomerID;
                        detail.CustomerName = loadGroup.CUS_GroupOfProduct.CUS_Customer.CustomerName;
                        detail.GroupOfProductID = loadGroup.GroupOfProductID;
                        detail.GroupOfProductCode = loadGroup.CUS_GroupOfProduct.Code;
                        detail.GroupOfProductName = loadGroup.CUS_GroupOfProduct.GroupName;
                        detail.PriceOfGOPCode = loadGroup.SYS_Var.Code;
                        detail.PriceOfGOPName = loadGroup.SYS_Var.ValueOfVar;
                        detail.PriceOfGOPID = loadGroup.PriceOfGOPID;
                        detail.Price = loadGroup.Price;
                        obj.ListPriceTruckLoadingDetail.Add(detail);
                    }

                    result.ListData.Add(obj);
                }
                return result;
            }
            catch (Exception ex)
            {
                throw FaultHelper.BusinessFault(ex);
            }
        }
        #endregion

        #region Partner
        public static List<DTOPriceDILoadPartner> Price_DI_LoadParner_Partner_List(DataEntities model, AccountItem Account, int priceID)
        {
            try
            {
                List<DTOPriceDILoadPartner> result = new List<DTOPriceDILoadPartner>();
                var query = model.CAT_PriceDILoad.Where(c => c.PriceID == priceID && c.IsLoading && c.PartnerID.HasValue).ToList();
                foreach (var item in query)
                {
                    DTOPriceDILoadPartner obj = new DTOPriceDILoadPartner();
                    obj.ID = item.ID;
                    obj.IsLoading = item.IsLoading;
                    obj.PriceID = item.PriceID;
                    obj.PartnerID = item.PartnerID;
                    obj.PartnerCode = item.PartnerID.HasValue ? item.CAT_Partner.Code : string.Empty;
                    obj.PartnerName = item.PartnerID.HasValue ? item.CAT_Partner.PartnerName : string.Empty;
                    obj.ListPriceDILoadPartnerDetail = new List<DTOPriceDILoadPartnerDetail>();
                    foreach (var loadGroup in item.CAT_PriceDILoadDetail)
                    {
                        DTOPriceDILoadPartnerDetail detail = new DTOPriceDILoadPartnerDetail();
                        detail.ID = loadGroup.ID;
                        detail.PriceDILoadID = loadGroup.PriceDILoadID;
                        detail.CustomerID = loadGroup.CUS_GroupOfProduct.CustomerID;
                        detail.CustomerName = loadGroup.CUS_GroupOfProduct.CUS_Customer.CustomerName;
                        detail.GroupOfProductID = loadGroup.GroupOfProductID;
                        detail.GroupOfProductCode = loadGroup.CUS_GroupOfProduct.Code;
                        detail.GroupOfProductName = loadGroup.CUS_GroupOfProduct.GroupName;
                        detail.PriceOfGOPCode = loadGroup.SYS_Var.Code;
                        detail.PriceOfGOPName = loadGroup.SYS_Var.ValueOfVar;
                        detail.PriceOfGOPID = loadGroup.PriceOfGOPID;
                        detail.Price = loadGroup.Price;
                        obj.ListPriceDILoadPartnerDetail.Add(detail);
                    }

                    result.Add(obj);
                }
                return result;
            }
            catch (Exception ex)
            {
                throw FaultHelper.BusinessFault(ex);
            }
        }
        public static void Price_DI_LoadPartner_Partner_SaveList(DataEntities model, AccountItem Account, List<DTOPriceDILoadPartner> data)
        {
            try
            {
                foreach (var item in data)
                {
                    if (item.ListPriceDILoadPartnerDetail != null && item.ListPriceDILoadPartnerDetail.Count > 0)
                    {
                        foreach (var detail in item.ListPriceDILoadPartnerDetail)
                        {
                            var obj = model.CAT_PriceDILoadDetail.FirstOrDefault(c => c.PriceDILoadID == item.ID && c.GroupOfProductID == detail.GroupOfProductID);
                            if (obj == null)
                            {
                                obj = new CAT_PriceDILoadDetail();
                                obj.CreatedBy = Account.UserName;
                                obj.CreatedDate = DateTime.Now;
                                obj.PriceDILoadID = item.ID;
                                obj.GroupOfProductID = detail.GroupOfProductID;
                                model.CAT_PriceDILoadDetail.Add(obj);
                            }
                            else
                            {
                                obj.ModifiedBy = Account.UserName;
                                obj.ModifiedDate = DateTime.Now;
                            }
                            obj.PriceOfGOPID = detail.PriceOfGOPID;
                            obj.Price = detail.Price;
                        }
                    }
                }
                model.SaveChanges();
            }
            catch (Exception ex)
            {
                throw FaultHelper.BusinessFault(ex);
            }
        }
        public static DTOResult Price_DI_LoadPartner_Partner_PartnerNotIn_List(DataEntities model, AccountItem Account, string request, int priceID)
        {
            try
            {
                DTOResult result = new DTOResult();
                var objPrice = model.CAT_Price.FirstOrDefault(c => c.ID == priceID);
                if (objPrice != null)
                {
                    int ContractID = objPrice.CAT_ContractTerm.ContractID;
                    var lstPartnerID = objPrice.CAT_PriceDILoad.Where(c => c.IsLoading && c.PartnerID.HasValue).Select(c => c.PartnerID).Distinct().ToList();
                    var query = model.CAT_Partner.Where(c => !lstPartnerID.Contains(c.ID)).Select(c => new DTOCATPartner
                    {
                        ID = c.ID,
                        Code = c.Code,
                        PartnerName = c.PartnerName
                    }).ToDataSourceResult(CreateRequest(request));
                    result.Total = query.Total;
                    result.Data = query.Data as IEnumerable<DTOCATPartner>;
                }
                return result;
            }
            catch (Exception ex)
            {
                throw FaultHelper.BusinessFault(ex);
            }
        }
        public static void Price_DI_LoadPartner_Partner_PartnerNotIn_SaveList(DataEntities model, AccountItem Account, List<int> data, int priceID)
        {
            try
            {
                var customerID = model.CAT_Price.FirstOrDefault(c => c.ID == priceID).CAT_ContractTerm.CAT_Contract.CustomerID;
                var lstGroupOfProduct = model.CUS_GroupOfProduct.Where(c => c.CustomerID == customerID && c.PriceOfGOPID > 0);
                foreach (var item in data)
                {
                    CAT_PriceDILoad obj = new CAT_PriceDILoad();
                    obj.CreatedBy = Account.UserName;
                    obj.CreatedDate = DateTime.Now;
                    obj.PriceID = priceID;
                    obj.PartnerID = item;
                    obj.IsLoading = true;
                    model.CAT_PriceDILoad.Add(obj);

                    foreach (var group in lstGroupOfProduct)
                    {
                        CAT_PriceDILoadDetail objDetail = new CAT_PriceDILoadDetail();
                        objDetail.CreatedBy = Account.UserName;
                        objDetail.CreatedDate = DateTime.Now;
                        objDetail.GroupOfProductID = group.ID;
                        objDetail.PriceOfGOPID = group.PriceOfGOPID;
                        obj.CAT_PriceDILoadDetail.Add(objDetail);
                    }
                }
                model.SaveChanges();
            }
            catch (Exception ex)
            {
                throw FaultHelper.BusinessFault(ex);
            }
        }
        public static void Price_DI_LoadPartner_Partner_DeleteList(DataEntities model, AccountItem Account, int priceID)
        {
            try
            {
                var query = model.CAT_Price.Where(c => c.ID == priceID).FirstOrDefault();
                if (query != null)
                {
                    foreach (var diLoad in model.CAT_PriceDILoad.Where(c => c.PriceID == query.ID && c.IsLoading && c.PartnerID > 0))
                    {
                        foreach (var detail in model.CAT_PriceDILoadDetail.Where(c => c.PriceDILoadID == diLoad.ID))
                            model.CAT_PriceDILoadDetail.Remove(detail);
                        model.CAT_PriceDILoad.Remove(diLoad);
                    }
                    model.SaveChanges();
                }
                else
                    throw FaultHelper.BusinessFault(null, null, "Không tìm thấy bảng giá");
            }
            catch (Exception ex)
            {
                throw FaultHelper.BusinessFault(ex);
            }
        }

        #endregion
        #endregion

        #region Unload

        #region Location
        public static List<DTOPriceTruckDILoad> Price_DI_UnLoadLocation_List(DataEntities model, AccountItem Account, int priceID)
        {
            try
            {
                List<DTOPriceTruckDILoad> result = new List<DTOPriceTruckDILoad>();
                var query = model.CAT_PriceDILoad.Where(c => c.PriceID == priceID && !c.IsLoading && c.LocationID.HasValue).ToList();
                foreach (var item in query)
                {
                    DTOPriceTruckDILoad obj = new DTOPriceTruckDILoad();
                    obj.ID = item.ID;
                    obj.IsLoading = item.IsLoading;
                    obj.PriceID = item.PriceID;
                    obj.LocationID = item.LocationID;
                    obj.LocationCode = item.LocationID.HasValue ? item.CAT_Location.Code : string.Empty;
                    obj.LocationName = item.LocationID.HasValue ? item.CAT_Location.Location : string.Empty;
                    obj.Address = item.LocationID.HasValue ? item.CAT_Location.Address : string.Empty;
                    obj.ListPriceTruckLoadingDetail = new List<DTOPriceTruckDILoadDetail>();
                    foreach (var loadGroup in item.CAT_PriceDILoadDetail)
                    {
                        DTOPriceTruckDILoadDetail detail = new DTOPriceTruckDILoadDetail();
                        detail.ID = loadGroup.ID;
                        detail.PriceDILoadID = loadGroup.PriceDILoadID;
                        detail.CustomerID = loadGroup.CUS_GroupOfProduct.CustomerID;
                        detail.CustomerName = loadGroup.CUS_GroupOfProduct.CUS_Customer.CustomerName;
                        detail.GroupOfProductID = loadGroup.GroupOfProductID;
                        detail.GroupOfProductCode = loadGroup.CUS_GroupOfProduct.Code;
                        detail.GroupOfProductName = loadGroup.CUS_GroupOfProduct.GroupName;
                        detail.PriceOfGOPCode = loadGroup.SYS_Var.Code;
                        detail.PriceOfGOPName = loadGroup.SYS_Var.ValueOfVar;
                        detail.PriceOfGOPID = loadGroup.PriceOfGOPID;
                        detail.Price = loadGroup.Price;
                        obj.ListPriceTruckLoadingDetail.Add(detail);
                    }

                    result.Add(obj);
                }
                return result;
            }
            catch (Exception ex)
            {
                throw FaultHelper.BusinessFault(ex);
            }
        }
        public static void Price_DI_UnLoadLocation_SaveList(DataEntities model, AccountItem Account, List<DTOPriceTruckDILoad> data)
        {
            try
            {
                model.EventAccount = Account; model.EventRunning = false;
                foreach (var item in data)
                {
                    if (item.ListPriceTruckLoadingDetail != null && item.ListPriceTruckLoadingDetail.Count > 0)
                    {
                        foreach (var detail in item.ListPriceTruckLoadingDetail)
                        {
                            var obj = model.CAT_PriceDILoadDetail.FirstOrDefault(c => c.PriceDILoadID == item.ID && c.GroupOfProductID == detail.GroupOfProductID);
                            if (obj == null)
                            {
                                obj = new CAT_PriceDILoadDetail();
                                obj.CreatedBy = Account.UserName;
                                obj.CreatedDate = DateTime.Now;
                                obj.PriceDILoadID = item.ID;
                                obj.GroupOfProductID = detail.GroupOfProductID;
                                model.CAT_PriceDILoadDetail.Add(obj);
                            }
                            else
                            {
                                obj.ModifiedBy = Account.UserName;
                                obj.ModifiedDate = DateTime.Now;
                            }
                            obj.PriceOfGOPID = detail.PriceOfGOPID;
                            obj.Price = detail.Price;
                        }
                    }
                }
                model.SaveChanges();
            }
            catch (Exception ex)
            {
                throw FaultHelper.BusinessFault(ex);
            }
        }
        public static DTOResult Price_DI_UnLoadLocation_LocationNotIn_List(DataEntities model, AccountItem Account, string request, int priceID, bool isVen)
        {
            try
            {
                DTOResult result = new DTOResult();
                var objPrice = model.CAT_Price.FirstOrDefault(c => c.ID == priceID);
                if (objPrice != null)
                {
                    int CustomerID = objPrice.CAT_ContractTerm.CAT_Contract.CustomerID.Value;
                    if (isVen)
                    {
                        int CompanyID = objPrice.CAT_ContractTerm.CAT_Contract.CompanyID > 0 ? objPrice.CAT_ContractTerm.CAT_Contract.CUS_Company.CustomerRelateID : -1;
                        var lstID = new List<int>();

                        if (CompanyID > 0)
                        {
                            lstID.Add(CompanyID);
                        }
                        else
                        {
                            lstID = model.CUS_Company.Where(c => c.CustomerOwnID == CustomerID).Select(c => c.CustomerRelateID).Distinct().ToList();
                        }

                        var lstLocationID = objPrice.CAT_PriceDILoad.Where(c => !c.IsLoading && c.LocationID.HasValue).Select(c => c.LocationID).Distinct().ToList();
                        var query = model.CUS_Location.Where(c => lstID.Contains(c.CustomerID) && !lstLocationID.Contains(c.LocationID)).Select(c => new DTOCATLocation
                        {
                            ID = c.LocationID,
                            Code = c.Code,
                            Location = c.LocationName,
                            Address = c.CAT_Location.Address,
                        }).ToDataSourceResult(CreateRequest(request));

                        result.Total = query.Total;
                        result.Data = query.Data as IEnumerable<DTOCATLocation>;
                    }
                    else
                    {
                        var lstLocationID = objPrice.CAT_PriceDILoad.Where(c => !c.IsLoading && c.LocationID.HasValue).Select(c => c.LocationID).Distinct().ToList();
                        var query = model.CUS_Location.Where(c => c.CustomerID == CustomerID && !lstLocationID.Contains(c.LocationID)).Select(c => new DTOCATLocation
                        {
                            ID = c.LocationID,
                            Code = c.Code,
                            Location = c.LocationName,
                            Address = c.CAT_Location.Address,
                        }).ToDataSourceResult(CreateRequest(request));

                        result.Total = query.Total;
                        result.Data = query.Data as IEnumerable<DTOCATLocation>;
                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                throw FaultHelper.BusinessFault(ex);
            }
        }
        public static void Price_DI_UnLoadLocation_LocationNotIn_SaveList(DataEntities model, AccountItem Account, List<int> data, int priceID)
        {
            try
            {
                var customerID = model.CAT_Price.FirstOrDefault(c => c.ID == priceID).CAT_ContractTerm.CAT_Contract.CustomerID;
                var lstGroupOfProduct = model.CUS_GroupOfProduct.Where(c => c.CustomerID == customerID && c.PriceOfGOPID>0);
                foreach (var item in data)
                {
                    CAT_PriceDILoad obj = new CAT_PriceDILoad();
                    obj.CreatedBy = Account.UserName;
                    obj.CreatedDate = DateTime.Now;
                    obj.PriceID = priceID;
                    obj.LocationID = item;
                    obj.IsLoading = false;
                    model.CAT_PriceDILoad.Add(obj);

                    foreach (var group in lstGroupOfProduct)
                    {
                        CAT_PriceDILoadDetail objDetail = new CAT_PriceDILoadDetail();
                        objDetail.CreatedBy = Account.UserName;
                        objDetail.CreatedDate = DateTime.Now;
                        objDetail.GroupOfProductID = group.ID;
                        objDetail.PriceOfGOPID = group.PriceOfGOPID;
                        obj.CAT_PriceDILoadDetail.Add(objDetail);
                    }
                }
                model.SaveChanges();
            }
            catch (Exception ex)
            {
                throw FaultHelper.BusinessFault(ex);
            }
        }
        public static void Price_DI_UnLoadLocation_DeleteList(DataEntities model, AccountItem Account, int priceID)
        {
            try
            {
                var query = model.CAT_Price.Where(c => c.ID == priceID).FirstOrDefault();
                if (query != null)
                {
                    foreach (var diLoad in model.CAT_PriceDILoad.Where(c => c.PriceID == query.ID && !c.IsLoading && c.LocationID > 0))
                    {
                        foreach (var detail in model.CAT_PriceDILoadDetail.Where(c => c.PriceDILoadID == diLoad.ID))
                            model.CAT_PriceDILoadDetail.Remove(detail);
                        model.CAT_PriceDILoad.Remove(diLoad);
                    }
                    model.SaveChanges();
                }
                else
                    throw FaultHelper.BusinessFault(null, null, "Không tìm thấy bảng giá");
            }
            catch (Exception ex)
            {
                throw FaultHelper.BusinessFault(ex);
            }
        }

        //Excel
        public static DTOPriceTruckDILoad_Export Price_DI_UnLoadLocation_Export(DataEntities model, AccountItem Account, int contractTermID, int priceID, bool isVen)
        {
            try
            {
                DTOPriceTruckDILoad_Export result = new DTOPriceTruckDILoad_Export();
                result.ListData = new List<DTOPriceTruckDILoad>();
                result.ListGroupProduct = new List<DTOCUSGroupOfProduct>();

                var objContractTerm = model.CAT_ContractTerm.FirstOrDefault(c => c.ID == contractTermID);
                if (objContractTerm == null) throw FaultHelper.BusinessFault(null, null, "Không tìm thấy phụ lục ID:" + contractTermID);

                var objContract = model.CAT_Contract.FirstOrDefault(c => c.ID == objContractTerm.ContractID);
                if (objContract == null) throw FaultHelper.BusinessFault(null, null, "Không tìm thấy hợp đồng ");

                int cusID = -1;
                if (objContract != null && objContract.CustomerID.HasValue)
                    cusID = objContract.CustomerID.Value;

                var listGOPMapping = new List<int>();
                if (isVen)
                {
                    if (objContract.CompanyID > 0)
                    {
                        var CustomerRelateID = objContract.CUS_Company.CustomerRelateID;
                        listGOPMapping = model.CUS_GroupOfProductMapping.Where(c => c.VendorID == objContract.CustomerID && c.CustomerID == CustomerRelateID).Select(c => c.GroupOfProductVENID).ToList();
                    }
                }

                result.ListGroupProduct = model.CUS_GroupOfProduct.Where(c => c.CustomerID == cusID && (!isVen ? true : (objContract.CompanyID == null ? true : listGOPMapping.Contains(c.ID)))).Select(c => new DTOCUSGroupOfProduct
                {
                    ID = c.ID,
                    Code = c.Code,
                    GroupName = c.GroupName,
                    PriceOfGOPID = c.PriceOfGOPID,
                    PriceOfGOPName = c.SYS_Var.ValueOfVar
                }).ToList();

                var query = model.CAT_PriceDILoad.Where(c => c.PriceID == priceID && !c.IsLoading && c.LocationID.HasValue).ToList();
                foreach (var item in query)
                {
                    DTOPriceTruckDILoad obj = new DTOPriceTruckDILoad();
                    obj.ID = item.ID;
                    obj.IsLoading = item.IsLoading;
                    obj.PriceID = item.PriceID;
                    obj.LocationID = item.LocationID;
                    obj.LocationCode = item.LocationID.HasValue ? item.CAT_Location.Code : string.Empty;
                    obj.LocationName = item.LocationID.HasValue ? item.CAT_Location.Location : string.Empty;
                    obj.Address = item.LocationID.HasValue ? item.CAT_Location.Address : string.Empty;
                    obj.ListPriceTruckLoadingDetail = new List<DTOPriceTruckDILoadDetail>();
                    foreach (var loadGroup in item.CAT_PriceDILoadDetail)
                    {
                        DTOPriceTruckDILoadDetail detail = new DTOPriceTruckDILoadDetail();
                        detail.ID = loadGroup.ID;
                        detail.PriceDILoadID = loadGroup.PriceDILoadID;
                        detail.CustomerID = loadGroup.CUS_GroupOfProduct.CustomerID;
                        detail.CustomerName = loadGroup.CUS_GroupOfProduct.CUS_Customer.CustomerName;
                        detail.GroupOfProductID = loadGroup.GroupOfProductID;
                        detail.GroupOfProductCode = loadGroup.CUS_GroupOfProduct.Code;
                        detail.GroupOfProductName = loadGroup.CUS_GroupOfProduct.GroupName;
                        detail.PriceOfGOPCode = loadGroup.SYS_Var.Code;
                        detail.PriceOfGOPName = loadGroup.SYS_Var.ValueOfVar;
                        detail.PriceOfGOPID = loadGroup.PriceOfGOPID;
                        detail.Price = loadGroup.Price;
                        obj.ListPriceTruckLoadingDetail.Add(detail);
                    }

                    result.ListData.Add(obj);
                }
                return result;
            }
            catch (Exception ex)
            {
                throw FaultHelper.BusinessFault(ex);
            }
        }
        #endregion

        #region Route
        public static List<DTOPriceTruckDILoad> Price_DI_UnLoadRoute_List(DataEntities model, AccountItem Account, int priceID)
        {
            try
            {
                List<DTOPriceTruckDILoad> result = new List<DTOPriceTruckDILoad>();
                var query = model.CAT_PriceDILoad.Where(c => c.PriceID == priceID && !c.IsLoading && c.RoutingID.HasValue).ToList();
                foreach (var item in query)
                {
                    DTOPriceTruckDILoad obj = new DTOPriceTruckDILoad();
                    obj.ID = item.ID;
                    obj.IsLoading = item.IsLoading;
                    obj.PriceID = item.PriceID;
                    obj.RoutingID = item.RoutingID;
                    obj.RoutingCode = item.RoutingID.HasValue ? item.CAT_Routing1.Code : string.Empty;
                    obj.RoutingName = item.RoutingID.HasValue ? item.CAT_Routing1.RoutingName : string.Empty;
                    obj.ListPriceTruckLoadingDetail = new List<DTOPriceTruckDILoadDetail>();
                    foreach (var loadGroup in item.CAT_PriceDILoadDetail)
                    {
                        DTOPriceTruckDILoadDetail detail = new DTOPriceTruckDILoadDetail();
                        detail.ID = loadGroup.ID;
                        detail.PriceDILoadID = loadGroup.PriceDILoadID;
                        detail.CustomerID = loadGroup.CUS_GroupOfProduct.CustomerID;
                        detail.CustomerName = loadGroup.CUS_GroupOfProduct.CUS_Customer.CustomerName;
                        detail.GroupOfProductID = loadGroup.GroupOfProductID;
                        detail.GroupOfProductCode = loadGroup.CUS_GroupOfProduct.Code;
                        detail.GroupOfProductName = loadGroup.CUS_GroupOfProduct.GroupName;
                        detail.PriceOfGOPCode = loadGroup.SYS_Var.Code;
                        detail.PriceOfGOPName = loadGroup.SYS_Var.ValueOfVar;
                        detail.PriceOfGOPID = loadGroup.PriceOfGOPID;
                        detail.Price = loadGroup.Price;
                        obj.ListPriceTruckLoadingDetail.Add(detail);
                    }

                    result.Add(obj);
                }
                return result;
            }
            catch (Exception ex)
            {
                throw FaultHelper.BusinessFault(ex);
            }
        }
        public static void Price_DI_UnLoadRoute_SaveList(DataEntities model, AccountItem Account, List<DTOPriceTruckDILoad> data)
        {
            try
            {
                foreach (var item in data)
                {
                    if (item.ListPriceTruckLoadingDetail != null && item.ListPriceTruckLoadingDetail.Count > 0)
                    {
                        foreach (var detail in item.ListPriceTruckLoadingDetail)
                        {
                            var obj = model.CAT_PriceDILoadDetail.FirstOrDefault(c => c.PriceDILoadID == item.ID && c.GroupOfProductID == detail.GroupOfProductID);
                            if (obj == null)
                            {
                                obj = new CAT_PriceDILoadDetail();
                                obj.CreatedBy = Account.UserName;
                                obj.CreatedDate = DateTime.Now;
                                obj.PriceDILoadID = item.ID;
                                obj.GroupOfProductID = detail.GroupOfProductID;
                                model.CAT_PriceDILoadDetail.Add(obj);
                            }
                            else
                            {
                                obj.ModifiedBy = Account.UserName;
                                obj.ModifiedDate = DateTime.Now;
                            }
                            obj.PriceOfGOPID = detail.PriceOfGOPID;
                            obj.Price = detail.Price;
                        }
                    }
                }
                model.SaveChanges();
            }
            catch (Exception ex)
            {
                throw FaultHelper.BusinessFault(ex);
            }
        }
        public static DTOResult Price_DI_UnLoadRoute_RouteNotIn_List(DataEntities model, AccountItem Account, string request, int priceID)
        {
            try
            {
                DTOResult result = new DTOResult();
                var objPrice = model.CAT_Price.FirstOrDefault(c => c.ID == priceID);
                if (objPrice != null)
                {
                    int ContractID = objPrice.CAT_ContractTerm.ContractID;
                    var lstRouteID = objPrice.CAT_PriceDILoad.Where(c => !c.IsLoading && c.RoutingID.HasValue).Select(c => c.RoutingID).Distinct().ToList();
                    if (objPrice.CAT_ContractTerm.IsAllRouting)
                    {
                        var query = model.CAT_ContractRouting.Where(c => c.ContractID == ContractID && !lstRouteID.Contains(c.RoutingID) && c.ContractRoutingTypeID == -(int)SYSVarType.ContractRoutingTypePrice).Select(c => new DTOPriceRoute
                        {
                            RouteID = c.RoutingID,
                            RouteCode = c.Code,
                            RouteName = c.RoutingName,
                            CATRouteCode = c.CAT_Routing.Code,
                            CATRouteName = c.CAT_Routing.RoutingName,
                        }).ToDataSourceResult(CreateRequest(request));
                        result.Total = query.Total;
                        result.Data = query.Data as IEnumerable<DTOPriceRoute>;
                    }
                    else
                    {
                        var query = model.CAT_ContractRouting.Where(c => c.ContractTermID == objPrice.ContractTermID && !lstRouteID.Contains(c.RoutingID) && c.ContractRoutingTypeID == -(int)SYSVarType.ContractRoutingTypePrice).Select(c => new DTOPriceRoute
                        {
                            RouteID = c.RoutingID,
                            RouteCode = c.Code,
                            RouteName = c.RoutingName,
                            CATRouteCode = c.CAT_Routing.Code,
                            CATRouteName = c.CAT_Routing.RoutingName,
                        }).ToDataSourceResult(CreateRequest(request));
                        result.Total = query.Total;
                        result.Data = query.Data as IEnumerable<DTOPriceRoute>;
                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                throw FaultHelper.BusinessFault(ex);
            }
        }
        public static void Price_DI_UnLoadRoute_RouteNotIn_SaveList(DataEntities model, AccountItem Account, List<int> data, int priceID)
        {
            try
            {
                var customerID = model.CAT_Price.FirstOrDefault(c => c.ID == priceID).CAT_ContractTerm.CAT_Contract.CustomerID;
                var lstGroupOfProduct = model.CUS_GroupOfProduct.Where(c => c.CustomerID == customerID && c.PriceOfGOPID>0);
                foreach (var item in data)
                {
                    CAT_PriceDILoad obj = new CAT_PriceDILoad();
                    obj.CreatedBy = Account.UserName;
                    obj.CreatedDate = DateTime.Now;
                    obj.PriceID = priceID;
                    obj.RoutingID = item;
                    obj.IsLoading = false;
                    model.CAT_PriceDILoad.Add(obj);

                    foreach (var group in lstGroupOfProduct)
                    {
                        CAT_PriceDILoadDetail objDetail = new CAT_PriceDILoadDetail();
                        objDetail.CreatedBy = Account.UserName;
                        objDetail.CreatedDate = DateTime.Now;
                        objDetail.GroupOfProductID = group.ID;
                        objDetail.PriceOfGOPID = group.PriceOfGOPID;
                        obj.CAT_PriceDILoadDetail.Add(objDetail);
                    }
                }
                model.SaveChanges();
            }
            catch (Exception ex)
            {
                throw FaultHelper.BusinessFault(ex);
            }
        }
        public static void Price_DI_UnLoadRoute_DeleteList(DataEntities model, AccountItem Account, int priceID)
        {
            try
            {
                var query = model.CAT_Price.Where(c => c.ID == priceID).FirstOrDefault();
                if (query != null)
                {
                    foreach (var diLoad in model.CAT_PriceDILoad.Where(c => c.PriceID == query.ID && !c.IsLoading && c.RoutingID > 0))
                    {
                        foreach (var detail in model.CAT_PriceDILoadDetail.Where(c => c.PriceDILoadID == diLoad.ID))
                            model.CAT_PriceDILoadDetail.Remove(detail);
                        model.CAT_PriceDILoad.Remove(diLoad);
                    }
                    model.SaveChanges();
                }
                else
                    throw FaultHelper.BusinessFault(null, null, "Không tìm thấy bảng giá");
            }
            catch (Exception ex)
            {
                throw FaultHelper.BusinessFault(ex);
            }
        }

        //Excel
        public static DTOPriceTruckDILoad_Export Price_DI_UnLoadRoute_Export(DataEntities model, AccountItem Account, int contractTermID, int priceID, bool isVen)
        {
            try
            {
                DTOPriceTruckDILoad_Export result = new DTOPriceTruckDILoad_Export();
                result.ListData = new List<DTOPriceTruckDILoad>();
                result.ListGroupProduct = new List<DTOCUSGroupOfProduct>();

                var objContractTerm = model.CAT_ContractTerm.FirstOrDefault(c => c.ID == contractTermID);
                if (objContractTerm == null) throw FaultHelper.BusinessFault(null, null, "Không tìm thấy phụ lục ID:" + contractTermID);

                var objContract = model.CAT_Contract.FirstOrDefault(c => c.ID == objContractTerm.ContractID);
                if (objContract == null) throw FaultHelper.BusinessFault(null, null, "Không tìm thấy hợp đồng ");

                int cusID = -1;
                if (objContract != null && objContract.CustomerID.HasValue)
                    cusID = objContract.CustomerID.Value;

                var listGOPMapping = new List<int>();
                if (isVen)
                {
                    if (objContract.CompanyID > 0)
                    {
                        var CustomerRelateID = objContract.CUS_Company.CustomerRelateID;
                        listGOPMapping = model.CUS_GroupOfProductMapping.Where(c => c.VendorID == objContract.CustomerID && c.CustomerID == CustomerRelateID).Select(c => c.GroupOfProductVENID).ToList();
                    }
                }

                result.ListGroupProduct = model.CUS_GroupOfProduct.Where(c => c.CustomerID == cusID && (!isVen ? true : (objContract.CompanyID == null ? true : listGOPMapping.Contains(c.ID)))).Select(c => new DTOCUSGroupOfProduct
                {
                    ID = c.ID,
                    Code = c.Code,
                    GroupName = c.GroupName,
                    PriceOfGOPID = c.PriceOfGOPID,
                    PriceOfGOPName = c.SYS_Var.ValueOfVar
                }).ToList();

                var query = model.CAT_PriceDILoad.Where(c => c.PriceID == priceID && !c.IsLoading && c.RoutingID.HasValue).ToList();
                foreach (var item in query)
                {
                    DTOPriceTruckDILoad obj = new DTOPriceTruckDILoad();
                    obj.ID = item.ID;
                    obj.IsLoading = item.IsLoading;
                    obj.PriceID = item.PriceID;
                    obj.RoutingID = item.RoutingID;
                    obj.RoutingCode = item.RoutingID.HasValue ? item.CAT_Routing1.Code : string.Empty;
                    obj.RoutingName = item.RoutingID.HasValue ? item.CAT_Routing1.RoutingName : string.Empty;
                    obj.ListPriceTruckLoadingDetail = new List<DTOPriceTruckDILoadDetail>();
                    foreach (var loadGroup in item.CAT_PriceDILoadDetail)
                    {
                        DTOPriceTruckDILoadDetail detail = new DTOPriceTruckDILoadDetail();
                        detail.ID = loadGroup.ID;
                        detail.PriceDILoadID = loadGroup.PriceDILoadID;
                        detail.CustomerID = loadGroup.CUS_GroupOfProduct.CustomerID;
                        detail.CustomerName = loadGroup.CUS_GroupOfProduct.CUS_Customer.CustomerName;
                        detail.GroupOfProductID = loadGroup.GroupOfProductID;
                        detail.GroupOfProductCode = loadGroup.CUS_GroupOfProduct.Code;
                        detail.GroupOfProductName = loadGroup.CUS_GroupOfProduct.GroupName;
                        detail.PriceOfGOPCode = loadGroup.SYS_Var.Code;
                        detail.PriceOfGOPName = loadGroup.SYS_Var.ValueOfVar;
                        detail.PriceOfGOPID = loadGroup.PriceOfGOPID;
                        detail.Price = loadGroup.Price;
                        obj.ListPriceTruckLoadingDetail.Add(detail);
                    }

                    result.ListData.Add(obj);
                }
                return result;
            }
            catch (Exception ex)
            {
                throw FaultHelper.BusinessFault(ex);
            }
        }
        #endregion

        #region TypeOfPartner
        public static List<DTOPriceTruckDILoad> Price_DI_UnLoadPartner_List(DataEntities model, AccountItem Account, int priceID)
        {
            try
            {
                List<DTOPriceTruckDILoad> result = new List<DTOPriceTruckDILoad>();
                var query = model.CAT_PriceDILoad.Where(c => c.PriceID == priceID && !c.IsLoading && c.GroupOfLocationID.HasValue).ToList();
                foreach (var item in query)
                {
                    DTOPriceTruckDILoad obj = new DTOPriceTruckDILoad();
                    obj.ID = item.ID;
                    obj.IsLoading = item.IsLoading;
                    obj.PriceID = item.PriceID;
                    obj.GroupOfLocationID = item.GroupOfLocationID;
                    obj.GroupOfLocationCode = item.GroupOfLocationID.HasValue ? item.CAT_GroupOfLocation.Code : string.Empty;
                    obj.GroupOfLocationName = item.GroupOfLocationID.HasValue ? item.CAT_GroupOfLocation.GroupName : string.Empty;
                    obj.ListPriceTruckLoadingDetail = new List<DTOPriceTruckDILoadDetail>();
                    foreach (var loadGroup in item.CAT_PriceDILoadDetail)
                    {
                        DTOPriceTruckDILoadDetail detail = new DTOPriceTruckDILoadDetail();
                        detail.ID = loadGroup.ID;
                        detail.PriceDILoadID = loadGroup.PriceDILoadID;
                        detail.CustomerID = loadGroup.CUS_GroupOfProduct.CustomerID;
                        detail.CustomerName = loadGroup.CUS_GroupOfProduct.CUS_Customer.CustomerName;
                        detail.GroupOfProductID = loadGroup.GroupOfProductID;
                        detail.GroupOfProductCode = loadGroup.CUS_GroupOfProduct.Code;
                        detail.GroupOfProductName = loadGroup.CUS_GroupOfProduct.GroupName;
                        detail.PriceOfGOPCode = loadGroup.SYS_Var.Code;
                        detail.PriceOfGOPName = loadGroup.SYS_Var.ValueOfVar;
                        detail.PriceOfGOPID = loadGroup.PriceOfGOPID;
                        detail.Price = loadGroup.Price;
                        obj.ListPriceTruckLoadingDetail.Add(detail);
                    }

                    result.Add(obj);
                }
                return result;
            }
            catch (Exception ex)
            {
                throw FaultHelper.BusinessFault(ex);
            }
        }
        public static void Price_DI_UnLoadPartner_SaveList(DataEntities model, AccountItem Account, List<DTOPriceTruckDILoad> data)
        {
            try
            {
                foreach (var item in data)
                {
                    if (item.ListPriceTruckLoadingDetail != null && item.ListPriceTruckLoadingDetail.Count > 0)
                    {
                        foreach (var detail in item.ListPriceTruckLoadingDetail)
                        {
                            var obj = model.CAT_PriceDILoadDetail.FirstOrDefault(c => c.PriceDILoadID == item.ID && c.GroupOfProductID == detail.GroupOfProductID);
                            if (obj == null)
                            {
                                obj = new CAT_PriceDILoadDetail();
                                obj.CreatedBy = Account.UserName;
                                obj.CreatedDate = DateTime.Now;
                                obj.PriceDILoadID = item.ID;
                                obj.GroupOfProductID = detail.GroupOfProductID;
                                model.CAT_PriceDILoadDetail.Add(obj);
                            }
                            else
                            {
                                obj.ModifiedBy = Account.UserName;
                                obj.ModifiedDate = DateTime.Now;
                            }
                            obj.PriceOfGOPID = detail.PriceOfGOPID;
                            obj.Price = detail.Price;
                        }
                    }
                }
                model.SaveChanges();
            }
            catch (Exception ex)
            {
                throw FaultHelper.BusinessFault(ex);
            }
        }
        public static DTOResult Price_DI_UnLoadPartner_PartnerNotIn_List(DataEntities model, AccountItem Account, string request, int priceID)
        {
            try
            {
                DTOResult result = new DTOResult();
                var objPrice = model.CAT_Price.FirstOrDefault(c => c.ID == priceID);
                if (objPrice != null)
                {
                    int ContractID = objPrice.CAT_ContractTerm.ContractID;
                    var lstRouteID = objPrice.CAT_PriceDILoad.Where(c => c.GroupOfLocationID.HasValue && !c.IsLoading).Select(c => c.GroupOfLocationID).Distinct().ToList();
                    var query = model.CAT_GroupOfLocation.Where(c => !lstRouteID.Contains(c.ID)).Select(c => new DTOCATGroupOfLocation
                    {
                        ID = c.ID,
                        Code = c.Code,
                        GroupName = c.GroupName
                    }).ToDataSourceResult(CreateRequest(request));
                    result.Total = query.Total;
                    result.Data = query.Data as IEnumerable<DTOCATGroupOfLocation>;
                }
                return result;
            }
            catch (Exception ex)
            {
                throw FaultHelper.BusinessFault(ex);
            }
        }
        public static void Price_DI_UnLoadPartner_PartnerNotIn_SaveList(DataEntities model, AccountItem Account, List<int> data, int priceID)
        {
            try
            {
                var customerID = model.CAT_Price.FirstOrDefault(c => c.ID == priceID).CAT_ContractTerm.CAT_Contract.CustomerID;
                var lstGroupOfProduct = model.CUS_GroupOfProduct.Where(c => c.CustomerID == customerID && c.PriceOfGOPID>0);
                foreach (var item in data)
                {
                    CAT_PriceDILoad obj = new CAT_PriceDILoad();
                    obj.CreatedBy = Account.UserName;
                    obj.CreatedDate = DateTime.Now;
                    obj.PriceID = priceID;
                    obj.GroupOfLocationID = item;
                    obj.IsLoading = false;
                    model.CAT_PriceDILoad.Add(obj);

                    foreach (var group in lstGroupOfProduct)
                    {
                        CAT_PriceDILoadDetail objDetail = new CAT_PriceDILoadDetail();
                        objDetail.CreatedBy = Account.UserName;
                        objDetail.CreatedDate = DateTime.Now;
                        objDetail.GroupOfProductID = group.ID;
                        objDetail.PriceOfGOPID = group.PriceOfGOPID;
                        obj.CAT_PriceDILoadDetail.Add(objDetail);
                    }
                }
                model.SaveChanges();
            }
            catch (Exception ex)
            {
                throw FaultHelper.BusinessFault(ex);
            }
        }
        public static void Price_DI_UnLoadPartner_DeleteList(DataEntities model, AccountItem Account, int priceID)
        {
            try
            {
                var query = model.CAT_Price.Where(c => c.ID == priceID).FirstOrDefault();
                if (query != null)
                {
                    foreach (var diLoad in model.CAT_PriceDILoad.Where(c => c.PriceID == query.ID && !c.IsLoading && c.GroupOfLocationID > 0))
                    {
                        foreach (var detail in model.CAT_PriceDILoadDetail.Where(c => c.PriceDILoadID == diLoad.ID))
                            model.CAT_PriceDILoadDetail.Remove(detail);
                        model.CAT_PriceDILoad.Remove(diLoad);
                    }
                    model.SaveChanges();
                }
                else
                    throw FaultHelper.BusinessFault(null, null, "Không tìm thấy bảng giá");
            }
            catch (Exception ex)
            {
                throw FaultHelper.BusinessFault(ex);
            }
        }

        //Excel
        public static DTOPriceTruckDILoad_Export Price_DI_UnLoadPartner_Export(DataEntities model, AccountItem Account, int contractTermID, int priceID, bool isVen)
        {
            try
            {
                DTOPriceTruckDILoad_Export result = new DTOPriceTruckDILoad_Export();
                result.ListData = new List<DTOPriceTruckDILoad>();
                result.ListGroupProduct = new List<DTOCUSGroupOfProduct>();

                var objContractTerm = model.CAT_ContractTerm.FirstOrDefault(c => c.ID == contractTermID);
                if (objContractTerm == null) throw FaultHelper.BusinessFault(null, null, "Không tìm thấy phụ lục ID:" + contractTermID);

                var objContract = model.CAT_Contract.FirstOrDefault(c => c.ID == objContractTerm.ContractID);
                if (objContract == null) throw FaultHelper.BusinessFault(null, null, "Không tìm thấy hợp đồng ");

                int cusID = -1;
                if (objContract != null && objContract.CustomerID.HasValue)
                    cusID = objContract.CustomerID.Value;

                var listGOPMapping = new List<int>();
                if (isVen)
                {
                    if (objContract.CompanyID > 0)
                    {
                        var CustomerRelateID = objContract.CUS_Company.CustomerRelateID;
                        listGOPMapping = model.CUS_GroupOfProductMapping.Where(c => c.VendorID == objContract.CustomerID && c.CustomerID == CustomerRelateID).Select(c => c.GroupOfProductVENID).ToList();
                    }
                }

                result.ListGroupProduct = model.CUS_GroupOfProduct.Where(c => c.CustomerID == cusID && (!isVen ? true : (objContract.CompanyID == null ? true : listGOPMapping.Contains(c.ID)))).Select(c => new DTOCUSGroupOfProduct
                {
                    ID = c.ID,
                    Code = c.Code,
                    GroupName = c.GroupName,
                    PriceOfGOPID = c.PriceOfGOPID,
                    PriceOfGOPName = c.SYS_Var.ValueOfVar
                }).ToList();

                var query = model.CAT_PriceDILoad.Where(c => c.PriceID == priceID && !c.IsLoading && c.GroupOfLocationID.HasValue).ToList();
                foreach (var item in query)
                {
                    DTOPriceTruckDILoad obj = new DTOPriceTruckDILoad();
                    obj.ID = item.ID;
                    obj.IsLoading = item.IsLoading;
                    obj.PriceID = item.PriceID;
                    obj.GroupOfLocationID = item.GroupOfLocationID;
                    obj.GroupOfLocationCode = item.GroupOfLocationID.HasValue ? item.CAT_GroupOfLocation.Code : string.Empty;
                    obj.GroupOfLocationName = item.GroupOfLocationID.HasValue ? item.CAT_GroupOfLocation.GroupName : string.Empty;
                    obj.ListPriceTruckLoadingDetail = new List<DTOPriceTruckDILoadDetail>();
                    foreach (var loadGroup in item.CAT_PriceDILoadDetail)
                    {
                        DTOPriceTruckDILoadDetail detail = new DTOPriceTruckDILoadDetail();
                        detail.ID = loadGroup.ID;
                        detail.PriceDILoadID = loadGroup.PriceDILoadID;
                        detail.CustomerID = loadGroup.CUS_GroupOfProduct.CustomerID;
                        detail.CustomerName = loadGroup.CUS_GroupOfProduct.CUS_Customer.CustomerName;
                        detail.GroupOfProductID = loadGroup.GroupOfProductID;
                        detail.GroupOfProductCode = loadGroup.CUS_GroupOfProduct.Code;
                        detail.GroupOfProductName = loadGroup.CUS_GroupOfProduct.GroupName;
                        detail.PriceOfGOPCode = loadGroup.SYS_Var.Code;
                        detail.PriceOfGOPName = loadGroup.SYS_Var.ValueOfVar;
                        detail.PriceOfGOPID = loadGroup.PriceOfGOPID;
                        detail.Price = loadGroup.Price;
                        obj.ListPriceTruckLoadingDetail.Add(detail);
                    }

                    result.ListData.Add(obj);
                }
                return result;
            }
            catch (Exception ex)
            {
                throw FaultHelper.BusinessFault(ex);
            }
        }
        #endregion

        #region Partner
        public static List<DTOPriceDILoadPartner> Price_DI_UnLoadPartner_Partner_List(DataEntities model, AccountItem Account, int priceID)
        {
            try
            {
                List<DTOPriceDILoadPartner> result = new List<DTOPriceDILoadPartner>();
                var query = model.CAT_PriceDILoad.Where(c => c.PriceID == priceID && !c.IsLoading && c.PartnerID.HasValue).ToList();
                foreach (var item in query)
                {
                    DTOPriceDILoadPartner obj = new DTOPriceDILoadPartner();
                    obj.ID = item.ID;
                    obj.IsLoading = item.IsLoading;
                    obj.PriceID = item.PriceID;
                    obj.PartnerID = item.PartnerID;
                    obj.PartnerCode = item.PartnerID.HasValue ? item.CAT_Partner.Code : string.Empty;
                    obj.PartnerName = item.PartnerID.HasValue ? item.CAT_Partner.PartnerName : string.Empty;
                    obj.ListPriceDILoadPartnerDetail = new List<DTOPriceDILoadPartnerDetail>();
                    foreach (var loadGroup in item.CAT_PriceDILoadDetail)
                    {
                        DTOPriceDILoadPartnerDetail detail = new DTOPriceDILoadPartnerDetail();
                        detail.ID = loadGroup.ID;
                        detail.PriceDILoadID = loadGroup.PriceDILoadID;
                        detail.CustomerID = loadGroup.CUS_GroupOfProduct.CustomerID;
                        detail.CustomerName = loadGroup.CUS_GroupOfProduct.CUS_Customer.CustomerName;
                        detail.GroupOfProductID = loadGroup.GroupOfProductID;
                        detail.GroupOfProductCode = loadGroup.CUS_GroupOfProduct.Code;
                        detail.GroupOfProductName = loadGroup.CUS_GroupOfProduct.GroupName;
                        detail.PriceOfGOPCode = loadGroup.SYS_Var.Code;
                        detail.PriceOfGOPName = loadGroup.SYS_Var.ValueOfVar;
                        detail.PriceOfGOPID = loadGroup.PriceOfGOPID;
                        detail.Price = loadGroup.Price;
                        obj.ListPriceDILoadPartnerDetail.Add(detail);
                    }

                    result.Add(obj);
                }
                return result;
            }
            catch (Exception ex)
            {
                throw FaultHelper.BusinessFault(ex);
            }
        }
        public static void Price_DI_UnLoadPartner_Partner_SaveList(DataEntities model, AccountItem Account, List<DTOPriceDILoadPartner> data)
        {
            try
            {
                foreach (var item in data)
                {
                    if (item.ListPriceDILoadPartnerDetail != null && item.ListPriceDILoadPartnerDetail.Count > 0)
                    {
                        foreach (var detail in item.ListPriceDILoadPartnerDetail)
                        {
                            var obj = model.CAT_PriceDILoadDetail.FirstOrDefault(c => c.PriceDILoadID == item.ID && c.GroupOfProductID == detail.GroupOfProductID);
                            if (obj == null)
                            {
                                obj = new CAT_PriceDILoadDetail();
                                obj.CreatedBy = Account.UserName;
                                obj.CreatedDate = DateTime.Now;
                                obj.PriceDILoadID = item.ID;
                                obj.GroupOfProductID = detail.GroupOfProductID;
                                model.CAT_PriceDILoadDetail.Add(obj);
                            }
                            else
                            {
                                obj.ModifiedBy = Account.UserName;
                                obj.ModifiedDate = DateTime.Now;
                            }
                            obj.PriceOfGOPID = detail.PriceOfGOPID;
                            obj.Price = detail.Price;
                        }
                    }
                }
                model.SaveChanges();
            }
            catch (Exception ex)
            {
                throw FaultHelper.BusinessFault(ex);
            }
        }
        public static DTOResult Price_DI_UnLoadPartner_Partner_PartnerNotIn_List(DataEntities model, AccountItem Account, string request, int priceID)
        {
            try
            {
                DTOResult result = new DTOResult();
                var objPrice = model.CAT_Price.FirstOrDefault(c => c.ID == priceID);
                if (objPrice != null)
                {
                    int ContractID = objPrice.CAT_ContractTerm.ContractID;
                    var lstRouteID = objPrice.CAT_PriceDILoad.Where(c => c.PartnerID.HasValue && !c.IsLoading).Select(c => c.PartnerID).Distinct().ToList();
                    var query = model.CAT_Partner.Where(c => !lstRouteID.Contains(c.ID)).Select(c => new DTOCATPartner
                    {
                        ID = c.ID,
                        Code = c.Code,
                        PartnerName = c.PartnerName
                    }).ToDataSourceResult(CreateRequest(request));
                    result.Total = query.Total;
                    result.Data = query.Data as IEnumerable<DTOCATPartner>;
                }
                return result;
            }
            catch (Exception ex)
            {
                throw FaultHelper.BusinessFault(ex);
            }
        }
        public static void Price_DI_UnLoadPartner_Partner_PartnerNotIn_SaveList(DataEntities model, AccountItem Account, List<int> data, int priceID)
        {
            try
            {
                var customerID = model.CAT_Price.FirstOrDefault(c => c.ID == priceID).CAT_ContractTerm.CAT_Contract.CustomerID;
                var lstGroupOfProduct = model.CUS_GroupOfProduct.Where(c => c.CustomerID == customerID && c.PriceOfGOPID > 0);
                foreach (var item in data)
                {
                    CAT_PriceDILoad obj = new CAT_PriceDILoad();
                    obj.CreatedBy = Account.UserName;
                    obj.CreatedDate = DateTime.Now;
                    obj.PriceID = priceID;
                    obj.PartnerID = item;
                    obj.IsLoading = false;
                    model.CAT_PriceDILoad.Add(obj);

                    foreach (var group in lstGroupOfProduct)
                    {
                        CAT_PriceDILoadDetail objDetail = new CAT_PriceDILoadDetail();
                        objDetail.CreatedBy = Account.UserName;
                        objDetail.CreatedDate = DateTime.Now;
                        objDetail.GroupOfProductID = group.ID;
                        objDetail.PriceOfGOPID = group.PriceOfGOPID;
                        obj.CAT_PriceDILoadDetail.Add(objDetail);
                    }
                }
                model.SaveChanges();
            }
            catch (Exception ex)
            {
                throw FaultHelper.BusinessFault(ex);
            }
        }
        public static void Price_DI_UnLoadPartner_Partner_DeleteList(DataEntities model, AccountItem Account, int priceID)
        {
            try
            {
                var query = model.CAT_Price.Where(c => c.ID == priceID).FirstOrDefault();
                if (query != null)
                {
                    foreach (var diLoad in model.CAT_PriceDILoad.Where(c => c.PriceID == query.ID && !c.IsLoading && c.GroupOfLocationID > 0))
                    {
                        foreach (var detail in model.CAT_PriceDILoadDetail.Where(c => c.PriceDILoadID == diLoad.ID))
                            model.CAT_PriceDILoadDetail.Remove(detail);
                        model.CAT_PriceDILoad.Remove(diLoad);
                    }
                    model.SaveChanges();
                }
                else
                    throw FaultHelper.BusinessFault(null, null, "Không tìm thấy bảng giá");
            }
            catch (Exception ex)
            {
                throw FaultHelper.BusinessFault(ex);
            }
        }

        #endregion

        #endregion
        #endregion

        #region Price FTL

        #endregion

        #region common
        public static int GetTransportMode(DataEntities model, int catID)
        {
            var obj = model.CAT_TransportMode.FirstOrDefault(c => c.ID == catID);
            if (obj != null)
                return obj.TransportModeID;
            return -1;
        }

        public static int GetServiceOfOrder(DataEntities model, int? catID)
        {
            if (catID == null)
                return -(int)SYSVarType.ServiceOfOrderLocal;
            var obj = model.CAT_ServiceOfOrder.FirstOrDefault(c => c.ID == catID);
            if (obj != null)
                return obj.ServiceOfOrderID;
            return -1;
        }

        public static int GetTransportMode_First(DataEntities model, int sysID)
        {
            var obj = model.CAT_TransportMode.FirstOrDefault(c => c.TransportModeID == sysID);
            if (obj != null)
                return obj.ID;
            return -1;
        }

        public static int GetServiceOfOrder_First(DataEntities model, int? sysID)
        {
            var obj = model.CAT_ServiceOfOrder.FirstOrDefault(c => c.ServiceOfOrderID == sysID);
            if (obj != null)
                return obj.ID;
            return -1;
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
        #endregion

    }
}
