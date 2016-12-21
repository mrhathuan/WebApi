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
using System.Text.RegularExpressions;
using System.Security.Cryptography;
using System.Text;
using Presentation;

namespace ClientWeb
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

        protected void RegistryCache(DTOAuthorization item)
        {
            item.HeaderKey = StringHelper.RNGCharacterMask();
            SecurityHelper.SetCache(item.HeaderKey, item);
        }

        protected DTOAuthorization GetCache()
        {
            var result = SecurityHelper.GetCache();
            if (result == null)
                throw new Exception("GetCache fail");
            else
                return result;
        }

        protected bool HasCache()
        {
            var result = SecurityHelper.GetCache();
            if (result == null)
                return false;
            else
                return true;
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

    //public class StringHelper
    //{
    //    private static readonly string[] VietnameseSigns = new string[] { "aAeEoOuUiIdDyY", "áàạảãâấầậẩẫăắằặẳẵ", "ÁÀẠẢÃÂẤẦẬẨẪĂẮẰẶẲẴ", "éèẹẻẽêếềệểễ", "ÉÈẸẺẼÊẾỀỆỂỄ", "óòọỏõôốồộổỗơớờợởỡ", "ÓÒỌỎÕÔỐỒỘỔỖƠỚỜỢỞỠ", "úùụủũưứừựửữ", "ÚÙỤỦŨƯỨỪỰỬỮ", "íìịỉĩ", "ÍÌỊỈĨ", "đ", "Đ", "ýỳỵỷỹ", "ÝỲỴỶỸ" };

    //    public static string RemoveSign4VietnameseString(string str)
    //    {
    //        for (int i = 1; i < VietnameseSigns.Length; i++)
    //            for (int j = 0; j < VietnameseSigns[i].Length; j++)
    //                str = str.Replace(VietnameseSigns[i][j], VietnameseSigns[0][i - 1]);
    //        return str;
    //    }

    //    public static string RemoveSpecialCharacters(string input)
    //    {
    //        Regex r = new Regex(@"[^a-zA-Z0-9\-\\/(),_\s]+", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.Compiled);
    //        return r.Replace(input, String.Empty);
    //    }

    //    public static string RemoveVietSignAndSpecialChar(string input)
    //    {
    //        return RemoveSpecialCharacters(RemoveSign4VietnameseString(input));
    //    }

    //    public static string ReadVietFromNumber(long value)
    //    {
    //        string result = string.Empty;
    //        Dictionary<string, string> charNormal = new Dictionary<string, string> { { "0", "không" }, { "1", "một" }, { "2", "hai" }, { "3", "ba" }, { "4", "bốn" }, { "5", "năm" }, { "6", "sáu" }, { "7", "bảy" }, { "8", "tám" }, { "9", "chín" } };
    //        Dictionary<string, string> charLessHundred = new Dictionary<string, string> { { "0", "lẻ" }, { "1", "mốt" }, { "2", "hai" }, { "3", "ba" }, { "4", "bốn" }, { "5", "lăm" }, { "6", "sáu" }, { "7", "bảy" }, { "8", "tám" }, { "9", "chín" } };
    //        string[] charLevel = new string[] { "mươi", "trăm", "ngàn", "mươi", "trăm", "triệu", "mươi", "trăm", "tỉ" };
    //        string charLevelTen = "mười";

    //        if (value == 0)
    //            result = charNormal["0"];
    //        else
    //        {
    //            List<string> lstStr = new List<string>();
    //            int i = value.ToString().Length / 9;
    //            while (i >= 0)
    //            {
    //                int f = i > 0 ? value.ToString().Length - (i * 9) : 0;
    //                int t = f == 0 ? value.ToString().Length > 9 ? value.ToString().Length - (i * 9 + 9) : value.ToString().Length : 9;
    //                lstStr.Add(value.ToString().Substring(f, t));

    //                i--;
    //            }
    //            if (lstStr.Count > 0)
    //            {
    //                foreach (var str in lstStr)
    //                {
    //                    string strLevel = string.Empty;
    //                    for (int j = 0; j < str.Length; j++)
    //                    {
    //                        string s = str[str.Length - j - 1].ToString();
    //                        string n = j + 1 < str.Length ? str[str.Length - j - 2].ToString() : "";
    //                        switch (s)
    //                        {
    //                            case "0":
    //                                if (j == 0 || j == 3 || j == 6)
    //                                    s = "";
    //                                else if (j == 1 || j == 4 || j == 7)
    //                                {
    //                                    if (strLevel.Trim() == "")
    //                                        s = "";
    //                                    else
    //                                        s = charLessHundred[s];
    //                                }
    //                                else
    //                                    s = charNormal[s];
    //                                break;
    //                            case "1":
    //                                if (j == 1 || j == 4 || j == 7)
    //                                    s = "";
    //                                else if ((j == 0 || j == 3 || j == 6) && n.Length > 0 && n == "1")
    //                                    s = charNormal[s];
    //                                else if ((j == 0 || j == 3 || j == 6) && n.Length > 0 && n != "0")
    //                                    s = charLessHundred[s];
    //                                else
    //                                    s = charNormal[s];
    //                                break;
    //                            default:
    //                                if (j == 0 || j == 3 || j == 6)
    //                                    s = charLessHundred[s];
    //                                else
    //                                    s = charNormal[s];
    //                                break;
    //                        }
    //                        if (n != "")
    //                        {
    //                            if ((j == 0 || j == 3 || j == 6) && n == "0")
    //                                strLevel = s != "" ? s + " " + strLevel : strLevel;
    //                            else
    //                            {
    //                                if ((j == 0 || j == 3 || j == 6) && n.Length > 0 && n == "1")
    //                                    strLevel = s != "" ? charLevelTen + " " + s + " " + strLevel : charLevelTen + " " + strLevel;
    //                                else
    //                                    strLevel = s != "" ? charLevel[j] + " " + s + " " + strLevel : charLevel[j] + " " + strLevel;
    //                            }
    //                        }
    //                        else
    //                            strLevel = s != "" ? s + " " + strLevel : strLevel;
    //                    }
    //                    result = charLevel[8] + " " + strLevel + result;
    //                }
    //                if (result != string.Empty)
    //                    result = result.Substring((charLevel[8] + " ").Length).Trim();
    //            }
    //        }
    //        return result;
    //    }

    //    //Create Unique Key
    //    public static string RNGCharacterMask()
    //    {
    //        int maxSize = 8;
    //        int minSize = 5;
    //        char[] chars = new char[62];
    //        string a = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
    //        chars = a.ToCharArray();
    //        int size = maxSize;
    //        byte[] data = new byte[1];
    //        RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider();
    //        crypto.GetNonZeroBytes(data);
    //        size = maxSize;
    //        data = new byte[size];
    //        crypto.GetNonZeroBytes(data);
    //        StringBuilder result = new StringBuilder(size);
    //        foreach (byte b in data)
    //        { result.Append(chars[b % (chars.Length - 1)]); }
    //        return result.ToString();
    //    }
    //}
}