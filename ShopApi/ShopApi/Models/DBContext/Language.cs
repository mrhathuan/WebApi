namespace ShopApi.Models.DBContext
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Language")]
    public partial class Language
    {
        [StringLength(2)]
        public string ID { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        public bool? IsDefault { get; set; }
    }
}
