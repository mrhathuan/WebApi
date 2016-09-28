using Shop_Nhi.Models.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Shop_Nhi.Models.DAO
{
    public class PageBodyDAO
    {
        private DBConnect db = null;

        public PageBodyDAO()
        {
            db = new DBConnect();
        }

        public List<PageBody> List()
        {
            return db.PageBodies.OrderByDescending(x => x.createDate).ToList();
        }

        public PageBody GetContent(string linkMenu)
        {
            return db.PageBodies.SingleOrDefault(x=>x.metatTitle == linkMenu);
        }

        public void Create(PageBody page)
        {
            db.PageBodies.Add(page);
            db.SaveChanges();
        }

        public PageBody GetByID(long id)
        {
            return db.PageBodies.Find(id);
        }

        //Edit
        public bool Edit(PageBody page)
        {
            var result = db.PageBodies.Find(page.ID);
            try
            {
                result.detail = page.detail;                
                result.modifiedByDate = DateTime.Now;
                result.modifiedByID = page.modifiedByID;
                result.metaKeywords = page.metaKeywords;
                result.metaDescription = page.metaDescription;
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }


        //Change Status
        public bool ChangeStatus(long id)
        {
            var result = db.PageBodies.Find(id);
            result.status = !result.status;
            db.SaveChanges();
            return result.status;
        }

        public bool CheckMenu(int menuID)
        {
            return db.PageBodies.Count(x => x.menuID == menuID) > 0;
        }

    }
}