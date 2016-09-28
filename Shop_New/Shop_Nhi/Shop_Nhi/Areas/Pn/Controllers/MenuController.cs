using Shop_Nhi.Common;
using Shop_Nhi.Models.DAO;
using Shop_Nhi.Models.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Shop_Nhi.Areas.Pn.Controllers
{
    [Authorize(Roles = "ADMIN")]
    public class MenuController : BaseController
    {
        //
        // GET: /Pn/Menu/
        public ActionResult Index()
        {
            var dao = new MenuDAO();
            return View(dao.List());
        }

        [HttpGet]
        public ActionResult Create()
        {
            GetType();
            return View();
        }

        [HttpPost]
        public ActionResult Create(Menu menu)
        {

            try
            {
                var dao = new MenuDAO();
                var pageDao = new PageBodyDAO();
                string link = StringHelper.RemoveSpecialChars(menu.Name.Trim()).Replace(" ","-"); 
                if (dao.CheckLink(link.ToLower()))
                {
                    SetAlert("Menu này đã tồn tại", "error");
                    GetType();
                    return View();
                }
                else
                {
                    menu.status = true;
                    menu.link = link.ToLower();
                    int idMenu = dao.Create(menu);
                    var page = new PageBody();
                    page.metatTitle = link;
                    page.createDate = DateTime.Now;
                    page.createByID = (string)Session["username"];
                    page.menuID = idMenu;
                    pageDao.Create(page);
                    SetAlert("Thêm thành công.", "success");
                    return RedirectToAction("Index", "Menu");
                }                
            }
            catch
            {
                GetType();
                return View();
            }
        }


        // GET: Thuankay/s/Edit/5
        [HttpGet]
        public ActionResult Edit(long id)
        {
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var menu = new MenuDAO();

            var result = menu.GetByID(id);
            if (result == null)
            {
                return HttpNotFound();
            }        
            GetType(result.typeID);
            return View(result);
        }

      
        [HttpPost]
        public ActionResult Edit(Menu menu)
        {
            var dao = new MenuDAO();
            string link = StringHelper.RemoveSpecialChars(menu.Name.Trim()).Replace(" ", "-"); 
            if (dao.CheckLink(link.ToLower()))
            {
                SetAlert("Menu này đã tồn tại", "error");
                GetType(menu.typeID);
                return View();
            }
            else
            {
                menu.link = link.ToLower();
                var result = dao.Edit(menu);
                if (result)
                {
                    SetAlert("Sửa thành công", "success");
                    return RedirectToAction("Index");
                }
                else
                {
                    SetAlert("Sửa thất bại", "error");
                    GetType(menu.typeID);
                    return View();
                }
            }
        }

        // GET: Thuankay/s/Delete/5
        public ActionResult Delete(long id)
        {
            try
            {
                var dao = new MenuDAO();
                dao.Delete(id);
                SetAlert("Xóa thành công", "success");
                return RedirectToAction("Index");
            }
            catch
            {
                return HttpNotFound();
            }
        }

      
        //change status
        public JsonResult ChangeStatus(long id)
        {
            var result = new MenuDAO().ChangeStatus(id);
            return Json(new
            {
                status = result
            });
        }


        public void GetType(int? selectedId = null)
        {
            var dao = new MenuDAO();
            ViewBag.TypeID = new SelectList(dao.GetMenuType(), "ID", "name", selectedId);
        }
	}
}