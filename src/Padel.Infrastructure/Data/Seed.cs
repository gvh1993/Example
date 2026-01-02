using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Padel.Domain;
using Padel.Domain.Courts;
using Padel.Domain.Player;

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
                    context.Courts.Add(new Court(
                        Guid.CreateVersion7(),
                        $"Court {i}"));
                }

                await context.SaveChangesAsync();
            }

            if (!await context.Players.AnyAsync())
            {
                for (var i = 0; i < 10; i++)
                {
                    context.Players.Add(new Player(
                        Guid.CreateVersion7(),
                        "Player",
                        $"{i}",
                        DateTime.UtcNow,
                        EmailAddress.Create($"player{i}@example.com").Value,
                        PhoneNumber.Create($"123-456-789{i}").Value));
                }
                await context.SaveChangesAsync();
            }

            return app;
        }
    }
}
