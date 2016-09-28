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
        public void Create(Contact contact)
        {
            db.Contacts.Add(contact);
            db.SaveChanges();
        }

        //Edit
        public bool Edit(Contact contact)
        {
            var result = db.Contacts.Find(contact.ID);
            try
            {
                result.detail = contact.detail;
                result.name = contact.name;
                result.map = contact.map;
                result.metatTitle = contact.metatTitle;
                result.metaKeywords = contact.metaKeywords;
                result.metaDescription = contact.metaDescription;
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
            var result = db.Contacts.Find(id);            
            db.Contacts.Remove(result);
            db.SaveChanges();
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