using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc;
using Shop_Nhi.Models.Framework;
using Shop_Nhi.Models.DAO;
using Shop_Nhi.Common;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using System.Web.Script.Serialization;

namespace Shop_Nhi.Areas.Pn.Controllers
{

    [Authorize(Roles="ADMIN")]
    public class UsersController : BaseController
    {

        // GET: Thuankay/Users
        public ActionResult USER_Index()
        {            
            return View();
        }

        public ActionResult USER_ChangePasss()
        {
            return View();
        }

        [HttpPost]
        public JsonResult USER_Read([DataSourceRequest]DataSourceRequest request)
        {
            var dao = new UserDAO();
            IList<User> item = new List<User>();
            item = dao.ListUsers().Select(x => new User
            {
                ID = x.ID,
                userName= x.userName,
                fullname = x.fullname,
                email = x.email,
                roleID = x.roleID,
                Role = new Role
                {
                    ID = x.Role.ID,
                    Name = x.Role.Name
                },
                status = x.status
            }).ToList();
            return Json(item.ToDataSourceResult(request));
        }

        [HttpPost]
        public JsonResult USER_Get(long id)
        {
            var dao = new UserDAO();
            var user = new User();
            if (id == 0)
            {
                user = new User();
            }
            else
            {
                var result = dao.GetByID(id);
                user.ID = result.ID;
                user.email = result.email;
                user.userName = result.userName;
                user.password = result.password;
                user.fullname = result.fullname;
                user.roleID = result.roleID;               
            }
            return Json(new
            {
                user = user
            });
        }
        
        [HttpPost]
        public JsonResult USER_Save(string item)
        {
            try
            {
                var dao = new UserDAO();
                JavaScriptSerializer seriaLizer = new JavaScriptSerializer();
                User user = seriaLizer.Deserialize<User>(item);
                if(user.ID > 0)
                {
                    user.userName = "";
                    user.email = "";
                }
                if (dao.CheckUsername(user.userName.Trim()))
                    throw new Exception("TÀI KHOẢN ĐÃ TỒN TẠI.KHÔNG THỂ LƯU.");
                if (dao.CheckEmail(user.email.Trim()))
                    throw new Exception("EMAIL ĐÃ TỒN TẠI.KHÔNG THỂ LƯU.");                                        
                dao.Save(user);
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

        [HttpGet]
        public ActionResult GET_Role()
        {
            var dao = new UserDAO();
            var result = dao.ListRole().Select(x => new Role
            {
                ID = x.ID,
                Name = x.Name
            }).ToList();
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult USER_Delete(long id)
        {
            var result = new UserDAO().Delete(id);
            return Json(new
            {
                status = result
            });

        }

        [HttpPost]
        public JsonResult USER_ChangeStatus(long id)
        {
            var result = new UserDAO().ChangeStatus(id);
            return Json(new
            {
                status = result
            });
        }
        
        [HttpPost]
        public JsonResult ChangePassword(string password,string renewPassword)
        {
            try
            {                
                var dao = new UserDAO();
                if (!dao.ChekcPassword(Encryptor.MD5Hash(password)))
                    throw new Exception("MẬT KHẨU KHÔNG ĐÚNG.");
                string username = null;
                if (Session["username"] != null)
                {
                    username = (string)Session["username"];
                }
                var user = dao.GetByUsername(username);
                user.password = Encryptor.MD5Hash(renewPassword.Replace(" ","").Trim());
                var result = dao.ChangePassword(user);
                return Json(new
                {
                    msg = "Thành công",
                    status = result
                });
            }
            catch(Exception e)
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
