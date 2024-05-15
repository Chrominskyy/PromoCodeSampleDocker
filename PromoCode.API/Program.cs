using System.Globalization;
using System.Reflection;
using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using PromoCode.Application.Services;
using PromoCode.Infrastructure.Contexts;
using PromoCode.Infrastructure.Repositories;

namespace PromoCode.API;

public class Program
{
    public static void Main(string[] args)
    {
        CultureInfo.DefaultThreadCurrentCulture = CultureInfo.InvariantCulture;
        CultureInfo.DefaultThreadCurrentUICulture = CultureInfo.InvariantCulture;
        
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddAuthorization();

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddControllers();
        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "PromotionalCode API", Version = "v1" });
        });

        // Register the DbContext with the correct connection string
        builder.Services.AddDbContext<PromoCodeDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("PromoCodeDatabase")));

        builder.Services.AddScoped<IPromotionalCodeRepository, PromotionalCodeRepository>();
        builder.Services.AddScoped<IPromotionalCodeService, PromotionalCodeService>();
        // InstallServicesByReflection(builder.Services, "Repository");
        // InstallServicesByReflection(builder.Services, "Service");
        
        var app = builder.Build();
        // Ensure the database is created and migrated on startup
        // using (var scope = app.Services.CreateScope())
        // {
        //     var services = scope.ServiceProvider;
        //     var dbContext = services.GetRequiredService<PromoCodeDbContext>();
        //     dbContext.Database.Migrate(); // This method will automatically apply all pending migrations
        // }

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "PromotionalCode API V1");
            });
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();
        app.MapControllers();

        app.Run();
    }

    #region AddAutomaticServices

    static void InstallServicesByReflection(IServiceCollection services, string regexPatternObjectName)
    {
        var regex = new Regex("I[A-Z][a-zA-z]*"+regexPatternObjectName);

        var types = Assembly.GetEntryAssembly()?.DefinedTypes
            .Where(x => x.IsClass)
            .Where(x => x.ImplementedInterfaces.Any(z => regex.IsMatch(z.Name)))
            .Where(x => !x.IsGenericType);

        foreach (var classType in types)
        {
            var regextInterfaceName = new Regex("I" + classType.Name);
            var interfaceObject =
                classType.ImplementedInterfaces.FirstOrDefault(x => regextInterfaceName.IsMatch(x.Name));

            if (interfaceObject is not null)
            {
                services.AddScoped(Type.GetType(interfaceObject.FullName), Type.GetType(classType.FullName));
            }

            Console.WriteLine($"Registering: {interfaceObject.FullName} with {classType.FullName}");
        }
    }

    #endregion
}