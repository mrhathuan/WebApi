using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Shop_Nhi.Areas.Pn.Models
{
    public class ProductModel
    {
        public long ID { get; set; }


        public string code { get; set; }


        public string productName { get; set; }


        public string image { get; set; }


        public string moreImages { get; set; }

        public decimal? price { get; set; }

        public decimal? promotionPrice { get; set; }

        public int? quantity { get; set; }


        public string chatlieu { get; set; }


        public string madeIn { get; set; }


        public string size { get; set; }

        public int? like { get; set; }

        public int? viewCount { get; set; }

        public long? categoryID { get; set; }

        public DateTime? createDate { get; set; }

        public string createByID { get; set; }

        public string modifiedByID { get; set; }

        public DateTime? modifiedByDate { get; set; }

        public string detail { get; set; }

        public string description { get; set; }

        public string metatTitle { get; set; }


        public string metaKeywords { get; set; }


        public string metaDescription { get; set; }

    }
}