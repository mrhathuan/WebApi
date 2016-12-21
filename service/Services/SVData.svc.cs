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

    #region Constructor

    public partial class SVData : SVBase, ISVData
    {

        public SVData()
        {

        }
    }

    #endregion


    #region CATContainer
    public partial class SVData : SVBase, ISVData
    {
        public DTOResult CATContainer_List(DTORequest request)
        {
            try
            {
                using (var bl = CreateBusiness<BLDataCATContainer>())
                {
                    return bl.List(request.ToKendo());
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

        public CATContainer CATContainer_Get(int id)
        {
            try
            {
                using (var bl = CreateBusiness<BLDataCATContainer>())
                {
                    return bl.Get(id);
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

        public int CATContainer_Save(CATContainer item)
        {
            try
            {
                using (var bl = CreateBusiness<BLDataCATContainer>())
                {
                    return bl.Save(item);
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

        public List<int> CATContainer_SaveList(List<CATContainer> lst)
        {
            try
            {
                using (var bl = CreateBusiness<BLDataCATContainer>())
                {
                    return bl.SaveList(lst);
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

        public void CATContainer_Delete(CATContainer item)
        {
            try
            {
                using (var bl = CreateBusiness<BLDataCATContainer>())
                {
                    bl.Delete(item);
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

        public void CATContainer_DeleteList(List<CATContainer> lst)
        {
            try
            {
                using (var bl = CreateBusiness<BLDataCATContainer>())
                {
                    bl.DeleteList(lst);
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
    }
    #endregion
}

