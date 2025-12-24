using Padel.Domain.Courts;

namespace Padel.Infrastructure.Data.Courts;

internal sealed class CourtRepository(PadelDbContext dbContext) : ICourtRepository
{
    public async Task AddAsync(Court court, CancellationToken cancellationToken)
    {
        dbContext.Courts.Add(court);
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}
