using Microsoft.EntityFrameworkCore;
using Padel.API.Courts.GetAll;
using Padel.Application.Courts;
using Padel.Application.Courts.Get;

namespace Padel.Infrastructure.Data.Courts;

internal sealed class CourtQueryService(PadelDbContext context) : ICourtQueryService
{
    public Task<bool> CourtExistsAsync(string name, CancellationToken cancellationToken)
    {
        return context
            .Courts
            .AsNoTracking()
            .AnyAsync(x => x.Name == name, cancellationToken);
    }

    public Task<GetCourtItem?> FindCourtAsync(Guid id, CancellationToken cancellationToken)
    {
        return context
            .Courts
            .AsNoTracking()
            .Where(x => x.Id == id)
            .Select(x => new GetCourtItem(x.Id, x.Name))
            .SingleOrDefaultAsync(cancellationToken);
    }

    public async Task<IReadOnlyCollection<GetAllCourtsItem>> GetAllCourtsAsync(CancellationToken cancellationToken)
    {
        return await context
            .Courts
            .AsNoTracking()
            .Select(x => new GetAllCourtsItem(x.Id, x.Name))
            .ToArrayAsync(cancellationToken);
    }
}
