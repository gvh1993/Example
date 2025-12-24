using Padel.API.Courts.GetAll;
using Padel.Application.Courts.Get;

namespace Padel.Application.Courts;

public interface ICourtQueryService
{
    Task<bool> CourtExistsAsync(string name, CancellationToken cancellationToken);
    Task<GetCourtItem?> FindCourtAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyCollection<GetAllCourtsItem>> GetAllCourtsAsync(CancellationToken cancellationToken);
}
