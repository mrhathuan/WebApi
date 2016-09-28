using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Shop_Nhi.Areas.Pn.Controllers
{
        public class BaseController : Controller
        {
            protected override void OnActionExecuting(ActionExecutingContext filterContext)
            {
                var username = Session["username"];
                if (username == null)
                {
                    filterContext.Result = new RedirectToRouteResult(new
                        RouteValueDictionary(new { controller = "Login", action = "Index", Area = "Pn" }));
                }
                base.OnActionExecuting(filterContext);
            }

            //thông báo
            protected void SetAlert(string message, string type)
            {
                TempData["AlertMessage"] = message;
                if (type == "success")
                {
                    TempData["AlertType"] = "alert-success";
                }
                else if (type == "warning")
                {
                    TempData["AlertType"] = "alert-warning";
                }
                else if (type == "error")
                {
                    TempData["AlertType"] = "alert-danger";
                }
            }
        }
	}
