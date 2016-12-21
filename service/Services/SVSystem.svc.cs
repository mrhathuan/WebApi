using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using Kendo.Mvc.Extensions;
using DTO;
using IServices;
using Business;

namespace Services
{
    public partial class SVSystem : SVBase, ISVSystem
    {
        #region App
        public void App_Init()
        {
            try
            {
                using (var bl = CreateBusiness<BLSystem>())
                {
                    bl.App_Init();
                }
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

        public DTOAuthorization App_User()
        {
            try
            {
                using (var bl = CreateBusiness<BLSystem>())
                {
                    return bl.App_User();
                }
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

        public List<SYSFunction> App_ListFunction(int parentid)
        {
            try
            {
                using (var bl = CreateBusiness<BLSystem>())
                {
                    return bl.App_ListFunction(parentid);
                }
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

        public List<SYSResource> App_ListResource()
        {
            try
            {
                using (var bl = CreateBusiness<BLSystem>())
                {
                    return bl.App_ListResource();
                }
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

        public List<SYSResource> App_ListResourceEmpty()
        {
            try
            {
                using (var bl = CreateBusiness<BLSystem>())
                {
                    return bl.App_ListResourceEmpty();
                }
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
        public void App_UserOptionsSetting_Save(int referID, string referKey, Dictionary<string, string> options)
        {
            try
            {
                using (var bl = CreateBusiness<BLSystem>())
                {
                    bl.App_UserOptionsSetting_Save(referID, referKey, options);
                }
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

        public void App_UserGridSetting_Save(int referID, string referKey, SYSUserSettingFunction_Grid item)
        {
            try
            {
                using (var bl = CreateBusiness<BLSystem>())
                {
                    bl.App_UserGridSetting_Save(referID, referKey, item);
                }
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
        
        public void App_UserGridSetting_Delete(int referID, string referKey, SYSUserSettingFunction_Grid item)
        {
            try
            {
                using (var bl = CreateBusiness<BLSystem>())
                {
                    bl.App_UserGridSetting_Delete(referID, referKey, item);
                }
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

        public DTOResult App_FileList(string request, CATTypeOfFileCode code, int id)
        {
            try
            {
                using (var bl = CreateBusiness<BLSystem>())
                {
                    return bl.App_FileList(request, code, id);
                }
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

        public int App_FileSave(CATFile item)
        {
            try
            {
                using (var bl = CreateBusiness<BLSystem>())
                {
                    return bl.App_FileSave(item);
                }
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

        public void App_FileDelete(List<int> lstid)
        {
            try
            {
                using (var bl = CreateBusiness<BLSystem>())
                {
                    bl.App_FileDelete(lstid);
                }
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



        public List<CATComment> App_CommentList(CATTypeOfCommentCode code, int referid)
        {
            try
            {
                using (var bl = CreateBusiness<BLSystem>())
                {
                    return bl.App_CommentList(code, referid);
                }
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

        public long App_CommentSave(CATComment item)
        {
            try
            {
                using (var bl = CreateBusiness<BLSystem>())
                {
                    return bl.App_CommentSave(item);
                }
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



        public SYSUserSettingFunction App_UserSettingGet(SYSUserSettingFunction item)
        {
            try
            {
                using (var bl = CreateBusiness<BLSystem>())
                {
                    return bl.App_UserSettingGet(item);
                }
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

        public void App_UserSettingSave(SYSUserSettingFunction item)
        {
            try
            {
                using (var bl = CreateBusiness<BLSystem>())
                {
                    bl.App_UserSettingSave(item);
                }
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

        #region Action
        public DTOResult SYSAction_List(DTORequest request)
        {
            try
            {
                using (var bl = CreateBusiness<BLSystem>())
                {
                    return bl.SYSAction_List(request.ToKendo());
                }
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

        public int SYSAction_Save(SYSAction item)
        {
            try
            {
                using (var bl = CreateBusiness<BLSystem>())
                {
                    return bl.SYSAction_Save(item);
                }
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

        public void SYSAction_Delete(SYSAction item)
        {
            try
            {
                using (var bl = CreateBusiness<BLSystem>())
                {
                    bl.SYSAction_Delete(item);
                }
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

        #region Group
        public DTOResult SYSGroup_Read(string request)
        {
            try
            {
                using (var bl = CreateBusiness<BLSystem>())
                {
                    return bl.SYSGroup_Read(request);
                }
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


        public SYSGroup SYSGroup_Item(int id)
        {
            try
            {
                using (var bl = CreateBusiness<BLSystem>())
                {
                    return bl.SYSGroup_Item(id);
                }
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

        public int SYSGroup_Save(SYSGroup item)
        {
            try
            {
                using (var bl = CreateBusiness<BLSystem>())
                {
                    return bl.SYSGroup_Save(item);
                }
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

        public void SYSGroup_Delete(List<int> lstid)
        {
            try
            {
                using (var bl = CreateBusiness<BLSystem>())
                {
                    bl.SYSGroup_Delete(lstid);
                }
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

        #region GroupChild
        public DTOResult SYSGroupChild_Read(string request)
        {
            try
            {
                using (var bl = CreateBusiness<BLSystem>())
                {
                    return bl.SYSGroupChild_Read(request);
                }
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

        public List<DTOCombobox> SYSGroupChild_Parent(int id)
        {
            try
            {
                using (var bl = CreateBusiness<BLSystem>())
                {
                    return bl.SYSGroupChild_Parent(id);
                }
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

        public SYSGroup SYSGroupChild_Item(int id)
        {
            try
            {
                using (var bl = CreateBusiness<BLSystem>())
                {
                    return bl.SYSGroupChild_Item(id);
                }
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

        public int SYSGroupChild_Save(SYSGroup item)
        {
            try
            {
                using (var bl = CreateBusiness<BLSystem>())
                {
                    return bl.SYSGroupChild_Save(item);
                }
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

        public void SYSGroupChild_Delete(List<int> lstid)
        {
            try
            {
                using (var bl = CreateBusiness<BLSystem>())
                {
                    bl.SYSGroupChild_Delete(lstid);
                }
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

        #region Function
        public DTOResult SYSFunction_Read(string request)
        {
            try
            {
                using (var bl = CreateBusiness<BLSystem>())
                {
                    return bl.SYSFunction_Read(request);
                }
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

        public List<DTOCombobox> SYSFunction_Parent(int id)
        {
            try
            {
                using (var bl = CreateBusiness<BLSystem>())
                {
                    return bl.SYSFunction_Parent(id);
                }
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

        public SYSFunction SYSFunction_Item(int id)
        {
            try
            {
                using (var bl = CreateBusiness<BLSystem>())
                {
                    return bl.SYSFunction_Item(id);
                }
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

        public void SYSFunction_Move(int id, int typeid)
        {
            try
            {
                using (var bl = CreateBusiness<BLSystem>())
                {
                    bl.SYSFunction_Move(id, typeid);
                }
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

        public int SYSFunction_Save(SYSFunction item)
        {
            try
            {
                using (var bl = CreateBusiness<BLSystem>())
                {
                    return bl.SYSFunction_Save(item);
                }
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

        public void SYSFunction_Delete(List<int> lstid)
        {
            try
            {
                using (var bl = CreateBusiness<BLSystem>())
                {
                    bl.SYSFunction_Delete(lstid);
                }
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

        public void SYSFunction_Refresh()
        {
            try
            {
                using (var bl = CreateBusiness<BLSystem>())
                {
                    bl.SYSFunction_Refresh();
                }
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

        public SYSFunction_Export SYSFunction_Export()
        {
            try
            {
                using (var bl = CreateBusiness<BLSystem>())
                {
                    return bl.SYSFunction_Export();
                }
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

        public SYSFunction_ImportCheck SYSFunction_ImportCheck()
        {
            try
            {
                using (var bl = CreateBusiness<BLSystem>())
                {
                    return bl.SYSFunction_ImportCheck();
                }
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

        public void SYSFunction_ExcelImport(List<SYSFunction_Import> lst)
        {
            try
            {
                using (var bl = CreateBusiness<BLSystem>())
                {
                    bl.SYSFunction_ExcelImport(lst);
                }
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

        #region Config
        public DTOResult SYSConfigGroup_Read(string request)
        {
            try
            {
                using (var bl = CreateBusiness<BLSystem>())
                {
                    return bl.SYSConfigGroup_Read(request);
                }
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

        public SYSGroup SYSConfigGroup_GroupItem(int groupid)
        {
            try
            {
                using (var bl = CreateBusiness<BLSystem>())
                {
                    return bl.SYSConfigGroup_GroupItem(groupid);
                }
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

        public DTOResult SYSConfigFunction_InRead(string request, int groupid)
        {
            try
            {
                using (var bl = CreateBusiness<BLSystem>())
                {
                    return bl.SYSConfigFunction_InRead(request, groupid);
                }
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

        public DTOResult SYSConfigFunction_NotInRead(string request, int groupid)
        {
            try
            {
                using (var bl = CreateBusiness<BLSystem>())
                {
                    return bl.SYSConfigFunction_NotInRead(request, groupid);
                }
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

        public void SYSConfigFunction_AddFunction(List<SYSFunction> lst, int groupid)
        {
            try
            {
                using (var bl = CreateBusiness<BLSystem>())
                {
                    bl.SYSConfigFunction_AddFunction(lst, groupid);
                }
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

        public void SYSConfigFunction_DelFunction(List<SYSFunction> lst, int groupid)
        {
            try
            {
                using (var bl = CreateBusiness<BLSystem>())
                {
                    bl.SYSConfigFunction_DelFunction(lst, groupid);
                }
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

        public SYSFunction SYSConfigFunction_GetItem(int groupid, int functionid)
        {
            try
            {
                using (var bl = CreateBusiness<BLSystem>())
                {
                    return bl.SYSConfigFunction_GetItem(groupid, functionid);
                }
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

        public void SYSConfigFunction_SaveItem(int groupid, int functionid, SYSFunction item)
        {
            try
            {
                using (var bl = CreateBusiness<BLSystem>())
                {
                    bl.SYSConfigFunction_SaveItem(groupid, functionid, item);
                }
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



        public List<SYSFunction> SYSConfigFunctionNotInGroup_List(int groupid)
        {
            try
            {
                using (var bl = CreateBusiness<BLSystem>())
                {
                    return bl.SYSConfigFunctionNotInGroup_List(groupid);
                }
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

        public List<SYSFunction> SYSConfigFunctionInGroup_List(int groupid)
        {
            try
            {
                using (var bl = CreateBusiness<BLSystem>())
                {
                    return bl.SYSConfigFunctionInGroup_List(groupid);
                }
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

        public void SYSConfigFunction_Save(List<int> lstid, int groupid)
        {
            try
            {
                using (var bl = CreateBusiness<BLSystem>())
                {
                    bl.SYSConfigFunction_Save(lstid, groupid);
                }
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

        public void SYSConfigFunction_Delete(List<int> lstid, int groupid)
        {
            try
            {
                using (var bl = CreateBusiness<BLSystem>())
                {
                    bl.SYSConfigFunction_Delete(lstid, groupid);
                }
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

        public List<SYSAction> SYSConfigAction_Get(int groupid, int functionid)
        {
            try
            {
                using (var bl = CreateBusiness<BLSystem>())
                {
                    return bl.SYSConfigAction_Get(groupid, functionid);
                }
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

        public void SYSConfigAction_Save(int groupid, int functionid, List<SYSAction> lst)
        {
            try
            {
                using (var bl = CreateBusiness<BLSystem>())
                {
                    bl.SYSConfigAction_Save(groupid, functionid, lst);
                }
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

        public List<SYSFunction> SYSConfig_ExcelFuntion()
        {
            try
            {
                using (var bl = CreateBusiness<BLSystem>())
                {
                    return bl.SYSConfig_ExcelFuntion();
                }
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

        public List<SYSAction> SYSConfig_ExcelAction()
        {
            try
            {
                using (var bl = CreateBusiness<BLSystem>())
                {
                    return bl.SYSConfig_ExcelAction();
                }
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

        public List<DTOSYSFunctionInGroup> SYSConfig_ExcelFunctionInGroup(int groupid)
        {
            try
            {
                using (var bl = CreateBusiness<BLSystem>())
                {
                    return bl.SYSConfig_ExcelFunctionInGroup(groupid);
                }
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

        #region User
        public DTOResult SYSUser_Read(string request)
        {
            try
            {
                using (var bl = CreateBusiness<BLSystem>())
                {
                    return bl.SYSUser_Read(request);
                }
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

        public SYSUser SYSUser_Get(int id)
        {
            try
            {
                using (var bl = CreateBusiness<BLSystem>())
                {
                    return bl.SYSUser_Get(id);
                }
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

        public List<DTOCombobox> SYSUser_Group()
        {
            try
            {
                using (var bl = CreateBusiness<BLSystem>())
                {
                    return bl.SYSUser_Group();
                }
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

        public List<DTOCombobox> SYSUser_Customer()
        {
            try
            {
                using (var bl = CreateBusiness<BLSystem>())
                {
                    return bl.SYSUser_Customer();
                }
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

        public bool SYSUser_CheckUserName(int id, string username)
        {
            try
            {
                using (var bl = CreateBusiness<BLSystem>())
                {
                    return bl.SYSUser_CheckUserName(id, username);
                }
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

        public bool SYSUser_CheckIsAdmin()
        {
            try
            {
                using (var bl = CreateBusiness<BLSystem>())
                {
                    return bl.SYSUser_CheckIsAdmin();
                }
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

        public int SYSUser_CheckData(int id, string username, string email)
        {
            try
            {
                using (var bl = CreateBusiness<BLSystem>())
                {
                    return bl.SYSUser_CheckData(id, username, email);
                }
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

        public int SYSUser_Save(SYSUser item)
        {
            try
            {
                using (var bl = CreateBusiness<BLSystem>())
                {
                    return bl.SYSUser_Save(item);
                }
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

        public void SYSUser_Delete(List<int> lstid)
        {
            try
            {
                using (var bl = CreateBusiness<BLSystem>())
                {
                    bl.SYSUser_Delete(lstid);
                }
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

        public SYSUser_Export SYSUser_Export()
        {
            try
            {
                using (var bl = CreateBusiness<BLSystem>())
                {
                    return bl.SYSUser_Export();
                }
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

        public SYSUser_ImportCheck SYSUser_ImportCheck()
        {
            try
            {
                using (var bl = CreateBusiness<BLSystem>())
                {
                    return bl.SYSUser_ImportCheck();
                }
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

        public void SYSUser_Import(List<SYSUser_Import> lst)
        {
            try
            {
                using (var bl = CreateBusiness<BLSystem>())
                {
                    bl.SYSUser_Import(lst);
                }
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

        public List<DTOCombobox> SYSUser_Driver()
        {
            try
            {
                using (var bl = CreateBusiness<BLSystem>())
                {
                    return bl.SYSUser_Driver();
                }
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

        public DTOFLMDriver FLMDriver_Get(int driverID)
        {
            try
            {
                using (var bl = CreateBusiness<BLSystem>())
                {
                    return bl.FLMDriver_Get(driverID);
                }
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

        public DTOFLMTruck Truck_GetByDriver(int driverid)
        {
            try
            {
                using (var bl = CreateBusiness<BLSystem>())
                {
                    return bl.Truck_GetByDriver(driverid);
                }
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
                using (var bl = CreateBusiness<BLSystem>())
                {
                    return bl.SYSUserChild_Read(request);
                }
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

        public SYSUser SYSUserChild_Get(int id)
        {
            try
            {
                using (var bl = CreateBusiness<BLSystem>())
                {
                    return bl.SYSUserChild_Get(id);
                }
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

        public List<DTOCombobox> SYSUserChild_Group()
        {
            try
            {
                using (var bl = CreateBusiness<BLSystem>())
                {
                    return bl.SYSUserChild_Group();
                }
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

        public List<DTOCombobox> SYSUserChild_Customer()
        {
            try
            {
                using (var bl = CreateBusiness<BLSystem>())
                {
                    return bl.SYSUserChild_Customer();
                }
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

        public bool SYSUserChild_CheckUserName(int id, string username)
        {
            try
            {
                using (var bl = CreateBusiness<BLSystem>())
                {
                    return bl.SYSUserChild_CheckUserName(id, username);
                }
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

        public bool SYSUserChild_CheckIsAdmin()
        {
            try
            {
                using (var bl = CreateBusiness<BLSystem>())
                {
                    return bl.SYSUserChild_CheckIsAdmin();
                }
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

        public int SYSUserChild_CheckData(int id, string username, string email)
        {
            try
            {
                using (var bl = CreateBusiness<BLSystem>())
                {
                    return bl.SYSUserChild_CheckData(id, username, email);
                }
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

        public int SYSUserChild_Save(SYSUser item)
        {
            try
            {
                using (var bl = CreateBusiness<BLSystem>())
                {
                    return bl.SYSUserChild_Save(item);
                }
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

        public void SYSUserChild_Delete(List<int> lstid)
        {
            try
            {
                using (var bl = CreateBusiness<BLSystem>())
                {
                    bl.SYSUserChild_Delete(lstid);
                }
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

        public SYSUser_Export SYSUserChild_Export()
        {
            try
            {
                using (var bl = CreateBusiness<BLSystem>())
                {
                    return bl.SYSUserChild_Export();
                }
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

        public SYSUser_ImportCheck SYSUserChild_ImportCheck()
        {
            try
            {
                using (var bl = CreateBusiness<BLSystem>())
                {
                    return bl.SYSUserChild_ImportCheck();
                }
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

        public void SYSUserChild_Import(List<SYSUser_Import> lst)
        {
            try
            {
                using (var bl = CreateBusiness<BLSystem>())
                {
                    bl.SYSUserChild_Import(lst);
                }
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

        public List<DTOCombobox> SYSUserChild_Driver()
        {
            try
            {
                using (var bl = CreateBusiness<BLSystem>())
                {
                    return bl.SYSUserChild_Driver();
                }
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

        public DTOFLMDriver FLMDriverChild_Get(int driverID)
        {
            try
            {
                using (var bl = CreateBusiness<BLSystem>())
                {
                    return bl.FLMDriverChild_Get(driverID);
                }
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

        public DTOFLMTruck TruckChild_GetByDriver(int driverid)
        {
            try
            {
                using (var bl = CreateBusiness<BLSystem>())
                {
                    return bl.TruckChild_GetByDriver(driverid);
                }
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

        //public DTOResult SYSCustomerSYS(DTORequest request)
        //{
        //    try
        //    {
        //        using (var bl = CreateBusiness<BLSystem>())
        //        {
        //            return bl.SYSCustomerSYS(request.ToKendo());
        //        }
        //    }
        //    catch (FaultException<DTOError> ex)
        //    {
        //        throw ex;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw FaultHelper.ServiceFault(ex);
        //    }
        //}

        //public DTOResult SYSCustomerAll(DTORequest request)
        //{
        //    try
        //    {
        //        using (var bl = CreateBusiness<BLSystem>())
        //        {
        //            return bl.SYSCustomerAll(request.ToKendo());
        //        }
        //    }
        //    catch (FaultException<DTOError> ex)
        //    {
        //        throw ex;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw FaultHelper.ServiceFault(ex);
        //    }
        //}

        #region Profile

        public DTOSYSUser UserProfile_Get(int? userID)
        {
            try
            {
                using (var bl = CreateBusiness<BLSystem>())
                {
                    return bl.UserProfile_Get(userID);
                }
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

        public DTOResult UserProfileComment_List(DTORequest request, int? userID)
        {
            try
            {
                using (var bl = CreateBusiness<BLSystem>())
                {
                    return bl.UserProfileComment_List(request.ToKendo(), userID);
                }
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

        #region Profile

        public DTOUserProfile UserProfile_GetUser()
        {
            try
            {
                using (var bl = CreateBusiness<BLSystem>())
                {
                    return bl.UserProfile_GetUser();
                }
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

        public void UserProfile_Save(DTOUserProfile item)
        {
            try
            {
                using (var bl = CreateBusiness<BLSystem>())
                {
                    bl.UserProfile_Save(item);
                }
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

        #region Mail Template
        public DTOResult MailTemplate_List(string request)
        {
            try
            {
                using (var bl = CreateBusiness<BLSystem>())
                {
                    return bl.MailTemplate_List(request);
                }
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

        public void MailTemplate_Save(CATMailTemplate item)
        {
            try
            {
                using (var bl = CreateBusiness<BLSystem>())
                {
                    bl.MailTemplate_Save(item);
                }
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

        public void MailTemplate_Delete(CATMailTemplate item)
        {
            try
            {
                using (var bl = CreateBusiness<BLSystem>())
                {
                    bl.MailTemplate_Delete(item);
                }
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

        public CATMailTemplate MailTemplate_Get(int id)
        {
            try
            {
                using (var bl = CreateBusiness<BLSystem>())
                {
                    return bl.MailTemplate_Get(id);
                }
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

        public CATMailTemplate MailTemplate_GetBySYSCustomerID(int syscustomerID)
        {
            try
            {
                using (var bl = CreateBusiness<BLSystem>())
                {
                    return bl.MailTemplate_GetBySYSCustomerID(syscustomerID);
                }
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

        public CATMailTemplate MailTemplate_GetByCurrentSYSCustomerID()
        {
            try
            {
                using (var bl = CreateBusiness<BLSystem>())
                {
                    return bl.MailTemplate_GetByCurrentSYSCustomerID();
                }
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

        #region SYSResource
        public DTOResult SYSResource_List(string request, int Language)
        {
            try
            {
                using (var bl = CreateBusiness<BLSystem>())
                {
                    return bl.SYSResource_List(request, Language);
                }
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

        public void SYSResource_Save(DTOSYSResource item, int Language)
        {
            try
            {
                using (var bl = CreateBusiness<BLSystem>())
                {
                    bl.SYSResource_Save(item, Language);
                }
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

        public void SYSResource_Delete(DTOSYSResource item, int Language)
        {
            try
            {
                using (var bl = CreateBusiness<BLSystem>())
                {
                    bl.SYSResource_Delete(item, Language);
                }
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

        public DTOSYSResource SYSResource_Get(int id, int Language)
        {
            try
            {
                using (var bl = CreateBusiness<BLSystem>())
                {
                    return bl.SYSResource_Get(id, Language);
                }
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

        public List<DTOSYSResource> SYSResource_Data(int Language)
        {
            try
            {
                using (var bl = CreateBusiness<BLSystem>())
                {
                    return bl.SYSResource_Data(Language);
                }
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
        public void SYSResource_Import(List<DTOSYSResourceImport> lst, int Language)
        {
            try
            {
                using (var bl = CreateBusiness<BLSystem>())
                {
                    bl.SYSResource_Import(lst, Language);
                }
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
        public void SYSResource_ChangeDefault(int Language)
        {
            try
            {
                using (var bl = CreateBusiness<BLSystem>())
                {
                    bl.SYSResource_ChangeDefault(Language);
                }
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

        #region SYSUserResource
        public DTOResult SYSUserResource_List(string request)
        {
            try
            {
                using (var bl = CreateBusiness<BLSystem>())
                {
                    return bl.SYSUserResource_List(request);
                }
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

        public void SYSUserResource_Save(DTOSYSUserResource item)
        {
            try
            {
                using (var bl = CreateBusiness<BLSystem>())
                {
                    bl.SYSUserResource_Save(item);
                }
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

        public DTOSYSUserResource SYSUserResource_Get(int id)
        {
            try
            {
                using (var bl = CreateBusiness<BLSystem>())
                {
                    return bl.SYSUserResource_Get(id);
                }
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

        public List<DTOSYSUserResource> SYSUserResource_Data()
        {
            try
            {
                using (var bl = CreateBusiness<BLSystem>())
                {
                    return bl.SYSUserResource_Data();
                }
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
        public void SYSUserResource_Import(List<DTOSYSUserResourceImport> lst)
        {
            try
            {
                using (var bl = CreateBusiness<BLSystem>())
                {
                    bl.SYSUserResource_Import(lst);
                }
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
        public void SYSUserResource_SetDefault()
        {
            try
            {
                using (var bl = CreateBusiness<BLSystem>())
                {
                    bl.SYSUserResource_SetDefault();
                }
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

        public void SYSUserResource_SetDefaultByUser(int userID)
        {
            try
            {
                using (var bl = CreateBusiness<BLSystem>())
                {
                    bl.SYSUserResource_SetDefaultByUser(userID);
                }
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

        #region Dashboard
        public List<MAP_Vehicle> Dashboard_Truck_List()
        {
            try
            {
                using (var bl = CreateBusiness<BLSystem>())
                {
                    return bl.Dashboard_Truck_List();
                }
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

        public List<MAP_Vehicle> Dashboard_Tractor_List()
        {
            try
            {
                using (var bl = CreateBusiness<BLSystem>())
                {
                    return bl.Dashboard_Tractor_List();
                }
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


        public Dashboard_UserSetting Dashboard_UserSetting_Get(int functionID)
        {
            try
            {
                using (var bl = CreateBusiness<BLSystem>())
                {
                    return bl.Dashboard_UserSetting_Get(functionID);
                }
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

        public void Dashboard_UserSetting_Save(Dashboard_UserSetting item)
        {
            try
            {
                using (var bl = CreateBusiness<BLSystem>())
                {
                    bl.Dashboard_UserSetting_Save(item);
                }
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



        public Chart_Summary Chart_Summary_Data(DateTime dtfrom, DateTime dtto, int? customerid)
        {
            try
            {
                using (var bl = CreateBusiness<BLSystem>())
                {
                    return bl.Chart_Summary_Data(dtfrom, dtto, customerid);
                }
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

        public Chart_Customer_Order Chart_Customer_Order_Data(DateTime dtfrom, DateTime dtto, int? customerid)
        {
            try
            {
                using (var bl = CreateBusiness<BLSystem>())
                {
                    return bl.Chart_Customer_Order_Data(dtfrom, dtto, customerid);
                }
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

        public Chart_Customer_OPS Chart_Customer_OPS_Data(DateTime dtfrom, DateTime dtto, int? customerid, int statusid)
        {
            try
            {
                using (var bl = CreateBusiness<BLSystem>())
                {
                    return bl.Chart_Customer_OPS_Data(dtfrom, dtto, customerid, statusid);
                }
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

        public Chart_Customer_Order_Rate Chart_Customer_Order_Rate_Data(DateTime dtfrom, DateTime dtto, int quantity)
        {
            try
            {
                using (var bl = CreateBusiness<BLSystem>())
                {
                    return bl.Chart_Customer_Order_Rate_Data(dtfrom, dtto, quantity);
                }
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

        public Chart_Owner_Capacity Chart_Owner_Capacity_Data(DateTime dtfrom, DateTime dtto)
        {
            try
            {
                using (var bl = CreateBusiness<BLSystem>())
                {
                    return bl.Chart_Owner_Capacity_Data(dtfrom, dtto);
                }
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

        public Chart_Owner_KM Chart_Owner_KM_Data(DateTime dtfrom, DateTime dtto)
        {
            try
            {
                using (var bl = CreateBusiness<BLSystem>())
                {
                    return bl.Chart_Owner_KM_Data(dtfrom, dtto);
                }
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

        public Chart_Owner_Operation Chart_Owner_Operation_Data(DateTime dtfrom, DateTime dtto)
        {
            try
            {
                using (var bl = CreateBusiness<BLSystem>())
                {
                    return bl.Chart_Owner_Operation_Data(dtfrom, dtto);
                }
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

        public Chart_Owner_CostRate Chart_Owner_CostRate_Data(DateTime dtfrom, DateTime dtto)
        {
            try
            {
                using (var bl = CreateBusiness<BLSystem>())
                {
                    return bl.Chart_Owner_CostRate_Data(dtfrom, dtto);
                }
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

        public Chart_Owner_CostChange Chart_Owner_CostChange_Data(DateTime dtfrom, DateTime dtto)
        {
            try
            {
                using (var bl = CreateBusiness<BLSystem>())
                {
                    return bl.Chart_Owner_CostChange_Data(dtfrom, dtto);
                }
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

        public Chart_Owner_OnTime Chart_Owner_OnTime_PickUp_Data(DateTime dtfrom, DateTime dtto, int? customerid)
        {
            try
            {
                using (var bl = CreateBusiness<BLSystem>())
                {
                    return bl.Chart_Owner_OnTime_PickUp_Data(dtfrom, dtto, customerid);
                }
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

        public Chart_Owner_OnTime Chart_Owner_OnTime_Delivery_Data(DateTime dtfrom, DateTime dtto, int? customerid)
        {
            try
            {
                using (var bl = CreateBusiness<BLSystem>())
                {
                    return bl.Chart_Owner_OnTime_Delivery_Data(dtfrom, dtto, customerid);
                }
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

        public Chart_Owner_OnTime Chart_Owner_OnTime_POD_Data(DateTime dtfrom, DateTime dtto, int? customerid)
        {
            try
            {
                using (var bl = CreateBusiness<BLSystem>())
                {
                    return bl.Chart_Owner_OnTime_POD_Data(dtfrom, dtto, customerid);
                }
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

        public Chart_Owner_Return Chart_Owner_Return_Data(DateTime dtfrom, DateTime dtto, int? customerid)
        {
            try
            {
                using (var bl = CreateBusiness<BLSystem>())
                {
                    return bl.Chart_Owner_Return_Data(dtfrom, dtto, customerid);
                }
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

        public Chart_Owner_Profit Chart_Owner_Profit_Data(DateTime dtfrom, DateTime dtto, int? customerid)
        {
            try
            {
                using (var bl = CreateBusiness<BLSystem>())
                {
                    return bl.Chart_Owner_Profit_Data(dtfrom, dtto, customerid);
                }
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

        public Chart_Owner_Profit_Customer Chart_Owner_Profit_Customer_Data(DateTime dtfrom, DateTime dtto, int? customerid)
        {
            try
            {
                using (var bl = CreateBusiness<BLSystem>())
                {
                    return bl.Chart_Owner_Profit_Customer_Data(dtfrom, dtto, customerid);
                }
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

        public Chart_Owner_OnTime Chart_Owner_OnTime_PickUp_Radial_Data(DateTime dtfrom, DateTime dtto, int? customerid)
        {
            try
            {
                using (var bl = CreateBusiness<BLSystem>())
                {
                    return bl.Chart_Owner_OnTime_PickUp_Radial_Data(dtfrom, dtto, customerid);
                }
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

        public Chart_Owner_OnTime Chart_Owner_OnTime_Delivery_Radial_Data(DateTime dtfrom, DateTime dtto, int? customerid)
        {
            try
            {
                using (var bl = CreateBusiness<BLSystem>())
                {
                    return bl.Chart_Owner_OnTime_Delivery_Radial_Data(dtfrom, dtto, customerid);
                }
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

        public Chart_Owner_OnTime Chart_Owner_OnTime_POD_Radial_Data(DateTime dtfrom, DateTime dtto, int? customerid)
        {
            try
            {
                using (var bl = CreateBusiness<BLSystem>())
                {
                    return bl.Chart_Owner_OnTime_POD_Radial_Data(dtfrom, dtto, customerid);
                }
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

        public Chart_Owner_Profit_Customer Chart_Owner_Profit_Vendor_Data(DateTime dtfrom, DateTime dtto)
        {
            try
            {
                using (var bl = CreateBusiness<BLSystem>())
                {
                    return bl.Chart_Owner_Profit_Vendor_Data(dtfrom, dtto);
                }
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



        public Widget_Data Widget_Data(DateTime dtfrom, DateTime dtto, int typeofchart, int? customerid)
        {
            try
            {
                using (var bl = CreateBusiness<BLSystem>())
                {
                    return bl.Widget_Data(dtfrom, dtto, typeofchart, customerid);
                }
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


        public MAP_Summary MAP_Summary_Data(string request, List<int> lstCustomerID, DateTime dtfrom, DateTime dtto, int provinceID, int typeoflocationID)
        {
            try
            {
                using (var bl = CreateBusiness<BLSystem>())
                {
                    return bl.MAP_Summary_Data(request, lstCustomerID, dtfrom, dtto, provinceID, typeoflocationID);
                }
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

        public MAP_Summary MAP_Summary_Vehicle_Data(string request, DateTime dtfrom, DateTime dtto, int vehicleID)
        {
            try
            {
                using (var bl = CreateBusiness<BLSystem>())
                {
                    return bl.MAP_Summary_Vehicle_Data(request, dtfrom, dtto, vehicleID);
                }
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

        public MAP_Summary_Master MAP_Summary_Master_Data(int masterID)
        {
            try
            {
                using (var bl = CreateBusiness<BLSystem>())
                {
                    return bl.MAP_Summary_Master_Data(masterID);
                }
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

        public MAP_Summary_Master MAP_Summary_Master_DataList(List<int> lstMasterID)
        {
            try
            {
                using (var bl = CreateBusiness<BLSystem>())
                {
                    return bl.MAP_Summary_Master_DataList(lstMasterID);
                }
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


        public MAP_SummaryCO MAP_SummaryCO_Data(string request, List<int> lstCustomerID, DateTime dtfrom, DateTime dtto, int provinceID, int typeoflocationID)
        {
            try
            {
                using (var bl = CreateBusiness<BLSystem>())
                {
                    return bl.MAP_SummaryCO_Data(request, lstCustomerID, dtfrom, dtto, provinceID, typeoflocationID);
                }
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

        public MAP_SummaryCO MAP_SummaryCO_Vehicle_Data(string request, DateTime dtfrom, DateTime dtto, int vehicleID)
        {
            try
            {
                using (var bl = CreateBusiness<BLSystem>())
                {
                    return bl.MAP_SummaryCO_Vehicle_Data(request, dtfrom, dtto, vehicleID);
                }
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

        public MAP_SummaryCO_Master MAP_SummaryCO_Master_Data(int masterID)
        {
            try
            {
                using (var bl = CreateBusiness<BLSystem>())
                {
                    return bl.MAP_SummaryCO_Master_Data(masterID);
                }
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

        public MAP_SummaryCO_Master MAP_SummaryCO_Master_DataList(List<int> lstMasterID)
        {
            try
            {
                using (var bl = CreateBusiness<BLSystem>())
                {
                    return bl.MAP_SummaryCO_Master_DataList(lstMasterID);
                }
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

        #region Search
        public DTOResult SYSSearchDI(int type, string content, string request)
        {
            try
            {
                using (var bl = CreateBusiness<BLSystem>())
                {
                    return bl.SYSSearchDI(type, content, request);
                }
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

        public DTOResult SYSSearchCO(int type, string content, string request)
        {
            try
            {
                using (var bl = CreateBusiness<BLSystem>())
                {
                    return bl.SYSSearchCO(type, content, request);
                }
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

        #region Notification
        public DTOResult Notification_Read(string request)
        {
            try
            {
                using (var bl = CreateBusiness<BLSystem>())
                {
                    return bl.Notification_Read(request);
                }
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

        public DTOResult Notification_List(string request)
        {
            try
            {
                using (var bl = CreateBusiness<BLSystem>())
                {
                    return bl.Notification_List(request);
                }
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
    }
}

