using Shop_Nhi.Models.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Shop_Nhi.Models.DAO
{
    public class MenuDAO
    {
        private DBConnect db = null;

        public MenuDAO()
        {
            db = new DBConnect();
        }

        public List<Menu> List()
        {
            return db.Menus.OrderBy(x => x.dislayOrder).ToList();
        }

        public int Create(Menu menu)
        {
            try
            {
                var result = db.Menus.Add(menu);
                db.SaveChanges();
                return result.ID;
            }
            catch
            {
                return 0;
            }
            
        }

        public Menu GetByID(long id)
        {
            return db.Menus.Find(id);
        }
        //Edit
        public bool Edit(Menu menu)
        {
            var result = db.Menus.Find(menu.ID);
            var page = db.PageBodies.SingleOrDefault(z => z.menuID == menu.ID);
            try
            {
                result.Name = menu.Name;
                result.link = menu.link;
                result.taget = menu.taget;
                result.typeID = menu.typeID;
                page.metatTitle = menu.link;
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        //Delete
        public void Delete(long id)
        {
            var result = db.Menus.Find(id);
            var page = db.PageBodies.Where(x => x.menuID == id);
            foreach (var item in page)
            {
                db.PageBodies.Remove(item);
            }
            db.Menus.Remove(result);
            db.SaveChanges();
        }

        //Change Status
        public bool ChangeStatus(long id)
        {
            var result = db.Menus.Find(id);
            result.status = !result.status;
            db.SaveChanges();
            return result.status;
        }


        public bool CheckLink(string link)
        {
            return db.Menus.Count(x => x.link == link) > 0;
        }

        public List<MenuType> GetMenuType()
        {
            return db.MenuTypes.OrderBy(x => x.ID).ToList();
        }
    }
}