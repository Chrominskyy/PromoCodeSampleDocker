using System.Globalization;
using System.Reflection;
using System.Text.RegularExpressions;
using Chrominsky.Utils.Mappers.Base;
using Chrominsky.Utils.Repositories;
using Chrominsky.Utils.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using PromoCode.Application.Services;
using PromoCode.Domain.Mappers;
using PromoCode.Domain.Models;
using PromoCode.Infrastructure.Contexts;
using PromoCode.Infrastructure.Repositories;
using StackExchange.Redis;

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
    builder.Services.AddControllers();

    // Add API versioning
    builder.Services.AddApiVersioning(options =>
    {
        options.ReportApiVersions = true;
        options.AssumeDefaultVersionWhenUnspecified = true;
        options.DefaultApiVersion = new ApiVersion(1, 0);
    });

    // Add versioned API explorer
    builder.Services.AddVersionedApiExplorer(options =>
    {
        options.GroupNameFormat = "'v'VVV";
        options.SubstituteApiVersionInUrl = true;
    });

    // Add Swagger
    builder.Services.AddSwaggerGen(c =>
    {
        var provider = builder.Services.BuildServiceProvider().GetRequiredService<IApiVersionDescriptionProvider>();
        foreach (var description in provider.ApiVersionDescriptions)
        {
            c.SwaggerDoc(description.GroupName, new OpenApiInfo
            {
                Title = $"API {description.ApiVersion}",
                Version = $"PromotionalCode API - {description.ApiVersion.ToString()}",
                Description = description.ApiVersion.ToString() switch
                {
                    "1.0" => "API version 1.0 summary description. Tak jest wg dokumentacji.",
                    "2.0" => "API version 2.0 summary description. To jest ulepszona wersja, bazując na doświadczeniu programisty.",
                    _ => "API version description."
                }
            });
        }

        var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
        c.IncludeXmlComments(xmlPath);
    });

    builder.Services.AddDbContext<PromoCodeDbContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("PromoCodeDatabase")));

    builder.Services.AddSingleton<IConnectionMultiplexer>(sp =>
    {
        var configuration = ConfigurationOptions.Parse(builder.Configuration.GetConnectionString("Redis"), true);
        return ConnectionMultiplexer.Connect(configuration);
    });

    builder.Services.AddScoped<IPromotionalCodeRepository, PromotionalCodeRepository>();
    builder.Services.AddScoped<IPromotionalCodeService, PromotionalCodeService>();
    builder.Services.AddScoped<ICacheRepository, RedisCacheRepository>();
    builder.Services.AddScoped<ICacheService, RedisCacheService>();
    builder.Services.AddScoped<IObjectVersioningRepository, ObjectVersioningRepository>();
    builder.Services.AddScoped<IObjectVersioningService, ObjectVersioningService>();
    builder.Services.AddScoped<IBaseMapper<PromotionalCode, PromotionalCodeDto>, PromotionalCodeDtoMapper>();

    var app = builder.Build();

    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();
            foreach (var description in provider.ApiVersionDescriptions)
            {
                c.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", "PromotionalCode API - " + description.GroupName.ToUpperInvariant());
            }
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