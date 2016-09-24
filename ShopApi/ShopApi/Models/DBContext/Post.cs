namespace ShopApi.Models.DBContext
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Post")]
    public partial class Post
    {
        public long ID { get; set; }

        [StringLength(100)]
        public string name { get; set; }

        [StringLength(500)]
        public string description { get; set; }

        [StringLength(300)]
        public string image { get; set; }

        [StringLength(50)]
        public string createDate { get; set; }

        [StringLength(50)]
        public string createByID { get; set; }

        [StringLength(50)]
        public string modifiedByID { get; set; }

        [StringLength(50)]
        public string modifiedByDate { get; set; }

        [Column(TypeName = "ntext")]
        public string detail { get; set; }

        [StringLength(250)]
        public string metatTitle { get; set; }

        [StringLength(250)]
        public string metaKeywords { get; set; }

        [StringLength(250)]
        public string metaDescription { get; set; }

        [StringLength(500)]
        public string tag { get; set; }

        [StringLength(2)]
        public string language { get; set; }

        public bool? status { get; set; }
    }
}
