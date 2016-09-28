using Shop_Nhi.Models.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Shop_Nhi.Models.DAO
{
    public class CodeDAO
    {
         private DBConnect db = null;

        public CodeDAO()
        {
            db = new DBConnect();
        }

        //get list
        public List<Code> ListAll()
        {
            return db.Codes.OrderByDescending(x=>x.ID).ToList();
        }
             
        public void Create(Code code)
        {
            db.Codes.Add(code);
            db.SaveChanges();
        }

        //Sửa
        public bool Edit(Code code)
        {
            var result = db.Codes.Find(code.ID);
            try
            {
                result.ID = code.ID;
                result.Sale = code.Sale;
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        //Xóa
        public bool Delete(string id)
        {
            var result = db.Codes.Find(id);           
            try
            {
                
                db.Codes.Remove(result);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        
        //Get ID
        public Code GetByID(string id)
        {
            return db.Codes.Find(id);
        }

        public bool CheckCode(string code)
        {
            return db.Codes.Count(x => x.ID == code) > 0;
        }
       
        //Change Status
        public bool ChangeStatus(string id)
        {
            var result = db.Codes.Find(id);
            result.Status = !result.Status;
            db.SaveChanges();
            return result.Status;
        }
    }
}