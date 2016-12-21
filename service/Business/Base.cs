using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
using System.Threading;
using Data;
using System.Configuration;
using System.Net.Mail;
using System.Net;
using System.Web.Hosting;
using Kendo.Mvc.UI;
using Newtonsoft.Json;
using ExpressionEvaluator;
using System.Runtime.CompilerServices;

namespace Business
{
    public interface IBase : IDisposable { }

    public abstract partial class Base : IBase
    {
        protected readonly AccountItem Account;
        protected int LocationDefaultID = 1;

        public Base()
        {
            Account = (AccountItem)Thread.GetData(Thread.GetNamedDataSlot("Account"));
            if (DataEntitiesObject.IsRegistry() == false)
            {
                System.Diagnostics.Debug.WriteLine("Workflow registry");
                DataEntitiesObject.OnDataEntitiesChanged += HelperMessageQueue.EventQueue.EnqueueAMessage;
            }
        }

        public virtual void Dispose()
        {

        }

        protected DataSourceRequest CreateRequest(string strRequest)
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

        protected DataSourceRequest CreateRequest_ByFieldAndDate(DateTime from, DateTime to, string field)
        {
            var result = new DataSourceRequest();

            try
            {
                var filters = new List<Kendo.Mvc.IFilterDescriptor>();

                var fAnd = new Kendo.Mvc.CompositeFilterDescriptor();
                fAnd.LogicalOperator = Kendo.Mvc.FilterCompositionLogicalOperator.And;
                fAnd.FilterDescriptors = new Kendo.Mvc.Infrastructure.Implementation.FilterDescriptorCollection();
                var fFrom = new Kendo.Mvc.FilterDescriptor();
                fFrom.Member = field;
                fFrom.Operator = Kendo.Mvc.FilterOperator.IsGreaterThan;
                fFrom.Value = from;
                var fTo = new Kendo.Mvc.FilterDescriptor();
                fTo.Member = field;
                fTo.Operator = Kendo.Mvc.FilterOperator.IsLessThan;
                fTo.Value = to;
                fAnd.FilterDescriptors.Add(fFrom);
                fAnd.FilterDescriptors.Add(fTo);
                filters.Add(fAnd);
                result.Filters = filters;
            }
            catch
            {
                result = new DataSourceRequest();
            }

            return result;
        }
    }

    public class AccountItem
    {
        public int? UserID { get; set; }
        public string UserName { get; set; }
        public int? FunctionID { get; set; }
        public int SYSCustomerID { get; set; }
        public int? DriverID { get; set; }
        public bool IsAdmin { get; set; }
        public int? GroupID { get; set; }
        public int[] ListCustomerID { get; set; }
        public string[] ListActionCode { get; set; }
    }

