namespace ShopApi.Models.DBContext
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Category")]
    public partial class Category
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Category()
        {
            Products = new HashSet<Product>();
        }

        public long ID { get; set; }

        [StringLength(50)]
        public string name { get; set; }

        public int? parentID { get; set; }

        public DateTime? createDate { get; set; }

        [StringLength(50)]
        public string createByID { get; set; }

        [StringLength(50)]
        public string modifiedByID { get; set; }

        public DateTime? modifiedByDate { get; set; }

        [StringLength(250)]
        public string metatTitle { get; set; }

        [StringLength(250)]
        public string metaKeywords { get; set; }

        [StringLength(250)]
        public string metaDescription { get; set; }

        [StringLength(2)]
        public string language { get; set; }

        public bool? status { get; set; }

        public bool? showOnHome { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Product> Products { get; set; }
    }
}
