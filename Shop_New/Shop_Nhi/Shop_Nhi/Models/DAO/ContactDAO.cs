using Shop_Nhi.Models.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Shop_Nhi.Models.DAO
{
    public class ContactDAO
    {
        private DBConnect db = null;

        public ContactDAO()
        {
            db = new DBConnect();
        }

        public IEnumerable<Contact> List()
        {        
            return db.Contacts.OrderByDescending(x => x.createDate);
        }
     
    
        public Contact GetByID(long id)
        {
            return db.Contacts.Find(id);
        }

        public void Save(Contact contact)
        {
            var result = db.Contacts.Find(contact.ID);
            if(result == null || contact.ID == 0)
            {
                contact.ID = -1;
                contact.createDate = DateTime.Now;
                db.Contacts.Add(contact);
            }
            else
            {
                result.detail = contact.detail;
                result.name = contact.name;
                result.map = contact.map;
                result.modifiedByDate = DateTime.Now;
                result.modifiedByID = contact.modifiedByID;
                result.metatTitle = contact.metatTitle;
                result.metaKeywords = contact.metaKeywords;
                result.metaDescription = contact.metaDescription;
            }
            db.SaveChanges();
        }

        //Delete
        public bool Delete(long id)
        {
            try
            {
                var result = db.Contacts.Find(id);
                db.Contacts.Remove(result);
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
            var result = db.Contacts.Find(id);
            result.status = !result.status;
            db.SaveChanges();
            return result.status;
        }
    
    }
}