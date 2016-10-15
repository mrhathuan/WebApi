using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Shop_Nhi.Models.DAO;
using Shop_Nhi.Models.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace Shop_Nhi.Areas.Pn.Controllers
{
    public class ContactController : BaseController
    {
        // GET: Pn/Contact
        [Authorize(Roles = "ADMIN,MANAGE")]
        public ActionResult CONTACT_Index()
        {
            return View();
        }
        [HttpPost]
        public JsonResult CONTACT_Read([DataSourceRequest]DataSourceRequest request)
        {
            var dao = new ContactDAO();
            IList<Contact> item = new List<Contact>();
            item = dao.List().Select(x => new Contact
            {
                ID = x.ID,
                map = x.map,
                createByID = x.createByID,
                createDate = x.createDate,
                name = x.name,
                metatTitle = x.metatTitle,
                detail = x.detail,               
                modifiedByDate = x.modifiedByDate,
                modifiedByID = x.modifiedByID,
                status = x.status
            }).ToList();
            return Json(item.ToDataSourceResult(request));
        }

        [HttpPost]
        public JsonResult CONTACT_Get(long id)
        {
            var dao = new ContactDAO();
            var contact = new Contact();
            if (id == 0)
            {
                contact = new Contact();
            }
            else
            {
                var result = dao.GetByID(id);
                contact.ID = result.ID;
                contact.name = result.name;
                contact.createByID = result.createByID;
                contact.detail = result.detail;
                contact.map = result.map;
                contact.metatTitle = result.metatTitle;
                contact.metaKeywords = result.metaKeywords;
                contact.metaDescription = result.metaDescription;
            }
            return Json(new
            {
                contact = contact
            });
        }

        [HttpPost]
        public JsonResult CONTACT_Save(string item)
        {
            try
            {
                var dao = new ContactDAO();
                JavaScriptSerializer seriaLizer = new JavaScriptSerializer();
                Contact contact = seriaLizer.Deserialize<Contact>(item);
                if (contact.ID == 0)
                {
                    contact.createByID = (string)Session["username"];
                }
                else
                {
                    contact.modifiedByID = (string)Session["username"];
                }
                dao.Save(contact);
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

        public JsonResult CONTACT_Delete(long id)
        {
            var result = new ContactDAO().Delete(id);
            return Json(new
            {
                status = result
            });
        }

        [HttpPost]
        public JsonResult CONTACT_ChangeStatus(long id)
        {
            try
            {
                var dao = new ContactDAO();
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