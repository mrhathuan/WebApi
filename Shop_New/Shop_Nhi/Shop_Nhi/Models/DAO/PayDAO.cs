using Shop_Nhi.Models.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Shop_Nhi.Models.DAO
{
    public class PayDAO
    {
         private DBConnect db = null;

        public PayDAO()
        {
            db = new DBConnect();
        }

        //get list
        public List<Pay> ListAll()
        {
            return db.Pays.OrderByDescending(x=>x.ID).ToList();
        }

        public void Save(Pay pay)
        {
            var result = db.Pays.Find((pay.ID));
            if (result == null || pay.ID == 0)
            {
                pay.ID = -1;
                db.Pays.Add(pay);
            }
            else
            {
                result.name = pay.name;
            }
            db.SaveChanges();
        }
       
      
        //Xóa
        public void Delete(int id)
        {
            var result = db.Pays.Find(id);
            db.Pays.Remove(result);
            db.SaveChanges();
        }

        public bool CheckOrder(int id)
        {
            return db.Orders.Count(x => x.payID == id) > 0;
        }

        //Get ID
        public Pay GetByID(int id)
        {
            return db.Pays.Find(id);
        }
    
    }
}