    public class CommonList : Base, IBase
    {
        /// <summary>
        /// Lấy danh sách các địa chỉ theo khách hàng
        /// </summary>
        /// <param name="customerID">Mã khách hàng</param>
        /// <param name="ParentType">Phân loại đối tượng Partner (0: khách hàng)</param>
        /// <param name="PartnerID">Mã đối tác</param>
        /// <returns>Giá trị trả về customize CUSLocation ép theo kiểu CATLOcation</returns>
        //public List<CATLocation> GetLocations(int? customerID, int? ParentType, int? PartnerID)
        //{
        //    try
        //    {
        //        using (var model = new DataEntities())
        //        {
        //            using (CopyHelper helper = new CopyHelper())
        //            {
        //                List<CATLocation> list = null;
        //                if (ParentType == 0)
        //                {
        //                    list = model.CUS_Location.Where(c => c.CustomerID == customerID &&
        //                        (c.CusPartID == null || c.CusPartID == 0)).Select(c => new CATLocation
        //                        {
        //                            ID = c.ID,
        //                            Address = c.CAT_Location.Address,
        //                            Code = c.CAT_Location.Code,
        //                            Customer = new CUSCustomer
        //                            {
        //                                Address = c.CUS_Customer.Address,
        //                                BiddingID = c.CUS_Customer.BiddingID,
        //                                BillingAddress = c.CUS_Customer.BillingAddress,
        //                                BillingName = c.CUS_Customer.BillingName,
        //                                Code = c.CUS_Customer.Code,
        //                                CountryID = c.CUS_Customer.CountryID,
        //                                CustomerName = c.CUS_Customer.CustomerName,
        //                                DistrictID = c.CUS_Customer.DistrictID,
        //                                Email = c.CUS_Customer.Email,
        //                                Fax = c.CUS_Customer.Fax,
        //                                ID = c.CUS_Customer.ID,
        //                                //IsSKU = c.CUS_Customer.IsSKU,
        //                                Note = c.CUS_Customer.Note,
        //                                ProvinceID = c.CUS_Customer.ProvinceID,
        //                                TaxCode = c.CUS_Customer.TaxCode,
        //                                TelNo = c.CUS_Customer.TelNo,
        //                                WardID = c.CUS_Customer.WardID
        //                            },
        //                            ParentID = c.CusPartID,
        //                            DistrictID = c.CAT_Location.DistrictID,
        //                            Lat = c.CAT_Location.Lat,
        //                            Lng = c.CAT_Location.Lng,
        //                            Location = c.CAT_Location.Location,
        //                            ProvinceID = c.CAT_Location.ProvinceID,
        //                            WardID = c.CAT_Location.WardID
        //                        }).ToList<CATLocation>();
        //                }
        //                else if (PartnerID == null || PartnerID == 0)
        //                {
        //                    list = model.CUS_Location.Where(c => c.CustomerID == customerID &&
        //                        c.CusPartID != null && c.CUS_Partner.CAT_Partner.TypeOfPartnerID == ParentType).Select(c => new CATLocation
        //                         {
        //                             ID = c.ID,
        //                             Address = c.CAT_Location.Address,
        //                             Code = c.CAT_Location.Code,
        //                             Customer = new CUSCustomer
        //                             {
        //                                 Address = c.CUS_Customer.Address,
        //                                 BiddingID = c.CUS_Customer.BiddingID,
        //                                 BillingAddress = c.CUS_Customer.BillingAddress,
        //                                 BillingName = c.CUS_Customer.BillingName,
        //                                 Code = c.CUS_Customer.Code,
        //                                 CountryID = c.CUS_Customer.CountryID,
        //                                 CustomerName = c.CUS_Customer.CustomerName,
        //                                 DistrictID = c.CUS_Customer.DistrictID,
        //                                 Email = c.CUS_Customer.Email,
        //                                 Fax = c.CUS_Customer.Fax,
        //                                 ID = c.CUS_Customer.ID,
        //                                 //IsSKU = c.CUS_Customer.IsSKU,
        //                                 Note = c.CUS_Customer.Note,
        //                                 ProvinceID = c.CUS_Customer.ProvinceID,
        //                                 TaxCode = c.CUS_Customer.TaxCode,
        //                                 TelNo = c.CUS_Customer.TelNo,
        //                                 WardID = c.CUS_Customer.WardID
        //                             },
        //                             DistrictID = c.CAT_Location.DistrictID,
        //                             Lat = c.CAT_Location.Lat,
        //                             Lng = c.CAT_Location.Lng,
        //                             Location = c.CAT_Location.Location,
        //                             ProvinceID = c.CAT_Location.ProvinceID,
        //                             WardID = c.CAT_Location.WardID,
        //                             ParentID = c.CusPartID,
        //                             Partner = new CATPartner
        //                             {
        //                                 Address = c.CUS_Partner.CAT_Partner.Address,
        //                                 BillingName = c.CUS_Partner.CAT_Partner.BillingName,
        //                                 BillingAddress = c.CUS_Partner.CAT_Partner.BillingAddress,
        //                                 Code = c.CUS_Partner.CAT_Partner.Code,
        //                                 CountryID = c.CUS_Partner.CAT_Partner.CountryID,
        //                                 PartnerName = c.CUS_Partner.CAT_Partner.PartnerName,
        //                                 DistrictID = c.CUS_Partner.CAT_Partner.DistrictID,
        //                                 Email = c.CUS_Partner.CAT_Partner.Email,
        //                                 Fax = c.CUS_Partner.CAT_Partner.Fax,
        //                                 ID = c.CUS_Partner.CAT_Partner.ID,
        //                                 Note = c.CUS_Partner.CAT_Partner.Note,
        //                                 ProvinceID = c.CUS_Partner.CAT_Partner.ProvinceID,
        //                                 TaxCode = c.CUS_Partner.CAT_Partner.TaxCode,
        //                                 TelNo = c.CUS_Partner.CAT_Partner.TelNo,
        //                                 WardID = c.CUS_Partner.CAT_Partner.WardID,
        //                                 TypeOfPartnerID = c.CUS_Partner.CAT_Partner.TypeOfPartnerID
        //                             }
        //                         }).ToList<CATLocation>();
        //                }
        //                else
        //                {
        //                    list = model.CUS_Location.Where(c => c.CustomerID == customerID &&
        //                        c.CusPartID == PartnerID).Select(c => new CATLocation
        //                         {
        //                             ID = c.ID,
        //                             Address = c.CAT_Location.Address,
        //                             Code = c.CAT_Location.Code,
        //                             Customer = new CUSCustomer
        //                             {
        //                                 Address = c.CUS_Customer.Address,
        //                                 BiddingID = c.CUS_Customer.BiddingID,
        //                                 BillingAddress = c.CUS_Customer.BillingAddress,
        //                                 BillingName = c.CUS_Customer.BillingName,
        //                                 Code = c.CUS_Customer.Code,
        //                                 CountryID = c.CUS_Customer.CountryID,
        //                                 CustomerName = c.CUS_Customer.CustomerName,
        //                                 DistrictID = c.CUS_Customer.DistrictID,
        //                                 Email = c.CUS_Customer.Email,
        //                                 Fax = c.CUS_Customer.Fax,
        //                                 ID = c.CUS_Customer.ID,
        //                                 //IsSKU = c.CUS_Customer.IsSKU,
        //                                 Note = c.CUS_Customer.Note,
        //                                 ProvinceID = c.CUS_Customer.ProvinceID,
        //                                 TaxCode = c.CUS_Customer.TaxCode,
        //                                 TelNo = c.CUS_Customer.TelNo,
        //                                 WardID = c.CUS_Customer.WardID
        //                             },
        //                             DistrictID = c.CAT_Location.DistrictID,
        //                             Lat = c.CAT_Location.Lat,
        //                             Lng = c.CAT_Location.Lng,
        //                             Location = c.CAT_Location.Location,
        //                             ProvinceID = c.CAT_Location.ProvinceID,
        //                             WardID = c.CAT_Location.WardID,
        //                             ParentID = c.CusPartID,
        //                             Partner = new CATPartner
        //                             {
        //                                 Address = c.CUS_Partner.CAT_Partner.Address,
        //                                 BillingName = c.CUS_Partner.CAT_Partner.BillingName,
        //                                 BillingAddress = c.CUS_Partner.CAT_Partner.BillingAddress,
        //                                 Code = c.CUS_Partner.CAT_Partner.Code,
        //                                 CountryID = c.CUS_Partner.CAT_Partner.CountryID,
        //                                 PartnerName = c.CUS_Partner.CAT_Partner.PartnerName,
        //                                 DistrictID = c.CUS_Partner.CAT_Partner.DistrictID,
        //                                 Email = c.CUS_Partner.CAT_Partner.Email,
        //                                 Fax = c.CUS_Partner.CAT_Partner.Fax,
        //                                 ID = c.CUS_Partner.CAT_Partner.ID,
        //                                 Note = c.CUS_Partner.CAT_Partner.Note,
        //                                 ProvinceID = c.CUS_Partner.CAT_Partner.ProvinceID,
        //                                 TaxCode = c.CUS_Partner.CAT_Partner.TaxCode,
        //                                 TelNo = c.CUS_Partner.CAT_Partner.TelNo,
        //                                 WardID = c.CUS_Partner.CAT_Partner.WardID,
        //                                 TypeOfPartnerID = c.CUS_Partner.CAT_Partner.TypeOfPartnerID
        //                             }
        //                         }).ToList<CATLocation>();
        //                }
        //                return list;
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw FaultHelper.BusinessFault(ex);
        //    }
        //}

