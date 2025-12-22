using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Padel.Infrastructure.Data;

namespace Padel.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<PadelDbContext>(x => x
            .UseNpgsql(configuration.GetConnectionString("Padel"), y => y.EnableRetryOnFailure()));

        return services;
    }
}
