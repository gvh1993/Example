using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using Padel.Infrastructure.Data;
using Padel.Infrastructure.Environment;

namespace Padel.Infrastructure;

public static class ApplicationBuilderExtensions
{
    extension(IApplicationBuilder app)
    {
        public async Task<IApplicationBuilder> UseInfrastructure(IHostEnvironment environment)
        {
            if (environment.IsDevelopmentOrTest)
            {
                await app.SeedDataAsync();
            }

            return app;
        }
    }
}
