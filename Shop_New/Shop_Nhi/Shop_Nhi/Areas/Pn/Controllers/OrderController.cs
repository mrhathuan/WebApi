using log4net;
using Shop_Nhi.Models.DAO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace Shop_Nhi.Areas.Pn.Controllers
{
   
    public class OrderController : BaseController
    {
        private static readonly ILog logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        // GET: Thuankay/Order

         [Authorize(Roles = "ADMIN,MANAGE")]
        public ActionResult Index()
        {
            var dao = new OrderDAO();
            return View(dao.ListOrder());
        }

        // GET: Thuankay/Order/Details/5

        [Authorize(Roles = "ADMIN,MANAGE")]
        public ActionResult Details(long id)
        {
            var dao = new OrderDAO();
            dao.ChangeStatus(id);
            ViewBag.OrderDetails = new OrderDetailsDAO().GetById(id);
            return View(dao.GetByID(id));
        }

        // GET: Thuankay/Order/Create

        // GET: Thuankay/Order/Delete/5
         [Authorize(Roles = "ADMIN")]
        public ActionResult Delete(long id)
        {
            try
            {
                var dao = new OrderDAO();
                dao.Delete(id);
                return RedirectToAction("Index", "Order");
            }
            catch(Exception e)
            {
                SetAlert("Xóa thất bại", "error");
                logger.Error(e);
                return RedirectToAction("Index", "Order");
            }
        }

        //Change Status
        public ActionResult ChangeStatus(long id)
        {
            var dao = new OrderDAO();
            dao.ChangeStatus(id);
            return RedirectToAction("Index");
        }

        public JsonResult ChangePayment(long id)
        {
            var dao = new OrderDAO();
            var result = dao.ChangePayment(id);
            return Json(new
            {
                status = result
            });
        }

       

        //Export
        //public ActionResult ExportPdf(long id)
        //{
        //    var orderDetails = new OrderDetailsDAO().GetById(id);
        //    var od = new OrderDAO().GetByID(id);
        //    ReportDocument rd = new ReportDocument();
        //    DataSet1 ds = new DataSet1();
        //    rd.Load(Path.Combine(Server.MapPath("~/Reports/Donhang.rpt")));           
        //    rd.SetDataSource(orderDetails.Select(order => new {
        //        ID = order.orderID,
        //        productName = order.productName,
        //        price = order.price.Value,
        //        quantity = order.quantity.Value,
        //        thanhtien = order.totalAmount.Value
        //    }).ToList());
        //    Response.Buffer = false;
        //    Response.ClearContent();
        //    Response.ClearHeaders();
        //    Stream s = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
        //    s.Seek(0, SeekOrigin.Begin);
        //    return File(s, "application/pdf","Donhang.pdf");
        //}
    }
}
