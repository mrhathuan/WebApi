using Shop_Nhi.Models.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Shop_Nhi.Models
{
    [Serializable]
    public class CartItem
    {
        public Product product { get; set; }
        public int quantity { get; set; }
    }
}