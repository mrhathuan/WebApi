
using Shop_Nhi.Models.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Shop_Nhi.Common;
using System.Text.RegularExpressions;

namespace Shop_Nhi.Controllers
{
    public class LoginController : Controller
    {
        // GET: Thuankay/Login
        [HttpPost]
        public ActionResult Index(string username, string password)
        {
            ViewBag.Error = null;
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                ViewBag.Error = "Bạn chưa nhập tài khoản";
            }
            else
            {
                var dao = new UserDAO();
                var result = dao.Login(username, password);
                if (result == 1)
                {
                    var user = dao.GetByUsername(username);
                    Session["username"] = user.userName;
                    return Redirect("/");
                }
                else if (result == 0)
                {
                    ViewBag.Error = "Tài khoản không tồn tại.";
                }
                else if (result == -1)
                {
                    ViewBag.Error = "Tài khoản đang bị khoá.";
                }
                else if (result == -2)
                {
                    ViewBag.Error = "Mật khẩu không đúng.";
                }
            }
          
            return null;
        }


        //Log out
        public ActionResult Logout()
        {
            Session["username"] = null;
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

