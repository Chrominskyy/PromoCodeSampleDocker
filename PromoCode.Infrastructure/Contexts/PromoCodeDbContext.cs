using Microsoft.EntityFrameworkCore;
using PromoCode.Domain.Models;

namespace PromoCode.Infrastructure.Contexts;

// public class PromoCodeDbContext(DbContextOptions<PromoCodeDbContext> options, DbSet<PromotionalCode> promotionalCodes) : DbContext(options)
// {
//     public DbSet<PromotionalCode> PromotionalCodes { get; set; } = promotionalCodes;
// }
public class PromoCodeDbContext : DbContext
{
    public PromoCodeDbContext(DbContextOptions<PromoCodeDbContext> options) : base(options) { }

    public DbSet<PromotionalCode?>? PromotionalCodes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configure your model here
    }
}