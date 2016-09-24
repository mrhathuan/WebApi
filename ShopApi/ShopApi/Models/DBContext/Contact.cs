namespace ShopApi.Models.DBContext
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Contact")]
    public partial class Contact
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }

        [StringLength(50)]
        public string name { get; set; }

        [StringLength(300)]
        public string map { get; set; }

        public DateTime? createDate { get; set; }

        [StringLength(50)]
        public string createByID { get; set; }

        [StringLength(50)]
        public string modifiedByID { get; set; }

        public DateTime? modifiedByDate { get; set; }

        [Column(TypeName = "ntext")]
        public string detail { get; set; }

        [StringLength(250)]
        public string metatTitle { get; set; }

        [StringLength(250)]
        public string metaKeywords { get; set; }

        [StringLength(250)]
        public string metaDescription { get; set; }

        [StringLength(2)]
        public string language { get; set; }

        public bool? status { get; set; }
    }
}
