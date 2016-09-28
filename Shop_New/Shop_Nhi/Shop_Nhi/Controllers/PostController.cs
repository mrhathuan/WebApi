using Shop_Nhi.Models.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Shop_Nhi.Controllers
{
    public class PostController : Controller
    {
        // GET: Post
        public ActionResult Index()
        {
            var dao = new PostDAO();
            return View(dao.List());
        }

        public ActionResult Detail(long id)
        {
            var dao = new PostDAO();
            ViewBag.Tags = dao.ListTag(id);
            return View(dao.GetByID(id));
        }

        public ActionResult Tag(string tagId)
        {
            var dao = new PostDAO();
            ViewBag.Tag = dao.GetTag(tagId);
            return View(dao.ListAllByTag(tagId));
        }
    }
}