using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace PromoCode.Infrastructure.Contexts;

public class PromoCodeDbContextFactory : IDesignTimeDbContextFactory<PromoCodeDbContext>
{
    public PromoCodeDbContext CreateDbContext(string[] args)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        var optionsBuilder = new DbContextOptionsBuilder<PromoCodeDbContext>();
        var connectionString = configuration.GetConnectionString("PromoCodeDatabase");

        optionsBuilder.UseSqlServer(connectionString);

        return new PromoCodeDbContext(optionsBuilder.Options);
    }
}