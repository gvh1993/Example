using Microsoft.EntityFrameworkCore;
using Padel.Domain.Courts;

namespace Padel.Infrastructure.Data;

public sealed class PadelDbContext(DbContextOptions<PadelDbContext> options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(Schemas.Application);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(PadelDbContext).Assembly);
    }

    public DbSet<Court> Courts => Set<Court>();
}
