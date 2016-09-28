using Shop_Nhi.Models.DAO;
using Shop_Nhi.Models.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Shop_Nhi.Areas.Pn.Controllers
{
    public class SlideController : BaseController
    {
        // GET: Thuankay/Slide
        public ActionResult Index()
        {
            var dao = new SlideDAO();
            return View(dao.GetListSlide());
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Slide slide)
        {
            try
            {
                var dao = new SlideDAO();
                slide.status = true;             
                dao.Create(slide);
                SetAlert("Thêm mới thành công", "success");
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }


        //Sửa
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var dao = new SlideDAO();
            return View(dao.GetSlideById(id));
        }

        [HttpPost]
        public ActionResult Edit(Slide slide)
        {
            var dao = new SlideDAO();
            var result = dao.Edit(slide);
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
            var result = new SlideDAO().ChangeStatus(id);
            return Json(new
            {
                status = result
            });
        }
    }
}