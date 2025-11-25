using E_com_Web.Areas.Admin.Models;
using E_com_Web.Models;
using Microsoft.EntityFrameworkCore;

namespace E_com_Web.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Shoe> Shoes { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<OrderCustomer> OrderCustomers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure Shoe entity
            modelBuilder.Entity<Shoe>(entity =>
            {
                entity.Property(e => e.Price).HasPrecision(18, 2);
                entity.Property(e => e.Rating).HasPrecision(3, 1);

                entity.Property(e => e.Sizes)
                    .HasConversion(
                        v => string.Join(',', v),
                        v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList())
                    .Metadata.SetValueComparer(
                        new Microsoft.EntityFrameworkCore.ChangeTracking.ValueComparer<List<string>>(
                            (c1, c2) => c1!.SequenceEqual(c2!),
                            c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
                            c => c.ToList()));

                entity.Property(e => e.Colors)
                    .HasConversion(
                        v => string.Join(',', v),
                        v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList())
                    .Metadata.SetValueComparer(
                        new Microsoft.EntityFrameworkCore.ChangeTracking.ValueComparer<List<string>>(
                            (c1, c2) => c1!.SequenceEqual(c2!),
                            c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
                            c => c.ToList()));
            });

            // Configure Product entity
            modelBuilder.Entity<Product>(entity =>
            {
                entity.Property(e => e.Price).HasPrecision(18, 2);
            });

            // Configure Order entity
            modelBuilder.Entity<Order>(entity =>
            {
                entity.Property(e => e.TotalAmount).HasPrecision(18, 2);

                entity.HasOne(o => o.Customer)
                    .WithOne(c => c.Order)
                    .HasForeignKey<OrderCustomer>(c => c.OrderId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasMany(o => o.OrderItems)
                    .WithOne(oi => oi.Order)
                    .HasForeignKey(oi => oi.OrderId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // Configure OrderItem entity
            modelBuilder.Entity<OrderItem>(entity =>
            {
                entity.Property(e => e.PricePerUnit).HasPrecision(18, 2);
                entity.Property(e => e.Subtotal).HasPrecision(18, 2);
            });
        }
    }
}