        public string GetWebsite()
        {
            string result = string.Empty;
            try
            {
                result = ConfigurationManager.AppSettings["Website"];
            }
            catch { }
            return result;
        }
    }

    public class MailHelper
    {
        public const string OPSTenderingContainerSubject = "Đề nghị vận chuyển container";
        public const string OPSTenderingDistributorSubject = "Đề nghị vận chuyển hàng ngày";

        public static void SendMail(DTOSYSSetting pSystem, string pMailTo, string pMailName, string pCC, string pSubject, string pBody)
        {
            try
            {
                if (pSystem != null)
                {
                    string smtp = pSystem.MailSMTP;
                    int port = Convert.ToInt32(pSystem.MailPORT);
                    string username = pSystem.MailUSER;
                    string password = pSystem.MailPASS;
                    string from = pSystem.MailFROM;
                    string name = pSystem.MailNAME;

                    SmtpClient client = new SmtpClient(smtp, port);
                    client.EnableSsl = true;
                    MailAddress fr = new MailAddress(from, name);
                    MailAddress to = new MailAddress(pMailTo, pMailName);
                    MailMessage message = new MailMessage(from, pMailTo);
                    message.Subject = pSubject;
                    if (!string.IsNullOrEmpty(pCC))
                    {
                        string[] strs = pCC.Split(';');
                        foreach (var str in strs)
                        {
                            var cc = new MailAddress(str, str);
                            message.CC.Add(cc);
                        }
                    }
                    message.SubjectEncoding = System.Text.Encoding.UTF8;
                    message.Body = pBody;
                    message.BodyEncoding = System.Text.Encoding.UTF8;

                    message.IsBodyHtml = true;
                    NetworkCredential myCreds = new NetworkCredential(username, password, "");
                    client.Credentials = myCreds;
                    client.Send(message);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static string StringHTML(string pContent, Action<MailTemplate> pAction)
        {
            string result = string.Empty;
            try
            {
                if (!string.IsNullOrEmpty(pContent))
                {
                    string[] tokens = pContent.Split(new char[] { '[', ']' });
                    foreach (string token in tokens)
                    {
                        char splitter = (char)254;
                        string[] tokenParts = token.Replace("::", splitter.ToString()).Split(new char[] { splitter });
                        if (tokenParts[0].Length > 0)
                        {
                            var obj = new MailTemplate();
                            obj.Token = tokenParts[0];
                            obj.HTML = tokenParts[0];
                            pAction(obj);
                            result += obj.HTML;
                        }
                    }
                }
            }
            catch { }

            return result;
        }
    }

    public enum MailTemplateCode
    {
        OPSTenderingContainer,
        OPSTenderingDistributor
    }

    public class MailTemplate
    {
        public string Token { get; set; }
        public string HTML { get; set; }
    }
}
