using Microsoft.EntityFrameworkCore;
using SimpleTenant.Models;

namespace SimpleTenant.Contexts;

public class TenantDbContext : DbContext
{
    public TenantDbContext(DbContextOptions<TenantDbContext> options) : base(options) { }
    
    public DbSet<Tenant> Tenants { get; set; }
    public DbSet<TenantUser> TenantUsers { get; set; }
}