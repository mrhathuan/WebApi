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
    public class NotificationsController : BaseController
    {
        // GET: Thuankay/Notifications
        public ActionResult Index()
        {
            var dao = new NotificationDAO();
            return View(dao.GetListNotifications());
        }


        //Sửa
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var dao = new NotificationDAO();
            return View(dao.GetNotificationById(id));
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(Notification notification)
        {
            var dao = new NotificationDAO();
            var result = dao.Edit(notification);
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
            var result = new NotificationDAO().ChangeStatus(id);
            return Json(new
            {
                status = result
            });
        }

    }
}