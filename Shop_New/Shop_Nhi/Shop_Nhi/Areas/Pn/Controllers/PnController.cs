using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Shop_Nhi.Common;
using Shop_Nhi.Models.DAO;
using Shop_Nhi.Models.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace Shop_Nhi.Areas.Pn.Controllers
{
    public class PnController : BaseController
    {

        private ProductDAO productDao = new ProductDAO();
        //
        // GET: /Pn/Pn/

        #region Index

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


        #region DASH
        public ActionResult DASH_Index()
        {

            return View();
        }
        #endregion DASH

        #region product
        [Authorize(Roles = "ADMIN,MANAGE")]
        public ActionResult PRO_Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult PRO_Read([DataSourceRequest]DataSourceRequest request)
        {
            var dao = new ProductDAO();
            IList<Product> item = new List<Product>();
            item = dao.List().Select(x => new Product
            {
                ID = x.ID,
                code = x.code,
                productName = x.productName,
                image = x.image,
                price = x.price,
                promotionPrice = x.promotionPrice,
                quantity = x.quantity,
                categoryID = x.categoryID,
                Category = new Category
                {
                    ID = x.Category.ID,
                    name = x.Category.name
                },
                createDate = x.createDate,
                modifiedByDate = x.modifiedByDate,
                like = x.like,
                viewCount = x.viewCount,
                status = x.status

            }).ToList();
            return Json(item.ToDataSourceResult(request));
        }

        //excel
        [HttpPost]
        public ActionResult Excel_Export_Save(string contentType, string base64, string fileName)
        {
            var fileContents = Convert.FromBase64String(base64);

            return File(fileContents, contentType, fileName);
        }

        public ActionResult PRO_Categories_Filter()
        {
            var dao = new CategoryDAO();
            var categories = dao.ListAll()
                        .Select(c => new Category
                        {
                            ID = c.ID,
                            name = c.name
                        })
                        .OrderBy(e => e.createDate);

            return Json(categories, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateInput(false)]
        public JsonResult PRO_Save(string item)
        {
            try
            {
                var dao = new ProductDAO();
                JavaScriptSerializer seriaLizer = new JavaScriptSerializer();
                Product pro = seriaLizer.Deserialize<Product>(item);
                if (!StringHelper.IsValiCode(pro.code.Trim()) || pro.code.Trim().Length > 20)
                    throw new Exception("MÃ SẢN PHẨM KHỐNG ĐÚNG. KHÔNG THỂ LƯU.");
                pro.code = pro.code.Trim();
                pro.metatTitle = StringHelper.RemoveSpecialChars(pro.productName.Trim()).Replace(" ", "-");
                pro.status = true;
                pro.createDate = DateTime.Now;
                pro.createByID = (string)Session["username"];
                dao.Save(pro);
                return Json(new
                {
                    msg = "Thành công",
                    status = true
                });
            }
            catch (Exception e)
            {
                return Json(new
                {
                    msg = e.Message,
                    status = false
                });
            }
        }

        #endregion product

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