using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Padel.Domain.Courts;

namespace Padel.Infrastructure.Data;

internal static class Seed
{
    extension(IApplicationBuilder app)
    {
        public async Task<IApplicationBuilder> SeedDataAsync()
        {
            await using var scope = app.ApplicationServices.CreateAsyncScope();
            var context = scope.ServiceProvider.GetRequiredService<PadelDbContext>();

            if (!await context.Courts.AnyAsync())
            {
                for (var i = 0; i < 10; i++)
                {
                    context.Courts.Add(new Court(Guid.CreateVersion7(), $"Court {i}"));
                }

                await context.SaveChangesAsync();
            }

            return app;
        }
    }
}
