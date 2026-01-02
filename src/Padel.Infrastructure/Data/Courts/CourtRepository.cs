using Microsoft.EntityFrameworkCore;
using Padel.Domain.Courts;

namespace Padel.Infrastructure.Data.Courts;

internal sealed class CourtRepository(PadelDbContext dbContext) : ICourtRepository
{
    public async Task<Court?> FindAsync(Guid id, CancellationToken cancellationToken)
    {
        return await dbContext
            .Courts
            .SingleOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task UpdateAsync(Court court, CancellationToken cancellationToken)
    {
        dbContext.Courts.Update(court);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task AddAsync(Court court, CancellationToken cancellationToken)
    {
        dbContext.Courts.Add(court);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Court court, CancellationToken cancellationToken)
    {
        dbContext.Courts.Remove(court);
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}
