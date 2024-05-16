using Microsoft.EntityFrameworkCore;

namespace SimpleTenant.Contexts;

public class TenantDbContextFactory
{
    public TenantDbContext CreateDbContext(string[] args)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        var optionsBuilder = new DbContextOptionsBuilder<TenantDbContext>();
        var connectionString = configuration.GetConnectionString("DefaultDbContext");

        optionsBuilder.UseSqlServer(connectionString);

        return new TenantDbContext(optionsBuilder.Options);
    }
}