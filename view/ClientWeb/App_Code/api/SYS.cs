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
using System.IO;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using ServicesExtend;

namespace ClientWeb
{
    public class SYSController : BaseController
    {
        #region App
        [HttpPost]
        public bool App_Connect(dynamic dynParam)
        {
            try
            {
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOAuthorization App_GetAuthorization(dynamic dynParam)
        {
            try
            {
                return GetCache();
            }
            catch
            {
                return default(DTOAuthorization);
            }
        }

        [HttpPost]
        public DTOAuthorization App_Login(dynamic dynParam)
        {
            try
            {
                string username = dynParam.username;
                string password = dynParam.password;
                int devicetype = dynParam.devicetype;

                var result = new DTOAuthorization();
                result.StringError = string.Empty;

                var user = Membership.GetUser(username);
                if (user == null || string.IsNullOrEmpty(user.UserName))
                    result.StringError = "Tài khoản không tồn tại";
                else
                {
                    if (!user.IsApproved)
                    {
                        result.StringError = "Tài khoản chưa xác nhận";
                    }
                    else if (user.IsLockedOut)
                    {
                        result.StringError = "Tài khoản đã bị khóa";
                        //user.UnlockUser();
                        //result.StringError = "Nhập lại mật khẩu";
                    }
                    else
                    {
                        if (!Membership.ValidateUser(username, password))
                            result.StringError = "Sai mật khẩu";
                        else
                        {
                            result.UserName = username;
                            var objUser = default(DTOAuthorization);
                            ServiceFactory.SVApp((ISVSystem sv) =>
                            {
                                objUser = sv.App_User();
                            }, username, -1);
                            if (objUser == null)
                                result.StringError = "Tài khoản không tồn tại";
                            else
                            {
                                result.UserName = objUser.UserName;
                                result.UserID = objUser.UserID;
                                result.IsAdmin = objUser.IsAdmin;
                                result.GroupID = objUser.GroupID;
                                result.SYSCustomerID = objUser.SYSCustomerID;
                                result.LastName = objUser.LastName;
                                result.FirstName = objUser.FirstName;
                                result.CustomerName = objUser.CustomerName;
                                result.Address = objUser.Address;
                                result.TelNo = objUser.TelNo;
                                result.Fax = objUser.Fax;
                                result.Email = objUser.Email;
                                result.Note = objUser.Note;
                                result.Note1 = objUser.Note1;
                                result.Note2 = objUser.Note2;
                                result.Image = objUser.Image;
                                result.ListCustomerID = objUser.ListCustomerID;
                                result.DriverID = objUser.DriverID;
                                result.CustomerID = objUser.CustomerID;
                                result.ListActionCode = new List<string>();
                                RegistryCache(result);
                            }
                        }
                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.StackTrace);
            }
        }

        [HttpPost]
        public void App_Logout(dynamic dynParam)
        {
            try
            {
                SecurityHelper.DelCache();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.StackTrace);
            }
        }

        [HttpPost]
        public List<SYSFunction> App_ListFunction()
        {
            try
            {
                var result = new List<SYSFunction>();
                var obj = default(DTOAuthorization);
                try
                {
                    obj = GetCache();
                }
                catch { }
                if (obj != null)
                {
                    ServiceFactory.SVSystem((ISVSystem sv) =>
                    {
                        result = sv.App_ListFunction(2);
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
        public List<SYSFunction> App_ListFunctionMobile()
        {
            try
            {
                var result = new List<SYSFunction>();
                var obj = default(DTOAuthorization);
                try
                {
                    obj = GetCache();
                }
                catch { }
                if (obj != null)
                {
                    ServiceFactory.SVSystem((ISVSystem sv) =>
                    {
                        result = sv.App_ListFunction(3);
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
        public void App_ChangeAction(dynamic dynParam)
        {
            try
            {
                var user = GetCache();
                user.ListActionCode = new List<string>();
                List<string> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<string>>(dynParam.ListActionCode.ToString());
                //List<SYSAction> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<SYSAction>>(dynParam.ListActions.ToString());
                user.ListActionCode = lst;
            }
            catch { }

        }

        [HttpPost]
        public List<SYSResource> App_ListResource(dynamic dynParam)
        {
            try
            {
                var result = new List<SYSResource>();
                ServiceFactory.SVSystem((ISVSystem sv) =>
                {
                    result = sv.App_ListResource();
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public List<SYSResource> App_ListResourceEmpty(dynamic dynParam)
        {
            try
            {
                var result = new List<SYSResource>();
                ServiceFactory.SVApp((ISVSystem sv) =>
                {
                    result = sv.App_ListResourceEmpty();
                }, "", -1);
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void App_UserGridSetting_Save(dynamic dynParam)
        {
            try
            {
                int referID = (int)dynParam.referID;
                string referKey = dynParam.referKey;
                SYSUserSettingFunction_Grid item = Newtonsoft.Json.JsonConvert.DeserializeObject<SYSUserSettingFunction_Grid>(dynParam.item.ToString());
                ServiceFactory.SVSystem((ISVSystem sv) =>
                {
                    sv.App_UserGridSetting_Save(referID, referKey, item);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void App_UserOptionsSetting_Save(dynamic dynParam)
        {
            try
            {
                int referID = (int)dynParam.referID;
                string referKey = dynParam.referKey;
                Dictionary<string, string> options = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, string>>(dynParam.options.ToString());
                ServiceFactory.SVSystem((ISVSystem sv) =>
                {
                    sv.App_UserOptionsSetting_Save(referID, referKey, options);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void App_UserGridSetting_Delete(dynamic dynParam)
        {
            try
            {
                int referID = (int)dynParam.referID;
                string referKey = dynParam.referKey;
                SYSUserSettingFunction_Grid item = Newtonsoft.Json.JsonConvert.DeserializeObject<SYSUserSettingFunction_Grid>(dynParam.item.ToString());
                ServiceFactory.SVSystem((ISVSystem sv) =>
                {
                    sv.App_UserGridSetting_Delete(referID, referKey, item);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void App_UserSettingSave(SYSUserSettingFunction item)
        {
            try
            {
                var result = new List<SYSResource>();
                ServiceFactory.SVSystem((ISVSystem sv) =>
                {
                    sv.App_UserSettingSave(item);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        [HttpPost]
        public DTOResult App_FileList(dynamic dynParam)
        {
            try
            {
                var result = new DTOResult();
                if (HasCache())
                {
                    string strCode = dynParam.code.ToString();
                    if (!string.IsNullOrEmpty(strCode))
                    {
                        string request = dynParam.request.ToString();
                        CATTypeOfFileCode code = (CATTypeOfFileCode)Enum.Parse(typeof(CATTypeOfFileCode), strCode);
                        int id = (int)dynParam.id;

                        ServiceFactory.SVSystem((ISVSystem sv) =>
                        {
                            result = sv.App_FileList(request, code, id);
                        });
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
        public int App_FileSave(dynamic dynParam)
        {
            try
            {
                CATFile item = Newtonsoft.Json.JsonConvert.DeserializeObject<CATFile>(dynParam.item.ToString());
                var result = -1;
                ServiceFactory.SVSystem((ISVSystem sv) =>
                {
                    result = sv.App_FileSave(item);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void App_FileDelete(dynamic dynParam)
        {
            try
            {
                List<int> lstid = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.lstid.ToString());
                ServiceFactory.SVSystem((ISVSystem sv) =>
                {
                    sv.App_FileDelete(lstid);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        [HttpPost]
        public List<CATComment> App_CommentList(dynamic dynParam)
        {
            try
            {
                var result = new List<CATComment>();
                if (HasCache())
                {
                    string strType = dynParam.type.ToString();
                    if (!string.IsNullOrEmpty(strType))
                    {
                        CATTypeOfCommentCode code = (CATTypeOfCommentCode)Enum.Parse(typeof(CATTypeOfCommentCode), strType);
                        int referid = (int)dynParam.referid;
                        ServiceFactory.SVSystem((ISVSystem sv) =>
                        {
                            result = sv.App_CommentList(code, referid);
                        });
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
        public long App_CommentSave(dynamic dynParam)
        {
            try
            {
                CATComment item = Newtonsoft.Json.JsonConvert.DeserializeObject<CATComment>(dynParam.item.ToString());
                long result = -1;
                ServiceFactory.SVSystem((ISVSystem sv) =>
                {
                    result = sv.App_CommentSave(item);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        [HttpPost]
        public List<DTOTriggerMessage> App_MessageCall_User(dynamic dynParam)
        {
            try
            {
                var result = new List<DTOTriggerMessage>();
                ServiceFactory.SVTrigger((IServices.ISVTrigger sv) =>
                {
                    result = sv.MessageCall_User();
                });
                try
                {
                    ServiceFactory.SVTrigger((IServices.ISVTrigger sv) =>
                    {
                        sv.MessageCall_Sended(result.Select(c => c.ID).ToList());
                    });
                }
                catch { }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTONotificationUser App_MessageCall_LoadMore(dynamic dynParam)
        {
            try
            {
                int currentPage = dynParam.currentPage;
                int pageSize = dynParam.pageSize;
                string typeOfMessage = dynParam.typeOfMessage;
                var result = new DTONotificationUser();
                result.ListMessage = new List<DTONotification>();
                ServiceFactory.SVTrigger((IServices.ISVTrigger sv) =>
                {
                    result = sv.MessageCall_LoadMore(currentPage, pageSize, typeOfMessage);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        [HttpPost]
        public void App_MessageCall_Read(dynamic dynParam)
        {
            try
            {
                ServiceFactory.SVTrigger((IServices.ISVTrigger sv) =>
                {
                    sv.MessageCall_Read();
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOResult Notification_Read(dynamic d)
        {
            try
            {
                string request = d.request.ToString();
                var result = default(DTOResult);
                ServiceFactory.SVSystem((ISVSystem sv) =>
                {
                    result = sv.Notification_Read(request);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        [HttpPost]
        public List<DTOTriggerMessage> App_WFL_MessageCall_User(dynamic dynParam)
        {
            try
            {
                var result = new List<DTOTriggerMessage>();
                ServiceFactory.SVTrigger((IServices.ISVTrigger sv) =>
                {
                    result = sv.WFL_MessageCall_User();
                });
                try
                {
                    ServiceFactory.SVTrigger((IServices.ISVTrigger sv) =>
                    {
                        sv.WFL_MessageCall_Sended(result.Select(c => c.ID).ToList());
                    });
                }
                catch { }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTONotificationUser App_WFL_MessageCall_LoadMore(dynamic dynParam)
        {
            try
            {
                int currentPage = dynParam.currentPage;
                int pageSize = dynParam.pageSize;
                string typeOfMessage = dynParam.typeOfMessage;
                var result = new DTONotificationUser();
                result.ListMessage = new List<DTONotification>();
                ServiceFactory.SVTrigger((IServices.ISVTrigger sv) =>
                {
                    result = sv.WFL_MessageCall_LoadMore(currentPage, pageSize, typeOfMessage);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        [HttpPost]
        public void App_WFL_MessageCall_Read(dynamic dynParam)
        {
            try
            {
                ServiceFactory.SVTrigger((IServices.ISVTrigger sv) =>
                {
                    sv.WFL_MessageCall_Read();
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOResult Notification_List(dynamic d)
        {
            try
            {
                string request = d.request.ToString();
                var result = default(DTOResult);
                ServiceFactory.SVSystem((ISVSystem sv) =>
                {
                    result = sv.Notification_List(request);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region SYSFunction
        [HttpPost]
        public DTOResult SYSFunction_Read(dynamic dynParam)
        {
            try
            {
                string request = dynParam.request.ToString();
                var result = new DTOResult();
                ServiceFactory.SVSystem((ISVSystem sv) =>
                {
                    result = sv.SYSFunction_Read(request);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public List<DTOCombobox> SYSFunction_Parent(dynamic dynParam)
        {
            try
            {
                int id = (int)dynParam.id;
                var result = new List<DTOCombobox>();
                ServiceFactory.SVSystem((ISVSystem sv) =>
                {
                    result = sv.SYSFunction_Parent(id);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public SYSFunction SYSFunction_Item(dynamic dynParam)
        {
            try
            {
                int id = (int)dynParam.id;
                var result = default(SYSFunction);
                ServiceFactory.SVSystem((ISVSystem sv) =>
                {
                    result = sv.SYSFunction_Item(id);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public SYSFunction SYSFunction_Move(dynamic dynParam)
        {
            try
            {
                int id = (int)dynParam.id;
                int typeid = (int)dynParam.typeid;
                var result = default(SYSFunction);
                ServiceFactory.SVSystem((ISVSystem sv) =>
                {
                    sv.SYSFunction_Move(id, typeid);
                    sv.SYSFunction_Refresh();
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public int SYSFunction_Save(dynamic dynParam)
        {
            try
            {
                SYSFunction item = Newtonsoft.Json.JsonConvert.DeserializeObject<SYSFunction>(dynParam.item.ToString());
                int result = -1;
                ServiceFactory.SVSystem((ISVSystem sv) =>
                {
                    result = sv.SYSFunction_Save(item);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void SYSFunction_Delete(dynamic dynParam)
        {
            try
            {
                List<int> lstid = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.lstid.ToString());
                ServiceFactory.SVSystem((ISVSystem sv) =>
                {
                    sv.SYSFunction_Delete(lstid);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void SYSFunction_Refresh()
        {
            try
            {
                ServiceFactory.SVSystem((ISVSystem sv) =>
                {
                    sv.SYSFunction_Refresh();
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public string SYSFunction_Export()
        {
            try
            {
                var data = default(SYSFunction_Export);
                ServiceFactory.SVSystem((ISVSystem sv) =>
                {
                    data = sv.SYSFunction_Export();
                });

                string filePath = "/" + FolderUpload.Export + "export" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx";
                if (System.IO.File.Exists(HttpContext.Current.Server.MapPath(filePath)))
                    System.IO.File.Delete(HttpContext.Current.Server.MapPath(filePath));
                FileInfo file = new FileInfo(HttpContext.Current.Server.MapPath(filePath));
                using (ExcelPackage package = new ExcelPackage(file))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Sheet 1");

                    int col = 0;
                    int row = 0;
                    row = 2;
                    col = 1; worksheet.Cells[row, col].Value = "ID";
                    col++; worksheet.Cells[row, col].Value = "ParentID";
                    col++; worksheet.Cells[row, col].Value = "Mã";
                    col++; worksheet.Cells[row, col].Value = "Chức năng";
                    col++; worksheet.Cells[row, col].Value = "Mô tả";
                    col++; worksheet.Cells[row, col].Value = "Hình";
                    col++; worksheet.Cells[row, col].Value = "Cấp độ";
                    col++; worksheet.Cells[row, col].Value = "Sắp xếp";
                    Dictionary<int, int> dicAction = new Dictionary<int, int>();
                    foreach (var item in data.ListAction)
                    {
                        col++;
                        worksheet.Cells[row - 1, col].Value = item.Code;
                        worksheet.Cells[row, col].Value = item.ActionName;
                        dicAction.Add(item.ID, col);
                    }
                    row++;
                    Dictionary<int, int> dicParent = new Dictionary<int, int>();
                    foreach (var objFunc in data.ListFunction.OrderBy(c => c.SortOrder))
                    {
                        if (objFunc.Level > 0)
                            worksheet.Row(row).OutlineLevel = objFunc.Level;
                        dicParent.Add(objFunc.ID, row);
                        col = 1; worksheet.Cells[row, col].Value = objFunc.ID;
                        col++;
                        if (objFunc.ParentID != null && dicParent.ContainsKey(objFunc.ParentID.Value))
                            worksheet.Cells[row, col].Formula = worksheet.Cells[dicParent[objFunc.ParentID.Value], 1].FullAddressAbsolute;
                        col++; worksheet.Cells[row, col].Value = objFunc.Code;
                        col++; worksheet.Cells[row, col].Value = objFunc.FunctionName;
                        col++; worksheet.Cells[row, col].Value = objFunc.Description;
                        col++; worksheet.Cells[row, col].Value = objFunc.Icon;
                        col++; worksheet.Cells[row, col].Value = objFunc.Level;
                        col++; worksheet.Cells[row, col].Value = objFunc.SortOrder;
                        foreach (var item in objFunc.ListActions)
                        {
                            if (dicAction.ContainsKey(item.ID))
                                worksheet.Cells[row, dicAction[item.ID]].Value = "x";
                        }

                        row++;
                    }

                    row++;

                    package.Save();
                    return filePath;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public List<SYSFunction_Import> SYSFunction_ImportCheck(dynamic dynParam)
        {
            try
            {
                CATFile file = Newtonsoft.Json.JsonConvert.DeserializeObject<CATFile>(dynParam.file.ToString());
                var result = new List<SYSFunction_Import>();

                var data = new SYSFunction_ImportCheck() { ListFunction = new List<SYSFunction>(), ListAction = new List<SYSAction>() };
                ServiceFactory.SVSystem((ISVSystem sv) =>
                {
                    data = sv.SYSFunction_ImportCheck();
                });

                using (System.IO.FileStream fs = new System.IO.FileStream(HttpContext.Current.Server.MapPath("/" + file.FilePath), System.IO.FileMode.Open, System.IO.FileAccess.Read))
                {
                    using (var package = new ExcelPackage(fs))
                    {
                        ExcelWorksheet worksheet = ExcelHelper.GetWorksheetByIndex(package, 1);
                        if (worksheet != null)
                        {
                            Dictionary<int, int> dicAction = new Dictionary<int, int>();
                            int row = 1;
                            for (int col = 7; col < worksheet.Dimension.End.Column; col++)
                            {
                                string strCode = ExcelHelper.GetValue(worksheet, row, col);
                                var act = data.ListAction.FirstOrDefault(c => c.Code == strCode);
                                if (act != null)
                                    dicAction.Add(col, act.ID);
                            }

                            for (row = 3; row <= worksheet.Dimension.End.Row; row++)
                            {
                                int col = 1; string strID = ExcelHelper.GetValue(worksheet, row, col);
                                col++; string strParentID = ExcelHelper.GetValue(worksheet, row, col);
                                col++; string strCode = ExcelHelper.GetValue(worksheet, row, col);
                                col++; string strFunctionName = ExcelHelper.GetValue(worksheet, row, col);
                                col++; string strDescription = ExcelHelper.GetValue(worksheet, row, col);
                                col++; string strIcon = ExcelHelper.GetValue(worksheet, row, col);
                                col++; string strLevel = ExcelHelper.GetValue(worksheet, row, col);
                                col++; string strSortOrder = ExcelHelper.GetValue(worksheet, row, col);

                                int id = -1;
                                try { id = Convert.ToInt32(strID); }
                                catch { }
                                int parentid = -1;
                                try { parentid = Convert.ToInt32(strParentID); }
                                catch { }
                                int level = -1;
                                try { level = Convert.ToInt32(strLevel); }
                                catch { }
                                int sort = -1;
                                try { sort = Convert.ToInt32(strSortOrder); }
                                catch { }
                                //bool isDbl = result.Where(c => c.FunctionCode == strCode).Count() > 0;

                                var obj = new SYSFunction_Import();
                                if (strCode != "" && strFunctionName != "")
                                {
                                    obj.ExcelSuccess = true;
                                    obj.ExcelError = "";
                                    obj.ID = id;
                                    obj.ParentID = parentid;
                                    obj.Code = strCode;
                                    obj.FunctionName = strFunctionName;
                                    obj.Description = strDescription;
                                    obj.Icon = strIcon;
                                    obj.Level = level;
                                    obj.SortOrder = sort;
                                    //obj.ListActionID = new List<int>();
                                    //obj.StringAction = string.Empty;

                                    //foreach (var act in dicAction)
                                    //{
                                    //    string strAct = ExcelHelper.GetValue(worksheet, row, act.Key);
                                    //    if (strAct.ToLower() == "x")
                                    //    {
                                    //        obj.ListActionID.Add(act.Value);
                                    //        var objAct = lstAction.FirstOrDefault(c => c.ID == act.Value);
                                    //        if (objAct != null)
                                    //            obj.StringAction += "," + objAct.Code;
                                    //    }
                                    //}
                                    //if (obj.StringAction != string.Empty)
                                    //    obj.StringAction = obj.StringAction.Substring(1);
                                }
                                else
                                {
                                    obj.ExcelSuccess = false;
                                    obj.ExcelError = "";
                                    obj.Code = strCode;
                                    obj.FunctionName = strFunctionName;
                                    obj.Description = strDescription;
                                    obj.Icon = strIcon;
                                }
                                result.Add(obj);
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
        public void SYSFunction_ExcelImport(dynamic dynParam)
        {
            try
            {
                List<SYSFunction_Import> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<SYSFunction_Import>>(dynParam.lst.ToString());
                ServiceFactory.SVSystem((ISVSystem sv) =>
                {
                    sv.SYSFunction_ExcelImport(lst);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region SYSConfig
        [HttpPost]
        public DTOResult SYSConfigGroup_Read(dynamic dynParam)
        {
            try
            {
                string request = dynParam.request.ToString();
                var result = new DTOResult();
                ServiceFactory.SVSystem((ISVSystem sv) =>
                {
                    result = sv.SYSConfigGroup_Read(request);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public SYSGroup SYSConfigGroup_GroupItem(dynamic dynParam)
        {
            try
            {
                int groupid = (int)dynParam.groupid;
                var result = default(SYSGroup);
                ServiceFactory.SVSystem((ISVSystem sv) =>
                {
                    result = sv.SYSConfigGroup_GroupItem(groupid);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOResult SYSConfigFunction_InRead(dynamic dynParam)
        {
            try
            {
                string request = dynParam.request.ToString();
                int groupid = (int)dynParam.groupid;
                var result = new DTOResult();
                ServiceFactory.SVSystem((ISVSystem sv) =>
                {
                    result = sv.SYSConfigFunction_InRead(request, groupid);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOResult SYSConfigFunction_NotInRead(dynamic dynParam)
        {
            try
            {
                string request = dynParam.request.ToString();
                int groupid = (int)dynParam.groupid;
                var result = new DTOResult();
                ServiceFactory.SVSystem((ISVSystem sv) =>
                {
                    result = sv.SYSConfigFunction_NotInRead(request, groupid);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public string SYSConfigFunction_AddFunction(dynamic dynParam)
        {
            try
            {
                List<SYSFunction> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<SYSFunction>>(dynParam.lst.ToString());
                int groupid = (int)dynParam.groupid;
                ServiceFactory.SVSystem((ISVSystem sv) =>
                {
                    sv.SYSConfigFunction_AddFunction(lst, groupid);
                });
                return "";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public string SYSConfigFunction_DelFunction(dynamic dynParam)
        {
            try
            {
                List<SYSFunction> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<SYSFunction>>(dynParam.lst.ToString());
                int groupid = (int)dynParam.groupid;
                ServiceFactory.SVSystem((ISVSystem sv) =>
                {
                    sv.SYSConfigFunction_DelFunction(lst, groupid);
                });
                return "";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public SYSFunction SYSConfigFunction_GetItem(dynamic dynParam)
        {
            try
            {
                int groupid = (int)dynParam.groupid;
                int functionid = (int)dynParam.functionid;
                var result = default(SYSFunction);
                ServiceFactory.SVSystem((ISVSystem sv) =>
                {
                    result = sv.SYSConfigFunction_GetItem(groupid, functionid);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public string SYSConfigFunction_SaveItem(dynamic dynParam)
        {
            try
            {
                int groupid = (int)dynParam.groupid;
                int functionid = (int)dynParam.functionid;
                SYSFunction item = Newtonsoft.Json.JsonConvert.DeserializeObject<SYSFunction>(dynParam.item.ToString());
                ServiceFactory.SVSystem((ISVSystem sv) =>
                {
                    sv.SYSConfigFunction_SaveItem(groupid, functionid, item);
                });
                return "";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region SYSUser
        [HttpPost]
        public DTOResult SYSUser_Read(dynamic dynParam)
        {
            try
            {
                string request = dynParam.request.ToString();
                var result = new DTOResult();
                ServiceFactory.SVSystem((ISVSystem sv) =>
                {
                    result = sv.SYSUser_Read(request);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public SYSUser SYSUser_Get(dynamic dynParam)
        {
            try
            {
                int id = (int)dynParam.id;
                var result = default(SYSUser);
                ServiceFactory.SVSystem((ISVSystem sv) =>
                {
                    result = sv.SYSUser_Get(id);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public List<DTOCombobox> SYSUser_Group()
        {
            try
            {
                var result = new List<DTOCombobox>();
                ServiceFactory.SVSystem((ISVSystem sv) =>
                {
                    result = sv.SYSUser_Group();
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public List<DTOCombobox> SYSUser_Customer()
        {
            try
            {
                var result = new List<DTOCombobox>();
                ServiceFactory.SVSystem((ISVSystem sv) =>
                {
                    result = sv.SYSUser_Customer();
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public bool SYSUser_CheckUserName(dynamic dynParam)
        {
            try
            {
                int id = (int)dynParam.id;
                string username = (string)dynParam.username;
                var result = false;
                ServiceFactory.SVSystem((ISVSystem sv) =>
                {
                    result = sv.SYSUser_CheckUserName(id, username);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public bool SYSUser_CheckIsAdmin(dynamic dynParam)
        {
            try
            {
                var result = false;
                ServiceFactory.SVSystem((ISVSystem sv) =>
                {
                    result = sv.SYSUser_CheckIsAdmin();
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public int SYSUser_CheckData(dynamic dynParam)
        {
            try
            {
                int id = (int)dynParam.id;
                string username = (string)dynParam.username;
                string email = (string)dynParam.email;
                var result = -1;
                ServiceFactory.SVSystem((ISVSystem sv) =>
                {
                    result = sv.SYSUser_CheckData(id, username, email);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public int SYSUser_Save(dynamic dynParam)
        {
            try
            {
                SYSUser item = Newtonsoft.Json.JsonConvert.DeserializeObject<SYSUser>(dynParam.item.ToString());
                var user = Membership.GetUser(item.UserName);
                if (user == null || user.UserName == "")
                {
                    var password = item.Password;
                    if (password == "" || password == null)
                        password = System.Configuration.ConfigurationManager.AppSettings["password"];
                    user = Membership.CreateUser(item.UserName, password, item.Email);
                    user.IsApproved = true;
                    Membership.UpdateUser(user);
                }
                else
                {
                    if (item.IsApproved && user.IsLockedOut)
                        user.UnlockUser();
                    if (item.Password != "" && item.Password != null)
                    {
                        var password = user.ResetPassword();
                        user.ChangePassword(password, item.Password);
                    }
                    if (!user.IsApproved)
                    {
                        user.IsApproved = true;
                        Membership.UpdateUser(user);
                    }
                }
                var result = -1;
                ServiceFactory.SVSystem((ISVSystem sv) =>
                {
                    result = sv.SYSUser_Save(item);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void SYSUser_Delete(dynamic dynParam)
        {
            try
            {
                List<int> lstid = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.lstid.ToString());
                ServiceFactory.SVSystem((ISVSystem sv) =>
                {
                    sv.SYSUser_Delete(lstid);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public string SYSUser_Export()
        {
            try
            {
                var data = new SYSUser_Export { ListUser = new List<SYSUser>(), ListCustomer = new List<CUSCustomer>() };
                ServiceFactory.SVSystem((ISVSystem sv) =>
                {
                    data = sv.SYSUser_Export();
                });

                string filePath = "/" + FolderUpload.Export + "file" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx";
                if (System.IO.File.Exists(HttpContext.Current.Server.MapPath(filePath)))
                    System.IO.File.Delete(HttpContext.Current.Server.MapPath(filePath));
                FileInfo file = new FileInfo(HttpContext.Current.Server.MapPath(filePath));
                using (ExcelPackage package = new ExcelPackage(file))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Sheet 1");
                    int col = 0, row = 1, stt = 0;

                    row = 1;
                    col = 1; worksheet.Cells[row, col].Value = "STT"; worksheet.Column(col).Width = 10;
                    col++; worksheet.Cells[row, col].Value = "Tài khoản"; worksheet.Column(col).Width = 15;
                    col++; worksheet.Cells[row, col].Value = "Mật khẩu"; worksheet.Column(col).Width = 20;
                    col++; worksheet.Cells[row, col].Value = "Họ"; worksheet.Column(col).Width = 20;
                    col++; worksheet.Cells[row, col].Value = "Tên"; worksheet.Column(col).Width = 35;
                    col++; worksheet.Cells[row, col].Value = "Email"; worksheet.Column(col).Width = 20;
                    col++; worksheet.Cells[row, col].Value = "SĐT"; worksheet.Column(col).Width = 20;
                    col++; worksheet.Cells[row, col].Value = "Vai trò"; worksheet.Column(col).Width = 20;
                    col++; worksheet.Cells[row, col].Value = "Tên vai trò"; worksheet.Column(col).Width = 20;
                    col++; worksheet.Cells[row, col].Value = "Đang hoạt động"; worksheet.Column(col).Width = 10;
                    col++; worksheet.Cells[row, col].Value = "Danh sách khách hàng"; worksheet.Column(col).Width = 35;
                    col++; worksheet.Cells[row, col].Value = "Thuộc chi nhánh"; worksheet.Column(col).Width = 10;

                    row++;
                    stt = 1;
                    foreach (var item in data.ListUser)
                    {
                        col = 1; worksheet.Cells[row, col].Value = stt;
                        col++; worksheet.Cells[row, col].Value = item.UserName;
                        col++;
                        col++; worksheet.Cells[row, col].Value = item.LastName;
                        col++; worksheet.Cells[row, col].Value = item.FirstName;
                        col++; worksheet.Cells[row, col].Value = item.Email;
                        col++; worksheet.Cells[row, col].Value = item.TelNo;
                        col++; worksheet.Cells[row, col].Value = item.GroupCode;
                        col++; worksheet.Cells[row, col].Value = item.GroupName;
                        col++; worksheet.Cells[row, col].Value = item.IsApproved == true ? "x" : "";
                        col++;
                        var lstCustomerCode = "";
                        if (!string.IsNullOrEmpty(item.ListCustomerID))
                        {
                            var strs = item.ListCustomerID.Split(',');
                            foreach (var strid in strs)
                            {
                                var obj = data.ListCustomer.FirstOrDefault(c => c.ID == Convert.ToInt32(strid));
                                if (obj != null)
                                    lstCustomerCode += "," + obj.Code;
                            }
                            if (lstCustomerCode != "")
                                lstCustomerCode = lstCustomerCode.Substring(1);
                        }
                        worksheet.Cells[row, col].Value = lstCustomerCode;
                        col++;
                        if (item.SYSCustomerID > 0)
                        {
                            var cus = data.ListSYSCustomer.FirstOrDefault(c => c.ID == item.SYSCustomerID.Value);
                            if (cus != null)
                                worksheet.Cells[row, col].Value = cus.Code;
                        }

                        row++;
                        stt++;
                    }

                    package.Save();
                    return filePath;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public List<SYSUser_Import> SYSUser_ImportCheck(dynamic dynParam)
        {
            try
            {
                CATFile file = Newtonsoft.Json.JsonConvert.DeserializeObject<CATFile>(dynParam.file.ToString());
                var result = new List<SYSUser_Import>();

                var data = new SYSUser_ImportCheck() { ListUser = new List<SYSUser>(), ListGroup = new List<SYSGroup>() };
                ServiceFactory.SVSystem((ISVSystem sv) =>
                {
                    data = sv.SYSUser_ImportCheck();
                });

                using (System.IO.FileStream fs = new System.IO.FileStream(HttpContext.Current.Server.MapPath("/" + file.FilePath), System.IO.FileMode.Open, System.IO.FileAccess.Read))
                {
                    using (var package = new ExcelPackage(fs))
                    {
                        ExcelWorksheet worksheet = ExcelHelper.GetWorksheetByIndex(package, 1);
                        if (worksheet != null)
                        {
                            int col = 0, row = 1;

                            for (row = 2; row <= worksheet.Dimension.End.Row; row++)
                            {
                                col = 1; var strRow = ExcelHelper.GetValue(worksheet, row, col);
                                col++; var strUserName = ExcelHelper.GetValue(worksheet, row, col);
                                col++;
                                col++; var strLastName = ExcelHelper.GetValue(worksheet, row, col);
                                col++; var strFirstName = ExcelHelper.GetValue(worksheet, row, col);
                                col++; var strEmail = ExcelHelper.GetValue(worksheet, row, col);
                                col++; var strSDT = ExcelHelper.GetValue(worksheet, row, col);
                                col++; var strGroupCode = ExcelHelper.GetValue(worksheet, row, col);
                                col++;
                                col++; var strIsApproved = ExcelHelper.GetValue(worksheet, row, col);
                                col++; var strListCustomerCode = ExcelHelper.GetValue(worksheet, row, col);
                                col++; var strSYSCustomerCode = ExcelHelper.GetValue(worksheet, row, col);

                                if (string.IsNullOrEmpty(strRow))
                                    break;

                                int userid = -1;
                                try
                                {
                                    userid = data.ListUser.FirstOrDefault(c => c.UserName == strUserName).ID;
                                }
                                catch { }
                                bool isemail = true;
                                isemail = data.ListUser.Where(c => c.ID != userid && c.Email == strEmail).Count() == 0;
                                if (isemail == true)
                                    isemail = StringHelper.IsEmail(strEmail);
                                int groupid = -1;
                                try
                                {
                                    groupid = data.ListGroup.FirstOrDefault(c => c.Code == strGroupCode).ID;
                                }
                                catch { }
                                bool isapproved = false;
                                try
                                {
                                    isapproved = Convert.ToBoolean(strIsApproved);
                                }
                                catch { }
                                var strCustomerID = "";
                                foreach (var str in strListCustomerCode.Split(','))
                                {
                                    var obj = data.ListCustomer.FirstOrDefault(c => c.Code == str);
                                    if (obj != null)
                                        strCustomerID += "," + obj.ID;
                                }
                                if (strCustomerID != "")
                                    strCustomerID = strCustomerID.Substring(1);
                                var sysCustomerID = -1;
                                try
                                {
                                    sysCustomerID = data.ListSYSCustomer.FirstOrDefault(c => c.Code == strSYSCustomerCode).ID;
                                }
                                catch { }

                                var item = new SYSUser_Import();
                                item.ID = userid;
                                item.ExcelRow = row;
                                item.UserName = strUserName;
                                item.LastName = strLastName;
                                item.FirstName = strFirstName;
                                item.Email = strEmail;
                                item.TelNo = strSDT;
                                item.GroupID = groupid;
                                item.IsApproved = isapproved;
                                item.ListCustomerID = strCustomerID;
                                item.ListCustomerCode = strListCustomerCode;
                                item.SYSCustomerCode = strSYSCustomerCode;
                                item.SYSCustomerID = sysCustomerID;
                                if (isemail)
                                {
                                    item.ExcelSuccess = true;
                                    item.ExcelError = "";
                                }
                                else
                                {
                                    item.ExcelSuccess = false;
                                    if (!isemail)
                                        item.ExcelError = "Email đã sử dụng hoặc nhập sai";
                                }
                                result.Add(item);
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
        public void SYSUser_Import(dynamic dynParam)
        {
            try
            {
                List<SYSUser_Import> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<SYSUser_Import>>(dynParam.lst.ToString());
                ServiceFactory.SVSystem((ISVSystem sv) =>
                {
                    sv.SYSUser_Import(lst);
                });

                foreach (var item in lst.Where(c => c.ExcelSuccess))
                {
                    var user = Membership.GetUser(item.UserName);
                    if (user == null || user.UserName == "")
                    {
                        var password = item.Password;
                        if (password == "" || password == null)
                            password = System.Configuration.ConfigurationManager.AppSettings["password"];
                        user = Membership.CreateUser(item.UserName, password, item.Email);
                        user.IsApproved = true;
                        Membership.UpdateUser(user);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public List<DTOCombobox> SYSUser_Driver()
        {
            try
            {
                var result = new List<DTOCombobox>();
                ServiceFactory.SVSystem((ISVSystem sv) =>
                {
                    result = sv.SYSUser_Driver();
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region SYSGroup
        [HttpPost]
        public DTOResult SYSGroup_Read(dynamic dynParam)
        {
            try
            {
                string request = dynParam.request.ToString();
                var result = new DTOResult();
                ServiceFactory.SVSystem((ISVSystem sv) =>
                {
                    result = sv.SYSGroup_Read(request);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

       

        [HttpPost]
        public SYSGroup SYSGroup_Item(dynamic dynParam)
        {
            try
            {
                int id = (int)dynParam.id;
                var result = default(SYSGroup);
                ServiceFactory.SVSystem((ISVSystem sv) =>
                {
                    result = sv.SYSGroup_Item(id);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public int SYSGroup_Save(dynamic dynParam)
        {
            try
            {
                SYSGroup item = Newtonsoft.Json.JsonConvert.DeserializeObject<SYSGroup>(dynParam.item.ToString());
                int result = -1;
                ServiceFactory.SVSystem((ISVSystem sv) =>
                {
                    result = sv.SYSGroup_Save(item);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void SYSGroup_Delete(dynamic dynParam)
        {
            try
            {
                List<int> lstid = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.lstid.ToString());
                ServiceFactory.SVSystem((ISVSystem sv) =>
                {
                    sv.SYSGroup_Delete(lstid);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region SYSGroupChild
        [HttpPost]
        public DTOResult SYSGroupChild_Read(dynamic dynParam)
        {
            try
            {
                string request = dynParam.request.ToString();
                var result = new DTOResult();
                ServiceFactory.SVSystem((ISVSystem sv) =>
                {
                    result = sv.SYSGroupChild_Read(request);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public List<DTOCombobox> SYSGroup_Parent(dynamic dynParam)
        {
            try
            {
                int id = (int)dynParam.id;
                var result = new List<DTOCombobox>();
                ServiceFactory.SVSystem((ISVSystem sv) =>
                {
                    result = sv.SYSGroupChild_Parent(id);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public SYSGroup SYSGroupChild_Item(dynamic dynParam)
        {
            try
            {
                int id = (int)dynParam.id;
                var result = default(SYSGroup);
                ServiceFactory.SVSystem((ISVSystem sv) =>
                {
                    result = sv.SYSGroupChild_Item(id);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public int SYSGroupChild_Save(dynamic dynParam)
        {
            try
            {
                SYSGroup item = Newtonsoft.Json.JsonConvert.DeserializeObject<SYSGroup>(dynParam.item.ToString());
                int result = -1;
                ServiceFactory.SVSystem((ISVSystem sv) =>
                {
                    result = sv.SYSGroupChild_Save(item);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void SYSGroupChild_Delete(dynamic dynParam)
        {
            try
            {
                List<int> lstid = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.lstid.ToString());
                ServiceFactory.SVSystem((ISVSystem sv) =>
                {
                    sv.SYSGroupChild_Delete(lstid);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region userprofile
        [HttpPost]
        public DTOUserProfile UserProfile_GetUser()
        {
            try
            {
                var result = default(DTOUserProfile);
                ServiceFactory.SVSystem((ISVSystem sv) =>
                {
                    result = sv.UserProfile_GetUser();
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void UserProfile_SavePass(dynamic dynParam)
        {
            try
            {
                DTOUserProfile item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOUserProfile>(dynParam.item.ToString());
                var user = Membership.GetUser(item.UserName);
                item.Password = item.Password.Trim();
                item.NewPassword = item.NewPassword.Trim();
                if (user != null && user.UserName != "")
                {
                    if (!Membership.ValidateUser(item.UserName, item.Password))
                        throw FaultHelper.BusinessFault(null, null, "Mật khẩu hiện tại không đúng");
                    if (!string.IsNullOrEmpty(item.NewPassword))
                    {
                        var password = user.ResetPassword();
                        user.ChangePassword(password, item.NewPassword);
                    }
                }
                ServiceFactory.SVSystem((ISVSystem sv) =>
                {
                    sv.UserProfile_Save(item);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public void UserProfile_Save(dynamic dynParam)
        {
            try
            {
                DTOUserProfile item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOUserProfile>(dynParam.item.ToString());
                ServiceFactory.SVSystem((ISVSystem sv) =>
                {
                    sv.UserProfile_Save(item);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Mail Template
        [HttpPost]
        public DTOResult SYSMailTemplate_Read(dynamic dynParam)
        {
            try
            {
                string request = dynParam.request.ToString();
                var result = new DTOResult();
                ServiceFactory.SVSystem((ISVSystem sv) =>
                {
                    result = sv.MailTemplate_List(request);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public CATMailTemplate SYSMailTemplate_Get(dynamic dynParam)
        {
            try
            {
                int id = (int)dynParam.id;
                var result = default(CATMailTemplate);
                ServiceFactory.SVSystem((ISVSystem sv) =>
                {
                    result = sv.MailTemplate_Get(id);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]                      
        public void SYSMailTemplate_Save(dynamic dynParam)
        {
            try
            {
                CATMailTemplate item = Newtonsoft.Json.JsonConvert.DeserializeObject<CATMailTemplate>(dynParam.item.ToString());
                ServiceFactory.SVSystem((ISVSystem sv) =>
                {
                    sv.MailTemplate_Save(item);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region SYSResource
        [HttpPost]
        public DTOResult SYSResource_List(dynamic dynParam)
        {
            try
            {
                string request = dynParam.request.ToString();
                int langID = (int)dynParam.langID;
                var result = new DTOResult();
                ServiceFactory.SVSystem((ISVSystem sv) =>
                {
                    result = sv.SYSResource_List(request, langID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOSYSResource SYSResource_Get(dynamic dynParam)
        {
            try
            {
                int id = (int)dynParam.id;
                int langID = (int)dynParam.langID;
                var result = default(DTOSYSResource);
                ServiceFactory.SVSystem((ISVSystem sv) =>
                {
                    result = sv.SYSResource_Get(id, langID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void SYSResource_Save(dynamic dynParam)
        {
            try
            {
                int langID = (int)dynParam.langID;
                DTOSYSResource item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOSYSResource>(dynParam.item.ToString());
                ServiceFactory.SVSystem((ISVSystem sv) =>
                {
                    sv.SYSResource_Save(item, langID);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void SYSResource_Delete(dynamic dynParam)
        {
            try
            {
                int langID = (int)dynParam.langID;
                DTOSYSResource item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOSYSResource>(dynParam.item.ToString());
                ServiceFactory.SVSystem((ISVSystem sv) =>
                {
                    sv.SYSResource_Delete(item, langID);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public string SYSResource_ExcelExport(dynamic dynParam)
        {
            try
            {
                int langID = (int)dynParam.langID;
                List<DTOSYSResource> data = new List<DTOSYSResource>();
                ServiceFactory.SVSystem((ISVSystem sv) =>
                {
                    data = sv.SYSResource_Data(langID);
                });

                string file = "/" + FolderUpload.Export + "Dữ liệu hệ thống_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx";

                if (System.IO.File.Exists(HttpContext.Current.Server.MapPath(file)))
                    System.IO.File.Delete(HttpContext.Current.Server.MapPath(file));
                FileInfo exportfile = new FileInfo(HttpContext.Current.Server.MapPath(file));
                using (ExcelPackage package = new ExcelPackage(exportfile))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Sheet 1");
                    int col = 0, row = 1;

                    #region Header
                    col++; worksheet.Cells[row, col].Value = "STT"; worksheet.Column(col).Width = 5;
                    col++; worksheet.Cells[row, col].Value = "Mã"; worksheet.Column(col).Width = 25;
                    col++; worksheet.Cells[row, col].Value = "Tên"; worksheet.Column(col).Width = 25;
                    col++; worksheet.Cells[row, col].Value = "Tên ngắn"; worksheet.Column(col).Width = 25;
                    col++; worksheet.Cells[row, col].Value = "Mặt định"; worksheet.Column(col).Width = 20;

                    ExcelHelper.CreateCellStyle(worksheet, row, 1, row, col, false, false, ExcelHelper.ColorGreen, ExcelHelper.ColorWhite);

                    worksheet.Cells[1, 1, row, col].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    worksheet.Cells[1, 1, row, col].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                    #endregion

                    #region data
                    int stt = 0;
                    row = 2;
                    foreach (var item in data)
                    {
                        col = 1;
                        worksheet.Cells[row, col].Value = stt;
                        col++; worksheet.Cells[row, col].Value = item.Key;
                        col++; worksheet.Cells[row, col].Value = item.Name;
                        col++; worksheet.Cells[row, col].Value = item.ShortName;
                        if (item.IsDefault == true)
                        { col++; worksheet.Cells[row, col].Value = "X"; }
                        else { col++; worksheet.Cells[row, col].Value = ""; }
                        stt++;
                        row++;
                    }
                    #endregion

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
        public List<DTOSYSResourceImport> SYSResource_ExcelCheck(dynamic dynPram)
        {
            try
            {
                int langID = (int)dynPram.langID;
                CATFile item = Newtonsoft.Json.JsonConvert.DeserializeObject<CATFile>(dynPram.item.ToString());

                List<DTOSYSResourceImport> result = new List<DTOSYSResourceImport>();


                if (item != null && !string.IsNullOrEmpty(item.FilePath))
                {
                    List<DTOSYSResource> data = new List<DTOSYSResource>();

                    ServiceFactory.SVSystem((ISVSystem sv) =>
                    {
                        data = sv.SYSResource_Data(langID);
                    });

                    using (System.IO.FileStream fs = new System.IO.FileStream(HttpContext.Current.Server.MapPath("/" + item.FilePath), System.IO.FileMode.Open, System.IO.FileAccess.Read))
                    {
                        using (var package = new ExcelPackage(fs))
                        {
                            ExcelWorksheet worksheet = ExcelHelper.GetWorksheetByIndex(package, 1);
                            int row = 2, col = 1;
                            string input;
                            if (worksheet != null)
                            {
                                while (row <= worksheet.Dimension.End.Row)
                                {
                                    col = 2;
                                    List<string> lstError = new List<string>();
                                    DTOSYSResourceImport obj = new DTOSYSResourceImport();
                                    obj.ExcelRow = row;

                                    input = ExcelHelper.GetValue(worksheet, row, col);

                                    if (!string.IsNullOrEmpty(input))
                                    {
                                        var check = data.FirstOrDefault(c => c.Key == input);
                                        if (check != null)
                                        {
                                            obj.ID = check.ID;
                                            obj.ResourceID = check.ResourceID;
                                        }
                                        else
                                        {
                                            obj.ID = 0;
                                            obj.ResourceID = 0;
                                        }
                                        obj.Key = input;

                                        var checkOnFile = result.FirstOrDefault(c => c.Key == input);
                                        if (checkOnFile != null) lstError.Add("Mã [" + input + "] đã sử dụng trên file");
                                    }
                                    else
                                        lstError.Add("[Mã] không được trống");

                                    //name
                                    col++; input = ExcelHelper.GetValue(worksheet, row, col);

                                    if (!string.IsNullOrEmpty(input))
                                    {
                                        obj.Name = input;
                                    }
                                    else
                                        lstError.Add("[Tên] không được trống");
                                    // shortName
                                    col++; input = ExcelHelper.GetValue(worksheet, row, col);
                                    if (!string.IsNullOrEmpty(input))
                                    {
                                        obj.ShortName = input;
                                    }
                                    else
                                        lstError.Add("[Tên ngắn] không được trống");

                                    //Is Default
                                    col++; input = ExcelHelper.GetValue(worksheet, row, col);

                                    if (!string.IsNullOrEmpty(input))
                                    {
                                        if (input == "X" || input == "x")
                                            obj.IsDefault = true;
                                        else obj.IsDefault = false;
                                    }
                                    else
                                        obj.IsDefault = false;

                                    obj.ExcelError = string.Empty;
                                    obj.ExcelSuccess = true;
                                    if (lstError.Count() > 0) { obj.ExcelSuccess = false; obj.ExcelError = string.Join(", ", lstError); }

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
        [HttpPost]
        public void SYSResource_ExcelImport(dynamic dynParam)
        {
            try
            {
                int langID = (int)dynParam.langID;
                List<DTOSYSResourceImport> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DTOSYSResourceImport>>(dynParam.lst.ToString());
                ServiceFactory.SVSystem((ISVSystem sv) =>
                {
                    sv.SYSResource_Import(lst, langID);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public void SYSResource_ChangeDefault(dynamic dynParam)
        {
            try
            {
                int langID = (int)dynParam.langID;
                ServiceFactory.SVSystem((ISVSystem sv) =>
                {
                    sv.SYSResource_ChangeDefault(langID);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region SYSUserResource
        [HttpPost]
        public DTOResult SYSUserResource_List(dynamic dynParam)
        {
            try
            {
                string request = dynParam.request.ToString();
                var result = new DTOResult();
                ServiceFactory.SVSystem((ISVSystem sv) =>
                {
                    result = sv.SYSUserResource_List(request);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOSYSUserResource SYSUserResource_Get(dynamic dynParam)
        {
            try
            {
                int id = (int)dynParam.id;
                var result = default(DTOSYSUserResource);
                ServiceFactory.SVSystem((ISVSystem sv) =>
                {
                    result = sv.SYSUserResource_Get(id);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void SYSUserResource_Save(dynamic dynParam)
        {
            try
            {
                DTOSYSUserResource item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOSYSUserResource>(dynParam.item.ToString());
                ServiceFactory.SVSystem((ISVSystem sv) =>
                {
                    sv.SYSUserResource_Save(item);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        [HttpPost]
        public string SYSUserResource_ExcelExport()
        {
            try
            {
                List<DTOSYSUserResource> data = new List<DTOSYSUserResource>();
                ServiceFactory.SVSystem((ISVSystem sv) =>
                {
                    data = sv.SYSUserResource_Data();
                });

                string file = "/" + FolderUpload.Export + "Dữ liệu tài khoản_" + Account.UserName + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx";

                if (System.IO.File.Exists(HttpContext.Current.Server.MapPath(file)))
                    System.IO.File.Delete(HttpContext.Current.Server.MapPath(file));
                FileInfo exportfile = new FileInfo(HttpContext.Current.Server.MapPath(file));
                using (ExcelPackage package = new ExcelPackage(exportfile))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Sheet 1");
                    int col = 0, row = 1;

                    #region Header
                    col++; worksheet.Cells[row, col].Value = "STT"; worksheet.Column(col).Width = 5;
                    col++; worksheet.Cells[row, col].Value = "Mã hệ thống"; worksheet.Column(col).Width = 25;
                    col++; worksheet.Cells[row, col].Value = "Tên hệ thống"; worksheet.Column(col).Width = 25;
                    col++; worksheet.Cells[row, col].Value = "Tên người dùng"; worksheet.Column(col).Width = 25;
                    col++; worksheet.Cells[row, col].Value = "Short Name"; worksheet.Column(col).Width = 25;

                    ExcelHelper.CreateCellStyle(worksheet, row, 1, row, col, false, false, ExcelHelper.ColorGreen, ExcelHelper.ColorWhite);

                    worksheet.Cells[1, 1, row, col].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    worksheet.Cells[1, 1, row, col].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                    #endregion

                    #region data
                    int stt = 0;
                    row = 2;
                    foreach (var item in data)
                    {
                        col = 1;
                        worksheet.Cells[row, col].Value = stt;
                        col++; worksheet.Cells[row, col].Value = item.SYSKey;
                        col++; worksheet.Cells[row, col].Value = item.SYSName;
                        col++; worksheet.Cells[row, col].Value = item.Name;
                        col++; worksheet.Cells[row, col].Value = item.ShortName;
                        stt++;
                        row++;
                    }
                    #endregion

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
        public List<DTOSYSUserResourceImport> SYSUserResource_ExcelCheck(dynamic dynPram)
        {
            try
            {
                CATFile item = Newtonsoft.Json.JsonConvert.DeserializeObject<CATFile>(dynPram.item.ToString());

                List<DTOSYSUserResourceImport> result = new List<DTOSYSUserResourceImport>();


                if (item != null && !string.IsNullOrEmpty(item.FilePath))
                {
                    List<DTOSYSUserResource> data = new List<DTOSYSUserResource>();

                    ServiceFactory.SVSystem((ISVSystem sv) =>
                    {
                        data = sv.SYSUserResource_Data();
                    });

                    using (System.IO.FileStream fs = new System.IO.FileStream(HttpContext.Current.Server.MapPath("/" + item.FilePath), System.IO.FileMode.Open, System.IO.FileAccess.Read))
                    {
                        using (var package = new ExcelPackage(fs))
                        {
                            ExcelWorksheet worksheet = ExcelHelper.GetWorksheetByIndex(package, 1);
                            int row = 2, col = 1;
                            string input;
                            if (worksheet != null)
                            {
                                while (row <= worksheet.Dimension.End.Row)
                                {
                                    col = 2;
                                    List<string> lstError = new List<string>();
                                    DTOSYSUserResourceImport obj = new DTOSYSUserResourceImport();
                                    obj.ExcelRow = row;
                                    //lấy syskey
                                    input = ExcelHelper.GetValue(worksheet, row, col);

                                    if (!string.IsNullOrEmpty(input))
                                    {
                                        var check = data.FirstOrDefault(c => c.SYSKey == input);
                                        if (check != null)
                                        {
                                            obj.ID = check.ID;
                                            obj.ResourceID = check.ResourceID;
                                            obj.UserID = check.UserID;
                                            obj.SYSKey = check.SYSKey;
                                            obj.SYSName = check.SYSName;
                                        }
                                        else lstError.Add("[Mã hệ thống] không tìm thấy");
                                    }
                                    else
                                        lstError.Add("[Mã hệ thống] không được trống");

                                    //name
                                    col++;
                                    col++; input = ExcelHelper.GetValue(worksheet, row, col);

                                    obj.Name = string.IsNullOrEmpty(input) ? string.Empty : input;
                                    //shortname
                                    col++; input = ExcelHelper.GetValue(worksheet, row, col);
                                    obj.ShortName = string.IsNullOrEmpty(input) ? string.Empty : input;

                                    obj.ExcelError = string.Empty;
                                    obj.ExcelSuccess = true;
                                    if (lstError.Count() > 0) { obj.ExcelSuccess = false; obj.ExcelError = string.Join(", ", lstError); }

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
        [HttpPost]
        public void SYSUserResource_ExcelImport(dynamic dynParam)
        {
            try
            {
                List<DTOSYSUserResourceImport> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DTOSYSUserResourceImport>>(dynParam.lst.ToString());
                ServiceFactory.SVSystem((ISVSystem sv) =>
                {
                    sv.SYSUserResource_Import(lst);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public void SYSUserResource_SetDefault(dynamic dynParam)
        {
            try
            {
                ServiceFactory.SVSystem((ISVSystem sv) =>
                {
                    sv.SYSUserResource_SetDefault();
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void SYSUserResource_SetDefaultByUser(dynamic dynParam)
        {
            try
            {
                int userID = (int)dynParam.userID;
                ServiceFactory.SVSystem((ISVSystem sv) =>
                {
                    sv.SYSUserResource_SetDefaultByUser(userID);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Search
        [HttpPost]
        public DTOResult SYSSearchDI(dynamic dynParam)
        {
            try
            {
                string request = dynParam.request.ToString();
                string content = dynParam.content.ToString();
                int type = (int)dynParam.type;
                var result = new DTOResult();
                ServiceFactory.SVSystem((ISVSystem sv) =>
                {
                    result = sv.SYSSearchDI(type, content,request);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public DTOResult SYSSearchCO(dynamic dynParam)
        {
            try
            {
                string request = dynParam.request.ToString();
                string content = dynParam.content.ToString();
                int type = (int)dynParam.type;
                var result = new DTOResult();
                ServiceFactory.SVSystem((ISVSystem sv) =>
                {
                    result = sv.SYSSearchCO(type, content, request);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region UserChild
        [HttpPost]
        public DTOResult SYSUserChild_Read(dynamic dynParam)
        {
            try
            {
                string request = dynParam.request.ToString();
                var result = new DTOResult();
                ServiceFactory.SVSystem((ISVSystem sv) =>
                {
                    result = sv.SYSUserChild_Read(request);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public SYSUser SYSUserChild_Get(dynamic dynParam)
        {
            try
            {
                int id = (int)dynParam.id;
                var result = default(SYSUser);
                ServiceFactory.SVSystem((ISVSystem sv) =>
                {
                    result = sv.SYSUserChild_Get(id);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public List<DTOCombobox> SYSUserChild_Group()
        {
            try
            {
                var result = new List<DTOCombobox>();
                ServiceFactory.SVSystem((ISVSystem sv) =>
                {
                    result = sv.SYSUserChild_Group();
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public List<DTOCombobox> SYSUserChild_Customer()
        {
            try
            {
                var result = new List<DTOCombobox>();
                ServiceFactory.SVSystem((ISVSystem sv) =>
                {
                    result = sv.SYSUserChild_Customer();
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public bool SYSUserChild_CheckUserName(dynamic dynParam)
        {
            try
            {
                int id = (int)dynParam.id;
                string username = (string)dynParam.username;
                var result = false;
                ServiceFactory.SVSystem((ISVSystem sv) =>
                {
                    result = sv.SYSUserChild_CheckUserName(id, username);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public bool SYSUserChild_CheckIsAdmin(dynamic dynParam)
        {
            try
            {
                var result = false;
                ServiceFactory.SVSystem((ISVSystem sv) =>
                {
                    result = sv.SYSUserChild_CheckIsAdmin();
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public int SYSUserChild_CheckData(dynamic dynParam)
        {
            try
            {
                int id = (int)dynParam.id;
                string username = (string)dynParam.username;
                string email = (string)dynParam.email;
                var result = -1;
                ServiceFactory.SVSystem((ISVSystem sv) =>
                {
                    result = sv.SYSUserChild_CheckData(id, username, email);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public int SYSUserChild_Save(dynamic dynParam)
        {
            try
            {
                SYSUser item = Newtonsoft.Json.JsonConvert.DeserializeObject<SYSUser>(dynParam.item.ToString());
                var user = Membership.GetUser(item.UserName);
                if (user == null || user.UserName == "")
                {
                    var password = item.Password;
                    if (password == "" || password == null)
                        password = System.Configuration.ConfigurationManager.AppSettings["password"];
                    user = Membership.CreateUser(item.UserName, password, item.Email);
                    user.IsApproved = true;
                    Membership.UpdateUser(user);
                }
                else
                {
                    if (item.IsApproved && user.IsLockedOut)
                        user.UnlockUser();
                    if (item.Password != "" && item.Password != null)
                    {
                        var password = user.ResetPassword();
                        user.ChangePassword(password, item.Password);
                    }
                    if (!user.IsApproved)
                    {
                        user.IsApproved = true;
                        Membership.UpdateUser(user);
                    }
                }
                var result = -1;
                ServiceFactory.SVSystem((ISVSystem sv) =>
                {
                    result = sv.SYSUserChild_Save(item);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void SYSUserChild_Delete(dynamic dynParam)
        {
            try
            {
                List<int> lstid = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.lstid.ToString());
                ServiceFactory.SVSystem((ISVSystem sv) =>
                {
                    sv.SYSUserChild_Delete(lstid);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public string SYSUserChild_Export()
        {
            try
            {
                var data = new SYSUser_Export { ListUser = new List<SYSUser>(), ListCustomer = new List<CUSCustomer>() };
                ServiceFactory.SVSystem((ISVSystem sv) =>
                {
                    data = sv.SYSUserChild_Export();
                });

                string filePath = "/" + FolderUpload.Export + "fileUserChild" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx";
                if (System.IO.File.Exists(HttpContext.Current.Server.MapPath(filePath)))
                    System.IO.File.Delete(HttpContext.Current.Server.MapPath(filePath));
                FileInfo file = new FileInfo(HttpContext.Current.Server.MapPath(filePath));
                using (ExcelPackage package = new ExcelPackage(file))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Sheet 1");
                    int col = 0, row = 1, stt = 0;

                    row = 1;
                    col = 1; worksheet.Cells[row, col].Value = "STT"; worksheet.Column(col).Width = 10;
                    col++; worksheet.Cells[row, col].Value = "Tài khoản"; worksheet.Column(col).Width = 15;
                    col++; worksheet.Cells[row, col].Value = "Mật khẩu"; worksheet.Column(col).Width = 20;
                    col++; worksheet.Cells[row, col].Value = "Họ"; worksheet.Column(col).Width = 20;
                    col++; worksheet.Cells[row, col].Value = "Tên"; worksheet.Column(col).Width = 35;
                    col++; worksheet.Cells[row, col].Value = "Email"; worksheet.Column(col).Width = 20;
                    col++; worksheet.Cells[row, col].Value = "SĐT"; worksheet.Column(col).Width = 20;
                    col++; worksheet.Cells[row, col].Value = "Vai trò"; worksheet.Column(col).Width = 20;
                    col++; worksheet.Cells[row, col].Value = "Tên vai trò"; worksheet.Column(col).Width = 20;
                    col++; worksheet.Cells[row, col].Value = "Đang hoạt động"; worksheet.Column(col).Width = 10;
                    col++; worksheet.Cells[row, col].Value = "Danh sách khách hàng"; worksheet.Column(col).Width = 35;
                    col++; worksheet.Cells[row, col].Value = "Thuộc chi nhánh"; worksheet.Column(col).Width = 10;

                    row++;
                    stt = 1;
                    foreach (var item in data.ListUser)
                    {
                        col = 1; worksheet.Cells[row, col].Value = stt;
                        col++; worksheet.Cells[row, col].Value = item.UserName;
                        col++;
                        col++; worksheet.Cells[row, col].Value = item.LastName;
                        col++; worksheet.Cells[row, col].Value = item.FirstName;
                        col++; worksheet.Cells[row, col].Value = item.Email;
                        col++; worksheet.Cells[row, col].Value = item.TelNo;
                        col++; worksheet.Cells[row, col].Value = item.GroupCode;
                        col++; worksheet.Cells[row, col].Value = item.GroupName;
                        col++; worksheet.Cells[row, col].Value = item.IsApproved == true ? "x" : "";
                        col++;
                        var lstCustomerCode = "";
                        if (!string.IsNullOrEmpty(item.ListCustomerID))
                        {
                            var strs = item.ListCustomerID.Split(',');
                            foreach (var strid in strs)
                            {
                                var obj = data.ListCustomer.FirstOrDefault(c => c.ID == Convert.ToInt32(strid));
                                if (obj != null)
                                    lstCustomerCode += "," + obj.Code;
                            }
                            if (lstCustomerCode != "")
                                lstCustomerCode = lstCustomerCode.Substring(1);
                        }
                        worksheet.Cells[row, col].Value = lstCustomerCode;
                        col++;
                        if (item.SYSCustomerID > 0)
                        {
                            var cus = data.ListSYSCustomer.FirstOrDefault(c => c.ID == item.SYSCustomerID.Value);
                            if (cus != null)
                                worksheet.Cells[row, col].Value = cus.Code;
                        }

                        row++;
                        stt++;
                    }

                    package.Save();
                    return filePath;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public List<SYSUser_Import> SYSUserChild_ImportCheck(dynamic dynParam)
        {
            try
            {
                CATFile file = Newtonsoft.Json.JsonConvert.DeserializeObject<CATFile>(dynParam.file.ToString());
                var result = new List<SYSUser_Import>();

                var data = new SYSUser_ImportCheck() { ListUser = new List<SYSUser>(), ListGroup = new List<SYSGroup>() };
                ServiceFactory.SVSystem((ISVSystem sv) =>
                {
                    data = sv.SYSUserChild_ImportCheck();
                });

                using (System.IO.FileStream fs = new System.IO.FileStream(HttpContext.Current.Server.MapPath("/" + file.FilePath), System.IO.FileMode.Open, System.IO.FileAccess.Read))
                {
                    using (var package = new ExcelPackage(fs))
                    {
                        ExcelWorksheet worksheet = ExcelHelper.GetWorksheetByIndex(package, 1);
                        if (worksheet != null)
                        {
                            int col = 0, row = 1;

                            for (row = 2; row <= worksheet.Dimension.End.Row; row++)
                            {
                                col = 1; var strRow = ExcelHelper.GetValue(worksheet, row, col);
                                col++; var strUserName = ExcelHelper.GetValue(worksheet, row, col);
                                col++;
                                col++; var strLastName = ExcelHelper.GetValue(worksheet, row, col);
                                col++; var strFirstName = ExcelHelper.GetValue(worksheet, row, col);
                                col++; var strEmail = ExcelHelper.GetValue(worksheet, row, col);
                                col++; var strSDT = ExcelHelper.GetValue(worksheet, row, col);
                                col++; var strGroupCode = ExcelHelper.GetValue(worksheet, row, col);
                                col++;
                                col++; var strIsApproved = ExcelHelper.GetValue(worksheet, row, col);
                                col++; var strListCustomerCode = ExcelHelper.GetValue(worksheet, row, col);
                                col++; var strSYSCustomerCode = ExcelHelper.GetValue(worksheet, row, col);

                                if (string.IsNullOrEmpty(strRow))
                                    break;

                                int userid = -1;
                                try
                                {
                                    userid = data.ListUser.FirstOrDefault(c => c.UserName == strUserName).ID;
                                }
                                catch { }
                                bool isemail = true;
                                isemail = data.ListUser.Where(c => c.ID != userid && c.Email == strEmail).Count() == 0;
                                if (isemail == true)
                                    isemail = StringHelper.IsEmail(strEmail);
                                int groupid = -1;
                                try
                                {
                                    groupid = data.ListGroup.FirstOrDefault(c => c.Code == strGroupCode).ID;
                                }
                                catch { }
                                bool isapproved = false;
                                try
                                {
                                    isapproved = Convert.ToBoolean(strIsApproved);
                                }
                                catch { }
                                var strCustomerID = "";
                                foreach (var str in strListCustomerCode.Split(','))
                                {
                                    var obj = data.ListCustomer.FirstOrDefault(c => c.Code == str);
                                    if (obj != null)
                                        strCustomerID += "," + obj.ID;
                                }
                                if (strCustomerID != "")
                                    strCustomerID = strCustomerID.Substring(1);
                                var sysCustomerID = -1;
                                try
                                {
                                    sysCustomerID = data.ListSYSCustomer.FirstOrDefault(c => c.Code == strSYSCustomerCode).ID;
                                }
                                catch { }

                                var item = new SYSUser_Import();
                                item.ID = userid;
                                item.ExcelRow = row;
                                item.UserName = strUserName;
                                item.LastName = strLastName;
                                item.FirstName = strFirstName;
                                item.Email = strEmail;
                                item.TelNo = strSDT;
                                item.GroupID = groupid;
                                item.IsApproved = isapproved;
                                item.ListCustomerID = strCustomerID;
                                item.ListCustomerCode = strListCustomerCode;
                                item.SYSCustomerCode = strSYSCustomerCode;
                                item.SYSCustomerID = sysCustomerID;
                                if (isemail)
                                {
                                    item.ExcelSuccess = true;
                                    item.ExcelError = "";
                                }
                                else
                                {
                                    item.ExcelSuccess = false;
                                    if (!isemail)
                                        item.ExcelError = "Email đã sử dụng hoặc nhập sai";
                                }
                                result.Add(item);
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
        public void SYSUserChild_Import(dynamic dynParam)
        {
            try
            {
                List<SYSUser_Import> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<SYSUser_Import>>(dynParam.lst.ToString());
                ServiceFactory.SVSystem((ISVSystem sv) =>
                {
                    sv.SYSUserChild_Import(lst);
                });

                foreach (var item in lst.Where(c => c.ExcelSuccess))
                {
                    var user = Membership.GetUser(item.UserName);
                    if (user == null || user.UserName == "")
                    {
                        var password = item.Password;
                        if (password == "" || password == null)
                            password = System.Configuration.ConfigurationManager.AppSettings["password"];
                        user = Membership.CreateUser(item.UserName, password, item.Email);
                        user.IsApproved = true;
                        Membership.UpdateUser(user);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public List<DTOCombobox> SYSUserChild_Driver()
        {
            try
            {
                var result = new List<DTOCombobox>();
                ServiceFactory.SVSystem((ISVSystem sv) =>
                {
                    result = sv.SYSUserChild_Driver();
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        private string GetAddress()
        {
            string result = string.Empty;
            try
            {
                //result = sessionID + DateTime.Now.ToString("ddMMyyyyHHmm");
                System.Web.HttpContext context = System.Web.HttpContext.Current;
                if (context != null)
                {
                    //context.Request.us
                    if (context.Request.ServerVariables.AllKeys.Contains("REMOTE_ADDR"))
                        result += context.Request.ServerVariables["REMOTE_ADDR"];
                }
            }
            catch { }
            return result;
        }
    }
}