using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Padel.Application.Courts;
using Padel.Domain.Courts;
using Padel.Infrastructure.Data.Courts;

namespace Padel.Infrastructure.Data;

internal static class ServiceCollectionExtensions
{
    extension(IServiceCollection services)
    {
        public IServiceCollection AddData(IConfiguration configuration)
        {
            var padelConnectionString = configuration.GetConnectionString("Padel")
                                        ?? throw new InvalidOperationException(
                                            "The Padel database connectionstring should not be null");

            services.AddDbContext<PadelDbContext>(x => x
                .UseNpgsql(padelConnectionString, y => y.EnableRetryOnFailure()));

            services.AddHealthChecks()
                .AddNpgSql(padelConnectionString);

            services.AddScoped<ICourtQueryService, CourtQueryService>();
            services.AddScoped<ICourtRepository, CourtRepository>();
            return services;
        }
    }
}
