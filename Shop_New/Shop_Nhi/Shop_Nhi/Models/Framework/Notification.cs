namespace Shop_Nhi.Models.Framework
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

        public bool satus { get; set; }
    }
}
