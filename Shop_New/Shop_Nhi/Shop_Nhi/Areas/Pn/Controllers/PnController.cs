using Shop_Nhi.Models.DAO;
using Shop_Nhi.Models.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Shop_Nhi.Areas.Pn.Controllers
{
    public class PnController : BaseController
    {
        //
        // GET: /Pn/Pn/

        #region action

        public ActionResult Index()
        {
            var productDao = new ProductDAO();
            var orderDao = new OrderDAO();
            var postDao = new PostDAO();
            ViewBag.Product = productDao.ListAll().Count;
            ViewBag.Order = orderDao.ListOrder().Count;
            ViewBag.ProductTrue = productDao.ListByStatusTrue().Count;
            ViewBag.ProductFalse = productDao.ListByStatusFalse().Count;
            ViewBag.ProductNull = (productDao.ListAll().Where(x => x.quantity == null).ToList()).Count;
            ViewBag.Post = postDao.List().ToList().Count;
            return View(orderDao.ListOrder());
        }

        public JsonResult OrderCharts(string dFrom, string dTo)
        {
            DateTime dateF = DateTime.Parse(dFrom);
            DateTime dateT = DateTime.Parse(dTo);
            var dao = new OrderDAO();
            var result = dao.OrderCharts(dateF, dateT);
            return Json(new
            {
                result = result
            },JsonRequestBehavior.AllowGet);
        }
        #endregion action
        #region layout
        //Top left
        [ChildActionOnly]
        public ActionResult _TopLeft()
        {
            return PartialView();
        }

        //menu left
        [ChildActionOnly]
        public ActionResult _MenuLeft()
        {
            return PartialView();
        }

        //menu left
        [ChildActionOnly]
        public ActionResult _TopAdmin()
        {
            return PartialView();
        }
        #endregion layout

        #region body
        public ActionResult Body()
        {
            return View();
        }

        [Authorize(Roles = "ADMIN")]
        public JsonResult ChangeTheme(int id, string link)
        {
            try
            {
                var dao = new ThemeDAO();
                dao.ChangeTheme(id, link);
                return Json(new
                {
                    status = true
                },JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(new
                {
                    status = false
                }, JsonRequestBehavior.AllowGet);
            }
        }

        #endregion body


        #region seo
        [Authorize(Roles = "ADMIN,MANAGE")]
        public ActionResult GetContentSeo()
        {
            var dao = new SeoDAO();
            return View(dao.List());
        }


        [HttpGet]
        [Authorize(Roles = "ADMIN,MANAGE")]
        public ActionResult EditSeo(int id)
        {
            var dao = new SeoDAO();
            return View(dao.GetContent(id));
        }

        [HttpPost]
        [Authorize(Roles = "ADMIN,MANAGE")]
        public ActionResult EditSeo(Seo seo)
        {
            try
            {
                var dao = new SeoDAO();
                dao.Edit(seo);
                return RedirectToAction("GetContentSeo","Pn");
            }
            catch
            {
                return View();
            }
        }
        #endregion seo
    }
}