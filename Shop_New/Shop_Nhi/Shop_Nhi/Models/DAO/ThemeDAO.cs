using Shop_Nhi.Models.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Shop_Nhi.Models.DAO
{
    public class ThemeDAO
    {
        private DBConnect db = null;

        public ThemeDAO()
        {
            db = new DBConnect();
        }

        public string GetTheme()
        {
            return db.Themes.Find(1).link;
        }

        //
        public void ChangeTheme(int id, string link)
        {
            var result = db.Themes.Find(id);
            result.link = link;
            db.SaveChanges();
        }
    }
}