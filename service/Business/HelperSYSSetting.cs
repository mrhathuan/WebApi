using Data;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class HelperSYSSetting
    {
        public static DTOSYSSetting SYSSettingSystem_GetBySYSCustomerID(DataEntities model, int syscustomerID)
        {
            try
            {
                DTOSYSSetting result = new DTOSYSSetting();
                string sKey = SYSSettingKey.System.ToString();

                var item = model.SYS_Setting.FirstOrDefault(c => c.Key == sKey && c.SYSCustomerID == syscustomerID);
                if (item != null)
                {
                    if (!string.IsNullOrEmpty(item.Setting))
                    {
                        // Convert Setting thành list data
                        var lstData = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DTOSYSSetting>>(item.Setting);
                        if (lstData != null && lstData.Count > 0)
                            result = lstData.FirstOrDefault();
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

        public static CATMailTemplate MailTemplate_GetBySYSCustomerID(DataEntities model, MailTemplateCode template, int syscustomerID)
        {
            try
            {
                CATMailTemplate result = new CATMailTemplate();

                var obj = model.CAT_MailTemplate.FirstOrDefault(c => c.Code == template.ToString() && c.SYSCustomerID == syscustomerID);
                if (obj != null)
                {
                    using (var copy = new CopyHelper())
                    {
                        copy.Copy(obj, result);
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

        public static SYSUserSettingFunction SYSUserSettingFunction_Get(DataEntities model, AccountItem account, SYSUserSettingFunction item)
        {
            try
            {
                var result = new SYSUserSettingFunction();
                string strKey = SYSUserSettingKey.Function.ToString();
                if (account.UserID > 0)
                {
                    var obj = model.SYS_UserSetting.FirstOrDefault(c => c.Key == strKey && c.UserID == account.UserID.Value && c.ReferID == item.ReferID && c.ReferKey == item.ReferKey);
                    if (obj != null)
                    {
                        if (!string.IsNullOrEmpty(obj.Setting))
                        {
                            result = Newtonsoft.Json.JsonConvert.DeserializeObject<SYSUserSettingFunction>(obj.Setting);
                            result.ReferID = item.ReferID;
                            result.ReferKey = item.ReferKey;
                        }
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

        public static void SYSUserSettingFunction_Save(DataEntities model, AccountItem account, SYSUserSettingFunction item)
        {
            try
            {
                string strKey = SYSUserSettingKey.Function.ToString();
                if (item.ReferID > 0 && account.UserID > 0)
                {
                    var obj = model.SYS_UserSetting.FirstOrDefault(c => c.Key == strKey && c.UserID == account.UserID && c.ReferID == item.ReferID);
                    if (obj == null)
                    {
                        obj = new SYS_UserSetting();
                        obj.UserID = account.UserID.Value;
                        obj.ReferID = item.ReferID;
                        obj.Key = strKey;
                        obj.CreatedBy = account.UserName;
                        obj.CreatedDate = DateTime.Now;
                    }
                    else
                    {
                        obj.ModifiedBy = account.UserName;
                        obj.ModifiedDate = DateTime.Now;
                    }
                    obj.Setting = Newtonsoft.Json.JsonConvert.SerializeObject(item);

                    if (obj.ID < 1)
                        model.SYS_UserSetting.Add(obj);
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

        public static Dashboard_UserSetting SYSUserSetting_ORDDashboard_Get(DataEntities model, AccountItem account, int referid)
        {
            try
            {
                var result = new Dashboard_UserSetting();
                string strKey = SYSUserSettingKey.ORD_Dashboard.ToString();
                bool isNull = true;
                if (account.UserID > 0)
                {
                    var obj = model.SYS_UserSetting.FirstOrDefault(c => c.Key == strKey && c.UserID == account.UserID.Value && c.ReferID == referid);
                    if (obj != null)
                    {
                        try
                        {
                            if (!string.IsNullOrEmpty(obj.Setting))
                            {
                                result = Newtonsoft.Json.JsonConvert.DeserializeObject<Dashboard_UserSetting>(obj.Setting);
                                if (result.ListChart != null && result.ListWidget != null)
                                    isNull = false;
                            }
                        }
                        catch
                        {
                            model.SYS_UserSetting.Remove(obj);
                            model.SaveChanges();
                        }
                    }
                }

                // Tạo default config khi user chưa define
                if (isNull && account.UserID > 0 && referid > 0)
                {
                    if (result.ListChart == null)
                    {
                        result.UserID = account.UserID.Value;
                        result.ReferID = referid;
                        result.Layout = 1;
                        result.ListChart = new List<Dashboard_UserSetting_Chart>();
                        Dashboard_UserSetting_Chart chart = new Dashboard_UserSetting_Chart();
                        chart.ChartID = 1;
                        chart.ChartName = "Tổng đơn hàng";
                        chart.TypeOfChart = 1;
                        chart.ChartTheme = "Bootstrap";
                        chart.ChartType = "column";
                        chart.ChartKey = result.Layout + "_" + chart.ChartID;
                        chart.Color = "#000000";
                        chart.BackGroundColor = "#ffffff";
                        result.ListChart.Add(chart);
                    }

                    if (result.ListWidget == null)
                    {
                        result.ListWidget = new List<Dashboard_UserSetting_Chart>();
                        for (int i = 1; i <= 4; i++)
                        {
                            Dashboard_UserSetting_Chart widget = new Dashboard_UserSetting_Chart();
                            widget.ChartID = i;
                            widget.TypeOfChart = i;
                            widget.ChartKey = "Widget" + "_" + widget.ChartID;
                            result.ListWidget.Add(widget);

                            switch (i)
                            {
                                case 1: widget.ChartName = "Đơn hàng thành công"; break;
                                case 2: widget.ChartName = "Đơn hàng lập kế hoạch"; break;
                                case 3: widget.ChartName = "Vận chuyển chờ duyệt"; break;
                                case 4: widget.ChartName = "Vận chuyển đã duyệt"; break;
                            }
                        }
                    }

                    SYSUserSetting_ORDDashboard_Save(model, account, result);
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

        public static void SYSUserSetting_ORDDashboard_Save(DataEntities model, AccountItem account, Dashboard_UserSetting item)
        {
            try
            {
                string strKey = SYSUserSettingKey.ORD_Dashboard.ToString();
                if (item.ReferID > 0 && account.UserID > 0)
                {
                    var obj = model.SYS_UserSetting.FirstOrDefault(c => c.Key == strKey && c.UserID == account.UserID && c.ReferID == item.ReferID);
                    if (obj == null)
                    {
                        obj = new SYS_UserSetting();
                        obj.CreatedBy = account.UserName;
                        obj.CreatedDate = DateTime.Now;
                        obj.UserID = account.UserID.Value;
                        obj.ReferID = item.ReferID;
                        obj.Key = strKey;
                        obj.ReferKey = string.Empty;
                    }
                    else
                    {
                        obj.ModifiedBy = account.UserName;
                        obj.ModifiedDate = DateTime.Now;
                    }

                    foreach (var itemChart in item.ListChart)
                    {
                        itemChart.ChartKey = item.Layout + "_" + itemChart.ChartID;
                    }

                    obj.Setting = Newtonsoft.Json.JsonConvert.SerializeObject(item);

                    if (obj.ID < 1)
                        model.SYS_UserSetting.Add(obj);
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

        public static DTOTriggerMaterial SYSSettingSystem_GetMaterialPriceCurrent(DataEntities model, int syscustomerID)
        {
            try
            {
                DTOTriggerMaterial result = new DTOTriggerMaterial();
                string sKey = SYSSettingKey.Material.ToString();
                var sysSetting = model.SYS_Setting.FirstOrDefault(c => c.SYSCustomerID == syscustomerID && c.Key == sKey);
                if (sysSetting != null)
                {
                    try
                    {
                        result = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOTriggerMaterial>(sysSetting.Setting);
                    }
                    catch { }
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
    }
}
