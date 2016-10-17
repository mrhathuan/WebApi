using Kendo.Mvc.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Shop_Nhi.Models.DAO;
using Shop_Nhi.Models.Framework;
using Kendo.Mvc.Extensions;
using System.Web.Script.Serialization;
using Shop_Nhi.Common;

namespace Shop_Nhi.Areas.Pn.Controllers
{
    public class PageController : BaseController
    {
        // GET: Pn/Page
        [Authorize(Roles = "ADMIN")]
        public ActionResult PAGE_Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult PAGE_Read([DataSourceRequest]DataSourceRequest request)
        {
            var dao = new PageBodyDAO();
            IList<PageBody> item = new List<PageBody>();
            item = dao.List().Select(x => new PageBody
            {
                ID = x.ID,
                createByID = x.createByID,
                modifiedByDate = x.modifiedByDate,
                createDate = x.createDate,
                menuID = x.menuID,
                Menu = new Shop_Nhi.Models.Framework.Menu
                {
                    ID = x.Menu.ID,
                    Name = x.Menu.Name
                },
                modifiedByID = x.modifiedByID,
                status = x.status
            }).ToList();
            return Json(item.ToDataSourceResult(request));
        }
        [HttpPost]
        public JsonResult PAGE_Get(long id)
        {
            var dao = new PageBodyDAO();
            var page = new PageBody();
            if (id == 0)
            {
                page = new PageBody();
            }
            else
            {
                var result = dao.GetByID(id);
                page.ID = result.ID;
                page.detail = result.detail;
                page.metaDescription = result.metaDescription;
                page.metaKeywords = result.metaKeywords;
            }
            return Json(new
            {
                page = page
            });
        }

        [HttpPost]
        public JsonResult PAGE_Save(string item)
        {
            try
            {
                var dao = new PageBodyDAO();
                JavaScriptSerializer seriaLizer = new JavaScriptSerializer();
                PageBody page = seriaLizer.Deserialize<PageBody>(item);
                page.modifiedByID = (string)Session["username"];              
                dao.Edit(page);               
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


    }
}