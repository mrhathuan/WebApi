using Shop_Nhi.Models.DAO;
using Shop_Nhi.Models.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Shop_Nhi.Areas.Pn.Controllers
{
    public class PayController : BaseController
    {
        //
        // GET: /Pn/Pay/
       public ActionResult Index()
        {
            var dao = new PayDAO();
            return View(dao.ListAll());
        }


       public ActionResult Edit(int id)
       {
           var dao = new PayDAO();
           return View(dao.GetByID(id));
       }

       [HttpPost]
       [ValidateInput(false)]
       public ActionResult Edit(Pay pay)
       {
           try
           {
               var dao = new PayDAO();
               dao.Edit(pay);
               SetAlert("Thành công", "success");
               return RedirectToAction("Index", "Footer");
           }
           catch
           {
               SetAlert("Thất bại", "error");
               return View();
           }
       }

        [HttpGet]
        public ActionResult Create()
        {
           
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(Pay pay)
        {
            try
            {
                var dao = new PayDAO();
                dao.Create(pay);
                SetAlert("Thành công", "success");
                return RedirectToAction("Index", "Footer");
            }
            catch
            {
                SetAlert("Thất bại", "error");
                return View();
            }
        }

        public ActionResult Delete(int id)
        {
            var result = new PayDAO().Delete(id);
            if (result)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return HttpNotFound();
            }
            
        }
	}
}