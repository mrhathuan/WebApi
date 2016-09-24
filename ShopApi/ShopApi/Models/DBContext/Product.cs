namespace ShopApi.Models.DBContext
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Product")]
    public partial class Product
    {
        public long ID { get; set; }

        [StringLength(20)]
        public string code { get; set; }

        [StringLength(100)]
        public string productName { get; set; }

        [StringLength(300)]
        public string image { get; set; }

        [Column(TypeName = "xml")]
        public string moreImages { get; set; }

        public decimal? price { get; set; }

        public decimal? promotionPrice { get; set; }

        public int? quantity { get; set; }

        [StringLength(50)]
        public string chatlieu { get; set; }

        [StringLength(50)]
        public string madeIn { get; set; }

        [StringLength(10)]
        public string size { get; set; }

        public int? like { get; set; }

        public int? viewCount { get; set; }

        public long? categoryID { get; set; }

        public DateTime? createDate { get; set; }

        [StringLength(50)]
        public string createByID { get; set; }

        [StringLength(50)]
        public string modifiedByID { get; set; }

        public DateTime? modifiedByDate { get; set; }

        [Column(TypeName = "ntext")]
        public string detail { get; set; }

        [StringLength(250)]
        public string description { get; set; }

        [StringLength(250)]
        public string metatTitle { get; set; }

        [StringLength(250)]
        public string metaKeywords { get; set; }

        [StringLength(250)]
        public string metaDescription { get; set; }

        public bool? status { get; set; }

        public virtual Category Category { get; set; }
    }
}
