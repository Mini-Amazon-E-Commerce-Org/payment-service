
using Microsoft.EntityFrameworkCore;
using PaymentServiceApi.Models;

namespace PaymentServiceApi.Data
{
    public class PaymentDbContext: DbContext
    {
        public PaymentDbContext(DbContextOptions<PaymentDbContext> options)
        : base(options) { }

        public DbSet<Payment> Payments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Payment>(entity =>
            {
                entity.Property(p => p.Amount)
                      .HasPrecision(10, 2);

                entity.Property(p => p.Status)
                .HasConversion<int>();
            });
        }

    }
}
