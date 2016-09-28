using Shop_Nhi.Models.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Shop_Nhi.Models.DAO
{
    public class OrderDetailsDAO
    {
        private DBConnect db = null;

        public OrderDetailsDAO()
        {
            db = new DBConnect();
        }

        //Add
        public void AddOrderDetail(OrderDetail od)
        {
            db.OrderDetails.Add(od);
            db.SaveChanges();
        }
        //Add
        public List<OrderDetail> GetById(long id)
        {
            return db.OrderDetails.Where(x=>x.orderID == id).ToList();
        }
    }
}