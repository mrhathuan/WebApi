using CacheManager.Core;
using DTO;
using Kendo.Mvc.UI;
using Microsoft.Practices.Unity;
using Newtonsoft.Json;
using Presentation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace ClientWeb_API
{
    public class BaseController : ApiController
    {
        protected override void Initialize(System.Web.Http.Controllers.HttpControllerContext controllerContext)
        {

            try
            {
                var obj = GetCache();
                SecurityHelper.SetCache(obj.HeaderKey, obj);
            }
            catch (Exception ex)
            {
                //throw;
            }
            base.Initialize(controllerContext);
        }

        [Dependency]
        protected ICacheManager<DTOAuthorization> authorizationCache { get; set; }

        protected void RegistryCache(DTOAuthorization item)
        {
            item.HeaderKey = StringHelper.RNGCharacterMask();
            authorizationCache.Add(item.HeaderKey, item);
        }

        protected DTOAuthorization GetCache()
        {
            var request = HttpContext.Current.Request;
            string auth = request.Headers["auth"];
            if (!string.IsNullOrEmpty(auth))
            {
                var result = authorizationCache.Get(auth);
                if (result == null)
                    throw new Exception("GetCache fail");
                else
                    return result;
            }
            else
                throw new Exception("GetCache fail");
        }

        protected bool HasCache()
        {
            var result = false;
            var request = HttpContext.Current.Request;
            string auth = request.Headers["auth"];
            if (!string.IsNullOrEmpty(auth))
            {
                var obj = authorizationCache.Get(auth);
                if (obj != null)
                    result = true;
            }
            return result;
        }

        protected DTOAuthorization Account
        {
            get
            {
                return GetCache();
            }
        }

        protected DataSourceRequest CreateRequest(string str)
        {
            var result = new DataSourceRequest();

            var request = (CustomRequest)JsonConvert.DeserializeObject<CustomRequest>(str);

            result.Page = Convert.ToInt32(request.Page);
            result.PageSize = Convert.ToInt32(request.PageSize);

            //"FirstName~contains~'a'~and~LastName~contains~'b'"
            var filters = new List<Kendo.Mvc.IFilterDescriptor>();
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
                    f.Member = strs[0];
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
                    string strVal = strs[2];
                    if (strVal.StartsWith("'"))
                        strVal = strVal.Substring(1, strVal.Length - 2);
                    f.Value = strVal;

                    fand.FilterDescriptors.Add(f);
                }
            }
            filters.Add(fand);
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

            return result;
        }
    }

    public class CustomRequest
    {
        public string Group { get; set; }
        public string Page { get; set; }
        public string PageSize { get; set; }
        public string Sort { get; set; }
        public string Filter { get; set; }
    }
}
