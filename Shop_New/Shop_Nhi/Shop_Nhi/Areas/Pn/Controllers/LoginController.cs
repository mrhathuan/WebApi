using Shop_Nhi.Areas.Pn.Models;
using Shop_Nhi.Models.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.Mvc;
using Shop_Nhi.Common;
using System.Text.RegularExpressions;

namespace Shop_Nhi.Areas.Pn.Controllers
{
    public class LoginController : Controller
    {
        // GET: Thuankay/Login

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var dao = new UserDAO();
                var result = dao.Login(model.UserName, Encryptor.MD5Hash(model.Password));
                if (result == 1)
                {
                    var user = dao.GetByUsername(model.UserName);
                    Session["username"] = user.userName;
                    FormsAuthentication.SetAuthCookie(user.userName, false);
                    return Redirect("/Pn");
                }
                else if (result == 0)
                {
                    ModelState.AddModelError("", "Tài khoản không tồn tại.");
                }
                else if (result == -1)
                {
                    ModelState.AddModelError("", "Tài khoản đang bị khoá.");
                }
                else if (result == -2)
                {
                    ModelState.AddModelError("", "Mật khẩu không đúng.");
                }
            }
            return View(model);
        }

        //Login/Get
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        //error
        public ActionResult Error()
        {
            return View();
        }

        //Log out
        public ActionResult Logout()
        {
            Session["username"] = null;
            FormsAuthentication.SignOut();//gan quyen = null
            return RedirectToAction("Index", "Login");
        }

        //Quên mật khẩu

        [HttpGet]
        public ActionResult Quenmatkhau()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Quenmatkhau(string email)
        {

            Regex rg = new Regex(@"^[-!#$%&'*+/0-9=?A-Z^_a-z{|}~](\.?[-!#$%&'*+/0-9=?A-Z^_a-z{|}~])*@[a-zA-Z](-?[a-zA-Z0-9])*(\.[a-zA-Z](-?[a-zA-Z0-9])*)+$");
            ViewBag.Error = null;
            var dao = new UserDAO();
            if (string.IsNullOrEmpty(email))
            {
                ViewBag.Error = "Vui lòng điền đầy đủ thông tin";
            }
            else
            {
                if (!rg.IsMatch(email))
                {
                    ViewBag.Error = "Địa chỉ email không hợp lệ";
                }
                else
                {
                    if (!dao.CheckEmail(email))
                    {
                        ViewBag.Error = "Email chưa được sử dụng";
                    }
                    else
                    {
                        var user = dao.GetByEmail(email);
                        var password = StringHelper.CreateLostPassword(7);
                        string body = "<p>Tài khoản của bạn:<p>" + user.userName + "</br>" +
                                        "<p>Mật khẩu mới:" + password + "</br>" +
                                        "<p>Thanks you!<p>";
                        new Mail().SendMail(email, "Thông tin gửi từ shop", body);
                        user.password = Encryptor.MD5Hash(password);
                        var result = dao.ChangePassword(user);
                        if (result)
                        {
                            return Redirect("/dang-nhap");
                        }
                        else
                        {
                            return HttpNotFound();
                        }
                    }
                }
            }
            return View();
        }
    }
}

