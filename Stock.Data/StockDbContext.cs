using MassTransit;
using Microsoft.EntityFrameworkCore;
using Stock.Entity;

namespace Stock.Data
{
    public class StockDbContext : DbContext
    {
        public StockDbContext(DbContextOptions<StockDbContext> options)
            : base(options)
        {
        }

        public DbSet<ProductStock> ProductStocks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ProductStock>(entity =>
            {
                entity.ToTable("ProductStocks");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.ProductId).IsRequired();
                entity.Property(e => e.Quantity).IsRequired();
                entity.Property(e => e.LastUpdated).IsRequired();
                entity.Property(e => e.Version)
                    .HasColumnName("xmin")
                    .HasColumnType("xid")
                    .ValueGeneratedOnAddOrUpdate()
                    .IsConcurrencyToken();
                entity.HasIndex(e => e.ProductId).IsUnique();
            });

            modelBuilder.AddInboxStateEntity();
            modelBuilder.AddOutboxMessageEntity();
            modelBuilder.AddOutboxStateEntity();
        }
    }
}
