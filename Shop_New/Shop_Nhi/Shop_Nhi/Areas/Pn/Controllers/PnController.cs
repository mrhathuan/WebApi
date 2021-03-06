﻿using Shop_Nhi.Models.DAO;
using Shop_Nhi.Models.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Shop_Nhi.Common;
using System.Web.Script.Serialization;
using System.Xml.Linq;
using System.Reflection;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;

namespace Shop_Nhi.Areas.Pn.Controllers
{
    public class PnController : BaseController
    {
        //
        // GET: /Pn/Pn/

        #region Index

        public ActionResult Index()
        {
            return View();           
        }      
        #endregion Index
     

        #region DASH
        [Authorize(Roles = "ADMIN,MANAGE")]
        public ActionResult DASH_Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult DASH_Read()
        {
            var productDao = new ProductDAO();
            var orderDao = new OrderDAO();
            var postDao = new PostDAO();
            var totalProduct = productDao.ListAll().Count;
            var totalOrder = orderDao.ListOrder().Count;
            var totalProductTrue = productDao.ListByStatusTrue().Count;
            var totalProductFalse = productDao.ListByStatusFalse().Count;
            var totalProductNull = (productDao.ListAll().Where(x => x.quantity == null).ToList()).Count;
            var totalPost = postDao.List().ToList().Count;
            var newOrder = orderDao.ListOrder().Where(x => x.status == false).ToList().Count;
            var viewOrder = orderDao.ListOrder().Where(x => x.status == true).ToList().Count;
            var okOrder = orderDao.ListOrder().Where(x => x.Payment == true).ToList().Count;
            return Json(new
            {
                totalProduct = totalProduct,
                totalOrder = totalOrder,
                totalProductTrue = totalProductTrue,
                totalProductFalse = totalProductFalse,
                totalProductNull = totalProductNull,
                totalPost = totalPost,
                newOrder = newOrder,
                viewOrder = viewOrder,
                okOrder = okOrder
            });
        }

        [HttpPost]
        public JsonResult DASH_OrderCharts(string dFrom, string dTo)
        {
            DateTime dateF = DateTime.Parse(dFrom);
            DateTime dateT = DateTime.Parse(dTo);
            var dao = new OrderDAO();
            var result = dao.OrderCharts(dateF, dateT);
            return Json(new
            {
                result = result
            });
        }

        #endregion DASH

        #region product
        [Authorize(Roles = "ADMIN,MANAGE")]
        public ActionResult PRO_Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult PRO_Read([DataSourceRequest]DataSourceRequest request)
        {
            var dao = new ProductDAO();
            IList<Product> item = new List<Product>();
            item = dao.List().Select(x => new Product
            {
                ID = x.ID,
                code = x.code,
                productName = x.productName,
                image = x.image,
                price = x.price,
                promotionPrice = x.promotionPrice,
                quantity = x.quantity,
                categoryID = x.categoryID,
                Category = new Category
                {
                    ID = x.Category.ID,
                    name = x.Category.name
                },
                createDate = x.createDate,
                modifiedByDate = x.modifiedByDate,
                createByID = x.createByID,
                like = x.like,
                viewCount = x.viewCount,
                status = x.status

            }).ToList();
            return Json(item.ToDataSourceResult(request));
        }

        //excel
        [HttpPost]
        public ActionResult PRO_Excel_Export_Save(string contentType, string base64, string fileName)
        {
            var fileContents = Convert.FromBase64String(base64);

            return File(fileContents, contentType, fileName);
        }

