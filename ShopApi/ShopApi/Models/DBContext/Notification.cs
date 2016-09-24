namespace ShopApi.Models.DBContext
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Notification")]
    public partial class Notification
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }

        [Column(TypeName = "ntext")]
        public string detail { get; set; }

        public DateTime? createDate { get; set; }

        [StringLength(50)]
        public string createByID { get; set; }

        [StringLength(2)]
        public string language { get; set; }

        public bool? satus { get; set; }
    }
}
