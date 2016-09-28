namespace Shop_Nhi.Models.Framework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Code")]
    public partial class Code
    {
        [StringLength(10)]
        public string ID { get; set; }

        public int Sale { get; set; }

        public bool Status { get; set; }
    }
}
