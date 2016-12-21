using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using DTO;
using Business;
using IServices;
using System.ServiceModel.Web;
using Ninject.Modules;
using System.Threading;
using Ninject;

namespace Services
{
    public abstract class SVBase : ISVBase
    {
        protected T CreateBusiness<T>()
        {
            try
            {
                //FaultHelper.CheckTime();
                //if (DateTime.Now.Year >= 2017) throw new Exception("Fail");
                ////var account = (AccountItem)Thread.GetData(Thread.GetNamedDataSlot("Account"));
                //IKernel kernel = (account !=null)? new StandardKernel(): 
                //    new StandardKernel(new BLBindings());
                //IKernel kernel = new StandardKernel(new BLBindings());
                var callServiceDll = Convert.ToBoolean(System.Configuration.ConfigurationManager.AppSettings.Get("CALL_SERVICE_DLL"));
                IKernel kernel = new StandardKernel();
                if (callServiceDll == false)
                    kernel = new StandardKernel(new BLBindings());   
                return kernel.Get<T>();
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

    public class BLBindings : NinjectModule
    {
        public BLBindings(AccountItem accountItem)
        {
            Thread.SetData(Thread.GetNamedDataSlot("Account"), accountItem);

        }

        public BLBindings()
        {
            try
            {
                var obj = new AccountItem();
                obj.UserID = GetHeader<int?>("UserID");
                obj.UserName = GetHeader<string>("UserName");
                obj.FunctionID = GetHeader<int?>("FunctionID");
                obj.DriverID = GetHeader<int?>("DriverID");
                //int? customerid = GetHeader<int?>("CustomerID");
                //if (customerid > 0)
                //    obj.CustomerID = customerid.Value;
                //else
                //    obj.CustomerID = -1;
                int syscustomerid = GetHeader<int>("SYSCustomerID");
                obj.SYSCustomerID = syscustomerid;
                obj.IsAdmin = GetHeader<bool>("IsAdmin");
                obj.GroupID = GetHeader<int?>("GroupID");
                string str = GetHeader<string>("ListActionCode");
                if (!string.IsNullOrEmpty(str))
                    obj.ListActionCode = str.Split(',');
                else
                    obj.ListActionCode = new string[] { };
                str = GetHeader<string>("ListCustomerID");
                List<int> lst = new List<int>();
                if (!string.IsNullOrEmpty(str))
                {                    
                    try
                    {
                        foreach (var item in str.Split(','))
                            lst.Add(Convert.ToInt32(item));
                    }
                    catch  { }                    
                }
                obj.ListCustomerID = lst.ToArray();
                Thread.SetData(Thread.GetNamedDataSlot("Account"), obj);
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public override void Load()
        {

        }

        private T GetHeader<T>(string key)
        {
            if (OperationContext.Current.IncomingMessageHeaders.FindHeader(key, "") >= 0)
            {
                string val = OperationContext.Current.IncomingMessageHeaders.GetHeader<string>(key, "");
                if (!string.IsNullOrEmpty(val))
                {
                    if (typeof(T) == typeof(int?)) return (T)Convert.ChangeType(val, typeof(int));
                    else return (T)Convert.ChangeType(val, typeof(T));
                }
            }
            if (typeof(T) == typeof(bool))
                return (T)Convert.ChangeType(false, typeof(T));
            else
                return default(T);
        }
    }
}