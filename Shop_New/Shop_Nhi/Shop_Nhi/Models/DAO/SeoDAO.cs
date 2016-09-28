using Shop_Nhi.Models.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Shop_Nhi.Models.DAO
{
    public class SeoDAO
    {
        private DBConnect db = null;

        public SeoDAO()
        {
            db = new DBConnect();
        }

        public List<Seo> List()
        {
            return db.Seos.ToList();
        }

        public Seo GetContent(int id){
            return db.Seos.Find(id);
        }
        public void Edit(Seo seo)
        {
            var result = db.Seos.Find(seo.ID);
            result.metaTitle = seo.metaTitle;
            result.metaKeyword = seo.metaKeyword;
            result.metaDescription = seo.metaDescription;
            db.SaveChanges();
        }
    }
}