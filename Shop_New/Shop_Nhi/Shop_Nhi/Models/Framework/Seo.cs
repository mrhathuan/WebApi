namespace Shop_Nhi.Models.Framework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Seo")]
    public partial class Seo
    {
        public int ID { get; set; }

        [StringLength(100)]
        public string metaTitle { get; set; }

        [StringLength(100)]
        public string metaKeyword { get; set; }

        [StringLength(100)]
        public string metaDescription { get; set; }

        public bool status { get; set; }
    }
}
