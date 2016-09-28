using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Shop_Nhi.Models.Framework;
using Shop_Nhi.Models.DAO;
using Shop_Nhi.Common;

namespace Shop_Nhi.Areas.Pn.Controllers
{

    [Authorize(Roles="ADMIN")]
    public class UsersController : BaseController
    {

        // GET: Thuankay/Users
        public ActionResult Index()
        {
            var users = new UserDAO();
            return View(users.ListUsers());
        }

        // GET: Thuankay/Users/Details/5


        // GET: Thuankay/Users/Create
        [HttpGet]
        public ActionResult Create()
        {
            GetRole();
            return View();
        }

        // POST: Thuankay/Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Create(User user, string password2)
        {
            var dao = new UserDAO();
            if (dao.CheckUsername(user.userName))
            {
                SetAlert("Thêm thất bại! Tài khoản đã tồn tại", "error");
            }
            else
            {
                if (dao.CheckEmail(user.email))
                {
                    SetAlert("Thêm thất bại! Email đã tồn tại", "error");
                }
                else
                {
                    user.status = true;
                    user.password = Encryptor.MD5Hash(password2.Replace(" ", ""));
                    dao.Create(user);
                    SetAlert("Tạo mới tài khoản thành công", "success");
                    return RedirectToAction("Index");
                }
            }
            GetRole();
            return View();
        }

        // GET: Thuankay/Users/Edit/5
        [HttpGet]
        public ActionResult Edit(long id)
        {
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var user = new UserDAO();
            var result = user.GetByID(id);
            if (result == null)
            {
                return HttpNotFound();
            }
            ViewBag.Username = user.GetByID(id).userName;
            GetRole();
            return View(result);
        }

        // POST: Thuankay/Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Edit(User user)
        {
            var dao = new UserDAO();
            var result = dao.Edit(user);
            if (result)
            {
                SetAlert("Sửa thông tin tài khoản thành công", "success");
                return RedirectToAction("Index");
            }
            else
            {
                SetAlert("Sửa thông tin tài khoản thất bại", "error");
                return View();
            }

        }

        // GET: Thuankay/Users/Delete/5
        public ActionResult Delete(long id)
        {
            try
            {
                var dao = new UserDAO();
                dao.Delete(id);
                SetAlert("Xóa thành công một tài khoản", "success");
                return RedirectToAction("Index");
            }
            catch
            {
                return HttpNotFound();
            }
        }

        //Change Password

        [HttpGet]
        public ActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ChangePassword(string password, string password3)
        {
            string username = null;
            if (Session["username"] != null)
            {
                username = (string)Session["username"];
            }
            var dao = new UserDAO();
            var user = dao.GetByUsername(username);
            if (!dao.ChekcPassword(Encryptor.MD5Hash(password)))
            {
                SetAlert("Mật khẩu không đúng", "error");
            }
            else
            {
                user.password = Encryptor.MD5Hash(password3.Replace(" ", ""));
                var result = dao.ChangePassword(user);
                if (result)
                {
                    SetAlert("Đổi mật khẩu thành công", "success");
                    return Redirect("/Pn");
                }
                else
                {
                    return HttpNotFound();
                }
            }
            return View();
        }

        //change status
        public JsonResult ChangeStatus(long id)
        {
            var result = new UserDAO().ChangeStatus(id);
            return Json(new
            {
                status = result
            });
        }

        //Get ROLE
        public void GetRole(int? selectedId = null)
        {
            var dao = new UserDAO();
            ViewBag.RoleID = new SelectList(dao.ListRole(), "ID", "Name", selectedId);
        }

    }
}
