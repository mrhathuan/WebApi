using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Shop_Nhi.Models.Framework;

namespace Shop_Nhi.Models.DAO
{
    public class CategoryDAO
    {
        private DBConnect db = null;

        public CategoryDAO()
        {
            db = new DBConnect();
        }

        //get list
        public List<Category> ListAll()
        {
            return db.Categories.OrderByDescending(x=>x.createDate).ToList();
        }
        //get list whith status
        public List<Category> ListByStatus()
        {
            return db.Categories.Where(x => x.status == true).OrderBy(x => x.createDate).ToList();
        }

        //List ByShowHome
        public List<Category> ListByShowhome()
        {
            return db.Categories.Where(x => x.showOnHome == true).OrderBy(x => x.createDate).ToList();
        }
        //Thêm
        public void Create(Category cate)
        {
            db.Categories.Add(cate);
            db.SaveChanges();
        }

        //Sửa
        public bool Edit(Category cate)
        {
            var result = db.Categories.Find(cate.ID);            
            try
            {               
                result.name = cate.name;
                result.metatTitle = cate.metatTitle;               
                result.parentID = cate.parentID;
                result.metaKeywords = cate.metaKeywords;
                result.metaDescription = cate.metaDescription;
                result.modifiedByID = cate.modifiedByID;
                result.modifiedByDate = DateTime.Now;
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        //Xóa
        public bool Delete(long id)
        {
            var result = db.Categories.Find(id);
            var product = db.Products.Where(x => x.categoryID == id);
            try
            {
                foreach(var item in product)
                {
                    db.Products.Remove(item);
                }
                db.Categories.Remove(result);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        
        //Get ID
        public Category GetByID(long id)
        {
            return db.Categories.Find(id);
        }

        //Hiện menu
        public bool ChangeShowHome(long id)
        {
            var result = db.Categories.Find(id);
            result.showOnHome = !result.showOnHome;
            db.SaveChanges();
            return result.showOnHome;
        }

        //Change Status
        public bool ChangeStatus(long id)
        {
            var result = db.Categories.Find(id);
            result.status = !result.status;
            db.SaveChanges();
            return result.status;
        }
    }
}