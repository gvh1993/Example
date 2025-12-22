using Microsoft.EntityFrameworkCore;

namespace Padel.Infrastructure.Data;

public sealed class PadelDbContext(DbContextOptions<PadelDbContext> options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(Schemas.Application);
    }
}
