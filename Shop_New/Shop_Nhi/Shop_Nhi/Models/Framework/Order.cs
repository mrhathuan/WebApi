namespace Shop_Nhi.Models.Framework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Order")]
    public partial class Order
    {
        
        public long ID { get; set; }

        [StringLength(50)]
        public string fullName { get; set; }

        [StringLength(20)]
        public string phone { get; set; }

        [StringLength(50)]
        public string email { get; set; }

        [StringLength(100)]
        public string address { get; set; }

        public DateTime? dateSet { get; set; }

        [StringLength(200)]
        public string message { get; set; }

        public decimal? totalAmount { get; set; }

        [StringLength(10)]
        public string Code { get; set; }

        public int? payID { get; set; }

        public bool status { get; set; }

        public bool Payment { get; set; }

        public virtual Pay Pay { get; set; }

       
    }
}
