namespace ShopApi.Models.DBContext
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class DBContext : DbContext
    {
        public DBContext()
            : base("name=DBContext")
        {
        }

        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Code> Codes { get; set; }
        public virtual DbSet<Contact> Contacts { get; set; }
        public virtual DbSet<ContentTag> ContentTags { get; set; }
        public virtual DbSet<Footer> Footers { get; set; }
        public virtual DbSet<Language> Languages { get; set; }
        public virtual DbSet<Menu> Menus { get; set; }
        public virtual DbSet<MenuType> MenuTypes { get; set; }
        public virtual DbSet<Notification> Notifications { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderDetail> OrderDetails { get; set; }
        public virtual DbSet<PageBody> PageBodies { get; set; }
        public virtual DbSet<Pay> Pays { get; set; }
        public virtual DbSet<Post> Posts { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Seo> Seos { get; set; }
        public virtual DbSet<Slide> Slides { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<Theme> Themes { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>()
                .Property(e => e.createByID)
                .IsUnicode(false);

            modelBuilder.Entity<Category>()
                .Property(e => e.modifiedByID)
                .IsUnicode(false);

            modelBuilder.Entity<Category>()
                .Property(e => e.language)
                .IsUnicode(false);

            modelBuilder.Entity<Code>()
                .Property(e => e.ID)
                .IsFixedLength();

            modelBuilder.Entity<Contact>()
                .Property(e => e.map)
                .IsUnicode(false);

            modelBuilder.Entity<Contact>()
                .Property(e => e.createByID)
                .IsUnicode(false);

            modelBuilder.Entity<Contact>()
                .Property(e => e.modifiedByID)
                .IsUnicode(false);

            modelBuilder.Entity<Contact>()
                .Property(e => e.language)
                .IsUnicode(false);

            modelBuilder.Entity<ContentTag>()
                .Property(e => e.TagID)
                .IsUnicode(false);

            modelBuilder.Entity<Footer>()
                .Property(e => e.createByID)
                .IsUnicode(false);

            modelBuilder.Entity<Footer>()
                .Property(e => e.modifiedByID)
                .IsUnicode(false);

            modelBuilder.Entity<Language>()
                .Property(e => e.ID)
                .IsUnicode(false);

            modelBuilder.Entity<Menu>()
                .Property(e => e.language)
                .IsUnicode(false);

            modelBuilder.Entity<MenuType>()
                .HasMany(e => e.Menus)
                .WithOptional(e => e.MenuType)
                .HasForeignKey(e => e.typeID);

            modelBuilder.Entity<Notification>()
                .Property(e => e.createByID)
                .IsUnicode(false);

            modelBuilder.Entity<Notification>()
                .Property(e => e.language)
                .IsUnicode(false);

            modelBuilder.Entity<Order>()
                .Property(e => e.phone)
                .IsFixedLength();

            modelBuilder.Entity<Order>()
                .Property(e => e.totalAmount)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Order>()
                .Property(e => e.Code)
                .IsFixedLength();

            modelBuilder.Entity<OrderDetail>()
                .Property(e => e.productCode)
                .IsFixedLength();

            modelBuilder.Entity<OrderDetail>()
                .Property(e => e.productPrice)
                .HasPrecision(18, 0);

            modelBuilder.Entity<PageBody>()
                .Property(e => e.createByID)
                .IsUnicode(false);

            modelBuilder.Entity<PageBody>()
                .Property(e => e.modifiedByID)
                .IsUnicode(false);

            modelBuilder.Entity<PageBody>()
                .Property(e => e.language)
                .IsUnicode(false);

            modelBuilder.Entity<Post>()
                .Property(e => e.createByID)
                .IsUnicode(false);

            modelBuilder.Entity<Post>()
                .Property(e => e.modifiedByID)
                .IsUnicode(false);

            modelBuilder.Entity<Post>()
                .Property(e => e.language)
                .IsUnicode(false);

            modelBuilder.Entity<Product>()
                .Property(e => e.code)
                .IsFixedLength();

            modelBuilder.Entity<Product>()
                .Property(e => e.price)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Product>()
                .Property(e => e.promotionPrice)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Product>()
                .Property(e => e.size)
                .IsFixedLength();

            modelBuilder.Entity<Product>()
                .Property(e => e.createByID)
                .IsUnicode(false);

            modelBuilder.Entity<Product>()
                .Property(e => e.modifiedByID)
                .IsUnicode(false);

            modelBuilder.Entity<Role>()
                .Property(e => e.Name)
                .IsFixedLength();

            modelBuilder.Entity<Slide>()
                .Property(e => e.image)
                .IsUnicode(false);

            modelBuilder.Entity<Theme>()
                .Property(e => e.link)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.userName)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.password)
                .IsUnicode(false);
        }
    }
}
