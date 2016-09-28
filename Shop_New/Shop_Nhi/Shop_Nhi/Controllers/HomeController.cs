using Shop_Nhi.Models;
using Shop_Nhi.Models.DAO;
using Shop_Nhi.Models.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Shop_Nhi.Controllers
{
    public class HomeController : Controller
    {

        public ActionResult Demo()
        {
            var seo = new SeoDAO();
            var dao = new ProductDAO();
            ViewBag.ListByStatusFalse = dao.ListByStatusFalse();
            ViewBag.ListByStatusTrue = dao.ListByStatusTrue();
            ViewBag.ListNew = dao.ListNewPro(40);
            ViewBag.Hot = dao.ListViewcount(40);
            ViewBag.ListSale = dao.ListSale(40);
            ViewBag.Title = seo.GetContent(1).metaTitle;
            ViewBag.KeyWords = seo.GetContent(1).metaKeyword;
            ViewBag.Description = seo.GetContent(1).metaDescription;
            return View();
        }

       [OutputCache(Duration = 30)]
        public ActionResult Index()
        {
            var seo = new SeoDAO();
            var dao = new ProductDAO();
            ViewBag.ListByStatusFalse = dao.ListByStatusFalse();
            ViewBag.ListByStatusTrue = dao.ListByStatusTrue();
            ViewBag.ListNew = dao.ListNewPro(40);
            ViewBag.Hot = dao.ListViewcount(40);
            ViewBag.ListSale = dao.ListSale(40);
            ViewBag.Title = seo.GetContent(1).metaTitle;
            ViewBag.KeyWords = seo.GetContent(1).metaKeyword;
            ViewBag.Description = seo.GetContent(1).metaDescription;
            return View();
        }
        #region product
        public JsonResult ListName(string q)
        {
            var dao = new ProductDAO();
            var data = dao.ListName(q);
            return Json(new
            {
                status = true,
                data = data.Select(p => new
                {
                    name = p.productName
                })
            }, JsonRequestBehavior.AllowGet);
        }

        //Set like
        [HttpPost]
        public JsonResult SetLike(long id)
        {
            var dao = new ProductDAO();
            int like = (int)((dao.GetById(id).like.GetValueOrDefault(0)) + 1);
            dao.SetLike(id,like);
            return Json(new
            {
                status = true
            });
        }
        //Get id
        [HttpPost]
        public JsonResult GetProduct(long id)
        {
            try
            {
                var dao = new ProductDAO();
                var product = new Product();
                var category = new Category();
                var data = dao.GetById(id);
                decimal? price = 0;
                if (data.promotionPrice != null)
                {
                    price = data.promotionPrice;
                }
                else
                {
                    price = data.price;
                }
                product.ID = data.ID;
                product.metatTitle = data.metatTitle;
                product.productName = data.productName;                                
                product.price = price;
                product.quantity = data.quantity;
                product.description = data.description;
                product.image = data.image;
                product.status = data.status;

                category.name = data.Category.name;
                category.ID = data.Category.ID;
                category.metatTitle = data.Category.metatTitle;
                return Json(new
                {
                    status = true,
                    product,
                    category
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

        #endregion product

        #region layout
        public ActionResult _Header()
        {
            var cart = Session["CartSession"];
            var cartItem = new List<CartItem>();
            if (cart != null)
            {
                cartItem = (List<CartItem>)cart;
            }
            return PartialView(cartItem);
        }

        public ActionResult _TopMenu()
        {
            ViewBag.Session = null;
            if(Session["username"] != null)
            {
                ViewBag.Session = (string)Session["username"];
            }
            var dao = new MenuDAO();
            var result = dao.List();
            return PartialView(result);
        }

        [OutputCache(Duration = 3600 * 24)]
        public ActionResult _Menu()
        {
            var dao = new CategoryDAO();
            return PartialView(dao.ListByShowhome());
        }


        //[OutputCache(Duration = 3600 * 24)]
        public ActionResult _Slide()
        {
            var dao = new SlideDAO();
            return PartialView(dao.GetListSlide());
        }

        public ActionResult _Suport()
        {
            
            return PartialView();
        }


        [OutputCache(Duration = 3600)]
        public PartialViewResult _Category()
        {
            var cateDao = new CategoryDAO();
            var proDao = new ProductDAO();
            var postDao = new PostDAO();
            ViewBag.ListLike = proDao.ListLike(7);
            ViewBag.ListView = proDao.ListViewcount(7);
            ViewBag.CatePro = cateDao.ListByStatus();
            ViewBag.Tags = postDao.ListAllTag();
            return PartialView();
        }

        [OutputCache(Duration = 3600 * 24)]
        public ActionResult _Post()
        {
            var dao = new PostDAO();
            return PartialView(dao.List());
        }

        [OutputCache(Duration = 3600)]
        public ActionResult _Footer()
        {
            var page = new MenuDAO().List();
            var category = new CategoryDAO().ListByShowhome();
            ViewBag.Page = page;
            ViewBag.Menu = category;
            var footer = new FooterDAO();
            return PartialView(footer.GetByID(1));
        }

        [OutputCache(Duration = 3600 * 24)]
        public ActionResult _Popup()
        {
            var dao = new NotificationDAO();
            return PartialView(dao.GetNotificationById(1));
        }
        #endregion layout

        #region page
        public ActionResult PageContent(string linkMenu)
        {
            var dao = new PageBodyDAO();
            var result = dao.GetContent(linkMenu);          
            ViewBag.KeyWords = result.metaKeywords;
            ViewBag.Description = result.metaDescription;
            ViewBag.MenuName = result.Menu.Name;
            return View(dao.GetContent(linkMenu));
        }
        #endregion page


        #region contact
        [OutputCache(Duration = 3600 * 24)]
        public ActionResult Contact()
        {
            var dao = new ContactDAO();
            return View(dao.GetByID(1));
        }
        #endregion contact
    }
}