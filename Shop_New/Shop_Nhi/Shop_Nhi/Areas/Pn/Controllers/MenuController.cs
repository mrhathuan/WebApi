using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Shop_Nhi.Models.DAO;
using System;
using Shop_Nhi.Models.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Shop_Nhi.Common;

namespace Shop_Nhi.Areas.Pn.Controllers
{
    public class MenuController : BaseController
    {
        // GET: Pn/Menu
        [Authorize(Roles = "ADMIN,MANAGE")]
        public ActionResult MENU_Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult MENU_Read([DataSourceRequest]DataSourceRequest request)
        {
            var dao = new MenuDAO();
            IList<Shop_Nhi.Models.Framework.Menu> item = new List<Shop_Nhi.Models.Framework.Menu>();
            item = dao.List().Select(x => new Shop_Nhi.Models.Framework.Menu
            {
                ID = x.ID,
                dislayOrder = x.dislayOrder,
                link = x.link,
                Name = x.Name,
                taget = x.taget,
                typeID = x.typeID,
                MenuType = new MenuType
                {
                    ID = x.MenuType.ID,
                    name = x.MenuType.name
                },
                status = x.status
            }).ToList();
            return Json(item.ToDataSourceResult(request));
        }

        [HttpPost]
        public JsonResult MENU_Get(long id)
        {
            var dao = new MenuDAO();
            var menu = new Shop_Nhi.Models.Framework.Menu();
            if (id == 0)
            {
                menu = new Shop_Nhi.Models.Framework.Menu();
            }
            else
            {
                var result = dao.GetByID(id);
                menu.ID = result.ID;
                menu.Name = result.Name;
                menu.typeID = result.typeID;
                menu.dislayOrder = result.dislayOrder;                                
            }
            return Json(new
            {
                menu = menu
            });
        }

        [HttpGet]
        public ActionResult GET_Type()
        {
            var dao = new MenuDAO();
            var result = dao.GetMenuType().Select(x => new MenuType
            {
                ID = x.ID,
                name = x.name
            }).ToList();
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult MENU_Save(string item)
        {
            try
            {
                var dao = new MenuDAO(); 
                JavaScriptSerializer seriaLizer = new JavaScriptSerializer();
                Shop_Nhi.Models.Framework.Menu menu = seriaLizer.Deserialize<Shop_Nhi.Models.Framework.Menu>(item);
                string link = StringHelper.RemoveSpecialChars(menu.Name.Trim()).Replace(" ", "-");
                if (dao.CheckLink(link.ToLower()))
                    throw new Exception("MENU NÀY ĐÃ TỒN TẠI.KHÔNG THỂ LƯU.");                
                dao.Save(menu);
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

        public JsonResult MENU_Delete(long id)
        {
            new MenuDAO().Delete(id);
            return Json(new
            {
                status = true
            });
        }

        [HttpPost]
        public JsonResult MENU_ChangeStatus(long id)
        {
            try
            {
                var dao = new MenuDAO();
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

    }
}