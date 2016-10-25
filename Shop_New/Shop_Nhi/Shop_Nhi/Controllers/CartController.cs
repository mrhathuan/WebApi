using Shop_Nhi.Models;
using Shop_Nhi.Models.DAO;
using Shop_Nhi.Models.Framework;
using Shop_Nhi.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Shop_Nhi.Controllers
{

    public class CartController : Controller
    {
        // GET: Cart

        public static String CODE = null;

        #region cart
        public ActionResult Index()
        {
            var cart = Session["CartSession"];
            var cartItem = new List<CartItem>();
            if (cart != null)
            {
                cartItem = (List<CartItem>)cart;
            }
            return View(cartItem);
        }

        //Add Cart

        public JsonResult AddCartItem(long id, int qty)
        {
            try
            {
                var product = new ProductDAO().GetById(id);
                var cart = Session["CartSession"];
                var cartItem = new List<CartItem>();
                decimal tongtien = 0;
                decimal gia = 0;
                //Kiểm tra giỏ hàng
                if (cart != null)
                {
                    cartItem = (List<CartItem>)cart;
                    //kiểm tra sản phẩm
                    if (cartItem.Exists(x => x.product.ID == product.ID))
                    {
                        foreach (var item in cartItem)
                        {
                            if (item.product.ID == product.ID)
                            {
                                item.quantity += qty;
                            }
                        }
                    }
                    //nếu chưa có sp
                    else
                    {
                        var item = new CartItem();
                        item.product = product;
                        item.quantity = qty;
                        cartItem.Add(item);
                    }
                    Session["CartSession"] = cartItem;
                }
                //Nếu giỏ hàng rỗng
                else
                {
                    var item = new CartItem();
                    cartItem = new List<CartItem>();
                    item.product = product;
                    item.quantity = qty;
                    cartItem.Add(item);
                    Session["CartSession"] = cartItem;
                }
                foreach (var item in cartItem)
                {
                    if (item.product.promotionPrice > 0)
                    {
                        gia = item.product.promotionPrice.Value;
                        tongtien += (item.product.promotionPrice.GetValueOrDefault(0) * item.quantity);
                    }
                    else
                    {
                        gia = item.product.price.Value;
                        tongtien += (item.product.price.GetValueOrDefault(0) * item.quantity);
                    }
                }
                return Json(new
                {
                    status = true,
                    soluong = cartItem.Count,
                    tongtien,
                    productItem = cartItem.Select(p => new
                    {
                        ID = p.product.ID,
                        productName = p.product.productName,
                        price = p.product.promotionPrice.HasValue ? p.product.promotionPrice : p.product.price,
                        image = p.product.image,
                        quantity = p.quantity
                    })
                }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(new
                {
                    status = false
                });
            }
        }

        //Xóa sản phẩm khỏi giỏ hàng
        public JsonResult DelItemCart(long id)
        {
            var sessionCart = (List<CartItem>)Session["CartSession"];
            sessionCart.RemoveAll(x => x.product.ID == id);
            Session["CartSession"] = sessionCart;
            return Json(new
            {
                status = true
            });
        }

        //Xóa giỏ hàng
        [HttpPost]
        public JsonResult DelAll()
        {
            Session["CartSession"] = null;
            return Json(new
            {
                status = true
            });
        }

        //Update
        public JsonResult UpdateCart(long id, int qty)
        {
            var product = new ProductDAO().GetById(id);
            var cart = (List<CartItem>)Session["CartSession"];
            var itemCart = cart.SingleOrDefault(x => x.product.ID == product.ID);
            if (itemCart != null)
            {
                itemCart.quantity = qty;
            }
            Session["CartSession"] = cart;
            return Json(new
            {
                status = true
            });
        }

        #endregion cart


        #region payment


        public ActionResult Payment()
        {
            var cart = Session["CartSession"];
            var cartItem = new List<CartItem>();
            var orderDao = new OrderDAO();
            var pay = orderDao.GetPay();
            var code = TempData["Code"];
            ViewBag.CartItem = null;
            decimal total = 0;
            if (cart != null)
            {
                cartItem = (List<CartItem>)cart;
                foreach (var item in cartItem)
                {
                    total += (item.quantity * (item.product.promotionPrice.HasValue ? item.product.promotionPrice.GetValueOrDefault(0) : item.product.price.GetValueOrDefault(0)));
                }
                ViewBag.Total = total;
                ViewBag.Pay = pay;
                return View(cartItem);
            }
            else
            {
                return Redirect("/gio-hang");
            }

        }

        public JsonResult SendOrder(string fullname, string address, string email, string phone, string message, int pay)
        {
            try
            {
                var orderDao = new OrderDAO();
                var orderDetailDao = new OrderDetailsDAO();
                var productDao = new ProductDAO();
                var cart = (List<CartItem>)Session["CartSession"];
                decimal tongtien = 0;
                foreach (var item in cart)
                {
                    tongtien += ((item.product.promotionPrice.HasValue ? item.product.promotionPrice.GetValueOrDefault(0) : item.product.price.GetValueOrDefault(0)) * item.quantity);
                }
                var order = new Order();
                order.fullName = fullname;
                order.phone = phone;
                order.email = email;
                order.address = address;
                order.message = message;
                order.payID = pay;
                order.status = false;
                order.Payment = false;
                order.dateSet = DateTime.Now;
                order.totalAmount = tongtien;


                var orderID = orderDao.Payment(order);

                foreach (var item in cart)
                {
                    int viewCount = (int)(item.product.quantity.GetValueOrDefault(0) + 1);
                    var orderDetail = new OrderDetail();
                    orderDetail.orderID = orderID;
                    orderDetail.productID = item.product.ID;
                    orderDetail.productCode = item.product.code;
                    orderDetail.productName = item.product.productName;
                    orderDetail.quantity = item.quantity;
                    orderDetail.productPrice = item.product.promotionPrice.HasValue ? item.product.promotionPrice : item.product.price;
                    orderDetail.Amount = (int)(item.quantity * (item.product.promotionPrice.HasValue ? item.product.promotionPrice.GetValueOrDefault(0) : item.product.price.GetValueOrDefault(0)));
                    orderDetailDao.AddOrderDetail(orderDetail);
                    productDao.SetViewcount(item.product, viewCount);
                }

                string content = System.IO.File.ReadAllText(Server.MapPath("~/Common/neworder.html"));

                content = content.Replace("{{CustomerName}}", fullname);
                content = content.Replace("{{Phone}}", phone);
                content = content.Replace("{{Email}}", email);
                content = content.Replace("{{Address}}", address);
                content = content.Replace("{{Total}}", tongtien.ToString("N0"));
                var toEmail = "mrhathuan@gmail.com";
                new Mail().SendMail(toEmail.Trim(), "Đơn hàng mới từ shop", content);
                Session["CartSession"] = null;
                return Json(new
                {
                    status = true
                }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(new
                {
                    status = false
                }, JsonRequestBehavior.AllowGet);
            }


        }

        #endregion payment

        public ActionResult Success()
        {
            return View();
        }

        public JsonResult CheckCode(string code)
        {
            var status = false;
            var dao = new CodeDAO();
            TempData["Code"] = null;
            if (dao.CheckCode(code.Trim().Replace(" ", "")))
            {
                TempData["Code"] = code.Trim().Replace(" ", ""); ;
                status = true;
            }
            return Json(status);
        }

    }
}