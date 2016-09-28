namespace Shop_Nhi.Models.Framework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Theme")]
    public partial class Theme
    {
        public int ID { get; set; }

        [Column(TypeName = "text")]
        public string link { get; set; }

        public bool status { get; set; }
    }
}
