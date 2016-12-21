using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DTO;
using System.Web.Script.Serialization;

namespace ClientWeb
{
    /// <summary>
    /// Summary description for File
    /// </summary>
    public class FileHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            try
            {
                var folderPath = context.Request.Params.Get("folderPath");
                if (context.Request.Files.Count <= 0)
                {
                    context.Response.Write("No file");
                }
                else
                {
                    var result = new CATFile();
                    HttpPostedFile file = context.Request.Files[0];
                    string fileExt = file.FileName.Substring(file.FileName.LastIndexOf("."));
                    string filePath = folderPath + "_" + DateTime.Now.ToString("ddMMyyhhmmss") + fileExt;
                    file.SaveAs(context.Server.MapPath("/" + filePath));
                    result.FilePath = filePath;
                    result.FileName = file.FileName;
                    result.FileExt = fileExt;

                    context.Response.Write(new JavaScriptSerializer().Serialize(result));
                }
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