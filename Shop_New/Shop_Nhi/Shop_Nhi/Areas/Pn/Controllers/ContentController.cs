using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Shop_Nhi.Models.DAO;

namespace Shop_Nhi.Areas.Pn.Controllers
{
    public class ContentController : BaseController
    {
        // GET: Pn/Content
        public ActionResult CONTENT_Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult THEME_Change(string link)
        {
            try
            {
                var dao = new ThemeDAO();
                dao.ChangeTheme(1, link);
                return Json(new
                {
                    status = true
                });
            }
            catch
            {
                return Json(new
                {
                    status = false
                });
            }
        }
    }
}