using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kendo.Mvc.Extensions;
using Data;
using DTO;
using System.ServiceModel;

namespace Business
{
    public class BLDataCATContainer : Base, IBLData<CATContainer>
    {
        public DTOResult List(Kendo.Mvc.UI.DataSourceRequest request)
        {
            try
            {
                DTOResult result = new DTOResult();
                using (var model = new DataEntities())
                {
                    var query = model.CAT_Container.Select(c => new CATContainer
                    {

                        ID = c.ID,
                        ContainerNo = c.ContainerNo,
                        PackageID = c.PackageID,
                        Length = c.Length,
                        Height = c.Height,
                        Width = c.Width,
                        Capacity = c.Capacity,
                        Description = c.Description
                    }).ToDataSourceResult(request);
                    result.Total = query.Total;
                    result.Data = query.Data as IEnumerable<CATContainer>;
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

        public CATContainer Get(int id)
        {
            try
            {
                CATContainer result = default(CATContainer);
                using (var model = new DataEntities())
                {
                    result = model.CAT_Container.Where(c => c.ID == id).Select(c => new CATContainer
                    {

                        ID = c.ID,
                        ContainerNo = c.ContainerNo,
                        PackageID = c.PackageID,
                        Length = c.Length,
                        Height = c.Height,
                        Width = c.Width,
                        Capacity = c.Capacity,
                        Description = c.Description
                    }).FirstOrDefault();
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

        public int Save(CATContainer item)
        {
            try
            {
                DTOError result = new DTOError();
                using (var model = new DataEntities())
                {
                    var obj = model.CAT_Container.FirstOrDefault(c => c.ID == item.ID);
                    if (obj == null)
                    {
                        obj = new CAT_Container();
                        obj.CreatedBy = Account.UserName;
                        obj.CreatedDate = DateTime.Now;
                    }
                    else
                    {
                        obj.ModifiedBy = Account.UserName;
                        obj.ModifiedDate = DateTime.Now;
                    }
                    obj.ContainerNo = item.ContainerNo;
                    obj.PackageID = item.PackageID;
                    obj.Length = item.Length;
                    obj.Height = item.Height;
                    obj.Width = item.Width;
                    obj.Capacity = item.Capacity;
                    obj.Description = item.Description;
                    if (obj.ID < 1)
                        model.CAT_Container.Add(obj);
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
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public List<int> SaveList(List<CATContainer> lst)
        {
            try
            {
                List<int> result = new List<int>();
                using (var model = new DataEntities())
                {
                    foreach (var item in lst)
                    {
                        DTOError itemerror = new DTOError();
                        var obj = model.CAT_Container.FirstOrDefault(c => c.ID == item.ID);
                        if (obj == null)
                        {
                            obj = new CAT_Container();
                            obj.CreatedBy = Account.UserName;
                            obj.CreatedDate = DateTime.Now;
                        }
                        else
                        {
                            obj.ModifiedBy = Account.UserName;
                            obj.ModifiedDate = DateTime.Now;
                        }
                        obj.ContainerNo = item.ContainerNo;
                        obj.PackageID = item.PackageID;
                        obj.Length = item.Length;
                        obj.Height = item.Height;
                        obj.Width = item.Width;
                        obj.Capacity = item.Capacity;
                        obj.Description = item.Description;
                        if (obj.ID < 1)
                            model.CAT_Container.Add(obj);
                        model.SaveChanges();
                        result.Add(obj.ID);
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

        public void Delete(CATContainer item)
        {
            try
            {
                DTOError result = new DTOError();
                using (var model = new DataEntities())
                {
                    var obj = model.CAT_Container.FirstOrDefault(c => c.ID == item.ID);
                    if (obj != null)
                    {
                        model.CAT_Container.Remove(obj);
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
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void DeleteList(List<CATContainer> lst)
        {
            try
            {
                List<DTOError> result = new List<DTOError>();
                using (var model = new DataEntities())
                {
                    foreach (var item in lst)
                    {
                        DTOError itemerror = new DTOError();
                        var obj = model.CAT_Container.FirstOrDefault(c => c.ID == item.ID);
                        if (obj != null)
                        {

                            model.CAT_Container.Remove(obj);
                            model.SaveChanges();
                        }
                        result.Add(itemerror);
                    }
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
}

