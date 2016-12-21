using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClientWeb
{
    /// <summary>
    /// Summary description for Features
    /// </summary>
    public class FeatureHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/javascript";
            try
            {
                var filePath = "";
                if (context.Request.Params.AllKeys.Contains("customer"))
                    filePath = context.Request.Params.Get("customer");

                var output = System.IO.File.ReadAllText(context.Server.MapPath("~/Scripts/features" + filePath + ".js"));
                context.Response.Write(output);
            }
            catch (Exception ex)
            {
                context.Response.Write(ex.Message);
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}