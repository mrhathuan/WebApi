using System.IO;
using System.Threading;
using System.Web;
using Telerik.Reporting.Cache.Interfaces;
using Telerik.Reporting.Services.Engine;
using Telerik.Reporting.Services.WebApi;

namespace ClientWeb
{
    /// <summary>
    /// Summary description for ReportsController
    /// </summary>
    public class ReportsController : ReportsControllerBase
    {
        protected override IReportResolver CreateReportResolver()
        {
            var reportsPath = HttpContext.Current.Server.MapPath("~/Uploads/ReportDesigner");
            return new ReportFileResolver(reportsPath)
                .AddFallbackResolver(new ReportTypeResolver());
        }

        protected override ICache CreateCache()
        {
            var reportsPath = HttpContext.Current.Server.MapPath("~/Uploads/ReportCache");
            return Telerik.Reporting.Services.Engine.CacheFactory.CreateFileCache(reportsPath);
        }

        public override System.Net.Http.HttpResponseMessage GetParameters(string clientID, ClientReportSource reportSource)
        {
            var headerkey = reportSource.ParameterValues["headerkey"].ToString();
            var objAuth = Presentation.SecurityHelper.GetCacheByKey(headerkey);
            if (objAuth != null)
            {
                reportSource.ParameterValues.Add("CustomerName", objAuth.CustomerName);
                reportSource.ParameterValues.Add("Address", objAuth.Address);
                reportSource.ParameterValues.Add("TelNo", objAuth.TelNo);
                reportSource.ParameterValues.Add("Fax", objAuth.Fax);
                reportSource.ParameterValues.Add("Email", objAuth.Email);
                reportSource.ParameterValues.Add("Note", objAuth.Note);
                reportSource.ParameterValues.Add("Note1", objAuth.Note1);
                reportSource.ParameterValues.Add("Note2", objAuth.Note2);
                reportSource.ParameterValues.Add("Image", objAuth.Image);
            }
            else
            {
                reportSource.ParameterValues.Add("CustomerName", "");
                reportSource.ParameterValues.Add("Address", "");
                reportSource.ParameterValues.Add("TelNo", "");
                reportSource.ParameterValues.Add("Fax", "");
                reportSource.ParameterValues.Add("Email", "");
                reportSource.ParameterValues.Add("Note", "");
                reportSource.ParameterValues.Add("Note1", "");
                reportSource.ParameterValues.Add("Note2", "");
                reportSource.ParameterValues.Add("Image", "");
            }
            reportSource.ParameterValues.Add("Copyright", "© 2015 SMARTLOG");

            return base.GetParameters(clientID, reportSource);
        }

        //private string GetKeySession()
        //{
        //    if (this.ControllerContext.HttpContext.Request.Cookies.AllKeys.Contains("KeySession"))
        //        return this.ControllerContext.HttpContext.Request.Cookies["KeySession"].Value;
        //    else
        //        return null;
        //}

        //private void AuthKeySession()
        //{
        //    var key = GetKeySession();
        //    if (key != null)
        //        Thread.SetData(Thread.GetNamedDataSlot("KeySession"), GetKeySession());
        //}

        //private string GetKeySession()
        //{
        //    if (this.ControllerContext.HttpContext.Request.Cookies.AllKeys.Contains("KeySession"))
        //        return this.ControllerContext.HttpContext.Request.Cookies["KeySession"].Value;
        //    else
        //        return null;
        //}
    }
}