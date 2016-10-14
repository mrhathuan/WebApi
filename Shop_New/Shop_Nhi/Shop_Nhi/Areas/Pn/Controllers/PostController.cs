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
    public class PostController : BaseController
    {
        // GET: Pn/Post
        [Authorize(Roles = "ADMIN,MANAGE")]
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
                post.tag = result.tag;               
            }
            return Json(new
            {
                data = post
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
                if(post.ID == 0)
                {
                    post.createByID = (string)Session["username"];
                }else
                {
                    post.modifiedByID = (string)Session["username"];
                }                             
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

        public JsonResult POST_Delete(long id)
        {
            var result = new PostDAO().Delete(id);
            return Json(new
            {
                status = result         
            });
        }

        [HttpPost]
        public JsonResult POST_ChangeStatus(long id)
        {
            try
            {
                var dao = new PostDAO();
                dao.ChangeStatus(id);
                return Json(new
                {
                    status = true
                });
            }
            catch
            {
                return Json(new
                {
                    status = false
                });
            }
        }
    }
}