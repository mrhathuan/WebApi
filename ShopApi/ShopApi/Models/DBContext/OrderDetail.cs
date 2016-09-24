namespace ShopApi.Models.DBContext
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("OrderDetail")]
    public partial class OrderDetail
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long orderID { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long productID { get; set; }

        [StringLength(20)]
        public string productCode { get; set; }

        [StringLength(100)]
        public string productName { get; set; }

        public decimal? productPrice { get; set; }

        public int? quantity { get; set; }

        public int? Amount { get; set; }
    }
}
