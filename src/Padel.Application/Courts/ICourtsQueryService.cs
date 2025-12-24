using Padel.API.Courts.GetAll;
using Padel.Application.Courts.Get;

namespace Padel.Application.Courts;

public interface ICourtsQueryService
{
    Task<GetCourtItem?> FindCourtAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyCollection<GetAllCourtsItem>> GetAllCourtsAsync(CancellationToken cancellationToken);
}
