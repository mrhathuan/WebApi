namespace Shop_Nhi.Models.Framework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Slide")]
    public partial class Slide
    {
        public int ID { get; set; }

        [StringLength(100)]
        public string name { get; set; }

        [StringLength(300)]
        public string image { get; set; }

        public int dislayOrder { get; set; }

        [StringLength(100)]
        public string title { get; set; }

        [StringLength(250)]
        public string detail { get; set; }

        public bool status { get; set; }
    }
}
