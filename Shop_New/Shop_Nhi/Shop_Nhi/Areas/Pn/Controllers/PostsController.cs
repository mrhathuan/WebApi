using log4net;
using Shop_Nhi.Common;
using Shop_Nhi.Models.DAO;
using Shop_Nhi.Models.Framework;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace Shop_Nhi.Areas.Pn.Controllers
{

    [Authorize(Roles = "ADMIN,MANAGE")]
    public class PostsController : BaseController
    {
        // GET: Pn/Posts
        private static readonly ILog logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        public ActionResult Index()
        {
            var dao = new PostDAO();
            return View(dao.List());
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="post"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(Post post)
        {
            try
            {
                var dao = new PostDAO();
                var metaTitle = StringHelper.RemoveSpecialChars(post.name.Trim()).Replace(" ", "-"); 
                post.metatTitle = metaTitle.ToLower();
                post.status = true;
                post.createDate = DateTime.Now.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                post.createByID = (string)Session["username"];
                var id = dao.Create(post);
                //Xử lý tag
                if (!string.IsNullOrEmpty(post.tag))
                {
                    string[] tags = post.tag.Trim().Split(',');
                    foreach (var tag in tags)
                    {
                        var tagId = StringHelper.RemoveSpecialChars(tag.Trim()).Replace(" ","-");
                        var existedTag = dao.CheckTag(tagId);

                      
                        if (!existedTag)
                        {
                            dao.InsertTag(tagId, tag);
                        }
                      
                        dao.InsertContentTag(id, tagId);
                    }
                }
                SetAlert("Thêm bài viết thành công", "success");
                return RedirectToAction("Index");                  
            }
            catch(Exception e)
            {
                SetAlert(e.Message, "error");
                logger.Error(e);
                return View();
            }
        }


        // GET: Thuankay/s/Edit/5
        [HttpGet]        
        public ActionResult Edit(long id)
        {
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var post = new PostDAO();

            var result = post.GetByID(id);
            if (result == null)
            {
                return HttpNotFound();
            }
            
            return View(result);
        }


        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(Post post)
        {
            try
            {
                var dao = new PostDAO();
                var metaTitle = StringHelper.RemoveSpecialChars(post.name.Trim()).Replace(" ", "-"); 
                post.metatTitle = metaTitle.ToLower();
                post.modifiedByID = (string)Session["username"];
                dao.Edit(post);
                if(!string.IsNullOrEmpty(post.tag))
                {
                    dao.RemoveAllContentTag(post.ID);
                    string[] tags = post.tag.Trim().Split(',');
                    foreach (var tag in tags)
                    {
                        var tagId = StringHelper.RemoveSpecialChars(tag.Trim()).Replace(" ", "-");
                        var existedTag = dao.CheckTag(tagId);
                        //insert to to tag table
                        if (!existedTag)
                        {
                            dao.InsertTag(tagId, tag);
                        }
                        //insert to content tag
                        dao.InsertContentTag(post.ID, tagId);

                    }
                }
                SetAlert("Sửa nội dung thành công", "success");
                return RedirectToAction("Index");
            }
            catch(Exception e)
            {
                SetAlert(e.Message, "error");
                logger.Error(e);
                return View();
            }
        }

        // GET: Thuankay/s/Delete/5
        public ActionResult Delete(long id)
        {
            try
            {
                var dao = new PostDAO();
                dao.Delete(id);
                SetAlert("Xóa thành công", "success");
                return RedirectToAction("Index");
            }
            catch(Exception e)
            {
                logger.Error(e);
                return HttpNotFound();
            }
        }


        //change status
        public JsonResult ChangeStatus(long id)
        {
            var result = new PostDAO().ChangeStatus(id);
            return Json(new
            {
                status = result
            });
        }
   

    }
}