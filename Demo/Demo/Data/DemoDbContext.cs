using Demo.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Demo.Data


{
    public class DemoDbContext : IdentityDbContext<AppUser>
    {
        public DemoDbContext(DbContextOptions<DemoDbContext> options) : base(options) { }

        #region DbSet
        /// <summary>
        /// Category
        /// </summary>
        public DbSet<Category> Categories { get; set; }

        /// <summary>
        /// Product
        /// </summary>
        public DbSet<Product> Products { get; set; }

        /// <summary>
        /// Order
        /// </summary>
        public DbSet<Order> Orders { get; set; }

        /// <summary>
        /// OrderDetail
        /// </summary>
        public DbSet<OrderDetail> OrderDetails { get; set; }

        /// <summary>
        /// Cart
        /// </summary>
        public DbSet<Cart> Carts { get; set; }

        /// <summary>
        /// CartItem
        /// </summary>
        public DbSet<CartItem> CartItems { get; set; }

        /// <summary>
        /// Image
        /// </summary>
        public DbSet<Image> Images { get; set; }
        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Category>(ent =>
            {
                ent.ToTable("Categories");
                ent.HasKey(ct => ct.CategoryId);
                ent.Property(ct => ct.CategoryName).HasMaxLength(50).IsRequired(true);
                ent.HasIndex(ct => ct.CategoryName).IsUnique(true);
            });


            modelBuilder.Entity<Product>(ent =>
            {
                ent.ToTable("Products");
                ent.HasKey(pd => pd.ProductId);
                ent.HasOne<Category>(pd => pd.Category).WithMany(pd => pd.Products).HasForeignKey(pd => pd.CategoryId).HasConstraintName("FK_Product_Category").OnDelete(DeleteBehavior.NoAction);
                ent.Property(pd => pd.ProductName).HasMaxLength(100).IsRequired(true);
                ent.Property(pd => pd.ProductQuantity).IsRequired(true);
                ent.Property(pd => pd.ProductPrice).IsRequired(true);
                ent.HasIndex(pd => pd.ProductName).IsUnique(true);

            });

            modelBuilder.Entity<Order>(ent =>
            {
                ent.ToTable("Orders");
                ent.HasKey(od => od.OrderId);
                ent.Property(od => od.ReceivedName).HasMaxLength(100).IsRequired(true);
                ent.Property(od => od.ReceivedPhoneNumber).HasMaxLength(20).IsRequired(true);
                ent.Property(od => od.ReceivedEmail).HasMaxLength(100).IsRequired(true);
                ent.HasIndex(od => od.OrderCode).IsUnique(true);
                ent.HasOne(od => od.User).WithMany(od => od.Orders).HasForeignKey(od => od.UserId).HasConstraintName("FK_Order_User").OnDelete(DeleteBehavior.Cascade);
            });
            modelBuilder.Entity<OrderDetail>(ent =>
            {
                ent.ToTable("OrderDetails");
                ent.HasKey(odl => new { odl.OrderId, odl.ProductId });
                ent.HasOne(odl => odl.Order).WithMany(odl => odl.OrderDetails).HasForeignKey(odl => odl.OrderId).HasConstraintName("FK_OrderDetail_Order").OnDelete(DeleteBehavior.NoAction);
                ent.HasOne(odl => odl.Product).WithMany(odl => odl.OrderDetails).HasForeignKey(odl => odl.ProductId).HasConstraintName("FK_OrderDetail_Product").OnDelete(DeleteBehavior.NoAction);
                ent.Property(odl => odl.BuyQuantity).IsRequired(true);
            });

            modelBuilder.Entity<Cart>(ent =>
            {
                ent.ToTable("Carts");
                ent.HasKey(c => c.CartId);
                ent.HasOne(c => c.User).WithOne(c => c.Cart).HasForeignKey<Cart>(c => c.UserId).HasConstraintName("FK_Cart_User");
            });


            modelBuilder.Entity<CartItem>(ent =>
            {
                ent.ToTable("CartItems");
                ent.HasKey(ci => new { ci.CartId, ci.ProductId });
                ent.HasOne(ci => ci.Product).WithMany(ci => ci.CartItems).HasForeignKey(ci => ci.ProductId).HasConstraintName("FK_CartItem_Product");
                ent.HasOne(ci => ci.Cart).WithMany(ci => ci.CartItems).HasPrincipalKey(ci => ci.CartId).HasConstraintName("FK_CartItem_Cart");
                ent.Property(ci => ci.BuyQuanlity).IsRequired(true);
            });

            modelBuilder.Entity<Image>(ent =>
            {
                ent.ToTable("Images");
                ent.HasKey(im => im.ImageId);
                ent.HasOne(im => im.Product).WithMany(im => im.Images).HasForeignKey(im => im.ProductId).HasConstraintName("FK_Image_Product");
                ent.Property(im=>im.ImageName).IsRequired(true);
                ent.Property(im=>im.ImageUrl).IsRequired(true);
                ent.Property(im => im.IsAvatar).HasDefaultValue(false);
            });
            List<IdentityRole> roles = new List<IdentityRole>()
            {
                new IdentityRole
                {
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                },
                new IdentityRole
                {
                    Name ="User",
                    NormalizedName="USER"
                }
            };
            modelBuilder.Entity<IdentityRole>().HasData(roles);


        }

    }
}
