using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Padel.Infrastructure.Data;

namespace Padel.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var padelConnectionString = configuration.GetConnectionString("Padel")
                                    ?? throw new NullReferenceException(message: "The Padel database connectionstring should not be null");

        services.AddDbContext<PadelDbContext>(x => x
            .UseNpgsql(padelConnectionString, y => y.EnableRetryOnFailure()));

        services.AddHealthChecks()
            .AddNpgSql(padelConnectionString);

        return services;
    }
}
