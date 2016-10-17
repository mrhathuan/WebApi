using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Shop_Nhi.Models.DAO;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using System.Web.Script.Serialization;
using Shop_Nhi.Models.Framework;

namespace Shop_Nhi.Areas.Pn.Controllers
{
    [Authorize(Roles = "ADMIN")]
    public class ContentController : BaseController
    {
        // GET: Pn/Content

        #region VIEW
        public ActionResult CONTENT_Index()
        {
            return View();
        }

        public ActionResult NOTI_Index()
        {
            return View();
        }
        public ActionResult SLIDE_Index()
        {
            return View();
        }
        public ActionResult FOOTER_Index()
        {
            return View();
        }

        public ActionResult SEO_Index()
        {
            return View();
        }
        #endregion VIEW

        #region THEME
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

        #endregion THEME

        #region NOTI
        [HttpPost]
        public JsonResult NOTI_Read([DataSourceRequest]DataSourceRequest request)
        {
            var dao = new NotificationDAO();
            IList<Shop_Nhi.Models.Framework.Notification> item = new List<Shop_Nhi.Models.Framework.Notification>();
            item = dao.GetListNotifications().Select(x => new Shop_Nhi.Models.Framework.Notification
            {
               ID = x.ID,
               createByID = x.createByID,
               createDate = x.createDate,
               detail = x.detail,
               satus = x.satus
            }).ToList();
            return Json(item.ToDataSourceResult(request));
        }

        [HttpPost]
        public JsonResult NOTI_ChangeStatus(int id)
        {
            try
            {
                new NotificationDAO().ChangeStatus(id);
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

        [HttpPost]
        public JsonResult NOTI_Save(string detail)
        {
            try
            {
                var dao = new NotificationDAO();               
                Shop_Nhi.Models.Framework.Notification noti = new Shop_Nhi.Models.Framework.Notification();
                noti.ID = 1;
                noti.detail = detail;
                noti.createByID = (string)Session["username"];
                noti.createDate = DateTime.Now;
                dao.Edit(noti);
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
        #endregion NOTI

        #region SLIDE
        public JsonResult SLIDE_Read([DataSourceRequest]DataSourceRequest request)
        {
            var dao = new SlideDAO();
            IList<Shop_Nhi.Models.Framework.Slide> item = new List<Shop_Nhi.Models.Framework.Slide>();
            item = dao.GetListSlide().Select(x => new Shop_Nhi.Models.Framework.Slide
            {
                ID = x.ID,
                image = x.image,
                detail = x.detail,
                name = x.name,
                dislayOrder = x.dislayOrder,
                status =x.status,
                title = x.title
            }).ToList();
            return Json(item.ToDataSourceResult(request));
        }

        [HttpPost]
        public JsonResult SLIDE_Get(int id)
        {
            var dao = new SlideDAO();
            var slide = new Slide();
            if (id == 0)
            {
                slide = new Slide();
            }
            else
            {
                var result = dao.GetById(id);
                slide.ID = result.ID;
                slide.name = result.name;
                slide.image = result.image;
                slide.title = result.title;
                slide.detail = result.detail;
                slide.dislayOrder = result.dislayOrder;
            }
            return Json(new
            {
                slide = slide
            });
        }
        [HttpPost]
        public JsonResult SLIDE_Save(string item)
        {
            try
            {
                var dao = new SlideDAO();
                JavaScriptSerializer seriaLizer = new JavaScriptSerializer();
                Slide slide = seriaLizer.Deserialize<Slide>(item);               
                dao.Save(slide);
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

        public JsonResult SLIDE_Delete(int id)
        {
            var result = new SlideDAO().Delete(id);
            return Json(new
            {
                status = result
            });
        }

        [HttpPost]
        public JsonResult SLIDE_ChangeStatus(int id)
        {
            try
            {
                var dao = new SlideDAO();
                dao.ChangeStatus(id);
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


        #endregion SLIDE

        #region FOOTER
        [HttpPost]
        public JsonResult FOOTER_Read([DataSourceRequest]DataSourceRequest request)
        {
            var dao = new FooterDAO();
            IList<Shop_Nhi.Models.Framework.Footer> item = new List<Shop_Nhi.Models.Framework.Footer>();
            item = dao.List().Select(x => new Shop_Nhi.Models.Framework.Footer
            {
                ID = x.ID,
                createByID = x.createByID,
                createDate = x.createDate,
                detail = x.detail,
                modifiedByDate = x.modifiedByDate,
                modifiedByID = x.modifiedByID,
                status = x.status
            }).ToList();
            return Json(item.ToDataSourceResult(request));
        }

        [HttpPost]
        public JsonResult FOOTER_ChangeStatus(int id)
        {
            try
            {
                new FooterDAO().ChangeStatus(id);
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

        [HttpPost]
        public JsonResult FOOTER_Save(string detail)
        {
            try
            {
                var dao = new FooterDAO();
                Footer footer = new Footer();
                footer.ID = 1;
                footer.detail = detail;
                footer.modifiedByID = (string)Session["username"];
                footer.createDate = DateTime.Now;
                dao.Edit(footer);
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

        #endregion FOOTER

        #region SEO
        [HttpPost]
        public JsonResult SEO_Read([DataSourceRequest]DataSourceRequest request)
        {
            var dao = new SeoDAO();
            IList<Seo> item = new List<Seo>();
            item = dao.List().Select(x => new Seo
            {
                ID = x.ID,
                metaTitle = x.metaTitle,
                metaDescription= x.metaDescription,
                metaKeyword = x.metaKeyword,
                status = x.status
            }).ToList();
            return Json(item.ToDataSourceResult(request));
        }

        [HttpPost]
        public JsonResult SEO_Get(int id)
        {
            var dao = new SeoDAO();
            var seo = new Seo();
            if (id == 0)
            {
                seo = new Seo();
            }
            else
            {
                var result = dao.GetContent(id);
                seo.ID = result.ID;
                seo.metaKeyword = result.metaKeyword;
                seo.metaDescription = result.metaDescription;
                seo.metaTitle = result.metaTitle;
            }
            return Json(new
            {
                seo = seo
            });
        }

        [HttpPost]
        public JsonResult SEO_Save(string item)
        {
            try
            {
                var dao = new SeoDAO();
                JavaScriptSerializer seriaLizer = new JavaScriptSerializer();
                Seo seo = seriaLizer.Deserialize<Seo>(item);
                dao.Edit(seo);
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
        #endregion SEO


    }
}