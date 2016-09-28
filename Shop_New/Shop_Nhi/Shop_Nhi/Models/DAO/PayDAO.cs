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
             
        public void Create(Pay Pay)
        {
            db.Pays.Add(Pay);
            db.SaveChanges();
        }

        //Sửa
        public bool Edit(Pay pay)
        {
            var result = db.Pays.Find(pay.ID);
            try
            {
                result.name = pay.name;
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
            var result = db.Pays.Find(id);           
            try
            {                
                db.Pays.Remove(result);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        
        //Get ID
        public Pay GetByID(long id)
        {
            return db.Pays.Find(id);
        }
    
    }
}