using Kendo.Mvc.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Shop_Nhi.Models.DAO;
using Shop_Nhi.Models.Framework;
using Kendo.Mvc.Extensions;

namespace Shop_Nhi.Areas.Pn.Controllers
{
    public class PageController : BaseController
    {
        // GET: Pn/Page
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



    }
}