using Shop_Nhi.Models.DAO;
using Shop_Nhi.Models.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Shop_Nhi.Common;
using System.Web.Script.Serialization;
using System.Xml.Linq;
using log4net;
using System.Reflection;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using Shop_Nhi.Areas.Pn.Models;

namespace Shop_Nhi.Areas.Pn.Controllers
{

    [Authorize(Roles = "ADMIN,MANAGE")]
    public class ProductController : BaseController
    {

        private static readonly ILog logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        // GET: Thuankay/Product

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult List([DataSourceRequest]DataSourceRequest request)
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
                like = x.like,
                viewCount = x.viewCount,
                status = x.status
                
            }).ToList();
            return Json(item.ToDataSourceResult(request));
        }
        //excel
        [HttpPost]
        public ActionResult Excel_Export_Save(string contentType, string base64, string fileName)
        {
            var fileContents = Convert.FromBase64String(base64);

            return File(fileContents, contentType, fileName);
        }

        public ActionResult ByCategories()
        {
            var dao = new CategoryDAO();
            var categories = dao.ListAll()
                        .Select(c => new Category {
                            ID = c.ID,
                            name = c.name
                        })
                        .OrderBy(e => e.createDate);

            return Json(categories, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Create()
        {
            setCategory();
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(Product pro)
        {
            try
            {
                var dao = new ProductDAO();                               
                if (!StringHelper.IsValiCode(pro.code.Trim()) || pro.code.Trim().Length > 20)
                    throw new Exception("MÃ SẢN PHẨM KHỐNG ĐÚNG. KHÔNG THỂ LƯU.");
                pro.code = pro.code.Trim();
                pro.metatTitle = StringHelper.RemoveSpecialChars(pro.productName.Trim()).Replace(" ", "-");
                pro.status = true;
                pro.createDate = DateTime.Now;
                pro.createByID = (string)Session["username"];
                dao.Create(pro);
                SetAlert("Thêm sản phẩm thành công.", "success");
                return RedirectToAction("Index", "Product");
            }
            catch(Exception e)
            {
                SetAlert(e.Message, "error");
                logger.Error(e);
                setCategory();
                return View();
            }
        }

        /// <summary>
        /// Sửa
        /// </summary>
        /// <returns></returns>
        /// 
        [HttpGet]
        public ActionResult Edit(long id)
        {
            var dao = new ProductDAO();
            setCategory(dao.GetById(id).categoryID);
            return View(dao.GetById(id));
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(Product pro)
        {
            try
            {
                var dao = new ProductDAO();               
                if (!StringHelper.IsValiCode(pro.code.Trim()) || pro.code.Trim().Length > 20)
                    throw new Exception("MÃ SẢN PHẨM KHỐNG ĐÚNG. KHÔNG THỂ LƯU");
                pro.code = pro.code.Trim();
                pro.modifiedByID = (string)Session["username"];
                pro.metatTitle = StringHelper.RemoveSpecialChars(pro.productName.Trim()).Replace(" ", "-");
                var result = dao.Edit(pro);
                if (result)
                {
                    SetAlert("Thông báo! Sửa sản phẩm thành công.", "success");
                    return RedirectToAction("Index", "Product");
                }
                
            }
            catch (Exception e)
            {
                SetAlert(e.Message, "error");
                setCategory(pro.categoryID);
                logger.Error(e);
                return View();
            }
            return View();
        }

        //Detail
        [HttpPost]
        public JsonResult Detail(long id)
        {
            var dao = new ProductDAO();
            var result = dao.GetById(id);
            var product = new Product();
            product.code = result.code;
            product.productName = result.productName;
            product.image = result.image;
            product.price = result.price;
            product.promotionPrice = result.promotionPrice;
            product.like = product.like;
            product.viewCount = product.viewCount;
            product.quantity = product.quantity;
            product.metatTitle = product.metatTitle;
            product.createDate = result.createDate;
            product.modifiedByDate = product.modifiedByDate;
            product.createByID = result.createByID;
            product.modifiedByID = result.modifiedByID;
            product.metaKeywords = result.metaKeywords;
            product.metaDescription = result.metaDescription;
            return Json(new {
                product
            });
        }

        //Xóa
        [HttpPost]
        public JsonResult Delete(long id)
        {
            var result = new ProductDAO().Delete(id);
            return Json(new
            {
                status = result
            });
        }

        //Change status
        [HttpPost]
        public JsonResult ChangeStatus(long id)
        {
            var result = new ProductDAO().ChangeStatus(id);
            return Json(new
            {
                status = result
            });
        }

        //Quản lý ảnh
        public JsonResult SaveImages(long id, string images)
        {
            JavaScriptSerializer seriaLizer = new JavaScriptSerializer();
            var listImages = seriaLizer.Deserialize<List<string>>(images);
            XElement xElement = new XElement("Images");
            foreach (var item in listImages)
            {
                
                var subtringItem = item.Substring(27);
                xElement.Add(new XElement("Image", subtringItem));
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
                logger.Error(e);
                return Json(new
                {                    
                    status = false
                });
            }

        }

        //Load ảnh
        public JsonResult LoadImages(long id)
        {
            ProductDAO dao = new ProductDAO();
            var images = dao.GetById(id).moreImages;
            try
            {
                XElement xImages = XElement.Parse(images);
                List<string> listImages = new List<string>();
                foreach (XElement element in xImages.Elements())
                {
                    listImages.Add(element.Value);
                }
                return Json(new
                {
                    status = true,
                    data = listImages
                }, JsonRequestBehavior.AllowGet);
            }
            catch(Exception e)
            {
                logger.Error(e);
                return Json(new
                {
                    status = false
                }, JsonRequestBehavior.AllowGet);
            }
        }

        //Change quntity
        public JsonResult ChangeQuantity(long id, int qty)
        {
            try
            {
                var dao = new ProductDAO();
                dao.ChangeQuantity(id, qty);
                return Json(new
                {
                    status = true
                });
            }
            catch (Exception e)
            {
                logger.Error(e);
                return Json(new
                {
                    status = false
                });
            }
        }

        //Set danh muc
        public void setCategory(long? selectedId = null)
        {
            var dao = new CategoryDAO();
            ViewBag.CategoryID = new SelectList(dao.ListAll(), "ID", "name", selectedId);
        }
    }
}