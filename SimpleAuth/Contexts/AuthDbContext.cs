using Microsoft.EntityFrameworkCore;
using SimpleAuth.Models;

namespace SimpleAuth.Contexts;

public class AuthDbContext : DbContext
{
    public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options) { }
    
    public DbSet<User> Users { get; set; }
}