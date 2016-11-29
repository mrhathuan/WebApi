using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Client_Web.Areas.Tk.Controllers
{
    public class MainController : Controller
    {
        // GET: Tk/Main

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult DASH_Index()
        {
            return View();
        }
    }
}