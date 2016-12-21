using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kendo.Mvc.Extensions;
using Data;
using DTO;
using System.ServiceModel;
using OfficeOpenXml;

namespace Business
{
    public class BLSystem : Base, IBase
    {
        #region App
        public void App_Init()
        {
            try
            {

            }
            catch (Exception ex)
            {
                throw FaultHelper.BusinessFault(ex);
            }
        }

        public DTOAuthorization App_User()
        {
            try
            {
                var result = default(DTOAuthorization);
                if (string.IsNullOrEmpty(Account.UserName))
                    throw new Exception("Account empty");
                using (var model = new DataEntities())
                {
                    string username = Account.UserName;
                    result = model.SYS_User.Where(c => c.UserName == username).Select(c => new DTOAuthorization
                    {
                        UserID = c.ID,
                        GroupID = c.GroupID,
                        IsAdmin = c.IsAdmin,
                        UserName = Account.UserName,
                        LastName = c.LastName,
                        FirstName = c.FirstName,
                        SYSCustomerID = c.SYSCustomerID > 0 ? c.SYSCustomerID.Value : -1,
                        DriverID = c.DriverID > 0 ? c.DriverID.Value : -1,
                        ListCustomerID = c.ListCustomerID,
                        CustomerID = c.CustomerID > 0 ? c.CustomerID.Value : -1,
                    }).FirstOrDefault();

                    if (result != null)
                    {
                        var objCustomer = model.CUS_Customer.Where(c => c.ID == result.SYSCustomerID).Select(c => new { c.CustomerName, c.Address, c.TelNo, c.Fax, c.Email, c.Note, c.Note1, c.Note2, c.Image }).FirstOrDefault();
                        if (objCustomer != null)
                        {
                            result.CustomerName = objCustomer.CustomerName;
                            result.Address = objCustomer.Address;
                            result.TelNo = objCustomer.TelNo;
                            result.Fax = objCustomer.Fax;
                            result.Email = objCustomer.Email;
                            result.Note = objCustomer.Note;
                            result.Note1 = objCustomer.Note1;
                            result.Note2 = objCustomer.Note2;
                            result.Image = objCustomer.Image;
                        }
                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                throw FaultHelper.BusinessFault(ex);
            }
        }

        public List<SYSFunction> App_ListFunction(int parentid)
        {
            try
            {
                var result = new List<SYSFunction>();
                using (var model = new DataEntities())
                {
                    var obj = model.SYS_User.Where(c => c.UserName == Account.UserName).Select(c => new
                    {
                        UserID = c.ID,
                        GroupID = c.GroupID,
                        IsAdmin = c.IsAdmin
                    }).FirstOrDefault();
                    if (obj != null)
                    {
                        if (obj.GroupID > 0)
                        {
                            //string strKey = SYSUserSettingKey.Function.ToString();
                            //var lstSettingData = model.SYS_UserSetting.Where(c => c.Key == strKey && c.UserID == Account.UserID.Value).Select(c => new
                            //{
                            //    c.ReferID,
                            //    c.ReferKey,
                            //    c.Setting,
                            //    c.Key
                            //}).ToList();
                            //var lstSetting = new List<SYSUserSettingFunction>();
                            //foreach (var settingdata in lstSettingData)
                            //{
                            //    if (!string.IsNullOrEmpty(settingdata.Setting))
                            //    {
                            //        try
                            //        {
                            //            var item = Newtonsoft.Json.JsonConvert.DeserializeObject<SYSUserSettingFunction>(settingdata.Setting);
                            //            if (item != null)
                            //            {
                            //                item.ReferID = settingdata.ReferID;
                            //                item.ReferKey = settingdata.ReferKey;
                            //                lstSetting.Add(item);
                            //            }
                            //        }
                            //        catch { }
                            //    }
                            //}

                            //var objDefind = model.WFL_DefineGroup.Where(c => c.GroupID == obj.GroupID.Value).Select(c => new { c.DefineID }).FirstOrDefault();
                            //if (objDefind != null)
                            //{
                            //    var lstWorkFlowID = model.WFL_DefineWF.Where(c => c.DefineID == objDefind.DefineID).Select(c => c.WorkFlowID).ToList();
                            //    var lstData = model.WFL_WorkFlowFunction.Where(c => lstWorkFlowID.Contains(c.WorkFlowID)).Select(c => new
                            //    {
                            //        c.FunctionID,
                            //        FunctionCode = c.SYS_Function.Code,
                            //        FunctionName = c.SYS_Function.FunctionName,
                            //        c.SYS_Function.Icon,
                            //        c.SYS_Function.IsApproved,
                            //        c.SYS_Function.Level,
                            //        c.ActionID,
                            //        ActionCode = c.SYS_Action.Code,
                            //        c.SYS_Function.ParentID,
                            //        c.SYS_Function.SortOrder
                            //    }).OrderBy(c => c.SortOrder).ToList();
                            //    var objParent = lstData.FirstOrDefault(c => c.FunctionID == parentid);
                            //    if (objParent != null)
                            //    {
                            //        int sortStart = objParent.SortOrder;
                            //        int sortEnd = -1;
                            //        var objParentNext = lstData.Where(c => c.ParentID == objParent.ParentID && c.SortOrder > objParent.SortOrder).OrderBy(c => c.SortOrder).Select(c => new { c.SortOrder }).FirstOrDefault();
                            //        if (objParentNext != null)
                            //            sortEnd = objParentNext.SortOrder;
                            //        var lst = lstData.Where(c => c.SortOrder >= sortStart && (sortEnd < 0 || c.SortOrder < sortEnd) && c.IsApproved && c.ActionID == null).Select(c => new SYSFunction
                            //        {
                            //            ID = c.FunctionID,
                            //            ParentID = c.ParentID,
                            //            Code = c.FunctionCode,
                            //            FunctionName = c.FunctionName,
                            //            Icon = c.Icon,
                            //            Description = "",
                            //            SortOrder = c.SortOrder,
                            //            Level = c.Level,
                            //            HasChild = false
                            //        }).OrderBy(c => c.SortOrder).ToList();
                            //        foreach (var item in lst)
                            //        {
                            //            item.HasChild = lst.Where(c => c.ParentID == item.ID).Count() > 0;
                            //            item.ListSettings = lstSetting.Where(c => c.ReferID == item.ID).ToList();
                            //            item.ListActionCode = lstData.Where(c => c.FunctionID == item.ID && c.ActionID > 0).Select(c => c.ActionCode).ToList();
                            //            result.Add(item);
                            //        }
                            //    }
                            //}

                            var lstAction = model.SYS_FunctionInGroup.Where(c => c.GroupID == obj.GroupID.Value && c.ActionID > 0 && c.IsApproved).Select(c => new
                            {
                                c.FunctionID,
                                c.SYS_Action.Code
                            }).Distinct().ToList();
                            var lstDefineGroupID = model.WFL_DefineGroup.Where(c => c.GroupID == obj.GroupID && c.WFL_Define.WFL_DefineWF.Count > 0 && c.WFL_Define.SYSCustomerID == Account.SYSCustomerID).Select(c => c.DefineID).ToList();
                            var lstWorfFlowID = model.WFL_DefineWF.Where(c => lstDefineGroupID.Contains(c.DefineID) && c.WFL_WorkFlow.IsApproved).Select(c => c.WorkFlowID).Distinct().ToList();
                            lstAction.AddRange(model.WFL_WorkFlowFunction.Where(c => lstWorfFlowID.Contains(c.WorkFlowID) && c.ActionID > 0).Select(c => new
                                {
                                    c.FunctionID,
                                    c.SYS_Action.Code
                                }).Distinct().ToList());

                            string strKey = SYSUserSettingKey.Function.ToString();
                            var lstSettingData = model.SYS_UserSetting.Where(c => c.Key == strKey && c.UserID == Account.UserID.Value).Select(c => new
                            {
                                c.ReferID,
                                c.ReferKey,
                                c.Setting,
                                c.Key
                            }).ToList();
                            var lstSetting = new List<SYSUserSettingFunction>();
                            foreach (var settingdata in lstSettingData)
                            {
                                if (!string.IsNullOrEmpty(settingdata.Setting))
                                {
                                    try
                                    {
                                        var item = Newtonsoft.Json.JsonConvert.DeserializeObject<SYSUserSettingFunction>(settingdata.Setting);
                                        if (item != null)
                                        {
                                            item.ReferID = settingdata.ReferID;
                                            item.ReferKey = settingdata.ReferKey;
                                            lstSetting.Add(item);
                                        }
                                    }
                                    catch { }
                                }
                            }
                            var objParent = model.SYS_Function.Where(c => c.ID == parentid).Select(c => new { c.SortOrder, c.ParentID }).FirstOrDefault();
                            if (objParent != null)
                            {
                                long sortStart = objParent.SortOrder;
                                long sortEnd = -1;
                                var objParentNext = model.SYS_Function.Where(c => c.ParentID == objParent.ParentID && c.SortOrder > objParent.SortOrder).OrderBy(c => c.SortOrder).Select(c => new { c.SortOrder }).FirstOrDefault();
                                if (objParentNext != null)
                                    sortEnd = objParentNext.SortOrder;

                                var lst = model.SYS_FunctionInGroup.Where(c => c.SYS_Function.SortOrder >= sortStart && (sortEnd < 0 || c.SYS_Function.SortOrder < sortEnd) && c.SYS_Function.IsApproved && c.ActionID == null && c.GroupID == obj.GroupID && c.IsApproved).Select(c => new SYSFunction
                                {
                                    ID = c.FunctionID,
                                    ParentID = c.SYS_Function.ParentID,
                                    Code = c.SYS_Function.Code,
                                    FunctionName = c.SYS_Function.FunctionName,
                                    Icon = c.SYS_Function.Icon,
                                    Description = "",
                                    SortOrder = c.SYS_Function.SortOrder,
                                    Level = c.SYS_Function.Level,
                                    HasChild = false
                                }).OrderBy(c => c.SortOrder).ToList();
                                foreach (var item in lst)
                                {
                                    item.HasChild = lst.Where(c => c.ParentID == item.ID).Count() > 0;
                                    item.ListSettings = lstSetting.Where(c => c.ReferID == item.ID).ToList();
                                    item.ListActionCode = lstAction.Where(c => c.FunctionID == item.ID).Select(c => c.Code).ToList();
                                    result.Add(item);
                                }
                            }
                        }
                        if (Account.IsAdmin)
                        {
                            var lst = model.SYS_Function.Where(c => c.IsAdmin && c.IsApproved).OrderBy(c => c.SortOrder).Select(c => new SYSFunction
                            {
                                ID = c.ID,
                                ParentID = c.ParentID,
                                Code = c.Code,
                                FunctionName = c.FunctionName,
                                Icon = c.Icon,
                                Description = "",
                                Level = c.Level,
                                HasChild = false
                            }).ToList();
                            int currentLevel = -1;
                            var objFirst = lst.FirstOrDefault(c => c.ParentID == parentid);
                            if (objFirst != null)
                            {
                                currentLevel = objFirst.Level;
                                var query1 = lst.Where(c => c.Level == currentLevel).OrderBy(c => c.SortOrder);
                                foreach (var level1 in query1)
                                {
                                    if (result.Where(c => c.ID == level1.ID).Count() == 0)
                                    {
                                        var query2 = lst.Where(c => c.ParentID == level1.ID).OrderBy(c => c.SortOrder);
                                        level1.HasChild = query2.Count() > 0;
                                        result.Add(level1);
                                        foreach (var level2 in query2)
                                        {
                                            if (result.Where(c => c.ID == level2.ID).Count() == 0)
                                            {
                                                var query3 = lst.Where(c => c.ParentID == level2.ID).OrderBy(c => c.SortOrder);
                                                level2.HasChild = query3.Count() > 0;
                                                result.Add(level2);
                                                foreach (var level3 in query3)
                                                {
                                                    result.Add(level3);
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                throw FaultHelper.BusinessFault(ex);
            }
        }

        //public List<SYSFunction> App_ListFunction(int parentid)
        //{
        //    try
        //    {
        //        var result = new List<SYSFunction>();
        //        using (var model = new DataEntities())
        //        {
        //            var obj = model.SYS_User.Where(c => c.UserName == Account.UserName).Select(c => new
        //            {
        //                UserID = c.ID,
        //                GroupID = c.GroupID,
        //                IsAdmin = c.IsAdmin
        //            }).FirstOrDefault();
        //            if (obj != null)
        //            {
        //                if (obj.GroupID > 0)
        //                {
        //                    string strKey = SYSUserSettingKey.Function.ToString();
        //                    var lstSettingData = model.SYS_UserSetting.Where(c => c.Key == strKey && c.UserID == Account.UserID.Value).Select(c => new
        //                    {
        //                        c.ReferID,
        //                        c.ReferKey,
        //                        c.Setting,
        //                        c.Key
        //                    }).ToList();
        //                    var lstSetting = new List<SYSUserSettingFunction>();
        //                    foreach (var settingdata in lstSettingData)
        //                    {
        //                        if (!string.IsNullOrEmpty(settingdata.Setting))
        //                        {
        //                            try
        //                            {
        //                                var item = Newtonsoft.Json.JsonConvert.DeserializeObject<SYSUserSettingFunction>(settingdata.Setting);
        //                                if (item != null)
        //                                {
        //                                    item.ReferID = settingdata.ReferID;
        //                                    item.ReferKey = settingdata.ReferKey;
        //                                    lstSetting.Add(item);
        //                                }
        //                            }
        //                            catch { }
        //                        }
        //                    }

        //                    var lstDefineID = model.WFL_DefineGroup.Where(c => c.GroupID == obj.GroupID.Value && c.WFL_Define.WFL_DefineWF.Count > 0 && c.WFL_Define.SYSCustomerID == Account.SYSCustomerID).Select(c => c.DefineID).Distinct().ToList();
        //                    if (lstDefineID.Count > 0)
        //                    {
        //                        var lstWorkFlowID = model.WFL_DefineWF.Where(c => lstDefineID.Contains(c.DefineID)).Select(c => c.WorkFlowID).Distinct().ToList();
        //                        var lstData = model.WFL_WorkFlowFunction.Where(c => lstWorkFlowID.Contains(c.WorkFlowID)).Select(c => new
        //                        {
        //                            c.FunctionID,
        //                            FunctionCode = c.SYS_Function.Code,
        //                            FunctionName = c.SYS_Function.FunctionName,
        //                            c.SYS_Function.Icon,
        //                            c.SYS_Function.IsApproved,
        //                            c.SYS_Function.Level,
        //                            c.ActionID,
        //                            ActionCode = c.SYS_Action.Code,
        //                            c.SYS_Function.ParentID,
        //                            c.SYS_Function.SortOrder
        //                        }).OrderBy(c => c.SortOrder).Distinct().ToList();
        //                        var objParent = lstData.FirstOrDefault(c => c.FunctionID == parentid);
        //                        if (objParent != null)
        //                        {
        //                            int sortStart = objParent.SortOrder;
        //                            int sortEnd = -1;
        //                            var objParentNext = lstData.Where(c => c.ParentID == objParent.ParentID && c.SortOrder > objParent.SortOrder).OrderBy(c => c.SortOrder).Select(c => new { c.SortOrder }).FirstOrDefault();
        //                            if (objParentNext != null)
        //                                sortEnd = objParentNext.SortOrder;
        //                            var lst = lstData.Where(c => c.SortOrder >= sortStart && (sortEnd < 0 || c.SortOrder < sortEnd) && c.IsApproved && c.ActionID == null).Select(c => new SYSFunction
        //                            {
        //                                ID = c.FunctionID,
        //                                ParentID = c.ParentID,
        //                                Code = c.FunctionCode,
        //                                FunctionName = c.FunctionName,
        //                                Icon = c.Icon,
        //                                Description = "",
        //                                SortOrder = c.SortOrder,
        //                                Level = c.Level,
        //                                HasChild = false
        //                            }).OrderBy(c => c.SortOrder).ToList();
        //                            foreach (var item in lst)
        //                            {
        //                                item.HasChild = lst.Where(c => c.ParentID == item.ID).Count() > 0;
        //                                item.ListSettings = lstSetting.Where(c => c.ReferID == item.ID).ToList();
        //                                item.ListActionCode = lstData.Where(c => c.FunctionID == item.ID && c.ActionID > 0).Select(c => c.ActionCode).ToList();
        //                                result.Add(item);
        //                            }
        //                        }
        //                    }
        //                }

        //                if (Account.IsAdmin)
        //                {
        //                    var lstFunctionID = result.Select(c => c.ID).Distinct().ToList();

        //                    var lst = model.SYS_Function.Where(c => !lstFunctionID.Contains(c.ID) && c.IsAdmin && c.IsApproved).OrderBy(c => c.SortOrder).Select(c => new SYSFunction
        //                    {
        //                        ID = c.ID,
        //                        ParentID = c.ParentID,
        //                        Code = c.Code,
        //                        FunctionName = c.FunctionName,
        //                        Icon = c.Icon,
        //                        Description = "",
        //                        Level = c.Level,
        //                        HasChild = false
        //                    }).ToList();
        //                    int currentLevel = -1;
        //                    var objFirst = lst.FirstOrDefault(c => c.ParentID == parentid);
        //                    if (objFirst != null)
        //                    {
        //                        currentLevel = objFirst.Level;
        //                        var query1 = lst.Where(c => c.Level == currentLevel).OrderBy(c => c.SortOrder);
        //                        foreach (var level1 in query1)
        //                        {
        //                            if (result.Where(c => c.ID == level1.ID).Count() == 0)
        //                            {
        //                                var query2 = lst.Where(c => c.ParentID == level1.ID).OrderBy(c => c.SortOrder);
        //                                level1.HasChild = query2.Count() > 0;
        //                                result.Add(level1);
        //                                foreach (var level2 in query2)
        //                                {
        //                                    if (result.Where(c => c.ID == level2.ID).Count() == 0)
        //                                    {
        //                                        var query3 = lst.Where(c => c.ParentID == level2.ID).OrderBy(c => c.SortOrder);
        //                                        level2.HasChild = query3.Count() > 0;
        //                                        result.Add(level2);
        //                                        foreach (var level3 in query3)
        //                                        {
        //                                            result.Add(level3);
        //                                        }
        //                                    }
        //                                }
        //                            }
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //        return result;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw FaultHelper.BusinessFault(ex);
        //    }
        //}


        public List<SYSResource> App_ListResource()
        {
            try
            {
                var result = new List<SYSResource>();
                using (var model = new DataEntities())
                {
                    result = model.SYS_UserResource.Where(c => c.UserID == Account.UserID).Select(c => new SYSResource
                    {
                        ID = c.ResourceID,
                        Key = c.SYS_Resource.Key,
                        Name = c.Name,
                        ShortName = c.ShortName
                    }).ToList();
                }
                return result;
            }
            catch (Exception ex)
            {
                throw FaultHelper.BusinessFault(ex);
            }
        }

        public List<SYSResource> App_ListResourceEmpty()
        {
            try
            {
                var result = new List<SYSResource>();
                using (var model = new DataEntities())
                {
                    //result = model.SYS_Resource.Where(c => c.IsDefault == true).Select(c => new SYSResource
                    //{
                    //    ID = c.ID,
                    //    Key = c.Key,
                    //    Name = c.Name,
                    //    ShortName = c.ShortName
                    //}).ToList();
                    result = model.SYS_Resource.Select(c => new SYSResource
                    {
                        ID = c.ID,
                        Key = c.Key,
                        Name = c.Name,
                        ShortName = c.ShortName
                    }).ToList();
                }
                return result;
            }
            catch (Exception ex)
            {
                throw FaultHelper.BusinessFault(ex);
            }
        }

        public void App_UserOptionsSetting_Save(int referID, string referKey, Dictionary<string, string> options)
        {
            try
            {
                using (var model = new DataEntities())
                {
                    model.EventAccount = Account; model.EventRunning = false;

                    var strKey = SYSUserSettingKey.Function.ToString();
                    var objFunc = new SYSUserSettingFunction();

                    var obj = model.SYS_UserSetting.FirstOrDefault(c => c.Key == strKey && c.UserID == Account.UserID.Value && c.ReferID == referID && c.ReferKey == referKey);
                    if (obj == null)
                    {
                        obj = new SYS_UserSetting();
                        obj.ReferID = referID;
                        obj.ReferKey = referKey;
                        obj.Key = strKey;
                        obj.UserID = Account.UserID.Value;
                        obj.CreatedBy = Account.UserName;
                        obj.CreatedDate = DateTime.Now;
                        model.SYS_UserSetting.Add(obj);

                        objFunc.Options = options;
                        objFunc.DefaultFunctionID = 0;
                        objFunc.ReferID = referID;
                        objFunc.ReferKey = referKey;
                        objFunc.DefaultKey = string.Empty;
                    }
                    else
                    {
                        obj.ModifiedBy = Account.UserName;
                        obj.ModifiedDate = DateTime.Now;

                        if (!string.IsNullOrEmpty(obj.Setting))
                        {
                            objFunc = Newtonsoft.Json.JsonConvert.DeserializeObject<SYSUserSettingFunction>(obj.Setting);
                            if (objFunc != null)
                            {
                                if (objFunc.Options == null)
                                {
                                    objFunc.Options = new Dictionary<string, string>();
                                }
                                foreach (var item in options)
                                {
                                    if (objFunc.Options.ContainsKey(item.Key))
                                        objFunc.Options[item.Key] = item.Value;
                                    else
                                        objFunc.Options.Add(item.Key, item.Value);
                                }
                                obj.Setting = Newtonsoft.Json.JsonConvert.SerializeObject(objFunc);
                            }
                        }
                    }

                    obj.Setting = Newtonsoft.Json.JsonConvert.SerializeObject(objFunc);
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

        public void App_UserGridSetting_Save(int referID, string referKey, SYSUserSettingFunction_Grid item)
        {
            try
            {
                using (var model = new DataEntities())
                {
                    model.EventAccount = Account; model.EventRunning = false;

                    var strKey = SYSUserSettingKey.Function.ToString();
                    var objFunc = new SYSUserSettingFunction();

                    var obj = model.SYS_UserSetting.FirstOrDefault(c => c.Key == strKey && c.UserID == Account.UserID.Value && c.ReferID == referID && c.ReferKey == referKey);
                    if (obj == null)
                    {
                        obj = new SYS_UserSetting();
                        obj.ReferID = referID;
                        obj.ReferKey = referKey;
                        obj.Key = strKey;
                        obj.UserID = Account.UserID.Value;
                        obj.CreatedBy = Account.UserName;
                        obj.CreatedDate = DateTime.Now;
                        model.SYS_UserSetting.Add(obj);

                        objFunc.Grids = new List<SYSUserSettingFunction_Grid>();
                        objFunc.Grids.Add(item);
                        objFunc.DefaultFunctionID = 0;
                        objFunc.ReferID = referID;
                        objFunc.ReferKey = referKey;
                        objFunc.DefaultKey = string.Empty;
                    }
                    else
                    {
                        obj.ModifiedBy = Account.UserName;
                        obj.ModifiedDate = DateTime.Now;

                        if (!string.IsNullOrEmpty(obj.Setting))
                        {
                            objFunc = Newtonsoft.Json.JsonConvert.DeserializeObject<SYSUserSettingFunction>(obj.Setting);
                            if (objFunc != null)
                            {
                                if (objFunc.Grids == null)
                                    objFunc.Grids = new List<SYSUserSettingFunction_Grid>();
                                var objGrid = objFunc.Grids.FirstOrDefault(c => c.Name == item.Name);
                                if (objGrid == null)
                                {
                                    objFunc.Grids.Add(item);
                                }
                                else
                                {
                                    if (item.Columns.Count > 0)
                                        objGrid.Columns = item.Columns;
                                    objGrid.FilterRowHidden = item.FilterRowHidden;
                                    objGrid.FilterType = item.FilterType;
                                }
                                obj.Setting = Newtonsoft.Json.JsonConvert.SerializeObject(objFunc);
                            }
                        }
                    }

                    obj.Setting = Newtonsoft.Json.JsonConvert.SerializeObject(objFunc);
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

        public void App_UserGridSetting_Delete(int referID, string referKey, SYSUserSettingFunction_Grid item)
        {
            try
            {
                using (var model = new DataEntities())
                {
                    model.EventAccount = Account; model.EventRunning = false;

                    var strKey = SYSUserSettingKey.Function.ToString();
                    var objFunc = new SYSUserSettingFunction();

                    var obj = model.SYS_UserSetting.FirstOrDefault(c => c.Key == strKey && c.UserID == Account.UserID.Value && c.ReferID == referID && c.ReferKey == referKey);
                    if (obj != null)
                    {
                        if (!string.IsNullOrEmpty(obj.Setting))
                        {
                            objFunc = Newtonsoft.Json.JsonConvert.DeserializeObject<SYSUserSettingFunction>(obj.Setting);
                            if (objFunc != null)
                            {
                                if (objFunc.Grids == null)
                                    objFunc.Grids = new List<SYSUserSettingFunction_Grid>();
                                var objGrid = objFunc.Grids.FirstOrDefault(c => c.Name == item.Name);
                                if (objGrid != null)
                                {
                                    objFunc.Grids.Remove(objGrid);
                                }
                                obj.ModifiedBy = Account.UserName;
                                obj.ModifiedDate = DateTime.Now;
                                obj.Setting = Newtonsoft.Json.JsonConvert.SerializeObject(objFunc);
                                model.SaveChanges();
                            }
                        }
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

        public DTOResult App_FileList(string request, CATTypeOfFileCode code, int id)
        {
            try
            {
                DTOResult result = new DTOResult();
                result.Data = new List<CATFile>();
                using (var model = new DataEntities())
                {
                    var query = model.CAT_File.Where(c => c.SYSCustomerID == Account.SYSCustomerID && c.TypeOfFileID == (int)code && c.ReferID == id && !c.IsDelete).Select(c => new CATFile
                    {
                        ID = c.ID,
                        FileName = c.FileName,
                        FileExt = c.FileExt,
                        FilePath = c.FilePath,
                        ReferID = c.ReferID,
                        TypeOfFileID = c.TypeOfFileID
                    }).ToDataSourceResult(CreateRequest(request));
                    result.Total = query.Total;
                    result.Data = query.Data as IEnumerable<CATFile>;
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

        public int App_FileSave(CATFile item)
        {
            try
            {
                using (var model = new DataEntities())
                {
                    model.EventAccount = Account; model.EventRunning = false;
                    var objType = model.CAT_TypeOfFile.FirstOrDefault(c => c.Code == item.TypeOfFileCode);
                    if (objType != null)
                    {
                        var obj = new CAT_File();
                        obj.FileName = item.FileName;
                        obj.FileExt = item.FileExt;
                        obj.FilePath = item.FilePath;
                        obj.TypeOfFileID = objType.ID;
                        obj.ReferID = item.ReferID;
                        obj.IsDelete = false;
                        obj.SYSCustomerID = Account.SYSCustomerID;
                        obj.CreatedBy = Account.UserName;
                        obj.CreatedDate = DateTime.Now;
                        model.CAT_File.Add(obj);

                        if (item.TypeOfFileCode.ToUpper() == CATTypeOfFileCode.DIPOD.ToString().ToUpper())
                        {
                            var qrMasterDN = model.OPS_DITOGroupProduct.FirstOrDefault(c => c.ID == item.ReferID);
                            if (qrMasterDN != null)
                            {
                                qrMasterDN.ModifiedBy = Account.UserName;
                                qrMasterDN.ModifiedDate = DateTime.Now;
                                qrMasterDN.HasUpload = true;
                            }
                        }
                        model.SaveChanges();


                        return obj.ID;
                    }
                    return -1;
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

        public void App_FileDelete(List<int> lstid)
        {
            try
            {
                using (var model = new DataEntities())
                {
                    model.EventAccount = Account; model.EventRunning = false;
                    if (lstid.Count > 0)
                    {
                        foreach (var id in lstid)
                        {
                            var obj = model.CAT_File.FirstOrDefault(c => c.ID == id);
                            if (obj != null)
                            {
                                model.CAT_File.Remove(obj);
                            }
                        }
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



        public List<CATComment> App_CommentList(CATTypeOfCommentCode code, int referid)
        {
            try
            {
                var result = new List<CATComment>();
                using (var model = new DataEntities())
                {
                    result = model.CAT_Comment.Where(c => c.SYSCustomerID == Account.SYSCustomerID && c.TypeOfCommentID == (int)code && c.ReferID == referid).OrderBy(c => c.Date).Select(c => new CATComment
                    {
                        ID = c.ID,
                        ReferID = c.ReferID,
                        Comment = c.Comment,
                        UserID = c.UserID,
                        Date = c.Date,
                        UserName = c.SYS_User.UserName,
                        Image = c.SYS_User.Image
                    }).ToList();
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

        public long App_CommentSave(CATComment item)
        {
            try
            {
                using (var model = new DataEntities())
                {
                    model.EventAccount = Account; model.EventRunning = false;
                    var objType = model.CAT_TypeOfComment.FirstOrDefault(c => c.Code == item.TypeOfCommentCode);
                    if (objType != null)
                    {
                        CAT_Comment obj = new CAT_Comment();
                        obj.CreatedBy = Account.UserName;
                        obj.CreatedDate = DateTime.Now;
                        obj.SYSCustomerID = Account.SYSCustomerID;
                        obj.Comment = item.Comment;
                        obj.UserID = Account.UserID.HasValue ? Account.UserID.Value : 0;
                        obj.ReferID = item.ReferID;
                        obj.TypeOfCommentID = objType.ID;
                        obj.Date = DateTime.Now;
                        model.CAT_Comment.Add(obj);
                        model.SaveChanges();

                        obj.ModifiedBy = Account.UserName;
                        obj.ModifiedDate = DateTime.Now;
                        model.SaveChanges();
                        return obj.ID;
                    }
                    return -1;
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



        public SYSUserSettingFunction App_UserSettingGet(SYSUserSettingFunction item)
        {
            try
            {
                using (var model = new DataEntities())
                {
                    return HelperSYSSetting.SYSUserSettingFunction_Get(model, Account, item);
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

        public void App_UserSettingSave(SYSUserSettingFunction item)
        {
            try
            {
                using (var model = new DataEntities())
                {
                    model.EventAccount = Account; model.EventRunning = false;
                    HelperSYSSetting.SYSUserSettingFunction_Save(model, Account, item);
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


        public void App_PriceMaterial(DTOTriggerMaterial item)
        {
            try
            {
                using (var model = new DataEntities())
                {
                    model.EventAccount = Account; model.EventRunning = false;

                    if (item.DieselArea1_MaterialID > 0 && item.DieselArea1 > 0)
                        App_PriceMaterial_CheckContract(model, item.DieselArea1_MaterialID.Value, item.DieselArea1);
                    if (item.DieselArea2_MaterialID > 0 && item.DieselArea2 > 0)
                        App_PriceMaterial_CheckContract(model, item.DieselArea2_MaterialID.Value, item.DieselArea2);
                    if (item.DO05Area1_MaterialID > 0 && item.DO05Area1 > 0)
                        App_PriceMaterial_CheckContract(model, item.DO05Area1_MaterialID.Value, item.DO05Area1);
                    if (item.DO05Area2_MaterialID > 0 && item.DO05Area2 > 0)
                        App_PriceMaterial_CheckContract(model, item.DO05Area2_MaterialID.Value, item.DO05Area2);
                    if (item.DO25Area1_MaterialID > 0 && item.DO25Area1 > 0)
                        App_PriceMaterial_CheckContract(model, item.DO25Area1_MaterialID.Value, item.DO25Area1);
                    if (item.DO25Area2_MaterialID > 0 && item.DO25Area2 > 0)
                        App_PriceMaterial_CheckContract(model, item.DO25Area2_MaterialID.Value, item.DO25Area2);
                    if (item.E5RON92Area1_MaterialID > 0 && item.E5RON92Area1 > 0)
                        App_PriceMaterial_CheckContract(model, item.E5RON92Area1_MaterialID.Value, item.E5RON92Area1);
                    if (item.E5RON92Area2_MaterialID > 0 && item.E5RON92Area2 > 0)
                        App_PriceMaterial_CheckContract(model, item.E5RON92Area2_MaterialID.Value, item.E5RON92Area2);

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

        private void App_PriceMaterial_CheckContract(DataEntities model, int id, decimal price)
        {
            foreach (var item in model.CAT_ContractTerm.Where(c => c.MaterialID == id && c.PriceContract > 0))
            {
                item.PriceCurrent = price;
                item.ModifiedBy = Account.UserName;
                item.ModifiedDate = DateTime.Now;
                if (!string.IsNullOrEmpty(item.ExprInput))
                {
                    DTOMaterialChecking itemCheck = new DTOMaterialChecking();
                    itemCheck.PriceContract = item.PriceContract.Value;
                    itemCheck.PriceCurrent = item.PriceCurrent > 0 ? item.PriceCurrent.Value : 0;
                    bool flag = false;
                    try
                    {
                        flag = App_PriceMaterial_CheckBool(itemCheck, item.ExprInput);
                    }
                    catch { flag = false; }
                    if (flag)
                    {
                        item.PriceWarning = price;
                        item.IsWarning = true;
                    }
                }
            }
        }

        private bool App_PriceMaterial_CheckBool(DTOMaterialChecking item, string strExp)
        {
            bool result = false;
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

                //worksheet.Cells[row, col].Value = item.PriceFix;
                //strExp = strExp.Replace("[PriceFix]", strCol + row);
                //strRow = strCol + row; row++;

                //worksheet.Cells[row, col].Value = item.Percent;
                //strExp = strExp.Replace("[Percent]", strCol + row);
                //strRow = strCol + row; row++;

                worksheet.Cells[row, col].Formula = strExp;

                package.Workbook.Calculate();
                var val = worksheet.Cells[row, col].Value.ToString().Trim();

                if (val == "True") result = true;
                else if (val == "False") result = false;

                return result;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Thiết lập sai công thức: " + strExp);
                return result;
            }
        }



        public List<DTOCATLocation> App_LocationList()
        {
            try
            {
                var result = new List<DTOCATLocation>();
                using (var model = new DataEntities())
                {
                    model.EventAccount = Account; model.EventRunning = false;

                    result = model.CAT_Location.Where(c => !(c.Lat > 0 && c.Lng > 0)).Select(c => new DTOCATLocation
                    {
                        ID = c.ID,
                        Code = c.Code,
                        Address = c.Address,
                        ProvinceName = c.CAT_Province.ProvinceName,
                        DistrictName = c.CAT_District.DistrictName
                    }).ToList();
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

        public void App_LocationSave(List<DTOCATLocation> lst)
        {
            try
            {
                using (var model = new DataEntities())
                {
                    model.EventAccount = Account; model.EventRunning = false;

                    if (lst.Count > 0)
                    {
                        foreach (var item in lst.Where(c => c.Lat > 0 && c.Lng > 0))
                        {
                            var obj = model.CAT_Location.FirstOrDefault(c => c.ID == item.ID);
                            if (obj != null)
                            {
                                obj.Lat = item.Lat;
                                obj.Lng = item.Lng;
                            }
                        }
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

        public List<CATLocationMatrix> App_LocationMatrixList()
        {
            try
            {
                var result = new List<CATLocationMatrix>();
                using (var model = new DataEntities())
                {
                    model.EventAccount = Account; model.EventRunning = false;

                    result = model.CAT_LocationMatrix.Where(c => c.IsChecked == false).Select(c => new CATLocationMatrix
                    {
                        ID = c.ID,
                        LocationFromID = c.LocationFromID,
                        LocationFromLat = c.CAT_Location.Lat,
                        LocationFromLng = c.CAT_Location.Lng,
                        LocationToID = c.LocationToID,
                        LocationToLat = c.CAT_Location1.Lat,
                        LocationToLng = c.CAT_Location1.Lng,
                        KM = c.KM,
                        Hour = c.Hour
                    }).ToList();
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

        public void App_LocationMatrixSave(List<CATLocationMatrix> lst)
        {
            try
            {
                using (var model = new DataEntities())
                {
                    model.EventAccount = Account; model.EventRunning = false;

                    if (lst.Count > 0)
                    {
                        foreach (var item in lst.Where(c => c.Hour > 0 && c.KM > 0))
                        {
                            var obj = model.CAT_LocationMatrix.FirstOrDefault(c => c.ID == item.ID);
                            if (obj != null)
                            {
                                obj.IsChecked = true;
                                obj.Hour = item.Hour;
                                obj.KM = item.KM;
                                obj.Instructions = item.Instructions;
                            }
                        }
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

        #endregion

        #region Action
        public DTOResult SYSAction_List(Kendo.Mvc.UI.DataSourceRequest request)
        {
            try
            {
                DTOResult result = new DTOResult();
                using (var model = new DataEntities())
                {
                    var query = model.SYS_Action.Select(c => new SYSAction
                    {
                        ID = c.ID,
                        Code = c.Code,
                        ActionName = c.ActionName,
                        IsApproved = c.IsApproved
                    }).ToDataSourceResult(request);
                    result.Total = query.Total;
                    result.Data = query.Data as IEnumerable<SYSAction>;
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

        public int SYSAction_Save(SYSAction item)
        {
            try
            {
                DTOError result = new DTOError();
                using (var model = new DataEntities())
                {
                    model.EventAccount = Account; model.EventRunning = false;
                    if (model.SYS_Action.Where(c => c.ID != item.ID && c.Code == item.Code).Count() > 0)
                        throw FaultHelper.BusinessFault(DTOErrorString.DAT_Duplicate, DTOErrorMember.Code.ToString());
                    else
                    {
                        var obj = model.SYS_Action.FirstOrDefault(c => c.ID == item.ID);
                        if (obj == null)
                        {
                            obj = new SYS_Action();
                            obj.CreatedBy = Account.UserName;
                            obj.CreatedDate = DateTime.Now;
                        }
                        else
                        {
                            obj.ModifiedBy = Account.UserName;
                            obj.ModifiedDate = DateTime.Now;
                        }
                        obj.Code = item.Code;
                        obj.ActionName = item.ActionName;
                        obj.IsApproved = item.IsApproved;
                        if (obj.ID < 1)
                            model.SYS_Action.Add(obj);
                        model.SaveChanges();
                        return obj.ID;
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

        public void SYSAction_Delete(SYSAction item)
        {
            try
            {
                using (var model = new DataEntities())
                {
                    model.EventAccount = Account; model.EventRunning = false;
                    var obj = model.SYS_Action.FirstOrDefault(c => c.ID == item.ID);
                    if (obj != null)
                    {
                        if (model.SYS_ActionInFunction.Where(c => c.ActionID == item.ID).Count() > 0)
                            throw FaultHelper.BusinessFault(DTOErrorString.DAT_HasChild, DTOErrorMember.SYS_Function.ToString());
                        else if (model.SYS_FunctionInGroup.Where(c => c.ActionID == item.ID).Count() > 0)
                            throw FaultHelper.BusinessFault(DTOErrorString.DAT_HasChild, DTOErrorMember.SYS_FunctionInGroup.ToString());
                        else
                        {
                            model.SYS_Action.Remove(obj);
                            model.SaveChanges();
                        }
                    }
                    else
                        throw FaultHelper.BusinessFault(DTOErrorString.DAT_NoExists);
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

        #region Group
        public DTOResult SYSGroup_Read(string request)
        {
            try
            {
                DTOResult result = new DTOResult();
                using (var model = new DataEntities())
                {
                    var query = model.SYS_Group.Where(c => c.ParentID == null).Select(c => new SYSGroup
                    {
                        ID = c.ID,
                        ParentID = c.ParentID,
                        Code = c.Code,
                        GroupName = c.GroupName,
                        Description = c.Description,
                        SortOrder = c.SortOrder,
                        IsApproved = c.IsApproved
                    }).ToDataSourceResult(CreateRequest(request));
                    result.Total = query.Total;
                    result.Data = query.Data as IEnumerable<SYSGroup>;
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

        public SYSGroup SYSGroup_Item(int id)
        {
            var result = new SYSGroup { ID = -1, ListCustomer = new List<CUSCustomer>() };
            try
            {
                using (var model = new DataEntities())
                {
                    if (id > 0)
                    {
                        result = model.SYS_Group.Where(c => c.ID == id).Select(c => new SYSGroup
                        {
                            ID = c.ID,
                            Code = c.Code,
                            GroupName = c.GroupName,
                            Description = c.Description,
                            IsApproved = c.IsApproved,
                            SortOrder = c.SortOrder,
                            ListCustomerID = c.ListCustomerID
                        }).FirstOrDefault();
                        if (result != null)
                        {
                            result.ListCustomer = model.CUS_Customer.Where(c => c.IsSystem == false).Select(c => new CUSCustomer
                            {
                                ID = c.ID,
                                Code = c.Code,
                                CustomerName = c.CustomerName,
                                TypeOfCustomerCode = c.SYS_Var.Code,
                                TypeOfCustomerName = c.SYS_Var.ValueOfVar
                            }).ToList();
                        }
                    }
                    else
                    {
                        result.ListCustomer = model.CUS_Customer.Where(c => c.IsSystem == false).Select(c => new CUSCustomer
                        {
                            ID = c.ID,
                            Code = c.Code,
                            CustomerName = c.CustomerName,
                            TypeOfCustomerCode = c.SYS_Var.Code,
                            TypeOfCustomerName = c.SYS_Var.ValueOfVar
                        }).ToList();
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

        public int SYSGroup_Save(SYSGroup item)
        {
            try
            {
                using (var model = new DataEntities())
                {
                    model.EventAccount = Account; model.EventRunning = false;
                    if (model.SYS_Group.Where(c => c.ID != item.ID && c.Code == item.Code).Count() > 0)
                        throw FaultHelper.BusinessFault(null, null, "Mã đã sử dụng");

                    var obj = model.SYS_Group.FirstOrDefault(c => c.ID == item.ID);
                    if (obj == null)
                    {
                        obj = new SYS_Group();
                        obj.CreatedBy = Account.UserName;
                        obj.CreatedDate = DateTime.Now;
                        obj.ParentID = null;
                        obj.SortOrder = 0;
                        model.SYS_Group.Add(obj);
                    }
                    else
                    {
                        obj.ModifiedBy = Account.UserName;
                        obj.ModifiedDate = DateTime.Now;
                    }
                    obj.Code = item.Code;
                    obj.GroupName = item.GroupName;
                    obj.Description = item.Description;
                    obj.IsApproved = item.IsApproved;
                    obj.ListCustomerID = item.ListCustomerID;
                    model.SaveChanges();

                    var result = new List<DTOCombobox>();

                    // Lưu cho các group con
                    var lstData = model.SYS_Group.Select(c => new SYSGroup
                    {
                        ID = c.ID,
                        ParentID = c.ParentID,
                        Code = c.Code,
                        GroupName = c.GroupName
                    }).ToList();

                    SYSUserChild_Group_Child(ref result, lstData, obj.ID, 0);

                    var lstID = result.Where(c => c.ValueInt > 0).Select(c => c.ValueInt).Distinct().ToList();
                    foreach (var id in lstID)
                    {
                        var objChild = model.SYS_Group.FirstOrDefault(c => c.ID == id);
                        if (objChild != null)
                        {
                            objChild.ModifiedBy = Account.UserName;
                            objChild.ModifiedDate = DateTime.Now;
                            objChild.ListCustomerID = obj.ListCustomerID;
                        }
                    }
                    model.SaveChanges();

                    return obj.ID;
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

        public void SYSGroup_Delete(List<int> lstid)
        {
            try
            {
                using (var model = new DataEntities())
                {
                    model.EventAccount = Account; model.EventRunning = false;
                    foreach (var id in lstid)
                    {
                        var obj = model.SYS_Group.FirstOrDefault(c => c.ID == id);
                        if (obj != null)
                        {
                            if (model.SYS_Group.Where(c => c.ParentID == id).Count() > 0)
                                throw FaultHelper.BusinessFault(DTOErrorString.DAT_HasChild, DTOErrorMember.SYS_Group.ToString());
                            else
                            {
                                foreach (var item in model.SYS_User.Where(c => c.GroupID == obj.ID))
                                {
                                    item.GroupID = null;
                                    item.ModifiedBy = Account.UserName;
                                    item.ModifiedDate = DateTime.Now;
                                }

                                foreach (var detail in model.SYS_FunctionInGroup.Where(c => c.GroupID == id))
                                    model.SYS_FunctionInGroup.Remove(detail);
                                model.SYS_Group.Remove(obj);
                            }
                        }
                    }
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

        #region GroupChild
        public DTOResult SYSGroupChild_Read(string request)
        {
            try
            {
                DTOResult result = new DTOResult();
                using (var model = new DataEntities())
                {
                    var lstGroup = SYSUserChild_Group();
                    var lstChildID = lstGroup.Where(c => c.ValueInt > 0).Select(c => c.ValueInt).Distinct().ToList();
                    var query = model.SYS_Group.Where(c => lstChildID.Contains(c.ID)).Select(c => new SYSGroup
                    {
                        ID = c.ID,
                        ParentID = c.ParentID,
                        Code = c.Code,
                        GroupName = c.GroupName,
                        Description = c.Description,
                        SortOrder = c.SortOrder,
                        IsApproved = c.IsApproved
                    }).ToDataSourceResult(CreateRequest(request));
                    result.Total = query.Total;
                    result.Data = query.Data as IEnumerable<SYSGroup>;
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

        public List<DTOCombobox> SYSGroupChild_Parent(int id)
        {
            var result = new List<DTOCombobox>();
            try
            {
                using (var model = new DataEntities())
                {
                    result = SYSUserChild_Group();
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

        public SYSGroup SYSGroupChild_Item(int id)
        {
            var result = new SYSGroup { ParentID = -1 };
            try
            {
                using (var model = new DataEntities())
                {
                    if (id > 0)
                    {
                        result = model.SYS_Group.Where(c => c.ID == id).Select(c => new SYSGroup
                        {
                            ID = c.ID,
                            ParentID = c.ParentID > 0 ? c.ParentID.Value : -1,
                            Code = c.Code,
                            GroupName = c.GroupName,
                            Description = c.Description,
                            IsApproved = c.IsApproved,
                            SortOrder = c.SortOrder,
                        }).FirstOrDefault();
                    }
                    else
                        result.ParentID = Account.GroupID;
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int SYSGroupChild_Save(SYSGroup item)
        {
            try
            {
                DTOError result = new DTOError();
                using (var model = new DataEntities())
                {
                    model.EventAccount = Account; model.EventRunning = false;

                    var parent = model.SYS_Group.FirstOrDefault(c => c.ID == item.ParentID);
                    if (parent == null)
                        throw FaultHelper.BusinessFault(null, null, "Vui lòng chọn nhóm vai trò");

                    if (model.SYS_Group.Where(c => c.ID != item.ID && c.Code == item.Code).Count() > 0)
                        throw FaultHelper.BusinessFault(DTOErrorString.DAT_Duplicate, DTOErrorMember.Code.ToString());
                    else
                    {
                        var obj = model.SYS_Group.FirstOrDefault(c => c.ID == item.ID);
                        if (obj == null)
                        {
                            obj = new SYS_Group();
                            obj.CreatedBy = Account.UserName;
                            obj.CreatedDate = DateTime.Now;
                        }
                        else
                        {
                            obj.ModifiedBy = Account.UserName;
                            obj.ModifiedDate = DateTime.Now;
                        }
                        obj.ParentID = item.ParentID;
                        obj.Code = item.Code;
                        obj.GroupName = item.GroupName;
                        obj.Description = item.Description;
                        obj.SortOrder = item.SortOrder;
                        obj.IsApproved = item.IsApproved;
                        obj.ListCustomerID = parent.ListCustomerID;
                        if (obj.ID < 1)
                            model.SYS_Group.Add(obj);
                        model.SaveChanges();
                        return obj.ID;
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

        public void SYSGroupChild_Delete(List<int> lstid)
        {
            try
            {
                using (var model = new DataEntities())
                {
                    model.EventAccount = Account; model.EventRunning = false;
                    foreach (var id in lstid)
                    {
                        var obj = model.SYS_Group.FirstOrDefault(c => c.ID == id);
                        if (obj != null)
                        {
                            if (model.SYS_Group.Where(c => c.ParentID == id).Count() > 0)
                                throw FaultHelper.BusinessFault(DTOErrorString.DAT_HasChild, DTOErrorMember.SYS_Group.ToString());
                            else
                            {
                                foreach (var item in model.SYS_User.Where(c => c.GroupID == obj.ID))
                                {
                                    item.GroupID = null;
                                    item.ModifiedBy = Account.UserName;
                                    item.ModifiedDate = DateTime.Now;
                                }

                                foreach (var detail in model.SYS_FunctionInGroup.Where(c => c.GroupID == id))
                                    model.SYS_FunctionInGroup.Remove(detail);
                                model.SYS_Group.Remove(obj);
                            }
                        }
                    }
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

        #region Function
        public DTOResult SYSFunction_Read(string request)
        {
            try
            {
                DTOResult result = new DTOResult();
                using (var model = new DataEntities())
                {
                    var query = model.SYS_Function.OrderBy(c => c.SortOrder).Select(c => new SYSFunction
                    {
                        ID = c.ID,
                        ParentID = c.ParentID,
                        Code = c.Code,
                        FunctionName = c.FunctionName,
                        Description = c.Description,
                        Icon = c.Icon,
                        IsAdmin = c.IsAdmin,
                        IsApproved = c.IsApproved,
                        IsCustomer = c.IsCustomer,
                        SortOrder = c.SortOrder,
                        Level = c.Level
                    }).ToDataSourceResult(CreateRequest(request));
                    result.Total = query.Total;
                    result.Data = query.Data as IEnumerable<SYSFunction>;
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

        public List<DTOCombobox> SYSFunction_Parent(int id)
        {
            var result = new List<DTOCombobox>();
            try
            {
                using (var model = new DataEntities())
                {
                    var obj = model.SYS_Function.Where(c => c.ID == id).Select(c => new { c.SortOrder, c.Level }).FirstOrDefault();
                    if (obj != null)
                    {
                        var next = model.SYS_Function.Where(c => c.Level == obj.Level && c.SortOrder > obj.SortOrder).Select(c => new { c.SortOrder, c.Level }).FirstOrDefault();
                        if (next != null)
                        {
                            var lst = model.SYS_Function.Where(c => c.SortOrder < obj.SortOrder || c.SortOrder >= next.SortOrder).OrderBy(c => c.SortOrder).Select(c => new { c.ID, c.Code, c.FunctionName, c.Level }).ToList();
                            foreach (var item in lst)
                            {
                                result.Add(new DTOCombobox
                                {
                                    ValueInt = item.ID,
                                    ValueString = item.ID + "",
                                    Text = new string('.', item.Level * 3) + item.FunctionName
                                });
                            }
                        }
                    }

                    result.Insert(0, new DTOCombobox { ValueInt = -1, ValueString = "-1", Text = "" });
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

        public SYSFunction SYSFunction_Item(int id)
        {
            var result = new SYSFunction { ParentID = -1 };
            try
            {
                using (var model = new DataEntities())
                {
                    if (id > 0)
                    {
                        result = model.SYS_Function.Where(c => c.ID == id).Select(c => new SYSFunction
                        {
                            ID = c.ID,
                            ParentID = c.ParentID > 0 ? c.ParentID.Value : -1,
                            Code = c.Code,
                            FunctionName = c.FunctionName,
                            Description = c.Description,
                            Icon = c.Icon,
                            IsAdmin = c.IsAdmin,
                            IsApproved = c.IsApproved,
                            IsCustomer = c.IsCustomer,
                            SortOrder = c.SortOrder,
                            Level = c.Level
                        }).FirstOrDefault();

                        if (result != null)
                        {
                            result.ListActions = model.SYS_Action.Select(c => new SYSAction
                            {
                                ID = c.ID,
                                Code = c.Code,
                                ActionName = c.ActionName,
                                IsView = c.IsView,
                                IsApproved = c.IsApproved,
                                IsChoose = false
                            }).ToList();

                            var lstChoose = model.SYS_ActionInFunction.Where(c => c.FunctionID == id).Select(c => c.ActionID).ToArray();
                            foreach (var item in result.ListActions)
                            {
                                if (lstChoose.Contains(item.ID))
                                    item.IsChoose = true;
                            }
                        }
                    }
                    else
                    {
                        result.ListActions = model.SYS_Action.Select(c => new SYSAction
                        {
                            ID = c.ID,
                            Code = c.Code,
                            ActionName = c.ActionName,
                            IsView = c.IsView,
                            IsApproved = c.IsApproved,
                            IsChoose = false
                        }).ToList();
                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void SYSFunction_Move(int id, int typeid)
        {
            try
            {
                using (var model = new DataEntities())
                {
                    model.EventAccount = Account; model.EventRunning = false;
                    if (id > 0 && typeid != 0)
                    {
                        var objParent = model.SYS_Function.Where(c => c.ParentID > 0 && c.ID == id).Select(c => new { ID = c.ParentID.Value }).FirstOrDefault();
                        if (objParent != null)
                        {
                            var objFirst = model.SYS_Function.Where(c => c.ParentID == objParent.ID).OrderBy(c => c.SortOrder).Select(c => new { c.ID, c.SortOrder }).FirstOrDefault();
                            var objLast = model.SYS_Function.Where(c => c.ParentID == objParent.ID).OrderByDescending(c => c.SortOrder).Select(c => new { c.ID, c.SortOrder }).FirstOrDefault();
                            var obj = model.SYS_Function.FirstOrDefault(c => c.ID == id);
                            switch (typeid)
                            {
                                case 1://down
                                    if (obj.ID != objLast.ID)
                                    {
                                        var objNext = model.SYS_Function.Where(c => c.ParentID == objParent.ID && c.SortOrder >= obj.SortOrder && c.ID != obj.ID).OrderBy(c => c.SortOrder).FirstOrDefault();
                                        if (objNext != null)
                                        {
                                            long sort = obj.SortOrder;
                                            obj.SortOrder = objNext.SortOrder;
                                            objNext.SortOrder = sort;
                                            model.SaveChanges();
                                        }
                                    }
                                    break;
                                case 2://last
                                    if (obj.ID != objLast.ID)
                                    {
                                        var objNext = model.SYS_Function.Where(c => c.ID == objLast.ID).FirstOrDefault();
                                        if (objNext != null)
                                        {
                                            long sort = obj.SortOrder;
                                            obj.SortOrder = objNext.SortOrder;
                                            objNext.SortOrder = sort;
                                            model.SaveChanges();
                                        }
                                    }
                                    break;
                                case -1://up
                                    if (obj.ID != objFirst.ID)
                                    {
                                        var objNext = model.SYS_Function.Where(c => c.ParentID == objParent.ID && c.SortOrder <= obj.SortOrder && c.ID != obj.ID).OrderByDescending(c => c.SortOrder).FirstOrDefault();
                                        if (objNext != null)
                                        {
                                            long sort = obj.SortOrder;
                                            obj.SortOrder = objNext.SortOrder;
                                            objNext.SortOrder = sort;
                                            model.SaveChanges();
                                        }
                                    }
                                    break;
                                case -2://first
                                    if (obj.ID != objFirst.ID)
                                    {
                                        var objNext = model.SYS_Function.Where(c => c.ID == objFirst.ID).FirstOrDefault();
                                        if (objNext != null)
                                        {
                                            long sort = obj.SortOrder;
                                            obj.SortOrder = objNext.SortOrder;
                                            objNext.SortOrder = sort;
                                            model.SaveChanges();
                                        }
                                    }
                                    break;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int SYSFunction_Save(SYSFunction item)
        {
            try
            {
                using (var model = new DataEntities())
                {
                    //model.EventAccount = Account; model.EventRunning = false;

                    if (model.SYS_Function.Where(c => c.ID != item.ID && c.Code == item.Code).Count() > 0)
                        throw FaultHelper.BusinessFault(DTOErrorString.DAT_Duplicate, DTOErrorMember.Code.ToString());
                    else
                    {
                        var obj = model.SYS_Function.FirstOrDefault(c => c.ID == item.ID);
                        if (obj == null)
                        {
                            obj = new SYS_Function();
                            obj.CreatedBy = Account.UserName;
                            obj.CreatedDate = DateTime.Now;
                            obj.SortOrder = -1;
                        }
                        else
                        {
                            obj.ModifiedBy = Account.UserName;
                            obj.ModifiedDate = DateTime.Now;
                        }
                        if (item.ParentID > 0)
                            obj.ParentID = item.ParentID;
                        else
                            obj.ParentID = null;
                        var objParent = model.SYS_Function.Where(c => c.ID == item.ParentID).Select(c => new { c.Level }).FirstOrDefault();
                        if (objParent != null)
                            obj.Level = objParent.Level + 1;
                        obj.Code = item.Code;
                        obj.FunctionName = item.FunctionName;
                        obj.Description = item.Description;
                        obj.Icon = item.Icon;
                        obj.IsAdmin = item.IsAdmin;
                        obj.IsApproved = item.IsApproved;
                        obj.IsCustomer = item.IsCustomer;
                        if (obj.ID < 1)
                            model.SYS_Function.Add(obj);
                        model.SaveChanges();

                        foreach (var detail in model.SYS_ActionInFunction.Where(c => c.FunctionID == obj.ID))
                            model.SYS_ActionInFunction.Remove(detail);
                        if (item.ListActions != null && item.ListActions.Count > 0)
                        {
                            foreach (var detail in item.ListActions)
                            {
                                var objDetail = new SYS_ActionInFunction();
                                objDetail.ActionID = detail.ID;
                                objDetail.FunctionID = obj.ID;
                                objDetail.CreatedBy = Account.UserName;
                                objDetail.CreatedDate = DateTime.Now;
                                model.SYS_ActionInFunction.Add(objDetail);
                            }
                            model.SaveChanges();
                        }

                        return obj.ID;
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

        public void SYSFunction_Delete(List<int> lstid)
        {
            try
            {
                using (var model = new DataEntities())
                {
                    model.EventAccount = Account; model.EventRunning = false;
                    if (lstid.Count > 0)
                    {
                        foreach (var item in lstid)
                        {
                            var obj = model.SYS_Function.FirstOrDefault(c => c.ID == item);
                            if (obj != null)
                            {
                                foreach (var detail in model.SYS_FunctionInGroup.Where(c => c.FunctionID == item))
                                    model.SYS_FunctionInGroup.Remove(detail);
                                foreach (var detail in model.SYS_ActionInFunction.Where(c => c.FunctionID == item))
                                    model.SYS_ActionInFunction.Remove(detail);
                                model.SYS_Function.Remove(obj);
                            }
                        }
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

        public void SYSFunction_Refresh()
        {
            try
            {
                using (var model = new DataEntities())
                {
                    model.EventAccount = Account; model.EventRunning = false;
                    var sort = 0;
                    foreach (var item in model.SYS_Function.Where(c => c.ParentID == null).OrderBy(c => c.SortOrder))
                    {
                        item.Level = 1;
                        int countPow = (int)Math.Pow(10, FunctionCount + 1);
                        item.SortOrder = (10 + sort++) * (int)Math.Pow(countPow, FunctionMaxLevel - item.Level);
                    }
                    model.SaveChanges();

                    var lstParent = model.SYS_Function.Where(c => c.ParentID == null).Select(c => new { c.ID, c.SortOrder, c.Level }).ToList();
                    var lstChild = model.SYS_Function.Where(c => c.ParentID > 0 && c.Level == 2).Select(c => new { c.ParentID, c.ID, c.SortOrder }).OrderBy(c => c.SortOrder).ToList();
                    foreach (var item in lstParent)
                    {
                        int countPow = (int)Math.Pow(10, FunctionCount + 1);
                        int countSort = (int)Math.Pow(10, FunctionCount);
                        int div = (int)Math.Pow(countPow, FunctionMaxLevel - item.Level - 1);
                        long divsort = item.SortOrder / div;
                        foreach (var child in lstChild.Where(c => c.ParentID == item.ID).Select(c => new { c.ID, c.SortOrder }).OrderBy(c => c.SortOrder))
                        {
                            SYSFunction_RefreshSort(model, child.ID, divsort + countSort, item.Level + 1);
                            countSort++;
                        }
                    }
                    model.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        const int FunctionMaxLevel = 5;
        const int FunctionCount = 1;
        private void SYSFunction_RefreshSort(DataEntities model, int id, long divsort, int level)
        {
            int countPow = (int)Math.Pow(10, FunctionCount + 1);
            int countSort = (int)Math.Pow(10, FunctionCount);
            var obj = model.SYS_Function.FirstOrDefault(c => c.ID == id);
            obj.Level = level;
            obj.SortOrder = divsort * (int)Math.Pow(countPow, FunctionMaxLevel - level);
            divsort = divsort * countPow;
            var lstChilds = model.SYS_Function.Where(c => c.ParentID == obj.ID).Select(c => new { c.ID, c.SortOrder }).OrderBy(c => c.SortOrder).ToList();
            foreach (var child in lstChilds)
            {
                SYSFunction_RefreshSort(model, child.ID, divsort + countSort, level + 1);
                countSort++;
            }
        }

        public SYSFunction_Export SYSFunction_Export()
        {
            try
            {
                var result = new SYSFunction_Export();
                using (var model = new DataEntities())
                {
                    result.ListFunction = model.SYS_Function.Select(c => new SYSFunction
                    {
                        ID = c.ID,
                        ParentID = c.ParentID,
                        Code = c.Code,
                        FunctionName = c.FunctionName,
                        Level = c.Level,
                        SortOrder = c.SortOrder,
                        Icon = c.Icon,
                        Description = c.Description,
                        ListActions = c.SYS_ActionInFunction.Select(d => new SYSAction { ID = d.ActionID }).ToList()
                    }).ToList();
                    result.ListAction = model.SYS_Action.Where(c => c.IsApproved).Select(c => new SYSAction
                    {
                        ID = c.ID,
                        Code = c.Code,
                        ActionName = c.ActionName,
                        IsView = c.IsView
                    }).ToList();
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

        public SYSFunction_ImportCheck SYSFunction_ImportCheck()
        {
            try
            {
                var result = new SYSFunction_ImportCheck();
                using (var model = new DataEntities())
                {
                    result.ListFunction = model.SYS_Function.Select(c => new SYSFunction
                    {
                        ID = c.ID,
                        ParentID = c.ParentID,
                        Code = c.Code,
                        FunctionName = c.FunctionName,
                        Level = c.Level,
                        SortOrder = c.SortOrder,
                        Icon = c.Icon,
                        Description = c.Description
                    }).ToList();
                    result.ListAction = model.SYS_Action.Where(c => c.IsApproved).Select(c => new SYSAction
                    {
                        ID = c.ID,
                        Code = c.Code,
                        ActionName = c.ActionName,
                        IsView = c.IsView
                    }).ToList();
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

        public void SYSFunction_ExcelImport(List<SYSFunction_Import> lst)
        {
            try
            {
                using (var model = new DataEntities())
                {
                    model.EventAccount = Account; model.EventRunning = false;
                    foreach (var item in lst.Where(c => c.ExcelSuccess))
                    {
                        var obj = model.SYS_Function.FirstOrDefault(c => c.ID == item.ID);
                        if (obj == null)
                        {
                            obj = new SYS_Function();
                            obj.CreatedBy = Account.UserName;
                            obj.CreatedDate = DateTime.Now;
                            obj.IsAdmin = obj.IsCustomer = false;
                            obj.IsApproved = true;
                        }
                        else
                        {
                            obj.ModifiedBy = Account.UserName;
                            obj.ModifiedDate = DateTime.Now;
                        }
                        obj.Code = item.Code;
                        obj.FunctionName = item.FunctionName;
                        obj.Description = item.Description;
                        obj.Icon = item.Icon;
                        //obj.Level = item.Level;
                        //obj.SortOrder = item.SortOrder;
                        //if (item.ParentID > 0)
                        //    obj.ParentID = item.ParentID.Value;
                        //else
                        //    obj.ParentID = null;
                        if (obj.ID < 1)
                            model.SYS_Function.Add(obj);
                        model.SaveChanges();


                        //foreach (var act in model.SYS_ActionInFunction.Where(c => c.FunctionID == obj.ID))
                        //    model.SYS_ActionInFunction.Remove(act);
                        //if (item.ListActions != null)
                        //{
                        //    foreach (var act in item.ListActions)
                        //    {
                        //        var objAction = new SYS_ActionInFunction();
                        //        objAction.CreatedBy = Account.UserName;
                        //        objAction.CreatedDate = DateTime.Now;
                        //        objAction.FunctionID = obj.ID;
                        //        objAction.ActionID = act.ID;
                        //        model.SYS_ActionInFunction.Add(objAction);
                        //    }
                        //    model.SaveChanges();
                        //}
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
        #endregion

        #region Config
        public DTOResult SYSConfigGroup_Read(string request)
        {
            try
            {
                DTOResult result = new DTOResult();
                using (var model = new DataEntities())
                {
                    var query = model.SYS_Group.Where(c => c.IsApproved).Select(c => new SYSGroup
                    {
                        ID = c.ID,
                        ParentID = c.ParentID,
                        Code = c.Code,
                        GroupName = c.GroupName,
                        Description = c.Description
                    }).ToDataSourceResult(CreateRequest(request));
                    result.Total = query.Total;
                    result.Data = query.Data as IEnumerable<SYSGroup>;
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

        public SYSGroup SYSConfigGroup_GroupItem(int groupid)
        {
            var result = default(SYSGroup);
            using (var model = new DataEntities())
            {
                result = model.SYS_Group.Where(c => c.ID == groupid).Select(c => new SYSGroup
                {
                    ID = c.ID,
                    Code = c.Code,
                    GroupName = c.GroupName
                }).FirstOrDefault();
            }
            return result;
        }

        public DTOResult SYSConfigFunction_InRead(string request, int groupid)
        {
            try
            {
                DTOResult result = new DTOResult();
                using (var model = new DataEntities())
                {
                    //foreach (var item in model.SYS_FunctionInGroup.Where(c => c.GroupID == groupid))
                    //{
                    //    model.SYS_FunctionInGroup.Remove(item);
                    //}
                    //model.SaveChanges();

                    var query = model.SYS_FunctionInGroup.Where(c => c.GroupID == groupid && c.ActionID == null).Select(c => new SYSFunction
                    {
                        ID = c.FunctionID,
                        ParentID = c.SYS_Function.ParentID,
                        Code = c.SYS_Function.Code,
                        FunctionName = c.SYS_Function.FunctionName,
                        Description = c.SYS_Function.Description,
                        Icon = c.SYS_Function.Icon,
                        IsAdmin = c.SYS_Function.IsAdmin,
                        IsApproved = c.SYS_Function.IsApproved,
                        IsCustomer = c.SYS_Function.IsCustomer,
                        SortOrder = c.SYS_Function.SortOrder,
                        Level = c.SYS_Function.Level
                    }).ToDataSourceResult(CreateRequest(request));
                    result.Total = query.Total;
                    result.Data = query.Data as IEnumerable<SYSFunction>;
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

        public DTOResult SYSConfigFunction_NotInRead(string request, int groupid)
        {
            try
            {
                DTOResult result = new DTOResult();
                using (var model = new DataEntities())
                {
                    var query = model.SYS_Function.Select(c => new SYSFunction
                    {
                        ID = c.ID,
                        ParentID = c.ParentID,
                        Code = c.Code,
                        FunctionName = c.FunctionName,
                        Description = c.Description,
                        Icon = c.Icon,
                        IsAdmin = c.IsAdmin,
                        IsApproved = c.IsApproved,
                        IsCustomer = c.IsCustomer,
                        SortOrder = c.SortOrder,
                        Level = c.Level
                    }).ToDataSourceResult(CreateRequest(request));
                    var lst = query.Data.Cast<SYSFunction>().ToList();
                    var lstIn = model.SYS_FunctionInGroup.Where(c => c.GroupID == groupid && c.ActionID == null).Select(c => new { c.FunctionID }).ToList();
                    var lstCheck = lst.Where(c => c.Level == 5).ToList();
                    foreach (var item in lstCheck)
                    {
                        if (lstIn.Where(c => c.FunctionID == item.ID).Count() > 0)
                            lst.Remove(item);
                    }
                    lstCheck = lst.Where(c => c.Level == 4).ToList();
                    foreach (var item in lstCheck)
                    {
                        if (lstIn.Where(c => c.FunctionID == item.ID).Count() > 0 && lst.Where(c => c.ParentID == item.ID).Count() == 0)
                            lst.Remove(item);
                    }
                    lstCheck = lst.Where(c => c.Level == 3).ToList();
                    foreach (var item in lstCheck)
                    {
                        if (lstIn.Where(c => c.FunctionID == item.ID).Count() > 0 && lst.Where(c => c.ParentID == item.ID).Count() == 0)
                            lst.Remove(item);
                    }
                    lstCheck = lst.Where(c => c.Level == 2).ToList();
                    foreach (var item in lstCheck)
                    {
                        if (lstIn.Where(c => c.FunctionID == item.ID).Count() > 0 && lst.Where(c => c.ParentID == item.ID).Count() == 0)
                            lst.Remove(item);
                    }
                    lstCheck = lst.Where(c => c.Level == 1).ToList();
                    foreach (var item in lstCheck)
                    {
                        if (lstIn.Where(c => c.FunctionID == item.ID).Count() > 0 && lst.Where(c => c.ParentID == item.ID).Count() == 0)
                            lst.Remove(item);
                    }

                    result.Total = lst.Count;
                    result.Data = lst.ToArray();
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

        public void SYSConfigFunction_AddFunction(List<SYSFunction> lst, int groupid)
        {
            try
            {
                using (var model = new DataEntities())
                {
                    model.EventAccount = Account; model.EventRunning = false;
                    foreach (var item in lst)
                    {
                        var obj = model.SYS_FunctionInGroup.FirstOrDefault(c => c.GroupID == groupid && c.FunctionID == item.ID && c.ActionID == null);
                        if (obj == null)
                        {
                            obj = new SYS_FunctionInGroup();
                            obj.GroupID = groupid;
                            obj.FunctionID = item.ID;
                            obj.IsApproved = true;
                            obj.CreatedBy = Account.UserName;
                            obj.CreatedDate = DateTime.Now;

                            model.SYS_FunctionInGroup.Add(obj);
                        }
                    }
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

        public void SYSConfigFunction_DelFunction(List<SYSFunction> lst, int groupid)
        {
            try
            {
                using (var model = new DataEntities())
                {
                    model.EventAccount = Account; model.EventRunning = false;
                    var flag = false;
                    foreach (var item in lst.Where(c => c.Level == 5))
                    {
                        var obj = model.SYS_FunctionInGroup.FirstOrDefault(c => c.GroupID == groupid && c.FunctionID == item.ID && c.ActionID == null);
                        if (obj != null)
                        {
                            model.SYS_FunctionInGroup.Remove(obj);
                            foreach (var detail in model.SYS_FunctionInGroup.Where(c => c.GroupID == groupid && c.FunctionID == item.ID && c.ActionID > 0))
                                model.SYS_FunctionInGroup.Remove(detail);
                            flag = true;
                        }
                    }
                    if (flag)
                        model.SaveChanges();

                    flag = false;
                    foreach (var item in lst.Where(c => c.Level == 4))
                    {
                        var obj = model.SYS_FunctionInGroup.FirstOrDefault(c => c.GroupID == groupid && c.FunctionID == item.ID && c.ActionID == null);
                        if (obj != null && model.SYS_FunctionInGroup.Where(c => c.GroupID == groupid && c.SYS_Function.ParentID == item.ID && c.ActionID == null).Count() == 0)
                        {
                            model.SYS_FunctionInGroup.Remove(obj);
                            foreach (var detail in model.SYS_FunctionInGroup.Where(c => c.GroupID == groupid && c.FunctionID == item.ID && c.ActionID > 0))
                                model.SYS_FunctionInGroup.Remove(detail);
                            flag = true;
                        }
                    }
                    if (flag)
                        model.SaveChanges();

                    flag = false;
                    foreach (var item in lst.Where(c => c.Level == 3))
                    {
                        var obj = model.SYS_FunctionInGroup.FirstOrDefault(c => c.GroupID == groupid && c.FunctionID == item.ID && c.ActionID == null);
                        if (obj != null && model.SYS_FunctionInGroup.Where(c => c.GroupID == groupid && c.SYS_Function.ParentID == item.ID && c.ActionID == null).Count() == 0)
                        {
                            model.SYS_FunctionInGroup.Remove(obj);
                            foreach (var detail in model.SYS_FunctionInGroup.Where(c => c.GroupID == groupid && c.FunctionID == item.ID && c.ActionID > 0))
                                model.SYS_FunctionInGroup.Remove(detail);
                            flag = true;
                        }
                    }
                    if (flag)
                        model.SaveChanges();

                    flag = false;
                    foreach (var item in lst.Where(c => c.Level == 2))
                    {
                        var obj = model.SYS_FunctionInGroup.FirstOrDefault(c => c.GroupID == groupid && c.FunctionID == item.ID && c.ActionID == null);
                        if (obj != null && model.SYS_FunctionInGroup.Where(c => c.GroupID == groupid && c.SYS_Function.ParentID == item.ID && c.ActionID == null).Count() == 0)
                        {
                            model.SYS_FunctionInGroup.Remove(obj);
                            foreach (var detail in model.SYS_FunctionInGroup.Where(c => c.GroupID == groupid && c.FunctionID == item.ID && c.ActionID > 0))
                                model.SYS_FunctionInGroup.Remove(detail);
                            flag = true;
                        }
                    }
                    if (flag)
                        model.SaveChanges();

                    flag = false;
                    foreach (var item in lst.Where(c => c.Level == 1))
                    {
                        var obj = model.SYS_FunctionInGroup.FirstOrDefault(c => c.GroupID == groupid && c.FunctionID == item.ID && c.ActionID == null);
                        if (obj != null && model.SYS_FunctionInGroup.Where(c => c.GroupID == groupid && c.SYS_Function.ParentID == item.ID && c.ActionID == null).Count() == 0)
                        {
                            model.SYS_FunctionInGroup.Remove(obj);
                            foreach (var detail in model.SYS_FunctionInGroup.Where(c => c.GroupID == groupid && c.FunctionID == item.ID && c.ActionID > 0))
                                model.SYS_FunctionInGroup.Remove(detail);
                            flag = true;
                        }
                    }
                    if (flag)
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

        public SYSFunction SYSConfigFunction_GetItem(int groupid, int functionid)
        {
            try
            {
                var result = default(SYSFunction);

                using (var model = new DataEntities())
                {
                    result = model.SYS_FunctionInGroup.Where(c => c.GroupID == groupid && c.FunctionID == functionid && c.ActionID == null).Select(c => new SYSFunction
                    {
                        ID = c.FunctionID,
                        Code = c.SYS_Function.Code,
                        FunctionName = c.SYS_Function.FunctionName,
                        IsApproved = c.IsApproved
                    }).FirstOrDefault();
                    if (result != null)
                    {
                        result.ListActions = model.SYS_ActionInFunction.Where(c => c.FunctionID == result.ID).Select(c => new SYSAction
                        {
                            ID = c.ActionID,
                            Code = c.SYS_Action.Code,
                            ActionName = c.SYS_Action.ActionName,
                            IsView = c.SYS_Action.IsView
                        }).ToList();

                        var lst = model.SYS_FunctionInGroup.Where(c => c.GroupID == groupid && c.FunctionID == functionid && c.ActionID > 0).Select(c => new { c.ActionID, c.IsApproved }).ToList();
                        foreach (var item in result.ListActions)
                        {
                            item.IsApproved = lst.Where(c => c.ActionID == item.ID && c.IsApproved == true).Count() > 0;
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

        public void SYSConfigFunction_SaveItem(int groupid, int functionid, SYSFunction item)
        {
            try
            {
                using (var model = new DataEntities())
                {
                    model.EventAccount = Account; model.EventRunning = false;
                    var obj = model.SYS_FunctionInGroup.FirstOrDefault(c => c.GroupID == groupid && c.FunctionID == functionid && c.ActionID == null);
                    if (obj != null)
                    {
                        foreach (var itemAction in model.SYS_FunctionInGroup.Where(c => c.FunctionID == functionid && c.GroupID == groupid && c.ActionID > 0))
                        {
                            itemAction.IsApproved = false;
                            itemAction.ModifiedBy = Account.UserName;
                            itemAction.ModifiedDate = DateTime.Now;
                        }

                        obj.IsApproved = item.IsApproved;
                        obj.ModifiedBy = Account.UserName;
                        obj.ModifiedDate = DateTime.Now;
                        foreach (var itemAction in item.ListActions.Where(c => c.ID > 0 && c.IsApproved == true))
                        {
                            var objAction = model.SYS_FunctionInGroup.FirstOrDefault(c => c.GroupID == groupid && c.FunctionID == functionid && c.ActionID == itemAction.ID);
                            if (objAction == null)
                            {
                                objAction = new SYS_FunctionInGroup();
                                objAction.FunctionID = functionid;
                                objAction.GroupID = groupid;
                                objAction.ActionID = itemAction.ID;
                                objAction.CreatedBy = Account.UserName;
                                objAction.CreatedDate = DateTime.Now;
                            }
                            else
                            {
                                objAction.ModifiedBy = Account.UserName;
                                objAction.ModifiedDate = DateTime.Now;
                            }
                            objAction.IsApproved = itemAction.IsApproved;
                            if (objAction.ID < 1)
                                model.SYS_FunctionInGroup.Add(objAction);
                        }
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



        public List<SYSFunction> SYSConfigFunctionNotInGroup_List(int groupid)
        {
            try
            {
                var result = new List<SYSFunction>();
                using (var model = new DataEntities())
                {
                    result = model.SYS_Function.Select(c => new SYSFunction
                    {
                        ID = c.ID,
                        ParentID = c.ParentID,
                        Code = c.Code,
                        FunctionName = c.FunctionName,
                        Description = c.Description,
                        Icon = c.Icon,
                        IsAdmin = c.IsAdmin,
                        IsApproved = c.IsApproved,
                        IsCustomer = c.IsCustomer,
                        SortOrder = c.SortOrder,
                        Level = c.Level
                    }).ToList();
                    var lstInGroup = model.SYS_FunctionInGroup.Where(c => c.GroupID == groupid).Select(c => c.FunctionID).ToList();
                    for (int i = 5; i > 0; i--)
                    {
                        var lstDel = result.Where(c => c.Level == i).ToList();
                        foreach (var item in lstDel)
                        {
                            if (result.Where(c => c.ParentID == item.ID).Count() == 0)
                                if (lstInGroup.Contains(item.ID))
                                    result.Remove(item);
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

        public List<SYSFunction> SYSConfigFunctionInGroup_List(int groupid)
        {
            try
            {
                var result = new List<SYSFunction>();
                using (var model = new DataEntities())
                {
                    result = model.SYS_Function.Where(c => c.SYS_FunctionInGroup.Any(d => d.GroupID == groupid)).Select(c => new SYSFunction
                    {
                        ID = c.ID,
                        ParentID = c.ParentID,
                        Code = c.Code,
                        FunctionName = c.FunctionName,
                        Description = c.Description,
                        Icon = c.Icon,
                        IsAdmin = c.IsAdmin,
                        IsApproved = c.IsApproved,
                        IsCustomer = c.IsCustomer,
                        SortOrder = c.SortOrder,
                        Level = c.Level
                    }).ToList();
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

        public void SYSConfigFunction_Save(List<int> lstid, int groupid)
        {
            try
            {
                using (var model = new DataEntities())
                {
                    model.EventAccount = Account; model.EventRunning = false;
                    foreach (var id in lstid)
                    {
                        var obj = new SYS_FunctionInGroup();
                        obj.GroupID = groupid;
                        obj.FunctionID = id;
                        obj.CreatedBy = Account.UserName;
                        obj.CreatedDate = DateTime.Now;
                        model.SYS_FunctionInGroup.Add(obj);
                    }
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

        public void SYSConfigFunction_Delete(List<int> lstid, int groupid)
        {
            try
            {
                using (var model = new DataEntities())
                {
                    model.EventAccount = Account; model.EventRunning = false;
                    foreach (var id in lstid)
                    {
                        if (model.SYS_Function.Where(c => c.ParentID == id).Count() == 0)
                            foreach (var item in model.SYS_FunctionInGroup.Where(c => c.GroupID == groupid && c.FunctionID == id))
                                model.SYS_FunctionInGroup.Remove(item);
                    }
                    model.SaveChanges();

                    foreach (var id in lstid)
                    {
                        if (model.SYS_FunctionInGroup.Where(c => c.GroupID == groupid && c.SYS_Function.ParentID == id).Count() == 0)
                            foreach (var item in model.SYS_FunctionInGroup.Where(c => c.GroupID == groupid && c.FunctionID == id))
                                model.SYS_FunctionInGroup.Remove(item);
                    }
                    model.SaveChanges();
                    foreach (var id in lstid)
                    {
                        if (model.SYS_FunctionInGroup.Where(c => c.GroupID == groupid && c.SYS_Function.ParentID == id).Count() == 0)
                            foreach (var item in model.SYS_FunctionInGroup.Where(c => c.GroupID == groupid && c.FunctionID == id))
                                model.SYS_FunctionInGroup.Remove(item);
                    }
                    model.SaveChanges();
                    foreach (var id in lstid)
                    {
                        if (model.SYS_FunctionInGroup.Where(c => c.GroupID == groupid && c.SYS_Function.ParentID == id).Count() == 0)
                            foreach (var item in model.SYS_FunctionInGroup.Where(c => c.GroupID == groupid && c.FunctionID == id))
                                model.SYS_FunctionInGroup.Remove(item);
                    }
                    model.SaveChanges();
                    foreach (var id in lstid)
                    {
                        if (model.SYS_FunctionInGroup.Where(c => c.GroupID == groupid && c.SYS_Function.ParentID == id).Count() == 0)
                            foreach (var item in model.SYS_FunctionInGroup.Where(c => c.GroupID == groupid && c.FunctionID == id))
                                model.SYS_FunctionInGroup.Remove(item);
                    }
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

        public List<SYSAction> SYSConfigAction_Get(int groupid, int functionid)
        {
            try
            {
                using (var model = new DataEntities())
                {
                    var result = model.SYS_ActionInFunction.Where(c => c.FunctionID == functionid).Select(c => new SYSAction
                    {
                        IsApproved = false,
                        ID = c.SYS_Action.ID,
                        Code = c.SYS_Action.Code,
                        ActionName = c.SYS_Action.ActionName
                    }).ToList();
                    var lstCheck = model.SYS_FunctionInGroup.Where(c => c.GroupID == groupid && c.FunctionID == functionid).Select(c => new { c.ActionID, c.IsApproved }).ToList();
                    foreach (var item in result)
                    {
                        item.IsApproved = lstCheck.Where(c => c.ActionID == item.ID && c.IsApproved).Count() > 0;
                    }
                    return result;
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

        public void SYSConfigAction_Save(int groupid, int functionid, List<SYSAction> lst)
        {
            try
            {
                using (var model = new DataEntities())
                {
                    model.EventAccount = Account; model.EventRunning = false;
                    foreach (var item in model.SYS_FunctionInGroup.Where(c => c.GroupID == groupid && c.FunctionID == functionid && c.ActionID != null))
                    {
                        item.IsApproved = false;
                        item.ModifiedBy = Account.UserName;
                        item.ModifiedDate = DateTime.Now;
                    }
                    foreach (var item in lst)
                    {
                        if (item.IsApproved)
                        {
                            var obj = model.SYS_FunctionInGroup.FirstOrDefault(c => c.GroupID == groupid && c.FunctionID == functionid && c.ActionID == item.ID);
                            if (obj == null)
                            {
                                obj = new SYS_FunctionInGroup();
                                obj.GroupID = groupid;
                                obj.FunctionID = functionid;
                                obj.ActionID = item.ID;
                                obj.CreatedBy = Account.UserName;
                                obj.CreatedDate = DateTime.Now;
                            }
                            else
                            {
                                obj.ModifiedBy = Account.UserName;
                                obj.ModifiedDate = DateTime.Now;
                            }
                            obj.IsApproved = true;
                            if (obj.ID < 1)
                                model.SYS_FunctionInGroup.Add(obj);
                        }
                    }
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

        public List<SYSFunction> SYSConfig_ExcelFuntion()
        {
            try
            {
                var result = new List<SYSFunction>();
                using (var model = new DataEntities())
                {
                    result = model.SYS_Function.Where(c => c.IsApproved).Select(c => new SYSFunction
                    {
                        ID = c.ID,
                        ParentID = c.ParentID,
                        Code = c.Code,
                        FunctionName = c.FunctionName,
                        Level = c.Level,
                        SortOrder = c.SortOrder,
                        //Details = c.SYS_ActionInFunction.Select(d => new SYSAction { ID = d.ActionID }).ToList()
                    }).ToList();
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

        public List<SYSAction> SYSConfig_ExcelAction()
        {
            try
            {
                var result = new List<SYSAction>();
                using (var model = new DataEntities())
                {
                    result = model.SYS_Action.Select(c => new SYSAction
                    {
                        ID = c.ID,
                        Code = c.Code,
                        ActionName = c.ActionName
                    }).ToList();
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

        public List<DTOSYSFunctionInGroup> SYSConfig_ExcelFunctionInGroup(int groupid)
        {
            try
            {
                var result = new List<DTOSYSFunctionInGroup>();
                using (var model = new DataEntities())
                {
                    result = model.SYS_FunctionInGroup.Where(c => c.GroupID == groupid).Select(c => new DTOSYSFunctionInGroup
                    {
                        FunctionID = c.FunctionID,
                        GroupID = c.GroupID,
                        ActionID = c.ActionID,
                        IsApproved = c.IsApproved
                    }).ToList();
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

        #region Dashboard

        #endregion

        #region User
        public DTOResult SYSUser_Read(string request)
        {
            try
            {
                DTOResult result = new DTOResult();
                using (var model = new DataEntities())
                {
                    var query = model.SYS_User.Where(c => c.UserName != Account.UserName && c.IsAdmin == false).Select(c => new SYSUser
                    {
                        ID = c.ID,
                        UserName = c.UserName,
                        Code = c.Code,
                        FirstName = c.FirstName,
                        LastName = c.LastName,
                        Email = c.Email,
                        IsAdmin = c.IsAdmin,
                        GroupID = c.GroupID,
                        Image = c.Image,
                        IsApproved = c.IsApproved,
                        DriverID = c.DriverID,
                        ListCustomerID = c.ListCustomerID,
                        GroupName = (c.GroupID != null) ? c.SYS_Group.GroupName : "",
                        CreatedDate = c.CreatedDate
                    }).ToDataSourceResult(CreateRequest(request));
                    if (Account.IsAdmin)
                    {
                        query = model.SYS_User.Where(c => c.UserName != Account.UserName).Select(c => new SYSUser
                        {
                            ID = c.ID,
                            UserName = c.UserName,
                            Code = c.Code,
                            FirstName = c.FirstName,
                            LastName = c.LastName,
                            Email = c.Email,
                            IsAdmin = c.IsAdmin,
                            GroupID = c.GroupID,
                            Image = c.Image,
                            IsApproved = c.IsApproved,
                            DriverID = c.DriverID,
                            ListCustomerID = c.ListCustomerID,
                            GroupName = (c.GroupID != null) ? c.SYS_Group.GroupName : "",
                            CreatedDate = c.CreatedDate
                        }).ToDataSourceResult(CreateRequest(request));
                    }

                    result.Total = query.Total;
                    result.Data = query.Data as IEnumerable<SYSUser>;
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

        public SYSUser SYSUser_Get(int id)
        {
            try
            {
                var result = new SYSUser { ID = -1, SYSCustomerID = 0, UserName = "", Email = "", ListCustomer = new List<CUSCustomer>() };
                using (var model = new DataEntities())
                {
                    if (id > 0)
                    {
                        result = model.SYS_User.Where(c => c.ID == id).Select(c => new SYSUser
                        {
                            ID = c.ID,
                            UserName = c.UserName,
                            Code = c.Code,
                            FirstName = c.FirstName,
                            LastName = c.LastName,
                            Email = c.Email,
                            IsAdmin = c.IsAdmin,
                            GroupID = c.GroupID,
                            Image = c.Image,
                            IsApproved = c.IsApproved,
                            CustomerID = c.CustomerID,
                            DriverID = c.DriverID,
                            SYSCustomerID = c.SYSCustomerID,
                            ListCustomerID = c.ListCustomerID,
                            TelNo = c.TelNo,
                        }).FirstOrDefault();
                        if (result != null)
                        {
                            result.ListCustomer = model.CUS_Customer.Where(c => c.IsSystem == false).Select(c => new CUSCustomer
                            {
                                ID = c.ID,
                                Code = c.Code,
                                CustomerName = c.CustomerName,
                                TypeOfCustomerCode = c.SYS_Var.Code,
                                TypeOfCustomerName = c.SYS_Var.ValueOfVar
                            }).ToList();
                        }
                    }
                    else
                    {
                        var sysCustomer = model.CUS_Customer.FirstOrDefault(c => c.IsSystem && c.ParentID > 0);
                        if (sysCustomer != null)
                            result.SYSCustomerID = sysCustomer.ID;

                        result.IsApproved = true;
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

        public List<DTOCombobox> SYSUser_Group()
        {
            var result = new List<DTOCombobox>();
            try
            {
                using (var model = new DataEntities())
                {
                    var lstData = model.SYS_Group.Select(c => new SYSGroup
                    {
                        ID = c.ID,
                        ParentID = c.ParentID,
                        Code = c.Code,
                        GroupName = c.GroupName
                    }).ToList();
                    SYSUser_Group_Child(ref result, lstData, null, 0);

                    result.Insert(0, new DTOCombobox { ValueInt = -1, ValueString = "-1", Text = "" });
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

        private void SYSUser_Group_Child(ref List<DTOCombobox> lst, List<SYSGroup> lstData, int? parentid, int lvl)
        {
            var lstChild = lstData.Where(c => c.ParentID == parentid).ToList();
            if (parentid == null)
                lstChild = lstData.Where(c => c.ParentID == null).ToList();
            foreach (var item in lstChild)
            {
                lst.Add(new DTOCombobox { ValueInt = item.ID, ValueString = item.ID + "", Text = new string('.', lvl * 3) + item.GroupName });
                SYSUser_Group_Child(ref lst, lstData, item.ID, lvl + 1);
            }
        }



        public List<DTOCombobox> SYSUser_Customer()
        {
            var result = new List<DTOCombobox>();
            try
            {
                using (var model = new DataEntities())
                {
                    //result = model.CUS_Customer.Where(c => c.IsSystem && c.ParentID == null).Select(c => new DTOCombobox
                    //{
                    //    ValueInt = c.ID,
                    //    ValueString = c.ID + "",
                    //    Text = c.CustomerName
                    //}).ToList();
                    result.AddRange(model.CUS_Customer.Where(c => c.IsSystem && c.ParentID > 0).Select(c => new DTOCombobox
                    {
                        ValueInt = c.ID,
                        ValueString = c.ID + "",
                        Text = c.CustomerName
                    }).ToList());
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

        public bool SYSUser_CheckUserName(int id, string username)
        {
            try
            {
                using (var model = new DataEntities())
                {
                    return model.SYS_User.Where(c => c.ID != id && c.UserName == username).Count() > 0;
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

        public bool SYSUser_CheckIsAdmin()
        {
            try
            {
                if (Account != null)
                    return Account.IsAdmin;
                else throw FaultHelper.BusinessFault(null, null, "Chưa đăng nhập");
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
        public int SYSUser_CheckData(int id, string username, string email)
        {
            try
            {
                var result = 0;
                using (var model = new DataEntities())
                {
                    result = model.SYS_User.Where(c => c.ID != id && c.UserName == username).Count() > 0 ? 1 : 0;
                    if (result == 0)
                        result = model.SYS_User.Where(c => c.ID != id && c.Email == email).Count() > 0 ? 2 : 0;
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

        public int SYSUser_Save(SYSUser item)
        {
            try
            {
                using (var model = new DataEntities())
                {
                    model.EventAccount = Account; model.EventRunning = false;
                    var obj = model.SYS_User.FirstOrDefault(c => c.ID == item.ID);
                    if (obj == null)
                    {
                        obj = new SYS_User();
                        obj.CreatedBy = Account.UserName;
                        obj.CreatedDate = DateTime.Now;
                        obj.Code = "";
                    }
                    else
                    {
                        obj.ModifiedBy = Account.UserName;
                        obj.ModifiedDate = DateTime.Now;
                    }
                    obj.SYSCustomerID = item.SYSCustomerID;
                    obj.LastName = item.LastName;
                    obj.FirstName = item.FirstName;
                    obj.Email = item.Email;
                    obj.UserName = item.UserName;
                    obj.IsApproved = item.IsApproved;
                    obj.IsAdmin = item.IsAdmin;
                    obj.DriverID = item.DriverID > 0 ? item.DriverID : null;
                    obj.TelNo = item.TelNo;
                    if (item.GroupID > 0)
                        obj.GroupID = item.GroupID;
                    else
                        obj.GroupID = null;
                    obj.Image = item.Image;
                    obj.ListCustomerID = item.ListCustomerID;

                    if (obj.ID < 1)
                        model.SYS_User.Add(obj);
                    model.SaveChanges();

                    return obj.ID;
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

        public void SYSUser_Delete(List<int> lstid)
        {
            try
            {
                using (var model = new DataEntities())
                {
                    model.EventAccount = Account; model.EventRunning = false;
                    if (lstid.Count > 0)
                    {
                        foreach (var item in lstid)
                        {
                            var obj = model.SYS_User.FirstOrDefault(c => c.ID == item);
                            if (obj != null)
                            {
                                foreach (var detail in model.SYS_UserResource.Where(c => c.UserID == obj.ID))
                                    model.SYS_UserResource.Remove(detail);
                                foreach (var detail in model.SYS_UserSetting.Where(c => c.UserID == obj.ID))
                                    model.SYS_UserSetting.Remove(detail);
                                foreach (var detail in model.WFL_Action.Where(c => c.UserID == obj.ID))
                                {
                                    foreach (var detail1 in model.WFL_Message.Where(c => c.ActionID == detail.ID))
                                        model.WFL_Message.Remove(detail1);
                                    model.WFL_Action.Remove(detail);
                                }

                                foreach (var detail in model.CAT_Comment.Where(c => c.UserID == obj.ID))
                                    model.CAT_Comment.Remove(detail);
                                foreach (var detail in model.WFL_DefineWFAction.Where(c => c.UserID == obj.ID))
                                {
                                    foreach (var detail1 in model.WFL_DefineWFMessage.Where(c => c.DefineWFActionID == detail.ID))
                                        model.WFL_DefineWFMessage.Remove(detail1);
                                    model.WFL_DefineWFAction.Remove(detail);
                                }
                                foreach (var detail in model.WFL_PacketSettingAction.Where(c => c.UserID == obj.ID))
                                {
                                    foreach (var detail1 in model.WFL_PacketAction.Where(c => c.PacketSettingActionID == detail.ID))
                                        model.WFL_PacketAction.Remove(detail1);
                                    model.WFL_PacketSettingAction.Remove(detail);
                                }

                                model.SYS_User.Remove(obj);
                            }
                        }
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

        public SYSUser_Export SYSUser_Export()
        {
            try
            {
                using (var model = new DataEntities())
                {
                    var result = new SYSUser_Export();

                    result.ListUser = model.SYS_User.Where(c => (c.SYSCustomerID == Account.SYSCustomerID || Account.IsAdmin) && c.UserName != Account.UserName).Select(c => new SYSUser
                    {
                        ID = c.ID,
                        UserName = c.UserName,
                        FirstName = c.FirstName,
                        LastName = c.LastName,
                        Email = c.Email,
                        IsAdmin = c.IsAdmin,
                        GroupID = c.GroupID,
                        GroupCode = (c.GroupID != null) ? c.SYS_Group.Code : "",
                        GroupName = (c.GroupID != null) ? c.SYS_Group.GroupName : "",
                        IsApproved = c.IsApproved,
                        TelNo = c.TelNo,
                        ListCustomerID = c.ListCustomerID,
                        SYSCustomerID = c.SYSCustomerID
                    }).ToList();

                    result.ListCustomer = model.CUS_Customer.Where(c => c.IsSystem == false).Select(c => new CUSCustomer
                    {
                        ID = c.ID,
                        Code = c.Code
                    }).ToList();

                    result.ListSYSCustomer = model.CUS_Customer.Where(c => c.IsSystem).Select(c => new CUSCustomer
                    {
                        ID = c.ID,
                        Code = c.Code
                    }).ToList();

                    return result;
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

        public SYSUser_ImportCheck SYSUser_ImportCheck()
        {
            try
            {
                using (var model = new DataEntities())
                {
                    var result = new SYSUser_ImportCheck();

                    result.ListUser = model.SYS_User.Where(c => (c.SYSCustomerID == Account.SYSCustomerID || Account.IsAdmin) && c.UserName != Account.UserName).Select(c => new SYSUser
                    {
                        ID = c.ID,
                        UserName = c.UserName,
                        Email = c.Email
                    }).ToList();

                    result.ListGroup = model.SYS_Group.Select(c => new SYSGroup
                    {
                        ID = c.ID,
                        Code = c.Code
                    }).ToList();

                    result.ListCustomer = model.CUS_Customer.Where(c => c.IsSystem == false).Select(c => new CUSCustomer
                    {
                        ID = c.ID,
                        Code = c.Code
                    }).ToList();

                    result.ListSYSCustomer = model.CUS_Customer.Where(c => c.IsSystem).Select(c => new CUSCustomer
                    {
                        ID = c.ID,
                        Code = c.Code
                    }).ToList();

                    return result;
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

        public void SYSUser_Import(List<SYSUser_Import> lst)
        {
            try
            {
                using (var model = new DataEntities())
                {
                    model.EventAccount = Account; model.EventRunning = false;
                    foreach (var item in lst.Where(c => c.ExcelSuccess))
                    {
                        var obj = model.SYS_User.FirstOrDefault(c => c.ID == item.ID);
                        if (obj == null)
                        {
                            obj = new SYS_User();
                            obj.UserName = item.UserName;
                            obj.Email = item.Email;
                            obj.IsAdmin = false;
                            obj.CreatedBy = Account.UserName;
                            obj.CreatedDate = DateTime.Now;
                        }
                        else
                        {
                            obj.ModifiedBy = Account.UserName;
                            obj.ModifiedDate = DateTime.Now;
                        }
                        if (item.SYSCustomerID > 0)
                            obj.SYSCustomerID = item.SYSCustomerID;
                        else
                            obj.SYSCustomerID = Account.SYSCustomerID;
                        obj.LastName = item.LastName;
                        obj.FirstName = item.FirstName;
                        obj.TelNo = item.TelNo;
                        if (item.GroupID > 0)
                            obj.GroupID = item.GroupID;
                        else
                            obj.GroupID = null;
                        obj.IsApproved = item.IsApproved;
                        obj.ListCustomerID = item.ListCustomerID;
                        obj.Code = string.Empty;
                        if (obj.ID < 1)
                            model.SYS_User.Add(obj);
                    }
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

        public List<DTOCombobox> SYSUser_Driver()
        {
            var result = new List<DTOCombobox>();
            try
            {
                using (var model = new DataEntities())
                {
                    result = model.FLM_Driver.Select(c => new DTOCombobox
                    {
                        ValueInt = c.ID,
                        ValueString = c.ID + "",
                        Text = c.CAT_Driver.FirstName + " " + c.CAT_Driver.LastName,
                    }).ToList();
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

        public DTOFLMDriver FLMDriver_Get(int driverID)
        {
            try
            {
                DTOFLMDriver result = new DTOFLMDriver();
                using (var model = new DataEntities())
                {
                    if (driverID > 0)
                    {
                        result = model.FLM_Driver.Where(c => c.ID == driverID).Select(c => new DTOFLMDriver
                        {
                            ID = c.ID,
                            EmployeeCode = c.Code,
                            LastName = c.CAT_Driver.LastName,
                            FirstName = c.CAT_Driver.FirstName,
                            Cellphone = c.CAT_Driver.Cellphone,
                            BiddingID = c.BiddingID,
                            SYSCustomerID = c.SYSCustomerID,
                            CardNumber = c.CAT_Driver.CardNumber,
                            Birthday = c.CAT_Driver.Birthday,
                            Note = c.Note,
                            IsUse = c.IsUse,
                            // DrivingLicenceID = c.DrivingLicenceID,
                            IsAssistant = c.IsAssistant,
                            ListDrivingLicence = c.CAT_Driver.ListDrivingLicence,
                            DriverName = string.Empty,
                            FeeBase = c.FeeBase,
                            Image = c.Image,
                            DateStart = c.DateStart.Value,
                            DateEnd = c.DateEnd.Value
                        }).FirstOrDefault();
                    }
                    else
                    {
                        result.ID = 0;
                        result.IsUse = true;
                        result.DateStart = DateTime.Now;
                        result.DateEnd = null;
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

        public DTOFLMTruck Truck_GetByDriver(int driverid)
        {
            try
            {
                DTOFLMTruck result = new DTOFLMTruck();
                using (var model = new DataEntities())
                {
                    if (driverid > 0)
                    {
                        var query = model.FLM_Asset.Where(c => c.CAT_Vehicle.DriverID == driverid).Select(c => new DTOFLMTruck
                        {
                            ID = c.ID,
                            VehicleID = c.VehicleID,
                            GroupOfVehicleID = c.CAT_Vehicle.GroupOfVehicleID,
                            GroupOfVehicleName = c.CAT_Vehicle.GroupOfVehicleID.HasValue ? c.CAT_Vehicle.CAT_GroupOfVehicle.GroupName : string.Empty,
                            YearOfProduction = c.YearOfProduction,
                            Manufactor = c.Manufactor,
                            BaseValue = c.BaseValue,
                            CurrentValue = c.CurrentValue,
                            RemainValue = c.RemainValue,
                            DepreciationPeriod = c.DepreciationPeriod,
                            DepreciationStart = c.DepreciationStart,
                            Specification = c.Specification,
                            IsRent = c.IsRent,
                            RentID = c.RentID,
                            RegNo = c.CAT_Vehicle.RegNo,
                            Note = c.CAT_Vehicle.Note,
                            IsOwn = c.CAT_Vehicle.IsOwn,
                            CurrentVendorID = c.CAT_Vehicle.CurrentVendorID,
                            TypeOfVehicleID = c.CAT_Vehicle.TypeOfVehicleID,
                            MaxWeight = c.CAT_Vehicle.MaxWeight,
                            MaxCapacity = c.CAT_Vehicle.MaxCapacity,
                            RegWeight = c.CAT_Vehicle.RegWeight,
                            RegCapacity = c.CAT_Vehicle.RegCapacity,
                            Lat = c.CAT_Vehicle.Lat,
                            Lng = c.CAT_Vehicle.Lng,
                            MinWeight = c.CAT_Vehicle.MinWeight,
                            MinCapacity = c.CAT_Vehicle.MinCapacity,
                            WarrantyEnd = c.WarrantyEnd,
                            WarrantyPeriod = c.WarrantyPeriod,
                            DriverID = c.CAT_Vehicle.FLM_Driver.ID,
                            AssistantID = c.CAT_Vehicle.FLM_Driver1.ID,
                        });
                        result = query.FirstOrDefault();
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
                throw FaultHelper.ServiceFault(ex);
            }
        }

        #endregion

        #region UserChild
        public DTOResult SYSUserChild_Read(string request)
        {
            try
            {
                DTOResult result = new DTOResult();
                using (var model = new DataEntities())
                {
                    var query = model.SYS_User.Where(c => c.UserName != Account.UserName && c.IsAdmin == false && c.SYSCustomerID == Account.SYSCustomerID).Select(c => new SYSUser
                    {
                        ID = c.ID,
                        UserName = c.UserName,
                        Code = c.Code,
                        FirstName = c.FirstName,
                        LastName = c.LastName,
                        Email = c.Email,
                        IsAdmin = c.IsAdmin,
                        GroupID = c.GroupID,
                        Image = c.Image,
                        IsApproved = c.IsApproved,
                        DriverID = c.DriverID,
                        ListCustomerID = c.ListCustomerID,
                        GroupName = (c.GroupID != null) ? c.SYS_Group.GroupName : "",
                        CreatedDate = c.CreatedDate
                    }).ToDataSourceResult(CreateRequest(request));
                    if (Account.IsAdmin)
                    {
                        query = model.SYS_User.Where(c => c.UserName != Account.UserName).Select(c => new SYSUser
                        {
                            ID = c.ID,
                            UserName = c.UserName,
                            Code = c.Code,
                            FirstName = c.FirstName,
                            LastName = c.LastName,
                            Email = c.Email,
                            IsAdmin = c.IsAdmin,
                            GroupID = c.GroupID,
                            Image = c.Image,
                            IsApproved = c.IsApproved,
                            DriverID = c.DriverID,
                            ListCustomerID = c.ListCustomerID,
                            GroupName = (c.GroupID != null) ? c.SYS_Group.GroupName : "",
                            CreatedDate = c.CreatedDate
                        }).ToDataSourceResult(CreateRequest(request));
                    }

                    result.Total = query.Total;
                    result.Data = query.Data as IEnumerable<SYSUser>;
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

        public SYSUser SYSUserChild_Get(int id)
        {
            try
            {
                var result = new SYSUser { ID = -1, SYSCustomerID = 0, UserName = "", Email = "", ListCustomer = new List<CUSCustomer>() };
                using (var model = new DataEntities())
                {
                    if (id > 0)
                    {
                        result = model.SYS_User.Where(c => c.ID == id).Select(c => new SYSUser
                        {
                            ID = c.ID,
                            UserName = c.UserName,
                            Code = c.Code,
                            FirstName = c.FirstName,
                            LastName = c.LastName,
                            Email = c.Email,
                            IsAdmin = c.IsAdmin,
                            GroupID = c.GroupID,
                            Image = c.Image,
                            IsApproved = c.IsApproved,
                            CustomerID = c.CustomerID,
                            DriverID = c.DriverID,
                            SYSCustomerID = c.SYSCustomerID,
                            ListCustomerID = c.ListCustomerID,
                            TelNo = c.TelNo,
                        }).FirstOrDefault();
                        if (result != null)
                        {
                            var lstCustomerID = new List<int>();
                            var group = model.SYS_Group.FirstOrDefault(c => c.ID == result.GroupID);
                            if (group != null)
                            {
                                if (!string.IsNullOrEmpty(group.ListCustomerID))
                                {
                                    var arrCus = group.ListCustomerID.Split(',').Distinct().ToList();
                                    foreach (var item in arrCus.Where(c => !string.IsNullOrEmpty(c)))
                                    {
                                        try { lstCustomerID.Add(Convert.ToInt32(item)); }
                                        catch { }
                                    }
                                }
                            }

                            result.ListCustomer = model.CUS_Customer.Where(c => c.IsSystem == false && lstCustomerID.Contains(c.ID)).Select(c => new CUSCustomer
                            {
                                ID = c.ID,
                                Code = c.Code,
                                CustomerName = c.CustomerName,
                                TypeOfCustomerCode = c.SYS_Var.Code,
                                TypeOfCustomerName = c.SYS_Var.ValueOfVar
                            }).ToList();
                        }
                    }
                    else
                    {
                        var sysCustomer = model.CUS_Customer.FirstOrDefault(c => c.IsSystem && c.ParentID > 0);
                        if (sysCustomer != null)
                            result.SYSCustomerID = sysCustomer.ID;

                        result.IsApproved = true;
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

        private void SYSUserChild_Group_Child(ref List<DTOCombobox> lst, List<SYSGroup> lstData, int? parentid, int lvl)
        {
            var lstChild = lstData.Where(c => c.ParentID == parentid).ToList();
            if (parentid == null)
                lstChild = lstData.Where(c => c.ParentID == null).ToList();
            foreach (var item in lstChild)
            {
                lst.Add(new DTOCombobox { ValueInt = item.ID, ValueString = item.ID + "", Text = new string('.', lvl * 3) + item.GroupName });
                SYSUser_Group_Child(ref lst, lstData, item.ID, lvl + 1);
            }
        }

        public List<DTOCombobox> SYSUserChild_Group()
        {
            var result = new List<DTOCombobox>();
            try
            {
                using (var model = new DataEntities())
                {
                    var lstData = model.SYS_Group.Select(c => new SYSGroup
                    {
                        ID = c.ID,
                        ParentID = c.ParentID,
                        Code = c.Code,
                        GroupName = c.GroupName
                    }).ToList();

                    SYSUserChild_Group_Child(ref result, lstData, Account.GroupID, 0);

                    result.Insert(0, new DTOCombobox { ValueInt = -1, ValueString = "-1", Text = "" });
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

        public List<DTOCombobox> SYSUserChild_Customer()
        {
            var result = new List<DTOCombobox>();
            try
            {
                using (var model = new DataEntities())
                {
                    var lstCustomerID = new List<int>();
                    var group = model.SYS_Group.FirstOrDefault(c => c.ID == Account.GroupID);
                    if (group != null)
                    {
                        if (!string.IsNullOrEmpty(group.ListCustomerID))
                        {
                            var arrCus = group.ListCustomerID.Split(',').Distinct().ToList();
                            foreach (var item in arrCus.Where(c => !string.IsNullOrEmpty(c)))
                            {
                                try { lstCustomerID.Add(Convert.ToInt32(item)); }
                                catch { }
                            }
                        }
                    }
                    result.AddRange(model.CUS_Customer.Where(c => lstCustomerID.Contains(c.ID)).Select(c => new DTOCombobox
                    {
                        ValueInt = c.ID,
                        ValueString = c.ID + "",
                        Text = c.CustomerName
                    }).ToList());
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

        public bool SYSUserChild_CheckUserName(int id, string username)
        {
            try
            {
                using (var model = new DataEntities())
                {
                    return model.SYS_User.Where(c => c.ID != id && c.UserName == username).Count() > 0;
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

        public bool SYSUserChild_CheckIsAdmin()
        {
            try
            {
                if (Account != null)
                    return Account.IsAdmin;
                else throw FaultHelper.BusinessFault(null, null, "Chưa đăng nhập");
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
        public int SYSUserChild_CheckData(int id, string username, string email)
        {
            try
            {
                var result = 0;
                using (var model = new DataEntities())
                {
                    result = model.SYS_User.Where(c => c.ID != id && c.UserName == username).Count() > 0 ? 1 : 0;
                    if (result == 0)
                        result = model.SYS_User.Where(c => c.ID != id && c.Email == email).Count() > 0 ? 2 : 0;
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

        public int SYSUserChild_Save(SYSUser item)
        {
            try
            {
                using (var model = new DataEntities())
                {
                    model.EventAccount = Account; model.EventRunning = false;
                    var obj = model.SYS_User.FirstOrDefault(c => c.ID == item.ID);
                    if (obj == null)
                    {
                        obj = new SYS_User();
                        obj.CreatedBy = Account.UserName;
                        obj.CreatedDate = DateTime.Now;
                        obj.Code = "";
                    }
                    else
                    {
                        obj.ModifiedBy = Account.UserName;
                        obj.ModifiedDate = DateTime.Now;
                    }
                    obj.SYSCustomerID = Account.SYSCustomerID;
                    obj.LastName = item.LastName;
                    obj.FirstName = item.FirstName;
                    obj.Email = item.Email;
                    obj.UserName = item.UserName;
                    obj.IsApproved = item.IsApproved;
                    obj.IsAdmin = item.IsAdmin;
                    obj.DriverID = item.DriverID > 0 ? item.DriverID : null;
                    obj.TelNo = item.TelNo;
                    if (item.GroupID > 0)
                        obj.GroupID = item.GroupID;
                    else
                        obj.GroupID = null;
                    obj.Image = item.Image;
                    obj.ListCustomerID = item.ListCustomerID;

                    if (obj.ID < 1)
                        model.SYS_User.Add(obj);
                    model.SaveChanges();

                    return obj.ID;
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

        public void SYSUserChild_Delete(List<int> lstid)
        {
            try
            {
                using (var model = new DataEntities())
                {
                    model.EventAccount = Account; model.EventRunning = false;
                    if (lstid.Count > 0)
                    {
                        foreach (var item in lstid)
                        {
                            var obj = model.SYS_User.FirstOrDefault(c => c.ID == item);
                            if (obj != null)
                            {
                                foreach (var detail in model.SYS_UserResource.Where(c => c.UserID == obj.ID))
                                    model.SYS_UserResource.Remove(detail);
                                foreach (var detail in model.SYS_UserSetting.Where(c => c.UserID == obj.ID))
                                    model.SYS_UserSetting.Remove(detail);
                                foreach (var detail in model.WFL_Action.Where(c => c.UserID == obj.ID))
                                {
                                    foreach (var detail1 in model.WFL_Message.Where(c => c.ActionID == detail.ID))
                                        model.WFL_Message.Remove(detail1);
                                    model.WFL_Action.Remove(detail);
                                }

                                foreach (var detail in model.CAT_Comment.Where(c => c.UserID == obj.ID))
                                    model.CAT_Comment.Remove(detail);
                                foreach (var detail in model.WFL_DefineWFAction.Where(c => c.UserID == obj.ID))
                                {
                                    foreach (var detail1 in model.WFL_DefineWFMessage.Where(c => c.DefineWFActionID == detail.ID))
                                        model.WFL_DefineWFMessage.Remove(detail1);
                                    model.WFL_DefineWFAction.Remove(detail);
                                }
                                foreach (var detail in model.WFL_PacketSettingAction.Where(c => c.UserID == obj.ID))
                                {
                                    foreach (var detail1 in model.WFL_PacketAction.Where(c => c.PacketSettingActionID == detail.ID))
                                        model.WFL_PacketAction.Remove(detail1);
                                    model.WFL_PacketSettingAction.Remove(detail);
                                }

                                model.SYS_User.Remove(obj);
                            }
                        }
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

        public SYSUser_Export SYSUserChild_Export()
        {
            try
            {
                using (var model = new DataEntities())
                {
                    var result = new SYSUser_Export();

                    result.ListUser = model.SYS_User.Where(c => (c.SYSCustomerID == Account.SYSCustomerID || Account.IsAdmin) && c.UserName != Account.UserName).Select(c => new SYSUser
                    {
                        ID = c.ID,
                        UserName = c.UserName,
                        FirstName = c.FirstName,
                        LastName = c.LastName,
                        Email = c.Email,
                        IsAdmin = c.IsAdmin,
                        GroupID = c.GroupID,
                        GroupCode = (c.GroupID != null) ? c.SYS_Group.Code : "",
                        GroupName = (c.GroupID != null) ? c.SYS_Group.GroupName : "",
                        IsApproved = c.IsApproved,
                        TelNo = c.TelNo,
                        ListCustomerID = c.ListCustomerID,
                        SYSCustomerID = c.SYSCustomerID
                    }).ToList();

                    result.ListCustomer = model.CUS_Customer.Where(c => c.IsSystem == false).Select(c => new CUSCustomer
                    {
                        ID = c.ID,
                        Code = c.Code
                    }).ToList();

                    result.ListSYSCustomer = model.CUS_Customer.Where(c => c.IsSystem).Select(c => new CUSCustomer
                    {
                        ID = c.ID,
                        Code = c.Code
                    }).ToList();

                    return result;
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

        public SYSUser_ImportCheck SYSUserChild_ImportCheck()
        {
            try
            {
                using (var model = new DataEntities())
                {
                    var result = new SYSUser_ImportCheck();

                    result.ListUser = model.SYS_User.Where(c => (c.SYSCustomerID == Account.SYSCustomerID || Account.IsAdmin) && c.UserName != Account.UserName).Select(c => new SYSUser
                    {
                        ID = c.ID,
                        UserName = c.UserName,
                        Email = c.Email
                    }).ToList();

                    result.ListGroup = model.SYS_Group.Select(c => new SYSGroup
                    {
                        ID = c.ID,
                        Code = c.Code
                    }).ToList();

                    result.ListCustomer = model.CUS_Customer.Where(c => c.IsSystem == false).Select(c => new CUSCustomer
                    {
                        ID = c.ID,
                        Code = c.Code
                    }).ToList();

                    result.ListSYSCustomer = model.CUS_Customer.Where(c => c.IsSystem).Select(c => new CUSCustomer
                    {
                        ID = c.ID,
                        Code = c.Code
                    }).ToList();

                    return result;
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

        public void SYSUserChild_Import(List<SYSUser_Import> lst)
        {
            try
            {
                using (var model = new DataEntities())
                {
                    model.EventAccount = Account; model.EventRunning = false;
                    foreach (var item in lst.Where(c => c.ExcelSuccess))
                    {
                        var obj = model.SYS_User.FirstOrDefault(c => c.ID == item.ID);
                        if (obj == null)
                        {
                            obj = new SYS_User();
                            obj.UserName = item.UserName;
                            obj.Email = item.Email;
                            obj.IsAdmin = false;
                            obj.CreatedBy = Account.UserName;
                            obj.CreatedDate = DateTime.Now;
                        }
                        else
                        {
                            obj.ModifiedBy = Account.UserName;
                            obj.ModifiedDate = DateTime.Now;
                        }
                        if (item.SYSCustomerID > 0)
                            obj.SYSCustomerID = item.SYSCustomerID;
                        else
                            obj.SYSCustomerID = Account.SYSCustomerID;
                        obj.LastName = item.LastName;
                        obj.FirstName = item.FirstName;
                        obj.TelNo = item.TelNo;
                        if (item.GroupID > 0)
                            obj.GroupID = item.GroupID;
                        else
                            obj.GroupID = null;
                        obj.IsApproved = item.IsApproved;
                        obj.ListCustomerID = item.ListCustomerID;
                        obj.Code = string.Empty;
                        if (obj.ID < 1)
                            model.SYS_User.Add(obj);
                    }
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

        public List<DTOCombobox> SYSUserChild_Driver()
        {
            var result = new List<DTOCombobox>();
            try
            {
                using (var model = new DataEntities())
                {
                    result = model.FLM_Driver.Where(c => c.SYSCustomerID == Account.SYSCustomerID).Select(c => new DTOCombobox
                    {
                        ValueInt = c.ID,
                        ValueString = c.ID + "",
                        Text = c.CAT_Driver.FirstName + " " + c.CAT_Driver.LastName,
                    }).ToList();
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

        public DTOFLMDriver FLMDriverChild_Get(int driverID)
        {
            try
            {
                DTOFLMDriver result = new DTOFLMDriver();
                using (var model = new DataEntities())
                {
                    if (driverID > 0)
                    {
                        result = model.FLM_Driver.Where(c => c.ID == driverID).Select(c => new DTOFLMDriver
                        {
                            ID = c.ID,
                            EmployeeCode = c.Code,
                            LastName = c.CAT_Driver.LastName,
                            FirstName = c.CAT_Driver.FirstName,
                            Cellphone = c.CAT_Driver.Cellphone,
                            BiddingID = c.BiddingID,
                            SYSCustomerID = c.SYSCustomerID,
                            CardNumber = c.CAT_Driver.CardNumber,
                            Birthday = c.CAT_Driver.Birthday,
                            Note = c.Note,
                            IsUse = c.IsUse,
                            // DrivingLicenceID = c.DrivingLicenceID,
                            IsAssistant = c.IsAssistant,
                            ListDrivingLicence = c.CAT_Driver.ListDrivingLicence,
                            DriverName = string.Empty,
                            FeeBase = c.FeeBase,
                            Image = c.Image,
                            DateStart = c.DateStart.Value,
                            DateEnd = c.DateEnd.Value
                        }).FirstOrDefault();
                    }
                    else
                    {
                        result.ID = 0;
                        result.IsUse = true;
                        result.DateStart = DateTime.Now;
                        result.DateEnd = null;
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

        public DTOFLMTruck TruckChild_GetByDriver(int driverid)
        {
            try
            {
                DTOFLMTruck result = new DTOFLMTruck();
                using (var model = new DataEntities())
                {
                    if (driverid > 0)
                    {
                        var query = model.FLM_Asset.Where(c => c.CAT_Vehicle.DriverID == driverid).Select(c => new DTOFLMTruck
                        {
                            ID = c.ID,
                            VehicleID = c.VehicleID,
                            GroupOfVehicleID = c.CAT_Vehicle.GroupOfVehicleID,
                            GroupOfVehicleName = c.CAT_Vehicle.GroupOfVehicleID.HasValue ? c.CAT_Vehicle.CAT_GroupOfVehicle.GroupName : string.Empty,
                            YearOfProduction = c.YearOfProduction,
                            Manufactor = c.Manufactor,
                            BaseValue = c.BaseValue,
                            CurrentValue = c.CurrentValue,
                            RemainValue = c.RemainValue,
                            DepreciationPeriod = c.DepreciationPeriod,
                            DepreciationStart = c.DepreciationStart,
                            Specification = c.Specification,
                            IsRent = c.IsRent,
                            RentID = c.RentID,
                            RegNo = c.CAT_Vehicle.RegNo,
                            Note = c.CAT_Vehicle.Note,
                            IsOwn = c.CAT_Vehicle.IsOwn,
                            CurrentVendorID = c.CAT_Vehicle.CurrentVendorID,
                            TypeOfVehicleID = c.CAT_Vehicle.TypeOfVehicleID,
                            MaxWeight = c.CAT_Vehicle.MaxWeight,
                            MaxCapacity = c.CAT_Vehicle.MaxCapacity,
                            RegWeight = c.CAT_Vehicle.RegWeight,
                            RegCapacity = c.CAT_Vehicle.RegCapacity,
                            Lat = c.CAT_Vehicle.Lat,
                            Lng = c.CAT_Vehicle.Lng,
                            MinWeight = c.CAT_Vehicle.MinWeight,
                            MinCapacity = c.CAT_Vehicle.MinCapacity,
                            WarrantyEnd = c.WarrantyEnd,
                            WarrantyPeriod = c.WarrantyPeriod,
                            DriverID = c.CAT_Vehicle.FLM_Driver.ID,
                            AssistantID = c.CAT_Vehicle.FLM_Driver1.ID,
                        });
                        result = query.FirstOrDefault();
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
                throw FaultHelper.ServiceFault(ex);
            }
        }

        #endregion

        #region Comment

        #endregion

        #region File
        public DTOResult File_List(CATTypeOfFileCode code, int id)
        {
            try
            {
                DTOResult result = new DTOResult();
                result.Data = new List<CATFile>();
                using (var model = new DataEntities())
                {
                    var query = model.CAT_File.Where(c => c.SYSCustomerID == Account.SYSCustomerID && c.TypeOfFileID == (int)code && c.ReferID == id && !c.IsDelete).Select(c => new CATFile
                    {
                        ID = c.ID,
                        FileName = c.FileName,
                        FileExt = c.FileExt,
                        FilePath = c.FilePath,
                        ReferID = c.ReferID,
                        TypeOfFileID = c.TypeOfFileID
                    }).ToList();
                    result.Data = query as IEnumerable<CATFile>;
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

        public void File_Save(CATFile item)
        {
            try
            {
                using (var model = new DataEntities())
                {
                    model.EventAccount = Account; model.EventRunning = false;
                    var obj = new CAT_File();
                    obj.FileName = item.FileName;
                    obj.FileExt = item.FileExt;
                    obj.FilePath = item.FilePath;
                    obj.TypeOfFileID = item.TypeOfFileID;
                    obj.ReferID = item.ReferID;
                    obj.IsDelete = false;
                    obj.SYSCustomerID = Account.SYSCustomerID;
                    obj.CreatedBy = Account.UserName;
                    obj.CreatedDate = DateTime.Now;
                    model.CAT_File.Add(obj);
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

        public void File_Delete(int id)
        {
            try
            {
                using (var model = new DataEntities())
                {
                    model.EventAccount = Account; model.EventRunning = false;
                    var obj = model.CAT_File.FirstOrDefault(c => c.ID == id);
                    if (obj != null)
                    {
                        model.CAT_File.Remove(obj);
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
        #endregion

        #region Profile

        public DTOSYSUser UserProfile_Get(int? userID)
        {
            try
            {
                if (!(userID.HasValue && userID > 0))
                    userID = Account.UserID;
                DTOSYSUser obj = new DTOSYSUser();
                var copy = new CopyHelper();
                using (var model = new DataEntities())
                {
                    var sysUser = model.SYS_User.FirstOrDefault(c => c.ID == userID);
                    if (sysUser != null)
                    {
                        copy.Copy(sysUser, obj);
                        obj.FullName = obj.LastName + " " + obj.FirstName;
                    }
                }
                return obj;
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

        public DTOResult UserProfileComment_List(Kendo.Mvc.UI.DataSourceRequest request, int? userID)
        {
            try
            {
                if (!(userID.HasValue && userID > 0))
                    userID = Account.UserID;
                var result = new DTOResult();
                var copy = new CopyHelper();
                using (var model = new DataEntities())
                {
                    //const int iOPSCO = (int)CATTypeOfCommentCode.OPSCO;
                    //const int iOPSDI = (int)CATTypeOfCommentCode.OPSDI;
                    //const int iORDCO = (int)CATTypeOfCommentCode.ORDCO;
                    //const int iORDDI = (int)CATTypeOfCommentCode.ORDDI;
                    var sysUser = model.SYS_User.FirstOrDefault(c => c.ID == userID);
                    if (sysUser != null)
                    {
                        var query = model.CAT_Comment.Where(c => c.UserID == userID).Select(c => new
                        {
                            ID = c.ID,
                            ReferID = c.ReferID,
                            TypeOfCommentID = c.TypeOfCommentID,
                            Comment = c.Comment,
                            Date = c.Date
                        }).ToList();
                        var lstComment = new List<DTOSYSComment>();
                        foreach (var item in query)
                        {
                            var obj = new DTOSYSComment();
                            copy.Copy(item, obj);
                            //switch (item.TypeOfCommentID)
                            //{
                            //    case iOPSCO:
                            //        obj.TypeOfCommentName = "Điều phối xe container";
                            //        var objOPSCO = model.OPS_COTOMaster.FirstOrDefault(c => c.ID == item.ReferID);
                            //        if (objOPSCO != null)
                            //            obj.ReferName = objOPSCO.Code;
                            //        break;
                            //    case iOPSDI:
                            //        obj.TypeOfCommentName = "Điều phối xe tải";
                            //        var objOPSDI = model.OPS_DITOMaster.FirstOrDefault(c => c.ID == item.ReferID);
                            //        if (objOPSDI != null)
                            //            obj.ReferName = objOPSDI.Code;
                            //        break;
                            //    //case iORDCO:
                            //    //    obj.TypeOfCommentName = "Đơn hàng xe container";
                            //    //    var objORDCO = model.ORD_CODetail.FirstOrDefault(c => c.ID == item.ReferID);
                            //    //    if (objORDCO != null)
                            //    //        obj.ReferName = objORDCO.Code;
                            //    //    break;
                            //    //case iORDDI:
                            //    //    obj.TypeOfCommentName = "Đơn hàng xe tải";
                            //    //    var objORDDI = model.ORD_DIOrder.FirstOrDefault(c => c.ID == item.ReferID);
                            //    //    if (objORDDI != null)
                            //    //        obj.ReferName = objORDDI.Code;
                            //    //    break;
                            //    default:
                            //        break;
                            //}
                            lstComment.Add(obj);
                        }
                        result.Data = lstComment.ToDataSourceResult(request).Data as IEnumerable<DTOSYSComment>;
                        result.Total = lstComment.ToDataSourceResult(request).Total;
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


        #endregion

        #region Notification
        public DTOResult Notification_Order()
        {
            try
            {
                DTOResult result = new DTOResult();

                result.Data = new List<ORDOrder>();
                var isAllowed = Account.ListActionCode.Contains(SYSActionCode.ActComment.ToString());
                if (isAllowed)
                {
                    using (var model = new DataEntities())
                    {
                        //var query=model.ORD_Order.Where(c=>c.SYSCustomerID==Account.SYSCustomerID && c.StatusOfOrderID==-(int)SYSVarType.StatusOfOrderNew)
                        //var query = model.CAT_Comment.Where(c => c.SYSCustomerID == Account.SYSCustomerID && c.TypeOfCommentID == (int)code && c.ReferID == id).Select(c => new DTOCATComment
                        //{
                        //    ID = c.ID,
                        //    ReferID = c.ReferID,
                        //    Comment = c.Comment,
                        //    UserID = c.UserID,
                        //    Date = c.Date,
                        //    UserName = c.SYS_User.UserName,
                        //    AvatarPath = c.SYS_User.Image
                        //}).ToList();
                        //result.Data = query as IEnumerable<DTOCATComment>;
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

        public DTOResult Notification_Read(string request)
        {
            try
            {
                DTOResult result = new DTOResult();
                using (var model = new DataEntities())
                {
                    var query = model.WFL_Message.Where(c => c.WFL_Action.TypeOfActionID == (int)WFLTypeOfAction.MessageTMS && c.WFL_Action.UserID == Account.UserID && c.StatusOfMessageID >= -(int)SYSVarType.StatusOfMessageNotified).OrderByDescending(c => c.CreatedDate).Select(c => new DTONotification
                    {
                        ID = c.ID,
                        EventCode = c.WFL_Action.WFL_Event.Code,
                        Message = c.Message,
                        UserID = c.WFL_Action.UserID.Value,
                        CreatedDate = c.CreatedDate,
                        IsUnRead = c.StatusOfMessageID == -(int)SYSVarType.StatusOfMessageNotified,
                    }).ToDataSourceResult(CreateRequest(request));

                    foreach (DTONotification item in query.Data)
                    {
                        string strModule = item.EventCode.Substring(0, 3);
                        switch (strModule)
                        {
                            case "ORD": item.TypeOfEvent = "Đơn hàng"; break;
                            case "OPS": item.TypeOfEvent = "Điều phối"; break;
                            case "MON": item.TypeOfEvent = "Giám sát"; break;
                            case "POD": item.TypeOfEvent = "Chứng từ"; break;
                            default: item.TypeOfEvent = "Khác"; break;
                        }
                    }

                    result.Total = query.Total;
                    result.Data = query.Data as IEnumerable<DTONotification>;
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

        public DTOResult Notification_List(string request)
        {
            try
            {
                DTOResult result = new DTOResult();
                using (var model = new DataEntities())
                {
                    var query = model.WFL_DefineWFMessage.Where(c => c.WFL_DefineWFAction.TypeOfActionID == (int)WFLTypeOfAction.MessageTMS && c.WFL_DefineWFAction.UserID == Account.UserID && c.StatusOfMessageID >= -(int)SYSVarType.StatusOfMessageNotified).OrderByDescending(c => c.CreatedDate).Select(c => new DTONotification
                    {
                        ID = c.ID,
                        EventCode = c.WFL_DefineWFAction.WFL_DefineWFEvent.WFL_WFEvent.Code,
                        Message = c.Message,
                        UserID = c.WFL_DefineWFAction.UserID.Value,
                        CreatedDate = c.CreatedDate,
                        IsUnRead = c.StatusOfMessageID == -(int)SYSVarType.StatusOfMessageNotified,
                    }).ToDataSourceResult(CreateRequest(request));

                    foreach (DTONotification item in query.Data)
                    {
                        string strModule = item.EventCode.Substring(0, 3);
                        switch (strModule)
                        {
                            case "ORD": item.TypeOfEvent = "Đơn hàng"; break;
                            case "OPS": item.TypeOfEvent = "Điều phối"; break;
                            case "MON": item.TypeOfEvent = "Giám sát"; break;
                            case "POD": item.TypeOfEvent = "Chứng từ"; break;
                            default: item.TypeOfEvent = "Khác"; break;
                        }
                    }

                    result.Total = query.Total;
                    result.Data = query.Data as IEnumerable<DTONotification>;
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

        #region UserProfile
        public DTOUserProfile UserProfile_GetUser()
        {
            try
            {
                var result = default(DTOUserProfile);
                if (string.IsNullOrEmpty(Account.UserName))
                    throw new Exception("Account empty");
                using (var model = new DataEntities())
                {
                    string username = Account.UserName;
                    result = model.SYS_User.Where(c => c.UserName == username).Select(c => new DTOUserProfile
                    {
                        ID = c.ID,
                        UserName = Account.UserName,
                        LastName = c.LastName,
                        FirstName = c.FirstName,
                        Code = c.Code,
                        Email = c.Email,
                        Image = c.Image
                    }).FirstOrDefault();
                }
                return result;
            }
            catch (Exception ex)
            {
                throw FaultHelper.BusinessFault(ex);
            }
        }

        public void UserProfile_Save(DTOUserProfile item)
        {
            try
            {
                using (var model = new DataEntities())
                {
                    model.EventAccount = Account; model.EventRunning = false;
                    var obj = model.SYS_User.FirstOrDefault(c => c.ID == item.ID);
                    if (obj != null)
                    {
                        obj.ModifiedBy = Account.UserName;
                        obj.ModifiedDate = DateTime.Now;

                        model.SaveChanges();

                    }
                }

            }
            catch (Exception ex)
            {
                throw FaultHelper.BusinessFault(ex);
            }
        }

        #endregion

        #region Mail Template
        public DTOResult MailTemplate_List(string request)
        {
            try
            {
                DTOResult result = new DTOResult();
                using (var model = new DataEntities())
                {
                    var query = model.CAT_MailTemplate.Where(c => c.SYSCustomerID == Account.SYSCustomerID).Select(c => new CATMailTemplate
                    {
                        ID = c.ID,
                        SYSCustomerID = c.SYSCustomerID,
                        Code = c.Code,
                        TemplateName = c.TemplateName,
                        CC = c.CC,
                        Subject = c.Subject,
                        Content = c.Content,
                        Details = c.Details,
                        Note = c.Note,
                        IsUse = c.IsUse
                    }).ToDataSourceResult(CreateRequest(request));
                    result.Total = query.Total;
                    result.Data = query.Data as IEnumerable<CATMailTemplate>;
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

        public CATMailTemplate MailTemplate_Get(int id)
        {
            try
            {
                var result = new CATMailTemplate { ID = -1 };
                if (id > 0)
                {
                    using (var model = new DataEntities())
                    {
                        result = model.CAT_MailTemplate.Where(c => c.ID == id).Select(c => new CATMailTemplate
                        {
                            ID = c.ID,
                            SYSCustomerID = c.SYSCustomerID,
                            Code = c.Code,
                            TemplateName = c.TemplateName,
                            CC = c.CC,
                            Subject = c.Subject,
                            Content = c.Content,
                            Details = c.Details,
                            Note = c.Note,
                            IsUse = c.IsUse
                        }).FirstOrDefault();
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

        public void MailTemplate_Save(CATMailTemplate item)
        {
            try
            {
                using (var model = new DataEntities())
                {
                    model.EventAccount = Account; model.EventRunning = false;
                    var obj = model.CAT_MailTemplate.FirstOrDefault(c => c.ID == item.ID);
                    if (obj == null)
                    {
                        obj.CreatedBy = Account.UserName;
                        obj.CreatedDate = DateTime.Now;
                        obj.SYSCustomerID = Account.SYSCustomerID;
                        model.CAT_MailTemplate.Add(obj);
                    }
                    else
                    {
                        obj.ModifiedBy = Account.UserName;
                        obj.ModifiedDate = DateTime.Now;
                    }
                    obj.TemplateName = item.TemplateName;
                    obj.CC = item.CC;
                    obj.Subject = item.Subject;
                    obj.Content = item.Content;
                    obj.Details = item.Details;
                    obj.Note = item.Note;
                    obj.IsUse = item.IsUse;

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

        public void MailTemplate_Delete(CATMailTemplate item)
        {
            try
            {
                using (var model = new DataEntities())
                {
                    model.EventAccount = Account; model.EventRunning = false;
                    var obj = model.CAT_MailTemplate.FirstOrDefault(c => c.ID == item.ID);
                    if (obj != null)
                    {
                        model.CAT_MailTemplate.Remove(obj);
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

        public CATMailTemplate MailTemplate_GetBySYSCustomerID(int syscustomerID)
        {
            try
            {
                CATMailTemplate result = new CATMailTemplate();
                using (var model = new DataEntities())
                {
                    var obj = model.CAT_MailTemplate.FirstOrDefault(c => c.SYSCustomerID == syscustomerID);
                    if (obj != null)
                    {
                        using (var copy = new CopyHelper())
                        {
                            copy.Copy(obj, result);
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

        public CATMailTemplate MailTemplate_GetByCurrentSYSCustomerID()
        {
            try
            {
                CATMailTemplate result = new CATMailTemplate();
                using (var model = new DataEntities())
                {
                    var obj = model.CAT_MailTemplate.FirstOrDefault(c => c.SYSCustomerID == Account.SYSCustomerID);
                    if (obj != null)
                    {
                        using (var copy = new CopyHelper())
                        {
                            copy.Copy(obj, result);
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
        #endregion

        #region SYSResource
        //language 0: SYS_Resource, >0 : SYS_ResourceLang // 1:vi-vn,2:en-US
        public DTOResult SYSResource_List(string request, int Language)
        {
            try
            {
                DTOResult result = new DTOResult();
                using (var model = new DataEntities())
                {
                    string key = string.Empty;
                    switch (Language)
                    {
                        default: key = string.Empty;
                            break;
                        case 1: key = "vi-VN"; break;
                        case 2: key = "en-US"; break;
                    }
                    if (Language <= 0)
                    {
                        var query = model.SYS_Resource.Where(c => c.SYSCustomerID == Account.SYSCustomerID).Select(c => new DTOSYSResource
                        {
                            ID = c.ID,
                            ResourceID = c.ID,
                            Key = c.Key,
                            Name = c.Name,
                            ShortName = c.ShortName,
                            IsDefault = c.IsDefault
                        }).ToDataSourceResult(CreateRequest(request));
                        result.Total = query.Total;
                        result.Data = query.Data as IEnumerable<DTOSYSResource>;
                    }
                    else
                    {
                        var query = model.SYS_ResourceLang.Where(c => c.SYS_Resource.SYSCustomerID == Account.SYSCustomerID && c.Language == key).Select(c => new DTOSYSResource
                        {
                            ID = c.ID,
                            ResourceID = c.ResourceID,
                            Key = c.SYS_Resource.Key,
                            Name = c.Name,
                            ShortName = c.ShortName,
                            IsDefault = c.SYS_Resource.IsDefault
                        }).ToDataSourceResult(CreateRequest(request));
                        result.Total = query.Total;
                        result.Data = query.Data as IEnumerable<DTOSYSResource>;
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

        public DTOSYSResource SYSResource_Get(int id, int Language)
        {
            try
            {
                var result = new DTOSYSResource { ID = 0, ResourceID = 0, Key = string.Empty, Name = string.Empty, ShortName = string.Empty, IsDefault = false };
                if (id > 0)
                {
                    string key = string.Empty;
                    switch (Language)
                    {
                        default: key = string.Empty;
                            break;
                        case 1: key = "vi-VN"; break;
                        case 2: key = "en-US"; break;
                    }
                    using (var model = new DataEntities())
                    {
                        if (Language <= 0)
                        {
                            result = model.SYS_Resource.Where(c => c.ID == id).Select(c => new DTOSYSResource
                            {
                                ID = c.ID,
                                ResourceID = c.ID,
                                Key = c.Key,
                                Name = c.Name,
                                ShortName = c.ShortName,
                                IsDefault = c.IsDefault
                            }).FirstOrDefault();
                        }
                        else
                        {
                            result = model.SYS_ResourceLang.Where(c => c.ID == id).Select(c => new DTOSYSResource
                            {
                                ID = c.ID,
                                ResourceID = c.ResourceID,
                                Key = c.SYS_Resource.Key,
                                Name = c.Name,
                                ShortName = c.ShortName,
                                IsDefault = c.SYS_Resource.IsDefault
                            }).FirstOrDefault();
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

        public void SYSResource_Save(DTOSYSResource item, int Language)
        {
            try
            {
                using (var model = new DataEntities())
                {
                    string key = string.Empty;
                    switch (Language)
                    {
                        default: key = string.Empty;
                            break;
                        case 1: key = "vi-VN"; break;
                        case 2: key = "en-US"; break;
                    }

                    model.EventAccount = Account; model.EventRunning = false;
                    if (Language <= 0)
                    {
                        #region ngon ngu mac dinh
                        if (model.SYS_Resource.Count(c => c.SYSCustomerID == Account.SYSCustomerID && c.ID != item.ID && c.Key == item.Key) > 0)
                            throw new Exception("Mã [" + item.Key + "] đã sử dụng");
                        if (string.IsNullOrEmpty(item.Key) || string.IsNullOrEmpty(item.Name) || string.IsNullOrEmpty(item.ShortName))
                            throw new Exception("Mã, Tên, Tên ngắn không được trống");
                        var obj = model.SYS_Resource.FirstOrDefault(c => c.ID == item.ID);
                        if (obj == null)
                        {
                            obj = new SYS_Resource();
                            obj.CreatedBy = Account.UserName;
                            obj.CreatedDate = DateTime.Now;
                            obj.SYSCustomerID = Account.SYSCustomerID;
                            model.SYS_Resource.Add(obj);
                        }
                        else
                        {
                            obj.ModifiedBy = Account.UserName;
                            obj.ModifiedDate = DateTime.Now;
                        }
                        obj.Key = item.Key;
                        obj.Name = item.Name;
                        obj.ShortName = item.ShortName;
                        obj.IsDefault = item.IsDefault;
                        //them moi thì add cho all user  
                        if (obj.ID < 1)
                        {
                            var listUserID = model.SYS_User.Select(c => c.ID).Distinct().ToList();
                            foreach (var userid in listUserID)
                            {
                                SYS_UserResource objUser = new SYS_UserResource();
                                objUser.CreatedBy = Account.UserName;
                                objUser.CreatedDate = DateTime.Now;
                                objUser.UserID = userid;
                                objUser.Name = item.Name;
                                objUser.ShortName = item.ShortName;
                                objUser.SYS_Resource = obj;
                                model.SYS_UserResource.Add(objUser);
                            }

                            List<string> lstLang = new List<string> { "vi-VN", "en-US" };
                            foreach (var lang in lstLang)
                            {
                                SYS_ResourceLang objLang = new SYS_ResourceLang();
                                objLang.CreatedBy = Account.UserName;
                                objLang.CreatedDate = DateTime.Now;
                                objLang.Language = lang.Trim();
                                objLang.Name = item.Name;
                                objLang.ShortName = item.ShortName;
                                objLang.SYS_Resource = obj;
                                model.SYS_ResourceLang.Add(objLang);
                            }
                        }
                        model.SaveChanges();
                        #endregion
                    }
                    else
                    {
                        #region luu theo ngon ngu
                        if (model.SYS_Resource.Count(c => c.SYSCustomerID == Account.SYSCustomerID && c.ID != item.ID && c.Key == item.Key) > 0 && item.ID <= 0)
                            throw new Exception("Mã [" + item.Key + "] đã sử dụng");
                        if (string.IsNullOrEmpty(item.Key) || string.IsNullOrEmpty(item.Name) || string.IsNullOrEmpty(item.ShortName))
                            throw new Exception("Mã, Tên, Tên ngắn không được trống");

                        var obj = model.SYS_Resource.FirstOrDefault(c => c.ID == item.ResourceID);
                        if (obj == null)
                        {
                            obj = new SYS_Resource();
                            obj.CreatedBy = Account.UserName;
                            obj.CreatedDate = DateTime.Now;
                            obj.SYSCustomerID = Account.SYSCustomerID;
                            obj.Key = item.Key;
                            obj.Name = item.Name;
                            obj.ShortName = item.ShortName;
                            obj.IsDefault = item.IsDefault;
                            model.SYS_Resource.Add(obj);
                        }

                        var objLang = model.SYS_ResourceLang.FirstOrDefault(c => c.ID == item.ID);
                        if (objLang == null)
                        {
                            objLang = new SYS_ResourceLang();
                            objLang.CreatedBy = Account.UserName;
                            objLang.CreatedDate = DateTime.Now;
                            objLang.Language = key.Trim();
                            objLang.SYS_Resource = obj;
                            model.SYS_ResourceLang.Add(objLang);
                        }
                        else
                        {
                            objLang.ModifiedBy = Account.UserName;
                            objLang.ModifiedDate = DateTime.Now;
                        }
                        objLang.Name = item.Name;
                        objLang.ShortName = item.ShortName;
                        model.SaveChanges();
                        #endregion
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

        public void SYSResource_Delete(DTOSYSResource item, int Language)
        {
            try
            {
                using (var model = new DataEntities())
                {
                    string key = string.Empty;
                    switch (Language)
                    {
                        default: key = string.Empty;
                            break;
                        case 1: key = "vi-VN"; break;
                        case 2: key = "en-US"; break;
                    }

                    model.EventAccount = Account; model.EventRunning = false;
                    if (Language <= 0)
                    {
                        var obj = model.SYS_Resource.FirstOrDefault(c => c.ID == item.ID);
                        if (obj == null)
                            throw FaultHelper.BusinessFault(null, null, "Không tìm thấy dữ liệu SYS_Resource");
                        foreach (var detail in model.SYS_UserResource.Where(c => c.ResourceID == obj.ID))
                            model.SYS_UserResource.Remove(detail);
                        foreach (var detail in model.SYS_ResourceLang.Where(c => c.ResourceID == obj.ID))
                            model.SYS_ResourceLang.Remove(detail);
                        model.SYS_Resource.Remove(obj);
                        model.SaveChanges();
                    }
                    else
                    {
                        var objLang = model.SYS_ResourceLang.FirstOrDefault(c => c.ID == item.ID);
                        if (objLang == null)
                            throw FaultHelper.BusinessFault(null, null, "Không tìm thấy dữ liệu SYS_ResourceLang");
                        model.SYS_ResourceLang.Remove(objLang);
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

        public List<DTOSYSResource> SYSResource_Data(int Language)
        {
            try
            {
                List<DTOSYSResource> result = new List<DTOSYSResource>();
                using (var model = new DataEntities())
                {
                    string key = string.Empty;
                    switch (Language)
                    {
                        default: key = string.Empty;
                            break;
                        case 1: key = "vi-VN"; break;
                        case 2: key = "en-US"; break;
                    }
                    if (Language <= 0)
                    {
                        result = model.SYS_Resource.Where(c => c.SYSCustomerID == Account.SYSCustomerID).Select(c => new DTOSYSResource
                        {
                            ID = c.ID,
                            ResourceID = c.ID,
                            Key = c.Key,
                            Name = c.Name,
                            ShortName = c.ShortName,
                            IsDefault = c.IsDefault
                        }).ToList();
                    }
                    else
                    {
                        result = model.SYS_ResourceLang.Where(c => c.SYS_Resource.SYSCustomerID == Account.SYSCustomerID && c.Language == key).Select(c => new DTOSYSResource
                        {
                            ID = c.ID,
                            ResourceID = c.ResourceID,
                            Key = c.SYS_Resource.Key,
                            Name = c.Name,
                            ShortName = c.ShortName,
                            IsDefault = c.SYS_Resource.IsDefault
                        }).ToList();
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

        public void SYSResource_Import(List<DTOSYSResourceImport> lst, int Language)
        {
            try
            {
                using (var model = new DataEntities())
                {


                    model.EventAccount = Account; model.EventRunning = false;
                    if (Language <= 0)
                    {
                        #region SYS_Resource SYS_UserResource
                        var listUserID = model.SYS_User.Select(c => c.ID).Distinct().ToList();
                        List<string> lstLang = new List<string> { "vi-VN", "en-US" };
                        foreach (var item in lst.Where(c => c.ExcelSuccess))
                        {
                            if (string.IsNullOrEmpty(item.Key) || string.IsNullOrEmpty(item.Name))
                                throw new Exception("Mã và Tên không được trống");
                            var obj = model.SYS_Resource.FirstOrDefault(c => c.ID == item.ID);
                            if (obj == null)
                            {
                                obj = new SYS_Resource();
                                obj.CreatedBy = Account.UserName;
                                obj.CreatedDate = DateTime.Now;
                                model.SYS_Resource.Add(obj);
                            }
                            else
                            {
                                obj.ModifiedBy = Account.UserName;
                                obj.ModifiedDate = DateTime.Now;
                            }
                            obj.Key = item.Key;
                            obj.Name = item.Name;
                            obj.ShortName = item.ShortName;
                            obj.IsDefault = item.IsDefault;
                            //them moi thì add cho all user  
                            if (obj.ID < 1)
                            {
                                foreach (var userid in listUserID)
                                {
                                    SYS_UserResource objUser = new SYS_UserResource();
                                    objUser.CreatedBy = Account.UserName;
                                    objUser.CreatedDate = DateTime.Now;
                                    objUser.UserID = userid;
                                    objUser.Name = item.Name;
                                    objUser.ShortName = item.ShortName;
                                    objUser.SYS_Resource = obj;
                                    model.SYS_UserResource.Add(objUser);
                                }

                                foreach (var lang in lstLang)
                                {
                                    SYS_ResourceLang objLang = new SYS_ResourceLang();
                                    objLang.CreatedBy = Account.UserName;
                                    objLang.CreatedDate = DateTime.Now;
                                    objLang.Language = lang.Trim();
                                    objLang.Name = item.Name;
                                    objLang.ShortName = item.ShortName;
                                    objLang.SYS_Resource = obj;
                                    model.SYS_ResourceLang.Add(objLang);
                                }
                            }
                        }
                        model.SaveChanges();
                        #endregion
                    }
                    else
                    {
                        #region luu theo ngon ngu SYS_Resource SYS_ResourceLang
                        string key = string.Empty;
                        switch (Language)
                        {
                            default: key = string.Empty;
                                break;
                            case 1: key = "vi-VN"; break;
                            case 2: key = "en-US"; break;
                        }

                        foreach (var item in lst.Where(c => c.ExcelSuccess))
                        {
                            var obj = model.SYS_Resource.FirstOrDefault(c => c.ID == item.ResourceID);
                            if (obj == null)
                            {
                                obj = new SYS_Resource();
                                obj.CreatedBy = Account.UserName;
                                obj.CreatedDate = DateTime.Now;
                                obj.Key = item.Key;
                                obj.Name = item.Name;
                                obj.ShortName = item.ShortName;
                                obj.IsDefault = item.IsDefault;
                                model.SYS_Resource.Add(obj);
                            }

                            var objLang = model.SYS_ResourceLang.FirstOrDefault(c => c.ID == item.ID);
                            if (objLang == null)
                            {
                                objLang = new SYS_ResourceLang();
                                objLang.CreatedBy = Account.UserName;
                                objLang.CreatedDate = DateTime.Now;
                                objLang.Language = key.Trim();
                                objLang.SYS_Resource = obj;
                                model.SYS_ResourceLang.Add(objLang);
                            }
                            else
                            {
                                objLang.ModifiedBy = Account.UserName;
                                objLang.ModifiedDate = DateTime.Now;
                            }
                            objLang.Name = item.Name;
                            objLang.ShortName = item.ShortName;
                        }
                        model.SaveChanges();
                        #endregion
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

        public void SYSResource_ChangeDefault(int Language)
        {
            try
            {
                string key = string.Empty;
                switch (Language)
                {
                    default: key = string.Empty;
                        break;
                    case 1: key = "vi-VN"; break;
                    case 2: key = "en-US"; break;
                }
                using (var model = new DataEntities())
                {
                    foreach (var item in model.SYS_ResourceLang.Where(c => c.Language == key))
                    {
                        item.SYS_Resource.Name = item.Name;
                        item.SYS_Resource.ShortName = item.ShortName;
                        item.SYS_Resource.ModifiedBy = Account.UserName;
                        item.SYS_Resource.ModifiedDate = DateTime.Now;
                    }
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

        #region SYSUserResource
        public DTOResult SYSUserResource_List(string request)
        {
            try
            {
                DTOResult result = new DTOResult();
                using (var model = new DataEntities())
                {
                    var query = model.SYS_UserResource.Where(c => c.UserID == Account.UserID.Value).Select(c => new DTOSYSUserResource
                    {
                        ID = c.ID,
                        SYSKey = c.SYS_Resource.Key,
                        SYSName = c.SYS_Resource.Name,
                        SYSShortName = c.SYS_Resource.ShortName,
                        Name = c.Name,
                        UserID = c.UserID,
                        ResourceID = c.ResourceID,
                        ShortName = c.ShortName
                    }).ToDataSourceResult(CreateRequest(request));
                    result.Total = query.Total;
                    result.Data = query.Data as IEnumerable<DTOSYSUserResource>;
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

        public DTOSYSUserResource SYSUserResource_Get(int id)
        {
            try
            {
                var result = new DTOSYSUserResource { ID = 0 };
                if (id > 0)
                {
                    using (var model = new DataEntities())
                    {
                        result = model.SYS_UserResource.Where(c => c.ID == id).Select(c => new DTOSYSUserResource
                        {
                            ID = c.ID,
                            SYSKey = c.SYS_Resource.Key,
                            SYSName = c.SYS_Resource.Name,
                            SYSShortName = c.SYS_Resource.ShortName,
                            Name = c.Name,
                            UserID = c.UserID,
                            ResourceID = c.ResourceID,
                            ShortName = c.ShortName
                        }).FirstOrDefault();
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

        public void SYSUserResource_Save(DTOSYSUserResource item)
        {
            try
            {
                using (var model = new DataEntities())
                {
                    model.EventAccount = Account; model.EventRunning = false;
                    var obj = model.SYS_UserResource.FirstOrDefault(c => c.ID == item.ID);
                    if (obj != null)
                    {
                        obj.ModifiedBy = Account.UserName;
                        obj.ModifiedDate = DateTime.Now;
                        obj.Name = string.IsNullOrEmpty(item.Name) ? string.Empty : item.Name;
                        obj.ShortName = string.IsNullOrEmpty(item.ShortName) ? string.Empty : item.ShortName;
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

        public List<DTOSYSUserResource> SYSUserResource_Data()
        {
            try
            {
                List<DTOSYSUserResource> result = new List<DTOSYSUserResource>();
                using (var model = new DataEntities())
                {
                    result = model.SYS_UserResource.Where(c => c.UserID == Account.UserID).Select(c => new DTOSYSUserResource
                    {
                        ID = c.ID,
                        SYSKey = c.SYS_Resource.Key,
                        SYSName = c.SYS_Resource.Name,
                        SYSShortName = c.SYS_Resource.ShortName,
                        Name = c.Name,
                        UserID = c.UserID,
                        ResourceID = c.ResourceID,
                        ShortName = c.ShortName
                    }).ToList();
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

        public void SYSUserResource_Import(List<DTOSYSUserResourceImport> lst)
        {
            try
            {
                using (var model = new DataEntities())
                {
                    model.EventAccount = Account; model.EventRunning = false;
                    foreach (var item in lst.Where(c => c.ExcelSuccess))
                    {
                        var obj = model.SYS_UserResource.FirstOrDefault(c => c.ID == item.ID);
                        if (obj != null)
                        {
                            obj.Name = string.IsNullOrEmpty(item.Name) ? string.Empty : item.Name;
                            obj.ShortName = string.IsNullOrEmpty(item.ShortName) ? string.Empty : item.ShortName;
                        }
                    }
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

        public void SYSUserResource_SetDefault()
        {
            try
            {
                using (var model = new DataEntities())
                {
                    model.EventAccount = Account; model.EventRunning = false;

                    foreach (var item in model.SYS_UserResource.Where(c => c.UserID == Account.UserID))
                        model.SYS_UserResource.Remove(item);
                    model.SaveChanges();

                    var lst = model.SYS_Resource.Select(c => new { c.ID, c.Name, c.ShortName }).ToList();
                    foreach (var item in lst)
                    {
                        var obj = new SYS_UserResource();
                        obj.UserID = Account.UserID.Value;
                        obj.ResourceID = item.ID;
                        obj.Name = item.Name;
                        obj.ShortName = string.IsNullOrEmpty(item.ShortName) ? string.Empty : item.ShortName;
                        obj.CreatedBy = Account.UserName;
                        obj.CreatedDate = DateTime.Now;
                        model.SYS_UserResource.Add(obj);
                    }
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

        public void SYSUserResource_SetDefaultByUser(int userID)
        {
            try
            {
                using (var model = new DataEntities())
                {
                    model.EventAccount = Account; model.EventRunning = false;

                    foreach (var item in model.SYS_UserResource.Where(c => c.UserID == userID))
                        model.SYS_UserResource.Remove(item);
                    model.SaveChanges();

                    var lst = model.SYS_Resource.Select(c => new { c.ID, c.Name, c.ShortName }).ToList();
                    foreach (var item in lst)
                    {
                        var obj = new SYS_UserResource();
                        obj.UserID = userID;
                        obj.ResourceID = item.ID;
                        obj.Name = item.Name;
                        obj.ShortName = string.IsNullOrEmpty(item.ShortName) ? string.Empty : item.ShortName;
                        obj.CreatedBy = Account.UserName;
                        obj.CreatedDate = DateTime.Now;
                        model.SYS_UserResource.Add(obj);
                    }
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

        #region Dashboard
        public Dashboard_UserSetting Dashboard_UserSetting_Get(int functionID)
        {
            try
            {
                var result = new Dashboard_UserSetting();
                result.ListChart = new List<Dashboard_UserSetting_Chart>();
                result.ListWidget = new List<Dashboard_UserSetting_Chart>();
                using (var model = new DataEntities())
                {
                    result = HelperSYSSetting.SYSUserSetting_ORDDashboard_Get(model, Account, functionID);
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

        public void Dashboard_UserSetting_Save(Dashboard_UserSetting item)
        {
            try
            {
                using (var model = new DataEntities())
                {
                    HelperSYSSetting.SYSUserSetting_ORDDashboard_Save(model, Account, item);
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

        // Common
        public List<MAP_Vehicle> Dashboard_Truck_List()
        {
            try
            {
                var result = new List<MAP_Vehicle>();
                using (var model = new DataEntities())
                {
                    result = model.CUS_Vehicle.Where(c => c.CustomerID == Account.SYSCustomerID && c.VehicleID > 2 && (c.CAT_Vehicle.TypeOfVehicleID == -(int)SYSVarType.TypeOfVehicleTruck)).Select(c => new MAP_Vehicle
                        {
                            ID = c.ID,
                            Code = c.CAT_Vehicle.RegNo,
                        }).ToList();
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

        public List<MAP_Vehicle> Dashboard_Tractor_List()
        {
            try
            {
                var result = new List<MAP_Vehicle>();
                using (var model = new DataEntities())
                {
                    result = model.CUS_Vehicle.Where(c => c.CustomerID == Account.SYSCustomerID && c.VehicleID > 2 && (c.CAT_Vehicle.TypeOfVehicleID == -(int)SYSVarType.TypeOfVehicleTractor)).Select(c => new MAP_Vehicle
                    {
                        ID = c.ID,
                        Code = c.CAT_Vehicle.RegNo,
                    }).ToList();
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

        // Chart 1: Tổng đơn hàng
        public Chart_Summary Chart_Summary_Data(DateTime dtfrom, DateTime dtto, int? customerid)
        {
            try
            {
                var result = new Chart_Summary();
                result.ListData = new List<Chart_Summary_Order>();
                using (var model = new DataEntities())
                {
                    dtfrom = dtfrom.Date;
                    dtto = dtto.Date.AddDays(1);

                    List<int> lstCustomerID = new List<int>();
                    string ViewAdmin = SYSViewCode.ViewAdmin.ToString();
                    if (customerid.HasValue)
                        lstCustomerID.Add(customerid.Value);
                    else
                        lstCustomerID = model.CUS_Customer.Where(c => (c.TypeOfCustomerID == -(int)SYSVarType.TypeOfCustomerCUS || c.TypeOfCustomerID == -(int)SYSVarType.TypeOfCustomerBOTH) && !c.IsSystem && (Account.ListActionCode.Contains(ViewAdmin) || Account.ListCustomerID.Contains(c.ID))).Select(c => c.ID).ToList();

                    var lstOrder = model.ORD_Order.Where(c => c.SYSCustomerID == Account.SYSCustomerID && c.RequestDate >= dtfrom && c.RequestDate < dtto && lstCustomerID.Contains(c.CustomerID)).Select(c => new { c.ID, c.CAT_TransportMode.TransportModeID, c.RequestDate }).ToList();
                    while (dtfrom < dtto)
                    {
                        Chart_Summary_Order item = new Chart_Summary_Order();
                        item.Date = dtfrom;
                        item.TotalLTL = lstOrder.Count(c => c.RequestDate.Date == dtfrom && c.TransportModeID == -(int)SYSVarType.TransportModeLTL);
                        item.TotalFTL = lstOrder.Count(c => c.RequestDate.Date == dtfrom && c.TransportModeID == -(int)SYSVarType.TransportModeFTL);
                        item.TotalFCL = lstOrder.Count(c => c.RequestDate.Date == dtfrom && c.TransportModeID == -(int)SYSVarType.TransportModeFCL);
                        item.TotalLCL = lstOrder.Count(c => c.RequestDate.Date == dtfrom && c.TransportModeID == -(int)SYSVarType.TransportModeLCL);
                        result.ListData.Add(item);

                        dtfrom = dtfrom.AddDays(1);
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

        // Chart 2: Đơn hàng theo khách hàng
        public Chart_Customer_Order Chart_Customer_Order_Data(DateTime dtfrom, DateTime dtto, int? customerid)
        {
            try
            {
                var result = new Chart_Customer_Order();
                result.ListLTL = new List<Chart_Customer_Order_Truck_LTL>();
                result.ListFTL = new List<Chart_Customer_Order_Truck_FTL>();
                result.ListFCL = new List<Chart_Customer_Order_Container>();
                result.ListLCL = new List<Chart_Customer_Order_Container>();
                using (var model = new DataEntities())
                {
                    dtfrom = dtfrom.Date;
                    dtto = dtto.Date.AddDays(1);

                    string ViewAdmin = SYSViewCode.ViewAdmin.ToString();
                    var lstCustomerID = model.CUS_Customer.Where(c => (c.TypeOfCustomerID == -(int)SYSVarType.TypeOfCustomerCUS || c.TypeOfCustomerID == -(int)SYSVarType.TypeOfCustomerBOTH) && !c.IsSystem && (Account.ListActionCode.Contains(ViewAdmin) || Account.ListCustomerID.Contains(c.ID))).Select(c => c.ID).ToList();

                    var lstOrder = model.ORD_Order.Where(c => c.SYSCustomerID == Account.SYSCustomerID && c.RequestDate >= dtfrom && c.RequestDate < dtto && (customerid > 0 ? c.CustomerID == customerid : lstCustomerID.Contains(c.CustomerID))).Select(c => new { c.ID, c.CAT_TransportMode.TransportModeID, c.RequestDate }).ToList();
                    var lstGroup = model.ORD_GroupProduct.Where(c => c.ORD_Order.SYSCustomerID == Account.SYSCustomerID && c.ORD_Order.RequestDate >= dtfrom && c.ORD_Order.RequestDate < dtto && (customerid > 0 ? c.ORD_Order.CustomerID == customerid : lstCustomerID.Contains(c.ORD_Order.CustomerID))).Select(c => new { c.ID, c.Ton, c.CBM, c.OrderID, c.ORD_Order.RequestDate, c.ORD_Order.CAT_TransportMode.TransportModeID }).ToList();
                    var lstContainer = model.ORD_Container.Where(c => c.ORD_Order.SYSCustomerID == Account.SYSCustomerID && c.ORD_Order.RequestDate >= dtfrom && c.ORD_Order.RequestDate < dtto && (customerid > 0 ? c.ORD_Order.CustomerID == customerid : lstCustomerID.Contains(c.ORD_Order.CustomerID))).Select(c => new { c.ID, c.PackingID, c.ORD_Order.RequestDate, c.ORD_Order.CAT_TransportMode.TransportModeID }).ToList();

                    while (dtfrom < dtto)
                    {
                        result.ListLTL.Add(new Chart_Customer_Order_Truck_LTL
                        {
                            CustomerID = customerid > 0 ? customerid.Value : 0,
                            Date = dtfrom.Date,
                            Ton = lstGroup.Count(c => c.RequestDate.Date == dtfrom.Date && c.TransportModeID == -(int)SYSVarType.TransportModeLTL) > 0 ? lstGroup.Where(c => c.RequestDate.Date == dtfrom.Date && c.TransportModeID == -(int)SYSVarType.TransportModeLTL).Sum(c => c.Ton) : 0,
                            CBM = lstGroup.Count(c => c.RequestDate.Date == dtfrom.Date && c.TransportModeID == -(int)SYSVarType.TransportModeLTL) > 0 ? lstGroup.Where(c => c.RequestDate.Date == dtfrom.Date && c.TransportModeID == -(int)SYSVarType.TransportModeLTL).Sum(c => c.CBM) : 0,
                        });

                        result.ListFTL.Add(new Chart_Customer_Order_Truck_FTL
                        {
                            CustomerID = customerid > 0 ? customerid.Value : 0,
                            Date = dtfrom.Date,
                            Total = lstOrder.Count(c => c.RequestDate.Date == dtfrom.Date && c.TransportModeID == -(int)SYSVarType.TransportModeFTL)
                        });
                        result.ListFCL.Add(new Chart_Customer_Order_Container
                        {
                            CustomerID = customerid > 0 ? customerid.Value : 0,
                            Date = dtfrom.Date,
                            Total20DC = lstContainer.Count(c => c.RequestDate.Date == dtfrom.Date && c.TransportModeID == -(int)SYSVarType.TransportModeFCL && c.PackingID == (int)CATPackingCOCode.CO20),
                            Total40DC = lstContainer.Count(c => c.RequestDate.Date == dtfrom.Date && c.TransportModeID == -(int)SYSVarType.TransportModeFCL && c.PackingID == (int)CATPackingCOCode.CO40),
                            Total40HC = lstContainer.Count(c => c.RequestDate.Date == dtfrom.Date && c.TransportModeID == -(int)SYSVarType.TransportModeFCL && c.PackingID == (int)CATPackingCOCode.CO40H),
                        });

                        result.ListLCL.Add(new Chart_Customer_Order_Container
                        {
                            CustomerID = customerid > 0 ? customerid.Value : 0,
                            Date = dtfrom.Date,
                            Total20DC = lstContainer.Count(c => c.RequestDate.Date == dtfrom.Date && c.TransportModeID == -(int)SYSVarType.TransportModeLCL && c.PackingID == (int)CATPackingCOCode.CO20),
                            Total40DC = lstContainer.Count(c => c.RequestDate.Date == dtfrom.Date && c.TransportModeID == -(int)SYSVarType.TransportModeLCL && c.PackingID == (int)CATPackingCOCode.CO40),
                            Total40HC = lstContainer.Count(c => c.RequestDate.Date == dtfrom.Date && c.TransportModeID == -(int)SYSVarType.TransportModeLCL && c.PackingID == (int)CATPackingCOCode.CO40H),
                        });

                        dtfrom = dtfrom.AddDays(1);
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

        // Chart 3: Tình hình vận chuyển
        public Chart_Customer_OPS Chart_Customer_OPS_Data(DateTime dtfrom, DateTime dtto, int? customerid, int statusid)
        {
            try
            {
                var result = new Chart_Customer_OPS();
                result.ListLTL = new List<Chart_Customer_OPS_Truck_LTL>();
                result.ListFTL = new List<Chart_Customer_OPS_Truck_FTL>();
                result.ListFCL = new List<Chart_Customer_OPS_Container>();
                result.ListLCL = new List<Chart_Customer_OPS_Container>();
                using (var model = new DataEntities())
                {
                    dtfrom = dtfrom.Date;
                    dtto = dtto.Date.AddDays(1);

                    string ViewAdmin = SYSViewCode.ViewAdmin.ToString();
                    var lstCustomerID = model.CUS_Customer.Where(c => (c.TypeOfCustomerID == -(int)SYSVarType.TypeOfCustomerCUS || c.TypeOfCustomerID == -(int)SYSVarType.TypeOfCustomerBOTH) && !c.IsSystem && (Account.ListActionCode.Contains(ViewAdmin) || Account.ListCustomerID.Contains(c.ID))).Select(c => c.ID).ToList();

                    var lstOrder = model.ORD_Order.Where(c => c.SYSCustomerID == Account.SYSCustomerID && c.RequestDate >= dtfrom && c.RequestDate < dtto && (customerid > 0 ? c.CustomerID == customerid : lstCustomerID.Contains(c.CustomerID))).Select(c => new { c.ID, c.CAT_TransportMode.TransportModeID, c.RequestDate }).ToList();
                    var lstGroup = model.ORD_GroupProduct.Where(c => c.ORD_Order.SYSCustomerID == Account.SYSCustomerID && c.ORD_Order.RequestDate >= dtfrom && c.ORD_Order.RequestDate < dtto && (customerid > 0 ? c.ORD_Order.CustomerID == customerid : lstCustomerID.Contains(c.ORD_Order.CustomerID))).Select(c => new { c.ID, c.Ton, c.CBM, c.OrderID, c.ORD_Order.RequestDate, c.ORD_Order.CAT_TransportMode.TransportModeID }).ToList();
                    var lstContainer = model.ORD_Container.Where(c => c.ORD_Order.SYSCustomerID == Account.SYSCustomerID && c.ORD_Order.RequestDate >= dtfrom && c.ORD_Order.RequestDate < dtto && (customerid > 0 ? c.ORD_Order.CustomerID == customerid : lstCustomerID.Contains(c.ORD_Order.CustomerID))).Select(c => new { c.ID, c.PackingID, c.ORD_Order.RequestDate, c.ORD_Order.CAT_TransportMode.TransportModeID }).ToList();
                    var lstOPSGroup = model.OPS_DITOGroupProduct.Where(c => c.ORD_GroupProduct.ORD_Order.SYSCustomerID == Account.SYSCustomerID && c.ORD_GroupProduct.ORD_Order.RequestDate >= dtfrom && c.ORD_GroupProduct.ORD_Order.RequestDate < dtto && (customerid > 0 ? c.ORD_GroupProduct.ORD_Order.CustomerID == customerid : lstCustomerID.Contains(c.ORD_GroupProduct.ORD_Order.CustomerID))).Select(c => new { c.TonTranfer, c.CBMTranfer, c.QuantityTranfer, c.ORD_GroupProduct.ORD_Order.CustomerID, StatusOfDITOMasterID = c.DITOMasterID > 0 ? c.OPS_DITOMaster.StatusOfDITOMasterID : -1, c.DITOGroupProductStatusID, c.DITOGroupProductStatusPODID, c.ORD_GroupProduct.ORD_Order.RequestDate, c.ORD_GroupProduct.OrderID, c.ORD_GroupProduct.ORD_Order.CAT_TransportMode.TransportModeID, c.DITOMasterID, c.ORD_GroupProduct.ORD_Order.StatusOfOrderID }).ToList();
                    var lstOPSContainer = model.OPS_COTOContainer.Where(c => c.OPS_Container.ORD_Container.ORD_Order.SYSCustomerID == Account.SYSCustomerID && c.OPS_Container.ORD_Container.ORD_Order.RequestDate >= dtfrom && c.OPS_Container.ORD_Container.ORD_Order.RequestDate < dtto && (customerid > 0 ? c.OPS_Container.ORD_Container.ORD_Order.CustomerID == customerid : lstCustomerID.Contains(c.OPS_Container.ORD_Container.ORD_Order.CustomerID))).Select(c => new { c.OPS_Container.ContainerID, c.COTOMasterID, c.OPS_Container.ORD_Container.ORD_Order.CustomerID, c.OPS_Container.ORD_Container.ORD_Order.RequestDate, c.OPS_Container.ORD_Container.ORD_Order.CAT_TransportMode.TransportModeID, c.OPS_Container.ORD_Container.OrderID, StatusOfCOTOMasterID = c.COTOMasterID > 0 ? c.OPS_COTOMaster.StatusOfCOTOMasterID : -1, c.StatusOfCOContainerID, c.TypeOfStatusContainerID, c.OPS_Container.ORD_Container.PackingID, c.OPS_Container.ORD_Container.ORD_Order.StatusOfOrderID }).ToList();

                    #region Đang lập kế hoạch
                    if (statusid == 0)
                    {
                        while (dtfrom < dtto)
                        {
                            result.ListLTL.Add(new Chart_Customer_OPS_Truck_LTL
                            {
                                CustomerID = customerid > 0 ? customerid.Value : 0,
                                CustomerName = customerid > 0 ? model.CUS_Customer.Where(c => c.ID == customerid.Value).Select(c => c.CustomerName).FirstOrDefault() : "",
                                Date = dtfrom.Date,
                                Ton = lstOPSGroup.Count(c => c.RequestDate.Date == dtfrom.Date && c.TransportModeID == -(int)SYSVarType.TransportModeLTL && c.StatusOfDITOMasterID < -(int)SYSVarType.StatusOfDITOMasterTendered && c.DITOGroupProductStatusID == -(int)SYSVarType.DITOGroupProductStatusWaiting) > 0 ? lstOPSGroup.Where(c => c.RequestDate.Date == dtfrom.Date && c.TransportModeID == -(int)SYSVarType.TransportModeLTL && c.StatusOfDITOMasterID < -(int)SYSVarType.StatusOfDITOMasterTendered && c.DITOGroupProductStatusID == -(int)SYSVarType.DITOGroupProductStatusWaiting).Sum(c => c.TonTranfer) : 0,
                                CBM = lstOPSGroup.Count(c => c.RequestDate.Date == dtfrom.Date && c.TransportModeID == -(int)SYSVarType.TransportModeLTL && c.StatusOfDITOMasterID < -(int)SYSVarType.StatusOfDITOMasterTendered && c.DITOGroupProductStatusID == -(int)SYSVarType.DITOGroupProductStatusWaiting) > 0 ? lstOPSGroup.Where(c => c.RequestDate.Date == dtfrom.Date && c.TransportModeID == -(int)SYSVarType.TransportModeLTL && c.StatusOfDITOMasterID < -(int)SYSVarType.StatusOfDITOMasterTendered && c.DITOGroupProductStatusID == -(int)SYSVarType.DITOGroupProductStatusWaiting).Sum(c => c.CBMTranfer) : 0,
                            });

                            result.ListFTL.Add(new Chart_Customer_OPS_Truck_FTL
                            {
                                CustomerID = customerid > 0 ? customerid.Value : 0,
                                CustomerName = customerid > 0 ? model.CUS_Customer.Where(c => c.ID == customerid.Value).Select(c => c.CustomerName).FirstOrDefault() : "",
                                Date = dtfrom.Date,
                                Total = lstOPSGroup.Count(c => c.RequestDate.Date == dtfrom.Date && c.TransportModeID == -(int)SYSVarType.TransportModeFTL && c.StatusOfOrderID == -(int)SYSVarType.StatusOfOrderPlaning) > 0 ? lstOPSGroup.Where(c => c.RequestDate.Date == dtfrom.Date && c.TransportModeID == -(int)SYSVarType.TransportModeFTL && c.StatusOfOrderID == -(int)SYSVarType.StatusOfOrderPlaning).GroupBy(c => c.OrderID).Count() : 0,
                            });
                            Chart_Customer_OPS_Container itemFCL = new Chart_Customer_OPS_Container();
                            itemFCL.CustomerID = customerid > 0 ? customerid.Value : 0;
                            itemFCL.Date = dtfrom.Date;
                            result.ListFCL.Add(itemFCL);

                            Chart_Customer_OPS_Container itemLCL = new Chart_Customer_OPS_Container();
                            itemLCL.CustomerID = customerid > 0 ? customerid.Value : 0;
                            itemLCL.CustomerName = customerid > 0 ? model.CUS_Customer.Where(c => c.ID == customerid.Value).Select(c => c.CustomerName).FirstOrDefault() : "";
                            itemLCL.Date = dtfrom.Date;
                            result.ListLCL.Add(itemLCL);

                            var lstContainerDate = lstContainer.Where(c => c.RequestDate.Date == dtfrom.Date);
                            var lstOPSContainerDate = lstOPSContainer.Where(c => c.RequestDate.Date == dtfrom.Date);

                            foreach (var itemContainerDate in lstContainerDate)
                            {
                                var lstOPS = lstOPSContainerDate.Where(c => c.ContainerID == itemContainerDate.ID && c.COTOMasterID == null);
                                if (lstOPS.Count() > 0)
                                {
                                    if (itemContainerDate.PackingID == (int)CATPackingCOCode.CO20)
                                    {
                                        if (itemContainerDate.TransportModeID == -(int)SYSVarType.TransportModeFCL)
                                            itemFCL.Total20DC += 1;
                                        else
                                            itemLCL.Total20DC += 1;
                                    }
                                    if (itemContainerDate.PackingID == (int)CATPackingCOCode.CO40)
                                    {
                                        if (itemContainerDate.TransportModeID == -(int)SYSVarType.TransportModeFCL)
                                            itemFCL.Total40DC += 1;
                                        else
                                            itemLCL.Total40DC += 1;
                                    }
                                    if (itemContainerDate.PackingID == (int)CATPackingCOCode.CO40H)
                                    {
                                        if (itemContainerDate.TransportModeID == -(int)SYSVarType.TransportModeFCL)
                                            itemFCL.Total40HC += 1;
                                        else
                                            itemLCL.Total40HC += 1;
                                    }
                                }
                            }

                            dtfrom = dtfrom.AddDays(1);
                        }
                    }
                    #endregion

                    #region Yêu cầu
                    if (statusid == 1)
                    {
                        while (dtfrom < dtto)
                        {
                            result.ListLTL.Add(new Chart_Customer_OPS_Truck_LTL
                            {
                                CustomerID = customerid > 0 ? customerid.Value : 0,
                                Date = dtfrom.Date,
                                Ton = lstGroup.Count(c => c.RequestDate.Date == dtfrom.Date && c.TransportModeID == -(int)SYSVarType.TransportModeLTL) > 0 ? lstGroup.Where(c => c.RequestDate.Date == dtfrom.Date && c.TransportModeID == -(int)SYSVarType.TransportModeLTL).Sum(c => c.Ton) : 0,
                                CBM = lstGroup.Count(c => c.RequestDate.Date == dtfrom.Date && c.TransportModeID == -(int)SYSVarType.TransportModeLTL) > 0 ? lstGroup.Where(c => c.RequestDate.Date == dtfrom.Date && c.TransportModeID == -(int)SYSVarType.TransportModeLTL).Sum(c => c.CBM) : 0,
                            });

                            result.ListFTL.Add(new Chart_Customer_OPS_Truck_FTL
                            {
                                CustomerID = customerid > 0 ? customerid.Value : 0,
                                Date = dtfrom.Date,
                                Total = lstOrder.Count(c => c.RequestDate.Date == dtfrom.Date && c.TransportModeID == -(int)SYSVarType.TransportModeFTL)
                            });

                            result.ListFCL.Add(new Chart_Customer_OPS_Container
                            {
                                CustomerID = customerid > 0 ? customerid.Value : 0,
                                Date = dtfrom.Date,
                                Total20DC = lstContainer.Count(c => c.RequestDate.Date == dtfrom.Date && c.TransportModeID == -(int)SYSVarType.TransportModeFCL && c.PackingID == (int)CATPackingCOCode.CO20),
                                Total40DC = lstContainer.Count(c => c.RequestDate.Date == dtfrom.Date && c.TransportModeID == -(int)SYSVarType.TransportModeFCL && c.PackingID == (int)CATPackingCOCode.CO40),
                                Total40HC = lstContainer.Count(c => c.RequestDate.Date == dtfrom.Date && c.TransportModeID == -(int)SYSVarType.TransportModeFCL && c.PackingID == (int)CATPackingCOCode.CO40H),
                            });

                            result.ListLCL.Add(new Chart_Customer_OPS_Container
                            {
                                CustomerID = customerid > 0 ? customerid.Value : 0,
                                Date = dtfrom.Date,
                                Total20DC = lstContainer.Count(c => c.RequestDate.Date == dtfrom.Date && c.TransportModeID == -(int)SYSVarType.TransportModeLCL && c.PackingID == (int)CATPackingCOCode.CO20),
                                Total40DC = lstContainer.Count(c => c.RequestDate.Date == dtfrom.Date && c.TransportModeID == -(int)SYSVarType.TransportModeLCL && c.PackingID == (int)CATPackingCOCode.CO40),
                                Total40HC = lstContainer.Count(c => c.RequestDate.Date == dtfrom.Date && c.TransportModeID == -(int)SYSVarType.TransportModeLCL && c.PackingID == (int)CATPackingCOCode.CO40H),
                            });

                            dtfrom = dtfrom.AddDays(1);
                        }
                    }
                    #endregion

                    #region Vận chuyển
                    if (statusid == 2)
                    {
                        while (dtfrom < dtto)
                        {
                            result.ListLTL.Add(new Chart_Customer_OPS_Truck_LTL
                            {
                                CustomerID = customerid > 0 ? customerid.Value : 0,
                                Date = dtfrom.Date,
                                Ton = lstOPSGroup.Count(c => c.RequestDate.Date == dtfrom.Date && c.TransportModeID == -(int)SYSVarType.TransportModeLTL && c.StatusOfDITOMasterID >= -(int)SYSVarType.StatusOfDITOMasterDelivery && c.DITOGroupProductStatusID == -(int)SYSVarType.DITOGroupProductStatusWaiting) > 0 ? lstOPSGroup.Where(c => c.RequestDate.Date == dtfrom.Date && c.TransportModeID == -(int)SYSVarType.TransportModeLTL && c.StatusOfDITOMasterID >= -(int)SYSVarType.StatusOfDITOMasterDelivery && c.DITOGroupProductStatusID == -(int)SYSVarType.DITOGroupProductStatusWaiting).Sum(c => c.TonTranfer) : 0,
                                CBM = lstOPSGroup.Count(c => c.RequestDate.Date == dtfrom.Date && c.TransportModeID == -(int)SYSVarType.TransportModeLTL && c.StatusOfDITOMasterID >= -(int)SYSVarType.StatusOfDITOMasterDelivery && c.DITOGroupProductStatusID == -(int)SYSVarType.DITOGroupProductStatusWaiting) > 0 ? lstOPSGroup.Where(c => c.RequestDate.Date == dtfrom.Date && c.TransportModeID == -(int)SYSVarType.TransportModeLTL && c.StatusOfDITOMasterID >= -(int)SYSVarType.StatusOfDITOMasterDelivery && c.DITOGroupProductStatusID == -(int)SYSVarType.DITOGroupProductStatusWaiting).Sum(c => c.CBMTranfer) : 0,
                            });

                            result.ListFTL.Add(new Chart_Customer_OPS_Truck_FTL
                            {
                                CustomerID = customerid > 0 ? customerid.Value : 0,
                                Date = dtfrom.Date,
                                Total = lstOPSGroup.Count(c => c.RequestDate.Date == dtfrom.Date && c.TransportModeID == -(int)SYSVarType.TransportModeFTL && c.StatusOfOrderID == -(int)SYSVarType.StatusOfOrderTranfer) > 0 ? lstOPSGroup.Where(c => c.RequestDate.Date == dtfrom.Date && c.TransportModeID == -(int)SYSVarType.TransportModeFTL && c.StatusOfOrderID == -(int)SYSVarType.StatusOfOrderTranfer).GroupBy(c => c.OrderID).Count() : 0,
                            });
                            Chart_Customer_OPS_Container itemFCL = new Chart_Customer_OPS_Container();
                            itemFCL.CustomerID = customerid > 0 ? customerid.Value : 0;
                            itemFCL.Date = dtfrom.Date;
                            result.ListFCL.Add(itemFCL);

                            Chart_Customer_OPS_Container itemLCL = new Chart_Customer_OPS_Container();
                            itemLCL.CustomerID = customerid > 0 ? customerid.Value : 0;
                            itemLCL.Date = dtfrom.Date;
                            result.ListLCL.Add(itemLCL);

                            var lstContainerDate = lstContainer.Where(c => c.RequestDate.Date == dtfrom.Date);
                            var lstOPSContainerDate = lstOPSContainer.Where(c => c.RequestDate.Date == dtfrom.Date);

                            foreach (var itemContainerDate in lstContainerDate)
                            {
                                var lstOPS = lstOPSContainerDate.Where(c => c.ContainerID == itemContainerDate.ID);
                                if (lstOPS.Count(c => c.COTOMasterID > 0) > 0)
                                {
                                    if (lstOPS.Count(c => c.TypeOfStatusContainerID == -(int)SYSVarType.TypeOfStatusContainerComplete) != lstOPS.Count())
                                    {
                                        if (itemContainerDate.PackingID == (int)CATPackingCOCode.CO20)
                                        {
                                            if (itemContainerDate.TransportModeID == -(int)SYSVarType.TransportModeFCL)
                                                itemFCL.Total20DC += 1;
                                            else
                                                itemLCL.Total20DC += 1;
                                        }
                                        if (itemContainerDate.PackingID == (int)CATPackingCOCode.CO40)
                                        {
                                            if (itemContainerDate.TransportModeID == -(int)SYSVarType.TransportModeFCL)
                                                itemFCL.Total40DC += 1;
                                            else
                                                itemLCL.Total40DC += 1;
                                        }
                                        if (itemContainerDate.PackingID == (int)CATPackingCOCode.CO40H)
                                        {
                                            if (itemContainerDate.TransportModeID == -(int)SYSVarType.TransportModeFCL)
                                                itemFCL.Total40HC += 1;
                                            else
                                                itemLCL.Total40HC += 1;
                                        }
                                    }
                                }
                            }

                            dtfrom = dtfrom.AddDays(1);
                        }
                    }
                    #endregion

                    #region Đã hoàn tất
                    if (statusid == 3)
                    {
                        while (dtfrom < dtto)
                        {
                            result.ListLTL.Add(new Chart_Customer_OPS_Truck_LTL
                            {
                                CustomerID = customerid > 0 ? customerid.Value : 0,
                                Date = dtfrom.Date,
                                Ton = lstOPSGroup.Count(c => c.RequestDate.Date == dtfrom.Date && c.TransportModeID == -(int)SYSVarType.TransportModeLTL && c.DITOGroupProductStatusID == -(int)SYSVarType.DITOGroupProductStatusComplete) > 0 ? lstOPSGroup.Where(c => c.RequestDate.Date == dtfrom.Date && c.TransportModeID == -(int)SYSVarType.TransportModeLTL && c.DITOGroupProductStatusID == -(int)SYSVarType.DITOGroupProductStatusComplete).Sum(c => c.TonTranfer) : 0,
                                CBM = lstOPSGroup.Count(c => c.RequestDate.Date == dtfrom.Date && c.TransportModeID == -(int)SYSVarType.TransportModeLTL && c.DITOGroupProductStatusID == -(int)SYSVarType.DITOGroupProductStatusComplete) > 0 ? lstOPSGroup.Where(c => c.RequestDate.Date == dtfrom.Date && c.TransportModeID == -(int)SYSVarType.TransportModeLTL && c.DITOGroupProductStatusID == -(int)SYSVarType.DITOGroupProductStatusComplete).Sum(c => c.CBMTranfer) : 0,
                            });

                            result.ListFTL.Add(new Chart_Customer_OPS_Truck_FTL
                            {
                                CustomerID = customerid > 0 ? customerid.Value : 0,
                                Date = dtfrom.Date,
                                Total = lstOPSGroup.Count(c => c.RequestDate.Date == dtfrom.Date && c.TransportModeID == -(int)SYSVarType.TransportModeFTL && (c.StatusOfOrderID >= -(int)SYSVarType.StatusOfOrderReceived && c.StatusOfOrderID <= -(int)SYSVarType.StatusOfOrderInvoicePart)) > 0 ? lstOPSGroup.Where(c => c.RequestDate.Date == dtfrom.Date && c.TransportModeID == -(int)SYSVarType.TransportModeFTL && (c.StatusOfOrderID >= -(int)SYSVarType.StatusOfOrderReceived && c.StatusOfOrderID <= -(int)SYSVarType.StatusOfOrderInvoicePart)).GroupBy(c => c.OrderID).Count() : 0,
                            });

                            Chart_Customer_OPS_Container itemFCL = new Chart_Customer_OPS_Container();
                            itemFCL.CustomerID = customerid > 0 ? customerid.Value : 0;
                            itemFCL.Date = dtfrom.Date;
                            result.ListFCL.Add(itemFCL);

                            Chart_Customer_OPS_Container itemLCL = new Chart_Customer_OPS_Container();
                            itemLCL.CustomerID = customerid > 0 ? customerid.Value : 0;
                            itemLCL.Date = dtfrom.Date;
                            result.ListLCL.Add(itemLCL);

                            var lstContainerDate = lstContainer.Where(c => c.RequestDate.Date == dtfrom.Date);
                            var lstOPSContainerDate = lstOPSContainer.Where(c => c.RequestDate.Date == dtfrom.Date);

                            foreach (var itemContainerDate in lstContainerDate)
                            {
                                var lstOPS = lstOPSContainerDate.Where(c => c.ContainerID == itemContainerDate.ID);
                                if (lstOPS.Count(c => c.COTOMasterID > 0) > 0)
                                {
                                    if (lstOPS.Count(c => c.TypeOfStatusContainerID == -(int)SYSVarType.TypeOfStatusContainerComplete) == lstOPS.Count())
                                    {
                                        if (itemContainerDate.PackingID == (int)CATPackingCOCode.CO20)
                                        {
                                            if (itemContainerDate.TransportModeID == -(int)SYSVarType.TransportModeFCL)
                                                itemFCL.Total20DC += 1;
                                            else
                                                itemLCL.Total20DC += 1;
                                        }
                                        if (itemContainerDate.PackingID == (int)CATPackingCOCode.CO40)
                                        {
                                            if (itemContainerDate.TransportModeID == -(int)SYSVarType.TransportModeFCL)
                                                itemFCL.Total40DC += 1;
                                            else
                                                itemLCL.Total40DC += 1;
                                        }
                                        if (itemContainerDate.PackingID == (int)CATPackingCOCode.CO40H)
                                        {
                                            if (itemContainerDate.TransportModeID == -(int)SYSVarType.TransportModeFCL)
                                                itemFCL.Total40HC += 1;
                                            else
                                                itemLCL.Total40HC += 1;
                                        }
                                    }
                                }
                            }

                            dtfrom = dtfrom.AddDays(1);
                        }
                    }
                    #endregion

                    #region Đã nhận chứng từ
                    if (statusid == 4)
                    {
                        while (dtfrom < dtto)
                        {
                            result.ListLTL.Add(new Chart_Customer_OPS_Truck_LTL
                            {
                                CustomerID = customerid > 0 ? customerid.Value : 0,
                                Date = dtfrom.Date,
                                Ton = lstOPSGroup.Count(c => c.RequestDate.Date == dtfrom.Date && c.TransportModeID == -(int)SYSVarType.TransportModeLTL && c.DITOGroupProductStatusID == -(int)SYSVarType.DITOGroupProductStatusComplete && c.DITOGroupProductStatusPODID == -(int)SYSVarType.DITOGroupProductStatusPODComplete) > 0 ? lstOPSGroup.Where(c => c.RequestDate.Date == dtfrom.Date && c.TransportModeID == -(int)SYSVarType.TransportModeLTL && c.DITOGroupProductStatusID == -(int)SYSVarType.DITOGroupProductStatusComplete && c.DITOGroupProductStatusPODID == -(int)SYSVarType.DITOGroupProductStatusPODComplete).Sum(c => c.TonTranfer) : 0,
                                CBM = lstOPSGroup.Count(c => c.RequestDate.Date == dtfrom.Date && c.TransportModeID == -(int)SYSVarType.TransportModeLTL && c.DITOGroupProductStatusID == -(int)SYSVarType.DITOGroupProductStatusComplete && c.DITOGroupProductStatusPODID == -(int)SYSVarType.DITOGroupProductStatusPODComplete) > 0 ? lstOPSGroup.Where(c => c.RequestDate.Date == dtfrom.Date && c.TransportModeID == -(int)SYSVarType.TransportModeLTL && c.DITOGroupProductStatusID == -(int)SYSVarType.DITOGroupProductStatusComplete && c.DITOGroupProductStatusPODID == -(int)SYSVarType.DITOGroupProductStatusPODComplete).Sum(c => c.CBMTranfer) : 0,
                            });

                            result.ListFTL.Add(new Chart_Customer_OPS_Truck_FTL
                            {
                                CustomerID = customerid > 0 ? customerid.Value : 0,
                                Date = dtfrom.Date,
                                Total = lstOPSGroup.Count(c => c.RequestDate.Date == dtfrom.Date && c.TransportModeID == -(int)SYSVarType.TransportModeFTL && c.StatusOfOrderID == -(int)SYSVarType.StatusOfOrderInvoiceComplete) > 0 ? lstOPSGroup.Where(c => c.RequestDate.Date == dtfrom.Date && c.TransportModeID == -(int)SYSVarType.TransportModeFTL && c.StatusOfOrderID == -(int)SYSVarType.StatusOfOrderInvoiceComplete).GroupBy(c => c.OrderID).Count() : 0,
                            });

                            dtfrom = dtfrom.AddDays(1);
                        }

                    }
                    #endregion
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

        // Pie 4: Tỷ lệ đơn hàng yêu cầu
        public Chart_Customer_Order_Rate Chart_Customer_Order_Rate_Data(DateTime dtfrom, DateTime dtto, int quantity)
        {
            try
            {
                var result = new Chart_Customer_Order_Rate();
                result.ListData = new List<Chart_Customer_Order_Rate_Data>();
                using (var model = new DataEntities())
                {
                    dtfrom = dtfrom.Date;
                    dtto = dtto.Date.AddDays(1);

                    string ViewAdmin = SYSViewCode.ViewAdmin.ToString();
                    var lstCustomerID = model.CUS_Customer.Where(c => (c.TypeOfCustomerID == -(int)SYSVarType.TypeOfCustomerCUS || c.TypeOfCustomerID == -(int)SYSVarType.TypeOfCustomerBOTH) && !c.IsSystem && (Account.ListActionCode.Contains(ViewAdmin) || Account.ListCustomerID.Contains(c.ID))).Select(c => c.ID).ToList();

                    result.ListData = model.ORD_Order.Where(c => c.SYSCustomerID == Account.SYSCustomerID && c.RequestDate >= dtfrom && c.RequestDate < dtto && lstCustomerID.Contains(c.CustomerID)).GroupBy(c => new { c.CustomerID, c.CUS_Customer.ShortName }).Select(c => new Chart_Customer_Order_Rate_Data
                    {
                        CustomerID = c.Key.CustomerID,
                        CustomerName = c.Key.ShortName,
                        Total = c.Count()
                    }).OrderByDescending(c => c.Total).ToList();

                    if (quantity > 0)
                        result.ListData = result.ListData.Take(quantity).ToList();

                    var Total = result.ListData.Sum(c => c.Total);
                    if (Total > 0)
                    {
                        foreach (var itemData in result.ListData)
                            itemData.Percent = Math.Round((itemData.Total / Total) * 100, 2);
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

        // Chart 5: Khả năng chuyên chở
        public Chart_Owner_Capacity Chart_Owner_Capacity_Data(DateTime dtfrom, DateTime dtto)
        {
            try
            {
                var result = new Chart_Owner_Capacity();
                result.ListData = new List<Chart_Owner_Capacity_Data>();
                using (var model = new DataEntities())
                {
                    dtfrom = dtfrom.Date;
                    dtto = dtto.Date.AddDays(1);

                    string ViewAdmin = SYSViewCode.ViewAdmin.ToString();
                    var lstCustomerID = model.CUS_Customer.Where(c => (c.TypeOfCustomerID == -(int)SYSVarType.TypeOfCustomerCUS || c.TypeOfCustomerID == -(int)SYSVarType.TypeOfCustomerBOTH) && !c.IsSystem && (Account.ListActionCode.Contains(ViewAdmin) || Account.ListCustomerID.Contains(c.ID))).Select(c => c.ID).ToList();

                    var query = model.OPS_DITOMaster.Where(c => c.SYSCustomerID == Account.SYSCustomerID && c.StatusOfDITOMasterID >= -(int)SYSVarType.StatusOfDITOMasterReceived && c.VehicleID > 0 && (c.VendorOfVehicleID == null || c.VendorOfVehicleID == Account.SYSCustomerID) && c.ETD >= dtfrom && c.ETD < dtto && c.OPS_DITOGroupProduct.Count > 0 && c.CAT_Vehicle.GroupOfVehicleID > 0 && c.OPS_DITOGroupProduct.Any(d => lstCustomerID.Contains(d.ORD_GroupProduct.ORD_Order.CustomerID))).Select(c => new
                        {
                            DITOMasterID = c.ID,
                            GroupOfVehicleID = c.CAT_Vehicle.GroupOfVehicleID,
                            TonTranfer = c.OPS_DITOGroupProduct.Sum(d => d.TonTranfer),
                            CBMTranfer = c.OPS_DITOGroupProduct.Sum(d => d.CBMTranfer),
                            c.VehicleID,
                            c.CAT_Vehicle.MaxCapacity,
                            c.CAT_Vehicle.MaxWeightCal
                        }).ToList();

                    if (query.Count > 0)
                    {
                        var lstGroupVehicle = model.CAT_GroupOfVehicle.Select(c => new
                        {
                            c.ID,
                            c.Code,
                            c.GroupName,
                            c.Ton
                        }).ToList();

                        foreach (var itemGroup in query.GroupBy(c => c.GroupOfVehicleID))
                        {
                            var groupVehicle = lstGroupVehicle.FirstOrDefault(c => c.ID == itemGroup.Key);
                            if (groupVehicle != null)
                            {
                                Chart_Owner_Capacity_Data item = new Chart_Owner_Capacity_Data();
                                item.GroupOfVehicleID = groupVehicle.ID;
                                item.GroupOfVehicleCode = groupVehicle.Code;
                                item.TotalSchedule = itemGroup.Count();
                                item.TotalTon = itemGroup.Sum(c => c.TonTranfer);
                                item.TotalCBM = itemGroup.Sum(c => c.CBMTranfer);
                                foreach (var itemVehicle in itemGroup.GroupBy(c => c.VehicleID))
                                {
                                    var vehicle = itemVehicle.FirstOrDefault();
                                    item.TonMax += vehicle.MaxWeightCal > 0 ? vehicle.MaxWeightCal.Value * itemVehicle.Count() : 0;
                                    item.CBMMax += vehicle.MaxCapacity > 0 ? vehicle.MaxCapacity.Value * itemVehicle.Count() : 0;
                                }
                                item.ValueTon = item.TonMax > 0 ? (item.TotalTon / item.TonMax) * 100 : 0;
                                item.ValueCBM = item.CBMMax > 0 ? (item.TotalCBM / item.CBMMax) * 100 : 0;
                                item.ValueTon = Math.Round(item.ValueTon, 1);
                                item.ValueCBM = Math.Round(item.ValueCBM, 1);
                                result.ListData.Add(item);
                            }
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

        // Chart 6: KM theo từng ngày
        public Chart_Owner_KM Chart_Owner_KM_Data(DateTime dtfrom, DateTime dtto)
        {
            try
            {
                var result = new Chart_Owner_KM();
                result.ListData = new List<Chart_Owner_KM_Data>();
                using (var model = new DataEntities())
                {
                    dtfrom = dtfrom.Date;
                    dtto = dtto.Date.AddDays(1);

                    string ViewAdmin = SYSViewCode.ViewAdmin.ToString();
                    var lstCustomerID = model.CUS_Customer.Where(c => (c.TypeOfCustomerID == -(int)SYSVarType.TypeOfCustomerCUS || c.TypeOfCustomerID == -(int)SYSVarType.TypeOfCustomerBOTH) && !c.IsSystem && (Account.ListActionCode.Contains(ViewAdmin) || Account.ListCustomerID.Contains(c.ID))).Select(c => c.ID).ToList();

                    var query = model.OPS_DITOMaster.Where(c => c.SYSCustomerID == Account.SYSCustomerID && c.StatusOfDITOMasterID >= -(int)SYSVarType.StatusOfDITOMasterReceived && c.VehicleID > 0 && (c.VendorOfVehicleID == null || c.VendorOfVehicleID == Account.SYSCustomerID) && c.ETD >= dtfrom && c.ETD < dtto && c.OPS_DITOGroupProduct.Count > 0 && c.OPS_DITOGroupProduct.Any(d => lstCustomerID.Contains(d.ORD_GroupProduct.ORD_Order.CustomerID))).Select(c => new
                        {
                            DITOMasterID = c.ID,
                            c.ETD
                        }).ToList();

                    if (query.Count > 0)
                    {
                        while (dtfrom < dtto)
                        {
                            var lstMaster = query.Where(c => c.ETD.Value.Date == dtfrom);
                            Chart_Owner_KM_Data item = new Chart_Owner_KM_Data();
                            item.Date = dtfrom.Date;
                            result.ListData.Add(item);
                            foreach (var itemMaster in lstMaster)
                            {
                                var lstDITO = model.OPS_DITO.Where(c => c.DITOMasterID == itemMaster.DITOMasterID && c.LocationFromID > 0 && c.LocationToID > 0 && c.KM > 0).OrderBy(c => c.SortOrder).Select(c => new
                                {
                                    KM = c.KM > 0 ? c.KM.Value : 0,
                                    c.LocationFromID,
                                    c.LocationToID,
                                }).ToList();

                                if (lstDITO.Count > 0)
                                {
                                    var lstGroup = model.OPS_DITOGroupProduct.Where(c => c.DITOMasterID == itemMaster.DITOMasterID && c.ORD_GroupProduct.LocationFromID > 0 && c.ORD_GroupProduct.LocationToID > 0).Select(c => new
                                    {
                                        LocationFromID = c.ORD_GroupProduct.CUS_Location.LocationID,
                                        LocationToID = c.ORD_GroupProduct.CUS_Location1.LocationID,
                                    }).ToList();

                                    foreach (var itemDITO in lstDITO)
                                    {
                                        if (lstGroup.Any(c => c.LocationToID == itemDITO.LocationToID))
                                            item.KMLaden += itemDITO.KM;
                                        else
                                            item.KMEmpty += itemDITO.KM;

                                        item.TotalKM += itemDITO.KM;
                                    }
                                }
                            }

                            item.KMEmpty = Math.Round(item.KMEmpty, 1);
                            item.KMLaden = Math.Round(item.KMLaden, 1);
                            item.TotalKM = Math.Round(item.TotalKM, 1);

                            dtfrom = dtfrom.AddDays(1);
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

        // Chart 7: Thời gian vận hành
        public Chart_Owner_Operation Chart_Owner_Operation_Data(DateTime dtfrom, DateTime dtto)
        {
            try
            {
                var result = new Chart_Owner_Operation();
                result.ListData = new List<Chart_Owner_Operation_Data>();
                using (var model = new DataEntities())
                {
                    dtfrom = dtfrom.Date;
                    dtto = dtto.Date.AddDays(1);

                    string ViewAdmin = SYSViewCode.ViewAdmin.ToString();
                    var lstCustomerID = model.CUS_Customer.Where(c => (c.TypeOfCustomerID == -(int)SYSVarType.TypeOfCustomerCUS || c.TypeOfCustomerID == -(int)SYSVarType.TypeOfCustomerBOTH) && !c.IsSystem && (Account.ListActionCode.Contains(ViewAdmin) || Account.ListCustomerID.Contains(c.ID))).Select(c => c.ID).ToList();

                    var query = model.OPS_DITOMaster.Where(c => c.SYSCustomerID == Account.SYSCustomerID && c.StatusOfDITOMasterID >= -(int)SYSVarType.StatusOfDITOMasterReceived && c.VehicleID > 0 && (c.VendorOfVehicleID == null || c.VendorOfVehicleID == Account.SYSCustomerID) && c.ETD >= dtfrom && c.ETD < dtto && c.OPS_DITOGroupProduct.Count > 0 && c.OPS_DITOGroupProduct.Any(d => lstCustomerID.Contains(d.ORD_GroupProduct.ORD_Order.CustomerID))).Select(c => new
                    {
                        DITOMasterID = c.ID,
                        c.VehicleID,
                        VehicleCode = c.CAT_Vehicle.RegNo,
                        c.ETD
                    }).ToList();

                    if (query.Count > 0)
                    {
                        foreach (var itemVehicle in query.GroupBy(c => new { c.VehicleID, c.VehicleCode }))
                        {
                            Chart_Owner_Operation_Data item = new Chart_Owner_Operation_Data();
                            item.VehicleID = itemVehicle.Key.VehicleID.Value;
                            item.VehicleCode = itemVehicle.Key.VehicleCode;
                            result.ListData.Add(item);
                            var lstDate = itemVehicle.GroupBy(c => c.ETD.Value.Date);
                            item.TotalTime = 8 * lstDate.Count();
                            var lstMasterID = itemVehicle.Select(c => c.DITOMasterID).Distinct().ToList();
                            var lstGroup = model.OPS_DITOLocation.Where(c => c.DITOMasterID > 0 && lstMasterID.Contains(c.DITOMasterID)).OrderBy(c => c.DateCome).ThenBy(c => c.DateLeave).Select(c => new
                            {
                                c.DITOMasterID,
                                c.DateCome,
                                c.LoadingStart,
                                c.LoadingEnd,
                                c.DateLeave,
                            }).ToList();

                            foreach (var itemMasterGroup in lstGroup.GroupBy(c => c.DITOMasterID))
                            {
                                foreach (var itemGroup in itemMasterGroup.OrderBy(c => c.DateCome))
                                {
                                    if (itemGroup.DateCome.HasValue && itemGroup.DateLeave.HasValue && itemGroup.DateLeave > itemGroup.DateCome)
                                        item.RunningTime += (itemGroup.DateLeave.Value - itemGroup.DateCome.Value).TotalHours;

                                    if (itemGroup.LoadingStart.HasValue && itemGroup.LoadingEnd.HasValue && itemGroup.LoadingEnd > itemGroup.LoadingStart)
                                        item.LoadingTime += (itemGroup.LoadingEnd.Value - itemGroup.LoadingStart.Value).TotalHours;

                                    if (itemGroup.DateCome.HasValue && itemGroup.LoadingStart.HasValue && itemGroup.LoadingStart > itemGroup.DateCome)
                                        item.WaittingTime += (itemGroup.LoadingStart.Value - itemGroup.DateCome.Value).TotalHours;
                                }
                            }

                            item.TotalTime = Math.Round(item.TotalTime, 1);
                            item.RunningTime = Math.Round(item.RunningTime, 1);
                            item.LoadingTime = Math.Round(item.LoadingTime, 1);
                            item.WaittingTime = Math.Round(item.WaittingTime, 1);
                            item.OtherTime = item.TotalTime - item.RunningTime - item.LoadingTime - item.WaittingTime;
                            item.OtherTime = Math.Round(item.OtherTime, 1);
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

        // Chart 8: Chi phí trên mỗi tấn/khối/km của xe
        public Chart_Owner_CostRate Chart_Owner_CostRate_Data(DateTime dtfrom, DateTime dtto)
        {
            try
            {
                #region Temp Cost
                List<int> lstNoGroupDebit = new List<int>();
                lstNoGroupDebit.Add((int)CATCostType.FLMScheduleFeeNoGroup);
                lstNoGroupDebit.Add((int)CATCostType.FLMDriverFeeNoGroup);
                lstNoGroupDebit.Add((int)CATCostType.FLMReceiptRegistry);
                lstNoGroupDebit.Add((int)CATCostType.FLMReceiptMaintence);
                lstNoGroupDebit.Add((int)CATCostType.FLMReceiptRepairLarge);
                lstNoGroupDebit.Add((int)CATCostType.FLMReceiptRepairSmall);
                lstNoGroupDebit.Add((int)CATCostType.FLMReceiptMaterial);
                lstNoGroupDebit.Add((int)CATCostType.FLMDepreciationNoGroup);
                lstNoGroupDebit.Add((int)CATCostType.FLMDepreciationReceiptNoGroup);
                lstNoGroupDebit.Add((int)CATCostType.StationDebit);
                lstNoGroupDebit.Add((int)CATCostType.TroubleDebit);
                List<int> lstGroupDebit = new List<int>();
                lstGroupDebit.Add((int)CATCostType.FLMScheduleFee);
                lstGroupDebit.Add((int)CATCostType.FLMDriverFee);
                lstGroupDebit.Add((int)CATCostType.FLMDepreciation);
                lstGroupDebit.Add((int)CATCostType.FLMDepreciationReceipt);
                #endregion

                var result = new Chart_Owner_CostRate();
                result.ListData = new List<Chart_Owner_CostRate_Data>();
                using (var model = new DataEntities())
                {
                    dtfrom = dtfrom.Date;
                    dtto = dtto.Date.AddDays(1);

                    string ViewAdmin = SYSViewCode.ViewAdmin.ToString();
                    var lstCustomerID = model.CUS_Customer.Where(c => (c.TypeOfCustomerID == -(int)SYSVarType.TypeOfCustomerCUS || c.TypeOfCustomerID == -(int)SYSVarType.TypeOfCustomerBOTH) && !c.IsSystem && (Account.ListActionCode.Contains(ViewAdmin) || Account.ListCustomerID.Contains(c.ID))).Select(c => c.ID).ToList();

                    var lstAllVehicle = model.FLM_Asset.Where(c => c.VehicleID > 0 && c.SYSCustomerID == Account.SYSCustomerID).Select(c => new
                    {
                        VehicleID = c.VehicleID.Value,
                        VehicleCode = c.CAT_Vehicle.RegNo
                    }).ToList();

                    // Danh sách chuyến
                    var lstMaster = model.OPS_DITOMaster.Where(c => c.SYSCustomerID == Account.SYSCustomerID && c.StatusOfDITOMasterID >= -(int)SYSVarType.StatusOfDITOMasterReceived && c.VehicleID > 0 && (c.VendorOfVehicleID == null || c.VendorOfVehicleID == Account.SYSCustomerID) && c.DateConfig >= dtfrom && c.DateConfig < dtto && c.OPS_DITOGroupProduct.Count > 0 && c.OPS_DITOGroupProduct.Any(d => lstCustomerID.Contains(d.ORD_GroupProduct.ORD_Order.CustomerID))).Select(c => new
                    {
                        DITOMasterID = c.ID,
                        VehicleID = c.VehicleID,
                        TonTranfer = c.OPS_DITOGroupProduct.Sum(d => d.TonTranfer),
                        CBMTranfer = c.OPS_DITOGroupProduct.Sum(d => d.CBMTranfer),
                        KM = c.KM > 0 ? c.KM.Value : 0,
                    }).ToList();

                    // Lấy chi phí
                    var lstFIN = model.FIN_PLGroupOfProduct.Where(c => c.GroupOfProductID > 0 && c.FIN_PLDetails.FIN_PL.SYSCustomerID == Account.SYSCustomerID && c.FIN_PLDetails.FIN_PL.FINPLTypeID == -(int)SYSVarType.FINPLTypeDriver &&
                    c.FIN_PLDetails.FIN_PL.Effdate >= dtfrom && c.FIN_PLDetails.FIN_PL.Effdate < dtto && c.FIN_PLDetails.FIN_PL.VehicleID > 0 && lstGroupDebit.Contains(c.FIN_PLDetails.CostID)).Select(c => new
                    {
                        c.FIN_PLDetails.FIN_PL.VehicleID,
                        c.FIN_PLDetails.Debit
                    }).ToList();

                    lstFIN.AddRange(model.FIN_PLDetails.Where(c => c.FIN_PL.SYSCustomerID == Account.SYSCustomerID && c.FIN_PL.FINPLTypeID == -(int)SYSVarType.FINPLTypeDriver && c.FIN_PL.VehicleID > 0 && c.FIN_PL.Effdate >= dtfrom && c.FIN_PL.Effdate < dtto && lstNoGroupDebit.Contains(c.CostID)).Select(c => new
                    {
                        c.FIN_PL.VehicleID,
                        c.Debit
                    }).ToList());

                    var lstVehicleID = lstFIN.Select(c => c.VehicleID).Distinct().ToList();
                    lstVehicleID.AddRange(lstMaster.Select(c => c.VehicleID).Distinct().ToList());
                    lstVehicleID = lstVehicleID.Distinct().ToList();

                    foreach (var itemVehicleID in lstVehicleID)
                    {
                        var vehicle = lstAllVehicle.FirstOrDefault(c => c.VehicleID == itemVehicleID);
                        if (vehicle != null)
                        {
                            Chart_Owner_CostRate_Data item = new Chart_Owner_CostRate_Data();
                            item.VehicleID = vehicle.VehicleID;
                            item.VehicleCode = vehicle.VehicleCode;

                            var queryFIN = lstFIN.Where(c => c.VehicleID == item.VehicleID);
                            if (queryFIN.Count() > 0)
                                item.TotalCost = queryFIN.Sum(c => c.Debit);

                            var queryMaster = lstMaster.Where(c => c.VehicleID == item.VehicleID);
                            if (queryMaster.Count() > 0)
                            {
                                item.TotalKM = queryMaster.Sum(c => c.KM);
                                item.TotalTon = queryMaster.Sum(c => c.TonTranfer);
                                item.TotalCBM = queryMaster.Sum(c => c.CBMTranfer);
                            }

                            if (item.TotalCost > 0)
                            {
                                if (item.TotalKM > 0)
                                    item.KMIndex = Math.Round((double)item.TotalCost / item.TotalKM, 1);
                                if (item.TotalTon > 0)
                                    item.TonIndex = Math.Round((double)item.TotalCost / item.TotalTon, 1);
                                if (item.TotalCBM > 0)
                                    item.CBMIndex = Math.Round((double)item.TotalCost / item.TotalCBM, 1);
                            }

                            result.ListData.Add(item);
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

        // Chart 9: Chi phí biến đổi
        public Chart_Owner_CostChange Chart_Owner_CostChange_Data(DateTime dtfrom, DateTime dtto)
        {
            try
            {
                #region Temp Cost
                List<int> lstFixedNoGroup = new List<int>();
                lstFixedNoGroup.Add((int)CATCostType.FLMScheduleFeeNoGroup);
                lstFixedNoGroup.Add((int)CATCostType.FLMDriverFeeNoGroup);
                List<int> lstFixedGroup = new List<int>();
                lstFixedGroup.Add((int)CATCostType.FLMScheduleFee);
                lstFixedGroup.Add((int)CATCostType.FLMDriverFee);

                List<int> lstGroup = new List<int>();
                lstGroup.Add((int)CATCostType.FLMDepreciation);
                lstGroup.Add((int)CATCostType.FLMDepreciationReceipt);
                List<int> lstNoGroup = new List<int>();
                lstNoGroup.Add((int)CATCostType.FLMReceiptRegistry);
                lstNoGroup.Add((int)CATCostType.FLMReceiptMaintence);
                lstNoGroup.Add((int)CATCostType.FLMReceiptRepairLarge);
                lstNoGroup.Add((int)CATCostType.FLMReceiptRepairSmall);
                lstNoGroup.Add((int)CATCostType.FLMReceiptMaterial);
                lstNoGroup.Add((int)CATCostType.FLMDepreciationNoGroup);
                lstNoGroup.Add((int)CATCostType.FLMDepreciationReceiptNoGroup);
                lstNoGroup.Add((int)CATCostType.StationDebit);
                lstNoGroup.Add((int)CATCostType.TroubleDebit);
                #endregion

                var result = new Chart_Owner_CostChange();
                result.ListData = new List<Chart_Owner_CostChange_Data>();
                using (var model = new DataEntities())
                {
                    dtfrom = dtfrom.Date;
                    dtto = dtto.Date.AddDays(1);

                    var lstFINFixed = model.FIN_PLGroupOfProduct.Where(c => c.GroupOfProductID > 0 && c.FIN_PLDetails.FIN_PL.SYSCustomerID == Account.SYSCustomerID && c.FIN_PLDetails.FIN_PL.FINPLTypeID == -(int)SYSVarType.FINPLTypeDriver &&
                    c.FIN_PLDetails.FIN_PL.Effdate >= dtfrom && c.FIN_PLDetails.FIN_PL.Effdate < dtto && c.FIN_PLDetails.FIN_PL.VehicleID > 0 && lstFixedGroup.Contains(c.FIN_PLDetails.CostID)).Select(c => new
                    {
                        c.FIN_PLDetails.Debit
                    }).ToList();

                    lstFINFixed.AddRange(model.FIN_PLDetails.Where(c => c.FIN_PL.SYSCustomerID == Account.SYSCustomerID && c.FIN_PL.FINPLTypeID == -(int)SYSVarType.FINPLTypeDriver && c.FIN_PL.VehicleID > 0 && c.FIN_PL.Effdate >= dtfrom && c.FIN_PL.Effdate < dtto && lstFixedNoGroup.Contains(c.CostID)).Select(c => new
                    {
                        c.Debit
                    }).ToList());

                    var lstFIN = model.FIN_PLGroupOfProduct.Where(c => c.GroupOfProductID > 0 && c.FIN_PLDetails.FIN_PL.SYSCustomerID == Account.SYSCustomerID && c.FIN_PLDetails.FIN_PL.FINPLTypeID == -(int)SYSVarType.FINPLTypeDriver &&
                       c.FIN_PLDetails.FIN_PL.Effdate >= dtfrom && c.FIN_PLDetails.FIN_PL.Effdate < dtto && c.FIN_PLDetails.FIN_PL.VehicleID > 0 && lstGroup.Contains(c.FIN_PLDetails.CostID)).Select(c => new
                       {
                           c.FIN_PLDetails.Debit
                       }).ToList();

                    lstFIN.AddRange(model.FIN_PLDetails.Where(c => c.FIN_PL.SYSCustomerID == Account.SYSCustomerID && c.FIN_PL.FINPLTypeID == -(int)SYSVarType.FINPLTypeDriver && c.FIN_PL.VehicleID > 0 && c.FIN_PL.Effdate >= dtfrom && c.FIN_PL.Effdate < dtto && lstNoGroup.Contains(c.CostID)).Select(c => new
                    {
                        c.Debit
                    }).ToList());

                    Chart_Owner_CostChange_Data itemFix = new DTO.Chart_Owner_CostChange_Data();
                    itemFix.Category = "Chi phí cố định";
                    itemFix.Total = (decimal)Math.Round(lstFINFixed.Sum(c => c.Debit), 0);
                    result.ListData.Add(itemFix);

                    Chart_Owner_CostChange_Data itemVariable = new DTO.Chart_Owner_CostChange_Data();
                    itemVariable.Category = "Chi phí biến đổi";
                    itemVariable.Total = (decimal)Math.Round(lstFIN.Sum(c => c.Debit), 0);
                    result.ListData.Add(itemVariable);

                    if (itemFix.Total + itemVariable.Total > 0)
                    {
                        itemFix.Percent = (double)Math.Round((itemFix.Total / (itemFix.Total + itemVariable.Total)), 1) * 100;
                        itemVariable.Percent = (double)Math.Round((itemVariable.Total / (itemFix.Total + itemVariable.Total)), 1) * 100;
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

        // Chart 10: Tỉ lệ đơn hàng bốc đúng giờ theo ngày
        public Chart_Owner_OnTime Chart_Owner_OnTime_PickUp_Data(DateTime dtfrom, DateTime dtto, int? customerid)
        {
            try
            {
                var result = new Chart_Owner_OnTime();
                result.ListData = new List<Chart_Owner_OnTime_Data>();
                using (var model = new DataEntities())
                {
                    dtfrom = dtfrom.Date;
                    dtto = dtto.Date.AddDays(1);

                    var lstCustomerID = new List<int>();
                    if (customerid > 0)
                        lstCustomerID.Add(customerid.Value);
                    else
                    {
                        string ViewAdmin = SYSViewCode.ViewAdmin.ToString();
                        lstCustomerID = model.CUS_Customer.Where(c => (c.TypeOfCustomerID == -(int)SYSVarType.TypeOfCustomerCUS || c.TypeOfCustomerID == -(int)SYSVarType.TypeOfCustomerBOTH) && !c.IsSystem && (Account.ListActionCode.Contains(ViewAdmin) || Account.ListCustomerID.Contains(c.ID))).Select(c => c.ID).ToList();
                    }

                    var query = model.KPI_KPITime.Where(c => c.IsKPI.HasValue && lstCustomerID.Contains(c.CustomerID) && c.KPIID == (int)KPICode.Export && c.DateData >= dtfrom && c.DateData < dtto && c.OrderID > 0 && c.ORD_Order.SYSCustomerID == Account.SYSCustomerID).Select(c => new
                        {
                            c.DateData,
                            IsKPI = c.IsKPI.Value
                        }).ToList();

                    result.Total = query.Count;
                    result.TotalKPI = query.Count(c => c.IsKPI);
                    if (result.Total > 0)
                        result.Percent = (double)Math.Round((result.TotalKPI / result.Total), 1) * 100;

                    while (dtfrom < dtto)
                    {
                        var queryKPI = query.Where(c => c.DateData.Value.Date == dtfrom.Date);
                        if (queryKPI.Count() > 0)
                        {
                            Chart_Owner_OnTime_Data item = new Chart_Owner_OnTime_Data();
                            item.Date = dtfrom.Date;
                            item.Total = queryKPI.Count();
                            item.TotalKPI = queryKPI.Count(c => c.IsKPI);
                            if (item.TotalKPI > 0)
                                item.Percent = (double)Math.Round((item.TotalKPI / item.Total), 1) * 100;
                            result.ListData.Add(item);
                        }
                        //var random = new Random();

                        //Chart_Owner_OnTime_Data item = new Chart_Owner_OnTime_Data();
                        //item.Date = dtfrom.Date;
                        //item.Total = random.Next(90, 100);
                        //item.TotalKPI = random.Next(20, 90);
                        //if (item.TotalKPI > 0)
                        //    item.Percent = (double)Math.Round((item.TotalKPI / item.Total), 1) * 100;
                        //result.ListData.Add(item);

                        dtfrom = dtfrom.AddDays(1);
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

        // Chart 11: Tỉ lệ đơn hàng giao đúng giờ theo ngày
        public Chart_Owner_OnTime Chart_Owner_OnTime_Delivery_Data(DateTime dtfrom, DateTime dtto, int? customerid)
        {
            try
            {
                var result = new Chart_Owner_OnTime();
                result.ListData = new List<Chart_Owner_OnTime_Data>();
                using (var model = new DataEntities())
                {
                    dtfrom = dtfrom.Date;
                    dtto = dtto.Date.AddDays(1);

                    var lstCustomerID = new List<int>();
                    if (customerid > 0)
                        lstCustomerID.Add(customerid.Value);
                    else
                    {
                        string ViewAdmin = SYSViewCode.ViewAdmin.ToString();
                        lstCustomerID = model.CUS_Customer.Where(c => (c.TypeOfCustomerID == -(int)SYSVarType.TypeOfCustomerCUS || c.TypeOfCustomerID == -(int)SYSVarType.TypeOfCustomerBOTH) && !c.IsSystem && (Account.ListActionCode.Contains(ViewAdmin) || Account.ListCustomerID.Contains(c.ID))).Select(c => c.ID).ToList();
                    }

                    var query = model.KPI_KPITime.Where(c => c.IsKPI.HasValue && lstCustomerID.Contains(c.CustomerID) && c.KPIID == (int)KPICode.OPS && c.DateData >= dtfrom && c.DateData < dtto && c.OrderID > 0 && c.ORD_Order.SYSCustomerID == Account.SYSCustomerID).Select(c => new
                    {
                        c.DateData,
                        IsKPI = c.IsKPI.Value
                    }).ToList();

                    result.Total = query.Count;
                    result.TotalKPI = query.Count(c => c.IsKPI);
                    if (result.Total > 0)
                        result.Percent = (double)Math.Round((result.TotalKPI / result.Total), 1) * 100;

                    while (dtfrom < dtto)
                    {
                        var queryKPI = query.Where(c => c.DateData.Value.Date == dtfrom.Date);
                        if (queryKPI.Count() > 0)
                        {
                            Chart_Owner_OnTime_Data item = new Chart_Owner_OnTime_Data();
                            item.Date = dtfrom.Date;
                            item.Total = queryKPI.Count();
                            item.TotalKPI = queryKPI.Count(c => c.IsKPI);
                            if (item.TotalKPI > 0)
                                item.Percent = (double)Math.Round((item.TotalKPI / item.Total), 1) * 100;
                            result.ListData.Add(item);
                        }

                        dtfrom = dtfrom.AddDays(1);
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

        // Chart 12: Tỉ lệ chứng từ giao đúng giờ theo ngày
        public Chart_Owner_OnTime Chart_Owner_OnTime_POD_Data(DateTime dtfrom, DateTime dtto, int? customerid)
        {
            try
            {
                var result = new Chart_Owner_OnTime();
                result.ListData = new List<Chart_Owner_OnTime_Data>();
                using (var model = new DataEntities())
                {
                    dtfrom = dtfrom.Date;
                    dtto = dtto.Date.AddDays(1);

                    var lstCustomerID = new List<int>();
                    if (customerid > 0)
                        lstCustomerID.Add(customerid.Value);
                    else
                    {
                        string ViewAdmin = SYSViewCode.ViewAdmin.ToString();
                        lstCustomerID = model.CUS_Customer.Where(c => (c.TypeOfCustomerID == -(int)SYSVarType.TypeOfCustomerCUS || c.TypeOfCustomerID == -(int)SYSVarType.TypeOfCustomerBOTH) && !c.IsSystem && (Account.ListActionCode.Contains(ViewAdmin) || Account.ListCustomerID.Contains(c.ID))).Select(c => c.ID).ToList();
                    }

                    var query = model.KPI_KPITime.Where(c => c.IsKPI.HasValue && lstCustomerID.Contains(c.CustomerID) && c.KPIID == (int)KPICode.POD && c.DateData >= dtfrom && c.DateData < dtto && c.OrderID > 0 && c.ORD_Order.SYSCustomerID == Account.SYSCustomerID).Select(c => new
                    {
                        c.DateData,
                        IsKPI = c.IsKPI.Value
                    }).ToList();

                    result.Total = query.Count;
                    result.TotalKPI = query.Count(c => c.IsKPI);
                    if (result.Total > 0)
                        result.Percent = (double)Math.Round((result.TotalKPI / result.Total), 1) * 100;

                    while (dtfrom < dtto)
                    {
                        var queryKPI = query.Where(c => c.DateData.Value.Date == dtfrom.Date);
                        if (queryKPI.Count() > 0)
                        {
                            Chart_Owner_OnTime_Data item = new Chart_Owner_OnTime_Data();
                            item.Date = dtfrom.Date;
                            item.Total = queryKPI.Count();
                            item.TotalKPI = queryKPI.Count(c => c.IsKPI);
                            if (item.TotalKPI > 0)
                                item.Percent = (double)Math.Round((item.TotalKPI / item.Total), 1) * 100;
                            result.ListData.Add(item);
                        }

                        dtfrom = dtfrom.AddDays(1);
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

        // Chart 13: Hàng trả về
        public Chart_Owner_Return Chart_Owner_Return_Data(DateTime dtfrom, DateTime dtto, int? customerid)
        {
            try
            {
                var result = new Chart_Owner_Return();
                result.ListData = new List<Chart_Owner_Return_Data>();
                using (var model = new DataEntities())
                {
                    dtfrom = dtfrom.Date;
                    dtto = dtto.Date.AddDays(1);

                    var lstCustomerID = new List<int>();
                    if (customerid > 0)
                        lstCustomerID.Add(customerid.Value);
                    else
                    {
                        string ViewAdmin = SYSViewCode.ViewAdmin.ToString();
                        lstCustomerID = model.CUS_Customer.Where(c => (c.TypeOfCustomerID == -(int)SYSVarType.TypeOfCustomerCUS || c.TypeOfCustomerID == -(int)SYSVarType.TypeOfCustomerBOTH) && !c.IsSystem && (Account.ListActionCode.Contains(ViewAdmin) || Account.ListCustomerID.Contains(c.ID))).Select(c => c.ID).ToList();
                    }

                    var query = model.OPS_DITOGroupProduct.Where(c => c.DITOMasterID > 0 && c.OPS_DITOMaster.SYSCustomerID == Account.SYSCustomerID && c.OPS_DITOMaster.ETD >= dtfrom && c.OPS_DITOMaster.ETD < dtto && lstCustomerID.Contains(c.ORD_GroupProduct.ORD_Order.CustomerID) && (c.TonReturn > 0 || c.CBMReturn > 0 || c.QuantityReturn > 0)).Select(c => new
                        {
                            c.OPS_DITOMaster.ETD,
                            c.TonReturn,
                            c.CBMReturn,
                            c.QuantityReturn
                        }).ToList();

                    while (dtfrom < dtto)
                    {
                        Chart_Owner_Return_Data item = new Chart_Owner_Return_Data();
                        item.Date = dtfrom.Date;
                        result.ListData.Add(item);
                        var queryDate = query.Where(c => c.ETD.Value.Date == dtfrom);
                        if (queryDate.Count() > 0)
                        {
                            item.Ton = queryDate.Sum(c => c.TonReturn);
                            item.CBM = queryDate.Sum(c => c.CBMReturn);
                            item.Quantity = queryDate.Sum(c => c.QuantityReturn);
                        }

                        dtfrom = dtfrom.AddDays(1);
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

        // Chart 14: Cấu phần doanh thu, chi phí
        public Chart_Owner_Profit Chart_Owner_Profit_Data(DateTime dtfrom, DateTime dtto, int? customerid)
        {
            try
            {
                #region Temp Cost
                List<int> lstNoGroupCredit = new List<int>();
                lstNoGroupCredit.Add((int)CATCostType.DITOFreightNoGroupCredit);
                lstNoGroupCredit.Add((int)CATCostType.DITOMOQNoGroupCredit);
                lstNoGroupCredit.Add((int)CATCostType.DITOMOQLoadNoGroupCredit);
                lstNoGroupCredit.Add((int)CATCostType.DITOMOQUnLoadNoGroupCredit);
                lstNoGroupCredit.Add((int)CATCostType.DITOExNoGroupCredit);
                lstNoGroupCredit.Add((int)CATCostType.ManualFixCredit);
                List<int> lstNoGroupDebit = new List<int>();
                lstNoGroupDebit.Add((int)CATCostType.DITOFreightNoGroupDebit);
                lstNoGroupDebit.Add((int)CATCostType.DITOMOQNoGroupDebit);
                lstNoGroupDebit.Add((int)CATCostType.DITOMOQLoadNoGroupDebit);
                lstNoGroupDebit.Add((int)CATCostType.DITOMOQUnLoadNoGroupDebit);
                lstNoGroupDebit.Add((int)CATCostType.DITOExNoGroupDebit);
                lstNoGroupDebit.Add((int)CATCostType.ManualFixDebit);
                #endregion

                var result = new Chart_Owner_Profit();
                result.ListData = new List<Chart_Owner_Profit_Data>();
                using (var model = new DataEntities())
                {
                    dtfrom = dtfrom.Date;
                    dtto = dtto.Date.AddDays(1);

                    var lstCustomerID = new List<int>();
                    if (customerid > 0)
                        lstCustomerID.Add(customerid.Value);
                    else
                    {
                        string ViewAdmin = SYSViewCode.ViewAdmin.ToString();
                        lstCustomerID = model.CUS_Customer.Where(c => (c.TypeOfCustomerID == -(int)SYSVarType.TypeOfCustomerCUS || c.TypeOfCustomerID == -(int)SYSVarType.TypeOfCustomerBOTH) && !c.IsSystem && (Account.ListActionCode.Contains(ViewAdmin) || Account.ListCustomerID.Contains(c.ID))).Select(c => c.ID).ToList();
                    }

                    var lstFIN = model.FIN_PLGroupOfProduct.Where(c => c.GroupOfProductID > 0 && c.OPS_DITOGroupProduct.ORD_GroupProduct.ORD_Order.SYSCustomerID == Account.SYSCustomerID &&
                        c.FIN_PLDetails.FIN_PL.FINPLTypeID == -(int)SYSVarType.FINPLTypePL &&
                        c.OPS_DITOGroupProduct.ORD_GroupProduct.ORD_Order.DateConfig >= dtfrom && c.OPS_DITOGroupProduct.ORD_GroupProduct.ORD_Order.DateConfig < dtto &&
                        lstCustomerID.Contains(c.OPS_DITOGroupProduct.ORD_GroupProduct.ORD_Order.CustomerID)).Select(c => new
                        {
                            CostID = c.FIN_PLDetails.CostID,
                            Credit = c.FIN_PLDetails.Credit,
                            Debit = c.FIN_PLDetails.Debit
                        }).ToList();

                    //Other
                    lstFIN.AddRange(model.FIN_PLGroupOfProduct.Where(c => c.OPS_DITOGroupProduct.OrderGroupProductID > 0 && c.OPS_DITOGroupProduct.ORD_GroupProduct.ORD_Order.SYSCustomerID == Account.SYSCustomerID && c.OPS_DITOGroupProduct.DITOMasterID > 0 &&
                        c.FIN_PLDetails.FIN_PL.Effdate >= dtfrom && c.FIN_PLDetails.FIN_PL.Effdate < dtto && lstNoGroupCredit.Contains(c.FIN_PLDetails.CostID) && c.FIN_PLDetails.FIN_PL.FINPLTypeID == -(int)SYSVarType.FINPLTypePL &&
                        lstCustomerID.Contains(c.OPS_DITOGroupProduct.ORD_GroupProduct.ORD_Order.CustomerID)).Select(c => new
                        {
                            CostID = c.FIN_PLDetails.CostID,
                            Credit = c.FIN_PLDetails.Credit,
                            Debit = c.FIN_PLDetails.Debit
                        }).ToList());

                    lstFIN.AddRange(model.FIN_PLGroupOfProduct.Where(c => c.OPS_DITOGroupProduct.OrderGroupProductID > 0 && c.OPS_DITOGroupProduct.ORD_GroupProduct.ORD_Order.SYSCustomerID == Account.SYSCustomerID && c.OPS_DITOGroupProduct.DITOMasterID > 0 &&
                        c.FIN_PLDetails.FIN_PL.Effdate >= dtfrom && c.FIN_PLDetails.FIN_PL.Effdate < dtto && lstNoGroupDebit.Contains(c.FIN_PLDetails.CostID) && c.FIN_PLDetails.FIN_PL.FINPLTypeID == -(int)SYSVarType.FINPLTypePL &&
                        lstCustomerID.Contains(c.OPS_DITOGroupProduct.ORD_GroupProduct.ORD_Order.CustomerID)).Select(c => new
                        {
                            CostID = c.FIN_PLDetails.CostID,
                            Credit = c.FIN_PLDetails.Credit,
                            Debit = c.FIN_PLDetails.Debit
                        }).ToList());

                    Chart_Owner_Profit_Data itemNormal = new Chart_Owner_Profit_Data();
                    itemNormal.Category = "Vận chuyển";
                    result.ListData.Add(itemNormal);
                    var queryFIN = lstFIN.Where(c => c.CostID == (int)CATCostType.DITOFreightCredit);
                    if (queryFIN.Count() > 0)
                        itemNormal.Credit = queryFIN.Sum(c => c.Credit);

                    queryFIN = lstFIN.Where(c => c.CostID == (int)CATCostType.DITOFreightDebit);
                    if (queryFIN.Count() > 0)
                        itemNormal.Debit = queryFIN.Sum(c => c.Debit);

                    Chart_Owner_Profit_Data itemLoad = new Chart_Owner_Profit_Data();
                    itemLoad.Category = "Bốc xếp";
                    result.ListData.Add(itemLoad);
                    queryFIN = lstFIN.Where(c => (c.CostID == (int)CATCostType.DITOLoadCredit || c.CostID == (int)CATCostType.DITOUnLoadCredit || c.CostID == (int)CATCostType.DITOLoadReturnCredit || c.CostID == (int)CATCostType.DITOUnLoadReturnCredit));
                    if (queryFIN.Count() > 0)
                        itemLoad.Credit = queryFIN.Sum(c => c.Credit);

                    queryFIN = lstFIN.Where(c => (c.CostID == (int)CATCostType.DITOLoadDebit || c.CostID == (int)CATCostType.DITOUnLoadDebit || c.CostID == (int)CATCostType.DITOLoadReturnDebit || c.CostID == (int)CATCostType.DITOUnLoadReturnDebit));
                    if (queryFIN.Count() > 0)
                        itemLoad.Debit = queryFIN.Sum(c => c.Debit);

                    Chart_Owner_Profit_Data itemEx = new Chart_Owner_Profit_Data();
                    itemEx.Category = "Phụ thu";
                    result.ListData.Add(itemEx);
                    queryFIN = lstFIN.Where(c => c.CostID == (int)CATCostType.DITOExCredit || c.CostID == (int)CATCostType.DITOExNoGroupCredit);
                    if (queryFIN.Count() > 0)
                        itemEx.Credit = queryFIN.Sum(c => c.Credit);

                    queryFIN = lstFIN.Where(c => c.CostID == (int)CATCostType.DITOExDebit || c.CostID == (int)CATCostType.DITOExNoGroupDebit);
                    if (queryFIN.Count() > 0)
                        itemEx.Debit = queryFIN.Sum(c => c.Debit);

                    Chart_Owner_Profit_Data itemTrouble = new Chart_Owner_Profit_Data();
                    itemTrouble.Category = "Phát sinh";
                    result.ListData.Add(itemTrouble);
                    queryFIN = lstFIN.Where(c => c.CostID == (int)CATCostType.TroubleCredit);
                    if (queryFIN.Count() > 0)
                        itemTrouble.Credit = queryFIN.Sum(c => c.Credit);

                    queryFIN = lstFIN.Where(c => c.CostID == (int)CATCostType.TroubleDebit);
                    if (queryFIN.Count() > 0)
                        itemTrouble.Debit = queryFIN.Sum(c => c.Debit);
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

        // Chart 15, 20: Doanh thu, chi phí theo khách hàng
        public Chart_Owner_Profit_Customer Chart_Owner_Profit_Customer_Data(DateTime dtfrom, DateTime dtto, int? customerid)
        {
            try
            {
                var result = new Chart_Owner_Profit_Customer();
                result.ListData = new List<Chart_Owner_Profit_Customer_Data>();
                using (var model = new DataEntities())
                {
                    dtfrom = dtfrom.Date;
                    dtto = dtto.Date.AddDays(1);

                    List<int> lstCustomerID = new List<int>();
                    string ViewAdmin = SYSViewCode.ViewAdmin.ToString();
                    if (customerid.HasValue)
                        lstCustomerID.Add(customerid.Value);
                    else
                        lstCustomerID = model.CUS_Customer.Where(c => (c.TypeOfCustomerID == -(int)SYSVarType.TypeOfCustomerCUS || c.TypeOfCustomerID == -(int)SYSVarType.TypeOfCustomerBOTH) && !c.IsSystem && (Account.ListActionCode.Contains(ViewAdmin) || Account.ListCustomerID.Contains(c.ID))).Select(c => c.ID).ToList();

                    var lstCustomer = model.CUS_Customer.Select(c => new
                        {
                            c.ID,
                            c.CustomerName,
                            c.Code,
                        }).ToList();

                    var lstFIN = model.FIN_PLGroupOfProduct.Where(c => c.GroupOfProductID > 0 && c.OPS_DITOGroupProduct.ORD_GroupProduct.ORD_Order.SYSCustomerID == Account.SYSCustomerID &&
                        c.FIN_PLDetails.FIN_PL.FINPLTypeID == -(int)SYSVarType.FINPLTypePL &&
                        c.FIN_PLDetails.FIN_PL.Effdate >= dtfrom && c.FIN_PLDetails.FIN_PL.Effdate < dtto &&
                        lstCustomerID.Contains(c.OPS_DITOGroupProduct.ORD_GroupProduct.ORD_Order.CustomerID)).Select(c => new
                        {
                            CustomerID = c.OPS_DITOGroupProduct.ORD_GroupProduct.ORD_Order.CustomerID,
                            Credit = c.FIN_PLDetails.Credit,
                            Debit = c.FIN_PLDetails.Debit
                        }).ToList();

                    var lstFINCustomer = lstFIN.GroupBy(c => c.CustomerID);
                    foreach (var itemCustomer in lstFINCustomer)
                    {
                        var customer = lstCustomer.FirstOrDefault(c => c.ID == itemCustomer.Key);
                        if (customer != null)
                        {
                            Chart_Owner_Profit_Customer_Data item = new Chart_Owner_Profit_Customer_Data();
                            item.CustomerID = customer.ID;
                            item.CustomerName = customer.CustomerName;
                            item.CustomerCode = customer.Code;
                            item.Credit = itemCustomer.Sum(c => c.Credit);
                            item.Debit = itemCustomer.Sum(c => c.Debit);
                            result.ListData.Add(item);
                        }
                    }

                    if (result.ListData.Count > 0)
                    {
                        var TotalCredit = result.ListData.Sum(c => c.Credit);
                        foreach (var itemData in result.ListData)
                        {
                            if (TotalCredit > 0)
                                itemData.CreditPercent = (double)Math.Round((itemData.Credit / TotalCredit), 1) * 100;
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

        // Chart 16: Tỉ lệ đơn hàng bốc đúng giờ
        public Chart_Owner_OnTime Chart_Owner_OnTime_PickUp_Radial_Data(DateTime dtfrom, DateTime dtto, int? customerid)
        {
            try
            {
                var result = new Chart_Owner_OnTime();
                result.ListData = new List<Chart_Owner_OnTime_Data>();
                using (var model = new DataEntities())
                {
                    dtfrom = dtfrom.Date;
                    dtto = dtto.Date.AddDays(1);

                    var lstCustomerID = new List<int>();
                    if (customerid > 0)
                        lstCustomerID.Add(customerid.Value);
                    else
                    {
                        string ViewAdmin = SYSViewCode.ViewAdmin.ToString();
                        lstCustomerID = model.CUS_Customer.Where(c => (c.TypeOfCustomerID == -(int)SYSVarType.TypeOfCustomerCUS || c.TypeOfCustomerID == -(int)SYSVarType.TypeOfCustomerBOTH) && !c.IsSystem && (Account.ListActionCode.Contains(ViewAdmin) || Account.ListCustomerID.Contains(c.ID))).Select(c => c.ID).ToList();
                    }

                    var query = model.KPI_KPITime.Where(c => c.IsKPI.HasValue && lstCustomerID.Contains(c.CustomerID) && c.KPIID == (int)KPICode.Export && c.DateData >= dtfrom && c.DateData < dtto && c.OrderID > 0 && c.ORD_Order.SYSCustomerID == Account.SYSCustomerID).Select(c => new
                    {
                        c.DateData,
                        IsKPI = c.IsKPI.Value
                    }).ToList();

                    result.Total = query.Count;
                    result.TotalKPI = query.Count(c => c.IsKPI);
                    if (result.Total > 0)
                        result.Percent = (double)Math.Round((result.TotalKPI / result.Total), 1) * 100;
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

        // Chart 17: Tỉ lệ đơn hàng giao đúng giờ
        public Chart_Owner_OnTime Chart_Owner_OnTime_Delivery_Radial_Data(DateTime dtfrom, DateTime dtto, int? customerid)
        {
            try
            {
                var result = new Chart_Owner_OnTime();
                result.ListData = new List<Chart_Owner_OnTime_Data>();
                using (var model = new DataEntities())
                {
                    dtfrom = dtfrom.Date;
                    dtto = dtto.Date.AddDays(1);

                    var lstCustomerID = new List<int>();
                    if (customerid > 0)
                        lstCustomerID.Add(customerid.Value);
                    else
                    {
                        string ViewAdmin = SYSViewCode.ViewAdmin.ToString();
                        lstCustomerID = model.CUS_Customer.Where(c => (c.TypeOfCustomerID == -(int)SYSVarType.TypeOfCustomerCUS || c.TypeOfCustomerID == -(int)SYSVarType.TypeOfCustomerBOTH) && !c.IsSystem && (Account.ListActionCode.Contains(ViewAdmin) || Account.ListCustomerID.Contains(c.ID))).Select(c => c.ID).ToList();
                    }

                    var query = model.KPI_KPITime.Where(c => c.IsKPI.HasValue && lstCustomerID.Contains(c.CustomerID) && c.KPIID == (int)KPICode.OPS && c.DateData >= dtfrom && c.DateData < dtto && c.OrderID > 0 && c.ORD_Order.SYSCustomerID == Account.SYSCustomerID).Select(c => new
                    {
                        c.DateData,
                        IsKPI = c.IsKPI.Value
                    }).ToList();

                    result.Total = query.Count;
                    result.TotalKPI = query.Count(c => c.IsKPI);
                    if (result.Total > 0)
                        result.Percent = (double)Math.Round((result.TotalKPI / result.Total), 1) * 100;
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

        // Chart 18: Tỉ lệ chứng từ nhận đúng giờ
        public Chart_Owner_OnTime Chart_Owner_OnTime_POD_Radial_Data(DateTime dtfrom, DateTime dtto, int? customerid)
        {
            try
            {
                var result = new Chart_Owner_OnTime();
                result.ListData = new List<Chart_Owner_OnTime_Data>();
                using (var model = new DataEntities())
                {
                    dtfrom = dtfrom.Date;
                    dtto = dtto.Date.AddDays(1);

                    var lstCustomerID = new List<int>();
                    if (customerid > 0)
                        lstCustomerID.Add(customerid.Value);
                    else
                    {
                        string ViewAdmin = SYSViewCode.ViewAdmin.ToString();
                        lstCustomerID = model.CUS_Customer.Where(c => (c.TypeOfCustomerID == -(int)SYSVarType.TypeOfCustomerCUS || c.TypeOfCustomerID == -(int)SYSVarType.TypeOfCustomerBOTH) && !c.IsSystem && (Account.ListActionCode.Contains(ViewAdmin) || Account.ListCustomerID.Contains(c.ID))).Select(c => c.ID).ToList();
                    }

                    var query = model.KPI_KPITime.Where(c => c.IsKPI.HasValue && lstCustomerID.Contains(c.CustomerID) && c.KPIID == (int)KPICode.POD && c.DateData >= dtfrom && c.DateData < dtto && c.OrderID > 0 && c.ORD_Order.SYSCustomerID == Account.SYSCustomerID).Select(c => new
                    {
                        c.DateData,
                        IsKPI = c.IsKPI.Value
                    }).ToList();

                    result.Total = query.Count;
                    result.TotalKPI = query.Count(c => c.IsKPI);
                    if (result.Total > 0)
                        result.Percent = (double)Math.Round((result.TotalKPI / result.Total), 1) * 100;
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

        // Chart 19, 20: Tỷ lệ, Chi phí theo vendor
        public Chart_Owner_Profit_Customer Chart_Owner_Profit_Vendor_Data(DateTime dtfrom, DateTime dtto)
        {
            try
            {
                #region Temp Cost
                List<int> lstNoGroupDebit = new List<int>();
                lstNoGroupDebit.Add((int)CATCostType.DITOFreightNoGroupDebit);
                lstNoGroupDebit.Add((int)CATCostType.DITOMOQNoGroupDebit);
                lstNoGroupDebit.Add((int)CATCostType.DITOMOQLoadNoGroupDebit);
                lstNoGroupDebit.Add((int)CATCostType.DITOMOQUnLoadNoGroupDebit);
                lstNoGroupDebit.Add((int)CATCostType.DITOExNoGroupDebit);
                lstNoGroupDebit.Add((int)CATCostType.ManualFixDebit);
                #endregion

                var result = new Chart_Owner_Profit_Customer();
                result.ListData = new List<Chart_Owner_Profit_Customer_Data>();
                using (var model = new DataEntities())
                {
                    dtfrom = dtfrom.Date;
                    dtto = dtto.Date.AddDays(1);

                    string ViewAdmin = SYSViewCode.ViewAdmin.ToString();
                    var lstCustomerID = model.CUS_Customer.Where(c => (c.TypeOfCustomerID == -(int)SYSVarType.TypeOfCustomerVEN || c.TypeOfCustomerID == -(int)SYSVarType.TypeOfCustomerBOTH) && !c.IsSystem && (Account.ListActionCode.Contains(ViewAdmin) || Account.ListCustomerID.Contains(c.ID))).Select(c => c.ID).ToList();

                    var lstCustomer = model.CUS_Customer.Select(c => new
                    {
                        c.ID,
                        c.CustomerName,
                        c.Code,
                    }).ToList();

                    var lstFIN = model.FIN_PLGroupOfProduct.Where(c => c.GroupOfProductID > 0 && c.OPS_DITOGroupProduct.ORD_GroupProduct.ORD_Order.SYSCustomerID == Account.SYSCustomerID &&
                        c.FIN_PLDetails.FIN_PL.FINPLTypeID == -(int)SYSVarType.FINPLTypePL && c.FIN_PLDetails.FIN_PL.VendorID > 0 &&
                        c.OPS_DITOGroupProduct.ORD_GroupProduct.ORD_Order.DateConfig >= dtfrom && c.OPS_DITOGroupProduct.ORD_GroupProduct.ORD_Order.DateConfig < dtto &&
                        lstCustomerID.Contains(c.FIN_PLDetails.FIN_PL.VendorID.Value) && c.FIN_PLDetails.Debit > 0).Select(c => new
                        {
                            CustomerID = c.FIN_PLDetails.FIN_PL.VendorID,
                            Debit = c.FIN_PLDetails.Debit
                        }).ToList();

                    lstFIN.AddRange(model.FIN_PLGroupOfProduct.Where(c => c.OPS_DITOGroupProduct.OrderGroupProductID > 0 && c.OPS_DITOGroupProduct.ORD_GroupProduct.ORD_Order.SYSCustomerID == Account.SYSCustomerID && c.OPS_DITOGroupProduct.DITOMasterID > 0 &&
                        c.FIN_PLDetails.FIN_PL.Effdate >= dtfrom && c.FIN_PLDetails.FIN_PL.Effdate < dtto && lstNoGroupDebit.Contains(c.FIN_PLDetails.CostID) && c.FIN_PLDetails.FIN_PL.FINPLTypeID == -(int)SYSVarType.FINPLTypePL && c.FIN_PLDetails.FIN_PL.VendorID > 0 &&
                        lstCustomerID.Contains(c.FIN_PLDetails.FIN_PL.VendorID.Value) && c.FIN_PLDetails.Debit > 0).Select(c => new
                        {
                            CustomerID = c.FIN_PLDetails.FIN_PL.VendorID,
                            Debit = c.FIN_PLDetails.Debit
                        }).ToList());

                    var lstFINCustomer = lstFIN.GroupBy(c => c.CustomerID);
                    foreach (var itemCustomer in lstFINCustomer)
                    {
                        var customer = lstCustomer.FirstOrDefault(c => c.ID == itemCustomer.Key);
                        if (customer != null)
                        {
                            Chart_Owner_Profit_Customer_Data item = new Chart_Owner_Profit_Customer_Data();
                            item.CustomerID = customer.ID;
                            item.CustomerCode = customer.Code;
                            item.CustomerName = customer.CustomerName;
                            item.Debit = itemCustomer.Sum(c => c.Debit);
                            result.ListData.Add(item);
                        }
                    }

                    if (result.ListData.Count > 0)
                    {
                        var TotalDebit = result.ListData.Sum(c => c.Debit);
                        foreach (var itemData in result.ListData)
                        {
                            if (TotalDebit > 0)
                                itemData.DebitPercent = (double)Math.Round((itemData.Debit / TotalDebit), 1) * 100;
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

        // Widget
        public Widget_Data Widget_Data(DateTime dtfrom, DateTime dtto, int typeofchart, int? customerid)
        {
            try
            {
                var result = new Widget_Data();
                using (var model = new DataEntities())
                {
                    dtfrom = dtfrom.Date;
                    dtto = dtto.Date.AddDays(1);
                    List<int> lstCustomerID = new List<int>();
                    string ViewAdmin = SYSViewCode.ViewAdmin.ToString();
                    if (customerid.HasValue)
                        lstCustomerID.Add(customerid.Value);
                    else
                        lstCustomerID = model.CUS_Customer.Where(c => (c.TypeOfCustomerID == -(int)SYSVarType.TypeOfCustomerCUS || c.TypeOfCustomerID == -(int)SYSVarType.TypeOfCustomerBOTH) && !c.IsSystem && (Account.ListActionCode.Contains(ViewAdmin) || Account.ListCustomerID.Contains(c.ID))).Select(c => c.ID).ToList();

                    switch (typeofchart)
                    {
                        #region Đơn hàng thành công
                        case 1:
                            var query = model.ORD_Order.Where(c => c.SYSCustomerID == Account.SYSCustomerID && c.RequestDate >= dtfrom && c.RequestDate < dtto && c.StatusOfOrderID >= -(int)SYSVarType.StatusOfOrderReceived && lstCustomerID.Contains(c.CustomerID));
                            if (query.Count() > 0)
                            {
                                result.Total = query.Count();
                                var lstOPSGroup = model.OPS_DITOGroupProduct.Where(c => c.ORD_GroupProduct.ORD_Order.SYSCustomerID == Account.SYSCustomerID && lstCustomerID.Contains(c.ORD_GroupProduct.ORD_Order.CustomerID) && c.ORD_GroupProduct.ORD_Order.RequestDate >= dtfrom && c.ORD_GroupProduct.ORD_Order.RequestDate < dtto && c.ORD_GroupProduct.ORD_Order.StatusOfOrderID >= -(int)SYSVarType.StatusOfOrderReceived && c.DITOGroupProductStatusID != -(int)SYSVarType.DITOGroupProductStatusCancel);
                                if (lstOPSGroup.Count() > 0)
                                {
                                    result.Ton = lstOPSGroup.Sum(c => c.TonTranfer);
                                    result.CBM = lstOPSGroup.Sum(c => c.CBMTranfer);
                                }
                                var lstOPSContainer = model.OPS_COTOContainer.Where(c => c.OPS_Container.ORD_Container.ORD_Order.SYSCustomerID == Account.SYSCustomerID && lstCustomerID.Contains(c.OPS_Container.ORD_Container.ORD_Order.CustomerID) && c.OPS_Container.ORD_Container.ORD_Order.RequestDate >= dtfrom && c.OPS_Container.ORD_Container.ORD_Order.RequestDate < dtto && c.OPS_Container.ORD_Container.ORD_Order.StatusOfOrderID >= -(int)SYSVarType.StatusOfOrderReceived).Select(c => new
                                {
                                    c.OPS_Container.ContainerID,
                                    c.TypeOfStatusContainerID,
                                    c.OPS_Container.ORD_Container.PackingID
                                }).ToList();
                                if (lstOPSContainer.Count > 0)
                                {
                                    foreach (var itemOPSContainer in lstOPSContainer.GroupBy(c => c.ContainerID))
                                    {
                                        if (itemOPSContainer.Count() == itemOPSContainer.Count(c => c.TypeOfStatusContainerID == -(int)SYSVarType.TypeOfStatusContainerCancel || c.TypeOfStatusContainerID == -(int)SYSVarType.TypeOfStatusContainerComplete))
                                        {
                                            if (itemOPSContainer.FirstOrDefault().PackingID == (int)CATPackingCOCode.CO20)
                                                result.Total20DC += 1;
                                            if (itemOPSContainer.FirstOrDefault().PackingID == (int)CATPackingCOCode.CO40)
                                                result.Total40DC += 1;
                                            if (itemOPSContainer.FirstOrDefault().PackingID == (int)CATPackingCOCode.CO40H)
                                                result.Total40HC += 1;
                                        }
                                    }
                                }
                            }
                            break;
                        #endregion

                        #region Đơn hàng lập kế hoạch
                        case 2:
                            query = model.ORD_Order.Where(c => c.SYSCustomerID == Account.SYSCustomerID && lstCustomerID.Contains(c.CustomerID) && c.RequestDate >= dtfrom && c.RequestDate < dtto && c.StatusOfOrderID > -(int)SYSVarType.StatusOfOrderNew && c.StatusOfOrderID < -(int)SYSVarType.StatusOfOrderTranfer && lstCustomerID.Contains(c.CustomerID));
                            if (query.Count() > 0)
                            {
                                result.Total = query.Count();
                                var lstOPSGroup = model.OPS_DITOGroupProduct.Where(c => c.ORD_GroupProduct.ORD_Order.SYSCustomerID == Account.SYSCustomerID && lstCustomerID.Contains(c.ORD_GroupProduct.ORD_Order.CustomerID) && c.ORD_GroupProduct.ORD_Order.RequestDate >= dtfrom && c.ORD_GroupProduct.ORD_Order.RequestDate < dtto && c.ORD_GroupProduct.ORD_Order.StatusOfOrderID > -(int)SYSVarType.StatusOfOrderNew && c.ORD_GroupProduct.ORD_Order.StatusOfOrderID < -(int)SYSVarType.StatusOfOrderTranfer && c.DITOGroupProductStatusID != -(int)SYSVarType.DITOGroupProductStatusCancel && c.DITOGroupProductStatusID != -(int)SYSVarType.DITOGroupProductStatusComplete);
                                if (lstOPSGroup.Count() > 0)
                                {
                                    result.Ton = lstOPSGroup.Sum(c => c.TonTranfer);
                                    result.CBM = lstOPSGroup.Sum(c => c.CBMTranfer);
                                }
                                var lstOPSContainer = model.OPS_COTOContainer.Where(c => c.OPS_Container.ORD_Container.ORD_Order.SYSCustomerID == Account.SYSCustomerID && lstCustomerID.Contains(c.OPS_Container.ORD_Container.ORD_Order.CustomerID) && c.OPS_Container.ORD_Container.ORD_Order.RequestDate >= dtfrom && c.OPS_Container.ORD_Container.ORD_Order.RequestDate < dtto && c.OPS_Container.ORD_Container.ORD_Order.StatusOfOrderID > -(int)SYSVarType.StatusOfOrderNew && c.OPS_Container.ORD_Container.ORD_Order.StatusOfOrderID < -(int)SYSVarType.StatusOfOrderTranfer).Select(c => new
                                {
                                    c.OPS_Container.ContainerID,
                                    c.TypeOfStatusContainerID,
                                    c.OPS_Container.ORD_Container.PackingID
                                }).ToList();
                                if (lstOPSContainer.Count > 0)
                                {
                                    foreach (var itemOPSContainer in lstOPSContainer.GroupBy(c => c.ContainerID))
                                    {
                                        if (itemOPSContainer.Count() != itemOPSContainer.Count(c => c.TypeOfStatusContainerID == -(int)SYSVarType.TypeOfStatusContainerCancel || c.TypeOfStatusContainerID == -(int)SYSVarType.TypeOfStatusContainerComplete))
                                        {
                                            if (itemOPSContainer.FirstOrDefault().PackingID == (int)CATPackingCOCode.CO20)
                                                result.Total20DC += 1;
                                            if (itemOPSContainer.FirstOrDefault().PackingID == (int)CATPackingCOCode.CO40)
                                                result.Total40DC += 1;
                                            if (itemOPSContainer.FirstOrDefault().PackingID == (int)CATPackingCOCode.CO40H)
                                                result.Total40HC += 1;
                                        }
                                    }
                                }
                            }
                            break;
                        #endregion

                        #region Vận chuyển chờ duyệt
                        case 3:
                            var queryDI = model.OPS_DITOMaster.Where(c => c.SYSCustomerID == Account.SYSCustomerID && c.ETD >= dtfrom && c.ETD < dtto && c.StatusOfDITOMasterID >= -(int)SYSVarType.StatusOfDITOMasterPlanning && c.StatusOfDITOMasterID < -(int)SYSVarType.StatusOfDITOMasterTendered && c.OPS_DITOGroupProduct.Count > 0 && c.OPS_DITOGroupProduct.Any(d => lstCustomerID.Contains(d.ORD_GroupProduct.ORD_Order.CustomerID)));
                            if (queryDI.Count() > 0)
                            {
                                result.Total = queryDI.Count();
                                var lstOPSGroup = model.OPS_DITOGroupProduct.Where(c => c.ORD_GroupProduct.ORD_Order.SYSCustomerID == Account.SYSCustomerID && lstCustomerID.Contains(c.ORD_GroupProduct.ORD_Order.CustomerID) && c.DITOMasterID > 0 && c.OPS_DITOMaster.ETD >= dtfrom && c.OPS_DITOMaster.ETD < dtto && c.OPS_DITOMaster.StatusOfDITOMasterID < -(int)SYSVarType.StatusOfDITOMasterTendered && c.DITOGroupProductStatusID != -(int)SYSVarType.DITOGroupProductStatusCancel);
                                if (lstOPSGroup.Count() > 0)
                                {
                                    result.Ton = lstOPSGroup.Sum(c => c.TonTranfer);
                                    result.CBM = lstOPSGroup.Sum(c => c.CBMTranfer);
                                }
                            }
                            var queryCO = model.OPS_COTOMaster.Where(c => c.SYSCustomerID == Account.SYSCustomerID && c.ETD >= dtfrom && c.ETD < dtto && c.StatusOfCOTOMasterID >= -(int)SYSVarType.StatusOfCOTOMasterPlanning && c.StatusOfCOTOMasterID < -(int)SYSVarType.StatusOfCOTOMasterTendered && c.OPS_COTOContainer.Count > 0 && c.OPS_COTOContainer.Any(d => lstCustomerID.Contains(d.OPS_Container.ORD_Container.ORD_Order.CustomerID)));
                            if (queryCO.Count() > 0)
                            {
                                result.Total += queryCO.Count();
                                var lstOPSContainer = model.OPS_COTOContainer.Where(c => c.OPS_Container.ORD_Container.ORD_Order.SYSCustomerID == Account.SYSCustomerID && lstCustomerID.Contains(c.OPS_Container.ORD_Container.ORD_Order.CustomerID) && c.COTOMasterID > 0 && c.OPS_COTOMaster.ETD >= dtfrom && c.OPS_COTOMaster.ETD < dtto && c.OPS_COTOMaster.StatusOfCOTOMasterID < -(int)SYSVarType.StatusOfCOTOMasterTendered).Select(c => new
                                {
                                    c.OPS_Container.ContainerID,
                                    c.TypeOfStatusContainerID,
                                    c.OPS_Container.ORD_Container.PackingID
                                }).ToList();
                                if (lstOPSContainer.Count > 0)
                                {
                                    foreach (var itemOPSContainer in lstOPSContainer.GroupBy(c => c.ContainerID))
                                    {
                                        if (itemOPSContainer.FirstOrDefault().PackingID == (int)CATPackingCOCode.CO20)
                                            result.Total20DC += 1;
                                        if (itemOPSContainer.FirstOrDefault().PackingID == (int)CATPackingCOCode.CO40)
                                            result.Total40DC += 1;
                                        if (itemOPSContainer.FirstOrDefault().PackingID == (int)CATPackingCOCode.CO40H)
                                            result.Total40HC += 1;
                                    }
                                }
                            }
                            break;
                        #endregion

                        #region Vận chuyển đã duyệt
                        case 4:
                            queryDI = model.OPS_DITOMaster.Where(c => c.SYSCustomerID == Account.SYSCustomerID && c.ETD >= dtfrom && c.ETD < dtto && c.StatusOfDITOMasterID >= -(int)SYSVarType.StatusOfDITOMasterTendered && c.OPS_DITOGroupProduct.Count > 0 && c.OPS_DITOGroupProduct.Any(d => lstCustomerID.Contains(d.ORD_GroupProduct.ORD_Order.CustomerID)));
                            if (queryDI.Count() > 0)
                            {
                                result.Total = queryDI.Count();
                                var lstOPSGroup = model.OPS_DITOGroupProduct.Where(c => c.ORD_GroupProduct.ORD_Order.SYSCustomerID == Account.SYSCustomerID && lstCustomerID.Contains(c.ORD_GroupProduct.ORD_Order.CustomerID) && c.DITOMasterID > 0 && c.OPS_DITOMaster.ETD >= dtfrom && c.OPS_DITOMaster.ETD < dtto && c.OPS_DITOMaster.StatusOfDITOMasterID >= -(int)SYSVarType.StatusOfDITOMasterTendered && c.DITOGroupProductStatusID != -(int)SYSVarType.DITOGroupProductStatusCancel);
                                if (lstOPSGroup.Count() > 0)
                                {
                                    result.Ton = lstOPSGroup.Sum(c => c.TonTranfer);
                                    result.CBM = lstOPSGroup.Sum(c => c.CBMTranfer);
                                }
                            }
                            queryCO = model.OPS_COTOMaster.Where(c => c.SYSCustomerID == Account.SYSCustomerID && c.ETD >= dtfrom && c.ETD < dtto && c.StatusOfCOTOMasterID >= -(int)SYSVarType.StatusOfCOTOMasterPlanning && c.StatusOfCOTOMasterID < -(int)SYSVarType.StatusOfCOTOMasterTendered && c.OPS_COTOContainer.Count > 0 && c.OPS_COTOContainer.Any(d => lstCustomerID.Contains(d.OPS_Container.ORD_Container.ORD_Order.CustomerID)));
                            if (queryCO.Count() > 0)
                            {
                                result.Total += queryCO.Count();
                                var lstOPSContainer = model.OPS_COTOContainer.Where(c => c.OPS_Container.ORD_Container.ORD_Order.SYSCustomerID == Account.SYSCustomerID && lstCustomerID.Contains(c.OPS_Container.ORD_Container.ORD_Order.CustomerID) && c.COTOMasterID > 0 && c.OPS_COTOMaster.ETD >= dtfrom && c.OPS_COTOMaster.ETD < dtto && c.OPS_COTOMaster.StatusOfCOTOMasterID >= -(int)SYSVarType.StatusOfCOTOMasterTendered).Select(c => new
                                {
                                    c.OPS_Container.ContainerID,
                                    c.TypeOfStatusContainerID,
                                    c.OPS_Container.ORD_Container.PackingID
                                }).ToList();
                                if (lstOPSContainer.Count > 0)
                                {
                                    foreach (var itemOPSContainer in lstOPSContainer.GroupBy(c => c.ContainerID))
                                    {
                                        if (itemOPSContainer.FirstOrDefault().PackingID == (int)CATPackingCOCode.CO20)
                                            result.Total20DC += 1;
                                        if (itemOPSContainer.FirstOrDefault().PackingID == (int)CATPackingCOCode.CO40)
                                            result.Total40DC += 1;
                                        if (itemOPSContainer.FirstOrDefault().PackingID == (int)CATPackingCOCode.CO40H)
                                            result.Total40HC += 1;
                                    }
                                }
                            }
                            break;
                        #endregion

                        #region Vận chuyển đã hủy
                        case 5:
                            var lstGroup = model.OPS_DITOGroupProduct.Where(c => c.ORD_GroupProduct.ORD_Order.SYSCustomerID == Account.SYSCustomerID && c.ORD_GroupProduct.ORD_Order.RequestDate >= dtfrom && c.ORD_GroupProduct.ORD_Order.RequestDate < dtto && lstCustomerID.Contains(c.ORD_GroupProduct.ORD_Order.CustomerID) && c.DITOGroupProductStatusID == -(int)SYSVarType.DITOGroupProductStatusCancel);
                            if (lstGroup.Count() > 0)
                            {
                                result.Total = lstGroup.Count();
                                result.Ton = lstGroup.Sum(c => c.TonTranfer);
                                result.CBM = lstGroup.Sum(c => c.CBMTranfer);
                            }
                            break;
                        #endregion

                        #region Vận chuyển đã hoàn tất
                        case 6:
                            queryDI = model.OPS_DITOMaster.Where(c => c.SYSCustomerID == Account.SYSCustomerID && c.ATD >= dtfrom && c.ATD < dtto && c.StatusOfDITOMasterID >= -(int)SYSVarType.StatusOfDITOMasterReceived && c.OPS_DITOGroupProduct.Count > 0 && c.OPS_DITOGroupProduct.Any(d => lstCustomerID.Contains(d.ORD_GroupProduct.ORD_Order.CustomerID)));
                            if (queryDI.Count() > 0)
                            {
                                result.Total = queryDI.Count();
                                var lstOPSGroup = model.OPS_DITOGroupProduct.Where(c => c.ORD_GroupProduct.ORD_Order.SYSCustomerID == Account.SYSCustomerID && lstCustomerID.Contains(c.ORD_GroupProduct.ORD_Order.CustomerID) && c.DITOMasterID > 0 && c.OPS_DITOMaster.ATD >= dtfrom && c.OPS_DITOMaster.ATD < dtto && c.OPS_DITOMaster.StatusOfDITOMasterID >= -(int)SYSVarType.StatusOfDITOMasterReceived && c.DITOGroupProductStatusID != -(int)SYSVarType.DITOGroupProductStatusCancel);
                                if (lstOPSGroup.Count() > 0)
                                {
                                    result.Ton = lstOPSGroup.Sum(c => c.TonTranfer);
                                    result.CBM = lstOPSGroup.Sum(c => c.CBMTranfer);
                                }
                            }
                            queryCO = model.OPS_COTOMaster.Where(c => c.SYSCustomerID == Account.SYSCustomerID && c.ATD >= dtfrom && c.ATD < dtto && c.StatusOfCOTOMasterID >= -(int)SYSVarType.StatusOfCOTOMasterReceived && c.OPS_COTOContainer.Count > 0 && c.OPS_COTOContainer.Any(d => lstCustomerID.Contains(d.OPS_Container.ORD_Container.ORD_Order.CustomerID)));
                            if (queryCO.Count() > 0)
                            {
                                result.Total += queryCO.Count();
                                var lstOPSContainer = model.OPS_COTOContainer.Where(c => c.OPS_Container.ORD_Container.ORD_Order.SYSCustomerID == Account.SYSCustomerID && lstCustomerID.Contains(c.OPS_Container.ORD_Container.ORD_Order.CustomerID) && c.COTOMasterID > 0 && c.OPS_COTOMaster.ATD >= dtfrom && c.OPS_COTOMaster.ATD < dtto && c.OPS_COTOMaster.StatusOfCOTOMasterID >= -(int)SYSVarType.StatusOfCOTOMasterReceived).Select(c => new
                                {
                                    c.OPS_Container.ContainerID,
                                    c.TypeOfStatusContainerID,
                                    c.OPS_Container.ORD_Container.PackingID
                                }).ToList();
                                if (lstOPSContainer.Count > 0)
                                {
                                    foreach (var itemOPSContainer in lstOPSContainer.GroupBy(c => c.ContainerID))
                                    {
                                        if (itemOPSContainer.FirstOrDefault().PackingID == (int)CATPackingCOCode.CO20)
                                            result.Total20DC += 1;
                                        if (itemOPSContainer.FirstOrDefault().PackingID == (int)CATPackingCOCode.CO40)
                                            result.Total40DC += 1;
                                        if (itemOPSContainer.FirstOrDefault().PackingID == (int)CATPackingCOCode.CO40H)
                                            result.Total40HC += 1;
                                    }
                                }
                            }
                            break;
                        #endregion
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

        // Map
        public MAP_Summary MAP_Summary_Data(string request, List<int> lstCustomerID, DateTime dtfrom, DateTime dtto, int provinceID, int typeoflocationID)
        {
            try
            {
                const int iTon = -(int)SYSVarType.PriceOfGOPTon;
                const int iCBM = -(int)SYSVarType.PriceOfGOPCBM;
                //const int iTU = -(int)SYSVarType.PriceOfGOPTU;

                MAP_Summary result = new MAP_Summary();
                result.ListData = new List<MAP_Summary_Data>();
                result.ListMarker = new List<MAP_Summary_Marker>();
                result.ListMarkerLegend = new List<MAP_Summary_Marker_Legend>();
                result.ListMaster = new List<MAP_Summary_Master>();
                result.ListColor = new List<string> { "#FFA500", "#FF8C00", "#FF7F50", "#FF6347", "#FF4500" };
                using (var model = new DataEntities())
                {
                    dtfrom = dtfrom.Date;
                    dtto = dtto.AddDays(1).Date;

                    if (lstCustomerID == null || lstCustomerID.Count == 0)
                    {
                        string ViewAdmin = SYSViewCode.ViewAdmin.ToString();
                        lstCustomerID = model.CUS_Customer.Where(c => (c.TypeOfCustomerID == -(int)SYSVarType.TypeOfCustomerCUS || c.TypeOfCustomerID == -(int)SYSVarType.TypeOfCustomerBOTH) && !c.IsSystem && (Account.ListActionCode.Contains(ViewAdmin) || Account.ListCustomerID.Contains(c.ID))).Select(c => c.ID).ToList();
                    }

                    var queryLTL = model.OPS_DITOGroupProduct.Where(c => c.DITOMasterID > 0 && (c.OPS_DITOMaster.StatusOfDITOMasterID >= -(int)SYSVarType.StatusOfDITOMasterReceived) && lstCustomerID.Contains(c.ORD_GroupProduct.ORD_Order.CustomerID) && (provinceID > 0 ? c.ORD_GroupProduct.LocationToID.HasValue && c.ORD_GroupProduct.CUS_Location1.CAT_Location.ProvinceID == provinceID : true) &&
                    c.OPS_DITOMaster.SYSCustomerID == Account.SYSCustomerID && c.ORD_GroupProduct.DateConfig >= dtfrom && c.ORD_GroupProduct.DateConfig < dtto && c.DITOGroupProductStatusID != -(int)SYSVarType.DITOGroupProductStatusCancel && c.OPS_DITOMaster.VehicleID.HasValue).Select(c => new MAP_Summary_Data
                    {
                        ID = c.ID,
                        DITOMasterID = c.DITOMasterID.HasValue ? c.DITOMasterID.Value : -1,
                        DITOMasterCode = c.DITOMasterID.HasValue ? c.OPS_DITOMaster.Code : string.Empty,
                        VehicleID = c.OPS_DITOMaster.VehicleID.Value,
                        KM = c.DITOMasterID.HasValue && c.OPS_DITOMaster.KM.HasValue ? c.OPS_DITOMaster.KM.Value : 0,
                        CustomerID = c.ORD_GroupProduct.ORD_Order.CustomerID,
                        CustomerCode = c.ORD_GroupProduct.ORD_Order.CUS_Customer.Code,
                        OrderID = c.ORD_GroupProduct.OrderID,
                        OrderCode = c.ORD_GroupProduct.ORD_Order.Code,
                        OrderGroupProductID = c.OrderGroupProductID.Value,
                        SOCode = c.ORD_GroupProduct.SOCode,
                        DNCode = c.ORD_GroupProduct.DNCode,
                        PriceOfGOPID = c.ORD_GroupProduct.PriceOfGOPID.HasValue ? c.ORD_GroupProduct.PriceOfGOPID.Value : -1,
                        RequestDate = c.ORD_GroupProduct.ORD_Order.RequestDate,
                        ETD = c.DITOMasterID.HasValue ? c.OPS_DITOMaster.ATD.HasValue ? c.OPS_DITOMaster.ATD : c.OPS_DITOMaster.ETD : null,
                        ETA = c.DITOMasterID.HasValue ? c.OPS_DITOMaster.ATA.HasValue ? c.OPS_DITOMaster.ATA : c.OPS_DITOMaster.ETA : null,
                        DateConfig = c.ORD_GroupProduct.ORD_Order.DateConfig,
                        TonTranfer = c.TonTranfer,
                        CBMTranfer = c.CBMTranfer,
                        QuantityTranfer = c.QuantityTranfer,
                        TonReturn = c.TonReturn,
                        CBMReturn = c.CBMReturn,
                        QuantityReturn = c.QuantityReturn,
                        DITOGroupProductStatusPODID = c.DITOGroupProductStatusPODID,

                        DistributorID = c.ORD_GroupProduct.CUS_Location1.LocationID,
                        DistributorCode = c.ORD_GroupProduct.CUS_Location1.CAT_Location.Code,
                        DistributorName = c.ORD_GroupProduct.CUS_Location1.CAT_Location.Location,
                        DistributorProvinceID = c.ORD_GroupProduct.CUS_Location1.CAT_Location.ProvinceID,
                        DistributorDistrictID = c.ORD_GroupProduct.CUS_Location1.CAT_Location.DistrictID,
                        DistributorAddress = c.ORD_GroupProduct.CUS_Location1.CAT_Location.Address,
                        DistributorLat = c.ORD_GroupProduct.CUS_Location1.CAT_Location.Lat > 0 ? c.ORD_GroupProduct.CUS_Location1.CAT_Location.Lat : null,
                        DistributorLng = c.ORD_GroupProduct.CUS_Location1.CAT_Location.Lng > 0 ? c.ORD_GroupProduct.CUS_Location1.CAT_Location.Lng : null,

                        StockID = c.ORD_GroupProduct.CUS_Location.LocationID,
                        StockCode = c.ORD_GroupProduct.CUS_Location.CAT_Location.Code,
                        StockName = c.ORD_GroupProduct.CUS_Location.CAT_Location.Location,
                        StockProvinceID = c.ORD_GroupProduct.CUS_Location.CAT_Location.ProvinceID,
                        StockDistrictID = c.ORD_GroupProduct.CUS_Location.CAT_Location.DistrictID,
                        StockAddress = c.ORD_GroupProduct.CUS_Location.CAT_Location.Address,
                        StockLat = c.ORD_GroupProduct.CUS_Location.CAT_Location.Lat > 0 ? c.ORD_GroupProduct.CUS_Location.CAT_Location.Lat : null,
                        StockLng = c.ORD_GroupProduct.CUS_Location.CAT_Location.Lng > 0 ? c.ORD_GroupProduct.CUS_Location.CAT_Location.Lng : null,

                        DateFromLoadStart = c.DateFromLoadStart,
                        DateFromLoadEnd = c.DateFromLoadEnd,
                        DateToLoadStart = c.DateToLoadStart,
                        DateToLoadEnd = c.DateToLoadEnd,
                        IsReturn = c.ORD_GroupProduct.IsReturn.HasValue ? c.ORD_GroupProduct.IsReturn.Value : false,
                    }).ToDataSourceResult(CreateRequest(request));

                    result.ListData = queryLTL.Data.Cast<MAP_Summary_Data>().ToList();

                    var lstCATVehicle = model.CAT_Vehicle.Select(c => new
                    {
                        c.ID,
                        CBM = c.MaxCapacity > 0 ? c.MaxCapacity.Value : 0,
                        Ton = c.MaxWeightCal > 0 ? c.MaxWeightCal.Value : 0
                    }).ToList();

                    var lstLoading = model.FIN_PLGroupOfProduct.Where(c => c.GroupOfProductID > 0 && c.OPS_DITOGroupProduct.ORD_GroupProduct.DateConfig >= dtfrom && c.OPS_DITOGroupProduct.ORD_GroupProduct.DateConfig < dtto && c.FIN_PLDetails.FIN_PL.FINPLTypeID == -(int)SYSVarType.FINPLTypePL &&
                        (c.FIN_PLDetails.CostID == (int)CATCostType.DITOLoadCredit || c.FIN_PLDetails.CostID == (int)CATCostType.DITOUnLoadCredit)).Select(c => new
                        {
                            c.GroupOfProductID,
                            c.Quantity,
                            c.UnitPrice,
                            c.FIN_PLDetails.CostID
                        }).ToList();

                    foreach (var item in result.ListData)
                    {
                        // Bốc xếp
                        var lstLoadingGroup = lstLoading.Where(c => c.GroupOfProductID == item.ID && c.Quantity > 0);
                        if (lstLoadingGroup != null && lstLoadingGroup.Count() > 0)
                        {
                            var lstMyLoading = lstLoadingGroup.Where(c => c.CostID == (int)CATCostType.DITOLoadCredit);
                            var lstMyUnLoading = lstLoadingGroup.Where(c => c.CostID == (int)CATCostType.DITOUnLoadCredit);

                            switch (item.PriceOfGOPID)
                            {
                                case iTon:
                                    item.TonLoading = lstMyLoading != null ? lstMyLoading.Sum(c => c.Quantity) : 0;
                                    item.TonUnLoading = lstMyUnLoading != null ? lstMyUnLoading.Sum(c => c.Quantity) : 0;
                                    break;
                                case iCBM:
                                    item.CBMLoading = lstMyLoading != null ? lstMyLoading.Sum(c => c.Quantity) : 0;
                                    item.CBMUnLoading = lstMyUnLoading != null ? lstMyUnLoading.Sum(c => c.Quantity) : 0;
                                    break;
                            }
                        }

                        // Thời gian bốc xếp
                        if (item.DateFromLoadStart.HasValue && item.DateFromLoadEnd.HasValue)
                            item.TimeLoading = (item.DateFromLoadEnd.Value - item.DateFromLoadStart.Value).TotalHours;

                        if (item.DateToLoadStart.HasValue && item.DateToLoadEnd.HasValue)
                            item.TimeUnLoading = (item.DateToLoadEnd.Value - item.DateToLoadStart.Value).TotalHours;

                        if (item.DistributorLat > 0 && item.DistributorLng > 0)
                        {
                            var itemMarker = result.ListMarker.FirstOrDefault(c => c.ID == item.DistributorID);
                            if (typeoflocationID == 2)
                                itemMarker = result.ListMarker.FirstOrDefault(c => c.ID == item.StockID);
                            if (itemMarker == null)
                            {
                                itemMarker = new MAP_Summary_Marker
                                {
                                    ID = item.DistributorID,
                                    Code = item.DistributorCode,
                                    Name = item.DistributorName,
                                    ProvinceID = item.DistributorProvinceID,
                                    DistrictID = item.DistributorDistrictID,
                                    Address = item.DistributorAddress,
                                    Lat = item.DistributorLat,
                                    Lng = item.DistributorLng,
                                };

                                if (typeoflocationID == 2)
                                {
                                    itemMarker = new MAP_Summary_Marker
                                    {
                                        ID = item.StockID,
                                        Code = item.StockCode,
                                        Name = item.StockName,
                                        ProvinceID = item.StockProvinceID,
                                        DistrictID = item.StockDistrictID,
                                        Address = item.StockAddress,
                                        Lat = item.StockLat,
                                        Lng = item.StockLng,
                                    };
                                }

                                result.ListMarker.Add(itemMarker);
                            }

                            itemMarker.TonTranfer += item.TonTranfer;
                            itemMarker.CBMTranfer += item.CBMTranfer;
                            itemMarker.TonLoading += item.TonLoading;
                            itemMarker.CBMLoading += item.CBMLoading;
                            itemMarker.TonUnLoading += item.TonUnLoading;
                            itemMarker.CBMUnLoading += item.CBMUnLoading;
                            itemMarker.TonReturn += item.TonReturn;
                            itemMarker.CBMReturn += item.CBMReturn;
                            itemMarker.TimeLoading += item.TimeLoading;
                            itemMarker.TimeUnLoading += item.TimeUnLoading;
                        }

                        // Tổng hợp dữ liệu
                        result.TonTranfer += item.TonTranfer;
                        result.CBMTranfer += item.CBMTranfer;
                        result.TonReturn += item.TonReturn;
                        result.CBMReturn += item.CBMReturn;
                        result.TonLoading += item.TonLoading;
                        result.CBMLoading += item.CBMLoading;
                        result.TonUnLoading += item.TonUnLoading;
                        result.CBMUnLoading += item.CBMUnLoading;
                        result.TimeLoading += item.TimeLoading;
                        result.TimeUnLoading += item.TimeUnLoading;

                        // Làm tròn
                        item.TonTranfer = Math.Round(item.TonTranfer, 1);
                        item.CBMTranfer = Math.Round(item.CBMTranfer, 1);
                        item.TonReturn = Math.Round(item.TonReturn, 1);
                        item.CBMReturn = Math.Round(item.CBMReturn, 1);
                        item.TonLoading = Math.Round(item.TonLoading, 1);
                        item.CBMLoading = Math.Round(item.CBMLoading, 1);
                        item.TonUnLoading = Math.Round(item.TonUnLoading, 1);
                        item.CBMUnLoading = Math.Round(item.CBMUnLoading, 1);
                        item.TimeLoading = Math.Round(item.TimeLoading, 1);
                        item.TimeUnLoading = Math.Round(item.TimeUnLoading, 1);
                    }

                    // Tổng hợp thông tin khác
                    var lstMaster = result.ListData.Select(c => new { c.DITOMasterID, c.KM, c.VehicleID }).Distinct().ToList();
                    result.Schedule = lstMaster.Count;
                    result.KM = lstMaster.Sum(c => c.KM);
                    result.KMAverage = result.Schedule > 0 ? result.KM / result.Schedule : 0;
                    var lstVehicle = result.ListData.Select(c => c.VehicleID).Distinct().ToList();
                    result.Vehicle = lstVehicle.Count;
                    result.ScheduleEmpty = result.ListData.Where(c => c.IsReturn).Select(c => c.DITOMasterID).Distinct().Count();

                    // Tổng trọng tải của các chuyến
                    foreach (var item in lstVehicle)
                    {
                        var totalMaster = lstMaster.Where(c => c.VehicleID == item);
                        var vehicle = lstCATVehicle.FirstOrDefault(c => c.ID == item);
                        result.TonMax += totalMaster.Count() * vehicle.Ton;
                        result.CBMMax += totalMaster.Count() * vehicle.CBM;
                    }

                    // Tính legend
                    double maxTransfer = 0;
                    if (result.ListMarker.Count > 0)
                    {
                        // Ktra điểm chưa có thì đưa vào ListData
                        //maxTransfer = result.ListMarker.OrderByDescending(c => c.TonTranfer).FirstOrDefault().TonTranfer;
                        maxTransfer = 100;
                        int totalLegend = 5;
                        double baseUnit = maxTransfer / totalLegend;

                        for (int i = 0; i < 5; i++)
                        {
                            MAP_Summary_Marker_Legend legend = new MAP_Summary_Marker_Legend();
                            legend.Value = i * baseUnit;
                            legend.ValueFrom = i * baseUnit;
                            legend.ValueTo = (i + 1) * baseUnit;
                            legend.Radius = (i * 2) + 5;
                            legend.Color = result.ListColor[i];
                            result.ListMarkerLegend.Add(legend);
                        }
                    }

                    // Tính marker size
                    foreach (var item in result.ListMarker)
                    {
                        item.Radius = 5;
                        if (item.TonTranfer >= maxTransfer)
                        {
                            item.Radius = result.ListMarkerLegend.OrderByDescending(c => c.Value).FirstOrDefault().Radius;
                            item.Color = result.ListMarkerLegend.OrderByDescending(c => c.Value).FirstOrDefault().Color;
                        }
                        else
                        {
                            var radius = result.ListMarkerLegend.OrderBy(c => c.ValueTo).FirstOrDefault(c => (item.TonTranfer >= c.ValueFrom && item.TonTranfer < c.ValueTo));
                            if (radius != null)
                            {
                                item.Radius = radius.Radius;
                                item.Color = radius.Color;
                            }
                        }

                        // Làm tròn
                        item.TonTranfer = Math.Round(item.TonTranfer, 1);
                        item.CBMTranfer = Math.Round(item.CBMTranfer, 1);
                        item.TonReturn = Math.Round(item.TonReturn, 1);
                        item.CBMReturn = Math.Round(item.CBMReturn, 1);
                        item.TonLoading = Math.Round(item.TonLoading, 1);
                        item.CBMLoading = Math.Round(item.CBMLoading, 1);
                        item.TonUnLoading = Math.Round(item.TonUnLoading, 1);
                        item.CBMUnLoading = Math.Round(item.CBMUnLoading, 1);
                        item.TimeLoading = Math.Round(item.TimeLoading, 1);
                        item.TimeUnLoading = Math.Round(item.TimeUnLoading, 1);
                    }

                    // Làm tròn
                    result.TonMax = Math.Round(result.TonMax, 1);
                    result.CBMMax = Math.Round(result.CBMMax, 1);
                    result.TonTranfer = Math.Round(result.TonTranfer, 1);
                    result.CBMTranfer = Math.Round(result.CBMTranfer, 1);
                    result.TonReturn = Math.Round(result.TonReturn, 1);
                    result.CBMReturn = Math.Round(result.CBMReturn, 1);
                    result.TonLoading = Math.Round(result.TonLoading, 1);
                    result.CBMLoading = Math.Round(result.CBMLoading, 1);
                    result.TonUnLoading = Math.Round(result.TonUnLoading, 1);
                    result.CBMUnLoading = Math.Round(result.CBMUnLoading, 1);
                    result.TimeLoading = Math.Round(result.TimeLoading, 1);
                    result.TimeUnLoading = Math.Round(result.TimeUnLoading, 1);
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

        public MAP_Summary MAP_Summary_Vehicle_Data(string request, DateTime dtfrom, DateTime dtto, int vehicleID)
        {
            try
            {
                const int iTon = -(int)SYSVarType.PriceOfGOPTon;
                const int iCBM = -(int)SYSVarType.PriceOfGOPCBM;

                MAP_Summary result = new MAP_Summary();
                result.ListData = new List<MAP_Summary_Data>();
                result.ListMarker = new List<MAP_Summary_Marker>();
                result.ListMarkerLegend = new List<MAP_Summary_Marker_Legend>();
                using (var model = new DataEntities())
                {
                    dtfrom = dtfrom.Date;
                    dtto = dtto.AddDays(1).Date;

                    var queryLTL = model.OPS_DITOGroupProduct.Where(c => c.DITOMasterID > 0 && (c.OPS_DITOMaster.StatusOfDITOMasterID >= -(int)SYSVarType.StatusOfDITOMasterReceived) && c.OPS_DITOMaster.VendorOfVehicleID == Account.SYSCustomerID &&
                    c.OPS_DITOMaster.SYSCustomerID == Account.SYSCustomerID && c.ORD_GroupProduct.DateConfig >= dtfrom && c.ORD_GroupProduct.DateConfig < dtto && c.DITOGroupProductStatusID != -(int)SYSVarType.DITOGroupProductStatusCancel && c.OPS_DITOMaster.VehicleID.HasValue && (vehicleID > 0 ? c.OPS_DITOMaster.VehicleID == vehicleID : true)).Select(c => new MAP_Summary_Data
                    {
                        ID = c.ID,
                        DITOMasterID = c.DITOMasterID.HasValue ? c.DITOMasterID.Value : -1,
                        DITOMasterCode = c.DITOMasterID.HasValue ? c.OPS_DITOMaster.Code : string.Empty,
                        VehicleID = c.OPS_DITOMaster.VehicleID.Value,
                        VehicleCode = c.OPS_DITOMaster.CAT_Vehicle.RegNo,
                        KM = c.DITOMasterID.HasValue && c.OPS_DITOMaster.KM.HasValue ? c.OPS_DITOMaster.KM.Value : 0,
                        CustomerID = c.ORD_GroupProduct.ORD_Order.CustomerID,
                        CustomerCode = c.ORD_GroupProduct.ORD_Order.CUS_Customer.Code,
                        OrderID = c.ORD_GroupProduct.OrderID,
                        OrderCode = c.ORD_GroupProduct.ORD_Order.Code,
                        OrderGroupProductID = c.OrderGroupProductID.Value,
                        SOCode = c.ORD_GroupProduct.SOCode,
                        DNCode = c.ORD_GroupProduct.DNCode,
                        PriceOfGOPID = c.ORD_GroupProduct.PriceOfGOPID.HasValue ? c.ORD_GroupProduct.PriceOfGOPID.Value : -1,
                        RequestDate = c.ORD_GroupProduct.ORD_Order.RequestDate,
                        ETD = c.DITOMasterID.HasValue ? c.OPS_DITOMaster.ATD.HasValue ? c.OPS_DITOMaster.ATD : c.OPS_DITOMaster.ETD : null,
                        ETA = c.DITOMasterID.HasValue ? c.OPS_DITOMaster.ATA.HasValue ? c.OPS_DITOMaster.ATA : c.OPS_DITOMaster.ETA : null,
                        DateConfig = c.ORD_GroupProduct.ORD_Order.DateConfig,
                        TonTranfer = c.TonTranfer,
                        CBMTranfer = c.CBMTranfer,
                        QuantityTranfer = c.QuantityTranfer,
                        TonReturn = c.TonReturn,
                        CBMReturn = c.CBMReturn,
                        QuantityReturn = c.QuantityReturn,
                        DITOGroupProductStatusPODID = c.DITOGroupProductStatusPODID,

                        DistributorID = c.ORD_GroupProduct.CUS_Location1.LocationID,
                        DistributorCode = c.ORD_GroupProduct.CUS_Location1.CAT_Location.Code,
                        DistributorName = c.ORD_GroupProduct.CUS_Location1.CAT_Location.Location,
                        DistributorProvinceID = c.ORD_GroupProduct.CUS_Location1.CAT_Location.ProvinceID,
                        DistributorDistrictID = c.ORD_GroupProduct.CUS_Location1.CAT_Location.DistrictID,
                        DistributorAddress = c.ORD_GroupProduct.CUS_Location1.CAT_Location.Address,
                        DistributorLat = c.ORD_GroupProduct.CUS_Location1.CAT_Location.Lat > 0 ? c.ORD_GroupProduct.CUS_Location1.CAT_Location.Lat : null,
                        DistributorLng = c.ORD_GroupProduct.CUS_Location1.CAT_Location.Lng > 0 ? c.ORD_GroupProduct.CUS_Location1.CAT_Location.Lng : null,

                        StockID = c.ORD_GroupProduct.CUS_Location.LocationID,
                        StockCode = c.ORD_GroupProduct.CUS_Location.CAT_Location.Code,
                        StockName = c.ORD_GroupProduct.CUS_Location.CAT_Location.Location,
                        StockProvinceID = c.ORD_GroupProduct.CUS_Location.CAT_Location.ProvinceID,
                        StockDistrictID = c.ORD_GroupProduct.CUS_Location.CAT_Location.DistrictID,
                        StockAddress = c.ORD_GroupProduct.CUS_Location.CAT_Location.Address,
                        StockLat = c.ORD_GroupProduct.CUS_Location.CAT_Location.Lat > 0 ? c.ORD_GroupProduct.CUS_Location.CAT_Location.Lat : null,
                        StockLng = c.ORD_GroupProduct.CUS_Location.CAT_Location.Lng > 0 ? c.ORD_GroupProduct.CUS_Location.CAT_Location.Lng : null,

                        DateFromLoadStart = c.DateFromLoadStart,
                        DateFromLoadEnd = c.DateFromLoadEnd,
                        DateToLoadStart = c.DateToLoadStart,
                        DateToLoadEnd = c.DateToLoadEnd,
                        IsReturn = c.ORD_GroupProduct.IsReturn.HasValue ? c.ORD_GroupProduct.IsReturn.Value : false,
                    }).ToDataSourceResult(CreateRequest(request));

                    result.ListData = queryLTL.Data.Cast<MAP_Summary_Data>().ToList();


                    var lstCATVehicle = model.CAT_Vehicle.Select(c => new
                    {
                        c.ID,
                        CBM = c.MaxCapacity > 0 ? c.MaxCapacity.Value : 0,
                        Ton = c.MaxWeightCal > 0 ? c.MaxWeightCal.Value : 0
                    }).ToList();

                    var lstLoading = model.FIN_PLGroupOfProduct.Where(c => c.GroupOfProductID > 0 && c.OPS_DITOGroupProduct.ORD_GroupProduct.DateConfig >= dtfrom && c.OPS_DITOGroupProduct.ORD_GroupProduct.DateConfig < dtto && c.FIN_PLDetails.FIN_PL.FINPLTypeID == -(int)SYSVarType.FINPLTypePL &&
                        (c.FIN_PLDetails.CostID == (int)CATCostType.DITOLoadCredit || c.FIN_PLDetails.CostID == (int)CATCostType.DITOUnLoadCredit)).Select(c => new
                        {
                            c.GroupOfProductID,
                            c.Quantity,
                            c.UnitPrice,
                            c.FIN_PLDetails.CostID
                        }).ToList();

                    foreach (var item in result.ListData)
                    {
                        // Bốc xếp
                        var lstLoadingGroup = lstLoading.Where(c => c.GroupOfProductID == item.ID && c.Quantity > 0);
                        if (lstLoadingGroup != null && lstLoadingGroup.Count() > 0)
                        {
                            var lstMyLoading = lstLoadingGroup.Where(c => c.CostID == (int)CATCostType.DITOLoadCredit);
                            var lstMyUnLoading = lstLoadingGroup.Where(c => c.CostID == (int)CATCostType.DITOUnLoadCredit);

                            switch (item.PriceOfGOPID)
                            {
                                case iTon:
                                    item.TonLoading = lstMyLoading != null ? lstMyLoading.Sum(c => c.Quantity) : 0;
                                    item.TonUnLoading = lstMyUnLoading != null ? lstMyUnLoading.Sum(c => c.Quantity) : 0;
                                    break;
                                case iCBM:
                                    item.CBMLoading = lstMyLoading != null ? lstMyLoading.Sum(c => c.Quantity) : 0;
                                    item.CBMUnLoading = lstMyUnLoading != null ? lstMyUnLoading.Sum(c => c.Quantity) : 0;
                                    break;
                            }
                        }

                        // Thời gian bốc xếp
                        if (item.DateFromLoadStart.HasValue && item.DateFromLoadEnd.HasValue)
                            item.TimeLoading = (item.DateFromLoadEnd.Value - item.DateFromLoadStart.Value).TotalHours;

                        if (item.DateToLoadStart.HasValue && item.DateToLoadEnd.HasValue)
                            item.TimeUnLoading = (item.DateToLoadEnd.Value - item.DateToLoadStart.Value).TotalHours;

                        if (item.DistributorLat > 0 && item.DistributorLng > 0)
                        {
                            var itemMarker = result.ListMarker.FirstOrDefault(c => c.ID == item.DistributorID);
                            if (itemMarker == null)
                            {
                                itemMarker = new MAP_Summary_Marker
                                {
                                    ID = item.DistributorID,
                                    Code = item.DistributorCode,
                                    Name = item.DistributorName,
                                    ProvinceID = item.DistributorProvinceID,
                                    DistrictID = item.DistributorDistrictID,
                                    Address = item.DistributorAddress,
                                    Lat = item.DistributorLat,
                                    Lng = item.DistributorLng,
                                };

                                result.ListMarker.Add(itemMarker);
                            }

                            itemMarker.TonTranfer += item.TonTranfer;
                            itemMarker.CBMTranfer += item.CBMTranfer;
                            itemMarker.TonLoading += item.TonLoading;
                            itemMarker.CBMLoading += item.CBMLoading;
                            itemMarker.TonUnLoading += item.TonUnLoading;
                            itemMarker.CBMUnLoading += item.CBMUnLoading;
                            itemMarker.TonReturn += item.TonReturn;
                            itemMarker.CBMReturn += item.CBMReturn;
                            itemMarker.TimeLoading += item.TimeLoading;
                            itemMarker.TimeUnLoading += item.TimeUnLoading;
                        }

                        // Tổng hợp dữ liệu
                        result.TonTranfer += item.TonTranfer;
                        result.CBMTranfer += item.CBMTranfer;
                        result.TonReturn += item.TonReturn;
                        result.CBMReturn += item.CBMReturn;
                        result.TonLoading += item.TonLoading;
                        result.CBMLoading += item.CBMLoading;
                        result.TonUnLoading += item.TonUnLoading;
                        result.CBMUnLoading += item.CBMUnLoading;
                        result.TimeLoading += item.TimeLoading;
                        result.TimeUnLoading += item.TimeUnLoading;

                        // Làm tròn
                        item.TonTranfer = Math.Round(item.TonTranfer, 1);
                        item.CBMTranfer = Math.Round(item.CBMTranfer, 1);
                        item.TonReturn = Math.Round(item.TonReturn, 1);
                        item.CBMReturn = Math.Round(item.CBMReturn, 1);
                        item.TonLoading = Math.Round(item.TonLoading, 1);
                        item.CBMLoading = Math.Round(item.CBMLoading, 1);
                        item.TonUnLoading = Math.Round(item.TonUnLoading, 1);
                        item.CBMUnLoading = Math.Round(item.CBMUnLoading, 1);
                        item.TimeLoading = Math.Round(item.TimeLoading, 1);
                        item.TimeUnLoading = Math.Round(item.TimeUnLoading, 1);
                    }

                    // Tổng hợp thông tin khác
                    var lstMaster = result.ListData.Select(c => new { c.DITOMasterID, c.KM, c.VehicleID }).Distinct().ToList();
                    result.Schedule = lstMaster.Count;
                    result.KM = lstMaster.Sum(c => c.KM);
                    result.KMAverage = result.Schedule > 0 ? result.KM / result.Schedule : 0;
                    var lstVehicle = result.ListData.Select(c => c.VehicleID).Distinct().ToList();
                    result.Vehicle = lstVehicle.Count;
                    result.ScheduleEmpty = result.ListData.Where(c => c.IsReturn).Select(c => c.DITOMasterID).Distinct().Count();

                    // Tổng trọng tải của các chuyến
                    foreach (var item in lstVehicle)
                    {
                        var totalMaster = lstMaster.Where(c => c.VehicleID == item);
                        var vehicle = lstCATVehicle.FirstOrDefault(c => c.ID == item);
                        result.TonMax += totalMaster.Count() * vehicle.Ton;
                        result.CBMMax += totalMaster.Count() * vehicle.CBM;
                    }

                    // Tính legend
                    double maxTransfer = 0;
                    if (result.ListMarker.Count > 0)
                    {
                        // Ktra điểm chưa có thì đưa vào ListData
                        maxTransfer = result.ListMarker.OrderByDescending(c => c.TonTranfer).FirstOrDefault().TonTranfer;
                        int totalLegend = 5;
                        double baseUnit = maxTransfer / totalLegend;

                        for (int i = 0; i < 5; i++)
                        {
                            MAP_Summary_Marker_Legend legend = new MAP_Summary_Marker_Legend();
                            legend.Value = i * baseUnit;
                            legend.ValueFrom = i * baseUnit;
                            legend.ValueTo = (i + 1) * baseUnit;
                            legend.Radius = (i * 2) + 5;
                            result.ListMarkerLegend.Add(legend);
                        }
                    }

                    // Tính marker size
                    foreach (var item in result.ListMarker)
                    {
                        item.Radius = 5;
                        if (item.TonTranfer == maxTransfer)
                            item.Radius = result.ListMarkerLegend.OrderByDescending(c => c.Value).FirstOrDefault().Radius;
                        else
                        {
                            var radius = result.ListMarkerLegend.FirstOrDefault(c => item.TonTranfer >= c.ValueFrom && item.TonTranfer < c.ValueTo);
                            if (radius != null)
                                item.Radius = radius.Radius;
                        }

                        // Làm tròn
                        item.TonTranfer = Math.Round(item.TonTranfer, 1);
                        item.CBMTranfer = Math.Round(item.CBMTranfer, 1);
                        item.TonReturn = Math.Round(item.TonReturn, 1);
                        item.CBMReturn = Math.Round(item.CBMReturn, 1);
                        item.TonLoading = Math.Round(item.TonLoading, 1);
                        item.CBMLoading = Math.Round(item.CBMLoading, 1);
                        item.TonUnLoading = Math.Round(item.TonUnLoading, 1);
                        item.CBMUnLoading = Math.Round(item.CBMUnLoading, 1);
                        item.TimeLoading = Math.Round(item.TimeLoading, 1);
                        item.TimeUnLoading = Math.Round(item.TimeUnLoading, 1);
                    }

                    // Làm tròn
                    result.TonMax = Math.Round(result.TonMax, 1);
                    result.CBMMax = Math.Round(result.CBMMax, 1);
                    result.TonTranfer = Math.Round(result.TonTranfer, 1);
                    result.CBMTranfer = Math.Round(result.CBMTranfer, 1);
                    result.TonReturn = Math.Round(result.TonReturn, 1);
                    result.CBMReturn = Math.Round(result.CBMReturn, 1);
                    result.TonLoading = Math.Round(result.TonLoading, 1);
                    result.CBMLoading = Math.Round(result.CBMLoading, 1);
                    result.TonUnLoading = Math.Round(result.TonUnLoading, 1);
                    result.CBMUnLoading = Math.Round(result.CBMUnLoading, 1);
                    result.TimeLoading = Math.Round(result.TimeLoading, 1);
                    result.TimeUnLoading = Math.Round(result.TimeUnLoading, 1);


                    var ListMaster = result.ListData.Select(c => new
                        {
                            ID = c.DITOMasterID,
                            Code = c.DITOMasterCode,
                            VehicleCode = c.VehicleCode,
                            ATD = c.ETD,
                            ATA = c.ETA,
                        }).Distinct().ToList();

                    result.ListMaster = ListMaster.Select(c => new MAP_Summary_Master
                        {
                            ID = c.ID.Value,
                            Code = c.Code,
                            VehicleCode = c.VehicleCode,
                            ATD = c.ATD,
                            ATA = c.ATA
                        }).ToList();
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

        public MAP_Summary_Master MAP_Summary_Master_Data(int masterID)
        {
            try
            {
                MAP_Summary_Master result = new MAP_Summary_Master();
                result.ListMarker = new List<MAP_Summary_Marker>();
                using (var model = new DataEntities())
                {
                    var queryLTL = model.OPS_DITOGroupProduct.Where(c => c.DITOMasterID == masterID).Select(c => new MAP_Summary_Data
                    {
                        ID = c.ID,
                        DITOMasterID = c.DITOMasterID.HasValue ? c.DITOMasterID.Value : -1,
                        DITOMasterCode = c.DITOMasterID.HasValue ? c.OPS_DITOMaster.Code : string.Empty,
                        VehicleID = c.OPS_DITOMaster.VehicleID.Value,
                        VehicleCode = c.OPS_DITOMaster.CAT_Vehicle.RegNo,
                        KM = c.DITOMasterID.HasValue && c.OPS_DITOMaster.KM.HasValue ? c.OPS_DITOMaster.KM.Value : 0,
                        ETD = c.DITOMasterID.HasValue ? c.OPS_DITOMaster.ATD.HasValue ? c.OPS_DITOMaster.ATD : c.OPS_DITOMaster.ETD : null,
                        ETA = c.DITOMasterID.HasValue ? c.OPS_DITOMaster.ATA.HasValue ? c.OPS_DITOMaster.ATA : c.OPS_DITOMaster.ETA : null,
                        TonTranfer = c.TonTranfer,
                        CBMTranfer = c.CBMTranfer,
                        QuantityTranfer = c.QuantityTranfer,

                        DistributorID = c.ORD_GroupProduct.CUS_Location1.LocationID,
                        DistributorCode = c.ORD_GroupProduct.CUS_Location1.CAT_Location.Code,
                        DistributorName = c.ORD_GroupProduct.CUS_Location1.CAT_Location.Location,
                        DistributorProvinceID = c.ORD_GroupProduct.CUS_Location1.CAT_Location.ProvinceID,
                        DistributorDistrictID = c.ORD_GroupProduct.CUS_Location1.CAT_Location.DistrictID,
                        DistributorAddress = c.ORD_GroupProduct.CUS_Location1.CAT_Location.Address,
                        DistributorLat = c.ORD_GroupProduct.CUS_Location1.CAT_Location.Lat > 0 ? c.ORD_GroupProduct.CUS_Location1.CAT_Location.Lat : null,
                        DistributorLng = c.ORD_GroupProduct.CUS_Location1.CAT_Location.Lng > 0 ? c.ORD_GroupProduct.CUS_Location1.CAT_Location.Lng : null,

                        StockID = c.ORD_GroupProduct.CUS_Location.LocationID,
                        StockCode = c.ORD_GroupProduct.CUS_Location.CAT_Location.Code,
                        StockName = c.ORD_GroupProduct.CUS_Location.CAT_Location.Location,
                        StockProvinceID = c.ORD_GroupProduct.CUS_Location.CAT_Location.ProvinceID,
                        StockDistrictID = c.ORD_GroupProduct.CUS_Location.CAT_Location.DistrictID,
                        StockAddress = c.ORD_GroupProduct.CUS_Location.CAT_Location.Address,
                        StockLat = c.ORD_GroupProduct.CUS_Location.CAT_Location.Lat > 0 ? c.ORD_GroupProduct.CUS_Location.CAT_Location.Lat : null,
                        StockLng = c.ORD_GroupProduct.CUS_Location.CAT_Location.Lng > 0 ? c.ORD_GroupProduct.CUS_Location.CAT_Location.Lng : null,
                    }).ToList();

                    result.ListMarker = model.OPS_DITOLocation.Where(c => c.DITOMasterID == masterID && c.LocationID > 0 && c.CAT_Location.Lat > 0 && c.CAT_Location.Lng > 0).OrderBy(c => c.SortOrder).Select(c => new MAP_Summary_Marker
                        {
                            ID = c.LocationID.Value,
                            Code = c.CAT_Location.Code,
                            Name = c.CAT_Location.Location,
                            Address = c.CAT_Location.Address,
                            Lat = c.CAT_Location.Lat,
                            Lng = c.CAT_Location.Lng,
                            TypeOfLocation = c.TypeOfTOLocationID,
                            SortOrder = c.SortOrder,
                        }).ToList();

                    foreach (var item in result.ListMarker)
                    {
                        if (item.TypeOfLocation == -(int)SYSVarType.TypeOfTOLocationGet || item.TypeOfLocation == -(int)SYSVarType.TypeOfTOLocationGetDelivery)
                        {
                            var queryLocation = queryLTL.Where(c => c.StockID == item.ID || c.DistributorID == item.ID);
                            if (queryLocation.Count() > 0)
                            {
                                item.TonTranfer = queryLocation.Sum(c => c.TonTranfer);
                                item.CBMTranfer = queryLocation.Sum(c => c.CBMTranfer);
                                if (item.TypeOfLocation == -(int)SYSVarType.TypeOfTOLocationGet)
                                    item.TypeOfLocation = 1;
                                else
                                    item.TypeOfLocation = 2;
                            }
                        }
                    }

                    if (queryLTL.Count > 0)
                    {
                        result.ID = queryLTL.FirstOrDefault().DITOMasterID.Value;
                        result.Code = queryLTL.FirstOrDefault().DITOMasterCode;
                        result.VehicleCode = queryLTL.FirstOrDefault().VehicleCode;
                        result.KM = queryLTL.FirstOrDefault().KM;
                        result.Ton = queryLTL.Sum(c => c.TonTranfer);
                        result.CBM = queryLTL.Sum(c => c.CBMTranfer);
                        result.ATD = queryLTL.FirstOrDefault().ETD;
                        result.ATA = queryLTL.FirstOrDefault().ETA;
                        result.TotalGet = queryLTL.Select(c => c.StockID).Distinct().Count();
                        result.TotalDelivery = queryLTL.Select(c => c.DistributorID).Distinct().Count();
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

        public MAP_Summary_Master MAP_Summary_Master_DataList(List<int> lstMasterID)
        {
            try
            {
                MAP_Summary_Master result = new MAP_Summary_Master();
                using (var model = new DataEntities())
                {
                    var queryLTL = model.OPS_DITOGroupProduct.Where(c => c.DITOMasterID > 0 && lstMasterID.Contains(c.DITOMasterID.Value)).Select(c => new MAP_Summary_Data
                    {
                        ID = c.ID,
                        DITOMasterID = c.DITOMasterID.HasValue ? c.DITOMasterID.Value : -1,
                        DITOMasterCode = c.DITOMasterID.HasValue ? c.OPS_DITOMaster.Code : string.Empty,
                        VehicleID = c.OPS_DITOMaster.VehicleID.Value,
                        VehicleCode = c.OPS_DITOMaster.CAT_Vehicle.RegNo,
                        KM = c.DITOMasterID.HasValue && c.OPS_DITOMaster.KM.HasValue ? c.OPS_DITOMaster.KM.Value : 0,
                        ETD = c.DITOMasterID.HasValue ? c.OPS_DITOMaster.ATD.HasValue ? c.OPS_DITOMaster.ATD : c.OPS_DITOMaster.ETD : null,
                        ETA = c.DITOMasterID.HasValue ? c.OPS_DITOMaster.ATA.HasValue ? c.OPS_DITOMaster.ATA : c.OPS_DITOMaster.ETA : null,
                        TonTranfer = c.TonTranfer,
                        CBMTranfer = c.CBMTranfer,
                        QuantityTranfer = c.QuantityTranfer,

                        DistributorID = c.ORD_GroupProduct.CUS_Location1.LocationID,
                        StockID = c.ORD_GroupProduct.CUS_Location.LocationID,
                    }).ToList();

                    if (queryLTL.Count > 0)
                    {
                        result.ID = queryLTL.FirstOrDefault().DITOMasterID.Value;
                        result.Code = string.Empty;
                        result.VehicleCode = string.Empty;
                        result.KM = queryLTL.Sum(c => c.KM);
                        result.Ton = queryLTL.Sum(c => c.TonTranfer);
                        result.CBM = queryLTL.Sum(c => c.CBMTranfer);
                        result.ATD = queryLTL.OrderBy(c => c.ETD).FirstOrDefault().ETD;
                        result.ATA = queryLTL.OrderByDescending(c => c.ETA).FirstOrDefault().ETA;
                        result.TotalGet = queryLTL.Select(c => c.StockID).Distinct().Count();
                        result.TotalDelivery = queryLTL.Select(c => c.DistributorID).Distinct().Count();
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

        public MAP_SummaryCO MAP_SummaryCO_Data(string request, List<int> lstCustomerID, DateTime dtfrom, DateTime dtto, int provinceID, int typeoflocationID)
        {
            try
            {
                int iComplete = -(int)SYSVarType.TypeOfStatusContainerComplete;
                int iCancel = -(int)SYSVarType.TypeOfStatusContainerCancel;
                int iEXLaden = -(int)SYSVarType.StatusOfCOContainerEXLaden;
                int iIMLaden = -(int)SYSVarType.StatusOfCOContainerIMLaden;
                int iLOLaden = -(int)SYSVarType.StatusOfCOContainerLOLaden;

                MAP_SummaryCO result = new MAP_SummaryCO();
                result.ListData = new List<MAP_SummaryCO_Data>();
                result.ListMarker = new List<MAP_SummaryCO_Marker>();
                result.ListMarkerLegend = new List<MAP_Summary_Marker_Legend>();
                result.ListMaster = new List<MAP_SummaryCO_Master>();
                using (var model = new DataEntities())
                {
                    dtfrom = dtfrom.Date;
                    dtto = dtto.AddDays(1).Date;

                    if (lstCustomerID == null || lstCustomerID.Count == 0)
                    {
                        string ViewAdmin = SYSViewCode.ViewAdmin.ToString();
                        lstCustomerID = model.CUS_Customer.Where(c => (c.TypeOfCustomerID == -(int)SYSVarType.TypeOfCustomerCUS || c.TypeOfCustomerID == -(int)SYSVarType.TypeOfCustomerBOTH) && !c.IsSystem && (Account.ListActionCode.Contains(ViewAdmin) || Account.ListCustomerID.Contains(c.ID))).Select(c => c.ID).ToList();
                    }

                    var queryLTL = model.OPS_COTOContainer.Where(c => c.COTOMasterID > 0 && (c.OPS_COTOMaster.StatusOfCOTOMasterID >= -(int)SYSVarType.StatusOfCOTOMasterReceived) && lstCustomerID.Contains(c.OPS_Container.ORD_Container.ORD_Order.CustomerID) && (provinceID > 0 ? c.OPS_Container.ORD_Container.LocationToID.HasValue && c.OPS_Container.ORD_Container.CUS_Location3.CAT_Location.ProvinceID == provinceID : true) &&
                    c.OPS_COTOMaster.SYSCustomerID == Account.SYSCustomerID && c.OPS_Container.ORD_Container.DateConfig >= dtfrom && c.OPS_Container.ORD_Container.DateConfig < dtto && (c.TypeOfStatusContainerID == iComplete || c.TypeOfStatusContainerID == iCancel) && (c.StatusOfCOContainerID == iEXLaden || c.StatusOfCOContainerID == iIMLaden || c.StatusOfCOContainerID == iLOLaden) && c.OPS_COTOMaster.VehicleID.HasValue).Select(c => new MAP_SummaryCO_Data
                    {
                        ID = c.ID,
                        COTOMasterID = c.COTOMasterID.HasValue ? c.COTOMasterID.Value : -1,
                        COTOMasterCode = c.COTOMasterID.HasValue ? c.OPS_COTOMaster.Code : string.Empty,
                        VehicleID = c.OPS_COTOMaster.VehicleID.Value,
                        VehicleCode = c.OPS_COTOMaster.CAT_Vehicle.RegNo,
                        RomoocID = c.OPS_COTOMaster.RomoocID.HasValue ? c.OPS_COTOMaster.RomoocID.Value : -1,
                        RomoocCode = c.OPS_COTOMaster.RomoocID.HasValue ? c.OPS_COTOMaster.CAT_Romooc.RegNo : string.Empty,
                        KM = c.COTOMasterID.HasValue && c.OPS_COTOMaster.KM.HasValue ? c.OPS_COTOMaster.KM.Value : 0,
                        CustomerID = c.OPS_Container.ORD_Container.ORD_Order.CustomerID,
                        CustomerCode = c.OPS_Container.ORD_Container.ORD_Order.CUS_Customer.Code,
                        OrderID = c.OPS_Container.ORD_Container.OrderID,
                        OrderCode = c.OPS_Container.ORD_Container.ORD_Order.Code,
                        OrderContainerID = c.OPS_Container.ContainerID,
                        RequestDate = c.OPS_Container.ORD_Container.ORD_Order.RequestDate,
                        ETD = c.COTOMasterID.HasValue ? c.OPS_COTOMaster.ATD.HasValue ? c.OPS_COTOMaster.ATD : c.OPS_COTOMaster.ETD : null,
                        ETA = c.COTOMasterID.HasValue ? c.OPS_COTOMaster.ATA.HasValue ? c.OPS_COTOMaster.ATA : c.OPS_COTOMaster.ETA : null,
                        DateConfig = c.OPS_Container.ORD_Container.ORD_Order.DateConfig,

                        LocationFromID = c.LocationFromID,
                        LocationFromCode = c.CAT_Location.Code,
                        LocationFromName = c.CAT_Location.Location,
                        LocationFromProvinceID = c.CAT_Location.ProvinceID,
                        LocationFromDistrictID = c.CAT_Location.DistrictID,
                        LocationFromAddress = c.CAT_Location.Address,
                        LocationFromLat = c.CAT_Location.Lat > 0 ? c.CAT_Location.Lat : null,
                        LocationFromLng = c.CAT_Location.Lng > 0 ? c.CAT_Location.Lng : null,

                        LocationToID = c.LocationToID,
                        LocationToCode = c.CAT_Location1.Code,
                        LocationToName = c.CAT_Location1.Location,
                        LocationToProvinceID = c.CAT_Location1.ProvinceID,
                        LocationToDistrictID = c.CAT_Location1.DistrictID,
                        LocationToAddress = c.CAT_Location1.Address,
                        LocationToLat = c.CAT_Location1.Lat > 0 ? c.CAT_Location1.Lat : null,
                        LocationToLng = c.CAT_Location1.Lng > 0 ? c.CAT_Location1.Lng : null,

                        PackingID = c.OPS_Container.ORD_Container.PackingID,
                        StatusOfCOContainerID = c.StatusOfCOContainerID
                    }).ToDataSourceResult(CreateRequest(request));

                    result.ListData = queryLTL.Data.Cast<MAP_SummaryCO_Data>().ToList();

                    foreach (var item in result.ListData)
                    {
                        if (item.LocationToLat > 0 && item.LocationToLng > 0)
                        {
                            var itemMarker = result.ListMarker.FirstOrDefault(c => c.ID == item.LocationToID);
                            if (typeoflocationID == 2)
                                itemMarker = result.ListMarker.FirstOrDefault(c => c.ID == item.LocationFromID);
                            if (itemMarker == null)
                            {
                                itemMarker = new MAP_SummaryCO_Marker
                                {
                                    ID = item.LocationToID,
                                    Code = item.LocationToCode,
                                    Name = item.LocationToName,
                                    ProvinceID = item.LocationToProvinceID,
                                    DistrictID = item.LocationToDistrictID,
                                    Address = item.LocationToAddress,
                                    Lat = item.LocationToLat,
                                    Lng = item.LocationToLng,
                                    ListMasterID = new List<int>(),
                                };

                                if (typeoflocationID == 2)
                                {
                                    itemMarker = new MAP_SummaryCO_Marker
                                    {
                                        ID = item.LocationFromID,
                                        Code = item.LocationFromCode,
                                        Name = item.LocationFromName,
                                        ProvinceID = item.LocationFromProvinceID,
                                        DistrictID = item.LocationFromDistrictID,
                                        Address = item.LocationFromAddress,
                                        Lat = item.LocationFromLat,
                                        Lng = item.LocationFromLng,
                                        ListMasterID = new List<int>(),
                                    };
                                }

                                result.ListMarker.Add(itemMarker);
                            }

                            itemMarker.c20DC += item.PackingID == (int)CATPackingCOCode.CO20 ? 1 : 0;
                            itemMarker.c40DC += item.PackingID == (int)CATPackingCOCode.CO40 ? 1 : 0;
                            itemMarker.c40HC += item.PackingID == (int)CATPackingCOCode.CO40H ? 1 : 0;
                            itemMarker.TotalContainer += 1;
                            itemMarker.ListMasterID.Add(item.COTOMasterID.Value);
                        }

                        if (item.StatusOfCOContainerID == iEXLaden)
                            item.Export += 1;
                        else if (item.StatusOfCOContainerID == iIMLaden)
                            item.Import += 1;
                        else if (item.StatusOfCOContainerID == iLOLaden)
                            item.Local += 1;

                        item.c20DC += item.PackingID == (int)CATPackingCOCode.CO20 ? 1 : 0;
                        item.c40DC += item.PackingID == (int)CATPackingCOCode.CO40 ? 1 : 0;
                        item.c40HC += item.PackingID == (int)CATPackingCOCode.CO40H ? 1 : 0;

                        // Tổng hợp dữ liệu
                        result.c20DC += item.PackingID == (int)CATPackingCOCode.CO20 ? 1 : 0;
                        result.c40DC += item.PackingID == (int)CATPackingCOCode.CO40 ? 1 : 0;
                        result.c40HC += item.PackingID == (int)CATPackingCOCode.CO40H ? 1 : 0;
                        result.TotalContainer += 1;
                        if (result.ListMaster.Count(c => c.ID == item.COTOMasterID) == 0)
                        {
                            result.ListMaster.Add(new MAP_SummaryCO_Master { ID = item.COTOMasterID.Value });
                            if (item.StatusOfCOContainerID == iEXLaden)
                                result.Export += 1;
                            else if (item.StatusOfCOContainerID == iIMLaden)
                                result.Import += 1;
                            else if (item.StatusOfCOContainerID == iLOLaden)
                                result.Local += 1;
                        }
                    }

                    // Tổng hợp thông tin khác
                    var lstMaster = result.ListData.Select(c => new { c.COTOMasterID, c.KM, c.VehicleID, c.RomoocID }).Distinct().ToList();
                    result.Schedule = lstMaster.Count;
                    result.KM = lstMaster.Sum(c => c.KM);
                    result.KMAverage = result.Schedule > 0 ? result.KM / result.Schedule : 0;
                    var lstVehicle = result.ListData.Select(c => c.VehicleID).Distinct().ToList();
                    result.Vehicle = lstVehicle.Count;

                    // Tính legend
                    double maxTransfer = 0;
                    if (result.ListMarker.Count > 0)
                    {
                        // Ktra điểm chưa có thì đưa vào ListData
                        //maxTransfer = result.ListMarker.OrderByDescending(c => c.TonTranfer).FirstOrDefault().TonTranfer;
                        maxTransfer = 100;
                        int totalLegend = 5;
                        double baseUnit = maxTransfer / totalLegend;

                        for (int i = 0; i < 5; i++)
                        {
                            MAP_Summary_Marker_Legend legend = new MAP_Summary_Marker_Legend();
                            legend.Value = i * baseUnit;
                            legend.ValueFrom = i * baseUnit;
                            legend.ValueTo = (i + 1) * baseUnit;
                            legend.Radius = (i * 2) + 5;
                            result.ListMarkerLegend.Add(legend);
                        }
                    }

                    // Tính marker size
                    foreach (var item in result.ListMarker)
                    {
                        item.Radius = 5;
                        if (item.TotalContainer == maxTransfer)
                            item.Radius = result.ListMarkerLegend.OrderByDescending(c => c.Value).FirstOrDefault().Radius;
                        else
                        {
                            var radius = result.ListMarkerLegend.OrderBy(c => c.ValueTo).FirstOrDefault(c => (item.TotalContainer >= c.ValueFrom && item.TotalContainer < c.ValueTo) || (item.TotalContainer >= c.ValueTo));
                            if (radius != null)
                                item.Radius = radius.Radius;
                        }

                        item.Schedule = item.ListMasterID.Distinct().Count();
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

        public MAP_SummaryCO MAP_SummaryCO_Vehicle_Data(string request, DateTime dtfrom, DateTime dtto, int vehicleID)
        {
            try
            {
                int iComplete = -(int)SYSVarType.TypeOfStatusContainerComplete;
                int iCancel = -(int)SYSVarType.TypeOfStatusContainerCancel;
                int iEXLaden = -(int)SYSVarType.StatusOfCOContainerEXLaden;
                int iIMLaden = -(int)SYSVarType.StatusOfCOContainerIMLaden;
                int iLOLaden = -(int)SYSVarType.StatusOfCOContainerLOLaden;

                MAP_SummaryCO result = new MAP_SummaryCO();
                result.ListData = new List<MAP_SummaryCO_Data>();
                result.ListMarker = new List<MAP_SummaryCO_Marker>();
                result.ListMarkerLegend = new List<MAP_Summary_Marker_Legend>();
                result.ListMaster = new List<MAP_SummaryCO_Master>();
                using (var model = new DataEntities())
                {
                    dtfrom = dtfrom.Date;
                    dtto = dtto.AddDays(1).Date;

                    var queryLTL = model.OPS_COTOContainer.Where(c => c.COTOMasterID > 0 && (c.OPS_COTOMaster.StatusOfCOTOMasterID >= -(int)SYSVarType.StatusOfCOTOMasterReceived) &&
                    c.OPS_COTOMaster.SYSCustomerID == Account.SYSCustomerID && c.OPS_Container.ORD_Container.DateConfig >= dtfrom && c.OPS_Container.ORD_Container.DateConfig < dtto && (c.TypeOfStatusContainerID == iComplete || c.TypeOfStatusContainerID == iCancel) && (c.StatusOfCOContainerID == iEXLaden || c.StatusOfCOContainerID == iIMLaden || c.StatusOfCOContainerID == iLOLaden) && c.OPS_COTOMaster.VehicleID.HasValue && (vehicleID > 0 ? c.OPS_COTOMaster.VehicleID == vehicleID : true)).Select(c => new MAP_SummaryCO_Data
                    {
                        ID = c.ID,
                        COTOMasterID = c.COTOMasterID.HasValue ? c.COTOMasterID.Value : -1,
                        COTOMasterCode = c.COTOMasterID.HasValue ? c.OPS_COTOMaster.Code : string.Empty,
                        VehicleID = c.OPS_COTOMaster.VehicleID.Value,
                        VehicleCode = c.OPS_COTOMaster.CAT_Vehicle.RegNo,
                        RomoocID = c.OPS_COTOMaster.RomoocID.HasValue ? c.OPS_COTOMaster.RomoocID.Value : -1,
                        RomoocCode = c.OPS_COTOMaster.RomoocID.HasValue ? c.OPS_COTOMaster.CAT_Romooc.RegNo : string.Empty,
                        KM = c.COTOMasterID.HasValue && c.OPS_COTOMaster.KM.HasValue ? c.OPS_COTOMaster.KM.Value : 0,
                        CustomerID = c.OPS_Container.ORD_Container.ORD_Order.CustomerID,
                        CustomerCode = c.OPS_Container.ORD_Container.ORD_Order.CUS_Customer.Code,
                        OrderID = c.OPS_Container.ORD_Container.OrderID,
                        OrderCode = c.OPS_Container.ORD_Container.ORD_Order.Code,
                        OrderContainerID = c.OPS_Container.ContainerID,
                        RequestDate = c.OPS_Container.ORD_Container.ORD_Order.RequestDate,
                        ETD = c.COTOMasterID.HasValue ? c.OPS_COTOMaster.ATD.HasValue ? c.OPS_COTOMaster.ATD : c.OPS_COTOMaster.ETD : null,
                        ETA = c.COTOMasterID.HasValue ? c.OPS_COTOMaster.ATA.HasValue ? c.OPS_COTOMaster.ATA : c.OPS_COTOMaster.ETA : null,
                        DateConfig = c.OPS_Container.ORD_Container.ORD_Order.DateConfig,

                        LocationFromID = c.LocationFromID,
                        LocationFromCode = c.CAT_Location.Code,
                        LocationFromName = c.CAT_Location.Location,
                        LocationFromProvinceID = c.CAT_Location.ProvinceID,
                        LocationFromDistrictID = c.CAT_Location.DistrictID,
                        LocationFromAddress = c.CAT_Location.Address,
                        LocationFromLat = c.CAT_Location.Lat > 0 ? c.CAT_Location.Lat : null,
                        LocationFromLng = c.CAT_Location.Lng > 0 ? c.CAT_Location.Lng : null,

                        LocationToID = c.LocationToID,
                        LocationToCode = c.CAT_Location1.Code,
                        LocationToName = c.CAT_Location1.Location,
                        LocationToProvinceID = c.CAT_Location1.ProvinceID,
                        LocationToDistrictID = c.CAT_Location1.DistrictID,
                        LocationToAddress = c.CAT_Location1.Address,
                        LocationToLat = c.CAT_Location1.Lat > 0 ? c.CAT_Location1.Lat : null,
                        LocationToLng = c.CAT_Location1.Lng > 0 ? c.CAT_Location1.Lng : null,

                        PackingID = c.OPS_Container.ORD_Container.PackingID,
                        StatusOfCOContainerID = c.StatusOfCOContainerID
                    }).ToDataSourceResult(CreateRequest(request));

                    result.ListData = queryLTL.Data.Cast<MAP_SummaryCO_Data>().ToList();

                    foreach (var item in result.ListData)
                    {
                        if (item.LocationToLat > 0 && item.LocationToLng > 0)
                        {
                            var itemMarker = result.ListMarker.FirstOrDefault(c => c.ID == item.LocationToID);
                            if (itemMarker == null)
                            {
                                itemMarker = new MAP_SummaryCO_Marker
                                {
                                    ID = item.LocationToID,
                                    Code = item.LocationToCode,
                                    Name = item.LocationToName,
                                    ProvinceID = item.LocationToProvinceID,
                                    DistrictID = item.LocationToDistrictID,
                                    Address = item.LocationToAddress,
                                    Lat = item.LocationToLat,
                                    Lng = item.LocationToLng,
                                    ListMasterID = new List<int>(),
                                };

                                result.ListMarker.Add(itemMarker);
                            }

                            itemMarker.c20DC += item.PackingID == (int)CATPackingCOCode.CO20 ? 1 : 0;
                            itemMarker.c40DC += item.PackingID == (int)CATPackingCOCode.CO40 ? 1 : 0;
                            itemMarker.c40HC += item.PackingID == (int)CATPackingCOCode.CO40H ? 1 : 0;
                            itemMarker.TotalContainer += 1;
                            itemMarker.ListMasterID.Add(item.COTOMasterID.Value);
                        }

                        if (item.StatusOfCOContainerID == iEXLaden)
                            item.Export += 1;
                        else if (item.StatusOfCOContainerID == iIMLaden)
                            item.Import += 1;
                        else if (item.StatusOfCOContainerID == iLOLaden)
                            item.Local += 1;

                        item.c20DC += item.PackingID == (int)CATPackingCOCode.CO20 ? 1 : 0;
                        item.c40DC += item.PackingID == (int)CATPackingCOCode.CO40 ? 1 : 0;
                        item.c40HC += item.PackingID == (int)CATPackingCOCode.CO40H ? 1 : 0;

                        // Tổng hợp dữ liệu
                        result.c20DC += item.PackingID == (int)CATPackingCOCode.CO20 ? 1 : 0;
                        result.c40DC += item.PackingID == (int)CATPackingCOCode.CO40 ? 1 : 0;
                        result.c40HC += item.PackingID == (int)CATPackingCOCode.CO40H ? 1 : 0;
                        result.TotalContainer += 1;
                        if (result.ListMaster.Count(c => c.ID == item.COTOMasterID) == 0)
                        {
                            result.ListMaster.Add(new MAP_SummaryCO_Master { ID = item.COTOMasterID.Value });
                            if (item.StatusOfCOContainerID == iEXLaden)
                                result.Export += 1;
                            else if (item.StatusOfCOContainerID == iIMLaden)
                                result.Import += 1;
                            else if (item.StatusOfCOContainerID == iLOLaden)
                                result.Local += 1;
                        }
                    }

                    // Tổng hợp thông tin khác
                    var lstMaster = result.ListData.Select(c => new { c.COTOMasterID, c.KM, c.VehicleID, c.RomoocID }).Distinct().ToList();
                    result.Schedule = lstMaster.Count;
                    result.KM = lstMaster.Sum(c => c.KM);
                    result.KMAverage = result.Schedule > 0 ? result.KM / result.Schedule : 0;
                    var lstVehicle = result.ListData.Select(c => c.VehicleID).Distinct().ToList();
                    result.Vehicle = lstVehicle.Count;

                    // Tính legend
                    double maxTransfer = 0;
                    if (result.ListMarker.Count > 0)
                    {
                        // Ktra điểm chưa có thì đưa vào ListData
                        //maxTransfer = result.ListMarker.OrderByDescending(c => c.TonTranfer).FirstOrDefault().TonTranfer;
                        maxTransfer = 100;
                        int totalLegend = 5;
                        double baseUnit = maxTransfer / totalLegend;

                        for (int i = 0; i < 5; i++)
                        {
                            MAP_Summary_Marker_Legend legend = new MAP_Summary_Marker_Legend();
                            legend.Value = i * baseUnit;
                            legend.ValueFrom = i * baseUnit;
                            legend.ValueTo = (i + 1) * baseUnit;
                            legend.Radius = (i * 2) + 5;
                            result.ListMarkerLegend.Add(legend);
                        }
                    }

                    // Tính marker size
                    foreach (var item in result.ListMarker)
                    {
                        item.Radius = 5;
                        if (item.TotalContainer == maxTransfer)
                            item.Radius = result.ListMarkerLegend.OrderByDescending(c => c.Value).FirstOrDefault().Radius;
                        else
                        {
                            var radius = result.ListMarkerLegend.OrderBy(c => c.ValueTo).FirstOrDefault(c => (item.TotalContainer >= c.ValueFrom && item.TotalContainer < c.ValueTo) || (item.TotalContainer >= c.ValueTo));
                            if (radius != null)
                                item.Radius = radius.Radius;
                        }

                        item.Schedule = item.ListMasterID.Distinct().Count();
                    }

                    var ListMaster = result.ListData.Select(c => new
                    {
                        ID = c.COTOMasterID,
                        Code = c.COTOMasterCode,
                        VehicleCode = c.VehicleCode,
                        RomoocID = c.RomoocID,
                        RomoocCode = c.RomoocCode,
                        ATD = c.ETD,
                        ATA = c.ETA,
                    }).Distinct().ToList();

                    result.ListMaster = new List<MAP_SummaryCO_Master>();

                    result.ListMaster = ListMaster.Select(c => new MAP_SummaryCO_Master
                    {
                        ID = c.ID.Value,
                        Code = c.Code,
                        VehicleCode = c.VehicleCode,
                        RomoocCode = c.RomoocCode,
                        ATD = c.ATD,
                        ATA = c.ATA
                    }).ToList();
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

        public MAP_SummaryCO_Master MAP_SummaryCO_Master_Data(int masterID)
        {
            try
            {
                MAP_SummaryCO_Master result = new MAP_SummaryCO_Master();
                result.ListMarker = new List<MAP_SummaryCO_Marker>();
                using (var model = new DataEntities())
                {
                    var queryLTL = model.OPS_COTOContainer.Where(c => c.COTOMasterID == masterID).Select(c => new MAP_SummaryCO_Data
                    {
                        ID = c.ID,
                        COTOMasterID = c.COTOMasterID.HasValue ? c.COTOMasterID.Value : -1,
                        COTOMasterCode = c.COTOMasterID.HasValue ? c.OPS_COTOMaster.Code : string.Empty,
                        VehicleID = c.OPS_COTOMaster.VehicleID.Value,
                        VehicleCode = c.OPS_COTOMaster.CAT_Vehicle.RegNo,
                        RomoocID = c.OPS_COTOMaster.RomoocID.HasValue ? c.OPS_COTOMaster.RomoocID.Value : -1,
                        RomoocCode = c.OPS_COTOMaster.RomoocID.HasValue ? c.OPS_COTOMaster.CAT_Romooc.RegNo : string.Empty,
                        KM = c.COTOMasterID.HasValue && c.OPS_COTOMaster.KM.HasValue ? c.OPS_COTOMaster.KM.Value : 0,
                        CustomerID = c.OPS_Container.ORD_Container.ORD_Order.CustomerID,
                        CustomerCode = c.OPS_Container.ORD_Container.ORD_Order.CUS_Customer.Code,
                        OrderID = c.OPS_Container.ORD_Container.OrderID,
                        OrderCode = c.OPS_Container.ORD_Container.ORD_Order.Code,
                        OrderContainerID = c.OPS_Container.ContainerID,
                        RequestDate = c.OPS_Container.ORD_Container.ORD_Order.RequestDate,
                        ETD = c.COTOMasterID.HasValue ? c.OPS_COTOMaster.ATD.HasValue ? c.OPS_COTOMaster.ATD : c.OPS_COTOMaster.ETD : null,
                        ETA = c.COTOMasterID.HasValue ? c.OPS_COTOMaster.ATA.HasValue ? c.OPS_COTOMaster.ATA : c.OPS_COTOMaster.ETA : null,
                        DateConfig = c.OPS_Container.ORD_Container.ORD_Order.DateConfig,

                        LocationFromID = c.LocationFromID,
                        LocationFromCode = c.CAT_Location.Code,
                        LocationFromName = c.CAT_Location.Location,
                        LocationFromProvinceID = c.CAT_Location.ProvinceID,
                        LocationFromDistrictID = c.CAT_Location.DistrictID,
                        LocationFromAddress = c.CAT_Location.Address,
                        LocationFromLat = c.CAT_Location.Lat > 0 ? c.CAT_Location.Lat : null,
                        LocationFromLng = c.CAT_Location.Lng > 0 ? c.CAT_Location.Lng : null,

                        LocationToID = c.LocationToID,
                        LocationToCode = c.CAT_Location1.Code,
                        LocationToName = c.CAT_Location1.Location,
                        LocationToProvinceID = c.CAT_Location1.ProvinceID,
                        LocationToDistrictID = c.CAT_Location1.DistrictID,
                        LocationToAddress = c.CAT_Location1.Address,
                        LocationToLat = c.CAT_Location1.Lat > 0 ? c.CAT_Location1.Lat : null,
                        LocationToLng = c.CAT_Location1.Lng > 0 ? c.CAT_Location1.Lng : null,

                        PackingID = c.OPS_Container.ORD_Container.PackingID,
                        StatusOfCOContainerID = c.StatusOfCOContainerID,

                        c20DC = c.OPS_Container.ORD_Container.PackingID == (int)CATPackingCOCode.CO20 ? 1 : 0,
                        c40DC = c.OPS_Container.ORD_Container.PackingID == (int)CATPackingCOCode.CO40 ? 1 : 0,
                        c40HC = c.OPS_Container.ORD_Container.PackingID == (int)CATPackingCOCode.CO40H ? 1 : 0,
                    }).ToList();

                    result.ListMarker = model.OPS_COTOLocation.Where(c => c.COTOMasterID == masterID && c.LocationID > 0 && c.CAT_Location.Lat > 0 && c.CAT_Location.Lng > 0).OrderBy(c => c.SortOrder).Select(c => new MAP_SummaryCO_Marker
                    {
                        ID = c.LocationID.Value,
                        Code = c.CAT_Location.Code,
                        Name = c.CAT_Location.Location,
                        Address = c.CAT_Location.Address,
                        Lat = c.CAT_Location.Lat,
                        Lng = c.CAT_Location.Lng,
                        TypeOfLocation = c.TypeOfTOLocationID,
                        SortOrder = c.SortOrder,
                    }).ToList();

                    foreach (var item in result.ListMarker)
                    {
                        if (item.TypeOfLocation == -(int)SYSVarType.TypeOfTOLocationGet || item.TypeOfLocation == -(int)SYSVarType.TypeOfTOLocationGetDelivery)
                        {
                            var queryLocation = queryLTL.Where(c => c.LocationFromID == item.ID || c.LocationToID == item.ID);
                            if (queryLocation.Count() > 0)
                            {
                                item.c20DC = queryLocation.Sum(c => c.c20DC);
                                item.c40DC = queryLocation.Sum(c => c.c40DC);
                                item.c40HC = queryLocation.Sum(c => c.c40HC);

                                if (item.TypeOfLocation == -(int)SYSVarType.TypeOfTOLocationGet)
                                    item.TypeOfLocation = 1;
                                else
                                    item.TypeOfLocation = 2;
                            }
                        }
                    }

                    if (queryLTL.Count > 0)
                    {
                        result.ID = queryLTL.FirstOrDefault().COTOMasterID.Value;
                        result.Code = queryLTL.FirstOrDefault().COTOMasterCode;
                        result.VehicleCode = queryLTL.FirstOrDefault().VehicleCode;
                        result.RomoocCode = queryLTL.FirstOrDefault().RomoocCode;
                        result.KM = queryLTL.FirstOrDefault().KM;
                        result.c20DC = queryLTL.Sum(c => c.c20DC);
                        result.c40DC = queryLTL.Sum(c => c.c40DC);
                        result.c40HC = queryLTL.Sum(c => c.c40HC);
                        result.ATD = queryLTL.FirstOrDefault().ETD;
                        result.ATA = queryLTL.FirstOrDefault().ETA;
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

        public MAP_SummaryCO_Master MAP_SummaryCO_Master_DataList(List<int> lstMasterID)
        {
            try
            {
                MAP_SummaryCO_Master result = new MAP_SummaryCO_Master();
                using (var model = new DataEntities())
                {
                    var queryLTL = model.OPS_COTOContainer.Where(c => c.COTOMasterID > 0 && lstMasterID.Contains(c.COTOMasterID.Value)).Select(c => new MAP_SummaryCO_Data
                    {
                        ID = c.ID,
                        COTOMasterID = c.COTOMasterID.HasValue ? c.COTOMasterID.Value : -1,
                        COTOMasterCode = c.COTOMasterID.HasValue ? c.OPS_COTOMaster.Code : string.Empty,
                        VehicleID = c.OPS_COTOMaster.VehicleID.Value,
                        VehicleCode = c.OPS_COTOMaster.CAT_Vehicle.RegNo,
                        KM = c.COTOMasterID.HasValue && c.OPS_COTOMaster.KM.HasValue ? c.OPS_COTOMaster.KM.Value : 0,
                        ETD = c.COTOMasterID.HasValue ? c.OPS_COTOMaster.ATD.HasValue ? c.OPS_COTOMaster.ATD : c.OPS_COTOMaster.ETD : null,
                        ETA = c.COTOMasterID.HasValue ? c.OPS_COTOMaster.ATA.HasValue ? c.OPS_COTOMaster.ATA : c.OPS_COTOMaster.ETA : null,

                        LocationFromID = c.LocationFromID,
                        LocationToID = c.LocationToID,
                    }).ToList();

                    if (queryLTL.Count > 0)
                    {
                        result.ID = queryLTL.FirstOrDefault().COTOMasterID.Value;
                        result.Code = string.Empty;
                        result.VehicleCode = string.Empty;
                        result.KM = queryLTL.Sum(c => c.KM);
                        result.ATD = queryLTL.OrderBy(c => c.ETD).FirstOrDefault().ETD;
                        result.ATA = queryLTL.OrderByDescending(c => c.ETA).FirstOrDefault().ETA;
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

        #endregion

        #region Search
        public DTOResult SYSSearchDI(int type, string content, string request)
        {
            try
            {
                var result = new DTOResult();
                if (!string.IsNullOrEmpty(content) && content.Length < 3)
                    throw FaultHelper.BusinessFault(null, null, "Nội dung tìm kiếm phải ít nhất 3 kí tự");

                if (!string.IsNullOrEmpty(content))
                {
                    //add content to request
                    var kendoRequest = CreateRequest(request);
                    Kendo.Mvc.FilterDescriptor filter = new Kendo.Mvc.FilterDescriptor();
                    filter.Operator = Kendo.Mvc.FilterOperator.Contains;
                    filter.Value = content;

                    switch (type)
                    {
                        case 1:
                            filter.Member = "OrderCode";
                            kendoRequest.Filters.Add(filter);
                            break;
                        case 5:
                            filter.Member = "LocationToAddress";
                            kendoRequest.Filters.Add(filter);
                            break;
                        case 2:
                            filter.Member = "MasterCode";
                            kendoRequest.Filters.Add(filter);
                            break;
                        case 3:
                            filter.Member = "VehicleCode";
                            kendoRequest.Filters.Add(filter);
                            break;
                        case 4:
                            filter.Member = "DriverName1";
                            kendoRequest.Filters.Add(filter);
                            break;
                        case 6:
                            filter.Member = "InvoiceNo";
                            kendoRequest.Filters.Add(filter);
                            break;
                    }

                    result = SYSSearch_Read(kendoRequest, false);

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

        public DTOResult SYSSearchCO(int type, string content, string request)
        {
            try
            {
                var result = new DTOResult();
                if (!string.IsNullOrEmpty(content) && content.Length < 3)
                    throw FaultHelper.BusinessFault(null, null, "Nội dung tìm kiếm phải ít nhất 3 kí tự");

                if (!string.IsNullOrEmpty(content))
                {
                    //add content to request
                    var kendoRequest = CreateRequest(request);
                    Kendo.Mvc.FilterDescriptor filter = new Kendo.Mvc.FilterDescriptor();
                    filter.Operator = Kendo.Mvc.FilterOperator.Contains;
                    filter.Value = content;

                    kendoRequest.Filters.Add(filter);

                    switch (type)
                    {
                        case 1:
                            filter.Member = "OrderCode";
                            kendoRequest.Filters.Add(filter);
                            break;
                        case 5:
                            filter.Member = "LocationToAddress";
                            kendoRequest.Filters.Add(filter);
                            break;
                        case 2:
                            filter.Member = "MasterCode";
                            kendoRequest.Filters.Add(filter);
                            break;
                        case 3:
                            filter.Member = "VehicleCode";
                            kendoRequest.Filters.Add(filter);
                            break;
                        case 4:
                            filter.Member = "DriverName1";
                            kendoRequest.Filters.Add(filter);
                            break;
                        case 6:
                            filter.Member = "InvoiceNo";
                            kendoRequest.Filters.Add(filter);
                            break;
                    }

                    result = SYSSearch_Read(kendoRequest, true);
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

        private DTOResult SYSSearch_Read(Kendo.Mvc.UI.DataSourceRequest request, bool isCO)
        {
            try
            {
                var result = new DTOResult();
                using (var model = new DataEntities())
                {

                    if (!isCO)
                    {
                        #region DI
                        var query = model.OPS_DITOGroupProduct.Where(c => c.OrderGroupProductID > 0).Select(c => new SYSSearchDetail
                        {
                            ID = c.ID,
                            CustomerID = c.ORD_GroupProduct.ORD_Order.CustomerID,
                            CustomerCode = c.ORD_GroupProduct.ORD_Order.CUS_Customer.Code,
                            CustomerName = c.ORD_GroupProduct.ORD_Order.CUS_Customer.CustomerName,
                            OrderID = c.ORD_GroupProduct.OrderID,
                            OrderCode = c.ORD_GroupProduct.ORD_Order.Code,
                            DNCode = c.DNCode == null ? string.Empty : c.DNCode,
                            SOCode = c.ORD_GroupProduct.SOCode == null ? string.Empty : c.ORD_GroupProduct.SOCode,
                            ContractNo = c.ORD_GroupProduct.ORD_Order.ContractID > 0 ? c.ORD_GroupProduct.ORD_Order.CAT_Contract.ContractNo : "",
                            CreatedBy = c.ORD_GroupProduct.ORD_Order.CreatedBy,
                            CreatedDate = c.ORD_GroupProduct.ORD_Order.CreatedDate,
                            RequestDate = c.ORD_GroupProduct.ORD_Order.RequestDate,
                            ServiceOfOrderName = c.ORD_GroupProduct.ORD_Order.ServiceOfOrderID > 0 ? c.ORD_GroupProduct.ORD_Order.CAT_ServiceOfOrder.Name : "",
                            TransportModeName = c.ORD_GroupProduct.ORD_Order.CAT_TransportMode.Name,
                            LocationFromCode = c.ORD_GroupProduct.LocationFromID.HasValue ? c.ORD_GroupProduct.CUS_Location.Code : string.Empty,
                            LocationFromName = c.ORD_GroupProduct.LocationFromID.HasValue ? c.ORD_GroupProduct.CUS_Location.LocationName : string.Empty,
                            LocationFromAddress = c.ORD_GroupProduct.LocationFromID.HasValue ? c.ORD_GroupProduct.CUS_Location.CAT_Location.Address : string.Empty,
                            LocationFromProvince = c.ORD_GroupProduct.LocationFromID.HasValue ? c.ORD_GroupProduct.CUS_Location.CAT_Location.CAT_Province.ProvinceName : string.Empty,
                            LocationFromDistrict = c.ORD_GroupProduct.LocationFromID.HasValue ? c.ORD_GroupProduct.CUS_Location.CAT_Location.CAT_District.DistrictName : string.Empty,
                            LocationToCode = c.ORD_GroupProduct.LocationToID.HasValue ? c.ORD_GroupProduct.CUS_Location1.Code : string.Empty,
                            LocationToName = c.ORD_GroupProduct.LocationToID.HasValue ? c.ORD_GroupProduct.CUS_Location1.LocationName : string.Empty,
                            LocationToAddress = c.ORD_GroupProduct.LocationToID.HasValue ? c.ORD_GroupProduct.CUS_Location1.CAT_Location.Address : string.Empty,
                            LocationToProvince = c.ORD_GroupProduct.LocationToID.HasValue ? c.ORD_GroupProduct.CUS_Location1.CAT_Location.CAT_Province.ProvinceName : string.Empty,
                            LocationToDistrict = c.ORD_GroupProduct.LocationToID.HasValue ? c.ORD_GroupProduct.CUS_Location1.CAT_Location.CAT_District.DistrictName : string.Empty,
                            DateFromCome = c.DateFromCome,
                            DateFromLeave = c.DateFromLeave,
                            DateFromLoadEnd = c.DateFromLoadEnd,
                            DateFromLoadStart = c.DateFromLoadStart,
                            DateToCome = c.DateToCome,
                            DateToLeave = c.DateToLeave,
                            DateToLoadEnd = c.DateToLoadEnd,
                            DateToLoadStart = c.DateToLoadStart,
                            DITOGroupProductStatusID = c.DITOGroupProductStatusID == -(int)SYSVarType.DITOGroupProductStatusWaiting ? 1 : c.DITOGroupProductStatusID == -(int)SYSVarType.DITOGroupProductStatusComplete ? 2 : 3,
                            DITOGroupProductStatusName = c.SYS_Var.ValueOfVar,
                            DITOGroupProductStatusPODID = c.DITOGroupProductStatusPODID,
                            DITOGroupProductStatusPODName = c.SYS_Var1.ValueOfVar,
                            Note = c.Note,
                            Note1 = c.Note1,
                            Note2 = c.Note2,
                            DriverName1 = c.DITOMasterID.HasValue ? c.OPS_DITOMaster.DriverName1 : "",
                            DriverCard1 = c.DITOMasterID.HasValue ? c.OPS_DITOMaster.DriverCard1 : "",
                            DriverTel1 = c.DITOMasterID.HasValue ? c.OPS_DITOMaster.DriverTel1 : "",
                            DriverName2 = c.DITOMasterID.HasValue ? c.OPS_DITOMaster.DriverName2 : "",
                            DriverCard2 = c.DITOMasterID.HasValue ? c.OPS_DITOMaster.DriverCard2 : "",
                            DriverTel2 = c.DITOMasterID.HasValue ? c.OPS_DITOMaster.DriverTel2 : "",
                            MasterID = c.DITOMasterID,
                            MasterCode = c.DITOMasterID.HasValue ? c.OPS_DITOMaster.Code : string.Empty,
                            MasterETD = c.DITOMasterID.HasValue ? c.OPS_DITOMaster.ETD : null,
                            MasterStatus = c.DITOMasterID.HasValue ? c.OPS_DITOMaster.SYS_Var.ValueOfVar : "",
                            VehicleCode = c.DITOMasterID.HasValue && c.OPS_DITOMaster.VehicleID > 0 ? c.OPS_DITOMaster.CAT_Vehicle.RegNo : "",
                            TonTranfer = c.TonTranfer,
                            CBMTranfer = c.CBMTranfer,
                            QuantityTranfer = c.QuantityTranfer,
                            VendorCode = c.DITOMasterID > 0 && c.OPS_DITOMaster.VendorOfVehicleID > 0 ? c.OPS_DITOMaster.CUS_Customer.Code : "",
                            VendorName = c.DITOMasterID > 0 && c.OPS_DITOMaster.VendorOfVehicleID > 0 ? c.OPS_DITOMaster.CUS_Customer.ShortName : "",
                            InvoiceBy = c.InvoiceBy,
                            InvoiceDate = c.InvoiceDate,
                            InvoiceNo = c.InvoiceNote,
                            OPSDateConfig = c.DateConfig,
                            OrderDateConfig = c.ORD_GroupProduct.DateConfig,
                            VendorLoadCode = c.VendorLoadID > 0 ? c.CUS_Customer.Code : "",
                            VendorLoadName = c.VendorLoadID > 0 ? c.CUS_Customer.ShortName : "",
                            VendorLoadContract = c.VendorLoadContractID > 0 ? c.CAT_Contract.DisplayName : "",
                            VendorUnLoadCode = c.VendorUnLoadID > 0 ? c.CUS_Customer1.Code : "",
                            VendorUnLoadName = c.VendorUnLoadID > 0 ? c.CUS_Customer1.ShortName : "",
                            VendorUnLoadContract = c.VendorUnLoadContractID > 0 ? c.CAT_Contract1.DisplayName : "",
                            GroupOfProductCode = c.ORD_GroupProduct.GroupOfProductID > 0 ? c.ORD_GroupProduct.CUS_GroupOfProduct.Code : "",
                            OrderGroupRouting = c.ORD_GroupProduct.CUSRoutingID > 0 ? c.ORD_GroupProduct.CUS_Routing.CAT_Routing.Code : "",
                            OPSGroupRouting = c.CATRoutingID > 0 ? c.CAT_Routing.Code : "",
                        }).ToDataSourceResult(request);

                        result.Total = query.Total;
                        result.Data = query.Data as IEnumerable<SYSSearchDetail>;
                        #endregion
                    }
                    else
                    {
                        #region CO
                        DateTime dtNOw = DateTime.Now;
                        var query = model.OPS_COTOContainer.Where(c => !c.IsSplit).Select(c => new SYSSearchDetail
                        {
                            ID = c.OPS_Container.ORD_Container.ID,
                            CustomerID = c.OPS_Container.ORD_Container.ORD_Order.CustomerID,
                            CustomerCode = c.OPS_Container.ORD_Container.ORD_Order.CUS_Customer.Code,
                            CustomerName = c.OPS_Container.ORD_Container.ORD_Order.CUS_Customer.CustomerName,
                            OrderID = c.OPS_Container.ORD_Container.OrderID,
                            OrderCode = c.OPS_Container.ORD_Container.ORD_Order.Code,
                            ContractNo = c.OPS_Container.ORD_Container.ORD_Order.ContractID > 0 ? c.OPS_Container.ORD_Container.ORD_Order.CAT_Contract.ContractNo : "",
                            CreatedBy = c.OPS_Container.ORD_Container.ORD_Order.CreatedBy,
                            CreatedDate = c.OPS_Container.ORD_Container.ORD_Order.CreatedDate,
                            RequestDate = c.OPS_Container.ORD_Container.ORD_Order.RequestDate,
                            ServiceOfOrderName = c.OPS_Container.ORD_Container.ORD_Order.CAT_ServiceOfOrder.Name,
                            TransportModeName = c.OPS_Container.ORD_Container.ORD_Order.CAT_TransportMode.Name,
                            LocationFromID = c.LocationFromID,
                            LocationFromCode = c.LocationFromID > 0 ? c.CAT_Location.Code : string.Empty,
                            LocationFromName = c.LocationFromID > 0 ? c.CAT_Location.Location : string.Empty,
                            LocationFromAddress = c.LocationFromID > 0 ? c.CAT_Location.Address : string.Empty,
                            LocationToID = c.LocationToID,
                            LocationToCode = c.LocationToID > 0 ? c.CAT_Location.Code : string.Empty,
                            LocationToName = c.LocationToID > 0 ? c.CAT_Location.Location : string.Empty,
                            LocationToAddress = c.LocationToID > 0 ? c.CAT_Location.Address : string.Empty,
                            LocationDepotID = c.OPS_Container.ORD_Container.LocationDepotID,
                            LocationDepotCode = c.OPS_Container.ORD_Container.LocationDepotID > 0 ? c.OPS_Container.ORD_Container.CUS_Location.Code : string.Empty,
                            LocationDepotName = c.OPS_Container.ORD_Container.LocationDepotID > 0 ? c.OPS_Container.ORD_Container.CUS_Location.LocationName : string.Empty,
                            LocationDepotReturnID = c.OPS_Container.ORD_Container.LocationDepotReturnID,
                            LocationDepotReturnCode = c.OPS_Container.ORD_Container.LocationDepotReturnID > 0 ? c.OPS_Container.ORD_Container.CUS_Location.Code : string.Empty,
                            LocationDepotReturnName = c.OPS_Container.ORD_Container.LocationDepotReturnID > 0 ? c.OPS_Container.ORD_Container.CUS_Location.LocationName : string.Empty,
                            DateFromCome = c.DateFromCome,
                            DateFromLeave = c.DateFromLeave,
                            DateToCome = c.DateToCome,
                            DateToLeave = c.DateToLeave,
                            PackingCode = c.OPS_Container.ORD_Container.CAT_Packing.Code,
                            ContainerNo = c.OPS_Container.ContainerNo,
                            SealNo1 = c.OPS_Container.SealNo1,
                            SealNo2 = c.OPS_Container.SealNo2,
                            Note = c.OPS_Container.Note,
                            CutOffTime = c.OPS_Container.ORD_Container.CutOffTime,
                            LoadingTime = c.OPS_Container.ORD_Container.LoadingTime,
                            DateShipCome = c.OPS_Container.ORD_Container.DateShipCome,
                            DateDocument = c.OPS_Container.ORD_Container.DateDocument,
                            DateGetEmpty = c.OPS_Container.ORD_Container.DateGetEmpty,
                            DateReturnEmpty = c.OPS_Container.ORD_Container.DateReturnEmpty,
                            DateUnloading = c.OPS_Container.ORD_Container.DateUnloading,
                            DriverName1 = c.COTOMasterID.HasValue ? c.OPS_COTOMaster.DriverName1 : "",
                            DriverCard1 = c.COTOMasterID.HasValue ? c.OPS_COTOMaster.DriverCard1 : "",
                            DriverTel1 = c.COTOMasterID.HasValue ? c.OPS_COTOMaster.DriverTel1 : "",
                            DriverName2 = c.COTOMasterID.HasValue ? c.OPS_COTOMaster.DriverName2 : "",
                            DriverCard2 = c.COTOMasterID.HasValue ? c.OPS_COTOMaster.DriverCard2 : "",
                            DriverTel2 = c.COTOMasterID.HasValue ? c.OPS_COTOMaster.DriverTel2 : "",
                            MasterID = c.COTOMasterID,
                            MasterCode = c.COTOMasterID.HasValue ? c.OPS_COTOMaster.Code : string.Empty,
                            MasterETD = c.COTOMasterID.HasValue ? c.OPS_COTOMaster.ETD : dtNOw,
                            MasterStatus = c.COTOMasterID.HasValue ? c.OPS_COTOMaster.SYS_Var.ValueOfVar : "",
                            VehicleCode = c.COTOMasterID.HasValue && c.OPS_COTOMaster.VehicleID > 0 ? c.OPS_COTOMaster.CAT_Vehicle.RegNo : "",
                            RomoocNo = c.COTOMasterID.HasValue && c.OPS_COTOMaster.RomoocID > 0 ? c.OPS_COTOMaster.CAT_Romooc.RegNo : "",
                            VendorCode = c.COTOMasterID > 0 && c.OPS_COTOMaster.VendorOfVehicleID > 0 ? c.OPS_COTOMaster.CUS_Customer.Code : "",
                            VendorName = c.COTOMasterID > 0 && c.OPS_COTOMaster.VendorOfVehicleID > 0 ? c.OPS_COTOMaster.CUS_Customer.ShortName : "",
                            InvoiceBy = c.InvoiceBy,
                            InvoiceDate = c.InvoiceDate,
                            InvoiceNo = c.InvoiceNo,
                            StatusOfCOContainerID = c.StatusOfCOContainerID,
                            StatusOfCOContainerName = c.SYS_Var.ValueOfVar,
                            TypeOfStatusContainerID = c.TypeOfStatusContainerID == -(int)SYSVarType.TypeOfStatusContainerWait ? 1 : (c.TypeOfStatusContainerID == -(int)SYSVarType.TypeOfStatusContainerComplete || c.TypeOfStatusContainerID == -(int)SYSVarType.TypeOfStatusContainerCancel) ? 2 : 3,
                            TypeOfStatusContainerName = c.SYS_Var1.ValueOfVar,
                            TypeOfStatusContainerPODID = c.TypeOfStatusContainerPODID,
                            TypeOfStatusContainerPODName = c.SYS_Var2.ValueOfVar,
                            StatusTOMaster = c.COTOMasterID > 0 ? c.OPS_COTOMaster.StatusOfCOTOMasterID : -1,
                            OrderGroupRouting = c.OPS_Container.ORD_Container.CUSRoutingID > 0 ? c.OPS_Container.ORD_Container.CUS_Routing.Code : "",
                            OPSGroupRouting = c.CATRoutingID > 0 ? c.CAT_Routing.Code : "",
                        }).ToDataSourceResult(request);

                        result.Total = query.Total;
                        result.Data = query.Data as IEnumerable<SYSSearchDetail>;
                        #endregion
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
        #endregion
    }
}

