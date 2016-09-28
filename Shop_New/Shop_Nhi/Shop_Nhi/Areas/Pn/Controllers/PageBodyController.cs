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
    public class PageBodyController : BaseController
    {
        //
        // GET: /Pn/PageBody/
        public ActionResult Index()
        {
            var dao = new PageBodyDAO();
            return View(dao.List());
        }



        // GET: Thuankay/s/Edit/5
        [HttpGet]
        public ActionResult Edit(long id)
        {
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var PageBody = new PageBodyDAO();

            var result = PageBody.GetByID(id);
            if (result == null)
            {
                return HttpNotFound();
            }
            GetType(result.ID);
            return View(result);
        }


        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(PageBody page)
        {
            try
            {
                var dao = new PageBodyDAO();
                page.modifiedByID = (string)Session["username"];
                var result = dao.Edit(page);
                if (result)
                {
                    SetAlert("Sửa thành công", "success");
                    return RedirectToAction("Index");
                }
                else
                {
                    SetAlert("Sửa thất bại", "error");
                    GetType(page.ID);
                    return View();
                }
            }
            catch
            {
                SetAlert("Sửa thất bại", "error");
                GetType(page.ID);
                return View();
            }
           
        }

        //change status
        public JsonResult ChangeStatus(long id)
        {
            var result = new PageBodyDAO().ChangeStatus(id);
            return Json(new
            {
                status = result
            });
        }


        public void GetType(int? selectedId = null)
        {
            var dao = new MenuDAO();
            ViewBag.MenuID = new SelectList(dao.List(), "ID", "Name", selectedId);
        }
    }
}