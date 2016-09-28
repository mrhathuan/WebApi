using Shop_Nhi.Models.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Shop_Nhi.Models.DAO
{
    public class NotificationDAO
    {
        private DBConnect db = null;

        public NotificationDAO()
        {
            db = new DBConnect();
        }

        public Notification GetNotification()
        {
            return db.Notifications.SingleOrDefault(x => x.satus == true);
        }
        //list
        public List<Notification> GetListNotifications()
        {
            return db.Notifications.OrderByDescending(x => x.createDate).ToList();
        }
        //thêm
        public void Create(Notification notification)
        {
            db.Notifications.Add(notification);
            db.SaveChanges();
        }
        public Notification GetNotificationById(int id)
        {
            return db.Notifications.Find(id);
        }
        //sửa
        public bool Edit(Notification notifications)
        {
            try
            {
                var result = db.Notifications.Find(notifications.ID);
                result.detail = notifications.detail;
                result.createByID = notifications.createByID;               
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        //xóa
        public void Delete(int id)
        {
            var result = db.Notifications.Find(id);
            db.Notifications.Remove(result);
            db.SaveChanges();
        }
        //Sửa trạng thái
        public bool ChangeStatus(int id)
        {
            var result = db.Notifications.Find(id);
            result.satus = !result.satus;
            db.SaveChanges();
            return result.satus;
        }
    }

}

