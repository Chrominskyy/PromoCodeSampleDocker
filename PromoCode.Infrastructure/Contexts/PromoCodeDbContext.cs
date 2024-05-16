using Microsoft.EntityFrameworkCore;
using PromoCode.Domain.Models;

namespace PromoCode.Infrastructure.Contexts;
public class PromoCodeDbContext : DbContext
{
    public PromoCodeDbContext(DbContextOptions<PromoCodeDbContext> options) : base(options) { }

    public DbSet<PromotionalCode?>? PromotionalCodes { get; set; }
    
    public DbSet<ObjectVersioning?>? ObjectVersionings { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configure your model here
    }
}