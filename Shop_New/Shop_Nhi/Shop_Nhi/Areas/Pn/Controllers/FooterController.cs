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
    public class FooterController : BaseController
    {
        //
        // GET: /Pn/Footer/
        public ActionResult Index()
        {
            var dao = new FooterDAO();
            return View(dao.List());
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var dao = new FooterDAO();
            return View(dao.GetByID(id));
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(Footer footer)
        {
            try
            {
                var dao = new FooterDAO();
                dao.Edit(footer);
                SetAlert("Thành công", "success");
                return RedirectToAction("Index", "Footer");
            }
            catch
            {
                SetAlert("Thất bại", "error");
                return View();
            }
        }
	}
}