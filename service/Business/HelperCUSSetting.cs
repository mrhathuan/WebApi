using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;
using System.ServiceModel;

namespace Business
{
    public class HelperCUSSetting
    {
        public static List<CUSSettingRevenueAllocationCO> RevenueAllocationCO_List(DataEntities model, int customerid)
        {
            try
            {
                string strKey = CUSSettingKey.RevenueAllocationCO.ToString();
                var objSetting = model.CUS_Setting.FirstOrDefault(c => c.CustomerID == customerid && c.Key == strKey);
                if (objSetting != null)
                {
                    if (!string.IsNullOrEmpty(objSetting.Setting))
                        return Newtonsoft.Json.JsonConvert.DeserializeObject<List<CUSSettingRevenueAllocationCO>>(objSetting.Setting);
                }

                //strKey = SYSVarType.StatusOfContainer.ToString();
                strKey = "";
                var result = new List<CUSSettingRevenueAllocationCO>();
                foreach (var status in model.SYS_Var.Where(c => c.SYS_Var2.Code == strKey))
                {
                    CUSSettingRevenueAllocationCO obj = new CUSSettingRevenueAllocationCO();
                    obj.StatusOfContainerID = status.ID;
                    obj.StatusOfContainerName = status.ValueOfVar;
                    obj.Rate = 0;
                    result.Add(obj);
                }
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

        public static List<DTOCustomerSettings> CustomerSettings_Get(DataEntities model, AccountItem account, int cusID)
        {
            try
            {
                string sKey = CUSSettingKey.Order.ToString();
                List<DTOCustomerSettings> result = new List<DTOCustomerSettings>();
                var item = model.CUS_Setting.FirstOrDefault(c => c.Key == sKey && c.CustomerID == cusID && c.SYSCustomerID == 2);
                if (item != null && !string.IsNullOrEmpty(item.Setting))
                    result = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DTOCustomerSettings>>(item.Setting);
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

        #region TenderLTL
        public static List<CUSSettingsTenderLTL> CUSSettingsTenderLTL_GetList(DataEntities model, AccountItem account, int referID)
        {
            try
            {
                string sKey = CUSSettingKey.TenderLTL.ToString();
                List<CUSSettingsTenderLTL> result = new List<CUSSettingsTenderLTL>();
                List<int> dataUse = new List<int>();

                var objUser = model.SYS_UserSetting.FirstOrDefault(c => c.UserID == account.UserID && c.Key == sKey && c.ReferID == referID);
                if (objUser != null && !string.IsNullOrEmpty(objUser.Setting))
                {
                    dataUse = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(objUser.Setting);
                }

                var data = model.CUS_Setting.Where(c => c.Key == sKey && c.ReferID == referID && c.SYSCustomerID == account.SYSCustomerID).Select(c => new
                {
                    ID = c.ID,
                    ReferID = c.ReferID,
                    Name = c.Name,
                    Key = c.Key,
                    Setting = c.Setting,
                    CreateDate = c.CreatedDate
                }).ToList();
                foreach (var obj in data)
                {
                    if (!string.IsNullOrEmpty(obj.Setting))
                    {
                        CUSSettingsTenderLTL item = Newtonsoft.Json.JsonConvert.DeserializeObject<CUSSettingsTenderLTL>(obj.Setting);
                        item.ID = obj.ID;
                        if (dataUse.Contains(obj.ID))
                            item.IsUse = true;
                        result.Add(item);
                    }
                }
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

        public static CUSSettingsTenderLTL CUSSettingsTenderLTL_Get(DataEntities model, AccountItem account, int id)
        {
            try
            {
                string sKey = CUSSettingKey.TenderLTL.ToString();
                CUSSettingsTenderLTL result = new CUSSettingsTenderLTL();
                var obj = model.CUS_Setting.FirstOrDefault(c => c.Key == sKey && c.ID == id && c.SYSCustomerID == account.SYSCustomerID);
                if (obj != null && !string.IsNullOrEmpty(obj.Setting))
                {
                    result = Newtonsoft.Json.JsonConvert.DeserializeObject<CUSSettingsTenderLTL>(obj.Setting);
                    var objUser = model.SYS_UserSetting.FirstOrDefault(c => c.UserID == account.UserID && c.Key == sKey && c.ReferID == obj.ReferID);
                    if (objUser != null && !string.IsNullOrEmpty(objUser.Setting))
                    {
                        var data = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(objUser.Setting);
                        if (data != null && data.Contains(result.ID))
                            result.IsUse = true;
                    }
                    result.ID = id;
                }
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

        public static void CUSSettingsTenderLTL_Save(DataEntities model, AccountItem account, CUSSettingsTenderLTL item)
        {
            try
            {
                string sKey = CUSSettingKey.TenderLTL.ToString();
                var obj = model.CUS_Setting.FirstOrDefault(c => c.Key == sKey && c.ID == item.ID && c.SYSCustomerID == account.SYSCustomerID);
                if (obj == null)
                {
                    obj = new CUS_Setting();
                    obj.CreatedBy = account.UserName;
                    obj.CreatedDate = DateTime.Now;
                    model.CUS_Setting.Add(obj);

                    obj.Key = sKey;
                    obj.CustomerID = account.SYSCustomerID;
                    obj.SYSCustomerID = account.SYSCustomerID;
                    item.CreateDate = DateTime.Now;
                }
                else
                {
                    obj.ModifiedBy = account.UserName;
                    obj.ModifiedDate = DateTime.Now;
                }
                obj.ReferID = item.ReferID;
                obj.Name = item.Name;
                obj.Setting = Newtonsoft.Json.JsonConvert.SerializeObject(item);

                model.SaveChanges();
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

        public static void CUSSettingsTenderLTL_Delete(DataEntities model, AccountItem account, int id)
        {
            try
            {
                string sKey = CUSSettingKey.TenderLTL.ToString();
                var obj = model.CUS_Setting.FirstOrDefault(c => c.Key == sKey && c.ID == id && c.SYSCustomerID == account.SYSCustomerID);
                if (obj != null)
                {
                    model.CUS_Setting.Remove(obj);
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
        #endregion

        #region TenderFCL
        public static List<CUSSettingsTenderFCL> CUSSettingsTenderFCL_GetList(DataEntities model, AccountItem account, int referID)
        {
            try
            {
                string sKey = CUSSettingKey.TenderFCL.ToString();
                List<CUSSettingsTenderFCL> result = new List<CUSSettingsTenderFCL>();               
                List<int> dataUse = new List<int>();
                var objUser = model.SYS_UserSetting.FirstOrDefault(c => c.UserID == account.UserID && c.Key == sKey && c.ReferID == referID);
                if (objUser != null && !string.IsNullOrEmpty(objUser.Setting))
                {
                    dataUse = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(objUser.Setting);
                }

                var data = model.CUS_Setting.Where(c => c.Key == sKey && c.ReferID == referID && c.SYSCustomerID == account.SYSCustomerID).Select(c => new
                {
                    ID = c.ID,
                    ReferID = c.ReferID,
                    Name = c.Name,
                    Key = c.Key,
                    Setting = c.Setting,
                    CreateDate = c.CreatedDate
                }).ToList();
                foreach (var obj in data)
                {
                    if (!string.IsNullOrEmpty(obj.Setting))
                    {
                        CUSSettingsTenderFCL item = Newtonsoft.Json.JsonConvert.DeserializeObject<CUSSettingsTenderFCL>(obj.Setting);
                        item.ID = obj.ID;
                        if (dataUse.Contains(obj.ID))
                            item.IsUse = true;
                        result.Add(item);
                    }
                }
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

        public static CUSSettingsTenderFCL CUSSettingsTenderFCL_Get(DataEntities model, AccountItem account, int id)
        {
            try
            {
                string sKey = CUSSettingKey.TenderFCL.ToString();
                CUSSettingsTenderFCL result = new CUSSettingsTenderFCL();
                var obj = model.CUS_Setting.FirstOrDefault(c => c.Key == sKey && c.ID == id && c.SYSCustomerID == account.SYSCustomerID);
                if (obj != null && !string.IsNullOrEmpty(obj.Setting))
                {
                    result = Newtonsoft.Json.JsonConvert.DeserializeObject<CUSSettingsTenderFCL>(obj.Setting);
                    var objUser = model.SYS_UserSetting.FirstOrDefault(c => c.UserID == account.UserID && c.Key == sKey && c.ReferID == obj.ReferID);
                    if (objUser != null && !string.IsNullOrEmpty(objUser.Setting))
                    {
                        var data = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(objUser.Setting);
                        if (data != null && data.Contains(result.ID))
                            result.IsUse = true;
                    }
                    result.ID = id;
                }
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

        public static void CUSSettingsTenderFCL_Save(DataEntities model, AccountItem account, CUSSettingsTenderFCL item)
        {
            try
            {
                string sKey = CUSSettingKey.TenderFCL.ToString();
                var obj = model.CUS_Setting.FirstOrDefault(c => c.Key == sKey && c.ID == item.ID && c.SYSCustomerID == account.SYSCustomerID);
                if (obj == null)
                {
                    obj = new CUS_Setting();
                    obj.CreatedBy = account.UserName;
                    obj.CreatedDate = DateTime.Now;
                    model.CUS_Setting.Add(obj);

                    obj.Key = sKey;
                    obj.CustomerID = account.SYSCustomerID;
                    obj.SYSCustomerID = account.SYSCustomerID;
                    item.CreateDate = DateTime.Now;
                }
                else
                {
                    obj.ModifiedBy = account.UserName;
                    obj.ModifiedDate = DateTime.Now;
                }
                obj.ReferID = item.ReferID;
                obj.Name = item.Name;
                obj.Setting = Newtonsoft.Json.JsonConvert.SerializeObject(item);

                model.SaveChanges();
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

        public static void CUSSettingsTenderFCL_Delete(DataEntities model, AccountItem account, int id)
        {
            try
            {
                string sKey = CUSSettingKey.TenderFCL.ToString();
                var obj = model.CUS_Setting.FirstOrDefault(c => c.Key == sKey && c.ID == id && c.SYSCustomerID == account.SYSCustomerID);
                if (obj != null)
                {
                    model.CUS_Setting.Remove(obj);
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
        #endregion TenderFCL

        #region OPSDIImport
        public static List<DTOCUSSettingPlan> CUSSettingsOPSImport_GetList(DataEntities model, AccountItem account)
        {
            try
            {
                List<DTOCUSSettingPlan> result = new List<DTOCUSSettingPlan>();
                string sKey = CUSSettingKey.Plan.ToString();
                var data = model.CUS_Setting.Where(c => c.Key == sKey && c.SYSCustomerID == account.SYSCustomerID && (account.SYSCustomerID == c.CustomerID || account.ListCustomerID.Contains(c.CustomerID))).ToList();
                foreach (var item in data)
                {
                    var obj = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOCUSSettingPlan>(item.Setting);
                    if (obj != null)
                    {
                        obj.ID = item.ID;
                        obj.CreateBy = item.CreatedBy;
                        obj.CreateDate = item.CreatedDate;
                        result.Add(obj);
                    }
                }
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

        public static DTOCUSSettingPlan CUSSettingsOPSImport_Get(DataEntities model, AccountItem account, int sID)
        {
            try
            {
                DTOCUSSettingPlan result = new DTOCUSSettingPlan();
                string sKey = CUSSettingKey.Plan.ToString();
                var item = model.CUS_Setting.Where(c => c.Key == sKey && c.SYSCustomerID == account.SYSCustomerID && (account.SYSCustomerID == c.CustomerID || account.ListCustomerID.Contains(c.CustomerID)) && c.ID == sID).FirstOrDefault();
                if (item != null)
                {
                    result = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOCUSSettingPlan>(item.Setting);
                    result.ID = item.ID;
                    result.CreateBy = item.CreatedBy;
                    result.CreateDate = item.CreatedDate;
                }
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

        #region CUSSettingsReport
        public static List<CUSSettingsReport> CUSSettingsReport_List(DataEntities model, AccountItem account, int referid)
        {
            try
            {
                string strKey = CUSSettingKey.Report.ToString();
                var result = new List<CUSSettingsReport>();
                foreach (var itemSetting in model.CUS_Setting.Where(c => c.Key == strKey && c.ReferID == referid && c.CustomerID == account.SYSCustomerID).Select(c => new { c.ID, c.Key, c.ReferID, c.Name, c.Setting }))
                {
                    if (!string.IsNullOrEmpty(itemSetting.Setting))
                    {
                        var item = Newtonsoft.Json.JsonConvert.DeserializeObject<CUSSettingsReport>(itemSetting.Setting);
                        item.ID = itemSetting.ID;
                        item.ReferID = itemSetting.ReferID;
                        item.Name = itemSetting.Name;
                        item.Key = itemSetting.Key;
                        if (item.TypeOfDate == null)
                            item.TypeOfDate = 0;
                        if (item.ListGroupOfLocation == null)
                            item.ListGroupOfLocation = new List<CUSSettingsReport_GroupOfLocation>();
                        if (item.ListGroupOfPartner == null)
                            item.ListGroupOfPartner = new List<CUSSettingsReport_GroupOfPartner>();
                        if (item.ListProvince == null)
                            item.ListProvince = new List<CUSSettingsReport_Province>();
                        if (item.ListServiceOfOrder == null)
                            item.ListServiceOfOrder = new List<CUSSettingsReport_ServiceOfOrder>();
                        if (item.ListOrderRouting == null)
                            item.ListOrderRouting = new List<CUSSettingsReport_Routing>();
                        if (item.ListOPSRouting == null)
                            item.ListOPSRouting = new List<CUSSettingsReport_Routing>();
                        if (item.ListPartner == null)
                            item.ListPartner = new List<CUSSettingsReport_Partner>();

                        if (item.ListCustomer != null && item.ListCustomer.Count > 0)
                        {
                            var lstCustomerID = item.ListCustomer.Select(c => c.CustomerID).Distinct().ToList();
                            if (account.ListCustomerID.Any(c => lstCustomerID.Contains(c)))
                                result.Add(item);
                        }
                        else
                            result.Add(item);
                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                throw FaultHelper.BusinessFault(ex);
            }
        }

        public static void CUSSettingsReport_Save(DataEntities model, AccountItem account, CUSSettingsReport item)
        {
            try
            {
                string strKey = CUSSettingKey.Report.ToString();
                var obj = model.CUS_Setting.FirstOrDefault(c => c.ID == item.ID);
                if (obj == null)
                {
                    obj = new CUS_Setting();
                    obj.Key = strKey;
                    obj.ReferID = item.ReferID;
                    obj.CreatedBy = account.UserName;
                    obj.CreatedDate = DateTime.Now;
                    obj.CustomerID = account.SYSCustomerID;
                    obj.SYSCustomerID = account.SYSCustomerID;
                    item.CreateDate = DateTime.Now;
                }
                else
                {
                    obj.ModifiedBy = account.UserName;
                    obj.ModifiedDate = DateTime.Now;
                }
                obj.Name = item.Name;
                item.TypeOfDate = item.TypeOfDate == null ? 0 : item.TypeOfDate.Value;
                obj.Setting = Newtonsoft.Json.JsonConvert.SerializeObject(item);
                if (obj.ID < 1)
                    model.CUS_Setting.Add(obj);
                model.SaveChanges();
            }
            catch (Exception ex)
            {
                throw FaultHelper.BusinessFault(ex);
            }
        }

        public static void CUSSettingsReport_Delete(DataEntities model, AccountItem account, List<int> lstid)
        {
            try
            {
                if (lstid.Count > 0)
                {
                    foreach (var item in model.CUS_Setting.Where(c => lstid.Contains(c.ID)))
                        model.CUS_Setting.Remove(item);
                    model.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw FaultHelper.BusinessFault(ex);
            }
        }
        #endregion

        #region CUSSettingsPlan
        public static List<CUSSettingsReport> CUSSettingsPlan_List(DataEntities model, AccountItem account, int referid)
        {
            try
            {
                string strKey = CUSSettingKey.Plan.ToString();
                var result = new List<CUSSettingsReport>();
                foreach (var itemSetting in model.CUS_Setting.Where(c => c.Key == strKey && c.ReferID == referid && c.CustomerID == account.SYSCustomerID).Select(c => new { c.ID, c.Key, c.ReferID, c.Name, c.Setting }))
                {
                    if (!string.IsNullOrEmpty(itemSetting.Setting))
                    {
                        var item = Newtonsoft.Json.JsonConvert.DeserializeObject<CUSSettingsReport>(itemSetting.Setting);
                        item.ID = itemSetting.ID;
                        item.ReferID = itemSetting.ReferID;
                        item.Name = itemSetting.Name;
                        item.Key = itemSetting.Key;

                        if (item.ListServiceOfOrder == null)
                            item.ListServiceOfOrder = new List<CUSSettingsReport_ServiceOfOrder>();

                        if (item.ListCustomer != null && item.ListCustomer.Count > 0)
                        {
                            var lstCustomerID = item.ListCustomer.Select(c => c.CustomerID).Distinct().ToList();
                            if (account.ListCustomerID.Any(c => lstCustomerID.Contains(c)))
                                result.Add(item);
                        }
                        else
                            result.Add(item);
                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                throw FaultHelper.BusinessFault(ex);
            }
        }

        public static void CUSSettingsPlan_Save(DataEntities model, AccountItem account, CUSSettingsReport item)
        {
            try
            {
                string strKey = CUSSettingKey.Plan.ToString();
                var obj = model.CUS_Setting.FirstOrDefault(c => c.ID == item.ID);
                if (obj == null)
                {
                    obj = new CUS_Setting();
                    obj.Key = strKey;
                    obj.ReferID = item.ReferID;
                    obj.CreatedBy = account.UserName;
                    obj.CreatedDate = DateTime.Now;
                    obj.CustomerID = account.SYSCustomerID;
                    obj.SYSCustomerID = account.SYSCustomerID;
                    item.CreateDate = DateTime.Now;
                }
                else
                {
                    obj.ModifiedBy = account.UserName;
                    obj.ModifiedDate = DateTime.Now;
                }
                obj.Name = item.Name;
                obj.Setting = Newtonsoft.Json.JsonConvert.SerializeObject(item);
                if (obj.ID < 1)
                    model.CUS_Setting.Add(obj);
                model.SaveChanges();
            }
            catch (Exception ex)
            {
                throw FaultHelper.BusinessFault(ex);
            }
        }

        public static void CUSSettingsPlan_Delete(DataEntities model, AccountItem account, List<int> lstid)
        {
            try
            {
                if (lstid.Count > 0)
                {
                    foreach (var item in model.CUS_Setting.Where(c => lstid.Contains(c.ID)))
                        model.CUS_Setting.Remove(item);
                    model.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw FaultHelper.BusinessFault(ex);
            }
        }
        #endregion

        #region CUSSettingsCutOfTimeSuggest
        public static CUSSettingsCutOffTimeSuggest CUSSettingsReport_CutOfTimeSuggest_Get(DataEntities model, AccountItem account, int cusID)
        {
            try
            {
                string strKey = CUSSettingKey.CutOfTimeSuggest.ToString();
                var result = new CUSSettingsCutOffTimeSuggest();

                var obj = model.CUS_Setting.FirstOrDefault(c => c.CustomerID == cusID && c.Key == strKey && c.SYSCustomerID == account.SYSCustomerID);
                if (obj != null && !string.IsNullOrEmpty(obj.Setting) && cusID > 0)
                {
                    try
                    {
                        result = Newtonsoft.Json.JsonConvert.DeserializeObject<CUSSettingsCutOffTimeSuggest>(obj.Setting);
                    }
                    catch { CUSSettingsReport_CutOfTimeSuggest_Save(model, account, cusID, result); }
                }
                return result;
            }
            catch (Exception ex)
            {
                throw FaultHelper.BusinessFault(ex);
            }
        }

        public static void CUSSettingsReport_CutOfTimeSuggest_Save(DataEntities model, AccountItem account, int cusID, CUSSettingsCutOffTimeSuggest item)
        {
            try
            {
                if (cusID > 0)
                {
                    string strKey = CUSSettingKey.CutOfTimeSuggest.ToString();

                    var obj = model.CUS_Setting.FirstOrDefault(c => c.CustomerID == cusID && c.Key == strKey && c.SYSCustomerID == account.SYSCustomerID);
                    if (obj == null)
                    {
                        obj = new CUS_Setting();
                        obj.Key = strKey;
                        obj.CreatedBy = account.UserName;
                        obj.CreatedDate = DateTime.Now;
                        obj.CustomerID = cusID;
                        obj.SYSCustomerID = account.SYSCustomerID;
                    }
                    else
                    {
                        obj.ModifiedBy = account.UserName;
                        obj.ModifiedDate = DateTime.Now;
                    }
                    obj.Setting = Newtonsoft.Json.JsonConvert.SerializeObject(item);
                    if (obj.ID < 1)
                        model.CUS_Setting.Add(obj);
                    model.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw FaultHelper.BusinessFault(ex);
            }
        }
        #endregion
    }
}