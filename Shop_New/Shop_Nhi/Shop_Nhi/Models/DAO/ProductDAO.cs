using Shop_Nhi.Models.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Shop_Nhi.Models.DAO
{
    public class ProductDAO
    {
        private DBConnect db = null;

        public ProductDAO()
        {            
            db = new DBConnect();
        }


        public void Save(Product pro)
        {
            var result = db.Products.Find(pro.ID);
            if (result == null || pro.ID ==0)
            {
                pro.ID = -1;
                pro.createDate = DateTime.Now;              
                db.Products.Add(pro);
            }
            else
            {               
                result.code = pro.code;
                result.productName = pro.productName;
                result.price = pro.price;
                result.image = pro.image;
                result.promotionPrice = pro.promotionPrice;
                result.quantity = pro.quantity;
                result.categoryID = pro.categoryID;
                result.metatTitle = pro.metatTitle;
                result.metaKeywords = pro.metaKeywords;
                result.metaDescription = pro.metaDescription;
                result.chatlieu = pro.chatlieu;
                result.madeIn = pro.madeIn;
                result.detail = pro.detail;
                result.modifiedByID = pro.modifiedByID;
                result.modifiedByDate = DateTime.Now;
            }
            db.SaveChanges();
        }

        public IEnumerable<Product> List()
        {
            return db.Products.OrderByDescending(x => x.createDate).ToList();
        }

        public List<Product> ListAll()
        {
            return db.Products.OrderByDescending(x => x.createDate).ToList();
        }

        //danh sách mới
        public List<Product> ListNewPro(int top)
        {

            return db.Products.Where(x => x.status == true).OrderByDescending(x => x.createDate).Take(top).ToList();
        }

     
        //product sort
        public List<Product> ProductCategory(int sort, long cateId, ref int totalRecord, int pageIndex = 1, int pageSize = 2)
        {
            var query = (from c in db.Categories where c.ID == cateId select c.ID).Union(
                    from c1 in db.Categories
                    join
                        c2 in db.Categories on c1.parentID.Value equals c2.ID
                    where c2.ID == cateId
                    select c1.ID
                ).ToList();
            var listProduct = from p in db.Products where (query.Contains(p.categoryID.Value) && p.status == true) select p;
            totalRecord = listProduct.Count();
            switch (sort)
            {
                case 1:
                    listProduct = listProduct.OrderByDescending(x => x.createDate);
                    break;
                case 2:
                    listProduct = listProduct.OrderBy(x => x.price);
                    break;
                case 3:
                    listProduct = listProduct.OrderByDescending(x => x.viewCount);
                    break;
                case 4:
                    listProduct = listProduct.OrderByDescending(x => x.like);
                    break;
                default:
                    listProduct = listProduct.OrderByDescending(x => x.createDate);
                    break;
            }
            return listProduct.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            
        }

        public List<Product> ListName(string keyword)
        {
            return db.Products.Where(x => x.productName.Contains(keyword)).ToList();
        }


        //Search
        public List<Product> ListSearch(int sort,string keyword, ref int totalRecord, int pageIndex = 1, int pageSize = 2)
        {
            totalRecord = db.Products.Where(x => x.status == true && (x.code.Contains(keyword) || x.productName.Contains(keyword))).Count();
            var listProduct = db.Products.Where(x => x.status == true && (x.code.Contains(keyword) || x.productName.Contains(keyword)));
            switch (sort)
            {
                case 1:
                    listProduct = listProduct.OrderByDescending(x => x.createDate);
                    break;
                case 2:
                    listProduct = listProduct.OrderBy(x => x.price);
                    break;
                case 3:
                    listProduct = listProduct.OrderByDescending(x => x.viewCount);
                    break;
                case 4:
                    listProduct = listProduct.OrderByDescending(x => x.like);
                    break;
                default:
                    listProduct = listProduct.OrderByDescending(x => x.createDate);
                    break;
            }
            return listProduct.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
        }

        
        //List All status = true
        public List<Product> ListAllTrue(int sort,ref int totalRecord, int pageIndex = 1, int pageSize = 2)
        {            
            var listProduct = db.Products.Where(x => x.status == true);
            totalRecord = listProduct.Count();
            switch (sort)
            {
                case 1:
                    listProduct = listProduct.OrderByDescending(x => x.createDate);
                    break;
                case 2:
                    listProduct = listProduct.OrderBy(x => x.price);
                    break;
                case 3:
                    listProduct = listProduct.OrderByDescending(x => x.viewCount);
                    break;
                case 4:
                    listProduct = listProduct.OrderByDescending(x => x.like);
                    break;
                default:
                    listProduct = listProduct.OrderByDescending(x => x.createDate);
                    break;
            }
            return listProduct.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
        }

        //Sale
        public List<Product> ListSale(int top)
        {
            return db.Products.Where(x => x.promotionPrice != null && x.status == true).OrderByDescending(x => x.createDate).Take(top).ToList();
        }
       
        //Xóa
        public bool Delete(long id)
        {
            try
            {
                var product = db.Products.Find(id);
                db.Products.Remove(product);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        //Find id
        public Product GetById(long id)
        {
            return db.Products.Find(id);
        }

        //Sản phẩm cùng loại
        public List<Product> ListRelateProduct(long productId, int show)
        {
            var product = db.Products.Find(productId);
            return db.Products.Where(x =>x.status == true && x.categoryID == product.categoryID && x.ID != productId).OrderByDescending(x=>x.modifiedByDate).Take(show).ToList();
        }

        //change status
        public bool ChangeStatus(long id)
        {
            var result = db.Products.Find(id);
            result.status = !result.status;
            db.SaveChanges();
            return result.status;
        }
        //Sản phẩm xem nhiều
        public List<Product> ListViewcount(int show)
        {
            return db.Products.Where(x=>x.status == true).OrderByDescending(x => x.viewCount).Take(show).ToList();
        }

        //Hàng sắp về
        public List<Product> ListByStatusFalse()
        {
            return db.Products.Where(x => x.status == false).OrderByDescending(x => x.createDate).ToList();
        }

        //Hàng đã về
        public List<Product> ListByStatusTrue()
        {
            return db.Products.Where(x => x.status == true).OrderByDescending(x => x.createDate).ToList();
        }
        //List like
        public List<Product> ListLike(int show)
        {
            return db.Products.Where(x => x.status == true).OrderByDescending(x => x.like).Take(show).ToList();
        }
        //Set lượt xem
        public void SetViewcount(Product pro,int viewCount)
        {
            var result = db.Products.Find(pro.ID);
            result.viewCount = viewCount ;
            db.SaveChanges();
        }

        //Set like
        public void SetLike(long id,int like)
        {
            var result = db.Products.Find(id);
            result.like = like;
            db.SaveChanges();
        }

        //Thêm ảnh
        public void UpdateImages(long id, string images)
        {
            var result = db.Products.Find(id);
            result.moreImages = images;
            db.SaveChanges();
        }

        //change quantity
        public void ChangeQuantity(long id, int qty)
        {
            var result = db.Products.Find(id);
            result.quantity = qty;
            db.SaveChanges();
        }
    }   
}