using Shop_Nhi.Models.DAO;
using Shop_Nhi.Models.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Shop_Nhi.Areas.Pn.Controllers
{
    [Authorize(Roles = "ADMIN")]
    public class ContactsController : BaseController
    {
        // GET: Thuankay/Contacts
        public ActionResult Index()
        {
            var dao = new ContactDAO();
            return View(dao.List());
        }


        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        
        //Sửa
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var dao = new ContactDAO();
            return View(dao.GetByID(id));
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(Contact contact)
        {
            var dao = new ContactDAO();
            contact.modifiedByID = (string)Session["username"];
            contact.modifiedByDate = DateTime.Now;
            var result = dao.Edit(contact);
            if (result)
            {
                SetAlert("Sửa nội dung thành công", "success");
                return RedirectToAction("Index");
            }
            else
            {
                return HttpNotFound();
            }
        }

        public JsonResult ChangeStatus(int id)
        {
            var result = new ContactDAO().ChangeStatus(id);
            return Json(new
            {
                status = result
            });
        }

    }
}