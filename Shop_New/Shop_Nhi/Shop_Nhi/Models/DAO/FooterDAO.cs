using Shop_Nhi.Models.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Shop_Nhi.Models.DAO
{
    public class FooterDAO
    {
        private DBConnect db = null;

        public FooterDAO()
        {
            db = new DBConnect();
        }

        public List<Footer> List()
        {
            return db.Footers.OrderBy(x => x.createDate).ToList();
        }

        public Footer GetByID(long id)
        {
            return db.Footers.Find(id);
        }
        //Edit
        public void Edit(Footer footer)
        {
            var result = db.Footers.Find(footer.ID);
            result.name = footer.name;
            result.metatTitle = footer.metatTitle;
            result.metaKeywords = footer.metaKeywords;
            result.metaDescription = footer.metaDescription;
            result.modifiedByID = footer.modifiedByID;
            result.modifiedByDate = DateTime.Now;
            result.detail = footer.detail;
            db.SaveChanges();
        }


        //Change Status
        public bool ChangeStatus(long id)
        {
            var result = db.Footers.Find(id);
            result.status = !result.status;
            db.SaveChanges();
            return result.status;
        }

    }
}