        public ActionResult PRO_Categories_Filter()
        {
            var dao = new CategoryDAO();
            var categories = dao.ListAll()
                        .Select(c => new Category
                        {
                            ID = c.ID,
                            name = c.name
                        })
                        .OrderBy(e => e.createDate);

            return Json(categories, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult PRO_Get(long id)
        {
            var dao = new ProductDAO();
            var product = new Product();
            if (id == 0)
            {
                product = new Product();
            }
            else
            {
                var result = dao.GetById(id);
                product.ID = result.ID;
                product.code = result.code;
                product.productName = result.productName;
                product.image = result.image;
                product.price = result.price;
                product.promotionPrice = result.promotionPrice;
                product.quantity = result.quantity;
                product.madeIn = result.madeIn;
                product.size = result.size;
                product.chatlieu = result.chatlieu;
                product.metatTitle = result.metatTitle;
                product.categoryID = result.categoryID;
                product.description = result.description;
                product.detail = result.detail;
                product.metaKeywords = result.metaKeywords;
                product.metaDescription = result.metaDescription;
            }
            return Json(new
            {
                product
            });
        }


        [HttpPost]
        [ValidateInput(false)]
        public JsonResult PRO_Save(string item)
        {
            try
            {
                var dao = new ProductDAO();
                JavaScriptSerializer seriaLizer = new JavaScriptSerializer();
                Product pro = seriaLizer.Deserialize<Product>(item);
                if (!StringHelper.IsValiCode(pro.code.Trim()) || pro.code.Trim().Length > 20)
                    throw new Exception("MÃ SẢN PHẨM KHỐNG ĐÚNG. KHÔNG THỂ LƯU.");
                pro.code = pro.code.Trim();
                pro.metatTitle = StringHelper.RemoveSpecialChars(pro.productName.Trim()).Replace(" ", "-");
                pro.status = true;
                pro.createDate = DateTime.Now;
                pro.createByID = (string)Session["username"];
                dao.Save(pro);
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

        [HttpPost]
        public JsonResult PRO_Delete(long id)
        {
            var result = new ProductDAO().Delete(id);
            return Json(new
            {
                status = result
            });
        }
        //Change status
        [HttpPost]
        public JsonResult PRO_ChangeStatus(long id)
        {
            var result = new ProductDAO().ChangeStatus(id);
            return Json(new
            {
                status = result
            });
        }

        //Quản lý ảnh
        public JsonResult PRO_SaveImages(long id, string images)
        {
            JavaScriptSerializer seriaLizer = new JavaScriptSerializer();
            var listImages = seriaLizer.Deserialize<List<string>>(images);
            XElement xElement = new XElement("Images");
            foreach (var item in listImages)
            {

               // var subtringItem = item.Substring(23);//local
               // var subtringItem = item.Substring(27);//zury
                //var subtringItem = item.Substring(27);//nhi
                xElement.Add(new XElement("Image", item));
            }
            ProductDAO dao = new ProductDAO();
            try
            {
                dao.UpdateImages(id, xElement.ToString());
                return Json(new
                {
                    status = true
                });
            }
            catch (Exception e)
            {                
                return Json(new
                {
                    status = false
                });
            }

        }

        //Load ảnh
        [HttpPost]
        public JsonResult PRO_LoadImages(long id)
        {            
            try
            {
                var dao = new ProductDAO();
                var images = dao.GetById(id).moreImages;
                List<string> listImages = new List<string>();
                XElement xImages = XElement.Parse(images);                        
                foreach (XElement element in xImages.Elements())
                {
                    listImages.Add(element.Value);
                }
                return Json(new
                {
                    status = true,
                    producImages = listImages
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new
                {
                    msg = e.Message,
                    status = false
                }, JsonRequestBehavior.AllowGet);
            }
        }



        #endregion product

        #region categories
        [Authorize(Roles = "ADMIN,MANAGE")]
        public ActionResult CAT_Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult CAT_Read([DataSourceRequest]DataSourceRequest request)
        {
            var dao = new CategoryDAO();
            IList<Category> item = new List<Category>();
            item = dao.ListAll().Select(x => new Category
            {
                ID = x.ID,
                name = x.name,
                createDate = x.createDate,
                createByID = x.createByID,
                modifiedByID = x.modifiedByID,
                parentID = x.parentID,
                status = x.status,
                showOnHome = x.showOnHome,
                modifiedByDate = x.modifiedByDate

            }).ToList();
            return Json(item.ToDataSourceResult(request));
        }


        public ActionResult CAT_ParentIdIsNull()
        {
            var dao = new CategoryDAO();
            var categories = dao.ListAll().Where(x => x.parentID == null || x.parentID == 0)
                 .Select(c => new Category
                 {
                     ID = c.ID,
                     name = c.name
                 });

            return Json(categories, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult CAT_Get(long id)
        {
            var dao = new CategoryDAO();
            var cat = new Category();
            if(id== 0)
            {
                cat = new Category();
            }else
            {
                var result = dao.GetByID(id);
                cat.ID = result.ID;
                cat.name = result.name;
                cat.parentID = result.parentID;
                cat.metaKeywords = result.metaKeywords;
                cat.metaDescription = result.metaDescription;
            }
            return Json(new
            {
                cat = cat
            });
        }

        [HttpPost]
        public JsonResult CAT_Save(string item)
        {
            try
            {
                var dao = new CategoryDAO();
                JavaScriptSerializer seriaLizer = new JavaScriptSerializer();
                Category cate = seriaLizer.Deserialize<Category>(item);
                if (cate.ID == 0)
                {
                    cate.createDate = DateTime.Now;
                    cate.createByID = (string)Session["username"];
                }
                else
                {
                    cate.modifiedByID = (string)Session["username"];
                    cate.modifiedByDate = DateTime.Now;
                }
                cate.metatTitle = StringHelper.RemoveSpecialChars(cate.name.Trim()).Replace(" ", "-");
                cate.status = true;
                cate.showOnHome = false;
                dao.Save(cate);
                return Json(new
                {
                    msg = "Thành công",
                    status = true
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

        [HttpPost]
        public JsonResult CAT_Remove(long id)
        {
            var result = new CategoryDAO().Delete(id);
            return Json(new
            {
                status = result
            });
        }

        //Change Status
        [HttpPost]
        public JsonResult CAT_ChangeStatus(long id)
        {
            var result = new CategoryDAO().ChangeStatus(id);
            return Json(new
            {
                status = result
            });
        }

        //Show Home
        [HttpPost]
        public JsonResult CAT_ShowHome(long id)
        {
            var result = new CategoryDAO().ChangeShowHome(id);
            return Json(new
            {
                status = result
            });
        }

        #endregion categories

        #region order
        public ActionResult ORD_Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult ORD_Read([DataSourceRequest]DataSourceRequest request)
        {
            var dao = new OrderDAO();
            IList<Order> item = new List<Order>();
            item = dao.ListOrder().Select(x => new Order
            {
                ID = x.ID,
                fullName = x.fullName,
                dateSet = x.dateSet,
                email = x.email,
                phone = x.phone,
                address = x.address,
                payID = x.payID,
                message = x.message,                
                totalAmount = x.totalAmount,
                Pay = new Pay
                {
                    ID = x.Pay.ID,
                    name = x.Pay.name
                },
                status = x.status,
                Payment = x.Payment,

            }).ToList();
            return Json(item.ToDataSourceResult(request));
        }

        [HttpPost]
        public JsonResult ORD_Detail([DataSourceRequest]DataSourceRequest request,long id)
        {
            var dao = new OrderDetailsDAO();
            IList<OrderDetail> item = new List<OrderDetail>();
            item = dao.GetById(id).Select(x => new OrderDetail
            {
              orderID = x.orderID,
              productID = x.productID,
              productCode = x.productCode,
              productName = x.productName,
              productPrice = x.productPrice,
              Amount = x.Amount,
              quantity = x.quantity
            }).ToList();
            return Json(item.ToDataSourceResult(request));
        }

        [HttpPost]
        public JsonResult ORD_Remove(long id)
        {
            var result = new OrderDAO().Delete(id);
            return Json(new
            {
                status = result
            });
        }

        [HttpPost]
        public JsonResult ORD_ChangeStatus(long id)
        {
            var result = new OrderDAO().ChangeStatus(id);
            return Json(new
            {
                status = result
            });
        }

        [HttpPost]
        public JsonResult ORD_ChangePayment(long id)
        {
            var result = new OrderDAO().ChangePayment(id);
            return Json(new
            {
                status = result
            });
        }

        #endregion order

        #region payment
        [Authorize(Roles = "ADMIN")]
        public ActionResult PAYMENT_Index()
        {
            return View();
        }
        [HttpPost]
        public JsonResult PAYMENT_Read([DataSourceRequest]DataSourceRequest request)
        {
            var dao = new PayDAO();
            IList<Pay> item = new List<Pay>();
            item = dao.ListAll().Select(x => new Pay
            {
                ID = x.ID,
                name = x.name
            }).ToList();
            return Json(item.ToDataSourceResult(request));
        }

        [HttpPost]
        public JsonResult PAYMENT_Get(int id)
        {
            var dao = new PayDAO();
            var pay = new Pay();
            if (id == 0)
            {
                pay = new Pay();
            }
            else
            {
                var result = dao.GetByID(id);
                pay.ID = result.ID;
                pay.name = result.name;               
            }
            return Json(new
            {
                pay = pay
            });
        }

        [HttpPost]
        public JsonResult PAYMENT_Save(string item)
        {
            try
            {
                var dao = new PayDAO();
                JavaScriptSerializer seriaLizer = new JavaScriptSerializer();
                Pay pay = seriaLizer.Deserialize<Pay>(item);                                
                dao.Save(pay);
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

        [HttpPost]
        public JsonResult PAYMENT_Remove(int id)
        {
            try
            {
                var dao = new PayDAO();
                if (dao.CheckOrder(id))
                    throw new Exception("PHƯƠNG THỨC RÀNG BUỘC ĐƠN HÀNG.KHÔNG THỂ XÓA.");
                dao.Delete(id);
                return Json(new
                {
                    msg="Thành công",
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

        #endregion payment

        #region error
        [Authorize(Roles = "ADMIN,MANAGE")]
        public ActionResult ERROR_Index()
        {
            return View();
        }
        #endregion error

    }
}