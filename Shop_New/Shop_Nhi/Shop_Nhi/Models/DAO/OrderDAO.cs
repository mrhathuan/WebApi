using Shop_Nhi.Models.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Shop_Nhi.Models.DAO
{
    public class OrderDAO
    {
        private DBConnect db = null;

        public OrderDAO()
        {
            db = new DBConnect();
        }

        //Đặt hàng
        public long Payment(Order od)
        {
            try
            {
                var reslut = db.Orders.Add(od);
                db.SaveChanges();
                return reslut.ID;
            }
            catch
            {
                return 0;
            }
        }

        //Danh sách đơn hàng
        public List<Order> ListOrder()
        {
            return db.Orders.OrderByDescending(x => x.dateSet).ToList();
        }

        //Đon hàng mới
        public List<Order> ListNewOrder()
        {
            return db.Orders.Where(x => x.status == false).OrderByDescending(x => x.dateSet).ToList();
        }
        //Chi tiết
        public Order GetByID(long id)
        {
            return db.Orders.Find(id);
        }

        //Delete
        public bool Delete(long id)
        {
            try
            {
                var order = db.Orders.Find(id);
                var orderDetail = db.OrderDetails.Where(x => x.orderID == id);
                foreach(var item in orderDetail)
                {
                    db.OrderDetails.Remove(item);
                }
                db.Orders.Remove(order);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        //Trạng thái
       public bool ChangeStatus(long id)
        {
            var result = db.Orders.Find(id);
            result.status = true;
            db.SaveChanges();
            return result.status;
        }

       //Trạng thái
       public bool ChangePayment(long id)
       {
           var result = db.Orders.Find(id);
           result.Payment = !result.Payment;
           db.SaveChanges();
           return result.status;
       }

        //Charts
        public List<int> OrderCharts(DateTime dFrom, DateTime dTo)
        {
            var result = new List<int>();
            for (var i = dFrom; dFrom < dTo; dFrom = dFrom.AddDays(1))
            {
                var from = dFrom.Date;
                var to = dFrom.Date.AddDays(1);
                result.Add(db.Orders.Where(c => c.dateSet >= from && c.dateSet < to).Select(c => c.ID).ToList().Distinct().Count());
            }
            return result;
        }

        //Get pay
       public List<Pay> GetPay()
       {
           return db.Pays.OrderBy(x => x.ID).ToList();
       }
    }
}