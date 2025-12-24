using Microsoft.EntityFrameworkCore;
using Padel.API.Courts.GetAll;
using Padel.Application.Courts;

namespace Padel.Infrastructure.Data.Courts;

internal sealed class CourtsQueryService(PadelDbContext context) : ICourtsQueryService
{
    public async Task<IReadOnlyCollection<GetAllCourtsItem>> GetAllCourtsAsync(CancellationToken cancellationToken)
    {
        return await context
            .Courts
            .AsNoTracking()
            .Select(x => new GetAllCourtsItem(x.Id, x.Name))
            .ToArrayAsync(cancellationToken);
    }
}
