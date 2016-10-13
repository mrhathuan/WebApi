using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Kendo.Mvc.UI;
using System.Web.Mvc;
using Shop_Nhi.Models.DAO;
using Shop_Nhi.Models.Framework;
using Kendo.Mvc.Extensions;
using System.Web.Script.Serialization;

namespace Shop_Nhi.Areas.Pn.Controllers
{
    public class PostController : Controller
    {
        // GET: Pn/Post
        public ActionResult POST_Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult POST_Read([DataSourceRequest]DataSourceRequest request)
        {
            var dao = new PostDAO();
            IList<Post> item = new List<Post>();
            item = dao.List().Select(x => new Post
            {
                ID = x.ID,
                image = x.image,
                createByID = x.createByID,
                createDate = x.createDate,
                name = x.name,
                tag = x.tag,
                modifiedByDate = x.modifiedByDate,
                modifiedByID = x.modifiedByID,
                status = x.status
            }).ToList();
            return Json(item.ToDataSourceResult(request));
        }

        [HttpPost]
        public JsonResult POST_Get(long id)
        {
            var dao = new PostDAO();
            var post = new Post();
            if (id == 0)
            {
                post = new Post();
            }else
            {
                var result = dao.GetByID(id);
                post.ID = result.ID;
                post.name = result.name;
                post.image = result.image;
                post.createByID = result.createByID;
                post.detail = result.detail;
                post.description = result.description;                
                post.metaKeywords = result.metaKeywords;
                post.metaDescription = result.metaDescription;
                post.status = post.status;
                post.tag = post.tag;               
            }
            return Json(new
            {
                post = post
            });
        }

        [HttpPost]
        public JsonResult POST_Save(string item)
        {
            try
            {
                var dao = new PostDAO();
                JavaScriptSerializer seriaLizer = new JavaScriptSerializer();
                Post post = seriaLizer.Deserialize<Post>(item);                             
                dao.Save(post);
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
    }
}