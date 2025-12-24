using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Padel.Infrastructure.Data;
using Padel.Infrastructure.Monitoring;

namespace Padel.Infrastructure;

public static class DependencyInjection
{
    extension(IServiceCollection services)
    {
        public IServiceCollection AddInfrastructure(
            IConfiguration configuration,
            ConfigureHostBuilder host,
            IHostEnvironment environment,
            ILoggingBuilder loggingBuilder)
        {
            services.AddData(configuration)
                .AddLogging(configuration, host, environment, loggingBuilder);

            return services;
        }
    }
}
