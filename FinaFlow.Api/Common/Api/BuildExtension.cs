using FinaFlow.Api.Data;
using FinaFlow.Api.Handler;
using FinaFlow.Core;
using FinaFlow.Core.Handler;
using Microsoft.EntityFrameworkCore;

namespace FinaFlow.Api.Common.Api;

public static class BuildExtension
{
    public static void AddConfiguration(this WebApplicationBuilder builder)
    {
        ApiConfigurations.ConncetionString =
            builder.Configuration.GetConnectionString("DefaultConnection") ?? string.Empty;
        Configurations.BackendUrl =
            builder.Configuration.GetValue<string>("BackendUrl") ?? string.Empty;
        Configurations.FrontendUrl =
            builder.Configuration.GetValue<string>("FrontendUrl") ?? string.Empty;
    }

    public static void AddDocumentation(this WebApplicationBuilder builder)
    {
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(x =>
        {
            x.CustomSchemaIds(n => n.FullName);
        });
    }

    public static void AddDbContexts(this WebApplicationBuilder builder)
    {
        builder
            .Services
            .AddDbContext<AppDbContext>(x =>
            {
                x.UseSqlServer(ApiConfigurations.ConncetionString);
            });
    }

    // CORS -> Cross Origin Resource Sharing
    public static void AddCrossOrigin(this WebApplicationBuilder builder)
    {
        builder.Services.AddCors(
            options => options.AddPolicy(
                ApiConfigurations.CorsPolicyName,
                policy => policy
                .WithOrigins([
                    Configurations.BackendUrl,
                    Configurations.FrontendUrl
                    ])
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials()
                ));
    }

    public static void AddServices(this WebApplicationBuilder builder)
    {
        builder
            .Services
            .AddTransient<ICategoryHandler, CategoryHandler>()
            .AddTransient<ITransactionHandler, TransactionHandler>();
    }
}
